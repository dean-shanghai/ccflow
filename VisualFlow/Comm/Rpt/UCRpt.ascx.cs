namespace BP.Web.Comm.Rpt
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using BP.Rpt;
	using BP.En;
	using BP.DA;
	using BP.Sys;
	using BP.Web.Controls;
	using BP.Web.UC;

	/// <summary>
	/// UCRpt2D 的摘要说明。
	/// </summary>
	public partial class UCRpt : UCGraphics
	{

		#region 
		public void RptTemplateSet(Rpt3D rpt, RptTemplate rt)
		{
            //RptTemplates rts = new RptTemplates(rt.RptName);

            //this.Controls.Clear(); // cellpadding='0' cellspacing='0'
            //this.Add("<table border='1' class='OpTable'   >");
            //// 备选方案
            //this.Add("<TR>");
            //this.Add("<TD class='OpLeftTD' >备选方案</TD>");			
            //this.Add("<TD>");

            //DDL ddl = new DDL();
            //ddl.BindEntities(rts,RptTemplateAttr.OID, RptTemplateAttr.RptDesc);
            //this.Add(ddl);

            //this.Add("</TD>");
            //this.AddTREnd();

            //// 分析内容
            //this.Add("<TR>");
            //this.Add("<TD  class='OpLeftTD' >分析内容</TD>");
            //this.Add("<TD>");

            //int i=0;
            //foreach(AnalyseObj ao in rpt.HisAnalyseObjs)
            //{
            //    CheckBox cb1 = new CheckBox();
            //    cb1.ID="CB_AO_"+i.ToString();
            //    cb1.Text=ao.DataProperty;

            //    if (rt.AlObjs.IndexOf(ao.DataProperty+"@")!=-1)
            //    {
            //        cb1.Checked=true;
            //    }
            //    this.Add(cb1);
            //    i++;
            //}
            //this.Add("</TD>");
            //this.AddTREnd();


            //#region 分析指标
            //// 分析指标
            //this.Add("<TR>");
            //this.Add("<TD  class='OpLeftTD' >行指标</TD>");
            //this.Add("<TD>");

            //ddl = new DDL();
            //ddl.ID="DDL_D1";
            //foreach(Attr attr in rpt.DAttrs)
            //{
            //    ddl.Items.Add( new ListItem(attr.Desc, attr.Key) );
            //}
            //ddl.SetSelectItem(rt.D1);
            //this.Add(ddl);

            //this.Add("</TD>");

            //// 分析指标2
            //this.Add("<TR>");
            //this.Add("<TD  class='OpLeftTD' >列指标1</TD>");
            //this.Add("<TD>");

            //ddl = new DDL();
            //ddl.ID="DDL_D2";
            //foreach(Attr attr in rpt.DAttrs)
            //{
            //    ddl.Items.Add( new ListItem(attr.Desc, attr.Key) );
            //}
            //this.Add(ddl);
            //ddl.SetSelectItem(rt.D2);

            //this.Add("</TD>");
            //this.AddTREnd();

            //// 分析指标3
            //this.Add("<TR>");
            //this.Add("<TD  class='OpLeftTD' >列指标2</TD>");
            //this.Add("<TD>");
            //ddl = new DDL();
            //ddl.ID="DDL_D3";
            //foreach(Attr attr in rpt.DAttrs)
            //{
            //    ddl.Items.Add( new ListItem(attr.Desc, attr.Key) );
            //}
            //ddl.SetSelectItem(rt.D3);
            //this.Add(ddl);
            //this.Add("</TD>");
            //this.AddTREnd();
            //#endregion

            //// 比率显示 
            //this.Add("<TR>");
            //this.Add("<TD  class='OpLeftTD' >比率显示</TD>");
            //this.Add("<TD>");

            //RadioBtn rb = new RadioBtn();
            //rb.GroupName="RB_Rate";
            //rb.ID="RB_Rate0";
            //rb.Text="不显示";
            //if (rt.PercentModel==PercentModel.None)
            //    rb.Checked=true;
            //this.Add(rb);


            //rb = new RadioBtn();
            //rb.ID="RB_Rate1";
            //rb.GroupName="RB_Rate";
            //rb.Text="横向比";
            //if (rt.PercentModel==PercentModel.Transverse)
            //    rb.Checked=true;
            //this.Add(rb);

            //rb = new RadioBtn();
            //rb.ID="RB_Rate2";
            //rb.GroupName="RB_Rate";
            //rb.Text="纵向比";
            //if (rt.PercentModel==PercentModel.Vertical)
            //    rb.Checked=true;
            //this.Add(rb);

 
            //this.Add("</TD>");
            //this.AddTREnd();

            //// 合计显示 
            //this.Add("<TR>");
            //this.Add("<TD  class='OpLeftTD' >合计显示</TD>");
            //this.Add("<TD>");

            //CheckBox cb = new CheckBox();
            //cb.ID="CB_BigSum";
            //cb.Text="大合计";
            //cb.Checked=rt.IsSumBig;
            //this.Add(cb);

            //cb = new CheckBox();
            //cb.ID="CB_BigLittle";
            //cb.Text="小合计";
            //cb.Checked=rt.IsSumLittle;
            //this.Add(cb);

            //cb = new CheckBox();
            //cb.ID="CB_BigRight";
            //cb.Text="右合计";
            //cb.Checked=rt.IsSumRight;
            //this.Add(cb);

            //this.Add("</TD>");
            //this.AddTREnd();

            //// 图片属性
            //this.Add("<TR>");
            //this.Add("<TD  class='OpLeftTD' >图片属性</TD>");
            //this.Add("<TD>");
			
            //this.Add("宽度");
            //TextBox tb1 = new TextBox();
            //tb1.ID="TB_Width";
            //tb1.Columns=4;
            //tb1.Text=rt.Width.ToString();
            //this.Add(tb1);

            //this.Add("高度");
            //tb1 = new TextBox();
            //tb1.ID="TB_Height";
            //tb1.Columns=4;
            //tb1.Text=rt.Height.ToString();
            //this.Add(tb1);
 
            //this.Add("</TD>");
            //this.AddTREnd();

            //// 相关功能
            //this.Add("<TR>");
            //this.Add("<TD  class='OpLeftTD' >相关功能</TD>");
            //this.Add("<TD>");

            //this.Add("[<a href='../PanelEns.aspx?EnsName="+rpt.HisEns.ToString()+"'>分组查询</a>]");
            //this.Add("[<a href='../UIEnsCols.aspx?EnsName="+rpt.HisEns.ToString()+"'>选择列查询</a>]");
            //this.Add("[<a href='../GroupEnsMNum.aspx?EnsName="+rpt.HisEns.ToString()+"'>数据分组</a>]");
            //this.Add("</TD>");
            //this.AddTREnd();

            //// btn 
            //this.Add("<TR>");
            //this.Add("<TD colspan='2'  align='right' >");

            //Btn btn = new Btn();
            //btn.ID="Btn_Output";
            //btn.Text=" 导 出 ";
            //btn.Click+=new EventHandler(btn_Click);
            //this.Add(btn);
            //this.Add("&nbsp;&nbsp;");

            //btn = new Btn();
            //btn.ID="Btn_Print";
            //btn.Text=" 打 印 ";
            //btn.Click+=new EventHandler(btn_Click);
            //this.Add(btn);
            //this.Add("&nbsp;&nbsp;");


            //btn = new Btn();
            //btn.ID="Btn_App";
            //btn.Text=" 应 用 ";
            //btn.Click+=new EventHandler(btn_Click);
            //this.Add(btn);
            //this.Add("&nbsp;&nbsp;");


            //this.Add("</TD>");
            //this.AddTREnd();

            //this.Add("</Table>");
		}
		private void btn_Click(object sender, EventArgs e)
		{

		}

		public RptTemplate RptTemplateGet()
		{
			return new RptTemplate();
		}
		#endregion 

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
		}
		/// <summary>
		/// 显示报表。
		/// </summary>
		/// <param name="rpt">RptEntitiesNoNameWithNum</param>
		/// <param name="numName">numName</param>
		/// <param name="IsShowRate">是否显示百分比</param>
		public void BindRpt(RptEntitiesNoNameWithNum rpt, string numName, bool IsShowRate)
		{

			this.Text="";
			this.Text="<p align=center><b>"+rpt.Title+"</b></p>" ; 
			this.Text+="<hr>";
			this.Text+="<Table align=center border=1 class='Table'>" ;

			#region out 表格表头. 
			this.Text+="<TR  nowrap>";
			this.Text+="<TD class='D1'>编号</TD>";
			this.Text+="<TD class='D1'>名称</TD>";
			this.Text+="<TD class='D1'>"+numName+"</TD>";
			if (IsShowRate)
				this.Text+="<TD class='D1'>百分比</TD>";

			this.Text+="</TR>";
			#endregion 

			#region out body. 
			decimal f = 0;
			foreach(EntityNoName en1 in rpt.SingleDimensionItem1)
			{
				decimal obj =rpt.Rpt1DCells.GetCell(en1.No).valOfDecimal ;
				f +=obj;
				// 输出2纬  onmousedown='javascript:OnMousedown(this) '
				this.Text+="<TR   >";
				//this.Text+="<TR   >";
				this.Text+="<TD class='D2'>"+en1.No+"</TD>";
				this.Text+="<TD class='D2'>"+en1.Name+"</TD>";
				this.Text+="<TD class='Cell'>"+obj+"</TD>";
				if (IsShowRate)
					this.Text+="<TD class='Cell'>"+rpt.Rpt1DCells.GetRate(en1.No) +"</TD>";
				this.Text+="</TR>"; 
			}
			this.Text+="</TR>";
			this.Text+="<TR><tr><td   valign='top' colspan='2'class='D1' >合计</td>";

			
			this.Text+="<td>"+f.ToString("0.00")+"</td>";
			if (IsShowRate)
				this.Text+="<TD class='Cell'>--</TD>";

			this.Text+="</TR>";

			#endregion 

			this.Text+="</Table>";

			this.Text+="<p align=center>作者："+rpt.Author+"  日期:"+DA.DataType.CurrentDataTime+"</p>";
			this.ParseControl();
			//this.Response.Write(this.Text);
		}
		/// <summary>
		/// 数据Bind，
		/// </summary>
		/// <param name="rpt">3纬，数据实体。</param>
		/// <param name="IsShowRate">是否显示百分比</param>
		/// <param name="isShowRightSum">是否显示右边的合计。</param>
		/// <param name="isShowBottomSum">是否显示合计</param>
		/// <param name="isShowLittleSum">是否显示小计</param>
		public void BindRpt(Rpt3DEntity rpt, PercentModel pm, bool isShowRightSum, bool isShowBottomSum, bool isShowLittleSum)
		{
			string percentName="";
			if (pm==PercentModel.Transverse)
				percentName="横向比";
			else
				percentName="纵向比";

			//this.Text="";
			//this.Text="<p align=center><b>")+rpt.Title+"</b></p>\n";
			//this.Text+="<hr>\n";

			this.AddTable();

			//this.Text+="<Table align=left border=1 class='Table' style='border-collapse: collapse' bordercolor='#111111' >") ;

			//this.Text+="<Table align=center border=1 class='Table'>\n" ;

			#region out 表格表头.
			int rowspan=1;
			if ( pm!= PercentModel.None && isShowBottomSum) 
				rowspan=2; 
			
			this.Add("<TR class='D1'  nowrap>\n");
			this.Add("<TD colspan='2' rowspan="+rowspan+" >\n");
			this.Add(rpt.LeftTitle ) ;
			this.Add("</TD>\n" ) ;

			rowspan=1;
            if (pm != PercentModel.None && isShowBottomSum)
                rowspan = 2;
			 
			foreach(EntityNoName en in rpt.SingleDimensionItem1)
			{
				/* 纬度1标题 */
				this.Add("<TD class='D1' colspan='"+rowspan+"'  >");
				this.Add(rpt.GetD1Context(en) ) ;
				this.Add("</TD>\n" );
			}

			if (pm!= PercentModel.None && isShowBottomSum && isShowRightSum )			 
			{
				/* 如果要显示右边的核计。*/
				this.Add("<TD class='D1' colspan='"+rowspan+"'  >\n");
                this.Add(this.ToE("Sum","合计"));
				this.Add("</TD>\n");
			}
			else if ( pm!= PercentModel.None  && isShowBottomSum==false && isShowRightSum)
			{
				this.Add("<TD class='D1' colspan='"+rowspan+"'  >\n");
                this.Add(this.ToE("Sum","合计"));
				this.Add("</TD>\n");

				this.Add("<TD class='D1' colspan='"+rowspan+"'  >\n");
				this.Add(percentName);
				this.Add("</TD>\n");
			}
			else if (isShowRightSum)
			{
				this.Add("<TD class='D1' colspan='"+rowspan+"'  >\n");
                this.Add(this.ToE("Sum","合计"));
				this.Add("</TD>\n");
			}

			this.Add("</TR>\n" ) ;

			if (pm!= PercentModel.None && isShowBottomSum )
			{
				this.Add("<TR>\n");
				foreach(EntityNoName en in rpt.SingleDimensionItem1)
				{
					this.Add("<TD class='D1'  >");
					this.Add(rpt.DataProperty);
					this.Add("</TD>\n");
					this.Add("<TD class='D1'  >");
					this.Add( percentName) ;
					this.Add("</TD>\n");
				}

				if (isShowRightSum==true)
				{
					this.Add("<TD class='D1'  >");
					this.Add(rpt.DataProperty);
					this.Add("</TD>\n");

					this.Add("<TD class='D1'  >");
					this.Add(percentName);
					this.Add("</TD>\n");
				}
				this.Add("</TR>\n");
			}
			#endregion 

			#region out 表格left表头. 

			foreach(EntityNoName en2 in rpt.SingleDimensionItem2)
			{
				bool isfirst=true;				
				this.Add("<TR >\n"); // 输出2纬。

				#region 计算出来2d单元格的跨度。
				int count=0 ; 
				if (rpt.D2D3RefKey==null)
				{
					/* 如果d2,d3 没有任何关联。 他的跨度就是 3d 的个数 */
					count =rpt.SingleDimensionItem3.Count;
				}
				else
				{
					/* 如果d2,d3 有关联
					 * 就可以，找到的明细个数。
					 *  */
					foreach(Entity en in rpt.SingleDimensionItem3)
					{
						if (en.GetValStringByKey(rpt.D2D3RefKey)==en2.No)
						{
							count++;
						}
					}
				}
				if (isShowLittleSum )
				{ 
					/*如果要显示 2维单元格 合计*/
					count=count+1;
				}
				#endregion

				this.Add("  <TD class='D2' rowspan='"+count+"'  >");
				this.Add(rpt.GetD2Context(en2) );
				//this.Add(en2.Name+en2.No; //输出2纬度。
				this.Add("  </TD>\n");
				foreach(EntityNoName en3 in rpt.SingleDimensionItem3)
				{
					if (rpt.D2D3RefKey!=null)
					{
						/* 判断是不是2纬度3纬度关联。*/
						if (en3.GetValStringByKey(rpt.D2D3RefKey)!=en2.No)
							continue;
					}

					#region 输出3维
					// 输出3纬
					if (isfirst==false)
						this.Add("<TR>\n" ) ;
					this.Add("  <TD class='D3'  >");
					this.Add(rpt.GetD3Context(en3) ) ; 
					this.Add("  </TD>\n" ) ;

					decimal rightsum=0;
					foreach(EntityNoName en1 in rpt.SingleDimensionItem1)
					{
						this.Add("  <TD class='Cell'  >");
						Rpt3DCell cell =rpt.HisCells.GetCell(en1.No,en2.No,en3.No) ;
						decimal cellVal=cell.valOfDecimal ;  // decimal.Parse( cell.val.ToString() );
						rightsum+=cellVal;
						this.Add( rpt.GetCellContext(en1.No,en2.No,en3.No,rpt.HisADT ) );  //输出单元格。
						this.Add("  </TD>\n" );
						if (pm!= PercentModel.None && isShowBottomSum)
						{
							if (cellVal==0)
								this.Add("<TD  class='Rate'  > -- </TD>\n");
							else
							{
								if (pm==PercentModel.Vertical)
								{
									/*纵向的*/
									this.Add("<TD  class='Rate'  > "+DataType.GetPercent(cellVal,rpt.GetSumByD1(en1.No))+" </TD>\n" );
								}
								else
								{
									/*横向的*/
									//this.Add("<TD  class='Rate' > "+DataType.GetPercent(mysum,rpt.GetSumByD2(en1.No))+" </TD>\n";
									//this.Add("<TD  class='Rate' >  横向的 </TD>\n";
									this.Add("<TD  class='Rate'  > "+DataType.GetPercent( cellVal,rpt.GetSumByD2D3(en2.No,en3.No) )+" </TD>\n" );
								}
							}
						}
					}
					#endregion

					#region 如果显示 right 合计
					if (isShowRightSum)
					{
						this.Add("<TD class='RightSum'>\n" );

						switch(rpt.HisADT)
						{
							case AnalyseDataType.AppFloat:
								this.Add(rightsum.ToString() ) ;					 
								break;
							case AnalyseDataType.AppInt:
								this.Add(rightsum.ToString("0") );
								break;
							case AnalyseDataType.AppMoney:
								this.Add(rightsum.ToString("0.00") );
								break;
							default:
								break;
						}

						this.Add("</TD>\n");

						if ( pm!= PercentModel.None)
						{
							if (rightsum==0)
							{
								this.Add("<TD class='RightSum'   > -- </TD>\n" );
							}
							else
							{
								if (pm==PercentModel.Vertical)
								{
									this.Add("<TD class='RightSum'  >"+DataType.GetPercent(rightsum ,rpt.HisSum )+" </TD>\n" ) ;
								}
								else
								{
									this.Add("<TD class='RightSum'  >--</TD>\n");
								}
							}
						}
					}
					#endregion

					if (isfirst)
						isfirst=false;
					this.Add("</TR>\n");
				}  // 结束3纬度的遍历。`


				#region 输出小计
				if (isShowLittleSum)
				{
					this.Add("<TR>\n");
					this.Add("<TD class='LittleSum'  >小计</TD>");

					decimal mySum=0 ;
					foreach(EntityNoName en1 in rpt.SingleDimensionItem1 )
					{
						decimal endsum=0;
						foreach(EntityNoName en3 in rpt.SingleDimensionItem3)
						{
							if (rpt.D2D3RefKey!=null)
							{
								/* 判断是不是2纬度3纬度关联。*/
								if (en3.GetValStringByKey(rpt.D2D3RefKey)!=en2.No)
									continue;
							}
							Rpt3DCell cell =rpt.HisCells.GetCell(en1.No,en2.No,en3.No);
							endsum+=decimal.Parse( cell.val.ToString() );
						}

						switch(rpt.HisADT)
						{
							case AnalyseDataType.AppFloat:
								this.Add("<TD class='LittleSum'  >"+endsum+"</TD>");					 
								break;
							case AnalyseDataType.AppInt:
								this.Add("<TD class='LittleSum'  >"+endsum.ToString("0")+"</TD>");
								break;
							case AnalyseDataType.AppMoney:
								this.Add("<TD class='LittleSum'  >"+endsum.ToString("0.00")+"</TD>)" ) ;
								break;
							default:
								break;
						}


						if (isShowBottomSum && pm!= PercentModel.None)
						{
							if (endsum==0)							 
								this.Add("<TD class='LittleSum'  > -- </TD>");							 
							else 
							{
								if (pm==PercentModel.Vertical)
								{
									this.Add("<TD class='LittleSum'  > "+DataType.GetPercent(endsum ,rpt.GetSumByD1(en1.No))+" </TD>\n");
								}
								else if (pm==PercentModel.Transverse)
								{
									//this.Add("<TD class='LittleSum' > "+DataType.GetPercent(endsum ,rpt.GetSumByD2(en1.No))+" </TD>\n";
									this.Add("<TD class='LittleSum'  >  "+DataType.GetPercent(endsum ,rpt.GetSumByD2(en2.No))+"  </TD>\n");
								}
							}
						}

						mySum+=endsum ;
					}
					if ( isShowRightSum )
					{
						/* 显示右边的合计 */
						switch(rpt.HisADT)
						{
							case AnalyseDataType.AppFloat:
								this.Add("<TD class='LittleSum'  >"+mySum+"</TD>");					 
								break;
							case AnalyseDataType.AppInt:
								this.Add("<TD class='LittleSum'  >"+mySum.ToString("0")+"</TD>");
								break;
							case AnalyseDataType.AppMoney:
								this.Add("<TD class='LittleSum'   >"+mySum.ToString("0.00")+"</TD>");
								break;
							default:
								break;
						}

						if ( pm!= PercentModel.None )
						{
							/* 显示比率.*/
							if ( mySum==0)							 
								this.Add("<TD class='LittleSum' > -- </TD>" ) ;							 
							else
								this.Add("<TD class='LittleSum' > "+DataType.GetPercent(mySum ,rpt.HisSum )+" </TD>");
						}
					}
					this.Add("</TR>\n");
				}
				#endregion 输出小合计

				this.Add("</TR>\n" );
			}

			if (isShowBottomSum)
			{
				// 输出2纬.
				this.Add("<TR>" );
				this.Add(" <TD class='D1' colspan='2'  >" );
				this.Add(this.ToE("Sum","合计") );
				this.Add("</TD>" ) ;
				float x=0;
				float sum=0;
				foreach(EntityNoName en1 in rpt.SingleDimensionItem1)
				{
					x=rpt.HisCells.GetSumByPK1(en1.No);
					sum+=x;
					this.Add("<TD class='BottomSum' >" );

					switch(rpt.HisADT)
					{
						case AnalyseDataType.AppFloat:
							this.Add(x.ToString() );					 
							break;
						case AnalyseDataType.AppInt:
							this.Add(x.ToString("0") );
							break;
						case AnalyseDataType.AppMoney:
							this.Add(x.ToString("0.00") );
							break;
						default:
							break;
					}

					//this.Add(x.ToString();
					this.Add("</TD>" ) ;
					if (pm!= PercentModel.None)
					{
						if (pm==PercentModel.Vertical)
						{
							this.Add("<TD class='BottomSum' > -- </TD>\n" );
						}
						else
						{
							this.Add("<TD class='BottomSum' > "+DataType.GetPercent( (decimal)x, (decimal)rpt.HisCells.Sum ) +" </TD>\n");
						}
					}
				}
				if (isShowRightSum)
				{
					this.Add("<TD class='BottomSum'>" ) ;

					switch(rpt.HisADT)
					{
						case AnalyseDataType.AppFloat:
							this.Add(sum.ToString() ) ;					 
							break;
						case AnalyseDataType.AppInt:
							this.Add(sum.ToString("0") ) ;
							break;
						case AnalyseDataType.AppMoney:
							this.Add(sum.ToString("0.00") ) ;
							break;
						default:
							break;
					}

					//this.Add(sum.ToString();
					this.Add("</TD>" );
					if (pm!= PercentModel.None)
					{
						if (pm==PercentModel.Vertical)
							this.Add("<TD class='BottomSum' > -- </TD>\n");
						else
							this.Add("<TD class='BottomSum' > -- </TD>\n");
					}
				}
				this.AddTREnd();
			}
			#endregion 


			this.Add("</Table>");
			//this.Text+="<hr><p align=left>作者："+rpt.Author+"  日期:"+DA.DataType.CurrentDataTime+"</p>";
			//this.ParseControl();
		}
		/// <summary>
		/// RptPlanarEntity
		/// </summary>
		/// <param name="rpt">RptPlanarEntity</param>
		/// <param name="IsRowOrder">是否显示行次</param>
		/// <param name="isShowLefSum">是否显示左边的合计</param>
		/// <param name="isShowBottomSum">是否显示右边的合计</param>
		public void BindRpt(RptPlanarEntity rpt,bool IsRowOrder, bool isShowLefSum, bool isShowBottomSum, PercentModel pm )
		{
			string percentName="";
			if (pm==PercentModel.Transverse)
				percentName="横向比";
			else
				percentName="纵向比";

			//IsRowOrder=false;
			//this.Text="";
			//this.Text="<p align=center><b>"+rpt.Title+"</b></p>" ; 
			//this.Add("<hr>";

			this.AddTable();
			//this.Add("<Table id='tb1' align=left border=1 class='Table' style='border-collapse: collapse' bordercolor='#111111' >") ;

			#region out 表格表头. 
			 
			this.Add("<TR  id='trleft'  nowrap  >");
			if ( pm!=PercentModel.None )
				this.Add("<TD rowspan='2'  >项目</TD>");
			else
				this.Add("<TD rowspan='1'  >项目</TD>");

			if (IsRowOrder)
			{
				/* 行次 */				
				this.Add("<TD class='D1' nowrap >");
				this.Add("行次");
				this.Add("</TD>");
			}
			if ( pm!=PercentModel.None )
			{
				foreach(EntityNoName en in rpt.SingleDimensionItem1)
				{
					this.Add("<TD class='D1' colspan='2'  >");
					this.Add(en.Name);
					this.Add("</TD>");
				}
			}
			else
			{
				foreach(EntityNoName en in rpt.SingleDimensionItem1)
				{
					this.Add("<TD class='D1'  >");
					this.Add(en.Name);
					this.Add("</TD>");
				}
			}

			if ( pm!=PercentModel.None )
			{
				if (isShowLefSum)
				{
					/* 如果要显示左边的核计。*/
					this.Add("<TD colspan='2'>");
					this.Add(this.ToE("Sum","合计"));
					this.Add("</TD>");
				}
			}
			else
			{
				if (isShowLefSum)
				{
					/* 如果要显示左边的核计。*/
					this.Add("<TD>");
					this.Add(this.ToE("Sum","合计"));
					this.Add("</TD>");
				}
			}
			this.AddTREnd();
			if ( pm!=PercentModel.None )
			{
				this.Add("<TR>");
				foreach(EntityNoName en in rpt.SingleDimensionItem1)
				{
					//en;
					this.Add("<TD class='D1'   >"+rpt.DataProperty+"</TD>");
					this.Add("<TD class='D1'   >"+percentName+"</TD>");
				}

				this.Add("<TD class='D1' >"+rpt.DataProperty+"</TD>");
				this.Add("<TD class='D1' >"+percentName+"</TD>");
				this.AddTREnd();
			}
			#endregion 

			#region out 表格表头. 
			int i = 0;
			foreach(EntityNoName en2 in rpt.SingleDimensionItem2)
			{
				/* 遍历 2纬度 */
				i++;
				// 输出2纬 
				this.Add("<TR>");
				this.Add("<TD class='D2'   >");
				this.Add(en2.Name ) ;
				this.Add("</TD>");

				if (IsRowOrder)
				{
					// 是否显示行次 					 
					this.Add("<TD class='D2'   >");
					this.Add(i.ToString() ) ;
					this.Add("</TD>");
				}

				foreach(EntityNoName en1 in rpt.SingleDimensionItem1)
				{
					this.Add("<TD class='Cell'  >");
					this.Add(rpt.PlanarCells.GenerHtmlStrBy(rpt.Key1,en1.No, rpt.Key2 ,en2.No,rpt.HisADT) ) ;
					//this.Add(rpt.PlanarCells.GenerHtmlStrBy(en1.No,en2.No);
					this.Add("</TD>");

					if (pm==PercentModel.Vertical)
					{
						/* 纵向百分比 */
						this.Add("<TD class='Rate'  >"+rpt.PlanarCells.GetRateVertical(en1.No,en2.No).ToString("0.00%")+"</TD>");
					}
					else if (pm==PercentModel.Transverse)
					{
						/* 横向百分比 */
						this.Add("<TD class='Rate'  >"+rpt.PlanarCells.GetRateTransverse(en1.No,en2.No).ToString("0.00%")+"</TD>");
					}
				}
				if (isShowLefSum)
				{
					/*如果显示right的核计*/
					float sum =0 ;
					foreach(EntityNoName en1 in rpt.SingleDimensionItem1)
					{
						try
						{
							sum+=float.Parse( rpt.PlanarCells.GetCell(en1.No,en2.No).val.ToString() ) ;
						}
						catch
						{
							//string str=rpt.PlanarCells.GetCell(en1.No,en2.No).val.ToString();
							//	throw new Exception( str  ); 
						}
					}
					this.Add("<TD class='RightSum'   >");

					switch(rpt.HisADT)
					{
						case AnalyseDataType.AppFloat:
							this.Add( sum.ToString()  ) ;
							break;
						case AnalyseDataType.AppInt:
							this.Add(sum.ToString("0") ) ;	
							break;
						case AnalyseDataType.AppMoney:
							this.Add(sum.ToString("0.00") ) ;	
							break;
						default:
							throw new Exception("error rpt.hisADT");
					}

		 
					this.Add("</TD>");

					if (pm==PercentModel.Vertical)
					{
						/* 纵向百分比 */
						this.Add("<TD class='RightSum'  >"+rpt.PlanarCells.GetRateByPK2(en2.No).ToString("0.00%")+"</TD>");
					}
					else if (pm==PercentModel.Transverse)
					{
						/* 横向百分比 */
						this.Add("<TD class='RightSum'  >--</TD>");
					}
				}
				this.AddTREnd();
			}

			if (isShowBottomSum)
			{
				// 输出2纬  onmousedown=\"D2Down('Cell')\"
				this.Add("<TR  >");
				this.Add("<TD class='Sum' >");
				this.Add(this.ToE("Sum","合计") ) ;
				this.Add("</TD>");
				foreach(EntityNoName en1 in rpt.SingleDimensionItem1)
				{
					float x=0;
					float sumX=0;
					foreach(EntityNoName en2 in rpt.SingleDimensionItem2)
					{
						try
						{
							x+= rpt.PlanarCells.GetCell(en1.No,en2.No).valOfFloat;
						}
						catch
						{

						}
					}
					this.Add("<TD class='Sum'  >");

					switch(rpt.HisADT)
					{
						case AnalyseDataType.AppFloat:
							this.Add(x.ToString() ) ;
							break;
						case AnalyseDataType.AppInt:
							this.Add(x.ToString("0") ) ;	
							break;
						case AnalyseDataType.AppMoney:
							this.Add(x.ToString("0.00") ) ;
							break;
						default:
							throw new Exception("error rpt.hisADT x");
					}

					this.Add("</TD>");

					if (pm == PercentModel.Vertical )
					{
						/* 如果是横向 */
						this.Add("<TD  >--</TD>");
					}
					else if (pm==PercentModel.Transverse)
					{
						/* 如果是纵向 */
						float cel= x/rpt.PlanarCells.SumOfFloat;
						this.Add("<TD class='RightSum'  >"+cel.ToString("0.00%") +"</TD>");
					}
				}
				if (isShowLefSum)
				{
					this.Add("<TD class='Sum'>");

					switch(rpt.HisADT)
					{
						case AnalyseDataType.AppFloat:
							this.Add(rpt.PlanarCells.SumOfDecimal.ToString() ) ;
							break;
						case AnalyseDataType.AppInt:							
							this.Add(rpt.PlanarCells.SumOfInt.ToString() ) ;
							break;
						case AnalyseDataType.AppMoney:
							this.Add(rpt.PlanarCells.SumOfDecimal.ToString("0.00") );
							break;
					}

					this.Add("</TD>");

					if (pm == PercentModel.Transverse)
					{
						/* 横向比 */
						this.Add("<TD  >--</TD>");
						//this.Add("<TD   >--</TD>");
					}
					else if (pm == PercentModel.Vertical)
					{
						this.Add("<TD  >--</TD>");
					}
				}
				this.AddTREnd();
			}
			#endregion

			this.Add("</Table>");

			//this.Text+="<p align=center>作者："+rpt.Author+"  日期:"+DA.DataType.CurrentDataTime+"</p>";
			//this.ParseControl();

			//this.Response.Write(this.Text);
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
		///		设计器支持所需的方法 - 不要使用代码编辑器
		///		修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion

		
	}
}
