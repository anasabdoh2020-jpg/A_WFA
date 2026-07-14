using A_WFA.Navigation;
using A_WFA.Security;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace A_WFA
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                // 1. تهيئة قاعدة البيانات
                if (!DatabaseManagerLite.InitializeDatabase())
                {
                    MessageBox.Show("❌ فشل في تهيئة قاعدة البيانات!", "خطأ",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 2. ترقية قاعدة البيانات (إضافة الجداول الجديدة)
                try
                {
                    DatabaseManagerLite.UpgradeDatabase();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"⚠️ تحذير في ترقية قاعدة البيانات: {ex.Message}");
                }

                // 3. تهيئة الجهاز
                DeviceManager.InitializeDevice(1);

                // 4. تهيئة المفاتيح
                byte[] masterKey = KeyManager.InitializeKey(1);
                CryptoService.Initialize(masterKey);

                // 5. التحقق من مسار التخزين
                string storagePath = DatabaseManagerLite.GetStoragePath();
                if (!Directory.Exists(storagePath))
                    Directory.CreateDirectory(storagePath);

                // 6. إنشاء النموذج الرئيسي
                ArchivOffiiceForm mainFormInstance = new ArchivOffiiceForm();

                // 7. تهيئة مدير التنقل (إذا كان موجوداً)
                try
                {
                    NavigationManager.Initialize(mainFormInstance);
                }
                catch
                {
                    // إذا لم يكن NavigationManager موجوداً، تجاهل
                }

                // 8. ✅ تشغيل التطبيق
                Application.Run(mainFormInstance);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ خطأ في تشغيل التطبيق: {ex.Message}",
                    "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}