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
        /// <summary>
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
            var actualCollection = databaseAccess.ReadAll().Where(e => e.TestResultId <= 2);
            CollectionAssert.IsNotEmpty(actualCollection);
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
                    Name = testName,
                    Date = DateTime.Now,
                    GroupName = groupName
                },
                ExamId = examId,
                Student = new Student()
                {
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

        #region AdditionalTests

        /// <summary>
        /// Adds Exam to the data base.
        /// </summary>
        [Order(1)]
        [TestCase(3, "Art", "20.05.2020", "IP-31")]
        public void Add_ExamSaved(int id, string name, string date, string groupName)
        {
            var databaseAccess = DatabaseAccess<Exam>.Factory(_connectionString);
            var exam = new Exam()
            {
                ExamId = id,
                Name = name,
                Date = DateTime.Parse(date),
                GroupName = groupName
            };

            databaseAccess.Add(exam);
        }

        /// <summary>
        /// Updates exam in the data base.
        /// </summary>
        [Order(2)]
        [TestCase(2)]
        public void Update_ExamUpdated(int id)
        {
            var dataAccess = DatabaseAccess<Exam>.Factory(_connectionString);
            var exam = dataAccess.ReadAll().Where(e => e.ExamId == id).FirstOrDefault();
            exam.Date = DateTime.Parse("18.06.2001");
            dataAccess.Update(exam);
        }

        /// <summary>
        /// Reads all exams from the data base.
        /// </summary>
        [Order(3)]
        [Test]
        public void ReadAll_ExamsAreReaded()
        {
            var dataAccess = DatabaseAccess<Exam>.Factory(_connectionString);
            var exams = dataAccess.ReadAll();
            CollectionAssert.IsNotEmpty(exams);
        }

        /// <summary>
        /// Removes exam from the database.
        /// </summary>
        /// <param name="id"></param>
        [Order(4)]
        [TestCase(3)]
        public void Delete_ExamRemoved(int id)
        {
            var dataAccess = DatabaseAccess<Exam>.Factory(_connectionString);
            var examForRemoving = dataAccess.ReadAll().Where(e => e.ExamId == id).FirstOrDefault();
            dataAccess.Delete(examForRemoving);
        }

        /// <summary>
        /// Adds test to the data base.
        /// </summary>
        [Order(5)]
        [TestCase(3, "Math" , "21.05.2019", "IP-31")]
        public void Add_TestSaved(int id, string name, string date, string groupName)
        {
            var databaseAccess = DatabaseAccess<Test>.Factory(_connectionString);
            var exam = new Test()
            {
                TestId = id,
                Name = name,
                Date = DateTime.Parse(date),
                GroupName = groupName
            };

            databaseAccess.Add(exam);
        }

        /// <summary>
        /// Updates test in the data base.
        /// </summary>
        [Order(6)]
        [TestCase(3)]
        public void Update_TestUpdated(int id)
        {
            var dataAccess = DatabaseAccess<Test>.Factory(_connectionString);
            var exam = dataAccess.ReadAll().Where(e => e.TestId == id).FirstOrDefault();
            exam.Date = DateTime.Parse("18.06.2001");
            dataAccess.Update(exam);
        }

        /// <summary>
        /// Reads all tests from the data base.
        /// </summary>
        [Order(7)]
        [Test]
        public void ReadAll_TestsAreReaded()
        {
            var dataAccess = DatabaseAccess<Test>.Factory(_connectionString);
            var exams = dataAccess.ReadAll();
            CollectionAssert.IsNotEmpty(exams);
        }

        /// <summary>
        /// Removes test from the data base.
        /// </summary>
        [Order(8)]
        [TestCase(3)]
        public void Delete_TestRemoved(int id)
        {
            var dataAccess = DatabaseAccess<Test>.Factory(_connectionString);
            var examForRemoving = dataAccess.ReadAll().Where(e => e.TestId == id).FirstOrDefault();
            dataAccess.Delete(examForRemoving);
        }

        /// <summary>
        /// Adds student to the data base.
        /// </summary>
        [Order(8)]
        [TestCase(4, "Medved", "Gordei", "Petrovich", "Male", "20.01.1999", "ITI-41")]
        public void Add_StudentSaved(int id, string firstName, string middleName, string lastName, string gender, string dateOfBirthday, string group)
        {
            var dataAccess = DatabaseAccess<Student>.Factory(_connectionString);
            var student = new Student()
            {
                StudentId = id,
                FirstName = firstName,
                MiddleName = middleName,
                LastName = lastName,
                Gender = gender,
                DateOfBirthday = DateTime.Parse(dateOfBirthday),
                GroupName = group
            };

            dataAccess.Add(student);
        }

        /// <summary>
        /// Updates student in the data base.
        /// </summary>
        [Order(9)]
        [TestCase(4)]
        public void Update_StudentUpdated(int id)
        {
            var dataAccess = DatabaseAccess<Student>.Factory(_connectionString);
            var student = dataAccess.ReadAll().Where(e => e.StudentId == id).FirstOrDefault();
            student.DateOfBirthday = DateTime.Now;
            dataAccess.Update(student);
        }

        /// <summary>
        /// Reads all student records from the data base.
        /// </summary>
        [Order(10)]
        [Test]
        public void ReadAll_StudentsAreReaded()
        {
            var dataAccess = DatabaseAccess<Student>.Factory(_connectionString);
            var students = dataAccess.ReadAll();
            CollectionAssert.IsNotEmpty(students);
        }

        /// <summary>
        /// Renoves student from the data base.
        /// </summary>
        [Order(11)]
        [TestCase(4)]
        public void Delete_StudentRemoved(int id)
        {
            var dataAccess = DatabaseAccess<Student>.Factory(_connectionString);
            var student = dataAccess.ReadAll().Where(e => e.StudentId == id).FirstOrDefault();
            dataAccess.Delete(student);
        }

        #endregion
    }
}