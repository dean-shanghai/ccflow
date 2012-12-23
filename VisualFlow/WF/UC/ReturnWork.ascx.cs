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
using BP.Port;
using BP.Web.Controls;
using BP.DA;
using BP.En;
using BP.Web;

public partial class WF_UC_ReturnWork : BP.Web.UC.UCBase3
{
    #region FK_Node
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
            try
            {
                return int.Parse(this.Request.QueryString["FK_Node"]);
            }
            catch
            {
                return 0;
            }
        }
    }
    public int FID
    {
        get
        {
            try
            {
                return int.Parse(this.Request.QueryString["FID"]);
            }
            catch
            {
                return 0;
            }
        }
    }
    public Int64 WorkID
    {
        get
        {
            try
            {
                return Int64.Parse(this.Request.QueryString["WorkID"]);
            }
            catch
            {
                return 0;
            }
        }
    }
    public DDL DDL1
    {
        get
        {
            return this.ToolBar1.GetDDLByID("DDL1");
        }
    }
    public TextBox  TB1
    {
        get
        {
            return this.Pub1.GetTextBoxByID("TB_Doc");
        }
    }
    #endregion FK_Node

    public void BindItWorkHL(BP.WF.Node nd)
    {
        this.ToolBar1.AddBtn("Btn_OK", this.ToE("OK", "确定退回"));
        this.ToolBar1.GetBtnByID("Btn_OK").Attributes["onclick"] = " return confirm('" + this.ToE("AYS", "您确定要执行吗?") + "');";
        this.ToolBar1.GetBtnByID("Btn_OK").Click += new EventHandler(WF_UC_ReturnWork_HL_Click);
        TextBox tb = new TextBox();
        tb.TextMode = TextBoxMode.MultiLine;
        tb.ID = "TB_Doc";
        tb.Rows = 20;
        tb.Columns = 50;
        this.Pub1.Add(tb);
        if (this.IsPostBack == false)
        {
            try
            {
                this.TB1.Text = "您好: \n   您处理的工作有错误，需要您重新办理．\n\n  此致!!!   \n " + WebUser.Name + BP.DA.DataType.CurrentDataTime;
            }
            catch (Exception ex)
            {
                this.Pub1.AddMsgOfWarning("提示:", "@:" + this.ToE("WorkBackErr", "下列原因造成不能退回:") + ex.Message);
            }
        }
    }
    public void BindItWork_SubWork_Return(BP.WF.Node nd)
    {
        this.ToolBar1.AddLab("sd", "<b>退回到:</b>");
        this.ToolBar1.AddDDL("DDL1");
        this.ToolBar1.AddBtn("Btn_OK", "确定");  
        this.ToolBar1.GetBtnByID("Btn_OK").Attributes["onclick"] = " return confirm('" + this.ToE("AYS", "您确定要执行吗?") + "');";
        this.ToolBar1.GetBtnByID("Btn_OK").Click += new EventHandler(WF_UC_ReturnWork_FL_Click);
        DataTable dt = BP.WF.Dev2Interface.DB_GenerWillReturnNodes(this.FK_Node, this.WorkID, this.FID);
        foreach (DataRow dr in dt.Rows)
        {
            this.DDL1.Items.Add(new ListItem( dr["RecName"]+"=>"+dr["Name"].ToString(), dr["No"].ToString()));
        }

        TB tb = new TB();
        tb.TextMode = TextBoxMode.MultiLine;
        tb.ID = "TB_Doc";
        tb.Rows = 15;
        tb.Columns = 50;
        this.Pub1.Add(tb);
        if (this.IsPostBack == false)
        {
            try
            {
                WorkNode wn = new WorkNode(this.WorkID, this.FK_Node);
                Work wk = wn.HisWork;
                this.TB1.Text = this.ToEP4("WBackInfo",
                    "{0}同志: \n   您在{1}处理的“{2}”工作有错误，需要您重新办理．\n\n  此致!!!   \n {3}",
                    wk.Rec + wk.RecText, wk.CDT, wn.HisNode.Name, WebUser.Name + BP.DA.DataType.CurrentDataTime);
            }
            catch (Exception ex)
            {
                this.Pub1.AddMsgOfWarning("提示:", "@:" + this.ToE("WorkBackErr", "下列原因造成不能退回:") + ex.Message);
            }
        }
    }
    void WF_UC_ReturnWork_FL_Click(object sender, EventArgs e)
    {
        try
        {
            WorkNode wn = new WorkNode(this.WorkID, this.FK_Node);
            Work wk = wn.HisWork;
            wk.OID = this.WorkID;
            wk.FID = this.FID;
            wk.Retrieve();
            WorkNode mywn = null;
            mywn = wn.DoReturnWork(this.ToolBar1.GetDDLByID("DDL1").SelectedItemIntVal, this.TB1.Text);

            // 退回事件。
            string msg = mywn.HisNode.MapData.FrmEvents.DoEventNode(EventListOfNode.ReturnAfter, wk);
            this.ToMsg(this.ToEP2("WReInfo", "@任务被你成功退回到【{0}】，退回给【{1}】。", mywn.HisNode.Name, mywn.HisWork.Rec),
                "info");
            return;
        }
        catch (Exception ex)
        {
            this.ToMsg(ex.Message, "info");
        }
    }
    void WF_UC_ReturnWork_HL_Click(object sender, EventArgs e)
    {
        try
        {
            string info = BP.WF.Dev2Interface.Node_FHL_Return(this.FK_Node, this.FID, this.WorkID, this.TB1.Text);
            if (this.PageID == "ReturnWorkSmall")
                this.WinCloseWithMsg(info);
            else
                this.ToMsg(info, "info");
        }
        catch (Exception ex)
        {
            this.ToMsg(ex.Message, "info");
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Page.Title = this.ToE("WorkBack", "工作退回");
        BP.WF.Node nd = new BP.WF.Node(this.FK_Node);
        if (this.FID != 0)
        {
            this.BindItWork_SubWork_Return(nd);
            return;
        }

        this.ToolBar1.Add(this.ToE("ReturnTo", "<b>退回到:</b>"));
        this.ToolBar1.AddDDL("DDL1");
        this.ToolBar1.AddBtn("Btn_OK", this.ToE("OK", "确定"));
        this.ToolBar1.GetBtnByID("Btn_OK").Attributes["onclick"] = " return confirm('" + this.ToE("AYS", "您确定要执行吗?") + "');";
        this.ToolBar1.GetBtnByID("Btn_OK").Click += new EventHandler(WF_UC_ReturnWork_Click);
        this.ToolBar1.AddBtn("Btn_Cancel", this.ToE("Cancel", "取消"));
        this.ToolBar1.GetBtnByID("Btn_Cancel").Click += new EventHandler(WF_UC_ReturnWork_Click);
        string appPath = this.Request.ApplicationPath;
        if (nd.IsBackTracking)
        {
            /*如果允许原路退回*/
            CheckBox cb = new CheckBox();
            cb.ID = "CB_IsBackTracking";
            cb.Text = "退回后是否要原路返回?";
            this.ToolBar1.Add(cb);
        }
       
        TextBox tb = new TextBox();
        tb.TextMode = TextBoxMode.MultiLine;
        tb.ID = "TB_Doc";
        tb.Rows = 15;
        tb.Columns = 50;
        this.Pub1.Add(tb);
        if (this.IsPostBack == false)
        {
            try
            {
                WorkNode wn = new WorkNode(this.WorkID, this.FK_Node);
                DataTable dt = BP.WF.Dev2Interface.DB_GenerWillReturnNodes(this.FK_Node, this.WorkID,this.FID);
                foreach (DataRow dr in dt.Rows)
                {
                    this.DDL1.Items.Add(new ListItem(dr["RecName"] + "=>" + dr["Name"].ToString(), dr["No"].ToString()));
                }

                WorkNode pwn = wn.GetPreviousWorkNode();
                this.DDL1.SetSelectItem(pwn.HisNode.NodeID);
                this.DDL1.Enabled = true;
                Work wk = pwn.HisWork;
                if (wn.HisNode.FocusField != "")
                {
                    this.TB1.Text = wn.HisWork.GetValStrByKey(wn.HisNode.FocusField);
                }
                else
                {
                    this.TB1.Text = this.ToEP4("WBackInfo",
                  "{0}同志: \n  您在{1}处理的“{2}”工作有错误，需要您重新办理．\n\n此致!!!   \n\n  {3}",
                  "", wk.CDT, pwn.HisNode.Name, WebUser.Name + "\n  " + BP.DA.DataType.CurrentDataTime);
                }
            }
            catch (Exception ex)
            {
                this.Pub1.AddMsgOfWarning("提示:", "@:" + this.ToE("WorkBackErr", "下列原因造成不能退回:") + ex.Message);
            }
        }
    }
    void WF_UC_ReturnWork_Click(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        switch (btn.ID)
        {
            case "Btn_Cancel":
                this.Response.Redirect("MyFlow" + Glo.FromPageType + ".aspx?FK_Flow=" + this.FK_Flow + "&WorkID=" + this.WorkID + "&FK_Node=" + this.FK_Node, true);
                return;
            default:
                break;
        }

        try
        {

            WorkNode wn = new WorkNode(this.WorkID, this.FK_Node);
            Work wk = wn.HisWork;
            string returnInfo = this.TB1.Text;
            int reNode = this.DDL1.SelectedItemIntVal;

            bool IsBackTracking = false;
            if (wn.HisNode.IsBackTracking)
                IsBackTracking = this.ToolBar1.GetCBByID("CB_IsBackTracking").Checked;

            //执行退回api.
            string rInfo = BP.WF.Dev2Interface.Node_ReturnWork(this.FK_Flow, this.WorkID, reNode, returnInfo, IsBackTracking);
            this.ToMsg(rInfo, "info");
            return;
        }
        catch (Exception ex)
        {
            this.ToMsg(ex.Message, "info");
        }
    }

    public void ToMsg(string msg, string type)
    {
        this.Session["info"] = msg;
        this.Response.Redirect("MyFlowInfo" + Glo.FromPageType + ".aspx?FK_Flow=" + this.FK_Flow + "&FK_Type=" + type + "&FK_Node=" + this.FK_Node + "&WorkID=" + this.WorkID, false);
    }
}
