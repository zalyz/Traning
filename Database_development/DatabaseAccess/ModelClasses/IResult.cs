using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseAccess.ModelClasses
{
    /// <summary>
    /// Defines properties for represents of the Student's result.
    /// </summary>
    interface IResult
    {
        /// <summary>
        /// Gets or sets Student.
        /// </summary>
        public Student Student { get; set; }

        /// <summary>
        /// Gets or sets mark.
        /// </summary>
        public int Mark { get; set; }
    }
}
