using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using IAS.BLL;

namespace IAS.Service
{
    /// <summary>
    /// Summary description for RegistrationWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class RegistrationWebService : System.Web.Services.WebService
    {
        //[WebMethod]
        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public bool VerifyIdCard(string strIdCard)
        {
            BLL.RegistrationBiz biz = new BLL.RegistrationBiz();
            var res = biz.VerifyIdCard(strIdCard);
            return res.ResultMessage;
        }
    }
}
