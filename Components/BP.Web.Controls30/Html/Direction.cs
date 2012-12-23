using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.Sys
{
	/// <summary>
	/// 方向
	/// </summary>
    public class DirectionAttr : EntityNoNameAttr
    {
        /// <summary>
        /// FK_Dept
        /// </summary>
        public const string FK_Dept = "FK_Dept";
    }
	/// <summary>
	/// 方向
	/// </summary>
	public class Direction : EntityNoName
	{
		#region  属性
		/// <summary>
		///  隶属部门编号
		/// </summary>
		public string  FK_Dept
		{
			get
			{
				return this.GetValStringByKey(DirectionAttr.FK_Dept);
			}
			set
			{
				SetValByKey(DirectionAttr.FK_Dept,value);
			}
		}		
		#endregion 
		 
		#region 构造函数
		/// <summary>
		/// 方向
		/// </summary>
		public Direction(){}
		/// <summary>
		/// 方向
		/// </summary>
		/// <param name="_No"></param>
		public Direction(string _No) :base(_No)
		{ 
		}
		/// <summary>
		/// Map
		/// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("Sys_Direction");
                map.EnDesc = "方向";

                map.EnType = EnType.App;
                map.DepositaryOfMap = Depositary.Application;
                map.DepositaryOfEntity = Depositary.Application;

                map.AddTBStringPK(DirectionAttr.No, null, "编号", true, false, 4, 20, 100);
                map.AddTBString(DirectionAttr.Name, null, "名称", true, false, 0, 50, 200);

                this._enMap = map;
                return this._enMap;
            }
        }
		#endregion 

	}
	/// <summary>
	/// 方向集合
	/// </summary>
    public class Directions : EntitiesNoName
    {
        /// <summary>
        /// GetNewEntity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new Direction();
            }
        }
        /// <summary>
        /// 方向集合()
        /// </summary>
        public Directions() { }
    }
}
 