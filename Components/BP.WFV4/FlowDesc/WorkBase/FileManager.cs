using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.WF
{
    /// <summary>
    /// 流程附件
    /// </summary>
    public class FileManagerAttr : EntityNoNameAttr
    {
        public const string FK_Node = "FK_Node";
        public const string Desc = "Desc";
        public const string FK_Flow = "FK_Flow";
        public const string FileSize = "FileSize";
        public const string Ext = "Ext";
        public const string FK_Emp = "FK_Emp";
        public const string RDT = "RDT";
        public const string WorkID = "WorkID";
        public const string FK_Dept = "FK_Dept";
        public const string FID = "FID";
        public const string Note = "Note";
    }
	/// <summary>
	/// 流程附件
	/// </summary>
	public class FileManager :EntityOIDName
	{
        public Int64 WorkID
        {
            get
            {
                return this.GetValInt64ByKey(FileManagerAttr.WorkID);
            }
            set
            {
                this.SetValByKey(FileManagerAttr.WorkID, value);
            }
        }
        public Int64 FID
        {
            get
            {
                return this.GetValInt64ByKey(FileManagerAttr.FID);
            }
            set
            {
                this.SetValByKey(FileManagerAttr.FID, value);
            }
        }
        public string Note
        {
            get
            {
                return this.GetValStringByKey(FileManagerAttr.Note);
            }
            set
            {
                this.SetValByKey(FileManagerAttr.Note, value);
            }
        }
        public string FK_Node
        {
            get
            {
                return this.GetValStringByKey(FileManagerAttr.FK_Node);
            }
            set
            {
                this.SetValByKey(FileManagerAttr.FK_Node, value);
            }
        }
        public string FK_NodeText
        {
            get
            {
                return this.GetValRefTextByKey(FileManagerAttr.FK_Node);
            }
        }
        public string FK_EmpText
        {
            get
            {
                return this.GetValRefTextByKey(FileManagerAttr.FK_Emp);
            }
        }
        public string FK_Dept
        {
            get
            {
                return this.GetValStringByKey(FileManagerAttr.FK_Dept);
            }
            set
            {
                this.SetValByKey(FileManagerAttr.FK_Dept, value);
            }
        }
        public string FK_DeptT
        {
            get
            {
                return this.GetValRefTextByKey(FileManagerAttr.FK_Dept);
            }
        }
        public string RDT
        {
            get
            {
                return this.GetValStringByKey(FileManagerAttr.RDT);
            }
            set
            {
                this.SetValByKey(FileManagerAttr.RDT, value);
            }
        }
        public string Ext
        {
            get
            {
                return this.GetValStringByKey(FileManagerAttr.Ext);
            }
            set
            {
                this.SetValByKey(FileManagerAttr.Ext, value);
            }
        }
        public string FK_Emp
        {
            get
            {
                return this.GetValStringByKey(FileManagerAttr.FK_Emp);
            }
            set
            {
                this.SetValByKey(FileManagerAttr.FK_Emp, value);
            }
        }
        public string Desc
        {
            get
            {
                return this.GetValStringByKey(FileManagerAttr.Desc);
            }
            set
            {
                this.SetValByKey(FileManagerAttr.Desc, value);
            }
        }
        public string FK_Flow_del
        {
            get
            {
                return this.GetValStringByKey(FileManagerAttr.FK_Flow);
            }
            set
            {
                this.SetValByKey(FileManagerAttr.FK_Flow, value);
            }
        }
        public string FK_FlowText_del
        {
            get
            {
                return this.GetValRefTextByKey(FileManagerAttr.FK_Flow);
            }
        }
        public int FileSize
        {
            get
            {
                return this.GetValIntByKey(FileManagerAttr.FileSize);
            }
            set
            {
                this.SetValByKey(FileManagerAttr.FileSize, value/1024);
            }
        }
        
		#region 实现基本的方方法	
        
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map("WF_FileManager");
                map.EnDesc = "流程附件";
                map.EnType = EnType.App;
                map.DepositaryOfEntity = Depositary.None;

                map.AddTBIntPKOID();

                map.AddTBInt(FileManagerAttr.WorkID, 0, "WorkID", false, false);
                map.AddTBInt(FileManagerAttr.FID, 0, "FID", false, false);

               // map.AddDDLEntities(FileManagerAttr.FK_Flow, null, "流程", new Flows(), false);
                map.AddDDLEntities(FileManagerAttr.FK_Node, null, "节点", new NodeExts(), false);
                map.AddTBString(FileManagerAttr.Name, null, "名称", true, false, 1, 100, 100);
                map.AddTBString(FileManagerAttr.Ext, null, "扩展", true, false, 0, 100, 100);

                map.AddTBString(FileManagerAttr.Note, null, "备注", true, false, 0, 800, 100);

                map.AddTBInt(FileManagerAttr.FileSize, 10, "大小", true, false);
                map.AddDDLEntities(FileManagerAttr.FK_Emp, null, "上传人", new Port.Emps(), false);

                map.AddDDLEntities(FileManagerAttr.FK_Dept, null, "部门", new BP.Port.Depts(), false);
                map.AddTBDate(FileManagerAttr.RDT, "RDT", true, true);

                this._enMap = map;
                return this._enMap;
            }
        }
		#endregion 

        public string DoDD(string SPR, string FK_Flow)
        {
            return SPR + "调动成功。" + FK_Flow;
        }

		#region 构造方法
		/// <summary>
		/// 流程附件
		/// </summary> 
		public FileManager()
        {
        }
        public FileManager(int oid)
            : base(oid)
        {
        }
		#endregion 
	}

    public class FileManagers : EntitiesNoName
	{
		/// <summary>
		/// 流程附件
		/// </summary>
		public FileManagers(){}
		/// <summary>
		/// 得到它的 流程附件 
		/// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new FileManager();
            }
        }
        /// <summary>
        /// 获取个数
        /// </summary>
        /// <param name="workid"></param>
        /// <param name="fid"></param>
        /// <param name="open"></param>
        /// <returns></returns>
        public static int NumOfFile(Int64 workid, Int64 fid, FJOpen open)
        {
            string sql = null;
            switch (open)
            {
                case FJOpen.ForEmp:
                    sql = "SELECT COUNT(OID) FROM WF_FileManager WHERE WorkID=" + workid.ToString() + " AND FK_Emp='" + Web.WebUser.No + "'";
                    break;
                case FJOpen.ForFID:
                    sql = "SELECT COUNT(OID) FROM WF_FileManager WHERE FID=" + fid;
                    break;
                case FJOpen.ForWorkID:
                    sql = "SELECT COUNT(OID) FROM WF_FileManager WHERE WorkID=" + workid.ToString();
                    break;
                default:
                    break;
            }
            return DBAccess.RunSQLReturnValInt(sql);
        }
	}
}
