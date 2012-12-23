<%@ Page Language="C#" MasterPageFile="~/WF/Admin/WinOpen.master" AutoEventWireup="true" CodeFile="Cond.aspx.cs" Inherits="WF_Admin_Cond" Title="未命名頁面" %>
<%@ Register src="UC/CondExt.ascx" tagname="CondExt" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:CondExt ID="CondExt1" runat="server" />
</asp:Content>

