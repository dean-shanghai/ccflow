using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.WF;
using BP.En;
using BP.Port;
using BP.Web.Controls;
using BP.Web;
using BP.Sys;

public partial class WF_Admin_StartFlow : WebPage
{
    public string FK_Flow
    {
        get
        {
            return this.Request.QueryString["FK_Flow"];
        }
    }
    public string Lang
    {
        get
        {
            return this.Request.QueryString["Lang"];
        }
    }
    public void BindHelp()
    {
        this.Pub2.AddFieldSet("测试帮助");

        this.Pub2.Add(BP.DA.DataType.ReadTextFile2Html(BP.SystemConfig.PathOfWebApp + @"\\WF\\Admin\\Help\\TestHelp_CN.txt"));
        
        this.Pub2.AddFieldSetEnd();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        this.BindHelp();

        if (this.Request.Browser.Cookies == false)
        {
            this.Response.Write("您的浏览器不支持cookies功能，无法使用改系统。");
            return;
        }

        Emp emp1 = new Emp("admin");
        WebUser.SignInOfGenerLang(emp1, this.Lang);

        //   this.BindFlowList();
        if (this.FK_Flow == null)
        {
            this.Pub1.AddFieldSet("关于流程测试");

            this.Pub1.AddUL();
            this.Pub1.AddLi("现在是流程测试状态，此功能紧紧提供给流程设计人员使用。");
            this.Pub1.AddLi("提供此功能的目的是，快速的让各个角色人员登陆，以便减少登陆的繁琐麻烦。");
            this.Pub1.AddLi("点左边的流程列表后，系统自动显示能够发起此流程的工作人员，点一个工作人员就直接登陆了。");
            this.Pub1.AddULEnd();
            this.Pub1.AddFieldSetEnd();
            return;
        }

        if (this.RefNo != null)
        {
            Emp emp = new Emp(this.RefNo);
            BP.Web.WebUser.SignInOfGenerLang(emp, this.Lang);
            this.Session["FK_Flow"] = this.FK_Flow;
            if (this.Request.QueryString["Type"] != null)
            {
                string url = "../../WAP/MyFlow.aspx?FK_Flow=" + this.FK_Flow;
                if (this.Request.QueryString["IsWap"] == "1")
                {
                    //  this.Response.Write("<script  language=javascript>  window.open( '" + url + "' , 'ass' ,'width=50,top=50,left=50,height=20,scrollbars=yes,resizable=yes,toolbar=false,location=false') </script>");
                    this.Response.Redirect("../../WAP/MyFlow.aspx?FK_Flow=" + this.FK_Flow, true);
                }
                else
                    this.Response.Redirect("../../WF/MyFlow.aspx?FK_Flow=" + this.FK_Flow, true);
            }
            else
            {
                this.Response.Redirect("../Port/Home.htm?FK_Flow=" + this.FK_Flow, true);
            }
            return;
        }

        BP.Web.WebUser.SysLang = this.Lang;

        Flow fl = new Flow(this.FK_Flow);
        //fl.DoCheck();

        int nodeid = int.Parse(this.FK_Flow + "01");
        Emps emps = new Emps();
        emps.RetrieveInSQL_Order("select fk_emp from Port_Empstation WHERE fk_station in (select fk_station from WF_NodeStation WHERE FK_Node=" + nodeid + " )", "FK_Dept");

        if (emps.Count == 0)
            emps.RetrieveInSQL("select fk_emp from wf_NodeEmp WHERE fk_node=" + int.Parse(this.FK_Flow + "01") + " ");

        if (emps.Count == 0)
        {
            this.Pub1.AddMsgOfWarning("Error",
                this.ToE("StartError", "错误原因 <br>@1，可能是您没有正确的设置岗位、部门、人员。<br>@2，可能是没有给开始节点设置工作岗位。。"));
            return;
        }

        this.Pub1.AddFieldSet(this.ToE("ChoseStarter", "可发起(<font color=red>" + fl.Name + "</font>)流程的人员，<a href=\"javascript:WinOpen('./../../Comm/UIEn.aspx?EnName=BP.WF.Ext.FlowSheet&No=" + this.FK_Flow + "');\">流程属性</a>."));

        this.Pub1.AddTable("border=0");
        this.Pub1.AddTR();
        this.Pub1.AddTDTitle("IDX");
        this.Pub1.AddTDTitle("Users");
        this.Pub1.AddTDTitle("独立模式");
        this.Pub1.AddTDTitle("调用模式");
        //  this.Pub1.AddTDTitle("特小窗口模式");
        this.Pub1.AddTDTitle("手机模式");
        this.Pub1.AddTDTitle("部门");
        this.Pub1.AddTDTitle("调用SDK");
        this.Pub1.AddTREnd();
        bool is1 = false;
        int idx = 0;
        foreach (Emp emp in emps)
        {
            is1 = this.Pub1.AddTR(is1);
            idx++;
            this.Pub1.AddTDIdx(idx);
            this.Pub1.AddTD(emp.No + "," + emp.Name);
            this.Pub1.AddTD("<a href='./../Port.aspx?DoWhat=Start&UserNo=" + emp.No + "&FK_Flow=" + this.FK_Flow + "&Lang=" + BP.Web.WebUser.SysLang + "&Type=" + this.Request.QueryString["Type"] + "'  ><img src='./../Img/IE.gif' border=0 />IE独立运行</a>");
            this.Pub1.AddTD("<a href='./../Port.aspx?DoWhat=StartSmall&UserNo=" + emp.No + "&FK_Flow=" + this.FK_Flow + "&Lang=" + BP.Web.WebUser.SysLang + "&Type=" + this.Request.QueryString["Type"] + "'  ><img src='./../Img/IE.gif' border=0 />IE调用模式</a>");
            this.Pub1.AddTD("<a href='TestFlow.aspx?RefNo=" + emp.No + "&FK_Flow=" + this.FK_Flow + "&Lang=" + BP.Web.WebUser.SysLang + "&Type=" + this.Request.QueryString["Type"] + "&IsWap=1'  ><img src='./../Img/Mobile.gif' border=0 width=25px height=18px />Mobile</a> ");
            this.Pub1.AddTD(emp.FK_DeptText);
            this.Pub1.AddTD("<a href='TestSDK.aspx?RefNo=" + emp.No + "&FK_Flow=" + this.FK_Flow + "&Lang=" + BP.Web.WebUser.SysLang + "&Type=" + this.Request.QueryString["Type"] + "&IsWap=1'  >SDK</a> ");
            this.Pub1.AddTREnd();
        }
        this.Pub1.AddTableEnd();
        this.Pub1.AddFieldSetEnd();
    }
}