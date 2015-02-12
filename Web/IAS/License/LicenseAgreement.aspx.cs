using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.MasterPage;
using IAS.BLL.AttachFilesIAS;
using System.Web.SessionState;

namespace IAS.License
{
    public partial class LicenseAgreement : basepage
    {
        #region Public Param & Session
        public MasterLicense MasterLicense
        {
            get
            {
                return (this.Page.Master as MasterLicense);
            }

        }
        #endregion


        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                base.HasPermit();
                MenuInit();
                AgreementRemember();
            }
        }
        #endregion

        #region Main Public & Private Function
        /// <summary>
        /// แก้ไขล่าสุดวันที่ 27/07/2557 เพิ่ม else case
        /// <Editor>Natta</Editor>
        /// </summary>
        private void MenuInit()
        {
            IList<AttachFile> AttachFile = this.MasterLicense.AttachFiles;
            //DTO.ResponseMessage<bool> res = new DTO.ResponseMessage<bool>();
            //res = this.MasterLicense.InitArgReport();
            DTO.ResponseService<string> res = new DTO.ResponseService<string>();
            res.DataResponse = this.MasterLicense.InitialReport().DataResponse;
            if ((res.DataResponse != null) && (res.DataResponse != "")) 
            {
                if (this.MasterLicense.Menu.Equals((int)DTO.MenuLicenses.Step1))
                {
                    string path = base.AgreementFilePath_Key + this.MasterLicense.ArgOutPutFile[0];

                    this.ucPDFArg.IFramArgree.Attributes.Add("src", "" + res.DataResponse + "");
                    //string pathh = "../License/Agreement_2.aspx?M=" + this.MasterLicense.Menu + "";
                    //iframAgree.Attributes.Add("src", pathh);
                }
                else if (this.MasterLicense.Menu.Equals((int)DTO.MenuLicenses.Step2))
                {
                    string path = base.AgreementFilePath_Key + this.MasterLicense.ArgOutPutFile[1];

                    this.ucPDFArg.IFramArgree.Attributes.Add("src", "" + res.DataResponse + "");
                }
                else  if (this.MasterLicense.Menu.Equals((int)DTO.MenuLicenses.Step3))
                {
                    string path = base.AgreementFilePath_Key + this.MasterLicense.ArgOutPutFile[2];

                    this.ucPDFArg.IFramArgree.Attributes.Add("src", "" + res.DataResponse + "");
                }
                else if (this.MasterLicense.Menu.Equals((int)DTO.MenuLicenses.Step4))
                {
                    string path = base.AgreementFilePath_Key + this.MasterLicense.ArgOutPutFile[0];

                    this.ucPDFArg.IFramArgree.Attributes.Add("src", "" + res.DataResponse + "");
                }
                else
                {
                    string path = base.AgreementFilePath_Key + this.MasterLicense.ArgOutPutFile[0];

                    this.ucPDFArg.IFramArgree.Attributes.Add("src", "" + res.DataResponse + "");
                }
                //string path = base.AgreementFilePath_Key + this.MasterLicense.ArgOutPutFile[0];
                //iframAgree.Attributes.Add("src", path);

                //string pathh = "../License/Agreement_2.aspx?M="+this.MasterLicense.Menu+"";
                //iframAgree.Attributes.Add("src", pathh);

            }
            else if (res.IsError)
            {
                this.MasterLicense.UCLicenseUCLicenseModelError.ShowMessageError = res.ErrorMsg;
                this.MasterLicense.UCLicenseUCLicenseModelError.ShowModalError();
                this.MasterLicense.UpdatePanelLicense.Update();
                return;
            }
        }

        private void AgreementRemember()
        {
            chkAgreement.Checked = this.MasterLicense.SelectAgreement;
        }
        #endregion

        #region UI Function
        protected void btnNext_Click(object sender, ImageClickEventArgs e)
        {
            if (chkAgreement.Checked == true)
            {
                this.MasterLicense.Step += 1;
                Response.Redirect("../License/LicenseValidate.aspx?M=" + MasterLicense.Menu + "&S=" + MasterLicense.Step + "");
                //Response.Redirect("../License/LicenseContinue.aspx?M=" + MasterLicense.Menu + "&S=" + MasterLicense.Step + "");

            }
            else
            {
                string Alert = "alert('" + SysMessage.AgreementRequire + "')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", Alert, true);

            }

        }

        protected void btnBack_Click(object sender, ImageClickEventArgs e)
        {
            this.MasterLicense.Step -= 1;
            Response.Redirect("../License/LicenseAttatchFiles.aspx?M=" + MasterLicense.Menu + "&S=" + MasterLicense.Step + "");
        }

        protected void chkAgreement_CheckedChanged(object sender, EventArgs e)
        {
            this.MasterLicense.SelectAgreement = chkAgreement.Checked;
        }
        #endregion

    }
}