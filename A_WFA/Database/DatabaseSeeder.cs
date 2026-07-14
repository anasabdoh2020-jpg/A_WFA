using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace A_WFA
{
    /// <summary>
    /// كلاس مسؤول عن إدراج البيانات الافتراضية (Seed Data) في قاعدة البيانات
    /// </summary>
    public static class DatabaseSeeder
    {
        #region "البيانات الثابتة"

        // أنواع الوثائق
        private static readonly string[] DocumentTypes = {
            "مراسلة رسمية",
            "تقرير",
            "عقد",
            "قرار",
            "محضر اجتماع",
            "خطاب",
            "مذكرة",
            "كتاب واردة",
            "كتاب صادر",
            "طلب",
            "إذن",
            "شهادة",
            "سجل"
        };

        // تصنيفات الوثائق
        private static readonly string[] DocumentCategories = {
            "شؤون الموظفين",
            "شؤون مالية",
            "شؤون إدارية",
            "شؤون تقنية",
            "شؤون قانونية",
            "شؤون تدريب",
            "شؤون لوجستية",
            "شؤون أمنية",
            "شؤون صحية",
            "شؤون تعليمية"
        };

        // الأقسام
        private static readonly string[] Departments = {
            "إدارة الموارد البشرية",
            "إدارة المالية",
            "إدارة التقنية",
            "إدارة الشؤون القانونية",
            "إدارة العمليات",
            "إدارة التدريب",
            "إدارة الأمن",
            "إدارة اللوجستية",
            "إدارة التخطيط",
            "إدارة الجودة"
        };

        // أسماء صناديق افتراضية
        private static readonly string[] BoxNames = {
            "صندوق المراسلات 2024",
            "صندوق التقارير 2024",
            "صندوق العقود 2024",
            "صندوق القرارات 2024",
            "صندوق المحاضر 2024",
            "صندوق الخطابات 2024",
            "صندوق المذكرات 2024",
            "صندوق الكتب الواردة 2024",
            "صندوق الكتب الصادرة 2024",
            "صندوق الطلبات 2024"
        };

        // أسماء الجنود/الأفراد
        private static readonly (string Name, string Unite, string NationalId)[] Soldiers = {
            ("أحمد محمد العلي", "الوحدة الأولى", "123456789"),
            ("خالد سعيد الحمادي", "الوحدة الثانية", "987654321"),
            ("سلمان عبدالله الفهد", "الوحدة الثالثة", "456789123"),
            ("ناصر علي الشمراني", "الوحدة الأولى", "789123456"),
            ("فهد عبدالعزيز الدوسري", "الوحدة الثانية", "321654987"),
            ("تركي بن محمد السبيعي", "الوحدة الثالثة", "654987321"),
            ("بندر ناصر العتيبي", "الوحدة الأولى", "147258369"),
            ("سلطان فهد المطيري", "الوحدة الثانية", "258369147"),
            ("منصور خالد الحربي", "الوحدة الثالثة", "369147258"),
            ("سعد عبدالله الغامدي", "الوحدة الأولى", "159357486")
        };

        // عناوين وثائق افتراضية
        private static readonly string[] DocumentTitles = {
            "طلب توظيف جديد",
            "تقرير الأداء السنوي",
            "عقد توريد معدات",
            "قرار إداري رقم ١٢",
            "محضر اجتماع اللجنة الفنية",
            "خطاب رسمي إلى الجهة المختصة",
            "مذكرة داخلية بشأن الإجازات",
            "كتاب واردة من الوزارة",
            "كتاب صادر إلى الإدارة العامة",
            "طلب صرف مكافأة",
            "إذن سفر رسمي",
            "شهادة خبرة للموظف",
            "سجل حضور وانصراف",
            "تقرير المشروع النهائي",
            "عقد خدمات استشارية",
            "قرار تشكيل لجنة",
            "محضر تسليم واستلام",
            "خطاب شكر وتقدير",
            "مذكرة طلب احتياجات",
            "كتاب دوري رقم ٥"
        };

        // حالات الوثائق
        private static readonly string[] Statuses = {
            "جديدة",
            "قيد المراجعة",
            "معتمد",
            "منفذ",
            "مؤرشف",
            "ملغي",
            "معلق"
        };

        // الأولويات
        private static readonly string[] Priorities = {
            "عادية",
            "مهمة",
            "عاجلة",
            "فورية"
        };

        // أنواع العلاقات في DocumentSoldiers
        private static readonly string[] RelationshipTypes = {
            "مقدم طلب",
            "مسؤول",
            "شاهد",
            "معني بالأمر",
            "مخول بالتوقيع",
            "مستلم",
            "مرسل",
            "متابع"
        };

        #endregion

        #region "الدالة الرئيسية"

        /// <summary>
        /// إدراج جميع البيانات الافتراضية
        /// </summary>
        /// <param name="seedDocuments">هل يتم إدراج وثائق افتراضية؟</param>
        /// <param name="seedSoldiers">هل يتم إدراج جنود افتراضيين؟</param>
        /// <param name="count">عدد السجلات الافتراضية لكل جدول</param>
        /// <returns>عدد السجلات المدرجة</returns>
        public static int SeedAll(bool seedDocuments = true, bool seedSoldiers = true, int count = 10)
        {
            int totalInserted = 0;

            try
            {
                Debug.WriteLine("🚀 بدء إدراج البيانات الافتراضية...");

                // 1. إدراج أنواع الوثائق
                totalInserted += SeedDocumentTypes();

                // 2. إدراج تصنيفات الوثائق
                totalInserted += SeedDocumentCategories();

                // 3. إدراج الأقسام
                totalInserted += SeedDepartments();

                // 4. إدراج المستخدمين
                totalInserted += SeedUsers();

                // 5. إدراج الصناديق
                totalInserted += SeedBoxes(count);

                // 6. إدراج الجنود (إذا كان مطلوباً)
                if (seedSoldiers)
                {
                    totalInserted += SeedSoldiers();
                }

                // 7. إدراج الوثائق (إذا كان مطلوباً)
                if (seedDocuments)
                {
                    totalInserted += SeedDocuments(count * 2);
                }

                // 8. إدراج علاقات الوثائق بالجنود (إذا كان مطلوباً)
                if (seedDocuments && seedSoldiers)
                {
                    totalInserted += SeedDocumentSoldiers(count);
                }

                Debug.WriteLine($"✅ تم إدراج {totalInserted} سجل بنجاح");
                return totalInserted;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"❌ فشل في إدراج البيانات: {ex.Message}");
                throw new Exception($"فشل في إدراج البيانات الافتراضية: {ex.Message}", ex);
            }
        }

        #endregion

        #region "دوال إدراج البيانات"

        /// <summary>
        /// 1. إدراج أنواع الوثائق
        /// </summary>
        private static int SeedDocumentTypes()
        {
            int inserted = 0;

            try
            {
                // ✅ استخدام DatabaseManagerLite
                string checkQuery = "SELECT COUNT(*) FROM Document_Types";
                int count = Convert.ToInt32(DatabaseManagerLite.ExecuteScalar(checkQuery));

                if (count > 0)
                {
                    Debug.WriteLine("ℹ️ أنواع الوثائق موجودة مسبقاً، تخطي...");
                    return 0;
                }

                foreach (string type in DocumentTypes)
                {
                    string sql = @"
                        INSERT INTO Document_Types (name, description, is_active) 
                        VALUES (@name, @desc, 1)";

                    var parameters = new Dictionary<string, object>
                    {
                        { "@name", type },
                        { "@desc", $"نوع وثيقة: {type}" }
                    };

                    DatabaseManagerLite.ExecuteNonQuery(sql, parameters);
                    inserted++;
                }

                Debug.WriteLine($"✅ تم إدراج {inserted} نوع وثيقة");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"⚠️ خطأ في إدراج أنواع الوثائق: {ex.Message}");
            }

            return inserted;
        }

        /// <summary>
        /// 2. إدراج تصنيفات الوثائق
        /// </summary>
        private static int SeedDocumentCategories()
        {
            int inserted = 0;

            try
            {
                string checkQuery = "SELECT COUNT(*) FROM Document_Categories";
                int count = Convert.ToInt32(DatabaseManagerLite.ExecuteScalar(checkQuery));

                if (count > 0)
                {
                    Debug.WriteLine("ℹ️ تصنيفات الوثائق موجودة مسبقاً، تخطي...");
                    return 0;
                }

                foreach (string category in DocumentCategories)
                {
                    string sql = @"
                        INSERT INTO Document_Categories (name, description, is_active) 
                        VALUES (@name, @desc, 1)";

                    var parameters = new Dictionary<string, object>
                    {
                        { "@name", category },
                        { "@desc", $"تصنيف: {category}" }
                    };

                    DatabaseManagerLite.ExecuteNonQuery(sql, parameters);
                    inserted++;
                }

                Debug.WriteLine($"✅ تم إدراج {inserted} تصنيف وثيقة");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"⚠️ خطأ في إدراج تصنيفات الوثائق: {ex.Message}");
            }

            return inserted;
        }

        /// <summary>
        /// 3. إدراج الأقسام
        /// </summary>
        private static int SeedDepartments()
        {
            int inserted = 0;

            try
            {
                string checkQuery = "SELECT COUNT(*) FROM Departments";
                int count = Convert.ToInt32(DatabaseManagerLite.ExecuteScalar(checkQuery));

                if (count > 0)
                {
                    Debug.WriteLine("ℹ️ الأقسام موجودة مسبقاً، تخطي...");
                    return 0;
                }

                foreach (string dept in Departments)
                {
                    string sql = @"
                        INSERT INTO Departments (name, description, is_active) 
                        VALUES (@name, @desc, 1)";

                    var parameters = new Dictionary<string, object>
                    {
                        { "@name", dept },
                        { "@desc", $"قسم: {dept}" }
                    };

                    DatabaseManagerLite.ExecuteNonQuery(sql, parameters);
                    inserted++;
                }

                Debug.WriteLine($"✅ تم إدراج {inserted} قسم");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"⚠️ خطأ في إدراج الأقسام: {ex.Message}");
            }

            return inserted;
        }

        /// <summary>
        /// 4. إدراج المستخدمين
        /// </summary>
        private static int SeedUsers()
        {
            int inserted = 0;

            try
            {
                string checkQuery = "SELECT COUNT(*) FROM Users";
                int count = Convert.ToInt32(DatabaseManagerLite.ExecuteScalar(checkQuery));

                if (count > 0)
                {
                    Debug.WriteLine("ℹ️ المستخدمون موجودون مسبقاً، تخطي...");
                    return 0;
                }

                // مستخدمين افتراضيين
                var users = new[]
                {
                    new { Username = "admin", Password = "admin123", FullName = "مدير النظام", Role = "Admin" },
                    new { Username = "manager", Password = "manager123", FullName = "مدير عام", Role = "Manager" },
                    new { Username = "user1", Password = "user123", FullName = "مستخدم أول", Role = "User" },
                    new { Username = "user2", Password = "user123", FullName = "مستخدم ثاني", Role = "User" },
                    new { Username = "archivist", Password = "arch123", FullName = "أمين أرشيف", Role = "Archivist" }
                };

                foreach (var user in users)
                {
                    string sql = @"
                        INSERT INTO Users (username, password_hash, full_name, role, is_active) 
                        VALUES (@username, @password, @fullName, @role, 1)";

                    var parameters = new Dictionary<string, object>
                    {
                        { "@username", user.Username },
                        { "@password", user.Password },
                        { "@fullName", user.FullName },
                        { "@role", user.Role }
                    };

                    DatabaseManagerLite.ExecuteNonQuery(sql, parameters);
                    inserted++;
                }

                Debug.WriteLine($"✅ تم إدراج {inserted} مستخدم");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"⚠️ خطأ في إدراج المستخدمين: {ex.Message}");
            }

            return inserted;
        }

        /// <summary>
        /// 5. إدراج الصناديق
        /// </summary>
        private static int SeedBoxes(int count)
        {
            int inserted = 0;

            try
            {
                string checkQuery = "SELECT COUNT(*) FROM Boxes";
                int existingCount = Convert.ToInt32(DatabaseManagerLite.ExecuteScalar(checkQuery));

                if (existingCount >= count)
                {
                    Debug.WriteLine($"ℹ️ عدد الصناديق كافٍ ({existingCount})، تخطي...");
                    return 0;
                }

                Random random = new Random();
                int toInsert = count - existingCount;

                for (int i = 0; i < toInsert; i++)
                {
                    string boxName = i < BoxNames.Length
                        ? BoxNames[i]
                        : $"صندوق رقم {i + 1}";

                    string sql = @"
                        INSERT INTO Boxes (name, image_path, start_date, details, is_active) 
                        VALUES (@name, @imagePath, @startDate, @details, @isActive)";

                    var parameters = new Dictionary<string, object>
                    {
                        { "@name", boxName },
                        { "@imagePath", DBNull.Value },
                        { "@startDate", DateTime.Now.AddDays(-random.Next(1, 365)).ToString("yyyy-MM-dd") },
                        { "@details", $"صندوق لحفظ الوثائق - تم إنشاؤه بشكل افتراضي" },
                        { "@isActive", random.Next(0, 10) > 2 ? 1 : 0 }
                    };

                    DatabaseManagerLite.ExecuteNonQuery(sql, parameters);
                    inserted++;
                }

                Debug.WriteLine($"✅ تم إدراج {inserted} صندوق");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"⚠️ خطأ في إدراج الصناديق: {ex.Message}");
            }

            return inserted;
        }

        /// <summary>
        /// 6. إدراج الجنود/الأفراد
        /// </summary>
        private static int SeedSoldiers()
        {
            int inserted = 0;

            try
            {
                string checkQuery = "SELECT COUNT(*) FROM Soldiers";
                int count = Convert.ToInt32(DatabaseManagerLite.ExecuteScalar(checkQuery));

                if (count > 0)
                {
                    Debug.WriteLine("ℹ️ الجنود موجودون مسبقاً، تخطي...");
                    return 0;
                }

                Random random = new Random();

                foreach (var soldier in Soldiers)
                {
                    string sql = @"
                        INSERT INTO Soldiers (name, unite, national_id, phone, address, is_active) 
                        VALUES (@name, @unite, @nationalId, @phone, @address, 1)";

                    var parameters = new Dictionary<string, object>
                    {
                        { "@name", soldier.Name },
                        { "@unite", soldier.Unite },
                        { "@nationalId", soldier.NationalId },
                        { "@phone", $"05{random.Next(10000000, 99999999)}" },
                        { "@address", $"الرياض - حي {random.Next(1, 20)} - شارع {random.Next(1, 50)}" }
                    };

                    DatabaseManagerLite.ExecuteNonQuery(sql, parameters);
                    inserted++;
                }

                // إضافة جنود إضافيين بشكل عشوائي
                for (int i = 0; i < 10; i++)
                {
                    string sql = @"
                        INSERT INTO Soldiers (name, unite, national_id, phone, address, is_active) 
                        VALUES (@name, @unite, @nationalId, @phone, @address, 1)";

                    var parameters = new Dictionary<string, object>
                    {
                        { "@name", $"جندي تجريبي {i + 1}" },
                        { "@unite", $"الوحدة {random.Next(1, 5)}" },
                        { "@nationalId", random.Next(100000000, 999999999).ToString() },
                        { "@phone", $"05{random.Next(10000000, 99999999)}" },
                        { "@address", $"الرياض - حي {random.Next(1, 20)}" }
                    };

                    DatabaseManagerLite.ExecuteNonQuery(sql, parameters);
                    inserted++;
                }

                Debug.WriteLine($"✅ تم إدراج {inserted} جندي");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"⚠️ خطأ في إدراج الجنود: {ex.Message}");
            }

            return inserted;
        }

        /// <summary>
        /// 7. إدراج الوثائق (مع دعم file_path بدلاً من file_data)
        /// </summary>
        private static int SeedDocuments(int count)
        {
            int inserted = 0;

            try
            {
                string checkQuery = "SELECT COUNT(*) FROM Documents";
                int existingCount = Convert.ToInt32(DatabaseManagerLite.ExecuteScalar(checkQuery));

                if (existingCount >= count)
                {
                    Debug.WriteLine($"ℹ️ عدد الوثائق كافٍ ({existingCount})، تخطي...");
                    return 0;
                }

                // جلب المعرفات من الجداول المرتبطة
                var documentTypes = GetIds("Document_Types");
                var categories = GetIds("Document_Categories");
                var departments = GetIds("Departments");
                var boxes = GetIds("Boxes");
                var users = GetIds("Users");

                if (documentTypes.Count == 0 || categories.Count == 0 ||
                    departments.Count == 0 || boxes.Count == 0 || users.Count == 0)
                {
                    Debug.WriteLine("⚠️ لا توجد بيانات كافية لإنشاء وثائق");
                    return 0;
                }

                Random random = new Random();
                int toInsert = count - existingCount;

                // مسار التخزين
                string storagePath = DatabaseManagerLite.GetStoragePath();
                string filesPath = Path.Combine(storagePath, DateTime.Now.ToString("yyyy"), DateTime.Now.ToString("MM"));

                if (!Directory.Exists(filesPath))
                    Directory.CreateDirectory(filesPath);

                for (int i = 0; i < toInsert; i++)
                {
                    string title = i < DocumentTitles.Length
                        ? DocumentTitles[i]
                        : $"وثيقة تجريبية رقم {i + 1}";

                    string status = Statuses[random.Next(Statuses.Length)];
                    string priority = Priorities[random.Next(Priorities.Length)];
                    string docNature = random.Next(0, 2) == 0 ? "داخلية" : "خارجية";

                    DateTime docDate = DateTime.Now.AddDays(-random.Next(1, 180));
                    DateTime receiveDate = docDate.AddDays(random.Next(1, 10));
                    DateTime issueDate = docDate.AddDays(random.Next(1, 5));

                    int typeId = documentTypes[random.Next(documentTypes.Count)];
                    int catId = categories[random.Next(categories.Count)];
                    int fromDept = departments[random.Next(departments.Count)];
                    int toDept = departments[random.Next(departments.Count)];
                    int boxId = boxes[random.Next(boxes.Count)];
                    int userId = users[random.Next(users.Count)];

                    string archiveBox = $"ARCH-{boxId:000}";
                    string archiveDoc = $"DOC-{i + 1:000}";

                    // ✅ إنشاء ملف وهمي مشفر (بدلاً من تخزينه في قاعدة البيانات)
                    string fileName = $"document_{i + 1}.pdf";
                    string filePath = Path.Combine(filesPath, $"{Guid.NewGuid()}_{fileName}.enc");

                    // إنشاء ملف وهمي
                    byte[] dummyData = new byte[random.Next(1024, 10240)];
                    random.NextBytes(dummyData);
                    File.WriteAllBytes(filePath, dummyData);

                    // المسار النسبي للتخزين في قاعدة البيانات
                    string relativePath = Path.Combine(
                        DateTime.Now.ToString("yyyy"),
                        DateTime.Now.ToString("MM"),
                        Path.GetFileName(filePath)
                    );

                    string sql = @"
                        INSERT INTO Documents (
                            title, document_type_id, category_id, 
                            from_department_id, to_department_id, box_id,
                            document_date, receive_date, issue_date,
                            uploaded_by, status, priority,
                            summary, notes, archiveDoc_number, ReferenceNumber,
                            file_path, file_name, file_type, file_size, file_hash
                        ) VALUES (
                            @title, @typeId, @catId,
                            @fromDept, @toDept, @boxId,
                            @docDate, @recDate, @issueDate,
                            @userId, @status, @priority,
                            @summary, @notes, @archiveDoc, @refNumber,
                            @filePath, @fileName, @fileType, @fileSize, @fileHash
                        )";

                    var parameters = new Dictionary<string, object>
                    {
                        { "@title", title },
                        { "@typeId", typeId },
                        { "@catId", catId },
                        { "@fromDept", fromDept },
                        { "@toDept", toDept },
                        { "@boxId", boxId },
                        { "@docDate", docDate.ToString("yyyy-MM-dd") },
                        { "@recDate", receiveDate.ToString("yyyy-MM-dd") },
                        { "@issueDate", issueDate.ToString("yyyy-MM-dd") },
                        { "@userId", userId },
                        { "@status", status },
                        { "@priority", priority },
                        { "@summary", $"ملخص الوثيقة: {title}" },
                        { "@notes", $"ملاحظات على الوثيقة: {title}" },
                        { "@archiveDoc", archiveDoc },
                        { "@refNumber", $"REF-{random.Next(1000, 9999)}" },
                        { "@filePath", relativePath },
                        { "@fileName", fileName },
                        { "@fileType", "application/pdf" },
                        { "@fileSize", dummyData.Length },
                        { "@fileHash", ComputeHash(dummyData) }
                    };

                    DatabaseManagerLite.ExecuteNonQuery(sql, parameters);
                    inserted++;
                }

                Debug.WriteLine($"✅ تم إدراج {inserted} وثيقة");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"⚠️ خطأ في إدراج الوثائق: {ex.Message}");
            }

            return inserted;
        }

        /// <summary>
        /// حساب Hash للملف
        /// </summary>
        private static string ComputeHash(byte[] data)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] hash = sha256.ComputeHash(data);
                return Convert.ToBase64String(hash);
            }
        }

        /// <summary>
        /// 8. إدراج علاقات الوثائق بالجنود
        /// </summary>
        private static int SeedDocumentSoldiers(int count)
        {
            int inserted = 0;

            try
            {
                string checkQuery = "SELECT COUNT(*) FROM DocumentSoldiers";
                int existingCount = Convert.ToInt32(DatabaseManagerLite.ExecuteScalar(checkQuery));

                if (existingCount >= count * 2)
                {
                    Debug.WriteLine($"ℹ️ عدد العلاقات كافٍ ({existingCount})، تخطي...");
                    return 0;
                }

                // جلب المعرفات
                var documents = GetIds("Documents");
                var soldiers = GetIds("Soldiers");

                if (documents.Count == 0 || soldiers.Count == 0)
                {
                    Debug.WriteLine("⚠️ لا توجد وثائق أو جنود لإنشاء علاقات");
                    return 0;
                }

                Random random = new Random();
                int toInsert = (count * 2) - existingCount;

                for (int i = 0; i < toInsert && i < documents.Count * 2; i++)
                {
                    int docId = documents[random.Next(documents.Count)];
                    int soldierId = soldiers[random.Next(soldiers.Count)];
                    string relType = RelationshipTypes[random.Next(RelationshipTypes.Length)];

                    string sql = @"
                        INSERT INTO DocumentSoldiers (
                            DocumentId, SoldierId, RelationshipType, 
                            RelationDate, RelationMonth, Notes
                        ) VALUES (
                            @docId, @soldierId, @relType,
                            @relDate, @relMonth, @notes
                        )";

                    DateTime relDate = DateTime.Now.AddDays(-random.Next(1, 365));
                    string relMonth = relDate.ToString("MMMM yyyy");

                    var parameters = new Dictionary<string, object>
                    {
                        { "@docId", docId },
                        { "@soldierId", soldierId },
                        { "@relType", relType },
                        { "@relDate", relDate.ToString("yyyy-MM-dd") },
                        { "@relMonth", relMonth },
                        { "@notes", $"علاقة من نوع: {relType}" }
                    };

                    try
                    {
                        DatabaseManagerLite.ExecuteNonQuery(sql, parameters);
                        inserted++;
                    }
                    catch
                    {
                        // تخطي إذا كان هناك تكرار
                    }
                }

                Debug.WriteLine($"✅ تم إدراج {inserted} علاقة وثائق بالجنود");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"⚠️ خطأ في إدراج علاقات الوثائق بالجنود: {ex.Message}");
            }

            return inserted;
        }

        #endregion

        #region "دوال مساعدة"

        /// <summary>
        /// جلب قائمة المعرفات من جدول معين
        /// </summary>
        private static List<int> GetIds(string tableName)
        {
            var ids = new List<int>();

            try
            {
                string query = $"SELECT id FROM {tableName} WHERE is_active = 1 ORDER BY id";
                var dt = DatabaseManagerLite.ExecuteQuery(query);

                foreach (System.Data.DataRow row in dt.Rows)
                {
                    ids.Add(Convert.ToInt32(row["id"]));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"⚠️ خطأ في جلب المعرفات من {tableName}: {ex.Message}");
            }

            return ids;
        }

        /// <summary>
        /// حذف جميع البيانات (تنبيه: عملية خطيرة) - SQLite
        /// </summary>
        public static void TruncateAll()
        {
            try
            {
                // ترتيب الحذف مهم بسبب العلاقات
                string[] tables = {
                    "DocumentSoldiers",
                    "Documents",
                    "Boxes",
                    "Soldiers",
                    "Users",
                    "Departments",
                    "Document_Categories",
                    "Document_Types"
                };

                using (var connection = DatabaseManagerLite.GetConnection())
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // تعطيل القيود المؤقتة
                            using (var cmd = new System.Data.SQLite.SQLiteCommand("PRAGMA foreign_keys = OFF;", connection, transaction))
                            {
                                cmd.ExecuteNonQuery();
                            }

                            foreach (string table in tables)
                            {
                                try
                                {
                                    string sql = $"DELETE FROM {table}";
                                    using (var cmd = new System.Data.SQLite.SQLiteCommand(sql, connection, transaction))
                                    {
                                        cmd.ExecuteNonQuery();
                                    }
                                    Debug.WriteLine($"🗑️ تم حذف بيانات جدول {table}");
                                }
                                catch (Exception ex)
                                {
                                    Debug.WriteLine($"⚠️ فشل في حذف جدول {table}: {ex.Message}");
                                }
                            }

                            // إعادة تعيين المعرفات (AUTOINCREMENT)
                            foreach (string table in tables)
                            {
                                try
                                {
                                    string sql = $"DELETE FROM sqlite_sequence WHERE name = '{table}'";
                                    using (var cmd = new System.Data.SQLite.SQLiteCommand(sql, connection, transaction))
                                    {
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                                catch
                                {
                                    // تجاهل الأخطاء في إعادة التعيين
                                }
                            }

                            // إعادة تفعيل القيود
                            using (var cmd = new System.Data.SQLite.SQLiteCommand("PRAGMA foreign_keys = ON;", connection, transaction))
                            {
                                cmd.ExecuteNonQuery();
                            }

                            transaction.Commit();
                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }

                // حذف الملفات المشفرة
                try
                {
                    string storagePath = DatabaseManagerLite.GetStoragePath();
                    if (Directory.Exists(storagePath))
                    {
                        // حذف جميع الملفات مع الاحتفاظ بالمجلدات
                        foreach (string file in Directory.GetFiles(storagePath, "*.*", SearchOption.AllDirectories))
                        {
                            try { File.Delete(file); } catch { }
                        }
                        Debug.WriteLine("🗑️ تم حذف جميع الملفات المشفرة");
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"⚠️ فشل في حذف الملفات: {ex.Message}");
                }

                Debug.WriteLine("✅ تم حذف جميع البيانات");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"❌ فشل في حذف البيانات: {ex.Message}");
                throw;
            }
        }

        #endregion
    }
}
