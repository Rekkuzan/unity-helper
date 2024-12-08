using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Rekkuzan.Utilities.Extensions
{
    public static class StringExtension
    {
        private static StringBuilder _stringBuilder;
        private static StringBuilder StringBuilder => _stringBuilder ??= new StringBuilder();
        
        /// <summary>
        /// Will remove all special character from string (no replacement)
        /// </summary>
        /// <param name="str">String to remove the special characters.</param>
        /// <returns>String without any special characters.</returns>
        public static string RemoveSpecialCharacters(this string str)
        {
            StringBuilder.Clear();

            for(int i = 0, count = str.Length; i < count; i++)
            {
                var c = str[i];
                if(c is >= '0' and <= '9' or >= 'A' and <= 'Z' or >= 'a' and <= 'z' or '.' or '_')
                {
                    StringBuilder.Append(c);
                }
            }

            return StringBuilder.ToString();
        }

        /// <summary>
        /// Replace all char in a string by a new string value
        /// </summary>
        /// <param name="str">String to replace all characters.</param>
        /// <param name="separators">Array of separators to remove.</param>
        /// <param name="newVal">String that replaces the separators.</param>
        /// <returns>Result string with all characters replaced.</returns>
        public static string ReplaceAll(this string str, char[] separators, string newVal)
        {
            return string.Join(newVal, str.Split(separators, StringSplitOptions.RemoveEmptyEntries));
        }

        public static bool CaseInsensitiveContains(this string source, string value)
        {
            return source.IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0;
        }


        private static HashSet<char> _invalidCharForPathName;
        private static HashSet<char> InvalidCharForPathName
        {
            get
            {
                return _invalidCharForPathName ??= new HashSet<char>(Path.GetInvalidPathChars()) { '\\', '/' };
            }
        }

        /// <summary>
        /// Ensures the string is valid for use as a file or folder name by removing invalid characters.
        /// </summary>
        /// <param name="input">The input string to validate and sanitize.</param>
        /// <returns>A sanitized string safe to use in paths.</returns>
        public static string ToValidPathName(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException("Input cannot be null or whitespace.", nameof(input));

            _stringBuilder.Clear();
            _stringBuilder.EnsureCapacity(input.Length);
            foreach (char c in input)
            {
                if (!InvalidCharForPathName.Contains(c))
                {
                    _stringBuilder.Append(c);
                }
            }
            
            return _stringBuilder.ToString();
        }

    }
}