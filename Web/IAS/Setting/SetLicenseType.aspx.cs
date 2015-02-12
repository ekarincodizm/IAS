using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.BLL;
using IAS.Properties;

namespace IAS.Setting
{
    public partial class SetLicenseType : System.Web.UI.Page
    {
        LicenseTypeBiz biz = new LicenseTypeBiz();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        private void BindGrid()
        {
            if (Cache["AgentType"] == null)
            {
                Cache["AgentType"] = biz.GetAgentTypeList().DataResponse;
            }
          
            ddlAgentype.DataSource = Cache["AgentType"];
            ddlAgentype.DataBind();

            ddlAgentTypeUpdate.DataSource = Cache["AgentType"];
            ddlAgentTypeUpdate.DataBind();
           

            GvLicense.DataSource = biz.GetLicensetypeList("").DataResponse;
            GvLicense.DataBind();         

            ddlAgentype.Items.Add(new ListItem("--เลือก--", ""));
            ddlAgentype.SelectedValue = "";

            ddlInsuran.Items.Add(new ListItem("--เลือก--", ""));
            ddlInsuran.SelectedValue = "";
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            DTO.LicenseTypet licensetype = new DTO.LicenseTypet();
            licensetype.AGENT_TYPE = ddlAgentTypeUpdate.SelectedValue;
            licensetype.INSURANCE_TYPE = ddlInsuranUpdate.SelectedValue;
            licensetype.LICENSE_TYPE_CODE = hdTypeCode.Value;
            licensetype.LICENSE_TYPE_NAME = txtUpdatename.Text;
            var res = biz.UpdateLicenseType(licensetype);
            if (res.ErrorMsg != null)
            {
                UCModalError1.ShowMessageError = res.ErrorMsg;
                UCModalError1.ShowModalError();
            }
            else
            {
                txtLicenseName.Text = "";
                BindDrop();
                ddlInsuran.SelectedValue = "";
                ddlAgentype.SelectedValue = "";
                UCModalSuccess1.ShowMessageSuccess = Resources.infoSetSubject_002;
                UCModalSuccess1.ShowModalSuccess();

            }
            ModalPopupUpdate.Hide();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ModalPopupUpdate.Hide();
        }

        protected void lbtUpdate_Click(object sender, EventArgs e)
        {
            hdTypeCode.Value = ((Label)((GridViewRow)((LinkButton)sender).Parent.Parent).FindControl("lblTypeCode")).Text;
            ddlAgentTypeUpdate.SelectedValue = ((Label)((GridViewRow)((LinkButton)sender).Parent.Parent).FindControl("lblAgenCode")).Text;
            ddlInsuranUpdate.SelectedValue = ((Label)((GridViewRow)((LinkButton)sender).Parent.Parent).FindControl("lblInsuranCode")).Text;
            txtUpdatename.Text = ((Label)((GridViewRow)((LinkButton)sender).Parent.Parent).FindControl("lblNameType")).Text;
            ModalPopupUpdate.Show();
        }

        protected void ddlAgentype_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDrop();
        }

        private void BindDrop()
        {
            GvLicense.DataSource = biz.GetLicensetypeList(ddlAgentype.SelectedValue).DataResponse;
            GvLicense.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            DTO.LicenseTypet licensetype = new DTO.LicenseTypet();
            licensetype.AGENT_TYPE = ddlAgentype.SelectedValue;
            licensetype.INSURANCE_TYPE = ddlInsuran.SelectedValue;
            licensetype.LICENSE_TYPE_NAME = txtLicenseName.Text;
            var res = biz.AddLicenseType(licensetype);
            if (res.ErrorMsg != null)
            {
                UCModalError1.ShowMessageError = res.ErrorMsg;
                UCModalError1.ShowModalError();            
            }
            else
            {
                txtLicenseName.Text = "";               
                BindDrop();
                ddlInsuran.SelectedValue = "";
                ddlAgentype.SelectedValue = "";
                UCModalSuccess1.ShowMessageSuccess = "";
                UCModalSuccess1.ShowModalSuccess();              
            }
        }

        protected void lbtDelete_Click(object sender, EventArgs e)
        {
            string licensecode = ((Label)((GridViewRow)((LinkButton)sender).Parent.Parent).FindControl("lblTypeCode")).Text;
            var res = biz.DeleteLicenseType(licensecode);
            if (res.ErrorMsg != null)
            {
                UCModalError1.ShowMessageError = res.ErrorMsg;
                UCModalError1.ShowModalError();
            }
            else
            {
                BindDrop();
                UCModalSuccess1.ShowMessageSuccess = Resources.infoSetSubject_003;
                UCModalSuccess1.ShowModalSuccess();
            }
        }
    }
}