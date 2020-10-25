<%--
    This is the game page of the application

    File name     : GamePage.aspx
    Project name  : Card Memory Game 
    Written by    : Goutham
    Date modified : 23-09-2020
    Dependencies  : .NET framework 4.7.2
 --%>

<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="GamePage.aspx.cs" Inherits="CardMemoryGame.GamePage" %>

<%-- Content for the navigation bar --%>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationContent" runat="server">
    
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

<%-- Content for playing the game --%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
    
    <%-- Instruction content --%>
    <div id="instruction" class=" overlay " OnClick="myFunction()">INSTRUCTIONS
        <span class="overlay-small"><br>Click on a card to flip it.<br>If two consecutive flipped cards are not same, they are flipped back automatically.<br>
            The game ends when timer runs out or all the matching cards are identified.</span>
        <span class="overlay-small"><br>Click to Start</span>
    </div>
    
    <%-- Game lost content --%>
    <div id="gameover" class="overlay">TIME'S UP
         <asp:Button CssClass="overlay-small" style="cursor:pointer" ID="GameOverBtn" runat="server" Text="Click to Restart" OnClick="GameOver_RedirectToHome"/>
    </div>
    
    <%-- Game won content --%>
    <div id="victory" class="overlay">YOU WON
         <%--<span class="overlay-small" >Click to Restart</span>--%> 
        <%--<asp:HiddenField ID="VictoryRedirect" runat="server" onclick="Victory_RedirectToHome" />--%>
        <%--<a class="overlay-small" runat="server" href="Default.aspx">Click to Restart</a>--%>   
        <asp:Label runat="server" ID="UserScore" ClientIDMode="Static"></asp:Label>
        <asp:HiddenField ID="UserScoreHidden" runat="server" ClientIDMode="Static"/>
         <asp:Button CssClass="overlay-small" style="cursor:pointer" ID ="VictoryBtn" runat="server" Text="Click to Restart" OnClick="Victory_RedirectToHome"/>
    </div>
    
    <%-- Game content --%>
    <div class="game-info-container">
        <div class="game-info">
            Time: <span id="time-remain"></span>
        </div>
        <div class="game-info">
            Flips: <span id="filps">0</span>
        </div>
    </div>
    
    <div id="blocker"></div>
    <div id="dummygrid" ></div>
    
    <%-- Footer content --%>
    <%--<footer class="footer-game-page">
        <p>This is just a test website. Do not enter any personal or sensitive information<br />
                Image are taken form <a href="https://opengameart.org/" style="color: white;"> here</a> 
                and <a href="https://www.pinterest.ca/pin/598697344184536467/" style="color: white;">here</a>
    </footer>--%>
    
    <script type ="text/javascript">
        window.load = startinstruction();
    </script>

</asp:Content>

