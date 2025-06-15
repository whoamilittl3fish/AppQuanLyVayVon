using Microsoft.Data.Sqlite;
using QuanLyVayVon.CSDL;
namespace QuanLyVayVon.QuanLyHD
{
    public partial class QuanLyHopDong : Form
    {

        private int? lastIdCuoiTrang;

        // ... (other code)

        private void LoadMaHDToDataGridView()
        {
            string dbDir = Path.Combine(Application.StartupPath, "DataBase");
            string dbPath = Path.Combine(dbDir, "data.db");

            if (!Function_Reuse.KiemTraDatabaseTonTai())
            {
                CustomMessageBox.ShowCustomMessageBox("Cơ sở dữ liệu không tồn tại. Vui lòng tạo cơ sở dữ liệu trước.");
                return;
            }

            // Reset lại DataGridView
            dataGridView_ThongTinHopDong.Columns.Clear();
            dataGridView_ThongTinHopDong.Rows.Clear();

            // Thêm cột tiêu đề
            // Cấu hình tên cột (Name) và tiêu đề (HeaderText)
            var columnDefinitions = new (string Name, string HeaderText)[]
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
                dataGridView_ThongTinHopDong.Columns.Add(name, header);
            }


         

            // Thêm cột thao tác (nút)
            var actionColumn = new DataGridViewButtonColumn
            {
                Name = "ThaoTac",
                HeaderText = "Thao tác",
                Text = "Chi tiết",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            };
            dataGridView_ThongTinHopDong.Columns.Add(actionColumn);

            // Gắn lại sự kiện click nút chi tiết (sửa nullability cho đúng delegate)
            dataGridView_ThongTinHopDong.CellContentClick -= new DataGridViewCellEventHandler(DataGridView_ThongTinHopDong_CellContentClick);
            dataGridView_ThongTinHopDong.CellContentClick += new DataGridViewCellEventHandler(DataGridView_ThongTinHopDong_CellContentClick);

            // Lấy Id dòng hợp đồng mới tạo gần nhất
            int? idGanNhat = LayIdHopDongTaoGanNhat();
            if (idGanNhat == null)
            {
                CustomMessageBox.ShowCustomMessageBox("Không có hợp đồng nào trong cơ sở dữ liệu.");
                return;
            }

