<%--
    This page is used to view and edit user details

    File name     : MyProfile.aspx
    Project name  : Card Memory Game 
    Written by    : Goutham
    Date modified : 24-10-2020
    Dependencies  : .NET framework 4.7.2
 --%>

<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="MyProfile.aspx.cs" Inherits="CardMemoryGame.MyProfile" %>

<%-- Content for navigation bar --%>
<asp:Content ID="Content1" ContentPlaceHolderID="NavigationContent" runat="server">
    
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

<%-- Content to display and edit details --%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <table style="margin-top:5%; margin-left:23%">
        <tr>
        <td class="box" style="left:35%; position:initial; transform: translate(0,0);">
            <h2 class="indexh2" style="text-transform: uppercase">My Profile</h2>
            
            <%-- Table to display user details --%>
            <table style="margin-left: 15px">

                <%-- User name --%>
                <tr class="profile_section">
                    <td style="text-align:center;"><asp:Label CssClass="ProfileLabel" ID="Label4" runat="server" Text="User Name" ForeColor="White"></asp:Label></td>
                    <td style="text-align:center;"><asp:TextBox CssClass="PofileValue" ID="UserName" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="text-align:center;" colspan="2"><asp:Label CssClass="ProfileLabel" ID="UsernameValidate" runat="server" ForeColor="White"></asp:Label></td>
                </tr>

                <%-- Password --%>
                <tr class="profile_section">
                    <td style="text-align:center;"><asp:Label CssClass="ProfileLabel" ID="Label6" runat="server" Text="E-mail" ForeColor="White"></asp:Label></td>
                    <td style="text-align:center;"><asp:TextBox CssClass="PofileValue" ID="Email" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="text-align:center;" colspan="2"><asp:Label CssClass="ProfileLabel" ID="EmailValidate" runat="server" ForeColor="White"></asp:Label></td>
                </tr>

                <%-- E-mail addres --%>
                <tr class="profile_section">
                    <td style="text-align:center;"><asp:Label CssClass="ProfileLabel" ID="Label5" runat="server" Text="Password" ForeColor="White"></asp:Label></td>
                    <td style="text-align:center;"><asp:TextBox CssClass="PofileValue" ID="Password" runat="server" TextMode="Password" 
                        Placeholder="Current Password"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="text-align:center;" colspan="2"><asp:Label CssClass="ProfileLabel" ID="PasswordValidate" runat="server" ForeColor="White"></asp:Label></td>
                </tr>

                 <%-- Generate button --%>
                <tr>
                    <td style="text-align:center;" colspan="2"><asp:Button runat="server" Text="Get OTP" onclick="GenerateOTP"/></td>
                </tr>

                <%-- OTP --%>
                <tr class="profile_section">
                    <td style="text-align:center;"><asp:Label CssClass="ProfileLabel" ID="Label7" runat="server" Text="OTP" ForeColor="White"></asp:Label></td>
                    <td style="text-align:center;"><asp:TextBox CssClass="PofileValue" ID="OtpBox" runat="server" TextMode="Password" 
                        Placeholder="OTP"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="text-align:center;" colspan="2"><asp:Label CssClass="OtpLabel" ID="OtpValidate" runat="server" ForeColor="White"></asp:Label></td>
                </tr>

                <%-- Button --%>
                <tr>
                    <td style="text-align:center;" colspan="2"><asp:Button runat="server" Text="Update Profile" onclick="UpdateProfileClick"/></td>
                </tr>
            </table>

            <%-- Validating inputs --%>
            <asp:RegularExpressionValidator ID="EmailValidator" ControlToValidate="Email" runat="server" 
            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
            ErrorMessage="Enter valid email address<br>" Display="Dynamic" ForeColor="White"></asp:RegularExpressionValidator>
            <asp:Label ID="UpdateLabel" runat="server" ForeColor="White"></asp:Label>

        </td>
        <td style="padding: 10px"></td>


        <td class="box" style="left: 65%; position:initial; transform: translate(0,0);">
            <h2 class="indexh2" style="text-transform: uppercase">Update password</h2>

            <%-- Table to change password --%>
            <table style="margin-left: 15px">

                <%-- Current password --%>
                <tr class="profile_section">
                    <td style="text-align:center;"><asp:Label CssClass="ProfileLabel" ID="Label1" runat="server" Text="Password" ></asp:Label></td>
                    <td style="text-align:center;"><asp:TextBox CssClass="PofileValue" ID="OldPassword" runat="server" TextMode="Password" 
                        Placeholder="Current Password"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="text-align:center;" colspan="2"><asp:Label CssClass="ProfileLabel" ID="OldPasswordValidate" runat="server" ForeColor="White" ></asp:Label></td>
                </tr>

                <%-- New password --%>
                <tr class="profile_section">
                    <td style="text-align:center;"><asp:Label CssClass="ProfileLabel" ID="Label2" runat="server" Text="New Password" ></asp:Label></td>
                    <td style="text-align:center;"><asp:TextBox CssClass="PofileValue" ID="NewPassword" runat="server" TextMode="Password" 
                        Placeholder="New Password"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="text-align:center;" colspan="2"><asp:Label CssClass="ProfileLabel" ID="NewPasswordValidate" runat="server" ForeColor="White" ></asp:Label></td>
                </tr>

                <%-- Retype new password --%>
                <tr class="profile_section">
                    <td style="text-align:center; max-width:20px;"><asp:Label CssClass="ProfileLabel" ID="Label3" runat="server"
                        Text="Retype New Password"></asp:Label></td>
                    <td style="text-align:center;"><asp:TextBox CssClass="PofileValue" ID="ReNewPassword" runat="server" TextMode="Password" 
                        Placeholder="New Password"></asp:TextBox></td>
                </tr>
                    <%-- Validating inputs --%>
                <tr>
                    <td style="text-align:center;" colspan="2"><asp:RegularExpressionValidator ID="PasswordValidator" ControlToValidate="NewPassword" runat="server" 
                    ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,15}$" 
                    ErrorMessage="Password should contain minimum eight and<br>maximum 15 characters, at least one uppercase letter,<br>one lowercase letter, 
                    one number and one special character<br>" Display="Dynamic" ForeColor="White"></asp:RegularExpressionValidator></td>
                </tr>
                <tr>
                    <td style="text-align:center;" colspan="2"><asp:Label CssClass="ProfileLabel" ID="RePasswordValidate" runat="server" ForeColor="White" ></asp:Label></td>
                </tr>

                <%-- Button --%>
                <tr>
                    <td style="text-align:center;" colspan="2"><asp:Button runat="server" Text="Update Password" OnClick="UpdatePassword_Click" /></td>
                </tr>
            </table>
            <asp:Label ID="PasswordUpdateLabel" runat="server" ForeColor="White"></asp:Label>
        </td>
        </tr>
    </table>

</asp:Content>