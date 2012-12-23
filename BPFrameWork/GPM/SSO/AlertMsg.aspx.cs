using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.GPM;

public partial class SSO_AlertMsg : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    public PerAlerts PerAlerts
    {
        get
        {
            BP.GPM.PerAlerts pls = new PerAlerts();
            pls.RetrieveAll();
            return pls;
        }
    }
    public int GetNum(PerAlert pa)
    {
        int num = BP.DA.DBAccess.RunSQLReturnValInt(pa.GetSQL);
        return num;
    }
}