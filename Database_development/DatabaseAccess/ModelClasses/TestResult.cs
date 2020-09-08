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

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return Equals((TestResult)obj);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="testResult">Test result for comparing.</param>
        /// <returns>True if object is equal, False otherwise.</returns>
        public bool Equals(TestResult testResult)
        {
            return TestResultId == testResult.TestResultId &&
                Test.Equals(testResult.Test) &&
                Student.Equals(testResult.Student) &&
                TestId == testResult.TestId &&
                StudentId == testResult.StudentId &&
                Mark == testResult.Mark;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return (TestResultId, Test, Student, TestId, StudentId, Mark).GetHashCode();
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{Test.ToString()} | {Student.ToString()} | {TestId} | {StudentId} | {Mark}";
        }
    }
}
