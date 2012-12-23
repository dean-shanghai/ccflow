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
    public partial class FrmTemNote : ChildWindow
    {
        private string description;

        public FrmTemNote()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 模板描述
        /// </summary>
        public string TemDescription
        {
            get
            {
                return description;
            }
        }
        
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            description = txbNote.Text.Trim();

            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}

