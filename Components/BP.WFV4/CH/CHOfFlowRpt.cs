using System;
using BP.Port;
using System.Collections;
using BP.DA;
using System.Data;
using BP.Sys;
using BP.En;
using BP.Rpt;
using BP.Web;
using BP.WF;

namespace BP.WF
{
	/// <summary>
	/// 流程量化分析
	/// </summary>
	public class CHOfFlowRpt : Rpt3D
	{
		/// <summary>
		/// 流程量化分析
		/// </summary>
        public CHOfFlowRpt()
        {
            #region 报表的基本属性
            this.HisEns = new BP.WF.CHOfFlows();
            this.Title = "流程分析";
            this.DataProperty = "个数";
            this.IsShowRate = true; //是否显示百分比。
            this.IsShowSum = true;  //是否显示合计。
            // 设置 计算列。default  COUNT(*)
            //this.OperationColumn="AVG(ZCZJ)";
            #endregion

            #region 设置普同字段查询条件
            this.IsShowSearchKey = true;
            //this.HisAttrsOfSearch.AddFromTo("记录日期",CHOfFlowAttr.RDT, DateTime.Now.AddMonths(-12).ToString("yyyy-MM-dd"), DA.DataType.CurrentData, 7 );
            #endregion

            #region 设置外键查询条件
            this.AddFKSearchAttrs(CHOfFlowAttr.WFState);
            this.AddFKSearchAttrs(CHOfFlowAttr.FK_Emp);
            this.AddFKSearchAttrs(CHOfFlowAttr.FK_Dept);
            #endregion

            #region 设置纬度属性
            
                this.AddDAttrByKey(CHOfFlowAttr.FK_Dept);

            this.AddDAttrByKey(CHOfFlowAttr.FK_NY);
            this.AddDAttrByKey(CHOfFlowAttr.FK_Dept);
            this.AddDAttrByKey(CHOfFlowAttr.FK_Emp);
            #endregion

            #region 设置默认的纬度属性， 让用户进入就可以使用它.
            this.AttrOfD1 = CHOfFlowAttr.FK_NY;

            this.AddDAttrByKey(CHOfFlowAttr.FK_Flow);
            
                this.AttrOfD2 = CHOfFlowAttr.FK_Dept;
                this.AttrOfD3 = CHOfFlowAttr.FK_Dept;
            #endregion
        } 
	}
}
