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
using BP.WF.XML;
using BP.Sys;
using BP.Port;
using BP.Web.Controls;
using BP.DA;
using BP.En;
using BP.Web;

public partial class WF_Admin_Port_Left : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        AdminMenus ens = new AdminMenus();
        ens.RetrieveAll();
        string no = "";
        foreach (AdminMenu en in ens)
        {
            if (en.No.Length == 2)
            {
                if (no == "")
                {
                    this.Pub1.AddFieldSet(en.Name);
                    this.Pub1.AddUL();
                    continue;
                }
                else
                {
                    this.Pub1.AddULEnd();
                    this.Pub1.AddFieldSetEnd();
                    this.Pub1.AddFieldSet(en.Name);
                    this.Pub1.AddUL();
                    continue;
                }
            }
            this.Pub1.AddLi(en.Url, en.Name,"Main");
            no = en.No;
        }
    }
}
