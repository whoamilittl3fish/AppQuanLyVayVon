namespace QuanLyVayVon.QuanLyHD
{
    partial class ThongKe
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
            rtb_TongLaiThuTrongThang = new ReaLTaiizor.Controls.HopeRichTextBox();
            lb_TongLaiDaThu = new ReaLTaiizor.Controls.MaterialLabel();
            lb_TienDangChoVay = new ReaLTaiizor.Controls.MaterialLabel();
            rtb_TienDangChoVay = new ReaLTaiizor.Controls.HopeRichTextBox();
            SuspendLayout();
            // 
            // rtb_TongLaiThuTrongThang
            // 
            rtb_TongLaiThuTrongThang.BorderColor = Color.FromArgb(220, 223, 230);
            rtb_TongLaiThuTrongThang.Font = new Font("Segoe UI", 12F);
            rtb_TongLaiThuTrongThang.ForeColor = Color.FromArgb(48, 49, 51);
            rtb_TongLaiThuTrongThang.Hint = "";
            rtb_TongLaiThuTrongThang.HoverBorderColor = Color.FromArgb(64, 158, 255);
            rtb_TongLaiThuTrongThang.Location = new Point(76, 217);
            rtb_TongLaiThuTrongThang.MaxLength = 32767;
            rtb_TongLaiThuTrongThang.Multiline = true;
            rtb_TongLaiThuTrongThang.Name = "rtb_TongLaiThuTrongThang";
            rtb_TongLaiThuTrongThang.PasswordChar = '\0';
            rtb_TongLaiThuTrongThang.ScrollBars = ScrollBars.None;
            rtb_TongLaiThuTrongThang.SelectedText = "";
            rtb_TongLaiThuTrongThang.SelectionLength = 0;
            rtb_TongLaiThuTrongThang.SelectionStart = 0;
            rtb_TongLaiThuTrongThang.Size = new Size(198, 47);
            rtb_TongLaiThuTrongThang.TabIndex = 0;
            rtb_TongLaiThuTrongThang.TabStop = false;
            rtb_TongLaiThuTrongThang.Text = "rtb_TongLaiDaThu";
            rtb_TongLaiThuTrongThang.UseSystemPasswordChar = false;
            // 
            // lb_TongLaiDaThu
            // 
            lb_TongLaiDaThu.AutoSize = true;
            lb_TongLaiDaThu.Depth = 0;
            lb_TongLaiDaThu.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            lb_TongLaiDaThu.Location = new Point(76, 195);
            lb_TongLaiDaThu.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            lb_TongLaiDaThu.Name = "lb_TongLaiDaThu";
            lb_TongLaiDaThu.Size = new Size(198, 19);
            lb_TongLaiDaThu.TabIndex = 1;
            lb_TongLaiDaThu.Text = "LÃI ĐÃ THU TRONG THÁNG";
            // 
            // lb_TienDangChoVay
            // 
            lb_TienDangChoVay.AutoSize = true;
            lb_TienDangChoVay.Depth = 0;
            lb_TienDangChoVay.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            lb_TienDangChoVay.Location = new Point(76, 302);
            lb_TienDangChoVay.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            lb_TienDangChoVay.Name = "lb_TienDangChoVay";
            lb_TienDangChoVay.Size = new Size(152, 19);
            lb_TienDangChoVay.TabIndex = 2;
            lb_TienDangChoVay.Text = "TIỀN ĐANG CHO VAY";
            // 
            // rtb_TienDangChoVay
            // 
            rtb_TienDangChoVay.BorderColor = Color.FromArgb(220, 223, 230);
            rtb_TienDangChoVay.Font = new Font("Segoe UI", 12F);
            rtb_TienDangChoVay.ForeColor = Color.FromArgb(48, 49, 51);
            rtb_TienDangChoVay.Hint = "";
            rtb_TienDangChoVay.HoverBorderColor = Color.FromArgb(64, 158, 255);
            rtb_TienDangChoVay.Location = new Point(76, 334);
            rtb_TienDangChoVay.MaxLength = 32767;
            rtb_TienDangChoVay.Multiline = true;
            rtb_TienDangChoVay.Name = "rtb_TienDangChoVay";
            rtb_TienDangChoVay.PasswordChar = '\0';
            rtb_TienDangChoVay.ScrollBars = ScrollBars.None;
            rtb_TienDangChoVay.SelectedText = "";
            rtb_TienDangChoVay.SelectionLength = 0;
            rtb_TienDangChoVay.SelectionStart = 0;
            rtb_TienDangChoVay.Size = new Size(198, 47);
            rtb_TienDangChoVay.TabIndex = 3;
            rtb_TienDangChoVay.TabStop = false;
            rtb_TienDangChoVay.Text = "rtb_DangChoVay";
            rtb_TienDangChoVay.UseSystemPasswordChar = false;
            // 
            // ThongKe
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(rtb_TienDangChoVay);
            Controls.Add(lb_TienDangChoVay);
            Controls.Add(lb_TongLaiDaThu);
            Controls.Add(rtb_TongLaiThuTrongThang);
            Name = "ThongKe";
            Text = "ThongKeTien";
            Load += ThongKeTien_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ReaLTaiizor.Controls.HopeRichTextBox rtb_TongLaiThuTrongThang;
        private ReaLTaiizor.Controls.MaterialLabel lb_TongLaiDaThu;
        private ReaLTaiizor.Controls.MaterialLabel lb_TienDangChoVay;
        private ReaLTaiizor.Controls.HopeRichTextBox rtb_TienDangChoVay;
    }
}