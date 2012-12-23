<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Dot2Dot.ascx.cs" Inherits="Comm_RefFunc_Dot2Dot_UC" %>
<%@ Register src="../UC/ToolBar.ascx" tagname="ToolBar" tagprefix="uc1" %>
<%@ Register src="../UC/Pub.ascx" tagname="Pub" tagprefix="uc2" %>
<%@ Register src="../UC/UCSys.ascx" tagname="UCSys" tagprefix="uc3" %>
<table width='100%' border="0" style="height:90px;"  >
<tr>
<td width='100%' class="ToolBar" >
    <uc1:ToolBar ID="ToolBar1" runat="server" />
    </td>
</tr>
<tr>
<td>
    <%--<asp:TreeView ID="Tree1" runat="server">
    </asp:TreeView>--%>

    <uc3:UCSys ID="UCSys1" runat="server" />
    </td>
</tr>
</table>
