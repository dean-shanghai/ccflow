using System;
using BP.En;
using BP.DA;
//using BP.En.MBase;
namespace BP.En
{
    public class SqlBuilder
    {
        #region 关于IEntitiy的操作
        //		public static String GetKeyCondition(IEntity en)
        //		{ 
        //			return null;
        //		}
        //		public static String RetrieveAll(IEntity en)
        //		{ 
        //			return null;
        //		}
        //		public static String Retrieve(IEntity en)
        //		{ 
        //			return null;
        //		}
        //		public static String Insert(IEntity en)
        //		{ 
        //			return null;
        //		}
        //		public static String Delete(IEntity en)
        //		{ 
        //			return null;
        //		}
        //		public static String Update(IEntity en)
        //		{ 
        //			return null;
        //		}

        #endregion

        #region GetKeyCondition

        /// <summary>
        /// 得到主健
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public static String GetKeyConditionOfMS(Entity en)
        {
            // 判断特殊情况。
            if (en.EnMap.Attrs.Contains("OID"))
            {
                int key = en.GetValIntByKey("OID");
                if (key == 0)
                    throw new Exception("@在执行[" + en.EnMap.EnDesc + " " + en.EnMap.PhysicsTable + "]，没有给PK OID 赋值,不能进行查询操作。");
                if (en.PKs.Length == 1)
                    return " OID=" + key;
            }

            string sql = " (1=1) ";
            foreach (Attr attr in en.EnMap.Attrs)
            {
                if (attr.MyFieldType == FieldType.PK || attr.MyFieldType == FieldType.PKFK || attr.MyFieldType == FieldType.PKEnum)
                {
                    if (attr.MyDataType == DataType.AppString)
                    {
                        string val = en.GetValByKey(attr.Key).ToString();
                        if (val == null || val == "")
                            throw new Exception("@在执行[" + en.EnMap.EnDesc + " " + en.EnMap.PhysicsTable + "]没有给PK " + attr.Key + " 赋值,不能进行查询操作。");
                        sql = sql + " AND " + attr.Field + "=@" + attr.Key;
                        continue;
                    }
                    if (attr.MyDataType == DataType.AppInt)
                    {
                        if (en.GetValIntByKey(attr.Key) == 0 && attr.Key == "OID")
                            throw new Exception("@在执行[" + en.EnMap.EnDesc + " " + en.EnMap.PhysicsTable + "]，没有给PK " + attr.Key + " 赋值,不能进行查询操作。");
                        sql = sql + " AND " + attr.Field + "=@" + attr.Key;
                        continue;
                    }
                }
            }
            return sql;
        }
        /// <summary>
        /// 得到主健
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public static String GetKeyConditionOfOLE(Entity en)
        {
            // 判断特殊情况。
            if (en.EnMap.Attrs.Contains("OID"))
            {
                int key = en.GetValIntByKey("OID");
                if (key == 0)
                    throw new Exception("@在执行[" + en.EnMap.EnDesc + " " + en.EnMap.PhysicsTable + "]，没有给PK OID 赋值,不能进行查询操作。");

                if (en.PKs.Length == 1)
                    return en.EnMap.PhysicsTable + ".OID=" + key;
            }
            //			if (en.EnMap.Attrs.Contains("MID"))
            //			{
            //				int key=en.GetValIntByKey("MID");
            //				if (key==0)
            //					throw new Exception("@在执行["+en.EnMap.EnDesc+ " " +en.EnMap.PhysicsTable +"]，没有给PK MID 赋值,不能进行查询操作。");
            //				return en.EnMap.PhysicsTable+".MID="+key ;
            //			}

            string sql = " (1=1) ";
            foreach (Attr attr in en.EnMap.Attrs)
            {
                if (attr.MyFieldType == FieldType.PK || attr.MyFieldType == FieldType.PKFK || attr.MyFieldType == FieldType.PKEnum)
                {
                    if (attr.MyDataType == DataType.AppString)
                    {
                        string val = en.GetValByKey(attr.Key).ToString();
                        if (val == null || val == "")
                            throw new Exception("@在执行[" + en.EnMap.EnDesc + " " + en.EnMap.PhysicsTable + "]没有给PK " + attr.Key + " 赋值,不能进行查询操作。");
                        sql = sql + " AND " + en.EnMap.PhysicsTable + ".[" + attr.Field + "]='" + en.GetValByKey(attr.Key).ToString() + "'";
                        continue;
                    }

                    if (attr.MyDataType == DataType.AppInt)
                    {
                        if (en.GetValIntByKey(attr.Key) == 0 && attr.Key == "OID")
                            throw new Exception("@在执行[" + en.EnMap.EnDesc + " " + en.EnMap.PhysicsTable + "]，没有给PK " + attr.Key + " 赋值,不能进行查询操作。");
                        sql = sql + " AND " + en.EnMap.PhysicsTable + ".[" + attr.Field + "]=" + en.GetValStringByKey(attr.Key);
                        continue;
                    }
                }
            }
            return sql;
        }
        /// <summary>
        /// 得到主健
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public static String GenerWhereByPK(Entity en, string dbStr)
        {
            if (en.PKCount == 1)
                return en.EnMap.PhysicsTable + "." + en.PKField + "=" + dbStr + en.PK;

            string sql = " (1=1) ";
            foreach (Attr attr in en.EnMap.Attrs)
            {
                if (attr.MyFieldType == FieldType.PK || attr.MyFieldType == FieldType.PKFK || attr.MyFieldType == FieldType.PKEnum)
                    sql = sql + " AND " + attr.Field + "=" + dbStr + attr.Field;
            }
            return sql;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public static Paras GenerParasPK(Entity en)
        {
            Paras paras = new Paras();
            string pk = en.PK;
            if (pk == "OID")
            {
                paras.Add("OID", en.GetValIntByKey("OID"));
                return paras;
            }

            if (pk == "No")
            {
                paras.Add("No", en.GetValStrByKey("No"));
                return paras;
            }

            if (pk == "MyPK")
            {
                paras.Add("MyPK", en.GetValStrByKey("MyPK"));
                return paras;
            }


            foreach (Attr attr in en.EnMap.Attrs)
            {
                if (attr.MyFieldType == FieldType.PK || attr.MyFieldType == FieldType.PKFK || attr.MyFieldType == FieldType.PKEnum)
                {
                    if (attr.MyDataType == DataType.AppString)
                    {
                        paras.Add(attr.Key, en.GetValByKey(attr.Key).ToString());
                        continue;
                    }
                    if (attr.MyDataType == DataType.AppInt)
                    {
                        paras.Add(attr.Key, en.GetValIntByKey(attr.Key));
                        continue;
                    }
                }
            }
            return paras;
        }

        public static String GetKeyConditionOfOraForPara(Entity en)
        {
            // 不能删除物理表名称，会引起未定义列。

            // 判断特殊情况, 
            switch (en.PK)
            {
                case "OID":
                    return en.EnMap.PhysicsTable + ".OID=" + en.HisDBVarStr + "OID";
                case "No":
                    return en.EnMap.PhysicsTable + ".No=" + en.HisDBVarStr + "No";
                case "MyPK":
                    return en.EnMap.PhysicsTable + ".MyPK=" + en.HisDBVarStr + "MyPK";
                default:
                    break;
            }

            string sql = " (1=1) ";
            foreach (Attr attr in en.EnMap.Attrs)
            {
                if (attr.MyFieldType == FieldType.PK || attr.MyFieldType == FieldType.PKFK || attr.MyFieldType == FieldType.PKEnum)
                {
                    if (attr.MyDataType == DataType.AppString)
                    {
                        sql = sql + " AND " + en.EnMap.PhysicsTable + "." + attr.Field + "=" + en.HisDBVarStr + attr.Key;
                        continue;
                    }
                    if (attr.MyDataType == DataType.AppInt)
                    {
                        sql = sql + " AND " + en.EnMap.PhysicsTable + "." + attr.Field + "=" + en.HisDBVarStr + attr.Key;
                        continue;
                    }
                }
            }
            return sql.Substring(" (1=1)  AND ".Length);
        }
        #endregion

        /// <summary>
        /// 查询全部信息
        /// </summary>
        /// <param name="en">实体</param>
        /// <returns>sql</returns>
        public static string RetrieveAll(Entity en)
        {
            return SqlBuilder.SelectSQL(en, SystemConfig.TopNum);
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="en">实体</param>
        /// <returns>string</returns>
        public static string Retrieve(Entity en)
        {
            string sql = "";
            switch (en.EnMap.EnDBUrl.DBType)
            {
                case DBType.SQL2000:
                    sql = SqlBuilder.SelectSQLOfMS(en, 1) + "   AND ( " + SqlBuilder.GenerWhereByPK(en, "@") + " )";
                    break;
                case DBType.Access:
                   // sql = SqlBuilder.SelectSQLOfOLE(en, 1) + "  AND ( " + SqlBuilder.GetKeyConditionOfOLE(en,"@") + " )";
                    sql = SqlBuilder.SelectSQLOfOLE(en, 1) + "  AND ( " + SqlBuilder.GenerWhereByPK(en, "@") + " )";
                    break;
                case DBType.Oracle9i:
                    sql = SqlBuilder.SelectSQLOfOra(en, 1) + "  AND ( " + SqlBuilder.GenerWhereByPK(en, ":") + " )";
                    break;
                case DBType.DB2:
                    throw new Exception("还没有实现。");
                default:
                    throw new Exception("还没有实现。");
            }
            sql = sql.Replace("WHERE   AND", " WHERE ");
            sql = sql.Replace("WHERE  AND", " WHERE ");
            sql = sql.Replace("WHERE AND", " WHERE ");
            return sql;
        }
        public static string RetrieveForPara(Entity en)
        {
            string sql = null;
            switch (en.EnMap.EnDBUrl.DBType)
            {
                case DBType.SQL2000:
                    sql = SqlBuilder.SelectSQLOfMS(en, 1) + " AND " + SqlBuilder.GenerWhereByPK(en, "@");
                    break;
                case DBType.Access:
                    sql = SqlBuilder.SelectSQLOfOLE(en, 1) + " AND "+ SqlBuilder.GenerWhereByPK(en, "@");
                    break;
                case DBType.Oracle9i:
                    sql = SqlBuilder.SelectSQLOfOra(en, 1) + "AND (" + SqlBuilder.GenerWhereByPK(en, ":") + " )";
                    break;
                case DBType.DB2:
                    throw new Exception("还没有实现。");
                default:
                    throw new Exception("还没有实现。");
            }
            sql = sql.Replace("WHERE  AND", "WHERE");
            sql = sql.Replace("WHERE AND", "WHERE");
            return sql;
        }
        public static string RetrieveForPara_bak(Entity en)
        {
            switch (en.EnMap.EnDBUrl.DBType)
            {
                case DBType.SQL2000:
                case DBType.Access:

                    if (en.EnMap.HisFKAttrs.Count == 0)
                        return SqlBuilder.SelectSQLOfMS(en, 1) + SqlBuilder.GetKeyConditionOfOraForPara(en);
                    else
                        return SqlBuilder.SelectSQLOfMS(en, 1) + "  AND ( " + SqlBuilder.GetKeyConditionOfOraForPara(en) + " )";
                    return SqlBuilder.SelectSQLOfMS(en, 1) + "  AND ( " + SqlBuilder.GetKeyConditionOfMS(en) + " )";
                //case DBType.Access:
                //    return SqlBuilder.SelectSQLOfOLE(en, 1) + "  AND ( " + SqlBuilder.GetKeyConditionOfOLE(en) + " )";
                case DBType.Oracle9i:
                    if (en.EnMap.HisFKAttrs.Count == 0)
                        return SqlBuilder.SelectSQLOfOra(en, 1) + SqlBuilder.GetKeyConditionOfOraForPara(en);
                    else
                        return SqlBuilder.SelectSQLOfOra(en, 1) + "  AND ( " + SqlBuilder.GetKeyConditionOfOraForPara(en) + " )";
                case DBType.DB2:
                    throw new Exception("还没有实现。");
                default:
                    throw new Exception("还没有实现。");
            }
        }
        /// <summary>
        /// 产生Ora 的where.
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public static string GenerFormWhereOfOra_For9i(Entity en)
        {
            string from = " FROM " + en.EnMap.PhysicsTable;
            //	string where="  ";
            string table = "";
            string tableAttr = "";
            string enTable = en.EnMap.PhysicsTable;
            foreach (Attr attr in en.EnMap.Attrs)
            {
                if (attr.MyFieldType == FieldType.Normal || attr.MyFieldType == FieldType.PK || attr.MyFieldType == FieldType.RefText)
                    continue;

                if (attr.MyFieldType == FieldType.FK || attr.MyFieldType == FieldType.PKFK)
                {
                    Entity en1 = attr.HisFKEn; ;
                    table = en1.EnMap.PhysicsTable;
                    tableAttr = "" + en1.EnMap.PhysicsTable + "_" + attr.Key + "";
                    from = from + " LEFT OUTER JOIN " + table + "   " + tableAttr + " ON " + enTable + "." + attr.Field + "=" + tableAttr + "." + en1.EnMap.GetFieldByKey(attr.UIRefKeyValue);
                    //where=where+" AND "+" ("+en.EnMap.PhysicsTable+"."+attr.Field+"="+en1.EnMap.PhysicsTable+"_"+attr.Key+"."+en1.EnMap.Attrs.GetFieldByKey(attr.UIRefKeyValue )+" ) "  ;
                    continue;
                }
                if (attr.MyFieldType == FieldType.Enum || attr.MyFieldType == FieldType.PKEnum)
                {
                    //from= from+ " LEFT OUTER JOIN "+table+" AS "+tableAttr+ " ON "+enTable+"."+attr.Field+"="+tableAttr+"."+en1.EnMap.Attrs.GetFieldByKey( attr.UIRefKeyValue );
                    tableAttr = "Enum_" + attr.Key;
                    from = from + " LEFT OUTER JOIN ( SELECT Lab, IntKey FROM Sys_Enum WHERE EnumKey='" + attr.UIBindKey + "' )  Enum_" + attr.Key + " ON " + enTable + "." + attr.Field + "=" + tableAttr + ".IntKey ";
                    //	where=where+" AND  ( "+en.EnMap.PhysicsTable+"."+attr.Field+"= Enum_"+attr.Key+".IntKey ) ";
                }
            }
            //from=from+", "+en.EnMap.PhysicsTable;
            //where="("+where+")";			
            return from + "  WHERE (1=1) ";
        }
        public static string GenerFormOfOra(Entity en)
        {
            string from = " FROM " + en.EnMap.PhysicsTable;

            if (en.EnMap.HisFKEnumAttrs.Count == 0)
                return from + " WHERE (1=1) ";

            string mytable = en.EnMap.PhysicsTable;
            from += ",";
            // 产生外键表列表。
            Attrs fkAttrs = en.EnMap.HisFKAttrs;
            foreach (Attr attr in fkAttrs)
            {
                if (attr.MyFieldType == FieldType.RefText)
                    continue;

                Entity en1 = attr.HisFKEn;
                string fktable = en1.EnMap.PhysicsTable;
                fktable = fktable + " " + fktable + "_" + attr.Key;
                from += fktable + " ,";
            }

            //产生枚举表列表。
            Attrs enumAttrs = en.EnMap.HisEnumAttrs;
            foreach (Attr attr in enumAttrs)
            {
                if (attr.MyFieldType == FieldType.RefText)
                    continue;
                //string enumTable = "Enum_"+attr.Key;
                from += " (SELECT Lab, IntKey FROM Sys_Enum WHERE EnumKey='" + attr.UIBindKey + "' )  Enum_" + attr.Key + " ,";
            }
            from = from.Substring(0, from.Length - 1);
            return from;
        }
        public static string GenerFormWhereOfOra(Entity en)
        {
            string from = " FROM " + en.EnMap.PhysicsTable;

            if (en.EnMap.HisFKAttrs.Count == 0)
                return from + " WHERE ";

            string mytable = en.EnMap.PhysicsTable;
            from += ",";
            // 产生外键表列表。
            Attrs fkAttrs = en.EnMap.HisFKAttrs;
            foreach (Attr attr in fkAttrs)
            {
                if (attr.MyFieldType == FieldType.RefText)
                    continue;

                string fktable = attr.HisFKEn.EnMap.PhysicsTable;

                fktable = fktable + " " + fktable + "_" + attr.Key;
                from += fktable + " ,";
            }

            //产生枚举表列表。
            //Attrs enumAttrs=en.EnMap.HisEnumAttrs;
            //foreach(Attr attr in enumAttrs)
            //{
            //    if (attr.MyFieldType==FieldType.RefText)
            //        continue;
            //    from += " (SELECT Lab, IntKey FROM Sys_Enum WHERE EnumKey='" + attr.UIBindKey + "' )  Enum_" + attr.Key + " ,";
            //    //string enumTable = "Enum_"+attr.Key;
            //    //from +=" (SELECT Lab, IntKey FROM Sys_Enum WHERE EnumKey='"+attr.UIBindKey+"' )  Enum_"+attr.Key+" ,";
            //}

            from = from.Substring(0, from.Length - 1);

            string where = " WHERE ";
            bool isAddAnd = true;

            // 开始形成 外键 where.
            foreach (Attr attr in fkAttrs)
            {
                if (attr.MyFieldType == FieldType.RefText)
                    continue;

                Entity en1 = attr.HisFKEn; // ClassFactory.GetEns(attr.UIBindKey).GetNewEntity;
                string fktable = en1.EnMap.PhysicsTable + "_" + attr.Key;

                if (isAddAnd == false)
                {
                    if (attr.MyDataType == DataType.AppString)
                    {
                        //where+= "( NVL("+mytable+"."+attr.Key+",'"+attr.DefaultVal+"')="+fktable+"."+en1.EnMap.GetFieldByKey(attr.UIRefKeyValue )+"  (+) )";
                        where += "(  " + mytable + "." + attr.Key + "=" + fktable + "." + en1.EnMap.GetFieldByKey(attr.UIRefKeyValue) + "  (+) )";
                    }
                    else
                    {
                        where += "(  " + mytable + "." + attr.Key + "=" + fktable + "." + en1.EnMap.GetFieldByKey(attr.UIRefKeyValue) + "  (+) )";
                    }
                    isAddAnd = true;
                }
                else
                {
                    if (attr.MyDataType == DataType.AppString)
                    {
                        where += " AND (  " + mytable + "." + attr.Key + "=" + fktable + "." + en1.EnMap.GetFieldByKey(attr.UIRefKeyValue) + "  (+) )";
                    }
                    else
                    {
                        where += " AND (  " + mytable + "." + attr.Key + "=" + fktable + "." + en1.EnMap.GetFieldByKey(attr.UIRefKeyValue) + "  (+) )";
                    }
                    //where+= " AND ( "+mytable+"."+attr.Key+"="+fktable+"."+en1.EnMap.GetFieldByKey(attr.UIRefKeyValue )+")";
                }
            }

            //产生枚举 where .。
            //foreach(Attr attr in enumAttrs)
            //{
            //    if (attr.MyFieldType==FieldType.RefText)
            //        continue;

            //    string enumTable = "Enum_"+attr.Key;
            //    if ( isAddAnd==false)
            //    {
            //        if (attr.MyDataType==DataType.AppString)
            //        {
            //            where+= "(   "+mytable+"."+attr.Key+"="+enumTable+".IntKey (+) ) ";
            //        }
            //        else
            //        {
            //            where+= "(  "+mytable+"."+attr.Key+"="+enumTable+".IntKey (+)) ";
            //        }
            //        isAddAnd=true;
            //    }
            //    else
            //    {
            //        if (attr.MyDataType==DataType.AppString)
            //        {
            //            where+= " AND ( "+mytable+"."+attr.Key+"="+enumTable+".IntKey (+)) ";
            //        }
            //        else
            //        {
            //            where+= " AND (  "+mytable+"."+attr.Key+"="+enumTable+".IntKey (+))  ";
            //        } 
            //    }
            //}

            where = where.Replace("WHERE  AND", "WHERE");
            where = where.Replace("WHERE AND", "WHERE");

            //if (where.IndexOf("WHERE  AND ") != -1)
            //  where = where.Replace("WHERE  AND", "WHERE");
            return from + where;
        }
        /// <summary>
        /// 生成sql.
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public static string GenerCreateTableSQLOfMS(Entity en)
        {
            if (en.EnMap.PhysicsTable == "" || en.EnMap.PhysicsTable == null)
                return "delete sys_enum where enumkey='sdsf44a23'";

            //    throw new Exception(en.ToString() +" map error "+en.GetType() );

            string sql = "CREATE TABLE  " + en.EnMap.PhysicsTable + " ( ";
            Attrs attrs = en.EnMap.Attrs;
            if (attrs.Count == 0)
                throw new Exception("@" + en.EnDesc + " , 没有属性集合 attrs.Count = 0 ,能执行数据表的创建.");

            foreach (Attr attr in attrs)
            {
                if (attr.MyFieldType == FieldType.RefText)
                    continue;

                int len = attr.MaxLength;

                switch (attr.MyDataType)
                {
                    case DataType.AppString:
                    case DataType.AppDate:
                    case DataType.AppDateTime:

                        if (attr.MinLength >= 1 || attr.IsPK)
                            sql += "[" + attr.Field + "]  VARCHAR (" + attr.MaxLength + ") NOT NULL,";
                        else
                            sql += "[" + attr.Field + "]  VARCHAR (" + attr.MaxLength + ") NULL,";
                        break;
                    case DataType.AppRate:
                    case DataType.AppFloat:
                    case DataType.AppMoney:
                        sql += "[" + attr.Field + "] FLOAT  NULL,";
                        break;
                    case DataType.AppBoolean:
                        sql += "[" + attr.Field + "] INT  NULL,";
                        break;
                    case DataType.AppInt:
                        if (attr.IsPK)
                            sql += "[" + attr.Field + "] INT NOT NULL,";
                        else
                            sql += "[" + attr.Field + "] INT  NULL,";
                        break;
                    case DataType.AppDouble:
                        sql += "[" + attr.Field + "]  FLOAT  NULL,";
                        break;
                    default:
                        break;
                }
            }
            sql = sql.Substring(0, sql.Length - 1);
            sql += ")";
            return sql;
        }
        public static string GenerCreateTableSQLOf_OLE(Entity en)
        {
            string sql = "CREATE TABLE  " + en.EnMap.PhysicsTable + " (";
            foreach (Attr attr in en.EnMap.Attrs)
            {
                if (attr.MyFieldType == FieldType.RefText)
                    continue;

                int len = attr.MaxLength;
                switch (attr.MyDataType)
                {
                    case DataType.AppString:
                    case DataType.AppDate:
                    case DataType.AppDateTime:
                        if (attr.MaxLength <= 254)
                        {
                            if (attr.MinLength >= 1 || attr.IsPK)
                                sql += "[" + attr.Field + "]  varchar (" + attr.MaxLength + ") NOT NULL,";
                            else
                                sql += "[" + attr.Field + "]  varchar (" + attr.MaxLength + ") NULL,";
                        }
                        else
                        {
                            if (attr.MinLength >= 1 || attr.IsPK)
                                sql += "[" + attr.Field + "]  text  NOT NULL,";
                            else
                                sql += "[" + attr.Field + "] text ,";
                        }
                        break;
                    case DataType.AppRate:
                    case DataType.AppFloat:
                    case DataType.AppMoney:
                        sql += "[" + attr.Field + "] float  NULL,";
                        break;
                    case DataType.AppBoolean:
                    case DataType.AppInt:
                        if (attr.IsPK)
                            sql += "[" + attr.Field + "] int NOT NULL,";
                        else
                            sql += "[" + attr.Field + "] int  NULL,";
                        break;
                    case DataType.AppDouble:
                        sql += "[" + attr.Field + "]  float  NULL,";
                        break;
                    default:
                        break;
                }
            }
            sql = sql.Substring(0, sql.Length - 1);
            sql += ")";
            return sql;
        }
        public static string GenerCreateTableSQL(Entity en)
        {
            switch (DBAccess.AppCenterDBType)
            {
                case DBType.Oracle9i:
                    return GenerCreateTableSQLOfOra(en);
                case DBType.SQL2000:
                case DBType.Access:
                    return GenerCreateTableSQLOfMS(en);
                default:
                    break;
            }
            return null;
        }
        /// <summary>
        /// 生成sql.
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public static string GenerCreateTableSQLOfOra(Entity en)
        {
            if (en.EnMap.PhysicsTable == null)
                throw new Exception("您没有为[" + en.EnDesc + "],设置物理表。");

            if (en.EnMap.PhysicsTable.Trim().Length == 0)
                throw new Exception("您没有为[" + en.EnDesc + "],设置物理表。");

            string sql = "CREATE TABLE  " + en.EnMap.PhysicsTable + " (";

            foreach (Attr attr in en.EnMap.Attrs)
            {
                if (attr.MyFieldType == FieldType.RefText)
                    continue;

                switch (attr.MyDataType)
                {
                    case DataType.AppString:
                    case DataType.AppDate:
                    case DataType.AppDateTime:
                        if (attr.MinLength >= 1 || attr.IsPK)
                            sql += attr.Field + " varchar (" + attr.MaxLength + ") NOT NULL,";
                        else
                            sql += attr.Field + " varchar (" + attr.MaxLength + ") NULL,";
                        break;
                    case DataType.AppRate:
                    case DataType.AppFloat:
                    case DataType.AppMoney:
                    case DataType.AppDouble:
                        sql += attr.Field + " float  NULL,";
                        break;
                    case DataType.AppBoolean:
                    case DataType.AppInt:
                        if (attr.IsPK)
                            sql += attr.Field + " int NOT NULL,";
                        else
                            sql += attr.Field + " int  NULL,";
                        break;
                    default:
                        break;
                }
            }
            sql = sql.Substring(0, sql.Length - 1);
            sql += ")";

            return sql;
        }

        public static string GenerFormWhereOfOra(Entity en, Map map)
        {
            string from = " FROM " + map.PhysicsTable;
            //	string where="  ";
            string table = "";
            string tableAttr = "";
            string enTable = map.PhysicsTable;

            foreach (Attr attr in map.Attrs)
            {
                if (attr.MyFieldType == FieldType.Normal || attr.MyFieldType == FieldType.PK || attr.MyFieldType == FieldType.RefText)
                    continue;

                if (attr.MyFieldType == FieldType.FK || attr.MyFieldType == FieldType.PKFK)
                {
                    //Entity en1= ClassFactory.GetEns(attr.UIBindKey).GetNewEntity;
                    Entity en1 = attr.HisFKEn;

                    table = en1.EnMap.PhysicsTable;
                    tableAttr = "" + en1.EnMap.PhysicsTable + "_" + attr.Key + "";
                    from = from + " LEFT OUTER JOIN " + table + "   " + tableAttr + " ON " + enTable + "." + attr.Field + "=" + tableAttr + "." + en1.EnMap.GetFieldByKey(attr.UIRefKeyValue);
                    //where=where+" AND "+" ("+en.EnMap.PhysicsTable+"."+attr.Field+"="+en1.EnMap.PhysicsTable+"_"+attr.Key+"."+en1.EnMap.Attrs.GetFieldByKey(attr.UIRefKeyValue )+" ) "  ;
                    continue;
                }
                if (attr.MyFieldType == FieldType.Enum || attr.MyFieldType == FieldType.PKEnum)
                {
                    //from= from+ " LEFT OUTER JOIN "+table+" AS "+tableAttr+ " ON "+enTable+"."+attr.Field+"="+tableAttr+"."+en1.EnMap.Attrs.GetFieldByKey( attr.UIRefKeyValue );
                    tableAttr = "Enum_" + attr.Key;
                    from = from + " LEFT OUTER JOIN ( SELECT Lab, IntKey FROM Sys_Enum WHERE EnumKey='" + attr.UIBindKey + "' )  Enum_" + attr.Key + " ON " + enTable + "." + attr.Field + "=" + tableAttr + ".IntKey ";
                    //	where=where+" AND  ( "+en.EnMap.PhysicsTable+"."+attr.Field+"= Enum_"+attr.Key+".IntKey ) ";
                }
            }
            //from=from+", "+en.EnMap.PhysicsTable;
            //where="("+where+")";			
            return from + "  WHERE (1=1) ";
        }

        public static string GenerFormWhereOfMS(Entity en)
        {
            string from = " FROM " + en.EnMap.PhysicsTable;

            if (en.EnMap.HisFKAttrs.Count == 0)
                return from + " WHERE (1=1)";

            string mytable = en.EnMap.PhysicsTable;
            Attrs fkAttrs = en.EnMap.HisFKAttrs;
            foreach (Attr attr in fkAttrs)
            {
                if (attr.MyFieldType == FieldType.RefText)
                    continue;

                string fktable = attr.HisFKEn.EnMap.PhysicsTable;
                Attr refAttr = attr.HisFKEn.EnMap.GetAttrByKey(attr.UIRefKeyValue);
                from += " LEFT JOIN " + fktable + " AS " + fktable + "_" + attr.Key + " ON " + mytable + "." + attr.Field + "=" + fktable + "_" + attr.Field + "." + refAttr.Field;
            }
            return from + " WHERE (1=1) ";
        }

        public static string GenerFormWhereOfMS_bak(Entity en)
        {
            string from = " FROM " + en.EnMap.PhysicsTable;

            if (en.EnMap.HisFKAttrs.Count == 0)
                return from + " WHERE (1=1)";

            string mytable = en.EnMap.PhysicsTable;
            from += ",";
            // 产生外键表列表。
            Attrs fkAttrs = en.EnMap.HisFKAttrs;
            foreach (Attr attr in fkAttrs)
            {
                if (attr.MyFieldType == FieldType.RefText)
                    continue;

                string fktable = attr.HisFKEn.EnMap.PhysicsTable;

                fktable = fktable + " " + fktable + "_" + attr.Key;
                from += fktable + " ,";
            }

            //产生枚举表列表。
            //Attrs enumAttrs=en.EnMap.HisEnumAttrs;
            //foreach(Attr attr in enumAttrs)
            //{
            //    if (attr.MyFieldType==FieldType.RefText)
            //        continue;
            //    from += " (SELECT Lab, IntKey FROM Sys_Enum WHERE EnumKey='" + attr.UIBindKey + "' )  Enum_" + attr.Key + " ,";
            //    //string enumTable = "Enum_"+attr.Key;
            //    //from +=" (SELECT Lab, IntKey FROM Sys_Enum WHERE EnumKey='"+attr.UIBindKey+"' )  Enum_"+attr.Key+" ,";
            //}

            from = from.Substring(0, from.Length - 1);

            string where = " WHERE ";
            bool isAddAnd = true;

            // 开始形成 外键 where.
            foreach (Attr attr in fkAttrs)
            {
                if (attr.MyFieldType == FieldType.RefText)
                    continue;

                Entity en1 = attr.HisFKEn;
                string fktable = en1.EnMap.PhysicsTable + "_" + attr.Key;

                if (isAddAnd == false)
                {
                    if (attr.MyDataType == DataType.AppString)
                    {
                        where += "(  " + mytable + "." + attr.Key + "=" + fktable + "." + en1.EnMap.GetFieldByKey(attr.UIRefKeyValue) + "  (+)  )";
                    }
                    else
                    {
                        where += "(  " + mytable + "." + attr.Key + "=" + fktable + "." + en1.EnMap.GetFieldByKey(attr.UIRefKeyValue) + "   (+) )";
                    }
                    isAddAnd = true;
                }
                else
                {
                    if (attr.MyDataType == DataType.AppString)
                    {
                        where += " AND (  " + mytable + "." + attr.Key + "=" + fktable + "." + en1.EnMap.GetFieldByKey(attr.UIRefKeyValue) + " )";
                    }
                    else
                    {
                        where += " AND (  " + mytable + "." + attr.Key + "=" + fktable + "." + en1.EnMap.GetFieldByKey(attr.UIRefKeyValue) + " )";
                    }
                }
            }

            where = where.Replace("WHERE  AND", "WHERE");
            where = where.Replace("WHERE AND", "WHERE");

            return from + where;
        }
        /// <summary>
        /// GenerFormWhere
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public static string GenerFormWhereOfMS(Entity en, Map map)
        {
            string from = " FROM " + map.PhysicsTable;
            //	string where="  ";
            string table = "";
            string tableAttr = "";
            string enTable = map.PhysicsTable;
            foreach (Attr attr in map.Attrs)
            {
                if (attr.MyFieldType == FieldType.Normal || attr.MyFieldType == FieldType.PK || attr.MyFieldType == FieldType.RefText)
                    continue;

                if (attr.MyFieldType == FieldType.FK || attr.MyFieldType == FieldType.PKFK)
                {
                    //Entity en1= ClassFactory.GetEns(attr.UIBindKey).GetNewEntity;
                    Entity en1 = attr.HisFKEn;

                    table = en1.EnMap.PhysicsTable;
                    tableAttr = en1.EnMap.PhysicsTable + "_" + attr.Key;
                    if (attr.MyDataType == DataType.AppInt)
                    {
                        from = from + " LEFT OUTER JOIN " + table + " AS " + tableAttr + " ON ISNULL( " + enTable + "." + attr.Field + ", " + en.GetValIntByKey(attr.Key) + ")=" + tableAttr + "." + en1.EnMap.GetFieldByKey(attr.UIRefKeyValue);
                    }
                    else
                    {
                        from = from + " LEFT OUTER JOIN " + table + " AS " + tableAttr + " ON ISNULL( " + enTable + "." + attr.Field + ", '" + en.GetValByKey(attr.Key) + "')=" + tableAttr + "." + en1.EnMap.GetFieldByKey(attr.UIRefKeyValue);
                    }
                    //where=where+" AND "+" ("+en.EnMap.PhysicsTable+"."+attr.Field+"="+en1.EnMap.PhysicsTable+"_"+attr.Key+"."+en1.EnMap.Attrs.GetFieldByKey(attr.UIRefKeyValue )+" ) "  ;
                    continue;
                }
                if (attr.MyFieldType == FieldType.Enum || attr.MyFieldType == FieldType.PKEnum)
                {
                    //from= from+ " LEFT OUTER JOIN "+table+" AS "+tableAttr+ " ON "+enTable+"."+attr.Field+"="+tableAttr+"."+en1.EnMap.Attrs.GetFieldByKey( attr.UIRefKeyValue );
                    tableAttr = "Enum_" + attr.Key;
                    from = from + " LEFT OUTER JOIN ( SELECT Lab, IntKey FROM Sys_Enum WHERE EnumKey='" + attr.UIBindKey + "' )  Enum_" + attr.Key + " ON ISNULL( " + enTable + "." + attr.Field + ", " + en.GetValIntByKey(attr.Key) + ")=" + tableAttr + ".IntKey ";
                    //	where=where+" AND  ( "+en.EnMap.PhysicsTable+"."+attr.Field+"= Enum_"+attr.Key+".IntKey ) ";
                }
            }
            //from=from+", "+en.EnMap.PhysicsTable;
            //where="("+where+")";			
            return from + "  WHERE (1=1) ";
        }
        /// <summary>
        /// GenerFormWhere
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        protected static string GenerFormWhereOfMSOLE(Entity en)
        {
            string fromTop = en.EnMap.PhysicsTable;

            string from = "";
            //	string where="  ";
            string table = "";
            string tableAttr = "";
            string enTable = en.EnMap.PhysicsTable;
            foreach (Attr attr in en.EnMap.Attrs)
            {
                if (attr.MyFieldType == FieldType.Normal || attr.MyFieldType == FieldType.PK || attr.MyFieldType == FieldType.RefText)
                    continue;
                fromTop = "(" + fromTop;

                if (attr.MyFieldType == FieldType.FK || attr.MyFieldType == FieldType.PKFK)
                {
                    Entity en1 = attr.HisFKEn; // ClassFactory.GetEns(attr.UIBindKey).GetNewEntity;
                    table = en1.EnMap.PhysicsTable;
                    tableAttr = en1.EnMap.PhysicsTable + "_" + attr.Key;

                    if (attr.MyDataType == DataType.AppInt)
                    {
                        from = from + " LEFT OUTER JOIN " + table + " AS " + tableAttr + " ON IIf( ISNULL( " + enTable + "." + attr.Field + "), " + en.GetValIntByKey(attr.Key) + " , " + enTable + "." + attr.Field + " )=" + tableAttr + "." + en1.EnMap.GetFieldByKey(attr.UIRefKeyValue);
                    }
                    else
                    {
                        from = from + " LEFT OUTER JOIN " + table + " AS " + tableAttr + " ON IIf( ISNULL( " + enTable + "." + attr.Field + "), '" + en.GetValStringByKey(attr.Key) + "', " + enTable + "." + attr.Field + " )=" + tableAttr + "." + en1.EnMap.GetFieldByKey(attr.UIRefKeyValue);
                    }
                }
                if (attr.MyFieldType == FieldType.Enum || attr.MyFieldType == FieldType.PKEnum)
                {
                    //from= from+ " LEFT OUTER JOIN "+table+" AS "+tableAttr+ " ON "+enTable+"."+attr.Field+"="+tableAttr+"."+en1.EnMap.Attrs.GetFieldByKey( attr.UIRefKeyValue );
                    tableAttr = "Enum_" + attr.Key;
                    from = from + " LEFT OUTER JOIN ( SELECT Lab, IntKey FROM Sys_Enum WHERE EnumKey='" + attr.UIBindKey + "' )  Enum_" + attr.Key + " ON IIf( ISNULL( " + enTable + "." + attr.Field + "), " + en.GetValIntByKey(attr.Key) + ", " + enTable + "." + attr.Field + ")=" + tableAttr + ".IntKey ";
                    //	where=where+" AND  ( "+en.EnMap.PhysicsTable+"."+attr.Field+"= Enum_"+attr.Key+".IntKey ) ";
                }

                from = from + ")";
            }
            fromTop = " FROM " + fromTop;
            //from=from+", "+en.EnMap.PhysicsTable;
            //where="("+where+")";			
            return fromTop + from + "  WHERE (1=1) ";
        }

        protected static string SelectSQLOfOra(Entity en, int topNum)
        {
            string val = ""; // key = null;
            string mainTable = "";

            if (en.EnMap.HisFKAttrs.Count != 0)
                mainTable = en.EnMap.PhysicsTable + ".";

            foreach (Attr attr in en.EnMap.Attrs)
            {
                if (attr.MyFieldType == FieldType.RefText)
                    continue;
                switch (attr.MyDataType)
                {
                    case DataType.AppString:
                        if (attr.DefaultVal == null || attr.DefaultVal as string == "")
                        {
                            if (attr.IsKeyEqualField)
                                val = val + ", " + mainTable + attr.Field;
                            else
                                val = val + "," + mainTable + attr.Field + " " + attr.Key;
                        }
                        else
                        {
                            val = val + ",NVL(" + mainTable + attr.Field + ", '" + attr.DefaultVal + "') " + attr.Key;
                        }

                        if (attr.MyFieldType == FieldType.FK || attr.MyFieldType == FieldType.PKFK)
                        {
                            Map map = attr.HisFKEn.EnMap;
                            val = val + ", " + map.PhysicsTable + "_" + attr.Key + "." + map.GetFieldByKey(attr.UIRefKeyText) + " AS " + attr.Key + "Text";
                        }
                        break;
                    case DataType.AppInt:

                        val = val + ",NVL(" + mainTable + attr.Field + "," +
                        attr.DefaultVal + ")   " + attr.Key + "";

                        if (attr.MyFieldType == FieldType.Enum || attr.MyFieldType == FieldType.PKEnum)
                        {
                            Sys.SysEnums ses = new BP.Sys.SysEnums(attr.UIBindKey, attr.UITag);
                            val = val + "," + ses.GenerCaseWhenForOracle(en.ToString(), mainTable, attr.Key, attr.Field, attr.UIBindKey, int.Parse(attr.DefaultVal.ToString()));
                        }
                        if (attr.MyFieldType == FieldType.FK || attr.MyFieldType == FieldType.PKFK)
                        {
                            Map map = attr.HisFKEn.EnMap;
                            val = val + ", " + map.PhysicsTable + "_" + attr.Key + "." + map.GetFieldByKey(attr.UIRefKeyText) + "  AS " + attr.Key + "Text";
                        }
                        break;
                    case DataType.AppFloat:
                        val = val + ", NVL( round(" + mainTable + attr.Field + ",4) ," +
                            attr.DefaultVal.ToString() + ") AS  " + attr.Key;
                        break;
                    case DataType.AppBoolean:
                        if (attr.DefaultVal.ToString().ToLower() == "false")
                            val = val + ", NVL( " + mainTable + attr.Field + ",0) " + attr.Key;
                        else
                            val = val + ", NVL(" + mainTable + attr.Field + ",1) " +
                                attr.Key;
                        break;
                    case DataType.AppDouble:
                        val = val + ", NVL( round(" + mainTable + attr.Field + " ,4) ," +
                            attr.DefaultVal.ToString() + ") " + attr.Key;
                        break;
                    case DataType.AppMoney:
                        val = val + ", NVL( round(" + mainTable + attr.Field + ",4)," +
                            attr.DefaultVal.ToString() + ") " + attr.Key;
                        break;
                    case DataType.AppDate:
                    case DataType.AppDateTime:
                        if (attr.DefaultVal == null || attr.DefaultVal.ToString() == "")
                            val = val + "," + mainTable + attr.Field + " " + attr.Key;
                        else
                        {
                            val = val + ",NVL(" + mainTable + attr.Field + ",'" +
                                                         attr.DefaultVal.ToString() + "') " + attr.Key;
                        }
                        break;
                    default:
                        throw new Exception("@没有定义的数据类型! attr=" + attr.Key + " MyDataType =" + attr.MyDataType);
                }
            }
            //return  " SELECT TOP " +topNum.ToString()+" " +val.Substring(1) + " FROM "+en.EnMap.PhysicsTable;
            return " SELECT  " + val.Substring(1) + SqlBuilder.GenerFormWhereOfOra(en);
        }

        public static string SelectSQL(Entity en, int topNum)
        {
            switch (en.EnMap.EnDBUrl.DBType)
            {
                case DBType.SQL2000:
                    return SqlBuilder.SelectSQLOfMS(en, topNum);
                case DBType.Access:
                    return SqlBuilder.SelectSQLOfOLE(en, topNum);
                case DBType.Oracle9i:
                    return SqlBuilder.SelectSQLOfOra(en, topNum);
                default:
                    return null;
            }
        }

        /// <summary>
        /// 得到sql of select
        /// </summary>
        /// <param name="en">实体</param>
        /// <param name="top">top</param>
        /// <returns>sql</returns>
        public static string SelectCountSQL(Entity en)
        {
            switch (en.EnMap.EnDBUrl.DBType)
            {
                case DBType.SQL2000:
                    return SqlBuilder.SelectCountSQLOfMS(en);
                case DBType.Access:
                    return SqlBuilder.SelectSQLOfOLE(en, 0);
                case DBType.Oracle9i:
                    return SqlBuilder.SelectSQLOfOra(en, 0);
                default:
                    return null;
            }
        }
        /// <summary>
        /// 建立SelectSQLOfOLE 
        /// </summary>
        /// <param name="en">要执行的en</param>
        /// <param name="topNum">最高查询个数</param>
        /// <returns>返回查询sql</returns>
        public static string SelectSQLOfOLE(Entity en, int topNum)
        {
            string val = ""; // key = null;
            string mainTable = en.EnMap.PhysicsTable + ".";
            foreach (Attr attr in en.EnMap.Attrs)
            {
                if (attr.MyFieldType == FieldType.RefText)
                    continue;
                switch (attr.MyDataType)
                {
                    case DataType.AppString:
                        val = val + ", IIf( ISNULL(" + mainTable + attr.Field + "), '" +
                            attr.DefaultVal.ToString() + "', " + mainTable + attr.Field + " ) AS [" + attr.Key + "]";
                        if (attr.MyFieldType == FieldType.FK || attr.MyFieldType == FieldType.PKFK)
                        {
                            Entity en1 = attr.HisFKEn; // ClassFactory.GetEns(attr.UIBindKey).GetNewEntity;
                            val = val + ", IIf( ISNULL(" + en1.EnMap.PhysicsTable + "_" + attr.Key + "." + en1.EnMap.GetFieldByKey(attr.UIRefKeyText) + ") ,''," + en1.EnMap.PhysicsTable + "_" + attr.Key + "." + en1.EnMap.GetFieldByKey(attr.UIRefKeyText) + ") AS " + attr.Key + "Text";
                        }
                        break;
                    case DataType.AppInt:
                        val = val + ",IIf( ISNULL(" + mainTable + attr.Field + ")," +
                            attr.DefaultVal.ToString() + "," + mainTable + attr.Field + ") AS [" + attr.Key + "]";
                        if (attr.MyFieldType == FieldType.Enum || attr.MyFieldType == FieldType.PKEnum)
                        {
                            val = val + ",IIf( ISNULL( Enum_" + attr.Key + ".Lab),'',Enum_" + attr.Key + ".Lab ) AS " + attr.Key + "Text";
                        }
                        if (attr.MyFieldType == FieldType.FK || attr.MyFieldType == FieldType.PKFK)
                        {
                            Entity en1 = attr.HisFKEn; // ClassFactory.GetEns(attr.UIBindKey).GetNewEntity;
                            val = val + ", IIf( ISNULL(" + en1.EnMap.PhysicsTable + "_" + attr.Key + "." + en1.EnMap.GetFieldByKey(attr.UIRefKeyText) + "),0 ," + en1.EnMap.PhysicsTable + "_" + attr.Key + "." + en1.EnMap.GetFieldByKey(attr.UIRefKeyText) + ") AS " + attr.Key + "Text";
                        }
                        break;
                    case DataType.AppFloat:
                        val = val + ",IIf( ISNULL( Round(" + mainTable + attr.Field + ",2) )," +
                            attr.DefaultVal.ToString() + "," + mainTable + attr.Field + ") AS [" + attr.Key + "]";
                        break;
                    case DataType.AppBoolean:
                        if (attr.DefaultVal.ToString().ToLower() == "false")
                            val = val + ", IIf( ISNULL(" + mainTable + attr.Field + "),0 ," + mainTable + attr.Field + ") AS [" +
                                attr.Key + "]";
                        else
                            val = val + ",IIf( ISNULL(" + mainTable + attr.Field + "),1," + mainTable + attr.Field + ") AS [" +
                                attr.Key + "]";
                        break;
                    case DataType.AppDouble:
                        val = val + ", IIf(ISNULL( Round(" + mainTable + attr.Field + ",4) )," +
                            attr.DefaultVal.ToString() + "," + mainTable + attr.Field + ") AS [" + attr.Key + "]";
                        break;
                    case DataType.AppMoney:
                        val = val + ",IIf( ISNULL(  Round(" + mainTable + attr.Field + ",2) )," +
                            attr.DefaultVal.ToString() + "," + mainTable + attr.Field + ") AS [" + attr.Key + "]";
                        break;
                    case DataType.AppDate:
                        val = val + ", IIf(ISNULL( " + mainTable + attr.Field + "), '" +
                            attr.DefaultVal.ToString() + "'," + mainTable + attr.Field + ") AS [" + attr.Key + "]";
                        break;
                    case DataType.AppDateTime:
                        val = val + ", IIf(ISNULL(" + mainTable + attr.Field + "), '" +
                            attr.DefaultVal.ToString() + "'," + mainTable + attr.Field + ") AS [" + attr.Key + "]";
                        break;
                    default:
                        throw new Exception("@没有定义的数据类型! attr=" + attr.Key + " MyDataType =" + attr.MyDataType);
                }
            }
            if (topNum == -1 || topNum ==0 )
                topNum = 99999;

            //return  " SELECT TOP " +topNum.ToString()+" " +val.Substring(1) + " FROM "+en.EnMap.PhysicsTable;
            return " SELECT TOP " + topNum.ToString() + " " + val.Substring(1) + SqlBuilder.GenerFormWhereOfMSOLE(en);
        }
        public static string SelectSQLOfMS(Entity en, int topNum)
        {
            string val = ""; // key = null;
            string mainTable = "";

            if (en.EnMap.HisFKAttrs.Count != 0)
                mainTable = en.EnMap.PhysicsTable + ".";

            foreach (Attr attr in en.EnMap.Attrs)
            {
                if (attr.MyFieldType == FieldType.RefText)
                    continue;
                switch (attr.MyDataType)
                {
                    case DataType.AppString:
                        if (attr.DefaultVal == null || attr.DefaultVal as string == "")
                        {
                            if (attr.IsKeyEqualField)
                                val = val + ", " + mainTable + attr.Field;
                            else
                                val = val + "," + mainTable + attr.Field + " " + attr.Key;
                        }
                        else
                        {
                            val = val + ",ISNULL(" + mainTable + attr.Field + ", '" + attr.DefaultVal + "') " + attr.Key;
                        }

                        if (attr.MyFieldType == FieldType.FK || attr.MyFieldType == FieldType.PKFK)
                        {
                            Map map = attr.HisFKEn.EnMap;
                            val = val + ", " + map.PhysicsTable + "_" + attr.Key + "." + map.GetFieldByKey(attr.UIRefKeyText) + " AS " + attr.Key + "Text";
                        }
                        break;
                    case DataType.AppInt:
                        val = val + ",ISNULL(" + mainTable + attr.Field + "," +
                        attr.DefaultVal + ")   " + attr.Key + "";
                        if (attr.MyFieldType == FieldType.Enum || attr.MyFieldType == FieldType.PKEnum)
                        {
                            Sys.SysEnums ses = new BP.Sys.SysEnums(attr.UIBindKey, attr.UITag);
                            val = val + "," + ses.GenerCaseWhenForOracle(mainTable, attr.Key, attr.Field, attr.UIBindKey, int.Parse(attr.DefaultVal.ToString()));
                        }
                        if (attr.MyFieldType == FieldType.FK || attr.MyFieldType == FieldType.PKFK)
                        {
                            Map map = attr.HisFKEn.EnMap;
                            val = val + ", " + map.PhysicsTable + "_" + attr.Key + "." + map.GetFieldByKey(attr.UIRefKeyText) + "  AS " + attr.Key + "Text";
                        }
                        break;
                    case DataType.AppFloat:
                        val = val + ", ISNULL( round(" + mainTable + attr.Field + ",4) ," +
                            attr.DefaultVal.ToString() + ") AS  " + attr.Key;
                        break;
                    case DataType.AppBoolean:
                        if (attr.DefaultVal.ToString()  == "0")
                            val = val + ", ISNULL( " + mainTable + attr.Field + ",0) " + attr.Key;
                        else
                            val = val + ", ISNULL(" + mainTable + attr.Field + ",1) " +
                                attr.Key;
                        break;
                    case DataType.AppDouble:
                        val = val + ", ISNULL( round(" + mainTable + attr.Field + " ,4) ," +
                            attr.DefaultVal.ToString() + ") " + attr.Key;
                        break;
                    case DataType.AppMoney:
                        val = val + ", ISNULL( round(" + mainTable + attr.Field + ",4)," +
                            attr.DefaultVal.ToString() + ") " + attr.Key;
                        break;
                    case DataType.AppDate:
                    case DataType.AppDateTime:
                        if (attr.DefaultVal == null || attr.DefaultVal.ToString() == "")
                            val = val + "," + mainTable + attr.Field + " " + attr.Key;
                        else
                        {
                            val = val + ",ISNULL(" + mainTable + attr.Field + ",'" +
                                                         attr.DefaultVal.ToString() + "') " + attr.Key;
                        }
                        break;
                    default:
                        throw new Exception("@没有定义的数据类型! attr=" + attr.Key + " MyDataType =" + attr.MyDataType);
                }
            }

            //return  " SELECT TOP " +topNum.ToString()+" " +val.Substring(1) + " FROM "+en.EnMap.PhysicsTable;
            if (topNum == -1 || topNum == 0)
                topNum = 99999;
            return " SELECT  TOP " + topNum.ToString() + " " + val.Substring(1) + SqlBuilder.GenerFormWhereOfMS(en);
        }
        /// <summary>
        /// 建立selectSQL 
        /// </summary>
        /// <param name="en">要执行的en</param>
        /// <param name="topNum">最高查询个数</param>
        /// <returns>返回查询sql</returns>
        public static string SelectSQLOfMS_bak(Entity en, int topNum)
        {
            // 判断内存里面是否有 此sql.
            string sql = "";
            if (en.EnMap.DepositaryOfMap == Depositary.None)
            {
                /*如果 */
                sql = SelectSQLOfMS(en.EnMap);
            }
            else
            {
                sql = DA.Cash.GetObj(en.ToString() + "SQL", en.EnMap.DepositaryOfMap) as string;

                if (sql == null)
                {
                    sql = SelectSQLOfMS(en.EnMap);
                    // 把来之不易的sql 放入内存
                    DA.Cash.AddObj(en.ToString() + "SQL", Depositary.Application, sql);
                }
            }

            // 替换他
            if (topNum == -1)
                topNum = 99999;

            sql = sql.Replace("@TopNum", topNum.ToString());
            return sql + SqlBuilder.GenerFormWhereOfMS(en, en.EnMap);
        }

        /// <summary>
        /// 建立selectSQL 
        /// </summary>
        /// <param name="en">要执行的en</param>
        /// <param name="topNum">最高查询个数</param>
        /// <returns>返回查询sql</returns>
        public static string SelectCountSQLOfMS(Entity en)
        {
            // 判断内存里面是否有 此sql.
            string sql = "SELECT COUNT(*) ";
            return sql + SqlBuilder.GenerFormWhereOfMS(en, en.EnMap);
        }
        public static string SelectSQLOfOra(string enName, Map map)
        {
            string val = ""; // key = null;
            string mainTable = map.PhysicsTable + ".";
            foreach (Attr attr in map.Attrs)
            {
                if (attr.MyFieldType == FieldType.RefText)
                    continue;

                switch (attr.MyDataType)
                {
                    case DataType.AppString:
                        if (attr.DefaultVal == null)
                            val = val + ", " + mainTable + attr.Field + " AS " + attr.Key;
                        else
                            val = val + ", NVL(" + mainTable + attr.Field + ", '" + attr.DefaultVal + "') AS " + attr.Key;

                        if (attr.MyFieldType == FieldType.FK || attr.MyFieldType == FieldType.PKFK)
                        {
                            Entity en1 = attr.HisFKEn; // ClassFactory.GetEns(attr.UIBindKey).GetNewEntity;
                            val = val + ", " + en1.EnMap.PhysicsTable + "_" + attr.Key + "." + en1.EnMap.GetFieldByKey(attr.UIRefKeyText) + " AS " + attr.Key + "Text";
                        }
                        break;
                    case DataType.AppInt:
                        val = val + ", NVL(" + mainTable + attr.Field + "," +
                            attr.DefaultVal + ") AS  " + attr.Key + "";
                        if (attr.MyFieldType == FieldType.Enum || attr.MyFieldType == FieldType.PKEnum)
                        {
                            Sys.SysEnums ses = new BP.Sys.SysEnums(attr.UIBindKey, attr.UITag);
                            val = val + "," + ses.GenerCaseWhenForOracle(enName, mainTable, attr.Key, attr.Field, attr.UIBindKey, int.Parse(attr.DefaultVal.ToString()));
                        }
                        if (attr.MyFieldType == FieldType.FK || attr.MyFieldType == FieldType.PKFK)
                        {
                            Entity en1 = attr.HisFKEn; // ClassFactory.GetEns(attr.UIBindKey).GetNewEntity;
                            val = val + ", " + en1.EnMap.PhysicsTable + "_" + attr.Key + "." + en1.EnMap.GetFieldByKey(attr.UIRefKeyText) + "  AS " + attr.Key + "Text";
                        }
                        break;
                    case DataType.AppFloat:
                        val = val + ", NVL( ROUND(" + mainTable + attr.Field + ", 2 )," +
                            attr.DefaultVal.ToString() + ") AS  " + attr.Key;
                        break;
                    case DataType.AppBoolean:
                        if (attr.DefaultVal.ToString().ToLower() == "false")
                            val = val + ", NVL( " + mainTable + attr.Field + " , 0)  AS " + attr.Key;
                        else
                            val = val + ", NVL(  " + mainTable + attr.Field + ", 1)  AS " +
                                attr.Key;
                        break;
                    case DataType.AppDouble:
                        val = val + ", NVL( ROUND(" + mainTable + attr.Field + ",4)," +
                            attr.DefaultVal.ToString() + ") AS " + attr.Key;
                        break;
                    case DataType.AppMoney:
                        val = val + ", NVL( ROUND(" + mainTable + attr.Field + ",2)," +
                            attr.DefaultVal.ToString() + ") AS " + attr.Key;
                        break;
                    case DataType.AppDate:
                        val = val + ", NVL(  " + mainTable + attr.Field + ", '" +
                            attr.DefaultVal.ToString() + "')  AS " + attr.Key;
                        break;
                    case DataType.AppDateTime:
                        val = val + ", NVL(" + mainTable + attr.Field + ", '" +
                            attr.DefaultVal.ToString() + "') AS " + attr.Key;
                        break;
                    default:
                        throw new Exception("@没有定义的数据类型! attr=" + attr.Key + " MyDataType =" + attr.MyDataType);
                }
            }
            return "SELECT " + val.Substring(1);
        }
        /// <summary>
        /// 产生sql
        /// </summary>
        /// <returns></returns>
        public static string SelectSQLOfMS(Map map)
        {
            string val = ""; // key = null;
            string mainTable = map.PhysicsTable + ".";
            foreach (Attr attr in map.Attrs)
            {
                if (attr.MyFieldType == FieldType.RefText)
                    continue;
                switch (attr.MyDataType)
                {
                    case DataType.AppString:
                        if (attr.DefaultVal == null)
                            val = val + " " + mainTable + attr.Field + " AS [" + attr.Key + "]";
                        else
                            val = val + ", ISNULL(" + mainTable + attr.Field + ", '" + attr.DefaultVal.ToString() + "') AS [" + attr.Key + "]";

                        if (attr.MyFieldType == FieldType.FK || attr.MyFieldType == FieldType.PKFK)
                        {
                            Entity en1 = attr.HisFKEn;
                            //Entity en1 = ClassFactory.GetEns(attr.UIBindKey).GetNewEntity;
                            val = val + ", " + en1.EnMap.PhysicsTable + "_" + attr.Key + "." + en1.EnMap.GetFieldByKey(attr.UIRefKeyText) + " AS " + attr.Key + "Text";
                        }
                        break;
                    case DataType.AppInt:
                        val = val + ", ISNULL(" + mainTable + attr.Field + "," +
                            attr.DefaultVal.ToString() + ") AS [" + attr.Key + "]";
                        if (attr.MyFieldType == FieldType.Enum || attr.MyFieldType == FieldType.PKEnum)
                        {
                            val = val + ",  Enum_" + attr.Key + ".Lab  AS " + attr.Key + "Text";
                        }
                        if (attr.MyFieldType == FieldType.FK || attr.MyFieldType == FieldType.PKFK)
                        {
                            //Entity en1= ClassFactory.GetEns(attr.UIBindKey).GetNewEntity;
                            Entity en1 = attr.HisFKEn;
                            val = val + ",  " + en1.EnMap.PhysicsTable + "_" + attr.Key + "." + en1.EnMap.GetFieldByKey(attr.UIRefKeyText) + " AS " + attr.Key + "Text";
                        }
                        break;
                    case DataType.AppFloat:
                        val = val + ", ISNULL(" + mainTable + attr.Field + "," +
                            attr.DefaultVal.ToString() + ") AS [" + attr.Key + "]";
                        break;
                    case DataType.AppBoolean:
                        if (attr.DefaultVal.ToString().ToLower() == "false")
                            val = val + ", ISNULL(" + mainTable + attr.Field + ",0) AS [" +
                                attr.Key + "]";
                        else
                            val = val + ", ISNULL(" + mainTable + attr.Field + ",1) AS [" +
                                attr.Key + "]";
                        break;
                    case DataType.AppDouble:
                        val = val + ", ISNULL(" + mainTable + attr.Field + "," +
                            attr.DefaultVal.ToString() + ") AS [" + attr.Key + "]";
                        break;
                    case DataType.AppMoney:
                        val = val + ", ISNULL(" + mainTable + attr.Field + "," +
                            attr.DefaultVal.ToString() + ") AS [" + attr.Key + "]";
                        break;
                    case DataType.AppDate:
                        val = val + ", ISNULL(" + mainTable + attr.Field + ", '" +
                            attr.DefaultVal.ToString() + "') AS [" + attr.Key + "]";
                        break;
                    case DataType.AppDateTime:
                        val = val + ", ISNULL(" + mainTable + attr.Field + ", '" +
                            attr.DefaultVal.ToString() + "') AS [" + attr.Key + "]";
                        break;
                    default:
                        if (attr.Key == "MyNum")
                        {
                            val = val + ", COUNT(*)  AS MyNum ";
                            break;
                        }
                        else
                            throw new Exception("@没有定义的数据类型! attr=" + attr.Key + " MyDataType =" + attr.MyDataType);
                }
            }
            return "SELECT TOP @TopNum " + val.Substring(1);
        }
        public static string UpdateOfMSAccess(Entity en, string[] keys)
        {
            string val = "";
            foreach (Attr attr in en.EnMap.Attrs)
            {
                /* 两个PK 的情况 */
                //if (attr.IsPK)
                //    continue;

                if (keys != null)
                {
                    bool isHave = false;
                    foreach (string s in keys)
                    {
                        if (attr.Key == s)
                        {
                            /* 如果找到了要更新的key */
                            isHave = true;
                            break;
                        }
                    }
                    if (isHave == false)
                        continue;
                }


                if (attr.MyFieldType == FieldType.RefText
                    || attr.MyFieldType == FieldType.PK
                    || attr.MyFieldType == FieldType.PKFK
                    || attr.MyFieldType == FieldType.PKEnum)
                    continue;

                switch (attr.MyDataType)
                {
                    case DataType.AppString:
                        val = val + ",[" + attr.Field + "]='" + en.GetValStringByKey(attr.Key) + "'";
                        break;
                    case DataType.AppInt:
                        val = val + ",[" + attr.Field + "]=" + en.GetValStringByKey(attr.Key);
                        break;
                    case DataType.AppFloat:
                    case DataType.AppDouble:
                    case DataType.AppMoney:
                        string str = en.GetValStringByKey(attr.Key).ToString();
                        str = str.Replace("￥", "");
                        str = str.Replace(",", "");
                        val = val + ",[" + attr.Field + "]=" + str;
                        break;
                    case DataType.AppBoolean:
                        val = val + ",[" + attr.Field + "]=" + en.GetValStringByKey(attr.Key);
                        break;
                    case DataType.AppDate: // 如果是日期类型。
                    case DataType.AppDateTime:
                        string da = en.GetValStringByKey(attr.Key);
                        if (da.IndexOf("_DATE") == -1)
                            val = val + ",[" + attr.Field + "]='" + da + "'";
                        else
                            val = val + ",[" + attr.Field + "]=" + da;
                        break;
                    default:
                        throw new Exception("@SqlBulider.update, 没有这个数据类型");
                }
            }

            string sql = "";

            if (val == "")
            {
                /*如果没有出现要更新.*/
                foreach (Attr attr in en.EnMap.Attrs)
                {
                    if (keys != null)
                    {
                        bool isHave = false;
                        foreach (string s in keys)
                        {
                            if (attr.Key == s)
                            {
                                /* 如果找到了要更新的key */
                                isHave = true;
                                break;
                            }
                        }
                        if (isHave == false)
                            continue;
                    }
                    switch (attr.MyDataType)
                    {
                        case DataType.AppString:
                            val = val + ",[" + attr.Field + "]='" + en.GetValStringByKey(attr.Key) + "'";
                            break;
                        case DataType.AppInt:
                            val = val + ",[" + attr.Field + "]=" + en.GetValStringByKey(attr.Key);
                            break;
                        case DataType.AppFloat:
                        case DataType.AppDouble:
                        case DataType.AppMoney:
                            string str = en.GetValStringByKey(attr.Key).ToString();
                            str = str.Replace("￥", "");
                            str = str.Replace(",", "");
                            val = val + ",[" + attr.Field + "]=" + str;
                            break;
                        case DataType.AppBoolean:
                            val = val + ",[" + attr.Field + "]=" + en.GetValStringByKey(attr.Key);
                            break;
                        case DataType.AppDate: // 如果是日期类型。
                        case DataType.AppDateTime:
                            string da = en.GetValStringByKey(attr.Key);
                            if (da.IndexOf("_DATE") == -1)
                                val = val + ",[" + attr.Field + "]='" + da + "'";
                            else
                                val = val + ",[" + attr.Field + "]=" + da;
                            break;
                        default:
                            throw new Exception("@SqlBulider.update, 没有这个数据类型");
                    }
                }
                //return null;
                //throw new Exception("出现了一个不合理的更新：没有要更新的数据。");
            }

            if (val == "")
            {
                string ms = "";
                foreach (string str in keys)
                {
                    ms += str;
                }
                throw new Exception(en.EnDesc + "执行更新错误：无效的属性[" + ms + "]对于本实体来说。");
            }
            sql = "UPDATE " + en.EnMap.PhysicsTable + " SET " + val.Substring(1) +
                        " WHERE " + SqlBuilder.GetKeyConditionOfOLE(en);
            return sql.Replace(",=''", "");
        }
        /// <summary>
        /// 产生要更新的sql 语句
        /// </summary>
        /// <param name="en">要更新的entity</param>
        /// <param name="keys">要更新的keys</param>
        /// <returns>sql</returns>
        public static string Update(Entity en, string[] keys)
        {
            Map map = en.EnMap;

            string val = "";
            foreach (Attr attr in map.Attrs)
            {
                if (keys != null)
                {
                    bool isHave = false;
                    foreach (string s in keys)
                    {
                        if (attr.Key == s)
                        {
                            /* 如果找到了要更新的key*/
                            isHave = true;
                            break;
                        }
                    }
                    if (isHave == false)
                        continue;
                }

                if (attr.MyFieldType == FieldType.RefText)
                    continue;

                /*有可能是两个主键的情况。*/
              //  if (attr.IsPK)
                  //  continue;

                switch (attr.MyDataType)
                {
                    case DataType.AppString:
                        if (map.EnDBUrl.DBType == DBType.Access)
                        {
                            val = val + ",[" + attr.Field + "]='" + en.GetValStringByKey(attr.Key).Replace('\'', '~') + "'";
                        }
                        else
                        {
                            if (attr.UIIsDoc && attr.Key == "Doc")
                            {

                                string doc = en.GetValStringByKey(attr.Key);
                                if (map.Attrs.Contains("DocLength"))
                                    en.SetValByKey("DocLength", doc.Length);

                                if (doc.Length >= 2000)
                                {
                                    Sys.SysDocFile.SetValV2(en.ToString(), en.PKVal.ToString(), doc);
                                    val = val + "," + attr.Field + "=''";
                                }
                                else
                                {
                                    val = val + "," + attr.Field + "='" + en.GetValStringByKey(attr.Key).Replace('\'', '~') + "'";
                                }
                            }
                            else
                            {
                                val = val + "," + attr.Field + "='" + en.GetValStringByKey(attr.Key).Replace('\'', '~') + "'";
                            }
                        }
                        break;
                    case DataType.AppInt:
                        val = val + "," + attr.Field + "=" + en.GetValStringByKey(attr.Key);
                        break;
                    case DataType.AppFloat:
                    case DataType.AppDouble:
                    case DataType.AppMoney:
                        string str = en.GetValStringByKey(attr.Key).ToString();
                        str = str.Replace("￥", "");
                        str = str.Replace(",", "");
                        val = val + "," + attr.Field + "=" + str;
                        break;
                    case DataType.AppBoolean:
                        val = val + "," + attr.Field + "=" + en.GetValStringByKey(attr.Key);
                        break;
                    case DataType.AppDate: // 如果是日期类型。
                    case DataType.AppDateTime:
                        string da = en.GetValStringByKey(attr.Key);
                        if (da.IndexOf("_DATE") == -1)
                            val = val + "," + attr.Field + "='" + da + "'";
                        else
                            val = val + "," + attr.Field + "=" + da;
                        break;
                    default:
                        throw new Exception("@SqlBulider.update, 没有这个数据类型");
                }
            }

            string sql = "";

            if (val == "")
            {
                /*如果没有出现要更新.*/
                foreach (Attr attr in map.Attrs)
                {
                    if (keys != null)
                    {
                        bool isHave = false;
                        foreach (string s in keys)
                        {
                            if (attr.Key == s)
                            {
                                /* 如果找到了要更新的key */
                                isHave = true;
                                break;
                            }
                        }
                        if (isHave == false)
                            continue;
                    }

                    // 两个PK 的情况。
                    //if (attr.IsPK)
                    //    continue;


                    switch (attr.MyDataType)
                    {
                        case DataType.AppString:
                            val = val + "," + attr.Field + "='" + en.GetValStringByKey(attr.Key) + "'";
                            break;
                        case DataType.AppInt:
                            val = val + "," + attr.Field + "=" + en.GetValStringByKey(attr.Key);
                            break;
                        case DataType.AppFloat:
                        case DataType.AppDouble:
                        case DataType.AppMoney:
                            string str = en.GetValStringByKey(attr.Key).ToString();
                            str = str.Replace("￥", "");
                            str = str.Replace(",", "");
                            val = val + "," + attr.Field + "=" + str;
                            break;
                        case DataType.AppBoolean:
                            val = val + "," + attr.Field + "=" + en.GetValStringByKey(attr.Key);
                            break;
                        case DataType.AppDate: // 如果是日期类型。
                        case DataType.AppDateTime:
                            string da = en.GetValStringByKey(attr.Key);
                            if (da.IndexOf("_DATE") == -1)
                                val = val + "," + attr.Field + "='" + da + "'";
                            else
                                val = val + "," + attr.Field + "=" + da;
                            break;
                        default:
                            throw new Exception("@SqlBulider.update, 没有这个数据类型");
                    }
                }
                //return null;
                //throw new Exception("出现了一个不合理的更新：没有要更新的数据。");
            }

            if (val == "")
            {
                string ms = "";
                foreach (string str in keys)
                {
                    ms += str;
                }
                throw new Exception(en.EnDesc + "执行更新错误：无效的属性[" + ms + "]对于本实体来说。");
            }

            switch (en.EnMap.EnDBUrl.DBType)
            {
                case DBType.SQL2000:
                case DBType.Access:
                    sql = "UPDATE " + en.EnMap.PhysicsTable + " SET " + val.Substring(1) +
                        " WHERE " + SqlBuilder.GenerWhereByPK(en, "@");
                    break;
                //case DBType.Access:
                //    sql = "UPDATE " + en.EnMap.PhysicsTable + " SET " + val.Substring(1) +
                //        " WHERE " + SqlBuilder.GenerWhereByPK(en,"@");
                //    break;
                case DBType.Oracle9i:
                    sql = "UPDATE " + en.EnMap.PhysicsTable + " SET " + val.Substring(1) +
                        " WHERE " + SqlBuilder.GenerWhereByPK(en, ":");
                    break;
                default:
                    throw new Exception("no this case db type . ");
            }
            return sql.Replace(",=''", "");
        }
        public static Paras GenerParas(Entity en, string[] keys)
        {
            string mykeys = "";
            if (keys != null)
                foreach (string key in keys)
                    mykeys += "@" + key;
          
            Map map = en.EnMap;
            Paras ps = new Paras();
            foreach (Attr attr in map.Attrs)
            {
                if (attr.MyFieldType == FieldType.RefText)
                    continue;

                if (keys != null)
                    if (mykeys.Contains("@" + attr.Key) == false)
                    {
                        //if (attr.IsPK == false)
                        //    continue;
                    }

                switch (attr.MyDataType)
                {
                    case DataType.AppString:
                        if (attr.UIIsDoc && attr.Key == "Doc")
                        {
                            string doc = en.GetValStrByKey(attr.Key).Replace('\'', '~');

                            if (map.Attrs.Contains("DocLength"))
                                en.SetValByKey("DocLength", doc.Length);

                            if (doc.Length >= 2000)
                            {
                                Sys.SysDocFile.SetValV2(en.ToString(), en.PKVal.ToString(), doc);
                                ps.Add(attr.Key, "");
                            }
                            else
                            {
                                ps.Add(attr.Key, en.GetValStrByKey(attr.Key).Replace('\'', '~'));
                            }
                        }
                        else
                        {
                            ps.Add(attr.Key, en.GetValStrByKey(attr.Key).Replace('\'', '~'));
                        }
                        break;
                    case DataType.AppBoolean:
                    case DataType.AppInt:
                        ps.Add(attr.Key, en.GetValIntByKey(attr.Key));
                        break;
                    case DataType.AppFloat:
                    case DataType.AppDouble:
                    case DataType.AppMoney:
                        string str = en.GetValStrByKey(attr.Key).ToString();
                        str = str.Replace("￥", "");
                        str = str.Replace(",", "");
                        ps.Add(attr.Key, decimal.Parse(str));
                        break;
                    case DataType.AppDate: // 如果是日期类型。
                    case DataType.AppDateTime:
                        string da = en.GetValStringByKey(attr.Key);
                        ps.Add(attr.Key, da);
                        break;
                    default:
                        throw new Exception("@SqlBulider.update, 没有这个数据类型");
                }
            }
            return ps;
        }
        public static string UpdateForPara(Entity en, string[] keys)
        {
            string mykey = "";
            if (keys != null)
                foreach (string s in keys)
                    mykey += "@" + s;

            //  string dbstr = en.HisDBVarStr;
            Map map = en.EnMap;
            string val = "";
            foreach (Attr attr in map.Attrs)
            {
                if (attr.MyFieldType == FieldType.RefText)
                    continue;

                if (attr.IsPK)
                {
                    // #warning add 2009 - 11- 04
                    // 两个PK 的情况。
                   // continue;
                    if (en.PKCount==1)
                        continue;
                }
                else
                {
                    if (keys != null)
                        if (mykey.Contains("@" + attr.Key) == false)
                            continue;
                }

                val = val + "," + attr.Field + "=" + en.HisDBVarStr + attr.Key;
            }
            string sql = "";
            switch (en.EnMap.EnDBUrl.DBType)
            {
                case DBType.SQL2000:
                case DBType.Access:
                    sql = "UPDATE " + en.EnMap.PhysicsTable + " SET " + val.Substring(1) +
                        " WHERE " + SqlBuilder.GenerWhereByPK(en, "@");
                    break;
                case DBType.Oracle9i:
                    sql = "UPDATE " + en.EnMap.PhysicsTable + " SET " + val.Substring(1) +
                        " WHERE " + SqlBuilder.GenerWhereByPK(en, ":");
                    break;
                default:
                    throw new Exception("no this case db type . ");
            }
            return sql.Replace(",=''", "");
        }
        /// <summary>
        /// Delete sql
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public static string DeleteForPara(Entity en)
        {
            string dbstr = en.HisDBVarStr;
            string sql = "DELETE FROM " + en.EnMap.PhysicsTable + " WHERE " +
                SqlBuilder.GenerWhereByPK(en, dbstr);
            return sql;
        }
        public static string InsertForPara(Entity en)
        {
            string dbstr = en.HisDBVarStr;

            bool isInnkey = false;
            if (en.IsOIDEntity)
            {
                EntityOID myen = en as EntityOID;
#warning edit
                isInnkey = false; // myen.IsInnKey;
            }


            string key = "", field = "", val = "";
            foreach (Attr attr in en.EnMap.Attrs)
            {
                if (attr.MyFieldType == FieldType.RefText)
                    continue;

                if (isInnkey)
                {
                    if (attr.Key == "OID")
                        continue;
                }

                key = attr.Key;
                field = field + ",[" + attr.Field+"]";
                val = val + "," + dbstr + attr.Key;
            }
            string sql = "INSERT INTO " + en.EnMap.PhysicsTable + " (" +
                field.Substring(1) + " ) VALUES ( " + val.Substring(1) + ")";
            return sql;
        }
        /// <summary>
        /// Insert 
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public static string Insert(Entity en)
        {
            string key = "", field = "", val = "";
            Attrs attrs = en.EnMap.Attrs;
            foreach (Attr attr in attrs)
            {
                if (attr.MyFieldType == FieldType.RefText)
                    continue;
                key = attr.Key;
                field = field + "," + attr.Field;
                switch (attr.MyDataType)
                {
                    case DataType.AppString:
                        if (attr.UIIsDoc && attr.Key == "Doc")
                        {
                            string doc = en.GetValStringByKey(key).Replace('\'', '~');
                            if (attrs.Contains("DocLength"))
                                en.SetValByKey("DocLength", doc.Length);

                            if (doc.Length >= 2000)
                            {
                                Sys.SysDocFile.SetValV2(en.ToString(), en.PKVal.ToString(), doc);
                                if (attrs.Contains("DocLength"))
                                    en.SetValByKey("DocLength", doc.Length);
                                val = val + ",''";
                            }
                            else
                            {
                                val = val + ",'" + en.GetValStringByKey(key).Replace('\'', '~') + "'";
                            }
                        }
                        else
                        {
                            val = val + ",'" + en.GetValStringByKey(key).Replace('\'', '~') + "'";
                        }
                        break;
                    case DataType.AppInt:
                        val = val + "," + en.GetValIntByKey(key);
                        break;
                    case DataType.AppFloat:
                    case DataType.AppDouble:
                    case DataType.AppMoney:
                        string str = en.GetValStringByKey(key).ToString();
                        str = str.Replace("￥", "");
                        str = str.Replace(",", "");
                        if (str == "")
                            str = "0";
                        val = val + "," + str;
                        break;
                    case DataType.AppBoolean:
                        val = val + "," + en.GetValIntByKey(key);
                        break;
                    case DataType.AppDate:
                    case DataType.AppDateTime:
                        string da = en.GetValStringByKey(attr.Key);
                        if (da.IndexOf("_DATE") == -1)
                            val = val + ",'" + da + "'";
                        else
                            val = val + "," + da;
                        break;
                    default:
                        throw new Exception("@bulider insert sql error: 没有这个数据类型");
                }
            }
            string sql = "INSERT INTO " + en.EnMap.PhysicsTable + " (" +
                field.Substring(1) + " ) VALUES ( " + val.Substring(1) + ")";
            return sql;
        }
        public static string InsertOFOLE(Entity en)
        {
            string key = "", field = "", val = "";
            foreach (Attr attr in en.EnMap.Attrs)
            {
                if (attr.MyFieldType == FieldType.RefText)
                    continue;
                key = attr.Key;
                field = field + ",[" + attr.Field + "]";
                switch (attr.MyDataType)
                {
                    case DataType.AppString:
                        val = val + ", '" + en.GetValStringByKey(key) + "'";
                        break;
                    case DataType.AppInt:
                        val = val + "," + en.GetValIntByKey(key);
                        break;
                    case DataType.AppFloat:
                    case DataType.AppDouble:
                    case DataType.AppMoney:
                        string str = en.GetValStringByKey(key).ToString();
                        str = str.Replace("￥", "");
                        str = str.Replace(",", "");
                        if (str == "")
                            str = "0";
                        val = val + "," + str;
                        break;
                    case DataType.AppBoolean:
                        val = val + "," + en.GetValIntByKey(key);
                        break;
                    case DataType.AppDate:
                    case DataType.AppDateTime:
                        string da = en.GetValStringByKey(attr.Key);
                        if (da.IndexOf("_DATE") == -1)
                            val = val + ",'" + da + "'";
                        else
                            val = val + "," + da;
                        break;
                    default:
                        throw new Exception("@bulider insert sql error: 没有这个数据类型");
                }
            }
            string sql = "INSERT INTO " + en.EnMap.PhysicsTable + " (" +
                field.Substring(1) + " ) VALUES ( " + val.Substring(1) + ")";
            return sql;
        }
    }

}
