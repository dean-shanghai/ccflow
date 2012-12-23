using System;
using System.Collections.Generic;
using System.Text;
using BP.En;
using BP.Sys;

namespace BP.WF
{
    /// <summary>
    ///  属性
    /// </summary>
    public class GERptAttr
    {
        public const string Title = "Title";
        public const string FlowEmps = "FlowEmps";
        public const string FID = "FID";
        public const string OID = "OID";
        public const string FK_NY = "FK_NY";
        public const string FlowStarter = "FlowStarter";
        public const string FlowStartRDT = "FlowStartRDT";
        public const string FK_Dept = "FK_Dept";
        public const string WFState = "WFState";
        public const string MyNum = "MyNum";
        /// <summary>
        /// 结束人
        /// </summary>
        public const string FlowEnder = "FlowEnder";
        public const string FlowEnderRDT = "FlowEnderRDT";
        /// <summary>
        /// 跨度
        /// </summary>
        public const string FlowDaySpan = "FlowDaySpan";
        /// <summary>
        /// 结束节点
        /// </summary>
        public const string FlowEndNode = "FlowEndNode";
    }
    /// <summary>
    /// 报表
    /// </summary>
    public class GERpt : BP.En.EntityOID
    {
        #region attrs
        /// <summary>
        /// 流程时间跨度
        /// </summary>
        public int FlowDaySpan
        {
            get
            {
                return this.GetValIntByKey(GERptAttr.FlowDaySpan);
            }
            set
            {
                this.SetValByKey(GERptAttr.FlowDaySpan, value);
            }
        }
        public int MyNum
        {
            get
            {
                return this.GetValIntByKey(GERptAttr.MyNum);
            }
            set
            {
                this.SetValByKey(GERptAttr.MyNum, value);
            }
        }
        public int FID
        {
            get
            {
                return this.GetValIntByKey(GERptAttr.FID);
            }
            set
            {
                this.SetValByKey(GERptAttr.FID, value);
            }
        }
        public string FlowEmps
        {
            get
            {
                return this.GetValStringByKey(GERptAttr.FlowEmps);
            }
            set
            {
                this.SetValByKey(GERptAttr.FlowEmps, value);
            }
        }
        /// <summary>
        /// 流程发起人
        /// </summary>
        public string FlowStarter
        {
            get
            {
                return this.GetValStringByKey(GERptAttr.FlowStarter);
            }
            set
            {
                this.SetValByKey(GERptAttr.FlowStarter, value);
            }
        }
        public string FlowStartRDT
        {
            get
            {
                return this.GetValStringByKey(GERptAttr.FlowStartRDT);
            }
            set
            {
                this.SetValByKey(GERptAttr.FlowStartRDT, value);
            }
        }
        /// <summary>
        /// 流程结束者
        /// </summary>
        public string FlowEnder
        {
            get
            {
                return this.GetValStringByKey(GERptAttr.FlowEnder);
            }
            set
            {
                this.SetValByKey(GERptAttr.FlowEnder, value);
            }
        }
        /// <summary>
        /// 流程结束时间
        /// </summary>
        public string FlowEnderRDT
        {
            get
            {
                return this.GetValStringByKey(GERptAttr.FlowEnderRDT);
            }
            set
            {
                this.SetValByKey(GERptAttr.FlowEnderRDT, value);
            }
        }
        public int FlowEndNode
        {
            get
            {
                return this.GetValIntByKey(GERptAttr.FlowEndNode);
            }
            set
            {
                this.SetValByKey(GERptAttr.FlowEndNode, value);
            }
        }

        
        public string Title
        {
            get
            {
                return this.GetValStringByKey(GERptAttr.Title);
            }
            set
            {
                this.SetValByKey(GERptAttr.Title, value);
            }
        }
        public string FK_NY
        {
            get
            {
                return this.GetValStringByKey(GERptAttr.FK_NY);
            }
            set
            {
                this.SetValByKey(GERptAttr.FK_NY, value);
            }
        }
        public string FK_Dept
        {
            get
            {
                return this.GetValStringByKey(GERptAttr.FK_Dept);
            }
            set
            {
                this.SetValByKey(GERptAttr.FK_Dept, value);
            }
        }
        public int WFState
        {
            get
            {
                return this.GetValIntByKey(GERptAttr.WFState);
            }
            set
            {
                this.SetValByKey(GERptAttr.WFState, value);
            }
        }
        #endregion attrs

        #region attrs - attrs 
        private string _RptName = null;
        public string RptName
        {
            get
            {
                return _RptName;
            }
            set
            {
                this._enMap = null;
                _RptName = value;
            }
        }
        /// <summary>
        /// Map
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this.RptName == null)
                {
                    BP.Port.Emp emp = new BP.Port.Emp();
                    return emp.EnMap;
                }

                if (this._enMap == null)
                {
                    MapData md = new MapData();
                    md.No = this.RptName;
                    md.Retrieve();
                    this._enMap = MapData.GenerHisMap(md.No);
                }
                return this._enMap;
            }
        }
        /// <summary>
        /// 报表
        /// </summary>
        /// <param name="rptName"></param>
        public GERpt(string rptName)
        {
            this.RptName = rptName;
        }
        public GERpt()
        {
        }
        /// <summary>
        /// 报表
        /// </summary>
        /// <param name="rptName"></param>
        /// <param name="oid"></param>
        public GERpt(string rptName, int oid)
        {
            this.RptName = rptName;
            this.Retrieve();
        }
        #endregion attrs
    }
    /// <summary>
    /// 报表集合
    /// </summary>
    public class GERpts : BP.En.EntitiesOID
    {
        /// <summary>
        /// 报表集合
        /// </summary>
        public GERpts()
        {
        }

        public override Entity GetNewEntity
        {
            get
            {
                return new GERpt();
            }
        }
    }
}
