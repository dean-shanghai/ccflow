using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Text;
using BP.WF;
using BP.DA;
using BP.Port;
using BP.Web;
using BP.En;
using BP.Sys;

namespace BP.WF
{
    /// <summary>
    /// 此接口为程序员二次开发使用,在阅读代码前请注意如下事项.
    /// 1, CCFlow的对外的接口都是以静态方法来实现的.
    /// 2, 以 DB_ 开头的是需要返回结果集合的接口.
    /// 3, 以 Flow_ 是流程接口.
    /// 4, 以 Node_ 是节点接口。
    /// 5, 以 Port_ 是组织架构接口.
    /// 6, 以 DTS_是调度．
    /// </summary>
    public class Dev2Interface
    {
        #region 自动执行
        /// <summary>
        /// 自动执行开始节点数据
        /// </summary>
        public static void DTS_AutoStarterFlow(Flow fl)
        {
            #region 读取数据.
            BP.Sys.MapExt me = new Sys.MapExt();
            int i = me.Retrieve(MapExtAttr.FK_MapData, "ND" + int.Parse(fl.No) + "01",
                MapExtAttr.ExtType, "PageLoadFull");
            if (i == 0)
            {
                BP.DA.Log.DefaultLogWriteLineError("没有为流程(" + fl.Name + ")的开始节点设置发起数据,请参考说明书解决.");
                return;
            }

            // 获取从表数据.
            DataSet ds = new DataSet();
            string[] dtlSQLs = me.Tag1.Split('*');
            foreach (string sql in dtlSQLs)
            {
                if (string.IsNullOrEmpty(sql))
                    continue;

                string[] tempStrs = sql.Split('=');
                string dtlName = tempStrs[0];
                DataTable dtlTable = BP.DA.DBAccess.RunSQLReturnTable(sql.Replace(dtlName + "=", ""));
                dtlTable.TableName = dtlName;
                ds.Tables.Add(dtlTable);
            }
            #endregion 读取数据.

            #region 检查数据源是否正确.
            string errMsg = "";
            // 获取主表数据.
            DataTable dtMain = BP.DA.DBAccess.RunSQLReturnTable(me.Tag);
            if (dtMain.Columns.Contains("Starter") == false)
                errMsg += "@配值的主表中没有Starter列.";

            if (dtMain.Columns.Contains("MainPK") == false)
                errMsg += "@配值的主表中没有MainPK列.";

            if (errMsg.Length > 2)
            {
                BP.DA.Log.DefaultLogWriteLineError("流程(" + fl.Name + ")的开始节点设置发起数据,不完整." + errMsg);
                return;
            }
            #endregion 检查数据源是否正确.

            #region 处理流程发起.

            string nodeTable = "ND" + int.Parse(fl.No) + "01";
            foreach (DataRow dr in dtMain.Rows)
            {
                string mainPK = dr["MainPK"].ToString();
                string sql = "SELECT OID FROM " + nodeTable + " WHERE MainPK='" + mainPK + "'";
                if (DBAccess.RunSQLReturnTable(sql).Rows.Count != 0)
                    continue; /*说明已经调度过了*/

                string starter = dr["Starter"].ToString();
                if (Web.WebUser.No != starter)
                {
                    BP.Web.WebUser.Exit();
                    BP.Port.Emp emp = new BP.Port.Emp();
                    emp.No = starter;
                    if (emp.RetrieveFromDBSources() == 0)
                    {
                        BP.DA.Log.DefaultLogWriteLineInfo("@数据驱动方式发起流程(" + fl.Name + ")设置的发起人员:" + emp.No + "不存在。");
                        continue;
                    }

                    BP.Web.WebUser.SignInOfGener(emp);
                }

                #region  给值.
                Work wk = fl.NewWork();
                foreach (DataColumn dc in dtMain.Columns)
                    wk.SetValByKey(dc.ColumnName, dr[dc.ColumnName].ToString());

                if (ds.Tables.Count != 0)
                {
                    string refPK = dr["MainPK"].ToString();
                    MapDtls dtls = wk.HisNode.MapData.MapDtls; // new MapDtls(nodeTable);
                    foreach (MapDtl dtl in dtls)
                    {
                        foreach (DataTable dt in ds.Tables)
                        {
                            if (dt.TableName != dtl.No)
                                continue;

                            //删除原来的数据。
                            GEDtl dtlEn = dtl.HisGEDtl;
                            dtlEn.Delete(GEDtlAttr.RefPK, wk.OID.ToString());

                            // 执行数据插入。
                            foreach (DataRow drDtl in dt.Rows)
                            {
                                if (drDtl["RefMainPK"].ToString() != refPK)
                                    continue;

                                dtlEn = dtl.HisGEDtl;

                                foreach (DataColumn dc in dt.Columns)
                                    dtlEn.SetValByKey(dc.ColumnName, drDtl[dc.ColumnName].ToString());

                                dtlEn.RefPK = wk.OID.ToString();
                                dtlEn.Insert();
                            }
                        }
                    }
                }
                #endregion  给值.

                // 处理发送信息.
                Node nd = fl.HisStartNode;
                try
                {
                    WorkNode wn = new WorkNode(wk, nd);
                    string msg = wn.NodeSend().ToMsgOfHtml();
                    //BP.DA.Log.DefaultLogWriteLineInfo(msg);
                }
                catch (Exception ex)
                {
                    BP.DA.Log.DefaultLogWriteLineWarning(ex.Message);
                }
            }
            #endregion 处理流程发起.

        }
        #endregion

        #region 数据集合接口(如果您想获取一个结果集合的接口，都是以DB_开头的.)
        #region 获取流程事例的轨迹图
        /// <summary>
        /// 获取流程事例的轨迹图
        /// </summary>
        /// <param name="fk_flow">流程编号</param>
        /// <param name="workid">工作ID</param>
        /// <param name="fid">流程ID</param>
        /// <returns>从临时表与轨迹表获取的track.</returns>
        public static DataTable DB_GenerTrack(string fk_flow, Int64 workid, Int64 fid)
        {
            string sql = "";
            sql = "SELECT * FROM WF_TrackTemp WHERE FID=" + fid + " AND WorkID=" + workid + " AND FK_Flow=" + fk_flow + " ORDER BY RDT";
            sql += " UNION ";
            sql += " SELECT * FROM WF_Track WHERE FID=" + fid + " AND WorkID=" + workid + " AND FK_Flow=" + fk_flow + " ORDER BY RDT";
            return DBAccess.RunSQLReturnTable(sql);
        }
        #endregion 获取流程事例的轨迹图

        #region 获取操送列表
        /// <summary>
        /// 获取抄送列表
        /// </summary>
        /// <param name="fk_emp">人员编号</param>
        /// <returns></returns>
        public static DataTable DB_CCList(string fk_emp)
        {
            return DBAccess.RunSQLReturnTable("SELECT * FROM WF_CCList WHERE CCTo='" + fk_emp + "'");
        }
        /// <summary>
        /// 获取未读的抄送
        /// </summary>
        /// <param name="fk_emp">人员编号</param>
        /// <returns></returns>
        public static DataTable DB_CCList_UnRead(string fk_emp)
        {
            return DBAccess.RunSQLReturnTable("SELECT * FROM WF_CCList WHERE CCTo='" + fk_emp + "' AND Sta=0");
        }
        /// <summary>
        /// 获取已读的抄送
        /// </summary>
        /// <param name="fk_emp">人员编号</param>
        /// <returns></returns>
        public static DataTable DB_CCList_Read(string fk_emp)
        {
            return DBAccess.RunSQLReturnTable("SELECT * FROM WF_CCList WHERE CCTo='" + fk_emp + "' AND Sta=1");
        }
        /// <summary>
        /// 获取已删除的抄送
        /// </summary>
        /// <param name="fk_emp">人员编号</param>
        /// <returns></returns>
        public static DataTable DB_CCList_Delete(string fk_emp)
        {
            return DBAccess.RunSQLReturnTable("SELECT * FROM WF_CCList WHERE CCTo='" + fk_emp + "' AND Sta=2");
        }
        #endregion

