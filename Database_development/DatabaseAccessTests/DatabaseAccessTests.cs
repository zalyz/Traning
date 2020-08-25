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
            var student = new Student()
            {
                StudentId = 1,
                FirstName = "Bob",
                MiddleName = "Alice",
                LastName = "Rom",
                DateOfBirthday = DateTime.Parse("21.11.2020"),
                Gender = "Male",
                GroupName = "IP-31"
            };

            databaseAccess.Add(student);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            var connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\slava\Desktop\Traning\Database_development\DatabaseAccess\Session.mdf;Integrated Security=True";
            var databaseAccess = DatabaseAccess<Student>.Factory(connectionString);
            var student = new Student()
            {
                StudentId = 1,
                FirstName = "Bob",
                MiddleName = "Alice",
                LastName = "Rom",
                DateOfBirthday = DateTime.Parse("21.11.2020"),
                Gender = "Male",
                GroupName = "IP-31"
            };

            databaseAccess.Delete(student);
        }

        [TestMethod()]
        public void ReadAllTest()
        {
            var connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\slava\Desktop\Traning\Database_development\DatabaseAccess\Session.mdf;Integrated Security=True";
            var databaseAccess = DatabaseAccess<Student>.Factory(connectionString);
            var asd = databaseAccess.ReadAll();
        }

        [TestMethod()]
        public void UpdateTest()
        {
            var connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\slava\Desktop\Traning\Database_development\DatabaseAccess\Session.mdf;Integrated Security=True";
            var databaseAccess = DatabaseAccess<Student>.Factory(connectionString);
            var student = new Student()
            {
                StudentId = 1,
                FirstName = "slava",
                MiddleName = "Alice",
                LastName = "Dranev",
                DateOfBirthday = DateTime.Parse("21.11.2020"),
                Gender = "Male",
                GroupName = "IP-31"
            };

            databaseAccess.Update(student);
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