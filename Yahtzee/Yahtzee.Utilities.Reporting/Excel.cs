using ClosedXML.Excel;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee.Utilities.Reporting
{
    public static class Excel
    {
        //data = 2D array of any type
        public static void Export(string filename, string[,] data)
        {
            try
            {
                IXLWorkbook xLWorkbook = new XLWorkbook();
                IXLWorksheet xLWorksheet = xLWorkbook.AddWorksheet("Data");
                int rows = data.GetLength(0);
                int cols = data.GetLength(1);

                PdfWriter writer = new PdfWriter(filename.Substring(0, filename.Length - 4) + "pdf");
                PdfDocument pdf = new PdfDocument(writer);
                Document document = new Document(pdf);
                Paragraph header = new Paragraph("Data")
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetFontSize(20);
                document.Add(header);

                Paragraph subHeader = new Paragraph("Information")
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetFontSize(15);
                document.Add(subHeader);

                Table table = new Table(cols, false);

                for (int iRow = 1; iRow <= rows; iRow++)
                {
                    for (int iCol = 1; iCol <= rows; iCol++)
                    {
                        xLWorksheet.Cell(iRow, iCol).Value = data[iRow - 1, iCol - 1];
                        Cell cell = new Cell(1, 1);
                        cell.Add(new Paragraph(data[iRow - 1, iCol - 1]));

                        table.AddCell(cell);
                    }
                }

                xLWorksheet.Columns().AdjustToContents();

                IXLRange range = xLWorksheet.Range(xLWorksheet.Cell(1, 1).Address,
                                                   xLWorksheet.Cell(rows, cols).Address);

                range.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                range.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                xLWorkbook.SaveAs(filename);
                document.Add(table);
                document.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

