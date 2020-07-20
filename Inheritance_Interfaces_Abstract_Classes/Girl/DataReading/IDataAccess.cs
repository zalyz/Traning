using System;
using System.Collections.Generic;
using System.Text;

namespace Girl.DataReading
{
    interface IDataAccess<T> where T : class
    {
        public T[] ReadData(string path);

        public void WriteData(T[] source, string path);
    }
}
