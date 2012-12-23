<%@ Page Language="C#" MasterPageFile="~/WF/MasterPage.master" AutoEventWireup="true" CodeFile="Forward.aspx.cs" Inherits="WF_Forward_UI" Title="无标题页" %>
<%@ Register src="UC/ForwardUC.ascx" tagname="ForwardUC" tagprefix="uc266" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc266:ForwardUC ID="ForwardUC1" runat="server" />
</asp:Content>