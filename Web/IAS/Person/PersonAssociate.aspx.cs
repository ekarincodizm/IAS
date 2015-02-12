using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using IAS.Utils;
using System.IO;
using IAS.DTO;
using System.Text;
using System.Web.Configuration;
using AjaxControlToolkit;
using IAS.BLL;
using IAS.BLL.AttachFilesIAS;
using IAS.BLL.AttachFilesIAS.States;
using IAS.MasterPage;
using IAS.Properties;

namespace IAS.Person
{
    public partial class PersonAssociate : basepage
    {

        #region Public Param & Session
        ////ยังไม่ทำการอนุมัติ(แก้ไข)
        private string waitApprove = Resources.propMasterPerson_001;
        ////ไม่อนุมัติ(แก้ไข)
        private string notApprove = Resources.propPersonAssociate_notApprove;

        public string reqReg = Resources.propReg_NotApprove_reqRegAssoc;

        protected DTO.MemberType _memberType = MemberType.Association;
        protected String _memberTypeName = Resources.propReg_Assoc_MemberTypeAssoc;

        public MasterPerson MasterPage
        {
            get { return (this.Page.Master as MasterPerson); }
        }

        public DTO.MemberType MemberType
        {
            get { return _memberType; }
        }

        public String MemberTypeName
        {
            get { return _memberTypeName; }
        }

        public string Status
        {
            get
            {
                return (Session["Status"] == null) ? "" : Session["Status"].ToString();
            }
            set
            {
                Session["Status"] = value;
            }
        }

        #endregion

        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {

            txtEmail.Attributes.Add("onblur", "javascript:return checkemail(" + txtEmail.ClientID + ");");
            txtIDCard.Attributes.Add("onblur", "javascript:return checkUser();");

            txtIDCard.Attributes.Add("onkeydown", "javascript:return checkKeyNumberFixPoint(event, this, 13);");
            txtIDCard.Attributes.Add("onkeypress", "javascript:return checkKeyNumberKeyPressFixPoint(event, this, 13);");

            txtTel.Attributes.Add("onkeydown", "javascript:return checkKeyNumberFixPoint(event, this, 15);");
            txtTel.Attributes.Add("onkeypress", "javascript:return checkKeyNumberKeyPressFixPoint(event, this, 15);");

            UcAddress.TextBoxPostCode.Attributes.Add("onkeydown", "javascript:return checkKeyNumberFixPoint(event, this, 5);");
            UcAddress.TextBoxPostCode.Attributes.Add("onkeypress", "javascript:return checkKeyNumberKeyPressFixPoint(event, this, 5);");

            txtAssociationTel.Attributes.Add("onkeydown", "javascript:return checkKeyNumberFixPoint(event, this, 15);");
            txtAssociationTel.Attributes.Add("onkeypress", "javascript:return checkKeyNumberKeyPressFixPoint(event, this, 15);");

            if (!IsPostBack)
            {
                base.HasPermit();
                //Set Group Validation
                this.MasterPage.SetValidateGroup(this.reqReg);
                Init();
            }
        }
        #endregion

        #region Main Function
        protected virtual void Init()
        {

            MasterPage.GetTitle(ddlTitle);
            MasterPage.Init();
            this.MasterPage.SetValidateGroup(this.reqReg);
            this.UcAddress.SetValidateGroup(this.reqReg);

            txtMemberType.Text = MemberTypeName;
            txtMemberType.Enabled = false;

            switch (MasterPage.ActionMode)
            {

                case IAS.DTO.DataActionMode.Edit:
                    lblRegister.Text = Resources.propPersonAssociate_001;
                    InitOnEditMode();
                    break;
                case IAS.DTO.DataActionMode.View:
                    lblRegister.Text = Resources.propPersonAssociate_002;
                    InitOnViewMode();
                    break;
                default:
                    InitOnViewMode();
                    break;
            }
        }


        protected void InitOnEditMode()
        {
            GetTempPerson();
            //GetLastPersonal();
            EnabledControl(true);
            StatusInit();
        }

        protected void InitOnViewMode()
        {
            GetLastPerson();
            EnabledControl(false);
            StatusInit();
        }

