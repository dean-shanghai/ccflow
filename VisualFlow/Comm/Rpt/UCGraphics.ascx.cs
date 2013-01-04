namespace BP.Web.UC
{
	using System;
	using System.Collections;
	using System.ComponentModel;
	using System.Data;
	using System.Data.SqlClient;
	using System.Data.Odbc ;
	using System.Drawing;
	using System.Web;
	using System.Web.SessionState;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using BP.Rpt ; 
	using BP.En;
	using BP.DA;
    //using Microsoft.Office;
    //using Microsoft.Office.Interop;
    //using Microsoft.Web.UI.WebControls;
    using Microsoft.Office.Interop.Owc11;
    //using OWC10;

	/// <summary>
	///		UCGraphics 的摘要说明。
	/// </summary>
	public partial class UCGraphics : UCBase
	{

		#region 3 d
		public void BindPie(Rpt3DEntity rpt , int chartWidth,int chartHeight)
		{
			Bind(rpt,ChartChartTypeEnum.chChartTypePie3D, chartWidth,chartHeight);
 		}
		public void BindLine(Rpt3DEntity rpt , int chartWidth,int chartHeight)
		{
			Bind(rpt,ChartChartTypeEnum.chChartTypeLineStacked, chartWidth,chartHeight);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="rpt"></param>
		/// <param name="chartWidth"></param>
		/// <param name="chartHeight"></param>
		public void BindHistogram(Rpt3DEntity rpt , int chartWidth,int chartHeight)
		{
			//Bind(rpt,ChartChartTypeEnum.chChartTypeColumn3D, chartWidth,chartHeight);
			//Bind(rpt,ChartChartTypeEnum.chChartTypeColumnStacked, chartWidth,chartHeight);
			//Bind(rpt,ChartChartTypeEnum.chChartTypeColumnStacked100, chartWidth,chartHeight);
			Bind(rpt,ChartChartTypeEnum.chChartTypeColumnClustered, chartWidth,chartHeight);
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="colOfGroupField"></param>
        /// <param name="colOfGroupName"></param>
        /// <param name="colOfNumField"></param>
        /// <param name="colOfNumName"></param>
        /// <param name="title"></param>
        /// <param name="chartHeight"></param>
        /// <param name="chartWidth"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public static string GenerChart(DataTable dt, string colOfGroupField, string colOfGroupName,
            string colOfNumField, string colOfNumName, string title, int chartHeight, int chartWidth, ChartType ct)
        {
            string strCategory = ""; // "1" + '\t' + "2" + '\t' + "3" + '\t'+"4" + '\t' + "5" + '\t' + "6" + '\t';
            string strValue = ""; // "9" + '\t' + "8" + '\t' + "4" + '\t'+"10" + '\t' + "12" + '\t' + "6" + '\t';
            //声明对象
            ChartSpace ThisChart = new ChartSpaceClass();
            ChChart ThisChChart = ThisChart.Charts.Add(0);
            ChSeries ThisChSeries = ThisChChart.SeriesCollection.Add(0);

            //显示图例
            ThisChChart.HasLegend = true;
            //标题
            ThisChChart.HasTitle = true;
            ThisChChart.Title.Caption = title;

            //给定x,y轴图示说明
            ThisChChart.Axes[0].HasTitle = true;
            ThisChChart.Axes[1].HasTitle = true;

            ThisChChart.Axes[0].Title.Caption = colOfGroupName;
            ThisChChart.Axes[1].Title.Caption = colOfNumName;

            switch (ct)
            {
                case ChartType.Histogram:
                    foreach (DataRow dr in dt.Rows)
                    {
                        strCategory += dr[colOfGroupField].ToString() + '\t';
                        strValue += dr[colOfNumField].ToString() + '\t';
                    }
                    ThisChChart.Type = ChartChartTypeEnum.chChartTypeColumnClustered;
                    ThisChChart.Overlap = 50;
                    //旋转
                    ThisChChart.Rotation = 360;
                    ThisChChart.Inclination = 10;
                    //背景颜色
                    ThisChChart.PlotArea.Interior.Color = "red";
                    //底色
                    ThisChChart.PlotArea.Floor.Interior.Color = "green";
                    ////给定series的名字
                    ThisChSeries.SetData(ChartDimensionsEnum.chDimSeriesNames,
                        ChartSpecialDataSourcesEnum.chDataLiteral.GetHashCode(), colOfGroupName);
                    //给定分类
                    ThisChSeries.SetData(ChartDimensionsEnum.chDimCategories,
                        ChartSpecialDataSourcesEnum.chDataLiteral.GetHashCode(), strCategory);
                    //给定值
                    ThisChSeries.SetData(ChartDimensionsEnum.chDimValues,
                        ChartSpecialDataSourcesEnum.chDataLiteral.GetHashCode(), strValue);
                    break;
                case ChartType.Pie:
                    // 产生数据
                    foreach (DataRow dr in dt.Rows)
                    {
                        strCategory += dr[colOfGroupField].ToString() + '\t';
                        strValue += dr[colOfNumField].ToString() + '\t';
                    }

                    ThisChChart.Type = ChartChartTypeEnum.chChartTypePie3D;
                    ThisChChart.SeriesCollection.Add(0);
                    //在图表上显示数据
                    ThisChChart.SeriesCollection[0].DataLabelsCollection.Add();
                    ThisChChart.SeriesCollection[0].DataLabelsCollection[0].Position = ChartDataLabelPositionEnum.chLabelPositionAutomatic;
                    ThisChChart.SeriesCollection[0].Marker.Style = ChartMarkerStyleEnum.chMarkerStyleCircle;

                    //给定该组图表数据的名字 
                    ThisChChart.SeriesCollection[0].SetData(ChartDimensionsEnum.chDimSeriesNames,
                        +(int)ChartSpecialDataSourcesEnum.chDataLiteral, "strSeriesName");

                    //给定数据分类 
                    ThisChChart.SeriesCollection[0].SetData(ChartDimensionsEnum.chDimCategories,
                        +(int)ChartSpecialDataSourcesEnum.chDataLiteral, strCategory);

                    //给定值 
                    ThisChChart.SeriesCollection[0].SetData(ChartDimensionsEnum.chDimValues,
                        (int)ChartSpecialDataSourcesEnum.chDataLiteral, strValue);
                    break;
                case ChartType.Line:
                    // 产生数据
                    foreach (DataRow dr in dt.Rows)
                    {
                        strCategory += dr[colOfGroupField].ToString() + '\t';
                        strValue += dr[colOfNumField].ToString() + '\t';
                    }
                    ThisChChart.Type = ChartChartTypeEnum.chChartTypeLineStacked;
                    ThisChChart.SeriesCollection.Add(0);
                    //在图表上显示数据
                    ThisChChart.SeriesCollection[0].DataLabelsCollection.Add();
                    //ThisChChart.SeriesCollection[0].DataLabelsCollection[0].Position=ChartDataLabelPositionEnum.chLabelPositionAutomatic;
                    //ThisChChart.SeriesCollection[0].DataLabelsCollection[0].Position=ChartDataLabelPositionEnum.chLabelPositionOutsideBase;

                    ThisChChart.SeriesCollection[0].Marker.Style = ChartMarkerStyleEnum.chMarkerStyleCircle;

                    //给定该组图表数据的名字 
                    ThisChChart.SeriesCollection[0].SetData(ChartDimensionsEnum.chDimSeriesNames,
                        +(int)ChartSpecialDataSourcesEnum.chDataLiteral, "strSeriesName");

                    //给定数据分类 
                    ThisChChart.SeriesCollection[0].SetData(ChartDimensionsEnum.chDimCategories,
                        +(int)ChartSpecialDataSourcesEnum.chDataLiteral, strCategory);

                    //给定值 
                    ThisChChart.SeriesCollection[0].SetData(ChartDimensionsEnum.chDimValues,
                        (int)ChartSpecialDataSourcesEnum.chDataLiteral, strValue);
                    break;
            }

            //导出图像文件
            //ThisChart.ExportPicture("G:\\chart.gif","gif",600,350);

            string fileName = ct.ToString() + PubClass.GenerTempFileName("GIF");
            string strAbsolutePath = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "\\Temp\\" + fileName;
            try
            {
                ThisChart.ExportPicture(strAbsolutePath, "GIF", chartWidth, chartHeight);
            }
            catch (Exception ex)
            {
                throw new Exception("@不能创建文件,可能是权限的问题，请把该目录设置为任何人都可以修改。" + strAbsolutePath + " Exception:" + ex.Message);
            }
            return fileName;
        }
		/// <summary>
		/// 产生2纬度的表
		/// </summary>
		/// <param name="dt"></param>
		/// <param name="colOfGroupField1"></param>
		/// <param name="colOfGroupName1"></param>
		/// <param name="colOfGroupField2"></param>
		/// <param name="colOfGroupName2"></param>
		/// <param name="colOfNumField"></param>
		/// <param name="colOfNumName"></param>
		/// <param name="title"></param>
		/// <param name="chartHeight"></param>
		/// <param name="chartWidth"></param>
		/// <param name="ct"></param>
		/// <returns></returns>
		public   static string  GenerChart2D(DataTable dt,string colOfGroupField1, string colOfGroupName1, 
			string colOfGroupField2, string colOfGroupName2,
			string colOfNumField,string colOfNumName, string title, int chartHeight, int chartWidth, ChartType ct )
		{
			string strCategory =""; // "1" + '\t' + "2" + '\t' + "3" + '\t'+"4" + '\t' + "5" + '\t' + "6" + '\t';
			string strValue =""; // "9" + '\t' + "8" + '\t' + "4" + '\t'+"10" + '\t' + "12" + '\t' + "6" + '\t';

			//声明对象
			ChartSpace ThisChart = new ChartSpaceClass();
			ChChart ThisChChart  = ThisChart.Charts.Add(0);
			//ChSeries ThisChSeries = ThisChChart.SeriesCollection.Add(0);

			//显示图例
			ThisChChart.HasLegend = true;
			//标题
			ThisChChart.HasTitle = true;
			ThisChChart.Title.Caption = title;

			//给定x,y轴图示说明
			ThisChChart.Axes[0].HasTitle = true;
			ThisChChart.Axes[1].HasTitle = true;

//			ThisChChart.Axes[0].Title.Caption = colOfGroupName1;
//			ThisChChart.Axes[1].Title.Caption = colOfNumName;

			switch( ct)
			{
				case ChartType.Histogram:
					ThisChChart.Type = ChartChartTypeEnum.chChartTypeColumnClustered;
					DataTable dtC=dt.Clone();
					int j=-1;
					foreach(DataRow dr1 in dtC.Rows)
					{
						j++;
						ChSeries ThisChSeries =ThisChChart.SeriesCollection.Add(j);
						ThisChChart.SeriesCollection[j].DataLabelsCollection.Add();
						//给定series的名字
						ThisChChart.SeriesCollection[j].SetData (ChartDimensionsEnum.chDimSeriesNames,
							(int)ChartSpecialDataSourcesEnum.chDataLiteral, dr1[colOfGroupField1].ToString() );

						strCategory="";
						strValue="";
						foreach(DataRow dr in dt.Rows)
						{
							if (dr1[colOfGroupField1].Equals( dr[colOfGroupField1] ) ==false )
								continue;

							strCategory+= dr[ colOfGroupField1 ].ToString()+'\t'+dr[ colOfGroupField2 ].ToString() + '\t';
							strValue   += dr[ colOfNumField    ].ToString() + '\t';
						}

						//给定分类
						ThisChSeries.SetData (ChartDimensionsEnum.chDimCategories,
							(int)ChartSpecialDataSourcesEnum.chDataLiteral,strCategory);
						//给定值
						ThisChSeries.SetData
							(ChartDimensionsEnum.chDimValues,
							(int)ChartSpecialDataSourcesEnum.chDataLiteral,strValue);
					}
//					ThisChChart.Overlap = 50;
//					//旋转
//					ThisChChart.Rotation  = 360;
//					ThisChChart.Inclination = 10;
//					//背景颜色
//					ThisChChart.PlotArea.Interior.Color = "red";
//					//底色
//					ThisChChart.PlotArea.Floor.Interior.Color = "green";
					////给定series的名字
					//ThisChSeries.SetData(ChartDimensionsEnum.chDimSeriesNames,ChartSpecialDataSourcesEnum.chDataLiteral.GetHashCode(),"ssdd" );
					//给定分类
					//ThisChSeries.SetData(ChartDimensionsEnum.chDimCategories,ChartSpecialDataSourcesEnum.chDataLiteral.GetHashCode(),strCategory);
					//给定值
					//ThisChSeries.SetData(ChartDimensionsEnum.chDimValues,ChartSpecialDataSourcesEnum.chDataLiteral.GetHashCode(),strValue);
					break;
				case ChartType.Pie:
					// 产生数据
					foreach(DataRow dr in dt.Rows)
					{
						strCategory+=dr[ colOfGroupField1 ].ToString() + '\t';
						strValue   +=dr[ colOfNumField   ].ToString() +'\t';
					}

					ThisChChart.Type = ChartChartTypeEnum.chChartTypePie3D;
					ThisChChart.SeriesCollection.Add(0); 
					//在图表上显示数据
					ThisChChart.SeriesCollection[0].DataLabelsCollection.Add();
					ThisChChart.SeriesCollection[0].DataLabelsCollection[0].Position=ChartDataLabelPositionEnum.chLabelPositionAutomatic;
					ThisChChart.SeriesCollection[0].Marker.Style=ChartMarkerStyleEnum.chMarkerStyleCircle;

					//给定该组图表数据的名字 
					ThisChChart.SeriesCollection[0].SetData (ChartDimensionsEnum.chDimSeriesNames, 
						+ (int)ChartSpecialDataSourcesEnum.chDataLiteral, "strSeriesName" ); 

					//给定数据分类 
					ThisChChart.SeriesCollection[0].SetData (ChartDimensionsEnum.chDimCategories, 
						+ (int)ChartSpecialDataSourcesEnum.chDataLiteral,strCategory); 

					//给定值 
					ThisChChart.SeriesCollection[0].SetData (ChartDimensionsEnum.chDimValues, 
						(int)ChartSpecialDataSourcesEnum.chDataLiteral, strValue);
					break;
				case ChartType.Line:
					// 产生数据
					foreach(DataRow dr in dt.Rows)
					{
						strCategory+=dr[ colOfGroupField1 ].ToString() + '\t';
						strValue   +=dr[ colOfNumField   ].ToString() +'\t';
					}
					ThisChChart.Type = ChartChartTypeEnum.chChartTypeLineStacked;
					ThisChChart.SeriesCollection.Add(0); 
					//在图表上显示数据
					ThisChChart.SeriesCollection[0].DataLabelsCollection.Add();
					//ThisChChart.SeriesCollection[0].DataLabelsCollection[0].Position=ChartDataLabelPositionEnum.chLabelPositionAutomatic;
					//ThisChChart.SeriesCollection[0].DataLabelsCollection[0].Position=ChartDataLabelPositionEnum.chLabelPositionOutsideBase;

					ThisChChart.SeriesCollection[0].Marker.Style=ChartMarkerStyleEnum.chMarkerStyleCircle;

					//给定该组图表数据的名字 
					ThisChChart.SeriesCollection[0].SetData (ChartDimensionsEnum.chDimSeriesNames, 
						+ (int)ChartSpecialDataSourcesEnum.chDataLiteral, "strSeriesName" ); 

					//给定数据分类 
					ThisChChart.SeriesCollection[0].SetData (ChartDimensionsEnum.chDimCategories, 
						+ (int)ChartSpecialDataSourcesEnum.chDataLiteral,strCategory); 

					//给定值 
					ThisChChart.SeriesCollection[0].SetData (ChartDimensionsEnum.chDimValues, 
						(int)ChartSpecialDataSourcesEnum.chDataLiteral, strValue);
					break;
			}

			
			//导出图像文件
			//ThisChart.ExportPicture("G:\\chart.gif","gif",600,350);

			string fileName= ct.ToString()+PubClass.GenerTempFileName("GIF");
			string strAbsolutePath = System.Web.HttpContext.Current.Request.PhysicalApplicationPath+"\\Temp\\"+fileName; 

			try
			{
				ThisChart.ExportPicture(strAbsolutePath, "GIF", chartWidth, chartHeight); 
			}
			catch(Exception ex)
			{
				throw new Exception("@不能创建文件,可能是权限的问题，请把该目录设置为任何人都可以修改。"+strAbsolutePath+" Exception:"+ex.Message);
			}
			return fileName;
			//
			//
			//			//创建GIF文件的相对路径. 
			//			string strRelativePath = "./Temp/"+fileName;
			//
			//			//把图片添加到placeholder.  onmousedown=\"CellDown('Cell')\"
			//			//string strImageTag = "<IMG SRC='../../Temp/" + fileName + "'  />"; 
			//			return strRelativePath ;
		}
		/// <summary>
		/// 生成图表
		/// </summary>
		/// <param name="sdr">数据源(sqlDataReader)</param>
		/// <param name="_type">图表类型(枚举)</param>
		/// <param name="filePath">图片路径</param>
		/// <param name="chartWidth">图片宽度</param>
		/// <param name="chartHeight">图片高度</param>
		/// <returns>图片名称</returns>
		public void Bind(Rpt3DEntity rpt , ChartChartTypeEnum charttype, int chartWidth,int chartHeight)
		{
			 
			ChartSpace objCSpace = new ChartSpaceClass(); 
			//在ChartSpace对象中添加图表，Add方法返回chart对象
			ChChart objChart = objCSpace.Charts.Add (0);
			
			//指定图表是否需要图例
			objChart.HasLegend=true;
			objChart.HasTitle=true;
			objChart.Title.Caption=rpt.Title;
			
			//指定图表的类型。类型由OWC.ChartChartTypeEnum枚举值得到
			//objChart.Type = ChartChartTypeEnum.chChartTypeColumnClustered;
			//objChart.Type = charttype;	
			objChart.Type =charttype ; // ChartChartTypeEnum.chChartTypeColumnClustered3D;
			
			#region chart1
			int j=-1;
			foreach(EntityNoName en1 in rpt.SingleDimensionItem1)
			{
				j++;
				//在 ChartSpace 对象中添加图表，Add方法返回chart对象。
				objChart.SeriesCollection.Add(j);
				objChart.SeriesCollection[j].DataLabelsCollection.Add();
				//string strSeriesName=""+(i+1);
				string strSeriesName=en1.No+en1.Name;
				//给定series的名字
				objChart.SeriesCollection[j].SetData (ChartDimensionsEnum.chDimSeriesNames,
					(int)ChartSpecialDataSourcesEnum.chDataLiteral, strSeriesName);

				string strCategory="";
				string strValue="";
				foreach(EntityNoName en2 in rpt.SingleDimensionItem2)
				{
					foreach(EntityNoName en3 in rpt.SingleDimensionItem3)
					{
						float val =float.Parse( rpt.HisCells.GetCell(en1.No,en2.No,en3.No).val.ToString())  ;
						if (val==0)
							continue;

						strCategory+= en2.Name + '\t'  +en3.Name + '\t' ;
						strValue+= val.ToString() + '\t';
					}
				}
				//给定分类
				objChart.SeriesCollection[j].SetData (ChartDimensionsEnum.chDimCategories,
					(int)ChartSpecialDataSourcesEnum.chDataLiteral,strCategory);
				//给定值
				objChart.SeriesCollection[j].SetData
					(ChartDimensionsEnum.chDimValues,
					(int)ChartSpecialDataSourcesEnum.chDataLiteral,strValue);
				//objChart.SeriesCollection[i].Caption="";
			}		 

			//objChart.Rotation  = 360;
			//objChart.Inclination = 10;
			#endregion


			string fileName= (int)charttype+PubClass.GenerTempFileName("GIF");
			string strAbsolutePath = this.Request.PhysicalApplicationPath+"\\Temp\\"+fileName; 

			try
			{
				objCSpace.ExportPicture(strAbsolutePath, "GIF", chartWidth, chartHeight); 
			}
			catch(Exception ex)
			{
				throw new Exception("@不能创建文件,可能是权限的问题，请把该目录设置为任何人都可以修改。"+strAbsolutePath+" Exception:"+ex.Message);
			}

			//创建GIF文件的相对路径. 
			string strRelativePath = "./Temp/"+fileName;


			//把图片添加到placeholder.  onmousedown=\"CellDown('Cell')\"
			string strImageTag = "<IMG SRC='../../Temp/" + fileName + "'  />"; 
			//string strImageTag = "<IMG SRC='../../Temp/" + fileName + "'  />"; 
			this.Add(strImageTag);

			//PlaceHolder1.Controls.Add(new LiteralControl(strImageTag));
			return  ;
	
		}
		#endregion 

		#region 2纬度图形		 
		/// <summary>
		/// 交叉报表
		/// </summary>
		/// <param name="rpt">报表实体</param>
		/// <param name="chartWidth">宽度</param>
		/// <param name="chartHeight">高度</param>
		public void BindLine(RptPlanarEntity rpt,int chartWidth,int chartHeight)
		{
			Bind( rpt , ChartChartTypeEnum.chChartTypeLineStacked ,  chartWidth, chartHeight);
		}
		public void BindPie(RptPlanarEntity rpt,int chartWidth,int chartHeight)
		{
			Bind( rpt , ChartChartTypeEnum.chChartTypePie3D,  chartWidth, chartHeight);
		}
		/// <summary>
		/// 交叉报表
		/// </summary>
		/// <param name="rpt">报表实体</param>
		/// <param name="chartWidth">宽度</param>
		/// <param name="chartHeight">高度</param>
		public void BindHistogram(RptPlanarEntity rpt,int chartWidth,int chartHeight)
		{
			Bind( rpt , ChartChartTypeEnum.chChartTypeColumnClustered ,  chartWidth, chartHeight);
		}
		/// <summary>
		/// 生成图表
		/// </summary>
		/// <param name="sdr">数据源(sqlDataReader)</param>
		/// <param name="_type">图表类型(枚举)</param>
		/// <param name="filePath">图片路径</param>
		/// <param name="chartWidth">图片宽度</param>
		/// <param name="chartHeight">图片高度</param>
		/// <returns>图片名称</returns>
		public void Bind(RptPlanarEntity rpt , ChartChartTypeEnum charttype, int chartWidth,int chartHeight)
		{
			#region  RptPlanarEntity

//			BP.Port.HYOfBigs d1= new BP.Port.HYOfBigs();
//			d1.RetrieveAll();
//
//			BP.Port.Depts d2= new BP.Port.Depts();
//			d2.RetrieveAll();
//
//			DataTable dt = DBAccess.RunSQLReturnTable("SELECT FK_HYOfBig,FK_Dept, COUNT(*) FROM GS_Taxpayer WHERE (1=1) AND ( ( ( 1=1 ) ) AND ( ( GS_Taxpayer.SignDate < '2004-10-14') ) AND ( ( 1=1 ) ) ) GROUP BY FK_HYOfBig, FK_Dept") ; 
//			RptPlanarEntity rpt= new RptPlanarEntity(d1,d2,dt);

			#endregion

			#region gener 
			//			ArrayList[] data_al=new ArrayList[rpt.SingleDimensionItem1.Count];
			//			for(int i=0;i<rpt.SingleDimensionItem1.Count;i++)
			//			{
			//				data_al[i] = new ArrayList();
			//			}
			//			int j=-1 ;
			//			foreach(EntityNoName en1 in rpt.SingleDimensionItem1)
			//			{
			//				j++ ;
			//				
			//				foreach(EntityNoName en2 in rpt.SingleDimensionItem2)
			//				{
			//					//data_al[j].Add(en1.No);
			//					data_al[j].Add(en1.Name);
			//
			//					//data_al[j].Add(en2.No);
			//					data_al[j].Add(en2.Name);
			//					data_al[j].Add(rpt.PlanarCells.GetCell(en1.No,en2.No)); 
			//				}
			//			}		 
			#endregion

			ChartSpace objCSpace = new ChartSpaceClass(); 
			//在ChartSpace对象中添加图表，Add方法返回chart对象
			ChChart objChart = objCSpace.Charts.Add (0); 
			
			//指定图表是否需要图例
			objChart.HasLegend=true;
			objChart.HasTitle=true;
			objChart.Title.Caption=rpt.Title;
				
			//指定图表的类型。类型由OWC.ChartChartTypeEnum枚举值得到
			//objChart.Type = ChartChartTypeEnum.chChartTypeColumnClustered;

			objChart.Type = charttype;

			
			
			#region chart1
			int j=-1;
			foreach(EntityNoName en1 in rpt.SingleDimensionItem1)
			{
				j++;
				//在 ChartSpace 对象中添加图表，Add方法返回chart对象。
				objChart.SeriesCollection.Add(j);			
				objChart.SeriesCollection[j].DataLabelsCollection.Add();
				//string strSeriesName=""+(i+1);
				string strSeriesName=en1.No+en1.Name;

				//给定series的名字
				objChart.SeriesCollection[j].SetData (ChartDimensionsEnum.chDimSeriesNames,
					(int)ChartSpecialDataSourcesEnum.chDataLiteral, strSeriesName);

				string strCategory="";
				string strValue="";
				foreach(EntityNoName en2 in rpt.SingleDimensionItem2)
				{
					float val =float.Parse( rpt.PlanarCells.GetCell(en1.No,en2.No).val.ToString())  ;
					if (val==0)
						continue;

					strCategory+=en2.Name + '\t' ;
					strValue+= val.ToString() + '\t';
				}
				//给定分类
				objChart.SeriesCollection[j].SetData (ChartDimensionsEnum.chDimCategories,
					(int)ChartSpecialDataSourcesEnum.chDataLiteral,strCategory);
				//给定值
				objChart.SeriesCollection[j].SetData
					(ChartDimensionsEnum.chDimValues,					
					(int)ChartSpecialDataSourcesEnum.chDataLiteral,strValue);
				//objChart.SeriesCollection[i].Caption="";
			}

		 
			#endregion

			string fileName=(int)charttype + PubClass.GenerTempFileName("gif")  ;
			string strAbsolutePath = this.Request.PhysicalApplicationPath+"\\Temp\\"+fileName;

			try
			{
				objCSpace.ExportPicture(strAbsolutePath, "GIF", chartWidth, chartHeight); 
			}
			catch(Exception ex)
			{
				throw new Exception("@不能创建文件,可能是全线的问题，请把该目录设置为任何人都可以修改。"+strAbsolutePath+" Exception:"+ex.Message);
			}

			//创建GIF文件的相对路径. 
			// string strRelativePath = "./i/test.gif"; 
			string strRelativePath = "./Temp/"+fileName;


			//把图片添加到placeholder.  onmousedown=\"CellDown('Cell')\"
			string strImageTag = "<IMG SRC='../../Temp/" + fileName + "'   />"; 
			//string strImageTag = "<IMG SRC='../../Temp/" + fileName + "' />"; 

			this.Add(strImageTag);

			//PlaceHolder1.Controls.Add(new LiteralControl(strImageTag));
			return  ;
			
			//_page.Controls.AddAt(2,new LiteralControl(strImageTag));
		}
		 
		#endregion

		protected void Page_Load(object sender, System.EventArgs e)
		{
			
		}
		/// <summary>
		/// BindPie
		/// </summary>
		/// <param name="rpt"></param>
		/// <param name="width"></param>
		/// <param name="Height"></param>
		public void BindPie(RptEntitiesNoNameWithNum rpt,int width, int Height)
		{
			this.Bind_pie(rpt,ChartChartTypeEnum.chChartTypePie3D );
		} 
		/// <summary>
		/// 柱状图
		/// </summary>
		/// <param name="rpt"></param>
		public void BindHistogram(RptEntitiesNoNameWithNum rpt, int width, int Height )
		{
			this.Bind(rpt,ChartChartTypeEnum.chChartTypeColumnClustered3D,width,Height);
		}
		/// <summary>
		/// chChartTypeLine
		/// </summary>
		/// <param name="rpt"></param>
		public void BindLine(RptEntitiesNoNameWithNum rpt, int width, int Height)
		{
			this.Bind(rpt,ChartChartTypeEnum.chChartTypeColumnClustered);
		}
		public void Bind(RptEntitiesNoNameWithNum rpt, ChartChartTypeEnum  _ChartChartTypeEnum)
		{
			Bind(rpt,_ChartChartTypeEnum,800,600);
		}
		/// <summary>
		/// 生成折线或者柱形图表 
		/// </summary>
		/// <param name="rpt">传入一个报表</param>
		/// <param name="_ChartChartTypeEnum">图表形状，如柱状图、折线图</param>
		public void Bind(RptEntitiesNoNameWithNum rpt, ChartChartTypeEnum  _ChartChartTypeEnum, int width, int Height)
		{
			// 在此处放置用户代码以初始化页面
			//创建ChartSpace对象来放置图表
			//ChartSpace是用来放置图表的类，图表完成后用它来输出。
			ChartSpace objCSpace = new ChartSpaceClass();

			//在ChartSpace对象中添加图表，ChartSpace的Add方法创建图表，返回chart对象，参数表示所创建图表的索引
			ChChart objChart = objCSpace.Charts.Add(0); 
			//objChart.SeriesCollection.Add(0);
			//指定图表的类型。类型由ChartChartTypeEnum枚举值得到，通过设定Chart类对象的Type属性来指定图表的类型。
			/*其他的类型包括：chChartTypeArea 面积图、chChartTypeBarClustered 条形图、chChartTypePie 饼图、
			 * chChartType RadarLine 雷达线图、chChartTypeSmoothLine 平滑曲线图、chChartTypeDoughnut 环形图等等*/
			//			objChart.Type = ChartChartTypeEnum.chChartTypeColumnClustered; 
			objChart.Type = _ChartChartTypeEnum ;  // ChartChartTypeEnum.chChartTypeSmoothLine; 

			//ChartChartTypeEnum.chChartTypeSmoothLine; 

			// chChartTypeColumnClustered3D
			//chChartTypeColumnClustered3D

			//指定图表是否需要图例 
			/*图示说明主要包括图例（用颜色表示数据类型）、图题（图表的标题）、
			 * ＸＹ轴的数据说明（一般用来说明各轴上的数据单位）*/
			objChart.HasLegend = true; 

			//给定标题 
			objChart.HasTitle = true; //是否显示标题，如设为false刚标题不能赋值
			objChart.Title.Caption= rpt.Title; 
			

			/*以下代码设置X，Y轴图示的说明，可以设置说明文内容显示方式，字体颜色等
			 * Axes[0]为X轴，Axes[1]为Y轴*/
			//给定x,y轴的图示说明. 
			objChart.Axes[0].HasTitle = true; 
			objChart.Axes[0].Title.Caption = rpt.SingleDimensionItem1.GetNewEntity.EnDesc ;
			objChart.Axes[0].Orientation=-90 ; 



			//objChart.Axes[0].


			//objChart.Axes[1].Title.Font.Color="red";
			objChart.Axes[1].HasTitle = true; 
			objChart.Axes[1].Title.Caption = rpt.LeftTitle; 
			//objChart.Axes[1].Title.Font.Color="red";
//			objCSpace.
//
//			objChart.Axes[0].ValueToPoint("78");
//			objChart.Axes[1].ValueToPoint("13");
//
//			 ActiveChart.ApplyDataLabels AutoText:=True, LegendKey:=False, _
//        HasLeaderLines:=False, ShowSeriesName:=False, ShowCategoryName:=False, _
//        ShowValue:=True, ShowPercentage:=False, ShowBubbleSize:=False
//End Sub


			


			//计算数据 
			/*在运行时向OWC中输入数据有多种方法。所有这些方法都要用到 SetData 方法来真正将数据写入 Chart 组件，
			 * 因此将详细介绍此方法。SetData 方法应用于 WCChart、WCErrorBars 和 WCSeries 对象并能向这三种对象输
			 * 入数据。 
			 * SetData 方法有三个参数：Dimension、DataSourceIndex 和 DataReference。Dimension 参数引用图表中被填
			 * 充数据的一部分。可用的维度常数为 SeriesNames、Categories、Values、YValues、XValues、OpenValues、
			 * CloseValues、HighValues、LowValues、BubbleValues、RValues 和 ThetaValues。这些常数每一种都引用了
			 * 图表的一部分；例如，SeriesNames 常数引用了每个序列名，OpenValues 和 CloseValues 常数引用股票图的
			 * 开盘价和收盘价等等。每个常数都是作为诸如 chDimSeriesNames、chDimCategories、chDimValues等窗体中的
			 * 枚举常数而传递给该方法的。
			 * 参见http://www.webasp.net/tech/article_show.asp?id=13635*/
			/*categories 和 values 可以用tab分割的字符串来表示*/ 
			string strSeriesName = "图例 1"; 


//			string strCategory = "1" + '\t' + "2" + '\t' + "3" + '\t'+"4" + '\t' + "5" + '\t' + "6" + '\t'; 
//			string strValue =    "9" + '\t' + "8" + '\t' + "4" + '\t'+"10" + '\t' + "12" + '\t' + "6" + '\t';

			string strCategory="" ; 
			string strValue="" ; 

			foreach(BP.Rpt.Rpt1DCell cell in rpt.Rpt1DCells)
			{
				
				strCategory+= rpt.GetTextByValue(cell.PK) + '\t' ;
				strValue+= cell.val.ToString() + '\t';
			}
			//foreach(rpt

			//添加一组图表数据 
			/*添加数据主要设定Chart类对象的SeriesCollection属性。首先使用SeriesCollection的Add方法创建一组数据。
			 * 然后使用SetData方法具体添加数据。*/
			objChart.SeriesCollection.Add(0); 
			//在图表上显示数据
			objChart.SeriesCollection[0].DataLabelsCollection.Add();
			objChart.SeriesCollection[0].Marker.Style=ChartMarkerStyleEnum.chMarkerStyleCircle;

			//给定该组图表数据的名字 
			objChart.SeriesCollection[0].SetData (ChartDimensionsEnum.chDimSeriesNames, 
				+ (int)ChartSpecialDataSourcesEnum.chDataLiteral, strSeriesName); 

			//给定数据分类 
			objChart.SeriesCollection[0].SetData (ChartDimensionsEnum.chDimCategories, 
				+ (int)ChartSpecialDataSourcesEnum.chDataLiteral,strCategory); 

			//给定值 
			objChart.SeriesCollection[0].SetData 
				(ChartDimensionsEnum.chDimValues, 
				(int)ChartSpecialDataSourcesEnum.chDataLiteral, strValue);  

			//输出成GIF文件. 
			//string strAbsolutePath = (Server.MapPath(".")) + "\\i\\test.gif";

			string fileName= (int)_ChartChartTypeEnum +DateTime.Now.ToString("MMddhhmmss")+".gif";
			//string fileName=f+".gif";
			string strAbsolutePath = this.Request.PhysicalApplicationPath+"\\Temp\\"+fileName; 
			try
			{
				objCSpace.ExportPicture(strAbsolutePath, "GIF", width, Height); 
			}
			catch(Exception ex)
			{
				throw new Exception("@不能创建文件,可能是全线的问题，请把该目录设置为任何人都可以修改。"+strAbsolutePath+" Exception:"+ex.Message);
			}

			//创建GIF文件的相对路径. 
			// string strRelativePath = "./i/test.gif"; 
			string strRelativePath = "./Temp/"+fileName;


			//把图片添加到placeholder.  onmousedown=\"CellDown('Cell')\"
			string strImageTag = "<IMG SRC='../../Temp/" + fileName + "'   />"; 
			this.Add(strImageTag);

			//PlaceHolder1.Controls.Add(new LiteralControl(strImageTag));
		}

		/// <summary>
		/// 生成饼状图表 
		/// </summary>
		/// <param name="rpt">传入一个报表</param>
		/// <param name="_ChartChartTypeEnum">图表形状，如柱状图、折线图</param>
		public void Bind_pie(RptEntitiesNoNameWithNum rpt, ChartChartTypeEnum  _ChartChartTypeEnum)
		{
			//在此处放置用户代码以初始化页面
			//创建ChartSpace对象来放置图表
			//ChartSpace是用来放置图表的类，图表完成后用它来输出。
			ChartSpace objCSpace = new ChartSpaceClass(); 

			//在ChartSpace对象中添加图表，ChartSpace的Add方法创建图表，返回chart对象，参数表示所创建图表的索引
			ChChart objChart = objCSpace.Charts.Add(0); 
			//objChart.SeriesCollection.Add(0);

			//指定图表的类型。类型由ChartChartTypeEnum枚举值得到，通过设定Chart类对象的Type属性来指定图表的类型。
			/*其他的类型包括：chChartTypeArea 面积图、chChartTypeBarClustered 条形图、chChartTypePie 饼图、
			 * chChartType RadarLine 雷达线图、chChartTypeSmoothLine 平滑曲线图、chChartTypeDoughnut 环形图等等*/
			//			objChart.Type = ChartChartTypeEnum.chChartTypeColumnClustered; 
			// ChartChartTypeEnum.chChartTypeSmoothLine;
			objChart.Type = _ChartChartTypeEnum ;  

			//指定图表是否需要图例 
			/*图示说明主要包括图例（用颜色表示数据类型）、图题（图表的标题）、
			 * ＸＹ轴的数据说明（一般用来说明各轴上的数据单位）*/
			objChart.HasLegend = true; 

			//给定标题 
			objChart.HasTitle = true; //是否显示标题，如设为false刚标题不能赋值
			objChart.Title.Caption= rpt.Title; 
 
			/*categories 和 values 可以用tab分割的字符串来表示*/ 
			string strSeriesName = "图例 1"; 
			string strCategory="" ; 
			string strValue="";

			foreach(BP.Rpt.Rpt1DCell cell in rpt.Rpt1DCells)
			{
				//string val=
				strCategory+= rpt.GetTextByValue(cell.PK)+"%" + '\t' ;
				strValue+= cell.val.ToString() +"%"+ '\t';
			}
			//this.ResponseWriteBlueMsg(strCategory+"<br>"+strValue);
			//foreach(rpt

			//添加一组图表数据 
			/*添加数据主要设定Chart类对象的SeriesCollection属性。首先使用SeriesCollection的Add方法创建一组数据。
			 * 然后使用SetData方法具体添加数据。*/
			objChart.SeriesCollection.Add(0); 
			//在图表上显示数据
			objChart.SeriesCollection[0].DataLabelsCollection.Add();
			if (_ChartChartTypeEnum==ChartChartTypeEnum.chChartTypePie3D)
			{
				objChart.SeriesCollection[0].DataLabelsCollection[0].Position=ChartDataLabelPositionEnum.chLabelPositionAutomatic;
			}
			else
			{
				objChart.SeriesCollection[0].DataLabelsCollection[0].Position=ChartDataLabelPositionEnum.chLabelPositionOutsideBase;
			}

			objChart.SeriesCollection[0].Marker.Style=ChartMarkerStyleEnum.chMarkerStyleCircle;

			//给定该组图表数据的名字 
			objChart.SeriesCollection[0].SetData (ChartDimensionsEnum.chDimSeriesNames, 
				+ (int)ChartSpecialDataSourcesEnum.chDataLiteral, strSeriesName); 

			//给定数据分类 
			objChart.SeriesCollection[0].SetData (ChartDimensionsEnum.chDimCategories, 
				+ (int)ChartSpecialDataSourcesEnum.chDataLiteral,strCategory); 

			//给定值 
			objChart.SeriesCollection[0].SetData (ChartDimensionsEnum.chDimValues, 
				(int)ChartSpecialDataSourcesEnum.chDataLiteral, strValue);
			//输出成GIF文件. 
			//string strAbsolutePath = (Server.MapPath(".")) + "\\i\\test.gif"; 

			string fileName= (int)_ChartChartTypeEnum + PubClass.GenerTempFileName("gif");
			string strAbsolutePath = this.Request.PhysicalApplicationPath+"\\Temp\\"+fileName; 

			try
			{
				objCSpace.ExportPicture(strAbsolutePath, "GIF", 800, 600); 
			}
			catch(Exception ex)
			{
				throw new Exception("@不能创建文件,可能是全线的问题，请把该目录设置为任何人都可以修改。"+strAbsolutePath+" Exception:"+ex.Message);
			}

			//创建GIF文件的相对路径. 
			// string strRelativePath = "./i/test.gif"; 
			string strRelativePath = "./Temp/"+fileName;


			//把图片添加到placeholder.  onmousedown=\"CellDown('Cell')\"
			string strImageTag = "<IMG SRC='../../Temp/" + fileName + "'   />"; 
			this.Add(strImageTag);

			//PlaceHolder1.Controls.Add(new LiteralControl(strImageTag));


			//			string fileName=DBAccess.GenerOID()+".gif";
			//			string strAbsolutePath = (Server.MapPath(".")) + "\\Temp\\"+fileName; 
			//			objCSpace.ExportPicture(strAbsolutePath, "GIF", 800, 600); 
			//
			//			//创建GIF文件的相对路径. 
			//			// string strRelativePath = "./i/test.gif"; 
			//			string strRelativePath = "./Temp/"+fileName;
			//
			//
			//			//把图片添加到placeholder. 
			//			string strImageTag = "<IMG SRC='" + strRelativePath + "'/>"; 
			//			PlaceHolder1.Controls.Add(new LiteralControl(strImageTag));
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
