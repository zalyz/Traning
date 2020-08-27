using System;

namespace DatabaseAccess.ModelClasses
{
    public class Exam
    {
        public int ExamId { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public string GroupName { get; set; }
    }
}
