using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.Utils;
using System.IO;
using IAS.DTO;

using System.Text;
using System.Web.Configuration;
using AjaxControlToolkit;
using IAS.BLL;
using IAS.BLL.AttachFilesIAS;
using IAS.BLL.AttachFilesIAS.States;
using IAS.MasterPage;
using IAS.Properties;

namespace IAS.Person
{
    public partial class PersonCompany : basepage
    {
        #region Public Param & Session
        ////ยังไม่ทำการอนุมัติ(แก้ไข)
        private string waitApprove = Resources.propMasterPerson_001;
        ////ไม่อนุมัติ(แก้ไข)
        private string notApprove = Resources.propPersonAssociate_notApprove;
        private string mType = Resources.propReg_Co_MemberTypeCompany;
        public string reqReg = Resources.propRegisCompany_reqRegCompany;
        string mapPath;

        public MasterPerson MasterPage
        {
            get { return (this.Page.Master as MasterPerson); }
        }

        public string Status
        {
            get
            {
                return (Session["Status"] == null) ? "" : Session["Status"].ToString();
            }
            set
            {
                Session["Status"] = value;
            }
        }
      

        #endregion

        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {

            txtEmail.Attributes.Add("onblur", "javascript:return checkemail(" + txtEmail.ClientID + ");");
            txtIDCard.Attributes.Add("onblur", "javascript:return checkUser();");

            txtIDCard.Attributes.Add("onkeydown", "javascript:return checkKeyNumberFixPoint(event, this, 13);");
            txtIDCard.Attributes.Add("onkeypress", "javascript:return checkKeyNumberKeyPressFixPoint(event, this, 13);");

            txtTel.Attributes.Add("onkeydown", "javascript:return checkKeyNumberFixPoint(event, this, 15);");
            txtTel.Attributes.Add("onkeypress", "javascript:return checkKeyNumberKeyPressFixPoint(event, this, 15);");

            UcAddress.TextBoxPostCode.Attributes.Add("onkeydown", "javascript:return checkKeyNumberFixPoint(event, this, 5);");
            UcAddress.TextBoxPostCode.Attributes.Add("onkeypress", "javascript:return checkKeyNumberKeyPressFixPoint(event, this, 5);");

            txtCompanyTel.Attributes.Add("onkeydown", "javascript:return checkKeyNumberFixPoint(event, this, 15);");
            txtCompanyTel.Attributes.Add("onkeypress", "javascript:return checkKeyNumberKeyPressFixPoint(event, this, 15);");

            if (!IsPostBack)
            {
                base.HasPermit();
                //Set Group Validation
                this.MasterPage.SetValidateGroup(this.reqReg);
                Init();
            }
        }
        #endregion

        #region Main Function
        private void Init()
        {

            MasterPage.GetTitle(ddlTitle);
            MasterPage.Init();
            this.MasterPage.SetValidateGroup(this.reqReg);
            this.UcAddress.SetValidateGroup(this.reqReg);

            txtMemberType.Text = this.mType;
            txtMemberType.Enabled = false;

            switch (MasterPage.ActionMode)
            {

                case IAS.DTO.DataActionMode.Edit:
                    lblRegister.Text = Resources.propPersonCompany_001;
                    InitOnEditMode();
                    break;
                case IAS.DTO.DataActionMode.View:
                    lblRegister.Text = Resources.propPersonCompany_002;
                    InitOnViewMode();
                    break;
                default:
                    InitOnViewMode();
                    break;
            }
        }


        private void InitOnEditMode()
        {
            //AG_IAS_TEMP_PERSONAL_T
            //GetRegistration();
            GetTempPerson();
            EnabledControl(true);
            StatusInit();
        }

        private void InitOnViewMode()
        {
            GetLastPerson();
            EnabledControl(false);
            StatusInit();
        }

        //View  Mode
        private void GetLastPerson()
        {

            DTO.Person res = MasterPage.GetPersonal();
            if (res != null)
                GetLoadDataToControl(res);
        }

        //Edit Mode
        private void GetTempPerson()
        {

            DTO.PersonTemp res = MasterPage.GetPersonTemp();
            if (res != null)
            {
                GetLoadTempDataToControl(res);
            }
        }

