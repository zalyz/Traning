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

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return Equals((ExamResult)obj);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="examResult">Exam result for comparing.</param>
        /// <returns>True if object is equal, False otherwise.</returns>
        public bool Equals(ExamResult examResult)
        {
            return ExamResultId == examResult.ExamResultId &&
                Exam.Equals(examResult.Exam) &&
                Student.Equals(examResult.Student) &&
                ExamId == examResult.ExamId &&
                StudentId == examResult.StudentId &&
                Mark == examResult.Mark;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return (ExamResultId, Exam, Student, ExamId, StudentId, Mark).GetHashCode();
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{Exam.ToString()} | {Student.ToString()} | {ExamId} | {StudentId} | {Mark}";
        }
    }
}
