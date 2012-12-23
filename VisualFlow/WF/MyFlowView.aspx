<%@ Page Title="" Language="C#" MasterPageFile="~/WF/WinOpen.master" AutoEventWireup="true" CodeFile="MyFlowView.aspx.cs" Inherits="WF_MyFlowView" %>
<%@ Register src="UC/UCEn.ascx" tagname="UCEn" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">
.Bar
{
    width:500px;
    text-align:center;
}
#tabForm, D
{
    width:960px;
    text-align:left;
    margin:0 auto;
    margin-bottom:5px;
}
#divFreeFrm {
 position:relative;
 left:25PX;
 width:960px;
}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id=tabForm >
</div>
<div id="D" >
    <uc1:UCEn ID="UCEn1" runat="server" />
</div>
</asp:Content>

