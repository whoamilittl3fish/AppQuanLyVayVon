namespace QuanLyVayVon
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // Hi?n th? TrangChu tr??c (d?ng modal)
            using (var trangChu = new TrangChu())
            {
                var result = trangChu.ShowDialog();
                if (result == DialogResult.OK) // ho?c ki?m tra ?i?u ki?n b?n mu?n
                {
                    Application.Run(new QuanLyHD.QuanLyHopDong());
                }
            }
        }
    }
}