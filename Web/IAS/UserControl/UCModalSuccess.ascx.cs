using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAS.UserControl
{
    public partial class UCModalSuccess : System.Web.UI.UserControl
    {
        public string ShowMessageSuccess
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

        public void HideModalSuccess()
        {
            mpeMessageModalSuccess.Hide();
        }

        public void ShowModalSuccess()
        {
            mpeMessageModalSuccess.Show();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            pnlMessageModalSuccess.Style.Add("display", "none");
            btnClose.OnClientClick = String.Format("fnClickOK('{0}','{1}')", btnClose.UniqueID, "");
            lbtnClose.OnClientClick = String.Format("fnClickOK('{0}','{1}')", btnClose.UniqueID, "");
        }
    }
}