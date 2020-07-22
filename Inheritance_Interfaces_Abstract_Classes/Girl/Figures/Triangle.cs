using System;
using System.Linq;
using System.Text;

namespace Girl.Figures
{
    /// <summary>
    /// Represent essence of geometric triangle.
    /// </summary>
    public class Triangle : Figure
    {
        /// <inheritdoc/>
        public override double[] SidesLength { get; } = new double[3];

        /// <summary>
        /// Creates instance of Triangle Class.
        /// </summary>
        /// <param name="figureMaterial"> Material of triangle.</param>
        /// <param name="firstSide"> The first figure side.</param>
        /// <param name="secondSide"> The second figure side.</param>
        /// <param name="thirdSide"> The third figure side.</param>
        public Triangle(FigureMaterial figureMaterial, double firstSide, double secondSide, double thirdSide) : base(figureMaterial)
        {
            SidesLength[0] = firstSide;
            SidesLength[1] = secondSide;
            SidesLength[2] = thirdSide;
        }

        /// <summary>
        /// Creates an instance of the Triangle class with an area less than that of the passed figure.
        /// </summary>
        /// <param name="figure"> Figure to check.</param>
        /// <param name="firstSide"> The first figure side.</param>
        /// <param name="secondSide"> The second figure side.</param>
        /// <param name="thirdSide"> The third figure side.</param>
        /// <exception cref="ArgumentException"> Throws if figure area is more zen area of new figure.</exception>
        public Triangle(Figure figure, double firstSide, double secondSide, double thirdSide) : base(figure)
        {
            var halfPerimeter = (firstSide + secondSide + thirdSide) / 2;
            var areaOfNewFigure = Math.Sqrt(halfPerimeter *(halfPerimeter - firstSide) *
                                                           (halfPerimeter - secondSide) *
                                                           (halfPerimeter - thirdSide));
            if (areaOfNewFigure < figure.Area())
            {
                SidesLength[0] = firstSide;
                SidesLength[1] = secondSide;
                SidesLength[2] = thirdSide;
            }
            else
            {
                throw new ArgumentException("Can't cut a Triangle with these parameters.");
            }
        }

        /// <inheritdoc/>
        public override double Area()
        {
            var halfPerimeter = Perimeter() / 2;
            return Math.Sqrt(halfPerimeter * (halfPerimeter - SidesLength[0]) *
                                             (halfPerimeter - SidesLength[1]) *
                                             (halfPerimeter - SidesLength[2]));
        }

        /// <inheritdoc/>
        public override double Perimeter()
        {
            return SidesLength[0] + SidesLength[1] + SidesLength[2];
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return Equals(obj as Triangle);
        }

        /// <summary>
        /// Determaines whether the passed triangle is equal to the current.
        /// </summary>
        /// <param name="triangle"> Triangle to check.</param>
        /// <returns> True if Triangles are equal, False otherwise.</returns>
        public bool Equals(Triangle triangle)
        {
            return triangle != null &&
                Color == triangle.Color &&
                SidesLength.OrderBy(e => e).SequenceEqual(
                      triangle.SidesLength.OrderBy(e => e));
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return (Color, SidesLength[0], SidesLength[1], SidesLength[2]).GetHashCode();
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            var stringFormatOfSidesLength = new StringBuilder();
            foreach (var side in SidesLength)
            {
                stringFormatOfSidesLength.Append(" " + side);
            }
            return "Triangle: " + stringFormatOfSidesLength.ToString() + ", Color: " + Color;
        }
    }
}
