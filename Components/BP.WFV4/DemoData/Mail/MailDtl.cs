using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Web;
using BP.Port;

namespace BP.TA
{
	public enum MailDtlState
	{
		/// <summary>
		/// 未阅读
		/// </summary>
		UnRead,
		/// <summary>
		/// 已阅读
		/// </summary>
		Read,
		/// <summary>
		/// 已回复
		/// </summary>
		Re,		 
		/// <summary>
		/// 已删除
		/// </summary>
		Delete
	}
	/// <summary>
	/// 邮件属性
	/// </summary>
	public class MailDtlAttr:EntityOIDNameAttr
	{
		/// <summary>
		/// 单位
		/// </summary>
		public const string Title="Title";
		/// <summary>
		/// 发送人的iD
		/// </summary>
		public const string RefMailID="RefMailID";
		/// <summary>
		/// 内容
		/// </summary>
		public const string Doc="Doc";
		/// <summary>
		/// 
		/// </summary>
		public const string MailDtlState="MailDtlState";
		/// <summary>
		/// 附件个数
		/// </summary>
		public const string Attachments="Attachments";
		/// <summary>
		/// 接爱人
		/// </summary>
		public const string Accpter="Accpter";
		/// <summary>
		/// 发送人
		/// </summary>
		public const string Sender="Sender";
		/// <summary>
		/// 记录时间
		/// </summary>
		public const string RDT="RDT";
		/// <summary>
		/// 阅读时间
		/// </summary>
		public const string ReadDateTime="ReadDateTime";
		/// <summary>
		/// PIR
		/// </summary>
		public const string PRI="PRI";

	}
	/// <summary>
	/// 邮件
	/// </summary> 
	public class MailDtl : EntityOID
	{
		#region 基本属性
		/// <summary>
		/// 相关联的mail id .
		/// </summary>
		public int RefMailID
		{
			get
			{
				return this.GetValIntByKey(MailDtlAttr.RefMailID); 
			}
			set
			{
				SetValByKey(MailDtlAttr.RefMailID,value);
			}
		}
		/// <summary>
		/// RDT
		/// </summary>
		public string RDT 
		{
			get
			{
				return this.GetValStringByKey(MailDtlAttr.RDT);
			}
			set
			{
				SetValByKey(MailDtlAttr.RDT,value);
			}
		}
		public string Sender
		{
			get
			{
				return this.GetValStringByKey(MailDtlAttr.Sender); 
			}
			set
			{
				SetValByKey(MailDtlAttr.Sender,value);
			}
		}
		public string SenderText
		{
			get
			{
				return this.GetValRefTextByKey(MailDtlAttr.Sender); 
			}
		}
		public string Accpter
		{
			get
			{
				return this.GetValStringByKey(MailDtlAttr.Accpter); 
			}
			set
			{
				SetValByKey(MailDtlAttr.Accpter,value);
			}
		}
		public string Title
		{
			get
			{
				return this.GetValStringByKey(MailDtlAttr.Title); 
			}
			set
			{
				SetValByKey(MailDtlAttr.Title,value);
			}
		}
		public int PRI
		{
			get
			{
				return this.GetValIntByKey(MailDtlAttr.PRI); 
			}
			set
			{
				SetValByKey(MailDtlAttr.PRI,value);
			}
		}
		public MailDtlState MailDtlState
		{
			get
			{
				return (MailDtlState)this.GetValIntByKey(MailDtlAttr.MailDtlState); 
			}
			set
			{
				SetValByKey(MailDtlAttr.MailDtlState,(int)MailDtlState  );
			}
		}
		
		public string Doc
		{
			get
			{
				return this.GetValStringByKey(MailDtlAttr.Doc); 
			}
			set
			{
				SetValByKey(MailDtlAttr.Doc,value);
			}
		}
		/// <summary>
		/// 附件个数
		/// </summary>
		public int Attachments
		{
			get
			{
				return this.GetValIntByKey(MailDtlAttr.Attachments); 
			}
			set
			{
				SetValByKey(MailDtlAttr.Attachments,value);
			}
		}
		#endregion
 
