using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Text;
using System.Web.UI.WebControls;

using IAS.DTO;
using AjaxControlToolkit;
using IAS.MasterPage;
using IAS.Properties;

namespace IAS.Register
{
    public partial class Reg_Place_Group : basepage
    {
        public string GroupValidation = "PlaceGroupValidationGroup";
        public List<DTO.PersonAttatchFile> PersonAttachFiles
        {
            get
            {
                if (Session["PersonAttachFiles"] == null)
                {
                    Session["PersonAttachFiles"] = new List<DTO.PersonAttatchFile>();
                }

                return (List<DTO.PersonAttatchFile>)Session["PersonAttachFiles"];
            }
            set
            {
                Session["PersonAttachFiles"] = value;
            }
        }

      
        public Site1 MasterPage
        {
            get { return (this.Page.Master as Site1);}
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

        public List<DTO.RegistrationAttatchFile> AttachFiles
        {
            get
            {
                if (Session["AttatchFiles"] == null)
                {
                    Session["AttatchFiles"] = new List<DTO.RegistrationAttatchFile>();
                }

                return (List<DTO.RegistrationAttatchFile>)Session["AttatchFiles"];
            }

            set
            {
                Session["AttatchFiles"] = value;
            }
        }

        public DTO.UserProfile UserProfile
        {
            get
            {
                return Session["UserProfile"] == null ? null : (DTO.UserProfile)Session["UserProfile"];
            }
        }

        public string MememberTypePlaceGroup
        {
            get
            {
                return (string)Session["MememberTypePlaceGroup"];
            }
        }

        public string TempFolderOracle
        {
            get
            {
                return (string)Session["TempFolderOracle"];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            txtEmail.Attributes.Add("onblur", "javascript:return checkemail(" + txtEmail.ClientID + ");");
            //txtIDNumber.Attributes.Add("onblur", "javascript:return checkUser();");

            //txtIDNumber.Attributes.Add("onkeydown", "javascript:return checkKeyNumberFixPoint(event, this, 13);");
            //txtIDNumber.Attributes.Add("onkeypress", "javascript:return checkKeyNumberKeyPressFixPoint(event, this, 13);");

            //txtTel.Attributes.Add("onkeydown", "javascript:return checkKeyNumberFixPoint(event, this, 15);");
           // txtTel.Attributes.Add("onkeypress", "javascript:return checkKeyNumberKeyPressFixPoint(event, this, 15);");

           // txtPostcode.Attributes.Add("onkeydown", "javascript:return checkKeyNumberFixPoint(event, this, 5);");
           // txtPostcode.Attributes.Add("onkeypress", "javascript:return checkKeyNumberKeyPressFixPoint(event, this, 5);");

           // txtPlaceGroupTel.Attributes.Add("onkeydown", "javascript:return checkKeyNumberFixPoint(event, this, 15);");
            //txtPlaceGroupTel.Attributes.Add("onkeypress", "javascript:return checkKeyNumberKeyPressFixPoint(event, this, 15);");

            if (!Page.IsPostBack)
            {

                string Mode = Request.QueryString["Mode"];

                if (Mode != null)
                {
                    ClearControl();

                    Session["MememberTypePlaceGroup"] = "5";
                    Session["TempFolderOracle"] = IAS.BLL.Helpers.GenerateIdHelper.GetGenAutoId();
                    if (this.MememberTypePlaceGroup == "5")
                    {
                        txtMemberType.Text = Resources.propReg_Place_Group_001;
                        txtMemberType.Enabled = false;
                    }

                    GetProvince();
                    GetTitleName();
                    GetExamPlaceGroup();

                    InitData();


                    PnlAddButton.Visible = false;
                    PnlEditButton.Visible = true;

                    if (Mode == "E")
                    {
                        lblRegister.Text = Resources.propReg_Place_Group_002;
                  
                        this.DataAction = DTO.DataActionMode.Edit;
                        divPassword.Visible = false;
                        ddlPlaceGroup.Enabled = false;
                        PnlAddButton.Visible = false;
                        PnlEditButton.Visible = true;
                        
                    }
                    else
                    {
                        lblRegister.Text = Resources.propReg_Place_Group_003;
                        this.DataAction = DTO.DataActionMode.View;
                        pnlMain.Enabled = false;
                        divPassword.Visible = false;
                        PnlAddButton.Visible = false;
                        PnlEditButton.Visible = false;
                    }

                }
                else
                {
                    this.DataAction = DTO.DataActionMode.Add;

                    Session["MememberTypePlaceGroup"] = ((int)DTO.RegistrationType.TestCenter).ToString();
                    Session["UserID"] = IAS.BLL.Helpers.GenerateIdHelper.GetGenAutoId();
                    Session["TempFolderOracle"] = IAS.BLL.Helpers.GenerateIdHelper.GetGenAutoId();

                    if (this.MememberTypePlaceGroup == ((int)DTO.RegistrationType.TestCenter).ToString())
                    {
                        txtMemberType.Text = Resources.propReg_Place_Group_001;
                        txtMemberType.Enabled = false;
                    }

                    //addData();

                    GetProvince();
                    GetTitleName();
                    GetExamPlaceGroup();
                }

            }
            else
            {

            }
        }

        private Action<DropDownList, List<DTO.DataItem>> BindToDDL = (ddl, ls) =>
        {
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Id";
            ddl.DataSource = ls;
            ddl.DataBind();
        };

        private void InitData()
        {
            BLL.PersonBiz biz = new BLL.PersonBiz();
            var res = biz.GetUserProfileById(this.UserProfile.Id);
            BLL.DataCenterBiz dtbiz = new BLL.DataCenterBiz();

            if (!res.IsError)
            {
                var person = res.DataResponse;

                ddlPlaceGroup.SelectedValue = person.COMP_CODE;
                ddlTitle.SelectedValue = person.PRE_NAME_CODE;
                txtFirstName.Text = person.NAMES;
                txtLastName.Text = person.LASTNAME;
                txtIDNumber.Text = person.ID_CARD_NO;
                txtIDNumber.Enabled = false;
                rblSex.Text = person.SEX;
                txtTel.Text = person.TELEPHONE;
                txtPlaceGroupTel.Text = person.LOCAL_TELEPHONE;
                txtEmail.Text = person.EMAIL;
                txtEmail.Enabled = false;
                txtAddress.Text = person.ADDRESS_1;
                txtPostcode.Text = person.ZIP_CODE;

                var message = SysMessage.DefaultSelecting;

                BLL.DataCenterBiz dataCenter = new BLL.DataCenterBiz();
                ddlProvince.SelectedValue = person.PROVINCE_CODE;
                var lsPC = dataCenter.GetAmpur(message, ddlProvince.SelectedValue);
                BindToDDL(ddlDistrict, lsPC);

                ddlDistrict.SelectedValue = person.AREA_CODE;
                var lsTC = dataCenter.GetTumbon(message, ddlProvince.SelectedValue, ddlDistrict.SelectedValue);
                BindToDDL(ddlParish, lsTC);

                ddlParish.SelectedValue = person.TUMBON_CODE;

            }
            else
            {
                UCModalError.ShowMessageError = res.ErrorMsg;
                UCModalError.ShowModalError();
            }

        }

        private Action<DropDownList, DTO.DataItem[]> BindToDDLArr = (ddl, ls) =>
        {
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Id";
            ddl.DataSource = ls;
            ddl.DataBind();
        };
        private void ClearControl()
        {
            ddlTitle.SelectedIndex = 0;
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtIDNumber.Text = "";
            rblSex.SelectedIndex = 0;
            txtTel.Text = string.Empty;
            txtPlaceGroupTel.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtAddress.Text = string.Empty;
            ddlProvince.SelectedIndex = 0;
            ddlDistrict.SelectedIndex = 0;
            ddlParish.SelectedIndex = 0;
            txtPostcode.Text = string.Empty;

        }
        private void GetProvince()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetProvince(SysMessage.DefaultSelecting);
            BindToDDL(ddlProvince, ls);
        }
        private void GetExamPlaceGroup()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetExamPlaceGroup(SysMessage.DefaultSelecting);
            BindToDDLArr(ddlPlaceGroup, ls.DataResponse);
        }
        private void GetTitleName()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetTitleName(SysMessage.DefaultSelecting);
            BindToDDL(ddlTitle, ls);
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

