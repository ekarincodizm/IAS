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

namespace IAS.Register
{
    public partial class regApproveOfficerOic : basepage
    {
        string mapPath;
        DTO.DataActionMode _DataAction;

        public string RegStatus
        {
            get { return Session["RegStatus"] == null ? string.Empty : Session["RegStatus"].ToString(); }
            set { Session["RegStatus"] = value; }
        }

        public string ApproveStatus
        {
            get { return Session["ApproveStatus"] == null ? string.Empty : Session["ApproveStatus"].ToString(); }
            set { Session["ApproveStatus"] = value; }
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

        public DTO.UserProfile UserProfile
        {
            get
            {
                return Session["UserProfile"] == null ? null : (DTO.UserProfile)Session["UserProfile"];
            }
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

        public IAS.MasterPage.Site1 MasterPage
        {
            get { return Page.Master as IAS.MasterPage.Site1;  }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Add new
                string Mode = DataActionMode.View.ToString();
                ucAttachFileControl1.ModeForm = DataActionMode.View;
                base.HasPermit();
                mapPath = WebConfigurationManager.AppSettings["UploadFilePath"];
                Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");
                txtApproval.Text = DateTime.Today.ToString("dd MMMM yyyy");

                //TabCurrentAddress.CssClass = "Clicked";
                //mainView.ActiveViewIndex = 0;
                //MasterPage.ddlAgentTypeInit(this.ddlAgentType);
                GetRegistrationDetail();

                GetStatusApproval();
                disableControl();
                GetAttachFilesType();
                txtUsername.Text = base.UserProfile.Name;

                GetAttatchFiles();

                //BindDataInGridView();
                GetAttachFilesTypeImage();

                DisableGVUpload();

                ChkMode(Mode);
                VisibleText();
                
            }
        }


        private void VisibleText()
        {
            if (txtTypeMemberBeforeReg.Text.Contains("บุคคลทั่วไป") == true)
            {
                lblOfficerCode.Visible = false;
                txtCompanyBeforeReg.Visible = false;

                lblStartDate2.Visible = false;
                txtIDOicBeforeReg.Visible = false;
            }
            else
            {
                lblOfficerCode.Visible = true;
                txtCompanyBeforeReg.Visible = true;

                lblStartDate2.Visible = true;
                txtIDOicBeforeReg.Visible = true;
            }
        }

        private void GetAttachFilesType()
        {
            var message = SysMessage.DefaultSelecting;
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetDocumentType(message);

            ucAttachFileControl1.DocumentTypes = ls;
        }
        private void ChkMode(string Mode)
        {
            if (Mode.Equals("View"))
            {
                this.DataAction = DTO.DataActionMode.View;
                //pnlMain.Enabled = false;
                //Add new 
                ucAttachFileControl1.ModeForm = DTO.DataActionMode.View;
            }
        }

        protected void DisableGVUpload()
        {

            if (this.RegStatus.Equals("1"))
            {

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
        private void GetAttachFilesTypeImage()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            Session["DocumentTypeIsImage"] = biz.GetDocumentTypeIsImage();
        }

        private Action<DropDownList, List<DTO.DataItem>> BindToDDL = (ddl, ls) =>
        {
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Id";
            ddl.DataSource = ls;
            ddl.DataBind();
        };

        private void BindDataInGridView()
        {
            ucAttachFileControl1.BindAttachFile();
        }

        /// <summary>
        /// Edited by Nattapong @08/08/2557
        /// Check Regis status from regApproveOfficerOic.aspx
        /// </summary>
        private void GetStatusApproval()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetStatus(SysMessage.DefaultSelecting);

            List<DTO.DataItem> newls = new List<DTO.DataItem>();
            if (ls.Count >= 0)
            {
                //1	รออนุมัติ(สมัคร)
                //2	อนุมัติ(สมัคร)
                //3	ไม่อนุมัติ(สมัคร)
                if (this.ApproveStatus.Equals("1") || this.ApproveStatus.Equals("2") || this.ApproveStatus.Equals("3"))
                {
                    newls = ls.Where(item => item.Id.Equals("") || item.Id.Equals("2") || item.Id.Equals("3")).ToList();
                }
                //4	รออนุมัติ(แก้ไข)
                //5	อนุมัติ(แก้ไข)
                //6	ไม่อนุมัติ(แก้ไข)
                else if (this.ApproveStatus.Equals("4") || this.ApproveStatus.Equals("5") || this.ApproveStatus.Equals("6"))
                {
                    newls = ls.Where(item => item.Id.Equals("4") || item.Id.Equals("5") || item.Id.Equals("6")).ToList();
                }
                //7 = ไม่ใช้งาน
                else
                {
                    newls = ls.Where(item => item.Id.Equals("")).ToList();
                }

            }
            BindToDDL(ddlStatusApproval, newls);
        }

        protected void TabCurrentAddress_Click(object sender, EventArgs e)
        {
            //TabCurrentAddress.CssClass = "Clicked";
            //TabRegisterAddress.CssClass = "Initial";
            //mainView.ActiveViewIndex = 0;
        }

        protected void TabRegisterAddress_Click(object sender, EventArgs e)
        {
            //TabCurrentAddress.CssClass = "Initial";
            //TabRegisterAddress.CssClass = "Clicked";
            //mainView.ActiveViewIndex = 1;
        }

        private void GetProvinceRegisterAddressBefore(string id)
        {
            var biz = new BLL.DataCenterBiz();
            var ls = biz.GetProvinceById(id);


            if (ls.DataResponse != null)
            {
                txtProvinceRegisterAddressBeforeReg.Text = ls.DataResponse.Name;
            }
            else
            {
                txtProvinceRegisterAddressBeforeReg.Text = "-";
            }

        }

        protected void hplView_Click(object sender, EventArgs e)
        {
            var gr = (GridViewRow)((LinkButton)sender).NamingContainer;

            var text = (Label)gr.FindControl("lblFileGv");

            Session["ViewFileName"] = text.Text;

            string showPic = IAS.Utils.CryptoBase64.Encryption(text.Text.ToString().Trim());

            string OpenWindow = "window.open('../Register/ImagePopup.aspx?targetImage=" + showPic + "','','')";
            ToolkitScriptManager.RegisterStartupScript(this, this.GetType(), "newWindow", OpenWindow, true);
        }

        /// <summary>
        /// Last Update 15/7/2557
        /// By Natta
        /// </summary>
        private void GetRegistrationDetail()
        {
            var biz = new BLL.RegistrationBiz();
            var dbiz = new BLL.DataCenterBiz();
            string personID = this.PersonID;
            var res = biz.GetById(personID);
            var comp = string.Empty;
            if (!string.IsNullOrEmpty(res.DataResponse.COMP_CODE))
            {
                if(res.DataResponse.MEMBER_TYPE.Equals(DTO.MemberType.Association.GetEnumValue().ToString()))
                {
                    //DTO.ResponseService<string> cc = dbiz.GetAssociateNameById(res.DataResponse.COMP_CODE);
                    //comp = cc.DataResponse.ToString();
                    //string dd = dbiz.GetAssociateNameById(res.DataResponse.COMP_CODE).ToString();
                    DTO.ResponseService<DTO.ASSOCIATION> getass = dbiz.GetInsuranceAssociateNameByID(res.DataResponse.COMP_CODE);
                    if (getass.DataResponse != null)
                    {
                        comp = getass.DataResponse.ASSOCIATION_NAME;
                    }
                }
                else
                {
                    comp = dbiz.GetCompanyNameById(res.DataResponse.COMP_CODE);
                }
            }
            if (res.DataResponse != null)
            {
                int title = Convert.ToInt32(res.DataResponse.PRE_NAME_CODE);
                GetTitleAfter(title);
                if (res.DataResponse.MEMBER_TYPE != null)
                {
                    //Add new
                    this.MemberTypeSession = res.DataResponse.MEMBER_TYPE;

                    if (res.DataResponse.MEMBER_TYPE == "1")
                    {
                        txtTypeMemberBeforeReg.Text = Resources.propReg_NotApprove_MemberTypeGeneral;
                        lblStartDate8.Text = Resources.propEdit_Reg_Person_002;
                    }
                    else if (res.DataResponse.MEMBER_TYPE == "2")
                    {
                        txtTypeMemberBeforeReg.Text = Resources.propReg_Co_MemberTypeCompany;
                        lblStartDate8.Text = Resources.propregApproveOfficerOIC_001;
                    }
                    else if (res.DataResponse.MEMBER_TYPE == "3")
                    {
                        txtTypeMemberBeforeReg.Text = Resources.propReg_Assoc_MemberTypeAssoc;
                        lblStartDate8.Text = Resources.propregApproveOfficerOIC_001;
                    }
                }
                else
                {
                    txtTypeMemberBeforeReg.Text = Resources.propregApproveOfficerOIC_002;
                }
                if (comp != null)
                {
                    txtCompanyBeforeReg.Text = comp;
                }
                else
                {
                    txtCompanyBeforeReg.Text = Resources.propregApproveOfficerOIC_002;
                }

                if (res.DataResponse.NAMES != null)
                {
                    txtFirstName.Text = res.DataResponse.NAMES;
                }
                else
                {
                    txtFirstName.Text = Resources.propregApproveOfficerOIC_002;
                }

                if (res.DataResponse.LASTNAME != null)
                {
                    txtLastName.Text = res.DataResponse.LASTNAME;
                }
                else
                {
                    txtLastName.Text = Resources.propregApproveOfficerOIC_002;
                }

                if (res.DataResponse.SEX != null)
                {
                    rblSex.SelectedValue = res.DataResponse.SEX;
                }
                else
                {
                    rblSex.SelectedIndex = 0;
                }

                if (res.DataResponse.ID_CARD_NO != null)
                {
                    txtIDCard.Text = res.DataResponse.ID_CARD_NO;
                }
                else
                {
                    txtIDCard.Text = Resources.propregApproveOfficerOIC_002;
                }
                if (res.DataResponse.BIRTH_DATE != null)
                {
                    txtBirthDay.CssClass = "txt";
                    txtBirthDay.Text = res.DataResponse.BIRTH_DATE.Value.ToString("dd MMMM yyyy");
                }
                else
                {
                    txtBirthDay.CssClass = "txt";
                    txtBirthDay.Text = Resources.propregApproveOfficerOIC_002;
                }


                if (res.DataResponse.APPROVE_RESULT != null)
                {
                    txtResultReg.Text = res.DataResponse.APPROVE_RESULT;
                }
                else
                {
                    txtResultReg.Text = "";
                }



                string education = res.DataResponse.EDUCATION_CODE;
                GetEducationBefore(education);

                string Nationality = res.DataResponse.NATIONALITY;
                GetNationalityBefore(Nationality);

                if (res.DataResponse.EMAIL != null)
                {
                    txtEmailBeforeReg.Text = res.DataResponse.EMAIL;
                }
                else
                {
                    txtEmailBeforeReg.Text = Resources.propregApproveOfficerOIC_002;
                }

                if (res.DataResponse.LOCAL_TELEPHONE != null)
                {
                    txtTelBeforeReg.Text = res.DataResponse.LOCAL_TELEPHONE;
                }
                else
                {
                    txtTelBeforeReg.Text = Resources.propregApproveOfficerOIC_002;
                }

                if (res.DataResponse.TELEPHONE != null)
                {
                    txtMobilePhoneBeforeReg.Text = res.DataResponse.TELEPHONE;
                }
                else
                {
                    txtMobilePhoneBeforeReg.Text = Resources.propregApproveOfficerOIC_002;
                }
                if (res.DataResponse.ADDRESS_1 != null)
                {
                    txtCurrentAddressBeforeReg.Text = res.DataResponse.ADDRESS_1;
                }
                else
                {
                    txtCurrentAddressBeforeReg.Text = Resources.propregApproveOfficerOIC_002;
                }

                string province = res.DataResponse.PROVINCE_CODE;
                GetProvinceCurrentAddressBefore(province);

                string ampur = res.DataResponse.AREA_CODE;
                GetAmpurCurrentAddressBefore(province, ampur);

                string tumbon = res.DataResponse.TUMBON_CODE;
                GetTumbonCurrentAddressBefore(province, ampur, tumbon);

                if (res.DataResponse.LOCAL_ADDRESS1 != null)
                {
                    txtRegisterAddressBeforeReg.Text = res.DataResponse.LOCAL_ADDRESS1;
                }
                else
                {
                    txtRegisterAddressBeforeReg.Text = Resources.propregApproveOfficerOIC_002;
                }


                string localProvince = res.DataResponse.LOCAL_PROVINCE_CODE;
                GetProvinceRegisterAddressBefore(localProvince);

                string localampur = res.DataResponse.LOCAL_AREA_CODE;
                GetAmpurRegisterAddressBefore(localProvince, localampur);

                string localtumbon = res.DataResponse.LOCAL_TUMBON_CODE;
                GetTumbonRegisterAddressBefore(localProvince, localampur, localtumbon);

                //Add new 28/8/2556
                if (res.DataResponse.ZIP_CODE != null)
                {
                    //txtPostcodeRegisterAddress.Enabled = false;
                    txtPostcodeCurrentAddress.Text = res.DataResponse.ZIP_CODE;
                }
                else//milk
                {
                    txtPostcodeCurrentAddress.Text = Resources.propregApproveOfficerOIC_002;
                }

                if (res.DataResponse.LOCAL_ZIPCODE != null)
                {
                    //txtPostcodeCurrentAddress.Enabled = false;

                    txtPostcodeRegisterAddress.Text = res.DataResponse.LOCAL_ZIPCODE;
                }
                else//milk
                {
                    txtPostcodeRegisterAddress.Text = Resources.propregApproveOfficerOIC_002;
                }

               // txtResultReg.Text = res.DataResponse.APPROVE_RESULT;

                //if ((res.DataResponse.AGENT_TYPE != null) && (res.DataResponse.AGENT_TYPE != ""))
                //{
                //    ddlAgentType.SelectedValue = res.DataResponse.AGENT_TYPE;

                //}
                //else if ((res.DataResponse.AGENT_TYPE == "") || (res.DataResponse.AGENT_TYPE == null))
                //{
                //    ddlAgentType.Items.Clear();
                //    ddlAgentType.DataSource = null;
                //    ddlAgentType.DataBind();
                //}

            }

            if (res.IsError)
            {
                UCModalError.ShowMessageError = res.ErrorMsg;
                UCModalError.ShowModalError();
            }

        }

        private void GetProvinceCurrentAddressBefore(string id)
        {
            var biz = new BLL.DataCenterBiz();
            var ls = biz.GetProvinceById(id);

            if (ls.DataResponse != null)
            {
                txtProvinceCurrentAddressBeforeReg.Text = ls.DataResponse.Name;
            }
            else
            {
                txtProvinceCurrentAddressBeforeReg.Text = Resources.propregApproveOfficerOIC_002;
            }
        }

        private void GetTumbonRegisterAddressBefore(string provinceId, string ampurId, string id)
        {
            var biz = new BLL.DataCenterBiz();
            var ls = biz.GetTumbonById(provinceId, ampurId, id);

            if (ls.DataResponse != null)
            {
                txtParishRegisterAddressBeforeReg.Text = ls.DataResponse.Name;
            }
            else
            {
                txtParishRegisterAddressBeforeReg.Text = Resources.propregApproveOfficerOIC_002;
            }
        }

        private void GetTumbonCurrentAddressBefore(string provinceId, string ampurId, string id)
        {
            var biz = new BLL.DataCenterBiz();
            var ls = biz.GetTumbonById(provinceId, ampurId, id);

            if (ls.DataResponse != null)
            {
                txtParishCurrentAddressBeforeReg.Text = ls.DataResponse.Name;
            }
            else
            {
                txtParishCurrentAddressBeforeReg.Text = Resources.propregApproveOfficerOIC_002;
            }

        }

        private void GetAmpurRegisterAddressBefore(string provinceId, string id)
        {
            var biz = new BLL.DataCenterBiz();
            var ls = biz.GetAmpurById(provinceId, id);

            if (ls.DataResponse != null)
            {
                txtDistrictRegisterAddressBeforeReg.Text = ls.DataResponse.Name;
            }
            else
            {
                txtDistrictRegisterAddressBeforeReg.Text = Resources.propregApproveOfficerOIC_002;
            }
        }

        private void GetAmpurCurrentAddressBefore(string provinceId, string id)
        {
            var biz = new BLL.DataCenterBiz();
            var ls = biz.GetAmpurById(provinceId, id);

            if (ls.DataResponse != null)
            {
                txtDistrictCurrentAddressBeforeReg.Text = ls.DataResponse.Name;
            }
            else
            {
                txtDistrictCurrentAddressBeforeReg.Text = Resources.propregApproveOfficerOIC_002;
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
                txtEducationBeforeReg.Text = Resources.propregApproveOfficerOIC_002;
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
                txtNationalityBeforeReg.Text = Resources.propregApproveOfficerOIC_002;
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
                txtTitleAfterReg.Text = Resources.propregApproveOfficerOIC_002;
            }
        }

        private void disableControl()
        {

            //Before Reg
            txtTypeMemberBeforeReg.Enabled = false;
            txtCompanyBeforeReg.Enabled = false;
            txtTitleAfterReg.Enabled = false;
            txtFirstName.Enabled = false;
            txtLastName.Enabled = false;
            rblSex.Enabled = false;
            txtIDCard.Enabled = false;
            txtBirthDay.Enabled = false;
            txtEducationBeforeReg.Enabled = false;
            txtNationalityBeforeReg.Enabled = false;
            txtEmailBeforeReg.Enabled = false;
            txtTelBeforeReg.Enabled = false;
            txtMobilePhoneBeforeReg.Enabled = false;
            txtIDOicBeforeReg.Enabled = false;
            txtCurrentAddressBeforeReg.Enabled = false;
            txtProvinceCurrentAddressBeforeReg.Enabled = false;
            txtDistrictCurrentAddressBeforeReg.Enabled = false;
            txtParishCurrentAddressBeforeReg.Enabled = false;
            txtRegisterAddressBeforeReg.Enabled = false;
            txtProvinceRegisterAddressBeforeReg.Enabled = false;
            txtDistrictRegisterAddressBeforeReg.Enabled = false;
            txtParishRegisterAddressBeforeReg.Enabled = false;
            //ddlTypeAttachment.Enabled = false;
            //fulFile.Enabled = false;
            //txtDetail.Enabled = false;
            //btnUploadFile.Enabled = false;
            ucAttachFileControl1.EnableGridView(false);
            ucAttachFileControl1.VisableUpload(false);
            ucAttachFileControl1.EnableUpload(false);
            txtUsername.Enabled = false;
            txtApproval.Enabled = false;

            //Add new by Nattapong 
            txtPostcodeRegisterAddress.Enabled = false;
            txtPostcodeCurrentAddress.Enabled = false;

            //ddlAgentType.Enabled = false;

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Register/regSearchOfficerOIC.aspx?Back=R");
        }

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

            var detail = biz.GetRegistrationsByCriteria(txtFirstName.Text, txtLastName.Text,null,null, txtIDCard.Text, null, txtEmailBeforeReg.Text, null, this.RegStatus, 1, 20,"2");
            DataSet ds = detail.DataResponse;

            #region Get MemberType From Control @Nattapong
            //string RegMemType = string.Empty;
            //if (txtTypeMemberBeforeReg.Text != "")
            //{
            //    switch (txtTypeMemberBeforeReg.Text)
            //    {
            //        case "บุคคลทั่วไป" :
            //            RegMemType = Convert.ToString((int)DTO.MemberType.General.GetEnumValue());
            //            break;
            //        case "บุคคลทั่วไป/ตัวแทน/นายหน้า":
            //            RegMemType = Convert.ToString((int)DTO.MemberType.General.GetEnumValue());
            //            break;
            //        case "บริษัทประกัน":
            //            RegMemType = Convert.ToString((int)DTO.MemberType.Insurance.GetEnumValue());
            //            break;
            //        case "บริษัท":
            //            RegMemType = Convert.ToString((int)DTO.MemberType.Insurance.GetEnumValue());
            //            break;
            //        case "สมาคม" :
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
                    //var res = biz.RegistrationApprove(ls, "อนุมัติกลุ่ม");

                    string userid = UserProfile.Id;

                    var res = biz.RegistrationApprove(ls, txtResultReg.Text, userid, this.MemberTypeSession);
                    if (res.IsError)
                    {
                        UCModalError.ShowMessageError = res.ErrorMsg;
                        UCModalError.ShowModalError();
                    }
                    else
                    {
                        //UCModalSuccess.ShowMessageSuccess = "ทำรายการเรียบร้อย";
                        //UCModalSuccess.ShowModalSuccess();
                        //btnOk.Enabled = false;
                        //btnOk.Visible = false;
                        //btnCancel.Text = "ย้อนกลับ";
                        string AlertSussuss = String.Format("alert('{0}');window.location.assign('../Register/regSearchOfficerOIC.aspx?Back=R')", SysMessage.SaveSucess);
                        ToolkitScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", AlertSussuss, true);
                    }
                }
                else if (ddlStatusApproval.SelectedValue.Equals("3"))
                {
                    //string userid = UserProfile.Id;
                    string userid = Session["PersonalIDCard"].ToString();
                    var res = biz.RegistrationNotApprove(ls, txtResultReg.Text.ToString(),userid);
                    if (res.IsError)
                    {
                        UCModalError.ShowMessageError = res.ErrorMsg;
                        UCModalError.ShowModalError();
                    }
                    else
                    {
                        //UCModalSuccess.ShowMessageSuccess = "ทำรายการเรียบร้อย";
                        //UCModalSuccess.ShowModalSuccess();
                        ////Response.Redirect("~/Register/regSearchOfficerOIC.aspx");
                        //btnOk.Enabled = false;
                        //btnOk.Visible = false;
                        //btnCancel.Text = "ย้อนกลับ";
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
                        //UCModalSuccess.ShowMessageSuccess = "ทำรายการเรียบร้อย";
                        //UCModalSuccess.ShowModalSuccess();
                        ////Response.Redirect("~/Register/regSearchOfficerOIC.aspx");
                        //btnOk.Enabled = false;
                        //btnOk.Visible = false;
                        //btnCancel.Text = "ย้อนกลับ";
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
                       // UCModalSuccess.ShowMessageSuccess = "ทำรายการเรียบร้อย";
                       // UCModalSuccess.ShowModalSuccess();
                       // btnOk.Enabled = false;
                       // btnOk.Visible = false;
                       ////Response.Redirect("~/Register/regSearchOfficerOIC.aspx");
                       // btnCancel.Text = "ย้อนกลับ";
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

    }
}