		#region 构造函数
		public override UAC HisUAC
		{
			get
			{
				UAC uac = new UAC();
				uac.OpenAll();
				return uac;
			}
		}
		/// <summary>
		/// 邮件
		/// </summary>
		public MailDtl()
		{
		  
		}
		/// <summary>
		/// 邮件
		/// </summary>
		/// <param name="_No">No</param>
		public MailDtl(int oid):base(oid)
		{
		}
		/// <summary>
		/// Map
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) 
					return this._enMap;

				Map map = new Map("TA_MailDtl");
				map.EnDesc="邮件";
				map.Icon="../TA/Images/MailDtl_s.ico";
				map.AddTBIntPKOID();
				map.AddTBInt(MailDtlAttr.RefMailID,0,"refMailID",false,false);
				map.AddTBStringDoc(MailDtlAttr.Title,null,"标题",true,false);
				map.AddTBStringDoc(MailDtlAttr.Doc,null,"内容",true,false);

				//map.AddTBStringDoc(MailDtlAttr.Sender,null,"接受人",true,false);
				//map.AddTBStringDoc(MailDtlAttr.Accpter,null,"发送人",true,false);

				map.AddDDLEntities(MailDtlAttr.Sender,null,"接受人",new Emps(),false);
				map.AddDDLEntities(MailDtlAttr.Accpter,null,"发送人",new Emps(),false);

				map.AddTBDateTime(MailDtlAttr.RDT,null,"记录时间",true,false);
				map.AddTBDateTime(MailDtlAttr.ReadDateTime,null,"阅读时间",true,false);
				map.AddDDLSysEnum(MailDtlAttr.PRI,0,"紧急程度",false,true);
                map.AddDDLSysEnum(MailDtlAttr.MailDtlState, 0, "状态", false, true, "MailDtlState", "@0=未阅读@1=已阅读@2=已回复@3=已删除");
				map.AddTBInt(MailDtlAttr.Attachments,0,"附件个数",false,false);

				map.AddSearchAttr(MailDtlAttr.MailDtlState);
				map.AddSearchAttr(MailDtlAttr.Accpter);
				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 

		protected override bool beforeInsert()
		{
			this.RDT=DataType.CurrentDataTimeCNOfShort;
			return base.beforeInsert ();
		}
	}
	/// <summary>
	/// 邮件s
	/// </summary> 
	public class MailDtls: Entities
	{
		
		/// <summary>
		/// 获取entity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new MailDtl();
			}
		}
		/// <summary>
		/// MailDtls
		/// </summary>
		public MailDtls()
		{
		}
		/// <summary>
		/// MailDtls
		/// </summary>
		public MailDtls(string Rec)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(MailDtlAttr.Accpter, Rec);
			qo.DoQuery();			
		}
		public int SearchSend(string sender)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(MailDtlAttr.Sender, sender);
			
			return qo.DoQuery();
		}
		public int Search(string accepter, int MailDtlstate)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(MailDtlAttr.Accpter, accepter);
			qo.addAnd();
			qo.AddWhere(MailDtlAttr.MailDtlState, MailDtlstate);
			return qo.DoQuery();
		}
		public int Search(string accepter, int MailDtlstate1, int MailDtlstate2 )
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(MailDtlAttr.Accpter, accepter);
			qo.addAnd();
			qo.addLeftBracket();
			qo.AddWhere(MailDtlAttr.MailDtlState, MailDtlstate1);
			qo.addOr();
			qo.AddWhere(MailDtlAttr.MailDtlState, MailDtlstate2);
			qo.addRightBracket();
			return qo.DoQuery();
		}
		public int Search(string accepter, int MailDtlstate1, int MailDtlstate2 , int MailDtlstate3 )
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(MailDtlAttr.Accpter, accepter);
			qo.addAnd();
			qo.addLeftBracket();
			qo.AddWhere(MailDtlAttr.MailDtlState, MailDtlstate1);
			qo.addOr();
			qo.AddWhere(MailDtlAttr.MailDtlState, MailDtlstate2);
			qo.addOr();
			qo.AddWhere(MailDtlAttr.MailDtlState, MailDtlstate3);
			qo.addRightBracket();
			return qo.DoQuery();
		}
	}
}
 