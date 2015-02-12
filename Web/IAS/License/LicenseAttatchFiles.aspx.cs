using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.MasterPage;
using IAS.BLL.AttachFilesIAS;
using IAS.BLL;

namespace IAS.License
{
    public partial class LicenseAttatchFiles : basepage
    {
        #region Public Param & Session
        
        public String _personId;

        public MasterLicense MasterLicense
        {
            get
            {
                return (this.Page.Master as MasterLicense);
            }

        }

        public DTO.UserProfile UserProfile
        {
            get
            {
                return Session[PageList.UserProfile] == null ? null : (DTO.UserProfile)Session[PageList.UserProfile];
            }
        }

        public String PersonId
        {
            get { return _personId; }
            set
            {
                _personId = value;
                ViewState["PersonId"] = _personId;
            }
        }
        #endregion


        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                base.HasPermit();

                //GetAttachFilesType();
                InitAttachFileControl();
                GetAttatchFiles();
                this.GetPersonalLicenseApprover();
            }
            else
            {
                ucAttachLicense.AttachFiles = this.MasterLicense.AttachFiles;

            }
        }
        #endregion

        #region Main Public & Private Function
        /// <summary>
        /// Files check before Agreement confirmed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private DTO.ResponseMessage<bool> DocValidation()
        {
            DTO.ResponseMessage<bool> res = new DTO.ResponseMessage<bool>();

            DTO.ResponseMessage<bool> docRes = this.ucAttachLicense.DocRequire();

            if (docRes.ResultMessage == true)
            {
                res = docRes;
            }
            else if ((docRes.ResultMessage == false) || (docRes.IsError))
            {
                this.MasterLicense.UCLicenseUCLicenseModelError.ShowMessageError = docRes.ErrorMsg.ToString();
                this.MasterLicense.UCLicenseUCLicenseModelError.ShowModalError();
                this.MasterLicense.UpdatePanelLicense.Update();
                res = docRes;
            }

            return res;
        }

        public void InitAttachFileControl()
        {
            GetAttachFilesType();
            ucAttachLicense.RegisterationId = this.MasterLicense.UserProfile.Id;
        }

        public void GetAttachFilesType()
        {
            var message = SysMessage.DefaultSelecting;
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetDocumentType(message);

            this.ucAttachLicense.DocumentTypes = ls;
        }

        public void GetAttatchFiles()
        {
            LicenseBiz biz = new LicenseBiz();

            if (this.MasterLicense.AttachFiles == null)
            {
                DTO.ResponseService<DTO.PersonAttatchFile[]> res = biz.GetAttatchFileLicenseByPersonId(this.MasterLicense.UserProfile.Id);
                var list = res.DataResponse.ToList();

                ucAttachLicense.AttachFiles = list.ConvertToAttachFilesView();
                udpMain.Update();
            }
            else if (this.MasterLicense.AttachFiles != null)
            {
                ucAttachLicense.AttachFiles = this.MasterLicense.AttachFiles;
                udpMain.Update();
            }

        }

        public void GetPersonalLicenseApprover()
        {
            LicenseBiz biz = new LicenseBiz();
            DTO.ResponseService<DTO.PersonLicenseApprover[]> approverlist = biz.GetPersonalLicenseApprover(this.MasterLicense.LicenseTypeCode);

            ddlLicenseApprover.DataTextField = "ASSOCIATION_NAME";
            ddlLicenseApprover.DataValueField = "ASSOCIATION_CODE";
            ddlLicenseApprover.DataSource = approverlist.DataResponse;
            ddlLicenseApprover.DataBind();

            if (!string.IsNullOrEmpty(this.MasterLicense.LicenseApprover))
            {
                if (ddlLicenseApprover.Items.FindByValue(this.MasterLicense.LicenseApprover) != null)
                    ddlLicenseApprover.SelectedValue = this.MasterLicense.LicenseApprover;
            }

        }

        private void MenuBackValidation()
        {
            if (this.MasterLicense.Menu != null)
            {
                switch (this.MasterLicense.Menu)
                {
                    case 1:
                        Session["CheckClearSession"] = "P";
                        MasterLicense.Step -= 1;
                        Response.Redirect("../License/NewLicense.aspx?M=" + MasterLicense.Menu + "&S=" + MasterLicense.Step + "");
                        break;
                    case 2:
                        Session["CheckClearSession"] = "P";
                        MasterLicense.Step -= 1;
                        Response.Redirect("../License/RenewLicense.aspx?M=" + MasterLicense.Menu + "&S=" + MasterLicense.Step + "");
                        break;
                    case 3:
                        Session["CheckClearSession"] = "P";
                        MasterLicense.Step -= 1;
                        Response.Redirect("../License/ExpiredRenewLicense.aspx?M=" + MasterLicense.Menu + "&S=" + MasterLicense.Step + "");
                        break;
                    case 4:
                        Session["CheckClearSession"] = "P";
                        MasterLicense.Step -= 1;
                        Response.Redirect("../License/LicenseReplace.aspx?M=" + MasterLicense.Menu + "&S=" + MasterLicense.Step + "");
                        break;
                    case 6:
                        Session["CheckClearSession"] = "P";
                        MasterLicense.Step -= 1;
                        Response.Redirect("../License/LicenseReplaceRename.aspx?M=" + MasterLicense.Menu + "&S=" + MasterLicense.Step + "");
                        break;
                }

            }
        }

        private void MenuForwardValidation()
        {
            if (this.MasterLicense.Menu != null)
            {
                switch (this.MasterLicense.Menu)
                {
                    case 1:
                        this.MasterLicense.getPersonLicenseEntity();
                        MasterLicense.Step += 1;
                        Response.Redirect("../License/LicenseAgreement.aspx?M=" + MasterLicense.Menu + "&S=" + MasterLicense.Step + "");
                        break;
                    case 2:
                        this.MasterLicense.getPersonRenewLicense1Y();
                        MasterLicense.Step += 1;
                        Response.Redirect("../License/LicenseAgreement.aspx?M=" + MasterLicense.Menu + "&S=" + MasterLicense.Step + "");
                        break;
                    case 3:
                        this.MasterLicense.getPersonExpiredLicense();
                        MasterLicense.Step += 1;
                        Response.Redirect("../License/LicenseAgreement.aspx?M=" + MasterLicense.Menu + "&S=" + MasterLicense.Step + "");
                        break;
                    case 4:
                        this.MasterLicense.getReplaceLicenseByLostCase();
                        MasterLicense.Step += 1;
                        Response.Redirect("../License/LicenseAgreement.aspx?M=" + MasterLicense.Menu + "&S=" + MasterLicense.Step + "");
                        break;
                    case 6:
                        this.MasterLicense.getReplaceLicenseByRenameCase();
                        MasterLicense.Step += 1;
                        Response.Redirect("../License/LicenseAgreement.aspx?M=" + MasterLicense.Menu + "&S=" + MasterLicense.Step + "");
                        break;
                }

            }
        }
        #endregion

        #region UI Function
        protected void btnNext_Click(object sender, ImageClickEventArgs e)
        {
            if (ddlLicenseApprover.SelectedItem.Value.Equals("0"))
            {
                this.MasterLicense.UCLicenseUCLicenseModelError.ShowMessageError = "เลือกสมาคมที่ต้องการจะยื่นเรื่องเพื่อขอรับใบอนุญาต";
                this.MasterLicense.UCLicenseUCLicenseModelError.ShowModalError();
                this.MasterLicense.UpdatePanelLicense.Update();
                return;
            }
            else
            {
                DTO.ResponseMessage<bool> res = this.DocValidation();
                if ((res.ResultMessage == false) || (res.IsError))
                {
                    this.MasterLicense.UCLicenseUCLicenseModelError.ShowMessageError = res.ErrorMsg;
                    this.MasterLicense.UCLicenseUCLicenseModelError.ShowModalError();
                    this.MasterLicense.UpdatePanelLicense.Update();
                    return;
                }
                else if (res.ResultMessage == true)
                {
                    this.MasterLicense.LicenseApprover = ddlLicenseApprover.SelectedValue;
                    this.MenuForwardValidation();
                }
                
            }
        }

        protected void btnBack_Click(object sender, ImageClickEventArgs e)
        {
            this.MenuBackValidation();
        }
        #endregion

    }
}