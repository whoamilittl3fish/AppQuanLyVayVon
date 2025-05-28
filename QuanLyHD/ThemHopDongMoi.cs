using QuanLyVayVon.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static QuanLyVayVon.CSDL.QuanLyCSDL;

namespace QuanLyVayVon.QuanLyHD
{
    public partial class ThemHopDongMoi : Form
    {
        private int? idTuCSDL = null;
        private static readonly Color AppBackColor = Color.FromArgb(245, 245, 250);
        private static readonly Font AppFont = new Font("Segoe UI", 11F, FontStyle.Regular);

        public ThemHopDongMoi(int? loaiTaiSanID = null)
        {
            InitializeComponent();
            this.idTuCSDL = loaiTaiSanID;

            this.BackColor = AppBackColor;
            this.Font = AppFont;
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;

            // Khởi tạo combo box cho loại tài sản và hình thức lãi
            InitLoaiTaiSanComboBox();
            InitHinhThucLaiComboBox();

            dTimePicker_NgayVay.CustomFormat = "dd/MM/yyyy"; // Định dạng ngày tháng
            dTimePicker_NgayVay.Value = DateTime.Now; // Mặc định là ngày hiện tại

            // Thiết lập các thuộc tính cho các TextBox và RichTextBox
            Function_Reuse.ClearTextBoxOnClick(tbox_MaHD, "Nhập mã hợp đồng.");
            Function_Reuse.ClearTextBoxOnClick(tbox_Ten, "Nhập họ và tên khách hàng.");
            Function_Reuse.ClearTextBoxOnClick(tbox_SDT, "Nhập số điện thoại.");
            Function_Reuse.ClearTextBoxOnClick(tbox_CCCD, "Nhập số CCCD/hộ chiếu.");
            Function_Reuse.ClearTextBoxOnClick(tb_TenTaiSan, "Nhập tên tài sản.");
            Function_Reuse.ClearRichTextBoxOnClick(rtb_DiaChi, "Nhập mô địa chỉ khách hàng");

            Function_Reuse.ClearTextBoxOnClick(tb_TienVay, "0");
            Function_Reuse.ClearRichTextBoxOnClick(rtb_GhiChu, "Nhập ghi chú (nếu có)");
        }

        private void InitLoaiTaiSanComboBox()
        {
            List<LoaiTaiSanItem> dsLoai = new()
                {
                    new LoaiTaiSanItem { ID = 1, Ten = "Xe máy" },
                    new LoaiTaiSanItem { ID = 2, Ten = "Ô tô" },
                    new LoaiTaiSanItem { ID = 3, Ten = "Điện thoại" },
                    new LoaiTaiSanItem { ID = 4, Ten = "Laptop" },
                    new LoaiTaiSanItem { ID = 5, Ten = "Vàng" },
                    new LoaiTaiSanItem { ID = 6, Ten = "Cavet" },
                    new LoaiTaiSanItem { ID = 7, Ten = "Sổ đỏ - nhà đất" },
                    new LoaiTaiSanItem { ID = 8, Ten = "Khác" }
                };

            cbBox_LoaiTaiSan.DataSource = dsLoai;
            cbBox_LoaiTaiSan.DisplayMember = "Ten";
            cbBox_LoaiTaiSan.ValueMember = "ID";

            if (idTuCSDL != null)
                cbBox_LoaiTaiSan.SelectedValue = idTuCSDL;
        }

        private void InitHinhThucLaiComboBox()
        {
            var dsHinhThucLai = new List<LoaiTaiSanItem>
            {
                new LoaiTaiSanItem { ID = 1, Ten = "Lãi VNĐ/ngày" },
                new LoaiTaiSanItem { ID = 2, Ten = "Lãi VNĐ/tuần" },
                new LoaiTaiSanItem { ID = 3, Ten = "Lãi VNĐ/tháng" },
                new LoaiTaiSanItem { ID = 4, Ten = "Lãi %/ngày" },
                new LoaiTaiSanItem { ID = 5, Ten = "Lãi %/tuần" },
                new LoaiTaiSanItem { ID = 6, Ten = "Lãi %/tháng" }
            };

            cbBox_HinhThucLai.DataSource = dsHinhThucLai;
            cbBox_HinhThucLai.DisplayMember = "Ten";
            cbBox_HinhThucLai.ValueMember = "ID";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void ThemHopDongMoi_Load(object sender, EventArgs e)
        {
            // Đã khởi tạo dữ liệu cho cbBox_LoaiTaiSan ở constructor, không cần gán lại ở đây
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void tbox_Ten_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbox_SDT_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Function_Reuse.ConfirmAndClose_App();
        }

        private void btn_QuayLai_Click(object sender, EventArgs e)
        {
            Function_Reuse.ShowFormIfNotOpen<QuanLyHopDong>();
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void lb_TongThoiGianVay_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void dTimePicker_NgayVay_ValueChanged(object sender, EventArgs e)
        {

        }

        private void tbox_MaHD_TextChanged(object sender, EventArgs e)
        {

        }

        private void tb_TenTaiSan_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

