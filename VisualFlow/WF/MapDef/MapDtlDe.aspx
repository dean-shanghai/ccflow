<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MapDtlDe.aspx.cs" Inherits="Comm_MapDef_MapDtlDe" %>
<%@ Register Assembly="BP.Web.Controls" Namespace="BP.Web.Controls" TagPrefix="cc1" %>
<%@ Register src="Pub.ascx" tagname="Pub" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>表单设计</title>
	<script language="JavaScript" src="../../Comm/JScript.js" ></script>
    <script language="JavaScript" src="./../../Comm/JS/Calendar/WdatePicker.js"  defer="defer" ></script>
	<base target="_self" />
	<script language="javascript">
	function Insert(mypk,IDX)
    {
        var url='Do.aspx?DoType=AddF&MyPK='+mypk+'&IDX=' +IDX ;
        var b=window.showModalDialog( url , 'ass' ,'dialogHeight: 400px; dialogWidth: 600px;center: yes; help: no'); 
        window.location.href = window.location.href;
    }
	function AddF(mypk)
    {
        var url='Do.aspx?DoType=AddF&MyPK='+mypk;
        var b=window.showModalDialog( url , 'ass' ,'dialogHeight: 500px; dialogWidth: 600px;center: yes; help: no'); 
        window.location.href = window.location.href;
    }
    function AddFGroup(mypk) {
        var url = 'Do.aspx?DoType=AddFGroup&FK_MapData=' + mypk;
        var b = window.showModalDialog(url, 'ass', 'dialogHeight: 500px; dialogWidth: 600px;center: yes; help: no');
        window.location.href = window.location.href;
    }
    function CopyF(mypk) {
        var url = 'CopyDtlField.aspx?MyPK=' + mypk;
        var b = window.showModalDialog(url, 'ass', 'dialogHeight: 600px; dialogWidth: 800px;center: yes; help: no');
        window.location.href = window.location.href;
    }

    function HidAttr(mypk) {
        var url = 'HidAttr.aspx?FK_MapData=' + mypk;
        var b = window.showModalDialog(url, 'ass', 'dialogHeight: 600px; dialogWidth: 800px;center: yes; help: no');
        //  window.location.href = window.location.href;
    }
    function Edit(mypk,refNo, ftype)
    {
        var url='EditF.aspx?DoType=Edit&MyPK='+mypk+'&RefNo='+refNo +'&FType=' + ftype;
        var b=window.showModalDialog( url , 'ass' ,'dialogHeight: 500px; dialogWidth: 600px;center: yes; help: no'); 
        window.location.href = window.location.href;
    }
    function EditEnum(mypk,refNo)
    {
        var url='EditEnum.aspx?DoType=Edit&MyPK='+mypk+'&RefNo='+refNo;
        var b=window.showModalDialog( url , 'ass' ,'dialogHeight: 500px; dialogWidth: 600px;center: yes; help: no'); 
        window.location.href = window.location.href;
    }
    function EditTable(mypk, refno) {
        var url = 'EditTable.aspx?DoType=Edit&MyPK=' + mypk + '&RefNo=' + refno;
        var b = window.showModalDialog(url, 'ass', 'dialogHeight: 500px; dialogWidth: 600px;center: yes; help: no');
        window.location.href = window.location.href;
    }

    function Up(mypk, refNo) {
        var url = 'Do.aspx?DoType=Up&MyPK=' + mypk + '&RefNo=' + refNo + "&IsDtl=1";
        var b = window.showModalDialog(url, 'ass', 'dialogHeight: 400px; dialogWidth: 600px;center: yes; help: no');
        //window.location.href ='MapDef.aspx?PK='+mypk+'&IsOpen=1';
        window.location.href = window.location.href;
    }
    function Down(mypk, refNo) {
        var url = 'Do.aspx?DoType=Down&MyPK=' + mypk + '&RefNo=' + refNo + "&IsDtl=1";
        var b = window.showModalDialog(url, 'ass', 'dialogHeight: 400px; dialogWidth: 600px;center: yes; help: no');
        window.location.href = window.location.href;
    }
    function Del(mypk, refNo) {
        if (window.confirm('您确定要删除吗？') == false)
            return;

        var url = 'Do.aspx?DoType=Del&MyPK=' + mypk + '&RefNo=' + refNo;
        var b = window.showModalDialog(url, 'ass', 'dialogHeight: 400px; dialogWidth: 600px;center: yes; help: no');
        window.location.href = window.location.href;
    }
    function DtlMTR(MyPK) {
        var url = 'MapDtlMTR.aspx?MyPK=' + MyPK;
        var b = window.showModalDialog(url, 'ass', 'dialogHeight: 350px; dialogWidth: 550px;center: yes; help: no');
        window.location.href = window.location.href;
    }
    function Esc() {
        if (event.keyCode == 27)
            window.close();
        return true;
    }
    function Attachment(fk_mapdtl) {
        window.showModalDialog('Attachment.aspx?IsBTitle=1&PKVal=0&FK_MapData=' + fk_mapdtl + '&FK_FrmAttachment=' + fk_mapdtl + '_AthM&Ath=AthM');
    }

    function MapM2M(fk_mapdtl) {
        window.showModalDialog('MapM2M.aspx?NoOfObj=M2M&PKVal=0&FK_MapData=' + fk_mapdtl + '&FK_FrmAttachment=' + fk_mapdtl + '_AthM&Ath=AthM');
    }

   // this.Pub1.AddTDTitle("<a href=\"javascript:window.showModalDialog('MapM2M.aspx?NoOfObj=M2M&FK_MapData=" + this.FK_MapDtl + "',)\"><img src='./../Img/set.gif' border=0 width='16px' /></a>");

	</script>
  <script language="javascript" for="document" event="onkeydown">
//    if(event.keyCode==13)
//       event.keyCode=9;
    </script>
    <link href="../../Comm/Style/Table0.css" rel="stylesheet" type="text/css" />
</head>
<body topmargin="0" leftmargin="0" onkeypress="Esc()"  style="padding:0px"   bgcolor="white" >
    <form id="form1" runat="server">
     <uc1:Pub ID="Pub1" runat="server" />
     </form>
</body>
</html>
