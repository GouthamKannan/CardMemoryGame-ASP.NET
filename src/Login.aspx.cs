//------------------------------------------------------------------------------
// This is the backend code for the page Login.aspx
// This file is used to login a user or allows them to coninue as guest
//
// File name     : Login.aspx.cs
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
using System.Net;
using System.Net.Mail;  

namespace CardMemoryGame
{
    public partial class Login : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LoginButton(object sender, EventArgs e)
        {
            if (DataAccess.CheckLoginDetails(Username.Text, Password.Text))
            {
                // If validation is successful, initialize session variable and
                //   redirect to home page
                InvalidLabel.Text = "";
                Session["UserName"] = Username.Text;
                Logging.WriteLog(Session["UserName"].ToString(), "User logged in");

                if (DataAccess.CheckTempPassword(Username.Text) == "true")
                {
                    Response.Redirect("MyProfile.aspx");
                }
                Response.Redirect("Default.aspx");
            } 
            else
            {
                // If validation fails, display error message
                Session["UserName"] = null;
                InvalidLabel.Text = "Username or password is wrong";
            }
        }

        protected void PlayAsGuest(object sender, EventArgs e)
        {
            // Initialize session variable for guest and redirect to home page
            Session["UserName"] = "Guest";
            Response.Redirect("Default.aspx");
        }

        protected void ResetPassword(object sender, EventArgs e)
        {
            // Rdirect to reset password page
            Response.Redirect("ResetPassword.aspx");
        }
    }
}