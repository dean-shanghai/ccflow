using System;
using System.Web;
using BP.En;
using BP.En;
using BP.DA;
using System.Collections;
using System.Data;
using BP.Port;
using BP.Sys;

namespace BP.WF
{
	/// <summary>
	/// WFPubClass 的摘要说明。
	/// </summary>
	public class WFPubClass
	{
		/// <summary>
		/// 向一个工作节点发送消息
		/// </summary>
		/// <param name="wns"></param>
		/// <param name="title"></param>
		/// <param name="docs"></param>
		public static void SendMsg(WorkNodes wns, string title , string docs)
		{
			return ;

//#warning 消息发送
			/*
			//return ;
			Emps emps = new Emps();
			foreach(WorkNode wn in wns)			 
				emps.AddEntity(wn.HisWork.HisRec);
			int workId=wns[0].HisWork.OID;
			string flowNo =wns[0].HisNode.FK_Flow;
			SysMsg msg = new SysMsg();
			msg.Title= title;
			msg.MsgID=workId;
			msg.FromEmp=Web.WebUser.No;
			msg.ToEmp=BP.EnExt.GetEnsString(emps,"No",",");
			msg.Doc=docs +"<p>关于工作的更详细信息请看<a  href=\"../../WF/WFRpt.aspx?FK_Flow="+flowNo+"&WorkID="+workId+"\" target=rpt >工作报告</a></p>";
			msg.Save();
			*/
		}

		/// <summary>
		/// 默认的工作ID authorize
		/// </summary>
        public static Int64 DefaultWorkID
		{
			get
			{
				if (System.Web.HttpContext.Current.Session["DefaultWorkID"]==null)
					return 0 ;
                Int64 val = (Int64)System.Web.HttpContext.Current.Session["DefaultWorkID"];
				//DefaultWorkID=0;
				return val;
			}
			set
			{
				System.Web.HttpContext.Current.Session["DefaultWorkID"]=value;
			}
		}
		/// <summary>
		/// 默认的审核金额
		/// </summary>
		public static float DefaultCheckNum
		{
			get
			{
				if (System.Web.HttpContext.Current.Session["DefaultCheckNum"]==null)
					return 0 ; 
				float val= (float)System.Web.HttpContext.Current.Session["DefaultCheckNum"];
				//DefaultCheckNum=0;
				return val;
			}
			set
			{
				System.Web.HttpContext.Current.Session["DefaultCheckNum"]=value;
			}
		}
		/// <summary>
		/// 参考消息
		/// </summary>
		public static string DefaultRefMsg
		{
			get
			{
				if (System.Web.HttpContext.Current.Session["DefaultRefMsg"]==null)
					return ""; 
				string val= (string)System.Web.HttpContext.Current.Session["DefaultRefMsg"];
				//DefaultRefMsg="";
				return val;
			}
			set
			{
				System.Web.HttpContext.Current.Session["DefaultRefMsg"]=value;
			}
		}	 
	}
}
