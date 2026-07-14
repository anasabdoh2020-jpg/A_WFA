using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace A_WFA
{
    public partial class FrmBackupSettings : Form
    {
        public FrmBackupSettings()
        {
            InitializeComponent1();
            LoadSettings();
        }

        private void InitializeComponent1()
        {
            this.Text = "⚙️ إعدادات النسخ الاحتياطي";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Size = new Size(500, 450);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.FromArgb(240, 240, 240);
            this.Padding = new Padding(20);

            // العنوان
            Label lblTitle = new Label
            {
                Text = "⚙️ إعدادات النسخ الاحتياطي",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Location = new Point(10, 10),
                Size = new Size(460, 30),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // خط فاصل
            Panel line1 = new Panel
            {
                Location = new Point(10, 50),
                Size = new Size(460, 2),
                BackColor = Color.FromArgb(200, 200, 200)
            };

            // مسار النسخ الاحتياطي
            Label lblBackupPath = new Label
            {
                Text = "📂 مسار حفظ النسخ الاحتياطية:",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(10, 70),
                Size = new Size(460, 25)
            };

            TextBox txtBackupPath = new TextBox
            {
                Name = "txtBackupPath",
                Location = new Point(10, 100),
                Size = new Size(370, 25),
                Font = new Font("Segoe UI", 10),
                ReadOnly = true,
                BackColor = Color.White
            };

            Button btnBrowsePath = new Button
            {
                Text = "📁",
                Location = new Point(390, 100),
                Size = new Size(70, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnBrowsePath.Click += (s, e) =>
            {
                using (var dialog = new FolderBrowserDialog())
                {
                    dialog.Description = "اختر مجلد حفظ النسخ الاحتياطية";
                    dialog.ShowNewFolderButton = true;
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        txtBackupPath.Text = dialog.SelectedPath;
                    }
                }
            };

            // النسخ الاحتياطي التلقائي
            CheckBox chkAutoBackup = new CheckBox
            {
                Name = "chkAutoBackup",
                Text = "🔄 تفعيل النسخ الاحتياطي التلقائي",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(10, 150),
                Size = new Size(460, 25),
                Checked = true
            };

            // فترة النسخ الاحتياطي
            Label lblInterval = new Label
            {
                Text = "⏰ فترة النسخ (بالساعات):",
                Font = new Font("Segoe UI", 10),
                Location = new Point(30, 185),
                Size = new Size(200, 25)
            };

            NumericUpDown numInterval = new NumericUpDown
            {
                Name = "numInterval",
                Location = new Point(240, 185),
                Size = new Size(80, 25),
                Minimum = 1,
                Maximum = 168,
                Value = 24,
                Font = new Font("Segoe UI", 10)
            };

            Label lblHours = new Label
            {
                Text = "ساعة",
                Font = new Font("Segoe UI", 10),
                Location = new Point(330, 185),
                Size = new Size(50, 25)
            };

            // عدد النسخ المحتفظ بها
            Label lblMaxBackups = new Label
            {
                Text = "📊 عدد النسخ الاحتفاظ بها:",
                Font = new Font("Segoe UI", 10),
                Location = new Point(30, 225),
                Size = new Size(200, 25)
            };

            NumericUpDown numMaxBackups = new NumericUpDown
            {
                Name = "numMaxBackups",
                Location = new Point(240, 225),
                Size = new Size(80, 25),
                Minimum = 1,
                Maximum = 100,
                Value = 10,
                Font = new Font("Segoe UI", 10)
            };

            Label lblCopies = new Label
            {
                Text = "نسخة",
                Font = new Font("Segoe UI", 10),
                Location = new Point(330, 225),
                Size = new Size(50, 25)
            };

            // ضغط النسخ
            CheckBox chkCompress = new CheckBox
            {
                Name = "chkCompress",
                Text = "📦 ضغط النسخ الاحتياطية (ZIP)",
                Font = new Font("Segoe UI", 10),
                Location = new Point(10, 265),
                Size = new Size(460, 25),
                Checked = true
            };

            // خط فاصل
            Panel line2 = new Panel
            {
                Location = new Point(10, 310),
                Size = new Size(460, 2),
                BackColor = Color.FromArgb(200, 200, 200)
            };

            // أزرار
            Button btnSave = new Button
            {
                Text = "💾 حفظ الإعدادات",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(200, 340),
                Size = new Size(120, 40),
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnSave.Click += (s, e) =>
            {
                try
                {
                    DatabaseManagerLite.SetSetting("BackupPath", txtBackupPath.Text);
                    DatabaseManagerLite.SetSetting("AutoBackup", chkAutoBackup.Checked.ToString());
                    DatabaseManagerLite.SetSetting("BackupInterval", numInterval.Value.ToString());
                    DatabaseManagerLite.SetSetting("MaxBackups", numMaxBackups.Value.ToString());
                    DatabaseManagerLite.SetSetting("CompressBackup", chkCompress.Checked.ToString());

                    MessageBox.Show("✅ تم حفظ الإعدادات بنجاح!", "نجاح",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"❌ خطأ في حفظ الإعدادات: {ex.Message}", "خطأ",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            Button btnCancel = new Button
            {
                Text = "إلغاء",
                Font = new Font("Segoe UI", 10),
                Location = new Point(330, 340),
                Size = new Size(100, 40),
                BackColor = Color.FromArgb(149, 165, 166),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnCancel.Click += (s, e) => this.Close();

            // إضافة العناصر
            this.Controls.AddRange(new Control[] {
                lblTitle, line1,
                lblBackupPath, txtBackupPath, btnBrowsePath,
                chkAutoBackup,
                lblInterval, numInterval, lblHours,
                lblMaxBackups, numMaxBackups, lblCopies,
                chkCompress,
                line2,
                btnSave, btnCancel
            });

            // حفظ المراجع
            this.Tag = new
            {
                txtBackupPath,
                chkAutoBackup,
                numInterval,
                numMaxBackups,
                chkCompress
            };
        }

        private void LoadSettings()
        {
            try
            {
                var controls = (dynamic)this.Tag;

                controls.txtBackupPath.Text = DatabaseManagerLite.GetSetting("BackupPath") ?? GetDefaultBackupPath();
                controls.chkAutoBackup.Checked = DatabaseManagerLite.GetSettingBool("AutoBackup", true);
                controls.numInterval.Value = DatabaseManagerLite.GetSettingInt("BackupInterval", 24);
                controls.numMaxBackups.Value = DatabaseManagerLite.GetSettingInt("MaxBackups", 10);
                controls.chkCompress.Checked = DatabaseManagerLite.GetSettingBool("CompressBackup", true);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"⚠️ خطأ في تحميل الإعدادات: {ex.Message}");
            }
        }

        private string GetDefaultBackupPath()
        {
            return Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "A_WFA",
                "Backups"
            );
        }
    }
}