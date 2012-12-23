using System;
using System.Data.SqlClient;

namespace BP.DA
{
	/// <summary>
	///　连接到哪个库上．
	///  他们存放在 web.config 的列表内．
	/// </summary>
	public enum DBUrlType
	{ 
		/// <summary>
		/// 主要的应用程序
		/// </summary>
		AppCenterDSN,
		/// <summary>
		/// DBAccessOfOracle9i
		/// </summary>
		DBAccessOfOracle9i,
		/// <summary>
		/// DBAccessOfOracle9i1
		/// </summary>
		DBAccessOfOracle9i1,
		/// <summary>
		/// DBAccessOfMSSQL2000
		/// </summary>
		DBAccessOfMSSQL2000,
		/// <summary>
		/// access的连接．
		/// </summary>
		DBAccessOfOLE,
		/// <summary>
		/// DBAccessOfODBC
		/// </summary>
		DBAccessOfODBC
	}
	/// <summary>
	/// DBUrl 的摘要说明。
	/// </summary>
	public class DBUrl
	{
		/// <summary>
		/// 连接
		/// </summary>
		public DBUrl()
		{
		} 
		/// <summary>
		/// 连接
		/// </summary>
		/// <param name="type">连接type</param>
		public DBUrl(DBUrlType type)
		{
			this.DBUrlType=type;
		}
		/// <summary>
		/// 默认值
		/// </summary>
		public DBUrlType  _DBUrlType=DBUrlType.AppCenterDSN;
		/// <summary>
		/// 要连接的到的库。
		/// </summary>
		public DBUrlType DBUrlType
		{
			get
			{
				return _DBUrlType;
			}
			set
			{
				_DBUrlType=value;
			}
		}
        public string DBVarStr
        {
            get
            {
                switch (this.DBType)
                {
                    case DBType.Oracle9i:
                        return ":";
                    case DBType.MySQL:
                    case DBType.SQL2000:
                        return "@";
                    case DBType.Informix:
                        return "?";
                    default:
                        return "@";
                }
            }
        }
		/// <summary>
		/// 数据库类型
		/// </summary>
		public DBType DBType
		{
			get
			{
				switch(this.DBUrlType)
				{
					case DBUrlType.AppCenterDSN:
						return DBAccess.AppCenterDBType ; 
					case DBUrlType.DBAccessOfMSSQL2000:
						return DBType.SQL2000;
					case DBUrlType.DBAccessOfOLE:
						return DBType.Access;
					case DBUrlType.DBAccessOfOracle9i1:
                    case DBUrlType.DBAccessOfOracle9i:
						return DBType.Oracle9i ;
					default:
						throw new Exception("不明确的连接");
				}
			}
		}
	}
	
}
