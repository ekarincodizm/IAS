using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using System.Web.UI.HtmlControls;
using System.Collections.Specialized;
using IAS.BLL.AttachFilesIAS;
using IAS.BLL.AttachFilesIAS.States;
using System.Text;
using IAS.Utils;
using System.Threading;
using IAS.BLL;
using IAS.DTO;
using IAS.Properties;
using System.Runtime.Serialization;

namespace IAS.MasterPage
{

    public partial class MasterRegister : System.Web.UI.MasterPage
    {
        #region Public Param & Session
        public event EventHandler SaveRegiter_Click;
        public event EventHandler OkRegister_Click;
        public event EventHandler CancelRegister_Click;
        public event EventHandler CheckAgreement_CheckedChanged;
        private String _registrationId;
        private string[] ctrlName = { "reqPW1", "reqRegPW1", "reqPW2", "reqRegPW2", "reqComparePW" };
        private List<System.Web.UI.WebControls.RequiredFieldValidator> lsREQCtrl = new List<System.Web.UI.WebControls.RequiredFieldValidator>();
        private List<System.Web.UI.WebControls.RegularExpressionValidator> lsREGCtrl = new List<System.Web.UI.WebControls.RegularExpressionValidator>();
        public List<DTO.PersonLicenseTransaction> CurrentLicenseByIDCard = new List<PersonLicenseTransaction>();
        
        public IAS.UserControl.ucAttachFileControl AttachFileControl { get { return ucAttachFileControl1; } set { ucAttachFileControl1 = value; } }
        public String ConfirmUserName { get { return txtConfirmUserName.Text; } set { txtConfirmUserName.Text = value; } }
        public String Password { get { return txtPassword.Text; } set { txtPassword.Text = value; } }
        public String ConfirmPassword { get { return txtConfirmPassword.Text; } set { txtConfirmPassword.Text = value; } }
        public String ResultRegister { get { return txtResultReg.Text; } set { txtResultReg.Text = value; } }
        public Boolean AgreementStatus { get { return chkAgreement.Checked; } }
        public IAS.UserControl.UCModalSuccess ModelSuccess { get { return UCModalSuccess; } }
        public IAS.UserControl.UCModalError ModelError { get { return UCModalError; } }
        public Button ButtonSubmit { get { return btnSubmit; } set { btnSubmit = value; } }
        public Button ButtonOk { get { return btnOk; } set { btnOk = value; } }
        public Panel PNLCondition { get { return PnlCodition; } set { PnlCodition = value; } }
        public Panel PNLAddButton { get { return PnlAddButton; } set { PnlAddButton = value; } }
        public Label LabelMsg { get { return lblMsg; } set { lblMsg = value; } }

        public RequiredFieldValidator ReqPW1 { get { return reqPW1; } set { reqPW1 = value; } }
        public RegularExpressionValidator ReqRegPW1 { get { return reqRegPW1; } set { reqRegPW1 = value; } }

        public RequiredFieldValidator ReqPW2 { get { return reqPW2; } set { reqPW2 = value; } }
        public RegularExpressionValidator ReqRegPW2 { get { return reqRegPW2; } set { reqRegPW2 = value; } }

        public CompareValidator ReqComparePW { get { return reqComparePW; } set { reqComparePW = value; } }


        public UpdatePanel UpdatePanelMaster { get { return UpdatePanelUpload; } set { UpdatePanelUpload = value; } }

        public String LabelPassword { get { return lblPassword.Text; } set { lblPassword.Text = value; } }
        public String LableConfirmPassword { get { return lblConfirmPassword.Text; } set { lblConfirmPassword.Text = value; } }

        public TextBox TextBoxPassword { get { return txtPassword; } }
        public TextBox TextBoxConfirmPassword { get { return txtConfirmPassword; } }
        public TextBox TextBoxConfirmUserName { get { return txtConfirmUserName; } }
        public TextBox TextBoxResultReg { get { return txtResultReg; } }
        public ContentPlaceHolder ContentDetails { get { return Detail; } }

        public Register.RegisGeneral RegisGeneral
        {
            get { return (this.Page as Register.RegisGeneral); }
        }

        public Register.RegisCompany RegisCompany
        {
            get { return (this.Page as Register.RegisCompany); }
        }

        public Register.RegisAssociate RegisAssociate
        {
            get { return (this.Page as Register.RegisAssociate); }
        }

        public DTO.UserProfile UserProfile
        {
            get
            {
                return Session[PageList.UserProfile] == null ? null : (DTO.UserProfile)Session[PageList.UserProfile];
            }
        }

        public DTO.DataActionMode ActionMode
        {
            get
            {
                switch (ViewState["ActionMode"].ToString())
                {

                    case "E": return IAS.DTO.DataActionMode.Edit;

                    case "V": return IAS.DTO.DataActionMode.View;

                    case "T": return IAS.DTO.DataActionMode.TargetView;

                    default:
                        return IAS.DTO.DataActionMode.Add;
                }

            }
        }

        public String RegistrationId
        {
            get { return _registrationId; }
            set {
                _registrationId = value;
                ViewState["RegistrationId"] = _registrationId;
            }
        }

        public string CurPage
        {
            get { return (Session["CurrentPage"] == null) ? "" : Session["CurrentPage"].ToString(); }
            set 
            { 
                Session["CurrentPage"] = value; 
            }
        }

        public string GroupValidation
        {
            get { return (Session["groupvalidation"] == null) ? "" : Session["groupvalidation"].ToString(); }
            set
            {
                Session["groupvalidation"] = value;
            }
        }

        public string PageTimeout
        {
            get { return (Session["pagetimeout"] == null) ? "" : Session["pagetimeout"].ToString(); }
            set
            {
                Session["pagetimeout"] = value;
            }
        }

