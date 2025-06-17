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
            tableLayoutPanel_info = new TableLayoutPanel();
            lb_info = new Label();
            lb_MaHD = new Label();
            flowLayoutPanel_infoHD = new FlowLayoutPanel();
            btn_Tattoan = new Button();
            dataGridView_LichSuDongLai = new DataGridView();
            btn_Thoát = new Button();
            flow_exit = new FlowLayoutPanel();
            btn_Hide = new Button();
            btn_Maxsize = new Button();
            flowLayoutPanel_infoHD.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView_LichSuDongLai).BeginInit();
            flow_exit.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel_info
            // 
            tableLayoutPanel_info.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            tableLayoutPanel_info.ColumnCount = 2;
            tableLayoutPanel_info.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel_info.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 212F));
            tableLayoutPanel_info.Location = new Point(38, 73);
            tableLayoutPanel_info.Name = "tableLayoutPanel_info";
            tableLayoutPanel_info.RowCount = 4;
            tableLayoutPanel_info.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel_info.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel_info.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel_info.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel_info.Size = new Size(427, 101);
            tableLayoutPanel_info.TabIndex = 0;
            // 
            // lb_info
            // 
            lb_info.AutoSize = true;
            lb_info.Location = new Point(3, 0);
            lb_info.Name = "lb_info";
            lb_info.Size = new Size(43, 15);
            lb_info.TabIndex = 1;
            lb_info.Text = "lb_info";
            // 
            // lb_MaHD
            // 
            lb_MaHD.AutoSize = true;
            lb_MaHD.Location = new Point(52, 0);
            lb_MaHD.Name = "lb_MaHD";
            lb_MaHD.Size = new Size(56, 15);
            lb_MaHD.TabIndex = 2;
            lb_MaHD.Text = "lb_MaHD";
            // 
            // flowLayoutPanel_infoHD
            // 
            flowLayoutPanel_infoHD.Controls.Add(lb_info);
            flowLayoutPanel_infoHD.Controls.Add(lb_MaHD);
            flowLayoutPanel_infoHD.Location = new Point(38, 31);
            flowLayoutPanel_infoHD.Name = "flowLayoutPanel_infoHD";
            flowLayoutPanel_infoHD.Size = new Size(244, 20);
            flowLayoutPanel_infoHD.TabIndex = 3;
            // 
            // btn_Tattoan
            // 
            btn_Tattoan.Location = new Point(38, 215);
            btn_Tattoan.Name = "btn_Tattoan";
            btn_Tattoan.Size = new Size(92, 40);
            btn_Tattoan.TabIndex = 4;
            btn_Tattoan.Text = "Chuộc đồ";
            btn_Tattoan.UseVisualStyleBackColor = true;
            btn_Tattoan.Click += btn_Tattoan_Click;
            // 
            // dataGridView_LichSuDongLai
            // 
            dataGridView_LichSuDongLai.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView_LichSuDongLai.Location = new Point(17, 269);
            dataGridView_LichSuDongLai.Name = "dataGridView_LichSuDongLai";
            dataGridView_LichSuDongLai.Size = new Size(982, 308);
            dataGridView_LichSuDongLai.TabIndex = 5;
            // 
            // btn_Thoát
            // 
            btn_Thoát.Anchor = AnchorStyles.Right;
            btn_Thoát.Location = new Point(193, 3);
            btn_Thoát.Name = "btn_Thoát";
            btn_Thoát.Size = new Size(89, 40);
            btn_Thoát.TabIndex = 6;
            btn_Thoát.Text = "Thoát";
            btn_Thoát.UseVisualStyleBackColor = true;
            btn_Thoát.Click += btn_Thoát_Click_1;
            // 
            // flow_exit
            // 
            flow_exit.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            flow_exit.Controls.Add(btn_Hide);
            flow_exit.Controls.Add(btn_Maxsize);
            flow_exit.Controls.Add(btn_Thoát);
            flow_exit.Location = new Point(710, 12);
            flow_exit.Name = "flow_exit";
            flow_exit.Size = new Size(289, 47);
            flow_exit.TabIndex = 7;
            // 
            // btn_Hide
            // 
            btn_Hide.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btn_Hide.Location = new Point(3, 3);
            btn_Hide.Name = "btn_Hide";
            btn_Hide.Size = new Size(89, 40);
            btn_Hide.TabIndex = 8;
            btn_Hide.Text = "_";
            btn_Hide.UseVisualStyleBackColor = true;
            btn_Hide.Click += btn_Hide_Click;
            // 
            // btn_Maxsize
            // 
            btn_Maxsize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btn_Maxsize.Location = new Point(98, 3);
            btn_Maxsize.Name = "btn_Maxsize";
            btn_Maxsize.Size = new Size(89, 40);
            btn_Maxsize.TabIndex = 9;
            btn_Maxsize.Text = "O";
            btn_Maxsize.UseVisualStyleBackColor = true;
            btn_Maxsize.Click += btn_Maxsize_Click;
            // 
            // LichSuDongLai
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1011, 589);
            Controls.Add(flow_exit);
            Controls.Add(dataGridView_LichSuDongLai);
            Controls.Add(btn_Tattoan);
            Controls.Add(flowLayoutPanel_infoHD);
            Controls.Add(tableLayoutPanel_info);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "LichSuDongLai";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "LichSuDongLai";
            Load += LichSuDongLai_Load;
            flowLayoutPanel_infoHD.ResumeLayout(false);
            flowLayoutPanel_infoHD.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView_LichSuDongLai).EndInit();
            flow_exit.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel_info;
        private Label lb_info;
        private Label lb_MaHD;
        private FlowLayoutPanel flowLayoutPanel_infoHD;
        private Button btn_Tattoan;
        private DataGridView dataGridView_LichSuDongLai;
        private Button btn_Thoát;
        private FlowLayoutPanel flow_exit;
        private Button btn_Hide;
        private Button btn_Maxsize;
    }
}