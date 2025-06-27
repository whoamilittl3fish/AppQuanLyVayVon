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
            this.ClientSize = new Size(320, 400); // Tăng chiều cao để đủ cho btn_TiemCamDo
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
            StyleButton(btn_TiemCamDo, "Thông tin tiệm cầm đồ", 120);
            StyleButton(btn_TaoCSDL, "Tạo cơ sở dữ liệu", 60);
            StyleButton(btn_SaoLuu, "Sao lưu", 180);
            StyleButton(btn_UploadSaoluu, "Tải lên sao lưu có sẵn", 240);
            StyleButton(btn_QuayLai, "Quay lại", 300);
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
  -- Bảng chính: Hợp đồng vay
CREATE TABLE IF NOT EXISTS HopDongVay (
    Id INTEGER PRIMARY KEY AUTOINCREMENT, -- ID xác định duy nhất
    MaHD TEXT UNIQUE NOT NULL, -- Mã hợp đồng duy nhất
    TenKH TEXT NOT NULL, -- Tên khách hàng
    SDT TEXT, -- Số điện thoại khách hàng
    CCCD TEXT,
    NgayCapCCCD TEXT, -- Ngày cấp CCCD
    NoiCapCCCD TEXT, -- Nơi cấp CCCD
    DiaChi TEXT, -- Địa chỉ khách hàng
    TienVay REAL, -- Số tiền vay
    TienVayThem REAL DEFAULT 0, -- Số tiền vay thêm (nếu có)
    HinhThucLaiID INTEGER, --   Hình thức lãi (1: Tiền mặt/ngày, 2: Tiền mặt/tuần, 3: Tiền mặt/tháng, 4: Phần trăm/ngày, 5: Phần trăm/tuần, 6 : Phần trăm/tháng)
    SoNgayVay INTEGER, -- Số ngày vay
    KyDongLai INTEGER, -- bao lâu đóng lần
    NgayVay TEXT, -- Ngày bắt đầu vay
    NgayHetHan TEXT, -- Ngày hết hạn hợp đồng
    NgayDongLaiGanNhat TEXT, -- Ngày đóng lãi tiếp theo

    Extended INTEGER DEFAULT 0, -- Có gia hạn hay không (0: Không, 1: Có)
    TinhTrang INTEGER DEFAULT 10, -- Tình trạng hợp đồng ( -2: Chuộc, -1: Chuộc sớm, 0: Đã đóng tất cả kỳ, 1: Đang vay, 2: Sắp tới hạn, 3: Quá hạn, 4: Tới hạn hôm nay, 5: Tới hạn hôm nay và đã đóng lãi)
    TinhTrangLanCuoi INTEGER DEFAULT 10, -- Tình trạng hợp đồng lần cuối (để theo dõi thay đổi)

    Lai REAL DEFAULT 0, -- Lãi suất 
    SoTienLaiMoiKy REAL, -- Số tiền lãi mỗi kỳ
    SoTienLaiCuoiKy REAL, -- Số tiền lãi kỳ cuối
    LaiMoiNgay REAL DEFAULT 0, -- Lãi mỗi ngày

    TongLai REAL DEFAULT 0, -- Tổng lãi phải trả
    TienLaiDaDong REAL DEFAULT 0, -- Tổng lãi đã đóng
    TienLaiDaDongThang REAL DEFAULT 0, -- Tổng lãi đã đóng trong tháng (để tra cứu thông tin)
    TienLaiDaDongTruocDo REAL DEFAULT 0, -- Tổng lãi đã đóng trước đó (để tính toán khi đóng lãi)

    KetThuc BOOLEAN DEFAULT 0, -- Hợp đồng đã kết thúc hay chưa (0: Chưa, 1: Đã kết thúc)
    NgayKetThuc TEXT, -- Ngày kết thúc hợp đồng (nếu đã kết thúc)
    TienKhac REAL DEFAULT 0, -- Tiền khác (nếu có, ví dụ: phí dịch vụ, bảo hiểm, v.v.)
    TongTienChuocDo REAL DEFAULT 0, -- Tổng tiền chuộc đồ (bao gồm tiền gốc, lãi và các khoản khác)
    TienNoConLai REAL DEFAULT 0, -- Tiền nợ còn lại (số tiền khách hàng còn nợ sau khi đã đóng lãi)
    TienDaDong REAL DEFAULT 0, -- Tiền lãi đóng khi chuộc (tra cứu thông tin) 

    TenTaiSan TEXT,
    LoaiTaiSanID INTEGER,
    ThongTinTaiSan1 TEXT,
    ThongTinTaiSan2 TEXT,
    ThongTinTaiSan3 TEXT,
    NVThuTien TEXT,
    GhiChu TEXT,
    LichSu TEXT,

    CreatedAt TEXT DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt TEXT
);

