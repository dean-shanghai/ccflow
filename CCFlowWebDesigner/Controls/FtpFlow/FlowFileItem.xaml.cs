using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;

using BP;
using WF.WSFtp;
using WF.WS;
using BP;
using BP.CY;

namespace BP.Controls
{
    #region 双击定义

    public delegate void MouseLeftDoubleDownEventHandler(object sender, MouseButtonEventArgs e);
    public delegate void MouseLeftOnceDownEventHandler(object sender, MouseButtonEventArgs e);

    /// <summary>
    /// 定义了双击事件的类
    /// </summary>
    public class DoubleClick
    {
        /// <summary>
        /// 双击事件定时器
        /// </summary>
        private DispatcherTimer doubleClickTimer;

        /// <summary>
        /// 是否单击
        /// </summary>
        private bool isOnceClick;

        /// <summary>
        /// 双击事件
        /// </summary>
        public MouseLeftDoubleDownEventHandler mouseLeftDoubleDown;

        /// <summary>
        /// 单击事件
        /// </summary>
        public MouseLeftOnceDownEventHandler mouseLeftOnceDown;

        /// <summary>
        /// 拥有双击事件的UI
        /// </summary>
        private UIElement owner;

        /// <summary>
        /// 实例化DoubleClick
        /// </summary>
        /// <param name="owner">具有双击事件的UI</param>
        public DoubleClick(UIElement owner)
        {
            this.owner = owner;
            this.bindEvent();
        }

        /// <summary>
        /// 绑定事件
        /// </summary>
        private void bindEvent()
        {
            this.owner.MouseLeftButtonDown += (new MouseButtonEventHandler(this.owner_MouseLeftButtonDown));
            DispatcherTimer timer = new DispatcherTimer();
            //设置单击事件
            timer.Interval = (new TimeSpan(0, 0, 0, 0, 200));
            this.doubleClickTimer = timer;
            this.doubleClickTimer.Tick += (new EventHandler(this.doubleClickTimer_Tick));
        }

        private void doubleClickTimer_Tick(object sender, EventArgs e)
        {
            this.isOnceClick = false;
            this.doubleClickTimer.Stop();
        }

