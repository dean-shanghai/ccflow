using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.Common;
using System.Net;
using System.Net.Mail;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Security.Cryptography;
using BP.WF;
using BP.Port;
using BP.En;
using BP.Sys;
using BP.DA;
using BP;
using BP.Web;
using System.Collections;

namespace SMSServices
{
    public enum ScanSta
    {
        Working,
        Pause,
        Stop
    }
    public partial class FrmMain : Form
    {
        delegate void SetTextCallback(string text);
        public FrmMain()
        {
            InitializeComponent();
        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.Show();
            this.notifyIcon1.Visible = true;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            this.toolStripStatusLabel1.Text = "流程引擎单元测试";
            this.textBox1.Text = "";
        }
        public ScanSta HisScanSta = ScanSta.Pause;
        /// <summary>
        /// 执行线程.
        /// </summary>
        public void RunIt(BP.CT.EditState sta)
        {
            this.toolStripStatusLabel1.Text = "正在准备执行（"+sta.ToString()+"）测试..";
            string errorInfo = "";
            //开始执行信息.
            ArrayList al = BP.DA.ClassFactory.GetObjects("BP.CT.TestBase");

            int i = 0;
            int numOk = 0;
            int numErr = 0;
            foreach (BP.CT.TestBase en in al)
            {
                    if (sta != en.EditState)
                        continue;
                i++;
                this.SetText("========== NO:"+i.ToString().PadLeft(3,'0')+" ========================");
                this.SetText("开始执行:" + en.Title);
                this.SetText("类:" + en.ToString());
                this.SetText("测试内容:" + en.DescIt);

                if (sta == BP.CT.EditState.Editing)
                {
                    /*目的是能够看到异常的位置.*/
                    en.Do();
                    numOk++;
                    this.toolStripStatusLabel1.Text = "执行:" + i + "个,成功:" + numOk + "个,失败:" + numErr + "个";
                    continue;
                }

                try
                {
                    en.Do();
                    this.SetText("成功通过. \t\n");
                    numOk++;
                    this.toolStripStatusLabel1.Text = "执行:" + i + "个,成功:" + numOk + "个,失败:" + numErr + "个";
                }
                catch (Exception ex)
                {
                    this.SetText("Error:" + ex.Message);
                    numErr++;
                    errorInfo += "=== No:" + i.ToString().PadLeft(3, '0') + " unpass.";
                    errorInfo += "\t\nEnName:" + en.ToString();
                    errorInfo += "\t\nTitle:" + en.Title;
                    errorInfo += "\t\nError:" + ex.Message;
                    this.toolStripStatusLabel1.Text = "执行:" + i + "个,成功:" + numOk + "个,失败:" + numErr + "个";
                }

            } //结束循环.

            this.toolStripStatusLabel1.Text = "执行:" + i + "个,成功:" + numOk + "个,失败:" + numErr + "个";

            this.SetText("**************** OVER *********************");
            this.SetText("结果:" + this.toolStripStatusLabel1.Text);

            //写入信息.
            if (string.IsNullOrEmpty(errorInfo))
            {
                BP.DA.DataType.WriteFile("C:\\CCFlowCellTestLog.txt", errorInfo);
            }
        }
        /// <summary>
        /// 设置内容
        /// </summary>
        /// <param name="text"></param>
        private void SetText(string text)
        {
            //写入信息.
            BP.DA.Log.DefaultLogWriteLineInfo(text);
            if (this.textBox1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                try
                {
                    this.Invoke(d, new object[] { text });
                }
                catch
                {
                }
            }
            else
            {
                this.textBox1.Text += "\r\n" + text;
                this.textBox1.SelectionStart = this.textBox1.TextLength;
                this.textBox1.ScrollToCaret();
            }
        }
        private void Btn_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Btn_Editing_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            this.textBox1.Text = "正在为执行单元测试做准备......";
            this.toolStripStatusLabel1.Text = "正在为执行单元测试做准备......";
            btn.Enabled = false;
            this.RunIt(BP.CT.EditState.Editing);
            btn.Enabled = true;
        }

        private void Btn_OK_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            this.textBox1.Text = "正在为执行单元测试做准备......";
            this.toolStripStatusLabel1.Text = "正在为执行单元测试做准备......";
            btn.Enabled = false;
            this.RunIt(BP.CT.EditState.OK);
            btn.Enabled = true;
        }

        private void Btn_All_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            this.textBox1.Text = "正在为执行单元测试做准备......";
            this.toolStripStatusLabel1.Text = "正在为执行单元测试做准备......";
            btn.Enabled = false;
            this.RunIt(BP.CT.EditState.UnOK );
            btn.Enabled = true;
        }

      
    }
}
