using System;
using System.Collections;
using System.IO;
using System.Data;
using System.Windows.Forms;
using BP.WF;
using BP.Win.WF;

namespace BP.WF
{
	/// <summary>
	/// Global 的摘要说明。
	/// </summary>
    public class Global
    {

        #region 发布设置
        public static string Host
        {
            get
            {
                string s = BP.SystemConfig.AppSettings["BPMHost"];
                if (s == null)
                    s = "127.0.0.1/Flow/";
                return s;
            }
        }
        static Global()
        {
            SystemConfig.IsBSsystem = false;
            System.DateTime dt = File.GetLastWriteTime(Application.ExecutablePath);
            VersionDate = dt.ToString("yyyy-MM-dd HH:mm");
        }
        #endregion 发布设置

        #region Ver
        public static readonly string VersionDate = "2004-03-16 15:26"; //  
        public static readonly string UpdateItems = "";//"\r\n此更新不包括目录“Image”，如果登陆失败，请手动更新目录“Image”中的文件！";//"\n最近更新项目：BP.Tax.WF ->CUI， ->YUE"; //
        private static string _updatePath = "";
        public static string UpdatePath
        {
            get
            {
                return _updatePath;
            }
        }
        public static readonly string AppConfigPath = Application.StartupPath + "\\App.config";
        private static string _RefConfigPath = "";
        public static string RefConfigPath
        {
            get
            {
                return _RefConfigPath;
            }
        }
        /// <summary>
        /// 应用程序基础路径
        /// </summary>
        public static string PathOfBase
        {
            get
            {
                return Application.StartupPath + @"\.\..\..\";
            }
        }
        public static string PathOfVisualFlow
        {
            get
            {
                return Application.StartupPath + @"\.\..\..\..\VisualFlow\";
            }
        }
        public static string PathOfVisualFlowDesigner
        {
            get
            {
                return Application.StartupPath + @"\.\..\..\..\VisualFlowDesigner\";
            }
        }
        #endregion Ver

        #region 登陆信息
        public static string User = "8888";
        public static string Right = "管理员";
        public static string RightID = "00";
        public static FrmMain MainForm;
        public static BP.Port.Emp LoadEmp
        {
            get
            {
                return BP.Win.WF.Global.LoadEmp;
            }
            set
            {
                BP.Win.WF.Global.LoadEmp = value;
            }
        }
        #endregion 登陆信息

        #region 登陆
       
        private static int times = 1;
        /// <summary>
        /// 装载配置通过文件
        /// </summary>
        /// <returns></returns>
        public static void LoadConfigByFile()
        {
            BP.WF.Glo.IntallPath = BP.WF.Global.PathOfVisualFlow;

            string path = BP.WF.Global.PathOfVisualFlow + "\\web.config"; //如果有这个文件就装载它。
            if (System.IO.File.Exists(path) == false)
                throw new Exception("配置文件没有找到:" + path);


            BP.DA.ClassFactory.LoadConfig(path);
            try
            {
                BP.Port.Emp em = new BP.Port.Emp("admin");
            }
            catch
            {
                BP.Port.Emp em = new BP.Port.Emp("admin");
            }

            BP.SystemConfig.IsBSsystem_Test = false;
            BP.SystemConfig.IsBSsystem = false;
            SystemConfig.IsBSsystem = false;
            BP.Win.WF.Global.FlowImagePath = BP.WF.Global.PathOfVisualFlow + "\\Data\\FlowDesc\\";

            BP.Web.WebUser.SysLang = BP.WF.Glo.Language;
            BP.SystemConfig.IsBSsystem_Test = false;
            BP.SystemConfig.IsBSsystem = false;
            SystemConfig.IsBSsystem = false;
        }
        #endregion

        #region 应用开发
        public static bool IsExitProcess(string name)
        {
            System.Diagnostics.Process[] processes = System.Diagnostics.Process.GetProcesses();
            foreach (System.Diagnostics.Process pro in processes)
            {
                if (pro.ProcessName+".exe" == name)
                    return true;
            }
            return false;
        }
        public static bool KillProcess(string name)
        {
            System.Diagnostics.Process[] processes = System.Diagnostics.Process.GetProcesses();
            foreach (System.Diagnostics.Process pro in processes)
            {
                if (pro.ProcessName == name)
                {
                    pro.Kill();
                    return true;
                }
            }
            return false;
        }

