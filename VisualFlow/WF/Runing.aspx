<%@ Page Language="C#" MasterPageFile="~/WF/MasterPage.master" AutoEventWireup="true" CodeFile="Runing.aspx.cs" Inherits="WF_Runing" Title="无标题页" %>

<%@ Register src="UC/Runing.ascx" tagname="Runing" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script language=javascript>
		
		</script>
    <uc1:Runing ID="Runing1" runat="server" />
</asp:Content>

