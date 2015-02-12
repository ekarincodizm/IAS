using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAS.UserControl
{
    public partial class UCModalWarning : System.Web.UI.UserControl
    {
        public string ShowMessageWarning
        {
            get
            {
                if (ViewState["AnyMessage"] == null) ViewState["AnyMessage"] = string.Empty;

                return (string)ViewState["AnyMessage"];
            }

            set
            {
                ViewState["AnyMessage"] = value;
            }

        }

        public void HideModalWarning()
        {
            mpeMessageModalWarning.Hide();
        }

        public void ShowModalWarning()
        {
            mpeMessageModalWarning.Show();
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            pnlMessageModalWarning.Style.Add("display", "none");
            //btnYes.OnClientClick = String.Format("fnClickOK('{0}','{1}')", btnNo.UniqueID, "");
            btnNo.OnClientClick = String.Format("fnClickOK('{0}','{1}')", btnNo.UniqueID, "");
        }
    }
}