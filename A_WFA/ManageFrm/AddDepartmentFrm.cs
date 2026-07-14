using A_WFA.Database.LTE;
using A_WFA.Navigation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace A_WFA.ManageFrm
{
    public partial class AddDepartmentFrm : Form
    {
        private int? editingDepartmentId = null;
        private bool isEditMode = false;

        public AddDepartmentFrm(int? departmentId = null)
        {
            InitializeComponent();

            this.Load += AddDepartmentFrm_Load;

            BtnSave.Click += BtnSave_Click;
            BtnCancel.Click += BtnCancel_Click;

            if (departmentId.HasValue && departmentId.Value > 0)
            {
                editingDepartmentId = departmentId;
                isEditMode = true;
                this.Text = "✏️ تعديل قسم";
                BtnSave.Text = "💾 تحديث";
                LoadDepartmentData(departmentId.Value);
            }
            else
            {
                this.Text = "🏢 إضافة قسم جديد";
                BtnSave.Text = "💾 حفظ";
            }

            SetupPlaceholderTexts();
        }

        #region "تحميل النموذج"

        private void AddDepartmentFrm_Load(object sender, EventArgs e)
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
                TxtName.Text = "أدخل اسم القسم...";
                TxtName.ForeColor = Color.Gray;
            }

            TxtName.Enter += (s, e) =>
            {
                if (TxtName.Text == "أدخل اسم القسم...")
                {
                    TxtName.Text = "";
                    TxtName.ForeColor = Color.Black;
                }
            };

            TxtName.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(TxtName.Text))
                {
                    TxtName.Text = "أدخل اسم القسم...";
                    TxtName.ForeColor = Color.Gray;
                }
            };

            // Placeholder لـ TxtDescription
            if (string.IsNullOrEmpty(TxtDescription.Text))
            {
                TxtDescription.Text = "وصف القسم...";
                TxtDescription.ForeColor = Color.Gray;
            }

            TxtDescription.Enter += (s, e) =>
            {
                if (TxtDescription.Text == "وصف القسم...")
                {
                    TxtDescription.Text = "";
                    TxtDescription.ForeColor = Color.Black;
                }
            };

            TxtDescription.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(TxtDescription.Text))
                {
                    TxtDescription.Text = "وصف القسم...";
                    TxtDescription.ForeColor = Color.Gray;
                }
            };
        }

        #endregion

        #region "تحميل البيانات"

        private void LoadDepartmentData(int departmentId)
        {
            try
            {
                string query = "SELECT * FROM Departments WHERE id = @id";
                var parameters = new Dictionary<string, object> { { "@id", departmentId } };
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
                MessageBox.Show($"خطأ في تحميل بيانات القسم: {ex.Message}", "خطأ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region "التحقق من صحة الإدخال"

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(TxtName.Text) || TxtName.Text == "أدخل اسم القسم...")
            {
                MessageBox.Show("يرجى إدخال اسم القسم", "تنبيه",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TxtName.Focus();
                return false;
            }

            if (TxtName.Text.Length < 3)
            {
                MessageBox.Show("اسم القسم يجب أن يكون 3 أحرف على الأقل", "تنبيه",
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
                string query = "SELECT COUNT(*) FROM Departments WHERE name = @name";
                if (isEditMode && editingDepartmentId.HasValue)
                {
                    query += " AND id != @id";
                }

                var parameters = new Dictionary<string, object> { { "@name", name } };
                if (isEditMode && editingDepartmentId.HasValue)
                {
                    parameters.Add("@id", editingDepartmentId.Value);
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

                if (isEditMode && editingDepartmentId.HasValue)
                {
                    // تحديث - SQLite
                    string query = @"
                        UPDATE Departments SET
                            name = @name,
                            description = @description,
                            is_active = @isActive
                        WHERE id = @id";

                    var parameters = new Dictionary<string, object>
                    {
                        { "@name", name },
                        { "@description", string.IsNullOrEmpty(description) ? DBNull.Value : (object)description },
                        { "@isActive", isActive ? 1 : 0 },
                        { "@id", editingDepartmentId.Value }
                    };

                    success = DatabaseModuleLite.ExecuteNonQuery(query, parameters) > 0;
                }
                else
                {
                    // إضافة - SQLite
                    string query = @"
                        INSERT INTO Departments (name, description, is_active, created_at)
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
                    MessageBox.Show($"✅ تم {(isEditMode ? "تحديث" : "إضافة")} القسم بنجاح!", "نجاح",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show($"❌ فشل في {(isEditMode ? "تحديث" : "إضافة")} القسم", "خطأ",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في حفظ القسم: {ex.Message}", "خطأ",
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