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

        // Pseudocode plan:
        // - Increase the width of the right column (các control bên phải: label4, tbox_CCCD, label5, tbox_SDT, label10, lb1_ThongtinTaiSan, tb1_ThongtinTaiSan, lb2_ThongtinTaiSan, tb2_ThongtinTaiSan, lb3_ThongtinTaiSan, tb3_ThongtinTaiSan, rtb_ThongtinTaiSan, lb_TinhLai, tb_ChuyenDoiLaiSuat).
        // - Move the right column controls further to the right (increase X).
        // - Increase the width of the TextBox/RichTextBox controls in the right column for more spacing between label and input.
        // - Keep the vertical (Y) positions and label order as before.
        // - Adjust the form width and possibly the button positions for balance.

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HopDongForm));
            label1 = new Label();
            pictureBox1 = new PictureBox();
            label2 = new Label();
            tbox_Ten = new TextBox();
            lb_MaHD = new Label();
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
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            tableLayoutPanel3 = new TableLayoutPanel();
            tableLayoutPanel4 = new TableLayoutPanel();
            btn_Hide = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Arial", 14F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.Location = new Point(103, 61);
            label1.Name = "label1";
            label1.Size = new Size(199, 23);
            label1.TabIndex = 0;
            label1.Text = "Thông tin khách hàng";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(53, 51);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(40, 40);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Left;
            label2.AutoSize = true;
            label2.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(3, 60);
            label2.Name = "label2";
            label2.Size = new Size(142, 19);
            label2.TabIndex = 2;
            label2.Text = "Tên khách hàng *";
            // 
            // tbox_Ten
            // 
            tbox_Ten.Anchor = AnchorStyles.None;
            tbox_Ten.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tbox_Ten.Location = new Point(358, 155);
            tbox_Ten.Name = "tbox_Ten";
            tbox_Ten.Size = new Size(288, 26);
            tbox_Ten.TabIndex = 2;
            tbox_Ten.TextChanged += tbox_Ten_TextChanged;
            // 
            // lb_MaHD
            // 
            lb_MaHD.Anchor = AnchorStyles.Left;
            lb_MaHD.AutoSize = true;
            lb_MaHD.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lb_MaHD.Location = new Point(3, 12);
            lb_MaHD.Name = "lb_MaHD";
            lb_MaHD.Size = new Size(120, 19);
            lb_MaHD.TabIndex = 5;
            lb_MaHD.Text = "Mã hợp đồng *";
            // 
            // tbox_MaHD
            // 
            tbox_MaHD.Anchor = AnchorStyles.None;
            tbox_MaHD.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tbox_MaHD.Location = new Point(358, 103);
            tbox_MaHD.Name = "tbox_MaHD";
            tbox_MaHD.Size = new Size(288, 26);
            tbox_MaHD.TabIndex = 1;
            tbox_MaHD.TextChanged += tbox_MaHD_TextChanged;
            // 
            // tbox_CCCD
            // 
            tbox_CCCD.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tbox_CCCD.Location = new Point(1013, 151);
            tbox_CCCD.Name = "tbox_CCCD";
            tbox_CCCD.Size = new Size(288, 26);
            tbox_CCCD.TabIndex = 5;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Left;
            label5.AutoSize = true;
            label5.Font = new Font("Arial", 12F, FontStyle.Bold);
            label5.Location = new Point(3, 14);
            label5.Name = "label5";
            label5.Size = new Size(42, 19);
            label5.TabIndex = 9;
            label5.Text = "SĐT";
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Left;
            label6.AutoSize = true;
            label6.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.Location = new Point(3, 132);
            label6.Name = "label6";
            label6.Size = new Size(61, 19);
            label6.TabIndex = 11;
            label6.Text = "Địa chỉ";
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Left;
            label4.AutoSize = true;
            label4.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(3, 63);
            label4.Name = "label4";
            label4.Size = new Size(158, 19);
            label4.TabIndex = 12;
            label4.Text = "Số CCCD/ Hộ chiếu";
            // 
            // tbox_SDT
            // 
            tbox_SDT.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tbox_SDT.Location = new Point(1013, 103);
            tbox_SDT.Name = "tbox_SDT";
            tbox_SDT.Size = new Size(288, 26);
            tbox_SDT.TabIndex = 4;
            // 
            // rtb_DiaChi
            // 
            rtb_DiaChi.Anchor = AnchorStyles.None;
            rtb_DiaChi.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            rtb_DiaChi.Location = new Point(358, 201);
            rtb_DiaChi.Name = "rtb_DiaChi";
            rtb_DiaChi.Size = new Size(288, 85);
            rtb_DiaChi.TabIndex = 3;
            rtb_DiaChi.Text = "";
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(56, 307);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(40, 40);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 16;
            pictureBox2.TabStop = false;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Arial", 14F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label7.Location = new Point(106, 317);
            label7.Name = "label7";
            label7.Size = new Size(187, 23);
            label7.TabIndex = 15;
            label7.Text = "Thông tin khoản vay";
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.Left;
            label8.AutoSize = true;
            label8.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label8.Location = new Point(3, 14);
            label8.Name = "label8";
            label8.Size = new Size(106, 19);
            label8.TabIndex = 18;
            label8.Text = "Loại tài sản *";
            // 
            // cbBox_LoaiTaiSan
            // 
            cbBox_LoaiTaiSan.Anchor = AnchorStyles.None;
            cbBox_LoaiTaiSan.FormattingEnabled = true;
            cbBox_LoaiTaiSan.Location = new Point(358, 367);
            cbBox_LoaiTaiSan.Name = "cbBox_LoaiTaiSan";
            cbBox_LoaiTaiSan.Size = new Size(287, 26);
            cbBox_LoaiTaiSan.TabIndex = 6;
            cbBox_LoaiTaiSan.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // btn_QuayLai
            // 
            btn_QuayLai.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btn_QuayLai.Location = new Point(1323, 12);
            btn_QuayLai.Name = "btn_QuayLai";
            btn_QuayLai.Size = new Size(56, 50);
            btn_QuayLai.TabIndex = 21;
            btn_QuayLai.Text = "X";
            btn_QuayLai.UseVisualStyleBackColor = true;
            btn_QuayLai.Click += btn_QuayLai_Click;
            // 
            // label9
            // 
            label9.Anchor = AnchorStyles.Left;
            label9.AutoSize = true;
            label9.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.Location = new Point(3, 61);
            label9.Name = "label9";
            label9.Size = new Size(145, 19);
            label9.TabIndex = 22;
            label9.Text = "Tổng số tiền vay *";
            // 
            // label10
            // 
            label10.Anchor = AnchorStyles.Left;
            label10.AutoSize = true;
            label10.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label10.Location = new Point(3, 37);
            label10.Name = "label10";
            label10.Size = new Size(146, 19);
            label10.TabIndex = 23;
            label10.Text = "Thông tin tài sản *";
            // 
            // tb_TienVay
            // 
            tb_TienVay.Anchor = AnchorStyles.None;
            tb_TienVay.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tb_TienVay.Location = new Point(358, 414);
            tb_TienVay.Name = "tb_TienVay";
            tb_TienVay.Size = new Size(288, 26);
            tb_TienVay.TabIndex = 7;
            tb_TienVay.TextChanged += tb_TienVay_TextChanged;
            // 
            // label11
            // 
            label11.Anchor = AnchorStyles.Left;
            label11.AutoSize = true;
            label11.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label11.Location = new Point(3, 108);
            label11.Name = "label11";
            label11.Size = new Size(116, 19);
            label11.TabIndex = 26;
            label11.Text = "Hình thức lãi *";
            // 
            // cbBox_HinhThucLai
            // 
            cbBox_HinhThucLai.Anchor = AnchorStyles.None;
            cbBox_HinhThucLai.FormattingEnabled = true;
            cbBox_HinhThucLai.Location = new Point(358, 461);
            cbBox_HinhThucLai.Name = "cbBox_HinhThucLai";
            cbBox_HinhThucLai.Size = new Size(288, 26);
            cbBox_HinhThucLai.TabIndex = 8;
            cbBox_HinhThucLai.SelectedIndexChanged += comboBox1_SelectedIndexChanged_1;
            // 
            // lb_TongThoiGianVay
            // 
            lb_TongThoiGianVay.Anchor = AnchorStyles.Left;
            lb_TongThoiGianVay.AutoSize = true;
            lb_TongThoiGianVay.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lb_TongThoiGianVay.Location = new Point(3, 155);
            lb_TongThoiGianVay.Name = "lb_TongThoiGianVay";
            lb_TongThoiGianVay.Size = new Size(161, 19);
            lb_TongThoiGianVay.TabIndex = 28;
            lb_TongThoiGianVay.Text = "Tổng thời gian vay *";
            lb_TongThoiGianVay.Click += lb_TongThoiGianVay_Click;
            // 
            // tb_TongThoiGianVay
            // 
            tb_TongThoiGianVay.Anchor = AnchorStyles.None;
            tb_TongThoiGianVay.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tb_TongThoiGianVay.Location = new Point(358, 508);
            tb_TongThoiGianVay.Name = "tb_TongThoiGianVay";
            tb_TongThoiGianVay.Size = new Size(288, 26);
            tb_TongThoiGianVay.TabIndex = 9;
            tb_TongThoiGianVay.TextChanged += tb_TongThoiGianVay_TextChanged;
            // 
            // lb_DonVi_TongSoTienVay
            // 
            lb_DonVi_TongSoTienVay.AutoSize = true;
            lb_DonVi_TongSoTienVay.BackColor = SystemColors.ButtonHighlight;
            lb_DonVi_TongSoTienVay.BorderStyle = BorderStyle.Fixed3D;
            lb_DonVi_TongSoTienVay.FlatStyle = FlatStyle.Flat;
            lb_DonVi_TongSoTienVay.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lb_DonVi_TongSoTienVay.Location = new Point(660, 417);
            lb_DonVi_TongSoTienVay.Name = "lb_DonVi_TongSoTienVay";
            lb_DonVi_TongSoTienVay.Size = new Size(44, 20);
            lb_DonVi_TongSoTienVay.TabIndex = 30;
            lb_DonVi_TongSoTienVay.Text = "VND";
            // 
            // lb_DonVi_TongThoiGianVay
            // 
            lb_DonVi_TongThoiGianVay.AutoSize = true;
            lb_DonVi_TongThoiGianVay.BackColor = SystemColors.ButtonHighlight;
            lb_DonVi_TongThoiGianVay.BorderStyle = BorderStyle.Fixed3D;
            lb_DonVi_TongThoiGianVay.FlatStyle = FlatStyle.Flat;
            lb_DonVi_TongThoiGianVay.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lb_DonVi_TongThoiGianVay.Location = new Point(660, 511);
            lb_DonVi_TongThoiGianVay.Name = "lb_DonVi_TongThoiGianVay";
            lb_DonVi_TongThoiGianVay.Size = new Size(46, 20);
            lb_DonVi_TongThoiGianVay.TabIndex = 31;
            lb_DonVi_TongThoiGianVay.Text = "Ngày";
            // 
            // lb_KyLai
            // 
            lb_KyLai.Anchor = AnchorStyles.Left;
            lb_KyLai.AutoSize = true;
            lb_KyLai.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lb_KyLai.Location = new Point(3, 202);
            lb_KyLai.Name = "lb_KyLai";
            lb_KyLai.Size = new Size(61, 19);
            lb_KyLai.TabIndex = 32;
            lb_KyLai.Text = "Kỳ lãi *";
            // 
            // tb_KyLai
            // 
            tb_KyLai.Anchor = AnchorStyles.None;
            tb_KyLai.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tb_KyLai.Location = new Point(358, 559);
            tb_KyLai.Name = "tb_KyLai";
            tb_KyLai.Size = new Size(288, 26);
            tb_KyLai.TabIndex = 10;
            tb_KyLai.TextChanged += tb_KyLai_TextChanged;
            // 
            // lb_DonVi_KyLai
            // 
            lb_DonVi_KyLai.AutoSize = true;
            lb_DonVi_KyLai.BackColor = SystemColors.ButtonHighlight;
            lb_DonVi_KyLai.BorderStyle = BorderStyle.Fixed3D;
            lb_DonVi_KyLai.FlatStyle = FlatStyle.Flat;
            lb_DonVi_KyLai.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lb_DonVi_KyLai.Location = new Point(660, 562);
            lb_DonVi_KyLai.Name = "lb_DonVi_KyLai";
            lb_DonVi_KyLai.Size = new Size(46, 20);
            lb_DonVi_KyLai.TabIndex = 34;
            lb_DonVi_KyLai.Text = "Ngày";
            // 
            // lb_Lai
            // 
            lb_Lai.Anchor = AnchorStyles.Left;
            lb_Lai.AutoSize = true;
            lb_Lai.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lb_Lai.Location = new Point(3, 249);
            lb_Lai.Name = "lb_Lai";
            lb_Lai.Size = new Size(42, 19);
            lb_Lai.TabIndex = 35;
            lb_Lai.Text = "Lãi *";
            // 
            // tb_Lai
            // 
            tb_Lai.Anchor = AnchorStyles.None;
            tb_Lai.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tb_Lai.Location = new Point(358, 602);
            tb_Lai.Name = "tb_Lai";
            tb_Lai.Size = new Size(288, 26);
            tb_Lai.TabIndex = 11;
            tb_Lai.TextChanged += tb_Lai_TextChanged;
            // 
            // lb_DonVi_Lai
            // 
            lb_DonVi_Lai.AutoSize = true;
            lb_DonVi_Lai.BackColor = SystemColors.ButtonHighlight;
            lb_DonVi_Lai.BorderStyle = BorderStyle.Fixed3D;
            lb_DonVi_Lai.FlatStyle = FlatStyle.Flat;
            lb_DonVi_Lai.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lb_DonVi_Lai.Location = new Point(660, 605);
            lb_DonVi_Lai.Name = "lb_DonVi_Lai";
            lb_DonVi_Lai.Size = new Size(84, 20);
            lb_DonVi_Lai.TabIndex = 37;
            lb_DonVi_Lai.Text = "VNĐ/Ngày";
            // 
            // label13
            // 
            label13.Anchor = AnchorStyles.Left;
            label13.AutoSize = true;
            label13.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label13.Location = new Point(3, 296);
            label13.Name = "label13";
            label13.Size = new Size(90, 19);
            label13.TabIndex = 38;
            label13.Text = "Ngày vay *";
            // 
            // dTimePicker_NgayVay
            // 
            dTimePicker_NgayVay.Anchor = AnchorStyles.None;
            dTimePicker_NgayVay.Location = new Point(358, 649);
            dTimePicker_NgayVay.Name = "dTimePicker_NgayVay";
            dTimePicker_NgayVay.Size = new Size(288, 26);
            dTimePicker_NgayVay.TabIndex = 12;
            // 
            // label15
            // 
            label15.Anchor = AnchorStyles.Left;
            label15.AutoSize = true;
            label15.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label15.Location = new Point(3, 364);
            label15.Name = "label15";
            label15.Size = new Size(68, 19);
            label15.TabIndex = 40;
            label15.Text = "Ghi chú";
            // 
            // rtb_GhiChu
            // 
            rtb_GhiChu.Anchor = AnchorStyles.None;
            rtb_GhiChu.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            rtb_GhiChu.Location = new Point(358, 703);
            rtb_GhiChu.Name = "rtb_GhiChu";
            rtb_GhiChu.Size = new Size(288, 70);
            rtb_GhiChu.TabIndex = 13;
            rtb_GhiChu.Text = "";
            // 
            // label16
            // 
            label16.Anchor = AnchorStyles.Left;
            label16.AutoSize = true;
            label16.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label16.Location = new Point(3, 434);
            label16.Name = "label16";
            label16.Size = new Size(147, 19);
            label16.TabIndex = 42;
            label16.Text = "Nhân viên thu tiền";
            // 
            // tb_NhanVienThuTien
            // 
            tb_NhanVienThuTien.Anchor = AnchorStyles.None;
            tb_NhanVienThuTien.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tb_NhanVienThuTien.Location = new Point(358, 791);
            tb_NhanVienThuTien.Name = "tb_NhanVienThuTien";
            tb_NhanVienThuTien.Size = new Size(288, 26);
            tb_NhanVienThuTien.TabIndex = 14;
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
            tb_ChuyenDoiLaiSuat.Location = new Point(1013, 606);
            tb_ChuyenDoiLaiSuat.Name = "tb_ChuyenDoiLaiSuat";
            tb_ChuyenDoiLaiSuat.ReadOnly = true;
            tb_ChuyenDoiLaiSuat.Size = new Size(288, 26);
            tb_ChuyenDoiLaiSuat.TabIndex = 19;
            tb_ChuyenDoiLaiSuat.TextChanged += tb_ChuyenDoiLaiSuat_TextChanged;
            // 
            // lb1_ThongtinTaiSan
            // 
            lb1_ThongtinTaiSan.Anchor = AnchorStyles.Left;
            lb1_ThongtinTaiSan.AutoSize = true;
            lb1_ThongtinTaiSan.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lb1_ThongtinTaiSan.Location = new Point(3, 107);
            lb1_ThongtinTaiSan.Name = "lb1_ThongtinTaiSan";
            lb1_ThongtinTaiSan.Size = new Size(95, 19);
            lb1_ThongtinTaiSan.TabIndex = 45;
            lb1_ThongtinTaiSan.Text = "Thông tin 1";
            // 
            // tb1_ThongtinTaiSan
            // 
            tb1_ThongtinTaiSan.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tb1_ThongtinTaiSan.Location = new Point(1013, 465);
            tb1_ThongtinTaiSan.Name = "tb1_ThongtinTaiSan";
            tb1_ThongtinTaiSan.Size = new Size(288, 26);
            tb1_ThongtinTaiSan.TabIndex = 16;
            // 
            // tb2_ThongtinTaiSan
            // 
            tb2_ThongtinTaiSan.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tb2_ThongtinTaiSan.Location = new Point(1013, 512);
            tb2_ThongtinTaiSan.Name = "tb2_ThongtinTaiSan";
            tb2_ThongtinTaiSan.Size = new Size(288, 26);
            tb2_ThongtinTaiSan.TabIndex = 17;
            // 
            // lb2_ThongtinTaiSan
            // 
            lb2_ThongtinTaiSan.Anchor = AnchorStyles.Left;
            lb2_ThongtinTaiSan.AutoSize = true;
            lb2_ThongtinTaiSan.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lb2_ThongtinTaiSan.Location = new Point(3, 154);
            lb2_ThongtinTaiSan.Name = "lb2_ThongtinTaiSan";
            lb2_ThongtinTaiSan.Size = new Size(95, 19);
            lb2_ThongtinTaiSan.TabIndex = 47;
            lb2_ThongtinTaiSan.Text = "Thông tin 2";
            // 
            // tb3_ThongtinTaiSan
            // 
            tb3_ThongtinTaiSan.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tb3_ThongtinTaiSan.Location = new Point(1013, 559);
            tb3_ThongtinTaiSan.Name = "tb3_ThongtinTaiSan";
            tb3_ThongtinTaiSan.Size = new Size(288, 26);
            tb3_ThongtinTaiSan.TabIndex = 18;
            // 
            // lb3_ThongtinTaiSan
            // 
            lb3_ThongtinTaiSan.Anchor = AnchorStyles.Left;
            lb3_ThongtinTaiSan.AutoSize = true;
            lb3_ThongtinTaiSan.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lb3_ThongtinTaiSan.Location = new Point(3, 201);
            lb3_ThongtinTaiSan.Name = "lb3_ThongtinTaiSan";
            lb3_ThongtinTaiSan.Size = new Size(95, 19);
            lb3_ThongtinTaiSan.TabIndex = 49;
            lb3_ThongtinTaiSan.Text = "Thông tin 3";
            // 
            // rtb_ThongtinTaiSan
            // 
            rtb_ThongtinTaiSan.Location = new Point(1013, 371);
            rtb_ThongtinTaiSan.Name = "rtb_ThongtinTaiSan";
            rtb_ThongtinTaiSan.Size = new Size(288, 73);
            rtb_ThongtinTaiSan.TabIndex = 15;
            rtb_ThongtinTaiSan.Text = "";
            rtb_ThongtinTaiSan.TextChanged += rtb_ThongtinTaiSan_TextChanged;
            // 
            // lb_TinhLai
            // 
            lb_TinhLai.Anchor = AnchorStyles.Left;
            lb_TinhLai.AutoSize = true;
            lb_TinhLai.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lb_TinhLai.Location = new Point(3, 249);
            lb_TinhLai.Name = "lb_TinhLai";
            lb_TinhLai.Size = new Size(176, 19);
            lb_TinhLai.TabIndex = 52;
            lb_TinhLai.Text = "Chuyển đổi lãi (tháng)";
            // 
            // btn_Luu
            // 
            btn_Luu.Location = new Point(358, 833);
            btn_Luu.Name = "btn_Luu";
            btn_Luu.Size = new Size(288, 50);
            btn_Luu.TabIndex = 53;
            btn_Luu.Text = "Lưu";
            btn_Luu.UseVisualStyleBackColor = true;
            btn_Luu.Click += btn_Luu_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(label6, 0, 2);
            tableLayoutPanel1.Controls.Add(label2, 0, 1);
            tableLayoutPanel1.Controls.Add(lb_MaHD, 0, 0);
            tableLayoutPanel1.Location = new Point(106, 98);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 23.4042549F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 27.6595745F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 92F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(196, 188);
            tableLayoutPanel1.TabIndex = 54;
            tableLayoutPanel1.Paint += tableLayoutPanel1_Paint;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.AutoScroll = true;
            tableLayoutPanel2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 199F));
            tableLayoutPanel2.Controls.Add(label8, 0, 0);
            tableLayoutPanel2.Controls.Add(label9, 0, 1);
            tableLayoutPanel2.Controls.Add(label11, 0, 2);
            tableLayoutPanel2.Controls.Add(label16, 0, 8);
            tableLayoutPanel2.Controls.Add(lb_TongThoiGianVay, 0, 3);
            tableLayoutPanel2.Controls.Add(label15, 0, 7);
            tableLayoutPanel2.Controls.Add(lb_KyLai, 0, 4);
            tableLayoutPanel2.Controls.Add(label13, 0, 6);
            tableLayoutPanel2.Controls.Add(lb_Lai, 0, 5);
            tableLayoutPanel2.Location = new Point(103, 360);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 9;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 90F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new Size(199, 469);
            tableLayoutPanel2.TabIndex = 55;
            tableLayoutPanel2.Paint += tableLayoutPanel2_Paint;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 1;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Controls.Add(label4, 0, 1);
            tableLayoutPanel3.Controls.Add(label5, 0, 0);
            tableLayoutPanel3.Location = new Point(793, 98);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 2;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel3.Size = new Size(200, 97);
            tableLayoutPanel3.TabIndex = 56;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.ColumnCount = 1;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.Controls.Add(label10, 0, 0);
            tableLayoutPanel4.Controls.Add(lb1_ThongtinTaiSan, 0, 1);
            tableLayoutPanel4.Controls.Add(lb2_ThongtinTaiSan, 0, 2);
            tableLayoutPanel4.Controls.Add(lb3_ThongtinTaiSan, 0, 3);
            tableLayoutPanel4.Controls.Add(lb_TinhLai, 0, 4);
            tableLayoutPanel4.Location = new Point(793, 360);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 5;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Absolute, 93F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.Size = new Size(200, 283);
            tableLayoutPanel4.TabIndex = 57;
            // 
            // btn_Hide
            // 
            btn_Hide.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btn_Hide.Location = new Point(1261, 12);
            btn_Hide.Name = "btn_Hide";
            btn_Hide.Size = new Size(56, 50);
            btn_Hide.TabIndex = 58;
            btn_Hide.Text = "_";
            btn_Hide.UseVisualStyleBackColor = true;
            btn_Hide.Click += btn_Hide_Click;
            // 
            // HopDongForm
            // 
            AutoScaleDimensions = new SizeF(9F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(1391, 920);
            Controls.Add(btn_Hide);
            Controls.Add(tableLayoutPanel4);
            Controls.Add(tableLayoutPanel3);
            Controls.Add(tb_NhanVienThuTien);
            Controls.Add(rtb_GhiChu);
            Controls.Add(dTimePicker_NgayVay);
            Controls.Add(tb_Lai);
            Controls.Add(tb_KyLai);
            Controls.Add(tb_TongThoiGianVay);
            Controls.Add(cbBox_HinhThucLai);
            Controls.Add(tb_TienVay);
            Controls.Add(cbBox_LoaiTaiSan);
            Controls.Add(tbox_MaHD);
            Controls.Add(tableLayoutPanel2);
            Controls.Add(rtb_DiaChi);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(btn_Luu);
            Controls.Add(tbox_Ten);
            Controls.Add(rtb_ThongtinTaiSan);
            Controls.Add(tb3_ThongtinTaiSan);
            Controls.Add(tb2_ThongtinTaiSan);
            Controls.Add(tb1_ThongtinTaiSan);
            Controls.Add(tb_ChuyenDoiLaiSuat);
            Controls.Add(lb_DonVi_Lai);
            Controls.Add(lb_DonVi_KyLai);
            Controls.Add(lb_DonVi_TongThoiGianVay);
            Controls.Add(lb_DonVi_TongSoTienVay);
            Controls.Add(btn_QuayLai);
            Controls.Add(pictureBox2);
            Controls.Add(label7);
            Controls.Add(tbox_SDT);
            Controls.Add(tbox_CCCD);
            Controls.Add(pictureBox1);
            Controls.Add(label1);
            Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
           
            Name = "HopDongForm";
            Text = "Thêm hợp đồng mới";
            Load += HopDongForm_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel4.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        
        private Label label1;
        private PictureBox pictureBox1;
        private Label label2;
        private TextBox tbox_Ten;
        private Label lb_MaHD;
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
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel tableLayoutPanel3;
        private TableLayoutPanel tableLayoutPanel4;
        private Button btn_Hide;
    }
}