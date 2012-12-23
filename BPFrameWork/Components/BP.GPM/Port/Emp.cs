using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Port;

namespace BP.GPM
{
	/// <summary>
	/// 工作人员属性
	/// </summary>
    public class EmpAttr : BP.En.EntityNoNameAttr
    {
        #region 基本属性
        /// <summary>
        /// 部门
        /// </summary>
        public const string FK_Dept = "FK_Dept";
        /// <summary>
        /// 密码
        /// </summary>
        public const string Pass = "Pass";
        /// <summary>
        /// SID
        /// </summary>
        public const string SID = "SID";
        /// <summary>
        /// 菜单更新时间
        /// </summary>
        public const string UpdateMenu = "UpdateMenu";
        /// <summary>
        /// StaffID
        /// </summary>
        public const string StaffID = "StaffID";
        /// <summary>
        /// DepartmentID
        /// </summary>
        public const string DepartmentID = "DepartmentID";
        #endregion
    }
	/// <summary>
	/// Emp 的摘要说明。
	/// </summary>
    public class Emp : EntityNoName
    {
        public new string Name
        {
            get
            {
                if (BP.Web.WebUser.SysLang == "B5")
                    return Sys.Language.Turn2Traditional(this.GetValStrByKey("Name"));
                return this.GetValStrByKey("Name");
            }
            set
            {
                this.SetValByKey("Name", value);
            }
        }

        #region 扩展属性
        public int StaffID
        {
            get
            {
                return this.GetValIntByKey(EmpAttr.StaffID);
            }
            set
            {
                this.SetValByKey(EmpAttr.StaffID, value);
            }
        }
        /// <summary>
        /// 主要的部门。
        /// </summary>
        public Dept HisDept
        {
            get
            {

                try
                {
                    return new Dept(this.FK_Dept);
                }
                catch (Exception ex)
                {
                    throw new Exception("@获取操作员" + this.No + "部门[" + this.FK_Dept + "]出现错误,可能是系统管理员没有给他维护部门.@" + ex.Message);
                }
            }
        }
        /// <summary>
        /// 工作岗位集合。
        /// </summary>
        public Stations HisStations
        {
            get
            {
                EmpStations sts = new EmpStations();
                Stations mysts = sts.GetHisStations(this.No);
                return mysts;
                //return new Station(this.FK_Station);
            }
        }
        /// <summary>
        /// 部门
        /// </summary>
        public string FK_Dept
        {
            get
            {
                return this.GetValStrByKey(EmpAttr.FK_Dept);
            }
            set
            {
                this.SetValByKey(EmpAttr.FK_Dept, value);
            }
        }
        public string FK_DeptText
        {
            get
            {
                return this.GetValRefTextByKey(EmpAttr.FK_Dept);
            }
        }
        /// <summary>
        /// 密码
        /// </summary>
        public string Pass
        {
            get
            {
                return this.GetValStrByKey(EmpAttr.Pass);
            }
            set
            {
                this.SetValByKey(EmpAttr.Pass, value);
            }
        }
        /// <summary>
        /// SID
        /// </summary>
        public string SID
        {
            get
            {
                return this.GetValStrByKey(EmpAttr.SID);
            }
            set
            {
                this.SetValByKey(EmpAttr.SID, value);
            }
        }
        #endregion

        public bool CheckPass(string pass)
        {
            if (this.Pass == pass)
                return true;
            return false;
        }
        /// <summary>
        /// 工作人员
        /// </summary>
        public Emp()
        {

        }
        /// <summary>
        /// 工作人员编号
        /// </summary>
        /// <param name="_No">No</param>
        public Emp(string no)
        {
            this.No = no.Trim();
            if (this.No.Length == 0)
                throw new Exception("@要查询的操作员编号为空。");
            try
            {
                this.Retrieve();
            }
            catch (Exception ex1)
            {
                int i = this.RetrieveFromDBSources();
                if (i == 0)
                    throw new Exception("@用户或者密码错误：[" + no + "]，或者帐号被停用。@技术信息(从内存中查询出现错误)：ex1=" + ex1.Message);
            }
        }
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                uac.OpenForAppAdmin();
                return uac;
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

