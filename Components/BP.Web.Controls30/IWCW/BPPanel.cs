using System;
//using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using BP.En;
using BP.DA;
using BP.Web;
using BP.Web.Controls;
 


namespace BP.Web.Controls
{
	/// <summary>
	/// BPListBox 的摘要说明。
	/// </summary>
	[System.Drawing.ToolboxBitmap(typeof(System.Web.UI.WebControls.Panel))]
	public class BPPanel:System.Web.UI.WebControls.Panel 
	{

		#region 设置
		/// <summary>
		/// 初始化map
		/// </summary>
		/// <param name="map">map</param>
		/// <param name="i">选择的页</param>
		public void InitByMapV2(Map map,int page) 
		{
			InitByMapV2(true,map.AttrsOfSearch,map.SearchAttrs,null, page);
		}
        public string EnsName
        {
            get
            {
                string val = this.Page.Request.QueryString["EnsName"];
                if (val == null)
                    val = this.Page.Request.QueryString["EnsName"];

                return val;
            }
        }
		public void InitByMapVGroup(Map map)
		{
		 
			//AttrOfSearch
			// 外键属性查询。		
	 
			Attrs attrs =map.SearchAttrs;
			bool isfirst=true;
			foreach(Attr attr in attrs )
			{
				if (attr.MyFieldType==FieldType.RefText)
					continue;

				DDL ddl = new DDL();
				ddl.ID="DDL_"+attr.Key;
				this.Add(ddl);  

				if (attr.MyFieldType==FieldType.Enum)
				{
					this.GetDDLByKey("DDL_"+attr.Key).BindSysEnum(attr.UIBindKey);
					this.GetDDLByKey("DDL_"+attr.Key).Items[0].Text="选择"+attr.Desc;
				}
				else
				{
                    switch (attr.UIBindKey)
                    {
                        case "BP.Port.Depts":
                            BP.Port.Depts Depts = new BP.Port.Depts();
                            Depts.RetrieveAll();
                            foreach (Entity zsjg in Depts)
                            {
                                ListItem li = new ListItem(zsjg.GetValStringByKey("Name"), zsjg.GetValStringByKey("No"));
                                this.GetDDLByKey("DDL_" + attr.Key).Items.Add(li);
                            }
                            break;
                        default:
                            this.GetDDLByKey("DDL_" + attr.Key).SelfBind();
                            this.GetDDLByKey("DDL_" + attr.Key).Items[0].Text = "选择" + attr.Desc;
                            break;
                    }
				}

				if (isfirst)
				{

					isfirst=false;
				}
			}
			this.Add("方式");
			DDL ddl1 = new DDL();
			ddl1.ID="DDL_ShowType";
			this.Add(ddl1);
			this.GetDDLByKey("DDL_ShowType").Items.Add( new ListItem("报表","rpt") );
			this.GetDDLByKey("DDL_ShowType").Items.Add( new ListItem("柱状图","a") );
			this.GetDDLByKey("DDL_ShowType").Items.Add( new ListItem("折线图","b") );
			this.GetDDLByKey("DDL_ShowType").Items.Add( new ListItem("饼图","c") );

			Btn btn= new Btn();
			btn.ID="Btn_Search";
            btn.Text = this.ToE("Search","查询");
			this.Add(btn);



			
			//this.AddBtn("Btn_Save","保存为模板");

			 

		}
        public string ToE(string no,string chval)
        {
            return BP.Sys.Language.GetValByUserLang(no,chval);
        }
		/// <summary>
		/// 根据Map, 构造一个ToolBar
		/// </summary>
		/// <param name="map"></param>
        public void InitByMapV2(bool isShowKey, AttrsOfSearch attrsOfSearch, AttrSearchs attrsOfFK, Attrs attrD1, int page) 
		{
			int keysNum=0;

			// 关键字。
			if (isShowKey)
			{
				this.Add(BP.Sys.Language.GetValByUserLang("Key","关键字") );
				TB tb = new TB();
				tb.ID = "TB_Key";
				tb.Columns=9;
				this.Add(tb);
				keysNum++;
			}

			//			BP.Sys.Operators ops = new BP.Sys.Operators();
			//			ops.RetrieveAll();

			// 非外键属性。			
			foreach(AttrOfSearch attr in  attrsOfSearch )
			{
				if (attr.IsHidden)
					continue;

				this.Add(attr.Lab);
				keysNum++;
				if (keysNum==3 || keysNum==6 || keysNum==9)
					this.Add("<BR>");


				if (attr.SymbolEnable==true)
				{
					DDL ddl = new DDL();
					ddl.ID = "DDL_"+attr.Key ;		 
					ddl.SelfShowType =DDLShowType.Ens ; //  attr.UIDDLShowType;		 
					ddl.SelfBindKey="BP.Sys.Operators" ; 
					ddl.SelfEnsRefKey ="No";
					ddl.SelfEnsRefKeyText ="Name";
					ddl.SelfDefaultVal =attr.DefaultSymbol;
					ddl.SelfAddAllLocation =AddAllLocation.None ; 
					ddl.SelfIsShowVal = false; ///不让显示编号
					ddl.ID="DDL_"+attr.Key;
					//ddl.SelfBind();
					this.Add(ddl);
					this.GetDDLByKey("DDL_"+attr.Key).SelfBind();
				}


				TB tb = new TB();
				tb.ID = "TB_"+attr.Key;
				tb.Text = attr.DefaultVal;
				tb.Columns=attr.TBWidth ; 
				this.Add(tb);
			}

			// 外键属性查询。			 
			bool isfirst=true;
			foreach(Attr attr in attrsOfFK)
			{
				if (attr.MyFieldType==FieldType.RefText)
					continue;
				DDL ddl  = new DDL();
				ddl.ID="DDL_"+attr.Key;
				ddl.SelfAddAllLocation=AddAllLocation.TopAndEnd;
				this.Add( ddl );

				keysNum++;
				if (keysNum==3 || keysNum==6 || keysNum==9)
					this.Add("<BR>");

                if (attr.MyFieldType == FieldType.Enum)
                {
                    this.GetDDLByKey("DDL_" + attr.Key).BindSysEnum(attr.UIBindKey);
                    this.GetDDLByKey("DDL_" + attr.Key).Items[0].Text = "选择" + attr.Desc;
                }
                else
                {
                    // 把所有的信息都Bind.
                    switch (attr.UIBindKey)
                    {
                        case "BP.Port.Depts":
                            BP.Port.Depts Depts = new BP.Port.Depts();
                            Depts.RetrieveAll();
                            foreach (Entity zsjg in Depts)
                            {
                                ListItem li = new ListItem(zsjg.GetValStringByKey("Name"), zsjg.GetValStringByKey("No"));
                                this.GetDDLByKey("DDL_" + attr.Key).Items.Add(li);
                            }
                            break;
                        default:
                            this.GetDDLByKey("DDL_" + attr.Key).SelfBind();
                            this.GetDDLByKey("DDL_" + attr.Key).Items[0].Text = "选择" + attr.Desc;
                            break;
                    }
                }

				if (isfirst)
				{
					isfirst=false;
				}
			}
			Btn btn = new Btn();
			btn.ID="Btn_Search";
            btn.Text = this.ToE("Search", "查询");
			this.Add( btn );

			DDL ddlPage = new DDL();
			ddlPage.ID = "DDL_Page";
			ddlPage.Items.Add(new ListItem("1","1"));
	
			this.Add(ddlPage);
			Btn btn1 = new Btn();
			btn1.ID="Btn_Go";
			btn1.Text="转到";
			this.Add( btn1 );
		}

