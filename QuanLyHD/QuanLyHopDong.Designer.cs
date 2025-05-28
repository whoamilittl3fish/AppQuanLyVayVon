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
            dataGridView_ThongTinHopDong = new DataGridView();
            MaHD = new DataGridViewTextBoxColumn();
            Ten = new DataGridViewTextBoxColumn();
            DoCam = new DataGridViewTextBoxColumn();
            TienVay = new DataGridViewTextBoxColumn();
            NgayVay = new DataGridViewTextBoxColumn();
            LaiDaDong = new DataGridViewTextBoxColumn();
            TienNo = new DataGridViewTextBoxColumn();
            LaiDenHomNay = new DataGridViewTextBoxColumn();
            NgayDongLai = new DataGridViewTextBoxColumn();
            TinhTrang = new DataGridViewTextBoxColumn();
            btn_ThemHopDong = new Button();
            btn_Thoat = new Button();
            btn_QuayLai = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView_ThongTinHopDong).BeginInit();
            SuspendLayout();
            // 
            // dataGridView_ThongTinHopDong
            // 
            dataGridView_ThongTinHopDong.BackgroundColor = SystemColors.Window;
            dataGridView_ThongTinHopDong.Columns.AddRange(new DataGridViewColumn[] { MaHD, Ten, DoCam, TienVay, NgayVay, LaiDaDong, TienNo, LaiDenHomNay, NgayDongLai, TinhTrang });
            dataGridView_ThongTinHopDong.Location = new Point(12, 156);
            dataGridView_ThongTinHopDong.Name = "dataGridView_ThongTinHopDong";
            dataGridView_ThongTinHopDong.Size = new Size(1206, 368);
            dataGridView_ThongTinHopDong.TabIndex = 1;
            // 
            // MaHD
            // 
            MaHD.HeaderText = "Mã HĐ";
            MaHD.Name = "MaHD";
            MaHD.ReadOnly = true;
            // 
            // Ten
            // 
            Ten.HeaderText = "Khách Hàng";
            Ten.Name = "Ten";
            Ten.ReadOnly = true;
            // 
            // DoCam
            // 
            DoCam.HeaderText = "Tài sản";
            DoCam.Name = "DoCam";
            DoCam.ReadOnly = true;
            // 
            // TienVay
            // 
            TienVay.HeaderText = "Tiền cầm";
            TienVay.Name = "TienVay";
            TienVay.ReadOnly = true;
            // 
            // NgayVay
            // 
            NgayVay.HeaderText = "Ngày cầm";
            NgayVay.Name = "NgayVay";
            NgayVay.ReadOnly = true;
            // 
            // LaiDaDong
            // 
            LaiDaDong.HeaderText = "Lãi đã đóng";
            LaiDaDong.Name = "LaiDaDong";
            LaiDaDong.ReadOnly = true;
            // 
            // TienNo
            // 
            TienNo.HeaderText = "Tiền nợ";
            TienNo.Name = "TienNo";
            TienNo.ReadOnly = true;
            // 
            // LaiDenHomNay
            // 
            LaiDenHomNay.HeaderText = "Lãi đến hôm nay";
            LaiDenHomNay.Name = "LaiDenHomNay";
            LaiDenHomNay.ReadOnly = true;
            // 
            // NgayDongLai
            // 
            NgayDongLai.HeaderText = "Ngày phải đóng lãi";
            NgayDongLai.Name = "NgayDongLai";
            NgayDongLai.ReadOnly = true;
            // 
            // TinhTrang
            // 
            TinhTrang.HeaderText = "Tình Trạng";
            TinhTrang.Name = "TinhTrang";
            TinhTrang.ReadOnly = true;
            // 
            // btn_ThemHopDong
            // 
            btn_ThemHopDong.BackColor = SystemColors.HighlightText;
            btn_ThemHopDong.Location = new Point(12, 12);
            btn_ThemHopDong.Name = "btn_ThemHopDong";
            btn_ThemHopDong.Size = new Size(146, 41);
            btn_ThemHopDong.TabIndex = 0;
            btn_ThemHopDong.Text = "Thêm hợp đồng mới";
            btn_ThemHopDong.UseVisualStyleBackColor = false;
            btn_ThemHopDong.Click += button1_Click;
            // 
            // btn_Thoat
            // 
            btn_Thoat.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btn_Thoat.ForeColor = Color.Black;
            btn_Thoat.Location = new Point(1124, 12);
            btn_Thoat.Name = "btn_Thoat";
            btn_Thoat.Size = new Size(94, 41);
            btn_Thoat.TabIndex = 2;
            btn_Thoat.Text = "Thoát";
            btn_Thoat.UseVisualStyleBackColor = true;
            btn_Thoat.Click += btnClose_Click;
            // 
            // btn_QuayLai
            // 
            btn_QuayLai.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btn_QuayLai.ForeColor = Color.IndianRed;
            btn_QuayLai.Location = new Point(1024, 12);
            btn_QuayLai.Name = "btn_QuayLai";
            btn_QuayLai.Size = new Size(94, 41);
            btn_QuayLai.TabIndex = 3;
            btn_QuayLai.Text = "Quay lại";
            btn_QuayLai.UseVisualStyleBackColor = true;
            btn_QuayLai.Click += btn_QuayLai_Click;
            // 
            // QuanLyHopDong
            // 
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(1230, 536);
            Controls.Add(btn_QuayLai);
            Controls.Add(btn_Thoat);
            Controls.Add(btn_ThemHopDong);
            Controls.Add(dataGridView_ThongTinHopDong);
            Name = "QuanLyHopDong";
            Text = "ThemHopDongMoi";
            FormClosing += QuanLyHopDong_FormClosing;
            Load += QuanLyHopDong_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView_ThongTinHopDong).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private DataGridView dataGridView_ThongTinHopDong;
        private Button btn_ThemHopDong;
        private DataGridViewTextBoxColumn MaHD;
        private DataGridViewTextBoxColumn Ten;
        private DataGridViewTextBoxColumn DoCam;
        private DataGridViewTextBoxColumn TienVay;
        private DataGridViewTextBoxColumn NgayVay;
        private DataGridViewTextBoxColumn LaiDaDong;
        private DataGridViewTextBoxColumn TienNo;
        private DataGridViewTextBoxColumn LaiDenHomNay;
        private DataGridViewTextBoxColumn NgayDongLai;
        private DataGridViewTextBoxColumn TinhTrang;
        private Button btn_Thoat;
        private Button btn_QuayLai;


    }
}