<%--
    This page is used to login existing user or allow to play as guest

    File name     : Login.aspx
    Project name  : Card Memory Game 
    Written by    : Goutham
    Date modified : 23-09-2020
    Dependencies  : .NET framework 4.7.2
 --%>

<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CardMemoryGame.Login" %>

<%-- Content for navigation bar--%>
<asp:Content ID="LoginContent" ContentPlaceHolderID="NavigationContent" runat="server">

    <li class="indexh2"><a href="/Signup.aspx">Sign up</a></li>

</asp:Content>

<%-- Content for getting user details for logging in --%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="box">

        <%-- Getting the details from user --%>
        <h2 class="indexh2" style="text-transform: uppercase">login</h2>
        <asp:TextBox ID="Username" runat="server" placeholder="user name"></asp:TextBox>
        <asp:TextBox ID="Password" runat="server" TextMode="Password" placeholder="password"></asp:TextBox>
        <asp:Label ID="InvalidLabel" runat="server" ForeColor="White"></asp:Label>
        <p style="color: white;">forget password? <asp:LinkButton ID="LinkButton1" runat="server" OnClick="ResetPassword" ForeColor="White">click here</asp:LinkButton></p>
        
        <%-- Buttons --%>
        <div class="Btn_Login">
            <asp:Button cssclass="LoginButton" runat="server" Text="Log in" style="margin-right:10px" onclick="LoginButton"/>
            <asp:Button cssclass="GuestLogin" runat="server" Text="Guest" style="margin-left:10px" onclick="PlayAsGuest"/>    
        </div>

    </div>

</asp:Content>