        public string ImportStatus
        {
            get { return (Session["importstatus"] == null) ? "" : Session["importstatus"].ToString(); }
            set
            {
                Session["importstatus"] = value;
            }
        }

        #endregion

        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {

            //if (this.ActionMode != null)
            //{
            //    if (!this.ActionMode.Equals(IAS.DTO.DataActionMode.Add))
            //    {
            //        CheckSession();
            //    }
            //}
            //CheckSession();
            
            if (!Page.IsPostBack)
            {
                Init();
            }
            else
            {
                _registrationId = ViewState["RegistrationId"].ToString();
                ucAttachFileControl1.RegisterationId = _registrationId;
                ucAttachFileControl1.CurrentUser = (UserProfile == null) ? "" : UserProfile.LoginName;
            }
            CheckSession();
        }

        public void CheckSession()
        {
            if (this.PageTimeout == "")
            {
                Session["SessionLost"] = true;
                Response.Redirect(PageList.Home);
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

        public void Init()
        {
            this.CurrentPage();
            this.ImportStatus = "N";
            if (Request.QueryString["Mode"] == null)
            {

                InitOnAddMode();
            }
            else
            {
                switch (Request.QueryString["Mode"].ToString())
                {
                    case "A": InitOnAddMode();
                        break;
                    case "E": InitOnEditMode();
                        break;
                    case "V": InitOnViewMode();
                        break;
                    case "T": InitOnTargetViewMode();
                        break;
                    default:
                        break;
                }
            }

            
        }


        #region Initial Form

        private void InitOnAddMode()
        {
            ViewState["ActionMode"] = "A";
            btnOk.Visible = true;
            btnCancel.Visible = true;
            btnSubmit.Visible = false;
            RegistrationId = IAS.BLL.Helpers.GenerateIdHelper.GetGenAutoId();
            InitAttachFileControl();
            PnlResultReg.Visible = false;

            this.PageTimeout = "A";


            if (RegisGeneral != null)
            {
                this.ddlAgentTypeInit(RegisGeneral.FindControl("ddlAgentType") as DropDownList);
            }
            
        }

        //Last Update 16-10-56
        private void InitOnEditMode()
        {
            ViewState["ActionMode"] = "E";
            btnOk.Visible = true;
            btnCancel.Visible = true;
            btnSubmit.Visible = false;
            RegistrationId = this.UserProfile.Id;
            InitAttachFileControl();
            GetAttatchRegisterationFiles();
            PnlResultReg.Visible = false;

            this.PageTimeout = "E";

            if (RegisGeneral != null)
            {
                this.ddlAgentTypeInit(RegisGeneral.FindControl("ddlAgentType") as DropDownList);
            }
            

        }

        private void InitOnViewMode()
        {
            ViewState["ActionMode"] = "V";
            btnCancel.Visible = false;
            btnOk.Visible = false;
            btnSubmit.Visible = false;
            RegistrationId = this.UserProfile.Id;
            InitAttachFileControl();
            GetAttatchRegisterationFiles();

            this.PageTimeout = "V";

            if (RegisGeneral != null)
            {
                this.ddlAgentTypeInit(RegisGeneral.FindControl("ddlAgentType") as DropDownList);
            }
            
        }

        private void InitOnTargetViewMode()
        {
            ViewState["ActionMode"] = "T";
            this.RegistrationId = Request.QueryString["PersonIdQuery"].ToString();
            btnCancel.OnClientClick = String.Format("javascript:window.location = '{0}';", Utils.UrlHelper.Resolve("/Register/regSearchOfficerOIC.aspx?Back=R"));
            btnCancel.Text = "ย้อนกลับ";
           //btnCancel.Visible = false;
            btnOk.Visible = false;
            btnSubmit.Visible = false;
            InitAttachFileControl();
            GetAttatchRegisterationFiles();

            this.PageTimeout = "T";

            if (RegisGeneral != null)
            {
                this.ddlAgentTypeInit(RegisGeneral.FindControl("ddlAgentType") as DropDownList);
            }
            

        }

        public void InitAttachFileControl()
        {
            GetAttachFilesType();
            ucAttachFileControl1.RegisterationId = this.RegistrationId;
        }

        #endregion End Initial


        public void GetAttatchRegisterationFiles()
        {
            var biz = new BLL.RegistrationBiz();

            DTO.ResponseService<DTO.RegistrationAttatchFile[]> res = biz.GetAttatchFilesByRegisterationID(RegistrationId);

            var list = res.DataResponse.ToList();

            ucAttachFileControl1.AttachFiles = list.ConvertToAttachFilesView();

            UpdatePanelUpload.Update();

        }

        //คุณวุฒิ
        public void GetQualification(DropDownList dropdown)
        {
            var message = SysMessage.DefaultSelecting;
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetQualification(message);
            BindToDDL(dropdown, ls);
        }

        public void GetEducation(DropDownList dropdown)
        {
            var message = SysMessage.DefaultSelecting;

            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetEducation(message);
            BindToDDL(dropdown, ls);
        }


        /// <summary>
        /// ISSUE IAS INTERNAL AR IASAR-392
        /// </summary>
        /// <param name="dropdown"></param>
        /// <AUTHOR>Natta</AUTHOR>
        public void GetNationality(DropDownList dropdown)
        {
            var message = SysMessage.DefaultSelecting;
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetNationality(message);
            BindToDDL(dropdown, ls);
            //string code = ls.FirstOrDefault(w => w.Name == "ไทย").Id;
            //dropdown.SelectedValue = code;
        }

        public void GetTitle(DropDownList dropdown)
        {
            var message = SysMessage.DefaultSelecting;

            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetTitleName(message);
            BindToDDL(dropdown, ls);

        }

        public DTO.Registration GetLastRegistration()  
        {
            BLL.RegistrationBiz regBiz = new BLL.RegistrationBiz();
            var res = regBiz.GetById(UserProfile.Id);
            if (res.IsError)
            {
                ModelError.ShowMessageError = res.ErrorMsg;
                ModelError.ShowModalError();
            }
            RegistrationId = res.DataResponse.ID;
            txtResultReg.Text = res.DataResponse.APPROVE_RESULT;
           
            return res.DataResponse;
        }

        public DTO.Registration GetRegistration()
        {
            BLL.RegistrationBiz regBiz = new BLL.RegistrationBiz();
            var res = regBiz.GetById(RegistrationId);
            if (res.IsError)
            {
                ModelError.ShowMessageError = res.ErrorMsg;
                ModelError.ShowModalError();
            }
            RegistrationId = res.DataResponse.ID;
            txtResultReg.Text = res.DataResponse.APPROVE_RESULT;
            return res.DataResponse;
        }

        /// <summary>
        /// GetAttachFilesType : ISSUE > IAS INTERNAL AR IASAR-318
        /// </summary>
        /// <LASTUPDATE>08/08/2557</LASTUPDATE>
        /// <AUTHOR>Natta</AUTHOR>
        public void GetAttachFilesType()
        {
            var message = SysMessage.DefaultSelecting;
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetDocumentType(message);
            //this.ucAttachFileControl1.DocumentTypes = ls;

            List<DTO.DataItem> newls = ls.Where(s => s.EXAM_DISCOUNT_STATUS == null && s.TRAIN_DISCOUNT_STATUS == null).ToList()
                .Union(ls.Where(s => s.EXAM_DISCOUNT_STATUS == "N" && s.TRAIN_DISCOUNT_STATUS == "N").ToList()).ToList().OrderBy(code => code.Id).ToList();
            this.ucAttachFileControl1.DocumentTypes = newls;
        }

        /// <summary>
        /// SetValidateGroup
        /// Set GroupValidation into Session
        /// Do not remove
        /// Func renew by Nattapong @ 01/11/2556
        /// </summary>
        /// <param name="reqFormat"></param>
        public void SetValidateGroup(string reqFormat)
        {
            this.GroupValidation = reqFormat;
            this.ButtonOk.ValidationGroup = reqFormat;
            this.ButtonSubmit.ValidationGroup = reqFormat;
            this.reqPW1.ValidationGroup = reqFormat;
            this.reqRegPW1.ValidationGroup = reqFormat;
            this.reqPW2.ValidationGroup = reqFormat;
            this.reqRegPW2.ValidationGroup = reqFormat;
            this.reqComparePW.ValidationGroup = reqFormat;

            this.btnSubmit.ValidationGroup = reqFormat;
        }

        private String ModeValue(DTO.DataActionMode mode)
        {
            switch (mode)
            {

                case IAS.DTO.DataActionMode.Edit:
                    return "E";

                case IAS.DTO.DataActionMode.View:
                    return "V";
                default:
                    return "A";

            }
        }

        /// <summary>
        /// RequiredFieldValidator & RegularExpressionValidator Validation Function
        /// Added new & Last Update 30/10/2556
        /// by Nattapong
        /// </summary>
        /// <param name="registrationType"></param>
        /// <param name="registration"></param>
        private DTO.ResponseMessage<bool> RegPassValidation()
        {
            DTO.ResponseMessage<bool> res = new DTO.ResponseMessage<bool>();
            List<FilesValidate> lsValidate = new List<FilesValidate>();
            try
            {

                this.Page.Validate(this.GroupValidation);
                if (this.Page.IsValid)
                {

                    res.ResultMessage = true;
                }
                else
                {
                    if ((this.reqPW1.IsValid == false) || (this.reqPW2.IsValid == false))
                    {
                        res.ErrorMsg = SysMessage.ReqValidationPW;
                        res.ResultMessage = false;
                    }
                    if ((this.reqRegPW1.IsValid == false) || (this.reqRegPW2.IsValid == false))
                    {
                        res.ErrorMsg = SysMessage.RegValidationPW;
                        res.ResultMessage = false;
                    }
                    //if (this.reqPW2.IsValid == false)
                    //{
                    //    res.ErrorMsg = SysMessage.ReqValidationPW;
                    //    res.ResultMessage = false;
                    //}
                    //if (this.reqRegPW2.IsValid == false)
                    //{
                    //    res.ErrorMsg = SysMessage.RegValidationPW;
                    //    res.ResultMessage = false;
                    //}
                    if (this.reqComparePW.IsValid == false)
                    {
                        res.ErrorMsg = SysMessage.RegValidationCompare;
                        res.ResultMessage = false;
                    }
                    if ((this.reqPW1.IsValid == true) && (this.reqRegPW1.IsValid == true) && (this.reqPW2.IsValid == true)
                        && (this.reqRegPW2.IsValid == true) && (this.reqComparePW.IsValid == true))
                    {
                        res.ResultMessage = true;
                    }
                }
                

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }

            return res;
        }

        /// <summary>
        /// RequiredFieldValidator & RegularExpressionValidator Validation Function
        /// Added new & Last Update 31/10/2556
        /// by Nattapong
        /// </summary>
        /// <param name="registrationType"></param>
        /// <param name="registration"></param>
        public void LsControlValidation(ControlCollection objSiteControls)
        {
            //ControlCollection objSiteControls = this.Detail.Controls;

            foreach (Control objCurrControl in objSiteControls)
            {
                string strCurrControlName = objCurrControl.GetType().Name;

                switch (strCurrControlName)
                {
                    case "TextBox":
                        TextBox objTextBoxControl = (TextBox)objCurrControl;
                        objTextBoxControl.Text = string.Empty;
                        break;
                    case "DropDownList":
                        DropDownList objDropDownControl = (DropDownList)objCurrControl;
                        objDropDownControl.SelectedIndex = -1;
                        break;
                    case "GridView":
                        GridView objGridViewControl = (GridView)objCurrControl;
                        objGridViewControl.SelectedIndex = -1;
                        break;
                    case "CheckBox":
                        CheckBox objCheckBoxControl = (CheckBox)objCurrControl;
                        objCheckBoxControl.Checked = false;
                        break;
                    case "CheckBoxList":
                        CheckBoxList objCheckBoxListControl = (CheckBoxList)objCurrControl;
                        objCheckBoxListControl.ClearSelection();
                        break;
                    case "RadioButtonList":
                        RadioButtonList objRadioButtonList = (RadioButtonList)objCurrControl;
                        objRadioButtonList.ClearSelection();
                        break;
                    case "RequiredFieldValidator":
                        RequiredFieldValidator objRequiredFieldValidator = (RequiredFieldValidator)objCurrControl;
                        objRequiredFieldValidator.IsValid = false;
                        break;
                    case "RegularExpressionValidator":
                        RegularExpressionValidator objRegularExpressionValidator = (RegularExpressionValidator)objCurrControl;
                        objRegularExpressionValidator.IsValid = false;
                        break;
                    case "Panel":
                        break;

                }
            }

        }

        public void ControlValidation(Control parent)
        {
            //DTO.ResponseMessage<bool> res = new DTO.ResponseMessage<bool>();
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl.Controls.Count > 0)
                    ControlValidation(ctrl);
                else
                {
                    if (ctrl is RequiredFieldValidator)
                        (ctrl as RequiredFieldValidator).IsValid = false;

                    if (ctrl is RegularExpressionValidator)
                        (ctrl as RegularExpressionValidator).IsValid = false;
                }
            }

        }

