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
    public partial class PrintHD : Form
    {
        private string MaHD;
        public PrintHD(string MaHD)
        {
            InitializeComponent();
            this.MaHD = MaHD;
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
        }
        private void btn_In_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tb_ID.Text))
            {
               CustomMessageBox.ShowCustomMessageBox("Vui lòng nhập ID hợp đồng để in.", null, "THIẾU THÔNG TIN");
                return;
            }
            
        }
    }
}
