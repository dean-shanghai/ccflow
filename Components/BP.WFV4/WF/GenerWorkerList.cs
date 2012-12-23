using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.WF;
using BP.Port;
using BP.En;

namespace BP.WF
{
    /// <summary>
    /// 工作人员集合
    /// </summary>
    public class GenerWorkerListAttr
    {
        #region 基本属性
        /// <summary>
        /// 工作节点
        /// </summary>
        public const string WorkID = "WorkID";
        /// <summary>
        /// 处罚单据编号
        /// </summary>
        public const string FK_Node = "FK_Node";
        /// <summary>
        /// 流程
        /// </summary>
        public const string FK_Flow = "FK_Flow";
        /// <summary>
        /// 征管软件是不是罚款
        /// </summary>
        public const string FK_Emp = "FK_Emp";
        /// <summary>
        /// 使用的岗位
        /// </summary>
        public const string UseStation_del = "UseStation";
        /// <summary>
        /// 使用的部门
        /// </summary>
        public const string FK_Dept = "FK_Dept";
        /// <summary>
        /// 应该完成时间
        /// </summary>
        public const string SDT = "SDT";
        /// <summary>
        /// 警告日期
        /// </summary>
        public const string DTOfWarning = "DTOfWarning";
        public const string RDT = "RDT";
        /// <summary>
        /// 是否可用
        /// </summary>
        public const string IsEnable = "IsEnable";
        /// <summary>
        /// WarningDays
        /// </summary>
        public const string WarningDays = "WarningDays";
        /// <summary>
        /// 是否自动分配
        /// </summary>
        //public const  string IsAutoGener="IsAutoGener";
        /// <summary>
        /// 产生时间
        /// </summary>
        //public const  string GenerDateTime="GenerDateTime";
        /// <summary>
        /// IsPass
        /// </summary>
        public const string IsPass = "IsPass";
        /// <summary>
        /// 流程ID
        /// </summary>
        public const string FID = "FID";
        /// <summary>
        /// 人员名称
        /// </summary>
        public const string FK_EmpText = "FK_EmpText";
        /// <summary>
        /// 节点名称
        /// </summary>
        public const string FK_NodeText = "FK_NodeText";
        /// <summary>
        /// 发送人
        /// </summary>
        public const string Sender = "Sender";
        /// <summary>
        /// 谁执行它?
        /// </summary>
        public const string WhoExeIt = "WhoExeIt";
        /// <summary>
        /// 优先级
        /// </summary>
        public const string PRI = "PRI";
        /// <summary>
        /// 是否读取？
        /// </summary>
        public const string IsRead = "IsRead";
        /// <summary>
        /// 催办次数
        /// </summary>
        public const string PressTimes = "PressTimes";
        /// <summary>
        /// 备注
        /// </summary>
        public const string Tag = "Tag";
        #endregion
    }
    /// <summary>
    /// 工作者列表
    /// </summary>
    public class GenerWorkerList : Entity
    {
        #region 基本属性
        /// <summary>
        /// 谁来执行它
        /// </summary>
        public int WhoExeIt
        {
            get
            {
                return this.GetValIntByKey(GenerWorkerListAttr.WhoExeIt);
            }
            set
            {
                SetValByKey(GenerWorkerListAttr.WhoExeIt, value);
            }
        }
        public int PressTimes
        {
            get
            {
                return this.GetValIntByKey(GenerWorkerListAttr.PressTimes);
            }
            set
            {
                SetValByKey(GenerWorkerListAttr.PressTimes, value);
            }
        }
        /// <summary>
        /// 优先级
        /// </summary>
        public int PRI
        {
            get
            {
                return this.GetValIntByKey(GenerWorkFlowAttr.PRI);
            }
            set
            {
                SetValByKey(GenerWorkFlowAttr.PRI, value);
            }
        }
        /// <summary>
        /// WorkID
        /// </summary>
        public override string PK
        {
            get
            {
                return "WorkID,FK_Emp,FK_Node";
            }
        }
        /// <summary>
        /// 是否可用
        /// </summary>
        public bool IsEnable
        {
            get
            {
                return this.GetValBooleanByKey(GenerWorkerListAttr.IsEnable);
            }
            set
            {
                this.SetValByKey(GenerWorkerListAttr.IsEnable, value);
            }
        }
        /// <summary>
        /// 是否通过(对审核的会签节点有效)
        /// </summary>
        public bool IsPass
        {
            get
            {
                return this.GetValBooleanByKey(GenerWorkerListAttr.IsPass);
            }
            set
            {
                this.SetValByKey(GenerWorkerListAttr.IsPass, value);
            }
        }
        /// <summary>
        /// WorkID
        /// </summary>
        public Int64 WorkID
        {
            get
            {
                return this.GetValInt64ByKey(GenerWorkerListAttr.WorkID);
            }
            set
            {
                this.SetValByKey(GenerWorkerListAttr.WorkID, value);
            }
        }
        /// <summary>
        /// Node
        /// </summary>
        public int FK_Node
        {
            get
            {
                return this.GetValIntByKey(GenerWorkerListAttr.FK_Node);
            }
            set
            {
                this.SetValByKey(GenerWorkerListAttr.FK_Node, value);
            }
           
        }
        /// <summary>
        /// 发送人
        /// </summary>
        public string Sender
        {
            get
            {
                return this.GetValStrByKey(GenerWorkerListAttr.Sender);
            }
            set
            {
                this.SetValByKey(GenerWorkerListAttr.Sender, value);
            }
        }
        /// <summary>
        /// 节点名称
        /// </summary>
        public string FK_NodeText
        {
            get
            {
                return this.GetValStrByKey(GenerWorkerListAttr.FK_NodeText);
            }
            set
            {
                this.SetValByKey(GenerWorkerListAttr.FK_NodeText, value);
            }
        }
        
