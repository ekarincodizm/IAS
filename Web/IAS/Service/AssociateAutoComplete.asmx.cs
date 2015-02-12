using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace IAS.Service
{
    /// <summary>
    /// Summary description for AssociateAutoComplete
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class AssociateAutoComplete : System.Web.Services.WebService
    {
        [WebMethod]
        public string[] GetAutomCompleteAssociate(string prefixText)
        {
            //BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            //var list = biz.GetAssociate(prefixText);
            //return list;

            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var list = biz.GetInsuranceAssociate(prefixText);
            return list;
        }

    }
}