        private void StatusInit()
        {
            PersonBiz biz = new PersonBiz();

            if (this.MasterPage.UserProfile.Id != null)
            {
                DTO.ResponseService<DTO.Person> res = biz.GetById(this.MasterPage.UserProfile.Id);

                if (res.DataResponse != null)
                {
                    if (res.DataResponse.STATUS == Convert.ToString((int)DTO.PersonDataStatus.WaitForApprove))
                    {
                        lblMessage.Text = this.waitApprove;
                        lblMessage.Visible = true;
                    }
                    else if (res.DataResponse.STATUS == Convert.ToString((int)DTO.PersonDataStatus.NotApprove))
                    {
                        lblMessage.Text = this.notApprove;
                        lblMessage.Visible = true;

                    }
                }


            }

        }

        public void EditPersonal()
        {
            MasterPage.EditCompany(DTO.MemberType.Insurance, GetInsuranceFromDataControl());

            //Added new by Nattapong 11/10/2556
            //InitOnEditMode();
        }

        private DTO.PersonTemp GetInsuranceFromDataControl()
        {
            DTO.PersonTemp company = new DTO.PersonTemp();
            string strFullCompanyName = txtCompany.Text;
            string[] strFullCompanyNameArray = strFullCompanyName.Split('[', ']');
            DTO.DataItem dc = new BLL.DataCenterBiz().GetCompanyCodeById(strFullCompanyNameArray[1].ToString());
            if (dc == null)
            {
                MasterPage.ModelError.ShowMessageError = SysMessage.PleaseInputCompCode;
                MasterPage.ModelError.ShowModalError();
                return null;
            }

            company.ID = MasterPage.PersonId;
            company.MEMBER_TYPE = ((int)DTO.MemberType.Insurance).ToString();
            company.COMP_CODE = dc.Id;
            company.ID_CARD_NO = txtIDCard.Text;
            company.PRE_NAME_CODE = ddlTitle.SelectedValue;
            company.NAMES = txtFirstName.Text;
            company.LASTNAME = txtLastName.Text;
            company.SEX = rblSex.SelectedValue;
            company.EMAIL = txtEmail.Text;
            company.TELEPHONE = txtTel.Text + ((String.IsNullOrWhiteSpace(txtTelExt.Text)) ? "" : ("#" + txtTelExt.Text.Trim()));
            company.LOCAL_TELEPHONE = txtCompanyTel.Text + ((String.IsNullOrWhiteSpace(txtCompanyTelExt.Text)) ? "" : ("#" + txtCompanyTelExt.Text.Trim()));
            company.ADDRESS_1 = UcAddress.Address;
            company.PROVINCE_CODE = UcAddress.ProvinceValue;
            company.AREA_CODE = UcAddress.DistrictValue;
            company.TUMBON_CODE = UcAddress.ParishValue;
            company.ZIP_CODE = UcAddress.TextBoxPostCode.Text;
            company.CREATED_BY = (MasterPage.UserProfile != null) ? MasterPage.UserProfile.LoginName : "AGDOI";
            //company.CREATED_DATE = DateTime.Now;
            company.CREATED_DATE = MasterPage.CreateDateTemp;
            company.UPDATED_BY = (MasterPage.UserProfile != null) ? MasterPage.UserProfile.LoginName : "AGDOI";
            company.UPDATED_DATE = DateTime.Now;
            company.STATUS = ((int)DTO.PersonDataStatus.WaitForApprove).ToString();
            return company;
        }

