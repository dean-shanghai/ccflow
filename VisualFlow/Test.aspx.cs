using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.WF;
using BP.Port;
using BP.Sys;
using BP.DA;
using BP.En;
using IBM.Data;
using IBM.Data.Informix;
using IBM.Data.Utilities;

public partial class TestFrm : BP.Web.PageBase
{
    public  string GetMac(string clientip)
    {
        string mac = "";
        System.Diagnostics.Process process = new System.Diagnostics.Process();
        //线程类，可以启动关闭一些线程
        process.StartInfo.FileName = "nbtstat";
        //传递给启动线程名，命令为nbtstat
        //此命令为cmd命令，可以根据其进一步获取mac等地址
        process.StartInfo.Arguments = "-a " + clientip;
        //设置nbtstat的命令参数
        process.StartInfo.UseShellExecute = false;
        //不启用外壳启动线程
        process.StartInfo.CreateNoWindow = true;
        //不创建新窗口启动线程
        process.Start();
        //启动
        string output = process.StandardOutput.ReadToEnd();
        //获取流全部内容
        int length = output.IndexOf("MAC Address = ");
        if (length > 0)
        {
            mac = output.Substring(length - 14, 17);
        }
        return mac;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        return;

        //BP.WF.DTS.LoadTemplete d = new BP.WF.DTS.LoadTemplete();
        //d.Do();
        //return;

        ////BP.WF.Demo.EmpDemo ed = new BP.WF.Demo.EmpDemo();
        ////ed.No = "zhangsan";
        ////ed.Retrieve();
        ////ed.Addr = "sdsfdsfsddddddddddddfffffffffffd";
        ////ed.Update();
        //return;


        //this.Response.Write(ed.Name + "<br>");
        //this.Response.Write(ed.Tel + "<br>");
        //this.Response.Write(ed.Addr + "<br>");

        //this.Response.Write(ed.FK_Dept+"<br>");
        //this.Response.Write(ed.FK_Dept_Text+"<br>");

        //this.Response.Write(ed.XB + "<br>");
        //this.Response.Write(ed.XB_Text + "<br>");
        //return;


     //   BP.WF.Demo.EmpDemo ed = new BP.WF.Demo.EmpDemo();
     ////  ed.CheckPhysicsTable();

     //   ed.No = "zhangsan";
     //   ed.Name = "张三";
     //   ed.FK_Dept = "02";
     //   ed.Email = "zhangsans@ss.com";
     //   ed.Addr = "shandong jinan";
     //   ed.Tel = "1866018232323";
     //   ed.XB = 1;
     //   ed.Insert();
     //   return;

     
        //BP.WF.Demo.Emp emp = new BP.WF.Demo.Emp();
        //BP.En.QueryObject q1o = new QueryObject(emp);
        //q1o.AddWhere("No", "zhangsan");
        //q1o.DoQuery();
        //this.Response.Write(q1o.SQL);

        //BP.WF.Demo.Emp emp = new BP.WF.Demo.Emp();
        //emp.No = "zhangsan";
        //emp.Retrieve();

        //this.Response.Write("<br>"+emp.Name);
        //this.Response.Write("<br>" + emp.FK_Dept);
        //this.Response.Write("<br>" + emp.FK_DeptText);
        //this.Response.Write("<br>" + emp.XB);
        //this.Response.Write("<br>" + emp.XBText);
        //return;

        //BP.WF.Demo.Emp emp = new BP.WF.Demo.Emp();
        //emp.CheckPhysicsTable();
        //emp.No = "zhangsan";
        //emp.Name = "张三";
        //emp.FK_Dept = "0102";
        //emp.XB = 1;
        //emp.Addr = "河南平顶山";
        //emp.Tel = "1233";
        //emp.Insert();
        return;

        this.Response.Write(this.GetMac("192.168.1.13"));
        return;
       
        Flow fl = new Flow();
        fl.CheckPhysicsTable();
        return;
         string s = fl.GenerNewNo;
         fl.No = s;
         fl.Name = "";
         fl.Insert();
         s = fl.GenerNewNo;
       return;

        Emps emps = new Emps();
        emps.RetrieveAll();
        return;


        GenerFH fh = new GenerFH(812);
        fh.DirectUpdate();
        return;

        //BP.WF.Node nd = new Node(199);
        //nd.Update();
        //return;


        BP.WF.DTS.LoadTemplete ld = new BP.WF.DTS.LoadTemplete();
        ld.Do();
        return;


        //Emp emp = new Emp("admin");
        //BP.Web.WebUser.SignInOfGener(emp);

        BP.Sys.SysEnums ses = new BP.Sys.SysEnums();
        QueryObject qo = new QueryObject(ses);
        qo.AddWhere(SysEnumAttr.Lang, "CH");
        qo.addAnd();
        qo.AddWhere(SysEnumAttr.EnumKey, "WFState");
        qo.DoQuery();

       // emps.RetrieveAll();
        //Flow fl = new Flow();
        //fl.CheckPhysicsTable();
        //fl.No = "001";
        //fl.Name = "name";
        //fl.IsOK = true;
        //fl.Update();
        return;


        for (int i = 0; i < 10; i++)
        {
            int workid = BP.DA.DBAccess.GenerOID();
            Int64 wid = BP.DA.DBAccess.GenerOID("WID");
        }
        //BP.DA.DBAccess.RunSQL("DROP TABLE WF_GenerWorkFlow");
        //GenerWorkFlow gwf = new GenerWorkFlow();
        //gwf.CheckPhysicsTable();
        return;

        // 把流程运行到最后的节点上去，并且结束流程。
        string file = @"C:\aa\开票流程-流程已完成.xls";
        string info = BP.WF.Glo.LoadFlowDataWithToSpecEndNode(file);
        this.Response.Write(info);
        return;

        // 把流程运行到指定的节点上去，并且不结束流程。
        string file1 = @"C:\aa\开票流程1.xls";
        string info1 = BP.WF.Glo.LoadFlowDataWithToSpecNode(file1);
        this.Response.Write(info1);
        return;
    }

