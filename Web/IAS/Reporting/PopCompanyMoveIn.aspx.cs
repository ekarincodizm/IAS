using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.BLL;

namespace IAS.Reporting
{
    public partial class PopCompanyMoveIn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MoveCompanyInBiz biz = new MoveCompanyInBiz();
            GridView1.DataSource = biz.get();
            GridView1.DataBind();
        }
    }
}