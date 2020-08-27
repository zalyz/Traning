using DatabaseAccess.Attributes;

namespace DatabaseAccess.ModelClasses
{
    public class ExamResult : IResult
    {
        public int ExamResultId { get; set; }

        [ForeignKey("ExamId")]
        public Exam Exam { get; set; }

        [ForeignKey("StudentId")]
        public Student Student { get; set; }

        public int ExamId { get; set; }
        
        public int StudentId { get; set; }

        public int Mark { get; set; }
    }
}
