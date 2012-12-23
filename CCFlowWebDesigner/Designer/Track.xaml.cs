using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Linq;
using System.IO;
using BP;
using BP.WF;
using FluxJpeg.Core;
using WF.WS;
using Silverlight;
using System.Windows.Browser;

namespace BP
{
    /// <summary>
    /// 主要用来显示流程轨迹
    /// </summary>
    public partial class Track : UserControl, IContainer
    {
        #region Constructs
        public Track()
        {
            InitializeComponent();
            Application.Current.Host.Content.Resized += new EventHandler(Content_Resized);
            SetGridLines(); 
            dicReturnTrackTips = new Dictionary<Direction, IList<string>>();
            _doubleClickTimer = new System.Windows.Threading.DispatcherTimer();
            _doubleClickTimer.Interval = new TimeSpan(0, 0, 0, 0, SystemConst.DoubleClickTime);
            _doubleClickTimer.Tick += new EventHandler(DoubleClick_Timer);
        }
       
        /// <summary>
        /// 轨迹图
        /// </summary>
        /// <param name="fk_flow"></param>
        /// <param name="workid"></param>
        public Track(string fk_flow, string workid)
            : this()
        {
            this.FK_Flow = fk_flow;
            this.WorkID = workid;
            if (string.IsNullOrEmpty(workid) == false)
            {
                //如果传递过来workid 说明要显示轨图.
                WSDesignerSoapClient ws = BP.Glo.GetDesignerServiceInstance();
                ws.GetDTOfWorkListAsync(this.FK_Flow,  workid);
                ws.GetDTOfWorkListCompleted += new EventHandler<GetDTOfWorkListCompletedEventArgs>(ws_GetDTOfWorkListCompleted);
            }
            else
            {
                this.GenerFlowChart(FK_Flow);
            }
        }
        /// <summary>
        /// 获取轨迹数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ws_GetDTOfWorkListCompleted(object sender, GetDTOfWorkListCompletedEventArgs e)
        {
            // 给track dataset 赋值.
            trackDataSet = new DataSet();
            trackDataSet.FromXml(e.Result);

            //产生轨迹图.
            this.GenerFlowChart(FK_Flow);
        }
        #endregion

        #region Properties
        public bool IsMouseSelecting
        {
            get { return (temproaryEllipse != null); }
        }
        public WSDesignerSoapClient _Service
        {
            get { return _service; }
            set { _service = value; }
        }
        public string FK_Flow { get; set; }
        public string WorkID { get; set; }
        public bool IsNeedSave { get; set; }

        /// <summary>
        /// 当前是否为添加回退连线状态
        /// </summary>
        public bool IsReturnTypeDir { get; set; }

        /// <summary>
        /// 当前是否有节点在编辑状态
        /// </summary>
        public bool IsSomeChildEditing { get; set; }
        public PageEditType EditType
        {
            get
            {
                if (editType == PageEditType.None)
                {
                    editType = PageEditType.Add;
                }
                return editType;
            }
            set { editType = value; }
        }
        public int NodeID;
        public List<FlowNode> flowNodeCollections;
        public List<FlowNode> FlowNodeCollections
        {
            get
            {
                if (flowNodeCollections == null)
                {
                    flowNodeCollections = new List<FlowNode>();
                }
                return flowNodeCollections;
            }
        }
        /// <summary>
        /// 方向
        /// </summary>
        public List<Direction> directionCollections;
        /// <summary>
        /// 方向
        /// </summary>
        public List<Direction> DirectionCollections
        {
            get
            {
                if (directionCollections == null)
                {
                    directionCollections = new List<Direction>();
                }
                return directionCollections;
            }
        }
        /// <summary>
        /// 标签
        /// </summary>
        public List<NodeLabel> lableCollections;
        /// <summary>
        /// 标签
        /// </summary>
        public List<NodeLabel> LableCollections
        {
            get
            {
                if (lableCollections == null)
                {
                    lableCollections = new List<NodeLabel>();
                }
                return lableCollections;
            }
        }
        private int nextMaxIndex = 0;
        public int NextMaxIndex
        {
            get
            {
                nextMaxIndex++;
                return nextMaxIndex;
            }
        }
        public double Left
        {
            get { return 230; }
        }
        public double Top
        {
            get { return 40; }
        }

