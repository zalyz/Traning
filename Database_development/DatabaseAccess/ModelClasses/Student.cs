using System;

namespace DatabaseAccess.ModelClasses
{
    /// <summary>
    /// Represent the essence of the student.
    /// </summary>
    public class Student
    {
        /// <summary>
        /// Gets or sets student id.
        /// </summary>
        public int StudentId { get; set; }

        /// <summary>
        /// Gets or sets first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets middle name.
        /// </summary>
        public string MiddleName { get; set; }
        
        /// <summary>
        /// Gets or sets last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets gender.
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// Gets or sets Date of birthday.
        /// </summary>
        public DateTime DateOfBirthday { get; set; }

        /// <summary>
        /// Gets or sets group name.
        /// </summary>
        public string GroupName { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return Equals((Student)obj);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="student">Student for comparing.</param>
        /// <returns>True if object is equal, False otherwise.</returns>
        private bool Equals(Student student)
        {
            return StudentId == student.StudentId &&
                string.Equals(FirstName, student.FirstName) &&
                string.Equals(MiddleName, student.MiddleName) &&
                string.Equals(LastName, student.LastName) &&
                string.Equals(Gender, student.Gender) &&
                DateTime.Equals(DateOfBirthday, student.DateOfBirthday) &&
                string.Equals(GroupName, student.GroupName);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return (StudentId, FirstName, MiddleName, LastName, Gender, DateOfBirthday, GroupName).GetHashCode();
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{FirstName} | {MiddleName} | {LastName} | {Gender} | {DateOfBirthday} | {GroupName}";
        }
    }
}
