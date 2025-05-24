using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;


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
