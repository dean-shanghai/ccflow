using System;
using System.Collections;
using System.Data;
using BP.DA;
using BP.Web;
using BP.En;

namespace BP.GPM
{
    /// <summary>
    /// 人员菜单
    /// </summary>
    public class MenuEmpAttr
    {
        public const string FK_Emp = "FK_Emp";
        public const string FK_Menu = "FK_Menu";
    }
    /// <summary>
    /// 菜单
    /// </summary>
    public class MenuEmp : EntityMyPK
    {
        #region 属性
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
        public string FK_Emp
        {
            get
            {
                return this.GetValStringByKey(MenuEmpAttr.FK_Emp);
            }
            set
            {
                this.SetValByKey(MenuEmpAttr.FK_Emp, value);
            }
        }
        public string FK_Menu
        {
            get
            {
                return this.GetValStringByKey(MenuEmpAttr.FK_Menu);
            }
            set
            {
                this.SetValByKey(MenuEmpAttr.FK_Menu, value);
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
        public MenuEmp()
        {
        }
        /// <summary>
        /// 菜单
        /// </summary>
        /// <param name="mypk"></param>
        public MenuEmp(string no)
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
                Map map = new Map("GPM_MenuEmp");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "人员菜单";
                map.EnType = EnType.Sys;

                map.AddMyPK();

                map.AddTBString(MenuEmpAttr.FK_Emp, null, "FK_Emp", true, false, 0, 30, 20);
                map.AddTBInt(MenuEmpAttr.FK_Menu, 0, "FK_Menu", true, false);

                map.AddTBString(MenuAttr.TreeNo, null, "编号", true, false, 2, 30, 20);
                map.AddTBString(MenuAttr.Name, null, "名称", true, false, 0, 3900, 20);
                map.AddDDLSysEnum(MenuAttr.MenuType, 0, "菜单类型", true, true,
                    MenuAttr.MenuType, "@0=目录@1=功能@2=功能控制点");

                map.AddTBString(MenuAttr.FK_STem, null, "FK_Emp", true, false, 0, 30, 20);
                map.AddTBString(STemAttr.Url, null, "连接", true, false, 0, 3900, 20, true);
                map.AddMyFile("图标");

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

        /// <summary>
        /// 初试化人员菜单
        /// </summary>
        public void InitMenu()
        {
            MenuEmp myme = new MenuEmp();
            myme.Delete(MenuEmpAttr.FK_Emp, WebUser.No);

            Menus ens = new Menus();
            ens.RetrieveAllFromDBSource();
            foreach (Menu en in ens)
            {
                /* 把此人能看到的菜单init 里面去。*/
                if (Glo.IsCanDoIt(en.ID, en.HisCtrlWay) == false)
                    continue;

                MenuEmp me = new MenuEmp();
                me.Copy(en);
                me.FK_Emp = WebUser.No;
                me.FK_Menu = en.ID;
                me.MyPK = me.FK_Menu + "_" + me.FK_Emp;
                me.Insert();
            }
        }
    }
    /// <summary>
    /// 菜单s
    /// </summary>
    public class MenuEmps : EntitiesOID
    {
        #region 构造
        /// <summary>
        /// 菜单s
        /// </summary>
        public MenuEmps()
        {
        }
        /// <summary>
        /// 得到它的 Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new MenuEmp();
            }
        }
        #endregion
    }
}
