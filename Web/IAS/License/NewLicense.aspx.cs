using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.MasterPage;
using IAS.DTO;

using IAS.BLL;
using System.Text;
using System.Security.Cryptography;

namespace IAS.License
{
    public partial class NewLicense : basepage
    {
        #region Public Param & Session
        public MasterLicense MasterLicense
        {
            get
            {
                return (this.Page.Master as MasterLicense);
            }

        }

        public IAS.MasterPage.MasterRegister MasterRegis
        {
            get
            {
                return (this.Page.Master as IAS.MasterPage.MasterRegister);
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
            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>changeUrl();</script>", false);
            if (!IsPostBack)
            {
                base.HasPermit();
                CheckSessionRemember();
                PageInit();
                if (Session["CheckClearSession"] == null)
                {
                    ClearSessionLicense();
                }
                else
                {
                    Session["CheckClearSession"] = null;
                }
            }

            this.MasterLicense.PersonalSkillStage = string.Empty;
        }
        #endregion

        #region Main Public & Private Function
        private Action<DropDownList, DTO.DataItem[]> BindToDDL = (ddl, ls) =>
        {
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Id";
            ddl.DataSource = ls;
            ddl.DataBind();
        };

        private void PageInit()
        {
            GetLicenseType();

            if(!string.IsNullOrEmpty(this.MasterLicense.LicenseTypeCode)){
                if (ddBusinessType.Items.FindByValue(this.MasterLicense.LicenseTypeCode) != null)
                    ddBusinessType.SelectedValue = this.MasterLicense.LicenseTypeCode;
            }
        }

        private void GetLicenseType()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            DTO.ResponseService<DTO.DataItem[]> ls = biz.GetPersonLicenseType(SysMessage.DefaultSelecting);
            BindToDDL(ddBusinessType, ls.DataResponse);
        }

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

        private DTO.ResponseMessage<bool> CheckCurrentPersoanlLicenseByIDCard()
        {
            LicenseBiz biz = new LicenseBiz();
            DTO.ResponseMessage<bool> res = new ResponseMessage<bool>();
            DTO.PersonLicenseTransaction licensechk = new PersonLicenseTransaction();

            try
            {
                DTO.ResponseService<DTO.PersonLicenseTransaction[]> resLicense = biz.GetAllLicenseByIDCard(base.UserProfile.IdCard, "1", 3);
                if (resLicense.DataResponse.Count() > 0)
                {
                    licensechk = resLicense.DataResponse.Where(li => li.LICENSE_TYPE_CODE.Equals(this.ddBusinessType.SelectedItem.Value)).FirstOrDefault();
                    if (licensechk == null)
                    {
                        res.ResultMessage = true;
                    }
                    else
                    {
                        res.ErrorMsg = "ไม่สามารถทำรายการได้เนื่องจากมีใบอนุญาตประเภท " + licensechk.LICENSE_TYPE_NAME + " อยู่ในระบบแล้ว";
                        res.ResultMessage = false;
                    }
                }
                else
                {
                    res.ResultMessage = true;
                }
            }
            catch (Exception ex)
            {
                res.ResultMessage = false;
                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
            }

            return res;
        }

        #endregion

        #region UI Function
        protected void btnNext_Click(object sender, ImageClickEventArgs e)
        {
            DTO.ResponseMessage<bool> resCheck = new ResponseMessage<bool>();

            if (ddBusinessType.SelectedValue.Equals(""))
            {
                this.MasterLicense.UCLicenseUCLicenseModelError.ShowMessageError = SysMessage.UserRequire;
                this.MasterLicense.UCLicenseUCLicenseModelError.ShowModalError();
                this.MasterLicense.UpdatePanelLicense.Update();
                return;
            }
            else
            {
                resCheck = this.CheckCurrentPersoanlLicenseByIDCard();

                if (resCheck.ResultMessage == true)
                {
                    MasterLicense.Step += 1;
                    Response.Redirect("../License/LicenseAttatchFiles.aspx?M=" + MasterLicense.Menu + "&S=" + MasterLicense.Step + "");
                }
                else
                {
                    this.MasterLicense.UCLicenseUCLicenseModelError.ShowMessageError = resCheck.ErrorMsg; ;
                    this.MasterLicense.UCLicenseUCLicenseModelError.ShowModalError();
                    return;
                }

                
            }

        }

        protected void ddBusinessType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();

            //Set LicenseTypeCode Session
            this.MasterLicense.LicenseTypeCode = ddBusinessType.SelectedValue;

            //Get Company by License Type
            var message = SysMessage.DefaultSelecting;
            DTO.ResponseService<DTO.DataItem[]> ls = biz.GetCompanyByLicenseType(message, ddBusinessType.SelectedValue);
            BindToDDL(ddCompany, ls.DataResponse);

        }
        #endregion
    }
}