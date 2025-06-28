using Microsoft.Data.Sqlite;
using QuanLyVayVon.QuanLyHD;
using static QuanLyVayVon.QuanLyHD.QuanLyHopDong;

namespace QuanLyVayVon.CSDL
{
    public partial class ThongTinTiemCamDo : Form
    {
        public ThongTinTiemCamDo()
        {
            InitializeComponent();
            CustomizeUI();

            if (Function_Reuse.KiemTraDatabaseTonTai() == false)
            {
                CustomMessageBox.ShowCustomMessageBox("Cơ sở dữ liệu không tồn tại. Vui lòng kiểm tra lại.", null, "Thông báo");
                return;
            }
        }
        private void CustomizeUI()
        {
            QuanLyHopDong.StyleButton(btn_CapNhat);
            QuanLyHopDong.StyleExitButton(btn_Thoat, "X");

            QuanLyHopDong.StyleTextBox(tb_TenTiemCamDo);

            QuanLyHopDong.StyleTextBox(tb_Hotline);
            QuanLyHopDong.StyleTextBox(tb_DaiDien);
            QuanLyHopDong.StyleTextBox(tb_SDTDaiDien);
            QuanLyHopDong.StyleTextBox(tb_TaiKhoan);
            QuanLyHopDong.StyleTextBox(tb_TenNganHang);
            QuanLyHopDong.StyleTextBox(tb_TruongPGDTT);
            QuanLyHopDong.StyleTextBox(tb_TenTiemCamDo);
            QuanLyHopDong.StyleTextBox(tb_ID);

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

        }
        private bool CheckInput_1()
        {
            string err = "";
            if (string.IsNullOrWhiteSpace(tb_ID.Text))
            {
                err += "Vui lòng nhập ID tiệm cầm đồ.\n";
                tb_ID.BackColor = Color.LightPink; // Đặt màu nền đỏ nhạt nếu không có ID
                return false; // Trả về false nếu có lỗi
            }
           
       
            if (string.IsNullOrWhiteSpace(tb_TenTiemCamDo.Text))
            {
                err += "Vui lòng nhập tên tiệm cầm đồ.\n";
                tb_TenTiemCamDo.BackColor = Color.LightPink; // Đặt màu nền đỏ nhạt nếu không có tên
                return false; // Trả về false nếu có lỗi
            }
           
            if (string.IsNullOrWhiteSpace(rtb_DiaChi.Text))
            {
                err += "Vui lòng nhập địa chỉ tiệm cầm đồ.\n";
                rtb_DiaChi.BackColor = Color.LightPink; // Đặt màu nền đỏ nhạt nếu không có địa chỉ
                return false; // Trả về false nếu có lỗi
            }
            if (string.IsNullOrWhiteSpace(tb_Hotline.Text))
            {
                err += "Vui lòng nhập số hotline.\n";
                tb_Hotline.BackColor = Color.LightPink; // Đặt màu nền đỏ nhạt nếu không có hotline
                return false; // Trả về false nếu có lỗi
            }
            if (string.IsNullOrWhiteSpace(tb_DaiDien.Text))
            {
                err += "Vui lòng nhập tên đại diện tiệm cầm đồ.\n";
                tb_DaiDien.BackColor = Color.LightPink; // Đặt màu nền đỏ nhạt nếu không có tên đại diện
                return false; // Trả về false nếu có lỗi
            }
            if (string.IsNullOrWhiteSpace(tb_SDTDaiDien.Text))
            {
                err += "Vui lòng nhập số điện thoại đại diện tiệm cầm đồ.\n";
                tb_SDTDaiDien.BackColor = Color.LightPink; // Đặt màu nền đỏ nhạt nếu không có số điện thoại đại diện
                return false; // Trả về false nếu có lỗi
            }
            if (string.IsNullOrWhiteSpace(tb_TaiKhoan.Text))
            {
                err += "Vui lòng nhập tài khoản ngân hàng.\n";
                tb_TaiKhoan.BackColor = Color.LightPink; // Đặt màu nền đỏ nhạt nếu không có tài khoản ngân hàng
                return false; // Trả về false nếu có lỗi
            }
            if (string.IsNullOrWhiteSpace(tb_TenNganHang.Text))
            {
                err += "Vui lòng nhập tên ngân hàng.\n";
                tb_TenNganHang.BackColor = Color.LightPink; // Đặt màu nền đỏ nhạt nếu không có tên ngân hàng
                return false; // Trả về false nếu có lỗi
            }
            return true; // Trả về true nếu không có lỗi
        }

