<%@ Page Language="C#" MasterPageFile="~/WF/MapDef/WinOpen.master" AutoEventWireup="true" CodeFile="CopyDtlField.aspx.cs" Inherits="WF_MapDef_CopyDtlField" Title="Untitled Page" %>

<%@ Register src="UC/CopyDtlField.ascx" tagname="CopyDtlField" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../../Comm/Style/Table0.css" rel="stylesheet" type="text/css" />

	<script language="JavaScript" src="../../Comm/JScript.js" ></script>

	<base target="_self" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:CopyDtlField ID="CopyDtlField1" runat="server" />
</asp:Content>

