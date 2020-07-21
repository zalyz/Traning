using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Girl.Figures
{
    public class Triangle : Figure
    {
        public override double[] SidesLength { get; } = new double[3];

        public Triangle(FigureMaterial figureMaterial, double firstSide, double secondSide, double thirdSide) : base(figureMaterial)
        {
            SidesLength[0] = firstSide;
            SidesLength[1] = secondSide;
            SidesLength[2] = thirdSide;
        }

        public Triangle(Figure figure, double firstSide, double secondSide, double thirdSide) : base(figure)
        {
            var halfPerimeter = (firstSide + secondSide + thirdSide) / 2;
            var areaOfNewFigure = Math.Sqrt(halfPerimeter *(halfPerimeter - firstSide) *
                                                           (halfPerimeter - secondSide) *
                                                           (halfPerimeter - thirdSide));
            if (areaOfNewFigure < figure.Area())
            {
                SidesLength[0] = firstSide;
                SidesLength[1] = secondSide;
                SidesLength[2] = thirdSide;
            }
            else
            {
                throw new ArgumentException("Can't cut a Triangle with these parameters.");
            }
        }

        public override double Area()
        {
            var halfPerimeter = Perimeter() / 2;
            return Math.Sqrt(halfPerimeter * (halfPerimeter - SidesLength[0]) *
                                             (halfPerimeter - SidesLength[1]) *
                                             (halfPerimeter - SidesLength[2]));
        }

        public override double Perimeter()
        {
            return SidesLength[0] + SidesLength[1] + SidesLength[2];
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Triangle);
        }

        public bool Equals(Triangle triangle)
        {
            return triangle != null &&
                Color == triangle.Color &&
                SidesLength.OrderBy(e => e).SequenceEqual(
                      triangle.SidesLength.OrderBy(e => e));
        }

        public override int GetHashCode()
        {
            return (Color, SidesLength[0], SidesLength[1], SidesLength[2]).GetHashCode();
        }

        public override string ToString()
        {
            var stringFormatOfSidesLength = new StringBuilder();
            foreach (var side in SidesLength)
            {
                stringFormatOfSidesLength.Append(" " + side);
            }
            return "Triangle: " + stringFormatOfSidesLength.ToString() + ", Color: " + Color;
        }
    }
}
