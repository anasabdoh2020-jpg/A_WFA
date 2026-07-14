namespace A_WFA.BoxFrms
{
    partial class AddBoxFrm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.BtnSave = new System.Windows.Forms.Button();
            this.BtnUpdate = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.PanelButtons = new System.Windows.Forms.Panel();
            this.BtnDelete = new System.Windows.Forms.Button();
            this.LblStatusTitle = new System.Windows.Forms.Label();
            this.ChkIsActive = new System.Windows.Forms.CheckBox();
            this.PanelImage = new System.Windows.Forms.Panel();
            this.LblImageTitle = new System.Windows.Forms.Label();
            this.PicBoxImage = new System.Windows.Forms.PictureBox();
            this.BtnBrowseImage = new System.Windows.Forms.Button();
            this.BtnRemoveImage = new System.Windows.Forms.Button();
            this.BtnGenerateNumber = new System.Windows.Forms.Button();
            this.LblDetails = new System.Windows.Forms.Label();
            this.LblBoxName = new System.Windows.Forms.Label();
            this.LblArchiveNumber = new System.Windows.Forms.Label();
            this.TxtArchiveNumber = new System.Windows.Forms.TextBox();
            this.LblTitle = new System.Windows.Forms.Label();
            this.PanelInfo = new System.Windows.Forms.Panel();
            this.TxtBoxName = new System.Windows.Forms.TextBox();
            this.TxtDetails = new System.Windows.Forms.TextBox();
            this.PanelMain = new System.Windows.Forms.Panel();
            this.PanelButtons.SuspendLayout();
            this.PanelImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicBoxImage)).BeginInit();
            this.PanelInfo.SuspendLayout();
            this.PanelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnSave
            // 
            this.BtnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.BtnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnSave.FlatAppearance.BorderSize = 0;
            this.BtnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnSave.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.BtnSave.ForeColor = System.Drawing.Color.White;
            this.BtnSave.Location = new System.Drawing.Point(146, 64);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(114, 50);
            this.BtnSave.TabIndex = 0;
            this.BtnSave.Text = "💾 حفظ";
            this.BtnSave.UseVisualStyleBackColor = false;
            // 
            // BtnUpdate
            // 
            this.BtnUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.BtnUpdate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnUpdate.FlatAppearance.BorderSize = 0;
            this.BtnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnUpdate.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.BtnUpdate.ForeColor = System.Drawing.Color.White;
            this.BtnUpdate.Location = new System.Drawing.Point(266, 64);
            this.BtnUpdate.Name = "BtnUpdate";
            this.BtnUpdate.Size = new System.Drawing.Size(140, 50);
            this.BtnUpdate.TabIndex = 1;
            this.BtnUpdate.Text = "✏️ تحديث";
            this.BtnUpdate.UseVisualStyleBackColor = false;
            this.BtnUpdate.Visible = false;
            // 
            // BtnCancel
            // 
            this.BtnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.BtnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnCancel.FlatAppearance.BorderSize = 0;
            this.BtnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnCancel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.BtnCancel.ForeColor = System.Drawing.Color.White;
            this.BtnCancel.Location = new System.Drawing.Point(266, 129);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(140, 50);
            this.BtnCancel.TabIndex = 3;
            this.BtnCancel.Text = "❌ إلغاء";
            this.BtnCancel.UseVisualStyleBackColor = false;
            // 
            // PanelButtons
            // 
            this.PanelButtons.BackColor = System.Drawing.Color.White;
            this.PanelButtons.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelButtons.Controls.Add(this.BtnSave);
            this.PanelButtons.Controls.Add(this.BtnUpdate);
            this.PanelButtons.Controls.Add(this.BtnDelete);
            this.PanelButtons.Controls.Add(this.BtnCancel);
            this.PanelButtons.Location = new System.Drawing.Point(260, 281);
            this.PanelButtons.Name = "PanelButtons";
            this.PanelButtons.Size = new System.Drawing.Size(605, 269);
            this.PanelButtons.TabIndex = 4;
            // 
            // BtnDelete
            // 
            this.BtnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.BtnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnDelete.FlatAppearance.BorderSize = 0;
            this.BtnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnDelete.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.BtnDelete.ForeColor = System.Drawing.Color.White;
            this.BtnDelete.Location = new System.Drawing.Point(146, 129);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(114, 50);
            this.BtnDelete.TabIndex = 2;
            this.BtnDelete.Text = "🗑️ حذف";
            this.BtnDelete.UseVisualStyleBackColor = false;
            this.BtnDelete.Visible = false;
            // 
            // LblStatusTitle
            // 
            this.LblStatusTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.LblStatusTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.LblStatusTitle.Location = new System.Drawing.Point(412, 155);
            this.LblStatusTitle.Name = "LblStatusTitle";
            this.LblStatusTitle.Size = new System.Drawing.Size(194, 49);
            this.LblStatusTitle.TabIndex = 0;
            this.LblStatusTitle.Text = "📊 حالة الصندوق";
            // 
            // ChkIsActive
            // 
            this.ChkIsActive.Appearance = System.Windows.Forms.Appearance.Button;
            this.ChkIsActive.Checked = true;
            this.ChkIsActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkIsActive.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.ChkIsActive.ForeColor = System.Drawing.Color.Green;
            this.ChkIsActive.Location = new System.Drawing.Point(275, 149);
            this.ChkIsActive.Name = "ChkIsActive";
            this.ChkIsActive.Size = new System.Drawing.Size(120, 46);
            this.ChkIsActive.TabIndex = 1;
            this.ChkIsActive.Text = "✅ نشط";
            // 
            // PanelImage
            // 
            this.PanelImage.BackColor = System.Drawing.Color.White;
            this.PanelImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelImage.Controls.Add(this.LblImageTitle);
            this.PanelImage.Controls.Add(this.PicBoxImage);
            this.PanelImage.Controls.Add(this.BtnBrowseImage);
            this.PanelImage.Controls.Add(this.BtnRemoveImage);
            this.PanelImage.Location = new System.Drawing.Point(10, 70);
            this.PanelImage.Name = "PanelImage";
            this.PanelImage.Size = new System.Drawing.Size(244, 480);
            this.PanelImage.TabIndex = 2;
            // 
            // LblImageTitle
            // 
            this.LblImageTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.LblImageTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.LblImageTitle.Location = new System.Drawing.Point(15, 0);
            this.LblImageTitle.Name = "LblImageTitle";
            this.LblImageTitle.Size = new System.Drawing.Size(213, 47);
            this.LblImageTitle.TabIndex = 0;
            this.LblImageTitle.Text = "🖼️ صورة الصندوق";
            this.LblImageTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PicBoxImage
            // 
            this.PicBoxImage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.PicBoxImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PicBoxImage.Location = new System.Drawing.Point(15, 50);
            this.PicBoxImage.Name = "PicBoxImage";
            this.PicBoxImage.Size = new System.Drawing.Size(213, 329);
            this.PicBoxImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PicBoxImage.TabIndex = 1;
            this.PicBoxImage.TabStop = false;
            // 
            // BtnBrowseImage
            // 
            this.BtnBrowseImage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.BtnBrowseImage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnBrowseImage.FlatAppearance.BorderSize = 0;
            this.BtnBrowseImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnBrowseImage.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.BtnBrowseImage.ForeColor = System.Drawing.Color.White;
            this.BtnBrowseImage.Location = new System.Drawing.Point(15, 385);
            this.BtnBrowseImage.Name = "BtnBrowseImage";
            this.BtnBrowseImage.Size = new System.Drawing.Size(213, 35);
            this.BtnBrowseImage.TabIndex = 2;
            this.BtnBrowseImage.Text = "📂 اختيار صورة";
            this.BtnBrowseImage.UseVisualStyleBackColor = false;
            // 
            // BtnRemoveImage
            // 
            this.BtnRemoveImage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.BtnRemoveImage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnRemoveImage.FlatAppearance.BorderSize = 0;
            this.BtnRemoveImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnRemoveImage.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.BtnRemoveImage.ForeColor = System.Drawing.Color.White;
            this.BtnRemoveImage.Location = new System.Drawing.Point(15, 427);
            this.BtnRemoveImage.Name = "BtnRemoveImage";
            this.BtnRemoveImage.Size = new System.Drawing.Size(213, 35);
            this.BtnRemoveImage.TabIndex = 3;
            this.BtnRemoveImage.Text = "🗑️ إزالة الصورة";
            this.BtnRemoveImage.UseVisualStyleBackColor = false;
            // 
            // BtnGenerateNumber
            // 
            this.BtnGenerateNumber.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.BtnGenerateNumber.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnGenerateNumber.FlatAppearance.BorderSize = 0;
            this.BtnGenerateNumber.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnGenerateNumber.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.BtnGenerateNumber.ForeColor = System.Drawing.Color.White;
            this.BtnGenerateNumber.Location = new System.Drawing.Point(29, 63);
            this.BtnGenerateNumber.Name = "BtnGenerateNumber";
            this.BtnGenerateNumber.Size = new System.Drawing.Size(264, 35);
            this.BtnGenerateNumber.TabIndex = 4;
            this.BtnGenerateNumber.Text = "🔄 توليد تلقائي";
            this.BtnGenerateNumber.UseVisualStyleBackColor = false;
            // 
            // LblDetails
            // 
            this.LblDetails.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.LblDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.LblDetails.Location = new System.Drawing.Point(413, 109);
            this.LblDetails.Name = "LblDetails";
            this.LblDetails.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.LblDetails.Size = new System.Drawing.Size(176, 37);
            this.LblDetails.TabIndex = 5;
            this.LblDetails.Text = "📝 تفاصيل البكس:";
            // 
            // LblBoxName
            // 
            this.LblBoxName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.LblBoxName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.LblBoxName.Location = new System.Drawing.Point(413, 22);
            this.LblBoxName.Name = "LblBoxName";
            this.LblBoxName.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.LblBoxName.Size = new System.Drawing.Size(175, 30);
            this.LblBoxName.TabIndex = 0;
            this.LblBoxName.Text = "🏷️ اسم الصندوق:";
            // 
            // LblArchiveNumber
            // 
            this.LblArchiveNumber.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.LblArchiveNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.LblArchiveNumber.Location = new System.Drawing.Point(413, 65);
            this.LblArchiveNumber.Name = "LblArchiveNumber";
            this.LblArchiveNumber.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.LblArchiveNumber.Size = new System.Drawing.Size(158, 40);
            this.LblArchiveNumber.TabIndex = 2;
            this.LblArchiveNumber.Text = "📋 رقم الأرشيف:";
            // 
            // TxtArchiveNumber
            // 
            this.TxtArchiveNumber.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.TxtArchiveNumber.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.TxtArchiveNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(57)))), ((int)(((byte)(43)))));
            this.TxtArchiveNumber.Location = new System.Drawing.Point(311, 64);
            this.TxtArchiveNumber.Name = "TxtArchiveNumber";
            this.TxtArchiveNumber.Size = new System.Drawing.Size(84, 34);
            this.TxtArchiveNumber.TabIndex = 3;
            this.TxtArchiveNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // LblTitle
            // 
            this.LblTitle.BackColor = System.Drawing.Color.Lime;
            this.LblTitle.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.LblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.LblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.LblTitle.Location = new System.Drawing.Point(0, 0);
            this.LblTitle.Name = "LblTitle";
            this.LblTitle.Size = new System.Drawing.Size(877, 67);
            this.LblTitle.TabIndex = 0;
            this.LblTitle.Text = "اضافة بكس";
            this.LblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PanelInfo
            // 
            this.PanelInfo.BackColor = System.Drawing.Color.White;
            this.PanelInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelInfo.Controls.Add(this.ChkIsActive);
            this.PanelInfo.Controls.Add(this.LblStatusTitle);
            this.PanelInfo.Controls.Add(this.LblBoxName);
            this.PanelInfo.Controls.Add(this.TxtBoxName);
            this.PanelInfo.Controls.Add(this.LblArchiveNumber);
            this.PanelInfo.Controls.Add(this.TxtArchiveNumber);
            this.PanelInfo.Controls.Add(this.BtnGenerateNumber);
            this.PanelInfo.Controls.Add(this.LblDetails);
            this.PanelInfo.Controls.Add(this.TxtDetails);
            this.PanelInfo.Location = new System.Drawing.Point(260, 70);
            this.PanelInfo.Name = "PanelInfo";
            this.PanelInfo.Padding = new System.Windows.Forms.Padding(15);
            this.PanelInfo.Size = new System.Drawing.Size(608, 205);
            this.PanelInfo.TabIndex = 1;
            // 
            // TxtBoxName
            // 
            this.TxtBoxName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.TxtBoxName.Location = new System.Drawing.Point(29, 19);
            this.TxtBoxName.Name = "TxtBoxName";
            this.TxtBoxName.Size = new System.Drawing.Size(366, 34);
            this.TxtBoxName.TabIndex = 1;
            this.TxtBoxName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TxtDetails
            // 
            this.TxtDetails.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.TxtDetails.Location = new System.Drawing.Point(29, 108);
            this.TxtDetails.Name = "TxtDetails";
            this.TxtDetails.Size = new System.Drawing.Size(366, 34);
            this.TxtDetails.TabIndex = 6;
            this.TxtDetails.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // PanelMain
            // 
            this.PanelMain.BackColor = System.Drawing.Color.Lime;
            this.PanelMain.Controls.Add(this.LblTitle);
            this.PanelMain.Controls.Add(this.PanelInfo);
            this.PanelMain.Controls.Add(this.PanelImage);
            this.PanelMain.Controls.Add(this.PanelButtons);
            this.PanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelMain.Location = new System.Drawing.Point(0, 0);
            this.PanelMain.Name = "PanelMain";
            this.PanelMain.Padding = new System.Windows.Forms.Padding(20);
            this.PanelMain.Size = new System.Drawing.Size(877, 573);
            this.PanelMain.TabIndex = 1;
            // 
            // AddBoxFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(877, 573);
            this.Controls.Add(this.PanelMain);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AddBoxFrm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Text = "اضافة بكس";
            this.PanelButtons.ResumeLayout(false);
            this.PanelImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PicBoxImage)).EndInit();
            this.PanelInfo.ResumeLayout(false);
            this.PanelInfo.PerformLayout();
            this.PanelMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Button BtnUpdate;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.Panel PanelButtons;
        private System.Windows.Forms.Button BtnDelete;
        private System.Windows.Forms.Label LblStatusTitle;
        private System.Windows.Forms.CheckBox ChkIsActive;
        private System.Windows.Forms.Panel PanelImage;
        private System.Windows.Forms.Label LblImageTitle;
        private System.Windows.Forms.PictureBox PicBoxImage;
        private System.Windows.Forms.Button BtnBrowseImage;
        private System.Windows.Forms.Button BtnRemoveImage;
        private System.Windows.Forms.Button BtnGenerateNumber;
        private System.Windows.Forms.Label LblDetails;
        private System.Windows.Forms.Label LblBoxName;
        private System.Windows.Forms.Label LblArchiveNumber;
        private System.Windows.Forms.TextBox TxtArchiveNumber;
        private System.Windows.Forms.Label LblTitle;
        private System.Windows.Forms.Panel PanelInfo;
        private System.Windows.Forms.TextBox TxtBoxName;
        private System.Windows.Forms.TextBox TxtDetails;
        private System.Windows.Forms.Panel PanelMain;
    }
}