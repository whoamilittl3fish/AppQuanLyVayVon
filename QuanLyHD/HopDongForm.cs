using Microsoft.Data.Sqlite;
using QuanLyVayVon.CSDL;
using QuanLyVayVon.Models;
using System.Globalization;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing;
using System.Linq;
using Application = System.Windows.Forms.Application;
using Font = System.Drawing.Font;
namespace QuanLyVayVon.QuanLyHD
{
    public partial class HopDongForm : Form
    {
        public bool isThisEditMode = false; // Biến để xác định chế độ chỉnh sửa hay thêm mới
        public bool isThisReadOnly = false; // Biến để xác định chế độ chỉ đọc
        public HopDongForm(string? MaHD, bool isThisReadOnly)
        {
            InitializeComponent();
            this.AutoScaleMode = AutoScaleMode.Font;

            cbBox_HinhThucLai.Enabled = true;
            cbBox_LoaiTaiSan.Enabled = true;
            cbBox_HinhThucLai.DropDownStyle = ComboBoxStyle.DropDownList;
            cbBox_LoaiTaiSan.DropDownStyle = ComboBoxStyle.DropDownList;

            CustomizeUI();
            InitLoaiTaiSanComboBox();
            InitHinhThucLaiComboBox();


            if (MaHD != null)
            {
                isThisEditMode = true; // Chế độ chỉnh sửa
                LoadHopDong(MaHD);

            }
            else
            {
                LoadThemHopDong(); // Chế độ thêm mới
            }


            toolTip_KyLai.SetToolTip(lb_KyLai, "Kỳ lãi được tính theo đơn vị.\r\n10 (ngày) tương đương với kỳ hạn đóng lãi 10 ngày trả một lần.\r\n1 (tuần) tương đương với kỳ hạn 1 tuần = 7 ngày trả một lần.\r\n\r\nVD: \r\n*  Ngày 01/01/2025 vay, kỳ lãi là 3 ngày. Thì ngày 04/01/2025 sẽ phải đóng lãi.\r\n*  1 tuần đóng một lần thì sẽ nhập vào 1");
            toolTip_KyLai.SetToolTip(tb_KyLai, "Kỳ lãi được tính theo đơn vị.\r\n10 (ngày) tương đương với kỳ hạn đóng lãi 10 ngày trả một lần.\r\n1 (tuần) tương đương với kỳ hạn 1 tuần = 7 ngày trả một lần.\r\n\r\nVD: \r\n*  Ngày 01/01/2025 vay, kỳ lãi là 3 ngày. Thì ngày 04/01/2025 sẽ phải đóng lãi.\r\n1 tuần đóng một lần thì sẽ nhập vào 1");
            toolTip_KyLai.SetToolTip(lb_TongThoiGianVay, "Tổng thời gian vay được tính theo đơn vị.\r\n10 (ngày) tương đương với tổng thời gian vay là 10 ngày.\r\n1 (tuần) tương đương với tổng thời gian vay là 7 ngày.\r\n\r\nVD: \r\n*  Ngày 01/01/2025 vay, tổng thời gian vay là 3 ngày. Thì ngày 04/01/2025 sẽ hết hạn hợp đồng.\r\n");
            toolTip_KyLai.SetToolTip(tb_TongThoiGianVay, "Tổng thời gian vay được tính theo đơn vị.\r\n10 (ngày) tương đương với tổng thời gian vay là 10 ngày.\r\n1 (tuần) tương đương với tổng thời gian vay là 7 ngày.\r\n\r\nVD: \r\n*  Ngày 01/01/2025 vay, tổng thời gian vay là 3 ngày. Thì ngày 04/01/2025 sẽ hết hạn hợp đồng.\r\n");
            toolTip_KyLai.SetToolTip(tb_Lai, "Lãi tiền cố định theo đơn vị thời gian.\r\nVD: 1000 (VNĐ/ngày) sẽ là 1000 VNĐ mỗi ngày.\r\n1 (tuần) tương đương với 7000 VNĐ mỗi tuần.\r\n\r\nNếu lãi là phần trăm thì sẽ điền vào ô bên dưới.");
            toolTip_KyLai.SetToolTip(lb_Lai, "Lãi tiền cố định theo đơn vị thời gian.\r\nVD: 1000 (VNĐ/ngày) sẽ là 1000 VNĐ mỗi ngày.\r\n1 (tuần) tương đương với 7000 VNĐ mỗi tuần.\r\n\r\nNếu lãi là phần trăm thì sẽ điền vào ô bên dưới.");
            this.isThisReadOnly = isThisReadOnly;
        }
        private void LoadHopDong(string MaHD)
        {

            // Thiết lập chế độ chỉnh sửa
            isThisEditMode = true;
            if (MaHD == null)
            {
                MessageBox.Show("Không tìm thấy hợp đồng với mã: " + MaHD, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Lấy thông tin hợp đồng từ CSDL
            var hopDong = GetHopDongByMaHD(MaHD);

            if (hopDong == null)
            {
                MessageBox.Show("Không tìm thấy hợp đồng với mã: " + MaHD, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Title label
            var titleLabel = new Label
            {
                Text = "Sửa Hợp Đồng Vay Vốn",
                Font = new System.Drawing.Font("Montserrat", 18F, FontStyle.Bold, GraphicsUnit.Point),
                ForeColor = Color.FromArgb(44, 62, 80),
                AutoSize = true
            };

            // Center the title label horizontally at the top
            titleLabel.Location = new Point((this.ClientSize.Width - titleLabel.PreferredWidth) / 2, 15);
            titleLabel.Anchor = AnchorStyles.Top;

            this.Controls.Add(titleLabel);

            // Điền thông tin vào các trường
            tbox_MaHD.Text = hopDong.MaHD;
            tbox_Ten.Text = hopDong.TenKH;
            tbox_SDT.Text = hopDong.SDT;
            tbox_CCCD.Text = hopDong.CCCD;
            rtb_DiaChi.Text = hopDong.DiaChi;
            tb_TienVay.Text = hopDong.TienVay.ToString();
            cbBox_HinhThucLai.SelectedValue = hopDong.HinhThucLaiID;
            tb_TongThoiGianVay.Text = hopDong.SoNgayVay.ToString();
            dTimePicker_NgayVay.Value = DateTime.Parse(hopDong.NgayVay);
            tb_KyLai.Text = hopDong.KyDongLai.ToString();
            rtb_ThongtinTaiSan.Text = hopDong.TenTaiSan;
            tb1_ThongtinTaiSan.Text = hopDong.ThongTinTaiSan1;
            tb2_ThongtinTaiSan.Text = hopDong.ThongTinTaiSan2;
            tb3_ThongtinTaiSan.Text = hopDong.ThongTinTaiSan3;
            cbBox_LoaiTaiSan.SelectedValue = hopDong.LoaiTaiSanID;
            tb_NhanVienThuTien.Text = hopDong.NVThuTien;
            rtb_GhiChu.Text = hopDong.GhiChu;
            tb_Lai.Text = hopDong.Lai.ToString();

            tbox_MaHD.Enabled = false; // Không cho phép chỉnh sửa mã hợp đồng khi ở chế độ chỉnh sửa
            tbox_MaHD.BackColor = Color.LightGray; // Đổi màu nền để hiển thị là không thể chỉnh sửa
            toolTip_KyLai.SetToolTip(lb_MaHD, "Mã hợp đồng không thể chỉnh sửa trong chế độ chỉnh sửa.");
            toolTip_KyLai.SetToolTip(tbox_MaHD, "Mã hợp đồng không thể chỉnh sửa trong chế độ chỉnh sửa.");

            decimal lai = tb_Lai.Text == "" ? 0 : Convert.ToDecimal(tb_Lai.Text);
            string rawText = tb_TienVay.Text;
            decimal tienVay = 0;

            if (!string.IsNullOrWhiteSpace(rawText))
            {
                string cleaned = Function_Reuse.ExtractNumberString(rawText);
                decimal.TryParse(cleaned, NumberStyles.Number, CultureInfo.InvariantCulture, out tienVay);
            }


            decimal result = 0;
            switch (hopDong.HinhThucLaiID)
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
                    // Lãi %/ngày → chuyển đổi sang VNĐ/tháng
                    result = (int)((lai / 100) * tienVay * 30);
                    tb_ChuyenDoiLaiSuat.Text = result.ToString("F0") + " VNĐ/tháng";
                    break;
                case 5:
                    // Lãi %/tuần → chuyển đổi sang VNĐ/tháng (1 tuần = 7 ngày, 1 tháng ~ 30 ngày)
                    result = (int)((lai / 100) * tienVay * (30m / 7m));
                    tb_ChuyenDoiLaiSuat.Text = result.ToString("F0") + " VNĐ/tháng";
                    break;

                case 6:
                    result = (int)((lai * 30 / 100) * tienVay);
                    tb_ChuyenDoiLaiSuat.Text = result.ToString("F0") + " VNĐ/tháng";
                    break;
            }
        }

        public static HopDongModel GetHopDongByMaHD(string maHD)
        {
            string dbPath = Path.Combine(Application.StartupPath, "Database", "data.db");
            using (var connection = new SqliteConnection($"Data Source={dbPath}"))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM HopDongVay WHERE MaHD = @MaHD";
                command.Parameters.AddWithValue("@MaHD", maHD);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var hopDong = new HopDongModel
                        {
                            MaHD = reader["MaHD"].ToString(),
                            TenKH = reader["TenKH"].ToString(),
                            SDT = reader["SDT"].ToString(),
                            CCCD = reader["CCCD"].ToString(),
                            DiaChi = reader["DiaChi"].ToString(),
                            TienVay = Convert.ToDecimal(reader["TienVay"]),
                            Lai = reader.GetOrdinal("Lai") >= 0 ? Convert.ToDecimal(reader["Lai"]) : 0,
                            HinhThucLaiID = Convert.ToInt32(reader["HinhThucLaiID"]),
                            SoNgayVay = Convert.ToInt32(reader["SoNgayVay"]),
                            KyDongLai = Convert.ToInt32(reader["KyDongLai"]),
                            NgayVay = reader["NgayVay"].ToString(),
                            NgayHetHan = reader["NgayHetHan"].ToString(),
                            NgayDongLaiGanNhat = reader["NgayDongLaiGanNhat"].ToString(),
                            TinhTrang = Convert.ToInt32(reader["TinhTrang"]),
                            SoTienLaiMoiKy = Convert.ToDecimal(reader["SoTienLaiMoiKy"]),
                            SoTienLaiCuoiKy = Convert.ToDecimal(reader["SoTienLaiCuoiKy"]),
                            TenTaiSan = reader["TenTaiSan"].ToString(),
                            LoaiTaiSanID = Convert.ToInt32(reader["LoaiTaiSanID"]),
                            ThongTinTaiSan1 = reader["ThongTinTaiSan1"].ToString(),
                            ThongTinTaiSan2 = reader["ThongTinTaiSan2"].ToString(),
                            ThongTinTaiSan3 = reader["ThongTinTaiSan3"].ToString(),
                            NVThuTien = reader["NVThuTien"].ToString(),
                            GhiChu = reader["GhiChu"].ToString(),
                            CreatedAt = reader["CreatedAt"].ToString(),
                            UpdatedAt = reader["UpdatedAt"].ToString()
                        };

                        // Sau khi có hopDong, bạn có thể sử dụng hoặc gán lên form tùy ý
                        return hopDong;
                    }

                }

            }

            return null;
        }

