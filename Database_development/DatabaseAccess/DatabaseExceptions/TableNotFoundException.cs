using System.Data.Common;

namespace DatabaseAccess.DatabaseExceptions
{
    /// <summary>
    /// Occurs when Table is not found.
    /// </summary>
    class TableNotFoundException : DbException
    {
        /// <summary>
        /// Create instance of TableNotFoundException.
        /// </summary>
        /// <param name="message">Exception information.</param>
        public TableNotFoundException(string message) : base(message)
        {
        }
    }
}
