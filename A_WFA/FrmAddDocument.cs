using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using PdfiumViewer;
using Spire.Doc;
using Spire.Xls;
using A_WFA.ModServices;
using A_WFA.Database.LTE;

namespace A_WFA
{
    public partial class FrmAddDocument : Form
    {
        #region 1. المتغيرات والخصائص العامة (Fields & Properties)

        private int boxId;
        private string boxName;
        private string boxArchiveNumber;
        private int currentDocumentId = 0;
        private int currentDocSequence = 0;
        private byte[] fileBytes = null;
        private string fileName = "";
        private string fileType = "";
        private long fileSize = 0;
        private bool isEditMode = false;
        private string _existingFilePath = ""; // ✅ مسار الملف الموجود (للتعديل)

        // عارض الـ PDF والـ WebBrowser للمعاينة
        private PdfViewer pdfViewer = null;
        private WebBrowser docBrowser = null;

        #endregion

        #region 2. مشيد الشاشة والتهيئة المبدئية (Constructor)

        public FrmAddDocument(int boxId = 0, int documentId = 0)
        {
            InitializeComponent();

            this.boxId = boxId;
            this.currentDocumentId = documentId;
            this.isEditMode = (documentId > 0);

            // ربط أحداث الشاشة الأساسية
            this.Load += FrmAddDocument_Load;
            this.FormClosing += FrmAddDocument_FormClosing;

            // ربط أحداث الأزرار وعناصر التحكم
            RegisterControlEvents();
        }

        private void RegisterControlEvents()
        {
            btnBrowseFile.Click += btnBrowseFile_Click;
            btnCancel.Click += btnCancel_Click;
            btnSave.Click += btnSave_Click;
            btnSaveDocument.Click += btnSaveDocument_Click;
            btnAddSoldiers.Click += btnAddSoldiers_Click;
            btnInfo.Click += btnInfo_Click;
            btnScanDocument.Click += btnScanDocument_Click;
            btnOpenScanner.Click += btnOpenScanner_Click;
            btnIncreaseDocumentNumber.Click += btnIncreaseDocumentNumber_Click;
            btnDecreaseDocumentNumber.Click += btnDecreaseDocumentNumber_Click;
            btnResetSequential.Click += btnResetSequential_Click;
            btnAddNewDocument.Click += btnAddNewDocument_Click;
            btnClearForm.Click += btnClearForm_Click;
            btnLoadTemplate.Click += btnLoadTemplate_Click;
            btnCopyCurrent.Click += btnCopyCurrent_Click;
            Button1.Click += Button1_Click;

            cmbcangeboxs.SelectedIndexChanged += cmbcangeboxs_SelectedIndexChanged;
            DataGridView1.CellDoubleClick += DataGridView1_CellDoubleClick;

            Label14.Click += Label14_Click;
            Label13.Click += Label13_Click;
            Label4.Click += Label4_Click;
        }

        #endregion

        #region 3. أحداث تحميل وإغلاق الشاشة (Form Lifecycle)