        /// <summary>
        /// 流程编号
        /// </summary>
        public string FlowID { get; set; }
        public Double ContainerWidth
        {
            get { return paint.Width; }
            set { paint.Width = value; }
        }

        public Double ContainerHeight
        {
            get { return paint.Height; }
            set { paint.Height = value; }
        }
        public int NextNewFlowNodeIndex
        {
            get { return 0; }
        }
        public int NextNewDirectionIndex
        {
            get { return 0; }
        }

        public int NextNewLabelIndex
        {
            get { return 0; }
        }
        public Double ScrollViewerHorizontalOffset
        {
            get { return svContainer.HorizontalOffset; }
            set { svContainer.ScrollToHorizontalOffset(value); }
        }
        public Double ScrollViewerVerticalOffset
        {
            get { return svContainer.VerticalOffset; }
            set { svContainer.ScrollToVerticalOffset(value); }
        }
        public Canvas GridLinesContainer
        {
            get
            {
                if (_gridLinesContainer == null)
                {
                    Canvas temCan = new Canvas();
                    temCan.Name = "canGridLinesContainer";
                    paint.Children.Add(temCan);
                    _gridLinesContainer = temCan;
                }
                return _gridLinesContainer;
            }
        }
        private List<Control> copyElementCollectionInMemory;
        public List<Control> CopyElementCollectionInMemory
        {
            get
            {
                if (copyElementCollectionInMemory == null)
                    copyElementCollectionInMemory = new List<System.Windows.Controls.Control>();
                return copyElementCollectionInMemory;
            }
            set { copyElementCollectionInMemory = value; }
        }
        private bool mouseIsInContainer = false;
        public bool MouseIsInContainer
        {
            get { return mouseIsInContainer; }
            set { mouseIsInContainer = value; }
        }
        public Direction CurrentTemporaryDirection { get; set; }
        private List<System.Windows.Controls.Control> _currentSelectedControlCollection;
        public List<System.Windows.Controls.Control> CurrentSelectedControlCollection
        {
            get
            {
                if (_currentSelectedControlCollection == null)
                    _currentSelectedControlCollection = new List<System.Windows.Controls.Control>();
                return _currentSelectedControlCollection;
            }
        }
        public bool CtrlKeyIsPress
        {
            get
            {
                return (Keyboard.Modifiers == ModifierKeys.Control);
            }
        }
        public bool IsContainerRefresh { get; set; }
        #endregion

        #region Variables
        private WSDesignerSoapClient _service =  Glo.GetDesignerServiceInstance();
        private PageEditType editType = PageEditType.None;
        private Point mousePosition;
        private bool trackingMouseMove = false;
        private System.Windows.Threading.DispatcherTimer _doubleClickTimer;
        private Canvas _gridLinesContainer;
        private Rectangle temproaryEllipse;
        private DataSet trackDataSet;
        private double DesignerHeight = 50;
        private double DesignerWdith = 50;
        private Dictionary<Direction, IList<string>> dicReturnTrackTips;
        #endregion
        
