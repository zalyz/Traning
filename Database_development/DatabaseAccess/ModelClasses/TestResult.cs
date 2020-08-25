using DatabaseAccess.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseAccess.ModelClasses
{
    class TestResult
    {
        [ForeignKey("ExamId")]
        public Test Test { get; set; }

        [ForeignKey("StudentId")]
        public Student Student { get; set; }

        public int TestId { get; set; }

        public int StudentId { get; set; }

        public int Mark { get; set; }
    }
}
