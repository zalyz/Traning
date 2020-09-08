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

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return Equals((Exam)obj);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="exam">Exam for comparing.</param>
        /// <returns>True if object is equal, False otherwise.</returns>
        public bool Equals(Exam exam)
        {
            return this.ExamId == exam.ExamId &&
                string.Equals(Name, exam.Name) &&
                DateTime.Equals(Date, exam.Date) &&
                string.Equals(GroupName, exam.GroupName);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return (ExamId, Name, Date, GroupName).GetHashCode();
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{Name} | {Date} | {GroupName}";
        }
    }
}
