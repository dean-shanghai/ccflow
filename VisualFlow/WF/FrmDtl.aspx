<%@ Page Title="" Language="C#" MasterPageFile="~/WF/WinOpen.master" AutoEventWireup="true" CodeFile="FrmDtl.aspx.cs" Inherits="WF_FrmDtl" %>

<%@ Register src="UC/UCEn.ascx" tagname="UCEn" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

  <style type="text/css">
        .HBtn
        {
        	/* display:none; */
        	visibility:visible;
        }
    </style>
	<script language="JavaScript" src="./../Comm/JScript.js"></script>
    <script language="JavaScript" src="./../Comm/JS/Calendar/WdatePicker.js" defer="defer" ></script>
    <script language=javascript>
        function ss() {
            document.getElementById()
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:Button ID="Btn_Save" runat="server" Text="保存"  CssClass="Btn" Visible=true 
        onclick="Btn_Save_Click"  />
    <uc1:UCEn ID="UCEn1" runat="server" />
</asp:Content>

