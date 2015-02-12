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
using IAS.DTO;
using IAS.Properties;
using IAS.BLL;
using System.Runtime.Serialization;

namespace IAS.Register
{
    public partial class RegisGeneral : System.Web.UI.Page
    {
        #region Public Param & Session
        ////ยังไม่ทำการอนุมัติ(สมัคร)
        private string waitApprove = Resources.propReg_NotApprove_waitApprove;
        ////ไม่อนุมัติ(สมัคร)
        private string notApprove = Resources.propReg_NotApprove_notApprove;
        private string mType = Resources.propReg_NotApprove_MemberTypeGeneral;
        public string reqReg = Resources.propRegisGeneral_regGuest;
        public TextBox TextboxIDNumber { get { return txtIDNumber; } }
        public TextBox TextboxBirthDay { get { return txtBirthDay; } }
        public DTO.ResponseMessage<bool> EMailValidationBeforeSubmit { get { return this.EMailValidation(); } }

        //public Panel PanelMainDCardValidate { get { return pnlMainDCardValidate; } set { pnlMainDCardValidate = value; } }
        ControlHelper ctrlHelper = new ControlHelper();

        public MasterRegister MasterPage
        {
            get { return (this.Page.Master as MasterRegister); }
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

        public List<DTO.TranSpecial> ListTranSpecial
        {
            get
            {
                if (Session["TranSpecial"] == null)
                {
                    Session["TranSpecial"] = new List<DTO.TranSpecial>();
                }

                return (List<DTO.TranSpecial>)Session["TranSpecial"];
            }
            set
            {
                Session["TranSpecial"] = value;
            }
        }
        #endregion

        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            ucAddressCurrent.errorAddressRequired = Resources.errorRegisGeneral_ucAddressCurrent_errorAddressRequired;

            ucAddressCurrent.errorProvinceAddressRequired = Resources.errorRegisGeneral_ucAddressCurrent_errorProvinceAddressRequired;
            ucAddressCurrent.errorDistrictAddressRequired = Resources.errorRegisGeneral_ucAddressCurrent_errorDistrictAddressRequired;
            ucAddressCurrent.errorParishAddressRequired = Resources.errorRegisGeneral_ucAddressCurrent_errorParishAddressRequired;
            ucAddressCurrent.errorPostcodeAddressRequired = Resources.errorRegisGeneral_ucAddressCurrent_errorPostcodeAddressRequired;

            ucAddressRegister.errorAddressRequired = Resources.errorRegisGeneral_ucAddressRegister_errorAddressRequired;
            ucAddressRegister.errorProvinceAddressRequired = Resources.errorRegisGeneral_ucAddressRegister_errorProvinceAddressRequired;
            ucAddressRegister.errorDistrictAddressRequired = Resources.errorRegisGeneral_ucAddressRegister_errorDistrictAddressRequired;
            ucAddressRegister.errorParishAddressRequired = Resources.errorRegisGeneral_ucAddressRegister_errorParishAddressRequired;
            ucAddressRegister.errorPostcodeAddressRequired = Resources.errorRegisGeneral_ucAddressRegister_errorPostcodeAddressRequired;

            txtBirthDay.Attributes.Add("readonly", "true");
            txtImportBirthdayHead.Attributes.Add("readonly", "true");
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "Call my function", "setdate()", true);
            //ScriptManager.RegisterStartupScript(this, GetType(), "textreadonly", "setdate();", true);
            //txtIDNumber.Attributes.Add("onblur", "javascript:return checkUser(" + txtIDNumber.ClientID + ");");

            //txtEmail.Attributes.Add("onblur", "javascript:return checkemail(" + txtEmail.ClientID + ");");

            //txtIDNumber.Attributes.Add("onkeydown", "javascript:return checkKeyNumberFixPoint(event, this, 13);");
            //txtIDNumber.Attributes.Add("onkeypress", "javascript:return checkKeyNumberKeyPressFixPoint(event, this, 13);");

            txtTel.Attributes.Add("onkeydown", "javascript:return checkKeyNumberFixPoint(event, this, 15);");
            txtTel.Attributes.Add("onkeypress", "javascript:return checkKeyNumberKeyPressFixPoint(event, this, 15);");

            //compDateValidator.ValueToCompare = DateTime.Now.ToString("MM/dd/yyyy");

            txtMobilePhone.Attributes.Add("onkeydown", "javascript:return checkKeyNumberFixPoint(event, this, 15);");
            txtMobilePhone.Attributes.Add("onkeypress", "javascript:return checkKeyNumberKeyPressFixPoint(event, this, 15);");

