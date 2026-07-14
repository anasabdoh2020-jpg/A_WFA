using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using A_WFA.Security;

namespace A_WFA.Security
{
    public static class SecureFileManager
    {
        private static string _storagePath;
        private static byte[] _encryptionKey;
        private static readonly object _lock = new object();

        /// <summary>
        /// تهيئة مدير الملفات
        /// </summary>
        public static void Initialize(string storagePath, byte[] encryptionKey)
        {
            _storagePath = storagePath;
            _encryptionKey = encryptionKey;

            if (!Directory.Exists(_storagePath))
                Directory.CreateDirectory(_storagePath);
        }

        /// <summary>
        /// حفظ ملف مشفر
        /// </summary>
        public static FileSaveResult SaveEncryptedFile(byte[] fileData, string originalFileName, int boxId = 0)
        {
            lock (_lock)
            {
                try
                {
                    // 1. إنشاء المسار
                    string year = DateTime.Now.ToString("yyyy");
                    string month = DateTime.Now.ToString("MM");
                    string boxFolder = boxId > 0 ? $"Box_{boxId:D3}" : "General";

                    string fullPath = Path.Combine(_storagePath, year, month, boxFolder);
                    if (!Directory.Exists(fullPath))
                        Directory.CreateDirectory(fullPath);

                    // 2. إنشاء اسم ملف فريد
                    string fileId = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(originalFileName);
                    string encryptedFileName = $"{fileId}{extension}.enc";
                    string relativePath = Path.Combine(year, month, boxFolder, encryptedFileName);
                    string fullFilePath = Path.Combine(_storagePath, relativePath);

                    // 3. تشفير الملف
                    byte[] encryptedData = CryptoService.Encrypt(fileData, _encryptionKey);

                    // 4. حفظ الملف المشفر
                    File.WriteAllBytes(fullFilePath, encryptedData);

                    // 5. حساب Hash للتحقق من السلامة
                    string fileHash = CryptoService.ComputeHash(fileData);

                    // 6. إرجاع النتيجة
                    return new FileSaveResult
                    {
                        FileId = fileId,
                        RelativePath = relativePath,
                        FullPath = fullFilePath,
                        FileName = originalFileName,
                        FileSize = fileData.Length,
                        FileHash = fileHash,
                        EncryptedSize = encryptedData.Length
                    };
                }
                catch (Exception ex)
                {
                    throw new Exception($"فشل حفظ الملف المشفر: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// استرجاع ملف مشفر وفك تشفيره
        /// </summary>
        public static byte[] LoadEncryptedFile(string relativePath)
        {
            lock (_lock)
            {
                try
                {
                    string fullPath = Path.Combine(_storagePath, relativePath);

                    if (!File.Exists(fullPath))
                        throw new FileNotFoundException("الملف غير موجود", fullPath);

                    // قراءة الملف المشفر
                    byte[] encryptedData = File.ReadAllBytes(fullPath);

                    // فك التشفير
                    return CryptoService.Decrypt(encryptedData, _encryptionKey);
                }
                catch (Exception ex)
                {
                    throw new Exception($"فشل تحميل الملف المشفر: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// استرجاع ملف مع التحقق من السلامة
        /// </summary>
        public static byte[] LoadEncryptedFileWithIntegrity(string relativePath, string expectedHash)
        {
            byte[] data = LoadEncryptedFile(relativePath);

            if (!CryptoService.VerifyIntegrity(data, expectedHash))
                throw new Exception("⚠️ تحذير: الملف تالف أو معدل!");

            return data;
        }

        /// <summary>
        /// حذف ملف مشفر (آمن)
        /// </summary>
        public static bool DeleteEncryptedFile(string relativePath)
        {
            try
            {
                string fullPath = Path.Combine(_storagePath, relativePath);

                if (!File.Exists(fullPath))
                    return false;

                // حذف آمن (الكتابة فوق البيانات)
                SecureDelete(fullPath);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// حذف آمن للملفات (الكتابة فوق البيانات)
        /// </summary>
        private static void SecureDelete(string filePath)
        {
            try
            {
                // الكتابة فوق الملف ببيانات عشوائية 3 مرات
                using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Write))
                {
                    byte[] randomData = new byte[fs.Length];

                    for (int i = 0; i < 3; i++)
                    {
                        using (var rng = RandomNumberGenerator.Create())
                        {
                            rng.GetBytes(randomData);
                        }
                        fs.Position = 0;
                        fs.Write(randomData, 0, randomData.Length);
                        fs.Flush();
                    }
                }
                File.Delete(filePath);
            }
            catch { File.Delete(filePath); }
        }

        /// <summary>
        /// إنشاء ملف مؤقت للعرض (يحذف تلقائياً بعد 5 دقائق)
        /// </summary>
        public static string CreateTempFile(byte[] fileData, string originalFileName)
        {
            string tempPath = Path.Combine(_storagePath, "Temp");
            if (!Directory.Exists(tempPath))
                Directory.CreateDirectory(tempPath);

            string extension = Path.GetExtension(originalFileName);
            string tempFileName = $"{Guid.NewGuid()}{extension}";
            string tempFilePath = Path.Combine(tempPath, tempFileName);

            // حفظ الملف المؤقت
            File.WriteAllBytes(tempFilePath, fileData);

            // جدولة الحذف بعد 5 دقائق
            System.Threading.Tasks.Task.Delay(300000).ContinueWith(_ =>
            {
                try { SecureDelete(tempFilePath); } catch { }
            });

            return tempFilePath;
        }

        /// <summary>
        /// الحصول على معلومات الملف
        /// </summary>
        public static FileInfo GetFileInfo(string relativePath)
        {
            string fullPath = Path.Combine(_storagePath, relativePath);
            return File.Exists(fullPath) ? new FileInfo(fullPath) : null;
        }

        /// <summary>
        /// الحصول على المسار الكامل
        /// </summary>
        public static string GetFullPath(string relativePath)
        {
            return Path.Combine(_storagePath, relativePath);
        }

        /// <summary>
        /// التحقق من وجود ملف
        /// </summary>
        public static bool FileExists(string relativePath)
        {
            string fullPath = Path.Combine(_storagePath, relativePath);
            return File.Exists(fullPath);
        }

        /// <summary>
        /// تنظيف الملفات المؤقتة القديمة
        /// </summary>
        public static void CleanTempFiles()
        {
            try
            {
                string tempPath = Path.Combine(_storagePath, "Temp");
                if (!Directory.Exists(tempPath))
                    return;

                foreach (string file in Directory.GetFiles(tempPath, "*.*", SearchOption.AllDirectories))
                {
                    try
                    {
                        if (File.GetCreationTime(file) < DateTime.Now.AddMinutes(-5))
                        {
                            SecureDelete(file);
                        }
                    }
                    catch { }
                }
            }
            catch { }
        }

        /// <summary>
        /// الحصول على حجم التخزين الإجمالي
        /// </summary>
        public static long GetTotalSize()
        {
            try
            {
                long total = 0;
                foreach (string file in Directory.GetFiles(_storagePath, "*.*", SearchOption.AllDirectories))
                {
                    try { total += new FileInfo(file).Length; } catch { }
                }
                return total;
            }
            catch { return 0; }
        }

        /// <summary>
        /// الحصول على حجم التخزين كـ string مقروء
        /// </summary>
        public static string GetTotalSizeReadable()
        {
            long size = GetTotalSize();
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            int order = 0;
            while (size >= 1024 && order < sizes.Length - 1)
            {
                order++;
                size = size / 1024;
            }
            return $"{size:0.##} {sizes[order]}";
        }
    }

    /// <summary>
    /// نتيجة حفظ الملف
    /// </summary>
    public class FileSaveResult
    {
        public string FileId { get; set; }
        public string RelativePath { get; set; }
        public string FullPath { get; set; }
        public string FileName { get; set; }
        public long FileSize { get; set; }
        public string FileHash { get; set; }
        public long EncryptedSize { get; set; }
    }
}