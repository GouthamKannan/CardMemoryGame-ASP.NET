//------------------------------------------------------------------------------
// This is the backend code for the Home.Master
//
// File name     : Home.Master.cs
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
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string PageName = Page.AppRelativeVirtualPath;
            if (PageName == "~/GamePage.aspx" || PageName == "~/MyProfile.aspx")
            {
                footer.Style["position"] = "relative";
                footer.Style["bottom"] = "0";
            }
            else
            {
                footer.Style["position"] = "absolute";
            }
        }
    }
}