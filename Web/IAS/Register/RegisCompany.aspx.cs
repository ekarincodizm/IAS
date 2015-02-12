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
using System.Runtime.Serialization;


namespace IAS.Register
{
    public partial class RegisCompany : System.Web.UI.Page
    {
        #region Public Param & Session
        ////ยังไม่ทำการอนุมัติ(สมัคร)
        private string waitApprove = Resources.propReg_NotApprove_waitApprove;
        ////ไม่อนุมัติ(สมัคร)
        private string notApprove = Resources.propReg_NotApprove_notApprove;
        private string mType = Resources.propReg_Co_MemberTypeCompany;
        public string reqReg = Resources.propRegisCompany_reqRegCompany;
        public TextBox TextboxEmail { get { return txtEmail; } }
        public DTO.ResponseMessage<bool> EMailValidationBeforeSubmit { get { return this.EMailValidation(); } }

        //public Panel PanelMainDCardValidate { get { return pnlMainDCardValidate; } set { pnlMainDCardValidate = value; } }
        ControlHelper ctrlHelper = new ControlHelper();

        public MasterRegister MasterPage
        {
            get { return (this.Page.Master as MasterRegister); }
        }

        DTO.DataActionMode _DataAction;
        public DTO.DataActionMode DataAction
        {
            get
            {
                _DataAction = Session["UserProfile"] == null ? DTO.DataActionMode.Add : DTO.DataActionMode.Edit;

                return _DataAction;
            }
            set
            {
                _DataAction = value;
            }
        }

        public DTO.UserProfile UserProfile
        {
            get
            {
                return Session["UserProfile"] == null ? null : (DTO.UserProfile)Session["UserProfile"];
            }
        }

        public String UserProfileId { get; set; }

        public string MememberTypeCompany
        {
            get
            {
                return (string)Session["MememberTypeCompany"];
            }
        }

        public List<DTO.DataItem> GetDocumentTypeIsImage
        {
            get
            {
                if (Session["DocumentTypeIsImage"] == null)
                {
                    Session["DocumentTypeIsImage"] = new List<DTO.DataItem>();
                }
                return (List<DTO.DataItem>)Session["DocumentTypeIsImage"];
            }
            set
            {
                Session["DocumentTypeIsImage"] = value;
            }
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
            //txtEmail.Attributes.Add("onblur", "javascript:return checkemail(" + txtEmail.ClientID + ");");
            //txtIDNumber.Attributes.Add("onblur", "javascript:return checkUser();");

            //txtIDNumber.Attributes.Add("onkeydown", "javascript:return checkKeyNumberFixPoint(event, this, 13);");
            //txtIDNumber.Attributes.Add("onkeypress", "javascript:return checkKeyNumberKeyPressFixPoint(event, this, 13);");

            txtTel.Attributes.Add("onkeydown", "javascript:return checkKeyNumberFixPoint(event, this, 15);");
            txtTel.Attributes.Add("onkeypress", "javascript:return checkKeyNumberKeyPressFixPoint(event, this, 15);");

            UcAddress.TextBoxPostCode.Attributes.Add("onkeydown", "javascript:return checkKeyNumberFixPoint(event, this, 5);");
            UcAddress.TextBoxPostCode.Attributes.Add("onkeypress", "javascript:return checkKeyNumberKeyPressFixPoint(event, this, 5);");

            txtCompanyTel.Attributes.Add("onkeydown", "javascript:return checkKeyNumberFixPoint(event, this, 15);");
            txtCompanyTel.Attributes.Add("onkeypress", "javascript:return checkKeyNumberKeyPressFixPoint(event, this, 15);");

            if (!IsPostBack)
            {
                InitData();
                txtCompanyRegister.Enabled = false;//milk
            }
        }
        #endregion
        
        #region Main Public && Private Function
        private Action<DropDownList, List<DTO.DataItem>> BindToDDL = (ddl, ls) =>
        {
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Id";
            ddl.DataSource = ls;
            ddl.DataBind();
        };

