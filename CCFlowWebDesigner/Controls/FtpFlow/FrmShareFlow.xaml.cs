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

using BP;

namespace BP.Controls
{
    public partial class FrmShareFlow : ChildWindow
    {
        private string flowID = string.Empty;
        private string currentDir;
        private MainPage currentDesinger;

        public FrmShareFlow()
        {
            InitializeComponent();

            AttachEvent();
            this.treeFtpFolder.Load();
        }

        /// <summary>
        /// 事件绑定
        /// </summary>
        private void AttachEvent()
        {
            //地址栏项单击
            this.dirNavBar.DirNavBarItemClick += (sender, e) =>
            {
                DirNavBarItem item = sender as DirNavBarItem;

                this.currentDir = item.DirPath;
                this.fileView.FolderPath = item.DirPath;
            };

            //获取默认流程模板根目录
            this.treeFtpFolder.GetDefaultFlowRootCompleted += (sender, e) =>
            {
                this.dirNavBar.DefaultRoot = e.Root;
            };

            //左侧文件夹树项切换
            this.treeFtpFolder.FolderChanged += (sender, e) =>
            {
                this.fileView.FolderPath = e.FolderPath;
                CurrentDir = e.FolderPath;
            };

            //右侧文件浏览中文件夹双击切换
            this.fileView.DirChanged += (dir) =>
            {
                CurrentDir = dir;
            };
        }

        /// <summary>
        /// 当前设计器的实例
        /// </summary>
        public MainPage CurrentDesinger
        {
            get
            {
                return currentDesinger;
            }
            set
            {
                currentDesinger = value;

                this.fileView.CurrentDesinger = value;

            }
        }

        public string FlowID
        {
            set
            {
                this.flowID = value;
            }
        }

        /// <summary>
        /// 当前选中目录
        /// </summary>
        public string CurrentDir
        {
            get
            {
                return this.currentDir;
            }
            set
            {
                this.currentDir = value;

                this.dirNavBar.Dir = value;
            }
        }

        /// <summary>
        /// 重新加载文件显示
        /// </summary>
        public void ReloadFile()
        {
            fileView.ReLoad();
        }
    }
}

