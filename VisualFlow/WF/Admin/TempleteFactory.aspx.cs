using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using BP.WF;
using BP.En;
using BP.Port;
using BP.Web.Controls;
using BP.Web;
using BP.Sys;

public partial class WF_Admin_TempleteFactory : WebPage
{
    #region 属性
    int NodeID
    {
        get
        {
            return int.Parse(this.Request.QueryString["NodeID"]);
        }
    }
    Int64 WorkID
    {
        get
        {
            return Int64.Parse(this.Request.QueryString["WorkID"]);
        }
    }
    #endregion 属性

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}
