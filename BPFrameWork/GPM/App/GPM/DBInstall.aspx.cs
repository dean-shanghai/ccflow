using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.Sys;

public partial class WF_Admin_DBInstall : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Pub1.AddH3("GPM 数据库修复与安装工具");
        this.Pub1.AddHR();
        if (this.Request.QueryString["DoType"] == "OK")
        {
            this.Pub1.AddFieldSet("提示");
            this.Pub1.Add("GPM数据库初始化成功.");
            this.Pub1.AddBR("<a href='../Port/Signin.aspx' >登陆 admin 初试化密码是pub .</a>");
            this.Pub1.AddFieldSetEnd();
            return;
        }

        if (BP.DA.DBAccess.IsExitsObject("Port_Emp") == true)
        {
            this.Pub1.AddFieldSet("提示");
            this.Pub1.Add("数据已经安装，如果您要重新安装，您需要手工的清除数据库里对象。");
            this.Pub1.AddFieldSetEnd();

            this.Pub1.AddFieldSet("修复数据表");
            this.Pub1.Add("把最新的版本的与当前的数据表结构，做一个自动修复, 修复内容：缺少列，缺少列注释，列注释不完整或者有变化。 <br> <a href='DBInstall.aspx?DoType=FixDB' >执行...</a>。");
            this.Pub1.AddFieldSetEnd();

            if (this.Request.QueryString["DoType"] == "FixDB")
            {
                string rpt = BP.PubClass.DBRpt(BP.DBLevel.High);

                this.Pub1.AddMsgGreen("同步数据表结构成功, 部分错误不会影响系统运行.",
                    "执行成功，希望在系统每次升级后执行此功能，不会对你的数据库数据产生影响。<br> <a href='./XAP/Designer.aspx'>进入流程设计器.</a>");
            }
            return;
        }

        // this.Pub1.AddH2("数据库安装向导...");
        RadioButton rb = new RadioButton();

        //this.Pub1.AddFieldSet("选择安装语言.");
        //BP.WF.XML.Langs langs = new BP.WF.XML.Langs();
        //langs.RetrieveAll();
        //RadioButton rb = new RadioButton();
        //foreach (BP.WF.XML.Lang lang in langs)
        //{
        //    rb = new RadioButton();
        //    rb.Text = lang.Name;
        //    rb.ID = "RB_" + lang.No;
        //    rb.GroupName = "ch";
        //    this.Pub1.Add(rb);
        //    this.Pub1.AddBR();
        //}
        //this.Pub1.GetRadioButtonByID("RB_CH").Checked = true;
        //this.Pub1.AddFieldSetEndBR();



        this.Pub1.AddFieldSet("选择数据库安装类型.");
        rb = new RadioButton();
        rb.Text = "SQLServer2000,2005,2008";
        rb.ID = "MSSQL";
        if (BP.SystemConfig.AppCenterDBType == BP.DA.DBType.SQL2000)
            rb.Checked = true;

        rb.GroupName = "sd";
        rb.Checked = true;
        this.Pub1.Add(rb);
        this.Pub1.AddBR();

        rb = new RadioButton();
        rb.Text = "Oracle9i,Oracle 10g";
        rb.ID = "RB_Oracle";
        rb.GroupName = "sd";
        if (BP.SystemConfig.AppCenterDBType == BP.DA.DBType.Oracle9i)
            rb.Checked = true;
        this.Pub1.Add(rb);
        this.Pub1.AddBR();

        rb = new RadioButton();
        rb.Text = "DB2";
        rb.ID = "RB_DB2";
        rb.GroupName = "sd";
        if (BP.SystemConfig.AppCenterDBType == BP.DA.DBType.DB2)
            rb.Checked = true;
        this.Pub1.Add(rb);
        this.Pub1.AddBR();

        rb = new RadioButton();
        rb.Text = "MySQL";
        rb.ID = "RB_MYSQL";
        rb.GroupName = "sd";
        if (BP.SystemConfig.AppCenterDBType == BP.DA.DBType.MySQL)
            rb.Checked = true;
        this.Pub1.Add(rb);
        this.Pub1.AddBR();
        this.Pub1.AddFieldSetEnd();


        this.Pub1.AddFieldSet("应用环境模拟.");
        rb = new RadioButton();
        rb.Text = "集团公司，企业单位。";
        rb.ID = "RB_Inc";
        rb.GroupName = "hj";
        rb.Checked = true;
        this.Pub1.Add(rb);
        this.Pub1.AddBR();
        rb = new RadioButton();
        rb.Text = "政府机关，事业单位。";
        rb.ID = "RB_Gov";
        rb.GroupName = "hj";
        this.Pub1.Add(rb);
        this.Pub1.AddBR();
        this.Pub1.AddFieldSetEndBR();

        //this.Pub1.AddFieldSet("是否装载演示流程模板?");
        //rb = new RadioButton();
        //rb.Text = "是:我要安装demo流程模板、表单模板，以方便我学习ccflow与ccform.";
        //rb.ID = "RB_DemoOn";
        //rb.GroupName = "hjd";
        //rb.Checked = true;
        //this.Pub1.Add(rb);
        //this.Pub1.AddBR();
        //rb = new RadioButton();
        //rb.Text = "否:不安装。";
        //rb.ID = "RB_DemoOff";
        //rb.GroupName = "hjd";
        //this.Pub1.Add(rb);
        //this.Pub1.AddBR();
        //this.Pub1.AddFieldSetEndBR();

        Button btn = new Button();
        btn.ID = "Btn_s";
        btn.Text = "下一步";
        btn.UseSubmitBehavior = false;
        btn.OnClientClick = "this.disabled=true;";
        btn.Click += new EventHandler(btn_Click);
        this.Pub1.Add(btn);
    }
    void btn_Click(object sender, EventArgs e)
    {
        string lang = "CH";
        string hj = "Inc";

        //if (this.Pub1.GetRadioButtonByID("RB_SQL").Checked)
        //    db = "SQLServer";

        //if (this.Pub1.GetRadioButtonByID("RB_Oracle").Checked)
        //    db = "Oracle";

        //if (this.Pub1.GetRadioButtonByID("RB_DB2").Checked)
        //    db = "DB2";

        //if (this.Pub1.GetRadioButtonByID("RB_MYSQL").Checked)
        //    db = "MySQL";

        //BP.WF.XML.Langs langs = new BP.WF.XML.Langs();
        //langs.RetrieveAll();
        //foreach (BP.WF.XML.Lang xml in langs)
        //{
        //    if (this.Pub1.GetRadioButtonByID("RB_" + xml.No).Checked)
        //        lang = xml.No;
        //}

        if (this.Pub1.GetRadioButtonByID("RB_Inc").Checked)
            hj = "Inc";

        if (this.Pub1.GetRadioButtonByID("RB_Gov").Checked)
            hj = "Gov";

        hj = "Inc";
        //运行。
        BP.GPM.Glo.DoInstallDataBase(lang, hj);

        //加注释.
        BP.PubClass.AddComment();

        //// 装载演示数据.
        //if (this.Pub1.GetRadioButtonByID("RB_DemoOn").Checked)
        //{
        //    BP.WF.DTS.LoadTemplete l = new BP.WF.DTS.LoadTemplete();
        //    l.Do();
        //}
     

        //try
        //{
        //    //增加图片签名
        //    BP.WF.DTS.GenerSiganture gs = new BP.WF.DTS.GenerSiganture();
        //    gs.Do();
        //}
        //catch
        //{
        //}

      
        this.Response.Redirect("DBInstall.aspx?DoType=OK", true);
    }
}