-- Bảng lịch sử đóng lãi
CREATE TABLE IF NOT EXISTS LichSuDongLai (
    ID INTEGER PRIMARY KEY AUTOINCREMENT,
    MaHD TEXT NOT NULL,
    KyThu INTEGER NOT NULL,
    NgayBatDauKy TEXT NOT NULL,
    NgayDenHan TEXT NOT NULL,
    NgayDongThucTe TEXT,
    SoTienPhaiDong REAL NOT NULL,
    SoTienDaDong REAL DEFAULT 0,
    TinhTrang INTEGER DEFAULT 1,
    GhiChu TEXT,
    CreatedAt TEXT DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt TEXT,

    FOREIGN KEY (MaHD) REFERENCES HopDongVay(MaHD) ON DELETE CASCADE
);

-- Bảng lịch sử cập nhật hợp đồng
CREATE TABLE IF NOT EXISTS LichSuCapNhatHopDong (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    MaHD TEXT NOT NULL,
    ThoiGian TEXT NOT NULL,
    HanhDong TEXT NOT NULL,
    GhiChu TEXT
);

-- Bảng cấu hình hệ thống
CREATE TABLE IF NOT EXISTS HeThong (
    Khoa TEXT PRIMARY KEY,
    GiaTri TEXT,
    GhiChu TEXT,
    UpdatedAt TEXT DEFAULT CURRENT_TIMESTAMP
);



-- Bảng tổng tiền đã thu trong tháng
CREATE TABLE IF NOT EXISTS TienDaThuTrongThang (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    ThangNam TEXT NOT NULL,          -- Định dạng YYYY-MM
    MaHD TEXT NOT NULL,
    TongTienDaThu REAL DEFAULT 0,
    UpdatedAt TEXT DEFAULT CURRENT_TIMESTAMP,

    UNIQUE (ThangNam, MaHD),
    FOREIGN KEY (MaHD) REFERENCES HopDongVay(MaHD) ON DELETE CASCADE
);

-- Bảng tiệm cầm đồ
CREATE TABLE IF NOT EXISTS TiemCamDo (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,

    TenTiem TEXT NOT NULL,               -- Tên tiệm: TIỆM CẦM ĐỒ PHÚC DUY
    DiaChi TEXT NOT NULL,               -- Địa chỉ tiệm
    Hotline TEXT,                       -- Hotline/SĐT liên hệ

    DaiDien TEXT,                       -- Người đại diện
    SDTDaiDien TEXT,                    -- SĐT người đại diện
    TaiKhoan TEXT,                      -- Số tài khoản ngân hàng
    TenNganHang TEXT,                   -- Tên ngân hàng

    TruongPGDTT TEXT,                   -- Trưởng phòng giao dịch tài chính tiêu dùng

    UpdatedAt TEXT DEFAULT CURRENT_TIMESTAMP
);




-- Index
CREATE INDEX IF NOT EXISTS idx_HopDongVay_MaHD ON HopDongVay(MaHD);
CREATE INDEX IF NOT EXISTS idx_HopDongVay_TenKH ON HopDongVay(TenKH);
CREATE INDEX IF NOT EXISTS idx_HopDongVay_SDT ON HopDongVay(SDT);
CREATE INDEX IF NOT EXISTS idx_HopDongVay_CCCD ON HopDongVay(CCCD);
CREATE INDEX IF NOT EXISTS idx_LichSuDongLai_MaHD ON LichSuDongLai(MaHD);
CREATE INDEX IF NOT EXISTS idx_LichSuDongLai_MaHD_KyThu ON LichSuDongLai(MaHD, KyThu);

-- Trigger: Sau khi INSERT dòng đóng lãi
CREATE TRIGGER IF NOT EXISTS trg_insert_LichSuDongLai
AFTER INSERT ON LichSuDongLai
FOR EACH ROW
BEGIN
    -- Cập nhật tổng tiền lãi đã đóng vào HopDongVay
    UPDATE HopDongVay
    SET
        TienLaiDaDong = (
            SELECT SUM(SoTienDaDong)
            FROM LichSuDongLai
            WHERE MaHD = NEW.MaHD
        ),
        UpdatedAt = CURRENT_TIMESTAMP
    WHERE MaHD = NEW.MaHD;

    -- Cập nhật tổng tiền đã thu trong tháng hiện tại
    INSERT INTO TienDaThuTrongThang (ThangNam, MaHD, TongTienDaThu)
    SELECT strftime('%Y-%m', 'now'), NEW.MaHD,
           (
             SELECT TienLaiDaDong - TienLaiDaDongTruocDo
             FROM HopDongVay
             WHERE MaHD = NEW.MaHD
           )
    ON CONFLICT(ThangNam, MaHD) DO UPDATE SET
        TongTienDaThu = (
            SELECT TienLaiDaDong - TienLaiDaDongTruocDo
            FROM HopDongVay
            WHERE MaHD = NEW.MaHD
        ),
        UpdatedAt = CURRENT_TIMESTAMP;
