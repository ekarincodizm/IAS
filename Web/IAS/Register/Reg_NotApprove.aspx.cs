using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using IAS.Utils;

using System.Text;
using IAS.DTO;
using AjaxControlToolkit;
using System.IO;
using IAS.BLL.AttachFilesIAS;
using IAS.BLL.AttachFilesIAS.States;
using IAS.Properties;

namespace IAS.Register
{
    public partial class Reg_NotApprove : System.Web.UI.Page
    {
        ////ยังไม่ทำการอนุมัติ(สมัคร)
        private string waitApprove = Resources.propReg_NotApprove_waitApprove;
        ////ไม่อนุมัติ(สมัคร)
        private string notApprove = Resources.propReg_NotApprove_notApprove;

        //public List<DTO.RegistrationAttatchFile> AttachFiles
        //{
        //    get
        //    {
        //        if (Session["AttatchFiles"] == null)
        //        {
        //            Session["AttatchFiles"] = new List<DTO.RegistrationAttatchFile>();
        //        }

        //        return (List<DTO.RegistrationAttatchFile>)Session["AttatchFiles"];
        //    }

        //    set
        //    {
        //        Session["AttatchFiles"] = value;
        //    }
        //}

        //public List<DTO.PersonAttatchFile> PersonAttachFiles
        //{
        //    get
        //    {
        //        if (Session["PersonAttachFiles"] == null)
        //        {
        //            Session["PersonAttachFiles"] = new List<DTO.PersonAttatchFile>();
        //        }

        //    }
        //}

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

        public string UserID
        {
            get
            {
                return (string)Session["UserID"];
            }

        }

        public string RegisterationID
        {
            get
            {
                return (string)Session["RegisterationID"];
            }
        }

