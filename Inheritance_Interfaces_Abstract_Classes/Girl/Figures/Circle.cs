using System;
using System.Collections.Generic;
using System.Text;

namespace Girl.Figures
{
    public class Circle : Figure
    {
        public override double[] SidesLength { get; } = new double[1];

        public override double GetArea()
        {
            return Math.PI * Math.Pow(SidesLength[0], 2);
        }

        public override double GetPerimeter()
        {
            return 2 * Math.PI * SidesLength[0];
        }
    }
}
