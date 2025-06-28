using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;
using System.Management;

namespace GenKey
{
    public partial class Form1 : Form
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

        public Form1()
        {
            InitializeComponent();
            rtb_Key.ReadOnly = true;
            InitLoaiKeyComboBox();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string privatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PrivateKey", "private_key.xml");
            string publicPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PublicKey", "public_key.xml");

            if (!File.Exists(privatePath) || !File.Exists(publicPath))
            {
                TaoRSAKey();
            }
        }

        private void InitLoaiKeyComboBox()
        {
            cbb_LoaiKey.Items.Clear();
            cbb_LoaiKey.Items.Add("Lifetime");
            cbb_LoaiKey.Items.Add("Trial");
            cbb_LoaiKey.SelectedIndex = 0;
            tb_TimeTrial.Enabled = false;
            cbb_LoaiKey.SelectedIndexChanged += (s, e) => tb_TimeTrial.Enabled = cbb_LoaiKey.SelectedItem.ToString() == "Trial";
        }

        private string GetHWID_1()
        {
            try
            {
                using (var searcher = new ManagementObjectSearcher("SELECT SerialNumber FROM Win32_BIOS"))
                {
                    foreach (var wmi in searcher.Get())
                        return wmi["SerialNumber"]?.ToString()?.Trim() ?? "NULL";
                }
            }
            catch { }

            return "NULL"; // rõ ràng tất cả đường đi đều return
        }

        private string GetHWID_2()
        {
            try
            {
                using (var searcher = new ManagementObjectSearcher("SELECT SerialNumber FROM Win32_BaseBoard"))
                {
                    foreach (var wmi in searcher.Get())
                        return wmi["SerialNumber"]?.ToString()?.Trim() ?? "NULL";
                }
            }
            catch { }

            return "NULL";
        }

        private string GetHWID_3()
        {
            try
            {
                using (var searcher = new ManagementObjectSearcher("SELECT ProcessorId FROM Win32_Processor"))
                {
                    foreach (var wmi in searcher.Get())
                        return wmi["ProcessorId"]?.ToString()?.Trim() ?? "NULL";
                }
            }
            catch { }

            return "NULL";
        }

        private string GetHWID_4()
        {
            try
            {
                using (var searcher = new ManagementObjectSearcher("SELECT VolumeSerialNumber FROM Win32_LogicalDisk WHERE DeviceID='C:'"))
                {
                    foreach (var wmi in searcher.Get())
                        return wmi["VolumeSerialNumber"]?.ToString()?.Trim() ?? "NULL";
                }
            }
            catch { }

            return "NULL";
        }


        private string TaoKey()
        {
            string privateKeyPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PrivateKey", "private_key.xml");
            if (!File.Exists(privateKeyPath))
            {
                MessageBox.Show("Không tìm thấy khóa riêng (private_key.xml).", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            string loaiKey = cbb_LoaiKey.SelectedItem.ToString();
            long createdAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            long? expiredAt = null;
            if (loaiKey == "Trial" && int.TryParse(tb_TimeTrial.Text, out int days))
            {
                expiredAt = createdAt + days * 86400;
            }

            var data = new LicenseData
            {
                LoaiKey = loaiKey,
                CreatedAtUnix = createdAt,
                ExpiredAtUnix = expiredAt,
                HWID_1 = GetHWID_1(),
                HWID_2 = GetHWID_2(),
                HWID_3 = GetHWID_3(),
                HWID_4 = GetHWID_4()
            };

            string jsonData = JsonSerializer.Serialize(data);
            string privateKeyXml = File.ReadAllText(privateKeyPath);

            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(privateKeyXml);
                byte[] dataBytes = Encoding.UTF8.GetBytes(jsonData);
                byte[] signBytes = rsa.SignData(dataBytes, CryptoConfig.MapNameToOID("SHA256"));

                var licenseKey = new LicenseKey
                {
                    Data = data,
                    Sign = Convert.ToBase64String(signBytes)
                };

                string jsonKey = JsonSerializer.Serialize(licenseKey);
                return Convert.ToBase64String(Encoding.UTF8.GetBytes(jsonKey));
            }
        }

        private void btn_Genkey_Click(object sender, EventArgs e)
        {
            string key = TaoKey();
            if (key == null) return;

            rtb_Key.Text = key;
            Clipboard.SetText(key);

            string keyDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Key");
            Directory.CreateDirectory(keyDir);
            string fileName = $"key_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
            string filePath = Path.Combine(keyDir, fileName);
            File.WriteAllText(filePath, key);

            MessageBox.Show("Đã tạo, lưu và sao chép key thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btn_Copy_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(rtb_Key.Text))
            {
                Clipboard.SetText(rtb_Key.Text);
                MessageBox.Show("Đã copy key vào clipboard.", "Thông báo");
            }
        }

        private void TaoRSAKey()
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string privateDir = Path.Combine(basePath, "PrivateKey");
            string publicDir = Path.Combine(basePath, "PublicKey");

            Directory.CreateDirectory(privateDir);
            Directory.CreateDirectory(publicDir);

            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                string privateKey = rsa.ToXmlString(true);
                string publicKey = rsa.ToXmlString(false);

                File.WriteAllText(Path.Combine(privateDir, "private_key.xml"), privateKey);
                File.WriteAllText(Path.Combine(publicDir, "public_key.xml"), publicKey);

                MessageBox.Show("Đã tạo xong cặp khóa RSA!", "Thông báo");
            }
        }

        private void rtb_Key_TextChanged(object sender, EventArgs e) { }
        private void cbb_LoaiKey_SelectedIndexChanged(object sender, EventArgs e) { }
    }
}
