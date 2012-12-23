using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Web;
using System.Text;
using System.IO;

namespace BP.GPM
{
	/// <summary>
	/// 操作员
	/// </summary>
    public class WFEmpAttr
    {
        #region 基本属性
        /// <summary>
        /// No
        /// </summary>
        public const string No = "No";
        /// <summary>
        /// 申请人
        /// </summary>
        public const string Name = "Name";
        public const string LoginData = "LoginData";
        public const string Tel = "Tel";
        /// <summary>
        /// 授权人
        /// </summary>
        public const string Author = "Author";
        /// <summary>
        /// 授权日期
        /// </summary>
        public const string AuthorDate = "AuthorDate";
        /// <summary>
        /// 是否处于授权状态
        /// </summary>
        public const string AuthorIsOK = "AuthorIsOK";
        public const string Email = "Email";
        public const string AlertWay = "AlertWay";
        public const string Stas = "Stas";
        public const string FK_Dept = "FK_Dept";
        public const string Idx = "Idx";
        public const string FtpUrl = "FtpUrl";
        public const string Style = "Style";
        public const string Msg = "Msg";
        public const string TM = "TM";
        #endregion
    }
	/// <summary>
	/// 操作员
	/// </summary>
    public class WFEmp : EntityNoName
    {
        #region 基本属性
        public string HisAlertWayT
        {
            get
            {
                return this.GetValRefTextByKey(WFEmpAttr.AlertWay);
            }
        }
        public string FK_Dept
        {
            get
            {
                return this.GetValStringByKey(WFEmpAttr.FK_Dept);
            }
            set
            {
                SetValByKey(WFEmpAttr.FK_Dept, value);
            }
        }
        public string Style
        {
            get
            {
                return this.GetValStringByKey(WFEmpAttr.Style);
            }
            set
            {
                this.SetValByKey(WFEmpAttr.Style, value);
            }
        }
        public string TM
        {
            get
            {
                return this.GetValStringByKey(WFEmpAttr.TM);
            }
            set
            {
                this.SetValByKey(WFEmpAttr.TM, value);
            }
        }
        public string Tel
        {
            get
            {
                return this.GetValStringByKey(WFEmpAttr.Tel);
            }
            set
            {
                SetValByKey(WFEmpAttr.Tel, value);
            }
        }
        public int Idx
        {
            get
            {
                return this.GetValIntByKey(WFEmpAttr.Idx);
            }
            set
            {
                SetValByKey(WFEmpAttr.Idx, value);
            }
        }
        public string TelHtml
        {
            get
            {
                if (this.Tel.Length == 0)
                    return this.ToE("NotSet","未设置");
                else
                    return "<a href=\"javascript:WinOpen('./Msg/SMS.aspx?Tel=" + this.Tel + "');\"  ><img src=./Img/SMS.gif border=0/>" + this.Tel + "</a>";
            }
        }
        public string EmailHtml
        {
            get
            {
                if (this.Email==null || this.Email.Length == 0)
                    return this.ToE("NotSet","未设置");
                else
                    return "<a href='Mailto:" + this.Email + "' ><img src=./Img/SMS.gif border=0/>" + this.Email + "</a>";
            }
        }
        public string Email
        {
            get
            {
                return this.GetValStringByKey(WFEmpAttr.Email);
            }
            set
            {
                SetValByKey(WFEmpAttr.Email, value);
            }
        }
        public string Author
        {
            get
            {
                return this.GetValStrByKey(WFEmpAttr.Author);
            }
            set
            {
                SetValByKey(WFEmpAttr.Author, value);
            }
        }
        public string AuthorDate
        {
            get
            {
                return this.GetValStringByKey(WFEmpAttr.AuthorDate);
            }
            set
            {
                SetValByKey(WFEmpAttr.AuthorDate, value);
            }
        }
        public string FtpUrl
        {
            get
            {
                return this.GetValStringByKey(WFEmpAttr.FtpUrl);
            }
            set
            {
                SetValByKey(WFEmpAttr.FtpUrl, value);
            }
        }
        public string Stas
        {
            get
            {
                string s= this.GetValStringByKey(WFEmpAttr.Stas);
                if (s == "")
                {
                    EmpStations ess = new EmpStations();
                    ess.Retrieve(EmpStationAttr.FK_Emp, this.No);
                    foreach (EmpStation es in ess)
                    {
                        s += es.FK_StationT + ",";
                    }

                    if (ess.Count != 0)
                    {
                        this.Stas = s;
                        this.Update();
                        //this.Update(WFEmpAttr.Stas, s);
                    }
                    return s;
                }
                else
                {
                    return s;
                }
            }
            set
            {
                SetValByKey(WFEmpAttr.Stas, value);
            }
        }
        public bool AuthorIsOK
        {
            get
            {
                return this.GetValBooleanByKey(WFEmpAttr.AuthorIsOK);
            }
            set
            {
                SetValByKey(WFEmpAttr.AuthorIsOK, value);
            }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 操作员
        /// </summary>
        public WFEmp() { }
        /// <summary>
        /// 操作员
        /// </summary>
        /// <param name="no"></param>
        public WFEmp(string no)
        {
            this.No = no;
            try
            {
                if (this.RetrieveFromDBSources() == 0)
                {
                    Emp emp = new Emp(no);
                    this.Copy(emp);
                    this.Insert();
                }
            }
            catch
            {
                this.CheckPhysicsTable();
            }
        }
        /// <summary>
        /// 重写基类方法
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("WF_Emp");
                map.EnDesc = "操作员";
                map.EnType = EnType.App;
                map.AddTBStringPK(WFEmpAttr.No, null, "No", true, true, 1, 50, 20);
                map.AddTBString(WFEmpAttr.Name, null, "Name", true, true, 0, 50, 20);
                map.AddTBString(WFEmpAttr.Tel, null, "Tel", true, true, 0, 50, 20);
                map.AddTBString(WFEmpAttr.FK_Dept, null, "FK_Dept", true, true, 0, 50, 20);
                map.AddTBString(WFEmpAttr.Email, null, "Email", true, true, 0, 50, 20);
                map.AddTBString(WFEmpAttr.TM, null, "即时通讯号", true, true, 0, 50, 20);
                map.AddDDLSysEnum(WFEmpAttr.AlertWay, 3, "收听方式", true, true, WFEmpAttr.AlertWay);
                map.AddTBString(WFEmpAttr.Author, null, "授权人", true, true, 0, 50, 20);
                map.AddTBString(WFEmpAttr.AuthorDate, null, "授权日期", true, true, 0, 50, 20);
                map.AddTBInt(WFEmpAttr.AuthorIsOK, 0, "是否授权成功", true, true);
                map.AddTBString(WFEmpAttr.Stas, null, "岗位s", true, true, 0, 3000, 20);
                map.AddTBString(WFEmpAttr.FtpUrl, null, "FtpUrl", true, true, 0, 50, 20);
                map.AddTBString(WFEmpAttr.Msg, null, "Msg", true, true, 0, 4000, 20);
                map.AddTBInt(WFEmpAttr.Style, 0, "Style", false, false);
                map.AddTBInt(WFEmpAttr.Idx, 0, "Idx", false, false);
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

        #region 方法
        protected override bool beforeUpdate()
        {
            string msg = "";
            //if (this.Email.Length == 0)
            //{
            //    if (this.HisAlertWay == AlertWay.SMSAndEmail || this.HisAlertWay == AlertWay.Email)
            //        msg += "错误：您设置了用e-mail接收信息，但是您没有设置e-mail。";
            //}

            //if (this.Tel.Length == 0)
            //{
            //    if (this.HisAlertWay == AlertWay.SMSAndEmail || this.HisAlertWay == AlertWay.SMS)
            //        msg += "错误：您设置了用短信接收信息，但是您没有设置手机号。";
            //}

            EmpStations ess = new EmpStations();
            ess.Retrieve(EmpStationAttr.FK_Emp, this.No);
            string sts = "";
            foreach (EmpStation es in ess)
            {
                sts += es.FK_StationT + " ";
            }
            this.Stas = sts;

            if (msg != "")
                throw new Exception(msg);

            return base.beforeUpdate();
        }
        #endregion

       
        public void DoUp()
        {
            this.DoOrderUp("FK_Dept", this.FK_Dept, "Idx");
            return;

            //string sql = "SELECT No,Idx FROM WF_Emp WHERE FK_Dept='" + this.FK_Dept + "' order by IDX";
            //DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
            //int idx = 0;
            //string beforeNo = "";
            //string myNo = "";
            //bool isMeet = false;
            //foreach (DataRow dr in dt.Rows)
            //{
            //    idx++;
            //    myNo = dr["No"].ToString();
            //    if (myNo == this.No)
            //        isMeet = true;

            //    if (isMeet == false)
            //        beforeNo = myNo;
            //    DBAccess.RunSQL("UPDATE WF_Emp SET idx=" + idx + " WHERE No='" + myNo + "'");
            //}
            //DBAccess.RunSQL("UPDATE WF_Emp SET Idx=Idx+1 WHERE No='" + beforeNo + "'");
            //DBAccess.RunSQL("UPDATE WF_Emp SET Idx=Idx-1 WHERE No='" + this.No + "'");
        }
        public void DoDown()
        {
            this.DoOrderDown("FK_Dept", this.FK_Dept, "Idx");
            return;
            //string sql = "SELECT No,Idx FROM WF_Emp WHERE FK_Dept='" + this.FK_Dept + "' order by IDX";
            //DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
            //int idx = 0;
            //string nextNo = "";
            //string myNo = "";
            //bool isMeet = false;
            //foreach (DataRow dr in dt.Rows)
            //{
            //    myNo = dr["No"].ToString();
            //    if (isMeet == true)
            //    {
            //        nextNo = myNo;
            //        isMeet = false;
            //    }

            //    idx++;

            //    if (myNo == this.No)
            //        isMeet = true;

               
            //    DBAccess.RunSQL("UPDATE WF_Emp SET idx=" + idx + " WHERE No='" + myNo + "'");
            //}

            //DBAccess.RunSQL("UPDATE WF_Emp SET Idx=Idx-1 WHERE No='" + nextNo + "'");
            //DBAccess.RunSQL("UPDATE WF_Emp SET Idx=Idx+1 WHERE No='" + this.No + "'");
        }
    }
	/// <summary>
	/// 操作员s 
	/// </summary>
	public class WFEmps : EntitiesNoName
	{	 
		#region 构造
		/// <summary>
		/// 操作员s
		/// </summary>
		public WFEmps()
		{
		}
		/// <summary>
		/// 得到它的 Entity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new WFEmp();
			}
		}

        public override int RetrieveAll()
        {
            return base.RetrieveAll("FK_Dept","Idx");
        }
		#endregion
	}
	
}
