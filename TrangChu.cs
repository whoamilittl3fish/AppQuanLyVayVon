namespace QuanLyVayVon
{
    public partial class TrangChu : Form
    {
        public TrangChu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MatKhauCSDL matKhau = new MatKhauCSDL();
            matKhau.Show();

            this.Hide();


        }

        private void button3_Click(object sender, EventArgs e)
        {
            var formThem = new ThemHopDongMoi();
            formThem.Show();

            this.Hide();
        }
    }
}
