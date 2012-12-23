<%@ Page Title="" Language="C#" MasterPageFile="~/WF/WinOpen.master" AutoEventWireup="true" CodeFile="WatchDog.aspx.cs" Inherits="WF_Admin_Sys_WatchDogaspx" %><%@ Register src="../Pub.ascx" tagname="Pub" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../../../Comm/Style/Tabs.css" rel="stylesheet" type="text/css" />
    <link href="../../../Comm/Style/Table.css" rel="stylesheet" type="text/css" />
    <link href="../../../Comm/Style/Table0.css" rel="stylesheet" type="text/css" />

    <script language=javascript>
        function DelIt(workid, fk_flow) {
            if (window.confirm('您确定要删除吗？') == false)
                return;
            var url = 'Do.aspx?DoType=DelFlow&WorkID=' + workid + '&FK_Flow=' + fk_flow;
            var b = window.showModalDialog(url, 'ass', 'dialogHeight: 500px; dialogWidth: 700px;center: yes; help: no');
            window.location.href = window.location.href;
        }
        function Track(workid, fk_flow, fid) {
            var url = '../../Chart.aspx?WorkID=' + workid + '&FK_Flow=' + fk_flow + '&FID=' + fid;
           // var b = window.open(url, 'ass', 'dialogHeight: 500px; dialogWidth: 700px;center: yes; help: no');
            var c = window.showModalDialog(url, 'ass', 'dialogHeight: 500px; dialogWidth: 700px;center: yes; help: no');
        }
        function Rpt(workid, fk_flow, fid) {
            var url = '../../WFRpt.aspx?WorkID=' + workid + '&FK_Flow=' + fk_flow + '&FID=' + fid;
            // var b = window.open(url, 'ass', 'dialogHeight: 500px; dialogWidth: 700px;center: yes; help: no');
            var c = window.showModalDialog(url, 'ass', 'dialogHeight: 500px; dialogWidth: 700px;center: yes; help: no');
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table>
<tr>
<td></td>
<td ><uc1:Pub ID="Top" runat="server" /></td>
</tr>
<tr>
<td colspan=1 width='1%'><uc1:Pub ID="Left" runat="server" /></td>
<td colspan=1 valign=top><uc1:Pub ID="Right" runat="server" /></td>
</tr>
</table>
</asp:Content>

