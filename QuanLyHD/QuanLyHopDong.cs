using Microsoft.Data.Sqlite;
using QuanLyVayVon.CSDL;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;
namespace QuanLyVayVon.QuanLyHD
{
    public partial class QuanLyHopDong : Form
    {

        // Khai báo biến toàn cục
        private int pageSize = 20;
        private string? lastCreatedAt = null;
        private string? firstCreatedAt = null;

        private bool isSearchMode = false;
        private int currentSearchPage = 1;
        private string? searchKeyword = null;
        private string? searchField = null;

        private static class AppFonts
        {
            // Font cho header DataGridView, tiêu đề lớn
            public static readonly Font Header = new Font("Segoe UI", 12F, FontStyle.Bold);
            // Font cho text/cell DataGridView, nội dung chính
            public static readonly Font Cell = new Font("Segoe UI", 11F, FontStyle.Regular);
            // Font cho các button
            public static readonly Font Button = new Font("Segoe UI", 12F, FontStyle.Bold);
            // Font cho textbox nhập liệu
            public static readonly Font TextBox = new Font("Montserrat", 11F, FontStyle.Regular, GraphicsUnit.Point);
            // Font cho label thông thường
            public static readonly Font Label = new Font("Montserrat", 11F, FontStyle.Regular, GraphicsUnit.Point);
            // Font cho label đậm
            public static readonly Font LabelBold = new Font("Montserrat", 13F, FontStyle.Bold, GraphicsUnit.Point);
            // Font cho đơn vị nhỏ
            public static readonly Font DonVi = new Font("Montserrat", 12F, FontStyle.Regular, GraphicsUnit.Point);
            // Font cho ngày tháng
            public static readonly Font DateTime = new Font("Montserrat", 12.5F, FontStyle.Regular, GraphicsUnit.Point);
            // Font cho các note, ghi chú, lịch sử
            public static readonly Font Note = new Font("Segoe UI", 11F, FontStyle.Italic);
        }

        private void KhoiTaoPhanTrang()
        {
            LoadTrangDauTien();
        }

        private void LoadTrangDauTien()
        {
            var danhSach = LayHopDongTheoTrangTheoCreatedAt(null, true, pageSize);
            HienThiHopDong(danhSach);
        }

        private void LoadTrangTiepTheo()
        {
            if (lastCreatedAt == null) return;
            var danhSach = LayHopDongTheoTrangTheoCreatedAt(lastCreatedAt, true, pageSize);
            HienThiHopDong(danhSach);
        }

        private void LoadTrangTruoc()
        {
            if (firstCreatedAt == null) return;

            var danhSach = LayHopDongTheoTrangTheoCreatedAt(firstCreatedAt, false, pageSize);

            // Nếu không có dữ liệu mới, hoặc dữ liệu trả về vẫn là trang cũ => không hiển thị lại
            if (danhSach == null || danhSach.Count == 0 || danhSach.Last().CreatedAt == firstCreatedAt)
                return;

            HienThiHopDong(danhSach);
        }

        void HienThiHopDong(List<HopDongModel> danhSach)
        {
            if (danhSach == null || danhSach.Count == 0)
            {
                CustomMessageBox.ShowCustomMessageBox("Không có hợp đồng nào.");
                return;
            }

            dataGridView_ThongTinHopDong.Columns.Clear();
            dataGridView_ThongTinHopDong.Rows.Clear();
            dataGridView_ThongTinHopDong.AllowUserToAddRows = false; // ❗ Rất quan trọng

            var columnDefinitions = new[]
            {
                ("MaHD", "MaHD"),
                ("TenKH", "Khách Hàng"),
                ("TenTaiSan", "Tài sản"),
                ("TienVay", "Tiền vay"),
                ("NgayVay", "Ngày vay"),
                ("LaiDaDong", "Lãi đã đóng"),
                ("TienNo", "Tiền nợ"),
                ("LaiDenHomNay", "Lãi đến hôm nay"),
                ("NgayPhaiDongLai", "Ngày phải đóng lãi"),
                ("TinhTrang", "Tình trạng")
            };

            foreach (var (name, header) in columnDefinitions)
            {
                dataGridView_ThongTinHopDong.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = name,
                    HeaderText = header
                });
            }

            // Nút "Ghi chú"
            var ghiChuColumn = new DataGridViewButtonColumn
            {
                Name = "GhiChu",
                HeaderText = "Ghi chú",
                Text = "Xem",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            };
            dataGridView_ThongTinHopDong.Columns.Add(ghiChuColumn);

            // Nút "Lịch sử"
            var lichSuColumn = new DataGridViewButtonColumn
            {
                Name = "LichSu",
                HeaderText = "Lịch sử",
                Text = "Xem",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            };
            dataGridView_ThongTinHopDong.Columns.Add(lichSuColumn);

            var actionColumn = new DataGridViewButtonColumn
            {
                Name = "ThaoTac",
                HeaderText = "Thao tác",
                Text = "Chi tiết",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            };
            dataGridView_ThongTinHopDong.Columns.Add(actionColumn);

            foreach (var item in danhSach)
            {
                string tienVay = Function_Reuse.FormatNumberWithThousandsSeparator(item.TienVay);
                string laiDaDong = Function_Reuse.FormatNumberWithThousandsSeparator(item.TienLaiDaDong ?? 0);
                string tongLai = Function_Reuse.FormatNumberWithThousandsSeparator(item.TongLai ?? 0);
                string tienNo = Function_Reuse.FormatNumberWithThousandsSeparator((item.TongLai ?? 0) - (item.TienLaiDaDong ?? 0));
                string laiDenHomNay = CapNhatLaiDenHomNay(item.MaHD).ToString();
                string tinhTrangText = item.TinhTrang switch
                {
                    0 => "Đã tất toán",
                    1 => "Đang vay",
                    2 => "Sắp tới hạn",
                    3 => "Quá hạn",
                    4 => "Tới hạn đóng lãi",
                    5 => "Tới hạn vã đã đóng",
                    _ => "Mới hoặc vừa chỉnh sửa"
                };

                // Thêm dòng vào DataGridView
                int rowIndex = dataGridView_ThongTinHopDong.Rows.Add(
                    item.MaHD,
                    item.TenKH,
                    item.TenTaiSan,
                    tienVay,
                    item.NgayVay,
                    laiDaDong,
                    tienNo,
                    laiDenHomNay,
                    item.NgayDongLaiGanNhat,
                    tinhTrangText,
                    "Xem", // Nút Ghi chú
                    "Xem", // Nút Lịch sử
                    null   // Nút Thao tác (sẽ được gán bởi DataGridViewButtonColumn)
                );

                // Lưu GhiChu vào Tag của cell để sử dụng khi click
                dataGridView_ThongTinHopDong.Rows[rowIndex].Cells["GhiChu"].Tag = item.GhiChu;

                // Gán màu dòng sau khi thêm
                var row = dataGridView_ThongTinHopDong.Rows[rowIndex];
                row.DefaultCellStyle.BackColor = item.TinhTrang switch
                {
                    0 => Color.Gray,
                    1 => Color.White,
                    2 => Color.LightYellow,
                    3 => Color.LightCoral,
                    4 => Color.LightGreen,
                    5 => Color.Green,
                    _ => Color.White
                };
            }
            // Gắn lại sự kiện
            dataGridView_ThongTinHopDong.CellContentClick -= DataGridView_ThongTinHopDong_CellContentClick;
            dataGridView_ThongTinHopDong.CellContentClick += DataGridView_ThongTinHopDong_CellContentClick;

