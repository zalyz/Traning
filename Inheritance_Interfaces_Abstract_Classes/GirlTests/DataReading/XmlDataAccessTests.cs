using Microsoft.VisualStudio.TestTools.UnitTesting;
using Girl.DataReading;
using System;
using System.Collections.Generic;
using System.Text;
using Girl.Figures;

namespace Girl.DataReading.Tests
{
    [TestClass()]
    public class XmlDataAccessTests
    {
        [TestMethod()]
        public void ReadDataTest()
        {
            var path = "Figures.xml";
            var reader = new XmlDataAccess();
            var expected = new Figure[] { new Circle(FigureMaterial.Paper, 3), new Triangle(FigureMaterial.Film, 1, 2, 3) };
            var actual = reader.ReadData(path);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void WriteDataTest()
        {
            var a = new XmlDataAccess();
            a.WriteData(new Figure[]{ new Circle(FigureMaterial.Paper, 3), new Triangle(FigureMaterial.Film, 1, 2, 3) }, "Figures.xml");
        }
    }
}