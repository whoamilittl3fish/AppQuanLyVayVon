using Microsoft.Data.Sqlite;
using System.Media;

namespace QuanLyVayVon
{
    public partial class ThemHopDongMoi : Form
    {
        public ThemHopDongMoi()
        {
            InitializeComponent();
        }

        private void ThemHopDongMoi_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

            // Set the DateTimePicker to current system time, allow user to edit, format dd-mm-yyyy
            dtimep_NgayVay.Value = DateTime.Now;
            dtimep_NgayVay.Format = DateTimePickerFormat.Custom;
            dtimep_NgayVay.CustomFormat = "dd-MM-yyyy";

            // Set dTimeP_NgayHetHan to 6 months after current date, format dd-mm-yyyy
            dtimep_NgayHetHan.Value = DateTime.Now.AddMonths(6);
            dtimep_NgayHetHan.Format = DateTimePickerFormat.Custom;
            dtimep_NgayHetHan.CustomFormat = "dd-MM-yyyy";

            // Dock tableLayoutP_Nhap to Top so it stretches horizontally but keeps height
            tableLayoutP_Nhap.Dock = DockStyle.Top;

            // Position flowLayoutP_Luu below tableLayoutP_Nhap and align right
            flowLayoutP_Luu.Dock = DockStyle.None;
            flowLayoutP_Luu.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            flowLayoutP_Luu.Top = tableLayoutP_Nhap.Bottom + 10; // 10px margin
            flowLayoutP_Luu.Left = this.ClientSize.Width - flowLayoutP_Luu.Width - 20; // 20px right margin

            // Handle resize to keep flowLayoutP_Luu at bottom right of tableLayoutP_Nhap
            this.Resize += (s, ev) =>
            {
                flowLayoutP_Luu.Left = this.ClientSize.Width - flowLayoutP_Luu.Width - 20;
                flowLayoutP_Luu.Top = tableLayoutP_Nhap.Bottom + 10;
            };
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            // Stretch tableLayoutP_Nhap horizontally, keep height unchanged
            if (tableLayoutP_Nhap != null)
            {
                tableLayoutP_Nhap.Width = this.ClientSize.Width;
                // Height remains unchanged
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();

            var trangChu = Application.OpenForms.OfType<TrangChu>().FirstOrDefault();
            if (trangChu == null)
            {
                trangChu = new TrangChu();
                trangChu.Show();
            }
            else
            {
                trangChu.BringToFront();
            }
        }

        private void ThemHopDongMoi_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;   // Hủy đóng form
                this.Hide();       // Ẩn form phụ

                // Mở lại form chính
                var mainForm = Application.OpenForms.OfType<TrangChu>().FirstOrDefault();
                if (mainForm != null)
                {
                    mainForm.Show();
                    mainForm.BringToFront();
                }
                else
                {
                    // Nếu form chính chưa tồn tại (ít khi xảy ra), bạn có thể tạo mới
                    mainForm = new TrangChu();
                    mainForm.Show();
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dTimeP_NgayHetHan_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_2(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            try
            {
                // 1. Lấy dữ liệu từ giao diện
                string maHD = tbox_MaHD.Text.Trim();
                string tenKH = tbox_Ten.Text.Trim();
                string sdt = tbox_SDT.Text.Trim();
                string cccd = tbox_CCCD.Text.Trim();
                double tienVay = double.Parse(tbox_TienVay.Text);
                DateTime ngayVay = dtimep_NgayVay.Value;
                DateTime ngayHetHan = dtimep_NgayHetHan.Value;
                double laiThang = double.Parse(tb_LaiThang.Text); // % tháng
                string doCam = rtbox_DoCam.Text.Trim();

                // 2. Tính toán
                double laiTuan = (tienVay * laiThang / 100) / 4;

                int soThangQua = ((DateTime.Now.Year - ngayVay.Year) * 12) + (DateTime.Now.Month - ngayVay.Month);
                double laiDenHomNay = tienVay * laiThang / 100 * soThangQua;

                DateTime ngayDongLai = ngayVay.AddMonths(soThangQua + 1);

                // 3. Kết nối và lưu vào SQLite
                string dbPath = Path.Combine(Application.StartupPath, "data.db");
                string connectionString = $"Data Source={dbPath}";
                MessageBox.Show(dbPath);

                using (var conn = new SqliteConnection(connectionString))
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"
                    INSERT INTO HopDongVay (
                        MaHD, TenKH, SDT, CCCD, TienVay, LaiSuatPhanTram,
                        LaiTuan, LaiDenHomNay, NgayDongLai,
                        NgayVay, NgayHetHan, DoCam, DaChuoc, UpdatedAt
                    ) VALUES (
                        @MaHD, @TenKH, @SDT, @CCCD, @TienVay, @LaiSuatPhanTram,
                        @LaiTuan, @LaiDenHomNay, @NgayDongLai,
                        @NgayVay, @NgayHetHan, @DoCam, 0, @UpdatedAt
                    );";

                        cmd.Parameters.AddWithValue("@MaHD", maHD);
                        cmd.Parameters.AddWithValue("@TenKH", tenKH);
                        cmd.Parameters.AddWithValue("@SDT", sdt);
                        cmd.Parameters.AddWithValue("@CCCD", cccd);
                        cmd.Parameters.AddWithValue("@TienVay", tienVay);
                        cmd.Parameters.AddWithValue("@LaiSuatPhanTram", laiThang);
                        cmd.Parameters.AddWithValue("@LaiTuan", laiTuan);
                        cmd.Parameters.AddWithValue("@LaiDenHomNay", laiDenHomNay);
                        cmd.Parameters.AddWithValue("@NgayDongLai", ngayDongLai.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@NgayVay", ngayVay.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@NgayHetHan", ngayHetHan.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@DoCam", doCam);
                        cmd.Parameters.AddWithValue("@UpdatedAt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                        cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                }

                MessageBox.Show("✅ Lưu hợp đồng thành công!");
                SystemSounds.Asterisk.Play();

            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi khi lưu: " + ex.Message);
                SystemSounds.Hand.Play();
            }
        }

    }
}

