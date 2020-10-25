<%--
    This page is used to view top scores

    File name     : ViewScores.aspx
    Project name  : Card Memory Game 
    Written by    : Goutham
    Date modified : 23-09-2020
    Dependencies  : .NET framework 4.7.2
 --%>

<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="ViewScores.aspx.cs" Inherits="CardMemoryGame.ViewScores" %>

<%-- Content for navigation bar --%>
<asp:Content ID="Content1" ContentPlaceHolderID="NavigationContent" runat="server">

    <%-- Drop down list for logged in user --%>
    <asp:Menu ID="mnu" runat="server" OnMenuItemClick="Navclick" Orientation="horizontal" staticmenustyle="margin-right=10px margin-top=20px" StaticEnableDefaultPopOutImage="false">
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

<%-- Content to display scores --%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <table >
        <tr>

            <%-- Display overall top 10 scores --%>
            <td class="box" style="left:35%; position:absolute; transform: translate(-50%,-50%);">
                <h2 class="indexh2" style="text-transform: uppercase">Top Scores</h2> 
                <asp:Table ID="tbl" runat="server" font-size="Large" />
            </td>

            <%-- Display top 10 scores of current user --%>
            <td class="box" style="left: 65%; position:absolute; transform: translate(-50%,-50%);">
                <h2 class="indexh2" style="text-transform: uppercase">Your Top 10 Scores</h2>
                <asp:Table ID="tb2" runat="server" font-size="Large" />
            </td>

        </tr>
    </table>

</asp:Content>
