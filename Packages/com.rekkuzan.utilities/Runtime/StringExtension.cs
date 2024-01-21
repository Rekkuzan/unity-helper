using System;
using System.Text;

namespace Rekkuzan.Helper
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

            for(int i = 0; i < str.Length; i++)
            {
                char c = str[i];
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
    }
}