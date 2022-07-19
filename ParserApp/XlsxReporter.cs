using System;
using System.Collections.Generic;
using System.Linq;
using Excel = Microsoft.Office.Interop.Excel;

namespace ParserApp
{
    class XlsxReporter : IFormatReporter
    {
        public void CreateReport(List<ModelItem> items)
        {
            var application = new Excel.Application();
            application.SheetsInNewWorkbook = 1;

            Excel.Workbook workbook = application.Workbooks.Add(Type.Missing);

            int startRowIndex = 1;

            Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)application.Sheets[1];
            worksheet.Name = "Информация о статьях";

            worksheet.Cells[startRowIndex, 1] = "Заголовок";
            worksheet.Cells[startRowIndex, 2] = "Ссылка";
            worksheet.Cells[startRowIndex, 3] = "Описание";
            worksheet.Cells[startRowIndex, 4] = "Дата публикации";

            Excel.Range headerRange = worksheet.Range[worksheet.Cells[startRowIndex, 1], worksheet.Cells[startRowIndex, 4]];
            headerRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            headerRange.Font.Bold = true;

            startRowIndex++;

            var categorizedItems = items.OrderBy(p => p.PubDate).GroupBy(p => p.Category);

            foreach (var category in categorizedItems)
            {
                Excel.Range categoryRange = worksheet.Range[worksheet.Cells[startRowIndex, 1], worksheet.Cells[startRowIndex, 4]];
                categoryRange.Merge();
                categoryRange.Value = category.Key;
                categoryRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                categoryRange.Font.Italic = true;

                startRowIndex++;

                foreach (var item in category)
                {
                    worksheet.Cells[startRowIndex, 1] = item.Title;
                    worksheet.Cells[startRowIndex, 2] = item.Link;
                    worksheet.Hyperlinks.Add(worksheet.Cells[startRowIndex, 2], item.Link);
                    worksheet.Cells[startRowIndex, 3] = item.Description;
                    worksheet.Cells[startRowIndex, 4] = item.PubDate;
                    startRowIndex++;
                }

                Excel.Range rangeBorders = worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[startRowIndex - 1, 4]];
                rangeBorders.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle =
                rangeBorders.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle =
                rangeBorders.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle =
                rangeBorders.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle =
                rangeBorders.Borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle =
                rangeBorders.Borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = Excel.XlLineStyle.xlContinuous;

                worksheet.Columns.AutoFit();
            }

            string reportTime = DateTime.Now.ToString().Replace(' ', '_').Replace(':', '.');
            string path = PathConstructor.BuildPath("Reports", $"Report_{reportTime}.xlsx");

            worksheet.SaveAs2(path);
            workbook.Close();
        }
    }
}
