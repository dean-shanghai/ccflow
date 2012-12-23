using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.GPM
{
    /// <summary>
    /// 单点登陆系统
    /// </summary>
    public class STemAttr : EntityNoNameAttr
    {
        /// <summary>
        /// 系统类型
        /// </summary>
        public const string XieYi = "XieYi";
        /// <summary>
        /// 控制方法
        /// </summary>
        public const string CtrlWay = "CtrlWay";
        /// <summary>
        /// 顺序
        /// </summary>
        public const string Idx = "Idx";
        /// <summary>
        /// 应用类型
        /// </summary>
        public const string AppModel = "AppModel";
        /// <summary>
        /// LIANJIE
        /// </summary>
        public const string Url = "Url";
        /// <summary>
        /// 是否启用.
        /// </summary>
        public const string IsEnable = "IsEnable";
    }
    /// <summary>
    /// 单点登陆系统
    /// </summary>
    public class STem : EntityNoName
    {
        #region 属性
        /// <summary>
        /// 打开方式
        /// </summary>
        public string OpenWay
        {
            get
            {
                int openWay = 0;

                switch (openWay)
                {
                    case 0:
                        return "_blank";
                    case 1:
                        return this.No;
                    default:
                        return "";
                }
            }
        }
        /// <summary>
        /// 路径
        /// </summary>
        public string WebPath
        {
            get
            {
                return this.GetValStringByKey("WebPath");
            }
        }
        /// <summary>
        /// ICON
        /// </summary>
        public string ICON
        {
            get
            {
                return this.WebPath;
            }
            set
            {
                this.SetValByKey(PerAlertAttr.ICON, value);
            }
        }
        /// <summary>
        /// 连接
        /// </summary>
        public string Url
        {
            get
            {
                string url = this.GetValStrByKey(STemAttr.Url);
                if (url.Contains("?"))
                    url += "&UserNo=" + Web.WebUser.No + "&SID=" + Web.WebUser.SID;
                else
                    url += "?UserNo=" + Web.WebUser.No + "&SID=" + Web.WebUser.SID;
                return url;
            }
            set
            {
                this.SetValByKey(STemAttr.Url, value);
            }
        }
        /// <summary>
        /// 协议类型
        /// </summary>
        public int XieYi
        {
            get
            {
                return this.GetValIntByKey(STemAttr.XieYi);
            }
            set
            {
                this.SetValByKey(STemAttr.XieYi, value);
            }
        }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable
        {
            get
            {
                return this.GetValBooleanByKey(STemAttr.IsEnable);
            }
            set
            {
                this.SetValByKey(STemAttr.IsEnable, value);
            }
        }
        /// <summary>
        /// 顺序
        /// </summary>
        public int Idx
        {
            get
            {
                return this.GetValIntByKey(STemAttr.Idx);
            }
            set
            {
                this.SetValByKey(STemAttr.Idx, value);
            }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 单点登陆系统
        /// </summary>
        public STem()
        {
        }
        /// <summary>
        /// 单点登陆系统
        /// </summary>
        /// <param name="mypk"></param>
        public STem(string no)
        {
            this.No = no;
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
                Map map = new Map("GPM_STem");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "系统";
                map.EnType = EnType.Sys;

                map.AddTBStringPK(STemAttr.No, null, "编号", true, false, 2, 30, 20);
                map.AddTBString(STemAttr.Name, null, "名称", true, false, 0, 3900, 20);
                map.AddDDLSysEnum(STemAttr.XieYi, 0, "连接协议", true,true,
                    STemAttr.XieYi, "@0=基于SID@1=基于SSL@2=基于ACL@3=基于LDAP@4=基于http");
                map.AddDDLSysEnum(STemAttr.CtrlWay, 0, "控制方式", true, true,STemAttr.CtrlWay, "@0=游客@1=所有系统人员@2=按岗位@3=按部门@4=按人员@5=按SQL");
                map.AddTBString(STemAttr.CtrlWay, null, "SQL表达式", true, false, 0, 3900, 20);
                map.AddDDLSysEnum(STemAttr.AppModel, 0, "应用类型", true, true,STemAttr.AppModel, "@0=BS系统@1=CS系统");
                map.AddTBString(STemAttr.Url, null, "连接", true, false, 0, 3900, 20, true);
                map.AddDDLSysEnum(BarAttr.OpenWay, 0, "打开方式", true, true, BarAttr.OpenWay, "@0=新窗口@1=本窗口@2=覆盖新窗口");
                map.AddTBInt(STemAttr.Idx, 0, "显示顺序", true, false);
                map.AddBoolean(STemAttr.IsEnable, true, "是否启用", true, false);
                
                map.AddMyFile("ICON");

                map.AddSearchAttr(STemAttr.AppModel);
                map.AddSearchAttr(STemAttr.XieYi);
                map.AddSearchAttr(BarAttr.OpenWay);




                map.AttrsOfOneVSM.Add(new ByStations(), new Stations(), ByStationAttr.RefObj,
                    ByStationAttr.FK_Station, StationAttr.Name, StationAttr.No, "可访问的岗位");
                map.AttrsOfOneVSM.Add(new ByDepts(), new Depts(), ByStationAttr.RefObj, ByDeptAttr.FK_Dept, DeptAttr.Name, DeptAttr.No, "可访问的部门");
                map.AttrsOfOneVSM.Add(new ByEmps(), new Emps(), ByStationAttr.RefObj, ByEmpAttr.FK_Emp, EmpAttr.Name, EmpAttr.No, "可访问的人员");

                // map.AttrsOfOneVSM.Add(new TemStations(), new Stations(), TemStationAttr.FK_STem, TemStationAttr.FK_Station, DeptAttr.Name, DeptAttr.No, "可访问的岗位");
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
    /// <summary>
    /// 单点登陆系统s
    /// </summary>
    public class STems : EntitiesNoName
    {
        #region 构造
        /// <summary>
        /// 单点登陆系统s
        /// </summary>
        public STems()
        {
        }
        /// <summary>
        /// 得到它的 Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new STem();
            }
        }
        #endregion
    }
}
