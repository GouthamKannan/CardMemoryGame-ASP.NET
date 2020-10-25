//------------------------------------------------------------------------------
// This fille is used to log messages and errors in Card Memory Game project
//
// File name     : Logging.cs
// Project name  : Card Memory Game 
// Written by    : Goutham
// Language      : C#
// Date modified : 23-09-2020
// Dependencies  : .NET framework 4.7.2
//------------------------------------------------------------------------------
 
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.IO;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Threading;

namespace CardMemoryGame
{
    /// <summary>This class is used to Log status and errors into a log file. 
    ///   A new log file is created every day.</summary>
    public class Logging
    {
        /// <summary>
        /// This function is used to create and write logs into the log file.
        /// </summary>
        /// <param name="UserName">(String) User name of current user</param>
        /// <param name="Message">(String) The message to be logged</param>
        public static void WriteLog(string UserName, string Message)
        {
            var Retries = Convert.ToInt32(ConfigurationManager.AppSettings["LogRetryCount"]);
            while (Retries > 0)
            {
                try
                {
                    // Get the path for log file
                    var Path = ConfigurationManager.AppSettings["LogPath"];
                    
                    // Create file if it does not exists
                    var FileName = "Log_" + System.DateTime.Now.ToString("dd_MMM_yyyy") + ".txt";
                    if (!Directory.Exists(Path))
                        Directory.CreateDirectory(Path);

                    // Write log into the file
                    StreamWriter Writer = new StreamWriter(Path + FileName, true);
                    Writer.WriteLine(DateTime.Now + " [" + UserName + "] : " + Message);
                    
                    Writer.Close();
                    Writer.Dispose();
                    Retries = 0;
                }
                catch
                {
                    Thread.Sleep(500);
                    Retries--;
                }
            }
        }
    }
}