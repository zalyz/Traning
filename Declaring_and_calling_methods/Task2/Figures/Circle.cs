using System;

namespace Task2.Figures
{
    /// <summary>
    /// Represents a circle.
    /// </summary>
    public class Circle : Figure
    {
        public override int[] SideLenghts { get; } = new int[1];

        public Circle(int radius)
        {
            if (radius <= 0)
                throw new ArgumentException("Parameters can't be less than or equal to zero.");

            SideLenghts[0] = radius;
        }

        /// <inheritdoc/>
        public override double GetFigureArea()
        {
            return Math.PI * Math.Pow(SideLenghts[0], 2);
        }

        /// <inheritdoc/>
        public override double GetFigurePerimeter()
        {
            return 2 * Math.PI * SideLenghts[0];
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return "Circle: " + SideLenghts[0];
        }
    }
}
