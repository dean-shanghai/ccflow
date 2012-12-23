using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using WF.WSFtp;
using WF.WS;
using BP;
using BP;

namespace BP.Controls
{
    public delegate void DirChangedHandler(string dir);

    public partial class FtpFileView : UserControl
    {
        private string ftpFolderPath;
        private FlowFileItem selectedItem;
        private LoadingWindow loadingWindow = new LoadingWindow();

        CYFtpSoapClient ftpClient;
        List<FlowFileItem> items = new List<FlowFileItem>();

        public event DirChangedHandler DirChanged;
        public event EventHandler<FlowTemplete_LoadCompletedEventArgs> FlowTempleteLoadCompeleted;

        public FtpFileView()
        {
            InitializeComponent();

            ftpClient = Glo.GetFtpServiceInstance();
        }

        /// <summary>
        /// 当前设计器的实例
        /// </summary>
        public MainPage CurrentDesinger { get; set; }

        /// <summary>
        /// 重新加载
        /// </summary>
        public void ReLoad()
        {
            LoadFile();
        }

        /// <summary>
        /// 当前选中的项
        /// </summary>
        public FlowFileItem SelectedItem
        {
            get
            {
                return selectedItem;
            }
        }

        /// <summary>
        /// 文件夹路径
        /// </summary>
        public string FolderPath
        {
            get
            {
                return ftpFolderPath;
            }
            set
            {
                ftpFolderPath = value;

                selectedItem = null;

                LoadFile();
            }
        }

        /// <summary>
        /// 加载相应文件夹中的文件
        /// </summary>
        private void LoadFile()
        {
            loadingWindow.Show();

            items.Clear();
            this.fileContainer.Children.Clear();
            if (!string.IsNullOrEmpty(ftpFolderPath))
            {
                ftpClient.GetFlowFilesCompleted += GetFlowFiles_Completed;
                ftpClient.GetFlowFilesAsync(ftpFolderPath);
            }
        }

        /// <summary>
        /// 文件列表读取完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetFlowFiles_Completed(object sender, GetFlowFilesCompletedEventArgs e)
        {
            ftpClient.GetFlowFilesCompleted -= GetFlowFiles_Completed;
            FlowFileItem flowFileItem = null;
            List<FsFileItem> lst = e.Result.ToList();

            if (lst != null)
            {
                foreach (FsFileItem item in lst)
                {
                    if (item.IsFolder)
                    {
                        flowFileItem = new FlowFileItem()
                        {
                            FlowName = item.Name,
                            Path = item.Path,
                            IsFolder = true
                        };

                        flowFileItem.FlowFileItemDoubleClick += FlowFileItem_MouseDoubleClick;
                    }
                    else
                    {
                        flowFileItem = new FlowFileItem()
                        {
                            FlowName = item.Name,
                            Path = item.Path,
                            IsFolder = false,
                            FlowImg = item.Bitmap,
                            CurrentDesinger = this.CurrentDesinger
                        };

                        flowFileItem.FlowTempleteLoadCompeleted += FlowTempleteLoad_Compeleted;
                    }

                    flowFileItem.FlowFileItemClick += FlowFileItem_MouseDown;
                    items.Add(flowFileItem);
                    fileContainer.Children.Add(flowFileItem);
                }
            }

            loadingWindow.Close();
        }

        /// <summary>
        /// item鼠标按下事件处理程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FlowFileItem_MouseDown(object sender, MouseEventArgs e)
        {
            selectedItem = sender as FlowFileItem;
            selectedItem.IsSelected = true;

            foreach (FlowFileItem item in items)
            {
                if (item != selectedItem)
                {
                    item.IsSelected = false;
                }
            }
        }

        /// <summary>
        /// 文件夹双击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FlowFileItem_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            selectedItem = sender as FlowFileItem;
            selectedItem.IsSelected = true;

            if (selectedItem.IsFolder && DirChanged != null)
            {
                DirChanged(selectedItem.Path);
                FolderPath = selectedItem.Path;
            }
        }

        private void FlowTempleteLoad_Compeleted(object sender, FlowTemplete_LoadCompletedEventArgs e)
        {
            if (null != FlowTempleteLoadCompeleted)
            {
                FlowTempleteLoadCompeleted(sender, e);
            }
        }
    }
}
