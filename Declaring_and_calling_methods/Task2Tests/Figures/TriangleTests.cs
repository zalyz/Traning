using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task2.Figures;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Task2.Figures.Tests
{
    [TestClass]
    public class TriangleTests
    {
        private Triangle _triangle;

        [TestInitialize]
        public void TestInitialize()
        {
            _triangle = new Triangle(3, 4, 5);
        }

        [TestMethod]
        public void GetFigureArea_TriangleWhithSides3And4And5_28Returned()
        {
            var expectedArea = 6;
            var actual = _triangle.GetFigureArea();
            Assert.AreEqual(expectedArea, actual, 0.1);
        }

        [TestMethod]
        public void GetFigurePerimeter_TriangleWhithSides3And4And5_9Returned()
        {
            var expectedPerimeter = 12;
            var actual = _triangle.GetFigurePerimeter();
            Assert.AreEqual(expectedPerimeter, actual, 0.1);
        }

        [TestMethod]
        public void EqualsTest()
        {
            var filePath = @"../../../../Figures.txt";
            var arrayOfFigures = DataReader.ReadFiguresFrom(filePath);
            var expectedFigure = new Triangle(1, 2, 4);
            var actualFigure = arrayOfFigures.Where(e => e.Equals(expectedFigure));
            Equals(expectedFigure.ToString(), actualFigure.ToString());
        }

        [TestMethod]
        public void ToStringTest()
        {
            var expectedString = "Triangle: 3 4 5";
            var actual = _triangle.ToString();
            Equals(expectedString, actual);
        }
    }
}