END;


-- Trigger: Sau khi UPDATE số tiền đã đóng
CREATE TRIGGER IF NOT EXISTS trg_update_SoTienDaDong
AFTER UPDATE OF SoTienDaDong ON LichSuDongLai
FOR EACH ROW
BEGIN
    UPDATE HopDongVay
    SET
        TienLaiDaDong = (
            SELECT SUM(SoTienDaDong)
            FROM LichSuDongLai
            WHERE MaHD = NEW.MaHD
        ),
        UpdatedAt = CURRENT_TIMESTAMP
    WHERE MaHD = NEW.MaHD;

    UPDATE TienDaThuTrongThang
    SET TongTienDaThu = (
        SELECT TienLaiDaDong - TienLaiDaDongTruocDo
        FROM HopDongVay
        WHERE MaHD = NEW.MaHD
    ),
        UpdatedAt = CURRENT_TIMESTAMP
    WHERE MaHD = NEW.MaHD
      AND ThangNam = strftime('%Y-%m', 'now');
END;

-- Trigger: Sau khi DELETE dòng đóng lãi
CREATE TRIGGER IF NOT EXISTS trg_delete_LichSuDongLai
AFTER DELETE ON LichSuDongLai
FOR EACH ROW
BEGIN
    UPDATE HopDongVay

    SET
        TienLaiDaDong = (
            SELECT IFNULL(SUM(SoTienDaDong), 0)
            FROM LichSuDongLai
            WHERE MaHD = OLD.MaHD
        ),
        UpdatedAt = CURRENT_TIMESTAMP
    WHERE MaHD = OLD.MaHD;

    UPDATE TienDaThuTrongThang
    SET TongTienDaThu = (
        SELECT TienLaiDaDong - TienLaiDaDongTruocDo
        FROM HopDongVay
        WHERE MaHD = OLD.MaHD
    ),
        UpdatedAt = CURRENT_TIMESTAMP
    WHERE MaHD = OLD.MaHD
      AND ThangNam = strftime('%Y-%m', 'now');
END;

-- Trigger: Cập nhật trạng thái 5 → 0 nếu kỳ sau đã trả
CREATE TRIGGER IF NOT EXISTS trg_UpdateTinhTrang5To0
AFTER INSERT ON LichSuDongLai
BEGIN
    UPDATE LichSuDongLai
    SET TinhTrang = 0,
        UpdatedAt = CURRENT_TIMESTAMP
    WHERE TinhTrang = 5
      AND EXISTS (
          SELECT 1 FROM LichSuDongLai AS next
          WHERE next.MaHD = LichSuDongLai.MaHD
            AND next.KyThu = LichSuDongLai.KyThu + 1
            AND next.SoTienDaDong >= next.SoTienPhaiDong
      );
END;

CREATE TRIGGER IF NOT EXISTS trg_UpdateTinhTrang5To0_AfterUpdate
AFTER UPDATE ON LichSuDongLai
BEGIN
    UPDATE LichSuDongLai
    SET TinhTrang = 0,
        UpdatedAt = CURRENT_TIMESTAMP
    WHERE TinhTrang = 5
      AND EXISTS (
          SELECT 1 FROM LichSuDongLai AS next
          WHERE next.MaHD = LichSuDongLai.MaHD
            AND next.KyThu = LichSuDongLai.KyThu + 1
            AND next.SoTienDaDong >= next.SoTienPhaiDong
      );
END;
        ";
                        command.ExecuteNonQuery();

                        connection.Close();
                    }
                    KhoiTaoDuLieuLanDau(); // Khởi tạo dữ liệu lần đầu
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

        public static void KhoiTaoDuLieuLanDau()
        {
            string dbPath = Path.Combine(Application.StartupPath, "DataBase", "data.db");
            using (var connection = new SqliteConnection($"Data Source={dbPath}"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = @"
        INSERT OR IGNORE INTO HeThong (Khoa, GiaTri, GhiChu, UpdatedAt)
        VALUES 
     
        ('ResetDauThang', strftime('%Y-%m', 'now'), 'Khởi tạo reset đầu tháng', CURRENT_TIMESTAMP);
        ";
                command.ExecuteNonQuery();
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

        private void btn_TiemCamDo_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<ThongTinTiemCamDo>().Any())
            {
                Application.OpenForms.OfType<ThongTinTiemCamDo>().First().Show(); // Hiển thị lại nếu đã mở
            }
            else
            {
                var tiemCamDoForm = new ThongTinTiemCamDo();
                tiemCamDoForm.Show(); // Mở mới nếu chưa có
                this.Close(); // Đóng form hiện tại
            }
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
