using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task2;
using System;
using System.Collections.Generic;
using System.Text;
using Task2.Figures;

namespace Task2.Tests
{
    [TestClass]
    public class DataReaderTests
    {
        [TestMethod]
        public void ReadFiguresFromTest()
        {
            var filePath = @"../../../../Figures.txt";
            Figure[] expectedArrayOfFigures = { new Triangle(1, 2, 4), new Rectangle(2, 4), new Quadrant(5)};
            var actualArrayOfFigures = DataReader.ReadFiguresFrom(filePath);
            CollectionAssert.AreEqual(expectedArrayOfFigures, actualArrayOfFigures);
        }
    }
}