            if (!Page.IsPostBack)
            {
                //Set Group Validation
                this.MasterPage.SetValidateGroup(this.reqReg);
                this.ucAddressCurrent.SetValidateGroup(this.reqReg);
                //this.ucAddressRegister.SetValidateGroup(this.reqReg); //Allow null
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
            //MasterPage.ddlAgentTypeInit(this.ddlAgentType);
            MasterPage.GetQualification(ddlQualification);

            newName.Append(this.mType);
            //newName.Append("/ตัวแทน/นายหน้า");
            txtMemberType.Text = newName.ToString();
            //txtMemberType.Text = this.mType;
            txtMemberType.Enabled = false;

            switch (MasterPage.ActionMode)
            {
                case IAS.DTO.DataActionMode.Add:
                    InitOnAddMode();
                    break;
                case IAS.DTO.DataActionMode.Edit:
                    InitOnEditMode();
                    break;
                case IAS.DTO.DataActionMode.View:
                    InitOnViewMode();
                    break;
                case IAS.DTO.DataActionMode.TargetView:
                    InitOnTargetViewMode();
                    break;
                default:
                    InitOnAddMode();
                    break;
            }

        }


        /// <summary>
        /// ControlValidation on NewRegis
        /// By Nattapong @ 01/11/2556
        /// </summary>
        private void InitOnAddMode()
        {
            MasterPage.RegistrationId = IAS.BLL.Helpers.GenerateIdHelper.GetGenAutoId();
            this.MasterPage.ControlValidation(this);
            
            //this.pnlMainDCardValidate.Enabled = true;
            //this.pnlMainDCardValidate.Visible = true;
        }

        private void InitOnEditMode()
        {
            GetRegistration();
            EnableControl(true);
            StatusInit();

        }

        private void InitOnViewMode()
        {
            GetRegistration();
            EnableControl(false);
            StatusInit();
        }

        private void InitOnTargetViewMode()
        {
            //DTO.Registration res = MasterPage.GetRegistration();
            //if (res != null)
            //    GetLoadDataToControl(res);
            //EnableControl(false);
            //StatusInit();

            DTO.Registration res = MasterPage.GetRegistration();
            if (res != null)
            {
                GetLoadDataToControl(res);
                //Status 1 = รออนุมัติ(สมัคร), Status 3 = ไม่อนุมัติ(สมัคร)
                if (!res.STATUS.Equals("1") && !res.STATUS.Equals("3"))
                {
                    this.MasterPage.ShowApproveResultPerson();
                }
            }
            EnableControl(false);
            StatusInit();
        }

        private void StatusInit()
        {

            if (this.MasterPage.UserProfile.IS_APPROVE.Equals("N"))
            {

                if (this.Status != null)
                {
                    if (this.Status == DTO.RegistrationStatus.WaitForApprove.GetEnumValue().ToString())
                    {
                        lblMessage.Text = this.waitApprove;
                        lblMessage.Visible = true;
                    }
                    else if (this.Status == DTO.RegistrationStatus.NotApprove.GetEnumValue().ToString())
                    {
                        lblMessage.Text = this.notApprove;
                        lblMessage.Visible = true;

                    }

                }

            }

        }

        private void GetRegistration()
        {

            DTO.Registration res = MasterPage.GetLastRegistration();
            if (res != null)
                GetLoadDataToControl(res);
        }

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

            ucAddressRegister.DropdownProvince.SelectedIndex = ucAddressCurrent.DropdownProvince.SelectedIndex;

            ucAddressRegister.DropdownDistrict.Items.Clear();
            ListItem districtItem = ucAddressCurrent.DropdownDistrict.SelectedItem;
            if (ucAddressCurrent.DropdownDistrict.SelectedValue != "")//--milk
                ucAddressRegister.DropdownDistrict.Items.Add(districtItem);

            ucAddressRegister.DropdownParish.Items.Clear();
            ListItem parish = ucAddressCurrent.DropdownParish.SelectedItem;
            if (ucAddressCurrent.DropdownParish.SelectedValue != "")//--milk
                ucAddressRegister.DropdownParish.Items.Add(parish);

            ucAddressRegister.Address = ucAddressCurrent.Address;
            ucAddressRegister.PostCode = ucAddressCurrent.PostCode;
        }

