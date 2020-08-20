using Microsoft.VisualStudio.TestTools.UnitTesting;
using DatabaseAccess;
using DatabaseAccess.ModelClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseAccess.Tests
{
    [TestClass()]
    public class DatabaseAccessTests
    {
        [TestMethod()]
        public void CreateEntityTest()
        {
            var databaseAccess = new DatabaseAccess<Student>();
            databaseAccess.CreateTable();
        }

        [TestMethod()]
        public void DeleteTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ReadAllTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void UpdateTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CreateTableTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CreateTableTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void FactoryTest()
        {
            Assert.Fail();
        }
    }
}