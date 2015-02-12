using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAS.UserControl
{
    public partial class ucLicense : System.Web.UI.UserControl
    {
        #region Public Param & Session
        public GridView GridLicense { get { return gvLicense; } set { gvLicense = value; } }
        #endregion

        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }
        #endregion

        #region UI Function
        
        #endregion

        #region Main Public && Private Function
        #endregion
    }
}