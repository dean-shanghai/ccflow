<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FeatureSet.ascx.cs" Inherits="WF_Admin_UC_FeatureSet" %>
<%@ Register src="Pub.ascx" tagname="Pub" tagprefix="uc1" %>
<%@ Register src="../../../Comm/UC/ToolBar.ascx" tagname="ToolBar" tagprefix="uc2" %>
<table border=0 width='100%' >
<tr>
<td valign=top align=left width='30%'>
    <uc1:Pub ID="Pub1" runat="server" />
    </td>
<td valign=top align=left width='70%'>
    <uc1:Pub ID="Pub2" runat="server" />
    </td>
</tr>
</table>
