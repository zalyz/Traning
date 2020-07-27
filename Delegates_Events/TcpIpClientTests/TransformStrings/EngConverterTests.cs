using Microsoft.VisualStudio.TestTools.UnitTesting;
using TcpIpClient.TransformStrings;
using System;
using System.Collections.Generic;
using System.Text;
using TcpIpClient.MessageDisplay;

namespace TcpIpClient.TransformStrings.Tests
{
    [TestClass()]
    public class EngConverterTests
    {
        // Result in "Additional output for this test".
        [TestMethod()]
        public void ConvertString_InputRusString_EngStringDisplayed()
        {
            var engConverter = new EngConverter();
            var expectedString = "privet";
            engConverter.ConvertString(new DebugDisplay(), "Привет");
        }
    }
}