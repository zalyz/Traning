using System;
using Task2.Figures;

namespace Task2.Factory
{
    /// <summary>
    /// Represents Factory thet create geometric figures.
    /// </summary>
    public class FigureFactory
    {
        /// <summary>
        /// Create a geometric shape.
        /// </summary>
        /// <param name="lineFromFile"> A string that contains information about the figure.</param>
        /// <returns> A specific geometric figure.</returns>
        public static Figure CreateFigure(string lineFromFile)
        {
            if (string.IsNullOrWhiteSpace(lineFromFile))
                throw new ArgumentException("An empty line was read.");

            var splitedLineFromFile = lineFromFile.Split(' ');
            return splitedLineFromFile[0].ToUpper() switch
            {
                "CIRCLE" => GetCircle(splitedLineFromFile),
                "QUADRANT" => GetQuadrant(splitedLineFromFile),
                "RECTANGLE" => GetRectangle(splitedLineFromFile),
                "TRIANGLE" => GetTriangle(splitedLineFromFile),
                _ => throw new ArgumentException($"String '{lineFromFile}' has an invalid format."),
            };
        }

        /// <summary>
        /// Create instance of the Circle class.
        /// </summary>
        /// <param name="splitedDataFromFile"> A array that contains splited information about the figure.</param>
        /// <returns> Instance of the Circle class.</returns>
        private static Circle GetCircle(string[] splitedDataFromFile)
        {
            var radius = int.Parse(splitedDataFromFile[1]);
            return new Circle(radius);
        }

        /// <summary>
        /// Create instance of the Quadrant class.
        /// </summary>
        /// <param name="splitedDataFromFile"> A array that contains splited information about the figure.</param>
        /// <returns> Instance of the Quadrant class.</returns>
        private static Quadrant GetQuadrant(string[] splitedDataFromFile)
        {
            var radius = int.Parse(splitedDataFromFile[1]);
            return new Quadrant(radius);
        }

        /// <summary>
        /// Create instance of the Rectangle class.
        /// </summary>
        /// <param name="splitedDataFromFile"> A array that contains splited information about the figure.</param>
        /// <returns> Instance of the Rectangle class.</returns>
        private static Rectangle GetRectangle(string[] splitedDataFromFile)
        {
            var firstSide = int.Parse(splitedDataFromFile[1]);
            var secondSide = int.Parse(splitedDataFromFile[2]);
            return new Rectangle(firstSide, secondSide);
        }

        /// <summary>
        /// Create instance of the Triangle class.
        /// </summary>
        /// <param name="splitedDataFromFile"> A array that contains splited information about the figure.</param>
        /// <returns> Instance of the Triangle class.</returns>
        private static Triangle GetTriangle(string[] splitedDataFromFile)
        {
            var firstSide = int.Parse(splitedDataFromFile[1]);
            var secondSide = int.Parse(splitedDataFromFile[2]);
            var thirdSide = int.Parse(splitedDataFromFile[3]);
            return new Triangle(firstSide, secondSide, thirdSide);
        }
    }
}
