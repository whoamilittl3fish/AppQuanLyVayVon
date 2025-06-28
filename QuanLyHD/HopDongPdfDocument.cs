using QuanLyVayVon.CSDL;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Globalization;

public class HopDongPdfDocument : IDocument
{
    private readonly HopDongModel hd;
    private readonly TiemCamDoModel tiem;

    public HopDongPdfDocument(HopDongModel hopdong, TiemCamDoModel tiemCamDo)
    {
        this.hd = hopdong;
        this.tiem = tiemCamDo;
    }

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Size(PageSizes.A4);
            page.Margin(30); // giảm lề
            page.DefaultTextStyle(x => x.FontSize(13).LineHeight(1.4f)); // tăng font

            page.Content().Column(col =>
            {
                col.Item().Row(row =>
                {
                    row.RelativeItem().Column(c =>
                    {
                        c.Item().Text(tiem.TenTiem ?? "").Bold();
                        c.Item().Text(t => { t.Span("Hotline: ").Bold(); t.Span(tiem.Hotline ?? ""); });
                        c.Item().Text(t => { t.Span("Mã Giao Dịch: ").Bold(); t.Span(hd.MaHD ?? ""); });
                    });

                    row.RelativeItem().AlignRight().Column(c =>
                    {
                        c.Item().Text("HỢP ĐỒNG CẦM ĐỒ").Bold().FontSize(16);
                        c.Item().Text("(Kiêm phiếu chi tiền mặt)").Bold();
                        c.Item().Text(t =>
                        {
                            t.Span("Ngày : ").Bold();
                            t.Span(FormatDate(hd.NgayVay)).Bold();
                        });
                    });
                });

                // BÊN CẦM ĐỒ
                col.Item().PaddingTop(10).Text("BÊN CẦM ĐỒ: " + (tiem.TenTiem ?? "")).Bold().Underline();
                col.Item().Text(t => { t.Span("Người đại diện : "); t.Span(tiem.DaiDien ?? "").Bold(); });
                col.Item().Text(t => { t.Span("Điện thoại: "); t.Span(tiem.SDTDaiDien ?? "").Bold(); });
                col.Item().Text(t => { t.Span("Địa chỉ: "); t.Span(tiem.DiaChi ?? "").Bold(); });
                col.Item().Text(t => { t.Span("Tên ngân hàng: "); t.Span(tiem.TenNganHang ?? "").Bold(); });
                col.Item().Text(t => { t.Span("STK: "); t.Span(tiem.TaiKhoan ?? "").Bold(); });

                // NGƯỜI VAY
                col.Item().PaddingTop(10).Text("NGƯỜI CẦM ĐỒ: " + hd.TenKH).Bold().Underline();
                col.Item().Text(t => { t.Span("Số điện thoại: "); t.Span(hd.SDT ?? "").Bold(); });
                col.Item().Text(t =>
                {
                    t.Span("Số CMND: "); t.Span(hd.CCCD ?? "").Bold();
                    t.Span("     Ngày cấp: "); t.Span(hd.NgayCapCCCD ?? "").Bold();
                    t.Span("     Nơi cấp: "); t.Span(hd.NoiCapCCCD ?? "").Bold();
                });
                col.Item().Text(t => { t.Span("Địa chỉ: "); t.Span(hd.DiaChi ?? "").Bold(); });

                // TÀI SẢN
                col.Item().PaddingTop(10).Text("THÔNG TIN TÀI SẢN & GIẤY TỜ KÈM THEO").Bold().Underline();
                col.Item().Text(t =>
                {
                    t.Span("1. Loại tài sản: "); t.Span(GetTenLoaiTaiSan(hd.LoaiTaiSanID)).Bold();
                    t.Span("     Chi tiết tài sản: "); t.Span(hd.TenTaiSan ?? "").Bold();
                });
                col.Item().Text(t =>
                {
                    string soTien = $"{hd.TienVay:n0}";
                    t.Span("Số tiền vay: "); t.Span(soTien).Bold();
                    t.Span(" (VNĐ) (Bằng chữ: "); t.Span(NumberTextToVietnameseWords(soTien)).Bold(); t.Span(")");
                });
                col.Item().Text(t =>
                {
                    t.Span("Thời hạn vay: từ ngày: "); t.Span(FormatDate(hd.NgayVay)).Bold();
                    t.Span(" đến ngày: "); t.Span(FormatDate(hd.NgayHetHan)).Bold();
                });

                // CAM KẾT
                col.Item().PaddingTop(10).Text("CAM KẾT CỦA NGƯỜI CẦM ĐỒ :").Bold().Underline();
                col.Item().Text("1. Tự nguyện chi trả lãi phí : 1,5%/tháng + phí thẩm định, quản lý, kho bãi (tổng 3% - 6%).");
                col.Item().Text("2. Tôi cam kết tài sản thuộc quyền sở hữu hợp pháp của tôi và các giấy tờ đã xuất trình là bản gốc do các cơ quan quản lý nhà nước cấp. Nếu sai, tôi hoàn toàn chịu trách nhiệm trước Pháp luật.");
                col.Item().Text("3. Tôi cam kết trả gốc và lệ phí đúng hạn. Hết thời hạn trên, tôi không đến chuộc lại tài sản hoặc trả lệ phí để kéo dài thêm thời hạn thì Tài sản trên sẽ thuộc quyền sở hữu của Bên cho vay, mà bên cho vay không có nghĩa vụ thông báo với Bên vay. Lúc đó, Hợp đồng này có giá trị như giấy bán tài sản của tôi. Bên cho vay được toàn quyền thanh lý để thu hồi vốn và số tiền nhận được từ việc thanh lý. Bên cho vay được hưởng toàn bộ.");
                col.Item().Text("4. Tôi thực hiện việc lập Hợp đồng này trong trạng thái tinh thần hoàn toàn minh mẫn, đã đọc kỹ và hiểu toàn bộ trách nhiệm và nghĩa vụ trả nợ vay số tiền trên. Tôi cam kết thực hiện tất cả các nội dung trong Hợp đồng này, ký tên và điểm chỉ dưới đây để làm bằng chứng.");

                // CHỮ KÝ
                // Dòng trên: Đại diện bên cầm đồ & Người cầm đồ
                col.Item().PaddingTop(15).Row(row =>
                {
                    row.RelativeItem().AlignLeft().PaddingLeft(80).Text("ĐẠI DIỆN BÊN CẦM ĐỒ").Bold();
                    row.RelativeItem().AlignRight().PaddingRight(34).Text("NGƯỜI CẦM ĐỒ").Bold();
                });

                // Dòng dưới: chữ ký
                col.Item().PaddingTop(10).Row(row =>
                {
                    row.RelativeItem().AlignLeft().PaddingLeft(30).Text("Thẩm định"); // thêm PaddingLeft
                    row.RelativeItem().AlignCenter().Text("Trưởng PGDTT");
                    row.RelativeItem().AlignRight().Text("(Ký, ghi rõ họ tên và điểm chỉ)");
                });





                col.Item().PaddingTop(70).AlignCenter().Text(tiem.DaiDien ?? "").Bold();
            });

