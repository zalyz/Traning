using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DatabaseAccess.ExcelSerialization
{
    public class XlsxFormat<T> : IFormat<T>
    {
        private string _filePath;

        public XlsxFormat(string filePath)
        {
            _filePath = filePath;
        }

        public void SaveToFile(string[] columnNames, IEnumerable<T> rowValues)
        {
            ExcelPackage ExcelPkg = new ExcelPackage();
            ExcelWorksheet worksheet = ExcelPkg.Workbook.Worksheets.Add("Sheet1");
            SetColumnNames(columnNames, worksheet);
            SetRowValues(rowValues, worksheet);
            worksheet.Protection.IsProtected = false;
            worksheet.Protection.AllowSelectLockedCells = false;
            ExcelPkg.SaveAs(new FileInfo(_filePath));
        }

        private void SetRowValues(IEnumerable<T> values, ExcelWorksheet worksheet)
        {
            var fields = typeof(T).GetFields();
            var row = 2;
            foreach (var item in values)
            {
                var column = 1;
                for (int index = 0; index < fields.Length; index++)
                {
                    using (var Rng = worksheet.Cells[row, column])
                    {
                        Rng.Value = fields[index].GetValue(item);
                    }

                    column++;
                }

                row++;
            }
        }

        private void SetColumnNames(string[] columnNames, ExcelWorksheet worksheet)
        {
            for (int index = 0; index < columnNames.Length; index++)
            {
                using (var Rng = worksheet.Cells[1, index + 1])
                {
                    Rng.Value = columnNames[index];
                }
            }
        }
    }
}
