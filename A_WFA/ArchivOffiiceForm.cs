//using A_WFA.BoxFrms;
//using A_WFA.ManageFrm;
//using A_WFA.Navigation;
//using A_WFA.uti;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Diagnostics;
//using System.Drawing;
//using System.IO;
//using System.Linq;
//using System.Windows.Forms;

//namespace A_WFA
//{
//    public partial class ArchivOffiiceForm : Form
//    {
//        private string currentFilter = "ALL";
//        private static Dictionary<string, Image> imageCache = new Dictionary<string, Image>();
//        private bool isLoading = false;
//        private bool isSideMenuExpanded = true;
//        private Timer animationTimer;

//        public ArchivOffiiceForm()
//        {
//            InitializeComponent();

//            this.Load += ArchivOffiiceForm_Load;
//            this.FormClosing += ArchivOffiiceForm_FormClosing;

//            // ربط أحداث الأزرار
//            BtnToggleMenu.Click += BtnToggleMenu_Click;
//            BtnDashboard.Click += BtnDashboard_Click;
//            BtnManagBox.Click += BtnAddBox_Click;
//            BtnFilterAll.Click += BtnFilterAll_Click;
//            BtnFilterActive.Click += BtnFilterActive_Click;
//            BtnFilterInactive.Click += BtnFilterInactive_Click;
//            BtnRefresh.Click += BtnRefresh_Click;
//            BtnManagType.Click += BtnManagType_Click;
//            BtnSettingf.Click += BtnSettingf_Click;
//            BtnExit.Click += BtnExit_Click;
//            BtnAdvancedSearch.Click += BtnAdvancedSearch_Click;
//            button2.Click += button2_Click;
//            BtnCategories.Click += BtnCategories_Click;
//            BtnDepartments.Click += BtnDepartments_Click;
//            TxtSearch.TextChanged += SearchBoxes;

//            animationTimer = new Timer();
//            animationTimer.Interval = 10;
//            animationTimer.Tick += AnimationTimer_Tick;

//            CreateStatCardContent();
//            NavigationManager.Initialize(this);
//        }

//        #region "تحميل النموذج"

//        private void ArchivOffiiceForm_Load(object sender, EventArgs e)
//        {
//            try
//            {
//                // استخدام DatabaseManagerLite بدلاً من DatabaseManagerSR
//                if (!DatabaseManagerLite.SchemaExists())
//                {
//                    DialogResult result = MessageBox.Show(
//                        "⚠️ الجداول غير موجودة في قاعدة البيانات.\n" +
//                        "هل تريد إنشاؤها تلقائياً؟",
//                        "تهيئة قاعدة البيانات",
//                        MessageBoxButtons.YesNo,
//                        MessageBoxIcon.Question);

//                    if (result == DialogResult.Yes)
//                    {
//                        Cursor = Cursors.WaitCursor;
//                        bool success = DatabaseManagerLite.CreateDatabaseSchema();

//                        if (success)
//                        {
//                            MessageBox.Show("✅ تم إنشاء الجداول بنجاح!",
//                                "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                        }
//                        else
//                        {
//                            MessageBox.Show("❌ فشل في إنشاء الجداول",
//                                "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                            return;
//                        }
//                    }
//                    else
//                    {
//                        MessageBox.Show("⚠️ لا يمكن تشغيل التطبيق بدون الجداول.",
//                            "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                        this.Close();
//                        return;
//                    }
//                }

//                InitializeUI();
//                ConfigureFlowLayout();
//                LoadBoxesIntoFlow();
//                LoadStatistics();

//                DatabaseManagerLite.SafeLogAuditTrail(GetCurrentUserId(), "form_open", "تم فتح نموذج الأرشيف");

//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"حدث خطأ أثناء تحميل النموذج: {ex.Message}", "خطأ",
//                    MessageBoxButtons.OK, MessageBoxIcon.Error);
//                DatabaseManagerLite.SafeLogAuditTrail(GetCurrentUserId(), "error", $"خطأ في تحميل نموذج الأرشيف: {ex.Message}");
//            }
//            finally
//            {
//                Cursor = Cursors.Default;
//            }
//            H();


//        }

//        private void ArchivOffiiceForm_FormClosing(object sender, FormClosingEventArgs e)
//        {
//            try
//            {
//                foreach (var img in imageCache.Values)
//                {
//                    img?.Dispose();
//                }
//                imageCache.Clear();
//                animationTimer?.Dispose();

