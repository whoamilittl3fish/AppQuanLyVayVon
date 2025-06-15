using Microsoft.Data.Sqlite;
using System.Text;

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
CREATE TABLE IF NOT EXISTS CauHinhHeThong (
    TenBang TEXT PRIMARY KEY,
    Version INTEGER DEFAULT 1,
    NgayCapNhat TEXT DEFAULT CURRENT_TIMESTAMP
);

                   CREATE TABLE IF NOT EXISTS HopDongVay (
    MaHD TEXT PRIMARY KEY,              -- Mã hợp đồng
    TenKH TEXT NOT NULL,                -- Tên khách hàng
    SDT TEXT,                           -- Số điện thoại
    CCCD TEXT,                          -- CCCD / hộ chiếu
    DiaChi TEXT,                        -- Địa chỉ khách hàng
    TienVay REAL,                       -- Tổng tiền vay
    HinhThucLaiID INTEGER,                -- Index 1 tới 6 tương ứng với cách tính lãi
    SoNgayVay INTEGER,                  -- Tổng số ngày vay
    KyDongLai INTEGER,                  -- Kỳ đóng lãi (thời gian mỗi đợt đóng lãi)
    NgayVay TEXT,                       -- Ngày bắt đầu vay
    NgayHetHan TEXT,                    -- Ngày kết thúc
    NgayDongLaiGanNhat TEXT,
    TinhTrang INTEGER DEFAULT 0,        -- 0: Đang vay, 1: Đã tất toán 
    
    Lai Real DEFAULT 0,           
    SoTienLaiMoiKy REAL,                -- Tiền lãi mỗi kỳ (các kỳ đầu, chưa gồm kỳ cuối)
    SoTienLaiCuoiKy REAL,               -- Tiền lãi kỳ cuối
    TienLaiDaDong REAL DEFAULT 0,       -- Tổng tiền lãi đã đóng (tính đến thời điểm hiện tại)
    TienNo REAL DEFAULT 0,              -- Tổng tiền nợ (tiền vay + tiền lãi chưa đóng)
    TongLai REAL DEFAULT 0,            -- Tổng tiền lãi (khi đóng full)

    TenTaiSan TEXT,                     -- Tên tài sản
    LoaiTaiSanID INTEGER,               -- ID loại tài sản từ ComboBox
    ThongTinTaiSan1 TEXT,               -- Thông tin tài sản 1
    ThongTinTaiSan2 TEXT,               -- Thông tin tài sản 2
    ThongTinTaiSan3 TEXT,               -- Thông tin tài sản 3
    NVThuTien TEXT,                     -- Nhân viên thu tiền
    GhiChu TEXT,                        -- Ghi chú

    CreatedAt TEXT DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt TEXT
);

-- Các chỉ mục hỗ trợ tìm kiếm nhanh
CREATE INDEX IF NOT EXISTS idx_TenKH ON HopDongVay (TenKH);
CREATE INDEX IF NOT EXISTS idx_CCCD ON HopDongVay (CCCD);
CREATE INDEX IF NOT EXISTS idx_TenKH_CCCD ON HopDongVay (TenKH, CCCD);


CREATE TABLE IF NOT EXISTS LichSuDongLai (
    ID INTEGER PRIMARY KEY AUTOINCREMENT,
    MaHD TEXT NOT NULL,                    -- Mã hợp đồng liên kết
    KyThu INTEGER NOT NULL,                -- Số kỳ (1, 2, 3, ...)
    NgayBatDauKy TEXT NOT NULL,          -- Ngày bắt đầu kỳ
    NgayDenHan TEXT NOT NULL,              -- Ngày đến hạn đóng
    NgayDongThucTe TEXT,                   -- Ngày thực tế khách đóng
    SoTienPhaiDong REAL NOT NULL,          -- Số tiền phải đóng kỳ đó
    SoTienDaDong REAL DEFAULT 0,           -- Số tiền đã đóng
    SoTienNo REAL DEFAULT 0,                -- Số tiền đã đóng
    TinhTrang INTEGER DEFAULT 0,        -- 0: Đang vay, 1: Đã đóng lãi
    GhiChu TEXT,                           -- Ghi chú thêm nếu có

    CreatedAt TEXT DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt TEXT,

    FOREIGN KEY (MaHD) REFERENCES HopDongVay(MaHD)
);

