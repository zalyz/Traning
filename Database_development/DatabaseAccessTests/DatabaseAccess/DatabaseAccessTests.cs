using DatabaseAccess.ModelClasses;
using System;
using NUnit.Framework;

namespace DatabaseAccess.Tests
{
    [TestFixture]
    public class DatabaseAccessTests
    {
        [TestCase(6, "Fiz", 5, "IP-51", 5, "Rewy", "Gleb", "Ivan", "Female", 4)]
        public void AddTest_TestResultSaved(int resultId, string testName, int testId, string groupName,
            int studentId ,string fName, string midName, string lName, string gender, int mark)
        {
            var connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\slava\Desktop\Traning\Database_development\DatabaseAccess\Session.mdf;Integrated Security=True";
            var databaseAccess = DatabaseAccess<TestResult>.Factory(connectionString);
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
        
        [TestCase(3, "OAIP", 3, "IP-41", 4, "Aleks", "Rewil", "Clow", "Male", 4)]
        public void AddTest_ExamResultSaved(int resultId, string testName, int examId, string groupName,
            int studentId ,string fName, string midName, string lName, string gender, int mark)
        {
            var connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\slava\Desktop\Traning\Database_development\DatabaseAccess\Session.mdf;Integrated Security=True";
            var databaseAccess = DatabaseAccess<ExamResult>.Factory(connectionString);
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

        [TestCase(1, "SUBD", 1, "IP-11", 1, "Bob", "Marley", "Bob", "Male", 10)]
        public void Delete_TestResultDeleted(int resultId, string testName, int testId, string groupName,
            int studentId, string fName, string midName, string lName, string gender, int mark)
        {
            var connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\slava\Desktop\Traning\Database_development\DatabaseAccess\Session.mdf;Integrated Security=True";
            var databaseAccess = DatabaseAccess<TestResult>.Factory(connectionString);
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

        [Test]
        public void ReadAll_TestResultsAreReaded()
        {
            var connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\slava\Desktop\Traning\Database_development\DatabaseAccess\Session.mdf;Integrated Security=True";
            var databaseAccess = DatabaseAccess<TestResult>.Factory(connectionString);
            var asd = databaseAccess.ReadAll();
        }

        [TestCase(3, "OAIP", 3, "IP-41", 4, "Aleks", "Rewil", "Clow", "Male", 4)]
        public void Update_EaxamResultUpdated(int resultId, string testName, int examId, string groupName,
            int studentId, string fName, string midName, string lName, string gender, int mark)
        {
            var connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\slava\Desktop\Traning\Database_development\DatabaseAccess\Session.mdf;Integrated Security=True";
            var databaseAccess = DatabaseAccess<ExamResult>.Factory(connectionString);
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

        [Test]
        public void FactoryTest()
        {
            var connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\slava\Desktop\Traning\Database_development\DatabaseAccess\Session.mdf;Integrated Security=True";
            var databaseAccess = DatabaseAccess<ExamResult>.Factory(connectionString);
        }
    }
}