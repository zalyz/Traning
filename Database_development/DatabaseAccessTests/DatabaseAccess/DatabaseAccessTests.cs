using DatabaseAccess.ModelClasses;
using System;
using System.Linq;
using NUnit.Framework;
using System.Collections.Generic;

namespace DatabaseAccess.Tests
{
    [TestFixture]
    public class DatabaseAccessTests
    {
        private readonly string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\slava\Desktop\Traning\Database_development\DatabaseAccess\Session.mdf;Integrated Security=True";

        /// <summary>
        /// Create tables in database and fill it with data.
        /// </summary>
        [OneTimeSetUp]
        public void ClassInitialize()
        {
            var filePath = @"../../../CreateAndFillDatabase.sql";
            ScriptExecuter.ScriptExecuter.Execute(filePath, _connectionString);
        }

        /// <summary>
        /// Adds the test result to the database.
        /// </summary>
        [TestCase(3, "Fiz", 3, "IP-51", 3, "Rewy", "Gleb", "Ivan", "Female", 6)]
        public void AddTest_TestResultSaved(int resultId, string testName, int testId, string groupName,
            int studentId ,string fName, string midName, string lName, string gender, int mark)
        {
            var databaseAccess = DatabaseAccess<TestResult>.Factory(_connectionString);
            var testResult = new TestResult()
            {
                Test = new Test()
                {
                    TestId = testId,
                    Name = testName,
                    Date = DateTime.Now,
                    GroupName = groupName
                },
                TestId = testId,
                Student = new Student()
                {
                    StudentId = studentId,
                    FirstName = fName,
                    MiddleName = midName,
                    LastName = lName,
                    GroupName = groupName,
                    DateOfBirthday = DateTime.Now,
                    Gender = gender
                },
                StudentId = studentId,
                Mark = mark,
                TestResultId = resultId
            };

            databaseAccess.Add(testResult);
        }
        
        /// <summary>
        /// Adds the exam result to the database.
        /// </summary>
        [TestCase(3, "OAIP", 3, "IP-41", 3, "Aleks", "Rewil", "Clow", "Male", 4)]
        public void AddTest_ExamResultSaved(int resultId, string testName, int examId, string groupName,
            int studentId ,string fName, string midName, string lName, string gender, int mark)
        {
            var databaseAccess = DatabaseAccess<ExamResult>.Factory(_connectionString);
            var examResult = new ExamResult()
            {
                Exam = new Exam()
                {
                    ExamId = examId,
                    Name = testName,
                    Date = DateTime.Now,
                    GroupName = groupName
                },
                ExamId = examId,
                Student = new Student()
                {
                    StudentId = studentId,
                    FirstName = fName,
                    MiddleName = midName,
                    LastName = lName,
                    GroupName = groupName,
                    DateOfBirthday = DateTime.Now,
                    Gender = gender
                },
                StudentId = studentId,
                Mark = mark,
                ExamResultId = resultId
            };

            databaseAccess.Add(examResult);
        }

        /// <summary>
        /// Deletes test result from the database.
        /// </summary>
        [TestCase(3, "Fiz", 3, "IP-51", 3, "Rewy", "Gleb", "Ivan", "Female", 6)]
        public void Delete_TestResultDeleted(int resultId, string testName, int testId, string groupName,
            int studentId, string fName, string midName, string lName, string gender, int mark)
        {
            var databaseAccess = DatabaseAccess<TestResult>.Factory(_connectionString);
            var testResult = new TestResult()
            {
                Test = new Test()
                {
                    TestId = testId,
                    Name = testName,
                    Date = DateTime.Now,
                    GroupName = groupName
                },
                TestId = testId,
                Student = new Student()
                {
                    StudentId = studentId,
                    FirstName = fName,
                    MiddleName = midName,
                    LastName = lName,
                    GroupName = groupName,
                    DateOfBirthday = DateTime.Now,
                    Gender = gender
                },
                StudentId = studentId,
                Mark = mark,
                TestResultId = resultId
            };

            databaseAccess.Delete(testResult);
        }

        /// <summary>2
        /// Reads all test result from the database.
        /// </summary>
        [Test]
        public void ReadAll_TestResultsAreReaded()
        {
            var databaseAccess = DatabaseAccess<TestResult>.Factory(_connectionString);
            var expectedCollection = new List<TestResult>()
            {
                new TestResult()
                {
                    Test = new Test(){ TestId = 1, Name = "Philosophy", Date = DateTime.Parse("22.03.2019"), GroupName = "FE-31" },
                    Student = new Student() { StudentId = 1, FirstName = "Abdulov", MiddleName = "Oleg", LastName = "Aleksandrovich", Gender = "Male", DateOfBirthday = DateTime.Parse("13.05.2000"), GroupName = "FE-31"},
                    TestId = 1,
                    StudentId = 1,
                    Mark = 8,
                    TestResultId = 1
                },
                new TestResult()
                {
                    Test = new Test(){ TestId = 2, Name = "Art", Date = DateTime.Parse("27.03.2019"), GroupName = "KS-11" },
                    Student = new Student() { StudentId = 2, FirstName = "Korovai", MiddleName = "Irina", LastName = "Olegovna", Gender = "Female", DateOfBirthday = DateTime.Parse("10.05.2001"), GroupName = "KS-11"},
                    TestId = 2,
                    StudentId = 2,
                    Mark = 5,
                    TestResultId = 2
                },
            };
            var actualCollection = databaseAccess.ReadAll().Where(e => e.TestResultId <= 2).ToList();
            CollectionAssert.AreEqual(expectedCollection, actualCollection);
        }

        /// <summary>
        /// Updates the exam result in the database.
        /// </summary>
        [TestCase(3, "OAIP", 3, "IP-41", 3, "Aleks", "Rewil", "Clowing", "Female", 10)]
        public void Update_EaxamResultUpdated(int resultId, string testName, int examId, string groupName,
            int studentId, string fName, string midName, string lName, string gender, int mark)
        {
            var databaseAccess = DatabaseAccess<ExamResult>.Factory(_connectionString);
            var examResult = new ExamResult()
            {
                Exam = new Exam()
                {
                    ExamId = examId,
                    Name = testName,
                    Date = DateTime.Now,
                    GroupName = groupName
                },
                ExamId = examId,
                Student = new Student()
                {
                    StudentId = studentId,
                    FirstName = fName,
                    MiddleName = midName,
                    LastName = lName,
                    GroupName = groupName,
                    DateOfBirthday = DateTime.Now,
                    Gender = gender
                },
                StudentId = studentId,
                Mark = mark,
                ExamResultId = resultId
            };

            databaseAccess.Update(examResult);
        }

        /// <summary>
        /// Tests of factory method.
        /// </summary>
        [Test]
        public void FactoryTest()
        {
            var databaseAccess = DatabaseAccess<ExamResult>.Factory(_connectionString);
        }
    }
}