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
using System.Net;
using System.Collections.Specialized;
using IAS.Properties;
using System.Runtime.Serialization;

namespace IAS.Register
{
    public partial class RegisAssociate : System.Web.UI.Page
    {
        #region Public Param & Session
        ////ยังไม่ทำการอนุมัติ(สมัคร)
        private string waitApprove = Resources.propReg_NotApprove_waitApprove;
        ////ไม่อนุมัติ(สมัคร)
        private string notApprove = Resources.propReg_NotApprove_notApprove;
        private string mType = Resources.propReg_Assoc_MemberTypeAssoc;
        public string reqReg = Resources.propReg_NotApprove_reqRegAssoc;
        public TextBox TextboxEmail { get { return txtEmail; } }

        //public Panel PanelMainDCardValidate { get { return pnlMainDCardValidate; } set { pnlMainDCardValidate = value; } }
        ControlHelper ctrlHelper = new ControlHelper();

        public DTO.ResponseMessage<bool> EMailValidationBeforeSubmit { get { return this.EMailValidation(); } }

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

        public String UserProfileId
        {
            get
            {
                return (Session["UserProfileId"] == null) ? "" : Session["UserProfileId"].ToString();
            }
            set
            {
                Session["UserProfileId"] = value;
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

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>changeUrl();</script>", false);

            //txtEmail.Attributes.Add("onblur", "javascript:return checkemail(" + txtEmail.ClientID + ");");
            //txtIDCard.Attributes.Add("onblur", "javascript:return checkUser();");

            //txtIDCard.Attributes.Add("onkeydown", "javascript:return checkKeyNumberFixPoint(event, this, 13);");
            //txtIDCard.Attributes.Add("onkeypress", "javascript:return checkKeyNumberKeyPressFixPoint(event, this, 13);");

            txtTel.Attributes.Add("onkeydown", "javascript:return checkKeyNumberFixPoint(event, this, 15);");
            txtTel.Attributes.Add("onkeypress", "javascript:return checkKeyNumberKeyPressFixPoint(event, this, 15);");

            UcAddress.TextBoxPostCode.Attributes.Add("onkeydown", "javascript:return checkKeyNumberFixPoint(event, this, 5);");
            UcAddress.TextBoxPostCode.Attributes.Add("onkeydown", "javascript:return checkKeyNumberFixPoint(event, this, 5);");

            txtAssociationTel.Attributes.Add("onkeydown", "javascript:return checkKeyNumberFixPoint(event, this, 15);");
            txtAssociationTel.Attributes.Add("onkeypress", "javascript:return checkKeyNumberKeyPressFixPoint(event, this, 15);");


            if (!IsPostBack)
            {
                InitData();
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
            string personID = UserProfileId;
            DTO.ResponseService<DTO.PersonAttatchFile[]> res = biz.GetAttatchFileByPersonId(personID);

            var list = res.DataResponse.ToList();
            this.MasterPage.AttachFileControl.AttachFiles = list.ConvertToAttachFilesView();
            udpDetailAssoc.Update();
            
        }

        private void GetAttatchRegisterationFiles()
        {
            var biz = new BLL.RegistrationBiz();
            string personID = UserProfileId;
            DTO.ResponseService<DTO.RegistrationAttatchFile[]> res = biz.GetAttatchFilesByRegisterationID(personID);

            var list = res.DataResponse.ToList();


            this.MasterPage.AttachFileControl.AttachFiles = list.ConvertToAttachFilesView();

            udpDetailAssoc.Update();
        }

        private void GetAssociation()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var list = biz.GetAssociate("");
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
            //using
            BindToDDL(ddlTitle, ls);

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
            Session["strListDocImg"] = biz.GetDocumentTypeIsImage();
        }

        private static bool validateFileType(string tempFileName)
        {
            string fileExtension = System.IO.Path.GetExtension(tempFileName).Replace(".", string.Empty).ToLower();
            bool invalidFileExtensions = DTO.DocumentFileType.IMAGE_BMP_GIF_JPG_PNG_TIF_PDF.ToString().ToLower().Contains(fileExtension);
            return invalidFileExtensions;
        }

        private void ClearValue()
        {

            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            rblSex.SelectedValue = string.Empty;
            txtTel.Text = string.Empty;
            txtEmail.Text = string.Empty;
            ddlTitle.SelectedIndex = 0;
            UcAddress.DropdownProvince.SelectedIndex = 0;
            UcAddress.DropdownDistrict.SelectedIndex = 0;
        }

        public void ClearControl()
        {
            ddlTitle.SelectedIndex = 0;
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtIDCard.Text = string.Empty;
            rblSex.SelectedIndex = 0;
            txtTel.Text = string.Empty;
            txtAssociationTel.Text = string.Empty;
            txtEmail.Text = string.Empty;
            UcAddress.TextBoxAddress.Text = string.Empty;
            UcAddress.DropdownProvince.SelectedIndex = 0;
            UcAddress.DropdownDistrict.SelectedIndex = 0;
            UcAddress.DropdownParish.SelectedIndex = 0;
        }

        private void EnableControl(Boolean IsEnable)
        {
            foreach (Control item in udpDetailAssoc.Controls[0].Controls)
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
            udpDetailAssoc.Update();

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
                txtAssociation.Enabled = false;
                txtAssociationRegister.Enabled = false;
                txtIDCard.Enabled = false;
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
                txtAssociation.Enabled = false;
                txtAssociationRegister.Enabled = false;
                txtIDCard.Enabled = false;
                MasterPage.PNLAddButton.Visible = false;
                MasterPage.PNLAddButton.Enabled = false;
                txtEmail.Enabled = false;
                this.MasterPage.AttachFileControl.EnableGridView(false);
                this.MasterPage.AttachFileControl.EnableUpload(false);

            }
            else
            {
                Session["Status"] = this.MasterPage.NullableString(res.DataResponse.STATUS);
                txtAssociation.Enabled = IsEnable;
                txtAssociationRegister.Enabled = IsEnable;
                txtIDCard.Enabled = IsEnable;
                txtEmail.Enabled = IsEnable;
                this.MasterPage.AttachFileControl.EnableGridView(false);
                this.MasterPage.AttachFileControl.EnableUpload(false);

            }

            //this.MasterPage.GetCurrentLicense(this.ucCurrentLicense, res.DataResponse.ID_CARD_NO);
        }

