using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace A_WFA.Security
{
    public static class KeyManager
    {
        private static byte[] _currentMasterKey;
        private static int _currentKeyVersion;
        private static readonly object _lock = new object();

        /// <summary>
        /// تهيئة نظام المفاتيح - تحميل أو إنشاء مفتاح
        /// </summary>
        public static byte[] InitializeKey(int userId = 1)
        {
            lock (_lock)
            {
                try
                {
                    // 1. محاولة تحميل المفتاح النشط من قاعدة البيانات
                    byte[] key = LoadActiveKey();

                    if (key != null)
                    {
                        _currentMasterKey = key;
                        Debug.WriteLine($"✅ تم تحميل المفتاح الإصدار {_currentKeyVersion}");
                        return key;
                    }

                    // 2. إنشاء مفتاح جديد
                    Debug.WriteLine("⚠️ لم يتم العثور على مفتاح نشط، جاري إنشاء مفتاح جديد...");
                    return CreateNewKey(userId);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"❌ خطأ في تهيئة المفاتيح: {ex.Message}");
                    throw;
                }
            }
        }

        /// <summary>
        /// تحميل المفتاح النشط من قاعدة البيانات
        /// </summary>
        /// <summary>
        /// تحميل المفتاح النشط من قاعدة البيانات
        /// </summary>
        private static byte[] LoadActiveKey()
        {
            try
            {
                string query = @"
            SELECT id, key_version, encrypted_key, algorithm 
            FROM EncryptionKeys 
            WHERE is_active = 1 
            ORDER BY key_version DESC 
            LIMIT 1";

                DataTable dt = DatabaseManagerLite.ExecuteQuery(query);

                if (dt.Rows.Count == 0)
                    return null;

                DataRow row = dt.Rows[0];
                _currentKeyVersion = Convert.ToInt32(row["key_version"]);

                string encryptedKeyBase64 = row["encrypted_key"].ToString();
                byte[] encryptedKey = Convert.FromBase64String(encryptedKeyBase64);

                // ✅ محاولة فك التشفير مع معالجة الأخطاء
                try
                {
                    return DecryptKeyWithSessionKey(encryptedKey);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"⚠️ فشل فك تشفير المفتاح الإصدار {_currentKeyVersion}: {ex.Message}");

                    // ✅ إذا فشل فك التشفير، تعطيل المفتاح القديم وإنشاء مفتاح جديد
                    string deactivateQuery = "UPDATE EncryptionKeys SET is_active = 0 WHERE id = @id";
                    var parameters = new Dictionary<string, object> { { "@id", row["id"] } };
                    DatabaseManagerLite.ExecuteNonQuery(deactivateQuery, parameters);

                    Debug.WriteLine($"⚠️ تم تعطيل المفتاح الإصدار {_currentKeyVersion} بسبب فشل فك التشفير");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"⚠️ فشل تحميل المفتاح: {ex.Message}");
                return null;
            }
        }
        /// <summary>
        /// إنشاء مفتاح جديد وحفظه في قاعدة البيانات
        /// </summary>
        private static byte[] CreateNewKey(int userId)
        {
            try
            {
                // 1. توليد مفتاح جديد
                byte[] newKey = CryptoService.GenerateKey();

                // 2. تشفير المفتاح بمفتاح جلسة
                byte[] encryptedKey = EncryptKeyWithSessionKey(newKey);

                // 3. حساب Hash للمفتاح
                string keyHash = CryptoService.ComputeHash(newKey);

                // 4. الحصول على الإصدار التالي
                int nextVersion = GetNextKeyVersion();

                // 5. حفظ في قاعدة البيانات
                string query = @"
                    INSERT INTO EncryptionKeys (
                        key_version, encrypted_key, algorithm, 
                        key_hash, created_by, created_at, is_active
                    ) VALUES (
                        @version, @encryptedKey, @algorithm,
                        @hash, @userId, CURRENT_TIMESTAMP, 1
                    )";

                var parameters = new Dictionary<string, object>
                {
                    { "@version", nextVersion },
                    { "@encryptedKey", Convert.ToBase64String(encryptedKey) },
                    { "@algorithm", "AES-256-CBC" },
                    { "@hash", keyHash },
                    { "@userId", userId }
                };

                DatabaseManagerLite.ExecuteNonQuery(query, parameters);

                _currentKeyVersion = nextVersion;
                _currentMasterKey = newKey;

                Debug.WriteLine($"✅ تم إنشاء مفتاح جديد الإصدار {nextVersion}");

                // تسجيل العملية
                DatabaseManagerLite.SafeLogAuditTrail(userId, "KEY_CREATED",
                    $"تم إنشاء مفتاح تشفير جديد الإصدار {nextVersion}");

                return newKey;
            }
            catch (Exception ex)
            {
                throw new Exception($"فشل إنشاء مفتاح جديد: {ex.Message}");
            }
        }

        /// <summary>
        /// الحصول على الإصدار التالي للمفتاح
        /// </summary>
        private static int GetNextKeyVersion()
        {
            try
            {
                string query = "SELECT COALESCE(MAX(key_version), 0) + 1 FROM EncryptionKeys";
                object result = DatabaseManagerLite.ExecuteScalar(query);
                return result != null ? Convert.ToInt32(result) : 1;
            }
            catch
            {
                return 1;
            }
        }

        /// <summary>
        /// تشفير المفتاح بمفتاح جلسة (يعتمد على الجهاز)
        /// </summary>
        private static byte[] EncryptKeyWithSessionKey(byte[] key)
        {
            // استخدام مفتاح جلسة مشتق من معرف الجهاز
            byte[] sessionKey = GetSessionKey();
            return CryptoService.Encrypt(key, sessionKey);
        }

        /// <summary>
        /// فك تشفير المفتاح بمفتاح جلسة
        /// </summary>
        private static byte[] DecryptKeyWithSessionKey(byte[] encryptedKey)
        {
            byte[] sessionKey = GetSessionKey();
            return CryptoService.Decrypt(encryptedKey, sessionKey);
        }

        /// <summary>
        /// الحصول على مفتاح جلسة فريد للجهاز
        /// </summary>
        private static byte[] GetSessionKey()
        {
            // استخدام معرف الجهاز كمفتاح جلسة
            string deviceId = DeviceManager.GetCurrentDeviceGuid();
            using (var sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(Encoding.UTF8.GetBytes(deviceId));
            }
        }

        /// <summary>
        /// الحصول على المفتاح الحالي
        /// </summary>
        public static byte[] GetCurrentKey()
        {
            if (_currentMasterKey == null)
                throw new InvalidOperationException("المفتاح غير مهيأ");
            return _currentMasterKey;
        }

        /// <summary>
        /// التحقق من صحة المفتاح
        /// </summary>
        public static bool ValidateKey(byte[] key)
        {
            try
            {
                string query = @"
                    SELECT key_hash FROM EncryptionKeys 
                    WHERE key_version = @version AND is_active = 1";

                var parameters = new Dictionary<string, object>
                {
                    { "@version", _currentKeyVersion }
                };

                object result = DatabaseManagerLite.ExecuteScalar(query, parameters);
                if (result == null) return false;

                string storedHash = result.ToString();
                string currentHash = CryptoService.ComputeHash(key);
                return storedHash == currentHash;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// تدوير المفتاح (إنشاء مفتاح جديد وتعطيل القديم)
        /// </summary>
        public static byte[] RotateKey(int userId)
        {
            lock (_lock)
            {
                try
                {
                    // 1. تعطيل المفتاح القديم
                    string deactivateQuery = "UPDATE EncryptionKeys SET is_active = 0 WHERE is_active = 1";
                    DatabaseManagerLite.ExecuteNonQuery(deactivateQuery);

                    // 2. إنشاء مفتاح جديد
                    return CreateNewKey(userId);
                }
                catch (Exception ex)
                {
                    throw new Exception($"فشل تدوير المفتاح: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// تصدير المفتاح للنسخ الاحتياطي
        /// </summary>
        public static string ExportKeyForBackup()
        {
            try
            {
                byte[] key = GetCurrentKey();
                string encryptedKey = Convert.ToBase64String(
                    ProtectedData.Protect(key, null, DataProtectionScope.CurrentUser)
                );
                return encryptedKey;
            }
            catch (Exception ex)
            {
                throw new Exception($"فشل تصدير المفتاح: {ex.Message}");
            }
        }

        /// <summary>
        /// استيراد المفتاح من النسخ الاحتياطي
        /// </summary>
        public static bool ImportKeyFromBackup(string encryptedKeyBase64, int userId)
        {
            try
            {
                byte[] protectedKey = Convert.FromBase64String(encryptedKeyBase64);
                byte[] key = ProtectedData.Unprotect(protectedKey, null, DataProtectionScope.CurrentUser);

                // تعطيل المفاتيح القديمة
                string deactivateQuery = "UPDATE EncryptionKeys SET is_active = 0";
                DatabaseManagerLite.ExecuteNonQuery(deactivateQuery);

                // تشفير المفتاح بمفتاح جلسة
                byte[] encryptedKey = EncryptKeyWithSessionKey(key);
                string keyHash = CryptoService.ComputeHash(key);
                int nextVersion = GetNextKeyVersion();

                string query = @"
                    INSERT INTO EncryptionKeys (
                        key_version, encrypted_key, algorithm,
                        key_hash, created_by, created_at, is_active
                    ) VALUES (
                        @version, @encryptedKey, @algorithm,
                        @hash, @userId, CURRENT_TIMESTAMP, 1
                    )";

                var parameters = new Dictionary<string, object>
                {
                    { "@version", nextVersion },
                    { "@encryptedKey", Convert.ToBase64String(encryptedKey) },
                    { "@algorithm", "AES-256-CBC" },
                    { "@hash", keyHash },
                    { "@userId", userId }
                };

                DatabaseManagerLite.ExecuteNonQuery(query, parameters);

                _currentMasterKey = key;
                _currentKeyVersion = nextVersion;

                Debug.WriteLine($"✅ تم استيراد المفتاح الإصدار {nextVersion}");
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"فشل استيراد المفتاح: {ex.Message}");
            }
        }

        /// <summary>
        /// الحصول على نسخة احتياطية من المفتاح (مشفرة)
        /// </summary>
        public static string GetKeyBackup()
        {
            try
            {
                byte[] key = GetCurrentKey();
                // تشفير المفتاح بكلمة مرور
                byte[] salt = new byte[16];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(salt);
                }

                // استخدام كلمة مرور ثابتة أو من المستخدم
                string password = "ArchiveSystemBackupKey2024";
                using (var deriveBytes = new Rfc2898DeriveBytes(password, salt, 100000))
                {
                    byte[] passwordKey = deriveBytes.GetBytes(32);
                    byte[] encryptedKey = CryptoService.Encrypt(key, passwordKey);

                    byte[] result = new byte[salt.Length + encryptedKey.Length];
                    Buffer.BlockCopy(salt, 0, result, 0, salt.Length);
                    Buffer.BlockCopy(encryptedKey, 0, result, salt.Length, encryptedKey.Length);

                    return Convert.ToBase64String(result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"فشل إنشاء نسخة احتياطية للمفتاح: {ex.Message}");
            }
        }

        /// <summary>
        /// استعادة المفتاح من نسخة احتياطية
        /// </summary>
        public static byte[] RestoreKeyFromBackup(string backupBase64)
        {
            try
            {
                byte[] data = Convert.FromBase64String(backupBase64);

                // استخراج salt
                byte[] salt = new byte[16];
                byte[] encryptedKey = new byte[data.Length - 16];
                Buffer.BlockCopy(data, 0, salt, 0, 16);
                Buffer.BlockCopy(data, 16, encryptedKey, 0, encryptedKey.Length);

                string password = "ArchiveSystemBackupKey2024";
                using (var deriveBytes = new Rfc2898DeriveBytes(password, salt, 100000))
                {
                    byte[] passwordKey = deriveBytes.GetBytes(32);
                    return CryptoService.Decrypt(encryptedKey, passwordKey);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"فشل استعادة المفتاح: {ex.Message}");
            }
        }
    }
}