		/// <summary>
		/// 得到一个QueryObject
		/// </summary>
		/// <param name="ens"></param>
		/// <param name="en"></param>
		/// <returns></returns>
		public QueryObject InitQueryObjectByEns(Entities ens,Entity en)
		{
			#region 关键字
			string keyVal="%"+this.GetTBByKey("TB_Key").Text.Trim()+"%";
			//int top=int.Parse(this.GetTBByKey("TB_Top").Text.Trim());
			QueryObject qo = new QueryObject(ens);
			//qo.Top=top;
			Attrs attrs = en.EnMap.Attrs;
            if (keyVal.Length > 2)
            {
                qo.addLeftBracket();
                string pk = ens.GetNewEntity.PK;
                if (pk == "OID" || pk.ToLower()=="workid" )
                    qo.AddHD_Not();
                else
                    qo.AddWhere(ens.GetNewEntity.PK, " LIKE ", "%"+keyVal+"%");

                foreach (Attr attr in attrs)
                {
                    if (attr.Key == "OID")
                        continue;

                    if (attr.MyDataType != DataType.AppString)
                        continue;

                    if (attr.MyFieldType == FieldType.RefText)
                        continue;

                    if (attr.UIContralType == UIContralType.DDL || attr.UIContralType == UIContralType.CheckBok)
                    {
                        if (attr.Key != "FK_Taxpayer")
                            continue;
                    }

                    qo.addOr();
                    qo.AddWhere(attr.Key, " LIKE ", keyVal);
                }
                qo.addRightBracket();
            }
            else
            {
                qo.addLeftBracket();
                qo.AddWhere("abc", "all");
                qo.addRightBracket();
            }
			#endregion

			#region 普通属性
			AttrsOfSearch attrsOfSearch = en.EnMap.AttrsOfSearch;

			string opkey=""; // 操作符号。
			foreach(AttrOfSearch attr in attrsOfSearch)
			{
				if (attr.IsHidden)
				{
					qo.addAnd();
					qo.addLeftBracket();
					qo.AddWhere(attr.RefAttrKey, attr.DefaultSymbol , attr.DefaultVal  ) ;
					qo.addRightBracket();
					continue;				
				}
				if (attr.SymbolEnable==true)
				{
					opkey=this.GetDDLByKey("DDL_"+attr.Key).SelectedItemStringVal;
					if (opkey=="all")
						continue;
				}
				else
				{
					opkey=attr.DefaultSymbol;
				}
 
				qo.addAnd();
				qo.addLeftBracket();
				qo.AddWhere(attr.RefAttrKey, opkey , this.GetTBByKey("TB_"+attr.Key).Text  ) ;
				qo.addRightBracket();
			}
			#endregion

			#region 外键
			Attrs searchAttrs = en.EnMap.SearchAttrs;
			foreach(Attr attr in searchAttrs)
			{
				if (attr.MyFieldType==FieldType.RefText) 
					continue;

				qo.addAnd();
				qo.addLeftBracket();

				if (attr.UIBindKey=="BP.Port.Depts")  //判断特殊情况。
					qo.AddWhere(attr.Key, " LIKE ", this.GetDDLByKey("DDL_"+attr.Key).SelectedItemStringVal+"%" ) ;
				else
					qo.AddWhere(attr.Key,this.GetDDLByKey("DDL_"+attr.Key).SelectedItemStringVal ) ;

				//qo.AddWhere(attr.Key,this.GetDDLByKey("DDL_"+attr.Key).SelectedItemStringVal ) ;
				qo.addRightBracket();
			}
			#endregion.

			return qo;

		}
		public int recordConut=0;

