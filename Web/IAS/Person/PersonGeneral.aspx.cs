using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.MasterPage;

using IAS.BLL.AttachFilesIAS;
using IAS.BLL.AttachFilesIAS.States;
using AjaxControlToolkit;
using System.Text;
using IAS.Utils;
using System.IO;
using IAS.DTO;
using IAS.BLL;
using IAS.Properties;

namespace IAS.Person
{
    public partial class PersonGeneral : basepage
    {
        #region Public Param & Session
        ////ยังไม่ทำการอนุมัติ(แก้ไข)
        private string waitApprove = Resources.propMasterPerson_001;
        ////ไม่อนุมัติ(แก้ไข)
        private string notApprove = Resources.propPersonAssociate_notApprove;
        private string mType = Resources.propReg_NotApprove_MemberTypeGeneral;
        public string reqReg = Resources.propRegisGeneral_regGuest;
        public TextBox TextboxBirthDay { get { return txtBirthDay; } }

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
            txtBirthDay.Attributes.Add("readonly", "true");
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "Call my function", "setdate()", true);
            //ScriptManager.RegisterStartupScript(this, GetType(), "textreadonly", "setdate();", true);
            txtIDNumber.Attributes.Add("onblur", "javascript:return checkUser(" + txtIDNumber.ClientID + ");");

            txtEmail.Attributes.Add("onblur", "javascript:return checkemail(" + txtEmail.ClientID + ");");

            //txtIDNumber.Attributes.Add("onkeydown", "javascript:return checkKeyNumberFixPoint(event, this, 13);");
            //txtIDNumber.Attributes.Add("onkeypress", "javascript:return checkKeyNumberKeyPressFixPoint(event, this, 13);");

            txtTel.Attributes.Add("onkeydown", "javascript:return checkKeyNumberFixPoint(event, this, 15);");
            txtTel.Attributes.Add("onkeypress", "javascript:return checkKeyNumberKeyPressFixPoint(event, this, 15);");


            txtMobilePhone.Attributes.Add("onkeydown", "javascript:return checkKeyNumberFixPoint(event, this, 15);");
            txtMobilePhone.Attributes.Add("onkeypress", "javascript:return checkKeyNumberKeyPressFixPoint(event, this, 15);");

            if (!(ViewState["ActiveTabIndex"] == null))
            {
                TabAddress.ActiveTabIndex = (int)ViewState["ActiveTabIndex"];

            }