            // Lấy danh sách theo phân trang
            var danhSach = LayHopDongTheoTrangTheoId(idGanNhat.Value, 50);
            if (danhSach == null || danhSach.Count == 0)
            {
                CustomMessageBox.ShowCustomMessageBox("Không có hợp đồng nào để hiển thị.");
                return;
            }

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
                    "", // Lãi đến hôm nay: để sau tính
                    item.NgayDongLaiGanNhat,
                    item.TinhTrang == 0 ? "Đang vay" : "Đã tất toán"
                );
            }
        }


        


        public static int? GetIdFromMaHD(string maHD)
        {
            string dbDir = Path.Combine(Application.StartupPath, "DataBase");
            string dbPath = Path.Combine(dbDir, "data.db");

            if (!Function_Reuse.KiemTraDatabaseTonTai())
            {
                CustomMessageBox.ShowCustomMessageBox("Cơ sở dữ liệu không tồn tại. Vui lòng tạo cơ sở dữ liệu trước.");
                return null;

            }
            else
            {
                using (var connection = new SqliteConnection($"Data Source={dbPath}"))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = "SELECT Id FROM HopDongVay WHERE MaHD = @MaHD LIMIT 1";
                    command.Parameters.AddWithValue("@MaHD", maHD);

                    var result = command.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : (int?)null;
                }
            }
        }
        public static int? LayIdHopDongTaoGanNhat()
        {
            string dbPath = Path.Combine(Application.StartupPath, "DataBase", "data.db");

            if (!Function_Reuse.KiemTraDatabaseTonTai())
            {
                CustomMessageBox.ShowCustomMessageBox("Cơ sở dữ liệu không tồn tại. Vui lòng tạo cơ sở dữ liệu trước.");
                return null;
            }

            using (var connection = new SqliteConnection($"Data Source={dbPath}"))
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = @"
            SELECT Id
            FROM HopDongVay
            ORDER BY datetime(CreatedAt) DESC, Id DESC
            LIMIT 1";

                var result = command.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : (int?)null;
            }
        }


        public static List<HopDongModel> LayHopDongTheoTrangTheoId(int? lastId = null, int pageSize = 50)
        {
            var ds = new List<HopDongModel>();
            string dbPath = Path.Combine(Application.StartupPath, "DataBase", "data.db");

            using (var connection = new SqliteConnection($"Data Source={dbPath}"))
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = @"
    SELECT Id, MaHD, TenKH, TenTaiSan, TienVay, NgayVay,
           TienLaiDaDong, TongLai, NgayDongLaiGanNhat AS NgayPhaiDongLai,
           TinhTrang, CreatedAt
    FROM HopDongVay
    WHERE (@LastId IS NULL OR Id <= @LastId)
    ORDER BY datetime(CreatedAt) DESC
    LIMIT @PageSize";


                command.Parameters.AddWithValue("@LastId", (object?)lastId ?? DBNull.Value);
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
                            NgayDongLaiGanNhat = reader["NgayPhaiDongLai"]?.ToString(),
                            TinhTrang = Convert.ToInt32(reader["TinhTrang"] ?? 0),
                            CreatedAt = reader["CreatedAt"]?.ToString()
                        });
                    }
                }
            }

            return ds;
        }





        // Call this method in the QuanLyHopDong_Load event:
        private void QuanLyHopDong_Load(object sender, EventArgs e)
        {
            LoadMaHDToDataGridView();

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
                int newTop = (flowLayoutPanel_button != null) ? flowLayoutPanel_button.Bottom + 10 : 20;
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
                LoadMaHDToDataGridView(); // Chỉ load khi lưu thành công
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
                    CapNhatCurrentRow(hopDong); // Chỉ cập nhật lại dòng hiện tại
                }
            }

        }
        private void CapNhatCurrentRow(HopDongModel hopDong)
        {
            var row = dataGridView_ThongTinHopDong.CurrentRow;
            if (row == null) return;

            row.Cells["TenKH"].Value = hopDong.TenKH;
            row.Cells["TenTaiSan"].Value = hopDong.TenTaiSan;
            row.Cells["TienVay"].Value = Function_Reuse.FormatNumberWithThousandsSeparator(hopDong.TienVay);
            row.Cells["NgayVay"].Value = hopDong.NgayVay;
            row.Cells["LaiDaDong"].Value = Function_Reuse.FormatNumberWithThousandsSeparator(hopDong.TienLaiDaDong ?? 0);

            decimal tongLai = 0;
            decimal.TryParse(hopDong.TongLai?.ToString(), out tongLai);
            decimal tienNo = tongLai - (hopDong.TienLaiDaDong ?? 0);
            row.Cells["TienNo"].Value = Function_Reuse.FormatNumberWithThousandsSeparator(tienNo);

            row.Cells["NgayPhaiDongLai"].Value = hopDong.NgayDongLaiGanNhat;
            row.Cells["TinhTrang"].Value = hopDong.TinhTrang == 0 ? "Đang vay" : "Đã tất toán";
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
                if (LichSuDongLaiform.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Cập nhật thành công!");
                    var hopDong = HopDongForm.GetHopDongByMaHD(maHD);
                    if (hopDong != null)
                    {
                        CapNhatCurrentRow(hopDong); // Chỉ cập nhật lại dòng hiện tại
                    }
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void thêmToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        List<HopDongModel> LayDanhSachHopDong()
        {
            var ds = new List<HopDongModel>();
            string dbPath = Path.Combine(Application.StartupPath, "DataBase", "data.db");

            using (var connection = new SqliteConnection($"Data Source={dbPath}"))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"
            SELECT 
                MaHD, TenKH, TenTaiSan, TienVay, NgayVay,
                TienLaiDaDong, TongLai,
                NgayDongLaiGanNhat AS NgayPhaiDongLai,
                TinhTrang
            FROM HopDongVay
            ORDER BY MaHD;
        ";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ds.Add(new HopDongModel
                        {
                            MaHD = reader["MaHD"]?.ToString(),
                            TenKH = reader["TenKH"]?.ToString(),
                            TenTaiSan = reader["TenTaiSan"]?.ToString(),
                            TienVay = Convert.ToDecimal(reader["TienVay"] ?? 0),
                            NgayVay = reader["NgayVay"]?.ToString(),
                            TienLaiDaDong = Convert.ToDecimal(reader["TienLaiDaDong"] ?? 0),
                            TongLai = Convert.ToDecimal(reader["TongLai"] ?? 0),
                            NgayDongLaiGanNhat = reader["NgayPhaiDongLai"]?.ToString(),
                            TinhTrang = Convert.ToInt32(reader["TinhTrang"] ?? 0)
                        });
                    }
                }
            }

            return ds;
        }

        private void btn_Tien_Click(object sender, EventArgs e)
        {
            //LoadNextPage();
        }
    }


}
