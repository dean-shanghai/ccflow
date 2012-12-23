using System;
using System.Collections.Generic;
using System.Text;
using BP.En;
using BP.DA;
using BP.Sys;

namespace BP.WF
{
   
    /// <summary>
    ///  属性
    /// </summary>
    public class TrackTempAttr
    {
        /// <summary>
        /// 记录日期
        /// </summary>
        public const string RDT = "RDT";
        /// <summary>
        /// 完成日期
        /// </summary>
        public const string CDT = "CDT";
        /// <summary>
        /// FID
        /// </summary>
        public const string FID = "FID";
        /// <summary>
        /// WorkID
        /// </summary>
        public const string WorkID = "WorkID";
        /// <summary>
        /// 活动类型
        /// </summary>
        public const string ActionType = "ActionType";
        /// <summary>
        /// 时间跨度
        /// </summary>
        public const string WorkTimeSpan = "WorkTimeSpan";
        /// <summary>
        /// 节点数据
        /// </summary>
        public const string NodeData = "NodeData";
        /// <summary>
        /// 流程编号
        /// </summary>
        public const string FK_Flow = "FK_Flow";
        /// <summary>
        /// 轨迹临时表字段
        /// </summary>
        public const string TrackTempFields = "TrackTempFields";
        /// <summary>
        /// 备注
        /// </summary>
        public const string Note = "Note";
        /// <summary>
        /// 从节点
        /// </summary>
        public const string NDFrom = "NDFrom";
        /// <summary>
        /// 到节点
        /// </summary>
        public const string NDTo = "NDTo";
        /// <summary>
        /// 从人员
        /// </summary>
        public const string EmpFrom = "EmpFrom";
        /// <summary>
        /// 到人员
        /// </summary>
        public const string EmpTo = "EmpTo";
        /// <summary>
        /// 审核
        /// </summary>
        public const string Msg = "Msg";
        /// <summary>
        /// EmpFromT
        /// </summary>
        public const string EmpFromT = "EmpFromT";
        /// <summary>
        /// NDFromT
        /// </summary>
        public const string NDFromT = "NDFromT";
        /// <summary>
        /// NDToT
        /// </summary>
        public const string NDToT = "NDToT";
        /// <summary>
        /// EmpToT
        /// </summary>
        public const string EmpToT = "EmpToT";
        /// <summary>
        /// 实际执行人员
        /// </summary>
        public const string Exer = "Exer";
    }
    /// <summary>
    /// 轨迹临时表
    /// </summary>
    public class TrackTemp : BP.En.EntityMyPK
    {
        #region 轨迹临时表
        /// <summary>
        /// 节点从
        /// </summary>
        public int NDFrom
        {
            get
            {
                 
                return this.GetValIntByKey(TrackTempAttr.NDFrom);
            }
            set
            {
                this.SetValByKey(TrackTempAttr.NDFrom, value);
            }
        }
        /// <summary>
        /// 节点到
        /// </summary>
        public int NDTo
        {
            get
            {
                return this.GetValIntByKey(TrackTempAttr.NDTo);
            }
            set
            {
                this.SetValByKey(TrackTempAttr.NDTo, value);
            }
        }
        /// <summary>
        /// 流程编号
        /// </summary>
        public string FK_Flow
        {
            get
            {
                return this.GetValStringByKey(TrackTempAttr.FK_Flow);
            }
            set
            {
                this.SetValByKey(TrackTempAttr.FK_Flow, value);
            }
        }
        /// <summary>
        /// 从人员
        /// </summary>
        public string EmpFrom
        {
            get
            {
                return this.GetValStringByKey(TrackTempAttr.EmpFrom);
            }
            set
            {
                this.SetValByKey(TrackTempAttr.EmpFrom, value);
            }
        }
        /// <summary>
        /// 到人员
        /// </summary>
        public string EmpTo
        {
            get
            {
                return this.GetValStringByKey(TrackTempAttr.EmpTo);
            }
            set
            {
                this.SetValByKey(TrackTempAttr.EmpTo, value);
            }
        }
        /// <summary>
        /// 记录日期
        /// </summary>
        public string RDT
        {
            get
            {
                return this.GetValStringByKey(TrackTempAttr.RDT);
            }
            set
            {
                this.SetValByKey(TrackTempAttr.RDT, value);
            }
        }
        /// <summary>
        /// fid
        /// </summary>
        public Int64 FID
        {
            get
            {
                return this.GetValInt64ByKey(TrackTempAttr.FID);
            }
            set
            {
                this.SetValByKey(TrackTempAttr.FID, value);
            }
        }
        /// <summary>
        /// Workid
        /// </summary>
        public Int64 WorkID
        {
            get
            {
                return this.GetValInt64ByKey(TrackTempAttr.WorkID);
            }
            set
            {
                this.SetValByKey(TrackTempAttr.WorkID, value);
            }
        }
        /// <summary>
        /// 活动类型
        /// </summary>
        public ActionType HisActionType
        {
            get
            {
                return (ActionType)this.GetValIntByKey(TrackTempAttr.ActionType);
            }
            set
            {
                this.SetValByKey(TrackTempAttr.ActionType, (int)value);
            }
        }
        public static string GetActionTypeT(ActionType at)
        {
            switch (at)
            {
                case ActionType.Forward:
                    return "前进";
                case ActionType.Return:
                    return "退回";
                case ActionType.Shift:
                    return "移交";
                case ActionType.UnShift:
                    return "撤消移交";
                case ActionType.Start:
                    return "发起";
                case ActionType.UnSend:
                    return "撤消发起";
                case ActionType.ForwardFL:
                    return " -前进(分流点)";
                case ActionType.ForwardHL:
                    return " -向合流点发送";
                case ActionType.FlowOver:
                    return "流程结束";
                case ActionType.CallSubFlow:
                    return "调用起子流程";
                case ActionType.StartSubFlow:
                    return "子流程发起";
                case ActionType.SubFlowForward:
                    return "子流程前进";
                case ActionType.RebackOverFlow:
                    return "恢复已完成的流程";
                case ActionType.FlowOverByCoercion:
                    return "强制结束流程";
                case ActionType.HungUp:
                    return "挂起";
                case ActionType.UnHungUp:
                    return "取消挂起";
                case ActionType.Press:
                    return "催办";
                default:
                    return "未知";
            }
        }
        public string HisActionTypeT
        {
            get
            {
                return TrackTemp.GetActionTypeT(this.HisActionType);
            }
        }
        /// <summary>
        /// 节点数据
        /// </summary>
        public string NodeData
        {
            get
            {
                return this.GetValStringByKey(TrackTempAttr.NodeData);
            }
            set
            {
                this.SetValByKey(TrackTempAttr.NodeData, value);
            }
        }
        public string Exer
        {
            get
            {
                return this.GetValStringByKey(TrackTempAttr.Exer);
            }
            set
            {
                this.SetValByKey(TrackTempAttr.Exer, value);
            }
        }
        /// <summary>
        /// 审核意见
        /// </summary>
        public string Msg
        {
            get
            {
                return this.GetValStringByKey(TrackTempAttr.Msg);
            }
            set
            {
                this.SetValByKey(TrackTempAttr.Msg, value);
            }
        }
        public string MsgHtml
        {
            get
            {
                return this.GetValHtmlStringByKey(TrackTempAttr.Msg);
            }
        }
        public string EmpToT
        {
            get
            {
                return this.GetValStringByKey(TrackTempAttr.EmpToT);
            }
            set
            {
                this.SetValByKey(TrackTempAttr.EmpToT, value);
            }
        }
        public string EmpFromT
        {
            get
            {
                return this.GetValStringByKey(TrackTempAttr.EmpFromT);
            }
            set
            {
                this.SetValByKey(TrackTempAttr.EmpFromT, value);
            }
        }

