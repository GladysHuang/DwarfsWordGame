//-----------------------------------------------------------------------
// <copyright file="HelperClass.cs" company="GH">
//     Copyright (c) All rights reserved.
// </copyright>
// <author>Gladys Huang</author>
//-----------------------------------------------------------------------
namespace DwarfsWordGame
{
    /// <summary>
    /// This is utility class that contains helper functions.
    /// </summary>
    public class HelperClass
    {
        /// <summary>
        /// Check if an input string is palindrome or not
        /// </summary>
        /// <remarks>
        /// This function is case sensitive check, so please convert case
        /// if you prefer to do case insensitive check.
        /// </remarks>
        /// <param name="str">The string that to be check</param>
        /// <returns>True is it is palindrome. False if the string is not palindrome</returns>
        public static bool IsPalindrome(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return false;
            }

            int i = 0;
            int j = str.Length - 1;

            while (i < j)
            {
                if (str[i] != str[j])
                {
                    return false;
                }

                i++;
                j--;
            }

            return true;
        }
    }
}
