using Ecommerce.Core.DTOs.Excels;
using Syncfusion.Drawing;
using Syncfusion.XlsIO;

namespace Ecommerce.Core.Extensions
{
    public static class ExportExcelDetail
    {
        public static MemoryStream ExportOrderDetail(ReportExcelDTO data)
        {
            //Create an instance of ExcelEngine
            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                IApplication application = excelEngine.Excel;
                application.DefaultVersion = ExcelVersion.Xlsx;

                //Create a workbook
                IWorkbook workbook = application.Workbooks.Create(1);
                IWorksheet worksheet = workbook.Worksheets[0];

                //Adding a picture
                //FileStream imageStream = new FileStream("AdventureCycles-Logo.png", FileMode.Open, FileAccess.Read);
                //IPictureShape shape = worksheet.Pictures.AddPicture(1, 1, imageStream, 20, 20);

                //Disable gridlines in the worksheet
                worksheet.IsGridLinesVisible = false;

                //Enter values to the cells from A3 to A5
                worksheet.Range["A3"].Text = data.Address;
                worksheet.Range["A4"].Text = data.City + ", " + data.Country;
                worksheet.Range["A5"].Text = "Phone: " + data.PhoneNumber;

                //Make the text bold
                worksheet.Range["A3:A5"].CellStyle.Font.Bold = true;

                //Merge cells
                worksheet.Range["D1:E1"].Merge();

                //Enter text to the cell D1 and apply formatting.
                worksheet.Range["D1"].Text = "INVOICE";
                worksheet.Range["D1"].CellStyle.Font.Bold = true;
                worksheet.Range["D1"].CellStyle.Font.RGBColor = Color.FromArgb(42, 118, 189);
                worksheet.Range["D1"].CellStyle.Font.Size = 35;

                //Apply alignment in the cell D1
                worksheet.Range["D1"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignRight;
                worksheet.Range["D1"].CellStyle.VerticalAlignment = ExcelVAlign.VAlignTop;

                //Enter values to the cells from D5 to E8
                worksheet.Range["D5"].Text = "INVOICE#";
                worksheet.Range["E5"].Text = "DATE";
                worksheet.Range["D6"].Text = data.InvoiceId;
                worksheet.Range["E6"].Value = data.CreatedDate;

                //Apply RGB backcolor to the cells from D5 to E8
                worksheet.Range["D5:E5"].CellStyle.Color = Color.FromArgb(42, 118, 189);
                worksheet.Range["D7:E7"].CellStyle.Color = Color.FromArgb(42, 118, 189);

                //Apply known colors to the text in cells D5 to E8
                worksheet.Range["D5:E5"].CellStyle.Font.Color = ExcelKnownColors.White;
                worksheet.Range["D7:E7"].CellStyle.Font.Color = ExcelKnownColors.White;

                //Make the text as bold from D5 to E8
                worksheet.Range["D5:E8"].CellStyle.Font.Bold = true;

                //Apply alignment to the cells from D5 to E8
                worksheet.Range["D5:E8"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
                worksheet.Range["D5:E5"].CellStyle.VerticalAlignment = ExcelVAlign.VAlignCenter;
                worksheet.Range["D7:E7"].CellStyle.VerticalAlignment = ExcelVAlign.VAlignCenter;
                worksheet.Range["D6:E6"].CellStyle.VerticalAlignment = ExcelVAlign.VAlignCenter;

                //Enter value and applying formatting in the cell A7
                worksheet.Range["A7"].Text = "  CUSTOMER INFORMATION";
                worksheet.Range["A7"].CellStyle.Color = Color.FromArgb(42, 118, 189);
                worksheet.Range["A7"].CellStyle.Font.Bold = true;
                worksheet.Range["A7"].CellStyle.Font.Color = ExcelKnownColors.White;

                //Apply alignment
                worksheet.Range["A7"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignLeft;
                worksheet.Range["A7"].CellStyle.VerticalAlignment = ExcelVAlign.VAlignCenter;

                //Enter values in the cells A8 to A12
                worksheet.Range["A8"].Text = data.CustomerName;
                worksheet.Range["A9"].Text = data.CustomerAddress;
                worksheet.Range["A10"].Text = data.PhoneNumber;
                worksheet.Range["A11"].Text = data.Country;

                //Create a Hyperlink for e-mail in the cell A13
                IHyperLink hyperlink = worksheet.HyperLinks.Add(worksheet.Range["A12"]);
                hyperlink.Type = ExcelHyperLinkType.Url;
                hyperlink.Address = data.CustomerEmail;
                hyperlink.ScreenTip = "Send Mail";

                //Merge column A and B from row 15 to 22
                worksheet.Range["A15:B15"].Merge();
                worksheet.Range["A16:B16"].Merge();
                worksheet.Range["A17:B17"].Merge();
                worksheet.Range["A18:B18"].Merge();
                worksheet.Range["A19:B19"].Merge();
                worksheet.Range["A20:B20"].Merge();
                worksheet.Range["A21:B21"].Merge();
                worksheet.Range["A22:B22"].Merge();

                //Enter details of products and prices
                worksheet.Range["A15"].Text = "  DESCRIPTION";
                worksheet.Range["C15"].Text = "QUANTITY";
                worksheet.Range["D15"].Text = "UNIT PRICE";
                worksheet.Range["E15"].Text = "AMOUNT";
                int i = 16;
                foreach (var item in data.productDetails)
                {                
                    worksheet.Range[$"A{i}"].Text = item.ProductName;
                    worksheet.Range[$"C{i}"].Number = item.Quantity;
                    worksheet.Range[$"D{i}"].Number = (double)item.UnitPrice;
                    worksheet.Range[$"E{i}"].Number = (double)item.Total;
                    i++;
                }
                worksheet.Range[$"D{i + 1}"].Text = "Total";
                worksheet.Range[$"D{i + 1}"].CellStyle.Font.Bold = true;
                worksheet.Range[$"D{i + 1}"].CellStyle.VerticalAlignment = ExcelVAlign.VAlignCenter;


                //Apply number format
                worksheet.Range[$"D16:E{i}"].NumberFormat = "$0.00";
                worksheet.Range[$"E{i+1}"].NumberFormat = "$0.00";

                //Formula for Sum the total
                worksheet.Range[$"E{i + 1}"].Formula = $"=SUM(E16:E{i})";
                worksheet.Range[$"E{i + 1}"].CellStyle.VerticalAlignment = ExcelVAlign.VAlignCenter;

                //Apply borders
                worksheet.Range[$"A16:E{i + 1}"].CellStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;
                worksheet.Range[$"A16:E{i + 1}"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
                worksheet.Range[$"A16:E{i + 1}"].CellStyle.Borders[ExcelBordersIndex.EdgeTop].Color = ExcelKnownColors.Grey_25_percent;
                worksheet.Range[$"A16:E{i + 1}"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].Color = ExcelKnownColors.Grey_25_percent;
                
                
          
                worksheet.Range["A15:E15"].CellStyle.Font.Color = ExcelKnownColors.White;
                worksheet.Range["A15:E15"].CellStyle.Font.Bold = true;

                //Apply cell color
                worksheet.Range["A15:E15"].CellStyle.Color = Color.FromArgb(42, 118, 189);

                //Apply alignment to cells with product details
                worksheet.Range["A15"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignLeft;
                worksheet.Range["C15:C22"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
                worksheet.Range["D15:E15"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
                worksheet.Range["A15:E15"].CellStyle.VerticalAlignment = ExcelVAlign.VAlignCenter;

                //Apply row height and column width to look good
                worksheet.Range["A1"].ColumnWidth = 36;
                worksheet.Range["B1"].ColumnWidth = 11;
                worksheet.Range["C1"].ColumnWidth = 13;
                worksheet.Range["D1:E1"].ColumnWidth = 18;
                worksheet.Range["A1"].RowHeight = 47;
                worksheet.Range["A2"].RowHeight = 15;
                worksheet.Range["A3:A4"].RowHeight = 15;
                worksheet.Range["A5"].RowHeight = 18;
                worksheet.Range["A6"].RowHeight = 29;
                worksheet.Range["A7"].RowHeight = 18;
                worksheet.Range["A8"].RowHeight = 15;
                worksheet.Range["A9:A14"].RowHeight = 15;
                worksheet.Range["A15:A23"].RowHeight = 18;

                //Saving the Excel to the MemoryStream 
                MemoryStream stream = new MemoryStream();
                workbook.SaveAs(stream);

                //Set the position as '0'.
                stream.Position = 0;
                return stream;
            }
        }
    }
}
