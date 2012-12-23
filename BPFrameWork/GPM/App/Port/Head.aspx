s<%@ Page language="c#" Inherits="BP.Web.App.Head1" CodeFile="Head.aspx.cs" %>

<%@ Register Src="../../Comm/UC/ucsys.ascx" TagName="ucsys" TagPrefix="uc2" %>
<%@ Register TagPrefix="uc1" TagName="Navigation" Src="Navigation.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Home</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../Comm/Style.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body  topmargin="0" leftmargin="0"   background="./../Images/ccflow/Banal.jpg"   >
		<form id="Form1" method="post" runat="server">
            <uc2:ucsys ID="Ucsys1" runat="server" />
		</form>
	</body>
</HTML>
