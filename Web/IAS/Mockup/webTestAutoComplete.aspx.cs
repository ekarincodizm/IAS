using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAS.Mockup
{
    public partial class webTestAutoComplete : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
                var ls = biz.GetTitleName("เลือก");
                ddl.DataValueField = "Id";
                ddl.DataTextField = "Name";
                ddl.DataSource = ls;
                ddl.DataBind();

                ddl.Items.Cast<ListItem>().FirstOrDefault(s => s.Value == "1").Selected = true;

                //for (int i = 0; i < ddl.Items.Count; i++)
                //{
                //    if (ddl.Items[i].Value == "01")
                //    {
                //        ddl.Items[i].Selected = true;
                //        break;
                //    }
                //}
            }
        }
    }
}