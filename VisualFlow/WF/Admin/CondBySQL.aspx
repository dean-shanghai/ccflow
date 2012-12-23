<%@ Page Title="" Language="C#" MasterPageFile="~/WF/Admin/WinOpen.master" AutoEventWireup="true" CodeFile="CondBySQL.aspx.cs" Inherits="WF_Admin_CondBySQL" %>
<%@ Register src="UC/CondBySQL.ascx" tagname="CondBySQL" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:CondBySQL ID="CondBySQL1" runat="server" />
</asp:Content>

