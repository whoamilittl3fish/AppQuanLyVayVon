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
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            flowLayoutPanel_UseForm = new FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)dataGridView_LichSuDongLai).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            flowLayoutPanel_UseForm.SuspendLayout();
            SuspendLayout();
            // 
            // btn_Tattoan
            // 
            btn_Tattoan.Anchor = AnchorStyles.Right;
            btn_Tattoan.Location = new Point(231, 8);
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
            dataGridView_LichSuDongLai.Location = new Point(3, 238);
            dataGridView_LichSuDongLai.Name = "dataGridView_LichSuDongLai";
            dataGridView_LichSuDongLai.Size = new Size(1234, 416);
            dataGridView_LichSuDongLai.TabIndex = 5;
            // 
            // btn_Thoát
            // 
            btn_Thoát.Anchor = AnchorStyles.Right;
            btn_Thoát.Location = new Point(519, 3);
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
            btn_Maxsize.Location = new Point(424, 3);
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
            btn_Hide.Location = new Point(329, 3);
            btn_Hide.Name = "btn_Hide";
            btn_Hide.Size = new Size(89, 40);
            btn_Hide.TabIndex = 8;
            btn_Hide.Text = "_";
            btn_Hide.UseVisualStyleBackColor = true;
            btn_Hide.Click += btn_Hide_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(dataGridView_LichSuDongLai, 0, 1);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 0);
            tableLayoutPanel1.Location = new Point(12, 12);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 35.7686462F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 64.23135F));
            tableLayoutPanel1.Size = new Size(1240, 657);
            tableLayoutPanel1.TabIndex = 8;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(flowLayoutPanel_UseForm, 1, 0);
            tableLayoutPanel2.Location = new Point(3, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 75F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new Size(1234, 215);
            tableLayoutPanel2.TabIndex = 9;
            // 
            // flowLayoutPanel_UseForm
            // 
            flowLayoutPanel_UseForm.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            flowLayoutPanel_UseForm.Controls.Add(btn_Thoát);
            flowLayoutPanel_UseForm.Controls.Add(btn_Maxsize);
            flowLayoutPanel_UseForm.Controls.Add(btn_Hide);
            flowLayoutPanel_UseForm.Controls.Add(btn_Tattoan);
            flowLayoutPanel_UseForm.FlowDirection = FlowDirection.RightToLeft;
            flowLayoutPanel_UseForm.Location = new Point(620, 3);
            flowLayoutPanel_UseForm.Name = "flowLayoutPanel_UseForm";
            flowLayoutPanel_UseForm.Size = new Size(611, 69);
            flowLayoutPanel_UseForm.TabIndex = 9;
            // 
            // LichSuDongLai
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1264, 681);
            Controls.Add(tableLayoutPanel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "LichSuDongLai";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "LichSuDongLai";
            Load += LichSuDongLai_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView_LichSuDongLai).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            flowLayoutPanel_UseForm.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Button btn_Tattoan;
        private DataGridView dataGridView_LichSuDongLai;
        private Button btn_Thoát;
        private Button btn_Hide;
        private Button btn_Maxsize;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private FlowLayoutPanel flowLayoutPanel_UseForm;
    }
}