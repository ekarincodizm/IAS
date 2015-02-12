using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAS.UserControl
{
    public partial class UCCalendar : basepage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void cld_SelectionChanged(object sender, EventArgs e)
        {
            //gvExamSchedule.Visible = true;

            //DateTime cldselectDate = cld.SelectedDate;

            //string strYear = cld.SelectedDate.Year.ToString();
            //string strMonth = cld.SelectedDate.Month.ToString();
            //if (cld.SelectedDate.Month < 10)
            //{
            //    strMonth = "0" + strMonth;
            //}
            //BLL.ExamScheduleBiz biz = new BLL.ExamScheduleBiz();
            //var res = biz.GetExamByTestCenter("", "", "", strYear + strMonth, "", cldselectDate, base.UserProfile.CompCode);

            //gvExamSchedule.DataSource = res.DataResponse;
            //gvExamSchedule.DataBind();
        }
    }
}