        public string TempFolderOracle
        {
            get
            {
                return (string)Session["TempFolderOracle"];
            }
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

        public string MememberTypeGuest
        {
            get
            {
                return Session["MememberTypeGuest"].ToString();
            }
        }

        public string CompCode
        {
            get { return Session["CompCode"] == null ? string.Empty : Session["CompCode"].ToString(); }
            set { Session["CompCode"] = value; }
        }

        string mapPath;


        protected void Page_Load(object sender, EventArgs e)
        {
            mapPath = WebConfigurationManager.AppSettings["UploadFilePath"];

            txtIDNumber.Attributes.Add("onblur", "javascript:return checkUser(" + txtIDNumber.ClientID + ");");

            txtEmail.Attributes.Add("onblur", "javascript:return checkemail(" + txtEmail.ClientID + ");");

            txtIDNumber.Attributes.Add("onkeydown", "javascript:return checkKeyNumberFixPoint(event, this, 13);");
            txtIDNumber.Attributes.Add("onkeypress", "javascript:return checkKeyNumberKeyPressFixPoint(event, this, 13);");

            txtTel.Attributes.Add("onkeydown", "javascript:return checkKeyNumberFixPoint(event, this, 15);");
            txtTel.Attributes.Add("onkeypress", "javascript:return checkKeyNumberKeyPressFixPoint(event, this, 15);");


            txtMobilePhone.Attributes.Add("onkeydown", "javascript:return checkKeyNumberFixPoint(event, this, 15);");
            txtMobilePhone.Attributes.Add("onkeypress", "javascript:return checkKeyNumberKeyPressFixPoint(event, this, 15);");

            UcCurrentAdd.TextBoxAddress.Attributes.Add("onkeydown", "javascript:return checkKeyNumberFixPoint(event, this, 5);");
            UcCurrentAdd.TextBoxAddress.Attributes.Add("onkeypress", "javascript:return checkKeyNumberKeyPressFixPoint(event, this, 5);");
            //txtPostcodeCurrentAddress.Attributes.Add("onkeydown", "javascript:return checkKeyNumberFixPoint(event, this, 5);");
            //txtPostcodeCurrentAddress.Attributes.Add("onkeypress", "javascript:return checkKeyNumberKeyPressFixPoint(event, this, 5);");

            //txtPostcodeRegisterAddress.Attributes.Add("onkeydown", "javascript:return checkKeyNumberFixPoint(event, this, 5);");
            //txtPostcodeRegisterAddress.Attributes.Add("onkeypress", "javascript:return checkKeyNumberKeyPressFixPoint(event, this, 5);");
            UcRegisAdd.TextBoxAddress.Attributes.Add("onkeydown", "javascript:return checkKeyNumberFixPoint(event, this, 5);");
            UcRegisAdd.TextBoxAddress.Attributes.Add("onkeypress", "javascript:return checkKeyNumberKeyPressFixPoint(event, this, 5);");

            if (!Page.IsPostBack)
            {
                string Mode = DataActionMode.View.ToString();
                ucAttachFileControl1.ModeForm = DataActionMode.View;
                if (Mode != null)
                {
                    ClearControl();


                    //UserProfile usr = new UserProfile();
                    if (this.UserProfile.MemberType.Equals(1))
                    {

                        Session["MememberTypeGuest"] = this.UserProfile.MemberType;
                        txtTypeMember.Text = Resources.propReg_NotApprove_MemberTypeGeneral;
                        txtTypeMember.Enabled = false;

                        GetProvince();
                        GetTitle();
                        GetEducation();
                        GetNationality();
                        GetAttachFilesType();

                        InitData();
                        GetAttatchFiles();

                        //BindDataInGridView();
                        GetAttachFilesTypeImage();

                        this.DataAction = DTO.DataActionMode.View;
                        pnlMain.Enabled = false;

                        ChkMode(Mode);
                        StatusInit();
                    }
                    else if (this.UserProfile.MemberType.Equals(2))
                    {
                        Session["MememberTypeGuest"] = this.UserProfile.MemberType;
                        txtTypeMember.Text = Resources.propReg_Co_MemberTypeCompany;
                        txtTypeMember.Enabled = false;

                        GetProvince();
                        GetTitle();
                        GetEducation();
                        GetNationality();
                        GetAttachFilesType();

                        InitData();
                        GetAttatchFiles();

                        //BindDataInGridView();
                        GetAttachFilesTypeImage();

                        this.DataAction = DTO.DataActionMode.View;
                        pnlMain.Enabled = false;
                        ChkMode(Mode);
                        StatusInit();
                    }
                    else if (this.UserProfile.MemberType.Equals(3))
                    {
                        Session["MememberTypeGuest"] = this.UserProfile.MemberType;
                        txtTypeMember.Text = Resources.propReg_Assoc_MemberTypeAssoc;
                        txtTypeMember.Enabled = false;

                        GetProvince();
                        GetTitle();
                        GetEducation();
                        GetNationality();
                        GetAttachFilesType();

                        InitData();
                        GetAttatchFiles();

                        //BindDataInGridView();
                        GetAttachFilesTypeImage();

                        this.DataAction = DTO.DataActionMode.View;
                        pnlMain.Enabled = false;
                        ChkMode(Mode);
                        StatusInit();
                    }

                    //Session["MememberTypeGuest"] = "1";
                    ////Session["TempFolderOracle"] = IAS.BLL.Helpers.GenerateIdHelper.GetGenAutoId();
                    //if (this.MememberTypeGuest == "1")
                    //{
                    //    txtTypeMember.Text = "บุคคลทั่วไป";
                    //    txtTypeMember.Enabled = false;
                    //}

                    //GetProvince();
                    //GetTitle();
                    //GetEducation();
                    //GetNationality();
                    //GetAttachFilesType();

                    //InitData();
                    //GetAttatchFiles();

                    //BindDataInGridView();
                    //GetAttachFilesTypeImage();



                    ////PnlEditButton.Visible = true;


                    //this.DataAction = DTO.DataActionMode.View;
                    //pnlMain.Enabled = false;

                    ucAttachFileControl1.BindAll();
                }
            }
        }

        /// <summary>
        /// Check สถานะการอนุมัติ
        /// ยังไม่ทำการอนุมัติ(สมัคร) > IS_APPROVE="N" && STATUS=1
        /// และ ไม่อนุมัติ(สมัคร) > IS_APPROVE="N" && STATUS=3 
        /// </summary>
        private void StatusInit()
        {

            DTO.UserProfile usrProfile = (DTO.UserProfile)Session[PageList.UserProfile];

            if (usrProfile.IS_APPROVE.Equals("N"))
            {

                ResponseService<Registration> res;
                using (BLL.RegistrationBiz biz = new BLL.RegistrationBiz())
                {
                    res = biz.GetById(this.UserProfile.Id);
                }

                if (!res.IsError)
                {
                    if (res.DataResponse.STATUS == DTO.RegistrationStatus.WaitForApprove.GetEnumValue().ToString())
                    {
                        lblMessage.Text = this.waitApprove;
                        lblMessage.Visible = true;
                    }
                    else if (res.DataResponse.STATUS == DTO.RegistrationStatus.NotApprove.GetEnumValue().ToString())
                    {
                        lblMessage.Text = this.notApprove;
                        lblMessage.Visible = true;

                    }

                }

            }

        }

        private void ChkMode(string Mode)
        {
            if (Mode.Equals("View"))
            {
                this.DataAction = DTO.DataActionMode.View;
                pnlMain.Enabled = false;

                //Add new 




            }
        }

        private void InitData()
        {
            ResponseService<Registration> res;

            using (BLL.RegistrationBiz biz = new BLL.RegistrationBiz())
            {
                res = biz.GetById(this.UserProfile.Id);
            }


            if (!res.IsError)
            {
                var registeration = res.DataResponse;

                if (registeration.STATUS == "3")
                {
                    PnlAddButton.Visible = true;
                }
                else
                {
                    PnlAddButton.Visible = false;
                }

                Session["UserID"] = this.UserProfile.Id;

                Session["RegisterationID"] = registeration.ID;

                ucAttachFileControl1.RegisterationId = this.RegisterationID;
                ucAttachFileControl1.CurrentUser = this.UserProfile.Id;

                ddlTitle.SelectedValue = registeration.PRE_NAME_CODE;
                txtFirstName.Text = registeration.NAMES;
                txtLastName.Text = registeration.LASTNAME;
                txtIDNumber.Text = registeration.ID_CARD_NO;
                txtIDNumber.Enabled = false;
                //Added By Nattapong @23/8/56
                if (registeration.BIRTH_DATE != null)
                {
                    txtBirthDay.CssClass = "txt";
                    txtBirthDay.Text = registeration.BIRTH_DATE.Value.ToString("dd/MM/yyyy");
                }
                else
                {
                    txtBirthDay.CssClass = "txt";
                    txtBirthDay.Text = "-";
                }

                rblSex.SelectedValue = registeration.SEX;
                if (registeration.NATIONALITY != null)
                {
                    ddlNationality.SelectedValue = registeration.NATIONALITY;
                }

                ddlEducation.SelectedValue = registeration.EDUCATION_CODE;
                txtEmail.Text = registeration.EMAIL;
                txtTel.Text = registeration.LOCAL_TELEPHONE;
                txtMobilePhone.Text = registeration.TELEPHONE;
                UcCurrentAdd.TextBoxAddress.Text = registeration.ADDRESS_1;

                var message = SysMessage.DefaultSelecting;

                BLL.DataCenterBiz dataCenter = new BLL.DataCenterBiz();
                UcCurrentAdd.DropdownProvince.SelectedValue = registeration.PROVINCE_CODE;
                var lsPC = dataCenter.GetAmpur(message, UcCurrentAdd.DropdownProvince.SelectedValue);
                BindToDDL(UcCurrentAdd.DropdownDistrict, lsPC);

                UcCurrentAdd.DropdownDistrict.SelectedValue = registeration.AREA_CODE;
                var lsTC = dataCenter.GetTumbon(message, UcCurrentAdd.DropdownProvince.SelectedValue, UcCurrentAdd.DropdownDistrict.SelectedValue);
                BindToDDL(UcCurrentAdd.DropdownParish, lsTC);

                UcCurrentAdd.DropdownParish.SelectedValue = registeration.TUMBON_CODE;

                UcCurrentAdd.TextBoxAddress.Text = registeration.ZIP_CODE;
                UcRegisAdd.TextBoxAddress.Text = registeration.LOCAL_ADDRESS1;

                UcRegisAdd.DropdownProvince.SelectedValue = registeration.LOCAL_PROVINCE_CODE;
                var lsPR = dataCenter.GetAmpur(message, UcRegisAdd.DropdownProvince.SelectedValue);
                BindToDDL(UcRegisAdd.DropdownDistrict, lsPR);

                UcRegisAdd.DropdownDistrict.SelectedValue = registeration.LOCAL_AREA_CODE;
                var lsTR = dataCenter.GetTumbon(message, UcRegisAdd.DropdownProvince.SelectedValue, UcRegisAdd.DropdownDistrict.SelectedValue);
                BindToDDL(UcRegisAdd.DropdownParish, lsTR);

                UcRegisAdd.DropdownParish.SelectedValue = registeration.LOCAL_TUMBON_CODE;

                UcRegisAdd.TextBoxAddress.Text = registeration.LOCAL_ZIPCODE;

                txtResultReg.Text = registeration.APPROVE_RESULT;
            }
            else
            {
                UCModalError.ShowMessageError = res.ErrorMsg;
                UCModalError.ShowModalError();
            }

        }

        private void GetAttatchFiles()
        {
            ResponseService<RegistrationAttatchFile[]> res;
            string personID = this.UserProfile.Id;

            using (BLL.RegistrationBiz biz = new BLL.RegistrationBiz())
            {
                res = biz.GetAttatchFilesByRegisterationID(this.RegisterationID);
            }


            List<RegistrationAttatchFile> list = res.DataResponse.ToList();


            ucAttachFileControl1.AttachFiles = list.ConvertToAttachFilesView();

            UpdatePanelUpload.Update();
        }

        private bool ValidDateInput()
        {
            StringBuilder message = new StringBuilder();
            StringBuilder messageOther = new StringBuilder();
            bool isFocus = false;

            if (ddlTitle.SelectedValue.Length < 1 && ddlTitle.SelectedIndex == 0)
            {
                if (message.Length > 0)
                {
                    message.Append(", ");
                }
                message.Append(lblTitle.Text);
                if (!isFocus)
                {
                    ddlTitle.Focus();
                    isFocus = true;
                }
            }

            if (string.IsNullOrEmpty(txtFirstName.Text) && txtFirstName.Text.Length < 1)
            {
                if (message.Length > 0)
                {
                    message.Append(", ");
                }
                message.Append(lblFirstName.Text);
                if (!isFocus)
                {
                    txtFirstName.Focus();
                    isFocus = true;
                }
            }

            if (string.IsNullOrEmpty(txtLastName.Text) && txtLastName.Text.Length < 1)
            {
                if (message.Length > 0)
                {
                    message.Append(", ");
                }
                message.Append(lblLastName.Text);
                if (!isFocus)
                {
                    txtLastName.Focus();
                    isFocus = true;
                }
            }

            if (string.IsNullOrEmpty(txtIDNumber.Text) && txtIDNumber.Text.Length < 1)
            {
                if (message.Length > 0)
                {
                    message.Append(", ");
                }
                message.Append(lblIDNumber.Text);
                if (!isFocus)
                {
                    txtIDNumber.Focus();
                    isFocus = true;
                }
            }

            if (string.IsNullOrEmpty(txtBirthDay.Text) && txtBirthDay.Text.Length < 1)
            {
                if (message.Length > 0)
                {
                    message.Append(", ");
                }
                message.Append(lblBirthDay.Text);
                if (!isFocus)
                {
                    txtBirthDay.Focus();
                    isFocus = true;
                }
            }

            if (ddlEducation.SelectedValue.Length < 1 && ddlEducation.SelectedIndex == 0)
            {
                if (message.Length > 0)
                {
                    message.Append(", ");
                }
                message.Append(lblEducation.Text);
                if (!isFocus)
                {
                    ddlEducation.Focus();
                    isFocus = true;
                }
            }

            if (ddlNationality.SelectedValue.Length < 1 && ddlNationality.SelectedIndex == 0)
            {
                if (message.Length > 0)
                {
                    message.Append(", ");
                }
                message.Append(lblNationality.Text);
                if (!isFocus)
                {
                    ddlNationality.Focus();
                    isFocus = true;
                }
            }

            if (string.IsNullOrEmpty(txtMobilePhone.Text) && txtMobilePhone.Text.Length < 1)
            {
                if (message.Length > 0)
                {
                    message.Append(", ");
                }
                message.Append(lblMobilePhone.Text);
                if (!isFocus)
                {
                    txtMobilePhone.Focus();
                    isFocus = true;
                }
            }

            if (string.IsNullOrEmpty(txtEmail.Text) && txtEmail.Text.Length < 1)
            {
                if (message.Length > 0)
                {
                    message.Append(", ");
                }
                message.Append(lblEmail.Text);
                if (!isFocus)
                {
                    txtEmail.Focus();
                    isFocus = true;
                }
            }

            if (string.IsNullOrEmpty(txtTypeMember.Text) && txtTypeMember.Text.Length < 1)
            {
                if (message.Length > 0)
                {
                    message.Append(", ");
                }
                message.Append(lblTypeMember.Text);
                if (!isFocus)
                {
                    txtTypeMember.Focus();
                    isFocus = true;
                }
            }

            if (string.IsNullOrEmpty(UcCurrentAdd.TextBoxAddress.Text) && UcCurrentAdd.TextBoxAddress.Text.Length < 1)
            {
                if (message.Length > 0)
                {
                    message.Append(", ");
                }
                //message.Append(lblCurrentAddress.Text);
                //if (!isFocus)
                //{
                //    txtCurrentAddress.Focus();
                //    isFocus = true;
                //}
            }

            if (UcCurrentAdd.DropdownProvince.SelectedValue.Length < 1 && UcCurrentAdd.DropdownProvince.SelectedIndex == 0)
            {
                if (message.Length > 0)
                {
                    message.Append(", ");
                }
                //message.Append(lblProvinceCurrentAddress.Text);
                //if (!isFocus)
                //{
                //    UcCurrentAdd.DropdownProvince.Focus();
                //    isFocus = true;
                //}
            }

            if (UcCurrentAdd.DropdownDistrict.SelectedValue.Length < 1 && UcCurrentAdd.DropdownDistrict.SelectedIndex == 0)
            {
                if (message.Length > 0)
                {
                    message.Append(", ");
                }
                //message.Append(lblDistrictCurrentAddress.Text);
                //if (!isFocus)
                //{
                //    UcCurrentAdd.DropdownDistrict.Focus();
                //    isFocus = true;
                //}
            }

            if (UcCurrentAdd.DropdownParish.SelectedValue.Length < 1 && UcCurrentAdd.DropdownParish.SelectedIndex == 0)
            {
                if (message.Length > 0)
                {
                    message.Append(", ");
                }
                //message.Append(lblParishCurrentAddress.Text);
                //if (!isFocus)
                //{
                //    UcCurrentAdd.DropdownParish.Focus();
                //    isFocus = true;
                //}
            }

            if (string.IsNullOrEmpty(UcCurrentAdd.TextBoxPostCode.Text) && UcCurrentAdd.TextBoxPostCode.Text.Length < 1)
            {
                if (message.Length > 0)
                {
                    message.Append(", ");
                }
                //message.Append(lblPostcodeCurrentAddress.Text);
                //if (!isFocus)
                //{
                //    txtPostcodeCurrentAddress.Focus();
                //    isFocus = true;
                //}
            }

            if (string.IsNullOrEmpty(UcRegisAdd.TextBoxAddress.Text) && UcRegisAdd.TextBoxAddress.Text.Length < 1)
            {
                if (message.Length > 0)
                {
                    message.Append(", ");
                }
                //message.Append(lblRegisterAddress.Text);
                //if (!isFocus)
                //{
                //    txtRegisterAddress.Focus();
                //    isFocus = true;
                //}
            }

            if (UcRegisAdd.DropdownProvince.SelectedValue.Length < 1 && UcRegisAdd.DropdownProvince.SelectedIndex == 0)
            {
                if (message.Length > 0)
                {
                    message.Append(", ");
                }
                //message.Append(lblProvinceRegisterAddress.Text);
                //if (!isFocus)
                //{
                //    UcRegisAdd.DropdownProvince.Focus();
                //    isFocus = true;
                //}
            }

            if (UcRegisAdd.DropdownDistrict.SelectedValue.Length < 1 && UcRegisAdd.DropdownDistrict.SelectedIndex == 0)
            {
                if (message.Length > 0)
                {
                    message.Append(", ");
                }
                //message.Append(lblDistrictRegisterAddress.Text);
                //if (!isFocus)
                //{
                //    UcRegisAdd.DropdownDistrict.Focus();
                //    isFocus = true;
                //}
            }

            if (UcRegisAdd.DropdownParish.SelectedValue.Length < 1 && UcRegisAdd.DropdownParish.SelectedIndex == 0)
            {
                if (message.Length > 0)
                {
                    message.Append(", ");
                }
                //message.Append(lblParishRegisterAddress.Text);
                //if (!isFocus)
                //{
                //    UcRegisAdd.DropdownParish.Focus();
                //    isFocus = true;
                //}
            }

            if (string.IsNullOrEmpty(UcRegisAdd.TextBoxPostCode.Text) && UcRegisAdd.TextBoxPostCode.Text.Length < 1)
            {
                if (message.Length > 0)
                {
                    message.Append(", ");
                }
                //message.Append(lblProvinceRegisterAddress.Text);
                //if (!isFocus)
                //{
                //    txtPostcodeRegisterAddress.Focus();
                //    isFocus = true;
                //}
            }

            if (message.Length > 0)
            {
                UCModalError.ShowMessageError = message.ToString();
                UCModalError.ShowModalError();

                return false;
            }
            if (messageOther.Length > 0)
            {
                UCModalError.ShowMessageError = messageOther.ToString();
                UCModalError.ShowModalError();

                txtFirstName.Focus();
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
                //AlertMessage.ShowAlertMessage(string.Empty, SysMessage.EmailErrorFormat);

                UCModalError.ShowMessageError = SysMessage.EmailErrorFormat;
                UCModalError.ShowModalError();

                return false;
            }

        }

        protected void btnEdit_OnClick(object sender, EventArgs e)
        {
            this.DataAction = DataActionMode.Edit;
            ucAttachFileControl1.ModeForm = DataActionMode.Edit;
            ucAttachFileControl1.BindAttachFile();

            pnlMain.Enabled = true;

            PnlEditButton.Visible = true;

            PnlAddButton.Visible = false;

            txtBirthDay.CssClass = "datepicker";
        }

        protected void btnOkEdit_Click(object sender, EventArgs e)
        {
            //บุคคลธรรมดา
            if (this.UserProfile.MemberType.ToString() == DTO.MemberType.General.GetEnumValue().ToString())
            {
                if (!ValidDateInput())
                {
                    return;
                }

                Registration item = new Registration();

                //var attachFiles = this.AttachFiles;
                //var personAttachFiles = this.PersonAttachFiles;

                item.ID = this.RegisterationID;

                item.MEMBER_TYPE = this.MememberTypeGuest;

                item.ID_CARD_NO = txtIDNumber.Text;
                item.PRE_NAME_CODE = ddlTitle.SelectedValue;
                item.NAMES = txtFirstName.Text;
                item.LASTNAME = txtLastName.Text;
                item.ID_CARD_NO = txtIDNumber.Text;
                item.BIRTH_DATE = Convert.ToDateTime(txtBirthDay.Text);
                item.SEX = rblSex.SelectedValue;
                item.NATIONALITY = ddlNationality.SelectedValue;
                item.EDUCATION_CODE = ddlEducation.SelectedValue;
                item.EMAIL = txtEmail.Text;
                item.LOCAL_TELEPHONE = txtTel.Text;
                item.TELEPHONE = txtMobilePhone.Text;
                item.ADDRESS_1 = UcCurrentAdd.TextBoxAddress.Text;
                item.PROVINCE_CODE = UcCurrentAdd.DropdownProvince.SelectedValue;
                item.AREA_CODE = UcCurrentAdd.DropdownDistrict.SelectedValue;
                item.TUMBON_CODE = UcCurrentAdd.DropdownParish.SelectedValue;
                item.ZIP_CODE = UcCurrentAdd.TextBoxPostCode.Text;
                item.LOCAL_ADDRESS1 = UcRegisAdd.TextBoxAddress.Text;
                item.LOCAL_PROVINCE_CODE = UcRegisAdd.DropdownProvince.SelectedValue;
                item.LOCAL_AREA_CODE = UcRegisAdd.DropdownDistrict.SelectedValue;
                item.LOCAL_TUMBON_CODE = UcRegisAdd.DropdownParish.SelectedValue;
                item.LOCAL_ZIPCODE = UcRegisAdd.TextBoxPostCode.Text;
                item.UPDATED_BY = this.UserID;
                item.UPDATED_DATE = DateTime.Now;
                item.STATUS = "1";

                if (item != null)
                {
                    BLL.RegistrationBiz biz = new BLL.RegistrationBiz();

                    var result = biz.ValidateBeforeSubmit(DTO.RegistrationType.General, item);

                    if (result.IsError)
                    {
                        UCModalError.ShowMessageError = result.ErrorMsg;
                        UCModalError.ShowModalError();
                    }
                    else
                    {
                        if (this.DataAction == DTO.DataActionMode.Edit)
                        {
                            try
                            {
                                var res = biz.Update(item, ucAttachFileControl1.AttachFiles.ConvertToRegistrationAttachFiles().ToList());

                                if (res.IsError)
                                {
                                    UCModalError.ShowMessageError = res.ErrorMsg;
                                    UCModalError.ShowModalError();
                                }
                                else
                                {
                                    Session.Remove("AttatchFiles");

                                    Session.Abandon();

                                    ClearControl();

                                    UCModalError.Visible = false;
                                    UCModalSuccess.Visible = false;
                                    BLL.DataCenterBiz dbiz = new BLL.DataCenterBiz();
                                    var r = dbiz.GetConfigApproveMember();
                                    foreach (var items in r.DataResponse)
                                    {
                                        if (items.Id == "01" && items.Value == "Y")
                                        {
                                            string AlertWaitingForApprove = "alert('"+ Resources.infoSysMessage_EditSuccess +"');window.location.assign('../home.aspx')";
                                            ToolkitScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", AlertWaitingForApprove, true);
                                        }
                                    }
                                    string Alert = "alert('"+ Resources.infoSysMessage_RegisSuccess2 +"');window.location.assign('../home.aspx')";
                                    ToolkitScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", Alert, true);

                                }

                            }
                            catch (Exception ex)
                            {
                                UCModalError.ShowMessageError = ex.Message;
                                UCModalError.ShowModalError();
                            }

                        }

                    }

                }
            }
            //บริษัท ยังไม่แยก Methodเฉพาะ > var result = biz.ValidateBeforeSubmit(DTO.RegistrationType.Insurance, item);
            else if (this.UserProfile.MemberType.ToString() == DTO.MemberType.Insurance.GetEnumValue().ToString())
            {
                //if (!ValidDateInput())
                //{
                //    return;
                //}

                Registration item = new Registration();

                //var attachFiles = this.AttachFiles;
                //var personAttachFiles = this.PersonAttachFiles;

                item.COMP_CODE = this.UserProfile.CompCode;

                item.ID = this.RegisterationID;

                item.MEMBER_TYPE = this.MememberTypeGuest;

                item.ID_CARD_NO = txtIDNumber.Text;
                item.PRE_NAME_CODE = ddlTitle.SelectedValue;
                item.NAMES = txtFirstName.Text;
                item.LASTNAME = txtLastName.Text;
                item.ID_CARD_NO = txtIDNumber.Text;
                //item.BIRTH_DATE = Convert.ToDateTime(txtBirthDay.Text);
                item.SEX = rblSex.SelectedValue;
                item.NATIONALITY = ddlNationality.SelectedValue;
                //item.EDUCATION_CODE = ddlEducation.SelectedValue;
                item.EMAIL = txtEmail.Text;
                item.LOCAL_TELEPHONE = txtTel.Text;
                item.TELEPHONE = txtMobilePhone.Text;
                item.ADDRESS_1 = UcCurrentAdd.TextBoxAddress.Text;
                item.PROVINCE_CODE = UcCurrentAdd.DropdownProvince.SelectedValue;
                item.AREA_CODE = UcCurrentAdd.DropdownDistrict.SelectedValue;
                item.TUMBON_CODE = UcCurrentAdd.DropdownParish.SelectedValue;
                item.ZIP_CODE = UcCurrentAdd.TextBoxPostCode.Text;
                //item.LOCAL_ADDRESS1 = txtRegisterAddress.Text;
                //item.LOCAL_PROVINCE_CODE = UcRegisAdd.DropdownProvince.SelectedValue;
                //item.LOCAL_AREA_CODE = UcRegisAdd.DropdownDistrict.SelectedValue;
                //item.LOCAL_TUMBON_CODE = UcRegisAdd.DropdownParish.SelectedValue;
                //item.LOCAL_ZIPCODE = txtPostcodeRegisterAddress.Text;
                item.UPDATED_BY = this.UserID;
                item.UPDATED_DATE = DateTime.Now;
                item.STATUS = DTO.RegistrationStatus.WaitForApprove.GetEnumValue().ToString();

                if (item != null)
                {
                    BLL.RegistrationBiz biz = new BLL.RegistrationBiz();

                    var result = biz.ValidateBeforeSubmit(DTO.RegistrationType.General, item);

                    if (result.IsError)
                    {
                        UCModalError.ShowMessageError = result.ErrorMsg;
                        UCModalError.ShowModalError();
                    }
                    else
                    {
                        if (this.DataAction == DTO.DataActionMode.Edit)
                        {
                            try
                            {
                                var res = biz.Update(item, ucAttachFileControl1.AttachFiles.ConvertToRegistrationAttachFiles().ToList());

                                if (res.IsError)
                                {
                                    UCModalError.ShowMessageError = res.ErrorMsg;
                                    UCModalError.ShowModalError();
                                }
                                else
                                {
                                    Session.Remove("AttatchFiles");

                                    Session.Abandon();

                                    ClearControl();

                                    UCModalError.Visible = false;
                                    UCModalSuccess.Visible = false;
                                    BLL.DataCenterBiz dbiz = new BLL.DataCenterBiz();
                                    var r = dbiz.GetConfigApproveMember();
                                    foreach (var items in r.DataResponse)
                                    {
                                        if (items.Id == "01" && items.Value == "Y")
                                        {
                                            string AlertWaitingForApprove = "alert('"+ Resources.infoSysMessage_EditSuccess +"');window.location.assign('../home.aspx')";
                                            ToolkitScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", AlertWaitingForApprove, true);
                                        }
                                    }
                                    string Alert = "alert('"+ Resources.infoSysMessage_RegisSuccess2 +"');window.location.assign('../home.aspx')";
                                    ToolkitScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", Alert, true);

                                }

                            }
                            catch (Exception ex)
                            {
                                UCModalError.ShowMessageError = ex.Message;
                                UCModalError.ShowModalError();
                            }

                        }

                    }

                }
                else
                {
                    UCModalError.ShowMessageError = SysMessage.TryAgain;
                    UCModalError.ShowModalError();
                }

            }
            //สมาคม ยังไม่แยก Methodเฉพาะ > var result = biz.ValidateBeforeSubmit(DTO.RegistrationType.Association, item);
            else if (this.UserProfile.MemberType.ToString() == DTO.MemberType.Association.GetEnumValue().ToString())
            {
                //if (!ValidDateInput())
                //{
                //    return;
                //}

                Registration item = new Registration();

                //var attachFiles = this.AttachFiles;
                //var personAttachFiles = this.PersonAttachFiles;

                item.ID = this.RegisterationID;

                item.MEMBER_TYPE = this.MememberTypeGuest;

                item.ID_CARD_NO = txtIDNumber.Text;
                item.PRE_NAME_CODE = ddlTitle.SelectedValue;
                item.NAMES = txtFirstName.Text;
                item.LASTNAME = txtLastName.Text;
                item.ID_CARD_NO = txtIDNumber.Text;
                //item.BIRTH_DATE = Convert.ToDateTime(txtBirthDay.Text);
                item.SEX = rblSex.SelectedValue;
                item.NATIONALITY = ddlNationality.SelectedValue;
                //item.EDUCATION_CODE = ddlEducation.SelectedValue;
                item.EMAIL = txtEmail.Text;
                item.LOCAL_TELEPHONE = txtTel.Text;
                item.TELEPHONE = txtMobilePhone.Text;
                item.ADDRESS_1 = UcCurrentAdd.TextBoxAddress.Text;
                item.PROVINCE_CODE = UcCurrentAdd.DropdownProvince.SelectedValue;
                item.AREA_CODE = UcCurrentAdd.DropdownDistrict.SelectedValue;
                item.TUMBON_CODE = UcCurrentAdd.DropdownParish.SelectedValue;
                item.ZIP_CODE = UcCurrentAdd.TextBoxPostCode.Text;
                //item.LOCAL_ADDRESS1 = txtRegisterAddress.Text;
                //item.LOCAL_PROVINCE_CODE = UcRegisAdd.DropdownProvince.SelectedValue;
                //item.LOCAL_AREA_CODE = UcRegisAdd.DropdownDistrict.SelectedValue;
                //item.LOCAL_TUMBON_CODE = UcRegisAdd.DropdownParish.SelectedValue;
                //item.LOCAL_ZIPCODE = txtPostcodeRegisterAddress.Text;
                item.UPDATED_BY = this.UserID;
                item.UPDATED_DATE = DateTime.Now;
                item.STATUS = DTO.RegistrationStatus.WaitForApprove.GetEnumValue().ToString();

                if (item != null)
                {
                    BLL.RegistrationBiz biz = new BLL.RegistrationBiz();

                    var result = biz.ValidateBeforeSubmit(DTO.RegistrationType.General, item);

                    if (result.IsError)
                    {
                        UCModalError.ShowMessageError = result.ErrorMsg;
                        UCModalError.ShowModalError();
                    }
                    else
                    {
                        if (this.DataAction == DTO.DataActionMode.Edit)
                        {
                            try
                            {
                                var res = biz.Update(item, ucAttachFileControl1.AttachFiles.ConvertToRegistrationAttachFiles().ToList());

                                if (res.IsError)
                                {
                                    UCModalError.ShowMessageError = res.ErrorMsg;
                                    UCModalError.ShowModalError();
                                }
                                else
                                {
                                    Session.Remove("AttatchFiles");

                                    Session.Abandon();

                                    ClearControl();

                                    UCModalError.Visible = false;
                                    UCModalSuccess.Visible = false;
                                    BLL.DataCenterBiz dbiz = new BLL.DataCenterBiz();
                                    var r = dbiz.GetConfigApproveMember();
                                    foreach (var items in r.DataResponse)
                                    {
                                        if (items.Id == "01" && items.Value == "Y")
                                        {
                                            string AlertWaitingForApprove = "alert('"+ Resources.infoSysMessage_EditSuccess +"');window.location.assign('../home.aspx')";
                                            ToolkitScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", AlertWaitingForApprove, true);
                                        }
                                    }
                                    string Alert = "alert('"+ Resources.infoSysMessage_RegisSuccess2 +"');window.location.assign('../home.aspx')";
                                    ToolkitScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", Alert, true);

                                }

                            }
                            catch (Exception ex)
                            {
                                UCModalError.ShowMessageError = ex.Message;
                                UCModalError.ShowModalError();
                            }

                        }

                    }

                }
                else
                {
                    UCModalError.ShowMessageError = SysMessage.TryAgain;
                    UCModalError.ShowModalError();
                }

            }
            else
            {
                UCModalError.ShowMessageError = SysMessage.TryAgain;
                UCModalError.ShowModalError();
            }
        }

        protected void btnCancelEdit_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Register/Reg_NotApprove.aspx?Mode=V");
        }

