using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAS.UserControl
{
    public partial class ucLicenseArgreement : System.Web.UI.UserControl
    {
        #region Public & Private Session
        public System.Web.UI.HtmlControls.HtmlControl IFramArgree { get { return iframAgree; } }

        #endregion

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }
        #endregion


        #region Public & Private Function

        #endregion

        #region UI Function

        #endregion
    }
}