<%@ Register TagPrefix="cc1" Namespace="BP.Web.Controls" Assembly="BP.Web.Controls" %>
<%@ Control Language="c#" Inherits="BP.Web.WF.Port.Navigation" CodeFile="Navigation.ascx.cs" %>
<div align="left">
	<center>
		<table id="Table1" height="100%" width="100%" border="0" align="center"    background="GTS<%=BP.Web.WebUser.Style%>.jpg" >
			<tr>
				<td align="left" width="305" rowSpan="2">&nbsp;</td>
				<td width="50%" align="right" height="14">
					<asp:hyperlink id="ChangeSys" ForeColor="Gold" Font-Bold="True" Font-Size="Smaller" runat="server"
						EnableViewState="False" Visible="True"></asp:hyperlink>&nbsp;<FONT color="#ffffff" size="5"><FONT face="Á¥Êé"></FONT></FONT><asp:hyperlink id="ChangeUser" Visible="True" EnableViewState="False" runat="server" Font-Size="Smaller"
						Font-Bold="True" ForeColor="Gold"></asp:hyperlink><asp:hyperlink id="AuthorizedAgent" Visible="True" EnableViewState="False" runat="server" Font-Size="Smaller"
						Font-Bold="True" ForeColor="Gold"></asp:hyperlink>
					<asp:hyperlink id="LoginWithA" Font-Size="Smaller" runat="server" EnableViewState="False" Visible="True"
						Font-Bold="True" ForeColor="Gold"></asp:hyperlink><asp:hyperlink id="BackToMySession" Visible="True" EnableViewState="False" runat="server" Font-Size="Smaller"
						Font-Bold="True" ForeColor="Gold"></asp:hyperlink><FONT face="Á¥Êé" color="#000066" size="5">
					</FONT>
				</td>
			</tr>
			<tr colspan="2">
				<TD align="center" width="100%">
					<asp:hyperlink id="HomeMenu" Visible="True" EnableViewState="False" runat="server" Font-Size="X-Small"
						Font-Bold="True" ForeColor="Crimson"></asp:hyperlink><asp:hyperlink id="MyWork" Visible="True" EnableViewState="False" runat="server" Font-Size="X-Small"
						Font-Bold="True" ForeColor="Crimson"></asp:hyperlink>
					<asp:hyperlink id="WorkHistory" Visible="True" EnableViewState="False" runat="server" Font-Size="X-Small"
						Font-Bold="True" ForeColor="Crimson"></asp:hyperlink>
					<asp:hyperlink id="MLWork" Visible="True" EnableViewState="False" runat="server" Font-Size="X-Small"
						Font-Bold="True" ForeColor="Crimson"></asp:hyperlink>
					<asp:hyperlink id="MLHistory" Visible="True" EnableViewState="False" runat="server" Font-Size="X-Small"
						Font-Bold="True" ForeColor="Crimson"></asp:hyperlink>
					<asp:hyperlink id="OnlineUsersMenu" Visible="True" EnableViewState="False" runat="server" Font-Size="X-Small"
						Font-Bold="True" ForeColor="Crimson"></asp:hyperlink>
					<asp:hyperlink id="NewMess" Visible="True" EnableViewState="False" runat="server" Font-Size="X-Small"
						Font-Bold="True" ForeColor="Crimson"></asp:hyperlink>
					<asp:hyperlink id="HelpMenu" Visible="True" EnableViewState="False" runat="server" Font-Size="X-Small"
						Font-Bold="True" ForeColor="Crimson"></asp:hyperlink>
					<asp:hyperlink id="PersonalMenu" Font-Size="X-Small" runat="server" EnableViewState="False" Visible="True"
						Font-Bold="True" ForeColor="Crimson"></asp:hyperlink>
					<asp:hyperlink id="MyMsgs" Font-Size="X-Small" runat="server" EnableViewState="False" Visible="True"
						Font-Bold="True" ForeColor="Crimson"></asp:hyperlink>
				</TD>
			</tr>
		</table>
	</center>
</div>
