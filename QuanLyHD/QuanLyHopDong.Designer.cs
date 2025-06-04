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
            btn_MoCSDL = new Button();
            tb_Test = new TextBox();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView_ThongTinHopDong).BeginInit();
            SuspendLayout();
            // 
            // dataGridView_ThongTinHopDong
            // 
            dataGridView_ThongTinHopDong.BackgroundColor = SystemColors.Window;
            dataGridView_ThongTinHopDong.Columns.AddRange(new DataGridViewColumn[] { MaHD, Ten, DoCam, TienVay, NgayVay, LaiDaDong, TienNo, LaiDenHomNay, NgayDongLai, TinhTrang });
            dataGridView_ThongTinHopDong.Location = new Point(12, 175);
            dataGridView_ThongTinHopDong.Name = "dataGridView_ThongTinHopDong";
            dataGridView_ThongTinHopDong.Size = new Size(1206, 349);
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
            btn_ThemHopDong.Location = new Point(12, 128);
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
            // btn_MoCSDL
            // 
            btn_MoCSDL.BackColor = SystemColors.HighlightText;
            btn_MoCSDL.Location = new Point(972, 12);
            btn_MoCSDL.Name = "btn_MoCSDL";
            btn_MoCSDL.Size = new Size(146, 41);
            btn_MoCSDL.TabIndex = 4;
            btn_MoCSDL.Text = "Cơ sở dữ liệu";
            btn_MoCSDL.UseVisualStyleBackColor = false;
            btn_MoCSDL.Click += btn_MoCSDL_Click;
            // 
            // tb_Test
            // 
            tb_Test.Location = new Point(208, 138);
            tb_Test.Name = "tb_Test";
            tb_Test.Size = new Size(100, 23);
            tb_Test.TabIndex = 5;
            tb_Test.Text = "1";
            tb_Test.TextChanged += textBox1_TextChanged;
            // 
            // button1
            // 
            button1.Location = new Point(327, 138);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 6;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click_1;
            // 
            // QuanLyHopDong
            // 
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(1230, 536);
            Controls.Add(button1);
            Controls.Add(tb_Test);
            Controls.Add(btn_MoCSDL);
            Controls.Add(btn_Thoat);
            Controls.Add(btn_ThemHopDong);
            Controls.Add(dataGridView_ThongTinHopDong);
            Name = "QuanLyHopDong";
            Text = "ThemHopDongMoi";
            FormClosing += QuanLyHopDong_FormClosing;
            Load += QuanLyHopDong_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView_ThongTinHopDong).EndInit();
            ResumeLayout(false);
            PerformLayout();
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
        private Button btn_MoCSDL;
        private TextBox tb_Test;
        private Button button1;
    }
}