using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseAccess.ExcelSerialization
{
    interface IFormat<T>
    {
        public void SaveToFile(string[] columnNames, IEnumerable<T> rowValues);
    }
}
