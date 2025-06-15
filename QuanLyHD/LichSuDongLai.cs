using Microsoft.Data.Sqlite;
using QuanLyVayVon.CSDL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static QuanLyVayVon.QuanLyHD.QuanLyHopDong;

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
            InitDataGridView();
        }
        private void InitDataGridView()
        {
            this.WindowState = FormWindowState.Maximized;
            dataGridView_LichSuDongLai.Dock = DockStyle.None;
             dataGridView_LichSuDongLai.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
             dataGridView_LichSuDongLai.Left = 20;
             dataGridView_LichSuDongLai.Width = this.ClientSize.Width - 40;
             dataGridView_LichSuDongLai.Height = this.ClientSize.Height -  dataGridView_LichSuDongLai.Top - 20;

            // Tự động resize khi thay đổi kích thước form
            this.Resize += (s, ev) =>
            {
                 dataGridView_LichSuDongLai.Left = 20;
                 dataGridView_LichSuDongLai.Width = this.ClientSize.Width - 40;
                 dataGridView_LichSuDongLai.Height = this.ClientSize.Height -  dataGridView_LichSuDongLai.Top - 20;
            };

            // Font đồng bộ với button
            var cellFont = new Font("Segoe UI", 12F, FontStyle.Regular);
            var headerFont = new Font("Segoe UI", 13F, FontStyle.Bold);

             dataGridView_LichSuDongLai.Font = cellFont;
             dataGridView_LichSuDongLai.ColumnHeadersDefaultCellStyle.Font = headerFont;
             dataGridView_LichSuDongLai.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
             dataGridView_LichSuDongLai.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
             dataGridView_LichSuDongLai.DefaultCellStyle.Font = cellFont;
             dataGridView_LichSuDongLai.RowTemplate.Height = 38;
             dataGridView_LichSuDongLai.ColumnHeadersHeight = 44;

            // Căn giữa header và cell cho tất cả các cột (kể cả khi cột được thêm động)
             dataGridView_LichSuDongLai.ColumnAdded += (s, e) =>
            {
                e.Column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                e.Column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                e.Column.ReadOnly = true;
            };
            foreach (DataGridViewColumn col in  dataGridView_LichSuDongLai.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.ReadOnly = true;
            }

            // Màu nền, lưới, alternating row
             dataGridView_LichSuDongLai.BackgroundColor = Color.White;
             dataGridView_LichSuDongLai.GridColor = Color.LightGray;
             dataGridView_LichSuDongLai.BorderStyle = BorderStyle.None;
             dataGridView_LichSuDongLai.EnableHeadersVisualStyles = false;
             dataGridView_LichSuDongLai.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
             dataGridView_LichSuDongLai.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
             dataGridView_LichSuDongLai.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
             dataGridView_LichSuDongLai.AutoResizeColumnHeadersHeight();
             dataGridView_LichSuDongLai.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
             dataGridView_LichSuDongLai.DefaultCellStyle.SelectionBackColor = Color.FromArgb(70, 130, 180);
             dataGridView_LichSuDongLai.DefaultCellStyle.SelectionForeColor = Color.White;
             dataGridView_LichSuDongLai.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(235, 240, 250);
             dataGridView_LichSuDongLai.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
             dataGridView_LichSuDongLai.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
             dataGridView_LichSuDongLai.AllowUserToResizeRows = false;
             dataGridView_LichSuDongLai.RowHeadersWidth = 40;
             dataGridView_LichSuDongLai.ScrollBars = ScrollBars.Both;

            // Tự động wrap text nếu cần
             dataGridView_LichSuDongLai.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
             dataGridView_LichSuDongLai.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;

            // Đặt toàn bộ DataGridView thành read-only để không chỉnh sửa được
             dataGridView_LichSuDongLai.ReadOnly = true;
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
                Dock = DockStyle.Fill,
                MaximumSize = new Size(0, 0),
                AutoEllipsis = true
            };

            Panel wrapperPanel = new Panel
            {
                BackColor = effectivePanelBackColor.Value,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Margin = new Padding(4),
                Dock = DockStyle.Fill
            };

            wrapperPanel.Controls.Add(lb);
            table.Controls.Add(wrapperPanel, col, row);
            table.SetCellPosition(wrapperPanel, new TableLayoutPanelCellPosition(col, row));

            if (table.ColumnCount > col)
                table.ColumnStyles[col].SizeType = SizeType.AutoSize;
            if (table.RowCount > row)
                table.RowStyles[row].SizeType = SizeType.AutoSize;

            // Update rounded corners when size changes
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

        private void LichSuDongLai_Load(object sender, EventArgs e)
        {
            tableLayoutPanel_info.ColumnStyles[0].SizeType = SizeType.AutoSize;
            tableLayoutPanel_info.ColumnStyles[1].SizeType = SizeType.AutoSize;
            tableLayoutPanel_info.RowStyles[0].SizeType = SizeType.AutoSize;
            tableLayoutPanel_info.AutoSize = true;

            string dbDir = Path.Combine(Application.StartupPath, "DataBase");
            string dbPath = Path.Combine(dbDir, "data.db");

            if (!Function_Reuse.KiemTraDatabaseTonTai())
            {
                CustomMessageBox.ShowCustomMessageBox("Cơ sở dữ liệu không tồn tại. Vui lòng tạo cơ sở dữ liệu trước.");
                return;
            }


            // Tùy chỉnh giao diện
            CustomizeUI_LichSuDongLai();
            LoadDuLieu();

        }
        private void LoadDuLieu()
        {
            var hopDong = HopDongForm.GetHopDongByMaHD(MaHD);
            var lichSuDongLaiList = GetLichSuDongLaiByMaHD(MaHD);
            string TenKH = hopDong.TenKH;
            if (hopDong != null)
            {
                AddStyledLabelToTable(tableLayoutPanel_info, 0, 0, TenKH, 11f, FontStyle.Bold, Color.FromArgb(30, 90, 160), default, new Padding(12, 4, 12, 4));

            }
            else
            {
                MessageBox.Show("Không tìm thấy hợp đồng.");
            }
            string SDT = hopDong.SDT;

            if (SDT != "")
            {
                AddStyledLabelToTable(tableLayoutPanel_info, 0, 1, SDT, 11f, FontStyle.Regular, Color.FromArgb(30, 90, 160), default, new Padding(12, 4, 12, 4));

            }
            else
            {
                AddStyledLabelToTable(tableLayoutPanel_info, 0, 1, "Không lưu số điện thoại", 11f, FontStyle.Regular, Color.FromArgb(30, 90, 160), default, new Padding(12, 4, 12, 4));


            }

            string lb1_0 = "Tiền vay ";
            AddStyledLabelToTable(tableLayoutPanel_info, 1, 0, lb1_0, 11f, FontStyle.Regular, Color.FromArgb(30, 90, 160), default, new Padding(12, 4, 12, 4));

            string lb1_1 = Function_Reuse.FormatNumberWithThousandsSeparator((decimal)hopDong.TienVay) + " VNĐ";
            
            AddStyledLabelToTable(tableLayoutPanel_info, 1, 1, lb1_1, 11f, FontStyle.Regular, Color.FromArgb(30, 90, 160), default, new Padding(12, 4, 12, 4));


            LoadLichSuDongLaiToDataGridView(MaHD);

            tableLayoutPanel_info.AutoSize = true;
        }
        
        private void LoadLichSuDongLaiToDataGridView(string maHD)
        {
            string dbPath = Path.Combine(Application.StartupPath, "Database", "data.db");

            dataGridView_LichSuDongLai.Columns.Clear();
            dataGridView_LichSuDongLai.Rows.Clear();

            dataGridView_LichSuDongLai.Columns.Add("KyThu", "Kỳ");
            dataGridView_LichSuDongLai.Columns.Add("NgayBatDauKy", "Bắt đầu");
            dataGridView_LichSuDongLai.Columns.Add("NgayDenHan", "Đến hạn");
            dataGridView_LichSuDongLai.Columns.Add("NgayDongThucTe", "Ngày đóng");
            dataGridView_LichSuDongLai.Columns.Add("SoTienPhaiDong", "Tiền lãi");
            dataGridView_LichSuDongLai.Columns.Add("SoTienDaDong", "Đã đóng");
            dataGridView_LichSuDongLai.Columns.Add("SoTienNo", "Còn nợ");
          
            dataGridView_LichSuDongLai.Columns.Add("TrangThai", "Trạng thái");
            dataGridView_LichSuDongLai.Columns.Add("GhiChu", "Ghi chú");
            var actionButtonColumn = new DataGridViewButtonColumn
            {
                Name = "ThaoTac",
                HeaderText = "Thao tác",
                Text = "Đóng lãi",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            };
            dataGridView_LichSuDongLai.Columns.Add(actionButtonColumn);

            dataGridView_LichSuDongLai.CellContentClick -= DataGridView_LichSuDongLai_CellContentClick;
            dataGridView_LichSuDongLai.CellContentClick += DataGridView_LichSuDongLai_CellContentClick;

            using (var connection = new SqliteConnection($"Data Source={dbPath}"))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"
            SELECT KyThu, NgayBatDauKy, NgayDenHan, NgayDongThucTe, 
                   SoTienPhaiDong, SoTienDaDong, SoTienNo, TinhTrang, GhiChu
            FROM LichSuDongLai
            WHERE MaHD = @MaHD
            ORDER BY KyThu;
        ";
                command.Parameters.AddWithValue("@MaHD", maHD);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string kyThu = reader["KyThu"].ToString() ?? "";
                        string ngayBD = reader["NgayBatDauKy"].ToString() ?? "";
                        string ngayDH = reader["NgayDenHan"].ToString() ?? "";
                        string ngayDong = reader["NgayDongThucTe"].ToString() ?? "";

                        decimal phaiDong = Convert.ToDecimal(reader["SoTienPhaiDong"]);
                        decimal daDong = Convert.ToDecimal(reader["SoTienDaDong"]);
                        decimal tienNo = Convert.ToDecimal(reader["SoTienNo"]);

                        string strPhaiDong = Function_Reuse.FormatNumberWithThousandsSeparator(phaiDong);
                        string strDaDong = Function_Reuse.FormatNumberWithThousandsSeparator(daDong);
                        string strConThieu = Function_Reuse.FormatNumberWithThousandsSeparator(tienNo);

                        string trangThai = Convert.ToInt32(reader["TinhTrang"]) == 1 ? "Đã đóng" : "Chưa đóng";
                        string ghiChu = reader["GhiChu"]?.ToString() ?? "";

                        dataGridView_LichSuDongLai.Rows.Add(
                            kyThu,
                            ngayBD,
                            ngayDH,
                            ngayDong,
                            strPhaiDong,
                            strDaDong,
                            strConThieu,
                            trangThai,
                            ghiChu
                        );
                    }
                }
            }
        }
        private void DataGridView_LichSuDongLai_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridView_LichSuDongLai.Columns[e.ColumnIndex].Name == "ThaoTac")
            {
                string KyThu = dataGridView_LichSuDongLai.Rows[e.RowIndex].Cells["KyThu"].Value?.ToString();
                string TienPhaiDong = dataGridView_LichSuDongLai.Rows[e.RowIndex].Cells["SoTienPhaiDong"].Value?.ToString();
                string TienDaDong = dataGridView_LichSuDongLai.Rows[e.RowIndex].Cells["SoTienDaDong"].Value?.ToString();
                string TienConNo = dataGridView_LichSuDongLai.Rows[e.RowIndex].Cells["SoTienNo"].Value?.ToString();

                string? result = Function_Reuse.ShowCustomInputMoneyBox("Số tiền lãi đóng kỳ này:", this, "Xác nhận", TienPhaiDong);


                decimal tienLaiDong = decimal.TryParse(Function_Reuse.ExtractNumberString(result), NumberStyles.Number, CultureInfo.InvariantCulture, out var value) ? value : 0;
                
                decimal tienPhaiDong = decimal.TryParse(Function_Reuse.ExtractNumberString(TienPhaiDong), NumberStyles.Number, CultureInfo.InvariantCulture, out var valuePhaiDong) ? valuePhaiDong : 0;
                decimal tienDaDong = decimal.TryParse(Function_Reuse.ExtractNumberString(TienDaDong), NumberStyles.Number, CultureInfo.InvariantCulture, out var valueDaDong) ? valueDaDong : 0;
                decimal tienConNo = decimal.TryParse(Function_Reuse.ExtractNumberString(TienConNo), NumberStyles.Number, CultureInfo.InvariantCulture, out var valueConNo) ? valueConNo : 0;
                if (string.IsNullOrEmpty(result) || tienLaiDong <= 0 || tienLaiDong > tienConNo)
                {
                    CustomMessageBox.ShowCustomMessageBox("Số tiền đóng lãi không hợp lệ. Vui lòng nhập lại.", this);
                }
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


        public static List<LichSuDongLaiModel> GetLichSuDongLaiByMaHD(string maHD)
        {
            string dbPath = Path.Combine(Application.StartupPath, "Database", "data.db");
            var list = new List<LichSuDongLaiModel>();

            using (var connection = new SqliteConnection($"Data Source={dbPath}"))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM LichSuDongLai WHERE MaHD = @MaHD ORDER BY KyThu";
                command.Parameters.AddWithValue("@MaHD", maHD);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new LichSuDongLaiModel
                        {
                            ID = Convert.ToInt32(reader["ID"]),
                            MaHD = reader["MaHD"].ToString(),
                            KyThu = Convert.ToInt32(reader["KyThu"]),
                            NgayBatDauKy = reader["NgayBatDauKy"].ToString(),
                            NgayDenHan = reader["NgayDenHan"].ToString(),
                            NgayDongThucTe = reader["NgayDongThucTe"].ToString(),
                            SoTienPhaiDong = Convert.ToDecimal(reader["SoTienPhaiDong"]),
                            SoTienDaDong = Convert.ToDecimal(reader["SoTienDaDong"]),
                            TinhTrang = reader["TinhTrang"] != DBNull.Value ? Convert.ToInt32(reader["TinhTrang"]) : 0,
                            GhiChu = reader["GhiChu"].ToString(),
                            CreatedAt = reader["CreatedAt"].ToString(),
                            UpdatedAt = reader["UpdatedAt"].ToString()
                        };

                        list.Add(item);
                    }
                }
            }

            return list;
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

        private void btn_GiaHan_Click(object sender, EventArgs e)
        {

        }
    }
}
