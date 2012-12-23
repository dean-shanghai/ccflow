using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.ServiceModel;
using System.Web;
using System.Web.Services;
using BP;
using BP.Sys;
using BP.DA;
using BP.WF;
using BP.En;
using BP.Port;
using BP.Web;
using Silverlight.DataSetConnector;
using System.Data;
using System.Data.SqlClient;
using System.IO;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[ServiceBehavior(IncludeExceptionDetailInFaults = true)]
public class WSDesigner : WSBase
{
    #region 建模有关的方法
    [WebMethod]
    public string GenerOrgModel()
    {
        return null;

        //BP.Port.Org.Dept dept = new BP.Port.Org.Dept();
        //dept.CheckPhysicsTable();

        //BP.Port.Org.Station sta = new BP.Port.Org.Station();
        //sta.CheckPhysicsTable();

        //BP.Port.Org.Emp emp = new BP.Port.Org.Emp();
        //emp.CheckPhysicsTable();

        //DataSet ds = new DataSet();
        //string sql = "SELECT * FROM Port_Dept";
        //DataTable dt = DBAccess.RunSQLReturnTable(sql);
        //dt.TableName = "Depts";
        //ds.Tables.Add(dt);

        //sql = "SELECT * FROM Port_Station";
        //dt = DBAccess.RunSQLReturnTable(sql);
        //dt.TableName = "Stations";
        //ds.Tables.Add(dt);

        //sql = "SELECT * FROM WF_Emp";
        //dt = DBAccess.RunSQLReturnTable(sql);
        //dt.TableName = "Emps";
        //ds.Tables.Add(dt);
        //return Connector.ToXml(ds);
    }
    #endregion

