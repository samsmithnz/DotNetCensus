<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Title="Home" Inherits="Default" MasterPageFile="~/Site.master" %>

<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">
    
    <div style="margin-left:10px; margin-right:10px;">

    <center>
    <p>Welcome to <font size="16px">Ci</font>35.</p>
    
    <div>
    <asp:Login ID="btnLogin" runat="server" CssClass="LoginControl" BackColor="#F7F7DE" BorderColor="#CCCC99" 
            BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="10pt" 
            onauthenticate="btnLogin_Authenticate">
    <TitleTextStyle BackColor="#6B696B" Font-Bold="True" ForeColor="#FFFFFF" />
    </asp:Login>
     <asp:Label ID="Label1" runat="server"></asp:Label>
    </div>
        
    <p><a href="Register.aspx">Register</a> | <a href="Getpwd.aspx">Forgot Password</a></p>
    
    </center>
    
    </div>
</asp:Content>