        public string NDFromT
        {
            get
            {
                return this.GetValStringByKey(TrackTempAttr.NDFromT);
            }
            set
            {
                this.SetValByKey(TrackTempAttr.NDFromT, value);
            }
        }
        public string NDToT
        {
            get
            {
                return this.GetValStringByKey(TrackTempAttr.NDToT);
            }
            set
            {
                this.SetValByKey(TrackTempAttr.NDToT, value);
            }
        }
        #endregion attrs

        #region 属性
        public string RptName = null;
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                
                Map map = new Map();

                #region 基本属性
                map.EnDBUrl = new DBUrl(DBUrlType.AppCenterDSN); //要连接的数据源（表示要连接到的那个系统数据库）。
                map.PhysicsTable = "WF_TrackTemp"; // 要物理表。
                map.EnDesc = "轨迹临时表";
                map.EnType = EnType.App;
                #endregion

                #region 字段
                map.AddMyPK();
                map.AddTBString(TrackTempAttr.FK_Flow, null, "流程", true, false, 0, 100, 100);
                map.AddTBInt(TrackTempAttr.ActionType, 0, "操作类型", true, false);

                map.AddTBInt(TrackTempAttr.FID, 0, "流程ID", true, false);
                map.AddTBInt(TrackTempAttr.WorkID, 0, "工作ID", true, false);

