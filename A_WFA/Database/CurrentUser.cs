using System;

namespace A_WFA
{
    /// <summary>
    /// كلاس المستخدم الحالي - يحفظ بيانات المستخدم المسجل دخوله
    /// </summary>
    public static class CurrentUser
    {
        private static int _id = 1;
        private static string _username = "Admin";
        private static string _fullName = "مدير النظام";
        private static string _role = "Admin";
        private static bool _isAuthenticated = true;

        /// <summary>
        /// معرف المستخدم
        /// </summary>
        public static int Id
        {
            get => _id;
            set => _id = value;
        }

        /// <summary>
        /// اسم المستخدم
        /// </summary>
        public static string Username
        {
            get => _username;
            set => _username = value;
        }

        /// <summary>
        /// الاسم الكامل
        /// </summary>
        public static string FullName
        {
            get => _fullName;
            set => _fullName = value;
        }

        /// <summary>
        /// الدور / الصلاحية
        /// </summary>
        public static string Role
        {
            get => _role;
            set => _role = value;
        }

        /// <summary>
        /// هل المستخدم مصادق عليه
        /// </summary>
        public static bool IsAuthenticated
        {
            get => _isAuthenticated;
            set => _isAuthenticated = value;
        }

        /// <summary>
        /// تسجيل دخول المستخدم
        /// </summary>
        public static void Login(int userId, string username, string fullName, string role)
        {
            Id = userId;
            Username = username;
            FullName = fullName;
            Role = role;
            IsAuthenticated = true;
        }

        /// <summary>
        /// تسجيل خروج المستخدم
        /// </summary>
        public static void Logout()
        {
            Id = 0;
            Username = string.Empty;
            FullName = string.Empty;
            Role = string.Empty;
            IsAuthenticated = false;
        }

        /// <summary>
        /// التحقق من صلاحية المستخدم
        /// </summary>
        public static bool HasRole(string role)
        {
            return IsAuthenticated && Role == role;
        }

        /// <summary>
        /// التحقق من صلاحية المستخدم (Admin أو Manager)
        /// </summary>
        public static bool IsAdminOrManager()
        {
            return IsAuthenticated && (Role == "Admin" || Role == "Manager");
        }
    }
}