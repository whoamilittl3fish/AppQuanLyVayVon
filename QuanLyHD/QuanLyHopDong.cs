using Microsoft.Data.Sqlite;
using QuanLyVayVon.CSDL;
using QuanLyVayVon.Models;
using System.Security.Cryptography.X509Certificates;
namespace QuanLyVayVon.QuanLyHD
{
    public partial class QuanLyHopDong : Form
    {

        // Khai báo biến toàn cục
        private int pageSize = 20;
        private string? lastCreatedAt = null;
        private string? firstCreatedAt = null;

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
            LoadTrangTiepTheo();
        }

        private void btn_Lui_Click(object sender, EventArgs e)
        {
            LoadTrangTruoc();
        }



        // Call this method in the QuanLyHopDong_Load event:
        private void QuanLyHopDong_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.icon_ico; // Assuming you have an icon in your resources

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
            Color baseColor = Color.Black;
            Color hoverColor = Color.FromArgb(50, 50, 50);
            Color textColor = Color.WhiteSmoke;

            btn.Text = text;
            btn.Font = new Font("Segoe UI", 14F, FontStyle.Bold); // Giảm nhẹ size để rõ hơn
            btn.Size = new Size(44, 44);
            btn.BackColor = baseColor;
            btn.ForeColor = textColor;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Cursor = Cursors.Hand;
            btn.TextAlign = ContentAlignment.MiddleCenter;
            btn.UseCompatibleTextRendering = true;
            btn.FlatAppearance.MouseDownBackColor = hoverColor;
            btn.FlatAppearance.MouseOverBackColor = hoverColor;

