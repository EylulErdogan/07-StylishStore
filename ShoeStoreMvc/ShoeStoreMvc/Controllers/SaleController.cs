using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using ShoeStoreMvc.Models;

namespace ShoeStoreMvc.Controllers
{
    public class SaleController : Controller
    {
        private readonly string apiUrl = "https://localhost:7186/api/Sales/GetSales";

        public IActionResult Index()
        {
            HttpClient client = new HttpClient();

            var response = client.GetAsync(apiUrl).Result;

            List<Sale> sales = new List<Sale>();

            if (response.IsSuccessStatusCode)
            {
                var jsonData = response.Content.ReadAsStringAsync().Result;
                sales = JsonConvert.DeserializeObject<List<Sale>>(jsonData);
            }

            return View(sales);
        }

        public IActionResult ExportToPdf()
        {
            HttpClient client = new HttpClient();
            var response = client.GetAsync(apiUrl).Result;

            var jsonData = response.Content.ReadAsStringAsync().Result;
            var sales = JsonConvert.DeserializeObject<List<Sale>>(jsonData);

            var pdfDocument = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(10).FontFamily("Arial"));

                    page.Header()
                        .Text("ShoeStore Satış Raporu")
                        .SemiBold()
                        .FontSize(20)
                        .FontColor(Colors.Blue.Medium);

                    page.Content()
                        .PaddingTop(1, Unit.Centimetre)
                        .Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(35);
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.ConstantColumn(45);
                                columns.ConstantColumn(70);
                                columns.ConstantColumn(75);
                                columns.ConstantColumn(90);
                            });

                            table.Header(header =>
                            {
                                header.Cell().Background(Colors.Grey.Lighten2).Padding(5).Text("ID").Bold();
                                header.Cell().Background(Colors.Grey.Lighten2).Padding(5).Text("Ürün").Bold();
                                header.Cell().Background(Colors.Grey.Lighten2).Padding(5).Text("Marka").Bold();
                                header.Cell().Background(Colors.Grey.Lighten2).Padding(5).Text("Adet").Bold();
                                header.Cell().Background(Colors.Grey.Lighten2).Padding(5).Text("Fiyat").Bold();
                                header.Cell().Background(Colors.Grey.Lighten2).Padding(5).Text("Toplam").Bold();
                                header.Cell().Background(Colors.Grey.Lighten2).Padding(5).Text("Tarih").Bold();
                            });

                            foreach (var item in sales)
                            {
                                table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(5).Text(item.Id.ToString());
                                table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(5).Text(item.ShoeName);
                                table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(5).Text(item.BrandName);
                                table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(5).Text(item.Quantity.ToString());
                                table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(5).Text($"{item.Price} TL");
                                table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(5).Text($"{item.TotalPrice} TL");
                                table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(5).Text(item.SaleDate.ToString("dd.MM.yyyy HH:mm"));
                            }
                        });

                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Sayfa ");
                            x.CurrentPageNumber();
                        });
                });
            });

            var pdfBytes = pdfDocument.GeneratePdf();

            return File(pdfBytes, "application/pdf", $"Satis_Raporu_{DateTime.Now:yyyyMMdd}.pdf");
        }

        public IActionResult ExportToExcel()
        {
            ExcelPackage.License.SetNonCommercialPersonal("ShoeStore");

            HttpClient client = new HttpClient();
            var response = client.GetAsync(apiUrl).Result;

            var jsonData = response.Content.ReadAsStringAsync().Result;
            var sales = JsonConvert.DeserializeObject<List<Sale>>(jsonData);

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Satış Raporu");

                worksheet.Cells[1, 1].Value = "ID";
                worksheet.Cells[1, 2].Value = "Ürün Adı";
                worksheet.Cells[1, 3].Value = "Marka";
                worksheet.Cells[1, 4].Value = "Adet";
                worksheet.Cells[1, 5].Value = "Birim Fiyat";
                worksheet.Cells[1, 6].Value = "Toplam Fiyat";
                worksheet.Cells[1, 7].Value = "Satış Tarihi";

                using (var range = worksheet.Cells[1, 1, 1, 7])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(17, 24, 39));
                    range.Style.Font.Color.SetColor(System.Drawing.Color.White);
                    range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }

                int rowNumber = 2;

                foreach (var item in sales)
                {
                    worksheet.Cells[rowNumber, 1].Value = item.Id;
                    worksheet.Cells[rowNumber, 2].Value = item.ShoeName;
                    worksheet.Cells[rowNumber, 3].Value = item.BrandName;
                    worksheet.Cells[rowNumber, 4].Value = item.Quantity;
                    worksheet.Cells[rowNumber, 5].Value = item.Price;
                    worksheet.Cells[rowNumber, 6].Value = item.TotalPrice;
                    worksheet.Cells[rowNumber, 7].Value = item.SaleDate.ToString("dd.MM.yyyy HH:mm");

                    rowNumber++;
                }

                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                var fileBytes = package.GetAsByteArray();

                return File(
                    fileBytes,
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    $"Satis_Raporu_{DateTime.Now:yyyyMMdd}.xlsx"
                );
            }
        }
    }
}