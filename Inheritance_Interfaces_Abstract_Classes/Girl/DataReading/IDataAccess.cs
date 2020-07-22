using System;
using System.Collections.Generic;
using System.Text;

namespace Girl.DataReading
{
    /// <summary>
    /// Defines methods to read or write to file.
    /// </summary>
    /// <typeparam name="T"> Any class.</typeparam>
    public interface IDataAccess<T> where T : class
    {
        /// <summary>
        /// Read instance from file.
        /// </summary>
        /// <param name="path"> The Path to file.</param>
        /// <returns> Array of instance.</returns>
        public T[] ReadData(string path);

        /// <summary>
        /// Write array of instance to file.
        /// </summary>
        /// <param name="source"> Array of instance.</param>
        /// <param name="path"> The Path to file.</param>
        public void WriteData(T[] source, string path);
    }
}
