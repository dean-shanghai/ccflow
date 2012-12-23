<%@ Page Title="" Language="C#" MasterPageFile="~/WF/OneFlow/MasterPage.master" AutoEventWireup="true" CodeFile="HungUp.aspx.cs" Inherits="WF_OneFlow_HungUp" %>

<%@ Register src="UC/Working.ascx" tagname="Working" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Right" Runat="Server">
    <uc1:Working ID="Working1" runat="server" />
</asp:Content>

