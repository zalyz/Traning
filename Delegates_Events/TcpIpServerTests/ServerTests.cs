using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using TcpIpClient;

namespace TcpIpServer.Tests
{
    [TestClass()]
    public class ServerTests
    {

        [TestMethod()]
        public void ProcessingClientMessages_InputString_StringToUpperReturned()
        {
            var server = new Server("127.0.0.1");
            server.Start();
            var client = new Client("127.0.0.1");
            server.AcceptClient();
            client.SendMessage("Hellow");
            server.ProcessingClientMessages();
            var expected = "HELLOW";
            var actual = client.ResponseFromServer();
            StringAssert.Contains(expected, actual);
        }

        [TestMethod()]
        public void Subscribe_InputMessageArchiveMethod_MessageArchived()
        {
            var server = new Server("127.0.0.1");
            var messageArchive = new MessageArchive.MessageArchive();
            server.Subscribe(messageArchive.AddRecord);
            server.Start();
            var client = new Client("127.0.0.1");
            server.AcceptClient();
            client.SendMessage("Hellow");
            server.ProcessingClientMessages();
            var contains = "Hellow";
            var value = messageArchive.GetAllRecords().First();
            StringAssert.Contains(value, contains);
        }
    }
}