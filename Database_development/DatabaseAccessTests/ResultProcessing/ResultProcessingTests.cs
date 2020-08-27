using NUnit.Framework;
using DatabaseAccess.ResultProcessing;
using System;
using System.Collections.Generic;
using System.Text;
using DatabaseAccess.ModelClasses;

namespace DatabaseAccess.ResultProcessing.Tests
{
    [TestFixture]
    public class ResultProcessingTests
    {
        [Test]
        public void GetExpelledStudents_ExpelledStudentsByTestReturned()
        {
            var connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\slava\Desktop\Traning\Database_development\DatabaseAccess\Session.mdf;Integrated Security=True";
            var dataAccessTest = DatabaseAccess<TestResult>.Factory(connectionString);
            var resProc = new ResultProcessing();
            var results = resProc.GetExpelledStudents(dataAccessTest);
        }

        [Test]
        public void GetExpelledStudents_ExpelledStudentsByExamReturned()
        {
            var connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\slava\Desktop\Traning\Database_development\DatabaseAccess\Session.mdf;Integrated Security=True";
            var dataAccessExam = DatabaseAccess<ExamResult>.Factory(connectionString);
            var resProc = new ResultProcessing();
            var results = resProc.GetExpelledStudents(dataAccessExam);
        }

        [Test]
        public void SessionMarks_TestMarksReturned()
        {
            var connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\slava\Desktop\Traning\Database_development\DatabaseAccess\Session.mdf;Integrated Security=True";
            var dataAccess = DatabaseAccess<TestResult>.Factory(connectionString);
            var resProc = new ResultProcessing();
            var results = resProc.SessionResults(dataAccess);
        }

        [Test]
        public void SessionMarks_ExamMarksReturned()
        {
            var connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\slava\Desktop\Traning\Database_development\DatabaseAccess\Session.mdf;Integrated Security=True";
            var dataAccess = DatabaseAccess<ExamResult>.Factory(connectionString);
            var resProc = new ResultProcessing();
            var results = resProc.SessionResults(dataAccess);
        }

        [Test]
        public void SessionResults_TestResultsReturned()
        {
            var connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\slava\Desktop\Traning\Database_development\DatabaseAccess\Session.mdf;Integrated Security=True";
            var dataAccess = DatabaseAccess<TestResult>.Factory(connectionString);
            var resProc = new ResultProcessing();
            var results = resProc.SessionResults(dataAccess);
        }

        [Test]
        public void SessionResults_ExamResultsReturned()
        {
            var connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\slava\Desktop\Traning\Database_development\DatabaseAccess\Session.mdf;Integrated Security=True";
            var dataAccess = DatabaseAccess<ExamResult>.Factory(connectionString);
            var resProc = new ResultProcessing();
            var result = resProc.SessionResults(dataAccess);
        }
    }
}