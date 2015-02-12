using System;
using System.Collections.Generic;
using System.Linq;

namespace IAS.Mockup
{
    public partial class GroupApp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
        }

        protected void btnIdCard_Click(object sender, EventArgs e)
        {
            var biz = new BLL.RegistrationBiz();
            var res = biz.VerifyIdCard(txtIdCard.Text);
            Response.Write(res.ResultMessage + " " + res.ErrorMsg);
        }
    }
}
