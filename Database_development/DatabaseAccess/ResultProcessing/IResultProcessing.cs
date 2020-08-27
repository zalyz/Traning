using DatabaseAccess.ModelClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseAccess.ResultProcessing
{
    interface IResultProcessing
    {
        public IEnumerable<(string, string, int, int)> SessionResults(IDatabaseAccess<TestResult> database);

        public IEnumerable<(string, string, int, int)> SessionResults(IDatabaseAccess<ExamResult> database);

        public IEnumerable<(string, string, int, int, int)> SessionMarks(IDatabaseAccess<TestResult> database);

        public IEnumerable<(string, string, int, int, int)> SessionMarks(IDatabaseAccess<ExamResult> database);

        public IEnumerable<(string, string, int)> GetExpelledStudents(IDatabaseAccess<TestResult> database);

        public IEnumerable<(string, string, int)> GetExpelledStudents(IDatabaseAccess<ExamResult> database);
    }
}