        private DTO.Registration GetRegistrationFromDataControl()
        {

            DTO.Registration general = new DTO.Registration();

            //var attachFiles = this.AttachFiles;
            //var personAttachFiles = this.PersonAttachFiles;
            general.ID = MasterPage.RegistrationId;
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
            general.CREATED_BY = "agdoi";
            general.CREATED_DATE = DateTime.Now;
            general.UPDATED_BY = "agdoi";
            general.UPDATED_DATE = DateTime.Now;
            general.REG_PASS = MasterPage.Password;
            
            //get result
            if (!this.MasterPage.ActionMode.Equals(IAS.DTO.DataActionMode.Add))
            {
                BLL.RegistrationBiz regBiz = new BLL.RegistrationBiz();
                var res = regBiz.GetById(general.ID.ToString());
                if (res != null)
                {
                    if (res.DataResponse.APPROVE_RESULT != null)
                    {
                        general.APPROVE_RESULT = res.DataResponse.APPROVE_RESULT;
                    }

                }
            }
            else
            {
                general.APPROVE_RESULT = "";
            }

            //general.AGENT_TYPE = ddlAgentType.SelectedValue;
            //SELECT FROM AG_AGENT_TYPE_R > Z = ตัวแทนและนายหน้า
            general.AGENT_TYPE = "Z";
            general.IMPORT_STATUS = this.MasterPage.NullableString(this.MasterPage.ImportStatus);

            return general;
            
        }

