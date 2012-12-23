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
using BP.Port;
using BP.Sys;
using BP.Web.Controls;
using BP.DA;
using BP.En;
using BP.Web;

public partial class WF_UC_CC : BP.Web.UC.UCBase3
{
    public string _PageSamll = null;
    public string PageSmall
    {
        get
        {
            if (_PageSamll == null)
            {
                if (this.PageID.ToLower().Contains("smallsingle"))
                    _PageSamll = "SmallSingle";
                else if (this.PageID.ToLower().Contains("small"))
                    _PageSamll = "Small";
                else
                    _PageSamll = "";
            }
            return _PageSamll;
        }
    }
    public string FK_Flow
    {
        get
        {
            string s = this.Request.QueryString["FK_Flow"];
            if (s == null)
                return this.ViewState["FK_Flow"] as string;
            return s;
        }
        set
        {
            this.ViewState["FK_Flow"] = value;
        }
    }
    public string GroupBy
    {
        get
        {
            string s = this.Request.QueryString["GroupBy"];
            if (s == null)
            {
                if (this.DoType == "CC")
                    s = "Rec";
                else
                    s = "FlowName";
            }
            return s;
        }
    }
    public string Sta
    {
        get
        {
            string s= this.Request["Sta"];
            if (s == null)
                s = "0";
            return s;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        switch (this.Sta)
        {
            case "-1":
                this.Bind(BP.WF.Dev2Interface.DB_CCList(WebUser.No));
                break;
            case "0":
                this.Bind(BP.WF.Dev2Interface.DB_CCList_UnRead(WebUser.No));
                break;
            case "1":
                this.Bind(BP.WF.Dev2Interface.DB_CCList_Read(WebUser.No));
                break;
            case "2":
            default:
                this.Bind(BP.WF.Dev2Interface.DB_CCList_Delete(WebUser.No));
                break;
        }
    }
    public string GenerMenu()
    {
        string msg = "<a href='" + this.PageID + ".aspx?Sta=-1&FK_Flow=" + this.FK_Flow + "' >全部</a> - <a href='" + this.PageID + ".aspx?Sta=0&FK_Flow=" + this.FK_Flow + "' >未读</a> - <a href='" + this.PageID + ".aspx?Sta=1&FK_Flow=" + this.FK_Flow + "' >已读</a> - <a href='" + this.PageID + ".aspx?Sta=2&FK_Flow=" + this.FK_Flow + "' >删除</a>";
        return msg;
    }
    /// <summary>
    /// 绑定
    /// </summary>
    public void Bind(DataTable dt)
    {
        string appPath = this.Request.ApplicationPath;
        string groupVals = "";
        foreach (DataRow dr in dt.Rows)
        {
            if (groupVals.Contains("@" + dr[this.GroupBy].ToString() + ","))
                continue;
            groupVals += "@" + dr[this.GroupBy].ToString() + ",";
        }

        if (this.PageSmall != "")
            this.Pub1.AddBR();
        int colspan = 9;
        this.Pub1.AddTable("width='960px' align=center");
        this.Pub1.AddCaptionLeft("<img src='" + appPath + "/WF/Img/CCSta/CC.gif' >&nbsp;" + this.GenerMenu());
        this.Pub1.AddTR();
        this.Pub1.AddTDTitle("ID");
        this.Pub1.AddTDTitle("流程标题");
        this.Pub1.AddTDTitle("内容");

        if (this.GroupBy != "FlowName")
            this.Pub1.AddTDTitle("<a href='" + this.PageID + ".aspx?GroupBy=FlowName&DoType=CC&Sta=" + this.Sta + "&FK_Flow="+this.FK_Flow+"' >" + this.ToE("Flow", "流程") + "</a>");

        if (this.GroupBy != "NodeName")
            this.Pub1.AddTDTitle("<a href='" + this.PageID + ".aspx?GroupBy=NodeName&DoType=CC&Sta=" + this.Sta + "&FK_Flow=" + this.FK_Flow + "' >" + this.ToE("NodeName", "节点") + "</a>");

        if (this.GroupBy != "Rec")
            this.Pub1.AddTDTitle("<a href='" + this.PageID + ".aspx?GroupBy=Rec&DoType=CC&Sta=" + this.Sta + "&FK_Flow=" + this.FK_Flow + "' >抄送人</a>");


        if (this.Sta == "1")
            this.Pub1.AddTDTitle("删除");
        this.Pub1.AddTREnd();

        int i = 0;
        bool is1 = false;
        DateTime cdt = DateTime.Now;
        string[] gVals = groupVals.Split('@');
        int gIdx = 0;
        foreach (string g in gVals)
        {
            if (string.IsNullOrEmpty(g))
                continue;

            gIdx++;
            this.Pub1.AddTR();
            this.Pub1.AddTD("colspan=" + colspan + " class=Sum onclick=\"GroupBarClick('"+appPath+"','" + gIdx + "')\" ", "<div style='text-align:left; float:left' ><img src='"+appPath+"/WF/Style/Min.gif' alert='Min' id='Img" + gIdx + "'   border=0 />&nbsp;<b>" + g.Replace(",", "") + "</b>");
            this.Pub1.AddTREnd();
            foreach (DataRow dr in dt.Rows)
            {
                if (dr[this.GroupBy].ToString() + "," != g)
                    continue;
                this.Pub1.AddTR("ID='" + gIdx + "_" + i + "'");
                i++;
                int Sta = (int)dr["Sta"];

                this.Pub1.AddTDIdx(i);
                if (Sta == 0)
                    this.Pub1.AddTDB("Class=TTD onclick=\"SetImg('" + appPath + "','" + dr["MyPK"] + "')\"", "<a href=\"javascript:WinOpen('"+appPath+"','" + dr["MyPK"] + "','" + dr["FK_Flow"] + "','" + dr["FK_Node"] + "','" + dr["RefWorkID"] + "','" + dr["FID"] + "','" + dr["Sta"] + "');\" ><img src='" + appPath + "/WF/Img/CCSta/0.png' id='I" + dr["MyPK"] + "' class=Icon >" + dr["Title"] + "</a><br>日期:" + dr["RDT"].ToString().Substring(5));
                else
                    this.Pub1.AddTD("Class=TTD", "<a href=\"javascript:WinOpen('"+appPath+"','" + dr["MyPK"] + "','" + dr["FK_Flow"] + "','" + dr["FK_Node"] + "','" + dr["RefWorkID"] + "','" + dr["FID"] + "','" + dr["Sta"] + "');\" ><img src='" + appPath + "/WF/Img/CCSta/" + dr["Sta"] + ".png' class=Icon >" + dr["Title"] + "</a><br>日期:" + dr["RDT"].ToString().Substring(5));

                this.Pub1.AddTDBigDoc(DataType.ParseText2Html(dr["Doc"].ToString()));

                if (this.GroupBy != "FlowName")
                {
                    if (Sta == 0)
                        this.Pub1.AddTDB(dr["FlowName"].ToString());
                    else
                        this.Pub1.AddTD(dr["FlowName"].ToString());
                }

                if (this.GroupBy != "NodeName")
                {
                    if (Sta == 0)
                        this.Pub1.AddTDB(dr["NodeName"].ToString());
                    else
                        this.Pub1.AddTD(dr["NodeName"].ToString());
                }

                if (this.GroupBy != "Rec")
                    this.Pub1.AddTD(dr["Rec"].ToString());

                if (this.Sta == "1")
                    this.Pub1.AddTD("<a href=\"javascript:DoDelCC('" + dr["MyPK"] + "');\"><img src='" + this.Request.ApplicationPath + "/Images/Btn/Delete.gif' /></a>");
                this.Pub1.AddTREnd();
            }
        }
        this.Pub1.AddTRSum();
        this.Pub1.AddTD("colspan=" + colspan, "&nbsp;");
        this.Pub1.AddTREnd();
        this.Pub1.AddTableEnd();
    }
}
