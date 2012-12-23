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
using BP.Sys;
using BP.DA;
using BP.Web;

public partial class Comm_Sys_ConnView : WebPage
{
    public void BindDtl_Oracle()
    {
        int id = int.Parse( this.Request.QueryString["ID"] );
        BP.DA.ConnOfOra conn = DBAccess.HisConnOfOras.GetByID(id);
        string dotype=this.Request.QueryString["DoType"] ;
        if (dotype == "Clear")
            conn.DoClearSQL();

        this.Ucsys1.AddTable();
        this.Ucsys1.AddCaptionLeftTX(" ID (" + id + ") Connection Run SQLs ,[<a href='ConnView.aspx?ID=" + id + "'>All</a>][<a href='ConnView.aspx?ID=" + id + "&DoType=Distinct'>Distinct</a>][<a href='ConnView.aspx?ID=" + id + "&DoType=Clear'>Clear</a>]");
        this.Ucsys1.AddTR();
        this.Ucsys1.AddTDTitle("ID");
        this.Ucsys1.AddTDTitle("SQL");
        this.Ucsys1.AddTREnd();


        System.Collections.Hashtable ht = new Hashtable();
        int i = 0;
        foreach (string s in conn.SQLs)
        {
            if (s == null || s == "")
                continue;

            if (dotype == "Distinct")
            {
                if (ht.Contains(s) )
                    continue;

                ht.Add(s, s);
            }
            
            i++;

            if (s == null)
                break;

            this.Ucsys1.AddTR();
            this.Ucsys1.AddTDIdx(i);

            if (i != conn.SQLID)
                this.Ucsys1.AddTDBigDoc(s);
            else
                this.Ucsys1.AddTDBigDoc("<font color=green>" + s + "</font>");

            this.Ucsys1.AddTREnd();
        }
        this.Ucsys1.AddTableEnd();
    }
    public void BindDtl_SQL()
    {
        int id = int.Parse(this.Request.QueryString["ID"]);
        BP.DA.ConnOfSQL conn = DBAccess.HisConnOfSQLs.GetByID(id);
        string dotype = this.Request.QueryString["DoType"];
        if (dotype == "Clear")
            conn.DoClearSQL();

        this.Ucsys1.AddTable();
        this.Ucsys1.AddCaptionLeftTX(" ID (" + id + ") Connection Run SQLs ,[<a href='ConnView.aspx?ID=" + id + "'>All</a>][<a href='ConnView.aspx?ID=" + id + "&DoType=Distinct'>Distinct</a>][<a href='ConnView.aspx?ID=" + id + "&DoType=Clear'>Clear</a>]");
        this.Ucsys1.AddTR();
        this.Ucsys1.AddTDTitle("ID");
        this.Ucsys1.AddTDTitle("SQL");
        this.Ucsys1.AddTREnd();


        System.Collections.Hashtable ht = new Hashtable();
        int i = 0;
        foreach (string s in conn.SQLs)
        {
            if (s == null || s == "")
                continue;

            if (dotype == "Distinct")
            {
                if (ht.Contains(s))
                    continue;
                ht.Add(s, s);
            }

            i++;

            if (s == null)
                break;

            this.Ucsys1.AddTR();
            this.Ucsys1.AddTDIdx(i);

            if (i != conn.SQLID)
                this.Ucsys1.AddTDBigDoc(s);
            else
                this.Ucsys1.AddTDBigDoc("<font color=green>" + s + "</font>");

            this.Ucsys1.AddTREnd();
        }
        this.Ucsys1.AddTableEnd();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Request.QueryString["ID"] != null)
        {
            switch (BP.SystemConfig.AppCenterDBType)
            {
                case DBType.SQL2000:
                    this.BindDtl_SQL();
                    break;
                case DBType.Oracle9i:
                    this.BindDtl_Oracle();
                    break;
                default:
                    break;
            }
            return;
        }
        this.Ucsys1.AddTable();
        this.Ucsys1.AddCaptionLeftTX("Connection View <a href=\"javascript:window.location.reload();\" >reload</a>");
        this.Ucsys1.AddTR();
        this.Ucsys1.AddTDTitle("ID");
        this.Ucsys1.AddTDTitle("Using State");
        this.Ucsys1.AddTDTitle("Connection State");
        this.Ucsys1.AddTDTitle("Using Times");
        this.Ucsys1.AddTDTitle("Connection String");
        this.Ucsys1.AddTDTitle("Operator");
        this.Ucsys1.AddTREnd();


        switch(BP.SystemConfig.AppCenterDBType)
        { 
            case DBType.Oracle9i:
                BindOracle();
                break;
            case DBType.SQL2000:
                BindSQL();
                break;
            default:
                break;
        }

        this.Ucsys1.AddTable();
    }
    public void BindSQL()
    {
        BP.DA.ConnOfSQLs conns = DBAccess.HisConnOfSQLs;
        foreach (BP.DA.ConnOfSQL conn in conns)
        {
            this.Ucsys1.AddTR();
            this.Ucsys1.AddTDIdx(conn.IDX);
            this.Ucsys1.AddTD(conn.IsUsing.ToString());

            if (conn.Conn == null)
                this.Ucsys1.AddTD("NULL");
            else
                this.Ucsys1.AddTD(conn.Conn.State.ToString());

            if (conn.Times == 0)
                this.Ucsys1.AddTD(conn.Times);
            else
                this.Ucsys1.AddTD("<a href=\"javascript:WinOpen('ConnView.aspx?ID=" + conn.IDX + "');\" >" + conn.Times + "</a>");


            if (conn.Conn == null)
                this.Ucsys1.AddTD("NULL");
            else
                this.Ucsys1.AddTD(conn.Conn.ConnectionString);

            this.Ucsys1.AddTD("<a href=\"javascript:WinOpen('ConnView.aspx?ID=" + conn.IDX + "&DoType=Clear');\" >Clear SQLs</a>");

            this.Ucsys1.AddTREnd();
        }
    }
    public void BindOracle()
    {
        BP.DA.ConnOfOras conns = DBAccess.HisConnOfOras;
        foreach (BP.DA.ConnOfOra conn in conns)
        {
            this.Ucsys1.AddTR();
            this.Ucsys1.AddTDIdx(conn.IDX);
            this.Ucsys1.AddTD(conn.IsUsing.ToString());

            if (conn.Conn == null)
                this.Ucsys1.AddTD("NULL");
            else
                this.Ucsys1.AddTD(conn.Conn.State.ToString());

            if (conn.Times == 0)
                this.Ucsys1.AddTD(conn.Times);
            else
                this.Ucsys1.AddTD("<a href=\"javascript:WinOpen('ConnView.aspx?ID=" + conn.IDX + "');\" >" + conn.Times + "</a>");


            if (conn.Conn == null)
                this.Ucsys1.AddTD("NULL");
            else
                this.Ucsys1.AddTD(conn.Conn.ConnectionString);

            this.Ucsys1.AddTD("<a href=\"javascript:WinOpen('ConnView.aspx?ID=" + conn.IDX + "&DoType=Clear');\" >Clear SQLs</a>");

            this.Ucsys1.AddTREnd();
        }
    }
}
