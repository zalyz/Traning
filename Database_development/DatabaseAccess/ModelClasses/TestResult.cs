using DatabaseAccess.Attributes;

namespace DatabaseAccess.ModelClasses
{
    public class TestResult : IResult
    {
        public int TestResultId { get; set; }

        [ForeignKey("TestId")]
        public Test Test { get; set; }

        [ForeignKey("StudentId")]
        public Student Student { get; set; }

        public int TestId { get; set; }

        public int StudentId { get; set; }

        public int Mark { get; set; }
    }
}
