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
using BP.Web.Controls;

public partial class WF_Admin_TitleSet : BP.Web.WebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.AdminSet();
    }
    public void AdminSet()
    {
        this.Pub1.AddFieldSet(this.ToE("Setting", "设置"));

        this.Pub1.AddTable();
        //this.Pub1.AddTR();
        //this.Pub1.AddTDTitle( this.ToE("Item", "项目") );
        //this.Pub1.AddTDTitle("项目值");
        //this.Pub1.AddTDTitle("描述");
        //this.Pub1.AddTREnd();

        this.Pub1.AddTR();
        this.Pub1.AddTD( this.ToE("TitleImg", "标题图片"));
        FileUpload fu = new FileUpload();
        fu.ID = "F";
        this.Pub1.AddTD(fu);
        this.Pub1.AddTD();
       // this.Pub1.AddTDBigDoc("请您自己调整好图片大小，然后把它上传上去。在系统设置里可以控制标题图片是否显示。");
       // this.Pub1.AddTDBigDoc("请您自己调整好图片大小，然后把它上传上去。在系统设置里可以控制标题图片是否显示。");
        this.Pub1.AddTREnd();

        this.Pub1.AddTR();
        this.Pub1.AddTD("FTP server URL");
        TextBox tb = new TextBox();
        tb.Width = 200;
        tb.ID = "TB_FtpUrl";
        this.Pub1.AddTD(tb);
        this.Pub1.AddTD();
        this.Pub1.AddTREnd();


        this.Pub1.AddTR();
        this.Pub1.AddTD("");

        Btn btn = new Btn();
        btn.Text = "Save";
        btn.Click += new EventHandler(btn_AdminSet_Click);
        this.Pub1.AddTD(btn);
        this.Pub1.AddTD();
        this.Pub1.AddTREnd();

        this.Pub1.AddTR();
        this.Pub1.AddTD();
        this.Pub1.AddTD("<a href='../../OA/FtpSet.aspx'  >FTP Server</a>-<a href='../../Comm/Ens.aspx?EnsName=BP.OA.Links' >Link</a>");
        this.Pub1.AddTD();
        this.Pub1.AddTD();
        this.Pub1.AddTREnd();
        this.Pub1.AddTableEnd();
        this.Pub1.AddFieldSetEnd();
    }

    void btn_AdminSet_Click(object sender, EventArgs e)
    {
        FileUpload f = (FileUpload)this.Pub1.FindControl("F");
        throw new Exception("@ddddddd");

        //if (f.HasFile == false)
        //    return;
        //f.SaveAs(BP.SystemConfig.PathOfWebApp + "/DataUser/Title.gif");

        this.Response.Redirect(this.Request.RawUrl, true);
    }
}