//                DatabaseManagerLite.SafeLogAuditTrail(GetCurrentUserId(), "form_close", "تم إغلاق نموذج الأرشيف");
//            }
//            catch { }
//        }

//#endregion

using A_WFA.BoxFrms;
using A_WFA.ManageFrm;
using A_WFA.Navigation;
using A_WFA.uti;

using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace A_WFA
{
    public partial class ArchivOffiiceForm : Form
    {
        private string currentFilter = "ALL";

        private static Dictionary<string, Image> imageCache =
            new Dictionary<string, Image>();

        private bool isLoading = false;
        private bool isSideMenuExpanded = true;

        private Timer animationTimer;

        private bool navigationInitialized = false;


        public ArchivOffiiceForm()
        {
            InitializeComponent();


            // أحداث النموذج
            this.Load += ArchivOffiiceForm_Load;
            this.FormClosing += ArchivOffiiceForm_FormClosing;


            // أحداث الأزرار
            BtnToggleMenu.Click += BtnToggleMenu_Click;

            BtnDashboard.Click += BtnDashboard_Click;

            BtnManagBox.Click += BtnManagBox_Click;

            BtnFilterAll.Click += BtnFilterAll_Click;

            BtnFilterActive.Click += BtnFilterActive_Click;

            BtnFilterInactive.Click += BtnFilterInactive_Click;

            BtnRefresh.Click += BtnRefresh_Click;

            BtnManagType.Click += BtnManagType_Click;

            BtnSettingf.Click += BtnSettingf_Click;

            BtnExit.Click += BtnExit_Click;

            BtnAdvancedSearch.Click += BtnAdvancedSearch_Click;

            button2.Click += button2_Click;

            BtnCategories.Click += BtnCategories_Click;

            BtnDepartments.Click += BtnDepartments_Click;


            TxtSearch.TextChanged += SearchBoxes;



            // مؤقت الحركة
            animationTimer = new Timer();

            animationTimer.Interval = 10;

            animationTimer.Tick += AnimationTimer_Tick;



            // إنشاء محتوى البطاقات
            CreateStatCardContent();

        }



        #region تحميل النموذج


        private void ArchivOffiiceForm_Load(object sender, EventArgs e)
        {

            try
            {

                // تهيئة مدير التنقل مرة واحدة فقط
                if (!navigationInitialized)
                {
                    NavigationManager.Initialize(this);

                    navigationInitialized = true;
                }



                // تكبير الشاشة
                this.WindowState = FormWindowState.Maximized;



                // فحص قاعدة البيانات
                if (!DatabaseManagerLite.SchemaExists())
                {

                    DialogResult result = MessageBox.Show(
                        "⚠️ الجداول غير موجودة في قاعدة البيانات.\nهل تريد إنشاءها؟",
                        "تهيئة قاعدة البيانات",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);



                    if (result == DialogResult.Yes)
                    {

                        Cursor = Cursors.WaitCursor;


                        bool success =
                            DatabaseManagerLite.CreateDatabaseSchema();



                        if (success)
                        {

                            MessageBox.Show(
                                "✅ تم إنشاء الجداول بنجاح",
                                "نجاح",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                        }
                        else
                        {

                            MessageBox.Show(
                                "❌ فشل إنشاء الجداول",
                                "خطأ",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);

                            return;
                        }

                    }
                    else
                    {

                        MessageBox.Show(
                            "لا يمكن تشغيل التطبيق بدون قاعدة البيانات",
                            "تنبيه",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);


                        this.Close();

                        return;
                    }

                }



                // تهيئة الواجهة
                InitializeUI();


                ConfigureFlowLayout();


                LoadBoxesIntoFlow();


                LoadStatistics();



                DatabaseManagerLite.SafeLogAuditTrail(
                    GetCurrentUserId(),
                    "form_open",
                    "تم فتح نموذج الأرشيف");


            }
            catch (Exception ex)
            {

                MessageBox.Show(
                    $"حدث خطأ أثناء تحميل نموذج الأرشيف:\n{ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);



                DatabaseManagerLite.SafeLogAuditTrail(
                    GetCurrentUserId(),
                    "error",
                    ex.Message);

            }
            finally
            {

                Cursor = Cursors.Default;

            }


            H();

        }



        #endregion



        #region إغلاق النموذج


        private void ArchivOffiiceForm_FormClosing(
            object sender,
            FormClosingEventArgs e)
        {

            try
            {

                foreach (Image img in imageCache.Values)
                {

                    img?.Dispose();

                }


                imageCache.Clear();



                animationTimer?.Dispose();



                DatabaseManagerLite.SafeLogAuditTrail(
                    GetCurrentUserId(),
                    "form_close",
                    "تم إغلاق نموذج الأرشيف");


            }
            catch
            {

            }

        }


        #endregion



#region "تهيئة الواجهة"

private void InitializeUI()
        {
            PanelSideMenu.Visible = true;
            PanelSideMenu.Width = 220;

            BtnToggleMenu.Visible = true;
            BtnToggleMenu.Text = "☰";
            BtnToggleMenu.TextAlign = ContentAlignment.MiddleLeft;
            BtnToggleMenu.Padding = new Padding(15, 0, 0, 0);
            BtnToggleMenu.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            BtnToggleMenu.BringToFront();

            LblLogo.Text = "📁 نظام الأرشيف";
            LblLogo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            LblLogo.TextAlign = ContentAlignment.MiddleCenter;

            BtnDashboard.Text = "🏠 الرئيسية";
            BtnManagBox.Text = "📦 ادارة البكسات";
            BtnFilterAll.Text = "📋 جميع البكسات";
            BtnFilterActive.Text = "🟢 النشطة فقط";
            BtnFilterInactive.Text = "🔴 غير النشطة";
            BtnRefresh.Text = "🔄 تحديث";
            BtnManagType.Text = "📊 ادارة انواع الوثائق";
            BtnAdvancedSearch.Text = "🔍 بحث متقدم";
            BtnCategories.Text = "📂 إدارة التصنيفات";
            BtnDepartments.Text = "🏢 الأقسام";
            BtnSettingf.Text = "🔙 الاعدادات";
            BtnExit.Text = "🚪 خروج";

            var buttons = new List<Button> {
                BtnExit, BtnSettingf, BtnManagBox, BtnFilterAll,
                BtnFilterActive, BtnFilterInactive, BtnRefresh,
                BtnManagType, BtnAdvancedSearch, BtnCategories,
                BtnDepartments, BtnDashboard
            };

            foreach (var btn in buttons)
            {
                btn.TextAlign = ContentAlignment.MiddleLeft;
                btn.Padding = new Padding(15, 0, 0, 0);
                btn.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                btn.Visible = true;
            }

            BtnDashboard.BackColor = Color.FromArgb(41, 128, 185);

            SetupSearchBox();
            SetupFilterButtons();
            UpdateFilterButtons();

            PanelMain.Location = new Point(220, 0);
            PanelMain.Width = this.ClientSize.Width - 220;

            isSideMenuExpanded = true;

            this.PerformLayout();
        }

        private void ConfigureFlowLayout()
        {
            FlowBoxes.FlowDirection = FlowDirection.LeftToRight;
            FlowBoxes.WrapContents = true;
            FlowBoxes.AutoScroll = true;
            FlowBoxes.AutoScrollMargin = new Size(10, 10);
            FlowBoxes.Padding = new Padding(10);
        }

        private void SetupSearchBox()
        {
            TxtSearch.Font = new Font("Segoe UI", 11F);
            TxtSearch.Text = "🔍 ابحث باسم الصندوق أو التفاصيل...";
            TxtSearch.ForeColor = Color.Gray;

            TxtSearch.Enter += (s, e) =>
            {
                if (TxtSearch.Text == "🔍 ابحث باسم الصندوق أو التفاصيل...")
                {
                    TxtSearch.Text = "";
                    TxtSearch.ForeColor = Color.Black;
                }
            };

            TxtSearch.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(TxtSearch.Text))
                {
                    TxtSearch.Text = "🔍 ابحث باسم الصندوق أو التفاصيل...";
                    TxtSearch.ForeColor = Color.Gray;
                }
            };
        }

        private void SetupFilterButtons()
        {
            UpdateFilterButtons();
        }

        private void UpdateFilterButtons()
        {
            BtnFilterAll.BackColor = currentFilter == "ALL" ?
                Color.FromArgb(41, 128, 185) : Color.FromArgb(44, 62, 80);
            BtnFilterActive.BackColor = currentFilter == "ACTIVE" ?
                Color.FromArgb(41, 128, 185) : Color.FromArgb(44, 62, 80);
            BtnFilterInactive.BackColor = currentFilter == "INACTIVE" ?
                Color.FromArgb(41, 128, 185) : Color.FromArgb(44, 62, 80);
        }

        #endregion

        #region "بطاقات الإحصائيات"

        private void CreateStatCardContent()
        {
            CreateStatCard(CardTotalBoxes, "📦 إجمالي الصناديق", Color.FromArgb(52, 152, 219));
            CreateStatCard(CardActiveBoxes, "✅ صناديق نشطة", Color.FromArgb(46, 204, 113));
            CreateStatCard(CardTotalDocs, "📄 إجمالي الوثائق", Color.FromArgb(155, 89, 182));
            CreateStatCard(CardActiveDocs, "📑 وثائق نشطة", Color.FromArgb(241, 196, 15));
        }

        private void CreateStatCard(Panel card, string title, Color color)
        {
            card.Controls.Clear();

            var flowLayout = new FlowLayoutPanel();
            flowLayout.Dock = DockStyle.Fill;
            flowLayout.FlowDirection = FlowDirection.LeftToRight;
            flowLayout.Padding = new Padding(5, 2, 5, 2);

            var lblTitle = new Label();
            lblTitle.Text = title;
            lblTitle.Font = new Font("Segoe UI", 8F);
            lblTitle.ForeColor = Color.Gray;
            lblTitle.TextAlign = ContentAlignment.MiddleLeft;
            lblTitle.AutoSize = false;
            lblTitle.Size = new Size(120, 20);
            flowLayout.Controls.Add(lblTitle);

            var lblValue = new Label();
            lblValue.Text = "0";
            lblValue.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblValue.ForeColor = color;
            lblValue.TextAlign = ContentAlignment.MiddleRight;
            lblValue.AutoSize = false;
            lblValue.Size = new Size(60, 20);
            lblValue.Name = "lblValue";
            flowLayout.Controls.Add(lblValue);

            card.Controls.Add(flowLayout);
            card.Tag = lblValue;
        }

        #endregion

        #region "حركة القائمة الجانبية"

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            // تم إيقاف استخدام المؤقت
        }

        private void UpdateMenuItemsVisibility(bool showText)
        {
            foreach (Control control in PanelSideMenu.Controls)
            {
                if (control == PanelLogo || control == BtnToggleMenu) continue;

                if (control is Button btn)
                {
                    if (showText)
                    {
                        if (btn == BtnDashboard) btn.Text = "🏠 الرئيسية";
                        else if (btn == BtnManagBox) btn.Text = "📦 إضافة صندوق";
                        else if (btn == BtnFilterAll) btn.Text = "📋 جميع الصناديق";
                        else if (btn == BtnFilterActive) btn.Text = "🟢 النشطة فقط";
                        else if (btn == BtnFilterInactive) btn.Text = "🔴 غير النشطة";
                        else if (btn == BtnRefresh) btn.Text = "🔄 تحديث";
                        else if (btn == BtnManagType) btn.Text = "📊 تصدير البيانات";
                        else if (btn == BtnSettingf) btn.Text = "🔙 العودة للقائمة";
                        else if (btn == BtnExit) btn.Text = "🚪 خروج";
                        else if (btn == BtnAdvancedSearch) btn.Text = "🔍 بحث متقدم";
                        else if (btn == BtnCategories) btn.Text = "📂 إدارة التصنيفات";
                        else if (btn == BtnDepartments) btn.Text = "🏢 الأقسام";

                        btn.TextAlign = ContentAlignment.MiddleLeft;
                        btn.Padding = new Padding(15, 0, 0, 0);
                        btn.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                    }
                    else
                    {
                        if (btn == BtnDashboard) btn.Text = "🏠";
                        else if (btn == BtnManagBox) btn.Text = "📦";
                        else if (btn == BtnFilterAll) btn.Text = "📋";
                        else if (btn == BtnFilterActive) btn.Text = "🟢";
                        else if (btn == BtnFilterInactive) btn.Text = "🔴";
                        else if (btn == BtnRefresh) btn.Text = "🔄";
                        else if (btn == BtnManagType) btn.Text = "📊";
                        else if (btn == BtnSettingf) btn.Text = "🔙";
                        else if (btn == BtnExit) btn.Text = "🚪";
                        else if (btn == BtnAdvancedSearch) btn.Text = "🔍";
                        else if (btn == BtnCategories) btn.Text = "📂";
                        else if (btn == BtnDepartments) btn.Text = "🏢";

                        btn.TextAlign = ContentAlignment.MiddleCenter;
                        btn.Padding = new Padding(0);
                        btn.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
                    }
                }
            }

            LblLogo.Text = isSideMenuExpanded ? "📁 نظام الأرشيف" : "📁";
            LblLogo.Font = isSideMenuExpanded ?
                new Font("Segoe UI", 12F, FontStyle.Bold) :
                new Font("Segoe UI", 18F, FontStyle.Bold);
        }

        #endregion

        #region "تحميل الصناديق"

        private void LoadBoxesIntoFlow(string searchText = "")
        {
            if (isLoading) return;
            isLoading = true;

            FlowBoxes.Controls.Clear();
            Cursor = Cursors.WaitCursor;

            try
            {
                string query = "SELECT id, name, image_path, start_date, details, is_active FROM Boxes";
                var conditions = new List<string>();
                var parameters = new Dictionary<string, object>();

                switch (currentFilter)
                {
                    case "ACTIVE":
                        conditions.Add("is_active = 1");
                        break;
                    case "INACTIVE":
                        conditions.Add("is_active = 0");
                        break;
                }

                if (!string.IsNullOrEmpty(searchText) && searchText != "🔍 ابحث باسم الصندوق أو التفاصيل...")
                {
                    conditions.Add("(name LIKE @search OR details LIKE @search)");
                    parameters.Add("@search", $"%{searchText}%");
                }

                if (conditions.Count > 0)
                {
                    query += " WHERE " + string.Join(" AND ", conditions);
                }

                query += " ORDER BY name";

                var dt = DatabaseManagerLite.ExecuteQuery(query, parameters);

                if (dt.Rows.Count == 0)
                {
                    ShowEmptyMessage();
                }
                else
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        CreateBoxCard(row);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"حدث خطأ أثناء تحميل الصناديق: {ex.Message}", "خطأ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                DatabaseManagerLite.SafeLogAuditTrail(GetCurrentUserId(), "error", $"خطأ في تحميل الصناديق: {ex.Message}");
            }
            finally
            {
                Cursor = Cursors.Default;
                isLoading = false;
            }
        }

        private void ShowEmptyMessage()
        {
            var lblEmpty = new Label
            {
                Text = "📭 لا توجد صناديق لعرضها",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.Gray,
                AutoSize = true,
                Location = new Point(300, 200)
            };
            FlowBoxes.Controls.Add(lblEmpty);
        }

        private void CreateBoxCard(DataRow row)
        {
            int boxId = SafeGetInt32(row, "id");
            string boxName = SafeGetString(row, "name");
            string imgPath = SafeGetString(row, "image_path");
            string startDate = SafeGetString(row, "start_date");
            string details = SafeGetString(row, "details");
            bool isActive = SafeGetBoolean(row, "is_active");

            var cardPanel = new Panel
            {
                Width = 163,
                Height = 480,
                Margin = new Padding(2),
                BackColor = isActive ? Color.White : Color.FromArgb(245, 245, 245),
                BorderStyle = BorderStyle.FixedSingle,
                Cursor = Cursors.Hand,
                Tag = boxId,
                Padding = new Padding(5)
            };

            cardPanel.MouseEnter += (s, e) =>
            {
                if (isActive)
                {
                    cardPanel.BackColor = Color.FromArgb(245, 250, 255);
                    cardPanel.BorderStyle = BorderStyle.Fixed3D;
                }
            };

            cardPanel.MouseLeave += (s, e) =>
            {
                cardPanel.BackColor = isActive ? Color.White : Color.FromArgb(245, 245, 245);
                cardPanel.BorderStyle = BorderStyle.FixedSingle;
            };

            var picBox = new PictureBox
            {
                Width = 151,
                Height = 390,
                Location = new Point(5, 5),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.FromArgb(240, 240, 240),
                Cursor = Cursors.Hand,
                BorderStyle = BorderStyle.FixedSingle,
                Tag = boxId
            };

            LoadBoxImage(picBox, imgPath);

            var lblName = new Label
            {
                Text = boxName,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = isActive ? Color.FromArgb(44, 62, 80) : Color.Gray,
                Location = new Point(5, 395),
                Size = new Size(151, 30),
                TextAlign = ContentAlignment.MiddleCenter
            };

            var lblDate = new Label
            {
                Text = $"📅 {(!string.IsNullOrEmpty(startDate) ? startDate : "غير محدد")}",
                Font = new Font("Segoe UI", 8F),
                ForeColor = isActive ? Color.DarkGray : Color.LightGray,
                Location = new Point(5, 260),
                Size = new Size(151, 20),
                TextAlign = ContentAlignment.MiddleCenter
            };

            var lblStatus = new Label
            {
                Text = isActive ? "🟢 نشط" : "🔴 غير نشط",
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                ForeColor = isActive ? Color.Green : Color.Red,
                Location = new Point(5, 280),
                Size = new Size(151, 20),
                TextAlign = ContentAlignment.MiddleCenter
            };

            var btnPanel = new Panel
            {
                Location = new Point(5, 425),
                Size = new Size(151, 20),
                BackColor = Color.Transparent
            };

            var btnViewDocs = new Button
            {
                Text = "📄 عرض الوثائق",
                Location = new Point(0, 0),
                Size = new Size(78, 20),
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Tag = boxId
            };
            btnViewDocs.FlatAppearance.BorderSize = 0;
            btnViewDocs.Click += (s, e) => OpenBoxContentForm(boxId, boxName);

            var btnAddDoc = new Button
            {
                Text = "📎 إضافة وثيقة",
                Location = new Point(80, 0),
                Size = new Size(78, 20),
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Tag = boxId
            };
            btnAddDoc.FlatAppearance.BorderSize = 0;
            btnAddDoc.Click += (s, e) => OpenAddDocumentForm(boxId, boxName);

            btnPanel.Controls.Add(btnViewDocs);
            btnPanel.Controls.Add(btnAddDoc);

            var lblDetails = new Label
            {
                Text = string.IsNullOrEmpty(details) ? "لا توجد تفاصيل" :
                       (details.Length > 40 ? details.Substring(0, 40) + "..." : details),
                Font = new Font("Segoe UI", 7F),
                ForeColor = isActive ? Color.DimGray : Color.LightGray,
                Location = new Point(5, 450),
                Size = new Size(151, 30),
                TextAlign = ContentAlignment.TopCenter
            };

            cardPanel.Controls.AddRange(new Control[] { picBox, lblName, lblDate, lblStatus, btnPanel, lblDetails });

            cardPanel.Click += (s, e) => OpenBoxContentForm(boxId, boxName);
            picBox.Click += (s, e) => OpenBoxContentForm(boxId, boxName);
            lblName.Click += (s, e) => OpenBoxContentForm(boxId, boxName);

            FlowBoxes.Controls.Add(cardPanel);
        }

        private void LoadBoxImage(PictureBox picBox, string imgPath)
        {
            try
            {
                if (string.IsNullOrEmpty(imgPath) || !File.Exists(imgPath))
                {
                    picBox.Image = CreateDefaultBoxImage(picBox.Size);
                    return;
                }

                if (imageCache.ContainsKey(imgPath))
                {
                    picBox.Image = imageCache[imgPath];
                    return;
                }

                using (var originalImage = Image.FromFile(imgPath))
                {
                    var resizedImage = new Bitmap(picBox.Width, picBox.Height);
                    using (var g = Graphics.FromImage(resizedImage))
                    {
                        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        g.DrawImage(originalImage, 0, 0, picBox.Width, picBox.Height);
                    }
                    imageCache[imgPath] = resizedImage;
                    picBox.Image = resizedImage;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"⚠️ خطأ في تحميل الصورة: {ex.Message}");
                picBox.Image = CreateDefaultBoxImage(picBox.Size);
            }
        }

        private Image CreateDefaultBoxImage(Size size)
        {
            var img = new Bitmap(size.Width, size.Height);
            using (var g = Graphics.FromImage(img))
            {
                g.Clear(Color.FromArgb(240, 240, 240));
                using (var font = new Font("Segoe UI", 48F, FontStyle.Bold))
                using (var brush = new SolidBrush(Color.LightGray))
                {
                    float x = (size.Width - 50) / 2;
                    float y = (size.Height - 50) / 2;
                    g.DrawString("📁", font, brush, new PointF(x, y));
                }
            }
            return img;
        }

        #endregion

        #region "فتح محتويات الصندوق وإضافة وثيقة"

        private void OpenBoxContentForm(int boxId, string boxName)
        {
            try
            {
                using (var frm = new FrmBoxContent(boxId, boxName))
                {
                    frm.ShowDialog();
                }

                DatabaseManagerLite.SafeLogAuditTrail(GetCurrentUserId(), "view_box_content",
                    $"فتح محتويات الصندوق: {boxName} (ID: {boxId})");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"حدث خطأ أثناء فتح محتويات الصندوق: {ex.Message}", "خطأ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenAddDocumentForm(int boxId, string boxName)
        {
            try
            {
                using (var frm = new FrmAddDocument(boxId))
                {
                    frm.SetBoxName(boxName);

                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        LoadStatistics();
                        LoadBoxesIntoFlow();

                        DatabaseManagerLite.SafeLogAuditTrail(GetCurrentUserId(), "document_added",
                            $"تم إضافة وثيقة إلى الصندوق: {boxName} (ID: {boxId})");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"حدث خطأ أثناء فتح نموذج إضافة الوثيقة: {ex.Message}", "خطأ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region "الإحصائيات"

        private void LoadStatistics()
        {
            try
            {
                var stats = DatabaseManagerLite.GetStatistics();
                UpdateStatistics(stats.TotalBoxes, stats.ActiveBoxes, stats.TotalDocuments, stats.ActiveDocuments);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("خطأ في تحميل الإحصائيات: " + ex.Message);
            }
        }

        private void UpdateStatistics(int totalBoxes, int activeBoxes, int totalDocs, int activeDocs)
        {
            UpdateStatCard(CardTotalBoxes, totalBoxes.ToString());
            UpdateStatCard(CardActiveBoxes, activeBoxes.ToString());
            UpdateStatCard(CardTotalDocs, totalDocs.ToString());
            UpdateStatCard(CardActiveDocs, activeDocs.ToString());
        }

        private void UpdateStatCard(Panel card, string value)
        {
            if (card.Tag is Label lblValue)
            {
                lblValue.Text = value;
            }
        }

        #endregion

        #region "أزرار التحكم والأحداث"
        


        private void BtnToggleMenu_Click(object sender, EventArgs e)  { H();}
        private void BtnDashboard_Click(object sender, EventArgs e)
        {
           
        }
        private void BtnManagBox_Click(object sender, EventArgs e)
        {
            try
            {
                NavigationManager.Navigate<ManageBoxesFrm>();

                DatabaseManagerLite.SafeLogAuditTrail(
                    GetCurrentUserId(),
                    "ManageBoxesFrm",
                    "فتح نافذة إدارة البكسات");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"خطأ في فتح نموذج إدارة البكسات: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        private void BtnFilterAll_Click(object sender, EventArgs e)
        {
            currentFilter = "ALL";

            UpdateFilterButtons();
            LoadBoxesIntoFlow();

            DatabaseManagerLite.SafeLogAuditTrail(
                GetCurrentUserId(),
                "filter",
                "عرض جميع البكسات");
        }
        private void BtnFilterActive_Click(object sender, EventArgs e)
        {
            currentFilter = "ACTIVE";

            UpdateFilterButtons();
            LoadBoxesIntoFlow();

            DatabaseManagerLite.SafeLogAuditTrail(
                GetCurrentUserId(),
                "filter",
                "عرض البكسات النشطة فقط");
        }
        private void BtnFilterInactive_Click(object sender, EventArgs e)
        {
            currentFilter = "INACTIVE";

            UpdateFilterButtons();
            LoadBoxesIntoFlow();

            DatabaseManagerLite.SafeLogAuditTrail(
                GetCurrentUserId(),
                "filter",
                "عرض البكسات غير النشطة فقط");
        }
        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                TxtSearch.Text = "🔍 ابحث باسم البكس أو التفاصيل...";

                currentFilter = "ALL";

                UpdateFilterButtons();

                LoadStatistics();
                LoadBoxesIntoFlow();


                DatabaseManagerLite.SafeLogAuditTrail(
                    GetCurrentUserId(),
                    "refresh",
                    "تحديث قائمة البكسات والإحصائيات");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"حدث خطأ أثناء تحديث البيانات: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        private void BtnManagType_Click(object sender, EventArgs e)
        {
            try
            {
                NavigationManager.Navigate<ManageDocumentTypesFrm>();

                DatabaseManagerLite.SafeLogAuditTrail(
                    GetCurrentUserId(),
                    "ManageDocumentTypesFrm",
                    "فتح نافذة ادارة انواع الوثائق");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"خطأ في فتح نموذج ادارة انواع الوثائق: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        private void BtnSettingf_Click(object sender, EventArgs e)
        {
            try
            {
                NavigationManager.Navigate<FrmSettings>();

                DatabaseManagerLite.SafeLogAuditTrail(
                    GetCurrentUserId(),
                    "FrmBackupManager",
                    "فتح نافذة الاعدادات");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"خطأ في فتح نموذج الاعدادات: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                NavigationManager.Navigate<FrmBackupManager>();

                DatabaseManagerLite.SafeLogAuditTrail(
                    GetCurrentUserId(),
                    "FrmBackupManager",
                    "فتح نافذة إدارة النسخ الاحتياطية");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"خطأ في فتح نموذج إدارة النسخ الاحتياطية: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void BtnAdvancedSearch_Click(object sender, EventArgs e)
        {
            // البحث المتقدم
        }
        private void BtnCategories_Click(object sender, EventArgs e)
        {
            try
            {
                NavigationManager.Navigate<ManageCategoriesFrm>();

                DatabaseManagerLite.SafeLogAuditTrail(
                    GetCurrentUserId(),
                    "ManageCategoriesFrm",
                    "فتح نافذة إدارة التصنيفات");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"خطأ في فتح نموذج إدارة التصنيفات: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        private void BtnDepartments_Click(object sender, EventArgs e)
        {
            try
            {
                NavigationManager.Navigate<ManageDepartmentsFrm>();

                DatabaseManagerLite.SafeLogAuditTrail(
                    GetCurrentUserId(),
                    "ManageCategoriesFrm",
                    "فتح نافذة إدارة الاقسام");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"خطأ في فتح نموذج إدارة الاقسام: {ex.Message}",
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        private void SearchBoxes(object sender, EventArgs e)
        {
            string filterText = TxtSearch.Text.Trim();


            if (filterText == "🔍 ابحث باسم الصندوق أو التفاصيل...")
            {
                LoadBoxesIntoFlow();
            }
            else
            {
                LoadBoxesIntoFlow(filterText);
            }
        }

        
        #endregion  
        private void H() {
            isSideMenuExpanded = !isSideMenuExpanded;

            if (isSideMenuExpanded)
            {
                PanelSideMenu.Width = 220;
                UpdateMenuItemsVisibility(true);

                BtnToggleMenu.TextAlign = ContentAlignment.MiddleLeft;
                BtnToggleMenu.Padding = new Padding(15, 0, 0, 0);
            }
            else
            {
                PanelSideMenu.Width = 60;
                UpdateMenuItemsVisibility(false);

                BtnToggleMenu.TextAlign = ContentAlignment.MiddleCenter;
                BtnToggleMenu.Padding = new Padding(0);
            }
            PanelSideMenu.Visible = true;

            PanelMain.Location = new Point(PanelSideMenu.Width, 0);
            PanelMain.Width = this.ClientSize.Width - PanelSideMenu.Width;

            this.PerformLayout();
        }

        //#endregion

        #region "مساعدات الأمان واسترجاع القيم"

        private int SafeGetInt32(DataRow row, string columnName)
        {
            if (row.Table.Columns.Contains(columnName) && row[columnName] != DBNull.Value)
            {
                int.TryParse(row[columnName].ToString(), out int result);
                return result;
            }
            return 0;
        }

        private string SafeGetString(DataRow row, string columnName)
        {
            if (row.Table.Columns.Contains(columnName) && row[columnName] != DBNull.Value)
            {
                return row[columnName].ToString();
            }
            return string.Empty;
        }

        private bool SafeGetBoolean(DataRow row, string columnName)
        {
            if (row.Table.Columns.Contains(columnName) && row[columnName] != DBNull.Value)
            {
                string val = row[columnName].ToString().ToLower();
                return val == "1" || val == "true";
            }
            return false;
        }

        private int GetCurrentUserId()
        {
            // يفضل استرجاع رقم المستخدم الفعلي من جلسة تسجيل الدخول لديك
            return 1;
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            DatabaseSeeder.SeedAll();
        }
    }
}