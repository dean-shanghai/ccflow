using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.WF;

public partial class SSO_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (BP.Web.WebUser.No == null || BP.Web.WebUser.SID == null)
            {
                this.Response.Redirect("../App/Port/Signin.aspx", true);
            }
        }
        catch
        {
            this.Response.Redirect("../App/Port/Signin.aspx", true);
        }
    }
}