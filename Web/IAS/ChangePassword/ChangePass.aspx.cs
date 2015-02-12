using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using System.Text;
using IAS.BLL;
using IAS.Properties;

namespace IAS.ChangePassword
{
    public partial class ChangePass : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                
                if (Request.QueryString["dat"] != null)
                {
                    String getdata =  Utils.CryptoBase64.Decryption(Request.QueryString["dat"].ToString());
                    String[] datas = getdata.Split('|');
                    if (datas.Length == 3)
                    {
                        txtUserName.Text = datas[0];
                        txtOldPassword.Attributes.Add("value", datas[2]); 

                        PersonBiz personBiz = new PersonBiz();
                        var res = personBiz.UserAuthen(txtUserName.Text, datas[2], false,"");
                        if (res.IsError || res.DataResponse==null) {
                            Response.Redirect(PageList.Home);
                        }

                        txtOldPassword.Enabled = false;
                        txtUserName.Enabled = false;
                        DTO.UserProfile userProfile = res.DataResponse;
                        Session[PageList.UserProfile] = userProfile;
                        txtUserName.Text = userProfile.LoginName;
                    }
                }
                else {
                    PushAuthen();
                }
            }
        }

        private void PushAuthen()
        {
            if (Session[PageList.UserProfile] != null) {
                txtUserName.Text = ((DTO.UserProfile)Session[PageList.UserProfile]).LoginName;
            }

            txtUserName.ReadOnly = true;

        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            DTO.ResponseMessage<bool> resValidate = this.RegPassValidation();
            if (resValidate.ResultMessage == false)
            {
                UCModalError.ShowMessageError = this.RegPassValidation().ErrorMsg;
                UCModalError.ShowModalError();
                return;
            }
            else if (resValidate.ResultMessage == true)
            {

                if (txtNewPassword.Text == txtConfirmPassword.Text)
                {
                    //chek รหัสเดิมห้ามตรงกับรหัสใหม่
                    if (txtOldPassword.Text == txtNewPassword.Text)
                    {
                         UCModalError.ShowMessageError = "รหัสผ่านใหม่ห้ามซ้ำกับรหัสผ่านเดิม";
                         UCModalError.ShowModalError();
                         return;
                    }



                    var biz = new BLL.UserAuthenBiz();

                    if (Session[PageList.UserProfile] != null)
                    {
                        DTO.UserProfile userProfile = (DTO.UserProfile)Session[PageList.UserProfile];
                        var res = biz.ChangePassword(userProfile.Id, txtOldPassword.Text, txtNewPassword.Text);
                        if (res.IsError)
                        {
                            var errorMsg = res.ErrorMsg;

                            UCModalError.ShowMessageError = res.ErrorMsg;
                            UCModalError.ShowModalError();
                            return;
                        }
                        else
                        {
                            string Alert = "alert('"+ Resources.infoSysMessage_RegisSuccess2 +"');window.location.assign('../home.aspx')";
                            ToolkitScriptManager.RegisterStartupScript(this, this.GetType(), "alert", Alert, true);

                        }
                    }
                    else {
                        var errorMsg = Resources.errorChangePass_001;

                        UCModalError.ShowMessageError = errorMsg;
                        UCModalError.ShowModalError();
                        return;
                    }
                    

                   
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/BankPage.aspx");
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
            StringBuilder strBuild = new StringBuilder();

            try
            {
                this.Page.Validate("reqValidation");
                if (this.Page.IsValid)
                {

                    res.ResultMessage = true;
                }
                else
                {
                    //รหัสผ่านเดิม
                    if (reqOldPW1.IsValid == false)
                    {
                        //res.ErrorMsg = SysMessage.ReqValidationPW;
                        strBuild.Append(lblOldPassword.Text + " : " + SysMessage.ReqValidationPW + "<br/>");
                        res.ResultMessage = false;
                    }

                    //รหัสผ่านใหม่
                    if (reqPW1.IsValid == false)
                    {
                        //res.ErrorMsg = SysMessage.ReqValidationPW;
                        strBuild.Append(lblNewPassword.Text + " : " + SysMessage.ReqValidationPW + "<br/>");
                        res.ResultMessage = false;
                    }
                    if (regPW1.IsValid == false) //RegularExpressionValidator
                    {
                        //res.ErrorMsg = SysMessage.RegValidationPW;
                        if (strBuild.Length > 0)
                        {
                            bool chkError = strBuild.ToString().Equals(lblNewPassword.Text + " : " + SysMessage.RegValidationPW + "<br/>");
                            if (chkError == false)
                            {
                                strBuild.Append(lblNewPassword.Text + " : " + SysMessage.RegValidationPW + "<br/>");
                            }
                        }
                        else
                        {
                            strBuild.Append(lblNewPassword.Text + " : " + SysMessage.RegValidationPW + "<br/>");
                        }

                        //strBuild.Append(lblNewPassword.Text + " : " + SysMessage.RegValidationPW + "<br/>");
                        res.ResultMessage = false;
                    }

                    //ยืนยันรหัสผ่าน
                    if (reqPW2.IsValid == false)
                    {
                        //res.ErrorMsg = SysMessage.ReqValidationPW;
                        strBuild.Append(lblConfirmPassword.Text + " : " + SysMessage.ReqValidationPW + "<br/>");
                        res.ResultMessage = false;
                    }
                    if (regPW2.IsValid == false) //RegularExpressionValidator
                    {
                        //res.ErrorMsg = SysMessage.RegValidationPW;
                        if (strBuild.Length > 0)
                        {
                            bool chkError = strBuild.ToString().Equals(lblConfirmPassword.Text + " : " + SysMessage.RegValidationPW + "<br/>");
                            if (chkError == false)
                            {
                                strBuild.Append(lblConfirmPassword.Text + " : " + SysMessage.RegValidationPW + "<br/>");
                            }
                        }
                        else
                        {
                            strBuild.Append(lblConfirmPassword.Text + " : " + SysMessage.RegValidationPW + "<br/>");
                        }

                        //strBuild.Append(lblConfirmPassword.Text + " : " + SysMessage.RegValidationPW + "<br/>");
                        res.ResultMessage = false;
                    }
                    if (reqComparePW.IsValid == false)
                    {
                        //res.ErrorMsg = SysMessage.RegValidationCompare;
                        strBuild.Append(lblConfirmPassword.Text + " : " + SysMessage.RegValidationCompare + "<br/>");
                        res.ResultMessage = false;
                    }

                    if ( (this.reqPW1.IsValid == true) && (this.regPW1.IsValid == true)
                       && (this.reqPW2.IsValid == true) && (this.regPW2.IsValid == true) && (this.reqComparePW.IsValid == true))
                    {
                        res.ResultMessage = true;
                    }
                }
                

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }

            if (strBuild != null)
            {
                res.ErrorMsg = strBuild.ToString();
            }

            return res;
        }

        public class FilesValidate
        {
            public bool Status { get; set; }
            public string RegName { get; set; }
        } 
    }
}