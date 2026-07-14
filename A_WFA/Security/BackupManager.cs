using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;

namespace A_WFA.Security
{
    public static class BackupManager
    {
        /// <summary>
        /// إنشاء نسخة احتياطية كاملة
        /// </summary>
        public static string CreateFullBackup(int userId, bool includeFiles = true, string destinationPath = null)
        {
            try
            {
                string backupFolder = GetBackupFolder();
                if (string.IsNullOrEmpty(destinationPath))
                    destinationPath = backupFolder;

                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string backupName = $"ArchiveBackup_{timestamp}";
                string tempFolder = Path.Combine(Path.GetTempPath(), $"Backup_{Guid.NewGuid()}");

                Directory.CreateDirectory(tempFolder);

                // 1. نسخ قاعدة البيانات
                string dbPath = DatabaseManagerLite.GetDatabaseFilePath();
                if (File.Exists(dbPath))
                {
                    string dbDest = Path.Combine(tempFolder, "Archive.db");
                    File.Copy(dbPath, dbDest, true);
                }

                // 2. تصدير مفتاح التشفير
                string keyBackup = KeyManager.ExportKeyForBackup();
                File.WriteAllText(Path.Combine(tempFolder, "encryption_key.bak"), keyBackup);

                // 3. معلومات النسخة
                string info = $@"
╔═══════════════════════════════════════════════════════════════╗
║                   معلومات النسخة الاحتياطية                  ║
╠═══════════════════════════════════════════════════════════════╣
║                                                               ║
║  📅 تاريخ الإنشاء: {DateTime.Now:yyyy/MM/dd HH:mm:ss}       ║
║  🆔 معرف الجهاز: {DeviceManager.GetCurrentDeviceId()}        ║
║  📱 اسم الجهاز: {Environment.MachineName}                    ║
║  👤 المستخدم: {Environment.UserName}                        ║
║  🔑 إصدار المفتاح: {GetCurrentKeyVersion()}                 ║
║  📁 تشمل الملفات: {(includeFiles ? "نعم" : "لا")}           ║
║  ⚙️  إصدار النظام: {Application.ProductVersion}             ║
║                                                               ║
╚═══════════════════════════════════════════════════════════════╝
                ";
                File.WriteAllText(Path.Combine(tempFolder, "BackupInfo.txt"), info);

                // 4. نسخ الملفات (اختياري)
                if (includeFiles)
                {
                    string storagePath = DatabaseManagerLite.GetStoragePath();
                    if (Directory.Exists(storagePath))
                    {
                        string filesDest = Path.Combine(tempFolder, "Files");
                        CopyDirectory(storagePath, filesDest);
                    }
                }

                // 5. ضغط المجلد
                string zipPath = Path.Combine(destinationPath, $"{backupName}.zip");
                ZipFile.CreateFromDirectory(tempFolder, zipPath);

                // 6. تنظيف المجلد المؤقت
                try { Directory.Delete(tempFolder, true); } catch { }

                // 7. تسجيل في قاعدة البيانات
                long fileSize = new FileInfo(zipPath).Length;
                RegisterBackupHistory(backupName, zipPath, fileSize, userId);

                Debug.WriteLine($"✅ تم إنشاء النسخة الاحتياطية: {zipPath}");
                return zipPath;
            }
            catch (Exception ex)
            {
                throw new Exception($"فشل إنشاء النسخة الاحتياطية: {ex.Message}");
            }
        }

