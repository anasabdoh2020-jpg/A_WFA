using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace A_WFA.Security
{
    public static class CryptoService
    {
        private static byte[] _masterKey;
        private static readonly int KeySize = 32; // 256-bit
        private static readonly object _lock = new object();

        /// <summary>
        /// تهيئة نظام التشفير بالمفتاح الرئيسي
        /// </summary>
        public static void Initialize(byte[] masterKey)
        {
            if (masterKey == null || masterKey.Length != KeySize)
                throw new ArgumentException($"المفتاح يجب أن يكون {KeySize} بايت");
            _masterKey = masterKey;
        }

        /// <summary>
        /// تشفير البيانات باستخدام AES-256-CBC
        /// </summary>
        public static byte[] Encrypt(byte[] data, byte[] key = null)
        {
            if (data == null || data.Length == 0)
                return data;

            byte[] encryptionKey = key ?? _masterKey;
            if (encryptionKey == null || encryptionKey.Length != KeySize)
                throw new InvalidOperationException("المفتاح غير مهيأ");

            using (var aes = Aes.Create())
            {
                aes.Key = encryptionKey;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.GenerateIV();

                using (var encryptor = aes.CreateEncryptor())
                {
                    byte[] ciphertext = encryptor.TransformFinalBlock(data, 0, data.Length);

                    // دمج IV + البيانات المشفرة
                    byte[] result = new byte[aes.IV.Length + ciphertext.Length];
                    Buffer.BlockCopy(aes.IV, 0, result, 0, aes.IV.Length);
                    Buffer.BlockCopy(ciphertext, 0, result, aes.IV.Length, ciphertext.Length);

                    return result;
                }
            }
        }

        /// <summary>
        /// فك تشفير البيانات
        /// </summary>
        public static byte[] Decrypt(byte[] encryptedData, byte[] key = null)
        {
            if (encryptedData == null || encryptedData.Length == 0)
                return encryptedData;

            if (encryptedData.Length < 16)
                throw new ArgumentException("البيانات المشفرة غير صالحة");

            byte[] encryptionKey = key ?? _masterKey;
            if (encryptionKey == null || encryptionKey.Length != KeySize)
                throw new InvalidOperationException("المفتاح غير مهيأ");

            using (var aes = Aes.Create())
            {
                aes.Key = encryptionKey;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                // استخراج IV
                byte[] iv = new byte[16];
                byte[] ciphertext = new byte[encryptedData.Length - 16];
                Buffer.BlockCopy(encryptedData, 0, iv, 0, 16);
                Buffer.BlockCopy(encryptedData, 16, ciphertext, 0, ciphertext.Length);

                aes.IV = iv;

                using (var decryptor = aes.CreateDecryptor())
                {
                    return decryptor.TransformFinalBlock(ciphertext, 0, ciphertext.Length);
                }
            }
        }

        /// <summary>
        /// تشفير ملف وحفظه
        /// </summary>
        public static void EncryptFile(string inputPath, string outputPath, byte[] key = null)
        {
            byte[] data = File.ReadAllBytes(inputPath);
            byte[] encrypted = Encrypt(data, key);
            File.WriteAllBytes(outputPath, encrypted);
        }

        /// <summary>
        /// فك تشفير ملف
        /// </summary>
        public static void DecryptFile(string inputPath, string outputPath, byte[] key = null)
        {
            byte[] encrypted = File.ReadAllBytes(inputPath);
            byte[] decrypted = Decrypt(encrypted, key);
            File.WriteAllBytes(outputPath, decrypted);
        }

        /// <summary>
        /// حساب Hash للملف للتحقق من السلامة
        /// </summary>
        public static string ComputeHash(byte[] data)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] hash = sha256.ComputeHash(data);
                return Convert.ToBase64String(hash);
            }
        }

        /// <summary>
        /// التحقق من سلامة الملف
        /// </summary>
        public static bool VerifyIntegrity(byte[] data, string expectedHash)
        {
            string actualHash = ComputeHash(data);
            return actualHash == expectedHash;
        }

        /// <summary>
        /// توليد مفتاح عشوائي
        /// </summary>
        public static byte[] GenerateKey()
        {
            byte[] key = new byte[KeySize];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(key);
            }
            return key;
        }

        /// <summary>
        /// تشفير نص
        /// </summary>
        public static string EncryptString(string plainText, byte[] key = null)
        {
            if (string.IsNullOrEmpty(plainText))
                return null;

            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] encrypted = Encrypt(plainBytes, key);
            return Convert.ToBase64String(encrypted);
        }

        /// <summary>
        /// فك تشفير نص
        /// </summary>
        public static string DecryptString(string cipherText, byte[] key = null)
        {
            if (string.IsNullOrEmpty(cipherText))
                return null;

            byte[] encrypted = Convert.FromBase64String(cipherText);
            byte[] plainBytes = Decrypt(encrypted, key);
            return Encoding.UTF8.GetString(plainBytes);
        }
    }
}