<%@ Page Title="" Language="C#" MasterPageFile="~/SSO/MasterPage.master" AutoEventWireup="true"
    CodeFile="STemSettingPage.aspx.cs" Inherits="SSO_STemSettingPage" %>
<%@ Register Src="~/SSO/PerAlert.ascx" TagName="PerAlert" TagPrefix="uc1" %>
<%@ Register Src="STemSetting.ascx" TagName="STemSetting" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Style/main.css" rel="stylesheet" type="text/css" />
        <link href="./../Comm/Style/Table.css" rel="stylesheet" type="text/css" />
        <link href="./../Comm/Style/Table0.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <uc1:PerAlert ID="PerAlert1" runat="server" />
    <uc2:STemSetting ID="ToolBarss1" runat="server" />
</asp:Content>
