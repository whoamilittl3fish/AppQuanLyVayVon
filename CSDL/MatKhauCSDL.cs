using QuanLyVayVon.QuanLyHD;
using System;
using System.Drawing;
using System.Runtime.InteropServices; // Add this namespace for DllImport
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace QuanLyVayVon.CSDL
{
    [SupportedOSPlatform("windows6.1")]
    public partial class MatKhauCSDL : Form
    {
        // Import for rounded corners
        [DllImport("gdi32.dll", SetLastError = true)]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        private static readonly Color AppBackColor = Color.FromArgb(245, 245, 250);
        private static readonly Font AppFont = new Font("Segoe UI", 11F, FontStyle.Regular);

        public MatKhauCSDL()
        {
            InitializeComponent();
            CustomizeUI();
            this.TopMost = true;
        }

        [SupportedOSPlatform("windows6.1")]
        private void CustomizeUI()
        {
            // Form style
            this.BackColor = AppBackColor;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Font = AppFont;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Width = 400;
            this.Height = 220;
            this.FormBorderStyle = FormBorderStyle.None;


            // Title label
            var lblTitle = new Label
            {
                Text = "Nhập mật khẩu CSDL",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.FromArgb(44, 62, 80),
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleCenter,
                Padding = new Padding(0, 8, 0, 8)
            };
            this.Controls.Add(lblTitle);

            // Password textbox
            tbox_MatKhauCSDL.Font = new Font("Segoe UI", 13F);
            tbox_MatKhauCSDL.PasswordChar = '●';
            tbox_MatKhauCSDL.BorderStyle = BorderStyle.FixedSingle;
            tbox_MatKhauCSDL.Width = 220;
            tbox_MatKhauCSDL.Height = TextRenderer.MeasureText("A", tbox_MatKhauCSDL.Font).Height + 14;

            // Đăng nhập button
            btn_DangNhapCSDL.Text = "Đăng nhập";
            btn_DangNhapCSDL.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btn_DangNhapCSDL.BackColor = Color.FromArgb(52, 152, 219);
            btn_DangNhapCSDL.ForeColor = Color.White;
            btn_DangNhapCSDL.FlatStyle = FlatStyle.Flat;
            btn_DangNhapCSDL.Height = 38;
            btn_DangNhapCSDL.Cursor = Cursors.Hand;
            btn_DangNhapCSDL.FlatAppearance.BorderSize = 0;
            btn_DangNhapCSDL.AutoSize = true;
            btn_DangNhapCSDL.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btn_DangNhapCSDL.Padding = new Padding(18, 0, 18, 0);
            btn_DangNhapCSDL.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btn_DangNhapCSDL.Width, btn_DangNhapCSDL.Height, 18, 18));

            // Quay lại button
            btn_QuayLai.Text = "Quay lại";
            btn_QuayLai.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btn_QuayLai.BackColor = Color.FromArgb(231, 76, 60);
            btn_QuayLai.ForeColor = Color.White;
            btn_QuayLai.FlatStyle = FlatStyle.Flat;
            btn_QuayLai.Height = 38;
            btn_QuayLai.Cursor = Cursors.Hand;
            btn_QuayLai.FlatAppearance.BorderSize = 0;
            btn_QuayLai.AutoSize = true;
            btn_QuayLai.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btn_QuayLai.Padding = new Padding(14, 0, 14, 0);
            btn_QuayLai.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btn_QuayLai.Width, btn_QuayLai.Height, 12, 12));

            // Calculate layout
            int margin = 18;
            int spacingY = 18;
            int spacingBtn = 16;

            // Title position
            lblTitle.Left = margin;
            lblTitle.Top = margin;
            lblTitle.Width = Math.Max(lblTitle.PreferredWidth, tbox_MatKhauCSDL.Width) + 2 * margin;

            // Password textbox position
            tbox_MatKhauCSDL.Left = (lblTitle.Width - tbox_MatKhauCSDL.Width) / 2;
            tbox_MatKhauCSDL.Top = lblTitle.Bottom + spacingY;

            // Buttons position
            int totalBtnWidth = btn_DangNhapCSDL.Width + spacingBtn + btn_QuayLai.Width;
            int btnsLeft = (lblTitle.Width - totalBtnWidth) / 2;
            int btnsTop = tbox_MatKhauCSDL.Bottom + spacingY;

            btn_DangNhapCSDL.Location = new Point(btnsLeft, btnsTop);
            btn_QuayLai.Location = new Point(btnsLeft + btn_DangNhapCSDL.Width + spacingBtn, btnsTop);

            // Add controls if not already
            if (!this.Controls.Contains(tbox_MatKhauCSDL)) this.Controls.Add(tbox_MatKhauCSDL);
            if (!this.Controls.Contains(btn_DangNhapCSDL)) this.Controls.Add(btn_DangNhapCSDL);
            if (!this.Controls.Contains(btn_QuayLai)) this.Controls.Add(btn_QuayLai);

            // Fit form size to content
            int formWidth = lblTitle.Width + margin * 2;
            int formHeight = btn_DangNhapCSDL.Bottom + margin;

            this.ClientSize = new Size(formWidth, formHeight);

            // Center all controls horizontally
            lblTitle.Left = (this.ClientSize.Width - lblTitle.Width) / 2;
            tbox_MatKhauCSDL.Left = (this.ClientSize.Width - tbox_MatKhauCSDL.Width) / 2;
            btn_DangNhapCSDL.Top = btn_QuayLai.Top = tbox_MatKhauCSDL.Bottom + spacingY;
            totalBtnWidth = btn_DangNhapCSDL.Width + spacingBtn + btn_QuayLai.Width;
            btnsLeft = (this.ClientSize.Width - totalBtnWidth) / 2;
            btn_DangNhapCSDL.Left = btnsLeft;
            btn_QuayLai.Left = btnsLeft + btn_DangNhapCSDL.Width + spacingBtn;

            // Focus textbox on load
            this.Load += (s, e) => tbox_MatKhauCSDL.Focus();
        }

        /// <summary>
        /// Kiểm tra mật khẩu và chuyển form nếu đúng
        /// </summary>
        private void CheckPassword()
        {
            if (tbox_MatKhauCSDL.Text == "3710")
            {
               if (Application.OpenForms.OfType<CSDL.QuanLyCSDL>().Any())
                {
                    Application.OpenForms.OfType<CSDL.QuanLyCSDL>().First().BringToFront();
                }
                else
                {
                    var form = new CSDL.QuanLyCSDL();
                    form.Show();
                }
                this.Close(); // Đóng form MatKhauCSDL sau khi đăng nhập thành công
            }
            else
            {
                CustomMessageBox.ShowCustomMessageBox("Mật khẩu không đúng!");
                tbox_MatKhauCSDL.Clear();
                tbox_MatKhauCSDL.Focus();
            }
        }

        /// <summary>
        /// Hiển thị message box custom
        /// </summary>
       

        #region Sự kiện nút và textbox

        // Nhấn Enter trong ô mật khẩu
        private void tbox_MatKhauCSDL_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CheckPassword();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        // Click label (không xử lý)
        private void label1_Click(object sender, EventArgs e) { }

        // Bo tròn góc cho TextBox mật khẩu khi thay đổi text
        private void tbox_MatKhauCSDL_TextChanged(object sender, EventArgs e)
        {
            tbox_MatKhauCSDL.Region = Region.FromHrgn(
                CreateRoundRectRgn(0, 0, tbox_MatKhauCSDL.Width, tbox_MatKhauCSDL.Height, 12, 12)
            );
        }

        // Nhấn nút Quay lại
        private void btn_QuayLai_Click(object sender, EventArgs e)
        {

            var qlhdForm = Application.OpenForms.OfType<QuanLyHD.QuanLyHopDong>().FirstOrDefault();

            if (qlhdForm != null)
            {
                if (!qlhdForm.Visible)
                    qlhdForm.Show();

                if (qlhdForm.WindowState == FormWindowState.Minimized)
                    qlhdForm.WindowState = FormWindowState.Normal;

                qlhdForm.BringToFront();
            }
            else
            {
                var form = new QuanLyHD.QuanLyHopDong();
                form.Show();
            }

            this.Close(); // Đóng form hiện tại (MatKhauCSDL)

        }

        // Nhấn nút Đăng nhập
        private void btn_DangNhapCSDL_Click(object sender, EventArgs e)
        {
            CheckPassword();
        }

        #endregion

        /// <summary>
        /// Bo tròn góc cho cửa sổ chính khi handle được tạo
        /// </summary>
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            this.Region = Region.FromHrgn(
                CreateRoundRectRgn(0, 0, this.Width, this.Height, 18, 18)
            );
        }
    }
}
