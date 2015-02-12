using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.Utils;
using IAS.Properties;
using System.Data;
namespace IAS
{
    public partial class BankPage : basepage
    {
        //ยังไม่ทำการอนุมัติ(สมัคร)
        private string waitApproveRegit = Resources.propReg_NotApprove_waitApprove;

        private string notApproveRegit = Resources.propReg_NotApprove_notApprove;



        private string waitApproveEdit = Resources.propMasterPerson_001;

        private string notApproveEdit = Resources.propPersonAssociate_notApprove;


        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                DTO.UserProfile userProfile = (DTO.UserProfile)Session[PageList.UserProfile];
                //Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                //dlg.Multiselect = true;
                //dlg.Filter = "Image File|*.bmp;*.gif;*.jpeg;*.jpg;*.png;*.tif;*.tiff";
                //if (userProfile.IS_APPROVE != "Y")
                //{
                //    lblMessage.Text = waitApprove;
                //    lblMessage.Visible = true;
                //}

                init_flows();
                SetRenewLicenseQuickCount();
            }
        }

        private void init_flows()
        {
            DTO.UserProfile userProfile = (DTO.UserProfile)Session[PageList.UserProfile];
            if (userProfile.MemberType == DTO.RegistrationType.General.GetEnumValue())
            {
                TabContainer1.Visible = true;
                Image1General.Visible = true;
                Image2General.Visible = true;
                Image3General.Visible = true;
                Image4General.Visible = true;
                Image5General.Visible = true;
                Image6General.Visible = true;
                Image7General.Visible = true;
                //TabPanel1.Visible = true;
                TabPanel2.Visible = true;
                TabPanel3.Visible = true;
                TabPanel4.Visible = true;
                TabPanel5.Visible = true;
                TabPanel6.Visible = true;
                TabPanel7.Visible = true;
                DashboardAll.Visible = true;
                TabPanel37.Visible = true; // new tab
                TabContainerGeneral.Visible = true;
            }
            else if (userProfile.MemberType == DTO.RegistrationType.Insurance.GetEnumValue())
            {
                TabContainer1.Visible = true;
                Image1Company.Visible = true;
                Image2Company.Visible = true;
                Image3Company.Visible = true;
                Image4Company.Visible = true;
                Image5Company.Visible = true;
                Image6Company.Visible = true;
                Image7Company.Visible = true;
                //TabPanel1.Visible = true;
                TabPanel2.Visible = true;
                TabPanel3.Visible = true;
                TabPanel4.Visible = true;
                TabPanel5.Visible = true;
                TabPanel6.Visible = true;
                TabPanel7.Visible = true;
                DashboardAll.Visible = true;
                TabPanel37.Visible = true; // new tab
                TabContainerGeneral.Visible = true;
            }
            else if (userProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue())
            {
                TabContainer1.Visible = true;
                Image1Association.Visible = true;
                //TabPanel1.Visible = true;
                DashboardAll.Visible = true;
                TabPanel37.Visible = false; // new tab
                TabContainerGeneral.Visible = true;
            }
            else if (userProfile.MemberType == DTO.RegistrationType.OIC.GetEnumValue())
            {
                tabAdmin.Visible = true;
                DashboardAdmin.Visible = true;
                DashboardAll.Visible = false;
            }
            else if (userProfile.MemberType == DTO.RegistrationType.OICAgent.GetEnumValue())
            {
                tabAgent.Visible = true;
                DashboardAll.Visible = true;
            }
            else if (userProfile.MemberType == DTO.RegistrationType.OICFinace.GetEnumValue())
            {
                tabFinance.Visible = true;
                DashboardAll.Visible = true;
            }
            else if(userProfile.MemberType == DTO.RegistrationType.TestCenter.GetEnumValue()){
                tabTestGroup.Visible = true;
                DashboardAll.Visible = true;
            }

            if ((userProfile.MemberType != DTO.RegistrationType.OIC.GetEnumValue()) && (userProfile.MemberType != DTO.RegistrationType.OICAgent.GetEnumValue())
              && (userProfile.MemberType != DTO.RegistrationType.OICFinace.GetEnumValue()) && (userProfile.MemberType != DTO.RegistrationType.TestCenter.GetEnumValue()))
            {
                if (userProfile.STATUS == DTO.RegistrationStatus.WaitForApprove.GetEnumValue().ToString())
                {
                    lblText.Text = this.waitApproveRegit;
                    divTitle.Visible = true;
                }
                else if(userProfile.STATUS == DTO.RegistrationStatus.Approve.GetEnumValue().ToString())
                {
                    divTitle.Visible = false;
                }
                else if(userProfile.STATUS == DTO.RegistrationStatus.NotApprove.GetEnumValue().ToString())
                {
                    lblText.Text = this.notApproveRegit;
                    divTitle.Visible = true;
                }
                else if(userProfile.STATUS==DTO.PersonDataStatus.WaitForApprove.GetEnumValue().ToString())
                {
                    lblText.Text = this.waitApproveEdit;
                    divTitle.Visible = true;
                }
                else if (userProfile.STATUS==DTO.PersonDataStatus.Approve.GetEnumValue().ToString())
	            {
                    divTitle.Visible = false;
	            }
                else if (userProfile.STATUS == DTO.PersonDataStatus.NotApprove.GetEnumValue().ToString())
                {
                    lblText.Text = this.notApproveEdit;
                    divTitle.Visible = true;
                }
            }
        }

        protected void SetRenewLicenseQuickCount()
        {
            try
            {
                BLL.LicenseBiz biz = new BLL.LicenseBiz();
                string Days = "7";
                DataSet ds = new DataSet();
                // 13-ขอต่ออายุใบอนุญาต 1 ปี
                ds = biz.GetRenewLicenseQuick("13", null, null, "", Days).DataResponse;
                lblPetitionCode13.Text = ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows.Count.ToString() : "0";
                // 14-ขอต่ออายุใบอนุญาต 5 ปี
                ds = biz.GetRenewLicenseQuick("14", null, null, "", Days).DataResponse;
                lblPetitionCode14.Text = ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows.Count.ToString() : "0";
                // 16-ใบแทนใบอนุญาต
                ds = biz.GetRenewLicenseQuick("16", null, null, "", Days).DataResponse;
                lblPetitionCode16.Text = ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows.Count.ToString() : "0";

                if ((lblPetitionCode13.Text.ToInt() + lblPetitionCode14.Text.ToInt() + lblPetitionCode16.Text.ToInt() == 0))
                {
                    btnReportQuickReport.Visible = false;
                }
                else
                {
                    btnReportQuickReport.Visible = true;
                }
            }
            catch { }
        }

        protected void btnReportQuickReport_Click(object sender, EventArgs e)
        {
            string param = String.Format("'','','','','7'");
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "", "OpenPopupViewer(" + param + ")", true);
        }
    }
}