namespace QuanLyVayVon.QuanLyHD
{
    partial class QuanLyHopDong
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QuanLyHopDong));
            btn_ThemHopDong = new Button();
            btn_Thoat = new Button();
            btn_MoCSDL = new Button();
            btn_chinhsua = new Button();
            flowLayoutPanel_HopDong = new FlowLayoutPanel();
            flowLayoutPanel_UseForm = new FlowLayoutPanel();
            btn_Resize = new Button();
            btn_Hide = new Button();
            btn_About = new Button();
            btn_UpdateInfoSystem = new Button();
            btn_Tien = new Button();
            btn_Home = new Button();
            btn_Lui = new Button();
            toolTip1 = new ToolTip(components);
            cbBox_Search = new ComboBox();
            flowLayoutPanel_Search = new FlowLayoutPanel();
            btn_Search = new Button();
            tb_Search = new TextBox();
            tbLayout_Form = new TableLayoutPanel();
            dataGridView_ThongTinHopDong = new DataGridView();
            MaHD = new DataGridViewTextBoxColumn();
            TenKH = new DataGridViewTextBoxColumn();
            TenTaiSan = new DataGridViewTextBoxColumn();
            TienVay = new DataGridViewTextBoxColumn();
            NgayVay = new DataGridViewTextBoxColumn();
            LaiDaDong = new DataGridViewTextBoxColumn();
            TienNo = new DataGridViewTextBoxColumn();
            LaiDenHomNay = new DataGridViewTextBoxColumn();
            NgayDongLai = new DataGridViewTextBoxColumn();
            TinhTrang = new DataGridViewTextBoxColumn();
            tbLayout_Button = new TableLayoutPanel();
            flowLayoutPanel_HopDong.SuspendLayout();
            flowLayoutPanel_UseForm.SuspendLayout();
            flowLayoutPanel_Search.SuspendLayout();
            tbLayout_Form.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView_ThongTinHopDong).BeginInit();
            tbLayout_Button.SuspendLayout();
            SuspendLayout();
            // 
            // btn_ThemHopDong
            // 
            btn_ThemHopDong.BackColor = SystemColors.HighlightText;
            btn_ThemHopDong.Location = new Point(3, 3);
            btn_ThemHopDong.Name = "btn_ThemHopDong";
            btn_ThemHopDong.Size = new Size(225, 41);
            btn_ThemHopDong.TabIndex = 0;
            btn_ThemHopDong.Text = "Thêm hợp đồng mới";
            btn_ThemHopDong.UseVisualStyleBackColor = false;
            btn_ThemHopDong.Click += button1_Click;
            // 
            // btn_Thoat
            // 
            btn_Thoat.Anchor = AnchorStyles.Right;
            btn_Thoat.ForeColor = Color.Black;
            btn_Thoat.Location = new Point(3, 3);
            btn_Thoat.Name = "btn_Thoat";
            btn_Thoat.Size = new Size(57, 41);
            btn_Thoat.TabIndex = 2;
            btn_Thoat.Text = "X";
            btn_Thoat.UseVisualStyleBackColor = true;
            btn_Thoat.Click += btnClose_Click;
            // 
            // btn_MoCSDL
            // 
            btn_MoCSDL.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btn_MoCSDL.BackColor = SystemColors.HighlightText;
            btn_MoCSDL.Location = new Point(465, 3);
            btn_MoCSDL.Name = "btn_MoCSDL";
            btn_MoCSDL.Size = new Size(225, 41);
            btn_MoCSDL.TabIndex = 4;
            btn_MoCSDL.Text = "Cơ sở dữ liệu";
            btn_MoCSDL.UseVisualStyleBackColor = false;
            btn_MoCSDL.Click += btn_MoCSDL_Click;
            // 
            // btn_chinhsua
            // 
            btn_chinhsua.Location = new Point(234, 3);
            btn_chinhsua.Name = "btn_chinhsua";
            btn_chinhsua.Size = new Size(225, 40);
            btn_chinhsua.TabIndex = 6;
            btn_chinhsua.Text = "Sửa hợp đồng";
            btn_chinhsua.UseVisualStyleBackColor = true;
            btn_chinhsua.Click += button1_Click_1;
            // 
            // flowLayoutPanel_HopDong
            // 
            flowLayoutPanel_HopDong.Controls.Add(btn_ThemHopDong);
            flowLayoutPanel_HopDong.Controls.Add(btn_chinhsua);
            flowLayoutPanel_HopDong.Controls.Add(btn_MoCSDL);
            flowLayoutPanel_HopDong.Location = new Point(3, 3);
            flowLayoutPanel_HopDong.Name = "flowLayoutPanel_HopDong";
            flowLayoutPanel_HopDong.Size = new Size(771, 60);
            flowLayoutPanel_HopDong.TabIndex = 7;
            // 
            // flowLayoutPanel_UseForm
            // 
            flowLayoutPanel_UseForm.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            flowLayoutPanel_UseForm.Controls.Add(btn_Thoat);
            flowLayoutPanel_UseForm.Controls.Add(btn_Resize);
            flowLayoutPanel_UseForm.Controls.Add(btn_Hide);
            flowLayoutPanel_UseForm.Controls.Add(btn_About);
            flowLayoutPanel_UseForm.Controls.Add(btn_UpdateInfoSystem);
            flowLayoutPanel_UseForm.Controls.Add(btn_Tien);
            flowLayoutPanel_UseForm.Controls.Add(btn_Home);
            flowLayoutPanel_UseForm.Controls.Add(btn_Lui);
            flowLayoutPanel_UseForm.Location = new Point(780, 3);
            flowLayoutPanel_UseForm.Name = "flowLayoutPanel_UseForm";
            flowLayoutPanel_UseForm.Size = new Size(771, 60);
            flowLayoutPanel_UseForm.TabIndex = 8;
            // 
            // btn_Resize
            // 
            btn_Resize.Anchor = AnchorStyles.Right;
            btn_Resize.ForeColor = Color.Black;
            btn_Resize.Location = new Point(66, 3);
            btn_Resize.Name = "btn_Resize";
            btn_Resize.Size = new Size(57, 41);
            btn_Resize.TabIndex = 13;
            btn_Resize.Text = "O";
            btn_Resize.UseVisualStyleBackColor = true;
            btn_Resize.Click += btn_Resize_Click;
            // 
            // btn_Hide
            // 
            btn_Hide.Anchor = AnchorStyles.Right;
            btn_Hide.Location = new Point(129, 4);
            btn_Hide.Name = "btn_Hide";
            btn_Hide.Size = new Size(56, 39);
            btn_Hide.TabIndex = 12;
            btn_Hide.Text = "_";
            btn_Hide.UseVisualStyleBackColor = true;
            btn_Hide.Click += btn_Hide_Click;
            // 
            // btn_About
            // 
            btn_About.Anchor = AnchorStyles.Right;
            btn_About.Location = new Point(191, 4);
            btn_About.Name = "btn_About";
            btn_About.Size = new Size(105, 39);
            btn_About.TabIndex = 11;
            btn_About.Text = "Giới thiệu";
            btn_About.UseVisualStyleBackColor = true;
            btn_About.Click += btn_About_Click;
            // 
            // btn_UpdateInfoSystem
            // 
            btn_UpdateInfoSystem.Anchor = AnchorStyles.Right;
            btn_UpdateInfoSystem.Location = new Point(302, 4);
            btn_UpdateInfoSystem.Name = "btn_UpdateInfoSystem";
            btn_UpdateInfoSystem.Size = new Size(105, 39);
            btn_UpdateInfoSystem.TabIndex = 11;
            btn_UpdateInfoSystem.Text = "Cập nhật";
            btn_UpdateInfoSystem.UseVisualStyleBackColor = true;
            btn_UpdateInfoSystem.Click += btn_UpdateInfoSystem_Click_1;
            // 
            // btn_Tien
            // 
            btn_Tien.Anchor = AnchorStyles.Left;
            btn_Tien.Location = new Point(413, 3);
            btn_Tien.Name = "btn_Tien";
            btn_Tien.Size = new Size(105, 40);
            btn_Tien.TabIndex = 9;
            btn_Tien.Text = ">>";
            btn_Tien.UseVisualStyleBackColor = true;
            btn_Tien.Click += btn_Tien_Click;
            // 
            // btn_Home
            // 
            btn_Home.Anchor = AnchorStyles.Left;
            btn_Home.BackgroundImageLayout = ImageLayout.Stretch;
            btn_Home.Image = (Image)resources.GetObject("btn_Home.Image");
            btn_Home.Location = new Point(524, 4);
            btn_Home.Name = "btn_Home";
            btn_Home.Size = new Size(105, 39);
            btn_Home.TabIndex = 11;
            btn_Home.UseVisualStyleBackColor = true;
            btn_Home.Click += btn_Home_Click;
            // 
            // btn_Lui
            // 
            btn_Lui.Anchor = AnchorStyles.Left;
            btn_Lui.Location = new Point(635, 3);
            btn_Lui.Name = "btn_Lui";
            btn_Lui.Size = new Size(105, 40);
            btn_Lui.TabIndex = 10;
            btn_Lui.Text = "<<";
            btn_Lui.UseVisualStyleBackColor = true;
            btn_Lui.Click += btn_Lui_Click;
            // 
            // cbBox_Search
            // 
            cbBox_Search.Anchor = AnchorStyles.Right;
            cbBox_Search.FormattingEnabled = true;
            cbBox_Search.Location = new Point(114, 11);
            cbBox_Search.Name = "cbBox_Search";
            cbBox_Search.Size = new Size(151, 23);
            cbBox_Search.TabIndex = 9;
            cbBox_Search.SelectedIndexChanged += cbBox_Search_SelectedIndexChanged;
            // 
            // flowLayoutPanel_Search
            // 
            flowLayoutPanel_Search.Controls.Add(btn_Search);
            flowLayoutPanel_Search.Controls.Add(cbBox_Search);
            flowLayoutPanel_Search.Controls.Add(tb_Search);
            flowLayoutPanel_Search.Location = new Point(3, 69);
            flowLayoutPanel_Search.Name = "flowLayoutPanel_Search";
            flowLayoutPanel_Search.Size = new Size(771, 48);
            flowLayoutPanel_Search.TabIndex = 10;
            // 
            // btn_Search
            // 
            btn_Search.Anchor = AnchorStyles.Left;
            btn_Search.BackgroundImageLayout = ImageLayout.Stretch;
            btn_Search.Image = (Image)resources.GetObject("btn_Search.Image");
            btn_Search.Location = new Point(3, 3);
            btn_Search.Name = "btn_Search";
            btn_Search.Size = new Size(105, 39);
            btn_Search.TabIndex = 12;
            btn_Search.UseVisualStyleBackColor = true;
            btn_Search.Click += btn_Search_Click;
            // 
            // tb_Search
            // 
            tb_Search.Anchor = AnchorStyles.Left;
            tb_Search.Location = new Point(271, 11);
            tb_Search.Name = "tb_Search";
            tb_Search.Size = new Size(158, 23);
            tb_Search.TabIndex = 10;
            // 
            // tbLayout_Form
            // 
            tbLayout_Form.ColumnCount = 2;
            tbLayout_Form.ColumnStyles.Add(new ColumnStyle());
            tbLayout_Form.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbLayout_Form.Controls.Add(dataGridView_ThongTinHopDong, 1, 1);
            tbLayout_Form.Controls.Add(tbLayout_Button, 1, 0);
            tbLayout_Form.Location = new Point(12, 12);
            tbLayout_Form.Name = "tbLayout_Form";
            tbLayout_Form.RowCount = 2;
            tbLayout_Form.RowStyles.Add(new RowStyle(SizeType.Absolute, 138F));
            tbLayout_Form.RowStyles.Add(new RowStyle(SizeType.Absolute, 539F));
            tbLayout_Form.Size = new Size(1560, 837);
            tbLayout_Form.TabIndex = 11;
            // 
            // dataGridView_ThongTinHopDong
            // 
            dataGridView_ThongTinHopDong.Anchor = AnchorStyles.None;
            dataGridView_ThongTinHopDong.BackgroundColor = SystemColors.Window;
            dataGridView_ThongTinHopDong.ColumnHeadersHeight = 34;
            dataGridView_ThongTinHopDong.Columns.AddRange(new DataGridViewColumn[] { MaHD, TenKH, TenTaiSan, TienVay, NgayVay, LaiDaDong, TienNo, LaiDenHomNay, NgayDongLai, TinhTrang });
            dataGridView_ThongTinHopDong.Location = new Point(15, 164);
            dataGridView_ThongTinHopDong.Name = "dataGridView_ThongTinHopDong";
            dataGridView_ThongTinHopDong.RowHeadersWidth = 62;
            dataGridView_ThongTinHopDong.Size = new Size(1529, 647);
            dataGridView_ThongTinHopDong.TabIndex = 1;
            dataGridView_ThongTinHopDong.RowPostPaint += dataGridView_ThongTinHopDong_RowPostPaint;
            // 
            // MaHD
            // 
            MaHD.HeaderText = "Mã HĐ";
            MaHD.MinimumWidth = 8;
            MaHD.Name = "MaHD";
            MaHD.ReadOnly = true;
            MaHD.Width = 150;
            // 
            // TenKH
            // 
            TenKH.HeaderText = "Khách Hàng";
            TenKH.MinimumWidth = 8;
            TenKH.Name = "TenKH";
            TenKH.ReadOnly = true;
            TenKH.Width = 150;
            // 
            // TenTaiSan
            // 
            TenTaiSan.HeaderText = "Tài sản";
            TenTaiSan.MinimumWidth = 8;
            TenTaiSan.Name = "TenTaiSan";
            TenTaiSan.ReadOnly = true;
            TenTaiSan.Width = 150;
            // 
            // TienVay
            // 
            TienVay.HeaderText = "Tiền vay";
            TienVay.MinimumWidth = 8;
            TienVay.Name = "TienVay";
            TienVay.ReadOnly = true;
            TienVay.Width = 150;
            // 
            // NgayVay
            // 
            NgayVay.HeaderText = "Ngày vay";
            NgayVay.MinimumWidth = 8;
            NgayVay.Name = "NgayVay";
            NgayVay.ReadOnly = true;
            NgayVay.Width = 150;
            // 
            // LaiDaDong
            // 
            LaiDaDong.HeaderText = "Lãi đã đóng";
            LaiDaDong.MinimumWidth = 8;
            LaiDaDong.Name = "LaiDaDong";
            LaiDaDong.ReadOnly = true;
            LaiDaDong.Width = 150;
            // 
            // TienNo
            // 
            TienNo.HeaderText = "Tiền nợ";
            TienNo.MinimumWidth = 8;
            TienNo.Name = "TienNo";
            TienNo.ReadOnly = true;
            TienNo.Width = 150;
            // 
            // LaiDenHomNay
            // 
            LaiDenHomNay.HeaderText = "Lãi đến hôm nay";
            LaiDenHomNay.MinimumWidth = 8;
            LaiDenHomNay.Name = "LaiDenHomNay";
            LaiDenHomNay.ReadOnly = true;
            LaiDenHomNay.Width = 150;
            // 
            // NgayDongLai
            // 
            NgayDongLai.HeaderText = "Ngày phải đóng lãi";
            NgayDongLai.MinimumWidth = 8;
            NgayDongLai.Name = "NgayDongLai";
            NgayDongLai.ReadOnly = true;
            NgayDongLai.Width = 150;
            // 
            // TinhTrang
            // 
            TinhTrang.HeaderText = "Tình Trạng";
            TinhTrang.MinimumWidth = 8;
            TinhTrang.Name = "TinhTrang";
            TinhTrang.ReadOnly = true;
            TinhTrang.Width = 150;
            // 
            // tbLayout_Button
            // 
            tbLayout_Button.ColumnCount = 2;
            tbLayout_Button.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbLayout_Button.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbLayout_Button.Controls.Add(flowLayoutPanel_HopDong, 0, 0);
            tbLayout_Button.Controls.Add(flowLayoutPanel_UseForm, 1, 0);
            tbLayout_Button.Controls.Add(flowLayoutPanel_Search, 0, 1);
            tbLayout_Button.Location = new Point(3, 3);
            tbLayout_Button.Name = "tbLayout_Button";
            tbLayout_Button.RowCount = 2;
            tbLayout_Button.RowStyles.Add(new RowStyle(SizeType.Percent, 50.5618F));
            tbLayout_Button.RowStyles.Add(new RowStyle(SizeType.Percent, 49.4382F));
            tbLayout_Button.Size = new Size(1554, 132);
            tbLayout_Button.TabIndex = 12;
            // 
            // QuanLyHopDong
            // 
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(1584, 861);
            Controls.Add(tbLayout_Form);
            Name = "QuanLyHopDong";
            Text = "ThemHopDongMoi";
            FormClosing += QuanLyHopDong_FormClosing;
            Load += QuanLyHopDong_Load;
            flowLayoutPanel_HopDong.ResumeLayout(false);
            flowLayoutPanel_UseForm.ResumeLayout(false);
            flowLayoutPanel_Search.ResumeLayout(false);
            flowLayoutPanel_Search.PerformLayout();
            tbLayout_Form.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView_ThongTinHopDong).EndInit();
            tbLayout_Button.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Button btn_ThemHopDong;
        private Button btn_Thoat;
        private Button btn_MoCSDL;
        private Button btn_chinhsua;
        private FlowLayoutPanel flowLayoutPanel_HopDong;
        private FlowLayoutPanel flowLayoutPanel_UseForm;
        private ToolTip toolTip1;
        private Button btn_Tien;
        private Button btn_Lui;
        private Button btn_Home;
        private ComboBox cbBox_Search;
        private FlowLayoutPanel flowLayoutPanel_Search;
        private Button btn_Search;
        private Button btn_UpdateInfoSystem;
        private Button btn_About;
        private Button btn_Hide;
        private TableLayoutPanel tbLayout_Form;
        private DataGridView dataGridView_ThongTinHopDong;
        private DataGridViewTextBoxColumn MaHD;
        private DataGridViewTextBoxColumn TenKH;
        private DataGridViewTextBoxColumn TenTaiSan;
        private DataGridViewTextBoxColumn TienVay;
        private DataGridViewTextBoxColumn NgayVay;
        private DataGridViewTextBoxColumn LaiDaDong;
        private DataGridViewTextBoxColumn TienNo;
        private DataGridViewTextBoxColumn LaiDenHomNay;
        private DataGridViewTextBoxColumn NgayDongLai;
        private DataGridViewTextBoxColumn TinhTrang;
        private TableLayoutPanel tbLayout_Button;
        private TextBox tb_Search;
        private Button btn_Resize;
    }
}