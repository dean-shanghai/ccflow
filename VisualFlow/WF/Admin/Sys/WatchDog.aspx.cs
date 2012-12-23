using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.En;
using BP.DA;
using BP.WF;
using BP.Web;
using BP.WF.XML;

public partial class WF_Admin_Sys_WatchDogaspx : WebPage
{
    public string FK_Flow
    {
        get
        {
            string s= this.Request.QueryString["FK_Flow"];
            if (s == "")
                return null;
            return s;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        #region 生成菜单。
        BP.WF.XML.WatchDogXmls xmls = new BP.WF.XML.WatchDogXmls();
        xmls.RetrieveAll();
        this.Top.Add("\t\n<div id='tabsJ'  align='center'>");
        this.Top.Add("\t\n<ul>");
        foreach (WatchDogXml item in xmls)
        {
            if (this.DoType == item.No)
                this.Top.AddLi("<a href='WatchDog.aspx?FK_Flow=" + this.FK_Flow + "&DoType=" + item.No + "' ><span><b>" + item.Name + "</b></span></a>");
            else
                this.Top.AddLi("<a href='WatchDog.aspx?FK_Flow=" + this.FK_Flow + "&DoType=" + item.No + "' ><span>" + item.Name + "</span></a>");
        }
        this.Top.Add("\t\n</ul>");
        this.Top.Add("\t\n</div>");
        #endregion 生成菜单。


        #region 流程树
        //FlowSorts fss = new FlowSorts();
        //fss.RetrieveAll();
        //Flows fls = new Flows();
        //fls.RetrieveAll();

        //this.Left.AddTable();
        //foreach (FlowSort fs in fss)
        //{
        //    this.Left.AddTR();
        //    this.Left.AddTDTitle(fs.Name);
        //    this.Left.AddTREnd();

        //    this.Left.AddTR();
        //    this.Left.AddTDBegin();
        //    this.Left.AddUL();
        //    foreach (Flow fl in fls)
        //    {
        //        if (fl.FK_FlowSort != fs.No)
        //            continue;
        //        if (this.FK_Flow == fl.No)
        //            this.Left.AddLi("WatchDog.aspx?DoType=" + this.DoType + "&FK_Flow=" + fl.No, "<b>" + fl.Name + "</b>");
        //        else
        //            this.Left.AddLi("WatchDog.aspx?DoType=" + this.DoType + "&FK_Flow=" + fl.No, fl.Name);
        //    }
        //    this.Left.AddULEnd();
        //    this.Left.AddTDEnd();
        //    this.Left.AddTREnd();
        //}
        //this.Left.AddTableEnd();
        #endregion

        #region 生成流程数据列
        this.Right.AddTable("width='96%'");
        this.Right.AddTR();
        this.Right.AddTDTitle("IDX");
        this.Right.AddTDTitle("WorkID");
        this.Right.AddTDTitle("流程");
        this.Right.AddTDTitle("停留节点");
        this.Right.AddTDTitle("标题");
        this.Right.AddTDTitle("处理人");
        this.Right.AddTDTitle("接受时间");
        this.Right.AddTDTitle("应完成时间");
        this.Right.AddTDTitle("状态");
        this.Right.AddTDTitle("操作");
        this.Right.AddTREnd();
        string sql = "";
        if (this.FK_Flow != null)
            sql = "SELECT * FROM WF_EmpWorks WHERE FK_Flow='" + this.FK_Flow + "'";
        else
            sql = "SELECT * FROM WF_EmpWorks WHERE 1=1";

        DataTable dt = DBAccess.RunSQLReturnTable(sql);
        int idx = 1;
        DateTime cdt = DateTime.Now;
        string sta = "";
        foreach (DataRow dr in dt.Rows)
        {
            string sdt = dr["SDT"] as string;
            DateTime mysdt = DataType.ParseSysDate2DateTime(dr["SDT"].ToString());
            if (cdt >= mysdt)
                sta = "<font color=red>逾期</font>";
            else
                sta = "正常";

            this.Right.AddTR();
            this.Right.AddTDIdx(idx++);
            this.Right.AddTD(dr["WorkID"].ToString());
            this.Right.AddTD(dr["FlowName"].ToString());
            this.Right.AddTD(dr["NodeName"].ToString());

            this.Right.AddTD("<a href=\"javascript:Rpt('" + dr["WorkID"] + "','" + dr["FK_Flow"] + "','" + dr["FID"] + "');\" >" + dr["Title"] + "</a>");

            this.Right.AddTD(dr["FK_Emp"].ToString());
            this.Right.AddTD(dr["ADT"].ToString());
            this.Right.AddTD(dr["SDT"].ToString());

            this.Right.AddTD(sta);

            this.Right.AddTDBegin();
            this.Right.Add("<a href=\"javascript:DelIt('" + dr["WorkID"] + "','" + dr["FK_Flow"] + "');\" >删除</a>");
            this.Right.Add("-<a href=\"javascript:Track('" + dr["WorkID"] + "','" + dr["FK_Flow"] + "','0');\" >轨迹</a>");
            this.Right.AddTDEnd();
            this.Right.AddTREnd();
        }
        this.Right.AddTableEnd();
        #endregion
    }


}