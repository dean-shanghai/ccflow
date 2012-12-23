using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BP.WF;
using BP.En;
using BP.Port;
using BP.Web.Controls;
using BP.Web;
using BP.Sys;

public partial class WF_Admin_FApp : WebPage
{
    public int NodeID
    {
        get
        {
            return int.Parse(this.Request.QueryString["NodeID"]);
        }
    }
    public string FK_Flow
    {
        get
        {
            return this.Request.QueryString["FK_Flow"];
        }
    }
    public void DoNew(FAppSet Bill)
    {
        this.Ucsys1.Clear();

        BP.WF.Node nd = new BP.WF.Node(this.NodeID);

        this.Ucsys1.AddTable("width='500px' align=center" );
        this.Ucsys1.AddCaptionLeft("<a href='FAppSet.aspx?FK_Flow=" + this.FK_Flow + "&NodeID=" + this.NodeID + "' >" + nd.Name + "</a> - <img src='../../Images/Btn/New.gif' />" + this.ToE("New", "新建") + " - " + BP.WF.Glo.GenerHelp("FAppSet"));

        this.Ucsys1.AddTR();
        this.Ucsys1.AddTDTitle(this.ToE("Item", "项目"));
        this.Ucsys1.AddTDTitle( this.ToE("Input","采集") );
        this.Ucsys1.AddTDTitle(this.ToE("Desc","描述"));
        this.Ucsys1.AddTREnd();

        this.Ucsys1.AddTR();
        this.Ucsys1.AddTD(this.ToE("InvoType", "调用类型"));

        DDL ddl = new DDL();
        ddl.ID = "DDL_AppType";
        ddl.BindSysEnum("AppType");
        ddl.SetSelectItem(Bill.AppType);

        this.Ucsys1.AddTD(ddl);
        this.Ucsys1.AddTD("");
        this.Ucsys1.AddTREnd();


        this.Ucsys1.AddTR();
        this.Ucsys1.AddTD(this.ToE("Label","标签"));
        TB tb = new TB();
        tb.ID = "TB_Name";
        tb.Text = Bill.Name;
        this.Ucsys1.AddTD(tb);
        this.Ucsys1.AddTD(this.ToE("ShowName","显示名称") ); // 显示名称
        this.Ucsys1.AddTREnd();

        this.Ucsys1.AddTR();
        this.Ucsys1.AddTD(this.ToE("DoWhat","执行内容")); //"执行内容"
        tb = new TB();
        tb.ID = "TB_DoWhat";
        tb.Text = Bill.DoWhat;
        tb.Columns = 55;
        tb.TextMode = TextBoxMode.MultiLine;
        tb.Rows = 4;
        this.Ucsys1.AddTD(tb);
        this.Ucsys1.AddTD("");
        this.Ucsys1.AddTREnd();

        this.Ucsys1.AddTRSum();
        this.Ucsys1.Add("<TD class=TD colspan=3 align=center>");
        Button btn = new Button();
        btn.ID = "Btn_Save";
        btn.CssClass = "Btn";
        btn.Text = " "+this.ToE("Save","保存")+" ";
        this.Ucsys1.Add(btn);
        btn.Click += new EventHandler(btn_Click);
        this.Ucsys1.Add(btn);
        if (Bill.OID > 1)
        {
            btn = new Button();
            btn.ID = "Btn_Del";
            btn.CssClass = "Btn";
            btn.Text = " " + this.ToE("Del", "删除") + " ";
            this.Ucsys1.Add(btn);
            btn.Attributes["onclick"] += " return confirm('" + this.ToE("AYS", "您确认吗？") + "');";
            btn.Click += new EventHandler(btn_Del_Click);
        }
        this.Ucsys1.Add("</TD>");
        this.Ucsys1.AddTREnd();

        string help = "";
        help += "<br>1, 执行内容里面支持用@符号取变量。";
        help += "<br>2, 可获取全局变量@WebUser.No,@WebUser.FK_Dept,@WebUser.SID,@FK_Node";
        help += "<br><b>例如：</b>您要打印当前节点的单据可以如下配置:<br>./WorkOpt/PrintDoc.aspx?FK_Node=202&WorkID=@OID";



        this.Ucsys1.AddTR1();
        this.Ucsys1.AddTDBigDoc("colspan=3 class=BigDoc", "<a href='./Help/index.htm' target=_b><img src='../../Images/Btn/Help.gif' border=0/>" + this.ToE("Help", "帮助") + "</a>"+help);
        this.Ucsys1.AddTREnd();

        //this.Ucsys1.AddTRSum();
        //this.Ucsys1.AddTD("colspan=3", "在当前节点上要执行的外部程序。比如：在此节点点调用sina.");
        //this.Ucsys1.AddTREnd();
        this.Ucsys1.AddTable();
    }

