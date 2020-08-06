using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Text;

namespace BinaryTree
{
    /// <summary>
    /// Represents the essence of the student's test result.
    /// </summary>
    [Serializable]
    public class TestResult : IComparable
    {
        /// <summary>
        /// Gets or sets student name.
        /// </summary>
        public string StudentName { get; set; }
        
        /// <summary>
        /// Gets or sets test name.
        /// </summary>
        public string TestName { get; set; }
        
        /// <summary>
        /// Gets or sets test date.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets test score.
        /// </summary>
        public int TestScore { get; set; }

        public TestResult()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="studentName"></param>
        /// <param name="testName"></param>
        /// <param name="date"></param>
        /// <param name="testScore"></param>
        public TestResult(string studentName, string testName, DateTime date, int testScore)
        {
            StudentName = studentName;
            TestName = testName;
            Date = date;
            TestScore = testScore;
        }

        /// <inheritdoc/>
        int IComparable.CompareTo(object obj)
        {
            return CompareTo(obj as TestResult);
        }

        /// <summary>
        /// Compares two instance of TestResult class.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        int CompareTo(TestResult other)
        {
            if (other == null)
                throw new ArgumentNullException();

            if (TestScore == other.TestScore)
            {
                return 0;
            }
            else
            {
                if (TestScore < other.TestScore)
                {
                    return -1;
                }
                else
                {
                    return 1;
                }
            }
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as TestResult);
        }

        public bool Equals(TestResult testResult)
        {
            return string.Equals(StudentName, testResult.StudentName) &&
                string.Equals(TestName, testResult.TestName) &&
                DateTime.Equals(Date, testResult.Date) &&
                TestScore == testResult.TestScore;
        }

        public override int GetHashCode()
        {
            return (StudentName, TestName, Date, TestScore).GetHashCode();
        }
    }
}
