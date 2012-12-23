<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MyFlowUC.ascx.cs" Inherits="WF_UC_MyFlowUC" %>
<%@ Register src="../../Comm/UC/UCSys.ascx" tagname="UCSys" tagprefix="uc3" %>
<%@ Register src="../../Comm/UC/ToolBar.ascx" tagname="ToolBar" tagprefix="uc4" %>
<%@ Register src="Pub.ascx" tagname="Pub" tagprefix="uc5" %>
<%@ Register src="UCEn.ascx" tagname="UCEn" tagprefix="uc6" %>
    <%@ Register src="MyFlow.ascx" tagname="MyFlow" tagprefix="uc2" %>
    <style type="text/css">
        .style1
        {
            width: 225px;
        }
        
        .left
        {
            float:left;
        }
        .right
        {
            float:right;
        }
        
        .main
        {
            text-align:left;
            
        }
    </style>
	<script language="JavaScript" src="./../../Comm/JScript.js"></script>

    <div align=main  >
 <%--   <span >
    <uc1:FlowInfoSimple ID="FlowInfoSimple1" runat="server" />
    </span>--%>

    <span align=center >
    <uc2:MyFlow ID="MyFlow1" runat="server" />
    </span>
    </div>

