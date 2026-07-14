using A_WFA.Database.LTE;
using A_WFA.Navigation;
using Spire.DataExport.RTF;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace A_WFA.ManageFrm
{
    public partial class ManageDocumentTypesFrm : Form
    {
        private DataTable typesData;
        private string currentSearch = "";

        public ManageDocumentTypesFrm()
        {
            InitializeComponent();
            this.Load += ManageDocumentTypesFrm_Load;

            BtnAddNew.Click += BtnAddNew_Click;
            BtnRefresh.Click += BtnRefresh_Click;
            BtnDeleteSelected.Click += BtnDeleteSelected_Click;
            BtnExport.Click += BtnExport_Click;
            TxtSearch.TextChanged += TxtSearch_TextChanged;
            DgvTypes.CellDoubleClick += DgvTypes_CellDoubleClick;
            DgvTypes.KeyDown += DgvTypes_KeyDown;

            SetupSearchBox();
        }

        private void ManageDocumentTypesFrm_Load(object sender, EventArgs e)
        {
            LoadTypes();
            SetupDataGridView();
            UpdateStatus("✅ جاهز");
        }

        private void SetupDataGridView()
        {
            DgvTypes.AutoGenerateColumns = false;
            DgvTypes.Columns.Clear();

            DgvTypes.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "id",
                HeaderText = "المعرف",
                DataPropertyName = "id",
                Width = 60,
                Visible = false
            });

            DgvTypes.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "name",
                HeaderText = "اسم النوع",
                DataPropertyName = "name",
                Width = 250
            });

            DgvTypes.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "description",
                HeaderText = "الوصف",
                DataPropertyName = "description",
                Width = 350
            });

            DgvTypes.Columns.Add(new DataGridViewCheckBoxColumn()
            {
                Name = "is_active",
                HeaderText = "نشط",
                DataPropertyName = "is_active",
                Width = 80
            });

            DgvTypes.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "created_at",
                HeaderText = "تاريخ الإنشاء",
                DataPropertyName = "created_at",
                Width = 150
            });
        }

        private void LoadTypes()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                UpdateStatus("⏳ جاري تحميل الأنواع...");

                string query = @"
                    SELECT 
                        id,
                        name,
                        description,
                        is_active,
                        created_at
                    FROM Document_Types
                    WHERE 1=1";

                if (!string.IsNullOrEmpty(currentSearch))
                {
                    query += $" AND (name LIKE '%{currentSearch}%' OR description LIKE '%{currentSearch}%')";
                }

                query += " ORDER BY name";

                typesData = DatabaseModuleLite.ExecuteQuery(query);

                // تنسيق التاريخ
                if (typesData.Columns.Contains("created_at"))
                {
                    foreach (DataRow row in typesData.Rows)
                    {
                        if (row["created_at"] != DBNull.Value)
                        {
                            try
                            {
                                DateTime dt = Convert.ToDateTime(row["created_at"]);
                                row["created_at"] = dt.ToString("yyyy/MM/dd HH:mm");
                            }
                            catch { }
                        }
                    }
                }

                DgvTypes.DataSource = typesData;
                DgvTypes.AutoResizeColumns();

                LblRecordCount.Text = $"عدد السجلات: {typesData.Rows.Count}";
                UpdateStatus($"✅ تم تحميل {typesData.Rows.Count} نوع");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في تحميل الأنواع: {ex.Message}", "خطأ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateStatus($"❌ خطأ: {ex.Message}");
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

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
                LoadTypes();
            }
            else
            {
                currentSearch = "";
                LoadTypes();
            }
        }

        private void BtnAddNew_Click(object sender, EventArgs e)
        {
            ShowTypeForm();
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadTypes();
        }

        private void BtnDeleteSelected_Click(object sender, EventArgs e)
        {
            DeleteSelectedType();
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            ExportToExcel();
        }

        private void DgvTypes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                EditType();
            }
        }

        private void DgvTypes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                EditType();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Delete)
            {
                DeleteSelectedType();
                e.Handled = true;
            }
        }

        private void ShowTypeForm(int? typeId = null)
        {
            try
            {
                using (var frm = new AddDocumentTypeFrm(typeId))
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        LoadTypes();
                        UpdateStatus($"✅ تم {(typeId.HasValue ? "تحديث" : "إضافة")} النوع بنجاح");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في فتح النموذج: {ex.Message}", "خطأ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EditType()
        {
            if (DgvTypes.SelectedRows.Count == 0)
            {
                MessageBox.Show("الرجاء تحديد نوع للتعديل", "تنبيه",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int typeId = Convert.ToInt32(DgvTypes.SelectedRows[0].Cells["id"].Value);
            ShowTypeForm(typeId);
        }

        private void DeleteSelectedType()
        {
            if (DgvTypes.SelectedRows.Count == 0)
            {
                MessageBox.Show("الرجاء تحديد نوع للحذف", "تنبيه",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int typeId = Convert.ToInt32(DgvTypes.SelectedRows[0].Cells["id"].Value);
            string typeName = DgvTypes.SelectedRows[0].Cells["name"].Value?.ToString() ?? "";

            // التحقق من وجود وثائق مرتبطة
            if (HasRelatedDocuments(typeId))
            {
                DialogResult confirm = MessageBox.Show(
                    $"⚠️ هناك وثائق مرتبطة بهذا النوع!\n\n" +
                    $"سيتم حذف النوع: {typeName}\n" +
                    $"سيتم تعيين قيمة NULL للوثائق المرتبطة.\n\n" +
                    $"هل تريد المتابعة؟",
                    "تأكيد الحذف",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirm != DialogResult.Yes)
                    return;
            }
            else
            {
                DialogResult result = MessageBox.Show(
                    $"⚠️ هل أنت متأكد من حذف النوع: {typeName}؟",
                    "تأكيد الحذف",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result != DialogResult.Yes)
                    return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;

                // تحديث الوثائق المرتبطة
                string updateDocs = "UPDATE Documents SET document_type_id = NULL WHERE document_type_id = @id";
                var updateParams = new Dictionary<string, object> { { "@id", typeId } };
                DatabaseModuleLite.ExecuteNonQuery(updateDocs, updateParams);

                // حذف النوع
                string query = "DELETE FROM Document_Types WHERE id = @id";
                var parameters = new Dictionary<string, object> { { "@id", typeId } };
                int rowsAffected = DatabaseModuleLite.ExecuteNonQuery(query, parameters);

                if (rowsAffected > 0)
                {
                    LoadTypes();
                    UpdateStatus($"✅ تم حذف النوع: {typeName}");
                }
                else
                {
                    MessageBox.Show("❌ فشل في حذف النوع", "خطأ",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في حذف النوع: {ex.Message}", "خطأ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private bool HasRelatedDocuments(int typeId)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM Documents WHERE document_type_id = @id";
                var parameters = new Dictionary<string, object> { { "@id", typeId } };
                object result = DatabaseModuleLite.ExecuteScalar(query, parameters);
                return result != null && Convert.ToInt32(result) > 0;
            }
            catch
            {
                return false;
            }
        }

        private void ExportToExcel()
        {
            try
            {
                if (typesData == null || typesData.Rows.Count == 0)
                {
                    MessageBox.Show("لا توجد بيانات للتصدير", "تنبيه",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "ملفات CSV (*.csv)|*.csv|جميع الملفات (*.*)|*.*";
                    sfd.Title = "تصدير إلى CSV";
                    sfd.FileName = $"أنواع_الوثائق_{DateTime.Now:yyyyMMdd_HHmmss}.csv";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
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
                string[] headers = { "المعرف", "اسم النوع", "الوصف", "نشط", "تاريخ الإنشاء" };
                writer.WriteLine(string.Join(",", headers));

                foreach (DataRow row in typesData.Rows)
                {
                    string[] values = {
                        row["id"].ToString(),
                        row["name"].ToString(),
                        row["description"].ToString(),
                        Convert.ToBoolean(row["is_active"]) ? "نعم" : "لا",
                        row["created_at"].ToString()
                    };
                    writer.WriteLine(string.Join(",", values));
                }
            }
        }

        private void UpdateStatus(string message)
        {
            LblStatus.Text = message;
            LblStatus.ForeColor = message.StartsWith("❌") ? Color.Red :
                                    message.StartsWith("✅") ? Color.FromArgb(46, 204, 113) :
                                    Color.White;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NavigationManager.GoBack();
            this.Hide();
        }
    }
}