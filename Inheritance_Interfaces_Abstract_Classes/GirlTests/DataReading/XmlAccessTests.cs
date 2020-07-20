using Microsoft.VisualStudio.TestTools.UnitTesting;
using Girl.DataReading;
using System;
using System.Collections.Generic;
using System.Text;
using Girl.Figures;

namespace Girl.DataReading.Tests
{
    [TestClass()]
    public class XmlAccessTests
    {
        /*[TestMethod()]
        public void ReadDataTest()
        {

        }*/

        [TestMethod()]
        public void WriteDataTest()
        {
            var a = new XmlDataAccess();
            a.WriteData(new Figure[]{ new Circle(FigureMaterial.Paper, 3), new Triangle(FigureMaterial.Film, 1, 2, 3) }, "Figures.xml");
        }
    }
}