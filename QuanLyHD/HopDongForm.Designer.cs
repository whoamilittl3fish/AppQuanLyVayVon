namespace QuanLyVayVon.QuanLyHD
{
    partial class HopDongForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HopDongForm));
            label1 = new Label();
            pictureBox1 = new PictureBox();
            label2 = new Label();
            tbox_Ten = new TextBox();
            label3 = new Label();
            tbox_MaHD = new TextBox();
            tbox_CCCD = new TextBox();
            label5 = new Label();
            label6 = new Label();
            label4 = new Label();
            tbox_SDT = new TextBox();
            rtb_DiaChi = new RichTextBox();
            pictureBox2 = new PictureBox();
            label7 = new Label();
            label8 = new Label();
            cbBox_LoaiTaiSan = new ComboBox();
            btn_QuayLai = new Button();
            label9 = new Label();
            label10 = new Label();
            tb_TienVay = new TextBox();
            label11 = new Label();
            cbBox_HinhThucLai = new ComboBox();
            lb_TongThoiGianVay = new Label();
            tb_TongThoiGianVay = new TextBox();
            lb_DonVi_TongSoTienVay = new Label();
            lb_DonVi_TongThoiGianVay = new Label();
            lb_KyLai = new Label();
            tb_KyLai = new TextBox();
            lb_DonVi_KyLai = new Label();
            lb_Lai = new Label();
            tb_Lai = new TextBox();
            lb_DonVi_Lai = new Label();
            label13 = new Label();
            dTimePicker_NgayVay = new DateTimePicker();
            label15 = new Label();
            rtb_GhiChu = new RichTextBox();
            label16 = new Label();
            tb_NhanVienThuTien = new TextBox();
            toolTip_KyLai = new ToolTip(components);
            tb_ChuyenDoiLaiSuat = new TextBox();
            lb1_ThongtinTaiSan = new Label();
            tb1_ThongtinTaiSan = new TextBox();
            tb2_ThongtinTaiSan = new TextBox();
            lb2_ThongtinTaiSan = new Label();
            tb3_ThongtinTaiSan = new TextBox();
            lb3_ThongtinTaiSan = new Label();
            rtb_ThongtinTaiSan = new RichTextBox();
            lb_TinhLai = new Label();
            btn_Luu = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Arial", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.Location = new Point(103, 48);
            label1.Margin = new Padding(5, 0, 5, 0);
            label1.Name = "label1";
            label1.Size = new Size(163, 19);
            label1.TabIndex = 0;
            label1.Text = "Thông tin khách hàng";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(44, 37);
            pictureBox1.Margin = new Padding(5, 2, 5, 2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(51, 40);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(104, 151);
            label2.Margin = new Padding(5, 0, 5, 0);
            label2.Name = "label2";
            label2.Size = new Size(142, 19);
            label2.TabIndex = 2;
            label2.Text = "Tên khách hàng *";
            // 
            // tbox_Ten
            // 
            tbox_Ten.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tbox_Ten.Location = new Point(300, 147);
            tbox_Ten.Margin = new Padding(4, 2, 4, 2);
            tbox_Ten.Name = "tbox_Ten";
            tbox_Ten.Size = new Size(284, 26);
            tbox_Ten.TabIndex = 2;
            tbox_Ten.TextAlign = HorizontalAlignment.Center;
            tbox_Ten.TextChanged += tbox_Ten_TextChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(106, 105);
            label3.Margin = new Padding(5, 0, 5, 0);
            label3.Name = "label3";
            label3.Size = new Size(120, 19);
            label3.TabIndex = 5;
            label3.Text = "Mã hợp đồng *";
            // 
            // tbox_MaHD
            // 
            tbox_MaHD.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tbox_MaHD.Location = new Point(300, 98);
            tbox_MaHD.Margin = new Padding(4, 2, 4, 2);
            tbox_MaHD.Name = "tbox_MaHD";
            tbox_MaHD.Size = new Size(285, 26);
            tbox_MaHD.TabIndex = 1;
            tbox_MaHD.TextAlign = HorizontalAlignment.Center;
            tbox_MaHD.TextChanged += tbox_MaHD_TextChanged;
            // 
            // tbox_CCCD
            // 
            tbox_CCCD.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tbox_CCCD.Location = new Point(945, 147);
            tbox_CCCD.Margin = new Padding(4, 2, 4, 2);
            tbox_CCCD.Name = "tbox_CCCD";
            tbox_CCCD.Size = new Size(285, 26);
            tbox_CCCD.TabIndex = 5;
            tbox_CCCD.TextAlign = HorizontalAlignment.Center;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Arial", 12F, FontStyle.Bold);
            label5.Location = new Point(739, 105);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(42, 19);
            label5.TabIndex = 9;
            label5.Text = "SĐT";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.Location = new Point(106, 194);
            label6.Margin = new Padding(5, 0, 5, 0);
            label6.Name = "label6";
            label6.Size = new Size(61, 19);
            label6.TabIndex = 11;
            label6.Text = "Địa chỉ";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(739, 151);
            label4.Margin = new Padding(5, 0, 5, 0);
            label4.Name = "label4";
            label4.Size = new Size(158, 19);
            label4.TabIndex = 12;
            label4.Text = "Số CCCD/ Hộ chiếu";
            // 
            // tbox_SDT
            // 
            tbox_SDT.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tbox_SDT.Location = new Point(945, 98);
            tbox_SDT.Margin = new Padding(4, 2, 4, 2);
            tbox_SDT.Name = "tbox_SDT";
            tbox_SDT.Size = new Size(285, 26);
            tbox_SDT.TabIndex = 4;
            tbox_SDT.TextAlign = HorizontalAlignment.Center;
            // 
            // rtb_DiaChi
            // 
            rtb_DiaChi.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            rtb_DiaChi.Location = new Point(300, 194);
            rtb_DiaChi.Margin = new Padding(4, 3, 4, 3);
            rtb_DiaChi.Name = "rtb_DiaChi";
            rtb_DiaChi.Size = new Size(285, 81);
            rtb_DiaChi.TabIndex = 3;
            rtb_DiaChi.Text = "";
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(44, 286);
            pictureBox2.Margin = new Padding(5, 2, 5, 2);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(51, 45);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 16;
            pictureBox2.TabStop = false;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Arial", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label7.Location = new Point(106, 310);
            label7.Margin = new Padding(5, 0, 5, 0);
            label7.Name = "label7";
            label7.Size = new Size(153, 19);
            label7.TabIndex = 15;
            label7.Text = "Thông tin khoản vay";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label8.Location = new Point(106, 367);
            label8.Margin = new Padding(5, 0, 5, 0);
            label8.Name = "label8";
            label8.Size = new Size(106, 19);
            label8.TabIndex = 18;
            label8.Text = "Loại tài sản *";
            // 
            // cbBox_LoaiTaiSan
            // 
            cbBox_LoaiTaiSan.FormattingEnabled = true;
            cbBox_LoaiTaiSan.Location = new Point(300, 367);
            cbBox_LoaiTaiSan.Name = "cbBox_LoaiTaiSan";
            cbBox_LoaiTaiSan.Size = new Size(285, 26);
            cbBox_LoaiTaiSan.TabIndex = 6;
            cbBox_LoaiTaiSan.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // btn_QuayLai
            // 
            btn_QuayLai.Location = new Point(401, 821);
            btn_QuayLai.Name = "btn_QuayLai";
            btn_QuayLai.Size = new Size(95, 41);
            btn_QuayLai.TabIndex = 21;
            btn_QuayLai.Text = "Quay lại";
            btn_QuayLai.UseVisualStyleBackColor = true;
            btn_QuayLai.Click += btn_QuayLai_Click;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.Location = new Point(103, 418);
            label9.Margin = new Padding(5, 0, 5, 0);
            label9.Name = "label9";
            label9.Size = new Size(145, 19);
            label9.TabIndex = 22;
            label9.Text = "Tổng số tiền vay *";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label10.Location = new Point(739, 374);
            label10.Margin = new Padding(5, 0, 5, 0);
            label10.Name = "label10";
            label10.Size = new Size(146, 19);
            label10.TabIndex = 23;
            label10.Text = "Thông tin tài sản *";
            // 
            // tb_TienVay
            // 
            tb_TienVay.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tb_TienVay.Location = new Point(300, 411);
            tb_TienVay.Margin = new Padding(4, 2, 4, 2);
            tb_TienVay.Name = "tb_TienVay";
            tb_TienVay.Size = new Size(285, 26);
            tb_TienVay.TabIndex = 7;
            tb_TienVay.TextAlign = HorizontalAlignment.Center;
            tb_TienVay.TextChanged += tb_TienVay_TextChanged;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label11.Location = new Point(106, 465);
            label11.Margin = new Padding(5, 0, 5, 0);
            label11.Name = "label11";
            label11.Size = new Size(116, 19);
            label11.TabIndex = 26;
            label11.Text = "Hình thức lãi *";
            // 
            // cbBox_HinhThucLai
            // 
            cbBox_HinhThucLai.FormattingEnabled = true;
            cbBox_HinhThucLai.Location = new Point(300, 462);
            cbBox_HinhThucLai.Name = "cbBox_HinhThucLai";
            cbBox_HinhThucLai.Size = new Size(285, 26);
            cbBox_HinhThucLai.TabIndex = 8;
            cbBox_HinhThucLai.SelectedIndexChanged += comboBox1_SelectedIndexChanged_1;
            // 
            // lb_TongThoiGianVay
            // 
            lb_TongThoiGianVay.AutoSize = true;
            lb_TongThoiGianVay.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lb_TongThoiGianVay.Location = new Point(106, 509);
            lb_TongThoiGianVay.Margin = new Padding(5, 0, 5, 0);
            lb_TongThoiGianVay.Name = "lb_TongThoiGianVay";
            lb_TongThoiGianVay.Size = new Size(161, 19);
            lb_TongThoiGianVay.TabIndex = 28;
            lb_TongThoiGianVay.Text = "Tổng thời gian vay *";
            lb_TongThoiGianVay.Click += lb_TongThoiGianVay_Click;
            // 
            // tb_TongThoiGianVay
            // 
            tb_TongThoiGianVay.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tb_TongThoiGianVay.Location = new Point(299, 509);
            tb_TongThoiGianVay.Margin = new Padding(4, 2, 4, 2);
            tb_TongThoiGianVay.Name = "tb_TongThoiGianVay";
            tb_TongThoiGianVay.Size = new Size(285, 26);
            tb_TongThoiGianVay.TabIndex = 9;
            tb_TongThoiGianVay.TextAlign = HorizontalAlignment.Center;
            tb_TongThoiGianVay.TextChanged += tb_TongThoiGianVay_TextChanged;
            // 
            // lb_DonVi_TongSoTienVay
            // 
            lb_DonVi_TongSoTienVay.AutoSize = true;
            lb_DonVi_TongSoTienVay.BackColor = SystemColors.ButtonHighlight;
            lb_DonVi_TongSoTienVay.BorderStyle = BorderStyle.Fixed3D;
            lb_DonVi_TongSoTienVay.FlatStyle = FlatStyle.Flat;
            lb_DonVi_TongSoTienVay.Font = new Font("Arial", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lb_DonVi_TongSoTienVay.Location = new Point(592, 411);
            lb_DonVi_TongSoTienVay.Name = "lb_DonVi_TongSoTienVay";
            lb_DonVi_TongSoTienVay.Size = new Size(54, 26);
            lb_DonVi_TongSoTienVay.TabIndex = 30;
            lb_DonVi_TongSoTienVay.Text = "VND";
            // 
            // lb_DonVi_TongThoiGianVay
            // 
            lb_DonVi_TongThoiGianVay.AutoSize = true;
            lb_DonVi_TongThoiGianVay.BackColor = SystemColors.ButtonHighlight;
            lb_DonVi_TongThoiGianVay.BorderStyle = BorderStyle.Fixed3D;
            lb_DonVi_TongThoiGianVay.FlatStyle = FlatStyle.Flat;
            lb_DonVi_TongThoiGianVay.Font = new Font("Arial", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lb_DonVi_TongThoiGianVay.Location = new Point(592, 509);
            lb_DonVi_TongThoiGianVay.Name = "lb_DonVi_TongThoiGianVay";
            lb_DonVi_TongThoiGianVay.Size = new Size(60, 26);
            lb_DonVi_TongThoiGianVay.TabIndex = 31;
            lb_DonVi_TongThoiGianVay.Text = "Ngày";
            // 
            // lb_KyLai
            // 
            lb_KyLai.AutoSize = true;
            lb_KyLai.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lb_KyLai.Location = new Point(106, 555);
            lb_KyLai.Margin = new Padding(5, 0, 5, 0);
            lb_KyLai.Name = "lb_KyLai";
            lb_KyLai.Size = new Size(61, 19);
            lb_KyLai.TabIndex = 32;
            lb_KyLai.Text = "Kỳ lãi *";
            // 
            // tb_KyLai
            // 
            tb_KyLai.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tb_KyLai.Location = new Point(300, 555);
            tb_KyLai.Margin = new Padding(4, 2, 4, 2);
            tb_KyLai.Name = "tb_KyLai";
            tb_KyLai.Size = new Size(285, 26);
            tb_KyLai.TabIndex = 10;
            tb_KyLai.TextAlign = HorizontalAlignment.Center;
            tb_KyLai.TextChanged += tb_KyLai_TextChanged;
            // 
            // lb_DonVi_KyLai
            // 
            lb_DonVi_KyLai.AutoSize = true;
            lb_DonVi_KyLai.BackColor = SystemColors.ButtonHighlight;
            lb_DonVi_KyLai.BorderStyle = BorderStyle.Fixed3D;
            lb_DonVi_KyLai.FlatStyle = FlatStyle.Flat;
            lb_DonVi_KyLai.Font = new Font("Arial", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lb_DonVi_KyLai.Location = new Point(592, 555);
            lb_DonVi_KyLai.Name = "lb_DonVi_KyLai";
            lb_DonVi_KyLai.Size = new Size(60, 26);
            lb_DonVi_KyLai.TabIndex = 34;
            lb_DonVi_KyLai.Text = "Ngày";
            // 
            // lb_Lai
            // 
            lb_Lai.AutoSize = true;
            lb_Lai.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lb_Lai.Location = new Point(106, 597);
            lb_Lai.Margin = new Padding(5, 0, 5, 0);
            lb_Lai.Name = "lb_Lai";
            lb_Lai.Size = new Size(42, 19);
            lb_Lai.TabIndex = 35;
            lb_Lai.Text = "Lãi *";
            // 
            // tb_Lai
            // 
            tb_Lai.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tb_Lai.Location = new Point(299, 597);
            tb_Lai.Margin = new Padding(4, 2, 4, 2);
            tb_Lai.Name = "tb_Lai";
            tb_Lai.Size = new Size(285, 26);
            tb_Lai.TabIndex = 11;
            tb_Lai.TextAlign = HorizontalAlignment.Center;
            tb_Lai.TextChanged += tb_Lai_TextChanged;
            // 
            // lb_DonVi_Lai
            // 
            lb_DonVi_Lai.AutoSize = true;
            lb_DonVi_Lai.BackColor = SystemColors.ButtonHighlight;
            lb_DonVi_Lai.BorderStyle = BorderStyle.Fixed3D;
            lb_DonVi_Lai.FlatStyle = FlatStyle.Flat;
            lb_DonVi_Lai.Font = new Font("Arial", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lb_DonVi_Lai.Location = new Point(592, 597);
            lb_DonVi_Lai.Name = "lb_DonVi_Lai";
            lb_DonVi_Lai.Size = new Size(108, 26);
            lb_DonVi_Lai.TabIndex = 37;
            lb_DonVi_Lai.Text = "VNĐ/Ngày";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label13.Location = new Point(106, 633);
            label13.Margin = new Padding(5, 0, 5, 0);
            label13.Name = "label13";
            label13.Size = new Size(90, 19);
            label13.TabIndex = 38;
            label13.Text = "Ngày vay *";
            // 
            // dTimePicker_NgayVay
            // 
            dTimePicker_NgayVay.Location = new Point(299, 633);
            dTimePicker_NgayVay.Name = "dTimePicker_NgayVay";
            dTimePicker_NgayVay.Size = new Size(286, 26);
            dTimePicker_NgayVay.TabIndex = 12;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label15.Location = new Point(106, 671);
            label15.Margin = new Padding(5, 0, 5, 0);
            label15.Name = "label15";
            label15.Size = new Size(68, 19);
            label15.TabIndex = 40;
            label15.Text = "Ghi chú";
            // 
            // rtb_GhiChu
            // 
            rtb_GhiChu.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            rtb_GhiChu.Location = new Point(300, 683);
            rtb_GhiChu.Margin = new Padding(4, 3, 4, 3);
            rtb_GhiChu.Name = "rtb_GhiChu";
            rtb_GhiChu.Size = new Size(285, 81);
            rtb_GhiChu.TabIndex = 13;
            rtb_GhiChu.Text = "";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label16.Location = new Point(106, 790);
            label16.Margin = new Padding(5, 0, 5, 0);
            label16.Name = "label16";
            label16.Size = new Size(147, 19);
            label16.TabIndex = 42;
            label16.Text = "Nhân viên thu tiền";
            // 
            // tb_NhanVienThuTien
            // 
            tb_NhanVienThuTien.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tb_NhanVienThuTien.Location = new Point(299, 790);
            tb_NhanVienThuTien.Margin = new Padding(4, 2, 4, 2);
            tb_NhanVienThuTien.Name = "tb_NhanVienThuTien";
            tb_NhanVienThuTien.Size = new Size(285, 26);
            tb_NhanVienThuTien.TabIndex = 14;
            tb_NhanVienThuTien.TextAlign = HorizontalAlignment.Center;
            toolTip_KyLai.SetToolTip(tb_NhanVienThuTien, "Chú thích: Kỳ lãi.");
            // 
            // toolTip_KyLai
            // 
            toolTip_KyLai.ToolTipIcon = ToolTipIcon.Info;
            toolTip_KyLai.ToolTipTitle = "Chú thích kỳ lãi";
            // 
            // tb_ChuyenDoiLaiSuat
            // 
            tb_ChuyenDoiLaiSuat.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tb_ChuyenDoiLaiSuat.Location = new Point(945, 597);
            tb_ChuyenDoiLaiSuat.Margin = new Padding(4, 2, 4, 2);
            tb_ChuyenDoiLaiSuat.Name = "tb_ChuyenDoiLaiSuat";
            tb_ChuyenDoiLaiSuat.ReadOnly = true;
            tb_ChuyenDoiLaiSuat.Size = new Size(285, 26);
            tb_ChuyenDoiLaiSuat.TabIndex = 19;
            tb_ChuyenDoiLaiSuat.TextAlign = HorizontalAlignment.Center;
            tb_ChuyenDoiLaiSuat.TextChanged += tb_ChuyenDoiLaiSuat_TextChanged;
            // 
            // lb1_ThongtinTaiSan
            // 
            lb1_ThongtinTaiSan.AutoSize = true;
            lb1_ThongtinTaiSan.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lb1_ThongtinTaiSan.Location = new Point(739, 462);
            lb1_ThongtinTaiSan.Margin = new Padding(5, 0, 5, 0);
            lb1_ThongtinTaiSan.Name = "lb1_ThongtinTaiSan";
            lb1_ThongtinTaiSan.Size = new Size(95, 19);
            lb1_ThongtinTaiSan.TabIndex = 45;
            lb1_ThongtinTaiSan.Text = "Thông tin 1";
            // 
            // tb1_ThongtinTaiSan
            // 
            tb1_ThongtinTaiSan.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tb1_ThongtinTaiSan.Location = new Point(945, 462);
            tb1_ThongtinTaiSan.Margin = new Padding(4, 2, 4, 2);
            tb1_ThongtinTaiSan.Name = "tb1_ThongtinTaiSan";
            tb1_ThongtinTaiSan.Size = new Size(285, 26);
            tb1_ThongtinTaiSan.TabIndex = 16;
            tb1_ThongtinTaiSan.TextAlign = HorizontalAlignment.Center;
            // 
            // tb2_ThongtinTaiSan
            // 
            tb2_ThongtinTaiSan.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tb2_ThongtinTaiSan.Location = new Point(945, 509);
            tb2_ThongtinTaiSan.Margin = new Padding(4, 2, 4, 2);
            tb2_ThongtinTaiSan.Name = "tb2_ThongtinTaiSan";
            tb2_ThongtinTaiSan.Size = new Size(285, 26);
            tb2_ThongtinTaiSan.TabIndex = 17;
            tb2_ThongtinTaiSan.TextAlign = HorizontalAlignment.Center;
            // 
            // lb2_ThongtinTaiSan
            // 
            lb2_ThongtinTaiSan.AutoSize = true;
            lb2_ThongtinTaiSan.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lb2_ThongtinTaiSan.Location = new Point(739, 509);
            lb2_ThongtinTaiSan.Margin = new Padding(5, 0, 5, 0);
            lb2_ThongtinTaiSan.Name = "lb2_ThongtinTaiSan";
            lb2_ThongtinTaiSan.Size = new Size(95, 19);
            lb2_ThongtinTaiSan.TabIndex = 47;
            lb2_ThongtinTaiSan.Text = "Thông tin 2";
            // 
            // tb3_ThongtinTaiSan
            // 
            tb3_ThongtinTaiSan.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tb3_ThongtinTaiSan.Location = new Point(945, 553);
            tb3_ThongtinTaiSan.Margin = new Padding(4, 2, 4, 2);
            tb3_ThongtinTaiSan.Name = "tb3_ThongtinTaiSan";
            tb3_ThongtinTaiSan.Size = new Size(285, 26);
            tb3_ThongtinTaiSan.TabIndex = 18;
            tb3_ThongtinTaiSan.TextAlign = HorizontalAlignment.Center;
            // 
            // lb3_ThongtinTaiSan
            // 
            lb3_ThongtinTaiSan.AutoSize = true;
            lb3_ThongtinTaiSan.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lb3_ThongtinTaiSan.Location = new Point(739, 553);
            lb3_ThongtinTaiSan.Margin = new Padding(5, 0, 5, 0);
            lb3_ThongtinTaiSan.Name = "lb3_ThongtinTaiSan";
            lb3_ThongtinTaiSan.Size = new Size(95, 19);
            lb3_ThongtinTaiSan.TabIndex = 49;
            lb3_ThongtinTaiSan.Text = "Thông tin 3";
            // 
            // rtb_ThongtinTaiSan
            // 
            rtb_ThongtinTaiSan.Location = new Point(945, 364);
            rtb_ThongtinTaiSan.Name = "rtb_ThongtinTaiSan";
            rtb_ThongtinTaiSan.Size = new Size(285, 64);
            rtb_ThongtinTaiSan.TabIndex = 15;
            rtb_ThongtinTaiSan.Text = "";
            rtb_ThongtinTaiSan.TextChanged += rtb_ThongtinTaiSan_TextChanged;
            // 
            // lb_TinhLai
            // 
            lb_TinhLai.AutoSize = true;
            lb_TinhLai.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lb_TinhLai.Location = new Point(739, 601);
            lb_TinhLai.Margin = new Padding(5, 0, 5, 0);
            lb_TinhLai.Name = "lb_TinhLai";
            lb_TinhLai.Size = new Size(176, 19);
            lb_TinhLai.TabIndex = 52;
            lb_TinhLai.Text = "Chuyển đổi lãi (tháng)";
            // 
            // btn_Luu
            // 
            btn_Luu.Location = new Point(309, 821);
            btn_Luu.Name = "btn_Luu";
            btn_Luu.Size = new Size(95, 41);
            btn_Luu.TabIndex = 53;
            btn_Luu.Text = "Lưu";
            btn_Luu.UseVisualStyleBackColor = true;
            btn_Luu.Click += btn_Luu_Click;
            // 
            // HopDongForm
            // 
            AutoScaleDimensions = new SizeF(9F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(1299, 1048);
            Controls.Add(btn_Luu);
            Controls.Add(lb_TinhLai);
            Controls.Add(rtb_ThongtinTaiSan);
            Controls.Add(tb3_ThongtinTaiSan);
            Controls.Add(lb3_ThongtinTaiSan);
            Controls.Add(tb2_ThongtinTaiSan);
            Controls.Add(lb2_ThongtinTaiSan);
            Controls.Add(tb1_ThongtinTaiSan);
            Controls.Add(lb1_ThongtinTaiSan);
            Controls.Add(tb_ChuyenDoiLaiSuat);
            Controls.Add(tb_NhanVienThuTien);
            Controls.Add(label16);
            Controls.Add(rtb_GhiChu);
            Controls.Add(label15);
            Controls.Add(dTimePicker_NgayVay);
            Controls.Add(label13);
            Controls.Add(lb_DonVi_Lai);
            Controls.Add(tb_Lai);
            Controls.Add(lb_Lai);
            Controls.Add(lb_DonVi_KyLai);
            Controls.Add(tb_KyLai);
            Controls.Add(lb_KyLai);
            Controls.Add(lb_DonVi_TongThoiGianVay);
            Controls.Add(lb_DonVi_TongSoTienVay);
            Controls.Add(tb_TongThoiGianVay);
            Controls.Add(lb_TongThoiGianVay);
            Controls.Add(cbBox_HinhThucLai);
            Controls.Add(label11);
            Controls.Add(tb_TienVay);
            Controls.Add(label10);
            Controls.Add(label9);
            Controls.Add(btn_QuayLai);
            Controls.Add(cbBox_LoaiTaiSan);
            Controls.Add(label8);
            Controls.Add(pictureBox2);
            Controls.Add(label7);
            Controls.Add(rtb_DiaChi);
            Controls.Add(tbox_SDT);
            Controls.Add(label4);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(tbox_CCCD);
            Controls.Add(tbox_MaHD);
            Controls.Add(label3);
            Controls.Add(tbox_Ten);
            Controls.Add(label2);
            Controls.Add(pictureBox1);
            Controls.Add(label1);
            Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(5, 2, 5, 2);
            Name = "HopDongForm";
            Text = "Thêm hợp đồng mới";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private PictureBox pictureBox1;
        private Label label2;
        private TextBox tbox_Ten;
        private Label label3;
        private TextBox tbox_MaHD;
        private TextBox tbox_CCCD;
        private Label label5;
        private Label label6;
        private Label label4;
        private TextBox tbox_SDT;
        private RichTextBox rtb_DiaChi;
        private PictureBox pictureBox2;
        private Label label7;
        private Label label8;
        private ComboBox cbBox_LoaiTaiSan;
        private Button btn_QuayLai;
        private Label label9;
        private Label label10;
        private TextBox tb_TienVay;
        private Label label11;
        private ComboBox cbBox_HinhThucLai;
        private Label lb_TongThoiGianVay;
        private TextBox tb_TongThoiGianVay;
        private Label lb_DonVi_TongSoTienVay;
        private Label lb_DonVi_TongThoiGianVay;
        private Label lb_KyLai;
        private TextBox tb_KyLai;
        private Label lb_DonVi_KyLai;
        private Label lb_Lai;
        private TextBox tb_Lai;
        private Label lb_DonVi_Lai;
        private Label label13;
        private DateTimePicker dTimePicker_NgayVay;
        private Label label15;
        private RichTextBox rtb_GhiChu;
        private Label label16;
        private TextBox tb_NhanVienThuTien;
        private ToolTip toolTip_KyLai;
        private TextBox tb_ChuyenDoiLaiSuat;
        private Label lb1_ThongtinTaiSan;
        private TextBox tb1_ThongtinTaiSan;
        private TextBox tb2_ThongtinTaiSan;
        private Label lb2_ThongtinTaiSan;
        private TextBox tb3_ThongtinTaiSan;
        private Label lb3_ThongtinTaiSan;
        private RichTextBox rtb_ThongtinTaiSan;
        private Label lb_TinhLai;
        private Button btn_Luu;
    }
}