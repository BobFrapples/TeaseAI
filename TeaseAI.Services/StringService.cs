using System.Text.RegularExpressions;
using TeaseAI.Common.Interfaces;

namespace TeaseAI.Services
{
    public class StringService : IStringService
    {
        /// <summary>
        /// Capitalize the first letter in <paramref name="input"/>
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string Capitalize(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;
            var inputAr = input.ToCharArray();
            inputAr[0] = char.ToUpper(input[0]);

            return new string(inputAr);
        }

        public bool WordExists(string haystack, string needle)
        {
            return haystack.Contains(needle);
            return Regex.Matches(haystack, "\b" + needle + "\b").Count > 0;
        }
    }
}