        //private void BindDataInGridView()
        //{

        //        gvUpload.DataSource = this.AttachFiles;
        //        gvUpload.DataBind();

        //        UpdatePanelUpload.Update();


        //}

        public void ClearControl()
        {
            HiddenField_ID.Value = string.Empty;
            ddlTitle.SelectedIndex = 0;
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtIDNumber.Text = string.Empty;
            txtBirthDay.Text = DateUtil.dd_MMMM_yyyy_Now_TH;
            rblSex.SelectedIndex = 0;
            ddlNationality.SelectedIndex = 0;
            ddlEducation.SelectedIndex = 0;
            txtMobilePhone.Text = string.Empty;
            txtEmail.Text = string.Empty;
            UcCurrentAdd.TextBoxAddress.Text = string.Empty;
            UcCurrentAdd.DropdownProvince.SelectedIndex = 0;
            UcCurrentAdd.DropdownDistrict.SelectedIndex = 0;
            UcCurrentAdd.DropdownParish.SelectedIndex = 0;
            UcCurrentAdd.TextBoxPostCode.Text = string.Empty;
            UcRegisAdd.TextBoxAddress.Text = string.Empty;
            UcRegisAdd.DropdownProvince.SelectedIndex = 0;
            UcRegisAdd.DropdownDistrict.SelectedIndex = 0;
            UcRegisAdd.DropdownParish.SelectedIndex = 0;
            UcRegisAdd.TextBoxPostCode.Text = string.Empty;

            //Attach File//
            //ddlTypeAttachment.SelectedIndex = 0;
            //txtDetail.Text = string.Empty;

            pnlMain.DefaultButton = btnEdit.ID.ToString();
        }

