using System;
using System.Windows.Forms;
using A_WFA.Navigation;

namespace A_WFA
{
    public partial class MainForm : Form
    {

        public MainForm()
        {
            InitializeComponent();

            //// تهيئة مدير التنقل مع النموذج الرئيسي
            //NavigationManager.Initialize(this);
        }


        // زر فتح الأرشيف
        private void button1_Click(object sender, EventArgs e)
        {
            //NavigationManager.Navigate<ArchivOffiiceForm>();
        }


        // عند إغلاق الرئيسية يغلق النظام بالكامل
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Application.Exit();
        }

    }
}