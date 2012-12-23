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
using BP.Web;

namespace BP.Web.App
{
	/// <summary>
	/// Home 的摘要说明。
	/// </summary>
	public partial class Head1 : System.Web.UI.Page
	{
        protected void Page_Load(object sender, System.EventArgs e)
        {
            this.Ucsys1.Add("<TABLE height='100%' width='100%' border=0>");
            this.Ucsys1.Add("<TR height='100%' width='100%' valign=top  >");
            this.Ucsys1.Add("<TD valign='top' height='100%' width='20%' rowspan='2' >");
            this.Ucsys1.Add("</TD><TD height='50%' width='80%' valign=top >");
            this.Ucsys1.Add("<FONT style='FONT-SIZE: 20pt; FILTER: shadow(color=ActiveBorder); width: 100%; color: yellow; LINE-HEIGHT: 100%; FONT-FAMILY: 隶书'>");
            this.Ucsys1.Add("<B>" + SystemConfig.CustomerName + "&nbsp;&nbsp;" + SystemConfig.SysName + "</b>");
            this.Ucsys1.Add("</FONT></TD>");
            this.Ucsys1.Add("</TR>");
            this.Ucsys1.Add("</TABLE>");
            return;
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
