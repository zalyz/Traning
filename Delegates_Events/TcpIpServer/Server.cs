using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace TcpIpServer
{
    /// <summary>
    /// Class representing the TcpIp connection server.
    /// </summary>
    public class Server
    {
        /// <summary>
        /// Contains all observers of this server.
        /// </summary>
        private event Action<string> _observers;

        /// <summary>
        /// Instance of TcpServer class for convenient handling.
        /// </summary>
        private readonly TcpListener _server;

        /// <summary>
        /// Instance of TcpClient class for convenient handling.
        /// </summary>
        private TcpClient _client;

        /// <summary>
        /// Instance of Stream class for convenient handling.
        /// </summary>
        private NetworkStream _stream;

        /// <summary>
        /// Constructor for creating an instance of the server class.
        /// </summary>
        /// <param name="ip"> IP of connection.</param>
        /// <param name="port"> Port of connection.</param>
        public Server(string ip, int port = 13000)
        {
            _server = new TcpListener(IPAddress.Parse(ip), port);
        }

        /// <summary>
        /// Start the server.
        /// </summary>
        public void Start()
        {
            _server.Start();
        }

        /// <summary>
        /// Accepts client connection.
        /// </summary>
        /// <returns> Client that connected.</returns>
        public TcpClient AcceptClient()
        {
            _client = _server.AcceptTcpClient();
            _stream = _client.GetStream();
            return _client;
        }

        /// <summary>
        /// Receive messages from the client and sends a responce.
        /// </summary>
        public void ProcessingClientMessages()
        {
            byte[] bytes = new byte[256];

            int i;

            // Loop to receive all the data sent by the client.
            if ((i = _stream.Read(bytes, 0, bytes.Length)) != 0)
            {
                // Translate data bytes to a ASCII string.
                var data = TranslateBytesToString(bytes, i);

                _observers?.Invoke(data);

                // Process the data sent by the client.
                data = data.ToUpper();
                byte[] msg = TranslateStringToBytes(data);

                // Send back a response.
                _stream.Write(msg, 0, msg.Length);

            }
        }

        /// <summary>
        /// Closes client connection.
        /// </summary>
        public void CloseClient()
        {
            _stream.Close();
            _client.Close();
        }

        /// <summary>
        /// Stop the server.
        /// </summary>
        public void Stop()
        {
            _server.Stop();
        }

        /// <summary>
        /// Adds an observer that remembers messages from the client.
        /// </summary>
        /// <param name="observer"></param>
        public void Subscribe(Action<string> observer)
        {
            _observers += observer;
        }

        /// <summary>
        /// Remove an observer that remembers messages from the client
        /// </summary>
        /// <param name="observer"></param>
        public void Unsubscribe(Action<string> observer)
        {
            _observers -= observer;
        }

        /// <summary>
        /// Translats string to byte array.
        /// </summary>
        /// <param name="data"> Message in string form.</param>
        /// <returns> Message as array of bytes.</returns>
        private byte[] TranslateStringToBytes(string data)
        {
            return System.Text.Encoding.UTF8.GetBytes(data);
        }

        /// <summary>
        /// Translats array of butes to string.
        /// </summary>
        /// <param name="bytes"> Message as bytes array.</param>
        /// <param name="i"> Number of received bytes.</param>
        /// <returns> Message in string form.</returns>
        private string TranslateBytesToString(byte[] bytes, int i)
        {
            return System.Text.Encoding.UTF8.GetString(bytes, 0, i);
        }
    }
}
