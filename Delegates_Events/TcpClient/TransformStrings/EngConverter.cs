using System;
using System.Collections.Generic;
using System.Text;

namespace TcpIpClient.TransformStrings
{
    class EngConverter : ILanguageConverter
    {
        public string ConvertString(string message)
        {
            var resultString = new StringBuilder();
            foreach (var character in message.ToUpper())
            {
                var engCharacter = GetEnglishCharacter(character);
                resultString.Append(engCharacter);
            }

            return resultString.ToString();
        }

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
