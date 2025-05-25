using Microsoft.Data.Sqlite;

namespace QuanLyVayVon
{
    // Pseudocode plan:
    // 1. Set form properties for a modern look (background color, font, size).
    // 2. Style button1: flat style, custom color, rounded corners.
    // 3. Optionally add a label with a title and custom font.
    // 4. Optionally add an icon or image for visual appeal.

    public partial class QuanLyCSDL : Form
    {
        private static readonly Color AppBackColor = Color.FromArgb(245, 245, 250);
        private static readonly Font AppFont = new Font("Segoe UI", 11F, FontStyle.Regular);

        public QuanLyCSDL()
        {
            InitializeComponent();
            this.BackColor = AppBackColor;
            this.Font = AppFont;
            CustomizeUI();
        }

        private void CustomizeUI()
        {
            // Form styling
            this.Text = "Quản Lý Cơ Sở Dữ Liệu";
            this.BackColor = Color.FromArgb(245, 245, 250);
            this.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // Title label
            var titleLabel = new Label
            {
                Text = "Tạo Cơ Sở Dữ Liệu Vay Vốn",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.FromArgb(44, 62, 80),
                AutoSize = true,
                Location = new Point(30, 20)
            };
            this.Controls.Add(titleLabel);

            // Button styling
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;
            button1.BackColor = Color.FromArgb(52, 152, 219);
            button1.ForeColor = Color.White;
            button1.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            button1.Size = new Size(260, 50);
            button1.Location = new Point(30, 70);
            button1.Text = "Tạo Database";
            button1.Cursor = Cursors.Hand;

            // Rounded corners for button
            button1.Region = System.Drawing.Region.FromHrgn(
                NativeMethods.CreateRoundRectRgn(0, 0, button1.Width, button1.Height, 20, 20)
            );
        }

        // Native method for rounded corners
        private static class NativeMethods
        {
            [System.Runtime.InteropServices.DllImport("gdi32.dll", SetLastError = true)]
            public static extern IntPtr CreateRoundRectRgn(
                int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);
        }

        // Button click event handler
        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Tạo Database được nhấn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            // TODO: Add logic to create or manage the database
        }

        // Form closing event handler
        private void TaoCSDL_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Optional: Add logic to confirm exit or cleanup resources
            // Example:
            // if (MessageBox.Show("Bạn có chắc muốn thoát?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.No)
            // {
            //     e.Cancel = true;
            // }
        }
    }
}
