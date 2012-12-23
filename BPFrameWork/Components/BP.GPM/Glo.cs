using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using BP.Sys;
using BP.DA;
using BP.En;
using BP;

namespace BP.GPM
{
    public class Glo
    {
        /// <summary>
        /// 安装包
        /// </summary>
        public static void DoInstallDataBase(string lang, string yunXingHuanjing)
        {
            ArrayList al = null;
            string info = "BP.En.Entity";
            al = BP.DA.ClassFactory.GetObjects(info);

            #region 1, 修复表
            foreach (Object obj in al)
            {
                Entity en = null;
                en = obj as Entity;
                if (en == null)
                    continue;
                string table = null;
                try
                {
                    table = en.EnMap.PhysicsTable;
                    if (table == null)
                        continue;
                }
                catch
                {
                    continue;
                }

                switch (table)
                {
                    case "WF_EmpWorks":
                    case "WF_GenerEmpWorkDtls":
                    case "WF_GenerEmpWorks":
                    case "WF_NodeExt":
                        continue;
                    case "Sys_Enum":
                        en.CheckPhysicsTable();
                        break;
                    default:
                        en.CheckPhysicsTable();
                        break;
                }

                en.PKVal = "123";
                try
                {
                    en.RetrieveFromDBSources();
                }
                catch (Exception ex)
                {
                    Log.DebugWriteWarning(ex.Message);
                    en.CheckPhysicsTable();
                }
            }
            #endregion 修复

            #region 2, 注册枚举类型 sql
            // 2, 注册枚举类型。
            BP.Sys.Xml.EnumInfoXmls xmls = new BP.Sys.Xml.EnumInfoXmls();
            xmls.RetrieveAll();
            foreach (BP.Sys.Xml.EnumInfoXml xml in xmls)
            {
                BP.Sys.SysEnums ses = new BP.Sys.SysEnums();
                ses.RegIt(xml.Key, xml.Vals);
            }
            #endregion 注册枚举类型

            #region 3, 执行基本的 sql
            string sqlscript = SystemConfig.PathOfData + "Install\\SQLScript\\Port_" + yunXingHuanjing + "_" + lang + ".sql";
            BP.DA.DBAccess.RunSQLScript(sqlscript);
            #endregion 修复

            #region 5, 初始化数据。
            sqlscript = SystemConfig.PathOfData + "Install\\SQLScript\\InitPublicData.sql";
            BP.DA.DBAccess.RunSQLScript(sqlscript);
            #endregion 初始化数据
        }
        /// <summary>
        /// 是否可以执行判断
        /// </summary>
        /// <param name="obj">判断对象</param>
        /// <param name="cw">方式</param>
        /// <returns>是否可以执行</returns>
        public static bool IsCanDoIt(object obj, BP.GPM.CtrlWay cw)
        {
            int n = 0;
            string sql = "";
            switch (cw)
            {
                case CtrlWay.AnyOne:
                    return true;
                case CtrlWay.ByStation:
                    sql = "SELECT count(*) FROM GPM_ByStation WHERE RefObj='" + obj + "'  AND FK_Station IN (select fk_station from Port_EmpStation where FK_Emp='" + BP.Web.WebUser.No + "')";
                    n = BP.DA.DBAccess.RunSQLReturnValInt(sql, 0);
                    break;
                case CtrlWay.ByDept:
                    sql = "SELECT count(*) FROM GPM_ByDept WHERE RefObj='" + obj + "'  AND FK_Dept IN (select FK_Dept from Port_EmpDept WHERE FK_Emp='" + BP.Web.WebUser.No + "')";
                    n = BP.DA.DBAccess.RunSQLReturnValInt(sql, 0);
                    break;
                case CtrlWay.ByEmp:
                    sql = "SELECT count(*) FROM GPM_ByEmp WHERE RefObj='" + obj + "'  AND  FK_Emp='" + BP.Web.WebUser.No + "'";
                    n = BP.DA.DBAccess.RunSQLReturnValInt(sql, 0);
                    break;
                default:
                    break;
            }

            if (n == 0)
                return false;
            else
                return true;
        }
    }
}
