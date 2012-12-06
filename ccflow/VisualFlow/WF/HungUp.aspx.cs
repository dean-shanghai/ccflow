﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.WF;
using BP.DA;
using BP.Web;

public partial class WF_HungUp : BP.Web.WebPage
{
    public string FK_Flow
    {
        get
        {
            return this.Request.QueryString["FK_Flow"];
        }
    }
    public int FK_Node
    {
        get
        {
            return int.Parse(this.Request.QueryString["FK_Node"]);
        }
    }
    public Int64 WorkID
    {
        get
        {
            return Int64.Parse(this.Request.QueryString["WorkID"]);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        HungUp hu = new HungUp();
        hu.MyPK = this.WorkID + "_" + this.FK_Node;
        int i = hu.RetrieveFromDBSources();

        this.Pub1.AddFieldSet("挂起方式");
        RadioButton rb = new RadioButton();
        rb.GroupName = "s";
        rb.Text = "永久挂起";
        rb.ID = "RB_HungWay0";
        if (hu.HungUpWay == 0)
            rb.Checked = true;
        else
            rb.Checked = false;

        this.Pub1.Add(rb);
        this.Pub1.AddBR();

        rb = new RadioButton();
        rb.GroupName = "s";
        rb.Text = "在指定的日期自动解除挂起.";
        rb.ID = "RB_HungWay1";
        if (hu.HungUpWay == 1)
            rb.Checked = true;
        else
            rb.Checked = false;
        this.Pub1.Add(rb);
        this.Pub1.AddBR();

        this.Pub1.Add("&nbsp;&nbsp;&nbsp;&nbsp;解除流程挂起的日期:");
        BP.Web.Controls.TB tb = new BP.Web.Controls.TB();
        tb.ShowType = BP.Web.Controls.TBType.DateTime;
        tb.ID = "TB_RelData";
        if (hu.RelData.Length == 0)
        {
            DateTime dt = DateTime.Now.AddDays(7);
            hu.RelData = dt.ToString("yyyy-MM-dd HH:mm");
        }
        tb.Text = hu.RelData;
        this.Pub1.Add(tb);
        this.Pub1.AddFieldSetEnd();

        this.Pub1.AddFieldSet("挂起原因:");
        tb = new BP.Web.Controls.TB();
        tb.ID = "TB_Note";
        tb.TextMode = TextBoxMode.MultiLine;
        tb.Columns = 70;
        tb.Height = 50;
        this.Pub1.Add(tb);
        this.Pub1.AddFieldSetEnd();

        this.Pub1.Add("&nbsp;&nbsp;&nbsp;&nbsp;");
        Button btn = new Button();
        btn.ID = "Btn_OK";

        GenerWorkFlow gwf = new GenerWorkFlow(this.WorkID);
        if (gwf.WFState == WFState.HungUp)
            btn.Text = " 取消挂起 ";
        else
            btn.Text = " 挂起 ";

        btn.Click += new EventHandler(btn_Click);
        this.Pub1.Add(btn);

        btn = new Button();
        btn.ID = "Btn_Cancel";
        btn.Text = " 关闭 ";
        btn.Click += new EventHandler(btn_Click);
        this.Pub1.Add(btn);
    }

    void btn_Click(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        if (btn.ID == "Btn_Cancel")
        {
            this.WinClose();
            return;
        }

        try
        {
            BP.WF.WorkFlow wf = new WorkFlow(this.FK_Flow, this.WorkID);
            if (wf.HisGenerWorkFlow.WFState == WFState.HungUp)
            {
                string msg=wf.DoUnHungUp();
                this.WinClose(msg);
                return;
            }

            HungUpWay way = HungUpWay.SpecDataRel;
            RadioButton rb = this.Pub1.GetRadioButtonByID("RB_HungWay0");
            if (rb.Checked)
                way = HungUpWay.ForEver;
            
            string reldata= this.Pub1.GetTBByID("TB_RelData").Text;
            string note = this.Pub1.GetTBByID("TB_Note").Text;
            string msg1 = wf.DoHungUp(way, reldata, note);

            BP.WF.Dev2Interface.Node_HungUp_Or_UnHungUp(this.FK_Flow, this.WorkID, (int)way, reldata, msg1);
            this.WinClose(msg1);
        }
        catch (Exception ex)
        {
            this.Alert(ex.Message);
            //this.Pub1.AddMsgOfWarning.Response.Write(ex);
        }
    }
}