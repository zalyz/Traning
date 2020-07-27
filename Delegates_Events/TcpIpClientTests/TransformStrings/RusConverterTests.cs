using Microsoft.VisualStudio.TestTools.UnitTesting;
using TcpIpClient.TransformStrings;
using System;
using System.Collections.Generic;
using System.Text;
using TcpIpClient.MessageDisplay;

namespace TcpIpClient.TransformStrings.Tests
{
    [TestClass()]
    public class RusConverterTests
    {
        // Result in "Additional output for this test".
        [TestMethod()]
        public void ConvertString_InputEngString_RusStringDisplayed()
        {
            var rusConverter = new RusConverter();
            var expectedString = "хеллов";
            rusConverter.ConvertString(new DebugDisplay(), "Hellow");
        }
    }
}