        public DTO.ResponseMessage<bool> ControlValidationBeforeSubmit(Page child)
        {
            DTO.ResponseMessage<bool> res = new DTO.ResponseMessage<bool>();
            StringBuilder strBuild = new StringBuilder();

            this.Page.Validate(this.GroupValidation);
            if (this.Page.IsValid)
            {
                res.ResultMessage = true;
            }
            else
            {
                if (child.Validators.Count > 0)
                {
                    for (int i = 0; i < child.Validators.Count; i++)
                    {
                        string nameType = child.Validators[i].GetType().Name;
                        if (nameType == "RequiredFieldValidator")
                        {
                            bool validate = child.Validators[i].IsValid;
                            if (validate == false)
                            {
                                strBuild.Append(child.Validators[i].ErrorMessage + "<br/>");
                            }

                        }
                        if (nameType == "RegularExpressionValidator")
                        {
                            bool validate = child.Validators[i].IsValid;
                            if (validate == false)
                            {
                                if (strBuild.Length > 0)
                                {
                                    bool chkError = strBuild.ToString().Equals(child.Validators[i].ErrorMessage + "<br/>");
                                    if (chkError == false)
                                    {
                                        strBuild.Append(child.Validators[i].ErrorMessage + "<br/>");
                                    }
                                }
                                else
                                {
                                    strBuild.Append(child.Validators[i].ErrorMessage + "<br/>");
                                }
                                //var chkError = strBuild.ToString().Where(str => str.Equals(child.Validators[i].ErrorMessage)).FirstOrDefault();
                                //if (chkError == null)
                                //{
                                //    strBuild.Append(child.Validators[i].ErrorMessage + "<br/>");
                                //}
                                //strBuild.Append(child.Validators[i].ErrorMessage + "<br/>");
                                
                            }
                        }
                        if (nameType == "CompareValidator")
                        {
                            bool validate = child.Validators[i].IsValid;
                            if (validate == false)
                            {
                                strBuild.Append(child.Validators[i].ErrorMessage + "<br/>");
                            }
                        }
                    }

                }
               
            }

            if (strBuild.ToString() != string.Empty)
            {
                res.ErrorMsg = strBuild.ToString();
                res.ResultMessage = false;
            }
            else
            {
                res.ResultMessage = true;
            }
            
            return res;
        }


