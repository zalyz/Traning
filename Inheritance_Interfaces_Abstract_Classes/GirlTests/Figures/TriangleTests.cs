using Microsoft.VisualStudio.TestTools.UnitTesting;
using Girl.Figures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Girl.Figures.Tests
{
    [TestClass()]
    public class TriangleTests
    {
        [TestMethod()]
        public void CutTriangle_InputCircleAndSides_TriangleReturned()
        {
            var circle = new Circle(FigureMaterial.Paper, 4);
            var expected = new Triangle(FigureMaterial.Paper, 2, 2, 3);
            var actual = new Triangle(circle, 2, 2, 3);
            Assert.AreEqual(expected, actual);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod()]
        public void CutTriangle_InputCircleAndSides_ThrowsArgumentException()
        {
            var circle = new Circle(FigureMaterial.Paper, 1);
            var actual = new Triangle(circle, 4, 8, 6);
        }

        [TestMethod()]
        public void CutTriangle_InputRectangleAndSides_TriangleReturned()
        {
            var rectangle = new Rectangle(FigureMaterial.Paper, 4, 3);
            var expected = new Triangle(FigureMaterial.Paper, 4, 5, 6);
            var actual = new Triangle(rectangle, 4, 5, 6);
            Assert.AreEqual(expected, actual);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod()]
        public void CutTriangle_InputRectangleAndSides_ThrowsArgumentException()
        {
            var rectangle = new Rectangle(FigureMaterial.Paper, 2, 3);
            var actual = new Triangle(rectangle, 4, 5, 6);
        }

        [TestMethod()]
        public void Area_TriangleWhithSide4And5And6_9Returned()
        {
            var triangle = new Triangle(FigureMaterial.Film, 4, 5, 6);
            var expected = 9.92;
            var actual = triangle.Area();
            Assert.AreEqual(expected, actual, 0.1);
        }

        [TestMethod()]
        public void Perimeter_TriangleWhithSide4And5And6_15Returned()
        {
            var triangle = new Triangle(FigureMaterial.Film, 4, 5, 6);
            var expected = 15;
            var actual = triangle.Perimeter();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Equals_InputTheSameTriangleAsObject_TrueReturned()
        {
            var triangle = new Triangle(FigureMaterial.Film, 4, 5, 6);
            Assert.IsTrue(triangle.Equals((object)triangle));
        }

        [TestMethod()]
        public void Equals_InputTheSameTriangle_TrueReturned()
        {
            var triangle = new Triangle(FigureMaterial.Film, 4, 5, 6);
            Assert.IsTrue(triangle.Equals(triangle));
        }

        [TestMethod()]
        public void ToStringTest()
        {
            var triangle = new Triangle(FigureMaterial.Film, 4, 5, 6);
            var expected = "Triangle: 4 5 6, Color: Transparent";
            var actual = triangle.ToString();
            Equals(expected, actual);
        }
    }
}