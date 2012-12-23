<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FAppSet.aspx.cs" Inherits="WF_Admin_FApp" %>
<%@ Register Src="../../Comm/UC/ucsys.ascx" TagName="ucsys" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
   <link href="../../Comm/Style/Table0.css" rel="stylesheet" type="text/css" />
    <script  language=javascript>
    function Del(mypk, fk_flow, refoid)
    {
        if (window.confirm('您确定要删除吗？') ==false)
            return ;
    
        var url='Do.aspx?DoType=Del&MyPK='+mypk+'&RefOID='+refoid;
        var b=window.showModalDialog( url , 'ass' ,'dialogHeight: 400px; dialogWidth: 600px;center: yes; help: no'); 
        window.location.href = window.location.href;
    }
    </script>
</head>
<body leftMargin=0  topMargin=0>
    <form id="form1" runat="server">
    <div align=center >
                   <uc2:ucsys ID="Ucsys1" runat="server" />
                   </div>
    </form>
</body>
</html>
