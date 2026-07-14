using A_WFA.Database.LTE;
using A_WFA.Navigation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;

namespace A_WFA.ManageFrm
{
    public partial class AddCategoryFrm : Form
    {
        private int? editingCategoryId = null;
        private bool isEditMode = false;

        public AddCategoryFrm(int? categoryId = null)
        {
            InitializeComponent();

            this.Load += AddCategoryFrm_Load;

            BtnSave.Click += BtnSave_Click;
            BtnCancel.Click += BtnCancel_Click;

            if (categoryId.HasValue && categoryId.Value > 0)
            {
                editingCategoryId = categoryId;
                isEditMode = true;
                this.Text = "✏️ تعديل تصنيف";
                BtnSave.Text = "💾 تحديث";
                LoadCategoryData(categoryId.Value);
            }
            else
            {
                this.Text = "📂 إضافة تصنيف جديد";
                BtnSave.Text = "💾 حفظ";
            }

            SetupPlaceholderTexts();
        }

        #region "تحميل النموذج"

        private void AddCategoryFrm_Load(object sender, EventArgs e)
        {
            StyleButtons();
        }

        private void StyleButtons()
        {
            BtnSave.MouseEnter += (s, ev) => { BtnSave.BackColor = ControlPaint.Light(BtnSave.BackColor, 0.2f); };
            BtnSave.MouseLeave += (s, ev) => { BtnSave.BackColor = Color.FromArgb(46, 204, 113); };

            BtnCancel.MouseEnter += (s, ev) => { BtnCancel.BackColor = ControlPaint.Light(BtnCancel.BackColor, 0.2f); };
            BtnCancel.MouseLeave += (s, ev) => { BtnCancel.BackColor = Color.FromArgb(149, 165, 166); };
        }

        private void SetupPlaceholderTexts()
        {
            // Placeholder لـ TxtName
            if (string.IsNullOrEmpty(TxtName.Text))
            {
                TxtName.Text = "أدخل اسم التصنيف...";
                TxtName.ForeColor = Color.Gray;
            }

            TxtName.Enter += (s, e) =>
            {
                if (TxtName.Text == "أدخل اسم التصنيف...")
                {
                    TxtName.Text = "";
                    TxtName.ForeColor = Color.Black;
                }
            };

            TxtName.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(TxtName.Text))
                {
                    TxtName.Text = "أدخل اسم التصنيف...";
                    TxtName.ForeColor = Color.Gray;
                }
            };

            // Placeholder لـ TxtDescription
            if (string.IsNullOrEmpty(TxtDescription.Text))
            {
                TxtDescription.Text = "وصف التصنيف...";
                TxtDescription.ForeColor = Color.Gray;
            }

            TxtDescription.Enter += (s, e) =>
            {
                if (TxtDescription.Text == "وصف التصنيف...")
                {
                    TxtDescription.Text = "";
                    TxtDescription.ForeColor = Color.Black;
                }
            };

            TxtDescription.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(TxtDescription.Text))
                {
                    TxtDescription.Text = "وصف التصنيف...";
                    TxtDescription.ForeColor = Color.Gray;
                }
            };
        }

        #endregion

        #region "تحميل البيانات"

        private void LoadCategoryData(int categoryId)
        {
            try
            {
                // ✅ استخدام DatabaseModuleLite مع SQLite
                string query = "SELECT * FROM Document_Categories WHERE id = @id";
                var parameters = new Dictionary<string, object> { { "@id", categoryId } };
                DataTable dt = DatabaseModuleLite.ExecuteQuery(query, parameters);

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    TxtName.Text = row["name"].ToString();
                    TxtName.ForeColor = Color.Black;
                    TxtDescription.Text = row["description"] != DBNull.Value ? row["description"].ToString() : "";
                    TxtDescription.ForeColor = Color.Black;
                    ChkIsActive.Checked = Convert.ToBoolean(row["is_active"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في تحميل بيانات التصنيف: {ex.Message}", "خطأ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region "التحقق من صحة الإدخال"

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(TxtName.Text) || TxtName.Text == "أدخل اسم التصنيف...")
            {
                MessageBox.Show("يرجى إدخال اسم التصنيف", "تنبيه",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TxtName.Focus();
                return false;
            }

            if (TxtName.Text.Length < 3)
            {
                MessageBox.Show("اسم التصنيف يجب أن يكون 3 أحرف على الأقل", "تنبيه",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TxtName.Focus();
                return false;
            }

            // التحقق من عدم تكرار الاسم
            if (IsNameExists(TxtName.Text.Trim()))
            {
                MessageBox.Show("⚠️ هذا الاسم موجود بالفعل! يرجى اختيار اسم آخر.", "اسم مكرر",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TxtName.Focus();
                TxtName.SelectAll();
                return false;
            }

            return true;
        }

        private bool IsNameExists(string name)
        {
            try
            {
                // ✅ استعلام SQLite
                string query = "SELECT COUNT(*) FROM Document_Categories WHERE name = @name";
                if (isEditMode && editingCategoryId.HasValue)
                {
                    query += " AND id != @id";
                }

                var parameters = new Dictionary<string, object> { { "@name", name } };
                if (isEditMode && editingCategoryId.HasValue)
                {
                    parameters.Add("@id", editingCategoryId.Value);
                }

                object result = DatabaseModuleLite.ExecuteScalar(query, parameters);
                return result != null && Convert.ToInt32(result) > 0;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region "حفظ البيانات"

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            try
            {
                Cursor = Cursors.WaitCursor;

                string name = TxtName.Text.Trim();
                string description = TxtDescription.Text.Trim();
                bool isActive = ChkIsActive.Checked;

                bool success = false;

                if (isEditMode && editingCategoryId.HasValue)
                {
                    // ✅ تحديث - SQLite
                    string query = @"
                        UPDATE Document_Categories SET
                            name = @name,
                            description = @description,
                            is_active = @isActive
                        WHERE id = @id";

                    var parameters = new Dictionary<string, object>
                    {
                        { "@name", name },
                        { "@description", string.IsNullOrEmpty(description) ? DBNull.Value : (object)description },
                        { "@isActive", isActive ? 1 : 0 },
                        { "@id", editingCategoryId.Value }
                    };

                    success = DatabaseModuleLite.ExecuteNonQuery(query, parameters) > 0;
                }
                else
                {
                    // ✅ إضافة - SQLite
                    string query = @"
                        INSERT INTO Document_Categories (name, description, is_active, created_at)
                        VALUES (@name, @description, @isActive, CURRENT_TIMESTAMP)";

                    var parameters = new Dictionary<string, object>
                    {
                        { "@name", name },
                        { "@description", string.IsNullOrEmpty(description) ? DBNull.Value : (object)description },
                        { "@isActive", isActive ? 1 : 0 }
                    };

                    success = DatabaseModuleLite.ExecuteNonQuery(query, parameters) > 0;
                }

                if (success)
                {
                    MessageBox.Show($"✅ تم {(isEditMode ? "تحديث" : "إضافة")} التصنيف بنجاح!", "نجاح",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show($"❌ فشل في {(isEditMode ? "تحديث" : "إضافة")} التصنيف", "خطأ",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في حفظ التصنيف: {ex.Message}", "خطأ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        #endregion

        #region "إلغاء"

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            if (isEditMode)
            {
                DialogResult result = MessageBox.Show("هل تريد إلغاء التعديلات؟", "تأكيد",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                    NavigationManager.GoBack();
                }
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
                NavigationManager.GoBack();
            }
        }

        #endregion
    }
}