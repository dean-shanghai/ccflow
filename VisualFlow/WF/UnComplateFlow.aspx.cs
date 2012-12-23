using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using BP.WF;
using BP.DA;
using BP.En;

namespace BP.Web.WFQH.WF
{
	/// <summary>
	/// UnComplateFlow 的摘要说明。
	/// </summary>
	public partial class UnComplateFlow : WebPage
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
            this.Page.RegisterClientScriptBlock("s",
          "<link href='" + this.Request.ApplicationPath + "/Comm/Style/Table" + BP.Web.WebUser.Style + ".css' rel='stylesheet' type='text/css' />");


			string fk_flow = this.Request.QueryString["FK_Flow"] ; 

			//查询未完成的工作信息
			string sql="SELECT FK_FLOW, FK_Node AS FK_Node, COUNT(*) AS NUM "
				+"FROM WF_GENERWORKFLOW WHERE WORKID IN (SELECT WORKID FROM WF_GenerWorkerlist WHERE FK_EMP='"+WebUser.No+"' )"
				+"GROUP BY FK_FLOW, FK_Node";

			DataTable dt = DBAccess.RunSQLReturnTable(sql);
			Nodes nds = new Nodes(fk_flow);
			BP.WF.Flow fl = new BP.WF.Flow(fk_flow);

			this.UCSys1.Clear();
			this.UCSys1.AddTable();
            this.UCSys1.AddCaptionLeft(fl.Name);
			this.UCSys1.AddTR();
			this.UCSys1.AddTDTitle("工作步骤");
			this.UCSys1.AddTDTitle("停留工作节点名称");
			this.UCSys1.AddTDTitle("未完成个数");
			this.UCSys1.AddTREnd();

			foreach(Node nd in nds)
			{
				this.UCSys1.AddTR();
				this.UCSys1.AddTDIdx(nd.Step);
				this.UCSys1.AddTD(nd.Name);
				string wwc="0"; //未完成。
				foreach(DataRow dr in dt.Rows)
				{
					if (dr["FK_Node"].ToString() !=nd.NodeID.ToString())
						continue;
				 
					wwc= dr["Num"].ToString() ;
				}
				//string str="<a title='【"+nd.FlowName+"】中有【"+wwc+"】个工作已经流转到【"+nd.Name+"】节点上' href=\"javascript:alert('有"+wwc+"个工作停留在此工作结点上！');window.open('UnCompFlowDetails.aspx?FK_Flow="+nd.FK_Flow+"&FK_Node="+nd.NodeID+"&FK_Emp="+Web.WebUser.No+"&IsClose=1','newwin','width=750,top=300,left=300,height=300,scrollbars=yes,resizable=no,toolbar=false,location=false');newWindow.focus();\" >"+wwc+"</a>";
//				if(int.Parse(wwc)==0)
//				{
////					PubClass.Alert("目前没有工作停留在此工作节点上！");
//					this.UCSys1.AddTD("<a href=\"javascript:alert('有"+wwc+"个工作停留在此工作结点上！');\" >"+wwc+"</a>");
//				}
				this.UCSys1.AddTD("<a title='【"+nd.FlowName+"】中有【"+wwc+"】个工作已经流转到【"+nd.Name+"】节点上' href=\"javascript:alert('有"+wwc+"个工作停留在此工作结点上！');window.open('UnCompFlowDetails.aspx?FK_Flow="+nd.FK_Flow+"&FK_Node="+nd.NodeID+"&FK_Emp="+Web.WebUser.No+"&IsClose=1','newwin','width=750,top=300,left=300,height=300,scrollbars=yes,resizable=no,toolbar=false,location=false');newWindow.focus();\" >"+wwc+"</a>");
				this.UCSys1.AddTREnd();
			}
			this.UCSys1.AddTableEnd();
		}

		#region Web 窗体设计器生成的代码
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion
	}
}
