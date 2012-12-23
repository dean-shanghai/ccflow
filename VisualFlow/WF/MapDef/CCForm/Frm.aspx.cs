using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.En;
using BP.Port;
using BP.Web;

public partial class WF_MapDef_FreeFrm_Frm : BP.Web.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Emp emp = new Emp("admin");
            BP.Web.WebUser.SignInOfGener(emp);
        }
        catch
        {
        }
    }
}