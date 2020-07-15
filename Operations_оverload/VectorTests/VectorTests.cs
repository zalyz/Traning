using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vector;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vector.Tests
{
    [TestClass()]
    public class VectorTests
    {
        [TestMethod()]
        public void ScalarProduct_InputTwoSameVectors_81Returned()
        {
            var vector1 = new Vector(4, 8, 1);
            var expected = 81;
            var actual = Vector.ScalarProduct(vector1, vector1, 0);
            Assert.AreEqual(expected, actual, 0.1);
        }

        [TestMethod()]
        public void ScalarProduct_InputTwoVectors_17Returned()
        {
            var vector1 = new Vector(4, 8, 1);
            var vector2 = new Vector(6, -1, 1);
            var expected = 17;
            var actual = Vector.ScalarProduct(vector1, vector2);
            Assert.AreEqual(expected, actual, 0.1);
        }

        [TestMethod()]
        public void MixedProduct_InputThreeVecrots_2Returned()
        {
            var vector1 = new Vector(1, 2, 3);
            var vector2 = new Vector(1, 1, 1);
            var vector3 = new Vector(1, 2, 1);
            var expected = 2;
            var actual = Vector.MixedProduct(vector1, vector2, vector3);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Multiplication_VectorAnd0_ZeroVectorReturned()
        {
            var vector = new Vector(1, 2, 3);
            var multiplicationNumber = 0;
            var expected = new Vector(0, 0, 0);
            var actual = vector * multiplicationNumber;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Multiplication_TwowVectors_ZeroVectorReturned()
        {
            var vector1 = new Vector(1, 2, 3);
            var vector2 = new Vector(1, 2, 3);
            var expected = new Vector(0, 0, 0);
            var actual = vector1 * vector2;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Sum_TwoVectors_UnitVectorReturned()
        {
            var vector1 = new Vector(1, 2, 3);
            var vector2 = new Vector(0, -1, -2);
            var expected = new Vector(1, 1, 1);
            var actual = vector1 + vector2;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void EqualsTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ToStringTest()
        {
            Assert.Fail();
        }
    }
}