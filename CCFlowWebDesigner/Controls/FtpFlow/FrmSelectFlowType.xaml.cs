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
using System.Collections;

using Silverlight;
using WF.WS;
using BP;

namespace BP.Controls
{
    public partial class FrmSelectFlowType : ChildWindow
    {
        WSDesignerSoapClient wsDesignerClient = null;

        /// <summary>
        /// 模板类型
        /// </summary>
        public string FlowType
        {
            get
            {
                if (cbxFlowSortImport.Items.Count > 0)
                {
                    return (cbxFlowSortImport.SelectedItem as BindableObject).GetValue("NO");
                }
                else
                {
                    return "";
                }
            }
        }

        public FrmSelectFlowType()
        {
            InitializeComponent();

            wsDesignerClient = Glo.GetDesignerServiceInstance();

            wsDesignerClient.DoAsync("GetFlows", string.Empty, true);
            wsDesignerClient.DoCompleted += GetFlows_Completed;
        }

        void GetFlows_Completed(object sender, DoCompletedEventArgs e)
        {
            DataSet ds = new DataSet();
            ds.FromXml(e.Result);

            // 得到默认的流程类别
            int defaultFlowSort = 0;

            IList list = ds.Tables[0].GetBindableData(new Connector());

            if (list.Count > 0)
            {
                cbxFlowSortImport.ItemsSource = list;
                cbxFlowSortImport.DisplayMemberPath = "NAME";
                cbxFlowSortImport.SelectedIndex = defaultFlowSort;
            }

            wsDesignerClient.DoCompleted -= GetFlows_Completed;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}

