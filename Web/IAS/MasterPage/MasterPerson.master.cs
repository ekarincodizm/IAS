using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.BLL.AttachFilesIAS;
using AjaxControlToolkit;
using IAS.BLL;
using System.Text;
using IAS.DTO;
using IAS.Properties;

namespace IAS.MasterPage
{
    public partial class MasterPerson : System.Web.UI.MasterPage
    {
        #region Public Param & Session
        public event EventHandler OkRegister_Click;
        public event EventHandler CancelRegister_Click;
        private String _personId;

        public IAS.UserControl.ucAttachFileControl AttachFileControl { get { return ucAttachFileControl1; } set { ucAttachFileControl1 = value; } }
        public IAS.UserControl.UCModalSuccess ModelSuccess { get { return UCModalSuccess; } }
        public IAS.UserControl.UCModalError ModelError { get { return UCModalError; } }
        public Button ButtonOk { get { return btnOk; } set { btnOk = value; } }
        public Label LabelMsg { get { return lblMsg; } set { lblMsg = value; } }
      

        public String ResultRegister { get { return txtResultReg.Text; } set { txtResultReg.Text = value; } }
        public Panel PNLAddButton { get { return PnlAddButton; } set { PnlAddButton = value; } }
        public ContentPlaceHolder ContentDetails { get { return Detail; } }

