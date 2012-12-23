using System;
using System.Data;
using BP.DA;
using BP.En;
using System.Collections;
using BP.Port;

namespace BP.WF.Ext
{
    /// <summary>
    /// 这里存放每个节点的信息.
    /// </summary>
    public class NodeO : Entity
    {
        /// <summary>
        /// 访问规则
        /// </summary>
        public ReturnRole HisReturnRole
        {
            get
            {
                return (ReturnRole)this.GetValIntByKey(NodeAttr.ReturnRole);
            }
            set
            {
                this.SetValByKey(NodeAttr.ReturnRole, (int)value);
            }
        }
        public FJOpen HisFJOpen
        {
            get
            {
                return (FJOpen)this.GetValIntByKey(NodeAttr.FJOpen);
            }
            set
            {
                this.SetValByKey(NodeAttr.FJOpen, (int)value);
            }
        }
        /// <summary>
        /// 访问规则
        /// </summary>
        public DeliveryWay HisDeliveryWay
        {
            get
            {
                return (DeliveryWay)this.GetValIntByKey(NodeAttr.DeliveryWay);
            }
            set
            {
                this.SetValByKey(NodeAttr.DeliveryWay, (int)value);
            }
        }
        public int Step
        {
            get
            {
                return this.GetValIntByKey(NodeAttr.Step);
            }
            set
            {
                this.SetValByKey(NodeAttr.Step, value);
            }
        }
        public int NodeID
        {
            get
            {
                return this.GetValIntByKey(NodeAttr.NodeID);
            }
            set
            {
                this.SetValByKey(NodeAttr.NodeID, value);
            }
        }
        public string Name
        {
            get
            {
                return this.GetValStringByKey(NodeAttr.Name);
            }
            set
            {
                this.SetValByKey(NodeAttr.Name, value);
            }
        }
        public string FK_Flow
        {
            get
            {
                return this.GetValStringByKey(NodeAttr.FK_Flow);
            }
            set
            {
                this.SetValByKey(NodeAttr.FK_Flow, value);
            }
        }
        public string FlowName
        {
            get
            {
                return this.GetValStringByKey(NodeAttr.FlowName);
            }
            set
            {
                this.SetValByKey(NodeAttr.FlowName, value);
            }
        }
        /// <summary>
        /// 接受人sql
        /// </summary>
        public string RecipientSQL
        {
            get
            {
                return this.GetValStringByKey(NodeAttr.RecipientSQL);
            }
            set
            {
                this.SetValByKey(NodeAttr.RecipientSQL, value);
            }
        }
        /// <summary>
        /// 是否可以退回
        /// </summary>
        public bool ReturnEnable
        {
            get
            {
                return this.GetValBooleanByKey(BtnAttr.ReturnRole);
            }
        }
        public bool AthEnable
        {
            get
            {
                if (HisFJOpen == FJOpen.None)
                    return false;
                return true;
            }
        }
        public override string PK
        {
            get
            {
                return "NodeID";
            }
        }
        protected override bool beforeUpdate()
        {
            Node nd = new Node(this.NodeID);
            nd.Update();
            return base.beforeUpdate();
        }


        #region 初试化全局的 Node
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                if (BP.Web.WebUser.No == "admin")
                    uac.IsUpdate = true;
                return uac;
            }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 节点
        /// </summary>
        public NodeO() { }
        /// <summary>
        /// 重写基类方法
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("WF_Node");
                map.EnDesc = this.ToE("Node", "节点");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;

                // 基础属性
                map.AddTBIntPK(NodeAttr.NodeID, 0, this.ToE("NodeID", "节点ID"), true, true);
                map.AddTBInt(NodeAttr.Step, 0, "步骤(无计算意义)", true, false);
                map.AddTBString(NodeAttr.FK_Flow, null, "流程编号", false, false, 3, 3, 10, false);

