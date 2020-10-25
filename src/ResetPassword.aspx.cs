//------------------------------------------------------------------------------
// This is the backend code for the page ResetPassword.aspx
// This file is used to reset user password
//
// File name     : ResetPassword.aspx.cs
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
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace CardMemoryGame
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GenerateNewPassword(object sender, EventArgs e)
        {
            // Check if username and details are valid
            if (DataAccess.UserValidation(Username.Text, Email.Text))
            {

                // Create random password
                string NewPassword = GeneratePassword.Generate(8);

                if (ConfigurationManager.AppSettings["DisableMail"] == "false")
                {
                    
                    // Send the password to user e-mail id and store the password
                    bool sent = Mail.SendNewPasswordMail(NewPassword, Email.Text);
                    if (sent == true)
                    {
                        DataAccess.StoreTempPassword(Username.Text, NewPassword);
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "redirect",
                        "alert ('Password is sent to your e-mail. Use the new password to login'); window.location = 'Login.aspx';", true);
                        Logging.WriteLog(Username.Text, "Temporary password created");
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "redirect",
                       "alert ('Cannot send e-mail. Check your mail ID or try after sometime'); window.location = 'Login.aspx';", true);
                    }
                }

                else
                {
                    DataAccess.StoreTempPassword(Username.Text, NewPassword);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "redirect",
                        "alert ('This feature is disabled. Use " + NewPassword + " as new password'); window.location = 'Login.aspx';", true);
                    Logging.WriteLog(Username.Text, "Temporary password created");
                }

            }
            else
            {
                InvalidLabel.Text = "Invalid user name or e-mail id";
            }
        }
    }
}