using System;

namespace DatabaseAccess.ModelClasses
{
    /// <summary>
    /// Represent the essence of the test.
    /// </summary>
    public class Test
    {
        /// <summary>
        /// Gets or sets Test Id.
        /// </summary>
        public int TestId { get; set; }

        /// <summary>
        /// Gets or sets Test name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets Test date.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets group name.
        /// </summary>
        public string GroupName { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return Equals((Test)obj);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="exam">Exam for comparing.</param>
        /// <returns>True if object is equal, False otherwise.</returns>
        public bool Equals(Test exam)
        {
            return this.TestId == exam.TestId &&
                string.Equals(Name, exam.Name) &&
                DateTime.Equals(Date, exam.Date) &&
                string.Equals(GroupName, exam.GroupName);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return (TestId, Name, Date, GroupName).GetHashCode();
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{Name} | {Date} | {GroupName}";
        }
    }
}
