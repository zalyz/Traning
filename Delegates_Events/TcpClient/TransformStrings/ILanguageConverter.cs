using System.Collections;
using TcpIpClient.MessageDisplay;

namespace TcpIpClient.TransformStrings
{
    /// <summary>
    /// Defines method for translating a string to another language.
    /// </summary>
    interface ILanguageConverter
    {
        /// <summary>
        /// Translats a string to another language
        /// </summary>
        /// <param name="display"> Display that displays the translated string.</param>
        /// <param name="message"> String to convert.</param>
        public void ConvertString(IDisplay<string> display, string message);
    }
}
