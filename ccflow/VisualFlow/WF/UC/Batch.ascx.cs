﻿using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.WF;
using BP.Port;
using BP.Sys;
using BP.Web.Controls;
using BP.DA;
using BP.En;
using BP.Web;

public partial class WF_UC_Batch : BP.Web.UC.UCBase3
{
    public int FK_Node
    {
        get
        {
            try
            {
                return int.Parse(this.Request.QueryString["FK_Node"]);
            }
            catch
            {
                return 299;
            }
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        string sql = "SELECT Title,RDT,ADT,SDT,FID,WorkID,Starter FROM WF_EmpWorks WHERE FK_Emp='" + WebUser.No + "'";
        DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
        BP.WF.Node nd = new BP.WF.Node(this.FK_Node);

        this.AddTable("width=80%");
        this.AddCaption("批处理:" + nd.Name);
        this.AddTR();
        this.AddTDTitle("IDX");
        string str1 = "<INPUT id='checkedAll' onclick='SelectAll()' value='选择' type='checkbox' name='checkedAll'>";
        this.AddTDTitle(str1);
        this.AddTDTitle("发起人");
        this.AddTDTitle("发起日期");
        this.AddTDTitle("接受日期");
        this.AddTDTitle("应完成日期");
        this.AddTREnd();
        bool is1 = false;
        int idx = 0;
        foreach (DataRow dr in dt.Rows)
        {
            idx++;
            is1 = this.AddTR(is1);
            this.AddTDIdx(idx);
            CheckBox cb = new CheckBox();
            cb.ID = "CB_" + dr["WorkID"].ToString();
            cb.Text = dr["Title"].ToString();
            this.AddTD(cb);
            this.AddTD(dr["Starter"].ToString());
            this.AddTD(dr["RDT"].ToString());
            this.AddTD(dr["ADT"].ToString());
            this.AddTD(dr["SDT"].ToString());
            this.AddTREnd();
        }
        this.AddTableEndWithHR();

        Button btn = new Button();
        btn.ID = "Btn_Do";
        btn.Text = "批量处理";
        btn.Click += new EventHandler(btn_Click);
        btn.Attributes["onclick"] = " return confirm('您确定要执行吗？');";
        this.Add(btn);

        //btn = new Button();
        //btn.ID = "Btn_Del";
        //btn.Text = "批量删除";
        //btn.Click += new EventHandler(btn_Click);
        //this.Add(btn);
    }

    void btn_Click(object sender, EventArgs e)
    {
        string sql = "SELECT Title,RDT,ADT,SDT,FID,WorkID,Starter FROM WF_EmpWorks WHERE FK_Emp='" + WebUser.No + "'";
        DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(sql);

        string msg = "";
        foreach (DataRow dr in dt.Rows)
        {
            int workid = int.Parse(dr["WorkID"].ToString());
            CheckBox cb = this.GetCBByID("CB_" + workid);
            if (cb.Checked == false)
                return;


            msg += "@对工作(" + dr["Title"] + ")处理情况如下。<br>";
            WorkNode wn = new WorkNode(workid, this.FK_Node);
            msg += wn.AfterNodeSave();
            msg += "<hr>";
        }

        if (msg == "")
        {
            this.Alert("您没有选择工作.");
        }
        else
        {
            this.Clear();
            msg += "<a href='Batch"+BP.WF.Glo.FromPageType+".aspx'>返回...</a>";
            this.AddMsgOfInfo("批量处理信息", msg);
        }
    }
}