<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="AppDemo_Default" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title><%=BP.SystemConfig.SysName %></title>
     <link href="css/basic.css" rel="stylesheet" type="text/css" />
<link href="css/base.css" rel="stylesheet" type="text/css" />
</head>
 
<frameset rows="121,*,20" frameborder="NO" border="0" framespacing="0">

        <frame src="Top.aspx" noresize="noresize" frameborder="NO" name="topFrame" scrolling="no" marginwidth="0" marginheight="0" target="main"></frame>

        <frameset cols="180,*" frameborder="NO" border="0" framespacing="0">
            <frame src="Left.aspx" name="leftFrame" noresize="noresize" marginwidth="0" marginheight="0" frameborder="0" scrolling="no" target="main" ></frame>
            <frame src="../WF/StartSmall.aspx" name="main" marginwidth="0" marginheight="0" frameborder="0" scrolling="auto" target="_self" ></frame>
        </frameset>
    
    <frame  src="Bottom.aspx" noresize="noresize" frameborder="NO" name="btmFrame" scrolling="no" marginwidth="0" marginheight="0" target="_self"> 
        
    </frame>
    </frameset>
    <noframeset>

    </noframeset>  
<body >
     
</body>    
</html>
