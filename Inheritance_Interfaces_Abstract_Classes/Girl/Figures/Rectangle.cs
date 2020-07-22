using System;
using System.Linq;

namespace Girl.Figures
{
    /// <summary>
    /// Represent essence of geometric rectangle.
    /// </summary>
    public class Rectangle : Figure
    {
        /// <inheritdoc/>
        public override double[] SidesLength { get; } = new double[2];

        /// <summary>
        /// Creates instance of Rectangle Class.
        /// </summary>
        /// <param name="figureMaterial"> Material of rectangle</param>
        /// <param name="firstSide"> The first figure side.</param>
        /// <param name="secondSide"> The second figure side.</param>
        public Rectangle(FigureMaterial figureMaterial, double firstSide, double secondSide) : base(figureMaterial)
        {
            SidesLength[0] = firstSide;
            SidesLength[1] = secondSide;
        }

        /// <summary>
        /// Creates an instance of the Rectangle class with an area less than that of the passed figure.
        /// </summary>
        /// <param name="figure"> Figure to check</param>
        /// <param name="firstSide"> The first side of figure.</param>
        /// <param name="secondSide"> The second side of figure.</param>
        /// <exception cref="ArgumentException"> Throws if figure area is more zen area of new figure.</exception>
        public Rectangle(Figure figure, double firstSide, double secondSide) : base(figure)
        {
            var areaOfNewFigure = firstSide * secondSide;
            if (areaOfNewFigure < figure.Area())
            {
                SidesLength[0] = firstSide;
                SidesLength[1] = secondSide;
            }
            else
            {
                throw new ArgumentException("Can't cut a Triangle with these parameters.");
            }
        }

        /// <inheritdoc/>
        public override double Area()
        {
            return SidesLength[0] * SidesLength[1];
        }

        /// <inheritdoc/>
        public override double Perimeter()
        {
            return (SidesLength[0] + SidesLength[1]) * 2;
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return Equals(obj as Rectangle);
        }

        /// <summary>
        /// Determaines whether the passed rectangle is equal to the current.
        /// </summary>
        /// <param name="rectangle"> Rectangle to check.</param>
        /// <returns> True if Rectangles are equal, False otherwise.</returns>
        public bool Equals(Rectangle rectangle)
        {
            return rectangle != null &&
                Color == rectangle.Color &&
                SidesLength.OrderBy(e => e).SequenceEqual(
                      rectangle.SidesLength.OrderBy(e => e));
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return (Color, SidesLength[0]).GetHashCode();
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return "Rectangle: " + SidesLength[0] + " " + SidesLength[1] + ", Color: " + Color;
        }
    }
}
