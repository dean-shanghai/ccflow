<%@ Page Title="" Language="C#" MasterPageFile="~/WF/WinOpen.master" AutoEventWireup="true" CodeFile="ViewFrm.aspx.cs" 
Inherits="WF_MapDef_FreeFrm_ViewFrm" %>
<%@ Register src="../UC/UCEn.ascx" tagname="UCEn" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script src="./../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script language="JavaScript" src="../../Comm/JScript.js"></script>
    <script language="JavaScript" src="MapDef.js"></script>
    <script language="JavaScript" src="./../Style/Verify.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:UCEn ID="UCEn1" runat="server" />
</asp:Content>