    void btn_Click(object sender, EventArgs e)
    {
        FAppSet bt = new FAppSet();
        if (this.RefOID != 0)
        {
            bt.OID = this.RefOID;
            bt.Retrieve();
        }

        bt = this.Ucsys1.Copy(bt) as FAppSet;
        bt.NodeID = this.NodeID;
        if (this.RefOID == 0)
            bt.Insert();
        else
            bt.Update();
        this.Response.Redirect("FAppSet.aspx?FK_Flow=" + this.FK_Flow + "&NodeID=" + this.NodeID, true);
    }
    void btn_Del_Click(object sender, EventArgs e)
    {
        FAppSet t = new FAppSet();
        t.OID = this.RefOID;
        t.Delete();
        this.Response.Redirect("FAppSet.aspx?FK_Flow=" + this.FK_Flow + "&NodeID=" + this.NodeID, true);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        switch (this.DoType)
        {
            case "New":
                FAppSet bk = new FAppSet();
                bk.NodeID = this.RefOID;
                this.DoNew(bk);
                return;
            case "Edit":
                FAppSet bk1 = new FAppSet(this.RefOID);
                bk1.NodeID = this.NodeID;
                this.DoNew(bk1);
                return;
            default:
                break;
        }

        FAppSets Bills = new FAppSets(this.NodeID);
        if (Bills.Count == 0)
        {
            this.Response.Redirect("FAppSet.aspx?FK_Flow=" + this.FK_Flow + "&NodeID=" + this.NodeID + "&DoType=New", true);
            return;
        }

        if (Bills.Count ==1)
        {
            //this.Response.Redirect("FAppSet.aspx?FK_Flow=" + this.FK_Flow + "&NodeID=" + this.NodeID + "&DoType=Edit&RefOID="+Bills[0].GetValByKey("OID"), true);
           // return;
        }

        BP.WF.Node nd = new BP.WF.Node(this.NodeID);
        this.Title = nd.Name + " - " + this.ToE("FAppSet", "调用外部程序接口");
        this.Ucsys1.AddTable();
        this.Ucsys1.AddCaptionLeft(nd.Name + " - <a href='FAppSet.aspx?FK_Flow=" + this.FK_Flow + "&NodeID=" + this.NodeID + "&DoType=New'><img src='../../Images/Btn/New.gif' border=0/>" + this.ToE("New", "新建") + "</a>  - " + BP.WF.Glo.GenerHelp("FAppSet"));
        this.Ucsys1.AddTR();
        this.Ucsys1.AddTDTitle("ID");
        this.Ucsys1.AddTDTitle( this.ToE("Node","节点") );
        this.Ucsys1.AddTDTitle(this.ToE("ShowName","显示名称"));
        this.Ucsys1.AddTDTitle(this.ToE("InvoType", "调用类型"));
       // this.Ucsys1.AddTDTitle(this.ToE("InvoWhen", "调用时间"));
        this.Ucsys1.AddTDTitle(this.ToE("DoWhat", "执行内容"));
        this.Ucsys1.AddTDTitle(this.ToE("Oper", "操作"));
        this.Ucsys1.AddTREnd();
        int i = 0;
        foreach (FAppSet Bill in Bills)
        {
            i++;
            this.Ucsys1.AddTR();
            this.Ucsys1.AddTDIdx(i);
            this.Ucsys1.AddTD(nd.Name );
            this.Ucsys1.AddTD(Bill.Name);

            this.Ucsys1.AddTD(Bill.AppTypeT);
            this.Ucsys1.AddTD(Bill.DoWhat);

            this.Ucsys1.AddTD("<a href='FAppSet.aspx?FK_Flow=" + this.FK_Flow + "&NodeID=" + this.NodeID + "&DoType=Edit&RefOID=" + Bill.OID + "'><img src='../../Images/Btn/Edit.gif' border=0/>"+this.ToE("Edit","编辑")+"</a>");
            this.Ucsys1.AddTREnd();
        }
        this.Ucsys1.AddTableEnd();
    }


}
