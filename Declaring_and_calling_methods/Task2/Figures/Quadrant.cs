using System;

namespace Task2.Figures
{
    /// <summary>
    /// Represents a quadrant.
    /// </summary>
    public class Quadrant : Figure
    {
        public override int[] SideLenghts { get; } = new int[1];

        public Quadrant(int radius)
        {
            if (radius <= 0)
                throw new ArgumentException("Parameters can't be less than or equal to zero.");

            SideLenghts[0] = radius;
        }

        /// <inheritdoc/>
        public override double GetFigureArea()
        {
            return (Math.PI * Math.Pow(SideLenghts[0], 2)) / 4;
        }

        /// <inheritdoc/>
        public override double GetFigurePerimeter()
        {
            return SideLenghts[0] * 2 + (2 * Math.PI * SideLenghts[0] / 4);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return "Quadrant: " + SideLenghts[0];
        }
    }
}
