<%@ Page Language="C#" MasterPageFile="~/WF/WinOpen.master" AutoEventWireup="true" 
CodeFile="Write.aspx.cs" Inherits="WF_Msg_Write" Title="信息发送" %>
<%@ Register src="../Pub.ascx" tagname="Pub" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<base target=_self />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:Pub ID="Pub1" runat="server" />
</asp:Content>