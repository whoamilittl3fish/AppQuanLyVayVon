using Microsoft.Data.Sqlite;
using System.Drawing.Drawing2D; // Add this namespace to resolve 'GraphicsPath'
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static QuanLyVayVon.QuanLyHD.QuanLyHopDong;

namespace QuanLyVayVon.QuanLyHD
{
    public partial class ChuocDoForm : Form
    {
        int? hinhthucchuoc = null;
        private string? MaHD = null;
        private string? note = null;
        /// <s
        public ChuocDoForm(string? MaHD, int? hinhthucchuoc = -1)
        {
            this.MaHD = MaHD;
            if (MaHD == null)
            {
                CustomMessageBox.ShowCustomMessageBox("Không tìm thấy MaHD.", null, "LỖI MÃ HỢP ĐỒNG");
            }
            InitializeComponent();
            CustomizeUI(); // Gọi hàm tùy chỉnh giao diện
            this.MouseDown += Form1_MouseDown; // Cho phép kéo form
            this.hinhthucchuoc = hinhthucchuoc;
        }


        private void Btn_Luu_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;

            decimal tienVay = decimal.TryParse(Function_Reuse.ExtractNumberString(tb_TienVay.Text), NumberStyles.Number, CultureInfo.InvariantCulture, out var value) ? value : 0;
            decimal tienKhac = decimal.TryParse(Function_Reuse.ExtractNumberString(tb_TienKhac.Text), NumberStyles.Number, CultureInfo.InvariantCulture, out var value2) ? value2 : 0;
            DateTime ngayKetThuc = dtp_NgayChuocDo.Value.Date;

            var hopDong = HopDongForm.GetHopDongByMaHD(MaHD);
            if (hopDong == null)
            {
                CustomMessageBox.ShowCustomMessageBox("Không tìm thấy hợp đồng.", null, "LỖI");
                return;
            }

            decimal tongNoConLai = QuanLyHopDong.CapNhatLaiDenHomNay(MaHD);
            decimal tongTienChuoc = tienVay + tienKhac + tongNoConLai;
            var lichSuDongLai = LichSuDongLai.GetLichSuDongLaiByMaHD(MaHD);
            decimal tongTienDaDong = lichSuDongLai.Sum(x => x.SoTienDaDong);

            string note = $"CHUỘC HỢP ĐỒNG {MaHD}." +
                          $"\nHÌNH THỨC CHUỘC: {(this.hinhthucchuoc == -1 ? "CHUỘC SỚM TRƯỚC HẠN" : "CHUỘC SAU KHI ĐÓNG HẾT")}" +
                          $"\nTỔNG TIỀN CHUỘC: {Function_Reuse.FormatNumberWithThousandsSeparator(tongTienChuoc)} VNĐ." +
                          $"\nNgày chuộc: {ngayKetThuc:dd/MM/yyyy}." +
                          $"\n(Tiền vay): {Function_Reuse.FormatNumberWithThousandsSeparator(tienVay)} VNĐ." +
                          $"\n(Tiền nợ còn lại): {Function_Reuse.FormatNumberWithThousandsSeparator(tongNoConLai)} VNĐ." +
                          $"\n(Tiền khác): {tienKhac:N0} VNĐ." +
                          $"\nTổng kỳ: {lichSuDongLai.Count} kỳ." +
                          $"\nLãi đã đóng: {Function_Reuse.FormatNumberWithThousandsSeparator(hopDong.TienLaiDaDong)} VNĐ.";

            // Ghi chi tiết các kỳ đã đóng
            foreach (var dong in lichSuDongLai.Where(x => x.TinhTrang == 0 || x.SoTienDaDong > 0).OrderBy(x => x.KyThu))
            {
                note += $"\n- Kỳ {dong.KyThu}: Đóng {dong.SoTienDaDong:N0}đ vào {Function_Reuse.FormatDate(dong.NgayDongThucTe)}.";
            }

            this.note = note;

            var frm_XuatText = new TextToScreen(note, "Thông tin chuộc đồ của hợp đồng: ", this.MaHD, true);
            if (frm_XuatText.ShowDialog() != DialogResult.OK)
            {
                CustomMessageBox.ShowCustomMessageBox("Bạn đã huỷ chuộc đồ hợp đồng này.", null, "HUỶ CHUỘC ĐỒ");
                return;
            }

