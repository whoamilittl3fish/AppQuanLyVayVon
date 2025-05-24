using Microsoft.Data.Sqlite;

namespace QuanLyVayVon
{
    public partial class QuanLyCSDL : Form
    {
        public QuanLyCSDL()
        {
            InitializeComponent();
        }

        private void CreateDB_Load(object sender, EventArgs e)
        {

        }

        private void CreateDB_Load_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string dbPath = "data.db";

            if (!File.Exists(dbPath))
            {
                using (var connection = new SqliteConnection($"Data Source={dbPath}"))
                {
                    connection.Open();

                    string createTableQuery = @"
        CREATE TABLE IF NOT EXISTS HopDongVay (
    MaHD TEXT PRIMARY KEY,                  -- Mã hợp đồng (ví dụ: ""CĐ-24"")
    TenKH TEXT NOT NULL,                    -- Tên khách hàng
    SDT TEXT,                               -- Số điện thoại
    CCCD TEXT,                              -- Căn cước công dân
    TienVay REAL,                           -- Tiền vay
    LaiSuatPhanTram REAL,                   -- Lãi suất (%/tháng hoặc %/năm)

    LaiTuan REAL,                           -- Lãi theo tuần (tự động tính dựa trên lãi suất)
    LaiDenHomNay REAL DEFAULT 0,            -- Lãi tính đến hôm nay
    NgayDongLai TEXT,                       -- Ngày đóng lãi tiếp theo (1 tháng sau mỗi lần đóng)

    NgayVay TEXT,                           -- Ngày vay
    NgayHetHan TEXT,                        -- Ngày hết hạn
    DoCam TEXT,                             -- Đồ cầm
    DaChuoc INTEGER DEFAULT 0,              -- Đã chuộc (0 = chưa, 1 = đã)

    CreatedAt TEXT DEFAULT CURRENT_TIMESTAMP, -- Thời gian tạo
    UpdatedAt TEXT                          -- Thời gian chỉnh sửa
);

        ";

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = createTableQuery;
                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show("Đã tạo database và bảng HopDongVay thành công!");
                }
            }
            else
            {
                MessageBox.Show("File database đã tồn tại!");
            }
        }
        private void TaoCSDL_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Nếu người dùng tự bấm X để đóng form
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;     // Hủy việc đóng form
                this.Hide();         // Ẩn form taoCSDL

                // Tìm form TrangChu đang mở
                var formTrangChu = Application.OpenForms.OfType<TrangChu>().FirstOrDefault();

                if (formTrangChu != null)
                {
                    formTrangChu.Show();
                    formTrangChu.BringToFront();
                }
                else
                {
                    // Nếu TrangChu chưa tồn tại thì tạo mới
                    new TrangChu().Show();
                }
            }
        }


    }
}