        private void InitData()
        {
            this.MasterPage.GetTitle(ddlTitle);
            this.MasterPage.Init();
            //Set Group Validation
            this.MasterPage.SetValidateGroup(this.reqReg);
            this.UcAddress.SetValidateGroup(this.reqReg);

            txtMemberType.Text = this.mType;
            txtMemberType.Enabled = false;

            switch (this.MasterPage.ActionMode)
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
            this.MasterPage.RegistrationId = IAS.BLL.Helpers.GenerateIdHelper.GetGenAutoId();
            this.MasterPage.ControlValidation(this);
        }

        private void InitOnEditMode()
        {
            GetLastReteration();
            EnableControl(true);
            StatusInit();
        }

        private void InitOnViewMode()
        {
            GetLastReteration();
            EnableControl(false);
            StatusInit();
        }

        private void InitOnTargetViewMode()
        {
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

        private void GetAttatchFiles()
        {
            var biz = new BLL.PersonBiz();
            string personID = this.UserProfile.Id;
            DTO.ResponseService<DTO.PersonAttatchFile[]> res = biz.GetUserProfileAttatchFileByPersonId(personID);

            var list = res.DataResponse.ToList();

            this.MasterPage.AttachFileControl.AttachFiles = list.ConvertToAttachFilesView();

        }

        private void GetCompany()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var list = biz.GetCompanyCodeByName("");
            string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(list);
            hdf.Value = jsonString;
        }

        private void GetProvince()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetProvince(SysMessage.DefaultSelecting);
            BindToDDL(UcAddress.DropdownProvince, ls);
        }

