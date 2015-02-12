using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.MasterPage;

namespace IAS.License
{
    public partial class LicenseReplace : basepage
    {
        #region Private & Public Session
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

        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

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

            this.MasterLicense.PersonalSkillStage = string.Empty;

        }
        #endregion

        #region Main Public & Private Function
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

        #region UI Function
        protected void btnNext_Click(object sender, ImageClickEventArgs e)
        {
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