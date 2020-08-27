using System.Data.Common;

namespace DatabaseAccess.DatabaseExceptions
{
    /// <summary>
    /// Occurs when Id property is not found.
    /// </summary>
    class IdPropertyNotFoundException : DbException
    {
        /// <summary>
        /// Create instance of IdPropertyNotFoundException.
        /// </summary>
        /// <param name="message">Exception information.</param>
        public IdPropertyNotFoundException(string message) : base(message)
        {
        }
    }
}
