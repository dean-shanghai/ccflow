<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Pub.ascx.cs" Inherits="SSO_Pub" %>
<div class="Pub">
    <ul>
        <% foreach (BP.GPM.STem en in STems)
           {
               if (en.IsEnable == false)
                   continue;

               if (en.No == "SSO")
                   continue;
        %>
        <li><a href="<%=en.Url %>" target="<%=en.OpenWay %>">
            <img src="<%=en.ICON %>" width="220px" border="0"></a> </li>
        <% }%>
       <%-- <li><a href='STemSettingPage.aspx'>密码设置 </a></li>
        <li><a href='STemSettingPage.aspx'>密码修改 </a></li>
        <li><a href='Default.aspx?DoType=Setting'>信息块定义 </a></li>--%>
    </ul>
</div>
