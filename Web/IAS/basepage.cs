using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IAS.Utils;
using IAS.DTO;
using System.Configuration;
using IAS.MasterPage;
using AjaxControlToolkit;
using IAS.BLL;

namespace IAS
{
    public class basepage : System.Web.UI.Page
    {
        protected string ReportFilePath_Key
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ReportFilePath"];
            }
        }

        protected string PDFPath_Key
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["PDFFilePath"];
            }
        }

        protected string PDFPath_Temp_Key
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["PDFFilePath_Temp"];
            }
        }

        protected string PDFPath_OIC_Key
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["PDFFilePath_OIC"];
            }
        }

        protected string PDFPath_Users_Key
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["PDFFilePath_Users"];
            }
        }

        protected string UploadFilePath_Key
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["UploadFilePath"];
            }
        }

        protected string UploadTempPath_Key
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["UploadTempPath"];
            }
        }

        protected string AgreementFilePath_Key
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["AgreementFilePath"];
            }
        }

        protected string UploadRecieveLicense_Key
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["UploadRecieveLicense"];
            }
        }
        protected Int32 PAGE_SIZE_Key
        {
            get
            {
                return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PAGE_SIZE"]);
            }
        }
        protected Int32 EXCEL_SIZE_Key
        {
            get
            {
                return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["EXCEL_SIZE"]);
            }
        }
        protected string UploadRecieveLicense
        {
            get
            {
                return Server.MapPath(this.UploadRecieveLicense_Key);
            }
        }

        protected string UploadTempPath
        {
            get
            {
                return Server.MapPath(this.UploadTempPath_Key);
            }
        }

        protected string ReportFilePath
        {
            get
            {
                return Server.MapPath(this.ReportFilePath_Key);
            }
        }

        protected string PDFPath
        {
            get
            {
                return Server.MapPath(this.PDFPath_Key);
            }
        }

        protected string PDFPath_Temp
        {
            get
            {
                return Server.MapPath(this.PDFPath_Temp_Key);
            }
        }

        protected string PDFPath_OIC
        {
            get
            {
                return Server.MapPath(this.PDFPath_OIC_Key);
            }
        }

        protected string PDFPath_Users
        {
            get
            {
                return Server.MapPath(this.PDFPath_Users_Key);
            }
        }

        protected string UploadFilePath
        {
            get
            {
                return Server.MapPath(this.UploadFilePath_Key);
            }
        }

        protected string AgreementFilePath
        {

            get
            {
                return Server.MapPath(this.AgreementFilePath_Key);
            }
        }

        protected DTO.UserProfile UserProfile
        {
            get
            {
                return (DTO.UserProfile)Session[PageList.UserProfile];
            }
        }
        protected DTO.RegistrationType UserRegType
        {
            get
            {
                DTO.RegistrationType result = RegistrationType.General;
                DTO.UserProfile userProfile = this.UserProfile;

                if (userProfile.MemberType == DTO.RegistrationType.Insurance.GetEnumValue())
                {
                    result = RegistrationType.Insurance;
                }
                else if (userProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue())
                {
                    result = RegistrationType.Association;
                }
                else if (userProfile.MemberType == DTO.RegistrationType.OIC.GetEnumValue())
                {
                    result = RegistrationType.OIC;
                }
                else if (userProfile.MemberType == DTO.RegistrationType.TestCenter.GetEnumValue())
                {
                    result = RegistrationType.TestCenter;
                }
                else if (userProfile.MemberType == DTO.RegistrationType.OICAgent.GetEnumValue())
                {
                    result = RegistrationType.OICAgent;
                }
                return result;
            }
        }

        protected string UserId
        {
            get
            {
                return this.UserProfile.Id;
            }
        }

        protected string IdCard
        {
            get
            {
                return this.UserProfile.IdCard;
            }
        }

        protected string UserNameLastName
        {
            get
            {
                return this.UserProfile.Name;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            CheckSession();
            base.OnLoad(e);
        }

        public void CheckSession()
        {
            if (Session[PageList.UserProfile] == null)
            {
                //Get Sessions State for Logout
                this.SetLogoutStatus();

                Session["SessionLost"] = true;
                Response.Redirect(PageList.Home);
            }
        }

        protected override void OnError(EventArgs e)
        {
            //CheckSession();
            base.OnError(e);
            //Response.Redirect(PageList.UnderConstruction);
        }

        protected void HasPermit()
        {
            CheckSession();
            if (!CanAccess())
                Response.Redirect(PageList.Home);
        }

        private bool CanAccess()
        {
            var ent = this.UserProfile
                             .CanAccessSystem
                             .Where(delegate(DTO.FunctionMenu menu)
                             {
                                 return Request.CurrentExecutionFilePath.LastIndexOf(menu.FunctionName) > -1;
                             })
                             .FirstOrDefault();

            //Log Keeping
            if (ent != null)
            {
                var biz = new BLL.DataCenterBiz();
                biz.InsertLogOpenMenu(this.UserId, ent.FunctionId);

                if (this.UserProfile != null && ("1_2_3_7").Contains( this.UserProfile.MemberType.ToString()) ) {
                    PersonBiz bizPerson = new PersonBiz();
                    DTO.ResponseService<DTO.UserProfile> res = bizPerson.RefeshUserProfileStatus(this.UserProfile);
                    Session[PageList.UserProfile] = res.DataResponse;
                    SetSessionLogin(UserProfile);
                }
                
            }

            return ent != null;
        }

        public void SetSessionLogin(DTO.UserProfile profile) {
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

        public void SetSessionLogout()
        {
            HttpContext.Current.Session["LogingName"] = null;
            HttpContext.Current.Session["OICUserId"] = null;
            HttpContext.Current.Session["DeptCode"] = null;
            HttpContext.Current.Session["CompanyCode"] = null;
            HttpContext.Current.Session["CreatedBy"] = null;
            //AD Service
            HttpContext.Current.Session["DepartmentCode"] = null;
            HttpContext.Current.Session["DepartmentName"] = null;
            HttpContext.Current.Session["EmployeeCode"] = null;
            HttpContext.Current.Session["EmployeeName"] = null;
            HttpContext.Current.Session["PositionCode"] = null;
            HttpContext.Current.Session["PositionName"] = null;
        }

        private void SetLogoutStatus()
        {
            PersonBiz biz = new PersonBiz();
            //var curUserName = SessionsState.IUserName;

            if (HttpContext.Current.Session != null && HttpContext.Current.Session.IsNewSession)
            {
                if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies["ASP.NET_SessionId"] != null)
                {
                    //Get User && SessionID from Auth
                    if ((SessionsState.IUserName != null) && (SessionsState.ISessionID.Equals(HttpContext.Current.Session.SessionID)))
                    {
                        ResponseMessage<bool> res = biz.SetOffLineStatus(SessionsState.IUserName);

                        if (res.ResultMessage == true)
                        {
                            Session["currentusername"] = null;
                            SetSessionLogout();
                        }

                    }
                }
            }

        }

    }
}
