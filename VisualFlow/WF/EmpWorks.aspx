<%@ Page Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" 
CodeFile="EmpWorks.aspx.cs" Inherits="Face_EmpWorks" Title="待办工作" %>
<%@ Register src="Pub.ascx" tagname="Pub" tagprefix="uc1" %>
<%@ Register src="UC/EmpWorks.ascx" tagname="EmpWorks" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc2:EmpWorks ID="EmpWorks1" runat="server" />
</asp:Content>