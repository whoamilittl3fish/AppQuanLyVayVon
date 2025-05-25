namespace QuanLyVayVon
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
            button1 = new Button();
            button2 = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView_ThongTinHopDong).BeginInit();
            SuspendLayout();
            // 
            // dataGridView_ThongTinHopDong
            // 
            dataGridView_ThongTinHopDong.BackgroundColor = SystemColors.Window;
            dataGridView_ThongTinHopDong.Columns.AddRange(new DataGridViewColumn[] { MaHD, Ten, DoCam, TienVay, NgayVay, LaiDaDong, TienNo, LaiDenHomNay, NgayDongLai, TinhTrang });
            dataGridView_ThongTinHopDong.Location = new Point(-47, 158);
            dataGridView_ThongTinHopDong.Name = "dataGridView_ThongTinHopDong";
            dataGridView_ThongTinHopDong.Size = new Size(1171, 346);
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
            // button1
            // 
            button1.BackColor = SystemColors.HighlightText;
            button1.Location = new Point(12, 107);
            button1.Name = "button1";
            button1.Size = new Size(146, 23);
            button1.TabIndex = 0;
            button1.Text = "Thêm hợp đồng mới";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.ForeColor = Color.IndianRed;
            button2.Location = new Point(1124, 12);
            button2.Name = "button2";
            button2.Size = new Size(94, 41);
            button2.TabIndex = 2;
            button2.Text = "Thoát";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // QuanLyHopDong
            // 
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(1230, 536);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(dataGridView_ThongTinHopDong);
            Name = "QuanLyHopDong";
            Text = "ThemHopDongMoi";
            FormClosing += ThemHopDongMoi_FormClosing;
            Load += ThemHopDongMoi_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView_ThongTinHopDong).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private DataGridView dataGridView_ThongTinHopDong;
        private Button button1;
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
        private Button button2;
    }
}