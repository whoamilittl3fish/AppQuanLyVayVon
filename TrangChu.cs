namespace QuanLyVayVon
{
    public partial class TrangChu : Form
    {
        private static readonly Color AppBackColor = Color.FromArgb(245, 245, 250);
        private static readonly Font AppFont = new Font("Segoe UI", 11F, FontStyle.Regular);

        public TrangChu()
        {
            InitializeComponent();
            this.BackColor = AppBackColor;
            this.Font = AppFont;
            this.StartPosition = FormStartPosition.CenterScreen;
          
            // Do not call TrangChu_Load() directly here.
            // The event will be triggered automatically when the form loads.
        }

        /// <summary>
        /// Applies custom UI styles to the form and its controls (only common properties, no size/position).
        /// </summary>
        private void ApplyCustomUI()
        {
            // Set form appearance
            this.BackColor = Color.FromArgb(245, 245, 255);
            this.FormBorderStyle = FormBorderStyle.None; // Remove border for rounded corners
            

            // Set a large, bold Arial font for all controls
            var commonFont = new Font("Arial", 16, FontStyle.Bold);

            // Apply rounded corners to the form
            int radius = 30;
            this.Region = System.Drawing.Region.FromHrgn(
                NativeMethods.CreateRoundRectRgn(0, 0, this.Width, this.Height, radius, radius)
            );

            // Style all controls (only common properties)
            foreach (Control ctrl in this.Controls)
            {
                ctrl.Font = commonFont;

                if (ctrl is Button btn)
                {
                    btn.BackColor = Color.FromArgb(70, 130, 180);
                    btn.ForeColor = Color.White;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.Cursor = Cursors.Hand;
                    // Only apply rounded corners, do not change size or position
                    btn.Region = System.Drawing.Region.FromHrgn(
                        NativeMethods.CreateRoundRectRgn(0, 0, btn.Width, btn.Height, 20, 20)
                    );
                }
                else if (ctrl is Label lbl)
                {
                    lbl.ForeColor = Color.FromArgb(40, 40, 80);
                }
            }
        }

        /// <summary>
        /// Handles the click event for button1 (Cơ sở dữ liệu).
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            Function_Reuse.ShowFormIfNotOpen<CSDL.MatKhauCSDL>();
            this.Hide(); // Close the current form after opening the new one
        }

        // Native method for rounded corners
        private static class NativeMethods
        {
            [System.Runtime.InteropServices.DllImport("gdi32.dll", SetLastError = true)]
            public static extern IntPtr CreateRoundRectRgn(
                int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);
        }

        private void TrangChu_Load(object sender, EventArgs e)
        {
            ApplyCustomUI();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Function_Reuse.ShowFormIfNotOpen<QuanLyHD.QuanLyHopDong>();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Function_Reuse.ConfirmAndClose_App();
        }
    }
}
