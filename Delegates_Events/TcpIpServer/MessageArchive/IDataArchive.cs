using System.Collections.Generic;

namespace TcpIpServer.MessageArchive
{
    /// <summary>
    /// Defines method for record archive.
    /// </summary>
    /// <typeparam name="T"> Any Class.</typeparam>
    public interface IDataArchive<T> where T : class
    {
        /// <summary>
        /// Add record to archive.
        /// </summary>
        /// <param name="record"> Record to adding.</param>
        public void AddRecord(string record);

        /// <summary>
        /// Gets all records from archive.
        /// </summary>
        /// <returns> Collection of reccords.</returns>
        public IEnumerable<T> GetAllRecords();
    }
}
