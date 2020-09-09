using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccess.ResultProcessing
{
    /// <summary>
    /// Defines methods for getting an average score based on certain criteria.
    /// </summary>
    interface IResultProcessing
    {
        /// <summary>
        /// Returns average score by specialty.
        /// </summary>
        /// <param name="context">Context for accessing the database.</param>
        /// <returns>Collection that contains time of year, average mark and group name.</returns>
        IEnumerable<(string, double, string)> AverageScoreInSpecialty(SessionDataContext context);

        /// <summary>
        /// Returns average score by teacher.
        /// </summary>
        /// <param name="context">Context for accessing the database.</param>
        /// <returns>Collection that contains time of year, average mark and teacher name.</returns>
        IEnumerable<(string, double, string)> AverageScoreByTeacher(SessionDataContext context);

        /// <summary>
        /// Returns average score by test and exam names.
        /// </summary>
        /// <param name="context">Context for accessing the database.</param>
        /// <returns>Collection that contains time of year, average mark and test or exam names.</returns>
        IEnumerable<(string, double, string)> DynamicsOfTheAverageScore(SessionDataContext context);
    }
}
