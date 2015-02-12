using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.MasterPage;


namespace IAS.Mockup
{
    public partial class RegisFormMaster : System.Web.UI.Page
    {
        public MasterRegister MasterPage { 
            get { return (this.Page.Master as MasterRegister);} 
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            txtIDNumber.Attributes.Add("onblur", "javascript:return checkUser(" + txtIDNumber.ClientID + ");");

            txtEmail.Attributes.Add("onblur", "javascript:return checkemail(" + txtEmail.ClientID + ");");

            //txtIDNumber.Attributes.Add("onkeydown", "javascript:return checkKeyNumberFixPoint(event, this, 13);");
            //txtIDNumber.Attributes.Add("onkeypress", "javascript:return checkKeyNumberKeyPressFixPoint(event, this, 13);");

            txtTel.Attributes.Add("onkeydown", "javascript:return checkKeyNumberFixPoint(event, this, 15);");
            txtTel.Attributes.Add("onkeypress", "javascript:return checkKeyNumberKeyPressFixPoint(event, this, 15);");


            txtMobilePhone.Attributes.Add("onkeydown", "javascript:return checkKeyNumberFixPoint(event, this, 15);");
            txtMobilePhone.Attributes.Add("onkeypress", "javascript:return checkKeyNumberKeyPressFixPoint(event, this, 15);");

            if (!Page.IsPostBack) {
                GetEducation();
                GetNationality();
                GetTitle();
            }
        }
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            (this.Page.Master as MasterRegister).OkRegister_Click += new EventHandler(Ok_Click);
            (this.Page.Master as MasterRegister).CancelRegister_Click += new EventHandler(Cancel_Click);
            (this.Page.Master as MasterRegister).CheckAgreement_CheckedChanged += new EventHandler(Agreement_CheckedChanged);
            (this.Page.Master as MasterRegister).SaveRegiter_Click += new EventHandler(Save_Click);
        }
        protected void Ok_Click(Object sender, EventArgs e)
        {
            (this.Page.Master as MasterRegister).ConfirmUserName = txtIDNumber.Text.Trim();
          
        }
        protected void Cancel_Click(Object sender, EventArgs e) 
        { 
        
        }
        protected void Agreement_CheckedChanged(Object sender, EventArgs e) 
        { 
        
        }
        protected void Save_Click(Object sender, EventArgs e) 
        { 
        
        }

        protected void chkCopyAdd_CheckedChanged(object sender, EventArgs e)
        {
            //ucAddressCurrent.Clone(ucAddressRegister);
            //ucAddressRegister = ucAddressCurrent;
            if (((CheckBox)sender).Checked)
            {

                CloneAddress();
                ucAddressRegister.Enabled(false);
            }
            else {
                ucAddressRegister.Enabled(true);
                ucAddressRegister.Clear();
            }
            
            UpdatePanelUpload.Update();

        }

        private void CloneAddress()
        {
            ucAddressRegister.DropdownProvince.SelectedIndex = ucAddressCurrent.DropdownProvince.SelectedIndex;

            ucAddressRegister.DropdownDistrict.Items.Clear();
            ListItem districtItem = ucAddressCurrent.DropdownDistrict.SelectedItem;
            ucAddressRegister.DropdownDistrict.Items.Add(districtItem);

            ucAddressRegister.DropdownParish.Items.Clear();
            ListItem parish = ucAddressCurrent.DropdownParish.SelectedItem;
            ucAddressRegister.DropdownParish.Items.Add(parish);

            ucAddressRegister.Address = ucAddressCurrent.Address;
            ucAddressRegister.PostCode = ucAddressCurrent.PostCode;
        }

        protected void TabAddress_ActiveTabChanged(object sender, EventArgs e)
        {
            if (TabAddress.ActiveTabIndex == 1 && chkCopyAdd.Checked) {
                CloneAddress();
            }
        }
        private Action<DropDownList, List<DTO.DataItem>> BindToDDL = (ddl, ls) =>
        {
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Id";
            ddl.DataSource = ls;
            ddl.DataBind();
        };
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
        private void GetTitle()
        {
            var message = SysMessage.DefaultSelecting;

            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetTitleName(message);
            BindToDDL(ddlTitle, ls);

        }

