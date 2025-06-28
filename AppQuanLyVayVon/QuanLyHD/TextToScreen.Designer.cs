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
            btn_XacNhan = new Button();
            tblayout_top = new TableLayoutPanel();
            tblayout_topleft = new TableLayoutPanel();
            tb_form = new TableLayoutPanel();
            tblayout_top.SuspendLayout();
            tblayout_topleft.SuspendLayout();
            tb_form.SuspendLayout();
            SuspendLayout();
            // 
            // rtb_Text
            // 
            rtb_Text.Location = new Point(3, 112);
            rtb_Text.Name = "rtb_Text";
            rtb_Text.Size = new Size(1234, 542);
            rtb_Text.TabIndex = 0;
            rtb_Text.Text = "";
            // 
            // btn_Exit
            // 
            btn_Exit.Anchor = AnchorStyles.None;
            btn_Exit.Location = new Point(1053, 27);
            btn_Exit.Name = "btn_Exit";
            btn_Exit.Size = new Size(46, 46);
            btn_Exit.TabIndex = 1;
            btn_Exit.Text = "X";
            btn_Exit.UseVisualStyleBackColor = true;
            btn_Exit.Click += button1_Click;
            // 
            // rtb_TieuDe
            // 
            rtb_TieuDe.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            rtb_TieuDe.Location = new Point(177, 3);
            rtb_TieuDe.Name = "rtb_TieuDe";
            rtb_TieuDe.Size = new Size(559, 40);
            rtb_TieuDe.TabIndex = 2;
            rtb_TieuDe.Text = "";
            // 
            // btn_XacNhan
            // 
            btn_XacNhan.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            btn_XacNhan.Location = new Point(3, 49);
            btn_XacNhan.Name = "btn_XacNhan";
            btn_XacNhan.Size = new Size(75, 43);
            btn_XacNhan.TabIndex = 3;
            btn_XacNhan.Text = "Xác nhận";
            btn_XacNhan.UseVisualStyleBackColor = true;
            btn_XacNhan.Click += btn_XacNhan_Click;
            // 
            // tblayout_top
            // 
            tblayout_top.ColumnCount = 2;
            tblayout_top.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 74.5454559F));
            tblayout_top.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25.454546F));
            tblayout_top.Controls.Add(tblayout_topleft, 0, 0);
            tblayout_top.Controls.Add(btn_Exit, 1, 0);
            tblayout_top.Location = new Point(3, 3);
            tblayout_top.Name = "tblayout_top";
            tblayout_top.RowCount = 1;
            tblayout_top.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tblayout_top.Size = new Size(1234, 101);
            tblayout_top.TabIndex = 4;
            // 
            // tblayout_topleft
            // 
            tblayout_topleft.ColumnCount = 1;
            tblayout_topleft.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblayout_topleft.Controls.Add(btn_XacNhan, 0, 1);
            tblayout_topleft.Controls.Add(rtb_TieuDe, 0, 0);
            tblayout_topleft.Location = new Point(3, 3);
            tblayout_topleft.Name = "tblayout_topleft";
            tblayout_topleft.RowCount = 2;
            tblayout_topleft.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tblayout_topleft.RowStyles.Add(new RowStyle(SizeType.Absolute, 49F));
            tblayout_topleft.Size = new Size(913, 95);
            tblayout_topleft.TabIndex = 5;
            // 
            // tb_form
            // 
            tb_form.ColumnCount = 1;
            tb_form.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tb_form.Controls.Add(tblayout_top, 0, 0);
            tb_form.Controls.Add(rtb_Text, 0, 1);
            tb_form.Location = new Point(12, 12);
            tb_form.Name = "tb_form";
            tb_form.RowCount = 2;
            tb_form.RowStyles.Add(new RowStyle(SizeType.Percent, 16.74277F));
            tb_form.RowStyles.Add(new RowStyle(SizeType.Percent, 83.25723F));
            tb_form.Size = new Size(1240, 657);
            tb_form.TabIndex = 5;
            // 
            // TextToScreen
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1264, 681);
            Controls.Add(tb_form);
            Name = "TextToScreen";
            tblayout_top.ResumeLayout(false);
            tblayout_topleft.ResumeLayout(false);
            tb_form.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private RichTextBox rtb_Text;
        private Button btn_Exit;
        private RichTextBox rtb_TieuDe;
        private Button btn_XacNhan;
        private TableLayoutPanel tblayout_top;
        private TableLayoutPanel tblayout_topleft;
        private TableLayoutPanel tb_form;
    }
}