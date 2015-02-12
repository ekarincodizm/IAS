using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAS.Mockup
{
    public partial class WebFormTestAutoComplete : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                var biz = new BLL.DataCenterBiz();

                List<DTO.DataItem> list = biz.GetCompanyCode("");

                ddlTest.DataValueField = "Id";
                ddlTest.DataTextField = "Name";
                ddlTest.DataSource = list;
                ddlTest.DataBind();

            }
        }
    }
}