using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseAccess.Attributes
{
    /// <summary>
    /// Represents an entity that stores the name of the foreign key.
    /// </summary>
    class ForeignKeyAttribute : Attribute
    {
        /// <summary>
        /// Foreign key name.
        /// </summary>
        public string KeyName { get; private set; }

        /// <summary>
        /// Create instance of ForeignKeyAttribute.
        /// </summary>
        /// <param name="keyName">Foreign key name.</param>
        public ForeignKeyAttribute(string keyName)
        {
            KeyName = keyName;
        }
    }
}