    public void repariIt()
    {
        string sql = "SELECT * FROM WF_GenerWorkFlow WHERE Title LIKE '%2月28日%' and FK_Flow='079'";
        DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(sql);

        string xlsFile = @"C:\aa\开票流程2.xls";
        DataTable dtFrom = BP.DBLoad.GetTableByExt(xlsFile);

        Node mynd = new Node(7901);
        string info = "";
        foreach (DataRow dr in dt.Rows)
        {
            int fk_node = int.Parse(dr["FK_Node"].ToString());
            Node nd = new Node(fk_node);
            if (nd.IsEndNode == false)
                continue;

            int workid = int.Parse(dr["WorkID"].ToString());
            sql = "SELECT FK_Emp FROM WF_GenerWorkerList WHERE ispass=0 and WORKID=" + workid + " AND FK_Node=" + fk_node;
            string currEmp = BP.DA.DBAccess.RunSQLReturnString(sql);
            if (currEmp == null)
                throw new Exception(sql + "没有找到人员.");

            Work mywkStart = mynd.HisWork;
            mywkStart.OID = workid;
            if (mywkStart.RetrieveFromDBSources() == 0)
                continue;

            Work mywkEnd = nd.HisWork;
            mywkEnd.OID = workid;
            if (mywkEnd.RetrieveFromDBSources() == 0)
                continue;

            //处理没有复制上的数据。
            foreach (DataRow mydr in dtFrom.Rows)
            {
                string flowpk = mydr["FlowPK"].ToString();

                if (flowpk.Contains(mywkStart.GetValStrByKey("FlowPK")) == false)
                    continue;
                foreach (DataColumn dc in dtFrom.Columns)
                {
                    mywkStart.SetValByKey(dc.ColumnName, mydr[dc.ColumnName]);
                    mywkEnd.SetValByKey(dc.ColumnName, mydr[dc.ColumnName]);
                }
                mywkStart.DirectUpdate();
                mywkEnd.DirectUpdate();
            }

            WorkNode wn = new WorkNode(workid, fk_node);
            info += wn.NodeSend().ToMsgOfHtml();
        }

        this.Response.Write(info);
    }
}