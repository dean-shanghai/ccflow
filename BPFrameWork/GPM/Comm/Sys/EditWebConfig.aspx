<%@ Page Language="C#" MasterPageFile="~/Comm/MasterPage.master" AutoEventWireup="true" CodeFile="EditWebConfig.aspx.cs" Inherits="Comm_Sys_EditWebconfig" Title="System Seting" %>
<%@ Register src="../UC/UCSys.ascx" tagname="UCSys" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../Style/Table0.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:UCSys ID="UCSys1" runat="server" />
</asp:Content>

