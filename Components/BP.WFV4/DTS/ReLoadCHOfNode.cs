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
    public class ReLoadCHOfNode : Method
    {
        /// <summary>
        /// 不带有参数的方法
        /// </summary>
        public ReLoadCHOfNode()
        {
            this.Title = "重新生成每个节点工作的考核数据放入WF_CHOfNode表里";
            this.Help = "如果数据量较大有可能在web程序上执行失败。";
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
        /// 
        /// </summary>
        public void DoFlowData()
        {
        }
        /// <summary>
        /// 执行
        /// </summary>
        /// <returns>返回执行结果</returns>
        public override object Do()
        {
            string msg = "";
            Flows fls = new Flows();
            fls.RetrieveAllFromDBSource();
            try
            {
                DBAccess.RunSQL("DROP TABLE WF_CHOfNode");
            }
            catch
            {
            }

            CHOfNode ndData = new CHOfNode();
            ndData.CheckPhysicsTable();

            DataTable dt = null;
            string sql = "";
            foreach (Flow fl in fls)
            {
                Nodes nds = fl.HisNodes;
                foreach (Node nd in nds)
                {
                    try
                    {
                        sql = "SELECT OID,FID,RDT,CDT,Rec,Emps FROM ND" + nd.NodeID + " WHERE NodeState=1 AND OID NOT IN(SELECT WorkID FROM WF_CHOfNode WHERE FK_Node=" + nd.NodeID + ")";
                        dt = DBAccess.RunSQLReturnTable(sql);
                    }
                    catch (Exception ex)
                    {
                        msg += ex.Message;
                        continue;
                    }

                    foreach (DataRow dr in dt.Rows)
                    {
                        ndData.MyPK = nd.NodeID + "_" + dr["OID"].ToString();
                        ndData.FK_Node = nd.NodeID;
                        ndData.FK_Flow = nd.FK_Flow;
                     

                        ndData.WorkID = Int64.Parse(dr["OID"].ToString());
                        ndData.FK_Emp = dr["Rec"].ToString();
                        try
                        {
                            ndData.FK_Dept = DBAccess.RunSQLReturnString("SELECT FK_Dept FROM Port_Emp WHERE No='" + ndData.FK_Emp + "'");
                        }
                        catch (Exception ex)
                        {
                           // msg += ex.Message;
                        }

                        ndData.RDT = dr["RDT"].ToString();
                        ndData.CDT = dr["CDT"].ToString();
                        ndData.FK_NY = dr["RDT"].ToString().Substring(0, 7);

                        // 求应完成日期.
                        DateTime dtOfShould;
                        int day = 0;
                        if (nd.DeductDays < 1)
                            day = 0;
                        else
                            day = (int)nd.DeductDays;
                        dtOfShould = DataType.AddDays(DataType.ParseSysDate2DateTime(ndData.RDT), day);
                        ndData.SDT = dtOfShould.ToString("yyyy-MM-dd");

                        ndData.SpanDays = DataType.GetSpanDays(ndData.RDT, ndData.CDT); /*完成天数*/
                        ndData.NodeDeductDays = (int)nd.DeductDays; /*需要完成天数*/
                        ndData.NodeDeductCent = nd.DeductCent; /*每延期一天扣分*/
                        ndData.NodeMaxDeductCent = (int)nd.MaxDeductCent; /*最大工作扣分*/
                        ndData.NodeSwinkCent = (int)nd.SwinkCent; /*工作加分*/
                        ndData.CentOfAdd = (int)nd.SwinkCent;
                        if (ndData.SpanDays <= ndData.NodeDeductDays)
                        {
                            ndData.CentOfCut = 0;
                        }
                        else
                        {
                            /*计算扣分*/
                            float cent = (ndData.SpanDays - ndData.NodeDeductDays) * nd.DeductCent;
                            ndData.CentOfCut = cent;
                        }
                        ndData.Cent = ndData.CentOfAdd - ndData.CentOfCut;
                        ndData.Insert();
                    }
                }
            }
            return msg+"\t\n -执行完成.....";
        }
    }
}
