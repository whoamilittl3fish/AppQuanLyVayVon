using Microsoft.Data.Sqlite;
using QuanLyVayVon.CSDL;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure; // Add this namespace for LicenseType
using System.Data;
using System.Text.RegularExpressions;
using static QuanLyVayVon.QuanLyHD.QuanLyHopDong;

namespace QuanLyVayVon.QuanLyHD
{
    public partial class PrintHD : Form
    {
        private string MaHD;
        private TiemCamDoModel tiemCamDoModel;
        private HopDongModel hopdong;
        public PrintHD(string MaHD)
        {
            if (Function_Reuse.KiemTraDatabaseTonTai() == false)
            {
                CustomMessageBox.ShowCustomMessageBox("Không tìm thấy cơ sở dữ liệu. Vui lòng kiểm tra lại.", null, "LỖI CƠ SỞ DỮ LIỆU");
                return;
            }

            QuestPDF.Settings.License = LicenseType.Community; // Or LicenseType.Commercial if applicable

            InitializeComponent();
            this.MaHD = MaHD;

            this.hopdong = HopDongForm.GetHopDongByMaHD(MaHD);
            hopdong.CCCD = CleanPhoneNumber(hopdong.CCCD);
            hopdong.SDT = CleanPhoneNumber(hopdong.SDT);
            MessageBox.Show(hopdong.TenKH);
            if (this.hopdong == null)
            {
                CustomMessageBox.ShowCustomMessageBox("Không tìm thấy hợp đồng với mã đã nhập.", null, "KHÔNG TÌM THẤY");
                return;
            }
            CustomizeUI();
        }
        private void CustomizeUI()
        {
            this.FormBorderStyle = FormBorderStyle.None; // Ẩn nút tắt/ẩn/phóng to mặc định
            this.MaximizeBox = false;
            this.CenterToScreen(); // Đặt
            this.BackColor = ColorTranslator.FromHtml("#F2F2F7"); // dịu và đúng tone hơn

            // Bo tròn form (bo nhiều hơn)
            int borderRadius = 32;
            this.Region = System.Drawing.Region.FromHrgn(
                NativeMethods.CreateRoundRectRgn(0, 0, this.Width, this.Height, borderRadius, borderRadius)
            );
            this.Text = "In hợp đồng"; // Đặt tiêu đề form
            tb_TenNguoiDaiDien.ReadOnly = true; // Chỉ đọc
            tb_TenKH.ReadOnly = true; // Chỉ đọc
            QuanLyHopDong.StyleButton(btn_In);
            QuanLyHopDong.StyleExitButton(btn_Thoat);
            QuanLyHopDong.StyleTextBox(tb_ID);
            QuanLyHopDong.StyleTextBox(tb_TenNguoiDaiDien);
            QuanLyHopDong.StyleTextBox(tb_TenKH);
            tb_TenKH.Text = hopdong?.TenKH ?? string.Empty;
        }
        private void btn_In_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tb_ID.Text))
            {
                CustomMessageBox.ShowCustomMessageBox("Vui lòng nhập ID hợp đồng để in.", null, "THIẾU THÔNG TIN");
                return;
            }
            if (string.IsNullOrEmpty(tb_TenNguoiDaiDien.Text))
            {
                CustomMessageBox.ShowCustomMessageBox("Vui lòng tìm kiếm ID hợp đồng trước khi in.", null, "THIẾU THÔNG TIN");
                return;
            }
            if (string.IsNullOrEmpty(tb_TenKH.Text))
            {
                CustomMessageBox.ShowCustomMessageBox("Không tìm thấy hợp đồng để in. Vui lòng thử lại.", null, "THIẾU THÔNG TIN");
            }

            try
            {


                string folderPath = Path.Combine(Application.StartupPath, "PrintContracts");
                Directory.CreateDirectory(folderPath);

                string timestamp = DateTime.Now.ToString("yyyy-MM-dd");
                string fileName = $"HopDong-{hopdong.MaHD}_HopDong-{timestamp}.pdf";
                string filePath = Path.Combine(folderPath, fileName);


                var document = new HopDongPdfDocument(hopdong, tiemCamDoModel);
                document.GeneratePdf(filePath);

                MessageBox.Show("Đã lưu hợp đồng tại:\n" + filePath, "Xuất PDF thành công");

                System.Diagnostics.Process.Start("explorer.exe", filePath);
            }
            catch (Exception ex)
            {
                CustomMessageBox.ShowCustomMessageBox("Đã xảy ra lỗi khi xuất hợp đồng: " + ex.Message, null, "LỖI XUẤT PDF");
            }

            this.Close(); // Đóng form sau khi in

        }
        private void btn_Search_Click(object sender, EventArgs e)
        {
            if (Function_Reuse.KiemTraDatabaseTonTai() == false)
            {
                CustomMessageBox.ShowCustomMessageBox("Không tìm thấy cơ sở dữ liệu. Vui lòng kiểm tra lại.", null, "LỖI CƠ SỞ DỮ LIỆU");
                return;
            }
            if (string.IsNullOrEmpty(tb_ID.Text))
            {
                CustomMessageBox.ShowCustomMessageBox("Vui lòng nhập ID hợp đồng để tìm kiếm.", null, "THIẾU THÔNG TIN");
                return;
            }
            int ID = int.TryParse(tb_ID.Text, out int parsedId) ? parsedId : -1;
            this.tiemCamDoModel = GetTiemCamDoInfo(ID);
            tb_TenNguoiDaiDien.Text = tiemCamDoModel?.DaiDien ?? string.Empty;
            if (tiemCamDoModel == null)
            {
                CustomMessageBox.ShowCustomMessageBox("Không tìm thấy thông tin tiệm cầm đồ với ID đã nhập.", null, "KHÔNG TÌM THẤY");
                return;
            }

        }


        public static string CleanPhoneNumber(string? rawHotline)
        {
            if (string.IsNullOrWhiteSpace(rawHotline))
                return string.Empty;

            return Regex.Replace(rawHotline, @"\D", ""); // Loại bỏ mọi ký tự không phải số
        }
        private TiemCamDoModel? GetTiemCamDoInfo(int ID)
        {
            TiemCamDoModel? model = null;
            string dbPath = Path.Combine(Application.StartupPath, "DataBase", "data.db");

            if (Function_Reuse.KiemTraDatabaseTonTai() == false)
            {
                CustomMessageBox.ShowCustomMessageBox("Không tìm thấy cơ sở dữ liệu. Vui lòng kiểm tra lại.", null, "LỖI CƠ SỞ DỮ LIỆU");
                return null;
            }

            using (var connection = new SqliteConnection($"Data Source={dbPath}")) // Corrected to use Microsoft.Data.Sqlite.SqliteConnection
            {
                connection.Open();
                string query = "SELECT Id, TenTiem, DiaChi, Hotline, DaiDien, SDTDaiDien, TaiKhoan, TenNganHang, TruongPGDTT, UpdatedAt FROM TiemCamDo LIMIT 1";
                using (var cmd = connection.CreateCommand()) // Corrected to use connection.CreateCommand()
                {
                    cmd.CommandText = query;
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            model = new TiemCamDoModel
                            {
                                Id = reader.GetInt32(0),
                                TenTiem = reader.IsDBNull(1) ? null : reader.GetString(1),
                                DiaChi = reader.IsDBNull(2) ? null : reader.GetString(2),
                                Hotline = CleanPhoneNumber(reader.IsDBNull(3) ? null : reader.GetString(3)), // Làm sạch số điện thoại
                                DaiDien = reader.IsDBNull(4) ? null : reader.GetString(4),
                                SDTDaiDien = CleanPhoneNumber(reader.IsDBNull(5) ? null : reader.GetString(5)), // Làm sạch số điện thoại đại diện
                                TaiKhoan = CleanPhoneNumber(reader.IsDBNull(6) ? null : reader.GetString(6)), // Làm sạch số tài khoản
                                TenNganHang = reader.IsDBNull(7) ? null : reader.GetString(7),
                                TruongPGDTT = reader.IsDBNull(8) ? null : reader.GetString(8),
                                UpdatedAt = reader.IsDBNull(9) ? null : reader.GetString(9)
                            };
                        }
                    }
                }
            }
            return model;
        }

        private void tb_ID_TextChanged(object sender, EventArgs e)
        {
            // Chỉ cho phép nhập số
            var tb = sender as TextBox;
            if (tb == null) return;

            if (tb_ID.Text.Length > 1)
            {
                CustomMessageBox.ShowCustomMessageBox("Chỉ lưu tối đa 9 ô thông tin tiệm", null, "LỖI ID");
                tb_ID.Text = tb_ID.Text.Substring(0, 1); // Giới hạn độ dài tối đa là 2 ký tự
            }

            string digitsOnly = new string(tb.Text.Where(char.IsDigit).ToArray());
            if (tb.Text != digitsOnly)
            {
                int selStart = tb.SelectionStart;
                tb.Text = digitsOnly;
                tb.SelectionStart = Math.Min(selStart, tb.Text.Length);
            }
        }

        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            this.Close(); // Đóng form hiện tại
        }
    }
}
