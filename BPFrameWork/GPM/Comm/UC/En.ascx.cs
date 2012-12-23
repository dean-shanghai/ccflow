
namespace BP.Web.Comm.UC
{
    using System;
    using System.Collections;
    using System.Configuration;
    using System.Data;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using System.Web.UI.WebControls.WebParts;
    using BP.En;
    using BP.Sys;
    using BP.DA;
    using BP.Web;
    using BP.Web.Controls;
    using BP.Web.UC;
    using BP.XML;
    using BP.Sys.Xml;
    using BP.Port;
    public partial class En : BP.Web.UC.UCBase3
    {
        #region 属性
        public string EnTitleLegend = null;
        public string EnSaveAlertInfo = null;
        public Entity HisEn = null;
        //public string EnName
        //{
        //    get
        //    {
        //        string s = ViewState["EnName"] as string;
        //        if (s == null)
        //            s = "BP.GE.OAEvent";
        //        return s;
        //    }
        //    set
        //    {
        //        ViewState["EnName"] = value;
        //    }
        //}
        #endregion 属性

        protected void Page_Load(object sender, EventArgs e)
        {
            //    Entity en = this.HisEn BP.DA.ClassFactory.GetEn(this.EnName);
        }

        //protected override void OnUnload(EventArgs e)
        //{
        //    this.LoadIt(this.EnTitleLegend, this.HisEn, this.EnSaveAlertInfo);
        //    base.OnUnload(e);
        //}

        public void LoadIt(string lab, Entity en, string saveInfo)
        {
            this.EnSaveAlertInfo = saveInfo;
            this.EnTitleLegend = lab;
            this.HisEn = en;

            Map map = this.HisEn.EnMap;
            if (this.EnSaveAlertInfo == null)
                this.EnTitleLegend = map.EnDesc;

            this.UCEn1.Bind(HisEn, this.HisEn.ToString(), false, true);

            this.UCEn2.DivInfoBlockBegin();
            this.UCEn2.AddSpace(5);

            Btn btn = new Btn();
            btn.ID = "ds";
            btn.Text = " 提交 ";

            btn.Click += new EventHandler(btn_Click);
            this.UCEn2.Add(btn);

            btn = new Btn();
            btn.ID = "calcel";
            btn.Text = " 取消 ";
            btn.OnClientClick = "windows.close();";
            btn.Click += new EventHandler(btn_Click);
            this.UCEn2.Add(btn);
            this.UCEn2.DivInfoBlockEnd();
        }
        void btn_Click(object sender, EventArgs e)
        {
           //// BP.GE.OAEvent en = new BP.GE.OAEvent();
           // this.UCEn1.Copy(en);
           // if (en.IsEmpty)
           // {
           //     en.No = en.GenerNewNo;
           //     en.CheckPhysicsTable();
           //     en.Insert();
           // }
           // else
           // {
           //     en.Save();
           // }

           // this.UCEn2.Clear();
           // this.UCEn1.Clear();

           // this.UCEn1.DivInfoBlockBegin();

           // if (this.EnSaveAlertInfo == null)
           // {
           //     this.UCEn1.AddH3("提交成功");
           //     this.UCEn1.Add("谢谢使用。");
           // }
           // else
           // {
           //     this.UCEn1.Add(this.EnSaveAlertInfo);
           // }
           // this.UCEn1.DivInfoBlockEnd();
        }

    }
}