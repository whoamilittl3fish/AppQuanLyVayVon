using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyVayVon.Models
{
    public class LoaiTaiSanItem
    {
        public int ID { get; set; }
        public string Ten { get; set; } = string.Empty; // Initialize with a default value

        public override string ToString()
        {
            return Ten;
        }
    }
    public class TimKiemHopDongItem
    {
        public int ID { get; set; } // 1: MaHD, 2: TenKH, 3: SDT, 4: CCCD
        public string FieldName { get; set; } = "";    // Ví dụ: "MaHD"
        public string DisplayName { get; set; } = "";  // Ví dụ: "Mã hợp đồng"

        public override string ToString()
        {
            return DisplayName;
        }
    }

    public class HinhThucLaiInfo
    {
        public int ID { get; set; }
        public string? LoaiLai { get; set; }  // "tienmat" hoặc "phantram"
        public string? DonVi { get; set; }    // "ngay", "tuan", "thang"
    }

    public static class HinhThucLaiHelper
    {
        public static HinhThucLaiInfo GetHinhThucLaiInfo(int id)
        {
            return id switch
            {
                1 => new HinhThucLaiInfo { ID = 1, LoaiLai = "tienmat", DonVi = "ngay" },
                2 => new HinhThucLaiInfo { ID = 2, LoaiLai = "tienmat", DonVi = "tuan" },
                3 => new HinhThucLaiInfo { ID = 3, LoaiLai = "tienmat", DonVi = "thang" },
                4 => new HinhThucLaiInfo { ID = 4, LoaiLai = "phantram", DonVi = "ngay" },
                5 => new HinhThucLaiInfo { ID = 5, LoaiLai = "phantram", DonVi = "tuan" },
                6 => new HinhThucLaiInfo { ID = 6, LoaiLai = "phantram", DonVi = "thang" },
                _ => throw new Exception("ID hình thức lãi không hợp lệ")
            };
        }
    }
}

