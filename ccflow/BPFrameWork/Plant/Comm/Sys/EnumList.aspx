﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Comm/WinOpen.master" AutoEventWireup="true" CodeFile="EnumList.aspx.cs" 
Inherits="Comm_Sys_EnumList" %>
<%@ Register src="../UC/UCSys.ascx" tagname="UCSys" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script language="JavaScript" src="../JScript.js"></script>
    <link href="../Style/Table0.css" rel="stylesheet" type="text/css" />
    <link href="../Style/Table.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <uc1:UCSys ID="UCSys1" runat="server" />
</asp:Content>