using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryTree
{
    public class TestResult : IComparable
    {
        public string StudentName { get; private set; }
        
        public string TestName { get; private set; }
        
        public DateTime Date { get; private set; }

        public int TestScore { get; private set; }

        public TestResult(string studentName, string testName, DateTime date, int testScore)
        {
            StudentName = studentName;
            TestName = testName;
            Date = date;
            TestName = testName;
        }

        int IComparable.CompareTo(object obj)
        {
            return CompareTo(obj as TestResult);
        }

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
    }
}
