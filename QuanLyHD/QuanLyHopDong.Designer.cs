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
            btn_ThemHopDong = new Button();
            btn_Thoat = new Button();
            btn_MoCSDL = new Button();
            btn_chinhsua = new Button();
            flowLayoutPanel_button = new FlowLayoutPanel();
            flowLayoutPanel_Thoat = new FlowLayoutPanel();
            btn_Lui = new Button();
            btn_Home = new Button();
            btn_Tien = new Button();
            btn_UpdateInfoSystem = new Button();
            btn_About = new Button();
            btn_Hide = new Button();
            toolTip1 = new ToolTip(components);
            cbBox_Search = new ComboBox();
            flowLayoutPanel_Search = new FlowLayoutPanel();
            btn_Search = new Button();
            tb_Search = new TextBox();
            flowLayout_Panel_TinhTrangSearch = new FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)dataGridView_ThongTinHopDong).BeginInit();
            flowLayoutPanel_button.SuspendLayout();
            flowLayoutPanel_Thoat.SuspendLayout();
            flowLayoutPanel_Search.SuspendLayout();
            flowLayout_Panel_TinhTrangSearch.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridView_ThongTinHopDong
            // 
            dataGridView_ThongTinHopDong.BackgroundColor = SystemColors.Window;
            dataGridView_ThongTinHopDong.ColumnHeadersHeight = 34;
            dataGridView_ThongTinHopDong.Columns.AddRange(new DataGridViewColumn[] { MaHD, TenKH, TenTaiSan, TienVay, NgayVay, LaiDaDong, TienNo, LaiDenHomNay, NgayDongLai, TinhTrang });
            dataGridView_ThongTinHopDong.Location = new Point(12, 184);
            dataGridView_ThongTinHopDong.Name = "dataGridView_ThongTinHopDong";
            dataGridView_ThongTinHopDong.RowHeadersWidth = 62;
            dataGridView_ThongTinHopDong.Size = new Size(1206, 340);
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
            btn_Thoat.Location = new Point(669, 3);
            btn_Thoat.Name = "btn_Thoat";
            btn_Thoat.Size = new Size(94, 41);
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
            // flowLayoutPanel_button
            // 
            flowLayoutPanel_button.Controls.Add(btn_ThemHopDong);
            flowLayoutPanel_button.Controls.Add(btn_chinhsua);
            flowLayoutPanel_button.Controls.Add(btn_MoCSDL);
            flowLayoutPanel_button.Location = new Point(21, 12);
            flowLayoutPanel_button.Name = "flowLayoutPanel_button";
            flowLayoutPanel_button.Size = new Size(697, 47);
            flowLayoutPanel_button.TabIndex = 7;
            // 
            // flowLayoutPanel_Thoat
            // 
            flowLayoutPanel_Thoat.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            flowLayoutPanel_Thoat.Controls.Add(btn_Lui);
            flowLayoutPanel_Thoat.Controls.Add(btn_Home);
            flowLayoutPanel_Thoat.Controls.Add(btn_Tien);
            flowLayoutPanel_Thoat.Controls.Add(btn_UpdateInfoSystem);
            flowLayoutPanel_Thoat.Controls.Add(btn_About);
            flowLayoutPanel_Thoat.Controls.Add(btn_Hide);
            flowLayoutPanel_Thoat.Controls.Add(btn_Thoat);
            flowLayoutPanel_Thoat.Location = new Point(447, 12);
            flowLayoutPanel_Thoat.Name = "flowLayoutPanel_Thoat";
            flowLayoutPanel_Thoat.Size = new Size(771, 47);
            flowLayoutPanel_Thoat.TabIndex = 8;
            // 
            // btn_Lui
            // 
            btn_Lui.Anchor = AnchorStyles.Left;
            btn_Lui.Location = new Point(3, 3);
            btn_Lui.Name = "btn_Lui";
            btn_Lui.Size = new Size(105, 40);
            btn_Lui.TabIndex = 10;
            btn_Lui.Text = "<<";
            btn_Lui.UseVisualStyleBackColor = true;
            btn_Lui.Click += btn_Lui_Click;
            // 
            // btn_Home
            // 
            btn_Home.Anchor = AnchorStyles.Left;
            btn_Home.BackgroundImageLayout = ImageLayout.Stretch;
            btn_Home.Image = (Image)resources.GetObject("btn_Home.Image");
            btn_Home.Location = new Point(114, 4);
            btn_Home.Name = "btn_Home";
            btn_Home.Size = new Size(105, 39);
            btn_Home.TabIndex = 11;
            btn_Home.UseVisualStyleBackColor = true;
            btn_Home.Click += btn_Home_Click;
            // 
            // btn_Tien
            // 
            btn_Tien.Anchor = AnchorStyles.Left;
            btn_Tien.Location = new Point(225, 3);
            btn_Tien.Name = "btn_Tien";
            btn_Tien.Size = new Size(105, 40);
            btn_Tien.TabIndex = 9;
            btn_Tien.Text = ">>";
            btn_Tien.UseVisualStyleBackColor = true;
            btn_Tien.Click += btn_Tien_Click;
            // 
            // btn_UpdateInfoSystem
            // 
            btn_UpdateInfoSystem.Anchor = AnchorStyles.Right;
            btn_UpdateInfoSystem.Location = new Point(336, 4);
            btn_UpdateInfoSystem.Name = "btn_UpdateInfoSystem";
            btn_UpdateInfoSystem.Size = new Size(105, 39);
            btn_UpdateInfoSystem.TabIndex = 11;
            btn_UpdateInfoSystem.Text = "Cập nhật";
            btn_UpdateInfoSystem.UseVisualStyleBackColor = true;
            btn_UpdateInfoSystem.Click += btn_UpdateInfoSystem_Click_1;
            // 
            // btn_About
            // 
            btn_About.Anchor = AnchorStyles.Right;
            btn_About.Location = new Point(447, 4);
            btn_About.Name = "btn_About";
            btn_About.Size = new Size(105, 39);
            btn_About.TabIndex = 11;
            btn_About.Text = "Giới thiệu";
            btn_About.UseVisualStyleBackColor = true;
            btn_About.Click += btn_About_Click;
            // 
            // btn_Hide
            // 
            btn_Hide.Anchor = AnchorStyles.Right;
            btn_Hide.Location = new Point(558, 4);
            btn_Hide.Name = "btn_Hide";
            btn_Hide.Size = new Size(105, 39);
            btn_Hide.TabIndex = 12;
            btn_Hide.UseVisualStyleBackColor = true;
            btn_Hide.Click += btn_Hide_Click;
            // 
            // cbBox_Search
            // 
            cbBox_Search.Anchor = AnchorStyles.Right;
            cbBox_Search.FormattingEnabled = true;
            cbBox_Search.Location = new Point(3, 3);
            cbBox_Search.Name = "cbBox_Search";
            cbBox_Search.Size = new Size(151, 23);
            cbBox_Search.TabIndex = 9;
            cbBox_Search.SelectedIndexChanged += cbBox_Search_SelectedIndexChanged;
            // 
            // flowLayoutPanel_Search
            // 
            flowLayoutPanel_Search.Controls.Add(btn_Search);
            flowLayoutPanel_Search.Controls.Add(tb_Search);
            flowLayoutPanel_Search.Location = new Point(21, 76);
            flowLayoutPanel_Search.Name = "flowLayoutPanel_Search";
            flowLayoutPanel_Search.Size = new Size(284, 47);
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
            tb_Search.Location = new Point(114, 11);
            tb_Search.Name = "tb_Search";
            tb_Search.Size = new Size(158, 23);
            tb_Search.TabIndex = 10;
            // 
            // flowLayout_Panel_TinhTrangSearch
            // 
            flowLayout_Panel_TinhTrangSearch.Anchor = AnchorStyles.Right;
            flowLayout_Panel_TinhTrangSearch.Controls.Add(cbBox_Search);
            flowLayout_Panel_TinhTrangSearch.Location = new Point(934, 71);
            flowLayout_Panel_TinhTrangSearch.Name = "flowLayout_Panel_TinhTrangSearch";
            flowLayout_Panel_TinhTrangSearch.Size = new Size(284, 47);
            flowLayout_Panel_TinhTrangSearch.TabIndex = 13;
            // 
            // QuanLyHopDong
            // 
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(1230, 536);
            Controls.Add(flowLayoutPanel_Thoat);
            Controls.Add(flowLayout_Panel_TinhTrangSearch);
            Controls.Add(flowLayoutPanel_Search);
            Controls.Add(flowLayoutPanel_button);
            Controls.Add(dataGridView_ThongTinHopDong);
            Name = "QuanLyHopDong";
            Text = "ThemHopDongMoi";
            FormClosing += QuanLyHopDong_FormClosing;
            Load += QuanLyHopDong_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView_ThongTinHopDong).EndInit();
            flowLayoutPanel_button.ResumeLayout(false);
            flowLayoutPanel_Thoat.ResumeLayout(false);
            flowLayoutPanel_Search.ResumeLayout(false);
            flowLayoutPanel_Search.PerformLayout();
            flowLayout_Panel_TinhTrangSearch.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private DataGridView dataGridView_ThongTinHopDong;
        private Button btn_ThemHopDong;
        private Button btn_Thoat;
        private Button btn_MoCSDL;
        private Button btn_chinhsua;
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
        private FlowLayoutPanel flowLayoutPanel_button;
        private FlowLayoutPanel flowLayoutPanel_Thoat;
        private ToolTip toolTip1;
        private Button btn_Tien;
        private Button btn_Lui;
        private Button btn_Home;
        private ComboBox cbBox_Search;
        private FlowLayoutPanel flowLayoutPanel_Search;
        private TextBox tb_Search;
        private Button btn_Search;
        private Button btn_UpdateInfoSystem;
        private Button btn_About;
        private Button btn_Hide;
        private FlowLayoutPanel flowLayout_Panel_TinhTrangSearch;
    }
}