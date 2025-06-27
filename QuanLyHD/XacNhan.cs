using Microsoft.Data.Sqlite;
using static QuanLyVayVon.QuanLyHD.QuanLyHopDong;

namespace QuanLyVayVon.QuanLyHD
{
    public partial class XacNhan : Form
    {

        private bool isThisNewPass;
        public XacNhan(bool isThisNewPass = false)
        {
            InitializeComponent();
            CustomizeUI();

            this.isThisNewPass = isThisNewPass;
        }
        private void CustomizeUI()
        {
            // Form style
            this.BackColor = Color.FromArgb(245, 245, 250);
            this.FormBorderStyle = FormBorderStyle.None;
            this.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Width = 400;
            this.Height = 220;

            // Title label
            var lblTitle = new Label
            {
                Text = isThisNewPass ? "Nhập mật khẩu xác nhận" : "Đặt mật khẩu mới",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.FromArgb(44, 62, 80),
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleCenter,
                Padding = new Padding(0, 8, 0, 8)
            };
            this.Controls.Add(lblTitle);

            // Password textbox
            tb_MatKhau.Font = new Font("Segoe UI", 13F);
            tb_MatKhau.PasswordChar = '●';
            tb_MatKhau.BorderStyle = BorderStyle.FixedSingle;
            tb_MatKhau.Width = 220;
            tb_MatKhau.Height = TextRenderer.MeasureText("A", tb_MatKhau.Font).Height + 14;
            tb_MatKhau.Region = Region.FromHrgn(
                NativeMethods.CreateRoundRectRgn(0, 0, tb_MatKhau.Width, tb_MatKhau.Height, 12, 12)
            );

            // Confirm button
            btn_XacNhan.Text = isThisNewPass ? "Lưu mật khẩu" : "Xác nhận";
            btn_XacNhan.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btn_XacNhan.BackColor = Color.FromArgb(52, 152, 219);
            btn_XacNhan.ForeColor = Color.White;
            btn_XacNhan.FlatStyle = FlatStyle.Flat;
            btn_XacNhan.Height = 38;
            btn_XacNhan.Cursor = Cursors.Hand;
            btn_XacNhan.FlatAppearance.BorderSize = 0;
            btn_XacNhan.AutoSize = true;
            btn_XacNhan.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btn_XacNhan.Padding = new Padding(18, 0, 18, 0);
            btn_XacNhan.Region = Region.FromHrgn(
                NativeMethods.CreateRoundRectRgn(0, 0, btn_XacNhan.Width, btn_XacNhan.Height, 18, 18)
            );

            // Exit button
            btn_Thoat.Text = "Thoát";
            btn_Thoat.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btn_Thoat.BackColor = Color.FromArgb(231, 76, 60);
            btn_Thoat.ForeColor = Color.White;
            btn_Thoat.FlatStyle = FlatStyle.Flat;
            btn_Thoat.Height = 38;
            btn_Thoat.Cursor = Cursors.Hand;
            btn_Thoat.FlatAppearance.BorderSize = 0;
            btn_Thoat.AutoSize = true;
            btn_Thoat.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btn_Thoat.Padding = new Padding(14, 0, 14, 0);
            btn_Thoat.Region = Region.FromHrgn(
                NativeMethods.CreateRoundRectRgn(0, 0, btn_Thoat.Width, btn_Thoat.Height, 12, 12)
            );

            // Layout calculation
            int margin = 18;
            int spacingY = 18;
            int spacingBtn = 16;

            // Title position
            lblTitle.Left = margin;
            lblTitle.Top = margin;
            lblTitle.Width = Math.Max(lblTitle.PreferredWidth, tb_MatKhau.Width) + 2 * margin;

            // Password textbox position
            tb_MatKhau.Left = (lblTitle.Width - tb_MatKhau.Width) / 2;
            tb_MatKhau.Top = lblTitle.Bottom + spacingY;

            // Buttons position
            int totalBtnWidth = btn_XacNhan.Width + spacingBtn + btn_Thoat.Width;
            int btnsLeft = (lblTitle.Width - totalBtnWidth) / 2;
            int btnsTop = tb_MatKhau.Bottom + spacingY;

            btn_XacNhan.Location = new Point(btnsLeft, btnsTop);
            btn_Thoat.Location = new Point(btnsLeft + btn_XacNhan.Width + spacingBtn, btnsTop);

            // Add controls if not already
            if (!this.Controls.Contains(tb_MatKhau)) this.Controls.Add(tb_MatKhau);
            if (!this.Controls.Contains(btn_XacNhan)) this.Controls.Add(btn_XacNhan);
            if (!this.Controls.Contains(btn_Thoat)) this.Controls.Add(btn_Thoat);

            // Fit form size to content
            int formWidth = lblTitle.Width + margin * 2;
            int formHeight = btn_XacNhan.Bottom + margin;

            this.ClientSize = new Size(formWidth, formHeight);

            // Center all controls horizontally
            lblTitle.Left = (this.ClientSize.Width - lblTitle.Width) / 2;
            tb_MatKhau.Left = (this.ClientSize.Width - tb_MatKhau.Width) / 2;
            btn_XacNhan.Top = btn_Thoat.Top = tb_MatKhau.Bottom + spacingY;
            totalBtnWidth = btn_XacNhan.Width + spacingBtn + btn_Thoat.Width;
            btnsLeft = (this.ClientSize.Width - totalBtnWidth) / 2;
            btn_XacNhan.Left = btnsLeft;
            btn_Thoat.Left = btnsLeft + btn_XacNhan.Width + spacingBtn;

            // Focus textbox on load
            this.Load += (s, e) => tb_MatKhau.Focus();

            // Bo tròn góc cho cửa sổ
            this.Region = System.Drawing.Region.FromHrgn(
                NativeMethods.CreateRoundRectRgn(0, 0, this.Width, this.Height, 18, 18)
            );

            this.TopMost = true;
        }
        private void btn_XacNhan_Click(object sender, EventArgs e)
        {
            // Luu mat khau moi 
            if (this.isThisNewPass)
            {
                if (tb_MatKhau.Text.Length > 4)
                {
                    CustomMessageBox.ShowCustomMessageBox("Mt khẩu phải có đúng 4 ký tự!", null, "THÔNG BÁO");
                }
                string matKhau = tb_MatKhau.Text;
                string ghiChu = $"Thay đổi mật khẩu vào ngày {DateTime.Now:dd/MM/yyyy}";
                string dbPath = Path.Combine(Application.StartupPath, "DataBase", "data.db");
                try
                {
                    using (var conn = new SqliteConnection($"Data Source={dbPath}"))
                    {
                        conn.Open();
                        string sql = @"
        INSERT INTO HeThong (Khoa, GiaTri, GhiChu)
        VALUES ('MatKhau', @GiaTri, @GhiChu)
        ON CONFLICT(Khoa) DO UPDATE SET 
            GiaTri = excluded.GiaTri,
            GhiChu = excluded.GhiChu,
            UpdatedAt = CURRENT_TIMESTAMP;
    ";

                        using (var cmd = new SqliteCommand(sql, conn))
                        {
                            cmd.Parameters.AddWithValue("@GiaTri", matKhau);
                            cmd.Parameters.AddWithValue("@GhiChu", ghiChu);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    CustomMessageBox.ShowCustomMessageBox($"Lỗi khi cập nhật mật khẩu: {ex.Message}", null, "LỖI CẬP NHẬT MẬT KHẨU");
                    return;
                }
                CustomMessageBox.ShowCustomMessageBox("Mật khẩu đã được cập nhật thành công.", null, "Thông báo");
                return;
                this.Close();
            }
            else
            {
                // Kiểm tra mật khẩu
                if (string.IsNullOrEmpty(tb_MatKhau.Text))
                {
                    CustomMessageBox.ShowCustomMessageBox("Vui lòng nhập mật khẩu!", null, "THIẾU MẬT KHẨU");
                    return;
                }

                string nhapMatKhau = tb_MatKhau.Text.Trim();
                string dbPath = Path.Combine(Application.StartupPath, "DataBase", "data.db");

                try
                {
                    using (var conn = new SqliteConnection($"Data Source={dbPath}"))
                    {
                        conn.Open();
                        string sql = "SELECT GiaTri FROM HeThong WHERE Khoa = 'MatKhau' LIMIT 1";
                        using (var cmd = new SqliteCommand(sql, conn))
                        {
                            object? result = cmd.ExecuteScalar();
                            if (result == null)
                            {
                                CustomMessageBox.ShowCustomMessageBox("Không tìm thấy mật khẩu trong hệ thống!", null, "LỖI");
                                return;
                            }

                            string matKhauHeThong = result.ToString() ?? "";
                            if (nhapMatKhau != matKhauHeThong)
                            {
                                CustomMessageBox.ShowCustomMessageBox("Mật khẩu không đúng!", null, "SAI MẬT KHẨU");
                                return;
                            }
                        }
                    }

                    // Nếu đúng mật khẩu
                    CustomMessageBox.ShowCustomMessageBox("Mật khẩu chính xác.", null, "XÁC NHẬN");
                    this.DialogResult = DialogResult.Yes;
                    this.Close();
                }
                catch (Exception ex)
                {
                    CustomMessageBox.ShowCustomMessageBox($"Lỗi kiểm tra mật khẩu: {ex.Message}", null, "LỖI KIỂM TRA");
                }

            }
        }

        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }

        private void tb_MatKhau_TextChanged(object sender, EventArgs e)
        {
            if (tb_MatKhau.Text.Length > 4)
            {
               CustomMessageBox.ShowCustomMessageBox("Mt khẩu phải có đúng 4 ký tự!", null, "THÔNG BÁO");
            }
        }
    }
}
