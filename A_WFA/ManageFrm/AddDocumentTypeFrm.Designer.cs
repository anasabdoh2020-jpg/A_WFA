namespace A_WFA.ManageFrm
{
    partial class AddDocumentTypeFrm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label LblTitle;
        private System.Windows.Forms.Panel PanelHeader;
        private System.Windows.Forms.PictureBox PicIcon;
        private System.Windows.Forms.Panel PanelBody;
        private System.Windows.Forms.Label LblName;
        private System.Windows.Forms.TextBox TxtName;
        private System.Windows.Forms.Label LblDescription;
        private System.Windows.Forms.TextBox TxtDescription;
        private System.Windows.Forms.CheckBox ChkIsActive;
        private System.Windows.Forms.Panel PanelButtons;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Button BtnCancel;

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
            this.LblTitle = new System.Windows.Forms.Label();
            this.PanelHeader = new System.Windows.Forms.Panel();
            this.PicIcon = new System.Windows.Forms.PictureBox();
            this.PanelBody = new System.Windows.Forms.Panel();
            this.LblName = new System.Windows.Forms.Label();
            this.TxtName = new System.Windows.Forms.TextBox();
            this.LblDescription = new System.Windows.Forms.Label();
            this.TxtDescription = new System.Windows.Forms.TextBox();
            this.ChkIsActive = new System.Windows.Forms.CheckBox();
            this.PanelButtons = new System.Windows.Forms.Panel();
            this.BtnSave = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.PanelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicIcon)).BeginInit();
            this.PanelBody.SuspendLayout();
            this.PanelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // LblTitle
            // 
            this.LblTitle.AutoSize = true;
            this.LblTitle.BackColor = System.Drawing.Color.Transparent;
            this.LblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.LblTitle.ForeColor = System.Drawing.Color.White;
            this.LblTitle.Location = new System.Drawing.Point(80, 18);
            this.LblTitle.Name = "LblTitle";
            this.LblTitle.Size = new System.Drawing.Size(312, 48);
            this.LblTitle.TabIndex = 1;
            this.LblTitle.Text = "📄 إضافة نوع وثيقة";
            this.LblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PanelHeader
            // 
            this.PanelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.PanelHeader.Controls.Add(this.PicIcon);
            this.PanelHeader.Controls.Add(this.LblTitle);
            this.PanelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelHeader.Location = new System.Drawing.Point(0, 0);
            this.PanelHeader.Name = "PanelHeader";
            this.PanelHeader.Size = new System.Drawing.Size(500, 70);
            this.PanelHeader.TabIndex = 0;
            // 
            // PicIcon
            // 
            this.PicIcon.BackColor = System.Drawing.Color.Transparent;
            this.PicIcon.Font = new System.Drawing.Font("Segoe UI", 24F);
            this.PicIcon.Location = new System.Drawing.Point(20, 10);
            this.PicIcon.Name = "PicIcon";
            this.PicIcon.Size = new System.Drawing.Size(50, 50);
            this.PicIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PicIcon.TabIndex = 0;
            this.PicIcon.TabStop = false;
            this.PicIcon.Text = "📄";
            // 
            // PanelBody
            // 
            this.PanelBody.BackColor = System.Drawing.Color.White;
            this.PanelBody.Controls.Add(this.LblName);
            this.PanelBody.Controls.Add(this.TxtName);
            this.PanelBody.Controls.Add(this.LblDescription);
            this.PanelBody.Controls.Add(this.TxtDescription);
            this.PanelBody.Controls.Add(this.ChkIsActive);
            this.PanelBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelBody.Location = new System.Drawing.Point(0, 70);
            this.PanelBody.Name = "PanelBody";
            this.PanelBody.Padding = new System.Windows.Forms.Padding(30);
            this.PanelBody.Size = new System.Drawing.Size(500, 260);
            this.PanelBody.TabIndex = 1;
            // 
            // LblName
            // 
            this.LblName.AutoSize = true;
            this.LblName.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.LblName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.LblName.Location = new System.Drawing.Point(308, 12);
            this.LblName.Name = "LblName";
            this.LblName.Size = new System.Drawing.Size(142, 30);
            this.LblName.TabIndex = 0;
            this.LblName.Text = "📛 اسم النوع:";
            // 
            // TxtName
            // 
            this.TxtName.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.TxtName.Location = new System.Drawing.Point(30, 50);
            this.TxtName.Name = "TxtName";
            this.TxtName.Size = new System.Drawing.Size(420, 37);
            this.TxtName.TabIndex = 0;
            // 
            // LblDescription
            // 
            this.LblDescription.AutoSize = true;
            this.LblDescription.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.LblDescription.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.LblDescription.Location = new System.Drawing.Point(326, 95);
            this.LblDescription.Name = "LblDescription";
            this.LblDescription.Size = new System.Drawing.Size(124, 30);
            this.LblDescription.TabIndex = 1;
            this.LblDescription.Text = "📝 الوصف:";
            // 
            // TxtDescription
            // 
            this.TxtDescription.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.TxtDescription.Location = new System.Drawing.Point(30, 133);
            this.TxtDescription.Name = "TxtDescription";
            this.TxtDescription.Size = new System.Drawing.Size(420, 37);
            this.TxtDescription.TabIndex = 1;
            // 
            // ChkIsActive
            // 
            this.ChkIsActive.AutoSize = true;
            this.ChkIsActive.Checked = true;
            this.ChkIsActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkIsActive.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.ChkIsActive.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.ChkIsActive.Location = new System.Drawing.Point(60, 185);
            this.ChkIsActive.Name = "ChkIsActive";
            this.ChkIsActive.Size = new System.Drawing.Size(118, 34);
            this.ChkIsActive.TabIndex = 2;
            this.ChkIsActive.Text = "🟢 نشط";
            // 
            // PanelButtons
            // 
            this.PanelButtons.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.PanelButtons.Controls.Add(this.BtnSave);
            this.PanelButtons.Controls.Add(this.BtnCancel);
            this.PanelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PanelButtons.Location = new System.Drawing.Point(0, 330);
            this.PanelButtons.Name = "PanelButtons";
            this.PanelButtons.Padding = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.PanelButtons.Size = new System.Drawing.Size(500, 70);
            this.PanelButtons.TabIndex = 2;
            // 
            // BtnSave
            // 
            this.BtnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.BtnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnSave.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.BtnSave.ForeColor = System.Drawing.Color.White;
            this.BtnSave.Location = new System.Drawing.Point(180, 10);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(130, 45);
            this.BtnSave.TabIndex = 0;
            this.BtnSave.Text = "💾 حفظ";
            this.BtnSave.UseVisualStyleBackColor = false;
            // 
            // BtnCancel
            // 
            this.BtnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.BtnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnCancel.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.BtnCancel.ForeColor = System.Drawing.Color.White;
            this.BtnCancel.Location = new System.Drawing.Point(320, 10);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(100, 45);
            this.BtnCancel.TabIndex = 1;
            this.BtnCancel.Text = "إلغاء";
            this.BtnCancel.UseVisualStyleBackColor = false;
            // 
            // AddDocumentTypeFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 400);
            this.Controls.Add(this.PanelBody);
            this.Controls.Add(this.PanelHeader);
            this.Controls.Add(this.PanelButtons);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddDocumentTypeFrm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "📄 إضافة نوع وثيقة";
            this.PanelHeader.ResumeLayout(false);
            this.PanelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicIcon)).EndInit();
            this.PanelBody.ResumeLayout(false);
            this.PanelBody.PerformLayout();
            this.PanelButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}