using System;
using System.Collections;
using BP.DA;
using BP.En;
namespace BP.Sys
{
    /// <summary>
    /// 点对点
    /// </summary>
    public class MapM2MAttr : EntityNoNameAttr
    {
        /// <summary>
        /// 主表
        /// </summary>
        public const string FK_MapData = "FK_MapData";
        /// <summary>
        /// 插入表单的位置
        /// </summary>
        public const string RowIdx = "RowIdx";
        /// <summary>
        /// GroupID
        /// </summary>
        public const string GroupID = "GroupID";
        public const string Height = "Height";
        public const string Width = "Width";
        /// <summary>
        /// 是否可以自适应大小
        /// </summary>
        public const string IsAutoSize = "IsAutoSize";
        public const string DBOfObjs = "DBOfObjs";
        public const string DBOfGroups = "DBOfGroups";
        public const string IsDelete = "IsDelete";
        public const string IsInsert = "IsInsert";
    }
    /// <summary>
    /// 点对点
    /// </summary>
    public class MapM2M : EntityNoName
    {
        public GEEntity HisGEEntity
        {
            get
            {
                GEEntity en = new GEEntity(this.No);
                return en;
            }
        }

        #region 属性
        /// <summary>
        /// 是否自适应大小
        /// </summary>
        public bool IsAutoSize
        {
            get
            {
                return this.GetValBooleanByKey(MapM2MAttr.IsAutoSize);
            }
            set
            {
                this.SetValByKey(MapM2MAttr.IsAutoSize, value);
            }
        }
        public bool IsDelete
        {
            get
            {
                return this.GetValBooleanByKey(MapM2MAttr.IsDelete);
            }
            set
            {
                this.SetValByKey(MapM2MAttr.IsDelete, value);
            }
        }
        public bool IsInsert
        {
            get
            {
                return this.GetValBooleanByKey(MapM2MAttr.IsInsert);
            }
            set
            {
                this.SetValByKey(MapM2MAttr.IsInsert, value);
            }
        }
        public string DBOfObjs
        {
            get
            {
                return this.GetValStrByKey(MapM2MAttr.DBOfObjs);
            }
            set
            {
                this.SetValByKey(MapM2MAttr.DBOfObjs, value);
            }
        }
        public string DBOfGroups
        {
            get
            {
                return this.GetValStrByKey(MapM2MAttr.DBOfGroups);
            }
            set
            {
                this.SetValByKey(MapM2MAttr.DBOfGroups, value);
            }
        }
        public bool IsUse = false;
        public string FK_MapData
        {
            get
            {
                return this.GetValStrByKey(MapM2MAttr.FK_MapData);
            }
            set
            {
                this.SetValByKey(MapM2MAttr.FK_MapData, value);
            }
        }
        public int RowIdx
        {
            get
            {
                return this.GetValIntByKey(MapM2MAttr.RowIdx);
            }
            set
            {
                this.SetValByKey(MapM2MAttr.RowIdx, value);
            }
        }

        public int GroupID
        {
            get
            {
                return this.GetValIntByKey(MapM2MAttr.GroupID);
            }
            set
            {
                this.SetValByKey(MapM2MAttr.GroupID, value);
            }
        }
        public string Height
        {
            get
            {
                return this.GetValStringByKey(MapM2MAttr.Height);
            }
            set
            {
                this.SetValByKey(MapM2MAttr.Height, value);
            }
        }
        public string Width
        {
            get
            {
                return this.GetValStringByKey(MapM2MAttr.Width);
            }
            set
            {
                this.SetValByKey(MapM2MAttr.Width, value);
            }
        }
        public int FK_Node
        {
            get
            {
                return int.Parse(this.FK_MapData.Replace("ND", ""));
            }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 点对点
        /// </summary>
        public MapM2M()
        {
        }
        /// <summary>
        /// 点对点
        /// </summary>
        /// <param name="no"></param>
        public MapM2M(string no)
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
                Map map = new Map("Sys_MapM2M");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "点对点";
                map.EnType = EnType.Sys;

                map.AddTBStringPK(MapM2MAttr.No, null, "编号", true, false, 1, 20, 20);
                map.AddTBString(MapM2MAttr.Name, null, "描述", true, false, 1, 200, 20);
                map.AddTBString(MapM2MAttr.FK_MapData, null, "主表", true, false, 0, 30, 20);

                map.AddTBString(MapM2MAttr.DBOfObjs, null, "DBOfObjs", true, false, 0, 4000, 20);
                map.AddTBString(MapM2MAttr.DBOfGroups, null, "DBOfGroups", true, false, 0, 4000, 20);


                map.AddTBString(MapM2MAttr.Height, "100%", "Height", true, false, 0, 200, 20);
                map.AddTBString(MapM2MAttr.Width, "100%", "Width", true, false, 0, 200, 20);

                map.AddBoolean(MapM2MAttr.IsAutoSize, true, "是否自动设置大小", false, false);

                map.AddTBInt(MapM2MAttr.RowIdx, 99, "位置", false, false);
                map.AddTBInt(MapM2MAttr.GroupID, 0, "分组ID", false, false);

                map.AddBoolean(MapM2MAttr.IsDelete, true, "可删除否", false, false);
                map.AddBoolean(MapM2MAttr.IsInsert, true, "可插入否", false, false);

                this._enMap = map;
                return this._enMap;
            }
        }
        protected override bool beforeInsert()
        {
            return base.beforeInsert();
        }
        protected override void afterInsert()
        {
            base.afterInsert();
        }
        protected override void afterDelete()
        {
            base.afterDelete();
        }
        #endregion
    }
    /// <summary>
    /// 点对点s
    /// </summary>
    public class MapM2Ms : EntitiesNoName
    {
        #region 构造
        /// <summary>
        /// 点对点s
        /// </summary>
        public MapM2Ms()
        {
        }
        /// <summary>
        /// 点对点s
        /// </summary>
        /// <param name="fk_mapdata">s</param>
        public MapM2Ms(string fk_mapdata)
        {
            this.Retrieve(MapM2MAttr.FK_MapData, fk_mapdata, MapM2MAttr.GroupID);
        }
        /// <summary>
        /// 得到它的 Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new MapM2M();
            }
        }
        #endregion
    }
}
