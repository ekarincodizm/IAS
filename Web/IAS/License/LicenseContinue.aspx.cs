using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.MasterPage;
using IAS.DTO;

using IAS.BLL;
using IAS.Properties;

namespace IAS.License
{
    public partial class LicenseContinue : basepage
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
            }

        }
        #endregion

        #region Main Public & Private Function
        private void MenuInit()
        {
            GetLicenseDetail();
            
        }

        private void GetLicenseDetail()
        {
            //List<DTO.PersonLicenseTransaction> list = new List<DTO.PersonLicenseTransaction>();
            //BLL.LicenseBiz biz = new BLL.LicenseBiz();

            //DTO.ResponseService<DTO.PersonLicenseTransaction[]> res = biz.GetLicenseTransaction(this.MasterLicense.PersonLicenseH.ToArray(), this.MasterLicense.PersonLicenseD.ToArray());

            DTO.ResponseService<DTO.PersonLicenseTransaction[]> res = this.MasterLicense.GenLicenseTransaction();
            if (res.DataResponse != null)
            {
                //this.ucPerLicense.GridLicense.DataSource = res.DataResponse;
                //this.ucPerLicense.GridLicense.DataBind();

                res.DataResponse.ToList().ForEach(lic =>
                {
                    if (lic.FEES == 0)
                    {
                        lic.PETITION_TYPE_CODE = "ใบแทนใบอนุญาต(เปลี่ยนชื่อ-สกุล)";
                    }
                });

                this.ucPerLicense.GridLicense.DataSource = res.DataResponse;
                this.ucPerLicense.GridLicense.DataBind();
            }
            else
            {
                this.MasterLicense.UCLicenseUCLicenseModelError.ShowMessageError = res.IsError.ToString();
                this.MasterLicense.UCLicenseUCLicenseModelError.ShowModalError();
                this.MasterLicense.UpdatePanelLicense.Update();
                return;

            }
            
        }

        #endregion

        #region UI Function

        protected void btnNext_Click(object sender, ImageClickEventArgs e)
        {
            LicenseBiz biz = new LicenseBiz();
            DTO.ResponseMessage<bool> resInsert = new ResponseMessage<bool>();

            if (rdPayment.Checked == true)
            {
                resInsert = this.MasterLicense.InsertLicense();
                if (resInsert.ResultMessage == true)
                {
                    this.MasterLicense.Step += 1;
                    Response.Redirect("../License/LicensePayment.aspx?M=" + MasterLicense.Menu + "&S=" + MasterLicense.Step + "");

                }
                else if ((resInsert.ResultMessage == false) || (resInsert.IsError))
                {
                    this.MasterLicense.UCLicenseUCLicenseModelError.ShowMessageError = resInsert.ErrorMsg;
                    this.MasterLicense.UCLicenseUCLicenseModelError.ShowModalError();
                    this.MasterLicense.UpdatePanelLicense.Update();
                    return;
                }
            }
            else if (rdLicenseList.SelectedValue.Equals(Convert.ToString((int)DTO.PersonLicenses.New)))
            {
                this.MasterLicense.Menu = 1;
                this.MasterLicense.Step = 1;
                Session["CheckClearSession"] = "P";
                this.MasterLicense.Redo = "1";

                Response.Redirect("../License/NewLicense.aspx?M=" + MasterLicense.Menu + "&S=" + MasterLicense.Step + "");
               
            }
            else if (rdLicenseList.SelectedValue.Equals(Convert.ToString((int)DTO.PersonLicenses.ReNew)))
            {
                this.MasterLicense.Menu = 2;
                this.MasterLicense.Step = 1;
                Session["CheckClearSession"] = "P";
                this.MasterLicense.Redo = "1";
                Response.Redirect("../License/RenewLicense.aspx?M=" + MasterLicense.Menu + "&S=" + MasterLicense.Step + "");
               
            }
            else if (rdLicenseList.SelectedValue.Equals(Convert.ToString((int)DTO.PersonLicenses.ExpireReNew)))
            {
                this.MasterLicense.Menu = 3;
                this.MasterLicense.Step = 1;
                Session["CheckClearSession"] = "P";
                this.MasterLicense.Redo = "1";
                Response.Redirect("../License/ExpiredRenewLicense.aspx?M=" + MasterLicense.Menu + "&S=" + MasterLicense.Step + "");
                
            }
            else if (rdLicenseList.SelectedValue.Equals(Convert.ToString((int)DTO.PersonLicenses.Other)))
            {
                this.MasterLicense.Menu = 4;
                this.MasterLicense.Step = 1;
                Session["CheckClearSession"] = "P";
                this.MasterLicense.Redo = "1";
                Response.Redirect("../License/LicenseReplace.aspx?M=" + MasterLicense.Menu + "&S=" + MasterLicense.Step + "");

            }
            else
            {
                this.MasterLicense.UCLicenseUCLicenseModelError.ShowMessageError = Resources.errorLicenseContinue_001;
                this.MasterLicense.UCLicenseUCLicenseModelError.ShowModalError();
                this.MasterLicense.UpdatePanelLicense.Update();
                return;
            }
        }

        protected void btnBack_Click(object sender, ImageClickEventArgs e)
        {
            this.MasterLicense.Step -= 1;
            Response.Redirect("../License/LicenseAgreement.aspx?M=" + MasterLicense.Menu + "&S=" + MasterLicense.Step + "");
        }

        protected void rdPayment_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdPayment.Checked == true)
            {
                //Clear Selection
                rdLicenseList.SelectedValue = null;

            }
        }

        protected void rdLicenseList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Clear Selection
            this.rdPayment.Checked = false;

            if (rdLicenseList.SelectedValue.Equals(Convert.ToString((int)DTO.PersonLicenses.New)))
            {

            }
            else if (rdLicenseList.SelectedValue.Equals(Convert.ToString((int)DTO.PersonLicenses.ReNew)))
            {

            }
            else if (rdLicenseList.SelectedValue.Equals(Convert.ToString((int)DTO.PersonLicenses.ExpireReNew)))
            {

            }
            else if (rdLicenseList.SelectedValue.Equals(Convert.ToString((int)DTO.PersonLicenses.Other)))
            {
                //ddlOther.Enabled = true;
            }
        }

        #endregion




    }
}