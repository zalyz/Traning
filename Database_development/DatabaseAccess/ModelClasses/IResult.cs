using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseAccess.ModelClasses
{
    interface IResult
    {
        public Student Student { get; set; }

        public int Mark { get; set; }
    }
}
