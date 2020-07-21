using Microsoft.VisualStudio.TestTools.UnitTesting;
using Girl.DataReading;
using System;
using System.Collections.Generic;
using System.Text;
using Girl.Figures;

namespace Girl.DataReading.Tests
{
    [TestClass()]
    public class StreamDataAccessTests
    {
        [TestMethod()]
        public void ReadDataTest()
        {
            var a = new StreamDataAccess();
            var expected = new Figure[] { new Circle(FigureMaterial.Paper, 3), new Triangle(FigureMaterial.Film, 1, 2, 3) };
            var actual = a.ReadData("Figures.xml");
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void WriteDataTest()
        {
            var a = new StreamDataAccess();
            a.WriteData(new Figure[] { new Circle(FigureMaterial.Paper, 3), new Triangle(FigureMaterial.Film, 1, 2, 3) }, "Figures.xml");
        }
    }
}