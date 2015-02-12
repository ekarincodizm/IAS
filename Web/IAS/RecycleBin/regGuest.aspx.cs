using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.DTO;

using IAS.Utils;
using System.Text;
using System.IO;
using System.Threading;
using System.Globalization;
using IAS.Properties;

namespace IAS.RecycleBin
{
    public partial class regGuest : System.Web.UI.Page
    {

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

        public List<DTO.PersonAttatchFile> PersonAttachFiles
        {
            get
            {
                if (Session["PersonAttachFiles"] == null)
                {
                    Session["PersonAttatchFile"] = new List<DTO.PersonAttatchFile>();
                }

                return (List<DTO.PersonAttatchFile>)Session["PersonAttatchFile"];
            }
            set
            {
                Session["PersonAttachFiles"] = value;
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

        public string UserID
        {
            get
            {
                return (string)Session["UserID"];
            }

        }

        public string TempFolderOracle
        {
            get
            {
                return (string)Session["TempFolderOracle"];
            }
        }

        public DTO.DataActionMode DataAction
        {
            get
            {
                return Session["UserProfile"] == null ? DTO.DataActionMode.Add : DTO.DataActionMode.Edit;
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
                return (string)Session["MememberTypeGuest"];
            }
        }

        string mapPath = "~/UploadFile/";

        protected void Page_Load(object sender, EventArgs e)
        {
            txtIDNumber.Attributes.Add("onblur", "javascript:return checkUser();");

            txtIDNumber.Attributes.Add("onkeydown", "javascript:return checkKeyNumberFixPoint(event, this, 13);");
            txtIDNumber.Attributes.Add("onkeypress", "javascript:return checkKeyNumberKeyPressFixPoint(event, this, 13);");

            txtTel.Attributes.Add("onkeydown", "javascript:return checkKeyNumberFixPoint(event, this, 15);");
            txtTel.Attributes.Add("onkeypress", "javascript:return checkKeyNumberKeyPressFixPoint(event, this, 15);");


            txtMobilePhone.Attributes.Add("onkeydown", "javascript:return checkKeyNumberFixPoint(event, this, 15);");
            txtMobilePhone.Attributes.Add("onkeypress", "javascript:return checkKeyNumberKeyPressFixPoint(event, this, 15);");

            txtPostcodeCurrentAddress.Attributes.Add("onkeydown", "javascript:return checkKeyNumberFixPoint(event, this, 5);");
            txtPostcodeCurrentAddress.Attributes.Add("onkeypress", "javascript:return checkKeyNumberKeyPressFixPoint(event, this, 5);");

            txtPostcodeRegisterAddress.Attributes.Add("onkeydown", "javascript:return checkKeyNumberFixPoint(event, this, 5);");
            txtPostcodeRegisterAddress.Attributes.Add("onkeypress", "javascript:return checkKeyNumberKeyPressFixPoint(event, this, 5);");

            if (!Page.IsPostBack)
            {
                Session["MememberTypeGuest"] = "1";
                Session["UserID"] = IAS.BLL.Helpers.GenerateIdHelper.GetGenAutoId();
                Session["TempFolderOracle"] = IAS.BLL.Helpers.GenerateIdHelper.GetGenAutoId();
                //Session["DataAction"] = BLL.DataActionMode.Add;

                if (this.MememberTypeGuest == "1")
                {
                    txtTypeMember.Text = Resources.propReg_NotApprove_MemberTypeGeneral;
                    txtTypeMember.Enabled = false;
                }

                Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");
                txtBirthDay.Text = DateTime.Today.ToString("dd MMMM yyyy");
                txtBirthDay.Text = DateUtil.dd_MMMM_yyyy_Now_TH;

                TabCurrentAddress.CssClass = "Clicked";
                mainView.ActiveViewIndex = 0;

                //addData();

                GetProvince();
                GetTitle();
                GetEducation();
                GetNationality();
                GetAttachFilesType();

                BindDataInGridView();
                GetAttachFilesTypeImage();

                if (this.DataAction == DTO.DataActionMode.Edit)
                {
                    ClearControl();
                    InitData();
                    GetAttatchFiles();
                }



            }
        }

        private void InitData()
        {
            BLL.PersonBiz biz = new BLL.PersonBiz();
            var res = biz.GetById(this.UserProfile.Id);

            if (!res.IsError)
            {
                var person = res.DataResponse;

                ddlTitle.SelectedValue = person.PRE_NAME_CODE;
                txtFirstName.Text = person.NAMES;
                txtLastName.Text = person.LASTNAME;
                txtIDNumber.Text = person.ID_CARD_NO;
                txtBirthDay.Text = person.BIRTH_DATE.Value.ToString("dd MMMM yyyy");
                rblSex.SelectedValue = person.SEX;
                ddlNationality.SelectedValue = person.NATIONALITY;
                ddlEducation.SelectedValue = person.EDUCATION_CODE;
                txtEmail.Text = person.EMAIL;
                txtTel.Text = person.LOCAL_TELEPHONE;
                txtMobilePhone.Text = person.TELEPHONE;
                txtCurrentAddress.Text = person.ADDRESS_1;

                var message = SysMessage.DefaultSelecting;

                BLL.DataCenterBiz dataCenter = new BLL.DataCenterBiz();
                ddlProvinceCurrentAddress.SelectedValue = person.PROVINCE_CODE;
                var lsPC = dataCenter.GetAmpur(message, ddlProvinceCurrentAddress.SelectedValue);
                BindToDDL(ddlDistrictCurrentAddress, lsPC);

                ddlDistrictCurrentAddress.SelectedValue = person.AREA_CODE;
                var lsTC = dataCenter.GetTumbon(message, ddlProvinceCurrentAddress.SelectedValue, ddlDistrictCurrentAddress.SelectedValue);
                BindToDDL(ddlParishCurrentAddress, lsTC);

                ddlParishCurrentAddress.SelectedValue = person.TUMBON_CODE;

                txtPostcodeCurrentAddress.Text = person.ZIP_CODE;
                txtRegisterAddress.Text = person.LOCAL_ADDRESS1;

                ddlProvinceRegisterAddress.SelectedValue = person.LOCAL_PROVINCE_CODE;
                var lsPR = dataCenter.GetAmpur(message, ddlProvinceRegisterAddress.SelectedValue);
                BindToDDL(ddlDistrictRegisterAddress, lsPR);

                ddlDistrictRegisterAddress.SelectedValue = person.LOCAL_AREA_CODE;
                var lsTR = dataCenter.GetTumbon(message, ddlProvinceRegisterAddress.SelectedValue, ddlDistrictRegisterAddress.SelectedValue);
                BindToDDL(ddlParishRegisterAddress, lsTR);

                ddlParishRegisterAddress.SelectedValue = person.TUMBON_CODE;

                txtPostcodeRegisterAddress.Text = person.LOCAL_ZIPCODE;

            }
            else
            {
                var errorMsg = res.ErrorMsg;

                AlertMessage.ShowAlertMessage(string.Empty, errorMsg);
            }

        }

        private void GetAttatchFiles()
        {
            var biz = new BLL.PersonBiz();
            string personID = this.UserProfile.Id;
            DTO.ResponseService<DTO.PersonAttatchFile[]> res = biz.GetAttatchFileByPersonId(personID);

            var list = res.DataResponse.ToList();


            if (this.DataAction == DTO.DataActionMode.Edit)
            {
                this.PersonAttachFiles = list;
            }

            gvUpload.DataSource = list;
            gvUpload.DataBind();

        }


        protected void TabCurrentAddress_Click(object sender, EventArgs e)
        {
            TabCurrentAddress.CssClass = "Clicked";
            TabRegisterAddress.CssClass = "Initial";
            mainView.ActiveViewIndex = 0;
        }

        protected void TabRegisterAddress_Click(object sender, EventArgs e)
        {
            TabCurrentAddress.CssClass = "Initial";
            TabRegisterAddress.CssClass = "Clicked";
            mainView.ActiveViewIndex = 1;
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

            if (string.IsNullOrEmpty(txtCurrentAddress.Text) && txtCurrentAddress.Text.Length < 1)
            {
                if (message.Length > 0)
                {
                    message.Append(", ");
                }
                message.Append(lblCurrentAddress.Text);
                if (!isFocus)
                {
                    txtCurrentAddress.Focus();
                    isFocus = true;
                }
            }

            if (ddlProvinceCurrentAddress.SelectedValue.Length < 1 && ddlProvinceCurrentAddress.SelectedIndex == 0)
            {
                if (message.Length > 0)
                {
                    message.Append(", ");
                }
                message.Append(lblProvinceCurrentAddress.Text);
                if (!isFocus)
                {
                    ddlProvinceCurrentAddress.Focus();
                    isFocus = true;
                }
            }

            if (ddlDistrictCurrentAddress.SelectedValue.Length < 1 && ddlDistrictCurrentAddress.SelectedIndex == 0)
            {
                if (message.Length > 0)
                {
                    message.Append(", ");
                }
                message.Append(lblDistrictCurrentAddress.Text);
                if (!isFocus)
                {
                    ddlDistrictCurrentAddress.Focus();
                    isFocus = true;
                }
            }

            if (ddlParishCurrentAddress.SelectedValue.Length < 1 && ddlParishCurrentAddress.SelectedIndex == 0)
            {
                if (message.Length > 0)
                {
                    message.Append(", ");
                }
                message.Append(lblParishCurrentAddress.Text);
                if (!isFocus)
                {
                    ddlParishCurrentAddress.Focus();
                    isFocus = true;
                }
            }

            if (string.IsNullOrEmpty(txtPostcodeCurrentAddress.Text) && txtPostcodeCurrentAddress.Text.Length < 1)
            {
                if (message.Length > 0)
                {
                    message.Append(", ");
                }
                message.Append(lblPostcodeCurrentAddress.Text);
                if (!isFocus)
                {
                    txtPostcodeCurrentAddress.Focus();
                    isFocus = true;
                }
            }

            if (string.IsNullOrEmpty(txtRegisterAddress.Text) && txtRegisterAddress.Text.Length < 1)
            {
                if (message.Length > 0)
                {
                    message.Append(", ");
                }
                message.Append(lblRegisterAddress.Text);
                if (!isFocus)
                {
                    txtRegisterAddress.Focus();
                    isFocus = true;
                }
            }

            if (ddlProvinceRegisterAddress.SelectedValue.Length < 1 && ddlProvinceRegisterAddress.SelectedIndex == 0)
            {
                if (message.Length > 0)
                {
                    message.Append(", ");
                }
                message.Append(lblProvinceRegisterAddress.Text);
                if (!isFocus)
                {
                    ddlProvinceRegisterAddress.Focus();
                    isFocus = true;
                }
            }

            if (ddlDistrictRegisterAddress.SelectedValue.Length < 1 && ddlDistrictRegisterAddress.SelectedIndex == 0)
            {
                if (message.Length > 0)
                {
                    message.Append(", ");
                }
                message.Append(lblDistrictRegisterAddress.Text);
                if (!isFocus)
                {
                    ddlDistrictRegisterAddress.Focus();
                    isFocus = true;
                }
            }

            if (ddlParishRegisterAddress.SelectedValue.Length < 1 && ddlParishRegisterAddress.SelectedIndex == 0)
            {
                if (message.Length > 0)
                {
                    message.Append(", ");
                }
                message.Append(lblParishRegisterAddress.Text);
                if (!isFocus)
                {
                    ddlParishRegisterAddress.Focus();
                    isFocus = true;
                }
            }

            if (string.IsNullOrEmpty(txtPostcodeRegisterAddress.Text) && txtPostcodeRegisterAddress.Text.Length < 1)
            {
                if (message.Length > 0)
                {
                    message.Append(", ");
                }
                message.Append(lblProvinceRegisterAddress.Text);
                if (!isFocus)
                {
                    txtPostcodeRegisterAddress.Focus();
                    isFocus = true;
                }
            }

            if (!chkCodition.Checked)
            {
                if (message.Length > 0)
                {
                    message.Append(", ");
                }
            }

            if (string.IsNullOrEmpty(txtPassword.Text) && txtPassword.Text.Length < 1)
            {
                if (message.Length > 0)
                {
                    message.Append(", ");
                }
                message.Append(lblPassword.Text);
                if (!isFocus)
                {
                    txtPassword.Focus();
                    isFocus = true;
                }
            }

            if (string.IsNullOrEmpty(txtConfirmPassword.Text) && txtConfirmPassword.Text.Length < 1)
            {
                if (message.Length > 0)
                {
                    message.Append(", ");
                }
                message.Append(lblConfirmPassword.Text);
                if (!isFocus)
                {
                    txtPassword.Focus();
                    isFocus = true;
                }
            }

            if (message.Length > 0)
            {
                AlertMessage.ShowAlertMessage(SysMessage.DataEmpty, message.ToString());

                return false;
            }
            if (messageOther.Length > 0)
            {
                AlertMessage.ShowAlertMessage(string.Empty, messageOther.ToString());
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
                AlertMessage.ShowAlertMessage(string.Empty, SysMessage.EmailErrorFormat);

                return false;
            }

        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            //if (!ValidDateInput())
            //{
            //    return;
            //}

            udpCondition.Visible = true;
            btnOk.Visible = false;
            btnCancel.Visible = false;
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (chkCodition.Checked)
            {
                btnSubmit.Enabled = true;

                if (!ValidDateInput())
                {
                    return;
                }

                Registration item = new Registration();

                var attachFiles = this.AttachFiles;

                item.ID = this.UserID;

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
                item.ADDRESS_1 = txtCurrentAddress.Text;
                item.PROVINCE_CODE = ddlProvinceCurrentAddress.SelectedValue;
                item.AREA_CODE = ddlDistrictCurrentAddress.SelectedValue;
                item.TUMBON_CODE = ddlParishCurrentAddress.SelectedValue;
                item.ZIP_CODE = txtPostcodeCurrentAddress.Text;
                item.LOCAL_ADDRESS1 = txtRegisterAddress.Text;
                item.LOCAL_PROVINCE_CODE = ddlProvinceRegisterAddress.SelectedValue;
                item.LOCAL_AREA_CODE = ddlDistrictRegisterAddress.SelectedValue;
                item.LOCAL_TUMBON_CODE = ddlParishRegisterAddress.SelectedValue;
                item.LOCAL_ZIPCODE = txtPostcodeRegisterAddress.Text;
                item.CREATED_BY = "123";
                item.CREATED_DATE = DateTime.Now;
                item.UPDATED_BY = "123";
                item.UPDATED_DATE = DateTime.Now;
                if (txtPassword.Text == txtConfirmPassword.Text)
                {
                    item.REG_PASSWORD = txtPassword.Text;
                }
                else
                {
                    AlertMessage.ShowAlertMessage(string.Empty, SysMessage.NotSame);
                    return;
                }


                if (item != null)
                {
                    //var res = biz.Insert(item);

                    BLL.RegistrationBiz biz = new BLL.RegistrationBiz();

                    var result = biz.ValidateBeforeSubmit(DTO.RegistrationType.General, item);

                    if (result.IsError)
                    {
                        var errorMsg = result.ErrorMsg;
                        AlertMessage.ShowAlertMessage(string.Empty, errorMsg);
                    }
                    else
                    {
                        if (this.AttachFiles.Count != 0)
                        {
                            foreach (var att in this.AttachFiles)
                            {
                                var isImage = att.IsImage;
                                FileInfo f = new FileInfo(att.ATTACH_FILE_PATH);
                                string surNameFile = f.Extension;

                                if (isImage)
                                {
                                    var tempPath = att.TempFilePath;
                                    string[] arrTempPath = tempPath.Split('/');

                                    var source = Server.MapPath(mapPath + arrTempPath[0] + "/" + arrTempPath[1]);
                                    var target = Server.MapPath(mapPath + txtIDNumber.Text + surNameFile);


                                    System.IO.Directory.Move(source, target);
                                }
                                else
                                {
                                    var tempPath = att.TempFilePath;
                                    string[] arrTempPath = tempPath.Split('/');

                                    var source = Server.MapPath(mapPath + arrTempPath[0] + "/" + arrTempPath[1]);
                                    var target = Server.MapPath(mapPath + txtIDNumber.Text + "_" + att.ATTACH_FILE_TYPE + surNameFile);

                                    System.IO.Directory.Move(source, target);
                                }

                            }

                            DirectoryInfo deleteDirectory = new DirectoryInfo(Server.MapPath(mapPath) + this.TempFolderOracle);

                            deleteDirectory.Delete();
                        }

                        var res = biz.InsertWithAttatchFile(DTO.RegistrationType.General, item, attachFiles);

                        //ถ้าเกิด Error อะไรให้มาทำในที่นี่ Tob 12022013
                        if (res.IsError)
                        {
                            //Response.Write(res.ErrorMsg);
                            var errorMsg = res.ErrorMsg;

                            AlertMessage.ShowAlertMessage(string.Empty, errorMsg);

                        }
                        else
                        {

                            Session.Remove("TempFolderOracle");
                            Session.Remove("AttatchFiles");

                            ClearControl();

                            //AlertMessage.ShowAlertMessage(string.Empty, SysMessage.SaveSucess);

                            //string strScript = "<script>" + "alert('ทำการบันทึกข้อมูลเรียบร้อย');";
                            //strScript += "window.location='~/home.aspx';";
                            //strScript += "</script>";
                            //Page.ClientScript.RegisterStartupScript(this.GetType(), "Startup", strScript);

                            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('"+ Resources.infoSysMessage_RegisSuccess2 +"');window.location.href='home.aspx';", true);
                            //Response.Write("<script>alert('ทำการบันทึกข้อมูลเรียบร้อย');window.location.href='home.aspx';</script>");

                            //Response.Redirect("~/home.aspx");

                            //foreach (var att in attachFiles)
                            //{


                            //    var nameFile = att.ATTACH_FILE_PATH;

                            //    string[] arrNameFile = nameFile.Split('/');

                            //    var source = Server.MapPath(mapPath + arrNameFile[0] + "/" + "_" + arrNameFile[1]);
                            //    var target = Server.MapPath(mapPath + arrNameFile[0] + "/" + arrNameFile[1]);


                            //    System.IO.Directory.Move(source, target);
                            //}
                        }
                    }


                    //ถ้าเกิด Error อะไรให้มาทำในที่นี่ Tob 12022013


                    //Response.Write(detail.ErrorMsg);
                }
                else
                {
                    AlertMessage.ShowAlertMessage(SysMessage.Fail, SysMessage.TryAgain);
                }
            }
            else
            {
                AlertMessage.ShowAlertMessage(string.Empty, SysMessage.CheckCondition);

                btnSubmit.Enabled = false;
            }

        }

        protected void ddlProvinceCurrentAddress_SelectedIndexChanged(object sender, EventArgs e)
        {
            var message = SysMessage.DefaultSelecting;

            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetAmpur(message, ddlProvinceCurrentAddress.SelectedValue);
            BindToDDL(ddlDistrictCurrentAddress, ls);
        }

        protected void ddlDistrictCurrentAddress_SelectedIndexChanged(object sender, EventArgs e)
        {
            var message = SysMessage.DefaultSelecting;

            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetTumbon(message, ddlProvinceCurrentAddress.SelectedValue, ddlDistrictCurrentAddress.SelectedValue);
            BindToDDL(ddlParishCurrentAddress, ls);
        }

        protected void ddlProvinceRegisterAddress_SelectedIndexChanged(object sender, EventArgs e)
        {
            var message = SysMessage.DefaultSelecting;

            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetAmpur(message, ddlProvinceRegisterAddress.SelectedValue);
            BindToDDL(ddlDistrictRegisterAddress, ls);
        }

        protected void ddlDistrictRegisterAddress_SelectedIndexChanged(object sender, EventArgs e)
        {
            var message = SysMessage.DefaultSelecting;

            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetTumbon(message, ddlProvinceRegisterAddress.SelectedValue, ddlDistrictRegisterAddress.SelectedValue);
            BindToDDL(ddlParishRegisterAddress, ls);
        }

        //private List<RegistrationAttatchFile> GetDataTestGridview()
        //{
        //    var data = new List<RegistrationAttatchFile>();

        //    //RegistrationAttatchFile ts = new RegistrationAttatchFile();


        //    ///*การประกาศค่า ในแบบที่เรากรอกข้อมูลลงไป Tob Edit 11022013*/
        //    for (int i = 0; i < 5; i++)
        //    {
        //        data.Add(new RegistrationAttatchFile
        //        {
        //            ATTACH_FILE_TYPE = "สำเนาบัตรประชาชน",
        //            ATTACH_FILE_PATH = "IDCard.pdf",
        //            REMARK = "Test",
        //            ID = "Test"
        //        }
        //                );
        //    }
        //    ///*การประกาศค่า ในแบบที่เรากรอกข้อมูลลงไป Tob Edit 11022013*/
        //    ///

        //    ///*การประกาศค่า ในแบบที่เรามีข้อมูลอยู่แล้ว Tob Edit 11022013*/
        //    //TestData ts2 = new TestData
        //    //{
        //    //    TypeAttachment = "สำเนาบัตรประชาชน",
        //    //    File = "IDCard.pdf",
        //    //    Detail = "Test",
        //    //    Action = "Test"
        //    //};

        //    //data.Add(ts2);
        //    ///*การประกาศค่า ในแบบที่เรามีข้อมูลอยู่แล้ว Tob Edit 11022013*/

        //    return data;

        //}

        //private List<Registration> GetDataTestRegister()
        //{
        //    var data = new List<Registration>();

        //    string id = Session["UserID"].ToString();

        //    data.Add(new Registration
        //    {
        //        ID = id,
        //        MEMBER_TYPE = "1",
        //        PRE_NAME_CODE = "1",
        //        NAMES = "สมชาย",
        //        LASTNAME = "เข็มขัด",
        //        ID_CARD_NO = "1120000324121",
        //        SEX = "1",
        //        TELEPHONE = "024123974",
        //        MOBILE_PHONE = "0834301120",
        //        EMAIL = "sumchai@gmail.com",
        //        CREATED_BY = 123,
        //        CREATED_DATE = DateTime.Now,
        //        UPDATED_BY = 123,
        //        UPDATED_DATE = DateTime.Now


        //    });

        //    return data;
        //}

        //public void addData()
        //{
        //    List<Registration> _registration = this.GetDataTestRegister();


        //    foreach (var item in _registration)
        //    {
        //        txtTypeMember.Text = "1";
        //        txtFirstName.Text = item.NAMES;
        //        txtLastName.Text = item.LASTNAME;
        //        txtIDNumber.Text = item.ID_CARD_NO;
        //        txtMobilePhone.Text = item.MOBILE_PHONE;
        //        txtEmail.Text = item.EMAIL;


        //    }
        //}

        private void BindDataInGridView()
        {
            gvUpload.DataSource = this.AttachFiles;
            gvUpload.DataBind();
        }

        private void UploadFileImage(string fileName)
        {

            var list = this.AttachFiles;

            string userID = this.UserID;
            string tempFilePath = this.TempFolderOracle;

            string[] tempFileName = fileName.Split('_');

            string masterFileName = tempFileName[1];

            list.Add(new RegistrationAttatchFile
            {
                ID = IAS.BLL.Helpers.GenerateIdHelper.GetGenAutoId(),
                REGISTRATION_ID = userID,
                ATTACH_FILE_TYPE = ddlTypeAttachment.SelectedValue,
                DocumentTypeName = ddlTypeAttachment.SelectedItem.ToString(),
                ATTACH_FILE_PATH = masterFileName,
                REMARK = txtDetail.Text,
                TempFilePath = tempFilePath + "/" + masterFileName,
                IsImage = true,
                CREATED_BY = userID,
                CREATED_DATE = DateTime.Now,
                UPDATED_BY = userID,
                UPDATED_DATE = DateTime.Now
            });


            this.AttachFiles = list;
            BindDataInGridView();

        }

        private void UploadFile(string fileName)
        {
            var list = this.AttachFiles;

            string userID = this.UserID;
            string tempFilePath = this.TempFolderOracle;

            string[] tempFileName = fileName.Split('_');

            string masterFileName = tempFileName[1] + "_" + tempFileName[2];

            list.Add(new RegistrationAttatchFile
            {
                ID = IAS.BLL.Helpers.GenerateIdHelper.GetGenAutoId(),
                REGISTRATION_ID = userID,
                ATTACH_FILE_TYPE = ddlTypeAttachment.SelectedValue,
                DocumentTypeName = ddlTypeAttachment.SelectedItem.ToString(),
                ATTACH_FILE_PATH = masterFileName,
                REMARK = txtDetail.Text,
                TempFilePath = tempFilePath + "/" + masterFileName,
                IsImage = false,
                CREATED_BY = userID,
                CREATED_DATE = DateTime.Now,
                UPDATED_BY = userID,
                UPDATED_DATE = DateTime.Now
            });


            this.AttachFiles = list;
            BindDataInGridView();

        }


        protected void btnUploadFile_Click(object sender, EventArgs e)
        {
            string tempFileName = Path.GetFileName(fulFile.PostedFile.FileName);


            bool invalid = validateFileType(tempFileName);


            if (ddlTypeAttachment.SelectedIndex != 0)
            {
                if (tempFileName != string.Empty)
                {
                    FileInfo f = new FileInfo(tempFileName);
                    string surNameFile = f.Extension;

                    string fileName = string.Empty;

                    var TypeFile = AttachFiles.Where(w => w.ATTACH_FILE_TYPE == ddlTypeAttachment.SelectedValue).FirstOrDefault();


                    string yearMonthDay = DateTime.Now.ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                    string hourMinSec = DateTime.Now.ToString("HHmmss", System.Globalization.CultureInfo.InvariantCulture);
                    DirectoryInfo DirInfo = new DirectoryInfo(Server.MapPath(mapPath) + this.TempFolderOracle);

                    if (invalid)
                    {
                        if (txtIDNumber.Text != string.Empty)
                        {
                            var attFileImage = this.GetDocumentTypeIsImage.Where(w => w.Id == ddlTypeAttachment.SelectedValue).FirstOrDefault();

                            if (attFileImage != null)
                            {
                                if (TypeFile == null)
                                {
                                    tempFileName = "_" + txtIDNumber.Text + surNameFile;

                                    UploadFileImage(tempFileName);

                                    string[] _fileName = tempFileName.Split('_');

                                    string masterFileName = _fileName[1];

                                    //*** Create Folder ***//
                                    if (!DirInfo.Exists)
                                    {
                                        DirInfo.Create();

                                        fulFile.PostedFile.SaveAs(Server.MapPath(mapPath + "/" + this.TempFolderOracle + "/" + masterFileName));

                                        ddlTypeAttachment.SelectedIndex = 0;
                                        txtDetail.Text = string.Empty;
                                    }
                                    else
                                    {
                                        fulFile.PostedFile.SaveAs(Server.MapPath(mapPath + "/" + this.TempFolderOracle + "/" + masterFileName));

                                        ddlTypeAttachment.SelectedIndex = 0;
                                        txtDetail.Text = string.Empty;
                                    }
                                }
                                else
                                {
                                    AlertMessage.ShowAlertMessage(SysMessage.DeleteFile, SysMessage.PleaseDeleteFile);

                                    ddlTypeAttachment.SelectedIndex = 0;
                                    txtDetail.Text = string.Empty;
                                }

                                
                            }
                            else
                            {
                                if (TypeFile == null)
                                {
                                    tempFileName = "_" + txtIDNumber.Text + "_" + ddlTypeAttachment.SelectedValue + surNameFile;

                                    UploadFile(tempFileName);

                                    string[] _fileName = tempFileName.Split('_');

                                    string masterFileName = _fileName[1] + "_" + _fileName[2];

                                    //*** Create Folder ***//
                                    if (!DirInfo.Exists)
                                    {
                                        DirInfo.Create();

                                        fulFile.PostedFile.SaveAs(Server.MapPath(mapPath + "/" + this.TempFolderOracle + "/" + masterFileName));

                                        ddlTypeAttachment.SelectedIndex = 0;
                                        txtDetail.Text = string.Empty;
                                    }
                                    else
                                    {
                                        fulFile.PostedFile.SaveAs(Server.MapPath(mapPath + "/" + this.TempFolderOracle + "/" + masterFileName));

                                        ddlTypeAttachment.SelectedIndex = 0;
                                        txtDetail.Text = string.Empty;
                                    }

                                }
                                else
                                {
                                    AlertMessage.ShowAlertMessage(SysMessage.DeleteFile, SysMessage.PleaseDeleteFile);

                                    ddlTypeAttachment.SelectedIndex = 0;
                                    txtDetail.Text = string.Empty;
                                }

                            }
                        }
                        else
                        {
                            AlertMessage.ShowAlertMessage(string.Empty, SysMessage.PleaseInputIDNumber);

                            ddlTypeAttachment.SelectedIndex = 0;
                            txtDetail.Text = string.Empty;
                        }
                    }
                    else
                    {
                        AlertMessage.ShowAlertMessage(SysMessage.SelectFile, SysMessage.PleaseSelectFile);

                        ddlTypeAttachment.SelectedIndex = 0;
                        txtDetail.Text = string.Empty;
                    }

                }
                else
                {
                    AlertMessage.ShowAlertMessage(SysMessage.SelectFile, SysMessage.PleaseChooseFile);

                    ddlTypeAttachment.SelectedIndex = 0;
                    txtDetail.Text = string.Empty;
                }
            }
            else
            {
                AlertMessage.ShowAlertMessage(SysMessage.SelectFile, SysMessage.PleaseSelectFile);

                ddlTypeAttachment.SelectedIndex = 0;
                txtDetail.Text = string.Empty;
            }








        }

        private static bool validateFileType(string tempFileName)
        {
            string fileExtension = System.IO.Path.GetExtension(tempFileName).Replace(".", string.Empty).ToLower();
            bool invalidFileExtensions = DTO.DocumentFileType.IMAGE_BMP_GIF_JPG_PNG_TIF_PDF.ToString().ToLower().Contains(fileExtension);

            //bool invalid = false;

            //foreach (string extension in invalidFileExtensions)
            //{
            //    if (fileExtension == extension)
            //        invalid = true;
            //}

            return invalidFileExtensions;
        }


        protected void hplView_Click(object sender, EventArgs e)
        {
            var gr = (GridViewRow)((LinkButton)sender).NamingContainer;

            var text = (Label)gr.FindControl("lblFileGv");

            Session["ViewFileName"] = text.Text;

            string url = "regViewDocument.aspx";
            ClientScript.RegisterStartupScript(this.GetType(), "newWindow", String.Format("<script>window.open('{0}'); </script>", Page.ResolveUrl(url)));


        }

        protected void hplDelete_Click(object sender, EventArgs e)
        {
            var gr = (GridViewRow)((LinkButton)sender).NamingContainer;

            Label lblFileGv = (Label)gr.FindControl("lblFileGv");

            var list = this.AttachFiles;

            var _item = list.Find(l => l.ATTACH_FILE_PATH == lblFileGv.Text);

            list.Remove(_item);

            var source = Server.MapPath(mapPath + "/" + lblFileGv.Text);

            FileInfo fiPath = new FileInfo(source);
            if (fiPath.Exists)
            {
                File.Delete(source);
            }

            gvUpload.DataSource = list;
            gvUpload.DataBind();

            ddlTypeAttachment.SelectedIndex = 0;
            txtDetail.Text = string.Empty;
        }

        protected void gvUpload_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvUpload.EditIndex = e.NewEditIndex;


            BindDataInGridView();


        }

        protected void gvUpload_PreRender(object sender, EventArgs e)
        {
            if (this.gvUpload.EditIndex != -1)
            {
                LinkButton hplView = gvUpload.Rows[gvUpload.EditIndex].Cells[0].FindControl("hplView") as LinkButton;
                LinkButton hplDelete = gvUpload.Rows[gvUpload.EditIndex].Cells[0].FindControl("hplDelete") as LinkButton;

                ((DataControlField)gvUpload.Columns
                .Cast<DataControlField>()
                .Where(fld => fld.HeaderText == "ดำเนินการ")
                .SingleOrDefault()).Visible = false;
            }

        }

        protected void gvUpload_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            e.Cancel = true;
            gvUpload.EditIndex = -1;

            ((DataControlField)gvUpload.Columns
            .Cast<DataControlField>()
            .Where(fld => fld.HeaderText == "ดำเนินการ")
            .SingleOrDefault()).Visible = true;

            BindDataInGridView();
        }

