namespace A_WFA
{
    partial class FrmSettings
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.TabControlSettings = new System.Windows.Forms.TabControl();
            this.TabDatabase = new System.Windows.Forms.TabPage();
            this.PanelDatabase = new System.Windows.Forms.Panel();
            this.LblDatabaseStatus = new System.Windows.Forms.Label();
            this.LblStatus = new System.Windows.Forms.Label();
            this.LblTablesCount = new System.Windows.Forms.Label();
            this.BtnReinitialize = new System.Windows.Forms.Button();
            this.BtnCreateTables = new System.Windows.Forms.Button();
            this.BtnSeedData = new System.Windows.Forms.Button();
            this.LblReinitializeWarning = new System.Windows.Forms.Label();
            this.TabBackup = new System.Windows.Forms.TabPage();
            this.PanelBackup = new System.Windows.Forms.Panel();
            this.LblBackupTitle = new System.Windows.Forms.Label();
            this.LblBackupInfo = new System.Windows.Forms.Label();
            this.BtnCreateBackup = new System.Windows.Forms.Button();
            this.BtnRestoreBackup = new System.Windows.Forms.Button();
            this.BtnDeleteBackups = new System.Windows.Forms.Button();
            this.LblBackupFiles = new System.Windows.Forms.Label();
            this.LstBackupFiles = new System.Windows.Forms.ListBox();
            this.TabTables = new System.Windows.Forms.TabPage();
            this.PanelTables = new System.Windows.Forms.Panel();
            this.LblTablesTitle = new System.Windows.Forms.Label();
            this.BtnRefreshTables = new System.Windows.Forms.Button();
            this.DgvTables = new System.Windows.Forms.DataGridView();
            this.TabControlSettings.SuspendLayout();
            this.TabDatabase.SuspendLayout();
            this.PanelDatabase.SuspendLayout();
            this.TabBackup.SuspendLayout();
            this.PanelBackup.SuspendLayout();
            this.TabTables.SuspendLayout();
            this.PanelTables.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvTables)).BeginInit();
            this.SuspendLayout();
            // 
            // TabControlSettings
            // 
            this.TabControlSettings.Controls.Add(this.TabDatabase);
            this.TabControlSettings.Controls.Add(this.TabBackup);
            this.TabControlSettings.Controls.Add(this.TabTables);
            this.TabControlSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabControlSettings.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.TabControlSettings.Location = new System.Drawing.Point(0, 0);
            this.TabControlSettings.Name = "TabControlSettings";
            this.TabControlSettings.SelectedIndex = 0;
            this.TabControlSettings.Size = new System.Drawing.Size(900, 650);
            this.TabControlSettings.TabIndex = 0;
            // 
            // TabDatabase
            // 
            this.TabDatabase.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.TabDatabase.Controls.Add(this.PanelDatabase);
            this.TabDatabase.Location = new System.Drawing.Point(4, 37);
            this.TabDatabase.Name = "TabDatabase";
            this.TabDatabase.Size = new System.Drawing.Size(892, 609);
            this.TabDatabase.TabIndex = 0;
            this.TabDatabase.Text = "🗄️ قاعدة البيانات";
            // 
            // PanelDatabase
            // 
            this.PanelDatabase.Controls.Add(this.LblDatabaseStatus);
            this.PanelDatabase.Controls.Add(this.LblStatus);
            this.PanelDatabase.Controls.Add(this.LblTablesCount);
            this.PanelDatabase.Controls.Add(this.BtnReinitialize);
            this.PanelDatabase.Controls.Add(this.BtnCreateTables);
            this.PanelDatabase.Controls.Add(this.BtnSeedData);
            this.PanelDatabase.Controls.Add(this.LblReinitializeWarning);
            this.PanelDatabase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelDatabase.Location = new System.Drawing.Point(0, 0);
            this.PanelDatabase.Name = "PanelDatabase";
            this.PanelDatabase.Padding = new System.Windows.Forms.Padding(20);
            this.PanelDatabase.Size = new System.Drawing.Size(892, 609);
            this.PanelDatabase.TabIndex = 0;
            // 
            // LblDatabaseStatus
            // 
            this.LblDatabaseStatus.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.LblDatabaseStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.LblDatabaseStatus.Location = new System.Drawing.Point(20, 20);
            this.LblDatabaseStatus.Name = "LblDatabaseStatus";
            this.LblDatabaseStatus.Size = new System.Drawing.Size(400, 30);
            this.LblDatabaseStatus.TabIndex = 0;
            this.LblDatabaseStatus.Text = "📊 حالة قاعدة البيانات";
            // 
            // LblStatus
            // 
            this.LblStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.LblStatus.ForeColor = System.Drawing.Color.Gray;
            this.LblStatus.Location = new System.Drawing.Point(20, 60);
            this.LblStatus.Name = "LblStatus";
            this.LblStatus.Size = new System.Drawing.Size(400, 25);
            this.LblStatus.TabIndex = 1;
            this.LblStatus.Text = "الحالة: جاري التحقق...";
            // 
            // LblTablesCount
            // 
            this.LblTablesCount.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.LblTablesCount.ForeColor = System.Drawing.Color.Gray;
            this.LblTablesCount.Location = new System.Drawing.Point(20, 90);
            this.LblTablesCount.Name = "LblTablesCount";
            this.LblTablesCount.Size = new System.Drawing.Size(400, 25);
            this.LblTablesCount.TabIndex = 2;
            this.LblTablesCount.Text = "عدد الجداول: 0";
            // 
            // BtnReinitialize
            // 
            this.BtnReinitialize.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.BtnReinitialize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnReinitialize.FlatAppearance.BorderSize = 0;
            this.BtnReinitialize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnReinitialize.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.BtnReinitialize.ForeColor = System.Drawing.Color.White;
            this.BtnReinitialize.Location = new System.Drawing.Point(20, 140);
            this.BtnReinitialize.Name = "BtnReinitialize";
            this.BtnReinitialize.Size = new System.Drawing.Size(250, 50);
            this.BtnReinitialize.TabIndex = 0;
            this.BtnReinitialize.Text = "🔄 إعادة تهيئة قاعدة البيانات";
            this.BtnReinitialize.UseVisualStyleBackColor = false;
            // 
            // BtnCreateTables
            // 
            this.BtnCreateTables.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.BtnCreateTables.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnCreateTables.FlatAppearance.BorderSize = 0;
            this.BtnCreateTables.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnCreateTables.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.BtnCreateTables.ForeColor = System.Drawing.Color.White;
            this.BtnCreateTables.Location = new System.Drawing.Point(290, 140);
            this.BtnCreateTables.Name = "BtnCreateTables";
            this.BtnCreateTables.Size = new System.Drawing.Size(250, 50);
            this.BtnCreateTables.TabIndex = 1;
            this.BtnCreateTables.Text = "📦 إنشاء الجداول فقط";
            this.BtnCreateTables.UseVisualStyleBackColor = false;
            // 
            // BtnSeedData
            // 
            this.BtnSeedData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.BtnSeedData.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnSeedData.FlatAppearance.BorderSize = 0;
            this.BtnSeedData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnSeedData.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.BtnSeedData.ForeColor = System.Drawing.Color.White;
            this.BtnSeedData.Location = new System.Drawing.Point(560, 140);
            this.BtnSeedData.Name = "BtnSeedData";
            this.BtnSeedData.Size = new System.Drawing.Size(250, 50);
            this.BtnSeedData.TabIndex = 2;
            this.BtnSeedData.Text = "🌱 إدراج بيانات افتراضية";
            this.BtnSeedData.UseVisualStyleBackColor = false;
            // 
            // LblReinitializeWarning
            // 
            this.LblReinitializeWarning.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.LblReinitializeWarning.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(57)))), ((int)(((byte)(43)))));
            this.LblReinitializeWarning.Location = new System.Drawing.Point(20, 200);
            this.LblReinitializeWarning.Name = "LblReinitializeWarning";
            this.LblReinitializeWarning.Size = new System.Drawing.Size(800, 25);
            this.LblReinitializeWarning.TabIndex = 3;
            this.LblReinitializeWarning.Text = "⚠️ تحذير: إعادة التهيئة ستؤدي إلى حذف جميع البيانات الموجودة!";
            // 
            // TabBackup
            // 
            this.TabBackup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.TabBackup.Controls.Add(this.PanelBackup);
            this.TabBackup.Location = new System.Drawing.Point(4, 37);
            this.TabBackup.Name = "TabBackup";
            this.TabBackup.Size = new System.Drawing.Size(192, 59);
            this.TabBackup.TabIndex = 1;
            this.TabBackup.Text = "💾 النسخ الاحتياطي";
            // 
            // PanelBackup
            // 
            this.PanelBackup.Controls.Add(this.LblBackupTitle);
            this.PanelBackup.Controls.Add(this.LblBackupInfo);
            this.PanelBackup.Controls.Add(this.BtnCreateBackup);
            this.PanelBackup.Controls.Add(this.BtnRestoreBackup);
            this.PanelBackup.Controls.Add(this.BtnDeleteBackups);
            this.PanelBackup.Controls.Add(this.LblBackupFiles);
            this.PanelBackup.Controls.Add(this.LstBackupFiles);
            this.PanelBackup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelBackup.Location = new System.Drawing.Point(0, 0);
            this.PanelBackup.Name = "PanelBackup";
            this.PanelBackup.Padding = new System.Windows.Forms.Padding(20);
            this.PanelBackup.Size = new System.Drawing.Size(192, 59);
            this.PanelBackup.TabIndex = 0;
            // 
            // LblBackupTitle
            // 
            this.LblBackupTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.LblBackupTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.LblBackupTitle.Location = new System.Drawing.Point(20, 20);
            this.LblBackupTitle.Name = "LblBackupTitle";
            this.LblBackupTitle.Size = new System.Drawing.Size(400, 30);
            this.LblBackupTitle.TabIndex = 0;
            this.LblBackupTitle.Text = "💾 إدارة النسخ الاحتياطي";
            // 
            // LblBackupInfo
            // 
            this.LblBackupInfo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.LblBackupInfo.ForeColor = System.Drawing.Color.Gray;
            this.LblBackupInfo.Location = new System.Drawing.Point(20, 60);
            this.LblBackupInfo.Name = "LblBackupInfo";
            this.LblBackupInfo.Size = new System.Drawing.Size(600, 25);
            this.LblBackupInfo.TabIndex = 1;
            this.LblBackupInfo.Text = "قم بإنشاء نسخة احتياطية كاملة من قاعدة البيانات، أو استعادة نسخة سابقة.";
            // 
            // BtnCreateBackup
            // 
            this.BtnCreateBackup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.BtnCreateBackup.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnCreateBackup.FlatAppearance.BorderSize = 0;
            this.BtnCreateBackup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnCreateBackup.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.BtnCreateBackup.ForeColor = System.Drawing.Color.White;
            this.BtnCreateBackup.Location = new System.Drawing.Point(20, 110);
            this.BtnCreateBackup.Name = "BtnCreateBackup";
            this.BtnCreateBackup.Size = new System.Drawing.Size(250, 60);
            this.BtnCreateBackup.TabIndex = 0;
            this.BtnCreateBackup.Text = "📀 إنشاء نسخة احتياطية";
            this.BtnCreateBackup.UseVisualStyleBackColor = false;
            // 
            // BtnRestoreBackup
            // 
            this.BtnRestoreBackup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.BtnRestoreBackup.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnRestoreBackup.FlatAppearance.BorderSize = 0;
            this.BtnRestoreBackup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnRestoreBackup.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.BtnRestoreBackup.ForeColor = System.Drawing.Color.White;
            this.BtnRestoreBackup.Location = new System.Drawing.Point(290, 110);
            this.BtnRestoreBackup.Name = "BtnRestoreBackup";
            this.BtnRestoreBackup.Size = new System.Drawing.Size(250, 60);
            this.BtnRestoreBackup.TabIndex = 1;
            this.BtnRestoreBackup.Text = "📂 استعادة نسخة احتياطية";
            this.BtnRestoreBackup.UseVisualStyleBackColor = false;
            // 
            // BtnDeleteBackups
            // 
            this.BtnDeleteBackups.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.BtnDeleteBackups.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnDeleteBackups.FlatAppearance.BorderSize = 0;
            this.BtnDeleteBackups.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnDeleteBackups.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.BtnDeleteBackups.ForeColor = System.Drawing.Color.White;
            this.BtnDeleteBackups.Location = new System.Drawing.Point(560, 110);
            this.BtnDeleteBackups.Name = "BtnDeleteBackups";
            this.BtnDeleteBackups.Size = new System.Drawing.Size(250, 60);
            this.BtnDeleteBackups.TabIndex = 2;
            this.BtnDeleteBackups.Text = "🗑️ حذف النسخ القديمة";
            this.BtnDeleteBackups.UseVisualStyleBackColor = false;
            // 
            // LblBackupFiles
            // 
            this.LblBackupFiles.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.LblBackupFiles.Location = new System.Drawing.Point(20, 190);
            this.LblBackupFiles.Name = "LblBackupFiles";
            this.LblBackupFiles.Size = new System.Drawing.Size(400, 25);
            this.LblBackupFiles.TabIndex = 3;
            this.LblBackupFiles.Text = "📋 ملفات النسخ الاحتياطي المتوفرة:";
            // 
            // LstBackupFiles
            // 
            this.LstBackupFiles.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.LstBackupFiles.ItemHeight = 28;
            this.LstBackupFiles.Location = new System.Drawing.Point(20, 220);
            this.LstBackupFiles.Name = "LstBackupFiles";
            this.LstBackupFiles.Size = new System.Drawing.Size(800, 284);
            this.LstBackupFiles.TabIndex = 3;
            // 
            // TabTables
            // 
            this.TabTables.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.TabTables.Controls.Add(this.PanelTables);
            this.TabTables.Location = new System.Drawing.Point(4, 37);
            this.TabTables.Name = "TabTables";
            this.TabTables.Size = new System.Drawing.Size(192, 59);
            this.TabTables.TabIndex = 2;
            this.TabTables.Text = "📊 الجداول";
            // 
            // PanelTables
            // 
            this.PanelTables.Controls.Add(this.LblTablesTitle);
            this.PanelTables.Controls.Add(this.BtnRefreshTables);
            this.PanelTables.Controls.Add(this.DgvTables);
            this.PanelTables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelTables.Location = new System.Drawing.Point(0, 0);
            this.PanelTables.Name = "PanelTables";
            this.PanelTables.Padding = new System.Windows.Forms.Padding(20);
            this.PanelTables.Size = new System.Drawing.Size(192, 59);
            this.PanelTables.TabIndex = 0;
            // 
            // LblTablesTitle
            // 
            this.LblTablesTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.LblTablesTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.LblTablesTitle.Location = new System.Drawing.Point(20, 20);
            this.LblTablesTitle.Name = "LblTablesTitle";
            this.LblTablesTitle.Size = new System.Drawing.Size(400, 30);
            this.LblTablesTitle.TabIndex = 0;
            this.LblTablesTitle.Text = "📊 هيكل قاعدة البيانات";
            // 
            // BtnRefreshTables
            // 
            this.BtnRefreshTables.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.BtnRefreshTables.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnRefreshTables.FlatAppearance.BorderSize = 0;
            this.BtnRefreshTables.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnRefreshTables.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.BtnRefreshTables.ForeColor = System.Drawing.Color.White;
            this.BtnRefreshTables.Location = new System.Drawing.Point(720, 20);
            this.BtnRefreshTables.Name = "BtnRefreshTables";
            this.BtnRefreshTables.Size = new System.Drawing.Size(100, 35);
            this.BtnRefreshTables.TabIndex = 0;
            this.BtnRefreshTables.Text = "🔄 تحديث";
            this.BtnRefreshTables.UseVisualStyleBackColor = false;
            // 
            // DgvTables
            // 
            this.DgvTables.AllowUserToAddRows = false;
            this.DgvTables.AllowUserToDeleteRows = false;
            this.DgvTables.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DgvTables.BackgroundColor = System.Drawing.Color.White;
            this.DgvTables.ColumnHeadersHeight = 34;
            this.DgvTables.Location = new System.Drawing.Point(20, 70);
            this.DgvTables.Name = "DgvTables";
            this.DgvTables.ReadOnly = true;
            this.DgvTables.RowHeadersVisible = false;
            this.DgvTables.RowHeadersWidth = 62;
            this.DgvTables.Size = new System.Drawing.Size(800, 450);
            this.DgvTables.TabIndex = 1;
            // 
            // FrmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 650);
            this.Controls.Add(this.TabControlSettings);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.MinimumSize = new System.Drawing.Size(900, 650);
            this.Name = "FrmSettings";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "⚙️ إعدادات النظام";
            this.TabControlSettings.ResumeLayout(false);
            this.TabDatabase.ResumeLayout(false);
            this.PanelDatabase.ResumeLayout(false);
            this.TabBackup.ResumeLayout(false);
            this.PanelBackup.ResumeLayout(false);
            this.TabTables.ResumeLayout(false);
            this.PanelTables.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DgvTables)).EndInit();
            this.ResumeLayout(false);

        }

        #region "عناصر التحكم"

        // تبويب قاعدة البيانات
        private System.Windows.Forms.TabControl TabControlSettings;
        private System.Windows.Forms.TabPage TabDatabase;
        private System.Windows.Forms.TabPage TabBackup;
        private System.Windows.Forms.TabPage TabTables;
        private System.Windows.Forms.Panel PanelDatabase;
        private System.Windows.Forms.Panel PanelBackup;
        private System.Windows.Forms.Panel PanelTables;

        // تبويب قاعدة البيانات
        private System.Windows.Forms.Label LblDatabaseStatus;
        private System.Windows.Forms.Label LblStatus;
        private System.Windows.Forms.Label LblTablesCount;
        private System.Windows.Forms.Button BtnReinitialize;
        private System.Windows.Forms.Button BtnCreateTables;
        private System.Windows.Forms.Button BtnSeedData;
        private System.Windows.Forms.Label LblReinitializeWarning;

        // تبويب النسخ الاحتياطي
        private System.Windows.Forms.Label LblBackupTitle;
        private System.Windows.Forms.Label LblBackupInfo;
        private System.Windows.Forms.Button BtnCreateBackup;
        private System.Windows.Forms.Button BtnRestoreBackup;
        private System.Windows.Forms.Button BtnDeleteBackups;
        private System.Windows.Forms.Label LblBackupFiles;
        private System.Windows.Forms.ListBox LstBackupFiles;

        // تبويب الجداول
        private System.Windows.Forms.Label LblTablesTitle;
        private System.Windows.Forms.Button BtnRefreshTables;
        private System.Windows.Forms.DataGridView DgvTables;

        #endregion
    }
}