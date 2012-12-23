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

namespace BP.Web.PG.App.Port
{
	/// <summary>
	/// BarOfLeftMsg 的摘要说明。
	/// </summary>
	public partial class BarOfLeftMsg : System.Web.UI.Page
	{
        protected void Page_Load(object sender, System.EventArgs e)
        {
            return;
            string path = this.Request.ApplicationPath;
            string str = "";
            if (WebUser.No == null)
            {
                str = "<img  src='OnlineUser.gif' border='0'   ><font color='#000000' size='1' ><a href=\"SignInOfRe.aspx?IsChangeUser=1\" target='mainfrm' >登陆时间太长需要您重新登录</a></font>";
            }
            else
            {
                str = "<a href=\"SignInOfRe.aspx\"  target='mainfrm' ><img src='Img/OnlineUser.gif'  border='0' /><font color='#000000' size='1' >&nbsp;切换" + WebUser.Name + "</a>"; //"<a href=\"javascript: WinOpen('"+path+"/Comm/Port/OnlineUsers.aspx','online' ) ; \" >在线("+OnlineUserManager.GetUserCount()+")</font></a>";
            }
            this.Response.Write(str );
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
