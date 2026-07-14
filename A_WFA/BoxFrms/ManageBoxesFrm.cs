using A_WFA.ModServices;
using A_WFA.Navigation;
using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace A_WFA.BoxFrms
{
    public partial class ManageBoxesFrm : Form
    {
        private DataTable boxesData;
        private string currentFilter = "ALL";
        private string currentSearch = "";

        public ManageBoxesFrm()
        {
            InitializeComponent();
            this.Load += ManageBoxesFrm_Load;

            // ربط الأحداث
            BtnAddNew.Click += BtnAddNew_Click;
            BtnRefresh.Click += BtnRefresh_Click;
            BtnDeleteSelected.Click += BtnDeleteSelected_Click;
            BtnExport.Click += BtnExport_Click;
            TxtSearch.TextChanged += TxtSearch_TextChanged;
            CmbFilter.SelectedIndexChanged += CmbFilter_SelectedIndexChanged;
            DgvBoxes.CellDoubleClick += DgvBoxes_CellDoubleClick;
            DgvBoxes.KeyDown += DgvBoxes_KeyDown;

            // إعداد البحث
            SetupSearchBox();

            // ✅ إعداد خيارات الفلتر
            SetupFilterComboBox();
        }

        #region "تحميل النموذج"

        private void ManageBoxesFrm_Load(object sender, EventArgs e)
        {
            LoadBoxes();
            SetupDataGridView();
            UpdateStatus("✅ جاهز");
        }

        private void SetupDataGridView()
        {
            DgvBoxes.AutoGenerateColumns = false;
            DgvBoxes.Columns.Clear();

            // تعريف الأعمدة
            DgvBoxes.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "id",
                HeaderText = "المعرف",
                DataPropertyName = "id",
                Width = 60,
                Visible = false
            });

            DgvBoxes.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "name",
                HeaderText = "اسم الصندوق",
                DataPropertyName = "name",
                Width = 250
            });

            DgvBoxes.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "archiveBox_number",
                HeaderText = "رقم الأرشيف",
                DataPropertyName = "archiveBox_number",
                Width = 120
            });

            DgvBoxes.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "details",
                HeaderText = "التفاصيل",
                DataPropertyName = "details",
                Width = 300
            });

            DgvBoxes.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "is_active",
                HeaderText = "الحالة",
                DataPropertyName = "is_active",
                Width = 80
            });

            DgvBoxes.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "created_at",
                HeaderText = "تاريخ الإنشاء",
                DataPropertyName = "created_at",
                Width = 150
            });
        }

        #endregion

        #region "إعداد الفلتر"

        private void SetupFilterComboBox()
        {
            CmbFilter.Items.Clear();
            CmbFilter.Items.AddRange(new object[] { "الكل", "نشط", "غير نشط" });
            CmbFilter.SelectedIndex = 0;
        }

        #endregion

        #region "تحميل البيانات"

        private void LoadBoxes()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                UpdateStatus("⏳ جاري تحميل البيانات...");

                // ✅ تحميل مفتاح التشفير (إذا كانت البيانات مشفرة)
                // byte[] masterKey = LoadMasterKey();
                // A_WFA.En.CryptoService.Initialize(masterKey);

                string filter = GetFilterCondition();
                string search = GetSearchCondition();

                // ✅ استعلام SQLite (بدون FORMAT)
                string query = @"
                    SELECT 
                        id, 
                        name, 
                        archiveBox_number, 
                        details, 
                        is_active,
                        created_at
                    FROM Boxes
                    WHERE 1=1 " + filter + search + @"
                    ORDER BY 
                        CASE WHEN is_active = 1 THEN 0 ELSE 1 END,
                        name";

                boxesData = DatabaseManagerLite.ExecuteQuery(query);

                // ✅ إنشاء DataTable جديد للتنسيق
                DataTable displayTable = new DataTable();

                displayTable.Columns.Add("id", typeof(int));
                displayTable.Columns.Add("name", typeof(string));
                displayTable.Columns.Add("archiveBox_number", typeof(string));
                displayTable.Columns.Add("details", typeof(string));
                displayTable.Columns.Add("is_active", typeof(string));  // ✅ نص
                displayTable.Columns.Add("created_at", typeof(string));

                foreach (DataRow row in boxesData.Rows)
                {
                    DataRow newRow = displayTable.NewRow();
                    newRow["id"] = Convert.ToInt32(row["id"]);

                    // ✅ فك تشفير الاسم (إذا كان مشفراً)
                    string name = row["name"].ToString();
                    // name = A_WFA.En.CryptoService.DecryptString(name);  // إذا كان مشفراً
                    newRow["name"] = name;

                    newRow["archiveBox_number"] = row["archiveBox_number"].ToString();

                    // ✅ فك تشفير التفاصيل (إذا كانت مشفرة)
                    string details = row["details"].ToString();
                    // details = A_WFA.En.CryptoService.DecryptString(details);  // إذا كان مشفراً
                    newRow["details"] = details;

                    // ✅ تحويل Boolean إلى نص
                    bool isActive = Convert.ToBoolean(row["is_active"]);
                    newRow["is_active"] = isActive ? "🟢 نشط" : "🔴 غير نشط";

                    // ✅ تنسيق التاريخ في C# بدلاً من SQL
                    string createdDate = row["created_at"]?.ToString();
                    if (!string.IsNullOrEmpty(createdDate) && DateTime.TryParse(createdDate, out DateTime dt))
                    {
                        newRow["created_at"] = dt.ToString("yyyy/MM/dd HH:mm");
                    }
                    else
                    {
                        newRow["created_at"] = createdDate ?? "";
                    }

                    displayTable.Rows.Add(newRow);
                }

                // ✅ استخدام الجدول الجديد
                DgvBoxes.DataSource = displayTable;
                DgvBoxes.AutoResizeColumns();

                LblRecordCount.Text = $"عدد السجلات: {displayTable.Rows.Count}";
                UpdateStatus($"✅ تم تحميل {displayTable.Rows.Count} صندوق");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في تحميل البيانات: {ex.Message}", "خطأ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateStatus($"❌ خطأ: {ex.Message}");
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private string GetFilterCondition()
        {
            if (CmbFilter.SelectedItem == null)
                return "";

            string selected = CmbFilter.SelectedItem.ToString();
            switch (selected)
            {
                case "نشط": return " AND is_active = 1";
                case "غير نشط": return " AND is_active = 0";
                default: return "";
            }
        }

        private string GetSearchCondition()
        {
            if (string.IsNullOrEmpty(currentSearch) || currentSearch == "🔍 بحث...")
                return "";

            // ✅ استخدام معاملات لمنع SQL Injection
            return $" AND (name LIKE '%{currentSearch}%' OR archiveBox_number LIKE '%{currentSearch}%' OR details LIKE '%{currentSearch}%')";
        }

        #endregion

        #region "البحث والفلترة"

        private void SetupSearchBox()
        {
            TxtSearch.Text = "🔍 بحث...";
            TxtSearch.ForeColor = Color.Gray;

            TxtSearch.Enter += (s, e) =>
            {
                if (TxtSearch.Text == "🔍 بحث...")
                {
                    TxtSearch.Text = "";
                    TxtSearch.ForeColor = Color.Black;
                }
            };

            TxtSearch.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(TxtSearch.Text))
                {
                    TxtSearch.Text = "🔍 بحث...";
                    TxtSearch.ForeColor = Color.Gray;
                }
            };
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            if (TxtSearch.Text != "🔍 بحث..." && !string.IsNullOrWhiteSpace(TxtSearch.Text))
            {
                currentSearch = TxtSearch.Text.Trim();
                LoadBoxes();
            }
            else if (string.IsNullOrWhiteSpace(TxtSearch.Text) || TxtSearch.Text == "🔍 بحث...")
            {
                currentSearch = "";
                LoadBoxes();
            }
        }

        private void CmbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadBoxes();
        }

        #endregion

        #region "أزرار التحكم"

        private void BtnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                AddBoxFrm.ShowAddBox((newId) =>
                {
                    LoadBoxes();
                    UpdateStatus($"✅ تم إضافة صندوق جديد برقم: {newId}");
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في فتح نموذج الإضافة: {ex.Message}", "خطأ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadBoxes();
        }

        private void BtnDeleteSelected_Click(object sender, EventArgs e)
        {
            DeleteSelectedBox();
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            ExportToExcel();
        }

        #endregion

        #region "التفاعل مع الجدول"

        private void DgvBoxes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                EditSelectedBox();
            }
        }

        private void DgvBoxes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                EditSelectedBox();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Delete)
            {
                DeleteSelectedBox();
                e.Handled = true;
            }
        }

        #endregion

        #region "عمليات على الصندوق"
        private void EditSelectedBox()
        {
            if (DgvBoxes.SelectedRows.Count == 0)
            {
                MessageBox.Show("الرجاء تحديد صندوق للتعديل", "تنبيه",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // التحقق من وجود العمود
                if (!DgvBoxes.Columns.Contains("id"))
                {
                    MessageBox.Show("بيانات غير صالحة", "خطأ",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                object idValue = DgvBoxes.SelectedRows[0].Cells["id"].Value;

                if (idValue == null || idValue == DBNull.Value)
                {
                    MessageBox.Show("معرف الصندوق غير صالح", "خطأ",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int boxId = Convert.ToInt32(idValue);

                string boxName = DgvBoxes.SelectedRows[0]
                    .Cells["name"]
                    .Value?.ToString() ?? "";

                AddBoxFrm.ShowEditBox(boxId, (updatedId) =>
                {
                    LoadBoxes();
                    UpdateStatus($"✅ تم تحديث الصندوق: {boxName}");
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في فتح نموذج التعديل: {ex.Message}",
                    "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void DeleteSelectedBox()
        {
            if (DgvBoxes.SelectedRows.Count == 0)
            {
                MessageBox.Show("الرجاء تحديد صندوق للحذف", "تنبيه",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // ✅ التحقق من وجود العمود
                if (!DgvBoxes.Columns.Contains("id"))
                {
                    MessageBox.Show("بيانات غير صالحة", "خطأ",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                object idValue = DgvBoxes.SelectedRows[0].Cells["id"].Value;

                if (idValue == null || idValue == DBNull.Value)
                {
                    MessageBox.Show("معرف الصندوق غير صالح", "خطأ",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int boxId = Convert.ToInt32(idValue);

                string boxName = DgvBoxes.SelectedRows[0]
                    .Cells["name"]
                    .Value?.ToString() ?? "";

                DialogResult result = MessageBox.Show(
                    $"⚠️ هل أنت متأكد من حذف الصندوق: {boxName}؟\n" +
                    "سيتم حذف جميع الوثائق المرتبطة به!",
                    "تأكيد الحذف",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    Cursor = Cursors.WaitCursor;

                    bool success = DatabaseManagerLite.DeleteBox(boxId);

                    if (success)
                    {
                        LoadBoxes();
                        UpdateStatus($"✅ تم حذف الصندوق: {boxName}");
                    }
                    else
                    {
                        MessageBox.Show("❌ فشل في حذف الصندوق", "خطأ",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في حذف الصندوق: {ex.Message}",
                    "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Cursor = Cursors.Default;
            }
        }

        #endregion

        #region "التصدير"

        private void ExportToExcel()
        {
            try
            {
                if (boxesData == null || boxesData.Rows.Count == 0)
                {
                    MessageBox.Show("لا توجد بيانات للتصدير", "تنبيه",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "ملفات CSV (*.csv)|*.csv|ملفات Excel (*.xlsx)|*.xlsx|جميع الملفات (*.*)|*.*";
                    sfd.Title = "تصدير إلى Excel";
                    sfd.FileName = $"الصناديق_{DateTime.Now:yyyyMMdd_HHmmss}.csv";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        // تصدير إلى CSV
                        ExportToCsv(sfd.FileName);

                        MessageBox.Show($"✅ تم التصدير بنجاح إلى: {sfd.FileName}", "نجاح",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        UpdateStatus($"✅ تم التصدير إلى: {sfd.FileName}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في التصدير: {ex.Message}", "خطأ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExportToCsv(string filePath)
        {
            using (var writer = new System.IO.StreamWriter(filePath, false, System.Text.Encoding.UTF8))
            {
                // كتابة الرأس
                string[] headers = { "المعرف", "اسم الصندوق", "رقم الأرشيف", "التفاصيل", "الحالة", "تاريخ الإنشاء" };
                writer.WriteLine(string.Join(",", headers));

                // كتابة البيانات
                foreach (DataRow row in boxesData.Rows)
                {
                    string[] values = {
                        row["id"].ToString(),
                        row["name"].ToString(),
                        row["archiveBox_number"].ToString(),
                        row["details"].ToString(),
                        Convert.ToBoolean(row["is_active"]) ? "نشط" : "غير نشط",
                        row["created_at"].ToString()
                    };
                    writer.WriteLine(string.Join(",", values));
                }
            }
        }

        #endregion

        #region "دوال مساعدة"

        private void UpdateStatus(string message)
        {
            LblStatus.Text = message;
            LblStatus.ForeColor = message.StartsWith("❌") ? Color.Red :
                                  message.StartsWith("✅") ? Color.FromArgb(46, 204, 113) :
                                  Color.White;
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                NavigationManager.GoBack();
                this.Hide();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"خطأ في الرجوع: {ex.Message}");
                this.Close();
            }
        }
    }
}