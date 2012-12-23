<%@ Page Title="" Language="C#" MasterPageFile="~/WF/OneFlow/MasterPage.master" AutoEventWireup="true" CodeFile="Start.aspx.cs" Inherits="WF_OneFlow_Start" %>
<%@ Register src="UC/StartList.ascx" tagname="StartList" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Right" Runat="Server">
    <uc1:StartList ID="StartList1" runat="server" />
</asp:Content>

