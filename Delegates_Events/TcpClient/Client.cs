using System;
using System.Net.Sockets;
using System.IO;
using System.Text;

namespace TcpIpClient
{
    class Client
    {
        private event Func<string, string> _observers;

        private TcpClient TcpClient;

        private Stream stream;

        public Client(string ip, int port = 13000)
        {
            TcpClient = new TcpClient(ip, port);
            stream = TcpClient.GetStream();
        }

        public void SendMessage(string message)
        {
            Byte[] data = Encoding.UTF8.GetBytes(message);
            stream.Write(data, 0, data.Length);
        }

        public string ResponseFromServer()
        {
            // Buffer to store the response bytes.
            Byte[] data = new Byte[256];

            // String to store the response ASCII representation.
            String responseData = String.Empty;

            // Read the first batch of the TcpServer response bytes.
            Int32 bytes = stream.Read(data, 0, data.Length);
            responseData = Encoding.UTF8.GetString(data, 0, bytes);

            return responseData;
        }

        public void Close()
        {
            stream.Close();
            TcpClient.Close();
        }

        public void Subscribe(Func<string, string> func)
        {

        }

        public void Unsubscribe(Func<string, string> func)
        {

        }
    }
}
