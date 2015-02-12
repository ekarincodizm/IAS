using System;
using System.Collections.Generic;
using System.Linq;


namespace IAS.UserControl
{
    public partial class UCModalError : System.Web.UI.UserControl
    {
        public string ShowMessageError
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

        public void HideModalError()
        {
            mpeMessageModalError.Hide();
        }

        public void ShowModalError()
        {
            mpeMessageModalError.Show();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            pnlMessageModalError.Style.Add("display", "none");
            btnClose.OnClientClick = String.Format("fnClickOK('{0}','{1}')", btnClose.UniqueID, "");
            lbtnClose.OnClientClick = String.Format("fnClickOK('{0}','{1}')", btnClose.UniqueID, "");
        }
    }
}