        /// <summary>
        /// DateTime Compare
        /// Result ได้ -1 If  Time_1 น้อยกว่า  Time_2
        /// Result ได้ 0  If  Time_1 เท่ากับ Time_2
        /// Result ได้ 1  If  Time_1 มากกว่า Time_2
        /// How to use Compare?
        /// 1.เวลาปัจจุบัน เทียบกับ เวลาที่เลือก
        /// Coding BY Natta
        /// </summary>
        public DTO.ResponseMessage<bool> DateValidation(string birthDay)
        {
            DTO.ResponseMessage<bool> res = new DTO.ResponseMessage<bool>();

            if ((birthDay != null) && (birthDay != ""))
            {
                DateTime currDate = DateTime.Now;
                DateTime dateFromCtrl = Convert.ToDateTime(birthDay);

                string currDateFormat = String.Format("{0:dd/MM/yyy}", currDate).ToString();
                string birthDateFormat = String.Format("{0:dd/MM/yyy}", dateFromCtrl).ToString();
                DateTime currTime = DateTime.Parse(currDateFormat);
                DateTime birthTime = DateTime.Parse(birthDateFormat);

                int dateCompare = DateTime.Compare(birthTime, currTime);
                //BirthDay < CurrentTime
                if (dateCompare == -1)
                {
                    res.ResultMessage = true;
                }
                //BirthDay == CurrentTime
                if (dateCompare == 0)
                {
                    res.ErrorMsg = SysMessage.RegBirthDayValidationF;
                    res.ResultMessage = false;
                }
                //BirthDay > CurrentTime
                if (dateCompare == 1)
                {
                    res.ErrorMsg = SysMessage.RegBirthDayValidationF;
                    res.ResultMessage = false;
                }
                

            }
            else
            {
                res.ErrorMsg = Resources.errorSysMessage_RegBirthDayValidationF;
                res.ResultMessage = false;
            }

            return res;
        }

