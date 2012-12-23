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
using BP.En;
using BP.DA;
using BP.Port;
using BP.Web;

public partial class Face_WhySendToThem : System.Web.UI.Page
{
    public int NodeID
    {
        get
        {
            return int.Parse(this.Request.QueryString["NodeID"]);
        }
    }
    public Int64 WorkID
    {
        get
        {
            return Int64.Parse(this.Request.QueryString["WorkID"]);
        }
    }
    public int ToNodeID
    {
        get
        {
            return int.Parse(this.Request.QueryString["ToNodeID"]);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = "分析此问题有助于您，查看流程节点是否正确。";

        this.Pub1.AddFieldSet("为什么会发给下列人员");

        this.Pub1.Add("当前操作人员："+BP.Web.WebUser.No+" 操作员权限部门："+WebUser.FK_Dept+ " " +WebUser.FK_DeptName );
        this.Pub1.AddB("<br>当前操作员部门集合如下：<hr>");
        EmpDepts eds = new EmpDepts();
        eds.Retrieve(EmpDeptAttr.FK_Emp, WebUser.No);
        foreach (EmpDept ed in eds)
        {
            this.Pub1.AddLi(ed.FK_Dept + "" + ed.FK_DeptT);
        }

        this.Pub1.AddB("<br>当前操作员岗位集合如下：<hr>");
        EmpStations ets = new EmpStations();
        ets.Retrieve(EmpDeptAttr.FK_Emp, WebUser.No);
        foreach (EmpStation ed in ets)
        {
            this.Pub1.AddLi(ed.FK_Station + "" + ed.FK_StationT);
        }

        Node toNd = new Node(this.ToNodeID);

        this.Pub1.AddB("<br>接受的工作节点：" +toNd.NodeID  +" "+ toNd.Name );
        this.Pub1.AddB("<br>节点岗位集合：<hr>");
        NodeStations ndss = new NodeStations();
        ndss.Retrieve(NodeStationAttr.FK_Node, this.ToNodeID);
        foreach (NodeStation sta in ndss)
        {
            this.Pub1.AddLi(sta.FK_Station + " " + sta.FK_StationT);
        }

        this.Pub1.AddB("<br>接受的人员信息：");
        this.Pub1.AddB("<br>人员部门集合：<hr>");

        GenerWorkerLists wls = new GenerWorkerLists();
        wls.Retrieve(GenerWorkerListAttr.FK_Node, this.ToNodeID, GenerWorkerListAttr.WorkID, this.WorkID);
        foreach (GenerWorkerList wl in wls)
        {
            this.Pub1.AddLi(wl.FK_Emp + " " + wl.FK_EmpText + "个人信息如下:");
            EmpStations myess = new EmpStations();
            myess.Retrieve(EmpStationAttr.FK_Emp, wl.FK_Emp);
            foreach (EmpStation myes in myess)
            {
                this.Pub1.AddBR(myes.FK_Station + myes.FK_StationT);
            }

            EmpDepts mydepts = new EmpDepts();
            mydepts.Retrieve(EmpStationAttr.FK_Emp, wl.FK_Emp);
            foreach (EmpDept myde in mydepts)
            {
                this.Pub1.AddBR(myde.FK_Dept + myde.FK_DeptT);
            }

        }

        this.Pub1.AddFieldSetEnd(); 
        return;


        //this.Pub1.AddTable();
        //this.Pub1.AddCaptionLeftTX("为什么会发给下列人员？");

        //this.Pub1.AddTR();
        //this.Pub1.AddTDBegin();

        //this.Pub1.AddTDBegin();
        //this.Pub1.AddH1

    }
}
