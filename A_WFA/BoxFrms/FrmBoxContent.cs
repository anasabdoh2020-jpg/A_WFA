using A_WFA.Database.LTE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace A_WFA
{
    public partial class FrmBoxContent : Form  // ✅ يجب أن يرث من Form
    {
        private int boxId;
        private string boxName;
        private DataTable documentsData;
        private string currentFilter = "ALL";
        private string currentSearch = "";

        public FrmBoxContent(int boxId, string boxName)
        {
            InitializeComponent();
            this.boxId = boxId;
            this.boxName = boxName;

            this.Load += FrmBoxContent_Load;

            // ربط الأحداث
            BtnAddDocument.Click += BtnAddDocument_Click;
            BtnRefresh.Click += BtnRefresh_Click;
            BtnDeleteSelected.Click += BtnDeleteSelected_Click;
            BtnExport.Click += BtnExport_Click;
            TxtSearch.TextChanged += TxtSearch_TextChanged;
            CmbFilter.SelectedIndexChanged += CmbFilter_SelectedIndexChanged;
            DgvDocuments.CellDoubleClick += DgvDocuments_CellDoubleClick;
            DgvDocuments.KeyDown += DgvDocuments_KeyDown;

            SetupSearchBox();
        }

        #region "تحميل النموذج"

        private void FrmBoxContent_Load(object sender, EventArgs e)
        {
            LblBoxTitle.Text = $"📦 محتويات الصندوق: {boxName}";
            LoadBoxInfo();
            LoadDocuments();
            SetupDataGridView();
            UpdateStatus("✅ جاهز");
        }

        private void LoadBoxInfo()
        {
            try
            {
                DataRow box = DatabaseManagerLite.GetBoxById(boxId);
                if (box != null)
                {
                    // رقم الأرشيف
                    string archiveNumber = box["archiveBox_number"] != DBNull.Value
                        ? box["archiveBox_number"].ToString()
                        : "غير محدد";
                    LblArchiveValue.Text = archiveNumber;

                    // الحالة
                    bool isActive = Convert.ToBoolean(box["is_active"]);
                    LblStatusValue.Text = isActive ? "🟢 نشط" : "🔴 غير نشط";
                    LblStatusValue.ForeColor = isActive ? Color.Green : Color.Red;
                }
            }
            catch (Exception ex)
            {
                UpdateStatus($"❌ خطأ في تحميل معلومات الصندوق: {ex.Message}");
            }
        }

        private void SetupDataGridView()
        {
            DgvDocuments.AutoGenerateColumns = false;
            DgvDocuments.Columns.Clear();

            DgvDocuments.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "id",
                HeaderText = "المعرف",
                DataPropertyName = "id",
                Width = 60,
                Visible = false
            });

            DgvDocuments.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "title",
                HeaderText = "عنوان الوثيقة",
                DataPropertyName = "title",
                Width = 250
            });

            DgvDocuments.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "archiveDoc_number",
                HeaderText = "الرقم التسلسلي",
                DataPropertyName = "archiveDoc_number",
                Width = 120
            });

            DgvDocuments.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "document_date",
                HeaderText = "تاريخ الوثيقة",
                DataPropertyName = "document_date",
                Width = 120
            });

            DgvDocuments.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "status",
                HeaderText = "الحالة",
                DataPropertyName = "status",
                Width = 120
            });

            DgvDocuments.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "priority",
                HeaderText = "الأولوية",
                DataPropertyName = "priority",
                Width = 100
            });

            DgvDocuments.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "file_name",
                HeaderText = "الملف",
                DataPropertyName = "file_name",
                Width = 150
            });
        }

        #endregion

        #region "تحميل الوثائق"

        private void LoadDocuments()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                UpdateStatus("⏳ جاري تحميل الوثائق...");

                string filter = GetFilterCondition();
                string search = GetSearchCondition();

                string query = @"
                    SELECT 
                        id,
                        title,
                        archiveDoc_number,
                        FORMAT(document_date, 'yyyy/MM/dd') AS document_date,
                        status,
                        priority,
                        file_name
                    FROM Documents
                    WHERE box_id = @boxId " + filter + search + @"
                    ORDER BY 
                        CAST(archiveDoc_number AS INT)";

                var parameters = new Dictionary<string, object> { { "@boxId", boxId } };
                documentsData = DatabaseModuleLite.ExecuteQuery(query, parameters);

                DgvDocuments.DataSource = documentsData;
                DgvDocuments.AutoResizeColumns();

                LblDocCountValue.Text = documentsData.Rows.Count.ToString();
                LblRecordCount.Text = $"عدد السجلات: {documentsData.Rows.Count}";
                UpdateStatus($"✅ تم تحميل {documentsData.Rows.Count} وثيقة");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في تحميل الوثائق: {ex.Message}", "خطأ",
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
            switch (CmbFilter.SelectedItem?.ToString())
            {
                case "نشط": return " AND status = 'نشط'";
                case "غير نشط": return " AND status != 'نشط'";
                default: return "";
            }
        }

        private string GetSearchCondition()
        {
            if (string.IsNullOrEmpty(currentSearch) || currentSearch == "🔍 بحث...")
                return "";

            return $" AND (title LIKE '%{currentSearch}%' OR archiveDoc_number LIKE '%{currentSearch}%')";
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
            if (TxtSearch.Text != "🔍 بحث...")
            {
                currentSearch = TxtSearch.Text.Trim();
                LoadDocuments();
            }
        }

        private void CmbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDocuments();
        }

        #endregion

        #region "أزرار التحكم"

        private void BtnAddDocument_Click(object sender, EventArgs e)
        {
            try
            {
                using (var frm = new FrmAddDocument(boxId))
                {
                    frm.SetBoxName(boxName);
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        LoadDocuments();
                        LoadBoxInfo();
                        UpdateStatus($"✅ تم إضافة وثيقة جديدة");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في فتح نموذج الإضافة: {ex.Message}", "خطأ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadDocuments();
            LoadBoxInfo();
        }

        private void BtnDeleteSelected_Click(object sender, EventArgs e)
        {
            DeleteSelectedDocument();
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            ExportToExcel();
        }

        #endregion

        #region "التفاعل مع الجدول"

        private void DgvDocuments_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                EditDocument();
            }
        }

        private void DgvDocuments_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                EditDocument();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Delete)
            {
                DeleteSelectedDocument();
                e.Handled = true;
            }
        }

        #endregion

        #region "عمليات على الوثائق"

        private void EditDocument()
        {
            if (DgvDocuments.SelectedRows.Count == 0)
            {
                MessageBox.Show("الرجاء تحديد وثيقة للتعديل", "تنبيه",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int docId = Convert.ToInt32(DgvDocuments.SelectedRows[0].Cells["id"].Value);
                string title = DgvDocuments.SelectedRows[0].Cells["title"].Value?.ToString() ?? "";

                using (var frm = new FrmAddDocument(boxId, docId))
                {
                    frm.SetBoxName(boxName);
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        LoadDocuments();
                        UpdateStatus($"✅ تم تحديث الوثيقة: {title}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في فتح نموذج التعديل: {ex.Message}", "خطأ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteSelectedDocument()
        {
            if (DgvDocuments.SelectedRows.Count == 0)
            {
                MessageBox.Show("الرجاء تحديد وثيقة للحذف", "تنبيه",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int docId = Convert.ToInt32(DgvDocuments.SelectedRows[0].Cells["id"].Value);
            string title = DgvDocuments.SelectedRows[0].Cells["title"].Value?.ToString() ?? "";

            DialogResult result = MessageBox.Show(
                $"⚠️ هل أنت متأكد من حذف الوثيقة: {title}؟",
                "تأكيد الحذف",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;
                    bool success = DatabaseModuleLite.DeleteDocument(docId);

                    if (success)
                    {
                        LoadDocuments();
                        LoadBoxInfo();
                        UpdateStatus($"✅ تم حذف الوثيقة: {title}");
                    }
                    else
                    {
                        MessageBox.Show("❌ فشل في حذف الوثيقة", "خطأ",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"خطأ في حذف الوثيقة: {ex.Message}", "خطأ",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Cursor = Cursors.Default;
                }
            }
        }

        #endregion

        #region "التصدير"

        private void ExportToExcel()
        {
            try
            {
                if (documentsData == null || documentsData.Rows.Count == 0)
                {
                    MessageBox.Show("لا توجد بيانات للتصدير", "تنبيه",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "ملفات Excel (*.xlsx)|*.xlsx|جميع الملفات (*.*)|*.*";
                    sfd.Title = "تصدير إلى Excel";
                    sfd.FileName = $"وثائق_{boxName}_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
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

        #endregion

        #region "دوال مساعدة"

        private void UpdateStatus(string message)
        {
            LblStatusBar.Text = message;
            LblStatusBar.ForeColor = message.StartsWith("❌") ? Color.Red :
                                    message.StartsWith("✅") ? Color.LightGreen :
                                    Color.White;
        }

        #endregion
    }
}