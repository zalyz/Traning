using Microsoft.VisualStudio.TestTools.UnitTesting;
using TcpIpClient.MessageDisplay;
using System;
using System.Collections.Generic;
using System.Text;

namespace TcpIpClient.MessageDisplay.Tests
{
    [TestClass()]
    public class DebugDisplayTests
    {
        [TestMethod()]
        public void ShowTest()
        {
            var display = new DebugDisplay();
            display.Show("Hellow");
        }
    }
}