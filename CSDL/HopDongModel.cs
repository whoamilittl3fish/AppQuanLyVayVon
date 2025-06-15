namespace QuanLyVayVon.CSDL
{
    public class HopDongModel
    {

        public int Id { get; set; }  // thêm dòng này
        public string? MaHD { get; set; }
        public string? TenKH { get; set; }
        public string? SDT { get; set; }
        public string? CCCD { get; set; }
        public string? DiaChi { get; set; }
        public decimal TienVay { get; set; }
        public int? HinhThucLaiID { get; set; }
        public int SoNgayVay { get; set; }
        public int KyDongLai { get; set; }
        public string? NgayVay { get; set; }
        public string? NgayHetHan { get; set; }
        public string? NgayDongLaiGanNhat { get; set; }
        public int? TinhTrang { get; set; }

        public decimal Lai { get; set; }
        public decimal? SoTienLaiMoiKy { get; set; }
        public decimal? SoTienLaiCuoiKy { get; set; }
        public decimal? TienLaiDaDong { get; set; }
        public decimal? TongLai { get; set; }

        public int? LoaiTaiSanID { get; set; }
        public string? TenTaiSan { get; set; }
        public string? ThongTinTaiSan1 { get; set; }
        public string? ThongTinTaiSan2 { get; set; }
        public string? ThongTinTaiSan3 { get; set; }

        public string? NVThuTien { get; set; }
        public string? GhiChu { get; set; }

        public string? CreatedAt { get; set; }
        public string? UpdatedAt { get; set; }


    }

    public class LichSuDongLaiModel
    {
        public int ID { get; set; }                           // ID tự tăng
        public string? MaHD { get; set; }                     // Mã hợp đồng liên kết
        public int KyThu { get; set; }                        // Kỳ thứ mấy
        public string? NgayBatDauKy { get; set; }             // Ngày bắt đầu kỳ
        public string? NgayDenHan { get; set; }               // Ngày đến hạn đóng
        public string? NgayDongThucTe { get; set; }           // Ngày thực tế khách đóng

        public decimal SoTienPhaiDong { get; set; }           // Số tiền phải đóng
        public decimal SoTienDaDong { get; set; }             // Số tiền đã đóng

        public int? TinhTrang { get; set; }                   // 0: Đang vay, 1: Đã đóng lãi
        public string? GhiChu { get; set; }                   // Ghi chú thêm nếu có

        public string? CreatedAt { get; set; }                // Ngày tạo
        public string? UpdatedAt { get; set; }                // Ngày cập nhật
    }



}
