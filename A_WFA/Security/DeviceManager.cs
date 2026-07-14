using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;

namespace A_WFA.Security
{
    public static class DeviceManager
    {
        private static string _deviceGuid;
        private static int _deviceId;
        private static bool _isInitialized = false;
        private static readonly object _lock = new object();

        /// <summary>
        /// تهيئة الجهاز - تسجيل أو استرجاع معرف الجهاز
        /// </summary>
        public static void InitializeDevice(int userId = 1)
        {
            lock (_lock)
            {
                if (_isInitialized) return;

                try
                {
                    // 1. البحث عن الجهاز في قاعدة البيانات
                    string deviceGuid = GetDeviceFingerprint();
                    DataRow device = FindDeviceByGuid(deviceGuid);

                    if (device != null)
                    {
                        // جهاز موجود
                        _deviceGuid = deviceGuid;
                        _deviceId = Convert.ToInt32(device["id"]);
                        _isInitialized = true;

                        // تحديث آخر تواصل
                        UpdateDeviceLastSync(_deviceId);

                        Debug.WriteLine($"✅ تم العثور على الجهاز: {_deviceGuid} (ID: {_deviceId})");
                        return;
                    }

                    // 2. جهاز جديد - تسجيله
                    RegisterDevice(deviceGuid, userId);
                    _isInitialized = true;

                    Debug.WriteLine($"✅ تم تسجيل جهاز جديد: {_deviceGuid}");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"❌ خطأ في تهيئة الجهاز: {ex.Message}");
                    throw;
                }
            }
        }

        /// <summary>
        /// الحصول على بصمة الجهاز (معرف فريد)
        /// </summary>
        private static string GetDeviceFingerprint()
        {
            try
            {
                // استخدام معلومات الجهاز لتوليد معرف فريد
                string machineName = Environment.MachineName;
                string userName = Environment.UserName;
                string macAddress = GetMacAddress();

                string raw = $"{machineName}|{userName}|{macAddress}";
                using (var sha256 = SHA256.Create())
                {
                    byte[] hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(raw));
                    return Guid.Parse(Convert.ToBase64String(hash).Substring(0, 32)).ToString();
                }
            }
            catch
            {
                // في حالة الفشل، استخدام GUID عشوائي
                return Guid.NewGuid().ToString();
            }
        }

        /// <summary>
        /// الحصول على عنوان MAC للجهاز
        /// </summary>
        private static string GetMacAddress()
        {
            try
            {
                foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
                {
                    if (nic.OperationalStatus == OperationalStatus.Up &&
                        nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                    {
                        return nic.GetPhysicalAddress().ToString();
                    }
                }
            }
            catch { }
            return "00-00-00-00-00-00";
        }

        /// <summary>
        /// البحث عن جهاز بواسطة GUID
        /// </summary>
        private static DataRow FindDeviceByGuid(string guid)
        {
            try
            {
                string query = "SELECT * FROM Devices WHERE device_guid = @guid";
                var parameters = new Dictionary<string, object> { { "@guid", guid } };
                DataTable dt = DatabaseManagerLite.ExecuteQuery(query, parameters);
                return dt.Rows.Count > 0 ? dt.Rows[0] : null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// تسجيل جهاز جديد
        /// </summary>
        /// <summary>
        /// تسجيل جهاز جديد
        /// </summary>
        private static void RegisterDevice(string guid, int userId)
        {
            try
            {
                // ✅ التحقق من عدم وجود الجهاز مسبقاً (لتجنب التكرار)
                DataRow existing = FindDeviceByGuid(guid);
                if (existing != null)
                {
                    _deviceId = Convert.ToInt32(existing["id"]);
                    _deviceGuid = guid;
                    Debug.WriteLine($"✅ الجهاز موجود بالفعل: {_deviceGuid} (ID: {_deviceId})");
                    return;
                }

                string deviceName = $"{Environment.MachineName} - {Environment.UserName}";
                string appVersion = System.Windows.Forms.Application.ProductVersion;

                string query = @"
            INSERT INTO Devices (
                device_guid, device_name, device_type,
                user_id, installation_date, app_version, is_active
            ) VALUES (
                @guid, @name, @type,
                @userId, CURRENT_TIMESTAMP, @version, 1
            )";

                var parameters = new Dictionary<string, object>
        {
            { "@guid", guid },
            { "@name", deviceName },
            { "@type", "Desktop" },
            { "@userId", userId },
            { "@version", appVersion }
        };

                object result = DatabaseManagerLite.ExecuteScalar(query, parameters);
                _deviceId = result != null ? Convert.ToInt32(result) : 0;
                _deviceGuid = guid;

                // تسجيل العملية
                DatabaseManagerLite.SafeLogAuditTrail(userId, "DEVICE_REGISTERED",
                    $"تم تسجيل جهاز جديد: {deviceName}");
            }
            catch (Exception ex)
            {
                throw new Exception($"فشل تسجيل الجهاز: {ex.Message}");
            }
        }

        /// <summary>
        /// تحديث تاريخ آخر مزامنة
        /// </summary>
        private static void UpdateDeviceLastSync(int deviceId)
        {
            try
            {
                string query = "UPDATE Devices SET last_sync_date = CURRENT_TIMESTAMP WHERE id = @id";
                var parameters = new Dictionary<string, object> { { "@id", deviceId } };
                DatabaseManagerLite.ExecuteNonQuery(query, parameters);
            }
            catch { }
        }

        /// <summary>
        /// الحصول على معرف الجهاز الحالي
        /// </summary>
        public static int GetCurrentDeviceId()
        {
            if (!_isInitialized)
                throw new InvalidOperationException("الجهاز غير مهيأ");
            return _deviceId;
        }

        /// <summary>
        /// الحصول على GUID الجهاز الحالي
        /// </summary>
        public static string GetCurrentDeviceGuid()
        {
            if (!_isInitialized)
                throw new InvalidOperationException("الجهاز غير مهيأ");
            return _deviceGuid;
        }

        /// <summary>
        /// الحصول على جميع الأجهزة
        /// </summary>
        public static DataTable GetAllDevices(bool activeOnly = true)
        {
            string query = "SELECT * FROM Devices";
            if (activeOnly)
                query += " WHERE is_active = 1";
            query += " ORDER BY installation_date DESC";
            return DatabaseManagerLite.ExecuteQuery(query);
        }

        /// <summary>
        /// تحديث حالة الجهاز
        /// </summary>
        public static bool UpdateDeviceStatus(int deviceId, bool isActive)
        {
            try
            {
                string query = "UPDATE Devices SET is_active = @active WHERE id = @id";
                var parameters = new Dictionary<string, object>
                {
                    { "@active", isActive ? 1 : 0 },
                    { "@id", deviceId }
                };
                return DatabaseManagerLite.ExecuteNonQuery(query, parameters) > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}