        public void ArgRecheck(bool errStatus)
        {
            if (errStatus != null)
            {
                if (errStatus == true)
                {
                    chkAgreement.Checked = false;
                }
                else if (errStatus == false)
                {
                    chkAgreement.Checked = true;
                }

            }

        }

        /// <summary>
        /// Validation Last Update 30/10/2556
        /// by Nattapong
        /// Last update  by Nattapong @21-11-56
        /// </summary>
        /// <param name="registrationType"></param>
        /// <param name="registration"></param>
        public void NewRegister(DTO.RegistrationType registrationType, DTO.Registration registration)
        {

            if (AgreementStatus)
            {
                #region Validate before submit
                //General BirthDay Validation
                //CurrentPage Check
                if ((this.CurPage != null) && (this.CurPage != ""))
                {
                    //RegisGeneral
                    if (this.CurPage.Equals("1"))
                    {
                        DTO.ResponseMessage<bool> resMail = RegisGeneral.EMailValidationBeforeSubmit;
                        if (resMail.ResultMessage == true)
                        {
                            DTO.ResponseMessage<bool> resBirthDay = this.DateValidation(RegisGeneral.TextboxBirthDay.Text);
                            if (resBirthDay.IsError)
                            {
                                ModelError.ShowMessageError = resBirthDay.ErrorMsg;
                                ModelError.ShowModalError();
                                return;
                            }
                        }
                        else if (resMail.IsError)
                        {
                            ModelError.ShowMessageError = resMail.ErrorMsg;
                            ModelError.ShowModalError();
                            ArgRecheck(resMail.IsError);
                            return;
                        }
                    }
                    //RegisCompany
                    if (this.CurPage.Equals("2"))
                    {
                        DTO.ResponseMessage<bool> resMail = RegisCompany.EMailValidationBeforeSubmit;
                        if (resMail.IsError)
                        {
                            ModelError.ShowMessageError = resMail.ErrorMsg;
                            ModelError.ShowModalError();
                            ArgRecheck(resMail.IsError);
                            return;
                        }
                    }
                    //RegisAssociate
                    if (this.CurPage.Equals("3"))
                    {
                        DTO.ResponseMessage<bool> resMail = RegisAssociate.EMailValidationBeforeSubmit;
                        if (resMail.IsError)
                        {
                            ModelError.ShowMessageError = resMail.ErrorMsg;
                            ModelError.ShowModalError();
                            ArgRecheck(resMail.IsError);
                            return;
                        }

                    }
                }

                //Control Validation
                DTO.ResponseMessage<bool> resCtrl = this.ControlValidationBeforeSubmit(this.Page);
                if ((resCtrl.ResultMessage == false) || (resCtrl.IsError))
                {
                    ModelError.ShowMessageError = resCtrl.ErrorMsg;
                    ModelError.ShowModalError();
                    return;
                }
                #endregion

                if (this.RegPassValidation().ResultMessage == false)
                {
                    ModelError.ShowMessageError = this.RegPassValidation().ErrorMsg;
                    ModelError.ShowModalError();
                    return;
                }
                
                BLL.RegistrationBiz biz = new BLL.RegistrationBiz();
                //Nattapong @21-11-56
                //var result = biz.ValidateBeforeSubmit(registrationType, registration);

                //if (result.IsError)
                //{
                //    ModelError.ShowMessageError = result.ErrorMsg;
                //    ModelError.ShowModalError();
                //    return;
                //}
                var result = biz.EntityValidation(registrationType, registration);
                if (result.IsError)
                {
                    ModelError.ShowMessageError = result.ErrorMsg;
                    ModelError.ShowModalError();
                    return;
                }


                List<DTO.RegistrationAttatchFile> attachFileList = Utils.DistinctDuplicatesHelper.Duplicates<BLL.AttachFilesIAS.AttachFile>(AttachFileControl.AttachFiles).ToList().ConvertToRegistrationAttachFiles().ToList();
                attachFileList.ForEach(a => a.FILE_STATUS = BLL.AttachFilesIAS.States.AttachFileStatus.Active.Value());

                var res = biz.InsertWithAttatchFile(registrationType, registration, attachFileList);

                if (res.IsError)
                {
                    ModelError.ShowMessageError = res.ErrorMsg;
                    ModelError.ShowModalError();
                    return;
                }


                BLL.DataCenterBiz dbiz = new BLL.DataCenterBiz();
                var r = dbiz.GetConfigApproveMember();

                foreach (var items in r.DataResponse)
                {
                    if (items.Id == "01" && items.Value == "Y")
                    {
                        string AlertWaitingForApprove = "alert('"+Resources.infoSysMessage_EditSuccess+"');window.location.assign('../home.aspx')";
                        ToolkitScriptManager.RegisterClientScriptBlock(btnSubmit, this.GetType(), "alert", AlertWaitingForApprove, true);
                    }
                }
                string Alert = "alert('"+Resources.infoSysMessage_RegisSuccess2+"');window.location.assign('../home.aspx')";
                ToolkitScriptManager.RegisterClientScriptBlock(btnSubmit, this.GetType(), "alert", Alert, true);

            }
        }


