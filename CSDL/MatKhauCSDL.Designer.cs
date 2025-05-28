namespace QuanLyVayVon.CSDL
{
    partial class MatKhauCSDL
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
            tbox_MatKhauCSDL = new TextBox();
            btn_QuayLai = new Button();
            btn_DangNhapCSDL = new Button();
            SuspendLayout();
            // 
            // tbox_MatKhauCSDL
            // 
            tbox_MatKhauCSDL.Location = new Point(37, 40);
            tbox_MatKhauCSDL.Name = "tbox_MatKhauCSDL";
            tbox_MatKhauCSDL.PasswordChar = 'X';
            tbox_MatKhauCSDL.Size = new Size(185, 23);
            tbox_MatKhauCSDL.TabIndex = 1;
            tbox_MatKhauCSDL.TextChanged += tbox_MatKhauCSDL_TextChanged;
            tbox_MatKhauCSDL.KeyDown += tbox_MatKhauCSDL_KeyDown_1;
            // 
            // btn_QuayLai
            // 
            btn_QuayLai.Location = new Point(147, 69);
            btn_QuayLai.Name = "btn_QuayLai";
            btn_QuayLai.Size = new Size(75, 23);
            btn_QuayLai.TabIndex = 2;
            btn_QuayLai.Text = "Quay lại";
            btn_QuayLai.UseVisualStyleBackColor = true;
            btn_QuayLai.Click += btn_QuayLai_Click;
            // 
            // btn_DangNhapCSDL
            // 
            btn_DangNhapCSDL.Location = new Point(37, 69);
            btn_DangNhapCSDL.Name = "btn_DangNhapCSDL";
            btn_DangNhapCSDL.Size = new Size(104, 23);
            btn_DangNhapCSDL.TabIndex = 3;
            btn_DangNhapCSDL.Text = "Đăng nhập";
            btn_DangNhapCSDL.UseVisualStyleBackColor = true;
            btn_DangNhapCSDL.Click += btn_DangNhapCSDL_Click;
            // 
            // MatKhauCSDLForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(254, 113);
            Controls.Add(btn_DangNhapCSDL);
            Controls.Add(btn_QuayLai);
            Controls.Add(tbox_MatKhauCSDL);
            Name = "MatKhauCSDLForm";
            Text = "MẬT KHẨU QUẢN LÝ";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox tbox_MatKhauCSDL;
        private Button btn_QuayLai;
        private Button btn_DangNhapCSDL;
    }
}
