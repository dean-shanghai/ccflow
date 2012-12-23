using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace BP
{ 
    public class SystemConst
    {
        public class ColorConst
        {
            public static Brush WarningColor
            {
                get
                {
                    var brush = new SolidColorBrush {Color = Color.FromArgb(255, 255, 0, 0)};
                    return brush;
                }
            }
            public static Brush SelectedColor
            {
                get
                {
                    var brush = new SolidColorBrush {Color = Color.FromArgb(255, 255, 181, 0)};
                    return brush;
                }
            }
            
        }
        public static double DirectionThickness
        {
            get
            {
                return 2.0;
            }
        }

        /// <summary>
        /// 双击的时间间隔
        /// </summary>
        public static  int DoubleClickTime = 400;
    }
}