        /// <summary>
        /// استعادة نسخة احتياطية
        /// </summary>
        public static bool RestoreFullBackup(string backupPath, int userId)
        {
            try
            {
                if (!File.Exists(backupPath))
                    throw new FileNotFoundException("الملف غير موجود");

                // 1. فك الضغط
                string tempFolder = Path.Combine(Path.GetTempPath(), $"Restore_{Guid.NewGuid()}");
                ZipFile.ExtractToDirectory(backupPath, tempFolder);

                // 2. استعادة قاعدة البيانات
                string dbFile = Path.Combine(tempFolder, "Archive.db");
                if (!File.Exists(dbFile))
                    throw new Exception("ملف قاعدة البيانات غير موجود");

                string targetDb = DatabaseManagerLite.GetDatabaseFilePath();
                File.Copy(dbFile, targetDb, true);

                // 3. استعادة المفتاح
                string keyFile = Path.Combine(tempFolder, "encryption_key.bak");
                if (File.Exists(keyFile))
                {
                    string encryptedKey = File.ReadAllText(keyFile);
                    KeyManager.ImportKeyFromBackup(encryptedKey, userId);
                }

                // 4. استعادة الملفات
                string filesFolder = Path.Combine(tempFolder, "Files");
                if (Directory.Exists(filesFolder))
                {
                    string targetFiles = DatabaseManagerLite.GetStoragePath();
                    CopyDirectory(filesFolder, targetFiles);
                }

                // 5. تنظيف
                try { Directory.Delete(tempFolder, true); } catch { }

                // 6. تسجيل الاستعادة
                DatabaseManagerLite.SafeLogAuditTrail(userId, "RESTORE_BACKUP",
                    $"تم استعادة النسخة: {Path.GetFileName(backupPath)}");

                Debug.WriteLine($"✅ تم استعادة النسخة: {backupPath}");
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"فشل استعادة النسخة: {ex.Message}");
            }
        }

        /// <summary>
        /// الحصول على مجلد النسخ الاحتياطية
        /// </summary>
        private static string GetBackupFolder()
        {
            string path = DatabaseManagerLite.GetSetting("BackupPath");
            if (string.IsNullOrEmpty(path))
            {
                path = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "A_WFA",
                    "Backups"
                );
            }

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            return path;
        }

        /// <summary>
        /// نسخ مجلد
        /// </summary>
        private static void CopyDirectory(string sourceDir, string destDir)
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

        /// <summary>
        /// الحصول على إصدار المفتاح الحالي
        /// </summary>
        private static int GetCurrentKeyVersion()
        {
            try
            {
                string query = "SELECT key_version FROM EncryptionKeys WHERE is_active = 1";
                object result = DatabaseManagerLite.ExecuteScalar(query);
                return result != null ? Convert.ToInt32(result) : 0;
            }
            catch { return 0; }
        }

        /// <summary>
        /// تسجيل في سجل النسخ الاحتياطية
        /// </summary>
        private static void RegisterBackupHistory(string name, string path, long size, int userId)
        {
            try
            {
                string query = @"
                    INSERT INTO BackupHistory (
                        backup_name, backup_path, backup_date, backup_size,
                        database_version, device_id, created_by, status
                    ) VALUES (
                        @name, @path, CURRENT_TIMESTAMP, @size,
                        @dbVersion, @deviceId, @userId, 'Completed'
                    )";

                var parameters = new Dictionary<string, object>
                {
                    { "@name", name },
                    { "@path", path },
                    { "@size", size },
                    { "@dbVersion", GetCurrentKeyVersion().ToString() },
                    { "@deviceId", DeviceManager.GetCurrentDeviceId() },
                    { "@userId", userId }
                };

                DatabaseManagerLite.ExecuteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"⚠️ فشل تسجيل سجل النسخة: {ex.Message}");
            }
        }

        /// <summary>
        /// الحصول على سجل النسخ الاحتياطية
        /// </summary>
        public static DataTable GetBackupHistory()
        {
            try
            {
                string query = @"
                    SELECT 
                        bh.*,
                        d.device_name,
                        u.full_name as user_name
                    FROM BackupHistory bh
                    LEFT JOIN Devices d ON bh.device_id = d.id
                    LEFT JOIN Users u ON bh.created_by = u.id
                    ORDER BY bh.backup_date DESC";

                return DatabaseManagerLite.ExecuteQuery(query);
            }
            catch
            {
                return new DataTable();
            }
        }
    }
}