using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.GPM
{
    /// <summary>
    /// 消息提示
    /// </summary>
    public class PerAlertAttr : EntityNoNameAttr
    {
        /// <summary>
        /// 顺序
        /// </summary>
        public const string Idx = "Idx";
        /// <summary>
        /// 图标
        /// </summary>
        public const string ICON = "ICON";
        /// <summary>
        /// Url
        /// </summary>
        public const string Url = "Url";
        /// <summary>
        /// 获取的sql
        /// </summary>
        public const string GetSQL = "GetSQL";
    }
    /// <summary>
    /// 消息提示
    /// </summary>
    public class PerAlert : EntityNoName
    {
        #region 属性
        /// <summary>
        /// Idx
        /// </summary>
        public int Idx
        {
            get
            {
                return this.GetValIntByKey(PerAlertAttr.Idx);
            }
            set
            {
                this.SetValByKey(PerAlertAttr.Idx, value);
            }
        }
        /// <summary>
        /// GetSQL
        /// </summary>
        public string GetSQL
        {
            get
            {
                string sql = this.GetValStringByKey(PerAlertAttr.GetSQL);
                sql = sql.Replace("@WebUser.No", "'"+Web.WebUser.No+"'");
                return sql;
            }
            set
            {
                this.SetValByKey(PerAlertAttr.GetSQL, value);
            }
        }
        public string WebPath
        {
            get
            {
                return this.GetValStringByKey("WebPath");
            }
        }
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
        public string Url
        {
            get
            {
                return this.GetValStringByKey(PerAlertAttr.Url);
            }
            set
            {
                this.SetValByKey(PerAlertAttr.Url, value);
            }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 消息提示
        /// </summary>
        public PerAlert()
        {
        }
        /// <summary>
        /// 消息提示
        /// </summary>
        /// <param name="mypk"></param>
        public PerAlert(string no)
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
                Map map = new Map("GPM_PerAlert");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "系统";
                map.EnType = EnType.Sys;

                map.AddTBStringPK(PerAlertAttr.No, null, "编号", true, false, 2, 30, 20);
                map.AddTBString(PerAlertAttr.Name, null, "名称", true, false, 0, 3900, 20);
                map.AddTBString(PerAlertAttr.Url, null, "连接", true, false, 0, 3900, 20, true);
                map.AddDDLSysEnum(BarAttr.OpenWay, 0, "打开方式", true, true,
                BarAttr.OpenWay, "@0=新窗口@1=本窗口@2=覆盖新窗口");
                map.AddTBInt(PerAlertAttr.Idx, 0, "显示顺序", true, false);

                map.AddTBStringDoc(PerAlertAttr.GetSQL, null, "获取待处理数量的SQL", true, false, true);
                //map.AddTBString(PerAlertAttr.ICON, null, "ICON", true, false, 0, 3900, 20);

                map.AddMyFile("图标");
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
    /// <summary>
    /// 消息提示s
    /// </summary>
    public class PerAlerts : EntitiesNoName
    {
        #region 构造
        /// <summary>
        /// 消息提示s
        /// </summary>
        public PerAlerts()
        {
        }
        /// <summary>
        /// 得到它的 Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new PerAlert();
            }
        }
        #endregion
    }
}