        #region 加载流程相关
        private void GenerFlowChart(string flowID)
        {
            this.FlowID = flowID;
            string sqls = "";
            sqls += "SELECT NodeID,Name,X,Y,NodePosType,RunModel,HisToNDs FROM WF_Node WHERE FK_Flow='" + flowID + "'";
            sqls += "@SELECT MyPK,Name,X,Y FROM WF_LabNote WHERE FK_Flow='" + flowID + "'";
            sqls += "@SELECT Node,ToNode,DirType,IsCanBack,Dots FROM WF_Direction WHERE FK_Flow='" + flowID + "'";
            WSDesignerSoapClient ws = BP.Glo.GetDesignerServiceInstance();
            ws.RunSQLReturnTableSAsync(sqls);
            ws.RunSQLReturnTableSCompleted += new EventHandler<RunSQLReturnTableSCompletedEventArgs>(ws_RunSQLReturnTableSCompleted);
        }
        void ws_RunSQLReturnTableSCompleted(object sender, RunSQLReturnTableSCompletedEventArgs e)
        {
            var ds = new DataSet();
            ds.FromXml(e.Result);

            #region 画流程节点。
            DataTable dtNode = ds.Tables[0];
            foreach (DataRow dr in dtNode.Rows)
            {
                NodePosType postype = (NodePosType)int.Parse(dr["NodePosType"].ToString());
                FlowNodeType runModel = (FlowNodeType)int.Parse(dr["RunModel"].ToString());

                FlowNode flowNode = new FlowNode((IContainer)this, runModel);
                flowNode.HisPosType = postype;
                flowNode.SetValue(Canvas.ZIndexProperty, NextMaxIndex);
                flowNode.FK_Flow = FlowID;
                flowNode.NodeID = dr["NodeID"].ToString();
                flowNode.NodeName = dr["Name"].ToString();
                double x = double.Parse(dr["X"]);
                double y = double.Parse(dr["Y"]);
                if (x < 50)
                    x = 50;
                if (x > 1190)
                    x = 1190;
                if (y < 30)
                    y = 30;

                if (y > 770)
                    y = 770;

                flowNode.CenterPoint = new Point(x, y);

                // 永远使设计器的宽和高为节点的最大值　
                if (y > DesignerHeight)
                    DesignerHeight = y;

                if (x > DesignerWdith)
                    DesignerWdith = x;

                AddFlowNode(flowNode);
            }
            #endregion 画流程节点。

            #region 生成标签.
            DataTable dtLabel = ds.Tables[1];
            foreach (DataRow dr in dtLabel.Rows)
            {
                var nodeLabel = new NodeLabel((IContainer)this);
                nodeLabel.LabelName = dr["Name"].ToString();
                nodeLabel.Position = new Point(double.Parse(dr["X"].ToString()), double.Parse(dr["Y"].ToString()));
                nodeLabel.LableID = dr["MyPK"].ToString();
                this.AddLabel(nodeLabel);
            }
            #endregion 生成标签.

            #region 生成方向.
            DataTable dtDir = ds.Tables[2];
            foreach (FlowNode bfn in FlowNodeCollections)
            {
                foreach (DataRow dr in dtDir.Rows)
                {
                    if (bfn.NodeID == dr["Node"].ToString())
                    {
                        foreach (FlowNode efn in FlowNodeCollections)
                        {
                            if (efn.NodeID == dr["ToNode"].ToString())
                            {
                                var d = new Direction((IContainer)this);
                                d.IsReturnType = (int.Parse(dr["DirType"]) == 0) ? false : true;
                                d.IsCanBack = (int.Parse(dr["IsCanBack"]) == 0) ? false : true;

                                if (dr["Dots"] != null)
                                {
                                    string[] strs = dr["Dots"].ToString().Split('@');
                                    IList<double> dots = new List<double>();

                                    foreach (string str in strs)
                                    {
                                        if (str == null || str == "")
                                            continue;
                                        string[] mystr = str.Split(',');
                                        if (mystr.Length == 2)
                                        {
                                            dots.Add(double.Parse(mystr[0]));
                                            dots.Add(double.Parse(mystr[1]));
                                        }
                                    }

                                    d.LineType = DirectionLineType.Polyline;
                                    d.DirectionTurnPoint1.CenterPosition = new Point(dots[0], dots[1]);
                                    d.DirectionTurnPoint2.CenterPosition = new Point(dots[2], dots[3]);
                                }

                                d.FlowID = FlowID;
                                d.BeginFlowNode = bfn; //开始节点.
                                d.EndFlowNode = efn; //结束节点.

                                //增加方向.
                                this.AddDirection(d);
                            }
                        }
                    }
                }
            }
            #endregion 生成方向.

            #region 标记颜色, 显示轨迹。
            if (trackDataSet != null )
            {
                /*说明已经取道了轨迹数据.
                 * 1,流程在运行过程中WF_Track 忠实的记录了每个操作动作.这个表的数据只增加不会减少更不修改它.
                 * 2,每个事件都一个事件类型ActionType, 它是一个枚举类型的.
                 */
                this.dicReturnTrackTips.Clear();
                DataTable dt = trackDataSet.Tables["WF_Track"];

                foreach (DataRow dr in dt.Rows)
                {
                    string begin = dr["NDFrom"].ToString();
                    string to = dr["NDTo"].ToString();
                    string rdt = dr["RDT"].ToString();
                    string msg = dr["Msg"].ToString();

                    // 事件类型.
                    ActionType at = (ActionType)int.Parse(dr["ActionType"].ToString());
                    switch (at)
                    {
                        case ActionType.Forward: /*普通节点发送*/
                        case ActionType.ForwardFL: /*分流点发送*/
                        case ActionType.ForwardHL: /*合流点发送*/
                        case ActionType.SubFlowForward: /*子线程点发送*/
                            #region 画红色的轨迹线表示已经走过的节点.
                            foreach (Direction dir in DirectionCollections)
                            {
                                if (dir.BeginFlowNode.NodeID == begin && dir.EndFlowNode.NodeID == to)
                                {
                                    dir.IsTrackingLine = true;
                                    break;
                                }
                            }
                            #endregion
                            break;
                        case ActionType.Return: /*退回*/
#warning 如何标示退回请与马工商量.
                            foreach (Direction dir in DirectionCollections)
                            {
                                if (dir.BeginFlowNode.NodeID == begin && dir.EndFlowNode.NodeID == to && dir.IsReturnType)
                                {
                                    if (this.dicReturnTrackTips.ContainsKey(dir))
                                    {
                                        dicReturnTrackTips[dir].Add(dr["Msg"].ToString());
                                        continue;
                                    }

                                    dir.IsTrackingLine = true;
                                    dicReturnTrackTips.Add(dir, new List<string>());
                                    dicReturnTrackTips[dir].Add(dr["Msg"].ToString());
                                }
                            }
                            break;
                        case ActionType.HungUp: /*挂起*/
#warning 如何标示退回请与马工商量.
                            break;
                        default:
                            break;
                    }
                  
                }
            }
            #endregion 标记颜色.

            SaveChange(HistoryType.New);
            Content_Resized(null, null);
        }
        /// <summary>
        /// 增加方向
        /// </summary>
        /// <param name="r"></param>
        public void AddDirection(Direction dir)
        {
            if (paint.Children.Contains(dir) == false)
            {
                paint.Children.Add(dir);
                dir.Container = this;
            }

            if (DirectionCollections.Contains(dir) == false)
                DirectionCollections.Add(dir);

          
        }
        /// <summary>
        /// 增加标签
        /// </summary>
        /// <param name="l"></param>
        public void AddLabel(NodeLabel l)
        {
            if (paint.Children.Contains(l) == false)
                paint.Children.Add(l);

            if (LableCollections.Contains(l) == false)
                LableCollections.Add(l);
        }
        /// <summary>
        /// 增加节点
        /// </summary>
        /// <param name="a"></param>
        public void AddFlowNode(FlowNode a)
        {
            if (paint.Children.Contains(a) == false)
            {
                paint.Children.Add(a);
                a.Container = this;
                a.FK_Flow = FlowID;
            }

            if (FlowNodeCollections.Contains(a) == false)
                FlowNodeCollections.Add(a);
        }
        #endregion

