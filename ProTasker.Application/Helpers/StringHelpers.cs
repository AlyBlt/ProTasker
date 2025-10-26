using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProTasker.Application.Helpers
{
    public static class StringHelpers
    {
        // Metnin sadece ilk harfini büyük yapar.
        public static string Capitalize(string? text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            return char.ToUpper(text[0]) + text.Substring(1);
        }

        // Metindeki her kelimenin ilk harfini büyük yapar.
        public static string CapitalizeWords(string? input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            var words = input.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < words.Length; i++)
            {
                var word = words[i];
                if (word.Length > 1)
                    words[i] = char.ToUpper(word[0]) + word.Substring(1).ToLower();
                else
                    words[i] = word.ToUpper();
            }
            return string.Join(' ', words);
        }
    }
}
