using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task2.Figures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Task2.Figures.Tests
{
    [TestClass]
    public class QuadrantTests
    {
        private Quadrant _quadrant;

        [TestInitialize]
        public void TestInitialize()
        {
            _quadrant = new Quadrant(4);
        }

        [TestMethod]
        public void GetFigureArea_QuadrantWhithRadius4_12Returned()
        {
            var expectedArea = 12.56;
            var actual = _quadrant.GetFigureArea();
            Assert.AreEqual(expectedArea, actual, 0.1);
        }

        [TestMethod]
        public void GetFigurePerimeter_QuadrantWhithRadius4_14Returned()
        {
            var expectedPerimeter = 14.28;
            var actual = _quadrant.GetFigurePerimeter();
            Assert.AreEqual(expectedPerimeter, actual, 0.1);
        }

        [TestMethod]
        public void ToStringTest()
        {
            var expectedString = "Quadrant: 4";
            var actual = _quadrant.ToString();
            Equals(expectedString, actual);
        }
    }
}