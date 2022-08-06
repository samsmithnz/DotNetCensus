<%@ Page Language="c#" AutoEventWireup="true"  %>
<%@ Register TagPrefix="uc2" TagName="Header1" Src="top.ascx" %>

<script language="c#" runat="server">
public void Page_Load(object sender, EventArgs e)
{
    // some load code    
}
</script>

<!--DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"-->
<!--Use this instead of the DOCTYPE below, or not use DOCTYPE, causes bigger height of header image.-->

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Ci35 - Home - Welcome</title>
    <link href="css/menu.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4/jquery.min.js"></script>
</head>
<body style="margin-top: 0px; margin-left: 0px; margin-right: 0px; margin-bottom: 0px;">
    <uc2:Header1 ID="Header1" runat="server" page_title="About Us" />

<center>
<p>Welcome to <font size='16px'>C</font>i35. This is a C#.NET website framework.</p>

<p>Started construction since June 2013.</p>

</center>


</body>
</html>