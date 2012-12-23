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
using BP;

namespace BP.Controls
{
    public class GetDefaultRootEventArgs : EventArgs
    {
        public GetDefaultRootEventArgs(string root)
        {
            this.Root = root;
        }

        public string Root { get; set; }
    }

    public class FtpFolderChangedEventArgs : EventArgs
    {
        public FtpFolderChangedEventArgs(string folderPath)
        {
            this.FolderPath = folderPath;
        }

        public string FolderPath { get; set; }
    }

    public partial class FtpFolderView : UserControl
    {
        public event EventHandler<FtpFolderChangedEventArgs> FolderChanged;
        public event EventHandler<GetDefaultRootEventArgs> GetDefaultFlowRootCompleted;//获取默认流程模板根目录

        TreeViewItem currentItem;//当前
        CYFtpSoapClient ftpClient;

        public FtpFolderView()
        {
            InitializeComponent();

            ftpClient = Glo.GetFtpServiceInstance();
        }

        /// <summary>
        /// 开始加载文件夹
        /// </summary>
        public void Load()
        {
            ftpClient.GetDefaultFlowRootCompleted += GetDefaultFlowRoot_Completed;
            ftpClient.GetDefaultFlowRootAsync();
        }

        /// <summary>
        /// 当前选中的目录
        /// </summary>
        public string CurrentDir
        {
            get
            {
                return currentItem != null ? currentItem.Tag.ToString() : "";
            }
        }

        public void GetDefaultFlowRoot_Completed(object sender, GetDefaultFlowRootCompletedEventArgs e)
        {
            ftpClient.GetDefaultFlowRootCompleted -= GetDefaultFlowRoot_Completed;

            if (GetDefaultFlowRootCompleted != null)
            {
                GetDefaultFlowRootCompleted(null, new GetDefaultRootEventArgs(e.Result));
            }

            if (!string.IsNullOrEmpty(e.Result))
            {
                treeFolder.Items.Add(new TreeViewItem() { Header = "模板库", Tag = e.Result });

                LoadFolders();

                FolderChanged(null, new FtpFolderChangedEventArgs(e.Result));

                currentItem = treeFolder.Items[0] as TreeViewItem;
            }
        }

        /// <summary>
        /// 加载文件夹
        /// </summary>
        public void LoadFolders()
        {
            ftpClient.GetFoldersCompleted += GetFoldersCompleted;
            ftpClient.GetFoldersAsync("");
        }

        /// <summary>
        /// 文件夹加载完成
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="e"></param>
        public void GetFoldersCompleted(Object sender, GetFoldersCompletedEventArgs e)
        {
            ftpClient.GetFoldersCompleted -= GetFoldersCompleted;

            List<FsItem> lstFolder = e.Result.ToList();
            TreeViewItem root = treeFolder.Items[0] as TreeViewItem;

            if (lstFolder != null)
            {
                foreach (FsItem item in lstFolder)
                {
                    root.Items.Add(new TreeViewItem() { Header = item.Name, Tag = item.Path });
                }
            }
        }

        /// <summary>
        /// 获取子文件夹列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeFolder_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            currentItem = treeFolder.SelectedItem as TreeViewItem;

            if (currentItem.Items.Count == 0 && !string.IsNullOrEmpty(currentItem.Tag.ToString()))
            {
                ftpClient.GetFoldersCompleted += childFolder_GetCompleted;
                ftpClient.GetFoldersAsync(currentItem.Tag.ToString());
            }

            FolderChanged(sender, new FtpFolderChangedEventArgs(currentItem.Tag.ToString()));
        }

        private void childFolder_GetCompleted(Object sender, GetFoldersCompletedEventArgs e)
        {
            ftpClient.GetFoldersCompleted -= childFolder_GetCompleted;

            if (currentItem != null)
            {
                currentItem.Items.Clear();
                currentItem.IsExpanded = true;

                List<FsItem> lstFolder = e.Result.ToList();

                if (lstFolder != null)
                {
                    foreach (FsItem item in lstFolder)
                    {
                        currentItem.Items.Add(new TreeViewItem() { Header = item.Name, Tag = item.Path });
                    }
                }
            }
        }
    }
}
