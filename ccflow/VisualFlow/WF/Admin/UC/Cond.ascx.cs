﻿using System;
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

public partial class WF_Admin_UC_Cond : BP.Web.UC.UCBase3
{
    #region 属性
    /// <summary>
    /// 主键
    /// </summary>
    public new string MyPK
    {
        get
        {
            return this.Request.QueryString["MyPK"];
        }
    }
    /// <summary>
    /// 流程编号
    /// </summary>
    public string FK_Flow
    {
        get
        {
            return this.Request.QueryString["FK_Flow"];
        }
    }
    public string FK_Attr
    {
        get
        {
            string s = this.Request.QueryString["FK_Attr"];
            if (s == null || s == "")
                s = ViewState["FK_Attr"] as string;
 
            if (s == null || s== "")
            {
                try
                {
                    s = this.DDL_Attr.SelectedItemStringVal;
                }
                catch
                {
                    return null;
                }
            }
            if (s == "")
                return null;
            return s;
        }
        set
        {
            ViewState["FK_Attr"] = value;
        }
    }
    /// <summary>
    /// 节点
    /// </summary>
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
                return this.FK_MainNode;
            }
        }
    }
    public int FK_MainNode
    {
        get
        {
            return int.Parse(this.Request.QueryString["FK_MainNode"]);
        }
    }
    public int ToNodeID
    {
        get
        {
            try
            {
                return int.Parse(this.Request.QueryString["ToNodeID"]);
            }
            catch
            {
                return 0;
            }
        }
    }
    /// <summary>
    /// 执行类型
    /// </summary>
    public CondType HisCondType
    {
        get
        {
            return (CondType)int.Parse(this.Request.QueryString["CondType"]);
        }
    }
    public string GetOperVal
    {
        get
        {
            if (this.IsExit("TB_Val"))
                return this.GetTBByID("TB_Val").Text;
            return this.GetDDLByID("DDL_Val").SelectedItemStringVal;
        }
    }
    public string GetOperValText
    {
        get
        {
            if (this.IsExit("TB_Val"))
                return this.GetTBByID("TB_Val").Text;
            return this.GetDDLByID("DDL_Val").SelectedItem.Text;
        }
    }
    #endregion 属性

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Page.Title = "表单类型条件设置";

        if (this.Request.QueryString["DoType"] == "Del")
        {
            Cond nd = new Cond(this.MyPK);
            nd.Delete();
            this.Response.Redirect("Cond.aspx?CondType=" + (int)this.HisCondType + "&FK_Flow=" + this.FK_Flow + "&FK_MainNode=" + nd.NodeID + "&FK_Node=" + this.FK_MainNode + "&ToNodeID=" + nd.ToNodeID, true);
            return;
        }
        this.BindCond();

        if (this.FK_Attr == null)
        {
            this.FK_Attr = this.DDL_Attr.SelectedItemStringVal;
        }
    }
    public void BindCond()
    {
        string msg = "";
        string note = "";

        BP.WF.Cond cond = new Cond();
        cond.MyPK = this.MyPK;
        if (cond.RetrieveFromDBSources() == 0)
        {
            if (this.FK_Attr != null)
                cond.FK_Attr = this.FK_Attr;
            if (this.FK_MainNode != 0)
                cond.NodeID = this.FK_MainNode;
            if (this.FK_Node != 0)
                cond.FK_Node = this.FK_Node;
            if (this.FK_Flow != null)
                cond.FK_Flow = this.FK_Flow;
        }

        //this.Clear();
        //switch (this.HisCondType)
        //{
        //    case CondType.Node:
        //        this.AddFieldSet(this.ToE("NodeT", "节点方向") + " - " + this.ToE("CondDesign", "表单条件设计"));
        //        break;
        //    case CondType.Flow:
        //        this.AddFieldSet(this.ToE("FlowT", "流程完成条件") + " - " + this.ToE("CondDesign", "表单条件设计"));
        //        break;
        //    case CondType.Dir:
        //        this.AddFieldSet(this.ToE("DirT", "方向条件") + " - " + this.ToE("CondDesign", "表单条件设计"));
        //        break;
        //    case CondType.FLRole:
        //        this.AddFieldSet(this.ToE("DirT", "分流完成条件设计") + " - " + this.ToE("CondDesign", "表单条件设计"));
        //        break;
        //    default:
        //        break;
        //}

        this.AddFieldSet("表单类型:条件设置");

        this.AddTable("border=0 widht='500px'");
        this.AddTR();
        this.AddTDTitle(this.ToE("Item", "项目"));
        this.AddTDTitle(this.ToE("Input", "采集"));
        this.AddTDTitle(this.ToE("Desc", "描述"));
        this.AddTREnd();

        this.AddTR();
        this.AddTD(this.ToE("Node", "节点"));
        Nodes nds = new Nodes(cond.FK_Flow);
        Nodes ndsN = new Nodes();
        foreach (BP.WF.Node mynd in nds)
        {
            ndsN.AddEntity(mynd);
        }
        DDL ddl = new DDL();
        ddl.ID = "DDL_Node";
        ddl.BindEntities(ndsN, "NodeID", "Name");
        ddl.SetSelectItem(cond.FK_Node);
        ddl.AutoPostBack = true;
        ddl.SelectedIndexChanged += new EventHandler(ddl_SelectedIndexChanged);
        this.AddTD(ddl);
        this.AddTD(this.ToE("Node", "节点"));
        this.AddTREnd();

        // 属性/字段
        MapAttrs attrs = new MapAttrs();
        attrs.Retrieve(MapAttrAttr.FK_MapData, "ND" + ddl.SelectedItemStringVal);

        MapAttrs attrNs = new MapAttrs();
        foreach (MapAttr attr in attrs)
        {
            if (attr.IsBigDoc)
                continue;

            switch (attr.KeyOfEn)
            {
                case "Title":
                //case "RDT":
                //case "CDT":
                case "FK_Emp":
                case "NodeState":
                case "MyNum":
                case "FK_NY":
                case WorkAttr.Emps:
                case WorkAttr.OID:
                case StartWorkAttr.Rec:
                case StartWorkAttr.WFState:
                case StartWorkAttr.FID:
                    continue;
                default:
                    break;
            }
            attrNs.AddEntity(attr);
        }
        ddl = new DDL();
        ddl.ID = "DDL_Attr";
        if (attrNs.Count == 0)
        {
            BP.WF.Node nd = new BP.WF.Node(cond.FK_Node);
            nd.RepareMap();
            this.AddTR();
            this.AddTD("");
            this.AddTD("colspan=2", "节点没有找到合适的条件");
            this.AddTREnd();
            this.AddTableEnd();
            return;
        }
        else
        {
            ddl.BindEntities(attrNs, MapAttrAttr.MyPK, MapAttrAttr.Name);
            ddl.AutoPostBack = true;
            ddl.SelectedIndexChanged += new EventHandler(ddl_SelectedIndexChanged);
            ddl.SetSelectItem(cond.FK_Attr);
        }

        this.AddTR();
        this.AddTD(this.ToE("AttrFiled", "属性/字段"));
        this.AddTD(ddl);
        this.AddTD("");
        this.AddTREnd();

        MapAttr attrS = new MapAttr(this.DDL_Attr.SelectedItemStringVal);
        this.AddTR();
        this.AddTD(this.ToE("OperS", "操作符"));
        ddl = new DDL();
        ddl.ID = "DDL_Oper";
        switch (attrS.LGType)
        {
            case BP.En.FieldTypeS.Enum:
            case BP.En.FieldTypeS.FK:
                ddl.Items.Add(new ListItem("=", "="));
                ddl.Items.Add(new ListItem("<>", "<>"));
                break;
            case BP.En.FieldTypeS.Normal:
                switch (attrS.MyDataType)
                {
                    case BP.DA.DataType.AppString:
                    case BP.DA.DataType.AppDate:
                    case BP.DA.DataType.AppDateTime:
                        ddl.Items.Add(new ListItem("=", "="));
                        ddl.Items.Add(new ListItem("LIKE", "LIKE"));
                        ddl.Items.Add(new ListItem("<>", "<>"));
                        break;
                    case BP.DA.DataType.AppBoolean:
                        ddl.Items.Add(new ListItem("=", "="));
                        break;
                    default:
                        ddl.Items.Add(new ListItem("=", "="));
                        ddl.Items.Add(new ListItem(">", ">"));
                        ddl.Items.Add(new ListItem(">=", ">="));
                        ddl.Items.Add(new ListItem("<", "<"));
                        ddl.Items.Add(new ListItem("<=", "<="));
                        ddl.Items.Add(new ListItem("<>", "<>"));
                        break;
                }
                break;
            default:
                break;
        }

        if (cond != null)
        {
            try
            {
                ddl.SetSelectItem(cond.OperatorValueInt);
            }
            catch
            {
            }
        }
        this.AddTD(ddl);
        this.AddTD("");
        this.AddTREnd();
        switch (attrS.LGType)
        {
            case BP.En.FieldTypeS.Enum:
                this.AddTR();
                this.AddTD(this.ToE("Value", "值"));
                ddl = new DDL();
                ddl.ID = "DDL_Val";
                ddl.BindSysEnum(attrS.UIBindKey);
                if (cond != null)
                {
                    try
                    {
                        ddl.SetSelectItem(cond.OperatorValueInt);
                    }
                    catch
                    {
                    }
                }
                this.AddTD(ddl);
                this.AddTD("");
                this.AddTREnd();
                break;
            case BP.En.FieldTypeS.FK:
                this.AddTR();
                this.AddTD(this.ToE("Value", "值"));

                ddl = new DDL();
                ddl.ID = "DDL_Val";
                ddl.BindEntities(attrS.HisEntitiesNoName);
                if (cond != null)
                {
                    try
                    {
                        ddl.SetSelectItem(cond.OperatorValueStr);
                    }
                    catch
                    {
                    }
                }
                this.AddTD(ddl);
                this.AddTD("");
                this.AddTREnd();
                break;
            default:
                if (attrS.MyDataType == BP.DA.DataType.AppBoolean)
                {
                    this.AddTR();
                    this.AddTD(this.ToE("Value", "值"));
                    ddl = new DDL();
                    ddl.ID = "DDL_Val";
                    ddl.BindAppYesOrNo(0);
                    if (cond != null)
                    {
                        try
                        {
                            ddl.SetSelectItem(cond.OperatorValueInt);
                        }
                        catch
                        {
                        }
                    }
                    this.AddTD(ddl);
                    this.AddTD();
                    this.AddTREnd();
                }
                else
                {
                    this.AddTR();
                    this.AddTD(this.ToE("Value", "值"));
                    TB tb = new TB();
                    tb.ID = "TB_Val";
                    if (cond != null)
                        tb.Text = cond.OperatorValueStr;
                    this.AddTD(tb);
                    this.AddTD();
                    this.AddTREnd();
                }
                break;
        }


        ddl = new DDL();
        ddl.ID = "DDL_ConnJudgeWay";
        ddl.BindSysEnum(BP.WF.CondAttr.ConnJudgeWay);
        ddl.SetSelectItem((int)cond.HisConnJudgeWay);


        Conds conds = new Conds();
        QueryObject qo = new QueryObject(conds);
        qo.AddWhere(CondAttr.NodeID, this.FK_MainNode);
        qo.addAnd();
        qo.AddWhere(CondAttr.CondType, (int)this.HisCondType);
        if (this.ToNodeID != 0)
        {
            qo.addAnd();
            qo.AddWhere(CondAttr.ToNodeID, this.ToNodeID);
        }
        int num = qo.DoQuery();
        if (num >= 1)
        {
            this.AddTR();
            this.AddTD(this.ToE("CondWay", "条件计算方式"));
            this.AddTD(ddl);
            this.AddTD(this.ToE("CondWayD", "对于多个条件有效"));
            this.AddTREnd();
        }

        this.AddTRSum();
        this.Add("<TD class=TD colspan=3 align=center>");
        Button btn = new Button();
        btn.ID = "Btn_Save";
        btn.Text = this.ToE("Save", " 保 存 ");
        btn.Click += new EventHandler(btn_Save_Click);
        this.Add(btn);
        this.Add("</TD>");
        this.AddTREnd();
        this.AddTableEnd();


        #region 条件

        this.AddTable("border=0 widht='500px'");
        this.AddTR();
        this.AddTDTitle("IDX");
        this.AddTDTitle(this.ToE("NodeFrom", "数据来源"));
        this.AddTDTitle(this.ToE("Node", "节点"));
        this.AddTDTitle(this.ToE("Attr", "属性"));
        this.AddTDTitle(this.ToE("OperS", "操作符"));
        this.AddTDTitle(this.ToE("Value", "值"));
        if (num > 1)
            this.AddTDTitle(this.ToE("RunWay", "计算方式"));
        this.AddTDTitle(this.ToE("Oper", "操作"));
        this.AddTREnd();


        int i = 0;
        foreach (Cond mync in conds)
        {
            if (mync.HisDataFrom.ToString() == "Stas"
                || mync.HisDataFrom.ToString() == "Depts")
                continue;
            i++;
            

            this.AddTR();
            this.AddTDIdx(i);
            this.AddTD(mync.HisDataFrom.ToString());
            this.AddTD(mync.FK_NodeT);
            this.AddTD(mync.AttrName);
            this.AddTDCenter(mync.FK_Operator);
            this.AddTD(mync.OperatorValueT);
            if (num > 1)
                this.AddTD(mync.HisConnJudgeWayT);
            this.AddTD("<a href='Cond.aspx?MyPK=" + mync.MyPK + "&CondType=" + (int)this.HisCondType + "&FK_Flow=" + this.FK_Flow + "&FK_Attr=" + mync.FK_Attr + "&FK_MainNode=" + mync.NodeID + "&OperatorValue=" + mync.OperatorValueStr + "&FK_Node=" + mync.FK_Node + "&DoType=Del&ToNodeID=" + mync.ToNodeID + "' >" + this.ToE("Del", "删除") + "</a>");
            this.AddTREnd();
        }

        this.AddTableEnd();
        #endregion

        this.AddFieldSetEnd(); // ("条件设置");

    }
    public DDL DDL_Node
    {
        get
        {
            return this.GetDDLByID("DDL_Node");
        }
    }
    public Label Lab_Msg
    {
        get
        {
            return this.GetLabelByID("Lab_Msg");
        }
    }
    public Label Lab_Note
    {
        get
        {
            return this.GetLabelByID("Lab_Note");
        }
    }
    /// <summary>
    /// 属性
    /// </summary>
    public DDL DDL_Attr
    {
        get
        {
            return this.GetDDLByID("DDL_Attr");
        }
    }
    public DDL DDL_Oper
    {
        get
        {
            return this.GetDDLByID("DDL_Oper");
        }
    }
    public DDL DDL_ConnJudgeWay
    {
        get
        {
            return this.GetDDLByID("DDL_ConnJudgeWay");
        }
    }

    void ddl_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.Response.Redirect("Cond.aspx?FK_Flow=" + this.FK_Flow + "&FK_Node=" + this.DDL_Node.SelectedItemStringVal + "&FK_MainNode=" + this.FK_MainNode + "&CondType=" + (int)this.HisCondType + "&FK_Attr=" + this.DDL_Attr.SelectedItemStringVal + "&ToNodeID=" + this.Request.QueryString["ToNodeID"], true);
    }

    void btn_Save_Click(object sender, EventArgs e)
    {
        if (this.GetOperVal == "" || this.GetOperVal == null)
        {
            this.Alert(this.ToE("CondBlankWarning", "您没有设置条件，请在值文本框中输入值。"));
            return;
        }

        Cond cond = new Cond();
        cond.HisDataFrom = ConnDataFrom.Form;
        cond.NodeID = this.FK_MainNode;
        cond.FK_Attr = this.FK_Attr;
        cond.FK_Node = this.DDL_Node.SelectedItemIntVal;
        cond.FK_Operator = this.DDL_Oper.SelectedItemStringVal;
        cond.OperatorValue = this.GetOperVal;
        cond.OperatorValueT = this.GetOperValText;
        cond.FK_Flow = this.FK_Flow;
        cond.HisCondType = this.HisCondType;
        try
        {
            cond.HisConnJudgeWay = (ConnJudgeWay)this.DDL_ConnJudgeWay.SelectedItemIntVal;
        }
        catch
        {
        }

        string sql = "UPDATE WF_Cond SET ConnJudgeWay=" + (int)cond.HisConnJudgeWay + ", DataFrom=" + (int)ConnDataFrom.Form + " WHERE NodeID=" + cond.NodeID + "  AND FK_Node=" + cond.FK_Node + " AND ToNodeID=" + this.ToNodeID;
        switch (this.HisCondType)
        {
            case CondType.Flow:
            case CondType.Node:
      //      case CondType.FLRole:
                cond.MyPK = BP.DA.DBAccess.GenerOID().ToString();   //cond.NodeID + "_" + cond.FK_Node + "_" + cond.FK_Attr + "_" + cond.OperatorValue;
                cond.Insert();
                BP.DA.DBAccess.RunSQL(sql);
                this.Response.Redirect("Cond.aspx?MyPK=" + cond.MyPK + "&FK_Flow=" + cond.FK_Flow + "&FK_Node=" + cond.FK_Node + "&FK_MainNode=" + cond.NodeID + "&CondType=" + (int)cond.HisCondType + "&FK_Attr=" + cond.FK_Attr, true);
                return;
            case CondType.Dir:
                // cond.MyPK = cond.NodeID +"_"+ this.Request.QueryString["ToNodeID"]+"_" + cond.FK_Node + "_" + cond.FK_Attr + "_" + cond.OperatorValue;
                cond.MyPK = BP.DA.DBAccess.GenerOID().ToString();   //cond.NodeID + "_" + cond.FK_Node + "_" + cond.FK_Attr + "_" + cond.OperatorValue;
                cond.ToNodeID = this.ToNodeID;
                cond.Insert();
                BP.DA.DBAccess.RunSQL(sql);
                //if (cond.Update() == 0)
                //    cond.Insert();
                this.Response.Redirect("Cond.aspx?MyPK=" + cond.MyPK + "&FK_Flow=" + cond.FK_Flow + "&FK_Node=" + cond.FK_Node + "&FK_MainNode=" + cond.NodeID + "&CondType=" + (int)cond.HisCondType + "&FK_Attr=" + cond.FK_Attr + "&ToNodeID=" + this.Request.QueryString["ToNodeID"], true);
                return;
                break;
            default:
                throw new Exception("未设计的情况。");
        }
    }
}
