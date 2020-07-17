using System;
using System.Collections.Generic;
using System.Text;

namespace Girl.Figures
{
    enum FigureColor
    {
        Transparent,
        White,
        Red,
        Blue,
        Yellow,
        Green
    }

    enum FigureMaterial
    {
        Film = 1,
        Paper = 2
    }

    public abstract class Figure
    {
        private bool _isPaintable;

        public abstract double[] SidesLength { get; }

        public FigureColor Color { get; private set; }

        public virtual void PaintFigureIn(FigureColor color)
        {
            if (_isPaintable && (Color == FigureColor.White))
            {
                Color = color;
            }
            else
                throw new Exception();
        }

        public abstract double GetArea();

        public abstract double GetPerimeter();
    }
}