            try
            {
                string dbPath = Path.Combine(Application.StartupPath, "DataBase", "data.db");
                using (var connection = new SqliteConnection($"Data Source={dbPath}"))
                {
                    connection.Open();

                    // Cập nhật hợp đồng
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = @"
                    UPDATE HopDongVay
                    SET 
                        TinhTrang = CASE WHEN TinhTrang = 0 THEN -1 ELSE -2 END,
                        KetThuc = 1,
                        NgayKetThuc = @NgayKetThuc,
                        UpdatedAt = CURRENT_TIMESTAMP,
                        TienNoConLai = @TienNoConLai,
                        TienDaDong = @TienDaDong,
                        TongTienChuocDo = @TongTienChuocDo,
                        TienKhac = @TienKhac
                    WHERE MaHD = @MaHD;";
                        command.Parameters.AddWithValue("@MaHD", MaHD);
                        command.Parameters.AddWithValue("@NgayKetThuc", ngayKetThuc);
                        command.Parameters.AddWithValue("@TienNoConLai", tongNoConLai);
                        command.Parameters.AddWithValue("@TienDaDong", tongTienDaDong);
                        command.Parameters.AddWithValue("@TongTienChuocDo", tongTienChuoc);
                        command.Parameters.AddWithValue("@TienKhac", tienKhac);
                        command.ExecuteNonQuery();
                    }

                    // Ghi vào lịch sử cập nhật
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = @"
                    INSERT INTO LichSuCapNhatHopDong (MaHD, ThoiGian, HanhDong, GhiChu)
                    VALUES (@MaHD, CURRENT_TIMESTAMP, 'Chuộc đồ/Kết thúc hợp đồng', @Note);";
                        command.Parameters.AddWithValue("@MaHD", MaHD);
                        command.Parameters.AddWithValue("@Note", note);
                        command.ExecuteNonQuery();
                    }
                }

