using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.Web;
using BP.Port;
using BP.GPM;

public partial class SSO_InfoBar : BP.Web.UC.UCBase
{
    public string FK_Bar
    {
        get
        {
            return this.Request.QueryString["FK_Bar"];
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        switch (this.DoType)
        {
            case "Setting":
                //this.PerAlert();
                this.BindSetting();
                break;
            case "Up":
                BarEmp be = new BarEmp();
                be.MyPK= WebUser.No+"_"+this.FK_Bar ;
                be.Retrieve();
                be.DoUp();
                this.BindSetting();
                break;
            case "Down":
                BarEmp be1 = new BarEmp();
                be1.MyPK = WebUser.No + "_" + this.FK_Bar;
                be1.Retrieve();
                be1.DoDown();
                this.BindSetting();
                break;
            case "Hid":
            case "Show":
                BarEmp be2 = new BarEmp();
                be2.MyPK = WebUser.No + "_" + this.FK_Bar;
                be2.Retrieve();
                be2.DoHidShow();
                this.BindSetting();
                break;
            case "Disktop":
            case "":
            default:
                //this.PerAlert();
                this.Disktop();
                break;
        }
    }

    public void Disktop()
    {
        /*获取到该人员可以显示的infobar. 使用矩阵输出。*/
        #region 开始 [  信息发布 ] 的矩阵输出
        Bars ens = new Bars();
        ens.RetrieveAll();

        int cols = BP.Sys.GloVars.GetValByKeyInt("ColsOfSSO", 3);
        cols = 4;
        BarEmps bes = new BarEmps();
        bes.InitMyBars();

        bes.Retrieve(BarEmpAttr.FK_Emp, WebUser.No, BarEmpAttr.Idx);

        this.Add("<table width='100%' class='Desktop'>");
        int idx = -1;
        bool is1 = false;
        foreach (BarEmp be in bes)
        {
            Bar en = ens.GetEntityByKey(BarAttr.No, be.FK_Bar) as Bar;
            if (en == null)
            {
                be.Delete();
                continue;
            }
            else
            {
                if (be.IsShow == false)
                    continue;
            }

            idx++;
            if (idx == 0)
                is1 = this.AddTR(is1);

            #region 输出bar信息.
            this.AddTDBegin();
            this.Add(@"<h4 style='background:url(Img/back.png) repeat-x;'><span style='float:left' height='38px'><b>"
                + en.Title
                + "</b></span> <span style='float:right; font-size:12px;'><a  style='text-decoration: none' href='"
                + en.MoreUrl + "'>"
                + en.MoreLab + "</a></span></h4>");
            //this.AddBR();

            this.Add(en.Doc);

            this.AddTDEnd();
            #endregion 输出信息.

            if (idx == cols - 1)
            {
                idx = -1;
                this.AddTREnd();
            }
        }

        while (idx != -1)
        {
            idx++;
            if (idx == cols - 1)
            {
                idx = -1;
                this.AddTD();
                this.AddTREnd();
            }
            else
            {
                this.AddTD();
            }
        }
        this.AddTableEnd();

        #endregion 结束  [  信息发布 ]  矩阵输出

    }
    /// <summary>
    /// 桌面
    /// </summary>
    public void DisktopV2()
    {
        /*获取到该人员可以显示的infobar. 使用矩阵输出。*/
        Bars ens = new Bars();
        ens.RetrieveAll();

        int cols = BP.Sys.GloVars.GetValByKeyInt("ColsOfSSO", 3);
        BarEmps bes = new BarEmps();
        bes.InitMyBars();

        bes.Retrieve(BarEmpAttr.FK_Emp, WebUser.No, BarEmpAttr.Idx);
        string js = "<javascript>";
        js += " \t\n CDrag.database.json = [";

        foreach (BarEmp be in bes)
        {
            Bar en = ens.GetEntityByKey(BarAttr.No, be.FK_Bar) as Bar;
            if (en == null)
            {
                be.Delete();
                continue;
            }
            else
            {
                if (be.IsShow == false)
                    continue;
            }
        }

        //  string title="";
        //  js+="\t\n { id: \"m_1_1\", title: \""+be.tit+"\", className: "News", src: "News" },";
        //  js+= @"<h4 style='background:url(Img/back.png) repeat-x;'><span style='float:left' height='38px'><b>" 
        //      + en.Title 
        //      + "</b></span> <span style='float:right; font-size:12px;'><a  style='text-decoration: none' href='" 
        //      + en.MoreUrl + "'>" 
        //      + en.MoreLab + "</a></span></h4>");
        //  this.Add(en.Doc);
        //js += " ]; <javascript>";
    }

    #region 设置消息显示的顺序与位置
    /// <summary>
    /// 设置消息显示的顺序与位置
    /// </summary>
    public void BindSetting()
    {
        //this.AddTable();
        this.Add("<table class='STemSetting'>");
        //this.AddTR();
        this.Add("<tr id='title'>");
        this.AddTDTitle("序号");
        this.AddTDTitle("编号");
        this.AddTDTitle("名称");
        this.AddTDTitle("操作");
        this.AddTDTitle("操作");
        this.AddTDTitle("操作");
        this.AddTREnd();

        Bars ens = new Bars();
        ens.RetrieveAll();

        BarEmps pss = new BarEmps();
        pss.Retrieve(BarEmpAttr.FK_Emp, WebUser.No, BarEmpAttr.Idx);

        int idx = 0;
        foreach (BarEmp be in pss)
        {
            Bar en = ens.GetEntityByKey(BarEmpAttr.No, be.FK_Bar) as Bar;
            idx++;
            this.AddTR();
            this.AddTDIdx(idx);
            this.AddTD(en.No);
            this.AddTD(en.Name);

            this.AddTD("<a href='" + this.PageID + ".aspx?DoType=Up&FK_Bar=" + en.No + "' >上移</a>");
            this.AddTD("<a href='" + this.PageID + ".aspx?DoType=Down&FK_Bar=" + en.No + "' >下移</a>");
            if (en.IsDel == true)
            {
                if (be.IsShow)
                    this.AddTD("<a href='" + this.PageID + ".aspx?DoType=Hid&FK_Bar=" + en.No + "' >隐藏</a>");
                else
                    this.AddTD("<a href='" + this.PageID + ".aspx?DoType=Show&FK_Bar=" + en.No + "' >显示</a>");
            }
            else
            {
                this.AddTD();
            }
            this.AddTREnd();
        }
        this.AddTableEnd();
    }
    #endregion
}