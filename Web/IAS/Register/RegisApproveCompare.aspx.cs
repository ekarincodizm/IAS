using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Globalization;
using IAS.Utils;
using System.Data;
using System.Web.Configuration;
using AjaxControlToolkit;
using IAS.DTO;
using IAS.BLL.AttachFilesIAS;
using IAS.Properties;
using IAS.BLL;

namespace IAS.Register
{
    public partial class RegisApproveCompare : basepage
    {
        #region Session
        string mapPath;
        DTO.DataActionMode _DataAction;
        private int EDIT_USER_TYPE;

        public string RegStatus
        {
            get { return Session["RegStatus"] == null ? string.Empty : Session["RegStatus"].ToString(); }
            set { Session["RegStatus"] = value; }
        }

        public string TimerSession
        {
            get { return (string)Session["timersession"] == null ? "" : Session["timersession"].ToString(); }
            set { Session["timersession"] = value; }

        }

        public string PersonID
        {
            get
            {
                if (Session["PersonID"] == null)
                {
                    Session["PersonID"] = base.UserProfile.Id;
                }

                return Session["PersonID"].ToString();
            }
            set
            {
                Session["PersonID"] = value;
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

        public string PersonalIDCard
        {
            get { return Session["PersonalIDCard"] == null ? string.Empty : Session["PersonalIDCard"].ToString(); }
            set { Session["PersonalIDCard"] = value; }
        }

        public IAS.MasterPage.Site1 MasterPage
        {
            get { return Page.Master as IAS.MasterPage.Site1; }
        }

        public string MemberTypeSession
        {
            get
            {
                return Session["MemberTypeSession"] == null ? string.Empty : Session["MemberTypeSession"].ToString();
            }
            set
            {
                Session["MemberTypeSession"] = value;
            }

        }
        #endregion

        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string Mode = DataActionMode.View.ToString();
                ucAttachFileControl1.ModeForm = DataActionMode.View;
                base.HasPermit();
                mapPath = WebConfigurationManager.AppSettings["UploadFilePath"];
                Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");
                txtApproveDate.Text = DateTime.Today.ToString("dd MMMM yyyy");

                //Bind before
                //MasterPage.ddlAgentTypeInit(this.ddlAgentTypeBefore);
                //MasterPage.ddlAgentTypeInit(this.ddlAgentTypeAfter);

                GetStatus();
                GetPersonalDetail();
                GetRegistrationDetail();
                GetAttatchFiles();

                GetAttachFilesType();

                disableControl();
                this.DisableControlByMemberType(this.MemberTypeSession);

                Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");
                txtApproveDate.Text = DateTime.Today.ToString("dd MMMM yyyy");
                txtApproveDate.Text = DateUtil.dd_MMMM_yyyy_Now_TH;

                txtApprover.Text = base.UserProfile.Name;
                
            }
        }
        #endregion

        #region UI
        /// <summary>
        /// Edited by Nattapong @15/7/2557
        /// added RegistrationApprove() && RegistrationNotApprove()
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOk_Click(object sender, EventArgs e)
        {
            BLL.RegistrationBiz biz = new BLL.RegistrationBiz();
            BLL.PersonBiz bizPerson = new BLL.PersonBiz();

            //var detail = biz.GetRegistrationsByCriteria(txtFirstNameAfterReg.Text, txtLastNameAfterReg.Text, null, null, txtIDNumberAfterReg.Text, null, txtEmailAfterReg.Text, null, null, 1, 20, "2");
            var detail = biz.GetRegistrationsByCriteria(txtFirstNameAfterReg.Text, txtLastNameAfterReg.Text, null, null, txtIDNumberAfterReg.Text, null, txtEmailAfterReg.Text, null, Session["RegStatus"].ToString(), 1, 20, "2");
            DataSet ds = detail.DataResponse;

            #region Get MemberType From Control @Nattapong
            //string RegMemType = string.Empty;
            //if (txtTypeMemberAfterReg.Text != "")
            //{
            //    switch (txtTypeMemberAfterReg.Text)
            //    {
            //        case "บุคคลทั่วไป":
            //            RegMemType = Convert.ToString((int)DTO.MemberType.General.GetEnumValue());
            //            break;
            //        case "บุคคลทั่วไป/ตัวแทน/นายหน้า":
            //            RegMemType = Convert.ToString((int)DTO.MemberType.General.GetEnumValue());
            //            break;
            //        case "บุคคลทั่วไป (ตัวแทน/นายหน้า)/ตัวแทน/นายหน้า":
            //            RegMemType = Convert.ToString((int)DTO.MemberType.General.GetEnumValue());
            //            break;
            //        case "บริษัทประกัน":
            //            RegMemType = Convert.ToString((int)DTO.MemberType.Insurance.GetEnumValue());
            //            break;
            //        case "บริษัท":
            //            RegMemType = Convert.ToString((int)DTO.MemberType.Insurance.GetEnumValue());
            //            break;
            //        case "สมาคม":
            //            RegMemType = Convert.ToString((int)DTO.MemberType.Association.GetEnumValue());
            //            break;
            //    }
            //}
            #endregion

            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];

