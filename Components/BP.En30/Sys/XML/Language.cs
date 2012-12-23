using System;
using System.Collections;
using System.Data;
using BP.DA;
using BP.En;
using BP.XML;
using Microsoft.International.Converters.TraditionalChineseToSimplifiedConverter;

namespace BP.Sys
{
	/// <summary>
	/// 属性
	/// </summary>
	public class LanguageAttr
	{
		/// <summary>
		/// 编号
		/// </summary>
		public const string No="No";
        /// <summary>
        /// 中文
        /// </summary>
        public const string CH = "CH";
		/// <summary>
		/// 英文
		/// </summary>
        public const string EN = "EN";
        /// <summary>
        /// B5
        /// </summary>
        public const string B5 = "B5";
	}
    /// <summary>
    /// 语言
    /// </summary>
	public class Language:XmlEn
	{
		#region 属性
		public string No
		{
			get
			{
				return this.GetValStringByKey(LanguageAttr.No);
			}
		}
		/// <summary>
		/// 数据
		/// </summary>
        public string CH
		{
			get
			{
                return this.GetValStringByKey(LanguageAttr.CH);
			}
		}
        public string EN
		{
			get
			{
                return this.GetValStringByKey(LanguageAttr.EN);
			}
		}
        public string B5
        {
            get
            {
                return this.GetValStringByKey(LanguageAttr.B5);
            }
        }
		#endregion

		#region 构造
        /// <summary>
        /// 语言
        /// </summary>
		public Language()
		{
		}
        /// <summary>
        /// 语言
        /// </summary>
        /// <param name="no"></param>
        public Language(string no)
        {
            //  this.No = no;
            int i = this.RetrieveByPK("No", no);
            if (i == 0)
            {
                BP.SystemConfig.DoClearCash();
                Log.DebugWriteWarning(" not set values language no= " + no + "");
                throw new Exception("编号：No=" + no + " 错误：" + this.ToString());
            }
        }
		/// <summary>
		/// 获取一个实例
		/// </summary>
		public override XmlEns GetNewEntities
		{
			get
			{
				return new Languages();
			}
		}
		#endregion

        public static string Turn2Traditional(string s)
        {
            try
            {
                return ChineseConverter.Convert(s, ChineseConversionDirection.SimplifiedToTraditional);
            }
            catch
            {
                return s;
            }
        }
        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="no"></param>
        /// <param name="chVal"></param>
        /// <returns></returns>
        public static string GetValByUserLang(string no, string chVal)
        {
            if (no == null)
                throw new Exception("没有给编号值。");
            string lang = BP.Web.WebUser.SysLang;
            switch (lang)
            {
                case "CH":
                    return chVal;
                case "B5":
                    return Turn2Traditional(chVal);
                default:
                    break;
            }

            Language en = new Language(no);
            return en.GetValStringByKey(lang);
        }
        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="no"></param>
        /// <param name="ltype"></param>
        /// <param name="chVal"></param>
        /// <returns></returns>
        public static string GetVal(string no, string ltype, string chVal)
        {
            switch (ltype)
            {
                case "CH":
                    return chVal;
                case "B5":
                    return Turn2Traditional(chVal);
                default:
                    break;
            }
            Language en = new Language(no);
            return en.GetValStringByKey(ltype);
        }
	}
	/// <summary>
    /// 语言
	/// </summary>
	public class Languages:XmlEns
	{
		#region 构造
		/// <summary>
        /// 语言
		/// </summary>
		public Languages()
        {
        }
		#endregion

		#region 重写基类属性或方法。
		/// <summary>
		/// 得到它的 Entity 
		/// </summary>
		public override XmlEn GetNewEntity
		{
			get
			{
				return new Language();
			}
		}
		public override string File
		{
            get
            {
                if (SystemConfig.IsBSsystem)
                    return SystemConfig.PathOfData + "\\Language";
                else
                {
                    //  AppDomain.CurrentDomain.BaseDirectory + ".\\..\\..\\..\\.VisualFlow\\Data\\Language";
                    System.IO.DirectoryInfo info = new System.IO.DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "\\..\\..\\..\\VisualFlow\\Data\\Language");
                    return info.FullName;
                    // return info.Name;
                    // return AppDomain.CurrentDomain.BaseDirectory + ".\\..\\..\\..\\VisualFlow\\Data\\Language";
                }
            }
		}
		/// <summary>
		/// 物理表名
		/// </summary>
		public override string TableName
		{
			get
			{
				return "Item";
			}
		}
		public override Entities RefEns
		{
			get
			{
				return null;
			}
		}
		#endregion
		 
	}
}
