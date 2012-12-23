using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.DA;
using BP.En;
using BP.WF;
using BP.Web;
public partial class DataUser_Do : System.Web.UI.Page
{
    public string DoType
    {
        get
        {
            return this.Request.QueryString["DoType"];
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            switch (this.DoType)
            {
                case "OutError":
                    throw new Exception("您看到错误信息了吗？");
                case "OutOK":
                    /*在这是里处理您的业务过程。*/
                    return;
                default:
                    break;
            }
        }
        catch(Exception ex)
        {
            OutErrMsg(ex.Message);
        }
    }
    public void OutMsg(string msg)
    {
        this.Response.Write(msg);
    }
    public void OutErrMsg(string msg)
    {
        this.Response.Write("Error:"+msg);
    }
}