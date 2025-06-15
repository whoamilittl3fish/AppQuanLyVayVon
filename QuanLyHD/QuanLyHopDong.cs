using Microsoft.Data.Sqlite;
using QuanLyVayVon.CSDL;
using QuanLyVayVon.Models;
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

        private void HienThiHopDong(List<HopDongModel> danhSach)
        {
            if (danhSach == null || danhSach.Count == 0)
            {
                CustomMessageBox.ShowCustomMessageBox("Không có hợp đồng nào.");
                return;
            }

            dataGridView_ThongTinHopDong.Columns.Clear();
            dataGridView_ThongTinHopDong.Rows.Clear();

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

            var actionColumn = new DataGridViewButtonColumn
            {
                Name = "ThaoTac",
                HeaderText = "Thao tác",
                Text = "Chi tiết",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            };
            dataGridView_ThongTinHopDong.Columns.Add(actionColumn);

            dataGridView_ThongTinHopDong.CellContentClick -= DataGridView_ThongTinHopDong_CellContentClick;
            dataGridView_ThongTinHopDong.CellContentClick += DataGridView_ThongTinHopDong_CellContentClick;

            foreach (var item in danhSach)
            {
                string tienVay = Function_Reuse.FormatNumberWithThousandsSeparator(item.TienVay);
                string laiDaDong = Function_Reuse.FormatNumberWithThousandsSeparator(item.TienLaiDaDong ?? 0);
                string tongLai = Function_Reuse.FormatNumberWithThousandsSeparator(item.TongLai ?? 0);
                string tienNo = Function_Reuse.FormatNumberWithThousandsSeparator((item.TongLai ?? 0) - (item.TienLaiDaDong ?? 0));

                dataGridView_ThongTinHopDong.Rows.Add(
                    item.MaHD,
                    item.TenKH,
                    item.TenTaiSan,
                    tienVay,
                    item.NgayVay,
                    laiDaDong,
                    tienNo,
                    "",
                    item.NgayDongLaiGanNhat,
                    item.TinhTrang == 0 ? "Đang vay" : "Đã tất toán"
                );
            }


            // Cập nhật thời điểm
            firstCreatedAt = danhSach.First().CreatedAt;
            lastCreatedAt = danhSach.Last().CreatedAt;

            // Kiểm tra xem có còn dữ liệu phía trước (newer) hay phía sau (older)
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

            string dbPath = Path.Combine(Application.StartupPath, "Database", "data.db");

            if (!File.Exists(dbPath))
            {

                this.Hide();
                if (CustomMessageBox.ShowCustomYesNoMessageBox("Không tìm thấy cơ sở dữ liệu. Bạn có muốn nhập mật khẩu để mở cơ sở dữ liệu?", null, null, default, "LỖI CƠ SỞ DỮ LIỆU") == DialogResult.Yes)
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
                KhoiTaoPhanTrang();
                InitCbBoxSearch();
            }

        }
        // Màu nền và font mặc định cho ứng dụng

        private static readonly Font AppFont = new Font("Segoe UI", 11F, FontStyle.Regular);

        public QuanLyHopDong()
        {
            InitializeComponent();
            this.AutoScaleMode = AutoScaleMode.Font;

            this.Font = AppFont;

            StyleButton(btn_Thoat);
            StyleButton(btn_ThemHopDong);
            StyleButton(btn_MoCSDL);
            StyleButton(btn_chinhsua);
            StyleButton(btn_Lui);
            StyleButton(btn_Tien);
            StyleTextBox(tb_Search);

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

            dataGridView_ThongTinHopDong.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
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
        void StyleTextBox(TextBox tb)
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
            tb.Region = System.Drawing.Region.FromHrgn(
                NativeMethods.CreateRoundRectRgn(0, 0, tb.Width, tb.Height, 20, 20)
            );
        }
        // Hàm style riêng cho từng button: tự động fit text, font vừa nút, khoảng cách đẹp giữa các nút
        private void StyleButton(Button btn)
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
            // Bo góc cho button
            btn.Region = System.Drawing.Region.FromHrgn(
                NativeMethods.CreateRoundRectRgn(0, 0, btn.Width, btn.Height, 18, 18));

            // Đảm bảo khoảng cách giữa các nút khi đặt trong container (ví dụ FlowLayoutPanel)
            // Nếu dùng FlowLayoutPanel thì dùng Margin để tạo khoảng cách giữa các nút
            btn.Margin = new Padding(16, 8, 16, 8); // trái, trên, phải, dướix



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
                int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);
        }

        // Sự kiện đóng ứng dụng
        private void btnClose_Click(object sender, EventArgs e)
        {
            Function_Reuse.ConfirmAndClose_App();

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


        public HopDongModel LayThongTinTuRow(DataGridViewRow row)
        {
            return new HopDongModel
            {
                MaHD = row.Cells["MaHD"].Value?.ToString(),
                TenKH = row.Cells["TenKH"].Value?.ToString(),
                SDT = row.Cells["SDT"].Value?.ToString(),
                CCCD = row.Cells["CCCD"].Value?.ToString(),
                DiaChi = row.Cells["DiaChi"].Value?.ToString(),

                TienVay = Convert.ToDecimal(row.Cells["TienVay"].Value ?? 0),
                HinhThucLaiID = Convert.ToInt32(row.Cells["HinhThucLaiID"].Value ?? 0),
                SoNgayVay = Convert.ToInt32(row.Cells["SoNgayVay"].Value ?? 0),
                KyDongLai = Convert.ToInt32(row.Cells["KyDongLai"].Value ?? 0),
                NgayVay = row.Cells["NgayVay"].Value?.ToString(),
                NgayHetHan = row.Cells["NgayHetHan"].Value?.ToString(),
                NgayDongLaiGanNhat = row.Cells["NgayDongLaiGanNhat"].Value?.ToString(),
                TinhTrang = Convert.ToInt32(row.Cells["TinhTrang"].Value ?? 0),

                Lai = Convert.ToDecimal(row.Cells["Lai"].Value ?? 0),
                SoTienLaiMoiKy = Convert.ToDecimal(row.Cells["SoTienLaiMoiKy"].Value ?? 0),
                SoTienLaiCuoiKy = Convert.ToDecimal(row.Cells["SoTienLaiCuoiKy"].Value ?? 0),
                TienLaiDaDong = Convert.ToDecimal(row.Cells["TienLaiDaDong"].Value ?? 0),
                TongLai = Convert.ToDecimal(row.Cells["TongLai"].Value ?? 0),

                TenTaiSan = row.Cells["TenTaiSan"].Value?.ToString(),
                LoaiTaiSanID = Convert.ToInt32(row.Cells["LoaiTaiSanID"].Value ?? 0),
                ThongTinTaiSan1 = row.Cells["ThongTinTaiSan1"].Value?.ToString(),
                ThongTinTaiSan2 = row.Cells["ThongTinTaiSan2"].Value?.ToString(),
                ThongTinTaiSan3 = row.Cells["ThongTinTaiSan3"].Value?.ToString(),

                NVThuTien = row.Cells["NVThuTien"].Value?.ToString(),
                GhiChu = row.Cells["GhiChu"].Value?.ToString(),

                CreatedAt = row.Cells["CreatedAt"].Value?.ToString(),
                UpdatedAt = row.Cells["UpdatedAt"].Value?.ToString()
            };
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
                var hopDong = HopDongForm.GetHopDongByMaHD(MaHD);
                if (hopDong != null)
                {
                    CapNhatDongTheoMaHD(hopDong); // Chỉ cập nhật lại dòng hiện tại
                }
            }

        }

        private void CapNhatDongTheoMaHD(HopDongModel hopDong)
        {
            foreach (DataGridViewRow row in dataGridView_ThongTinHopDong.Rows)
            {
                if (row.Cells["MaHD"].Value?.ToString() == hopDong.MaHD)
                {
                    row.Cells["TenKH"].Value = hopDong.TenKH;
                    row.Cells["TenTaiSan"].Value = hopDong.TenTaiSan;
                    row.Cells["TienVay"].Value = Function_Reuse.FormatNumberWithThousandsSeparator(hopDong.TienVay);
                    row.Cells["NgayVay"].Value = hopDong.NgayVay;
                    row.Cells["LaiDaDong"].Value = Function_Reuse.FormatNumberWithThousandsSeparator(hopDong.TienLaiDaDong ?? 0);

                    decimal tongLai = hopDong.TongLai ?? 0;
                    decimal tienLaiDaDong = hopDong.TienLaiDaDong ?? 0;
                    decimal tienNo = tongLai - tienLaiDaDong;
                    row.Cells["TienNo"].Value = Function_Reuse.FormatNumberWithThousandsSeparator(tienNo);

                    row.Cells["NgayPhaiDongLai"].Value = hopDong.NgayDongLaiGanNhat;
                    row.Cells["TinhTrang"].Value = hopDong.TinhTrang == 0 ? "Đang vay" : "Đã tất toán";
                    break; // Tìm thấy là thoát
                }
            }
        }




        // Thêm phương thức xử lý sự kiện vào class QuanLyHopDong:
        private void DataGridView_ThongTinHopDong_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridView_ThongTinHopDong.Columns[e.ColumnIndex].Name == "ThaoTac")
            {
                string maHD = dataGridView_ThongTinHopDong.Rows[e.RowIndex].Cells["MaHD"].Value?.ToString();
                if (maHD == null || maHD == string.Empty)
                {
                    CustomMessageBox.ShowCustomMessageBox("Vui lòng chọn một hợp đồng để xem chi tiết.");
                    return;
                }
                // Xử lý mở form chi tiết hoặc thao tác khác với maHD
                CustomMessageBox.ShowCustomMessageBox($"Bạn đã chọn hợp đồng: {maHD}");
                // Có thể mở form chi tiết hợp đồng ở đây
                // Nếu form đã mở, show lên (tùy bạn có muốn cho mở nhiều hay không)
                if (Application.OpenForms.OfType<LichSuDongLai>().Any())
                {
                    Application.OpenForms.OfType<LichSuDongLai>().First().BringToFront();
                    return;
                }
                var LichSuDongLaiform = new LichSuDongLai(maHD);

                // Sử dụng ShowDialog để chờ người dùng bấm Lưu
                if (LichSuDongLaiform.ShowDialog() == DialogResult.Yes)
                {
                    MessageBox.Show("Cập nhật thành công!");
                    var hopDong = HopDongForm.GetHopDongByMaHD(maHD);
                    if (hopDong != null)
                    {
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
        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void thêmToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        

        private void cbBox_Search_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
