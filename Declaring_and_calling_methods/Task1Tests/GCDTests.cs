using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task1;
using System;
using System.Collections.Generic;
using System.Text;

namespace Task1.Tests
{
    [TestClass()]
    public class GCDTests
    {
        [TestMethod]
        public void GreatestCommonDivisorOf_4and6_2Returned()
        {
            var firstNumber = 4;
            var secondNumber = 6;
            var expectedNumber = 2;
            var actual = GCD.GreatestCommonDivisorOf(firstNumber, secondNumber, out TimeSpan time);
            Assert.AreEqual(expectedNumber, actual);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void GreatestCommonDivisorOf_InputNegativeNumberAndZero_ThrowsArgumentException()
        {
            var firstNumber = -2;
            var secondNumber = 0;
            var actual = GCD.GreatestCommonDivisorOf(firstNumber, secondNumber, out TimeSpan time);
            Assert.ThrowsException<ArgumentException>(() => GCD.GreatestCommonDivisorOf(firstNumber, secondNumber, out TimeSpan time));
        }

        [TestMethod]
        public void GreatestCommonDivisorOf_4and6and7_1Retuned()
        {
            var firstNumber = 4;
            var secondNumber = 6;
            var thirdNumber = 7;
            var expectedNumber = 1;
            var actual = GCD.GreatestCommonDivisorOf(firstNumber, secondNumber, thirdNumber, out TimeSpan time);
            Assert.AreEqual(expectedNumber, actual);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void GreatestCommonDivisorOf_InputTwoNegativeNumbersAndZero_ThrowsArgumentException()
        {
            var firstNumber = -4;
            var secondNumber = 0;
            var thirdNumber = -7;
            GCD.GreatestCommonDivisorOf(firstNumber, secondNumber, thirdNumber, out TimeSpan time);
        }

        [TestMethod]
        public void GreatestCommonDivisorOf_3and6and15and18_3Returned()
        {
            var firstNumber = 3;
            var secondNumber = 6;
            var thirdNumber = 15;
            var fouthNumber = 18;
            var expectedNumber = 3;
            var actual = GCD.GreatestCommonDivisorOf(firstNumber, secondNumber, thirdNumber, fouthNumber, out TimeSpan time);
            Assert.AreEqual(expectedNumber, actual);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void GreatestCommonDivisorOf_InputTwoNegativeNumbersAndZeroAnd2_ThrowsArgumentException()
        {
            var firstNumber = -4;
            var secondNumber = 0;
            var thirdNumber = -7;
            var fouthNumber = 2;
            GCD.GreatestCommonDivisorOf(firstNumber, secondNumber, thirdNumber, fouthNumber, out TimeSpan time);
        }

        [TestMethod]
        public void GreatestCommonDivisorOf_3and6and15and18and19_3Returned()
        {
            var firstNumber = 3;
            var secondNumber = 6;
            var thirdNumber = 15;
            var fouthNumber = 18;
            var fifthNumber = 19;
            var expectedNumber = 1;
            var actual = GCD.GreatestCommonDivisorOf(firstNumber, secondNumber, thirdNumber, fouthNumber, fifthNumber, out TimeSpan time);
            Assert.AreEqual(expectedNumber, actual);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void GreatestCommonDivisorOf_InputThreeNegativeNumbersAndZeroAnd2_ThrowsArgumentException()
        {
            var firstNumber = -4;
            var secondNumber = 0;
            var thirdNumber = -7;
            var fouthNumber = -2;
            var fifthNumber = 2;
            GCD.GreatestCommonDivisorOf(firstNumber, secondNumber, thirdNumber, fouthNumber, fifthNumber, out TimeSpan time);
        }

        [TestMethod]
        public void GCDByStein_40and72_2Returned()
        {
            var firstNumber = 40;
            var secondNumber = 72;
            var expectedNumber = 8;
            var actual = GCD.GreatestCommonDivisorOf(firstNumber, secondNumber, out TimeSpan time);
            Assert.AreEqual(expectedNumber, actual);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void GCDByStein_InputNegativeNumberAnd2_ThrowsArgumentException()
        {
            var firstNumber = -4;
            var secondNumber = 2;
            GCD.GCDByStein(firstNumber, secondNumber, out TimeSpan time);
        }

        [TestMethod]
        public void GetStatisticalDataForGraph_4and8and20and37and53_ValidArrayReturned()
        {
            var firstNumber = 4;
            var secondNumber = 8;
            var thirdNumber = 20;
            var fouthNumber = 37;
            var fifthNumber = 53;
            int[] expectedArray = { 2, 2, 3, 4, 5 };
            var actual = GCD.GetStatisticalDataForGraph(firstNumber, secondNumber, thirdNumber, fouthNumber, fifthNumber);
            CollectionAssert.AreEqual(expectedArray, actual.arrayOfResults);
        }
    }
}