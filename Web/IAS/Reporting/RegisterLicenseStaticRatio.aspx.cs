using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAS.Reporting
{
    public partial class RegisterLicenseStaticRatio : basepage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtStartDate.Attributes.Add("readonly", "true");
            txtEndDate.Attributes.Add("readonly", "true");

            txtStartDate2.Attributes.Add("readonly", "true");
            txtEndDate2.Attributes.Add("readonly", "true");

            if (!IsPostBack)
            {
                ddlLicenseType.Items.Add(new ListItem("ทั้งหมด", ""));
                ddlLicenseType.Items.Add(new ListItem("ตัวแทนประกันชีวิต", "01"));
                ddlLicenseType.Items.Add(new ListItem("ตัวแทนประกันวินาศภัย", "02"));
                ddlLicenseType.Items.Add(new ListItem("นายหน้าประกันชีวิต(บุคคลธรรมดา)", "03"));
                ddlLicenseType.Items.Add(new ListItem("นายหน้าประกันวินาศภัย(บุคคลธรรมดา)", "04"));

                txtStartDate.Text = DateTime.Now.ToShortDateString();
                txtEndDate.Text = DateTime.Now.ToShortDateString();
                txtStartDate2.Text = DateTime.Now.ToShortDateString();
                txtEndDate2.Text = DateTime.Now.ToShortDateString();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ddlLicenseType.SelectedValue = "";
            txtStartDate.Text = DateTime.Now.ToShortDateString();
            txtEndDate.Text = DateTime.Now.ToShortDateString();
            txtStartDate2.Text = DateTime.Now.ToShortDateString();
            txtEndDate2.Text = DateTime.Now.ToShortDateString();
        }
    }
}