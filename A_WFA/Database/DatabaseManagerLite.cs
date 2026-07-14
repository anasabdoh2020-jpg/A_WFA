using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace A_WFA
{
    /// <summary>
    /// مدير قاعدة البيانات - مسؤول عن الاتصال وإنشاء الهيكل والعمليات الأساسية
    /// </summary>
    public static class DatabaseManagerLite
    {
        #region "سلسلة الاتصال"

        private static string _connectionString;
        private static readonly object _lock = new object();

        /// <summary>
        /// الحصول على سلسلة الاتصال من App.config
        /// </summary>
        public static string GetConnectionString()
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                lock (_lock)
                {
                    if (string.IsNullOrEmpty(_connectionString))
                    {
                        try
                        {
                            var connectionStringSettings = ConfigurationManager
                                .ConnectionStrings["ArchiveDB"];

                            if (connectionStringSettings != null)
                            {
                                _connectionString = connectionStringSettings.ConnectionString;

                                if (_connectionString.Contains("|DataDirectory|"))
                                {
                                    string dataDirectory = AppDomain.CurrentDomain.GetData("DataDirectory")?.ToString();
                                    if (string.IsNullOrEmpty(dataDirectory))
                                    {
                                        dataDirectory = AppDomain.CurrentDomain.BaseDirectory;
                                    }
                                    _connectionString = _connectionString.Replace("|DataDirectory|", dataDirectory);
                                }

                                Debug.WriteLine($"✅ تم قراءة سلسلة الاتصال من App.config: {_connectionString}");
                                return _connectionString;
                            }
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine($"⚠️ فشل قراءة سلسلة الاتصال: {ex.Message}");
                        }

                        string defaultPath = Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                            "A_WFA",
                            "Archive.db"
                        );

                        string dbDir = Path.GetDirectoryName(defaultPath);
                        if (!Directory.Exists(dbDir))
                            Directory.CreateDirectory(dbDir);

                        _connectionString = $"Data Source={defaultPath};Version=3;";
                        Debug.WriteLine($"⚠️ تم استخدام المسار الافتراضي: {_connectionString}");
                    }
                }
            }

            return _connectionString;
        }

        public static SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(GetConnectionString());
        }

        #endregion

        #region "الدوال الأساسية للاستعلامات"

        /// <summary>
        /// تنفيذ استعلام SQL (INSERT, UPDATE, DELETE)
        /// </summary>
        public static int ExecuteNonQuery(string sql, Dictionary<string, object> parameters = null)
        {
            using (SQLiteConnection connection = GetConnection())
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                        }
                    }
                    return command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// تنفيذ استعلام SQL وإرجاع DataTable
        /// </summary>
        public static DataTable ExecuteQuery(string sql, Dictionary<string, object> parameters = null)
        {
            using (SQLiteConnection connection = GetConnection())
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                        }
                    }

                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        /// <summary>
        /// تنفيذ استعلام SQL وإرجاع قيمة واحدة
        /// </summary>
        public static object ExecuteScalar(string sql, Dictionary<string, object> parameters = null)
        {
            using (SQLiteConnection connection = GetConnection())
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                        }
                    }
                    return command.ExecuteScalar();
                }
            }
        }

        /// <summary>
        /// تنفيذ استعلام داخلي (مع اتصال ومعاملة)
        /// </summary>
        private static int ExecuteNonQueryInternal(string sql, SQLiteConnection connection, SQLiteTransaction transaction)
        {
            using (SQLiteCommand command = new SQLiteCommand(sql, connection, transaction))
            {
                return command.ExecuteNonQuery();
            }
        }

        #endregion

        #region "التحقق من وجود قاعدة البيانات"

        /// <summary>
        /// الحصول على مسار ملف قاعدة البيانات
        /// </summary>
        public static string GetDatabaseFilePath()
        {
            string connString = GetConnectionString();
            string dbPath = connString
                .Replace("Data Source=", "")
                .Replace(";Version=3;", "")
                .Replace(";", "")
                .Trim();

            if (dbPath.Contains("|DataDirectory|"))
            {
                string dataDirectory = AppDomain.CurrentDomain.GetData("DataDirectory")?.ToString()
                                       ?? AppDomain.CurrentDomain.BaseDirectory;
                dbPath = dbPath.Replace("|DataDirectory|", dataDirectory);
            }

            return dbPath;
        }

        /// <summary>
        /// التحقق من وجود قاعدة البيانات (الملف الفعلي)
        /// </summary>
        public static bool DatabaseExists()
        {
            try
            {
                string dbPath = GetDatabaseFilePath();
                return File.Exists(dbPath);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// التحقق من وجود الجداول
        /// </summary>
        public static bool SchemaExists()
        {
            try
            {
                if (!DatabaseExists())
                    return false;

                string[] tables = {
                    "Boxes", "Document_Types", "Document_Categories",
                    "Departments", "Soldiers", "Users", "Documents",
                    "DocumentSoldiers", "AuditTrail", "SystemSettings"
                };

                string query = @"
                    SELECT COUNT(*) FROM sqlite_master 
                    WHERE type='table' AND name IN ({0})";

                string tableNames = string.Join(",", tables.Select(t => $"'{t}'"));
                query = string.Format(query, tableNames);

                int count = Convert.ToInt32(ExecuteScalar(query));
                return count == tables.Length;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region "تهيئة قاعدة البيانات"

        /// <summary>
        /// تهيئة قاعدة البيانات - يتم استدعاؤها عند أول تشغيل
        /// </summary>
        public static bool InitializeDatabase()
        {
            try
            {
                Debug.WriteLine("🚀 بدء تهيئة قاعدة البيانات...");

                string dbPath = GetDatabaseFilePath();
                string dbDir = Path.GetDirectoryName(dbPath);
                if (!Directory.Exists(dbDir))
                {
                    Directory.CreateDirectory(dbDir);
                    Debug.WriteLine($"✅ تم إنشاء مجلد قاعدة البيانات: {dbDir}");
                }

                if (!DatabaseExists())
                {
                    Debug.WriteLine("⚠️ قاعدة البيانات غير موجودة، جاري الإنشاء...");
                    bool success = CreateDatabaseSchema();

                    if (success)
                    {
                        Debug.WriteLine("✅ تم إنشاء قاعدة البيانات والجداول بنجاح");
                        try
                        {
                            DatabaseSeeder.SeedAll(seedDocuments: false, seedSoldiers: false);
                            Debug.WriteLine("✅ تم إدراج البيانات الافتراضية");
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine($"⚠️ تحذير: فشل في إدراج البيانات الافتراضية: {ex.Message}");
                        }
                        return true;
                    }
                    return false;
                }
                else if (!SchemaExists())
                {
                    Debug.WriteLine("⚠️ قاعدة البيانات موجودة ولكن الجداول مفقودة، جاري الإنشاء...");
                    bool success = CreateDatabaseSchema();

                    if (success)
                    {
                        Debug.WriteLine("✅ تم إنشاء الجداول بنجاح");
                        try
                        {
                            DatabaseSeeder.SeedAll(seedDocuments: false, seedSoldiers: false);
                            Debug.WriteLine("✅ تم إدراج البيانات الافتراضية");
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine($"⚠️ تحذير: فشل في إدراج البيانات الافتراضية: {ex.Message}");
                        }
                        return true;
                    }
                    return false;
                }

                Debug.WriteLine("✅ قاعدة البيانات جاهزة");
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"❌ فشل في تهيئة قاعدة البيانات: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// إنشاء هيكل قاعدة البيانات
        /// </summary>
        public static bool CreateDatabaseSchema()
        {
            SQLiteConnection connection = null;
            SQLiteTransaction transaction = null;

            try
            {
                using (connection = GetConnection())
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();

                    CreateTableBoxes(connection, transaction);
                    CreateTableDocumentTypes(connection, transaction);
                    CreateTableDocumentCategories(connection, transaction);
                    CreateTableDepartments(connection, transaction);
                    CreateTableSoldiers(connection, transaction);
                    CreateTableUsers(connection, transaction);
                    CreateTableDocuments(connection, transaction);
                    CreateTableDocumentSoldiers(connection, transaction);
                    CreateTableAuditTrail(connection, transaction);
                    CreateTableSystemSettings(connection, transaction);

                    CreateIndexes(connection, transaction);
                    InsertDefaultSettings(connection, transaction);

                    transaction.Commit();
                    Debug.WriteLine("✅ تم إنشاء هيكل قاعدة البيانات بنجاح");
                    return true;
                }
            }
            catch (Exception ex)
            {
                try
                {
                    if (transaction != null && transaction.Connection != null)
                    {
                        transaction.Rollback();
                    }
                }
                catch (Exception rollbackEx)
                {
                    Debug.WriteLine($"⚠️ خطأ في Rollback: {rollbackEx.Message}");
                }

                Debug.WriteLine($"❌ خطأ في إنشاء قاعدة البيانات: {ex.Message}");
                return false;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        #endregion

        #region "إنشاء الجداول"

        private static void CreateTableBoxes(SQLiteConnection connection, SQLiteTransaction transaction)
        {
            string sql = @"
                CREATE TABLE IF NOT EXISTS Boxes (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    name TEXT NOT NULL,
                    image_path TEXT,
                    start_date TEXT,
                    details TEXT,
                    archiveBox_number TEXT,
                    is_active INTEGER DEFAULT 1,
                    created_at TEXT DEFAULT CURRENT_TIMESTAMP,
                    updated_at TEXT DEFAULT CURRENT_TIMESTAMP
                )";
            ExecuteNonQueryInternal(sql, connection, transaction);
            Debug.WriteLine("✅ تم إنشاء جدول Boxes");
        }

        private static void CreateTableDocumentTypes(SQLiteConnection connection, SQLiteTransaction transaction)
        {
            string sql = @"
                CREATE TABLE IF NOT EXISTS Document_Types (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    name TEXT NOT NULL,
                    description TEXT,
                    is_active INTEGER DEFAULT 1,
                    created_at TEXT DEFAULT CURRENT_TIMESTAMP
                )";
            ExecuteNonQueryInternal(sql, connection, transaction);
            Debug.WriteLine("✅ تم إنشاء جدول Document_Types");
        }

        private static void CreateTableDocumentCategories(SQLiteConnection connection, SQLiteTransaction transaction)
        {
            string sql = @"
                CREATE TABLE IF NOT EXISTS Document_Categories (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    name TEXT NOT NULL,
                    description TEXT,
                    is_active INTEGER DEFAULT 1,
                    created_at TEXT DEFAULT CURRENT_TIMESTAMP
                )";
            ExecuteNonQueryInternal(sql, connection, transaction);
            Debug.WriteLine("✅ تم إنشاء جدول Document_Categories");
        }

        private static void CreateTableDepartments(SQLiteConnection connection, SQLiteTransaction transaction)
        {
            string sql = @"
                CREATE TABLE IF NOT EXISTS Departments (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    name TEXT NOT NULL,
                    description TEXT,
                    is_active INTEGER DEFAULT 1,
                    created_at TEXT DEFAULT CURRENT_TIMESTAMP
                )";
            ExecuteNonQueryInternal(sql, connection, transaction);
            Debug.WriteLine("✅ تم إنشاء جدول Departments");
        }

        private static void CreateTableSoldiers(SQLiteConnection connection, SQLiteTransaction transaction)
        {
            string sql = @"
                CREATE TABLE IF NOT EXISTS Soldiers (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    name TEXT NOT NULL,
                    unite TEXT,
                    national_id TEXT,
                    phone TEXT,
                    address TEXT,
                    is_active INTEGER DEFAULT 1,
                    created_at TEXT DEFAULT CURRENT_TIMESTAMP,
                    updated_at TEXT DEFAULT CURRENT_TIMESTAMP
                )";
            ExecuteNonQueryInternal(sql, connection, transaction);
            Debug.WriteLine("✅ تم إنشاء جدول Soldiers");
        }

        private static void CreateTableUsers(SQLiteConnection connection, SQLiteTransaction transaction)
        {
            string sql = @"
                CREATE TABLE IF NOT EXISTS Users (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    username TEXT NOT NULL UNIQUE,
                    password_hash TEXT NOT NULL,
                    full_name TEXT,
                    role TEXT DEFAULT 'User',
                    is_active INTEGER DEFAULT 1,
                    created_at TEXT DEFAULT CURRENT_TIMESTAMP,
                    last_login TEXT
                )";
            ExecuteNonQueryInternal(sql, connection, transaction);
            Debug.WriteLine("✅ تم إنشاء جدول Users");
        }

        private static void CreateTableDocuments(SQLiteConnection connection, SQLiteTransaction transaction)
        {
            string sql = @"
                CREATE TABLE IF NOT EXISTS Documents (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    title TEXT NOT NULL,
                    document_type_id INTEGER,
                    category_id INTEGER,
                    from_department_id INTEGER,
                    to_department_id INTEGER,
                    box_id INTEGER,
                    document_date TEXT,
                    receive_date TEXT,
                    issue_date TEXT,
                    uploaded_by INTEGER,
                    uploaded_at TEXT DEFAULT CURRENT_TIMESTAMP,
                    updated_at TEXT DEFAULT CURRENT_TIMESTAMP,
                    status TEXT,
                    priority TEXT,
                    document_nature TEXT, 
                    summary TEXT,
                    notes TEXT,
                    archiveDoc_number TEXT,
                    ReferenceNumber TEXT,
                    file_path TEXT,
                    file_name TEXT,
                    file_type TEXT,
                    file_size INTEGER DEFAULT 0,
                    file_hash TEXT,
                    is_active INTEGER DEFAULT 1,
                    FOREIGN KEY (document_type_id) REFERENCES Document_Types(id) ON DELETE SET NULL,
                    FOREIGN KEY (category_id) REFERENCES Document_Categories(id) ON DELETE SET NULL,
                    FOREIGN KEY (from_department_id) REFERENCES Departments(id) ON DELETE NO ACTION,
                    FOREIGN KEY (to_department_id) REFERENCES Departments(id) ON DELETE NO ACTION,
                    FOREIGN KEY (box_id) REFERENCES Boxes(id) ON DELETE SET NULL,
                    FOREIGN KEY (uploaded_by) REFERENCES Users(id) ON DELETE SET NULL
                )";
            ExecuteNonQueryInternal(sql, connection, transaction);
            Debug.WriteLine("✅ تم إنشاء جدول Documents");
        }

        private static void CreateTableDocumentSoldiers(SQLiteConnection connection, SQLiteTransaction transaction)
        {
            string sql = @"
                CREATE TABLE IF NOT EXISTS DocumentSoldiers (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    DocumentId INTEGER NOT NULL,
                    SoldierId INTEGER NOT NULL,
                    RelationshipType TEXT,
                    RelationDate TEXT,
                    RelationMonth TEXT,
                    Notes TEXT,
                    created_at TEXT DEFAULT CURRENT_TIMESTAMP,
                    FOREIGN KEY (DocumentId) REFERENCES Documents(id) ON DELETE CASCADE,
                    FOREIGN KEY (SoldierId) REFERENCES Soldiers(id) ON DELETE CASCADE
                )";
            ExecuteNonQueryInternal(sql, connection, transaction);
            Debug.WriteLine("✅ تم إنشاء جدول DocumentSoldiers");
        }

        private static void CreateTableAuditTrail(SQLiteConnection connection, SQLiteTransaction transaction)
        {
            string sql = @"
                CREATE TABLE IF NOT EXISTS AuditTrail (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    user_id INTEGER,
                    action TEXT NOT NULL,
                    description TEXT,
                    ip_address TEXT,
                    created_at TEXT DEFAULT CURRENT_TIMESTAMP,
                    FOREIGN KEY (user_id) REFERENCES Users(id) ON DELETE SET NULL
                )";
            ExecuteNonQueryInternal(sql, connection, transaction);
            Debug.WriteLine("✅ تم إنشاء جدول AuditTrail");
        }

        private static void CreateTableSystemSettings(SQLiteConnection connection, SQLiteTransaction transaction)
        {
            string sql = @"
                CREATE TABLE IF NOT EXISTS SystemSettings (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    setting_key TEXT NOT NULL UNIQUE,
                    setting_value TEXT,
                    setting_type TEXT DEFAULT 'string',
                    description TEXT,
                    updated_at TEXT DEFAULT CURRENT_TIMESTAMP
                )";
            ExecuteNonQueryInternal(sql, connection, transaction);
            Debug.WriteLine("✅ تم إنشاء جدول SystemSettings");
        }

        #endregion

        #region "إنشاء الفهارس"

        private static void CreateIndexes(SQLiteConnection connection, SQLiteTransaction transaction)
        {
            string[] indexQueries = {
                "CREATE INDEX IF NOT EXISTS IX_Boxes_Name ON Boxes(name);",
                "CREATE INDEX IF NOT EXISTS IX_Boxes_IsActive ON Boxes(is_active);",
                "CREATE INDEX IF NOT EXISTS IX_Boxes_ArchiveNumber ON Boxes(archiveBox_number);",
                "CREATE INDEX IF NOT EXISTS IX_Documents_BoxId ON Documents(box_id);",
                "CREATE INDEX IF NOT EXISTS IX_Documents_Status ON Documents(status);",
                "CREATE INDEX IF NOT EXISTS IX_Documents_DocumentDate ON Documents(document_date);",
                "CREATE INDEX IF NOT EXISTS IX_Documents_DocumentTypeId ON Documents(document_type_id);",
                "CREATE INDEX IF NOT EXISTS IX_Documents_CategoryId ON Documents(category_id);",
                "CREATE INDEX IF NOT EXISTS IX_Documents_ArchiveDoc ON Documents(archiveDoc_number);",
                "CREATE INDEX IF NOT EXISTS IX_Documents_ReferenceNumber ON Documents(ReferenceNumber);",
                "CREATE INDEX IF NOT EXISTS IX_Documents_FilePath ON Documents(file_path);",
                "CREATE INDEX IF NOT EXISTS IX_DocumentSoldiers_DocumentId ON DocumentSoldiers(DocumentId);",
                "CREATE INDEX IF NOT EXISTS IX_DocumentSoldiers_SoldierId ON DocumentSoldiers(SoldierId);",
                "CREATE INDEX IF NOT EXISTS IX_Soldiers_Name ON Soldiers(name);",
                "CREATE INDEX IF NOT EXISTS IX_Departments_Name ON Departments(name);",
                "CREATE INDEX IF NOT EXISTS IX_AuditTrail_CreatedAt ON AuditTrail(created_at);"
            };

            foreach (string query in indexQueries)
            {
                try
                {
                    ExecuteNonQueryInternal(query, connection, transaction);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"⚠️ تحذير: فشل في إنشاء فهرس: {ex.Message}");
                }
            }

            Debug.WriteLine("✅ تم إنشاء الفهارس");
        }

        #endregion

        #region "الإعدادات الأساسية"

        private static void InsertDefaultSettings(SQLiteConnection connection, SQLiteTransaction transaction)
        {
            try
            {
                var settings = new Dictionary<string, (string value, string type, string desc)>
                {
                    { "AppName", ("نظام أرشفة الوثائق", "string", "اسم التطبيق") },
                    { "Version", ("2.0.0", "string", "إصدار التطبيق") },
                    { "DefaultValidityMonths", ("6", "int", "مدة صلاحية المفتاح الافتراضية") },
                    { "MaxFileSize", ("52428800", "int", "الحد الأقصى لحجم الملف (50 MB)") },
                    { "EnableAuditLog", ("true", "bool", "تفعيل سجل التدقيق") },
                    { "AutoBackup", ("true", "bool", "تفعيل النسخ الاحتياطي التلقائي") },
                    { "BackupInterval", ("24", "int", "فترة النسخ الاحتياطي بالساعات") },
                    { "StoragePath", (@"C:\ArchiveSystem\Files\", "string", "مسار تخزين الملفات") }
                };

                foreach (var setting in settings)
                {
                    string sql = @"
                        INSERT OR IGNORE INTO SystemSettings (setting_key, setting_value, setting_type, description) 
                        VALUES (@key, @value, @type, @desc)";
                    using (var cmd = new SQLiteCommand(sql, connection, transaction))
                    {
                        cmd.Parameters.AddWithValue("@key", setting.Key);
                        cmd.Parameters.AddWithValue("@value", setting.Value.value);
                        cmd.Parameters.AddWithValue("@type", setting.Value.type);
                        cmd.Parameters.AddWithValue("@desc", setting.Value.desc);
                        cmd.ExecuteNonQuery();
                    }
                }
                Debug.WriteLine("✅ تم إدراج الإعدادات الأساسية");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"⚠️ تحذير: فشل في إدراج الإعدادات الأساسية: {ex.Message}");
            }
        }

        #endregion

        #region "دوال الصناديق - Boxes CRUD"

        /// <summary>
        /// الحصول على صندوق بواسطة المعرف
        /// </summary>
        public static DataRow GetBoxById(int boxId)
        {
            string query = "SELECT * FROM Boxes WHERE id = @id";
            var parameters = new Dictionary<string, object> { { "@id", boxId } };
            DataTable dt = ExecuteQuery(query, parameters);
            return dt.Rows.Count > 0 ? dt.Rows[0] : null;
        }

        /// <summary>
        /// إضافة صندوق جديد
        /// </summary>
        public static int AddBox(string name, string imagePath, string startDate, string details, bool isActive)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                    throw new ArgumentException("اسم الصندوق مطلوب");

                string archiveNumber = GenerateArchiveBoxNumber();

                string query = @"
                    INSERT INTO Boxes (
                        name, image_path, start_date, details, archiveBox_number,
                        is_active, created_at, updated_at
                    ) VALUES (
                        @name, @imagePath, @startDate, @details, @archiveNumber,
                        @isActive, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
                    );
                    SELECT last_insert_rowid();";

                var parameters = new Dictionary<string, object>
                {
                    { "@name", name },
                    { "@imagePath", string.IsNullOrEmpty(imagePath) ? DBNull.Value : (object)imagePath },
                    { "@startDate", string.IsNullOrEmpty(startDate) ? DBNull.Value : (object)startDate },
                    { "@details", string.IsNullOrEmpty(details) ? DBNull.Value : (object)details },
                    { "@archiveNumber", archiveNumber },
                    { "@isActive", isActive ? 1 : 0 }
                };

                object result = ExecuteScalar(query, parameters);
                return result != null ? Convert.ToInt32(result) : 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"❌ خطأ في AddBox: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// تحديث صندوق
        /// </summary>
        public static bool UpdateBox(int id, string name, string imagePath, string startDate, string details, bool isActive)
        {
            string query = @"
                UPDATE Boxes 
                SET name = @name, 
                    image_path = @imagePath, 
                    start_date = @startDate, 
                    details = @details, 
                    is_active = @isActive,
                    updated_at = CURRENT_TIMESTAMP
                WHERE id = @id";

            var parameters = new Dictionary<string, object>
            {
                { "@id", id },
                { "@name", name },
                { "@imagePath", string.IsNullOrEmpty(imagePath) ? DBNull.Value : (object)imagePath },
                { "@startDate", string.IsNullOrEmpty(startDate) ? DBNull.Value : (object)startDate },
                { "@details", string.IsNullOrEmpty(details) ? DBNull.Value : (object)details },
                { "@isActive", isActive ? 1 : 0 }
            };

            int result = ExecuteNonQuery(query, parameters);
            return result > 0;
        }

        /// <summary>
        /// حذف صندوق
        /// </summary>
        public static bool DeleteBox(int id)
        {
            var parameters = new Dictionary<string, object> { { "@id", id } };

            // حذف الوثائق المرتبطة
            ExecuteNonQuery("DELETE FROM Documents WHERE box_id = @id", parameters);

            // حذف الصندوق
            int result = ExecuteNonQuery("DELETE FROM Boxes WHERE id = @id", parameters);
            return result > 0;
        }

        /// <summary>
        /// توليد رقم أرشيفي للصندوق
        /// </summary>
        private static string GenerateArchiveBoxNumber()
        {
            try
            {
                string query = @"
                    SELECT archiveBox_number
                    FROM Boxes
                    WHERE archiveBox_number IS NOT NULL
                    AND archiveBox_number LIKE 'ARCH-%'
                    ORDER BY id DESC
                    LIMIT 1";

                object result = ExecuteScalar(query);

                int number = 1;
                if (result != null && result != DBNull.Value)
                {
                    string lastNumber = result.ToString();
                    if (lastNumber.StartsWith("ARCH-") && lastNumber.Length > 5)
                    {
                        string numPart = lastNumber.Substring(5);
                        if (int.TryParse(numPart, out int parsed))
                        {
                            number = parsed + 1;
                        }
                    }
                }

                return $"ARCH-{number:000}";
            }
            catch
            {
                return "ARCH-001";
            }
        }

        /// <summary>
        /// الحصول على جميع الصناديق
        /// </summary>
        public static DataTable GetAllBoxes(bool activeOnly = false)
        {
            string query = "SELECT id, name, image_path, start_date, details, archiveBox_number, is_active FROM Boxes";
            if (activeOnly)
                query += " WHERE is_active = 1";
            query += " ORDER BY name";
            return ExecuteQuery(query);
        }

        #endregion

        #region "دوال الإحصائيات"

        public static (int TotalBoxes, int ActiveBoxes, int TotalDocuments, int ActiveDocuments) GetStatistics()
        {
            try
            {
                int totalBoxes = Convert.ToInt32(ExecuteScalar("SELECT COUNT(*) FROM Boxes"));
                int activeBoxes = Convert.ToInt32(ExecuteScalar("SELECT COUNT(*) FROM Boxes WHERE is_active = 1"));
                int totalDocuments = Convert.ToInt32(ExecuteScalar("SELECT COUNT(*) FROM Documents WHERE is_active = 1"));
                int activeDocuments = Convert.ToInt32(ExecuteScalar(
                    "SELECT COUNT(*) FROM Documents d INNER JOIN Boxes b ON d.box_id = b.id WHERE b.is_active = 1 AND d.is_active = 1"));

                return (totalBoxes, activeBoxes, totalDocuments, activeDocuments);
            }
            catch
            {
                return (0, 0, 0, 0);
            }
        }

        #endregion

        #region "دوال التدقيق"

        public static void SafeLogAuditTrail(int userId, string action, string description)
        {
            try
            {
                string sql = @"
                    INSERT INTO AuditTrail (user_id, action, description, created_at) 
                    VALUES (@userId, @action, @description, CURRENT_TIMESTAMP)";

                var parameters = new Dictionary<string, object>
                {
                    { "@userId", userId },
                    { "@action", action },
                    { "@description", description ?? string.Empty }
                };

                ExecuteNonQuery(sql, parameters);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"⚠️ خطأ في تسجيل التدقيق: {ex.Message}");
            }
        }

        #endregion

        #region "دوال مساعدة"

        /// <summary>
        /// الحصول على مسار تخزين الملفات
        /// </summary>
        public static string GetStoragePath()
        {
            try
            {
                string query = "SELECT setting_value FROM SystemSettings WHERE setting_key = 'StoragePath'";
                object result = ExecuteScalar(query);
                if (result != null && result != DBNull.Value)
                {
                    return result.ToString();
                }
            }
            catch { }

            string defaultPath = @"C:\ArchiveSystem\Files\";
            try
            {
                if (!Directory.Exists(defaultPath))
                    Directory.CreateDirectory(defaultPath);
            }
            catch { }

            return defaultPath;
        }

        /// <summary>
        /// التحقق من وجود جدول
        /// </summary>
        public static bool TableExists(string tableName)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM sqlite_master WHERE type='table' AND name=@name";
                var parameters = new Dictionary<string, object> { { "@name", tableName } };
                int count = Convert.ToInt32(ExecuteScalar(query, parameters));
                return count > 0;
            }
            catch
            {
                return false;
            }
        }

        #endregion
        #region "دوال الإعدادات"

        /// <summary>
        /// الحصول على إعداد من SystemSettings
        /// </summary>
        public static string GetSetting(string key, string defaultValue = null)
        {
            try
            {
                string query = "SELECT setting_value FROM SystemSettings WHERE setting_key = @key";
                var parameters = new Dictionary<string, object> { { "@key", key } };
                object result = ExecuteScalar(query, parameters);
                return result?.ToString() ?? defaultValue;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"⚠️ خطأ في GetSetting({key}): {ex.Message}");
                return defaultValue;
            }
        }

        /// <summary>
        /// تعيين إعداد في SystemSettings
        /// </summary>
        public static bool SetSetting(string key, string value, string type = "string")
        {
            try
            {
                string query = @"
            INSERT OR REPLACE INTO SystemSettings (setting_key, setting_value, setting_type, updated_at)
            VALUES (@key, @value, @type, CURRENT_TIMESTAMP)";

                var parameters = new Dictionary<string, object>
        {
            { "@key", key },
            { "@value", value },
            { "@type", type }
        };

                int result = ExecuteNonQuery(query, parameters);
                return result > 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"⚠️ خطأ في SetSetting({key}): {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// الحصول على إعداد كـ int
        /// </summary>
        public static int GetSettingInt(string key, int defaultValue = 0)
        {
            string value = GetSetting(key);
            if (int.TryParse(value, out int result))
                return result;
            return defaultValue;
        }

        /// <summary>
        /// الحصول على إعداد كـ bool
        /// </summary>
        public static bool GetSettingBool(string key, bool defaultValue = false)
        {
            string value = GetSetting(key);
            if (bool.TryParse(value, out bool result))
                return result;
            return defaultValue;
        }

        #endregion

        #region "ترقية قاعدة البيانات - إضافة الجداول الجديدة"

        /// <summary>
        /// ترقية قاعدة البيانات لإضافة الجداول والأعمدة الجديدة
        /// </summary>
        public static void UpgradeDatabase()
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        // 1. إنشاء الجداول الجديدة
                        CreateTableDevices(connection, transaction);
                        CreateTableEncryptionKeys(connection, transaction);
                        CreateTableBackupHistory(connection, transaction);

                        // 2. إضافة أعمدة جديدة للجداول الموجودة
                        AddNewColumns(connection, transaction);

                        transaction.Commit();
                        Debug.WriteLine("✅ تمت ترقية قاعدة البيانات بنجاح");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"❌ خطأ في ترقية قاعدة البيانات: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// إنشاء جدول الأجهزة
        /// </summary>
        private static void CreateTableDevices(SQLiteConnection connection, SQLiteTransaction transaction)
        {
            string sql = @"
        CREATE TABLE IF NOT EXISTS Devices (
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            device_guid TEXT NOT NULL UNIQUE,
            device_name TEXT NOT NULL,
            device_type TEXT DEFAULT 'Desktop',
            user_id INTEGER,
            installation_date TEXT DEFAULT CURRENT_TIMESTAMP,
            last_sync_date TEXT,
            app_version TEXT,
            notes TEXT,
            is_active INTEGER DEFAULT 1,
            FOREIGN KEY(user_id) REFERENCES Users(id)
        )";

            ExecuteNonQueryInternal(sql, connection, transaction);
            Debug.WriteLine("✅ تم إنشاء جدول Devices");

            // إنشاء الفهارس
            string[] indexes = {
        "CREATE INDEX IF NOT EXISTS IX_Devices_Guid ON Devices(device_guid);",
        "CREATE INDEX IF NOT EXISTS IX_Devices_User ON Devices(user_id);",
        "CREATE INDEX IF NOT EXISTS IX_Devices_Active ON Devices(is_active);"
    };

            foreach (string index in indexes)
            {
                try { ExecuteNonQueryInternal(index, connection, transaction); }
                catch (Exception ex) { Debug.WriteLine($"⚠️ فشل في إنشاء فهرس: {ex.Message}"); }
            }
        }

        /// <summary>
        /// إنشاء جدول مفاتيح التشفير
        /// </summary>
        private static void CreateTableEncryptionKeys(SQLiteConnection connection, SQLiteTransaction transaction)
        {
            string sql = @"
        CREATE TABLE IF NOT EXISTS EncryptionKeys (
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            key_version INTEGER NOT NULL,
            encrypted_key TEXT NOT NULL,
            algorithm TEXT DEFAULT 'AES-256-CBC',
            key_hash TEXT,
            created_by INTEGER,
            created_at TEXT DEFAULT CURRENT_TIMESTAMP,
            is_active INTEGER DEFAULT 1,
            FOREIGN KEY(created_by) REFERENCES Users(id)
        )";

            ExecuteNonQueryInternal(sql, connection, transaction);
            Debug.WriteLine("✅ تم إنشاء جدول EncryptionKeys");

            // إنشاء الفهارس
            string[] indexes = {
        "CREATE UNIQUE INDEX IF NOT EXISTS IX_EncryptionKeys_Version ON EncryptionKeys(key_version);",
        "CREATE INDEX IF NOT EXISTS IX_EncryptionKeys_Active ON EncryptionKeys(is_active);"
    };

            foreach (string index in indexes)
            {
                try { ExecuteNonQueryInternal(index, connection, transaction); }
                catch (Exception ex) { Debug.WriteLine($"⚠️ فشل في إنشاء فهرس: {ex.Message}"); }
            }
        }

        /// <summary>
        /// إنشاء جدول سجل النسخ الاحتياطية
        /// </summary>
        private static void CreateTableBackupHistory(SQLiteConnection connection, SQLiteTransaction transaction)
        {
            string sql = @"
        CREATE TABLE IF NOT EXISTS BackupHistory (
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            backup_name TEXT NOT NULL,
            backup_path TEXT,
            backup_date TEXT DEFAULT CURRENT_TIMESTAMP,
            backup_size INTEGER,
            database_version TEXT,
            device_id INTEGER,
            created_by INTEGER,
            status TEXT,
            notes TEXT,
            FOREIGN KEY(device_id) REFERENCES Devices(id),
            FOREIGN KEY(created_by) REFERENCES Users(id)
        )";

            ExecuteNonQueryInternal(sql, connection, transaction);
            Debug.WriteLine("✅ تم إنشاء جدول BackupHistory");

            // إنشاء الفهارس
            string[] indexes = {
        "CREATE INDEX IF NOT EXISTS IX_BackupHistory_Date ON BackupHistory(backup_date);",
        "CREATE INDEX IF NOT EXISTS IX_BackupHistory_Device ON BackupHistory(device_id);",
        "CREATE INDEX IF NOT EXISTS IX_BackupHistory_Status ON BackupHistory(status);"
    };

            foreach (string index in indexes)
            {
                try { ExecuteNonQueryInternal(index, connection, transaction); }
                catch (Exception ex) { Debug.WriteLine($"⚠️ فشل في إنشاء فهرس: {ex.Message}"); }
            }
        }

        /// <summary>
        /// إضافة أعمدة جديدة للجداول الموجودة
        /// </summary>
        /// <summary>
        /// إضافة أعمدة جديدة للجداول الموجودة
        /// </summary>
        private static void AddNewColumns(SQLiteConnection connection, SQLiteTransaction transaction)
        {
            // 1. إضافة device_id إلى AuditTrail
            AddColumnIfNotExists(connection, transaction, "AuditTrail", "device_id", "INTEGER");

            // 2. ✅ إضافة document_guid بدون UNIQUE في ALTER TABLE
            AddColumnIfNotExists(connection, transaction, "Documents", "document_guid", "TEXT", false);

            // 3. ✅ إنشاء فهرس UNIQUE بشكل منفصل
            try
            {
                string indexQuery = "CREATE UNIQUE INDEX IF NOT EXISTS IX_Documents_Guid ON Documents(document_guid)";
                using (var indexCmd = new SQLiteCommand(indexQuery, connection, transaction))
                {
                    indexCmd.ExecuteNonQuery();
                    Debug.WriteLine("✅ تم إنشاء فهرس UNIQUE لـ document_guid");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"⚠️ فشل في إنشاء فهرس UNIQUE لـ document_guid: {ex.Message}");
            }

            // 4. إضافة device_id إلى Users (اختياري)
            AddColumnIfNotExists(connection, transaction, "Users", "default_device_id", "INTEGER");

            // 5. إضافة sync_status إلى Documents (للمزامنة)
            AddColumnIfNotExists(connection, transaction, "Documents", "sync_status", "TEXT DEFAULT 'synced'");
            AddColumnIfNotExists(connection, transaction, "Documents", "sync_date", "TEXT");

            Debug.WriteLine("✅ تم إضافة الأعمدة الجديدة");
        }

        /// <summary>
        /// إضافة عمود إذا لم يكن موجوداً
        /// </summary>
        /// <summary>
        /// إضافة عمود إذا لم يكن موجوداً (مع دعم UNIQUE)
        /// </summary>
        private static void AddColumnIfNotExists(SQLiteConnection connection, SQLiteTransaction transaction, string tableName, string columnName, string columnType, bool isUnique = false)
        {
            try
            {
                string checkQuery = $@"
            SELECT COUNT(*) FROM pragma_table_info('{tableName}') 
            WHERE name = '{columnName}'";

                using (var cmd = new SQLiteCommand(checkQuery, connection, transaction))
                {
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    if (count == 0)
                    {
                        // ✅ إزالة UNIQUE من الإضافة المباشرة
                        string alterQuery = $"ALTER TABLE {tableName} ADD COLUMN {columnName} {columnType}";
                        using (var alterCmd = new SQLiteCommand(alterQuery, connection, transaction))
                        {
                            alterCmd.ExecuteNonQuery();
                            Debug.WriteLine($"✅ تم إضافة عمود {columnName} إلى جدول {tableName}");
                        }

                        // ✅ إنشاء فهرس UNIQUE بشكل منفصل
                        if (isUnique)
                        {
                            try
                            {
                                string indexQuery = $"CREATE UNIQUE INDEX IF NOT EXISTS IX_{tableName}_{columnName} ON {tableName}({columnName})";
                                using (var indexCmd = new SQLiteCommand(indexQuery, connection, transaction))
                                {
                                    indexCmd.ExecuteNonQuery();
                                    Debug.WriteLine($"✅ تم إنشاء فهرس UNIQUE للعمود {columnName}");
                                }
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine($"⚠️ فشل في إنشاء فهرس UNIQUE: {ex.Message}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"⚠️ فشل في إضافة عمود {columnName} إلى {tableName}: {ex.Message}");
            }
        }

        #endregion
    }
}