                CustomMessageBox.ShowCustomMessageBox("Đã kết thúc hợp đồng thành công.", null, "THÀNH CÔNG");
            }
            catch (Exception ex)
            {
                CustomMessageBox.ShowCustomMessageBox("Lỗi khi kết thúc hợp đồng: " + ex.Message, null, "LỖI");
            }
        }



       

        private void btn_QuayLai_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel; // Set the dialog result to Cancel
            this.Close(); // Close the form
        }

        // giao diện

        private void CustomizeUI()
        {
          
            this.AutoScaleMode = AutoScaleMode.Font;

            this.FormBorderStyle = FormBorderStyle.None; // Ẩn nút tắt/ẩn/phóng to mặc định
            this.MaximizeBox = false;
            this.CenterToScreen(); // Đặt
            this.BackColor = Color.FromArgb(240, 240, 240);
            // Bo tròn form (bo nhiều hơn)
            int borderRadius = 32;
            this.Region = System.Drawing.Region.FromHrgn(
                NativeMethods.CreateRoundRectRgn(0, 0, this.Width, this.Height, borderRadius, borderRadius)
            );
            // Font đẹp hơn cho toàn bộ form (không in nghiêng)
            System.Drawing.Font mainFont = new System.Drawing.Font("Montserrat", 12.5F, FontStyle.Regular, GraphicsUnit.Point);
            System.Drawing.Font mainFontBold = new System.Drawing.Font("Montserrat", 13.5F, FontStyle.Bold, GraphicsUnit.Point);
            System.Drawing.Font donViFont = new System.Drawing.Font("Montserrat", 11F, FontStyle.Regular, GraphicsUnit.Point);
            System.Drawing.Font dateTimeFont = new System.Drawing.Font("Montserrat", 12.5F, FontStyle.Regular, GraphicsUnit.Point);



            // Center the title label horizontally at the top



            void StyleTextBox(TextBox tb)
            {
                tb.Font = mainFont;
                tb.ForeColor = Color.Black;
                tb.TextAlign = HorizontalAlignment.Center;
                tb.Multiline = false; // Để tự động scale chiều cao theo font
                tb.AutoSize = false;

                // Tính chiều cao phù hợp dựa trên font
                using (var g = tb.CreateGraphics())
                {
                    SizeF textSize = g.MeasureString("Ag", tb.Font);
                    int newHeight = (int)Math.Ceiling(textSize.Height) + 6; // +6 để cao hơn chữ một chút
                    tb.Height = newHeight;
                }

                tb.Padding = new Padding(0, 0, 0, 0);
                tb.Region = System.Drawing.Region.FromHrgn(
                    NativeMethods.CreateRoundRectRgn(0, 0, tb.Width, tb.Height, 20, 20)
                );
            }

            void StyleDateTimePicker(DateTimePicker dtp)
            {
                dtp.Font = dateTimeFont;
                dtp.CalendarFont = dateTimeFont;
                dtp.CalendarForeColor = Color.Black;
                dtp.CalendarMonthBackground = Color.White;
                dtp.CalendarTitleBackColor = Color.FromArgb(235, 245, 255);
                dtp.CalendarTitleForeColor = Color.FromArgb(41, 128, 185);
                dtp.Region = System.Drawing.Region.FromHrgn(
                    NativeMethods.CreateRoundRectRgn(0, 0, dtp.Width, dtp.Height, 20, 20) // Bo nhiều hơn
                );

            }


            void HienThiTieuDe(string tieuDeTruocMaHD, string maHD, int y = 15, int maxWidth = 600)
            {
                RichTextBox richTieuDe = rtb_TieuDe;
                if (richTieuDe != null && this.Controls.Contains(richTieuDe))
                    this.Controls.Remove(richTieuDe);

                richTieuDe = new RichTextBox
                {
                    BorderStyle = BorderStyle.None,
                    BackColor = this.BackColor,
                    ReadOnly = true,
                    TabStop = false,
                    Font = new Font("Montserrat", 18F, FontStyle.Regular),
                    ForeColor = Color.FromArgb(44, 62, 80),
                    Width = maxWidth,
                    Height = 50,
                    ScrollBars = RichTextBoxScrollBars.None,
                    Multiline = false
                };

                // Soạn nội dung
                richTieuDe.SelectionColor = Color.FromArgb(44, 62, 80);
                richTieuDe.AppendText(tieuDeTruocMaHD);
                richTieuDe.SelectionColor = Color.Red;
                richTieuDe.AppendText(maHD);

                // Chặn chỉnh sửa
                richTieuDe.MouseDown += (s, e) => ((RichTextBox)s).DeselectAll();

                // Thêm vào form
                this.Controls.Add(richTieuDe);

                // Đặt vị trí ban đầu
                CanhGiuaRichTextBox(richTieuDe, y);
            }


            StyleDateTimePicker(dtp_NgayChuocDo);

            QuanLyHopDong.StyleControlButton(btn_QuayLai, "c");
            QuanLyHopDong.StyleButton(Btn_Luu);

            StyleTextBox(tb_Lai);
            StyleTextBox(tb_TienVay);
            StyleTextBox(tb_TienKhac);
            StyleTextBox(tb_TongTienChuoc);

            tb_Lai.BorderStyle = BorderStyle.None; // Bỏ viền
            tb_TienVay.BorderStyle = BorderStyle.None; // Bỏ viền
            tb_TongTienChuoc.BorderStyle = BorderStyle.None; // Bỏ viền
            tb_Lai.ReadOnly = true; // Không cho phép chỉnh sửa
            tb_TienVay.ReadOnly = true; // Không cho phép chỉnh sửa
            tb_TongTienChuoc.ReadOnly = true; // Không cho phép chỉnh sửa

            HienThiTieuDe("Chuộc đồ hợp đồng ", MaHD, 15, 600);


        }

        void CanhGiuaRichTextBox(RichTextBox rtb, int y)
        {
            using (Graphics g = rtb.CreateGraphics())
            {
                var textSize = g.MeasureString(rtb.Text, rtb.Font);
                int x = (this.ClientSize.Width - (int)textSize.Width) / 2;
                rtb.Location = new Point(Math.Max(0, x), y);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            int borderThickness = 2;
            int radius = 32;
            int w = this.Width - borderThickness;
            int h = this.Height - borderThickness;
            using (GraphicsPath path = new GraphicsPath())
            using (Pen pen = new Pen(Color.FromArgb(41, 128, 185), borderThickness))
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                path.AddArc(0, 0, radius, radius, 180, 90);
                path.AddArc(w - radius, 0, radius, radius, 270, 90);
                path.AddArc(w - radius, h - radius, radius, radius, 0, 90);
                path.AddArc(0, h - radius, radius, radius, 90, 90);
                path.CloseFigure();
                e.Graphics.DrawPath(pen, path);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            int radius = 32;
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddArc(0, 0, radius, radius, 180, 90);
                path.AddArc(this.Width - radius, 0, radius, radius, 270, 90);
                path.AddArc(this.Width - radius, this.Height - radius, radius, radius, 0, 90);
                path.AddArc(0, this.Height - radius, radius, radius, 90, 90);
                path.CloseFigure();
                this.Region = new Region(path);
            }
        }

        // Cho phép kéo form
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        // Gắn vào sự kiện MouseDown của Form hoặc một panel tiêu đề (tuỳ bạn)
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }


        private void ChuocDoForm_Load(object sender, EventArgs e)
        {
            string dbPath = Path.Combine(Application.StartupPath, "Database", "data.db");

            if (!File.Exists(dbPath))
            {
                CustomMessageBox.ShowCustomMessageBox("Không tìm thấy cơ sở dữ liệu. Vui lòng kiểm tra lại.", null, "LỖI CƠ SỞ DỮ LIỆU");
                return;
            }
            try
            {
                // Tạo kết nối đến cơ sở dữ liệu SQLite
                using (var connection = new SqliteConnection($"Data Source={dbPath}"))
                {
                    connection.Open();
                    // Truy vấn thông tin hợp đồng
                    string query = "SELECT * FROM HopDongVay WHERE MaHD = @MaHD";
                    using (var command = new SqliteCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("@MaHD", MaHD);
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Hiển thị thông tin hợp đồng

                                decimal dectienVay = Convert.ToDecimal(reader["TienVay"]);
                                decimal declai = QuanLyHopDong.CapNhatLaiDenHomNay(MaHD);
                                tb_TienVay.Text = Function_Reuse.FormatNumberWithThousandsSeparator(dectienVay);

                                tb_Lai.Text = Function_Reuse.FormatNumberWithThousandsSeparator(declai);


                            }
                            else
                            {
                                CustomMessageBox.ShowCustomMessageBox("Không tìm thấy hợp đồng với mã này.", null, "LỖI MÃ HỢP ĐỒNG");
                            }
                        }
                    }
                    // Tính toán tổng tiền chuộc
                    decimal tienVay = Convert.ToDecimal(tb_TienVay.Text);
                    decimal lai = Convert.ToDecimal(tb_Lai.Text);
                    decimal tongTienChuoc = tienVay + lai;

                    tb_TongTienChuoc.Text = tongTienChuoc.ToString("N0"); // Hiển thị với định dạng số nguyên
                }
            }
            catch (Exception ex)
            {
                CustomMessageBox.ShowCustomMessageBox($"Đã xảy ra lỗi: {ex.Message}", null, "LỖI KẾT NỐI CƠ SỞ DỮ LIỆU");

            }
        }

        private void tb_TienKhac_TextChanged(object sender, EventArgs e)
        {

            string placeholder = "Nhập số tiền khác thêm vào tổng tiền chuộc.";


            // Gắn sự kiện
            tb_TienKhac.KeyPress += Function_Reuse.OnlyAllowDigitAndDot_KeyPress;

            tb_TienKhac.Enter += (s, e) => Function_Reuse.ClearPlaceholderOnEnter(tb_TienKhac, placeholder);
            tb_TienKhac.Leave += (s, e) => Function_Reuse.SetPlaceholderIfEmpty(tb_TienKhac, placeholder);

            tb_TienKhac.TextChanged += (s, e) => Function_Reuse.FormatTextBoxWithThousands(tb_TienKhac, placeholder);


        }
    }
}
