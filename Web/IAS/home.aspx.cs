using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;
using System.Web.UI;
using IAS.BLL;
using IAS.DTO;
using IAS.Properties;
using IAS.Utils;
using IAS.Common.Logging;

namespace IAS
{
    public partial class home : System.Web.UI.Page
    {
        public string CurrentUserName
        {
            get { return (string)Session["currentusername"] == null ? "" : Session["currentusername"].ToString(); }
            set { Session["currentusername"] = value; }

        }

        public string gotoPage
        {
            get { return (string)Session["_gotoPage"] == null ? "" : Session["_gotoPage"].ToString(); }
            set { Session["_gotoPage"] = value; }
        }

        protected string GetApplicationVersion()
        {
            return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session.Abandon();
            }
            
            ToolkitScriptManager1.Scripts.Add(new ScriptReference("../Scripts/Scripts01042013/jquery-1.4.1.js"));
            ToolkitScriptManager1.Scripts.Add(new ScriptReference("../Scripts/Scripts01042013/jquery/development-bundle/ui/ui.core.js"));
            ToolkitScriptManager1.Scripts.Add(new ScriptReference("../Scripts/Scripts01042013/jquery/development-bundle/ui/ui.tabs.js"));
            ToolkitScriptManager1.Scripts.Add(new ScriptReference("../Scripts/Scripts01042013/jquery/development-bundle/ui/effects.core.js"));
            ToolkitScriptManager1.Scripts.Add(new ScriptReference("../Scripts/Scripts01042013/jquery/development-bundle/ui/ui.datepicker.js"));
            ToolkitScriptManager1.Scripts.Add(new ScriptReference("../Scripts/Scripts01042013/jquery/development-bundle/ui/i18n/ui.datepicker-th.js"));
            ToolkitScriptManager1.Scripts.Add(new ScriptReference("../Scripts/Scripts01042013/jquery/numberFormatter/jshashtable-2.1/jshashtable.js"));
            ToolkitScriptManager1.Scripts.Add(new ScriptReference("../Scripts/Scripts01042013/jquery/jquery.numeric/jquery.numeric.js"));
            ToolkitScriptManager1.Scripts.Add(new ScriptReference("../Scripts/Scripts01042013/jquery/maskmoney/jquery.maskMoney.js"));
            ToolkitScriptManager1.Scripts.Add(new ScriptReference("../Scripts/Scripts01042013/ValidatorInputKey.js"));
            ToolkitScriptManager1.Scripts.Add(new ScriptReference("../Scripts/formatTextBox.js"));
            string appForOIC = System.Configuration.ConfigurationManager.AppSettings["APP_FOR_OIC"] == null
                          ? "No"
                          : System.Configuration.ConfigurationManager.AppSettings["APP_FOR_OIC"].ToUpper();
            if (appForOIC == "YES")
            {
                regisId.Visible = false;
                middle.Visible = false;
                forgetpass.Visible = false;
            }

            if (Session["SessionLost"] != null)
            {
                Session.Abandon();
                pnlMessageModalError.Visible = true;
            }

        }

        public IAS.MasterPage.Site1 MasterSite
        {
            get
            {
                return (this.Page.Master as IAS.MasterPage.Site1);
            }

        }

        public void SetSessionLogin(DTO.UserProfile profile)
        {
            HttpContext.Current.Session["LogingName"] = profile.LoginName;
            HttpContext.Current.Session["OICUserId"] = profile.OIC_User_Id;
            HttpContext.Current.Session["DeptCode"] = profile.DeptCode;
            HttpContext.Current.Session["CompanyCode"] = profile.CompCode;
            HttpContext.Current.Session["CreatedBy"] = profile.Id;
            //AD Service
            HttpContext.Current.Session["DepartmentCode"] = profile.DepartmentCode;
            HttpContext.Current.Session["DepartmentName"] = profile.DepartmentName;
            HttpContext.Current.Session["EmployeeCode"] = profile.EmployeeCode;
            HttpContext.Current.Session["EmployeeName"] = profile.EmployeeName;
            HttpContext.Current.Session["PositionCode"] = profile.PositionCode;
            HttpContext.Current.Session["PositionName"] = profile.PositionName;
        }