        private void GetLoadDataToControl(DTO.Registration general)
        {

            //var attachFiles = this.AttachFiles;
            //var personAttachFiles = this.PersonAttachFiles;
            MasterPage.RegistrationId = general.ID;
            if (general.MEMBER_TYPE != ((int)DTO.MemberType.General).ToString())
            {
                MasterPage.ModelError.ShowMessageError = SysMessage.UserMissMatchRegitrationData;
                MasterPage.ModelError.ShowModalError();
            }

            txtIDNumber.Text = general.ID_CARD_NO;
            //ddlTitle.SelectedValue = general.PRE_NAME_CODE;
            ddlTitle.SelectedValue = (general.PRE_NAME_CODE == null) ? "" : Convert.ToString(Convert.ToInt32(general.PRE_NAME_CODE));
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

        public void NewRegistration()
        {
            //if (!ValidDateInput())
            //{
            //    return;
            //}

            MasterPage.NewRegister(DTO.RegistrationType.General, GetRegistrationFromDataControl());
        }

        public void EditRegistration()
        {
            //if (!ValidDateInput())
            //{
            //    return;
            //}

            MasterPage.EditRegister(DTO.RegistrationType.General, GetRegistrationFromDataControl());
        }

        private bool ValidDateInput()
        {
            StringBuilder message = new StringBuilder();
            StringBuilder messageOther = new StringBuilder();
            bool isFocus = false;

            if (ddlTitle.SelectedValue.Length < 1 && ddlTitle.SelectedIndex == 0)
            {

                message.AppendLine(lblTitle.Text);
                if (!isFocus)
                {
                    ddlTitle.Focus();
                    isFocus = true;
                }
            }

            if (string.IsNullOrEmpty(txtFirstName.Text) && txtFirstName.Text.Length < 1)
            {

                message.AppendLine(lblFirstName.Text);
                if (!isFocus)
                {
                    txtFirstName.Focus();
                    isFocus = true;
                }
            }

            if (string.IsNullOrEmpty(txtLastName.Text) && txtLastName.Text.Length < 1)
            {

                message.AppendLine(lblLastName.Text);
                if (!isFocus)
                {
                    txtLastName.Focus();
                    isFocus = true;
                }
            }

            if (string.IsNullOrEmpty(txtIDNumber.Text) && txtIDNumber.Text.Length < 1)
            {

                message.AppendLine(lblIDNumber.Text);
                if (!isFocus)
                {
                    txtIDNumber.Focus();
                    isFocus = true;
                }
            }

            if (string.IsNullOrEmpty(txtBirthDay.Text) && txtBirthDay.Text.Length < 1)
            {

                message.AppendLine(lblBirthDay.Text);
                if (!isFocus)
                {
                    txtBirthDay.Focus();
                    isFocus = true;
                }
            }

            if (ddlEducation.SelectedValue.Length < 1 && ddlEducation.SelectedIndex == 0)
            {

                message.AppendLine(lblEducation.Text);
                if (!isFocus)
                {
                    ddlEducation.Focus();
                    isFocus = true;
                }
            }

            if (ddlNationality.SelectedValue.Length < 1 && ddlNationality.SelectedIndex == 0)
            {

                message.AppendLine(lblNationality.Text);
                if (!isFocus)
                {
                    ddlNationality.Focus();
                    isFocus = true;
                }
            }

            if (string.IsNullOrEmpty(txtMobilePhone.Text) && txtMobilePhone.Text.Length < 1)
            {

                message.AppendLine(lblMobilePhone.Text);
                if (!isFocus)
                {
                    txtMobilePhone.Focus();
                    isFocus = true;
                }
            }

            if (string.IsNullOrEmpty(txtEmail.Text) && txtEmail.Text.Length < 1)
            {

                message.AppendLine(lblEmail.Text);
                if (!isFocus)
                {
                    txtEmail.Focus();
                    isFocus = true;
                }
            }

            if (string.IsNullOrEmpty(txtMemberType.Text) && txtMemberType.Text.Length < 1)
            {

                message.AppendLine(lblTypeMember.Text);
                if (!isFocus)
                {
                    txtMemberType.Focus();
                    isFocus = true;
                }
            }

            String addressCuurentThrowMessage = ucAddressCurrent.ThrowIsInValidMessage();
            if (!string.IsNullOrEmpty(addressCuurentThrowMessage))
                message.AppendLine(addressCuurentThrowMessage);

            String addressRegisterThrowMessage = ucAddressRegister.ThrowIsInValidMessage();
            if (!string.IsNullOrEmpty(addressRegisterThrowMessage))
                message.AppendLine(addressRegisterThrowMessage);



            if (string.IsNullOrEmpty(MasterPage.Password) && MasterPage.Password.Length < 1)
            {
                if (message.Length > 0)
                {
                    message.Append(", ");
                }
                message.Append(MasterPage.LabelPassword);
                if (!isFocus)
                {
                    MasterPage.TextBoxPassword.Focus();
                    isFocus = true;
                }
            }

            if (string.IsNullOrEmpty(MasterPage.ConfirmPassword) && MasterPage.ConfirmPassword.Length < 1)
            {
                if (message.Length > 0)
                {
                    message.Append(", ");
                }
                message.Append(MasterPage.LableConfirmPassword);
                if (!isFocus)
                {
                    MasterPage.TextBoxConfirmPassword.Focus();
                    isFocus = true;
                }
            }

            if (message.Length > 0)
            {
                //AlertMessage.ShowAlertMessage(SysMessage.DataEmpty, message.ToString());

                MasterPage.ModelError.ShowMessageError = message.ToString();
                MasterPage.ModelError.ShowModalError();

                return false;
            }


            IsValidEmail(txtEmail.Text);

            return true;
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
            ucAddressCurrent.Enabled(IsEnable);
            ucAddressRegister.Enabled(IsEnable);
            UpdatePanelUpload.Update();

            //Get Status after edit data for disable control
            ResponseService<Registration> res;
            using (BLL.RegistrationBiz biz = new BLL.RegistrationBiz())
            {
                //??
                //res = biz.GetById(this.MasterPage.UserProfile.Id);
                res = biz.GetById(this.MasterPage.RegistrationId);
                
            }
            
            string Mode = Request.QueryString["Mode"];
            if (Mode.Equals("E") && res.DataResponse.STATUS.Equals(DTO.RegistrationStatus.NotApprove.GetEnumValue().ToString()))
            {
                //txtBirthDay.ReadOnly = true; //Edited by Nattapong because of can't get the value while ReadOnly
                Session["Status"] = this.MasterPage.NullableString(res.DataResponse.STATUS);
                txtIDNumber.Enabled = false;
                MasterPage.PNLAddButton.Visible = true;
                MasterPage.PNLAddButton.Enabled = true;
                txtEmail.Enabled = false;
                this.MasterPage.AttachFileControl.EnableGridView(true);
                this.MasterPage.AttachFileControl.EnableUpload(true);

                this.pnlIDCardValidate.Enabled = false;
            }
            else if (Mode.Equals("E") && res.DataResponse.STATUS.Equals(DTO.RegistrationStatus.WaitForApprove.GetEnumValue().ToString()))
            {
                txtBirthDay.ReadOnly = true;
                Session["Status"] = this.MasterPage.NullableString(res.DataResponse.STATUS);
                txtIDNumber.Enabled = false;
                MasterPage.PNLAddButton.Visible = false;
                MasterPage.PNLAddButton.Enabled = false;
                txtEmail.Enabled = false;
                this.MasterPage.AttachFileControl.EnableGridView(false);
                this.MasterPage.AttachFileControl.EnableUpload(false);

                //this.chkEnableEditName.Enabled = false;
                this.pnlIDCardValidate.Enabled = false;
            }
            else
            {
                txtBirthDay.ReadOnly = true;
                Session["Status"] = this.MasterPage.NullableString(res.DataResponse.STATUS);
                txtIDNumber.Enabled = IsEnable;
                txtEmail.Enabled = IsEnable;
                this.MasterPage.AttachFileControl.EnableGridView(false);
                this.MasterPage.AttachFileControl.EnableUpload(false);

                this.pnlIDCardValidate.Enabled = false;
            }

            this.MasterPage.GetCurrentLicense(this.ucCurrentLicense, res.DataResponse.ID_CARD_NO);
        }

        public DTO.ResponseMessage<bool> EMailValidation()
        {
            //var res = new DTO.ResponseMessage<bool>();
            BLL.RegistrationBiz biz = new BLL.RegistrationBiz();
            DTO.Registration ent = new Registration();
            ent.ID_CARD_NO = txtIDNumber.Text;
            ent.MEMBER_TYPE = ((int)DTO.MemberType.General).ToString();
            ent.NAMES = txtFirstName.Text;
            ent.LASTNAME = txtLastName.Text;
            ent.EMAIL = txtEmail.Text;
            DTO.ResponseMessage<bool> res = biz.EntityValidation(DTO.RegistrationType.General, ent);
            //DTO.ResponseMessage<bool> res = biz.EntityValidation(DTO.RegistrationType.General, GetRegistrationFromDataControl());

            return res;
        }

        #endregion

        #region UI Function
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            (this.Page.Master as MasterRegister).OkRegister_Click += new EventHandler(Ok_Click);
            (this.Page.Master as MasterRegister).CancelRegister_Click += new EventHandler(Cancel_Click);
            (this.Page.Master as MasterRegister).CheckAgreement_CheckedChanged += new EventHandler(Agreement_CheckedChanged);
            (this.Page.Master as MasterRegister).SaveRegiter_Click += new EventHandler(Save_Click);
        }

