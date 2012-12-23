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

public partial class WF_Admin_BillSet : WebPage
{
    public int NodeID
    {
        get
        {
            return int.Parse( this.Request.QueryString["NodeID"] );
        }
    }
    public string FK_Flow
    {
        get
        {
            return this.Request.QueryString["FK_Flow"];
        }
    }

    public void DoNew(BillTemplate bill)
    {
        this.Ucsys1.Clear();
        BP.WF.Node nd = new BP.WF.Node(this.NodeID);
        this.Ucsys1.AddTable();
        this.Ucsys1.AddCaptionLeft("<a href='Bill.aspx?FK_Flow=" + this.FK_Flow + "&NodeID=" + this.NodeID + "' >" + this.ToE("Back", "返回") + "</a> - <a href=Bill.aspx?FK_Flow=" + this.FK_Flow + "&NodeID=" + this.NodeID + "&DoType=New ><img  border=0 src='../../Images/Btn/New.gif' />" + this.ToE("New", "新建") + "</a>-" + BP.WF.Glo.GenerHelp("Bill"));
        this.Ucsys1.AddTR();
        this.Ucsys1.AddTDTitle(ToE("Item", "项目"));
        this.Ucsys1.AddTDTitle(ToE("Input", "输入"));
        this.Ucsys1.AddTDTitle(ToE("Note", "备注"));
        this.Ucsys1.AddTREnd();

        this.Ucsys1.AddTR();
        this.Ucsys1.AddTD(this.ToE("BillType", "单据类型")); // 单据/单据名称
        DDL ddl = new DDL();
        ddl.ID = "DDL_BillType";

        BP.WF.BillTypes ens = new BillTypes();
        ens.RetrieveAllFromDBSource();

        if (ens.Count == 0)
        {
            BP.WF.BillType enB = new BillType();
            enB.Name = this.ToE("NewType", "新建类型") + "1";
            enB.FK_Flow = this.FK_Flow;
            enB.No ="01";
            enB.Insert();
            ens.AddEntity(enB);
        }

        ddl.BindEntities(ens);
        ddl.SetSelectItem(bill.FK_BillType);
        this.Ucsys1.AddTD(ddl);
        this.Ucsys1.AddTD("<a href='Bill.aspx?FK_Flow=" + this.FK_Flow + "&NodeID=" + this.NodeID + "&DoType=EditType'><img src='../../Images/Btn/Edit.gif' border=0/>类别维护</a>");
        this.Ucsys1.AddTREnd();

        this.Ucsys1.AddTR();
        this.Ucsys1.AddTD(ToE("No", "编号"));
        TB tb = new TB();
        tb.ID = "TB_No";
        tb.Text = bill.No;
        tb.Enabled = false;
        if (tb.Text == "")
            tb.Text = this.ToE("AutoGener", "系统自动生成");

        this.Ucsys1.AddTD(tb);
        this.Ucsys1.AddTD("");
        this.Ucsys1.AddTREnd();

        this.Ucsys1.AddTR();
        this.Ucsys1.AddTD(this.ToE("Name", "名称")); // 单据/单据名称
        tb = new TB();
        tb.ID = "TB_Name";
        tb.Text = bill.Name;
        tb.Columns = 40;
        this.Ucsys1.AddTD("colspan=2", tb);
        this.Ucsys1.AddTREnd();

        this.Ucsys1.AddTR();
        this.Ucsys1.AddTD(this.ToE("Name", "生成的文件类型")); // 单据/单据名称
        ddl = new DDL();
        ddl.ID = "DDL_BillFileType";
        ddl.BindSysEnum("BillFileType");
        ddl.SetSelectItem(bill.BillFileType);
        this.Ucsys1.AddTD(ddl);
        this.Ucsys1.AddTD("目前不支持excel,html格式.");
        this.Ucsys1.AddTREnd();


        this.Ucsys1.AddTR();
        this.Ucsys1.AddTD(this.ToE("BillTemplete", "单据模板"));
        HtmlInputFile file = new HtmlInputFile();
        file.ID = "f";
        file.Attributes["width"] = "100%";
        this.Ucsys1.AddTD("colspan=2", file);
         
        this.Ucsys1.AddTREnd();

        this.Ucsys1.AddTRSum();
        this.Ucsys1.Add("<TD class=TD colspan=3 align=center>");
        Button btn = new Button();
        btn.CssClass = "Btn";
        btn.ID = "Btn_Save";
        btn.Text = this.ToE("Save", "保存");
        this.Ucsys1.Add(btn);
        btn.Click += new EventHandler(btn_Click);
        this.Ucsys1.Add(btn);
        if (bill.No.Length > 1)
        {
          

            btn = new Button();
            btn.ID = "Btn_Del";
            btn.CssClass = "Btn";
            btn.Text = this.ToE("Del", "删除"); // "删除单据";
            this.Ucsys1.Add(btn);
            btn.Attributes["onclick"] += " return confirm('" + this.ToE("AYS", "您确认吗？") + "');";
            btn.Click += new EventHandler(btn_Del_Click);
        }
        string url = "";
        if (this.RefNo != null)
            url = "<a href='../../DataUser/CyclostyleFile/" + bill.No + ".rtf'><img src='../../Images/Btn/save.gif' border=0/>" + this.ToE("DownTemplete", "模板下载") + "</a>";

        this.Ucsys1.Add(url + "</TD>");
        this.Ucsys1.AddTREnd();
 
        this.Ucsys1.AddTable();
    }
    void btn_Gener_Click(object sender, EventArgs e)
    {
        string url = "Bill.aspx?FK_Flow=" + this.FK_Flow + "&NodeID=" + this.NodeID + "&DoType=Edit&RefNo=" + this.RefNo;
        this.Response.Redirect(url, true);
    }
    void btn_Click(object sender, EventArgs e)
    {
        HtmlInputFile file = this.Ucsys1.FindControl("f") as HtmlInputFile;
        BillTemplate bt = new BillTemplate();
        bt.NodeID = this.NodeID;
        BP.WF.Node nd = new BP.WF.Node(this.NodeID);
        if (this.RefNo != null)
        {
            bt.No = this.RefNo;
            bt.Retrieve();
            bt = this.Ucsys1.Copy(bt) as BillTemplate;
            bt.NodeID = this.NodeID;
            bt.FK_BillType = this.Ucsys1.GetDDLByID("DDL_BillType").SelectedItemStringVal;
            if (file.Value == null || file.Value.Trim() == "")
            {
                bt.Update();
                this.Alert(this.ToE("SaveOK", "保存成功"));
                return;
            }

            if (file.Value.ToLower().Contains(".rtf") == false)
            {
                this.Alert(this.ToE("Bill1", "@错误，非法的 rtf 格式文件。"));
                return;
            }
            string temp = BP.SystemConfig.PathOfCyclostyleFile + "\\Temp.rtf";
            file.PostedFile.SaveAs(temp);

            //检查文件是否正确。
            try
            {
                string[] paras = BP.DA.Cash.GetBillParas_Gener("Temp.rtf", nd.HisFlow.HisFlowData.EnMap.Attrs);
            }
            catch (Exception ex)
            {
                this.Ucsys2.AddMsgOfWarning("错误信息", ex.Message);
                return;
            }
            string fullFile = BP.SystemConfig.PathOfCyclostyleFile + "\\" + bt.No + ".rtf";
            System.IO.File.Copy(temp, fullFile, true);
            return;
        }
        bt = this.Ucsys1.Copy(bt) as BillTemplate;
        if (file.Value == null || file.Value.ToLower().Contains(".rtf") == false)
        {
            this.Alert(this.ToE("Bill1", "@错误，非法的 rtf 格式文件。"));
            // this.Alert("@错误，非法的 rtf 格式文件。");
            return;
        }

        /* 如果包含这二个字段。*/
        string fileName = file.PostedFile.FileName;
        fileName = fileName.Substring(fileName.LastIndexOf("\\") + 1);
        if (bt.Name == "")
            bt.Name = fileName.Replace(".rtf", "");
        try
        {
            bt.No = BP.DA.chs2py.convert(bt.Name);
            if (bt.IsExits)
                bt.No = bt.No + "." + BP.DA.DBAccess.GenerOID().ToString();
        }
        catch
        {
            bt.No = BP.DA.DBAccess.GenerOID().ToString();
        }

        string tmp = BP.SystemConfig.PathOfCyclostyleFile + "\\Temp.rtf";
        file.PostedFile.SaveAs(tmp);

        //检查文件是否正确。
        try
        {
            string[] paras1 = BP.DA.Cash.GetBillParas_Gener("Temp.rtf", nd.HisFlow.HisFlowData.EnMap.Attrs);
        }
        catch (Exception ex)
        {
            this.Ucsys2.AddMsgOfWarning("Error:", ex.Message);
            return;
        }

        string fullFile1 = BP.SystemConfig.PathOfCyclostyleFile + "\\" + bt.No + ".rtf";
        System.IO.File.Copy(tmp, fullFile1, true);
        // file.PostedFile.SaveAs(fullFile1);
        bt.FK_BillType = this.Ucsys1.GetDDLByID("DDL_BillType").SelectedItemStringVal;
        bt.Insert();

        #region 更新节点信息。
        string Billids = "";
        BillTemplates tmps = new BillTemplates(nd);
        foreach (BillTemplate Btmp in tmps)
        {
            Billids += "@" + Btmp.No;
        }
        nd.HisBillIDs = Billids;
        nd.Update();
        #endregion 更新节点信息。

        this.Response.Redirect("Bill.aspx?FK_Flow=" + this.FK_Flow + "&NodeID=" + this.NodeID, true);
    }
    void btn_Del_Click(object sender, EventArgs e)
    {
        BillTemplate t = new BillTemplate();
        t.No = this.RefNo;
        t.Delete();

        #region 更新节点信息。
        BP.WF.Node nd = new BP.WF.Node(this.NodeID);
        string Billids = "";
        BillTemplates tmps = new BillTemplates(nd);
        foreach (BillTemplate tmp in tmps)
        {
            Billids += "@" + tmp.No;
        }
        nd.HisBillIDs = Billids;
        nd.Update();
        #endregion 更新节点信息。
        this.Response.Redirect("Bill.aspx?FK_Flow=" + this.FK_Flow + "&NodeID=" + this.NodeID, true);
    }
    /// <summary>
    /// 类别修改
    /// </summary>
    public void EditTypes()
    {
        this.Ucsys1.AddTable();
        this.Ucsys1.AddCaptionLeft("<a href='Bill.aspx?FK_Flow=" + this.FK_Flow + "&NodeID=" + this.NodeID + "'>返回</a> -单据类别维护");
        this.Ucsys1.AddTR();
        this.Ucsys1.AddTDTitle("类别编号");
        this.Ucsys1.AddTDTitle("类别名称");
        this.Ucsys1.AddTREnd();

        BillTypes ens = new BillTypes();
        ens.RetrieveAll();
        for (int i = 1; i < 18; i++)
        {
            this.Ucsys1.AddTR();
            this.Ucsys1.AddTD(i.ToString().PadLeft(2, '0'));
            TextBox tb = new TextBox();
            tb.ID = "TB_" + i;
            tb.Columns = 50;
            try
            {
                BillType en = ens[i - 1] as BillType;
                tb.Text = en.Name;
                this.Ucsys1.AddTD(tb);
            }
            catch
            {
                this.Ucsys1.AddTD(tb);
            }
            this.Ucsys1.AddTREnd();
        }

        this.Ucsys1.AddTableEndWithHR();
        Button btn = new Button();
        btn.ID = "Btn_Save";
        btn.Text = "Save";
        btn.CssClass = "Btn";
        btn.Click+=new EventHandler(btn_SaveTypes_Click);
        this.Ucsys1.Add(btn);
    }
    protected void btn_SaveTypes_Click(object sender, EventArgs e)
    {
        BillTypes ens = new BillTypes();
        ens.RetrieveAll();
        ens.Delete();
        for (int i = 1; i < 18; i++)
        {
            string name = this.Ucsys1.GetTextBoxByID("TB_" + i).Text;
            if (string.IsNullOrEmpty(name))
                continue;

            BillType en = new BillType();
            en.No = i.ToString().PadLeft(2, '0');
            en.Name = name;
            en.FK_Flow = this.FK_Flow;
            en.Insert();
        }
        this.Alert("保存成功.");
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = this.ToE("NodeBillDesign", "节点单据设计"); //"节点单据设计";
        switch (this.DoType)
        {
            case "Edit":
                BillTemplate bk1 = new BillTemplate(this.RefNo);
                bk1.NodeID = this.NodeID;
                this.DoNew(bk1);
                return;
            case "New":
                 BillTemplate bk = new BillTemplate();
                bk.NodeID = this.RefOID;
                this.DoNew(bk);
                return;
            case "EditType":
                EditTypes();
                return;
            default:
                break;
        }

        BillTemplates Bills = new BillTemplates(this.NodeID);
        if (Bills.Count == 0)
        {
            this.Response.Redirect("Bill.aspx?FK_Flow=" + this.FK_Flow + "&NodeID=" + this.NodeID + "&DoType=New", true);
            return;
        }

        BP.WF.Node nd = new BP.WF.Node(this.NodeID);
        this.Title = nd.Name + " - " + this.ToE("BillMang", "单据管理");  //单据管理
        this.Ucsys1.AddTable();
        if (this.RefNo ==null)
            this.Ucsys1.AddCaptionLeft("<a href='Bill.aspx?FK_Flow=" + this.FK_Flow + "&NodeID=" + this.NodeID + "&DoType=New'><img src='../../Images/Btn/New.gif' border=0/>" + this.ToE("New", "新建") + "</a> -<a href='Bill.aspx?FK_Flow=" + this.FK_Flow + "&NodeID=" + this.NodeID + "&DoType=EditType'><img src='../../Images/Btn/Edit.gif' border=0/>类别维护</a>");
        this.Ucsys1.AddTR();
        this.Ucsys1.AddTDTitle("IDX");
        this.Ucsys1.AddTDTitle(this.ToE("No","编号"));
        this.Ucsys1.AddTDTitle(this.ToE("Name","名称"));
        this.Ucsys1.AddTDTitle(this.ToE("Oper", "操作"));
        this.Ucsys1.AddTREnd();
        int i = 0;
        foreach (BillTemplate Bill in Bills)
        {
            i++;
            this.Ucsys1.AddTR();
            this.Ucsys1.AddTDIdx(i);
             
            this.Ucsys1.AddTD(Bill.No);
            this.Ucsys1.AddTD("<img src='../../Images/Btn/Word.gif' >" + Bill.Name);
            this.Ucsys1.AddTD("<a href='Bill.aspx?FK_Flow=" + this.FK_Flow + "&NodeID=" + this.NodeID + "&DoType=Edit&RefNo=" + Bill.No + "'><img src='../../Images/Btn/Edit.gif' border=0/>" + this.ToE("Edit", "编辑") + "</a>|<a href='../../DataUser/CyclostyleFile/" + Bill.No + ".rtf'><img src='../../Images/Btn/save.gif' border=0/>" + this.ToE("DownTemplete", "模板下载") + "</a>");
            this.Ucsys1.AddTREnd();
        }
        this.Ucsys1.AddTableEnd();
    }
}