                Map map = new Map();

                #region 基本属性
                map.EnDBUrl =
                    new DBUrl(DBUrlType.AppCenterDSN); //要连接的数据源（表示要连接到的那个系统数据库）。
                map.PhysicsTable = "Port_Emp"; // 要物理表。
                map.DepositaryOfMap = Depositary.Application;    //实体map的存放位置.
                map.DepositaryOfEntity = Depositary.Application; //实体存放位置
                map.EnDesc = this.ToE("Emp", "用户"); // "用户";       // 实体的描述.
                map.EnType = EnType.App;   //实体类型。
                #endregion

                #region 字段
                /*关于字段属性的增加 */
                map.AddTBStringPK(EmpAttr.No, null, "编号", true, false, 1, 20, 100);
                map.AddTBString(EmpAttr.Name, null, "名称", true, false, 0, 100, 100);
                map.AddTBString(EmpAttr.Pass, "pub", "密码", false, false, 0, 20, 10);
                map.AddDDLEntities(EmpAttr.FK_Dept, null, "部门", new BP.Port.Depts(), true);
                map.AddTBString(EmpAttr.SID, null, "SID", false, false, 0, 200, 10);
                map.AddTBInt(EmpAttr.StaffID, 0, "StaffID", false, false);
                map.AddTBInt(EmpAttr.DepartmentID, 0, "DepartmentID", false, false);
                #endregion 字段

                map.AddSearchAttr(EmpAttr.FK_Dept);

                #region 增加一对多属性
                //他的部门权限
                map.AttrsOfOneVSM.Add(new EmpStations(), new Stations(), EmpStationAttr.FK_Emp, EmpStationAttr.FK_Station, DeptAttr.Name, DeptAttr.No, "岗位权限");
                map.AttrsOfOneVSM.Add(new EmpDepts(), new Depts(), EmpDeptAttr.FK_Emp, EmpDeptAttr.FK_Dept, DeptAttr.Name, DeptAttr.No, "工作部门");
                map.AttrsOfOneVSM.Add(new DeptSearchScorps(), new Depts(), EmpDeptAttr.FK_Emp, EmpDeptAttr.FK_Dept, DeptAttr.Name, DeptAttr.No, "查询权限");
                #endregion

                RefMethod rm = new RefMethod();
                rm.Title = "与CCIM数据同步";
                rm.ClassMethodName = this.ToString() + ".DoSubmitToCCIM";
                rm.IsForEns = false;
                map.AddRefMethod(rm);

                this._enMap = map;
                return this._enMap;
            }
        }
        public string DoSubmitToCCIM()
        {
            try
            {
                Emps ens = new Emps();
                ens.RetrieveAllFromDBSource();
                foreach (Emp en in ens)
                {
                    if (en.StaffID == 0)
                    {
                        en.StaffID = DBAccess.GenerOID();
                        en.Update();
                    }

                    Paras ps = new Paras();
                    ps.Add("UserID", en.StaffID);
                    BP.DA.DBProcedure.RunSP("sp_UpdateUser", ps);
                }

                /* 有可能是部门发生变化. */
                Dept dept = new Dept();
                dept.DoSubmitToCCIM();

                return "所有的数据同步执行成功。";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

             
        }
        public override Entities GetNewEntities
        {
            get { return new Emps(); }
        }
    }
	/// <summary>
	/// 工作人员
	/// </summary>
    public class Emps : EntitiesNoName
    {
        /// <summary>
        /// 得到它的 Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new Emp();
            }
        }
        /// <summary>
        /// 工作人员s
        /// </summary>
        public Emps()
        {
        }
        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        public override int RetrieveAll()
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(EmpAttr.FK_Dept, " like ", BP.Web.WebUser.FK_Dept + "%");
            qo.addOrderBy(EmpAttr.No);
            return qo.DoQuery();
        }
    }
}
 