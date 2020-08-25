using System;
using System.Collections.Generic;
using System.Text;

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
