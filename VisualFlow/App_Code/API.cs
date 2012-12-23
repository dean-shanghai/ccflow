using System;
using System.Collections.Generic;
using System.Data;
using BP.DA;
using BP.WF;
using BP.En;
using System.Web;
using System.Web.Services;

/// <summary>
///DocFlow 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
// [System.Web.Script.Services.ScriptService]
public class API : BP.Web.WSBase {

    /// <summary>
    /// API
    /// </summary>
    public API()
    {

    }

}
