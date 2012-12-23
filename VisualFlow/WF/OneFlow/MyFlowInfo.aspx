<%@ Page Title="" Language="C#" MasterPageFile="~/WF/OneFlow/MasterPage.master" AutoEventWireup="true" CodeFile="MyFlowInfo.aspx.cs" Inherits="WF_OneFlow_MyFlowInfo" %>

<%@ Register src="../UC/MyFlowInfo.ascx" tagname="MyFlowInfo" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Right" Runat="Server">
    <uc1:MyFlowInfo ID="MyFlowInfo1" runat="server" />
</asp:Content>

