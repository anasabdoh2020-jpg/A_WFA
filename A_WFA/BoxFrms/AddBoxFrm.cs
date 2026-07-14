using A_WFA.ModServices;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace A_WFA.BoxFrms
{
    public partial class AddBoxFrm : Form
    {
        private int? editingBoxId = null;
        private bool isEditMode = false;
        private string selectedImagePath = null;
        private string currentArchiveNumber = "";
        private Action<int> onBoxAdded;
        private Action<int> onBoxUpdated;
        private Action onBoxDeleted;

        #region "طرق الاستدعاء الثابتة"

        public static void ShowAddBox()
        {
            using (var frm = new AddBoxFrm())
            {
                frm.ShowDialog();
            }
        }

        public static void ShowAddBox(Action<int> onBoxAdded)
        {
            using (var frm = new AddBoxFrm(onBoxAdded))
            {
                frm.ShowDialog();
            }
        }

        public static void ShowEditBox(int boxId)
        {
            using (var frm = new AddBoxFrm(boxId))
            {
                frm.ShowDialog();
            }
        }

        public static void ShowEditBox(int boxId, Action<int> onBoxUpdated)
        {
            using (var frm = new AddBoxFrm(boxId, onBoxUpdated))
            {
                frm.ShowDialog();
            }
        }

        public static void ShowBoxForm(int? boxId = null, Action<int> onSaved = null)
        {
            if (boxId.HasValue && boxId.Value > 0)
            {
                using (var frm = new AddBoxFrm(boxId.Value, onSaved))
                {
                    frm.ShowDialog();
                }
            }
            else
            {
                using (var frm = new AddBoxFrm(onSaved))
                {
                    frm.ShowDialog();
                }
            }
        }

        #endregion

        #region "المُنشئات"

        public AddBoxFrm()
        {
            InitializeComponent();
            InitializeForm();
            this.Text = "📦 إضافة صندوق جديد";
            BtnSave.Visible = true;
            BtnUpdate.Visible = false;
            BtnDelete.Visible = false;
            GenerateArchiveNumber();
        }

        public AddBoxFrm(Action<int> onBoxAddedCallback)
        {
            InitializeComponent();
            InitializeForm();
            onBoxAdded = onBoxAddedCallback;
            this.Text = "📦 إضافة صندوق جديد";
            BtnSave.Visible = true;
            BtnUpdate.Visible = false;
            BtnDelete.Visible = false;
            GenerateArchiveNumber();
        }

        public AddBoxFrm(int boxId)
        {
            InitializeComponent();
            InitializeForm();
            editingBoxId = boxId;
            isEditMode = true;
            this.Text = "✏️ تعديل صندوق";
            BtnSave.Visible = false;
            BtnUpdate.Visible = true;
            BtnDelete.Visible = true;
            LoadBoxData(boxId);
        }

        public AddBoxFrm(int boxId, Action<int> onBoxUpdatedCallback)
        {
            InitializeComponent();
            InitializeForm();
            editingBoxId = boxId;
            isEditMode = true;
            onBoxUpdated = onBoxUpdatedCallback;
            this.Text = "✏️ تعديل صندوق";
            BtnSave.Visible = false;
            BtnUpdate.Visible = true;
            BtnDelete.Visible = true;
            LoadBoxData(boxId);
        }

        #endregion

        #region "تهيئة النموذج"

        private void InitializeForm()
        {
            this.Load += AddBoxFrm_Load;

            BtnSave.Click += BtnSave_Click;
            BtnUpdate.Click += BtnUpdate_Click;
            BtnDelete.Click += BtnDelete_Click;
            BtnCancel.Click += BtnCancel_Click;
            BtnBrowseImage.Click += BtnBrowseImage_Click;
            BtnRemoveImage.Click += BtnRemoveImage_Click;
            BtnGenerateNumber.Click += BtnGenerateNumber_Click;

            // ✅ إضافة حدث التحقق من صحة الرقم
            TxtArchiveNumber.TextChanged += TxtArchiveNumber_TextChanged;
            TxtArchiveNumber.KeyPress += TxtArchiveNumber_KeyPress;

            SetupPlaceholderTexts();
        }

        #endregion

        #region "تحميل النموذج"

        private void AddBoxFrm_Load(object sender, EventArgs e)
        {
            StyleButtons();
        }

        private void StyleButtons()
        {
            foreach (Button btn in new[] {
                BtnSave, BtnUpdate, BtnDelete, BtnCancel,
                BtnBrowseImage, BtnRemoveImage, BtnGenerateNumber
            })
            {
                btn.MouseEnter += (s, ev) =>
                {
                    var button = (Button)s;
                    button.BackColor = ControlPaint.Light(button.BackColor, 0.2f);
                };

                btn.MouseLeave += (s, ev) =>
                {
                    var button = (Button)s;
                    if (button == BtnSave)
                        button.BackColor = Color.FromArgb(46, 204, 113);
                    else if (button == BtnUpdate)
                        button.BackColor = Color.FromArgb(52, 152, 219);
                    else if (button == BtnDelete)
                        button.BackColor = Color.FromArgb(231, 76, 60);
                    else if (button == BtnCancel)
                        button.BackColor = Color.FromArgb(149, 165, 166);
                    else if (button == BtnBrowseImage || button == BtnGenerateNumber)
                        button.BackColor = Color.FromArgb(52, 152, 219);
                    else if (button == BtnRemoveImage)
                        button.BackColor = Color.FromArgb(231, 76, 60);
                };
            }
        }

        private void SetupPlaceholderTexts()
        {
            if (string.IsNullOrEmpty(TxtBoxName.Text))
            {
                TxtBoxName.Text = "أدخل اسم الصندوق...";
                TxtBoxName.ForeColor = Color.Gray;
            }

            TxtBoxName.Enter += (s, e) =>
            {
                if (TxtBoxName.Text == "أدخل اسم الصندوق...")
                {
                    TxtBoxName.Text = "";
                    TxtBoxName.ForeColor = Color.Black;
                }
            };

            TxtBoxName.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(TxtBoxName.Text))
                {
                    TxtBoxName.Text = "أدخل اسم الصندوق...";
                    TxtBoxName.ForeColor = Color.Gray;
                }
            };

            if (string.IsNullOrEmpty(TxtDetails.Text))
            {
                TxtDetails.Text = "تفاصيل إضافية عن الصندوق...";
                TxtDetails.ForeColor = Color.Gray;
            }

            TxtDetails.Enter += (s, e) =>
            {
                if (TxtDetails.Text == "تفاصيل إضافية عن الصندوق...")
                {
                    TxtDetails.Text = "";
                    TxtDetails.ForeColor = Color.Black;
                }
            };

            TxtDetails.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(TxtDetails.Text))
                {
                    TxtDetails.Text = "تفاصيل إضافية عن الصندوق...";
                    TxtDetails.ForeColor = Color.Gray;
                }
            };
        }

        #endregion

        #region "التحقق من رقم الصندوق"

        private void TxtArchiveNumber_TextChanged(object sender, EventArgs e)
        {
            // التحقق من أن الإدخال رقم صحيح
            if (!string.IsNullOrEmpty(TxtArchiveNumber.Text))
            {
                if (!int.TryParse(TxtArchiveNumber.Text, out _))
                {
                    // إذا لم يكن رقماً، نعرض تحذير
                    TxtArchiveNumber.ForeColor = Color.Red;
                }
                else
                {
                    TxtArchiveNumber.ForeColor = Color.FromArgb(44, 62, 80);
                }
            }
        }

        private void TxtArchiveNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            // السماح فقط بالأرقام و Backspace
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        #endregion

        #region "تحميل البيانات"

        private void LoadBoxData(int boxId)
        {
            try
            {
                // ✅ استخدام DatabaseManagerLite
                DataRow row = DatabaseManagerLite.GetBoxById(boxId);
                if (row != null)
                {
                    TxtBoxName.Text = row["name"].ToString();
                    TxtBoxName.ForeColor = Color.Black;

                    // ✅ عرض الرقم فقط (بدون ARCH-)
                    string fullNumber = row["archiveBox_number"] != DBNull.Value
                        ? row["archiveBox_number"].ToString()
                        : "";
                    currentArchiveNumber = fullNumber;

                    // استخراج الرقم من ARCH-XXX
                    if (fullNumber.StartsWith("ARCH-") && fullNumber.Length > 5)
                    {
                        string numPart = fullNumber.Substring(5);
                        TxtArchiveNumber.Text = numPart;
                    }
                    else
                    {
                        TxtArchiveNumber.Text = fullNumber;
                    }

                    TxtDetails.Text = row["details"] != DBNull.Value
                        ? row["details"].ToString()
                        : "";
                    TxtDetails.ForeColor = Color.Black;

                    ChkIsActive.Checked = Convert.ToBoolean(row["is_active"]);

                    string imagePath = row["image_path"] != DBNull.Value
                        ? row["image_path"].ToString()
                        : null;
                    if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
                    {
                        selectedImagePath = imagePath;
                        PicBoxImage.Image = Image.FromFile(imagePath);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في تحميل بيانات الصندوق: {ex.Message}", "خطأ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region "توليد رقم الأرشيف"

        private void GenerateArchiveNumber()
        {
            try
            {
                // ✅ الحصول على الرقم التالي كعدد صحيح
                int nextNumber = GetNextBoxNumber();
                TxtArchiveNumber.Text = nextNumber.ToString();
                currentArchiveNumber = $"ARCH-{nextNumber:000}";
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"خطأ في توليد رقم الأرشيف: {ex.Message}");
                TxtArchiveNumber.Text = "1";
                currentArchiveNumber = "ARCH-001";
            }
        }

        /// <summary>
        /// الحصول على الرقم التالي للصندوق - SQLite
        /// </summary>
        private int GetNextBoxNumber()
        {
            try
            {
                // ✅ استعلام SQLite
                string query = @"
                    SELECT COALESCE(MAX(
                        CAST(SUBSTR(archiveBox_number, 6) AS INTEGER)
                    ), 0)
                    FROM Boxes
                    WHERE archiveBox_number IS NOT NULL
                    AND archiveBox_number LIKE 'ARCH-%'";

                object result = DatabaseManagerLite.ExecuteScalar(query);
                int maxNumber = result != null && result != DBNull.Value
                    ? Convert.ToInt32(result)
                    : 0;

                return maxNumber + 1;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"خطأ في GetNextBoxNumber: {ex.Message}");
                return 1;
            }
        }

        private void BtnGenerateNumber_Click(object sender, EventArgs e)
        {
            GenerateArchiveNumber();
        }

        #endregion

        #region "إدارة الصورة"

        private void BtnBrowseImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "اختر صورة للصندوق";
                ofd.Filter = "ملفات الصور|*.jpg;*.jpeg;*.png;*.bmp;*.gif|" +
                           "JPEG|*.jpg;*.jpeg|" +
                           "PNG|*.png|" +
                           "جميع الملفات|*.*";
                ofd.FilterIndex = 1;

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        selectedImagePath = ofd.FileName;
                        PicBoxImage.Image = Image.FromFile(selectedImagePath);
                        PicBoxImage.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"خطأ في تحميل الصورة: {ex.Message}", "خطأ",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void BtnRemoveImage_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("هل تريد إزالة الصورة؟", "تأكيد",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                selectedImagePath = null;
                PicBoxImage.Image = null;
                PicBoxImage.BackColor = Color.FromArgb(240, 240, 240);
            }
        }

        #endregion

        #region "حفظ البيانات"

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(TxtBoxName.Text) || TxtBoxName.Text == "أدخل اسم الصندوق...")
            {
                MessageBox.Show("يرجى إدخال اسم الصندوق", "تنبيه",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TxtBoxName.Focus();
                return false;
            }

            if (TxtBoxName.Text.Length < 3)
            {
                MessageBox.Show("اسم الصندوق يجب أن يكون 3 أحرف على الأقل", "تنبيه",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TxtBoxName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(TxtArchiveNumber.Text))
            {
                MessageBox.Show("يرجى إدخال رقم الصندوق", "تنبيه",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TxtArchiveNumber.Focus();
                return false;
            }

            // ✅ التحقق من أن الرقم صحيح
            if (!int.TryParse(TxtArchiveNumber.Text, out int boxNumber) || boxNumber <= 0)
            {
                MessageBox.Show("رقم الصندوق يجب أن يكون رقماً موجباً", "تنبيه",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TxtArchiveNumber.Focus();
                return false;
            }

            return true;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SaveBox();
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            UpdateBox();
        }

        /// <summary>
        /// تحويل رقم الصندوق إلى الصيغة الكاملة ARCH-XXX
        /// </summary>
        private string GetFullArchiveNumber(string number)
        {
            if (int.TryParse(number, out int num))
            {
                return $"ARCH-{num:000}";
            }
            return $"ARCH-{number}";
        }

        private void SaveBox()
        {
            if (!ValidateInput()) return;

            try
            {
                Cursor = Cursors.WaitCursor;

                string name = TxtBoxName.Text.Trim();
                string boxNumber = TxtArchiveNumber.Text.Trim();
                string fullArchiveNumber = GetFullArchiveNumber(boxNumber);
                string details = TxtDetails.Text.Trim();
                bool isActive = ChkIsActive.Checked;

                // ✅ التحقق من عدم تكرار الرقم
                if (IsArchiveNumberExists(fullArchiveNumber))
                {
                    MessageBox.Show(
                        $"⚠️ رقم الصندوق '{boxNumber}' موجود بالفعل!\n" +
                        "يرجى إدخال رقم مختلف.",
                        "رقم مكرر",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    TxtArchiveNumber.Focus();
                    TxtArchiveNumber.SelectAll();
                    return;
                }

                string savedImagePath = null;
                if (!string.IsNullOrEmpty(selectedImagePath))
                {
                    savedImagePath = CopyImageToAppFolder(selectedImagePath);
                }

                // ✅ استخدام DatabaseManagerLite
                int newId = DatabaseManagerLite.AddBox(
                    name,
                    savedImagePath,
                    DateTime.Now.ToString("yyyy-MM-dd"),
                    details,
                    isActive
                );

                if (newId > 0)
                {
                    // ✅ تحديث رقم الأرشيف بالرقم الذي أدخله المستخدم
                    UpdateBoxArchiveNumber(newId, fullArchiveNumber);

                    MessageBox.Show("✅ تم إضافة الصندوق بنجاح!", "نجاح",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    onBoxAdded?.Invoke(newId);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("❌ فشل في إضافة الصندوق", "خطأ",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في حفظ الصندوق: {ex.Message}", "خطأ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void UpdateBox()
        {
            if (!ValidateInput()) return;
            if (!editingBoxId.HasValue) return;

            try
            {
                Cursor = Cursors.WaitCursor;

                string name = TxtBoxName.Text.Trim();
                string boxNumber = TxtArchiveNumber.Text.Trim();
                string fullArchiveNumber = GetFullArchiveNumber(boxNumber);
                string details = TxtDetails.Text.Trim();
                bool isActive = ChkIsActive.Checked;

                // ✅ التحقق من عدم تكرار الرقم (باستثناء الصندوق الحالي)
                if (fullArchiveNumber != currentArchiveNumber && IsArchiveNumberExists(fullArchiveNumber))
                {
                    MessageBox.Show(
                        $"⚠️ رقم الصندوق '{boxNumber}' موجود بالفعل!\n" +
                        "يرجى إدخال رقم مختلف.",
                        "رقم مكرر",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    TxtArchiveNumber.Focus();
                    TxtArchiveNumber.SelectAll();
                    return;
                }

                string savedImagePath = null;
                if (!string.IsNullOrEmpty(selectedImagePath))
                {
                    savedImagePath = CopyImageToAppFolder(selectedImagePath);
                }

                // ✅ استخدام DatabaseManagerLite
                bool success = DatabaseManagerLite.UpdateBox(
                    editingBoxId.Value,
                    name,
                    savedImagePath,
                    DateTime.Now.ToString("yyyy-MM-dd"),
                    details,
                    isActive);

                if (success)
                {
                    // ✅ تحديث رقم الأرشيف إذا تغير
                    if (fullArchiveNumber != currentArchiveNumber)
                    {
                        UpdateBoxArchiveNumber(editingBoxId.Value, fullArchiveNumber);
                    }

                    MessageBox.Show("✅ تم تحديث الصندوق بنجاح!", "نجاح",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    onBoxUpdated?.Invoke(editingBoxId.Value);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("❌ فشل في تحديث الصندوق", "خطأ",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في تحديث الصندوق: {ex.Message}", "خطأ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        #endregion

        #region "حذف البيانات"

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (!editingBoxId.HasValue) return;

            DialogResult result = MessageBox.Show(
                "⚠️ هل أنت متأكد من حذف هذا الصندوق؟\n" +
                "سيتم حذف جميع الوثائق المرتبطة به!",
                "تأكيد الحذف",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;
                    // ✅ استخدام DatabaseManagerLite
                    bool success = DatabaseManagerLite.DeleteBox(editingBoxId.Value);

                    if (success)
                    {
                        MessageBox.Show("✅ تم حذف الصندوق بنجاح", "نجاح",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        onBoxDeleted?.Invoke();

                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("❌ فشل في حذف الصندوق", "خطأ",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"خطأ في حذف الصندوق: {ex.Message}", "خطأ",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Cursor = Cursors.Default;
                }
            }
        }

        #endregion

        #region "إلغاء"

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            if (isEditMode)
            {
                DialogResult result = MessageBox.Show("هل تريد إلغاء التعديلات؟", "تأكيد",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                }
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        #endregion

        #region "دوال مساعدة"

        private bool IsArchiveNumberExists(string archiveNumber)
        {
            try
            {
                // ✅ استخدام DatabaseManagerLite
                string query = "SELECT COUNT(*) FROM Boxes WHERE archiveBox_number = @number";
                var parameters = new Dictionary<string, object> { { "@number", archiveNumber } };
                object result = DatabaseManagerLite.ExecuteScalar(query, parameters);
                return result != null && Convert.ToInt32(result) > 0;
            }
            catch
            {
                return false;
            }
        }

        private void UpdateBoxArchiveNumber(int boxId, string archiveNumber)
        {
            try
            {
                // ✅ استخدام DatabaseManagerLite
                string query = "UPDATE Boxes SET archiveBox_number = @number WHERE id = @id";
                var parameters = new Dictionary<string, object>
                {
                    { "@number", archiveNumber },
                    { "@id", boxId }
                };
                DatabaseManagerLite.ExecuteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"خطأ في تحديث رقم الأرشيف: {ex.Message}");
            }
        }

        private string CopyImageToAppFolder(string sourcePath)
        {
            try
            {
                if (string.IsNullOrEmpty(sourcePath) || !File.Exists(sourcePath))
                    return null;

                string imagesFolder = Path.Combine(Application.StartupPath, "Images", "Boxes");
                if (!Directory.Exists(imagesFolder))
                {
                    Directory.CreateDirectory(imagesFolder);
                }

                string extension = Path.GetExtension(sourcePath);
                string fileName = $"box_{DateTime.Now:yyyyMMdd_HHmmss}_{Guid.NewGuid().ToString().Substring(0, 8)}{extension}";
                string destPath = Path.Combine(imagesFolder, fileName);

                File.Copy(sourcePath, destPath, true);
                return destPath;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"خطأ في نسخ الصورة: {ex.Message}");
                return sourcePath;
            }
        }

        #endregion
    }
}