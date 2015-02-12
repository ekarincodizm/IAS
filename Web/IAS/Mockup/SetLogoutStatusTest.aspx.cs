using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAS.Mockup
{
    public partial class SetLogoutStatusTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //string ss = "alert(document.URL);";
            //string ss = "alert(document.location);";
            //AjaxControlToolkit.ToolkitScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", ss, true);

            //this.Session["currentusername"] = "2491086735245";
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "OPEN_WINDOW", "Logout()", true);

            if (this.IsPostBack)
            {
                
            }

            //this.RegisterOnSubmitStatement("OnSubmitScript", "g_isPostBack = true;");

        }
    }
}