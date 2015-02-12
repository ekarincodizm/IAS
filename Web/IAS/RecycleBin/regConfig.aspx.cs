using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAS.RecycleBin
{
    public partial class regConfig : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TabControl.CssClass = "Clicked";
                MainView.ActiveViewIndex = 0;
            }
        }

        protected void TabControl_Click(object sender, EventArgs e)
        {
            TabControl.CssClass = "Clicked";
            TabConfig.CssClass = "Initial";
            MainView.ActiveViewIndex = 0;
        }

        protected void TabConfig_Click(object sender, EventArgs e)
        {
            TabControl.CssClass = "Initial";
            TabConfig.CssClass = "Clicked";
            MainView.ActiveViewIndex = 1;
        }
    }
}