using Microsoft.VisualStudio.TestTools.UnitTesting;
using Polynomial;
using System;
using System.Collections.Generic;
using System.Text;

namespace Polynomial.Tests
{
    [TestClass()]
    public class PolynomialTests
    {
        [TestMethod()]
        public void MultiplicationOperator_PolynomialAnd2_CorrectPolynomialReturned()
        {
            var polynomial = new Polynomial(1, 0, 3);
            var numberForMultiplication = 2;
            var expected = new Polynomial(2, 0, 6);
            var actual = polynomial * numberForMultiplication;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void MultiplicationOperator_TwowPolynomials_CorrectPolynomialReturned()
        {
            var firstPolynomial = new Polynomial(1, 0, 4);
            var secondPolynomial = new Polynomial(1, 4);
            var expected = new Polynomial(1, 4, 4, 16);
            var actual = firstPolynomial * secondPolynomial;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void SumOperator_TwowPolynomials_CorrectPolynomialReturned()
        {
            var firstPolynomial = new Polynomial(1, 0, 4);
            var secondPolynomial = new Polynomial(1, 4);
            var expected = new Polynomial(1, 1, 8);
            var actual = firstPolynomial + secondPolynomial;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void SumOperator_PolynomialsAnd10_CorrectPolynomialReturned()
        {
            var firstPolynomial = new Polynomial(3, 2, 0);
            var numberForSum = 10;
            var expected = new Polynomial(3, 2, 10);
            var actual = firstPolynomial + numberForSum;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void DifferenceOperator_TwowPolynomials_CorrectPolynomialReturned()
        {
            var firstPolynomial = new Polynomial(1, 0, 4);
            var secondPolynomial = new Polynomial(1, 4);
            var expected = new Polynomial(1, -1, 0);
            var actual = firstPolynomial - secondPolynomial;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void DifferenceOperator_PolynomialAnd10_CorrectPolynomialReturned()
        {
            var firstPolynomial = new Polynomial(3, 2, 10);
            var numberForDifference = 10;
            var expected = new Polynomial(3, 2, 0);
            var actual = firstPolynomial - numberForDifference;
            Assert.AreEqual(expected, actual);
        }

        /*[TestMethod()]
        public void DivisionOperator_TwowPolynomials_CorrectPolynomialReturned()
        {
            var firstPolynomial = new Polynomial(1, 12, 0, -42);
            var secondPolynomial = new Polynomial(1, -3);
            var expected = new Polynomial(-9, 0, -42);
            var actual = firstPolynomial / secondPolynomial;
            Assert.AreEqual(expected, actual);
        }*/

        [TestMethod()]
        public void DivisionOperator_PolynomialAnd5_CorrectPolynomialReturned()
        {
            var firstPolynomial = new Polynomial(5, 15, 0, 10);
            var numberForDivision = 5;
            var expected = new Polynomial(1, 3, 0, 2);
            var actual = firstPolynomial / numberForDivision;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ToStringTest()
        {
            var polynomial = new Polynomial(1, -3);
            var expected = "1x^1 - 3x^0";
            var actual = polynomial.ToString();
            StringAssert.Contains(expected.ToString(), actual.ToString());
        }
    }
}