                map.AddTBString(NodeAttr.Name, null, this.ToE("Name", "名称"), true, true, 0, 100, 10, true);
                map.AddBoolean(NodeAttr.IsTask, true, this.ToE("IsTask", "允许分配工作否?"), true, true, false);
                map.AddBoolean(NodeAttr.IsRM, true, "是否起用投递路径自动记忆功能?", true, true, false);
                map.AddBoolean(NodeAttr.IsForceKill, false, "是否可以强制删除子流程(对合流点有效)", true, true, true);
                map.AddBoolean(NodeAttr.IsBackTracking, false, "是否可以在退回后原路返回(只有启用退回功能才有效)", true, true, true);


                // map.AddTBInt(NodeAttr.PassRate, 100, "通过率(对于合流节点有效)", true, true);
                map.AddTBDecimal(NodeAttr.PassRate, 0, "完成通过率(对合流点有效)", true, false);


                map.AddDDLSysEnum(NodeAttr.RunModel, 0, this.ToE("RunModel", "运行模式"),
                    true, true, NodeAttr.RunModel, "@0=普通@1=合流@2=分流@3=分合流@4=子线程");
            
                //map.AddDDLSysEnum(NodeAttr.FLRole, 0, this.ToE("FLRole", "分流规则"), true, true, NodeAttr.FLRole,
                //    "@0=按接受人@1=按部门@2=按岗位");

                map.AddTBString(NodeAttr.FocusField, null, "焦点字段", true, false, 0, 50, 10);

                map.AddDDLSysEnum(NodeAttr.DeliveryWay, 0, "访问规则", true, true);
                map.AddTBString(NodeAttr.RecipientSQL, null, "访问规则处理内容", true, false, 0, 500, 10, true);

                map.AddDDLSysEnum(NodeAttr.WhoExeIt, 0, "谁执行它",
              true, true, NodeAttr.WhoExeIt, "@0=操作员执行@1=机器执行@2=混合执行");

                map.AddDDLSysEnum(NodeAttr.FormType, 0, this.ToE("FormType", "节点表单类型"), true, true);
                map.AddTBString(NodeAttr.FormUrl, null, this.ToE("FormUrl", "表单URL"), true, false, 0, 200, 10, true);
                map.AddTBString(NodeAttr.FocusField, null, "焦点字段", false, false, 0, 50, 10, false);

                map.AddDDLSysEnum(NodeAttr.TurnToDeal, 0, "发送后转向",
                 true, true, NodeAttr.TurnToDeal, "@0=提示ccflow默认信息@1=提示指定信息@2=转向指定的url@3=按照条件转向");

                map.AddTBString(NodeAttr.TurnToDealDoc, null, "转向处理内容", true, false, 0, 999, 10, true);

                map.AddTBString(NodeAttr.JumpSQL, null, "可跳转的节点", true, false, 0, 200, 10, true);

                //map.AddBoolean("IsSkipReturn", false, "是否可以跨级撤销", true, true, true);
                map.AddTBDateTime("DTFrom", "生命周期从", true, true);
                map.AddTBDateTime("DTTo", "生命周期到", true, true);

                //保存方式.
                map.AddDDLSysEnum(NodeAttr.SubThreadType, 0, "子线程类型(对子线程节点有效)", true, true,
                    NodeAttr.SubThreadType, "@0=同表单@1=异表单");
                map.AddDDLSysEnum(NodeAttr.SaveModel, 0, "保存方式", true, true);


                #region  功能按钮状态
                map.AddTBString(BtnAttr.SendLab, "发送", "发送按钮标签", true, false, 0, 50, 10);
             //   map.AddBoolean(BtnAttr.SendEnable, true, "是否启用", true, false);
                map.AddTBString(BtnAttr.SendJS, "", "按钮JS函数", true, false, 0, 50, 10);


                map.AddTBString(BtnAttr.SaveLab, "保存", "保存按钮标签", true, false, 0, 50, 10);
                map.AddBoolean(BtnAttr.SaveEnable, true, "是否启用", true, true);

                map.AddTBString(BtnAttr.JumpWayLab, "跳转", "跳转按钮标签", true, false, 0, 50, 10);
                map.AddDDLSysEnum(NodeAttr.JumpWay, 0, "跳转规则",
           true, true, NodeAttr.JumpWay);


