using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WF_OneFlow_MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Page.RegisterClientScriptBlock("s",
           "<link href='" + this.Request.ApplicationPath + "/Comm/Style/Table" + BP.Web.WebUser.Style + ".css' rel='stylesheet' type='text/css' />");

        this.Page.RegisterClientScriptBlock("s",
           "<link href='" + this.Request.ApplicationPath + "/Comm/Style/Table.css' rel='stylesheet' type='text/css' />");

        this.Page.RegisterClientScriptBlock("s",
          "<link href='" + this.Request.ApplicationPath + "/Comm/JS/Calendar/WdatePicker.js'   defer='defer' type='text/javascript' />");

        this.Page.RegisterClientScriptBlock("s",
         "<link href='" + this.Request.ApplicationPath + "/Comm/JScript.js'  defer='defer' type='text/javascript' />");

        this.Page.RegisterClientScriptBlock("s2",
         "<link href='" + this.Request.ApplicationPath + "/WF/Scripts/MapExt.js'  defer='defer' type='text/javascript' />");

        this.Page.RegisterClientScriptBlock("ss2",
     "<link href='" + this.Request.ApplicationPath + "/WF/Scripts/jquery-1.4.1.min.js'  defer='defer' type='text/javascript' />");



    }
}
