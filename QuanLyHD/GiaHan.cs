using Microsoft.Data.Sqlite;
using QuanLyVayVon.CSDL;
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
    public partial class GiaHan : Form
    {
        private string? MaHD;
        private int? thoiGian1Ky = null;
        private string? TenKH = null;
        private string donViKy = string.Empty;
        public GiaHan(string MaHD)
        {
            if (MaHD == null)
            {
                CustomMessageBox.ShowCustomMessageBox("Mã hợp đồng không hợp lệ. VUi lòng thử lại.", null, "LỖI MÃ HỢP ĐỒNG");
            }
            this.MaHD = MaHD;
            var hopdong = HopDongForm.GetHopDongByMaHD(MaHD);
            var thoiGian1Ky = hopdong.KyDongLai;

            ShowKyThuVaTinhTrang(MaHD); // Hiển thị các kỳ và tình trạng trước khi gia hạn

            if (hopdong.HinhThucLaiID == 1 || hopdong.HinhThucLaiID == 4)
            {
                donViKy = " (ngày)";
                thoiGian1Ky = hopdong.KyDongLai; // Ngày thì không cần đổi
            }
            else if (hopdong.HinhThucLaiID == 2 || hopdong.HinhThucLaiID == 5)
            {
                donViKy = " (tuần)";
                thoiGian1Ky = hopdong.KyDongLai * 7; // Tuần thì đổi sang ngày
            }
            else if (hopdong.HinhThucLaiID == 3 || hopdong.HinhThucLaiID == 6)
            {
                donViKy = " (tháng)";
                thoiGian1Ky = hopdong.KyDongLai * 30; // Tháng thì đổi sang ngày
            }

            this.TenKH = hopdong?.TenKH;
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

            lb_GiaHanThem.Text = (lb_GiaHanThem.Text ?? "") + donViKy;

            tb_TenKH.Text = this.TenKH;

            // Tạo label với MaHD màu đỏ
            Label label = new Label
            {
                AutoSize = true,
                Location = new Point(20, 20)
            };

            // Sử dụng RichTextBox để hiển thị màu cho MaHD hoặc dùng HTML-like formatting với Label nếu cần
            // Nhưng WinForms Label không hỗ trợ định dạng nhiều màu, nên dùng RichTextBox readonly hoặc custom control
            // Ở đây dùng RichTextBox readonly
            RichTextBox rtbLabel = new RichTextBox
            {
                BorderStyle = BorderStyle.None,
                BackColor = this.BackColor,
                ReadOnly = true,
                Location = new Point(20, 20),
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Width = 400,
                Height = 40,
                ScrollBars = RichTextBoxScrollBars.None
            };
            rtbLabel.Text = $"Gia hạn hợp đồng: {MaHD}";
            int start = rtbLabel.Text.IndexOf(MaHD ?? "");
            if (start >= 0 && MaHD != null)
            {
                rtbLabel.Select(start, MaHD.Length);
                rtbLabel.SelectionColor = Color.Red;
                rtbLabel.Select(0, 0); // Bỏ chọn
            }
            this.Controls.Add(rtbLabel);

            QuanLyHopDong.StyleExitButton(btn_Thoat);
            QuanLyHopDong.StyleButton(btn_GiaHan);


        }

        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_GiaHan_Click(object sender, EventArgs e)
        {
            var lichSu = LichSuDongLai.GetLichSuDongLaiByMaHD(MaHD);


            GiaHanDayNguoc(MaHD, 1); // Ví dụ: thoiGian1Ky = 30 ngày

        }


        public static void ShowKyThuVaTinhTrang(string MaHD)
        {
            string dbPath = Path.Combine(Application.StartupPath, "Database", "data.db");

            using (var connection = new SqliteConnection($"Data Source={dbPath}"))
            {
                connection.Open();

                var cmd = connection.CreateCommand();
                cmd.CommandText = @"
            SELECT KyThu, TinhTrang
            FROM LichSuDongLai
            WHERE MaHD = @MaHD
            ORDER BY KyThu";
                cmd.Parameters.AddWithValue("@MaHD", MaHD);

                var sb = new StringBuilder();
                sb.AppendLine($"Các kỳ của hợp đồng {MaHD}:");

                using (var reader = cmd.ExecuteReader())
                {
                    int count = 0;
                    while (reader.Read())
                    {
                        int kyThu = reader.GetInt32(0);
                        int tinhTrang = reader.GetInt32(1);
                        sb.AppendLine($"- Kỳ {kyThu}: Tình trạng = {tinhTrang}");
                        count++;
                    }

                    if (count == 0)
                        sb.AppendLine("Không có dữ liệu kỳ nào.");
                }

                MessageBox.Show(sb.ToString(), "Thông tin kỳ đóng lãi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        public static void GiaHanDayNguoc(string MaHD, int thoiGian1Ky)
        {
            string dbPath = Path.Combine(Application.StartupPath, "Database", "data.db");

            using (var connection = new SqliteConnection($"Data Source={dbPath}"))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    // Bước 1: Lấy danh sách kỳ DESC (KyThu lớn nhất trước)
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = @"
                SELECT ID, KyThu, NgayBatDauKy, NgayDenHan, TinhTrang, SoTienPhaiDong
                FROM LichSuDongLai
                WHERE MaHD = @MaHD
                ORDER BY KyThu DESC";
                    cmd.Parameters.AddWithValue("@MaHD", MaHD);

                    var kyList = new List<LichSuDongLaiModel>();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            kyList.Add(new LichSuDongLaiModel
                            {
                                ID = reader.GetInt32(0),
                                KyThu = reader.GetInt32(1),
                                NgayBatDauKy = reader.GetString(2),
                                NgayDenHan = reader.GetString(3),
                                TinhTrang = Convert.ToInt32(reader["TinhTrang"]),
                                SoTienPhaiDong = Convert.ToDecimal(reader["SoTienPhaiDong"])
                            });
                        }
                    }

                    if (kyList.Count == 0)
                    {
                        CustomMessageBox.ShowCustomMessageBox("Không tìm thấy kỳ nào để xử lý!", null, "Thông báo");
                        return;
                    }

                    var kyThuLonNhat = kyList.First(); // DESC => lớn nhất
                    var newKyThu = kyThuLonNhat.KyThu + 2;
                    var ngayBDNew = DateTime.Parse(kyThuLonNhat.NgayBatDauKy).AddDays(thoiGian1Ky * 2);
                    var ngayKTNew = DateTime.Parse(kyThuLonNhat.NgayDenHan).AddDays(thoiGian1Ky * 2);

                    // Bước 2: Thêm kỳ mới -2 (cuối)
                    var insertCmd2 = connection.CreateCommand();
                    insertCmd2.CommandText = @"
                INSERT INTO LichSuDongLai (MaHD, KyThu, NgayBatDauKy, NgayDenHan,
                    SoTienPhaiDong, SoTienDaDong, TinhTrang, GhiChu)
                VALUES (@MaHD, @KyThu, @NgayBD, @NgayKT, 9999, 0, -2, 'Tự thêm do gia hạn')";
                    insertCmd2.Parameters.AddWithValue("@MaHD", MaHD);
                    insertCmd2.Parameters.AddWithValue("@KyThu", newKyThu);
                    insertCmd2.Parameters.AddWithValue("@NgayBD", ngayBDNew.ToString("yyyy-MM-dd"));
                    insertCmd2.Parameters.AddWithValue("@NgayKT", ngayKTNew.ToString("yyyy-MM-dd"));
                    insertCmd2.ExecuteNonQuery();

                    // Bước 3: Dời các kỳ và thêm kỳ -1 trước kỳ đầu tiên bị dời
                    for (int i = 0; i < kyList.Count; i++)
                    {
                        var ky = kyList[i];
                        if (ky.TinhTrang == 0 || ky.TinhTrang == -1)
                            break;

                        // Bước 3.1: Thêm bản sao kỳ đầu bị dời → thành kỳ -1
                        if (i == kyList.Count - 1 || kyList[i + 1].TinhTrang == 0 || kyList[i + 1].TinhTrang == -1)
                        {
                            var insertCmd1 = connection.CreateCommand();
                            insertCmd1.CommandText = @"
                        INSERT INTO LichSuDongLai (MaHD, KyThu, NgayBatDauKy, NgayDenHan,
                            SoTienPhaiDong, SoTienDaDong, TinhTrang, GhiChu)
                        VALUES (@MaHD, @KyThu, @NgayBD, @NgayKT, 9999, 0, -1, 'Kỳ bị gia hạn')";
                            insertCmd1.Parameters.AddWithValue("@MaHD", MaHD);
                            insertCmd1.Parameters.AddWithValue("@KyThu", ky.KyThu);
                            insertCmd1.Parameters.AddWithValue("@NgayBD", ky.NgayBatDauKy);
                            insertCmd1.Parameters.AddWithValue("@NgayKT", ky.NgayDenHan);
                            insertCmd1.ExecuteNonQuery();
                        }

                        // Bước 3.2: Dời kỳ
                        var updateCmd = connection.CreateCommand();
                        updateCmd.CommandText = @"
                    UPDATE LichSuDongLai
                    SET KyThu = @NewKyThu,
                        NgayBatDauKy = @NewBD,
                        NgayDenHan = @NewKT,
                        UpdatedAt = CURRENT_TIMESTAMP
                    WHERE ID = @ID";
                        updateCmd.Parameters.AddWithValue("@ID", ky.ID);
                        updateCmd.Parameters.AddWithValue("@NewKyThu", ky.KyThu + 1);
                        updateCmd.Parameters.AddWithValue("@NewBD", DateTime.Parse(ky.NgayBatDauKy).AddDays(thoiGian1Ky).ToString("yyyy-MM-dd"));
                        updateCmd.Parameters.AddWithValue("@NewKT", DateTime.Parse(ky.NgayDenHan).AddDays(thoiGian1Ky).ToString("yyyy-MM-dd"));
                        updateCmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }

                CustomMessageBox.ShowCustomMessageBox("Gia hạn kỳ thành công!", null, "Thành công");
            }
        }





    }
}
