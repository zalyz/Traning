using DatabaseAccess.ExcelSerialization;
using DatabaseAccess.ResultProcessing;
using NUnit.Framework;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using DatabaseAccess.ModelClasses;

namespace DatabaseAccess.ExcelSerialization.Tests
{
    /// <summary>
    /// Defines methods for testing XlsxFormat class.
    /// </summary>
    [TestFixture]
    public class XlsxFormatTests
    {
        private readonly string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\slava\Desktop\Traning\Database_development\DatabaseAccess\Session.mdf;Integrated Security=True";
        
        /// <summary>
        /// Creates and loads to file exam results data.
        /// </summary>
        [Test]
        public void SaveToFile_SessionExamResult()
        {
            var dataAccess = DatabaseAccess<ExamResult>.Factory(_connectionString);
            var resProc = new ResultProcessing.ResultProcessing();
            var listForFile = resProc.SessionResults(dataAccess);
            var columnNames = new string[]
            {
                "ExamName",
                "Group",
                "StudentId",
                "Mark"
            };

            var xlsxFormat = new XlsxFormat<(string, string, int, int)>(@"../../../SessionExamResult.xlsx");
            xlsxFormat.SaveToFile(columnNames, listForFile);
        }

        /// <summary>
        /// Creates and loads to file test results data.
        /// </summary>
        [Test]
        public void SaveToFile_SessionTestResult()
        {
            var dataAccess = DatabaseAccess<TestResult>.Factory(_connectionString);
            var resProc = new ResultProcessing.ResultProcessing();
            var listForFile = resProc.SessionResults(dataAccess);
            var columnNames = new string[]
            {
                "TestName",
                "Group",
                "StudentId",
                "Mark"
            };

            var xlsxFormat = new XlsxFormat<(string, string, int, int)>(@"../../../SessionTestResult.xlsx");
            xlsxFormat.SaveToFile(columnNames, listForFile);
        }

        /// <summary>
        /// Creates and loads to file Min, middle and max exam mark of the students.
        /// </summary>
        [Test]
        public void SaveToFile_ExamMinMiddleMaxMarks()
        {
            var dataAccess = DatabaseAccess<ExamResult>.Factory(_connectionString);
            var resProc = new ResultProcessing.ResultProcessing();
            var listForFile = resProc.SessionMarks(dataAccess);

            var columnNames = new string[]
            {
                "ExamName",
                "Group",
                "MinMark",
                "AverageMark",
                "MaxMark"
            };

            var xlsxFormat = new XlsxFormat<(string, string, int, int, int)>(@"../../../SessionExamMarks.xlsx");
            xlsxFormat.SaveToFile(columnNames, listForFile);
        }

        /// <summary>
        /// Creates and loads to file Min, middle and max test mark of the students.
        /// </summary>
        [Test]
        public void SaveToFile_TestMinMiddleMaxMarks()
        {
            var dataAccess = DatabaseAccess<TestResult>.Factory(_connectionString);
            var resProc = new ResultProcessing.ResultProcessing();
            var listForFile = resProc.SessionMarks(dataAccess);

            var columnNames = new string[]
            {
                "TestName",
                "Group",
                "MinMark",
                "AverageMark",
                "MaxMark"
            };

            var xlsxFormat = new XlsxFormat<(string, string, int, int, int)>(@"../../../SessionTestMarks.xlsx");
            xlsxFormat.SaveToFile(columnNames, listForFile);
        }

        /// <summary>
        /// Creates and loades to file list of expelled students.
        /// </summary>
        [Test]
        public void SaveToFile_ExpelledStudents()
        {
            var dataAccessTest = DatabaseAccess<TestResult>.Factory(_connectionString);
            var dataAccessResult = DatabaseAccess<ExamResult>.Factory(_connectionString);
            var resProc = new ResultProcessing.ResultProcessing();
            var listOfTestResult = resProc.GetExpelledStudents(dataAccessResult);
            var listOfExamResult = resProc.GetExpelledStudents(dataAccessTest);
            var listForFile = new List<(string, string, int)>(listOfTestResult);
            listForFile.AddRange(listOfExamResult);
            var columnNames = new string[]
            {
                "StudentName",
                "TestName",
                "Mark"
            };

            var xlsxFormat = new XlsxFormat<(string, string, int)>(@"../../../SessionExpelledStudents.xlsx");
            xlsxFormat.SaveToFile(columnNames, listForFile);
        }
    }
}