                map.AddTBInt(TrackTempAttr.NDFrom, 0, "从节点", true, false);
                map.AddTBString(TrackTempAttr.NDFromT, null, "从节点(名称)", true, false, 0, 100, 100);

                map.AddTBInt(TrackTempAttr.NDTo, 0, "到节点", true, false);
                map.AddTBString(TrackTempAttr.NDToT, null, "到节点(名称)", true, false, 0, 100, 100);

                map.AddTBString(TrackTempAttr.EmpFrom, null, "从人员", true, false, 0, 100, 100);
                map.AddTBString(TrackTempAttr.EmpFromT, null, "从人员(名称)", true, false, 0, 100, 100);

                map.AddTBString(TrackTempAttr.EmpTo, null, "到人员", true, false, 0, 4000, 100);
                map.AddTBString(TrackTempAttr.EmpToT, null, "到人员(名称)", true, false, 0, 100, 100);

                map.AddTBString(TrackTempAttr.RDT, null, "日期", true, false, 0, 30, 100);

                map.AddTBFloat(TrackTempAttr.WorkTimeSpan, 0, "时间跨度(天)", true, false);
                map.AddTBStringDoc(TrackTempAttr.Msg, null, "消息", true, false);
                map.AddTBStringDoc(TrackTempAttr.NodeData, null, "节点数据(日志信息)", true, false);

                map.AddTBString(TrackTempAttr.Exer, null, "执行人", true, false, 0, 30, 100);
                #endregion 字段

                this._enMap = map;
                return this._enMap;
            }
        }
        /// <summary>
        /// 轨迹临时表
        /// </summary>
        /// <param name="rptName"></param>
        public TrackTemp(string mypk)
        {
            this.MyPK = mypk;
            this.Retrieve();
        }
        /// <summary>
        /// 轨迹临时表
        /// </summary>
        public TrackTemp()
        {
        }
        /// <summary>
        /// 增加授权人
        /// </summary>
        /// <returns></returns>
        protected override bool beforeInsert()
        {
            if (BP.Web.WebUser.IsAuthorize)
                this.Exer = BP.Web.WebUser.AuthorizerEmpID + "," + BP.Web.WebUser.Auth;
            else
                this.Exer = BP.Web.WebUser.No + "," + BP.Web.WebUser.Name;
            return base.beforeInsert();
        }
        #endregion attrs
    }
    /// <summary>
    /// 轨迹临时表集合
    /// </summary>
    public class TrackTemps : BP.En.Entities
    {
        /// <summary>
        /// 轨迹临时表集合
        /// </summary>
        public TrackTemps()
        {
        }
        public override Entity GetNewEntity
        {
            get
            {
                return new TrackTemp();
            }
        }
    }
}
