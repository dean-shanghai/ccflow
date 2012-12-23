using System;
using System.Collections;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.GPM
{
    /// <summary>
    /// 控制方式
    /// </summary>
    public enum CtrlWay
    {
        /// <summary>
        /// 任何人
        /// </summary>
        AnyOne,
        /// <summary>
        /// 按岗位
        /// </summary>
        ByStation,
        /// <summary>
        /// 按部门
        /// </summary>
        ByDept,
        /// <summary>
        /// 按人员
        /// </summary>
        ByEmp,
        /// <summary>
        /// 按sql
        /// </summary>
        BySQL
    }
    public enum MenuType
    {
        /// <summary>
        /// 目录
        /// </summary>
        Dir,
        /// <summary>
        /// 功能
        /// </summary>
        Func,
        /// <summary>
        /// 功能点
        /// </summary>
        FuncDot
    }
    /// <summary>
    /// 菜单
    /// </summary>
    public class MenuAttr : EntityTreeAttr
    {
        /// <summary>
        /// 系统类型
        /// </summary>
        public const string MenuType = "MenuType";
        /// <summary>
        /// 控制方法
        /// </summary>
        public const string CtrlWay = "CtrlWay";
        /// <summary>
        /// 顺序
        /// </summary>
        public const string Idx = "Idx";
        /// <summary>
        /// 系统
        /// </summary>
        public const string FK_STem = "FK_STem";
        /// <summary>
        /// 树形编号
        /// </summary>
        public const string TreeNo = "TreeNo";
        /// <summary>
        /// 图片
        /// </summary>
        public const string Img = "Img";
        /// <summary>
        /// 连接
        /// </summary>
        public const string Url = "Url";
        /// <summary>
        /// 控制内容
        /// </summary>
        public const string CtrlObjs = "CtrlObjs";
    }
    /// <summary>
    /// 菜单
    /// </summary>
    public class Menu : EntityTree
    {
        #region 属性

        public override UAC HisUAC
        {
            get
            {
            //    UAC uac = new UAC();
                return base.HisUAC;
            }
        }
        public string CtrlObjs
        {
            get
            {
                return this.GetValStringByKey(MenuAttr.CtrlObjs);
            }
            set
            {
                this.SetValByKey(MenuAttr.CtrlObjs, value);
            }
        }
        public string Name
        {
            get
            {
                return this.GetValStringByKey(MenuAttr.Name);
            }
            set
            {
                this.SetValByKey(MenuAttr.Name, value);
            }
        }
        public CtrlWay HisCtrlWay
        {
            get
            {
                return (CtrlWay)this.GetValIntByKey(MenuAttr.CtrlWay);
            }
            set
            {
                this.SetValByKey(MenuAttr.CtrlWay, (int)value);
            }
        }
        /// <summary>
        /// 功能
        /// </summary>
        public MenuType HisMenuType
        {
            get
            {
                return (MenuType)this.GetValIntByKey(MenuAttr.MenuType);
            }
            set
            {
                this.SetValByKey(MenuAttr.MenuType, (int)value);
            }
        }
        /// <summary>
        /// 是否是ccSytem
        /// </summary>
        public int MenuType
        {
            get
            {
                return this.GetValIntByKey(MenuAttr.MenuType);
            }
            set
            {
                this.SetValByKey(MenuAttr.MenuType, value);
            }
        }
        public int Idx
        {
            get
            {
                return this.GetValIntByKey(MenuAttr.Idx);
            }
            set
            {
                this.SetValByKey(MenuAttr.Idx, value);
            }
        }

        public string FK_STem
        {
            get
            {
                return this.GetValStringByKey(MenuAttr.FK_STem);
            }
            set
            {
                this.SetValByKey(MenuAttr.FK_STem, value);
            }
        }
        public string TreeNo
        {
            get
            {
                return this.GetValStringByKey(MenuAttr.TreeNo);
            }
            set
            {
                this.SetValByKey(MenuAttr.TreeNo, value);
            }
        }
        public string Img
        {
            get
            {
                string s = this.GetValStringByKey("WebPath");
                if (string.IsNullOrEmpty(s))
                {
                    if (this.HisMenuType == GPM.MenuType.Dir)
                        return "../../Images/Btn/View.gif";
                    else
                        return "../../Images/Btn/Go.gif";
                }
                else
                {
                    return s;
                }
            }
        }
        public string Url
        {
            get
            {
                return this.GetValStringByKey(MenuAttr.Url);
            }
            set
            {
                this.SetValByKey(MenuAttr.Url, value);
            }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 菜单
        /// </summary>
        public Menu()
        {
        }
        /// <summary>
        /// 菜单
        /// </summary>
        /// <param name="mypk"></param>
        public Menu(string no)
        {
            this.Retrieve();
        }
        /// <summary>
        /// EnMap
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map("GPM_Menu");  // 类的基本属性.
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "系统";
                map.EnType = EnType.Sys;

                #region 与树有关的必备属性.
                map.AddTBStringPK(MenuAttr.ID, null, "编号", true, true, 1, 10, 10);
                map.AddTBString(MenuAttr.Name, null, "名称", true, false, 0, 60, 400);
                map.AddTBString(MenuAttr.PID, null, "父节点编号", true, true, 0, 10, 10);
                map.AddTBString(MenuAttr.TreeNo, null, "树编号", true, true, 0, 60, 400);
                map.AddTBInt(MenuAttr.Idx, 0, "顺序号", false, false);
                #endregion 与树有关的必备属性.

                // 类的字段属性.
                map.AddDDLSysEnum(MenuAttr.MenuType, 0, "菜单类型", true, true, MenuAttr.MenuType,
                    "@0=目录@1=功能@2=功能控制点");
                map.AddDDLEntities(MenuAttr.FK_STem, null, "系统", new STems(), true);
                map.AddDDLSysEnum(STemAttr.CtrlWay, 1, "控制方式", true, true, STemAttr.CtrlWay,
                    "@0=游客@1=所有人员@2=按岗位@3=按部门@4=按人员@5=按SQL");
                map.AddTBString(MenuAttr.CtrlObjs, null, "控制内容", false, false, 0, 4000, 20);
                map.AddTBString(STemAttr.Url, null, "连接", true, false, 0, 3900, 20, true);
                map.AddMyFile("图标");  // 附件属性.

                // 一对多的关系.
                map.AttrsOfOneVSM.Add(new ByStations(), new Stations(), ByStationAttr.RefObj, ByStationAttr.FK_Station,
                    StationAttr.Name, StationAttr.No, "可访问的岗位");
                map.AttrsOfOneVSM.Add(new ByDepts(), new Depts(), ByStationAttr.RefObj, ByDeptAttr.FK_Dept,
                    DeptAttr.Name, DeptAttr.No, "可访问的部门");
                map.AttrsOfOneVSM.Add(new ByEmps(), new Emps(), ByStationAttr.RefObj, ByEmpAttr.FK_Emp,
                    EmpAttr.Name, EmpAttr.No, "可访问的人员");
                map.AddSearchAttr(MenuAttr.FK_STem); // 查询属性.
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
    /// <summary>
    /// 菜单s
    /// </summary>
    public class Menus : EntitiesTree
    {
        #region 构造
        /// <summary>
        /// 菜单s
        /// </summary>
        public Menus()
        {
        }
        /// <summary>
        /// 得到它的 Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new Menu();
            }
        }
        #endregion
    }
}
