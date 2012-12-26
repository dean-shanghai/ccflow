using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.Port;
using BP.Sys;
using BP.DA;
using BP.WF;

public partial class Designer : System.Web.UI.Page
{
    public bool IsCheckUpdate
    {
        get
        {
            if (this.Request.QueryString["IsCheckUpdate"] == null)
                return false;
            return true;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //如果没有Port_Dept 表就可能没有安装ccflow.
            DBAccess.RunSQL("SELECT * FROM Port_Dept where 1=2");
        }
        catch
        {
            this.Response.Redirect("../DBInstall.aspx", true);
            return;
        }

        if (this.IsCheckUpdate == false)
        {
            #region 执行admin登陆.
            Emp emp = new Emp();
            emp.No = "admin";
            if (emp.RetrieveFromDBSources() == 1)
            {
                BP.Web.WebUser.SignInOfGener(emp, true);
            }
            else
            {
                emp.No = "admin";
                emp.Name = "admin";
                emp.FK_Dept = "01";
                emp.Pass = "pub";
                emp.Insert();
                BP.Web.WebUser.SignInOfGener(emp, true);
                //throw new Exception("admin 用户丢失，请注意大小写。");
            }
            #endregion 执行admin登陆.
            return;
        }

        string sql = "";
        string msg = "";
        try
        {
            msg = "@在检查数据库连接出现错误。";

            #region 2012-12-21 升级退回规则, 增加按轨迹图设置退回.
            DBAccess.RunSQL("DELETE FROM Sys_Enum WHERE EnumKey='ReturnRole'");
            #endregion

            #region 2012-12-15 升级退回规则. for boco.
            Direction dir = new Direction();
            dir.CheckPhysicsTable();
            try
            {
                sql = "UPDATE WF_Direction SET DirType=0  WHERE DirType is null ";
                sql += "@UPDATE WF_Direction SET  IsCanBack=0 WHERE IsCanBack IS NULL ";
                sql += "@UPDATE WF_Direction SET FK_Flow = (SELECT FK_FLOW FROM WF_Node WHERE NodeID=WF_Direction.Node)";
                if (BP.SystemConfig.AppCenterDBType == DBType.Informix)
                    sql += "@UPDATE WF_Direction SET MyPK =  to_char(Node) || '_' || to_char(ToNode) || '_' || to_char(DirType)";
                else
                    sql += "@UPDATE WF_Direction SET MyPK = CAST(Node as varchar)+'_'+CAST(ToNode as varchar)+'_'+CAST(DirType as varchar)";
                DBAccess.RunSQLs(sql);
            }
            catch
            {
            }
            #endregion 

            #region 2012-12-06 升级投递规则 与退回规则. for 亿阳信通.

            GenerWorkerList wl = new GenerWorkerList();
            wl.CheckPhysicsTable();

            Flow fl = new Flow();
            fl.CheckPhysicsTable();

            sql = " UPDATE WF_GenerWorkerList SET PressTimes=0 WHERE PressTimes IS NULL";
            DBAccess.RunSQL(sql);

            sql = " DELETE FROM Sys_Enum WHERE enumkey='DeliveryWay'";
            DBAccess.RunSQL(sql);

            sql = " DELETE FROM Sys_Enum WHERE enumkey='ReturnRole'";
            DBAccess.RunSQL(sql);
            #endregion 升级投递规则.

            #region 更新 WF_EmpWorks. 2012-12-08  2011-11-09 , 2011-11-30
            msg = "@更新视图出现错误。";
            try
            {
                GenerFH fh = new GenerFH();
                fh.CheckPhysicsTable();

                GenerWorkFlow gwf = new GenerWorkFlow();
                gwf.CheckPhysicsTable();

                GenerWorkerList gwls = new GenerWorkerList();
                gwls.CheckPhysicsTable();

                sql = "DROP VIEW WF_EmpWorks";
                BP.DA.DBAccess.RunSQLs(sql);
            }
            catch
            {
            }
            sql = "CREATE VIEW  WF_EmpWorks AS SELECT A.PRI, A.WorkID, B.IsRead, A.Rec AS Starter, A.RecName as StarterName, A.WFState, A.FK_Dept,A.DeptName, A.FK_Flow, A.FlowName,B.FK_Node, B.FK_NodeText AS NodeName, A.Title, A.RDT, a.SDTOfFlow, B.RDT AS ADT, B.SDT, B.FK_Emp,B.FK_EmpText, B.FID ,A.FK_FlowSort,B.PressTimes FROM  WF_GenerWorkFlow A, WF_GenerWorkerList B WHERE (B.IsEnable = 1) AND (B.IsPass = 0) AND A.WorkID = B.WorkID AND A.FK_Node = B.FK_Node ";
            BP.DA.DBAccess.RunSQLs(sql);

            // 更新老版本的字段长度。
            switch (DBAccess.AppCenterDBType)
            {
                case DBType.Oracle9i:
                case DBType.Informix:
                case DBType.MySQL:
                    sql = "ALTER TABLE WF_Track modify RDT varchar(20)";
                    break;
                case DBType.SQL2000:
                default:
                    sql = "ALTER TABLE WF_Track ALTER COLUMN RDT varchar(20)";
                    break;
            }
            BP.DA.DBAccess.RunSQLs(sql);
            #endregion 更新 WF_EmpWorks. 2011-11-09

            #region 增加是否只读.
            BP.WF.GenerWorkerList wl8 = new GenerWorkerList();
            wl8.CheckPhysicsTable();

            sql = " UPDATE WF_GenerWorkerList SET IsRead=0 WHERE IsRead IS NULL";
            DBAccess.RunSQL(sql);
            #endregion 增加是否只读.

            #region 2012-11-22 增加挂起状态.
            sql = " DELETE FROM Sys_Enum WHERE enumkey='WFState'";
            DBAccess.RunSQL(sql);
            #endregion

            #region 2012-11-17  去了开始节点的WFState 字段.
            sql = "DELETE SYS_MapAttr WHERE KeyOfEn='WFState' AND (FK_MapData LIKE 'ND%' AND FK_MapData LIKE '%01') ";
            DBAccess.RunSQL(sql);
            #endregion

            #region 2012-11-10
            BP.WF.Flow.RepareV_FlowData_View();

            // 增加了登录日志.
            UserLog ul = new UserLog();
            ul.CheckPhysicsTable();
            #endregion

            #region 升级 09-24 增加临时的trackTemp 表.
            TrackTemp tmp = new TrackTemp();
            tmp.CheckPhysicsTable();

            #endregion 升级 09-24

            #region 升级 07-01
            GenerWorkFlow gwfss = new GenerWorkFlow();
            gwfss.CheckPhysicsTable();
            #endregion  升级 07-01

            #region 升级 06-12
            // 升级抄送. 
            CCList cl = new CCList();
            cl.CheckPhysicsTable();
            #endregion 升级 06-12

            #region 升级退回规则 05-10
            ReturnWork rw1 = new ReturnWork();
            rw1.CheckPhysicsTable();
            #endregion 

            #region 升级用户禁用. 2012-06-19
            //int num = DBAccess.RunSQLReturnValInt("SELECT COUNT(*) FROM WF_Emp WHERE UseSta=0");
            //int num1 = DBAccess.RunSQLReturnValInt("SELECT COUNT(*) FROM WF_Emp");
            //if (num == num1)
            //    DBAccess.RunSQL("UPDATE WF_Emp SET UseSta=1");
            #endregion 升级用户禁用

            #region 测试数据库是否连接成功。
            try
            {
                if (DBAccess.IsExitsObject("WF_FlowSort") == false)
                {
                    /*如果是没有安装.*/
                    this.Response.Redirect("../DBInstall.aspx", true);
                    return;
                }
            }
            catch
            {
                if (DBAccess.IsExitsObject("WF_FlowSort") == false)
                {
                    /*如果是没有安装.*/
                    this.Response.Redirect("../DBInstall.aspx", true);
                    return;
                }
            }

            #endregion 测试数据库是否连接成功。

            #region 升级 Informix
            try
            {
                DBAccess.RunSQLReturnTable("SELECT MyPK FROM WF_RememberMe WHERE 1=2");
            }
            catch
            {
                DBAccess.RunSQL("DROP TABLE WF_RememberMe ");
                RememberMe me = new RememberMe();
                me.CheckPhysicsTable();
            }
            #endregion

            #region 修改Title在自由表单设计器中不能编辑。 5-15.
            DBAccess.RunSQL("UPDATE Sys_MapAttr SET EditType=0,UIVISIBLE=0 WHERE KeyOfEn='Title'");
            DBAccess.RunSQL("UPDATE Sys_MapDtl SET DtlOpenType=1 WHERE DtlOpenType=0");
            #endregion

            #region 升级转发功能 2012-05-05
            CCList cc = new CCList();
            cc.CheckPhysicsTable();
            #endregion

            #region 升级表单 2012-03-21
            BP.TA.SMS sms = new BP.TA.SMS();
            sms.CheckPhysicsTable();
                 
            FrmEle ele = new FrmEle();
            ele.CheckPhysicsTable();
            FrmEleDB eleDB = new FrmEleDB();
            eleDB.CheckPhysicsTable();
            #endregion

            #region 升级 DtlShowModel 2012-02-27
            DBAccess.RunSQL("DELETE FROM Sys_Enum WHERE EnumKey='DtlShowModel'");
            DBAccess.RunSQL("DELETE FROM Sys_MapExt where MyPK!= FK_MapData+'_'+ExtType and ExtType= 'PageLoadFull'");
            #endregion

            #region 升级 CCstaff
            sql ="CREATE  PROCEDURE CCstaff ";
            sql+="\t\n (@Sender nvarchar(100),	@Receivers nvarchar(2000),	@Title nvarchar(100),@Context nvarchar(max))  ";
            sql+="\t\n  AS ";
            sql+="\t\n  BEGIN ";
            sql+="\t\n  SET NOCOUNT ON; ";
            sql+="\t\n  /* 消息发送接口:";
            sql+="\t\n  ccflow 产生的消息通过这个存储过程接口传导到您需要处理它的程序中去。";
            sql+="\t\n  参数如下:";
            sql+="\t\n  ---------------------------------------";
            sql+="\t\n  @Sender - 发送人";
            sql+="\t\n  @Receivers 接受人，可以用逗号分开.";
            sql+="\t\n  @Title -消息的标题";
            sql+="\t\n  @Context - 内容";
            sql+="\t\n  */ ";
            sql += "\t\n  END ";
            try
            {
                /*如果有了就不执行进入，没有找到如何 判断多种数据库类型的sql语句，暂时用此方法处理。*/
                DBAccess.RunSQL(sql);
            }
            catch
            {
            }
            #endregion 升级 CCstaff

            #region 升级 chofFlow
            try
            {
                CHOfFlow ch = new CHOfFlow();
                ch.CheckPhysicsTable();

                BP.WF.Ext.NodeO nd = new BP.WF.Ext.NodeO();
                nd.CheckPhysicsTable();
                DBAccess.RunSQL("UPDATE WF_Node SET WhoExeIt=0 WHERE WhoExeIt IS NULL");
                DBAccess.RunSQL("UPDATE WF_GenerWorkerlist SET WhoExeIt=0 WHERE WhoExeIt IS NULL");
            }
            catch
            {
            }
            #endregion

            #region 2012- 01-29 增加字段。
            try
            {
                DBAccess.RunSQL("UPDATE WF_Flow SET FlowSheetType=0");
            }
            catch
            {
            }
            #endregion
            
            #region 2012- 01-29 增加字段。
            try
            {
                DBAccess.RunSQLReturnTable("SELECT TAG FROM Sys_mapdata where 1=2 ");
            }
            catch
            {
                MapData md = new MapData();
                md.CheckPhysicsTable();
            }
            #endregion

            #region 2012- 01-18 增加一个view. 
            try
            {
                try
                {
                    BP.DA.DBAccess.RunSQL("DROP VIEW WF_Track_NYR");
                }
                catch
                {
                }

                if (BP.SystemConfig.AppCenterDBType == DBType.Oracle9i)
                {
                    sql = "  CREATE  VIEW WF_Track_NYR AS SELECT EmpFrom as FK_Emp,SUBSTR(RDT,0,8) AS FK_NY, SUBSTR(RDT,0,11) AS RDT,  COUNT(*) AS Num FROM  WF_Track GROUP BY EmpFrom ,SUBSTR(RDT,0,8) , SUBSTR(RDT,0,11) ";
                    sql += " UNION ";
                    sql += "  SELECT EmpFrom as FK_Emp,SUBSTR(RDT,0,8) AS FK_NY, SUBSTR(RDT,0,11) AS RDT,  COUNT(*) AS Num FROM  WF_TrackTemp GROUP BY EmpFrom ,SUBSTR(RDT,0,8) , SUBSTR(RDT,0,11) ";
                }
                else
                {
                    sql = "  CREATE  VIEW WF_Track_NYR AS SELECT EmpFrom as FK_Emp,SUBSTRING(RDT,0,8) AS FK_NY, SUBSTRING(RDT,0,11) AS RDT,  COUNT(*) AS Num FROM  WF_Track GROUP BY EmpFrom ,SUBSTRING(RDT,0,8) , SUBSTRING(RDT,0,11) ";
                    sql += "  UNION ";
                    sql += "  SELECT EmpFrom as FK_Emp,SUBSTRING(RDT,0,8) AS FK_NY, SUBSTRING(RDT,0,11) AS RDT,  COUNT(*) AS Num FROM  WF_TrackTemp GROUP BY EmpFrom ,SUBSTRING(RDT,0,8) , SUBSTRING(RDT,0,11) ";
                }
                BP.DA.DBAccess.RunSQL(sql);
            }
            catch
            {
            }
            #endregion

            #region 2012-01-17 修复表单
            try
            {
                DBAccess.RunSQL("UPDATE Sys_FrmAttachment SET NoOfObj =NoOfAth WHERE (NoOfObj IS NULL OR NoOfObj='')");
                DBAccess.RunSQL("UPDATE Sys_FrmAttachment SET NOOfObj=NoOfAth where noofobj is null");
            }
            catch
            {
            }
            #endregion

            #region 2012-修复表单
            DBAccess.RunSQL("UPDATE sys_mapdata SET FrmW=900  where FrmW IS NULL");
            DBAccess.RunSQL("UPDATE sys_mapdata SET FrmH=1000 where FrmH IS NULL");
            #endregion

            #region 2011-12-29 升级表单元素.
            try
            {
                DataTable dt = DBAccess.RunSQLReturnTable("SELECT No FROM Sys_MapFrame");
                if (dt.Rows.Count == 0)
                {
                    DBAccess.RunSQL("DROP TABLE Sys_MapFrame");
                    MapFrame mf = new MapFrame();
                    mf.CheckPhysicsTable();
                }

                dt = DBAccess.RunSQLReturnTable("SELECT No FROM Sys_MapM2M");
                if (dt.Rows.Count == 0)
                {
                    DBAccess.RunSQL("DROP TABLE Sys_MapM2M");
                    MapM2M m2m = new MapM2M();
                    m2m.CheckPhysicsTable();
                }

                dt = DBAccess.RunSQLReturnTable("SELECT No FROM Sys_FrmAttachment");
                if (dt.Rows.Count == 0)
                {
                    DBAccess.RunSQL("DROP TABLE Sys_FrmAttachment");
                    FrmAttachment ath = new FrmAttachment();
                    ath.CheckPhysicsTable();
                }
            }
            catch
            {

            }
            #endregion
            
            #region 2011-12-21 升级节点属性规则.
            DBAccess.RunSQL("DELETE FROM Sys_Enum WHERE EnumKey='RunModel'");
            DBAccess.RunSQL("DELETE FROM Sys_Enum WHERE EnumKey='FlowRunWay'");
            #endregion

            #region 2011-12-15 处理更新错误.
            msg = "@执行升级出现错误。"; 
            //保障升级后的数据完整性. 2011-11-26.
            sql = "UPDATE SYS_MAPEXT SET ExtType='TBFullCtrl' WHERE ExtType='FullCtrl'";
            DBAccess.RunSQL(sql);

            sql = "UPDATE WF_GenerWorkFlow SET NodeName=(SELECT NAME FROM WF_Node WHERE WF_Node.NodeID=WF_GenerWorkFlow.FK_Node)";
            DBAccess.RunSQL(sql);
            #endregion

            #region 2011-12-13 删除了流程日志字段.
            DBAccess.RunSQL("DELETE FROM Sys_MapAttr WHERE KeyOfEn='WFLog'");
            #endregion

            #region 2011-12-01 升级访问规则.
            DBAccess.RunSQL("DELETE FROM Sys_Enum WHERE EnumKey='DeliveryWay'");
            DBAccess.RunSQL("DELETE FROM Sys_Enum WHERE EnumKey='FrmEventType'");
            DBAccess.RunSQL("DELETE FROM Sys_Enum WHERE EnumKey='EventDoType'");
            #endregion

            #region 手动升级. 2011-07-08 补充节点字段分组.
            //string sql = "DELETE FROM Sys_EnCfg WHERE No='BP.WF.Ext.NodeO'";
            //BP.DA.DBAccess.RunSQL(sql);
            //sql = "INSERT INTO Sys_EnCfg(No,GroupTitle) VALUES ('BP.WF.Ext.NodeO','NodeID=基本配置@WarningDays=考核属性@SendLab=功能按钮标签与状态')";
            //BP.DA.DBAccess.RunSQL(sql);
            #endregion 手动升级. 2011-07-08 补充节点字段分组.

            #region 升级基础信息。 2011-11-02。 在过1个月去掉它。
            msg = "@补充数据时出现错误。";
            sql = "SELECT count(*) as Num FROM CN_City";
            if (BP.DA.DBAccess.RunSQLReturnValInt(sql) == 0)
            {
                msg = "@补充数据时出现错误，获得文件。";
                string fileOfSQL = BP.SystemConfig.PathOfData + "\\Install\\SQLScript\\InitPublicData.sql";
                BP.DA.DBAccess.RunSQLScript(fileOfSQL);
            }

            msg = "@升级退回规则。";
            // 升级退回规则。
            try
            {
                sql = "SELECT ReturnToNode FROM WF_ReturnWork";
                /*如果有这个列说明是未升级的 , 让它删除重建。*/
                BP.DA.DBAccess.RunSQLReturnTable(sql);
            }
            catch
            {
                sql = "DROP TABLE WF_ReturnWork";
                BP.DA.DBAccess.RunSQL(sql);

                BP.WF.ReturnWork rw = new BP.WF.ReturnWork();
                rw.CheckPhysicsTable();
            }
            #endregion 升级基础信息。

            

            msg = "@登陆时间错误。。";

            #region 执行admin登陆.
            Emp emp = new Emp();
            emp.No = "admin";
            if (emp.RetrieveFromDBSources() == 1)
            {
                BP.Web.WebUser.SignInOfGener(emp, true);
            }
            else
            {
                emp.No = "admin";
                emp.Name = "admin";
                emp.FK_Dept = "01";
                emp.Pass = "pub";
                emp.Insert();
                BP.Web.WebUser.SignInOfGener(emp, true);
                //throw new Exception("admin 用户丢失，请注意大小写。");
            }
            #endregion 执行admin登陆.
        }
        catch (Exception ex)
        {
            this.Response.Write("问题出处:" + msg + "<br>详细信息:@" + ex.Message + "<br>@<a href='../DBInstall.aspx' >点这里到系统升级界面。</a>");
            return;
        }
    }
}