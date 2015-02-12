using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.BLL;
using IAS.DTO;

namespace IAS
{
    public partial class AutoLogout : System.Web.UI.Page
    {
        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnClose.Attributes.Add("onclick", "JavaScript:window.close(); return false;");
                GetSession();
            }

        }
        #endregion


        #region UI
        protected void btnClose_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region Private & Public Function
        private void GetSession()
        {
            PersonBiz biz = new PersonBiz();
            var curUserName = HttpContext.Current.Session["currentusername"];

            
            //if (curUserName != null)
            if ((curUserName != null) && (SessionsState.ISessionID.Equals(HttpContext.Current.Session.SessionID)))
            {
                ResponseMessage<bool> res = biz.SetOffLineStatus(curUserName.ToString());
                if (res.ResultMessage == true)
                {
                    Session["currentusername"] = null;
                   // Session.Clear();
                }
            }
            else
            {
               // Session.Clear();
                //Response.Redirect("~/home.aspx");
                //Response.Redirect("~/AutoLogout.aspx");
            }
        }

        #endregion

    }
}