		public QueryObject InitTableByEnsV2OfOrcale(Entities ens,Entity en,int pageSize,int page)
		{
			QueryObject qo =this.InitQueryObjectByEns(ens,en);
			this.recordConut=qo.GetCount();
			int pageCount=recordConut/pageSize;
			int myleftCount= recordConut-(pageCount*pageSize);
			pageCount++;
			int top=pageSize*(page-1);
			this.GetDDLByKey("DDL_Page").Items.Clear();
			for(int i=1;i<=pageCount;i++)
			{
				this.GetDDLByKey("DDL_Page").Items.Add(new ListItem(i.ToString(),i.ToString()));
			}
			if (pageCount==0)
				this.GetDDLByKey("DDL_Page").Items.Add(new ListItem("1","1"));

			if (page==1)
			{
				qo.Top=pageSize;
				return qo;
			}

			

			this.GetDDLByKey("DDL_Page").SetSelectItemByIndex(page-1);
			string pk=en.PK;

			string wheresql=qo.SQL;
			string sql="";
 
			qo.addLeftBracket();

			qo.AddWhere("ROWID", " > " ,pageSize*page +pageSize  );
			qo.addAnd();
			qo.AddWhere("ROWID", " < " ,pageSize*page );

			qo.addRightBracket();



			qo.addAnd();

			qo.AddWhereNotInSQL(pk,sql);

			qo.Top=pageSize;

			//Log.DefaultLogWriteLineInfo(qo.SQL);

			return qo;
		}

