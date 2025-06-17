namespace QuanLyVayVon.QuanLyHD
{
    partial class TextToScreen
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
            rtb_Text = new RichTextBox();
            btn_Exit = new Button();
            rtb_TieuDe = new RichTextBox();
            SuspendLayout();
            // 
            // rtb_Text
            // 
            rtb_Text.Location = new Point(4, 72);
            rtb_Text.Name = "rtb_Text";
            rtb_Text.Size = new Size(792, 375);
            rtb_Text.TabIndex = 0;
            rtb_Text.Text = "";
            // 
            // btn_Exit
            // 
            btn_Exit.Location = new Point(713, 12);
            btn_Exit.Name = "btn_Exit";
            btn_Exit.Size = new Size(75, 23);
            btn_Exit.TabIndex = 1;
            btn_Exit.Text = "button1";
            btn_Exit.UseVisualStyleBackColor = true;
            btn_Exit.Click += button1_Click;
            // 
            // rtb_TieuDe
            // 
            rtb_TieuDe.Location = new Point(180, 12);
            rtb_TieuDe.Name = "rtb_TieuDe";
            rtb_TieuDe.Size = new Size(435, 32);
            rtb_TieuDe.TabIndex = 2;
            rtb_TieuDe.Text = "";
            // 
            // TextToScreen
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(rtb_TieuDe);
            Controls.Add(btn_Exit);
            Controls.Add(rtb_Text);
            Name = "TextToScreen";
            ResumeLayout(false);
        }

        #endregion

        private RichTextBox rtb_Text;
        private Button btn_Exit;
        private RichTextBox rtb_TieuDe;
    }
}