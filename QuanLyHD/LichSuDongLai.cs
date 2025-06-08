using Microsoft.Data.Sqlite;
using static QuanLyVayVon.QuanLyHD.QuanLyHopDong;
using QuanLyVayVon.CSDL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyVayVon.QuanLyHD
{
    public partial class LichSuDongLai : Form
    {
        private string? MaHD = null;
        public LichSuDongLai(string? MaHD)
        {
            InitializeComponent();
            this.AutoScaleMode = AutoScaleMode.Font;

            if (MaHD == null)
            {
                CustomMessageBox.ShowCustomYesNoMessageBox("Không tìm thấy mã hợp đồng. Vui lòng thử lại.", this);
            }

            this.BackColor = Color.FromArgb(240, 240, 240);
            this.MaHD = MaHD;
        }

        public void StyleDonViLabelInTable(Label lb, Panel wrapperPanel)
        {
            lb.Font = new Font("Montserrat", 11F, FontStyle.Regular);
            lb.ForeColor = Color.FromArgb(30, 90, 160);
            lb.BackColor = Color.Transparent;
            lb.AutoSize = true;
            lb.TextAlign = ContentAlignment.MiddleCenter;
            lb.Padding = new Padding(12, 4, 12, 4);

            wrapperPanel.BackColor = Color.FromArgb(225, 240, 255);
            wrapperPanel.AutoSize = true;
            wrapperPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            wrapperPanel.Controls.Add(lb);
            wrapperPanel.Margin = new Padding(4);

            // Cập nhật bo góc khi kích thước thay đổi
            lb.SizeChanged += (s, e) =>
            {
                wrapperPanel.Region = Region.FromHrgn(
                    NativeMethods.CreateRoundRectRgn(0, 0, wrapperPanel.Width, wrapperPanel.Height, 16, 16)
                );
            };

            // Gọi ban đầu
            wrapperPanel.Region = Region.FromHrgn(
                NativeMethods.CreateRoundRectRgn(0, 0, wrapperPanel.Width, wrapperPanel.Height, 16, 16)
            );
        }
        public void AddStyledLabelToTable(
            TableLayoutPanel table,
            int row,
            int col,
            string text,
            float fontSize = 11f,
            FontStyle fontStyle = FontStyle.Regular,
            Color? foreColor = default,
            Color? panelBackColor = default,
            Padding? padding = default,
            int cornerRadius = 16
        )
        {
            Color defaultForeColor = Color.FromArgb(30, 90, 160);
            Color? effectivePanelBackColor = panelBackColor ?? Color.Transparent;

            Label lb = new Label
            {
                Text = text,
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleCenter,
                Padding = padding ?? new Padding(12, 4, 12, 4),
                Font = new Font("Montserrat", fontSize, fontStyle),
                ForeColor = foreColor ?? defaultForeColor,
                BackColor = Color.Transparent,
                Dock = DockStyle.None,
                MaximumSize = new Size(0, 0),
                AutoEllipsis = true
            };

            Panel wrapperPanel = new Panel
            {
                BackColor = effectivePanelBackColor.Value,
                AutoSize = false,
                Margin = new Padding(4),
                Dock = DockStyle.Fill
            };

            wrapperPanel.Controls.Add(lb);
            table.Controls.Add(wrapperPanel, col, row);
            table.SetCellPosition(wrapperPanel, new TableLayoutPanelCellPosition(col, row));

            if (table.ColumnCount > col)
                table.ColumnStyles[col].SizeType = SizeType.Percent;
            if (table.RowCount > row)
                table.RowStyles[row].SizeType = SizeType.AutoSize;

            table.Resize += (s, e) =>
            {
                if (table.GetColumnWidths().Length > col && table.GetRowHeights().Length > row)
                {
                    int colWidth = table.GetColumnWidths()[col];
                    int rowHeight = table.GetRowHeights()[row];
                    wrapperPanel.Width = colWidth;
                    wrapperPanel.Height = rowHeight;
                    lb.Width = colWidth - 8;
                    lb.Height = rowHeight - 8;
                }
            };

            wrapperPanel.SizeChanged += (s, e) =>
            {
                wrapperPanel.Region = Region.FromHrgn(
                    NativeMethods.CreateRoundRectRgn(0, 0, wrapperPanel.Width, wrapperPanel.Height, cornerRadius, cornerRadius)
                );
                lb.Width = wrapperPanel.Width - 8;
                lb.Height = wrapperPanel.Height - 8;
            };

            wrapperPanel.Region = Region.FromHrgn(
                NativeMethods.CreateRoundRectRgn(0, 0, wrapperPanel.Width, wrapperPanel.Height, cornerRadius, cornerRadius)
            );
            lb.Width = wrapperPanel.Width - 8;
            lb.Height = wrapperPanel.Height - 8;
        }

        public class HopDongVay
        {
            public string MaHD { get; set; }
            public string TenKH { get; set; }
            public string SDT { get; set; }
            public double TienVay { get; set; }
            public int HinhThucLaiID { get; set; }
            public double Lai { get; set; }
            public int TinhTrang { get; set; }
        }
        public HopDongVay LayThongTinHopDong(string dbPath, string maHD)
        {
            using (var connection = new SqliteConnection($"Data Source={dbPath}"))
            {
                connection.Open();

                string query = @"
            SELECT MaHD, TenKH, SDT, TienVay, HinhThucLaiID, Lai, TinhTrang
            FROM HopDongVay
            WHERE MaHD = @maHD
        ";

                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@maHD", maHD);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new HopDongVay
                            {
                                MaHD = reader.GetString(0),
                                TenKH = reader.GetString(1),
                                SDT = reader.IsDBNull(2) ? "" : reader.GetString(2),
                                TienVay = reader.IsDBNull(3) ? 0 : reader.GetDouble(3),
                                HinhThucLaiID = reader.IsDBNull(4) ? 0 : reader.GetInt32(4),
                                Lai = reader.IsDBNull(5) ? 0 : reader.GetDouble(5),
                                TinhTrang = reader.IsDBNull(6) ? 0 : reader.GetInt32(6)
                            };
                        }
                    }
                }

                connection.Close();
            }

            return null; // Nếu không tìm thấy
        }


        private void LichSuDongLai_Load(object sender, EventArgs e)
        {

            string dbDir = Path.Combine(Application.StartupPath, "DataBase");
            string dbPath = Path.Combine(dbDir, "data.db");

            if (!Function_Reuse.KiemTraDatabaseTonTai())
            {
                CustomMessageBox.ShowCustomMessageBox("Cơ sở dữ liệu không tồn tại. Vui lòng tạo cơ sở dữ liệu trước.");
                return;
            }


            // Tùy chỉnh giao diện
            CustomizeUI_LichSuDongLai();

            var hopDong = LayThongTinHopDong(dbPath, MaHD);
            string TenKH = hopDong.TenKH;
            if (hopDong != null)
            {
                AddStyledLabelToTable(tableLayoutPanel_info, 0, 0,TenKH, 11f, FontStyle.Bold, Color.FromArgb(30, 90, 160), default, new Padding(12, 4, 12, 4));

            }
            else
            {
                MessageBox.Show("Không tìm thấy hợp đồng.");
            }


        }
        private void CustomizeUI_LichSuDongLai()
        {
            flowLayoutPanel_infoHD.BackColor = Color.Transparent;

            flowLayoutPanel_infoHD.FlowDirection = FlowDirection.LeftToRight;
            flowLayoutPanel_infoHD.AutoSize = true;
            flowLayoutPanel_infoHD.WrapContents = false;
            flowLayoutPanel_infoHD.BackColor = Color.Transparent; // hoặc màu phù hợp với nền
            flowLayoutPanel_infoHD.Padding = new Padding(0);
            flowLayoutPanel_infoHD.Margin = new Padding(0);

            // MaHD - in đậm, màu đỏ
            lb_MaHD.Text = this.MaHD;
            lb_MaHD.Font = new Font("Montserrat", 13F, FontStyle.Bold);
            lb_MaHD.ForeColor = Color.Red;
            lb_MaHD.AutoSize = true;
            lb_MaHD.BackColor = Color.Transparent;

            // Info - chữ thường, đen
            lb_info.Text = "Bảng thông tin chi tiết hợp đồng:";
            lb_info.Font = new Font("Montserrat", 13F, FontStyle.Regular);
            lb_info.ForeColor = Color.Black;
            lb_info.AutoSize = true;
            lb_info.BackColor = Color.Transparent;

           


        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tb_TienVay_TextChanged(object sender, EventArgs e)
        {

        }

        private void lb_TenKH_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel_info_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lb_infoHD_Click(object sender, EventArgs e)
        {

        }
    }
}
