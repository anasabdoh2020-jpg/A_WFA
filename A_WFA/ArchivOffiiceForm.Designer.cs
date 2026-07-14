using System.Windows.Forms;

namespace A_WFA
{
    partial class ArchivOffiiceForm
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.PanelSideMenu = new System.Windows.Forms.Panel();
            this.BtnExit = new System.Windows.Forms.Button();
            this.BtnSettingf = new System.Windows.Forms.Button();
            this.BtnDepartments = new System.Windows.Forms.Button();
            this.BtnCategories = new System.Windows.Forms.Button();
            this.BtnAdvancedSearch = new System.Windows.Forms.Button();
            this.BtnManagType = new System.Windows.Forms.Button();
            this.BtnRefresh = new System.Windows.Forms.Button();
            this.BtnFilterInactive = new System.Windows.Forms.Button();
            this.BtnFilterActive = new System.Windows.Forms.Button();
            this.BtnFilterAll = new System.Windows.Forms.Button();
            this.BtnManagBox = new System.Windows.Forms.Button();
            this.BtnDashboard = new System.Windows.Forms.Button();
            this.PanelLogo = new System.Windows.Forms.Panel();
            this.LblLogo = new System.Windows.Forms.Label();
            this.BtnToggleMenu = new System.Windows.Forms.Button();
            this.PanelMain = new System.Windows.Forms.Panel();
            this.FlowBoxes = new System.Windows.Forms.FlowLayoutPanel();
            this.PanelStats = new System.Windows.Forms.Panel();
            this.FlowStats = new System.Windows.Forms.FlowLayoutPanel();
            this.CardActiveDocs = new System.Windows.Forms.Panel();
            this.CardActiveBoxes = new System.Windows.Forms.Panel();
            this.CardTotalBoxes = new System.Windows.Forms.Panel();
            this.CardTotalDocs = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.PanelSearch = new System.Windows.Forms.Panel();
            this.TxtSearch = new System.Windows.Forms.TextBox();
            this.PanelSideMenu.SuspendLayout();
            this.PanelLogo.SuspendLayout();
            this.PanelMain.SuspendLayout();
            this.PanelStats.SuspendLayout();
            this.FlowStats.SuspendLayout();
            this.PanelSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelSideMenu
            // 
            this.PanelSideMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.PanelSideMenu.Controls.Add(this.BtnExit);
            this.PanelSideMenu.Controls.Add(this.BtnSettingf);
            this.PanelSideMenu.Controls.Add(this.BtnDepartments);
            this.PanelSideMenu.Controls.Add(this.BtnCategories);
            this.PanelSideMenu.Controls.Add(this.BtnAdvancedSearch);
            this.PanelSideMenu.Controls.Add(this.BtnManagType);
            this.PanelSideMenu.Controls.Add(this.BtnRefresh);
            this.PanelSideMenu.Controls.Add(this.BtnFilterInactive);
            this.PanelSideMenu.Controls.Add(this.BtnFilterActive);
            this.PanelSideMenu.Controls.Add(this.BtnFilterAll);
            this.PanelSideMenu.Controls.Add(this.BtnManagBox);
            this.PanelSideMenu.Controls.Add(this.BtnDashboard);
            this.PanelSideMenu.Controls.Add(this.PanelLogo);
            this.PanelSideMenu.Controls.Add(this.BtnToggleMenu);
            this.PanelSideMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.PanelSideMenu.Location = new System.Drawing.Point(0, 0);
            this.PanelSideMenu.MinimumSize = new System.Drawing.Size(60, 0);
            this.PanelSideMenu.Name = "PanelSideMenu";
            this.PanelSideMenu.Size = new System.Drawing.Size(60, 700);
            this.PanelSideMenu.TabIndex = 0;
            // 
            // BtnExit
            // 
            this.BtnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.BtnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnExit.Dock = System.Windows.Forms.DockStyle.Top;
            this.BtnExit.FlatAppearance.BorderSize = 0;
            this.BtnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnExit.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.BtnExit.ForeColor = System.Drawing.Color.White;
            this.BtnExit.Location = new System.Drawing.Point(0, 625);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(60, 45);
            this.BtnExit.TabIndex = 13;
            this.BtnExit.Text = "🚪";
            this.BtnExit.UseVisualStyleBackColor = false;
            // 
            // BtnSettingf
            // 
            this.BtnSettingf.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.BtnSettingf.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnSettingf.Dock = System.Windows.Forms.DockStyle.Top;
            this.BtnSettingf.FlatAppearance.BorderSize = 0;
            this.BtnSettingf.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnSettingf.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.BtnSettingf.ForeColor = System.Drawing.Color.White;
            this.BtnSettingf.Location = new System.Drawing.Point(0, 580);
            this.BtnSettingf.Name = "BtnSettingf";
            this.BtnSettingf.Size = new System.Drawing.Size(60, 45);
            this.BtnSettingf.TabIndex = 12;
            this.BtnSettingf.Text = "🔙";
            this.BtnSettingf.UseVisualStyleBackColor = false;
            // 
            // BtnDepartments
            // 
            this.BtnDepartments.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.BtnDepartments.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnDepartments.Dock = System.Windows.Forms.DockStyle.Top;
            this.BtnDepartments.FlatAppearance.BorderSize = 0;
            this.BtnDepartments.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnDepartments.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.BtnDepartments.ForeColor = System.Drawing.Color.White;
            this.BtnDepartments.Location = new System.Drawing.Point(0, 535);
            this.BtnDepartments.Name = "BtnDepartments";
            this.BtnDepartments.Size = new System.Drawing.Size(60, 45);
            this.BtnDepartments.TabIndex = 11;
            this.BtnDepartments.Text = "🏢";
            this.BtnDepartments.UseVisualStyleBackColor = false;
            // 
            // BtnCategories
            // 
            this.BtnCategories.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.BtnCategories.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnCategories.Dock = System.Windows.Forms.DockStyle.Top;
            this.BtnCategories.FlatAppearance.BorderSize = 0;
            this.BtnCategories.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnCategories.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.BtnCategories.ForeColor = System.Drawing.Color.White;
            this.BtnCategories.Location = new System.Drawing.Point(0, 490);
            this.BtnCategories.Name = "BtnCategories";
            this.BtnCategories.Size = new System.Drawing.Size(60, 45);
            this.BtnCategories.TabIndex = 10;
            this.BtnCategories.Text = "📂";
            this.BtnCategories.UseVisualStyleBackColor = false;
            this.BtnCategories.Click += new System.EventHandler(this.BtnCategories_Click);
            // 
            // BtnAdvancedSearch
            // 
            this.BtnAdvancedSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.BtnAdvancedSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnAdvancedSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.BtnAdvancedSearch.FlatAppearance.BorderSize = 0;
            this.BtnAdvancedSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnAdvancedSearch.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.BtnAdvancedSearch.ForeColor = System.Drawing.Color.White;
            this.BtnAdvancedSearch.Location = new System.Drawing.Point(0, 445);
            this.BtnAdvancedSearch.Name = "BtnAdvancedSearch";
            this.BtnAdvancedSearch.Size = new System.Drawing.Size(60, 45);
            this.BtnAdvancedSearch.TabIndex = 9;
            this.BtnAdvancedSearch.Text = "🔍";
            this.BtnAdvancedSearch.UseVisualStyleBackColor = false;
            // 
            // BtnManagType
            // 
            this.BtnManagType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.BtnManagType.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnManagType.Dock = System.Windows.Forms.DockStyle.Top;
            this.BtnManagType.FlatAppearance.BorderSize = 0;
            this.BtnManagType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnManagType.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.BtnManagType.ForeColor = System.Drawing.Color.White;
            this.BtnManagType.Location = new System.Drawing.Point(0, 400);
            this.BtnManagType.Name = "BtnManagType";
            this.BtnManagType.Size = new System.Drawing.Size(60, 45);
            this.BtnManagType.TabIndex = 8;
            this.BtnManagType.Text = "📊";
            this.BtnManagType.UseVisualStyleBackColor = false;
            // 
            // BtnRefresh
            // 
            this.BtnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.BtnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnRefresh.Dock = System.Windows.Forms.DockStyle.Top;
            this.BtnRefresh.FlatAppearance.BorderSize = 0;
            this.BtnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnRefresh.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.BtnRefresh.ForeColor = System.Drawing.Color.White;
            this.BtnRefresh.Location = new System.Drawing.Point(0, 355);
            this.BtnRefresh.Name = "BtnRefresh";
            this.BtnRefresh.Size = new System.Drawing.Size(60, 45);
            this.BtnRefresh.TabIndex = 7;
            this.BtnRefresh.Text = "🔄";
            this.BtnRefresh.UseVisualStyleBackColor = false;
            // 
            // BtnFilterInactive
            // 
            this.BtnFilterInactive.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.BtnFilterInactive.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnFilterInactive.Dock = System.Windows.Forms.DockStyle.Top;
            this.BtnFilterInactive.FlatAppearance.BorderSize = 0;
            this.BtnFilterInactive.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnFilterInactive.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.BtnFilterInactive.ForeColor = System.Drawing.Color.White;
            this.BtnFilterInactive.Location = new System.Drawing.Point(0, 310);
            this.BtnFilterInactive.Name = "BtnFilterInactive";
            this.BtnFilterInactive.Size = new System.Drawing.Size(60, 45);
            this.BtnFilterInactive.TabIndex = 6;
            this.BtnFilterInactive.Text = "🔴";
            this.BtnFilterInactive.UseVisualStyleBackColor = false;
            // 
            // BtnFilterActive
            // 
            this.BtnFilterActive.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.BtnFilterActive.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnFilterActive.Dock = System.Windows.Forms.DockStyle.Top;
            this.BtnFilterActive.FlatAppearance.BorderSize = 0;
            this.BtnFilterActive.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnFilterActive.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.BtnFilterActive.ForeColor = System.Drawing.Color.White;
            this.BtnFilterActive.Location = new System.Drawing.Point(0, 265);
            this.BtnFilterActive.Name = "BtnFilterActive";
            this.BtnFilterActive.Size = new System.Drawing.Size(60, 45);
            this.BtnFilterActive.TabIndex = 5;
            this.BtnFilterActive.Text = "🟢";
            this.BtnFilterActive.UseVisualStyleBackColor = false;
            // 
            // BtnFilterAll
            // 
            this.BtnFilterAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.BtnFilterAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnFilterAll.Dock = System.Windows.Forms.DockStyle.Top;
            this.BtnFilterAll.FlatAppearance.BorderSize = 0;
            this.BtnFilterAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnFilterAll.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.BtnFilterAll.ForeColor = System.Drawing.Color.White;
            this.BtnFilterAll.Location = new System.Drawing.Point(0, 220);
            this.BtnFilterAll.Name = "BtnFilterAll";
            this.BtnFilterAll.Size = new System.Drawing.Size(60, 45);
            this.BtnFilterAll.TabIndex = 4;
            this.BtnFilterAll.Text = "📋";
            this.BtnFilterAll.UseVisualStyleBackColor = false;
            // 
            // BtnManagBox
            // 
            this.BtnManagBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.BtnManagBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnManagBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.BtnManagBox.FlatAppearance.BorderSize = 0;
            this.BtnManagBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnManagBox.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.BtnManagBox.ForeColor = System.Drawing.Color.White;
            this.BtnManagBox.Location = new System.Drawing.Point(0, 175);
            this.BtnManagBox.Name = "BtnManagBox";
            this.BtnManagBox.Size = new System.Drawing.Size(60, 45);
            this.BtnManagBox.TabIndex = 3;
            this.BtnManagBox.Text = "📦";
            this.BtnManagBox.UseVisualStyleBackColor = false;
            // 
            // BtnDashboard
            // 
            this.BtnDashboard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.BtnDashboard.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnDashboard.Dock = System.Windows.Forms.DockStyle.Top;
            this.BtnDashboard.FlatAppearance.BorderSize = 0;
            this.BtnDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnDashboard.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.BtnDashboard.ForeColor = System.Drawing.Color.White;
            this.BtnDashboard.Location = new System.Drawing.Point(0, 130);
            this.BtnDashboard.Name = "BtnDashboard";
            this.BtnDashboard.Size = new System.Drawing.Size(60, 45);
            this.BtnDashboard.TabIndex = 2;
            this.BtnDashboard.Text = "🏠";
            this.BtnDashboard.UseVisualStyleBackColor = false;
            // 
            // PanelLogo
            // 
            this.PanelLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.PanelLogo.Controls.Add(this.LblLogo);
            this.PanelLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelLogo.Location = new System.Drawing.Point(0, 50);
            this.PanelLogo.Name = "PanelLogo";
            this.PanelLogo.Size = new System.Drawing.Size(60, 80);
            this.PanelLogo.TabIndex = 1;
            // 
            // LblLogo
            // 
            this.LblLogo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblLogo.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.LblLogo.ForeColor = System.Drawing.Color.White;
            this.LblLogo.Location = new System.Drawing.Point(0, 0);
            this.LblLogo.Name = "LblLogo";
            this.LblLogo.Size = new System.Drawing.Size(60, 80);
            this.LblLogo.TabIndex = 0;
            this.LblLogo.Text = "📁";
            this.LblLogo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BtnToggleMenu
            // 
            this.BtnToggleMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.BtnToggleMenu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnToggleMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.BtnToggleMenu.FlatAppearance.BorderSize = 0;
            this.BtnToggleMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnToggleMenu.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.BtnToggleMenu.ForeColor = System.Drawing.Color.White;
            this.BtnToggleMenu.Location = new System.Drawing.Point(0, 0);
            this.BtnToggleMenu.Name = "BtnToggleMenu";
            this.BtnToggleMenu.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.BtnToggleMenu.Size = new System.Drawing.Size(60, 50);
            this.BtnToggleMenu.TabIndex = 0;
            this.BtnToggleMenu.Text = "☰";
            this.BtnToggleMenu.UseVisualStyleBackColor = false;
            // 
            // PanelMain
            // 
            this.PanelMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.PanelMain.Controls.Add(this.FlowBoxes);
            this.PanelMain.Controls.Add(this.PanelStats);
            this.PanelMain.Controls.Add(this.PanelSearch);
            this.PanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelMain.Location = new System.Drawing.Point(60, 0);
            this.PanelMain.Name = "PanelMain";
            this.PanelMain.Padding = new System.Windows.Forms.Padding(10);
            this.PanelMain.Size = new System.Drawing.Size(1140, 700);
            this.PanelMain.TabIndex = 1;
            // 
            // FlowBoxes
            // 
            this.FlowBoxes.AutoScroll = true;
            this.FlowBoxes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.FlowBoxes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FlowBoxes.Location = new System.Drawing.Point(10, 65);
            this.FlowBoxes.Name = "FlowBoxes";
            this.FlowBoxes.Padding = new System.Windows.Forms.Padding(10);
            this.FlowBoxes.Size = new System.Drawing.Size(1120, 575);
            this.FlowBoxes.TabIndex = 3;
            // 
            // PanelStats
            // 
            this.PanelStats.BackColor = System.Drawing.Color.White;
            this.PanelStats.Controls.Add(this.FlowStats);
            this.PanelStats.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PanelStats.Location = new System.Drawing.Point(10, 640);
            this.PanelStats.Name = "PanelStats";
            this.PanelStats.Padding = new System.Windows.Forms.Padding(10);
            this.PanelStats.Size = new System.Drawing.Size(1120, 50);
            this.PanelStats.TabIndex = 2;
            // 
            // FlowStats
            // 
            this.FlowStats.Controls.Add(this.CardActiveDocs);
            this.FlowStats.Controls.Add(this.CardActiveBoxes);
            this.FlowStats.Controls.Add(this.CardTotalBoxes);
            this.FlowStats.Controls.Add(this.CardTotalDocs);
            this.FlowStats.Controls.Add(this.button1);
            this.FlowStats.Controls.Add(this.button2);
            this.FlowStats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FlowStats.Location = new System.Drawing.Point(10, 10);
            this.FlowStats.Name = "FlowStats";
            this.FlowStats.Size = new System.Drawing.Size(1100, 30);
            this.FlowStats.TabIndex = 0;
            // 
            // CardActiveDocs
            // 
            this.CardActiveDocs.BackColor = System.Drawing.Color.White;
            this.CardActiveDocs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CardActiveDocs.Location = new System.Drawing.Point(903, 3);
            this.CardActiveDocs.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.CardActiveDocs.Name = "CardActiveDocs";
            this.CardActiveDocs.Size = new System.Drawing.Size(192, 24);
            this.CardActiveDocs.TabIndex = 3;
            // 
            // CardActiveBoxes
            // 
            this.CardActiveBoxes.BackColor = System.Drawing.Color.White;
            this.CardActiveBoxes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CardActiveBoxes.Location = new System.Drawing.Point(701, 3);
            this.CardActiveBoxes.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.CardActiveBoxes.Name = "CardActiveBoxes";
            this.CardActiveBoxes.Size = new System.Drawing.Size(192, 24);
            this.CardActiveBoxes.TabIndex = 1;
            // 
            // CardTotalBoxes
            // 
            this.CardTotalBoxes.BackColor = System.Drawing.Color.White;
            this.CardTotalBoxes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CardTotalBoxes.Location = new System.Drawing.Point(499, 3);
            this.CardTotalBoxes.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.CardTotalBoxes.Name = "CardTotalBoxes";
            this.CardTotalBoxes.Size = new System.Drawing.Size(192, 24);
            this.CardTotalBoxes.TabIndex = 0;
            // 
            // CardTotalDocs
            // 
            this.CardTotalDocs.BackColor = System.Drawing.Color.White;
            this.CardTotalDocs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CardTotalDocs.Location = new System.Drawing.Point(297, 3);
            this.CardTotalDocs.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.CardTotalDocs.Name = "CardTotalDocs";
            this.CardTotalDocs.Size = new System.Drawing.Size(192, 24);
            this.CardTotalDocs.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(192, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(97, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(89, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(97, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // PanelSearch
            // 
            this.PanelSearch.BackColor = System.Drawing.Color.White;
            this.PanelSearch.Controls.Add(this.TxtSearch);
            this.PanelSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelSearch.Location = new System.Drawing.Point(10, 10);
            this.PanelSearch.Name = "PanelSearch";
            this.PanelSearch.Padding = new System.Windows.Forms.Padding(10);
            this.PanelSearch.Size = new System.Drawing.Size(1120, 55);
            this.PanelSearch.TabIndex = 0;
            // 
            // TxtSearch
            // 
            this.TxtSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtSearch.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.TxtSearch.Location = new System.Drawing.Point(10, 10);
            this.TxtSearch.Name = "TxtSearch";
            this.TxtSearch.Size = new System.Drawing.Size(1100, 37);
            this.TxtSearch.TabIndex = 0;
            this.TxtSearch.Text = "🔍 ابحث باسم الصندوق أو التفاصيل...";
            this.TxtSearch.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ArchivOffiiceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.PanelMain);
            this.Controls.Add(this.PanelSideMenu);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "ArchivOffiiceForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "📁 نظام الأرشيف الإلكتروني";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.PanelSideMenu.ResumeLayout(false);
            this.PanelLogo.ResumeLayout(false);
            this.PanelMain.ResumeLayout(false);
            this.PanelStats.ResumeLayout(false);
            this.FlowStats.ResumeLayout(false);
            this.PanelSearch.ResumeLayout(false);
            this.PanelSearch.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

            // ============================================================
            // تعريف عناصر التحكم (Controls)
            // ============================================================
        private System.Windows.Forms.Panel PanelSideMenu;
        private System.Windows.Forms.Button BtnToggleMenu;
        private System.Windows.Forms.Panel PanelLogo;
        private System.Windows.Forms.Label LblLogo;
        private System.Windows.Forms.Button BtnDashboard;
        private System.Windows.Forms.Button BtnManagBox;
        private System.Windows.Forms.Button BtnFilterAll;
        private System.Windows.Forms.Button BtnFilterActive;
        private System.Windows.Forms.Button BtnFilterInactive;
        private System.Windows.Forms.Button BtnRefresh;
        private System.Windows.Forms.Button BtnManagType;
        private System.Windows.Forms.Button BtnAdvancedSearch;
        private System.Windows.Forms.Button BtnCategories;
        private System.Windows.Forms.Button BtnDepartments;
        private System.Windows.Forms.Button BtnSettingf;
        private System.Windows.Forms.Button BtnExit;
        private System.Windows.Forms.Panel PanelMain;
        private System.Windows.Forms.Panel PanelSearch;
        private System.Windows.Forms.TextBox TxtSearch;
        private System.Windows.Forms.Panel PanelStats;
        private System.Windows.Forms.FlowLayoutPanel FlowStats;
        private System.Windows.Forms.FlowLayoutPanel FlowBoxes;
        private System.Windows.Forms.Panel CardTotalBoxes;
        private System.Windows.Forms.Panel CardActiveBoxes;
        private System.Windows.Forms.Panel CardTotalDocs;
        private System.Windows.Forms.Panel CardActiveDocs;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}