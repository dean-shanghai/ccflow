using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using BP.DA;
using BP.Sys;
using BP.Web;
using BP.En;
using BP.WF;
using BP.Port;
using Silverlight.DataSetConnector;
using System.Drawing.Imaging;
using System.Drawing;
using System.Configuration;

/// <summary>
///ccflowAPI 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
// [System.Web.Script.Services.ScriptService]
public class CCFlowAPI : CCForm {


    public CCFlowAPI()
    {
        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }
     
    /// <summary>
    /// 获取当前操作员可以发起的流程集合
    /// </summary>
    /// <param name="userNo">人员编号</param>
    /// <returns>可以发起的xml</returns>
    [WebMethod(EnableSession = true)]
    public string DB_GenerCanStartFlowsOfDataTable(string userNo)
    {
        System.Data.DataSet ds = new System.Data.DataSet();
        ds.Tables.Add(BP.WF.Dev2Interface.DB_GenerCanStartFlowsOfDataTable(userNo));
        return Connector.ToXml(ds);
    }
    /// <summary>
    /// 待办提示
    /// </summary>
    /// <param name="userNo"></param>
    /// <returns></returns>
    [WebMethod]
    public string AlertString(string userNo)
    {
        return "@EmpWorks=12@CC=34";
    }
    /// <summary>
    /// 用户登录
    /// 0,密码用户名错误
    /// 1,成功.
    /// 2,服务器错误.
    /// </summary>
    /// <param name="userNo"></param>
    /// <param name="pass"></param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public int Port_Login(string userNo, string pass)
    {
        try
        {
            Emp emp = new Emp(userNo);
            if (emp.Pass.Equals(pass) == false)
                return 0;

            BP.WF.Dev2Interface.Port_Login(userNo, "");
            return 1;
        }
        catch(Exception ex)
        {
            Log.DefaultLogWriteLineError(ex.Message);
            return 2;
        }
    }
    /// <summary>
    /// 获取一条待办工作
    /// </summary>
    /// <param name="fk_flow">工作编号</param>
    /// <param name="fk_node">节点编号</param>
    /// <param name="workID">工作ID</param>
    /// <param name="userNo">操作员编号</param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public string GenerWorkNode(string fk_flow, int fk_node, Int64 workID, string userNo)
    {
        //if (Dev2Interface.Flow_CheckIsCanDoCurrentWork(workID, userNo) == false)
        //    throw new Exception("您没有处理当前工作的权限。");

        Emp emp = new Emp(userNo);
        BP.Web.WebUser.No = emp.No;
        BP.Web.WebUser.Name = emp.Name;
        BP.Web.WebUser.FK_Dept = emp.FK_Dept;
        BP.Web.WebUser.FK_DeptName = emp.FK_DeptText;

        MapData md = new MapData();
        md.No = "ND" + fk_node;
        if (md.RetrieveFromDBSources() == 0)
            throw new Exception("装载错误，该表单ID=" + md.No + "丢失，请修复一次流程重新加载一次.");
        DataSet myds = md.GenerHisDataSet();

        // 节点数据.
        Node nd = new Node(fk_node);
        myds.Tables.Add(nd.ToDataTableField("WF_Node"));

        //节点标签数据.
        BtnLab btnLab = new BtnLab(fk_node);
        myds.Tables.Add(btnLab.ToDataTableField("WF_BtnLab"));

        // 流程数据.
        Flow fl = new Flow(fk_flow);
        myds.Tables.Add(fl.ToDataTableField("WF_Flow"));

        //.工作数据放里面去.
        BP.WF.Work wk = nd.HisWork;
        wk.OID = workID;
        wk.RetrieveFromDBSources();
        myds.Tables.Add(wk.ToDataTableField("Main"));

#warning 还有从表一对多，附件...

        myds.WriteXml("c:\\sss.xml");
        return Connector.ToXml(myds);
    }
    /// <summary>
    /// 获取一条待办工作
    /// </summary>
    /// <param name="fk_flow">工作编号</param>
    /// <param name="fk_node">节点编号</param>
    /// <param name="workID">工作ID</param>
    /// <param name="userNo">操作员编号</param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public string Node_SaveWork(string fk_flow, int fk_node, Int64 workID, string userNo, string dsXml)
    {
        try
        {
            Emp emp = new Emp(userNo);
            BP.Web.WebUser.No = emp.No;
            BP.Web.WebUser.Name = emp.Name;
            BP.Web.WebUser.FK_Dept = emp.FK_Dept;
            BP.Web.WebUser.FK_DeptName = emp.FK_DeptText;

            StringReader sr = new StringReader(dsXml);
            DataSet ds = new DataSet();
            ds.ReadXml(sr);

            Hashtable htMain = new Hashtable();
            DataTable dtMain = ds.Tables["Main"];
            foreach (DataRow dr in dtMain.Rows)
            {
                htMain.Add(dr[0].ToString(), dr[1].ToString());
            }
            return BP.WF.Dev2Interface.Node_SaveWork(fk_flow, workID, htMain, ds);
        }
        catch(Exception ex)
        {
            return "@保存工作出现错误:" + ex.Message;
        }
    }
    /// <summary>
    /// 执行发送
    /// </summary>
    /// <param name="fk_flow"></param>
    /// <param name="fk_node"></param>
    /// <param name="workID"></param>
    /// <param name="userNo"></param>
    /// <param name="dsXml"></param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public string Node_SendWork(string fk_flow, int fk_node, Int64 workID, string userNo, string dsXml)
    {
        try
        {
            Emp emp = new Emp(userNo);
            BP.Web.WebUser.No = emp.No;
            BP.Web.WebUser.Name = emp.Name;
            BP.Web.WebUser.FK_Dept = emp.FK_Dept;
            BP.Web.WebUser.FK_DeptName = emp.FK_DeptText;

            StringReader sr = new StringReader(dsXml);
            DataSet ds = new DataSet();
            ds.ReadXml(sr);

            Hashtable htMain = new Hashtable();
            DataTable dtMain = ds.Tables["Main"];
            foreach (DataRow dr in dtMain.Rows)
            {
                htMain.Add(dr[0].ToString(), dr[1].ToString());
            }
            return BP.WF.Dev2Interface.Node_SendWork(fk_flow, workID, htMain, ds).ToMsgOfHtml();
            //return "保存成功";
        }
        catch (Exception ex)
        {
            return "@发送工作出现错误:" + ex.Message;
        }
    }
}
