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
using BP.Sys;
using BP.En;
using BP.Web;
using BP.Web.UC;

public partial class Comm_MapDef_SFTable : BP.Web.WebPage
{
    public new string DoType
    {
        get
        {
            return this.Request.QueryString["DoType"];
        }
    }
    public string IDX
    {
        get
        {
            return this.Request.QueryString["IDX"];
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        SFTable main = new SFTable();
        if (this.RefNo != null)
        {
            main.No = this.RefNo;
            main.Retrieve();
        }
        this.BindSFTable(main);
    }
    public void BindSFTable(SFTable en)
    {

        string star = "<font color=red><b>(*)</b></font>";
        this.Ucsys1.AddTable();
        if (this.RefNo == null)
            this.Ucsys1.AddCaption("<a href='Do.aspx?DoType=AddF&MyPK=" + this.MyPK + "&IDX=" + this.IDX + "'>" + this.ToE("GuideNewField", "增加新字段向导") + "</a> - <a href='Do.aspx?DoType=AddSFTable&MyPK=" + this.MyPK + "&IDX=" + this.IDX + "'>" + this.ToE("FK", "外键") + "</a> - " + this.ToE("NewTable", "新建表"));
        else
            this.Ucsys1.AddCaption("<a href='Do.aspx?DoType=AddF&MyPK=" + this.MyPK + "&IDX=" + this.IDX + "'>" + this.ToE("GuideNewField", "增加新字段向导") + "</a> - <a href='Do.aspx?DoType=AddSFTable&MyPK=" + this.MyPK + "&IDX=" + this.IDX + "'>" + this.ToE("FK", "外键") + "</a> - " + this.ToE("EditTable", "编辑表"));

        if (this.RefNo == null)
            this.Title = this.ToE("NewTable", "新建表");   // "新建表";ble
        else
            this.Title = this.ToE("EditTable", "编辑表");  //"编辑表";

        this.Ucsys1.AddTR();
        this.Ucsys1.AddTDTitle(this.ToE("Item", "项目"));
        this.Ucsys1.AddTDTitle(this.ToE("Gather", "采集"));
        this.Ucsys1.AddTDTitle( this.ToE("Note","备注") );
        this.Ucsys1.AddTREnd();

        this.Ucsys1.AddTR();
        this.Ucsys1.AddTD(this.ToE("TableEName", "表英文名称"+star));
        BP.Web.Controls.TB tb = new BP.Web.Controls.TB();
        tb.ID = "TB_No";
        tb.Text = en.No;
        if (this.RefNo == null)
            tb.Enabled = true;
        else
            tb.Enabled = false;

        if (tb.Text == "")
            tb.Text = "SF_";

        this.Ucsys1.AddTD(tb);
        this.Ucsys1.AddTD(this.ToE("PlsStartWord", "输入:新表名或已经存在的表名"));
        this.Ucsys1.AddTREnd();

        this.Ucsys1.AddTR();
        this.Ucsys1.AddTD(this.ToE("TableName", "表中文名称" + star));
        tb = new BP.Web.Controls.TB();
        tb.ID = "TB_Name";
        tb.Text = en.Name;
        this.Ucsys1.AddTD(tb);
        this.Ucsys1.AddTD();
        this.Ucsys1.AddTREnd();


        this.Ucsys1.AddTR();
        this.Ucsys1.AddTD(this.ToE("Desc", "描述" + star));
        tb = new BP.Web.Controls.TB();
        tb.ID = "TB_TableDesc";
        tb.Text = en.TableDesc;
        this.Ucsys1.AddTD(tb);
        this.Ucsys1.AddTD("");
        this.Ucsys1.AddTREnd();

        this.Ucsys1.AddTR();
        this.Ucsys1.Add("<TD colspan=3 align=center>");
        Button btn = new Button();
        btn.ID = "Btn_Save";
        btn.CssClass = "Btn";
        if (this.RefNo == null)
            btn.Text =  this.ToE("Create","创建");
        else
            btn.Text = this.ToE("Save","保存");

        btn.Click += new EventHandler(btn_Save_Click);
        this.Ucsys1.Add(btn);

        
            btn = new Button();
            btn.ID = "Btn_Edit";
            btn.CssClass = "Btn";
            btn.Text = this.ToE("EditData", "编辑数据"); // "编辑数据"
            if (this.RefNo == null)
                btn.Enabled = false;
            if (en.IsClass)
                btn.Attributes["onclick"] = "WinOpen('../PanelEns.aspx?EnsName=" + en.No + "','dg' ); return false;";
            else
                btn.Attributes["onclick"] = "WinOpen('SFTableEditData.aspx?RefNo=" + this.RefNo + "','dg' ); return false;";
            this.Ucsys1.Add(btn);
         

        btn = new Button();
        btn.ID = "Btn_Add";
        btn.CssClass = "Btn";

        btn.Text = this.ToE("AddToSheet", "添加到表单"); ; // "添加到表单";
        btn.Attributes["onclick"] = " return confirm('" + this.ToE("AYS", "您确认吗？") + "');";
        btn.Click += new EventHandler(btn_Add_Click);
        if (this.RefNo == null)
            btn.Enabled = false;

        this.Ucsys1.Add(btn);
        btn = new Button();
        btn.ID = "Btn_Del";
        btn.CssClass = "Btn";

        btn.Text = this.ToE("Del", "删除");
        btn.Attributes["onclick"] = " return confirm('" + this.ToE("AYS", "您确认吗？") + "');";
        if (this.RefNo == null)
            btn.Enabled = false;

        btn.Click += new EventHandler(btn_Del_Click);
        this.Ucsys1.Add(btn);
        this.Ucsys1.Add("</TD>");
        this.Ucsys1.AddTREnd();
        this.Ucsys1.AddTableEnd();
    }
    void btn_Add_Click(object sender, EventArgs e)
    {
        SFTable table = new SFTable(this.RefNo);
        if (table.HisEns.Count == 0)
        {
            this.Alert("该表里["+this.RefNo+"]中没有数据，您需要维护数据才能");
            return;
        }

        this.Response.Redirect("Do.aspx?DoType=AddSFTableAttr&MyPK=" + this.MyPK + "&IDX=" + this.IDX + "&RefNo=" + this.RefNo, true);
        this.WinClose();
        return;
    }
    void btn_EditData_Click(object sender, EventArgs e)
    {
        //this.Response.Redirect("SFTable.aspx?DoType=Edit&MyPK=" + this.MyPK + "&IDX=" + this.IDX + "&RefNo=" + this.RefNo, true);
        //this.WinClose();
        return;
    }
    void btn_Save_Click(object sender, EventArgs e)
    {
        try
        {
            SFTable main = new SFTable();
            main = (SFTable)this.Ucsys1.Copy(main);

            if (main.No.Length == 0 || main.Name.Length == 0)
                throw new Exception(this.ToE("NoNameBlank", "编号与名称不能为空"));

            try
            {
                main.HisEns.GetNewEntity.CheckPhysicsTable();
            }
            catch
            {
            }


            if (this.RefNo == null)
            {
                main.No = this.Ucsys1.GetTBByID("TB_No").Text;

                if (main.IsExits)
                {
                    string sql = "select No,Name from " + main.No + " WHERE 1=2";
                    try
                    {
                        BP.DA.DBAccess.RunSQLReturnTable(sql);
                    }
                    catch  
                    {
                        this.Alert(ToE("TVErr", "错误:表或视图不存在No,Name列不符合约定规则.") + " Key=" + main.No);
                        return;
                    }
                }
            }
            else
            {
                main.No = this.RefNo;
                main.Retrieve();
                main = (SFTable)this.Ucsys1.Copy(main);
                if (main.No.Length == 0 || main.Name.Length == 0)
                    throw new Exception(this.ToE("NoNameBlank", "编号与名称不能为空"));
            }

            if (main.Name.Length == 0)
                throw new Exception(this.ToE("NoNameBlank", "编号与名称不能为空"));

            if (main.TableDesc.Length == 0)
                throw new Exception(this.ToE("DescBlank", "描述不能为空"));

            if (this.RefNo == null)
            {
                //if (main.No.Contains("SF_") == false)
                //    throw new Exception("物理表不符合命名规则，必须以 SF_ 开头。");
                //  main.FK_Val = main.No.Replace("SF_", "FK_");
                main.FK_Val = main.No; //.Replace("SF_", "FK_");
            }

            //string cfgVal = "";
            //int idx = -1;
            //while (idx < 19)
            //{
            //    idx++;
            //    string t = this.Ucsys1.GetTBByID("TB_" + idx).Text.Trim();
            //    if (t.Length == 0)
            //        continue;
            //    cfgVal += "@" + idx + "=" + t;
            //}
            //main.CfgVal = cfgVal;
            //if (main.CfgVal == "")
            //    throw new Exception("错误，您必须输入表，请参考帮助。");
            // main.IsDel = true;
            main.Save();

          
            //重新生成
            this.Response.Redirect("SFTable.aspx?RefNo=" + main.No + "&MyPK=" + this.MyPK + "&IDX=" + this.IDX, true);
        }
        catch (Exception ex)
        {
            this.Alert(ex.Message);
        }
    }
    void btn_Del_Click(object sender, EventArgs e)
    {
        try
        {
            // 检查这个类型是否被使用？
            MapAttrs attrs = new MapAttrs();
            QueryObject qo = new QueryObject(attrs);
            qo.AddWhere(MapAttrAttr.MyDataType, (int)FieldTypeS.FK);
            qo.addAnd();
            qo.AddWhere(MapAttrAttr.KeyOfEn, this.RefNo);
            int i = qo.DoQuery();
            if (i == 0)
            {
                BP.Sys.SFTable m = new SFTable();
                m.No = this.RefNo;
                m.Delete();
                this.ToMsgPage("外键删除成功");
                return;
            }

            string msg = "错误:下列数据已经引用了外键您不能删除它。";
            foreach (MapAttr attr in attrs)
                msg += "\t\n" + attr.Field + "" + attr.Name + " 表" + attr.FK_MapData;

            throw new Exception(msg);
        }
        catch (Exception ex)
        {
            this.ToErrorPage(ex.Message);
        }

    }
}
