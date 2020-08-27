using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseAccess.ExcelSerialization
{
    /// <summary>
    /// Defines method for saving object in different formats.
    /// </summary>
    /// <typeparam name="T">Class for formatting.</typeparam>
    interface IFormat<T>
    {
        /// <summary>
        /// Saves an objects to the file in table form.
        /// </summary>
        /// <param name="columnNames">Column names</param>
        /// <param name="rowValues">Column values.</param>
        public void SaveToFile(string[] columnNames, IEnumerable<T> rowValues);
    }
}
