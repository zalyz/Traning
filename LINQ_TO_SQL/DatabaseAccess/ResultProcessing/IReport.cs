using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccess.ResultProcessing
{
    /// <summary>
    /// Defines properties for report.
    /// </summary>
    public interface IReport
    {
        /// <summary>
        /// Gets or sets session name.
        /// </summary>
        string SessionName { get; set; }

        /// <summary>
        /// Gets or sets average mark.
        /// </summary>
        double AverageMark { get; set; }

        /// <summary>
        /// Gets or sets criteria of report.
        /// </summary>
        string Criteria { get; set; }
    }
}
