using System;
using System.Collections.Generic;
using System.Text;
using BP.WF;
using BP.En;
using BP.DA;
using BP.Web;
using System.Data;
using System.Collections;
using BP.CT;

namespace BP.CT.T1Return
{
    public class Test001Return : TestBase
    {
        /// <summary>
        /// 测试退回
        /// </summary>
        public Test001Return()
        {
            this.Title = "财务报销流程的退回功能";
            this.DescIt = "发送的退回，与原路返回方式的退回.";
            this.EditState = EditState.OK;
        }
        /// <summary>
        /// 说明 ：此测试针对于演示环境中的 001 流程编写的单元测试代码。
        /// 涉及到了: 创建，发送，撤销，方向条件、退回等功能。
        /// </summary>
        public override void Do()
        {
            string fk_flow = "001";
            string startUser = "zhoutianjiao";

            Flow fl = new Flow(fk_flow);

            //让周天娇登录, 在以后，就可以访问WebUser.No, WebUser.Name 。
            BP.WF.Dev2Interface.Port_Login(startUser);

            //创建空白工作, 发起开始节点.
            Int64 workid = BP.WF.Dev2Interface.Node_CreateBlankWork(fk_flow, null, null, WebUser.No, null, 0);

            //执行发送，并获取发送对象,.
            SendReturnObjs objs = BP.WF.Dev2Interface.Node_SendWork(fk_flow, workid);

            //下一个工作者.
            string nextUser = objs.VarAcceptersID;
            // 下一个发送的节点ID
            int nextNodeID = objs.VarToNodeID;

            // 让 nextUser = qifenglin 登录.
            BP.WF.Dev2Interface.Port_Login(nextUser);

            //获取第二个节点上的退回集合.
            DataTable dt = BP.WF.Dev2Interface.DB_GenerWillReturnNodes(objs.VarToNodeID, workid, 0);

            #region 检查获取第二步退回的节点数据是否符合预期.
            if (dt.Rows.Count != 1)
                throw new Exception("@在第二个节点是获取退回节点集合时，不符合数据预期,应该只能获取一个退回节点，现在是:" + dt.Rows.Count);

            int nodeID = int.Parse(dt.Rows[0]["No"].ToString());
            if (nodeID != 101)
                throw new Exception("@在第二个节点是获取退回节点集合时，被退回的点应该是101");

            string RecNo = dt.Rows[0]["Rec"].ToString();
            if (RecNo != startUser)
                throw new Exception("@在第二个节点是获取退回节点集合时，被退回人应该是" + startUser + ",现在是" + RecNo);
            #endregion 检查获取第二步退回的节点数据是否符合预期.

            //在第二个节点执行退回.
            BP.WF.Dev2Interface.Node_ReturnWork(fk_flow, workid,0,objs.VarToNodeID, 101, "退回测试", false);

            #region 检查退回后的数据完整性.
            GenerWorkFlow gwf = new GenerWorkFlow(workid);
            Node nd = new Node(gwf.FK_Node);
            Work wk = nd.HisWork;
            wk.OID = workid;
            wk.Retrieve();
            if (wk.NodeState != NodeState.Back)
                throw new Exception("@执行退回，流程状态应该是退回,现在是:" + gwf.WFState.ToString());

            if (gwf.FK_Node != 101)
                throw new Exception("@执行退回，当前节点应该是101, 现在是" + gwf.FK_Node.ToString());

            //sql = "SELECT WFState from nd1rpt where oid=" + workid;
            //int wfstate = DBAccess.RunSQLReturnValInt(sql, -1);
            //if (wfstate != (int)WFState.ReturnSta)
            //    throw new Exception("@在第二个节点退回后rpt数据不正确，流程状态应该是退回,现在是:" + wfstate);

            sql = "SELECT FlowEndNode from nd1rpt where oid=" + workid;
            int FlowEndNode = DBAccess.RunSQLReturnValInt(sql, -1);
            if (FlowEndNode != 101)
                throw new Exception("@在第二个节点退回后rpt数据不正确，最后的节点应该是101,现在是:" + FlowEndNode);

            #endregion 检查退回后的数据完整性.

            //让发起人登录，并发送到部门经理审批.
            BP.WF.Dev2Interface.Port_Login(startUser);
            objs = BP.WF.Dev2Interface.Node_SendWork(fk_flow, workid);

            //让第二步骤的qifengin登录并处理它，发送到总经理审批。
            BP.WF.Dev2Interface.Port_Login(objs.VarAcceptersID);

            Hashtable ht = new Hashtable();
            ht.Add("HeJiFeiYong", 999999); //金额大于1w 就让它发送到总经理审批节点上去.
            objs = BP.WF.Dev2Interface.Node_SendWork(fk_flow, workid, ht);

            // 让zhoupeng登录, 并执行退回.
            BP.WF.Dev2Interface.Port_Login(objs.VarAcceptersID);

            //获取第三个节点上的退回集合.
            dt = BP.WF.Dev2Interface.DB_GenerWillReturnNodes(objs.VarToNodeID, workid, 0);

            #region 检查获取第二步退回的节点数据是否符合预期.
            if (dt.Rows.Count != 2)
                throw new Exception("@在第三个节点是获取退回节点集合时，不符合数据预期,应该 获取2个退回节点，现在是:" + dt.Rows.Count);

            if (dt.Rows[0][0].ToString() != "101")
                throw new Exception("@应该是101,现在是:" + dt.Rows[0][0].ToString());

            if (dt.Rows[1][0].ToString() != "102")
                throw new Exception("@应该是102,现在是:" + dt.Rows[0][0].ToString());

            #endregion 检查获取第二步退回的节点数据是否符合预期.

            //在第3个节点执行退回.
            BP.WF.Dev2Interface.Node_ReturnWork(fk_flow, workid, 0, objs.VarToNodeID, 101, "总经理-退回测试", false);

            #region 检查总经理-退回后的数据完整性.
            //gwf = new GenerWorkFlow(workid);
            //if (gwf.WFState != WFState.ReturnSta)
            //    throw new Exception("@执行退回，流程状态应该是退回,现在是:" + gwf.WFState.ToString());

            if (gwf.FK_Node != 101)
                throw new Exception("@执行退回，当前节点应该是101, 现在是" + gwf.FK_Node.ToString());

            sql = "SELECT WFState from nd1rpt where oid=" + workid;
            //wfstate = DBAccess.RunSQLReturnValInt(sql, -1);
            //if (wfstate != (int)WFState.ReturnSta)
            //    throw new Exception("@在第二个节点退回后rpt数据不正确，流程状态应该是退回,现在是:" + wfstate);

            sql = "SELECT FlowEndNode from nd1rpt where oid=" + workid;
            FlowEndNode = DBAccess.RunSQLReturnValInt(sql, -1);
            if (FlowEndNode != 101)
                throw new Exception("@在第二个节点退回后rpt数据不正确，最后的节点应该是101,现在是:" + FlowEndNode);
            #endregion 检查退回后的数据完整性.

            // 让发起人登录, 并执行发送.
            BP.WF.Dev2Interface.Port_Login(startUser);
            objs = BP.WF.Dev2Interface.Node_SendWork(fk_flow, workid);

            // 让部门经理登录登录, 并执行发送.
            BP.WF.Dev2Interface.Port_Login(objs.VarAcceptersID);
            ht = new Hashtable();
            ht.Add("HeJiFeiYong", 999999);
            objs = BP.WF.Dev2Interface.Node_SendWork(fk_flow, workid, ht);

            //让总经理登录.
            BP.WF.Dev2Interface.Port_Login(objs.VarAcceptersID);

            //执行退回并原路返回.
            BP.WF.Dev2Interface.Node_ReturnWork(fk_flow, workid,0, objs.VarToNodeID, 101, "总经理-退回并原路返回-测试", true);

            //让发起人登录, 此人在发起时，应该直接发送给第三个节点的退回人,就是zhoupeng才正确.
            BP.WF.Dev2Interface.Port_Login(startUser);
            objs=BP.WF.Dev2Interface.Node_SendWork(fk_flow, workid);

            #region 开始检查数据是否完整.
            if (objs.VarAcceptersID != "zhoupeng")
                throw new Exception("@退回并原路返回错误，应该发送给zhoupeng，但是目前发送到了:"+objs.VarAcceptersID);
            #endregion

        }
    }
}
