using System;

namespace Task2.Figures
{
    /// <summary>
    /// Represents a rectangle.
    /// </summary>
    public class Rectangle : Figure
    {
        public override int[] SideLenghts { get; } = new int[2];

        public Rectangle(int firstSide, int secondSide)
        {
            if (firstSide <= 0 || secondSide <= 0)
                throw new ArgumentException("Parameters can't be less than or equal to zero.");

            SideLenghts[0] = firstSide;
            SideLenghts[1] = secondSide;
        }

        /// <inheritdoc/>
        public override double GetFigureArea()
        {
            return SideLenghts[0] * SideLenghts[1];
        }

        /// <inheritdoc/>
        public override double GetFigurePerimeter()
        {
            return (SideLenghts[0] + SideLenghts[1]) * 2;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return "Rectangle: " + SideLenghts[0] + " " + SideLenghts[1];
        }
    }
}