        #region 菜单相关
        public void ShowFlowNodeContentMenu(FlowNode a, object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }
        public void ShowLabelContentMenu(NodeLabel l, object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }
        public void ShowDirectionContentMenu(Direction r, object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }
        /// <summary>
        /// 双击节点后要做的事件
        /// </summary>
        /// <param name="a">节点</param>
        public void ShowFlowNodeSetting(FlowNode a)
        {
            MessageBox.Show("在施工中.");
            ///todo 周朋说这里要弹出一个窗口。不过url还没定
        }
        #endregion

        /// <summary>
        /// Content改变时重设设计器的大小， 设计器的大小取Content及最大节点值中的最大值 
        /// 20为假设的滚动条宽度。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Content_Resized(object sender, EventArgs e)
        {
            var contentHeight = Application.Current.Host.Content.ActualHeight - 20;
            var contentWidth = Application.Current.Host.Content.ActualWidth - 20;
            paint.Width = DesignerWdith > contentWidth
                                             ? DesignerWdith
                                             : contentWidth;
            paint.Height = DesignerHeight > contentHeight
                                              ? DesignerHeight
                                              : contentHeight;
            SetGridLines();
        }
        public void SetGridLines()
        {
            GridLinesContainer.Children.Clear();
            SolidColorBrush brush = new SolidColorBrush();
            brush.Color = Color.FromArgb(255, 160, 160, 160);
            //  brush.Color = Color.FromArgb(255, 255, 255, 255);
            double thickness = 0.3;
            double top = 0;
            double left = 0;

            double width = paint.Width;
            double height = paint.Height;

            double stepLength = 40;

            double x, y;
            x = left + stepLength;
            y = top;

            while (x < width + left)
            {
                Line line = new Line();
                line.X1 = x;
                line.Y1 = y;
                line.X2 = x;
                line.Y2 = y + height;

                line.Stroke = brush;
                line.StrokeThickness = thickness;
                line.Stretch = Stretch.Fill;
                GridLinesContainer.Children.Add(line);
                x += stepLength;
            }

            x = left;
            y = top + stepLength;
            while (y < height + top)
            {
                Line line = new Line();
                line.X1 = x;
                line.Y1 = y;
                line.X2 = x + width;
                line.Y2 = y;

                line.Stroke = brush;
                line.Stretch = Stretch.Fill;
                line.StrokeThickness = thickness;
                GridLinesContainer.Children.Add(line);
                y += stepLength;
            }
        }

