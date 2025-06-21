namespace QuanLyVayVon.QuanLyHD
{
    partial class LichSuDongLai
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LichSuDongLai));
            btn_Tattoan = new Button();
            dataGridView_LichSuDongLai = new DataGridView();
            btn_Thoát = new Button();
            btn_Maxsize = new Button();
            btn_Hide = new Button();
            flowlayout_Button = new FlowLayoutPanel();
            tblayout_Top = new TableLayoutPanel();
            tblayout_form = new TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)dataGridView_LichSuDongLai).BeginInit();
            flowlayout_Button.SuspendLayout();
            tblayout_Top.SuspendLayout();
            tblayout_form.SuspendLayout();
            SuspendLayout();
            // 
            // btn_Tattoan
            // 
            btn_Tattoan.Anchor = AnchorStyles.Right;
            btn_Tattoan.Location = new Point(230, 8);
            btn_Tattoan.Name = "btn_Tattoan";
            btn_Tattoan.Size = new Size(92, 29);
            btn_Tattoan.TabIndex = 4;
            btn_Tattoan.Text = "Chuộc đồ";
            btn_Tattoan.UseVisualStyleBackColor = true;
            btn_Tattoan.Click += btn_Tattoan_Click;
            // 
            // dataGridView_LichSuDongLai
            // 
            dataGridView_LichSuDongLai.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView_LichSuDongLai.Location = new Point(3, 228);
            dataGridView_LichSuDongLai.Name = "dataGridView_LichSuDongLai";
            dataGridView_LichSuDongLai.Size = new Size(1234, 426);
            dataGridView_LichSuDongLai.TabIndex = 5;
            // 
            // btn_Thoát
            // 
            btn_Thoát.Anchor = AnchorStyles.Right;
            btn_Thoát.Location = new Point(518, 3);
            btn_Thoát.Name = "btn_Thoát";
            btn_Thoát.Size = new Size(89, 40);
            btn_Thoát.TabIndex = 6;
            btn_Thoát.Text = "Thoát";
            btn_Thoát.UseVisualStyleBackColor = true;
            btn_Thoát.Click += btn_Thoát_Click_1;
            // 
            // btn_Maxsize
            // 
            btn_Maxsize.Anchor = AnchorStyles.Right;
            btn_Maxsize.Location = new Point(423, 3);
            btn_Maxsize.Name = "btn_Maxsize";
            btn_Maxsize.Size = new Size(89, 40);
            btn_Maxsize.TabIndex = 9;
            btn_Maxsize.Text = "O";
            btn_Maxsize.UseVisualStyleBackColor = true;
            btn_Maxsize.Click += btn_Maxsize_Click;
            // 
            // btn_Hide
            // 
            btn_Hide.Anchor = AnchorStyles.Right;
            btn_Hide.Location = new Point(328, 3);
            btn_Hide.Name = "btn_Hide";
            btn_Hide.Size = new Size(89, 40);
            btn_Hide.TabIndex = 8;
            btn_Hide.Text = "_";
            btn_Hide.UseVisualStyleBackColor = true;
            btn_Hide.Click += btn_Hide_Click;
            // 
            // flowlayout_Button
            // 
            flowlayout_Button.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            flowlayout_Button.Controls.Add(btn_Thoát);
            flowlayout_Button.Controls.Add(btn_Maxsize);
            flowlayout_Button.Controls.Add(btn_Hide);
            flowlayout_Button.Controls.Add(btn_Tattoan);
            flowlayout_Button.FlowDirection = FlowDirection.RightToLeft;
            flowlayout_Button.Location = new Point(621, 3);
            flowlayout_Button.Name = "flowlayout_Button";
            flowlayout_Button.Size = new Size(610, 213);
            flowlayout_Button.TabIndex = 9;
            // 
            // tblayout_Top
            // 
            tblayout_Top.ColumnCount = 2;
            tblayout_Top.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblayout_Top.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblayout_Top.Controls.Add(flowlayout_Button, 1, 0);
            tblayout_Top.Location = new Point(3, 3);
            tblayout_Top.Name = "tblayout_Top";
            tblayout_Top.RowCount = 1;
            tblayout_Top.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tblayout_Top.Size = new Size(1234, 219);
            tblayout_Top.TabIndex = 11;
            // 
            // tblayout_form
            // 
            tblayout_form.ColumnCount = 1;
            tblayout_form.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblayout_form.Controls.Add(dataGridView_LichSuDongLai, 0, 1);
            tblayout_form.Controls.Add(tblayout_Top, 0, 0);
            tblayout_form.Location = new Point(12, 12);
            tblayout_form.Name = "tblayout_form";
            tblayout_form.RowCount = 2;
            tblayout_form.RowStyles.Add(new RowStyle(SizeType.Percent, 34.39878F));
            tblayout_form.RowStyles.Add(new RowStyle(SizeType.Percent, 65.60122F));
            tblayout_form.Size = new Size(1240, 657);
            tblayout_form.TabIndex = 12;
            // 
            // LichSuDongLai
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1264, 681);
            Controls.Add(tblayout_form);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "LichSuDongLai";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "LichSuDongLai";
            Load += LichSuDongLai_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView_LichSuDongLai).EndInit();
            flowlayout_Button.ResumeLayout(false);
            tblayout_Top.ResumeLayout(false);
            tblayout_form.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Button btn_Tattoan;
        private DataGridView dataGridView_LichSuDongLai;
        private Button btn_Thoát;
        private Button btn_Hide;
        private Button btn_Maxsize;
        private FlowLayoutPanel flowlayout_Button;
        private TableLayoutPanel tblayout_Top;
        private TableLayoutPanel tblayout_form;
    }
}