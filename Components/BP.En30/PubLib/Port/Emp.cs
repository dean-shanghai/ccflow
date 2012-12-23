using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Port;

namespace BP.Port
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
        /// FK_Unit
        /// </summary>
        public const string FK_Unit = "FK_Unit";
        /// <summary>
        /// 密码
        /// </summary>
        public const string Pass = "Pass";
        ///// <summary>
        ///// PID
        ///// </summary>
        //public const string PID = "PID";
        ///// <summary>
        ///// pin
        ///// </summary>
        //public const string PIN = "PIN";
        ///// <summary>
        ///// UKEY密码
        /////</summary>
        //public const string KeyPass = "KeyPass";
        ///// <summary>
        ///// 是否使用UKEY
        ///// </summary>
        //public const string IsUSBKEY = "IsUSBKEY";
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

        //public new string PID
        //{
        //    get
        //    {              
        //        return this.GetValStrByKey("PID");
        //    }
        //    set
        //    {
        //        this.SetValByKey("PID", value);
        //    }
        //}
        //public new string PIN
        //{
        //    get
        //    {
        //        return this.GetValStrByKey("PIN");
        //    }
        //    set
        //    {
        //        this.SetValByKey("PIN", value);
        //    }
        //}

        //public new string KeyPass
        //{
        //    get
        //    {
        //        return this.GetValStrByKey("KeyPass");
        //    }
        //    set
        //    {
        //        this.SetValByKey("KeyPass", value);
        //    }
        //}

        //public new string IsUSBKEY
        //{
        //    get
        //    {
        //        return this.GetValStrByKey("IsUSBKEY");
        //    }
        //    set
        //    {
        //        this.SetValByKey("IsUSBKEY", value);
        //    }
        //}
        #region 扩展属性
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
        /// 工作部门集合
        /// </summary>
        public Depts HisDepts
        {
            get
            {
                EmpDepts sts = new EmpDepts();
                Depts dpts = sts.GetHisDepts(this.No);
                if (dpts.Count==0)
                {
                    string sql = "select fk_dept from port_emp where no='"+this.No+"' and fk_dept in(select no from port_dept)";
                    string fk_dept = BP.DA.DBAccess.RunSQLReturnVal(sql) as string;
                    if (fk_dept == null)
                        return dpts;

                    Dept dept = new Dept(fk_dept);
                    dpts.AddEntity(dept);
                }
                return dpts;
            }
        }
        private BP.Port.Unit _HisUnit = null;
        public BP.Port.Unit HisUnit
        {
            get
            {
                if (_HisUnit == null)
                {
                    string sql = "SELECT FK_Unit FROM Port_Emp WHERE No='" + this.No+"'";
                    string no= BP.DA.DBAccess.RunSQLReturnString(sql);
                    _HisUnit = new Unit(no);
                }
                return _HisUnit;
            }
        }
        /// <summary>
        /// 单位
        /// </summary>
        public string FK_Unit
        {
            get
            {
              return  this.HisUnit.No;
            }
        }
        public string FK_UnitText
        {
            get
            {
                return this.HisUnit.Name;
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
                map.EnDesc = this.ToE("Emp", "用户"); // "用户"; // 实体的描述.
                map.EnType = EnType.App;   //实体类型。
                #endregion

                #region 字段
                /*关于字段属性的增加 */
                map.AddTBStringPK(EmpAttr.No, null, this.ToE("No", "编号"), true, false, 1, 20, 30);
                map.AddTBString(EmpAttr.Name, null, this.ToE("Name", "名称"), true, false, 0, 100, 30);
                
                map.AddTBString(EmpAttr.Pass, "pub", this.ToE("Pass", "密码"), false, false, 0, 20, 10);
                map.AddDDLEntities(EmpAttr.FK_Dept, null, this.ToE("Dept", "部门"), new Port.Depts(), true);


                //map.AddTBString(EmpAttr.PID, null, this.ToE("PID", "UKEY的PID"), true, false, 0, 100, 30);
                //map.AddTBString(EmpAttr.PIN, null, this.ToE("PIN", "UKEY的PIN"), true, false, 0, 100, 30);
                //map.AddTBString(EmpAttr.KeyPass, null, this.ToE("KeyPass", "UKEY的KeyPass"), true, false, 0, 100, 30);
                //map.AddTBString(EmpAttr.IsUSBKEY, null, this.ToE("IsUSBKEY", "是否使用usbkey"), true, false, 0, 100, 30);
                // map.AddDDLSysEnum("Sex", 0, "性别", "@0=女@1=男");
                #endregion 字段

                map.AddSearchAttr(EmpAttr.FK_Dept);

                #region 增加点对多属性
                //他的部门权限
                map.AttrsOfOneVSM.Add(new EmpDepts(), new Depts(), EmpDeptAttr.FK_Emp, EmpDeptAttr.FK_Dept, DeptAttr.Name, DeptAttr.No, "部门权限");
                map.AttrsOfOneVSM.Add(new EmpStations(), new Stations(), EmpStationAttr.FK_Emp, EmpStationAttr.FK_Station,
                    DeptAttr.Name, DeptAttr.No, "岗位权限");
                #endregion

                this._enMap = map;
                return this._enMap;
            }
        }
        public override Entities GetNewEntities
        {
            get { return new Emps(); } 
        }
    }
	/// <summary>
	/// 工作人员
	// </summary>
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
 