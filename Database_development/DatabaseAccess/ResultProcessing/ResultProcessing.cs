using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using DatabaseAccess.ModelClasses;

namespace DatabaseAccess.ResultProcessing
{
    /// <summary>
    /// Contains methods for the proseccing result.
    /// </summary>
    public class ResultProcessing : IResultProcessing
    {
        /// <inheritdoc/>
        public IEnumerable<(string, string, int)> GetExpelledStudents(IDatabaseAccess<TestResult> database)
        {
            var listOfTestResult = database.ReadAll().ToList();
            var groupedTest = listOfTestResult.Where(e => e.Mark < 5).GroupBy(e => e.Student.GroupName);
            var listForFile = new List<(string, string, int)>();
            foreach (var test in groupedTest)
            {
                foreach (var groupeKey in test)
                {
                    (string StudentName, string TestName, int Mark) record;
                    record.StudentName = $"{groupeKey.Student.FirstName} {groupeKey.Student.MiddleName} {groupeKey.Student.LastName}";
                    record.TestName = groupeKey.Test.Name;
                    record.Mark = groupeKey.Mark;
                    listForFile.Add(record);
                }
            }

            return listForFile;
        }

        /// <inheritdoc/>
        public IEnumerable<(string, string, int)> GetExpelledStudents(IDatabaseAccess<ExamResult> database)
        {
            var listOfExamResult = database.ReadAll().ToList();
            var groupedExam = listOfExamResult.Where(e => e.Mark < 5).GroupBy(e => e.Student.GroupName);
            var listForFile = new List<(string, string, int)>();
            foreach (var exam in groupedExam)
            {
                foreach (var groupeKey in exam)
                {
                    (string StudentName, string TestName, int Mark) record;
                    record.StudentName = $"{groupeKey.Student.FirstName} {groupeKey.Student.MiddleName} {groupeKey.Student.LastName}";
                    record.TestName = groupeKey.Exam.Name;
                    record.Mark = groupeKey.Mark;
                    listForFile.Add(record);
                }
            }

            return listForFile;
        }

        /// <inheritdoc/>
        public IEnumerable<(string, string, int, int, int)> SessionMarks(IDatabaseAccess<TestResult> database)
        {
            var listOfExamResult = database.ReadAll().ToList();
            var groupedByExamName = listOfExamResult.GroupBy(e => e.Test.Name);
            var listForFile = new List<(string, string, int, int, int)>();
            foreach (var exam in groupedByExamName)
            {
                var groupedByGroupe = exam.GroupBy(e => e.Student.GroupName);
                foreach (var groupeKey in groupedByGroupe)
                {
                    (string ExamName, string Groupe, int MinMark, int AverageMark, int MaxMark) record;
                    record.ExamName = exam.Key;
                    record.Groupe = groupeKey.Key;
                    record.MinMark = groupeKey.Min(e => e.Mark);
                    record.AverageMark = (groupeKey.Sum(e => e.Mark) / groupeKey.Count());
                    record.MaxMark = groupeKey.Max(e => e.Mark);
                    listForFile.Add(record);
                }
            }

            return listForFile;
        }

        /// <inheritdoc/>
        public IEnumerable<(string, string, int, int, int)> SessionMarks(IDatabaseAccess<ExamResult> database)
        {
            var listOfExamResult = database.ReadAll().ToList();
            var groupedByExamName = listOfExamResult.GroupBy(e => e.Exam.Name);
            var listForFile = new List<(string, string, int, int, int)>();
            foreach (var exam in groupedByExamName)
            {
                var groupedByGroupe = exam.GroupBy(e => e.Student.GroupName);
                foreach (var groupeKey in groupedByGroupe)
                {
                    (string ExamName, string Groupe, int MinMark, int AverageMark, int MaxMark) record;
                    record.ExamName = exam.Key;
                    record.Groupe = groupeKey.Key;
                    record.MinMark = groupeKey.Min(e => e.Mark);
                    record.AverageMark = (groupeKey.Sum(e => e.Mark) / groupeKey.Count());
                    record.MaxMark = groupeKey.Max(e => e.Mark);
                    listForFile.Add(record);
                }
            }

            return listForFile;
        }

        /// <inheritdoc/>
        public IEnumerable<(string, string, int, int)> SessionResults(IDatabaseAccess<TestResult> database)
        {
            var listOfExamResult = database.ReadAll().ToList();
            var groupedList = listOfExamResult.GroupBy(e => e.TestId);
            var listForFile = new List<(string, string, int, int)>();
            foreach (var group in groupedList)
            {
                foreach (var item in group)
                {
                    (string Test, string Group, int StudentId, int Mark) record;
                    record.Test = item.Test.Name;
                    record.Group = item.Test.GroupName;
                    record.StudentId = item.StudentId;
                    record.Mark = item.Mark;
                    listForFile.Add(record);
                }
            }

            return listForFile;
        }

        /// <inheritdoc/>
        public IEnumerable<(string, string, int, int)> SessionResults(IDatabaseAccess<ExamResult> database)
        {
            var listOfExamResult = database.ReadAll().ToList();
            var groupedList = listOfExamResult.GroupBy(e => e.ExamId);
            var listForFile = new List<(string, string, int, int)>();
            foreach (var group in groupedList)
            {
                foreach (var item in group)
                {
                    (string Exam, string Group, int StudentId, int Mark) record;
                    record.Exam = item.Exam.Name;
                    record.Group = item.Exam.GroupName;
                    record.StudentId = item.StudentId;
                    record.Mark = item.Mark;
                    listForFile.Add(record);
                }
            }

            return listForFile;
        }
    }
}
