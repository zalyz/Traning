﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseAccess.ModelClasses
{
    interface ITest
    {
        public string Name { get; set; }

        public DateTime Date { get; set; }

        public string GroupName { get; set; }
    }
}
