﻿<%@ Page Title="JSLab" Language="C#" MasterPageFile="~/Comm/WinOpen.master" AutoEventWireup="true" CodeFile="FuncLib.aspx.cs" Inherits="Comm_Sys_FuncLib" %>
<%@ Register src="Pub.ascx" tagname="Pub" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  <link href="../Style/Table.css" rel="stylesheet" type="text/css" />
    <link href="../Style/Table0.css" rel="stylesheet" type="text/css" />
		<script language="JavaScript" src="../JScript.js" type="text/javascript" ></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <uc1:Pub ID="Pub1" runat="server" />
</asp:Content>