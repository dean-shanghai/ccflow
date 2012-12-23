using System;

namespace BP.Web.Controls
{
	/// <summary>
	/// YearMonth 的摘要说明。
	/// </summary>
	public class YearMonth
	{
		public YearMonth(string strYear, string strMonth)
		{
			this.Year=strYear;
			this.Month=strMonth;
			 
		}
		public float x=0;
		public float y=0;
		public string Year;
		public string Month;
		public string strYearMonth
		{
			get
			{
				if (this.Month=="1")
					return  this.Year.Substring(2)+"/"+this.Month;
				else
					return  this.Month;
			}
		}
	}
	public class YearMonths : System.Collections.CollectionBase
	{
		public YearMonth this[int index]
		{
			get 
			{
				return (YearMonth)this.InnerList[index];
			}
		}
		/// <summary>
		/// 初始化这个年度的月份。
		/// </summary>
		public void InitMonthOfThisYear()
		{
			//this.InnerList.Clear();
			int m = DateTime.Now.Month;
			int i = 0 ;
			while ( i < 12)
			{
				i++;
				YearMonth li =new YearMonth( DateTime.Now.Year.ToString(), i.ToString() ); 
				if (i==m) 
				{
					this.InnerList.Add(li);					 
					break;
				}				
				this.InnerList.Add(li);
			}
		}
		/// <summary>
		/// 初始化这个年度的月份。
		/// </summary>
		public void InitMonthOfBeforeYear()
		{
			//this.InnerList.Clear();			 
			int i = 0 ;
			while ( i < 12)
			{
				i++;
				//YearMonth li =new YearMonth( SystemConfig.BeforeYear, i.ToString() ); 				 			
				//this.InnerList.Add(li);
			}
		}
		/// <summary>
		/// 初始化这个年度的月份。
		/// </summary>
		public void InitMonthOfThirdYear()
		{
			//this.InnerList.Clear();			 
			int i = 0 ;
			while ( i < 12)
			{
				i++;
			//	YearMonth li =new YearMonth( SystemConfig.ThirdYear, i.ToString() ); 				 			
				//this.InnerList.Add(li);
			}
		}
		public void InitThreeYear()
		{
			this.InitMonthOfThirdYear();
			this.InitMonthOfBeforeYear();
			this.InitMonthOfThisYear();
		}

		public YearMonth FindYMByKJNY(string kjny)
		{
			foreach( YearMonth en in this)
			{
				string str = en.Year+en.Month;
				if (str==kjny) 
					return en;
			}
			return null;
		}
	}
}
