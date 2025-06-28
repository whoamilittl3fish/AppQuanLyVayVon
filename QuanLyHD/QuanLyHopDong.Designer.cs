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
            btn_ThongKe = new Button();
            btn_HopDongHetHan = new Button();
            btn_Lui = new Button();
            btn_Home = new Button();
            btn_Tien = new Button();
            flowLayoutPanel_UseForm = new FlowLayoutPanel();
            btn_Resize = new Button();
            btn_Hide = new Button();
            btn_About = new Button();
            btn_Premium = new Button();
            toolTip1 = new ToolTip(components);
            cbBox_Search = new ComboBox();
            flowLayoutPanel_Search = new FlowLayoutPanel();
            btn_Search = new Button();
            tb_Search = new TextBox();
            dt_StartSearch = new DateTimePicker();
            dt_EndSearch = new DateTimePicker();
            tbLayout_Form = new TableLayoutPanel();
            tbLayout_Button = new TableLayoutPanel();
            flow_HetHan = new FlowLayoutPanel();
            flow_TuongTacDataGrid = new FlowLayoutPanel();
            btn_SapToiHan = new Button();
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
            flowLayoutPanel_HopDong.SuspendLayout();
            flowLayoutPanel_UseForm.SuspendLayout();
            flowLayoutPanel_Search.SuspendLayout();
            tbLayout_Form.SuspendLayout();
            tbLayout_Button.SuspendLayout();
            flow_HetHan.SuspendLayout();
            flow_TuongTacDataGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView_ThongTinHopDong).BeginInit();
            SuspendLayout();
            // 
            // btn_ThemHopDong
            // 
            btn_ThemHopDong.BackColor = SystemColors.HighlightText;
            btn_ThemHopDong.Location = new Point(114, 3);
            btn_ThemHopDong.Name = "btn_ThemHopDong";
            btn_ThemHopDong.Size = new Size(197, 41);
            btn_ThemHopDong.TabIndex = 0;
            btn_ThemHopDong.Text = "Thêm hợp đồng mới";
            btn_ThemHopDong.UseVisualStyleBackColor = false;
            btn_ThemHopDong.Click += button1_Click;
            // 
            // btn_Thoat
            // 
            btn_Thoat.Anchor = AnchorStyles.Right;
            btn_Thoat.ForeColor = Color.Black;
            btn_Thoat.Location = new Point(635, 3);
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
            btn_MoCSDL.Location = new Point(520, 3);
            btn_MoCSDL.Name = "btn_MoCSDL";
            btn_MoCSDL.Size = new Size(193, 41);
            btn_MoCSDL.TabIndex = 4;
            btn_MoCSDL.Text = "Cơ sở dữ liệu";
            btn_MoCSDL.UseVisualStyleBackColor = false;
            btn_MoCSDL.Click += btn_MoCSDL_Click;
            // 
            // btn_chinhsua
            // 
            btn_chinhsua.Location = new Point(317, 3);
            btn_chinhsua.Name = "btn_chinhsua";
            btn_chinhsua.Size = new Size(197, 40);
            btn_chinhsua.TabIndex = 6;
            btn_chinhsua.Text = "Sửa hợp đồng";
            btn_chinhsua.UseVisualStyleBackColor = true;
            btn_chinhsua.Click += button1_Click_1;
            // 
            // flowLayoutPanel_HopDong
            // 
            flowLayoutPanel_HopDong.Controls.Add(btn_ThongKe);
            flowLayoutPanel_HopDong.Controls.Add(btn_ThemHopDong);
            flowLayoutPanel_HopDong.Controls.Add(btn_chinhsua);
            flowLayoutPanel_HopDong.Controls.Add(btn_MoCSDL);
            flowLayoutPanel_HopDong.Location = new Point(3, 3);
            flowLayoutPanel_HopDong.Name = "flowLayoutPanel_HopDong";
            flowLayoutPanel_HopDong.Size = new Size(844, 55);
            flowLayoutPanel_HopDong.TabIndex = 7;
            // 
            // btn_ThongKe
            // 
            btn_ThongKe.Anchor = AnchorStyles.Right;
            btn_ThongKe.Location = new Point(3, 4);
            btn_ThongKe.Name = "btn_ThongKe";
            btn_ThongKe.Size = new Size(105, 39);
            btn_ThongKe.TabIndex = 17;
            btn_ThongKe.Text = "Thống kê";
            btn_ThongKe.UseVisualStyleBackColor = true;
            btn_ThongKe.Click += btn_ThongKe_Click;
            // 
            // btn_HopDongHetHan
            // 
            btn_HopDongHetHan.Anchor = AnchorStyles.None;
            btn_HopDongHetHan.Location = new Point(191, 3);
            btn_HopDongHetHan.Name = "btn_HopDongHetHan";
            btn_HopDongHetHan.Size = new Size(182, 39);
            btn_HopDongHetHan.TabIndex = 15;
            btn_HopDongHetHan.Text = "HetHan";
            btn_HopDongHetHan.UseVisualStyleBackColor = true;
            btn_HopDongHetHan.Click += btn_HopDongHetHan_Click_1;
            // 
            // btn_Lui
            // 
            btn_Lui.Anchor = AnchorStyles.Left;
            btn_Lui.Location = new Point(66, 3);
            btn_Lui.Name = "btn_Lui";
            btn_Lui.Size = new Size(105, 40);
            btn_Lui.TabIndex = 10;
            btn_Lui.UseVisualStyleBackColor = true;
            btn_Lui.Click += btn_Lui_Click;
            // 
            // btn_Home
            // 
            btn_Home.Anchor = AnchorStyles.Left;
            btn_Home.BackgroundImageLayout = ImageLayout.Stretch;
            btn_Home.Image = (Image)resources.GetObject("btn_Home.Image");
            btn_Home.Location = new Point(177, 4);
            btn_Home.Name = "btn_Home";
            btn_Home.Size = new Size(105, 39);
            btn_Home.TabIndex = 11;
            btn_Home.UseVisualStyleBackColor = true;
            btn_Home.Click += btn_Home_Click;
            // 
            // btn_Tien
            // 
            btn_Tien.Anchor = AnchorStyles.Left;
            btn_Tien.Location = new Point(288, 3);
            btn_Tien.Name = "btn_Tien";
            btn_Tien.Size = new Size(105, 40);
            btn_Tien.TabIndex = 9;
            btn_Tien.UseVisualStyleBackColor = true;
            btn_Tien.Click += btn_Tien_Click;
            // 
            // flowLayoutPanel_UseForm
            // 
            flowLayoutPanel_UseForm.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            flowLayoutPanel_UseForm.Controls.Add(btn_Thoat);
            flowLayoutPanel_UseForm.Controls.Add(btn_Resize);
            flowLayoutPanel_UseForm.Controls.Add(btn_Hide);
            flowLayoutPanel_UseForm.Controls.Add(btn_About);
            flowLayoutPanel_UseForm.Controls.Add(btn_Tien);
            flowLayoutPanel_UseForm.Controls.Add(btn_Home);
            flowLayoutPanel_UseForm.Controls.Add(btn_Lui);
            flowLayoutPanel_UseForm.FlowDirection = FlowDirection.RightToLeft;
            flowLayoutPanel_UseForm.Location = new Point(856, 3);
            flowLayoutPanel_UseForm.Name = "flowLayoutPanel_UseForm";
            flowLayoutPanel_UseForm.Size = new Size(695, 55);
            flowLayoutPanel_UseForm.TabIndex = 8;
            // 
            // btn_Resize
            // 
            btn_Resize.Anchor = AnchorStyles.Right;
            btn_Resize.ForeColor = Color.Black;
            btn_Resize.Location = new Point(572, 3);
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
            btn_Hide.Location = new Point(510, 4);
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
            btn_About.Location = new Point(399, 4);
            btn_About.Name = "btn_About";
            btn_About.Size = new Size(105, 39);
            btn_About.TabIndex = 11;
            btn_About.Text = "Giới thiệu";
            btn_About.UseVisualStyleBackColor = true;
            btn_About.Click += btn_About_Click;
            // 
            // btn_Premium
            // 
            btn_Premium.Anchor = AnchorStyles.Right;
            btn_Premium.Location = new Point(651, 3);
            btn_Premium.Name = "btn_Premium";
            btn_Premium.Size = new Size(44, 23);
            btn_Premium.TabIndex = 7;
            btn_Premium.UseVisualStyleBackColor = true;
            btn_Premium.Click += btn_Premium_Click;
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
            flowLayoutPanel_Search.Controls.Add(dt_StartSearch);
            flowLayoutPanel_Search.Controls.Add(dt_EndSearch);
            flowLayoutPanel_Search.Location = new Point(3, 64);
            flowLayoutPanel_Search.Name = "flowLayoutPanel_Search";
            flowLayoutPanel_Search.Size = new Size(844, 49);
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
            // dt_StartSearch
            // 
            dt_StartSearch.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            dt_StartSearch.Location = new Point(435, 11);
            dt_StartSearch.Name = "dt_StartSearch";
            dt_StartSearch.Size = new Size(296, 23);
            dt_StartSearch.TabIndex = 12;
            // 
            // dt_EndSearch
            // 
            dt_EndSearch.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            dt_EndSearch.Location = new Point(3, 48);
            dt_EndSearch.Name = "dt_EndSearch";
            dt_EndSearch.Size = new Size(296, 23);
            dt_EndSearch.TabIndex = 13;
            // 
            // tbLayout_Form
            // 
            tbLayout_Form.ColumnCount = 2;
            tbLayout_Form.ColumnStyles.Add(new ColumnStyle());
            tbLayout_Form.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbLayout_Form.Controls.Add(tbLayout_Button, 1, 0);
            tbLayout_Form.Controls.Add(dataGridView_ThongTinHopDong, 1, 1);
            tbLayout_Form.Location = new Point(12, 12);
            tbLayout_Form.Name = "tbLayout_Form";
            tbLayout_Form.RowCount = 2;
            tbLayout_Form.RowStyles.Add(new RowStyle(SizeType.Absolute, 191F));
            tbLayout_Form.RowStyles.Add(new RowStyle(SizeType.Absolute, 49F));
            tbLayout_Form.Size = new Size(1560, 837);
            tbLayout_Form.TabIndex = 11;
            // 
            // tbLayout_Button
            // 
            tbLayout_Button.ColumnCount = 2;
            tbLayout_Button.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbLayout_Button.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 704F));
            tbLayout_Button.Controls.Add(flowLayoutPanel_HopDong, 0, 0);
            tbLayout_Button.Controls.Add(flowLayoutPanel_UseForm, 1, 0);
            tbLayout_Button.Controls.Add(flow_HetHan, 1, 1);
            tbLayout_Button.Controls.Add(flow_TuongTacDataGrid, 0, 2);
            tbLayout_Button.Controls.Add(flowLayoutPanel_Search, 0, 1);
            tbLayout_Button.Location = new Point(3, 3);
            tbLayout_Button.Name = "tbLayout_Button";
            tbLayout_Button.RowCount = 3;
            tbLayout_Button.RowStyles.Add(new RowStyle(SizeType.Absolute, 61F));
            tbLayout_Button.RowStyles.Add(new RowStyle(SizeType.Percent, 45.0704231F));
            tbLayout_Button.RowStyles.Add(new RowStyle(SizeType.Absolute, 55F));
            tbLayout_Button.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbLayout_Button.Size = new Size(1554, 185);
            tbLayout_Button.TabIndex = 12;
            // 
            // flow_HetHan
            // 
            flow_HetHan.Controls.Add(btn_Premium);
            flow_HetHan.FlowDirection = FlowDirection.RightToLeft;
            flow_HetHan.Location = new Point(853, 64);
            flow_HetHan.Name = "flow_HetHan";
            flow_HetHan.Size = new Size(698, 49);
            flow_HetHan.TabIndex = 16;
            // 
            // flow_TuongTacDataGrid
            // 
            flow_TuongTacDataGrid.Controls.Add(btn_SapToiHan);
            flow_TuongTacDataGrid.Controls.Add(btn_HopDongHetHan);
            flow_TuongTacDataGrid.Location = new Point(3, 133);
            flow_TuongTacDataGrid.Name = "flow_TuongTacDataGrid";
            flow_TuongTacDataGrid.Size = new Size(844, 49);
            flow_TuongTacDataGrid.TabIndex = 17;
            // 
            // btn_SapToiHan
            // 
            btn_SapToiHan.Anchor = AnchorStyles.None;
            btn_SapToiHan.Location = new Point(3, 3);
            btn_SapToiHan.Name = "btn_SapToiHan";
            btn_SapToiHan.Size = new Size(182, 39);
            btn_SapToiHan.TabIndex = 16;
            btn_SapToiHan.Text = "SapToiHan";
            btn_SapToiHan.UseVisualStyleBackColor = true;
            btn_SapToiHan.Click += btn_SapToiHan_Click;
            // 
            // dataGridView_ThongTinHopDong
            // 
            dataGridView_ThongTinHopDong.Anchor = AnchorStyles.None;
            dataGridView_ThongTinHopDong.BackgroundColor = SystemColors.Window;
            dataGridView_ThongTinHopDong.ColumnHeadersHeight = 34;
            dataGridView_ThongTinHopDong.Columns.AddRange(new DataGridViewColumn[] { MaHD, TenKH, TenTaiSan, TienVay, NgayVay, LaiDaDong, TienNo, LaiDenHomNay, NgayDongLai, TinhTrang });
            dataGridView_ThongTinHopDong.Location = new Point(15, 204);
            dataGridView_ThongTinHopDong.Name = "dataGridView_ThongTinHopDong";
            dataGridView_ThongTinHopDong.RowHeadersWidth = 62;
            dataGridView_ThongTinHopDong.Size = new Size(1529, 620);
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
            tbLayout_Button.ResumeLayout(false);
            flow_HetHan.ResumeLayout(false);
            flow_TuongTacDataGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView_ThongTinHopDong).EndInit();
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
        private Button btn_About;
        private Button btn_Hide;
        private TableLayoutPanel tbLayout_Form;
        private TableLayoutPanel tbLayout_Button;
        private TextBox tb_Search;
        private Button btn_Resize;
        private DateTimePicker dt_EndSearch;
        private DateTimePicker dt_StartSearch;
        private Button btn_Premium;
        private Button btn_HopDongHetHan;
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
        private FlowLayoutPanel flow_HetHan;
        private FlowLayoutPanel flow_TuongTacDataGrid;
        private Button btn_SapToiHan;
        private Button btn_ThongKe;
    }
}