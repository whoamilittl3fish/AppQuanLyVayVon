using QuanLyVayVon.CSDL;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Collections.Generic;
using System.Linq;

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
            page.DefaultTextStyle(x => x.FontSize(13)); // Font chữ to hơn

            page.Content().Column(col =>
            {
                col.Spacing(10);

                // Tiêu đề
                col.Item().Text($"Lịch sử đóng lãi - Mã hợp đồng: {maHD}")
                          .FontSize(16).Bold().AlignCenter();

                // Bảng dữ liệu
                col.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(50);   // Kỳ
                        columns.RelativeColumn();     // Ngày bắt đầu
                        columns.RelativeColumn();     // Ngày đến hạn
                        columns.RelativeColumn();     // Số tiền
                    });

                    // Header trắng
                    table.Header(header =>
                    {
                        header.Cell().Element(CellHeaderStyle).AlignCenter().Text("Kỳ").Bold();
                        header.Cell().Element(CellHeaderStyle).AlignCenter().Text("Bắt đầu").Bold();
                        header.Cell().Element(CellHeaderStyle).AlignCenter().Text("Đến hạn").Bold();
                        header.Cell().Element(CellHeaderStyle).AlignCenter().Text("Phải đóng").Bold();
                    });

                    // Dữ liệu từng dòng (xám -> trắng xen kẽ, bắt đầu là xám)
                    int i = 0;
                    foreach (var row in data.OrderBy(x => x.KyThu))
                    {
                        var bg = i % 2 == 0 ? Colors.Grey.Lighten3 : Colors.White;

                        table.Cell().Element(c => CellStyle(c, bg)).AlignCenter().Text(row.KyThu.ToString());
                        table.Cell().Element(c => CellStyle(c, bg)).AlignCenter().Text(row.NgayBatDauKy ?? "");
                        table.Cell().Element(c => CellStyle(c, bg)).AlignCenter().Text(row.NgayDenHan ?? "");
                        table.Cell().Element(c => CellStyle(c, bg)).AlignRight().Text($"{row.SoTienPhaiDong:N0} VNĐ");

                        i++;
                    }

                    // Style cho ô thường
                    IContainer CellStyle(IContainer container, string backgroundColor) =>
                        container.Background(backgroundColor).Border(1).Padding(5);

                    // Header style: trắng (không cần set màu)
                    IContainer CellHeaderStyle(IContainer container) =>
                        container.Border(1).Padding(5);
                });
            });

            // Footer: số trang
            page.Footer().AlignCenter().Text(t =>
            {
                t.DefaultTextStyle(x => x.FontSize(11).Italic());
                t.CurrentPageNumber();
                t.Span("/");
                t.TotalPages();
                t.Span(" trang");
            });
        });
    }
}