        protected void gvUpload_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gvUpload.Rows[e.RowIndex];

            TextBox txtDetailGv = (TextBox)row.FindControl("txtDetailGv");


            string _ID = gvUpload.DataKeys[e.RowIndex].Value.ToString();

            var _item = this.AttachFiles.Find(l => l.ID == _ID);

            if (_item != null)
            {
                _item.REMARK = txtDetailGv.Text;
            }

            gvUpload.EditIndex = -1;

            ((DataControlField)gvUpload.Columns
            .Cast<DataControlField>()
            .Where(fld => fld.HeaderText == "ดำเนินการ")
            .SingleOrDefault()).Visible = true;

            BindDataInGridView();


        }

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
            txtCurrentAddress.Text = string.Empty;
            ddlProvinceCurrentAddress.SelectedIndex = 0;
            ddlDistrictCurrentAddress.SelectedIndex = 0;
            ddlParishCurrentAddress.SelectedIndex = 0;
            txtPostcodeCurrentAddress.Text = string.Empty;
            txtRegisterAddress.Text = string.Empty;
            ddlProvinceRegisterAddress.SelectedIndex = 0;
            ddlDistrictRegisterAddress.SelectedIndex = 0;
            ddlParishRegisterAddress.SelectedIndex = 0;
            txtPostcodeRegisterAddress.Text = string.Empty;