            dataGridView_ThongTinHopDong.Columns["MaHD"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView_ThongTinHopDong.Columns["TenKH"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView_ThongTinHopDong.Columns["TenTaiSan"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView_ThongTinHopDong.Columns["TienVay"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView_ThongTinHopDong.Columns["NgayVay"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView_ThongTinHopDong.Columns["LaiDaDong"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView_ThongTinHopDong.Columns["TienNo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView_ThongTinHopDong.Columns["LaiDenHomNay"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView_ThongTinHopDong.Columns["NgayPhaiDongLai"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;       
            dataGridView_ThongTinHopDong.Columns["TinhTrang"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            // Cập nhật thời điểm
            firstCreatedAt = danhSach.First().CreatedAt;
            lastCreatedAt = danhSach.Last().CreatedAt;

            btn_Lui.Enabled = CoTrangTruoc(firstCreatedAt);
            btn_Tien.Enabled = CoTrangTiepTheo(lastCreatedAt);
        }

        private bool CoTrangTruoc(string? createdAt)
        {
            if (createdAt == null) return false;

            string dbPath = Path.Combine(Application.StartupPath, "DataBase", "data.db");
            using (var connection = new SqliteConnection($"Data Source={dbPath}"))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT COUNT(*) FROM HopDongVay WHERE datetime(CreatedAt) > datetime(@CreatedAt)";
                command.Parameters.AddWithValue("@CreatedAt", createdAt);
                var result = Convert.ToInt32(command.ExecuteScalar());
                return result > 0;
            }
        }

        private bool CoTrangTiepTheo(string? createdAt)
        {
            if (createdAt == null) return false;

            string dbPath = Path.Combine(Application.StartupPath, "DataBase", "data.db");
            using (var connection = new SqliteConnection($"Data Source={dbPath}"))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT COUNT(*) FROM HopDongVay WHERE datetime(CreatedAt) < datetime(@CreatedAt)";
                command.Parameters.AddWithValue("@CreatedAt", createdAt);
                var result = Convert.ToInt32(command.ExecuteScalar());
                return result > 0;
            }
        }


        public static List<HopDongModel> LayHopDongTheoTrangTheoCreatedAt(string? createdAt = null, bool isNextPage = true, int pageSize = 50)
        {
            var ds = new List<HopDongModel>();
            string dbPath = Path.Combine(Application.StartupPath, "DataBase", "data.db");


            using (var connection = new SqliteConnection($"Data Source={dbPath}"))
            {
                connection.Open();
                var command = connection.CreateCommand();

                if (createdAt == null)
                {
                    command.CommandText = @"
                SELECT * FROM HopDongVay
                ORDER BY datetime(CreatedAt) DESC, Id DESC
                LIMIT @PageSize";
                }
                else if (isNextPage)
                {
                    command.CommandText = @"
                SELECT * FROM HopDongVay
                WHERE datetime(CreatedAt) < datetime(@CreatedAt)
                ORDER BY datetime(CreatedAt) DESC, Id DESC
                LIMIT @PageSize";
                }
                else
                {
                    command.CommandText = @"
                SELECT * FROM HopDongVay
                WHERE datetime(CreatedAt) > datetime(@CreatedAt)
                ORDER BY datetime(CreatedAt) ASC, Id ASC
                LIMIT @PageSize";
                }

                if (createdAt != null)
                    command.Parameters.AddWithValue("@CreatedAt", createdAt);

                command.Parameters.AddWithValue("@PageSize", pageSize);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ds.Add(new HopDongModel
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            MaHD = reader["MaHD"]?.ToString(),
                            TenKH = reader["TenKH"]?.ToString(),
                            TenTaiSan = reader["TenTaiSan"]?.ToString(),
                            TienVay = Convert.ToDecimal(reader["TienVay"] ?? 0),
                            NgayVay = reader["NgayVay"]?.ToString(),
                            TienLaiDaDong = Convert.ToDecimal(reader["TienLaiDaDong"] ?? 0),
                            TongLai = Convert.ToDecimal(reader["TongLai"] ?? 0),
                            NgayDongLaiGanNhat = reader["NgayDongLaiGanNhat"]?.ToString(),
                            TinhTrang = Convert.ToInt32(reader["TinhTrang"] ?? 0),
                            CreatedAt = reader["CreatedAt"]?.ToString()
                        });
                    }
                }

                if (!isNextPage)
                    ds.Reverse();
            }

            return ds;

        }
        private void btn_Tien_Click(object sender, EventArgs e)
        {
            if (isSearchMode)
            {
                currentSearchPage++;
                var ds = LayHopDong_TimKiemPhanTrang(searchKeyword, searchField, currentSearchPage, pageSize);
                HienThiHopDong(ds);
            }
            else
            {
                LoadTrangTiepTheo();
            }
        }

        private void btn_Lui_Click(object sender, EventArgs e)
        {
            if (isSearchMode && currentSearchPage > 1)
            {
                currentSearchPage--;
                var ds = LayHopDong_TimKiemPhanTrang(searchKeyword, searchField, currentSearchPage, pageSize);
                HienThiHopDong(ds);
            }
            else
            {
                LoadTrangTruoc();
            }
        }
        public static List<HopDongModel> LayHopDong_TimKiemPhanTrang(string? keyword, string? tinhTrangField, int page, int pageSize)
        {
            var ds = new List<HopDongModel>();
            string dbPath = Path.Combine(Application.StartupPath, "DataBase", "data.db");
            using (var connection = new SqliteConnection($"Data Source={dbPath}"))
            {
                connection.Open();
                var command = connection.CreateCommand();

                var whereClauses = new List<string>();
                if (!string.IsNullOrEmpty(keyword))
                {
                    whereClauses.Add("(MaHD LIKE @kw OR SDT LIKE @kw OR CCCD LIKE @kw)");
                    command.Parameters.AddWithValue("@kw", "%" + keyword + "%");
                }

                if (!string.IsNullOrEmpty(tinhTrangField))
                {
                    int tinhTrangCode = tinhTrangField switch
                    {
                        "DaChuoc" => 0,
                        "DangVay" => 1,
                        "SapToiHan" => 2,
                        "QuaHan" => 3,
                        "ToiHanHomNay" => 4,
                        "ToiHanVaDaDong" => 5,
                        _ => -999
                    };
                    whereClauses.Add("TinhTrang = @tinhTrang");
                    command.Parameters.AddWithValue("@tinhTrang", tinhTrangCode);
                }

                string whereSql = whereClauses.Count > 0 ? "WHERE " + string.Join(" AND ", whereClauses) : "";

                command.CommandText = $@"
            SELECT * FROM HopDongVay
            {whereSql}
            ORDER BY datetime(CreatedAt) DESC, Id DESC
            LIMIT @PageSize OFFSET @Offset";

                command.Parameters.AddWithValue("@PageSize", pageSize);
                command.Parameters.AddWithValue("@Offset", (page - 1) * pageSize);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ds.Add(new HopDongModel
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            MaHD = reader["MaHD"]?.ToString(),
                            TenKH = reader["TenKH"]?.ToString(),
                            TenTaiSan = reader["TenTaiSan"]?.ToString(),
                            TienVay = Convert.ToDecimal(reader["TienVay"] ?? 0),
                            NgayVay = reader["NgayVay"]?.ToString(),
                            TienLaiDaDong = Convert.ToDecimal(reader["TienLaiDaDong"] ?? 0),
                            TongLai = Convert.ToDecimal(reader["TongLai"] ?? 0),
                            NgayDongLaiGanNhat = reader["NgayDongLaiGanNhat"]?.ToString(),
                            TinhTrang = Convert.ToInt32(reader["TinhTrang"] ?? 0),
                            CreatedAt = reader["CreatedAt"]?.ToString()
                        });
                    }
                }
            }

