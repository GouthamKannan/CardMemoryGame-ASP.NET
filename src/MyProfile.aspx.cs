//------------------------------------------------------------------------------
// This is the backend code for the page MyProfile.aspx
// This file is used to display the logged in user details and update them
//
// File name     : GamePage.aspx.cs
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

    public partial class MyProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Redirect if session variable is not created
            if (Session["UserName"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                if (!Page.IsPostBack)
                {
                    Session["forget"] = DataAccess.CheckTempPassword(Session["UserName"].ToString());
                    if (Session["forget"].ToString() == "true")
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Change your password')", true);
                    }

                    // Display the user details
                    MenuItem mi = mnu.Items[0];
                    mi.Value = Session["UserName"].ToString();
                    mi.Text = mi.Value;
                    Dictionary<string, string> UserProfile = DataAccess.UserProfiledetails(Session["UserName"].ToString());
                    UserName.Text = UserProfile["UserName"];
                    Email.Text = UserProfile["EmailId"];

                }
            }
            
        }

        protected void Navclick(object sender, MenuEventArgs e)
        {
            // Disable if redirected after resetting the password
            if (Session["forget"].ToString() == "true")
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Change your password')", true);
                return;
            }

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

        private bool CheckProfileEmpty()
        {
            bool empty = false;

            // Check if user name is empty
            if (string.IsNullOrEmpty(UserName.Text))
            {
                UsernameValidate.Text = "required";
                empty = true;
            }
            else
                UsernameValidate.Text = "";

            // Check if Email is empty
            if (string.IsNullOrEmpty(Email.Text))
            {
                EmailValidate.Text = "required";
                empty = true;
            }
            else
                UsernameValidate.Text = "";

            // Check if password is empty
            if (string.IsNullOrEmpty(Password.Text))
            {
                PasswordValidate.Text = "required";
                empty = true;
            }
            else
                PasswordValidate.Text = "";

            return empty;
        }

        private void ClearText()
        {
            // Clear all labels in the page

            OldPasswordValidate.Text = "";
            NewPasswordValidate.Text = "";
            RePasswordValidate.Text = "";
            OtpValidate.Text = "";
            UpdateLabel.Text = "";
            PasswordUpdateLabel.Text = "";
            PasswordValidate.Text = "";
            UsernameValidate.Text = "";
            EmailValidate.Text = "";

        }
        protected void GenerateOTP(object sender, EventArgs e)
        {
            // Disable if redirected after resetting password
            if (Session["forget"].ToString() == "true")
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Change your password')", true);
                return;
            }

            // Clear all labels
            ClearText();

            bool empty = CheckProfileEmpty();
            if (empty == true)
                return;

            // Generate OTP
            Session["Otp"] = GenerateOtp.Generate();
            Logging.WriteLog(Session["UserName"].ToString(), "OTP generated");

            // Send the OTP to user e-mail
            if (ConfigurationManager.AppSettings["DisableMail"] == "true")
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('This feature is disabled. Use " + Session["Otp"].ToString() + " as OTP')", true);
                //OtpValidate.Text = "This feature is disabled. Use " + Session["Otp"].ToString() + " as OTP";
                return;
            }
            bool otp_sent = Mail.SendVerificationMail(Session["Otp"].ToString(), Email.Text);
            if (otp_sent == true)
            {
                OtpValidate.Text = "OTP is sent to your email";
                Logging.WriteLog(Session["UserName"].ToString(), "OTP sent to mail");
            }
            else
                OtpValidate.Text = "Unable to send OTP. Enter valid email ID or try after some time";
        }

        protected void UpdateProfileClick(object sender, EventArgs e)
        {
            // Disable if redirected after resetting password
            if (Session["forget"].ToString() == "true")
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Change your password')", true);
                return;
            }

            // Clear all labels
            ClearText();

            // Check for empty inputs
            bool empty = CheckProfileEmpty();
            if (empty == true)
                return;

            // Check if OTP is generated
            if (Session["Otp"] == null)
            {
                OtpValidate.Text = "OTP not generated";
                return;
            }

            // Check if OTP is correct
            if (OtpBox.Text == Session["Otp"].ToString())
            {

                // Checks if the user name already exists
                if (DataAccess.UsernameValidation(UserName.Text,Session["UserName"].ToString()))
                {
                    UpdateLabel.Text = "Username already exists";
                }
                else
                {
                    // Update profile
                    Dictionary<string, string> UpdatedDetails = DataAccess.UpdateProfileDetails(Session["UserName"].ToString(), Password.Text, UserName.Text, Email.Text);
                    if (UpdatedDetails.Count() == 2)
                    {
                        // Update session variable and display updated details
                        Session["UserName"] = UpdatedDetails["UserName"];
                        MenuItem mi = mnu.Items[0];
                        mi.Value = Session["UserName"].ToString();
                        mi.Text = mi.Value;
                        UserName.Text = UpdatedDetails["UserName"];
                        Email.Text = UpdatedDetails["EmailId"];
                        UpdateLabel.Text = "Profile updated successfully";
                        Logging.WriteLog(Session["UserName"].ToString(), "Updated user profile");
                    }
                    else
                        UpdateLabel.Text = "Password is wrong";
                }
                Session["Otp"] = null;
            }
            else
            {
                OtpValidate.Text = "OTP is wrong";
            }
            
        }

        private bool CheckPasswordEmpty()
        {
            // Check if password is empty
            bool empty = false;
            if (string.IsNullOrEmpty(OldPassword.Text))
            {
                OldPasswordValidate.Text = "required";
                empty = true;
            }
            else
                OldPasswordValidate.Text = "";

            // Check if new password is empty
            if (string.IsNullOrEmpty(NewPassword.Text))
            {
                NewPasswordValidate.Text = "required";
                empty = true;
            }
            else
                NewPasswordValidate.Text = "";

            // Check if retype password is empty
            if (string.IsNullOrEmpty(ReNewPassword.Text))
            {
                RePasswordValidate.Text = "required";
                empty = true;
            }
            else
                RePasswordValidate.Text = "";

            return empty;
        }
        protected void UpdatePassword_Click(object sender, EventArgs e)
        {

            ClearText();

            bool empty = CheckPasswordEmpty();
            if (empty == true)
                return;

            // Validate inputs
            if (OldPassword.Text == NewPassword.Text)
                PasswordUpdateLabel.Text = "Current password and new password should not be same";
            else if (NewPassword.Text != ReNewPassword.Text)
                PasswordUpdateLabel.Text = "New password and retry new password should be same";
            else
            {
                // Update new password
                if (DataAccess.UpdateProfilePassword(Session["UserName"].ToString(), OldPassword.Text, NewPassword.Text))
                {
                    PasswordUpdateLabel.Text = "password updated successfully";
                    Logging.WriteLog(Session["UserName"].ToString(), "Updated user password");
                    Session["forget"] = "false";
                }
                else
                    PasswordUpdateLabel.Text = "Current password is wrong";
            }
        }
    }
}