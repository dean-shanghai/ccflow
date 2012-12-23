using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BP.DA;
using BP.En;
using BP.Sys;
using BP.WF;
using BP.Web;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //BP.PubClass.WinOpen("http://sina.com.cn");
        //return;
        //DataSet ds =new DataSet();
        //ds.ReadXml(@"D:\ccflow\VisualFlow\Data\FlowDemo\Form\02.企业资质申请表单\01.企业基本情况.xml");
        //MapData.ImpMapData("Demo_Inc01", ds);
        //return;

        if (this.Request.RawUrl.ToLower().Contains("wap"))
        {
            this.Response.Redirect("./WF/WAP/", true);
            return;
        }

        if (this.Request.QueryString["IsCheckUpdate"] == "1")
            this.Response.Redirect("./WF/Admin/XAP/Designer.aspx?IsCheckUpdate=1", true);
        else
            this.Response.Redirect("./WF/Admin/XAP/Designer.aspx", true);
        return;
    }
}
