using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BP.WF
{
    /// <summary>
    /// 消息类型
    /// </summary>
    public enum SendReturnMsgType
    {
        /// <summary>
        /// 消息
        /// </summary>
        Info,
        /// <summary>
        /// 系统消息
        /// </summary>
        SystemMsg
    }
    /// <summary>
    /// 消息标记
    /// </summary>
    public class SendReturnMsgFlag
    {
        /// <summary>
        /// 符合工作流程完成条件
        /// </summary>
        public const string MacthFlowOver = "MacthFlowOver";
        /// <summary>
        /// 当前工作[{0}]已经完成
        /// </summary>
        public const string CurrWorkOver = "CurrWorkOver";
        /// <summary>
        /// 符合完成条件,流程完成
        /// </summary>
        public const string FlowOverByCond = "FlowOverByCond";
        /// <summary>
        /// 到人员
        /// </summary>
        public const string ToEmps = "ToEmps";
        /// <summary>
        /// 合流结束
        /// </summary>
        public const string HeLiuOver = "HeLiuOver";
        /// <summary>
        /// 工作报告
        /// </summary>
        public const string WorkRpt = "WorkRpt";
        /// <summary>
        /// 启动节点
        /// </summary>
        public const string WorkStartNode = "WorkStartNode";
        /// <summary>
        /// 工作启动
        /// </summary>
        public const string WorkStart = "WorkStart";
        /// <summary>
        /// 流程结束
        /// </summary>
        public const string FlowOver = "FlowOver";
        /// <summary>
        /// 发送成功后的事件异常
        /// </summary>
        public const string SendSuccessMsgErr = "SendSuccessMsgErr";
        /// <summary>
        /// 发送成功信息
        /// </summary>
        public const string SendSuccessMsg = "SendSuccessMsg";
        /// <summary>
        /// 分流程信息
        /// </summary>
        public const string FenLiuInfo = "FenLiuInfo";
        /// <summary>
        /// 抄送消息
        /// </summary>
        public const string CCMsg = "CCMsg";
        /// <summary>
        /// 编辑接受者
        /// </summary>
        public const string EditAccepter = "EditAccepter";
        /// <summary>
        /// 新建流程
        /// </summary>
        public const string NewFlowUnSend = "NewFlowUnSend";
        /// <summary>
        /// 撤销发送
        /// </summary>
        public const string UnSend = "UnSend";
        /// <summary>
        /// 报表
        /// </summary>
        public const string Rpt = "Rpt";
        /// <summary>
        /// 发送时
        /// </summary>
        public const string SendWhen = "SendWhen";
        /// <summary>
        /// 当前流程结束
        /// </summary>
        public const string End = "End";
        /// <summary>
        /// 当前流程完成
        /// </summary>
        public const string OverCurr = "OverCurr";
        /// <summary>
        /// 流程方向信息
        /// </summary>
        public const string CondInfo = "CondInfo";
        /// <summary>
        /// 一个节点完成
        /// </summary>
        public const string OneNodeSendOver = "OneNodeSendOver";
        /// <summary>
        /// 单据信息
        /// </summary>
        public const string BillInfo = "BillInfo";

        #region 系统变量
        /// <summary>
        /// VarWorkID
        /// </summary>
        public const string VarWorkID = "VarWorkID";
        /// <summary>
        /// 当前节点ID
        /// </summary>
        public const string VarCurrNodeID = "VarCurrNodeID";
        /// <summary>
        /// 当前节点名称
        /// </summary>
        public const string VarCurrNodeName = "VarCurrNodeName";
        /// <summary>
        /// 到达节点ID
        /// </summary>
        public const string VarToNodeID = "VarToNodeID";
        /// <summary>
        /// 到达节点名称
        /// </summary>
        public const string VarToNodeName = "VarToNodeName";
        /// <summary>
        /// 接受人集合的名称(用逗号分开)
        /// </summary>
        public const string VarAcceptersName = "VarAcceptersName";
        /// <summary>
        /// 接受人集合的ID(用逗号分开)
        /// </summary>
        public const string VarAcceptersID = "VarAcceptersID";
        /// <summary>
        /// 接受人集合的ID Name(用逗号分开)
        /// </summary>
        public const string VarAcceptersNID = "VarAcceptersNID";
        #endregion 系统变量

    }
    /// <summary>
    /// 工作发送返回对象
    /// </summary>
    public class SendReturnObj
    {
        /// <summary>
        /// 消息标记
        /// </summary>
        public string MsgFlag = null;
        /// <summary>
        /// 消息类型
        /// </summary>
        public SendReturnMsgType HisSendReturnMsgType = SendReturnMsgType.Info;
        /// <summary>
        /// 消息内容
        /// </summary>
        public string MsgOfText = null;
        /// <summary>
        /// 消息内容Html
        /// </summary>
        public string MsgOfHtml = null;
        /// <summary>
        /// 发送消息
        /// </summary>
        public SendReturnObj()
        {
        }
    }
    /// <summary>
    /// 工作发送返回对象集合.
    /// </summary>
    public class SendReturnObjs:System.Collections.CollectionBase
    {
        #region 获取系统变量.
        public Int64 VarWorkID
        {
            get
            {
                foreach (SendReturnObj item in this)
                {
                    if (item.MsgFlag == SendReturnMsgFlag.VarWorkID)
                        return Int64.Parse(item.MsgOfText);
                }
                return 0;
            }
        }
        /// <summary>
        /// 到达节点ID
        /// </summary>
        public int VarToNodeID
        {
            get
            {
                foreach (SendReturnObj item in this)
                {
                    if (item.MsgFlag == SendReturnMsgFlag.VarCurrNodeID)
                        return int.Parse( item.MsgOfText);
                }
                return 0;
            }
        }
        /// <summary>
        /// 到达节点名称
        /// </summary>
        public string VarToNodeName
        {
            get
            {
                foreach (SendReturnObj item in this)
                {
                    if (item.MsgFlag == SendReturnMsgFlag.VarToNodeName)
                        return item.MsgOfText;
                }
                return "没有找到变量.";
            }
        }
        /// <summary>
        /// 到达的节点名称
        /// </summary>
        public string VarCurrNodeName
        {
            get
            {
                foreach (SendReturnObj item in this)
                {
                    if (item.MsgFlag == SendReturnMsgFlag.VarCurrNodeName)
                        return  item.MsgOfText;
                }
                return null;
            }
        }
        /// <summary>
        /// 接受人
        /// </summary>
        public string VarAcceptersName
        {
            get
            {
                foreach (SendReturnObj item in this)
                {
                    if (item.MsgFlag == SendReturnMsgFlag.VarAcceptersName)
                        return item.MsgOfText;
                }
                return null;
            }
        }
        /// <summary>
        /// 接受人IDs
        /// </summary>
        public string VarAcceptersID
        {
            get
            {
                foreach (SendReturnObj item in this)
                {
                    if (item.MsgFlag == SendReturnMsgFlag.VarAcceptersID)
                        return item.MsgOfText;
                }
                return null;
            }
        }
        #endregion

        /// <summary>
        /// 输出text消息
        /// </summary>
        public string OutMessageText = null;
        /// <summary>
        /// 输出html信息
        /// </summary>
        public string OutMessageHtml = null;

        /// <summary>
        /// 增加消息
        /// </summary>
        /// <param name="msgFlag">消息标记</param>
        /// <param name="msg">文本消息</param>
        /// <param name="msgOfHtml">html消息</param>
        /// <param name="type">消息类型</param>
        public void AddMsg(string msgFlag, string msg, string msgOfHtml, SendReturnMsgType type)
        {
            SendReturnObj obj = new SendReturnObj();
            obj.MsgFlag = msgFlag;
            obj.MsgOfText = msg;
            obj.MsgOfHtml = msgOfHtml;
            obj.HisSendReturnMsgType = type;
            this.InnerList.Add(obj);
        }
        /// <summary>
        /// 转化成text方式的消息，以方便识别不出来html的设备输出.
        /// </summary>
        /// <returns></returns>
        public string ToMsgOfText()
        {
            if (this.OutMessageText != null)
                return this.OutMessageText;
            string msg = "";
            foreach (SendReturnObj item in this)
            {
                if (item.HisSendReturnMsgType == SendReturnMsgType.SystemMsg)
                    continue;

                if (item.MsgOfText != null)
                {
                    msg += "@" + item.MsgOfText;
                    continue;
                }
            }
            return msg;
        }
        /// <summary>
        /// 转化成html方式的消息，以方便html的信息输出.
        /// </summary>
        /// <returns></returns>
        public string ToMsgOfHtml()
        {
            if (this.OutMessageHtml != null)
                return this.OutMessageHtml;

            string msg = "";
            foreach (SendReturnObj item in this)
            {
                if (item.HisSendReturnMsgType == SendReturnMsgType.SystemMsg)
                    continue;

                if (item.MsgOfHtml != null)
                {
                    msg += "@" + item.MsgOfHtml;
                    continue;
                }

                if (item.MsgOfText != null)
                {
                    msg += "@" + item.MsgOfText;
                    continue;
                }
            }
            msg = msg.Replace("@@", "@");
            msg = msg.Replace("@@", "@");
            return msg;
        }
    }
}
