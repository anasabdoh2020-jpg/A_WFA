namespace A_WFA
{
    partial class FrmBackupManager
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel PanelHeader;
        private System.Windows.Forms.Label LblTitle;
        private System.Windows.Forms.PictureBox PicIcon;
        private System.Windows.Forms.Panel PanelMain;
        private System.Windows.Forms.Panel PanelControls;
        private System.Windows.Forms.Button BtnCreateBackup;
        private System.Windows.Forms.Button BtnRestoreBackup;
        private System.Windows.Forms.Button BtnExportBackup;
        private System.Windows.Forms.Button BtnImportBackup;
        private System.Windows.Forms.Button BtnDeleteBackup;
        private System.Windows.Forms.Button BtnRefresh;
        private System.Windows.Forms.Button BtnOpenFolder;
        private System.Windows.Forms.Button BtnSettings;
        private System.Windows.Forms.DataGridView DgvBackups;
        private System.Windows.Forms.Panel PanelFooter;
        private System.Windows.Forms.Label LblTotalBackups;
        private System.Windows.Forms.Label LblTotalSize;
        private System.Windows.Forms.Label LblCount;
        private System.Windows.Forms.Label LblSelectedInfo;
        private System.Windows.Forms.TextBox TxtSearch;
        private System.Windows.Forms.Label LblSearch;
        private System.Windows.Forms.ComboBox CmbFilter;
        private System.Windows.Forms.Label LblFilter;
        private System.Windows.Forms.Panel PanelSearch;
        private System.Windows.Forms.ToolTip ToolTip1;

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
            this.components = new System.ComponentModel.Container();
            this.PanelHeader = new System.Windows.Forms.Panel();
            this.PicIcon = new System.Windows.Forms.PictureBox();
            this.LblTitle = new System.Windows.Forms.Label();
            this.PanelMain = new System.Windows.Forms.Panel();
            this.PanelControls = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.BtnCreateBackup = new System.Windows.Forms.Button();
            this.BtnRestoreBackup = new System.Windows.Forms.Button();
            this.BtnExportBackup = new System.Windows.Forms.Button();
            this.BtnImportBackup = new System.Windows.Forms.Button();
            this.BtnDeleteBackup = new System.Windows.Forms.Button();
            this.BtnRefresh = new System.Windows.Forms.Button();
            this.BtnOpenFolder = new System.Windows.Forms.Button();
            this.BtnSettings = new System.Windows.Forms.Button();
            this.PanelSearch = new System.Windows.Forms.Panel();
            this.LblSearch = new System.Windows.Forms.Label();
            this.TxtSearch = new System.Windows.Forms.TextBox();
            this.LblFilter = new System.Windows.Forms.Label();
            this.CmbFilter = new System.Windows.Forms.ComboBox();
            this.DgvBackups = new System.Windows.Forms.DataGridView();
            this.PanelFooter = new System.Windows.Forms.Panel();
            this.LblSelectedInfo = new System.Windows.Forms.Label();
            this.LblTotalSize = new System.Windows.Forms.Label();
            this.LblTotalBackups = new System.Windows.Forms.Label();
            this.LblCount = new System.Windows.Forms.Label();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.PanelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicIcon)).BeginInit();
            this.PanelControls.SuspendLayout();
            this.PanelSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvBackups)).BeginInit();
            this.PanelFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelHeader
            // 
            this.PanelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.PanelHeader.Controls.Add(this.PicIcon);
            this.PanelHeader.Controls.Add(this.LblTitle);
            this.PanelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelHeader.Location = new System.Drawing.Point(0, 0);
            this.PanelHeader.Name = "PanelHeader";
            this.PanelHeader.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.PanelHeader.Size = new System.Drawing.Size(1096, 70);
            this.PanelHeader.TabIndex = 4;
            // 
            // PicIcon
            // 
            this.PicIcon.BackColor = System.Drawing.Color.Transparent;
            this.PicIcon.Location = new System.Drawing.Point(20, 15);
            this.PicIcon.Name = "PicIcon";
            this.PicIcon.Size = new System.Drawing.Size(40, 40);
            this.PicIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PicIcon.TabIndex = 0;
            this.PicIcon.TabStop = false;
            // 
            // LblTitle
            // 
            this.LblTitle.AutoSize = true;
            this.LblTitle.BackColor = System.Drawing.Color.Transparent;
            this.LblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.LblTitle.ForeColor = System.Drawing.Color.White;
            this.LblTitle.Location = new System.Drawing.Point(70, 18);
            this.LblTitle.Name = "LblTitle";
            this.LblTitle.Size = new System.Drawing.Size(382, 48);
            this.LblTitle.TabIndex = 1;
            this.LblTitle.Text = "🗄️ إدارة النسخ الاحتياطية";
            this.LblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PanelMain
            // 
            this.PanelMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.PanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelMain.Location = new System.Drawing.Point(0, 70);
            this.PanelMain.Name = "PanelMain";
            this.PanelMain.Padding = new System.Windows.Forms.Padding(10);
            this.PanelMain.Size = new System.Drawing.Size(1096, 475);
            this.PanelMain.TabIndex = 3;
            // 
            // PanelControls
            // 
            this.PanelControls.BackColor = System.Drawing.Color.White;
            this.PanelControls.Controls.Add(this.button1);
            this.PanelControls.Controls.Add(this.BtnCreateBackup);
            this.PanelControls.Controls.Add(this.BtnRestoreBackup);
            this.PanelControls.Controls.Add(this.BtnExportBackup);
            this.PanelControls.Controls.Add(this.BtnImportBackup);
            this.PanelControls.Controls.Add(this.BtnDeleteBackup);
            this.PanelControls.Controls.Add(this.BtnRefresh);
            this.PanelControls.Controls.Add(this.BtnOpenFolder);
            this.PanelControls.Controls.Add(this.BtnSettings);
            this.PanelControls.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelControls.Location = new System.Drawing.Point(0, 70);
            this.PanelControls.Name = "PanelControls";
            this.PanelControls.Padding = new System.Windows.Forms.Padding(5);
            this.PanelControls.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.PanelControls.Size = new System.Drawing.Size(1096, 60);
            this.PanelControls.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(30, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(130, 45);
            this.button1.TabIndex = 8;
            this.button1.Text = "➕ إنشاء نسخة";
            this.ToolTip1.SetToolTip(this.button1, "إنشاء نسخة احتياطية جديدة");
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // BtnCreateBackup
            // 
            this.BtnCreateBackup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.BtnCreateBackup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnCreateBackup.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.BtnCreateBackup.ForeColor = System.Drawing.Color.White;
            this.BtnCreateBackup.Location = new System.Drawing.Point(194, 7);
            this.BtnCreateBackup.Name = "BtnCreateBackup";
            this.BtnCreateBackup.Size = new System.Drawing.Size(130, 45);
            this.BtnCreateBackup.TabIndex = 0;
            this.BtnCreateBackup.Text = "➕ إنشاء نسخة";
            this.ToolTip1.SetToolTip(this.BtnCreateBackup, "إنشاء نسخة احتياطية جديدة");
            this.BtnCreateBackup.UseVisualStyleBackColor = false;
            // 
            // BtnRestoreBackup
            // 
            this.BtnRestoreBackup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.BtnRestoreBackup.Enabled = false;
            this.BtnRestoreBackup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnRestoreBackup.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.BtnRestoreBackup.ForeColor = System.Drawing.Color.White;
            this.BtnRestoreBackup.Location = new System.Drawing.Point(334, 7);
            this.BtnRestoreBackup.Name = "BtnRestoreBackup";
            this.BtnRestoreBackup.Size = new System.Drawing.Size(120, 45);
            this.BtnRestoreBackup.TabIndex = 1;
            this.BtnRestoreBackup.Text = "↩️ استعادة";
            this.ToolTip1.SetToolTip(this.BtnRestoreBackup, "استعادة البيانات من نسخة محددة");
            this.BtnRestoreBackup.UseVisualStyleBackColor = false;
            // 
            // BtnExportBackup
            // 
            this.BtnExportBackup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(196)))), ((int)(((byte)(15)))));
            this.BtnExportBackup.Enabled = false;
            this.BtnExportBackup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnExportBackup.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.BtnExportBackup.ForeColor = System.Drawing.Color.White;
            this.BtnExportBackup.Location = new System.Drawing.Point(464, 7);
            this.BtnExportBackup.Name = "BtnExportBackup";
            this.BtnExportBackup.Size = new System.Drawing.Size(110, 45);
            this.BtnExportBackup.TabIndex = 2;
            this.BtnExportBackup.Text = "📤 تصدير";
            this.ToolTip1.SetToolTip(this.BtnExportBackup, "تصدير النسخة المحددة إلى ملف");
            this.BtnExportBackup.UseVisualStyleBackColor = false;
            // 
            // BtnImportBackup
            // 
            this.BtnImportBackup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(89)))), ((int)(((byte)(182)))));
            this.BtnImportBackup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnImportBackup.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.BtnImportBackup.ForeColor = System.Drawing.Color.White;
            this.BtnImportBackup.Location = new System.Drawing.Point(584, 7);
            this.BtnImportBackup.Name = "BtnImportBackup";
            this.BtnImportBackup.Size = new System.Drawing.Size(110, 45);
            this.BtnImportBackup.TabIndex = 3;
            this.BtnImportBackup.Text = "📥 استيراد";
            this.ToolTip1.SetToolTip(this.BtnImportBackup, "استيراد نسخة من ملف خارجي");
            this.BtnImportBackup.UseVisualStyleBackColor = false;
            // 
            // BtnDeleteBackup
            // 
            this.BtnDeleteBackup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.BtnDeleteBackup.Enabled = false;
            this.BtnDeleteBackup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnDeleteBackup.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.BtnDeleteBackup.ForeColor = System.Drawing.Color.White;
            this.BtnDeleteBackup.Location = new System.Drawing.Point(704, 7);
            this.BtnDeleteBackup.Name = "BtnDeleteBackup";
            this.BtnDeleteBackup.Size = new System.Drawing.Size(100, 45);
            this.BtnDeleteBackup.TabIndex = 4;
            this.BtnDeleteBackup.Text = "🗑️ حذف";
            this.ToolTip1.SetToolTip(this.BtnDeleteBackup, "حذف النسخة المحددة");
            this.BtnDeleteBackup.UseVisualStyleBackColor = false;
            // 
            // BtnRefresh
            // 
            this.BtnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.BtnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnRefresh.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.BtnRefresh.ForeColor = System.Drawing.Color.White;
            this.BtnRefresh.Location = new System.Drawing.Point(814, 7);
            this.BtnRefresh.Name = "BtnRefresh";
            this.BtnRefresh.Size = new System.Drawing.Size(80, 45);
            this.BtnRefresh.TabIndex = 5;
            this.BtnRefresh.Text = "🔄 تحديث";
            this.ToolTip1.SetToolTip(this.BtnRefresh, "تحديث قائمة النسخ");
            this.BtnRefresh.UseVisualStyleBackColor = false;
            // 
            // BtnOpenFolder
            // 
            this.BtnOpenFolder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.BtnOpenFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnOpenFolder.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.BtnOpenFolder.ForeColor = System.Drawing.Color.White;
            this.BtnOpenFolder.Location = new System.Drawing.Point(904, 7);
            this.BtnOpenFolder.Name = "BtnOpenFolder";
            this.BtnOpenFolder.Size = new System.Drawing.Size(90, 45);
            this.BtnOpenFolder.TabIndex = 6;
            this.BtnOpenFolder.Text = "📂 فتح";
            this.ToolTip1.SetToolTip(this.BtnOpenFolder, "فتح مجلد النسخ الاحتياطية");
            this.BtnOpenFolder.UseVisualStyleBackColor = false;
            // 
            // BtnSettings
            // 
            this.BtnSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.BtnSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnSettings.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.BtnSettings.ForeColor = System.Drawing.Color.White;
            this.BtnSettings.Location = new System.Drawing.Point(1004, 7);
            this.BtnSettings.Name = "BtnSettings";
            this.BtnSettings.Size = new System.Drawing.Size(80, 45);
            this.BtnSettings.TabIndex = 7;
            this.BtnSettings.Text = "⚙️";
            this.ToolTip1.SetToolTip(this.BtnSettings, "إعدادات النسخ الاحتياطي");
            this.BtnSettings.UseVisualStyleBackColor = false;
            // 
            // PanelSearch
            // 
            this.PanelSearch.BackColor = System.Drawing.Color.White;
            this.PanelSearch.Controls.Add(this.LblSearch);
            this.PanelSearch.Controls.Add(this.TxtSearch);
            this.PanelSearch.Controls.Add(this.LblFilter);
            this.PanelSearch.Controls.Add(this.CmbFilter);
            this.PanelSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelSearch.Location = new System.Drawing.Point(0, 130);
            this.PanelSearch.Name = "PanelSearch";
            this.PanelSearch.Padding = new System.Windows.Forms.Padding(5);
            this.PanelSearch.Size = new System.Drawing.Size(1096, 45);
            this.PanelSearch.TabIndex = 1;
            // 
            // LblSearch
            // 
            this.LblSearch.AutoSize = true;
            this.LblSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.LblSearch.Location = new System.Drawing.Point(985, 8);
            this.LblSearch.Name = "LblSearch";
            this.LblSearch.Size = new System.Drawing.Size(85, 28);
            this.LblSearch.TabIndex = 0;
            this.LblSearch.Text = "🔍 بحث:";
            // 
            // TxtSearch
            // 
            this.TxtSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.TxtSearch.Location = new System.Drawing.Point(531, 5);
            this.TxtSearch.Name = "TxtSearch";
            this.TxtSearch.Size = new System.Drawing.Size(435, 34);
            this.TxtSearch.TabIndex = 1;
            this.ToolTip1.SetToolTip(this.TxtSearch, "ابحث باسم النسخة");
            // 
            // LblFilter
            // 
            this.LblFilter.AutoSize = true;
            this.LblFilter.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.LblFilter.Location = new System.Drawing.Point(395, 8);
            this.LblFilter.Name = "LblFilter";
            this.LblFilter.Size = new System.Drawing.Size(98, 28);
            this.LblFilter.TabIndex = 2;
            this.LblFilter.Text = "📋 تصفية:";
            // 
            // CmbFilter
            // 
            this.CmbFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbFilter.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.CmbFilter.Items.AddRange(new object[] {
            "الكل",
            "النسخ الكاملة",
            "النسخ الجزئية",
            "المضغوطة"});
            this.CmbFilter.Location = new System.Drawing.Point(225, 3);
            this.CmbFilter.Name = "CmbFilter";
            this.CmbFilter.Size = new System.Drawing.Size(150, 36);
            this.CmbFilter.TabIndex = 3;
            // 
            // DgvBackups
            // 
            this.DgvBackups.BackgroundColor = System.Drawing.Color.White;
            this.DgvBackups.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvBackups.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DgvBackups.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.DgvBackups.Location = new System.Drawing.Point(0, 175);
            this.DgvBackups.Name = "DgvBackups";
            this.DgvBackups.RowHeadersVisible = false;
            this.DgvBackups.RowHeadersWidth = 62;
            this.DgvBackups.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvBackups.Size = new System.Drawing.Size(1096, 370);
            this.DgvBackups.TabIndex = 0;
            // 
            // PanelFooter
            // 
            this.PanelFooter.BackColor = System.Drawing.Color.White;
            this.PanelFooter.Controls.Add(this.LblSelectedInfo);
            this.PanelFooter.Controls.Add(this.LblTotalSize);
            this.PanelFooter.Controls.Add(this.LblTotalBackups);
            this.PanelFooter.Controls.Add(this.LblCount);
            this.PanelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PanelFooter.Location = new System.Drawing.Point(0, 545);
            this.PanelFooter.Name = "PanelFooter";
            this.PanelFooter.Padding = new System.Windows.Forms.Padding(10);
            this.PanelFooter.Size = new System.Drawing.Size(1096, 50);
            this.PanelFooter.TabIndex = 5;
            // 
            // LblSelectedInfo
            // 
            this.LblSelectedInfo.AutoSize = true;
            this.LblSelectedInfo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.LblSelectedInfo.ForeColor = System.Drawing.Color.Gray;
            this.LblSelectedInfo.Location = new System.Drawing.Point(350, 13);
            this.LblSelectedInfo.Name = "LblSelectedInfo";
            this.LblSelectedInfo.Size = new System.Drawing.Size(179, 25);
            this.LblSelectedInfo.TabIndex = 0;
            this.LblSelectedInfo.Text = "❌ لم يتم اختيار نسخة";
            // 
            // LblTotalSize
            // 
            this.LblTotalSize.AutoSize = true;
            this.LblTotalSize.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.LblTotalSize.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.LblTotalSize.Location = new System.Drawing.Point(201, 10);
            this.LblTotalSize.Name = "LblTotalSize";
            this.LblTotalSize.Size = new System.Drawing.Size(57, 28);
            this.LblTotalSize.TabIndex = 1;
            this.LblTotalSize.Text = "💾 0";
            // 
            // LblTotalBackups
            // 
            this.LblTotalBackups.AutoSize = true;
            this.LblTotalBackups.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.LblTotalBackups.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.LblTotalBackups.Location = new System.Drawing.Point(10, 10);
            this.LblTotalBackups.Name = "LblTotalBackups";
            this.LblTotalBackups.Size = new System.Drawing.Size(99, 28);
            this.LblTotalBackups.TabIndex = 2;
            this.LblTotalBackups.Text = "📊 0 نسخ";
            // 
            // LblCount
            // 
            this.LblCount.AutoSize = true;
            this.LblCount.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.LblCount.ForeColor = System.Drawing.Color.Gray;
            this.LblCount.Location = new System.Drawing.Point(925, 15);
            this.LblCount.Name = "LblCount";
            this.LblCount.Size = new System.Drawing.Size(134, 25);
            this.LblCount.TabIndex = 3;
            this.LblCount.Text = "📊 عدد النسخ: 0";
            // 
            // ToolTip1
            // 
            this.ToolTip1.IsBalloon = true;
            this.ToolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.ToolTip1.ToolTipTitle = "معلومة";
            // 
            // FrmBackupManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1096, 595);
            this.Controls.Add(this.DgvBackups);
            this.Controls.Add(this.PanelSearch);
            this.Controls.Add(this.PanelControls);
            this.Controls.Add(this.PanelMain);
            this.Controls.Add(this.PanelHeader);
            this.Controls.Add(this.PanelFooter);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(900, 500);
            this.Name = "FrmBackupManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "🗄️ إدارة النسخ الاحتياطية";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.PanelHeader.ResumeLayout(false);
            this.PanelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicIcon)).EndInit();
            this.PanelControls.ResumeLayout(false);
            this.PanelSearch.ResumeLayout(false);
            this.PanelSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvBackups)).EndInit();
            this.PanelFooter.ResumeLayout(false);
            this.PanelFooter.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Button button1;
    }
}