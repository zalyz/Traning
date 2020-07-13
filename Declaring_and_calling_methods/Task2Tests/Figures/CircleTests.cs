using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task2.Figures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Task2.Figures.Tests
{
    [TestClass]
    public class CircleTests
    {
        [TestMethod]
        public void GetFigureArea_CircleWhithRadius3_28Returned()
        {
            var circle = new Circle(3);
            var expectedArea = 3.14 * 9;
            var actual = circle.GetFigureArea();
            Assert.AreEqual(expectedArea, actual, 0.1);
        }

        [TestMethod]
        public void GetFigurePerimeter_CircleWhithRadius3_28Returned()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void ToStringTest()
        {
            Assert.Fail();
        }
    }
}