using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.GPM;

public partial class SSO_Pub : BP.Web.UC.UCBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public STems STems
    {
        get
        {
            STems ens = new STems();
            ens.RetrieveAll();
            return ens;
        }
    }
}