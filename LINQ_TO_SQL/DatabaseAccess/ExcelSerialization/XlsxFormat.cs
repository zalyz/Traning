using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;

namespace DatabaseAccess.ExcelSerialization
{
    /// <summary>
    /// Saves an object to the .xlsx file.
    /// </summary>
    /// <typeparam name="T">Class for formatting</typeparam>
    public class XlsxFormat<T> : IFormat<T>
    {
        /// <summary>
        /// Path of the xlsx file.
        /// </summary>
        private string _filePath;

        /// <summary>
        /// Create instance of XlsxFormat class.
        /// </summary>
        /// <param name="filePath">File path to save.</param>
        public XlsxFormat(string filePath)
        {
            _filePath = filePath;
        }

        /// <inheritdoc/>
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

        /// <summary>
        /// Sets row values of the table.
        /// </summary>
        /// <param name="values">Collection of the values.</param>
        /// <param name="worksheet">Instance of Exel worksheet.</param>
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

        /// <summary>
        /// Sets Column names of the table.
        /// </summary>
        /// <param name="columnNames">Collection of the column names.</param>
        /// <param name="worksheet">Instance of Exel worksheet.</param>
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