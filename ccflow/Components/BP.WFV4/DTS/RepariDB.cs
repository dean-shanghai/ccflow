using System;
using System.Collections;
using BP.DA;
using BP.Web.Controls;
using System.Reflection;
using BP.Port;
using BP.En;
namespace BP.WF
{
    /// <summary>
    /// 修复数据库 的摘要说明
    /// </summary>
    public class RepariDB : Method
    {
        /// <summary>
        /// 不带有参数的方法
        /// </summary>
        public RepariDB()
        {
            this.Title = "修复数据库";
            this.Help = "把最新的版本的与当前的数据表结构，做一个自动修复, 修复内容：缺少列，缺少列注释，列注释不完整或者有变化。";
        }
        /// <summary>
        /// 设置执行变量
        /// </summary>
        /// <returns></returns>
        public override void Init()
        {
            //this.Warning = "您确定要执行吗？";
            //HisAttrs.AddTBString("P1", null, "原密码", true, false, 0, 10, 10);
            //HisAttrs.AddTBString("P2", null, "新密码", true, false, 0, 10, 10);
            //HisAttrs.AddTBString("P3", null, "确认", true, false, 0, 10, 10);
        }
        /// <summary>
        /// 当前的操纵员是否可以执行这个方法
        /// </summary>
        public override bool IsCanDo
        {
            get
            {
                return true;
            }
        }
        /// <summary>
        /// 执行
        /// </summary>
        /// <returns>返回执行结果</returns>
        public override object Do()
        {
            string rpt = BP.PubClass.DBRpt(BP.DBLevel.High);

            // 手动升级. 2011-07-08 补充节点字段分组.
            string sql = "DELETE Sys_EnCfg WHERE No='BP.WF.Ext.NodeO'";
            BP.DA.DBAccess.RunSQL(sql);

            sql = "INSERT INTO Sys_EnCfg(No,GroupTitle) VALUES ('BP.WF.Ext.NodeO','NodeID=基本配置@WarningDays=考核属性@SendLab=功能按钮标签与状态')";
            BP.DA.DBAccess.RunSQL(sql);

            //删除表单类型.
            sql = "DELETE Sys_Enum WHERE EnumKey='FormType'";
            BP.DA.DBAccess.RunSQLs(sql);

            return "执行成功...";
        }
    }
}
