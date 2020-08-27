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
    }
}
