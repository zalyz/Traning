using System;
using System.IO;
using Task2.Factory;

namespace Task2
{
    /// <summary>
    /// Allows reading data from a file.
    /// </summary>
    public static class DataReader
    {
        /// <summary>
        /// Reads figure data from a file.
        /// </summary>
        /// <param name="path"> File path.</param>
        /// <returns> Array of geometric figures.</returns>
        public static Figure[] ReadFiguresFrom(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException("File path is not correct.");

            var arrayOfFigures = new Figure[0];
            using (var reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    Array.Resize(ref arrayOfFigures, arrayOfFigures.Length + 1);
                    var lineFromFile = reader.ReadLine();
                    arrayOfFigures[^1] = FigureFactory.CreateFigure(lineFromFile);
                }
            }

            return arrayOfFigures;
        }
    }
}
