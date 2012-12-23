<%@ Page Title="" Language="C#" MasterPageFile="WinOpen.master" AutoEventWireup="true" CodeFile="MyFlowInfoSmallSingle.aspx.cs" Inherits="WF_MyFlowInfoSmallSingle" %>
<%@ Register src="UC/MyFlow.ascx" tagname="MyFlow" tagprefix="uc2" %>
<%@ Register src="UC/MyFlowInfoWap.ascx" tagname="MyFlowInfoWap" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script language="JavaScript" src="../Comm/JScript.js" ></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:MyFlowInfoWap ID="MyFlowInfoWap1" runat="server" />
</asp:Content>

