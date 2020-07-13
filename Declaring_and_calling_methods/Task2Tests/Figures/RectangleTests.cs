﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task2.Figures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Task2.Figures.Tests
{
    [TestClass]
    public class RectangleTests
    {
        private Rectangle _rectangle;

        [TestInitialize]
        public void TestInitialize()
        {
            _rectangle = new Rectangle(2, 3);
        }

        [TestMethod]
        public void GetFigureArea_RectangleWhithSides2And3_6Returned()
        {
            var expectedArea = 6;
            var actual = _rectangle.GetFigureArea();
            Assert.AreEqual(expectedArea, actual);
        }

        [TestMethod]
        public void GetFigurePerimeter_RectangleWhithSides2And3_10Returned()
        {
            var expectedPerimeter = 10;
            var actual = _rectangle.GetFigurePerimeter();
            Assert.AreEqual(expectedPerimeter, actual);
        }

        [TestMethod]
        public void ToStringTest()
        {
            var expectedString = "Circle: 3";
            var actual = _rectangle.ToString();
            Equals(expectedString, actual);
        }
    }
}