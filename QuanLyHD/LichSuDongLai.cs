using Microsoft.Data.Sqlite;
using QuanLyVayVon.CSDL;
using System.Globalization;
using static QuanLyVayVon.QuanLyHD.QuanLyHopDong;

namespace QuanLyVayVon.QuanLyHD
{
    public partial class LichSuDongLai : Form
    {
        private string? MaHD = null;
        private static readonly Font AppFont = new Font("Segoe UI", 11F, FontStyle.Regular);
        public LichSuDongLai(string? MaHD)
        {
            this.MaHD = MaHD;
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.CenterToScreen();
            if (MaHD == null)
            {
                CustomMessageBox.ShowCustomYesNoMessageBox("Không tìm thấy mã hợp đồng. Vui lòng thử lại.", this);
            }


            InitDataGridView();
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

        private void InitDataGridView()
        {
            this.WindowState = FormWindowState.Maximized;
            dataGridView_LichSuDongLai.Dock = DockStyle.Fill;
            dataGridView_LichSuDongLai.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;

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
                e.Column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                e.Column.MinimumWidth = 80;
                AutoFitDataGridViewColumnsAndRows();
            };
            foreach (DataGridViewColumn col in dataGridView_LichSuDongLai.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.ReadOnly = true;
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                col.MinimumWidth = 80;
            }

            // Màu nền, lưới, alternating row
            dataGridView_LichSuDongLai.BackgroundColor = Color.White;
            dataGridView_LichSuDongLai.GridColor = Color.LightGray;
            dataGridView_LichSuDongLai.BorderStyle = BorderStyle.None;
            dataGridView_LichSuDongLai.EnableHeadersVisualStyles = false;
            dataGridView_LichSuDongLai.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            dataGridView_LichSuDongLai.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView_LichSuDongLai.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView_LichSuDongLai.AutoResizeColumnHeadersHeight();
            dataGridView_LichSuDongLai.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView_LichSuDongLai.DefaultCellStyle.SelectionBackColor = Color.FromArgb(70, 130, 180);
            dataGridView_LichSuDongLai.DefaultCellStyle.SelectionForeColor = Color.White;
            dataGridView_LichSuDongLai.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(235, 240, 250);
            dataGridView_LichSuDongLai.AllowUserToResizeRows = true;
            dataGridView_LichSuDongLai.RowHeadersWidth = 40;
            dataGridView_LichSuDongLai.ScrollBars = ScrollBars.Both;
            dataGridView_LichSuDongLai.AllowUserToAddRows = false;
            dataGridView_LichSuDongLai.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            dataGridView_LichSuDongLai.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dataGridView_LichSuDongLai.RowTemplate.Height = 38;


            // Đặt toàn bộ DataGridView thành read-only để không chỉnh sửa được
            dataGridView_LichSuDongLai.ReadOnly = true;

            // Đảm bảo các cột fill hết chiều ngang
            dataGridView_LichSuDongLai.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            foreach (DataGridViewColumn col in dataGridView_LichSuDongLai.Columns)
            {
                if (col.Name == "GhiChuBtn" || col.Name == "ThaoTac" || col.Name == "TrangThai")
                    col.FillWeight = 80;
                else if (col.Name == "KyThu")
                    col.FillWeight = 60;
                else if (col.Name == "SoTienPhaiDong" || col.Name == "SoTienDaDong" || col.Name == "SoTienNo")
                    col.FillWeight = 120;
                else
                    col.FillWeight = 100;
            }

            // Tự động fit cột và hàng khi dữ liệu thay đổi
            dataGridView_LichSuDongLai.DataBindingComplete += (s, e) => AutoFitDataGridViewColumnsAndRows();
            dataGridView_LichSuDongLai.RowsAdded += (s, e) => AutoFitDataGridViewColumnsAndRows();
            dataGridView_LichSuDongLai.RowsRemoved += (s, e) => AutoFitDataGridViewColumnsAndRows();
        }

        private void AutoFitDataGridViewColumnsAndRows()
        {
            // Fit columns to content
            dataGridView_LichSuDongLai.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);
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



        private void LoadDuLieu()
        {
            var hopDong = HopDongForm.GetHopDongByMaHD(MaHD);
            var lichSuDongLaiList = GetLichSuDongLaiByMaHD(MaHD);
            LoadLichSuDongLaiToDataGridView(MaHD);
            tableLayoutPanel_info.AutoSize = true;

        }