                map.AddTBString(BtnAttr.ReturnLab, "退回", "退回按钮标签", true, false, 0, 50, 10);
                map.AddDDLSysEnum(NodeAttr.ReturnRole, 0, this.ToE("ReturnRole", "退回规则"),
           true, true, NodeAttr.ReturnRole);

                map.AddTBString(BtnAttr.CCLab, "抄送", "抄送按钮标签", true, false, 0, 50, 10);
                map.AddDDLSysEnum(NodeAttr.CCRole, 0, "抄送规则",
           true, true, NodeAttr.CCRole);


                map.AddTBString(BtnAttr.ShiftLab, "移交", "移交按钮标签", true, false, 0, 50, 10);
                map.AddBoolean(BtnAttr.ShiftEnable, true, "是否启用", true, true);

                map.AddTBString(BtnAttr.DelLab, "删除", "删除按钮标签", true, false, 0, 50, 10);
                map.AddBoolean(BtnAttr.DelEnable, true, "是否启用", true, true);

                map.AddTBString(BtnAttr.EndFlowLab, "结束流程", "结束流程按钮标签", true, false, 0, 50, 10);
                map.AddBoolean(BtnAttr.EndFlowEnable, false, "是否启用", true, true);
               

                map.AddTBString(BtnAttr.PrintDocLab, "打印单据", "打印单据按钮标签", true, false, 0, 50, 10);
                map.AddBoolean(BtnAttr.PrintDocEnable, false, "是否启用", true, true);

                //map.AddTBString(BtnAttr.AthLab, "附件", "附件按钮标签", true, false, 0, 50, 10);
                //map.AddDDLSysEnum(NodeAttr.FJOpen, 0, this.ToE("FJOpen", "附件权限"), true, true, 
                //    NodeAttr.FJOpen, "@0=关闭附件@1=操作员@2=工作ID@3=流程ID");

                map.AddTBString(BtnAttr.TrackLab, "轨迹", "轨迹按钮标签", true, false, 0, 50, 10);
                map.AddBoolean(BtnAttr.TrackEnable, true, "是否启用", true, true);

                map.AddTBString(BtnAttr.HungLab, "挂起", "挂起按钮标签", true, false, 0, 50, 10);
                map.AddBoolean(BtnAttr.HungEnable, false, "是否启用", true, true);

                map.AddTBString(BtnAttr.SelectAccepterLab, "接受人", "接受人按钮标签", true, false, 0, 50, 10);
                map.AddDDLSysEnum(BtnAttr.SelectAccepterEnable, 0, "工作方式",
          true, true, BtnAttr.SelectAccepterEnable);


                map.AddTBString(BtnAttr.SearchLab, "查询", "查询按钮标签", true, false, 0, 50, 10);
                map.AddBoolean(BtnAttr.SearchEnable, false, "是否启用", true, true);

                //map.AddBoolean(BtnAttr.SelectAccepterEnable, false, "是否启用", true, true);
                #endregion  功能按钮状态

                // 考核属性
                map.AddTBFloat(NodeAttr.WarningDays, 0, this.ToE(NodeAttr.WarningDays, "警告期限(0不警告)"), true, false); // "警告期限(0不警告)"
                map.AddTBFloat(NodeAttr.DeductDays, 1, this.ToE(NodeAttr.DeductDays, "限期(天)"), true, false); //"限期(天)"
                map.AddTBFloat(NodeAttr.DeductCent, 2, this.ToE(NodeAttr.DeductCent, "扣分(每延期1天扣)"), true, false); //"扣分(每延期1天扣)"

                map.AddTBFloat(NodeAttr.MaxDeductCent, 0, this.ToE(NodeAttr.MaxDeductCent, "最高扣分"), true, false);   //"最高扣分"
                map.AddTBFloat(NodeAttr.SwinkCent, float.Parse("0.1"), this.ToE("SwinkCent", "工作得分"), true, false); //"工作得分"
                map.AddDDLSysEnum(NodeAttr.OutTimeDeal, 0, this.ToE("OutTimeDeal", "超时处理"),
                true, true, NodeAttr.OutTimeDeal, "@0=不处理@1=自动转入下一步@2=自动转到指定的人员@3=向指定的人员发送消息@4=删除流程@5=执行SQL");

