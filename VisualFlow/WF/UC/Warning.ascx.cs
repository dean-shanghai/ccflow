using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.WF;
using BP.En;
using BP.DA;
using BP.Web;
using BP.Sys;

public partial class WF_UC_Warning : BP.Web.UC.UCBase3
{
    public string FK_Flow
    {
        get
        {
            return this.Request.QueryString["FK_Flow"];
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Page.Title = "工作预警";
        this.BindLeft();
        string sql = "SELECT Starter,RDT, FlowName, NodeName, ADT,SDT,FK_Emp,WorkID,Title from WF_EmpWorks WHERE FK_Flow='"+this.FK_Flow+"'";
        DataTable dt = DBAccess.RunSQLReturnTable(sql);

        this.Pub2.AddTable();
        this.Pub2.AddCaptionLeft("全部-正常-预警-逾期");
        this.Pub2.AddTR();
        this.Pub2.AddTDTitle("IDX");
        this.Pub2.AddTDTitle("发起人");
        this.Pub2.AddTDTitle("发起日期");
        this.Pub2.AddTDTitle("停留节点");
        this.Pub2.AddTDTitle("应完成日期");
        this.Pub2.AddTDTitle("工作人员");
        this.Pub2.AddTDTitle("标题");
        this.Pub2.AddTDTitle("状态");
        this.Pub2.AddTDTitle("剩余");
        this.Pub2.AddTREnd();

        bool is1 = false;
        int idx = 0;
        foreach (DataRow dr in dt.Rows)
        {
            idx++;
            int day = DataType.GetSpanDays(dr["SDT"].ToString(), DataType.CurrentDataTime);
            is1 = this.Pub2.AddTR(is1);
            this.Pub2.AddTDIdx(idx);
            this.Pub2.AddTD(dr[0].ToString()); //发起人
            this.Pub2.AddTD(dr[1].ToString()); // 发起日期
            this.Pub2.AddTD(dr["NodeName"].ToString()); //停留节点
            this.Pub2.AddTD(dr["SDT"].ToString()); //应完成日期
            this.Pub2.AddTD(dr["FK_Emp"].ToString()); //处理人
            this.Pub2.AddTD("<a href=\"javascript:WinOpen('MyFlowSmallSingle.aspx?FK_Flow=" + this.FK_Flow + "&WorkID=" + dr["WorkID"] + "')\">" + dr["Title"].ToString() + "</A>"); //处理人
            //this.Pub2.AddTD(); //处理人
            this.Pub2.AddTD("正常"); //状态

            this.Pub2.AddTD(day);

           // this.Pub2.AddTD(1); //剩余

            //this.Pub2.AddTD("停留节点");
            //this.Pub2.AddTD("应完成日期");
            //this.Pub2.AddTD("状态");
            //this.Pub2.AddTD("剩余");
            this.Pub2.AddTREnd();
        }
        this.Pub2.AddTableEnd();

    }
    public void BindLeft()
    {
        DataTable dtFlow = DBAccess.RunSQLReturnTable("SELECT FK_Flow, count(FK_Flow) as Num FROM WF_GenerWorkflow GROUP BY FK_Flow");
        this.Pub1.AddFieldSet("流程列表");
        this.Pub1.AddUL();
        foreach (DataRow dr in dtFlow.Rows)
        {
            Flow fl = new Flow(dr["FK_Flow"].ToString());
            if (fl.No==this.FK_Flow)
                this.Pub1.AddLiB("?FK_Flow=" + dr["FK_Flow"] , fl.Name + "(" + dr["Num"]+")");
            else
                this.Pub1.AddLi("<a href=\"?FK_Flow=" + dr["FK_Flow"] + "\" >" + fl.Name + "(" + dr["Num"] + ")</a>");
        }
        this.Pub1.AddULEnd();
        this.Pub1.AddFieldSetEnd();
    }
}