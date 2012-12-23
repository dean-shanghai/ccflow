using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Web;

namespace BP.GPM
{
	/// <summary>
	/// 部门属性
	/// </summary>
    public class DeptAttr : EntityNoNameAttr
	{
		/// <summary>
		/// 部门性质
		/// </summary>
		public const string WorkCharacter="WorkCharacter";
		/// <summary>
		/// 部门
		/// </summary>
		public const string FK_Dept="FK_Dept";
		/// <summary>
		/// 工作基次
		/// </summary>
		public const string WorkFloor="WorkFloor";
		/// <summary>
		/// 部门性质
		/// </summary>
		public const string DeptType="DeptType";
        /// <summary>
        /// DepartmentID
        /// </summary>
        public const string DepartmentID = "DepartmentID";
        public const string ParentID = "ParentID";

        
	}
	/// <summary>
	/// 部门
	/// </summary>
	public class Dept:EntityNoName
	{
		#region 属性
        public new string Name
        {
            get
            {
                if (BP.Web.WebUser.SysLang == "B5")
                    return Sys.Language.Turn2Traditional(this.GetValStrByKey("Name"));

                return this.GetValStrByKey("Name");
            }
        }
        /// <summary>
        /// 级别
        /// </summary>
        public int Grade
        {
            get
            {
                return this.No.Length / 2;
            }
        }
        public int DepartmentID
        {
            get
            {
                return this.GetValIntByKey(DeptAttr.DepartmentID);
            }
            set
            {
                this.SetValByKey(DeptAttr.DepartmentID, value);
            }
        }
        public int ParentID
        {
            get
            {
                return this.GetValIntByKey(DeptAttr.ParentID);
            }
            set
            {
                this.SetValByKey(DeptAttr.ParentID, value);
            }
        }
		#endregion

		#region 构造函数
		/// <summary>
		/// 部门
		/// </summary>
		public Dept(){}
		/// <summary>
		/// 部门
		/// </summary>
		/// <param name="no">编号</param>
        public Dept(string no) : base(no){}
		#endregion

		#region 重写方法
		public override UAC HisUAC
		{
			get
			{
				UAC uac = new UAC();
				uac.OpenForSysAdmin();
				return uac;
			}
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

                Map map = new Map();
                map.EnDBUrl = new DBUrl(DBUrlType.AppCenterDSN); //连接到的那个数据库上. (默认的是: AppCenterDSN )
                map.PhysicsTable = "Port_Dept";
                map.EnType = EnType.Admin;

                map.EnDesc = this.ToE("Dept", "部门"); // "部门";// 实体的描述.
                map.DepositaryOfEntity = Depositary.Application; //实体map的存放位置.
                map.DepositaryOfMap = Depositary.Application;    // Map 的存放位置.
                map.CodeStruct = "22222222";
                map.IsAllowRepeatNo = false;
                map.IsCheckNoLength = false;
                map.IsAutoGenerNo = false;

                map.AdjunctType = AdjunctType.None;
                map.AddTBStringPK(DeptAttr.No, null, null, true, false, 2, 20, 40);
                map.AddTBString(DeptAttr.Name, null,null, true, false, 0, 60, 400);

                map.AddTBInt(DeptAttr.DepartmentID, 0, "DepartmentID", false, false);
                map.AddTBInt(DeptAttr.ParentID, 0, "ParentID", false, false);

                
                //   map.AddTBInt(DeptAttr.Grade, 0, "级次", true, false);
                //  map.AddBoolean(DeptAttr.IsDtl, false, "是否明细", true, true);

                RefMethod rm = new RefMethod();
                rm.Title = "与CCIM数据同步";
                rm.ClassMethodName = this.ToString() + ".DoSubmitToCCIM";
                rm.IsForEns = false;
                map.AddRefMethod(rm);

                this._enMap = map;
                return this._enMap;
            }
		}
        /// <summary>
        /// 提交到ccim中去。
        /// </summary>
        /// <returns></returns>
        public string DoSubmitToCCIM()
        {
            try
            {
                string sql = "";
                Depts ens = new Depts();
                ens.RetrieveAll(DeptAttr.No);
                foreach (Dept en in ens)
                {
                    if (en.DepartmentID == 0)
                    {
                        en.DepartmentID = DBAccess.GenerOID();
                        if (this.No.Length == 2)
                        {
                            en.ParentID = 0;
                            en.Update();
                        }
                    }

                    if (en.ParentID == 0 && en.No.Trim().Length > 2)
                    {
                        //找出它的父节点的ID号.
                        string no = en.No.Trim().Substring(0, en.No.Trim().Length - 2);
                        no = no.Trim();
                        sql = "SELECT " + DeptAttr.DepartmentID + " FROM Port_Dept WHERE No='" + no + "'";
                        int pID = DBAccess.RunSQLReturnValInt(sql, 0);
                        if (pID == 0)
                            throw new Exception("编码机制不对，没有获取上级编号 SQL=" + sql);
                        en.ParentID = pID;
                        en.Update();
                    }

                    DBAccess.RunSQL("UPDATE Port_Emp SET " + EmpAttr.DepartmentID + "=" + en.DepartmentID + " WHERE FK_Dept='" + en.No + "' ");

                    Paras ps = new Paras();
                    ps.Add("@DeptID", en.DepartmentID);
                    BP.DA.DBProcedure.RunSP("sp_UpdateDept", ps);
                }
                return "所有的数据同步执行成功。";
            }
            catch (Exception ex)
            {
                return ex.Message.Replace("'","‘");
            }
        }

        protected override bool beforeUpdateInsertAction()
        {
           // this.Grade = this.No.Length / 2;
            return base.beforeUpdateInsertAction();
        }
		#endregion
	}
	/// <summary>
	///得到集合
	/// </summary>
	public class Depts: EntitiesNoName
	{
		/// <summary>
		/// 查询全部。
		/// </summary>
		/// <returns></returns>
        public override int RetrieveAll()
        {

            if (Web.WebUser.No == "admin")
                return base.RetrieveAll();

            QueryObject qo11 = new QueryObject(this);
            qo11.AddWhere(DeptAttr.No, " like ", Web.WebUser.FK_Dept + "%");
            return qo11.DoQuery();
        }
		/// <summary>
		/// 得到一个新实体
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new Dept();
			}
		}
		/// <summary>
		/// create ens
		/// </summary>
		public Depts(){}
		
	}
}
