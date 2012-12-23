<%@ Page Title="" Language="C#" MasterPageFile="~/SSO/MasterPage.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="SSO_Default" %>

<%@ Register Src="~/SSO/PerAlert.ascx" TagName="PerAlert" TagPrefix="uc1" %>
<%@ Register Src="InfoBar.ascx" TagName="InfoBar" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Style/main.css" rel="stylesheet" type="text/css" />
       <link href="./../Comm/Style/Table.css" rel="stylesheet" type="text/css" />
        <link href="./../Comm/Style/Table0.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <uc1:PerAlert ID="PerAlert1" runat="server" />
    <uc2:InfoBar ID="ToolBar1" runat="server" />
</asp:Content>
