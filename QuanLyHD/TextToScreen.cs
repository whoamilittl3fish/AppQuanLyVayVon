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
using System.Runtime.InteropServices;




namespace QuanLyVayVon.QuanLyHD
{
   
public partial class TextToScreen : Form
    {
        [DllImport("user32.dll")]
        private static extern bool HideCaret(IntPtr hWnd); // Corrected placement of DllImport attribute

        string? Text = null!;
        string? label = null!; // Biến để lưu nội dung lịch sử và nhãn

        public TextToScreen(string Text, string label)
        {
            InitializeComponent();
            CustomizeUI();
            this.Text = Text; // Lưu nội dung lịch sử vào thuộc tính Text của form
            this.label = label; // Lưu nhãn vào biến
            rtb_Text.Text = Text; // Hiển thị nội dung lịch sử vào RichTextBox
        }

        private void CustomizeUI()
        {
            QuanLyHopDong.StyleExitButton(btn_Exit, "X");
            this.MouseDown += Frm_MouseDown; // Cho phép kéo form bằng chuột
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

            rtb_Text.BackColor = Color.WhiteSmoke;
            rtb_Text.ForeColor = Color.FromArgb(44, 62, 80); // Màu chữ xanh đen dễ đọc
            rtb_Text.Font = new Font("Consolas", 11F); // Font mono nhìn rõ log, gọn
            rtb_Text.ReadOnly = true;
            rtb_Text.BorderStyle = BorderStyle.None;
            rtb_Text.Text = Text; // Hiển thị nội dung lịch sử vào RichTextBox
            rtb_Text.GotFocus += (s, e) => HideCaret(rtb_Text.Handle);
            rtb_Text.MouseDown += (s, e) => HideCaret(rtb_Text.Handle);
            rtb_Text.Enter += (s, e) => HideCaret(rtb_Text.Handle);

            rtb_Text.ShortcutsEnabled = true; // Cho phép Ctrl+C

            HienThiTieuDe("Lịch sử thay đổi hợp đồng mã: ", label, 10, 600); // Hiển thị tiêu đề với mã hợp đồng
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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close(); // Đóng form khi nhấn nút "Đóng"
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


        // Cho phép kéo form
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        // Gắn vào sự kiện MouseDown của form (hoặc panel tiêu đề tùy bạn)
        private void Frm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }
    }
}
