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

public partial class WF_Admin_TestFlow : WebPage
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
    public void BindFlowList()
    {
        this.Title = this.ToE("TestTitle", "感谢您选择驰骋工作流程引擎-流程设计&测试界面");

        //this.Pub1.Add("<h3>" + this.Title + "</h3>");

        //if (this.FK_Flow == null)
        //{
        //    BP.WF.Flows fls = new BP.WF.Flows();
        //    fls.RetrieveAllFromDBSource();

        //    this.Left.AddFieldSet("List");
        //    FlowSorts fss = new FlowSorts();
        //    fss.RetrieveAllFromDBSource();

        //    string FlowChart = this.ToE("FlowChart", "流程图");
        //    string FlowProperty = this.ToE("FlowProperty", "流程属性");

        //    this.Left.AddUL();
        //    foreach (FlowSort fs in fss)
        //    {
        //        this.Left.Add("<li><b>" + fs.Name + "</b></li>");
        //        foreach (BP.WF.Flow fl in fls)
        //        {
        //            if (fs.No != fl.FK_FlowSort)
        //                continue;

        //            if (fl.No == this.FK_Flow)
        //                this.Left.AddLi("<a href='TestFlow.aspx?FK_Flow=" + fl.No + "&Type=New&Lang=" + WebUser.SysLang + "'><b><font color=green>" + fl.Name + "</b></font></a> - <a href='./../WF/Chart.aspx?FK_Flow=" + fl.No + "&DoType=Chart' >" + FlowChart + "</a> - <a href=\"javascript:WinOpen('../../Comm/UIEn.aspx?EnName=BP.WF.Ext.FlowSheet&PK=" + fl.No + "','s')\" >" + FlowProperty + "</a>");
        //            else
        //                this.Left.AddLi("<a href='TestFlow.aspx?FK_Flow=" + fl.No + "&Type=New&Lang=" + WebUser.SysLang + "'>" + fl.Name + "</a>");
        //        }
        //    }
        //    this.Left.AddULEnd();
        //    this.Left.AddFieldSetEnd();
        //}
        //else
        //{
        //    Flow fl = new Flow(this.FK_Flow);
        //    this.Left.AddFieldSet(fl.Name);
        //    this.Left.AddH3("<a href='./../Chart.aspx?FK_Flow=" + fl.No + "&DoType=Chart'  >" + this.ToE("FlowChart", "流程图") + "</a>");
        //    this.Left.AddH3("<a href=\"javascript:WinOpen('../../Comm/UIEn.aspx?EnName=BP.WF.Ext.FlowSheet&PK=" + fl.No + "','s')\" >" + this.ToE("FlowProperty", "流程属性") + "</a>");
        //    this.Left.AddH3("<a href='TestFlow.aspx?Lang=" + this.Lang + "'>" + this.ToE("AllFlow", "全部流程") + "....</a>");
        //    this.Left.AddFieldSetEnd();
        //}


        //this.Left.AddFieldSet(this.ToE("PortData", "组织结构管理"));
        //this.Left.AddUL();
        //this.Left.AddLi("<a href=\"javascript:WinOpen('../../Comm/Ens.aspx?EnsName=BP.WF.Port.Stations')\">" + this.ToE("Station", "岗位维护") + "</a>");
        //this.Left.AddLi("<a href=\"javascript:WinOpen('../../Comm/Ens.aspx?EnsName=BP.WF.Port.Depts')\">" + this.ToE("Dept", "部门维护") + "</a>");
        //this.Left.AddLi("<a href=\"javascript:WinOpen('../../Comm/Ens.aspx?EnsName=BP.WF.Port.Emps')\">" + this.ToE("Emp", "人员维护") + "</a>");
        //this.Left.AddULEnd();
        //this.Left.AddFieldSetEnd();

        //this.Left.AddFieldSet(this.ToE("Tools", "系统管理"));
        ////this.Left.AddFieldSet("系统工具");
        //this.Left.AddUL();
        //this.Left.AddLi("<a href=\"javascript:WinOpen('../../WF/ClearDatabase.aspx')\">" + this.ToE("ClearDatabase", "清除流程数据") + "</a>");
        //this.Left.AddLi("<a href=\"javascript:WinOpen('../../Comm/Sys/EditWebConfig.aspx')\">" + this.ToE("WebConfig", "系统设置") + "</a>");
        //this.Left.AddLi("<a href=\"javascript:WinOpen('TitleSet.aspx')\">" + this.ToE("TitleImg", "标题图片设置") + "</a>");
        //this.Left.AddULEnd();
        //this.Left.AddFieldSetEnd();

        //this.Left.AddFieldSet("相关下载");
        //this.Left.Add("<a href=http://flow.ccFlow.org/ > http://flow.ccFlow.org/ 相关下载</a>");
        //this.Left.AddBR("<a href=/WF/Login.aspx > 直接登陆 (嵌入方式)</a>");
        //this.Left.AddBR("<a href=/WF/Port/Signin.aspx > 直接登陆 (系统方式)</a>");
        //this.Left.AddFieldSetEnd(); 
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Request.Browser.Cookies == false)
        {
            this.Response.Write("您的浏览器不支持cookies功能，无法使用改系统。");
            return;
        }

        Emp emp1 = new Emp("admin");
        WebUser.SignInOfGenerLang(emp1, this.Lang);

        // this.BindFlowList();
        if (this.FK_Flow == null)
        {
            this.Ucsys1.AddFieldSet("关于流程测试");
            this.Ucsys1.AddUL();
            this.Ucsys1.AddLi("现在是流程测试状态，此功能紧紧提供给流程设计人员使用。");
            this.Ucsys1.AddLi("提供此功能的目的是，快速的让各个角色人员登陆，以便减少登陆的繁琐麻烦。");
            this.Ucsys1.AddLi("点左边的流程列表后，系统自动显示能够发起此流程的工作人员，点一个工作人员就直接登陆了。");
            this.Ucsys1.AddULEnd();
            this.Ucsys1.AddFieldSetEnd();
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
                    this.Response.Redirect("../../WF/WAP/MyFlow.aspx?FK_Flow=" + this.FK_Flow + "&FK_Node=" + int.Parse(this.FK_Flow) + "01", true);
                else
                    this.Response.Redirect("../../WF/MyFlow.aspx?FK_Flow=" + this.FK_Flow + "&FK_Node=" + int.Parse(this.FK_Flow) + "01", true);
            }
            else
            {
                this.Response.Redirect("../Port/Home.htm?FK_Flow=" + this.FK_Flow, true);
            }
            return;
        }

        BP.Web.WebUser.SysLang = this.Lang;
        Flow fl = new Flow(this.FK_Flow);
        fl.DoCheck();

        int nodeid = int.Parse(this.FK_Flow + "01");
        Emps emps = new Emps();
        emps.RetrieveInSQL_Order("select fk_emp from Port_Empstation WHERE fk_station in (select fk_station from WF_NodeStation WHERE FK_Node=" + nodeid + " )", "FK_Dept");

        if (emps.Count == 0)
            emps.RetrieveInSQL("select fk_emp from wf_NodeEmp WHERE fk_node=" + int.Parse(this.FK_Flow + "01") + " ");

        if (emps.Count == 0)
            emps.RetrieveInSQL("select no from port_emp where fk_dept in (select fk_dept from wf_nodeDept where fk_node='"+nodeid+"') ");

        if (emps.Count == 0)
        {
            this.Ucsys1.AddMsgOfWarning("Error",
                this.ToE("StartError", "错误原因 <h2>@1，可能是您没有正确的设置岗位、部门、人员。<br>@2，可能是没有给开始节点设置工作岗位。</h2>"));
            return;
        }


        this.Ucsys1.AddFieldSet(this.ToE("ChoseStarter", "可发起(<font color=red>" + fl.Name + "</font>)流程的人员"));

        this.Ucsys1.AddTable("align=center");
        this.Ucsys1.AddCaptionLeft("流程编号:" + fl.No + " 名称:" + fl.Name + "  - <a href='../Login.aspx'>博客模式直接登陆</a> - <a href='../../AppDemo/Login.aspx' >应用程序模式直接登陆</a>");
        this.Ucsys1.AddTR();
        this.Ucsys1.AddTDTitle("Users");
       // this.Ucsys1.AddTDTitle("ccflow5版");
        this.Ucsys1.AddTDTitle("独立模式");
        this.Ucsys1.AddTDTitle("小窗口模式");
        this.Ucsys1.AddTDTitle("特小窗口模式");
        this.Ucsys1.AddTDTitle("手机模式");
        this.Ucsys1.AddTDTitle("Dept");
        this.Ucsys1.AddTDTitle("SDK");
        this.Ucsys1.AddTREnd();
        bool is1 = false;
        foreach (Emp emp in emps)
        {
            is1 = this.Ucsys1.AddTR(is1);
            this.Ucsys1.AddTD(emp.No + "," + emp.Name);
            // this.Ucsys1.AddTD("<a href='./../Port.aspx?DoWhat=Start5&UserNo=" + emp.No + "&FK_Flow=" + this.FK_Flow + "&Lang=" + BP.Web.WebUser.SysLang + "&Type=" + this.Request.QueryString["Type"] + "'  ><img src='./../Img/IE.gif' border=0 />Internet Explorer</a>");
            this.Ucsys1.AddTD("<a href='./../Port.aspx?DoWhat=Start&UserNo=" + emp.No + "&FK_Flow=" + this.FK_Flow + "&Lang=" + BP.Web.WebUser.SysLang + "&Type=" + this.Request.QueryString["Type"] + "'  ><img src='./../Img/IE.gif' border=0 />Internet Explorer</a>");
            this.Ucsys1.AddTD("<a href='./../Port.aspx?DoWhat=StartSmall&UserNo=" + emp.No + "&FK_Flow=" + this.FK_Flow + "&Lang=" + BP.Web.WebUser.SysLang + "&Type=" + this.Request.QueryString["Type"] + "'  ><img src='./../Img/IE.gif' border=0 />Internet Explorer</a>");
            this.Ucsys1.AddTD("<a href='./../Port.aspx?DoWhat=StartSmallSingle&UserNo=" + emp.No + "&FK_Flow=" + this.FK_Flow + "&Lang=" + BP.Web.WebUser.SysLang + "&Type=" + this.Request.QueryString["Type"] + "'  ><img src='./../Img/IE.gif' border=0 />Internet Explorer</a>");
            // this.Ucsys1.AddTD("<a href='TestFlow.aspx?RefNo=" + emp.No + "&FK_Flow=" + this.FK_Flow + "&Lang=" + BP.Web.WebUser.SysLang + "&Type=" + this.Request.QueryString["Type"] + "&IsWap=1'  ><img src='./../Img/Mobile.gif' border=0 width=25px height=18px />Mobile</a> ");
            this.Ucsys1.AddTD("<a href=\"javascript:WinOpen('TestFlow.aspx?RefNo=" + emp.No + "&FK_Flow=" + this.FK_Flow + "&Lang=" + BP.Web.WebUser.SysLang + "&Type=" + this.Request.QueryString["Type"] + "&IsWap=1','470px','600px','" + emp.No + "');\"  ><img src='./../Img/Mobile.gif' border=0 width=25px height=18px />Mobile</a> ");
            this.Ucsys1.AddTD(emp.FK_DeptText);
            this.Ucsys1.AddTD("<a href='TestSDK.aspx?RefNo=" + emp.No + "&FK_Flow=" + this.FK_Flow + "&Lang=" + BP.Web.WebUser.SysLang + "&Type=" + this.Request.QueryString["Type"] + "&IsWap=1'  >SDK</a> ");
            this.Ucsys1.AddTREnd();
        }
        this.Ucsys1.AddTableEndWithBR();
        this.Ucsys1.AddFieldSetEnd();

        //this.Ucsys1.AddFieldSet(fl.Name);
        //this.Ucsys1.AddUL();
        //Nodes nds = new Nodes(this.FK_Flow);
        //foreach (BP.WF.Node nd in nds)
        //{
        //    this.Ucsys1.AddLi("Step " + nd.Step + " :<a href=\"javascript:WinOpen('../../Comm/UIEn.aspx?EnName=BP.WF.Ext.NodeO&PK=" + nd.NodeID + "')\">" + nd.Name + "</a>, <a href=\"javascript:WinOpen('../MapDef/MapDef.aspx?PK=ND" + nd.NodeID + "')\">" + this.ToE("DNode", "设计表单") + "</a>");
        //}
        //this.Ucsys1.AddULEnd();
        //this.Ucsys1.AddFieldSetEnd();
        // this.Ucsys1.Add("<a href='../../DataUser/FlowDesc/" + this.FK_Flow + ".gif' target=_blank><img border=0 src='../../DataUser/FlowDesc/" + this.FK_Flow + ".gif' width=300px height=300px ></a>");
    }
}
