<%@ Page Title="" Language="C#" MasterPageFile="~/WF/WorkOpt/WinOpen.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="WF_WorkOpt_Home" %>

<%@ Register src="../Pub.ascx" tagname="Pub" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<uc1:Pub ID="Pub2" runat="server" />
</asp:Content>

