using System;
using System.Collections.Generic;
using System.Text;
using TcpIpClient.MessageDisplay;

namespace TcpIpClient.TransformStrings
{
    interface ILanguageConverter
    {
        public void ConvertString(IDisplay<string> display, string message);
    }
}