        private void btn_CapNhat_Click(object sender, EventArgs e)
        {
            string ID = tb_ID.Text.Trim();
            string TenTiem = tb_TenTiemCamDo.Text.Trim();
            string DiaChi = rtb_DiaChi.Text.Trim();
            string Hotline = tb_Hotline.Text.Trim();
            string DaiDien = tb_DaiDien.Text.Trim();
            string SDTDaiDien = tb_SDTDaiDien.Text.Trim();
            string TaiKhoan = tb_TaiKhoan.Text.Trim();
            string TenNganHang = tb_TenNganHang.Text.Trim();
            string TruongPGDTT = tb_TruongPGDTT.Text.Trim();


            string iHotline = Hotline.Replace(",", "");
            string iSDTDaiDien = SDTDaiDien.Replace(",", "");
            string iTaiKhoan = TaiKhoan.Replace(",", "");



            if (CheckInput_1() == false)
                return;

            string dbDir = Path.Combine(Application.StartupPath, "DataBase");
            if (!Directory.Exists(dbDir))
                Directory.CreateDirectory(dbDir);
            string dbPath = Path.Combine(dbDir, "data.db");

            if (Function_Reuse.KiemTraDatabaseTonTai() == false)
            {
                CustomMessageBox.ShowCustomMessageBox("Cơ sở dữ liệu không tồn tại. Vui lòng kiểm tra lại.", null, "Thông báo");
                return;
            }

            try
            {
                using (SqliteConnection conn = new SqliteConnection($"Data Source={dbPath}"))
                {
                    conn.Open();

                    bool idExists = false;

                    // Kiểm tra xem ID đã tồn tại trong bảng chưa
                    string checkQuery = "SELECT 1 FROM TiemCamDo WHERE Id = @Id";
                    using (SqliteCommand checkCmd = new SqliteCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@Id", ID);
                        var result = checkCmd.ExecuteScalar();
                        idExists = result != null;
                    }

                    if (idExists)
                    {
                        // Nếu tồn tại → cập nhật
                        string updateQuery = @"
                        UPDATE TiemCamDo SET
                            TenTiem = @TenTiem,
                            DiaChi = @DiaChi,
                            Hotline = @Hotline,
                            DaiDien = @DaiDien,
                            SDTDaiDien = @SDTDaiDien,
                            TaiKhoan = @TaiKhoan,
                            TenNganHang = @TenNganHang,
                            TruongPGDTT = @TruongPGDTT,
                            UpdatedAt = CURRENT_TIMESTAMP
                        WHERE Id = @Id";

                        using (SqliteCommand cmd = new SqliteCommand(updateQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@TenTiem", TenTiem);
                            cmd.Parameters.AddWithValue("@DiaChi", DiaChi);
                            cmd.Parameters.AddWithValue("@Hotline", iHotline);
                            cmd.Parameters.AddWithValue("@DaiDien", DaiDien);
                            cmd.Parameters.AddWithValue("@SDTDaiDien", iSDTDaiDien);
                            cmd.Parameters.AddWithValue("@TaiKhoan", iTaiKhoan);
                            cmd.Parameters.AddWithValue("@TenNganHang", TenNganHang);
                            cmd.Parameters.AddWithValue("@TruongPGDTT", TruongPGDTT);
                            cmd.Parameters.AddWithValue("@Id", ID);

                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Đã cập nhật thông tin tiệm cầm đồ.");
                        }
                    }
                    else
                    {
                        // Nếu không tồn tại → thêm mới
                        string insertQuery = @"
                            INSERT INTO TiemCamDo
                                (TenTiem, DiaChi, Hotline, DaiDien, SDTDaiDien, TaiKhoan, TenNganHang, TruongPGDTT)
                            VALUES
                                (@TenTiem, @DiaChi, @Hotline, @DaiDien, @SDTDaiDien, @TaiKhoan, @TenNganHang, @TruongPGDTT)";

                        using (SqliteCommand cmd = new SqliteCommand(insertQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@TenTiem", TenTiem);
                            cmd.Parameters.AddWithValue("@DiaChi", DiaChi);
                            cmd.Parameters.AddWithValue("@Hotline", iHotline);
                            cmd.Parameters.AddWithValue("@DaiDien", DaiDien);
                            cmd.Parameters.AddWithValue("@SDTDaiDien", iSDTDaiDien);
                            cmd.Parameters.AddWithValue("@TaiKhoan", TaiKhoan);
                            cmd.Parameters.AddWithValue("@TenNganHang", TenNganHang);
                            cmd.Parameters.AddWithValue("@TruongPGDTT", TruongPGDTT);

                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Đã thêm mới thông tin tiệm cầm đồ.");
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                CustomMessageBox.ShowCustomMessageBox("Đã xảy ra lỗi khi cập nhật thông tin tiệm cầm đồ: " + ex.Message, null, "Thông báo");
            }
            CustomMessageBox.ShowCustomMessageBox("Cập nhật thông tin tiệm cầm đồ thành công.", null, "Thông báo");
            this.Close(); // Đóng form sau khi cập nhật
            if (Application.OpenForms.OfType<QuanLyHD.QuanLyHopDong>().Any())
            {
                Application.OpenForms.OfType<QuanLyHD.QuanLyHopDong>().First().Show(); // Hiển thị lại TrangChu nếu đã mở
            }
            else
            {
                var mainForm = new QuanLyHD.QuanLyHopDong();
                mainForm.Show(); // Mở TrangChu mới nếu chưa có
            }
        }

        private void tb_Hotline_TextChanged(object sender, EventArgs e)
        {
            string placeholder = "";
            if (tb_Hotline.Text.Length > 0)
            {
                tb_Hotline.ForeColor = Color.Black;
                tb_Hotline.BackColor = Color.White;
            }
            else
            {
                tb_Hotline.BackColor = Color.LightGreen;
            }
            if (tb_Hotline.Text.Length > 15)
            {
                CustomMessageBox.ShowCustomMessageBox("Số điện thoại đại diện không được vượt quá 15 ký tự.", null, "Thông báo");
                tb_Hotline.Text = tb_Hotline.Text.Substring(0, 15);
                tb_Hotline.SelectionStart = tb_Hotline.Text.Length;
            }

            // Đảm bảo chỉ gắn sự kiện một lần
            tb_Hotline.KeyPress -= OnlyAllowDigit_KeyPress;
            tb_Hotline.KeyPress += OnlyAllowDigit_KeyPress;

            tb_Hotline.Enter -= TextBox_Enter_ClearPlaceholder;
            tb_Hotline.Enter += TextBox_Enter_ClearPlaceholder;
            tb_Hotline.Leave -= TextBox_Leave_SetPlaceholder;
            tb_Hotline.Leave += TextBox_Leave_SetPlaceholder;

            tb_Hotline.TextChanged -= TextBox_FormatWithThousands;
            tb_Hotline.TextChanged += TextBox_FormatWithThousands;
        }

        // Thêm hàm dùng chung cho các TextBox số
        private void OnlyAllowDigit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // Thêm hàm dùng chung cho sự kiện Enter
        private void TextBox_Enter_ClearPlaceholder(object sender, EventArgs e)
        {
            if (sender is TextBox tb)
            {
                string placeholder = "";
                Function_Reuse.ClearPlaceholderOnEnter(tb, placeholder);
            }
        }

        // Thêm hàm dùng chung cho sự kiện Leave
        private void TextBox_Leave_SetPlaceholder(object sender, EventArgs e)
        {
            if (sender is TextBox tb)
            {
                string placeholder = "";
                Function_Reuse.SetPlaceholderIfEmpty(tb, placeholder);
            }
        }

        // Thêm hàm dùng chung cho sự kiện TextChanged (format số, cho phép số 0 đầu)
        private void TextBox_FormatWithThousands(object sender, EventArgs e)
        {
            if (sender is TextBox tb)
            {
                string placeholder = "";
                Function_Reuse.FormatTextBoxWithThousandsAllowLeadingZero(tb, placeholder);
            }
        }



        private void OnlyAllowDigitAndDot_KeyPress(object sender, KeyPressEventArgs e)
        {

            // Cho phép số, phím điều khiển và duy nhất một dấu chấm
            TextBox tb = sender as TextBox;
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            // Chỉ cho phép một dấu chấm
            if (e.KeyChar == '.' && tb != null && tb.Text.Contains('.'))
            {
                e.Handled = true;
            }
        }

        private void tb_SDTDaiDien_TextChanged(object sender, EventArgs e)
        {
            string placeholder = "";
            if (tb_SDTDaiDien.Text.Length > 0)
            {
                tb_SDTDaiDien.ForeColor = Color.Black;
                tb_SDTDaiDien.BackColor = Color.White;
            }
            else
            {
                tb_SDTDaiDien.BackColor = Color.LightGreen;
            }
            if (tb_SDTDaiDien.Text.Length > 15)
            {
                CustomMessageBox.ShowCustomMessageBox("Số điện thoại đại diện không được vượt quá 15 ký tự.", null, "Thông báo");
                tb_SDTDaiDien.Text = tb_SDTDaiDien.Text.Substring(0, 15);
                tb_SDTDaiDien.SelectionStart = tb_SDTDaiDien.Text.Length;
            }


            // Đảm bảo chỉ gắn sự kiện một lần
            tb_SDTDaiDien.KeyPress -= OnlyAllowDigit_KeyPress;
            tb_SDTDaiDien.KeyPress += OnlyAllowDigit_KeyPress;

            tb_SDTDaiDien.Enter -= TextBox_Enter_ClearPlaceholder;
            tb_SDTDaiDien.Enter += TextBox_Enter_ClearPlaceholder;
            tb_SDTDaiDien.Leave -= TextBox_Leave_SetPlaceholder;
            tb_SDTDaiDien.Leave += TextBox_Leave_SetPlaceholder;

            tb_SDTDaiDien.TextChanged -= TextBox_FormatWithThousands;
            tb_SDTDaiDien.TextChanged += TextBox_FormatWithThousands;
        }

        private void tb_TaiKhoan_TextChanged(object sender, EventArgs e)
        {


            if (tb_TaiKhoan.Text.Length > 0)
            {
                tb_TaiKhoan.ForeColor = Color.Black; // Đặt màu chữ thành đen khi có nội dung
                tb_TaiKhoan.BackColor = Color.White; // Đặt màu nền trắng khi có nội dung
            }
            else
            {
                tb_TaiKhoan.BackColor = Color.LightGreen; // Đặt màu chữ thành xám khi không có nội dung
            }
            if (tb_TaiKhoan.Text.Length > 20)
            {
                CustomMessageBox.ShowCustomMessageBox("Số tài khoản không được vượt quá 20 ký tự.", null, "Thông báo");
                tb_TaiKhoan.Text = tb_TenNganHang.Text.Substring(0, 50); // Cắt chuỗi nếu vượt quá 50 ký tự
            }

            // Đảm bảo chỉ gắn sự kiện một lần
            tb_TaiKhoan.KeyPress -= OnlyAllowDigit_KeyPress;
            tb_TaiKhoan.KeyPress += OnlyAllowDigit_KeyPress;

            tb_TaiKhoan.Enter -= TextBox_Enter_ClearPlaceholder;
            tb_TaiKhoan.Enter += TextBox_Enter_ClearPlaceholder;
            tb_TaiKhoan.Leave -= TextBox_Leave_SetPlaceholder;
            tb_TaiKhoan.Leave += TextBox_Leave_SetPlaceholder;

            tb_TaiKhoan.TextChanged -= TextBox_FormatWithThousands;
            tb_TaiKhoan.TextChanged += TextBox_FormatWithThousands;
        }

        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            // Đóng form hiện tại
            if (Application.OpenForms.OfType<QuanLyHD.QuanLyHopDong>().Any())
            {
                Application.OpenForms.OfType<QuanLyHD.QuanLyHopDong>().First().Show(); // Hiển thị lại TrangChu nếu đã mở
            }
            else
            {
                var mainForm = new QuanLyHD.QuanLyHopDong();
                mainForm.Show(); // Mở TrangChu mới nếu chưa có
            }
            this.Close();
        }

        private void tb_TruongPGDTT_TextChanged(object sender, EventArgs e)
        {

            if (tb_TruongPGDTT.Text.Length > 0)
            {
                tb_TruongPGDTT.ForeColor = Color.Black; // Đặt màu chữ thành đen khi có nội dung
                tb_TruongPGDTT.BackColor = Color.White; // Đặt màu nền trắng khi có nội dung
            }
            else
            {
                tb_TruongPGDTT.BackColor = Color.LightGreen; // Đặt màu chữ thành xám khi không có nội dung
            }
            if (tb_TruongPGDTT.Text.Length > 50)
            {
                CustomMessageBox.ShowCustomMessageBox("Tên trưởng PGDTT không được vượt quá 50 ký tự.", null, "Thông báo");
                tb_TruongPGDTT.Text = tb_TruongPGDTT.Text.Substring(0, 50); // Cắt chuỗi nếu vượt quá 50 ký tự
            }
        }

        private void tb_TenTiemCamDo_TextChanged(object sender, EventArgs e)
        {


            if (tb_TenTiemCamDo.Text.Length > 0)
            {
                tb_TenTiemCamDo.ForeColor = Color.Black; // Đặt màu chữ thành đen khi có nội dung
                tb_TenTiemCamDo.BackColor = Color.White; // Đặt màu nền trắng khi có nội dung
            }
            else
            {
                tb_TenTiemCamDo.BackColor = Color.LightGreen; // Đặt màu chữ thành xám khi không có nội dung
            }
            if (tb_TenTiemCamDo.Text.Length > 50)
            {
                CustomMessageBox.ShowCustomMessageBox("Tên tiệm cầm đồ không được vượt quá 50 ký tự.", null, "Thông báo");
                tb_TenTiemCamDo.Text = tb_TenTiemCamDo.Text.Substring(0, 50); // Cắt chuỗi nếu vượt quá 50 ký tự
            }
        }

        private void tb_DiaChi_TextChanged(object sender, EventArgs e)
        {


        }

        private void tb_DaiDien_TextChanged(object sender, EventArgs e)
        {

            if (tb_DaiDien.Text.Length > 0)
            {
                tb_DaiDien.ForeColor = Color.Black; // Đặt màu chữ thành đen khi có nội dung
                tb_DaiDien.BackColor = Color.White; // Đặt màu nền trắng khi có nội dung
            }
            else
            {
                tb_DaiDien.BackColor = Color.LightGreen; // Đặt màu chữ thành xám khi không có nội dung
            }
            if (tb_DaiDien.Text.Length > 50)
            {
                CustomMessageBox.ShowCustomMessageBox("Tên đại diện không được vượt quá 50 ký tự.", null, "Thông báo");
                tb_DaiDien.Text = tb_DaiDien.Text.Substring(0, 50); // Cắt chuỗi nếu vượt quá 50 ký tự
            }
        }

        private void tb_TenNganHang_TextChanged(object sender, EventArgs e)
        {


            if (tb_TenNganHang.Text.Length > 0)
            {
                tb_TenNganHang.ForeColor = Color.Black; // Đặt màu chữ thành đen khi có nội dung
                tb_TenNganHang.BackColor = Color.White; // Đặt màu nền trắng khi có nội dung
            }
            else
            {
                tb_TenNganHang.BackColor = Color.LightGreen; // Đặt màu chữ thành xám khi không có nội dung
            }
            if (tb_TenNganHang.Text.Length > 50)
            {
                CustomMessageBox.ShowCustomMessageBox("Tên ngân hàng không được vượt quá 50 ký tự.", null, "Thông báo");
                tb_TenNganHang.Text = tb_TenNganHang.Text.Substring(0, 50); // Cắt chuỗi nếu vượt quá 50 ký tự
            }
        }

        private void rtb_DiaChi_TextChanged(object sender, EventArgs e)
        {
            if (rtb_DiaChi.Text.Length > 0)
            {
                rtb_DiaChi.ForeColor = Color.Black; // Đặt màu chữ thành đen khi có nội dung
                rtb_DiaChi.BackColor = Color.White; // Đặt màu nền trắng khi có nội dung
            }
            else
            {
                rtb_DiaChi.BackColor = Color.LightGreen; // Đặt màu chữ thành xám khi không có nội dung
            }
            if (rtb_DiaChi.Text.Length > 100)
            {
                CustomMessageBox.ShowCustomMessageBox("Địa chỉ không được vượt quá 100 ký tự.", null, "Thông báo");
                rtb_DiaChi.Text = rtb_DiaChi.Text.Substring(0, 100); // Cắt chuỗi nếu vượt quá 100 ký tự
            }
        }

        private void tb_ID_TextChanged(object sender, EventArgs e)
        {
            if (tb_ID.Text.Length > 0)
            {
                tb_ID.ForeColor = Color.Black; // Đặt màu chữ thành đen khi có nội dung
                tb_ID.BackColor = Color.White; // Đặt màu nền trắng khi có nội dung
            }
            else
            {
                tb_ID.BackColor = Color.LightGreen; // Đặt màu chữ thành xám khi không có nội dung
            }
            if (tb_ID.Text.Length > 1)
            {
                CustomMessageBox.ShowCustomMessageBox("Chỉ lưu tối đa 9 ô thông tin tiệm", null, "Thông báo");
                tb_ID.Text = tb_TenNganHang.Text.Substring(0, 50); // Cắt chuỗi nếu vượt quá 50 ký tự
            }

            // Đảm bảo chỉ gắn sự kiện một lần
            tb_ID.KeyPress -= OnlyAllowDigit_KeyPress;
            tb_ID.KeyPress += OnlyAllowDigit_KeyPress;

            tb_ID.Enter -= TextBox_Enter_ClearPlaceholder;
            tb_ID.Enter += TextBox_Enter_ClearPlaceholder;
            tb_ID.Leave -= TextBox_Leave_SetPlaceholder;
            tb_ID.Leave += TextBox_Leave_SetPlaceholder;

            tb_ID.TextChanged -= TextBox_FormatWithThousands;
            tb_ID.TextChanged += TextBox_FormatWithThousands;
        }
    }
}
