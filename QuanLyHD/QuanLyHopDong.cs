using Microsoft.Data.Sqlite;
using QuanLyVayVon.CSDL;
using System.Media;
using System.Reflection.Metadata.Ecma335;
using System.Windows.Forms;

namespace QuanLyVayVon.QuanLyHD
{
    public partial class QuanLyHopDong : Form
    {
        // Màu nền và font mặc định cho ứng dụng
        private static readonly Color AppBackColor = Color.FromArgb(245, 245, 250);
        private static readonly Font AppFont = new Font("Segoe UI", 11F, FontStyle.Regular);

        public QuanLyHopDong()
        {
            InitializeComponent();
            this.BackColor = AppBackColor;
            this.Font = AppFont;

            StyleButton(btn_Thoat);
            StyleButton(btn_ThemHopDong);
            StyleButton(btn_MoCSDL);
            InitDataGridView();
            this.FormBorderStyle = FormBorderStyle.None; // Loại bỏ viền để bo góc
        }

        // Hàm khởi tạo và style cho DataGridView
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

            // Cài đặt style cho header
            dataGridView_ThongTinHopDong.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            dataGridView_ThongTinHopDong.AutoResizeColumnHeadersHeight();

            // Tính toán lại độ rộng các cột
            int totalWidth = 0;
            foreach (DataGridViewColumn col in dataGridView_ThongTinHopDong.Columns)
            {
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                int cellWidth = col.Width;
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                int headerWidth = col.Width;
                col.Width = Math.Max(cellWidth, headerWidth);
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                totalWidth += col.Width;
            }
            int extraSpace = dataGridView_ThongTinHopDong.ClientSize.Width - totalWidth;
            if (extraSpace > 0 && dataGridView_ThongTinHopDong.Columns.Count > 0)
            {
                int addPerColumn = extraSpace / dataGridView_ThongTinHopDong.Columns.Count;
                foreach (DataGridViewColumn col in dataGridView_ThongTinHopDong.Columns)
                    col.Width += addPerColumn;
            }

            // Cài đặt style tổng thể cho DataGridView
            dataGridView_ThongTinHopDong.ScrollBars = ScrollBars.Both;
            dataGridView_ThongTinHopDong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView_ThongTinHopDong.Font = new Font("Segoe UI", 10);
            dataGridView_ThongTinHopDong.BackgroundColor = Color.White;
            dataGridView_ThongTinHopDong.GridColor = Color.LightGray;
            dataGridView_ThongTinHopDong.BorderStyle = BorderStyle.None;
            dataGridView_ThongTinHopDong.EnableHeadersVisualStyles = false;
            dataGridView_ThongTinHopDong.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            dataGridView_ThongTinHopDong.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView_ThongTinHopDong.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dataGridView_ThongTinHopDong.ColumnHeadersHeight = 35;
            dataGridView_ThongTinHopDong.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView_ThongTinHopDong.RowTemplate.Height = 30;
            dataGridView_ThongTinHopDong.DefaultCellStyle.SelectionBackColor = Color.LightSteelBlue;
            dataGridView_ThongTinHopDong.DefaultCellStyle.SelectionForeColor = Color.Black;
            dataGridView_ThongTinHopDong.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dataGridView_ThongTinHopDong.AllowUserToResizeRows = false;
            dataGridView_ThongTinHopDong.RowHeadersWidth = 40;
        }


        // Hàm style riêng cho từng button
        private void StyleButton(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.BackColor = Color.SteelBlue;
            btn.ForeColor = Color.White;
            btn.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btn.FlatAppearance.BorderSize = 0;
            btn.Height = 42;
            btn.Width = 150;
            btn.Cursor = Cursors.Hand;
            btn.Margin = new Padding(8);
            btn.Padding = new Padding(0);
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(70, 130, 180);
            btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(40, 90, 140);
            btn.TextAlign = ContentAlignment.MiddleCenter;
            // Bo góc cho button
            btn.Region = System.Drawing.Region.FromHrgn(
                NativeMethods.CreateRoundRectRgn(0, 0, btn.Width, btn.Height, 18, 18));
        }

        // Class hỗ trợ bo góc cho button
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
            var hopDongForm = new HopDongForm(null);
            hopDongForm.Show();

        }

        private void QuanLyHopDong_Load(object sender, EventArgs e)
        {

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
            string MaHD = tb_Test.Text.Trim();
            if (Application.OpenForms.OfType<HopDongForm>().Any())
            {
                Application.OpenForms.OfType<HopDongForm>().First().Show();
                return;
            }
            var hopDongForm = new HopDongForm(MaHD);
            hopDongForm.Show();
        }
    }
}