        public Person.PersonGeneral PersonGeneral
        {
            get { return (this.Page as Person.PersonGeneral); }
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
                    default:
                        return IAS.DTO.DataActionMode.View;
                }

            }
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
                    return "V";

            }
        }

        public String PersonId
        {
            get { return _personId; }
            set
            {
                _personId = value;
                ViewState["PersonId"] = _personId;
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

        public string CurPage
        {
            get { return (Session["CurrentPage"] == null) ? "" : Session["CurrentPage"].ToString(); }
            set
            {
                Session["CurrentPage"] = value;
            }
        }

        public Nullable<DateTime> CreateDateTemp
        {
            get { return Convert.ToDateTime(Session["createdatetemp"]); }
            set { Session["createdatetemp"] = value; }
        }

        #endregion

        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckSession();
            if (!Page.IsPostBack)
            {                
                Init();               
            }
            else
            {
                if (UserProfile.STATUS != Convert.ToString((int)DTO.PersonDataStatus.WaitForApprove))
                {
                    PnlAddButton.Visible = true;
                    PnlAddButton.Enabled = true;
                }

                _personId = ViewState["PersonId"].ToString();
                ucAttachFileControl1.RegisterationId = _personId;
                ucAttachFileControl1.CurrentUser = (UserProfile == null) ? "" : UserProfile.LoginName;
            }

        }
        public void CheckSession()
        {
            if (Session[PageList.UserProfile] == null)
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
            if (Request.QueryString["Mode"] == null)
            {

                InitOnViewMode();
            }
            else
            {
                switch (Request.QueryString["Mode"].ToString())
                {

                    case "E": InitOnEditMode();
                        break;
                    case "V": InitOnViewMode();
                        break;
                    default: InitOnViewMode();
                        break;
                }
            }

            
        }

        private void InitOnEditMode()
        {
            CheckSession();
            this.PersonId = UserProfile.Id;
            ViewState["ActionMode"] = "E";
            btnOk.Visible = true;
            btnCancel.Visible = true;
            InitAttachFileControl();
            GetAttatchFiles();
            Label L_lblMessage = (Label)Detail.FindControl("lblMessage"); //milk
            if (L_lblMessage.Text == Resources.propMasterPerson_001)
            {
                PnlAddButton.Visible = false;
            }//milk

            PnlResultReg.Visible = false;

            //this.ddlAgentTypeInit(PersonGeneral.FindControl("ddlAgentType") as DropDownList);
        }

        private void InitOnViewMode()
        {
            CheckSession();
            this.PersonId = UserProfile.Id;
            ViewState["ActionMode"] = "V";
            btnCancel.Visible = false;
            btnOk.Visible = false;
            InitAttachFileControl();
            GetAttatchFiles();

            //this.ddlAgentTypeInit(PersonGeneral.FindControl("ddlAgentType") as DropDownList);
        }

        public void InitAttachFileControl()
        {
            GetAttachFilesType();
            ucAttachFileControl1.RegisterationId = this.PersonId;
        }

        public void GetAttatchFiles()
        {
            var biz = new BLL.PersonBiz();
  
            DTO.ResponseService<DTO.PersonAttatchFile[]> res = biz.GetUserProfileAttatchFileByPersonId(PersonId);

            var list = res.DataResponse.ToList();

            ucAttachFileControl1.AttachFiles = list.ConvertToAttachFilesView();

            
            UpdatePanelUpload.Update();
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

        //View Mode
        public DTO.Person GetPersonal()    
        {
            BLL.PersonBiz regBiz = new BLL.PersonBiz();
            var res = regBiz.GetById(UserProfile.Id);
            if (res.IsError)
            {
                ModelError.ShowMessageError = res.ErrorMsg;
                ModelError.ShowModalError();
            }
            this.PersonId = res.DataResponse.ID;
            return res.DataResponse;
        }

        //Edit Mode
        public DTO.PersonTemp GetPersonTemp()
        {
            BLL.PersonBiz regBiz = new BLL.PersonBiz();
            var res = regBiz.GetPersonTemp(UserProfile.Id);
            if (res.IsError)
            {
                ModelError.ShowMessageError = res.ErrorMsg;
                ModelError.ShowModalError();
            }
            this.PersonId = res.DataResponse.ID;
            //Assign CreateDate
            this.CreateDateTemp = res.DataResponse.CREATED_DATE;
            return res.DataResponse;
        }

        /// <summary>
        /// SetValidateGroup
        /// Set GroupValidation into Session
        /// Do not remove
        /// Func renew by Nattapong @ 05/11/2556
        /// </summary>
        /// <param name="reqFormat"></param>
        public void SetValidateGroup(string reqFormat)
        {
            this.GroupValidation = reqFormat;
            this.ButtonOk.ValidationGroup = reqFormat;
            //this.ButtonOk.ValidationGroup = reqFormat;

        }

        private string CurrentPage()
        {
            //Get page current
            string Path = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            System.IO.FileInfo Info = new System.IO.FileInfo(Path);
            string pageName = Info.Name;

            string[] pagerS = pageName.Split('.');
            if (pagerS != null)
            {
                if (pagerS[0].Substring(6).Equals("General"))
                {
                    Session["CurrentPage"] = "1";
                }
                else if (pagerS[0].Substring(6).Equals("Company"))
                {
                    Session["CurrentPage"] = "2";
                }
                else if (pagerS[0].Substring(6).Equals("Associate"))
                {
                    Session["CurrentPage"] = "3";
                }
            }

            return pageName;

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


        /// <summary>
        /// RequiredFieldValidator & RegularExpressionValidator Validation Function
        /// Added new & Last Update 31/10/2556
        /// by Nattapong
        /// </summary>
        /// <param name="registrationType"></param>
        /// <param name="registration"></param>
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
                                strBuild.Append(child.Validators[i].ErrorMessage + "<br/>");
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
        /// Control Validate Before Edit
        /// If CurPage = "1" is GeneralMemberType must to validate birthday before submit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOk_Click(object sender, EventArgs e)
        {
            //General BirthDay Validation
            //CurrentPage Check
            if ((this.CurPage != null) && (this.CurPage != ""))
            {
                //PersonGeneral
                if (this.CurPage.Equals("1"))
                {
                    //Control Validation
                    //DTO.ResponseMessage<bool> res = this.ControlValidationBeforeSubmit(this.Page);
                    DTO.ResponseMessage<bool> resBirthDay = this.DateValidation(PersonGeneral.TextboxBirthDay.Text);
                    if ((resBirthDay.ResultMessage == false) || (resBirthDay.IsError))
                    {
                        ModelError.ShowMessageError = resBirthDay.ErrorMsg;
                        ModelError.ShowModalError();
                        return;
                    }
                }
                
                //Control Validation
                DTO.ResponseMessage<bool> res = this.ControlValidationBeforeSubmit(this.Page);
                if (res.ResultMessage == true)
                {
                    if (OkRegister_Click != null)
                    {
                        OkRegister_Click(sender, e);
                    }
                }
                if ((res.ResultMessage == false) || (res.IsError))
                {
                    ModelError.ShowMessageError = res.ErrorMsg;
                    ModelError.ShowModalError();
                    return;
                }
            }

            //PnlAddButton.Visible = false;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //Response.Redirect("~/Person/" + CurrentPage() + "?Mode=V");
            Response.Redirect("~/BankPage.aspx"); 
            if (CancelRegister_Click != null)
                CancelRegister_Click(sender, e);
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/BankPage.aspx"); 

        }

        public void EditPersonal(DTO.MemberType memberType, DTO.PersonTemp personal) 
        {
            var biz = new BLL.PersonBiz();
            String res = ucAttachFileControl1.ValidDocRequire();
            if (String.IsNullOrEmpty(res))
            {
                var final = biz.SetPersonTemp(personal, ucAttachFileControl1.AttachFiles.Where(p => p.FILE_STATUS != "A").ConvertToPersonAttatchFiles().ToArray());
                if ((final.IsError) || (final.ResultMessage == false))
                {
                    UCModalError.ShowMessageError = final.ErrorMsg;
                    UCModalError.ShowModalError();
                }
                else if (final.ResultMessage == true)
                {

                    UCModalSuccess.ShowMessageSuccess = SysMessage.SaveSucess;
                    UCModalSuccess.ShowModalSuccess();
                    btnClose.Visible = true;
                    btnCancel.Visible = false;
                    btnOk.Visible = false;
                    //Response.Redirect("~/BankPage.aspx"); 

                }

            }
            else
            {
                ModelError.ShowMessageError = res;
                ModelError.ShowModalError();
            }

        }

        public void EditAssociate(DTO.MemberType memberType, DTO.PersonTemp personal)
        {
            var biz = new BLL.PersonBiz();
            String res = ucAttachFileControl1.ValidDocRequire();
            if (String.IsNullOrEmpty(res))
            {
                var final = biz.SetPersonTemp(personal, ucAttachFileControl1.AttachFiles.Where(p => p.FILE_STATUS != "A").ConvertToPersonAttatchFiles().ToArray());
                if ((final.IsError) || (final.ResultMessage == false))
                {
                    UCModalError.ShowMessageError = final.ErrorMsg;
                    UCModalError.ShowModalError();
                }
                else if (final.ResultMessage == true)
                {
                    UCModalSuccess.ShowMessageSuccess = SysMessage.SaveSucess;
                    UCModalSuccess.ShowModalSuccess();
                    btnClose.Visible = true;
                    btnCancel.Visible = false;
                    btnOk.Visible = false;
                }

            }
            else 
            {
                ModelError.ShowMessageError = res;
                ModelError.ShowModalError();
            }
            //var biz = new BLL.PersonBiz();
            //DTO.ResponseMessage<bool> res = ucAttachFileControl1.DocRequire();
            //if (res.ResultMessage == true)
            //{
            //    var final = biz.SetPersonTemp(personal, ucAttachFileControl1.AttachFiles.Where(p => p.FILE_STATUS != "A").ConvertToPersonAttatchFiles().ToArray());
            //    if (final.IsError)
            //    {
            //        UCModalError.ShowMessageError = final.ErrorMsg;
            //        UCModalError.ShowModalError();
            //    }

            //}
            //else if ((res.ResultMessage == false) || (res.IsError))
            //{
            //    ModelError.ShowMessageError = SysMessage.FileRequire + res.ErrorMsg;
            //    ModelError.ShowModalError();
            //}

        }

        public void EditCompany(DTO.MemberType memberType, DTO.PersonTemp personal)
        {
            //var biz = new BLL.PersonBiz();
            //DTO.ResponseMessage<bool> res =  ucAttachFileControl1.DocRequire();
            //if (res.ResultMessage == true)
            //{
            //    var final = biz.SetPersonTemp(personal, ucAttachFileControl1.AttachFiles.Where(p => p.FILE_STATUS != "A").ConvertToPersonAttatchFiles().ToArray());

            //    if (final.IsError)
            //    {
            //        UCModalError.ShowMessageError = final.ErrorMsg;
            //        UCModalError.ShowModalError();

            //    }
            //}
            //else if ((res.ResultMessage == false) || (res.IsError))
            //{
            //    ModelError.ShowMessageError = SysMessage.FileRequire + res.ErrorMsg;
            //    ModelError.ShowModalError();
            //}
            var biz = new BLL.PersonBiz();
            String res = ucAttachFileControl1.ValidDocRequire();
            if (String.IsNullOrEmpty(res))
            {
                 var final = biz.SetPersonTemp(personal, ucAttachFileControl1.AttachFiles.Where(p => p.FILE_STATUS != "A").ConvertToPersonAttatchFiles().ToArray());
                if ((final.IsError) || (final.ResultMessage == false))
                {
                    UCModalError.ShowMessageError = final.ErrorMsg;
                    UCModalError.ShowModalError();
                }
                else if (final.ResultMessage == true)
                {
                    UCModalSuccess.ShowMessageSuccess = SysMessage.SaveSucess;
                    UCModalSuccess.ShowModalSuccess();
                    btnClose.Visible = true;
                    btnCancel.Visible = false;
                    btnOk.Visible = false;
                }
            }
            else
            {
                ModelError.ShowMessageError = res;
                ModelError.ShowModalError();
            }

        }

        #endregion

    }
}