        public string MyUrlParser(Dictionary<string, string> addressInfo, Dictionary<string, string> querystringInfo)
        {
            StringBuilder sb = new StringBuilder();                         // better to use a string builder for performance and memory
            sb.Append(addressInfo["protocol"] + addressInfo["domain"]);     // assigning the protocol and domain first since that will be included in any url.
            if (!string.IsNullOrEmpty(addressInfo["subdomain"]))      // checking to see if we need to add a subdomain
            {
                sb.Append("/" + addressInfo["subdomain"]);
            }

            int querystringCount = querystringInfo.Count;
            if (querystringCount > 0)                                       // checking if we have any querystrings to add
            {
                sb.Append("?");                                             // adding the start of the querystring

                if (querystringCount > 1)                                   // checking if we need to add more than 1 querysting. If not, we skip the following block of logic and add only 1 querystring key/value pair
                {
                    int i = 0;
                    while (i < querystringCount - 1)                        // telling the loop to stop before the last value if more than 1 are found so that the correct number of "&" are added.
                    {
                        sb.Append(querystringInfo.ElementAt(i).Key + "=" + querystringInfo.ElementAt(i).Value + "&");
                        i++;
                    }
                    sb.Append(querystringInfo.ElementAt(i).Key + "=" + querystringInfo.ElementAt(i).Value);   // adding the last key/value pair after the loop has finished.
                }
                else
                {
                    sb.Append(querystringInfo.ElementAt(0).Key + "=" + querystringInfo.ElementAt(0).Value);
                }
            }
            return sb.ToString();                                          // returning our completed url
        }

        private void NewRegis()
        {
            this.MasterPage.NewRegister(DTO.RegistrationType.Association, GetAsscoEntity());
        }

