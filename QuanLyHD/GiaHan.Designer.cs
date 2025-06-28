namespace QuanLyVayVon.QuanLyHD
{
    partial class GiaHan
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
            btn_GiaHan = new Button();
            lb_TenKhachHang = new ReaLTaiizor.Controls.DungeonHeaderLabel();
            btn_Thoat = new Button();
            btn_GhiChu = new ReaLTaiizor.Controls.DungeonHeaderLabel();
            tableLayoutPanel1 = new TableLayoutPanel();
            lb_GhiChu = new ReaLTaiizor.Controls.DungeonHeaderLabel();
            rtb_GhiChu = new ReaLTaiizor.Controls.HopeRichTextBox();
            tb_TenKH = new TextBox();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // btn_GiaHan
            // 
            btn_GiaHan.Location = new Point(12, 90);
            btn_GiaHan.Name = "btn_GiaHan";
            btn_GiaHan.Size = new Size(108, 42);
            btn_GiaHan.TabIndex = 0;
            btn_GiaHan.Text = "Gia hạn";
            btn_GiaHan.UseVisualStyleBackColor = true;
            btn_GiaHan.Click += btn_GiaHan_Click;
            // 
            // lb_TenKhachHang
            // 
            lb_TenKhachHang.Anchor = AnchorStyles.Left;
            lb_TenKhachHang.AutoSize = true;
            lb_TenKhachHang.BackColor = Color.Transparent;
            lb_TenKhachHang.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lb_TenKhachHang.ForeColor = Color.FromArgb(76, 76, 77);
            lb_TenKhachHang.Location = new Point(3, 4);
            lb_TenKhachHang.Name = "lb_TenKhachHang";
            lb_TenKhachHang.Size = new Size(118, 20);
            lb_TenKhachHang.TabIndex = 2;
            lb_TenKhachHang.Text = "Tên khách hàng";
            // 
            // btn_Thoat
            // 
            btn_Thoat.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btn_Thoat.Location = new Point(462, 12);
            btn_Thoat.Name = "btn_Thoat";
            btn_Thoat.Size = new Size(52, 43);
            btn_Thoat.TabIndex = 4;
            btn_Thoat.Text = "X";
            btn_Thoat.UseVisualStyleBackColor = true;
            btn_Thoat.Click += btn_Thoat_Click;
            // 
            // btn_GhiChu
            // 
            btn_GhiChu.Anchor = AnchorStyles.Left;
            btn_GhiChu.AutoSize = true;
            btn_GhiChu.BackColor = Color.Transparent;
            btn_GhiChu.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btn_GhiChu.ForeColor = Color.FromArgb(76, 76, 77);
            btn_GhiChu.Location = new Point(593, 164);
            btn_GhiChu.Name = "btn_GhiChu";
            btn_GhiChu.Size = new Size(62, 20);
            btn_GhiChu.TabIndex = 5;
            btn_GhiChu.Text = "Ghi chú";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 139F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(lb_TenKhachHang, 0, 0);
            tableLayoutPanel1.Controls.Add(lb_GhiChu, 0, 1);
            tableLayoutPanel1.Controls.Add(rtb_GhiChu, 1, 1);
            tableLayoutPanel1.Controls.Add(tb_TenKH, 1, 0);
            tableLayoutPanel1.Location = new Point(12, 138);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(502, 190);
            tableLayoutPanel1.TabIndex = 7;
            tableLayoutPanel1.Paint += tableLayoutPanel1_Paint;
            // 
            // lb_GhiChu
            // 
            lb_GhiChu.Anchor = AnchorStyles.Left;
            lb_GhiChu.AutoSize = true;
            lb_GhiChu.BackColor = Color.Transparent;
            lb_GhiChu.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lb_GhiChu.ForeColor = Color.FromArgb(76, 76, 77);
            lb_GhiChu.Location = new Point(3, 99);
            lb_GhiChu.Name = "lb_GhiChu";
            lb_GhiChu.Size = new Size(62, 20);
            lb_GhiChu.TabIndex = 7;
            lb_GhiChu.Text = "Ghi chú";
            // 
            // rtb_GhiChu
            // 
            rtb_GhiChu.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            rtb_GhiChu.BorderColor = Color.FromArgb(220, 223, 230);
            rtb_GhiChu.Font = new Font("Segoe UI", 12F);
            rtb_GhiChu.ForeColor = Color.FromArgb(48, 49, 51);
            rtb_GhiChu.Hint = "";
            rtb_GhiChu.HoverBorderColor = Color.FromArgb(64, 158, 255);
            rtb_GhiChu.Location = new Point(142, 48);
            rtb_GhiChu.MaxLength = 32767;
            rtb_GhiChu.Multiline = true;
            rtb_GhiChu.Name = "rtb_GhiChu";
            rtb_GhiChu.PasswordChar = '\0';
            rtb_GhiChu.ScrollBars = ScrollBars.None;
            rtb_GhiChu.SelectedText = "";
            rtb_GhiChu.SelectionLength = 0;
            rtb_GhiChu.SelectionStart = 0;
            rtb_GhiChu.Size = new Size(357, 122);
            rtb_GhiChu.TabIndex = 6;
            rtb_GhiChu.TabStop = false;
            rtb_GhiChu.UseSystemPasswordChar = false;
            // 
            // tb_TenKH
            // 
            tb_TenKH.Location = new Point(142, 3);
            tb_TenKH.Name = "tb_TenKH";
            tb_TenKH.Size = new Size(357, 23);
            tb_TenKH.TabIndex = 8;
            // 
            // GiaHan
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(526, 340);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(btn_Thoat);
            Controls.Add(btn_GiaHan);
            Name = "GiaHan";
            Text = "GiaHan";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button btn_GiaHan;
        private ReaLTaiizor.Controls.DungeonHeaderLabel lb_TenKhachHang;
        private ReaLTaiizor.Controls.HopeTextBox hopeTextBox1;
        private Button btn_Thoat;
        private ReaLTaiizor.Controls.DungeonHeaderLabel btn_GhiChu;
        private TableLayoutPanel tableLayoutPanel1;
        private ReaLTaiizor.Controls.DungeonHeaderLabel lb_GhiChu;
        private ReaLTaiizor.Controls.HopeRichTextBox rtb_GhiChu;
        private TextBox tb_TenKH;
    }
}