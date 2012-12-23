using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.WF;

namespace BP.WF
{
    public enum BillFileType
    {
        Word,
        PDF,
        Excel
    }
    public class BillTemplateAttr:BP.En.EntityNoNameAttr
    {
        public const string Url = "Url";
        /// <summary>
        /// NodeID
        /// </summary>
        public const string NodeID = "NodeID";
        /// <summary>
        /// 是否需要送达
        /// </summary>
        public const string IsNeedSend = "IsNeedSend";
        /// <summary>
        /// 为生成单据使用
        /// </summary>
        public const string IDX = "IDX";
        /// <summary>
        /// 要排除的字段
        /// </summary>
        public const string ExpField = "ExpField";
        /// <summary>
        /// 要替换的值
        /// </summary>
        public const string ReplaceVal = "ReplaceVal";
        /// <summary>
        /// 单据类型
        /// </summary>
        public const string FK_BillType = "FK_BillType";
        /// <summary>
        /// 是否生成PDF
        /// </summary>
        public const string BillFileType = "BillFileType";
    }
	/// <summary>
	/// 单据模板
	/// </summary>
	public class BillTemplate : EntityNoName
    {
        #region  属性
        public string FK_BillType
        {
            get
            {
                return this.GetValStringByKey(BillTemplateAttr.FK_BillType);
            }
            set
            {
                this.SetValByKey(BillTemplateAttr.FK_BillType, value);
            }
        }
        /// <summary>
        /// 要替换的值
        /// </summary>
        public string ReplaceVal
        {
            get
            {
                return this.GetValStringByKey(BillTemplateAttr.ReplaceVal);
            }
            set
            {
                this.SetValByKey(BillTemplateAttr.ReplaceVal, value);
            }
        }
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                uac.OpenForSysAdmin();
                return uac;
            }
        }
        public new string No
        {
            get
            {
                string no = this.GetValStrByKey("No");
                no = no.Replace("\n", "");
                no = no.Replace(" ", "");
                return no;
            }
            set
            {
                this.SetValByKey("No", value);
                this.SetValByKey(BillTemplateAttr.Url, value);
            }
        }
        public BillFileType HisBillFileType
        {
            get
            {
                return (BillFileType)this.GetValIntByKey(BillTemplateAttr.BillFileType);
            }
            set
            {
                this.SetValByKey(BillTemplateAttr.BillFileType, (int)value);
            }
        }
        public int BillFileType
        {
            get
            {
                return this.GetValIntByKey(BillTemplateAttr.BillFileType);
            }
            set
            {
                this.SetValByKey(BillTemplateAttr.BillFileType, value);
            }
        }
        public string IDX
        {
            get
            {
                return this.GetValStrByKey(BillTemplateAttr.IDX);
            }
            set
            {
                this.SetValByKey(BillTemplateAttr.IDX, value);
            }
        }
        public string Url
        {
            get
            {
                string s= this.GetValStrByKey(BillTemplateAttr.Url);
                if (s == "" || s == null)
                    return this.No;
                return s;
            }
            set
            {
                this.SetValByKey(BillTemplateAttr.Url, value);
            }
        }
        public string NodeName
        {
            get
            {
                Node nd = new Node(this.NodeID);
                return nd.Name;
            }
        }
        public int NodeID
        {
            get
            {
                return this.GetValIntByKey(BillTemplateAttr.NodeID);
            }
            set
            {
                this.SetValByKey(BillTemplateAttr.NodeID, value);
            }
        }
        /// <summary>
        /// 是否需要送达
        /// </summary>
        public bool IsNeedSend_del
        {
            get
            {
                return this.GetValBooleanByKey(BillTemplateAttr.IsNeedSend); 
            }
        }
        #endregion

        #region 构造函数
        /// <summary>
		/// ss
		/// </summary>
		public BillTemplate(){}
        public BillTemplate(string no):base(no.Replace( "\n","" ).Trim() ) 
        {
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
                Map map = new Map("WF_BillTemplate");
                map.EnDesc = this.ToE("BillTemplate", "单据模板"); // "单据模板";
                map.EnType = EnType.Admin;
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.CodeStruct = "6";

                map.AddTBStringPK(BillTemplateAttr.No, null, "No", true, false, 1, 300, 6);
                map.AddTBString(BillTemplateAttr.Name, null, "Name", true, false, 0, 200, 20);
                map.AddTBString(BillTemplateAttr.Url, null, "URL", true, false, 0, 200, 20);

                map.AddTBInt(BillTemplateAttr.NodeID, 0, "NodeID", true, false);

                map.AddDDLSysEnum(BillTemplateAttr.BillFileType, 0, "生成的文件类型", true, false,
                    "BillFileType","@0=Word@1=PDF@2=Excel(未完成)@3=Html(未完成)");

                map.AddTBString(BillTemplateAttr.FK_BillType, null, "单据类型", true, false, 0, 4, 4);

                map.AddTBString("IDX", null, "IDX", false, false, 0, 200, 20);
                map.AddTBString(BillTemplateAttr.ExpField, null, "要排除的字段", false, false, 0, 800, 20);
                map.AddTBString(BillTemplateAttr.ReplaceVal, null, "要替换的值", false, false, 0, 3000, 20);
                this._enMap = map;
                return this._enMap;
            }
        }
		#endregion 
	}
	/// <summary>
	/// 节点的单据模板
	/// </summary>
	public class BillTemplates: EntitiesNoName
	{
		#region 构造
		/// <summary>
		/// 得到它的 Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new BillTemplate();
			}
		}
		/// <summary>
		/// 单据模板
		/// </summary>
        public BillTemplates()
        {
        }
        /// <summary>
        /// BillTemplates
        /// </summary>
        /// <param name="nd"></param>
        public BillTemplates(Node nd)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(BillTemplateAttr.NodeID, nd.NodeID);
            if (nd.IsStartNode)
            {
                qo.addOr();
                qo.AddWhere("No", "SLHZ");
            }
            qo.DoQuery();
        }
        public BillTemplates(string fk_flow)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhereInSQL(BillTemplateAttr.NodeID, "SELECT NodeID FROM WF_Node WHERE fk_flow='" + fk_flow + "'");
            qo.DoQuery();
        }
        public BillTemplates(int nd)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(BillTemplateAttr.NodeID, nd);
            qo.DoQuery();
        }
		#endregion
	}
	
}