        private void FrmAddDocument_Load(object sender, EventArgs e)
        {
            try
            {
                // ✅ استخدام DatabaseManagerLite
                DatabaseManagerLite.SafeLogAuditTrail(GetCurrentUserId(), "open_form", "فتح نموذج إضافة وثيقة جديدة");

                InitializeFormElements();
                LoadComboBoxData();
                SetupFormForMode();

                btnAddSoldiers.Text = "👥 أشخاص متعلقين بالوثيقة";
                lblBoxName.Text = boxName;

                LoadBoxImage();
                SetSmartDefaults();
                nomberArshiv();
                SetupToolTips();

                // تحديث واجهة رقم الوثيقة الأرشيفي
                UpdateArchiveNumberDisplay();
            }
            catch (Exception ex)
            {
                DatabaseManagerLite.SafeLogAuditTrail(GetCurrentUserId(), "error", $"خطأ في تحميل النموذج: {ex.Message}");
                MessageBox.Show($"حدث خطأ أثناء تحميل النموذج: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmAddDocument_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (picBoxImage.Image != null) picBoxImage.Image.Dispose();
                if (picImagePreview.Image != null) picImagePreview.Image.Dispose();

                // تفريغ ذاكرة مستند PDF المفتوح لمنع قفل الملفات في النظام
                if (pdfViewer != null && pdfViewer.Document != null)
                {
                    pdfViewer.Document.Dispose();
                }
            }
            catch
            {
                // تجاهل أخطاء إغلاق الموارد غير الحرجة
            }
        }

        #endregion

        #region 4. تهيئة وتجهيز عناصر الواجهة (UI Setup)

        private void InitializeFormElements()
        {
            dtpDocumentDate.Value = DateTime.Now;
            dtpReceiveDate.Value = DateTime.Now;
            dtpIssueDate.Value = DateTime.Now;

            SetupSoldiersDataGridView();
        }

        private void SetupFormForMode()
        {
            if (isEditMode)
            {
                this.Text = "✏️ تعديل الوثيقة";
                LoadDocumentData();
            }
            else
            {
                this.Text = "📄 إضافة وثيقة جديدة";
            }
            UpdateUIButtons();
        }

        private void UpdateUIButtons()
        {
            if (isEditMode)
            {
                btnSave.Text = "💾 تحديث";
                btnSaveDocument.Text = "💾 تحديث المستند";
            }
            else
            {
                btnSave.Text = "💾 حفظ";
                btnSaveDocument.Text = "💾 حفظ المستند";
            }
        }

        private void SetSmartDefaults()
        {
            LoadStatusOptions();
            LoadPriorityOptions();
            LoadDocumentNatureOptions();

            if (cmbStatus.Items.Count > 0) cmbStatus.SelectedIndex = 0;
            if (cmbPriority.Items.Count > 0) cmbPriority.SelectedIndex = 0;
            if (Cmbdocument_nature.Items.Count > 0) Cmbdocument_nature.SelectedIndex = 0;
        }

        #endregion

        #region 5. تعبئة القوائم المنسدلة والبيانات المساعدة (Data Loading)

        private void LoadComboBoxData()
        {
            try
            {
                LoadDocumentTypes();
                LoadCategories();
                LoadDepartments();
                LoadBoxes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ في تحميل البيانات المساعدة: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDocumentTypes()
        {
            // ✅ استخدام DatabaseManagerLite
            DataTable dt = DatabaseManagerLite.ExecuteQuery("SELECT id, name FROM Document_Types ORDER BY name");
            cmbDocumentType.DataSource = dt;
            cmbDocumentType.DisplayMember = "name";
            cmbDocumentType.ValueMember = "id";
        }

        private void LoadCategories()
        {
            DataTable dt = DatabaseManagerLite.ExecuteQuery("SELECT id, name FROM Document_Categories ORDER BY name");
            cmbCategory.DataSource = dt;
            cmbCategory.DisplayMember = "name";
            cmbCategory.ValueMember = "id";
        }

        private void LoadDepartments()
        {
            DataTable dt = DatabaseManagerLite.ExecuteQuery("SELECT id, name FROM Departments ORDER BY name");
            cmbFromDepartment.DataSource = dt.Copy();
            cmbToDepartment.DataSource = dt.Copy();

            cmbFromDepartment.DisplayMember = "name";
            cmbFromDepartment.ValueMember = "id";
            cmbToDepartment.DisplayMember = "name";
            cmbToDepartment.ValueMember = "id";
        }

        private void LoadBoxes()
        {
            DataTable dt = DatabaseManagerLite.ExecuteQuery("SELECT id, name FROM Boxes WHERE is_active = 1 ORDER BY name");
            cmbcangeboxs.DataSource = dt;
            cmbcangeboxs.DisplayMember = "name";
            cmbcangeboxs.ValueMember = "id";

            if (boxId > 0) cmbcangeboxs.SelectedValue = boxId;
        }

        private void LoadStatusOptions()
        {
            cmbStatus.Items.Clear();
            cmbStatus.Items.AddRange(new object[] { "قيد المراجعة", "معتمد", "مرفوض", "منفذ", "ملغي", "مؤرشف" });
        }

        private void LoadPriorityOptions()
        {
            cmbPriority.Items.Clear();
            cmbPriority.Items.AddRange(new object[] { "عادية", "مهمة", "عاجلة" });
        }

        private void LoadDocumentNatureOptions()
        {
            Cmbdocument_nature.Items.Clear();
            Cmbdocument_nature.Items.AddRange(new object[] { "داخلية", "خارجية" });
        }

        #endregion

        //#region 6. جلب بيانات وثيقة محددة للتعديل (Document Data Retrieval)

        //private void LoadDocumentData()
        //{
        //    try
        //    {
        //        string query = "SELECT * FROM Documents WHERE id = @id";
        //        var parameters = new Dictionary<string, object> { { "@id", currentDocumentId } };
        //        DataTable dt = DatabaseManagerLite.ExecuteQuery(query, parameters);

        //        if (dt.Rows.Count > 0)
        //        {
        //            DataRow row = dt.Rows[0];

        //            txtTitle.Text = row["title"].ToString();

        //            if (!DBNull.Value.Equals(row["document_type_id"]))
        //                cmbDocumentType.SelectedValue = Convert.ToInt32(row["document_type_id"]);

        //            if (!DBNull.Value.Equals(row["category_id"]))
        //                cmbCategory.SelectedValue = Convert.ToInt32(row["category_id"]);

        //            if (!DBNull.Value.Equals(row["from_department_id"]))
        //                cmbFromDepartment.SelectedValue = Convert.ToInt32(row["from_department_id"]);

        //            if (!DBNull.Value.Equals(row["to_department_id"]))
        //                cmbToDepartment.SelectedValue = Convert.ToInt32(row["to_department_id"]);

        //            if (!DBNull.Value.Equals(row["document_date"]))
        //                dtpDocumentDate.Value = Convert.ToDateTime(row["document_date"]);

        //            if (!DBNull.Value.Equals(row["receive_date"]))
        //                dtpReceiveDate.Value = Convert.ToDateTime(row["receive_date"]);

        //            if (!DBNull.Value.Equals(row["issue_date"]))
        //                dtpIssueDate.Value = Convert.ToDateTime(row["issue_date"]);

        //            cmbStatus.Text = row["status"].ToString();
        //            cmbPriority.Text = !DBNull.Value.Equals(row["priority"]) ? row["priority"].ToString() : "عادية";
        //            Cmbdocument_nature.Text = !DBNull.Value.Equals(row["document_nature"]) ? row["document_nature"].ToString() : "داخلية";
        //            txtSummary.Text = !DBNull.Value.Equals(row["summary"]) ? row["summary"].ToString() : "";
        //            txtNotes.Text = !DBNull.Value.Equals(row["notes"]) ? row["notes"].ToString() : "";

        //            if (!DBNull.Value.Equals(row["archiveDoc_number"]))
        //            {
        //                string docNumber = row["archiveDoc_number"].ToString();
        //                int.TryParse(docNumber, out currentDocSequence);
        //            }

        //            if (!DBNull.Value.Equals(row["box_id"]))
        //                cmbcangeboxs.SelectedValue = Convert.ToInt32(row["box_id"]);

        //            // ✅ التغيير: قراءة المسار بدلاً من file_data
        //            string filePath = row["file_path"]?.ToString();
        //            if (!string.IsNullOrEmpty(filePath))
        //            {
        //                _existingFilePath = filePath;
        //                fileName = row["file_name"]?.ToString() ?? "document.pdf";
        //                fileType = row["file_type"]?.ToString() ?? "application/pdf";
        //                fileSize = Convert.ToInt64(row["file_size"]);
        //                txtFilePath.Text = fileName;

        //                // ✅ تحميل الملف من نظام الملفات
        //                try
        //                {
        //                    string fullPath = Path.Combine(DatabaseManagerLite.GetStoragePath(), filePath);
        //                    if (File.Exists(fullPath))
        //                    {
        //                        fileBytes = File.ReadAllBytes(fullPath);
        //                        // ✅ عرض المعاينة
        //                        PreviewDocument(fullPath);
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    Debug.WriteLine($"خطأ في تحميل الملف: {ex.Message}");
        //                }
        //            }

        //            LoadDocumentSoldiers();
        //            UpdateDocumentInfo();
        //            UpdateArchiveNumberDisplay();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("خطأ في تحميل بيانات الوثيقة: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        //private void LoadDocumentSoldiers()
        //{
        //    try
        //    {
        //        string query = @"SELECT ds.*, s.name AS soldier_name FROM DocumentSoldiers ds 
        //                        INNER JOIN Soldiers s ON ds.SoldierId = s.id 
        //                        WHERE ds.DocumentId = @documentId";
        //        var parameters = new Dictionary<string, object> { { "@documentId", currentDocumentId } };
        //        DataTable dt = DatabaseManagerLite.ExecuteQuery(query, parameters);

        //        var soldiersList = new List<TemporarySoldierInfo>();
        //        foreach (DataRow row in dt.Rows)
        //        {
        //            soldiersList.Add(new TemporarySoldierInfo
        //            {
        //                SoldierId = Convert.ToInt32(row["SoldierId"]),
        //                SoldierName = row["soldier_name"].ToString(),
        //                RelationshipType = row["RelationshipType"].ToString(),
        //                RelationDate = row["RelationDate"].ToString(),
        //                Notes = !DBNull.Value.Equals(row["Notes"]) ? row["Notes"].ToString() : ""
        //            });
        //        }
        //        RefreshSoldiersDataGridView(soldiersList);
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine("خطأ في تحميل الأشخاص المرتبطين: " + ex.Message);
        //    }
        //}

        //#endregion

        #region 6. جلب بيانات وثيقة محددة للتعديل (Document Data Retrieval)

        private void LoadDocumentData()
        {
            try
            {
                // ✅ تحميل مفتاح التشفير
                byte[] masterKey = LoadMasterKey();
                A_WFA.Security.CryptoService.Initialize(masterKey);

                string query = "SELECT * FROM Documents WHERE id = @id";
                var parameters = new Dictionary<string, object> { { "@id", currentDocumentId } };
                DataTable dt = DatabaseManagerLite.ExecuteQuery(query, parameters);

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];

                    // ✅ فك تشفير العنوان
                    txtTitle.Text = A_WFA.Security.CryptoService.DecryptString(row["title"].ToString());

                    if (!DBNull.Value.Equals(row["document_type_id"]))
                        cmbDocumentType.SelectedValue = Convert.ToInt32(row["document_type_id"]);

                    if (!DBNull.Value.Equals(row["category_id"]))
                        cmbCategory.SelectedValue = Convert.ToInt32(row["category_id"]);

                    if (!DBNull.Value.Equals(row["from_department_id"]))
                        cmbFromDepartment.SelectedValue = Convert.ToInt32(row["from_department_id"]);

                    if (!DBNull.Value.Equals(row["to_department_id"]))
                        cmbToDepartment.SelectedValue = Convert.ToInt32(row["to_department_id"]);

                    // ✅ فك تشفير التواريخ
                    string docDate = A_WFA.Security.CryptoService.DecryptString(row["document_date"].ToString());
                    if (!string.IsNullOrEmpty(docDate))
                        dtpDocumentDate.Value = Convert.ToDateTime(docDate);

                    string recDate = A_WFA.Security.CryptoService.DecryptString(row["receive_date"].ToString());
                    if (!string.IsNullOrEmpty(recDate))
                        dtpReceiveDate.Value = Convert.ToDateTime(recDate);

                    string issueDate = A_WFA.Security.CryptoService.DecryptString(row["issue_date"].ToString());
                    if (!string.IsNullOrEmpty(issueDate))
                        dtpIssueDate.Value = Convert.ToDateTime(issueDate);

                    // ✅ فك تشفير الحالة والأولوية والطبيعة
                    cmbStatus.Text = A_WFA.Security.CryptoService.DecryptString(row["status"].ToString());
                    cmbPriority.Text = A_WFA.Security.CryptoService.DecryptString(row["priority"].ToString());
                    Cmbdocument_nature.Text = A_WFA.Security.CryptoService.DecryptString(row["document_nature"].ToString());

                    // ✅ فك تشفير الملخص والملاحظات
                    txtSummary.Text = A_WFA.Security.CryptoService.DecryptString(row["summary"].ToString());
                    txtNotes.Text = A_WFA.Security.CryptoService.DecryptString(row["notes"].ToString());

                    if (!DBNull.Value.Equals(row["archiveDoc_number"]))
                    {
                        string docNumber = row["archiveDoc_number"].ToString();
                        int.TryParse(docNumber, out currentDocSequence);
                    }

                    if (!DBNull.Value.Equals(row["box_id"]))
                        cmbcangeboxs.SelectedValue = Convert.ToInt32(row["box_id"]);

                    // ✅ فك تشفير اسم الملف
                    string filePath = row["file_path"]?.ToString();
                    if (!string.IsNullOrEmpty(filePath))
                    {
                        _existingFilePath = filePath;
                        fileName = A_WFA.Security.CryptoService.DecryptString(row["file_name"].ToString());
                        fileType = A_WFA.Security.CryptoService.DecryptString(row["file_type"].ToString());
                        fileSize = Convert.ToInt64(row["file_size"]);
                        txtFilePath.Text = fileName;

                        // ✅ تحميل الملف من نظام الملفات
                        try
                        {
                            string fullPath = Path.Combine(DatabaseManagerLite.GetStoragePath(), filePath);
                            if (File.Exists(fullPath))
                            {
                                // ✅ قراءة الملف المشفر وفك تشفيره
                                byte[] encryptedData = File.ReadAllBytes(fullPath);
                                fileBytes = A_WFA.Security.CryptoService.Decrypt(encryptedData);

                                // ✅ التحقق من سلامة الملف
                                string currentHash = A_WFA.Security.CryptoService.ComputeHash(fileBytes);
                                string expectedHash = row["file_hash"]?.ToString();
                                if (!string.IsNullOrEmpty(expectedHash) && currentHash != expectedHash)
                                {
                                    MessageBox.Show("⚠️ تحذير: الملف قد يكون تالفاً أو معدلاً!", "تحذير",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }

                                // ✅ عرض المعاينة
                                // إنشاء ملف مؤقت للعرض
                                string tempFile = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}{Path.GetExtension(fileName)}");
                                File.WriteAllBytes(tempFile, fileBytes);
                                PreviewDocument(tempFile);

                                // جدولة حذف الملف المؤقت بعد 5 دقائق
                                System.Threading.Tasks.Task.Delay(300000).ContinueWith(_ =>
                                {
                                    try { File.Delete(tempFile); } catch { }
                                });
                            }
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine($"خطأ في تحميل الملف: {ex.Message}");
                        }
                    }

                    LoadDocumentSoldiers();
                    UpdateDocumentInfo();
                    UpdateArchiveNumberDisplay();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ في تحميل بيانات الوثيقة: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// تحميل مفتاح التشفير الرئيسي
        /// </summary>
        private byte[] LoadMasterKey()
        {
            try
            {
                string keyPath = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "A_WFA",
                    "Keys",
                    "master.key"
                );

                if (File.Exists(keyPath))
                {
                    byte[] protectedKey = File.ReadAllBytes(keyPath);
                    return System.Security.Cryptography.ProtectedData.Unprotect(
                        protectedKey,
                        null,
                        System.Security.Cryptography.DataProtectionScope.CurrentUser
                    );
                }
                else
                {
                    // إنشاء مفتاح جديد
                    byte[] newKey = A_WFA.Security.CryptoService.GenerateKey();
                    byte[] protectedKey = System.Security.Cryptography.ProtectedData.Protect(
                        newKey,
                        null,
                        System.Security.Cryptography.DataProtectionScope.CurrentUser
                    );

                    string keyDir = Path.GetDirectoryName(keyPath);
                    if (!Directory.Exists(keyDir))
                        Directory.CreateDirectory(keyDir);

                    File.WriteAllBytes(keyPath, protectedKey);
                    return newKey;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"فشل تحميل مفتاح التشفير: {ex.Message}");
            }
        }

        private void LoadDocumentSoldiers()
        {
            try
            {
                string query = @"SELECT ds.*, s.name AS soldier_name FROM DocumentSoldiers ds 
                        INNER JOIN Soldiers s ON ds.SoldierId = s.id 
                        WHERE ds.DocumentId = @documentId";
                var parameters = new Dictionary<string, object> { { "@documentId", currentDocumentId } };
                DataTable dt = DatabaseManagerLite.ExecuteQuery(query, parameters);

                var soldiersList = new List<TemporarySoldierInfo>();
                foreach (DataRow row in dt.Rows)
                {
                    soldiersList.Add(new TemporarySoldierInfo
                    {
                        SoldierId = Convert.ToInt32(row["SoldierId"]),
                        SoldierName = row["soldier_name"].ToString(),
                        RelationshipType = row["RelationshipType"].ToString(),
                        RelationDate = row["RelationDate"].ToString(),
                        Notes = !DBNull.Value.Equals(row["Notes"]) ? row["Notes"].ToString() : ""
                    });
                }
                RefreshSoldiersDataGridView(soldiersList);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("خطأ في تحميل الأشخاص المرتبطين: " + ex.Message);
            }
        }

        #endregion
        #region 7. الترقيم التسلسلي والأرشفة الذكية (Archive & Sequence Management)

        private void UpdateArchiveNumberDisplay()
        {
            if (cmbcangeboxs.SelectedIndex != -1)
            {
                int selectedBoxId = Convert.ToInt32(cmbcangeboxs.SelectedValue);
                string boxNumber = ArchiveBoxService.GetArchiveBoxNumber(selectedBoxId);
                txtArchiveNumber.Text = $"{boxNumber}-{currentDocSequence:000}";
            }
        }

        private void nomberArshiv()
        {
            try
            {
                if (cmbcangeboxs.SelectedIndex != -1)
                {
                    int selectedBoxId = Convert.ToInt32(cmbcangeboxs.SelectedValue);
                    boxArchiveNumber = ArchiveBoxService.GetArchiveBoxNumber(selectedBoxId);
                    currentDocSequence = ArchiveBoxService.GetNextDocumentSequence(selectedBoxId);
                    UpdateArchiveNumberDisplay();
                }
                else
                {
                    txtArchiveNumber.Text = "ARCH-001-001";
                }
            }
            catch
            {
                txtArchiveNumber.Text = "ARCH-001-001";
            }
        }

        private void btnIncreaseDocumentNumber_Click(object sender, EventArgs e)
        {
            currentDocSequence = ArchiveBoxService.IncreaseSequence(currentDocSequence);
            UpdateArchiveNumberDisplay();
        }

        private void btnDecreaseDocumentNumber_Click(object sender, EventArgs e)
        {
            currentDocSequence = ArchiveBoxService.DecreaseSequence(currentDocSequence);
            UpdateArchiveNumberDisplay();
        }

        private void btnResetSequential_Click(object sender, EventArgs e)
        {
            if (cmbcangeboxs.SelectedIndex != -1)
            {
                int selectedBoxId = Convert.ToInt32(cmbcangeboxs.SelectedValue);
                currentDocSequence = ArchiveBoxService.GetNextDocumentSequence(selectedBoxId);
                UpdateArchiveNumberDisplay();
            }
        }

        private void cmbcangeboxs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbcangeboxs.SelectedValue != null && int.TryParse(cmbcangeboxs.SelectedValue.ToString(), out int id))
            {
                boxId = id;
                nomberArshiv();
                LoadBoxImage();
            }
        }
        #endregion

        #region 8. محرك المعاينة المطور وفصل الامتدادات (Preview Engine & Viewer)

        private void btnBrowseFile_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Filter = "جميع الملفات المدعومة|*.pdf;*.jpg;*.jpeg;*.png;*.doc;*.docx;*.xls;*.xlsx|" +
                                 "ملفات PDF|*.pdf|" +
                                 "الصور|*.jpg;*.jpeg;*.png|" +
                                 "ملفات Word|*.doc;*.docx|" +
                                 "ملفات Excel|*.xls;*.xlsx|" +
                                 "جميع الملفات|*.*";
                    ofd.Title = "اختر ملف الوثيقة";
                    ofd.Multiselect = false;

                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        fileBytes = File.ReadAllBytes(ofd.FileName);
                        fileName = Path.GetFileName(ofd.FileName);
                        fileType = GetMimeType(ofd.FileName);
                        fileSize = fileBytes.Length;
                        txtFilePath.Text = ofd.FileName;

                        // استدعاء دالة المعاينة مع تمرير المسار الحقيقي للملف الذي تم اختياره
                        PreviewDocument(ofd.FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ في تحميل الملف: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetMimeType(string filePath)
        {
            string extension = Path.GetExtension(filePath).ToLower();
            switch (extension)
            {
                case ".pdf": return "application/pdf";
                case ".jpg": case ".jpeg": return "image/jpeg";
                case ".png": return "image/png";
                case ".doc": return "application/msword";
                case ".docx": return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                case ".xls": return "application/vnd.ms-excel";
                case ".xlsx": return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                default: return "application/octet-stream";
            }
        }

        private void ClearPreview()
        {
            if (picImagePreview.Image != null)
            {
                picImagePreview.Image.Dispose();
                picImagePreview.Image = null;
            }
            picImagePreview.Visible = false;

            if (pdfViewer != null)
            {
                if (pdfViewer.Document != null)
                {
                    pdfViewer.Document.Dispose();
                    pdfViewer.Document = null;
                }
                pdfViewer.Visible = false;
            }
            if (docBrowser != null) docBrowser.Visible = false;
            lblFilePreview.Visible = false;
        }

        private void ShowPdfInViewer(MemoryStream stream)
        {
            try
            {
                if (stream != null && stream.Length > 0)
                {
                    stream.Position = 0;

                    if (pdfViewer == null)
                    {
                        pdfViewer = new PdfViewer();
                        pdfViewer.Dock = DockStyle.Fill;
                        GroupBox1.Controls.Add(pdfViewer);
                    }

                    if (pdfViewer.Document != null)
                    {
                        pdfViewer.Document.Dispose();
                        pdfViewer.Document = null;
                    }

                    pdfViewer.Document = PdfiumViewer.PdfDocument.Load(stream);
                    pdfViewer.Visible = true;
                    pdfViewer.BringToFront();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ داخلي أثناء رسم ملف المعاينة: " + ex.Message, "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PreviewDocument(string actualFilePath = "")
        {
            try
            {
                // التحقق الذكي: إذا كان المسار فارغاً (مثلاً عند القراءة من قاعدة البيانات في وضع التعديل) نقوم بإنشاء ملف مؤقت فريد
                if (string.IsNullOrEmpty(actualFilePath) && fileBytes != null && fileBytes.Length > 0)
                {
                    // استخدام Guid لمنع تضارب أسماء الملفات وقفلها في الـ Temp
                    string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(fileName);
                    actualFilePath = Path.Combine(Path.GetTempPath(), uniqueFileName);
                    File.WriteAllBytes(actualFilePath, fileBytes);
                }

                if (!File.Exists(actualFilePath) && (fileBytes == null || fileBytes.Length == 0))
                {
                    ClearPreview();
                    return;
                }

                // تصفية الشاشة لإظهار العنصر الجديد
                lblFilePreview.Visible = false;
                picImagePreview.Visible = false;
                if (pdfViewer != null) pdfViewer.Visible = false;
                if (docBrowser != null) docBrowser.Visible = false;

                // الحصول على الامتداد الحقيقي للملف كخط دفاع أول بدلاً من الاعتماد الكلي على الـ MimeType
                string fileExtension = Path.GetExtension(actualFilePath).ToLower();

                // 1. معالجة الصور
                if (fileType.StartsWith("image/") || fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".png")
                {
                    byte[] bytesToLoad = (fileBytes != null && fileBytes.Length > 0) ? fileBytes : File.ReadAllBytes(actualFilePath);
                    using (MemoryStream ms = new MemoryStream(bytesToLoad))
                    {
                        if (picImagePreview.Image != null) picImagePreview.Image.Dispose();
                        picImagePreview.Image = Image.FromStream(ms);
                    }
                    picImagePreview.Dock = DockStyle.Fill;
                    picImagePreview.SizeMode = PictureBoxSizeMode.Zoom;
                    picImagePreview.Visible = true;
                    picImagePreview.BringToFront();
                }
                // 2. معالجة ملفات PDF الصافية
                else if (fileType.Contains("pdf") || fileExtension == ".pdf")
                {
                    using (FileStream fs = new FileStream(actualFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        MemoryStream ms = new MemoryStream();
                        fs.CopyTo(ms);
                        ms.Position = 0;
                        ShowPdfInViewer(ms);
                    }
                }
                // 3. معالجة مستندات الـ Word (DOC / DOCX)
                else if (fileType.Contains("msword") || fileType.Contains("wordprocessingml") || fileExtension == ".doc" || fileExtension == ".docx")
                {
                    try
                    {
                        if (File.Exists(actualFilePath))
                        {
                            using (Spire.Doc.Document doc = new Spire.Doc.Document())
                            {
                                doc.LoadFromFile(actualFilePath);
                                MemoryStream pdfStream = new MemoryStream();
                                doc.SaveToStream(pdfStream, Spire.Doc.FileFormat.PDF);
                                pdfStream.Position = 0;
                                ShowPdfInViewer(pdfStream);
                            }
                        }
                    }
                    catch (Exception exSpireDoc)
                    {
                        Debug.WriteLine($"فشل المعاينة الداخلية للـ Word: {exSpireDoc.Message}");
                        OpenComponentExternally(actualFilePath);
                    }
                }
                // 4. معالجة مستندات الـ Excel (XLS / XLSX)
                else if (fileType.Contains("ms-excel") || fileType.Contains("spreadsheetml") || fileExtension == ".xls" || fileExtension == ".xlsx")
                {
                    try
                    {
                        if (File.Exists(actualFilePath))
                        {
                            using (Spire.Xls.Workbook workbook = new Spire.Xls.Workbook())
                            {
                                workbook.LoadFromFile(actualFilePath);
                                MemoryStream pdfStream = new MemoryStream();
                                workbook.SaveToStream(pdfStream, Spire.Xls.FileFormat.PDF);
                                pdfStream.Position = 0;
                                ShowPdfInViewer(pdfStream);
                            }
                        }
                    }
                    catch (Exception exSpireXls)
                    {
                        Debug.WriteLine($"فشل المعاينة الداخلية للـ Excel: {exSpireXls.Message}");
                        OpenComponentExternally(actualFilePath);
                    }
                }
                else
                {
                    lblFilePreview.Text = "لا يمكن معاينة هذا النوع من الملفات داخلياً.";
                    lblFilePreview.Visible = true;
                    OpenComponentExternally(actualFilePath);
                }
            }
            catch (Exception ex)
            {
                ClearPreview();
                MessageBox.Show("حدث خطأ غير متوقع أثناء تحميل المعاينة: " + ex.Message, "خطأ عام", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenComponentExternally(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        FileName = filePath,
                        UseShellExecute = true
                    };
                    Process.Start(startInfo);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"تعذر فتح الملف خارجياً: {ex.Message}");
            }
        }

        #endregion

        #region 9. إدارة صور الصناديق الافتراضية (Box Images)

        public void LoadBoxImage()
        {
            try
            {
                if (boxId > 0)
                {
                    Image boxImage = ArchiveBoxService.GetBoxImage(boxId);
                    if (boxImage != null)
                    {
                        picBoxImage.Image = boxImage;
                        return;
                    }
                }
                SetDefaultBoxImage();
            }
            catch
            {
                SetDefaultBoxImage();
            }
        }

        private void SetDefaultBoxImage()
        {
            var img = new Bitmap(picBoxImage.Width, picBoxImage.Height);
            using (Graphics g = Graphics.FromImage(img))
            {
                g.Clear(Color.FromArgb(240, 240, 240));
                using (Font font = new Font("Segoe UI", 20, FontStyle.Bold))
                using (Brush brush = new SolidBrush(Color.LightGray))
                {
                    g.DrawString("📁", font, brush, new PointF(picBoxImage.Width / 2 - 15, picBoxImage.Height / 2 - 15));
                }
            }
            picBoxImage.Image = img;
        }

        #endregion

        #region 10. الحفظ (Save Document)
        #region 10. الحفظ (Save Document)

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveDocument();
        }

        private void btnSaveDocument_Click(object sender, EventArgs e)
        {
            SaveDocument();
        }

        private void SaveDocument()
        {
            if (!ValidateInput()) return;

            try
            {
                // ✅ تحميل مفتاح التشفير الرئيسي
                byte[] masterKey = LoadMasterKey();
                A_WFA.Security.CryptoService.Initialize(masterKey);

                // ✅ 1. حفظ الملف في نظام الملفات (إذا وجد) مع التشفير
                string filePath = null;
                string fileHash = null;
                long fileSize = 0;

                if (fileBytes != null && fileBytes.Length > 0)
                {
                    // ✅ تشفير الملف قبل الحفظ
                    byte[] encryptedData = A_WFA.Security.CryptoService.Encrypt(fileBytes);

                    // حفظ الملف المشفر في نظام الملفات
                    string storagePath = DatabaseManagerLite.GetStoragePath();
                    string year = DateTime.Now.ToString("yyyy");
                    string month = DateTime.Now.ToString("MM");
                    string boxFolder = boxId > 0 ? $"Box_{boxId:D3}" : "General";

                    string fullPath = Path.Combine(storagePath, year, month, boxFolder);
                    if (!Directory.Exists(fullPath))
                        Directory.CreateDirectory(fullPath);

                    // إنشاء اسم ملف فريد
                    string fileId = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(fileName);
                    string encryptedFileName = $"{fileId}{extension}.enc";
                    filePath = Path.Combine(year, month, boxFolder, encryptedFileName);

                    string fullFilePath = Path.Combine(storagePath, filePath);

                    // ✅ حفظ الملف المشفر
                    File.WriteAllBytes(fullFilePath, encryptedData);

                    // ✅ حساب Hash للملف الأصلي (للتحقق من السلامة)
                    fileHash = A_WFA.Security.CryptoService.ComputeHash(fileBytes);
                    fileSize = fileBytes.Length;
                }

                // ✅ 2. تشفير جميع البيانات النصية قبل الحفظ
                var document = new D.DocumentModel
                {
                    // ✅ تشفير العنوان
                    Title = A_WFA.Security.CryptoService.EncryptString(txtTitle.Text.Trim()),

                    // ✅ تشفير اسم الملف ونوعه
                    FileName = A_WFA.Security.CryptoService.EncryptString(fileName),
                    FileType = A_WFA.Security.CryptoService.EncryptString(fileType),

                    // بيانات الملف (غير مشفرة)
                    FileSize = fileSize,
                    FileData = fileBytes,
                    FilePath = filePath,
                    FileHash = fileHash,

                    // المعرفات (غير مشفرة)
                    DocumentTypeId = Convert.ToInt32(cmbDocumentType.SelectedValue),
                    CategoryId = Convert.ToInt32(cmbCategory.SelectedValue),
                    FromDepartmentId = cmbFromDepartment.SelectedValue != null ? Convert.ToInt32(cmbFromDepartment.SelectedValue) : 0,
                    ToDepartmentId = cmbToDepartment.SelectedValue != null ? Convert.ToInt32(cmbToDepartment.SelectedValue) : 0,
                    BoxId = boxId > 0 ? boxId : Convert.ToInt32(cmbcangeboxs.SelectedValue),
                    UploadedBy = GetCurrentUserId(),
                    ArchiveDocNumber = currentDocSequence.ToString("000"),
                    ReferenceNumber = "",

                    // ✅ تشفير التواريخ
                    DocumentDate = A_WFA.Security.CryptoService.EncryptString(dtpDocumentDate.Value.ToString("yyyy/MM/dd")),
                    ReceiveDate = A_WFA.Security.CryptoService.EncryptString(dtpReceiveDate.Value.ToString("yyyy/MM/dd")),
                    IssueDate = A_WFA.Security.CryptoService.EncryptString(dtpIssueDate.Value.ToString("yyyy/MM/dd")),

                    // ✅ تشفير النصوص الطويلة
                    Notes = A_WFA.Security.CryptoService.EncryptString(txtNotes.Text.Trim()),
                    Summary = A_WFA.Security.CryptoService.EncryptString(txtSummary.Text.Trim()),

                    // ✅ تشفير الحالة والأولوية والطبيعة
                    Status = A_WFA.Security.CryptoService.EncryptString(cmbStatus.Text),
                    Priority = A_WFA.Security   .CryptoService.EncryptString(cmbPriority.Text),
                    DocumentNature = A_WFA.Security.CryptoService.EncryptString(Cmbdocument_nature.Text)
                };

                bool success = false;
                if (isEditMode)
                {
                    document.Id = currentDocumentId;
                    success = DatabaseModuleLite.UpdateDocument(document);
                }
                else
                {
                    success = DatabaseModuleLite.SaveDocument(document);
                }

                if (success)
                {
                    DatabaseManagerLite.SafeLogAuditTrail(GetCurrentUserId(), "save_success", $"تم حفظ الوثيقة بنجاح");
                    MessageBox.Show("تم حفظ الوثيقة بنجاح", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (isEditMode)
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        ResetFormForNewDocument();
                    }
                }
                else
                {
                    DatabaseManagerLite.SafeLogAuditTrail(GetCurrentUserId(), "save_failed", "فشل في حفظ الوثيقة");
                    MessageBox.Show("فشل في حفظ الوثيقة", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                DatabaseManagerLite.SafeLogAuditTrail(GetCurrentUserId(), "save_error", $"خطأ أثناء الحفظ: {ex.Message}");
                MessageBox.Show("خطأ في حفظ الوثيقة: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
        
        private void ResetFormForNewDocument()
        {
            try
            {
                currentDocumentId = 0;
                isEditMode = false;
                this.Text = "إضافة وثيقة جديدة";

                ClearForm();
                SetSmartDefaults();
                UpdateUIForMode();
                nomberArshiv();

                MessageBox.Show("تم تهيئة النموذج لوثيقة جديدة", "جاهز", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"حدث خطأ أثناء إعداد وثيقة جديدة: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearForm()
        {
            txtTitle.Clear();
            txtSummary.Clear();
            txtNotes.Clear();
            txtArchiveNumber.Clear();
            txtFilePath.Clear();
            fileBytes = null;
            fileName = "";
            fileType = "";
            fileSize = 0;
            _existingFilePath = "";
            ClearPreview();
            RefreshSoldiersDataGridView(new List<TemporarySoldierInfo>());
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("يرجى إدخال عنوان الوثيقة", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTitle.Focus();
                return false;
            }

            if (cmbDocumentType.SelectedValue == null)
            {
                MessageBox.Show("يرجى اختيار نوع الوثيقة", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbDocumentType.Focus();
                return false;
            }

            if (cmbCategory.SelectedValue == null)
            {
                MessageBox.Show("يرجى اختيار التصنيف", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCategory.Focus();
                return false;
            }

            return true;
        }

        private void UpdateUIForMode()
        {
            if (isEditMode)
            {
                btnSave.Text = "💾 تحديث";
                btnSaveDocument.Text = "💾 تحديث المستند";
            }
            else
            {
                btnSave.Text = "💾 حفظ";
                btnSaveDocument.Text = "💾 حفظ المستند";
            }
        }
        #endregion

        #region 11. أزرار إضافية (Placeholders)

        private void btnCancel_Click(object sender, EventArgs e) { this.Close(); }
        private void btnAddSoldiers_Click(object sender, EventArgs e) { /* فتح شاشة إضافة الموظفين/الجنود */ }
        private void btnInfo_Click(object sender, EventArgs e) { }
        private void btnScanDocument_Click(object sender, EventArgs e) { }
        private void btnOpenScanner_Click(object sender, EventArgs e) { }
        private void btnAddNewDocument_Click(object sender, EventArgs e) { }
        private void btnClearForm_Click(object sender, EventArgs e) { ClearPreview(); txtTitle.Clear(); }
        private void btnLoadTemplate_Click(object sender, EventArgs e) { }
        private void btnCopyCurrent_Click(object sender, EventArgs e) { }
        private void Button1_Click(object sender, EventArgs e) { }

        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e) { }
        private void Label14_Click(object sender, EventArgs e) { }
        private void Label13_Click(object sender, EventArgs e) { }
        private void Label4_Click(object sender, EventArgs e) { }

        private void SetupSoldiersDataGridView() { }
        private void RefreshSoldiersDataGridView(List<TemporarySoldierInfo> list) { }
        private void SetupToolTips() { }
        private void UpdateDocumentInfo() { }
        private int GetCurrentUserId() { return 1; }

        #endregion

        public void SetBoxName(string name)
        {
            boxName = name;
            lblBoxName.Text = name;
        }

    }

    #region الكلاسات المساعدة المؤقتة (Helper Classes)
    public class TemporarySoldierInfo
    {
        public int SoldierId { get; set; }
        public string SoldierName { get; set; }
        public string RelationshipType { get; set; }
        public string RelationDate { get; set; }
        public string Notes { get; set; }
    }
    #endregion
}