        /// <summary>
        /// ISSUE : http://jira.ar.co.th/browse/IASAR-411
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <EDITOR>Natta</EDITOR>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            DTO.ResponseMessage<bool> res = this.ControlValidationBeforeSubmit(this.Page);
            if((res.ResultMessage == false) || (res.IsError))
            {  
                    UCModalError.ShowMessageError = res.ErrorMsg;
                    UCModalError.ShowModalError();
                    //return;
                    //ControlValidation(RegisGeneral);
                    return;
            }

            BLL.RegistrationBiz biz = new BLL.RegistrationBiz();
            Registration reg = new Registration();
            reg.ID = IAS.BLL.Helpers.GenerateIdHelper.GetGenAutoId();
            reg.PRE_NAME_CODE = ddlTitle.SelectedValue;
            if (rblSex.SelectedValue == "M")
            {
                reg.SEX = "M";
            }
            else
            {
                reg.SEX = "F";
            }
            reg.NAMES = txtFirstName.Text;
            reg.LASTNAME = txtLastName.Text;
            reg.MEMBER_TYPE = this.MememberTypePlaceGroup;
            reg.COMP_CODE = ddlPlaceGroup.SelectedValue;
            reg.ID_CARD_NO = txtIDNumber.Text;
            reg.PRE_NAME_CODE = ddlTitle.SelectedValue;
            reg.NAMES = txtFirstName.Text;
            reg.LASTNAME = txtLastName.Text;
            reg.EMAIL = txtEmail.Text;
            reg.LOCAL_TELEPHONE = txtPlaceGroupTel.Text;
            reg.TELEPHONE = txtTel.Text;
            reg.ADDRESS_1 = txtAddress.Text;
            reg.PROVINCE_CODE = ddlProvince.SelectedValue;
            reg.AREA_CODE = ddlDistrict.SelectedValue;
            reg.TUMBON_CODE = ddlParish.SelectedValue;
            reg.ZIP_CODE = txtPostcode.Text;
            reg.CREATED_BY = "agdoi";
            reg.CREATED_DATE = DateTime.Now;
            reg.UPDATED_BY = "agdoi";
            reg.UPDATED_DATE = DateTime.Now;
            if (txtPassword.Text == txtPassword.Text)
            {
                //reg.REG_PASSWORD = txtPassword.Text;
                reg.REG_PASS = txtPassword.Text;
            }

