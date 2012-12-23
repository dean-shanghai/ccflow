using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.Sys
{
	/// <summary>
	/// Img播放速度
	/// </summary>
    public class ImgSpeedAttr : EntityNoNameAttr
    {
        /// <summary>
        /// FK_Dept
        /// </summary>
        public const string FK_Dept = "FK_Dept";
    }
	/// <summary>
	/// Img播放速度
	/// </summary>
	public class ImgSpeed : EntityNoName
	{
		#region  属性
		/// <summary>
		///  隶属部门编号
		/// </summary>
		public string  FK_Dept
		{
			get
			{
				return this.GetValStringByKey(ImgSpeedAttr.FK_Dept);
			}
			set
			{
				SetValByKey(ImgSpeedAttr.FK_Dept,value);
			}
		}		
		#endregion 
		 
		#region 构造函数
		/// <summary>
		/// Img播放速度
		/// </summary>
		public ImgSpeed(){}
		/// <summary>
		/// Img播放速度
		/// </summary>
		/// <param name="_No"></param>
		public ImgSpeed(string _No) :base(_No)
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

                Map map = new Map("Sys_ImgSpeed");
                map.EnDesc = "Img播放速度";

                map.EnType = EnType.App;
                map.DepositaryOfMap = Depositary.Application;
                map.DepositaryOfEntity = Depositary.Application;

                map.AddTBStringPK(ImgSpeedAttr.No, null, "编号", true, false, 4, 20, 100);
                map.AddTBString(ImgSpeedAttr.Name, null, "名称", true, false, 0, 50, 200);

                this._enMap = map;
                return this._enMap;
            }
        }
		#endregion 

	}
	/// <summary>
	/// Img播放速度集合
	/// </summary>
    public class ImgSpeeds : EntitiesNoName
    {
        /// <summary>
        /// GetNewEntity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new ImgSpeed();
            }
        }
        /// <summary>
        /// Img播放速度集合()
        /// </summary>
        public ImgSpeeds() { }
    }
}
 