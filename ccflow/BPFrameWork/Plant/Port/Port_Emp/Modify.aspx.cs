﻿using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using Lizard.Common;
using LTP.Accounts.Bus;
namespace BP.EIP.Web.Port_Emp
{
    public partial class Modify : Page
    {       

        		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
				{
					string No= Request.Params["id"];
					ShowInfo(No);
				}
			}
		}
			
	private void ShowInfo(string No)
	{
		BP.EIP.BLL.Port_Emp bll=new BP.EIP.BLL.Port_Emp();
		BP.EIP.Model.Port_Emp model=bll.GetModel(No);
		this.lblNo.Text=model.No;
		this.txtName.Text=model.Name;
		this.txtPass.Text=model.Pass;
		this.txtFK_Dept.Text=model.FK_Dept;
		this.txtPID.Text=model.PID;
		this.txtPIN.Text=model.PIN;
		this.txtKeyPass.Text=model.KeyPass;
		this.txtIsUSBKEY.Text=model.IsUSBKEY;
		this.txtFK_Emp.Text=model.FK_Emp;
		this.chkStatus.Checked=model.Status;

	}

		public void btnSave_Click(object sender, EventArgs e)
		{
			
			string strErr="";
			if(this.txtName.Text.Trim().Length==0)
			{
				strErr+="名称不能为空！\\n";	
			}
			if(this.txtPass.Text.Trim().Length==0)
			{
				strErr+="密码不能为空！\\n";	
			}
			if(this.txtFK_Dept.Text.Trim().Length==0)
			{
				strErr+="部门, 外键:对应物理表:Po不能为空！\\n";	
			}
			if(this.txtPID.Text.Trim().Length==0)
			{
				strErr+="PID不能为空！\\n";	
			}
			if(this.txtPIN.Text.Trim().Length==0)
			{
				strErr+="PIN不能为空！\\n";	
			}
			if(this.txtKeyPass.Text.Trim().Length==0)
			{
				strErr+="KeyPass不能为空！\\n";	
			}
			if(this.txtIsUSBKEY.Text.Trim().Length==0)
			{
				strErr+="IsUSBKEY不能为空！\\n";	
			}
			if(this.txtFK_Emp.Text.Trim().Length==0)
			{
				strErr+="FK_Emp不能为空！\\n";	
			}

			if(strErr!="")
			{
				MessageBox.Show(this,strErr);
				return;
			}
			string No=this.lblNo.Text;
			string Name=this.txtName.Text;
			string Pass=this.txtPass.Text;
			string FK_Dept=this.txtFK_Dept.Text;
			string PID=this.txtPID.Text;
			string PIN=this.txtPIN.Text;
			string KeyPass=this.txtKeyPass.Text;
			string IsUSBKEY=this.txtIsUSBKEY.Text;
			string FK_Emp=this.txtFK_Emp.Text;
			bool Status=this.chkStatus.Checked;


			BP.EIP.Model.Port_Emp model=new BP.EIP.Model.Port_Emp();
			model.No=No;
			model.Name=Name;
			model.Pass=Pass;
			model.FK_Dept=FK_Dept;
			model.PID=PID;
			model.PIN=PIN;
			model.KeyPass=KeyPass;
			model.IsUSBKEY=IsUSBKEY;
			model.FK_Emp=FK_Emp;
			model.Status=Status;

			BP.EIP.BLL.Port_Emp bll=new BP.EIP.BLL.Port_Emp();
			bll.Update(model);
			Lizard.Common.MessageBox.ShowAndRedirect(this,"保存成功！","list.aspx");

		}


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
