<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MasterPage.master" AutoEventWireup="true" CodeFile="HungUp.aspx.cs" Inherits="WF_HungUp" %>

<%@ Register src="UC/HungUp.ascx" tagname="HungUp" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:HungUp ID="HungUp1" runat="server" />
</asp:Content>

