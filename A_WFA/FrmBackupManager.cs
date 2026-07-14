using A_WFA.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Windows.Forms;

namespace A_WFA
{
    public partial class FrmBackupManager : Form
    {
        private string _selectedBackupPath;
        private List<BackupFileInfo> _backupFiles;

        public FrmBackupManager()
        {
            InitializeComponent();
            InitializeForm();
            LoadBackupFiles();
        }

        #region "تهيئة الواجهة"

        private void InitializeForm()
        {
            this.Text = "🗄️ إدارة النسخ الاحتياطية";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(900, 600);
            this.MinimumSize = new Size(800, 500);
            this.BackColor = Color.FromArgb(240, 240, 240);

            // ربط الأحداث
            this.Load += FrmBackupManager_Load;
            BtnCreateBackup.Click += BtnCreateBackup_Click;
            BtnRestoreBackup.Click += BtnRestoreBackup_Click;
            BtnExportBackup.Click += BtnExportBackup_Click;
            BtnImportBackup.Click += BtnImportBackup_Click;
            BtnDeleteBackup.Click += BtnDeleteBackup_Click;
            BtnRefresh.Click += BtnRefresh_Click;
            BtnOpenFolder.Click += BtnOpenFolder_Click;
            BtnSettings.Click += BtnSettings_Click;
            DgvBackups.SelectionChanged += DgvBackups_SelectionChanged;
            TxtSearch.TextChanged += TxtSearch_TextChanged;
            CmbFilter.SelectedIndexChanged += CmbFilter_SelectedIndexChanged;

            // إعداد الـ DataGridView
            SetupDataGridView();
        }

        private void SetupDataGridView()
        {
            DgvBackups.AutoGenerateColumns = false;
            DgvBackups.AllowUserToAddRows = false;
            DgvBackups.AllowUserToDeleteRows = false;
            DgvBackups.ReadOnly = true;
            DgvBackups.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DgvBackups.MultiSelect = false;
            DgvBackups.RowHeadersVisible = false;
            DgvBackups.BackgroundColor = Color.White;
            DgvBackups.BorderStyle = BorderStyle.FixedSingle;
            DgvBackups.GridColor = Color.FromArgb(220, 220, 220);

            // الأعمدة
            DgvBackups.Columns.Clear();
            DgvBackups.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Name",
                HeaderText = "اسم الملف",
                Width = 200,
                DataPropertyName = "FileName"
            });

