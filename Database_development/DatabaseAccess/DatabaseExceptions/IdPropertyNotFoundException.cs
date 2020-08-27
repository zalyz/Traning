using System.Data.Common;

namespace DatabaseAccess.DatabaseExceptions
{
    class IdPropertyNotFoundException : DbException
    {
        public IdPropertyNotFoundException(string message) : base(message)
        {
        }
    }
}