        //View Mode
        protected void GetLastPerson()
        {
            DTO.Person res = MasterPage.GetPersonal();
            if (res != null)
            {
                GetLoadDataToControl(res);
            }
        }

        //Edit Mode
        protected void GetTempPerson()
        {
            DTO.PersonTemp res = MasterPage.GetPersonTemp();
            if (res != null)
            {
                GetLoadTempDataToControl(res);
            }
        }

        private void StatusInit()
        {
            PersonBiz biz = new PersonBiz();

            if (this.MasterPage.UserProfile.Id != null)
            {
                DTO.ResponseService<DTO.Person> res = biz.GetById(this.MasterPage.UserProfile.Id);

                if (res.DataResponse != null)
                {
                    if (res.DataResponse.STATUS == Convert.ToString((int)DTO.PersonDataStatus.WaitForApprove))
                    {
                        lblMessage.Text = this.waitApprove;
                        lblMessage.Visible = true;
                    }
                    else if (res.DataResponse.STATUS == Convert.ToString((int)DTO.PersonDataStatus.NotApprove))
                    {
                        lblMessage.Text = this.notApprove;
                        lblMessage.Visible = true;

                    }
                }


            }

        }

        public void EditPersonal()
        {

            MasterPage.EditAssociate(MemberType, GetInsuranceFromDataControl());

            //Added new by Nattapong 11/10/2556
            //InitOnEditMode();
        }

        protected DTO.PersonTemp GetInsuranceFromDataControl()
        {
            DTO.PersonTemp insurance = new DTO.PersonTemp();

            string strFullAssoName = txtAssociation.Text;
            string[] strFullAssoNameArray = strFullAssoName.Split('[', ']');
            
            insurance.ID = MasterPage.PersonId;
            insurance.MEMBER_TYPE = ((int)MemberType).ToString();
            //insurance.Association_Name = dc.Name;
            insurance.COMP_CODE = strFullAssoNameArray[1].ToString();


            insurance.ID_CARD_NO = txtIDCard.Text;
            insurance.PRE_NAME_CODE = ddlTitle.SelectedValue;
            insurance.NAMES = txtFirstName.Text;
            insurance.LASTNAME = txtLastName.Text;
            insurance.SEX = rblSex.SelectedValue;
            insurance.EMAIL = txtEmail.Text;
            insurance.LOCAL_TELEPHONE = txtAssociationTel.Text + ((String.IsNullOrWhiteSpace(txtAssociationTelExt.Text)) ? "" : ("#" + txtAssociationTelExt.Text.Trim()));// txtAssociationTel.Text;
            insurance.TELEPHONE =  txtTel.Text + ((String.IsNullOrWhiteSpace(txtTelExt.Text))?"": ("#"+txtTelExt.Text.Trim()));
            insurance.ADDRESS_1 = UcAddress.Address;
            insurance.PROVINCE_CODE = UcAddress.ProvinceValue;
            insurance.AREA_CODE = UcAddress.DistrictValue;
            insurance.TUMBON_CODE = UcAddress.ParishValue;
            insurance.ZIP_CODE = UcAddress.TextBoxPostCode.Text;
            insurance.CREATED_BY = ( MasterPage.UserProfile!=null)? MasterPage.UserProfile.LoginName:"AGDOI";
            //insurance.CREATED_DATE = DateTime.Now;
            insurance.CREATED_DATE = MasterPage.CreateDateTemp;
            insurance.UPDATED_BY = (MasterPage.UserProfile != null) ? MasterPage.UserProfile.LoginName : "AGDOI";
            insurance.UPDATED_DATE = DateTime.Now;
            insurance.STATUS = ((int)DTO.PersonDataStatus.WaitForApprove).ToString();
            return insurance;
        }

