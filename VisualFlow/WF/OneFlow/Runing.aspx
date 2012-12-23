<%@ Page Title="" Language="C#" MasterPageFile="~/WF/OneFlow/MasterPage.master" AutoEventWireup="true" CodeFile="Runing.aspx.cs" Inherits="WF_OneFlow_Runing" %>

<%@ Register src="./UC/Runing.ascx" tagname="Runing" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Right" Runat="Server">
    <uc1:Runing ID="Runing1" runat="server" />
</asp:Content>