        #region 获取当前操作员可以发起的流程集合
        /// <summary>
        /// 获取当前操作员可以发起的流程集合
        /// </summary>
        /// <returns>bp.wf.flows</returns>
        public static Flows DB_GenerCanStartFlowsOfEntities()
        {
            return DB_GenerCanStartFlowsOfEntities(WebUser.No);
        }
        /// <summary>
        /// 获取能够发起流程的集合.
        /// </summary>
        /// <param name="userNo"></param>
        /// <returns></returns>
        public static Flows DB_GenerCanStartFlowsOfEntities(string userNo)
        {
            // 按岗位计算.
            string sql = "SELECT FK_Flow FROM WF_Node WHERE NodePosType=0 AND ( WhoExeIt=0 OR WhoExeIt=2 ) AND NodeID IN ( SELECT FK_Node FROM WF_NodeStation WHERE FK_Station IN (SELECT FK_Station FROM Port_EmpStation WHERE FK_EMP='" + WebUser.No + "')) ";
            sql += " UNION  "; //按指定的人员计算.
            sql += "  SELECT FK_Flow FROM WF_Node WHERE NodePosType=0 AND ( WhoExeIt=0 OR WhoExeIt=2 ) AND NodeID IN ( SELECT FK_Node FROM WF_NodeEmp WHERE FK_Emp='" + userNo + "' ) ";
            sql += " UNION  "; // 按岗位计算.
            sql += " SELECT FK_Flow FROM WF_Node WHERE NodePosType=0 AND ( WhoExeIt=0 OR WhoExeIt=2 ) AND NodeID IN ( SELECT FK_Node FROM WF_NodeDept WHERE FK_Dept IN(SELECT FK_Dept FROM Port_Emp WHERE No='" + userNo + "' UNION SELECT FK_DEPT FROM Port_EmpDept WHERE FK_Emp='" + userNo + "') ) ";


            Flows fls = new Flows();
            BP.En.QueryObject qo = new BP.En.QueryObject(fls);
            qo.AddWhereInSQL("No", sql);
            qo.addAnd();
            qo.AddWhere(FlowAttr.IsOK, true);
            qo.addAnd();
            qo.AddWhere(FlowAttr.IsCanStart, true);
            if (WebUser.IsAuthorize)
            {
                /*如果是授权状态*/
                qo.addAnd();
                WF.Port.WFEmp wfEmp = new Port.WFEmp(WebUser.No);
                qo.AddWhereIn("No", wfEmp.AuthorFlows);
            }

            qo.addOrderBy("FK_FlowSort", FlowAttr.Idx);
            qo.DoQuery();
            return fls;
        }
        /// <summary>
        /// 获取当前操作员可以发起的流程集合
        /// 此方法用于sdk表单上合流点查看未完成的子线程。
        /// </summary>
        /// <param name="userNo"></param>
        /// <returns></returns>
        public static DataTable DB_GenerCanStartFlowsOfDataTable(string userNo)
        {
            // 按岗位计算.
            string sql = "";
            sql += "SELECT FK_Flow FROM WF_Node WHERE NodePosType=0 AND ( WhoExeIt=0 OR WhoExeIt=2 ) AND NodeID IN ( SELECT FK_Node FROM WF_NodeStation WHERE FK_Station IN (SELECT FK_Station FROM Port_EmpStation WHERE FK_Emp='" + WebUser.No + "')) ";
            sql += " UNION  "; //按指定的人员计算.
            sql += "SELECT FK_Flow FROM WF_Node WHERE NodePosType=0 AND ( WhoExeIt=0 OR WhoExeIt=2 ) AND NodeID IN ( SELECT FK_Node FROM WF_NodeEmp WHERE FK_Emp='" + userNo + "' ) ";
            sql += " UNION  "; // 按岗位计算.
            sql += "SELECT FK_Flow FROM WF_Node WHERE NodePosType=0 AND ( WhoExeIt=0 OR WhoExeIt=2 ) AND NodeID IN ( SELECT FK_Node FROM WF_NodeDept WHERE FK_Dept IN(SELECT FK_Dept FROM Port_Emp WHERE No='" + userNo + "' UNION SELECT FK_DEPT FROM Port_EmpDept WHERE FK_Emp='" + userNo + "') ) ";

            Flows fls = new Flows();
            BP.En.QueryObject qo = new BP.En.QueryObject(fls);
            qo.AddWhereInSQL("No", sql);
            qo.addAnd();
            qo.AddWhere(FlowAttr.IsOK, true);
            qo.addAnd();
            qo.AddWhere(FlowAttr.IsCanStart, true);
            if (WebUser.IsAuthorize)
            {
                /*如果是授权状态*/
                qo.addAnd();
                WF.Port.WFEmp wfEmp = new Port.WFEmp(WebUser.No);
                qo.AddWhereIn("No", wfEmp.AuthorFlows);
            }
            qo.addOrderBy("FK_FlowSort", FlowAttr.Idx);
            return qo.DoQueryToTable();
        }
        /// <summary>
        /// For:中船lizheng: 2012-10-17
        /// 获取能够发起流程的sql
        /// </summary>
        /// <param name="userNo">操作人员编号</param>
        /// <returns>返回获取该操作人员的SQL</returns>
        public static string DB_GenerCanStartFlowsOfSQL(string userNo)
        {
            // 按岗位计算.
            string sql = "";
            sql += "SELECT FK_Flow FROM WF_Node WHERE NodePosType=0 AND ( WhoExeIt=0 OR WhoExeIt=2 ) AND NodeID IN ( SELECT FK_Node FROM WF_NodeStation WHERE FK_Station IN (SELECT FK_Station FROM Port_EmpStation WHERE FK_Emp='" + WebUser.No + "')) ";
            sql += " UNION  "; //按指定的人员计算.
            sql += "SELECT FK_Flow FROM WF_Node WHERE NodePosType=0 AND ( WhoExeIt=0 OR WhoExeIt=2 ) AND NodeID IN ( SELECT FK_Node FROM WF_NodeEmp WHERE FK_Emp='" + userNo + "' ) ";
            sql += " UNION  "; // 按岗位计算.
            sql += "SELECT FK_Flow FROM WF_Node WHERE NodePosType=0 AND ( WhoExeIt=0 OR WhoExeIt=2 ) AND NodeID IN ( SELECT FK_Node FROM WF_NodeDept WHERE FK_Dept IN(SELECT FK_Dept FROM Port_Emp WHERE No='" + userNo + "' UNION SELECT FK_DEPT FROM Port_EmpDept WHERE FK_Emp='" + userNo + "') ) ";

            Flows fls = new Flows();
            BP.En.QueryObject qo = new BP.En.QueryObject(fls);
            qo.AddWhereInSQL("No", sql);
            qo.addAnd();
            qo.AddWhere(FlowAttr.IsOK, true);
            qo.addAnd();
            qo.AddWhere(FlowAttr.IsCanStart, true);
            if (WebUser.IsAuthorize)
            {
                /*如果是授权状态*/
                qo.addAnd();
                WF.Port.WFEmp wfEmp = new Port.WFEmp(WebUser.No);
                qo.AddWhereIn("No", wfEmp.AuthorFlows);
            }
            qo.addOrderBy("FK_FlowSort", FlowAttr.Idx);
            return qo.SQLWithOutPara;
        }
        /// <summary>
        /// 获取(同步)合流点上的子线程
        /// 此方法用于sdk表单上合流点查看未完成的子线程。
        /// </summary>
        /// <param name="nodeIDOfHL">合流点ID</param>
        /// <param name="workid">流程ID</param>
        /// <returns></returns>
        public static DataTable DB_GenerHLSubFlowDtl_TB(int nodeIDOfHL, Int64 workid)
        {
            Node nd = new Node(nodeIDOfHL);
            Work wk = nd.HisWork;
            wk.OID = workid;
            wk.Retrieve();

            GenerWorkerLists wls = new GenerWorkerLists();
            QueryObject qo = new QueryObject(wls);
            qo.AddWhere(GenerWorkerListAttr.FID, wk.OID);
            qo.addAnd();
            qo.AddWhere(GenerWorkerListAttr.IsEnable, 1);
            qo.addAnd();
            qo.AddWhere(GenerWorkerListAttr.FK_Node,
                nd.FromNodes[0].GetValByKey(NodeAttr.NodeID));

            DataTable dt = qo.DoQueryToTable();
            if (dt.Rows.Count == 1)
            {
                qo.clear();
                qo.AddWhere(GenerWorkerListAttr.FID, wk.OID);
                qo.addAnd();
                qo.AddWhere(GenerWorkerListAttr.IsEnable, 1);
                return qo.DoQueryToTable();
            }
            return dt;
        }
        /// <summary>
        /// 获取(异步)合流点上的子线程
        /// </summary>
        /// <param name="nodeIDOfHL">合流点ID</param>
        /// <param name="workid">流程ID</param>
        /// <returns></returns>
        public static DataTable DB_GenerHLSubFlowDtl_YB(int nodeIDOfHL, Int64 workid)
        {
            Node nd = new Node(nodeIDOfHL);
            Work wk = nd.HisWork;
            wk.OID = workid;
            wk.Retrieve();

            GenerWorkerLists wls = new GenerWorkerLists();
            QueryObject qo = new QueryObject(wls);
            qo.AddWhere(GenerWorkerListAttr.FID, wk.OID);
            qo.addAnd();
            qo.AddWhere(GenerWorkerListAttr.IsEnable, 1);
            qo.addAnd();
            qo.AddWhere(GenerWorkerListAttr.IsPass, 0);
            return qo.DoQueryToTable();
        }
        #endregion 获取当前操作员可以发起的流程集合

        #region 流程草稿
        /// <summary>
        /// 产生数据
        /// </summary>
        /// <param name="fk_flow"></param>
        /// <returns></returns>
        public static DataTable DB_GenerDraftDataTable(string fk_flow)
        {
            /*获取数据.*/
            string nodeTable = "ND" + int.Parse(fk_flow) + "01";
            string dbStr = BP.SystemConfig.AppCenterDBVarStr;
            BP.DA.Paras ps = new BP.DA.Paras();
            ps.Add("Rec", BP.Web.WebUser.No);
            ps.SQL = "SELECT OID,Title,RDT,'"+fk_flow+"' as FK_Flow,FID, "+int.Parse(fk_flow)+"01 as FK_Node FROM " + nodeTable + " WHERE NodeState=0 AND Rec=" + dbStr + "Rec";
            return BP.DA.DBAccess.RunSQLReturnTable(ps);
        }
        #endregion 流程草稿