        /// <summary>
        /// 流程ID
        /// </summary>
        public Int64 FID
        {
            get
            {
                return this.GetValInt64ByKey(GenerWorkerListAttr.FID);
            }
            set
            {
                this.SetValByKey(GenerWorkerListAttr.FID, value);
            }
        }
        /// <summary>
        /// 警告天
        /// </summary>
        public float WarningDays
        {
            get
            {
                return this.GetValFloatByKey(GenerWorkerListAttr.WarningDays);
            }
            set
            {
                this.SetValByKey(GenerWorkerListAttr.WarningDays, value);
            }
        }
        /// <summary>
        /// 工作人员
        /// </summary>
        public Emp HisEmp
        {
            get
            {
                return new Emp(this.FK_Emp);
            }
        }
        /// <summary>
        /// 发送日期
        /// </summary>
        public string RDT
        {
            get
            {
                return this.GetValStringByKey(GenerWorkerListAttr.RDT);
            }
            set
            {
                this.SetValByKey(GenerWorkerListAttr.RDT, value);
            }
        }
        /// <summary>
        /// 应该完成日期
        /// </summary>
        public string SDT
        {
            get
            {
                return this.GetValStringByKey(GenerWorkerListAttr.SDT);
            }
            set
            {
                this.SetValByKey(GenerWorkerListAttr.SDT, value);
            }
        }
        /// <summary>
        /// 警告日期
        /// </summary>
        public string DTOfWarning
        {
            get
            {
                return this.GetValStringByKey(GenerWorkerListAttr.DTOfWarning);
            }
            set
            {
                this.SetValByKey(GenerWorkerListAttr.DTOfWarning, value);
            }
        }
        /// <summary>
        /// 人员
        /// </summary>
        public string FK_Emp
        {
            get
            {
                return this.GetValStringByKey(GenerWorkerListAttr.FK_Emp);
            }
            set
            {
                this.SetValByKey(GenerWorkerListAttr.FK_Emp, value);
            }
        }
        /// <summary>
        /// 人员名称
        /// </summary>
        public string FK_EmpText
        {
            get
            {
                return this.GetValStrByKey(GenerWorkerListAttr.FK_EmpText);
            }
            set
            {
                this.SetValByKey(GenerWorkerListAttr.FK_EmpText, value);
            }
        }
        /// <summary>
        /// 部门
        /// </summary>
        public string FK_Dept
        {
            get
            {
                return this.GetValStringByKey(GenerWorkerListAttr.FK_Dept);
            }
            set
            {
                this.SetValByKey(GenerWorkerListAttr.FK_Dept, value);
            }
        }
        public string FK_DeptT
        {
            get
            {
                BP.Port.Dept d = new BP.Port.Dept(this.FK_Dept);
                return d.Name;
                //return this.GetValStringByKey(GenerWorkerListAttr.FK_Dept);
            }
        }
        /// <summary>
        /// 流程编号
        /// </summary>		 
        public string FK_Flow
        {
            get
            {
                return this.GetValStringByKey(GenerWorkerListAttr.FK_Flow);
            }
            set
            {
                this.SetValByKey(GenerWorkerListAttr.FK_Flow, value);
            }
        }
        #endregion

