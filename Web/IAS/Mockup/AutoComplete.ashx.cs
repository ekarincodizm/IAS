using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using IAS.BLL;

namespace IAS.Mockup
{
    /// <summary>
    /// Summary description for AutoComplete
    /// </summary>


    public class AutoComplete : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string searchText = context.Request.QueryString["term"];
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            DataCenterBiz biz = new DataCenterBiz();
            var res = biz.GetCompanyCodeByName(searchText);
            string jsonString = serializer.Serialize(res);

            context.Response.Write(jsonString);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}