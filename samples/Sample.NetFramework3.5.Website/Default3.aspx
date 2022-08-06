<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default3.aspx.cs" Inherits="_Default"  %>
<%@ Register TagPrefix="uc2" TagName="Header1" Src="top.ascx" %>

<!--DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"-->

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Ci35 - Home - Welcome</title>
    <link href="css/menu.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4/jquery.min.js"></script>
</head>
<body style="margin-top: 0px; margin-left: 0px; margin-right: 0px; margin-bottom: 0px;">
    <uc2:Header1 ID="Header1" runat="server" page_title="Home - Welcome"/>
    
    <center>
    <p>Welcome to <font size="16px">C</font>i35</p>
    
    <form id="form1" runat="server">
    <div>
    <p style="color:Gray;">This demonstrates the use of user control for header/footer.</p>
    </div>
    </form>
    
    </center>
    
</body>
</html>
