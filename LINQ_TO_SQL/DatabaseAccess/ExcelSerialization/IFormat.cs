using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccess
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
        void SaveToFile(string[] columnNames, IEnumerable<T> rowValues);
    }
}