            DgvBackups.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Size",
                HeaderText = "الحجم",
                Width = 100,
                DataPropertyName = "SizeFormatted"
            });

            DgvBackups.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Date",
                HeaderText = "تاريخ الإنشاء",
                Width = 150,
                DataPropertyName = "CreatedDateFormatted"
            });

            DgvBackups.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Type",
                HeaderText = "النوع",
                Width = 100,
                DataPropertyName = "Type"
            });

            DgvBackups.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Status",
                HeaderText = "الحالة",
                Width = 100,
                DataPropertyName = "Status"
            });

            DgvBackups.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Path",
                HeaderText = "المسار",
                Width = 200,
                DataPropertyName = "FullPath"
            });
        }

        #endregion

        #region "تحميل النسخ الاحتياطية"

        private void FrmBackupManager_Load(object sender, EventArgs e)
        {
            // تعيين خيارات الفلتر
            CmbFilter.Items.Clear();
            CmbFilter.Items.AddRange(new object[] { "الكل", "النسخ الكاملة", "النسخ الجزئية", "المضغوطة" });
            CmbFilter.SelectedIndex = 0;

            // تحديث الإحصائيات
            UpdateStatistics();
        }

        private void LoadBackupFiles()
        {
            try
            {
                string backupFolder = GetBackupFolder();
                if (!Directory.Exists(backupFolder))
                {
                    Directory.CreateDirectory(backupFolder);
                }

                var files = Directory.GetFiles(backupFolder, "*.*", SearchOption.TopDirectoryOnly)
                    .Where(f => IsBackupFile(f))
                    .Select(f => new BackupFileInfo
                    {
                        FullPath = f,
                        FileName = Path.GetFileName(f),
                        CreatedDate = File.GetCreationTime(f),
                        Size = new FileInfo(f).Length,
                        Type = GetBackupType(f),
                        Status = GetBackupStatus(f)
                    })
                    .OrderByDescending(f => f.CreatedDate)
                    .ToList();

                // تطبيق الفلتر والبحث
                _backupFiles = ApplyFilters(files);
                DgvBackups.DataSource = _backupFiles;

                // تحديث الإحصائيات
                UpdateStatistics();

                // تحديد الصف الأول
                if (DgvBackups.Rows.Count > 0)
                {
                    DgvBackups.Rows[0].Selected = true;
                }

                LblCount.Text = $"📊 عدد النسخ: {_backupFiles.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في تحميل النسخ الاحتياطية: {ex.Message}", "خطأ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<BackupFileInfo> ApplyFilters(List<BackupFileInfo> files)
        {
            var query = files.AsEnumerable();

            // فلتر حسب النوع
            string filter = CmbFilter.SelectedItem?.ToString() ?? "الكل";
            if (filter != "الكل")
            {
                query = query.Where(f => f.Type == filter);
            }

            // بحث
            string searchText = TxtSearch.Text.Trim();
            if (!string.IsNullOrEmpty(searchText))
            {
                query = query.Where(f => f.FileName.ToLower().Contains(searchText.ToLower()));
            }

            return query.ToList();
        }

        private bool IsBackupFile(string filePath)
        {
            string ext = Path.GetExtension(filePath).ToLower();
            return ext == ".bak" || ext == ".zip" || ext == ".db";
        }

        private string GetBackupType(string filePath)
        {
            string ext = Path.GetExtension(filePath).ToLower();
            switch (ext)
            {
                case ".bak": return "نسخة كاملة";
                case ".zip": return "مضغوطة";
                case ".db": return "قاعدة بيانات";
                default: return "أخرى";
            }
        }

        private string GetBackupStatus(string filePath)
        {
            try
            {
                var info = new FileInfo(filePath);
                // إذا كان الملف قديم (أكثر من 30 يوم)
                if (info.CreationTime < DateTime.Now.AddDays(-30))
                    return "قديم";

                // إذا كان الملف كبير جداً
                if (info.Length > 100 * 1024 * 1024) // 100 MB
                    return "كبير";

                // إذا كان الملف صغير جداً (أقل من 1 KB)
                if (info.Length < 1024)
                    return "صغير (تحذير)";

                return "جيد";
            }
            catch
            {
                return "غير معروف";
            }
        }

        private string GetBackupFolder()
        {
            // استخدام المسار من الإعدادات أو المسار الافتراضي
            string backupPath = DatabaseManagerLite.GetSetting("BackupPath");
            if (string.IsNullOrEmpty(backupPath))
            {
                backupPath = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "A_WFA",
                    "Backups"
                );
            }
            return backupPath;
        }

        #endregion

        #region "إنشاء نسخة احتياطية"

        private void BtnCreateBackup_Click(object sender, EventArgs e)
        {
            try
            {
                // اختيار نوع النسخة
                using (var form = new FrmCreateBackupOptions())
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        Cursor = Cursors.WaitCursor;
                        string backupPath = CreateBackup(form.SelectedType, form.IncludeFiles, form.Compress);
                        Cursor = Cursors.Default;

                        if (!string.IsNullOrEmpty(backupPath))
                        {
                            MessageBox.Show(
                                $"✅ تم إنشاء النسخة الاحتياطية بنجاح!\n\n" +
                                $"الموقع: {backupPath}\n" +
                                $"الحجم: {GetFileSize(new FileInfo(backupPath).Length)}",
                                "نجاح",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information
                            );

                            LoadBackupFiles();
                            DatabaseManagerLite.SafeLogAuditTrail(
                                1,
                                "CREATE_BACKUP",
                                $"تم إنشاء نسخة احتياطية: {Path.GetFileName(backupPath)}"
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show($"خطأ في إنشاء النسخة الاحتياطية: {ex.Message}", "خطأ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string CreateBackup(string type, bool includeFiles, bool compress)
        {
            try
            {
                string backupFolder = GetBackupFolder();
                if (!Directory.Exists(backupFolder))
                    Directory.CreateDirectory(backupFolder);

                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string fileName = $"ArchiveBackup_{timestamp}";
                string backupPath;

                // 1. إنشاء مجلد مؤقت
                string tempFolder = Path.Combine(Path.GetTempPath(), $"Backup_{Guid.NewGuid()}");
                Directory.CreateDirectory(tempFolder);

                // 2. نسخ قاعدة البيانات
                string dbPath = DatabaseManagerLite.GetDatabaseFilePath();
                string dbBackupPath = Path.Combine(tempFolder, "Archive.db");
                File.Copy(dbPath, dbBackupPath, true);

                // 3. نسخ الملفات (إذا كان مطلوباً)
                if (includeFiles)
                {
                    string filesPath = DatabaseManagerLite.GetStoragePath();
                    if (Directory.Exists(filesPath))
                    {
                        string filesBackupPath = Path.Combine(tempFolder, "Files");
                        CopyDirectory(filesPath, filesBackupPath);
                    }
                }

                // 4. إنشاء ملف معلومات
                string infoPath = Path.Combine(tempFolder, "BackupInfo.txt");
                string info = $@"
╔═══════════════════════════════════════════════════════════════╗
║                   معلومات النسخة الاحتياطية                  ║
╠═══════════════════════════════════════════════════════════════╣
║                                                               ║
║  📅 تاريخ الإنشاء: {DateTime.Now:yyyy/MM/dd HH:mm:ss}       ║
║  🏷️  نوع النسخة: {type}                                     ║
║  📁 تشمل الملفات: {(includeFiles ? "نعم" : "لا")}           ║
║  📦 مضغوطة: {(compress ? "نعم" : "لا")}                     ║
║  💾 حجم قاعدة البيانات: {GetFileSize(new FileInfo(dbPath).Length)} ║
║  ⚙️  إصدار النظام: {Application.ProductVersion}             ║
║  🖥️  الجهاز: {Environment.MachineName}                      ║
║  👤 المستخدم: {Environment.UserName}                        ║
║                                                               ║
║  ⚠️  ملاحظات:                                                ║
║  • لا تقم بتعديل محتويات هذه النسخة                         ║
║  • استخدم زر الاستعادة لاسترجاع البيانات                    ║
║  • احتفظ بنسخة من المفتاح في مكان آمن                       ║
║                                                               ║
╚═══════════════════════════════════════════════════════════════╝
                ";
                File.WriteAllText(infoPath, info, System.Text.Encoding.UTF8);

                // 5. حفظ النسخة
                if (compress)
                {
                    // ✅ استخدام ZipFile
                    string zipPath = Path.Combine(backupFolder, $"{fileName}.zip");
                    ZipFile.CreateFromDirectory(tempFolder, zipPath);
                    backupPath = zipPath;
                }
                else
                {
                    // نسخ المجلد بالكامل
                    string folderPath = Path.Combine(backupFolder, fileName);
                    Directory.CreateDirectory(folderPath);

                    foreach (string file in Directory.GetFiles(tempFolder, "*.*", SearchOption.AllDirectories))
                    {
                        string relativePath = file.Replace(tempFolder, "");
                        string destFile = Path.Combine(folderPath, relativePath.TrimStart('\\'));
                        Directory.CreateDirectory(Path.GetDirectoryName(destFile));
                        File.Copy(file, destFile, true);
                    }
                    backupPath = folderPath;
                }

                // 6. تنظيف المجلد المؤقت
                try { Directory.Delete(tempFolder, true); } catch { }

                return backupPath;
            }
            catch (Exception ex)
            {
                throw new Exception($"فشل إنشاء النسخة الاحتياطية: {ex.Message}");
            }
        }

        #endregion

        #region "استعادة نسخة احتياطية"

        private void BtnRestoreBackup_Click(object sender, EventArgs e)
        {
            if (DgvBackups.SelectedRows.Count == 0)
            {
                MessageBox.Show("الرجاء اختيار نسخة احتياطية أولاً!", "تنبيه",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selected = (BackupFileInfo)DgvBackups.SelectedRows[0].DataBoundItem;
            if (selected == null) return;

            // تأكيد الاستعادة
            DialogResult result = MessageBox.Show(
                $"⚠️ تحذير: سيتم استعادة البيانات من النسخة:\n\n" +
                $"📁 {selected.FileName}\n" +
                $"📅 {selected.CreatedDate:yyyy/MM/dd HH:mm}\n" +
                $"💾 {selected.SizeFormatted}\n\n" +
                $"سيتم استبدال جميع البيانات الحالية!\n" +
                $"هل أنت متأكد من المتابعة؟",
                "تأكيد الاستعادة",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;
                    bool success = RestoreBackup(selected.FullPath);
                    Cursor = Cursors.Default;

                    if (success)
                    {
                        MessageBox.Show(
                            "✅ تم استعادة البيانات بنجاح!\n\n" +
                            "سيتم إعادة تشغيل التطبيق لتطبيق التغييرات.",
                            "نجاح",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );

                        DatabaseManagerLite.SafeLogAuditTrail(
                            1,
                            "RESTORE_BACKUP",
                            $"تم استعادة النسخة: {selected.FileName}"
                        );

                        // إعادة تشغيل التطبيق
                        Application.Restart();
                    }
                }
                catch (Exception ex)
                {
                    Cursor = Cursors.Default;
                    MessageBox.Show($"خطأ في استعادة النسخة: {ex.Message}", "خطأ",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool RestoreBackup(string backupPath)
        {
            try
            {
                string tempFolder = Path.Combine(Path.GetTempPath(), $"Restore_{Guid.NewGuid()}");
                Directory.CreateDirectory(tempFolder);

                // فك الضغط إذا كان الملف ZIP
                if (Path.GetExtension(backupPath).ToLower() == ".zip")
                {
                    ZipFile.ExtractToDirectory(backupPath, tempFolder);
                }
                else if (Directory.Exists(backupPath))
                {
                    // نسخ الملفات من المجلد
                    foreach (string file in Directory.GetFiles(backupPath, "*.*", SearchOption.AllDirectories))
                    {
                        string relativePath = file.Replace(backupPath, "");
                        string destFile = Path.Combine(tempFolder, relativePath.TrimStart('\\'));
                        Directory.CreateDirectory(Path.GetDirectoryName(destFile));
                        File.Copy(file, destFile, true);
                    }
                }
                else
                {
                    throw new Exception("نوع الملف غير مدعوم");
                }

                // التحقق من وجود ملف قاعدة البيانات
                string dbFile = Path.Combine(tempFolder, "Archive.db");
                if (!File.Exists(dbFile))
                {
                    throw new Exception("ملف قاعدة البيانات غير موجود في النسخة!");
                }

                // إغلاق اتصالات قاعدة البيانات
                // (يفترض أن هناك دالة لإغلاق الاتصالات)

                // استعادة قاعدة البيانات
                string targetDb = DatabaseManagerLite.GetDatabaseFilePath();
                File.Copy(dbFile, targetDb, true);

                // استعادة الملفات إذا كانت موجودة
                string filesFolder = Path.Combine(tempFolder, "Files");
                if (Directory.Exists(filesFolder))
                {
                    string targetFiles = DatabaseManagerLite.GetStoragePath();
                    CopyDirectory(filesFolder, targetFiles);
                }

                // تنظيف المجلد المؤقت
                try { Directory.Delete(tempFolder, true); } catch { }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"فشل استعادة النسخة: {ex.Message}");
            }
        }

        #endregion

        #region "تصدير واستيراد"

        private void BtnExportBackup_Click(object sender, EventArgs e)
        {
            if (DgvBackups.SelectedRows.Count == 0)
            {
                MessageBox.Show("الرجاء اختيار نسخة للتصدير!", "تنبيه",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selected = (BackupFileInfo)DgvBackups.SelectedRows[0].DataBoundItem;
            if (selected == null) return;

            using (var saveDialog = new SaveFileDialog())
            {
                saveDialog.Title = "تصدير النسخة الاحتياطية";
                saveDialog.Filter = "ملفات النسخ الاحتياطية (*.zip)|*.zip|جميع الملفات (*.*)|*.*";
                saveDialog.FileName = selected.FileName;
                saveDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        Cursor = Cursors.WaitCursor;

                        // إذا كانت النسخة ملف ZIP، انسخه مباشرة
                        if (selected.FullPath.EndsWith(".zip"))
                        {
                            File.Copy(selected.FullPath, saveDialog.FileName, true);
                        }
                        else
                        {
                            // ضغط المجلد
                            ZipFile.CreateFromDirectory(selected.FullPath, saveDialog.FileName);
                        }

                        Cursor = Cursors.Default;

                        MessageBox.Show(
                            $"✅ تم تصدير النسخة بنجاح!\n\n" +
                            $"الموقع: {saveDialog.FileName}",
                            "نجاح",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );

                        DatabaseManagerLite.SafeLogAuditTrail(
                            1,
                            "EXPORT_BACKUP",
                            $"تم تصدير النسخة: {selected.FileName}"
                        );
                    }
                    catch (Exception ex)
                    {
                        Cursor = Cursors.Default;
                        MessageBox.Show($"خطأ في التصدير: {ex.Message}", "خطأ",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void BtnImportBackup_Click(object sender, EventArgs e)
        {
            using (var openDialog = new OpenFileDialog())
            {
                openDialog.Title = "استيراد نسخة احتياطية";
                openDialog.Filter = "ملفات النسخ الاحتياطية (*.zip;*.bak)|*.zip;*.bak|جميع الملفات (*.*)|*.*";
                openDialog.CheckFileExists = true;
                openDialog.Multiselect = false;

                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        Cursor = Cursors.WaitCursor;

                        // نسخ الملف إلى مجلد النسخ الاحتياطية
                        string backupFolder = GetBackupFolder();
                        if (!Directory.Exists(backupFolder))
                            Directory.CreateDirectory(backupFolder);

                        string destFile = Path.Combine(backupFolder, Path.GetFileName(openDialog.FileName));

                        // إذا كان الملف موجوداً، أضف رقم
                        if (File.Exists(destFile))
                        {
                            string name = Path.GetFileNameWithoutExtension(openDialog.FileName);
                            string ext = Path.GetExtension(openDialog.FileName);
                            destFile = Path.Combine(backupFolder, $"{name}_{DateTime.Now:yyyyMMdd_HHmmss}{ext}");
                        }

                        File.Copy(openDialog.FileName, destFile, true);

                        Cursor = Cursors.Default;

                        MessageBox.Show(
                            $"✅ تم استيراد النسخة بنجاح!\n\n" +
                            $"الموقع: {destFile}",
                            "نجاح",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );

                        DatabaseManagerLite.SafeLogAuditTrail(
                            1,
                            "IMPORT_BACKUP",
                            $"تم استيراد نسخة: {Path.GetFileName(destFile)}"
                        );

                        LoadBackupFiles();
                    }
                    catch (Exception ex)
                    {
                        Cursor = Cursors.Default;
                        MessageBox.Show($"خطأ في الاستيراد: {ex.Message}", "خطأ",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        #endregion

        #region "حذف نسخة احتياطية"

        private void BtnDeleteBackup_Click(object sender, EventArgs e)
        {
            if (DgvBackups.SelectedRows.Count == 0)
            {
                MessageBox.Show("الرجاء اختيار نسخة للحذف!", "تنبيه",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selected = (BackupFileInfo)DgvBackups.SelectedRows[0].DataBoundItem;
            if (selected == null) return;

            DialogResult result = MessageBox.Show(
                $"⚠️ هل أنت متأكد من حذف النسخة:\n\n" +
                $"📁 {selected.FileName}\n" +
                $"📅 {selected.CreatedDate:yyyy/MM/dd HH:mm}\n" +
                $"💾 {selected.SizeFormatted}\n\n" +
                $"لا يمكن استرجاعها بعد الحذف!",
                "تأكيد الحذف",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;

                    if (File.Exists(selected.FullPath))
                    {
                        File.Delete(selected.FullPath);
                    }
                    else if (Directory.Exists(selected.FullPath))
                    {
                        Directory.Delete(selected.FullPath, true);
                    }

                    Cursor = Cursors.Default;

                    MessageBox.Show("✅ تم حذف النسخة بنجاح!", "نجاح",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    DatabaseManagerLite.SafeLogAuditTrail(
                        1,
                        "DELETE_BACKUP",
                        $"تم حذف النسخة: {selected.FileName}"
                    );

                    LoadBackupFiles();
                }
                catch (Exception ex)
                {
                    Cursor = Cursors.Default;
                    MessageBox.Show($"خطأ في الحذف: {ex.Message}", "خطأ",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion

        #region "أزرار إضافية"

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadBackupFiles();
            DatabaseManagerLite.SafeLogAuditTrail(1, "REFRESH", "تحديث قائمة النسخ الاحتياطية");
        }

        private void BtnOpenFolder_Click(object sender, EventArgs e)
        {
            try
            {
                string backupFolder = GetBackupFolder();
                if (!Directory.Exists(backupFolder))
                    Directory.CreateDirectory(backupFolder);

                Process.Start("explorer.exe", backupFolder);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في فتح المجلد: {ex.Message}", "خطأ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSettings_Click(object sender, EventArgs e)
        {
            using (var form = new FrmBackupSettings())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadBackupFiles();
                }
            }
        }

        #endregion

        #region "أحداث إضافية"

        private void DgvBackups_SelectionChanged(object sender, EventArgs e)
        {
            bool hasSelection = DgvBackups.SelectedRows.Count > 0;
            BtnRestoreBackup.Enabled = hasSelection;
            BtnExportBackup.Enabled = hasSelection;
            BtnDeleteBackup.Enabled = hasSelection;

            if (hasSelection)
            {
                var selected = (BackupFileInfo)DgvBackups.SelectedRows[0].DataBoundItem;
                if (selected != null)
                {
                    LblSelectedInfo.Text = $"✅ المحدد: {selected.FileName} - {selected.SizeFormatted}";
                }
            }
            else
            {
                LblSelectedInfo.Text = "❌ لم يتم اختيار نسخة";
            }
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadBackupFiles();
        }

        private void CmbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadBackupFiles();
        }

        #endregion

        #region "تحديث الإحصائيات"

        private void UpdateStatistics()
        {
            try
            {
                string backupFolder = GetBackupFolder();
                if (!Directory.Exists(backupFolder))
                {
                    LblTotalBackups.Text = "📊 0 نسخ";
                    LblTotalSize.Text = "💾 0";
                    return;
                }

                var files = Directory.GetFiles(backupFolder, "*.*", SearchOption.TopDirectoryOnly)
                    .Where(f => IsBackupFile(f))
                    .ToList();

                long totalSize = files.Sum(f => new FileInfo(f).Length);
                int totalCount = files.Count;

                LblTotalBackups.Text = $"📊 {totalCount} نسخ";
                LblTotalSize.Text = $"💾 {GetFileSize(totalSize)}";
            }
            catch
            {
                LblTotalBackups.Text = "📊 0 نسخ";
                LblTotalSize.Text = "💾 0";
            }
        }

        #endregion

        #region "دوال مساعدة"

        private string GetFileSize(long size)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            int order = 0;
            while (size >= 1024 && order < sizes.Length - 1)
            {
                order++;
                size = size / 1024;
            }
            return $"{size:0.##} {sizes[order]}";
        }

        private void CopyDirectory(string sourceDir, string destDir)
        {
            if (!Directory.Exists(sourceDir))
                return;

            Directory.CreateDirectory(destDir);

            foreach (string file in Directory.GetFiles(sourceDir))
            {
                string destFile = Path.Combine(destDir, Path.GetFileName(file));
                File.Copy(file, destFile, true);
            }

            foreach (string subDir in Directory.GetDirectories(sourceDir))
            {
                string destSubDir = Path.Combine(destDir, Path.GetFileName(subDir));
                CopyDirectory(subDir, destSubDir);
            }
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            NavigationManager.GoBack();
        }
    }

    #region "كلاسات مساعدة"

    public class BackupFileInfo
    {
        public string FullPath { get; set; }
        public string FileName { get; set; }
        public DateTime CreatedDate { get; set; }
        public long Size { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }

        public string SizeFormatted
        {
            get
            {
                string[] sizes = { "B", "KB", "MB", "GB" };
                int order = 0;
                long size = Size;
                while (size >= 1024 && order < sizes.Length - 1)
                {
                    order++;
                    size = size / 1024;
                }
                return $"{size:0.##} {sizes[order]}";
            }
        }

        public string CreatedDateFormatted => CreatedDate.ToString("yyyy/MM/dd HH:mm");
    }

    #endregion
}