        public bool Contains(UIElement uie)
        {
            return paint.Children.Contains(uie);
        }
        public CheckResult CheckSave()
        {
            CheckResult cr = new CheckResult();
            cr.IsPass = true;
            return cr;
        }
        public void ShowMessage(string message)
        {
        }
        public void SaveChange(HistoryType action)
        {
        }
        private void DoubleClick_Timer(object sender, EventArgs e)
        {
            _doubleClickTimer.Stop();
        }

        #region 鼠标操作相关
        public void AddSelectedControl(System.Windows.Controls.Control uc)
        {
            if (!CurrentSelectedControlCollection.Contains(uc))
                CurrentSelectedControlCollection.Add(uc);
        }

        public void RemoveSelectedControl(System.Windows.Controls.Control uc)
        {
            if (CurrentSelectedControlCollection.Contains(uc))
                CurrentSelectedControlCollection.Remove(uc);
        }

        public void SetWorkFlowElementSelected(System.Windows.Controls.Control uc, bool isSelected)
        {
            if (isSelected)
                AddSelectedControl(uc);
            else
                RemoveSelectedControl(uc);
            if (!CtrlKeyIsPress)
                ClearSelectFlowElement(uc);
        }

        public void ClearSelectFlowElement(System.Windows.Controls.Control uc)
        {
            if (CurrentSelectedControlCollection == null || CurrentSelectedControlCollection.Count == 0)
                return;

            int count = CurrentSelectedControlCollection.Count;
            for (int i = 0; i < count; i++)
            {
                ((IElement)CurrentSelectedControlCollection[i]).IsSelectd = false;
            }
            CurrentSelectedControlCollection.Clear();
            if (uc != null)
            {
                ((IElement)uc).IsSelectd = true;
                AddSelectedControl(uc);
            }
            mouseIsInContainer = true;
        }