                var id = dt.AsEnumerable().Select(s => s.Field<string>("ID"));

                List<string> ls = new List<string>();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string strID = Convert.ToString(dt.Rows[0]["ID"]);
                    ls.Add(strID);
                }

                if (ddlStatusApproval.SelectedValue.Equals("2"))
                {
                    string userid = UserProfile.Id;
                    var res = biz.RegistrationApprove(ls, txtResultReg.Text, userid, this.MemberTypeSession);
                    if (res.IsError)
                    {
                        UCModalError.ShowMessageError = res.ErrorMsg;
                        UCModalError.ShowModalError();
                    }
                    else
                    {
                        string AlertSussuss = String.Format("alert('{0}');window.location.assign('../Register/regSearchOfficerOIC.aspx?Back=R')", SysMessage.SaveSucess);
                        ToolkitScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", AlertSussuss, true);
                    }
                }
                else if (ddlStatusApproval.SelectedValue.Equals("3"))
                {
                    string userid = UserProfile.Id;
                    var res = biz.RegistrationNotApprove(ls, txtResultReg.Text.ToString(), userid);
                    if (res.IsError)
                    {
                        UCModalError.ShowMessageError = res.ErrorMsg;
                        UCModalError.ShowModalError();
                    }
                    else
                    {
                        string AlertSussuss = String.Format("alert('{0}');window.location.assign('../Register/regSearchOfficerOIC.aspx?Back=R')", SysMessage.SaveSucess);
                        ToolkitScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", AlertSussuss, true);
                    }
                }
                else if (ddlStatusApproval.SelectedValue.Equals("5"))
                {
                    var res = bizPerson.PersonApprove(ls);
                    if (res.IsError)
                    {
                        UCModalError.ShowMessageError = res.ErrorMsg;
                        UCModalError.ShowModalError();
                    }
                    else
                    {
                        string AlertSussuss = String.Format("alert('{0}');window.location.assign('../Register/regSearchOfficerOIC.aspx?Back=R')", SysMessage.SaveSucess);
                        ToolkitScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", AlertSussuss, true);
                    }
                }
                else if (ddlStatusApproval.SelectedValue.Equals("6"))
                {
                    var res = bizPerson.PersonNotApprove(ls);
                    if (res.IsError)
                    {
                        UCModalError.ShowMessageError = res.ErrorMsg;
                        UCModalError.ShowModalError();
                    }
                    else
                    {
                        string AlertSussuss = String.Format("alert('{0}');window.location.assign('../Register/regSearchOfficerOIC.aspx?Back=R')", SysMessage.SaveSucess);
                        ToolkitScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", AlertSussuss, true);
                    }
                }
                else if (ddlStatusApproval.SelectedValue.Equals(""))
                {
                    UCModalError.ShowMessageError = Resources.errorEdit_Reg_Person_004;
                    UCModalError.ShowModalError();
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Register/regSearchOfficerOIC.aspx?Back=R");
        }
        #endregion

        #region Public & Private Function
        private Action<DropDownList, List<DTO.DataItem>> BindToDDL = (ddl, ls) =>
        {
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Id";
            ddl.DataSource = ls;
            ddl.DataBind();
        };

        /// <summary>
        /// Edited by Nattapong @26/8/2556
        /// Check Regis status from regApproveOfficerOic.aspx
        /// </summary>
        private void GetStatus()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetStatus(SysMessage.DefaultSelecting);

            List<DTO.DataItem> newls = new List<DTO.DataItem>();
            if (ls.Count >= 0)
            {
                if (this.RegStatus.Equals("1") || this.RegStatus.Equals("2") || this.RegStatus.Equals("3"))
                {
                    for (int i = 0; i < ls.Count; i++)
                    {

                        if (ls[i].Id.Equals("") || ls[i].Id.Equals("2") || ls[i].Id.Equals("3"))
                        {
                            DTO.DataItem item = new DTO.DataItem();
                            item.Name = ls[i].Name;
                            item.Id = ls[i].Id;
                            newls.Add(item);
                        }

                    }
                }
                else
                {
                    for (int i = 0; i < ls.Count; i++)
                    {

                        if (ls[i].Id.Equals("4") || ls[i].Id.Equals("5") || ls[i].Id.Equals("6"))
                        {
                            DTO.DataItem item = new DTO.DataItem();
                            item.Name = ls[i].Name;
                            item.Id = ls[i].Id;
                            newls.Add(item);
                        }

                    }

                }

            }

