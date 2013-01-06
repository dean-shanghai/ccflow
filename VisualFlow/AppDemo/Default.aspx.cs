using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.WF;
using BP.Port;

public partial class AppDemo_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (BP.Web.WebUser.No == null)
        {
            this.Response.Redirect("Login.aspx",true);
            return;
        }
    }
}