        //View Mode
        private void GetLoadDataToControl(DTO.Person company)
        {
            MasterPage.PersonId = company.ID;
            if (!company.MEMBER_TYPE.Equals(DTO.MemberType.Insurance.GetEnumValue().ToString()))
            {
                MasterPage.ModelError.ShowMessageError = SysMessage.UserMissMatchRegitrationData;
                MasterPage.ModelError.ShowModalError();
            }

            txtCompanyRegister.Text = company.COMP_CODE;
            ddlTitle.SelectedValue = company.PRE_NAME_CODE;
            txtFirstName.Text = company.NAMES;
            txtLastName.Text = company.LASTNAME;
            txtIDCard.Text = company.ID_CARD_NO;
            txtIDCard.Enabled = false;
            rblSex.SelectedValue = company.SEX;
            rblSex.Enabled = false;
            txtCompanyTel.Text = LocalTelephoneNumberHelper.GetPhoneNumber(company.LOCAL_TELEPHONE);
            txtCompanyTelExt.Text = LocalTelephoneNumberHelper.GetExtenNumber(company.LOCAL_TELEPHONE);
            txtTel.Text = LocalTelephoneNumberHelper.GetPhoneNumber(company.TELEPHONE);
            txtTelExt.Text = LocalTelephoneNumberHelper.GetExtenNumber(company.TELEPHONE);

            txtEmail.Text = company.EMAIL;
            txtEmail.Enabled = false;
            UcAddress.TextBoxAddress.Text = company.ADDRESS_1;
            UcAddress.SelectDropDownStep(company.PROVINCE_CODE, company.AREA_CODE, company.TUMBON_CODE);
            UcAddress.TextBoxPostCode.Text = company.ZIP_CODE;

            txtCompany.Text = new BLL.DataCenterBiz().GetCompanyNameById(company.COMP_CODE);
            UcAddress.DropdownParish.SelectedValue = company.TUMBON_CODE;
            
            if (company.STATUS != null)
            {
                Session["Status"] = company.STATUS.ToString(); //Set status after approve
            }
            if (company.STATUS != Convert.ToString((int)DTO.PersonDataStatus.WaitForApprove))
            {
                MasterPage.ResultRegister = company.APPROVE_RESULT;
            }
        }

        //Edit Mode
        private void GetLoadTempDataToControl(DTO.PersonTemp company)
        {
            MasterPage.PersonId = company.ID;
            if (!company.MEMBER_TYPE.Equals(DTO.MemberType.Insurance.GetEnumValue().ToString()))
            {
                MasterPage.ModelError.ShowMessageError = SysMessage.UserMissMatchRegitrationData;
                MasterPage.ModelError.ShowModalError();
            }

            txtCompanyRegister.Text = company.COMP_CODE;
            ddlTitle.SelectedValue = company.PRE_NAME_CODE;
            txtFirstName.Text = company.NAMES;
            txtLastName.Text = company.LASTNAME;
            txtIDCard.Text = company.ID_CARD_NO;
            txtIDCard.Enabled = false;
            rblSex.SelectedValue = company.SEX;
            txtCompanyTel.Text = LocalTelephoneNumberHelper.GetPhoneNumber(company.LOCAL_TELEPHONE);
            txtCompanyTelExt.Text = LocalTelephoneNumberHelper.GetExtenNumber(company.LOCAL_TELEPHONE);
            txtTel.Text = LocalTelephoneNumberHelper.GetPhoneNumber(company.TELEPHONE);
            txtTelExt.Text = LocalTelephoneNumberHelper.GetExtenNumber(company.TELEPHONE);

            txtEmail.Text = company.EMAIL;
            txtEmail.Enabled = false;
            UcAddress.TextBoxAddress.Text = company.ADDRESS_1;
            UcAddress.SelectDropDownStep(company.PROVINCE_CODE, company.AREA_CODE, company.TUMBON_CODE);
            UcAddress.TextBoxPostCode.Text = company.ZIP_CODE;

            txtCompany.Text = new BLL.DataCenterBiz().GetCompanyNameById(company.COMP_CODE);
            UcAddress.DropdownParish.SelectedValue = company.TUMBON_CODE;

            if (company.STATUS != null)
            {
                Session["Status"] = company.STATUS.ToString(); //Set status after approve
            }
        }

        private void GetCompany()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var list = biz.GetCompanyCodeByName("");
            string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(list);
            hdf.Value = jsonString;
        }

