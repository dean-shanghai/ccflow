<%@ Page Language="C#" MasterPageFile="~/Comm/MasterPage.master" AutoEventWireup="true" CodeFile="EnsCfg.aspx.cs" Inherits="Comm_Sys_EnConfig" Title="无标题页" %>

<%@ Register src="../UC/UCSys.ascx" tagname="UCSys" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../Table.css" rel="stylesheet" type="text/css" />
    <base target=_self />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:UCSys ID="UCSys1" runat="server" />
</asp:Content>

