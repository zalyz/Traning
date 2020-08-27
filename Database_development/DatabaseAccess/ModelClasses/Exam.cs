using System;

namespace DatabaseAccess.ModelClasses
{
    /// <summary>
    /// Represent the essence of the exam.
    /// </summary>
    public class Exam
    {
        /// <summary>
        /// Gets or sets Exam Id.
        /// </summary>
        public int ExamId { get; set; }

        /// <summary>
        /// Gets or sets Exam name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets Exam date.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets group name.
        /// </summary>
        public string GroupName { get; set; }
    }
}
