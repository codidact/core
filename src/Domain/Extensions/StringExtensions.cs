using System.Text.RegularExpressions;

namespace Codidact.Domain.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Converts a string to snake_case
        /// </summary>
        /// <param name="input">The string to convert</param>
        /// <returns></returns>
        public static string ToSnakeCase(this string input)
        {
            if (string.IsNullOrEmpty(input)) { 
                return input;
            }

            var startUnderscores = Regex.Match(input, @"^_+");
            return startUnderscores + Regex.Replace(input, @"([a-z0-9])([A-Z])", "$1_$2").ToLower();
        }

        public static string TrimSuffix(this string input, string suffix) {
            if(input.EndsWith(suffix)) {
                return input.Substring(0, input.Length - suffix.Length);
            } else {
                return input;
            }
        }
    }
}
