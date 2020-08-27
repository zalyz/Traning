using System;

namespace DatabaseAccess.ModelClasses
{
    /// <summary>
    /// Represent the essence of the test.
    /// </summary>
    public class Test
    {
        /// <summary>
        /// Gets or sets Test Id.
        /// </summary>
        public int TestId { get; set; }

        /// <summary>
        /// Gets or sets Test name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets Test date.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets group name.
        /// </summary>
        public string GroupName { get; set; }
    }
}
