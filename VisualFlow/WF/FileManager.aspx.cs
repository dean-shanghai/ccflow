using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BP.WF;
using BP.Web;
using BP.En;
using BP.DA;

public partial class WF_FileManager : WebPage
{
    public Int64 WorkID
    {
        get
        {
                return Int64.Parse(this.Request.QueryString["WorkID"]);
        }
    }
    public Int64 FID
    {
        get
        {
            return Int64.Parse(this.Request.QueryString["FID"]);
        }
    }
    public FJOpen HisFJOpen
    {
        get
        {
            return (FJOpen)int.Parse(this.Request.QueryString["FJOpen"]);
        }
    }
    public int FJOpenInt
    {
        get
        {
            return int.Parse(this.Request.QueryString["FJOpen"]);
        }
    }
    public string FK_Flow
    {
        get
        {
            return this.Request.QueryString["FK_Flow"];
        }
    }
    public string FK_Node
    {
        get
        {
            return this.Request.QueryString["FK_Node"];
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Page.RegisterClientScriptBlock("s",
          "<link href='" + this.Request.ApplicationPath + "/Comm/Style/Table" + BP.Web.WebUser.Style + ".css' rel='stylesheet' type='text/css' />");


        if (this.Request.QueryString["OID"] != null)
        {
            int oid = int.Parse(this.Request.QueryString["OID"]);
            FileManager fm = new FileManager(oid);
            fm.Delete();
            this.Response.Redirect(this.GenerUrl, true);
            return;
        }
        this.BindFiles();
    }
    public string GenerUrl
    {
        get
        {
            return "FileManager.aspx?FK_Node=" + this.FK_Node + "&WorkID=" + this.WorkID + "&FJOpen=" + this.FJOpenInt + "&FID=" + this.FID;
        }
    }
    public void BindFiles()
    {
        //BP.WF.CHOfFlow f = new CHOfFlow(this.WorkID);
        //Flow fl = new Flow(this.FK_Flow);

        FileManagers fms = new FileManagers();
        QueryObject qo = new QueryObject(fms);
        switch (this.HisFJOpen)
        {
            case FJOpen.ForEmp:
                qo.AddWhere(FileManagerAttr.WorkID, this.WorkID);
                qo.addAnd();
                qo.AddWhere(FileManagerAttr.FK_Emp, BP.Web.WebUser.No);
                qo.addOrderBy(FileManagerAttr.RDT);
                qo.DoQuery();
                break;
            case FJOpen.ForFID:
              //  qo.clear();
                qo.AddWhere(FileManagerAttr.WorkID, this.WorkID);
                qo.addOr();
                qo.AddWhere(FileManagerAttr.FID, this.FID);

                qo.addOrderBy(FileManagerAttr.RDT);
                qo.DoQuery();
                break;
            case FJOpen.ForWorkID:
                fms.Retrieve(FileManagerAttr.WorkID, this.WorkID, FileManagerAttr.RDT);
                break;
            default:
                throw new Exception("@没有判断的情况。");
        }


        this.Pub1.AddFieldSet("文件列表");

        this.Pub1.AddTable(); // ("<Table border=0 width='100%' >");
        //this.Pub1.AddCaptionLeft("流程附件 -  <a href='" + this.GenerUrl + "&DoType=DB' ><img src='../Images/FileType/zip.gif' border=0/>打包下载</a>");

        this.Pub1.AddTR();
        this.Pub1.AddTDTitle("IDX");
        this.Pub1.AddTDTitle("节点");
        this.Pub1.AddTDTitle("上传人");
        this.Pub1.AddTDTitle("文件名称");
        this.Pub1.AddTDTitle("备注");
        this.Pub1.AddTDTitle("日期");
        this.Pub1.AddTDTitle("KB");
        this.Pub1.AddTDTitle("操作");
        this.Pub1.AddTREnd();

        string appPath = this.Request.ApplicationPath;
        int i = 1;
        Node nd = new Node(this.FK_Node);
        Nodes nds = nd.FromNodes;
        foreach (FileManager fm in fms)
        {

            if (nds.Contains(fm.FK_Node) || this.FK_Node == fm.FK_Node)
                this.Pub1.AddTRSum();
            else
                this.Pub1.AddTR();

            this.Pub1.AddTDIdx(i++);

            this.Pub1.AddTD(fm.FK_NodeText);

            if (Glo.IsShowUserNoOnly)
                this.Pub1.AddTD(fm.FK_Emp);
            else
                this.Pub1.AddTD(fm.FK_Emp + "," + fm.FK_EmpText);

            this.Pub1.AddTD("<a href='../DataUser/FlowFile/" + fm.FK_Dept + "/" + fm.OID + "." + fm.Ext + "' target=_bl ><img src='../Images/FileType/" + fm.Ext + ".gif' border=0/>" + fm.Name + "</a>");

            this.Pub1.AddTD(fm.Note);
            this.Pub1.AddTD(fm.RDT);

            this.Pub1.AddTD(fm.FileSize);

            if (fm.FK_Emp == BP.Web.WebUser.No)
                this.Pub1.AddTD("[<a href=\"javascript:DoAction('" + this.GenerUrl + "&OID=" + fm.OID + "', '删除' ) ; \"  ><img src='../Images/Btn/Delete.gif' border=0 />删除</a>]");
            else
                this.Pub1.AddTD("无");

            this.Pub1.AddTREnd();
        }
        this.Pub1.AddTableEnd();

        this.Pub1.AddFieldSetEnd();
        #region 选择文件

        this.Pub1.AddFieldSet("上传文件");

        this.Pub1.Add("选择文件:");
        System.Web.UI.HtmlControls.HtmlInputFile file = new HtmlInputFile();
        file.ID = "File1";
        //file.Attributes.Add("WIDTH","");
        // file.Attributes.Add("style", "Font-Size:XX-Small;WIDTH:60%");
        file.MaxLength = 300;
        this.Pub1.Add(file);

        this.Pub1.Add("&nbsp;&nbsp;备注:");
        TextBox tb = new TextBox();
        tb.ID = "TB_Note";
        this.Pub1.Add(tb);

        Button btn = new Button();
        btn.ID = "Btn_Submit";
        btn.CssClass = "Btn";
        btn.Text = "上传文件";
        // btn.Attributes.Add("style", "Font-Size:XX-Small");
        btn.Click += new EventHandler(btn_Upload_Click);
        //btn.CssClass = "Btn";
        this.Pub1.Add(btn);

        this.Pub1.AddFieldSetEnd(); 
        #endregion

        //this.Pub1.AddFieldSetEnd(); 
        if (this.DoType == null)
            return;

        string zipFile = nd.Name + BP.DA.DataType.CurrentData + "_" + BP.Web.WebUser.No + ".rar";

        string tempDir = "D:\\流程文件\\" + nd.Name + "\\";
        if (System.IO.Directory.Exists(tempDir) == false)
            System.IO.Directory.CreateDirectory(tempDir);

        //ICSharpCode.SharpZipLib.Zip.ZipFile zf = ICSharpCode.SharpZipLib.Zip.ZipFile.Create(tempDir + zipFile);
        //zf.BeginUpdate();
        foreach (FileManager fm in fms)
        {
            string ffile = BP.SystemConfig.PathOfDataUser + @"\\FlowFile\\" + fm.FK_Dept + "\\" + fm.OID + "." + fm.Ext;
            string toFile = tempDir + fm.FK_DeptT + fm.FK_Emp + "_" + fm.Name;

            System.IO.File.Copy(ffile, toFile, true);
            //  zf.Add(toFile);
        }
        //zf.CommitUpdate();
        //zf.Close();
        string httpDir = BP.SystemConfig.PathOfDataUser + @"\\FlowFile\\Temp\\" + WebUser.No + "\\";
        if (System.IO.Directory.Exists(httpDir) == false)
        {
            System.IO.Directory.CreateDirectory(httpDir);
        }

        //  System.IO.File.Copy(ffile, toFile, true);
        System.IO.File.Copy(tempDir + zipFile, httpDir + zipFile, true);
        this.WinOpen("./../DataUser/Tmp/" + zipFile);
    }
    private void btn_Upload_Click(object sender, EventArgs e)
    {
        try
        {
            // string filePath = "D:\\WorkFlow\\FlowFile\\" + BP.Web.WebUser.FK_Dept + "\\";
            string filePath = BP.SystemConfig.PathOfDataUser + @"\\FlowFile\\" + BP.Web.WebUser.FK_Dept + "\\";

            if (System.IO.Directory.Exists(filePath) == false)
                System.IO.Directory.CreateDirectory(filePath);

            System.Web.UI.HtmlControls.HtmlInputFile File1 = (HtmlInputFile)this.Pub1.FindControl("File1");
            FileManager fm = new FileManager();
            fm.Ext = System.IO.Path.GetExtension(File1.PostedFile.FileName).Substring(1);

            string fileName = "";
            if (fileName == null || fileName == "")
                fileName = System.IO.Path.GetFileName(File1.PostedFile.FileName);

            fm.Name = fileName;
            fm.RDT = BP.DA.DataType.CurrentData;
            fm.FileSize = File1.PostedFile.ContentLength;
            fm.WorkID = this.WorkID;
            fm.FK_Emp = BP.Web.WebUser.No;
            fm.FK_Node = this.FK_Node;
            fm.Note = this.Pub1.GetTextBoxByID("TB_Note").Text;

            fm.FK_Dept = BP.Web.WebUser.FK_Dept;
            fm.FID = this.FID;
            fm.Insert();

            File1.PostedFile.SaveAs(filePath + fm.OID + "." + fm.Ext);
            this.Response.Redirect("FileManager.aspx?FK_Node=" + this.FK_Node + "&WorkID=" + this.WorkID + "&FID=" + this.FID + "&FJOpen=" + this.FJOpenInt, true);
        }
        catch (Exception ex)
        {
            this.Response.Write("<font color=red >文件上传递期间出现错误：" + ex.Message + "</font>");
        }
    }
}
