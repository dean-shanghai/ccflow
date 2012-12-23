<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MapDef/WinOpen.master" AutoEventWireup="true" CodeFile="MapExt.aspx.cs" Inherits="WF_MapDef_MapExt" %>
<%@ Register src="UC/MExt.ascx" tagname="MExt" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:MExt ID="MExt1" runat="server" />
</asp:Content>

