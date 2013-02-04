using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.WF;
using BP.En;
using BP.DA;
using BP.Web;
using System.Data;
using System.Collections;

namespace BP.CT.T2Cond
{
    /// <summary>
    /// 测试 按SQL 做为转向条件
    /// </summary>
    public class TestWorkIDRepaed : TestBase
    {
        /// <summary>
        /// 测试 按SQL 做为转向条件
        /// </summary>
        public TestWorkIDRepaed()
        {
            this.Title = "测试WorkID生成重复的问题";
            this.DescIt = "以测试用例的-001流程做测试用例.";
            this.EditState = EditState.OK;
        }
        /// <summary>
        /// 执行
        /// </summary>
        public override void Do()
        {
            string fk_flow = "001";
            Flow fl = new Flow(fk_flow);

            #region   zhoutianjiao 登录.
            BP.WF.Dev2Interface.Port_Login("zhoutianjiao");

            //创建空白工作, 发起开始节点.
            SendReturnObjs objs = BP.WF.Dev2Interface.Node_StartWork(fk_flow, null, null, 0, null);
            Int64 workid = objs.VarWorkID;

            //用第二个人员登录.
            BP.WF.Dev2Interface.Port_Login(objs.VarAcceptersID);
            objs = BP.WF.Dev2Interface.Node_SendWork(fk_flow,workid);

            //让zhoutianjiao 在执行一次。
            BP.WF.Dev2Interface.Port_Login("zhoutianjiao");

            objs = BP.WF.Dev2Interface.Node_StartWork(fk_flow, null, null, 0, null);
            Int64 workid2 = objs.VarWorkID;
            if (workid2 == workid)
                throw new Exception("@生成的WorkID重复.");

            #endregion
        }
    }
}
