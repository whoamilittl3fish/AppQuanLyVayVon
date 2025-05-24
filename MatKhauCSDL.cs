using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyVayVon
{
    public partial class MatKhauCSDL : Form
    {
        public MatKhauCSDL()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tbox_MatKhauCSDL_TextChanged(object sender, EventArgs e)
        {

        }

        
        private void tbox_MatKhauCSDL_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void tbox_MatKhauCSDL_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (tbox_MatKhauCSDL.Text == "3710")
                {
                    QuanLyCSDL createDB = new QuanLyCSDL();
                    createDB.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Mật khẩu không đúng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbox_MatKhauCSDL.Clear();
                    tbox_MatKhauCSDL.Focus();
                }
            }
        }
    }
}
