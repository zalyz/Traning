using Microsoft.VisualStudio.TestTools.UnitTesting;
using TcpIpClient;
using System;
using System.Collections.Generic;
using System.Text;
using TcpIpServer;
using System.Diagnostics;
using TcpIpClient.TransformStrings;
using TcpIpClient.MessageDisplay;

namespace TcpIpClient.Tests
{
    [TestClass()]
    public class ClientTests
    {

        [TestMethod()]
        public void SendMessageTest()
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
        public void ResponseFromServerTest()
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
        public void Subscribe_InputEngString_RusStringInDebugConsole()
        {
            var server = new Server("127.0.0.1");
            server.Start();
            var client = new Client("127.0.0.1");
            client.Subscribe(e => 
            { 
                new RusConverter().ConvertString(new DebugDisplay(), e); 
            });
            client.Subscribe(e => 
            { 
                new EngConverter().ConvertString(new DebugDisplay(), e); 
            });
            server.AcceptClient();
            client.SendMessage("Hellow");
            server.ProcessingClientMessages();
            var expected = "HELLOW";
            var actual = client.ResponseFromServer();
            StringAssert.Contains(expected, actual);
        }

        [TestMethod()]
        public void Subscribe_InputRusString_EngStringInDebugConsole()
        {
            var server = new Server("127.0.0.1");
            server.Start();
            var client = new Client("127.0.0.1");
            client.Subscribe(e =>
            {
                new RusConverter().ConvertString(new DebugDisplay(), e);
            });
            client.Subscribe(e =>
            {
                new EngConverter().ConvertString(new DebugDisplay(), e);
            });
            server.AcceptClient();
            client.SendMessage("Привет");
            server.ProcessingClientMessages();
            var expected = "ПРИВЕТ";
            var actual = client.ResponseFromServer();
            StringAssert.Contains(expected, actual);
        }

        // Result in "Additional output for this test".
        [TestMethod()]
        public void Unsubscribe_InputEngString_EngStringInDebugConsole()
        {
            var server = new Server("127.0.0.1");
            server.Start();
            var client = new Client("127.0.0.1");
            var rusConverter = new RusConverter();
            client.Subscribe(e =>
            {
                rusConverter.ConvertString(new DebugDisplay(), e);
            });
            client.Subscribe(e =>
            {
                new EngConverter().ConvertString(new DebugDisplay(), e);
            });
            client.Unsubscribe(e =>
            {
                rusConverter.ConvertString(new DebugDisplay(), e);
            });
            server.AcceptClient();
            client.SendMessage("Hellow");
            server.ProcessingClientMessages();
            var expected = "HELLOW";
            var actual = client.ResponseFromServer();
            StringAssert.Contains(expected, actual);
        }

        // Result in "Additional output for this test".
        [TestMethod()]
        public void Unsubscribe_InputRusString_RusStringInDebugConsole()
        {
            var server = new Server("127.0.0.1");
            server.Start();
            var client = new Client("127.0.0.1");
            var rengConverter = new EngConverter();
            var debugDisplay = new DebugDisplay();
            Action<string> convertAndDisplay  = e =>
            {
                rengConverter.ConvertString(debugDisplay, e);
            };
            client.Subscribe(e =>
            {
                new RusConverter().ConvertString(new DebugDisplay(), e);
            });
            client.Subscribe(convertAndDisplay);
            client.Unsubscribe(convertAndDisplay);
            server.AcceptClient();
            client.SendMessage("Привет");
            server.ProcessingClientMessages();
            var expected = "ПРИВЕТ";
            var actual = client.ResponseFromServer();
            StringAssert.Contains(expected, actual);
        }
    }
}