using DatabaseAccess.Attributes;

namespace DatabaseAccess.ModelClasses
{
    /// <summary>
    /// Represent the essence of the exam result.
    /// </summary>
    public class ExamResult : IResult
    {
        /// <summary>
        /// Gets or sets Exam result id.
        /// </summary>
        public int ExamResultId { get; set; }

        /// <summary>
        /// Gets or sets Exam.
        /// </summary>
        [ForeignKey("ExamId")]
        public Exam Exam { get; set; }

        /// <inheritdoc/>
        [ForeignKey("StudentId")]
        public Student Student { get; set; }

        /// <summary>
        /// Gets or sets Exam id.
        /// </summary>
        public int ExamId { get; set; }
        
        /// <summary>
        /// Gets or sets Student id.
        /// </summary>
        public int StudentId { get; set; }

        /// <inheritdoc/>
        public int Mark { get; set; }
    }
}