        //View Mode
        protected void GetLoadDataToControl(DTO.Person insurance)
        {
            BLL.DataCenterBiz dcbiz = new BLL.DataCenterBiz();

            MasterPage.PersonId = insurance.ID;
            if (!insurance.MEMBER_TYPE.Equals(DTO.MemberType.Association.GetEnumValue().ToString()))
            {
                MasterPage.ModelError.ShowMessageError = SysMessage.UserMissMatchRegitrationData;
                MasterPage.ModelError.ShowModalError();
            }

            txtAssociationRegister.Text = insurance.COMP_CODE;
            ddlTitle.SelectedValue = insurance.PRE_NAME_CODE;
            txtFirstName.Text = insurance.NAMES;
            txtLastName.Text = insurance.LASTNAME;
            txtIDCard.Text = insurance.ID_CARD_NO;
            txtIDCard.Enabled = false;
            rblSex.SelectedValue = insurance.SEX;
            rblSex.Enabled = false;
            txtAssociationTel.Text = LocalTelephoneNumberHelper.GetPhoneNumber(insurance.LOCAL_TELEPHONE);
            txtAssociationTelExt.Text = LocalTelephoneNumberHelper.GetExtenNumber(insurance.LOCAL_TELEPHONE);
            txtTel.Text = LocalTelephoneNumberHelper.GetPhoneNumber(insurance.TELEPHONE);
            txtTelExt.Text = LocalTelephoneNumberHelper.GetExtenNumber(insurance.TELEPHONE);

            txtEmail.Text = insurance.EMAIL;
            txtEmail.Enabled = false;
            UcAddress.TextBoxAddress.Text = insurance.ADDRESS_1;
            UcAddress.SelectDropDownStep(insurance.PROVINCE_CODE, insurance.AREA_CODE, insurance.TUMBON_CODE);
            UcAddress.TextBoxPostCode.Text = insurance.ZIP_CODE;

            txtAssociation.Text = new BLL.DataCenterBiz().GetInsuranceAssociateNameByID(insurance.COMP_CODE).DataResponse.ASSOCIATION_NAME + " " + "[" + insurance.COMP_CODE + "]";
            UcAddress.DropdownParish.SelectedValue = insurance.TUMBON_CODE;
            
            if (insurance.STATUS != null)
            {
                Session["Status"] = insurance.STATUS.ToString(); //Set status after approve
            }
            if (insurance.STATUS != Convert.ToString((int)DTO.PersonDataStatus.WaitForApprove))
            {
                MasterPage.ResultRegister = insurance.APPROVE_RESULT;
            }

        }

        //Edit Mode
        protected void GetLoadTempDataToControl(DTO.PersonTemp insurance)
        {
            BLL.DataCenterBiz dcbiz = new BLL.DataCenterBiz();

            MasterPage.PersonId = insurance.ID;
            if (!insurance.MEMBER_TYPE.Equals(DTO.MemberType.Association.GetEnumValue().ToString()))
            {
                MasterPage.ModelError.ShowMessageError = SysMessage.UserMissMatchRegitrationData;
                MasterPage.ModelError.ShowModalError();
            }

            txtAssociationRegister.Text = insurance.COMP_CODE;
            ddlTitle.SelectedValue = insurance.PRE_NAME_CODE;
            txtFirstName.Text = insurance.NAMES;
            txtLastName.Text = insurance.LASTNAME;
            txtIDCard.Text = insurance.ID_CARD_NO;
            txtIDCard.Enabled = false;
            rblSex.SelectedValue = insurance.SEX;
            rblSex.Enabled = false;
            txtAssociationTel.Text = LocalTelephoneNumberHelper.GetPhoneNumber(insurance.LOCAL_TELEPHONE);
            txtAssociationTelExt.Text = LocalTelephoneNumberHelper.GetExtenNumber(insurance.LOCAL_TELEPHONE);
            txtTel.Text = LocalTelephoneNumberHelper.GetPhoneNumber(insurance.TELEPHONE);
            txtTelExt.Text = LocalTelephoneNumberHelper.GetExtenNumber(insurance.TELEPHONE);

            txtEmail.Text = insurance.EMAIL;
            txtEmail.Enabled = false;
            UcAddress.TextBoxAddress.Text = insurance.ADDRESS_1;
            UcAddress.SelectDropDownStep(insurance.PROVINCE_CODE, insurance.AREA_CODE, insurance.TUMBON_CODE);
            UcAddress.TextBoxPostCode.Text = insurance.ZIP_CODE;

            txtAssociation.Text = new BLL.DataCenterBiz().GetInsuranceAssociateNameByID(insurance.COMP_CODE).DataResponse.ASSOCIATION_NAME + " " + "[" + insurance.COMP_CODE + "]";
            UcAddress.DropdownParish.SelectedValue = insurance.TUMBON_CODE;

            if (insurance.STATUS != null)
            {
                Session["Status"] = insurance.STATUS.ToString(); //Set status after approve
            }

        }

