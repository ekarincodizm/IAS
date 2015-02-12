using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.MasterPage;
using System.Data;

namespace IAS.License
{
    public partial class LicenseReplaceRename : basepage
    {
        #region Session
        public MasterLicense MasterLicense
        {
            get
            {
                return (this.Page.Master as MasterLicense);
            }

        }

        protected void ClearSessionLicense()
        {
            Session["lsLicensePayment"] = null;
            Session["personlicenseh"] = null;
            Session["personlicensed"] = null;
            Session["attachfiles"] = null;
            Session["PersonalSkillStage"] = null;
        }
        #endregion

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.MasterLicense.LicenseGroupValidation = "ReplaceRenameValidationGroup";
                this.MasterLicense.ListUploadGroupNo = new List<string>();
                base.HasPermit();
                if (Session["CheckClearSession"] == null)
                {
                    ClearSessionLicense();
                }
                else
                {
                    Session["CheckClearSession"] = null;
                }

                if ((this.MasterLicense.SelectedLicenseNo != null) && (this.MasterLicense.SelectedLicenseNo != ""))
                {
                    this.MasterLicense.SelectedLicenseNo = "";
                }

                CheckSessionRemember();
            }
        }
        #endregion

        #region Functional
        private void CheckSessionRemember()
        {
            if (Session["CheckClearSession"] == null)
            {
                this.MasterLicense.LicenseTypeCode = string.Empty;
                this.MasterLicense.LicenseApprover = string.Empty;
                this.MasterLicense.SelectAgreement = false;
                this.MasterLicense.PersonalSkillStage = string.Empty;
            }
        }

        #endregion

        #region UI
        protected void btnNext_Click(object sender, ImageClickEventArgs e)
        {
            //Control Validation
            DTO.ResponseMessage<bool> res = this.MasterLicense.ControlValidationBeforeSubmit(this.Page);
            if ((res.ResultMessage == false) || (res.IsError))
            {
                this.MasterLicense.UCLicenseUCLicenseModelError.ShowMessageError = res.ErrorMsg;
                this.MasterLicense.UCLicenseUCLicenseModelError.ShowModalError();
                return;
            }

            if ((this.MasterLicense.SelectedLicenseNo == null) || (this.MasterLicense.SelectedLicenseNo == ""))
            {
                this.MasterLicense.UCLicenseUCLicenseModelError.ShowMessageError = SysMessage.LicenseNull;
                this.MasterLicense.UCLicenseUCLicenseModelError.ShowModalError();
                this.MasterLicense.UpdatePanelLicense.Update();
                return;
            }
            else
            {
                if (this.ucReplaceLi.GridviewRenewLicense.Rows.Count > 0)
                {
                    this.MasterLicense.LicenseRenameFirstName = txtFirstName.Text.Trim();
                    this.MasterLicense.LicenseRenameLastName = txtLastName.Text.Trim();
                    MasterLicense.Step += 1;
                    Response.Redirect("../License/LicenseAttatchFiles.aspx?M=" + MasterLicense.Menu + "&S=" + MasterLicense.Step + "");

                }
                else
                {
                    this.MasterLicense.UCLicenseUCLicenseModelError.ShowMessageError = SysMessage.RenewLicenseNotFound;
                    this.MasterLicense.UCLicenseUCLicenseModelError.ShowModalError();
                    this.MasterLicense.UpdatePanelLicense.Update();
                    return;

                }
            }
        }
        #endregion

    }
}