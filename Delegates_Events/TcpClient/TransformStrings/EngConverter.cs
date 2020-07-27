using System.Text;
using TcpIpClient.MessageDisplay;

namespace TcpIpClient.TransformStrings
{
    /// <summary>
    /// Translate russian string to english language.
    /// </summary>
    public class EngConverter : ILanguageConverter
    {
        /// <inheritdoc/>
        public void ConvertString(IDisplay<string> display, string message)
        {
            var resultString = new StringBuilder();
            foreach (var character in message.ToLower())
            {
                // If character less then or equal to 122, character is english letter.
                if (character <= 122)
                {
                    display.Show(message);
                    return;
                }

                var engCharacter = GetEnglishCharacter(character);
                resultString.Append(engCharacter);
            }

            display.Show(resultString.ToString());
        }

        /// <summary>
        /// Translate each english letter to russian letter.
        /// </summary>
        /// <param name="character"> English letter to translate.</param>
        /// <returns> Russian latter.</returns>
        private string GetEnglishCharacter(char character)
        {
            return character switch
            {
                'а' =>  "a",
                'б' =>  "b",
                'в' =>  "v",
                'г' =>  "g",
                'д' =>  "d",
                'е' =>  "e",
                'ё' =>  "e",
                'ж' =>  "j",
                'з' =>  "z",
                'и' =>  "i",
                'й' =>  "i",
                'к' =>  "k",
                'л' =>  "l",
                'м' =>  "m",
                'н' =>  "n",
                'о' =>  "o",
                'п' =>  "p",
                'р' =>  "r",
                'с' =>  "s",
                'т' =>  "t",
                'у' =>  "y",
                'ф' =>  "f",
                'х' =>  "x",
                'ц' =>  "c",
                'ч' =>  "ch",
                'ш' =>  "sh",
                'щ' =>  "sh\'",
                'ъ' =>  "\'",
                'ы' =>  "i",
                'ь' =>  "\'",
                'э' =>  "a",
                'ю' =>  "yu",
                'я' =>  "ya",
                _ => "?",
            };
        }
    }
}
