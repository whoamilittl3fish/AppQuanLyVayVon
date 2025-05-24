namespace QuanLyVayVon
{
    partial class ThemHopDongMoi
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
            btn_Luu = new Button();
            flowLayoutP_Luu = new FlowLayoutPanel();
            tableLayoutP_Nhap = new TableLayoutPanel();
            dtimep_NgayHetHan = new DateTimePicker();
            dtimep_NgayVay = new DateTimePicker();
            label8 = new Label();
            label7 = new Label();
            label6 = new Label();
            tbox_TienVay = new TextBox();
            label5 = new Label();
            tbox_CCCD = new TextBox();
            label4 = new Label();
            tbox_SDT = new TextBox();
            label3 = new Label();
            label2 = new Label();
            tbox_Ten = new TextBox();
            tbox_MaHD = new TextBox();
            rtbox_DoCam = new RichTextBox();
            label1 = new Label();
            label9 = new Label();
            tb_LaiThang = new TextBox();
            dataGridView1 = new DataGridView();
            flowLayoutP_Luu.SuspendLayout();
            tableLayoutP_Nhap.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // btn_Luu
            // 
            btn_Luu.AutoSize = true;
            btn_Luu.Font = new Font("Calibri", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_Luu.Location = new Point(3, 3);
            btn_Luu.Name = "btn_Luu";
            btn_Luu.Size = new Size(97, 55);
            btn_Luu.TabIndex = 0;
            btn_Luu.Text = "Lưu";
            btn_Luu.UseVisualStyleBackColor = true;
            btn_Luu.Click += button1_Click_1;
            // 
            // flowLayoutP_Luu
            // 
            flowLayoutP_Luu.Controls.Add(btn_Luu);
            flowLayoutP_Luu.Location = new Point(12, 100);
            flowLayoutP_Luu.Name = "flowLayoutP_Luu";
            flowLayoutP_Luu.Size = new Size(102, 60);
            flowLayoutP_Luu.TabIndex = 1;
            // 
            // tableLayoutP_Nhap
            // 
            tableLayoutP_Nhap.Anchor = AnchorStyles.Top;
            tableLayoutP_Nhap.CellBorderStyle = TableLayoutPanelCellBorderStyle.OutsetPartial;
            tableLayoutP_Nhap.ColumnCount = 9;
            tableLayoutP_Nhap.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 26.2626266F));
            tableLayoutP_Nhap.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 73.73737F));
            tableLayoutP_Nhap.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 147F));
            tableLayoutP_Nhap.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 129F));
            tableLayoutP_Nhap.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 161F));
            tableLayoutP_Nhap.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 177F));
            tableLayoutP_Nhap.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 182F));
            tableLayoutP_Nhap.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 144F));
            tableLayoutP_Nhap.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 485F));
            tableLayoutP_Nhap.Controls.Add(tb_LaiThang, 7, 1);
            tableLayoutP_Nhap.Controls.Add(label9, 7, 0);
            tableLayoutP_Nhap.Controls.Add(dtimep_NgayHetHan, 6, 1);
            tableLayoutP_Nhap.Controls.Add(dtimep_NgayVay, 5, 1);
            tableLayoutP_Nhap.Controls.Add(label8, 8, 0);
            tableLayoutP_Nhap.Controls.Add(label7, 6, 0);
            tableLayoutP_Nhap.Controls.Add(label6, 5, 0);
            tableLayoutP_Nhap.Controls.Add(tbox_TienVay, 4, 1);
            tableLayoutP_Nhap.Controls.Add(label5, 4, 0);
            tableLayoutP_Nhap.Controls.Add(tbox_CCCD, 3, 1);
            tableLayoutP_Nhap.Controls.Add(label4, 3, 0);
            tableLayoutP_Nhap.Controls.Add(tbox_SDT, 2, 1);
            tableLayoutP_Nhap.Controls.Add(label3, 2, 0);
            tableLayoutP_Nhap.Controls.Add(label2, 1, 0);
            tableLayoutP_Nhap.Controls.Add(tbox_Ten, 1, 1);
            tableLayoutP_Nhap.Controls.Add(tbox_MaHD, 0, 1);
            tableLayoutP_Nhap.Controls.Add(rtbox_DoCam, 8, 1);
            tableLayoutP_Nhap.Controls.Add(label1, 0, 0);
            tableLayoutP_Nhap.Font = new Font("Calibri", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tableLayoutP_Nhap.Location = new Point(12, 12);
            tableLayoutP_Nhap.Name = "tableLayoutP_Nhap";
            tableLayoutP_Nhap.RowCount = 2;
            tableLayoutP_Nhap.RowStyles.Add(new RowStyle(SizeType.Percent, 27.8688526F));
            tableLayoutP_Nhap.RowStyles.Add(new RowStyle(SizeType.Percent, 72.13115F));
            tableLayoutP_Nhap.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutP_Nhap.Size = new Size(1815, 82);
            tableLayoutP_Nhap.TabIndex = 5;
            tableLayoutP_Nhap.Paint += tableLayoutPanel1_Paint;
            // 
            // dtimep_NgayHetHan
            // 
            dtimep_NgayHetHan.Anchor = AnchorStyles.None;
            dtimep_NgayHetHan.Location = new Point(997, 39);
            dtimep_NgayHetHan.Name = "dtimep_NgayHetHan";
            dtimep_NgayHetHan.Size = new Size(176, 27);
            dtimep_NgayHetHan.TabIndex = 8;
            dtimep_NgayHetHan.Value = new DateTime(2025, 5, 24, 18, 50, 27, 0);
            dtimep_NgayHetHan.ValueChanged += dTimeP_NgayHetHan_ValueChanged;
            // 
            // dtimep_NgayVay
            // 
            dtimep_NgayVay.Anchor = AnchorStyles.None;
            dtimep_NgayVay.Location = new Point(818, 39);
            dtimep_NgayVay.Name = "dtimep_NgayVay";
            dtimep_NgayVay.Size = new Size(168, 27);
            dtimep_NgayVay.TabIndex = 7;
            dtimep_NgayVay.Value = new DateTime(2025, 5, 24, 18, 50, 27, 0);
            dtimep_NgayVay.ValueChanged += dateTimePicker1_ValueChanged;
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.None;
            label8.AutoSize = true;
            label8.Location = new Point(1540, 3);
            label8.Name = "label8";
            label8.Size = new Size(58, 19);
            label8.TabIndex = 14;
            label8.Text = "Đồ cầm";
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.None;
            label7.AutoSize = true;
            label7.Location = new Point(1037, 3);
            label7.Name = "label7";
            label7.Size = new Size(95, 19);
            label7.TabIndex = 12;
            label7.Text = "Ngày hết hạn";
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.None;
            label6.AutoSize = true;
            label6.Location = new Point(868, 3);
            label6.Name = "label6";
            label6.Size = new Size(68, 19);
            label6.TabIndex = 10;
            label6.Text = "Ngày vay";
            // 
            // tbox_TienVay
            // 
            tbox_TienVay.Anchor = AnchorStyles.None;
            tbox_TienVay.Location = new Point(654, 39);
            tbox_TienVay.Name = "tbox_TienVay";
            tbox_TienVay.Size = new Size(153, 27);
            tbox_TienVay.TabIndex = 9;
            tbox_TienVay.Text = "10.000.000.000";
            tbox_TienVay.TextAlign = HorizontalAlignment.Center;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.None;
            label5.AutoSize = true;
            label5.Location = new Point(698, 3);
            label5.Name = "label5";
            label5.Size = new Size(64, 19);
            label5.TabIndex = 8;
            label5.Text = "Tiền Vay";
            label5.Click += label5_Click;
            // 
            // tbox_CCCD
            // 
            tbox_CCCD.Anchor = AnchorStyles.None;
            tbox_CCCD.Location = new Point(524, 39);
            tbox_CCCD.Name = "tbox_CCCD";
            tbox_CCCD.Size = new Size(116, 27);
            tbox_CCCD.TabIndex = 7;
            tbox_CCCD.Text = "072099004244";
            tbox_CCCD.TextAlign = HorizontalAlignment.Center;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.None;
            label4.AutoSize = true;
            label4.Location = new Point(559, 3);
            label4.Name = "label4";
            label4.Size = new Size(46, 19);
            label4.TabIndex = 6;
            label4.Text = "CCCD";
            // 
            // tbox_SDT
            // 
            tbox_SDT.Anchor = AnchorStyles.None;
            tbox_SDT.Location = new Point(376, 39);
            tbox_SDT.Name = "tbox_SDT";
            tbox_SDT.Size = new Size(130, 27);
            tbox_SDT.TabIndex = 5;
            tbox_SDT.Text = "09663466949494";
            tbox_SDT.TextAlign = HorizontalAlignment.Center;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.None;
            label3.AutoSize = true;
            label3.Location = new Point(424, 3);
            label3.Name = "label3";
            label3.Size = new Size(34, 19);
            label3.TabIndex = 4;
            label3.Text = "SĐT";
            label3.Click += label3_Click;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.None;
            label2.AutoSize = true;
            label2.Location = new Point(216, 3);
            label2.Name = "label2";
            label2.Size = new Size(32, 19);
            label2.TabIndex = 1;
            label2.Text = "Tên";
            label2.Click += label2_Click_1;
            // 
            // tbox_Ten
            // 
            tbox_Ten.Anchor = AnchorStyles.None;
            tbox_Ten.Location = new Point(137, 39);
            tbox_Ten.Name = "tbox_Ten";
            tbox_Ten.Size = new Size(191, 27);
            tbox_Ten.TabIndex = 3;
            tbox_Ten.Text = "Nguyễn Trần Trung Quốc";
            tbox_Ten.TextAlign = HorizontalAlignment.Center;
            // 
            // tbox_MaHD
            // 
            tbox_MaHD.Anchor = AnchorStyles.None;
            tbox_MaHD.Location = new Point(23, 39);
            tbox_MaHD.Name = "tbox_MaHD";
            tbox_MaHD.Size = new Size(53, 27);
            tbox_MaHD.TabIndex = 2;
            tbox_MaHD.Text = "00001";
            tbox_MaHD.TextAlign = HorizontalAlignment.Center;
            tbox_MaHD.TextChanged += textBox1_TextChanged_1;
            // 
            // rtbox_DoCam
            // 
            rtbox_DoCam.Anchor = AnchorStyles.None;
            rtbox_DoCam.Location = new Point(1329, 29);
            rtbox_DoCam.Name = "rtbox_DoCam";
            rtbox_DoCam.Size = new Size(480, 47);
            rtbox_DoCam.TabIndex = 16;
            rtbox_DoCam.Text = "Điện thoại, xe máy, cà vẹt, ...";
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.AutoSize = true;
            label1.FlatStyle = FlatStyle.Popup;
            label1.Location = new Point(23, 3);
            label1.Name = "label1";
            label1.Size = new Size(54, 19);
            label1.TabIndex = 0;
            label1.Text = "Mã HĐ";
            label1.Click += label1_Click_1;
            // 
            // label9
            // 
            label9.Anchor = AnchorStyles.None;
            label9.AutoSize = true;
            label9.Location = new Point(1188, 3);
            label9.Name = "label9";
            label9.Size = new Size(126, 19);
            label9.TabIndex = 17;
            label9.Text = "Lãi suất (% tháng)";
            // 
            // tb_LaiThang
            // 
            tb_LaiThang.Anchor = AnchorStyles.None;
            tb_LaiThang.Location = new Point(1214, 39);
            tb_LaiThang.Name = "tb_LaiThang";
            tb_LaiThang.Size = new Size(73, 27);
            tb_LaiThang.TabIndex = 18;
            tb_LaiThang.Text = "5";
            tb_LaiThang.TextAlign = HorizontalAlignment.Center;
            tb_LaiThang.TextChanged += textBox1_TextChanged_2;
            // 
            // dataGridView1
            // 
            dataGridView1.BackgroundColor = SystemColors.ActiveCaption;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(19, 199);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(1808, 714);
            dataGridView1.TabIndex = 6;
            // 
            // ThemHopDongMoi
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.InactiveCaption;
            ClientSize = new Size(1839, 938);
            Controls.Add(dataGridView1);
            Controls.Add(tableLayoutP_Nhap);
            Controls.Add(flowLayoutP_Luu);
            Name = "ThemHopDongMoi";
            Text = "ThemHopDongMoi";
            FormClosing += ThemHopDongMoi_FormClosing;
            Load += ThemHopDongMoi_Load;
            flowLayoutP_Luu.ResumeLayout(false);
            flowLayoutP_Luu.PerformLayout();
            tableLayoutP_Nhap.ResumeLayout(false);
            tableLayoutP_Nhap.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button btn_Luu;
        private FlowLayoutPanel flowLayoutP_Luu;
        private TableLayoutPanel tableLayoutP_Nhap;
        private Label label1;
        private Label label2;
        private TextBox tbox_MaHD;
        private Label label4;
        private TextBox tbox_SDT;
        private Label label3;
        private TextBox tbox_Ten;
        private Label label5;
        private TextBox tbox_CCCD;
        private TextBox tbox_TienVay;
        private Label label8;
        private Label label7;
        private Label label6;
        private RichTextBox rtbox_DoCam;
        private DateTimePicker dtimep_NgayVay;
        private DateTimePicker dtimep_NgayHetHan;
        private TextBox tb_LaiThang;
        private Label label9;
        private DataGridView dataGridView1;
    }
}