        private void GetTitleName()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetTitleName(SysMessage.DefaultSelecting);
            BindToDDL(ddlTitle, ls);
        }

        private static bool validateFileType(string tempFileName)
        {
            string fileExtension = System.IO.Path.GetExtension(tempFileName).Replace(".", string.Empty).ToLower();
            bool invalidFileExtensions = DTO.DocumentFileType.IMAGE_BMP_GIF_JPG_PNG_TIF_PDF.ToString().ToLower().Contains(fileExtension);
            return invalidFileExtensions;
        }

        private void GetAttachFilesType()
        {
            var message = SysMessage.DefaultSelecting;
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetDocumentType(message);
            this.MasterPage.AttachFileControl.DocumentTypes = ls;

        }

        private void GetAttachFilesTypeImage()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            Session["DocumentTypeIsImage"] = biz.GetDocumentTypeIsImage();
        }

        private void GetAttatchRegisterationFiles()
        {
            var biz = new BLL.RegistrationBiz();
            string personID = UserProfileId;
            DTO.ResponseService<DTO.RegistrationAttatchFile[]> res = biz.GetAttatchFilesByRegisterationID(personID);

            var list = res.DataResponse.ToList();

            this.MasterPage.AttachFileControl.AttachFiles = list.ConvertToAttachFilesView();
        }

        public void ClearControl()
        {
            ddlTitle.SelectedIndex = 0;
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtIDNumber.Text = "";
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

        private void EnableControl(Boolean IsEnable)
        {

            foreach (Control item in udpDetailCompany.Controls[0].Controls)
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
            this.UcAddress.Enabled(IsEnable);
            udpDetailCompany.Update();

            //Get Status after edit data for disable control
            ResponseService<Registration> res;
            using (BLL.RegistrationBiz biz = new BLL.RegistrationBiz())
            {
                //??
                //res = biz.GetById(this.MasterPage.UserProfile.Id);
                res = biz.GetById(this.MasterPage.RegistrationId);
                

            }

            string Mode = Request.QueryString["Mode"];
            if (Mode.Equals("E") && 
                res.DataResponse.STATUS.Equals(DTO.RegistrationStatus.NotApprove.GetEnumValue().ToString()))
            {
                Session["Status"] = this.MasterPage.NullableString(res.DataResponse.STATUS);
                txtCompany.Enabled = false;
                txtCompanyRegister.Enabled = false;
                txtIDNumber.Enabled = false;
                MasterPage.PNLAddButton.Visible = true;
                MasterPage.PNLAddButton.Enabled = true;
                txtEmail.Enabled = false;
                this.MasterPage.AttachFileControl.EnableGridView(true);
                this.MasterPage.AttachFileControl.EnableUpload(true);

            }
            else if (Mode.Equals("E") &&
                res.DataResponse.STATUS.Equals(DTO.RegistrationStatus.WaitForApprove.GetEnumValue().ToString()))
            {
                Session["Status"] = this.MasterPage.NullableString(res.DataResponse.STATUS);
                txtCompany.Enabled = false;
                txtCompanyRegister.Enabled = false;
                txtIDNumber.Enabled = false;
                MasterPage.PNLAddButton.Visible = false;
                MasterPage.PNLAddButton.Enabled = false;
                txtEmail.Enabled = false;
                this.MasterPage.AttachFileControl.EnableGridView(false);
                this.MasterPage.AttachFileControl.EnableUpload(false);

            }
            else
            {
                Session["Status"] = this.MasterPage.NullableString(res.DataResponse.STATUS);
                txtCompany.Enabled = IsEnable;
                txtCompanyRegister.Enabled = IsEnable;
                txtIDNumber.Enabled = IsEnable;
                txtEmail.Enabled = IsEnable;
                this.MasterPage.AttachFileControl.EnableGridView(false);
                this.MasterPage.AttachFileControl.EnableUpload(false);
            }

            //this.MasterPage.GetCurrentLicense(this.ucCurrentLicense, res.DataResponse.ID_CARD_NO);
        }

        private void NewRegis()
        {
            this.MasterPage.NewRegister(DTO.RegistrationType.Insurance, GetCompanyEntity());
        }

        private void EditRegis()
        {
            this.MasterPage.EditRegister(DTO.RegistrationType.Insurance, GetCompanyEntity());

        }

        private void GetLastReteration()
        {
            BLL.RegistrationBiz regBiz = new BLL.RegistrationBiz();
            var res = regBiz.GetById(this.MasterPage.UserProfile.Id);
            if (res.IsError)
            {
                this.MasterPage.ModelError.ShowMessageError = res.ErrorMsg;
                this.MasterPage.ModelError.ShowModalError();
            }
            this.MasterPage.RegistrationId = res.DataResponse.ID;

            //Get Data to Control
            GetLoadDataToControl(res.DataResponse);
        }

        private void GetLoadDataToControl(DTO.Registration companyent)
        {
            BLL.DataCenterBiz dcbiz = new BLL.DataCenterBiz();
            BLL.PersonBiz biz = new BLL.PersonBiz();
            BLL.DataCenterBiz dataCenter = new BLL.DataCenterBiz();

            var res = biz.GetUserProfileById(this.MasterPage.UserProfile.Id);
            this.MasterPage.AttachFileControl.RegisterationId = this.MasterPage.UserProfile.Id;
            this.MasterPage.AttachFileControl.CurrentUser = this.UserProfile.LoginName;

            this.MasterPage.RegistrationId = companyent.ID;

            if (!companyent.MEMBER_TYPE.Equals(DTO.MemberType.Insurance.GetEnumValue().ToString()))
            {
                MasterPage.ModelError.ShowMessageError = SysMessage.UserMissMatchRegitrationData;
                MasterPage.ModelError.ShowModalError();
            }
            rblSex.SelectedValue = companyent.SEX;//milk
            txtCompanyRegister.Text = companyent.COMP_CODE;
            ddlTitle.SelectedValue = companyent.PRE_NAME_CODE;
            txtFirstName.Text = companyent.NAMES;
            txtLastName.Text = companyent.LASTNAME;
            txtIDNumber.Text = companyent.ID_CARD_NO;
            txtIDNumber.Enabled = false;
            txtCompanyTel.Text = LocalTelephoneNumberHelper.GetPhoneNumber( companyent.LOCAL_TELEPHONE);
            txtCompanyTelExt.Text = LocalTelephoneNumberHelper.GetExtenNumber(companyent.LOCAL_TELEPHONE);
            txtTel.Text = LocalTelephoneNumberHelper.GetPhoneNumber(companyent.TELEPHONE);
            txtTelExt.Text = LocalTelephoneNumberHelper.GetExtenNumber(companyent.TELEPHONE);

            txtEmail.Text = companyent.EMAIL;
            txtEmail.Enabled = false;
            UcAddress.TextBoxAddress.Text = companyent.ADDRESS_1;
            UcAddress.TextBoxPostCode.Text = companyent.ZIP_CODE;
            
            var strName = dcbiz.GetCompanyNameById(companyent.COMP_CODE);
            txtCompany.Text = strName;
            this.MasterPage.TextBoxResultReg.Text = companyent.APPROVE_RESULT;

            UcAddress.SelectDropDownStep(companyent.PROVINCE_CODE, companyent.AREA_CODE, companyent.TUMBON_CODE);

        }

        public DTO.Registration GetCompanyEntity()
        {
            DTO.Registration companyent = new Registration();

            //var attachFiles = this.AttachFiles;

            string strFullCompanyName = txtCompany.Text;
            string[] strFullCompanyNameArray = strFullCompanyName.Split('[');
            string strCompanyNameTrim = strFullCompanyNameArray[0].Trim();

            companyent.ID = this.MasterPage.RegistrationId;
            companyent.COMP_CODE = strFullCompanyNameArray[1].Replace("]", "").Trim();
            companyent.Company_Name = strCompanyNameTrim;
            companyent.MEMBER_TYPE = DTO.MemberType.Insurance.GetEnumValue().ToString();
            companyent.ID_CARD_NO = txtIDNumber.Text;
            companyent.PRE_NAME_CODE = ddlTitle.SelectedValue;
            companyent.NAMES = txtFirstName.Text;
            companyent.LASTNAME = txtLastName.Text;
            companyent.SEX = rblSex.SelectedValue;
            companyent.EMAIL = txtEmail.Text;
            companyent.TELEPHONE = txtTel.Text + ((String.IsNullOrWhiteSpace(txtTelExt.Text)) ? "" : ("#" + txtTelExt.Text.Trim()));
            companyent.LOCAL_TELEPHONE = txtCompanyTel.Text + ((String.IsNullOrWhiteSpace(txtCompanyTelExt.Text)) ? "" : ("#" + txtCompanyTelExt.Text.Trim()));

            companyent.ADDRESS_1 = this.UcAddress.TextBoxAddress.Text;
            companyent.PROVINCE_CODE = this.UcAddress.DropdownProvince.SelectedValue;
            companyent.AREA_CODE = this.UcAddress.DropdownDistrict.SelectedValue;
            companyent.TUMBON_CODE = this.UcAddress.DropdownParish.SelectedValue;
            companyent.ZIP_CODE = this.UcAddress.TextBoxPostCode.Text;
            companyent.CREATED_BY = "AGDOI";
            companyent.CREATED_DATE = DateTime.Now;
            companyent.UPDATED_BY = "AGDOI";
            companyent.UPDATED_DATE = DateTime.Now;
            companyent.REG_PASS = this.MasterPage.TextBoxPassword.Text;
            companyent.IMPORT_STATUS = this.MasterPage.NullableString(this.MasterPage.ImportStatus);

            return companyent;
        }

        public DTO.ResponseMessage<bool> EMailValidation()
        {
            //var res = new DTO.ResponseMessage<bool>();
            BLL.RegistrationBiz biz = new BLL.RegistrationBiz();
            DTO.Registration ent = new Registration();
            ent.ID_CARD_NO = txtIDNumber.Text;
            ent.MEMBER_TYPE = DTO.MemberType.Insurance.GetEnumValue().ToString();
            ent.NAMES = txtFirstName.Text;
            ent.LASTNAME = txtLastName.Text;
            ent.EMAIL = txtEmail.Text;
            DTO.ResponseMessage<bool> res = biz.EntityValidation(DTO.RegistrationType.Insurance, ent);
            //DTO.ResponseMessage<bool> res = biz.EntityValidation(DTO.RegistrationType.Insurance, GetCompanyEntity());

            return res;
        }

        #endregion

        #region UI Function
        /// <summary>
        /// Assign Control Event from MasterPage
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            (this.MasterPage as MasterRegister).OkRegister_Click += new EventHandler(RegisCompany_OkRegister_Click);
            (this.MasterPage as MasterRegister).CancelRegister_Click += new EventHandler(RegisCompany_CancelRegister_Click);
            (this.MasterPage as MasterRegister).CheckAgreement_CheckedChanged += new EventHandler(RegisCompany_CheckAgreement_CheckedChanged);
            (this.MasterPage as MasterRegister).SaveRegiter_Click += new EventHandler(RegisCompany_SaveRegiter_Click);

        }

        void RegisCompany_OkRegister_Click(object sender, EventArgs e)
        {
            //Call MasterPage.OkRegister_Click
            txtEmail.Enabled = false;
            txtIDNumber.Enabled = false;
        }

        void RegisCompany_CancelRegister_Click(object sender, EventArgs e)
        {
            //Call MasterPage.CancelRegister_Click
        }

        void RegisCompany_CheckAgreement_CheckedChanged(object sender, EventArgs e)
        {
            //Call MasterPage.CheckAgreement_CheckedChanged
        }

        void RegisCompany_SaveRegiter_Click(object sender, EventArgs e)
        {
            //Call MasterPage.SaveRegiter_Click
            switch (this.MasterPage.ActionMode)
            {
                case DTO.DataActionMode.Add:
                    NewRegis();
                    break;
                case DTO.DataActionMode.Edit:
                    EditRegis();
                    break;
                case DTO.DataActionMode.View:
                    break;
            }
        }

        protected void hdf_ValueChanged(object sender, EventArgs e)
        {
            string selectedWidgetID = ((HiddenField)sender).Value;
            //Widget w = MyEntityService.GetWidget(selectedWidgetID);
            string[] compCode = selectedWidgetID.Split('[', ']');

            txtCompanyRegister.Text = compCode[1];

        }

        protected void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetAmpur(SysMessage.DefaultSelecting, UcAddress.DropdownProvince.SelectedValue);
            BindToDDL(UcAddress.DropdownDistrict, ls);

            UcAddress.DropdownParish.SelectedValue = "";
        }

        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetTumbon(SysMessage.DefaultSelecting, UcAddress.DropdownProvince.SelectedValue, UcAddress.DropdownDistrict.SelectedValue);
            BindToDDL(UcAddress.DropdownParish, ls);
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
            }
            else
            {
                //Enable for select Sex
                rblSex.SelectedValue = null;
                rblSex.Enabled = true;
            }
        }

        #endregion

        #region TOR
        /// <summary>
        /// Get Old Persoanl Data from AG_PERSONAL_T by ID_CARD_NO
        /// NT@13/2/2557 & Last Edited
        /// </summary>
        /// <param name="general"></param>
        //private void GetPersonalDataToControl(DTO.Person companyent)
        //{
        //    ddlTitle.ClearSelection();

        //    Func<string, string> NationConvert = delegate(string input)
        //    {
        //        if ((input != null) && (input != ""))
        //        {
        //            input = "001";

        //        }
        //        else
        //        {
        //            input = "";
        //        }

        //        return input;
        //    };

        //    rblSex.SelectedValue = companyent.SEX;//milk
        //    txtCompanyRegister.Text = companyent.COMP_CODE;
        //    ddlTitle.SelectedValue = companyent.PRE_NAME_CODE;
        //    txtFirstName.Text = companyent.NAMES;
        //    txtLastName.Text = companyent.LASTNAME;
        //    txtIDNumber.Text = companyent.ID_CARD_NO;
        //    //txtIDNumber.Enabled = false;
        //    txtCompanyTel.Text = LocalTelephoneNumberHelper.GetPhoneNumber(companyent.LOCAL_TELEPHONE);
        //    txtCompanyTelExt.Text = LocalTelephoneNumberHelper.GetExtenNumber(companyent.LOCAL_TELEPHONE);
        //    txtTel.Text = LocalTelephoneNumberHelper.GetPhoneNumber(companyent.TELEPHONE);
        //    txtTelExt.Text = LocalTelephoneNumberHelper.GetExtenNumber(companyent.TELEPHONE);

        //    txtEmail.Text = companyent.EMAIL;
        //    //txtEmail.Enabled = false;
        //    UcAddress.TextBoxAddress.Text = companyent.ADDRESS_1;
        //    UcAddress.TextBoxPostCode.Text = companyent.ZIP_CODE;
        //    txtCompany.Text = companyent.COMP_CODE;

        //    //AG_PERSONAL_T.AREA_CODE = PROVINCECODE(2)+AMPURCODE(2)+TUMBON(4)
        //    if ((companyent.AREA_CODE != null) && (companyent.AREA_CODE != ""))
        //    {
        //        if (companyent.AREA_CODE.Length > 2)
        //        {
        //            string province = companyent.AREA_CODE.Substring(0, 2);
        //            string district = companyent.AREA_CODE.Substring(2, 2);
        //            string tumbon = companyent.AREA_CODE.Substring(4);

        //            UcAddress.SelectDropDownStep(province, district, tumbon);
        //        }
        //        else
        //        {
        //            string province = this.MasterPage.NullableString(companyent.PROVINCE_CODE);
        //            string district = this.MasterPage.NullableString(companyent.AREA_CODE);
        //            string tumbon = this.MasterPage.NullableString(companyent.TUMBON_CODE);

        //            UcAddress.SelectDropDownStep(province, district, tumbon);
        //        }
        //    }


        //}

        //public void OnConfirm(object sender, EventArgs e)
        //{
        //    string confirmValue = Request.Form["confirm_value"];
        //    string mystring = confirmValue;
        //    string res = mystring.Substring(Math.Max(0, mystring.Length - 1));

        //    if (res == "Y")
        //    {
        //        //IF say Yes
        //        GetPersonalDataToControl(DTO.PersonalT.per);
        //        udpDetailCompany.Update();
        //        this.MasterPage.UpdatePanelMaster.Update();
        //    }
        //    if (res == "N")
        //    {
        //        //IF say No

        //    }

        //}

        //protected void chkEnableName_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (this.chkEnableEditName.Checked == true)
        //    {
        //        pnlIDCardValidate.Enabled = true;

        //    }
        //    else
        //    {
        //        pnlIDCardValidate.Enabled = false;
        //    }
        //}

        //protected void btnImgIDCardValidate_Click(object sender, ImageClickEventArgs e)
        //{
        //    RegistrationBiz biz = new RegistrationBiz();

        //    if (txtIDNumber.Text.Equals(""))
        //    {

        //        this.MasterPage.ModelError.ShowMessageError = IDCardRequired.ErrorMessage;
        //        this.MasterPage.ModelError.ShowModalError();
        //        return;
        //    }
        //    else
        //    {
        //        ResponseService<DTO.Person> res = biz.GetPersonalDetailByIDCard(txtIDNumber.Text.Trim());

        //        if (res.DataResponse != null)
        //        {
        //            this.MasterPage.SetPersonalData(res.DataResponse);
        //            ctrlHelper.ClearInput(this.mdpIDCardValidation);
        //            txtImportBirthdayHead.Text = "";
        //            txtImportLicenseNoHead.Text = "";
        //            mdpIDCardValidation.Show();

        //        }
        //        else
        //        {
        //            this.MasterPage.ModelError.ShowMessageError = res.ErrorMsg;
        //            this.MasterPage.ModelError.ShowModalError();
        //            return;
        //        }
        //    }
        //}

        //protected void btnImportByIDCard_Click(object sender, EventArgs e)
        //{
        //    string dtFromCtrl = string.Empty;
        //    string dtFromEnt = string.Empty;
        //    string licenseNo = string.Empty;

        //    if ((txtImportBirthdayHead.Text == "") && (txtImportLicenseNoHead.Text != ""))
        //    {
        //        List<DTO.PersonLicenseTransaction> lsLicense = this.MasterPage.CompareCurrentLicense(DTO.PersonalT.per.ID_CARD_NO);
        //        if (lsLicense.Count > 0)
        //        {
        //            this.MasterPage.CurrentLicenseByIDCard = lsLicense;
        //            DTO.PersonLicenseTransaction cur = lsLicense.FirstOrDefault(lic => lic.LICENSE_NO == txtImportLicenseNoHead.Text);
        //            if (cur != null)
        //            {
        //                licenseNo = this.MasterPage.NullableString(cur.LICENSE_NO);
        //            }

        //        }
        //        if (licenseNo == txtImportLicenseNoHead.Text)
        //        {
        //            GetPersonalDataToControl(DTO.PersonalT.per);
        //            this.MasterPage.SetCurrentLicense(this.ucCurrentLicense);
        //            udpDetailCompany.Update();
        //            this.MasterPage.UpdatePanelMaster.Update();
        //            this.MasterPage.ImportStatus = "Y";

        //        }
        //        else
        //        {
        //            this.MasterPage.ModelError.ShowMessageError = "เลขที่ใบอนุญาตไม่ถูกต้อง";
        //            this.MasterPage.ModelError.ShowModalError();
        //            return;
        //        }

        //        return;
        //    }
        //    else if ((txtImportBirthdayHead.Text != "") && (txtImportLicenseNoHead.Text == ""))
        //    {
        //        //BirthDay validate
        //        dtFromCtrl = String.Format("{0:dd/MM/yyyy}", txtImportBirthdayHead.Text);
        //        dtFromEnt = String.Format("{0:dd/MM/yyyy}", DTO.PersonalT.per.BIRTH_DATE);

        //        if (dtFromCtrl.Equals(dtFromEnt))
        //        {
        //            GetPersonalDataToControl(DTO.PersonalT.per);
        //            this.MasterPage.SetCurrentLicense(this.ucCurrentLicense);
        //            udpDetailCompany.Update();
        //            this.MasterPage.UpdatePanelMaster.Update();
        //            this.MasterPage.ImportStatus = "Y";
        //        }
        //        else
        //        {
        //            this.MasterPage.ModelError.ShowMessageError = "วันเดือนปีเกิดไม่ถูกต้อง";
        //            this.MasterPage.ModelError.ShowModalError();
        //            return;
        //        }
        //        return;
        //    }
        //    else
        //    {
        //        //BirthDay validate
        //        dtFromCtrl = String.Format("{0:dd/MM/yyyy}", txtImportBirthdayHead.Text);
        //        dtFromEnt = String.Format("{0:dd/MM/yyyy}", DTO.PersonalT.per.BIRTH_DATE);

        //        //License validate
        //        List<DTO.PersonLicenseTransaction> lsLicense = this.MasterPage.CompareCurrentLicense(DTO.PersonalT.per.ID_CARD_NO);
        //        if (lsLicense.Count > 0)
        //        {
        //            this.MasterPage.CurrentLicenseByIDCard = lsLicense;
        //            DTO.PersonLicenseTransaction cur = lsLicense.FirstOrDefault(lic => lic.LICENSE_NO == txtImportLicenseNoHead.Text);
        //            if (cur != null)
        //            {
        //                licenseNo = this.MasterPage.NullableString(cur.LICENSE_NO);
        //            }

        //        }

        //        if (dtFromCtrl.Equals(dtFromEnt) && (licenseNo == txtImportLicenseNoHead.Text))
        //        {
        //            GetPersonalDataToControl(DTO.PersonalT.per);
        //            this.MasterPage.SetCurrentLicense(this.ucCurrentLicense);
        //            udpDetailCompany.Update();
        //            this.MasterPage.UpdatePanelMaster.Update();
        //            this.MasterPage.ImportStatus = "Y";

        //        }
        //        else
        //        {
        //            this.MasterPage.ModelError.ShowMessageError = "กรุณาระบุวันเกิดและเลขที่ใบอนุญาต";
        //            this.MasterPage.ModelError.ShowModalError();
        //            return;
        //        }


        //    }


        //}

        #endregion
    }
}