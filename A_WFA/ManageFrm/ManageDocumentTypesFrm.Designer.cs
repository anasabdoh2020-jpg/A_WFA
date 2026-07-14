namespace A_WFA.ManageFrm
{
    partial class ManageDocumentTypesFrm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel PanelHeader;
        private System.Windows.Forms.Label LblTitle;
        private System.Windows.Forms.Panel PanelControls;
        private System.Windows.Forms.Button BtnAddNew;
        private System.Windows.Forms.Button BtnRefresh;
        private System.Windows.Forms.Button BtnDeleteSelected;
        private System.Windows.Forms.Button BtnExport;
        private System.Windows.Forms.Panel PanelSearch;
        private System.Windows.Forms.Label LblSearch;
        private System.Windows.Forms.TextBox TxtSearch;
        private System.Windows.Forms.DataGridView DgvTypes;
        private System.Windows.Forms.Panel PanelFooter;
        private System.Windows.Forms.Label LblRecordCount;
        private System.Windows.Forms.Label LblStatus;

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
            this.PanelHeader = new System.Windows.Forms.Panel();
            this.LblTitle = new System.Windows.Forms.Label();
            this.PanelControls = new System.Windows.Forms.Panel();
            this.BtnAddNew = new System.Windows.Forms.Button();
            this.BtnRefresh = new System.Windows.Forms.Button();
            this.BtnDeleteSelected = new System.Windows.Forms.Button();
            this.BtnExport = new System.Windows.Forms.Button();
            this.PanelSearch = new System.Windows.Forms.Panel();
            this.LblSearch = new System.Windows.Forms.Label();
            this.TxtSearch = new System.Windows.Forms.TextBox();
            this.DgvTypes = new System.Windows.Forms.DataGridView();
            this.PanelFooter = new System.Windows.Forms.Panel();
            this.LblRecordCount = new System.Windows.Forms.Label();
            this.LblStatus = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.PanelHeader.SuspendLayout();
            this.PanelControls.SuspendLayout();
            this.PanelSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvTypes)).BeginInit();
            this.PanelFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelHeader
            // 
            this.PanelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.PanelHeader.Controls.Add(this.LblTitle);
            this.PanelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelHeader.Location = new System.Drawing.Point(0, 0);
            this.PanelHeader.Name = "PanelHeader";
            this.PanelHeader.Size = new System.Drawing.Size(900, 60);
            this.PanelHeader.TabIndex = 0;
            // 
            // LblTitle
            // 
            this.LblTitle.AutoSize = true;
            this.LblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.LblTitle.ForeColor = System.Drawing.Color.White;
            this.LblTitle.Location = new System.Drawing.Point(382, 4);
            this.LblTitle.Name = "LblTitle";
            this.LblTitle.Size = new System.Drawing.Size(291, 45);
            this.LblTitle.TabIndex = 0;
            this.LblTitle.Text = "📄 إدارة أنواع الوثائق";
            // 
            // PanelControls
            // 
            this.PanelControls.BackColor = System.Drawing.Color.White;
            this.PanelControls.Controls.Add(this.BtnAddNew);
            this.PanelControls.Controls.Add(this.BtnRefresh);
            this.PanelControls.Controls.Add(this.BtnDeleteSelected);
            this.PanelControls.Controls.Add(this.BtnExport);
            this.PanelControls.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelControls.Location = new System.Drawing.Point(0, 60);
            this.PanelControls.Name = "PanelControls";
            this.PanelControls.Padding = new System.Windows.Forms.Padding(10, 8, 10, 8);
            this.PanelControls.Size = new System.Drawing.Size(900, 55);
            this.PanelControls.TabIndex = 1;
            // 
            // BtnAddNew
            // 
            this.BtnAddNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.BtnAddNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnAddNew.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.BtnAddNew.ForeColor = System.Drawing.Color.White;
            this.BtnAddNew.Location = new System.Drawing.Point(318, 8);
            this.BtnAddNew.Name = "BtnAddNew";
            this.BtnAddNew.Size = new System.Drawing.Size(148, 38);
            this.BtnAddNew.TabIndex = 0;
            this.BtnAddNew.Text = "➕ إضافة نوع";
            this.BtnAddNew.UseVisualStyleBackColor = false;
            // 
            // BtnRefresh
            // 
            this.BtnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.BtnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnRefresh.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.BtnRefresh.ForeColor = System.Drawing.Color.White;
            this.BtnRefresh.Location = new System.Drawing.Point(472, 8);
            this.BtnRefresh.Name = "BtnRefresh";
            this.BtnRefresh.Size = new System.Drawing.Size(124, 38);
            this.BtnRefresh.TabIndex = 1;
            this.BtnRefresh.Text = "🔄 تحديث";
            this.BtnRefresh.UseVisualStyleBackColor = false;
            // 
            // BtnDeleteSelected
            // 
            this.BtnDeleteSelected.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.BtnDeleteSelected.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnDeleteSelected.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.BtnDeleteSelected.ForeColor = System.Drawing.Color.White;
            this.BtnDeleteSelected.Location = new System.Drawing.Point(602, 8);
            this.BtnDeleteSelected.Name = "BtnDeleteSelected";
            this.BtnDeleteSelected.Size = new System.Drawing.Size(165, 38);
            this.BtnDeleteSelected.TabIndex = 2;
            this.BtnDeleteSelected.Text = "🗑️ حذف المحدد";
            this.BtnDeleteSelected.UseVisualStyleBackColor = false;
            // 
            // BtnExport
            // 
            this.BtnExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(89)))), ((int)(((byte)(182)))));
            this.BtnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnExport.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.BtnExport.ForeColor = System.Drawing.Color.White;
            this.BtnExport.Location = new System.Drawing.Point(773, 8);
            this.BtnExport.Name = "BtnExport";
            this.BtnExport.Size = new System.Drawing.Size(114, 38);
            this.BtnExport.TabIndex = 3;
            this.BtnExport.Text = "📤 تصدير";
            this.BtnExport.UseVisualStyleBackColor = false;
            // 
            // PanelSearch
            // 
            this.PanelSearch.BackColor = System.Drawing.Color.White;
            this.PanelSearch.Controls.Add(this.LblSearch);
            this.PanelSearch.Controls.Add(this.TxtSearch);
            this.PanelSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelSearch.Location = new System.Drawing.Point(0, 115);
            this.PanelSearch.Name = "PanelSearch";
            this.PanelSearch.Padding = new System.Windows.Forms.Padding(10, 8, 10, 8);
            this.PanelSearch.Size = new System.Drawing.Size(900, 40);
            this.PanelSearch.TabIndex = 2;
            // 
            // LblSearch
            // 
            this.LblSearch.AutoSize = true;
            this.LblSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.LblSearch.Location = new System.Drawing.Point(786, 8);
            this.LblSearch.Name = "LblSearch";
            this.LblSearch.Size = new System.Drawing.Size(85, 28);
            this.LblSearch.TabIndex = 0;
            this.LblSearch.Text = "🔍 بحث:";
            // 
            // TxtSearch
            // 
            this.TxtSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.TxtSearch.Location = new System.Drawing.Point(150, 3);
            this.TxtSearch.Name = "TxtSearch";
            this.TxtSearch.Size = new System.Drawing.Size(617, 34);
            this.TxtSearch.TabIndex = 1;
            // 
            // DgvTypes
            // 
            this.DgvTypes.BackgroundColor = System.Drawing.Color.White;
            this.DgvTypes.ColumnHeadersHeight = 34;
            this.DgvTypes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DgvTypes.Location = new System.Drawing.Point(0, 155);
            this.DgvTypes.Name = "DgvTypes";
            this.DgvTypes.RowHeadersVisible = false;
            this.DgvTypes.RowHeadersWidth = 62;
            this.DgvTypes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvTypes.Size = new System.Drawing.Size(900, 375);
            this.DgvTypes.TabIndex = 3;
            // 
            // PanelFooter
            // 
            this.PanelFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.PanelFooter.Controls.Add(this.button1);
            this.PanelFooter.Controls.Add(this.LblRecordCount);
            this.PanelFooter.Controls.Add(this.LblStatus);
            this.PanelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PanelFooter.Location = new System.Drawing.Point(0, 530);
            this.PanelFooter.Name = "PanelFooter";
            this.PanelFooter.Padding = new System.Windows.Forms.Padding(10);
            this.PanelFooter.Size = new System.Drawing.Size(900, 40);
            this.PanelFooter.TabIndex = 4;
            // 
            // LblRecordCount
            // 
            this.LblRecordCount.AutoSize = true;
            this.LblRecordCount.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.LblRecordCount.ForeColor = System.Drawing.Color.White;
            this.LblRecordCount.Location = new System.Drawing.Point(42, 7);
            this.LblRecordCount.Name = "LblRecordCount";
            this.LblRecordCount.Size = new System.Drawing.Size(137, 28);
            this.LblRecordCount.TabIndex = 0;
            this.LblRecordCount.Text = "عدد السجلات: 0";
            // 
            // LblStatus
            // 
            this.LblStatus.AutoSize = true;
            this.LblStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.LblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.LblStatus.Location = new System.Drawing.Point(546, 8);
            this.LblStatus.Name = "LblStatus";
            this.LblStatus.Size = new System.Drawing.Size(82, 28);
            this.LblStatus.TabIndex = 1;
            this.LblStatus.Text = "✅ جاهز";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.MediumPurple;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(723, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(164, 40);
            this.button1.TabIndex = 7;
            this.button1.Text = "العودة";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ManageDocumentTypesFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(900, 570);
            this.Controls.Add(this.DgvTypes);
            this.Controls.Add(this.PanelSearch);
            this.Controls.Add(this.PanelControls);
            this.Controls.Add(this.PanelHeader);
            this.Controls.Add(this.PanelFooter);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "ManageDocumentTypesFrm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "📄 إدارة أنواع الوثائق";
            this.PanelHeader.ResumeLayout(false);
            this.PanelHeader.PerformLayout();
            this.PanelControls.ResumeLayout(false);
            this.PanelSearch.ResumeLayout(false);
            this.PanelSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvTypes)).EndInit();
            this.PanelFooter.ResumeLayout(false);
            this.PanelFooter.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Button button1;
    }
}