namespace TcpIpClient.MessageDisplay
{
    /// <summary>
    /// Defines the method to display.
    /// </summary>
    /// <typeparam name="T"> The type that will be displayed.</typeparam>
    public interface IDisplay<T>
    {
        /// <summary>
        /// Displays message.
        /// </summary>
        /// <param name="message"> Message to display.</param>
        public void Show(T message);
    }
}
