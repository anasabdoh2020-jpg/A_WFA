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
    public partial class ManageDepartmentsFrm : Form
    {
        private DataTable departmentsData;
        private string currentSearch = "";

        public ManageDepartmentsFrm()
        {
            InitializeComponent();
            this.Load += ManageDepartmentsFrm_Load;

            BtnAddNew.Click += BtnAddNew_Click;
            BtnRefresh.Click += BtnRefresh_Click;
            BtnDeleteSelected.Click += BtnDeleteSelected_Click;
            BtnExport.Click += BtnExport_Click;
            TxtSearch.TextChanged += TxtSearch_TextChanged;
            DgvDepartments.CellDoubleClick += DgvDepartments_CellDoubleClick;
            DgvDepartments.KeyDown += DgvDepartments_KeyDown;

            SetupSearchBox();
        }

        private void ManageDepartmentsFrm_Load(object sender, EventArgs e)
        {
            LoadDepartments();
            SetupDataGridView();
            UpdateStatus("✅ جاهز");
        }

        private void SetupDataGridView()
        {
            DgvDepartments.AutoGenerateColumns = false;
            DgvDepartments.Columns.Clear();

            DgvDepartments.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "id",
                HeaderText = "المعرف",
                DataPropertyName = "id",
                Width = 60,
                Visible = false
            });

            DgvDepartments.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "name",
                HeaderText = "اسم القسم",
                DataPropertyName = "name",
                Width = 250
            });

            DgvDepartments.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "description",
                HeaderText = "الوصف",
                DataPropertyName = "description",
                Width = 350
            });

            DgvDepartments.Columns.Add(new DataGridViewCheckBoxColumn()
            {
                Name = "is_active",
                HeaderText = "نشط",
                DataPropertyName = "is_active",
                Width = 80
            });

            DgvDepartments.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "created_at",
                HeaderText = "تاريخ الإنشاء",
                DataPropertyName = "created_at",
                Width = 150
            });
        }

        private void LoadDepartments()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                UpdateStatus("⏳ جاري تحميل الأقسام...");

                string query = @"
                    SELECT 
                        id,
                        name,
                        description,
                        is_active,
                        created_at
                    FROM Departments
                    WHERE 1=1";

                if (!string.IsNullOrEmpty(currentSearch))
                {
                    query += $" AND (name LIKE '%{currentSearch}%' OR description LIKE '%{currentSearch}%')";
                }

                query += " ORDER BY name";

                departmentsData = DatabaseModuleLite.ExecuteQuery(query);

                // تنسيق التاريخ
                if (departmentsData.Columns.Contains("created_at"))
                {
                    foreach (DataRow row in departmentsData.Rows)
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

                DgvDepartments.DataSource = departmentsData;
                DgvDepartments.AutoResizeColumns();

                LblRecordCount.Text = $"عدد السجلات: {departmentsData.Rows.Count}";
                UpdateStatus($"✅ تم تحميل {departmentsData.Rows.Count} قسم");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في تحميل الأقسام: {ex.Message}", "خطأ",
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
                LoadDepartments();
            }
        }

        private void BtnAddNew_Click(object sender, EventArgs e)
        {
            ShowDepartmentForm();
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadDepartments();
        }

        private void BtnDeleteSelected_Click(object sender, EventArgs e)
        {
            DeleteSelectedDepartment();
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            ExportToExcel();
        }

        private void DgvDepartments_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                EditDepartment();
            }
        }

        private void DgvDepartments_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                EditDepartment();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Delete)
            {
                DeleteSelectedDepartment();
                e.Handled = true;
            }
        }

        private void ShowDepartmentForm(int? departmentId = null)
        {
            try
            {
                using (var frm = new AddDepartmentFrm(departmentId))
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        LoadDepartments();
                        UpdateStatus($"✅ تم {(departmentId.HasValue ? "تحديث" : "إضافة")} القسم بنجاح");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في فتح النموذج: {ex.Message}", "خطأ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EditDepartment()
        {
            if (DgvDepartments.SelectedRows.Count == 0)
            {
                MessageBox.Show("الرجاء تحديد قسم للتعديل", "تنبيه",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int departmentId = Convert.ToInt32(DgvDepartments.SelectedRows[0].Cells["id"].Value);
            ShowDepartmentForm(departmentId);
        }

        private void DeleteSelectedDepartment()
        {
            if (DgvDepartments.SelectedRows.Count == 0)
            {
                MessageBox.Show("الرجاء تحديد قسم للحذف", "تنبيه",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int departmentId = Convert.ToInt32(DgvDepartments.SelectedRows[0].Cells["id"].Value);
            string departmentName = DgvDepartments.SelectedRows[0].Cells["name"].Value?.ToString() ?? "";

            // التحقق من وجود وثائق مرتبطة
            if (HasRelatedDocuments(departmentId))
            {
                DialogResult confirm = MessageBox.Show(
                    $"⚠️ هناك وثائق مرتبطة بهذا القسم!\n\n" +
                    $"سيتم حذف القسم: {departmentName}\n" +
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
                    $"⚠️ هل أنت متأكد من حذف القسم: {departmentName}؟",
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
                string updateDocs = "UPDATE Documents SET from_department_id = NULL WHERE from_department_id = @id";
                var updateParams = new Dictionary<string, object> { { "@id", departmentId } };
                DatabaseModuleLite.ExecuteNonQuery(updateDocs, updateParams);

                updateDocs = "UPDATE Documents SET to_department_id = NULL WHERE to_department_id = @id";
                DatabaseModuleLite.ExecuteNonQuery(updateDocs, updateParams);

                // حذف القسم
                string query = "DELETE FROM Departments WHERE id = @id";
                var parameters = new Dictionary<string, object> { { "@id", departmentId } };
                int rowsAffected = DatabaseModuleLite.ExecuteNonQuery(query, parameters);

                if (rowsAffected > 0)
                {
                    LoadDepartments();
                    UpdateStatus($"✅ تم حذف القسم: {departmentName}");
                }
                else
                {
                    MessageBox.Show("❌ فشل في حذف القسم", "خطأ",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في حذف القسم: {ex.Message}", "خطأ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private bool HasRelatedDocuments(int departmentId)
        {
            try
            {
                string query = @"
                    SELECT COUNT(*) FROM Documents 
                    WHERE from_department_id = @id OR to_department_id = @id";
                var parameters = new Dictionary<string, object> { { "@id", departmentId } };
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
                if (departmentsData == null || departmentsData.Rows.Count == 0)
                {
                    MessageBox.Show("لا توجد بيانات للتصدير", "تنبيه",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "ملفات CSV (*.csv)|*.csv|جميع الملفات (*.*)|*.*";
                    sfd.Title = "تصدير إلى CSV";
                    sfd.FileName = $"الأقسام_{DateTime.Now:yyyyMMdd_HHmmss}.csv";

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
                string[] headers = { "المعرف", "اسم القسم", "الوصف", "نشط", "تاريخ الإنشاء" };
                writer.WriteLine(string.Join(",", headers));

                foreach (DataRow row in departmentsData.Rows)
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

        private void TxtSearch_TextChanged_1(object sender, EventArgs e)
        {

        }
    }
}