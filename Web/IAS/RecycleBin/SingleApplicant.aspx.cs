using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Threading;
using IAS.Utils;

namespace IAS.RecycleBin
{
    public partial class SingleApplicant : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int iYear = DateTime.Now.Year;
            txtYear.Text = Convert.ToString(iYear + 543);

            NumericUpDownExtender1.Maximum = iYear + 553;
            NumericUpDownExtender1.Minimum = 0;
            if (!IsPostBack)
            {
                //ModSingleApplicant.Show();
                GetExamPlaceGroup();
                GetLicenseType();
                GetExamTime();
            }
        }

        private Action<DropDownList, DTO.DataItem[]> BindToDDL = (ddl, ls) =>
        {
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Id";
            ddl.DataSource = ls;
            ddl.DataBind();
        };

        private void GetExamPlaceGroup()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetExamPlaceGroup(SysMessage.DefaultSelecting);
            BindToDDL(ddlPlace, ls.DataResponse);
        }

        private void GetLicenseType()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetLicenseType(SysMessage.DefaultSelecting);
            BindToDDL(ddlTypeLicense, ls.DataResponse);
        }

        private void GetExamTime() 
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetExamTime(SysMessage.DefaultSelecting);
            BindToDDL(ddlTime, ls.DataResponse);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (rblDisplay.SelectedValue == "1")
            {
                pnlCalendar.Visible = true;
            }
            else
            {
                pnlCalendar.Visible = false;
            }
        }

        protected void rblDisplay_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblDisplay.SelectedValue == "1")
            {
                pnlCalendar.Visible = true;
            }
            else
            {
                pnlCalendar.Visible = false;
            }
        }

        protected void btnModal_Click(object sender, EventArgs e)
        {
            ModSingleApplicant.Show();
        }

    }
}