using System;
using System.Linq;
using DatabaseAccess;
using DatabaseAccess.ScriptExecuter;
using NUnit.Framework;

namespace DatabaseAccessTest
{
    /// <summary>
    /// Defines methods for testing SessionDataContext class.
    /// </summary>
    [TestFixture]
    public class SessionDataContextTest
    {
        /// <summary>
        /// Connection string to the database.
        /// </summary>
        private string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\slava\Desktop\Traning\LINQ_TO_SQL\DatabaseAccess\Session.mdf;Integrated Security=True";

        /// <summary>
        /// Creates tables in the database and fill it.
        /// </summary>
        [OneTimeSetUp]
        public void ClassInitialize()
        {
            var filePath = @"C:\Users\slava\Desktop\LINQ_TO_SQL\CreateDatabaseAndFill.sql";
            ScriptExecuter.Execute(filePath, _connectionString);
        }

        /// <summary>
        /// Adds test result to the database.
        /// </summary>
        [TestCase("YAPVU", "21.12.2019", "IP-31", "Shibeko", "Viktor", "Nikolaevich", "Male", "Dranev", "Slava", "Alekseevich", "Male", "7.07.2000", "Semibold")]
        public void Add_TestResultSaved(string testName, string testDate, string groupName, string teacherFName, string teacherMName, string teacherLName, 
            string teacherGender, string studFName, string studMName, string studLName, string studGender, string studBirthday, string groupDescription)
        {
            var database = new SessionDataContext(_connectionString);
            var testResult = new DatabaseAccess.TestResult()
            {
                Test = new Test()
                {
                    Name = testName,
                    Date = DateTime.Parse(testDate),
                    GroupName = groupName,
                    Teacher = new Teacher()
                    {
                        FirstName = teacherFName,
                        MiddleName = teacherMName,
                        LastName = teacherLName,
                        Gender = teacherGender
                    },
                },
                Student = new Student()
                {
                    FirstName = studFName,
                    MiddleName = studMName,
                    LastName = studLName,
                    Gender = studGender,
                    DateOfBirthday = DateTime.Parse(studBirthday),
                    StudentGroup = new StudentGroup()
                    {
                        Name = groupName,
                        Description = groupDescription
                    }
                },
                Mark = 10
            };

            database.TestResults.InsertOnSubmit(testResult);
            database.SubmitChanges();
        }
        
        /// <summary>
        /// Adds exam result to the database.
        /// </summary>
        [Order(2)]
        [TestCase("Math", "29.06.2019", "IK-11", "Avaken", "Viktor", "Sergeevich", "Male", "Golovach", "Andrey", "Ivanovich", "Male", "15.10.2001", "Good choice")]
        public void Add_ExamResultSaved(string examName, string examDate, string groupName, string teacherFName, string teacherMName, string teacherLName, 
            string teacherGender, string studFName, string studMName, string studLName, string studGender, string studBirthday, string groupDescription)
        {
            var database = new SessionDataContext(_connectionString);
            var examResult = new ExamResult()
            {
                Exam = new Exam()
                {
                    Name = examName,
                    Date = DateTime.Parse(examDate),
                    GroupName = groupName,
                    Teacher = new Teacher()
                    {
                        FirstName = teacherFName,
                        MiddleName = teacherMName,
                        LastName = teacherLName,
                        Gender = teacherGender
                    },
                },
                Student = new Student()
                {
                    FirstName = studFName,
                    MiddleName = studMName,
                    LastName = studLName,
                    Gender = studGender,
                    DateOfBirthday = DateTime.Parse(studBirthday),
                    StudentGroup = new StudentGroup()
                    {
                        Name = groupName,
                        Description = groupDescription
                    }
                },
                Mark = 4
            };

            database.ExamResults.InsertOnSubmit(examResult);
            database.SubmitChanges();
        }

        /// <summary>
        /// Reads first test result from the database.
        /// </summary>
        [TestCase("YAPVU", "21.12.2019", "IP-31", "Shibeko", "Viktor", "Nikolaevich", "Male", "Dranev", "Slava", "Alekseevich", "Male", "7.07.2000", "Semibold")]
        public void Read_TestResultReturned(string testName, string testDate, string groupName, string teacherFName, string teacherMName, string teacherLName,
            string teacherGender, string studFName, string studMName, string studLName, string studGender, string studBirthday, string groupDescription)
        {
            var dataAccess = new SessionDataContext(_connectionString);
            var expectedTestResult = new TestResult()
            {
                Test = new Test()
                {
                    Name = testName,
                    Date = DateTime.Parse(testDate),
                    GroupName = groupName,
                    Teacher = new Teacher()
                    {
                        FirstName = teacherFName,
                        MiddleName = teacherMName,
                        LastName = teacherLName,
                        Gender = teacherGender
                    },
                },
                Student = new Student()
                {
                    FirstName = studFName,
                    MiddleName = studMName,
                    LastName = studLName,
                    Gender = studGender,
                    DateOfBirthday = DateTime.Parse(studBirthday),
                    StudentGroup = new StudentGroup()
                    {
                        Name = groupName,
                        Description = groupDescription
                    }
                },
                Mark = 10
            };
            var actualTestResult = dataAccess.TestResults.Where(e => e.Test.Name == testName && e.Student.MiddleName == studMName).FirstOrDefault();
            Assert.IsNotNull(expectedTestResult);
        }

        /// <summary>
        /// Updates exam result in the database.
        /// </summary>
        [Order(1)]
        [TestCase("Korovai", "Irina", "Olegovna", "Female", "10.5.2001", "IP-21", "FAIS")]
        public void Update_ExamResultUpdated(string studFName, string studMName, string studLName, string studGender, string studBirthday, string groupName, string groupDescription)
        {
            var dataAccess = new SessionDataContext(_connectionString);
            var student = new Student()
            {
                FirstName = studFName,
                MiddleName = studMName,
                LastName = studLName,
                Gender = studGender,
                DateOfBirthday = DateTime.Parse(studBirthday),
                StudentGroup = new StudentGroup()
                {
                    Name = groupName,
                    Description = groupDescription
                }
            };

            var examResultForUpdate = dataAccess.ExamResults.FirstOrDefault();
            examResultForUpdate.Mark = 10;
            dataAccess.SubmitChanges();
        }

        /// <summary>
        /// Deletes exam result from database.
        /// </summary>
        [Order(3)]
        [TestCase("Golovach", "Andrey", "Ivanovich", "Male", "15.10.2001", "IK-11", "Good choice")]
        public void Delete_ExamResultRemoed(string studFName, string studMName, string studLName, string studGender, string studBirthday, string groupName, string groupDescription)
        {
            var dataAccess = new SessionDataContext(_connectionString);
            var student = new Student()
            {
                FirstName = studFName,
                MiddleName = studMName,
                LastName = studLName,
                Gender = studGender,
                DateOfBirthday = DateTime.Parse(studBirthday),
                StudentGroup = new StudentGroup()
                {
                    Name = groupName,
                    Description = groupDescription
                }
            };

            var examResult = dataAccess.ExamResults.Where(e => e.Student.MiddleName == student.MiddleName).FirstOrDefault();
            dataAccess.ExamResults.DeleteOnSubmit(examResult);
            dataAccess.SubmitChanges();
        }
    }
}