        #region 构造函数
         
        /// <summary>
        /// 工作者
        /// </summary>
        public GenerWorkerList()
        {
        }
        public GenerWorkerList(Int64 workid, int FK_Node, string fk_emp)
        {
            if (this.WorkID == 0)
                return;

            this.WorkID = workid;
            this.FK_Node = FK_Node;
            this.FK_Emp = fk_emp;
            this.Retrieve();
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

                Map map = new Map("WF_GenerWorkerlist");
                map.EnDesc = "工作者";
                 

                map.AddTBIntPK(GenerWorkerListAttr.WorkID, 0, "工作ID", true, true);
                map.AddTBStringPK(GenerWorkerListAttr.FK_Emp, null, "人员", true, false, 0, 50, 100);
                map.AddTBIntPK(GenerWorkerListAttr.FK_Node, 0, "节点ID", true, false);

                map.AddTBString(GenerWorkerListAttr.FK_EmpText, null, "人员名称", true, false, 0, 100, 100);

                map.AddTBString(GenerWorkerListAttr.FK_NodeText, null, "节点名称", true, false, 0, 100, 100);
                map.AddTBInt(GenerWorkerListAttr.FID, 0, "流程ID", true, false);
                map.AddTBString(GenerWorkerListAttr.FK_Flow, null, "流程", true, false, 0, 100, 100);
                map.AddTBString(GenerWorkerListAttr.FK_Dept, null, "使用部门", true, false, 0, 100, 100);

                //如果是流程属性来控制的就按流程属性来计算。
                map.AddTBDateTime(GenerWorkerListAttr.SDT, "应完成日期", false, false);
                map.AddTBDateTime(GenerWorkerListAttr.DTOfWarning, "警告日期", false, false);
                map.AddTBFloat(GenerWorkerListAttr.WarningDays, 0, "预警天", true, false);
                map.AddTBDateTime(GenerWorkerListAttr.RDT, "RDT", false, false);
                map.AddBoolean(GenerWorkerListAttr.IsEnable, true, "是否可用", true, true);

                //  add for lijian 2012-11-30
                map.AddTBInt(GenerWorkerListAttr.IsRead, 0, "是否读取", true, true);

                //对会签节点有效
                map.AddTBInt(GenerWorkerListAttr.IsPass, 0, "是否通过(对合流节点有效)", false, false);

                // 谁执行它？
                map.AddTBInt(GenerWorkerListAttr.WhoExeIt, 0, "谁执行它", false, false);

                //发送人. 2011-11-12 为天津用户增加。
                map.AddTBString(GenerWorkerListAttr.Sender, null, "发送人", true, false, 0, 100, 100);

                //优先级，2012-06-15 为青岛用户增加。
                map.AddTBInt(GenerWorkFlowAttr.PRI, 1, "优先级", true, true);

                //催办次数. 为亿阳信通加.
                map.AddTBInt(GenerWorkerListAttr.PressTimes, 0, "催办次数", true, false);

                //标签.
                map.AddTBString(GenerWorkerListAttr.Tag, null, "Tag", true, false, 0, 3000, 100);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
        protected override bool beforeInsert()
        {
            if (this.FID != 0)
            {
                if (this.FID == this.WorkID)
                    this.FID = 0;
            }
            this.Sender = BP.Web.WebUser.No + "," + BP.Web.WebUser.Name;
            return base.beforeInsert();
        }
    }
    /// <summary>
    /// 工作人员集合
    /// </summary>
    public class GenerWorkerLists : Entities
    {
        #region 方法
        /// <summary>
        /// 得到它的 Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new GenerWorkerList();
            }
        }
        /// <summary>
        /// GenerWorkerList
        /// </summary>
        public GenerWorkerLists() { }
        public GenerWorkerLists(Int64 workId, int nodeId)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(GenerWorkerListAttr.WorkID, workId);
            qo.addAnd();
            qo.AddWhere(GenerWorkerListAttr.FK_Node, nodeId);
            qo.DoQuery();
            return;
        }
        public GenerWorkerLists(Int64 workId, int nodeId,string fk_emp)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(GenerWorkerListAttr.WorkID, workId);
            qo.addAnd();
            qo.AddWhere(GenerWorkerListAttr.FK_Node, nodeId);
            qo.addAnd();
            qo.AddWhere(GenerWorkerListAttr.FK_Emp, fk_emp);
            qo.DoQuery();
            return;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="workId"></param>
        /// <param name="nodeId"></param>
        /// <param name="isWithEmpExts">是否要包含记忆中的人员</param>
        public GenerWorkerLists(Int64 workId, int nodeId, bool isWithEmpExts)
        {
            QueryObject qo = new QueryObject(this);
            qo.addLeftBracket();
            qo.AddWhere(GenerWorkerListAttr.WorkID, workId);
            qo.addOr();
            qo.AddWhere(GenerWorkerListAttr.FID, workId);
            qo.addRightBracket();
            qo.addAnd();
            qo.AddWhere(GenerWorkerListAttr.FK_Node, nodeId);
            int i = qo.DoQuery();

            if (isWithEmpExts == false)
                return;

            if (i == 0)
                throw new Exception("@系统错误，工作人员丢失请与管理员联系。NodeID=" + nodeId + " WorkID=" + workId);

            RememberMe rm = new RememberMe();
            rm.FK_Emp = Web.WebUser.No;
            rm.FK_Node = nodeId;
            if (rm.RetrieveFromDBSources() == 0)
                return;

            GenerWorkerList wl = (GenerWorkerList)this[0];
            string[] emps = rm.Emps.Split('@');
            foreach (string emp in emps)
            {
                if (emp==null || emp=="")
                    continue;

                if (this.GetCountByKey(GenerWorkerListAttr.FK_Emp, emp) >= 1)
                    continue;

                GenerWorkerList mywl = new GenerWorkerList();
                mywl.Copy(wl);
                mywl.IsEnable = false;
                mywl.FK_Emp = emp;
                WF.Port.WFEmp myEmp = new Port.WFEmp(emp);
                mywl.FK_EmpText = myEmp.Name;
                try
                {
                    mywl.Insert();
                }
                catch
                {
                    mywl.Update();
                    continue;
                }
                this.AddEntity(mywl);
            }
            return;
        }
        /// <summary>
        /// 工作者
        /// </summary>
        /// <param name="workId">工作者ID</param>
        /// <param name="flowNo">流程编号</param>
        public GenerWorkerLists(Int64 workId, string flowNo)
        {
            if (workId == 0)
                return;

            Flow fl = new Flow(flowNo);
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(GenerWorkerListAttr.WorkID, workId);
            qo.addAnd();
            qo.AddWhere(GenerWorkerListAttr.FK_Flow, flowNo);
            qo.DoQuery();
        }
        #endregion
    }
}
