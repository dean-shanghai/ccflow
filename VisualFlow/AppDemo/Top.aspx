<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Top.aspx.cs" Inherits="AppDemo_Top" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="./css/basic.css" rel="stylesheet" type="text/css" />
    <link href="./css/base.css" rel="stylesheet" type="text/css" />
    <script language="javascript">
        function relogin() {
            //if(confirm("是否确定要重新登陆？"))
            //parent.location.href = "login.aspx?flag=logout";
            parent.location.href = "<%=webloginurl %>";
        }
    </script>
    <script type="text/javascript" language="javascript">
        var timer;
        function startTimer() {
            return;
            timer = setInterval("alert()", 60000);  //1分执行一次
        }
        function alert() {
            var arr = " <%=tempcount%>";
            //            var sname = "<%=sname%>";
            //            if (sname = 0) {
            //                
            //                return;
            //            }
            if (arr > 0) {
                //ifrmTop.src = "treelist\\setsession.ashx?sname=0";
                // ifrmTop.title
                ifrmTop.location.href = "treelist\\setsession.ashx?sname=0";

                //window.location.href("treelist\\setsession.ashx?sname=0");
                openwin();
                //refwindow(arr);
            }
        }
        function refwindow(num) {
            return;
            var num = num;
            num++;
            var mytimeout = window.setTimeout("refwindow(1)", 12000);
            if (num == 2) {
                //闪烁几下提示用户有信息
                var cdemo = window.setTimeout("window.focus()", 1000);
            }
        }
        var posx = screen.width - 330;
        var posy = screen.height - 300;
        function openwin() {
            divnew.style.display = "";
            //window.open("alert.htm", "", "height=240, width=320, toolbar= no, top=" + posy + ",left=" + posx + ",menubar=no, scrollbars=no, resizable=no, location=no, status=no")
        } 
    </script>
</head>
<body onload="startTimer()">
    <form id="form1" runat="server">
    <div class="JS_haed">
        <div class="JS_haed_n">
            <div class="JS_admin fr">
                <p>
                   日期：<%=System.DateTime.Now.ToLongDateString() %><br />
                   帐号：<span><%=BP.Web.WebUser.No.ToString() %></span> 姓名：<span> <a href="javascript:void(0)" onclick="window.top.main.location.href = './../WF/ToolsSmall.aspx'"><%=BP.Web.WebUser.Name.ToString() %></a></span><br />部门：<span><%=BP.Web.WebUser.FK_DeptName.ToString()%></span>
                </p>
                <!--<button class="dl fr  " type="submit" name="sm1" class="login-btn"><img src="images/tc.jpg" /></button> -->
                <asp:ImageButton ImageUrl="Img/Top/Exit.jpg" CssClass="login-btn" runat="server" ID="imgBtn" OnClick="Unnamed1_Click" />
            </div>
            <div id="divnew" name="divnew" style="display:none"><img alt="有新件收到，请查看待办列表" src="Img/newinfo.gif" onclick="divnew.style.display = 'none'"  /></div>
            <div style="display:none">
            <iframe src="" id="ifrmTop" name="ifrmTop"></iframe>
            </div>
                   <%-- <ul style="margin:0; margin-left:900px; margin-top:16px;">
    <li style="background:url(Img/li1.jpg)  -1px 0px"><a  href="querypage.aspx?faid=1" target="main">附件查询</a></li>
    <li style="background:url(Img/li3.jpg)"><a  href="querypage.aspx?faid=2" target="main">案件查询</a></li>
    <li style="background:url(Img/li4.jpg)"><a  href="reports.aspx?faid=1" target="main">统计报表</a></li>
            </ul>--%>
            <!--
			<ul>
				<li style="background:url(images/li1.jpg)"><a href="#">主　　页</a></li>
				<li style="background:url(images/li_bg.jpg)"><a href="#">我的工作</a></li>
				<li style="background:url(images/li3.jpg)"><a href="#">项目管理</a></li>
				<li style="background:url(images/li4.jpg)"><a href="#">系统管理</a></li>
				<li style="background:url(images/li5.jpg)"><a href="#">通 讯 录</a></li>
			</ul>
	-->

        </div>
    </div>
    <div class="title">
        建设工程管理系统
    </div>
    </form>
</body>
</html>