        #region 获取当前操作员的待办工作
        /// <summary>
        /// 获取当前操作员的待办工作
        /// </summary>
        /// <param name="fk_flow">根据流程编号，如果流程编号为空则返回全部</param>
        /// <returns>当前操作员待办工作</returns>
        public static DataTable DB_GenerEmpWorksOfDataTable(int wfState, string fk_flow)
        {
            Paras ps = new Paras();
            string dbstr = BP.SystemConfig.AppCenterDBVarStr;
            string sql;
            if (WebUser.IsAuthorize == false)
            {
                /*不是授权状态*/
                if (string.IsNullOrEmpty(fk_flow))
                {
                    ps.SQL = "SELECT * FROM WF_EmpWorks WHERE WFState=" + dbstr + "WFState AND FK_Emp=" + dbstr + "FK_Emp  ORDER BY FK_Flow,ADT DESC ";
                    ps.Add("WFState", wfState);
                    ps.Add("FK_Emp", BP.Web.WebUser.No);
                }
                else
                {
                    ps.SQL = "SELECT * FROM WF_EmpWorks WHERE WFState=" + dbstr + "WFState AND FK_Emp=" + dbstr + "FK_Emp AND FK_Flow="+dbstr+"FK_Flow ORDER BY  ADT DESC ";
                    ps.Add("WFState", wfState);
                    ps.Add("FK_Flow", fk_flow);
                    ps.Add("FK_Emp", BP.Web.WebUser.No);
                }
                return BP.DA.DBAccess.RunSQLReturnTable(ps);
            }

            /*如果是授权状态, 获取当前委托人的信息. */
            WF.Port.WFEmp emp = new Port.WFEmp(WebUser.No);
            switch (emp.HisAuthorWay)
            {
                case Port.AuthorWay.All:
                    if (string.IsNullOrEmpty(fk_flow))
                    {
                        ps.SQL = "SELECT * FROM WF_EmpWorks WHERE WFState=" + dbstr + "WFState AND FK_Emp=" + dbstr + "FK_Emp  ORDER BY FK_Flow,ADT DESC ";
                        ps.Add("WFState", wfState);
                        ps.Add("FK_Emp", BP.Web.WebUser.No);
                    }
                    else
                    {
                        ps.SQL = "SELECT * FROM WF_EmpWorks WHERE WFState=" + dbstr + "WFState AND FK_Emp=" + dbstr + "FK_Emp AND FK_Flow" + dbstr + "FK_Flow ORDER BY FK_Flow,ADT DESC ";
                        ps.Add("WFState", wfState);
                        ps.Add("FK_Emp", BP.Web.WebUser.No);
                        ps.Add("FK_Flow", fk_flow);
                    }
                    break;
                case Port.AuthorWay.SpecFlows:
                    if (string.IsNullOrEmpty(fk_flow))
                    {
                        sql = "SELECT * FROM WF_EmpWorks WHERE WFState=" + dbstr + "WFState AND FK_Emp=" + dbstr + "FK_Emp AND  FK_Flow IN " + emp.AuthorFlows + "  ORDER BY FK_Flow,ADT DESC ";
                        ps.Add("WFState", wfState);
                        ps.Add("FK_Emp", BP.Web.WebUser.No);
                    }
                    else
                    {
                        sql = "SELECT * FROM WF_EmpWorks WHERE WFState=" + dbstr + "WFState AND FK_Emp=" + dbstr + "FK_Emp  AND FK_Flow" + dbstr + "FK_Flow AND FK_Flow IN " + emp.AuthorFlows + "  ORDER BY FK_Flow,ADT DESC ";
                        ps.Add("WFState", wfState);
                        ps.Add("FK_Emp", BP.Web.WebUser.No);
                        ps.Add("FK_Flow", fk_flow);
                    }
                    break;
                case Port.AuthorWay.None:
                    throw new Exception("对方(" + WebUser.No + ")已经取消了授权.");
                default:
                    throw new Exception("no such way...");
            }
            return BP.DA.DBAccess.RunSQLReturnTable(ps);
        }
        /// <summary>
        /// 获取当前操作人员的待办信息
        /// 数据内容请参考试图:WF_EmpWorks
        /// </summary>
        /// <returns>返回从视图WF_EmpWorks查询出来的数据.</returns>
        public static DataTable DB_GenerEmpWorksOfDataTable()
        {
            return DB_GenerEmpWorksOfDataTable((int)WFState.Runing, null);
        }
        /// <summary>
        /// 获得挂起工作列表
        /// </summary>
        /// <returns>返回从视图WF_EmpWorks查询出来的数据.</returns>
        public static DataTable DB_GenerHungUpList()
        {
            return DB_GenerHungUpList(null);
        }
        /// <summary>
        /// 获得挂起工作列表:根据流程编号
        /// </summary>
        /// <param name="fk_flow">流程编号</param>
        /// <returns>返回从视图WF_EmpWorks查询出来的数据.</returns>
        public static DataTable DB_GenerHungUpList(string fk_flow)
        {
            string sql;
            int state = (int)WFState.HungUp;
            if (WebUser.IsAuthorize)
            {
                WF.Port.WFEmp emp = new Port.WFEmp(WebUser.No);
                if (string.IsNullOrEmpty(fk_flow))
                    sql = "SELECT a.WorkID FROM WF_GenerWorkFlow A, WF_GenerWorkerlist B WHERE  A.WFState=" + state + " AND A.WorkID=B.WorkID AND B.FK_EMP='" + WebUser.No + "' AND B.IsEnable=1 AND A.FK_Flow IN " + emp.AuthorFlows;
                else
                    sql = "SELECT a.WorkID FROM WF_GenerWorkFlow A, WF_GenerWorkerlist B WHERE A.FK_Flow='" + fk_flow + "'  AND A.WFState=" + state + " AND A.WorkID=B.WorkID AND B.FK_EMP='" + WebUser.No + "' AND  B.IsPass=1 AND A.FK_Flow IN " + emp.AuthorFlows;
            }
            else
            {
                if (string.IsNullOrEmpty(fk_flow))
                    sql = "SELECT a.WorkID FROM WF_GenerWorkFlow A, WF_GenerWorkerlist B WHERE  A.WFState=" + state + " AND A.WorkID=B.WorkID AND B.FK_EMP='" + WebUser.No + "' AND B.IsEnable=1   ";
                else
                    sql = "SELECT a.WorkID FROM WF_GenerWorkFlow A, WF_GenerWorkerlist B WHERE A.FK_Flow='" + fk_flow + "'  AND A.WFState=" + state + " AND A.WorkID=B.WorkID AND B.FK_EMP='" + WebUser.No + "' AND B.IsEnable=1 ";
            }
            GenerWorkFlows gwfs = new GenerWorkFlows();
            gwfs.RetrieveInSQL(GenerWorkFlowAttr.WorkID, "(" + sql + ")");
            return gwfs.ToDataTableField();
        }
        /// <summary>
        /// 获得逻辑删除的流程
        /// </summary>
        /// <returns>返回从视图WF_EmpWorks查询出来的数据.</returns>
        public static DataTable DB_GenerDeleteWorkList()
        {
            return DB_GenerDeleteWorkList(WebUser.No,null);
        }
        /// <summary>
        /// 获得逻辑删除的流程:根据流程编号
        /// </summary>
        /// <param name="userNo">操作员编号</param>
        /// <param name="fk_flow">流程编号(可以为空)</param>
        /// <returns>WF_GenerWorkFlow数据结构的集合</returns>
        public static DataTable DB_GenerDeleteWorkList(string userNo, string fk_flow)
        {
            string sql;
            int state = (int)WFState.Delete;
            if (WebUser.IsAuthorize)
            {
                WF.Port.WFEmp emp = new Port.WFEmp(WebUser.No);
                if (string.IsNullOrEmpty(fk_flow))
                    sql = "SELECT a.WorkID FROM WF_GenerWorkFlow A, WF_GenerWorkerlist B WHERE  A.WFState=" + state + " AND A.WorkID=B.WorkID AND B.FK_EMP='" + WebUser.No + "' AND B.IsEnable=1 AND A.FK_Flow IN " + emp.AuthorFlows;
                else
                    sql = "SELECT a.WorkID FROM WF_GenerWorkFlow A, WF_GenerWorkerlist B WHERE A.FK_Flow='" + fk_flow + "'  AND A.WFState=" + state + " AND A.WorkID=B.WorkID AND B.FK_EMP='" + WebUser.No + "' AND  B.IsPass=1 AND A.FK_Flow IN " + emp.AuthorFlows;
            }
            else
            {
                if (string.IsNullOrEmpty(fk_flow))
                    sql = "SELECT a.WorkID FROM WF_GenerWorkFlow A, WF_GenerWorkerlist B WHERE  A.WFState=" + state + " AND A.WorkID=B.WorkID AND B.FK_EMP='" + WebUser.No + "' AND B.IsEnable=1   ";
                else
                    sql = "SELECT a.WorkID FROM WF_GenerWorkFlow A, WF_GenerWorkerlist B WHERE A.FK_Flow='" + fk_flow + "'  AND A.WFState=" + state + " AND A.WorkID=B.WorkID AND B.FK_EMP='" + WebUser.No + "' AND B.IsEnable=1 ";
            }
            GenerWorkFlows gwfs = new GenerWorkFlows();
            gwfs.RetrieveInSQL(GenerWorkFlowAttr.WorkID, "(" + sql + ")");
            return gwfs.ToDataTableField();
        }
        /// <summary>
        /// For Lizheng: 2012-10-17
        /// 获取指定人员能够发起人员sql.
        /// </summary>
        /// <param name="fk_emp">指定人中编号</param>
        /// <returns>获取指定人员能够发起人员sql</returns>
        public static string DB_GenerEmpWorksOfSQL(string fk_emp)
        {
            if (WebUser.IsAuthorize == false)
            {
                /*如果不是授权状态*/
                return  "SELECT * FROM WF_EmpWorks WHERE FK_Emp='" + fk_emp + "'  ORDER BY FK_Flow,ADT DESC ";
            }
            /*如果是授权状态, 获取当前委托人的信息. */
            WF.Port.WFEmp emp = new Port.WFEmp(WebUser.No);
            switch (emp.HisAuthorWay)
            {
                case Port.AuthorWay.All:
                    return "SELECT * FROM WF_EmpWorks WHERE FK_Emp='" + WebUser.No + "' ORDER BY FK_Flow,ADT DESC ";
                    break;
                case Port.AuthorWay.SpecFlows:
                    return "SELECT * FROM WF_EmpWorks WHERE FK_Emp='" + WebUser.No + "' AND FK_Flow IN " + emp.AuthorFlows + "  ORDER BY FK_Flow,ADT DESC ";
                    break;
                case Port.AuthorWay.None:
                    throw new Exception("对方(" + WebUser.No + ")已经取消了授权.");
                default:
                    throw new Exception("no such way...");
            }

        }
        #endregion 获取当前操作员的待办工作

        #region 获取流程数据
        /// <summary>
        /// 根据流程状态获取指定流程数据
        /// </summary>
        /// <param name="fk_flow"></param>
        /// <param name="sta"></param>
        /// <returns></returns>
        public static DataTable DB_NDxxRpt(string fk_flow, WFState sta)
        {
            string dbstr = BP.SystemConfig.AppCenterDBVarStr;
            string sql = "SELECT OID,Title,RDT,FID FROM ND" + int.Parse(fk_flow) + "Rpt WHERE WFState="+(int)sta+" AND Rec=" + dbstr + "Rec";
            BP.DA.Paras ps = new BP.DA.Paras();
            ps.SQL = sql;
            ps.Add("Rec", BP.Web.WebUser.No);
            return DBAccess.RunSQLReturnTable(ps);


            //string dbstr = BP.SystemConfig.AppCenterDBVarStr;
            //string sql = "SELECT OID,Title,RDT,FID FROM ND" + int.Parse(fk_flow) + "Rpt WHERE WFState=" + dbstr + "WFState AND Rec=" + dbstr + "Rec";
            //BP.DA.Paras ps = new BP.DA.Paras();
            //ps.SQL = sql;
            //ps.Add("Rec", BP.Web.WebUser.No);

            //int s = (int)sta;
            //ps.Add("WFState", s);
            //return DBAccess.RunSQLReturnTable(ps);
        }
        #endregion