CREATE TRIGGER IF NOT EXISTS trg_update_SoTienNo_when_SoTienDaDong_changes
AFTER UPDATE OF SoTienDaDong ON LichSuDongLai
FOR EACH ROW
WHEN NEW.SoTienNo != (NEW.SoTienPhaiDong - NEW.SoTienDaDong)
BEGIN
    UPDATE LichSuDongLai
    SET SoTienNo = NEW.SoTienPhaiDong - NEW.SoTienDaDong,
        UpdatedAt = CURRENT_TIMESTAMP
    WHERE ID = NEW.ID;
END;

-- Trigger cập nhật SoTienDaDong khi SoTienNo thay đổi
CREATE TRIGGER IF NOT EXISTS trg_update_SoTienDaDong_when_SoTienNo_changes
AFTER UPDATE OF SoTienNo ON LichSuDongLai
FOR EACH ROW
WHEN NEW.SoTienDaDong != (NEW.SoTienPhaiDong - NEW.SoTienNo)
BEGIN
    UPDATE LichSuDongLai
    SET SoTienDaDong = NEW.SoTienPhaiDong - NEW.SoTienNo,
        UpdatedAt = CURRENT_TIMESTAMP
    WHERE ID = NEW.ID;
END;

-- Trigger cập nhật SoTienNo khi SoTienPhaiDong thay đổi
CREATE TRIGGER IF NOT EXISTS trg_update_SoTienNo_when_SoTienPhaiDong_changes
AFTER UPDATE OF SoTienPhaiDong ON LichSuDongLai
FOR EACH ROW
WHEN NEW.SoTienNo != (NEW.SoTienPhaiDong - NEW.SoTienDaDong)
BEGIN
    UPDATE LichSuDongLai
    SET SoTienNo = NEW.SoTienPhaiDong - NEW.SoTienDaDong,
        UpdatedAt = CURRENT_TIMESTAMP
    WHERE ID = NEW.ID;
END;

-- Trigger mặc định: tính SoTienNo ngay khi thêm mới
CREATE TRIGGER IF NOT EXISTS trg_set_SoTienNo_after_insert
AFTER INSERT ON LichSuDongLai
FOR EACH ROW
WHEN NEW.SoTienNo = 0
BEGIN
    UPDATE LichSuDongLai
    SET SoTienNo = NEW.SoTienPhaiDong - NEW.SoTienDaDong,
        UpdatedAt = CURRENT_TIMESTAMP
    WHERE ID = NEW.ID;
END;

";
                        command.ExecuteNonQuery();

                        connection.Close();
                    }

                    MessageBox.Show("Tạo cơ sở dữ liệu thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    //Luu lai cấu hình hệ thống
                    DumpSqlStructure();
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

        // dump structure of the database to a text file
        private void DumpSqlStructure()
        {
            try
            {
                string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DataBase", "data.db");
                string dumpPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Debug", "dump_SQL_structure.sql");

                if (!File.Exists(dbPath))
                {
                    MessageBox.Show("Không tìm thấy file database: " + dbPath, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Directory.CreateDirectory(Path.GetDirectoryName(dumpPath));

                using (var connection = new SqliteConnection($"Data Source={dbPath}"))
                {
                    connection.Open();

                    using (var command = new SqliteCommand("SELECT sql FROM sqlite_master WHERE type IN ('table', 'trigger', 'index') AND name NOT LIKE 'sqlite_%'", connection))
                    using (var reader = command.ExecuteReader())
                    using (var writer = new StreamWriter(dumpPath, false, Encoding.UTF8))
                    {
                        while (reader.Read())
                        {
                            if (!reader.IsDBNull(0))
                            {
                                writer.WriteLine(reader.GetString(0) + ";");
                                writer.WriteLine();
                            }
                        }
                    }

                    MessageBox.Show("Đã xuất cấu trúc cơ sở dữ liệu thành công!", "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất cấu trúc SQLite:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void btn_QuayLai_Click(object sender, EventArgs e)
        {
            // Đóng form hiện tại
            if (Application.OpenForms.OfType<QuanLyHD.QuanLyHopDong>().Any())
            {
                Application.OpenForms.OfType<QuanLyHD.QuanLyHopDong>().First().Show(); // Hiển thị lại TrangChu nếu đã mở
            }
            else
            {
                var mainForm = new QuanLyHD.QuanLyHopDong();
                mainForm.Show(); // Mở TrangChu mới nếu chưa có
            }
            this.Close();
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
