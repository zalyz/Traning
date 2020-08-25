using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace DatabaseAccess.DatabaseExceptions
{
    class IdPropertyNotFoundException : DbException
    {
        public IdPropertyNotFoundException(string message) : base(message)
        {
        }
    }
}