        private string UserAuthen(string appForOIC)
        {
            string toPage = "home.aspx"; // string.Empty;

            PersonBiz biz = new PersonBiz();

            //ใช้จริง

            DTO.ResponseService<DTO.UserProfile> res = biz.UserAuthen(txtUserName.Text.Trim(), txtPassword.Text, (appForOIC == "YES"), GetIPAddress());

            if (res.DataResponse != null)
            {
                //Set UserName & Type on Site1
                //this.CurrentUserName = res.DataResponse.LoginName;
                //this.SetCurrent(HttpContext.Current.Session.SessionID, res.DataResponse.LoginName, res.DataResponse.LoginStatus, res.DataResponse.MemberType);

                Session[PageList.UserProfile] = res.DataResponse;
                Session["LoginUser"] = res.DataResponse.LoginName;
                SetSessionLogin(res.DataResponse);
                LoggerFactory.CreateLog().LogInfo(String.Format("[{0}] User Login: {1} ", GetIPAddress(), res.DataResponse.LoginName));
                if (res.DataResponse.MemberType == DTO.RegistrationType.General.GetEnumValue())
                {
                    //ใช้จริง
                    //toPage = PageList.PersonRegister;

                    //tob edit 04052013
                    toPage = PageList.FirstPage;


                }
                else if (res.DataResponse.MemberType == DTO.RegistrationType.Insurance.GetEnumValue())
                {
                    //ใช้จริง
                    //toPage = PageList.CompanyRegister;

                    //tob edit 04052013
                    toPage = PageList.FirstPage;
                }
                else if (res.DataResponse.MemberType == DTO.RegistrationType.Association.GetEnumValue())
                {
                    //ใช้จริง
                    //toPage = PageList.AssociateRegister;

                    //tob edit 04052013
                    toPage = PageList.FirstPage;
                }
                else if (res.DataResponse.MemberType == DTO.RegistrationType.OIC.GetEnumValue())
                {
                    //ใช้จริง
                    //toPage = PageList.OICRegister;

                    //tob edit 04052013
                    toPage = PageList.FirstPage;
                }
                else if (res.DataResponse.MemberType == DTO.RegistrationType.TestCenter.GetEnumValue())
                {
                    //if (res.DataResponse.STATUS == "99")
                    //{
                    //    toPage = PageList.ChangePassword;
                    //}
                    //else
                    //{
                        //tob edit 27062013
                        toPage = PageList.FirstPage;
                    //}

                }
                else if (res.DataResponse.MemberType == DTO.RegistrationType.OICFinace.GetEnumValue())
                {
                    //tob edit 27062013
                    toPage = PageList.FirstPage;
                }
                else if (res.DataResponse.MemberType == DTO.RegistrationType.OICAgent.GetEnumValue())
                {
                    //tob edit 27062013
                    toPage = PageList.FirstPage;
                }
            }
            return toPage;

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
         

            //ตรวยสอบ Key APP_FOR_OIC ใน Web.config ถ้าไม่มีให้ Default = No
            string appForOIC = System.Configuration.ConfigurationManager.AppSettings["APP_FOR_OIC"] == null
                                    ? "No"
                                    : System.Configuration.ConfigurationManager.AppSettings["APP_FOR_OIC"].ToUpper();

            string toPage = UserAuthen(appForOIC);

            if (toPage != "home.aspx")
            {
                var memberType = (DTO.MemberType)((DTO.UserProfile)Session[PageList.UserProfile]).MemberType;
                if (memberType == DTO.MemberType.Insurance || memberType == DTO.MemberType.Association || memberType == DTO.MemberType.TestCenter)
                {
                    gotoPage = toPage;
                    CheckChangePassword();
                }
                else
                {                    
                    Response.Redirect(toPage);
                }
                
            }
            else
            {
                //biz.UserAuthen(txtUserName.Text, txtPassword.Text);
                PersonBiz biz = new PersonBiz();

                //ใช้จริง
                DTO.ResponseService<DTO.UserProfile> res = biz.UserAuthen(txtUserName.Text.Trim(), txtPassword.Text, (appForOIC == "YES"),GetIPAddress());
                if (res.IsError)
                {
                    AlertMessage.ShowAlertMessage(string.Empty, res.ErrorMsg);
                }
                else
                {
                    AlertMessage.ShowAlertMessage(string.Empty, Resources.infohome_001);
                }


            }
        }

        protected void lbtRegister_Click(object sender, EventArgs e)
        {
            Session.Abandon();

            string PageReg = "Register/regExplain.aspx";

            Response.Redirect(PageReg);
        }

        protected void lbtForgetPassword_Click(object sender, EventArgs e)
        {
            Session.Abandon();

            string PageReg = "ForgetPassword/ForgetPass.aspx";

            Response.Redirect(PageReg);
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            pnlMessageModalError.Visible = false;
        }

        private void SetCurrent(string SessionID, string LoginName, string LoginStatus, int MemType)
        {
            SessionsState sessionState = new SessionsState();
            SerializationInfo info = new SerializationInfo(this.GetType(), new System.Runtime.Serialization.FormatterConverter());
            StreamingContext context = new StreamingContext(System.Runtime.Serialization.StreamingContextStates.All);
            info.AddValue("ISessionID", SessionID);
            info.AddValue("IUserName", LoginName);
            info.AddValue("ILoginStatus", LoginStatus);
            info.AddValue("IMemType", MemType);
            sessionState.GetObjectData(info, context);

            //SessionsState sessionState = new SessionsState();
            //sessionState.GetObjectData(sessionID, LoginName, LoginStatus, MemType);
        }

