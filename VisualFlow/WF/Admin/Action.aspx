<%@ Page Title="" Language="C#" MasterPageFile="WinOpen.master" AutoEventWireup="true" CodeFile="Action.aspx.cs" Inherits="WF_Admin_Action" %>
<%@ Register src="Pub.ascx" tagname="Pub" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="./../../Comm/Style/Table0.css" rel="stylesheet" type="text/css" />
    <link href="./../../Comm/Style/Table.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function DoDel(nodeid, xmlEvent) {
            if (window.confirm('您确认要删除吗?') == false)
                return;
            window.location.href = 'Action.aspx?NodeID=' + nodeid + '&DoType=Del&RefXml=' + xmlEvent;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table width='100%'>
<uc1:Pub ID="Pub3" runat="server" />
<tr>
<td valign=top class=Left><uc1:Pub ID="Pub1" runat="server" /></td>
<td valign=top><uc1:Pub ID="Pub2" runat="server" /></td>
  </tr>
    </table>
</asp:Content>