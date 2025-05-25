using Microsoft.Data.Sqlite;
using System.Media;
using System.Windows.Forms;

namespace QuanLyVayVon
{
    public partial class QuanLyHopDong : Form
    {
        private static readonly Color AppBackColor = Color.FromArgb(245, 245, 250);
        private static readonly Font AppFont = new Font("Segoe UI", 11F, FontStyle.Regular);

        public QuanLyHopDong()
        {
            InitializeComponent();
            this.BackColor = AppBackColor;
            this.Font = AppFont;
            // ... các khởi tạo khác ...
        }

        private void ThemHopDongMoi_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            dataGridView_ThongTinHopDong.Dock = DockStyle.None;
            dataGridView_ThongTinHopDong.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            dataGridView_ThongTinHopDong.Left = 20;
            dataGridView_ThongTinHopDong.Width = this.ClientSize.Width - 40;
            dataGridView_ThongTinHopDong.Height = this.ClientSize.Height - dataGridView_ThongTinHopDong.Top - 20;
            this.Resize += (s, ev) =>
            {
                dataGridView_ThongTinHopDong.Left = 20;
                dataGridView_ThongTinHopDong.Width = this.ClientSize.Width - 40;
                dataGridView_ThongTinHopDong.Height = this.ClientSize.Height - dataGridView_ThongTinHopDong.Top - 20;
            };

            dataGridView_ThongTinHopDong.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            dataGridView_ThongTinHopDong.AutoResizeColumnHeadersHeight();

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
                    mainForm = new TrangChu();
                    mainForm.Show();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

