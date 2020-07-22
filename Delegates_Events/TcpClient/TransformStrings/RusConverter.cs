using System;
using System.Collections.Generic;
using System.Text;

namespace TcpIpClient.TransformStrings
{
    class RusConverter : ILanguageConverter
    {
        public string ConvertString(string message)
        {
            var resultString = new StringBuilder();
            foreach (var character in message.ToUpper())
            {
                var rusCharacter = GetRusCharacter(character);
                resultString.Append(rusCharacter);
            }

            return resultString.ToString();
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
