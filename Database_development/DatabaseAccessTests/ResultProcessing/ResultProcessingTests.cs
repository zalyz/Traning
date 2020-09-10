using NUnit.Framework;
using DatabaseAccess.ResultProcessing;
using System;
using System.Collections.Generic;
using System.Text;
using DatabaseAccess.ModelClasses;

namespace DatabaseAccess.ResultProcessing.Tests
{
    /// <summary>
    /// Defines methods for testing ResultProcessing class.
    /// </summary>
    [TestFixture]
    public class ResultProcessingTests
    {
        private readonly string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\slava\Desktop\Traning\Database_development\DatabaseAccess\Session.mdf;Integrated Security=True";
        
        /// <summary>
        /// Gets collection of expelled students by test reults.
        /// </summary>
        [Test]
        public void GetExpelledStudents_ExpelledStudentsByTestReturned()
        {
            var dataAccessTest = DatabaseAccess<TestResult>.Factory(_connectionString);
            var resProc = new ResultProcessing();
            var results = resProc.GetExpelledStudents(dataAccessTest);
        }

        /// <summary>
        /// Gets collection of expelled students by exam reults.
        /// </summary>
        [Test]
        public void GetExpelledStudents_ExpelledStudentsByExamReturned()
        {
            var dataAccessExam = DatabaseAccess<ExamResult>.Factory(_connectionString);
            var resProc = new ResultProcessing();
            var results = resProc.GetExpelledStudents(dataAccessExam);
        }

        /// <summary>
        /// Gets collection of test marks.
        /// </summary>
        [Test]
        public void SessionMarks_TestMarksReturned()
        {
            var dataAccess = DatabaseAccess<TestResult>.Factory(_connectionString);
            var resProc = new ResultProcessing();
            var results = resProc.SessionMarks(dataAccess);
        }

        /// <summary>
        /// Gets collection of exam marks.
        /// </summary>
        [Test]
        public void SessionMarks_ExamMarksReturned()
        {
            var dataAccess = DatabaseAccess<ExamResult>.Factory(_connectionString);
            var resProc = new ResultProcessing();
            var results = resProc.SessionMarks(dataAccess);
        }

        /// <summary>
        /// Gets collection of test result.
        /// </summary>
        [Test]
        public void SessionResults_TestResultsReturned()
        {
            var dataAccess = DatabaseAccess<TestResult>.Factory(_connectionString);
            var resProc = new ResultProcessing();
            var results = resProc.SessionResults(dataAccess);
        }

        /// <summary>
        /// Gets collection of exam result.
        /// </summary>
        [Test]
        public void SessionResults_ExamResultsReturned()
        {
            var dataAccess = DatabaseAccess<ExamResult>.Factory(_connectionString);
            var resProc = new ResultProcessing();
            var result = resProc.SessionResults(dataAccess);
        }
    }
}