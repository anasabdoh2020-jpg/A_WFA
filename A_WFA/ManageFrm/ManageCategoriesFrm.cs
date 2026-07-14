using A_WFA.Database.LTE;
using A_WFA.Navigation;
using A_WFA.uti;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace A_WFA.ManageFrm
{
    public partial class ManageCategoriesFrm : Form
    {
        private DataTable categoriesData;
        private string currentSearch = "";
        private int? editingCategoryId = null;

        public ManageCategoriesFrm()
        {
            InitializeComponent();
            this.Load += ManageCategoriesFrm_Load;

            // ربط الأحداث
            BtnAddNew.Click += BtnAddNew_Click;
            BtnRefresh.Click += BtnRefresh_Click;
            BtnDeleteSelected.Click += BtnDeleteSelected_Click;
            BtnExport.Click += BtnExport_Click;
            TxtSearch.TextChanged += TxtSearch_TextChanged;
            DgvCategories.CellDoubleClick += DgvCategories_CellDoubleClick;
            DgvCategories.KeyDown += DgvCategories_KeyDown;

            SetupSearchBox();
        }

        #region "تحميل النموذج"

        private void ManageCategoriesFrm_Load(object sender, EventArgs e)
        {
            LoadCategories();
            SetupDataGridView();
            UpdateStatus("✅ جاهز");
        }

        private void SetupDataGridView()
        {
            DgvCategories.AutoGenerateColumns = false;
            DgvCategories.Columns.Clear();

            DgvCategories.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "id",
                HeaderText = "المعرف",
                DataPropertyName = "id",
                Width = 60,
                Visible = false
            });

            DgvCategories.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "name",
                HeaderText = "اسم التصنيف",
                DataPropertyName = "name",
                Width = 300
            });

            DgvCategories.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "description",
                HeaderText = "الوصف",
                DataPropertyName = "description",
                Width = 400
            });

            DgvCategories.Columns.Add(new DataGridViewCheckBoxColumn()
            {
                Name = "is_active",
                HeaderText = "نشط",
                DataPropertyName = "is_active",
                Width = 80
            });

            DgvCategories.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "created_at",
                HeaderText = "تاريخ الإنشاء",
                DataPropertyName = "created_at",
                Width = 150
            });
        }

        #endregion

        #region "تحميل البيانات"

        private void LoadCategories()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                UpdateStatus("⏳ جاري تحميل التصنيفات...");

                string search = GetSearchCondition();

                // ✅ استعلام SQLite (بدون FORMAT)
                string query = @"
                    SELECT 
                        id,
                        name,
                        description,
                        is_active,
                        created_at
                    FROM Document_Categories
                    WHERE 1=1 " + search + @"
                    ORDER BY name";

                categoriesData = DatabaseModuleLite.ExecuteQuery(query);

                // ✅ تنسيق التاريخ في DataTable بدلاً من SQL
                if (categoriesData.Columns.Contains("created_at"))
                {
                    foreach (DataRow row in categoriesData.Rows)
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

                DgvCategories.DataSource = categoriesData;
                DgvCategories.AutoResizeColumns();

                LblRecordCount.Text = $"عدد السجلات: {categoriesData.Rows.Count}";
                UpdateStatus($"✅ تم تحميل {categoriesData.Rows.Count} تصنيف");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في تحميل التصنيفات: {ex.Message}", "خطأ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateStatus($"❌ خطأ: {ex.Message}");
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private string GetSearchCondition()
        {
            if (string.IsNullOrEmpty(currentSearch) || currentSearch == "🔍 بحث...")
                return "";

            // ✅ استخدام @searchParameter بدلاً من concatenation
            return $" AND (name LIKE @search OR description LIKE @search)";
        }

        #endregion

        #region "البحث"

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
                LoadCategories();
            }
            else if (string.IsNullOrWhiteSpace(TxtSearch.Text) || TxtSearch.Text == "🔍 بحث...")
            {
                currentSearch = "";
                LoadCategories();
            }
        }

        #endregion

        #region "أزرار التحكم"

        private void BtnAddNew_Click(object sender, EventArgs e)
        {
            ShowCategoryForm();
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadCategories();
        }

        private void BtnDeleteSelected_Click(object sender, EventArgs e)
        {
            DeleteSelectedCategory();
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            ExportToExcel();
        }

        #endregion

        #region "التفاعل مع الجدول"

        private void DgvCategories_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                EditCategory();
            }
        }

        private void DgvCategories_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                EditCategory();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Delete)
            {
                DeleteSelectedCategory();
                e.Handled = true;
            }
        }

        #endregion

        #region "عمليات على التصنيفات"

        private void ShowCategoryForm(int? categoryId = null)
        {
            try
            {
                using (var frm = new AddCategoryFrm(categoryId))
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        LoadCategories();
                        UpdateStatus($"✅ تم {(categoryId.HasValue ? "تحديث" : "إضافة")} التصنيف بنجاح");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في فتح النموذج: {ex.Message}", "خطأ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EditCategory()
        {
            if (DgvCategories.SelectedRows.Count == 0)
            {
                MessageBox.Show("الرجاء تحديد تصنيف للتعديل", "تنبيه",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int categoryId = Convert.ToInt32(DgvCategories.SelectedRows[0].Cells["id"].Value);
            ShowCategoryForm(categoryId);
        }

        private void DeleteSelectedCategory()
        {
            if (DgvCategories.SelectedRows.Count == 0)
            {
                MessageBox.Show("الرجاء تحديد تصنيف للحذف", "تنبيه",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int categoryId = Convert.ToInt32(DgvCategories.SelectedRows[0].Cells["id"].Value);
            string categoryName = DgvCategories.SelectedRows[0].Cells["name"].Value?.ToString() ?? "";

            // ✅ التحقق من وجود وثائق مرتبطة بهذا التصنيف
            if (HasRelatedDocuments(categoryId))
            {
                DialogResult confirm = MessageBox.Show(
                    $"⚠️ هناك وثائق مرتبطة بهذا التصنيف!\n\n" +
                    $"سيتم حذف التصنيف: {categoryName}\n" +
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
                    $"⚠️ هل أنت متأكد من حذف التصنيف: {categoryName}؟",
                    "تأكيد الحذف",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result != DialogResult.Yes)
                    return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;

                // ✅ تحديث الوثائق المرتبطة (تعيين NULL)
                string updateDocs = "UPDATE Documents SET category_id = NULL WHERE category_id = @id";
                var updateParams = new Dictionary<string, object> { { "@id", categoryId } };
                DatabaseModuleLite.ExecuteNonQuery(updateDocs, updateParams);

                // ✅ حذف التصنيف
                string query = "DELETE FROM Document_Categories WHERE id = @id";
                var parameters = new Dictionary<string, object> { { "@id", categoryId } };
                int rowsAffected = DatabaseModuleLite.ExecuteNonQuery(query, parameters);

                if (rowsAffected > 0)
                {
                    LoadCategories();
                    UpdateStatus($"✅ تم حذف التصنيف: {categoryName}");
                }
                else
                {
                    MessageBox.Show("❌ فشل في حذف التصنيف", "خطأ",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في حذف التصنيف: {ex.Message}", "خطأ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private bool HasRelatedDocuments(int categoryId)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM Documents WHERE category_id = @id";
                var parameters = new Dictionary<string, object> { { "@id", categoryId } };
                object result = DatabaseModuleLite.ExecuteScalar(query, parameters);
                return result != null && Convert.ToInt32(result) > 0;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region "التصدير"

        private void ExportToExcel()
        {
            try
            {
                if (categoriesData == null || categoriesData.Rows.Count == 0)
                {
                    MessageBox.Show("لا توجد بيانات للتصدير", "تنبيه",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "ملفات Excel (*.xlsx)|*.xlsx|ملفات CSV (*.csv)|*.csv|جميع الملفات (*.*)|*.*";
                    sfd.Title = "تصدير إلى Excel";
                    sfd.FileName = $"التصنيفات_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        // ✅ تصدير إلى CSV (بديل بسيط)
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
            try
            {
                using (var writer = new System.IO.StreamWriter(filePath, false, System.Text.Encoding.UTF8))
                {
                    // كتابة الرأس
                    string[] headers = { "المعرف", "اسم التصنيف", "الوصف", "نشط", "تاريخ الإنشاء" };
                    writer.WriteLine(string.Join(",", headers));

                    // كتابة البيانات
                    foreach (DataRow row in categoriesData.Rows)
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
            catch
            {
                throw;
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
            NavigationManager.GoBack();
            this.Hide();
        }
    }
}