        public void MoveControlCollectionByDisplacement(double x, double y, UserControl uc)
        {
            if (CurrentSelectedControlCollection == null || CurrentSelectedControlCollection.Count == 0)
                return;

            FlowNode selectedFlowNode = null;
            Direction selectedDirection = null;
            NodeLabel selectedLabel = null;
            if (uc is FlowNode)
                selectedFlowNode = uc as FlowNode;

            if (uc is Direction)
                selectedDirection = uc as Direction;
            if (uc is NodeLabel)
                selectedLabel = uc as NodeLabel;

            FlowNode a = null;
            Direction r = null;
            NodeLabel l = null;
            for (int i = 0; i < CurrentSelectedControlCollection.Count; i++)
            {
                if (CurrentSelectedControlCollection[i] is FlowNode)
                {
                    a = CurrentSelectedControlCollection[i] as FlowNode;
                    if (a == selectedFlowNode)
                        continue;
                    a.SetPositionByDisplacement(x, y);
                }
                if (CurrentSelectedControlCollection[i] is NodeLabel)
                {
                    l = CurrentSelectedControlCollection[i] as NodeLabel;
                    if (l == selectedLabel)
                        continue;
                    l.SetPositionByDisplacement(x, y);
                }
                if (CurrentSelectedControlCollection[i] is Direction)
                {
                    r = CurrentSelectedControlCollection[i] as Direction;
                    if (r == selectedDirection)
                        continue;
                    r.SetPositionByDisplacement(x, y);
                }
            }
        }

