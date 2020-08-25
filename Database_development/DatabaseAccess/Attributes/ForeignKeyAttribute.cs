using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseAccess.Attributes
{
    class ForeignKeyAttribute : Attribute
    {
        public string KeyName { get; private set; }

        public ForeignKeyAttribute(string keyName)
        {
            KeyName = keyName;
        }
    }
}
