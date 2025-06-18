using Microsoft.Data.Sqlite;
using QuanLyVayVon.CSDL;
using System.Drawing.Drawing2D; // Add 
using System.Globalization;
using System.Runtime.InteropServices;
using static QuanLyVayVon.QuanLyHD.QuanLyHopDong;

namespace QuanLyVayVon.QuanLyHD
{
    public partial class LichSuDongLai : Form
    {
        private string? MaHD = null;
        private static readonly Font AppFont = new Font("Segoe UI", 11F, FontStyle.Regular);
        // Khai báo thêm


        // Cho phép kéo form
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        // Gắn vào sự kiện MouseDown của form (hoặc panel tiêu đề tùy bạn)
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        public LichSuDongLai(string? MaHD)
        {
            this.MaHD = MaHD;
            this.MouseDown += Form1_MouseDown;

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
                            2 => "Sắp tới hạn",
                            3 => "Quá hạn",
                            4 => "Tới hạn hôm nay",
                            5 => "Tới hạn và đã đóng",
                            _ => "Không xác định"
                        };

                        string ghiChu = reader["GhiChu"]?.ToString() ?? "";
                        int IndexRow = dataGridView_LichSuDongLai.Rows.Add(
                            kyThu,
                            ngayBD,
                            ngayDH,
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
                            4 => Color.LightGreen,
                            5 => Color.Green,
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
                    string? maHD = this.MaHD;
                    string kyThu = row.Cells["KyThu"].Value?.ToString() ?? "";

                    if (string.IsNullOrWhiteSpace(maHD) || string.IsNullOrWhiteSpace(kyThu))
                    {
                        CustomMessageBox.ShowCustomMessageBox("Không xác định được mã hợp đồng hoặc kỳ thu.");
                        return;
                    }

                    string? ghiChu = null;
                    string dbPath = Path.Combine(Application.StartupPath, "Database", "data.db");
                    using (var connection = new Microsoft.Data.Sqlite.SqliteConnection($"Data Source={dbPath}"))
                    {
                        connection.Open();
                        using (var command = connection.CreateCommand())
                        {
                            command.CommandText = @"
                SELECT GhiChu 
                FROM LichSuDongLai 
                WHERE MaHD = @MaHD AND KyThu = @KyThu 
                LIMIT 1";
                            command.Parameters.AddWithValue("@MaHD", maHD);
                            command.Parameters.AddWithValue("@KyThu", kyThu);

                            var result = command.ExecuteScalar();
                            if (result != null && result != DBNull.Value)
                                ghiChu = result.ToString();
                        }
                    }

                    if (string.IsNullOrWhiteSpace(ghiChu))
                    {
                        CustomMessageBox.ShowCustomMessageBox("Không có ghi chú.");
                    }
                    else
                    {
                        // Mở form mới luôn, không tái sử dụng form cũ
                        var frm_XuatText = new TextToScreen(ghiChu, "Ghi chú kỳ: ", kyThu);
                        frm_XuatText.Show();
                    }

                    return;
                }

