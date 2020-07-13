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
        private Circle _circle;

        [TestInitialize]
        public void TestInitialize()
        {
            _circle = new Circle(3);
        }

        [TestMethod]
        public void GetFigureArea_CircleWhithRadius3_28Returned()
        {
            var expectedArea = 28.26;
            var actual = _circle.GetFigureArea();
            Assert.AreEqual(expectedArea, actual, 0.1);
        }

        [TestMethod]
        public void GetFigurePerimeter_CircleWhithRadius3_18Returned()
        {
            var expectedPerimeter = 18.84;
            var actual = _circle.GetFigurePerimeter();
            Assert.AreEqual(expectedPerimeter, actual, 0.1);
        }

        [TestMethod]
        public void ToStringTest()
        {
            var expectedString = "Circle: 3";
            var actual = _circle.ToString();
            Equals(expectedString, actual);
        }
    }
}