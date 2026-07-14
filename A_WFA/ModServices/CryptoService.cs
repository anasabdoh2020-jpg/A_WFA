using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace A_WFA.ModServices
{
    public static class CryptoService
    {
        // ⚠️ هام جداً: يجب أن يكون المفتاح 32 حرفاً (للتشفير بقوة 256 بت) 
        // ومفتاح الـ IV عبارة عن 16 حرفاً. لا تشارك هذه المفاتيح مع أحد!
        private static readonly byte[] Key = Encoding.UTF8.GetBytes("MySecretKeyMustBe32Characters!!!_"); // 32 Bytes
        private static readonly byte[] IV = Encoding.UTF8.GetBytes("InitVector16Char");               // 16 Bytes

        /// <summary>
        /// تشفير مصفوفة البايتات للملف الأصلي
        /// </summary>
        public static byte[] EncryptBytes(byte[] plainBytes)
        {
            if (plainBytes == null || plainBytes.Length == 0) return null;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = IV;

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(plainBytes, 0, plainBytes.Length);
                        cs.FlushFinalBlock();
                    }
                    return ms.ToArray();
                }
            }
        }

        /// <summary>
        /// فك تشفير مصفوفة البايتات المشفرة وإعادتها لأصلها
        /// </summary>
        public static byte[] DecryptBytes(byte[] cipherBytes)
        {
            if (cipherBytes == null || cipherBytes.Length == 0) return null;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = IV;

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.FlushFinalBlock();
                    }
                    return ms.ToArray();
                }
            }
        }
    }
}