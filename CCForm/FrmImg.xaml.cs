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

namespace CCForm
{
    public partial class FrmImg : ChildWindow
    {
        public FrmImg()
        {
            InitializeComponent();
        }
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("在施工中!");

            this.HisImg.WinURL = this.TB_Url.Text;
            this.HisImg.WinTarget = this.TB_WinName.Text;

            BPImg img = Glo.currEle as BPImg;
            img.WinURL = this.TB_Url.Text;
            img.WinTarget = this.TB_WinName.Text;
            this.DialogResult = true;
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
        public BPImg HisImg = null;
        public void BindIt(BPImg img)
        {
            HisImg = img;
            this.TB_Url.Text = img.WinURL;
            Glo.BindComboBoxWinOpenTarget(this.DDL_WinName, img.WinTarget);
            this.Show();
        }
        private void DDL_WinName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem it = (ComboBoxItem)this.DDL_WinName.SelectedItem;
            if (it == null)
                return;
            this.TB_WinName.Text = it.Tag.ToString();
            if (this.TB_WinName.Text == "def")
                this.TB_WinName.Text = "";
        }
    }
}

