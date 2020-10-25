<%--
    This page is used to register new user

    File name     : Signup.aspx
    Project name  : Card Memory Game 
    Written by    : Goutham
    Date modified : 24-10-2020
    Dependencies  : .NET framework 4.7.2
 --%>

<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="Signup.aspx.cs" Inherits="CardMemoryGame.Signup" %>

<%-- Content for navigation bar --%>
<asp:Content ID="LoginContent" ContentPlaceHolderID="NavigationContent" runat="server">

    <li class="indexh2"><a href="/Login.aspx">Login</a></li>

</asp:Content>

<%-- Content to get user details to register --%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="box">
        <h2 class="indexh2" style="text-transform: uppercase">Signup</h2>

        <%-- User name --%>
        <asp:TextBox runat="server" placeholder="user name" ID="UserName"></asp:TextBox>
        <asp:Label ID="UsernameValidate" runat="server" ForeColor="White"></asp:Label>

        <%-- E-mail address --%>
        <asp:TextBox runat="server" placeholder="E-mail" ID="Email"></asp:TextBox>
        <asp:Label ID="EmailValidate" runat="server" ForeColor="White"></asp:Label>
        <asp:RegularExpressionValidator ID="EmailValidator" ControlToValidate="Email" runat="server" 
            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
            ErrorMessage="Enter valid email address<br>" Display="Dynamic" ForeColor="White"></asp:RegularExpressionValidator>

        <%-- Generate button --%>
        <div class="Btn_Login" style="margin-left:0;">
            <asp:Button runat="server" Text="Get OTP" OnClick="GenerateOTP"/>
        </div>

        <%-- Password --%>
        <asp:TextBox runat="server" TextMode="Password" placeholder="password" ID="Password"></asp:TextBox>
        <asp:Label ID="PasswordValidate" runat="server" ForeColor="White"></asp:Label>
        <asp:RegularExpressionValidator ID="PasswordValidator" ControlToValidate="Password" runat="server" 
            ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,15}$" 
            ErrorMessage="Password should contain minimum eight and<br>maximum 15 characters, at least one uppercase letter,<br>
            one lowercase letter, one number and one special character<br>" Display="Dynamic" ForeColor="White"></asp:RegularExpressionValidator>

        <%-- Retype password --%>
        <asp:TextBox runat="server" TextMode="Password" placeholder="retype password" ID="RePassword"></asp:TextBox>
        <asp:Label ID="RePasswordValidate" runat="server" ForeColor="White"></asp:Label>
        <asp:CompareValidator ID="ComparePassword" runat="server" ControlToValidate="RePassword" 
            ControlToCompare="Password" ErrorMessage="Password and Retype password must be same<br>" Display="Dynamic" ForeColor="White"></asp:CompareValidator>

        <%-- OTP --%>
        <asp:TextBox runat="server" TextMode="Password" placeholder="OTP" ID="OTPbox"></asp:TextBox>
        <asp:Label ID="OtpInvalidLabel" runat="server" ForeColor="White"></asp:Label>

        <%-- SignUp button --%>
        <div class="Btn_Login" style="margin-left:0;">
            <asp:Button runat="server" Text="Sign up" onclick="SignupButton"/>
        </div>
        <asp:Label ID="SignUpValidate" runat="server" ForeColor="White"></asp:Label>

    </div>
   
</asp:Content>