        #region 获取当前可以退回的节点。
        /// <summary>
        /// 获取当前节点可以退回的节点
        /// </summary>
        /// <param name="fk_node">节点ID</param>
        /// <param name="workid">工作ID</param>
        /// <param name="fid">FID</param>
        /// <returns>No节点编号,Name节点名称,Rec记录人,RecName记录人名称</returns>
        public static DataTable DB_GenerWillReturnNodes(int fk_node, Int64 workid, Int64 fid)
        {
            DataTable dt = new DataTable("obt");
            dt.Columns.Add("No"); // 节点ID
            dt.Columns.Add("Name"); // 节点名称.
            dt.Columns.Add("Rec"); // 被退回节点上的操作员编号.
            dt.Columns.Add("RecName"); // 被退回节点上的操作员名称.

            Node nd = new Node(fk_node);
            if (nd.HisRunModel == RunModel.SubThread)
            {
                /*如果是子线程，它只能退回它的上一个节点，现在写死了，其它的设置不起作用了。*/
                Nodes nds = nd.FromNodes;
                foreach (Node ndFrom in nds)
                {
                    Work wk;
                    switch (ndFrom.HisRunModel)
                    {
                        case RunModel.FL:
                        case RunModel.FHL:
                            wk = ndFrom.HisWork;
                            wk.OID = fid;
                            if (wk.RetrieveFromDBSources() == 0)
                                continue;
                            break;
                        case RunModel.SubThread:
                            wk = ndFrom.HisWork;
                            wk.OID = workid;
                            if (wk.RetrieveFromDBSources() == 0)
                                continue;
                            break;
                        case RunModel.Ordinary:
                        default:
                            throw new Exception("流程设计异常，子线程的上一个节点不能是普通节点。");
                            break;
                    }
                    DataRow dr = dt.NewRow();
                    dr["No"] = ndFrom.NodeID;
                    dr["Name"] = wk.RecText + "=>" + ndFrom.Name;
                    dt.Rows.Add(dr);
                }
                return dt;
            }

            WorkNode wn = new WorkNode(workid, fk_node);
            WorkNodes wns = new WorkNodes();
            switch (nd.HisReturnRole)
            {
                case ReturnRole.CanNotReturn:
                    return dt;
                case ReturnRole.ReturnAnyNodes:
                    if (wns.Count == 0)
                        wns.GenerByWorkID(wn.HisNode.HisFlow, workid);
                    foreach (WorkNode mywn in wns)
                    {
                        if (mywn.HisNode.NodeID == fk_node)
                            continue;

                        DataRow dr = dt.NewRow();
                        dr["No"] = mywn.HisNode.NodeID.ToString();
                        dr["Name"] = mywn.HisNode.Name;
                        dr["Rec"] = mywn.HisWork.Rec;
                        dr["RecName"] = mywn.HisWork.RecText;
                        dt.Rows.Add(dr);
                    }
                    break;
                case ReturnRole.ReturnPreviousNode:
                    WorkNode mywnP = wn.GetPreviousWorkNode();
                    //  turnTo = mywnP.HisWork.Rec + mywnP.HisWork.RecText;
                    DataRow dr1 = dt.NewRow();
                    dr1["No"] = mywnP.HisNode.NodeID.ToString();
                    dr1["Name"] =mywnP.HisNode.Name;
                    dr1["Rec"] = mywnP.HisWork.Rec;
                    dr1["RecName"] = mywnP.HisWork.RecText;
                    dt.Rows.Add(dr1);
                    break;
                case ReturnRole.ReturnSpecifiedNodes: //退回指定的节点。
                    if (wns.Count == 0)
                        wns.GenerByWorkID(wn.HisNode.HisFlow, workid);

                    NodeReturns rnds = new NodeReturns();
                    rnds.Retrieve(NodeReturnAttr.FK_Node, fk_node);
                    if (rnds.Count == 0)
                        throw new Exception("@流程设计错误，您设置该节点可以退回指定的节点，但是指定的节点集合为空，请在节点属性设置它的制订节点。");
                    foreach (WorkNode mywn in wns)
                    {
                        if (mywn.HisNode.NodeID == fk_node)
                            continue;

                        if (rnds.Contains(NodeReturnAttr.ReturnTo,
                            mywn.HisNode.NodeID) == false)
                            continue;

                        DataRow dr = dt.NewRow();
                        dr["No"] = mywn.HisNode.NodeID.ToString();
                        dr["Name"] = mywn.HisNode.Name;
                        dr["Rec"] = mywn.HisWork.Rec;
                        dr["RecName"] = mywn.HisWork.RecText;
                        dt.Rows.Add(dr);
                    }
                    break;
                case ReturnRole.ByReturnLine: //按照流程图画的退回线执行退回.
                    if (wns.Count == 0)
                        wns.GenerByWorkID(wn.HisNode.HisFlow, workid);
                   
                    Directions dirs = new Directions();
                    dirs.Retrieve(DirectionAttr.ToNode, fk_node, DirectionAttr.DirType,1);
                    if (dirs.Count == 0)
                        throw new Exception("@流程设计错误:当前节点没有画向后退回的退回线,更多的信息请参考退回规则.");

                    foreach (WorkNode mywn in wns)
                    {
                        if (mywn.HisNode.NodeID == fk_node)
                            continue;

                        if (dirs.Contains(DirectionAttr.ToNode,
                            mywn.HisNode.NodeID) == false)
                            continue;

                        DataRow dr = dt.NewRow();
                        dr["No"] = mywn.HisNode.NodeID.ToString();
                        dr["Name"] = mywn.HisNode.Name;
                        dr["Rec"] = mywn.HisWork.Rec;
                        dr["RecName"] = mywn.HisWork.RecText;
                        dt.Rows.Add(dr);
                    }
                    break;
                default:
                    throw new Exception("@没有判断的退回类型。");
            }

            if (dt.Rows.Count == 0)
                throw new Exception("@没有计算出来要退回的节点，请管理员确认节点退回规则是否合理？");

            return dt;
        }
        #endregion 获取当前可以退回的节点

        #region 获取当前操作员的在途工作
        
        /// <summary>
        /// For Lizheng: 2012-10-12
        /// 获取指定人员在途工作
        /// </summary>
        /// <param name="userNo">指定人员编号</param>
        /// <returns>查询SQL</returns>
        public static string DB_GenerRuningOfSQL(string userNo)
        {
            string sql;
            if (WebUser.IsAuthorize)
            {
                WF.Port.WFEmp emp = new Port.WFEmp(WebUser.No);
                sql = "SELECT a.* FROM WF_GenerWorkFlow A, WF_GenerWorkerlist B WHERE A.WorkID=B.WorkID AND B.FK_EMP='" + userNo + "' AND B.IsEnable=1 AND B.IsPass=1 AND A.FK_Flow IN " + emp.AuthorFlows;
            }
            else
            {
                sql = "SELECT a.* FROM WF_GenerWorkFlow A, WF_GenerWorkerlist B WHERE A.WorkID=B.WorkID AND B.FK_EMP='" + userNo + "' AND B.IsEnable=1 AND B.IsPass=1 ";
            }
            return sql;
        }
        /// <summary>
        /// 获取未完成的流程(也称为在途流程:我参与的但是此流程未完成)
        /// </summary>
        /// <param name="fk_flow">流程编号</param>
        /// <returns>返回从数据视图WF_GenerWorkflow查询出来的数据.</returns>
        public static DataTable DB_GenerRuning(string fk_flow)
        {
            string sql;
            int state = (int)WFState.Runing;
            if (WebUser.IsAuthorize)
            {
                WF.Port.WFEmp emp = new Port.WFEmp(WebUser.No);
                if (string.IsNullOrEmpty(fk_flow))
                    sql = "SELECT a.WorkID FROM WF_GenerWorkFlow A, WF_GenerWorkerlist B WHERE  A.WFState=" + state + " AND A.WorkID=B.WorkID AND B.FK_EMP='" + WebUser.No + "' AND B.IsEnable=1 AND B.IsPass=1 AND A.FK_Flow IN " + emp.AuthorFlows;
                else
                    sql = "SELECT a.WorkID FROM WF_GenerWorkFlow A, WF_GenerWorkerlist B WHERE A.FK_Flow='" + fk_flow + "'  AND A.WFState=" + state + " AND A.WorkID=B.WorkID AND B.FK_EMP='" + WebUser.No + "' AND B.IsEnable=1 AND B.IsPass=1 AND A.FK_Flow IN " + emp.AuthorFlows;
            }
            else
            {
                if (string.IsNullOrEmpty(fk_flow))
                    sql = "SELECT a.WorkID FROM WF_GenerWorkFlow A, WF_GenerWorkerlist B WHERE  A.WFState=" + state + " AND A.WorkID=B.WorkID AND B.FK_EMP='" + WebUser.No + "' AND B.IsEnable=1 AND B.IsPass=1 ";
                else
                    sql = "SELECT a.WorkID FROM WF_GenerWorkFlow A, WF_GenerWorkerlist B WHERE A.FK_Flow='" + fk_flow + "'  AND A.WFState=" + state + " AND A.WorkID=B.WorkID AND B.FK_EMP='" + WebUser.No + "' AND B.IsEnable=1 AND B.IsPass=1 ";
            }
            GenerWorkFlows gwfs = new GenerWorkFlows();
            gwfs.RetrieveInSQL(GenerWorkFlowAttr.WorkID, "(" + sql + ")");
            return gwfs.ToDataTableField();
        }
        /// <summary>
        /// 获取未完成的流程(也称为在途流程:我参与的但是此流程未完成)
        /// </summary>
        /// <returns>返回从数据视图WF_GenerWorkflow查询出来的数据.</returns>
        public static DataTable DB_GenerRuning()
        {
            return DB_GenerRuning(null);
        }
        #endregion 获取当前操作员的待办工作

        #endregion

        #region UI 接口
        /// <summary>
        /// 获取按钮状态
        /// </summary>
        /// <param name="fk_flow">流程编号</param>
        /// <param name="workid">流程ID</param>
        /// <returns>返回按钮状态</returns>
        public static ButtonState UI_GetButtonState(string fk_flow, int fk_node, Int64 workid)
        {
            ButtonState bs = new ButtonState(fk_flow, fk_node, workid);
            return bs;
        }
        /// <summary>
        /// 打开退回窗口
        /// </summary>
        public static void UI_OpenReturnWindow(Int64 workid)
        {
        }
        #endregion UI 接口

