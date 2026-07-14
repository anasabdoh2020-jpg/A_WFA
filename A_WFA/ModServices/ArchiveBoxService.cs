using A_WFA.Database.LTE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;

namespace A_WFA.ModServices
{
    /// <summary>
    /// خدمة إدارة الصناديق والأرشيف
    /// </summary>
    public static class ArchiveBoxService
    {
        #region "جلب بيانات الصناديق"

        /// <summary>
        /// الحصول على صندوق بواسطة المعرف
        /// </summary>
        public static DataRow GetBox(int boxId)
        {
            try
            {
                string query = "SELECT * FROM Boxes WHERE id = @id";
                var parameters = new Dictionary<string, object> { { "@id", boxId } };
                DataTable dt = DatabaseModuleLite.ExecuteQuery(query, parameters);

                if (dt != null && dt.Rows.Count > 0)
                    return dt.Rows[0];

                return null;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ خطأ في GetBox: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// الحصول على جميع الصناديق
        /// </summary>
        public static DataTable GetAllBoxes(bool activeOnly = false)
        {
            try
            {
                string query = @"
                    SELECT id, name, archiveBox_number, details, image_path, is_active
                    FROM Boxes";

                if (activeOnly)
                    query += " WHERE is_active = 1";

                query += " ORDER BY name";
                return DatabaseModuleLite.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ خطأ في GetAllBoxes: {ex.Message}");
                return new DataTable();
            }
        }

        /// <summary>
        /// الحصول على اسم الصندوق
        /// </summary>
        public static string GetBoxName(int boxId)
        {
            try
            {
                DataRow box = GetBox(boxId);
                return box == null || box["name"] == DBNull.Value
                    ? "صندوق غير معروف"
                    : box["name"].ToString();
            }
            catch
            {
                return "صندوق غير معروف";
            }
        }

        /// <summary>
        /// الحصول على رقم الصندوق الأرشيفي
        /// </summary>
        public static string GetArchiveBoxNumber(int boxId)
        {
            try
            {
                DataRow box = GetBox(boxId);
                if (box == null || box["archiveBox_number"] == DBNull.Value)
                    return $"ARCH-{boxId:000}";
                return box["archiveBox_number"].ToString();
            }
            catch
            {
                return $"ARCH-{boxId:000}";
            }
        }

        /// <summary>
        /// الحصول على الرقم الأرشيفي الكامل
        /// </summary>
        public static string GetFullArchiveNumber(int boxId, int docSequence)
        {
            string boxNumber = GetArchiveBoxNumber(boxId);
            return $"{boxNumber}-{docSequence:000}";
        }

        /// <summary>
        /// التحقق من وجود صندوق
        /// </summary>
        public static bool BoxExists(int boxId)
        {
            return GetBox(boxId) != null;
        }

        /// <summary>
        /// التحقق من نشاط الصندوق
        /// </summary>
        public static bool IsBoxActive(int boxId)
        {
            try
            {
                DataRow box = GetBox(boxId);
                return box != null &&
                       box["is_active"] != DBNull.Value &&
                       Convert.ToBoolean(box["is_active"]);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// الحصول على عدد الوثائق في الصندوق
        /// </summary>
        public static int GetDocumentCount(int boxId)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM Documents WHERE box_id = @boxId";
                var parameters = new Dictionary<string, object> { { "@boxId", boxId } };
                object result = DatabaseModuleLite.ExecuteScalar(query, parameters);
                return result != null && result != DBNull.Value
                    ? Convert.ToInt32(result)
                    : 0;
            }
            catch
            {
                return 0;
            }
        }

        #endregion

        #region "أرقام الصناديق"

        /// <summary>
        /// توليد رقم الصندوق التالي - SQLite
        /// مثال: ARCH-001, ARCH-002, ...
        /// </summary>
        public static string GetNextArchiveBoxNumber()
        {
            try
            {
                // التحقق من وجود قاعدة البيانات أولاً
                if (!DatabaseManagerLite.DatabaseExists())
                {
                    System.Diagnostics.Debug.WriteLine("⚠️ قاعدة البيانات غير موجودة");
                    return "ARCH-001";
                }

                // ✅ استعلام SQLite
                string query = @"
                    SELECT archiveBox_number
                    FROM Boxes
                    WHERE archiveBox_number IS NOT NULL
                    AND archiveBox_number LIKE 'ARCH-%'
                    ORDER BY id DESC";

                DataTable dt = DatabaseModuleLite.ExecuteQuery(query);

                if (dt == null || dt.Rows.Count == 0)
                {
                    return "ARCH-001";
                }

                int maxNumber = 0;
                foreach (DataRow row in dt.Rows)
                {
                    try
                    {
                        string value = row["archiveBox_number"]?.ToString();

                        if (!string.IsNullOrEmpty(value) && value.StartsWith("ARCH-"))
                        {
                            string numPart = value.Substring(5);
                            if (int.TryParse(numPart, out int num))
                            {
                                if (num > maxNumber)
                                    maxNumber = num;
                            }
                        }
                    }
                    catch
                    {
                        // تجاهل الأخطاء في الصف الواحد
                        continue;
                    }
                }

                return $"ARCH-{maxNumber + 1:000}";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ خطأ في توليد رقم الأرشيف: {ex.Message}");
                return "ARCH-001";
            }
        }

        #endregion

        #region "إضافة صندوق (مع مسار الصورة)"

        /// <summary>
        /// إضافة صندوق جديد مع حفظ الصورة في نظام الملفات
        /// </summary>
        public static int AddBox(
            string name,
            string archiveBoxNumber,
            string details,
            string imagePath,
            bool isActive = true)
        {
            try
            {
                // ✅ استخدام DatabaseManagerLite.AddBox
                return DatabaseManagerLite.AddBox(
                    name,
                    imagePath,
                    DateTime.Now.ToString("yyyy-MM-dd"),
                    details,
                    isActive
                );
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ خطأ في AddBox: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// إضافة صندوق جديد مع صورة (محولة إلى مسار)
        /// </summary>
        public static int AddBoxWithImage(
            string name,
            string archiveBoxNumber,
            string details,
            Image boxImage,
            bool isActive = true)
        {
            try
            {
                string imagePath = null;

                // حفظ الصورة في نظام الملفات
                if (boxImage != null)
                {
                    string imagesFolder = Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        "A_WFA",
                        "Images",
                        "Boxes"
                    );

                    if (!Directory.Exists(imagesFolder))
                        Directory.CreateDirectory(imagesFolder);

                    string fileName = $"box_{DateTime.Now:yyyyMMdd_HHmmss}_{Guid.NewGuid().ToString().Substring(0, 8)}.png";
                    imagePath = Path.Combine(imagesFolder, fileName);

                    boxImage.Save(imagePath, System.Drawing.Imaging.ImageFormat.Png);
                }

                return DatabaseManagerLite.AddBox(
                    name,
                    imagePath,
                    DateTime.Now.ToString("yyyy-MM-dd"),
                    details,
                    isActive
                );
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ خطأ في AddBoxWithImage: {ex.Message}");
                throw;
            }
        }

        #endregion

        #region "تعديل وحذف الصندوق"

        /// <summary>
        /// تحديث صندوق
        /// </summary>
        public static bool UpdateBox(
            int id,
            string name,
            string archiveBoxNumber,
            string details,
            string imagePath,
            bool active)
        {
            try
            {
                // ✅ استخدام DatabaseManagerLite.UpdateBox
                return DatabaseManagerLite.UpdateBox(
                    id,
                    name,
                    imagePath,
                    DateTime.Now.ToString("yyyy-MM-dd"),
                    details,
                    active
                );
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ خطأ في UpdateBox: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// تحديث صندوق مع صورة
        /// </summary>
        public static bool UpdateBoxWithImage(
            int id,
            string name,
            string archiveBoxNumber,
            string details,
            Image image,
            bool active)
        {
            try
            {
                string imagePath = null;

                if (image != null)
                {
                    string imagesFolder = Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        "A_WFA",
                        "Images",
                        "Boxes"
                    );

                    if (!Directory.Exists(imagesFolder))
                        Directory.CreateDirectory(imagesFolder);

                    string fileName = $"box_{DateTime.Now:yyyyMMdd_HHmmss}_{Guid.NewGuid().ToString().Substring(0, 8)}.png";
                    imagePath = Path.Combine(imagesFolder, fileName);

                    image.Save(imagePath, System.Drawing.Imaging.ImageFormat.Png);
                }

                return DatabaseManagerLite.UpdateBox(
                    id,
                    name,
                    imagePath,
                    DateTime.Now.ToString("yyyy-MM-dd"),
                    details,
                    active
                );
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ خطأ في UpdateBoxWithImage: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// حذف صندوق
        /// </summary>
        public static bool DeleteBox(int id)
        {
            try
            {
                // ✅ استخدام DatabaseManagerLite.DeleteBox
                return DatabaseManagerLite.DeleteBox(id);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ خطأ في DeleteBox: {ex.Message}");
                return false;
            }
        }

        #endregion

        #region "صور الصناديق (مخزنة في نظام الملفات)"

        /// <summary>
        /// الحصول على صورة الصندوق من المسار المخزن في قاعدة البيانات
        /// </summary>
        public static Image GetBoxImage(int boxId)
        {
            try
            {
                DataRow box = GetBox(boxId);
                if (box == null)
                {
                    System.Diagnostics.Debug.WriteLine($"❌ الصندوق {boxId} غير موجود");
                    return null;
                }

                // ✅ استخدام مسار الصورة (image_path)
                if (box["image_path"] != DBNull.Value && box["image_path"] != null)
                {
                    string imagePath = box["image_path"].ToString();

                    if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
                    {
                        System.Diagnostics.Debug.WriteLine($"✅ تم العثور على الصورة: {imagePath}");
                        return Image.FromFile(imagePath);
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine($"⚠️ الملف غير موجود: {imagePath}");
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"⚠️ لا يوجد مسار صورة للصندوق {boxId}");
                }

                return null;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ خطأ في تحميل صورة الصندوق: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// الحصول على مسار صورة الصندوق
        /// </summary>
        public static string GetBoxImagePath(int boxId)
        {
            try
            {
                DataRow box = GetBox(boxId);
                if (box == null || box["image_path"] == DBNull.Value)
                    return null;

                return box["image_path"].ToString();
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region "أرقام الوثائق داخل الصندوق - SQLite"

        /// <summary>
        /// الحصول على الرقم التسلسلي التالي للوثيقة - SQLite
        /// </summary>
        public static int GetNextDocumentSequence(int boxId)
        {
            try
            {
                // ✅ استعلام SQLite
                string query = @"
                    SELECT COALESCE(MAX(CAST(archiveDoc_number AS INTEGER)), 0)
                    FROM Documents
                    WHERE box_id = @boxId
                    AND archiveDoc_number IS NOT NULL
                    AND archiveDoc_number <> ''";

                var parameters = new Dictionary<string, object>
                {
                    { "@boxId", boxId }
                };

                object result = DatabaseModuleLite.ExecuteScalar(query, parameters);
                int maxNumber = result != null && result != DBNull.Value
                    ? Convert.ToInt32(result)
                    : 0;

                return maxNumber + 1;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ خطأ في GetNextDocumentSequence: {ex.Message}");
                return 1;
            }
        }

        /// <summary>
        /// الحصول على الرقم التسلسلي الحالي للوثيقة
        /// </summary>
        public static int GetCurrentDocumentSequence(int boxId)
        {
            try
            {
                // ✅ استعلام SQLite
                string query = @"
                    SELECT COALESCE(MAX(CAST(archiveDoc_number AS INTEGER)), 0)
                    FROM Documents
                    WHERE box_id = @boxId
                    AND archiveDoc_number IS NOT NULL
                    AND archiveDoc_number <> ''";

                var parameters = new Dictionary<string, object>
                {
                    { "@boxId", boxId }
                };

                object result = DatabaseModuleLite.ExecuteScalar(query, parameters);
                return result != null && result != DBNull.Value
                    ? Convert.ToInt32(result)
                    : 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ خطأ في GetCurrentDocumentSequence: {ex.Message}");
                return 0;
            }
        }

        /// <summary>
        /// زيادة الرقم التسلسلي
        /// </summary>
        public static int IncreaseSequence(int current)
        {
            return current + 1;
        }

        /// <summary>
        /// إنقاص الرقم التسلسلي
        /// </summary>
        public static int DecreaseSequence(int current)
        {
            return current > 1 ? current - 1 : 1;
        }

        /// <summary>
        /// التحقق من صحة الرقم التسلسلي
        /// </summary>
        public static bool IsValidSequence(int sequence)
        {
            return sequence >= 1;
        }

        #endregion

        #region "دوال مساعدة إضافية"

        /// <summary>
        /// التحقق من وجود صندوق بالرقم
        /// </summary>
        public static bool BoxNumberExists(string archiveNumber)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM Boxes WHERE archiveBox_number = @number";
                var parameters = new Dictionary<string, object> { { "@number", archiveNumber } };
                object result = DatabaseModuleLite.ExecuteScalar(query, parameters);
                return result != null && Convert.ToInt32(result) > 0;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// الحصول على جميع أرقام الصناديق
        /// </summary>
        public static List<string> GetAllBoxNumbers()
        {
            var numbers = new List<string>();
            try
            {
                string query = "SELECT archiveBox_number FROM Boxes WHERE archiveBox_number IS NOT NULL ORDER BY archiveBox_number";
                DataTable dt = DatabaseModuleLite.ExecuteQuery(query);
                foreach (DataRow row in dt.Rows)
                {
                    numbers.Add(row["archiveBox_number"].ToString());
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ خطأ في GetAllBoxNumbers: {ex.Message}");
            }
            return numbers;
        }

        #endregion
    }
}