        private void EditRegis()
        {
            this.MasterPage.EditRegister(DTO.RegistrationType.Association, GetAsscoEntity());
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

        private void GetLoadDataToControl(DTO.Registration asscoent)
        {
            BLL.DataCenterBiz dcbiz = new BLL.DataCenterBiz();
            BLL.PersonBiz biz = new BLL.PersonBiz();
            BLL.DataCenterBiz dataCenter = new BLL.DataCenterBiz();

            var res = biz.GetUserProfileById(this.MasterPage.UserProfile.Id);
            this.MasterPage.AttachFileControl.RegisterationId = this.MasterPage.UserProfile.Id;
            this.MasterPage.AttachFileControl.CurrentUser = this.UserProfile.LoginName;

            this.MasterPage.RegistrationId = asscoent.ID;

            if (!asscoent.MEMBER_TYPE.Equals(DTO.MemberType.Association.GetEnumValue().ToString()))
            {
                MasterPage.ModelError.ShowMessageError = SysMessage.UserMissMatchRegitrationData;
                MasterPage.ModelError.ShowModalError();
            }

            txtAssociationRegister.Text = asscoent.COMP_CODE;
            ddlTitle.SelectedValue = asscoent.PRE_NAME_CODE;
            txtFirstName.Text = asscoent.NAMES;
            txtLastName.Text = asscoent.LASTNAME;
            txtIDCard.Text = asscoent.ID_CARD_NO;
            txtIDCard.Enabled = false;
            rblSex.Text = asscoent.SEX;
            txtAssociationTel.Text = LocalTelephoneNumberHelper.GetPhoneNumber(asscoent.LOCAL_TELEPHONE);
            txtAssociationTelExt.Text = LocalTelephoneNumberHelper.GetExtenNumber(asscoent.LOCAL_TELEPHONE);
            txtTel.Text = LocalTelephoneNumberHelper.GetPhoneNumber(asscoent.TELEPHONE);
            txtTelExt.Text = LocalTelephoneNumberHelper.GetExtenNumber(asscoent.TELEPHONE);

        
            txtEmail.Text = asscoent.EMAIL;
            txtEmail.Enabled = false;
            UcAddress.TextBoxAddress.Text = asscoent.ADDRESS_1;
            UcAddress.TextBoxPostCode.Text = asscoent.ZIP_CODE;

            //var strName = dcbiz.GetAssociateNameById(asscoent.COMP_CODE);
            DTO.ResponseService<DTO.ASSOCIATION> strName = dcbiz.GetInsuranceAssociateNameByID(asscoent.COMP_CODE);
            if (strName.DataResponse != null)
            {
                txtAssociation.Text = strName.DataResponse.ASSOCIATION_NAME + " " + "[" + asscoent.COMP_CODE + "]";
            }
            else
            {
                txtAssociation.Text = "Name[Null]" + " " + "[" + asscoent.COMP_CODE + "]";
            }

            this.MasterPage.TextBoxResultReg.Text = asscoent.APPROVE_RESULT;

            UcAddress.SelectDropDownStep(asscoent.PROVINCE_CODE, asscoent.AREA_CODE, asscoent.TUMBON_CODE);
        }

        public DTO.Registration GetAsscoEntity()
        {
            DTO.Registration asscoent = new Registration();

            //var attachFiles = this.MasterPage.AttachFileControl.AttachFiles;
            string strFullAssociaName = txtAssociation.Text;
            string[] strFullAssociaNameArray = strFullAssociaName.Split('[');
            string strAssociaNameTrim = strFullAssociaNameArray[0].Trim();
            asscoent.ID = this.MasterPage.RegistrationId;
            asscoent.COMP_CODE = strFullAssociaNameArray[1].Replace("]", "").Trim();
            asscoent.Company_Name = strAssociaNameTrim;
            asscoent.MEMBER_TYPE = DTO.MemberType.Association.GetEnumValue().ToString();
            asscoent.ID_CARD_NO = txtIDCard.Text;
            asscoent.PRE_NAME_CODE = ddlTitle.SelectedValue;
            asscoent.NAMES = txtFirstName.Text;
            asscoent.LASTNAME = txtLastName.Text;
            asscoent.SEX = rblSex.SelectedValue;
            asscoent.EMAIL = txtEmail.Text;
            asscoent.LOCAL_TELEPHONE = txtAssociationTel.Text + ((String.IsNullOrWhiteSpace(txtAssociationTelExt.Text)) ? "" : ("#" + txtAssociationTelExt.Text.Trim()));// txtAssociationTel.Text;
            asscoent.TELEPHONE = txtTel.Text + ((String.IsNullOrWhiteSpace(txtTelExt.Text)) ? "" : ("#" + txtTelExt.Text.Trim()));

            asscoent.ADDRESS_1 = this.UcAddress.TextBoxAddress.Text;
            asscoent.PROVINCE_CODE = this.UcAddress.DropdownProvince.SelectedValue;
            asscoent.AREA_CODE = this.UcAddress.DropdownDistrict.SelectedValue;
            asscoent.TUMBON_CODE = this.UcAddress.DropdownParish.SelectedValue;
            asscoent.ZIP_CODE = this.UcAddress.TextBoxPostCode.Text;
            asscoent.CREATED_BY = "AGDOI";
            asscoent.CREATED_DATE = DateTime.Now;
            asscoent.UPDATED_BY = "AGDOI";
            asscoent.UPDATED_DATE = DateTime.Now;
            asscoent.REG_PASS = this.MasterPage.TextBoxPassword.Text;
            asscoent.IMPORT_STATUS = this.MasterPage.NullableString(this.MasterPage.ImportStatus);

            return asscoent;

        }

        public DTO.ResponseMessage<bool> EMailValidation()
        {
            //var res = new DTO.ResponseMessage<bool>();
            BLL.RegistrationBiz biz = new BLL.RegistrationBiz();
            DTO.Registration ent = new Registration();
            ent.ID_CARD_NO = txtIDCard.Text;
            ent.MEMBER_TYPE = DTO.MemberType.Association.GetEnumValue().ToString();
            ent.NAMES = txtFirstName.Text;
            ent.LASTNAME = txtLastName.Text;
            ent.EMAIL = txtEmail.Text;
            DTO.ResponseMessage<bool> res = biz.EntityValidation(DTO.RegistrationType.Association, ent);
            //DTO.ResponseMessage<bool> res = biz.EntityValidation(DTO.RegistrationType.Association, GetAsscoEntity());

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
            (this.MasterPage as MasterRegister).OkRegister_Click += new EventHandler(RegisAssociate_OkRegister_Click);
            (this.MasterPage as MasterRegister).CancelRegister_Click += new EventHandler(RegisAssociate_CancelRegister_Click);
            (this.MasterPage as MasterRegister).CheckAgreement_CheckedChanged += new EventHandler(RegisAssociate_CheckAgreement_CheckedChanged);
            (this.MasterPage as MasterRegister).SaveRegiter_Click += new EventHandler(RegisAssociate_SaveRegiter_Click);

        }

        void RegisAssociate_OkRegister_Click(object sender, EventArgs e)
        {
            //Call MasterPage.OkRegister_Click
            txtEmail.Enabled = false;
            txtIDCard.Enabled = false;
        }

        void RegisAssociate_CancelRegister_Click(object sender, EventArgs e)
        {
            //Call MasterPage.CancelRegister_Click
        }

        void RegisAssociate_CheckAgreement_CheckedChanged(object sender, EventArgs e)
        {
            //Call MasterPage.CheckAgreement_CheckedChanged
        }

        void RegisAssociate_SaveRegiter_Click(object sender, EventArgs e)
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

            txtAssociationRegister.Text = compCode[1];

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
        /// Get&Set Old Persoanl Data from AG_PERSONAL_T by ID_CARD_NO
        /// NT@13/2/2557 & Last Edited
        /// </summary>
        /// <param name="general"></param>
        //private void GetPersonalDataToControl(DTO.Person asscoent)
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

        //    /////
        //    txtAssociationRegister.Text = asscoent.COMP_CODE;
        //    ddlTitle.SelectedValue = asscoent.PRE_NAME_CODE;
        //    txtFirstName.Text = asscoent.NAMES;
        //    txtLastName.Text = asscoent.LASTNAME;
        //    txtIDCard.Text = asscoent.ID_CARD_NO;
        //    //txtIDCard.Enabled = false;
        //    rblSex.Text = asscoent.SEX;
        //    txtAssociationTel.Text = LocalTelephoneNumberHelper.GetPhoneNumber(asscoent.LOCAL_TELEPHONE);
        //    txtAssociationTelExt.Text = LocalTelephoneNumberHelper.GetExtenNumber(asscoent.LOCAL_TELEPHONE);
        //    txtTel.Text = LocalTelephoneNumberHelper.GetPhoneNumber(asscoent.TELEPHONE);
        //    txtTelExt.Text = LocalTelephoneNumberHelper.GetExtenNumber(asscoent.TELEPHONE);

        //    txtEmail.Text = asscoent.EMAIL;
        //    //txtEmail.Enabled = false;
        //    UcAddress.TextBoxAddress.Text = asscoent.ADDRESS_1;
        //    UcAddress.TextBoxPostCode.Text = asscoent.ZIP_CODE;
        //    txtAssociation.Text = asscoent.COMP_CODE;

        //    //AG_PERSONAL_T.AREA_CODE = PROVINCECODE(2)+AMPURCODE(2)+TUMBON(4)
        //    if ((asscoent.AREA_CODE != null) && (asscoent.AREA_CODE != ""))
        //    {
        //        if (asscoent.AREA_CODE.Length > 2)
        //        {
        //            string province = asscoent.AREA_CODE.Substring(0, 2);
        //            string district = asscoent.AREA_CODE.Substring(2, 2);
        //            string tumbon = asscoent.AREA_CODE.Substring(4);

        //            UcAddress.SelectDropDownStep(province, district, tumbon);
        //        }
        //        else
        //        {
        //            string province = this.MasterPage.NullableString(asscoent.PROVINCE_CODE);
        //            string district = this.MasterPage.NullableString(asscoent.AREA_CODE);
        //            string tumbon = this.MasterPage.NullableString(asscoent.TUMBON_CODE);

        //            UcAddress.SelectDropDownStep(province, district, tumbon);
        //        }
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

        //        //Rebind for set to Default value
        //        //if not > selected index error
        //        //UcAddress.SelectDropDownStep("", "", "");

        //        //Clear All text in Control
        //        //ctrlHelper.ClearInput(this);
        //    }
        //}

        //protected void btnImgIDCardValidate_Click(object sender, ImageClickEventArgs e)
        //{
            
        //    RegistrationBiz biz = new RegistrationBiz();

        //    if (txtIDCard.Text.Equals(""))
        //    {
        //        this.MasterPage.ModelError.ShowMessageError = IDCardRequired.ErrorMessage;
        //        this.MasterPage.ModelError.ShowModalError();
        //        return;
        //    }
        //    else
        //    {
        //        ResponseService<DTO.Person> res = biz.GetPersonalDetailByIDCard(txtIDCard.Text.Trim());

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

        //public void OnConfirm(object sender, EventArgs e)
        //{
        //    string confirmValue = Request.Form["confirm_value"];
        //    string mystring = confirmValue;
        //    string res = mystring.Substring(Math.Max(0, mystring.Length - 1));

        //    if (res == "Y")
        //    {
        //        //IF say Yes
        //        GetPersonalDataToControl(DTO.PersonalT.per);
        //        udpDetailAssoc.Update();
        //        this.MasterPage.UpdatePanelMaster.Update();
        //    }
        //    if (res == "N")
        //    {
        //        //IF say No

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
        //            udpDetailAssoc.Update();
        //            this.MasterPage.UpdatePanelMaster.Update();
        //            this.MasterPage.ImportStatus = "Y";


        //            //Disable Control
        //            this.pnlIDCardValidate.Enabled = false;
        //            this.chkEnableEditName.Checked = false;
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
        //            udpDetailAssoc.Update();
        //            this.MasterPage.UpdatePanelMaster.Update();
        //            this.MasterPage.ImportStatus = "Y";


        //            //Disable Control
        //            this.pnlIDCardValidate.Enabled = false;
        //            this.chkEnableEditName.Checked = false;
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
        //            udpDetailAssoc.Update();
        //            this.MasterPage.UpdatePanelMaster.Update();
        //            this.MasterPage.ImportStatus = "Y";


        //            //Disable Control
        //            this.pnlIDCardValidate.Enabled = false;
        //            this.chkEnableEditName.Checked = false;
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
