<%@ Page Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="Link.aspx.cs" Inherits="WAP_Link" Title="Link" %>
<%@ Register src="../UC/Link.ascx" tagname="Link" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:Link ID="Link1" runat="server" />
</asp:Content>

