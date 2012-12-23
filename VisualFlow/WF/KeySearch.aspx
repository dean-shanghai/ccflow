<%@ Page Title="" Language="C#" MasterPageFile="~/WF/WinOpen.master" AutoEventWireup="true" CodeFile="KeySearch.aspx.cs" Inherits="WF_KeySearch" %>

<%@ Register src="UC/KeySearch.ascx" tagname="KeySearch" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:KeySearch ID="KeySearch1" runat="server" />
</asp:Content>

