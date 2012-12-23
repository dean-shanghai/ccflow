<%@ Page Title="" Language="C#" MasterPageFile="~/WF/OneFlow/MasterPage.master" AutoEventWireup="true" CodeFile="MyFlow.aspx.cs" Inherits="WF_OneFlow_MyFlow" %>
<%@ Register src="../UC/MyFlow.ascx" tagname="MyFlow" tagprefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="Right" Runat="Server">
    <uc1:MyFlow ID="MyFlow1" runat="server" />
</asp:Content>

