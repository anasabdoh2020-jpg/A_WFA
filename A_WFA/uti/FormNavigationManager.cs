using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace A_WFA.uti
{
    internal static class FormNavigationManager
    {
        public static Form MainForm = null;
        public static Form CurrentForm = null;  // متغير جديد لتتبع النافذة الحالية

        public static void OpenChildForm(Form parent, Form child)
        {
            MainForm = parent;
            CurrentForm = child;  // تعيين النافذة الحالية
            parent.Hide();

            child.FormClosing += ChildForm_FormClosing;
            child.Show();
        }

        // دالة جديدة للرجوع إلى الواجهة السابقة
        public static void GoBack(Form currentForm)
        {
            // التحقق من وجود نافذة رئيسية
            if (MainForm == null || MainForm.IsDisposed)
            {
                MessageBox.Show("لا توجد واجهة سابقة للرجوع إليها", "تنبيه",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // إلغاء ربط حدث الإغلاق من النافذة الحالية
            currentForm.FormClosing -= ChildForm_FormClosing;

            // إخفاء النافذة الحالية
            currentForm.Hide();

            // إظهار النافذة الرئيسية
            MainForm.Show();

            // تحديث المرجع
            CurrentForm = MainForm;
            MainForm = null;  // يمكن إعادة تعيينها حسب الحاجة
        }

        private static void ChildForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // فقط إذا كان الإغلاق من المستخدم
            if (e.CloseReason != CloseReason.UserClosing) return;

            DialogResult result = MessageBox.Show(
                "هل تريد إغلاق البرنامج؟",
                "تأكيد الإغلاق",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
            else
            {
                e.Cancel = true;
                if (MainForm != null && !MainForm.IsDisposed)
                {
                    MainForm.Show();
                }
            }
        }
    }
}