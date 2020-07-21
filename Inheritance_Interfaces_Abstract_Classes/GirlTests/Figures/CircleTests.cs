using Microsoft.VisualStudio.TestTools.UnitTesting;
using Girl.Figures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Girl.Figures.Tests
{
    [TestClass()]
    public class CircleTests
    {

        [TestMethod()]
        public void CutCircle_InputTriangleAndRadius_CircleReturned()
        {
            var trinagle = new Triangle(FigureMaterial.Paper, 4, 5, 3);
            var expected = new Circle(FigureMaterial.Paper, 1);
            var actual = new Circle(trinagle, 1);
            Assert.AreEqual(expected, actual);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod()]
        public void CutCircle_InputTriangleAndRadius_ThrowsArgumentException()
        {
            var trinagle = new Triangle(FigureMaterial.Paper, 4, 5, 3);
            var actual = new Circle(trinagle, 3);
        }

        [TestMethod()]
        public void CutCircle_InputRectangleAndRadius_CircleReturned()
        {
            var rectangle = new Rectangle(FigureMaterial.Paper, 4, 5);
            var expected = new Circle(FigureMaterial.Paper, 2);
            var actual = new Circle(rectangle, 2);
            Assert.AreEqual(expected, actual);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod()]
        public void CutCircle_InputRectangleAndRadius_ThrowsArgumentException()
        {
            var rectangle = new Rectangle(FigureMaterial.Paper, 4, 5);
            var actual = new Circle(rectangle, 3);
        }

        [TestMethod()]
        public void Area_CircleWhithRadius2_12Returned()
        {
            var circle = new Circle(FigureMaterial.Paper, 2);
            var expected = 12.56;
            var actual = circle.Area();
            Assert.AreEqual(expected, actual, 0.1);
        }

        [TestMethod()]
        public void Perimeter_CircleWhithRadius3_18Returned()
        {
            var circle = new Circle(FigureMaterial.Paper, 3);
            var expected = 18.84;
            var actual = circle.Perimeter();
            Assert.AreEqual(expected, actual, 0.1);
        }

        [TestMethod()]
        public void Equals_InputTheSameCircleAsObject_TrueReturned()
        {
            var circle = new Circle(FigureMaterial.Film, 3);
            Assert.IsTrue(circle.Equals((object)circle));
        }

        [TestMethod()]
        public void Equals_InputTheSameCircle_TrueReturned()
        {
            var circle = new Circle(FigureMaterial.Film, 3);
            Assert.IsTrue(circle.Equals(circle));
        }

        [TestMethod()]
        public void ToStringTest()
        {
            var circle = new Circle(FigureMaterial.Film, 3);
            var expected = "Circle: 3, Color: Transparent";
            var actual = circle.ToString();
            Equals(expected, actual);
        }
    }
}