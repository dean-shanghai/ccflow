using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.WF;
using BP.Port;
using BP.Port;
using BP.En;

namespace BP.WF
{
	/// <summary>
	/// 流程
	/// </summary>
	public class CHOfFlowAttr:BP.WF.GERptAttr
	{
		#region 基本属性
		/// <summary>
		/// 部门
		/// </summary>
		public const  string FK_Dept="FK_Dept";
		/// <summary>
		/// 流程状态
		/// </summary>
		public const  string WFState="WFState";
		/// <summary>
		/// 标题
		/// </summary>
		public const  string Title="Title";
		/// <summary>
		/// 流程
		/// </summary>
		public const  string FK_Flow="FK_Flow";
		/// <summary>
		/// 年月
		/// </summary>
		public const  string FK_NY="FK_NY";
		/// <summary>
		/// 季度
		/// </summary>
		public const  string FK_AP_del="FK_AP";
		/// <summary>
		/// 工作ID
		/// </summary>
		public const  string WorkID="WorkID";
        public const string FID = "FID";
		/// <summary>
		/// 考核的节点
		/// </summary>
		public const  string NodeState="NodeState";
		/// <summary>
		/// 执行员
		/// </summary>
		public const  string FK_Emp="FK_Emp";
		/// <summary>
		/// 考核的工作岗位
		/// </summary>
		public const  string FK_Station="FK_Station";		 
		/// <summary>
		/// 工作的发起时间
		/// </summary>
		public const  string RDT="RDT";		 	 
		/// <summary>
		/// 完成日期
		/// </summary>
		public const  string CDT="CDT";		 
		/// <summary>
		/// SpanDays
		/// </summary>
		public const  string SpanDays="SpanDays";
		/// <summary>
		/// Note
		/// </summary>
		public const  string Note="Note";
        /// <summary>
        /// 日期从
        /// </summary>
        public const string DateLitFrom = "DateLitFrom";
        /// <summary>
        /// 日期到
        /// </summary>
        public const string DateLitTo = "DateLitTo";
		#endregion
	}
	/// <summary>
	/// 流程
	/// </summary>
    public class CHOfFlow : Entity
    {
        #region 基本属性
        /// <summary>
        /// 节点ID
        /// </summary>
        public Int64 WorkID
        {
            get
            {
                return this.GetValInt64ByKey(CHOfFlowAttr.WorkID);
            }
            set
            {
                this.SetValByKey(CHOfFlowAttr.WorkID, value);
            }
        }
        public Int64 FID
        {
            get
            {
                return this.GetValInt64ByKey(CHOfFlowAttr.FID);
            }
            set
            {
                this.SetValByKey(CHOfFlowAttr.FID, value);
            }
        }
        public int SpanDays
        {
            get
            {
                return this.GetValIntByKey(CHOfFlowAttr.SpanDays);
            }
            set
            {
                this.SetValByKey(CHOfFlowAttr.SpanDays, value);
            }
        }
        /// <summary>
        /// 工作人员
        /// </summary>
        public string FK_Emp
        {
            get
            {
                return this.GetValStringByKey(CHOfFlowAttr.FK_Emp);
            }
            set
            {
                this.SetValByKey(CHOfFlowAttr.FK_Emp, value);
            }
        }
        public string FK_NY
        {
            get
            {
                return this.GetValStringByKey(CHOfFlowAttr.FK_NY);
            }
            set
            {
                this.SetValByKey(CHOfFlowAttr.FK_NY, value);
            }
        }
        /// <summary>
        /// 工作人员
        /// </summary>
        public int WFState
        {
            get
            {
                return this.GetValIntByKey(CHOfFlowAttr.WFState);
            }
            set
            {
                this.SetValByKey(CHOfFlowAttr.WFState, value);
            }
        }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get
            {
                return this.GetValStringByKey(CHOfFlowAttr.Title);
            }
            set
            {
                this.SetValByKey(CHOfFlowAttr.Title, value);
            }
        }
        /// <summary>
        /// 部门
        /// </summary>
        public string FK_Dept
        {
            get
            {
                return this.GetValStringByKey(CHOfFlowAttr.FK_Dept);
            }
            set
            {
                this.SetValByKey(CHOfFlowAttr.FK_Dept, value);
            }
        }
        public string DateLitFrom
        {
            get
            {
                return this.GetValStringByKey(CHOfFlowAttr.DateLitFrom);
            }
            set
            {
                this.SetValByKey(CHOfFlowAttr.DateLitFrom, value);
            }
        }
        public string DateLitTo
        {
            get
            {
                return this.GetValStringByKey(CHOfFlowAttr.DateLitTo);
            }
            set
            {
                this.SetValByKey(CHOfFlowAttr.DateLitTo, value);
            }
        }
        /// <summary>
        /// 流程
        /// </summary>
        public string FK_Flow
        {
            get
            {
                return this.GetValStringByKey(CHOfFlowAttr.FK_Flow);
            }
            set
            {
                this.SetValByKey(CHOfFlowAttr.FK_Flow, value);
            }
        }
        public string FK_FlowText
        {
            get
            {
                return this.GetValRefTextByKey(CHOfFlowAttr.FK_Flow);
            }
        }
        /// <summary>
        /// 部门
        /// </summary>
        public string FK_Dept_D
        {
            get
            {
                return this.GetValStringByKey(CHOfFlowAttr.FK_Dept);
            }
            set
            {
                this.SetValByKey(CHOfFlowAttr.FK_Dept, value);
            }
        }

