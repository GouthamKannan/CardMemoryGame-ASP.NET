//------------------------------------------------------------------------------
// This file is used to Generate One Time Password (OTP) for E-mail verification
//
// File name     : GenerateOtp.cs
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
    /// This class is used to generate OTP for verification
    /// </summary>
    public class GenerateOtp
    {
        /// <summary>
        /// This function is used to generate OTP
        /// </summary>
        /// <returns>OTP</returns>
        public static string Generate()
        {
            // Generate random OTP
            Random r = new Random();
            var num = r.Next(0, 1000000);
            string otp = num.ToString("000000");

            return otp;
        }
    }
}