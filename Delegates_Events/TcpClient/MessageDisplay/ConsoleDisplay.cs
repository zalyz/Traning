using System;
using System.Collections.Generic;
using System.Text;

namespace TcpIpClient.MessageDisplay
{
    class ConsoleDisplay : IDisplay<string>
    {
        public void Show(string message)
        {
            //Console.WriteLine(message);
        }
    }
}
