using System;
using System.Net.Sockets;
using System.IO;
using System.Text;

namespace TcpIpClient
{
    /// <summary>
    /// Class representing the TcpIp connection client.
    /// </summary>
    public class Client
    {
        /// <summary>
        /// Contains all observers of this client.
        /// </summary>
        private event Action<string> _observers;

        /// <summary>
        /// Instance of TcpClient class for convenient handling.
        /// </summary>
        private readonly TcpClient _tcpClient;

        /// <summary>
        /// Instance of Stream class for convenient handling.
        /// </summary>
        private readonly Stream stream;

        /// <summary>
        /// Constructor for creating an instance of the client class.
        /// </summary>
        /// <param name="ip"> IP of connection.</param>
        /// <param name="port"> Port of connection.</param>
        public Client(string ip, int port = 13000)
        {
            _tcpClient = new TcpClient(ip, port);
            stream = _tcpClient.GetStream();
        }

        /// <summary>
        /// Sends message to server.
        /// </summary>
        /// <param name="message"> Message for the server.</param>
        public void SendMessage(string message)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            stream.Write(data, 0, data.Length);
        }

        /// <summary>
        /// Returns responce from server on you message.
        /// </summary>
        /// <returns> Message from the server.</returns>
        public string ResponseFromServer()
        {
            var data = new byte[256];
            var bytes = stream.Read(data, 0, data.Length);
            string responseData = Encoding.UTF8.GetString(data, 0, bytes);
            _observers?.Invoke(responseData);
            return responseData;
        }

        /// <summary>
        /// Closes client connection.
        /// </summary>
        public void Close()
        {
            stream.Close();
            _tcpClient.Close();
        }

        /// <summary>
        /// Adds a translator for a message from the server.
        /// </summary>
        /// <param name="func"> Function that translate a message.</param>
        public void Subscribe(Action<string> func)
        {
            _observers += func;
        }

        /// <summary>
        /// Remove a translator for a message from the server
        /// </summary>
        /// <param name="func"> Function that translate a message.</param>
        public void Unsubscribe(Action<string> func)
        {
            _observers -= func;
        }
    }
}
