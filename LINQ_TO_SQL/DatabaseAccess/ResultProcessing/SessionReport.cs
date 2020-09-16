using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccess.ResultProcessing
{
    /// <summary>
    /// Represents the essence of the report of the session.
    /// </summary>
    public class SessionReport : IReport
    {
        /// <inheritdoc/>
        public string SessionName { get; set; }

        /// <inheritdoc/>
        public double AverageMark { get; set; }

        /// <inheritdoc/>
        public string Criteria { get; set; }
    }
}
