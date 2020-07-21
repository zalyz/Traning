using System;
using System.Collections.Generic;
using System.Text;

namespace Girl.Figures
{
    class Rectangle : Figure
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
    }
}
