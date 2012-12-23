<%@ Page Title="" Language="C#" MasterPageFile="~/WF/WAP/MasterPage.master" AutoEventWireup="true" CodeFile="JumpWap.aspx.cs" Inherits="WF_WAP_JumpWap" %>

<%@ Register src="../UC/JumpWay.ascx" tagname="JumpWay" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:JumpWay ID="JumpWay1" runat="server" />
</asp:Content>

