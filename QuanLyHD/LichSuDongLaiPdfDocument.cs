using QuanLyVayVon.CSDL;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

public class LichSuDongLaiPdfDocument : IDocument
{
    private readonly string maHD;
    private readonly List<LichSuDongLaiModel> data;

    public LichSuDongLaiPdfDocument(string maHD, List<LichSuDongLaiModel> data)
    {
        this.maHD = maHD;
        this.data = data;
    }

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Size(PageSizes.A4);
            page.Margin(40);
            page.DefaultTextStyle(x => x.FontSize(11));

            page.Content().Column(col =>
            {
              
                // ✅ Bảng dữ liệu
                col.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(50);   // Kỳ
                        columns.RelativeColumn();     // Ngày bắt đầu
                        columns.RelativeColumn();     // Ngày đến hạn
                        columns.RelativeColumn();     // Số tiền
                    });

                    // Header
                    table.Header(header =>
                    {
                        header.Cell().Text("Kỳ").Bold();
                        header.Cell().Text("Bắt đầu").Bold();
                        header.Cell().Text("Đến hạn").Bold();
                        header.Cell().Text("Phải đóng").Bold();
                    });

                    // Dòng dữ liệu
                    foreach (var row in data.OrderBy(x => x.KyThu))
                    {
                        table.Cell().Text(row.KyThu.ToString());
                        table.Cell().Text(row.NgayBatDauKy ?? "");
                        table.Cell().Text(row.NgayDenHan ?? "");
                        table.Cell().Text($"{row.SoTienPhaiDong:N0} VNĐ");
                    }
                });
            });

            // ✅ Footer: số trang động
            page.Footer().AlignCenter().Text(t =>
            {
                t.DefaultTextStyle(x => x.FontSize(10).Italic());
                t.CurrentPageNumber();
                t.Span("/");
                t.TotalPages();
                t.Span(" trang");
            });
        });
    }
}
