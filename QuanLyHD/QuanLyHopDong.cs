using Microsoft.Data.Sqlite;

namespace QuanLyVayVon.QuanLyHD
{
    public partial class QuanLyHopDong : Form
    {
        // Update LoadMaHDToDataGridView to also load TenKH
        private void LoadMaHDToDataGridView()
        {
            string dbDir = Path.Combine(Application.StartupPath, "DataBase");
            string dbPath = Path.Combine(dbDir, "data.db");

            if (!Function_Reuse.KiemTraDatabaseTonTai())
            {
                CustomMessageBox.ShowCustomMessageBox("Cơ sở dữ liệu không tồn tại. Vui lòng tạo cơ sở dữ liệu trước.");
                return;
            }

            // Xóa toàn bộ cột và dòng cũ
            dataGridView_ThongTinHopDong.Columns.Clear();
            dataGridView_ThongTinHopDong.Rows.Clear();

            // Tạo các cột cần hiển thị
            dataGridView_ThongTinHopDong.Columns.Add("MaHD", "Mã HĐ");
            dataGridView_ThongTinHopDong.Columns.Add("TenKH", "Khách Hàng");
            dataGridView_ThongTinHopDong.Columns.Add("TenTaiSan", "Tài sản");
            dataGridView_ThongTinHopDong.Columns.Add("TienVay", "Tiền vay");
            dataGridView_ThongTinHopDong.Columns.Add("NgayVay", "Ngày vay");
            dataGridView_ThongTinHopDong.Columns.Add("LaiDaDong", "Lãi đã đóng");
            dataGridView_ThongTinHopDong.Columns.Add("TienNo", "Tiền nợ");
            dataGridView_ThongTinHopDong.Columns.Add("LaiDenHomNay", "Lãi đến hôm nay");
            dataGridView_ThongTinHopDong.Columns.Add("NgayPhaiDongLai", "Ngày phải đóng lãi");
            // Sau khi tạo các cột cần hiển thị, thêm đoạn này để fix cột MaHD vừa với tiêu đề và hàng
            dataGridView_ThongTinHopDong.Columns["MaHD"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView_ThongTinHopDong.Columns["MaHD"].MinimumWidth = 60;



            // Thêm cột button "Thao tác" vào cuối DataGridView
            var actionButtonColumn = new DataGridViewButtonColumn
            {
                Name = "ThaoTac",
                HeaderText = "Thao tác",
                Text = "Chi tiết",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            };
            dataGridView_ThongTinHopDong.Columns.Add(actionButtonColumn);

            // Đăng ký sự kiện CellContentClick để xử lý khi bấm nút
            dataGridView_ThongTinHopDong.CellContentClick -= DataGridView_ThongTinHopDong_CellContentClick;
            dataGridView_ThongTinHopDong.CellContentClick += DataGridView_ThongTinHopDong_CellContentClick;


            using (var connection = new SqliteConnection($"Data Source={dbPath}"))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"
            SELECT 
                MaHD, 
                TenKH,
                TenTaiSan,
                TienVay,
                NgayVay,
                TienLaiDaDong AS LaiDaDong,
                TienNo,
                '' AS LaiDenHomNay, -- chưa tính nên để trống
                NgayDongLaiGanNhat AS NgayPhaiDongLai,
                TinhTrang
            FROM HopDongVay
            ORDER BY MaHD;
        ";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string maHD = reader["MaHD"]?.ToString() ?? "";
                        string tenKH = reader["TenKH"]?.ToString() ?? "";
                        string tenTS = reader["TenTaiSan"]?.ToString() ?? "";
                        string tienVay = reader["TienVay"]?.ToString() ?? "";
                        string ngayVay = reader["NgayVay"]?.ToString() ?? "";
                        string laiDaDong = reader["LaiDaDong"]?.ToString() ?? "";
                        string tienNo = reader["TienNo"]?.ToString() ?? "";
                        string laiDenHomNay = reader["LaiDenHomNay"]?.ToString() ?? "";
                        string ngayPhaiDongLai = reader["NgayPhaiDongLai"]?.ToString() ?? "";
                        string tinhTrang = Convert.ToInt32(reader["TinhTrang"]) == 0 ? "Đang vay" : "Đã tất toán";

                        dataGridView_ThongTinHopDong.Rows.Add(
                            maHD,
                            tenKH,
                            tenTS,
                            tienVay,
                            ngayVay,
                            laiDaDong,
                            tienNo,
                            laiDenHomNay,
                            ngayPhaiDongLai,
                            tinhTrang
                        );
                    }
                }
            }
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string MaHD = dataGridView_ThongTinHopDong.CurrentRow?.Cells["MaHD"].Value?.ToString();
            if (MaHD == null)
            {
                CustomMessageBox.ShowCustomMessageBox("Vui lòng chọn một hợp đồng để chỉnh sửa.");
                return;
            }
            if (Application.OpenForms.OfType<HopDongForm>().Any())
            {
                Application.OpenForms.OfType<HopDongForm>().First().Show();
                return;
            }
            var hopDongForm = new HopDongForm(MaHD, false);
            if (hopDongForm.ShowDialog() == DialogResult.OK)
            {
                LoadMaHDToDataGridView(); // Chỉ load khi lưu thành công
            }
        }

        // Thêm phương thức xử lý sự kiện vào class QuanLyHopDong:
        private void DataGridView_ThongTinHopDong_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridView_ThongTinHopDong.Columns[e.ColumnIndex].Name == "ThaoTac")
            {
                string maHD = dataGridView_ThongTinHopDong.Rows[e.RowIndex].Cells["MaHD"].Value?.ToString();
                // Xử lý mở form chi tiết hoặc thao tác khác với maHD
                CustomMessageBox.ShowCustomMessageBox($"Bạn đã chọn hợp đồng: {maHD}");
                // Có thể mở form chi tiết hợp đồng ở đây
                if (Application.OpenForms.OfType<LichSuDongLai>().Any())
                {
                    Application.OpenForms.OfType<LichSuDongLai>().First().Show();
                    return;
                }
                var LichSuDongLaiform = new LichSuDongLai(maHD);
                LichSuDongLaiform.Show();
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void thêmToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

    }


}
