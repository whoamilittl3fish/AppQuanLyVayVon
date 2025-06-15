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
            tableLayoutPanel_info = new TableLayoutPanel();
            lb_info = new Label();
            lb_MaHD = new Label();
            flowLayoutPanel_infoHD = new FlowLayoutPanel();
            btn_GiaHan = new Button();
            dataGridView_LichSuDongLai = new DataGridView();
            btn_Thoát = new Button();
            flowLayoutPanel_infoHD.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView_LichSuDongLai).BeginInit();
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
            tableLayoutPanel_info.Paint += tableLayoutPanel_info_Paint;
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
            // btn_GiaHan
            // 
            btn_GiaHan.Location = new Point(38, 215);
            btn_GiaHan.Name = "btn_GiaHan";
            btn_GiaHan.Size = new Size(92, 40);
            btn_GiaHan.TabIndex = 4;
            btn_GiaHan.Text = "button1";
            btn_GiaHan.UseVisualStyleBackColor = true;
            btn_GiaHan.Click += btn_GiaHan_Click;
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
            btn_Thoát.Location = new Point(910, 12);
            btn_Thoát.Name = "btn_Thoát";
            btn_Thoát.Size = new Size(89, 40);
            btn_Thoát.TabIndex = 6;
            btn_Thoát.Text = "Thoát";
            btn_Thoát.UseVisualStyleBackColor = true;
            btn_Thoát.Click += btn_Thoát_Click;
            // 
            // LichSuDongLai
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1011, 589);
            Controls.Add(btn_Thoát);
            Controls.Add(dataGridView_LichSuDongLai);
            Controls.Add(btn_GiaHan);
            Controls.Add(flowLayoutPanel_infoHD);
            Controls.Add(tableLayoutPanel_info);
            Name = "LichSuDongLai";
            Text = "LichSuDongLai";
            Load += LichSuDongLai_Load;
            flowLayoutPanel_infoHD.ResumeLayout(false);
            flowLayoutPanel_infoHD.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView_LichSuDongLai).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel_info;
        private Label lb_info;
        private Label lb_MaHD;
        private FlowLayoutPanel flowLayoutPanel_infoHD;
        private Button btn_GiaHan;
        private DataGridView dataGridView_LichSuDongLai;
        private Button btn_Thoát;
    }
}