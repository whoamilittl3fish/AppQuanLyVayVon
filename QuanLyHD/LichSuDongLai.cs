using Microsoft.Data.Sqlite;
using QuanLyVayVon.CSDL;
using QuestPDF.Fluent;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static QuanLyVayVon.QuanLyHD.QuanLyHopDong;
using System.Diagnostics;
using System.IO;

namespace QuanLyVayVon.QuanLyHD
{
    public partial class LichSuDongLai : RoundedForm
    {
        private string? MaHD = null;
        private string? tinhTrang = null;



        // Cho phép kéo form
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        // Gắn vào sự kiện MouseDown của Form hoặc một panel tiêu đề (tuỳ bạn)
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }


        public LichSuDongLai(string? MaHD, string? tinhTrang)
        {
            this.MaHD = MaHD;
            this.tinhTrang = tinhTrang;

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


            string dbDir = Path.Combine(Application.StartupPath, "DataBase");
            string dbPath = Path.Combine(dbDir, "data.db");

            if (!Function_Reuse.KiemTraDatabaseTonTai())
            {
                CustomMessageBox.ShowCustomMessageBox("Cơ sở dữ liệu không tồn tại. Vui lòng tạo cơ sở dữ liệu trước.");
                return;
            }

            LoadLayoutFull();
            // Tùy chỉnh giao diện
            CustomizeUI_LichSuDongLai();
            LoadDuLieu();

        }

        private void InitDataGridView()
        {
            this.WindowState = FormWindowState.Maximized;
            dataGridView_LichSuDongLai.Dock = DockStyle.Fill;
            dataGridView_LichSuDongLai.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;

            var cellFont = new Font("Segoe UI", 12F, FontStyle.Regular);
            var headerFont = new Font("Segoe UI", 13F, FontStyle.Bold);

            dataGridView_LichSuDongLai.Font = cellFont;
            dataGridView_LichSuDongLai.ColumnHeadersDefaultCellStyle.Font = headerFont;
            dataGridView_LichSuDongLai.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView_LichSuDongLai.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView_LichSuDongLai.RowTemplate.Height = 38;
            dataGridView_LichSuDongLai.ColumnHeadersHeight = 44;

            dataGridView_LichSuDongLai.ColumnAdded += (s, e) =>
            {
                e.Column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                e.Column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                e.Column.ReadOnly = true;
                e.Column.MinimumWidth = 80;
                // Bỏ AutoSizeMode ở đây
            };

            foreach (DataGridViewColumn col in dataGridView_LichSuDongLai.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.ReadOnly = true;
                col.MinimumWidth = 80;
                // Không set AutoSizeMode ở đây
            }

            // 🎨 Màu dịu theo macOS-style
            dataGridView_LichSuDongLai.BackgroundColor = Color.FromArgb(248, 249, 251); // Nền tổng thể
            dataGridView_LichSuDongLai.GridColor = Color.FromArgb(210, 215, 230);       // Lưới
            dataGridView_LichSuDongLai.BorderStyle = BorderStyle.None;
            dataGridView_LichSuDongLai.EnableHeadersVisualStyles = false;

            dataGridView_LichSuDongLai.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(100, 130, 200); // Header xanh tím
            dataGridView_LichSuDongLai.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView_LichSuDongLai.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;

            dataGridView_LichSuDongLai.DefaultCellStyle.SelectionBackColor = Color.FromArgb(80, 120, 200);
            dataGridView_LichSuDongLai.DefaultCellStyle.SelectionForeColor = Color.White;
            dataGridView_LichSuDongLai.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(235, 239, 245);

            dataGridView_LichSuDongLai.AllowUserToResizeRows = true;
            dataGridView_LichSuDongLai.RowHeadersWidth = 40;
            dataGridView_LichSuDongLai.ScrollBars = ScrollBars.Both;
            dataGridView_LichSuDongLai.AllowUserToAddRows = false;

            dataGridView_LichSuDongLai.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            dataGridView_LichSuDongLai.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dataGridView_LichSuDongLai.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            dataGridView_LichSuDongLai.ReadOnly = true;
            // Không set AutoSizeColumnsMode ở đây

            foreach (DataGridViewColumn col in dataGridView_LichSuDongLai.Columns)
            {
                // Không set FillWeight và AutoSizeMode ở đây nữa
            }
        }