        protected void GetAssociation()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var list = biz.GetAssociate("");
            string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(list);
            hdf.Value = jsonString;
        }

        public void ClearControl()
        {
            ddlTitle.SelectedIndex = 0;
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtIDCard.Text = "";
            rblSex.SelectedIndex = 0;
            txtTel.Text = string.Empty;
            txtAssociationTel.Text = string.Empty;
            txtEmail.Text = string.Empty;
            UcAddress.TextBoxAddress.Text = string.Empty;
            UcAddress.DropdownProvince.SelectedIndex = 0;
            UcAddress.DropdownDistrict.SelectedIndex = 0;
            UcAddress.DropdownParish.SelectedIndex = 0;
            UcAddress.TextBoxPostCode.Text = string.Empty;


        }

        protected void EnabledControl(Boolean IsEnable)
        {
            txtAssociation.Enabled = false; //Not Allow Edit
            txtAssociationRegister.Enabled = false; //Not Allow Edit

            ddlTitle.Enabled = IsEnable;
            txtFirstName.Enabled = IsEnable;
            txtLastName.Enabled = IsEnable;
            txtIDCard.Enabled = false; //Not Allow Edit
            rblSex.Enabled = IsEnable;
            txtTel.Enabled = IsEnable;
            txtAssociationTel.Enabled = IsEnable;
            txtEmail.Enabled = false; //Not Allow Edit
            UcAddress.TextBoxAddress.Enabled = IsEnable;
            UcAddress.DropdownProvince.Enabled = IsEnable;
            UcAddress.DropdownDistrict.Enabled = IsEnable;
            UcAddress.DropdownParish.Enabled = IsEnable;
            UcAddress.TextBoxPostCode.Enabled = IsEnable;

            //Get Status after edit data for disable control
            string Mode = Request.QueryString["Mode"];
            if (Mode.Equals("E") && this.Status.Equals(DTO.PersonDataStatus.WaitForApprove.GetEnumValue().ToString()))
            {
                MasterPage.AttachFileControl.PnlAttachFiles.Enabled = false;
                MasterPage.AttachFileControl.EnableUpload(false);
                MasterPage.PNLAddButton.Visible = true;
                MasterPage.PNLAddButton.Enabled = false;
            }
            else if(Mode.Equals("E"))
            {


                MasterPage.AttachFileControl.PnlAttachFiles.Enabled = true;
                MasterPage.AttachFileControl.EnableUpload(true);
                MasterPage.PNLAddButton.Visible = true;
                MasterPage.PNLAddButton.Enabled = true;
            }
            else
            {
               
 
                MasterPage.AttachFileControl.PnlAttachFiles.Enabled = false;
                MasterPage.AttachFileControl.EnableUpload(false);
            }

        }
        #endregion

        #region UI Function
        /// <summary>
        /// Assign Control Event from MasterPerson
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            (this.MasterPage as MasterPerson).OkRegister_Click += new EventHandler(PersonAssociation_OkRegister_Click);
            (this.MasterPage as MasterPerson).CancelRegister_Click += new EventHandler(PersonAssociation_CancelRegister_Click);

        }

        void PersonAssociation_OkRegister_Click(object sender, EventArgs e)
        {
            switch (MasterPage.ActionMode)
            {

                case IAS.DTO.DataActionMode.Edit:
                    EditPersonal();
                    break;
                case IAS.DTO.DataActionMode.View:
                    break;
                default:
                    break;
            }
        }

        void PersonAssociation_CancelRegister_Click(object sender, EventArgs e)
        {
            //Call MasterPage.CancelRegister_Click
        }

        protected void hdf_ValueChanged(object sender, EventArgs e)
        {
            string selectedWidgetID = ((HiddenField)sender).Value;
            string[] compCode = selectedWidgetID.Split('[', ']');

            txtAssociationRegister.Text = compCode[1];

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
                rblSex.Enabled = false;
            }
            else
            {
                //Enable for select Sex
                rblSex.SelectedValue = null;
                rblSex.Enabled = true;
            }
        }

        #endregion

        
    }
}