        private void owner_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!this.isOnceClick)
            {
                this.isOnceClick = true;
                this.doubleClickTimer.Start();
                this.mouseLeftOnceDown(sender, e);
            }
            else
            {
                this.mouseLeftDoubleDown(sender, e);
            }
        }
    }

    #endregion

    public partial class FlowFileItem : UserControl
    {
        #region 私有变量

        private string path;
        private string flowImgBase64;
        private bool isFolder = false;
        private bool isSelected = false;
        private string flowName = string.Empty;
        private CYFtpSoapClient ftpClient = null;
        private LoadingWindow loadingWindow = new LoadingWindow();

        private DoubleClick MouseDoubleClick;

        #endregion

        #region 事件

        public event MouseEventHandler FlowFileItemClick;
        public event MouseEventHandler FlowFileItemDoubleClick;
        public event EventHandler<FlowTemplete_LoadCompletedEventArgs> FlowTempleteLoadCompeleted;

        #endregion

        public FlowFileItem()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(UserControl_Loaded);

            ftpClient = Glo.GetFtpServiceInstance();

            AttachEvent();
        }

        #region 私有函数

        /// <summary>
        /// 绑定事件 
        /// </summary>
        private void AttachEvent()
        {
            this.MouseEnter += (sender, e) =>
                {
                    if (!this.isFolder)
                    {
                        this.OptContainer.Visibility = Visibility.Visible;
                    }
                };
            this.MouseLeave += (sender, e) =>
            {
                if (this.OptContainer.Visibility == Visibility.Visible)
                {
                    this.OptContainer.Visibility = Visibility.Collapsed;
                }
            };

            this.btnPreview.MouseLeftButtonDown += (sender, e) =>
            {
                FrmFlowPreview flowPrview = new FrmFlowPreview();
                flowPrview.FlowItem = this;

                flowPrview.Show();
            };

            this.btnDownload.MouseLeftButtonDown += DownloadFlow;
            this.btnImport.MouseLeftButtonDown += ImportFlow;
        }

        #region 按钮的处理函数

        /// <summary>
        /// 下载处理函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DownloadFlow(object sender, MouseEventArgs e)
        {
            ftpClient.DownloadFileCompleted += (sender1, e1) =>
            {
                if (!e1.Result.StartsWith("error"))
                {
                    BP.Glo.WinOpen("/WF/Admin/XAP/DoPort.aspx?DoType=DownFlowFile&path=" + System.Text.RegularExpressions.Regex.Escape(e1.Result) + "&Lang=CH", "Help", 50, 50);
                }
            };
            ftpClient.DownloadFileAsync(this.path);
        }

        /// <summary>
        /// 导入工作流
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImportFlow(object sender, MouseEventArgs e)
        {
            FrmSelectFlowType selectType = new FrmSelectFlowType();
            selectType.Closed += Type_Selected;
            selectType.Show();
        }

        /// <summary>
        /// 类型选择完成 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Type_Selected(object sender, EventArgs e)
        {
            FrmSelectFlowType frmSelectType = sender as FrmSelectFlowType;

            if (frmSelectType.DialogResult == true)
            {
                ftpClient.ImportFlowCompleted += (sender1, e1) =>
                    {
                        if (!e1.Result.StartsWith("error"))
                        {
                            WSDesignerSoapClient wsDesignerClient = Glo.GetDesignerServiceInstance();
                            wsDesignerClient.FlowTemplete_LoadCompleted += (sender2, e2) =>
                            {
                                loadingWindow.Close();

                                if (null != FlowTempleteLoadCompeleted)
                                {
                                    FlowTempleteLoadCompeleted(sender2, e2);
                                }
                            };
                            wsDesignerClient.FlowTemplete_LoadAsync(frmSelectType.FlowType, e1.Result, true);
                        }
                        else
                        {
                            loadingWindow.Close();
                            MessageBox.Show(e1.Result);
                        }
                    };

                loadingWindow.Show();
                ftpClient.ImportFlowAsync(this.path);
            }
        }

        #endregion

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            MouseDoubleClick = new DoubleClick(this);
            this.MouseDoubleClick.mouseLeftDoubleDown += new MouseLeftDoubleDownEventHandler(mouseLeftDoubleDown);
            this.MouseDoubleClick.mouseLeftOnceDown += new MouseLeftOnceDownEventHandler(mouseLeftOnceDown);
        }

        private void mouseLeftOnceDown(object sender, MouseEventArgs e)
        {
            if (null != FlowFileItemClick)
            {
                FlowFileItemClick(this, e);
            }
        }

        private void mouseLeftDoubleDown(object sender, MouseEventArgs e)
        {
            if (null != FlowFileItemDoubleClick)
            {
                FlowFileItemDoubleClick(this, e);
            }
        }

        #endregion

        #region 属性

        /// <summary>
        /// 当前设计器的实例
        /// </summary>
        public MainPage CurrentDesinger { get; set; }

        /// <summary>
        /// 是否是文件夹
        /// </summary>
        public bool IsFolder
        {
            get { return isFolder; }
            set
            {
                isFolder = value;

                if (value)
                {
                    flowImg.Source = new BitmapImage(new Uri("/Images/Folder_Icon.png", UriKind.Relative));
                }
            }
        }

        /// <summary>
        /// 是否被选中
        /// </summary>
        public bool IsSelected
        {
            get
            {
                return isSelected;
            }
            set
            {
                isSelected = value;

                if (value)
                {
                    this.background.Background = new SolidColorBrush(Color.FromArgb(255, 198, 222, 252));
                }
                else
                {
                    this.background.Background = null;
                }
            }
        }

        /// <summary>
        ///工作流名称
        /// </summary>
        public string FlowName
        {
            get
            {
                return flowName;
            }
            set
            {
                flowName = value;
                txbName.Text = flowName.Length > 15 ? flowName.Substring(0, 15) + "..." : flowName;

                this.SetValue(ToolTipService.ToolTipProperty, value);
            }
        }

        /// <summary>
        ///文件路径 
        /// </summary>
        public string Path
        {
            get
            {
                return path;
            }
            set
            {
                path = value;
            }
        }

        /// <summary>
        /// 工作流缩略图
        /// </summary>
        public string FlowImg
        {
            get
            {
                return flowImgBase64;
            }
            set
            {
                flowImgBase64 = value;

                if (this.isFolder)
                {
                    flowImg.Source = new BitmapImage(new Uri("/Images/Folder_Icon.png", UriKind.Relative));
                }
                else if (!string.IsNullOrEmpty(value))
                {
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.SetSource(StringHandler.ToStream(value));
                    flowImg.Source = bitmap;
                }
                else
                {
                    flowImg.Source = new BitmapImage(new Uri("/Images/ReleaseToFTP.png", UriKind.Relative));
                }
            }
        }

        #endregion
    }
}