    /// <summary>
    /// 执行功能返回信息
    /// </summary>
    /// <param name="doType"></param>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <param name="v3"></param>
    /// <param name="v4"></param>
    /// <param name="v5"></param>
    /// <returns></returns>
    [WebMethod(EnableSession = false)]
    public string DoType(string doType, string v1, string v2, string v3, string v4, string v5)
    {
        try
        {
            switch (doType)
            {
                case "AdminLogin":
                    try
                    {
                        Emp emp = new Emp();
                        emp.No = v1;
                        emp.RetrieveFromDBSources();
                        if (emp.Pass == v2)
                            return null;
                        return "error password.";
                    }
                    catch (Exception ex)
                    {
                        return ex.Message;
                    }
                case "DeleteFrmSort":
                    FrmSort fs = new FrmSort();
                    fs.No = v1;
                    fs.Delete();
                    return null;
                case "DeleteFrm":
                case "DelFrm":
                    MapData md = new MapData();
                    md.No = v1;
                    md.Delete();
                    return null;
                case "InitDesignerXml":
                    string path = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "\\Data\\Xml\\Designer.xml";
                    DataSet ds = new DataSet();
                    ds.ReadXml(path);
                    ds = this.TurnXmlDataSet2SLDataSet(ds);
                    return Connector.ToXml(ds);
                default:
                    throw new Exception("没有判断的，功能编号" + doType);
            }
        }
        catch (Exception ex)
        {
            BP.DA.Log.DefaultLogWriteLineError("执行错误，功能编号" + doType + " error:" + ex.Message);
            throw new Exception("执行错误，功能编号" + doType + " error:" + ex.Message);
        }
    }
    /// <summary>
    /// 根据workID获取工作列表
    /// FK_Node 节点ID
    /// rdt 记录日期，也是工作接受日期。
    /// sdt 应完成日期.
    /// FK_emp 操作员编号。
    /// EmpName 操作员名称.
    /// </summary>
    /// <param name="workid">workid</param>
    /// <returns></returns>
    [WebMethod(EnableSession = false)]
    public string GetDTOfWorkList(string fk_flow, string workid)
    {
        try
        {
            string sql = " SELECT A.FK_Node, A.RDT,A.SDT,A.FK_Emp,b.Name as EmpName";
            sql += " FROM WF_GenerWorkerList A, WF_Emp B WHERE A.FK_Emp=b.No AND A.IsEnable=1 AND A.WorkID=" + workid + " Order by A.SDT";
            
            DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
            if (dt.Rows.Count == 0)
            {
                sql = "SELECT A.FK_Node, A.RDT,A.CDT, A.CDT as SDT, A.Rec AS FK_Emp ,b.Name as EmpName";
                sql += " FROM V" + fk_flow + " A, WF_Emp B WHERE A.Rec=b.No AND A.OID=" + workid;
                dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
            }

            DataSet ds = new DataSet();
            sql = "SELECT NDFrom, NDTo,ActionType,Msg,RDT FROM WF_TRACK WHERE WorkID=" + workid + "   ";
            sql += " UNION ";
            sql = "SELECT NDFrom, NDTo,ActionType,Msg,RDT FROM WF_TrackTemp WHERE WorkID=" + workid + "   ";

            DataTable mydt = BP.DA.DBAccess.RunSQLReturnTable(sql);
            mydt.TableName = "WF_Track";
            ds.Tables.Add(mydt);
            ds.Tables.Add(dt);
            return Connector.ToXml(ds);
        }
        catch (Exception ex)
        {
            BP.DA.Log.DefaultLogWriteLineError("GetDTOfWorkList发生了错误 paras:" + fk_flow + "\t" + workid + ex.Message);
            return null;
        }
    }
    /// <summary>
    /// 让admin 登陆
    /// </summary>
    /// <param name="lang">当前的语言</param>
    /// <returns>成功则为空，有异常时返回异常信息</returns>
    [WebMethod(EnableSession = true)]
    public string LetAdminLogin(string lang, bool islogin)
    {
        try
        {
            if (islogin)
            {
                Emp emp = new Emp("admin");
                WebUser.SignInOfGener(emp, lang, "admin", true, false);
            }
        }
        catch (Exception exception)
        {
            return exception.Message;
        }
        return string.Empty;
    }
    [WebMethod(EnableSession = true)]
    [Obsolete]
    public string GetFlowBySort(string sort)
    {
        DataSet ds = new DataSet();
        ds = BP.DA.DBAccess.RunSQLReturnDataSet("select No,Name,FK_FlowSort from WF_Flow");
        return Connector.ToXml(ds);
    }
    /// <summary>
    /// 岗位人员
    /// </summary>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public string GetStationEmps()
    {
        DataSet ds = new DataSet();
        ds = BP.DA.DBAccess.RunSQLReturnDataSet(@"select e.No as EmpNo, e.Name as EmpName,s.No,s.Name  from Port_Station s,Port_EmpStation es ,Port_Emp e 
where s.No=es.FK_Station and e.No=es.FK_Emp");
        return Connector.ToXml(ds);
    }

    [WebMethod(EnableSession = true)]
    public string Do(string doWhat, string para1, bool isLogin)
    {
        // 如果admin账户登陆时有错误发生，则返回错误信息
        var result = LetAdminLogin("CH", isLogin);
        if (string.IsNullOrEmpty(result)==false)
        {
            return result;
        }

        switch (doWhat)
        {
            case "GenerFlowTemplete":
                Flow temp = new BP.WF.Flow(para1);
                return null;
            case "NewFrmSort":
                BP.Sys.FrmSort frmSort = null;
                try
                {
                    frmSort = new FrmSort();
                    frmSort.No="01";
                    frmSort.Name = para1;
                    frmSort.No = frmSort.GenerNewNo;
                    frmSort.Insert();
                    return null;
                }
                catch (Exception ex)
                {
                    return "Do Method NewFlowSort Branch has a error , para:\t" + para1 + ex.Message;
                }
            case "NewFlowSort":
                BP.WF.FlowSort fs = null;
                try
                {
                    fs = new FlowSort();
                    fs.Name = para1;
                    fs.No = fs.GenerNewNo;
                    fs.Insert();
                    return fs.No;
                }
                catch (Exception ex)
                {
                    BP.DA.Log.DefaultLogWriteLineError("Do Method NewFlowSort Branch has a error , para:\t" + para1 + ex.Message);
                    return null;
                }
            case "EditFlowSort":
                try
                {
                    var para = para1.Split(',');
                    fs = new FlowSort(para[0]);
                    fs.Name = para[1];
                    fs.Save();
                    return fs.No;
                }
                catch (Exception ex)
                {
                    BP.DA.Log.DefaultLogWriteLineError("Do Method EditFlowSort Branch has a error , para:\t" + para1 + ex.Message);
                    return null;
                }
            case "NewFlow":
                Flow fl = new Flow();

                try
                {
                    if (null == para1)
                    {
                        fl.Name = "新建流程";
                    }
                    else if (para1.IndexOf(',') < 0)
                    {
                        fl.FK_FlowSort = para1;
                        fl.Name = "新建流程";
                    }
                    else
                    {
                        fl.FK_FlowSort = para1.Split(',')[0];
                        fl.Name = para1.Split(',')[1];
                    }

                    fl.DoNewFlow();
                    return fl.No + ";" + fl.Name;
                }
                catch (Exception ex)
                {
                    BP.DA.Log.DefaultLogWriteLineError("Do Method NewFlow Branch has a error , para:\t" + para1 + ex.Message);
                    return ex.Message;

                }
            case "DelFlow":
                BP.WF.Flow fl1 = new BP.WF.Flow(para1);
                try
                {
                    fl1.DoDelete();
                    return null;
                }
                catch (Exception ex)
                {
                    BP.DA.Log.DefaultLogWriteLineError("Do Method DelFlow Branch has a error , para:\t" + para1 + ex.Message);
                    return ex.Message;
                }
            case "DelLable":
                BP.WF.LabNote ln = new BP.WF.LabNote(para1);
                try
                {
                    ln.Delete();
                }
                catch (Exception ex)
                {
                    BP.DA.Log.DefaultLogWriteLineError("Do Method DelLable Branch has a error , para:\t" + para1 + ex.Message);
                }
                return null;
            case "DelFlowSort":
                try
                {
                    FlowSort delfs = new FlowSort(para1);
                    delfs.Delete();
                }
                catch (Exception ex)
                {
                    BP.DA.Log.DefaultLogWriteLineError("Do Method DelFlowSort Branch has a error , para:\t" + para1 + ex.Message);
                }

                return null;
            case "NewNode":
                try
                {
                    BP.WF.Flow fl11 = new BP.WF.Flow(para1);
                    BP.WF.Node node = new BP.WF.Node();
                    node.FK_Flow = "";
                    node.X = 0;
                    node.Y = 0;
                    node.Insert();
                }
                catch (Exception ex)
                {
                    BP.DA.Log.DefaultLogWriteLineError("Do Method NewNode Branch has a error , para:\t" + para1 + ex.Message);
                }

                return null;
            case "DelNode":
                try
                {
                    if (!string.IsNullOrEmpty(para1))
                    {
                        BP.WF.Node delNode = new BP.WF.Node(int.Parse(para1));
                        delNode.Delete();
                    }
                }
                catch (Exception ex)
                {
                    BP.DA.Log.DefaultLogWriteLineError("Do Method DelNode Branch has a error , para:\t" + para1 + ex.Message);
                }
                return null;
            case "NewLab":
                BP.WF.LabNote lab = new BP.WF.LabNote(); ;
                try
                {
                    lab.FK_Flow = para1;
                    lab.MyPK = BP.DA.DBAccess.GenerOID().ToString();
                    lab.Insert();

                }
                catch (Exception ex)
                {
                    BP.DA.Log.DefaultLogWriteLineError("Do Method NewLab Branch has a error , para:\t" + para1 + ex.Message);
                }
                return lab.MyPK;
            case "DelLab":
                try
                {
                    BP.WF.LabNote dellab = new BP.WF.LabNote();
                    dellab.MyPK = para1;
                    dellab.Delete();
                }
                catch (Exception ex)
                {
                    BP.DA.Log.DefaultLogWriteLineError("Do Method DelLab Branch has a error , para:\t" + para1 + ex.Message);
                }

                return null;
            case "GetSettings":
                return SystemConfig.AppSettings[para1];
            case "GetFlows":
                string sqls = "SELECT NO,NAME FROM WF_FlowSort";
                sqls += "@SELECT No,Name,FK_FlowSort FROM WF_Flow";
                return RunSQLReturnTableS(sqls);
            case "SaveFlowFrm":
                Entity en = null;
                try
                {
                    AtPara ap = new AtPara(para1);
                    string enName = ap.GetValStrByKey("EnName");
                    string pk = ap.GetValStrByKey("PKVal");
                    en = ClassFactory.GetEn(enName);
                    en.ResetDefaultVal();

                    if (en == null)
                        throw new Exception("无效的类名:" + enName);

                    if (string.IsNullOrEmpty(pk) == false)
                    {
                        en.PKVal = pk;
                        en.RetrieveFromDBSources();
                    }

                    foreach (string key in ap.HisHT.Keys)
                    {
                        if (key == "PKVal")
                            continue;
                        en.SetValByKey(key, ap.HisHT[key].ToString().Replace('^', '@'));
                    }
                    en.Save();
                    return en.PKVal as string;
                }
                catch (Exception ex)
                {
                    if (en != null)
                        en.CheckPhysicsTable();
                    return "Error:" + ex.Message;
                }
            case "ReleaseToFTP":
                // 暂时注释，下次更新ftp功能时会得新编译 。
                //var args = para1.Split(',');
                //var binaryData = Convert.FromBase64String(args[1]);
                //var imageFilePath = Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath) + "/Temp/" + args[0] + ".jpg";
                //if (File.Exists(imageFilePath))
                //{
                //    File.Delete(imageFilePath);
                //}
                //System.IO.Directory.CreateDirectory(
                //    Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath) + "/Temp");
                //var stream = new System.IO.FileStream(imageFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                //stream.Write(binaryData, 0, binaryData.Length);
                //stream.Close();
                //var xmlFilePath = FlowTemplete_Gener(args[0], true);
                //string remoteDr = "/" + ConfigurationSettings.AppSettings["UserIdentifier"];;
                //var ftp = new FtpConnection();
                //try
                //{
                //    string ftpIP = ConfigurationSettings.AppSettings["FTPServerIP"];
                //    string email = ConfigurationSettings.AppSettings["FTPUserEmail"];

                //    Session session = new Session();
                //    session.Server = ftpIP;


                //    session.Connect("anonymous", "someone@somewhere.com");
                //    ftp.Connect(ftpIP, "anonymous", email);
                //    remoteDr = remoteDr;
                //    if(!ftp.DirectoryExist(remoteDr))
                //    {
                //        ftp.CreateDirectory(remoteDr);
                //    }
                //    ftp.SetCurrentDirectory(remoteDr);
                //    ftp.PutFile(imageFilePath, remoteDr);
                //    return string.Empty;//上传成功

                //}
                //catch (Exception err)
                //{
                //    return err.Message;//上传失败
                //}
                //finally
                //{
                //    ftp.Close();
                //}
                return string.Empty;
            default:
                throw null;
        }
    }
    /// <summary>
    /// 创建一个节点
    /// </summary>
    /// <param name="fk_flow">流程编号</param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns>返回节点编号</returns>
    [WebMethod(EnableSession = true)]
    public int DoNewNode(string fk_flow, int x, int y, string nodeName, bool isLogin)
    {
        LetAdminLogin("CH", isLogin);
        if (string.IsNullOrEmpty(fk_flow))
            return 0;

        Flow fl = new Flow(fk_flow);
        try
        {
            BP.WF.Node nf =fl.DoNewNode(x, y);
            nf.Name = nodeName;
            nf.Save();
            return nf.NodeID;
        }
        catch
        {
            return 0; 
        }
    }
    /// <summary>
    /// 删除一个连接线
    /// </summary>
    /// <param name="from">从节点</param>
    /// <param name="to">到节点</param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public bool DoDropLine(int from, int to)
    {
        BP.WF.Direction dir = new BP.WF.Direction();
        dir.Node = from;
        dir.ToNode = to;
        dir.Delete();
        return true;
    }
    /// <summary>
    /// 创建一个标签
    /// </summary>
    /// <param name="fk_flow">流程编号</param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns>返回标签编号</returns>
    [WebMethod(EnableSession = true)]
    public string DoNewLabel(string fk_flow, int x, int y, string name, string lableId)
    {
        LabNote lab = new LabNote();
        lab.FK_Flow = fk_flow;
        lab.X = x;
        lab.Y = y;
        if (string.IsNullOrEmpty(lableId))
        {
            lab.MyPK = BP.DA.DBAccess.GenerOID().ToString();
        }
        else
        {
            lab.MyPK = lableId;
        }
        lab.Name = name;
        try
        {
            lab.Save();
        }
        catch
        {
        }
        return lab.MyPK;
    }
    /// <summary>
    /// load flow templete.
    /// </summary>
    /// <param name="fk_flowSort">流程类别编号</param>
    /// <param name="path">模板文件路径</param>
    [WebMethod(EnableSession = true)]
    public string FlowTemplete_Load(string fk_flowSort, string path, bool islogin)
    {
        try
        {
            LetAdminLogin("CH", islogin);
            var result = Flow.DoLoadFlowTemplate(fk_flowSort, path, ImpFlowTempleteModel.AsTempleteFlowNo);
            return string.Format("{0},{1},{2}", fk_flowSort, result.No, result.Name);
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
    /// <summary>
    /// 保存流程
    /// </summary>
    /// <param name="fk_flow"></param>
    /// <param name="nodes"></param>
    /// <param name="dirs"></param>
    /// <param name="labes"></param>
    [WebMethod(EnableSession = true)]
    public string DoSaveFlow(string fk_flow, string nodes, string dirs, string labes)
    {
        LetAdminLogin("CH", true);
        try
        {
            //处理方向。
            string sql = "Delete FROM WF_Direction WHERE FK_Flow='" + fk_flow + "'";
            DBAccess.RunSQL(sql);

            string[] mydirs = dirs.Split('~');
            foreach (string dir in mydirs)
            {
                if (string.IsNullOrEmpty(dir))
                    continue;

                AtPara ap = new AtPara(dir);

                string dots = ap.GetValStrByKey("Dots").Replace('#', '@');
                sql = "INSERT INTO WF_Direction (Node,ToNode,FK_Flow,DirType,IsCanBack,Dots,MyPK) VALUES ("
                    + ap.GetValIntByKey("Node") + "," + ap.GetValIntByKey("ToNode") + ",'" + fk_flow
                    + "'," + ap.GetValIntByKey("DirType") + "," + ap.GetValIntByKey("IsCanBack")
                    + "," + (dots == string.Empty ? "null" : "'" + dots + "'") + ",'" + ap.GetValStrByKey("MyPK") + "')";
                DBAccess.RunSQL(sql);
            }

            //处理节点。
            string[] nds = nodes.Split('~');
            foreach (string nd in nds)
            {
                if (string.IsNullOrEmpty(nd))
                    continue;

                AtPara ap = new AtPara(nd);
                Node mynode = new Node(ap.GetValIntByKey("NodeID"));
                SetNodeProperties(mynode, ap.GetValStrByKey("Name"),
                    ap.GetValIntByKey("X"),
                    ap.GetValIntByKey("Y"),
                    ap.GetValIntByKey("NodeType"));

                mynode.DirectUpdate();
                //   mynode.Save();
            }

            //处理标签。
            string[] mylabs = labes.Split('~');
            foreach (string lab in mylabs)
            {
                if (string.IsNullOrEmpty(lab))
                    continue;

                AtPara ap = new AtPara(lab);
                LabNote ln = new LabNote();
                ln.MyPK = ap.GetValStrByKey("MyPK");
                ln.FK_Flow = fk_flow;
                ln.Name = ap.GetValStrByKey("Label");
                ln.X = ap.GetValIntByKey("X");
                ln.Y = ap.GetValIntByKey("Y");
                ln.Save();
            }
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
        return null;
    }

    private void SetNodeProperties(Node n, string nodeName, int x, int y, int nodeType)
    {
        n.Name = nodeName;
        n.X = x;
        n.Y = y;
        //if (0 == nodeType)
        //{
        //    n.NodePosType = NodePosType.Start;
        //}
        //else if (2 == nodeType)
        //{
        //    n.NodePosType = NodePosType.End;
        //}
        //else if (1 == nodeType)
        //{
        //    n.NodePosType = NodePosType.Mid;
        //    n.HisNodeWorkType = NodeWorkType.Work;
        //}
        //else if (3 == nodeType)
        //{
        //    n.NodePosType = NodePosType.Mid;
        //    n.HisNodeWorkType = NodeWorkType.WorkHL;
        //}
        //else if (4 == nodeType)
        //{
        //    n.NodePosType = NodePosType.Mid;
        //    n.HisNodeWorkType = NodeWorkType.WorkFL;
        //}
        //else if (5 == nodeType)
        //{
        //    n.NodePosType = NodePosType.Mid;
        //    n.HisNodeWorkType = NodeWorkType.WorkFHL;
        //}
        //else if (6 == nodeType)
        //{
        //    n.NodePosType = NodePosType.Mid;
        //    n.HisNodeWorkType = NodeWorkType.SubThreadWork;
        //}
    }
    [WebMethod]
    public string Uploadfile(byte[] FileByte, string fileName)
    {
        try
        {
            //文件存放路径
            string filepath = BP.SystemConfig.PathOfTemp  + "\\" + fileName;
            //如果文件已经存在则删除
            if (File.Exists(filepath))
                File.Delete(filepath);
            //创建文件流实例，用于写入文件
            FileStream stream = new FileStream(filepath, FileMode.CreateNew);
            //写入文件
            stream.Write(FileByte, 0, FileByte.Length);
            stream.Close();
            return filepath;
        }
        catch (Exception exception)
        {
            return "Error: Occured on upload the file. Error Message is :\n" + exception.Message;
        }
        
    }
    [WebMethod]
    public string UploadfileCCForm(byte[] FileByte, string fileName, string fk_frmSort)
    {
        try
        {
            //文件存放路径
            string filepath = BP.SystemConfig.PathOfTemp + "\\" + fileName;
            //如果文件已经存在则删除
            if (File.Exists(filepath))
                File.Delete(filepath);

            //创建文件流实例，用于写入文件
            FileStream stream = new FileStream(filepath, FileMode.CreateNew);

            //写入文件
            stream.Write(FileByte, 0, FileByte.Length);
            stream.Close();

            DataSet ds = new DataSet();
            ds.ReadXml(filepath);

            MapData md = MapData.ImpMapData(ds);
            md.FK_FrmSort = fk_frmSort;
            md.Update();
            return null;
        }
        catch (Exception exception)
        {
            return "Error: Occured on upload the file. Error Message is :\n" + exception.Message;
        }

    }

}
