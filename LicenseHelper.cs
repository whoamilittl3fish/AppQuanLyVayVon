using System;
using System.IO;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;
using System.Net.Http;

namespace QuanLyVayVon
{
    public static class LicenseHelper
    {
        public class LicenseData
        {
            public string LoaiKey { get; set; }
            public long CreatedAtUnix { get; set; }
            public long? ExpiredAtUnix { get; set; }
            public string HWID_1 { get; set; }
            public string HWID_2 { get; set; }
            public string HWID_3 { get; set; }
            public string HWID_4 { get; set; }
        }

        public class LicenseKey
        {
            public LicenseData Data { get; set; }
            public string Sign { get; set; }
        }

        private static readonly string keyFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "lic.dat");
        private static readonly string publicKeyPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PublicKey", "public_key.xml");
        private static long? CachedNetworkTime = null;

        public static bool KiemTraFilePublicKeyTonTai(out string fullPath)
        {
            fullPath = publicKeyPath;
            if (!File.Exists(fullPath))
            {
                MessageBox.Show($"Không tìm thấy file public key tại:\n{fullPath}", "Lỗi bản quyền", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
                return false;
            }
            return true;
        }

        public static void SaveKeyVaoFile(string base64Key)
        {
            try { File.WriteAllText(keyFilePath, base64Key); }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu key: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static string LoadKeyTuFile()
        {
            try { return File.Exists(keyFilePath) ? File.ReadAllText(keyFilePath).Trim() : null; }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải key: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public static string LayThongTinThoiGianConLai()
        {
            string base64Key = LoadKeyTuFile();
            try
            {
                var licenseKey = JsonSerializer.Deserialize<LicenseKey>(Encoding.UTF8.GetString(Convert.FromBase64String(base64Key)));
                if (licenseKey?.Data == null)
                    return "Không đọc được dữ liệu key.";

                var data = licenseKey.Data;

                if (data.LoaiKey == "Lifetime")
                    return "LIFETIME";

                if (data.LoaiKey == "Trial" && data.ExpiredAtUnix != null)
                {
                    long nowOnline = GetAccurateOnlineTime();
                    if (nowOnline == -1)
                        return "Không thể kiểm tra thời gian (mất mạng)";

                    long remaining = data.ExpiredAtUnix.Value - nowOnline;
                    if (remaining <= 0)
                        return "Dùng thử đã hết hạn.";

                    var ts = TimeSpan.FromSeconds(remaining);
                    return $"Dùng thử còn {ts.Days} ngày {ts.Hours} giờ {ts.Minutes} phút";
                }

                return "Loại key không xác định.";
            }
            catch
            {
                return "Key bị hỏng hoặc không hợp lệ.";
            }
        }

        public static int VerifyKeyWithPublicKey(string base64Key)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(base64Key)) return 0;

                var key = JsonSerializer.Deserialize<LicenseKey>(Encoding.UTF8.GetString(Convert.FromBase64String(base64Key)));
                if (key?.Data == null || string.IsNullOrEmpty(key.Sign)) return 0;

                if (!File.Exists(publicKeyPath)) return 0;
                using var rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(File.ReadAllText(publicKeyPath));

                byte[] dataBytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(key.Data));
                byte[] signBytes = Convert.FromBase64String(key.Sign);
                var sha256 = SHA256.Create();

                if (!rsa.VerifyData(dataBytes, sha256, signBytes))
                {
                    MessageBox.Show("Chữ ký không hợp lệ. Có thể key đã bị chỉnh sửa.", "Lỗi xác minh", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 0;
                }

                if (!IsValidHWID(key.Data))
                {
                    MessageBox.Show("Không khớp HWID. Vui lòng kiểm tra lại phần cứng.", "Lỗi HWID", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 0;
                }

                var data = key.Data;

                if (data.LoaiKey == "Lifetime")
                {
                    SaveKeyVaoFile(base64Key);
                    return 1;
                }

                if (data.LoaiKey == "Trial")
                {
                    if (data.ExpiredAtUnix == null) return 0;
                    long nowOnline = GetAccurateOnlineTime();
                    if (nowOnline == -1)
                    {
                        MessageBox.Show("Vui lòng kết nối mạng để sử dụng bản dùng thử.", "Yêu cầu mạng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return 0;
                    }

                    long remaining = data.ExpiredAtUnix.Value - nowOnline;
                    if (remaining <= 0)
                    {
                        MessageBox.Show("Thời gian dùng thử đã hết.", "Key hết hạn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return 0;
                    }

                    SaveKeyVaoFile(base64Key);
                    return 2;
                }

                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi xác minh key: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }

        public static bool IsKeyStillValid()
        {
            string base64Key = LoadKeyTuFile();
            try
            {
                if (string.IsNullOrWhiteSpace(base64Key)) return false;

                var licenseKey = JsonSerializer.Deserialize<LicenseKey>(Encoding.UTF8.GetString(Convert.FromBase64String(base64Key)));
                if (licenseKey?.Data == null) return false;
                if (!IsValidHWID(licenseKey.Data)) return false;

                var data = licenseKey.Data;

                if (data.LoaiKey == "Lifetime") return true;

                if (data.LoaiKey == "Trial" && data.ExpiredAtUnix != null)
                {
                    long nowOnline = GetAccurateOnlineTime();
                    if (nowOnline == -1) return false;

                    long remaining = data.ExpiredAtUnix.Value - nowOnline;
                    return remaining > 0;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        private static long GetAccurateOnlineTime()
        {
            if (CachedNetworkTime != null) return CachedNetworkTime.Value;

            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0");
                var response = client.Send(new HttpRequestMessage(HttpMethod.Head, "https://www.google.com"));

                if (response.Headers.Date.HasValue)
                {
                    DateTimeOffset utcDate = response.Headers.Date.Value;
                    CachedNetworkTime = utcDate.ToUnixTimeSeconds();
                    return CachedNetworkTime.Value;
                }
            }
            catch { }

            return -1;
        }

        private static bool IsValidHWID(LicenseData data)
        {
            int match = 0;
            StringBuilder debugInfo = new StringBuilder();

            string hwid1 = GetHWID_1();
            string hwid2 = GetHWID_2();
            string hwid3 = GetHWID_3();
            string hwid4 = GetHWID_4();

            if (data.HWID_1 == hwid1) match++; else debugInfo.AppendLine($"HWID_1 không khớp: {hwid1}");
            if (data.HWID_2 == hwid2) match++; else debugInfo.AppendLine($"HWID_2 không khớp: {hwid2}");
            if (data.HWID_3 == hwid3) match++; else debugInfo.AppendLine($"HWID_3 không khớp: {hwid3}");
            if (data.HWID_4 == hwid4) match++; else debugInfo.AppendLine($"HWID_4 không khớp: {hwid4}");

#if DEBUG
            if (match < 2)
                MessageBox.Show($"Match HWID: {match}/4\n{debugInfo}", "DEBUG: HWID KHÔNG HỢP LỆ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
#endif

            return match >= 2;
        }

        public static string GetHWID_1()
        {
            try
            {
                using var searcher = new ManagementObjectSearcher("SELECT SerialNumber FROM Win32_BIOS");
                foreach (var wmi in searcher.Get())
                    return wmi["SerialNumber"]?.ToString()?.Trim() ?? "NULL";
            }
            catch { return "NULL"; }
            return "NULL";
        }

        public static string GetHWID_2()
        {
            try
            {
                using var searcher = new ManagementObjectSearcher("SELECT SerialNumber FROM Win32_BaseBoard");
                foreach (var wmi in searcher.Get())
                    return wmi["SerialNumber"]?.ToString()?.Trim() ?? "NULL";
            }
            catch { return "NULL"; }
            return "NULL";
        }

        public static string GetHWID_3()
        {
            try
            {
                using var searcher = new ManagementObjectSearcher("SELECT ProcessorId FROM Win32_Processor");
                foreach (var wmi in searcher.Get())
                    return wmi["ProcessorId"]?.ToString()?.Trim() ?? "NULL";
            }
            catch { return "NULL"; }
            return "NULL";
        }

        public static string GetHWID_4()
        {
            try
            {
                using var searcher = new ManagementObjectSearcher("SELECT VolumeSerialNumber FROM Win32_LogicalDisk WHERE DeviceID='C:'");
                foreach (var wmi in searcher.Get())
                    return wmi["VolumeSerialNumber"]?.ToString()?.Trim() ?? "NULL";
            }
            catch { return "NULL"; }
            return "NULL";
        }
    }
}
