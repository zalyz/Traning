using System;
using System.Collections.Generic;
using System.Text;

namespace TcpIpClient.MessageDisplay
{
    interface IDisplay<T>
    {
        public void Show(T message);
    }
}