        public void ClearControl()
        {
            ddlTitle.SelectedIndex = 0;
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtIDCard.Text = "";
            rblSex.SelectedIndex = 0;
            txtTel.Text = string.Empty;
            txtCompanyTel.Text = string.Empty;
            txtEmail.Text = string.Empty;
            UcAddress.TextBoxAddress.Text = string.Empty;
            UcAddress.DropdownProvince.SelectedIndex = 0;
            UcAddress.DropdownDistrict.SelectedIndex = 0;
            UcAddress.DropdownParish.SelectedIndex = 0;
            UcAddress.TextBoxPostCode.Text = string.Empty;


        }

        private void EnabledControl(Boolean IsEnable)
        {
            txtCompany.Enabled = false; //Not Allow Edit
            txtCompanyRegister.Enabled = false; //Not Allow Edit

            ddlTitle.Enabled = IsEnable;
            txtFirstName.Enabled = IsEnable;
            txtLastName.Enabled = IsEnable;
            txtIDCard.Enabled = false; //Not Allow Edit
            rblSex.Enabled = IsEnable;
            txtTel.Enabled = IsEnable;
            txtCompanyTel.Enabled = IsEnable;
            txtEmail.Enabled = false; //Not Allow Edit
            UcAddress.TextBoxAddress.Enabled = IsEnable;
            UcAddress.DropdownProvince.Enabled = IsEnable;
            UcAddress.DropdownDistrict.Enabled = IsEnable;
            UcAddress.DropdownParish.Enabled = IsEnable;
            UcAddress.TextBoxPostCode.Enabled = IsEnable;

            //Get Status after edit data for disable control
            string Mode = Request.QueryString["Mode"];
            if (Mode.Equals("E") && this.Status.Equals(DTO.PersonDataStatus.WaitForApprove.GetEnumValue().ToString()))
            {
                MasterPage.AttachFileControl.PnlAttachFiles.Enabled = false;
                MasterPage.AttachFileControl.EnableUpload(false);
                MasterPage.PNLAddButton.Visible = true;
                MasterPage.PNLAddButton.Enabled = false;
            }
            else if(Mode.Equals("E"))
            {
                
                MasterPage.AttachFileControl.PnlAttachFiles.Enabled = true;
                MasterPage.AttachFileControl.EnableUpload(true);
                MasterPage.PNLAddButton.Visible = true;
                MasterPage.PNLAddButton.Enabled = true;
            }
            else
            {
                
                MasterPage.AttachFileControl.PnlAttachFiles.Enabled = false;
                MasterPage.AttachFileControl.EnableUpload(false);
            }

        }
        #endregion

        #region UI Function
        /// <summary>
        /// Assign Control Event from MasterPerson
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            (this.MasterPage as MasterPerson).OkRegister_Click += new EventHandler(PersonCompany_OkRegister_Click);
            (this.MasterPage as MasterPerson).CancelRegister_Click += new EventHandler(PersonCompany_CancelRegister_Click);

        }

        void PersonCompany_OkRegister_Click(object sender, EventArgs e)
        {
            switch (MasterPage.ActionMode)
            {

                case IAS.DTO.DataActionMode.Edit:
                    EditPersonal();
                    break;
                case IAS.DTO.DataActionMode.View:
                    break;
                default:
                    break;
            }
        }

        void PersonCompany_CancelRegister_Click(object sender, EventArgs e)
        {
            //Call MasterPage.CancelRegister_Click
        }

        protected void hdf_ValueChanged(object sender, EventArgs e)
        {
            string selectedWidgetID = ((HiddenField)sender).Value;
            string[] compCode = selectedWidgetID.Split('[', ']');

            txtCompanyRegister.Text = compCode[1];

        }

        protected void ddlTitle_SelectedIndexChanged(object sender, EventArgs e)
        {
            string resSex = Utils.GetTitleName.GetSex(ddlTitle.SelectedItem.Text);
            if ((resSex != null) && (resSex != ""))
            {
                if (resSex.Equals("M"))
                {
                    rblSex.SelectedValue = "M";
                }
                if (resSex.Equals("F"))
                {
                    rblSex.SelectedValue = "F";
                }
                rblSex.Enabled = false;
            }
            else
            {
                //Enable for select Sex
                rblSex.SelectedValue = null;
                rblSex.Enabled = true;
            }
        }
  
        #endregion


    }
}
