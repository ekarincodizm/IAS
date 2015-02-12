using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.BLL;
using IAS.DTO;

namespace IAS.Mockup
{
    public partial class AutoLogOut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PersonBiz biz = new PersonBiz();
            HttpContext.Current.Session["currentusername"] = "2491086735245";
            var curUserName = HttpContext.Current.Session["currentusername"];

            if (curUserName != null)
            {
                ResponseMessage<bool> res = biz.SetOffLineStatus("2491086735245");

                if (res.ResultMessage == true)
                {
                    //Session["currentusername"] = null;
                    //Session.Clear();
                    //Response.Redirect("~/home.aspx");
                }
            }

            string logoffDatabase = (this.Request["LogoffDatabase"] == null) ? string.Empty : this.Request["LogoffDatabase"];
            string returnValue = string.Empty;

            if (logoffDatabase == "Y")
            {
                if (returnValue.Length > 0)
                    returnValue += ", ";

                returnValue += this.LogoffUser(this.Session["currentusername"].ToString());
            }

            this.Response.ClearHeaders();
            this.Response.Clear();
            this.Response.Write(returnValue);
            this.Response.End();
        }

        protected string LogoffUser(string userId)
        {
            string returnValue = "OK";

            // Call the database, logging the user off here...

            return returnValue;
        }
    }
}