            page.Footer().AlignCenter().Text(text =>
            {
                text.DefaultTextStyle(x => x.FontSize(10).Italic());
                text.CurrentPageNumber();
                text.Span("/"); text.TotalPages(); text.Span(" trang");
            });
        });
    }

    // ========== Helpers ==========
    private static string FormatDate(string? input)
    {
        if (DateTime.TryParseExact(input, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
            return date.ToString("dd/MM/yyyy");
        return input ?? "";
    }

    private static readonly Dictionary<int, string> LoaiTaiSanDictionary = new()
    {
        { 1, "XE MÁY" }, { 2, "Ô TÔ" }, { 3, "ĐIỆN THOẠI" }, { 4, "LAPTOP" },
        { 5, "VÀNG" }, { 6, "CAVET" }, { 7, "SỔ ĐỎ - NHÀ ĐẤT" }, { 8, "KHÁC" }
    };

    private static string GetTenLoaiTaiSan(int? id)
    {
        return id.HasValue && LoaiTaiSanDictionary.TryGetValue(id.Value, out var ten) ? ten : "KHÔNG RÕ";
    }

    public static string NumberTextToVietnameseWords(string text)
    {
        if (decimal.TryParse(text.Replace(",", ""), out decimal number))
            return NumberToVietnameseWords(number);
        return "Không rõ số tiền";
    }

    public static string NumberToVietnameseWords(decimal amount)
    {
        long integerPart = (long)amount;
        string result = NumberToVietnameseWords_Long(integerPart);
        return char.ToUpper(result[0]) + result[1..].Trim() + " đồng";
    }

    public static string NumberToVietnameseWords_Long(long number)
    {
        if (number == 0) return "không";

        string[] dv = { "", " nghìn", " triệu", " tỷ" };
        string result = "";
        int i = 0;

        while (number > 0)
        {
            int block = (int)(number % 1000);
            bool needZeroHundreds = (i > 0 && block < 100 && result != "");
            if (block != 0)
            {
                string blockText = ReadBlock(block, needZeroHundreds);
                result = blockText + dv[i] + " " + result;
            }
            number /= 1000;
            i++;
        }

        return result.Trim();
    }

    private static string ReadBlock(int number, bool addZeroHundreds)
    {
        string[] cs = { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
        int hundreds = number / 100;
        int tens = (number % 100) / 10;
        int units = number % 10;

        string result = "";

        if (hundreds > 0)
            result += cs[hundreds] + " trăm";
        else if (addZeroHundreds)
            result += "không trăm";

        if (tens > 1)
        {
            result += " " + cs[tens] + " mươi";
            if (units == 1) result += " mốt";
            else if (units == 5) result += " lăm";
            else if (units > 0) result += " " + cs[units];
        }
        else if (tens == 1)
        {
            result += " mười";
            if (units == 1) result += " một";
            else if (units == 5) result += " lăm";
            else if (units > 0) result += " " + cs[units];
        }
        else if (tens == 0 && units > 0)
        {
            if (hundreds > 0 || addZeroHundreds)
                result += " lẻ " + cs[units];
            else
                result += cs[units];
        }

        return result.Trim();
    }
}