        // In LoadLichSuDongLaiToDataGridView, remove the "NgayDongThucTe" column and its usage
        private void LoadLichSuDongLaiToDataGridView(string maHD)
        {
            if (maHD == null || maHD.Trim() == "")
            {
                return;
            }
            string dbPath = Path.Combine(Application.StartupPath, "Database", "data.db");

            dataGridView_LichSuDongLai.Columns.Clear();
            dataGridView_LichSuDongLai.Rows.Clear();

            dataGridView_LichSuDongLai.Columns.Add("KyThu", "Kỳ");
            dataGridView_LichSuDongLai.Columns.Add("NgayBatDauKy", "Bắt đầu");
            dataGridView_LichSuDongLai.Columns.Add("NgayDenHan", "Đến hạn");
            // Removed: dataGridView_LichSuDongLai.Columns.Add("NgayDongThucTe", "Ngày đóng");
            dataGridView_LichSuDongLai.Columns.Add("SoTienPhaiDong", "Tiền lãi");
            dataGridView_LichSuDongLai.Columns.Add("SoTienDaDong", "Đã đóng");
            dataGridView_LichSuDongLai.Columns.Add("SoTienNo", "Còn nợ");
            dataGridView_LichSuDongLai.Columns.Add("TrangThai", "Trạng thái");

            var noteButtonColumn = new DataGridViewButtonColumn
            {
                Name = "GhiChuBtn",
                HeaderText = "Ghi chú",
                Text = "Xem ghi chú",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            };
            dataGridView_LichSuDongLai.Columns.Add(noteButtonColumn);

            dataGridView_LichSuDongLai.Columns["SoTienDaDong"].ReadOnly = false;
            var actionButtonColumn = new DataGridViewButtonColumn
            {
                Name = "ThaoTac",
                HeaderText = "Thao tác",
                Text = "Đóng lãi",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            };
            dataGridView_LichSuDongLai.Columns.Add(actionButtonColumn);

            using (var connection = new SqliteConnection($"Data Source={dbPath}"))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"
                    SELECT KyThu, NgayBatDauKy, NgayDenHan, NgayDongThucTe, 
                           SoTienPhaiDong, SoTienDaDong, TinhTrang, GhiChu
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
                        // Removed: string ngayDong = reader["NgayDongThucTe"].ToString() ?? "";

                        decimal phaiDong = Convert.ToDecimal(reader["SoTienPhaiDong"]);
                        decimal daDong = Convert.ToDecimal(reader["SoTienDaDong"]);
                        decimal tienNo = phaiDong - daDong;
                        string strPhaiDong = Function_Reuse.FormatNumberWithThousandsSeparator(phaiDong);
                        string strDaDong = Function_Reuse.FormatNumberWithThousandsSeparator(daDong);
                        string strConNo = Function_Reuse.FormatNumberWithThousandsSeparator(tienNo);
                        int trangThai = Convert.ToInt32(reader["TinhTrang"]);
                        string strtrangThai = trangThai switch
                        {
                            0 => "Đã đóng",
                            1 => "Chưa đóng",
                            2 => "Tới hạn",
                            3 => "Quá hạn",
                            _ => "Không xác định"
                        };

                        string ghiChu = reader["GhiChu"]?.ToString() ?? "";
                        int IndexRow = dataGridView_LichSuDongLai.Rows.Add(
                            kyThu,
                            ngayBD,
                            ngayDH,
                            // Removed: ngayDong,
                            strPhaiDong,
                            strDaDong,
                            strConNo,
                            strtrangThai,
                            null, // GhiChuBtn
                            null  // ThaoTac
                        );
                        var row = dataGridView_LichSuDongLai.Rows[IndexRow];
                        row.DefaultCellStyle.BackColor = trangThai switch
                        {
                            0 => Color.Gray,
                            1 => Color.White,
                            2 => Color.LightYellow,
                            3 => Color.LightCoral,
                            _ => Color.White
                        };
                        row.Tag = ghiChu;
                    }
                }
            }
            dataGridView_LichSuDongLai.CellContentClick -= DataGridView_LichSuDongLai_CellContentClick;
            dataGridView_LichSuDongLai.CellContentClick += DataGridView_LichSuDongLai_CellContentClick;
        }
        // Thêm xử lý cho nút Ghi chú
        private void DataGridView_LichSuDongLai_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var grid = dataGridView_LichSuDongLai;
                // Nút ghi chú
                if (grid.Columns[e.ColumnIndex].Name == "GhiChuBtn")
                {
                    var row = grid.Rows[e.RowIndex];
                    string kyThu = row.Cells["KyThu"].Value?.ToString() ?? "";
                    string ghiChu = row.Tag?.ToString() ?? "";
                    string message = string.IsNullOrWhiteSpace(ghiChu) ? "Không có ghi chú." : ghiChu;
                    CustomMessageBox.ShowCustomMessageBox($"\n{message}", this, $"Ghi chú kỳ {kyThu}:");
                    return;
                }
                // Nút đóng lãi
                if (grid.Columns[e.ColumnIndex].Name == "ThaoTac")
                {
                    string strKyThu = grid.Rows[e.RowIndex].Cells["KyThu"].Value?.ToString();
                    string strTienPhaiDong = grid.Rows[e.RowIndex].Cells["SoTienPhaiDong"].Value?.ToString();

                    var (result, strTienDong) = Function_Reuse.ShowCustomInputMoneyBox("Số tiền lãi đóng kỳ này:", this, "Xác nhận", strTienPhaiDong);

                    decimal tienDong = decimal.TryParse(Function_Reuse.ExtractNumberString(strTienDong), NumberStyles.Number, CultureInfo.InvariantCulture, out var value) ? value : 0;
                    if (result == DialogResult.OK)
                    {
                        decimal tienPhaiDong = decimal.TryParse(Function_Reuse.ExtractNumberString(strTienPhaiDong), NumberStyles.Number, CultureInfo.InvariantCulture, out var valuePhaiDong) ? valuePhaiDong : 0;

                        if (string.IsNullOrEmpty(strTienDong) || tienDong < 0 || tienDong > tienPhaiDong)
                        {
                            CustomMessageBox.ShowCustomMessageBox("Số tiền đóng lãi không hợp lệ. Vui lòng nhập lại.", this);
                        }
                        else
                        {
                            string dbPath = Path.Combine(Application.StartupPath, "Database", "data.db");

                            using (var connection = new SqliteConnection($"Data Source={dbPath}"))
                            {
                                try
                                {
                                    connection.Open();

                                    // Bước 1: Lấy GhiChu hiện tại
                                    string? currentNote = "";
                                    using (var getNoteCmd = connection.CreateCommand())
                                    {
                                        getNoteCmd.CommandText = @"
                                            SELECT GhiChu FROM LichSuDongLai
                                            WHERE MaHD = @MaHD AND KyThu = @KyThu;
                                        ";
                                        getNoteCmd.Parameters.AddWithValue("@MaHD", MaHD);
                                        getNoteCmd.Parameters.AddWithValue("@KyThu", int.Parse(strKyThu));

                                        currentNote = getNoteCmd.ExecuteScalar() as string ?? "";
                                    }

                                    // Bước 2: Tạo GhiChu mới
                                    string newNoteLine = $"Đóng tiền vào ngày {DateTime.Now:dd/MM/yyyy HH:mm:ss}";
                                    string updatedNote = string.IsNullOrWhiteSpace(currentNote)
                                        ? newNoteLine
                                        : $"{currentNote}\r\n{newNoteLine}";

                                    // Bước 3: Cập nhật lại dữ liệu, bao gồm NgayDongThucTe
                                    using (var command = connection.CreateCommand())
                                    {
                                        command.CommandText = @"
                                            UPDATE LichSuDongLai
                                            SET SoTienDaDong = @SoTienDaDong, 
                                                UpdatedAt = CURRENT_TIMESTAMP,
                                                GhiChu = @GhiChu,
                                                NgayDongThucTe = @NgayDongThucTe
                                            WHERE MaHD = @MaHD AND KyThu = @KyThu;
                                        ";

                                        command.Parameters.Clear();
                                        command.Parameters.AddWithValue("@SoTienDaDong", tienDong);
                                        command.Parameters.AddWithValue("@GhiChu", updatedNote);
                                        command.Parameters.AddWithValue("@NgayDongThucTe", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                                        command.Parameters.AddWithValue("@MaHD", MaHD);
                                        command.Parameters.AddWithValue("@KyThu", int.Parse(strKyThu));

                                        int rowsAffected = command.ExecuteNonQuery();

                                        if (rowsAffected > 0)
                                        {
                                            CustomMessageBox.ShowCustomMessageBox("Cập nhật thành công!", this);
                                            QuanLyHopDong.CapNhatTinhTrangLichSuDongLai(this.MaHD);
                                            this.LoadDuLieu();
                                        }
                                        else
                                        {
                                            CustomMessageBox.ShowCustomMessageBox("Không tìm thấy kỳ cần cập nhật.", this);
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    CustomMessageBox.ShowCustomMessageBox("Có lỗi khi cập nhật: " + ex.Message, this);
                                }
                            }
                        }
                    }
                }
            }
        }
        private void StyleFlowLayoutPanel(FlowLayoutPanel flowLayoutPanel)
        {
            // Style FlowLayoutPanel chứa các nút
            flowLayoutPanel.AutoSize = true;
            flowLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowLayoutPanel.FlowDirection = FlowDirection.LeftToRight;
            flowLayoutPanel.WrapContents = true;
            flowLayoutPanel.Padding = new Padding(10, 10, 10, 0);
            flowLayoutPanel.Margin = new Padding(0);
        }
        private void CustomizeUI_LichSuDongLai()
        {
            // Giới hạn kích thước form
            int minWidth = 1400;
            int minHeight = 700;
            
            this.MinimumSize = new Size(minWidth, minHeight);
     
            this.StartPosition = FormStartPosition.CenterScreen;

            // Đảm bảo form luôn nằm ở giữa màn hình khi hiển thị lần đầu
            this.Load += (s, e) => this.CenterToScreen();

            // Chỉ căn giữa khi form không bị thu nhỏ (Minimized)
            this.SizeChanged += (s, e) =>
            {
                if (this.WindowState != FormWindowState.Minimized)
                {
                    this.CenterToScreen();
                }
            };
            this.VisibleChanged += (s, e) =>
            {
                if (this.Visible && this.WindowState != FormWindowState.Minimized)
                {
                    this.CenterToScreen();
                }
            };
            this.SizeChanged += LichSuDongLai_SizeChanged;
            StyleExitButton(btn_Thoát, "X");
            StyleExitButton(btn_Hide, "_");
            StyleExitButton(btn_Maxsize, "O");
            // Form properties
            this.Text = "Quản Lý Hợp Đồng Vay";
            this.FormBorderStyle = FormBorderStyle.None;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(240, 240, 240);
            int borderRadius = 32;
            this.Region = System.Drawing.Region.FromHrgn(
                NativeMethods.CreateRoundRectRgn(0, 0, this.Width, this.Height, borderRadius, borderRadius)
            );

            System.Drawing.Font mainFont = new System.Drawing.Font("Montserrat", 12.5F, FontStyle.Regular, GraphicsUnit.Point);
            System.Drawing.Font mainFontBold = new System.Drawing.Font("Montserrat", 13.5F, FontStyle.Bold, GraphicsUnit.Point);
            System.Drawing.Font donViFont = new System.Drawing.Font("Montserrat", 11F, FontStyle.Regular, GraphicsUnit.Point);
            System.Drawing.Font dateTimeFont = new System.Drawing.Font("Montserrat", 12.5F, FontStyle.Regular, GraphicsUnit.Point);

            Color richTextBoxBackColor = Color.FromArgb(248, 250, 255);
            Color richTextBoxBorderColor = Color.FromArgb(200, 215, 240);
            Color donViLabelBackColor = Color.FromArgb(225, 240, 255);
            Color donViLabelForeColor = Color.FromArgb(30, 90, 160);

            // Các hàm style control không dùng đến đã bị loại bỏ để tránh cảnh báo CS8321

            flowLayoutPanel_infoHD.BackColor = Color.Transparent;
            StyleFlowLayoutPanel(flowLayoutPanel_infoHD);
            StyleFlowLayoutPanel(flow_exit);

           


            lb_MaHD.Text = this.MaHD;
            lb_MaHD.Font = new Font("Montserrat", 13F, FontStyle.Bold);
            lb_MaHD.ForeColor = Color.Red;
            lb_MaHD.AutoSize = true;
            lb_MaHD.BackColor = Color.Transparent;

            lb_info.Text = "Bảng thông tin chi tiết hợp đồng:";
            lb_info.Font = new Font("Montserrat", 13F, FontStyle.Regular);
            lb_info.ForeColor = Color.Black;
            lb_info.AutoSize = true;
            lb_info.BackColor = Color.Transparent;
        }


        public static List<LichSuDongLaiModel> GetLichSuDongLaiByMaHD(string maHD)
        {
            if (maHD == null || maHD.Trim() == "")
            {
                return null;
            }
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

                        //draft

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

        private void btn_Thoát_Click_1(object sender, EventArgs e)
        {
            if (CustomMessageBox.ShowCustomYesNoMessageBox("Bạn có chắc chắn muốn thoát?", this) == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Yes;
                this.Close();
            }
        }

        private void LichSuDongLai_Load(object sender, EventArgs e)
        {

        }

        private void btn_Maxsize_Click(object sender, EventArgs e)
        {
            this.WindowState = this.WindowState == FormWindowState.Maximized ? FormWindowState.Normal : FormWindowState.Maximized;
        }

        private void btn_Hide_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void LichSuDongLai_SizeChanged(object sender, EventArgs e)
        {
            int borderRadius = (this.WindowState == FormWindowState.Maximized) ? 0 : 32;
            this.Region = System.Drawing.Region.FromHrgn(
                NativeMethods.CreateRoundRectRgn(0, 0, this.Width, this.Height, borderRadius, borderRadius)
            );
        }
    }
}
