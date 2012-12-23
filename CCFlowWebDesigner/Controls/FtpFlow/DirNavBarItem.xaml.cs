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

namespace BP.Controls
{
    public partial class DirNavBarItem : UserControl
    {
        private string dirName = string.Empty;
        private string dirPath = string.Empty;

        public DirNavBarItem()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 目录名称
        /// </summary>
        public string DirName
        {
            get
            {
                return dirName;
            }
            set
            {
                dirName = value;

                txtName.Text = value.Trim();
            }
        }

        /// <summary>
        /// 目录路径
        /// </summary>
        public string DirPath
        {
            get
            {
                return dirPath;
            }
            set
            {
                dirPath = value;
            }
        }
    }
}
