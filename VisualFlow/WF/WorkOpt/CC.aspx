<%@ Page Title="" Language="C#" MasterPageFile="~/WF/WinOpen.master" AutoEventWireup="true" CodeFile="CC.aspx.cs" Inherits="WF_WorkOpt_CC" %>
<%@ Register src="./../Pub.ascx" tagname="Pub" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="./../../Comm/Style/Table.css" rel="stylesheet" type="text/css" />
    <link href="./../../Comm/Style/Table0.css" rel="stylesheet" type="text/css" />

    <script  type="text/javascript">
        function ShowIt(tb) {
            var url = 'SelectEmps.aspx?OID=123&CtrlVal=' + tb.value;
            var v = window.showModalDialog(url, 'dfg', 'dialogHeight: 450px; dialogWidth: 550px; dialogTop: 100px; dialogLeft: 150px; center: yes; help: no');
            if (v == null || v == '' || v == 'NaN') {
                return;
            }
            tb.value = v;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:Pub ID="Pub1" runat="server" />
</asp:Content>