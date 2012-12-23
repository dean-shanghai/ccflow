<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConnView.aspx.cs" Inherits="Comm_Sys_ConnView" %>

<%@ Register Src="../UC/ucsys.ascx" TagName="ucsys" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>测试</title>
	<script language="JavaScript" src="../../Comm/JScript.js"></script>
    <link href="../Style/Table.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:ucsys ID="Ucsys1" runat="server" />
    </div>
    </form>
</body>
</html>