        protected void Ok_Click(Object sender, EventArgs e)
        {

            (this.Page.Master as MasterRegister).ConfirmUserName = txtIDNumber.Text.Trim();
            txtIDNumber.Enabled = false;

            //Disable Control
            //this.chkEnableEditName.Checked = false;
            this.pnlIDCardValidate.Enabled = false;
        }

        protected void Cancel_Click(Object sender, EventArgs e)
        {

        }

        protected void Agreement_CheckedChanged(Object sender, EventArgs e)
        {

        }

        protected void Save_Click(Object sender, EventArgs e)
        {
            switch (MasterPage.ActionMode)
            {
                case IAS.DTO.DataActionMode.Add:
                    NewRegistration();
                    break;
                case IAS.DTO.DataActionMode.Edit:
                    EditRegistration();
                    break;
                case IAS.DTO.DataActionMode.View:
                    break;
                default:
                    break;
            }
        }

        protected void chkCopyAdd_CheckedChanged(object sender, EventArgs e)
        {
            //ucAddressCurrent.Clone(ucAddressRegister);
            //ucAddressRegister = ucAddressCurrent;
            if (((CheckBox)sender).Checked)
            {

                CloneAddress();
                //ucAddressRegister.Enabled(false);
            }
            else
            {
                ucAddressRegister.Enabled(true);
                ucAddressRegister.DropdownProvince.SelectedIndex = 0;
                ucAddressRegister.DropdownDistrict.Items.Clear();
                ucAddressRegister.DropdownParish.Items.Clear();
                ucAddressRegister.Address = ucAddressCurrent.Address;
                ucAddressRegister.PostCode = ucAddressCurrent.PostCode;
                ucAddressRegister.Clear();
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


        #region คุณวุฒิ
        protected void lbtAddQualification_Click(object sender, EventArgs e)
        {
            if (ddlQualification.SelectedValue == "")
            {
                UCModalError1.ShowMessageError = Resources.errorRegisGeneral_001;
                UCModalError1.ShowModalError();
                return;
            }
            DTO.TranSpecial ts = new DTO.TranSpecial();
            ts.SPECIAL_TYPE_CODE = ddlQualification.SelectedValue;
            ts.SPECIAL_TYPE_DESC = ddlQualification.SelectedItem.Text;
            if (ListTranSpecial.FirstOrDefault(x => x.SPECIAL_TYPE_CODE == ddlQualification.SelectedValue) == null)
            {
                ListTranSpecial.Add(ts);
                GvQualification.DataSource = ListTranSpecial;
                GvQualification.DataBind();
                ddlQualification.SelectedValue = "";
            }
            else
            {
                UCModalError1.ShowMessageError = Resources.errorRegisGeneral_002;
                UCModalError1.ShowModalError();
                ddlQualification.SelectedValue = "";
            }
        }

        protected void lblDelete_Click(object sender, EventArgs e)
        {
            Label lblcode = (Label)((GridViewRow)((LinkButton)sender).Parent.Parent).FindControl("lblCode");
            ListTranSpecial.Remove(ListTranSpecial.FirstOrDefault(x => x.SPECIAL_TYPE_CODE == lblcode.Text));
            GvQualification.DataSource = ListTranSpecial;
            GvQualification.DataBind();
        }

        protected void lbtUpdate_Click(object sender, EventArgs e)
        {
            ((LinkButton)sender).Visible = false;
            LinkButton lbtSave = (LinkButton)((GridViewRow)((LinkButton)sender).Parent.Parent).FindControl("lbtSave");
            lbtSave.Visible = true;
            LinkButton lbtCancel = (LinkButton)((GridViewRow)((LinkButton)sender).Parent.Parent).FindControl("lbtCancel");
            lbtCancel.Visible = true;
            DropDownList ddlQuali = (DropDownList)((GridViewRow)((LinkButton)sender).Parent.Parent).FindControl("ddlQualification");
            MasterPage.GetQualification(ddlQuali);   
            ddlQuali.Visible = true;
            Label lblname = (Label)((GridViewRow)((LinkButton)sender).Parent.Parent).FindControl("lblName");
            lblname.Visible = false;
            Label lblcode = (Label)((GridViewRow)((LinkButton)sender).Parent.Parent).FindControl("lblCode");
            ddlQuali.SelectedValue = lblcode.Text;
        }

        protected void lbtSave_Click(object sender, EventArgs e)
        {           
            LinkButton lbtUpdate = (LinkButton)((GridViewRow)((LinkButton)sender).Parent.Parent).FindControl("lbtUpdate");
            LinkButton lbtSave = (LinkButton)((GridViewRow)((LinkButton)sender).Parent.Parent).FindControl("lbtSave");
            LinkButton lbtCancel = (LinkButton)((GridViewRow)((LinkButton)sender).Parent.Parent).FindControl("lbtCancel");         
            DropDownList ddlQuali = (DropDownList)((GridViewRow)((LinkButton)sender).Parent.Parent).FindControl("ddlQualification");          
            Label lblname = (Label)((GridViewRow)((LinkButton)sender).Parent.Parent).FindControl("lblName");            
            Label lblcode = (Label)((GridViewRow)((LinkButton)sender).Parent.Parent).FindControl("lblCode");
            if (ddlQuali.SelectedValue == "")
            {
                UCModalError1.ShowMessageError = Resources.errorRegisGeneral_001;
                UCModalError1.ShowModalError();
                UpdatePanelUpload.Update();
            }
            else if (lblcode.Text == ddlQuali.SelectedValue)
            {
                lblcode.Text = ddlQuali.SelectedValue;
                lblname.Text = ddlQuali.SelectedItem.Text;
                lblcode.Visible = true;
                lblname.Visible = true;
                ddlQuali.Visible = false;
                lbtUpdate.Visible = true;
                lbtSave.Visible = false;
                lbtCancel.Visible = false;
            }
            else if (ListTranSpecial.FirstOrDefault(x => x.SPECIAL_TYPE_CODE == ddlQuali.SelectedValue) == null)
            {
                ListTranSpecial.Remove(ListTranSpecial.FirstOrDefault(x => x.SPECIAL_TYPE_CODE == lblcode.Text));
                ListTranSpecial.Add(new TranSpecial { SPECIAL_TYPE_CODE = ddlQuali.SelectedValue, SPECIAL_TYPE_DESC = ddlQuali.SelectedItem.Text });
                lblcode.Text = ddlQuali.SelectedValue;
                lblname.Text = ddlQuali.SelectedItem.Text;
                lblcode.Visible = true;
                lblname.Visible = true;
                ddlQuali.Visible = false;
                lbtUpdate.Visible = true;
                lbtSave.Visible = false;
                lbtCancel.Visible = false;
            }
            else
            {
                UCModalError1.ShowMessageError = Resources.errorRegisGeneral_002;
                UCModalError1.ShowModalError();
                UpdatePanelUpload.Update();
            }
         
        }

        protected void lbtCancel_Click(object sender, EventArgs e)
        {
            LinkButton lbtUpdate = (LinkButton)((GridViewRow)((LinkButton)sender).Parent.Parent).FindControl("lbtUpdate");
            LinkButton lbtSave = (LinkButton)((GridViewRow)((LinkButton)sender).Parent.Parent).FindControl("lbtSave");
            LinkButton lbtCancel = (LinkButton)((GridViewRow)((LinkButton)sender).Parent.Parent).FindControl("lbtCancel");
            DropDownList ddlQuali = (DropDownList)((GridViewRow)((LinkButton)sender).Parent.Parent).FindControl("ddlQualification");
            ddlQuali.Items.AddRange(ddlQualification.Items.OfType<ListItem>().ToList().ToArray());
            Label lblname = (Label)((GridViewRow)((LinkButton)sender).Parent.Parent).FindControl("lblName");
            Label lblcode = (Label)((GridViewRow)((LinkButton)sender).Parent.Parent).FindControl("lblCode");
            var special = ListTranSpecial.FirstOrDefault(x => x.SPECIAL_TYPE_CODE == lblcode.Text);
            lblname.Text = special.SPECIAL_TYPE_DESC;
            lblcode.Visible = true;
            lblname.Visible = true;
            ddlQuali.Visible = false;
            lbtUpdate.Visible = true;
            lbtSave.Visible = false;
            lbtCancel.Visible = false;
        }
        #endregion


        #region TOR
        /// <summary>
        /// Get&Set Old Persoanl Data from AG_PERSONAL_T by ID_CARD_NO
        /// NT@13/2/2557 & Last Edited
        /// </summary>
        /// <param name="general"></param>
        private void GetPersonalDataToControl(DTO.Person general)
        {
            //ddlAgentType.ClearSelection();
            ddlNationality.ClearSelection();
            ddlEducation.ClearSelection();

            Func<string, string> NationConvert = delegate(string input)
            {
                if ((input != null) && (input != ""))
                {
                    input = "001";

                }
                else
                {
                    input = "";
                }

                return input;
            };

            txtIDNumber.Text = general.ID_CARD_NO;
            //ddlTitle.SelectedValue = general.PRE_NAME_CODE;
            ddlTitle.SelectedValue = (general.PRE_NAME_CODE == null) ? "" : Convert.ToString(Convert.ToInt32(general.PRE_NAME_CODE));
            txtFirstName.Text = general.NAMES;
            txtLastName.Text = general.LASTNAME;
            txtIDNumber.Text = general.ID_CARD_NO;
            txtBirthDay.Text = (general.BIRTH_DATE == null) ? "" : ((DateTime)general.BIRTH_DATE).ToString("dd/MM/yyyy");
            rblSex.SelectedValue = general.SEX;
            ddlNationality.SelectedValue = NationConvert(general.NATIONALITY);
            ddlEducation.SelectedValue = general.EDUCATION_CODE;
            txtEmail.Text = general.EMAIL;
            txtTel.Text = LocalTelephoneNumberHelper.GetPhoneNumber(general.LOCAL_TELEPHONE);
            txtTelExt.Text = LocalTelephoneNumberHelper.GetExtenNumber(general.LOCAL_TELEPHONE);

            txtMobilePhone.Text = general.TELEPHONE;

            ucAddressCurrent.Address = general.ADDRESS_1; // txtCurrentAddress.Text;

            //AG_PERSONAL_T.AREA_CODE = PROVINCECODE(2)+AMPURCODE(2)+TUMBON(4)
            if ((general.AREA_CODE != null) && (general.AREA_CODE != ""))
            {
                if (general.AREA_CODE.Length > 2)
                {
                    string province = general.AREA_CODE.Substring(0, 2);
                    string district = general.AREA_CODE.Substring(2, 2);
                    string tumbon = general.AREA_CODE.Substring(4);

                    ucAddressCurrent.SelectDropDownStep(province, district, tumbon);
                }
                else
                {
                    string province = this.MasterPage.NullableString(general.PROVINCE_CODE);
                    string district = this.MasterPage.NullableString(general.AREA_CODE);
                    string tumbon = this.MasterPage.NullableString(general.TUMBON_CODE);

                    ucAddressCurrent.SelectDropDownStep(province, district, tumbon);
                }

            }

            ucAddressCurrent.PostCode = general.ZIP_CODE; // txtPostcodeCurrentAddress.Text;

            ucAddressRegister.Address = general.LOCAL_ADDRESS1; // txtRegisterAddress.Text;
            
            //AG_PERSONAL_T.AREA_CODE = PROVINCECODE(2)+AMPURCODE(2)+TUMBON(4)
            if ((general.LOCAL_AREA_CODE != null) && (general.LOCAL_AREA_CODE != ""))
            {
                if (general.LOCAL_AREA_CODE.Length > 2)
                {
                    string local_province = general.LOCAL_AREA_CODE.Substring(0, 2);
                    string local_district = general.LOCAL_AREA_CODE.Substring(2, 2);
                    string local_tumbon = general.LOCAL_AREA_CODE.Substring(4);

                    ucAddressRegister.SelectDropDownStep(local_province, local_district, local_tumbon);
                }
                else
                {
                    
                    string local_province = this.MasterPage.NullableString(general.LOCAL_PROVINCE_CODE);
                    string local_district = this.MasterPage.NullableString(general.LOCAL_AREA_CODE);
                    string local_tumbon = this.MasterPage.NullableString(general.LOCAL_TUMBON_CODE);

                    ucAddressRegister.SelectDropDownStep(local_province, local_district, local_tumbon);
                }
            }

            //ucAddressRegister.SelectDropDownStep(general.LOCAL_PROVINCE_CODE, general.LOCAL_AREA_CODE, general.LOCAL_TUMBON_CODE);
            ucAddressRegister.PostCode = general.LOCAL_ZIPCODE; // txtPostcodeRegisterAddress.Text;


            //if ((general.AGENT_TYPE != "") && (general.AGENT_TYPE != null))
            //{
            //    ddlAgentType.SelectedValue = general.AGENT_TYPE;
            //}

        }
        
        protected void chkIDCardValidate_CheckedChanged(object sender, EventArgs e)
        {
            //if (this.chkEnableEditName.Checked == true)
            //{
            //    pnlIDCardValidate.Enabled = true;

            //}
            //else
            //{
            //    pnlIDCardValidate.Enabled = false;
            //}
        }

        public void OnConfirm(object sender, EventArgs e)
        {
            string confirmValue = Request.Form["confirm_value"];
            string mystring = confirmValue;
            string res = mystring.Substring(Math.Max(0, mystring.Length - 1));

            if (res == "Y")
            {
                //IF say Yes
                GetPersonalDataToControl(DTO.PersonalT.per);
                UpdatePanelUpload.Update();
                this.MasterPage.UpdatePanelMaster.Update();
            }
            if (res == "N")
            {
                //IF say No

            }

        }

        protected void btnImportByIDCard_Click(object sender, EventArgs e)
        {
            string dtFromCtrl = string.Empty;
            string dtFromEnt = string.Empty;
            string licenseNo = string.Empty;

            if ((txtImportBirthdayHead.Text == "") || (txtImportLicenseNoHead.Text == ""))
            {
                this.MasterPage.ModelError.ShowMessageError = "กรุณาระบุวันเกิดและเลขที่ใบอนุญาต";
                this.MasterPage.ModelError.ShowModalError();
                return;
            }
            else
            {
                //BirthDay validate
                dtFromCtrl = String.Format("{0:dd/MM/yyyy}", txtImportBirthdayHead.Text);
                dtFromEnt = String.Format("{0:dd/MM/yyyy}", DTO.PersonalT.per.BIRTH_DATE);

                //License validate
                List<DTO.PersonLicenseTransaction> lsLicense = this.MasterPage.CompareCurrentLicense(DTO.PersonalT.per.ID_CARD_NO);
                if (lsLicense.Count > 0)
                {
                    this.MasterPage.CurrentLicenseByIDCard = lsLicense;
                    DTO.PersonLicenseTransaction cur = lsLicense.FirstOrDefault(lic => lic.LICENSE_NO == txtImportLicenseNoHead.Text);
                    if (cur != null)
                    {
                        licenseNo = this.MasterPage.NullableString(cur.LICENSE_NO);
                    }

                }

                if (dtFromCtrl.Equals(dtFromEnt) && (licenseNo == txtImportLicenseNoHead.Text))
                {
                    GetPersonalDataToControl(DTO.PersonalT.per);
                    this.MasterPage.SetCurrentLicense(this.ucCurrentLicense);
                    UpdatePanelUpload.Update();
                    this.MasterPage.UpdatePanelMaster.Update();
                    this.MasterPage.ImportStatus = "Y";


                    //Disable Control
                    this.pnlIDCardValidate.Enabled = false;
                    //this.chkEnableEditName.Checked = false;
                }
                else
                {
                    this.MasterPage.ModelError.ShowMessageError = "วันเกิดหรือเลขที่ใบอนุญาตไม่ถูกต้อง";
                    this.MasterPage.ModelError.ShowModalError();
                    return;
                }


            }


        }

        protected void btnImgIDCardValidate_Click(object sender, ImageClickEventArgs e)
        {
            RegistrationBiz biz = new RegistrationBiz();

            if (txtIDNumber.Text.Equals(""))
            {
                this.MasterPage.ModelError.ShowMessageError = IDNumberRequired.ErrorMessage;
                this.MasterPage.ModelError.ShowModalError();
                return;
            }
            else
            {
                ResponseService<DTO.Person> res = biz.GetPersonalDetailByIDCard(txtIDNumber.Text.Trim());

                if (res.DataResponse != null)
                {
                    this.MasterPage.SetPersonalData(res.DataResponse);
                    ctrlHelper.ClearInput(this.mdpIDCardValidation);
                    txtImportBirthdayHead.Text = "";
                    txtImportLicenseNoHead.Text = "";
                    mdpIDCardValidation.Show();

                }
                else
                {
                    this.MasterPage.ModelError.ShowMessageError = res.ErrorMsg;
                    this.MasterPage.ModelError.ShowModalError();
                    return;

                }
            }
        }

        //protected void chkEnableName_CheckedChanged(object sender, EventArgs e)
        //{
            //if (chkEnableEditName.Checked == true)
            //{
            //    pnlIDCardValidate.Enabled = true;
            //}
            //else
            //{
            //    pnlIDCardValidate.Enabled = false;

                //Rebind for set to Default value
                //if not > selected index error
                //ucAddressCurrent.SelectDropDownStep("", "", "");
                //ucAddressRegister.SelectDropDownStep("", "", "");

                //Clear All text in Control
                //ctrlHelper.ClearInput(this);
           // }

        //}

        #endregion

       
    }
}
