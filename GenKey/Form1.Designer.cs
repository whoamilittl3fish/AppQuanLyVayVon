namespace GenKey
{
    partial class Form1
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
            this.rtb_Key = new System.Windows.Forms.RichTextBox();
            this.btn_Genkey = new System.Windows.Forms.Button();
            this.cbb_LoaiKey = new System.Windows.Forms.ComboBox();
            this.tb_TimeTrial = new System.Windows.Forms.TextBox();
            this.btn_Copy = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rtb_Key
            // 
            this.rtb_Key.Location = new System.Drawing.Point(12, 12);
            this.rtb_Key.Name = "rtb_Key";
            this.rtb_Key.Size = new System.Drawing.Size(296, 67);
            this.rtb_Key.TabIndex = 0;
            this.rtb_Key.Text = "";
            this.rtb_Key.TextChanged += new System.EventHandler(this.rtb_Key_TextChanged);
            // 
            // btn_Genkey
            // 
            this.btn_Genkey.Location = new System.Drawing.Point(12, 152);
            this.btn_Genkey.Name = "btn_Genkey";
            this.btn_Genkey.Size = new System.Drawing.Size(115, 56);
            this.btn_Genkey.TabIndex = 1;
            this.btn_Genkey.Text = "Gen key";
            this.btn_Genkey.UseVisualStyleBackColor = true;
            this.btn_Genkey.Click += new System.EventHandler(this.btn_Genkey_Click);
            // 
            // cbb_LoaiKey
            // 
            this.cbb_LoaiKey.FormattingEnabled = true;
            this.cbb_LoaiKey.Location = new System.Drawing.Point(12, 101);
            this.cbb_LoaiKey.Name = "cbb_LoaiKey";
            this.cbb_LoaiKey.Size = new System.Drawing.Size(96, 21);
            this.cbb_LoaiKey.TabIndex = 2;
            // 
            // tb_TimeTrial
            // 
            this.tb_TimeTrial.Location = new System.Drawing.Point(207, 102);
            this.tb_TimeTrial.Name = "tb_TimeTrial";
            this.tb_TimeTrial.Size = new System.Drawing.Size(100, 20);
            this.tb_TimeTrial.TabIndex = 3;
            // 
            // btn_Copy
            // 
            this.btn_Copy.Location = new System.Drawing.Point(192, 152);
            this.btn_Copy.Name = "btn_Copy";
            this.btn_Copy.Size = new System.Drawing.Size(115, 56);
            this.btn_Copy.TabIndex = 4;
            this.btn_Copy.Text = "Copy";
            this.btn_Copy.UseVisualStyleBackColor = true;
            this.btn_Copy.Click += new System.EventHandler(this.btn_Copy_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(319, 222);
            this.Controls.Add(this.btn_Copy);
            this.Controls.Add(this.tb_TimeTrial);
            this.Controls.Add(this.cbb_LoaiKey);
            this.Controls.Add(this.btn_Genkey);
            this.Controls.Add(this.rtb_Key);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtb_Key;
        private System.Windows.Forms.Button btn_Genkey;
        private System.Windows.Forms.ComboBox cbb_LoaiKey;
        private System.Windows.Forms.TextBox tb_TimeTrial;
        private System.Windows.Forms.Button btn_Copy;
    }
}

