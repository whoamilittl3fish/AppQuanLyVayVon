namespace QuanLyVayVon.CSDL
{
    public class HopDongModel
    {

        public string? MaHD { get; set; }                 // Mã hợp đồng
        public string? TenKH { get; set; }                // Tên khách hàng
        public string? SDT { get; set; }                  // Số điện thoại
        public string? CCCD { get; set; }                 // CCCD / Hộ chiếu
        public string? DiaChi { get; set; }               // Địa chỉ
        public decimal TienVay { get; set; }             // Tổng tiền vay
        public decimal Lai { get; set; }                  // lãi
        public int? HinhThucLaiID { get; set; }           // Hình thức lãi suất
        public int SoNgayVay { get; set; }               // Số ngày vay
        public int KyDongLai { get; set; }               // Kỳ đóng lãi
        public string? NgayVay { get; set; }              // Ngày vay (dạng chuỗi, có thể dùng DateTime)
        public string? NgayHetHan { get; set; }           // Ngày hết hạn
        public string? NgayDongLaiGanNhat { get; set; }   // Ngày đóng lãi gần nhất
        public int? TinhTrang { get; set; }               // 0: Đang vay, 1: Đã tất toán

        public decimal? SoTienLaiMoiKy { get; set; }      // Tiền lãi mỗi kỳ
        public decimal? SoTienLaiCuoiKy { get; set; }     // Tiền lãi kỳ cuối

        public string? TenTaiSan { get; set; }            // Tên tài sản
        public int? LoaiTaiSanID { get; set; }            // Loại tài sản ID
        public string? ThongTinTaiSan1 { get; set; }      // Thông tin tài sản 1
        public string? ThongTinTaiSan2 { get; set; }      // Thông tin tài sản 2
        public string? ThongTinTaiSan3 { get; set; }      // Thông tin tài sản 3

        public string? NVThuTien { get; set; }            // Nhân viên thu tiền
        public string? GhiChu { get; set; }               // Ghi chú

        public string? CreatedAt { get; set; }            // Thời gian tạo
        public string? UpdatedAt { get; set; }            // Thời gian cập nhật


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
