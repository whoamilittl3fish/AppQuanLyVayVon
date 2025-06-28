using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using static QuanLyVayVon.QuanLyHD.QuanLyHopDong;

namespace QuanLyVayVon
{
    public partial class License : Form
    {
        bool? NhapKey = false;
        public License(bool? NhapKey = false)
        {

            InitializeComponent();
            ApplyCustomUI();
            this.NhapKey = NhapKey;

        }
        private void CheckSaveKey()
        {
            if (NhapKey == false)
            {
                var CheckKey = LicenseHelper.KiemTraFilePublicKeyTonTai(out string fullPath);
                if (CheckKey)
                {

                    string Key = LicenseHelper.LoadKeyTuFile();
                    if (Key != null)
                    {
                        {

                            var Key_Verified = LicenseHelper.VerifyKeyWithPublicKey(Key);
                            if (Key_Verified == 1)
                            {
                                CustomMessageBox.ShowCustomMessageBox("Cảm ơn bạn vì đã tin tưởng sản phẩm.", null, "Thông báo");
                                this.DialogResult = DialogResult.OK;
                                this.Close();

                            }
                            else if (Key_Verified == 2)
                            {                 // If license is valid, you can proceed with the application logic
                                string remainingTime = LicenseHelper.LayThongTinThoiGianConLai();

                                this.DialogResult = DialogResult.Yes;
                                this.Close();
                            }
                            else if (Key_Verified == 0)
                            {
                                CustomMessageBox.ShowCustomMessageBox("Mời bạn nhập KEY để sử dụng sản phẩm. Liên hệ: 0966346694 (Zalo)", null, "Thông báo");
                            }
                        }
                    }
                }
            }
        }
        private void ApplyCustomUI()
        {
            this.StartPosition = FormStartPosition.CenterScreen;

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




        private void button1_Click(object sender, EventArgs e)
        {
            string key = rtb_Key.Text.Trim();
            var checkKey = LicenseHelper.VerifyKeyWithPublicKey(key);

            if (checkKey == 1 )
            {
                LicenseHelper.SaveKeyVaoFile(key);
                CustomMessageBox.ShowCustomMessageBox("Cảm ơn bạn đã kích hoạt bản quyền thành công.", null, "Thông báo");

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else if (checkKey == 2)
            {
                string remainingTime = LicenseHelper.LayThongTinThoiGianConLai();
                MessageBox.Show($"Bản quyền hợp lệ. Thời gian còn lại: {remainingTime}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.Yes;
                this.Close();
            }
            else
            {
                MessageBox.Show("Key nhập không hợp lệ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void License_Load(object sender, EventArgs e)
        {
            CheckSaveKey();
        }
    }

}
