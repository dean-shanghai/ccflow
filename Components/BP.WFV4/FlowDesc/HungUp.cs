using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.WF;
using BP.Port;

namespace BP.WF
{
	/// <summary>
	/// 挂起 属性
	/// </summary>
    public class HungUpAttr:EntityMyPKAttr
    {
        #region 基本属性
        public const string Title = "Title";
        /// <summary>
        /// 工作ID
        /// </summary>
        public const string WorkID = "WorkID";
        /// <summary>
        /// 执行人
        /// </summary>
        public const string Rec = "Rec";
        /// <summary>
        /// 通知给
        /// </summary>
        public const string NoticeTo = "NoticeTo";
        /// <summary>
        /// 挂起原因
        /// </summary>
        public const string Note = "Note";
        /// <summary>
        /// 操作日期
        /// </summary>
        public const string RDT = "RDT";
        /// <summary>
        /// 节点ID
        /// </summary>
        public const string FK_Node = "FK_Node";
        /// <summary>
        /// 接受人
        /// </summary>
        public const string Accepter = "Accepter";
        /// <summary>
        /// 接触挂起日期
        /// </summary>
        public const string RelData = "RelData";
        /// <summary>
        /// 挂起方式.
        /// </summary>
        public const string HungUpWay = "HungUpWay";
        #endregion
    }
	/// <summary>
	/// 挂起
	/// </summary>
    public class HungUp:EntityMyPK
    {
        #region 属性
        public int HungUpWay
        {
            get
            {
                return this.GetValIntByKey(HungUpAttr.HungUpWay);
            }
            set
            {
                this.SetValByKey(HungUpAttr.HungUpWay, value);
            }
        }
        public int FK_Node
        {
            get
            {
                return this.GetValIntByKey(HungUpAttr.FK_Node);
            }
            set
            {
                this.SetValByKey(HungUpAttr.FK_Node, value);
            }
        }
        public Int64 WorkID
        {
            get
            {
                return this.GetValInt64ByKey(HungUpAttr.WorkID);
            }
            set
            {
                this.SetValByKey(HungUpAttr.WorkID, value);
            }
        }
        /// <summary>
        /// 挂起标题
        /// </summary>
        public string Title
        {
            get
            {
                string s= this.GetValStringByKey(HungUpAttr.Title);
                if (string.IsNullOrEmpty(s))
                    s = "来自@Rec的挂起信息.";
                return s;
            }
            set
            {
                this.SetValByKey(HungUpAttr.Title, value);
            }
        }
        /// <summary>
        /// 挂起原因
        /// </summary>
        public string Note
        {
            get
            {
               return this.GetValStringByKey(HungUpAttr.Note);
            }
            set
            {
                this.SetValByKey(HungUpAttr.Note, value);
            }
        }
        public string Rec
        {
            get
            {
                return this.GetValStringByKey(HungUpAttr.Rec);
            }
            set
            {
                this.SetValByKey(HungUpAttr.Rec, value);
            }
        }
        public string RelData
        {
            get
            {
                return this.GetValStringByKey(HungUpAttr.RelData);
            }
            set
            {
                this.SetValByKey(HungUpAttr.RelData, value);
            }
        }
        public string RDT
        {
            get
            {
                return this.GetValStringByKey(HungUpAttr.RDT);
            }
            set
            {
                this.SetValByKey(HungUpAttr.RDT, value);
            }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 挂起
        /// </summary>
        public HungUp()
        {
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

                Map map = new Map("WF_HungUp");
                map.EnDesc = "挂起";
                map.EnType = EnType.Admin;

                map.AddMyPK();
                map.AddTBInt(HungUpAttr.FK_Node, 0, "节点ID", true, true);
                map.AddTBInt(HungUpAttr.WorkID, 0, "WorkID", true, true);
                map.AddDDLSysEnum(HungUpAttr.HungUpWay, 0, "挂起方式", true, true, HungUpAttr.HungUpWay, 
                    "@0=无限挂起@1=按指定的时间解除挂起并通知我自己@2=按指定的时间解除挂起并通知所有人");
                map.AddTBDateTime(HungUpAttr.RelData, null, "恢复挂起时间", true, false);

            //    map.AddTBString(HungUpAttr.Accepter, null, "接受人", true, false, 0, 500, 10, true);
                map.AddTBStringDoc(HungUpAttr.Note, null, "挂起原因(标题与内容支持变量)", true, false, true);

                map.AddTBString(HungUpAttr.Rec, null, "挂起人", true, false, 0, 50, 10, true);
                map.AddTBDateTime(HungUpAttr.RDT, null, "操作时间", true, false);

                this._enMap = map;
                return this._enMap;
            }
        }
        /// <summary>
        /// 执行释放挂起
        /// </summary>
        public void DoRelease()
        {
        }
        #endregion
    }
	/// <summary>
	/// 挂起
	/// </summary>
	public class HungUps: EntitiesMyPK
	{
		#region 方法
		/// <summary>
		/// 得到它的 Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new HungUp();
			}
		}
		/// <summary>
        /// 挂起
		/// </summary>
		public HungUps(){} 		 
		#endregion
	}
}
