//------------------------------------------------------------------------------
// This file is used read and write data in database and run SQL stored 
// procedures
//
// File name     : DataAcess.cs
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
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace CardMemoryGame
{
    /// <summary>
    /// This class is used to access the database and read and write data
    /// </summary>
    public class DataAccess
    {
        /// <summary>This function is used to validate whether the user name and 
        ///   password are correct during logging in</summary>
        /// <param name="Username">(String) User name of current user</param>
        /// <param name="Password">(String) Password of current user</param>
        /// <returns>True if user name and password are valid; otherwise, 
        ///   False</returns>
        public static bool CheckLoginDetails(string Username, string Password)
        {
            SqlConnection sqlcon = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
            try
            { 
                sqlcon.Open();
                SqlCommand sqlcmd = new SqlCommand("LoginValidation", sqlcon);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.Parameters.AddWithValue("@Username", Username);
                sqlcmd.Parameters.AddWithValue("@Password", Password);
                SqlDataReader sqlreader = sqlcmd.ExecuteReader();
                while (sqlreader.Read())
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Logging.WriteLog("Data Access error", "Cannot validate login details. Error: " + ex.Message);
            }
            finally
            {
                sqlcon.Close();
            }
            return false;
        }

        /// <summary>This function is used to get Username and Email address of 
        ///   the current user.</summary>
        /// <param name="Username">(String) User name of the current user</param>
        /// <returns>User id and e-mail of the current user</returns>
        public static Dictionary<string,string> UserProfiledetails(string Username)
        {
            Dictionary<string,string> UserProfile = new Dictionary<string, string>();
            SqlConnection sqlcon = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
            try
            {
                sqlcon.Open();
                SqlCommand sqlcmd = new SqlCommand("ProfileDetails", sqlcon);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.Parameters.AddWithValue("@Username", Username);
                SqlDataReader sqlreader = sqlcmd.ExecuteReader();
                while (sqlreader.Read())
                {
                    UserProfile.Add("UserName",sqlreader["UserName"].ToString());
                    UserProfile.Add("EmailId",sqlreader["EmailId"].ToString());
                }
            }
            catch (Exception ex)
            {
                Logging.WriteLog("Data Access error", "Cannot get user profile details. Error: " + ex.Message);
            }
            finally
            {
                sqlcon.Close();
            }
            return UserProfile;
        }

        /// <summary>
        /// This function is used to validate the username while updating the user profile
        /// </summary>
        /// <param name="Username">(String) New user name of current user</param>
        /// <param name="CurrentUserName">(String) User name of current user</param>
        /// <returns>True if new user name is valid; otherwise, False</returns>
        public static bool UsernameValidation(string Username, string CurrentUserName)
        {
            SqlConnection sqlcon = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
            try
            {
                sqlcon.Open();
                SqlCommand sqlcmd = new SqlCommand("UsernameValidation", sqlcon);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.Parameters.AddWithValue("@Username", Username);
                sqlcmd.Parameters.AddWithValue("@CurrentUsername", CurrentUserName);
                SqlDataReader sqlreader = sqlcmd.ExecuteReader();
                while (sqlreader.Read())
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Logging.WriteLog("Data Access error", "Cannot validate the user name. Error: " + ex.Message);
            }
            finally
            {
                sqlcon.Close();
            }
            return false;
        }

        /// <summary>
        /// This function is used to validate the user name and E-mail id while resetting the password
        /// </summary>
        /// <param name="Username">(String) User name of current user</param>
        /// <param name="Email">(String) E-mail id of current user</param>
        /// <returns>True if user name and E-mail id are valid; otherwise, False</returns>
        public static bool UserValidation(string Username, string Email)
        {
            SqlConnection sqlcon = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
            try
            {
                sqlcon.Open();
                SqlCommand sqlcmd = new SqlCommand("UserValidation", sqlcon);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.Parameters.AddWithValue("@Username", Username);
                sqlcmd.Parameters.AddWithValue("@Email", Email);
                SqlDataReader sqlreader = sqlcmd.ExecuteReader();
                while (sqlreader.Read())
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Logging.WriteLog("Data Access error", "Cannot validate the user details. Error: " + ex.Message);
            }
            finally
            {
                sqlcon.Close();
            }
            return false;
        }

        /// <summary>
        /// This function is used to store the temporary password
        /// </summary>
        /// <param name="Username">(String) User name of current user</param>
        /// <param name="NewPassword">(String) New password of current user</param>
        public static void StoreTempPassword(string Username, string NewPassword)
        {
            SqlConnection sqlcon = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
            try
            {
                sqlcon.Open();
                SqlCommand sqlcmd = new SqlCommand("StoreTempPassword", sqlcon);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.Parameters.AddWithValue("@Username", Username);
                sqlcmd.Parameters.AddWithValue("@NewPassword", NewPassword);
                SqlDataReader sqlreader = sqlcmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                Logging.WriteLog("Data Access error", "Cannot store temp password. Error: " + ex.Message);
            }
            finally
            {
                sqlcon.Close();
            }
        }

        /// <summary>
        /// This function is used to check if the password is temporary
        /// </summary>
        /// <param name="Username">(String) User name of current user</param>
        /// <returns>True if current password is temporary password; otherwise, False</returns>
        public static string CheckTempPassword(string Username)
        {
            SqlConnection sqlcon = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
            try
            {
                sqlcon.Open();
                SqlCommand sqlcmd = new SqlCommand("CheckTempPassword", sqlcon);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.Parameters.AddWithValue("@Username", Username);
                SqlDataReader sqlreader = sqlcmd.ExecuteReader();
                while (sqlreader.Read())
                {
                    return "true";
                }
                return "false";
            }
            catch (Exception ex)
            {
                Logging.WriteLog("Data Access error", "Cannot validate for temp password. Error: " + ex.Message);
            }
            finally
            {
                sqlcon.Close();
            }
            return "false";
        }

        /// <summary>Updates the User name and e-mail address of the current user
        ///   </summary>
        /// <param name="Username">(String) User name of current user</param>
        /// <param name="Password">(String) Password of current user</param>
        /// <param name="NewUserName">(String) Updated user name</param>
        /// <param name="NewEmailId">(String) Updated e-mail id</param>
        /// <returns>Updated user name and e-mail id</returns>
        public static Dictionary<string, string> UpdateProfileDetails(string Username, string Password, string NewUserName, string NewEmailId)
        {
            Dictionary<string, string> UpdatedUserProfile = new Dictionary<string, string>();
            SqlConnection sqlcon = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
            try
            {
                sqlcon.Open();
                SqlCommand sqlcmd = new SqlCommand("UpdateProfileDetails", sqlcon);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.Parameters.AddWithValue("@Username", Username);
                sqlcmd.Parameters.AddWithValue("@Password", Password);
                sqlcmd.Parameters.AddWithValue("@NewUsername", NewUserName);
                sqlcmd.Parameters.AddWithValue("@NewEmailId", NewEmailId);
                SqlDataReader sqlreader = sqlcmd.ExecuteReader();
                while (sqlreader.Read())
                {
                    UpdatedUserProfile.Add("UserName", sqlreader["UserName"].ToString());
                    UpdatedUserProfile.Add("EmailId", sqlreader["EmailId"].ToString());
                }
            }
            catch (Exception ex)
            {
                Logging.WriteLog("Data Access error", "Cannot update user profile details. Error: " + ex.Message);
            }
            finally
            {
                sqlcon.Close();
            }
            return UpdatedUserProfile;
        }

        /// <summary>This function is used to update the password of current user
        ///   </summary>
        /// <param name="Username">(String) User name of curent user</param>
        /// <param name="Password">(String) assword of current user</param>
        /// <param name="NewPassword">(String) New password</param>
        /// <returns>True if password is updated; otherwise False</returns>
        public static bool UpdateProfilePassword(string Username, string Password, string NewPassword)
        {
            SqlConnection sqlcon = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
            try
            {
                sqlcon.Open();
                SqlCommand sqlcmd = new SqlCommand("UpdateProfilePassword", sqlcon);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.Parameters.AddWithValue("@Username", Username);
                sqlcmd.Parameters.AddWithValue("@Password", Password);
                sqlcmd.Parameters.AddWithValue("@NewPassword", NewPassword);
                SqlDataReader sqlreader = sqlcmd.ExecuteReader();
                while (sqlreader.Read())
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Logging.WriteLog("Data Access error", "Cannot update user password details. Error: " + ex.Message);
            }
            finally
            {
                sqlcon.Close();
            }

            return false;
        }

        /// <summary>This function is used to get users with top scores
        ///   </summary>
        /// <returns>Returns a dictionary of users and scores</returns>
        public static Dictionary<string, string> GetTopScores()
        {
            Dictionary<string, string> TopScore = new Dictionary<string, string>();
            TopScore.Add("UserName", "HighScore");
            SqlConnection sqlcon = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
            try
            {
                sqlcon.Open();
                SqlCommand sqlcmd = new SqlCommand("GetTopScores", sqlcon);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader sqlreader = sqlcmd.ExecuteReader();
                while (sqlreader.Read())
                {
                    TopScore.Add(sqlreader["UserName"].ToString(), sqlreader["HighScore"].ToString());
                }
            }
            catch (Exception ex)
            {
                Logging.WriteLog("Data Access error", "Cannot get top score details. Error: " + ex.Message);
            }
            finally
            {
                sqlcon.Close();
            }
            return TopScore;
        }

        /// <summary>This function returns the top 10 score of current user
        ///   </summary>
        /// <param name="Username">(String) User name of current user</param>
        /// <returns>Returns a list of top 10 scores</returns>
        public static List<int> GetUserTopScores(string Username)
        {
            List<int> UserTopScores = new List<int>();
            string ScorelList = null;
            SqlConnection sqlcon = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
            try
            {
                sqlcon.Open();
                SqlCommand sqlcmd = new SqlCommand("GetUserTopScores", sqlcon);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.Parameters.AddWithValue("@Username", Username);
                SqlDataReader sqlreader = sqlcmd.ExecuteReader();
                while (sqlreader.Read())
                {
                    ScorelList = sqlreader["Scores"].ToString();
                }
                if (!string.IsNullOrEmpty(ScorelList))
                {
                    UserTopScores = ScorelList.Split(',').ToList().ConvertAll(int.Parse);
                }
                
            }
            catch (Exception ex)
            {
                if (ex.Message != "Input string was not in a correct format.")
                {
                    Logging.WriteLog("Data Access error", "Cannot get user's top score details. Error: " + ex.Message);
                }
            }
            finally
            {
                sqlcon.Close();
            }
            return UserTopScores;
        }

        /// <summary>This function is used to update the score of current user
        ///   </summary>
        /// <param name="Username">(String) User name of current user</param>
        /// <param name="Scores">(String) Top 10 scores of current user</param>
        /// <param name="HighScore">(int) Highest score of current user</param>
        public static void UpdateUserScores(string Username, string Scores, int HighScore)
        {
            SqlConnection sqlcon = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
            try
            {
                sqlcon.Open();
                SqlCommand sqlcmd = new SqlCommand("UpdateUserScores", sqlcon);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.Parameters.AddWithValue("@Username", Username);
                sqlcmd.Parameters.AddWithValue("@Scores", Scores);
                sqlcmd.Parameters.AddWithValue("@HighScore", HighScore);
                SqlDataReader sqlreader = sqlcmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                Logging.WriteLog("Data Access error", "Cannot update user's score. Error: " + ex.Message);
            }
            finally
            {
                sqlcon.Close();
            }
        }

        /// <summary>This function is used to store details of new user
        /// </summary>
        /// <param name="Username">(String) User name of new user</param>
        /// <param name="Password">(String) Password of new user</param>
        /// <param name="Email">(String) E-mail address of new user</param>
        /// <returns>Returns true if new details are stored successfully;
        ///   otherwise, false</returns>
        public static bool Signup(string Username, string Password, string Email)
        {
            SqlConnection sqlcon = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
            try
            {
                sqlcon.Open();
                SqlCommand sqlcmd = new SqlCommand("SignUp", sqlcon);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.Parameters.AddWithValue("@Username", Username);
                sqlcmd.Parameters.AddWithValue("@Password", Password);
                sqlcmd.Parameters.AddWithValue("@Email", Email);
                SqlDataReader sqlreader = sqlcmd.ExecuteReader();
                while (sqlreader.Read())
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Logging.WriteLog("Data Access error", "Cannot sign up new user. Error: " + ex.Message);
            }
            finally
            {
                sqlcon.Close();
            }

            return false;
        }
    }
}