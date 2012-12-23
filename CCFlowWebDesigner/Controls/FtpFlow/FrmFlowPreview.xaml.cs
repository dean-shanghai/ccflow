using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using BP.CY;

namespace BP.Controls
{
    public partial class FrmFlowPreview : ChildWindow
    {
        public FrmFlowPreview()
        {
            InitializeComponent();
        }

        public FlowFileItem FlowItem
        {
            set
            {
                if (value != null && !string.IsNullOrEmpty(value.FlowImg))
                {
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.SetSource(StringHandler.ToStream(value.FlowImg));
                    flowBitmap.Source = bitmap;
                }
                else
                {
                    flowBitmap.Source = new BitmapImage(new Uri("/Images/ReleaseToFTP.png", UriKind.Relative));
                }
            }
        }
    }
}

