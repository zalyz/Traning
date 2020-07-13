using System;

namespace Task2.Figures
{
    /// <summary>
    /// Represents a rectangle.
    /// </summary>
    public class Triangle : Figure
    {
        public override int[] SideLenghts { get; } = new int[3];

        public Triangle(int firstSide, int secondSide, int thirdSide)
        {
            if (firstSide <= 0 || secondSide <= 0 || thirdSide <= 0)
                throw new ArgumentException("Parameters can't be less than or equal to zero.");

            SideLenghts[0] = firstSide;
            SideLenghts[1] = secondSide;
            SideLenghts[2] = thirdSide;
        }

        /// <inheritdoc/>
        public override double GetFigureArea()
        {
            var halfMeter = GetFigurePerimeter() / 2;
            return Math.Sqrt(halfMeter * (halfMeter - SideLenghts[0]) * (halfMeter - SideLenghts[1]) * (halfMeter - SideLenghts[2]));
        }

        /// <inheritdoc/>
        public override double GetFigurePerimeter()
        {
            return SideLenghts[0] + SideLenghts[1] + SideLenghts[2];
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return "Triangle: " + SideLenghts[0] + " " + SideLenghts[1] + " " + SideLenghts[2];
        }
    }
}
