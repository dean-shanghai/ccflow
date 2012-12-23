<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileManager.aspx.cs" Inherits="WF_FileManager" %>
<%@ Register src="Pub.ascx" tagname="Pub" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>驰骋软件</title>
		<base target=_self />
		<script language="JavaScript" src="../Comm/JScript.js"></script>
		<script language="JavaScript" src="../Comm/ActiveX.js"></script>
	 <script language="javascript">
		 function DoAction(url, msg )
         {
           if ( confirm("提示: \n \n 将要执行删除确认吗？" )==false )
		      return;
	        window.location.href= url;
         }
		</script>
       
</head>
<body topmargin="0" leftmargin="0" >
    <form id="form1" runat="server">
        <uc2:Pub ID="Pub1" runat="server" />
    </form>
</body>
</html>
