using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.WF;
using BP.En;
using BP.DA;
using BP.Web;
using BP.Web.UC;

public partial class WF_Admin_Sys_Do : WebPage
{
    public string FK_Flow
    {
        get
        {
            return this.Request.QueryString["FK_Flow"];
        }
    }
    public Int64 WorkID
    {
        get
        {
            return Int64.Parse(this.Request.QueryString["WorkID"]);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        switch (this.Request.QueryString["DoType"])
        {
            case "DelFlow":
                //调用DoDeleteWorkFlowByReal方法
                WorkFlow wf = new WorkFlow(new Flow(this.FK_Flow), this.WorkID);
                wf.DoDeleteWorkFlowByReal();
                this.WinClose();
                break;
            default:
                break;
        }
    }
}