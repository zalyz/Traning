using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shop.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Products.Tests
{
    [TestClass()]
    public class SodaTests
    {
        [TestMethod()]
        public void SumOperator_TwoSodas_SumOfSodasReturned()
        {
            var soda1 = new Soda("Cocacola", 3.99, "USA");
            var soda2 = new Soda("Pepsi", 4.99, "Kanada");
            var expected = new Soda("Cocacola - Pepsi", 4.49, "USA - Kanada");
            var actual = soda1 + soda2;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ConvertingToBun_Soda_BunReturned()
        {
            var soda = new Soda("Cocacola", 3.99, "USA");
            var expected = new Bun("Cocacola", 3.99, "USA");
            var actual = (Bun)soda;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ConvertingToVegetable_Soda_VegetableReturned()
        {
            var soda = new Soda("Cocacola", 3.99, "USA");
            var expected = new Vegetable("Cocacola", 3.99, "USA");
            var actual = (Vegetable)soda;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ConvertingToInt_Soda_3Returned()
        {
            var soda = new Soda("Cocacola", 3.99, "USA");
            var expected = 3;
            var actual = (int)soda;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ConvertingToDouble_Soda_PriceReturned()
        {
            var soda = new Soda("Cocacola", 3.99, "USA");
            var expected = 3.99;
            var actual = (double)soda;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ToStringTest()
        {
            var soda = new Soda("Cocacola", 3.99, "USA");
            var expected = "Soda: Name: Cocacola, Price: 3,99, Type: USA";
            var actual = soda.ToString();
            StringAssert.Contains(expected, actual);
        }
    }
}