        private void LoadDuLieu()
        {
            var hopDong = HopDongForm.GetHopDongByMaHD(MaHD);
            var lichSuDongLaiList = GetLichSuDongLaiByMaHD(MaHD);
            LoadLichSuDongLaiToDataGridView(MaHD);

            LoadTextBoxThongTin();
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
            dataGridView_LichSuDongLai.Columns.Add("SoTienPhaiDong", "Tiền lãi");
            dataGridView_LichSuDongLai.Columns.Add("SoTienDaDong", "Đã đóng");
            dataGridView_LichSuDongLai.Columns.Add("SoTienNo", "Còn nợ");
            dataGridView_LichSuDongLai.Columns.Add("TrangThai", "Trạng thái");

            var noteButtonColumn = new DataGridViewButtonColumn
            {
                Name = "GhiChuBtn",
                HeaderText = "Ghi chú",
                Text = "📝 Ghi chú", // Biểu tượng viết tay
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            };
            dataGridView_LichSuDongLai.Columns.Add(noteButtonColumn);

            dataGridView_LichSuDongLai.Columns["SoTienDaDong"].ReadOnly = false;
            var actionButtonColumn = new DataGridViewButtonColumn
            {
                Name = "ThaoTac",
                HeaderText = "Thao tác",
                Text = "💰 Đóng lãi", // biểu tượng tiền
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

                        decimal phaiDong = Convert.ToDecimal(reader["SoTienPhaiDong"]);
                        decimal daDong = Convert.ToDecimal(reader["SoTienDaDong"]);
                        decimal tienNo = phaiDong - daDong;
                        string strPhaiDong = Function_Reuse.FormatNumberWithThousandsSeparator(phaiDong);
                        string strDaDong = Function_Reuse.FormatNumberWithThousandsSeparator(daDong);
                        string strConNo = Function_Reuse.FormatNumberWithThousandsSeparator(tienNo);
                        int trangThai = Convert.ToInt32(reader["TinhTrang"]);

                        string strtrangThai = trangThai switch
                        {
                            -3 => "Đánh dấu gia hạn",
                            -2 => "Đã chuộc sớm",
                            -1 => "Đã chuộc",
                            0 => "Đã đóng",
                            1 => "Chưa đóng",
                            2 => "Sắp tới hạn",
                            3 => "Quá hạn",
                            4 => "Tới hạn hôm nay",
                            5 => "Tới hạn và đã đóng",
                            6 => "Gia hạn tăng thêm kỳ",
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
                            -3 => Color.DarkGray,
                            0 => Color.LightGray,
                            1 => Color.White,
                            2 => Color.LightYellow,
                            3 => Color.LightCoral,
                            4 => Color.LightGreen,
                            5 => Color.Green,
                            6 => Color.LightBlue,
                            _ => Color.White
                        };
                        row.Tag = ghiChu;

                        // Disable "ThaoTac" button if hợp đồng đã tất toán (tinhTrang == 0)
                        if (CheckKetThucHopDong(this.MaHD) == true)
                        {
                            var thaoTacCell = row.Cells["ThaoTac"] as DataGridViewButtonCell;
                            if (thaoTacCell != null)
                            {
                                thaoTacCell.Style.BackColor = Color.LightGray;
                                thaoTacCell.Style.ForeColor = Color.DarkGray;
                                thaoTacCell.ReadOnly = true;
                                thaoTacCell.FlatStyle = FlatStyle.Flat;
                                thaoTacCell.Value = "Đóng lãi";
                            }
                        }

                    }
                }
            }
            dataGridView_LichSuDongLai.Columns["KyThu"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView_LichSuDongLai.Columns["NgayBatDauKy"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView_LichSuDongLai.Columns["NgayDenHan"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView_LichSuDongLai.Columns["SoTienPhaiDong"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView_LichSuDongLai.Columns["SoTienDaDong"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView_LichSuDongLai.Columns["SoTienNo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView_LichSuDongLai.Columns["TrangThai"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView_LichSuDongLai.Columns["GhiChuBtn"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView_LichSuDongLai.Columns["ThaoTac"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

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


                    if (CheckKetThucHopDong(this.MaHD) == true)
                    {
                        CustomMessageBox.ShowCustomMessageBox("Hợp đồng đã tất toán và không thể thay đổi. \r\n Đề phòng thay đổi cơ sở dữ liệu bất hợp pháp (thay đổi tính năng sửa được hợp đồng khi đã tất toán liên hệ để thay đổi).", this);
                        return;
                    }


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

                                string note =
     $"ĐÓNG LÃI: {DateTime.Now:dd/MM/yyyy HH:mm:ss}\n" +
     $"Kỳ thứ: {strKyThu}\n" +
     $"Tiền lãi đã đóng: {Function_Reuse.FormatNumberWithThousandsSeparator(tienDong)} VNĐ";

                                GhiLichSuHopDong(MaHD, note);

                                // Ghi cập nhật kỳ
                                GhiLichSuKyThu(connection, MaHD, kyThu, tienDong);

                                QuanLyHopDong.CapNhatTinhTrangLichSuDongLai(MaHD);

                                // Cập nhật ngày đóng lãi gần nhất
                                CapNhatNgayDongLaiGanNhat(connection, MaHD);
                                QuanLyHopDong.CapNhatTinhTrangMaHD(MaHD);

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
            if (CheckKetThucHopDong(maHD) == true)
                return;
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
            if (CheckKetThucHopDong(maHD) == true)
                return;
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
                    $"ĐÓNG TIỀN: {DateTime.Now:dd/MM/yyyy HH:mm:ss} - ({Function_Reuse.FormatNumberWithThousandsSeparator(tienDong)} VNĐ)\r\n{separator}";

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
            if (CheckKetThucHopDong(maHD) == true)
                return;
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

        private void LoadLayoutFull()
        {
            // === 1. tblayout_form: 3 hàng ===
            tblayout_form.Dock = DockStyle.Fill;
            tblayout_form.ColumnStyles.Clear();
            tblayout_form.RowStyles.Clear();
            tblayout_form.ColumnCount = 1;
            tblayout_form.RowCount = 3;

            tblayout_form.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblayout_form.RowStyles.Add(new RowStyle(SizeType.AutoSize));      // Top
            tblayout_form.RowStyles.Add(new RowStyle(SizeType.AutoSize));      // Mid
            tblayout_form.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // Grid

            // === 2. Top: Nút điều hướng ===
            tblayout_Top.Dock = DockStyle.Fill;
            tblayout_Top.ColumnStyles.Clear();
            tblayout_Top.RowStyles.Clear();
            tblayout_Top.ColumnCount = 2;
            tblayout_Top.RowCount = 1;
            tblayout_Top.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblayout_Top.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            flowlayout_Button.Dock = DockStyle.Fill;
            tblayout_Top.Controls.Add(flowlayout_Button, 1, 0);
            StyleFlowLayoutPanel(flowlayout_Button);

            // === 3. tblayout_mid khởi tạo trước với Tên KH ===
            tblayout_mid.SuspendLayout();
            tblayout_mid.Controls.Clear();
            tblayout_mid.Dock = DockStyle.Fill;
            tblayout_mid.ColumnStyles.Clear();
            tblayout_mid.RowStyles.Clear();
            tblayout_mid.ColumnCount = 2;
            tblayout_mid.RowCount = 1;
            tblayout_mid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblayout_mid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblayout_mid.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            var hopdong = HopDongForm.GetHopDongByMaHD(this.MaHD);
            tblayout_mid.Controls.Add(rtb_TenKH, 0, 0);
            HienThiRichText(rtb_TenKH, hopdong.TenKH, 15F, Color.Brown, true);
            tblayout_mid.ResumeLayout();

            // === 4. DGV ===
            dataGridView_LichSuDongLai.Dock = DockStyle.Fill;

            // === 5. Gắn vào form ===
            tblayout_form.Controls.Add(tblayout_Top, 0, 0);
            tblayout_form.Controls.Add(tblayout_mid, 0, 1);
            tblayout_form.Controls.Add(dataGridView_LichSuDongLai, 0, 2);
        }

        private void LoadTextBoxThongTin()
        {
            tblayout_mid.SuspendLayout();
            tblayout_mid.Controls.Clear();
            tblayout_mid.Dock = DockStyle.Fill;
            tblayout_mid.ColumnStyles.Clear();
            tblayout_mid.RowStyles.Clear();
            tblayout_mid.ColumnCount = 2;
            tblayout_mid.RowCount = 4;
            tblayout_mid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblayout_mid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            for (int i = 0; i < 4; i++)
                tblayout_mid.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            var hopdong = HopDongForm.GetHopDongByMaHD(this.MaHD);
            var lichSuDongLaiList = GetLichSuDongLaiByMaHD(this.MaHD);
            decimal tongNo = CapNhatLaiDenHomNay(this.MaHD);
            decimal tongTienDaDong = lichSuDongLaiList.Sum(x => x.SoTienDaDong);
            string? ngayDongLaiGanNhat = lichSuDongLaiList?
                .Where(x => !string.IsNullOrWhiteSpace(x.NgayDongThucTe))
                .Select(x => DateTime.TryParse(x.NgayDongThucTe, out var dt) ? dt : (DateTime?)null)
                .Where(dt => dt.HasValue)
                .OrderByDescending(dt => dt.Value)
                .Select(dt => dt.Value.ToString("yyyy-MM-dd HH:mm:ss"))
                .FirstOrDefault();

            string tinhTrangChu = TinhTrangToString(hopdong.TinhTrang);

            // ==== Các dòng thông tin ====
            tblayout_mid.Controls.Add(rtb_TenKH, 0, 0);
            HienThiRichText(rtb_TenKH, hopdong.TenKH, 12F, Color.Brown, true);
            tblayout_mid.Controls.Add(CreateFlow("Lãi suất:", Function_Reuse.FormatNumberWithThousandsSeparator(hopdong.Lai) + HinhThucLaiIDToString(hopdong.HinhThucLaiID), Color.DarkOrange), 1, 0);
            tblayout_mid.Controls.Add(CreateFlow("Tiền cầm:", Function_Reuse.FormatNumberWithThousandsSeparator(hopdong.TienVay) + " VNĐ", Color.DarkGreen), 0, 1);
            tblayout_mid.Controls.Add(CreateFlow("Tiền lãi đã đóng:", Function_Reuse.FormatNumberWithThousandsSeparator(tongTienDaDong) + " VNĐ", Color.DarkSlateBlue), 1, 1);
            tblayout_mid.Controls.Add(CreateFlow("Vay từ ngày:", hopdong.NgayVay + " → " + hopdong.NgayHetHan, Color.MidnightBlue), 0, 2);
            tblayout_mid.Controls.Add(CreateFlow("Nợ các kỳ chưa đóng:", Function_Reuse.FormatNumberWithThousandsSeparator(tongNo) + " VNĐ", Color.DarkSlateBlue), 1, 2);
            tblayout_mid.Controls.Add(CreateFlow("Ngày trả lãi gần nhất:", ngayDongLaiGanNhat ?? "Chưa có", Color.DarkSlateGray), 0, 3);
            tblayout_mid.Controls.Add(CreateFlow("Tình trạng:", tinhTrangChu, Color.Indigo), 1, 3);

            tblayout_mid.ResumeLayout();
        }

        private FlowLayoutPanel CreateFlow(string labelText, string value, Color textColor)
        {
            FlowLayoutPanel flow = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                AutoSize = true,
                Dock = DockStyle.Fill,
                Margin = new Padding(4)
            };

            Label lbl = new Label
            {
                AutoSize = true,
                Text = labelText,
                Font = new Font("Montserrat", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(90, 90, 90),
                Margin = new Padding(4, 0, 4, 0)
            };

            TextBox tb = new TextBox();
            HienThiTextBox(tb, value, 12F, textColor, true);
            tb.TextAlign = HorizontalAlignment.Right;

            flow.Controls.Add(lbl);
            flow.Controls.Add(tb);
            return flow;
        }

        // HienThiTextBox(tb_TienLaiDaDong, Function_Reuse.FormatNumberWithThousandsSeparator(tienLaiDaDong) + " VNĐ", 12F, Color.DarkOliveGreen, true);
        private void StyleFlowLayoutPanel(FlowLayoutPanel panel)
        {
            panel.AutoSize = true;
            panel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panel.WrapContents = true;
            panel.Padding = new Padding(5);
            panel.Margin = new Padding(0);
        }
        private string TinhTrangToString(int? tinhtrang)
        {
            return tinhtrang switch
            {
                -2 => "Đã chuộc sớm",
                -1 => "Đã chuộc",
                0 => "Đã đóng",
                1 => "Chưa đóng",
                2 => "Sắp tới hạn",
                3 => "Quá hạn",
                4 => "Tới hạn hôm nay",
                5 => "Tới hạn và đã đóng",
                _ => "Không xác định"
            };
        }
        private string HinhThucLaiIDToString(int? hinhThucLaiID)
        {
            return hinhThucLaiID switch
            {
                1 => "VND/Ngày",
                2 => "VND/Tuần",
                3 => "VND/Tháng",
                4 => "%/Ngày",
                5 => "%/Tuần",
                6 => "%/Tháng",
                _ => "Không xác định"
            };
        }

        private void CustomizeUI_LichSuDongLai()
        {

            HienThiTieuDe("Lịch sử đóng lãi hợp đồng: ", this.MaHD);

            tblayout_mid.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            // Giới hạn kích thước form
            int minWidth = 1400;
            int minHeight = 700;
            this.MinimumSize = new Size(minWidth, minHeight);

            // Khởi tạo form
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Load += (s, e) => this.CenterToScreen();
            this.SizeChanged += (s, e) =>
            {
                if (this.WindowState != FormWindowState.Minimized)
                    this.CenterToScreen();
            };
            this.VisibleChanged += (s, e) =>
            {
                if (this.Visible && this.WindowState != FormWindowState.Minimized)
                    this.CenterToScreen();
            };
            this.SizeChanged += LichSuDongLai_SizeChanged;

            // Button control (exit/minimize/max)
            StyleControlButton(btn_Thoát, "c");
            StyleControlButton(btn_Hide, "m");
            StyleControlButton(btn_Maxsize, "mx");
            QuanLyHopDong.StyleButton(btn_GiaHan,"Gia hạn", Properties.Resources.giahan);
            QuanLyHopDong.StyleButton(btn_In, "In hợp đồng", Properties.Resources.printcontract);
            QuanLyHopDong.StyleButton(btn_Tattoan,"Chuộc đồ",Properties.Resources.chuoc);
            QuanLyHopDong.StyleButton(btn_XoaHopDong, "Xoá hợp đồng", Properties.Resources.xoa);
            QuanLyHopDong.StyleButton(btn_InLichSuDongLai, "In lịch sử đóng lãi", Properties.Resources.printtable);
            // Form properties
            this.Text = "Quản Lý Hợp Đồng Vay";
            this.FormBorderStyle = FormBorderStyle.None;
            this.MaximizeBox = false;
            this.BackColor = ColorTranslator.FromHtml("#F2F2F7"); // 🌫 Nền dịu macOS-like

            // Bo góc form
            int borderRadius = 32;
            this.Region = System.Drawing.Region.FromHrgn(
                NativeMethods.CreateRoundRectRgn(0, 0, this.Width, this.Height, borderRadius, borderRadius)
            );

            // Fonts
            System.Drawing.Font mainFont = new System.Drawing.Font("Montserrat", 12.5F, FontStyle.Regular, GraphicsUnit.Point);
            System.Drawing.Font mainFontBold = new System.Drawing.Font("Montserrat", 13.5F, FontStyle.Bold, GraphicsUnit.Point);
            System.Drawing.Font donViFont = new System.Drawing.Font("Montserrat", 11F, FontStyle.Regular, GraphicsUnit.Point);
            System.Drawing.Font dateTimeFont = new System.Drawing.Font("Montserrat", 12.5F, FontStyle.Regular, GraphicsUnit.Point);

            // Màu control nền dịu
            Color richTextBoxBackColor = Color.FromArgb(248, 250, 255);
            Color richTextBoxBorderColor = Color.FromArgb(200, 215, 240);
            Color donViLabelBackColor = Color.FromArgb(225, 240, 255);
            Color donViLabelForeColor = Color.FromArgb(30, 90, 160);

            // Layout panels


            flowlayout_Button.Dock = DockStyle.Right;

        }


        private void HienThiRichText(RichTextBox rtb, string text, float fontSize = 15F, Color? color = null, bool bold = false)
        {
            if (rtb == null) return;

            rtb.ReadOnly = true;
            rtb.BorderStyle = BorderStyle.None;
            rtb.BackColor = this.BackColor;
            rtb.TabStop = false;
            rtb.ScrollBars = RichTextBoxScrollBars.None;
            rtb.Multiline = false;
            rtb.AutoSize = false;

            var fontStyle = bold ? FontStyle.Bold : FontStyle.Regular;
            var font = new Font("Montserrat", fontSize, fontStyle);
            rtb.Font = font;
            rtb.ForeColor = color ?? Color.SaddleBrown;

            // Gán text
            string finalText = string.IsNullOrWhiteSpace(text) ? "(Không có nội dung)" : text.Trim();
            rtb.Text = finalText;

            // Tính kích thước theo nội dung
            using (Graphics g = rtb.CreateGraphics())
            {
                SizeF textSize = g.MeasureString(finalText, font);
                int padding = 8;

                rtb.Width = (int)textSize.Width + padding * 2;
                rtb.Height = (int)textSize.Height + padding;
            }

            rtb.Margin = new Padding(4, 4, 4, 4);

            rtb.MouseDown -= Rtb_Generic_MouseDown;
            rtb.MouseDown += Rtb_Generic_MouseDown;
        }

        private void HienThiTextBox(TextBox tb, string text, float fontSize = 15F, Color? color = null, bool bold = false)
        {
            if (tb == null) return;

            tb.ReadOnly = true;
            tb.BorderStyle = BorderStyle.None;
            tb.BackColor = this.BackColor;
            tb.TabStop = false;
            tb.Multiline = false;
            tb.AutoSize = false;

            var fontStyle = bold ? FontStyle.Bold : FontStyle.Regular;
            var font = new Font("Montserrat", fontSize, fontStyle);
            tb.Font = font;
            tb.ForeColor = color ?? Color.SaddleBrown;
            tb.TextAlign = HorizontalAlignment.Right;

            string finalText = string.IsNullOrWhiteSpace(text) ? "(Không có nội dung)" : text.Trim();
            tb.Text = finalText;

            using (Graphics g = tb.CreateGraphics())
            {
                SizeF textSize = g.MeasureString(finalText, font);
                int paddingX = 10, paddingY = 6;

                tb.Width = (int)textSize.Width + paddingX * 2;
                tb.Height = (int)textSize.Height + paddingY;
            }

            tb.Margin = new Padding(4, 0, 4, 0);
        }



        private void Rtb_Generic_MouseDown(object sender, MouseEventArgs e)
        {
            ((RichTextBox)sender).DeselectAll();
        }

        private void HienThiTieuDe(string tieuDeTruocMaHD, string maHD)
        {
            rtb_TieuDe.Clear();
            rtb_TieuDe.BorderStyle = BorderStyle.None;
            rtb_TieuDe.BackColor = this.BackColor;
            rtb_TieuDe.ReadOnly = true;
            rtb_TieuDe.TabStop = false;
            rtb_TieuDe.ScrollBars = RichTextBoxScrollBars.None;
            rtb_TieuDe.Multiline = false;
            rtb_TieuDe.Font = new Font("Montserrat", 18F, FontStyle.Regular);
            rtb_TieuDe.ForeColor = Color.FromArgb(44, 62, 80); // Màu xám đậm

            // Soạn nội dung
            rtb_TieuDe.SelectionColor = Color.FromArgb(44, 62, 80); // Xám đậm
            rtb_TieuDe.AppendText(tieuDeTruocMaHD);
            rtb_TieuDe.SelectionColor = Color.Red;
            rtb_TieuDe.AppendText(maHD);
            rtb_TieuDe.SelectionStart = 0;
            rtb_TieuDe.SelectionLength = 0;

            // Không cho chọn hoặc chỉnh sửa
            rtb_TieuDe.MouseDown += (s, e) => ((RichTextBox)s).DeselectAll();
        }




        public static bool CheckHopDongDaDongLai(string maHD)
        {
            if (string.IsNullOrWhiteSpace(maHD))
                return false;
            string dbPath = Path.Combine(Application.StartupPath, "DataBase", "data.db");
            using (var connection = new SqliteConnection($"Data Source={dbPath}"))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"
                SELECT COUNT(*) 
                FROM LichSuDongLai 
                WHERE MaHD = @MaHD AND TinhTrang IN (0, -1, -2)";
                    command.Parameters.AddWithValue("@MaHD", maHD);
                    long count = (long)command.ExecuteScalar();
                    return count > 0;
                }
            }
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

        public static bool CheckKetThucHopDong(string maHD)
        {
            if (string.IsNullOrWhiteSpace(maHD))
                return false;
            string dbPath = Path.Combine(Application.StartupPath, "DataBase", "data.db");
            using (var connection = new SqliteConnection($"Data Source={dbPath}"))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"
                        SELECT TinhTrang FROM HopDongVay
                        WHERE MaHD = @MaHD
                    ";
                    command.Parameters.AddWithValue("@MaHD", maHD);
                    var result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        int tinhTrang = Convert.ToInt32(result);
                        return tinhTrang == -1 || tinhTrang == -2;
                    }
                    // Không tìm thấy hợp đồng hoặc không có trạng thái kết thúc
                    return false;
                }
            }
        }
        public static bool CheckHopDongGiaHan(string MaHD)
        {
            if (string.IsNullOrWhiteSpace(MaHD))
                return false;
            string dbPath = Path.Combine(Application.StartupPath, "DataBase", "data.db");
            using (var connection = new SqliteConnection($"Data Source={dbPath}"))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"
                        SELECT Extended FROM HopDongVay
                        WHERE MaHD = @MaHD
                    ";
                    command.Parameters.AddWithValue("@MaHD", MaHD);
                    var result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        int Extended = Convert.ToInt32(result);
                        return Extended == 1; // Chưa đóng hoặc đã đóng
                    }
                    // Không tìm thấy hợp đồng hoặc không có trạng thái hợp lệ
                    return false;
                }
            }
        }
        private void btn_Tattoan_Click(object sender, EventArgs e)
        {
            if (CheckKetThucHopDong(MaHD))
            {
                CustomMessageBox.ShowCustomMessageBox("Hợp đồng này đã kết thúc. Không thể thực hiện thao tác này.", this);
                return;
            }
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

        private void tableLayoutPanel1_Paint_1(object sender, PaintEventArgs e)
        {

        }
        public static bool CheckGiaHan(string MaHD)
        {
            if (string.IsNullOrWhiteSpace(MaHD))
                return false;
            string dbPath = Path.Combine(Application.StartupPath, "DataBase", "data.db");
            using (var connection = new SqliteConnection($"Data Source={dbPath}"))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"
                        SELECT Extended FROM HopDongVay
                        WHERE MaHD = @MaHD
                    ";
                    command.Parameters.AddWithValue("@MaHD", MaHD);
                    var result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        int Extended = Convert.ToInt32(result);
                        return (Extended == -3 || Extended == 6); // Đã gia hạn
                    }
                    // Không tìm thấy hợp đồng hoặc không có trạng thái hợp lệ
                    return false;
                }
            }
        }
        private void btn_GiaHan_Click_1(object sender, EventArgs e)
        {

            if (CheckKetThucHopDong(MaHD))
            {
                CustomMessageBox.ShowCustomMessageBox("Hợp đồng này đã kết thúc. Không thể thực hiện thao tác này.", this);
                return;
            }
            if (Application.OpenForms.OfType<GiaHan>().Any())
            {
                Application.OpenForms.OfType<GiaHan>().First().Show();
                return;
            }
            var giaHanfrm = new GiaHan(MaHD);
            if (giaHanfrm.ShowDialog() == DialogResult.OK)
            {

            }
            else if (giaHanfrm.DialogResult == DialogResult.Cancel)
            {
                this.Show();
            }
        }

        private void btn_In_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<PrintHD>().Any())
            {
                Application.OpenForms.OfType<PrintHD>().First().Show();
                return;
            }
            else
            {
                var printHD = new PrintHD(MaHD);
                printHD.Show();
            }
        }
        private void ExportLichSuDongLaiToPdf(string maHD, List<LichSuDongLaiModel> list)
        {
            if (list == null || list.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để in lịch sử đóng lãi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                string folder = Path.Combine(Application.StartupPath, "PrintContracts");
                Directory.CreateDirectory(folder);

                string fileName = $"HopDong-{maHD}_LichSuDongLai.pdf";
                string fullPath = Path.Combine(folder, fileName);

                var doc = new LichSuDongLaiPdfDocument(maHD, list);
                doc.GeneratePdf(fullPath);

                Process.Start("explorer.exe", fullPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xuất PDF: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btn_XoaHopDong_Click(object sender, EventArgs e)
        {
            var existingForm = Application.OpenForms.OfType<XacNhan>().FirstOrDefault();
            if (existingForm != null)
            {
                existingForm.Close();
            }
            var thongbaoForm = CustomMessageBox.ShowCustomYesNoMessageBox("Hợp đồng xoá sẽ không được PHỤC HỒI.\nBạn có chắc chắn muốn xoá hợp đồng này không?", this);
            if (thongbaoForm != DialogResult.Yes)
            {
                return;
            }
            var xacNhanForm = new XacNhan();

            if (xacNhanForm.ShowDialog() == DialogResult.Yes)
            {
                // Xử lý xóa hợp đồng và các bảng liên quan đến MaHD
                string dbPath = Path.Combine(Application.StartupPath, "DataBase", "data.db");
                using (var connection = new SqliteConnection($"Data Source={dbPath}"))
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = @"
                            DELETE FROM LichSuDongLai WHERE MaHD = @MaHD;
                            DELETE FROM LichSuCapNhatHopDong WHERE MaHD = @MaHD;
                            DELETE FROM TienDaThuTrongThang WHERE MaHD = @MaHD;
                            DELETE FROM HopDongVay WHERE MaHD = @MaHD;
                        ";
                        command.Parameters.AddWithValue("@MaHD", MaHD);
                        command.ExecuteNonQuery();
                    }
                }
                CustomMessageBox.ShowCustomMessageBox("Hợp đồng và các thông tin liên quan đã được xóa thành công.", this);
                this.DialogResult = DialogResult.Yes;
                this.Close();

            }
        }

        private void btn_InLichSuDongLai_Click(object sender, EventArgs e)
        {
            
            var list = GetLichSuDongLaiByMaHD(this.MaHD);
            ExportLichSuDongLaiToPdf(this.MaHD, list);
        }
    }
}
