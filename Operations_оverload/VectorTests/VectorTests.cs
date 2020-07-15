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

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod()]
        public void ScalarProduct_InputVectorAndNull_ThrowsArgumentNullException()
        {
            var vector1 = new Vector(4, 8, 1);
            Vector.ScalarProduct(vector1, null, 0);
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

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod()]
        public void ScalarProduct_InputNull_ThrowsArgumentNullException()
        {
            var vector1 = new Vector(4, 8, 1);
            Vector.ScalarProduct(null, null);
        }

        [TestMethod()]
        public void EqualityOperator_TwoSameVectors_TrueReturned()
        {
            var vector1 = new Vector(2, 3, 1);
            var vector2 = new Vector(2, 3, 1);
            var expected = true;
            var actual = vector1 == vector2;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void InequalityOperator_TwoSameVectors_FalseReturned()
        {
            var vector1 = new Vector(2, 3, 1);
            var vector2 = new Vector(2, 3, 1);
            var expected = false;
            var actual = vector1 != vector2;
            Assert.AreEqual(expected, actual);
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

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod()]
        public void MixedProduct_InputTwoVectorsAndNull_ThrowsArgumentNullException()
        {
            var vector1 = new Vector(1, 2, 3);
            var vector2 = new Vector(1, 1, 1);
            Vector.MixedProduct(vector1, vector2, null);
        }

        [TestMethod()]
        public void MultiplicationOperator_VectorAnd0_ZeroVectorReturned()
        {
            var vector = new Vector(1, 2, 3);
            var multiplicationNumber = 0;
            var expected = new Vector(0, 0, 0);
            var actual = vector * multiplicationNumber;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void MultiplicationOperator_TwowVectors_ZeroVectorReturned()
        {
            var vector1 = new Vector(1, 2, 3);
            var vector2 = new Vector(1, 2, 3);
            var expected = new Vector(0, 0, 0);
            var actual = vector1 * vector2;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void SumOperator_TwoVectors_UnitVectorReturned()
        {
            var vector1 = new Vector(1, 2, 3);
            var vector2 = new Vector(0, -1, -2);
            var expected = new Vector(1, 1, 1);
            var actual = vector1 + vector2;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void DifferenceOperator_TwoVectors_UnitVectorReturned()
        {
            var vector1 = new Vector(1, 2, 3);
            var vector2 = new Vector(0, 1, 2);
            var expected = new Vector(1, 1, 1);
            var actual = vector1 - vector2;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Equals_TwoDifferentVectors_FalseReturned()
        {
            var vector1 = new Vector(1, 2, 3);
            var vector2 = new Vector(0, 1, 2);
            var expected = false;
            var actual = vector1.Equals(vector2);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ToString_UnitVector_CorrectStringReturned()
        {
            var unitVector = new Vector(1, 1, 1);
            var expected = "(1, 1, 1)";
            var actual = unitVector.ToString();
            StringAssert.Contains(expected, actual);
        }
    }
}