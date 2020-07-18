using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Girl.Figures
{
    public class Circle : Figure
    {
        public override double[] SidesLength { get; } = new double[1];

        public Circle(FigureMaterial figureMaterial, int radius) : base(figureMaterial)
        {
            SidesLength[0] = radius;
        }

        public Circle(Figure figure, double area) : base(figure)
        {
            if (area < figure.Area())
            {
                var radius = Math.Sqrt((area / Math.PI));
                SidesLength[0] = radius;
            }
            else
            {
                throw new Exception();
            }
        }

        public override double Area()
        {
            return Math.PI * Math.Pow(SidesLength[0], 2);
        }

        public override double Perimeter()
        {
            return 2 * Math.PI * SidesLength[0];
        }

        public override bool Equals(object obj)
        {
            return obj is Circle circle &&
                  Color == circle.Color &&
                  SidesLength.OrderBy(e => e).SequenceEqual(
                      circle.SidesLength.OrderBy(e => e));
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(SidesLength);
        }

        public override string ToString()
        {
            return "Circle: " + SidesLength[0] + ", Color: " + Color;
        }
    }
}