        public void EditRegister(DTO.RegistrationType registrationType, DTO.Registration registration )
        {
            BLL.RegistrationBiz biz = new BLL.RegistrationBiz();
            registration.STATUS = "1";

            

            if (registration != null)
            {
                try
                {
                    #region Validate before submit
                    //General BirthDay Validation
                    //CurrentPage Check
                    if ((this.CurPage != null) && (this.CurPage != ""))
                    {
                        //RegisGeneral
                        if (this.CurPage.Equals("1"))
                        {
                            DTO.ResponseMessage<bool> resBirthDay = this.DateValidation(RegisGeneral.TextboxBirthDay.Text);
                            if ((resBirthDay.ResultMessage == false) || (resBirthDay.IsError))
                            {
                                ModelError.ShowMessageError = resBirthDay.ErrorMsg;
                                ModelError.ShowModalError();
                                return;
                            }
                        }
                    }

                    //Control Validation
                    DTO.ResponseMessage<bool> resCtrl = this.ControlValidationBeforeSubmit(this.Page);
                    if ((resCtrl.ResultMessage == false) || (resCtrl.IsError))
                    {
                        ModelError.ShowMessageError = resCtrl.ErrorMsg;
                        ModelError.ShowModalError();
                        return;
                    }
                    #endregion

                    var res = biz.Update(registration, AttachFileControl.AttachFiles.ConvertToRegistrationAttachFiles().ToList());

                    if (res.IsError)
                    {
                        ModelError.ShowMessageError = res.ErrorMsg;
                        ModelError.ShowModalError();
                        return;
                    }


                    BLL.DataCenterBiz dbiz = new BLL.DataCenterBiz();
                    var r = dbiz.GetConfigApproveMember();
                    foreach (var items in r.DataResponse)
                    {
                        if (items.Id == "01" && items.Value == "Y")
                        {
                            if (registration.MEMBER_TYPE.Equals("1"))
                            {
                                string AlertWaitingForApprove = "alert('"+ Resources.infoSysMessage_EditSuccess +"');window.location.assign('../Register/RegisGeneral.aspx?Mode=V')";
                                ToolkitScriptManager.RegisterClientScriptBlock(btnSubmit, this.GetType(), "alert", AlertWaitingForApprove, true);
                            }
                            else if (registration.MEMBER_TYPE.Equals("2"))
                            {
                                string AlertWaitingForApprove = "alert('"+ Resources.infoSysMessage_EditSuccess +"');window.location.assign('../Register/RegisCompany.aspx?Mode=V')";
                                ToolkitScriptManager.RegisterClientScriptBlock(btnSubmit, this.GetType(), "alert", AlertWaitingForApprove, true);
                            }
                            else if (registration.MEMBER_TYPE.Equals("3"))
                            {
                                string AlertWaitingForApprove = "alert('"+ Resources.infoSysMessage_EditSuccess +"');window.location.assign('../Register/RegisAssociate.aspx?Mode=V')";
                                ToolkitScriptManager.RegisterClientScriptBlock(btnSubmit, this.GetType(), "alert", AlertWaitingForApprove, true);
                            }
                            
                        }
                    }
                    string Alert = "alert('"+ Resources.infoSysMessage_RegisSuccess2 +"');window.location.assign('../home.aspx')";
                    ToolkitScriptManager.RegisterClientScriptBlock(btnSubmit, this.GetType(), "alert", Alert, true);


                }
                catch (Exception ex)
                {
                    ModelError.ShowMessageError = ex.Message;
                    ModelError.ShowModalError();
                }
            }

        }

        private string CurrentPage()
        {
            //Get page current
            string Path = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            System.IO.FileInfo Info = new System.IO.FileInfo(Path);
            string pageName = Info.Name;

            //Set page Type on PageLoad()
            string[] pagerS = pageName.Split('.');

            if (pagerS != null)
            {
                if (pagerS[0].Substring(5).Equals("General"))
                {
                    Session["CurrentPage"] = "1";
                    lblText.Text = Resources.infoMasterRegister_001 +"<br>"+ Resources.infoMasterRegister_002;
                }
                else if (pagerS[0].Substring(5).Equals("Company"))
                {
                    Session["CurrentPage"] = "2";
                    lblText.Text = Resources.infoMasterRegister_003 +"<br>"+Resources.infoMasterRegister_002;
                }
                else if (pagerS[0].Substring(5).Equals("Associate"))
                {
                    Session["CurrentPage"] = "3";
                    lblText.Text = Resources.infoMasterRegister_003 + "<br>" + Resources.infoMasterRegister_002;
                }
            }

            return pageName;

        }

        public void ShowApproveResultPerson()
        {
            BLL.PersonBiz perBiz = new BLL.PersonBiz();
            var res = perBiz.GetById(RegistrationId);
            if (res.IsError)
            {
                ModelError.ShowMessageError = res.ErrorMsg;
                ModelError.ShowModalError();
            }
            else if (res != null)
            {
                txtResultReg.Text = res.DataResponse.APPROVE_RESULT;
            }
        }

        public void ddlAgentTypeInit(DropDownList ddl)
        {
            RegistrationBiz biz = new RegistrationBiz();

            if (ddl != null)
            {
                ResponseService<DTO.AgentTypeEntity[]> res = biz.GetAgentType("");
                if (res.DataResponse != null)
                {
                    if (res.DataResponse.Count() > 0)
                    {
                        ddl.DataSource = res.DataResponse;
                        ddl.DataTextField = "AGENT_TYPE_DESC";
                        ddl.DataValueField = "AGENT_TYPE";
                        ddl.DataBind();
                    }
                }
                else
                {
                    ddl.DataSource = null;
                    ddl.DataBind();
                }
            }

        }

        #endregion