		public QueryObject InitTableByEnsV2(Entities ens,Entity en,int pageSize,int page)
		{
			if (en.EnMap.EnDBUrl.DBType==DBType.Oracle9i)
				return this.InitTableByEnsV2OfOrcale(ens,en, pageSize,page);

			QueryObject qo =this.InitQueryObjectByEns(ens,en);
			this.recordConut=qo.GetCount();
			int pageCount=recordConut/pageSize;
			int myleftCount= recordConut-(pageCount*pageSize);

			pageCount++;

			int top=pageSize*(page-1);
			this.GetDDLByKey("DDL_Page").Items.Clear();

			for(int i=1;i<=pageCount;i++)
			{
				this.GetDDLByKey("DDL_Page").Items.Add(new ListItem(i.ToString(),i.ToString()));
			}
			if (pageCount==0)
				this.GetDDLByKey("DDL_Page").Items.Add(new ListItem("1","1"));


			if (page==1)
			{
				qo.Top=pageSize;
				return qo;
			}

			

			this.GetDDLByKey("DDL_Page").Items[page-1].Selected=true;
			string pk=en.PK;

			string wheresql=qo.SQL;
			string sql="";
			switch(DBAccess.AppCenterDBType)
			{
				case DBType.Oracle9i:
					sql="SELECT top "+top+" "+en.PKField+" FROM "+en.EnMap.PhysicsTable+" "+wheresql.Substring(wheresql.IndexOf("WHERE (1=1)")) ;
					break;
				default:
					sql="SELECT top "+top+" "+en.PKField+" FROM "+en.EnMap.PhysicsTable+" "+wheresql.Substring(wheresql.IndexOf("WHERE (1=1)")) ;
					break;
			}

			qo.addAnd();
			qo.AddWhereNotInSQL(pk,sql);
			qo.Top=pageSize;
			//Log.DefaultLogWriteLineInfo(qo.SQL);
			return qo;
		}

		#endregion


		#region  共用的
		public void Add(Control ctl)
		{
			this.Controls.Add(ctl);
		}
		public void Add(string html)
		{
			this.Controls.Add( new LiteralControl(html+"\n") );
		}
		public DDL GetDDLByKey(string key)
		{
			return (DDL)this.FindControl(key);
		}
		public TB GetTBByKey(string key)
		{
			return (TB)this.FindControl(key);
		}
		public Btn GetBtnByKey(string key)
		{
			return (Btn)this.FindControl(key);
		}
		#endregion 

		public BPPanel()
		{
			this.CssClass ="BPPanel"+WebUser.Style; 
			this.PreRender += new System.EventHandler(this.TBPreRender);
		}
		private void TBPreRender( object sender, System.EventArgs e )
		{
			#region
			#endregion 
		}
		/// <summary>
		/// 增加一个连接。
		/// </summary>
		/// <param name="text"></param>
		/// <param name="url"></param>
		/// <param name="target"></param>
		public void AddHyperLink(string text, string url, string target)
		{
			BPHyperLink hl = new BPHyperLink();
			hl.Text=text;
			hl.NavigateUrl=url;
			hl.Target=target;
			this.Controls.Add(hl) ; 
		}

