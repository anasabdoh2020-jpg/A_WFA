using System;
using System.Drawing;
using System.Windows.Forms;

namespace A_WFA
{
    public partial class FrmCreateBackupOptions : Form
    {
        #region "الخصائص"

        public string SelectedType { get; private set; } = "نسخة كاملة";
        public bool IncludeFiles { get; private set; } = true;
        public bool Compress { get; private set; } = true;

        #endregion

        #region "المُنشئ"

        public FrmCreateBackupOptions()
        {
            InitializeComponent1();
            SetupUI();
        }

        #endregion

        #region "تهيئة الواجهة"

        private void InitializeComponent1()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 350);
            this.Text = "🔧 خيارات إنشاء النسخة الاحتياطية";
        }

        private void SetupUI()
        {
            // إعدادات النموذج
            this.Text = "🔧 خيارات إنشاء النسخة الاحتياطية";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Size = new Size(480, 400);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.FromArgb(240, 240, 240);
            this.Padding = new Padding(20);
            this.Font = new Font("Segoe UI", 10);

            // =============================================
            // 1. العنوان
            // =============================================
            Label lblTitle = new Label
            {
                Text = "🔧 خيارات النسخة الاحتياطية",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Location = new Point(10, 10),
                Size = new Size(420, 35),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.FromArgb(44, 62, 80)
            };

            // =============================================
            // 2. خط فاصل
            // =============================================
            Panel line1 = new Panel
            {
                Location = new Point(10, 55),
                Size = new Size(420, 2),
                BackColor = Color.FromArgb(200, 200, 200)
            };

            // =============================================
            // 3. نوع النسخة
            // =============================================
            Label lblType = new Label
            {
                Text = "📋 نوع النسخة:",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Location = new Point(10, 75),
                Size = new Size(420, 30),
                ForeColor = Color.FromArgb(44, 62, 80)
            };

            ComboBox cmbType = new ComboBox
            {
                Name = "cmbType",
                Location = new Point(10, 110),
                Size = new Size(420, 30),
                Font = new Font("Segoe UI", 10),
                DropDownStyle = ComboBoxStyle.DropDownList,
                BackColor = Color.White
            };
            cmbType.Items.AddRange(new object[] {
                "✅ نسخة كاملة (قاعدة البيانات + الملفات)",
                "🗄️ نسخة قاعدة البيانات فقط",
                "📁 نسخة الملفات فقط"
            });
            cmbType.SelectedIndex = 0;

            // =============================================
            // 4. تضمين الملفات
            // =============================================
            CheckBox chkIncludeFiles = new CheckBox
            {
                Name = "chkIncludeFiles",
                Text = "📁 تضمين الملفات المشفرة",
                Font = new Font("Segoe UI", 11),
                Location = new Point(10, 160),
                Size = new Size(420, 30),
                Checked = true,
                ForeColor = Color.FromArgb(44, 62, 80)
            };

            // =============================================
            // 5. ضغط النسخة
            // =============================================
            CheckBox chkCompress = new CheckBox
            {
                Name = "chkCompress",
                Text = "📦 ضغط النسخة (ZIP)",
                Font = new Font("Segoe UI", 11),
                Location = new Point(10, 200),
                Size = new Size(420, 30),
                Checked = true,
                ForeColor = Color.FromArgb(44, 62, 80)
            };

            // =============================================
            // 6. معلومات إضافية
            // =============================================
            Label lblInfo = new Label
            {
                Text = "ℹ️ سيتم إنشاء نسخة احتياطية في مجلد النسخ الاحتياطية",
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Gray,
                Location = new Point(10, 245),
                Size = new Size(420, 30),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // =============================================
            // 7. خط فاصل
            // =============================================
            Panel line2 = new Panel
            {
                Location = new Point(10, 280),
                Size = new Size(420, 2),
                BackColor = Color.FromArgb(200, 200, 200)
            };

            // =============================================
            // 8. الأزرار
            // =============================================
            Button btnCreate = new Button
            {
                Text = "✅ إنشاء",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Location = new Point(180, 300),
                Size = new Size(120, 45),
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnCreate.FlatAppearance.BorderSize = 0;
            btnCreate.Click += (s, e) =>
            {
                // تعيين الخيارات المختارة
                string selected = cmbType.SelectedItem?.ToString() ?? "";

                if (selected.Contains("كاملة"))
                    SelectedType = "نسخة كاملة";
                else if (selected.Contains("قاعدة البيانات فقط"))
                    SelectedType = "قاعدة البيانات فقط";
                else if (selected.Contains("الملفات فقط"))
                    SelectedType = "الملفات فقط";

                IncludeFiles = chkIncludeFiles.Checked;
                Compress = chkCompress.Checked;

                this.DialogResult = DialogResult.OK;
                this.Close();
            };

            Button btnCancel = new Button
            {
                Text = "إلغاء",
                Font = new Font("Segoe UI", 11),
                Location = new Point(310, 300),
                Size = new Size(100, 45),
                BackColor = Color.FromArgb(149, 165, 166),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Click += (s, e) => this.Close();

            // =============================================
            // 9. تحديث حالة الـ CheckBoxes
            // =============================================
            cmbType.SelectedIndexChanged += (s, e) =>
            {
                string selected = cmbType.SelectedItem?.ToString() ?? "";
                bool isFull = selected.Contains("كاملة");
                bool isDbOnly = selected.Contains("قاعدة البيانات فقط");
                bool isFilesOnly = selected.Contains("الملفات فقط");

                chkIncludeFiles.Enabled = isFull || isFilesOnly;

                if (isFull)
                    chkIncludeFiles.Checked = true;
                else if (isFilesOnly)
                    chkIncludeFiles.Checked = true;
                else if (isDbOnly)
                    chkIncludeFiles.Checked = false;
            };

            // =============================================
            // 10. إضافة العناصر
            // =============================================
            this.Controls.AddRange(new Control[] {
                lblTitle,
                line1,
                lblType,
                cmbType,
                chkIncludeFiles,
                chkCompress,
                lblInfo,
                line2,
                btnCreate,
                btnCancel
            });
        }

        #endregion
    }
}