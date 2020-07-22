using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace TcpIpServer
{
    class Server
    {
        private event Action<string> _observers;

        private TcpListener _server;

        private TcpClient _client;

        private NetworkStream _stream;

        public Server(string ip, int port)
        {
            _server = new TcpListener(IPAddress.Parse(ip), port);
        }

        public void Start()
        {
            _server.Start();
        }

        public void AcceptClient()
        {
            _client = _server.AcceptTcpClient();
            _stream = _client.GetStream();
        }

        public void CheckForMessage()
        {
            byte[] bytes = new byte[256];

            int i;

            // Loop to receive all the data sent by the client.
            if ((i = _stream.Read(bytes, 0, bytes.Length)) != 0)
            {
                // Translate data bytes to a ASCII string.
                string data = System.Text.Encoding.UTF8.GetString(bytes, 0, i);

                _observers?.Invoke(DateTime.Now.ToString() + " : " + data);

                // Process the data sent by the client.
                data = data.ToUpper();

                byte[] msg = System.Text.Encoding.UTF8.GetBytes(data);

                // Send back a response.
                _stream.Write(msg, 0, msg.Length);

            }
        }

        public void CloseClient()
        {
            _stream.Close();
            _client.Close();
        }

        public void Stop()
        {
            _server.Stop();
        }

        public void Subscribe(Action<string> observer)
        {
            _observers += observer;
        }

        public void Unsubscribe(Action<string> observer)
        {
            _observers -= observer;
        }
    }
}
