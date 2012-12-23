<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MasterPage.master" AutoEventWireup="true" CodeFile="GetTask.aspx.cs" Inherits="WF_JumpCheck" %>
<%@ Register src="UC/GetTask.ascx" tagname="JumpCheck" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:JumpCheck ID="JumpCheck1" runat="server" />
</asp:Content>

