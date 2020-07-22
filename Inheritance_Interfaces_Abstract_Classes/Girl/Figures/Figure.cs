using System;

namespace Girl.Figures
{
    /// <summary>
    /// Contains figures colors.
    /// </summary>
    public enum FigureColor
    {
        Transparent,
        White,
        Red,
        Blue,
        Yellow,
        Green
    }

    /// <summary>
    /// Contains figures materials.
    /// </summary>
    public enum FigureMaterial
    {
        Film = 1,
        Paper = 2
    }

    /// <summary>
    /// Represents the essence of a geometric figure.
    /// </summary>
    public abstract class Figure
    {
        /// <summary>
        /// Gets length of figure sides.
        /// </summary>
        public abstract double[] SidesLength { get; }

        /// <summary>
        /// Gets or sets figure color.
        /// </summary>
        public FigureColor Color { get; private set; }

        /// <summary>
        /// Sets the shape color depending on the material.
        /// </summary>
        /// <param name="figureMaterial"></param>
        public Figure(FigureMaterial figureMaterial)
        {
            if (figureMaterial == FigureMaterial.Film)
            {
                Color = FigureColor.Transparent;
            }
            else
            {
                Color = FigureColor.White;
            }
        }

        /// <summary>
        /// Sets the figure color depending on the color of the passed figure.
        /// </summary>
        /// <param name="figure"> Figure to cut.</param>
        public Figure(Figure figure)
        {
            Color = figure.Color;
        }

        /// <summary>
        /// Paint figure.
        /// </summary>
        /// <param name="color"> The color to paint.</param>
        public void PaintFigureTo(FigureColor color)
        {
            if ((Color != FigureColor.Transparent) && (Color == FigureColor.White))
            {
                Color = color;
            }
            else
                throw new ArgumentException("Can't paint film figure.");
        }

        /// <summary>
        /// Calculate figure area.
        /// </summary>
        /// <returns> Figure arear.</returns>
        public abstract double Area();

        /// <summary>
        /// Calculate figure perimeter.
        /// </summary>
        /// <returns> Figure perimeter</returns>
        public abstract double Perimeter();
    }
}