        /// <summary>
        /// 完成时间
        /// </summary>
        public string CDT
        {
            get
            {
                return this.GetValStringByKey(CHOfFlowAttr.CDT);
            }
            set
            {
                this.SetValByKey(CHOfFlowAttr.CDT, value);
            }
        }
        /// <summary>
        /// 记录时间
        /// </summary>
        public string RDT
        {
            get
            {
                return this.GetValStringByKey(CHOfFlowAttr.RDT);
            }
            set
            {
                this.SetValByKey(CHOfFlowAttr.RDT, value);
            }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 流程
        /// </summary>
        public CHOfFlow()
        {
        }
        public CHOfFlow(Int64 workid)
        {
            this.WorkID = workid;
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
                Map map = new Map("WF_CHOfFlow");
                map.EnDesc = this.ToE("FlowSearch", "流程数据"); //"流程查询";
                map.EnType = EnType.App;

                map.AddTBIntPK(CHOfFlowAttr.WorkID, 0, "工作ID", true, true);
                map.AddTBInt(CHOfFlowAttr.FID, 0, "FID", true, true);
                map.AddDDLEntities(CHOfFlowAttr.FK_Flow, null, "流程", new Flows(), false);
                map.AddDDLSysEnum(CHOfFlowAttr.WFState, 0, "流程状态", true, true);
                map.AddTBString(CHOfFlowAttr.Title, null, "标题", true, true, 0, 400, 10);

                map.AddDDLEntities(CHOfFlowAttr.FlowStarter, null, "发起人", new BP.Port.Emps(), false);
                map.AddDDLEntities(CHOfFlowAttr.FK_Dept, null, "发起人部门", new Depts(), false);
                map.AddTBDateTime(CHOfFlowAttr.FlowStartRDT, "发起时间", true, true);

                map.AddTBString(CHOfFlowAttr.FlowEmps, null, "参与人", true, true, 0, 400, 10);
                map.AddDDLEntities(CHOfFlowAttr.FlowEnder, null, "结束人", new BP.Port.Emps(), false);
                map.AddDDLEntities(CHOfFlowAttr.FlowEnderRDT, null, "完成时间", new BP.Port.Emps(), false);

                map.AddTBInt(CHOfFlowAttr.FlowDaySpan, 0, "流程用天", true, true);

                map.AddDDLEntities(CHOfFlowAttr.FK_NY, DataType.CurrentYearMonth, "隶书年月", new BP.Pub.NYs(), false);
                map.AddTBIntMyNum();
                map.AddSearchAttr(CHOfFlowAttr.WFState);
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

        public string DoSelfTest()
        {
            string info = "";
            CHOfFlows ens = new CHOfFlows();
            QueryObject qo = new QueryObject(ens);
            qo.AddWhere(CHOfFlowAttr.FK_Dept, " like ", BP.Web.WebUser.FK_Dept + "%");
            qo.addAnd();
            qo.AddWhere(CHOfFlowAttr.WFState, (int)BP.WF.WFState.Runing);
            qo.DoQuery();
            foreach (CHOfFlow en in ens)
            {
                WorkFlow wf = new WorkFlow(this.FK_Flow, this.WorkID, this.FID);
                info += "<hr><b>对：" + this.Title + "，体检信息如下：</b><BR>" + wf.DoSelfTest();
            }
            return info;
        }
    }
	/// <summary>
	/// 流程s BP.Port.FK.CHOfFlows
	/// </summary>
	public class CHOfFlows:Entities
	{
		#region 方法
		/// <summary>
		/// 得到它的 Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new CHOfFlow();
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public CHOfFlows(){}
		#endregion
	}
	
}
