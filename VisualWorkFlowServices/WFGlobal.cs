using System;
using System.Collections;
using System.Windows.Forms;
using System.Drawing;

namespace BP.Win.WF
{
	/// <summary>
	/// WFGlobal 的摘要说明。
	/// </summary>
	public class Global
	{
        static Global()
		{  
		}
		private static BP.Port.Emp _loademp= null;		
		public  static BP.Port.Emp LoadEmp
		{
			get
			{
				return _loademp;
			}
			set
			{
				_loademp = value;
				if( value!=null)
				{
//					BP.Portal.Win.Current.DeptNo = WFGlobal.LoadEmp.DeptNo;
//					BP.Portal.Win.Current.IsOpenMenu = WFGlobal.LoadEmp.IsOpenMenu;
//					BP.Portal.Win.Current.LastEnterDate = WFGlobal.LoadEmp.LastEnterDate;
//					BP.Portal.Win.Current.Style = WFGlobal.LoadEmp.Style;
//					BP.Portal.Win.Current.TeamNo = WFGlobal.LoadEmp.TeamNo;
//					BP.Portal.Win.Current.UnitName = WFGlobal.LoadEmp.UnitNo;
//					BP.Portal.Win.Current.UnitNo = WFGlobal.LoadEmp.UnitNo;
//					//BP.Portal.Win.Current.UnitNoInHisSystem = ;
//					BP.Portal.Win.Current.UserName = WFGlobal.LoadEmp.Name;
//					BP.Portal.Win.Current.UserNo = WFGlobal.LoadEmp.No;
//					BP.Portal.Win.Current.UserOID = WFGlobal.LoadEmp.OID;
					//BP.Portal.Win.Current.UserType = ;
				}
			}
		}

		private static Cursor[] _toolCursors;
		public  static Cursor[] ToolCursors
		{
			get
			{
				return _toolCursors;
			}
			set
			{
				_toolCursors = value;
			}
		}
		private static Cursor[] _nodeCursors;
		public  static Cursor[] NodeCursors
		{
			get
			{
				return _nodeCursors;
			}
			set
			{
				_nodeCursors = value;
			}
		}
		public static Cursor CurrentToolCursor
		{
			get
			{
                if (Global.ToolIndex < 0)
                    return Global._toolCursors[0];
                return Global._toolCursors[Global.ToolIndex];
			}
		}
        public static BP.WF.Node _copyNode = null;
		private static int toolIndex = 0;
        /// <summary>
        /// 将要copy的节点
        /// </summary>
        /// <summary>
        /// 0 , 表示鼠标
        /// 1 , 普通节点
        /// 2， 审核节点。
        /// 3，连接线。
        /// 4， 标签。
        /// </summary>
		public static  int ToolIndex
		{
			get
			{
				return toolIndex;
			}
            set
            {
                toolIndex = value;
              
                SetPushButton(value);
            }
		}

		 
		public static Point GetCenterPoint(Point p1 ,Point p2)
		{
			Point p = new Point();			
			p.X = (p2.X - p1.X);
			p.Y = (p2.Y - p1.Y);

			return p;
		}

		private static string _flowImagePath = Application.StartupPath + "\\FlowImage\\";
        public static string FlowImagePath
        {
            get
            {
                return _flowImagePath;
            }
            set
            {
                _flowImagePath = value;
            }
        }
	}
}
