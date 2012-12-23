using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.WF;
using BP.En;
using BP.DA;
using BP.Sys;

public partial class WF_Test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        Frms frms = new Frms();
        frms.RetrieveAll();

        #region init.

        this.Page.RegisterClientScriptBlock("s",
"<link href='./Style/Frm/Tab.css' rel='stylesheet' type='text/css' />");
        this.Page.RegisterClientScriptBlock("s24",
 "<script language='JavaScript' src='./Style/Frm/jquery.min.js' ></script>");
        this.Page.RegisterClientScriptBlock("sd24j",
"<script language='JavaScript' src='./Style/Frm/jquery.idTabs.min.js' ></script>");
        #endregion 增加

        this.UCEn1.Add("\t\n<div id='usual2' class='usual'>"); //begain.
        this.UCEn1.AddUL();
        foreach (Frm frm in frms)
        {
            this.UCEn1.Add("\t\n<li><a href=\"#" + frm.No + "\" >" + frm.Name + "</a></li>");
        }
        this.UCEn1.AddULEnd();


        foreach (Frm frm in frms)
        {
            this.UCEn1.Add("\t\n<div id='" + frm.No + "'>“" + frm.Name + "”</div>");
        }


        this.UCEn1.Add("\t\n</div>"); // end add.

        this.UCEn1.Add("\t\n<script type='text/javascript'>");
        this.UCEn1.Add("\t\n  $(\"#usual2 ul\").idTabs(\"" + frms[0].GetValByKey("No") + "\");");
        this.UCEn1.Add("\t\n</script>");
    }
}