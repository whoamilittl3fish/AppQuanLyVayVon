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
          

            ShowKyThuVaTinhTrang(MaHD); // Hiển thị các kỳ và tình trạng trước khi gia hạn

            if (hopdong.HinhThucLaiID == 1 || hopdong.HinhThucLaiID == 4)
            {
                donViKy = " (ngày)";
                this.thoiGian1Ky = hopdong.KyDongLai; // Ngày thì không cần đổi
            }
            else if (hopdong.HinhThucLaiID == 2 || hopdong.HinhThucLaiID == 5)
            {
                donViKy = " (tuần)";
                this.thoiGian1Ky = hopdong.KyDongLai * 7; // Tuần thì đổi sang ngày
            }
            else if (hopdong.HinhThucLaiID == 3 || hopdong.HinhThucLaiID == 6)
            {
                donViKy = " (tháng)";
                this.thoiGian1Ky = hopdong.KyDongLai * 30; // Tháng thì đổi sang ngày
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

          
            tb_TenKH.ReadOnly = true; // Chỉ đọc
            tb_TenKH.Text = this.TenKH;
            QuanLyHopDong.StyleTextBox(tb_TenKH);
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
            GiaHanDayNguoc(MaHD, this.thoiGian1Ky); // Gia hạn

            string thoiGian = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string noiDungThem = rtb_GhiChu.Text.Trim();

            // ✅ Tạo note định dạng đúng để truyền vào hàm GhiLichSuHopDong (giữ nguyên hàm cũ)
            string noteLichSu = $"GIA HẠN\nThời gian: {thoiGian}";
            if (!string.IsNullOrWhiteSpace(noiDungThem))
                noteLichSu += $"\n{noiDungThem}";

            LichSuDongLai.GhiLichSuHopDong(MaHD, noteLichSu); // Gọi lại hàm cũ, không sửa hàm

            // ✅ Nếu có ghi chú thì mới lưu vào GhiChu
            if (!string.IsNullOrWhiteSpace(noiDungThem))
            {
                string ghiChuMoi = "\n__________________________________________________________________________________________";
                ghiChuMoi += "\nGhi chú gia hạn hợp đồng:";
                ghiChuMoi += $"\nThời gian: {thoiGian}";
                ghiChuMoi += "\n" + noiDungThem;

                string dbPath = Path.Combine(Application.StartupPath, "DataBase", "data.db");
                using (var connection = new Microsoft.Data.Sqlite.SqliteConnection($"Data Source={dbPath}"))
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = @"
                    UPDATE HopDongVay
                    SET GhiChu = COALESCE(GhiChu, '') || @GhiChuMoi,
                        UpdatedAt = CURRENT_TIMESTAMP
                    WHERE MaHD = @MaHD;
                ";
                        command.Parameters.AddWithValue("@GhiChuMoi", ghiChuMoi);
                        command.Parameters.AddWithValue("@MaHD", MaHD);
                        command.ExecuteNonQuery();
                    }
                }
            }

            this.DialogResult = DialogResult.OK;
        }




        public static void ShowKyThuVaTinhTrang(string MaHD)
        {
            string dbPath = Path.Combine(Application.StartupPath, "DataBase", "data.db");

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

              
            }
        }

        public static decimal LayLaiMoiNgayTuHopDong(string MaHD)
        {
            string dbPath = Path.Combine(Application.StartupPath, "DataBase", "data.db");
            using (var connection = new SqliteConnection($"Data Source={dbPath}"))
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())

                {
                    cmd.CommandText = "SELECT LaiMoiNgay FROM HopDongVay WHERE MaHD = @MaHD";
                    cmd.Parameters.AddWithValue("@MaHD", MaHD);
                    var result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToDecimal(result) : 0;
                }
            }
        }



        public static void GiaHanDayNguoc(string MaHD, int? thoiGian1Ky)
        {
            string dbPath = Path.Combine(Application.StartupPath, "DataBase", "data.db");

            using (var connection = new SqliteConnection($"Data Source={dbPath}"))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    // Bước 1: Lấy danh sách kỳ cần đẩy (DESC)
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
                            var tt = Convert.ToInt32(reader["TinhTrang"]);
                            if (tt != 0 && tt != -3)
                            {
                                kyList.Add(new LichSuDongLaiModel
                                {
                                    ID = reader.GetInt32(0),
                                    KyThu = reader.GetInt32(1),
                                    NgayBatDauKy = reader.GetString(2),
                                    NgayDenHan = reader.GetString(3),
                                    TinhTrang = tt,
                                    SoTienPhaiDong = Convert.ToDecimal(reader["SoTienPhaiDong"])
                                });
                            }
                        }
                    }

                    if (kyList.Count == 0)
                    {
                        CustomMessageBox.ShowCustomMessageBox("Không tìm thấy kỳ nào để xử lý!", null, "Thông báo");
                        return;
                    }

                    // Bước 2: Dời các kỳ lên 1 kỳ (cộng thời gian 1 kỳ)
                    foreach (var ky in kyList)
                    {
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
                        updateCmd.Parameters.AddWithValue("@NewBD", DateTime.Parse(ky.NgayBatDauKy!).AddDays(thoiGian1Ky ?? 0).ToString("yyyy-MM-dd"));
                        updateCmd.Parameters.AddWithValue("@NewKT", DateTime.Parse(ky.NgayDenHan!).AddDays(thoiGian1Ky ?? 0).ToString("yyyy-MM-dd"));
                        updateCmd.ExecuteNonQuery();
                    }

                    // Bước 3: Chèn kỳ -3 thay thế kỳ đầu tiên bị đẩy
                    var dauKy = kyList.Last(); // kỳ nhỏ nhất trong danh sách
                    var insertTruKy = connection.CreateCommand();
                    insertTruKy.CommandText = @"
                INSERT INTO LichSuDongLai (MaHD, KyThu, NgayBatDauKy, NgayDenHan,
                    SoTienPhaiDong, SoTienDaDong, TinhTrang, GhiChu)
                VALUES (@MaHD, @KyThu, @NgayBD, @NgayKT, 0, 0, -3, 'Kỳ đánh dấu gia hạn')";
                    insertTruKy.Parameters.AddWithValue("@MaHD", MaHD);
                    insertTruKy.Parameters.AddWithValue("@KyThu", dauKy.KyThu);
                    insertTruKy.Parameters.AddWithValue("@NgayBD", dauKy.NgayBatDauKy);
                    insertTruKy.Parameters.AddWithValue("@NgayKT", dauKy.NgayDenHan);
                    insertTruKy.ExecuteNonQuery();

                    // Bước 4: Tìm kỳ cuối cùng sau khi đã đẩy để thêm kỳ 6
                    var cmdKyCuoi = connection.CreateCommand();
                    cmdKyCuoi.CommandText = @"
                SELECT NgayBatDauKy, NgayDenHan, KyThu
                FROM LichSuDongLai
                WHERE MaHD = @MaHD
                ORDER BY KyThu DESC
                LIMIT 1";
                    cmdKyCuoi.Parameters.AddWithValue("@MaHD", MaHD);
                    string? ngayBD_cuoi = null;
                    string? ngayKT_cuoi = null;
                    int kyThuCuoi = 0;
                    using (var reader = cmdKyCuoi.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            ngayBD_cuoi = reader.GetString(0);
                            ngayKT_cuoi = reader.GetString(1);
                            kyThuCuoi = reader.GetInt32(2);
                        }
                    }

                    // Fix for CS1503: Convert 'int?' to 'double' by using the null-coalescing operator to provide a default value.
                    DateTime bd_Ky6 = DateTime.Parse(ngayBD_cuoi!).AddDays(thoiGian1Ky ?? 0);
                    DateTime kt_Ky6 = DateTime.Parse(ngayKT_cuoi!).AddDays(thoiGian1Ky ?? 0);

                    decimal laiMoiNgay = LayLaiMoiNgayTuHopDong(MaHD);
                    decimal tienPhaiDong_Ky6 = laiMoiNgay * (thoiGian1Ky ?? 0);

                    var insertKy6 = connection.CreateCommand();
                    insertKy6.CommandText = @"
                INSERT INTO LichSuDongLai (MaHD, KyThu, NgayBatDauKy, NgayDenHan,
                    SoTienPhaiDong, SoTienDaDong, TinhTrang, GhiChu)
                VALUES (@MaHD, @KyThu, @NgayBD, @NgayKT, @TienPhaiDong, 0, 6, 'Kỳ thêm do gia hạn')";
                    insertKy6.Parameters.AddWithValue("@MaHD", MaHD);
                    insertKy6.Parameters.AddWithValue("@KyThu", kyThuCuoi + 1);
                    insertKy6.Parameters.AddWithValue("@NgayBD", bd_Ky6.ToString("yyyy-MM-dd"));
                    insertKy6.Parameters.AddWithValue("@NgayKT", kt_Ky6.ToString("yyyy-MM-dd"));
                    insertKy6.Parameters.AddWithValue("@TienPhaiDong", tienPhaiDong_Ky6);
                    insertKy6.ExecuteNonQuery();

                    transaction.Commit();
                }

                CustomMessageBox.ShowCustomMessageBox("Gia hạn kỳ thành công!", null, "Thành công");
            }
        }




    }
}
