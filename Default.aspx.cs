//------------------------------------------------------------------------------
// This is the backend code for Default.aspx page
// Default.aspx page is used to select the number of cards and start the game
//
// File name     : Default.aspx.cs
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
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Redirect if no session variable created
            if(Session["UserName"] == null)
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

        protected void Btn_Start_Click(object sender, EventArgs e)
        {
            // Redirect the game page wtith the selected option in url
            var selector = RbSeclector.SelectedItem.Value;
            Logging.WriteLog(Session["UserName"].ToString(), "Game started with " + Convert.ToInt32(selector) * 4 + " cards");
            Response.Redirect(String.Format("GamePage.aspx?cards={0}", selector));
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