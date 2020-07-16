using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shop.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Products.Tests
{
    [TestClass()]
    public class BunTests
    {
        [TestMethod()]
        public void SumOperator_TwoBuns_SumOfBunsReturned()
        {
            var bun1 = new Bun("Sweet", 3.99, "Bulgarian");
            var bun2 = new Bun("Papa", 4.99, "Italy");
            var expected = new Bun("Sweet - Papa", 4.49, "Bulgarian - Italy");
            var actual = bun1 + bun2;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ConvertingToSoda_Bun_SodaReturned()
        {
            var bun = new Bun("Sweet", 3.99, "Bulgarian");
            var expected = new Soda("Sweet", 3.99, "Bulgarian");
            var actual = (Soda)bun;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ConvertingToVegetable_Bun_VegetableReturned()
        {
            var bun = new Bun("Sweet", 3.99, "Bulgarian");
            var expected = new Vegetable("Sweet", 3.99, "Bulgarian");
            var actual = (Vegetable)bun;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ConvertingToInt_Bun_3Returned()
        {
            var bun = new Bun("Sweet", 3.99, "Bulgarian");
            var expected = 3;
            var actual = (int)bun;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ConvertingToDouble_Bun_PriceReturned()
        {
            var bun = new Bun("Sweet", 3.99, "Bulgarian");
            var expected = 3.99;
            var actual = (double)bun;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ToStringTest()
        {
            var bun = new Bun("Sweet", 3.99, "Bulgarian");
            var expected = "Bun: Name: Sweet, Price: 3,99, Type: Bulgarian";
            var actual = bun.ToString();
            StringAssert.Contains(expected, actual);
        }
    }
}