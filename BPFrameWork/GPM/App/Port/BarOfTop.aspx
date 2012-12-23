<%@ Reference Control="~/comm/uc/ucsys.ascx" %>
<%@ Register TagPrefix="uc1" TagName="UCSys" Src="../../Comm/UC/UCSys.ascx" %>

<%@ Page Language="c#" Inherits="BP.Web.KM.Bar" CodeFile="BarOfTop.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Bar</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="../../Comm/Style.css" type="text/css" rel="stylesheet">
    <base target="_blank" />
    <script language="JavaScript"></script>
    <style>
        .navPoint
        {
            font-size: 7pt;
            cursor: hand;
            font-family: Webdings;
        }
        P
        {
            font-size: 7pt;
        }
    </style>
    <script language="javascript">
        function TDClick(webAppPath, file) {
            var url = webAppPath + "/Comm/port/LeftXml.aspx?xml=" + file;
            var myParent, i;
            myParent = window.parent;
            if (myParent == null)
                return;
            if (myParent.frames == null)
                return;
            // alert(url);
            for (i = 0; i < myParent.frames.length; i++) {

                if (myParent.frames(i).name == 'left') {

                    window.parent.frames(i).location.href = url;
                }
            }
        }
        function WinOpen(url, winName) {
            newWindow = window.open(url, winName, 'width=600,top=200,left=150,height=400,scrollbars=yes,resizable=yes,toolbar=false,location=false');
            newWindow.focus();
        }

        //true:当前处于扩展状态，灰色；false:当前处于缩小状态，蓝色
        //当窗体加载时，应为缩小状态

        var g_Extended = true;

        //记录Frame的大小。
        //var Top_Rows='75,*';
        var Top_Rows = '71,*';
        //var Bottom_Cols='198,*';
        var Bottom_Cols = '192,*';

        function shrinkForm() {
            window.parent.Top.rows = Top_Rows;
            window.parent.Bottom.cols = Bottom_Cols;
            switchUpPoint.innerText = 5
            switchLeftPoint.innerText = 3
        }

        function ExtendForm() {

            //此段代码无法实现预定的功能。当窗口的帧的大小改变时，
            //帧的rows和cols属性不会马上改变。
            if (window.parent.Top.rows != '0,*') Top_Rows = window.parent.Top.rows;
            if (window.parent.Bottom.cols != '0,*') Bottom_Cols = window.parent.Bottom.cols;
            switchUpPoint.innerText = 6
            switchLeftPoint.innerText = 4

            window.parent.Top.rows = '0,*';
            window.parent.Bottom.cols = '0,*';

        }

        function switchUpBar() {
            if (switchUpPoint.innerText == 5) {
                switchUpPoint.innerText = 6
                Top_Rows = window.parent.Top.rows;
                window.parent.Top.rows = '0,*';
            }
            else {
                window.parent.Top.rows = Top_Rows;
                switchUpPoint.innerText = 5
            }
            if (window.parent.Bottom.cols == '0,*' && window.parent.Top.rows == '0,*') {
                document.all("Tip").innerText = "标准化";
                document.all("imgsrc").src = "max.gif";
            }
            else {
                document.all("Tip").innerText = "最大化";
                document.all("imgsrc").src = "mix.gif";
            }
        }

        function switchLeftBar() {
            if (switchLeftPoint.innerText == 3) {
                switchLeftPoint.innerText = 4
                Bottom_Cols = window.parent.Bottom.cols;

                window.parent.Bottom.cols = '0,*';
            }
            else {
                window.parent.Bottom.cols = Bottom_Cols;
                switchLeftPoint.innerText = 3
            }

            if (window.parent.Bottom.cols == '0,*' && window.parent.Top.rows == '0,*') {
                document.all("Tip").innerText = "标准化"
                document.all("imgsrc").src = "max.gif";
            }
            else {
                document.all("Tip").innerText = "最大化"
                document.all("imgsrc").src = "mix.gif";
            }

        }
        function ShiftStatus() {
            //当窗体加载时，应为缩小状态
            //当前状态为扩展状态，要切换到缩小状态。
            if (g_Extended == true) {
                document.all("Tip").innerText = "最大化"
                document.all("imgsrc").src = "mix.gif";
                shrinkForm()
            }
            else {
                document.all("Tip").innerText = "标准化"
                document.all("imgsrc").src = "max.gif";
                ExtendForm();
            }

            g_Extended = !g_Extended;

        }

    </script>
    <link href="../../Comm/CSS/Link.css" type="text/css" rel="stylesheet" />
</head>
<body bgcolor="#99cccc" topmargin="0" leftmargin="0" onload="ShiftStatus();" ondblclick="ShiftStatus();"
    oncontextmenu="return false;">
    <form id="Form1" method="post" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0" height="100%" background="bagtop.jpg">
        <tr>
            <!--td height="20" width="90%" valign="top" id=iMessage><IMG height=20 src="index_04.gif" width=31><FONT color=yellow valign='left'>21212121</FONT></td-->
            <td height="30" width="30%" nowrap id="iMessage">
                <font face="宋体">
                    <uc1:UCSys ID="UCSys1" runat="server"></uc1:UCSys>
                </font>
            </td>
            <td height="30" width="10%" valign="middle">
                <table width="100" border="1" cellspacing="0" cellpadding="1" bgcolor="#cccccc" bordercolordark="#fffff0"
                    bordercolorlight="#000066">
                    <tr>
                        <td id="statusLeft" height="16" onclick="switchLeftBar();" class="HandCss" valign="middle">
                            <span class="navPoint" id="switchLeftPoint" title="点击"><font color="#000000">3 </font>
                            </span>
                        </td>
                        <td id="statusUp" height="16" onclick="switchUpBar();" class="HandCss">
                            <span class="navPoint" id="switchUpPoint" title="点击"><font color="#000000">5</font></span>
                        </td>
                        <td id="status" height="16" onclick="ShiftStatus();" class="HandCss">
                            <img id="imgsrc" height="9" src="mix.gif" width="8">
                            <font color="#000000" size="2" id="Tip">最大化</font>
                        </td>
                    </tr>
                </table>
                <div align="right">
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
