namespace QuanLyVayVon.CSDL
{
    partial class QuanLyCSDL
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btn_TaoCSDL = new Button();
            btn_SaoLuu = new Button();
            btn_UploadSaoluu = new Button();
            btn_QuayLai = new Button();
            SuspendLayout();
            // 
            // btn_TaoCSDL
            // 
            btn_TaoCSDL.Location = new Point(12, 76);
            btn_TaoCSDL.Name = "btn_TaoCSDL";
            btn_TaoCSDL.Size = new Size(234, 58);
            btn_TaoCSDL.TabIndex = 0;
            btn_TaoCSDL.Text = "Tạo cơ sở dữ liệu";
            btn_TaoCSDL.UseVisualStyleBackColor = true;
            btn_TaoCSDL.Click += btn_TaoCSDL_Click;
            // 
            // btn_SaoLuu
            // 
            btn_SaoLuu.Location = new Point(12, 14);
            btn_SaoLuu.Name = "btn_SaoLuu";
            btn_SaoLuu.Size = new Size(171, 58);
            btn_SaoLuu.TabIndex = 4;
            btn_SaoLuu.Text = "Sao lưu";
            btn_SaoLuu.UseVisualStyleBackColor = true;
            btn_SaoLuu.Click += btn_SaoLuu_Click;
            // 
            // btn_UploadSaoluu
            // 
            btn_UploadSaoluu.Location = new Point(189, 14);
            btn_UploadSaoluu.Name = "btn_UploadSaoluu";
            btn_UploadSaoluu.Size = new Size(171, 58);
            btn_UploadSaoluu.TabIndex = 5;
            btn_UploadSaoluu.Text = "Tải lên sao lưu có sẵn";
            btn_UploadSaoluu.UseVisualStyleBackColor = true;
            btn_UploadSaoluu.Click += btn_UploadSaoluu_Click;
            // 
            // btn_QuayLai
            // 
            btn_QuayLai.Location = new Point(252, 78);
            btn_QuayLai.Name = "btn_QuayLai";
            btn_QuayLai.Size = new Size(108, 58);
            btn_QuayLai.TabIndex = 6;
            btn_QuayLai.Text = "Quay lại";
            btn_QuayLai.UseVisualStyleBackColor = true;
            btn_QuayLai.Click += btn_QuayLai_Click;
            // 
            // QuanLyCSDL
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(363, 146);
            Controls.Add(btn_QuayLai);
            Controls.Add(btn_UploadSaoluu);
            Controls.Add(btn_SaoLuu);
            Controls.Add(btn_TaoCSDL);
            Name = "QuanLyCSDL";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Cơ sở dữ liệu";
            FormClosing += QuanLyCSDL_FormClosing;
            Load += QuanLyCSDL_Load;
            ResumeLayout(false);
        }

        private Button btn_TaoCSDL;
        private Button btn_SaoLuu;
        private Button btn_UploadSaoluu;
        private Button btn_QuayLai;
    }
}