            reg.LINK_REDIRECT = Utils.CryptoBase64.Encryption(reg.EMAIL.Trim() + "||" + txtPassword.Text.Trim());
            var attachFiles = this.AttachFiles;
            var result = biz.InsertWithAttatchFile(DTO.RegistrationType.TestCenter, reg, attachFiles);
            if (result.IsError)
            {
                UCModalError.ShowMessageError = result.ErrorMsg;
                UCModalError.ShowModalError();
                return;
                //Response.Redirect("Reg_Place_Group.aspx");
            }
            else
            {
                UCModalSuccess.ShowMessageSuccess = SysMessage.SuccessInsertTypeGroupPlace;
                UCModalSuccess.ShowModalSuccess();
            }

            Session.Remove("TempFolderOracle");
            Session.Remove("AttatchFiles");
            //Session.Abandon();

            ClearControl();

            UCModalError.Visible = false;
            UCModalSuccess.Visible = false;

            string Alert = "alert('" + Resources.infoSysMessage_RegisSuccess2 + "');window.location.assign('../Register/Reg_Place_Group.aspx')";
            ToolkitScriptManager.RegisterStartupScript(this, this.GetType(), "alert", Alert, true);

            //string Alert = "alert('"+ Resources.infoSysMessage_RegisSuccess2 +"');window.location.assign('../home.aspx')";
            //ToolkitScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", Alert, true);
        }

        protected void btnEditSubmit_Click(object sender, EventArgs e)
        {
            var biz = new BLL.PersonBiz();

            PersonTemp item = new PersonTemp();

            item.ID = this.UserProfile.Id;
            item.COMP_CODE = ddlPlaceGroup.SelectedValue;
            item.MEMBER_TYPE = this.MememberTypePlaceGroup;
            item.ID_CARD_NO = txtIDNumber.Text;
            item.PRE_NAME_CODE = ddlTitle.SelectedValue;
            item.NAMES = txtFirstName.Text;
            item.LASTNAME = txtLastName.Text;
            item.SEX = rblSex.SelectedValue;
            item.EMAIL = txtEmail.Text;
            item.LOCAL_TELEPHONE = txtPlaceGroupTel.Text;
            item.TELEPHONE = txtTel.Text;
            item.ADDRESS_1 = txtAddress.Text;
            item.PROVINCE_CODE = ddlProvince.SelectedValue;
            item.AREA_CODE = ddlDistrict.SelectedValue;
            item.TUMBON_CODE = ddlParish.SelectedValue;
            item.ZIP_CODE = txtPostcode.Text;
            item.CREATED_BY = "agdoi";
            item.CREATED_DATE = DateTime.Now;
            item.UPDATED_BY = "agdoi";
            item.UPDATED_DATE = DateTime.Now;


            string firstName = item.NAMES;
            string lastName = txtLastName.Text;

            var final = biz.SetPersonTemp(item, this.PersonAttachFiles.ToArray());



            if (final.IsError)
            {
                UCModalError.ShowMessageError = final.ErrorMsg;
                UCModalError.ShowModalError();
            }

            ClearControl();

            UCModalError.Visible = false;
            UCModalSuccess.Visible = false;



            MasterPage.SetUsername(firstName, lastName);
        
            string Alert = "alert('"+ Resources.infoSysMessage_RegisSuccess2 +"');window.location.assign('../Register/Reg_Place_Group.aspx?Mode=V')";
            ToolkitScriptManager.RegisterStartupScript(this, this.GetType(), "alert", Alert, true);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Register/Reg_Place_Group.aspx");
        }

        protected void btnEditCancel_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["Mode"] != null) 
            { 
                String mode = Request.QueryString["Mode"];
                if(mode == "E"){
                    Response.Redirect("~/Register/Reg_Place_Group.aspx?Mode=E");
                }else
                    Response.Redirect("~/Register/Reg_Place_Group.aspx?Mode=V");
            }else
                Response.Redirect("~/Register/Reg_Place_Group.aspx?Mode=V");
        }

        protected void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetAmpur(SysMessage.DefaultSelecting, ddlProvince.SelectedValue);
            BindToDDL(ddlDistrict, ls);
            ddlParish.SelectedIndex = 0;
        }

        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetTumbon(SysMessage.DefaultSelecting, ddlProvince.SelectedValue, ddlDistrict.SelectedValue);
            BindToDDL(ddlParish, ls);
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
    }
}