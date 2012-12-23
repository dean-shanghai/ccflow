<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PerAlert.ascx.cs" Inherits="SSO_PerAlert" %>
<div class="PerAlert">
    <ul>
        <li><a href="Default.aspx">首页</a> &nbsp;&nbsp;</li>
        <%foreach (BP.GPM.PerAlert pl in PerAlerts)
          {%>
        <li><a href="<%=pl.Url%>" target="_blank" >
            <img alt="" src="<%=pl.ICON%>" width="16px" height="16px" border="0" />
            <%=pl.Name%>(<%=GetNum(pl)%>)</a>&nbsp;&nbsp;</li>
        <% } %>

        <li style="float: right;">
            <ul style="margin: 0; padding: 0;">
                <li><a href='STemSettingPage.aspx'>密码设置 </a></li>
                <li><a href='STemSettingPage.aspx'>密码修改 </a></li>
                <li><a href='Default.aspx?DoType=Setting'>信息块定义 </a></li>
            </ul>
        </li>
    </ul>
</div>
