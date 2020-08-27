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
    [TestFixture]
    public class XlsxFormatTests
    {
        [Test]
        public void SaveToFile_SessionExamResult()
        {
            var connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\slava\Desktop\Traning\Database_development\DatabaseAccess\Session.mdf;Integrated Security=True";
            var dataAccess = DatabaseAccess<ExamResult>.Factory(connectionString);
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

        [Test]
        public void SaveToFile_SessionTestResult()
        {
            var connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\slava\Desktop\Traning\Database_development\DatabaseAccess\Session.mdf;Integrated Security=True";
            var dataAccess = DatabaseAccess<TestResult>.Factory(connectionString);
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

        [Test]
        public void SaveToFile_ExamMinMiddleMaxMarks()
        {
            var connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\slava\Desktop\Traning\Database_development\DatabaseAccess\Session.mdf;Integrated Security=True";
            var dataAccess = DatabaseAccess<ExamResult>.Factory(connectionString);
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
        
        [Test]
        public void SaveToFile_TestMinMiddleMaxMarks()
        {
            var connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\slava\Desktop\Traning\Database_development\DatabaseAccess\Session.mdf;Integrated Security=True";
            var dataAccess = DatabaseAccess<TestResult>.Factory(connectionString);
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

        [Test]
        public void SaveToFile_ExpelledStudents()
        {
            var connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\slava\Desktop\Traning\Database_development\DatabaseAccess\Session.mdf;Integrated Security=True";
            var dataAccessTest = DatabaseAccess<TestResult>.Factory(connectionString);
            var dataAccessResult = DatabaseAccess<ExamResult>.Factory(connectionString);
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