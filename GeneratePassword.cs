//------------------------------------------------------------------------------
// This file is used to Generate temporary password to reset password
//
// File name     : GeneratePassword.cs
// Project name  : Card Memory Game 
// Written by    : Goutham
// Language      : C#
// Date modified : 24-10-2020
// Dependencies  : .NET framework 4.7.2
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CardMemoryGame
{
    /// <summary>
    /// This class is used to generate a password
    /// </summary>
    public class GeneratePassword
    {
        /// <summary>
        /// This function is used to generate a password
        /// </summary>
        /// <param name="length">(int) length of the password to be generated, default = 8</param>
        /// <returns>password</returns>
        public static string Generate(int length=8)
        {
            // Create a string of characters, numbers, special characters that allowed in the password  
            string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?_-";
            Random random = new Random();

            // Select one random character at a time from the string  
            // and create an array of chars  
            char[] chars = new char[length];

            int i = 0;
            while (i < length)
            {
                chars[i] = validChars[random.Next(0, 25)];
                chars[i + 1] = validChars[random.Next(26, 51)];
                chars[i + 2] = validChars[random.Next(52, 61)];
                chars[i + 3] = validChars[random.Next(62, validChars.Length)];
                i += 4;
            }
            return new string(chars);
        }
    }
}