using DatabaseAccess.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseAccess.ModelClasses
{
    class ExamResult
    {
        [ForeignKey("ExamId")]
        public Exam Exam { get; set; }

        [ForeignKey("StudentId")]
        public Student Student { get; set; }

        public int ExamId { get; set; }
        
        public int StudentId { get; set; }

        public int Mark { get; set; }
    }
}
