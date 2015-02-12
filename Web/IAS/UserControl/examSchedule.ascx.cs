using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Globalization;
using IAS.Utils;

namespace IAS.UserControl
{
    public partial class examSchedule : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtDateStartExam.Text = DateUtil.dd_MMMM_yyyy_Now_TH;
            txtDateEndExam.Text = DateUtil.dd_MMMM_yyyy_Now_TH;
        }

        protected void rblDisplay_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}