        #region 登陆接口
        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <param name="userNo">用户名</param>
        /// <param name="sid">安全ID</param>
        public static void Port_Login(string userNo, string sid)
        {
            string sql = "select sid from port_emp where no='" + userNo + "'";
            DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
            if (dt.Rows.Count == 0)
                throw new Exception("用户不存在或者SID错误。");

            if (dt.Rows[0]["SID"].ToString() != sid)
                throw new Exception("用户不存在或者SID错误。");

            BP.Port.Emp emp = new BP.Port.Emp(userNo);
            WebUser.SignInOfGener(emp, true);
            WebUser.IsWap = false;
            return;
        }
        /// <summary>
        /// 注销
        /// </summary>
        public static void Port_SigOut()
        {
            WebUser.Exit();
        }
        /// <summary>
        /// 发送邮件与消息
        /// </summary>
        /// <param name="userNo">信息接收人</param>
        /// <param name="msgTitle">标题</param>
        /// <param name="msgDoc">内容</param>
        public static void Port_SendMail(string userNo, string msgTitle, string msgDoc)
        {
            //WF.Port.WFEmp emp = new BP.WF.Port.WFEmp(userNo);
            BP.TA.SMS.AddMsg(DateTime.Now.ToString() + WebUser.No, userNo, msgTitle + msgDoc, msgTitle, msgDoc);
        }
        /// <summary>
        /// 发送SMS
        /// </summary>
        /// <param name="userNo">信息接收人</param>
        /// <param name="msgTitle">标题</param>
        /// <param name="msgDoc">内容</param>
        public static void Port_SendSMS(string sendToEmp, string msgTitle, string msgDoc)
        {
            BP.TA.SMS.AddMsg(DateTime.Now.ToString() + WebUser.No, sendToEmp, msgTitle + msgDoc, msgTitle, msgDoc);
        }
        #endregion 登陆接口

        #region 与流程有关的接口
        /// <summary>
        /// 产生一个新的工作ID
        /// </summary>
        /// <param name="flowNo"></param>
        /// <returns></returns>
        public static Int64 Flow_GenerWorkID(string flowNo)
        {
            Flow fl = new Flow(flowNo);
            return fl.NewWork().OID;
        }
        /// <summary>
        /// 产生一个新的工作
        /// </summary>
        /// <param name="flowNo"></param>
        /// <returns></returns>
        public static Work Flow_GenerWork(string flowNo)
        {
            Flow fl = new Flow(flowNo);
            Work wk= fl.NewWork();
            wk.ResetDefaultVal();
            return wk;
        }
        /// <summary>
        /// 是否可以发起改流程?
        /// </summary>
        /// <param name="flowNo"></param>
        /// <param name="fk_emp"></param>
        /// <returns></returns>
        public static bool Flow_IsCanStartThisFlow(string flowNo, string fk_emp)
        {
            Flows fls = DB_GenerCanStartFlowsOfEntities(fk_emp);
            return fls.Contains(FlowAttr.No, flowNo);
        }
        /// <summary>
        /// 执行流程自检
        /// </summary>
        /// <param name="flowNo">流程编号</param>
        /// <param name="workID">工作ID</param>
        /// <returns>执行信息</returns>
        public static string Flow_DoSelfTest(string flowNo, Int64 workID)
        {
            WorkFlow wf = new WorkFlow(flowNo, workID);
            return wf.DoSelfTest();
        }
        /// <summary>
        /// 恢复流程, 在一件工作完成后，要恢复上来这条
        /// 流程并把待办工作放在最后一个的结束的节点上。
        /// </summary>
        /// <param name="flowNo">流程编号</param>
        /// <param name="workID">工作ID</param>
        /// <param name="msg">原因</param>
        /// <returns>执行信息</returns>
        public static void Flow_DoComeBackWrokFlow(string flowNo, Int64 workID, string msg)
        {
            WorkFlow wf = new WorkFlow(flowNo, workID);
            wf.DoComeBackWrokFlow(msg);
        }
        /// <summary>
        /// 执行删除流程:彻底的删除流程.
        /// 清除的内容如下:
        /// 1, 流程引擎中的数据.
        /// 2, 节点数据,NDxxRpt数据.
        /// 3, 轨迹表数据.
        /// </summary>
        /// <param name="flowNo">流程编号</param>
        /// <param name="workID">工作ID</param>
        /// <returns>执行信息</returns>
        public static string Flow_DoDeleteFlowByReal(string flowNo, Int64 workID)
        {
            WorkFlow wf = new WorkFlow(flowNo, workID);
            wf.DoDeleteWorkFlowByReal();
            return "删除成功";
        }
        /// <summary>
        /// 执行逻辑删除流程:此流程并非真正的删除仅做了流程删除标记
        /// 比如:撤销的工单.
        /// </summary>
        /// <param name="flowNo">流程编号</param>
        /// <param name="workID">工作ID</param>
        /// <param name="msg">撤销的原因</param>
        /// <returns>执行信息</returns>
        public static string Flow_DoDeleteFlowByFlag(string flowNo, Int64 workID,string msg)
        {
            WorkFlow wf = new WorkFlow(flowNo, workID);
            wf.DoDeleteWorkFlowByFlag(msg);
            return "删除成功";
        }
        public static string Flow_DoUnDeleteFlowByFlag(string flowNo, Int64 workID, string msg)
        {
            WorkFlow wf = new WorkFlow(flowNo, workID);
            wf.DoUnDeleteWorkFlowByFlag(msg);
            return "撤销删除成功.";
        }
        /// <summary>
        /// 执行-撤销发送
        /// </summary>
        /// <param name="flowNo">流程编号</param>
        /// <param name="workID">工作ID</param>
        /// <returns>返回撤销信息,它会抛出异常.有可能流程转入了下一个节点</returns>
        public static string Flow_DoUnSend(string flowNo, Int64 workID)
        {
            WorkFlow wf = new WorkFlow(flowNo, workID);
            return wf.DoUnSend();
        }
        /// <summary>
        /// 执行流程结束:正常的流程结束.
        /// </summary>
        /// <param name="flowNo">流程编号</param>
        /// <param name="workID">工作ID</param>
        /// <param name="msg">流程结束原因</param>
        /// <returns>执行信息</returns>
        public static string Flow_DoFlowOver(string flowNo, Int64 workID, string msg)
        {
            WorkFlow wf = new WorkFlow(flowNo, workID);
            return wf.DoFlowOver(ActionType.FlowOver,msg);
        }
        /// <summary>
        /// 执行流程结束:强制的流程结束.
        /// </summary>
        /// <param name="flowNo">流程编号</param>
        /// <param name="workID">工作ID</param>
        /// <param name="msg">强制流程结束的原因</param>
        /// <returns></returns>
        public static string Flow_DoFlowOverByCoercion(string flowNo, Int64 workID, string msg)
        {
            WorkFlow wf = new WorkFlow(flowNo, workID);
            return wf.DoFlowOver(ActionType.FlowOverByCoercion, msg);
        }
        /// <summary>
        /// 获取指定的workid 在运行到的节点编号
        /// 2012-09-12 for lijian
        /// </summary>
        /// <param name="workID">需要找到的workid</param>
        /// <returns>如果没有找到，就会抛出异常.</returns>
        public static int Flow_GetCurrentNode(Int64 workID)
        {
            Paras ps = new Paras();
            ps.SQL = "SELECT FK_Node FROM WF_GenerWorkFlow WHERE WorkID=" + SystemConfig.AppCenterDBVarStr + "WorkID";
            ps.Add("WorkID", workID);
            return BP.DA.DBAccess.RunSQLReturnValInt(ps);
        }
        /// <summary>
        /// 获取指定节点的Work
        /// </summary>
        /// <param name="nodeID">节点ID</param>
        /// <param name="workID"></param>
        /// <returns></returns>
        public static Work Flow_GetCurrentWork(int nodeID,Int64 workID)
        {
            Node nd = new Node(nodeID);
            Work wk = nd.HisWork;
            wk.OID = workID;
            wk.Retrieve();
            return wk;
        }
        /// <summary>
        /// 获取当前工作节点的Work
        /// </summary>
        /// <param name="fk_flow">节点ID</param>
        /// <param name="workID"></param>
        /// <returns></returns>
        public static Work Flow_GetCurrentWork(Int64 workID)
        {
            Node nd = new Node(Flow_GetCurrentNode(workID));
            Work wk = nd.HisWork;
            wk.OID = workID;
            wk.Retrieve();
            wk.ResetDefaultVal();
            return wk;
        }
        /// <summary>
        /// 获取指定的workid 当前节点由哪些人可以执行.
        /// 2012-09-12 for lijian
        /// </summary>
        /// <param name="workID">需要找到的workid</param>
        /// <returns>返回当前处理人员列表.</returns>
        public static DataTable Flow_GetWorkerList(Int64 workID)
        {
            Paras ps = new Paras();
            ps.SQL = "SELECT * FROM WF_GenerWorkerList WHERE IsEnable=1 AND IsPass=0 AND WorkID=" + SystemConfig.AppCenterDBVarStr + "WorkID";
            ps.Add("WorkID", workID);
            return BP.DA.DBAccess.RunSQLReturnTable(ps);
        }
        /// <summary>
        /// 检查当前人员是否有权限处理当前的工作.
        /// 2012-09-12 for lijian
        /// </summary>
        /// <param name="workID">工作ID</param>
        /// <param name="userNo">要判断的操作人员</param>
        /// <returns>返回指定的人员是否有操作当前工作的权限</returns>
        public static bool Flow_CheckIsCanDoCurrentWork(Int64 workID,string userNo)
        {
            if (workID == 0)
                return true;

            Paras ps = new Paras();
            ps.SQL = "SELECT c.RunModel FROM WF_GenerWorkFlow a , WF_GenerWorkerlist b, WF_Node c WHERE  b.FK_Node=c.NodeID AND a.workid=b.workid AND a.FK_Node=b.FK_Node  AND b.fk_emp=" + SystemConfig.AppCenterDBVarStr + "FK_Emp AND b.IsEnable=1 AND a.workid=" + SystemConfig.AppCenterDBVarStr + "WorkID";
            ps.Add("FK_Emp", userNo);
            ps.Add("WorkID", workID);

            DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(ps);
            if (dt.Rows.Count == 0)
            {
                //ps = new Paras();
                //ps.SQL = "SELECT  FROM  ND"+int.Parse("")+"01" + SystemConfig.AppCenterDBVarStr + "FK_Emp AND b.IsEnable=1 AND a.workid=" + SystemConfig.AppCenterDBVarStr + "WorkID";
                //ps.Add("FK_Emp", userNo);
                //ps.Add("WorkID", workID);
                //dt = BP.DA.DBAccess.RunSQLReturnTable(ps);
                return false;
            }

            int i = int.Parse(dt.Rows[0][0].ToString());
            RunModel rm = (RunModel)i;
            switch (rm)
            {
                case RunModel.Ordinary:
                    return true;
                case RunModel.FL:
                    return true;
                case RunModel.HL:
                    return true;
                case RunModel.FHL:
                    return true;
                case RunModel.SubThread:
                    return true;
                default:
                    break;
            }

            if (DBAccess.RunSQLReturnValInt(ps) == 0)
                return false;
            return true;
        }
        /// <summary>
        /// 检查当前人员是否有查看指定流程的权限
        /// 2012-09-12 for lijian
        /// </summary>
        /// <param name="workID">工作ID</param>
        /// <param name="userNo">用户编号</param>
        /// <returns>返回是否可以查看</returns>
        public static bool Flow_CheckIsCanViewCurrentWork(Int64 workID, string userNo)
        {
            string dbstr = SystemConfig.AppCenterDBVarStr;
            Paras ps = new Paras();
            ps.SQL = "SELECT COUNT(*) FROM WF_TrackTemp WHERE WorkID=" + dbstr + "WorkID AND (EmpFrom=" + dbstr + "user1 OR EmpTo=" + dbstr + "user2)";
            ps.Add("WorkID", workID);
            ps.Add("user1", userNo);
            ps.Add("user2", userNo);
            if (DBAccess.RunSQLReturnValInt(ps) == 0)
                return false;
            return true;
        }
        /// <summary>
        /// 执行工作催办
        /// </summary>
        /// <param name="workID">工作ID</param>
        /// <param name="msg">催办消息</param>
        /// <returns></returns>
        public static string Flow_DoPress(Int64 workID, string msg)
        {
            GenerWorkFlow gwf = new GenerWorkFlow(workID);

            /*找到当前待办的工作人员*/
            GenerWorkerLists wls = new GenerWorkerLists(workID, gwf.FK_Node);

            string toEmp = "", toEmpName = "";
            string mailTitle = "催办:" + gwf.Title + ", 发送人:" + WebUser.Name;
            if (wls.Count == 1)
            {
                GenerWorkerList gwl = (GenerWorkerList)wls[0];
                toEmp = gwl.FK_Emp;
                toEmpName = gwl.FK_EmpText;
                TA.SMS.AddMsg(workID + DataType.CurrentDataTime, toEmp, mailTitle + msg, mailTitle, msg);
                gwl.PressTimes = gwl.PressTimes+1;
                gwl.Update();
                //gwl.PRI = 1;
            }
            else
            {
                foreach (GenerWorkerList wl in wls)
                {
                    if (wl.IsEnable == false)
                        continue;

                    toEmp += wl.FK_Emp + ",";
                    toEmpName += wl.FK_EmpText + ",";

                    TA.SMS.AddMsg(workID + DataType.CurrentDataTime, wl.FK_Emp, mailTitle + msg, mailTitle, msg);
                    //   wl.PressTimes = wl.PressTimes++;
                    wl.Update(GenerWorkerListAttr.PressTimes, wl.PressTimes+1);
                }
            }

            //写入日志.
            WorkNode wn = new WorkNode(workID, gwf.FK_Node);
            wn.AddToTrack(ActionType.Press, toEmp, toEmpName, gwf.FK_Node, gwf.NodeName, msg);

            return "系统已经把您的信息通知给:" + toEmpName;
        }
        #endregion 与流程有关的接口