            // Tăng smoothness
            btn.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            };

            // Vẽ lại bo góc mỗi khi resize
            btn.Resize += (s, e) =>
            {
                btn.Region = Region.FromHrgn(
                    NativeMethods.CreateRoundRectRgn(0, 0, btn.Width, btn.Height, 10, 10)
                );
            };
            btn.Region = Region.FromHrgn(
                NativeMethods.CreateRoundRectRgn(0, 0, btn.Width, btn.Height, 10, 10)
            );

            // Hover effect
            btn.MouseEnter += (s, e) => btn.BackColor = hoverColor;
            btn.MouseLeave += (s, e) => btn.BackColor = baseColor;
        }


        void StyleComboBox(ComboBox cb)
        {
            cb.Font = mainFont;
            cb.ForeColor = Color.Black;
            cb.BackColor = Color.White;
            cb.Region = System.Drawing.Region.FromHrgn(
                NativeMethods.CreateRoundRectRgn(0, 0, cb.Width, cb.Height, 20, 20) // Bo nhiều hơn
            );
            // Thêm vào trong hàm StyleComboBox sau các thuộc tính khác
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
            cb.FlatStyle = FlatStyle.Flat; // Đặt kiểu phẳng

        }
        public QuanLyHopDong()
        {
            InitializeComponent();
            this.AutoScaleMode = AutoScaleMode.Font;

            this.Font = AppFont;


            StyleButton(btn_ThemHopDong);
            StyleButton(btn_MoCSDL);
            StyleButton(btn_chinhsua);
            StyleButton(btn_Lui);
            StyleButton(btn_Tien);
            StyleTextBox(tb_Search);
            StyleButton(btn_UpdateInfoSystem);
            StyleButton(btn_About);
            StyleButton(btn_Hide);
            StyleButton(btn_Search);
            StyleComboBox(cbBox_Search);
            StyleExitButton(btn_Thoat, "X");
            StyleExitButton(btn_Hide, "–");


            string iconPath_Home = Path.Combine(Application.StartupPath, "assets", "pictures", "home.png");
            btn_Home.BackgroundImage = Image.FromFile(iconPath_Home);
            btn_Home.BackgroundImageLayout = ImageLayout.Stretch;

            StyleButton(btn_Home);

            //btn.BackgroundImage = Image.FromFile(iconPath);
            InitDataGridView();
            this.FormBorderStyle = FormBorderStyle.None; // Loại bỏ viền để bo góc
            AutoLayoutControls();

        }

        // Pseudocode plan:
        // - Set DataGridView font to match button font (Segoe UI, 12F, Bold or Regular)
        // - Set header font to bold, larger size
        // - Set row font to regular, larger size
        // - Center-align all columns (header and cell)
        // - Set row height and header height for readability
        // - Set alternating row color for better readability
        // - Set selection color to match button hover
        // - Ensure text wraps if needed (for long content)
        // - Apply these in InitDataGridView()

        private void InitDataGridView()
        {
            this.WindowState = FormWindowState.Maximized;
            dataGridView_ThongTinHopDong.Dock = DockStyle.None;
            dataGridView_ThongTinHopDong.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            dataGridView_ThongTinHopDong.Left = 20;
            dataGridView_ThongTinHopDong.Width = this.ClientSize.Width - 40;
            dataGridView_ThongTinHopDong.Height = this.ClientSize.Height - dataGridView_ThongTinHopDong.Top - 20;
            //dataGridView_ThongTinHopDong.AllowUserToAddRows = false;

            // Tự động resize khi thay đổi kích thước form
            this.Resize += (s, ev) =>
            {
                dataGridView_ThongTinHopDong.Left = 20;
                dataGridView_ThongTinHopDong.Width = this.ClientSize.Width - 40;
                dataGridView_ThongTinHopDong.Height = this.ClientSize.Height - dataGridView_ThongTinHopDong.Top - 20;
            };

            // Font đồng bộ với button
            var cellFont = new Font("Segoe UI", 12F, FontStyle.Regular);
            var headerFont = new Font("Segoe UI", 13F, FontStyle.Bold);

            dataGridView_ThongTinHopDong.Font = cellFont;
            dataGridView_ThongTinHopDong.ColumnHeadersDefaultCellStyle.Font = headerFont;
            dataGridView_ThongTinHopDong.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView_ThongTinHopDong.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView_ThongTinHopDong.DefaultCellStyle.Font = cellFont;
            dataGridView_ThongTinHopDong.RowTemplate.Height = 38;
            dataGridView_ThongTinHopDong.ColumnHeadersHeight = 44;

            // Căn giữa header và cell cho tất cả các cột (kể cả khi cột được thêm động)
            dataGridView_ThongTinHopDong.ColumnAdded += (s, e) =>
            {
                e.Column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                e.Column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                e.Column.ReadOnly = true;
            };
            foreach (DataGridViewColumn col in dataGridView_ThongTinHopDong.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.ReadOnly = true;
            }

            // Màu nền, lưới, alternating row
            dataGridView_ThongTinHopDong.BackgroundColor = Color.White;
            dataGridView_ThongTinHopDong.GridColor = Color.LightGray;
            dataGridView_ThongTinHopDong.BorderStyle = BorderStyle.None;
            dataGridView_ThongTinHopDong.EnableHeadersVisualStyles = false;
            dataGridView_ThongTinHopDong.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            dataGridView_ThongTinHopDong.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView_ThongTinHopDong.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            dataGridView_ThongTinHopDong.AutoResizeColumnHeadersHeight();
            dataGridView_ThongTinHopDong.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;


            dataGridView_ThongTinHopDong.DefaultCellStyle.SelectionBackColor = Color.FromArgb(70, 130, 180);
            dataGridView_ThongTinHopDong.DefaultCellStyle.SelectionForeColor = Color.White;
            dataGridView_ThongTinHopDong.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(235, 240, 250);
            dataGridView_ThongTinHopDong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView_ThongTinHopDong.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dataGridView_ThongTinHopDong.AllowUserToResizeRows = false;
            dataGridView_ThongTinHopDong.RowHeadersWidth = 40;
            dataGridView_ThongTinHopDong.ScrollBars = ScrollBars.Both;

            // Tự động wrap text nếu cần
            dataGridView_ThongTinHopDong.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView_ThongTinHopDong.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;

            // Đặt toàn bộ DataGridView thành read-only để không chỉnh sửa được
            dataGridView_ThongTinHopDong.ReadOnly = true;

        }
        // Font đẹp hơn cho toàn bộ form (không in nghiêng)
        System.Drawing.Font mainFont = new System.Drawing.Font("Montserrat", 12.5F, FontStyle.Regular, GraphicsUnit.Point);
        System.Drawing.Font mainFontBold = new System.Drawing.Font("Montserrat", 13.5F, FontStyle.Bold, GraphicsUnit.Point);
        System.Drawing.Font donViFont = new System.Drawing.Font("Montserrat", 11F, FontStyle.Regular, GraphicsUnit.Point);
        System.Drawing.Font dateTimeFont = new System.Drawing.Font("Montserrat", 12.5F, FontStyle.Regular, GraphicsUnit.Point);
        void StyleTextBox(TextBox tb, bool boGoc = true)
        {
            tb.Font = mainFont;
            tb.ForeColor = Color.Black;
            tb.TextAlign = HorizontalAlignment.Center;
            tb.Multiline = false; // Để tự động scale chiều cao theo font
            tb.AutoSize = false;

            // Tính chiều cao phù hợp dựa trên font
            using (var g = tb.CreateGraphics())
            {
                SizeF textSize = g.MeasureString("Ag", tb.Font);
                int newHeight = (int)Math.Ceiling(textSize.Height) + 6; // +6 để cao hơn chữ một chút
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

        // Hàm style riêng cho từng button: tự động fit text, font vừa nút, khoảng cách đẹp giữa các nút
        public static void StyleButton(Button btn, bool boGoc = true)
        {
            // Đặt font mặc định lớn, đậm
            btn.Font = new Font("Segoe UI", 12F, FontStyle.Bold);

            // Tính toán kích thước button dựa trên text và font, tự động fit text
            using (Graphics g = btn.CreateGraphics())
            {
                SizeF textSize = g.MeasureString(btn.Text, btn.Font);
                int paddingWidth = 40; // padding trái/phải tổng cộng
                int paddingHeight = 20; // padding trên/dưới tổng cộng
                int minWidth = 120;
                int minHeight = 44;

                btn.Width = Math.Max((int)Math.Ceiling(textSize.Width) + paddingWidth, minWidth);
                btn.Height = Math.Max((int)Math.Ceiling(textSize.Height) + paddingHeight, minHeight);

                // Nếu text quá dài, giảm font cho vừa nút
                float maxFontSize = 12F;
                float minFontSize = 9F;
                float fontSize = maxFontSize;
                while (fontSize > minFontSize)
                {
                    using (Font testFont = new Font(btn.Font.FontFamily, fontSize, btn.Font.Style))
                    {
                        SizeF testSize = g.MeasureString(btn.Text, testFont);
                        if (testSize.Width + paddingWidth <= btn.Width)
                        {
                            btn.Font = new Font(btn.Font.FontFamily, fontSize, btn.Font.Style);
                            break;
                        }
                    }
                    fontSize -= 0.5F;
                }
            }

            // Style nút: bo góc, màu sắc, hiệu ứng hover, borderless
            btn.FlatStyle = FlatStyle.Flat;
            btn.BackColor = Color.SteelBlue;
            btn.ForeColor = Color.White;
            btn.FlatAppearance.BorderSize = 0;
            btn.Cursor = Cursors.Hand;
            btn.Padding = new Padding(0);
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(70, 130, 180);
            btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(40, 90, 140);
            btn.TextAlign = ContentAlignment.MiddleCenter;

            // Tùy chọn bo góc
            if (boGoc)
            {
                btn.Region = System.Drawing.Region.FromHrgn(
                    NativeMethods.CreateRoundRectRgn(0, 0, btn.Width, btn.Height, 18, 18));
            }
            else
            {
                btn.Region = null;
            }

            // Đảm bảo khoảng cách giữa các nút khi đặt trong container (ví dụ FlowLayoutPanel)
            btn.Margin = new Padding(16, 8, 16, 8); // trái, trên, phải, dưới

            // Ẩn chữ nếu nút không có nội dung text
            if (string.IsNullOrWhiteSpace(btn.Text))
            {
                btn.Text = "";
                btn.BackgroundImageLayout = ImageLayout.Zoom;
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
        private void AutoLayoutControls()
        {
            // Giả sử các nút nằm trong một FlowLayoutPanel tên là flowLayoutPanel_button
            // Nếu chưa có, bạn nên thêm FlowLayoutPanel vào form và đặt các nút vào đó
            if (dataGridView_ThongTinHopDong != null)
            {
                StyleFlowLayoutPanel(flowLayoutPanel_button);
                StyleFlowLayoutPanel(flowLayoutPanel_Thoat);
                StyleFlowLayoutPanel(flowLayoutPanel_Search);


            }

            // Đặt DataGridView phía dưới các nút, tự động co giãn
            int top = (flowLayoutPanel_button != null) ? flowLayoutPanel_button.Bottom + 10 : 20;
            int left = 20;
            int right = 20;
            int bottom = 20;

            dataGridView_ThongTinHopDong.Top = top;
            dataGridView_ThongTinHopDong.Left = left;
            dataGridView_ThongTinHopDong.Width = this.ClientSize.Width - left - right;
            dataGridView_ThongTinHopDong.Height = this.ClientSize.Height - top - bottom;

            // Đảm bảo DataGridView không che nút khi resize
            this.Resize += (s, e) =>
            {

                int newTop = (flowLayoutPanel_button != null) ? flowLayoutPanel_Search.Bottom + 10 : 20;
                dataGridView_ThongTinHopDong.Top = newTop;
                dataGridView_ThongTinHopDong.Left = left;
                dataGridView_ThongTinHopDong.Width = this.ClientSize.Width - left - right;
                dataGridView_ThongTinHopDong.Height = this.ClientSize.Height - newTop - bottom;
            };

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

            string MaHD = dataGridView_ThongTinHopDong.CurrentRow?.Cells["MaHD"].Value?.ToString();
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
            // Kết quả tổng lãi đến hôm nay
            decimal tongLai = 0;

            string dbPath = Path.Combine(Application.StartupPath, "DataBase", "data.db");
            using (var connection = new SqliteConnection($"Data Source={dbPath}"))
            {
                connection.Open();

                // 1. Lấy LaiMoiNgay từ bảng HopDongVay
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

                // 2. Lấy các kỳ đóng lãi của hợp đồng, sắp xếp theo NgayBatDauKy ASC
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

                    // Skip if already paid or just "sắp tới hạn"
                    if (ky.TinhTrang == 0 || ky.TinhTrang == 2)
                        continue;

                    DateTime end = now < ky.NgayDenHan ? now : ky.NgayDenHan;
                    if (end < ky.NgayBatDauKy)
                        continue;

                    int soNgay = (end - ky.NgayBatDauKy).Days;
                    decimal laiKy = soNgay * laiMoiNgay;

                    decimal conNo = laiKy - ky.SoTienDaDong;
                    if (conNo < 0) conNo = 0;

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
                string maHD = grid.Rows[e.RowIndex].Cells["MaHD"].Value?.ToString();
                string ghiChu = null;
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
                string maHD = grid.Rows[e.RowIndex].Cells["MaHD"].Value?.ToString();
                string lichSu = null;
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
                    var frm_XuatText = new TextToScreen(lichSu, "Lịch sử thay đổi hợp đồng mã: ", maHD );
                    frm_XuatText.Show();
                }
                return;
            }

            // Xử lý nút Thao tác (giữ nguyên logic cũ)
            if (grid.Columns[e.ColumnIndex].Name == "ThaoTac")
            {
                string maHD = grid.Rows[e.RowIndex].Cells["MaHD"].Value?.ToString();
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
                var LichSuDongLaiform = new LichSuDongLai(maHD);

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
    new TimKiemHopDongItem { ID = 1, FieldName = "MaHD", DisplayName = "Mã hợp đồng" },
    new TimKiemHopDongItem { ID = 2, FieldName = "TenKH", DisplayName = "Khách hàng" },
    new TimKiemHopDongItem { ID = 3, FieldName = "SDT", DisplayName = "Số điện thoại" },
    new TimKiemHopDongItem { ID = 4, FieldName = "CCCD", DisplayName = "Căn cước công dân" },
};

            cbBox_Search.DataSource = items;
            cbBox_Search.DisplayMember = "DisplayName";
            cbBox_Search.ValueMember = "FieldName"; // Giúp truy vấn dễ sau này
            cbBox_Search.SelectedIndex = 0;


        }

        public static void CapNhatTinhTrangMaHD(string maHD = null)
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
                AND NOT EXISTS (
                    SELECT 1 FROM LichSuDongLai
                    WHERE MaHD = HopDongVay.MaHD AND TinhTrang IN (1, 2, 3, 4)
                )
                {(string.IsNullOrEmpty(maHD) ? "" : "AND MaHD = @MaHD")};";
                    if (!string.IsNullOrEmpty(maHD)) command.Parameters.AddWithValue("@MaHD", maHD);
                    command.ExecuteNonQuery();

                    // Ưu tiên 0: Tất toán (tất cả kỳ = -1)
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
        }


        public static void CapNhatTinhTrangLichSuDongLai(string maHD = null)
        {
            string dbPath = Path.Combine(Application.StartupPath, "DataBase", "data.db");
            using (var connection = new SqliteConnection($"Data Source={dbPath}"))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    string maHDCondition = string.IsNullOrEmpty(maHD) ? "" : "AND MaHD = @MaHD";

                    command.CommandText = $@"
                -- 3: Quá hạn (chưa đóng đủ, đã quá hạn)
                UPDATE LichSuDongLai
                SET TinhTrang = 3, UpdatedAt = CURRENT_TIMESTAMP
                WHERE SoTienDaDong < SoTienPhaiDong
                  AND date('now') > date(NgayDenHan)
                  {maHDCondition};

                -- 4: Tới hạn hôm nay (chưa đóng đủ)
                UPDATE LichSuDongLai
                SET TinhTrang = 4, UpdatedAt = CURRENT_TIMESTAMP
                WHERE SoTienDaDong < SoTienPhaiDong
                  AND date(NgayDenHan) = date('now')
                  {maHDCondition};

                -- 2: Sắp tới hạn (<= 3 ngày nữa, chưa đóng đủ, không phải hôm nay)
                UPDATE LichSuDongLai
                SET TinhTrang = 2, UpdatedAt = CURRENT_TIMESTAMP
                WHERE SoTienDaDong < SoTienPhaiDong
                  AND date('now') < date(NgayDenHan)
                  AND julianday(NgayDenHan) - julianday('now') <= 3
                  AND date(NgayDenHan) != date('now')
                  {maHDCondition};

                -- 1: Đang vay (> 3 ngày tới hạn, chưa đóng đủ)
                UPDATE LichSuDongLai
                SET TinhTrang = 1, UpdatedAt = CURRENT_TIMESTAMP
                WHERE SoTienDaDong < SoTienPhaiDong
                  AND date('now') < date(NgayDenHan)
                  AND julianday(NgayDenHan) - julianday('now') > 3
                  {maHDCondition};

                -- 5: Tới hạn hôm nay và đã đóng đủ
                UPDATE LichSuDongLai
                SET TinhTrang = 5, UpdatedAt = CURRENT_TIMESTAMP
                WHERE SoTienDaDong >= SoTienPhaiDong
                  AND date(NgayDenHan) = date('now')
                  {maHDCondition};

                -- 0: Đã đóng đủ và không phải hôm nay
                UPDATE LichSuDongLai
                SET TinhTrang = 0, UpdatedAt = CURRENT_TIMESTAMP
                WHERE SoTienDaDong >= SoTienPhaiDong
                  AND date(NgayDenHan) != date('now')
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
            KhoiTaoPhanTrang(); // Tải lại dữ liệu hợp đồng
        }

        private void btn_About_Click(object sender, EventArgs e)
        {

        }

        private void btn_Hide_Click(object sender, EventArgs e)
        {

            this.WindowState = FormWindowState.Minimized; // Thu nhỏ form 

        }

    }
}
