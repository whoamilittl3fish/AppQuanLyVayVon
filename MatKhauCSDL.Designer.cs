namespace QuanLyVayVon
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
            label1 = new Label();
            tbox_MatKhauCSDL = new TextBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BorderStyle = BorderStyle.Fixed3D;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(37, 9);
            label1.Name = "label1";
            label1.Size = new Size(136, 17);
            label1.TabIndex = 0;
            label1.Text = "Mật khẩu Cơ sở dữ liệu";
            label1.Click += label1_Click;
            // 
            // tbox_MatKhauCSDL
            // 
            tbox_MatKhauCSDL.Location = new Point(37, 40);
            tbox_MatKhauCSDL.Name = "tbox_MatKhauCSDL";
            tbox_MatKhauCSDL.PasswordChar = 'X';
            tbox_MatKhauCSDL.Size = new Size(136, 23);
            tbox_MatKhauCSDL.TabIndex = 1;
            tbox_MatKhauCSDL.TextChanged += tbox_MatKhauCSDL_TextChanged;
            tbox_MatKhauCSDL.KeyDown += tbox_MatKhauCSDL_KeyDown_1;
            // 
            // MatKhau
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(206, 89);
            Controls.Add(tbox_MatKhauCSDL);
            Controls.Add(label1);
            Name = "MatKhau";
            Text = "MẬT KHẨU QUẢN LÝ";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox tbox_MatKhauCSDL;
    }
}