<%@ Page Language="c#" Inherits="BP.Web.WF.Port.LeftTree" CodeFile="LeftOutLook.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="UCSys" Src="../../Comm/UC/UCSys.ascx" %>
<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>LeftTree</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="LeftOutlook.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="LeftOutlook.js"></script>
    <style type="text/css">
        A:link
        {
            color: #636363;
        }
        A.ff
        {
            font-size: 12px;
            text-decoration: none;
        }
        A:visited
        {
            color: #999999;
        }
        A:hover
        {
            color: #cc0033;
        }
        
        .Title
        {
        }
    </style>
</head>
<body topmargin="0" leftmargin="0" class="Body">
    <form id="Form1" method="post" runat="server">
    <uc1:UCSys ID="UCSys1" runat="server"></uc1:UCSys>
    <iewc:TreeView ID="TreeView1" runat="server"></iewc:TreeView>
    <asp:TreeView ID="TreeView2" runat="server">
    </asp:TreeView>
    </form>
</body>
</html>
