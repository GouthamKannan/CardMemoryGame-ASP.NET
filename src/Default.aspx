<%--
    This is the game selection page of the application

    File name     : Default.aspx
    Project name  : Card Memory Game 
    Written by    : Goutham
    Date modified : 23-09-2020
    Dependencies  : .NET framework 4.7.2
 --%>

<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CardMemoryGame.Default" %>

<%-- Content for Navagation bar --%>
<asp:Content ID="LoginContent" ContentPlaceHolderID="NavigationContent" runat="server">

    <%-- Drop down list for guest user --%>
    <asp:Menu ID="Guest" runat="server" OnMenuItemClick="Navclick" Orientation="horizontal" staticmenustyle="margin-right=10px margin-top=20px" 
        StaticEnableDefaultPopOutImage="false">
        <items>
            <asp:MenuItem Text="Guest" Value="Guest">
                <asp:MenuItem Text="Login" Value="Login"></asp:MenuItem>
            </asp:MenuItem>
        </items>
        </asp:Menu>

    <%-- Drop down list for logged in user --%>
    <asp:Menu ID="mnu" runat="server" OnMenuItemClick="Navclick" Orientation="horizontal" staticmenustyle="margin-right=10px margin-top=20px" 
        StaticEnableDefaultPopOutImage="false">
        <items>
            <asp:MenuItem Text="X" Value="X">
                <asp:MenuItem Text="Game Page" Value ="Game Page"></asp:MenuItem>
                <asp:MenuItem Text="My Profile" Value ="My Profile"></asp:MenuItem>
                <asp:MenuItem Text="View scores" Value ="View scores"></asp:MenuItem>
                <asp:MenuItem Text="logout" Value="logout"></asp:MenuItem>
            </asp:MenuItem>
        </items>
    </asp:Menu>

</asp:Content>

<%-- Content to select the number of cards for the game --%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <div class="box">
            <h2 class = "indexh1">Choose number of cards</h2>

            <asp:RadioButtonList CssClass="radiobutton" ID="RbSeclector"  runat="server">
                <asp:ListItem Text="12" value="3" Selected ="True"></asp:ListItem>
                <asp:ListItem Text="16" value="4"></asp:ListItem>
                <asp:ListItem Text="20" value="5"></asp:ListItem>
            </asp:RadioButtonList>

            <asp:Button ID="Btn_Start" runat="server" Text="Start" OnClick="Btn_Start_Click" />
        </div>

</asp:Content>




    