            return ds;
        }



        public static List<HopDongModel> TimKiemHopDongTheoKeyword(string keyword, int page, int pageSize)
        {
            var ds = new List<HopDongModel>();
            string dbPath = Path.Combine(Application.StartupPath, "DataBase", "data.db");

            using (var connection = new SqliteConnection($"Data Source={dbPath}"))
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = @"
            SELECT * FROM HopDongVay
            WHERE MaHD LIKE @kw OR SDT LIKE @kw OR CCCD LIKE @kw
            ORDER BY datetime(CreatedAt) DESC, Id DESC
            LIMIT @PageSize OFFSET @Offset";

                command.Parameters.AddWithValue("@kw", $"%{keyword}%");
                command.Parameters.AddWithValue("@PageSize", pageSize);
                command.Parameters.AddWithValue("@Offset", (page - 1) * pageSize);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ds.Add(new HopDongModel
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            MaHD = reader["MaHD"]?.ToString(),
                            TenKH = reader["TenKH"]?.ToString(),
                            TenTaiSan = reader["TenTaiSan"]?.ToString(),
                            TienVay = Convert.ToDecimal(reader["TienVay"] ?? 0),
                            NgayVay = reader["NgayVay"]?.ToString(),
                            TienLaiDaDong = Convert.ToDecimal(reader["TienLaiDaDong"] ?? 0),
                            TongLai = Convert.ToDecimal(reader["TongLai"] ?? 0),
                            NgayDongLaiGanNhat = reader["NgayDongLaiGanNhat"]?.ToString(),
                            TinhTrang = Convert.ToInt32(reader["TinhTrang"] ?? 0),
                            CreatedAt = reader["CreatedAt"]?.ToString()
                        });
                    }
                }
            }

            return ds;
        }

    

        private void CustomizeUI()
        {
            
            this.AutoScaleMode = AutoScaleMode.Font;
            this.StartPosition = FormStartPosition.CenterScreen;


          

            StyleButton(btn_ThemHopDong);
            StyleButton(btn_MoCSDL);
            StyleButton(btn_chinhsua);
            StyleButton(btn_Lui);
            StyleButton(btn_Tien);
            StyleTextBox(tb_Search);
            StyleButton(btn_UpdateInfoSystem);
            StyleButton(btn_About);

            StyleButton(btn_Search);

            btn_Search.Text = "🔍 Tìm kiếm";
            StyleComboBox(cbBox_Search);
            StyleExitButton(btn_Thoat, "X");
            StyleExitButton(btn_Hide, "–");
            StyleExitButton(btn_Resize, "O");

            this.BackColor = ColorTranslator.FromHtml("#F2F2F7");
            StyleButton(btn_Home, Properties.Resources.home, true);
            //btn.BackgroundImage = Image.FromFile(iconPath);
            InitDataGridView();
            this.FormBorderStyle = FormBorderStyle.None; // Loại bỏ viền để bo góc
            AutoLayoutControls();
            this.Icon = Properties.Resources.icon_ico; // Assuming you have an icon in your resources

            this.MinimumSize = new Size(1600, 900);

          

           


        }
        // Call this method in the QuanLyHopDong_Load event:
        private void QuanLyHopDong_Load(object sender, EventArgs e)
        {

            string dbPath = Path.Combine(Application.StartupPath, "Database", "data.db");
            if (!File.Exists(dbPath))
            {

                this.Hide();
                if (CustomMessageBox.ShowCustomYesNoMessageBox("Không tìm thấy cơ sở dữ liệu. Bạn có muốn nhập mật khẩu để mở cơ sở dữ liệu?", this, null, default, "LỖI CƠ SỞ DỮ LIỆU") == DialogResult.Yes)
                {

                    if (Application.OpenForms.OfType<CSDL.MatKhauCSDL>().Any())
                    {
                        Application.OpenForms.OfType<CSDL.MatKhauCSDL>().First().Show();
                        return;
                    }
                    else
                    {
                        var form = new CSDL.MatKhauCSDL();
                        form.Show();
                    }
                }
                else
                {
                    Application.Exit();
                }

            }

            else
            {

                //CapNhatTinhTrangHopDong();

                CapNhatTinhTrangLichSuDongLai();
                CapNhatTinhTrangMaHD();
                LuuNgayCapNhatMoi();
                CustomMessageBox.ShowCustomMessageBox("Cập nhật tình trạng hợp đồng thành công!");
                KhoiTaoPhanTrang();
                InitCbBoxSearch();


            }

        }
        // Màu nền và font mặc định cho ứng dụng

        private static readonly Font AppFont = new Font("Segoe UI", 11F, FontStyle.Regular);

        public static void StyleExitButton(Button btn, string text)
        {
            // Fluent Blue Grey tone
            Color baseColor = ColorTranslator.FromHtml("#607D8B");    // Blue Grey 500
            Color hoverColor = ColorTranslator.FromHtml("#78909C");   // Blue Grey 300-400
            Color textColor = Color.WhiteSmoke;

            btn.Text = text;
            btn.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btn.Size = new Size(44, 44);
            btn.BackColor = baseColor;
            btn.ForeColor = textColor;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Cursor = Cursors.Hand;
            btn.TextAlign = ContentAlignment.MiddleCenter;
            btn.UseCompatibleTextRendering = true;

            btn.FlatAppearance.MouseOverBackColor = hoverColor;
            btn.FlatAppearance.MouseDownBackColor = ColorTranslator.FromHtml("#546E7A"); // Blue Grey 600

            // Smooth corner
            btn.Region = Region.FromHrgn(NativeMethods.CreateRoundRectRgn(0, 0, btn.Width, btn.Height, 10, 10));
            btn.Resize += (s, e) =>
            {
                btn.Region = Region.FromHrgn(NativeMethods.CreateRoundRectRgn(0, 0, btn.Width, btn.Height, 10, 10));
            };

            // Optional shadow or glow can be added here (if needed)
        }





        // 3. StyleComboBox
        void StyleComboBox(ComboBox cb)
        {
            cb.Font = AppFonts.TextBox;
            cb.ForeColor = Color.Black;
            cb.BackColor = Color.White;
            cb.Region = System.Drawing.Region.FromHrgn(
                NativeMethods.CreateRoundRectRgn(0, 0, cb.Width, cb.Height, 20, 20)
            );
            cb.DrawMode = DrawMode.OwnerDrawFixed;
            cb.DropDownStyle = ComboBoxStyle.DropDownList;
            cb.DrawItem += (s, e) =>
            {
                e.DrawBackground();
                using (var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                using (var brush = new SolidBrush(cb.ForeColor))
                {
                    if (e.Index >= 0)
                    {
                        string text = cb.Items[e.Index]?.ToString() ?? "";
                        e.Graphics.DrawString(text, cb.Font, brush, e.Bounds, sf);
                    }
                }
                e.DrawFocusRectangle();
            };
            cb.FlatStyle = FlatStyle.Flat;
        }
        public QuanLyHopDong()
        {
            InitializeComponent();
            CustomizeUI();

        }


        // 4. InitDataGridView
        private void InitDataGridView()
        {
            dataGridView_ThongTinHopDong.Dock = DockStyle.None;
            dataGridView_ThongTinHopDong.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            dataGridView_ThongTinHopDong.Left = 20;
            dataGridView_ThongTinHopDong.Width = this.ClientSize.Width - 40;
            dataGridView_ThongTinHopDong.Height = this.ClientSize.Height - dataGridView_ThongTinHopDong.Top - 20;

            this.Resize += (s, ev) =>
            {
                dataGridView_ThongTinHopDong.Left = 20;
                dataGridView_ThongTinHopDong.Width = this.ClientSize.Width - 40;
                dataGridView_ThongTinHopDong.Height = this.ClientSize.Height - dataGridView_ThongTinHopDong.Top - 20;
            };

            // Fonts
            dataGridView_ThongTinHopDong.Font = AppFonts.Cell;
            dataGridView_ThongTinHopDong.ColumnHeadersDefaultCellStyle.Font = AppFonts.Header;

            // Căn giữa header & cell
            dataGridView_ThongTinHopDong.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView_ThongTinHopDong.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView_ThongTinHopDong.RowTemplate.Height = 38;
            dataGridView_ThongTinHopDong.ColumnHeadersHeight = 44;

            // Căn giữa cột khi thêm mới
            dataGridView_ThongTinHopDong.ColumnAdded += (s, e) =>
            {
                e.Column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                e.Column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                e.Column.DefaultCellStyle.Font = AppFonts.Cell;
                e.Column.HeaderCell.Style.Font = AppFonts.Header;
                e.Column.ReadOnly = true;
            };
            foreach (DataGridViewColumn col in dataGridView_ThongTinHopDong.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.DefaultCellStyle.Font = AppFonts.Cell;
                col.HeaderCell.Style.Font = AppFonts.Header;
                col.ReadOnly = true;
            }

            // 🎨 Màu sắc macOS/iOS-like
            dataGridView_ThongTinHopDong.BackgroundColor = Color.FromArgb(248, 249, 251); // #F8F9FB
            dataGridView_ThongTinHopDong.GridColor = Color.FromArgb(210, 215, 230);       // #D2D7E6
            dataGridView_ThongTinHopDong.BorderStyle = BorderStyle.None;
            dataGridView_ThongTinHopDong.EnableHeadersVisualStyles = false;

            // Header màu xanh tím nhạt, chữ trắng
            dataGridView_ThongTinHopDong.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(100, 130, 200); // #6482C8
            dataGridView_ThongTinHopDong.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView_ThongTinHopDong.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView_ThongTinHopDong.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridView_ThongTinHopDong.ColumnHeadersHeight = 50; // Tùy chỉnh chiều cao tiêu đề


            // Khi chọn dòng
            dataGridView_ThongTinHopDong.DefaultCellStyle.SelectionBackColor = Color.FromArgb(80, 120, 200); // #5078C8
            dataGridView_ThongTinHopDong.DefaultCellStyle.SelectionForeColor = Color.White;

            // Xen kẽ dòng dịu mắt
            dataGridView_ThongTinHopDong.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(235, 239, 245); // #EBEFF5

            // Thiết lập khác
            dataGridView_ThongTinHopDong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            

            dataGridView_ThongTinHopDong.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dataGridView_ThongTinHopDong.AllowUserToResizeRows = false;
            dataGridView_ThongTinHopDong.RowHeadersWidth = 40;
            dataGridView_ThongTinHopDong.ScrollBars = ScrollBars.Both;
            dataGridView_ThongTinHopDong.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView_ThongTinHopDong.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;

            dataGridView_ThongTinHopDong.ReadOnly = true;
            dataGridView_ThongTinHopDong.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        }




        // 2. StyleTextBox
        void StyleTextBox(TextBox tb, bool boGoc = true)
        {
            tb.Font = AppFonts.TextBox;
            tb.ForeColor = Color.Black;
            tb.TextAlign = HorizontalAlignment.Center;
            tb.Multiline = false;
            tb.AutoSize = false;

            using (var g = tb.CreateGraphics())
            {
                SizeF textSize = g.MeasureString("Ag", tb.Font);
                int newHeight = (int)Math.Ceiling(textSize.Height) + 16;
                tb.Height = newHeight;
            }

            tb.Padding = new Padding(0, 0, 0, 0);

            if (boGoc)
            {
                tb.Region = System.Drawing.Region.FromHrgn(
                    NativeMethods.CreateRoundRectRgn(0, 0, tb.Width, tb.Height, 18, 18)
                );
            }
            else
            {
                tb.Region = null;
            }
        }

        void ScaleAllControls(Control parent, float scale)
        {
            foreach (Control ctrl in parent.Controls)
            {
                ctrl.Left = (int)(ctrl.Left * scale);
                ctrl.Top = (int)(ctrl.Top * scale);
                ctrl.Width = (int)(ctrl.Width * scale);
                ctrl.Height = (int)(ctrl.Height * scale);
                ctrl.Font = new Font(ctrl.Font.FontFamily, ctrl.Font.Size * scale, ctrl.Font.Style);

                if (ctrl.HasChildren)
                    ScaleAllControls(ctrl, scale);
            }
        }

        // Thay thế các dòng Font hardcode trong StyleButton, StyleTextBox, StyleComboBox, InitDataGridView, ... bằng AppFonts tương ứng

        // 1. StyleButton
        public static void StyleButton(Button btn, Image icon = null, bool boGoc = true)
        {
            btn.Font = AppFonts.Button;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = Color.Transparent;
            btn.ForeColor = Color.White;
            btn.Cursor = Cursors.Hand;
            btn.TextAlign = ContentAlignment.MiddleCenter;
            btn.AutoSize = false;

            int minW = 110, minH = 50;
            if (icon != null)
            {
                minW = 120;
                minH = 56;
            }

            using (var g = btn.CreateGraphics())
            {
                Size textSize = TextRenderer.MeasureText(btn.Text, btn.Font);
                int paddingW = 30;
                int paddingH = 16;

                btn.Width = Math.Max(textSize.Width + paddingW, minW);
                btn.Height = Math.Max(textSize.Height + paddingH, minH);
            }

            if (boGoc)
            {
                btn.Region = Region.FromHrgn(
                    NativeMethods.CreateRoundRectRgn(0, 0, btn.Width + 2, btn.Height + 2, 22, 22));
            }

            Color normalBack = Color.FromArgb(100, 140, 240);
            Color hoverBack = Color.FromArgb(130, 170, 255);
            Color clickBack = Color.FromArgb(80, 120, 210);

            bool isHover = false, isClick = false;

            btn.MouseEnter += (s, e) => { isHover = true; btn.Invalidate(); };
            btn.MouseLeave += (s, e) => { isHover = false; btn.Invalidate(); };
            btn.MouseDown += (s, e) => { isClick = true; btn.Invalidate(); };
            btn.MouseUp += (s, e) => { isClick = false; btn.Invalidate(); };

            btn.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.Clear(btn.Parent?.BackColor ?? SystemColors.Control);

                Color backColor = isClick ? clickBack : isHover ? hoverBack : normalBack;

                using (GraphicsPath path = CreateRoundedRectPath(
                    new Rectangle(0, 0, btn.Width - 1, btn.Height - 1), boGoc ? 22 : 0))
                using (SolidBrush brush = new SolidBrush(backColor))
                {
                    e.Graphics.FillPath(brush, path);
                }

                if (icon != null)
                {
                    int minIconSize = 32;
                    int padding = 8;
                    int maxW = btn.Width - padding * 2;
                    int maxH = btn.Height - padding * 2;

                    Size iconSize = icon.Size;
                    float scale = Math.Min((float)maxW / iconSize.Width, (float)maxH / iconSize.Height);
                    int drawW = (int)(iconSize.Width * scale);
                    int drawH = (int)(iconSize.Height * scale);

                    drawW = Math.Max(drawW, minIconSize);
                    drawH = Math.Max(drawH, minIconSize);

                    int drawX = (btn.Width - drawW) / 2;
                    int drawY = (btn.Height - drawH) / 2;

                    e.Graphics.DrawImage(icon, new Rectangle(drawX, drawY, drawW, drawH));
                }
                else
                {
                    Rectangle textRect = new Rectangle(0, -1, btn.Width, btn.Height);
                    TextRenderer.DrawText(e.Graphics, btn.Text, btn.Font, textRect, btn.ForeColor,
                        TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak);
                }
            };
        }





        private static GraphicsPath CreateRoundedRectPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int d = radius * 2;

            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();

            return path;
        }











        private void StyleFlowLayoutPanel(FlowLayoutPanel flowLayoutPanel)
        {
            flowLayoutPanel.AutoSize = true;
            flowLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowLayoutPanel.FlowDirection = FlowDirection.LeftToRight;
            flowLayoutPanel.WrapContents = true;
            flowLayoutPanel.Padding = new Padding(5, 5, 5, 5);

            flowLayoutPanel.Margin = new Padding(0);

        }

        private void AutoLayoutControls()
        {
            // Đảm bảo TableLayoutPanel co giãn đúng
            tbLayout_Form.Dock = DockStyle.Fill;
            tbLayout_Form.ColumnStyles.Clear();
            tbLayout_Form.RowStyles.Clear();
            tbLayout_Form.ColumnCount = 1;
            tbLayout_Form.RowCount = 2;
            tbLayout_Form.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tbLayout_Form.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // Hàng nút
            tbLayout_Form.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // Hàng DataGridView

            // tbLayout_Button: 2 hàng, 2 cột
            tbLayout_Button.Dock = DockStyle.Fill;
            tbLayout_Button.ColumnStyles.Clear();
            tbLayout_Button.RowStyles.Clear();
            tbLayout_Button.ColumnCount = 2;
            tbLayout_Button.RowCount = 2;
            tbLayout_Button.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbLayout_Button.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbLayout_Button.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tbLayout_Button.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            // flowLayoutPanel_HopDong: top left
            flowLayoutPanel_HopDong.Dock = DockStyle.Fill;
            tbLayout_Button.Controls.Add(flowLayoutPanel_HopDong, 0, 0);

            // Ô top right (ô 1,0): nếu có panel khác thì add vào đây, ví dụ flowLayoutPanel_UseForm
            flowLayoutPanel_UseForm.Dock = DockStyle.Fill;
            tbLayout_Button.Controls.Add(flowLayoutPanel_UseForm, 1, 0);

            // flowLayoutPanel_Search: dưới flowLayoutPanel_HopDong (ô 0,1)
            flowLayoutPanel_Search.Dock = DockStyle.Fill;
            tbLayout_Button.Controls.Add(flowLayoutPanel_Search, 0, 1);

            // Ô (1,1) trống, không add gì

            // dataGridView_ThongTinHopDong: hàng 2, cột 1 của tbLayout_Form
            dataGridView_ThongTinHopDong.Dock = DockStyle.Fill;

            // Đảm bảo các panel tự co giãn khi resize
            tbLayout_Form.Controls.SetChildIndex(tbLayout_Button, 0);
            tbLayout_Form.Controls.SetChildIndex(dataGridView_ThongTinHopDong, 1);

            // Gọi lại style cho các FlowLayoutPanel nếu cần
            StyleFlowLayoutPanel(flowLayoutPanel_HopDong);
            StyleFlowLayoutPanel(flowLayoutPanel_UseForm);
            // Fixing the error CS0266 by correcting the assignment to FlowDirection
            flowLayoutPanel_UseForm.FlowDirection = FlowDirection.RightToLeft; // Corrected to use the FlowDirection enum


            StyleFlowLayoutPanel(flowLayoutPanel_Search);
        }

        // Class hỗ trợ bo góc cho buttont
        internal static class NativeMethods
        {
            [System.Runtime.InteropServices.DllImport("gdi32.dll", SetLastError = true)]
            public static extern IntPtr CreateRoundRectRgn(
                int nLeftRect, int nTopRect, int nRightRect,
                int nBottomRect, int nWidthEllipse, int nHeightEllipse);
        }



        // Sự kiện đóng ứng dụng
        private void btnClose_Click(object sender, EventArgs e)
        {
            if (CustomMessageBox.ShowCustomYesNoMessageBox("Bạn có chắc chắn muốn thoát ứng dụng?", this, null, default, "XÁC NHẬN THOÁT") == DialogResult.Yes)
            {
                Application.Exit();
            }

        }

        // Sự kiện khi form đóng, ẩn form hiện tại và mở lại TrangChu
        private void QuanLyHopDong_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
                var mainForm = Application.OpenForms.OfType<TrangChu>().FirstOrDefault();
                if (mainForm != null)
                {
                    mainForm.Show();
                    mainForm.BringToFront();
                }
                else
                {
                    var newMainForm = new TrangChu();
                    newMainForm.Show();
                }
            }
        }

        // Sự kiện quay lại TrangChu
        private void btn_QuayLai_Click(object sender, EventArgs e)
        {
            this.Hide();
            if (Application.OpenForms.OfType<TrangChu>().Any())
            {
                Application.OpenForms.OfType<TrangChu>().First().Show();
                return;
            }
            var trangChuForm = new TrangChu();
            trangChuForm.Show();
        }

        // Hàm mẫu cho các button khác (nếu cần)
        private void button1_Click(object sender, EventArgs e)
        {

            if (Application.OpenForms.OfType<HopDongForm>().Any())
            {
                Application.OpenForms.OfType<HopDongForm>().First().Show();
                return;
            }
            var hopDongForm = new HopDongForm(null, false);
            if (hopDongForm.ShowDialog() == DialogResult.OK)
            {
                KhoiTaoPhanTrang(); // Load lại dữ liệu để hiển thị hợp đồng mới nhất
            }


        }



        private void btn_MoCSDL_Click(object sender, EventArgs e)
        {
            this.Hide();
            if (Application.OpenForms.OfType<CSDL.MatKhauCSDL>().Any())
            {
                Application.OpenForms.OfType<CSDL.MatKhauCSDL>().First().Show();
                return;
            }
            else
            {
                var form = new CSDL.MatKhauCSDL();
                form.Show();
            }
        }



        private void button1_Click_1(object sender, EventArgs e)
        {

            string? MaHD = dataGridView_ThongTinHopDong.CurrentRow?.Cells["MaHD"].Value?.ToString();
            if (string.IsNullOrEmpty(MaHD))
            {
                CustomMessageBox.ShowCustomMessageBox("Vui lòng chọn một hợp đồng để chỉnh sửa.");
                return;
            }

            // Nếu form đã mở, show lên (tùy bạn có muốn cho mở nhiều hay không)
            if (Application.OpenForms.OfType<HopDongForm>().Any())
            {
                Application.OpenForms.OfType<HopDongForm>().First().BringToFront();
                return;
            }

            // Mở form sửa hợp đồng
            var hopDongForm = new HopDongForm(MaHD, false);

            // Sử dụng ShowDialog để chờ người dùng bấm Lưu
            if (hopDongForm.ShowDialog() == DialogResult.OK)
            {
                CapNhatTinhTrangLichSuDongLai(MaHD); // Cập nhật tình trạng lịch sử đóng lãi

                CapNhatTinhTrangMaHD(MaHD); // Cập nhật tình trạng hợp đồng
                var hopDong = HopDongForm.GetHopDongByMaHD(MaHD);
                CapNhatDongTheoMaHD(hopDong); // Chỉ cập nhật lại dòng hiện tại


            }

        }
        public static decimal CapNhatLaiDenHomNay(string maHD)
        {
            decimal tongLai = 0;
            string dbPath = Path.Combine(Application.StartupPath, "DataBase", "data.db");

            using (var connection = new SqliteConnection($"Data Source={dbPath}"))
            {
                connection.Open();

                // 1. Lấy LaiMoiNgay
                decimal laiMoiNgay = 0;
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT LaiMoiNgay FROM HopDongVay WHERE MaHD = @MaHD";
                    cmd.Parameters.AddWithValue("@MaHD", maHD);
                    var result = cmd.ExecuteScalar();
                    if (result == null || result == DBNull.Value)
                        return 0;
                    laiMoiNgay = Convert.ToDecimal(result);
                }

                // 2. Lấy danh sách kỳ lãi
                var kyList = new List<(DateTime NgayBatDauKy, DateTime NgayDenHan, decimal SoTienPhaiDong, decimal SoTienDaDong, int TinhTrang)>();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"
                SELECT NgayBatDauKy, NgayDenHan, SoTienPhaiDong, SoTienDaDong, TinhTrang
                FROM LichSuDongLai
                WHERE MaHD = @MaHD
                ORDER BY date(NgayBatDauKy) ASC";
                    cmd.Parameters.AddWithValue("@MaHD", maHD);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DateTime ngayBatDauKy = DateTime.Parse(reader["NgayBatDauKy"].ToString() ?? "");
                            DateTime ngayDenHan = DateTime.Parse(reader["NgayDenHan"].ToString() ?? "");
                            decimal soTienPhaiDong = Convert.ToDecimal(reader["SoTienPhaiDong"] ?? 0);
                            decimal soTienDaDong = Convert.ToDecimal(reader["SoTienDaDong"] ?? 0);
                            int tinhTrang = Convert.ToInt32(reader["TinhTrang"] ?? 0);

                            kyList.Add((ngayBatDauKy, ngayDenHan, soTienPhaiDong, soTienDaDong, tinhTrang));
                        }
                    }
                }

                DateTime now = DateTime.Now.Date;

                foreach (var ky in kyList)
                {
                    // Nếu đã tất toán hoặc đóng đủ
                    if (ky.TinhTrang == 0 || ky.SoTienDaDong >= ky.SoTienPhaiDong)
                        continue;

                    DateTime ngayKetThucTinhLai = now < ky.NgayDenHan ? now : ky.NgayDenHan;

                    if (ngayKetThucTinhLai < ky.NgayBatDauKy)
                        continue;

                    int soNgayTinhLai = (ngayKetThucTinhLai - ky.NgayBatDauKy).Days + 1;
                    if (soNgayTinhLai <= 0)
                        continue;

                    decimal tongLaiKy = soNgayTinhLai * laiMoiNgay;
                    decimal conNo = tongLaiKy - ky.SoTienDaDong;
                    if (conNo > 0)
                        tongLai += conNo;
                }
            }

            return tongLai;
        }


        private void CapNhatDongTheoMaHD(HopDongModel hopDong)
        {
            foreach (DataGridViewRow row in dataGridView_ThongTinHopDong.Rows)
            {
                if (row.Cells["MaHD"].Value?.ToString() == hopDong.MaHD)
                {
                    decimal laidenhomnay = CapNhatLaiDenHomNay(hopDong.MaHD);
                    row.Cells["TenKH"].Value = hopDong.TenKH;
                    row.Cells["TenTaiSan"].Value = hopDong.TenTaiSan;
                    row.Cells["TienVay"].Value = Function_Reuse.FormatNumberWithThousandsSeparator(hopDong.TienVay);
                    row.Cells["NgayVay"].Value = hopDong.NgayVay;
                    row.Cells["LaiDaDong"].Value = Function_Reuse.FormatNumberWithThousandsSeparator(hopDong.TienLaiDaDong ?? 0);
                    row.Cells["LaiDenHomNay"].Value = Function_Reuse.FormatNumberWithThousandsSeparator(laidenhomnay);
                    decimal tongLai = hopDong.TongLai ?? 0;
                    decimal tienLaiDaDong = hopDong.TienLaiDaDong ?? 0;
                    decimal tienNo = tongLai - tienLaiDaDong;
                    row.Cells["TienNo"].Value = Function_Reuse.FormatNumberWithThousandsSeparator(tienNo);

                    row.Cells["NgayPhaiDongLai"].Value = hopDong.NgayDongLaiGanNhat;
                    if (hopDong.TinhTrang == 0)
                    {
                        row.Cells["TinhTrang"].Value = "Đã tất toán";
                        row.DefaultCellStyle.BackColor = Color.Gray; // Màu xám cho đã tất toán

                    }
                    else if (hopDong.TinhTrang == 1)
                    {
                        row.Cells["TinhTrang"].Value = "Đang vay";
                        row.DefaultCellStyle.BackColor = Color.White; // Màu trắng cho đang vay
                    }
                    else if (hopDong.TinhTrang == 2)
                    {
                        row.Cells["TinhTrang"].Value = "Sắp đến hạn";
                        row.DefaultCellStyle.BackColor = Color.LightYellow; // Màu vàng nhạt cho sắp đến hạn
                    }
                    else if (hopDong.TinhTrang == 3)
                    {
                        row.Cells["TinhTrang"].Value = "Quá hạn";
                        row.DefaultCellStyle.BackColor = Color.LightCoral; // Màu đỏ nhạt cho quá hạn

                    }
                    else if (hopDong.TinhTrang == 4)
                    {
                        row.Cells["TinhTrang"].Value = "Tới hạn đóng lãi";
                        row.DefaultCellStyle.BackColor = Color.LightGreen; // Màu xanh lá nhạt cho đã đóng lãi
                    }
                    else if (hopDong.TinhTrang == 5)
                    {
                        row.Cells["TinhTrang"].Value = "Đã đóng lãi";
                        row.DefaultCellStyle.BackColor = Color.Green; // Màu xanh dương nhạt cho đã đóng lãi
                    }
                    else
                    {
                        row.Cells["TinhTrang"].Value = "Không xác định";
                        row.DefaultCellStyle.BackColor = Color.White; // Màu trắng cho không xác định

                        break; // Tìm thấy là thoát
                    }
                }
            }
        }




        // Thay thế hoặc cập nhật sự kiện CellContentClick để xử lý nút Ghi chú và Lịch sử
        private void DataGridView_ThongTinHopDong_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var grid = dataGridView_ThongTinHopDong;

            // Xử lý nút Ghi chú
            if (grid.Columns[e.ColumnIndex].Name == "GhiChu")
            {
                string? maHD = grid.Rows[e.RowIndex].Cells["MaHD"].Value?.ToString();
                string? ghiChu = null;
                if (!string.IsNullOrWhiteSpace(maHD))
                {
                    string dbPath = Path.Combine(Application.StartupPath, "DataBase", "data.db");
                    using (var connection = new Microsoft.Data.Sqlite.SqliteConnection($"Data Source={dbPath}"))
                    {
                        connection.Open();
                        using (var command = connection.CreateCommand())
                        {
                            command.CommandText = "SELECT GhiChu FROM HopDongVay WHERE MaHD = @MaHD LIMIT 1";
                            command.Parameters.AddWithValue("@MaHD", maHD);
                            var result = command.ExecuteScalar();
                            if (result != null && result != DBNull.Value)
                                ghiChu = result.ToString();
                        }
                    }
                }
                if (string.IsNullOrWhiteSpace(ghiChu))
                    CustomMessageBox.ShowCustomMessageBox("Không có ghi chú.");
                else
                    CustomMessageBox.ShowCustomMessageBox(ghiChu);
                return;
            }

            // Xử lý nút Lịch sử (hiển thị cột LichSu trong db như GhiChu)
            if (grid.Columns[e.ColumnIndex].Name == "LichSu")
            {
                string? maHD = grid.Rows[e.RowIndex].Cells["MaHD"].Value?.ToString();
                string? lichSu = null;
                if (!string.IsNullOrWhiteSpace(maHD))
                {
                    string dbPath = Path.Combine(Application.StartupPath, "DataBase", "data.db");
                    using (var connection = new Microsoft.Data.Sqlite.SqliteConnection($"Data Source={dbPath}"))
                    {
                        connection.Open();
                        using (var command = connection.CreateCommand())
                        {
                            command.CommandText = "SELECT LichSu FROM HopDongVay WHERE MaHD = @MaHD LIMIT 1";
                            command.Parameters.AddWithValue("@MaHD", maHD);
                            var result = command.ExecuteScalar();
                            if (result != null && result != DBNull.Value)
                                lichSu = result.ToString();
                        }
                    }
                }

                if (string.IsNullOrWhiteSpace(lichSu))
                    CustomMessageBox.ShowCustomMessageBox("Không có lịch sử.");
                else
                {

                    if (Application.OpenForms.OfType<TextToScreen>().Any())
                    {
                        Application.OpenForms.OfType<TextToScreen>().First().Show();
                        return;
                    }
                    var frm_XuatText = new TextToScreen(lichSu, "Lịch sử thay đổi hợp đồng mã: ", maHD);
                    frm_XuatText.Show();
                }
                return;
            }

            // Xử lý nút Thao tác (giữ nguyên logic cũ)
            if (grid.Columns[e.ColumnIndex].Name == "ThaoTac")
            {
                string? maHD = grid.Rows[e.RowIndex].Cells["MaHD"].Value?.ToString();
                string? tinhTrang = grid.Rows[e.RowIndex].Cells["TinhTrang"].Value?.ToString();
                if (maHD == null || maHD == string.Empty)
                {
                    CustomMessageBox.ShowCustomMessageBox("Vui lòng chọn một hợp đồng để xem chi tiết.");
                    return;
                }
                CustomMessageBox.ShowCustomMessageBox($"Bạn đã chọn hợp đồng: {maHD}");
                if (Application.OpenForms.OfType<LichSuDongLai>().Any())
                {
                    Application.OpenForms.OfType<LichSuDongLai>().First().BringToFront();
                    return;
                }
                var LichSuDongLaiform = new LichSuDongLai(maHD, tinhTrang);

                if (LichSuDongLaiform.ShowDialog() == DialogResult.Yes)
                {
                    if (maHD != null)
                    {
                        CapNhatTinhTrangLichSuDongLai(maHD); // Cập nhật tình trạng lịch sử đóng lãi
                        CapNhatTinhTrangMaHD(maHD); // Cập nhật tình trạng hợp đồng
                        var hopDong = HopDongForm.GetHopDongByMaHD(maHD);
                        CapNhatDongTheoMaHD(hopDong); // Chỉ cập nhật lại dòng hiện tại
                    }
                }
            }
        }
        private void InitCbBoxSearch()
        {
            var items = new List<TimKiemHopDongItem>
    {
        new TimKiemHopDongItem { ID = -1, FieldName = null, DisplayName = "Tất cả" },
        new TimKiemHopDongItem { ID = 0, FieldName = "DaChuoc", DisplayName = "Đã chuộc" },
        new TimKiemHopDongItem { ID = 1, FieldName = "DangVay", DisplayName = "Đang vay" },
        new TimKiemHopDongItem { ID = 2, FieldName = "SapToiHan", DisplayName = "Sắp tới hạn" },
        new TimKiemHopDongItem { ID = 3, FieldName = "QuaHan", DisplayName = "Quá hạn" },
        new TimKiemHopDongItem { ID = 4, FieldName = "ToiHanHomNay", DisplayName = "Tới hạn hôm nay" },
        new TimKiemHopDongItem { ID = 5, FieldName = "ToiHanVaDaDong", DisplayName = "Tới hạn hôm nay và đã đóng" },
    };

            cbBox_Search.DataSource = items;
            cbBox_Search.DisplayMember = "DisplayName";
            cbBox_Search.ValueMember = "FieldName";
            cbBox_Search.SelectedIndex = 0;

        }


        public class TimKiemHopDongItem
        {
            public int ID { get; set; }
            public string? FieldName { get; set; }
            public string DisplayName { get; set; }
            public override string ToString() => DisplayName;
        }
       
       


        public static void CapNhatTinhTrangMaHD(string? maHD = null)
        {
            string dbPath = Path.Combine(Application.StartupPath, "DataBase", "data.db");
            using (var connection = new SqliteConnection($"Data Source={dbPath}"))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    // Ưu tiên 3: Quá hạn
                    command.CommandText = $@"
                UPDATE HopDongVay
                SET TinhTrang = 3, UpdatedAt = CURRENT_TIMESTAMP
                WHERE EXISTS (
                    SELECT 1 FROM LichSuDongLai
                    WHERE MaHD = HopDongVay.MaHD AND TinhTrang = 3
                    {(string.IsNullOrEmpty(maHD) ? "" : "AND MaHD = @MaHD")}
                )
                {(string.IsNullOrEmpty(maHD) ? "" : "AND MaHD = @MaHD")};";
                    if (!string.IsNullOrEmpty(maHD)) command.Parameters.AddWithValue("@MaHD", maHD);
                    command.ExecuteNonQuery();

                    // Ưu tiên 4: Tới hạn hôm nay
                    command.Parameters.Clear();
                    command.CommandText = $@"
                UPDATE HopDongVay
                SET TinhTrang = 4, UpdatedAt = CURRENT_TIMESTAMP
                WHERE EXISTS (
                    SELECT 1 FROM LichSuDongLai
                    WHERE MaHD = HopDongVay.MaHD AND date(NgayDenHan) = date('now')
                    {(string.IsNullOrEmpty(maHD) ? "" : "AND MaHD = @MaHD")}
                )
                AND NOT EXISTS (
                    SELECT 1 FROM LichSuDongLai
                    WHERE MaHD = HopDongVay.MaHD AND TinhTrang = 3
                )
                {(string.IsNullOrEmpty(maHD) ? "" : "AND MaHD = @MaHD")};";
                    if (!string.IsNullOrEmpty(maHD)) command.Parameters.AddWithValue("@MaHD", maHD);
                    command.ExecuteNonQuery();

                    // Ưu tiên 2: Sắp tới hạn
                    command.Parameters.Clear();
                    command.CommandText = $@"
                UPDATE HopDongVay
                SET TinhTrang = 2, UpdatedAt = CURRENT_TIMESTAMP
                WHERE EXISTS (
                    SELECT 1 FROM LichSuDongLai
                    WHERE MaHD = HopDongVay.MaHD AND TinhTrang = 2
                    {(string.IsNullOrEmpty(maHD) ? "" : "AND MaHD = @MaHD")}
                )
                AND NOT EXISTS (
                    SELECT 1 FROM LichSuDongLai
                    WHERE MaHD = HopDongVay.MaHD AND TinhTrang IN (3, 4)
                )
                {(string.IsNullOrEmpty(maHD) ? "" : "AND MaHD = @MaHD")};";
                    if (!string.IsNullOrEmpty(maHD)) command.Parameters.AddWithValue("@MaHD", maHD);
                    command.ExecuteNonQuery();

                    // Ưu tiên 1: Đang vay
                    command.Parameters.Clear();
                    command.CommandText = $@"
                UPDATE HopDongVay
                SET TinhTrang = 1, UpdatedAt = CURRENT_TIMESTAMP
                WHERE EXISTS (
                    SELECT 1 FROM LichSuDongLai
                    WHERE MaHD = HopDongVay.MaHD AND TinhTrang = 1
                    {(string.IsNullOrEmpty(maHD) ? "" : "AND MaHD = @MaHD")}
                )
                AND NOT EXISTS (
                    SELECT 1 FROM LichSuDongLai
                    WHERE MaHD = HopDongVay.MaHD AND TinhTrang IN (2, 3, 4)
                )
                {(string.IsNullOrEmpty(maHD) ? "" : "AND MaHD = @MaHD")};";
                    if (!string.IsNullOrEmpty(maHD)) command.Parameters.AddWithValue("@MaHD", maHD);
                    command.ExecuteNonQuery();

                    // Ưu tiên 5: Tới hạn hôm nay nhưng đã đóng (TinhTrang = 5)
                    command.Parameters.Clear();
                    command.CommandText = $@"
                UPDATE HopDongVay
                SET TinhTrang = 5, UpdatedAt = CURRENT_TIMESTAMP
                WHERE EXISTS (
                    SELECT 1 FROM LichSuDongLai
                    WHERE MaHD = HopDongVay.MaHD AND TinhTrang = 5
                    {(string.IsNullOrEmpty(maHD) ? "" : "AND MaHD = @MaHD")}
                )
               
                {(string.IsNullOrEmpty(maHD) ? "" : "AND MaHD = @MaHD")};";
                    if (!string.IsNullOrEmpty(maHD)) command.Parameters.AddWithValue("@MaHD", maHD);
                    command.ExecuteNonQuery();

                    // Ưu tiên 0: Tất toán (tất cả kỳ = 0)
                    command.Parameters.Clear();
                    command.CommandText = $@"
                UPDATE HopDongVay
                SET TinhTrang = 0, UpdatedAt = CURRENT_TIMESTAMP
                WHERE NOT EXISTS (
                    SELECT 1 FROM LichSuDongLai
                    WHERE MaHD = HopDongVay.MaHD AND TinhTrang !=0
                    {(string.IsNullOrEmpty(maHD) ? "" : "AND MaHD = @MaHD")}
                )
                {(string.IsNullOrEmpty(maHD) ? "" : "AND MaHD = @MaHD")};";
                    if (!string.IsNullOrEmpty(maHD)) command.Parameters.AddWithValue("@MaHD", maHD);
                    command.ExecuteNonQuery();
                }
            }
            CapNhatLichSuTinhTrangTheoMaHD(maHD); // Cập nhật lịch sử tình trạng hợp đồng
        }

        public static void CapNhatLichSuTinhTrangTheoMaHD(string maHD)
        {
            if (string.IsNullOrEmpty(maHD)) return;

            string dbPath = Path.Combine(Application.StartupPath, "DataBase", "data.db");

            using (var connection = new SqliteConnection($"Data Source={dbPath}"))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT TinhTrang FROM HopDongVay WHERE MaHD = @MaHD";
                    command.Parameters.AddWithValue("@MaHD", maHD);

                    var tinhTrangObj = command.ExecuteScalar();
                    if (tinhTrangObj == null || tinhTrangObj == DBNull.Value) return;

                    int tinhTrang = Convert.ToInt32(tinhTrangObj);

                    string moTa = MoTaTinhTrang(tinhTrang);
                    string ghiChu = $"TÌNH TRẠNG: {DateTime.Now:dd/MM/yyyy HH:mm:ss} - {moTa}\n__________________________________________________________________________________________\n";

                    using (var updateCmd = connection.CreateCommand())
                    {
                        updateCmd.CommandText = @"
                    UPDATE HopDongVay
                    SET LichSu = @LichSuMoi || COALESCE(LichSu, '')
                    WHERE MaHD = @MaHD";
                        updateCmd.Parameters.AddWithValue("@LichSuMoi", ghiChu);
                        updateCmd.Parameters.AddWithValue("@MaHD", maHD);
                        updateCmd.ExecuteNonQuery();
                    }
                }
            }
        }

        private static string MoTaTinhTrang(int tinhTrang)
        {
            return tinhTrang switch
            {
                0 => "Tất toán",
                1 => "Đang vay",
                2 => "Sắp tới hạn",
                3 => "Quá hạn",
                4 => "Tới hạn hôm nay",
                5 => "Tới hạn đã đóng",
                _ => "Không xác định"
            };
        }



        public static void CapNhatTinhTrangLichSuDongLai(string? maHD = null)
        {
            string dbPath = Path.Combine(Application.StartupPath, "DataBase", "data.db");
            using (var connection = new SqliteConnection($"Data Source={dbPath}"))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    string maHDCondition = string.IsNullOrEmpty(maHD) ? "" : "AND MaHD = @MaHD";

                    command.CommandText = $@"
               -- 3: Quá hạn
UPDATE LichSuDongLai
SET TinhTrang = 3, UpdatedAt = CURRENT_TIMESTAMP
WHERE SoTienDaDong < SoTienPhaiDong
  AND date('now', 'localtime') > date(NgayDenHan)
  {maHDCondition};

-- 4: Tới hạn hôm nay
UPDATE LichSuDongLai
SET TinhTrang = 4, UpdatedAt = CURRENT_TIMESTAMP
WHERE SoTienDaDong < SoTienPhaiDong
  AND date(NgayDenHan) = date('now', 'localtime')
  {maHDCondition};

-- 2: Sắp tới hạn
UPDATE LichSuDongLai
SET TinhTrang = 2, UpdatedAt = CURRENT_TIMESTAMP
WHERE SoTienDaDong < SoTienPhaiDong
  AND date('now', 'localtime') < date(NgayDenHan)
  AND julianday(NgayDenHan) - julianday('now', 'localtime') <= 3
  AND date(NgayDenHan) != date('now', 'localtime')
  {maHDCondition};

-- 1: Đang vay
UPDATE LichSuDongLai
SET TinhTrang = 1, UpdatedAt = CURRENT_TIMESTAMP
WHERE SoTienDaDong < SoTienPhaiDong
  AND date('now', 'localtime') < date(NgayDenHan)
  AND julianday(NgayDenHan) - julianday('now', 'localtime') > 3
  {maHDCondition};

-- 5: Đã đóng đủ và đúng ngày
UPDATE LichSuDongLai
SET TinhTrang = 5, UpdatedAt = CURRENT_TIMESTAMP
WHERE SoTienDaDong >= SoTienPhaiDong
  AND date(NgayDenHan) = date('now', 'localtime')
  {maHDCondition};

-- 0: Đã đóng đủ nhưng không phải hôm nay
UPDATE LichSuDongLai
SET TinhTrang = 0, UpdatedAt = CURRENT_TIMESTAMP
WHERE SoTienDaDong >= SoTienPhaiDong
  AND date(NgayDenHan) != date('now', 'localtime')
  {maHDCondition};

            ";

                    if (!string.IsNullOrEmpty(maHD))
                        command.Parameters.AddWithValue("@MaHD", maHD);

                    command.ExecuteNonQuery();
                }
            }
        }




        private bool CanCapNhatTheoNgay()
        {
            string dbPath = Path.Combine(Application.StartupPath, "DataBase", "data.db");
            using (var connection = new SqliteConnection($"Data Source={dbPath}"))
            {
                connection.Open();

                // Lấy ngày cập nhật cuối
                var command = connection.CreateCommand();
                command.CommandText = @"
            SELECT GiaTri FROM HeThong WHERE Khoa = 'LanCapNhatTinhTrang'";
                var lastUpdateStr = command.ExecuteScalar()?.ToString();

                if (DateTime.TryParse(lastUpdateStr, out DateTime lastUpdateDate))
                {
                    // Nếu đã qua ngày thì cho phép cập nhật
                    return DateTime.Now.Date > lastUpdateDate.Date;
                }

                // Chưa có giá trị => cho phép cập nhật
                return true;
            }
        }

        private void LuuNgayCapNhatMoi()
        {
            string dbPath = Path.Combine(Application.StartupPath, "DataBase", "data.db");
            using (var connection = new SqliteConnection($"Data Source={dbPath}"))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"
            INSERT INTO HeThong(Khoa, GiaTri, GhiChu, UpdatedAt)
            VALUES ('LanCapNhatTinhTrang', date('now'), 'Lần cập nhật tình trạng gần nhất', CURRENT_TIMESTAMP)
            ON CONFLICT(Khoa) DO UPDATE SET GiaTri = date('now'), UpdatedAt = CURRENT_TIMESTAMP";
                command.ExecuteNonQuery();
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void thêmToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }



        private void cbBox_Search_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btn_UpdateInfoSystem_Click_1(object sender, EventArgs e)
        {
            if (CanCapNhatTheoNgay())
            {


                CapNhatTinhTrangLichSuDongLai();
                LuuNgayCapNhatMoi();
                CustomMessageBox.ShowCustomMessageBox("Cập nhật tình trạng hợp đồng thành công!");
                CapNhatTinhTrangMaHD();
                KhoiTaoPhanTrang(); // Tải lại dữ liệu sau khi cập nhật
            }
            else
            {
                CustomMessageBox.ShowCustomMessageBox("Bạn chỉ có thể cập nhật tình trạng hợp đồng một lần mỗi ngày.");
            }
        }
        // row header paint để hiển thị STT
        private void dataGridView_ThongTinHopDong_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var grid = sender as DataGridView;

            // Tính toán kích thước STT
            string rowIdx = (e.RowIndex + 1).ToString();
            using (var centerFormat = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            })
            {
                var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top,
                                                 grid.RowHeadersWidth, e.RowBounds.Height);
                e.Graphics.DrawString(rowIdx, this.Font, SystemBrushes.ControlText,
                                      headerBounds, centerFormat);
            }
        }

        private void btn_Home_Click(object sender, EventArgs e)
        {
            isSearchMode = false;
            searchKeyword = null;
            searchField = null;
            currentSearchPage = 1;

            KhoiTaoPhanTrang(); // Load trang đầu bình thường
        }

        private void btn_About_Click(object sender, EventArgs e)
        {

        }

        private void btn_Hide_Click(object sender, EventArgs e)
        {

            this.WindowState = FormWindowState.Minimized; // Thu nhỏ form 

        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            string tuKhoa = tb_Search.Text.Trim(); // TextBox tìm theo MãHD, SĐT hoặc CCCD
            var selectedItem = cbBox_Search.SelectedItem as TimKiemHopDongItem; // ComboBox lọc tình trạng

            bool coTuKhoa = !string.IsNullOrEmpty(tuKhoa);
            bool coTinhTrang = selectedItem != null && !string.IsNullOrEmpty(selectedItem.FieldName);

            // Bật chế độ tìm kiếm
            isSearchMode = true;
            searchKeyword = tuKhoa;
            searchField = coTinhTrang ? selectedItem.FieldName : null;

            currentSearchPage = 1; // reset về trang đầu tìm kiếm

            var ketQua = LayHopDong_TimKiemPhanTrang(searchKeyword, searchField, currentSearchPage, pageSize);
            HienThiHopDong(ketQua);
        }
        // Pseudocode plan:
        // - Override the OnHandleCreated event to set a rounded region for the form
        // - Use NativeMethods.CreateRoundRectRgn to create a rounded rectangle region
        // - Set the form's Region property to the rounded region
        // - Update the region when the form is resized to maintain rounded corners

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            SetFormRoundCorners();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            SetFormRoundCorners();
        }

        private void SetFormRoundCorners()
        {
            int radius = 32; // Adjust the radius as needed
            if (this.Width > 0 && this.Height > 0)
            {
                IntPtr hRgn = NativeMethods.CreateRoundRectRgn(0, 0, this.Width + 1, this.Height + 1, radius, radius);
                this.Region = Region.FromHrgn(hRgn);
            }
        }

        private void btn_Resize_Click(object sender, EventArgs e)
        {
            this.WindowState = this.WindowState == FormWindowState.Maximized ? FormWindowState.Normal : FormWindowState.Maximized;
        }
    }
}
