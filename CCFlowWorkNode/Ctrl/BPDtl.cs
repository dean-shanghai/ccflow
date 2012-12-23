using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;
using System.Windows.Browser;
using System.Text;
using Microsoft.Expression.Interactivity;
using Microsoft.Expression.Interactivity.Layout;
using System.Windows.Media.Imaging;
using Silverlight;

namespace WorkNode
{
    public class BPDtl : System.Windows.Controls.HyperlinkButton
    {
        #region 处理选中.
        private bool _IsSelected = false;
        public bool IsSelected
        {
            get
            {
                return _IsSelected;
            }
            set
            {
                _IsSelected = value;
                if (value == true)
                {
                    Thickness d = new Thickness(0.5);
                    this.BorderThickness = d;
                    this.BorderBrush = new SolidColorBrush(Colors.Blue);
                }
                else
                {
                    Thickness d1 = new Thickness(0.5);
                    this.BorderThickness = d1;
                    this.BorderBrush = new SolidColorBrush(Colors.Black);
                }
            }
        }
        public void SetUnSelectedState()
        {
            if (this.IsSelected)
                this.IsSelected = false;
            else
                this.IsSelected = true;
        }
        #endregion 处理选中.

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
        }

        public BPDtl()
        {
            this.Foreground = new SolidColorBrush(Colors.Green);
            this.FontStyle = FontStyles.Normal;
            this.Width = 400;
            this.Height = 200;
            this.BorderThickness = new Thickness(5);
        }
        /// <summary>
        /// Dtl
        /// </summary>
        public BPDtl(string name)
        {
            this.Name = name;
            this.Foreground = new SolidColorBrush(Colors.Green);
            this.FontStyle = FontStyles.Normal;
            this.Width = 400;
            this.Height = 200;
            this.BorderThickness = new Thickness(5);
            this.LoadDtl();
        }
        public void LoadDtl()
        {
            FF.CCFlowAPISoapClient da = Glo.GetCCFlowAPISoapClientServiceInstance();
            da.RunSQLReturnTableAsync("SELECT * FROM Sys_MapAttr WHERE FK_MapData='" + this.Name + "'");
            da.RunSQLReturnTableCompleted += new EventHandler<FF.RunSQLReturnTableCompletedEventArgs>(da_RunSQLReturnTableCompleted);
        }
        void da_RunSQLReturnTableCompleted(object sender, FF.RunSQLReturnTableCompletedEventArgs e)
        {
            DataSet ds = new DataSet();
            ds.FromXml(e.Result);
            if (ds.Tables[0].Rows.Count == 0)
            {
               // this.NewDtl();
                return;
            }
         
            DataGrid dg = new DataGrid();
            dg.Name = "DG" + this.Name;
            DataGridTextColumn mycol = new DataGridTextColumn();
            mycol.Header = "IDX";
            dg.Columns.Add(mycol);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (dr["UIVisible"].ToString() == "0")
                    continue;

                DataGridTextColumn txtColumn = new DataGridTextColumn();
                txtColumn.Header = dr["Name"];
                dg.Columns.Add(txtColumn);
            }
            dg.Width = this.Width;
            dg.Height = this.Height;
            this.Content = dg;
            this.MyDG = dg;
        }
        public DataGrid MyDG = null;
        public void UpdatePos()
        {
            if (Keyboard.Modifiers == ModifierKeys.Shift)
            {
                MyDG.Width = this.Width;
                MyDG.Height = this.Height;
            }
        }
    }
}
