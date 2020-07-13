using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Task2.Figures;

namespace Task2.Factory.Tests
{
    [TestClass]
    public class FigureFactoryTests
    {
        [TestMethod]
        public void CreateFigure_LineWhithRectandleData_RectangleWhithSides2And3Returned()
        {
            var figureDataLine = "Rectangle 3 2";
            var expectedRectangle = new Rectangle(2, 3);
            var actualRectangle = FigureFactory.CreateFigure(figureDataLine);
            Equals(expectedRectangle.ToString(), actualRectangle.ToString());
        }
    }
}