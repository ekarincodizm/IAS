using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.MasterPage;
using IAS.BLL;
using AjaxControlToolkit;
using IAS.Properties;

namespace IAS.UserControl
{
    public partial class ucLicenseDetail : System.Web.UI.UserControl
    {
        #region Public & Private Session
        public MasterLicense MasterLicense
        {
            get
            {
                return (this.Page.Master as MasterLicense);
            }

        }

        public string IDCardNo
        {

            get { return Session["idCardNo"] == null ? "" : Session["idCardNo"].ToString(); }
            set { Session["idCardNo"] = value; }
        }

        public AjaxControlToolkit.ModalPopupExtender PopDetail { get { return PopupDetail; } }

        public TextBox TxtName { get { return txtFirstNameLastName; } }
        public TextBox TxtIDCard { get { return txtIdCard; } }
        public UpdatePanel UpdateTab { get { return udpFirstTab; } }
        public TabContainer TabLicenseDetail { get { return TabDetail; } }
        
        #endregion

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ClearGridView();
            }
            else
            {
                if (!(ViewState["ActiveTabIndex"] == null) && (!(sender == null)))
                {
                    ClearGridView();
                    if (!sender.GetType().ToString().Equals("AjaxControlToolkit.TabContainer"))
                    {
                        //((AjaxControlToolkit.TabContainer)sender).ActiveTabIndex = 0;
                        TabDetail.ActiveTab = TabProfile;
                        TabProfile.Focus();
                    }
                }
            }
        }
        #endregion

        #region Public & Private Fucntion
        public void FirstTabDataBind(string IDCard)
        {
            LicenseBiz biz = new LicenseBiz();
            var res = biz.GetPersonalHistoryByIdCard(IDCard);
           // if (res.DataResponse==null)
           // {
              //  ucmodalerror.showmessageerror = res.errormsg;
              //  ucmodalerror.showmodalerror();
            //}  


            if(res.DataResponse!=null)
            {
                DTO.PersonalHistory person = res.DataResponse;
              if (res.DataResponse != null)
              {
                if (!string.IsNullOrEmpty(person.FLNAME))
                    {
                    txtFirstNameLastName.Text = person.FLNAME;
                    }
                    if (!string.IsNullOrEmpty(person.ID_CARD_NO))
                    {
                        txtIdCard.Text = person.ID_CARD_NO;
                    }
                    if (!string.IsNullOrEmpty(person.SEX))
                    {
                        if (person.SEX != "M")
                        {
                            txtSex.Text = Resources.propLicenseGroup_Female;
                        }
                        else
                        {
                            txtSex.Text = Resources.propLicenseGroup_Male;
                        }
                    }
                    if (!string.IsNullOrEmpty(person.NATIONALITY))
                    {
                        txtNationality.Text = person.NATIONALITY;
                    }
                    if (!string.IsNullOrEmpty(person.EDUCATION_NAME))
                    {
                        txtEducation.Text = person.EDUCATION_NAME;
                    }
                    if (person.BIRTH_DATE.ToString() != null || person.BIRTH_DATE.ToString() != "")
                    {
                        txtBirthDay.Text = person.BIRTH_DATE.ToString("dd/MM/yyyy");
                    }
                    if (!string.IsNullOrEmpty(person.ADDRESS1))
                    {
                        txtCurrentAddress.Text = person.ADDRESS1;
                    }
                    if (!string.IsNullOrEmpty(person.PROVINCE))
                    {
                        txtProvinceCurrentAddress.Text = person.PROVINCE;
                    }
                    if (!string.IsNullOrEmpty(person.AMPUR))
                    {
                        txtDistrictCurrentAddress.Text = person.AMPUR;
                    }
                    if (!string.IsNullOrEmpty(person.TAMBON))
                    {
                        txtParishCurrentAddress.Text = person.TAMBON;
                    }
                    if (!string.IsNullOrEmpty(person.ZIPCODE))
                    {
                        txtPostcodeCurrentAddress.Text = person.ZIPCODE;
                    }
                    if (!string.IsNullOrEmpty(person.LOCAL_ADDRESS1))
                    {
                        txtRegisterAddress.Text = person.LOCAL_ADDRESS1;
                    }
                    if (!string.IsNullOrEmpty(person.LOCAL_PROVINCE))
                    {
                        txtProvinceRegisterAddress.Text = person.LOCAL_PROVINCE;
                    }
                    if (!string.IsNullOrEmpty(person.LOCAL_AMPUR))
                    {
                        txtDistrictRegisterAddress.Text = person.LOCAL_AMPUR;
                    }
                    if (!string.IsNullOrEmpty(person.LOCAL_TAMBON))
                    {
                        txtParishRegisterAddress.Text = person.LOCAL_TAMBON;
                    }
                    if (!string.IsNullOrEmpty(person.LOCAL_ZIPCODE))
                    {
                        txtPostcodeRegisterAddress.Text = person.LOCAL_ZIPCODE;
                    }
                    EnableControl();
                    ClearGridView();
                }
            }
                else
                {
                    this.ucLicDetailModelWarning.ShowMessageWarning = Resources.errorucLicenseDetail_001 + IDCard;
                    this.ucLicDetailModelWarning.ShowModalWarning();
                    this.udpLicenseDetail.Update();
                    //return;
                    PopupDetail.Hide();
                }
            

        }

        private void ClearGridView()
        {
            gvHistoryTraining.DataSource = null;
            gvHistoryTraining.DataBind();
            gvHistoryTraining.Visible = false;

            gvHistoryExam.DataSource = null;
            gvHistoryExam.DataBind();
            gvHistoryExam.Visible = false;

            gvObtainLicense.DataSource = null;
            gvObtainLicense.DataBind();
            gvObtainLicense.Visible = false;
           
        }

        public void InitTab(object sender, EventArgs e)
        {
            var biz = new BLL.LicenseBiz();
            var gr = (GridViewRow)((LinkButton)sender).NamingContainer;
            var idCardNo = (Label)gr.FindControl("lblIDNumberGv");
            this.IDCardNo = idCardNo.Text;
            var IDCard = this.IDCardNo;

            if (TabDetail.ActiveTabIndex == 0)
            {
                var res = biz.GetPersonalHistoryByIdCard(IDCard);
              //  if (res!=null)
               // {
                    //UCModalError.ShowMessageError = res.ErrorMsg;
                    //UCModalError.ShowModalError();
              //  }
                if (res.DataResponse != null)
                {
                    DTO.PersonalHistory person = res.DataResponse;
                    if (res.DataResponse != null)
                    {
                        if (!string.IsNullOrEmpty(person.FLNAME))
                        {
                            txtFirstNameLastName.Text = person.FLNAME;
                        }
                        if (!string.IsNullOrEmpty(person.ID_CARD_NO))
                        {
                            txtIdCard.Text = person.ID_CARD_NO;
                        }
                        if (!string.IsNullOrEmpty(person.SEX))
                        {
                            if (person.SEX != "M")
                            {
                                txtSex.Text = Resources.propLicenseGroup_Female;
                            }
                            else
                            {
                                txtSex.Text = Resources.propLicenseGroup_Male;
                            }
                        }
                        if (!string.IsNullOrEmpty(person.NATIONALITY))
                        {
                            txtNationality.Text = person.NATIONALITY;
                        }
                        if (!string.IsNullOrEmpty(person.EDUCATION_NAME))
                        {
                            txtEducation.Text = person.EDUCATION_NAME;
                        }
                        if (person.BIRTH_DATE.ToString() != null || person.BIRTH_DATE.ToString() != "")
                        {
                            txtBirthDay.Text = person.BIRTH_DATE.ToString("dd/MM/yyyy");
                        }
                        if (!string.IsNullOrEmpty(person.ADDRESS1))
                        {
                            txtCurrentAddress.Text = person.ADDRESS1;
                        }
                        if (!string.IsNullOrEmpty(person.PROVINCE))
                        {
                            txtProvinceCurrentAddress.Text = person.PROVINCE;
                        }
                        if (!string.IsNullOrEmpty(person.AMPUR))
                        {
                            txtDistrictCurrentAddress.Text = person.AMPUR;
                        }
                        if (!string.IsNullOrEmpty(person.TAMBON))
                        {
                            txtParishCurrentAddress.Text = person.TAMBON;
                        }
                        if (!string.IsNullOrEmpty(person.ZIPCODE))
                        {
                            txtPostcodeCurrentAddress.Text = person.ZIPCODE;
                        }
                        if (!string.IsNullOrEmpty(person.LOCAL_ADDRESS1))
                        {
                            txtRegisterAddress.Text = person.LOCAL_ADDRESS1;
                        }
                        if (!string.IsNullOrEmpty(person.LOCAL_PROVINCE))
                        {
                            txtProvinceRegisterAddress.Text = person.LOCAL_PROVINCE;
                        }
                        if (!string.IsNullOrEmpty(person.LOCAL_AMPUR))
                        {
                            txtDistrictRegisterAddress.Text = person.LOCAL_AMPUR;
                        }
                        if (!string.IsNullOrEmpty(person.LOCAL_TAMBON))
                        {
                            txtParishRegisterAddress.Text = person.LOCAL_TAMBON;
                        }
                        if (!string.IsNullOrEmpty(person.LOCAL_ZIPCODE))
                        {
                            txtPostcodeRegisterAddress.Text = person.LOCAL_ZIPCODE;
                        }
                        EnableControl();
                        PopupDetail.Show();
                    }
                    //else
                    //{
                    //    PopupDetail.Hide();
                    //    this.ucLicDetailModelError.ShowMessageError = "ไม่พบข้อมูลของเลขบัตรประชาชน " + IDCard;
                    //    this.ucLicDetailModelError.ShowModalError();
                    //    this.udpLicenseDetail.Update();
                    //    return;
                    //}
                }
            }
            if (TabDetail.ActiveTabIndex == 1)
            {
                var resExam = biz.GetExamHistoryByIdCard(IDCard);
                if (resExam.IsError)
                {
                    //UCModalError.ShowMessageError = resExam.ErrorMsg;
                    //UCModalError.ShowModalError();
                }
                else
                {
                    gvHistoryExam.DataSource = resExam.DataResponse;
                    gvHistoryExam.DataBind();
                    gvHistoryExam.Visible = true;
                    //UpdatePanelSearch.Update();
                    PopupDetail.Show();
                }
            }
            if (TabDetail.ActiveTabIndex == 2)
            {
                var resTraining = biz.GetTrainingHistoryBy(IDCard);
                if (resTraining.IsError)
                {
                    //UCModalError.ShowMessageError = resTraining.ErrorMsg;
                    //UCModalError.ShowModalError();
                }
                else
                {
                    gvHistoryTraining.DataSource = resTraining.DataResponse;
                    gvHistoryTraining.DataBind();
                    gvHistoryTraining.Visible = true;
                    //UpdatePanelSearch.Update();
                    PopupDetail.Show();
                }
            }
            //if (TabDetail.ActiveTabIndex == 3)
            //{
            //    var resTrain = biz.GetTrain_1_To_4_ByIdCard(IDCard);

            //    if (resTrain.IsError)
            //    {
            //        //UCModalError.ShowMessageError = resTrain.ErrorMsg;
            //        //UCModalError.ShowModalError();
            //    }
            //    else
            //    {
            //        GvTraining.DataSource = resTrain.DataResponse;
            //        GvTraining.DataBind();

            //        //UpdatePanelSearch.Update();
            //        PopupDetail.Show();
            //    }
            //}
            //if (TabDetail.ActiveTabIndex == 4)
            //{
            //    var resUnit = biz.GetUnitLinkByIdCard(IDCard);

            //    if (resUnit.IsError)
            //    {
            //        //UCModalError.ShowMessageError = resUnit.ErrorMsg;
            //        //UCModalError.ShowModalError();
            //    }
            //    else
            //    {
            //        gvUnitLink.DataSource = resUnit.DataResponse;
            //        gvUnitLink.DataBind();

            //        //UpdatePanelSearch.Update();
            //        PopupDetail.Show();
            //    }
            //}
            if (TabDetail.ActiveTabIndex == 3)
            {
                var resRequest = biz.GetObtainLicenseByIdCard(IDCard);
                if (resRequest.IsError)
                {
                    //UCModalError.ShowMessageError = resRequest.ErrorMsg;
                    //UCModalError.ShowModalError();
                }
                else
                {
                    gvObtainLicense.DataSource = resRequest.DataResponse;
                    gvObtainLicense.DataBind();
                    gvObtainLicense.Visible = true;

                    //UpdatePanelSearch.Update();
                    PopupDetail.Show();
                }
            }
        }

        private void EnableControl()
        {
            txtFirstNameLastName.Enabled = false;
            txtIdCard.Enabled = false;
            txtSex.Enabled = false;
            txtNationality.Enabled = false;
            txtEducation.Enabled = false;
            txtBirthDay.Enabled = false;
            txtCurrentAddress.Enabled = false;
            txtProvinceCurrentAddress.Enabled = false;
            txtDistrictCurrentAddress.Enabled = false;
            txtParishCurrentAddress.Enabled = false;
            txtPostcodeCurrentAddress.Enabled = false;
            txtRegisterAddress.Enabled = false;
            txtProvinceRegisterAddress.Enabled = false;
            txtDistrictRegisterAddress.Enabled = false;
            txtParishRegisterAddress.Enabled = false;
            txtPostcodeRegisterAddress.Enabled = false;


        }

        private void EnableTab()
        {
            TabProfile.Enabled = true;
            TabHistoryExam.Enabled = true;
            TabHistoryTraining.Enabled = true;
           // TabTraining.Enabled = true;
            //TabUnitLink.Enabled = true;
            TabObtainLicense.Enabled = true;


            TabDetail.Visible = true;
            TabProfile.Visible = true;
            TabHistoryExam.Visible = true;
            TabHistoryTraining.Visible = true;
            //TabTraining.Visible = true;
           // TabUnitLink.Visible = true;
            TabObtainLicense.Visible = true;


        }

        #endregion

        #region UI Function
        protected void TabDetail_ActiveTabChanged(object sender, EventArgs e)
        {
            LicenseBiz biz = new LicenseBiz();

            ViewState["ActiveTabIndex"] = TabDetail.ActiveTabIndex;

            if (Session["idCardNo"] != null)
            {
                var IDCard = Session["idCardNo"].ToString();

                if (TabDetail.ActiveTabIndex == 0)
                {
                    var res = biz.GetPersonalHistoryByIdCard(IDCard);
                    if (res.IsError)
                    {
                        //UCModalError.ShowMessageError = res.ErrorMsg;
                        //UCModalError.ShowModalError();
                    }
                    else
                    {
                        var person = res.DataResponse;
                        if (res.DataResponse != null)
                        {
                            if (!string.IsNullOrEmpty(person.FLNAME))
                            {
                                txtFirstNameLastName.Text = person.FLNAME;
                            }
                            if (!string.IsNullOrEmpty(person.ID_CARD_NO))
                            {
                                txtIdCard.Text = person.ID_CARD_NO;
                            }
                            if (!string.IsNullOrEmpty(person.SEX))
                            {
                                if (person.SEX != "M")
                                {
                                    txtSex.Text = Resources.propLicenseGroup_Female;
                                }
                                else
                                {
                                    txtSex.Text = Resources.propLicenseGroup_Male;
                                }
                            }
                            if (!string.IsNullOrEmpty(person.NATIONALITY))
                            {
                                txtNationality.Text = person.NATIONALITY;
                            }
                            if (!string.IsNullOrEmpty(person.EDUCATION_NAME))
                            {
                                txtEducation.Text = person.EDUCATION_NAME;
                            }
                            if (person.BIRTH_DATE.ToString() != null || person.BIRTH_DATE.ToString() != "")
                            {
                                txtBirthDay.Text = person.BIRTH_DATE.ToString("dd/MM/yyyy");
                            }
                            if (!string.IsNullOrEmpty(person.ADDRESS1))
                            {
                                txtCurrentAddress.Text = person.ADDRESS1;
                            }
                            if (!string.IsNullOrEmpty(person.PROVINCE))
                            {
                                txtProvinceCurrentAddress.Text = person.PROVINCE;
                            }
                            if (!string.IsNullOrEmpty(person.AMPUR))
                            {
                                txtDistrictCurrentAddress.Text = person.AMPUR;
                            }
                            if (!string.IsNullOrEmpty(person.TAMBON))
                            {
                                txtParishCurrentAddress.Text = person.TAMBON;
                            }
                            if (!string.IsNullOrEmpty(person.ZIPCODE))
                            {
                                txtPostcodeCurrentAddress.Text = person.ZIPCODE;
                            }
                            if (!string.IsNullOrEmpty(person.LOCAL_ADDRESS1))
                            {
                                txtRegisterAddress.Text = person.LOCAL_ADDRESS1;
                            }
                            if (!string.IsNullOrEmpty(person.LOCAL_PROVINCE))
                            {
                                txtProvinceRegisterAddress.Text = person.LOCAL_PROVINCE;
                            }
                            if (!string.IsNullOrEmpty(person.LOCAL_AMPUR))
                            {
                                txtDistrictRegisterAddress.Text = person.LOCAL_AMPUR;
                            }
                            if (!string.IsNullOrEmpty(person.LOCAL_TAMBON))
                            {
                                txtParishRegisterAddress.Text = person.LOCAL_TAMBON;
                            }
                            if (!string.IsNullOrEmpty(person.LOCAL_ZIPCODE))
                            {
                                txtPostcodeRegisterAddress.Text = person.LOCAL_ZIPCODE;
                            }
                            EnableControl();
                            // UpdatePanelSearch.Update();
                            PopupDetail.Show();
                        }
                    }
                }
                else if (TabDetail.ActiveTabIndex == 1)
                {
                    var res = biz.GetExamHistoryByIdCard(IDCard);
                    if (res.IsError)
                    {
                        //UCModalError.ShowMessageError = res.ErrorMsg;
                        //UCModalError.ShowModalError();
                    }
                    else
                    {
                        gvHistoryExam.DataSource = res.DataResponse;
                        gvHistoryExam.DataBind();
                        gvHistoryExam.Visible = true;

                        // UpdatePanelSearch.Update();
                        PopupDetail.Show();
                    }
                }
                else if (TabDetail.ActiveTabIndex == 2)
                {
                    //var res = biz.GetTrainingHistoryBy(IDCard);
                    var res = biz.GetTrainingHistoryBy(IDCard);
                    if (res.IsError)
                    {
                        //UCModalError.ShowMessageError = res.ErrorMsg;
                        //UCModalError.ShowModalError();
                    }
                    else
                    {
                        gvHistoryTraining.DataSource = res.DataResponse;
                        gvHistoryTraining.DataBind();
                        gvHistoryTraining.Visible = true;

                        //UpdatePanelSearch.Update();
                        PopupDetail.Show();
                    }
                }
                //else if (TabDetail.ActiveTabIndex == 3)
                //{
                //    var res = biz.GetTrain_1_To_4_ByIdCard(IDCard);

                //    if (res.IsError)
                //    {
                //        //UCModalError.ShowMessageError = res.ErrorMsg;
                //        //UCModalError.ShowModalError();
                //    }
                //    else
                //    {
                //        GvTraining.DataSource = res.DataResponse;
                //        GvTraining.DataBind();

                //        // UpdatePanelSearch.Update();
                //        PopupDetail.Show();
                //    }
                //}
                //else if (TabDetail.ActiveTabIndex == 4)
                //{
                //    var res = biz.GetUnitLinkByIdCard(IDCard);

                //    if (res.IsError)
                //    {
                //        //UCModalError.ShowMessageError = res.ErrorMsg;
                //        //UCModalError.ShowModalError();
                //    }
                //    else
                //    {
                //        gvUnitLink.DataSource = res.DataResponse;
                //        gvUnitLink.DataBind();

                //        //UpdatePanelSearch.Update();
                //        PopupDetail.Show();
                //    }
                //}
                else if (TabDetail.ActiveTabIndex == 3)
                {
                    //var res = biz.GetRenewLicneseByIdCard(IDCard);
                    var res = biz.GetObtainLicenseByIdCard(IDCard);
                    if (res.IsError)
                    {
                        //UCModalError.ShowMessageError = res.ErrorMsg;
                        //UCModalError.ShowModalError();
                    }
                    else
                    {
                        gvObtainLicense.DataSource = res.DataResponse;
                        gvObtainLicense.DataBind();
                        gvObtainLicense.Visible = true;

                        //UpdatePanelSearch.Update();
                        PopupDetail.Show();
                    }
                }
                PopupDetail.Show();
                //UplPopUp.Update();
            }
            else
            {

            }
            
        }
        #endregion

        protected void Close_PopUp_Click(object sender, EventArgs e)
        {
            this.PopupDetail.Hide();
            this.udpLicenseDetail.Update();
        }
    }
}
