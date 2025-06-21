namespace QuanLyVayVon
{
    partial class License
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
            rtb_Key = new RichTextBox();
            btn_Active = new Button();
            btn_Thoat = new Button();
            SuspendLayout();
            // 
            // rtb_Key
            // 
            rtb_Key.Location = new Point(45, 12);
            rtb_Key.Name = "rtb_Key";
            rtb_Key.Size = new Size(333, 155);
            rtb_Key.TabIndex = 0;
            rtb_Key.Text = "";
            // 
            // btn_Active
            // 
            btn_Active.Location = new Point(45, 173);
            btn_Active.Name = "btn_Active";
            btn_Active.Size = new Size(204, 48);
            btn_Active.TabIndex = 1;
            btn_Active.Text = "Kích hoạt";
            btn_Active.UseVisualStyleBackColor = true;
            btn_Active.Click += button1_Click;
            // 
            // btn_Thoat
            // 
            btn_Thoat.Location = new Point(255, 173);
            btn_Thoat.Name = "btn_Thoat";
            btn_Thoat.Size = new Size(123, 48);
            btn_Thoat.TabIndex = 2;
            btn_Thoat.Text = "Thoát";
            btn_Thoat.UseVisualStyleBackColor = true;
            btn_Thoat.Click += btn_Thoat_Click;
            // 
            // License
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(421, 233);
            Controls.Add(btn_Thoat);
            Controls.Add(btn_Active);
            Controls.Add(rtb_Key);
            Name = "License";
            Text = "License";
            Load += License_Load;
            ResumeLayout(false);
        }

        #endregion

        private RichTextBox rtb_Key;
        private Button btn_Active;
        private Button btn_Thoat;
    }
}