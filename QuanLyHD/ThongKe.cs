using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static QuanLyVayVon.QuanLyHD.QuanLyHopDong;


namespace QuanLyVayVon.QuanLyHD
{
    public partial class ThongKe : Form
    {
        public ThongKe()
        {
            InitializeComponent();
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
            QuanLyHopDong.StyleExitButton(btn_Thoat);

            // Tiêu đề
            var titleLabel = new Label
            {
                Text = "THỐNG KÊ DÒNG TIỀN VÀ HỢP ĐỒNG",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.FromArgb(44, 62, 80),
                AutoSize = true,
                Location = new Point(30, 10)
            };
            this.Controls.Add(titleLabel);

            tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single; // Đặt viền cho TableLayoutPanel

            // Chỉnh lại font chữ to, rõ, đậm, đẹp cho các RichTextBox thống kê
            Font thongKeFont = new Font("Segoe UI", 18F, FontStyle.Bold);
         
            lb_GioiThieuTongTienDangChoVay.Font = new Font("Segoe UI", 15, FontStyle.Regular); // Correctly setting the font
            lb_GioiThieuTongTienDangChoVay.ForeColor = Color.Black; // Correctly setting the text color
            lb_GioiThieuTongTienDangChoVay.TextAlign = ContentAlignment.MiddleCenter; // Correctly setting the text alignment

            lb_TongTienDangVay.Font = new Font("Segoe UI", 18, FontStyle.Bold); // Font to, rõ
            lb_TongTienDangVay.ForeColor = Color.FromArgb(0, 169, 135); // Xanh ngọc như ảnh
            lb_TongTienDangVay.TextAlign = ContentAlignment.MiddleCenter; // Correctly setting the text alignment

            // Thêm cấu hình cho lb_GioiThieuTongLai giống lb_TongLai nhưng nhỏ hơn, không in đậm
            lb_GioiThieuTongLai.Font = new Font("Segoe UI", 15, FontStyle.Regular); // Nhỏ hơn, không in đậm
            lb_GioiThieuTongLai.ForeColor = Color.Black;
            lb_GioiThieuTongLai.TextAlign = ContentAlignment.MiddleCenter;

            lb_TongLai.Font = new Font("Segoe UI", 18, FontStyle.Bold); // Font to, rõ
            lb_TongLai.ForeColor = Color.FromArgb(0, 169, 135); // Xanh ngọc như ảnh
            lb_TongLai.TextAlign = ContentAlignment.MiddleCenter;

            
            lb_GioiThieuTongHopDong.Font = new Font("Segoe UI", 15, FontStyle.Regular); // Correctly setting the font
            lb_GioiThieuTongHopDong.ForeColor = Color.Black; // Correctly setting the text color
            lb_GioiThieuTongHopDong.TextAlign = ContentAlignment.MiddleCenter; // Correctly setting the text alignment

            lb_TongHopDong.Font = new Font("Segoe UI", 18, FontStyle.Bold); // Font to, rõ
            lb_TongHopDong.ForeColor = Color.FromArgb(0, 169, 135); // Xanh ngọc như ảnh
            lb_TongHopDong.TextAlign = ContentAlignment.MiddleCenter;


        }

        private void ThongKeTien_Load(object sender, EventArgs e)
        {
            lb_TongLai.Text = LayTongLaiThuTrongThang().ToString("N0");
            lb_TongHopDong.Text = LayTongSoHopDong().ToString("N0");
            lb_TongTienDangVay.Text = TinhTongTienDangChoVay().ToString("N0");
        }
        public static decimal TinhTongTienDangChoVay()
        {
            try
            {
                string dbPath = Path.Combine(Application.StartupPath, "DataBase", "data.db");
                using (var connection = new SqliteConnection($"Data Source={dbPath}"))
                {
                    connection.Open();

                    string query = @"
                    SELECT SUM(TienVay + TienVayThem)
                    FROM HopDongVay
                    WHERE TinhTrang NOT IN (-1, -2);
                ";

                    using (var command = new SqliteCommand(query, connection))
                    {
                        object result = command.ExecuteScalar();
                        return result != DBNull.Value ? Convert.ToDecimal(result) : 0m;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tính tổng tiền đang cho vay:\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0m;
            }
        }
        public static decimal LayTongLaiThuTrongThang()
        {
            decimal tongTien = 0;

            string dbPath = Path.Combine(Application.StartupPath, "DataBase", "data.db");
            string connectionString = $"Data Source={dbPath}";

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string query = @"
            SELECT IFNULL(SUM(TienLaiDaDong - TienLaiDaDongTruocDo), 0)
            FROM HopDongVay;
        ";

                using (var command = new SqliteCommand(query, connection))
                {
                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        tongTien = Convert.ToDecimal(result);
                    }
                }

                connection.Close();
            }

            return tongTien;
        }
        public static decimal LayTongSoHopDong()
        {
            decimal tongSoHopDong = 0;
            string dbPath = Path.Combine(Application.StartupPath, "DataBase", "data.db");
            string connectionString = $"Data Source={dbPath}";
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string query = @"
                    SELECT COUNT(*)
                    FROM HopDongVay
                    WHERE TinhTrang NOT IN (0, 1, 2, 3, 4, 5);
                ";

                using (var command = new SqliteCommand(query, connection))
                {
                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        tongSoHopDong = Convert.ToDecimal(result);
                    }
                }
            }
            return tongSoHopDong;
        }

        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
