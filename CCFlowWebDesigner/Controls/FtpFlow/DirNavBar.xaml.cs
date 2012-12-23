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
using System.Windows.Media.Imaging;

namespace BP.Controls
{
    public partial class DirNavBar : UserControl
    {
        private string defaultRoot = string.Empty;
        private string dir = string.Empty;
        public event MouseEventHandler DirNavBarItemClick;

        public DirNavBar()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 默认根目录
        /// </summary>
        public string DefaultRoot
        {
            get
            {
                return defaultRoot;
            }
            set
            {
                defaultRoot = value;
            }
        }

        /// <summary>
        /// 当前目录
        /// </summary>
        public string Dir
        {
            get
            {
                return dir;
            }
            set
            {
                dir = value;

                ParseDir();
            }
        }

        /// <summary>
        /// 解析当前目录路径
        /// </summary>
        private void ParseDir()
        {
            dirContainer.Children.Clear();

            if (!string.IsNullOrEmpty(this.dir))
            {
                string[] arrRoot = defaultRoot.Split(new string[] { "/", "//" }, StringSplitOptions.RemoveEmptyEntries);
                string[] arrDir = this.dir.Split(new string[] { "/", "//" }, StringSplitOptions.RemoveEmptyEntries);
                string host = arrDir[1];
                string path = defaultRoot;
                DirNavBarItem item = null;

                item = new DirNavBarItem()
                {
                    DirName = "模板库",
                    DirPath = defaultRoot
                };
                item.MouseLeftButtonDown += dirNavBarItem_Click;
                dirContainer.Children.Add(item);

                for (int i = arrRoot.Length; i < arrDir.Length; i++)
                {
                    dirContainer.Children.Add(new Image()
                    {
                        Width = 10,
                        Height = 10,
                        Source = new BitmapImage(new Uri("../../images/arrow.png", UriKind.Relative))
                    });

                    path += "/" + arrDir[i];

                    item = new DirNavBarItem()
                    {
                        DirName = arrDir[i],
                        DirPath = path
                    };
                    item.MouseLeftButtonDown += dirNavBarItem_Click;
                    dirContainer.Children.Add(item);
                }
            }
        }

        /// <summary>
        /// 目录项单击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dirNavBarItem_Click(object sender, MouseEventArgs e)
        {
            DirNavBarItem item = sender as DirNavBarItem;

            if (!string.IsNullOrEmpty(item.DirPath))
            {
                this.Dir = item.DirPath;

                if (DirNavBarItemClick != null)
                {
                    DirNavBarItemClick(sender, e);
                }
            }
        }
    }
}
