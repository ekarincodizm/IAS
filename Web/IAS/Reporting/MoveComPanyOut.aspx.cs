using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.BLL;

namespace IAS.Reporting
{
    public partial class MoveComPanyOut : basepage
    {
        MoveCompanyOutBiz biz = new MoveCompanyOutBiz();
        protected void Page_Load(object sender, EventArgs e)
        {


            txtStartDate.Attributes.Add("readonly", "true");
            txtEndDate.Attributes.Add("readonly", "true");

            if (!IsPostBack)
            {
                txtStartDate.Text = DateTime.Now.ToShortDateString();
                txtEndDate.Text = DateTime.Now.ToShortDateString();

                ddlLicenseType.DataSource = biz.GetLicenseType();
                ddlLicenseType.DataBind();

                ddlCompany.DataSource = biz.GetCompany();
                ddlCompany.DataBind();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ddlCompany.SelectedValue = "";
            ddlLicenseType.SelectedValue = "";
            txtStartDate.Text = DateTime.Now.ToShortDateString();
            txtEndDate.Text = DateTime.Now.ToShortDateString();
        }
    }
}