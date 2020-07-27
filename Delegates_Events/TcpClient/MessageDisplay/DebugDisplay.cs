using System.Diagnostics;

namespace TcpIpClient.MessageDisplay
{
    /// <summary>
    /// Displays message to the Debug console.
    /// </summary>
    public class DebugDisplay : IDisplay<string>
    {
        /// <inheritdoc/>
        public void Show(string message)
        {
            Debug.WriteLine(message);
        }
    }
}
