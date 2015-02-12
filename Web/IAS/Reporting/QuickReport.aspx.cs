using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.BLL;
using IAS.Utils;

namespace IAS.Reporting
{
    public partial class QuickReport : basepage
    {
        DataCenterBiz dcbiz = new DataCenterBiz();
        private int DefaultDays = 5;

        protected void Page_Load(object sender, EventArgs e)
        {


            txtDateStart.Attributes.Add("readonly", "true");
            txtDateEnd.Attributes.Add("readonly", "true");

            if (!IsPostBack)
            {
                SetControlPageLoad();
                defaultData();
            }
        }

        private void defaultData()
        {
            txtDateStart.Text = DateTime.Today.ToString("dd/MM/yyyy");
            txtDateStart.Text = DateUtil.dd_MM_yyyy_Now_TH;

            txtDateEnd.Text = DateTime.Today.ToString("dd/MM/yyyy");
            txtDateEnd.Text = DateUtil.dd_MM_yyyy_Now_TH;
        }

        private Action<DropDownList, DTO.DataItem[]> BindToDDL = (ddl, ls) =>
        {
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Id";
            ddl.DataSource = ls;
            ddl.DataBind();
        };

        protected void SetControlPageLoad()
        {
            SetPetitionType();
        }

        protected void SetPetitionType()
        {
            List<DTO.DataItem> items = dcbiz.GetConfigPetitionLicenseType("ทั้งหมด").DataResponse.ToList();
            items = items.Where(s => s.Id == "13" || s.Id == "14" || s.Id == "16" || s.Id == null).ToList();
            BindToDDL(ddlPetitionType, items.ToArray());
        }

        protected void ResetControl()
        {
            ddlPetitionType.SelectedValue = "";
            txtDateStart.Text = "";
            txtDateEnd.Text = "";
            txtCompCode.Text = "";
            txtDays.Text = DefaultDays.ToString();
            tdDay1.Visible = false;
            tdDay2.Visible = false;
        }

        protected void ddlPetitionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPetitionType.SelectedValue == "13" || ddlPetitionType.SelectedValue == "14")
            {
                tdDay1.Visible = true;
                tdDay2.Visible = true;
            }
            else
            {
                tdDay1.Visible = false;
                tdDay2.Visible = false;
            }
            txtDays.Text = DefaultDays.ToString();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ResetControl();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string PetitionType = ddlPetitionType.SelectedValue.Trim();
            string Days = (txtDays.Text != "" && int.Parse(txtDays.Text) != 0) ? txtDays.Text : "";
            string param = String.Format("'{0}','{1}','{2}','{3}','{4}'",
                                PetitionType,
                                txtDateStart.Text,
                                txtDateEnd.Text,
                                txtCompCode.Text,
                                (PetitionType == "13" || PetitionType == "14") ? Days : "");
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "", "OpenPopupViewer(" + param + ")", true);
        }

    }
}