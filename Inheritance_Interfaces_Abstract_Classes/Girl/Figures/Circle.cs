using System;
using System.Linq;

namespace Girl.Figures
{
    /// <summary>
    /// Represent essence of geometric circle.
    /// </summary>
    public class Circle : Figure
    {
        /// <inheritdoc/>
        public override double[] SidesLength { get; } = new double[1];

        /// <summary>
        /// Creates instance of Circle Class.
        /// </summary>
        /// <param name="figureMaterial"> Material of Circle.</param>
        /// <param name="radius"> Circle radius.</param>
        public Circle(FigureMaterial figureMaterial, double radius) : base(figureMaterial)
        {
            SidesLength[0] = radius;
        }

        /// <summary>
        /// Creates an instance of the Circle class with an area less than that of the passed figure.
        /// </summary>
        /// <param name="figure"> Figure to check.</param>
        /// <param name="radius"> Circle radius.</param>
        /// <exception cref="ArgumentException"> Throws if figure area is more zen area of new figure.</exception>
        public Circle(Figure figure, double radius) : base(figure)
        {
            var arearOfNewCircle = Math.PI * Math.Pow(radius, 2);
            if (arearOfNewCircle < figure.Area())
            {
                SidesLength[0] = radius;
            }
            else
            {
                throw new ArgumentException("Can't cut a Circle with these parameters.");
            }
        }

        /// <inheritdoc/>
        public override double Area()
        {
            return Math.PI * Math.Pow(SidesLength[0], 2);
        }

        /// <inheritdoc/>
        public override double Perimeter()
        {
            return 2 * Math.PI * SidesLength[0];
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return Equals(obj as Circle);
        }

        /// <summary>
        /// Determaines whether the passed circle is equal to the current.
        /// </summary>
        /// <param name="circle"> Circle to check.</param>
        /// <returns> True if Circles are equal, False otherwise.</returns>
        public bool Equals(Circle circle)
        {
            return circle != null &&
                Color == circle.Color &&
                SidesLength.OrderBy(e => e).SequenceEqual(
                      circle.SidesLength.OrderBy(e => e));
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return (Color, SidesLength[0]).GetHashCode();
        }
        
        /// <inheritdoc/>
        public override string ToString()
        {
            return "Circle: " + SidesLength[0] + ", Color: " + Color;
        }
    }
}
