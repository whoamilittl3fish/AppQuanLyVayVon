using System;
using System.IO;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;
using System.Net.Http;
using System.Threading.Tasks;

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
            catch (Exception ex) { MessageBox.Show($"Lỗi khi lưu key: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        public static string LoadKeyTuFile()
        {
            try { return File.Exists(keyFilePath) ? File.ReadAllText(keyFilePath).Trim() : null; }
            catch (Exception ex) { MessageBox.Show($"Lỗi khi tải key: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); return null; }
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
                var sha256 = System.Security.Cryptography.SHA256.Create();
                if (!rsa.VerifyData(dataBytes, sha256, signBytes)) return 0;

                var data = key.Data;
                if (!IsValidHWID(data)) return 0;

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
                    if (remaining <= 0) return 0;

                    SaveKeyVaoFile(base64Key);
                    return 2;
                }

                return 0;
            }
            catch
            {
                return 0;
            }
        }

        private static long GetAccurateOnlineTime()
        {
            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0");
                var response = client.Send(new HttpRequestMessage(HttpMethod.Head, "https://www.google.com"));

                if (response.Headers.Date.HasValue)
                {
                    DateTimeOffset utcDate = response.Headers.Date.Value;
                    return utcDate.ToUnixTimeSeconds();
                }
            }
            catch { }

            return -1;
        }


        private static bool IsValidHWID(LicenseData data)
        {
            int match = 0;
            if (data.HWID_1 == GetHWID_1()) match++;
            if (data.HWID_2 == GetHWID_2()) match++;
            if (data.HWID_3 == GetHWID_3()) match++;
            if (data.HWID_4 == GetHWID_4()) match++;
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