        #region UI Function
        /// <summary>
        /// Validation before Submit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOk_Click(object sender, EventArgs e)
        {
            BLL.RegistrationBiz biz = new BLL.RegistrationBiz();

            #region Edit Mode
            //Edit Mode
            if (this.ActionMode.Equals(IAS.DTO.DataActionMode.Edit))
            {
                //General BirthDay Validation
                //CurrentPage Check
                if ((this.CurPage != null) && (this.CurPage != ""))
                {
                    //RegisGeneral
                    if (this.CurPage.Equals("1"))
                    {
                        DTO.ResponseMessage<bool> resBirthDay = this.DateValidation(RegisGeneral.TextboxBirthDay.Text);
                        if (resBirthDay.IsError)
                        {
                            ModelError.ShowMessageError = resBirthDay.ErrorMsg;
                            ModelError.ShowModalError();
                            return;
                        }
                    }
                }

                //Control Validation
                DTO.ResponseMessage<bool> res = this.ControlValidationBeforeSubmit(this.Page);
                if ((res.ResultMessage == false) || (res.IsError))
                {
                    ModelError.ShowMessageError = res.ErrorMsg;
                    ModelError.ShowModalError();
                    //return;
                    //ControlValidation(RegisGeneral);
                    return;
                }

                TextBox txtEmail = (TextBox)Detail.FindControl("txtEmail");
                //DTO.ResponseMessage<bool> resDoc = ucAttachFileControl1.DocRequire();
                String resDoc = ucAttachFileControl1.ValidDocRequire();
                //Get USER FROM Auth
                if (txtEmail != null)
                {
                    //Get Mode
                    if (this.ActionMode == DTO.DataActionMode.Edit)
                    {
                        //Files Validation
                        if ( !String.IsNullOrEmpty(resDoc))
                        {
                            ModelError.ShowMessageError = resDoc;
                            ModelError.ShowModalError();


                        }
                        else 
                        {
                            //PnlCodition.Visible = true;
                            //PnlAddButton.Visible = false;
                            txtConfirmUserName.Text = txtEmail.Text.Trim();
                            //if (OkRegister_Click != null)
                            //    OkRegister_Click(sender, e);
                            if (SaveRegiter_Click != null)
                            {
                                SaveRegiter_Click(sender, e);
                            }
                        }

                    }
                }
                else
                {
                    ModelError.ShowMessageError = Resources.errorMasterRegister_001;
                    ModelError.ShowModalError();

                }
            }
            #endregion

            #region Add Mode
            //Add Mode
            else if (this.ActionMode.Equals(DTO.DataActionMode.Add))
            {
                string curMode = this.ActionMode.ToString();
                
                //General BirthDay Validation
                //CurrentPage Check
                if ((this.CurPage != null) && (this.CurPage != ""))
                {
                    //RegisGeneral
                    if (this.CurPage.Equals("1"))
                    {
                        DTO.ResponseMessage<bool> resMail = RegisGeneral.EMailValidationBeforeSubmit;
                        if (resMail.ResultMessage == true)
                        {
                            DTO.ResponseMessage<bool> resBirthDay = this.DateValidation(RegisGeneral.TextboxBirthDay.Text);
                            if (resBirthDay.IsError)
                            {
                                ModelError.ShowMessageError = resBirthDay.ErrorMsg;
                                ModelError.ShowModalError();
                                return;
                            }
                        }
                        else if(resMail.IsError)
                        {
                            ModelError.ShowMessageError = resMail.ErrorMsg;
                            ModelError.ShowModalError();
                            return;
                        }
                    }
                    //RegisCompany
                    if (this.CurPage.Equals("2"))
                    {
                        DTO.ResponseMessage<bool> resMail = RegisCompany.EMailValidationBeforeSubmit;
                        if (resMail.IsError)
                        {
                            ModelError.ShowMessageError = resMail.ErrorMsg;
                            ModelError.ShowModalError();
                            return;
                        }
                    }
                    //RegisAssociate
                    if (this.CurPage.Equals("3"))
                    {
                        DTO.ResponseMessage<bool> resMail = RegisAssociate.EMailValidationBeforeSubmit;
                        if (resMail.IsError)
                        {
                            ModelError.ShowMessageError = resMail.ErrorMsg;
                            ModelError.ShowModalError();
                            return;
                        }

                    }
                }

                //Control Validation
                DTO.ResponseMessage<bool> res = this.ControlValidationBeforeSubmit(this.Page);
                if ((res.ResultMessage == false) || (res.IsError))
                {
                    ModelError.ShowMessageError = res.ErrorMsg;
                    ModelError.ShowModalError();
                    //return;
                    //ControlValidation(RegisGeneral);
                    return;
                }

                TextBox txtEmail = (TextBox)Detail.FindControl("txtEmail");
                String resDoc = ucAttachFileControl1.ValidDocRequire();

                //Get USER FROM Auth
                if (txtEmail != null)
                {
                    //Get Mode
                    if ((this.ActionMode == DTO.DataActionMode.Add) || (this.ActionMode == DTO.DataActionMode.Edit))
                    {
                        //Files Validation
                        if (!String.IsNullOrEmpty(resDoc))
                        {
                            ModelError.ShowMessageError = resDoc;
                            ModelError.ShowModalError();
                            return; 

                        }
                        else 
                        {
                            PnlCodition.Visible = true;
                            PnlAddButton.Visible = false;
                            txtConfirmUserName.Text = txtEmail.Text.Trim();
                            if (OkRegister_Click != null)
                            {
                                OkRegister_Click(sender, e);
                            }
                        }
                    }
                }
                else
                {
                    ModelError.ShowMessageError = Resources.errorMasterRegister_001;
                    ModelError.ShowModalError();

                }
            }
            #endregion

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //Response.Redirect(String.Format("~/Register/" + CurrentPage() + "?Mode={0}", ModeValue(ActionMode)));
            if (btnCancel.OnClientClick == "")
            {
                Response.Redirect("../home.aspx");
                if (CancelRegister_Click != null)
                    CancelRegister_Click(sender, e);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //Files validation
            String resDoc = this.ucAttachFileControl1.ValidDocRequire();
            if (!String.IsNullOrEmpty(resDoc))
            {
                ModelError.ShowMessageError = resDoc;
                ModelError.ShowModalError();
                return;
            }

            if (SaveRegiter_Click != null)
                SaveRegiter_Click(sender, e);
        }

        protected void chkAgreement_CheckedChanged(object sender, EventArgs e)
        {

            //Not  Finished
            if (chkAgreement.Checked == true)
            {
                btnSubmit.Enabled = true;
                btnSubmit.Visible = true;

                if (this.CurPage.Equals("1"))
                {
                    TextBox txtIDNumber = (TextBox)this.Detail.FindControl("txtIDNumber");
                    if (txtIDNumber != null)
                    {
                        txtConfirmUserName.Text = txtIDNumber.Text;
                    }
                }
                else if (this.CurPage.Equals("2"))
                {
                    TextBox txtEmail = (TextBox)this.Detail.FindControl("txtEmail");
                    if (txtEmail != null)
                    {
                        txtConfirmUserName.Text = txtEmail.Text;
                    }
                }
                else if (this.CurPage.Equals("3"))
                {
                    TextBox txtEmail = (TextBox)this.Detail.FindControl("txtEmail");
                    if (txtEmail!= null)
                    {
                        txtConfirmUserName.Text = txtEmail.Text;
                    }
                }

            }
            else if (chkAgreement.Checked == false)
            {
                btnSubmit.Enabled = false;
                btnSubmit.Visible = false;
                txtConfirmUserName.Text = "";
            }

            CheckAgreement_CheckedChanged(sender, e);
        }
        #endregion

        public class FilesValidate
        {
            public bool Status { get; set; }
            public string RegName { get; set; }
        }

        #region TOR
        /// <summary>
        /// Get&Set Old Persoanl Data from AG_PERSONAL_T by ID_CARD_NO
        /// NT@13/2/2557 & Last Edited
        /// </summary>
        /// <param name="general"></param>
        public void SetPersonalData(DTO.Person per)
        {
            PersonalT personT = new PersonalT();
            SerializationInfo info = new SerializationInfo(this.GetType(), new System.Runtime.Serialization.FormatterConverter());
            StreamingContext context = new StreamingContext(System.Runtime.Serialization.StreamingContextStates.All);
            info.AddValue("IID", per.ID);
            info.AddValue("IMEMBER_TYPE", per.MEMBER_TYPE);
            info.AddValue("IID_CARD_NO", per.ID_CARD_NO);
            info.AddValue("IEMPLOYEE_NO", per.EMPLOYEE_NO);
            info.AddValue("IPRE_NAME_CODE", per.PRE_NAME_CODE);
            info.AddValue("INAMES", per.NAMES);
            info.AddValue("ILASTNAME", per.LASTNAME);
            info.AddValue("INATIONALITY", per.NATIONALITY);
            info.AddValue("IBIRTH_DATE", per.BIRTH_DATE);
            info.AddValue("ISEX", per.SEX);
            info.AddValue("IEDUCATION_CODE", per.EDUCATION_CODE);
            info.AddValue("IADDRESS_1", per.ADDRESS_1);
            info.AddValue("IADDRESS_2", per.ADDRESS_2);
            info.AddValue("IAREA_CODE", per.AREA_CODE);
            info.AddValue("IPROVINCE_CODE", per.PROVINCE_CODE);
            info.AddValue("IZIP_CODE", per.ZIP_CODE);
            info.AddValue("ITELEPHONE", per.TELEPHONE);
            info.AddValue("ILOCAL_ADDRESS1", per.LOCAL_ADDRESS1);
            info.AddValue("ILOCAL_ADDRESS2", per.LOCAL_ADDRESS2);
            info.AddValue("ILOCAL_AREA_CODE", per.LOCAL_AREA_CODE);
            info.AddValue("ILOCAL_PROVINCE_CODE", per.LOCAL_PROVINCE_CODE);
            info.AddValue("ILOCAL_ZIPCODE", per.LOCAL_ZIPCODE);
            info.AddValue("ILOCAL_TELEPHONE", per.LOCAL_TELEPHONE);
            info.AddValue("IEMAIL", per.EMAIL);
            info.AddValue("ISTATUS", per.STATUS);
            info.AddValue("ITUMBON_CODE", per.TUMBON_CODE);
            info.AddValue("ILOCAL_TUMBON_CODE", per.LOCAL_TUMBON_CODE);
            info.AddValue("ICOMP_CODE", per.COMP_CODE);
            info.AddValue("ICREATED_BY", per.CREATED_BY);
            info.AddValue("ICREATED_DATE", per.CREATED_DATE);
            info.AddValue("IUPDATED_BY", per.UPDATED_BY);
            info.AddValue("IUPDATED_DATE", per.UPDATED_DATE);
            info.AddValue("IAPPROVE_RESULT", per.APPROVE_RESULT);
            info.AddValue("IAPPROVED_BY", per.APPROVED_BY);
            info.AddValue("IAGENT_TYPE", per.AGENT_TYPE);
            info.AddValue("ISIGNATUER_IMG", per.SIGNATUER_IMG);
            info.AddValue("IIMG_SIGN", per.IMG_SIGN);
            personT.GetObjectData(info, context);
        }

        public string NullableString(string input)
        {
            if ((input == null) || (input == ""))
            {
                input = "";
            }

            return input;
        }

        public void GetCurrentLicense(UserControl.ucCurrentLicense uctrl, string idCard)
        {
            if (uctrl != null)
            {
                uctrl.GetCurrentLicense(idCard);
            }

        }


        public void SetCurrentLicense(UserControl.ucCurrentLicense uctrl)
        {
            if (uctrl != null)
            {
                uctrl.GridCurrentLicense.DataSource = this.CurrentLicenseByIDCard;
                uctrl.GridCurrentLicense.DataBind();
            }

        }

        public List<DTO.PersonLicenseTransaction> CompareCurrentLicense(string idCard)
        {
            LicenseBiz biz = new LicenseBiz();
            List<DTO.PersonLicenseTransaction> lsLicense = new List<PersonLicenseTransaction>();

            //Get All Licese By ID_CARD_NO
            DTO.ResponseService<DTO.PersonLicenseTransaction[]> res = biz.GetAllLicenseByIDCard(idCard, "2", 1);
            if (res.DataResponse != null)
            {
                lsLicense = res.DataResponse.ToList();
            }

            return lsLicense;

        }
        #endregion
    }
}
