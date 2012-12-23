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
using WF.WSFtp;

namespace BP.Controls
{
    public partial class FrmLoginTemLib : ChildWindow
    {
        private bool loginStatus = false;
        CYFtpSoapClient ftpClient;

        public FrmLoginTemLib()
        {
            InitializeComponent();

            ftpClient = Glo.GetFtpServiceInstance();
            ftpClient.CheckUserCompleted += CheckUser_Completed;
        }

        private void CheckUser_Completed(object sender, CheckUserCompletedEventArgs e)
        {
            if (e.Result == "success")
            {
                SessionManager.Session["user"] = txtUserName.Text.Trim();

                this.loginStatus = true;
                this.DialogResult = true;
            }
            else
            {
                MessageBox.Show(e.Result);
                OKButton.IsEnabled = true;
                CancelButton.IsEnabled = true;
                RegButton.IsEnabled = true;
            }
        }

        /// <summary>
        /// 是否登录成功
        /// </summary>
        public bool LoginSuccess
        {
            get
            {
                return loginStatus;
            }
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            this.loginStatus = false;
            this.DialogResult = false;

            FrmRegTemLib frmReg = new FrmRegTemLib();
            frmReg.Show();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserName.Text) || string.IsNullOrEmpty(txtPass.Password))
            {
                MessageBox.Show("用户名或密码不能为空");
                return;
            }

            CancelButton.IsEnabled = false;
            RegButton.IsEnabled = false;
            OKButton.IsEnabled = false;
            ftpClient.CheckUserAsync(txtUserName.Text.Trim(), txtPass.Password.Trim());
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.loginStatus = false;
            this.DialogResult = false;
        }
    }
}