        private void LoadThemHopDong()
        {

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

            dTimePicker_NgayVay.CustomFormat = "dd/MM/yyyy";
            dTimePicker_NgayVay.Value = DateTime.Now;

            cbBox_LoaiTaiSan.SelectedValue = 8;
            cbBox_HinhThucLai.SelectedValue = 1;

            Function_Reuse.ClearTextBoxOnClick(tbox_MaHD, "Nhập mã hợp đồng.");
            Function_Reuse.ClearTextBoxOnClick(tbox_Ten, "Nhập họ và tên khách hàng.");
            Function_Reuse.ClearTextBoxOnClick(tbox_SDT, "Nhập số điện thoại.");
            Function_Reuse.ClearTextBoxOnClick(tbox_CCCD, "Nhập số CCCD/hộ chiếu.");

            Function_Reuse.ClearTextBoxOnClick(tb_TongThoiGianVay, "Nhập tổng thời gian vay.");
            Function_Reuse.ClearRichTextBoxOnClick(rtb_ThongtinTaiSan, "Nhập thông tin tài sản, chi tiết tài sản (nếu có).");
            Function_Reuse.ClearRichTextBoxOnClick(rtb_DiaChi, "Nhập địa chỉ khách hàng");
            Function_Reuse.ClearTextBoxOnClick(tb_NhanVienThuTien, "Nhập tên nhân viên thu tiền.");
            Function_Reuse.ClearRichTextBoxOnClick(rtb_GhiChu, "Nhập ghi chú (nếu có)");
            Function_Reuse.ClearTextBoxOnClick(tb_Lai, "Nhập tiền lãi.");
            Function_Reuse.ClearTextBoxOnClick(tb_KyLai, "Nhập kỳ lãi.");

            string tb_TienVay_placeholder = "Nhập số tiền vay.";
            tb_TienVay.Text = tb_TienVay_placeholder;
            tb_TienVay.ForeColor = Color.Gray;

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
            if (Function_Reuse.ConfirmAndClose(this, "Bạn có chắc muốn quay lại không?") != DialogResult.Yes)
                return;

            // Tới đây là người dùng đã bấm "Yes" → xử lý tiếp
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

            // Đóng form hiện tại sau khi xử lý UI xong
            this.BeginInvoke((MethodInvoker)(() => this.Close()));


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

            string placeholder = "Nhập tiền lãi.";


            // Gắn sự kiện
            tb_Lai.KeyPress += Function_Reuse.OnlyAllowDigit_KeyPress;

            tb_Lai.Enter += (s, e) => Function_Reuse.ClearPlaceholderOnEnter(tb_Lai, placeholder);
            tb_Lai.Leave += (s, e) => Function_Reuse.SetPlaceholderIfEmpty(tb_Lai, placeholder);

            tb_Lai.TextChanged += (s, e) => Function_Reuse.FormatTextBoxWithThousands(tb_Lai, placeholder);

            if (string.IsNullOrWhiteSpace(tb_Lai.Text) || string.IsNullOrWhiteSpace(tb_TienVay.Text) ||
                tb_Lai.Text == "Nhập tiền lãi." || tb_TienVay.Text == "0")
            {
                tb_ChuyenDoiLaiSuat.Text = "0";
                return;
            }

            if (!decimal.TryParse(tb_TienVay.Text, out decimal tienVay))
            {
                MessageBox.Show("Tiền vay phải là một số hợp lệ.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!decimal.TryParse(tb_Lai.Text, out decimal lai))
            {
                MessageBox.Show("Lãi phải là một số hợp lệ.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            decimal result = 0;
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
                    result = (tienVay * (lai / 100)) * 30;
                    tb_ChuyenDoiLaiSuat.Text = result.ToString("F0") + " VNĐ/tháng (tính 30 ngày)";
                    break;
                case 5:
                    result = ((tienVay * (lai / 100)) / 7) * 30;
                    tb_ChuyenDoiLaiSuat.Text = result.ToString("F0") + " VNĐ/tháng (tính 30 ngày)";
                    break;
                case 6:
                    result = tienVay * (lai / 100);
                    tb_ChuyenDoiLaiSuat.Text = result.ToString("F0") + " VNĐ/tháng (tính 30 ngày)";
                    break;
            }
        }

        private void btn_Luu_Click(object sender, EventArgs e)
        {
            if (isThisEditMode == false)
                Function_Reuse.ConfirmAndClose(this, "Bạn có chắc muốn lưu hợp đồng này không?", "LƯU HỢP ĐỒNG MỚI");
            else
                Function_Reuse.ConfirmAndClose(this, "Tất cả thông tin sẽ được ghi lại dựa trên " +
                    "chỉnh sửa này. \r\n Bạn có chắc muốn cập nhật hợp đồng này không?", "CHỈNH SỬA HOÀN TẤT");

            if (this.DialogResult == DialogResult.No) return; // Nếu không xác nhận thì dừng lại
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

            decimal tienVay = decimal.TryParse(Function_Reuse.ExtractNumberString(tb_TienVay.Text), NumberStyles.Number, CultureInfo.InvariantCulture, out var value) ? value : 0;
            // Lấy tongThoiGianVay giống như cách lấy tienVay nhưng là int
            int tongThoiGianVay = int.TryParse(Function_Reuse.ExtractNumberString(tb_TongThoiGianVay.Text), NumberStyles.Number, CultureInfo.InvariantCulture, out var tongThoiGianValue) ? tongThoiGianValue : 0;
           
            int KyLai = int.TryParse(Function_Reuse.ExtractNumberString(tb_KyLai.Text), NumberStyles.Number, CultureInfo.InvariantCulture, out var kyLaiValue) ? kyLaiValue : 0;

            decimal Lai = decimal.TryParse(Function_Reuse.ExtractNumberString(tb_Lai.Text), NumberStyles.Number, CultureInfo.InvariantCulture, out var laiValue) ? laiValue : 0;
          

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

            // hàm kiể tra đầu vào
            if (!isValid)
            {
                // Hiển thị lỗi nếu có
                CustomMessageBox.ShowCustomMessageBox(errorMessages, null, "LỖI NHẬP LIỆU");
                return; // Dừng xử lý tiếp
            }

            //Xử lý thông tin combobox
            int? loaiTaiSanID = null;
            if (cbBox_LoaiTaiSan.SelectedValue != null && int.TryParse(cbBox_LoaiTaiSan.SelectedValue.ToString(), out int selectedID))
            {
                loaiTaiSanID = selectedID;
            }
            int hinhThucLaiID = 1;
            if (cbBox_HinhThucLai.SelectedValue != null && int.TryParse(cbBox_HinhThucLai.SelectedValue.ToString(), out int selectedHinhThucLaiID))
            {
                hinhThucLaiID = selectedHinhThucLaiID;
            }

            string dbPath = Path.Combine(Application.StartupPath, "Database", "data.db");

            if (!File.Exists(dbPath))
            {
                CustomMessageBox.ShowCustomMessageBox("Không tìm thấy cơ sở dữ liệu. Vui lòng kiểm tra lại.", null, "LỖI CƠ SỞ DỮ LIỆU");
                return;
            }

            using (var connection = new SqliteConnection($"Data Source={dbPath}"))
            {
                connection.Open();

                var checkCmd = connection.CreateCommand();
                checkCmd.CommandText = "SELECT COUNT(*) FROM HopDongVay WHERE MaHD = @MaHD";
                checkCmd.Parameters.AddWithValue("@MaHD", MaHD);

                long count = Convert.ToInt64(checkCmd.ExecuteScalar() ?? 0);
                if (count > 0 && isThisEditMode == false)
                {
                    CustomMessageBox.ShowCustomMessageBox("Mã hợp đồng đã tồn tại. Vui lòng nhập mã khác.", null, "TRÙNG MÃ HỢP ĐỒNG");
                    return;
                }

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        if (isThisEditMode)
                        {
                            // Xoá dữ liệu cũ trước khi thêm lại
                            var deleteLichSuCmd = connection.CreateCommand();
                            deleteLichSuCmd.Transaction = transaction;
                            deleteLichSuCmd.CommandText = "DELETE FROM LichSuDongLai WHERE MaHD = @MaHD";
                            deleteLichSuCmd.Parameters.AddWithValue("@MaHD", MaHD);
                            deleteLichSuCmd.ExecuteNonQuery();

                            var deleteHopDongCmd = connection.CreateCommand();
                            deleteHopDongCmd.Transaction = transaction;
                            deleteHopDongCmd.CommandText = "DELETE FROM HopDongVay WHERE MaHD = @MaHD";
                            deleteHopDongCmd.Parameters.AddWithValue("@MaHD", MaHD);
                            deleteHopDongCmd.ExecuteNonQuery();
                        }

                        DateTime ngayVay = dTimePicker_NgayVay.Value.Date;
                        DateTime ngayHetHan = ngayVay.AddDays(tongThoiGianVay);
                        string ngayVayStr = ngayVay.ToString("yyyy-MM-dd");
                        string ngayHetHanStr = ngayHetHan.ToString("yyyy-MM-dd");

                        var kq = TinhLaiHopDong(tienVay, Lai, tongThoiGianVay, hinhThucLaiID, KyLai, ngayVay);

                        var insertCmd = connection.CreateCommand();
                        insertCmd.Transaction = transaction;
                        insertCmd.CommandText = @"
                        INSERT INTO HopDongVay (
                            MaHD, TenKH, SDT, CCCD, DiaChi,
                            TienVay, LoaiTaiSanID,
                            NgayVay, NgayHetHan, KyDongLai, HinhThucLaiID, SoNgayVay, GhiChu,
                            TenTaiSan, ThongTinTaiSan1, ThongTinTaiSan2, ThongTinTaiSan3, NVThuTien, Lai, 
                            SoTienLaiMoiKy, SoTienLaiCuoiKy, TongLai,
                            CreatedAt
                        )
                        VALUES (
                            @MaHD, @TenKH, @SDT, @CCCD, @DiaChi,
                            @TienVay, @LoaiTaiSanID,
                            @NgayVay, @NgayHetHan, @KyDongLai, @HinhThucLaiID, @SoNgayVay, @GhiChu,
                            @TenTaiSan, @ThongTinTaiSan1, @ThongTinTaiSan2, @ThongTinTaiSan3, @NVThuTien, @Lai, @SoTienLaiMoiKy,
                            @SoTienLaiCuoiKy, @TongLai,
                            CURRENT_TIMESTAMP
                        );
                    ";
                        insertCmd.Parameters.AddWithValue("@MaHD", MaHD);
                        insertCmd.Parameters.AddWithValue("@TenKH", TenKH);
                        insertCmd.Parameters.AddWithValue("@SDT", SDT);
                        insertCmd.Parameters.AddWithValue("@CCCD", CCCD);
                        insertCmd.Parameters.AddWithValue("@DiaChi", DiaChi);
                        insertCmd.Parameters.AddWithValue("@TienVay", tienVay);
                        insertCmd.Parameters.AddWithValue("@LoaiTaiSanID", loaiTaiSanID);
                        insertCmd.Parameters.AddWithValue("@NgayVay", ngayVayStr);
                        insertCmd.Parameters.AddWithValue("@NgayHetHan", ngayHetHanStr);
                        insertCmd.Parameters.AddWithValue("@KyDongLai", KyLai);
                        insertCmd.Parameters.AddWithValue("@Lai", Lai);
                        insertCmd.Parameters.AddWithValue("@HinhThucLaiID", hinhThucLaiID);
                        insertCmd.Parameters.AddWithValue("@SoNgayVay", tongThoiGianVay);
                        insertCmd.Parameters.AddWithValue("@GhiChu", GhiChu ?? "");
                        insertCmd.Parameters.AddWithValue("@TenTaiSan", TenTaiSan ?? "");
                        insertCmd.Parameters.AddWithValue("@ThongTinTaiSan1", ThongTinTaiSan1 ?? "");
                        insertCmd.Parameters.AddWithValue("@ThongTinTaiSan2", ThongTinTaiSan2 ?? "");
                        insertCmd.Parameters.AddWithValue("@ThongTinTaiSan3", ThongTinTaiSan3 ?? "");
                        insertCmd.Parameters.AddWithValue("@NVThuTien", NhanVienTT ?? "");
                        insertCmd.Parameters.AddWithValue("@SoTienLaiMoiKy", kq.TienLaiMoiKy);
                        insertCmd.Parameters.AddWithValue("@SoTienLaiCuoiKy", kq.TienLaiCuoiKy);
                        insertCmd.Parameters.AddWithValue("@TongLai", kq.TongLai);
                        insertCmd.ExecuteNonQuery();

                        if (loaiTaiSanID >= 1 && loaiTaiSanID <= 4)
                        {
                            string tt1 = tb1_ThongtinTaiSan.Text.Trim();
                            string tt2 = tb2_ThongtinTaiSan.Text.Trim();
                            string tt3 = tb3_ThongtinTaiSan.Text.Trim();

                            var updateCmd = connection.CreateCommand();
                            updateCmd.Transaction = transaction;
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

                        var insertLaiCmd = connection.CreateCommand();
                        insertLaiCmd.Transaction = transaction;
                        insertLaiCmd.CommandText = @"
                        INSERT INTO LichSuDongLai (
                            MaHD, KyThu, NgayBatDauKy, NgayDenHan, SoTienPhaiDong
                        ) VALUES (
                            @MaHD, @KyThu, @NgayBatDauKy, @NgayDenHan, @SoTienPhaiDong
                        );
                    ";

                        for (int i = 0; i < kq.LichDongLai.Count; i++)
                        {
                            var ky = kq.LichDongLai[i];
                            insertLaiCmd.Parameters.Clear();
                            insertLaiCmd.Parameters.AddWithValue("@MaHD", MaHD);
                            insertLaiCmd.Parameters.AddWithValue("@KyThu", i + 1);
                            insertLaiCmd.Parameters.AddWithValue("@NgayBatDauKy", ky.NgayBatDau.ToString("yyyy-MM-dd"));
                            insertLaiCmd.Parameters.AddWithValue("@NgayDenHan", ky.NgayKetThuc.ToString("yyyy-MM-dd"));
                            insertLaiCmd.Parameters.AddWithValue("@SoTienPhaiDong", (i == kq.LichDongLai.Count - 1) ? kq.TienLaiCuoiKy : kq.TienLaiMoiKy);
                            insertLaiCmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        CustomMessageBox.ShowCustomMessageBox("Hợp đồng đã được lưu thành công.", null, "THÀNH CÔNG");
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        CustomMessageBox.ShowCustomMessageBox("Đã xảy ra lỗi khi lưu hợp đồng: " + ex.Message, null, "LỖI");
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        private void tb_TongThoiGianVay_TextChanged(object sender, EventArgs e)
        {
            string placeholder = "Nhập tổng thời gian vay.";


            // Gắn sự kiện
            tb_TongThoiGianVay.KeyPress += Function_Reuse.OnlyAllowDigit_KeyPress;

            tb_TongThoiGianVay.Enter += (s, e) => Function_Reuse.ClearPlaceholderOnEnter(tb_TongThoiGianVay, placeholder);
            tb_TongThoiGianVay.Leave += (s, e) => Function_Reuse.SetPlaceholderIfEmpty(tb_TongThoiGianVay, placeholder);

            tb_TongThoiGianVay.TextChanged += (s, e) => Function_Reuse.FormatTextBoxWithThousands(tb_TongThoiGianVay, placeholder);
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
            decimal tmp_Tien;
            string TongThoiGianVay = Function_Reuse.ExtractNumberString(tb_TongThoiGianVay.Text.Trim());
            if (!int.TryParse(TongThoiGianVay, NumberStyles.Number, CultureInfo.InvariantCulture, out tmp) || tb_TongThoiGianVay.Text == "Nhập tổng thời gian vay.")
            {
                tb_TongThoiGianVay.BackColor = Color.MediumVioletRed;
                err += "Tổng thời gian vay trống hoặc không hợp lệ.\r\n";
            }
       
            string tienVayStr = Function_Reuse.ExtractNumberString(tb_TienVay.Text.Trim());
            if (!decimal.TryParse(tienVayStr, NumberStyles.Number, CultureInfo.InvariantCulture, out tmp_Tien) || tb_TienVay.Text == "Nhập số tiền vay.")
            {
                tb_TienVay.BackColor = Color.MediumVioletRed;
                err += "Tiền vay trống hoặc không hợp lệ.\r\n";
            }

            string KyLaiStr = Function_Reuse.ExtractNumberString(tb_KyLai.Text.Trim());        
            if (!int.TryParse(KyLaiStr, NumberStyles.Number, CultureInfo.InvariantCulture , out tmp) || tb_KyLai.Text == "Nhập kỳ lãi." )
            {
                tb_KyLai.BackColor = Color.MediumVioletRed;
                err += "Kỳ lãi trống hoặc không hợp lệ.\r\n";
            }
            string LaiStr = Function_Reuse.ExtractNumberString(tb_Lai.Text.Trim());
            if (!decimal.TryParse(LaiStr, NumberStyles.Number, CultureInfo.InvariantCulture, out tmp_Tien) || tb_Lai.Text == "Nhập tiền lãi.")
            {
                tb_Lai.BackColor = Color.MediumVioletRed;
                err += "Lãi trống hoặc không hợp lệ.\r\n";
            }

            return string.IsNullOrEmpty(err);
        }

        private void OnlyAllowDigit_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Chỉ cho phép số và phím điều khiển (Backspace, Delete,...)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void tbox_MaHD_TextChanged(object sender, EventArgs e)
        {
            tbox_MaHD.BackColor = Color.White; // Đặt lại màu nền khi người dùng bắt đầu nhập
        }

        private void tb_TienVay_TextChanged(object sender, EventArgs e)
        {

         

            string placeholder = "Nhập số tiền vay.";

          
            // Gắn sự kiện
            tb_TienVay.KeyPress += Function_Reuse.OnlyAllowDigit_KeyPress;

            tb_TienVay.Enter += (s, e) => Function_Reuse.ClearPlaceholderOnEnter(tb_TienVay, placeholder);
            tb_TienVay.Leave += (s, e) => Function_Reuse.SetPlaceholderIfEmpty(tb_TienVay, placeholder);

            tb_TienVay.TextChanged += (s, e) => Function_Reuse.FormatTextBoxWithThousands(tb_TienVay, placeholder);



        }

        private void tb_KyLai_TextChanged(object sender, EventArgs e)
        {
            string placeholder = "Nhập kỳ lãi.";


            // Gắn sự kiện
            tb_KyLai.KeyPress += Function_Reuse.OnlyAllowDigit_KeyPress;

            tb_KyLai.Enter += (s, e) => Function_Reuse.ClearPlaceholderOnEnter(tb_KyLai, placeholder);
            tb_KyLai.Leave += (s, e) => Function_Reuse.SetPlaceholderIfEmpty(tb_KyLai, placeholder);

            tb_KyLai.TextChanged += (s, e) => Function_Reuse.FormatTextBoxWithThousands(tb_KyLai, placeholder);
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
            public decimal TongLai { get; set; }
            public decimal TienLaiMoiKy { get; set; }
            public decimal TienLaiCuoiKy { get; set; }
            public int SoKy { get; set; }
            public int ThoiGianKyLaiCuoi { get; set; }

            public List<KyDongLai> LichDongLai { get; set; } // Danh sách các kỳ với ngày bắt đầu và kết thúc
        }


        public class KyDongLai
        {
            public DateTime NgayBatDau { get; set; }
            public DateTime NgayKetThuc { get; set; }
        }




        public static KetQuaTinhLai TinhLaiHopDong(
            decimal tienVay, decimal laiNhap, int TongThoiGianVay, int hinhThucLaiID, int kyDongLai, DateTime ngayVay)
        {
            if (kyDongLai <= 0)
                throw new ArgumentException("Kỳ đóng lãi phải lớn hơn 0", nameof(kyDongLai));
            if (TongThoiGianVay <= 0)
                throw new ArgumentException("Tổng thời gian vay phải lớn hơn 0", nameof(TongThoiGianVay));

            var hinhThuc = HinhThucLaiHelper.GetHinhThucLaiInfo(hinhThucLaiID);
            if (hinhThuc == null)
                throw new Exception("Không tìm thấy loại hình thức lãi phù hợp");

            // Xác định số ngày cho mỗi kỳ đóng lãi
            int donViNgay = 1;
            if (hinhThucLaiID == 2 || hinhThucLaiID == 5)
                donViNgay = 7;
            else if (hinhThucLaiID == 3 || hinhThucLaiID == 6)
                donViNgay = 30;

            int soKy = (int)Math.Ceiling((decimal)TongThoiGianVay / kyDongLai);
            if (soKy > 1000)
                throw new Exception("Số kỳ đóng lãi vượt quá giới hạn cho phép");

            List<KyDongLai> dsKyDongLai = new();
            List<decimal> tienLaiTungKy = new();
            int ngayConLai = TongThoiGianVay;
            DateTime ngayBatDau = ngayVay;

            for (int i = 0; i < soKy; i++)
            {
                int soNgayKy = Math.Min(kyDongLai, ngayConLai);
                int soNgayCong = donViNgay * soNgayKy;
                DateTime ngayKetThuc = ngayBatDau.AddDays(soNgayCong);

                dsKyDongLai.Add(new KyDongLai
                {
                    NgayBatDau = ngayBatDau,
                    NgayKetThuc = ngayKetThuc
                });

                decimal tienLaiKy = 0;
                if (hinhThuc.LoaiLai == "tienmat")
                {
                    // lãi * số kỳ * đơn vị ngày
                    tienLaiKy = laiNhap * soNgayKy;
                }
                else if (hinhThuc.LoaiLai == "phantram")
                {
                    // phần trăm * số kỳ * đơn vị ngày
                    tienLaiKy = tienVay * (laiNhap / 100m) * soNgayKy;
                }
                tienLaiKy = Math.Ceiling(tienLaiKy / 1000m) * 1000;
                tienLaiTungKy.Add(tienLaiKy);

                ngayBatDau = ngayKetThuc;
                ngayConLai -= soNgayKy;
            }

            decimal tongLai;
            if (hinhThucLaiID == 4 || hinhThucLaiID == 5 || hinhThucLaiID == 6)
            {
                tongLai = tienVay * (laiNhap / 100m) * TongThoiGianVay;
                tongLai = Math.Ceiling(tongLai / 1000m) * 1000;
            }
            else
            {
                tongLai = laiNhap * TongThoiGianVay;
                tongLai = Math.Ceiling(tongLai / 1000m) * 1000;
            }

            decimal tienLaiMoiKy = tienLaiTungKy.Count > 1 ? tienLaiTungKy[0] : tienLaiTungKy[0];
            decimal tienLaiCuoiKy = tienLaiTungKy.Last();
            MessageBox.Show(tongLai.ToString());
            return new KetQuaTinhLai
            {
                TongLai = tongLai,
                TienLaiMoiKy = tienLaiMoiKy,
                TienLaiCuoiKy = tienLaiCuoiKy,
                SoKy = soKy,
                ThoiGianKyLaiCuoi = ngayConLai > 0 ? ngayConLai : (tienLaiTungKy.Count > 1 ? 1 : TongThoiGianVay),
                LichDongLai = dsKyDongLai,
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




            // Font đẹp hơn cho toàn bộ form (không in nghiêng)
            System.Drawing.Font mainFont = new System.Drawing.Font("Montserrat", 12.5F, FontStyle.Regular, GraphicsUnit.Point);
            System.Drawing.Font mainFontBold = new System.Drawing.Font("Montserrat", 13.5F, FontStyle.Bold, GraphicsUnit.Point);
            System.Drawing.Font donViFont = new System.Drawing.Font("Montserrat", 11F, FontStyle.Regular, GraphicsUnit.Point);
            System.Drawing.Font dateTimeFont = new System.Drawing.Font("Montserrat", 12.5F, FontStyle.Regular, GraphicsUnit.Point);

            // chỉnh màu rtb và label
            Color richTextBoxBackColor = Color.FromArgb(248, 250, 255);
            Color richTextBoxBorderColor = Color.FromArgb(200, 215, 240);
            Color donViLabelBackColor = Color.FromArgb(225, 240, 255);
            Color donViLabelForeColor = Color.FromArgb(30, 90, 160);


            // Thay thế hàm StyleTextBox trong CustomizeUI bằng phiên bản tự động scale chiều cao theo chữ
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

            // Apply docking and anchoring to tableLayoutPanel1
            tableLayoutPanel1.Dock = DockStyle.None; // hoặc DockStyle.Fill nếu muốn chiếm toàn bộ
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Left; // hoặc thêm Bottom/Right nếu cần
            tableLayoutPanel2.Dock = DockStyle.None; // hoặc DockStyle.Fill nếu muốn chiếm toàn bộ
            tableLayoutPanel2.Anchor = AnchorStyles.Top | AnchorStyles.Left; // hoặc thêm Bottom/Right nếu cần
        }

        private void tb_ChuyenDoiLaiSuat_TextChanged(object sender, EventArgs e)
        {

        }
        private void HopDongForm_Load(object sender, EventArgs e)
        {
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }
        // Thay thế tất cả các dòng gắn sự kiện KeyPress cho các TextBox số (tb_Lai, tb_TienVay, tb_TongThoiGianVay, tb_KyLai)
        // từ: tb_Lai.KeyPress += Function_Reuse.OnlyAllowDigit_KeyPress;
        // thành: tb_Lai.KeyPress += OnlyAllowDigitAndDot_KeyPress;
        // và tương tự cho các textbox khác

       
    }
}
