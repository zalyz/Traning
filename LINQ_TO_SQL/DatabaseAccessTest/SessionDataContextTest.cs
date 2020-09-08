using System;
using System.Linq;
using DatabaseAccess;
using DatabaseAccess.ScriptExecuter;
using NUnit.Framework;

namespace DatabaseAccessTest
{
    [TestFixture]
    public class SessionDataContextTest
    {
        [OneTimeSetUp]
        public void ClassInitialize()
        {
            var filePath = @"C:\Users\slava\Desktop\LINQ_TO_SQL\CreateDatabaseAndFill.sql";
            var connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\slava\Desktop\LINQ_TO_SQL\DatabaseAccess\Session.mdf;Integrated Security=True";
            ScriptExecuter.Execute(filePath, connectionString);
        }

        [TestCase("YAPVU", "21.12.2019", "IP-31", "Shibeko", "Viktor", "Nikolaevich", "Male", "Dranev", "Slava", "Alekseevich", "Male", "7.07.2000", "Semibold")]
        public void Add_TestResultSaved(string testName, string testDate, string groupName, string teacherFName, string teacherMName, string teacherLName, 
            string teacherGender, string studFName, string studMName, string studLName, string studGender, string studBirthday, string groupDescription)
        {
            var connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\slava\Desktop\LINQ_TO_SQL\DatabaseAccess\Session.mdf;Integrated Security=True";
            var database = new SessionDataContext(connectionString);
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
        
        [Order(2)]
        [TestCase("Math", "29.06.2019", "IK-11", "Avaken", "Viktor", "Sergeevich", "Male", "Golovach", "Andrey", "Ivanovich", "Male", "15.10.2001", "Good choice")]
        public void Add_ExamResultSaved(string examName, string examDate, string groupName, string teacherFName, string teacherMName, string teacherLName, 
            string teacherGender, string studFName, string studMName, string studLName, string studGender, string studBirthday, string groupDescription)
        {
            var connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\slava\Desktop\LINQ_TO_SQL\DatabaseAccess\Session.mdf;Integrated Security=True";
            var database = new SessionDataContext(connectionString);
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

        [TestCase("YAPVU", "21.12.2019", "IP-31", "Shibeko", "Viktor", "Nikolaevich", "Male", "Dranev", "Slava", "Alekseevich", "Male", "7.07.2000", "Semibold")]
        public void Read_TestResultReturned(string testName, string testDate, string groupName, string teacherFName, string teacherMName, string teacherLName,
            string teacherGender, string studFName, string studMName, string studLName, string studGender, string studBirthday, string groupDescription)
        {
            var connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\slava\Desktop\LINQ_TO_SQL\DatabaseAccess\Session.mdf;Integrated Security=True";
            var dataAccess = new SessionDataContext(connectionString);
            var expectedTestResult = new DatabaseAccess.TestResult()
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
            var actualTestResult = dataAccess.TestResults.Where(e => e.Test.Name == testName && e.Student.MiddleName == studMName).First();
            
            //Assert.AreEqual(expectedTestResult, actualTestResult);
        }

        [Order(1)]
        [TestCase("Korovai", "Irina", "Olegovna", "Female", "10.5.2001", "IP-21", "FAIS")]
        public void Update_ExamResultUpdated(string studFName, string studMName, string studLName, string studGender, string studBirthday, string groupName, string groupDescription)
        {
            var connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\slava\Desktop\LINQ_TO_SQL\DatabaseAccess\Session.mdf;Integrated Security=True";
            var dataAccess = new SessionDataContext(connectionString);
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

            var examResultForUpdate = dataAccess.GetTable<ExamResult>().FirstOrDefault();
            examResultForUpdate.Mark = 10;
            dataAccess.SubmitChanges();
        }

        [Order(3)]
        [TestCase("Golovach", "Andrey", "Ivanovich", "Male", "15.10.2001", "IK-11", "Good choice")]
        public void Delete_ExamResultRemoed(string studFName, string studMName, string studLName, string studGender, string studBirthday, string groupName, string groupDescription)
        {
            var connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\slava\Desktop\LINQ_TO_SQL\DatabaseAccess\Session.mdf;Integrated Security=True";
            var dataAccess = new SessionDataContext(connectionString);
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
