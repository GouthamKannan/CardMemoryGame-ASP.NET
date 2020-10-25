//------------------------------------------------------------------------------
// This is the backend code for the page Signup.aspx
// This file is used to register new user
//
// File name     : Signup.aspx.cs
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
    public partial class Signup : System.Web.UI.Page
    {   
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GenerateOTP(object sender, EventArgs e)
        {
            // Check whether Username and Email are empty
            bool empty = false;
            if (string.IsNullOrEmpty(UserName.Text))
            {
                UsernameValidate.Text = "required";
                empty = true;
            }
            else
                UsernameValidate.Text = "";

            if (string.IsNullOrEmpty(Email.Text))
            {
                EmailValidate.Text = "required";
                empty = true;
            }
            else
                UsernameValidate.Text = "";

            if (empty == true)
                return;

            SignUpValidate.Text = "";
            OtpInvalidLabel.Text = "";

            // Generate OTP
            var Otp = GenerateOtp.Generate();
            Logging.WriteLog("New user", "OTP generated");
            Session["Otp"] = Otp;

            // Send the OTP to user e-mail
            if (ConfigurationManager.AppSettings["DisableMail"] == "true")
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('This feature is disabled. Use " + Otp + " as OTP')", true);
                return;
            }
            bool otp_sent = Mail.SendVerificationMail(Otp, Email.Text);
            if (otp_sent == true)
            {
                OtpInvalidLabel.Text = "OTP is sent to your email";
                Logging.WriteLog("New user", "OTP sent to mail");
            }
            else
                OtpInvalidLabel.Text = "Unable to send OTP. Enter valid email ID or try after some time";
        }

        protected void SignupButton(object sender, EventArgs e)
        {
            // Check if OTP is generated
            if (Session["Otp"] == null)
            {
                OtpInvalidLabel.Text = "OTP not generated";
                return;
            }

            // Check if OTP is correct
            if (OTPbox.Text == Session["Otp"].ToString())
            {
                // Check whether password is empty
                if (string.IsNullOrEmpty(Password.Text))
                {
                    PasswordValidate.Text = "Required";
                    RePasswordValidate.Text = "Required";
                    OtpInvalidLabel.Text = "Click again to Generate OTP";
                    Session["Otp"] = null;
                    return;
                }

                // Check if user name already exists
                if (DataAccess.Signup(UserName.Text, Password.Text, Email.Text))
                {
                    // Log and redirect to Login page
                    Logging.WriteLog(UserName.Text, "New user signed up");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "redirect", "alert ('Sign up successful'); window.location = 'Login.aspx';", true);

                }
                else 
                {
                    SignUpValidate.Text = "Username already exists";
                    OtpInvalidLabel.Text = "";
                    PasswordValidate.Text = "";
                    RePasswordValidate.Text = "";
                }
                Session["Otp"] = null;
            }
            else
            {
                SignUpValidate.Text = "Wrong OTP ";
                PasswordValidate.Text = "";
                RePasswordValidate.Text = "";
                PasswordValidate.Text = "";
                RePasswordValidate.Text = "";
            }
        }
    }
}