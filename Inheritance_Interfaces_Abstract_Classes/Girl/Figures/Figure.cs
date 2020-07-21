using System;
using System.Collections.Generic;
using System.Text;

namespace Girl.Figures
{
    public enum FigureColor
    {
        Transparent,
        White,
        Red,
        Blue,
        Yellow,
        Green
    }

    public enum FigureMaterial
    {
        Film = 1,
        Paper = 2
    }

    [Serializable]
    public abstract class Figure
    {
        public abstract double[] SidesLength { get; }

        public FigureColor Color { get; private set; }

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

        public Figure(Figure figure)
        {
            Color = figure.Color;
        }

        public void PaintFigureTo(FigureColor color)
        {
            if ((Color != FigureColor.Transparent) && (Color == FigureColor.White))
            {
                Color = color;
            }
            else
                throw new ArgumentException("Can't paint film figure.");
        }

        public abstract double Area();

        public abstract double Perimeter();
    }
}
