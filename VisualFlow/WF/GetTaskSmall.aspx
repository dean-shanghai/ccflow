<%@ Page Title="" Language="C#" MasterPageFile="~/WF/WinOpen.master" AutoEventWireup="true" CodeFile="GetTaskSmall.aspx.cs" Inherits="WF_JumpCheckSmall" %>
<%@ Register src="UC/GetTask.ascx" tagname="JumpCheck" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:JumpCheck ID="JumpCheck1" runat="server" />
</asp:Content>

