using DatabaseAccess.ModelClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseAccess.ResultProcessing
{
    /// <summary>
    /// Defines methods for processing results.
    /// </summary>
    interface IResultProcessing
    {
        /// <summary>
        /// Gets session results.
        /// </summary>
        /// <param name="database">Database which contain test results.</param>
        /// <returns>Collection of object that contains test name, group name, student id, mark.</returns>
        public IEnumerable<(string, string, int, int)> SessionResults(IDatabaseAccess<TestResult> database);

        /// <summary>
        /// Gets session results.
        /// </summary>
        /// <param name="database">Database which contain exam results.</param>
        /// <returns>Collection of object that contains exam name, group name, student id, mark.</returns>
        public IEnumerable<(string, string, int, int)> SessionResults(IDatabaseAccess<ExamResult> database);

        /// <summary>
        /// Gets min, average and max mark in each group.
        /// </summary>
        /// <param name="database">Database which contain test results.</param>
        /// <returns>Collection of object that contains test name, group name, min mark, average mark, max mark.</returns>
        public IEnumerable<(string, string, int, int, int)> SessionMarks(IDatabaseAccess<TestResult> database);

        /// <summary>
        /// Gets min, average and max mark in each group.
        /// </summary>
        /// <param name="database">Database which contain exam results.</param>
        /// <returns>Collection of object that contains exam name, group name, min mark, average mark, max mark.</returns>
        public IEnumerable<(string, string, int, int, int)> SessionMarks(IDatabaseAccess<ExamResult> database);

        /// <summary>
        /// Gets list of Expelled Students.
        /// </summary>
        /// <param name="database">Database which contain test results.</param>
        /// <returns>Collection of object that contains student name, test naem, mark.</returns>
        public IEnumerable<(string, string, int)> GetExpelledStudents(IDatabaseAccess<TestResult> database);

        /// <summary>
        /// Gets list of Expelled Students.
        /// </summary>
        /// <param name="database">Database which contain exam results.</param>
        /// <returns>Collection of object that contains student name, exam naem, mark.</returns>
        public IEnumerable<(string, string, int)> GetExpelledStudents(IDatabaseAccess<ExamResult> database);
    }
}
