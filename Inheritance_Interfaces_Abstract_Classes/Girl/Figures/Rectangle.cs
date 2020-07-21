using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Girl.Figures
{
    public class Rectangle : Figure
    {
        public override double[] SidesLength { get; } = new double[2];

        public Rectangle(FigureMaterial figureMaterial, double firstSide, double secondSide) : base(figureMaterial)
        {
            SidesLength[0] = firstSide;
            SidesLength[1] = secondSide;
        }

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

        public override double Area()
        {
            return SidesLength[0] * SidesLength[1];
        }

        public override double Perimeter()
        {
            return (SidesLength[0] + SidesLength[1]) * 2;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Rectangle);
        }

        public bool Equals(Rectangle rectangle)
        {
            return rectangle != null &&
                Color == rectangle.Color &&
                SidesLength.OrderBy(e => e).SequenceEqual(
                      rectangle.SidesLength.OrderBy(e => e));
        }

        public override int GetHashCode()
        {
            return (Color, SidesLength[0]).GetHashCode();
        }

        public override string ToString()
        {
            return "Rectangle: " + SidesLength[0] + " " + SidesLength[1] + ", Color: " + Color;
        }
    }
}
