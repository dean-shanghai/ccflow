using System;
using System.Data;
using System.Collections;
using BP.DA;
using BP.Web.Controls;
using System.Reflection;
using BP.Port;
using BP.En;
namespace BP.WF.DTS
{
    /// <summary>
    /// 修复表单物理表字段长度 的摘要说明
    /// </summary>
    public class SearchMaxWorkID : Method
    {
        /// <summary>
        /// 不带有参数的方法
        /// </summary>
        public SearchMaxWorkID()
        {
            this.Title = "查询最大的WorkID";
            this.Help = "检查是否workid重复。";
        }
        /// <summary>
        /// 设置执行变量
        /// </summary>
        /// <returns></returns>
        public override void Init()
        {
        }
        /// <summary>
        /// 当前的操纵员是否可以执行这个方法
        /// </summary>
        public override bool IsCanDo
        {
            get
            {
                if (Web.WebUser.No == "admin")
                    return true;
                else
                    return false;
            }
        }
        /// <summary>
        /// 执行
        /// </summary>
        /// <returns>返回执行结果</returns>
        public override object Do()
        {

            string msg="";

            Flows fls = new Flows();
            fls.RetrieveAllFromDBSource();
            Int64 workid = 0;
            foreach (Flow fl in fls)
            {
                DataTable dt = DBAccess.RunSQLReturnTable("SELECT MAX(OID) FROM ND" + int.Parse(fl.No) + "Rpt");
                if (dt.Rows.Count == 0)
                    continue;
                try
                {
                    Int64 workidN = Int64.Parse(dt.Rows[0][0].ToString());
                    if (workidN > workid)
                        workid = workidN;
                }
                catch(Exception ex)
                {
                    continue;
                    //msg += "@" + ex.Message + " val=" + dt.Rows[0][0].ToString()+".";
                }
            }

            DataTable d1t = DBAccess.RunSQLReturnTable("SELECT IntVal FROM Sys_Serial where CfgKey='OID'");
            Int64 workidOld = int.Parse(d1t.Rows[0][0].ToString());
            return "系统Sys_Serial OID ：" + workidOld + " ,流程中使用的最大OID是 " + workid +" <hr>"+msg;
        }
    }
}
