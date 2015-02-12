using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.Utils;

using IAS.DTO;
using System.IO;
using IAS.Properties;

namespace IAS.Person
{
    public partial class PersonOIC : basepage
    {
        public string UserID   
        {                                     
            get
            {
                return (string)Session["UserID"];
            }

        }

        public string MemberTypeOfficerOIC
        {
            get
            {
                return (string)Session["MemberTypeOfficerOIC"];
            }
        }

        public MasterPage.Site1 MasterSite
        {

            get
            {
                return (this.Page.Master as IAS.MasterPage.Site1);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetTitleName();
               // แก้ไขข้อมูล (เจ้าหน้าที่ คปภ.)
                lblRegisterOfficer.Text = "แก้ไขข้อมูลผู้ใช้ระบบ (" + GetMemberTypeOIC(UserProfile.MemberType.ToString()).Name + ")";
                using (BLL.PersonBiz biz = new BLL.PersonBiz()) {
                    var res = biz.GetById(UserProfile.Id);
                    if (res.DataResponse != null) {
                        DTO.Person person = res.DataResponse;

                        txtOICUserName.Text = UserProfile.OIC_User_Id;
                        txtIDNumber.Text = UserProfile.OIC_EMP_NO;
                        txtMemberType.Text = GetMemberTypeOIC(UserProfile.MemberType.ToString()).Name;
                        ddlAntecedent.SelectedValue = person.PRE_NAME_CODE;
                        txtFirstName.Text = person.NAMES;
                        txtLastName.Text = person.LASTNAME;
                        rblSex.SelectedValue = person.SEX;

                        if (Request.QueryString["Mode"] != null)
                        {
                            String mode = Request.QueryString["Mode"].Trim();
                            if (mode == "V")
                            {
                                lblRegisterOfficer.Text = "ข้อมูลผู้ใช้ระบบ (" + GetMemberTypeOIC(UserProfile.MemberType.ToString()).Name + ")";
                                txtOICUserName.Enabled = false;
                                txtIDNumber.Enabled = false;
                                txtMemberType.Enabled = false;
                                ddlAntecedent.Enabled = false;
                                txtFirstName.Enabled = false;
                                txtLastName.Enabled = false;
                                rblSex.Enabled = false;
                                btnSubmit.Visible = false;
                                btnCancel.Visible = false;
                                FileSign.Visible = false;
                                lblDescription.Visible = false;
                                if (person.MEMBER_TYPE == "5")
                                {
                                    lblSign.Visible = true;
                                    //if (person.IMG_SIGN != null)
                                    //{
                                        string base64String = biz.GetOicPersonSignImg(person.ID).DataResponse.Signture; // Convert.ToBase64String(person.IMG_SIGN, 0, person.IMG_SIGN.Length);
                                    if( !String.IsNullOrEmpty(base64String))    
                                    ImgSign.ImageUrl = "data:image/png;base64," + base64String;
                                    //}
                                }
                                else
                                {
                                    lblSign.Visible = false;
                                    ImgSign.Visible = false;                                    
                                }
                            }
                        }
                        else
                        {
                            if (person.MEMBER_TYPE == "5")
                            {
                                lblSign.Visible = true;
                                ImgSign.Visible = false;
                                FileSign.Visible = true;
                                lblDescription.Visible = true;
                            }
                            else
                            {
                                ImgSign.Visible = false;
                                FileSign.Visible = false;
                                lblSign.Visible = false;
                                lblDescription.Visible = false;
                            }
                        }                       
                    }

                } 

            }
        }

        private DTO.DataItem GetMemberTypeOIC(String typeCode)
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            DTO.DataItem memberType = biz.GetMemberTypeById(typeCode).DataResponse;// (SysMessage.DefaultSelecting);

            return memberType;
            //return ls.FirstOrDefault(a => a.Id == typeCode).Name;
        }
        private void GetTitleName()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetTitleName(SysMessage.DefaultSelecting);
            BindToDDL(ddlAntecedent, ls);
        }

        private Action<DropDownList, List<DTO.DataItem>> BindToDDL = (ddl, ls) =>
        {
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Id";
            ddl.DataSource = ls;
            ddl.DataBind();
        };

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //InsertMode();

            if (FileSign.FileName != "")
            {
                if (Path.GetExtension(FileSign.FileName) != ".png")
                {
                    this.MasterSite.ModelError.ShowMessageError = Resources.errorReg_OIC_001;
                    this.MasterSite.ModelError.ShowModalError();
                    return;
                    //UCModalError.ShowMessageError = Resources.errorReg_OIC_001;
                    //UCModalError.ShowModalError();
                    //return;
                }
            }
       
            using(BLL.PersonBiz biz = new BLL.PersonBiz()){
                var res = biz.UpdateOIC(UserProfile.Id, txtOICUserName.Text, ddlAntecedent.SelectedValue, 
                    txtFirstName.Text, txtLastName.Text, rblSex.SelectedValue, UserProfile.MemberType.ToString(),FileSign.FileBytes);

                if (res.IsError)
                {
                    this.MasterSite.ModelError.ShowMessageError = res.ErrorMsg;
                    this.MasterSite.ModelError.ShowModalError();
                    //UCModalError.ShowMessageError = res.ErrorMsg;
                    //UCModalError.ShowModalError();
                }
                else
                {
                    this.MasterSite.ModelSuccess.ShowMessageSuccess = SysMessage.SuccessEditTypeOIC;
                    this.MasterSite.ModelSuccess.ShowModalSuccess();
                    //UCModalSuccess.ShowMessageSuccess = SysMessage.SuccessEditTypeOIC;
                    //UCModalSuccess.ShowModalSuccess();
                }
            }
        }



        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/BankPage.aspx");
        }

        private void ClearControl()
        {
            txtIDNumber.Text = string.Empty;
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtOICUserName.Text = string.Empty;
            ddlAntecedent.SelectedIndex = 0;
        }

        private void UploadMode() 
        {
            //BLL.PersonBiz biz = new BLL.PersonBiz();
            //PersonTemp per = new PersonTemp();
            ////Registration reg = new Registration();
            //per.ID = IAS.BLL.Helpers.GenerateIdHelper.GetGenAutoId();
            //per.PRE_NAME_CODE = ddlAntecedent.SelectedValue;
            //if (rblSex.SelectedValue == "M")
            //{
            //    per.SEX = "M";
            //}
            //else
            //{
            //    per.SEX = "F";
            //}
            //per.NAMES = txtFirstName.Text;
            //per.LASTNAME = txtLastName.Text;
            //per.MEMBER_TYPE = this.MemberTypeOfficerOIC;
            //per.EMPLOYEE_NO = txtIDNumber.Text;
            //var result = null;// biz.InsertOIC(txtIDNumber.Text, txtOICUserName.Text
            ////                            , ddlAntecedent.SelectedValue, txtFirstName.Text
            ////                            , txtLastName.Text, rblSex.SelectedValue
            ////                            , ddlMemberType.SelectedValue);
            //if (result.IsError)
            //{
            //    UCModalError.ShowMessageError = result.ErrorMsg;
            //    UCModalError.ShowModalError();
            //}
            //else
            //{
            //    UCModalSuccess.ShowMessageSuccess = SysMessage.SuccessInsertTypeOIC;
            //    UCModalSuccess.ShowModalSuccess();
            //}
            //ClearControl();
            //dvOIC.Visible = false;
        }

    }
}