﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Tools.ascx.cs" Inherits="WF_UC_Tools" %>
<%@ Register src="Pub.ascx" tagname="Pub" tagprefix="uc1" %>
<%@ Register src="ToolsWap.ascx" tagname="ToolsWap" tagprefix="uc2" %>
<table border=0 width='80%' >
<tr>
<td  valign=top width='20%' align='left' >
    <uc1:Pub ID="Left" runat="server" />
    </td>
<td  valign=top  align='left'  width='80%' >
    <uc2:ToolsWap ID="ToolsWap1" runat="server" />
    </td>
</tr>
</table>
