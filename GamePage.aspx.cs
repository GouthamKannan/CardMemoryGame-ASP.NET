//------------------------------------------------------------------------------
// This is the backend code for the page GamePage.aspx
// This file is used to play the game and display the results after the game 
// ends.
//
// File name     : GamePage.aspx.cs
// Project name  : Card Memory Game 
// Written by    : Goutham
// Language      : C#
// Date modified : 23-09-2020
// Dependencies  : .NET framework 4.7.2
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CardMemoryGame
{
    public partial class GamePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        { 
            // Redirect if no session variable is created
            if (Session["UserName"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            // Check if password is temporary
            else if (DataAccess.CheckTempPassword(Session["UserName"].ToString()) == "true")
            {
                Response.Redirect("MyProfile.aspx");
            }

            else
            {
                MenuItem mi = mnu.Items[0];
                mi.Value = Session["UserName"].ToString();
                mi.Text = mi.Value;

                // Display options for guest user
                if (mi.Text == "Guest")
                {
                    Guest.Visible = true;
                    mnu.Visible = false;
                }

                // Display options for logged in user
                else
                {
                    Guest.Visible = false;
                    mnu.Visible = true;
                }

            }

        }
        protected void Victory_RedirectToHome(object sender, EventArgs e)
        {
            // Update scores only for logged in user
            if(Session["UserName"].ToString() != "Guest")
            {
                UpdateScoreDetails(UserScoreHidden.Value);
            }
            
            // Log and redirect to home page
            Logging.WriteLog(Session["UserName"].ToString(), "Game won, Score: "+ UserScoreHidden.Value);
            Response.Redirect(String.Format("Default.aspx"));
        }

        
        protected void GameOver_RedirectToHome(object sender, EventArgs e)
        {
            // Log and redirect after game lost
            Logging.WriteLog(Session["UserName"].ToString(), "Game lost");
            Response.Redirect(String.Format("Default.aspx"));
        }


        public void UpdateScoreDetails(string UserScore)
        {
            // Get the top scores of current user
            List<int> UserTopScore = DataAccess.GetUserTopScores(Session["UserName"].ToString());
            
            // Add the current score to the existing scores
            int HighScore = 0;
            string UserScores = null;
            UserTopScore.Add(Convert.ToInt32(UserScore));
            UserTopScore.Sort();
            UserTopScore.Reverse();
            UserTopScore = UserTopScore.Take(10).ToList();
            
            UserScores = string.Join(",", UserTopScore);
            HighScore = Convert.ToInt32(UserTopScore[0]);

            // Update the score in database
            DataAccess.UpdateUserScores(Session["UserName"].ToString(), UserScores, HighScore);
        }

        protected void Navclick(object sender, MenuEventArgs e)
        {
            if (e.Item.Text == "logout" || e.Item.Text == "Login")
            {
                Logging.WriteLog(Session["UserName"].ToString(), "User logged out");
                
                // Clear session variables and log out
                System.Web.Security.FormsAuthentication.SignOut();
                Session.Clear();
                Session.Abandon();
                Response.Redirect("Login.aspx");
            }

            else if (e.Item.Text == "My Profile")
            {
                // Redirect to profile page
                Response.Redirect("MyProfile.aspx");
            }

            else if (e.Item.Text == "View scores")
            {
                // Redirect to scores page
                Response.Redirect("ViewScores.aspx");
            }

            else if (e.Item.Text == "Game Page")
            {
                // Redirect to game page with options to select cards
                Response.Redirect("Default.aspx");
            }
        }
    }
}