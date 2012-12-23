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
using BP.WF;
using BP.DA;
using BP.En;
using BP.Web;

public partial class WF_UC_Pub : BP.Web.UC.UCBase3
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public void BindWorkDtl(Node nd, Works wks)
    {
        Attrs attrs = wks.GetNewEntity.EnMap.Attrs;
        this.AddTable("width='100%' align=center class=Table");
        this.AddTR();
        this.AddTDTitle("序");
        foreach (Attr attr in attrs)
        {
            if (attr.UIVisible == false)
                continue;

            this.AddTDTitle(attr.Desc);
        }
        this.AddTDTitle("报告");
        this.AddTDTitle("轨迹");
        this.AddTREnd();

        int idx = 0;
        bool is1 = false;
        foreach (Entity en in wks)
        {
            idx++;
            is1 = this.AddTR(is1);
            this.AddTD(idx);
            foreach (Attr attr in attrs)
            {
                if (attr.UIVisible == false)
                    continue;
                switch (attr.MyDataType)
                {
                    case DataType.AppBoolean:
                        this.AddTD(en.GetValBoolStrByKey(attr.Key));
                        break;
                    case DataType.AppFloat:
                    case DataType.AppInt:
                    case DataType.AppDouble:
                        this.AddTD(en.GetValFloatByKey(attr.Key));
                        break;
                    case DataType.AppMoney:
                        this.AddTDMoney(en.GetValDecimalByKey(attr.Key));
                        break;
                    default:
                        this.AddTD(en.GetValStrByKey(attr.Key));
                        break;
                }
            }
            this.AddTD("<a href=\"./../WF/WFRpt.aspx?WorkID=" + en.GetValIntByKey("OID") + "&FID=" + en.GetValByKey("FID") + "&FK_Flow=" + nd.FK_Flow + "\" target=bk >报告</a>");
            this.AddTD("<a href=\"./../WF/Chart.aspx?WorkID=" + en.GetValIntByKey("OID") + "&FID=" + en.GetValByKey("FID") + "&FK_Flow=" + nd.FK_Flow + "\" target=bk >轨迹</a>");
            this.AddTREnd();
        }

        this.AddTRSum();
        this.AddTD("");
        foreach (Attr attr in attrs)
        {
            if (attr.UIVisible == false)
                continue;
            switch (attr.MyDataType)
            {
                case DataType.AppFloat:
                case DataType.AppInt:
                case DataType.AppDouble:
                    this.AddTD("align=right class=TD", wks.GetSumDecimalByKey(attr.Key));
                    break;
                case DataType.AppMoney:
                    this.AddTD("align=right class=TD", wks.GetSumDecimalByKey(attr.Key).ToString("0.00"));
                    break;
                default:
                    this.AddTD();
                    break;
            }
        }
        this.AddTD();
        this.AddTD();
        this.AddTREnd();
        this.AddTableEnd();
    }
  
    public void BindWFRptV2(WorkFlow wf)
    {
        Int64 workid = wf.WorkID;
        this.Add("<p align='center'>　</p>");
        this.Add("<p align='center' style='line-height: 100%' ><b>" + this.ToE("About", "关于") + "《" + wf.HisStartWork.Title + "》" + this.ToE("WorkRpt", "工作报告") + "</b><hr width=75% /></p>");
        this.Add("<p align='center'>　</p>");

        #region 附件信息
        // 加上附件信息 
        FileManagers fms = new FileManagers();
        fms.Retrieve(FileManagerAttr.WorkID, workid);
        if (fms.Count > 0)
        {
            this.AddTable();
            this.AddCaption(this.ToE("FJ", "附件"));
            this.AddTR();
            this.AddTDTitle("ID");
            this.AddTDTitle(this.ToE("Node", "节点"));
            this.AddTDTitle(this.ToE("Date", "日期"));
            this.AddTDTitle(this.ToE("Worker", "工作者"));
            this.AddTDTitle(this.ToE("Name", "名称"));
            this.AddTDTitle(this.ToE("Size", "大小"));
            this.AddTREnd();
            int ii = 1;
            foreach (FileManager fm in fms)
            {
                this.AddTR();
                this.AddTDIdx(ii++);
                this.AddTD(fm.FK_NodeText);
                this.AddTD(fm.RDT);
                this.AddTD(fm.FK_EmpText);
                this.AddTD("<a href='/DataUser/" + BP.Web.WebUser.FK_Dept + "/" + fm.OID + "." + fm.Ext + "' target=_bl ><img src='../Images/FileType/" + fm.Ext + ".gif' border=0/>" + fm.Name + "</a>");
                this.AddTD(fm.FileSize);
                this.AddTREnd();
            }
            this.AddTableEnd();
        }
        #endregion

        //判断当前人员是否管理员。
        if (WebUser.No == "admin" && wf.IsComplete == false)
        {
            this.Add("<BR><a href='../../WF/Do.aspx?ActionType=DeleteFlow&WorkID=" + workid + "&FK_Flow=" + wf.HisFlow.No + "' /><img src='../Images/Btn/Delete.gif' border=0/>强制删除流程:(请慎重操作，系统会记录在操作日志中)</a><BR>");
        }

        WorkNodes wns = wf.HisWorkNodesOfWorkID;

        this.Add("<p align='left' style='line-height: 100%' > &nbsp;&nbsp;&nbsp; " + wf.HisStartWork.Title + " " + this.ToE("Work", "工作") + ", " + this.ToE("From", "从") + " " + wf.HisStartWork.HisRec.Name + "  " + wf.HisStartWork.RDT + " " + wns.Count + " " + this.ToE("WorkStep", "工作步骤"));

        //this.AddHtml(wf.IsCompleteStr + "结束，<a href=\"javascript:WinOpen('Option.aspx?WorkID=" + workid + "&FK_Flow=" + wf.HisFlow.No + "');\" >流程操作</a>。现在历经如下步骤,详细报告如下:</p>");
        this.Add(wf.IsCompleteStr + this.ToE("WFRpt1", "结束，现在历经如下步骤,详细报告如下:") + "</p>");
        int i = 0;
        foreach (WorkNode wn in wns)
        {
            if (wn.HisWork.NodeState == 0)
            {
                this.Add("<BR><div align=left >&nbsp;&nbsp;&nbsp;&nbsp;" + this.ToE("CrrStopNode", "当前停留工作节点") + "：" + wn.HisNode.Name + " " + this.ToE("Worker", "工作人员") + " " + wn.HisWork.Emps + "。</div>");
                i++;
            }
        }

        if (i > 1)
        {
            this.Clear();
            this.AddMsgOfWarning(this.ToE("Warning", "预警"), "流程出现错误，实际当前工作是[" + wf.HisGenerWorkFlow.NodeName + "]，请关闭当前窗口体检此流程。");
            return;
        }

        ReturnWorks rws = new ReturnWorks();
        rws.Retrieve(ReturnWorkAttr.WorkID, wf.WorkID);

        ForwardWorks fws = new ForwardWorks();
        fws.Retrieve(ReturnWorkAttr.WorkID, wf.WorkID);

        this.BindWorkNodes(wns, rws, fws);

        //			this.AddHtml("</TD></TR><TR><TD>");
        //			 
        this.Add("<p align='left'> &nbsp;&nbsp;&nbsp;&nbsp;<hr> </p>");
        //  this.Add("<p align='center'> 流程系统 </p>");
        this.Add("<p align='center'> " + BP.DA.DataType.CurrentDataTime + " </p>");
        this.Add("<p align='center'>　</p>");
        this.Add("</TABLE>");
        return;
    }
    public void BindWorkNodes(WorkNodes wns, ReturnWorks rws, ForwardWorks fws)
    {
        int idx = 0;
        Int64 workid = 0;
        foreach (WorkNode wn in wns)
        {
            idx++;
            workid = wn.HisWork.OID;
            if (wn.HisWork.NodeState == 0)
            {
                GenerWorkerLists wls = new GenerWorkerLists(workid, wn.HisNode.NodeID);
                this.Add("<p align='left' style='line-height: 100%' >&nbsp;&nbsp;&nbsp;&nbsp;<a name='ND" + wn.HisNode.NodeID + "' >" + this.ToEP1("NStep", "@第{0}步", idx.ToString()) + "</a>" + wn.HisNode.Name + "，" + this.ToE("NodeState", "节点状态") + "：" + wn.HisWork.NodeStateText + "。");
                string msg = this.ToE("WFRpt0", "当前操作人员:");
                foreach (GenerWorkerList wl in wls)
                {
                    if (wl.IsEnable)
                        msg += wl.FK_Emp + wl.FK_EmpText + "、";
                    else
                        msg += "<strike>" + wl.FK_Emp + wl.FK_EmpText + "</strike>、";
                }
                this.Add(msg);
            }
            else
            {
                this.Add("<p align='left' style='line-height: 100%' >&nbsp;&nbsp;&nbsp;&nbsp;<a name='ND" + wn.HisNode.NodeID + "' >" + this.ToEP1("NStep", "@第{0}步", idx.ToString()) + "</a>" + wn.HisNode.Name + "，" + this.ToE("DealEmp", "处理人") + "：" + wn.HisWork.Rec + wn.HisWork.HisRec.Name + "，" + this.ToE("NodeState", "节点状态") + "：" + wn.HisWork.NodeStateText + "。");
            }

            Bills bills = new Bills();
            //   bills.rea

            BillTemplates reffunc = wn.HisNode.BillTemplates;
            if (reffunc.Count > 0)
            {
                this.Add("&nbsp;" + this.ToE("Bill", "单据") + "：");
                bool isMyBill = false;
                if (wn.HisWork.Rec == WebUser.No)
                    isMyBill = true;

                string year = DateTime.Now.Year.ToString();

                foreach (BillTemplate func in reffunc)
                {
                    if (wn.HisWork.NodeState == 0)
                    {
                        this.Add("<img src='../Images/Btn/Word.gif' /> " + func.Name);
                    }
                    else
                    {
                        //string file = year + "_" + WebUser.FK_Dept + "_" + func.No + "_" + workid + ".doc";
                        //string[] paths = file.Split('_');
                        //string path = paths[0] + "/" + paths[1] + "/" + paths[2] + "/";

                        //   string BillInfo = "<img src='./../Images/Btn/Word.gif' /><a href='./../FlowFile/Bill/" + path + file + "' target=_blank >" + func.Name + "</a>";

                        //string BillInfo = "<img src='./../Images/Btn/Word.gif' /><a href=\"javascript:WinOpen('Do.aspx?DoType=DownBill&MyPK="+func+"')\" target=_blank >" + func.Name + "</a>";
                        //this.Add(BillInfo);

                        //  string  = BP.SystemConfig.GetConfig("FtpPath") + file;
                        // path = BP.SystemConfig.AppSettings["FtpPath"].ToString() + year + "\\" + WebUser.FK_Dept + "\\" + func.No + "\\";
                        // this.Add("<img src='../Images/Btn/Word.gif' /><a href=\"javascript:Run('C:\\\\ds2002\\\\OpenBill.EXE','" + file + "','0');\"  >" + func.Name + "</a>");
                    }
                }
                this.Add("</p>");
            }

            if (Glo.IsQL)
                this.Add("<a href=\"javascript:WinOpen('DoQL.aspx?WorkID=" + wn.HisWork.OID + "&FK_Node=" + wn.HisNode.NodeID + "','sds' )\" ><img src='../Images/Btn/Do.gif' border=0 />质量考核</a>");

            this.Add("<div align=center>");
            this.ADDWork(wn.HisWork, rws, fws, wn.HisNode.NodeID);
            this.Add("</div>");
        }
    }
    protected void AddContral(string desc, string text)
    {
        this.Add("<td  class='FDesc' nowrap> " + desc + "</td>");
        this.Add("<td width='40%' class=TD>");
        if (text == "")
            text = "&nbsp;";
        this.Add(text);
        this.AddTDEnd();
    }
    /// <summary>
    /// 增加一个工作
    /// </summary>
    /// <param name="en"></param>
    /// <param name="rws"></param>
    /// <param name="fws"></param>
    /// <param name="nodeId"></param>
    public void ADDWork(Work en, ReturnWorks rws, ForwardWorks fws, int nodeId)
    {
        this.BindViewEn(en, "width=90%");

        foreach (ReturnWork rw in rws)
        {
            if (rw.ReturnToNode != nodeId)
                continue;

            this.AddBR();
            this.AddMsgOfInfo("退回信息：", rw.NoteHtml);
        }

        foreach (ForwardWork fw in fws)
        {
            if (fw.FK_Node != nodeId)
                continue;

            this.AddBR();
            this.AddMsgOfInfo("转发信息：", fw.NoteHtml);
        }


        string refstrs = "";
        if (en.IsEmpty)
        {
            refstrs += "";
            return;
        }

        string keys = "&PK=" + en.PKVal.ToString();
        foreach (Attr attr in en.EnMap.Attrs)
        {
            if (attr.MyFieldType == FieldType.Enum ||
                attr.MyFieldType == FieldType.FK ||
                attr.MyFieldType == FieldType.PK ||
                attr.MyFieldType == FieldType.PKEnum ||
                attr.MyFieldType == FieldType.PKFK)
                keys += "&" + attr.Key + "=" + en.GetValStrByKey(attr.Key);
        }
        Entities hisens = en.GetNewEntities;

        #region 加入他的明细

        EnDtls enDtls = en.EnMap.Dtls;
        if (enDtls.Count > 0)
        {
            foreach (EnDtl enDtl in enDtls)
            {
                string url = "WFRptDtl.aspx?RefPK=" + en.PKVal.ToString() + "&EnName=" + enDtl.Ens.GetNewEntity.ToString();
                int i = 0;
                try
                {
                    i = DBAccess.RunSQLReturnValInt("SELECT COUNT(*) FROM " + enDtl.Ens.GetNewEntity.EnMap.PhysicsTable + " WHERE " + enDtl.RefKey + "='" + en.PKVal + "'");
                }
                catch
                {
                    i = DBAccess.RunSQLReturnValInt("SELECT COUNT(*) FROM " + enDtl.Ens.GetNewEntity.EnMap.PhysicsTable + " WHERE " + enDtl.RefKey + "=" + en.PKVal);
                }

                if (i == 0)
                    refstrs += "[<a href=\"javascript:WinOpen('" + url + "','u8');\" >" + enDtl.Desc + "</a>]";
                else
                    refstrs += "[<a  href=\"javascript:WinOpen('" + url + "','u8');\" >" + enDtl.Desc + "-" + i + "</a>]";
            }
        }
        #endregion

        #region 加入一对多的实体编辑
        AttrsOfOneVSM oneVsM = en.EnMap.AttrsOfOneVSM;
        if (oneVsM.Count > 0)
        {
            foreach (AttrOfOneVSM vsM in oneVsM)
            {
                string url = "UIEn1ToM.aspx?EnsName=" + en.ToString() + "&AttrKey=" + vsM.EnsOfMM.ToString() + keys;
                int i = 0;
                try
                {
                    i = DBAccess.RunSQLReturnValInt("SELECT COUNT(*) FROM  as NUM " + vsM.EnsOfMM.GetNewEntity.EnMap.PhysicsTable + " WHERE " + vsM.AttrOfOneInMM + "='" + en.PKVal + "'");
                }
                catch
                {
                    i = DBAccess.RunSQLReturnValInt("SELECT COUNT(*) FROM  as NUM " + vsM.EnsOfMM.GetNewEntity.EnMap.PhysicsTable + " WHERE " + vsM.AttrOfOneInMM + "=" + en.PKVal);
                }

                if (i == 0)
                    refstrs += "[<a href='" + url + "' target='_blank' >" + vsM.Desc + "</a>]";
                else
                    refstrs += "[<a href='" + url + "' target='_blank' >" + vsM.Desc + "-" + i + "</a>]";
            }
        }
        #endregion

        #region 加入他门的相关功能
        //			SysUIEnsRefFuncs reffuncs = en.GetNewEntities.HisSysUIEnsRefFuncs ;
        //			if ( reffuncs.Count > 0  )
        //			{
        //				foreach(SysUIEnsRefFunc en1 in reffuncs)
        //				{
        //					string url="RefFuncLink.aspx?RefFuncOID="+en1.OID.ToString()+"&MainEnsName="+hisens.ToString()+keys;
        //					//int i=DBAccess.RunSQLReturnValInt("SELECT COUNT(*) FROM "+vsM.EnsOfMM.GetNewEntity.EnMap.PhysicsTable+" WHERE "+vsM.AttrOfMInMM+"='"+en.PKVal+"'");
        //					refstrs+="[<a href='"+url+"' target='_blank' >"+en1.Name+"</a>]";
        //					//refstrs+="编辑: <a href=\"javascript:window.open(RefFuncLink.aspx?RefFuncOID="+en1.OID.ToString()+"&MainEnsName="+ens.ToString()+"'> )\" > "+en1.Name+"</a>";
        //					//var newWindow= window.open( this.Request.ApplicationPath+'/Comm/'+'RefFuncLink.aspx?RefFuncOID='+OID+'&MainEnsName='+ CurrEnsName +CurrKeys,'chosecol', 'width=100,top=400,left=400,height=50,scrollbars=yes,resizable=yes,toolbar=false,location=false' );
        //					//refstrs+="编辑: <a href=\"javascript:EnsRefFunc('"+en1.OID.ToString()+"')\" > "+en1.Name+"</a>";
        //					//refstrs+="编辑:"+en1.Name+"javascript: EnsRefFunc('"+en1.OID.ToString()+"',)";
        //					//this.AddItem(en1.Name,"EnsRefFunc('"+en1.OID.ToString()+"')",en1.Icon);
        //				}
        //			}
        #endregion

        // 不知道为什么去掉。
        this.Add(refstrs);
    }
}