            BindToDDL(ddlStatusApproval, newls);
        }

        /// <summary>
        /// GET Personal Detail from AG_PERSOANL_T
        /// </summary>
        private void GetPersonalDetail()
        {
            PersonBiz biz = new PersonBiz();
            DTO.ResponseService<DTO.Person> res = biz.GetPersonalDetail(this.PersonalIDCard);

            if (res.DataResponse != null)
            {
                //ไม่มี MemberType
                string memberType = this.NullableString(res.DataResponse.MEMBER_TYPE);
                this.MemberTypeSession = memberType;
                if (memberType != "")
                {
                    GetMemberTypeBefore(memberType);
                    EDIT_USER_TYPE = res.DataResponse.MEMBER_TYPE.ToInt();
                    
                }
                else
                {
                    txtTypeMemberBeforeReg.Text = "-";
                }

                txtResultReg.Text = this.NullableString(res.DataResponse.APPROVE_RESULT);

                string compCode = res.DataResponse.COMP_CODE;
                GetCompanyByIdBefore(compCode);

                int title = Convert.ToInt32(res.DataResponse.PRE_NAME_CODE);
                GetTitleBefore(title);

                if (res.DataResponse.NAMES != null)
                {
                    txtFirstNameBeforeReg.Text = res.DataResponse.NAMES;
                }
                else
                {
                    txtFirstNameBeforeReg.Text = "-";
                }

                if (res.DataResponse.LASTNAME != null)
                {
                    txtLastNameBeforeReg.Text = res.DataResponse.LASTNAME;
                }
                else
                {
                    txtLastNameBeforeReg.Text = "-";
                }

                if (res.DataResponse.SEX != null)
                {
                    rblSexBeforeReg.SelectedValue = res.DataResponse.SEX;
                }
                else
                {
                    rblSexBeforeReg.SelectedIndex = 0;
                }

                if (res.DataResponse.ID_CARD_NO != null)
                {
                    txtIDNumberBeforeReg.Text = res.DataResponse.ID_CARD_NO;
                }
                else
                {
                    txtIDNumberBeforeReg.Text = "-";
                }
                //Remove CSS
                if (res.DataResponse.BIRTH_DATE != null)
                {
                    txtBirthDayBeforeReg.CssClass = "txt";
                    txtBirthDayBeforeReg.Text = res.DataResponse.BIRTH_DATE.Value.ToString("dd MMMM yyyy");
                }
                else
                {
                    txtBirthDayBeforeReg.CssClass = "txt";
                    txtBirthDayBeforeReg.Text = "-";
                }

                string education = this.NullableString(res.DataResponse.EDUCATION_CODE);
                GetEducationBefore(education);

                string Nationality = this.NullableString(res.DataResponse.NATIONALITY);
                GetNationalityBefore(Nationality);

                if (res.DataResponse.EMAIL != null)
                {
                    txtEmailBeforeReg.Text = res.DataResponse.EMAIL;
                }
                else
                {
                    txtEmailBeforeReg.Text = "-";
                }

                if (res.DataResponse.LOCAL_TELEPHONE != null)
                {
                    txtTelBeforeReg.Text = res.DataResponse.LOCAL_TELEPHONE;
                }
                else
                {
                    txtTelBeforeReg.Text = "-";
                }

                if (res.DataResponse.TELEPHONE != null)
                {
                    txtMobilePhoneBeforeReg.Text = res.DataResponse.TELEPHONE;
                }
                else
                {
                    txtMobilePhoneBeforeReg.Text = "-";
                }
                //txtIDOicBeforeReg.Text =
                //txtIDMemberNumberBeforeReg.Text = 
                if (res.DataResponse.ADDRESS_1 != null)
                {
                    txtCurrentAddressBeforeReg.Text = res.DataResponse.ADDRESS_1;
                }
                else
                {
                    txtCurrentAddressBeforeReg.Text = "-";
                }

                //AG_PERSONAL_T.AREA_CODE = PROVINCECODE(2)+AMPURCODE(2)+TUMBON(4)
                if ((res.DataResponse.AREA_CODE != null) && (res.DataResponse.AREA_CODE != ""))
                {
                    if (res.DataResponse.AREA_CODE.Length > 2)
                    {
                        string province = res.DataResponse.AREA_CODE.Substring(0, 2);
                        string district = res.DataResponse.AREA_CODE.Substring(2, 2);
                        string tumbon = res.DataResponse.AREA_CODE.Substring(4);
                        GetAddressByCriteria(DTO.ApproveAddressMode.Personal_regis.GetEnumValue().ToString(), province, district, tumbon);

                    }
                    else
                    {

                        string province = this.NullableString(res.DataResponse.PROVINCE_CODE);
                        string district = this.NullableString(res.DataResponse.AREA_CODE);
                        string tumbon = this.NullableString(res.DataResponse.TUMBON_CODE);
                        GetAddressByCriteria(DTO.ApproveAddressMode.Personal_regis.GetEnumValue().ToString(), province, district, tumbon);
                    }

                }

                if (res.DataResponse.LOCAL_ADDRESS1 != null)
                {
                    txtRegisterAddressBeforeReg.Text = res.DataResponse.LOCAL_ADDRESS1;
                }
                else
                {
                    txtRegisterAddressBeforeReg.Text = "-";
                }

                //AG_PERSONAL_T.AREA_CODE = PROVINCECODE(2)+AMPURCODE(2)+TUMBON(4)
                if ((res.DataResponse.LOCAL_AREA_CODE != null) && (res.DataResponse.LOCAL_AREA_CODE != ""))
                {
                    if (res.DataResponse.LOCAL_AREA_CODE.Length > 2)
                    {
                        string local_province = res.DataResponse.LOCAL_AREA_CODE.Substring(0, 2);
                        string local_district = res.DataResponse.LOCAL_AREA_CODE.Substring(2, 2);
                        string local_tumbon = res.DataResponse.LOCAL_AREA_CODE.Substring(4);
                        GetAddressByCriteria(DTO.ApproveAddressMode.Personal_local.GetEnumValue().ToString(), local_province, local_district, local_tumbon);

                    }
                    else
                    {
                        string local_province = this.NullableString(res.DataResponse.LOCAL_PROVINCE_CODE);
                        string local_district = this.NullableString(res.DataResponse.LOCAL_AREA_CODE);
                        string local_tumbon = this.NullableString(res.DataResponse.LOCAL_TUMBON_CODE);
                        GetAddressByCriteria(DTO.ApproveAddressMode.Personal_local.GetEnumValue().ToString(), local_province, local_district, local_tumbon);
                    }
                }


                if (res.DataResponse.ZIP_CODE != null)
                {
                    txtZipCodeCurrentAddressBeforeReg.Text = res.DataResponse.ZIP_CODE;
                }
                else //milk
                {
                    txtZipCodeCurrentAddressBeforeReg.Text = "-";
                }

                if (res.DataResponse.LOCAL_ZIPCODE != null)
                {
                    txtZipCodeRegisterAddressBeforeReg.Text = res.DataResponse.LOCAL_ZIPCODE;
                }
                else//milk
                {
                    txtZipCodeRegisterAddressBeforeReg.Text = "-";
                }

                //if ((res.DataResponse.AGENT_TYPE != "") && (res.DataResponse.AGENT_TYPE != null))
                //{
                //    ddlAgentTypeBefore.SelectedValue = res.DataResponse.AGENT_TYPE;
                //}
                //else if ((res.DataResponse.AGENT_TYPE == "") || (res.DataResponse.AGENT_TYPE == null))
                //{
                //    ListItem ddlList = new ListItem("", "", true);
                //    ddlList.Selected = true;
                //    ddlAgentTypeBefore.Items.Add(ddlList);
                //}

            }

            if (res.IsError)
            {
                UCModalError.ShowMessageError = res.ErrorMsg;
                UCModalError.ShowModalError();
            }
        }
        
        /// <summary>
        /// GET Registration Detail from AG_IAS_REGISTRATION_T
        /// </summary>
        private void GetRegistrationDetail()
        {
            RegistrationBiz biz = new RegistrationBiz();
            //string personID = this.PersonID;
            var res = biz.GetByIdCard(this.PersonalIDCard);

            if (res != null)
            {
                string memberType = res.DataResponse.MEMBER_TYPE;
                this.MemberTypeSession = memberType;
                GetMembeTypeAfter(memberType);

                string compCode = res.DataResponse.COMP_CODE;
                GetCompanyByIdAfter(compCode);

                int title = Convert.ToInt32(res.DataResponse.PRE_NAME_CODE);
                GetTitleAfter(title);

                if (res.DataResponse.NAMES != null)
                {
                    txtFirstNameAfterReg.Text = res.DataResponse.NAMES;
                }
                else
                {
                    txtFirstNameAfterReg.Text = "-";
                }

                if (res.DataResponse.LASTNAME != null)
                {
                    txtLastNameAfterReg.Text = res.DataResponse.LASTNAME;
                }
                else
                {
                    txtLastNameAfterReg.Text = "-";
                }

                if (res.DataResponse.SEX != null)
                {
                    rblSexAfterReg.SelectedValue = res.DataResponse.SEX;
                }
                else
                {
                    rblSexAfterReg.SelectedIndex = 0;
                }

                if (res.DataResponse.ID_CARD_NO != null)
                {
                    txtIDNumberAfterReg.Text = res.DataResponse.ID_CARD_NO;
                }
                else
                {
                    txtIDNumberAfterReg.Text = "-";
                }

                if (res.DataResponse.BIRTH_DATE != null)
                {
                    txtBirthDayAfterReg.CssClass = "txt";
                    txtBirthDayAfterReg.Text = res.DataResponse.BIRTH_DATE.Value.ToString("dd MMMM yyyy");
                }
                else
                {
                    txtBirthDayAfterReg.CssClass = "txt";
                    txtBirthDayAfterReg.Text = "-";
                }

                string education = res.DataResponse.EDUCATION_CODE;
                GetEducationAfter(education);

                string Nationality = res.DataResponse.NATIONALITY;
                GetNationalityAfter(Nationality);

                if (res.DataResponse.EMAIL != null)
                {
                    txtEmailAfterReg.Text = res.DataResponse.EMAIL;
                }
                else
                {
                    txtEmailAfterReg.Text = "-";
                }

                if (res.DataResponse.LOCAL_TELEPHONE != null)
                {
                    txtTelAfterReg.Text = res.DataResponse.LOCAL_TELEPHONE;
                }
                else
                {
                    txtTelAfterReg.Text = "-";
                }

                if (res.DataResponse.TELEPHONE != null)
                {
                    txtMobilePhoneAfterReg.Text = res.DataResponse.TELEPHONE;
                }
                else
                {
                    txtMobilePhoneAfterReg.Text = "-";
                }
                //txtIDOicBeforeReg.Text =
                //txtIDMemberNumberAfterReg.Text = 

                if (res.DataResponse.ADDRESS_1 != null)
                {
                    txtCurrentAddressAfterReg.Text = res.DataResponse.ADDRESS_1;
                }
                else
                {
                    txtCurrentAddressAfterReg.Text = "-";
                }

                string province = this.NullableString(res.DataResponse.PROVINCE_CODE);
                string ampur = this.NullableString(res.DataResponse.AREA_CODE);
                string tumbon = this.NullableString(res.DataResponse.TUMBON_CODE);
                GetAddressByCriteria(DTO.ApproveAddressMode.Registration_regis.GetEnumValue().ToString(), province, ampur, tumbon);

                if (res.DataResponse.LOCAL_ADDRESS1 != null)
                {
                    txtRegisterAddressAfterReg.Text = res.DataResponse.LOCAL_ADDRESS1;
                }
                else
                {
                    txtRegisterAddressAfterReg.Text = "-";
                }

                string localProvince = this.NullableString(res.DataResponse.LOCAL_PROVINCE_CODE);
                string localampur = this.NullableString(res.DataResponse.LOCAL_AREA_CODE);
                string localtumbon = this.NullableString(res.DataResponse.LOCAL_TUMBON_CODE);
                GetAddressByCriteria(DTO.ApproveAddressMode.Registration_local.GetEnumValue().ToString(), localProvince, localampur, localtumbon);

                if (res.DataResponse.ZIP_CODE != null)
                {
                    txtZipCodeCurrentAddressAfterReg.Text = res.DataResponse.ZIP_CODE;
                }
                else //milk
                {
                    txtZipCodeCurrentAddressAfterReg.Text = "-";
                }
                if (res.DataResponse.LOCAL_ZIPCODE != null)
                {
                    txtZipCodeRegisterAddressAfterReg.Text = res.DataResponse.LOCAL_ZIPCODE;
                }
                else//milk
                {
                    txtZipCodeRegisterAddressAfterReg.Text = "-";
                }

                //if ((res.DataResponse.AGENT_TYPE != "") && (res.DataResponse.AGENT_TYPE != null))
                //{
                //    ddlAgentTypeAfter.SelectedValue = res.DataResponse.AGENT_TYPE;
                //}
                //else if ((res.DataResponse.AGENT_TYPE == "") || (res.DataResponse.AGENT_TYPE == null))
                //{
                //    ListItem ddlList = new ListItem("", "", true);
                //    ddlList.Selected = true;
                //    ddlAgentTypeAfter.Items.Add(ddlList);
                //}
            }
            else
            {
                UCModalError.ShowMessageError = res.ErrorMsg;
                UCModalError.ShowModalError();
            }
        }

        private void GetAttatchFiles()
        {
            var biz = new BLL.PersonBiz();
            string personID = this.PersonID;
            DTO.ResponseService<DTO.PersonAttatchFile[]> res = biz.GetAttatchFileByPersonId(personID);

            var list = res.DataResponse.ToList();

            ucAttachFileControl1.AttachFiles = list.ConvertToAttachFilesView();

        }

        private void GetAttachFilesType()
        {
            var message = SysMessage.DefaultSelecting;
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetDocumentType(message);

            ucAttachFileControl1.DocumentTypes = ls;
        }

        private void disableControl()
        {

            //Before Reg
            txtTypeMemberBeforeReg.Enabled = false;
            txtCompanyBeforeReg.Enabled = false;
            txtTitleBeforeReg.Enabled = false;
            txtFirstNameBeforeReg.Enabled = false;
            txtLastNameBeforeReg.Enabled = false;
            rblSexBeforeReg.Enabled = false;
            txtIDNumberBeforeReg.Enabled = false;
            txtBirthDayBeforeReg.Enabled = false;
            txtEducationBeforeReg.Enabled = false;
            txtNationalityBeforeReg.Enabled = false;
            txtEmailBeforeReg.Enabled = false;
            txtTelBeforeReg.Enabled = false;
            txtMobilePhoneBeforeReg.Enabled = false;
            txtIDOicBeforeReg.Enabled = false;
            txtIDMemberNumberBeforeReg.Enabled = false;
            txtCurrentAddressBeforeReg.Enabled = false;
            txtProvinceCurrentAddressBeforeReg.Enabled = false;
            txtDistrictCurrentAddressBeforeReg.Enabled = false;
            txtParishCurrentAddressBeforeReg.Enabled = false;
            txtRegisterAddressBeforeReg.Enabled = false;
            txtProvinceRegisterAddressBeforeReg.Enabled = false;
            txtDistrictRegisterAddressBeforeReg.Enabled = false;
            txtParishRegisterAddressBeforeReg.Enabled = false;
            txtZipCodeCurrentAddressBeforeReg.Enabled = false;
            txtZipCodeRegisterAddressBeforeReg.Enabled = false;
            txtApprover.Enabled = false;
            txtApproveDate.Enabled = false;
            //ddlAgentTypeBefore.Enabled = false;

            //After Reg
            txtTypeMemberAfterReg.Enabled = false;
            txtCompanyAfterReg.Enabled = false;
            txtTitleAfterReg.Enabled = false;
            txtFirstNameAfterReg.Enabled = false;
            txtLastNameAfterReg.Enabled = false;
            rblSexAfterReg.Enabled = false;
            txtIDNumberAfterReg.Enabled = false;
            txtBirthDayAfterReg.Enabled = false;
            txtEducationAfterReg.Enabled = false;
            txtNationalityAfterReg.Enabled = false;
            txtEmailAfterReg.Enabled = false;
            txtTelAfterReg.Enabled = false;
            txtMobilePhoneAfterReg.Enabled = false;
            txtIDOicAfterReg.Enabled = false;
            txtIDMemberNumberAfterReg.Enabled = false;
            txtCurrentAddressAfterReg.Enabled = false;
            txtProvinceCurrentAddressAfterReg.Enabled = false;
            txtDistrictCurrentAddressAfterReg.Enabled = false;
            txtParishCurrentAddressAfterReg.Enabled = false;
            txtRegisterAddressAfterReg.Enabled = false;
            txtProvinceRegisterAddressAfterReg.Enabled = false;
            txtDistrictRegisterAddressAfterReg.Enabled = false;
            txtParishRegisterAddressAfterReg.Enabled = false;
            txtZipCodeCurrentAddressAfterReg.Enabled = false;
            txtZipCodeRegisterAddressAfterReg.Enabled = false;
            txtApprover.Enabled = false;
            txtApproveDate.Enabled = false;
            //ddlAgentTypeAfter.Enabled = false;


            ucAttachFileControl1.EnableGridView(false);
            ucAttachFileControl1.VisableUpload(false);
            ucAttachFileControl1.EnableUpload(false);
            ucAttachFileControl1.EnablehpCancle(false);
            //txtUsername.Enabled = false;
            //txtApproval.Enabled = false;
        }

        private void GetCompanyByIdBefore(string id)
        {
            var biz = new BLL.DataCenterBiz();
            var ls = biz.GetCompanyCodeById(id);

            if (ls != null)
            {
                ListItem listItem = new ListItem();
                listItem.Value = ls.Id;
                listItem.Text = ls.Name;

                txtCompanyBeforeReg.Text = ls.Name;
            }
            else
            {
                txtCompanyBeforeReg.Text = "-";
            }

        }

        private void GetCompanyByIdAfter(string id)
        {
            var biz = new BLL.DataCenterBiz();
            var ls = biz.GetCompanyCodeById(id);

            if (ls != null)
            {
                ListItem listItem = new ListItem();
                listItem.Value = ls.Id;
                listItem.Text = ls.Name;

                txtCompanyAfterReg.Text = ls.Name;
            }
            else
            {
                txtCompanyAfterReg.Text = "-";
            }
        }

        private void GetMemberTypeBefore(string id)
        {
            var biz = new BLL.DataCenterBiz();
            var ls = biz.GetMemberTypeById(id);

            if (ls != null)
            {
                if (ls.DataResponse.Id == DTO.MemberType.General.GetEnumValue().ToString())
                {
                    ls.DataResponse.Name = ls.DataResponse.Name + "/ตัวแทน/นายหน้า";
                    txtTypeMemberBeforeReg.Text = ls.DataResponse.Name;
                }
                else
                {
                    txtTypeMemberBeforeReg.Text = ls.DataResponse.Name;
                }
                
            }
            else
            {
                txtTypeMemberBeforeReg.Text = "-";
            }

        }

        private void GetMembeTypeAfter(string id)
        {
            var biz = new BLL.DataCenterBiz();
            var ls = biz.GetMemberTypeById(id);

            if (ls != null)
            {
                if (ls.DataResponse.Id == DTO.MemberType.General.GetEnumValue().ToString())
                {
                    ls.DataResponse.Name = ls.DataResponse.Name + "/ตัวแทน/นายหน้า";
                    txtTypeMemberAfterReg.Text = ls.DataResponse.Name;
                }
                else
                {
                    txtTypeMemberAfterReg.Text = ls.DataResponse.Name;
                }
                
            }
            else
            {
                txtTypeMemberAfterReg.Text = "-";
            }
        }

        private void GetTitleBefore(int id)
        {
            var biz = new BLL.DataCenterBiz();
            var ls = biz.GetTitleNameById(id);

            if (ls != null)
            {
                ListItem listItem = new ListItem();
                listItem.Value = ls.Id;
                listItem.Text = ls.Name;

                txtTitleBeforeReg.Text = listItem.ToString();
            }
            else
            {
                txtTitleBeforeReg.Text = "-";
            }
        }

        private void GetTitleAfter(int id)
        {
            var biz = new BLL.DataCenterBiz();
            var ls = biz.GetTitleNameById(id);

            if (ls != null)
            {
                ListItem listItem = new ListItem();
                listItem.Value = ls.Id;
                listItem.Text = ls.Name;

                txtTitleAfterReg.Text = listItem.ToString();
            }
            else
            {
                txtTitleAfterReg.Text = "-";
            }
        }

        private void GetEducationBefore(string id)
        {
            var biz = new BLL.DataCenterBiz();
            var ls = biz.GetEducationById(id);

            if (ls != null)
            {
                ListItem listItem = new ListItem();
                listItem.Value = ls.Id;
                listItem.Text = ls.Name;

                txtEducationBeforeReg.Text = listItem.ToString();
            }
            else
            {
                txtEducationBeforeReg.Text = "-";
            }
        }

        private void GetEducationAfter(string id)
        {
            var biz = new BLL.DataCenterBiz();
            var ls = biz.GetEducationById(id);

            if (ls != null)
            {
                ListItem listItem = new ListItem();
                listItem.Value = ls.Id;
                listItem.Text = ls.Name;

                txtEducationAfterReg.Text = listItem.ToString();
            }
            else
            {
                txtEducationAfterReg.Text = "-";
            }
        }

        private void GetNationalityBefore(string id)
        {
            var biz = new BLL.DataCenterBiz();
            var ls = biz.GetNationalityById(id);

            if (ls != null)
            {
                ListItem listItem = new ListItem();
                listItem.Value = ls.Id;
                listItem.Text = ls.Name;

                txtNationalityBeforeReg.Text = listItem.ToString();
            }
            else
            {
                txtNationalityBeforeReg.Text = this.NullableString(id);
            }
        }

        private void GetNationalityAfter(string id)
        {
            var biz = new BLL.DataCenterBiz();
            var ls = biz.GetNationalityById(id);

            if (ls != null)
            {
                ListItem listItem = new ListItem();
                listItem.Value = ls.Id;
                listItem.Text = ls.Name;

                txtNationalityAfterReg.Text = listItem.ToString();
            }
            else
            {
                txtNationalityAfterReg.Text = "-";
            }
        }

        private void GetAddressByCriteria(string Mode, string pvID, string disctID, string tumbID)
        {
            DataCenterBiz biz = new DataCenterBiz();
            List<TextBox> txtList = new List<TextBox>();

            //AG_PERSONAL_T
            if (Mode.Equals(DTO.ApproveAddressMode.Personal_local.GetEnumValue().ToString()))
            {
                if (pvID.Equals("") || disctID.Equals("") || tumbID.Equals(""))
                {
                    txtProvinceCurrentAddressBeforeReg.Text = "-";
                    txtDistrictCurrentAddressBeforeReg.Text = "-";
                    txtParishCurrentAddressBeforeReg.Text = "-";
                }
                else
                {
                    var lsPV = biz.GetProvinceById(pvID);
                    var lsAum = biz.GetAmpurById(pvID, disctID);
                    var lsTum = biz.GetTumbonById(pvID, disctID, tumbID);
                    
                    txtList.Add(txtProvinceCurrentAddressBeforeReg);
                    txtList.Add(txtDistrictCurrentAddressBeforeReg);
                    txtList.Add(txtParishCurrentAddressBeforeReg);
                    string[] addressList = { lsPV.DataResponse.Name, lsAum.DataResponse.Name, lsTum.DataResponse.Name };
                    this.SetAddresstoControl(txtList, addressList);

                }
            }
            else if (Mode.Equals(DTO.ApproveAddressMode.Personal_regis.GetEnumValue().ToString()))
            {
                if (pvID.Equals("") || disctID.Equals("") || tumbID.Equals(""))
                {
                    txtProvinceRegisterAddressBeforeReg.Text = "-";
                    txtDistrictRegisterAddressBeforeReg.Text = "-";
                    txtParishRegisterAddressBeforeReg.Text = "-";
                }
                else
                {
                    var lsPV = biz.GetProvinceById(pvID);
                    var lsAum = biz.GetAmpurById(pvID, disctID);
                    var lsTum = biz.GetTumbonById(pvID, disctID, tumbID);

                    txtList.Add(txtProvinceRegisterAddressBeforeReg);
                    txtList.Add(txtDistrictRegisterAddressBeforeReg);
                    txtList.Add(txtParishRegisterAddressBeforeReg);
                    string[] addressList = { lsPV.DataResponse.Name, lsAum.DataResponse.Name, lsTum.DataResponse.Name };
                    this.SetAddresstoControl(txtList, addressList);
                }
            }
            //AG_IAS_REGISTRATION_T
            else if (Mode.Equals(DTO.ApproveAddressMode.Registration_local.GetEnumValue().ToString()))
            {
                if (pvID.Equals("") || disctID.Equals("") || tumbID.Equals(""))
                {
                    txtProvinceRegisterAddressAfterReg.Text = "-";
                    txtDistrictRegisterAddressAfterReg.Text = "-";
                    txtParishRegisterAddressAfterReg.Text = "-";
                }
                else
                {
                    var lsPV = biz.GetProvinceById(pvID);
                    var lsAum = biz.GetAmpurById(pvID, disctID);
                    var lsTum = biz.GetTumbonById(pvID, disctID, tumbID);

                    txtList.Add(txtProvinceRegisterAddressAfterReg);
                    txtList.Add(txtDistrictRegisterAddressAfterReg);
                    txtList.Add(txtParishRegisterAddressAfterReg);
                    string[] addressList = { lsPV.DataResponse.Name, lsAum.DataResponse.Name, lsTum.DataResponse.Name };
                    this.SetAddresstoControl(txtList, addressList);
                }
            }
            else if (Mode.Equals(DTO.ApproveAddressMode.Registration_regis.GetEnumValue().ToString()))
            {
                if (pvID.Equals("") || disctID.Equals("") || tumbID.Equals(""))
                {
                    txtProvinceCurrentAddressAfterReg.Text = "-";
                    txtDistrictCurrentAddressAfterReg.Text = "-";
                    txtParishCurrentAddressAfterReg.Text = "-";
                }
                else
                {
                    var lsPV = biz.GetProvinceById(pvID);
                    var lsAum = biz.GetAmpurById(pvID, disctID);
                    var lsTum = biz.GetTumbonById(pvID, disctID, tumbID);

                    txtList.Add(txtProvinceCurrentAddressAfterReg);
                    txtList.Add(txtDistrictCurrentAddressAfterReg);
                    txtList.Add(txtParishCurrentAddressAfterReg);
                    string[] addressList = { lsPV.DataResponse.Name, lsAum.DataResponse.Name, lsTum.DataResponse.Name };
                    this.SetAddresstoControl(txtList, addressList);
                }
            }
            
        }

        private void DisableControlByMemberType(string memberType)
        {
            if (memberType == DTO.MemberType.General.GetEnumValue().ToString())
            {
                lblTelCurrentAddressBeforeReg.Text = Resources.propEdit_Reg_Person_001;
                lblMobilePhoneBeforeReg.Text = Resources.propEdit_Reg_Person_002;

                this.pnlRegisAddressAfter.Visible = true;
                this.pnlRegisAddressBefore.Visible = true;

                lblIDOicBeforeReg.Visible = false;
                txtIDOicBeforeReg.Visible = false;
                lblMemberNumberBeforeReg.Visible = false;
                txtIDMemberNumberBeforeReg.Visible = false;

                lblIDOicAfterReg.Visible = false;
                txtIDOicAfterReg.Visible = false;
                lblMemberNumberAfterReg.Visible = false;
                txtIDMemberNumberAfterReg.Visible = false;

                lblCompanyBeforeReg.Visible = false;
                txtCompanyBeforeReg.Visible = false;
                lblCompanyAfterReg.Visible = false;
                txtCompanyAfterReg.Visible = false;
            }
            else
            {
                lblTelCurrentAddressBeforeReg.Text = Resources.propEdit_Reg_Person_003;
                lblMobilePhoneBeforeReg.Text = Resources.propEdit_Reg_Person_001;

                this.pnlRegisAddressAfter.Visible = false;
                this.pnlRegisAddressBefore.Visible = false;

                lblIDOicBeforeReg.Visible = false;
                txtIDOicBeforeReg.Visible = false;
                lblMemberNumberBeforeReg.Visible = false;
                txtIDMemberNumberBeforeReg.Visible = false;

                lblIDOicAfterReg.Visible = false;
                txtIDOicAfterReg.Visible = false;
                lblMemberNumberAfterReg.Visible = false;
                txtIDMemberNumberAfterReg.Visible = false;

                lblCompanyBeforeReg.Visible = true;
                txtCompanyBeforeReg.Visible = true;
                lblCompanyAfterReg.Visible = true;
                txtCompanyAfterReg.Visible = true;

            }
        }

        private void SetAddresstoControl(List<TextBox> ctrl, string[] name)
        {
            List<TextBox> _ctrl = ctrl;
            if(ctrl.Count != name.Count())
            {
                //
            }
            if (_ctrl.Count > 0)
            {
                for (int i = 0; i < _ctrl.Count; i++)
                {
                    if (ctrl[i].ClientID != null)
                    {
                        _ctrl[i].Text = name[i];
                    }
                }
            }
            
        }

        public string NullableString(string input)
        {
            if ((input == null) || (input == ""))
            {
                input = "";
            }

            return input;
        }
        #endregion


    }
}