                map.AddTBString(NodeAttr.DoOutTime, null, "处理内容", true, false, 0, 300, 10, true);
        //        map.AddTBString(NodeAttr.FK_Flows, null, "flow", false, false, 0, 100, 10);

                map.AddDDLSysEnum(NodeAttr.CHWay, 0, "考核方式", true, true, NodeAttr.CHWay, "@0=不考核@1=按时效@2=按工作量");
                map.AddTBFloat(NodeAttr.Workload, 0, "工作量(单位:分钟)", true, false);


                // 相关功能。
                map.AttrsOfOneVSM.Add(new BP.WF.NodeStations(), new BP.WF.Port.Stations(),
                    NodeStationAttr.FK_Node, NodeStationAttr.FK_Station,
                    DeptAttr.Name, DeptAttr.No, this.ToE("NodeSta", "节点岗位"));

                map.AttrsOfOneVSM.Add(new BP.WF.NodeDepts(), new BP.WF.Port.Depts(), NodeDeptAttr.FK_Node, NodeDeptAttr.FK_Dept, DeptAttr.Name,
                DeptAttr.No, this.ToE("AccptDept", "节点部门"));

                map.AttrsOfOneVSM.Add(new BP.WF.NodeEmps(), new BP.WF.Port.Emps(), NodeEmpAttr.FK_Node, EmpDeptAttr.FK_Emp, DeptAttr.Name,
                    DeptAttr.No, this.ToE("Accpter", "接受人员"));

                map.AttrsOfOneVSM.Add(new BP.WF.NodeFlows(), new Flows(), NodeFlowAttr.FK_Node, NodeFlowAttr.FK_Flow, DeptAttr.Name, DeptAttr.No,
                    this.ToE("CallSubFlow", "可调用的子流程"));

                //map.AttrsOfOneVSM.Add(new BP.WF.NodeReturns(), new BP.WF.NodeExts(), NodeEmpAttr.FK_Node, EmpDeptAttr.FK_Emp, DeptAttr.Name,
                //  DeptAttr.No, this.ToE("Accpter", "可退回的节点"));

                RefMethod rm = new RefMethod();
                rm.Title = this.ToE("CanReturnNodes", "可退回的节点"); // "设计表单";
                rm.ClassMethodName = this.ToString() + ".DoCanReturnNodes";
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = "自动抄送设置"; // "抄送规则";
                rm.ClassMethodName = this.ToString() + ".DoCCRole";
                //rm.Warning = "";
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = this.ToE("DesignSheet", "设计表单"); // "设计表单";
                rm.ClassMethodName = this.ToString() + ".DoMapData";
                map.AddRefMethod(rm);


                rm = new RefMethod();
                rm.Title = this.ToE("Bill", "单据打印"); //"单据&单据";
                rm.ClassMethodName = this.ToString() + ".DoBill";
                rm.Icon = "/Images/FileType/doc.gif";
                map.AddRefMethod(rm);

                if (BP.SystemConfig.CustomerNo == "HCBD")
                {
                    /* 为海成邦达设置的个性化需求. */
                    rm = new RefMethod();
                    rm.Title = "DXReport设置";
                    rm.ClassMethodName = this.ToString() + ".DXReport";
                    rm.Icon = "/Images/FileType/doc.gif";
                    map.AddRefMethod(rm);
                }

                //rm = new RefMethod();
                //rm.Title = this.ToE("DoFAppSet", "调用外部程序接口"); // "调用外部程序接口";
                //rm.ClassMethodName = this.ToString() + ".DoFAppSet";
                //map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = this.ToE("DoAction", "事件"); // "调用事件接口";
                rm.ClassMethodName = this.ToString() + ".DoAction";
                map.AddRefMethod(rm);

                //rm = new RefMethod();
                //rm.Title = "表单显示"; // this.ToE("DoAction", "调用事件接口"); // "调用事件接口";
                //rm.ClassMethodName = this.ToString() + ".DoShowSheets";
                //map.AddRefMethod(rm);

