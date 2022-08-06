<%@ Control Language="C#" AutoEventWireup="false" CodeFile="top.ascx.cs" Inherits="top" %>

<%
    bool admin = false;
    try {
        admin = (Session["role"].ToString() == "admin");
    }
    catch { }
    
%>

<table cellpadding='0' cellspacing='0' border="0" style="background-image:url('images/bg.jpg'); background-size:100%; width:100%;">
<tr><td>
<h1>&nbsp;</h1>
</td></tr>
</table>

<%
Response.Write (writeMenu());
%>


