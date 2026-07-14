namespace A_WFA
{
    partial class FrmBoxContent
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
            this.PanelMain = new System.Windows.Forms.Panel();
            this.LblBoxTitle = new System.Windows.Forms.Label();
            this.PanelInfo = new System.Windows.Forms.Panel();
            this.LblArchiveNumber = new System.Windows.Forms.Label();
            this.LblArchiveValue = new System.Windows.Forms.Label();
            this.LblDocCount = new System.Windows.Forms.Label();
            this.LblDocCountValue = new System.Windows.Forms.Label();
            this.LblStatus = new System.Windows.Forms.Label();
            this.LblStatusValue = new System.Windows.Forms.Label();
            this.PanelToolbar = new System.Windows.Forms.Panel();
            this.BtnAddDocument = new System.Windows.Forms.Button();
            this.BtnRefresh = new System.Windows.Forms.Button();
            this.BtnDeleteSelected = new System.Windows.Forms.Button();
            this.BtnExport = new System.Windows.Forms.Button();
            this.TxtSearch = new System.Windows.Forms.TextBox();
            this.CmbFilter = new System.Windows.Forms.ComboBox();
            this.DgvDocuments = new System.Windows.Forms.DataGridView();
            this.PanelStatus = new System.Windows.Forms.Panel();
            this.LblStatusBar = new System.Windows.Forms.Label();
            this.LblRecordCount = new System.Windows.Forms.Label();
            this.PanelMain.SuspendLayout();
            this.PanelInfo.SuspendLayout();
            this.PanelToolbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvDocuments)).BeginInit();
            this.PanelStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelMain
            // 
            this.PanelMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.PanelMain.Controls.Add(this.LblBoxTitle);
            this.PanelMain.Controls.Add(this.PanelInfo);
            this.PanelMain.Controls.Add(this.PanelToolbar);
            this.PanelMain.Controls.Add(this.DgvDocuments);
            this.PanelMain.Controls.Add(this.PanelStatus);
            this.PanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelMain.Location = new System.Drawing.Point(0, 0);
            this.PanelMain.Name = "PanelMain";
            this.PanelMain.Padding = new System.Windows.Forms.Padding(20);
            this.PanelMain.Size = new System.Drawing.Size(1060, 730);
            this.PanelMain.TabIndex = 0;
            // 
            // LblBoxTitle
            // 
            this.LblBoxTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.LblBoxTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.LblBoxTitle.Location = new System.Drawing.Point(20, 0);
            this.LblBoxTitle.Name = "LblBoxTitle";
            this.LblBoxTitle.Size = new System.Drawing.Size(1010, 62);
            this.LblBoxTitle.TabIndex = 0;
            this.LblBoxTitle.Text = "📦 محتويات الصندوق: ";
            this.LblBoxTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PanelInfo
            // 
            this.PanelInfo.BackColor = System.Drawing.Color.White;
            this.PanelInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelInfo.Controls.Add(this.LblArchiveNumber);
            this.PanelInfo.Controls.Add(this.LblArchiveValue);
            this.PanelInfo.Controls.Add(this.LblDocCount);
            this.PanelInfo.Controls.Add(this.LblDocCountValue);
            this.PanelInfo.Controls.Add(this.LblStatus);
            this.PanelInfo.Controls.Add(this.LblStatusValue);
            this.PanelInfo.Location = new System.Drawing.Point(20, 65);
            this.PanelInfo.Name = "PanelInfo";
            this.PanelInfo.Size = new System.Drawing.Size(1010, 54);
            this.PanelInfo.TabIndex = 1;
            // 
            // LblArchiveNumber
            // 
            this.LblArchiveNumber.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.LblArchiveNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.LblArchiveNumber.Location = new System.Drawing.Point(117, 1);
            this.LblArchiveNumber.Name = "LblArchiveNumber";
            this.LblArchiveNumber.Size = new System.Drawing.Size(120, 48);
            this.LblArchiveNumber.TabIndex = 0;
            this.LblArchiveNumber.Text = "📋 رقم الأرشيف:";
            this.LblArchiveNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblArchiveValue
            // 
            this.LblArchiveValue.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.LblArchiveValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(57)))), ((int)(((byte)(43)))));
            this.LblArchiveValue.Location = new System.Drawing.Point(242, 1);
            this.LblArchiveValue.Name = "LblArchiveValue";
            this.LblArchiveValue.Size = new System.Drawing.Size(150, 48);
            this.LblArchiveValue.TabIndex = 1;
            this.LblArchiveValue.Text = "-";
            this.LblArchiveValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblDocCount
            // 
            this.LblDocCount.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.LblDocCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.LblDocCount.Location = new System.Drawing.Point(422, 1);
            this.LblDocCount.Name = "LblDocCount";
            this.LblDocCount.Size = new System.Drawing.Size(120, 48);
            this.LblDocCount.TabIndex = 2;
            this.LblDocCount.Text = "📄 عدد الوثائق:";
            this.LblDocCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblDocCountValue
            // 
            this.LblDocCountValue.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.LblDocCountValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.LblDocCountValue.Location = new System.Drawing.Point(542, 1);
            this.LblDocCountValue.Name = "LblDocCountValue";
            this.LblDocCountValue.Size = new System.Drawing.Size(100, 48);
            this.LblDocCountValue.TabIndex = 3;
            this.LblDocCountValue.Text = "0";
            this.LblDocCountValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblStatus
            // 
            this.LblStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.LblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.LblStatus.Location = new System.Drawing.Point(672, 1);
            this.LblStatus.Name = "LblStatus";
            this.LblStatus.Size = new System.Drawing.Size(80, 48);
            this.LblStatus.TabIndex = 4;
            this.LblStatus.Text = "📊 الحالة:";
            this.LblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblStatusValue
            // 
            this.LblStatusValue.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.LblStatusValue.ForeColor = System.Drawing.Color.Green;
            this.LblStatusValue.Location = new System.Drawing.Point(752, 1);
            this.LblStatusValue.Name = "LblStatusValue";
            this.LblStatusValue.Size = new System.Drawing.Size(100, 48);
            this.LblStatusValue.TabIndex = 5;
            this.LblStatusValue.Text = "🟢 نشط";
            this.LblStatusValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PanelToolbar
            // 
            this.PanelToolbar.BackColor = System.Drawing.Color.White;
            this.PanelToolbar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelToolbar.Controls.Add(this.BtnAddDocument);
            this.PanelToolbar.Controls.Add(this.BtnRefresh);
            this.PanelToolbar.Controls.Add(this.BtnDeleteSelected);
            this.PanelToolbar.Controls.Add(this.BtnExport);
            this.PanelToolbar.Controls.Add(this.TxtSearch);
            this.PanelToolbar.Controls.Add(this.CmbFilter);
            this.PanelToolbar.Location = new System.Drawing.Point(20, 125);
            this.PanelToolbar.Name = "PanelToolbar";
            this.PanelToolbar.Size = new System.Drawing.Size(1010, 60);
            this.PanelToolbar.TabIndex = 2;
            // 
            // BtnAddDocument
            // 
            this.BtnAddDocument.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.BtnAddDocument.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnAddDocument.FlatAppearance.BorderSize = 0;
            this.BtnAddDocument.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnAddDocument.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.BtnAddDocument.ForeColor = System.Drawing.Color.White;
            this.BtnAddDocument.Location = new System.Drawing.Point(15, 10);
            this.BtnAddDocument.Name = "BtnAddDocument";
            this.BtnAddDocument.Size = new System.Drawing.Size(150, 40);
            this.BtnAddDocument.TabIndex = 0;
            this.BtnAddDocument.Text = "📎 إضافة وثيقة";
            this.BtnAddDocument.UseVisualStyleBackColor = false;
            // 
            // BtnRefresh
            // 
            this.BtnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.BtnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnRefresh.FlatAppearance.BorderSize = 0;
            this.BtnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnRefresh.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.BtnRefresh.ForeColor = System.Drawing.Color.White;
            this.BtnRefresh.Location = new System.Drawing.Point(180, 10);
            this.BtnRefresh.Name = "BtnRefresh";
            this.BtnRefresh.Size = new System.Drawing.Size(120, 40);
            this.BtnRefresh.TabIndex = 1;
            this.BtnRefresh.Text = "🔄 تحديث";
            this.BtnRefresh.UseVisualStyleBackColor = false;
            // 
            // BtnDeleteSelected
            // 
            this.BtnDeleteSelected.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.BtnDeleteSelected.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnDeleteSelected.FlatAppearance.BorderSize = 0;
            this.BtnDeleteSelected.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnDeleteSelected.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.BtnDeleteSelected.ForeColor = System.Drawing.Color.White;
            this.BtnDeleteSelected.Location = new System.Drawing.Point(315, 10);
            this.BtnDeleteSelected.Name = "BtnDeleteSelected";
            this.BtnDeleteSelected.Size = new System.Drawing.Size(150, 40);
            this.BtnDeleteSelected.TabIndex = 2;
            this.BtnDeleteSelected.Text = "🗑️ حذف المحدد";
            this.BtnDeleteSelected.UseVisualStyleBackColor = false;
            // 
            // BtnExport
            // 
            this.BtnExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(89)))), ((int)(((byte)(182)))));
            this.BtnExport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnExport.FlatAppearance.BorderSize = 0;
            this.BtnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnExport.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.BtnExport.ForeColor = System.Drawing.Color.White;
            this.BtnExport.Location = new System.Drawing.Point(480, 10);
            this.BtnExport.Name = "BtnExport";
            this.BtnExport.Size = new System.Drawing.Size(120, 40);
            this.BtnExport.TabIndex = 3;
            this.BtnExport.Text = "📊 تصدير";
            this.BtnExport.UseVisualStyleBackColor = false;
            // 
            // TxtSearch
            // 
            this.TxtSearch.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.TxtSearch.ForeColor = System.Drawing.Color.Gray;
            this.TxtSearch.Location = new System.Drawing.Point(620, 13);
            this.TxtSearch.Name = "TxtSearch";
            this.TxtSearch.Size = new System.Drawing.Size(250, 37);
            this.TxtSearch.TabIndex = 4;
            this.TxtSearch.Text = "🔍 بحث...";
            // 
            // CmbFilter
            // 
            this.CmbFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbFilter.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.CmbFilter.Items.AddRange(new object[] {
            "الكل",
            "نشط",
            "غير نشط"});
            this.CmbFilter.Location = new System.Drawing.Point(890, 14);
            this.CmbFilter.Name = "CmbFilter";
            this.CmbFilter.Size = new System.Drawing.Size(100, 36);
            this.CmbFilter.TabIndex = 5;
            // 
            // DgvDocuments
            // 
            this.DgvDocuments.AllowUserToAddRows = false;
            this.DgvDocuments.AllowUserToDeleteRows = false;
            this.DgvDocuments.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DgvDocuments.BackgroundColor = System.Drawing.Color.White;
            this.DgvDocuments.ColumnHeadersHeight = 34;
            this.DgvDocuments.Location = new System.Drawing.Point(20, 195);
            this.DgvDocuments.MultiSelect = false;
            this.DgvDocuments.Name = "DgvDocuments";
            this.DgvDocuments.ReadOnly = true;
            this.DgvDocuments.RowHeadersVisible = false;
            this.DgvDocuments.RowHeadersWidth = 62;
            this.DgvDocuments.RowTemplate.Height = 35;
            this.DgvDocuments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvDocuments.Size = new System.Drawing.Size(1010, 450);
            this.DgvDocuments.TabIndex = 3;
            // 
            // PanelStatus
            // 
            this.PanelStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.PanelStatus.Controls.Add(this.LblStatusBar);
            this.PanelStatus.Controls.Add(this.LblRecordCount);
            this.PanelStatus.Location = new System.Drawing.Point(20, 660);
            this.PanelStatus.Name = "PanelStatus";
            this.PanelStatus.Size = new System.Drawing.Size(1010, 58);
            this.PanelStatus.TabIndex = 4;
            // 
            // LblStatusBar
            // 
            this.LblStatusBar.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.LblStatusBar.ForeColor = System.Drawing.Color.White;
            this.LblStatusBar.Location = new System.Drawing.Point(15, 8);
            this.LblStatusBar.Name = "LblStatusBar";
            this.LblStatusBar.Size = new System.Drawing.Size(500, 42);
            this.LblStatusBar.TabIndex = 0;
            this.LblStatusBar.Text = "✅ جاهز";
            // 
            // LblRecordCount
            // 
            this.LblRecordCount.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.LblRecordCount.ForeColor = System.Drawing.Color.White;
            this.LblRecordCount.Location = new System.Drawing.Point(850, 8);
            this.LblRecordCount.Name = "LblRecordCount";
            this.LblRecordCount.Size = new System.Drawing.Size(150, 42);
            this.LblRecordCount.TabIndex = 1;
            this.LblRecordCount.Text = "عدد السجلات: 0";
            this.LblRecordCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmBoxContent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1060, 730);
            this.Controls.Add(this.PanelMain);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.MinimumSize = new System.Drawing.Size(1000, 700);
            this.Name = "FrmBoxContent";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "📦 محتويات الصندوق";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.PanelMain.ResumeLayout(false);
            this.PanelInfo.ResumeLayout(false);
            this.PanelToolbar.ResumeLayout(false);
            this.PanelToolbar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvDocuments)).EndInit();
            this.PanelStatus.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #region "عناصر التحكم"

        private System.Windows.Forms.Panel PanelMain;
        private System.Windows.Forms.Label LblBoxTitle;
        private System.Windows.Forms.Panel PanelInfo;
        private System.Windows.Forms.Label LblArchiveNumber;
        private System.Windows.Forms.Label LblArchiveValue;
        private System.Windows.Forms.Label LblDocCount;
        private System.Windows.Forms.Label LblDocCountValue;
        private System.Windows.Forms.Label LblStatus;
        private System.Windows.Forms.Label LblStatusValue;
        private System.Windows.Forms.Panel PanelToolbar;
        private System.Windows.Forms.Button BtnAddDocument;
        private System.Windows.Forms.Button BtnRefresh;
        private System.Windows.Forms.Button BtnDeleteSelected;
        private System.Windows.Forms.Button BtnExport;
        private System.Windows.Forms.TextBox TxtSearch;
        private System.Windows.Forms.ComboBox CmbFilter;
        private System.Windows.Forms.DataGridView DgvDocuments;
        private System.Windows.Forms.Panel PanelStatus;
        private System.Windows.Forms.Label LblStatusBar;
        private System.Windows.Forms.Label LblRecordCount;

        #endregion
    }
}