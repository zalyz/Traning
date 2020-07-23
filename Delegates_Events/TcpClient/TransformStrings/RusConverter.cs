using System;
using System.Collections.Generic;
using System.Text;
using TcpIpClient.MessageDisplay;

namespace TcpIpClient.TransformStrings
{
    class RusConverter : ILanguageConverter
    {
        public void ConvertString(IDisplay<string> display, string message)
        {
            var resultString = new StringBuilder();
            foreach (var character in message.ToUpper())
            {
                // If character more then or equal to 192, character is russian letter.
                if (character >= 192)
                    display.Show(message);

                var rusCharacter = GetRusCharacter(character);
                resultString.Append(rusCharacter);
            }

            display.Show(resultString.ToString());
        }

        private string GetRusCharacter(char character)
        {
            return character switch
            {
                'a' => "а",
                'b' => "б",
                'c' => "ц",
                'd' => "д",
                'e' => "и",
                'f' => "ф",
                'g' => "г",
                'h' => "х",
                'i' => "и",
                'j' => "дж",
                'k' => "к",
                'l' => "л",
                'm' => "м",
                'n' => "н",
                'o' => "о",
                'p' => "п",
                'q' => "куо",
                'r' => "р",
                's' => "с",
                't' => "т",
                'u' => "у",
                'v' => "в",
                'w' => "в",
                'x' => "кс",
                'y' => "ю",
                'z' => "з",
            };
        }
    }
}
