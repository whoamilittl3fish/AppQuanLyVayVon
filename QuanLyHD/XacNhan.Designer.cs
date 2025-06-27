namespace QuanLyVayVon.QuanLyHD
{
    partial class XacNhan
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
            tb_MatKhau = new TextBox();
            btn_XacNhan = new Button();
            btn_Thoat = new Button();
            SuspendLayout();
            // 
            // tb_MatKhau
            // 
            tb_MatKhau.Location = new Point(33, 45);
            tb_MatKhau.Name = "tb_MatKhau";
            tb_MatKhau.Size = new Size(206, 23);
            tb_MatKhau.TabIndex = 0;
            tb_MatKhau.TextChanged += tb_MatKhau_TextChanged;
            // 
            // btn_XacNhan
            // 
            btn_XacNhan.Location = new Point(12, 135);
            btn_XacNhan.Name = "btn_XacNhan";
            btn_XacNhan.Size = new Size(75, 23);
            btn_XacNhan.TabIndex = 2;
            btn_XacNhan.Text = "Xác nhận";
            btn_XacNhan.UseVisualStyleBackColor = true;
            btn_XacNhan.Click += btn_XacNhan_Click;
            // 
            // btn_Thoat
            // 
            btn_Thoat.Location = new Point(191, 135);
            btn_Thoat.Name = "btn_Thoat";
            btn_Thoat.Size = new Size(75, 23);
            btn_Thoat.TabIndex = 3;
            btn_Thoat.Text = "Thoát";
            btn_Thoat.UseVisualStyleBackColor = true;
            btn_Thoat.Click += btn_Thoat_Click;
            // 
            // XacNhan
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(278, 170);
            Controls.Add(btn_Thoat);
            Controls.Add(btn_XacNhan);
            Controls.Add(tb_MatKhau);
            Name = "XacNhan";
            Text = "XacNhan";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox tb_MatKhau;
        private Button btn_XacNhan;
        private Button btn_Thoat;
    }
}