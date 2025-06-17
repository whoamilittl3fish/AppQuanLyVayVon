
namespace QuanLyVayVon.QuanLyHD
{
    partial class ChuocDoForm
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
            dtp_NgayChuocDo = new DateTimePicker();
            tableLayoutPanel1 = new TableLayoutPanel();
            tb_TongTienChuoc = new TextBox();
            lb_TongTienChuoc = new Label();
            lb_TienKhac = new Label();
            tb_Lai = new TextBox();
            lb_Lai = new Label();
            lb_TienCam = new Label();
            lb_NgayChuocDo = new Label();
            tb_TienVay = new TextBox();
            tb_TienKhac = new TextBox();
            btn_QuayLai = new Button();
            Btn_Luu = new Button();
            rtb_TieuDe = new RichTextBox();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // dtp_NgayChuocDo
            // 
            dtp_NgayChuocDo.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            dtp_NgayChuocDo.Location = new Point(216, 6);
            dtp_NgayChuocDo.Name = "dtp_NgayChuocDo";
            dtp_NgayChuocDo.Size = new Size(321, 23);
            dtp_NgayChuocDo.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 39.4444427F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60.5555573F));
            tableLayoutPanel1.Controls.Add(tb_TongTienChuoc, 1, 4);
            tableLayoutPanel1.Controls.Add(lb_TongTienChuoc, 0, 4);
            tableLayoutPanel1.Controls.Add(lb_TienKhac, 0, 3);
            tableLayoutPanel1.Controls.Add(tb_Lai, 1, 2);
            tableLayoutPanel1.Controls.Add(lb_Lai, 0, 2);
            tableLayoutPanel1.Controls.Add(lb_TienCam, 0, 1);
            tableLayoutPanel1.Controls.Add(dtp_NgayChuocDo, 1, 0);
            tableLayoutPanel1.Controls.Add(lb_NgayChuocDo, 0, 0);
            tableLayoutPanel1.Controls.Add(tb_TienVay, 1, 1);
            tableLayoutPanel1.Controls.Add(tb_TienKhac, 1, 3);
            tableLayoutPanel1.Location = new Point(47, 94);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 5;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 38.0597F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 61.9403F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 53F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 59F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 73F));
            tableLayoutPanel1.Size = new Size(540, 280);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // tb_TongTienChuoc
            // 
            tb_TongTienChuoc.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            tb_TongTienChuoc.Location = new Point(216, 231);
            tb_TongTienChuoc.Name = "tb_TongTienChuoc";
            tb_TongTienChuoc.Size = new Size(321, 23);
            tb_TongTienChuoc.TabIndex = 9;
            // 
            // lb_TongTienChuoc
            // 
            lb_TongTienChuoc.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lb_TongTienChuoc.AutoSize = true;
            lb_TongTienChuoc.Location = new Point(3, 235);
            lb_TongTienChuoc.Name = "lb_TongTienChuoc";
            lb_TongTienChuoc.Size = new Size(207, 15);
            lb_TongTienChuoc.TabIndex = 8;
            lb_TongTienChuoc.Text = "Tổng tiền chuộc";
            // 
            // lb_TienKhac
            // 
            lb_TienKhac.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lb_TienKhac.AutoSize = true;
            lb_TienKhac.Location = new Point(3, 169);
            lb_TienKhac.Name = "lb_TienKhac";
            lb_TienKhac.Size = new Size(207, 15);
            lb_TienKhac.TabIndex = 7;
            lb_TienKhac.Text = "Tiền khác";
            // 
            // tb_Lai
            // 
            tb_Lai.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            tb_Lai.Location = new Point(216, 109);
            tb_Lai.Name = "tb_Lai";
            tb_Lai.Size = new Size(321, 23);
            tb_Lai.TabIndex = 5;
            // 
            // lb_Lai
            // 
            lb_Lai.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lb_Lai.AutoSize = true;
            lb_Lai.Location = new Point(3, 113);
            lb_Lai.Name = "lb_Lai";
            lb_Lai.Size = new Size(207, 15);
            lb_Lai.TabIndex = 4;
            lb_Lai.Text = "Tổng nợ còn lại đến hôm nay";
            // 
            // lb_TienCam
            // 
            lb_TienCam.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lb_TienCam.AutoSize = true;
            lb_TienCam.Location = new Point(3, 57);
            lb_TienCam.Name = "lb_TienCam";
            lb_TienCam.Size = new Size(207, 15);
            lb_TienCam.TabIndex = 2;
            lb_TienCam.Text = "Tiền vay (cầm)";
            // 
            // lb_NgayChuocDo
            // 
            lb_NgayChuocDo.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lb_NgayChuocDo.AutoSize = true;
            lb_NgayChuocDo.Location = new Point(3, 10);
            lb_NgayChuocDo.Name = "lb_NgayChuocDo";
            lb_NgayChuocDo.Size = new Size(207, 15);
            lb_NgayChuocDo.TabIndex = 1;
            lb_NgayChuocDo.Text = "Ngày chuộc đồ";
            // 
            // tb_TienVay
            // 
            tb_TienVay.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            tb_TienVay.Location = new Point(216, 53);
            tb_TienVay.Name = "tb_TienVay";
            tb_TienVay.Size = new Size(321, 23);
            tb_TienVay.TabIndex = 3;
            // 
            // tb_TienKhac
            // 
            tb_TienKhac.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            tb_TienKhac.Location = new Point(216, 165);
            tb_TienKhac.Name = "tb_TienKhac";
            tb_TienKhac.Size = new Size(321, 23);
            tb_TienKhac.TabIndex = 6;
            tb_TienKhac.TextChanged += tb_TienKhac_TextChanged;
            // 
            // btn_QuayLai
            // 
            btn_QuayLai.Location = new Point(47, 389);
            btn_QuayLai.Name = "btn_QuayLai";
            btn_QuayLai.Size = new Size(104, 41);
            btn_QuayLai.TabIndex = 2;
            btn_QuayLai.Text = "Quay lại";
            btn_QuayLai.UseVisualStyleBackColor = true;
            btn_QuayLai.Click += btn_QuayLai_Click;
            // 
            // Btn_Luu
            // 
            Btn_Luu.Location = new Point(480, 389);
            Btn_Luu.Name = "Btn_Luu";
            Btn_Luu.Size = new Size(104, 41);
            Btn_Luu.TabIndex = 3;
            Btn_Luu.Text = "Chuộc đồ";
            Btn_Luu.UseVisualStyleBackColor = true;
            Btn_Luu.Click += Btn_Luu_Click;
            // 
            // rtb_TieuDe
            // 
            rtb_TieuDe.Location = new Point(134, 32);
            rtb_TieuDe.Name = "rtb_TieuDe";
            rtb_TieuDe.Size = new Size(373, 28);
            rtb_TieuDe.TabIndex = 4;
            rtb_TieuDe.Text = "";
            // 
            // ChuocDoForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(642, 476);
            Controls.Add(rtb_TieuDe);
            Controls.Add(Btn_Luu);
            Controls.Add(btn_QuayLai);
            Controls.Add(tableLayoutPanel1);
            Name = "ChuocDoForm";
            Text = "Chuộc đồ";
            Load += ChuocDoForm_Load;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }



        #endregion

        private DateTimePicker dtp_NgayChuocDo;
        private TableLayoutPanel tableLayoutPanel1;
        private Label lb_NgayChuocDo;
        private TextBox tb_Lai;
        private Label lb_Lai;
        private Label lb_TienCam;
        private TextBox tb_TienVay;
        private TextBox tb_TongTienChuoc;
        private Label lb_TongTienChuoc;
        private Label lb_TienKhac;
        private TextBox tb_TienKhac;
        private Button btn_QuayLai;
        private Button Btn_Luu;
        private RichTextBox rtb_TieuDe;
    }
}