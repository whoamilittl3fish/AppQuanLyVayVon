using Microsoft.Data.Sqlite;
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
    public partial class HopDongForm : Form
    {
        bool isThisEditMode = false; // Biến để xác định chế độ chỉnh sửa hay thêm mới
        private string? MaHD = null; // Biến để lưu mã hợp đồng khi chỉnh sửa
        public HopDongForm()
        {
            InitializeComponent();

            cbBox_HinhThucLai.Enabled = true;
            cbBox_LoaiTaiSan.Enabled = true;
            cbBox_HinhThucLai.DropDownStyle = ComboBoxStyle.DropDownList;
            cbBox_LoaiTaiSan.DropDownStyle = ComboBoxStyle.DropDownList;

            CustomizeUI();

            InitLoaiTaiSanComboBox();
            InitHinhThucLaiComboBox();


            

            toolTip_KyLai.SetToolTip(lb_KyLai, "Kỳ lãi được tính theo đơn vị.\r\n10 (ngày) tương đương với kỳ hạn đóng lãi 10 ngày trả một lần.\r\n1 (tuần) tương đương với kỳ hạn 1 tuần = 7 ngày trả một lần.\r\n\r\nVD: \r\n*  Ngày 01/01/2025 vay, kỳ lãi là 3 ngày. Thì ngày 04/01/2025 sẽ phải đóng lãi.\r\n*  1 tuần đóng một lần thì sẽ nhập vào 1");
            toolTip_KyLai.SetToolTip(tb_KyLai, "Kỳ lãi được tính theo đơn vị.\r\n10 (ngày) tương đương với kỳ hạn đóng lãi 10 ngày trả một lần.\r\n1 (tuần) tương đương với kỳ hạn 1 tuần = 7 ngày trả một lần.\r\n\r\nVD: \r\n*  Ngày 01/01/2025 vay, kỳ lãi là 3 ngày. Thì ngày 04/01/2025 sẽ phải đóng lãi.\r\n1 tuần đóng một lần thì sẽ nhập vào 1");
            toolTip_KyLai.SetToolTip(lb_TongThoiGianVay, "Tổng thời gian vay được tính theo đơn vị.\r\n10 (ngày) tương đương với tổng thời gian vay là 10 ngày.\r\n1 (tuần) tương đương với tổng thời gian vay là 7 ngày.\r\n\r\nVD: \r\n*  Ngày 01/01/2025 vay, tổng thời gian vay là 3 ngày. Thì ngày 04/01/2025 sẽ hết hạn hợp đồng.\r\n");
            toolTip_KyLai.SetToolTip(tb_TongThoiGianVay, "Tổng thời gian vay được tính theo đơn vị.\r\n10 (ngày) tương đương với tổng thời gian vay là 10 ngày.\r\n1 (tuần) tương đương với tổng thời gian vay là 7 ngày.\r\n\r\nVD: \r\n*  Ngày 01/01/2025 vay, tổng thời gian vay là 3 ngày. Thì ngày 04/01/2025 sẽ hết hạn hợp đồng.\r\n");
            toolTip_KyLai.SetToolTip(tb_Lai, "Lãi tiền cố định theo đơn vị thời gian.\r\nVD: 1000 (VNĐ/ngày) sẽ là 1000 VNĐ mỗi ngày.\r\n1 (tuần) tương đương với 7000 VNĐ mỗi tuần.\r\n\r\nNếu lãi là phần trăm thì sẽ điền vào ô bên dưới.");
            toolTip_KyLai.SetToolTip(lb_Lai, "Lãi tiền cố định theo đơn vị thời gian.\r\nVD: 1000 (VNĐ/ngày) sẽ là 1000 VNĐ mỗi ngày.\r\n1 (tuần) tương đương với 7000 VNĐ mỗi tuần.\r\n\r\nNếu lãi là phần trăm thì sẽ điền vào ô bên dưới.");
        }
        private void LoadThemHopDong()
        {

            dTimePicker_NgayVay.CustomFormat = "dd/MM/yyyy";
            dTimePicker_NgayVay.Value = DateTime.Now;

            cbBox_LoaiTaiSan.SelectedValue = 8;
            cbBox_HinhThucLai.SelectedValue = 1;

            Function_Reuse.ClearTextBoxOnClick(tbox_MaHD, "Nhập mã hợp đồng.");
            Function_Reuse.ClearTextBoxOnClick(tbox_Ten, "Nhập họ và tên khách hàng.");
            Function_Reuse.ClearTextBoxOnClick(tbox_SDT, "Nhập số điện thoại.");
            Function_Reuse.ClearTextBoxOnClick(tbox_CCCD, "Nhập số CCCD/hộ chiếu.");
            Function_Reuse.ClearTextBoxOnClick(tb_TienVay, "Nhập số tiền vay.");
            Function_Reuse.ClearTextBoxOnClick(tb_TongThoiGianVay, "Nhập tổng thời gian vay.");
            Function_Reuse.ClearRichTextBoxOnClick(rtb_ThongtinTaiSan, "Nhập thông tin tài sản, chi tiết tài sản (nếu có).");
            Function_Reuse.ClearRichTextBoxOnClick(rtb_DiaChi, "Nhập địa chỉ khách hàng");
            Function_Reuse.ClearTextBoxOnClick(tb_NhanVienThuTien, "Nhập tên nhân viên thu tiền.");
            Function_Reuse.ClearRichTextBoxOnClick(rtb_GhiChu, "Nhập ghi chú (nếu có)");
            Function_Reuse.ClearTextBoxOnClick(tb_Lai, "Nhập tiền lãi.");
            Function_Reuse.ClearTextBoxOnClick(tb_KyLai, "Nhập kỳ lãi.");

            tb2_ThongtinTaiSan.Visible = false;
            lb2_ThongtinTaiSan.Visible = false;
            lb3_ThongtinTaiSan.Visible = false;
            tb3_ThongtinTaiSan.Visible = false;
            tb1_ThongtinTaiSan.Visible = false;
            lb1_ThongtinTaiSan.Visible = false;
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbBox_LoaiTaiSan.SelectedValue is int selectedId)
            {
                if (selectedId == 5 || selectedId == 6 || selectedId == 7 || selectedId == 8)
                {
                    lb1_ThongtinTaiSan.Visible = false;
                    tb1_ThongtinTaiSan.Visible = false;
                    lb2_ThongtinTaiSan.Visible = false;
                    tb2_ThongtinTaiSan.Visible = false;
                    lb3_ThongtinTaiSan.Visible = false;
                    tb3_ThongtinTaiSan.Visible = false;
                }
                else if (selectedId == 1)
                {
                    lb1_ThongtinTaiSan.Text = "Biển kiểm soát";
                    tb1_ThongtinTaiSan.Visible = true;
                    lb1_ThongtinTaiSan.Visible = true;
                    Function_Reuse.ClearTextBoxOnClick(tb1_ThongtinTaiSan, "Nhập biển kiểm soát xe máy.");

                    lb2_ThongtinTaiSan.Visible = true;
                    tb2_ThongtinTaiSan.Visible = true;
                    lb2_ThongtinTaiSan.Text = "Số khung xe máy";
                    Function_Reuse.ClearTextBoxOnClick(tb2_ThongtinTaiSan, "Nhập số khung xe máy.");

                    lb3_ThongtinTaiSan.Text = "Số máy xe máy";
                    lb3_ThongtinTaiSan.Visible = true;
                    tb3_ThongtinTaiSan.Visible = true;
                    Function_Reuse.ClearTextBoxOnClick(tb3_ThongtinTaiSan, "Nhập số máy xe máy.");
                }
                else if (selectedId == 2)
                {
                    lb1_ThongtinTaiSan.Text = "Biển kiểm soát";
                    tb1_ThongtinTaiSan.Visible = true;
                    lb1_ThongtinTaiSan.Visible = true;
                    Function_Reuse.ClearTextBoxOnClick(tb1_ThongtinTaiSan, "Nhập biển kiểm soát ô tô.");

                    lb2_ThongtinTaiSan.Visible = true;
                    tb2_ThongtinTaiSan.Visible = true;
                    lb2_ThongtinTaiSan.Text = "Số khung ô tô";
                    Function_Reuse.ClearTextBoxOnClick(tb2_ThongtinTaiSan, "Nhập số khung ô tô.");

                    lb3_ThongtinTaiSan.Text = "Số máy ô tô";
                    tb2_ThongtinTaiSan.Visible = true;
                    lb3_ThongtinTaiSan.Visible = true;
                    Function_Reuse.ClearTextBoxOnClick(tb3_ThongtinTaiSan, "Nhập số máy ô tô.");
                }
                else if (selectedId == 3)
                {
                    lb1_ThongtinTaiSan.Text = "Số IMEI";
                    tb1_ThongtinTaiSan.Visible = true;
                    lb1_ThongtinTaiSan.Visible = true;
                    Function_Reuse.ClearTextBoxOnClick(tb1_ThongtinTaiSan, "Nhập số IMEI của điện thoại.");

                    lb2_ThongtinTaiSan.Visible = true;
                    tb2_ThongtinTaiSan.Visible = true;
                    Function_Reuse.ClearTextBoxOnClick(tb2_ThongtinTaiSan, "Nhập mật khẩu điện thoại (nếu có).");
                    lb2_ThongtinTaiSan.Text = "Mật khẩu (nếu có)";

                    lb3_ThongtinTaiSan.Visible = true;
                    tb3_ThongtinTaiSan.Visible = true;
                    lb3_ThongtinTaiSan.Text = "Tình trạng máy";
                    Function_Reuse.ClearTextBoxOnClick(tb3_ThongtinTaiSan, "Nhập tình trạng máy (mới, cũ, hỏng, ...).");
                }
                else if (selectedId == 4)
                {
                    lb1_ThongtinTaiSan.Text = "Số seri";
                    tb1_ThongtinTaiSan.Visible = true;
                    lb1_ThongtinTaiSan.Visible = true;
                    Function_Reuse.ClearTextBoxOnClick(tb1_ThongtinTaiSan, "Nhập số seri của laptop.");
                    lb2_ThongtinTaiSan.Visible = true;
                    tb2_ThongtinTaiSan.Visible = true;
                    lb2_ThongtinTaiSan.Text = "Mật khẩu (nếu có)";
                    Function_Reuse.ClearTextBoxOnClick(tb2_ThongtinTaiSan, "Nhập mật khẩu của laptop (nếu có).");
                    lb3_ThongtinTaiSan.Visible = true;
                    tb3_ThongtinTaiSan.Visible = true;
                    lb3_ThongtinTaiSan.Text = "Tình trạng máy";
                    Function_Reuse.ClearTextBoxOnClick(tb3_ThongtinTaiSan, "Nhập tình trạng máy (mới, cũ, hỏng, ...).");
                }
            }
        }

        private void btn_QuayLai_Click(object sender, EventArgs e)
        {
            Function_Reuse.ConfirmAndClose(this, "Bạn có chắc muốn quay lại không?");
            if (this.DialogResult == DialogResult.No) return; // Nếu không xác nhận thì dừng lại
            this.Close();
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

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cbBox_HinhThucLai.SelectedValue is int selectedId)
            {
                if (selectedId == 1)
                {
                    lb_DonVi_TongThoiGianVay.Text = "Ngày";
                    lb_DonVi_KyLai.Text = "Ngày";
                    lb_DonVi_Lai.Text = "VNĐ/ngày";
                }
                else if (selectedId == 2)
                {
                    lb_DonVi_TongThoiGianVay.Text = "Tuần";
                    lb_DonVi_KyLai.Text = "Tuần";
                    lb_DonVi_Lai.Text = "VNĐ/tuần";
                }
                else if (selectedId == 3)
                {
                    lb_DonVi_TongThoiGianVay.Text = "Tháng";
                    lb_DonVi_KyLai.Text = "Tháng";
                    lb_DonVi_Lai.Text = "VNĐ/tháng";
                }
                else if (selectedId == 4)
                {
                    lb_DonVi_TongThoiGianVay.Text = "Ngày";
                    lb_DonVi_KyLai.Text = "Ngày";
                    lb_DonVi_Lai.Text = "%/ngày";
                }
                else if (selectedId == 5)
                {
                    lb_DonVi_TongThoiGianVay.Text = "Tuần";
                    lb_DonVi_KyLai.Text = "Tuần";
                    lb_DonVi_Lai.Text = "%/tuần";
                }
                else if (selectedId == 6)
                {
                    lb_DonVi_TongThoiGianVay.Text = "Tháng";
                    lb_DonVi_KyLai.Text = "Tháng";
                    lb_DonVi_Lai.Text = "%/tháng";
                }
            }
        }

        private void tb_Lai_TextChanged(object sender, EventArgs e)
        {
            if (cbBox_HinhThucLai.SelectedValue is not int selectedId) return;

            if (string.IsNullOrWhiteSpace(tb_Lai.Text) || string.IsNullOrWhiteSpace(tb_TienVay.Text) ||
                tb_Lai.Text == "Nhập tiền lãi." || tb_TienVay.Text == "0")
            {
                tb_ChuyenDoiLaiSuat.Text = "0";
                return;
            }

            if (!double.TryParse(tb_TienVay.Text, out double tienVay))
            {
                MessageBox.Show("Tiền vay phải là một số hợp lệ.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!double.TryParse(tb_Lai.Text, out double lai))
            {
                MessageBox.Show("Lãi phải là một số hợp lệ.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            double result = 0;
            switch (selectedId)
            {
                case 1:
                    result = (lai / tienVay) * 100 * 30;
                    tb_ChuyenDoiLaiSuat.Text = result.ToString("F2") + " %/tháng";
                    break;
                case 2:
                    result = ((lai / 7) / tienVay) * 100 * 30;
                    tb_ChuyenDoiLaiSuat.Text = result.ToString("F2") + " %/tháng";
                    break;
                case 3:
                    result = ((lai / 30) / tienVay) * 100 * 30;
                    tb_ChuyenDoiLaiSuat.Text = result.ToString("F2") + " %/tháng";
                    break;
                case 4:
                case 5:
                case 6:
                    result = (int)((lai * 30 / 100) * tienVay);
                    tb_ChuyenDoiLaiSuat.Text = result.ToString("F0") + " VNĐ/tháng";
                    break;
            }
        }

        private void btn_Luu_Click(object sender, EventArgs e)
        {
            Function_Reuse.ConfirmAndClose(this, "Bạn có chắc muốn lưu hợp đồng này không?");
            if (this.DialogResult != DialogResult.OK) return; // Nếu không xác nhận thì dừng lại
            string MaHD = tbox_MaHD.Text.Trim();
            string TenKH = tbox_Ten.Text.Trim();
            string SDT = tbox_SDT.Text.Trim();
            string CCCD = tbox_CCCD.Text.Trim();
            string DiaChi = rtb_DiaChi.Text.Trim();
            string GhiChu = rtb_GhiChu.Text.Trim();
            string NhanVienTT = tb_NhanVienThuTien.Text.Trim();
            string TenTaiSan = rtb_ThongtinTaiSan.Text.Trim();
            string ThongTinTaiSan1 = tb1_ThongtinTaiSan.Text.Trim();
            string ThongTinTaiSan2 = tb2_ThongtinTaiSan.Text.Trim();
            string ThongTinTaiSan3 = tb3_ThongtinTaiSan.Text.Trim();

            double tienVay = 0;
            double.TryParse(tb_TienVay.Text.Trim(), out tienVay);
            int tongThoiGianVay = 0;
            int.TryParse(tb_TongThoiGianVay.Text.Trim(), out tongThoiGianVay);
            int KyLai = 0;
            int.TryParse(tb_KyLai.Text.Trim(), out KyLai);
            int Lai = 0;
            int.TryParse(tb_Lai.Text.Trim(), out Lai);


            //clear textbox nếu không có giá trị nhập vào
            if (tbox_SDT.Text == "Nhập số điện thoại.")
            {
                SDT = string.Empty;
            }
            if (tbox_CCCD.Text == "Nhập số CCCD/hộ chiếu.")
            {
                CCCD = "";
            }
            if (rtb_DiaChi.Text == "Nhập địa chỉ khách hàng")
            {
                DiaChi = "";
            }
            if (rtb_GhiChu.Text == "Nhập ghi chú (nếu có)")
            {
                GhiChu = "";
            }
            if (tb_NhanVienThuTien.Text == "Nhập tên nhân viên thu tiền.")
            {
                NhanVienTT = "";
            }
            if (rtb_ThongtinTaiSan.Text == "Nhập thông tin tài sản, chi tiết tài sản (nếu có).")
            {
                TenTaiSan = "";
            }
            if (tb1_ThongtinTaiSan.Text == "Nhập biển kiểm soát xe máy." ||
                tb1_ThongtinTaiSan.Text == "Nhập biển kiểm soát ô tô." ||
                tb1_ThongtinTaiSan.Text == "Nhập số IMEI của điện thoại." ||
                tb1_ThongtinTaiSan.Text == "Nhập số seri của laptop.")
                ThongTinTaiSan1 = "";
            if (tb2_ThongtinTaiSan.Text == "Nhập số khung xe máy." ||
                tb2_ThongtinTaiSan.Text == "Nhập số khung ô tô." ||
                tb2_ThongtinTaiSan.Text == "Nhập mật khẩu điện thoại (nếu có)." ||
                tb2_ThongtinTaiSan.Text == "Nhập mật khẩu của laptop(nếu có).")
                ThongTinTaiSan2 = "";
            if (tb3_ThongtinTaiSan.Text == "Nhập số máy xe máy." ||
                tb3_ThongtinTaiSan.Text == "Nhập số máy ô tô." ||
                tb3_ThongtinTaiSan.Text == "Nhập tình trạng máy (mới, cũ, hỏng, ...).")
                ThongTinTaiSan3 = "";






            string errorMessages;
            // Gọi hàm validate, trả về true nếu hợp lệ, false nếu có lỗi
            bool isValid = CheckInput(out errorMessages);

            if (!isValid)
            {
                // Hiển thị lỗi nếu có
                MessageBox.Show(errorMessages, "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Dừng xử lý tiếp
            }



            //Xử lý thông tin combobox
            int? loaiTaiSanID = null;
            if (cbBox_LoaiTaiSan.SelectedValue != null && int.TryParse(cbBox_LoaiTaiSan.SelectedValue.ToString(), out int selectedID))
            {
                loaiTaiSanID = selectedID;
            }
            int hinhThucLaiID = 1;
            if (cbBox_LoaiTaiSan.SelectedValue != null && int.TryParse(cbBox_HinhThucLai.SelectedValue.ToString(), out int selectedHinhThucLaiID))
            {
                hinhThucLaiID = selectedHinhThucLaiID;
            }





            string dbPath = Path.Combine(Application.StartupPath, "Database", "data.db");
            using (var connection = new SqliteConnection($"Data Source={dbPath}"))
            {
                connection.Open();

                var checkCmd = connection.CreateCommand();
                checkCmd.CommandText = "SELECT COUNT(*) FROM HopDongVay WHERE MaHD = @MaHD";
                checkCmd.Parameters.AddWithValue("@MaHD", MaHD);

                long count = Convert.ToInt64(checkCmd.ExecuteScalar() ?? 0);

                if (count > 0)
                {
                    MessageBox.Show("Mã hợp đồng đã tồn tại, vui lòng nhập mã khác.");
                    return;
                }

                var insertCmd = connection.CreateCommand();
                insertCmd.CommandText = @"
    INSERT INTO HopDongVay (
        MaHD, TenKH, SDT, CCCD, DiaChi,
        TienVay, LoaiTaiSanID,
        NgayVay, NgayHetHan, KyDongLai, HinhThucLaiID, SoNgayVay, GhiChu,
    TenTaiSan, ThongTinTaiSan1, ThongTinTaiSan2, ThongTinTaiSan3, NVThuTien, SoTienLaiMoiKy, SoTienLaiCuoiKy,
        CreatedAt
    )
    VALUES (
        @MaHD, @TenKH, @SDT, @CCCD, @DiaChi,
        @TienVay, @LoaiTaiSanID,
        @NgayVay, @NgayHetHan, @KyDongLai, @HinhThucLaiID, @TenTaiSan, @SoNgayVay, @GhiChu,
        @ThongTinTaiSan1, @ThongTinTaiSan2, @ThongTinTaiSan3, @NVThuTien, @SoTienLaiMoiKy, @SoTienLaiCuoiKy,
        CURRENT_TIMESTAMP
    );
";

                // Các ngày tính toán:
                DateTime ngayVay = dTimePicker_NgayVay.Value.Date;
                DateTime ngayHetHan = ngayVay.AddDays(tongThoiGianVay); // Giả sử bạn đã có biến soNgayVay
                string ngayVayStr = ngayVay.ToString("yyyy-MM-dd");
                string ngayHetHanStr = ngayHetHan.ToString("yyyy-MM-dd");

                // Khởi tạo tính lãi để lưu
                var kq = TinhLaiHopDong(tienVay, Lai, tongThoiGianVay, hinhThucLaiID, KyLai);

                // Thêm tham số:
                insertCmd.Parameters.AddWithValue("@MaHD", MaHD);
                insertCmd.Parameters.AddWithValue("@TenKH", TenKH);
                insertCmd.Parameters.AddWithValue("@SDT", SDT);
                insertCmd.Parameters.AddWithValue("@CCCD", CCCD);
                insertCmd.Parameters.AddWithValue("@DiaChi", DiaChi);
                insertCmd.Parameters.AddWithValue("@TienVay", tienVay);
                insertCmd.Parameters.AddWithValue("@SoTienLaiMoiKy", kq.TienLaiMoiKy);
                insertCmd.Parameters.AddWithValue("@SoTienLaiCuoiKy", kq.TienLaiCuoiKy);
                insertCmd.Parameters.AddWithValue("@SoNgayVay", tongThoiGianVay);

                insertCmd.Parameters.AddWithValue("@LoaiTaiSanID", loaiTaiSanID);
                insertCmd.Parameters.AddWithValue("@TenTaiSan", TenTaiSan);
                insertCmd.Parameters.AddWithValue("@ThongTinTaiSan1", ThongTinTaiSan1);
                insertCmd.Parameters.AddWithValue("@ThongTinTaiSan2", ThongTinTaiSan2);
                insertCmd.Parameters.AddWithValue("@ThongTinTaiSan3", ThongTinTaiSan3);
                insertCmd.Parameters.AddWithValue("@NVThuTien", NhanVienTT);
                insertCmd.Parameters.AddWithValue("@GhiChu", GhiChu);


                insertCmd.Parameters.AddWithValue("@NgayVay", ngayVayStr);
                insertCmd.Parameters.AddWithValue("@NgayHetHan", ngayHetHanStr);
                insertCmd.Parameters.AddWithValue("@KyDongLai", KyLai);
                insertCmd.Parameters.AddWithValue("@HinhThucLaiID", hinhThucLaiID);




                try
                {
                    int rows = insertCmd.ExecuteNonQuery();

                    // Nếu là loại tài sản cần thông tin chi tiết thì cập nhật thêm
                    if (rows > 0 && (loaiTaiSanID >= 1 && loaiTaiSanID <= 4))
                    {
                        string tt1 = tb1_ThongtinTaiSan.Text.Trim();
                        string tt2 = tb2_ThongtinTaiSan.Text.Trim();
                        string tt3 = tb3_ThongtinTaiSan.Text.Trim();

                        var updateCmd = connection.CreateCommand();
                        updateCmd.CommandText = @"
                             UPDATE HopDongVay
                             SET ThongTinTaiSan1 = @TT1,
                                 ThongTinTaiSan2 = @TT2,
                                 ThongTinTaiSan3 = @TT3
                             WHERE MaHD = @MaHD;
                         ";
                        updateCmd.Parameters.AddWithValue("@TT1", tt1);
                        updateCmd.Parameters.AddWithValue("@TT2", tt2);
                        updateCmd.Parameters.AddWithValue("@TT3", tt3);
                        updateCmd.Parameters.AddWithValue("@MaHD", MaHD);

                        updateCmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Lưu hợp đồng thành công!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi lưu: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
               
            }
        }

        private void tb_TongThoiGianVay_TextChanged(object sender, EventArgs e)
        {
            tb_TongThoiGianVay.BackColor = Color.White; // Đặt lại màu nền khi người dùng bắt đầu nhập
        }
        private bool CheckInput(out string err)
        {
            err = "";

            if (tbox_MaHD.Text == "Nhập mã hợp đồng.")
            {
                tbox_MaHD.BackColor = Color.MediumVioletRed;

                err += "Mã hợp đồng không được để trống.\r\n";
            }

            if (tbox_Ten.Text == "Nhập họ và tên khách hàng.")
            {
                tbox_Ten.BackColor = Color.MediumVioletRed;
                err += "Tên khách hàng không được để trống.\r\n";
            }

            if (rtb_ThongtinTaiSan.Text == "Nhập thông tin tài sản, chi tiết tài sản (nếu có).")
            {
                rtb_ThongtinTaiSan.BackColor = Color.MediumVioletRed;
                err += "Thông tin tài sản không được để trống.\r\n";
            }

            int tmp;
            if (!int.TryParse(tb_TongThoiGianVay.Text, out tmp) || tb_TongThoiGianVay.Text == "Nhập tổng số tiền vay.")
            {
                tb_TongThoiGianVay.BackColor = Color.MediumVioletRed;
                err += "Tổng thời gian vay trống hoặc không hợp lệ.\r\n";
            }

            if (!int.TryParse(tb_TienVay.Text, out tmp) || tb_TienVay.Text == "Nhập tổng số tiền vay.")
            {
                tb_TienVay.BackColor = Color.MediumVioletRed;
                err += "Tiền vay trống hoặc không hợp lệ.\r\n";
            }
            if (!int.TryParse(tb_KyLai.Text, out tmp) || tb_KyLai.Text == "Nhập kỳ lãi.")
            {
                tb_KyLai.BackColor = Color.MediumVioletRed;
                err += "Kỳ lãi trống hoặc không hợp lệ.\r\n";
            }
            if (!int.TryParse(tb_Lai.Text, out tmp) || tb_Lai.Text == "Nhập tiền lãi.")
            {
                tb_Lai.BackColor = Color.MediumVioletRed;
                err += "Lãi trống hoặc không hợp lệ.\r\n";
            }

            return string.IsNullOrEmpty(err);
        }

        private void tbox_MaHD_TextChanged(object sender, EventArgs e)
        {
            tbox_MaHD.BackColor = Color.White; // Đặt lại màu nền khi người dùng bắt đầu nhập
        }

        private void tb_TienVay_TextChanged(object sender, EventArgs e)
        {
            tb_TienVay.BackColor = Color.White; // Đặt lại màu nền khi người dùng bắt đầu nhập
        }

        private void tb_KyLai_TextChanged(object sender, EventArgs e)
        {
            tb_TienVay.BackColor = Color.White; // Đặt lại màu nền khi người dùng bắt đầu nhập
        }

        private void rtb_ThongtinTaiSan_TextChanged(object sender, EventArgs e)
        {
            rtb_ThongtinTaiSan.BackColor = Color.White; // Đặt lại màu nền khi người dùng bắt đầu nhập
        }

        private void tbox_Ten_TextChanged(object sender, EventArgs e)
        {
            tbox_Ten.BackColor = Color.White;
        }

        public class KetQuaTinhLai
        {
            public double TongLai { get; set; }
            public double TienLaiMoiKy { get; set; }
            public double TienLaiCuoiKy { get; set; }
            public int SoKy { get; set; }  // Có thể cần dùng
        }


        public static KetQuaTinhLai TinhLaiHopDong(
    double tienVay, double laiNhap, int TongThoiGianVay, int hinhThucLaiID, int kyDongLai)
        {
            var hinhThuc = HinhThucLaiHelper.GetHinhThucLaiInfo(hinhThucLaiID);

            int soKy = (int)Math.Ceiling((double)TongThoiGianVay / kyDongLai);

            double tongLai = 0;
            if (hinhThuc.LoaiLai == "tienmat")
            {
                tongLai = laiNhap * TongThoiGianVay;
            }
            else if (hinhThuc.LoaiLai == "phantram")
            {
                tongLai = tienVay * (laiNhap / 100.0) * TongThoiGianVay;
            }
            else
            {
                throw new Exception("Loại lãi không hợp lệ");
            }

            tongLai = Math.Ceiling(tongLai / 1000.0) * 1000;

            double tienLaiMoiKy = Math.Ceiling((tongLai / soKy) / 1000.0) * 1000;
            double tienLaiCuoiKy = tongLai - tienLaiMoiKy * (soKy - 1);

            return new KetQuaTinhLai
            {
                TongLai = tongLai,
                TienLaiMoiKy = tienLaiMoiKy,
                TienLaiCuoiKy = tienLaiCuoiKy,
                SoKy = soKy
            };
        }

        private void lb_TongThoiGianVay_Click(object sender, EventArgs e)
        {

        }
        // ==== Native method bo góc button ====
        private static class NativeMethods
        {
            [System.Runtime.InteropServices.DllImport("gdi32.dll", SetLastError = true)]
            public static extern IntPtr CreateRoundRectRgn(
                int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);
        }
        public void CustomizeUI()
        {
            // Form properties
            this.Text = "Quản Lý Hợp Đồng Vay";
            this.FormBorderStyle = FormBorderStyle.None; // Ẩn nút tắt/ẩn/phóng to mặc định
            this.MaximizeBox = false;
            this.CenterToScreen(); // Đặt
            this.BackColor = Color.FromArgb(240, 240, 240);
            // Bo tròn form (bo nhiều hơn)
            int borderRadius = 32;
            this.Region = System.Drawing.Region.FromHrgn(
                NativeMethods.CreateRoundRectRgn(0, 0, this.Width, this.Height, borderRadius, borderRadius)
            );

            // Title label
            var titleLabel = new Label
            {
                Text = "Tạo Hợp Đồng Vay Vốn",
                Font = new Font("Montserrat", 18F, FontStyle.Bold, GraphicsUnit.Point),
                ForeColor = Color.FromArgb(44, 62, 80),
                AutoSize = true
            };

            // Center the title label horizontally at the top
            titleLabel.Location = new Point((this.ClientSize.Width - titleLabel.PreferredWidth) / 2, 15);
            titleLabel.Anchor = AnchorStyles.Top;

            this.Controls.Add(titleLabel);

            // Font đẹp hơn cho toàn bộ form (không in nghiêng)
            Font mainFont = new Font("Montserrat", 12.5F, FontStyle.Regular, GraphicsUnit.Point);
            Font mainFontBold = new Font("Montserrat", 13.5F, FontStyle.Bold, GraphicsUnit.Point);
            Font donViFont = new Font("Montserrat", 11F, FontStyle.Regular, GraphicsUnit.Point);
            Font dateTimeFont = new Font("Montserrat", 12.5F, FontStyle.Regular, GraphicsUnit.Point);

            // chỉnh màu rtb và label
            Color richTextBoxBackColor = Color.FromArgb(248, 250, 255);
            Color richTextBoxBorderColor = Color.FromArgb(200, 215, 240);
            Color donViLabelBackColor = Color.FromArgb(225, 240, 255);
            Color donViLabelForeColor = Color.FromArgb(30, 90, 160);


            void StyleTextBox(TextBox tb)
            {
                tb.Font = mainFont;
                tb.ForeColor = Color.Black;
                tb.Region = System.Drawing.Region.FromHrgn(
                    NativeMethods.CreateRoundRectRgn(0, 0, tb.Width, tb.Height, 20, 20) // Bo nhiều hơn
                );
            }

            void StyleRichTextBox(RichTextBox rtb)
            {
                void StyleRichTextBox(RichTextBox rtb)
                {
                    rtb.Font = mainFont;
                    rtb.ForeColor = Color.Black;
                    rtb.BackColor = richTextBoxBackColor;
                    rtb.BorderStyle = BorderStyle.None;

                    // Lưu lại vị trí và kích thước gốc
                    Point originalLocation = rtb.Location;
                    Size originalSize = rtb.Size;

                    // Tạo panel bo viền
                    var borderPanel = new Panel
                    {
                        BackColor = richTextBoxBorderColor,
                        Size = new Size(originalSize.Width + 4, originalSize.Height + 4),
                        Location = new Point(originalLocation.X - 2, originalLocation.Y - 2)
                    };

                    // Bo góc cho panel
                    borderPanel.Region = System.Drawing.Region.FromHrgn(
                        NativeMethods.CreateRoundRectRgn(0, 0, borderPanel.Width, borderPanel.Height, 24, 24)
                    );

                    // Di chuyển RichTextBox vào trong panel
                    rtb.Location = new Point(2, 2);
                    rtb.Size = originalSize;
                    borderPanel.Controls.Add(rtb);

                    // Thêm panel vào đúng chỗ trên form
                    this.Controls.Add(borderPanel);
                    borderPanel.BringToFront();
                }

            }

            void StyleButton(Button btn)
            {
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.BackColor = Color.FromArgb(52, 152, 219);
                btn.ForeColor = Color.White;
                btn.Font = mainFontBold;
                btn.Cursor = Cursors.Hand;
                btn.Region = System.Drawing.Region.FromHrgn(
                    NativeMethods.CreateRoundRectRgn(0, 0, btn.Width, btn.Height, 32, 32) // Bo nhiều hơn
                );
                btn.BackColor = Color.FromArgb(45, 140, 240);
            }

            void StyleComboBox(ComboBox cb)
            {
                cb.Font = mainFont;
                cb.ForeColor = Color.Black;
                cb.BackColor = Color.White;
                cb.Region = System.Drawing.Region.FromHrgn(
                    NativeMethods.CreateRoundRectRgn(0, 0, cb.Width, cb.Height, 20, 20) // Bo nhiều hơn
                );
                // Thêm vào trong hàm StyleComboBox sau các thuộc tính khác
                cb.DrawMode = DrawMode.OwnerDrawFixed;
                cb.DropDownStyle = ComboBoxStyle.DropDownList;
                cb.DrawItem += (s, e) =>
                {
                    e.DrawBackground();
                    using (var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                    using (var brush = new SolidBrush(cb.ForeColor))
                    {
                        if (e.Index >= 0)
                        {
                            string text = cb.Items[e.Index]?.ToString() ?? "";
                            e.Graphics.DrawString(text, cb.Font, brush, e.Bounds, sf);
                        }
                    }
                    e.DrawFocusRectangle();
                };
                cb.FlatStyle = FlatStyle.Flat; // Đặt kiểu phẳng

            }

            // Style cho DateTimePicker: bo tròn và chỉnh font lớn hơn
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

            // Thay thế hàm StyleDonViLabel trong CustomizeUI bằng phiên bản tự động co giãn chiều rộng theo nội dung
            void StyleDonViLabel(Label lb)
            {
                lb.Font = donViFont;
                lb.ForeColor = donViLabelForeColor;
                lb.BackColor = donViLabelBackColor;

                lb.AutoSize = true;
                lb.TextAlign = ContentAlignment.MiddleCenter;
                lb.Padding = new Padding(12, 0, 12, 0); // Padding lớn hơn để bo góc đẹp
                // Sau khi đặt AutoSize, cập nhật lại Region để bo góc đúng với kích thước mới
                lb.SizeChanged += (s, e) =>
                {
                    lb.Region = System.Drawing.Region.FromHrgn(
                        NativeMethods.CreateRoundRectRgn(0, 0, lb.Width, lb.Height, 16, 16) // Bo nhiều hơn
                    );
                };
                // Gọi luôn để bo góc ngay lần đầu
                lb.Region = System.Drawing.Region.FromHrgn(
                    NativeMethods.CreateRoundRectRgn(0, 0, lb.Width, lb.Height, 16, 16) // Bo nhiều hơn
                );
            }

            // Áp dụng style cho controls (không thay đổi vị trí/kích thước)
            StyleTextBox(tbox_MaHD);
            StyleTextBox(tbox_Ten);
            StyleTextBox(tb_TienVay);
            StyleTextBox(tb_TongThoiGianVay);
            StyleTextBox(tb_NhanVienThuTien);
            StyleTextBox(tb_Lai);
            StyleTextBox(tb_KyLai);
            StyleTextBox(tbox_SDT);
            StyleTextBox(tbox_CCCD);
            StyleTextBox(tb1_ThongtinTaiSan);
            StyleTextBox(tb2_ThongtinTaiSan);
            StyleTextBox(tb3_ThongtinTaiSan);
            StyleTextBox(tb_NhanVienThuTien);
            StyleTextBox(tb_ChuyenDoiLaiSuat);
            StyleRichTextBox(rtb_ThongtinTaiSan);
            StyleRichTextBox(rtb_DiaChi);
            StyleRichTextBox(rtb_GhiChu);
            StyleButton(btn_Luu);
            StyleButton(btn_QuayLai);
            StyleComboBox(cbBox_HinhThucLai);
            StyleComboBox(cbBox_LoaiTaiSan);
            StyleDateTimePicker(dTimePicker_NgayVay);

            // Style các label đơn vị
            StyleDonViLabel(lb_DonVi_TongThoiGianVay);
            StyleDonViLabel(lb_DonVi_KyLai);
            StyleDonViLabel(lb_DonVi_Lai);
            StyleDonViLabel(lb_DonVi_TongSoTienVay);

            // Đảm bảo controls đã được add vào form (không thay đổi vị trí)
            if (!this.Controls.Contains(tbox_MaHD)) this.Controls.Add(tbox_MaHD);
            if (!this.Controls.Contains(tbox_Ten)) this.Controls.Add(tbox_Ten);
            if (!this.Controls.Contains(tb_TienVay)) this.Controls.Add(tb_TienVay);
            if (!this.Controls.Contains(tb_TongThoiGianVay)) this.Controls.Add(tb_TongThoiGianVay);
            if (!this.Controls.Contains(cbBox_HinhThucLai)) this.Controls.Add(cbBox_HinhThucLai);
            if (!this.Controls.Contains(cbBox_LoaiTaiSan)) this.Controls.Add(cbBox_LoaiTaiSan);
            if (!this.Controls.Contains(rtb_ThongtinTaiSan)) this.Controls.Add(rtb_ThongtinTaiSan);
            if (!this.Controls.Contains(rtb_DiaChi)) this.Controls.Add(rtb_DiaChi);
            if (!this.Controls.Contains(tb_NhanVienThuTien)) this.Controls.Add(tb_NhanVienThuTien);
            if (!this.Controls.Contains(tb_Lai)) this.Controls.Add(tb_Lai);
            if (!this.Controls.Contains(tb_KyLai)) this.Controls.Add(tb_KyLai);
            if (!this.Controls.Contains(rtb_GhiChu)) this.Controls.Add(rtb_GhiChu);
            if (!this.Controls.Contains(btn_Luu)) this.Controls.Add(btn_Luu);
            if (!this.Controls.Contains(btn_QuayLai)) this.Controls.Add(btn_QuayLai);
            if (!this.Controls.Contains(dTimePicker_NgayVay)) this.Controls.Add(dTimePicker_NgayVay);
        }
    }
}
