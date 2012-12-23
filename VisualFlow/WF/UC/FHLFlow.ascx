<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FHLFlow.ascx.cs" Inherits="WF_UC_FHLFlow" %>
<%@ Register src="../../Comm/UC/ToolBar.ascx" tagname="ToolBar" tagprefix="uc1" %>
<%@ Register src="UCEn.ascx" tagname="UCEn" tagprefix="uc2" %>
<script  type="text/javascript">
    function ReinitIframe(frmID, tdID) {
        try {

            var iframe = document.getElementById(frmID);
            var tdF = document.getElementById(tdID);
            iframe.height = iframe.contentWindow.document.body.scrollHeight;
            iframe.width = iframe.contentWindow.document.body.scrollWidth;
            if (tdF.width < iframe.width) {
                tdF.width = iframe.width;
            } else {
                iframe.width = tdF.width;
            }
            tdF.height = iframe.height;
            return;
        } catch (ex) {
            return;
        }
        return;
    }
    function GroupBarClick(rowIdx) {
        var alt = document.getElementById('Img' + rowIdx).alert;
        var sta = 'block';
        if (alt == 'Max') {
            sta = 'block';
            alt = 'Min';
        } else {
            sta = 'none';
            alt = 'Max';
        }
        document.getElementById('Img' + rowIdx).src = './Img/' + alt + '.gif';
        document.getElementById('Img' + rowIdx).alert = alt;
        var i = 0
        for (i = 0; i <= 40; i++) {
            if (document.getElementById(rowIdx + '_' + i) == null)
                continue;
            document.getElementById(rowIdx + '_' + i).style.display = sta;
        }
    }
</script>
<script language=javascript>
    function Do(warning, url) {
        if (window.confirm(warning) == false)
            return;
        window.location.href = url;
        // WinOpen(url);
    }
</script>

<table border=0 width='80%'  >
<tr>
<td align=left >
    <uc1:ToolBar ID="ToolBar1" runat="server" />
    </td>
</tr>
<tr>
<td align=left >
    <uc2:UCEn ID="UCEn1" runat="server" />
    </td>
</tr>
</table>
