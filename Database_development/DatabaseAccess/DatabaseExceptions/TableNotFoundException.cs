using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace DatabaseAccess.DatabaseExceptions
{
    class TableNotFoundException : DbException
    {
        public TableNotFoundException(string message) : base(message)
        {
        }
    }
}
