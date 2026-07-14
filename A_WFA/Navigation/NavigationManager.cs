using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace A_WFA.Navigation
{
    public static class NavigationManager
    {
        #region المتغيرات

        // النموذج الرئيسي
        private static Form _mainForm;

        // النموذج الحالي
        private static Form _currentForm;

        // حفظ النماذج السابقة
        private static readonly Stack<Form> _history = new Stack<Form>();

        // حفظ نسخة واحدة من كل نموذج
        private static readonly Dictionary<Type, Form> _forms =
            new Dictionary<Type, Form>();

        private static bool _closingSystem = false;

        #endregion


        #region التهيئة

        /// <summary>
        /// تعيين النموذج الرئيسي للنظام
        /// يستدعى مرة واحدة فقط من MainForm
        /// </summary>
        public static void Initialize(Form mainForm)
        {
            _mainForm = mainForm;
            _currentForm = mainForm;

            _forms[mainForm.GetType()] = mainForm;

            mainForm.FormClosed += MainForm_FormClosed;
        }

        #endregion


        #region الانتقال بين النماذج



        #endregion


        #region إنشاء أو استرجاع النموذج


        private static Form GetForm<T>() where T : Form, new()
        {
            Type type = typeof(T);


            if (_forms.ContainsKey(type))
            {
                Form oldForm = _forms[type];


                // إذا كان النموذج مغلقاً نزيله وننشئ نسخة جديدة
                if (oldForm.IsDisposed)
                {
                    _forms.Remove(type);
                }
                else
                {
                    return oldForm;
                }
            }


            Form frm = new T();


            frm.FormClosed += ChildForm_FormClosed;


            _forms[type] = frm;


            return frm;
        }


        #endregion



        #region الرجوع للخلف


        #endregion



        #region الرئيسية


        /// <summary>
        /// العودة للشاشة الرئيسية
        /// </summary>
        public static void GoHome()
        {

            if (_mainForm == null)
                return;


            foreach (Form frm in _forms.Values.ToList())
            {

                if (frm != _mainForm)
                    frm.Hide();

            }


            _history.Clear();


            _currentForm = _mainForm;


            _mainForm.Show();
            _mainForm.BringToFront();

        }


        #endregion



        #region إغلاق النماذج

        #endregion



        #region تنظيف


        public static void CloseAll()
        {

            _closingSystem = true;


            foreach (Form frm in _forms.Values)
            {

                if (frm != null)
                    frm.Dispose();

            }


            _forms.Clear();
            _history.Clear();

        }


        #endregion
        

        #region إغلاق النماذج

        private static void ChildForm_FormClosed(
            object sender,
            FormClosedEventArgs e)
        {
            if (_closingSystem)
                return;


            Form closedForm = sender as Form;


            // إزالة النموذج المغلق من الذاكرة
            if (closedForm != null)
            {
                Type type = closedForm.GetType();

                if (_forms.ContainsKey(type))
                {
                    _forms.Remove(type);
                }
            }


            // إذا كان النموذج الحالي تم إغلاقه
            if (closedForm == _currentForm)
            {
                _currentForm = null;

                GoBack();
            }
        }



        private static void MainForm_FormClosed(
            object sender,
            FormClosedEventArgs e)
        {
            _closingSystem = true;

            CloseAll();

            Application.Exit();
        }

        #endregion


        public static void GoBack()
        {
            while (_history.Count > 0)
            {
                Form previous = _history.Pop();


                if (previous == null ||
                    previous.IsDisposed)
                {
                    continue;
                }


                _currentForm = previous;


                previous.Show();
                previous.WindowState = FormWindowState.Normal;
                previous.BringToFront();

                return;
            }


            GoHome();
        }
        public static void Navigate<T>() where T : Form, new()
        {
            if (_mainForm == null)
                return;


            try
            {
                Form nextForm = GetForm<T>();


                if (_currentForm != null &&
                    _currentForm != nextForm)
                {
                    _history.Push(_currentForm);

                    _currentForm.Hide();
                }


                _currentForm = nextForm;


                nextForm.Show();
                nextForm.WindowState = FormWindowState.Normal;
                nextForm.BringToFront();

            }
            catch (Exception ex)
            {
                _currentForm?.Show();

                MessageBox.Show(
                    ex.ToString(),
                    "خطأ في التنقل",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}




