using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shop.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Products.Tests
{
    [TestClass()]
    public class VegetableTests
    {
        [TestMethod()]
        public void SumOperator_TwoVegetables_SumOfVegetablesReturned()
        {
            var vegetable1 = new Vegetable("Red pepper", 3.99, "Bulgarian");
            var vegetable2 = new Vegetable("Yellow pepper", 4.99, "German");
            var expected = new Vegetable("Red pepper - Yellow pepper", 4.49, "Bulgarian - German");
            var actual = vegetable1 + vegetable2;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ConvertingToSoda_Vegetable_SodaReturned()
        {
            var vegetable = new Vegetable("Red pepper", 3.99, "Bulgarian");
            var expected = new Soda("Red pepper", 3.99, "Bulgarian");
            var actual = (Soda)vegetable;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ConvertingToBun_Vegetable_BunReturned()
        {
            var vegetable = new Vegetable("Red pepper", 3.99, "Bulgarian");
            var expected = new Bun("Red pepper", 3.99, "Bulgarian");
            var actual = (Bun)vegetable;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ConvertingToInt_Vegetable_3Returned()
        {
            var vegetable = new Vegetable("Red pepper", 3.99, "Bulgarian");
            var expected = 3;
            var actual = (int)vegetable;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ConvertingToDouble_Vegetable_PriceReturned()
        {
            var vegetable = new Vegetable("Red pepper", 3.99, "Bulgarian");
            var expected = 3.99;
            var actual = (double)vegetable;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ToStringTest()
        {
            var vegetable = new Vegetable("Red pepper", 3.99, "Bulgarian");
            var expected = "Vegetable: Name: Red pepper, Price: 3,99, Type: Bulgarian";
            var actual = vegetable.ToString();
            StringAssert.Contains(expected, actual);
        }
    }
}