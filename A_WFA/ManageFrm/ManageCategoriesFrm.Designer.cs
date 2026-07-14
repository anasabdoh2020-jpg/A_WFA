namespace A_WFA.ManageFrm
{
    partial class ManageCategoriesFrm
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
            this.LblTitle = new System.Windows.Forms.Label();
            this.PanelToolbar = new System.Windows.Forms.Panel();
            this.BtnAddNew = new System.Windows.Forms.Button();
            this.BtnRefresh = new System.Windows.Forms.Button();
            this.BtnDeleteSelected = new System.Windows.Forms.Button();
            this.BtnExport = new System.Windows.Forms.Button();
            this.TxtSearch = new System.Windows.Forms.TextBox();
            this.DgvCategories = new System.Windows.Forms.DataGridView();
            this.PanelStatus = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.LblStatus = new System.Windows.Forms.Label();
            this.LblRecordCount = new System.Windows.Forms.Label();
            this.PanelMain.SuspendLayout();
            this.PanelToolbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvCategories)).BeginInit();
            this.PanelStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelMain
            // 
            this.PanelMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.PanelMain.Controls.Add(this.LblTitle);
            this.PanelMain.Controls.Add(this.PanelToolbar);
            this.PanelMain.Controls.Add(this.DgvCategories);
            this.PanelMain.Controls.Add(this.PanelStatus);
            this.PanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelMain.Location = new System.Drawing.Point(0, 0);
            this.PanelMain.Name = "PanelMain";
            this.PanelMain.Padding = new System.Windows.Forms.Padding(20);
            this.PanelMain.Size = new System.Drawing.Size(1060, 660);
            this.PanelMain.TabIndex = 0;
            // 
            // LblTitle
            // 
            this.LblTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.LblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.LblTitle.Location = new System.Drawing.Point(20, 0);
            this.LblTitle.Name = "LblTitle";
            this.LblTitle.Size = new System.Drawing.Size(1010, 72);
            this.LblTitle.TabIndex = 0;
            this.LblTitle.Text = "📂 إدارة التصنيفات";
            this.LblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PanelToolbar
            // 
            this.PanelToolbar.BackColor = System.Drawing.Color.White;
            this.PanelToolbar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelToolbar.Controls.Add(this.BtnAddNew);
            this.PanelToolbar.Controls.Add(this.BtnRefresh);
            this.PanelToolbar.Controls.Add(this.BtnDeleteSelected);
            this.PanelToolbar.Controls.Add(this.BtnExport);
            this.PanelToolbar.Controls.Add(this.TxtSearch);
            this.PanelToolbar.Location = new System.Drawing.Point(20, 75);
            this.PanelToolbar.Name = "PanelToolbar";
            this.PanelToolbar.Size = new System.Drawing.Size(1010, 65);
            this.PanelToolbar.TabIndex = 1;
            // 
            // BtnAddNew
            // 
            this.BtnAddNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.BtnAddNew.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnAddNew.FlatAppearance.BorderSize = 0;
            this.BtnAddNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnAddNew.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.BtnAddNew.ForeColor = System.Drawing.Color.White;
            this.BtnAddNew.Location = new System.Drawing.Point(194, 10);
            this.BtnAddNew.Name = "BtnAddNew";
            this.BtnAddNew.Size = new System.Drawing.Size(128, 40);
            this.BtnAddNew.TabIndex = 0;
            this.BtnAddNew.Text = "➕ إضافة تصنيف";
            this.BtnAddNew.UseVisualStyleBackColor = false;
            // 
            // BtnRefresh
            // 
            this.BtnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.BtnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnRefresh.FlatAppearance.BorderSize = 0;
            this.BtnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnRefresh.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.BtnRefresh.ForeColor = System.Drawing.Color.White;
            this.BtnRefresh.Location = new System.Drawing.Point(333, 10);
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
            this.BtnDeleteSelected.Location = new System.Drawing.Point(464, 10);
            this.BtnDeleteSelected.Name = "BtnDeleteSelected";
            this.BtnDeleteSelected.Size = new System.Drawing.Size(115, 40);
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
            this.BtnExport.Location = new System.Drawing.Point(595, 10);
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
            this.TxtSearch.Location = new System.Drawing.Point(745, 13);
            this.TxtSearch.Name = "TxtSearch";
            this.TxtSearch.Size = new System.Drawing.Size(250, 37);
            this.TxtSearch.TabIndex = 4;
            this.TxtSearch.Text = "🔍 بحث...";
            // 
            // DgvCategories
            // 
            this.DgvCategories.AllowUserToAddRows = false;
            this.DgvCategories.AllowUserToDeleteRows = false;
            this.DgvCategories.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DgvCategories.BackgroundColor = System.Drawing.Color.White;
            this.DgvCategories.ColumnHeadersHeight = 34;
            this.DgvCategories.Location = new System.Drawing.Point(20, 155);
            this.DgvCategories.MultiSelect = false;
            this.DgvCategories.Name = "DgvCategories";
            this.DgvCategories.ReadOnly = true;
            this.DgvCategories.RowHeadersVisible = false;
            this.DgvCategories.RowHeadersWidth = 62;
            this.DgvCategories.RowTemplate.Height = 35;
            this.DgvCategories.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvCategories.Size = new System.Drawing.Size(1010, 420);
            this.DgvCategories.TabIndex = 2;
            // 
            // PanelStatus
            // 
            this.PanelStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.PanelStatus.Controls.Add(this.button1);
            this.PanelStatus.Controls.Add(this.LblStatus);
            this.PanelStatus.Controls.Add(this.LblRecordCount);
            this.PanelStatus.Location = new System.Drawing.Point(20, 590);
            this.PanelStatus.Name = "PanelStatus";
            this.PanelStatus.Size = new System.Drawing.Size(1010, 58);
            this.PanelStatus.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.MediumPurple;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(794, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(202, 52);
            this.button1.TabIndex = 5;
            this.button1.Text = "العودة";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // LblStatus
            // 
            this.LblStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.LblStatus.ForeColor = System.Drawing.Color.White;
            this.LblStatus.Location = new System.Drawing.Point(15, 8);
            this.LblStatus.Name = "LblStatus";
            this.LblStatus.Size = new System.Drawing.Size(500, 50);
            this.LblStatus.TabIndex = 0;
            this.LblStatus.Text = "✅ جاهز";
            // 
            // LblRecordCount
            // 
            this.LblRecordCount.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.LblRecordCount.ForeColor = System.Drawing.Color.White;
            this.LblRecordCount.Location = new System.Drawing.Point(15, 7);
            this.LblRecordCount.Name = "LblRecordCount";
            this.LblRecordCount.Size = new System.Drawing.Size(158, 42);
            this.LblRecordCount.TabIndex = 1;
            this.LblRecordCount.Text = "عدد السجلات: 0";
            this.LblRecordCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ManageCategoriesFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1060, 660);
            this.Controls.Add(this.PanelMain);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "ManageCategoriesFrm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "📂 إدارة التصنيفات";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.PanelMain.ResumeLayout(false);
            this.PanelToolbar.ResumeLayout(false);
            this.PanelToolbar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvCategories)).EndInit();
            this.PanelStatus.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #region "عناصر التحكم"

        private System.Windows.Forms.Panel PanelMain;
        private System.Windows.Forms.Label LblTitle;
        private System.Windows.Forms.Panel PanelToolbar;
        private System.Windows.Forms.Panel PanelStatus;
        private System.Windows.Forms.DataGridView DgvCategories;
        private System.Windows.Forms.Label LblStatus;
        private System.Windows.Forms.Label LblRecordCount;

        // أزرار شريط الأدوات
        private System.Windows.Forms.Button BtnAddNew;
        private System.Windows.Forms.Button BtnRefresh;
        private System.Windows.Forms.Button BtnDeleteSelected;
        private System.Windows.Forms.Button BtnExport;
        private System.Windows.Forms.TextBox TxtSearch;

        #endregion

        private System.Windows.Forms.Button button1;
    }
}