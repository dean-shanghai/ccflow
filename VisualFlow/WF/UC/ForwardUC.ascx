<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ForwardUC.ascx.cs" Inherits="WF_UC_Forward_UC" %>
<%@ Register src="../../Comm/UC/ToolBar.ascx" tagname="ToolBar" tagprefix="uc1" %>
<%@ Register src="Pub.ascx" tagname="Pub" tagprefix="uc2" %>
<table border=0  style="width: 60%; height: 100%" align=center>
            <tr>
                <td colspan=1  valign=top class=ToolBar align=left>
                    <uc1:ToolBar ID="ToolBar1" runat="server" /> 
                </td>
            </tr>
            
            <tr>
            <td bgcolor=InfoBackground align=right  valign=top>
                <uc2:Pub ID="Top" runat="server" />
             </td>
            </tr>
            <tr>
            <td  valign=top style="width: 300px" align=center>
                <uc2:Pub ID="Pub1" runat="server" />
                </td>
            </tr>
</table>
