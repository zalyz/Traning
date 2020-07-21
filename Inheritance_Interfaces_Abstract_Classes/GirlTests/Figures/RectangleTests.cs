using Microsoft.VisualStudio.TestTools.UnitTesting;
using Girl.Figures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Girl.Figures.Tests
{
    [TestClass()]
    public class RectangleTests
    {
        [TestMethod()]
        public void CutRectangle_InputCircleAndSides_RectangleReturned()
        {
            var circle = new Circle(FigureMaterial.Paper, 4);
            var expected = new Rectangle(FigureMaterial.Paper, 1, 2);
            var actual = new Rectangle(circle, 1, 2);
            Assert.AreEqual(expected, actual);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod()]
        public void CutRectangle_InputCircleAndSides_ThrowsArgumentException()
        {
            var circle = new Circle(FigureMaterial.Paper, 2);
            var actual = new Rectangle(circle, 4, 5);
        }

        [TestMethod()]
        public void CutRectangle_InputTriangleAndSides_RectangleReturned()
        {
            var triangle = new Triangle(FigureMaterial.Paper, 4, 5, 6);
            var expected = new Rectangle(FigureMaterial.Paper, 2, 3);
            var actual = new Rectangle(triangle, 2, 3);
            Assert.AreEqual(expected, actual);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod()]
        public void CutRectangle_InputTriangleAndSides_ThrowsArgumentException()
        {
            var triangle = new Triangle(FigureMaterial.Paper, 4, 5, 6);
            var actual = new Rectangle(triangle, 7, 7);
        }

        [TestMethod()]
        public void Area_RectangleWhithSides2And3_6Returned()
        {
            var rectangle = new Rectangle(FigureMaterial.Paper, 2, 3);
            var expected = 6;
            var actual = rectangle.Area();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Perimeter_RectangleWhithSides2And3_10Returned()
        {
            var rectangle = new Rectangle(FigureMaterial.Paper, 2, 3);
            var expected = 10;
            var actual = rectangle.Perimeter();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Equals_InputTheSameRectangleAsObject_TrueReturned()
        {
            var rectangle = new Rectangle(FigureMaterial.Paper, 2, 3);
            Assert.IsTrue(rectangle.Equals((object)rectangle));
        }

        [TestMethod()]
        public void Equals_InputTheSameRectangle_TrueReturned()
        {
            var rectangle = new Rectangle(FigureMaterial.Paper, 2, 3);
            Assert.IsTrue(rectangle.Equals(rectangle));
        }

        [TestMethod()]
        public void ToStringTest()
        {
            var rectangle = new Rectangle(FigureMaterial.Paper, 2, 3);
            var expected = "Rectangle: 2 3, Color: White";
            var actual = rectangle.ToString();
            Equals(expected, actual);
        }
    }
}