                // Nút đóng lãi
                if (grid.Columns[e.ColumnIndex].Name == "ThaoTac")
                {

                    string? strKyThu = grid.Rows[e.RowIndex].Cells["KyThu"].Value?.ToString();
                    string? strTienPhaiDong = grid.Rows[e.RowIndex].Cells["SoTienPhaiDong"].Value?.ToString();
                    int kyThu = int.TryParse(strKyThu, out var ky) ? ky : 0;


                    var tinhTrangKyTruoc = LayTinhTrangKyThu(MaHD, kyThu - 1);

                    var tinhTrangKySau = KiemTraKyThuDaDongLaiSau(this.MaHD, kyThu);
                    if (new[] { 1, 2, 3, 4 }.Contains(tinhTrangKyTruoc))
                    {
                        CustomMessageBox.ShowCustomMessageBox("Kỳ trước chưa được đóng. Vui lòng đóng kỳ trước trước khi đóng kỳ này.", this);
                        return;
                    }
                    else if (tinhTrangKyTruoc == -1)
                    {
                        CustomMessageBox.ShowCustomMessageBox("Không tìm thấy kỳ trước. Vui lòng kiểm tra lại.", this);
                        return;
                    }
                    else if (tinhTrangKySau == false)
                    {

                        CustomMessageBox.ShowCustomMessageBox("Kỳ sau đã đóng. Vui lòng sửa kỳ sau trước thành chưa đóng.", this);
                        return;
                    }
                    var (result, strTienDong) = Function_Reuse.ShowCustomInputMoneyBox("Số tiền lãi đóng kỳ này:", this, "Xác nhận", strTienPhaiDong);

                    decimal tienDong = decimal.TryParse(Function_Reuse.ExtractNumberString(strTienDong), NumberStyles.Number, CultureInfo.InvariantCulture, out var value) ? value : 0;
                    if (result == DialogResult.OK)
                    {
                        decimal tienPhaiDong = decimal.TryParse(Function_Reuse.ExtractNumberString(strTienPhaiDong), NumberStyles.Number, CultureInfo.InvariantCulture, out var valuePhaiDong) ? valuePhaiDong : 0;

                        if (string.IsNullOrWhiteSpace(strTienDong))
                        {
                            CustomMessageBox.ShowCustomMessageBox("Bạn chưa nhập số tiền đóng.", this);
                            return;
                        }
                        if (tienDong < 0)
                        {
                            CustomMessageBox.ShowCustomMessageBox("Số tiền đóng phải lớn hơn hoặc bằng 0.", this);
                            return;
                        }
                        if (tienDong > tienPhaiDong)
                        {
                            CustomMessageBox.ShowCustomMessageBox("Số tiền đóng vượt quá số phải đóng. Vui lòng kiểm tra lại.", this);
                            return;
                        }


                        string dbPath = Path.Combine(Application.StartupPath, "Database", "data.db");

                        using (var connection = new SqliteConnection($"Data Source={dbPath}"))
                        {
                            try
                            {
                                connection.Open();

                                string note = $"Đóng kỳ thứ {strKyThu} vào ngày {DateTime.Now:dd/MM/yyyy HH:mm:ss} - ({Function_Reuse.FormatNumberWithThousandsSeparator(tienDong)} VNĐ)";
                                GhiLichSuHopDong(MaHD, note);

                                // Ghi cập nhật kỳ
                                GhiLichSuKyThu(connection, MaHD, kyThu, tienDong);

                                QuanLyHopDong.CapNhatTinhTrangLichSuDongLai(MaHD);
                                
                                // Cập nhật ngày đóng lãi gần nhất
                                CapNhatNgayDongLaiGanNhat(connection, MaHD);
                               
                                
                                CustomMessageBox.ShowCustomMessageBox("Cập nhật thành công!", this);
                                this.LoadDuLieu();
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
        public static void GhiLichSuHopDong(string maHD, string noteNoiDung)
        {
            string dbPath = Path.Combine(Application.StartupPath, "Database", "data.db");
            using (var connection = new SqliteConnection($"Data Source={dbPath}"))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    string noteFull = $"{noteNoiDung}\r\n" +
                                      "__________________________________________________________________________________________";

                    command.CommandText = @"
                UPDATE HopDongVay
                SET LichSu = 
                    CASE 
                        WHEN LichSu IS NULL OR TRIM(LichSu) = '' THEN @NoteHD
                        ELSE @NoteHD || CHAR(13) || CHAR(10) || LichSu
                    END,
                    UpdatedAt = CURRENT_TIMESTAMP
                WHERE MaHD = @MaHD;
            ";
                    command.Parameters.AddWithValue("@NoteHD", noteFull);
                    command.Parameters.AddWithValue("@MaHD", maHD);
                    command.ExecuteNonQuery();
                }
            }
        }

        public static void GhiLichSuKyThu(SqliteConnection connection, string maHD, int kyThu, decimal tienDong)
        {
            try
            {
                string currentNote = "";
                using (var getNoteCmd = connection.CreateCommand())
                {
                    getNoteCmd.CommandText = @"
                SELECT GhiChu FROM LichSuDongLai
                WHERE MaHD = @MaHD AND KyThu = @KyThu;";
                    getNoteCmd.Parameters.AddWithValue("@MaHD", maHD);
                    getNoteCmd.Parameters.AddWithValue("@KyThu", kyThu);

                    currentNote = getNoteCmd.ExecuteScalar() as string ?? "";
                }

                string separator = "------------------------------------------------------------";
                string newNoteLine =
                    $"Đóng tiền vào ngày {DateTime.Now:dd/MM/yyyy HH:mm:ss} - ({Function_Reuse.FormatNumberWithThousandsSeparator(tienDong)} VNĐ)\r\n{separator}";

                string updatedNote = string.IsNullOrWhiteSpace(currentNote)
                    ? newNoteLine
                    : $"{newNoteLine}\r\n{currentNote}";

                using (var updateCmd = connection.CreateCommand())
                {
                    updateCmd.CommandText = @"
                UPDATE LichSuDongLai
                SET 
                    SoTienDaDong = @SoTienDaDong,
                    GhiChu = @UpdatedNote,
                    NgayDongThucTe = @NgayDongThucTe,
                    UpdatedAt = CURRENT_TIMESTAMP
                WHERE MaHD = @MaHD AND KyThu = @KyThu;";

                    updateCmd.Parameters.AddWithValue("@SoTienDaDong", tienDong);
                    updateCmd.Parameters.AddWithValue("@UpdatedNote", updatedNote);
                    updateCmd.Parameters.AddWithValue("@NgayDongThucTe", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    updateCmd.Parameters.AddWithValue("@MaHD", maHD);
                    updateCmd.Parameters.AddWithValue("@KyThu", kyThu);

                    updateCmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Lỗi khi ghi lịch sử kỳ thu: " + ex.Message);
            }
        }

        public static void CapNhatNgayDongLaiGanNhat(SqliteConnection conn, string maHD)
        {
            if (conn == null || conn.State != System.Data.ConnectionState.Open)
                throw new InvalidOperationException("Kết nối chưa được mở.");

            string query = @"
        UPDATE HopDongVay
        SET NgayDongLaiGanNhat = (
            SELECT NgayDenHan
            FROM LichSuDongLai
            WHERE MaHD = @MaHD AND TinhTrang IN (1, 2, 3, 4)
            ORDER BY KyThu ASC
            LIMIT 1
        ),
        UpdatedAt = CURRENT_TIMESTAMP
        WHERE MaHD = @MaHD;
    ";

            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@MaHD", maHD);
                cmd.ExecuteNonQuery();
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
            StyleButton(btn_Tattoan);
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
        /// <summary>
        /// Kiểm tra một kỳ đóng lãi bất kỳ đã được đóng chưa (Tình trạng == 0 là đã đóng).
        /// </summary>
        /// <param name="maHD">Mã hợp đồng</param>
        /// <param name="kyThu">Kỳ cần kiểm tra</param>
        /// <returns>true nếu kỳ đã đóng (Tình trạng == 0), false nếu chưa đóng hoặc không có kỳ này</returns>
        /// <summary>
        /// Trả về Tình Trạng của kỳ lãi:
        /// 0: Đã đóng, 1: Chưa đóng, 2: Sắp tới hạn, 3: Quá hạn, 4: Tới hạn hôm nay.
        /// Trả về -1 nếu không có dữ liệu.
        /// Nếu KyThu == 0 thì mặc định trả về 0 (đã đóng).
        /// </summary>
        private int LayTinhTrangKyThu(string maHD, int kyThu)
        {
            if (string.IsNullOrWhiteSpace(maHD))
                return -1;

            if (kyThu == 0)
                return 0;

            if (kyThu < 0)
                return -1;

            string dbPath = Path.Combine(Application.StartupPath, "Database", "data.db");
            using (var connection = new SqliteConnection($"Data Source={dbPath}"))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"
                SELECT TinhTrang FROM LichSuDongLai
                WHERE MaHD = @MaHD AND KyThu = @KyThu
            ";
                    command.Parameters.AddWithValue("@MaHD", maHD);
                    command.Parameters.AddWithValue("@KyThu", kyThu);

                    var result = command.ExecuteScalar();
                    if (result == null || result == DBNull.Value)
                        return -1;

                    return Convert.ToInt32(result);
                }
            }
        }

        private bool KiemTraKyThuDaDongLaiSau(string maHD, int kyThu)
        {
            if (string.IsNullOrWhiteSpace(maHD) || kyThu < 1)
                return false;

            // Lấy tổng số kỳ của hợp đồng
            int tongSoKy = 0;
            string dbPath = Path.Combine(Application.StartupPath, "Database", "data.db");
            using (var connection = new SqliteConnection($"Data Source={dbPath}"))
            {
                connection.Open();
                using (var countCmd = connection.CreateCommand())
                {
                    countCmd.CommandText = @"
                        SELECT MAX(KyThu) FROM LichSuDongLai
                        WHERE MaHD = @MaHD
                    ";
                    countCmd.Parameters.AddWithValue("@MaHD", maHD);
                    var maxKy = countCmd.ExecuteScalar();
                    tongSoKy = (maxKy != null && maxKy != DBNull.Value) ? Convert.ToInt32(maxKy) : 0;
                }
            }

            // Nếu là kỳ cuối thì return true
            if (kyThu >= tongSoKy)
                return true;

            int KySau = kyThu + 1; // Kiểm tra kỳ sau
            using (var connection = new SqliteConnection($"Data Source={dbPath}"))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"
                        SELECT SoTienDaDong FROM LichSuDongLai
                        WHERE MaHD = @MaHD AND KyThu = @KyThu
                    ";
                    command.Parameters.AddWithValue("@MaHD", maHD);
                    command.Parameters.AddWithValue("@KyThu", KySau);
                    var result = command.ExecuteScalar();
                    if (result == null || result == DBNull.Value)
                        return false; // Không có kỳ này
                    decimal soTienDaDong = Convert.ToDecimal(result);
                    // Nếu kỳ sau đã đóng tiền (>0) thì return false
                    if (soTienDaDong > 0)
                        return false;
                    // Nếu chưa đóng tiền (<=0) thì return true
                    return true;
                }
            }
        }

        private void btn_Tattoan_Click(object sender, EventArgs e)
        {

            if (Application.OpenForms.OfType<ChuocDoForm>().Any())
            {
                Application.OpenForms.OfType<ChuocDoForm>().First().Show();
                return;
            }
            var chuocDoFrm = new ChuocDoForm(MaHD);
            if (chuocDoFrm.ShowDialog() == DialogResult.OK)
            {

            }
            else if (chuocDoFrm.DialogResult == DialogResult.Cancel)
            {
                this.Show();
            }
        }
        // Thay thế đoạn vẽ viền bằng cách override OnPaint để vẽ viền custom, tránh lỗi Region khi resize
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            int borderRadius = (this.WindowState == FormWindowState.Maximized) ? 0 : 32;
            int borderWidth = 2;
            Color borderColor = Color.FromArgb(70, 130, 180); // SteelBlue

            using (GraphicsPath path = new GraphicsPath())

            {
                if (borderRadius > 0)
                {
                    path.AddArc(0, 0, borderRadius, borderRadius, 180, 90);
                    path.AddArc(this.Width - borderRadius - 1, 0, borderRadius, borderRadius, 270, 90);
                    path.AddArc(this.Width - borderRadius - 1, this.Height - borderRadius - 1, borderRadius, borderRadius, 0, 90);
                    path.AddArc(0, this.Height - borderRadius - 1, borderRadius, borderRadius, 90, 90);
                    path.CloseFigure();
                }
                else
                {
                    path.AddRectangle(new Rectangle(0, 0, this.Width, this.Height));
                }
                this.Region = new Region(path);
                using (Pen pen = new Pen(borderColor, borderWidth))
                {
                    e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    e.Graphics.DrawPath(pen, path);
                }
            }
        }
    }
    // Add the definition for the missing type 'KyDongLaiStatusModel' to resolve the CS0246 error.
    // This class is inferred based on its usage in the method 'GetKyThuVaTinhTrangByMaHD'.


}