        private Action<DropDownList, List<DTO.DataItem>> BindToDDL = (ddl, ls) =>
        {
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Id";
            ddl.DataSource = ls;
            ddl.DataBind();
        };

        private void GetProvince()
        {
            var message = SysMessage.DefaultSelecting;

            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetProvince(message);
            BindToDDL(UcCurrentAdd.DropdownProvince, ls);
            BindToDDL(UcRegisAdd.DropdownProvince, ls);
        }

        private void GetTitle()
        {
            var message = SysMessage.DefaultSelecting;

            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetTitleName(message);
            BindToDDL(ddlTitle, ls);

        }

        private void GetEducation()
        {
            var message = SysMessage.DefaultSelecting;

            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetEducation(message);
            BindToDDL(ddlEducation, ls);
        }

        private void GetNationality()
        {
            var message = SysMessage.DefaultSelecting;
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetNationality(message);
            BindToDDL(ddlNationality, ls);
            string code = ls.FirstOrDefault(w => w.Name == "ไทย").Id;
            ddlNationality.SelectedValue = code;
        }

        private void GetAttachFilesType()
        {
            var message = SysMessage.DefaultSelecting;
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetDocumentType(message);
            ucAttachFileControl1.DocumentTypes = ls;
        }

        private void GetAttachFilesTypeImage()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            Session["DocumentTypeIsImage"] = biz.GetDocumentTypeIsImage();
        }

