using Microsoft.Data.Sqlite;

namespace QuanLyVayVon.CSDL
{
    // Pseudocode plan:
    // 1. Set form properties for a modern look (background color, font, size).
    // 2. Style all buttons: flat style, custom color, rounded corners.
    // 3. Add a label with a title and custom font.
    // 4. Use correct button names as defined in Designer.
    // 5. Ensure event handlers and controls match Designer.

    public partial class QuanLyCSDL : Form
    {
        // ==== Cấu hình giao diện ====
        private static readonly Color AppBackColor = Color.FromArgb(245, 245, 250);
        private static readonly Font AppFont = new Font("Segoe UI", 11F, FontStyle.Regular);

        public QuanLyCSDL()
        {
            InitializeComponent();
            this.BackColor = AppBackColor;
            this.Font = AppFont;
            CustomizeUI();
        }

        // ==== Hàm tùy chỉnh giao diện ====
        private void CustomizeUI()
        {
            // Thuộc tính form
            this.Text = "Quản Lý Cơ Sở Dữ Liệu";
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.ClientSize = new Size(320, 310);
            this.FormBorderStyle = FormBorderStyle.None; // Remove border for rounded corners

            // Tiêu đề
            var titleLabel = new Label
            {
                Text = "Tạo Cơ Sở Dữ Liệu Vay Vốn",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.FromArgb(44, 62, 80),
                AutoSize = true,
                Location = new Point(30, 10)
            };
            this.Controls.Add(titleLabel);

            // Hàm style cho button
            void StyleButton(Button btn, string text, int top)
            {
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.BackColor = Color.FromArgb(52, 152, 219);
                btn.ForeColor = Color.White;
                btn.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
                btn.Size = new Size(260, 50);
                btn.Location = new Point(30, top);
                btn.Text = text;
                btn.Cursor = Cursors.Hand;
                btn.Region = System.Drawing.Region.FromHrgn(
                    NativeMethods.CreateRoundRectRgn(0, 0, btn.Width, btn.Height, 20, 20)
                );
            }

            // Gán style cho các button
            StyleButton(btn_TaoCSDL, "Tạo cơ sở dữ liệu", 180);
            StyleButton(btn_SaoLuu, "Sao lưu", 60);
            StyleButton(btn_UploadSaoluu, "Tải lên sao lưu có sẵn", 120);
            StyleButton(btn_QuayLai, "Quay lại", 240);
        }

        // ==== Native method bo góc button ====
        private static class NativeMethods
        {
            [System.Runtime.InteropServices.DllImport("gdi32.dll", SetLastError = true)]
            public static extern IntPtr CreateRoundRectRgn(
                int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);
        }

        // ==== Xử lý sự kiện ====
        private void btn_TaoCSDL_Click(object sender, EventArgs e)
        {
            try
            {
                // Đặt data.db vào thư mục DataBase trong thư mục ứng dụng
                string dbDir = Path.Combine(Application.StartupPath, "DataBase");
                if (!Directory.Exists(dbDir))
                    Directory.CreateDirectory(dbDir);
                string dbPath = Path.Combine(dbDir, "data.db");

                if (!File.Exists(dbPath))
                {
                    using (var connection = new SqliteConnection($"Data Source={dbPath}"))
                    {
                        connection.Open();

                        var command = connection.CreateCommand();

                        command.CommandText = @"
                   CREATE TABLE IF NOT EXISTS HopDongVay (
    MaHD TEXT PRIMARY KEY,
    TenKH TEXT NOT NULL,
    SDT TEXT,
    CCCD TEXT,
    TienVay REAL,
    LaiTien REAL,            -- Lãi tiền cố định (VNĐ theo đơn vị thời gian)
    LaiPhanTram REAL,        -- Lãi suất % theo đơn vị thời gian
    HinhThucLai TEXT,        -- ""tienmat"" hoặc ""phantram""
    DonViLai TEXT,           -- ""ngay"", ""tuan"", ""thang""
    SoNgayVay INTEGER,
    KyDongLaiNgay INTEGER,
    NgayVay TEXT,
    NgayHetHan TEXT,
    NgayDongLai TEXT,
    TinhTrang INTEGER DEFAULT 0,
    CreatedAt TEXT DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt TEXT
);

";
                        command.ExecuteNonQuery();

                        connection.Close();
                    }

                    MessageBox.Show("Tạo cơ sở dữ liệu thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Cơ sở dữ liệu đã tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_SaoLuu_Click(object sender, EventArgs e)
        {
            // Sửa: Chỉ gọi BackupDatabase() một lần và kiểm tra kết quả trả về
            if (CSDL_BackupFunc.BackupDatabase())
                MessageBox.Show("Sao lưu thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Sao lưu thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btn_UploadSaoluu_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "SQLite Database (*.db)|*.db";
            openDialog.Title = "Chọn file sao lưu để khôi phục";

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                var result = MessageBox.Show("Bạn có chắc muốn ghi đè cơ sở dữ liệu hiện tại?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    if (CSDL_BackupFunc.RestoreDatabase(openDialog.FileName))
                        MessageBox.Show("Khôi phục thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Khôi phục thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btn_QuayLai_Click(object sender, EventArgs e)
        {
            Function_Reuse.ShowFormIfNotOpen<TrangChu>();
        }

        private void QuanLyCSDL_Load(object sender, EventArgs e)
        {
        }

        private void QuanLyCSDL_FormClosing(object sender, FormClosingEventArgs e)
        { 
        }
        public enum LoaiTaiSan
        {
            XeMay = 1,
            OTo = 2,
            DienThoai = 3,
            Laptop = 4,
            Vang = 5,
            Cavet = 6,
            SoDo = 7,
            Khac = 8
        }

    }
}