		public void AddBtn(string id)
		{
			string text="" ;
			switch(id)
			{
				case NamesOfBtn.DataGroup:
					text="分组查询";
					break;	
				case NamesOfBtn.Copy:
					text="复制";
					break;	
				case NamesOfBtn.Go:
					text="转到";
					break;	
				case NamesOfBtn.ExportToModel:
					text="模板";
					break;	
				case NamesOfBtn.DataCheck:
					text="数据检查";
					break;					
				case NamesOfBtn.DataIO:
					text="数据导入";
					break;
				case NamesOfBtn.Statistic:
					text="统计";
					break;
				case NamesOfBtn.Balance:
					text="持平";
					break;
				case NamesOfBtn.Down:
					text="下降";
					break;
				case NamesOfBtn.Up:
					text="上升";
					break;
				case NamesOfBtn.Chart:
					text="图形";
					break;
				case NamesOfBtn.Rpt:
					text="报表";
					break;
				case NamesOfBtn.ChoseCols:
					text="选择列查询";
					break;
				case NamesOfBtn.Excel:
					text="导出";
					break;
				case NamesOfBtn.Xml:
					text="导出到Xml";
					break;				 
				case NamesOfBtn.Send:
					text="发送";
					break;
				case NamesOfBtn.Reply:
					text="回复";
					break;
				case NamesOfBtn.Forward:
					text="转发";
					break;
				case NamesOfBtn.Next:
					text="下一个";
					break;
				case NamesOfBtn.Previous:
					text="上一个";
					break;
				case NamesOfBtn.Selected:
					text="选择";
					break;
				case NamesOfBtn.Add:
					text="增加";
					break;
				case NamesOfBtn.Adjunct:
					text="附件";
					break;
				case NamesOfBtn.AllotTask:
					text="分批任务";
					break;
				case NamesOfBtn.Apply:
					text="申请";
					break;
				case NamesOfBtn.ApplyTask:
					text="申请任务";
					break;
				case NamesOfBtn.Back:
					text="后退";
					break;
				case NamesOfBtn.Card:
					text="卡片";
					break;
				case NamesOfBtn.Close:
					text="关闭";
					break;
				case NamesOfBtn.Confirm:
					text="确定";
					break;
				case NamesOfBtn.Delete:
					text="删除";
					break;
				case NamesOfBtn.Edit:
					text="编辑";
					break;
				case NamesOfBtn.EnList:
					text="列表";
					break;
				case NamesOfBtn.Cancel:
					text="取消";
					break;
				case NamesOfBtn.Export:
					text="导出";
					break;
				case NamesOfBtn.FileManager:
					text="文件管理";
					break;
				case NamesOfBtn.Help:
					text="帮助";
					break;
				case NamesOfBtn.Insert:
					text="插入";
					break;
				case NamesOfBtn.LogOut:
					text="注销";
					break;
				case NamesOfBtn.Messagers:
					text="消息";
					break;
				case NamesOfBtn.New:
					text="新建";
					break;
				case NamesOfBtn.Print:
					text="打印";
					break;
				case NamesOfBtn.Refurbish:
					text="刷新";
					break;
				case NamesOfBtn.Reomve:
					text="移除";
					break;
				case NamesOfBtn.Save:
					text="保存";
					break;
				case NamesOfBtn.SaveAndClose:
					text="保存并关闭";
					break;
				case NamesOfBtn.SaveAndNew:
					text="保存并新建";
					break;
				case NamesOfBtn.SaveAsDraft:
					text="保存草稿";
					break;
				case NamesOfBtn.Search:
					text="查找";
					break;
				case NamesOfBtn.SelectAll:
					text="选择全部";
					break;			 
				case NamesOfBtn.SelectNone:
					text="不选";
					break;
				case NamesOfBtn.View:
					text="查看";
					break;
				case NamesOfBtn.Update:
					text="更新";
					break;
				default:
					throw new Exception("@没有定义ToolBarBtn 标记 "+id);
			}
			Btn en = new Btn();
	 
			en.ID=id;
			en.Text=text;
			this.Add(en);		
		}

		public void AddHtml(string html)
		{
			this.Controls.Add( new LiteralControl( html) ); 
		}


		
		 

	}
	
}
