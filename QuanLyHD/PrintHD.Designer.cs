namespace QuanLyVayVon.QuanLyHD
{
    partial class PrintHD
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
            tb_ID = new TextBox();
            btn_Search = new Button();
            tb_TenNguoiDaiDien = new TextBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            lb_ID = new Label();
            lb_TenNguoiDaiDien = new Label();
            lb_TenKH = new Label();
            tb_TenKH = new TextBox();
            btn_In = new Button();
            btn_Thoat = new Button();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tb_ID
            // 
            tb_ID.Anchor = AnchorStyles.Right;
            tb_ID.Location = new Point(129, 12);
            tb_ID.Name = "tb_ID";
            tb_ID.Size = new Size(190, 23);
            tb_ID.TabIndex = 0;
            tb_ID.Text = "1";
            // 
            // btn_Search
            // 
            btn_Search.Anchor = AnchorStyles.Right;
            btn_Search.Location = new Point(325, 12);
            btn_Search.Name = "btn_Search";
            btn_Search.Size = new Size(75, 23);
            btn_Search.TabIndex = 1;
            btn_Search.Text = "button1";
            btn_Search.UseVisualStyleBackColor = true;
            // 
            // tb_TenNguoiDaiDien
            // 
            tb_TenNguoiDaiDien.Anchor = AnchorStyles.Right;
            tb_TenNguoiDaiDien.Location = new Point(129, 59);
            tb_TenNguoiDaiDien.Name = "tb_TenNguoiDaiDien";
            tb_TenNguoiDaiDien.Size = new Size(190, 23);
            tb_TenNguoiDaiDien.TabIndex = 2;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.Controls.Add(lb_TenNguoiDaiDien, 0, 1);
            tableLayoutPanel1.Controls.Add(btn_Search, 2, 0);
            tableLayoutPanel1.Controls.Add(tb_TenNguoiDaiDien, 1, 1);
            tableLayoutPanel1.Controls.Add(tb_ID, 1, 0);
            tableLayoutPanel1.Controls.Add(lb_ID, 0, 0);
            tableLayoutPanel1.Controls.Add(lb_TenKH, 0, 2);
            tableLayoutPanel1.Controls.Add(tb_TenKH, 1, 2);
            tableLayoutPanel1.Location = new Point(12, 62);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(403, 142);
            tableLayoutPanel1.TabIndex = 3;
            // 
            // lb_ID
            // 
            lb_ID.Anchor = AnchorStyles.Left;
            lb_ID.AutoSize = true;
            lb_ID.Location = new Point(3, 16);
            lb_ID.Name = "lb_ID";
            lb_ID.Size = new Size(97, 15);
            lb_ID.TabIndex = 3;
            lb_ID.Text = "ID thông tin tiệm";
            // 
            // lb_TenNguoiDaiDien
            // 
            lb_TenNguoiDaiDien.Anchor = AnchorStyles.Left;
            lb_TenNguoiDaiDien.AutoSize = true;
            lb_TenNguoiDaiDien.Location = new Point(3, 63);
            lb_TenNguoiDaiDien.Name = "lb_TenNguoiDaiDien";
            lb_TenNguoiDaiDien.Size = new Size(105, 15);
            lb_TenNguoiDaiDien.TabIndex = 4;
            lb_TenNguoiDaiDien.Text = "Tên người đại diện";
            // 
            // lb_TenKH
            // 
            lb_TenKH.Anchor = AnchorStyles.Left;
            lb_TenKH.AutoSize = true;
            lb_TenKH.Location = new Point(3, 110);
            lb_TenKH.Name = "lb_TenKH";
            lb_TenKH.Size = new Size(91, 15);
            lb_TenKH.TabIndex = 5;
            lb_TenKH.Text = "Tên khách hàng";
            // 
            // tb_TenKH
            // 
            tb_TenKH.Anchor = AnchorStyles.Right;
            tb_TenKH.Location = new Point(129, 106);
            tb_TenKH.Name = "tb_TenKH";
            tb_TenKH.Size = new Size(190, 23);
            tb_TenKH.TabIndex = 6;
            // 
            // btn_In
            // 
            btn_In.Location = new Point(12, 15);
            btn_In.Name = "btn_In";
            btn_In.Size = new Size(75, 32);
            btn_In.TabIndex = 7;
            btn_In.Text = "IN";
            btn_In.UseVisualStyleBackColor = true;
            btn_In.Click += btn_In_Click;
            // 
            // btn_Thoat
            // 
            btn_Thoat.Location = new Point(372, 12);
            btn_Thoat.Name = "btn_Thoat";
            btn_Thoat.Size = new Size(43, 35);
            btn_Thoat.TabIndex = 8;
            btn_Thoat.Text = "X";
            btn_Thoat.UseVisualStyleBackColor = true;
            // 
            // PrintHD
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(427, 216);
            Controls.Add(btn_Thoat);
            Controls.Add(btn_In);
            Controls.Add(tableLayoutPanel1);
            Name = "PrintHD";
            Text = "PrintHD";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TextBox tb_ID;
        private Button btn_Search;
        private TextBox tb_TenNguoiDaiDien;
        private TableLayoutPanel tableLayoutPanel1;
        private Label lb_TenNguoiDaiDien;
        private Label lb_ID;
        private Label lb_TenKH;
        private TextBox tb_TenKH;
        private Button btn_In;
        private Button btn_Thoat;
    }
}