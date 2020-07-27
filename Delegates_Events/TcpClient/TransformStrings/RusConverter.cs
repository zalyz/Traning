using System.Text;
using TcpIpClient.MessageDisplay;

namespace TcpIpClient.TransformStrings
{
    /// <summary>
    /// Translate english string to russian language.
    /// </summary>
    public class RusConverter : ILanguageConverter
    {
        /// <inheritdoc/>
        public void ConvertString(IDisplay<string> display, string message)
        {
            var resultString = new StringBuilder();
            foreach (var character in message.ToLower())
            {
                // If character more then or equal to 192, character is russian letter.
                if (character >= 192)
                {
                    display.Show(message);
                    return;
                }

                var rusCharacter = GetRusCharacter(character);
                resultString.Append(rusCharacter);
            }

            display.Show(resultString.ToString());
        }

        /// <summary>
        /// Translate each russian letter to english letter.
        /// </summary>
        /// <param name="character"> Russian letter to translate.</param>
        /// <returns> English latter.</returns>
        private string GetRusCharacter(char character)
        {
            return character switch
            {
                'a' => "а",
                'b' => "б",
                'c' => "ц",
                'd' => "д",
                'e' => "е",
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