        private void NewRegister() {


            if (MasterPage.AgreementStatus)
            {
                DTO.Registration general =  new DTO.Registration();
     
           

                //var attachFiles = this.AttachFiles;
                //var personAttachFiles = this.PersonAttachFiles;
                general.ID = IAS.BLL.Helpers.GenerateIdHelper.GetGenAutoId();
                general.ID_CARD_NO = txtIDNumber.Text;
                general.PRE_NAME_CODE = ddlTitle.SelectedValue;
                general.NAMES = txtFirstName.Text;
                general.LASTNAME = txtLastName.Text;
                general.ID_CARD_NO = txtIDNumber.Text;
                general.BIRTH_DATE = Convert.ToDateTime(txtBirthDay.Text);
                general.SEX = rblSex.SelectedValue;
                general.NATIONALITY = ddlNationality.SelectedValue;
                general.EDUCATION_CODE = ddlEducation.SelectedValue;
                general.EMAIL = txtEmail.Text;
                general.LOCAL_TELEPHONE = txtTel.Text;
                general.TELEPHONE = txtMobilePhone.Text;
                general.ADDRESS_1 = ucAddressCurrent.Address; // txtCurrentAddress.Text;
                general.PROVINCE_CODE = ucAddressCurrent.ProvinceValue; // ddlProvinceCurrentAddress.SelectedValue;
                general.AREA_CODE = ucAddressCurrent.DistrictValue; // ddlDistrictCurrentAddress.SelectedValue;
                general.TUMBON_CODE = ucAddressCurrent.ParishValue; // ddlParishCurrentAddress.SelectedValue;
                general.ZIP_CODE = ucAddressCurrent.PostCode; // txtPostcodeCurrentAddress.Text;
                general.LOCAL_ADDRESS1 = ucAddressRegister.Address; // txtRegisterAddress.Text;
                general.LOCAL_PROVINCE_CODE = ucAddressRegister.ProvinceValue; // ddlProvinceRegisterAddress.SelectedValue;
                general.LOCAL_AREA_CODE = ucAddressRegister.DistrictValue; // ddlDistrictRegisterAddress.SelectedValue;
                general.LOCAL_TUMBON_CODE = ucAddressRegister.ParishValue;// ddlParishRegisterAddress.SelectedValue;
                general.LOCAL_ZIPCODE = ucAddressRegister.PostCode; // txtPostcodeRegisterAddress.Text;
                general.CREATED_BY = "agdoi";
                general.CREATED_DATE = DateTime.Now;
                general.UPDATED_BY = "agdoi";
                general.UPDATED_DATE = DateTime.Now;
                general.REG_PASS = MasterPage.Password;

                if(MasterPage.Password!=MasterPage.ConfirmPassword){
                    MasterPage.ModelError.ShowMessageError = SysMessage.NotSame;
                    MasterPage.ModelError.ShowModalError();
                    return;
                }
                

                BLL.RegistrationBiz biz = new BLL.RegistrationBiz();
                var result = biz.ValidateBeforeSubmit(DTO.RegistrationType.General, general);

            //    else
            //    {
            //        //AlertMessage.ShowAlertMessage(string.Empty, SysMessage.NotSame);

                    

                    
            //    }


            //    if (item != null)
            //    {
                    

                    

            //        if (result.IsError)
            //        {
            //            UCModalError.ShowMessageError = result.ErrorMsg;
            //            UCModalError.ShowModalError();
            //        }
            //        else
            //        {
            //            if (this.DataAction == DTO.DataActionMode.Add)
            //            {
            //                try
            //                {
            //                    List<DTO.RegistrationAttatchFile> attachFileList = ucAttachFiles.AttachFiles.ConvertToRegistrationAttachFiles().ToList();
            //                    attachFileList.ForEach(a => a.FILE_STATUS = BLL.AttachFilesIAS.States.AttachFileStatus.Active.Value());

            //                    var res = biz.InsertWithAttatchFile(DTO.RegistrationType.General, item, attachFileList);

            //                    if (res.IsError)
            //                    {
            //                        UCModalError.ShowMessageError = res.ErrorMsg;
            //                        UCModalError.ShowModalError();

            //                        PnlCodition.Visible = false;
            //                        PnlAddButton.Visible = true;
            //                    }
            //                    else
            //                    {

            //                        Session.Remove("TempFolderOracle");
            //                        Session.Remove("AttatchFiles");

            //                        Session.Abandon();

            //                        ClearControl();

            //                        UCModalError.Visible = false;
            //                        UCModalSuccess.Visible = false;
            //                        BLL.DataCenterBiz dbiz = new BLL.DataCenterBiz();
            //                        var r = dbiz.GetConfigApproveMember();

            //                        foreach (var items in r.DataResponse)
            //                        {
            //                            if (items.Id == "01" && items.Value == "Y")
            //                            {
            //                                string AlertWaitingForApprove = "alert('ทำการบันทึกข้อมูลเรียบร้อย รอการอนุมัติการสมัครภายหลัง');window.location.assign('../home.aspx')";
            //                                ToolkitScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", AlertWaitingForApprove, true);
            //                            }
            //                        }
            //                        string Alert = "alert('ทำการบันทึกข้อมูลเรียบร้อย');window.location.assign('../home.aspx')";
            //                        ToolkitScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", Alert, true);

            //                    }

            //                }
            //                catch (Exception ex)
            //                {
            //                    UCModalError.ShowMessageError = ex.Message;
            //                    UCModalError.ShowModalError();
            //                }

            //            }

            //        }

            //    }
            //    else
            //    {
            //        UCModalError.ShowMessageError = SysMessage.TryAgain;
            //        UCModalError.ShowModalError();
            //    }
            //}
            //else
            //{
            //    UCModalError.ShowMessageError = SysMessage.CheckCondition;
            //    UCModalError.ShowModalError();
            }
        }


        private void ThrowRegistrationIssie(DTO.Registration registration) 
        { 
        
        }
   
    }
}