        protected void ddlProvinceCurrentAddress_SelectedIndexChanged(object sender, EventArgs e)
        {
            var message = SysMessage.DefaultSelecting;

            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetAmpur(message, UcCurrentAdd.DropdownProvince.SelectedValue);
            BindToDDL(UcCurrentAdd.DropdownDistrict, ls);

            UcCurrentAdd.DropdownParish.SelectedValue = "";
        }

        protected void ddlDistrictCurrentAddress_SelectedIndexChanged(object sender, EventArgs e)
        {
            var message = SysMessage.DefaultSelecting;

            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetTumbon(message, UcCurrentAdd.DropdownProvince.SelectedValue, UcCurrentAdd.DropdownDistrict.SelectedValue);
            BindToDDL(UcCurrentAdd.DropdownParish, ls);
        }

        protected void ddlProvinceRegisterAddress_SelectedIndexChanged(object sender, EventArgs e)
        {
            var message = SysMessage.DefaultSelecting;

            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetAmpur(message, UcRegisAdd.DropdownProvince.SelectedValue);
            BindToDDL(UcRegisAdd.DropdownDistrict, ls);
        }

        protected void ddlDistrictRegisterAddress_SelectedIndexChanged(object sender, EventArgs e)
        {
            var message = SysMessage.DefaultSelecting;

            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetTumbon(message, UcRegisAdd.DropdownProvince.SelectedValue, UcRegisAdd.DropdownDistrict.SelectedValue);
            BindToDDL(UcRegisAdd.DropdownParish, ls);
        }
    }
}