            //Attach File//
            ddlTypeAttachment.SelectedIndex = 0;
            txtDetail.Text = string.Empty;

            pnlMain.DefaultButton = btnOk.ID.ToString();
        }

        private void DisEnabled()
        {
            ddlTitle.Enabled = false;
            txtFirstName.Enabled = false;
            txtLastName.Enabled = false;
            txtIDNumber.Enabled = false;
            txtBirthDay.Enabled = false;
            rblSex.Enabled = false;
            ddlNationality.Enabled = false;
            ddlEducation.Enabled = false;
            txtMobilePhone.Enabled = false;
            txtEmail.Enabled = false;
            txtCurrentAddress.Enabled = false;
            ddlProvinceCurrentAddress.Enabled = false;
            ddlDistrictCurrentAddress.Enabled = false;
            ddlParishCurrentAddress.Enabled = false;
            txtPostcodeCurrentAddress.Enabled = false;
            txtRegisterAddress.Enabled = false;
            ddlProvinceRegisterAddress.Enabled = false;
            ddlDistrictRegisterAddress.Enabled = false;
            ddlParishRegisterAddress.Enabled = false;
            txtPostcodeRegisterAddress.Enabled = false;
            ddlTypeAttachment.Enabled = false;
            txtDetail.Enabled = false;
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
            BindToDDL(ddlProvinceCurrentAddress, ls);
            BindToDDL(ddlProvinceRegisterAddress, ls);
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
            BindToDDL(ddlTypeAttachment, ls);
        }

        private void GetAttachFilesTypeImage()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            this.GetDocumentTypeIsImage = biz.GetDocumentTypeIsImage();
        }

        //private void GetMemberType()
        //{
        //    BLL.RegistrationService.RegistrationType.General.GetEnumValue();
        //}









    }
}