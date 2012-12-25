<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Runing.ascx.cs" Inherits="WF_UC_Runing" %>
<%@ Register src="Pub.ascx" tagname="Pub" tagprefix="uc1" %>
<%@ Register src="../../Comm/UC/ToolBar.ascx" tagname="ToolBar" tagprefix="uc2" %>
    <uc1:Pub ID="Pub1" runat="server" />
    <script   type="text/javascript">
        // 撤销。
        function UnSend(appPath, pageID, fid, workid, fk_flow) {
            if (window.confirm('您确定要撤销本次发送吗？') == false)
                return;

            var url = appPath + 'WF/Do.aspx?DoType=UnSend&FID=' + fid + '&WorkID=' + workid + '&FK_Flow=' + fk_flow;
            window.location.href = url;
            return;

//            var url = appPath + '/WF/Do.aspx?DoType=UnSend&FID=' + fid + '&WorkID=' + workid + '&FK_Flow=' + fk_flow;
//            var myVal = window.showModalDialog(url, 'sd', 'dialogHeight: 20px; dialogWidth: 35px;center: yes; help: no');
//            if (myVal == null)
//                return;
//            window.location.href = window.location.href;
        }
        function Press(appPath, fid, workid, fk_flow) {
            var url = appPath+'/WF/WorkOpt/Press.aspx?FID=' + fid + '&WorkID=' + workid + '&FK_Flow=' + fk_flow;
            var v = window.showModalDialog(url, 'sd', 'dialogHeight: 200px; dialogWidth: 350px;center: yes; help: no');
        }

        function GroupBarClick(appPath,rowIdx) {
            var alt = document.getElementById('Img' + rowIdx).alert;
            var sta = 'block';
            if (alt == 'Max') {
                sta = 'block';
                alt = 'Min';
            } else {
                sta = 'none';
                alt = 'Max';
            }
            
            document.getElementById('Img' + rowIdx).src = appPath+'/WF/Img/' + alt + '.gif';
            document.getElementById('Img' + rowIdx).alert = alt;
            var i = 0;
            for (i = 0; i <= 5000; i++) {
                if (document.getElementById(rowIdx + '_' + i) == null)
                    continue;
                document.getElementById(rowIdx + '_' + i).style.display = sta;
            }
        }
    </script>
     <style>
        .TTD
        {
          word-wrap: break-word; 
      　　word-break: normal; 
        }
        .Icon
{
    width:16px;
    height:16px;
}
     </style>