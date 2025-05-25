namespace QuanLyVayVon
{
    partial class TrangChu
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

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button1 = new Button();
            button3 = new Button();
            button2 = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.Location = new Point(35, 191);
            button1.Name = "button1";
            button1.Size = new Size(222, 90);
            button1.TabIndex = 0;
            button1.Text = "Cơ sở dữ liệu";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button3
            // 
            button3.Font = new Font("Arial", 26.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button3.Location = new Point(35, 24);
            button3.Name = "button3";
            button3.Size = new Size(373, 128);
            button3.TabIndex = 2;
            button3.Text = "Hợp đồng";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button2
            // 
            button2.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button2.ForeColor = Color.IndianRed;
            button2.Location = new Point(294, 194);
            button2.Name = "button2";
            button2.Size = new Size(114, 87);
            button2.TabIndex = 3;
            button2.Text = "Thoát";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // TrangChu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(444, 325);
            Controls.Add(button2);
            Controls.Add(button3);
            Controls.Add(button1);
            Name = "TrangChu";
            Text = "Quản Lý Vay Vốn";
            Load += TrangChu_Load;
            ResumeLayout(false);
        }



        private Button button1;
        
        private Button button3;
        private Button button2;
    }

}   