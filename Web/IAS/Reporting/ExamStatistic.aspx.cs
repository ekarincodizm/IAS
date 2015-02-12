using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.Utils;

namespace IAS.Reporting
{
    public partial class ExamStatistic : basepage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtDateStart.Attributes.Add("readonly", "true");
            txtDateEnd.Attributes.Add("readonly", "true");
            if (!IsPostBack)
            {
                txtDateStart.Text = DateTime.Today.ToString("dd/MM/yyyy");
                txtDateStart.Text = DateUtil.dd_MM_yyyy_Now_TH;

                txtDateEnd.Text = DateTime.Today.ToString("dd/MM/yyyy");
                txtDateEnd.Text = DateUtil.dd_MM_yyyy_Now_TH;
            }

        }

        protected void btnViewer_Click(object sender, EventArgs e)
        {
            if (txtDateStart.Text == "" && txtDateEnd.Text != "")
            {
                UCError.ShowMessageError = "กรุณาเลือก วันที่";
                UCError.ShowModalError();
            }
            else if (txtDateEnd.Text == "" && txtDateStart.Text != "")
            {
                UCError.ShowMessageError = "กรุณาเลือก วันที่ถึง";
                UCError.ShowModalError();
            }
            else
            {
                string src = string.Format("'{0}', '{1}', '{2}', '{3}'"
                    , ddlLicenseType.SelectedValue, txtDateStart.Text, txtDateEnd.Text, (ddlLicenseType.SelectedValue != "" ? ddlLicenseType.SelectedItem.Text : ""));
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "", "OpenPopupViewer(" + src + ")", true);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ddlLicenseType.SelectedValue = "";
            txtDateStart.Text = "";
            txtDateEnd.Text = "";
        }
    }
}