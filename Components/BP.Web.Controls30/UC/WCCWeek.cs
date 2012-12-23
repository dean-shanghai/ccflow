using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace BP.Web.Controls
{
	/// <summary>
	/// WebCustomControl1 的摘要说明。
	/// </summary>
	[DefaultProperty("Text"), 
		ToolboxData("<{0}:WebCustomControl1 runat=server></{0}:WebCustomControl1>")]
	public class WebCustomControl1 : System.Web.UI.WebControls.WebControl
	{
		private string text;
	
		[Bindable(true), 
		Category("Appearance"), 
		DefaultValue("")] 
		public string Text 
		{
			get
			{
				return text;
			}
			set
			{
				text = value;
			}
		}
		/// <summary> 
		/// 将此控件呈现给指定的输出参数。
		/// </summary>
		/// <param name="output"> 要写出到的 HTML 编写器 </param>
        protected override void Render(HtmlTextWriter output)
        {
            output.Write(Text);
        }
	}
}
