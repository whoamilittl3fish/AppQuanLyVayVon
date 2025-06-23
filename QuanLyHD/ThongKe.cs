using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace QuanLyVayVon.QuanLyHD
{
    public partial class ThongKe : Form
    {
        public ThongKe()
        {
            InitializeComponent();
        }

        private void ThongKeTien_Load(object sender, EventArgs e)
        {
           rtb_TongLaiThuTrongThang.Text = LayTongLaiThuTrongThang().ToString("N0");
           rtb_TienDangChoVay.Text = TinhTongTienDangChoVay().ToString("N0");
        }
        public static decimal TinhTongTienDangChoVay()
        {
            try
            {
                string dbPath = Path.Combine(Application.StartupPath, "DataBase", "data.db");
                using (var connection = new SqliteConnection($"Data Source={dbPath}"))
                {
                    connection.Open();

                    string query = @"
                    SELECT SUM(TienVay + TienVayThem)
                    FROM HopDongVay
                    WHERE TinhTrang NOT IN (-1, -2);
                ";

                    using (var command = new SqliteCommand(query, connection))
                    {
                        object result = command.ExecuteScalar();
                        return result != DBNull.Value ? Convert.ToDecimal(result) : 0m;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tính tổng tiền đang cho vay:\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0m;
            }
        }
        public static decimal LayTongLaiThuTrongThang()
        {
            decimal tongTien = 0;

            string dbPath = Path.Combine(Application.StartupPath, "DataBase", "data.db");
            string connectionString = $"Data Source={dbPath}";

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string query = @"
            SELECT IFNULL(SUM(TienLaiDaDong - TienLaiDaDongTruocDo), 0)
            FROM HopDongVay;
        ";

                using (var command = new SqliteCommand(query, connection))
                {
                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        tongTien = Convert.ToDecimal(result);
                    }
                }

                connection.Close();
            }

            return tongTien;
        }


    }
}
