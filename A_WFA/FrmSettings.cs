using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace A_WFA
{
    public partial class FrmSettings : Form
    {
        public FrmSettings()
        {
            InitializeComponent();

            this.Load += FrmSettings_Load;
            BtnReinitialize.Click += BtnReinitialize_Click;
            BtnCreateTables.Click += BtnCreateTables_Click;
            BtnSeedData.Click += BtnSeedData_Click;
            BtnCreateBackup.Click += BtnCreateBackup_Click;
            BtnRestoreBackup.Click += BtnRestoreBackup_Click;
            BtnDeleteBackups.Click += BtnDeleteBackups_Click;
            BtnRefreshTables.Click += BtnRefreshTables_Click;
        }

        #region "تحميل النموذج"

        private void FrmSettings_Load(object sender, EventArgs e)
        {
            LoadDatabaseStatus();
            LoadBackupFiles();
            LoadTables();
        }

        #endregion

        #region "تبويب قاعدة البيانات"

        private void LoadDatabaseStatus()
        {
            try
            {
                bool exists = DatabaseManagerLite.DatabaseExists();
                bool schemaExists = DatabaseManagerLite.SchemaExists();

                if (exists && schemaExists)
                {
                    LblStatus.Text = "✅ الحالة: قاعدة البيانات موجودة وجاهزة للعمل";
                    LblStatus.ForeColor = Color.Green;

                    // عدد الجداول
                    string query = "SELECT COUNT(*) FROM sys.tables";
                    int count = Convert.ToInt32(DatabaseManagerLite.ExecuteScalar(query));
                    LblTablesCount.Text = $"📊 عدد الجداول: {count}";

                    // تفعيل أزرار الحذف والإعادة
                    BtnReinitialize.Enabled = true;
                    BtnCreateTables.Enabled = true;
                }
                else if (exists)
                {
                    LblStatus.Text = "⚠️ الحالة: قاعدة البيانات موجودة ولكن الجداول مفقودة";
                    LblStatus.ForeColor = Color.Orange;
                    LblTablesCount.Text = "📊 عدد الجداول: 0";
                    BtnReinitialize.Enabled = true;
                    BtnCreateTables.Enabled = true;
                }
                else
                {
                    LblStatus.Text = "❌ الحالة: قاعدة البيانات غير موجودة";
                    LblStatus.ForeColor = Color.Red;
                    LblTablesCount.Text = "📊 عدد الجداول: 0";
                    BtnReinitialize.Enabled = true;
                    BtnCreateTables.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                LblStatus.Text = $"❌ خطأ: {ex.Message}";
                LblStatus.ForeColor = Color.Red;
                BtnReinitialize.Enabled = true;
                BtnCreateTables.Enabled = true;
            }
        }

        private void BtnReinitialize_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "⚠️ تحذير! إعادة تهيئة قاعدة البيانات ستؤدي إلى:\n" +
                "• حذف جميع البيانات الحالية\n" +
                "• حذف جميع الجداول\n" +
                "• إنشاء جداول جديدة فارغة\n\n" +
                "هل أنت متأكد من المتابعة؟",
                "تأكيد إعادة التهيئة",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;
                    LblStatus.Text = "⏳ جاري إعادة تهيئة قاعدة البيانات...";

                    // حذف وإنشاء قاعدة البيانات
                    bool success = DatabaseManagerLite.CreateDatabaseSchema();

                    if (success)
                    {
                        MessageBox.Show(
                            "✅ تم إعادة تهيئة قاعدة البيانات بنجاح!",
                            "نجاح",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        LoadDatabaseStatus();
                        LoadTables();
                    }
                    else
                    {
                        MessageBox.Show(
                            "❌ فشل في إعادة تهيئة قاعدة البيانات",
                            "خطأ",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        $"❌ خطأ: {ex.Message}",
                        "خطأ",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                finally
                {
                    Cursor = Cursors.Default;
                }
            }
        }

        private void BtnCreateTables_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                LblStatus.Text = "⏳ جاري إنشاء الجداول...";

                bool success = DatabaseManagerLite.CreateDatabaseSchema();

                if (success)
                {
                    MessageBox.Show(
                        "✅ تم إنشاء الجداول بنجاح!",
                        "نجاح",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    LoadDatabaseStatus();
                    LoadTables();
                }
                else
                {
                    MessageBox.Show(
                        "❌ فشل في إنشاء الجداول",
                        "خطأ",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"❌ خطأ: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void BtnSeedData_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                int inserted = DatabaseSeeder.SeedAll(
                    seedDocuments: true,
                    seedSoldiers: true,
                    count: 10);

                MessageBox.Show(
                    $"✅ تم إدراج {inserted} سجل بنجاح!",
                    "نجاح",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                LoadDatabaseStatus();
                LoadTables();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"❌ خطأ: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        #endregion

        #region "تبويب النسخ الاحتياطي"

        private string GetBackupFolder()
        {
            string folder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "ArchiveDB_Backups");

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            return folder;
        }

        private void LoadBackupFiles()
        {
            try
            {
                LstBackupFiles.Items.Clear();

                string backupFolder = GetBackupFolder();
                var files = Directory.GetFiles(backupFolder, "*.bak")
                    .OrderByDescending(f => File.GetCreationTime(f))
                    .ToList();

                foreach (string file in files)
                {
                    string fileName = Path.GetFileName(file);
                    DateTime creationTime = File.GetCreationTime(file);
                    string fileSize = GetFileSize(file);
                    LstBackupFiles.Items.Add($"{fileName} ({creationTime:yyyy-MM-dd HH:mm}) - {fileSize}");
                }

                if (LstBackupFiles.Items.Count == 0)
                {
                    LstBackupFiles.Items.Add("📭 لا توجد نسخ احتياطية");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"خطأ في تحميل ملفات النسخ الاحتياطي: {ex.Message}");
            }
        }

        private string GetFileSize(string filePath)
        {
            long size = new FileInfo(filePath).Length;
            string[] sizes = { "B", "KB", "MB", "GB" };
            int order = 0;
            while (size >= 1024 && order < sizes.Length - 1)
            {
                order++;
                size = size / 1024;
            }
            return $"{size:0.##} {sizes[order]}";
        }

        private void BtnCreateBackup_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                string backupFolder = GetBackupFolder();
                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string backupFile = Path.Combine(backupFolder, $"ArchiveDB_{timestamp}.bak");

                string connectionString = DatabaseManagerLite.GetConnectionString();
                var builder = new System.Data.SqlClient.SqlConnectionStringBuilder(connectionString);
                string databaseName = builder.InitialCatalog;

                string query = $@"
                    BACKUP DATABASE [{databaseName}]
                    TO DISK = '{backupFile}'
                    WITH FORMAT, MEDIANAME = 'ArchiveDB_Backup', NAME = 'Full Backup of {databaseName}'";

                DatabaseManagerLite.ExecuteNonQuery(query);

                MessageBox.Show(
                    $"✅ تم إنشاء النسخة الاحتياطية بنجاح!\n\n📁 الملف: {backupFile}",
                    "نجاح",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                LoadBackupFiles();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"❌ خطأ في إنشاء النسخة الاحتياطية:\n{ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void BtnRestoreBackup_Click(object sender, EventArgs e)
        {
            if (LstBackupFiles.SelectedIndex == -1 || LstBackupFiles.Items.Count == 0)
            {
                MessageBox.Show(
                    "⚠️ الرجاء اختيار ملف النسخ الاحتياطي من القائمة",
                    "تنبيه",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            string selectedItem = LstBackupFiles.SelectedItem.ToString();
            string fileName = selectedItem.Split('(')[0].Trim();
            string backupPath = Path.Combine(GetBackupFolder(), fileName);

            if (!File.Exists(backupPath))
            {
                MessageBox.Show(
                    "❌ الملف غير موجود!",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                LoadBackupFiles();
                return;
            }

            DialogResult result = MessageBox.Show(
                $"⚠️ تحذير! استعادة النسخة الاحتياطية ستؤدي إلى:\n" +
                $"• استبدال قاعدة البيانات الحالية بالكامل\n" +
                $"• فقدان أي تغييرات غير محفوظة\n\n" +
                $"الملف: {fileName}\n" +
                $"هل أنت متأكد من المتابعة؟",
                "تأكيد الاستعادة",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;

                    string connectionString = DatabaseManagerLite.GetConnectionString();
                    var builder = new System.Data.SqlClient.SqlConnectionStringBuilder(connectionString);
                    string databaseName = builder.InitialCatalog;

                    // إغلاق جميع الاتصالات المفتوحة
                    string closeConnections = $@"
                        ALTER DATABASE [{databaseName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE";

                    try
                    {
                        DatabaseManagerLite.ExecuteNonQuery(closeConnections);
                    }
                    catch { }

                    // استعادة قاعدة البيانات
                    string restoreQuery = $@"
                        RESTORE DATABASE [{databaseName}]
                        FROM DISK = '{backupPath}'
                        WITH REPLACE, RESTART";

                    DatabaseManagerLite.ExecuteNonQuery(restoreQuery);

                    // إعادة فتح الاتصالات
                    string openConnections = $@"
                        ALTER DATABASE [{databaseName}] SET MULTI_USER";

                    try
                    {
                        DatabaseManagerLite.ExecuteNonQuery(openConnections);
                    }
                    catch { }

                    MessageBox.Show(
                        $"✅ تم استعادة قاعدة البيانات بنجاح!\n\n📁 الملف: {fileName}",
                        "نجاح",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    LoadDatabaseStatus();
                    LoadTables();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        $"❌ خطأ في استعادة النسخة الاحتياطية:\n{ex.Message}",
                        "خطأ",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                finally
                {
                    Cursor = Cursors.Default;
                }
            }
        }

        private void BtnDeleteBackups_Click(object sender, EventArgs e)
        {
            if (LstBackupFiles.SelectedIndex == -1 || LstBackupFiles.Items.Count == 0)
            {
                MessageBox.Show(
                    "⚠️ الرجاء اختيار ملف النسخ الاحتياطي من القائمة",
                    "تنبيه",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                "⚠️ هل أنت متأكد من حذف ملف النسخ الاحتياطي هذا؟",
                "تأكيد الحذف",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    string selectedItem = LstBackupFiles.SelectedItem.ToString();
                    string fileName = selectedItem.Split('(')[0].Trim();
                    string backupPath = Path.Combine(GetBackupFolder(), fileName);

                    if (File.Exists(backupPath))
                    {
                        File.Delete(backupPath);
                        MessageBox.Show(
                            "✅ تم حذف الملف بنجاح",
                            "نجاح",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        LoadBackupFiles();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        $"❌ خطأ في حذف الملف:\n{ex.Message}",
                        "خطأ",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        #endregion

        #region "تبويب الجداول"

        private void LoadTables()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("الجدول", typeof(string));
                dt.Columns.Add("الأعمدة", typeof(string));

                string query = @"
                    SELECT 
                        t.name AS TableName,
                        c.name AS ColumnName,
                        ty.name AS DataType,
                        c.max_length AS MaxLength,
                        c.is_nullable AS IsNullable
                    FROM sys.tables t
                    INNER JOIN sys.columns c ON t.object_id = c.object_id
                    INNER JOIN sys.types ty ON c.user_type_id = ty.user_type_id
                    ORDER BY t.name, c.column_id";

                DataTable result = DatabaseManagerLite.ExecuteQuery(query);

                var tableGroups = result.AsEnumerable()
                    .GroupBy(r => r["TableName"].ToString());

                foreach (var group in tableGroups)
                {
                    string columns = string.Join(", ",
                        group.Select(r =>
                        {
                            string colName = r["ColumnName"].ToString();
                            string dataType = r["DataType"].ToString();
                            string nullable = Convert.ToBoolean(r["IsNullable"]) ? "NULL" : "NOT NULL";
                            return $"{colName} ({dataType}, {nullable})";
                        }));

                    dt.Rows.Add(group.Key, columns);
                }

                DgvTables.DataSource = dt;
                DgvTables.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"❌ خطأ في تحميل الجداول:\n{ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void BtnRefreshTables_Click(object sender, EventArgs e)
        {
            LoadTables();
            LoadDatabaseStatus();
        }

        #endregion
    }
}