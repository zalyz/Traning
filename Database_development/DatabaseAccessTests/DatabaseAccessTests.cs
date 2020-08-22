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
            var connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\slava\Desktop\Traning\Database_development\DatabaseAccess\Session.mdf;Integrated Security=True";
            var databaseAccess = DatabaseAccess<Student>.Factory(connectionString);
            databaseAccess.CreateEntity();
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
            var connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\slava\Desktop\Traning\Database_development\DatabaseAccess\Session.mdf;Integrated Security=True";
            var databaseAccess = DatabaseAccess<Student>.Factory(connectionString);
            databaseAccess.CreateTable();
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