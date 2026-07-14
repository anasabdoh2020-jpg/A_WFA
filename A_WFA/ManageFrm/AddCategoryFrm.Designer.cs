namespace A_WFA.ManageFrm
{
    partial class AddCategoryFrm
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
            this.PanelData = new System.Windows.Forms.Panel();
            this.LblName = new System.Windows.Forms.Label();
            this.TxtName = new System.Windows.Forms.TextBox();
            this.LblDescription = new System.Windows.Forms.Label();
            this.TxtDescription = new System.Windows.Forms.TextBox();
            this.ChkIsActive = new System.Windows.Forms.CheckBox();
            this.PanelButtons = new System.Windows.Forms.Panel();
            this.BtnSave = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.PanelMain.SuspendLayout();
            this.PanelData.SuspendLayout();
            this.PanelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelMain
            // 
            this.PanelMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.PanelMain.Controls.Add(this.LblTitle);
            this.PanelMain.Controls.Add(this.PanelData);
            this.PanelMain.Controls.Add(this.PanelButtons);
            this.PanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelMain.Location = new System.Drawing.Point(0, 0);
            this.PanelMain.Name = "PanelMain";
            this.PanelMain.Padding = new System.Windows.Forms.Padding(30);
            this.PanelMain.Size = new System.Drawing.Size(550, 460);
            this.PanelMain.TabIndex = 0;
            // 
            // LblTitle
            // 
            this.LblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.LblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.LblTitle.Location = new System.Drawing.Point(20, 15);
            this.LblTitle.Name = "LblTitle";
            this.LblTitle.Size = new System.Drawing.Size(500, 52);
            this.LblTitle.TabIndex = 0;
            this.LblTitle.Text = "📂 إضافة تصنيف جديد";
            // 
            // PanelData
            // 
            this.PanelData.BackColor = System.Drawing.Color.White;
            this.PanelData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelData.Controls.Add(this.LblName);
            this.PanelData.Controls.Add(this.TxtName);
            this.PanelData.Controls.Add(this.LblDescription);
            this.PanelData.Controls.Add(this.TxtDescription);
            this.PanelData.Controls.Add(this.ChkIsActive);
            this.PanelData.Location = new System.Drawing.Point(20, 70);
            this.PanelData.Name = "PanelData";
            this.PanelData.Padding = new System.Windows.Forms.Padding(20);
            this.PanelData.Size = new System.Drawing.Size(500, 250);
            this.PanelData.TabIndex = 1;
            // 
            // LblName
            // 
            this.LblName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.LblName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.LblName.Location = new System.Drawing.Point(239, 20);
            this.LblName.Name = "LblName";
            this.LblName.Size = new System.Drawing.Size(226, 30);
            this.LblName.TabIndex = 0;
            this.LblName.Text = "🏷️ اسم التصنيف:";
            // 
            // TxtName
            // 
            this.TxtName.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.TxtName.ForeColor = System.Drawing.Color.Gray;
            this.TxtName.Location = new System.Drawing.Point(20, 55);
            this.TxtName.Name = "TxtName";
            this.TxtName.Size = new System.Drawing.Size(440, 37);
            this.TxtName.TabIndex = 1;
            this.TxtName.Text = "أدخل اسم التصنيف...";
            // 
            // LblDescription
            // 
            this.LblDescription.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.LblDescription.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.LblDescription.Location = new System.Drawing.Point(246, 107);
            this.LblDescription.Name = "LblDescription";
            this.LblDescription.Size = new System.Drawing.Size(219, 30);
            this.LblDescription.TabIndex = 2;
            this.LblDescription.Text = "📝 الوصف:";
            // 
            // TxtDescription
            // 
            this.TxtDescription.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.TxtDescription.ForeColor = System.Drawing.Color.Gray;
            this.TxtDescription.Location = new System.Drawing.Point(20, 140);
            this.TxtDescription.Name = "TxtDescription";
            this.TxtDescription.Size = new System.Drawing.Size(440, 37);
            this.TxtDescription.TabIndex = 3;
            this.TxtDescription.Text = "وصف التصنيف...";
            // 
            // ChkIsActive
            // 
            this.ChkIsActive.Checked = true;
            this.ChkIsActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkIsActive.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.ChkIsActive.ForeColor = System.Drawing.Color.Green;
            this.ChkIsActive.Location = new System.Drawing.Point(20, 190);
            this.ChkIsActive.Name = "ChkIsActive";
            this.ChkIsActive.Size = new System.Drawing.Size(120, 35);
            this.ChkIsActive.TabIndex = 4;
            this.ChkIsActive.Text = "✅ نشط";
            // 
            // PanelButtons
            // 
            this.PanelButtons.BackColor = System.Drawing.Color.White;
            this.PanelButtons.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelButtons.Controls.Add(this.BtnSave);
            this.PanelButtons.Controls.Add(this.BtnCancel);
            this.PanelButtons.Location = new System.Drawing.Point(20, 326);
            this.PanelButtons.Name = "PanelButtons";
            this.PanelButtons.Padding = new System.Windows.Forms.Padding(15);
            this.PanelButtons.Size = new System.Drawing.Size(500, 78);
            this.PanelButtons.TabIndex = 2;
            // 
            // BtnSave
            // 
            this.BtnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.BtnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnSave.FlatAppearance.BorderSize = 0;
            this.BtnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnSave.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.BtnSave.ForeColor = System.Drawing.Color.White;
            this.BtnSave.Location = new System.Drawing.Point(31, 10);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(200, 50);
            this.BtnSave.TabIndex = 0;
            this.BtnSave.Text = "💾 حفظ";
            this.BtnSave.UseVisualStyleBackColor = false;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.BtnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnCancel.FlatAppearance.BorderSize = 0;
            this.BtnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnCancel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.BtnCancel.ForeColor = System.Drawing.Color.White;
            this.BtnCancel.Location = new System.Drawing.Point(246, 10);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(200, 50);
            this.BtnCancel.TabIndex = 1;
            this.BtnCancel.Text = "❌ إلغاء";
            this.BtnCancel.UseVisualStyleBackColor = false;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // AddCategoryFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 404);
            this.Controls.Add(this.PanelMain);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximumSize = new System.Drawing.Size(550, 460);
            this.MinimumSize = new System.Drawing.Size(550, 460);
            this.Name = "AddCategoryFrm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "📂 إضافة تصنيف";
            this.PanelMain.ResumeLayout(false);
            this.PanelData.ResumeLayout(false);
            this.PanelData.PerformLayout();
            this.PanelButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #region "عناصر التحكم"

        private System.Windows.Forms.Panel PanelMain;
        private System.Windows.Forms.Panel PanelData;
        private System.Windows.Forms.Panel PanelButtons;
        private System.Windows.Forms.Label LblTitle;
        private System.Windows.Forms.Label LblName;
        private System.Windows.Forms.TextBox TxtName;
        private System.Windows.Forms.Label LblDescription;
        private System.Windows.Forms.TextBox TxtDescription;
        private System.Windows.Forms.CheckBox ChkIsActive;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Button BtnCancel;

        #endregion
    }
}