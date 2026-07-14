using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace A_WFA
{
    partial class FrmAddDocument : Form
    {
        private IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.cmbcangeboxs = new System.Windows.Forms.ComboBox();
            this.picBoxImage = new System.Windows.Forms.PictureBox();
            this.Label10 = new System.Windows.Forms.Label();
            this.lblBoxName = new System.Windows.Forms.Label();
            this.txtArchiveNumber = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.btnDecreaseDocumentNumber = new System.Windows.Forms.Button();
            this.btnResetSequential = new System.Windows.Forms.Button();
            this.btnIncreaseDocumentNumber = new System.Windows.Forms.Button();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.Panel5 = new System.Windows.Forms.Panel();
            this.lblSoldiersCount = new System.Windows.Forms.Label();
            this.DataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnAddSoldiers = new System.Windows.Forms.Button();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.picImagePreview = new System.Windows.Forms.PictureBox();
            this.Panel4 = new System.Windows.Forms.Panel();
            this.lblDocumentInfo = new System.Windows.Forms.Label();
            this.btnInfo = new System.Windows.Forms.Button();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.Label15 = new System.Windows.Forms.Label();
            this.txtDocumentNumber = new System.Windows.Forms.TextBox();
            this.Panel3 = new System.Windows.Forms.Panel();
            this.btnAddNewDocument = new System.Windows.Forms.Button();
            this.btnLoadTemplate = new System.Windows.Forms.Button();
            this.btnCopyCurrent = new System.Windows.Forms.Button();
            this.btnSaveDocument = new System.Windows.Forms.Button();
            this.btnClearForm = new System.Windows.Forms.Button();
            this.lblFilePreview = new System.Windows.Forms.Label();
            this.Button1 = new System.Windows.Forms.Button();
            this.btnOpenScanner = new System.Windows.Forms.Button();
            this.cmbPriority = new System.Windows.Forms.ComboBox();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.Label14 = new System.Windows.Forms.Label();
            this.Label11 = new System.Windows.Forms.Label();
            this.Cmbdocument_nature = new System.Windows.Forms.ComboBox();
            this.btnBrowseFile = new System.Windows.Forms.Button();
            this.btnScanDocument = new System.Windows.Forms.Button();
            this.cmbDocumentType = new System.Windows.Forms.ComboBox();
            this.Label17 = new System.Windows.Forms.Label();
            this.Label13 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.cmbFromDepartment = new System.Windows.Forms.ComboBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.cmbToDepartment = new System.Windows.Forms.ComboBox();
            this.Label9 = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.Label6 = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.txtSummary = new System.Windows.Forms.TextBox();
            this.dtpIssueDate = new System.Windows.Forms.DateTimePicker();
            this.Label8 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.dtpDocumentDate = new System.Windows.Forms.DateTimePicker();
            this.dtpReceiveDate = new System.Windows.Forms.DateTimePicker();
            this.Label12 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label16 = new System.Windows.Forms.Label();
            this.OpenFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxImage)).BeginInit();
            this.Panel2.SuspendLayout();
            this.Panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).BeginInit();
            this.GroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picImagePreview)).BeginInit();
            this.Panel4.SuspendLayout();
            this.Panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel1
            // 
            this.Panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.Panel1.Controls.Add(this.cmbcangeboxs);
            this.Panel1.Controls.Add(this.picBoxImage);
            this.Panel1.Controls.Add(this.Label10);
            this.Panel1.Controls.Add(this.lblBoxName);
            this.Panel1.Controls.Add(this.txtArchiveNumber);
            this.Panel1.Controls.Add(this.Label1);
            this.Panel1.Controls.Add(this.btnDecreaseDocumentNumber);
            this.Panel1.Controls.Add(this.btnResetSequential);
            this.Panel1.Controls.Add(this.btnIncreaseDocumentNumber);
            this.Panel1.Location = new System.Drawing.Point(1249, 0);
            this.Panel1.Margin = new System.Windows.Forms.Padding(4);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(297, 796);
            this.Panel1.TabIndex = 1;
            // 
            // cmbcangeboxs
            // 
            this.cmbcangeboxs.Font = new System.Drawing.Font("PT Bold Heading", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.cmbcangeboxs.ForeColor = System.Drawing.Color.Fuchsia;
            this.cmbcangeboxs.FormattingEnabled = true;
            this.cmbcangeboxs.Location = new System.Drawing.Point(36, 82);
            this.cmbcangeboxs.Name = "cmbcangeboxs";
            this.cmbcangeboxs.Size = new System.Drawing.Size(224, 47);
            this.cmbcangeboxs.TabIndex = 32;
            // 
            // picBoxImage
            // 
            this.picBoxImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picBoxImage.BackColor = System.Drawing.Color.White;
            this.picBoxImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBoxImage.Location = new System.Drawing.Point(36, 136);
            this.picBoxImage.Margin = new System.Windows.Forms.Padding(4);
            this.picBoxImage.Name = "picBoxImage";
            this.picBoxImage.Size = new System.Drawing.Size(225, 527);
            this.picBoxImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBoxImage.TabIndex = 2;
            this.picBoxImage.TabStop = false;
            // 
            // Label10
            // 
            this.Label10.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Label10.AutoSize = true;
            this.Label10.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.Label10.ForeColor = System.Drawing.Color.White;
            this.Label10.Location = new System.Drawing.Point(88, 667);
            this.Label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(125, 30);
            this.Label10.TabIndex = 24;
            this.Label10.Text = "رقم الأرشيف:";
            // 
            // lblBoxName
            // 
            this.lblBoxName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBoxName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblBoxName.ForeColor = System.Drawing.Color.White;
            this.lblBoxName.Location = new System.Drawing.Point(20, 48);
            this.lblBoxName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBoxName.Name = "lblBoxName";
            this.lblBoxName.Size = new System.Drawing.Size(241, 31);
            this.lblBoxName.TabIndex = 1;
            this.lblBoxName.Text = "اسم الصندوق";
            this.lblBoxName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtArchiveNumber
            // 
            this.txtArchiveNumber.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.txtArchiveNumber.BackColor = System.Drawing.Color.White;
            this.txtArchiveNumber.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.txtArchiveNumber.ForeColor = System.Drawing.Color.Red;
            this.txtArchiveNumber.Location = new System.Drawing.Point(36, 697);
            this.txtArchiveNumber.Margin = new System.Windows.Forms.Padding(4);
            this.txtArchiveNumber.Name = "txtArchiveNumber";
            this.txtArchiveNumber.Size = new System.Drawing.Size(224, 35);
            this.txtArchiveNumber.TabIndex = 25;
            this.txtArchiveNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Label1
            // 
            this.Label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.Label1.ForeColor = System.Drawing.Color.White;
            this.Label1.Location = new System.Drawing.Point(20, 3);
            this.Label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(241, 36);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "الصندوق الحالي";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnDecreaseDocumentNumber
            // 
            this.btnDecreaseDocumentNumber.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnDecreaseDocumentNumber.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.btnDecreaseDocumentNumber.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDecreaseDocumentNumber.ForeColor = System.Drawing.Color.Red;
            this.btnDecreaseDocumentNumber.Location = new System.Drawing.Point(36, 739);
            this.btnDecreaseDocumentNumber.Margin = new System.Windows.Forms.Padding(2);
            this.btnDecreaseDocumentNumber.Name = "btnDecreaseDocumentNumber";
            this.btnDecreaseDocumentNumber.Size = new System.Drawing.Size(40, 34);
            this.btnDecreaseDocumentNumber.TabIndex = 36;
            this.btnDecreaseDocumentNumber.Text = "-";
            this.btnDecreaseDocumentNumber.UseVisualStyleBackColor = false;
            // 
            // btnResetSequential
            // 
            this.btnResetSequential.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnResetSequential.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnResetSequential.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnResetSequential.ForeColor = System.Drawing.Color.White;
            this.btnResetSequential.Location = new System.Drawing.Point(82, 739);
            this.btnResetSequential.Margin = new System.Windows.Forms.Padding(2);
            this.btnResetSequential.Name = "btnResetSequential";
            this.btnResetSequential.Size = new System.Drawing.Size(119, 34);
            this.btnResetSequential.TabIndex = 37;
            this.btnResetSequential.Text = "اعادة التعيين";
            this.btnResetSequential.UseVisualStyleBackColor = false;
            // 
            // btnIncreaseDocumentNumber
            // 
            this.btnIncreaseDocumentNumber.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnIncreaseDocumentNumber.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.btnIncreaseDocumentNumber.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIncreaseDocumentNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnIncreaseDocumentNumber.Location = new System.Drawing.Point(206, 739);
            this.btnIncreaseDocumentNumber.Margin = new System.Windows.Forms.Padding(2);
            this.btnIncreaseDocumentNumber.Name = "btnIncreaseDocumentNumber";
            this.btnIncreaseDocumentNumber.Size = new System.Drawing.Size(54, 34);
            this.btnIncreaseDocumentNumber.TabIndex = 35;
            this.btnIncreaseDocumentNumber.Text = "+";
            this.btnIncreaseDocumentNumber.UseVisualStyleBackColor = false;
            // 
            // Panel2
            // 
            this.Panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel2.AutoScroll = true;
            this.Panel2.BackColor = System.Drawing.Color.White;
            this.Panel2.Controls.Add(this.Panel5);
            this.Panel2.Controls.Add(this.GroupBox1);
            this.Panel2.Controls.Add(this.Panel4);
            this.Panel2.Controls.Add(this.Panel3);
            this.Panel2.Controls.Add(this.Label16);
            this.Panel2.Location = new System.Drawing.Point(0, 0);
            this.Panel2.Margin = new System.Windows.Forms.Padding(4);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(1248, 831);
            this.Panel2.TabIndex = 1;
            // 
            // Panel5
            // 
            this.Panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel5.Controls.Add(this.lblSoldiersCount);
            this.Panel5.Controls.Add(this.DataGridView1);
            this.Panel5.Controls.Add(this.btnAddSoldiers);
            this.Panel5.Location = new System.Drawing.Point(2, 656);
            this.Panel5.Name = "Panel5";
            this.Panel5.Size = new System.Drawing.Size(739, 141);
            this.Panel5.TabIndex = 41;
            // 
            // lblSoldiersCount
            // 
            this.lblSoldiersCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSoldiersCount.AutoSize = true;
            this.lblSoldiersCount.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSoldiersCount.Location = new System.Drawing.Point(24, 10);
            this.lblSoldiersCount.Name = "lblSoldiersCount";
            this.lblSoldiersCount.Size = new System.Drawing.Size(211, 25);
            this.lblSoldiersCount.TabIndex = 40;
            this.lblSoldiersCount.Text = "عدد الأشخاص المضافين: 0";
            // 
            // DataGridView1
            // 
            this.DataGridView1.AllowUserToAddRows = false;
            this.DataGridView1.AllowUserToDeleteRows = false;
            this.DataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.DataGridView1.Location = new System.Drawing.Point(7, 40);
            this.DataGridView1.Name = "DataGridView1";
            this.DataGridView1.ReadOnly = true;
            this.DataGridView1.RowHeadersWidth = 62;
            this.DataGridView1.Size = new System.Drawing.Size(726, 95);
            this.DataGridView1.TabIndex = 39;
            // 
            // btnAddSoldiers
            // 
            this.btnAddSoldiers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddSoldiers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.btnAddSoldiers.FlatAppearance.BorderSize = 0;
            this.btnAddSoldiers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddSoldiers.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAddSoldiers.ForeColor = System.Drawing.Color.White;
            this.btnAddSoldiers.Location = new System.Drawing.Point(7, 4);
            this.btnAddSoldiers.Margin = new System.Windows.Forms.Padding(4);
            this.btnAddSoldiers.Name = "btnAddSoldiers";
            this.btnAddSoldiers.Size = new System.Drawing.Size(726, 29);
            this.btnAddSoldiers.TabIndex = 38;
            this.btnAddSoldiers.Text = "👥 أشخاص متعلقين بالوثيقة";
            this.btnAddSoldiers.UseVisualStyleBackColor = false;
            // 
            // GroupBox1
            // 
            this.GroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox1.Controls.Add(this.picImagePreview);
            this.GroupBox1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.GroupBox1.Location = new System.Drawing.Point(0, 3);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(739, 653);
            this.GroupBox1.TabIndex = 35;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "👁️ معاينة المستند";
            // 
            // picImagePreview
            // 
            this.picImagePreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picImagePreview.BackColor = System.Drawing.SystemColors.Control;
            this.picImagePreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picImagePreview.Location = new System.Drawing.Point(11, 28);
            this.picImagePreview.Margin = new System.Windows.Forms.Padding(2);
            this.picImagePreview.Name = "picImagePreview";
            this.picImagePreview.Size = new System.Drawing.Size(708, 612);
            this.picImagePreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picImagePreview.TabIndex = 1;
            this.picImagePreview.TabStop = false;
            this.picImagePreview.Visible = false;
            // 
            // Panel4
            // 
            this.Panel4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.Panel4.Controls.Add(this.lblDocumentInfo);
            this.Panel4.Controls.Add(this.btnInfo);
            this.Panel4.Controls.Add(this.txtTitle);
            this.Panel4.Controls.Add(this.Label15);
            this.Panel4.Controls.Add(this.txtDocumentNumber);
            this.Panel4.Location = new System.Drawing.Point(746, 3);
            this.Panel4.Name = "Panel4";
            this.Panel4.Size = new System.Drawing.Size(502, 86);
            this.Panel4.TabIndex = 0;
            // 
            // lblDocumentInfo
            // 
            this.lblDocumentInfo.AutoSize = true;
            this.lblDocumentInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lblDocumentInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDocumentInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblDocumentInfo.Location = new System.Drawing.Point(86, 10);
            this.lblDocumentInfo.Name = "lblDocumentInfo";
            this.lblDocumentInfo.Size = new System.Drawing.Size(178, 25);
            this.lblDocumentInfo.TabIndex = 43;
            this.lblDocumentInfo.Text = "تم الإنشاء: غير معروف";
            this.lblDocumentInfo.Visible = false;
            // 
            // btnInfo
            // 
            this.btnInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.btnInfo.FlatAppearance.BorderSize = 0;
            this.btnInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnInfo.ForeColor = System.Drawing.Color.White;
            this.btnInfo.Location = new System.Drawing.Point(370, 6);
            this.btnInfo.Name = "btnInfo";
            this.btnInfo.Size = new System.Drawing.Size(119, 26);
            this.btnInfo.TabIndex = 42;
            this.btnInfo.Text = "ℹ️ معلومات";
            this.btnInfo.UseVisualStyleBackColor = false;
            // 
            // txtTitle
            // 
            this.txtTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTitle.BackColor = System.Drawing.Color.White;
            this.txtTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtTitle.Location = new System.Drawing.Point(9, 44);
            this.txtTitle.Margin = new System.Windows.Forms.Padding(4);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(360, 45);
            this.txtTitle.TabIndex = 1;
            this.txtTitle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ToolTip1.SetToolTip(this.txtTitle, "عنوان الوثيقة");
            // 
            // Label15
            // 
            this.Label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label15.AutoSize = true;
            this.Label15.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.Label15.ForeColor = System.Drawing.Color.White;
            this.Label15.Location = new System.Drawing.Point(376, 44);
            this.Label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(133, 30);
            this.Label15.TabIndex = 0;
            this.Label15.Text = "عنوان الوثيقة:";
            // 
            // txtDocumentNumber
            // 
            this.txtDocumentNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDocumentNumber.BackColor = System.Drawing.Color.White;
            this.txtDocumentNumber.Enabled = false;
            this.txtDocumentNumber.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.txtDocumentNumber.ForeColor = System.Drawing.Color.Black;
            this.txtDocumentNumber.Location = new System.Drawing.Point(128, 8);
            this.txtDocumentNumber.Margin = new System.Windows.Forms.Padding(4);
            this.txtDocumentNumber.Name = "txtDocumentNumber";
            this.txtDocumentNumber.Size = new System.Drawing.Size(234, 35);
            this.txtDocumentNumber.TabIndex = 1;
            // 
            // Panel3
            // 
            this.Panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel3.Controls.Add(this.btnAddNewDocument);
            this.Panel3.Controls.Add(this.btnLoadTemplate);
            this.Panel3.Controls.Add(this.btnCopyCurrent);
            this.Panel3.Controls.Add(this.btnSaveDocument);
            this.Panel3.Controls.Add(this.btnClearForm);
            this.Panel3.Controls.Add(this.lblFilePreview);
            this.Panel3.Controls.Add(this.Button1);
            this.Panel3.Controls.Add(this.btnOpenScanner);
            this.Panel3.Controls.Add(this.cmbPriority);
            this.Panel3.Controls.Add(this.txtFilePath);
            this.Panel3.Controls.Add(this.Label14);
            this.Panel3.Controls.Add(this.Label11);
            this.Panel3.Controls.Add(this.Cmbdocument_nature);
            this.Panel3.Controls.Add(this.btnBrowseFile);
            this.Panel3.Controls.Add(this.btnScanDocument);
            this.Panel3.Controls.Add(this.cmbDocumentType);
            this.Panel3.Controls.Add(this.Label17);
            this.Panel3.Controls.Add(this.Label13);
            this.Panel3.Controls.Add(this.btnSave);
            this.Panel3.Controls.Add(this.btnCancel);
            this.Panel3.Controls.Add(this.cmbCategory);
            this.Panel3.Controls.Add(this.Label4);
            this.Panel3.Controls.Add(this.cmbFromDepartment);
            this.Panel3.Controls.Add(this.Label5);
            this.Panel3.Controls.Add(this.cmbToDepartment);
            this.Panel3.Controls.Add(this.Label9);
            this.Panel3.Controls.Add(this.txtNotes);
            this.Panel3.Controls.Add(this.Label6);
            this.Panel3.Controls.Add(this.cmbStatus);
            this.Panel3.Controls.Add(this.txtSummary);
            this.Panel3.Controls.Add(this.dtpIssueDate);
            this.Panel3.Controls.Add(this.Label8);
            this.Panel3.Controls.Add(this.Label7);
            this.Panel3.Controls.Add(this.Label3);
            this.Panel3.Controls.Add(this.dtpDocumentDate);
            this.Panel3.Controls.Add(this.dtpReceiveDate);
            this.Panel3.Controls.Add(this.Label12);
            this.Panel3.Controls.Add(this.Label2);
            this.Panel3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.Panel3.Location = new System.Drawing.Point(746, 87);
            this.Panel3.Name = "Panel3";
            this.Panel3.Size = new System.Drawing.Size(502, 709);
            this.Panel3.TabIndex = 32;
            // 
            // btnAddNewDocument
            // 
            this.btnAddNewDocument.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddNewDocument.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.btnAddNewDocument.FlatAppearance.BorderSize = 0;
            this.btnAddNewDocument.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddNewDocument.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAddNewDocument.ForeColor = System.Drawing.Color.White;
            this.btnAddNewDocument.Location = new System.Drawing.Point(219, 652);
            this.btnAddNewDocument.Margin = new System.Windows.Forms.Padding(4);
            this.btnAddNewDocument.Name = "btnAddNewDocument";
            this.btnAddNewDocument.Size = new System.Drawing.Size(83, 42);
            this.btnAddNewDocument.TabIndex = 45;
            this.btnAddNewDocument.Text = "btnAddNewDocument";
            this.btnAddNewDocument.UseVisualStyleBackColor = false;
            this.btnAddNewDocument.Visible = false;
            // 
            // btnLoadTemplate
            // 
            this.btnLoadTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadTemplate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.btnLoadTemplate.FlatAppearance.BorderSize = 0;
            this.btnLoadTemplate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadTemplate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLoadTemplate.ForeColor = System.Drawing.Color.White;
            this.btnLoadTemplate.Location = new System.Drawing.Point(191, 652);
            this.btnLoadTemplate.Margin = new System.Windows.Forms.Padding(4);
            this.btnLoadTemplate.Name = "btnLoadTemplate";
            this.btnLoadTemplate.Size = new System.Drawing.Size(70, 42);
            this.btnLoadTemplate.TabIndex = 43;
            this.btnLoadTemplate.Text = "btnLoadTemplate";
            this.btnLoadTemplate.UseVisualStyleBackColor = false;
            this.btnLoadTemplate.Visible = false;
            // 
            // btnCopyCurrent
            // 
            this.btnCopyCurrent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopyCurrent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.btnCopyCurrent.FlatAppearance.BorderSize = 0;
            this.btnCopyCurrent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCopyCurrent.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCopyCurrent.ForeColor = System.Drawing.Color.White;
            this.btnCopyCurrent.Location = new System.Drawing.Point(7, 668);
            this.btnCopyCurrent.Margin = new System.Windows.Forms.Padding(4);
            this.btnCopyCurrent.Name = "btnCopyCurrent";
            this.btnCopyCurrent.Size = new System.Drawing.Size(126, 25);
            this.btnCopyCurrent.TabIndex = 46;
            this.btnCopyCurrent.Text = "btnCopyCurrent";
            this.btnCopyCurrent.UseVisualStyleBackColor = false;
            this.btnCopyCurrent.Visible = false;
            // 
            // btnSaveDocument
            // 
            this.btnSaveDocument.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveDocument.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.btnSaveDocument.FlatAppearance.BorderSize = 0;
            this.btnSaveDocument.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveDocument.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSaveDocument.ForeColor = System.Drawing.Color.White;
            this.btnSaveDocument.Location = new System.Drawing.Point(162, 652);
            this.btnSaveDocument.Margin = new System.Windows.Forms.Padding(4);
            this.btnSaveDocument.Name = "btnSaveDocument";
            this.btnSaveDocument.Size = new System.Drawing.Size(64, 42);
            this.btnSaveDocument.TabIndex = 41;
            this.btnSaveDocument.Text = "💾 حفظ المستند";
            this.btnSaveDocument.UseVisualStyleBackColor = false;
            this.btnSaveDocument.Visible = false;
            // 
            // btnClearForm
            // 
            this.btnClearForm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.btnClearForm.FlatAppearance.BorderSize = 0;
            this.btnClearForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearForm.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnClearForm.ForeColor = System.Drawing.Color.White;
            this.btnClearForm.Location = new System.Drawing.Point(381, 652);
            this.btnClearForm.Margin = new System.Windows.Forms.Padding(4);
            this.btnClearForm.Name = "btnClearForm";
            this.btnClearForm.Size = new System.Drawing.Size(102, 42);
            this.btnClearForm.TabIndex = 44;
            this.btnClearForm.Text = "btnClearForm";
            this.btnClearForm.UseVisualStyleBackColor = false;
            this.btnClearForm.Visible = false;
            // 
            // lblFilePreview
            // 
            this.lblFilePreview.AutoSize = true;
            this.lblFilePreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lblFilePreview.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilePreview.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblFilePreview.Location = new System.Drawing.Point(14, 15);
            this.lblFilePreview.Name = "lblFilePreview";
            this.lblFilePreview.Size = new System.Drawing.Size(178, 25);
            this.lblFilePreview.TabIndex = 44;
            this.lblFilePreview.Text = "تم الإنشاء: غير معروف";
            // 
            // Button1
            // 
            this.Button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.Button1.FlatAppearance.BorderSize = 0;
            this.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.Button1.ForeColor = System.Drawing.Color.White;
            this.Button1.Location = new System.Drawing.Point(322, 652);
            this.Button1.Margin = new System.Windows.Forms.Padding(4);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(43, 42);
            this.Button1.TabIndex = 42;
            this.Button1.Text = "📷 فتح الماسح";
            this.Button1.UseVisualStyleBackColor = false;
            this.Button1.Visible = false;
            // 
            // btnOpenScanner
            // 
            this.btnOpenScanner.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenScanner.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.btnOpenScanner.FlatAppearance.BorderSize = 0;
            this.btnOpenScanner.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenScanner.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnOpenScanner.ForeColor = System.Drawing.Color.White;
            this.btnOpenScanner.Location = new System.Drawing.Point(352, 652);
            this.btnOpenScanner.Margin = new System.Windows.Forms.Padding(4);
            this.btnOpenScanner.Name = "btnOpenScanner";
            this.btnOpenScanner.Size = new System.Drawing.Size(68, 42);
            this.btnOpenScanner.TabIndex = 37;
            this.btnOpenScanner.Text = "📷 فتح الماسح";
            this.btnOpenScanner.UseVisualStyleBackColor = false;
            this.btnOpenScanner.Visible = false;
            // 
            // cmbPriority
            // 
            this.cmbPriority.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbPriority.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPriority.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.cmbPriority.FormattingEnabled = true;
            this.cmbPriority.Items.AddRange(new object[] {
            "عادية",
            "مهمة",
            "عاجلة"});
            this.cmbPriority.Location = new System.Drawing.Point(190, 223);
            this.cmbPriority.Margin = new System.Windows.Forms.Padding(4);
            this.cmbPriority.Name = "cmbPriority";
            this.cmbPriority.Size = new System.Drawing.Size(222, 37);
            this.cmbPriority.TabIndex = 17;
            // 
            // txtFilePath
            // 
            this.txtFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilePath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtFilePath.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtFilePath.ForeColor = System.Drawing.Color.White;
            this.txtFilePath.Location = new System.Drawing.Point(14, 586);
            this.txtFilePath.Margin = new System.Windows.Forms.Padding(4);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.ReadOnly = true;
            this.txtFilePath.Size = new System.Drawing.Size(471, 34);
            this.txtFilePath.TabIndex = 27;
            // 
            // Label14
            // 
            this.Label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label14.AutoSize = true;
            this.Label14.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.Label14.ForeColor = System.Drawing.Color.Blue;
            this.Label14.Location = new System.Drawing.Point(382, 17);
            this.Label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(108, 28);
            this.Label14.TabIndex = 2;
            this.Label14.Text = "نوع الوثيقة:";
            this.ToolTip1.SetToolTip(this.Label14, "يمكنك النقر هنا لاضافة انواع جديدة");
            // 
            // Label11
            // 
            this.Label11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label11.AutoSize = true;
            this.Label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.Label11.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.Label11.ForeColor = System.Drawing.Color.White;
            this.Label11.Location = new System.Drawing.Point(25, 57);
            this.Label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(121, 30);
            this.Label11.TabIndex = 26;
            this.Label11.Text = "مسار الملف:";
            this.Label11.Visible = false;
            // 
            // Cmbdocument_nature
            // 
            this.Cmbdocument_nature.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Cmbdocument_nature.Cursor = System.Windows.Forms.Cursors.Cross;
            this.Cmbdocument_nature.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cmbdocument_nature.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.Cmbdocument_nature.FormattingEnabled = true;
            this.Cmbdocument_nature.Items.AddRange(new object[] {
            "داخلية",
            "خارجية"});
            this.Cmbdocument_nature.Location = new System.Drawing.Point(190, 508);
            this.Cmbdocument_nature.Name = "Cmbdocument_nature";
            this.Cmbdocument_nature.Size = new System.Drawing.Size(128, 37);
            this.Cmbdocument_nature.TabIndex = 34;
            // 
            // btnBrowseFile
            // 
            this.btnBrowseFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.btnBrowseFile.FlatAppearance.BorderSize = 0;
            this.btnBrowseFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseFile.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnBrowseFile.ForeColor = System.Drawing.Color.White;
            this.btnBrowseFile.Location = new System.Drawing.Point(7, 238);
            this.btnBrowseFile.Margin = new System.Windows.Forms.Padding(4);
            this.btnBrowseFile.Name = "btnBrowseFile";
            this.btnBrowseFile.Size = new System.Drawing.Size(165, 42);
            this.btnBrowseFile.TabIndex = 28;
            this.btnBrowseFile.Text = "📂 استعراض ملف";
            this.btnBrowseFile.UseVisualStyleBackColor = false;
            // 
            // btnScanDocument
            // 
            this.btnScanDocument.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnScanDocument.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.btnScanDocument.FlatAppearance.BorderSize = 0;
            this.btnScanDocument.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnScanDocument.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnScanDocument.ForeColor = System.Drawing.Color.White;
            this.btnScanDocument.Location = new System.Drawing.Point(7, 188);
            this.btnScanDocument.Margin = new System.Windows.Forms.Padding(4);
            this.btnScanDocument.Name = "btnScanDocument";
            this.btnScanDocument.Size = new System.Drawing.Size(165, 42);
            this.btnScanDocument.TabIndex = 36;
            this.btnScanDocument.Text = "📄 سكانر";
            this.btnScanDocument.UseVisualStyleBackColor = false;
            // 
            // cmbDocumentType
            // 
            this.cmbDocumentType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbDocumentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDocumentType.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.cmbDocumentType.FormattingEnabled = true;
            this.cmbDocumentType.Location = new System.Drawing.Point(191, 15);
            this.cmbDocumentType.Margin = new System.Windows.Forms.Padding(4);
            this.cmbDocumentType.Name = "cmbDocumentType";
            this.cmbDocumentType.Size = new System.Drawing.Size(182, 37);
            this.cmbDocumentType.TabIndex = 3;
            // 
            // Label17
            // 
            this.Label17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label17.AutoSize = true;
            this.Label17.BackColor = System.Drawing.Color.Transparent;
            this.Label17.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.Label17.ForeColor = System.Drawing.Color.Black;
            this.Label17.Location = new System.Drawing.Point(290, 513);
            this.Label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label17.Name = "Label17";
            this.Label17.Size = new System.Drawing.Size(181, 28);
            this.Label17.TabIndex = 33;
            this.Label17.Text = "نوع الوثيقة (طبيعة):";
            // 
            // Label13
            // 
            this.Label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label13.AutoSize = true;
            this.Label13.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.Label13.ForeColor = System.Drawing.Color.Blue;
            this.Label13.Location = new System.Drawing.Point(393, 60);
            this.Label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(90, 28);
            this.Label13.TabIndex = 4;
            this.Label13.Text = "التصنيف:";
            this.ToolTip1.SetToolTip(this.Label13, "يمكنك النقرر هنا لاضافة اصناف جديدة");
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(0)))));
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(4, 287);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(168, 42);
            this.btnSave.TabIndex = 29;
            this.btnSave.Text = "💾 حفظ";
            this.btnSave.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(4, 336);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(168, 42);
            this.btnCancel.TabIndex = 30;
            this.btnCancel.Text = "❌ إلغاء";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // cmbCategory
            // 
            this.cmbCategory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Location = new System.Drawing.Point(191, 57);
            this.cmbCategory.Margin = new System.Windows.Forms.Padding(4);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(182, 37);
            this.cmbCategory.TabIndex = 5;
            // 
            // Label4
            // 
            this.Label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label4.AutoSize = true;
            this.Label4.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.Label4.ForeColor = System.Drawing.Color.Blue;
            this.Label4.Location = new System.Drawing.Point(404, 101);
            this.Label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(77, 28);
            this.Label4.TabIndex = 12;
            this.Label4.Text = "المرسل:";
            this.ToolTip1.SetToolTip(this.Label4, "يمكنك النقر هنا لاضافة اقسام جديدة");
            // 
            // cmbFromDepartment
            // 
            this.cmbFromDepartment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbFromDepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFromDepartment.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.cmbFromDepartment.FormattingEnabled = true;
            this.cmbFromDepartment.Location = new System.Drawing.Point(191, 98);
            this.cmbFromDepartment.Margin = new System.Windows.Forms.Padding(4);
            this.cmbFromDepartment.Name = "cmbFromDepartment";
            this.cmbFromDepartment.Size = new System.Drawing.Size(182, 37);
            this.cmbFromDepartment.TabIndex = 13;
            // 
            // Label5
            // 
            this.Label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label5.AutoSize = true;
            this.Label5.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.Label5.ForeColor = System.Drawing.Color.Black;
            this.Label5.Location = new System.Drawing.Point(386, 142);
            this.Label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(114, 28);
            this.Label5.TabIndex = 14;
            this.Label5.Text = " الموجه إليه:";
            // 
            // cmbToDepartment
            // 
            this.cmbToDepartment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbToDepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbToDepartment.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.cmbToDepartment.FormattingEnabled = true;
            this.cmbToDepartment.Location = new System.Drawing.Point(190, 140);
            this.cmbToDepartment.Margin = new System.Windows.Forms.Padding(4);
            this.cmbToDepartment.Name = "cmbToDepartment";
            this.cmbToDepartment.Size = new System.Drawing.Size(184, 37);
            this.cmbToDepartment.TabIndex = 15;
            // 
            // Label9
            // 
            this.Label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label9.AutoSize = true;
            this.Label9.BackColor = System.Drawing.Color.Transparent;
            this.Label9.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.Label9.ForeColor = System.Drawing.Color.Black;
            this.Label9.Location = new System.Drawing.Point(403, 457);
            this.Label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(93, 28);
            this.Label9.TabIndex = 22;
            this.Label9.Text = "ملاحظات:";
            // 
            // txtNotes
            // 
            this.txtNotes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNotes.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNotes.Location = new System.Drawing.Point(8, 454);
            this.txtNotes.Margin = new System.Windows.Forms.Padding(4);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNotes.Size = new System.Drawing.Size(386, 48);
            this.txtNotes.TabIndex = 23;
            // 
            // Label6
            // 
            this.Label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label6.AutoSize = true;
            this.Label6.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.Label6.ForeColor = System.Drawing.Color.Black;
            this.Label6.Location = new System.Drawing.Point(423, 180);
            this.Label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(63, 28);
            this.Label6.TabIndex = 16;
            this.Label6.Text = "الحالة:";
            // 
            // cmbStatus
            // 
            this.cmbStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Items.AddRange(new object[] {
            "قيد المراجعة",
            "معتمد",
            "مرفوض",
            "منفذ",
            "ملغي",
            "مؤرشف"});
            this.cmbStatus.Location = new System.Drawing.Point(190, 181);
            this.cmbStatus.Margin = new System.Windows.Forms.Padding(4);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(222, 37);
            this.cmbStatus.TabIndex = 31;
            // 
            // txtSummary
            // 
            this.txtSummary.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSummary.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSummary.Location = new System.Drawing.Point(8, 389);
            this.txtSummary.Margin = new System.Windows.Forms.Padding(4);
            this.txtSummary.Multiline = true;
            this.txtSummary.Name = "txtSummary";
            this.txtSummary.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSummary.Size = new System.Drawing.Size(386, 64);
            this.txtSummary.TabIndex = 21;
            // 
            // dtpIssueDate
            // 
            this.dtpIssueDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpIssueDate.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.dtpIssueDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpIssueDate.Location = new System.Drawing.Point(190, 345);
            this.dtpIssueDate.Margin = new System.Windows.Forms.Padding(4);
            this.dtpIssueDate.Name = "dtpIssueDate";
            this.dtpIssueDate.Size = new System.Drawing.Size(170, 36);
            this.dtpIssueDate.TabIndex = 11;
            // 
            // Label8
            // 
            this.Label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label8.AutoSize = true;
            this.Label8.BackColor = System.Drawing.Color.Transparent;
            this.Label8.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.Label8.ForeColor = System.Drawing.Color.Black;
            this.Label8.Location = new System.Drawing.Point(410, 389);
            this.Label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(75, 28);
            this.Label8.TabIndex = 20;
            this.Label8.Text = "ملخص:";
            // 
            // Label7
            // 
            this.Label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label7.AutoSize = true;
            this.Label7.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.Label7.ForeColor = System.Drawing.Color.Black;
            this.Label7.Location = new System.Drawing.Point(416, 226);
            this.Label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(80, 28);
            this.Label7.TabIndex = 18;
            this.Label7.Text = "الأولوية:";
            // 
            // Label3
            // 
            this.Label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label3.AutoSize = true;
            this.Label3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.Label3.ForeColor = System.Drawing.Color.Black;
            this.Label3.Location = new System.Drawing.Point(375, 349);
            this.Label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(124, 28);
            this.Label3.TabIndex = 10;
            this.Label3.Text = "تاريخ الإصدار:";
            // 
            // dtpDocumentDate
            // 
            this.dtpDocumentDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpDocumentDate.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.dtpDocumentDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDocumentDate.Location = new System.Drawing.Point(190, 266);
            this.dtpDocumentDate.Margin = new System.Windows.Forms.Padding(4);
            this.dtpDocumentDate.Name = "dtpDocumentDate";
            this.dtpDocumentDate.Size = new System.Drawing.Size(170, 36);
            this.dtpDocumentDate.TabIndex = 7;
            // 
            // dtpReceiveDate
            // 
            this.dtpReceiveDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpReceiveDate.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.dtpReceiveDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpReceiveDate.Location = new System.Drawing.Point(190, 303);
            this.dtpReceiveDate.Margin = new System.Windows.Forms.Padding(4);
            this.dtpReceiveDate.Name = "dtpReceiveDate";
            this.dtpReceiveDate.Size = new System.Drawing.Size(170, 36);
            this.dtpReceiveDate.TabIndex = 9;
            // 
            // Label12
            // 
            this.Label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label12.AutoSize = true;
            this.Label12.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.Label12.ForeColor = System.Drawing.Color.Black;
            this.Label12.Location = new System.Drawing.Point(379, 270);
            this.Label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(120, 28);
            this.Label12.TabIndex = 6;
            this.Label12.Text = "تاريخ الوثيقة:";
            // 
            // Label2
            // 
            this.Label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label2.AutoSize = true;
            this.Label2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.Label2.ForeColor = System.Drawing.Color.Black;
            this.Label2.Location = new System.Drawing.Point(369, 307);
            this.Label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(129, 28);
            this.Label2.TabIndex = 8;
            this.Label2.Text = "تاريخ الاستلام:";
            // 
            // Label16
            // 
            this.Label16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label16.AutoSize = true;
            this.Label16.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.Label16.ForeColor = System.Drawing.Color.White;
            this.Label16.Location = new System.Drawing.Point(286, 30);
            this.Label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label16.Name = "Label16";
            this.Label16.Size = new System.Drawing.Size(114, 30);
            this.Label16.TabIndex = 0;
            this.Label16.Text = "رقم الوثيقة:";
            // 
            // OpenFileDialog1
            // 
            this.OpenFileDialog1.FileName = "OpenFileDialog1";
            // 
            // ToolTip1
            // 
            this.ToolTip1.AutoPopDelay = 5000;
            this.ToolTip1.InitialDelay = 500;
            this.ToolTip1.ReshowDelay = 100;
            // 
            // FrmAddDocument
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1547, 798);
            this.Controls.Add(this.Panel2);
            this.Controls.Add(this.Panel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1562, 786);
            this.Name = "FrmAddDocument";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "إضافة وثيقة جديدة";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxImage)).EndInit();
            this.Panel2.ResumeLayout(false);
            this.Panel2.PerformLayout();
            this.Panel5.ResumeLayout(false);
            this.Panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).EndInit();
            this.GroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picImagePreview)).EndInit();
            this.Panel4.ResumeLayout(false);
            this.Panel4.PerformLayout();
            this.Panel3.ResumeLayout(false);
            this.Panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        // Control declarations
        private System.Windows.Forms.Panel Panel1;
        private System.Windows.Forms.Panel Panel2;
        private System.Windows.Forms.TextBox txtDocumentNumber;
        private System.Windows.Forms.Label Label16;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label Label15;
        private System.Windows.Forms.ComboBox cmbDocumentType;
        private System.Windows.Forms.Label Label14;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.Label Label13;
        private System.Windows.Forms.DateTimePicker dtpDocumentDate;
        private System.Windows.Forms.Label Label12;
        private System.Windows.Forms.DateTimePicker dtpReceiveDate;
        private System.Windows.Forms.Label Label2;
        private System.Windows.Forms.DateTimePicker dtpIssueDate;
        private System.Windows.Forms.Label Label3;
        private System.Windows.Forms.ComboBox cmbFromDepartment;
        private System.Windows.Forms.Label Label4;
        private System.Windows.Forms.ComboBox cmbToDepartment;
        private System.Windows.Forms.Label Label5;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Label Label6;
        private System.Windows.Forms.ComboBox cmbPriority;
        private System.Windows.Forms.Label Label7;
        private System.Windows.Forms.TextBox txtSummary;
        private System.Windows.Forms.Label Label8;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Label Label9;
        private System.Windows.Forms.TextBox txtArchiveNumber;
        private System.Windows.Forms.Label Label10;
        private System.Windows.Forms.Button btnBrowseFile;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Label Label11;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.PictureBox picBoxImage;
        private System.Windows.Forms.Label lblBoxName;
        private System.Windows.Forms.Label Label1;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog1;
        private System.Windows.Forms.Panel Panel3;
        private System.Windows.Forms.Panel Panel4;
        private System.Windows.Forms.GroupBox GroupBox1;
        private System.Windows.Forms.ComboBox cmbcangeboxs;
        private System.Windows.Forms.ComboBox Cmbdocument_nature;
        private System.Windows.Forms.Label Label17;
        private System.Windows.Forms.Button btnScanDocument;
        private System.Windows.Forms.Button btnOpenScanner;
        private System.Windows.Forms.Button btnAddSoldiers;
        private System.Windows.Forms.DataGridView DataGridView1;
        private System.Windows.Forms.Label lblSoldiersCount;
        private System.Windows.Forms.Panel Panel5;
        private System.Windows.Forms.Button btnSaveDocument;
        private System.Windows.Forms.PictureBox picImagePreview;
        private System.Windows.Forms.Button btnInfo;
        private System.Windows.Forms.ToolTip ToolTip1;
        private System.Windows.Forms.Label lblDocumentInfo;
        private System.Windows.Forms.Button Button1;
        private System.Windows.Forms.Button btnIncreaseDocumentNumber;
        private System.Windows.Forms.Label lblFilePreview;
        private System.Windows.Forms.Button btnDecreaseDocumentNumber;
        private System.Windows.Forms.Button btnResetSequential;
        private System.Windows.Forms.Button btnAddNewDocument;
        private System.Windows.Forms.Button btnClearForm;
        private System.Windows.Forms.Button btnLoadTemplate;
        private System.Windows.Forms.Button btnCopyCurrent;
    }
}