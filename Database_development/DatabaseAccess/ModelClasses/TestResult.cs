using DatabaseAccess.Attributes;

namespace DatabaseAccess.ModelClasses
{
    /// <summary>
    /// Represent the essence of the test result.
    /// </summary>
    public class TestResult : IResult
    {
        /// <summary>
        /// Gets or sets Test result id.
        /// </summary>
        public int TestResultId { get; set; }

        /// <summary>
        /// Gets or sets Test.
        /// </summary>
        [ForeignKey("TestId")]
        public Test Test { get; set; }

        /// <inheritdoc/>
        [ForeignKey("StudentId")]
        public Student Student { get; set; }

        /// <summary>
        /// Gets or sets Test id.
        /// </summary>
        public int TestId { get; set; }

        /// <summary>
        /// Gets or sets Student id.
        /// </summary>
        public int StudentId { get; set; }

        /// <inheritdoc/>
        public int Mark { get; set; }
    }
}
