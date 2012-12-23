<%@ Page language="c#" CodeFile="ClearDatabase.aspx.cs" AutoEventWireup="false" Inherits="BP.Web.WF.WF.DeleteZF" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>DeleteZF</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body >
		<form id="Form1" method="post" runat="server">
		 <fieldset>
                <legend>警告：</legend>
                <font color=red >在执行删除前一定要确认，这些运行中的数据是否需要。<BR>一但执行删除数据将无法恢复。</font>
                </fieldset>
                <br />
                        <asp:Button ID="Button2" runat="server"  Text="清除流程中运行的数据" OnClick="Button2_Click" /></td>
                 
		&nbsp;&nbsp;&nbsp; ---&nbsp;
         <asp:Button ID="Button3" runat="server" onclick="Button3_Click1" 
             Text="清除所有设计的流程" />
                 
		</form>
	</body>
</HTML>