        static bool Application_Start()
        {
            if (BP.WF.Global.IsExitProcess("VisualFlowAutoUpdate.exe"))
            {
                MessageBox.Show("驰骋工作流程设计器应用程序已经启动，您不能同时启动两个操作窗口。", "操作提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();
            }


            BP.Win32.FrmLanguage lan = new BP.Win32.FrmLanguage();
            lan.ShowDialog();

            try
            {
                LoadConfigByFile();
            }
            catch (Exception ex)
            {
                MessageBox.Show("装载配置文件时出现错误，请访问我们获取技术支持 http://ccflow.cn \n错误信息：" + ex.Message, "系统错错", MessageBoxButtons.OK,
                     MessageBoxIcon.Error);

                Application.Exit();
            }

            MainForm = new MainForm();
            Win.WF.Global.ToolCursors = new Cursor[7];//=[MainForm.wfToolBar1.ButtonsCount];
            Win.WF.Global.ToolCursors[0] = Cursors.Default;
            Win.WF.Global.ToolCursors[1] = Cursors.Cross;
            Win.WF.Global.ToolCursors[2] = Cursors.Cross;
            Win.WF.Global.ToolCursors[3] = Cursors.Cross;
            Win.WF.Global.ToolCursors[4] = Cursors.Cross;
            Win.WF.Global.ToolCursors[5] = Cursors.Cross;
            Win.WF.Global.ToolCursors[6] = Cursors.Cross;
            Win.WF.Global.WFTools = MainForm.wfToolBar1;
            return true;
        }
        public static void Update()
        {
            MainForm.Dispose();
            System.Diagnostics.Process.Start(Application.StartupPath + "\\OnlineUpdate.exe");
            Application.Exit();
        }

        [STAThread]
        static void Main()
        {

            if (!Application_Start())
                return;

            Application.Run(MainForm);
            if (MainForm != null)
                MainForm.Dispose();

            Application.Exit();
        }
        #endregion


        #region 流程常用的方法
        /// <summary>
        /// 流程常用的方法
        /// </summary>
        /// <param name="fk_flowSort"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static Flow DoLoadFlowTemplate(string fk_flowSort, string path)
        {
            FileInfo info = new FileInfo(path);
            DataSet ds = new DataSet();
            ds.ReadXml(path);

            DataTable dtFlow = ds.Tables["WF_Flow"];
            Flow fl = new Flow();
            string oldFlowNo = dtFlow.Rows[0]["No"].ToString();
            int oldFlowID = int.Parse(oldFlowNo);
            try
            {
                fl.DoNewFlow();

                int flowID = int.Parse(fl.No);

                #region 处理流程表数据
                foreach (DataColumn dc in dtFlow.Columns)
                {
                    string val = dtFlow.Rows[0][dc.ColumnName] as string;
                    switch (dc.ColumnName)
                    {
                        case "No":
                        case "FK_FlowSort":
                            continue;
                        case "Name":
                            val = "Copy of " + val;
                            break;
                        default:
                            break;
                    }
                    fl.SetValByKey(dc.ColumnName, val);
                }
                fl.FK_FlowSort = fk_flowSort;
                fl.Update();
                #endregion 处理流程表数据

                string msg = "";


                foreach (DataTable dt in ds.Tables)
                {
                    switch (dt.TableName)
                    {
                        case "WF_Flow": //模版文件。
                            continue;
                        case "BookTemplates": //模版文件。
                            foreach (DataRow dr in dt.Rows)
                            {
                                BookTemplate bt = new BookTemplate();
                                //   bt.NodeID = flowID;
                                foreach (DataColumn dc in dt.Columns)
                                {
                                    string val = dr[dc.ColumnName] as string;
                                    switch (dc.ColumnName)
                                    {
                                        case "NodeID":
                                            if (val.Length == 3)
                                                val = flowID + val.Substring(1);
                                            else
                                                val = flowID + val.Substring(2);
                                            break;
                                        default:
                                            break;
                                    }
                                    bt.SetValByKey(dc.ColumnName, val);
                                }
                                int i = 0;
                                string no = bt.No;
                                while (bt.IsExits == false)
                                {
                                    bt.No = no + i.ToString();
                                }
                                bt.Insert();
                            }

                            break;
                        case "WF_Cond": //Conds.xml。
                            foreach (DataRow dr in dt.Rows)
                            {
                                Cond cd = new Cond();
                                cd.FK_Flow = fl.No;
                                foreach (DataColumn dc in dt.Columns)
                                {
                                    string val = dr[dc.ColumnName] as string;
                                    switch (dc.ColumnName)
                                    {
                                        case "ToNodeID":
                                        case "FK_Node":
                                        case "NodeID":
                                            if (val.Length == 3)
                                                val = flowID + val.Substring(1);
                                            else if (val.Length == 4)
                                                val = flowID + val.Substring(2);
                                            break;
                                        default:
                                            break;
                                    }
                                    cd.SetValByKey(dc.ColumnName, val);
                                }
                                cd.MyPK = DA.DBAccess.GenerOID().ToString();
                                cd.Insert();
                            }

                            break;
                        case "Directions": //FAppSets.xml。
                            foreach (DataRow dr in dt.Rows)
                            {
                                Direction dir = new Direction();
                                foreach (DataColumn dc in dt.Columns)
                                {
                                    string val = dr[dc.ColumnName] as string;
                                    switch (dc.ColumnName)
                                    {
                                        case "Node":
                                        case "ToNode":
                                            if (val.Length == 3)
                                                val = flowID + val.Substring(1);
                                            else if (val.Length == 4)
                                                val = flowID + val.Substring(2);
                                            break;
                                        default:
                                            break;
                                    }
                                    dir.SetValByKey(dc.ColumnName, val);
                                }
                                dir.Insert();
                            }

                            break;
                        case "WF_FAppSet": //FAppSets.xml。
                            foreach (DataRow dr in dt.Rows)
                            {
                                FAppSet fs = new FAppSet();
                                fs.FK_Flow = fl.No;
                                foreach (DataColumn dc in dt.Columns)
                                {
                                    string val = dr[dc.ColumnName] as string;
                                    switch (dc.ColumnName)
                                    {
                                        case "FK_Node":
                                            if (val.Length == 3)
                                                val = flowID + val.Substring(1);
                                            else if (val.Length == 4)
                                                val = flowID + val.Substring(2);
                                            break;
                                        default:
                                            break;
                                    }
                                    fs.SetValByKey(dc.ColumnName, val);
                                }
                                fs.OID = DA.DBAccess.GenerOID();
                                fs.Insert();
                            }

                            break;
                        case "FlowStations": //FlowStations.xml。
                            foreach (DataRow dr in dt.Rows)
                            {
                                FlowStation fs = new FlowStation();
                                fs.FK_Flow = fl.No;
                                foreach (DataColumn dc in dt.Columns)
                                {
                                    string val = dr[dc.ColumnName] as string;
                                    switch (dc.ColumnName)
                                    {
                                        case FlowStationAttr.FK_Flow:
                                            continue;
                                        default:
                                            break;
                                    }
                                    fs.SetValByKey(dc.ColumnName, val);
                                }
                                fs.Insert();
                            }

                            break;
                        case "WF_LabNote": //LabNotes.xml。
                            foreach (DataRow dr in dt.Rows)
                            {
                                LabNote ln = new LabNote();
                                ln.FK_Flow = fl.No;
                                foreach (DataColumn dc in dt.Columns)
                                {
                                    string val = dr[dc.ColumnName] as string;
                                    switch (dc.ColumnName)
                                    {
                                        case LabNoteAttr.FK_Flow:
                                            continue;
                                        default:
                                            break;
                                    }
                                    ln.SetValByKey(dc.ColumnName, val);
                                }
                                ln.MyPK = ln.FK_Flow + "_" + ln.X + "_" + ln.Y;
                                ln.DirectInsert();
                                //ln.OID = DA.DBAccess.GenerOID();
                                //ln.InsertAsOID(ln.OID);
                            }
                            break;

                        case "NodeDept": //FAppSets.xml。

                            foreach (DataRow dr in dt.Rows)
                            {
                                NodeDept dir = new NodeDept();
                                foreach (DataColumn dc in dt.Columns)
                                {
                                    string val = dr[dc.ColumnName] as string;
                                    switch (dc.ColumnName)
                                    {
                                        case "FK_Node":
                                            if (val.Length == 3)
                                                val = flowID + val.Substring(1);
                                            else if (val.Length == 4)
                                                val = flowID + val.Substring(2);
                                            break;
                                        default:
                                            break;
                                    }
                                    dir.SetValByKey(dc.ColumnName, val);
                                }
                                dir.Insert();
                            }

                            break;
                        case "WF_Node": //LabNotes.xml。
                            foreach (DataRow dr in dt.Rows)
                            {
                                Node nd = new Node();
                                nd.FK_Flow = fl.No;
                                foreach (DataColumn dc in dt.Columns)
                                {
                                    string val = dr[dc.ColumnName] as string;
                                    switch (dc.ColumnName)
                                    {
                                        case NodeAttr.NodeID:
                                            if (val.Length == 3)
                                                val = flowID + val.Substring(1);
                                            else if (val.Length == 4)
                                                val = flowID + val.Substring(2);
                                            break;
                                        case NodeAttr.FK_Flow:
                                        case "FK_FlowSort":
                                            continue;
                                        case NodeAttr.ShowSheets:
                                        case NodeAttr.HisToNDs:
                                        case NodeAttr.GroupStaNDs:
                                            string key = "@" + flowID;
                                            val = val.Replace(key, "");
                                            break;
                                        default:
                                            break;
                                    }
                                    nd.SetValByKey(dc.ColumnName, val);
                                }
                                try
                                {
                                    nd.DirectInsert();
                                }
                                catch
                                {
                                    nd.DirectUpdate();
                                }
                            }

                            break;
                        case "NodeStations": //FAppSets.xml。
                            foreach (DataRow dr in dt.Rows)
                            {
                                NodeStation ns = new NodeStation();
                                foreach (DataColumn dc in dt.Columns)
                                {
                                    string val = dr[dc.ColumnName] as string;
                                    switch (dc.ColumnName)
                                    {
                                        case "FK_Node":
                                            if (val.Length == 3)
                                                val = flowID + val.Substring(1);
                                            else if (val.Length == 4)
                                                val = flowID + val.Substring(2);
                                            break;
                                        default:
                                            break;
                                    }
                                    ns.SetValByKey(dc.ColumnName, val);
                                }
                                ns.Insert();
                            }

                            break;
                        case "RptAttrs": //LabNotes.xml。

                            foreach (DataRow dr in dt.Rows)
                            {
                                RptAttr attr = new RptAttr();
                                attr.FK_Node = fl.No;
                                foreach (DataColumn dc in dt.Columns)
                                {
                                    string val = dr[dc.ColumnName] as string;
                                    switch (dc.ColumnName)
                                    {
                                        case RptAttrAttr.FK_Node:
                                            if (val.Length == 3)
                                                val = flowID + val.Substring(1);
                                            else if (val.Length == 4)
                                                val = flowID + val.Substring(2);
                                            break;
                                        default:
                                            break;
                                    }
                                    attr.SetValByKey(dc.ColumnName, val);
                                }
                                attr.MyPK = attr.FK_Node + "_" + attr.FK_Rpt + "_" + attr.RefAttrOID + "_" + attr.RefField;
                                attr.Save();
                            }

                            break;
                        case "RptStations": //RptEmps.xml。
                        case "RptEmps": //RptEmps.xml。
                            break;
                        case "Sys_Enum": //RptEmps.xml。

                            foreach (DataRow dr in dt.Rows)
                            {
                                Sys.SysEnum se = new Sys.SysEnum();
                                foreach (DataColumn dc in dt.Columns)
                                {
                                    string val = dr[dc.ColumnName] as string;
                                    switch (dc.ColumnName)
                                    {
                                        case RptAttrAttr.FK_Node:
                                            break;
                                        default:
                                            break;
                                    }
                                    se.SetValByKey(dc.ColumnName, val);
                                }
                                if (se.IsExits)
                                    continue;
                                se.Insert();
                            }

                            break;
                        case "Sys_EnumMain": //RptEmps.xml。
                            foreach (DataRow dr in dt.Rows)
                            {
                                Sys.SysEnumMain sem = new Sys.SysEnumMain();
                                foreach (DataColumn dc in dt.Columns)
                                {
                                    string val = dr[dc.ColumnName] as string;
                                    //switch (dc.ColumnName)
                                    //{
                                    //    case RptAttrAttr.FK_Node:
                                    //        break;
                                    //    default:
                                    //        break;
                                    //}
                                    sem.SetValByKey(dc.ColumnName, val);
                                }
                                if (sem.IsExits)
                                    continue;
                                sem.Insert();
                            }

                            break;
                        case "Sys_MapAttr": //RptEmps.xml。
                            foreach (DataRow dr in dt.Rows)
                            {
                                Sys.MapAttr ma = new Sys.MapAttr();
                                foreach (DataColumn dc in dt.Columns)
                                {
                                    string val = dr[dc.ColumnName] as string;
                                    switch (dc.ColumnName)
                                    {
                                        case Sys.MapAttrAttr.FK_MapData:
                                        case Sys.MapAttrAttr.KeyOfEn:
                                            val = val.Replace("ND" + oldFlowID, "ND" + flowID);
                                            break;
                                        default:
                                            break;
                                    }
                                    ma.SetValByKey(dc.ColumnName, val);
                                }
                                bool b = ma.IsExit(Sys.MapAttrAttr.FK_MapData, ma.FK_MapData, Sys.MapAttrAttr.KeyOfEn, ma.KeyOfEn);
                                if (b == true)
                                    ma.Update();
                                else
                                    ma.Insert();
                            }
                            break;
                        case "Sys_MapData": //RptEmps.xml。
                            foreach (DataRow dr in dt.Rows)
                            {
                                Sys.MapData md = new Sys.MapData();
                                foreach (DataColumn dc in dt.Columns)
                                {
                                    string val = dr[dc.ColumnName] as string;
                                    switch (dc.ColumnName)
                                    {
                                        case Sys.MapDataAttr.No:
                                            val = val.Replace("ND" + oldFlowNo, "ND" + fl.No);
                                            break;
                                        default:
                                            break;
                                    }
                                    md.SetValByKey(dc.ColumnName, val);
                                }
                                md.Save();
                            }
                            break;
                        case "Sys_MapDtl": //RptEmps.xml。
                            foreach (DataRow dr in dt.Rows)
                            {
                                Sys.MapDtl md = new Sys.MapDtl();
                                foreach (DataColumn dc in dt.Columns)
                                {
                                    string val = dr[dc.ColumnName] as string;
                                    switch (dc.ColumnName)
                                    {
                                        case Sys.MapDtlAttr.No:
                                        case Sys.MapDtlAttr.FK_MapData:
                                        case Sys.MapDtlAttr.PTable:
                                            val = val.Replace("ND" + oldFlowID, "ND" + flowID);
                                            break;
                                        default:
                                            break;
                                    }
                                    md.SetValByKey(dc.ColumnName, val);
                                }
                                md.Save();
                            }
                            break;
                        case "WF_NodeEmps": //FAppSets.xml。
                            foreach (DataRow dr in dt.Rows)
                            {
                                NodeEmp ne = new NodeEmp();
                                foreach (DataColumn dc in dt.Columns)
                                {
                                    string val = dr[dc.ColumnName] as string;
                                    switch (dc.ColumnName)
                                    {
                                        case "FK_Node":
                                            if (val.Length == 3)
                                                val = flowID + val.Substring(1);
                                            else if (val.Length == 4)
                                                val = flowID + val.Substring(2);
                                            break;
                                        default:
                                            break;
                                    }
                                    ne.SetValByKey(dc.ColumnName, val);
                                }
                                ne.Insert();
                            }
                            break;
                        case "WFRpts": //RptEmps.xml。
                            foreach (DataRow dr in dt.Rows)
                            {
                                WFRpt rpt = new WFRpt();
                                foreach (DataColumn dc in dt.Columns)
                                {
                                    string val = dr[dc.ColumnName] as string;
                                    switch (dc.ColumnName)
                                    {
                                        case WFRptAttr.FK_FlowSort:
                                            val = fl.FK_FlowSort;
                                            break;
                                        case WFRptAttr.FK_Flow:
                                            val = fl.No;
                                            break;
                                        default:
                                            break;
                                    }
                                    rpt.SetValByKey(dc.ColumnName, val);
                                }
                                try
                                {
                                    rpt.Save();
                                }
                                catch
                                {
                                }
                            }
                            break;
                        case "Sys_GroupField": // 
                            foreach (DataRow dr in dt.Rows)
                            {
                                Sys.GroupField gf = new Sys.GroupField();
                                foreach (DataColumn dc in dt.Columns)
                                {
                                    string val = dr[dc.ColumnName] as string;
                                    gf.SetValByKey(dc.ColumnName, val);
                                }
                                if (gf.IsExits)
                                    gf.Update();
                                else
                                    gf.InsertAsOID(gf.OID);
                            }
                            break;
                        default:
                            msg += "Error:" + dt.TableName;
                            break;
                        //    throw new Exception("@unhandle named " + dt.TableName);
                    }
                }


                #region 处理数据完整性。
                BP.DA.DBAccess.RunSQL("DELETE WF_Cond WHERE ToNodeID NOT IN (SELECT NodeID FROM WF_Node)");
                BP.DA.DBAccess.RunSQL("DELETE WF_Cond WHERE FK_Node NOT IN (SELECT NodeID FROM WF_Node)");
                BP.DA.DBAccess.RunSQL("DELETE WF_Cond WHERE NodeID NOT IN (SELECT NodeID FROM WF_Node)");
                #endregion

                MessageBox.Show("The flow templete succeedfuly install your system, you must be redesign it for fit your work."+msg,
                    "ok", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return fl;
            }
            catch (Exception ex)
            {
                fl.DoDelete();
                throw ex;
            }
        }
        /// <summary>
        /// gener flow Template.
        /// </summary>
        /// <param name="fl"></param>
        public static void DoGenerFlowTemplate(Flow fl)
        {
            string path = "C:\\FlowTemplate\\" + fl.Name + "\\";
            if (System.IO.Directory.Exists(path) == false)
                System.IO.Directory.CreateDirectory(path);

            DataSet ds = new DataSet();

            // 把流程信息生成一个文件。
            Flows fls = new Flows();
            fls.AddEntity(fl);
            // fls.SaveToXml(path + "Flow.xml");
            ds.Tables.Add(fls.ToDataTableField("Flow"));

            // 节点信息
            Nodes nds = fl.HisNodes;
            //   nds.SaveToXml(path + "Nodes.xml");
            ds.Tables.Add(nds.ToDataTableField("Nodes"));


            // 文书信息
            BookTemplates tmps = new BookTemplates(fl.No);
            //tmps.SaveToXml(path + "BookTemplates.xml");
            ds.Tables.Add(tmps.ToDataTableField("BookTemplates"));

            foreach (BookTemplate tmp in tmps)
            {
                File.Copy(@"D:\WorkFlow\Front\Data\CyclostyleFile\" + tmp.No + ".rtf", path + "\\" + tmp.No + ".rtf", true);
            }

            // 条件信息
            Conds cds = new Conds(fl.No);
            // cds.SaveToXml(path + "Conds.xml");
            ds.Tables.Add(cds.ToDataTableField("Conds"));


            // 方向
            string sqlin = "SELECT NodeID from wf_node where fk_flow='" + fl.No + "'";
            string sql = "select * from WF_Direction where Node IN (" + sqlin + ") OR ToNode In (" + sqlin + ")";

            DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
            dt.TableName = "Directions";
            //   ds1.WriteXml(path + "Directions.xml");
            ds.Tables.Add(dt);



            // 应用设置 FAppSet
            FAppSets sets = new FAppSets(fl.No);
            //sets.SaveToXml(path + "FAppSets.xml");
            ds.Tables.Add(sets.ToDataTableField("FAppSets"));




            // 流程发送完后抄送到岗位 
            FlowStations fstas = new FlowStations(fl.No);
            // fstas.SaveToXml(path + "FlowStations.xml");
            ds.Tables.Add(fstas.ToDataTableField("FlowStations"));


            // 流程标签.
            LabNotes labs = new LabNotes(fl.No);
            //  labs.SaveToXml(path + "LabNotes.xml");
            ds.Tables.Add(labs.ToDataTableField("LabNotes"));



            // 节点与部门。
            sql = "select * from WF_NodeDept where FK_Node IN (" + sqlin + ")";
            dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
            dt.TableName = "NodeDept";
            ds.Tables.Add(dt);


            // 节点与岗位权限。
            sql = "select * from WF_NodeStation where FK_Node IN (" + sqlin + ")";
            dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
            dt.TableName = "NodeStations";
            ds.Tables.Add(dt);

            // 节点与人员。
            sql = "select * from WF_NodeEmp where FK_Node IN (" + sqlin + ")";
            dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
            dt.TableName = "WF_NodeEmps";
            ds.Tables.Add(dt);

            //ds = BP.DA.DBAccess.RunSQLReturnDataSet(sql);
            //ds.WriteXml(path + "WF_NodeEmps.xml");

            // 流程报表。
            WFRpts rpts = new WFRpts(fl.No);
            // rpts.SaveToXml(path + "WFRpts.xml");
            ds.Tables.Add(rpts.ToDataTableField("WFRpts"));


            // 流程报表属性
            RptAttrs rptAttrs = new RptAttrs();
            rptAttrs.RetrieveAll();
            ds.Tables.Add(rptAttrs.ToDataTableField("RptAttrs"));

            //rptAttrs.SaveToXml(path + "RptAttrs.xml");


            // 流程报表访问权限。
            RptStations rptStations = new RptStations(fl.No);
            rptStations.RetrieveAll();
            //  rptStations.SaveToXml(path + "RptStations.xml");
            ds.Tables.Add(rptStations.ToDataTableField("RptStations"));


            // 流程报表人员访问权限。
            RptEmps rptEmps = new RptEmps(fl.No);
            rptEmps.RetrieveAll();
            // rptEmps.SaveToXml(path + "RptEmps.xml");
            ds.Tables.Add(rptEmps.ToDataTableField("RptEmps"));


            int flowID = int.Parse(fl.No);

            // Sys_MapData
            sql = "SELECT * FROM Sys_MapData WHERE No LIKE 'ND" + flowID + "%'";
            dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
            dt.TableName = "Sys_MapData";
            ds.Tables.Add(dt);

            //ds = BP.DA.DBAccess.RunSQLReturnDataSet(sql);
            //ds.WriteXml(path + "Sys_MapData.xml");


            // Sys_MapAttr
            sql = "SELECT * FROM Sys_MapAttr WHERE  FK_MapData LIKE 'ND" + flowID + "%'";
            dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
            dt.TableName = "Sys_MapAttr";
            ds.Tables.Add(dt);


            // Sys_EnumMain
            sql = "SELECT * FROM Sys_EnumMain WHERE No IN (SELECT KeyOfEn from Sys_MapAttr WHERE FK_MapData LIKE 'ND" + flowID + "%' )";
            dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
            dt.TableName = "Sys_EnumMain";
            ds.Tables.Add(dt);



            // Sys_MapDtl
            sql = "SELECT * FROM Sys_MapDtl WHERE  FK_MapData LIKE 'ND" + flowID + "%'";
            dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
            dt.TableName = "Sys_MapDtl";
            ds.Tables.Add(dt);



            // Sys_Enum
            sql = "SELECT * from Sys_Enum WHERE EnumKey IN ( SELECT No FROM Sys_EnumMain where NO IN (SELECT KeyOfEn from Sys_MapAttr WHERE FK_MapData LIKE 'ND" + flowID + "%' ) )";

            dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
            dt.TableName = "Sys_Enum";
            ds.Tables.Add(dt);


            // Sys_Enum
            sql = "SELECT * FROM Sys_GroupField WHERE EnName LIKE 'ND" + flowID + "%' ";
            dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
            dt.TableName = "Sys_GroupField";
            ds.Tables.Add(dt);

            ds.WriteXml(path + fl.Name + ".xml");

#warning share flow Template.
            string msg = "The flow Template is gener secceed,  would you like to share it ?";
            if (System.Windows.Forms.MessageBox.Show(msg, "Thanks for you those ccflow.",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
            }
            System.Diagnostics.Process.Start(path);
        }
        #endregion
    }

    public enum StartModel
    {
        /// <summary>
        /// 启动流程设计器
        /// </summary>
        WorkFlow,
        /// <summary>
        /// 调度
        /// </summary>
        DTS,
    }
}
