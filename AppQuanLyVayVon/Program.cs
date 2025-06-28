using QuanLyVayVon.CSDL;
using QuanLyVayVon.QuanLyHD;
using QuestPDF.Infrastructure;
namespace QuanLyVayVon

{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // Hi?n th? TrangChu tr??c (d?ng modal)
            using (var license = new License())
            {
                
                if (license.ShowDialog() == DialogResult.OK || license.ShowDialog() == DialogResult.Yes) // ho?c ki?m tra ?i?u ki?n b?n mu?n
                {
                   
                    Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
                    QuestPDF.Settings.License = LicenseType.Community;
                    QuanLyCSDL.TaoCoSoDuLieuNeuChuaCo();
                    QuanLyCSDL.DatMatKhauMacDinh();
                    Application.Run(new QuanLyHD.QuanLyHopDong());

                    
                }
                else
                    {
                    // Nếu người dùng không đồng ý với giấy phép, thoát ứng dụng
                    Application.Exit();
                }
            }
        }
    }
}