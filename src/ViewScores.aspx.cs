//------------------------------------------------------------------------------
// This is the backend code for the page ViewScores.aspx
// This file is used to display top scores and current user scores
//
// File name     : ViewScores.aspx.cs
// Project name  : Card Memory Game 
// Written by    : Goutham
// Language      : C#
// Date modified : 23-09-2020
// Dependencies  : .NET framework 4.7.2
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CardMemoryGame
{
    public partial class ViewScores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Redirect if session variable is not initialized
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

                // Get top 10 scores from database
                Dictionary<string, string> TopScores = DataAccess.GetTopScores();

                tbl.Controls.Clear();
                tbl.HorizontalAlign = HorizontalAlign.Center;
                tbl.CellPadding = 10;
                
                int rows = TopScores.Count;
                int cols = 2;

                // Create table and display the scores
                for (int i = 0; i < rows; i++)
                {
                    TableRow rowNew = new TableRow();
                    for (int j = 0; j < cols; j++)
                    {
                        TableCell cellNew = new TableCell();
                        cellNew.BorderWidth = Unit.Pixel(0);
                        cellNew.Width = 100;
                        cellNew.HorizontalAlign = HorizontalAlign.Center;
                        if (TopScores.ElementAt(i).Key.ToString() == Session["UserName"].ToString())
                            cellNew.ForeColor = System.Drawing.Color.DeepSkyBlue;
                        else
                            cellNew.ForeColor = System.Drawing.Color.White;
                        cellNew.BorderColor = System.Drawing.Color.White;
                        Label lblNew = new Label();
                        
                        // Add user name to the table
                        if (j == 0)
                            lblNew.Text = TopScores.ElementAt(i).Key.ToString();

                        // Add score to the table
                        else if(j == 1)
                            lblNew.Text = TopScores.ElementAt(i).Value.ToString();

                        cellNew.Controls.Add(lblNew);
                        rowNew.Controls.Add(cellNew);
                    }
                    tbl.Controls.Add(rowNew);
                }


                // Get all the scores of the current user from database
                List<int> UserTopScores = DataAccess.GetUserTopScores(Session["UserName"].ToString());

                tb2.Controls.Clear();
                tb2.HorizontalAlign = HorizontalAlign.Center;
                tb2.CellPadding = 10;

                int rows_CurrUser = UserTopScores.Count;

                // Create table and display the scores
                for (int i = 0; i < rows_CurrUser; i++)
                {
                    TableRow rowNew = new TableRow();
                    
                    TableCell cellNew = new TableCell();
                    cellNew.BorderWidth = Unit.Pixel(0);
                    cellNew.Width = 100;
                    cellNew.HorizontalAlign = HorizontalAlign.Center;
                    cellNew.ForeColor = System.Drawing.Color.White;
                    cellNew.BorderColor = System.Drawing.Color.White;
                    Label lblNew = new Label();
                    
                    // Add the score to display
                    lblNew.Text = UserTopScores[i].ToString();

                    cellNew.Controls.Add(lblNew);
                    rowNew.Controls.Add(cellNew);
                    
                    tb2.Controls.Add(rowNew);
                }
                if(!IsPostBack)
                    Logging.WriteLog(Session["UserName"].ToString(), "Score page loaded");
            }
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