                //rm = new RefMethod();
                //rm.Title = this.ToE("DoCond", "节点完成条件"); // "节点完成条件";
                //rm.ClassMethodName = this.ToString() + ".DoCond";
                //map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = this.ToE("DoCond", "流程完成条件"); // "流程完成条件";
                rm.ClassMethodName = this.ToString() + ".DoCond";
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = this.ToE("DoListen", "消息收听"); // "调用事件接口";
                rm.ClassMethodName = this.ToString() + ".DoListen";
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = this.ToE("DoTurn", "发送成功转向条件"); // "转向条件";
                rm.ClassMethodName = this.ToString() + ".DoTurn";
                map.AddRefMethod(rm);


                rm = new RefMethod();
                rm.Title = "接受人选择器"; // "个性化接受人窗口";
                rm.ClassMethodName = this.ToString() + ".DoAccepter";
                map.AddRefMethod(rm);
                 

                this._enMap = map;
                return this._enMap;
            }
        }
        public string DoTurn()
        {
            BP.WF.Node nd = new BP.WF.Node(this.NodeID);
            return nd.DoTurn();
        }
        /// <summary>
        /// 抄送规则
        /// </summary>
        /// <returns></returns>
        public string DoCCRole()
        {
            PubClass.WinOpen("./RefFunc/UIEn.aspx?EnName=BP.WF.CC&PK=" + this.NodeID , "抄送规则", "Bill", 800, 500, 200, 300);
            return null;
        }
        /// <summary>
        /// 个性化接受人窗口
        /// </summary>
        /// <returns></returns>
        public string DoAccepter()
        {
            PubClass.WinOpen("./RefFunc/UIEn.aspx?EnName=BP.WF.Selector&PK=" + this.NodeID, "个性化接受人窗口", "acce", 800, 500, 200, 300);
            return null;
        }
        /// <summary>
        /// 退回节点
        /// </summary>
        /// <returns></returns>
        public string DoCanReturnNodes()
        {
            PubClass.WinOpen("./../WF/Admin/CanReturnNodes.aspx?FK_Node=" + this.NodeID + "&FK_Flow=" + this.FK_Flow, "可退回的节点", "Bill", 500, 300, 200, 300);
            return null;
        }
        /// <summary>
        /// DXReport
        /// </summary>
        /// <returns></returns>
        public string DXReport()
        {
            PubClass.WinOpen("./../WF/Admin/DXReport.aspx?FK_Node=" + this.NodeID + "&FK_Flow=" + this.FK_Flow, "DXReport设置", "DXReport", 500, 300, 200, 300);
            return null;
        }
        public string DoListen()
        {
            BP.WF.Node nd = new BP.WF.Node(this.NodeID);
            return nd.DoListen();
        }
        public string DoFeatureSet()
        {
            BP.WF.Node nd = new BP.WF.Node(this.NodeID);
            return nd.DoFeatureSet();
        }
        public string DoShowSheets()
        {
            BP.WF.Node nd = new BP.WF.Node(this.NodeID);
            return nd.DoShowSheets();
        }
        public string DoCond()
        {
            BP.WF.Node nd = new BP.WF.Node(this.NodeID);
            return nd.DoCond();
        }
        //public string DoCondFL()
        //{
        //    BP.WF.Node nd = new BP.WF.Node(this.NodeID);
        //    return nd.DoCondFL();
        //}
        public string DoMapData()
        {
            BP.WF.Node nd = new BP.WF.Node(this.NodeID);
            return nd.DoMapData();
        }
        public string DoAction()
        {
            BP.WF.Node nd = new BP.WF.Node(this.NodeID);
            return nd.DoAction();
        }
        public string DoBill()
        {
            BP.WF.Node nd = new BP.WF.Node(this.NodeID);
            return nd.DoBill();
        }
        public string DoFAppSet()
        {
            BP.WF.Node nd = new BP.WF.Node(this.NodeID);
            return nd.DoFAppSet();
        }
        #endregion
    }
    /// <summary>
    /// 节点集合
    /// </summary>
    public class NodeOs : EntitiesOID
    {
        #region 构造方法
        /// <summary>
        /// 节点集合
        /// </summary>
        public NodeOs()
        {
        }
        #endregion

        public override Entity GetNewEntity
        {
            get { return new NodeO(); }
        }
    }
}