        #region 工作有关接口
        /// <summary>
        /// 发起新工作
        /// </summary>
        /// <param name="flowNo">流程编号</param>
        /// <param name="ht">节点表单:主表数据以Key Value 方式传递(可以为空)</param>
        /// <param name="workDtls">节点表单:从表数据，从表名称与从表单的从表编号要对应(可以为空)</param>
        /// <param name="fk_nodeOfJumpTo">发起后要跳转到的节点(可以为空)</param>
        /// <param name="nextWorker">发起后要跳转到的节点并指定的工作人员(可以为空)</param>
        /// <returns>执行信息</returns>
        public static string Node_StartWork(string flowNo, Hashtable ht, DataSet workDtls, int fk_nodeOfJumpTo, string nextWorker)
        {
            Flow fl = new Flow(flowNo);
            Work wk = fl.NewWork();
            Int64 workID = wk.OID;
            if (ht != null)
            {
                foreach (string str in ht.Keys)
                    wk.SetValByKey(str, ht[str]);
            }

            wk.OID = workID;
            if (workDtls != null)
            {
                //保存从表
                foreach (DataTable dt in workDtls.Tables)
                {
                    foreach (MapDtl dtl in wk.HisMapDtls)
                    {
                        if (dt.TableName != dtl.No)
                            continue;
                        //获取dtls
                        GEDtls daDtls = new GEDtls(dtl.No);
                        daDtls.Delete(GEDtlAttr.RefPK, wk.OID); // 清除现有的数据.

                        GEDtl daDtl = daDtls.GetNewEntity as GEDtl;
                        daDtl.RefPK = wk.OID.ToString();

                        // 为从表复制数据.
                        foreach (DataRow dr in dt.Rows)
                        {
                            daDtl.ResetDefaultVal();
                            daDtl.RefPK = wk.OID.ToString();

                            //明细列.
                            foreach (DataColumn dc in dt.Columns)
                            {
                                //设置属性.
                                daDtl.SetValByKey(dc.ColumnName, dr[dc.ColumnName]);
                            }
                            daDtl.InsertAsOID(DBAccess.GenerOID("Dtl")); //插入数据.
                        }
                    }
                }
            }

            WorkNode wn = new WorkNode(wk, fl.HisStartNode);
            if (nextWorker != null && fk_nodeOfJumpTo != 0)
                return wn.NodeSend(new Node(fk_nodeOfJumpTo), nextWorker).ToMsgOfHtml();
            else
                return wn.NodeSend().ToMsgOfHtml();
        }
        /// <summary>
        /// 发送工作
        /// </summary>
        /// <param name="nodeID">节点编号</param>
        /// <param name="workID">工作ID</param>
        /// <returns>返回执行信息</returns>
        public static SendReturnObjs Node_SendWork(string fk_flow, Int64 workID)
        {
            return Node_SendWork(fk_flow, workID, new Hashtable(),null);
        }
        /// <summary>
        /// 发送工作
        /// </summary>
        /// <param name="fk_flow">流程编号</param>
        /// <param name="workID">工作ID</param>
        /// <param name="htWork">节点表单数据(Hashtable中的key与节点表单的字段名相同,value 就是字段值)</param>
        /// <returns>执行信息</returns>
        public static SendReturnObjs Node_SendWork(string fk_flow, Int64 workID, Hashtable htWork)
        {
            return Node_SendWork(fk_flow, workID, htWork, null);
        }
        /// <summary>
        /// 发送工作
        /// </summary>
        /// <param name="fk_flow">流程编号</param>
        /// <param name="workID">工作ID</param>
        /// <param name="htWork">节点表单数据(Hashtable中的key与节点表单的字段名相同,value 就是字段值)</param>
        /// <param name="workDtls">节点表单明从表数据(dataset可以包含多个table，每个table的名称与从表名称相同，列名与从表的字段相同, OID,RefPK列需要为空或者null )</param>
        /// <returns>执行信息</returns>
        public static SendReturnObjs Node_SendWork(string fk_flow, Int64 workID, Hashtable htWork, DataSet workDtls)
        {
            Node nd = new Node(Dev2Interface.Node_GetCurrentNodeID(fk_flow, workID));
            Work sw = nd.HisWork;
            if (workID != 0)
            {
                sw.OID = workID;
                sw.RetrieveFromDBSources();
            }
            sw.ResetDefaultVal();

            if (htWork != null)
            {
                foreach (string str in htWork.Keys)
                    sw.SetValByKey(str, htWork[str]);
            }

            sw.SetValByKey(StartWorkAttr.FK_Dept, WebUser.FK_Dept);
            sw.Rec = WebUser.No;
            sw.RecText = WebUser.Name;

            sw.BeforeSave();
            sw.Save();

            if (workDtls != null)
            {
                //保存从表
                foreach (DataTable dt in workDtls.Tables)
                {
                    foreach (MapDtl dtl in sw.HisMapDtls)
                    {
                        if (dt.TableName != dtl.No)
                            continue;
                        //获取dtls
                        GEDtls daDtls = new GEDtls(dtl.No);
                        daDtls.Delete(GEDtlAttr.RefPK, workID); // 清除现有的数据.

                        GEDtl daDtl = daDtls.GetNewEntity as GEDtl;
                        daDtl.RefPK = workID.ToString();

                        // 为从表复制数据.
                        foreach (DataRow dr in dt.Rows)
                        {
                            daDtl.ResetDefaultVal();
                            daDtl.RefPK = workID.ToString();

                            //明细列.
                            foreach (DataColumn dc in dt.Columns)
                            {
                                //设置属性.
                                daDtl.SetValByKey(dc.ColumnName, dr[dc.ColumnName]);
                            }
                            daDtl.InsertAsOID( DBAccess.GenerOID("Dtl") ); //插入数据.
                        }
                    }
                }
            }
            WorkNode wn = new WorkNode(sw, nd);
            return wn.NodeSend();
        }
        /// <summary>
        /// 执行抄送
        /// </summary>
        /// <param name="empNo">抄送人员编号</param>
        /// <param name="empName">抄送人员人员名称</param>
        /// <param name="fk_node">节点编号</param>
        /// <param name="workid">工作ID</param>
        /// <param name="fid">FID</param>
        public static void Node_CC(string empNo, string empName, int fk_node, Int64 workid, Int64 fid)
        {
            string title = BP.DA.DBAccess.RunSQLReturnStringIsNull("SELECT Title FROM WF_GenerWorkFlow WHERE WorkID=" + workid, "无标题");
            Node_CC(empNo, empName, fk_node, workid, fid, title, "");
        }
        /// <summary>
        /// 执行抄送
        /// </summary>
        /// <param name="empNo">抄送人员编号</param>
        /// <param name="empName">抄送人员人员名称</param>
        /// <param name="fk_node">节点编号</param>
        /// <param name="workid">工作ID</param>
        /// <param name="fid">FID</param>
        /// <param name="msgTitle">标题</param>
        /// <param name="msgDoc">内容</param>
        public static void Node_CC(string empNo, string empName, int fk_node, Int64 workid, Int64 fid, string msgTitle, string msgDoc)
        {
            Node nd = new Node(fk_node);
            CCList list = new CCList();
            list.MyPK = workid + "_" + fk_node + "_" + empNo;
            list.FK_Flow = nd.FK_Flow;

            Flow fl = nd.HisFlow;
            list.FlowName = fl.Name;
            list.FK_Node = fk_node;
            list.NodeName = nd.Name;
            list.Title = msgTitle;
            list.Doc = msgDoc;
            list.CCTo = empNo;
            list.RDT = DataType.CurrentDataTime;
            list.Rec = WebUser.No;
            list.RefWorkID = workid;
            list.FID = fid;
            try
            {
                list.Insert();
            }
            catch
            {
                list.CheckPhysicsTable();
                list.Update();
            }
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="nodeID">节点ID</param>
        /// <param name="workID">工作ID</param>
        /// <returns>返回保存的信息</returns>
        public static string Node_SaveWork(string fk_flow, Int64 workID)
        {
            return Node_SaveWork(fk_flow, workID, new Hashtable(),null);
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="fk_flow">流程编号</param>
        /// <param name="workID">workid</param>
        /// <param name="wk">节点表单参数</param>
        /// <returns></returns>
        public static string Node_SaveWork(string fk_flow, Int64 workID,Hashtable wk)
        {
            return Node_SaveWork(fk_flow, workID, wk, null);
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="nodeID">节点ID</param>
        /// <param name="workID">工作ID</param>
        /// <param name="htWork">工作数据</param>
        /// <returns>返回执行信息</returns>
        public static string Node_SaveWork(string fk_flow, Int64 workID, Hashtable htWork, DataSet dsDtls)
        {
            try
            {
                Node nd = new Node(Dev2Interface.Node_GetCurrentNodeID(fk_flow, workID));
                Work sw = nd.HisWork;
                sw.OID = workID;
                sw.Retrieve();
                sw.ResetDefaultVal();

                //加上标题， 设置节点状态.
                sw.SetValByKey("Title", WorkNode.GenerTitle(sw));
                sw.SetValByKey("NodeState", 0);

                if (htWork != null)
                {
                    foreach (string str in htWork.Keys)
                        sw.SetValByKey(str, htWork[str]);
                }

                //增加其它的字段.
                sw.SetValByKey(StartWorkAttr.FK_Dept,WebUser.FK_Dept);
                sw.BeforeSave();
                sw.Save();

                if (dsDtls != null)
                {
                    //保存从表
                    foreach (DataTable dt in dsDtls.Tables)
                    {
                        foreach (MapDtl dtl in sw.HisMapDtls)
                        {
                            if (dt.TableName != dtl.No)
                                continue;
                            //获取dtls
                            GEDtls daDtls = new GEDtls(dtl.No);
                            daDtls.Delete(GEDtlAttr.RefPK, workID); // 清除现有的数据.

                            GEDtl daDtl = daDtls.GetNewEntity as GEDtl;
                            daDtl.RefPK = workID.ToString();

                            // 为从表复制数据.
                            foreach (DataRow dr in dt.Rows)
                            {
                                daDtl.ResetDefaultVal();
                                daDtl.RefPK = workID.ToString();

                                //明细列.
                                foreach (DataColumn dc in dt.Columns)
                                {
                                    //设置属性.
                                    daDtl.SetValByKey(dc.ColumnName, dr[dc.ColumnName]);
                                }
                                daDtl.InsertAsOID(DBAccess.GenerOID("Dtl") ); //插入数据.
                            }
                        }
                    }
                }

                if (nd.SaveModel == SaveModel.NDAndRpt)
                {
                    /* 如果保存模式是节点表与Node与Rpt表. */
                    WorkNode wn = new WorkNode(sw, nd);
                    GEEntity rptGe = nd.HisFlow.HisFlowData;
                    rptGe.SetValByKey("OID", workID);
                    wn.rptGe = rptGe;
                    if (rptGe.RetrieveFromDBSources() == 0)
                    {
                        rptGe.SetValByKey("OID", workID);
                        wn.DoCopyRptWork(sw);

                        rptGe.SetValByKey(GERptAttr.FlowEmps, "@" + WebUser.No + "," + WebUser.Name);
                        rptGe.SetValByKey(GERptAttr.FlowStarter, WebUser.No);
                        rptGe.SetValByKey(GERptAttr.FlowStartRDT, DataType.CurrentDataTime);
                        rptGe.SetValByKey(GERptAttr.WFState, 0);
                        rptGe.SetValByKey(GERptAttr.FK_NY, DataType.CurrentYearMonth);
                        rptGe.SetValByKey(GERptAttr.FK_Dept, WebUser.FK_Dept);
                        rptGe.Insert();
                    }
                    else
                    {
                        wn.DoCopyRptWork(sw);
                        rptGe.Update();
                    }
                }
                return "保存成功.";
            }
            catch (Exception ex)
            {
                return "保存失败:" + ex.Message;
            }
        }
        /// <summary>
        /// 保存流程表单
        /// For shanghai lijian 2012-09-20
        /// </summary>
        /// <param name="fk_mapdata">流程表单ID</param>
        /// <param name="workID">工作ID</param>
        /// <param name="htData">流程表单数据Key Value 格式存放.</param>
        /// <returns>返回执行信息</returns>
        public static void Node_SaveFlowSheet(string fk_mapdata, Int64 workID, Hashtable htData)
        {
            Node_SaveFlowSheet(fk_mapdata, workID, htData, null);
        }
        /// <summary>
        /// 保存流程表单
        /// For shanghai lijian 2012-09-20
        /// </summary>
        /// <param name="fk_mapdata">流程表单ID</param>
        /// <param name="workID">工作ID</param>
        /// <param name="htData">流程表单数据Key Value 格式存放.</param>
        /// <param name="workDtls">从表数据</param>
        /// <returns>返回执行信息</returns>
        public static void Node_SaveFlowSheet(string fk_mapdata, Int64 workID, Hashtable htData, DataSet workDtls)
        {
            MapData md = new MapData(fk_mapdata);
            GEEntity en = md.HisGEEn;
            en.SetValByKey("OID", workID);
            int i = en.RetrieveFromDBSources();

            foreach (string key in htData.Keys)
                en.SetValByKey(key, htData[key].ToString());

            en.SetValByKey("OID", workID);

            FrmEvents fes = md.FrmEvents;
            fes.DoEventNode(FrmEventList.SaveBefore, en);
            if (i == 0)
                en.Insert();
            else
                en.Update();

            if (workDtls != null)
            {
                MapDtls dtls = new MapDtls(fk_mapdata);
                //保存从表
                foreach (DataTable dt in workDtls.Tables)
                {
                    foreach (MapDtl dtl in dtls)
                    {
                        if (dt.TableName != dtl.No)
                            continue;
                        //获取dtls
                        GEDtls daDtls = new GEDtls(dtl.No);
                        daDtls.Delete(GEDtlAttr.RefPK, workID); // 清除现有的数据.

                        GEDtl daDtl = daDtls.GetNewEntity as GEDtl;
                        daDtl.RefPK = workID.ToString();

                        // 为从表复制数据.
                        foreach (DataRow dr in dt.Rows)
                        {
                            daDtl.ResetDefaultVal();
                            daDtl.RefPK = workID.ToString();

                            //明细列.
                            foreach (DataColumn dc in dt.Columns)
                            {
                                //设置属性.
                                daDtl.SetValByKey(dc.ColumnName, dr[dc.ColumnName]);
                            }
                            daDtl.InsertAsOID(DBAccess.GenerOID("Dtl") ); //插入数据.
                        }
                    }
                }
            }

            fes.DoEventNode(FrmEventList.SaveAfter, en);
        }

        /// <summary>
        /// 增加下一步骤的接受人(用于当前步骤向下一步骤发送时增加接受人)
        /// </summary>
        /// <param name="workID">工作ID</param>
        /// <param name="formNodeID">节点ID</param>
        /// <param name="emps">如果多个就用逗号分开</param>
        public static void Node_AddNextStepAccepters(Int64 workID, int formNodeID, string emps)
        {
            SelectAccper sa = new SelectAccper();
            sa.Delete(SelectAccperAttr.FK_Node, formNodeID, SelectAccperAttr.WorkID, workID);
            emps = emps.Replace(" ", "");
            emps = emps.Replace(";", ",");
            emps = emps.Replace("@", ",");
            string[] strs = emps.Split(',');
            foreach (string emp in strs)
            {
                if (string.IsNullOrEmpty(emp))
                    continue;
                sa.MyPK = formNodeID + "_" + workID + "_" + emp;
                sa.FK_Emp = emp;
                sa.FK_Node = formNodeID;
                sa.WorkID = workID;
                sa.Insert();
            }
        }
        /// <summary>
        /// 节点工作挂起
        /// </summary>
        /// <param name="fk_flow">流程编号</param>
        /// <param name="workid">工作ID</param>
        /// <param name="way">挂起方式</param>
        /// <param name="reldata">解除挂起日期(可以为空)</param>
        /// <param name="msg">挂起原因</param>
        /// <returns>返回执行信息</returns>
        public static string Node_HungUpWork(string fk_flow, Int64 workid, int wayInt, string reldata, string msg)
        {
            HungUpWay way = (HungUpWay)wayInt;
            BP.WF.WorkFlow wf = new WorkFlow(fk_flow, workid);
            return wf.DoHungUp(way, reldata, msg);
        }
        /// <summary>
        /// 节点工作取消挂起
        /// </summary>
        /// <param name="fk_flow">流程编号</param>
        /// <param name="workid">工作ID</param>
        /// <param name="msg">取消挂起原因</param>
        /// <returns>执行信息</returns>
        public static void Node_UnHungUpWork(string fk_flow, Int64 workid, string msg)
        {
            BP.WF.WorkFlow wf = new WorkFlow(fk_flow, workid);
            wf.DoUnHungUp();
        }
        /// <summary>
        /// 工作移交
        /// </summary>
        /// <param name="workid">工作ID</param>
        /// <param name="toEmp">移交到人员(只给移交给一个人)</param>
        /// <param name="msg">移交消息</param>
        public static string Node_Forward(Int64 workid, string toEmp, string msg)
        {
            //ArrayList al = new ArrayList();
            //al.Add(toEmp);

            // 删除当前非配的工作。
            // 已经非配或者自动分配的任务。
            GenerWorkFlow gwf = new GenerWorkFlow(workid);
            int nodeId = gwf.FK_Node;
            Int64 workId = workid;

            DBAccess.RunSQL("UPDATE WF_GenerWorkerlist SET IsEnable=0  WHERE WorkID=" + workid + " AND FK_Node=" + nodeId);
            int i = DBAccess.RunSQL("UPDATE WF_GenerWorkerlist set IsEnable=1  WHERE WorkID=" + workid + " AND FK_Node=" + nodeId + " AND FK_Emp='" + toEmp + "'");
            Emp emp = new Emp(toEmp);
            GenerWorkerLists wls = null;
            GenerWorkerList wl = null;
            if (i == 0)
            {
                /*说明: 用其它的岗位上的人来处理的，就给他增加待办工作。*/
                wls = new GenerWorkerLists(workid, nodeId);
                wl = wls[0] as GenerWorkerList;
                wl.FK_Emp = toEmp.ToString();
                wl.FK_EmpText = emp.Name;
                wl.IsEnable = true;
                wl.Insert();

                // 清楚工作者，为转发消息所用.
                wls.Clear();
                wls.AddEntity(wl);
            }

            BP.WF.Node nd = new BP.WF.Node(nodeId);
            Work wk = nd.HisWork;
            wk.OID = workid;
            wk.Retrieve();
            wk.Emps = "," + toEmp + ".";
            wk.Rec = toEmp;
            wk.NodeState = NodeState.Forward;
            wk.Update();

            ForwardWork fw = new ForwardWork();
            fw.WorkID = workid;
            fw.FK_Node = nodeId;
            fw.ToEmp = toEmp;
            fw.ToEmpName = emp.Name;
            fw.Note = msg;
            fw.FK_Emp = WebUser.No;
            fw.FK_EmpName = WebUser.Name;
            fw.Insert();

            // 记录日志.
            WorkNode wn = new WorkNode(wk, nd);
            wn.AddToTrack(ActionType.Shift, toEmp, emp.Name, nd.NodeID, nd.Name, fw.Note);
            if (wn.HisNode.FocusField != "")
            {
                wn.HisWork.Update(wn.HisNode.FocusField, "");
            }

            if (wls == null)
                wls = new GenerWorkerLists(workid, nodeId, WebUser.No);

            // 写入消息。
            wn.AddIntoWacthDog(wls);

            string info = "@工作移交成功。@您已经成功的把工作移交给：" + emp.No + " , " + emp.Name;
            info += "@<a href='MyFlowInfo" + Glo.FromPageType + ".aspx?DoType=UnShift&FK_Flow=" + nd.FK_Flow + "&WorkID=" + workid + "' ><img src='./Img/UnDo.gif' border=0 />撤消工作移交</a>.";
            return info;
        }
        /// <summary>
        /// 执行工作退回(退回指定的点)
        /// </summary>
        /// <param name="nodeID">节点ID</param>
        /// <param name="workID">工作ID</param>
        /// <param name="msg">信息</param>
        /// <param name="returnToNode">要退回的节点</param>
        /// <param name="isBackToThisNode">是否要原路返回(默认为false)</param>
        /// <returns>返回执行信息</returns>
        public static string Node_ReturnWork(string fk_flow, Int64 workID, int returnToNode, string msg, bool isBackToThisNode)
        {
            //生成工作节点.
            WorkNode wn = new WorkNode(workID, Dev2Interface.Node_GetCurrentNodeID(fk_flow, workID));

            //执行退回.
            WorkNode rWn = wn.DoReturnWork(returnToNode, msg, isBackToThisNode);
           

            //返回退回信息.
            return "@任务被你成功退回到【{" + rWn.HisNode.Name + "}】，退回给【{" + rWn.HisWork.Rec + "}】。" ;
        }
        /// <summary>
        /// 执行工作退回(退回上一个点)
        /// </summary>
        /// <param name="nodeID">节点ID</param>
        /// <param name="workID">工作ID</param>
        /// <param name="msg">信息</param>
        /// <returns>返回执行信息</returns>
        public static void Node_ReturnWork_Del(string fk_flow, Int64 workID, string msg)
        {
            WorkNode wn = new WorkNode(workID, Dev2Interface.Node_GetCurrentNodeID(fk_flow, workID));
            wn.DoReturnWork(msg);
        }
        public static int Node_GetCurrentNodeID(string fk_flow, Int64 workid)
        {
            int nodeID = BP.DA.DBAccess.RunSQLReturnValInt("SELECT FK_Node FROM WF_GenerWorkFlow WHERE WorkID=" + workid + " AND FK_Flow='" + fk_flow + "'", 0);
            if (nodeID == 0)
                return int.Parse(fk_flow + "01");
            return nodeID;
        }
        /// <summary>
        /// 工作结束
        /// </summary>
        /// <param name="nodeID">节点ID</param>
        /// <param name="workID">工作ID</param>
        /// <returns>返回执行信息</returns>
        public static string Node_SetThisWorkOver(int nodeID, Int64 workID)
        {
            WorkNode wn = new WorkNode(workID, nodeID);
            return wn.DoSetThisWorkOver();
        }
        /// <summary>
        /// 创建一个工作
        /// </summary>
        /// <param name="nodeId">节点ID</param>
        /// <returns>返回一个工作实例</returns>
        public static Work Node_CreateWork(int nodeId)
        {
            Node nd = new Node(nodeId);
            return nd.HisWork;
        }
        /// <summary>
        /// 删除子线程
        /// </summary>
        /// <param name="fk_flow">流程编号</param>
        /// <param name="fid">流程ID</param>
        /// <param name="workid">工作ID</param>
        public static void Node_FHL_KillSubFlow(string fk_flow, Int64 fid, Int64 workid)
        {
            WorkFlow wkf = new WorkFlow(fk_flow, workid);
            wkf.DoDeleteWorkFlowByReal();
        }
        /// <summary>
        /// 合流点驳回子线程
        /// </summary>
        /// <param name="fk_flow">流程编号</param>
        /// <param name="fid">流程ID</param>
        /// <param name="workid">子线程ID</param>
        /// <param name="msg">驳回消息</param>
        public static string Node_FHL_DoReject(string fk_flow, int nodeOfReject, Int64 fid, Int64 workid, string msg)
        {
            WorkFlow wkf = new WorkFlow(fk_flow, workid);
            return wkf.DoReject(fid, nodeOfReject, msg);
        }
        /// <summary>
        /// 合流点把工作退回.
        /// </summary>
        /// <param name="fk_nodeOfHL">合流点编号</param>
        /// <param name="fid">FID</param>
        /// <param name="workid">子线程ID</param>
        /// <param name="msg">退回原因</param>
        /// <returns></returns>
        public static string Node_FHL_Return(int fk_nodeOfHL, Int64 fid, Int64 workid, string msg)
        {
            WorkNode wn = new WorkNode(fid, fk_nodeOfHL);
            Work wk = wn.HisWork;
            WorkNode mywn = null;
            mywn = wn.DoReturnWorkHL(workid, msg);

            // 调用退回事件.
            mywn.HisNode.MapData.FrmEvents.DoEventNode(EventListOfNode.ReturnAfter, wk);
            return "@任务被你成功退回到【{" + mywn.HisNode.Name + "}】，退回给【{" + mywn.HisWork.Rec + "}】。";
        }
        /// <summary>
        /// 跳转审核取回
        /// </summary>
        /// <param name="fromNodeID">从节点ID</param>
        /// <param name="workid">工作ID</param>
        /// <param name="tackToNodeID">取回到的节点ID</param>
        /// <returns></returns>
        public static string Node_Tackback(int fromNodeID, Int64 workid, int tackToNodeID)
        {
            /*
             * 1,首先检查是否有此权限.
             * 2, 执行工作跳转.
             * 3, 执行写入日志.
             */
            Node nd = new Node(tackToNodeID);
            switch (nd.HisDeliveryWay)
            {
                case DeliveryWay.ByPreviousNodeFormEmpsField:
                    break;
            }

            WorkNode wn = new WorkNode(workid, fromNodeID);
            string msg = wn.NodeSend(new Node(tackToNodeID),BP.Web.WebUser.No).ToMsgOfHtml();
            wn.AddToTrack(ActionType.Tackback, WebUser.No, WebUser.Name, tackToNodeID, nd.Name, "执行跳转审核的取回.");
            return msg;
        }
        #endregion 工作有关接口

        #region 流程属性与节点属性变更接口.
        public static string ChangeAttr_Node()
        {
            return null;
        }
        /// <summary>
        /// 更改流程属性.
        /// </summary>
        /// <param name="fk_flow">流程编号</param>
        /// <param name="isEnableFlow">启用与禁用 true启用  false禁用</param>
        /// <returns>执行结果</returns>
        public static string ChangeAttr_Flow(string fk_flow, bool isEnableFlow)
        {
            return ChangeAttr_Flow(fk_flow, FlowAttr.IsOK, isEnableFlow,null,null);
        }
        /// <summary>
        /// 更改流程属性
        /// </summary>
        /// <param name="fk_flow">流程编号</param>
        /// <param name="attr1">字段1</param>
        /// <param name="v1">值1</param>
        /// <param name="attr2">字段2(可为null)</param>
        /// <param name="v2">值2(可为null)</param>
        /// <returns>执行结果</returns>
        public static string ChangeAttr_Flow(string fk_flow, string attr1, object v1, string attr2, object v2)
        {
            Flow fl = new Flow(fk_flow);
            if (attr1 != null)
                fl.SetValByKey(attr1, v1);
            if (attr2 != null)
                fl.SetValByKey(attr2, v2);
            fl.Update();
            return "修改成功";
        }
        /// <summary>
        /// 获取数据接口 For boco 2012-12-22
        /// 标记不同获得的数据也不同.
        /// Flow=流程数据
        /// </summary>
        /// <param name="flag">标记</param>
        /// <returns></returns>
        public static DataSet DS_GetDataByFlag(string flag)
        {
            DataSet ds = new DataSet();
            switch (flag)
            {
                case "Flow":
                    Flows fls = new Flows();
                    fls.RetrieveAll();
                    DataTable dt = fls.ToDataTableField();
                    dt.TableName = "WF_Flow";
                    dt.Columns.Add(new DataColumn("IsEnableText", typeof(string)));
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr[FlowAttr.IsOK].ToString() == "1")
                            dr["IsEnableText"] = "启用";
                        else
                            dr["IsEnableText"] = "禁用";
                    }
                    ds.Tables.Add(dt);
                    return ds;
                default:
                    break;
            }
            throw new Exception("标记错误:"+flag);
        }
        #endregion 流程属性与节点属性变更接口.
    }
}
