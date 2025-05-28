namespace QuanLyVayVon.QuanLyHD
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ThemHopDongMoi));
            sqliteCommand1 = new Microsoft.Data.Sqlite.SqliteCommand();
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
            richTextBox1 = new RichTextBox();
            pictureBox2 = new PictureBox();
            label7 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // sqliteCommand1
            // 
            sqliteCommand1.CommandTimeout = 30;
            sqliteCommand1.Connection = null;
            sqliteCommand1.Transaction = null;
            sqliteCommand1.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Arial", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.Location = new Point(117, 79);
            label1.Margin = new Padding(5, 0, 5, 0);
            label1.Name = "label1";
            label1.Size = new Size(163, 19);
            label1.TabIndex = 0;
            label1.Text = "Thông tin khách hàng";
            label1.Click += label1_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(58, 68);
            pictureBox1.Margin = new Padding(5, 2, 5, 2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(51, 40);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(118, 182);
            label2.Margin = new Padding(5, 0, 5, 0);
            label2.Name = "label2";
            label2.Size = new Size(142, 19);
            label2.TabIndex = 2;
            label2.Text = "Tên khách hàng *";
            // 
            // tbox_Ten
            // 
            tbox_Ten.Font = new Font("Arial", 12F, FontStyle.Italic);
            tbox_Ten.Location = new Point(345, 178);
            tbox_Ten.Margin = new Padding(4, 2, 4, 2);
            tbox_Ten.Name = "tbox_Ten";
            tbox_Ten.Size = new Size(512, 26);
            tbox_Ten.TabIndex = 4;
            tbox_Ten.Text = "Nhập tên";
            tbox_Ten.TextAlign = HorizontalAlignment.Center;
            tbox_Ten.TextChanged += tbox_Ten_TextChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(120, 136);
            label3.Margin = new Padding(5, 0, 5, 0);
            label3.Name = "label3";
            label3.Size = new Size(120, 19);
            label3.TabIndex = 5;
            label3.Text = "Mã hợp đồng *";
            // 
            // tbox_MaHD
            // 
            tbox_MaHD.Font = new Font("Arial", 12F, FontStyle.Italic);
            tbox_MaHD.Location = new Point(345, 129);
            tbox_MaHD.Margin = new Padding(4, 2, 4, 2);
            tbox_MaHD.Name = "tbox_MaHD";
            tbox_MaHD.Size = new Size(513, 26);
            tbox_MaHD.TabIndex = 6;
            tbox_MaHD.Text = "00001";
            tbox_MaHD.TextAlign = HorizontalAlignment.Center;
            // 
            // tbox_CCCD
            // 
            tbox_CCCD.Font = new Font("Arial", 12F, FontStyle.Italic);
            tbox_CCCD.Location = new Point(1196, 182);
            tbox_CCCD.Margin = new Padding(4, 2, 4, 2);
            tbox_CCCD.Name = "tbox_CCCD";
            tbox_CCCD.Size = new Size(498, 26);
            tbox_CCCD.TabIndex = 8;
            tbox_CCCD.Text = "Nhập căn cước công dân/ chứng minh thư/ hộ chiếu";
            tbox_CCCD.TextAlign = HorizontalAlignment.Center;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Arial", 12F, FontStyle.Bold);
            label5.Location = new Point(971, 137);
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
            label6.Location = new Point(120, 226);
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
            label4.Location = new Point(971, 182);
            label4.Margin = new Padding(5, 0, 5, 0);
            label4.Name = "label4";
            label4.Size = new Size(158, 19);
            label4.TabIndex = 12;
            label4.Text = "Số CCCD/ Hộ chiếu";
            // 
            // tbox_SDT
            // 
            tbox_SDT.Font = new Font("Arial", 12F, FontStyle.Italic);
            tbox_SDT.Location = new Point(1196, 134);
            tbox_SDT.Margin = new Padding(4, 2, 4, 2);
            tbox_SDT.Name = "tbox_SDT";
            tbox_SDT.Size = new Size(498, 26);
            tbox_SDT.TabIndex = 13;
            tbox_SDT.Text = "Nhập số điện thoại";
            tbox_SDT.TextAlign = HorizontalAlignment.Center;
            // 
            // richTextBox1
            // 
            richTextBox1.Font = new Font("Arial", 12F, FontStyle.Italic);
            richTextBox1.Location = new Point(345, 226);
            richTextBox1.Margin = new Padding(4, 3, 4, 3);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(513, 136);
            richTextBox1.TabIndex = 14;
            richTextBox1.Text = "Nhập địa chỉ khách hàng";
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(58, 389);
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
            label7.Location = new Point(120, 413);
            label7.Margin = new Padding(5, 0, 5, 0);
            label7.Name = "label7";
            label7.Size = new Size(153, 19);
            label7.TabIndex = 15;
            label7.Text = "Thông tin khoản vay";
            // 
            // ThemHopDongMoi
            // 
            AutoScaleDimensions = new SizeF(9F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(2070, 1048);
            Controls.Add(pictureBox2);
            Controls.Add(label7);
            Controls.Add(richTextBox1);
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
            Name = "ThemHopDongMoi";
            Text = "Thêm hợp đồng mới";
            Load += ThemHopDongMoi_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Microsoft.Data.Sqlite.SqliteCommand sqliteCommand1;
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
        private RichTextBox richTextBox1;
        private PictureBox pictureBox2;
        private Label label7;
    }
}