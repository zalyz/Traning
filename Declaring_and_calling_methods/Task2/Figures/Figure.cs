using System;
using System.Linq;

namespace Task2
{
    /// <summary>
    /// Represents geometric figures.
    /// </summary>
    public abstract class Figure
    {
        public abstract int[] SideLenghts { get; }

        /// <summary>
        /// Calculates the area of the figure.
        /// </summary>
        /// <returns> Area of the figure.</returns>
        public abstract double GetFigureArea();

        /// <summary>
        /// Calculates the perimeter of the figure.
        /// </summary>
        /// <returns> Perimeter of the figure.</returns>
        public abstract double GetFigurePerimeter();

        /// <inheritdoc/>
        public override int GetHashCode() => base.GetHashCode() + 1;

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj is Figure)
            {
                return SideLenghts.OrderBy(e => e).SequenceEqual(
                    (obj as Figure).SideLenghts.OrderBy(e => e));
            }

            return false;
        }
    }
}
