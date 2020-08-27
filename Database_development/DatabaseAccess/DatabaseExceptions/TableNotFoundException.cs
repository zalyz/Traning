using System.Data.Common;

namespace DatabaseAccess.DatabaseExceptions
{
    class TableNotFoundException : DbException
    {
        public TableNotFoundException(string message) : base(message)
        {
        }
    }
}
