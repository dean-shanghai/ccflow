<%@ Page Language="C#" MasterPageFile="~/WF/Admin/WinOpen.master" AutoEventWireup="true" CodeFile="CondStation.aspx.cs" Inherits="WF_Admin_CondStation" Title="未命名頁面" %>
<%@ Register src="UC/CondStation.ascx" tagname="CondStation" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:CondStation ID="CondStation1" runat="server" />
</asp:Content>

