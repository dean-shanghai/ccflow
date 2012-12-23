<%@ Page Title="" Language="C#" MasterPageFile="~/WF/OneFlow/MasterPage.master" AutoEventWireup="true" CodeFile="Working.aspx.cs" Inherits="WF_OneFlow_EmpWorks" %>
<%@ Register src="../UC/EmpWorks.ascx" tagname="EmpWorks" tagprefix="uc1" %>
<%@ Register src="UC/Working.ascx" tagname="Working" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Right" Runat="Server">
    <uc2:Working ID="Working1" runat="server" />
</asp:Content>