            if (!Page.IsPostBack)
            {
                base.HasPermit();
                //Set Group Validation
                this.MasterPage.SetValidateGroup(this.reqReg);
                Init();

            }
        }
        #endregion

        #region Main Public && Private Function

        private void Init()
        {
            StringBuilder newName = new StringBuilder();
            MasterPage.GetEducation(ddlEducation);
            MasterPage.GetNationality(ddlNationality);
            MasterPage.GetTitle(ddlTitle);
            MasterPage.Init();

            newName.Append(this.mType);
            //newName.Append("/ตัวแทน/นายหน้า");
            txtMemberType.Text = newName.ToString();
            //txtMemberType.Text = this.mType;
            txtMemberType.Enabled = false;

            switch (MasterPage.ActionMode)
            {

                case IAS.DTO.DataActionMode.Edit:
                    InitOnEditMode();
                    lblhead.Text = Resources.propPersonGeneral_001;
                    break;
                case IAS.DTO.DataActionMode.View:
                    InitOnViewMode();
                    lblhead.Text = Resources.propPersonGeneral_002;
                    break;
                default: InitOnViewMode();
                    break;
            }
        }


        private void InitOnEditMode()
        {
            //MasterPage.ddlAgentTypeInit(this.ddlAgentType);
            //AG_IAS_TEMP_PERSONAL_T
            GetPersonTemp();
            //GetLastPerson();
            EnableControl(true);
            StatusInit();
            
        }

        private void InitOnViewMode()
        {
            //MasterPage.ddlAgentTypeInit(this.ddlAgentType);
            GetLastPerson();
            EnableControl(false);
            StatusInit();
            
        }

        //View Mode
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

        //View Mode
        private void GetLastPerson()
        {
            BLL.PersonBiz personBiz = new BLL.PersonBiz();
            var res = personBiz.GetById(MasterPage.UserProfile.Id);
            if (res.IsError)
            {
                MasterPage.ModelError.ShowMessageError = res.ErrorMsg;
                MasterPage.ModelError.ShowModalError();
            }
            MasterPage.PersonId = res.DataResponse.ID;
            GetLoadDataToControl(res.DataResponse);
            MasterPage.GetAttatchFiles();
        }

        //Edit Mode
        private void GetPersonTemp()
        {

            BLL.PersonBiz personBiz = new BLL.PersonBiz();
            var res = personBiz.GetPersonTemp(MasterPage.UserProfile.Id);
            if (res.IsError)
            {
                MasterPage.ModelError.ShowMessageError = res.ErrorMsg;
                MasterPage.ModelError.ShowModalError();
            }
            MasterPage.PersonId = res.DataResponse.ID;
            GetLoadTempDataToControl(res.DataResponse);
            MasterPage.GetAttatchFiles();

            //Assign CreateDate
            MasterPage.CreateDateTemp = res.DataResponse.CREATED_DATE;

        }

        //View Mode
        private void GetLoadDataToControl(DTO.Person general) 
        {

            //var attachFiles = this.AttachFiles;
            //var personAttachFiles = this.PersonAttachFiles;
            MasterPage.PersonId = general.ID;
            if (general.MEMBER_TYPE != ((int)DTO.MemberType.General).ToString())
            {
                MasterPage.ModelError.ShowMessageError = SysMessage.UserMissMatchRegitrationData;
                MasterPage.ModelError.ShowModalError();
            }

            txtIDNumber.Text = general.ID_CARD_NO;
            ddlTitle.SelectedValue = general.PRE_NAME_CODE;
            txtFirstName.Text = general.NAMES;
            txtLastName.Text = general.LASTNAME;
            txtIDNumber.Text = general.ID_CARD_NO;
            txtBirthDay.Text = (general.BIRTH_DATE == null) ? "" : ((DateTime)general.BIRTH_DATE).ToString("dd/MM/yyyy");
            rblSex.SelectedValue = general.SEX;
            rblSex.Enabled = false;
            ddlNationality.SelectedValue = general.NATIONALITY;
            ddlEducation.SelectedValue = general.EDUCATION_CODE;
            txtEmail.Text = general.EMAIL;

            txtTel.Text = LocalTelephoneNumberHelper.GetPhoneNumber(general.LOCAL_TELEPHONE);
            txtTelExt.Text = LocalTelephoneNumberHelper.GetExtenNumber(general.LOCAL_TELEPHONE);
            txtMobilePhone.Text = general.TELEPHONE;

            ucAddressCurrent.Address = general.ADDRESS_1; // txtCurrentAddress.Text;
            ucAddressCurrent.SelectDropDownStep(general.PROVINCE_CODE, general.AREA_CODE, general.TUMBON_CODE);
            ucAddressCurrent.PostCode = general.ZIP_CODE; // txtPostcodeCurrentAddress.Text;

            ucAddressRegister.Address = general.LOCAL_ADDRESS1; // txtRegisterAddress.Text;
            ucAddressRegister.SelectDropDownStep(general.LOCAL_PROVINCE_CODE, general.LOCAL_AREA_CODE, general.LOCAL_TUMBON_CODE);
            ucAddressRegister.PostCode = general.LOCAL_ZIPCODE; // txtPostcodeRegisterAddress.Text;

            if (general.STATUS != null)
            {
                Session["Status"] = general.STATUS.ToString(); //Set status after approve
            }
            if (general.STATUS != Convert.ToString((int)DTO.PersonDataStatus.WaitForApprove))
            {
                MasterPage.ResultRegister = general.APPROVE_RESULT;
            }

            //if ((general.AGENT_TYPE != "") && (general.AGENT_TYPE != null))
            //{
            //    ddlAgentType.SelectedValue = general.AGENT_TYPE;
            //}
            //else if ((general.AGENT_TYPE == "") || (general.AGENT_TYPE == null))
            //{
            //    ddlAgentType.Items.Clear();
            //    ddlAgentType.DataSource = null;
            //    ddlAgentType.DataBind();
            //}
            //general.CREATED_BY = "agdoi";
            //general.CREATED_DATE = DateTime.Now;
            //general.UPDATED_BY = "agdoi";
            //general.UPDATED_DATE = DateTime.Now;
            //general.REG_PASS = MasterPage.Password;

        }

        //Edit Mode
        private void GetLoadTempDataToControl(DTO.PersonTemp general)
        {

            //var attachFiles = this.AttachFiles;
            //var personAttachFiles = this.PersonAttachFiles;
            MasterPage.PersonId = general.ID;
            if (general.MEMBER_TYPE != ((int)DTO.MemberType.General).ToString())
            {
                MasterPage.ModelError.ShowMessageError = SysMessage.UserMissMatchRegitrationData;
                MasterPage.ModelError.ShowModalError();
            }

            txtIDNumber.Text = general.ID_CARD_NO;
            ddlTitle.SelectedValue = general.PRE_NAME_CODE;
            txtFirstName.Text = general.NAMES;
            txtLastName.Text = general.LASTNAME;
            txtIDNumber.Text = general.ID_CARD_NO;
            txtBirthDay.Text = (general.BIRTH_DATE == null) ? "" : ((DateTime)general.BIRTH_DATE).ToString("dd/MM/yyyy");
            rblSex.SelectedValue = general.SEX;
            ddlNationality.SelectedValue = general.NATIONALITY;
            ddlEducation.SelectedValue = general.EDUCATION_CODE;
            txtEmail.Text = general.EMAIL;

            txtTel.Text = LocalTelephoneNumberHelper.GetPhoneNumber(general.LOCAL_TELEPHONE);
            txtTelExt.Text = LocalTelephoneNumberHelper.GetExtenNumber(general.LOCAL_TELEPHONE);
            txtMobilePhone.Text = general.TELEPHONE;

            ucAddressCurrent.Address = general.ADDRESS_1; // txtCurrentAddress.Text;
            ucAddressCurrent.SelectDropDownStep(general.PROVINCE_CODE, general.AREA_CODE, general.TUMBON_CODE);
            ucAddressCurrent.PostCode = general.ZIP_CODE; // txtPostcodeCurrentAddress.Text;

            ucAddressRegister.Address = general.LOCAL_ADDRESS1; // txtRegisterAddress.Text;
            ucAddressRegister.SelectDropDownStep(general.LOCAL_PROVINCE_CODE, general.LOCAL_AREA_CODE, general.LOCAL_TUMBON_CODE);
            ucAddressRegister.PostCode = general.LOCAL_ZIPCODE; // txtPostcodeRegisterAddress.Text;

            if (general.STATUS != null)
            {
                Session["Status"] = general.STATUS.ToString(); //Set status after approve
            }
            MasterPage.ResultRegister = general.APPROVE_RESULT;

            //if ((general.AGENT_TYPE != "") && (general.AGENT_TYPE != null))
            //{
            //    ddlAgentType.SelectedValue = general.AGENT_TYPE;
            //}
            //else if ((general.AGENT_TYPE == "") || (general.AGENT_TYPE == null))
            //{
            //    ListItem ddlList = new ListItem("กรุณาเลือกประเภทตัวแทนสมาชิก", "", true);
            //    ddlList.Selected = true;
            //    ddlAgentType.Items.Add(ddlList);

            //}
            //general.CREATED_BY = "agdoi";
            //general.CREATED_DATE = DateTime.Now;
            //general.UPDATED_BY = "agdoi";
            //general.UPDATED_DATE = DateTime.Now;
            //general.REG_PASS = MasterPage.Password;

        }

        /// <summary>
        /// <LASTUPDATE>07/05/2557</LASTUPDATE>
        /// <AUTHOR>Natta</AUTHOR>
        /// </summary>
        private void CloneAddress()
        {
            if ((ucAddressCurrent.DropdownProvince.SelectedValue == "") || (ucAddressCurrent.DropdownDistrict.SelectedValue == "") || (ucAddressCurrent.DropdownParish.SelectedValue == ""))
            {
                chkCopyAdd.Checked = false;
                TabAddress.ActiveTabIndex = 0;
                this.MasterPage.ModelError.ShowMessageError = "กรุณาระบุที่อยู่ปัจจุบันให้ครบ";
                this.MasterPage.ModelError.ShowModalError();
                return;
            }


            ucAddressRegister.SelectDropDownStep(ucAddressCurrent.DropdownProvince.SelectedValue, ucAddressCurrent.DropdownDistrict.SelectedValue, ucAddressCurrent.DropdownParish.SelectedValue);
            ucAddressRegister.Address = ucAddressCurrent.Address;
            ucAddressRegister.PostCode = ucAddressCurrent.PostCode;
        }

        private DTO.PersonTemp GetPersonalFromDataControl()
        {

            DTO.PersonTemp general = new DTO.PersonTemp();

            //var attachFiles = this.AttachFiles;
            //var personAttachFiles = this.PersonAttachFiles;
            general.ID = MasterPage.PersonId;
            general.MEMBER_TYPE = ((int)DTO.MemberType.General).ToString();
            general.ID_CARD_NO = txtIDNumber.Text;
            general.PRE_NAME_CODE = ddlTitle.SelectedValue;
            general.NAMES = txtFirstName.Text;
            general.LASTNAME = txtLastName.Text;
            general.ID_CARD_NO = txtIDNumber.Text;
            general.BIRTH_DATE = Convert.ToDateTime(txtBirthDay.Text);
            general.SEX = rblSex.SelectedValue;
            general.NATIONALITY = ddlNationality.SelectedValue;
            general.EDUCATION_CODE = ddlEducation.SelectedValue;
            general.EMAIL = txtEmail.Text;
            general.LOCAL_TELEPHONE = txtTel.Text + ((String.IsNullOrWhiteSpace(txtTelExt.Text)) ? "" : ("#" + txtTelExt.Text.Trim())); ;
            general.TELEPHONE = txtMobilePhone.Text;
            general.ADDRESS_1 = ucAddressCurrent.Address; // txtCurrentAddress.Text;
            general.PROVINCE_CODE = ucAddressCurrent.ProvinceValue; // ddlProvinceCurrentAddress.SelectedValue;
            general.AREA_CODE = ucAddressCurrent.DistrictValue; // ddlDistrictCurrentAddress.SelectedValue;
            general.TUMBON_CODE = ucAddressCurrent.ParishValue; // ddlParishCurrentAddress.SelectedValue;
            general.ZIP_CODE = ucAddressCurrent.PostCode; // txtPostcodeCurrentAddress.Text;
            general.LOCAL_ADDRESS1 = ucAddressRegister.Address; // txtRegisterAddress.Text;
            general.LOCAL_PROVINCE_CODE = ucAddressRegister.ProvinceValue; // ddlProvinceRegisterAddress.SelectedValue;
            general.LOCAL_AREA_CODE = ucAddressRegister.DistrictValue; // ddlDistrictRegisterAddress.SelectedValue;
            general.LOCAL_TUMBON_CODE = ucAddressRegister.ParishValue;// ddlParishRegisterAddress.SelectedValue;
            general.LOCAL_ZIPCODE = ucAddressRegister.PostCode; // txtPostcodeRegisterAddress.Text;
            general.CREATED_BY = (MasterPage.UserProfile != null) ? MasterPage.UserProfile.LoginName : "AGDOI";
            //general.CREATED_DATE = DateTime.Now;
            general.CREATED_DATE = MasterPage.CreateDateTemp;
            general.UPDATED_BY = (MasterPage.UserProfile != null) ? MasterPage.UserProfile.LoginName : "AGDOI";
            general.UPDATED_DATE = DateTime.Now;
            general.STATUS = ((int)DTO.PersonDataStatus.WaitForApprove).ToString();
            general.APPROVED_BY = null;
            //general.REG_PASS = MasterPage.Password;

            //if ((ddlAgentType.SelectedValue != "") && (ddlAgentType.SelectedValue != null))
            //{
            //    general.AGENT_TYPE = ddlAgentType.SelectedValue;
            //}
            

            return general;
        }

        private void EditPersonal()
        {

            MasterPage.EditPersonal(DTO.MemberType.General, GetPersonalFromDataControl());
            
            //Added new by Nattapong 11/10/2556
            //InitOnEditMode();
        }


        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return true;
            }
            catch
            {
                //AlertMessage.ShowAlertMessage(string.Empty, SysMessage.EmailErrorFormat);

                MasterPage.ModelError.ShowMessageError = SysMessage.EmailErrorFormat;
                MasterPage.ModelError.ShowModalError();

                return false;
            }

        }

        private void EnableControl(Boolean IsEnable)
        {

            foreach (Control item in UpdatePanelUpload.Controls[0].Controls)
            {

                if (item.GetType() == typeof(TextBox))
                    ((TextBox)item).Enabled = IsEnable;
                else if (item.GetType() == typeof(DropDownList))
                    ((DropDownList)item).Enabled = IsEnable;
                else if (item.GetType() == typeof(CheckBox))
                    ((CheckBox)item).Enabled = IsEnable;
                else if (item.GetType() == typeof(RadioButtonList))
                    ((RadioButtonList)item).Enabled = IsEnable;

            }
            chkCopyAdd.Enabled = IsEnable;
            MasterPage.AttachFileControl.EnableGridView(IsEnable);
            MasterPage.AttachFileControl.EnableUpload(IsEnable);
            ucAddressCurrent.Enabled(IsEnable);
            ucAddressRegister.Enabled(IsEnable);
            UpdatePanelUpload.Update();
            imgPopup_txtBirthDay.Visible = IsEnable;

            //Get Status after edit data for disable control
            string Mode = Request.QueryString["Mode"];
            if (Mode.Equals("E") && this.Status.Equals(DTO.PersonDataStatus.WaitForApprove.GetEnumValue().ToString()))
            {
               
                MasterPage.AttachFileControl.PnlAttachFiles.Enabled = false;
                MasterPage.AttachFileControl.EnableUpload(false);
                MasterPage.PNLAddButton.Visible = true;
                MasterPage.PNLAddButton.Enabled = false;
                txtIDNumber.Enabled = false;
            }
            else
            {
               
                txtIDNumber.Enabled = false;
                MasterPage.AttachFileControl.PnlAttachFiles.Enabled = IsEnable;
               
            }
        }

        #endregion

        #region UI Function
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            (this.MasterPage as MasterPerson).OkRegister_Click += new EventHandler(PersonGeneral_OkRegister_Click);
            (this.MasterPage as MasterPerson).CancelRegister_Click += new EventHandler(PersonGeneral_CancelRegister_Click);
        }

        void PersonGeneral_OkRegister_Click(object sender, EventArgs e)
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

        void PersonGeneral_CancelRegister_Click(object sender, EventArgs e)
        {
              
        }

        protected void chkCopyAdd_CheckedChanged(object sender, EventArgs e)
        {
            //ucAddressCurrent.Clone(ucAddressRegister);
            //ucAddressRegister = ucAddressCurrent;
            if (((CheckBox)sender).Checked)
            {
                CloneAddress();
                if (TabAddress.ActiveTabIndex == 0)
                {
                    ucAddressRegister.Enabled(true);
                }
                else
                {
                    ucAddressRegister.Enabled(false);
                }
            }
            else
            {
                ucAddressRegister.Enabled(true);
                //ucAddressRegister.Clear();
            }

            UpdatePanelUpload.Update();

        }

        protected void TabAddress_ActiveTabChanged(object sender, EventArgs e)
        {
            ViewState["ActiveTabIndex"] = TabAddress.ActiveTabIndex;
            if (TabAddress.ActiveTabIndex == 1 && chkCopyAdd.Checked)
            {
                CloneAddress();
                ucAddressRegister.Enabled(false);
            }
            else
            {
                ucAddressRegister.Enabled(true);
            }
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
