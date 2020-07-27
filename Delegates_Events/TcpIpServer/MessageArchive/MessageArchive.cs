using System;
using System.Collections.Generic;

namespace TcpIpServer.MessageArchive
{
    /// <summary>
    /// Represents an archive where message from users are saved.
    /// </summary>
    public class MessageArchive : IDataArchive<string>
    {
        /// <summary>
        /// Collection of messages.
        /// </summary>
        private readonly List<string> _dataArchive = new List<string>();

        /// <inheritdoc/>
        public void AddRecord(string message)
        {
            _dataArchive.Add(DateTime.Now.ToString() + " : " + message);
        }

        /// <inheritdoc/>
        public IEnumerable<string> GetAllRecords()
        {
            return _dataArchive;
        }
    }
}
