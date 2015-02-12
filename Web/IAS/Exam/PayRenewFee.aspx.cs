using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAS.Exam
{
    public partial class PayRenewFee : basepage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

            }
        }

        protected void btnLoadFile_Click(object sender, EventArgs e)
        {
            BLL.PaymentBiz biz = new BLL.PaymentBiz();
           // var res = biz.
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

        }
    }
}