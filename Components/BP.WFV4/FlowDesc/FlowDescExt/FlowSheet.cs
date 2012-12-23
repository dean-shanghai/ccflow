using System;
using System.Data;
using System.Collections;
using BP.DA;
using BP.Port;
using BP.En;
using BP.Web;

namespace BP.WF.Ext
{
    /// <summary>
    /// 流程
    /// </summary>
    public class FlowSheet : EntityNoName
    {
        #region 构造方法
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                if (Web.WebUser.No == "admin")
                    uac.IsUpdate = true;
                return uac;
            }
        }
        /// <summary>
        /// 流程
        /// </summary>
        public FlowSheet()
        {
        }
        /// <summary>
        /// 流程
        /// </summary>
        /// <param name="_No">编号</param>
        public FlowSheet(string _No)
        {
            this.No = _No;
            if (SystemConfig.IsDebug)
            {
                int i = this.RetrieveFromDBSources();
                if (i == 0)
                    throw new Exception("流程编号不存在");
            }
            else
            {
                this.Retrieve();
            }
        }
        /// <summary>
        /// 重写基类方法
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("WF_Flow");

                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = this.ToE("Flow", "流程");
                map.CodeStruct = "3";

                map.AddTBStringPK(FlowAttr.No, null, this.ToE("No", "编号"), true, true, 1, 10, 3);
                map.AddDDLEntities(FlowAttr.FK_FlowSort, "01", this.ToE("FlowSort", "流程类别"),
                    new FlowSorts(), true);
                map.AddTBString(FlowAttr.Name, null, this.ToE("Name", "名称"), true, false, 0, 50, 10, true);
                map.AddBoolean(FlowAttr.IsOK, true, this.ToE("IsEnable", "是否起用"), true, true);

                map.AddDDLSysEnum(FlowAttr.FlowRunWay, (int)FlowRunWay.HandWork, this.ToE("RunWay", "运行方式"),
                    true, true, FlowAttr.FlowRunWay, "@0=手工启动@1=指定人员按时启动@2=数据集按时启动@3=触发式启动");

                map.AddTBString(FlowAttr.RunObj, null, "运行内容", true, false, 0, 100, 10, true);
                map.AddBoolean(FlowAttr.IsCanStart, true, this.ToE("IsCanRunBySelf", "可以独立启动否？(独立启动的流程可以显示在发起流程列表里)"), true, true, true);

                map.AddBoolean(FlowAttr.IsMD5, false, "是否是数据加密流程(MD5数据加密防篡改)", true, true,true);

                map.AddTBStringDoc(FlowAttr.Note, null, this.ToE("Note", "备注"), 
                    true, false, true);
                map.AddTBString(FlowAttr.StartListUrl, null, this.ToE("StartListUrl", "导航Url"), true, false, 0, 500, 10, true);

                map.AddDDLSysEnum(FlowAttr.AppType, (int)FlowAppType.Normal,"流程应用类型",
                  true, true, "FlowAppType", "@0=正常的@1=工程类(具有项目组概念)");

                map.AddDDLSysEnum(FlowAttr.TimelineRole, (int)TimelineRole.ByNodeSet, "时效性规则",
                 true, true, FlowAttr.TimelineRole, "@0=按节点(由节点属性来定义)@1=按发起人(开始节点SysFlowOfSDT字段计算)");

                map.AddSearchAttr(BP.WF.FlowAttr.FK_FlowSort);

                RefMethod rm = new RefMethod();
                rm.Title = this.ToE("CCNode", "抄送节点"); // "抄送节点";
                rm.ToolTip = "当抄送方式设置为抄送节点时，此设置才有效。";
                rm.Icon = "/Images/Btn/Confirm.gif";
                rm.ClassMethodName = this.ToString() + ".DoCCNode";
                //map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = this.ToE("DesignCheckRpt", "检查报告"); // "设计检查报告";
                //rm.ToolTip = "检查流程设计的问题。";
                rm.Icon = "/Images/Btn/Confirm.gif";
                rm.ClassMethodName = this.ToString() + ".DoCheck";
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = this.ToE("Rpt", "设计报表"); // "报表运行";
                rm.Icon = "/Images/Btn/View.gif";
                rm.ClassMethodName = this.ToString() + ".DoOpenRpt()";
                //rm.Icon = "/Images/Btn/Table.gif"; 
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Icon = "/Images/Btn/Delete.gif";
                rm.Title = "删除数据"; // this.ToE("DelFlowData", "删除数据"); // "删除数据";
                rm.Warning = this.ToE("AYS", "您确定要执行删除流程数据吗?");// "您确定要执行删除流程数据吗？";
                rm.ClassMethodName = this.ToString() + ".DoDelData";
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Icon = "/Images/Btn/Delete.gif";
                rm.Title = "删除单个流程"; // this.ToE("DelFlowData", "删除数据"); // "删除数据";
                rm.ClassMethodName = this.ToString() + ".DoDelDataOne";
                rm.HisAttrs.AddTBInt("WorkID",0, "输入工作ID",true,false);
                rm.HisAttrs.AddTBString("sd", null, "删除备注", true, false,0,100,100);
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Icon = "/Images/Btn/DTS.gif";
                rm.Title = "重新生成报表数据"; // "删除数据";
                rm.Warning = this.ToE("AYS", "您确定要执行吗? 注意:此方法耗费资源。");// "您确定要执行删除流程数据吗？";
                rm.ClassMethodName = this.ToString() + ".DoReloadRptData";
                map.AddRefMethod(rm);


                rm = new RefMethod();
                rm.Title = "设置自动发起数据源";
                rm.Icon = "/Images/Btn/DTS.gif";

                rm.ClassMethodName = this.ToString() + ".DoSetStartFlowDataSources()";
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = "手工启动定时任务";
                rm.Icon = "/Images/Btn/DTS.gif";
                rm.Warning = this.ToE("AYS", "您确定要执行吗? 注意:对于数据量交大的数据因为web上执行时间的限时问题，会造成执行失败。");// "您确定要执行删除流程数据吗？";
                rm.ClassMethodName = this.ToString() + ".DoAutoStartIt()";
                map.AddRefMethod(rm);


                rm = new RefMethod();
                rm.Title = "流程数据管理";
                rm.Icon = "/Images/Btn/DTS.gif";
                rm.ClassMethodName = this.ToString() + ".DoDataManger()";
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = "重新生成流程标题";
                rm.Icon = "/Images/Btn/DTS.gif";
                rm.ClassMethodName = this.ToString() + ".DoGenerTitle()";
                rm.Warning = "您确定要根据新的规则重新产生标题吗？";
                map.AddRefMethod(rm);


                rm = new RefMethod();
                rm.Title = "回滚已经完成的流程";
                rm.Icon = "/Images/Btn/DTS.gif";
                rm.ClassMethodName = this.ToString() + ".DoRebackFlowData()";
               // rm.Warning = "您确定要回滚它吗？";
                rm.HisAttrs.AddTBInt("workid", 0, "请输入要会滚WorkID", true, false);
                rm.HisAttrs.AddTBString("note", null, "回滚原因", true, false,0,600,200);
                map.AddRefMethod(rm);

                //rm = new RefMethod();
                //rm.Title = "设置自动发起"; // "报表运行";
                //rm.Icon = "/Images/Btn/View.gif";
                //rm.ClassMethodName = this.ToString() + ".DoOpenRpt()";
                ////rm.Icon = "/Images/Btn/Table.gif"; 
                //map.AddRefMethod(rm);

                //rm = new RefMethod();
                //rm.Title = this.ToE("Event", "事件"); // "报表运行";
                //rm.Icon = "/Images/Btn/View.gif";
                //rm.ClassMethodName = this.ToString() + ".DoOpenRpt()";
                ////rm.Icon = "/Images/Btn/Table.gif";
                //map.AddRefMethod(rm);

                //rm = new RefMethod();
                //rm.Title = this.ToE("FlowSheetDataOut", "数据转出定义");  //"数据转出定义";
                ////  rm.Icon = "/Images/Btn/Table.gif";
                //rm.ToolTip = "在流程完成时间，流程数据转储存到其它系统中应用。";
                //rm.ClassMethodName = this.ToString() + ".DoExp";
                //map.AddRefMethod(rm);


                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

        #region  公共方法
        public string DoRebackFlowData(int workid, string note)
        {
            if (note.Length <= 2)
                return "请填写恢复已完成的流程原因.";

            GenerWorkFlow gwf = new GenerWorkFlow();
            gwf.WorkID = workid;
            int startNode = int.Parse(this.No + "01");
            try
            {
                string sql = "SELECT a.* FROM V" + this.No + " a, WF_Node b WHERE a.FK_Node=b.NodeID and OID=" + workid + " ORDER BY a.CDT DESC ";
                System.Data.DataTable dt = DBAccess.RunSQLReturnTable(sql);
                if (dt.Rows.Count == 0)
                    return "@工作ID为:" + workid + "的数据不存在.";

                gwf.WorkID = workid;
                if (gwf.RetrieveFromDBSources() == 1)
                    return "@当前工作ID为:" + workid + "的流程没有结束。";

                int endNode = int.Parse(dt.Rows[0]["FK_Node"].ToString());
                Node endN = new Node(endNode);

                Flow fl = new Flow(this.No);
                string starter = "";
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["FK_Node"].ToString() == startNode.ToString())
                    {
                        /*如果是开始节点 */
                        gwf.WorkID = workid;
                        gwf.FK_Dept = dr["FK_Dept"].ToString();
                        gwf.FK_Flow = this.No;
                        gwf.FK_Node = endNode;
                        //结束节点.
                        gwf.NodeName = endN.Name;

                        Dept dept = new Dept(gwf.FK_Dept);
                        gwf.DeptName = dept.Name;

                        gwf.Rec = dr["Rec"].ToString();
                        gwf.WFState =  WFState.Runing;
                        gwf.Title = DBAccess.RunSQLReturnString("select Title from nd" + startNode + " where oid=" + workid);
                        gwf.RDT = dr["RDT"].ToString();
                        gwf.FK_FlowSort = fl.FK_FlowSort;
                        gwf.FlowName = this.Name;
                        Emp emp = new Emp(gwf.Rec);
                        gwf.RecName = emp.Name;
                        gwf.PRI = 1;
                        gwf.Insert();
                        break;
                    }
                }

                GenerWorkerList currWl = new GenerWorkerList();
                foreach (DataRow dr in dt.Rows)
                {
                    GenerWorkerList wl = new GenerWorkerList();
                    wl.FID = 0;
                    wl.WorkID = workid;
                    wl.FK_Emp = dr["Rec"].ToString();
                    Emp emp = new Emp(wl.FK_Emp);
                    wl.FK_EmpText = emp.Name;

                    wl.FK_Node = int.Parse(dr["FK_Node"].ToString());

                    Node nd = new Node(wl.FK_Node);
                    wl.FK_NodeText = nd.Name;
                    wl.FID = 0;
                    wl.FK_Flow = this.No;
                    wl.FK_Dept = emp.FK_Dept;
                    wl.SDT = dr["RDT"].ToString();
                    wl.SDT = dr["RDT"].ToString();
                    wl.DTOfWarning = dr["RDT"].ToString();
                    wl.WarningDays = nd.WarningDays;
                    wl.IsEnable = true;
                    if (gwf.FK_Node == wl.FK_Node)
                    {
                        currWl = wl;
                        wl.IsPass = false;
                    }
                    else
                        wl.IsPass = true;

                    wl.WhoExeIt = nd.WhoExeIt;
                    wl.Insert();
                }

                string sqls = "UPDATE ND" + startNode + " SET WFState=0 WHERE OID=" + workid;
                sqls += "@UPDATE ND" + int.Parse(this.No) + "Rpt SET WFState=0 WHERE OID=" + workid;
                sqls += "@UPDATE ND" + endNode + " SET NodeState=0 WHERE OID=" + workid;
                DBAccess.RunSQLs(sqls);

                WorkNode wn = new WorkNode(workid, currWl.FK_Node);
                wn.AddToTrack(ActionType.RebackOverFlow, currWl.FK_Emp, currWl.FK_EmpText, currWl.FK_Node, currWl.FK_NodeText, note);
                return "@已经还原成功,现在的流程已经复原到最后一个节点处理人身上. @当前工作处理人为(" + currWl.FK_Emp + " , " + currWl.FK_EmpText + "), 当前节点为:" + endN.Name + ". @请通知他处理工作.";
            }
            catch (Exception ex)
            {
                gwf.Delete();
                GenerWorkerList wl = new GenerWorkerList();
                wl.Delete(GenerWorkerListAttr.WorkID, workid);
                string sqls = "";
                sqls += "@UPDATE ND" + int.Parse(this.No) + "Rpt SET WFState=1 WHERE OID=" + workid;
                DBAccess.RunSQLs(sqls);
                return "<font color=red>会滚期间出现错误</font><hr>" + ex.Message;
            }
        }
        /// <summary>
        /// 重新产生标题，根据新的规则.
        /// </summary>
        public string DoGenerTitle()
        {
            if (WebUser.No != "admin")
                return "非admin用户不能执行。";
            Flow fl = new Flow(this.No);
            Node nd = fl.HisStartNode;
            Works wks = nd.HisWorks;
            wks.RetrieveAllFromDBSource(WorkAttr.Rec);
            string table = nd.HisWork.EnMap.PhysicsTable;
            string tableRpt = "ND" + int.Parse(this.No) + "Rpt";
            foreach (Work wk in wks)
            {
                if (wk.NodeState == NodeState.Init)
                    continue;
                if (wk.Rec != WebUser.No)
                {
                    BP.Web.WebUser.Exit();
                    try
                    {
                        Emp emp = new Emp(wk.Rec);
                        BP.Web.WebUser.SignInOfGener(emp);
                    }
                    catch
                    {
                        continue;
                    }
                }
                string sql = "";
                string title = WorkNode.GenerTitle(wk);
                Paras ps = new Paras();
                ps.Add("Title", title);
                ps.Add("OID", wk.OID);
                ps.SQL = "UPDATE " + table + " SET Title=" + SystemConfig.AppCenterDBVarStr + "Title WHERE OID=" + SystemConfig.AppCenterDBVarStr + "OID";
                DBAccess.RunSQL(ps);

                ps.SQL = "UPDATE " + tableRpt + " SET Title=" + SystemConfig.AppCenterDBVarStr + "Title WHERE OID=" + SystemConfig.AppCenterDBVarStr + "OID";
                DBAccess.RunSQL(ps);

                ps.SQL = "UPDATE WF_GenerWorkFlow SET Title=" + SystemConfig.AppCenterDBVarStr + "Title WHERE WorkID=" + SystemConfig.AppCenterDBVarStr + "OID";
                DBAccess.RunSQL(ps);

                ps.SQL = "UPDATE WF_GenerFH SET Title=" + SystemConfig.AppCenterDBVarStr + "Title WHERE FID=" + SystemConfig.AppCenterDBVarStr + "OID";
                DBAccess.RunSQLs(sql);
            }
            Emp emp1 = new Emp("admin");
            BP.Web.WebUser.SignInOfGener(emp1);
            return "全部生成成功,影响数据(" + wks.Count + ")条";
        }
        /// <summary>
        /// 流程数据管理
        /// </summary>
        /// <returns></returns>
        public string DoDataManger()
        {
            PubClass.WinOpen("./../WF/Admin/FlowDB.aspx?s=d34&FK_Flow=" + this.No + "&ExtType=StartFlow&RefNo==", 700, 500);
            return null;
        }
        /// <summary>
        /// 定义报表
        /// </summary>
        /// <returns></returns>
        public string DoAutoStartIt()
        {
            Flow fl = new Flow();
            fl.No = this.No;
            fl.RetrieveFromDBSources();
            return fl.DoAutoStartIt();
        }

        public string DoDelDataOne(int workid, string sd)
        {
            return "删除成功 workid="+workid+"  理由:"+sd;
        }

        

        public string DoSetStartFlowDataSources()
        {
            string flowID=int.Parse(this.No).ToString()+"01";
            PubClass.WinOpen("./../WF/MapDef/MapExt.aspx?s=d34&FK_MapData=ND" + flowID + "&ExtType=StartFlow&RefNo==", 700, 500);
            return null;
        }
        public string DoCCNode()
        {
            PubClass.WinOpen("./../WF/Admin/CCNode.aspx?FK_Flow=" + this.No, 400, 500);
            return null;
        }
        /// <summary>
        /// 执行检查
        /// </summary>
        /// <returns></returns>
        public string DoCheck()
        {
            Flow fl = new Flow();
            fl.No = this.No;
            fl.RetrieveFromDBSources();
            return fl.DoCheck();
        }

        public string DoReloadRptData()
        {
            Flow fl = new Flow();
            fl.No = this.No;
            fl.RetrieveFromDBSources();
            return fl.DoReloadRptData();
        }

        public string DoDelData()
        {
            Flow fl = new Flow();
            fl.No = this.No;
            fl.RetrieveFromDBSources();
            return fl.DoDelData();
        }

        /// <summary>
        /// 设计数据转出
        /// </summary>
        /// <returns></returns>
        public string DoExp()
        {
            Flow fl = new Flow();
            fl.No = this.No;
            fl.RetrieveFromDBSources();
            return fl.DoExp();
        }
        /// <summary>
        /// 定义报表
        /// </summary>
        /// <returns></returns>
        public string DoDRpt()
        {
            Flow fl = new Flow();
            fl.No = this.No;
            fl.RetrieveFromDBSources();
            return fl.DoDRpt();
        }
        /// <summary>
        /// 运行报表
        /// </summary>
        /// <returns></returns>
        public string DoOpenRpt()
        {
            Flow fl = new Flow();
            fl.No = this.No;
            fl.RetrieveFromDBSources();
            return fl.DoOpenRpt();
        }
        /// <summary>
        /// 更新之后的事情，也要更新缓存。
        /// </summary>
        protected override void afterUpdate()
        {
            Flow fl = new Flow();
            fl.No = this.No;
            fl.RetrieveFromDBSources();
            fl.Update();
            base.afterUpdate();
        }
        #endregion
    }
    /// <summary>
    /// 流程集合
    /// </summary>
    public class FlowSheets : EntitiesNoName
    {
        #region 查询
        /// <summary>
        /// 查询出来全部的在生存期间内的流程
        /// </summary>
        /// <param name="FlowSort">流程类别</param>
        /// <param name="IsCountInLifeCycle">是不是计算在生存期间内 true 查询出来全部的 </param>
        public void Retrieve(string FlowSort)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(BP.WF.FlowAttr.FK_FlowSort, FlowSort);
            qo.addOrderBy(BP.WF.FlowAttr.No);
            qo.DoQuery();
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 工作流程
        /// </summary>
        public FlowSheets() { }
        /// <summary>
        /// 工作流程
        /// </summary>
        /// <param name="fk_sort"></param>
        public FlowSheets(string fk_sort)
        {
            this.Retrieve(BP.WF.FlowAttr.FK_FlowSort, fk_sort);
        }
        #endregion

        #region 得到实体
        /// <summary>
        /// 得到它的 Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new FlowSheet();
            }
        }
        #endregion
    }
}