        #region Change Password
        private void CheckChangePassword()
        {
            AccountBiz Accbiz = new AccountBiz();
            var userProfile = (DTO.UserProfile)Session[PageList.UserProfile];
            var res = Accbiz.IsChangePassword(userProfile);
            if (!res.IsError)
            {
                // จะต้องเปลี่ยนรหัสผ่าน
                if (res.ResultMessage)
                {
                    ClearControlPwd();
                    PopUpEditPassword.Show();
                }
                else
                {
                    Response.Redirect(gotoPage);
                }
            }
            else
            {
                AlertMessage.ShowAlertMessage(string.Empty, res.ErrorMsg);
            }
        }

        protected void btnPwdCancel_Click(object sender, EventArgs e)
        {
            PersonBiz biz = new PersonBiz();
            var curUserName = HttpContext.Current.Session["currentusername"];

            if (curUserName != null)
            {
                ResponseMessage<bool> res = biz.SetOffLineStatus(curUserName.ToString());

                if (res.ResultMessage == true)
                {
                    Session["currentusername"] = null;
                }
            }

            Session.Abandon();
        }

        protected void btnPwdSave_Click(object sender, EventArgs e)
        {
            try
            {
                string valid = Validation.Validate(Page, "ChangePW");
                if (RegPassValidation().ResultMessage == false)
                {
                    ShowErrorMsg("ผิดพลาด", RegPassValidation().ErrorMsg);
                }
                else
                {
                    var userProfile = (DTO.UserProfile)Session[PageList.UserProfile];
                    DTO.User user = new User();
                    user.USER_ID = userProfile.Id;
                    user.USER_PASS = txtOldpassword.Text;

                    AccountBiz biz = new AccountBiz();
                    var res = biz.ChangePassword(user, txtNewPassword.Text);
                    if (!res.IsError)
                    {
                        ShowSuccessMsg("เรียบร้อย", "เปลี่ยนรหัสผ่านเรียบร้อยแล้ว");
                    }
                    else
                    {
                        ShowErrorMsg("ผิดพลาด", res.ErrorMsg);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErrorMsg("ผิดพลาด", ex.Message);
            }
        }

        protected void ShowErrorMsg(string title, string message)
        {
            lblErrorTitle.Text = title;
            lblErrorMsg.Text = message;
            ModalPopupError.Show();
        }

        private DTO.ResponseMessage<bool> RegPassValidation()
        {
            DTO.ResponseMessage<bool> res = new DTO.ResponseMessage<bool>();
            try
            {

                this.Page.Validate("ChangePW");
                if (this.Page.IsValid)
                {
                    res.ResultMessage = true;
                }
                else
                {
                    if (this.reqOldPW.IsValid == false)
                    {
                        res.ErrorMsg = this.reqOldPW.ErrorMessage;
                        res.ResultMessage = false;
                    }
                    else if (this.reqNewPW.IsValid == false)
                    {
                        res.ErrorMsg = this.reqNewPW.ErrorMessage;
                        res.ResultMessage = false;
                    }
                    else if (this.reqRegNewPW.IsValid == false)
                    {
                        res.ErrorMsg = this.reqRegNewPW.ErrorMessage;
                        res.ResultMessage = false;
                    }
                    else if (this.reqConfirmPW.IsValid == false)
                    {
                        res.ErrorMsg = this.reqConfirmPW.ErrorMessage;
                        res.ResultMessage = false;
                    }
                    else if (this.reqRegConfirmPW.IsValid == false)
                    {
                        res.ErrorMsg = this.reqRegConfirmPW.ErrorMessage;
                        res.ResultMessage = false;
                    }
                    else if (this.reqComparePW.IsValid == false)
                    {
                        res.ErrorMsg = this.reqComparePW.ErrorMessage;
                        res.ResultMessage = false;
                    }
                    else if ((this.reqNewPW.IsValid == true) && (this.reqRegNewPW.IsValid == true) && (this.reqConfirmPW.IsValid == true)
                        && (this.reqRegConfirmPW.IsValid == true) && (this.reqComparePW.IsValid == true))
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

        protected void btnCloseError_Click(object sender, EventArgs e)
        {
            PopUpEditPassword.Show();
        }

        protected void ShowSuccessMsg(string title, string message)
        {
            lblSuccessTitle.Text = title;
            lblSuccessMsg.Text = message;
            ModalPopupSuccess.Show();
        }

        protected void btnCloseSuccess_Click(object sender, EventArgs e)
        {
            Response.Redirect(gotoPage);
        }

        protected void ClearControlPwd()
        {
            txtOldpassword.Text = "";
            txtNewPassword.Text = "";
            txtConfirmPassword.Text = "";
        }
        #endregion Change Password

        public String GetIPAddress()
        {
            String ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (string.IsNullOrEmpty(ip))
            {
                ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            System.Web.HttpBrowserCapabilities browser = Request.Browser;
            return ip+" "+browser.Type;
        }
    }
}