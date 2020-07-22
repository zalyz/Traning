using System;
using System.Collections.Generic;
using System.Text;

namespace TcpIpClient.TransformStrings
{
    interface ILanguageConverter
    {
        public string ConvertString(string message);
    }
}
