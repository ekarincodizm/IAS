using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using IAS.BLL;
using IAS.DTO;
using IAS.Properties;
using IAS.Utils;

namespace IAS.MasterPage
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        #region Public Param & Session
        public ContentPlaceHolder ContentDetails { get { return Detail; } }
        private string ApproveY = Resources.propSite1_ApproveY;
        private string ApproveN = Resources.propSite1_ApproveN;
        public IAS.UserControl.UCModalError ModelError { get { return ucModelError; } }
        public IAS.UserControl.UCModalSuccess ModelSuccess
        {
            get
            {
                return ucModelSuccess;
            }
        }

        public Label LabelUser { get { return lblUser; } set { lblUser = value; } }

        public HtmlGenericControl BodyTag
        {
            get
            {
                return MasterPageBodyTag;
            }
            set
            {
                MasterPageBodyTag = value;
            }
        }

        public string GlobalSession
        {
            get { return (string)Session["globalsession"] == null ? "" : Session["globalsession"].ToString(); }
            set { Session["globalsession"] = value; }

        }


        public IAS.home HomePage
        {
            get { return (this.Page as IAS.home); }
        }

        public static List<string> lsString = new List<string>();

        public static List<string> GetPeopleFromSession()
        {
            var people = HttpContext.Current.Session["currentusername"] as List<string>;
            //Create new, if null
            if (people == null)
                people = new List<string>();
            return people;
        }

        #endregion

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {

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

            HtmlGenericControl body = (HtmlGenericControl)this.FindControl("MasterPageBodyTag");
            if (body != null)
            {
                // MasterPageBodyTag.Attributes.Add("onunload", "bodyUnload();");
                // MasterPageBodyTag.Attributes.Add("onclick", "clicked=true;"); 
            }
            //BodyTag.Attributes.Add("onunload", "bodyUnload();");
            //BodyTag.Attributes.Add("onclick", "clicked=true;");


            ShowMenu();
            StatusInit();
        }
        #endregion

        #region Private & Public Function
        protected string GetApplicationVersion()
        {
            return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        private void ShowMenu()
        {
            string toPage = string.Empty;
            if (Session[PageList.UserProfile] != null)
            {
                DTO.UserProfile userProfile = (DTO.UserProfile)Session[PageList.UserProfile];
                Session["LoginUser"] = userProfile.LoginName;

                if (userProfile.IS_APPROVE != "N")
                {
                    if (userProfile != null)
                    {
                        if (userProfile.MemberType == DTO.RegistrationType.General.GetEnumValue())
                        {
                            mnuPerson.Visible = true;

                        }
                        else if (userProfile.MemberType == DTO.RegistrationType.Insurance.GetEnumValue())
                        {
                            mnuCompany.Visible = true;
                        }
                        else if (userProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue())
                        {
                            mnuAssociation.Visible = true;
                        }
                        else if (userProfile.MemberType == DTO.RegistrationType.OIC.GetEnumValue())
                        {
                            mnuOIC.Visible = true;
                        }
                        else if (userProfile.MemberType == DTO.RegistrationType.TestCenter.GetEnumValue())
                        {
                            mnuExamPlaceGroup.Visible = true;
                        }
                        else if (userProfile.MemberType == DTO.RegistrationType.OICFinace.GetEnumValue())
                        {
                            mnuOICFinace.Visible = true;
                        }
                        else if (userProfile.MemberType == DTO.RegistrationType.OICAgent.GetEnumValue())
                        {
                            mnuOICAgent.Visible = true;
                        }
                    }
                }
                else
                {
                    if (userProfile.MemberType == DTO.RegistrationType.General.GetEnumValue())
                    {
                        mnuNewGeneral.Visible = true;
                    }
                    else if (userProfile.MemberType == DTO.RegistrationType.Insurance.GetEnumValue())
                    {
                        mnuNewCompany.Visible = true;
                    }
                    else if (userProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue())
                    {
                        mnuNewAssociate.Visible = true;
                    }

                }


            }
        }

        private void StatusInit()
        {
            PersonBiz biz = new PersonBiz();
            DTO.UserProfile userProfile = (DTO.UserProfile)Session[PageList.UserProfile];

            if (userProfile != null)
            {
                //DTO.ResponseService<DTO.Person> res = biz.GetById(userProfile.IdCard);

                if (userProfile.MemberType.Equals((int)DTO.MemberType.General))
                {
                    DTO.ResponseService<DTO.Person> res = biz.GetUserProfile(userProfile.Id, userProfile.MemberType.ToString());


                    if (res.DataResponse != null)
                    {
                        string name;
                        name = Chklength(res.DataResponse);
                        lblUser.Text = name;

                    }
                }
                else if (userProfile.MemberType.Equals((int)DTO.MemberType.Insurance))
                {
                    DTO.ResponseService<DTO.Person> res = biz.GetUserProfile(userProfile.Id, userProfile.MemberType.ToString());
                    if (res.DataResponse != null)
                    {
                        string name;
                        name = Chklength(res.DataResponse);
                        lblUser.Text = name;
                        //res.DataResponse = Chklength(res.DataResponse);
                        //lblUser.Text = res.DataResponse.NAMES + " " + res.DataResponse.LASTNAME;

                    }
                }
                else if (userProfile.MemberType.Equals((int)DTO.MemberType.Association))
                {
                    DTO.ResponseService<DTO.Person> res = biz.GetUserProfile(userProfile.Id, userProfile.MemberType.ToString());
                    if (res.DataResponse != null)
                    {
                        string name;
                        name = Chklength(res.DataResponse);
                        lblUser.Text = name;
                        //  res.DataResponse = Chklength(res.DataResponse);
                        //  lblUser.Text = res.DataResponse.NAMES + " " + res.DataResponse.LASTNAME;

                    }
                }
                else if (userProfile.MemberType.Equals((int)DTO.MemberType.OIC))
                {
                    DTO.ResponseService<DTO.Person> res = biz.GetById(userProfile.Id);
                    if (res.DataResponse != null)
                    {
                        string name;
                        name = Chklength(res.DataResponse);
                        lblUser.Text = name;
                        //  res.DataResponse = Chklength(res.DataResponse);
                        //  lblUser.Text = res.DataResponse.NAMES + " " + res.DataResponse.LASTNAME;

                    }

                }
                else if (userProfile.MemberType.Equals((int)DTO.MemberType.OICAgent))
                {
                    DTO.ResponseService<DTO.Person> res = biz.GetById(userProfile.Id);
                    if (res.DataResponse != null)
                    {
                        string name;
                        name = Chklength(res.DataResponse);
                        lblUser.Text = name;
                        // res.DataResponse = Chklength(res.DataResponse);
                        //  lblUser.Text = res.DataResponse.NAMES + " " + res.DataResponse.LASTNAME;

                    }

                }
                else if (userProfile.MemberType.Equals((int)DTO.MemberType.OICFinance))
                {
                    DTO.ResponseService<DTO.Person> res = biz.GetById(userProfile.Id);
                    if (res.DataResponse != null)
                    {
                        string name;
                        name = Chklength(res.DataResponse);
                        lblUser.Text = name;
                        //  res.DataResponse = Chklength(res.DataResponse);
                        // lblUser.Text = res.DataResponse.NAMES + " " + res.DataResponse.LASTNAME;

                    }

                }
                else if (userProfile.MemberType.Equals((int)DTO.MemberType.TestCenter))
                {
                    DTO.ResponseService<DTO.Person> res = biz.GetById(userProfile.Id);
                    if (res.DataResponse != null)
                    {
                        string name;
                        name = Chklength(res.DataResponse);
                        lblUser.Text = name;
                        //  res.DataResponse = Chklength(res.DataResponse);
                        //  lblUser.Text = res.DataResponse.NAMES + " " + res.DataResponse.LASTNAME;

                    }

                }

                // this.lblUser.Text = "ชื่อผู้ใช้ : " + userProfile.Name + " "+ userProfile.LastName+" ("+ userProfile.MemberType +")";

                //this.lblUser.Text = ((userProfile.LastName == null)? userProfile.Name : userProfile.Name + " " + userProfile.LastName);

                try
                {
                    DataCenterBiz databiz = new DataCenterBiz();
                    var list = databiz.GetMemberTypeAll("").DataResponse;
                    lbl_status.Text = list.FirstOrDefault(x => x.Id == userProfile.MemberType.ToString()).Name;
                }
                catch { }
                //if (userProfile.MemberType == 1)
                //{
                //    lbl_status.Text = Resources.propReg_NotApprove_MemberTypeGeneral;
                //}
                //else if (userProfile.MemberType == 2)
                //{
                //    lbl_status.Text = Resources.propReg_Co_MemberTypeCompany;
                //}
                //else if (userProfile.MemberType == 3)
                //{
                //    lbl_status.Text = Resources.propReg_Assoc_MemberTypeAssoc;
                //}
                //else if (userProfile.MemberType == 4)
                //{
                //    lbl_status.Text = Resources.propSite1_OICAdmin;
                //}
                //else if (userProfile.MemberType == 5)
                //{
                //    lbl_status.Text = Resources.propSite1_OICFinace;
                //}
                //else if (userProfile.MemberType == 6)
                //{
                //    lbl_status.Text = Resources.propSite1_OICAgent;
                //}
                //else
                //{
                //    lbl_status.Text = Resources.propReg_Place_Group_001;
                //}

            }
            else
            {
                this.lblUser.Text = string.Empty;
            }

        }

        private string Chklength(DTO.Person person)
        {

            string name;
            name = person.NAMES + " " + person.LASTNAME;
            if (name.Length > 30)
            {
                name = name.Substring(0, 30);
            }

            return name;

        }

        private string CurrentPage()
        {
            //Get page current
            string Path = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            System.IO.FileInfo Info = new System.IO.FileInfo(Path);
            string pageName = Info.Name;

            return pageName;

        }

        private string Chklengthstring(string firstName, string lastname)
        {

            string name;
            name = firstName + " " + lastname;
            if (name.Length > 30)
            {
                name = name.Substring(0, 30);
            }

            return name;

        }

        public void SetUsername(string firstName, string lastname)
        {
            string username;

            username = Chklengthstring(firstName, lastname);
            lblUser.Text = username;

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

        #region UI
        protected void hpLogout_Click(object sender, EventArgs e)
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
            //Session.Clear();
            Response.Redirect("~/home.aspx");
        }

        #endregion

        /// <summary>
        /// DateTime Compare
        /// Result ได้ -1 If  Time_1 น้อยกว่า  Time_2
        /// Result ได้ 0  If  Time_1 เท่ากับ Time_2
        /// Result ได้ 1  If  Time_1 มากกว่า Time_2
        /// How to use Compare?
        /// 1.เวลาปัจุบัน เทียบกับ เวลาที่จ้องการเทียบ
        /// Coding BY Natta
        /// </summary>
        //protected void sessionTimer_Tick(object sender, EventArgs e)
        //{
        //    if((this.GlobalSession != null) || (this.GlobalSession != ""))
        //    {
        //        lblSession.Text = "เวลาคงเหลือในระบบ : " + ((Int32)DateTime.Parse(this.GlobalSession).Subtract(DateTime.Now).TotalMinutes).ToString();
        //        int sessionCompare = DateTime.Compare(DateTime.Now, DateTime.Parse(this.GlobalSession));
        //        //CurrentTime < Session
        //        if (sessionCompare == -1)
        //        {
        //            //Condition
        //        }
        //        //CurrentTime == Session
        //        if (sessionCompare == 0)
        //        {
        //            //Condition
        //        }
        //        //CurrentTime > Session
        //        if (sessionCompare == 1)
        //        {
        //            //ucModelError.ShowMessageError = "Session Timeout";
        //            //ucModelError.ShowModalError();
        //            //return;


        //            //Response.Redirect("~/home.aspx");
        //        }
        //    }
        //}
    }
}
