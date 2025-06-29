﻿using Microsoft.Data.Sqlite;
using System.Runtime.InteropServices;
using static QuanLyVayVon.QuanLyHD.QuanLyHopDong;




namespace QuanLyVayVon.QuanLyHD
{

    public partial class TextToScreen : Form
    {
        [DllImport("user32.dll")]
        private static extern bool HideCaret(IntPtr hWnd);

        // Rename to avoid hiding Form.Text
       
        private string? MaHD = null!;
        public TextToScreen(string text, string label1, string label2, bool isThisNeedButton = false)
        {
            

            InitializeComponent();
            this.TopMost = true;
      
            rtb_Text.Text = text;
            CustomizeUI(label1, label2);
            this.MaHD = label2;
            if (isThisNeedButton)
            {
                btn_XacNhan.Visible = true;
  
            }
            else
            {
                btn_XacNhan.Visible = false;
            }


        }

        private void CustomizeUI(string label1, string label2)
        {
            
            // Add this to the constructor (after InitializeComponent()):
            this.DoubleBuffered = true;
            QuanLyHopDong.StyleExitButton(btn_Exit, "X");
            btn_Exit.Anchor = AnchorStyles.Right;
            QuanLyHopDong.StyleButton(btn_XacNhan, "Xác nhận");
            this.MouseDown += Frm_MouseDown!;
            this.AutoScaleMode = AutoScaleMode.Font;
            this.FormBorderStyle = FormBorderStyle.None;
            this.MaximizeBox = false;
            this.CenterToScreen();
            this.BackColor = Color.FromArgb(240, 240, 240);

            int borderRadius = 32;
            this.Region = System.Drawing.Region.FromHrgn(
                NativeMethods.CreateRoundRectRgn(0, 0, this.Width, this.Height, borderRadius, borderRadius)
            );

            rtb_Text.BackColor = Color.WhiteSmoke;
            rtb_Text.ForeColor = Color.FromArgb(44, 62, 80);
            rtb_Text.Font = new Font("Consolas", 11F);
            rtb_Text.ReadOnly = true;
            rtb_Text.BorderStyle = BorderStyle.None;
  
            rtb_Text.GotFocus += (s, e) => HideCaret(rtb_Text.Handle);
            rtb_Text.MouseDown += (s, e) => HideCaret(rtb_Text.Handle);
            rtb_Text.Enter += (s, e) => HideCaret(rtb_Text.Handle);
            rtb_Text.ShortcutsEnabled = true;

            // Ensure rtb_TieuDe is visible and not overlapping btn_Exit
            if (rtb_TieuDe != null)
            {
                rtb_TieuDe.Clear();
                rtb_TieuDe.BorderStyle = BorderStyle.None;
                rtb_TieuDe.BackColor = this.BackColor;
                rtb_TieuDe.ReadOnly = true;
                rtb_TieuDe.TabStop = false;
                rtb_TieuDe.Font = new Font("Montserrat", 18F, FontStyle.Regular);
                rtb_TieuDe.ForeColor = Color.FromArgb(44, 62, 80);
                rtb_TieuDe.Width = 600;
                rtb_TieuDe.Height = 50;
                rtb_TieuDe.ScrollBars = RichTextBoxScrollBars.None;
                rtb_TieuDe.Multiline = false;
           

                // Show label and label2 safely
                string tieuDeMoTa = label1 ?? string.Empty;
                string giaTriChiTiet = label2 ?? string.Empty;

                rtb_TieuDe.SelectionColor = Color.FromArgb(44, 62, 80);
                rtb_TieuDe.AppendText(tieuDeMoTa);

                rtb_TieuDe.SelectionColor = Color.Red;
                rtb_TieuDe.AppendText(giaTriChiTiet);
                
                rtb_TieuDe.MouseDown += (s, e) => ((RichTextBox)s!).DeselectAll();
            }

            // Move btn_Exit to top right, above rtb_TieuDe
            if (btn_Exit != null)
            {
                btn_Exit.BringToFront();
                btn_Exit.Location = new Point(this.ClientSize.Width - btn_Exit.Width - 10, 10);
            }
      
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

   



        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private void Frm_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }
        // Pseudocode plan:
        // 1. Override OnPaint to draw a smooth border using GraphicsPath for rounded corners.
        // 2. Use SmoothingMode.AntiAlias for high quality.
        // 3. Draw the border after base.OnPaint.
        // 4. In constructor, set DoubleBuffered = true for flicker-free rendering.

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            int borderRadius = 32;
            int borderWidth = 2;
            Color borderColor = Color.FromArgb(180, 44, 62, 80);

            using (var path = new System.Drawing.Drawing2D.GraphicsPath())
            {
                int w = this.Width - 1;
                int h = this.Height - 1;
                int r = borderRadius;

                path.AddArc(0, 0, r, r, 180, 90);
                path.AddArc(w - r, 0, r, r, 270, 90);
                path.AddArc(w - r, h - r, r, r, 0, 90);
                path.AddArc(0, h - r, r, r, 90, 90);
                path.CloseFigure();

                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (var pen = new Pen(borderColor, borderWidth))
                {
                    e.Graphics.DrawPath(pen, path);
                }
            }
        }

        private void btn_XacNhan_Click(object sender, EventArgs e)
        {
            string message = "Bạn có CHẮC CHẮN chuộc đồ và kết thúc hợp đồng này không? \n" +
                "Hợp đồng này sẽ bị KHOÁ lại hoàn toàn và KHÔNG THỂ SỬA nữa. \n" +
                "Bạn chỉ có thể thêm hợp đồng mới và lưu lại thông tin mới nếu cần sửa.";
           
            if (CustomMessageBox.ShowCustomYesNoMessageBox(message, null, null, 18, "KẾT THÚC HỢP ĐỒNG") == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            
        }
       
    }
}