        private void Container_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_doubleClickTimer.IsEnabled)
            {
                _doubleClickTimer.Stop();
                if (!string.IsNullOrEmpty(FlowID))
                {
                    //FlowNode a = new FlowNode((IContainer)this, FlowNodeType.INTERACTION);
                    //a.NodeName = Text.NewFlowNode + NextNewFlowNodeIndex.ToString();
                    //Point p = e.GetPosition(this);
                    //a.CenterPoint = new Point(p.X - this.Left, p.Y - this.Top);
                    //a.IsSelectd = true;
                    //this.AddFlowNode(a);
                    //_Service.DoNewNodeAsync(FlowID, (int)a.CenterPoint.X, (int)a.CenterPoint.Y);
                }
            }
            else
            {
                _doubleClickTimer.Start();

                ClearSelectFlowElement(null);

                FrameworkElement element = sender as FrameworkElement;
                mousePosition = e.GetPosition(element);
                trackingMouseMove = true;
            }
        }

        private void Container_MouseMove(object sender, MouseEventArgs e)
        {
            if (trackingMouseMove)
            {
                FrameworkElement element = sender as FrameworkElement;
                Point beginPoint = mousePosition;
                Point endPoint = e.GetPosition(element);

                if (temproaryEllipse == null)
                {
                    temproaryEllipse = new Rectangle();


                    SolidColorBrush brush = new SolidColorBrush();
                    brush.Color = Color.FromArgb(255, 234, 213, 2);
                    temproaryEllipse.Fill = brush;
                    temproaryEllipse.Opacity = 0.2;

                    brush = new SolidColorBrush();
                    brush.Color = Color.FromArgb(255, 0, 0, 0);
                    temproaryEllipse.Stroke = brush;
                    temproaryEllipse.StrokeMiterLimit = 2.0;

                    paint.Children.Add(temproaryEllipse);
                }

                if (endPoint.X >= beginPoint.X)
                {
                    if (endPoint.Y >= beginPoint.Y)
                    {
                        temproaryEllipse.SetValue(Canvas.TopProperty, beginPoint.Y);
                        temproaryEllipse.SetValue(Canvas.LeftProperty, beginPoint.X);
                    }
                    else
                    {
                        temproaryEllipse.SetValue(Canvas.TopProperty, endPoint.Y);
                        temproaryEllipse.SetValue(Canvas.LeftProperty, beginPoint.X);
                    }
                }
                else
                {
                    if (endPoint.Y >= beginPoint.Y)
                    {
                        temproaryEllipse.SetValue(Canvas.TopProperty, beginPoint.Y);
                        temproaryEllipse.SetValue(Canvas.LeftProperty, endPoint.X);
                    }
                    else
                    {
                        temproaryEllipse.SetValue(Canvas.TopProperty, endPoint.Y);
                        temproaryEllipse.SetValue(Canvas.LeftProperty, endPoint.X);
                    }
                }


                temproaryEllipse.Width = Math.Abs(endPoint.X - beginPoint.X);
                temproaryEllipse.Height = Math.Abs(endPoint.Y - beginPoint.Y);
            }
            else
            {
                if (CurrentTemporaryDirection != null)
                {
                    CurrentTemporaryDirection.CaptureMouse();
                    Point currentPoint = e.GetPosition(CurrentTemporaryDirection);
                    CurrentTemporaryDirection.EndPointPosition = currentPoint;

                    if (CurrentTemporaryDirection.BeginFlowNode != null)
                    {
                        CurrentTemporaryDirection.BeginPointPosition =
                            CurrentTemporaryDirection.GetResetPoint(currentPoint,
                                                                    CurrentTemporaryDirection.BeginFlowNode.CenterPoint,
                                                                    CurrentTemporaryDirection.BeginFlowNode,
                                                                    DirectionMoveType.Begin);
                    }
                }
            }
        }

        private void Container_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            trackingMouseMove = false;
            if (CurrentTemporaryDirection != null)
            {
                CurrentTemporaryDirection.SimulateDirectionPointMouseLeftButtonUpEvent(DirectionMoveType.End,
                                                                                       CurrentTemporaryDirection, e);
                if (CurrentTemporaryDirection.EndFlowNode == null)
                {
                    CurrentTemporaryDirection.BeginFlowNode.CanShowMenu = false;

                    CurrentTemporaryDirection.CanShowMenu = false;
                    // this.RemoveDirection(CurrentTemporaryDirection);
                    CurrentTemporaryDirection.Delete();
                }
                else
                {
                    CurrentTemporaryDirection.CanShowMenu = false;
                    CurrentTemporaryDirection.BeginFlowNode.CanShowMenu = false;
                    CurrentTemporaryDirection.EndFlowNode.CanShowMenu = true;
                    CurrentTemporaryDirection.IsTemporaryDirection = false;
                    CurrentTemporaryDirection.IsSelectd = false;
                    RemoveSelectedControl(CurrentTemporaryDirection);
                    SaveChange(HistoryType.New);
                }
                CurrentTemporaryDirection.ReleaseMouseCapture();
                CurrentTemporaryDirection = null;
            }

            FrameworkElement element = sender as FrameworkElement;
            mousePosition = e.GetPosition(element);
            if (temproaryEllipse != null)
            {
                double width = temproaryEllipse.Width;
                double height = temproaryEllipse.Height;

                if (width > 10 && height > 10)
                {
                    Point p = new Point();
                    p.X = (double)temproaryEllipse.GetValue(Canvas.LeftProperty);
                    p.Y = (double)temproaryEllipse.GetValue(Canvas.TopProperty);

                    FlowNode a = null;
                    Direction r = null;
                    NodeLabel l = null;
                    foreach (UIElement uie in paint.Children)
                    {
                        if (uie is FlowNode)
                        {
                            a = uie as FlowNode;
                            if (p.X < a.CenterPoint.X && a.CenterPoint.X < p.X + width
                                && p.Y < a.CenterPoint.Y && a.CenterPoint.Y < p.Y + height)
                            {
                                AddSelectedControl(a);
                                a.IsSelectd = true;
                            }
                        }
                        if (uie is NodeLabel)
                        {
                            l = uie as NodeLabel;
                            if (p.X < l.Position.X && l.Position.X < p.X + width
                                && p.Y < l.Position.Y && l.Position.Y < p.Y + height)
                            {
                                AddSelectedControl(a);
                                l.IsSelectd = true;
                            }
                        }
                        if (uie is Direction)
                        {
                            r = uie as Direction;

                            Point ruleBeginPointPosition = r.BeginPointPosition;
                            Point ruleEndPointPosition = r.EndPointPosition;

                            if (p.X < ruleBeginPointPosition.X && ruleBeginPointPosition.X < p.X + width
                                && p.Y < ruleBeginPointPosition.Y && ruleBeginPointPosition.Y < p.Y + height
                                &&
                                p.X < ruleEndPointPosition.X && ruleEndPointPosition.X < p.X + width
                                && p.Y < ruleEndPointPosition.Y && ruleEndPointPosition.Y < p.Y + height
                                )
                            {
                                AddSelectedControl(r);
                                r.IsSelectd = true;
                            }
                        }
                    }
                }
                paint.Children.Remove(temproaryEllipse);
                temproaryEllipse = null;
            }
        }
        private void Container_MouseEnter(object sender, MouseEventArgs e)
        {
            mouseIsInContainer = true;
        }
        private void Container_MouseLeave(object sender, MouseEventArgs e)
        {
            mouseIsInContainer = false;
        }
        private void paint_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (mouseIsInContainer)
            {
                e.GetPosition(this);
            }
            e.Handled = true;
        }
        
        #endregion
       
        #region IContainer 成员，但是不需要实现

        public void SetProper(string lang, string dotype, string fk_flow, string node1, string node2, string title)
        {
            throw new NotImplementedException();
        }
        public void CopySelectedControlToMemory(System.Windows.Controls.Control currentControl)
        {
            throw new NotImplementedException();
        }

        public void UpdateSelectedControlToMemory(System.Windows.Controls.Control currentControl)
        {
            throw new NotImplementedException();
        }

        public void UpDateSelectedNode(System.Windows.Controls.Control currentControl)
        {
            throw new NotImplementedException();
        }

        public void DeleteSeletedControl()
        {
            throw new NotImplementedException();
        }

        public void RemoveFlowNode(FlowNode a)
        {
            throw new NotImplementedException();
        }

        public void RemoveLabel(NodeLabel l)
        {
            throw new NotImplementedException();
        }

        public void ShowDirectionSetting(Direction r)
        {
            if (this.dicReturnTrackTips.ContainsKey(r))
            {
                string tip = "未知原因";
                tip = string.Join("\n-------------------------\n", this.dicReturnTrackTips[r]);
                MessageBox.Show(tip, "退回原因", MessageBoxButton.OK);
            }
        }

        public void PreviousAction()
        {
            throw new NotImplementedException();
        }

        public void NextAction()
        {
            throw new NotImplementedException();
        }
        public void RemoveDirection(Direction r)
        {
            throw new NotImplementedException();
        }

        public void LoadFromXmlString(string xml)
        {
            throw new NotImplementedException();
        }

        public string ToXmlString()
        {
            throw new NotImplementedException();
        }

        public void PastMemoryToContainer()
        {
            throw new NotImplementedException();
        }

        public void AddLabel(int x, int y)
        {
            throw new NotImplementedException();
        }
        #endregion


        /// <summary>
        /// 屏蔽SL默认的右键菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }
    }
}
