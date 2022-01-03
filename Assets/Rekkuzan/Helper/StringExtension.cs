using System;
using System.Text;

namespace Rekkuzan.Helper
{
    public static class StringExtension
    {
        /// <summary>
        /// Will remove all special character from string (no replacement)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RemoveSpecialCharacters(this string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// Replace all char in a string by a new string value
        /// </summary>
        /// <param name="s"></param>
        /// <param name="separators"></param>
        /// <param name="newVal"></param>
        /// <returns></returns>
        public static string ReplaceAll(this string s, char[] separators, string newVal)
        {
            string[] temp;

            temp = s.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            return String.Join(newVal, temp);
        }
    }
}