<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestFlow.aspx.cs" Inherits="WF_Admin_TestFlow" %>
<%@ Register Src="../../Comm/UC/ucsys.ascx" TagName="ucsys" TagPrefix="uc2" %>
<%@ Register src="Pub.ascx" tagname="Pub" tagprefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <script  language=javascript>
    function Del(mypk, fk_flow, refoid)
    {
        if (window.confirm('Are you sure?') ==false)
            return ;
    
        var url='Do.aspx?DoType=Del&MyPK='+mypk+'&RefOID='+refoid;
        var b=window.showModalDialog( url , 'ass' ,'dialogHeight: 400px; dialogWidth: 600px;center: yes; help: no'); 
        window.location.href = window.location.href;
    }
    function WinOpen(url)
    {
        var b = window.open(url, 'ass', 'width=700,top=50,left=50,height=500,scrollbars=yes,resizable=yes,toolbar=false,location=false');
        b.focus();
       // var b=window.open( url , 'ass' ,'Height: 600px; dialogWidth: 700px;center: yes;'); 
       // var b=window.showModalDialog( url , 'ass' ,'dialogHeight: 600px; dialogWidth: 700px;center: yes; help: no'); 
        //window.location.href = window.location.href;
    }
    function WinOpen(url, w, h, name) {
        var b = window.open(url, name, 'width=' + w + ',height=' + h + ',scrollbars=yes,resizable=yes,toolbar=false,location=false,center: yes');
        // var b=window.open( url , 'ass' ,'Height: 600px; dialogWidth: 700px;center: yes;'); 
        // var b=window.showModalDialog( url , 'ass' ,'dialogHeight: 600px; dialogWidth: 700px;center: yes; help: no'); 
        //window.location.href = window.location.href;
    }
    function WinOpenWAP_Cross(url)
    {
        var b=window.open( url , 'ass' ,'width=50,top=50,left=50,height=20,scrollbars=yes,resizable=yes,toolbar=false,location=false'); 
       // var b=window.open( url , 'ass' ,'Height: 600px; dialogWidth: 700px;center: yes;'); 
       // var b=window.showModalDialog( url , 'ass' ,'dialogHeight: 600px; dialogWidth: 700px;center: yes; help: no'); 
        //window.location.href = window.location.href;
    }
    </script>
    <link href="./../../Comm/Style/Table0.css" rel="stylesheet" type="text/css" />
    </head>
<body    leftMargin=0  topMargin=0 bgcolor=white >
    <form id="form1" runat="server">
                   <uc2:ucsys ID="Ucsys1" runat="server" />

      <%--  <table border="0" cellpadding="0" align=center cellspacing="0"  bgcolor=white width='90%'  class=Table >
           <tr>
                <td  colspan=2    bgcolor="Silver"   >
                
                <!-- 
                <b>流程测试设计 - 感谢您选择驰骋工作流引擎</b> <a href='http://ccFlow.org:666/BugFree/'>Bug Report</a> - <a href='./../Login.aspx' > <% this.ToE("Login", "登陆"); %>   </a>
                -->
                    <uc3:Pub ID="Pub1" runat="server" />
                    <a href='./../Login.aspx' > 登陆  </a>
                    </td>
</tr>        
            <tr>
                <td valign=top align=left  align=left height=200%>
                   <uc2:ucsys ID="Left" runat="server" />
                </td>
                  <td valign=top  align=left>--%>
              <%--  </td>
            </tr>
            
            
             <tr>
                <td  colspan=2    bgcolor="Silver"  align=center  >

             CopyRight@2003-2010 chichengsoft.
                
                    </td>
</tr>        

        </table>--%>
    </form>
</body>
</html>
