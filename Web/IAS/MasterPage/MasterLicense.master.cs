using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.BLL;
using IAS.BLL.AttachFilesIAS;
using IAS.DTO;
using IAS.DTO.FileService;
using System.Data;

using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using IAS.Class;
using IAS.License;
using System.IO;
using System.Text;

using IAS.Utils;
using IAS.DTO.FileService;
using IAS.Properties;
using System.Web.UI.HtmlControls;
using IAS.Properties;

namespace IAS.MasterPage
{
    public partial class MasterLicense : System.Web.UI.MasterPage
    {
        #region Public Param & Session
        private string[] LicenseExp = { "01", "02", "03", "04",  "05", "06", "07", "08" };
        //private string[] LicenseExp = { "01", "02", "05", "06", "07", "08" };
        private string[] pageType = { Resources.propMasterLicense_001, Resources.propMasterLicense_002, Resources.propNatta_Pettion004, Resources.propMasterLicense_003, Resources.propMasterLicense_004, "ใบแทนใบอนุญาต(เปลี่ยนชื่อ-สกุล)" };
        private string[] menuType = { "1", "2", "3", "4", "5", "6" };
        private string[] stepType = { "1", "2", "3", "4", "5", "6", "7" };
        string ArgPath = System.Configuration.ConfigurationManager.AppSettings["AgreementFilePath"];
        public string[] ArgOutPutFile = { 
                                            "Agreement_1.pdf", 
                                            "Agreement_2.pdf", 
                                            "Agreement_3.pdf",
                                            "Agreement_4.pdf", 
                                            "Agreement_5.pdf", 
                                            "Agreement_6.pdf", 
                                            "Agreement_7.pdf", 
                                            "Agreement_8.pdf" 
                                        };

        public Label LabelHeadName { get { return lblHeadName; } }
        private String _personId;

        public IAS.UserControl.UCModalSuccess UCLicenseUCLicenseModelSuccess { get { return ucLicModelSuccess; } }

        public IAS.UserControl.UCModalError UCLicenseUCLicenseModelError { get { return ucLicModelError; } }

        public IAS.UserControl.ucAttachFileControl ucAttachFileControl;

        public IAS.UserControl.ucRenewLicense ucRenewLicense;

        public UpdatePanel UpdatePanelLicense { get { return udpLicense; } }

        public IList<IAS.BLL.AttachFilesIAS.AttachFile> AttachFiles
        {
            get { return (IList<IAS.BLL.AttachFilesIAS.AttachFile>)Session["attachfiles"] == null ? null : (IList<IAS.BLL.AttachFilesIAS.AttachFile>)Session["attachfiles"]; }
            set { Session["attachfiles"] = value; }
        }

        public List<DTO.PersonLicenseHead> PersonLicenseH
        {
            get { return (List<DTO.PersonLicenseHead>)Session["personlicenseh"] == null ? null : (List<DTO.PersonLicenseHead>)Session["personlicenseh"]; }
            set { Session["personlicenseh"] = value; }
        }

        public List<DTO.PersonLicenseDetail> PersonLicenseD
        {
            get { return (List<DTO.PersonLicenseDetail>)Session["personlicensed"] == null ? null : (List<DTO.PersonLicenseDetail>)Session["personlicensed"]; }
            set { Session["personlicensed"] = value; }
        }

        public DTO.PersonLicenseHead CurrentPersonLicenseH
        {
            get { return (DTO.PersonLicenseHead)Session["currentpersonlicenseh"] == null ? null : (DTO.PersonLicenseHead)Session["currentpersonlicenseh"]; }
            set { Session["currentpersonlicenseh"] = value; }
        }

        public DTO.PersonLicenseDetail CurrentPersonLicenseD
        {
            get { return (DTO.PersonLicenseDetail)Session["currentpersonlicensed"] == null ? null : (DTO.PersonLicenseDetail)Session["currentpersonlicensed"]; }
            set { Session["currentpersonlicensed"] = value; }
        }

        public string SelectedUploadGroupNo
        {
            get
            {
                return (string)Session["uploadgroupno"] == null ? string.Empty : (string)Session["uploadgroupno"].ToString();
            }
            set
            {
                Session["uploadgroupno"] = value;
            }

        }

        public List<string> ListUploadGroupNo
        {
            get
            {
                return (List<string>)Session["listuploadgroupno"] == null ? null : (List<string>)Session["listuploadgroupno"];
            }
            set
            {
                Session["listuploadgroupno"] = value;
            }

        }

        public List<DTO.SubGroupPayment> lsLicensePayment
        {
            get
            {
                if (Session["lsLicensePayment"] == null)
                {
                    Session["lsLicensePayment"] = new List<DTO.SubGroupPayment>();
                }

                return (List<DTO.SubGroupPayment>)Session["lsLicensePayment"];
            }
            set
            {
                Session["lsLicensePayment"] = value;
            }
        }

        public string SelectedLicenseNo
        {
            get
            {
                return (string)Session["SelectedLicenseNo"] == null ? string.Empty : Session["SelectedLicenseNo"].ToString();
            }
            set
            {
                Session["SelectedLicenseNo"] = value;
            }
        }

        public string GroupID
        {
            get
            {
                return (string)Session["groupid"] == null ? string.Empty : Session["groupid"].ToString();
            }
            set
            {
                Session["groupid"] = value;
            }
        }

        public string CheckClearSession
        {
            get
            {
                return (string)Session["CheckClearSession"] == null ? string.Empty : Session["CheckClearSession"].ToString();
            }
            set
            {
                Session["CheckClearSession"] = value;
            }
        }

        public DTO.UserProfile UserProfile
        {
            get
            {
                return Session[PageList.UserProfile] == null ? null : (DTO.UserProfile)Session[PageList.UserProfile];
            }
        }

        public Int32 Menu
        {
            get
            {
                return (Int32)Session["menu"];
            }
            set
            {
                Session["menu"] = value;
            }

        }

        public Int32 Step
        {
            get
            {
                return (Int32)Session["step"];
            }
            set
            {
                Session["step"] = value;
            }

        }

        public Decimal Fee
        {
            get
            {
                return (Decimal)Session["fee"];
            }
            set
            {
                Session["fee"] = value;
            }

        }

        public String PersonId
        {
            get { return _personId; }
            set
            {
                _personId = value;
                ViewState["PersonId"] = _personId;
            }
        }

        public string PettionTypeCode
        {
            get
            {
                return (Session["pettiontypecode"] == null) ? "" : Session["pettiontypecode"].ToString();
            }
            set
            {
                Session["pettiontypecode"] = value;
            }
        }

        public string LicenseTypeCode
        {

            get { return Session["licensetypecode"] == null ? "" : Session["licensetypecode"].ToString(); }
            set { Session["licensetypecode"] = value; }
        }

        public string CurrentLicenseRenewTime
        {

            get { return Session["currentlicenserenewtime"] == null ? "" : Session["currentlicenserenewtime"].ToString(); }
            set { Session["currentlicenserenewtime"] = value; }
        }

        public string LicensePath
        {
            get { return Session["licensepath"] == null ? "" : Session["licensepath"].ToString(); }
            set { Session["licensepath"] = value; }
        }

        public string Redo
        {
            get { return Session["redo"] == null ? "" : Session["redo"].ToString(); }
            set { Session["redo"] = value; }
        }

        public Agreement_2 Agreement_2
        {
            get
            {
                return (this.Agreement_2 as Agreement_2);
            }

        }

        public string RenewPetitionName
        {
            get { return Session["renewpetitionname"] == null ? "" : Session["renewpetitionname"].ToString(); }
            set { Session["renewpetitionname"] = value; }
        }

        public string LicenseApprover
        {
            get { return Session["licenseapprover"] == null ? "" : Session["licenseapprover"].ToString(); }
            set { Session["licenseapprover"] = value; }
        }

        public string CurrentUploadGroupNO
        {

            get { return Session["CurrentUploadGroupNO"] == null ? "" : Session["CurrentUploadGroupNO"].ToString(); }
            set { Session["CurrentUploadGroupNO"] = value; }
        }

        //ใช้กรณีตรวจสอบผลการอบรมในการขอ License
        public string LICENSE_TYPE_CODE
        {
            get
            {
                return (Session["LICENSE_TYPE_CODE"] == null) ? "" : Session["LICENSE_TYPE_CODE"].ToString();
            }
            set
            {
                Session["LICENSE_TYPE_CODE"] = value;
            }
        }

        public string TRAIN_TIMES
        {
            get
            {
                return (Session["TRAIN_TIMES"] == null) ? "" : Session["TRAIN_TIMES"].ToString();
            }
            set
            {
                Session["TRAIN_TIMES"] = value;
            }
        }

        public string PETITION_TYPE_CODE
        {
            get
            {
                return (Session["PETITION_TYPE_CODE"] == null) ? "" : Session["PETITION_TYPE_CODE"].ToString();
            }
            set
            {
                Session["PETITION_TYPE_CODE"] = value;
            }
        }

        public List<DTO.TrainPersonHistory> CurrentTrainPerson
        {
            get
            {
                if (Session["CurrentTrainPerson"] == null)
                {
                    Session["CurrentTrainPerson"] = new List<DTO.TrainPersonHistory>();
                }

                return (List<DTO.TrainPersonHistory>)Session["CurrentTrainPerson"];
            }

            set
            {
                Session["CurrentTrainPerson"] = value;
            }

        }

        public List<DTO.ExamHistory> CurrentExamPerson
        {
            get
            {
                if (Session["CurrentExamPerson"] == null)
                {
                    Session["CurrentExamPerson"] = new List<DTO.ExamHistory>();
                }

                return (List<DTO.ExamHistory>)Session["CurrentExamPerson"];
            }

            set
            {
                Session["CurrentExamPerson"] = value;
            }

        }

        public bool SelectAgreement
        {
            get
            {
                if (Session["selectAgreement"] == null)
                {
                    Session["selectAgreement"] = false;
                }
                return (bool)Session["selectAgreement"];
            }
            set { Session["selectAgreement"] = value; }
        }

        public string LicenseRenameFirstName
        {
            get
            {
                return (Session["LicenseRenameFirstName"] == null) ? "" : Session["LicenseRenameFirstName"].ToString();
            }
            set
            {
                Session["LicenseRenameFirstName"] = value;
            }
        }

        public string LicenseRenameLastName
        {
            get
            {
                return (Session["LicenseRenameLastName"] == null) ? "" : Session["LicenseRenameLastName"].ToString();
            }
            set
            {
                Session["LicenseRenameLastName"] = value;
            }
        }

        public string LicenseGroupValidation
        {
            get { return (Session["LicenseGroupValidation"] == null) ? "" : Session["LicenseGroupValidation"].ToString(); }
            set
            {
                Session["LicenseGroupValidation"] = value;
            }
        }

        public string PersonalSkillStage
        {
            get
            {
                return Session["PersonalSkillStage"] == null ? string.Empty : Session["PersonalSkillStage"].ToString();
            }
            set
            {
                Session["PersonalSkillStage"] = value;
            }
        }

        public string SelectedReplaceLicenseDate
        {
            get
            {
                return Session["SelectedReplaceLicenseDate"] == null ? string.Empty : Session["SelectedReplaceLicenseDate"].ToString();
            }
            set
            {
                Session["SelectedReplaceLicenseDate"] = value;
            }

        }

        public string SelectedReplaceLicenseExpireDate
        {
            get
            {
                return Session["SelectedReplaceLicenseExpireDate"] == null ? string.Empty : Session["SelectedReplaceLicenseExpireDate"].ToString();
            }
            set
            {
                Session["SelectedReplaceLicenseExpireDate"] = value;
            }

        }

        public List<DTO.PersonLicenseTransaction> CurrentExpiredLicneseList
        {
            get
            {
                if (Session["CurrentExpiredLicneseList"] == null)
                {
                    Session["CurrentExpiredLicneseList"] = new List<DTO.PersonLicenseTransaction>();
                }

                return (List<DTO.PersonLicenseTransaction>)Session["CurrentExpiredLicneseList"];
            }

            set
            {
                Session["CurrentExpiredLicneseList"] = value;
            }

        }

        #endregion

        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MenuInit();
                //Get Fee by License Type
                GetFee();
            }

        }
        #endregion

        #region Main Public & Private Function
        private void MenuInit()
        {
            this.PersonId = this.UserProfile.Id;
            string strMenu = this.Request.QueryString["M"];
            string strStep = this.Request.QueryString["S"];

            if ((strMenu == null) || (strStep == null))
            {
                return;
            }
            else
            {
                //Set Menu & Step Session
                this.Menu = Convert.ToInt32(strMenu);
                this.Step = Convert.ToInt32(strStep);

                if (strStep.Equals(stepType[0]))
                {
                    btnloop.Visible = true;
                    btnloop.Enabled = true;
                }
                else
                {
                    btnloop.Visible = false;
                    btnloop.Enabled = false;
                }

                //ขอรับใบอนุญาตใหม่
                if (strMenu.Equals(menuType[0]))
                {
                    this.lblHeadName.Text = pageType[0].ToString();
                    this.PettionTypeCode = Convert.ToString((int)DTO.PettionCode.NewLicense);
                    SwapMenu();
                }
                //ขอต่อใบอนุญาต 1 ปี
                else if (strMenu.Equals(menuType[1]))
                {
                    this.lblHeadName.Text = pageType[1].ToString();
                    this.PettionTypeCode = Convert.ToString((int)DTO.PettionCode.RenewLicense1Y);
                    SwapMenu();
                }
                //ขาดต่อขอใหม่
                else if (strMenu.Equals(menuType[2]))
                {
                    this.lblHeadName.Text = pageType[2].ToString();
                    this.PettionTypeCode = Convert.ToString((int)DTO.PettionCode.ExpireRenewLicense);
                    SwapMenu();
                }
                //ใบแทน : ชำรุดสูญหาย
                else if (strMenu.Equals(menuType[3]))
                {
                    this.lblHeadName.Text = pageType[3].ToString();
                    this.PettionTypeCode = Convert.ToString((int)DTO.PettionCode.OtherLicense_1);
                    SwapMenu();

                }
                //ตรวจสอบการอนุมัติเอกสาร
                else if (strMenu.Equals(menuType[4]))
                {
                    this.lblHeadName.Text = pageType[4].ToString();

                }
                //ใบแทน : เปลี่ยนชื่อ-สกุล
                else if (strMenu.Equals(menuType[5]))
                {
                    this.lblHeadName.Text = pageType[5].ToString();
                    this.PettionTypeCode = Convert.ToString((int)DTO.PettionCode.OtherLicense_1);
                    SwapMenu();

                }
            }



        }

        private void SwapMenu()
        {
            string strStep = this.Request.QueryString["S"];
            if((strStep != null) || (strStep != ""))
            {

                if (strStep.Equals(stepType[0]))
                {
                    imgStep1.ImageUrl = "../Images/lincese2/menu_step-Recovered_01.png";

                }
                else if (strStep.Equals(stepType[1]))
                {
                    imgStep1.ImageUrl = "../Images/lincese2/menu_step-Recovered_01.png";
                    imgStep2.ImageUrl = "../Images/lincese2/menu_step-Recovered_02.png";


                }
                else if (strStep.Equals(stepType[2]))
                {
                    imgStep1.ImageUrl = "../Images/lincese2/menu_step-Recovered_01.png";
                    imgStep2.ImageUrl = "../Images/lincese2/menu_step-Recovered_02.png";
                    imgStep3.ImageUrl = "../Images/lincese2/menu_step-Recovered_03.png";

                }
                else if (strStep.Equals(stepType[3]))
                {
                    imgStep1.ImageUrl = "../Images/lincese2/menu_step-Recovered_01.png";
                    imgStep2.ImageUrl = "../Images/lincese2/menu_step-Recovered_02.png";
                    imgStep3.ImageUrl = "../Images/lincese2/menu_step-Recovered_03.png";
                    imgStep4.ImageUrl = "../Images/lincese2/menu_step-Recovered_04.png";


                }
                else if (strStep.Equals(stepType[4]))
                {
                    imgStep1.ImageUrl = "../Images/lincese2/menu_step-Recovered_01.png";
                    imgStep2.ImageUrl = "../Images/lincese2/menu_step-Recovered_02.png";
                    imgStep3.ImageUrl = "../Images/lincese2/menu_step-Recovered_03.png";
                    imgStep4.ImageUrl = "../Images/lincese2/menu_step-Recovered_04.png";
                    imgStep5.ImageUrl = "../Images/lincese2/menu_step-Recovered_05.png";


                }
                else if (strStep.Equals(stepType[5]) || strStep.Equals(stepType[6]))
                {
                    imgStep1.Visible = false;
                    imgStep2.Visible = false;
                    imgStep3.Visible = false;
                    imgStep4.Visible = false;
                    imgStep5.Visible = false;
                }
            }
            
        }

        public void GetFee()
        {
            if (this.Menu != null)
            {
                if (this.Menu.Equals(1))
                {
                    TextBox txtFee = (TextBox)this.LicenseDetail.FindControl("txtFee");
                    if (txtFee != null)
                    {
                        //this.Fee = (int)DTO.FeeLicense.NewLicense;
                        GetFeeByPetitionCode();
                        txtFee.Text = this.Fee.ToString();
                    }
                }
                else
                {
                    switch (this.Menu)
                    {
                        //Renewlicense_1Y = 200, //ค่าต่ออายุบุคคลธรรมดา 1 ปี
                        case 2:
                            GetFeeByPetitionCode();
                            break;
                        //ExpiredRenewLicense = 300, //ค่าออกใบอนุญาตบุคคลธรรมดา (ขาดต่อขอใหม่)
                        case 3:
                            GetFeeByPetitionCode();
                            break;
                        //OtherLicense_1 = 200, //ค่าออกใบอนุญาตนิติบุคคล(ใบแทน ชำรุดสูญหาย)
                        case 4:
                            GetFeeByPetitionCode();
                            break;
                        //OtherLicense_1 = 0, //ค่าออกใบอนุญาตนิติบุคคล(ใบแทน เปลี่ยนชื่อ-สกุล)
                        case 6:
                            this.Fee = 0;
                            break;
                    }
                }

            }
        }

        public void GetFeeByPetitionCode()
        {
            DataCenterBiz biz = new DataCenterBiz();
            if( (this.PettionTypeCode != null) && (this.PettionTypeCode != "") )
            {

                DTO.ResponseMessage<decimal> resFee = biz.GetPetitionFee(this.PettionTypeCode);
                if (!resFee.IsError)
                {
                    this.Fee = resFee.ResultMessage;
                }
                else
                {
                    UCLicenseUCLicenseModelError.ShowMessageError = SysMessage.FeeNull;
                    UCLicenseUCLicenseModelError.ShowModalError();
                    return;
                }
            }
        }

        public ResponseService<string> GetTileName(int id)
        {
            var res = new ResponseService<string>();

            DataCenterBiz dbiz = new DataCenterBiz();
            DTO.DataItem resTitle = dbiz.GetTitleNameById(id);
            res.DataResponse = resTitle.Name;

            return res;
        }

        public bool ValidateLicenseDetail()
        {
            LicenseBiz biz = new LicenseBiz();
            ResponseMessage<bool> res = new ResponseMessage<bool>();

            foreach (DTO.PersonLicenseHead h in this.PersonLicenseH)
            {
                DTO.PersonLicenseDetail details = this.PersonLicenseD.Where(group => group.UPLOAD_GROUP_NO == h.UPLOAD_GROUP_NO).FirstOrDefault();
                ResponseMessage<bool> result = biz.SingleLicenseValidation(h, details);

                if (res.IsError)
                {
                    UCLicenseUCLicenseModelError.ShowMessageError = res.ErrorMsg.ToString();
                    UCLicenseUCLicenseModelError.ShowModalError();
                }

            }
            return res.ResultMessage;
        }

        /// <summary>
        /// Get New License Entity
        /// ขอใหม่
        /// Last update 21-11-56
        /// @Nattapong
        /// </summary>
        public void getPersonLicenseEntity()
        {
            LicenseBiz biz = new LicenseBiz();
            PersonBiz pbiz = new PersonBiz();
            ResponseService<DTO.Person> resUser = pbiz.GetById(this.UserProfile.Id);

            if (resUser.DataResponse != null)
            {
                //Declare New Entity equal Session
                List<PersonLicenseHead> lsHead = new List<PersonLicenseHead>();
                List<PersonLicenseDetail> lsDetail = new List<PersonLicenseDetail>();

                if ((this.PersonLicenseH != null) && (this.PersonLicenseD != null))
                {
                    lsHead = this.PersonLicenseH;
                    lsDetail = this.PersonLicenseD;

                }

                PersonLicenseHead h = new PersonLicenseHead();
                PersonLicenseDetail d = new PersonLicenseDetail();
                DTO.ResponseService<DTO.TrainPersonHistory[]> resTrain = biz.GetTrainingHistoryByIDWithCondition(this.UserProfile.IdCard, this.LicenseTypeCode);
                if (resTrain.DataResponse.Count() > 0)
                {
                    this.CurrentTrainPerson = resTrain.DataResponse.ToList();
                }

                //ตรวจสอบ ตั้งค่าการตรวจสอบคุณสมบัติ การขอรับใบอนุญาต 
                string statusCfg = this.GetLicenseConfigStatus();
                if (statusCfg.Equals("Y"))
                {
                    if (this.CurrentTrainPerson.Count > 0)
                    {
                        d.PAY_EXPIRE = this.CurrentTrainPerson[0].TRAIN_DATE_EXP;
                    }
                    else
                    {
                        d.PAY_EXPIRE = DateTime.Now.AddYears(1);
                    }
                }
                else
                {
                    d.PAY_EXPIRE = DateTime.Now.AddYears(1);
                }

                //Set AG_IAS_LICENSE_H
                //string groupNo = IAS.BLL.Helpers.GenerateIdHelper.GetGenAutoId();

                h.UPLOAD_GROUP_NO = d.UPLOAD_GROUP_NO = this.CurrentUploadGroupNO;
                //h.COMP_CODE = ddCompany.SelectedItem.Value;
                //h.COMP_NAME = ddCompany.SelectedItem.Text;
                h.TRAN_DATE = DateTime.Now;
                h.LOTS = 1;
                h.MONEY = Convert.ToDecimal(this.Fee);
                h.LICENSE_TYPE_CODE = this.LicenseTypeCode;
                h.FILENAME = "";
                h.PETITION_TYPE_CODE = this.PettionTypeCode;
                h.UPLOAD_BY_SESSION = this.UserProfile.Id;

                h.APPROVE_COMPCODE = this.LicenseApprover;
                h.APPROVED_DOC = DTO.ApprocLicense.W.ToString();
                this.CurrentLicenseRenewTime = "0";
                //Set AG_IAS_LICENSE_D
                ResponseService<DTO.PersonLicenseDetail[]> res2 = biz.GenSEQLicenseDetail(h);
                if (res2.DataResponse != null)
                {
                    if (res2.DataResponse.Count() > 0)
                    {
                        d.SEQ_NO = res2.DataResponse[0].SEQ_NO;
                        d.ORDERS = "";
                        d.LICENSE_NO = "";
                        d.LICENSE_DATE = null;
                        d.LICENSE_EXPIRE_DATE = null;
                        d.FEES = Convert.ToDecimal(this.Fee);
                        d.ID_CARD_NO = this.UserProfile.IdCard;
                        d.RENEW_TIMES = "0";
                        d.PRE_NAME_CODE = resUser.DataResponse.PRE_NAME_CODE;
                        d.TITLE_NAME = GetTileName(Convert.ToInt32(resUser.DataResponse.PRE_NAME_CODE)).DataResponse;  //resUser.DataResponse.PRE_NAME_CODE;
                        d.NAMES = resUser.DataResponse.NAMES;
                        d.LASTNAME = resUser.DataResponse.LASTNAME;
                        d.ADDRESS_1 = resUser.DataResponse.ADDRESS_1;
                        d.ADDRESS_2 = resUser.DataResponse.ADDRESS_2;
                        //d.AREA_CODE = resUser.DataResponse.AREA_CODE;
                        d.AREA_CODE = this.GetAreaCode(resUser.DataResponse.LOCAL_PROVINCE_CODE, resUser.DataResponse.LOCAL_AREA_CODE, resUser.DataResponse.LOCAL_TUMBON_CODE);
                        d.CURRENT_ADDRESS_1 = resUser.DataResponse.LOCAL_ADDRESS1;
                        d.CURRENT_ADDRESS_2 = resUser.DataResponse.LOCAL_ADDRESS2;
                        d.CURRENT_AREA_CODE = resUser.DataResponse.LOCAL_AREA_CODE;
                        d.EMAIL = resUser.DataResponse.EMAIL;
                        d.APPROVED = DTO.ApprocLicense.W.ToString();
                        if (this.CurrentTrainPerson.Count > 0)
                        {
                            d.PAY_EXPIRE = this.CurrentTrainPerson[0].TRAIN_DATE_EXP;
                        }
                        
                    }
                    else
                    {
                        d.SEQ_NO = res2.DataResponse[0].SEQ_NO;
                        d.ORDERS = "";
                        d.LICENSE_NO = "";
                        d.LICENSE_DATE = DateTime.Now;
                        d.LICENSE_EXPIRE_DATE = DateTime.Now.AddYears(5);
                        d.FEES = Convert.ToDecimal(this.Fee);
                        d.ID_CARD_NO = this.UserProfile.IdCard;
                        d.RENEW_TIMES = "0";
                        d.PRE_NAME_CODE = resUser.DataResponse.PRE_NAME_CODE;
                        d.TITLE_NAME = GetTileName(Convert.ToInt32(resUser.DataResponse.PRE_NAME_CODE)).DataResponse;  //resUser.DataResponse.PRE_NAME_CODE;
                        d.NAMES = resUser.DataResponse.NAMES;
                        d.LASTNAME = resUser.DataResponse.LASTNAME;
                        d.ADDRESS_1 = resUser.DataResponse.ADDRESS_1;
                        d.ADDRESS_2 = resUser.DataResponse.ADDRESS_2;
                        d.AREA_CODE = resUser.DataResponse.AREA_CODE;
                        d.CURRENT_ADDRESS_1 = resUser.DataResponse.LOCAL_ADDRESS1;
                        d.CURRENT_ADDRESS_2 = resUser.DataResponse.LOCAL_ADDRESS2;
                        d.CURRENT_AREA_CODE = resUser.DataResponse.LOCAL_AREA_CODE;
                        d.EMAIL = resUser.DataResponse.EMAIL;
                        d.APPROVED = DTO.ApprocLicense.W.ToString();
                        if (this.CurrentTrainPerson.Count > 0)
                        {
                            d.PAY_EXPIRE = this.CurrentTrainPerson[0].TRAIN_DATE_EXP;
                        }
                    }
                }
                if (this.PersonLicenseH == null)
                {
                    lsHead.Add(h);
                    lsDetail.Add(d);
                    this.PersonLicenseH = lsHead;
                    this.PersonLicenseD = lsDetail;

                    //Set Current License For Validation
                    this.CurrentPersonLicenseH = h;
                    this.CurrentPersonLicenseD = d;

                    //NEW_TOR สำหรับตรวจสอบผลอบรมเท่านั้น
                    this.PETITION_TYPE_CODE = h.PETITION_TYPE_CODE;
                    this.LICENSE_TYPE_CODE = this.LicenseTypeCode;
                    this.TRAIN_TIMES = d.RENEW_TIMES;
                }
                else
                {
                    if (this.PettionTypeCode == "11")
                    {
                        //NEW_TOR สำหรับตรวจสอบผลอบรมเท่านั้น
                        this.PETITION_TYPE_CODE = h.PETITION_TYPE_CODE;
                        this.LICENSE_TYPE_CODE = this.LicenseTypeCode;
                        this.TRAIN_TIMES = d.RENEW_TIMES;

                        //NEW_TOR
                        if (this.LicenseTypeCode != null)
                        {
                            var licenseChk = LicenseExp.Where(li => li.Contains(LicenseTypeCode)).FirstOrDefault();
                            if (licenseChk != null)
                            {
                                DTO.ResponseService<DTO.TrainPersonHistory[]> resExp = biz.GetTrainingHistoryByIDWithCondition(this.UserProfile.IdCard, this.LicenseTypeCode);
                                if (resExp.DataResponse.Count() > 0)
                                {
                                    d.PAY_EXPIRE = resExp.DataResponse[0].TRAIN_DATE_EXP;
                                }

                            }
                        }

                        if ((lsHead.Where(a => a.LICENSE_TYPE_CODE == "03").FirstOrDefault() == null) && (this.LicenseTypeCode != "04"))
                        {
                            lsHead.Add(h);
                            lsDetail.Add(d);
                            this.PersonLicenseH = lsHead;
                            this.PersonLicenseD = lsDetail;

                            //Set Current License For Validation
                            this.CurrentPersonLicenseH = h;
                            this.CurrentPersonLicenseD = d;

                        }
                        else if ((lsHead.Where(a => a.LICENSE_TYPE_CODE == "04").FirstOrDefault() == null) && (this.LicenseTypeCode != "03"))
                        {
                            lsHead.Add(h);
                            lsDetail.Add(d);
                            this.PersonLicenseH = lsHead;
                            this.PersonLicenseD = lsDetail;

                            //Set Current License For Validation
                            this.CurrentPersonLicenseH = h;
                            this.CurrentPersonLicenseD = d;
                        }
                        else
                        {
                            //Set Current License For Validation
                            this.CurrentPersonLicenseH = h;
                            this.CurrentPersonLicenseD = d;

                        }
                    }
                }
                //Add Loop License
           
               
            }
            else
            {
                UCLicenseUCLicenseModelError.ShowMessageError = resUser.ErrorMsg;
                UCLicenseUCLicenseModelError.ShowModalError();
                return;
            }
            
        }

        /// <summary>
        /// Get ReNew License Entity
        /// ต่ออายุ
        /// Last update 21-11-56
        /// @Nattapong
        /// </summary>
        public void getPersonRenewLicense1Y()
        {
            LicenseBiz biz = new LicenseBiz();
            PersonBiz pbiz = new PersonBiz();
            List<PersonLicenseHead> lsHead = new List<PersonLicenseHead>();
            List<PersonLicenseDetail> lsDetail = new List<PersonLicenseDetail>();
            ResponseService<DTO.Person> resUser = pbiz.GetById(this.UserProfile.Id);

            if ((this.PersonLicenseH != null) && (this.PersonLicenseD != null))
            {
                lsHead = this.PersonLicenseH;
                lsDetail = this.PersonLicenseD;

            }
            string selectedLicense = this.SelectedLicenseNo;
            DTO.ResponseService<DTO.PersonLicenseTransaction> res = biz.GetRenewLiceneEntityByLicenseNo(selectedLicense);
            if (res.DataResponse != null)
            {
                //Get Head
                PersonLicenseHead h = new PersonLicenseHead();
                PersonLicenseDetail d = new PersonLicenseDetail();
                DTO.ResponseService<DTO.TrainPersonHistory[]> resTrain = biz.GetTrainingHistoryByIDWithCondition(this.UserProfile.IdCard, this.LicenseTypeCode);
                if (resTrain.DataResponse.Count() > 0)
                {
                    this.CurrentTrainPerson = resTrain.DataResponse.ToList();
                    d.PAY_EXPIRE = resTrain.DataResponse[0].TRAIN_DATE_EXP;
                }
                else
                {
                    d.PAY_EXPIRE = DateTime.Now.AddYears(1);
                }
                //h.UPLOAD_GROUP_NO = res.DataResponse.UPLOAD_GROUP_NO;
                //string groupNo = IAS.BLL.Helpers.GenerateIdHelper.GetGenAutoId();
                h.UPLOAD_GROUP_NO = d.UPLOAD_GROUP_NO = this.CurrentUploadGroupNO;
                h.COMP_CODE = res.DataResponse.COMP_CODE;
                h.COMP_NAME = res.DataResponse.COMP_NAME;
                h.TRAN_DATE = DateTime.Now;
                h.LOTS = 1;
                this.CurrentLicenseRenewTime = res.DataResponse.RENEW_TIMES;
               
                this.LicenseTypeCode = h.LICENSE_TYPE_CODE = res.DataResponse.LICENSE_TYPE_CODE;
                h.FILENAME = res.DataResponse.FILENAME;
                if (((Convert.ToInt32(res.DataResponse.RENEW_TIMES) + 1) >= 0) && ((Convert.ToInt32(res.DataResponse.RENEW_TIMES) + 1) < 3))
                {
                    h.PETITION_TYPE_CODE = Convert.ToString((int)DTO.PettionCode.RenewLicense1Y);
                    this.Fee = (int)DTO.FeeLicense.Renewlicense_1Y;
                }
                else if ((Convert.ToInt32(res.DataResponse.RENEW_TIMES) + 1) >= 3)
                {
                    h.PETITION_TYPE_CODE = Convert.ToString((int)DTO.PettionCode.RenewLicense5Y);
                    this.Fee = (int)DTO.FeeLicense.Renewlicense_5Y;
                }
                //h.PETITION_TYPE_CODE = this.PettionTypeCode;
                h.UPLOAD_BY_SESSION = this.UserProfile.Id;
                h.MONEY = this.Fee;

                h.APPROVE_COMPCODE = this.LicenseApprover;

                if (h.PETITION_TYPE_CODE.Equals(PettionCode.RenewLicense5Y.GetEnumValue().ToString()))
                {
                    h.APPROVED_DOC = DTO.ApprocLicense.W.ToString();
                }
                else
                {
                    h.APPROVED_DOC = DTO.ApprocLicense.Y.ToString();
                }
              

                //Get Detail
                
                int renewtimes = Convert.ToInt32(res.DataResponse.RENEW_TIMES) + 1;
                ResponseService<DTO.PersonLicenseDetail[]> res2 = biz.GenSEQLicenseDetail(h);
                //d.UPLOAD_GROUP_NO = res.DataResponse.UPLOAD_GROUP_NO;
                d.SEQ_NO = res2.DataResponse[0].SEQ_NO;
                d.ORDERS = res.DataResponse.ORDERS;
                d.LICENSE_NO = res.DataResponse.LICENSE_NO;
                //Update after Pay
                //d.LICENSE_DATE = DateTime.Now;
                //if ((Convert.ToInt32(res.DataResponse.RENEW_TIMES) >= 0) && (Convert.ToInt32(res.DataResponse.RENEW_TIMES) < 3))
               // {
                    //Update after Pay
                    //d.LICENSE_EXPIRE_DATE = DateTime.Now.AddYears(1);
                   // this.Fee = (int)DTO.FeeLicense.Renewlicense_1Y;
                //}
                //else if (Convert.ToInt32(res.DataResponse.RENEW_TIMES) >= 3)
               // {
                    //Update after Pay
                    //d.LICENSE_EXPIRE_DATE = DateTime.Now.AddYears(5);
                 //   this.Fee = (int)DTO.FeeLicense.Renewlicense_5Y;
                //}
                //d.LICENSE_EXPIRE_DATE = DateTime.Now.AddYears(1);

                //update by Chalermwut 08/10/2557

                if (((Convert.ToInt32(res.DataResponse.RENEW_TIMES) + 1) >= 0) && ((Convert.ToInt32(res.DataResponse.RENEW_TIMES) + 1) < 3))
                {
                    this.Fee = (int)DTO.FeeLicense.Renewlicense_1Y;
                }
                else
                {
                    this.Fee = (int)DTO.FeeLicense.Renewlicense_5Y;
                }

                d.FEES = this.Fee;




                d.ID_CARD_NO = this.UserProfile.IdCard;
                d.RENEW_TIMES = Convert.ToString(renewtimes);

                d.PRE_NAME_CODE = resUser.DataResponse.PRE_NAME_CODE;
                d.TITLE_NAME = GetTileName(Convert.ToInt32(resUser.DataResponse.PRE_NAME_CODE)).DataResponse;  //resUser.DataResponse.PRE_NAME_CODE;
                d.NAMES = resUser.DataResponse.NAMES;
                d.LASTNAME = resUser.DataResponse.LASTNAME;
                d.ADDRESS_1 = resUser.DataResponse.ADDRESS_1;
                d.ADDRESS_2 = resUser.DataResponse.ADDRESS_2;
                //d.AREA_CODE = resUser.DataResponse.AREA_CODE;
                d.AREA_CODE = this.GetAreaCode(resUser.DataResponse.LOCAL_PROVINCE_CODE, resUser.DataResponse.LOCAL_AREA_CODE, resUser.DataResponse.LOCAL_TUMBON_CODE);
                d.CURRENT_ADDRESS_1 = resUser.DataResponse.LOCAL_ADDRESS1;
                d.CURRENT_ADDRESS_2 = resUser.DataResponse.LOCAL_ADDRESS2;
                d.CURRENT_AREA_CODE = resUser.DataResponse.LOCAL_AREA_CODE;
                d.EMAIL = resUser.DataResponse.EMAIL;
                d.HEAD_REQUEST_NO = res.DataResponse.HEAD_REQUEST_NO;

                
                //old code
                //if (this.PettionTypeCode.Equals(PettionCode.RenewLicense5Y.GetEnumValue().ToString()))
                //{
                //    d.APPROVED = this.GetDocStatus();
                //}

                //code by Chalermwut 16/09/2557
                if (h.PETITION_TYPE_CODE.Equals(PettionCode.RenewLicense5Y.GetEnumValue().ToString()))
                {
                    d.APPROVED = DTO.ApprocLicense.W.ToString();
                }
                else
                {
                    d.APPROVED = DTO.ApprocLicense.Y.ToString();
                }
                

                //NEW_TOR สำหรับตรวจสอบผลอบรมเท่านั้น
                this.PETITION_TYPE_CODE = h.PETITION_TYPE_CODE;
                this.LICENSE_TYPE_CODE = this.LicenseTypeCode;
                this.TRAIN_TIMES = d.RENEW_TIMES;

                //NEW_TOR
                if (this.LicenseTypeCode != null)
                {
                    var licenseChk = LicenseExp.Where(li => li.Contains(LicenseTypeCode)).FirstOrDefault();
                    if (licenseChk != null)
                    {
                        //DTO.ResponseService<DTO.TrainPersonHistory[]> resExp = biz.GetTrainingHistoryByIDWithCondition(this.UserProfile.IdCard, this.LicenseTypeCode);
                        //if (resExp.DataResponse.Count() > 0)
                        //{
                        //    d.PAY_EXPIRE = resExp.DataResponse[0].TRAIN_DATE_EXP;
                        //}

                        DTO.ResponseService<DTO.PersonLicenseTransaction> resExp = biz.GetLicenseRenewDateByLicenseNo(res.DataResponse.LICENSE_NO);
                        if (resExp.DataResponse != null)
                        {
                            d.PAY_EXPIRE = resExp.DataResponse.EXPIRE_DATE;
                        }
                    }
                }
            
                if (this.PersonLicenseH == null)
                {
                    lsHead.Add(h);
                    lsDetail.Add(d);
                    this.PersonLicenseH = lsHead;
                    this.PersonLicenseD = lsDetail;

                    //Set Current License For Validation
                    this.CurrentPersonLicenseH = h;
                    this.CurrentPersonLicenseD = d;
                }
                else
                {
                    
                    if (this.PettionTypeCode == "13")
                    {
                        //DTO.PersonLicenseHead CheckDup1Y03 = lsHead.Where(a => a.PETITION_TYPE_CODE == "13").FirstOrDefault();
                        //if ((CheckDup1Y03 == null))
                        //{
                        if ((lsHead.Where(a => a.LICENSE_TYPE_CODE == "03").FirstOrDefault() == null) && (this.LicenseTypeCode != "04"))
                            {
                                lsHead.Add(h);
                                lsDetail.Add(d);
                                this.PersonLicenseH = lsHead;
                                this.PersonLicenseD = lsDetail;

                                //Set Current License For Validation
                                this.CurrentPersonLicenseH = h;
                                this.CurrentPersonLicenseD = d;
                            }
                        else if ((lsHead.Where(a => a.LICENSE_TYPE_CODE == "04").FirstOrDefault() == null) && (this.LicenseTypeCode != "03"))
                            {
                                lsHead.Add(h);
                                lsDetail.Add(d);
                                this.PersonLicenseH = lsHead;
                                this.PersonLicenseD = lsDetail;

                                //Set Current License For Validation
                                this.CurrentPersonLicenseH = h;
                                this.CurrentPersonLicenseD = d;
                            }
                            else
                            {
                                //Set Current License For Validation
                                this.CurrentPersonLicenseH = h;
                                this.CurrentPersonLicenseD = d;

                            }

                       // }
                    }


                    else if (this.PettionTypeCode == "14")
                    {

                        if ((lsHead.Where(a => a.LICENSE_TYPE_CODE == "03").FirstOrDefault() == null) && (this.LicenseTypeCode != "04"))
                        {
                            lsHead.Add(h);
                            lsDetail.Add(d);
                            this.PersonLicenseH = lsHead;
                            this.PersonLicenseD = lsDetail;

                            //Set Current License For Validation
                            this.CurrentPersonLicenseH = h;
                            this.CurrentPersonLicenseD = d;

                        }
                        else if ((lsHead.Where(a => a.LICENSE_TYPE_CODE == "04").FirstOrDefault() == null) && (this.LicenseTypeCode != "03"))
                        {
                            lsHead.Add(h);
                            lsDetail.Add(d);
                            this.PersonLicenseH = lsHead;
                            this.PersonLicenseD = lsDetail;

                            //Set Current License For Validation
                            this.CurrentPersonLicenseH = h;
                            this.CurrentPersonLicenseD = d;
                        }
                        else
                        {
                            //Set Current License For Validation
                            this.CurrentPersonLicenseH = h;
                            this.CurrentPersonLicenseD = d;

                        }
                    }
                }

            }
            else
            {
                this.UCLicenseUCLicenseModelError.ShowMessageError = res.ErrorMsg;
                this.UCLicenseUCLicenseModelError.ShowModalError();
            }
        }

        /// <summary>
        /// Get Expired License Entity
        /// ขาดต่อขอใหม่
        /// Last update 21-11-56
        /// @Nattapong
        /// </summary>
        public void getPersonExpiredLicense()
        {
            LicenseBiz biz = new LicenseBiz();
            PersonBiz pbiz = new PersonBiz();
            List<PersonLicenseHead> lsHead = new List<PersonLicenseHead>();
            List<PersonLicenseDetail> lsDetail = new List<PersonLicenseDetail>();
            ResponseService<DTO.Person> resUser = pbiz.GetById(this.UserProfile.Id);

            if ((this.PersonLicenseH != null) && (this.PersonLicenseD != null))
            {
                lsHead = this.PersonLicenseH;
                lsDetail = this.PersonLicenseD;

            }
            string selectedLicense = this.SelectedLicenseNo;
            DTO.ResponseService<DTO.PersonLicenseTransaction> res = biz.GetRenewLiceneEntityByLicenseNo(selectedLicense);
            if (res.DataResponse != null)
            {
                //Get Head
                PersonLicenseHead h = new PersonLicenseHead();
                PersonLicenseDetail d = new PersonLicenseDetail();
                DTO.ResponseService<DTO.TrainPersonHistory[]> resTrain = biz.GetTrainingHistoryByIDWithCondition(this.UserProfile.IdCard, this.LicenseTypeCode);
                if (resTrain.DataResponse.Count() > 0)
                {
                    this.CurrentTrainPerson = resTrain.DataResponse.ToList();
                }

                //ตรวจสอบ ตั้งค่าการตรวจสอบคุณสมบัติ การขอรับใบอนุญาต 
                string statusCfg = this.GetLicenseConfigStatus();
                if (statusCfg.Equals("Y"))
                {
                    if (this.CurrentTrainPerson.Count > 0)
                    {
                        d.PAY_EXPIRE = this.CurrentTrainPerson[0].TRAIN_DATE_EXP;
                    }
                    else
                    {
                        d.PAY_EXPIRE = DateTime.Now.AddYears(1);
                    }
                }
                else
                {
                    d.PAY_EXPIRE = DateTime.Now.AddYears(1);
                }

                //h.UPLOAD_GROUP_NO = res.DataResponse.UPLOAD_GROUP_NO;
                //string groupNo = IAS.BLL.Helpers.GenerateIdHelper.GetGenAutoId();
                h.UPLOAD_GROUP_NO = d.UPLOAD_GROUP_NO = this.CurrentUploadGroupNO;
                h.COMP_CODE = res.DataResponse.COMP_CODE;
                h.COMP_NAME = res.DataResponse.COMP_NAME;
                h.TRAN_DATE = DateTime.Now;
                h.LOTS = 1;
                h.MONEY = this.Fee;
                this.CurrentLicenseRenewTime = res.DataResponse.RENEW_TIMES;
                this.LicenseTypeCode = h.LICENSE_TYPE_CODE = res.DataResponse.LICENSE_TYPE_CODE;
                h.FILENAME = res.DataResponse.FILENAME;
                h.PETITION_TYPE_CODE = this.PettionTypeCode;
                h.UPLOAD_BY_SESSION = this.UserProfile.Id;

                h.APPROVE_COMPCODE = this.LicenseApprover;
                h.APPROVED_DOC = DTO.ApprocLicense.W.ToString();

                //Get Detail
               
                int renewtimes = Convert.ToInt32(res.DataResponse.RENEW_TIMES) + 1;
                ResponseService<DTO.PersonLicenseDetail[]> res2 = biz.GenSEQLicenseDetail(h);
                //d.UPLOAD_GROUP_NO = res.DataResponse.UPLOAD_GROUP_NO;
                d.SEQ_NO = res2.DataResponse[0].SEQ_NO;
                d.ORDERS = "";
                d.LICENSE_NO = "";
                d.LICENSE_DATE = null;
                d.LICENSE_EXPIRE_DATE = null;
                d.FEES = this.Fee;
                d.ID_CARD_NO = this.UserProfile.IdCard;
                d.RENEW_TIMES = "0";
                d.PRE_NAME_CODE = resUser.DataResponse.PRE_NAME_CODE;
                d.TITLE_NAME = GetTileName(Convert.ToInt32(resUser.DataResponse.PRE_NAME_CODE)).DataResponse;  //resUser.DataResponse.PRE_NAME_CODE;
                d.NAMES = resUser.DataResponse.NAMES;
                d.LASTNAME = resUser.DataResponse.LASTNAME;
                d.ADDRESS_1 = resUser.DataResponse.ADDRESS_1;
                d.ADDRESS_2 = resUser.DataResponse.ADDRESS_2;
                //d.AREA_CODE = resUser.DataResponse.AREA_CODE;
                d.AREA_CODE = this.GetAreaCode(resUser.DataResponse.LOCAL_PROVINCE_CODE, resUser.DataResponse.LOCAL_AREA_CODE, resUser.DataResponse.LOCAL_TUMBON_CODE);
                d.CURRENT_ADDRESS_1 = resUser.DataResponse.LOCAL_ADDRESS1;
                d.CURRENT_ADDRESS_2 = resUser.DataResponse.LOCAL_ADDRESS2;
                d.CURRENT_AREA_CODE = resUser.DataResponse.LOCAL_AREA_CODE;
                d.EMAIL = resUser.DataResponse.EMAIL;
                //d.HEAD_REQUEST_NO = res.DataResponse.HEAD_REQUEST_NO;
                d.APPROVED = DTO.ApprocLicense.W.ToString();

                //Set Master
                //lsHead.Add(h);
                //lsDetail.Add(d);
                //this.PersonLicenseH = lsHead;
                //this.PersonLicenseD = lsDetail;

                //NEW_TOR สำหรับตรวจสอบผลอบรมเท่านั้น
                this.PETITION_TYPE_CODE = h.PETITION_TYPE_CODE;
                this.LICENSE_TYPE_CODE = this.LicenseTypeCode;
                this.TRAIN_TIMES = d.RENEW_TIMES;

                //NEW_TOR
                if (this.LicenseTypeCode != null)
                {
                    var licenseChk = LicenseExp.Where(li => li.Contains(LicenseTypeCode)).FirstOrDefault();
                    if (licenseChk != null)
                    {
                        DTO.ResponseService<DTO.TrainPersonHistory[]>  resExp = biz.GetTrainingHistoryByIDWithCondition(this.UserProfile.IdCard, this.LicenseTypeCode);
                        if(resExp.DataResponse.Count() > 0)
                        {
                            d.PAY_EXPIRE = resExp.DataResponse[0].TRAIN_DATE_EXP;
                        }
                        
                    }
                }

                if (this.PersonLicenseH == null)
                {
                    lsHead.Add(h);
                    lsDetail.Add(d);
                    this.PersonLicenseH = lsHead;
                    this.PersonLicenseD = lsDetail;

                    //Set Current License For Validation
                    this.CurrentPersonLicenseH = h;
                    this.CurrentPersonLicenseD = d;

                }
                else
                {
                    if (this.PettionTypeCode == "15")
                    {
                        if ((lsHead.Where(a => a.LICENSE_TYPE_CODE == "03").FirstOrDefault() == null) && (this.LicenseTypeCode != "04"))
                        {
                            lsHead.Add(h);
                            lsDetail.Add(d);
                            this.PersonLicenseH = lsHead;
                            this.PersonLicenseD = lsDetail;

                            //Set Current License For Validation
                            this.CurrentPersonLicenseH = h;
                            this.CurrentPersonLicenseD = d;

                        }
                        else if ((lsHead.Where(a => a.LICENSE_TYPE_CODE == "04").FirstOrDefault() == null) && (this.LicenseTypeCode != "03"))
                        {
                            lsHead.Add(h);
                            lsDetail.Add(d);
                            this.PersonLicenseH = lsHead;
                            this.PersonLicenseD = lsDetail;

                            //Set Current License For Validation
                            this.CurrentPersonLicenseH = h;
                            this.CurrentPersonLicenseD = d;
                        }
                        else
                        {
                            //Set Current License For Validation
                            this.CurrentPersonLicenseH = h;
                            this.CurrentPersonLicenseD = d;

                        }
                    }
                }
            }
            else
            {
                UCLicenseUCLicenseModelError.ShowMessageError = res.ErrorMsg;
                UCLicenseUCLicenseModelError.ShowModalError();
            }

        }

        /// <summary>
        /// Get All License Entity
        /// ขอใบแทน ใบอนุญาตที่มีผลปัจจุบัน
        /// Last update 27-06-57
        /// <AUTHOR>Natta</AUTHOR>
        /// </summary>
        /// <TYPE>ชำรุดสูญหาย</TYPE>
        /// <remarks>Fee = GetFrom DB</remarks>
        public void getReplaceLicenseByLostCase()
        {
            LicenseBiz biz = new LicenseBiz();
            PersonBiz pbiz = new PersonBiz();
            List<PersonLicenseHead> lsHead = new List<PersonLicenseHead>();
            List<PersonLicenseDetail> lsDetail = new List<PersonLicenseDetail>();
            ResponseService<DTO.Person> resUser = pbiz.GetById(this.UserProfile.Id);

            if ((this.PersonLicenseH != null) && (this.PersonLicenseD != null))
            {
                lsHead = this.PersonLicenseH;
                lsDetail = this.PersonLicenseD;

            }
            string selectedLicense = this.SelectedLicenseNo;
            DTO.ResponseService<DTO.PersonLicenseTransaction> res = biz.GetRenewLiceneEntityByLicenseNo(selectedLicense);
            if (res.DataResponse != null)
            {
                //Get Head
                PersonLicenseHead h = new PersonLicenseHead();
                PersonLicenseDetail d = new PersonLicenseDetail();
                DTO.ResponseService<DTO.TrainPersonHistory[]> resTrain = biz.GetTrainingHistoryByIDWithCondition(this.UserProfile.IdCard, this.LicenseTypeCode);
                if (resTrain.DataResponse.Count() > 0)
                {
                    this.CurrentTrainPerson = resTrain.DataResponse.ToList();
                    d.PAY_EXPIRE = resTrain.DataResponse[0].TRAIN_DATE_EXP;
                }
                else
                {
                    d.PAY_EXPIRE = DateTime.Now.AddYears(1);
                }
                //h.UPLOAD_GROUP_NO = res.DataResponse.UPLOAD_GROUP_NO;
                //string groupNo = IAS.BLL.Helpers.GenerateIdHelper.GetGenAutoId();
                h.UPLOAD_GROUP_NO = d.UPLOAD_GROUP_NO = this.CurrentUploadGroupNO;
                h.COMP_CODE = res.DataResponse.COMP_CODE;
                h.COMP_NAME = res.DataResponse.COMP_NAME;
                //h.TRAN_DATE = res.DataResponse.TRAN_DATE;
                h.TRAN_DATE = DateTime.Now;
                h.LOTS = 1;
                this.CurrentLicenseRenewTime = res.DataResponse.RENEW_TIMES;
                this.LicenseTypeCode = h.LICENSE_TYPE_CODE = res.DataResponse.LICENSE_TYPE_CODE;
                h.FILENAME = res.DataResponse.FILENAME;

                h.PETITION_TYPE_CODE = this.PettionTypeCode;
                h.UPLOAD_BY_SESSION = this.UserProfile.Id;
                h.MONEY = this.Fee;

                h.APPROVE_COMPCODE = this.LicenseApprover;
                h.APPROVED_DOC = DTO.ApprocLicense.W.ToString();

                //Get Detail
                ResponseService<DTO.PersonLicenseDetail[]> res2 = biz.GenSEQLicenseDetail(h);
                //d.UPLOAD_GROUP_NO = res.DataResponse.UPLOAD_GROUP_NO;
                d.SEQ_NO = res2.DataResponse[0].SEQ_NO;
                d.ORDERS = res.DataResponse.ORDERS;
                d.LICENSE_NO = res.DataResponse.LICENSE_NO;
                d.LICENSE_DATE = Convert.ToDateTime(this.SelectedReplaceLicenseDate);
                d.LICENSE_EXPIRE_DATE = Convert.ToDateTime(this.SelectedReplaceLicenseExpireDate);
                d.FEES = this.Fee;
                d.ID_CARD_NO = this.UserProfile.IdCard;
                d.RENEW_TIMES = res.DataResponse.RENEW_TIMES;

                d.PRE_NAME_CODE = resUser.DataResponse.PRE_NAME_CODE;
                d.TITLE_NAME = GetTileName(Convert.ToInt32(resUser.DataResponse.PRE_NAME_CODE)).DataResponse;  //resUser.DataResponse.PRE_NAME_CODE;
                d.NAMES = resUser.DataResponse.NAMES;
                d.LASTNAME = resUser.DataResponse.LASTNAME;
                d.ADDRESS_1 = resUser.DataResponse.ADDRESS_1;
                d.ADDRESS_2 = resUser.DataResponse.ADDRESS_2;
                d.AREA_CODE = resUser.DataResponse.AREA_CODE;
                d.CURRENT_ADDRESS_1 = resUser.DataResponse.LOCAL_ADDRESS1;
                d.CURRENT_ADDRESS_2 = resUser.DataResponse.LOCAL_ADDRESS2;
                d.CURRENT_AREA_CODE = resUser.DataResponse.LOCAL_AREA_CODE;
                d.EMAIL = resUser.DataResponse.EMAIL;
                d.HEAD_REQUEST_NO = res.DataResponse.HEAD_REQUEST_NO;
                d.APPROVED = DTO.ApprocLicense.W.ToString();

                //NEW_TOR สำหรับตรวจสอบผลอบรมเท่านั้น
                this.PETITION_TYPE_CODE = h.PETITION_TYPE_CODE;
                this.LICENSE_TYPE_CODE = this.LicenseTypeCode;
                this.TRAIN_TIMES = d.RENEW_TIMES;

                //NEW_TOR
                if (this.LicenseTypeCode != null)
                {
                    var licenseChk = LicenseExp.Where(li => li.Contains(LicenseTypeCode)).FirstOrDefault();
                    if (licenseChk != null)
                    {
                        DTO.ResponseService<DTO.TrainPersonHistory[]> resExp = biz.GetTrainingHistoryByIDWithCondition(this.UserProfile.IdCard, this.LicenseTypeCode);
                        if (resExp.DataResponse.Count() > 0)
                        {
                            d.PAY_EXPIRE = resExp.DataResponse[0].TRAIN_DATE_EXP;
                        }

                    }
                }

                if (this.PersonLicenseH == null)
                {
                    lsHead.Add(h);
                    lsDetail.Add(d);
                    this.PersonLicenseH = lsHead;
                    this.PersonLicenseD = lsDetail;

                    //Set Current License For Validation
                    this.CurrentPersonLicenseH = h;
                    this.CurrentPersonLicenseD = d;

                }
                else
                {

                    if (this.PettionTypeCode == "16")
                    {
                        if ((lsHead.Where(a => a.LICENSE_TYPE_CODE == "03").FirstOrDefault() == null) && (this.LicenseTypeCode != "04"))
                        {
                            lsHead.Add(h);
                            lsDetail.Add(d);
                            this.PersonLicenseH = lsHead;
                            this.PersonLicenseD = lsDetail;

                            //Set Current License For Validation
                            this.CurrentPersonLicenseH = h;
                            this.CurrentPersonLicenseD = d;

                        }
                        else if ((lsHead.Where(a => a.LICENSE_TYPE_CODE == "04").FirstOrDefault() == null) && (this.LicenseTypeCode != "03"))
                        {
                            lsHead.Add(h);
                            lsDetail.Add(d);
                            this.PersonLicenseH = lsHead;
                            this.PersonLicenseD = lsDetail;

                            //Set Current License For Validation
                            this.CurrentPersonLicenseH = h;
                            this.CurrentPersonLicenseD = d;
                        }
                        else
                        {
                            //Set Current License For Validation
                            this.CurrentPersonLicenseH = h;
                            this.CurrentPersonLicenseD = d;

                        }
                    }
                }

            }


        }

        /// <summary>
        /// Get All License Entity
        /// ขอใบแทน ใบอนุญาตที่มีผลปัจจุบัน
        /// Last update 27-06-57
        /// <AUTHOR>Natta</AUTHOR>
        /// </summary>
        /// <TYPE>เปลี่ยนชื่อ-สกุล</TYPE>
        /// <remarks>Fee = 0</remarks>
        public void getReplaceLicenseByRenameCase()
        {
            LicenseBiz biz = new LicenseBiz();
            PersonBiz pbiz = new PersonBiz();
            List<PersonLicenseHead> lsHead = new List<PersonLicenseHead>();
            List<PersonLicenseDetail> lsDetail = new List<PersonLicenseDetail>();
            ResponseService<DTO.Person> resUser = pbiz.GetById(this.UserProfile.Id);

            if ((this.PersonLicenseH != null) && (this.PersonLicenseD != null))
            {
                lsHead = this.PersonLicenseH;
                lsDetail = this.PersonLicenseD;

            }
            string selectedLicense = this.SelectedLicenseNo;
            DTO.ResponseService<DTO.PersonLicenseTransaction> res = biz.GetRenewLiceneEntityByLicenseNo(selectedLicense);
            if (res.DataResponse != null)
            {
                //Get Head
                PersonLicenseHead h = new PersonLicenseHead();
                PersonLicenseDetail d = new PersonLicenseDetail();
                DTO.ResponseService<DTO.TrainPersonHistory[]> resTrain = biz.GetTrainingHistoryByIDWithCondition(this.UserProfile.IdCard, this.LicenseTypeCode);
                if (resTrain.DataResponse.Count() > 0)
                {
                    this.CurrentTrainPerson = resTrain.DataResponse.ToList();
                    d.PAY_EXPIRE = resTrain.DataResponse[0].TRAIN_DATE_EXP;
                }
                else
                {
                    d.PAY_EXPIRE = DateTime.Now.AddYears(1);
                }
                //h.UPLOAD_GROUP_NO = res.DataResponse.UPLOAD_GROUP_NO;
                //string groupNo = IAS.BLL.Helpers.GenerateIdHelper.GetGenAutoId();
                h.UPLOAD_GROUP_NO = d.UPLOAD_GROUP_NO = this.CurrentUploadGroupNO;
                h.COMP_CODE = res.DataResponse.COMP_CODE;
                h.COMP_NAME = res.DataResponse.COMP_NAME;
                //h.TRAN_DATE = res.DataResponse.TRAN_DATE;
                h.TRAN_DATE = DateTime.Now;
                h.LOTS = 1;
                this.CurrentLicenseRenewTime = res.DataResponse.RENEW_TIMES;
                this.LicenseTypeCode = h.LICENSE_TYPE_CODE = res.DataResponse.LICENSE_TYPE_CODE;
                h.FILENAME = res.DataResponse.FILENAME;

                h.PETITION_TYPE_CODE = this.PettionTypeCode;
                h.UPLOAD_BY_SESSION = this.UserProfile.Id;
                h.MONEY = this.Fee;

                h.APPROVE_COMPCODE = this.LicenseApprover;
                h.APPROVED_DOC = DTO.ApprocLicense.W.ToString();

                //Get Detail
                ResponseService<DTO.PersonLicenseDetail[]> res2 = biz.GenSEQLicenseDetail(h);
                //d.UPLOAD_GROUP_NO = res.DataResponse.UPLOAD_GROUP_NO;
                d.SEQ_NO = res2.DataResponse[0].SEQ_NO;
                d.ORDERS = res.DataResponse.ORDERS;
                d.LICENSE_NO = res.DataResponse.LICENSE_NO;
                d.LICENSE_DATE = Convert.ToDateTime(this.SelectedReplaceLicenseDate);
                d.LICENSE_EXPIRE_DATE = Convert.ToDateTime(this.SelectedReplaceLicenseExpireDate);
                d.FEES = this.Fee;
                d.ID_CARD_NO = this.UserProfile.IdCard;
                d.RENEW_TIMES = res.DataResponse.RENEW_TIMES;

                d.PRE_NAME_CODE = resUser.DataResponse.PRE_NAME_CODE;
                d.TITLE_NAME = GetTileName(Convert.ToInt32(resUser.DataResponse.PRE_NAME_CODE)).DataResponse;  //resUser.DataResponse.PRE_NAME_CODE;
                d.NAMES = this.LicenseRenameFirstName;
                d.LASTNAME = this.LicenseRenameLastName;
                d.ADDRESS_1 = resUser.DataResponse.ADDRESS_1;
                d.ADDRESS_2 = resUser.DataResponse.ADDRESS_2;
                d.AREA_CODE = resUser.DataResponse.AREA_CODE;
                d.CURRENT_ADDRESS_1 = resUser.DataResponse.LOCAL_ADDRESS1;
                d.CURRENT_ADDRESS_2 = resUser.DataResponse.LOCAL_ADDRESS2;
                d.CURRENT_AREA_CODE = resUser.DataResponse.LOCAL_AREA_CODE;
                d.EMAIL = resUser.DataResponse.EMAIL;
                d.HEAD_REQUEST_NO = res.DataResponse.HEAD_REQUEST_NO;
                d.APPROVED = DTO.ApprocLicense.W.ToString();

                //NEW_TOR สำหรับตรวจสอบผลอบรมเท่านั้น
                this.PETITION_TYPE_CODE = h.PETITION_TYPE_CODE;
                this.LICENSE_TYPE_CODE = this.LicenseTypeCode;
                this.TRAIN_TIMES = d.RENEW_TIMES;

                //NEW_TOR
                if (this.LicenseTypeCode != null)
                {
                    var licenseChk = LicenseExp.Where(li => li.Contains(LicenseTypeCode)).FirstOrDefault();
                    if (licenseChk != null)
                    {
                        DTO.ResponseService<DTO.TrainPersonHistory[]> resExp = biz.GetTrainingHistoryByIDWithCondition(this.UserProfile.IdCard, this.LicenseTypeCode);
                        if (resExp.DataResponse.Count() > 0)
                        {
                            d.PAY_EXPIRE = resExp.DataResponse[0].TRAIN_DATE_EXP;
                        }

                    }
                }

                if (this.PersonLicenseH == null)
                {
                    lsHead.Add(h);
                    lsDetail.Add(d);
                    this.PersonLicenseH = lsHead;
                    this.PersonLicenseD = lsDetail;

                    //Set Current License For Validation
                    this.CurrentPersonLicenseH = h;
                    this.CurrentPersonLicenseD = d;

                }
                else
                {

                    if (this.PettionTypeCode == "16")
                    {
                        if ((lsHead.Where(a => a.LICENSE_TYPE_CODE == "03").FirstOrDefault() == null) && (this.LicenseTypeCode != "04"))
                        {
                            lsHead.Add(h);
                            lsDetail.Add(d);
                            this.PersonLicenseH = lsHead;
                            this.PersonLicenseD = lsDetail;

                            //Set Current License For Validation
                            this.CurrentPersonLicenseH = h;
                            this.CurrentPersonLicenseD = d;

                        }
                        else if ((lsHead.Where(a => a.LICENSE_TYPE_CODE == "04").FirstOrDefault() == null) && (this.LicenseTypeCode != "03"))
                        {
                            lsHead.Add(h);
                            lsDetail.Add(d);
                            this.PersonLicenseH = lsHead;
                            this.PersonLicenseD = lsDetail;

                            //Set Current License For Validation
                            this.CurrentPersonLicenseH = h;
                            this.CurrentPersonLicenseD = d;
                        }
                        else
                        {
                            //Set Current License For Validation
                            this.CurrentPersonLicenseH = h;
                            this.CurrentPersonLicenseD = d;

                        }
                    }
                }

            }

        }

        public DTO.ResponseService<DTO.PersonLicenseTransaction[]> GenLicenseTransaction()
        {
            PersonLicenseTransaction ent = new PersonLicenseTransaction();

            List<DTO.PersonLicenseTransaction> list = new List<DTO.PersonLicenseTransaction>();
            BLL.LicenseBiz biz = new BLL.LicenseBiz();
            DTO.ResponseService<DTO.PersonLicenseTransaction[]> res = biz.GetLicenseTransaction(PersonLicenseH.ToArray(), PersonLicenseD.ToArray());

            return res;

        }

        public DTO.ResponseService<DTO.PersonLicenseTransaction[]> GetPaymentLicenseTransaction()
        {
            PersonLicenseTransaction ent = new PersonLicenseTransaction();

            List<DTO.PersonLicenseTransaction> list = new List<DTO.PersonLicenseTransaction>();
            BLL.LicenseBiz biz = new BLL.LicenseBiz();

            DTO.ResponseService<DTO.PersonLicenseTransaction[]> res = biz.GetPaymentLicenseTransaction(PersonLicenseH.ToArray(), PersonLicenseD.ToArray());

            return res;

        }

        /// <summary>
        /// InsertLicense แก้ไข
        /// </summary>
        /// <returns></returns>
        public DTO.ResponseMessage<bool> InsertLicense()
        {
            DTO.ResponseMessage<bool> res = new ResponseMessage<bool>();
            LicenseBiz biz = new LicenseBiz();
            List<DTO.AttatchFileLicense> attachFilesLicense = new List<AttatchFileLicense>();
            PaymentBiz bizPay = new PaymentBiz();
            //add new Attachfiles List

            foreach (IAS.BLL.AttachFilesIAS.AttachFile lic in AttachFiles)
            {
                DTO.AttatchFileLicense ent = new AttatchFileLicense
                {
                    ID_ATTACH_FILE = lic.REGISTRATION_ID,
                    ID_CARD_NO = lic.ID_CARD_NO,
                    ATTACH_FILE_TYPE = lic.ATTACH_FILE_TYPE,
                    ATTACH_FILE_PATH = lic.ATTACH_FILE_PATH,
                    REMARK = lic.REMARK,
                    CREATED_BY = lic.CREATED_BY,
                    CREATED_DATE = lic.CREATED_DATE,
                    UPDATED_BY = lic.UPDATED_BY,
                    UPDATED_DATE = lic.UPDATED_DATE,
                    FILE_STATUS = lic.FILE_STATUS,
                    LICENSE_NO = lic.LICENSE_NO,
                    RENEW_TIME = lic.RENEW_TIME,
                    GROUP_LICENSE_ID = lic.GROUP_LICENSE_ID,

                };

                attachFilesLicense.Add(ent);
            }

            //Check APPROVED_DOC & APPROVED='W' > NEW_LICNESE || APPROVED_DOC & APPROVED='Y' > RENEW_LICNESE
            foreach (DTO.PersonLicenseDetail chkdetail in PersonLicenseD)
            {
                List<DTO.PersonLicenseHead> lsHeads = new List<PersonLicenseHead>();
                List<DTO.PersonLicenseDetail> lsDetails = new List<PersonLicenseDetail>();
                string approveDoc = PersonLicenseH.Where(c => c.UPLOAD_GROUP_NO == chkdetail.UPLOAD_GROUP_NO).FirstOrDefault().APPROVED_DOC;

                //APPROVED_DOC = "W" > NEW_LICNESE
                if (approveDoc == "W")
                {
                    //Get Head
                    DTO.PersonLicenseHead head = PersonLicenseH.Where(c => c.UPLOAD_GROUP_NO == chkdetail.UPLOAD_GROUP_NO).FirstOrDefault();
                    lsDetails.Add(chkdetail);
                    lsHeads.Add(head);

                    //Insert LICENSE_D & LICENSE_H
                    DTO.ResponseMessage<bool> resInsertLicense = biz.InsertPersonLicense(lsHeads.ToArray(), lsDetails.ToArray(), this.UserProfile, attachFilesLicense.ToArray());
                    if (resInsertLicense.ResultMessage == true)
                    {

	                    #region bizPay.GetSubGroup & SetSubGroup in to AG_IAS_SUBPAYMENT_D_T
                        //List<DTO.SubGroupPayment> data = new List<DTO.SubGroupPayment>();
                        ////string StartPaidSubDate = string.Format("{0:dd/MM/yyyy}", DateTime.Now);
                        ////string EndPaidSubDate = string.Format("{0:dd/MM/yyyy}", DateTime.Now);
                        ////var resGetSubGno = bizPay.GetSubGroup(PettionTypeCode, Convert.ToDateTime(StartPaidSubDate), Convert.ToDateTime(EndPaidSubDate), UserProfile, 1, 10);
                        ////DataSet ds = resGetSubGno.DataResponse;
                        ////DataTable dt = ds.Tables[0];

                        //////Get & Set SubGroupPayment
                        //data.Add(new DTO.SubGroupPayment
                        //{
                        //    uploadG = chkdetail.UPLOAD_GROUP_NO,
                        //    LicenseNo = chkdetail.LICENSE_NO,
                        //    RenewTime = chkdetail.RENEW_TIMES,
                        //    seqNo = chkdetail.SEQ_NO,
                        //    PaymentType = head.PETITION_TYPE_CODE
                        //});
                        //if (data != null)
                        //{
                        //    var bizPayment = new BLL.PaymentBiz();

                        //    var result = bizPayment.SetSubGroup(data.ToArray(), this.UserProfile.Id, this.UserProfile.CompCode, this.UserProfile.MemberType.ToString());

                        //    if (result.IsError)
                        //    {



                        //    }
                        //}
                        ////Insert SUBPAYMENT_D_T && H_T
                        //if (data != null)
                        //{
                        //    List<DTO.SubGroupPayment> ls = new List<DTO.SubGroupPayment>();

                        //    if (data.Count > 0)
                        //    {
                        //        for (int i = 0; i < data.Count; i++)
                        //        {
                        //            //Get SubGroupPayment
                        //            DTO.SubGroupPayment ent = new DTO.SubGroupPayment();
                        //            ent.uploadG = data[i].uploadG;
                        //            ent.seqNo = data[i].seqNo;
                        //            ent.LicenseNo = data[i].LicenseNo;
                        //            ent.RenewTime = data[i].RenewTime;
                        //            ent.PaymentType = data[i].PaymentType;
                        //            ls.Add(ent);

                        //            //string upLoadBySession = PersonLicenseH.Where(c => c.UPLOAD_GROUP_NO == data[i].uploadG).FirstOrDefault().UPLOAD_BY_SESSION;
                        //            ResponseMessage<bool> resPay = bizPay.SetSubGroup(ls.ToArray(), UserProfile.Id, head.UPLOAD_BY_SESSION, UserProfile.MemberType.ToString());
                        //            if ((resPay.IsError) || (resPay.ResultMessage == false))
                        //            {
                        //                UCLicenseModelError.ShowMessageError = resPay.ErrorMsg.ToString();
                        //                UCLicenseModelError.ShowModalError();
                        //                res = resPay;
                        //            }
                        //            else if (resPay.ResultMessage == true)
                        //            {
                        //                UCLicenseModelSuccess.ShowMessageSuccess = SysMessage.SaveSucess;
                        //                UCLicenseModelSuccess.ShowModalSuccess();
                        //                res = resPay;
                        //            }
                        //        }


                        //    }

                        //}
                        #endregion

                        res = resInsertLicense;
                    }
                    else if ((resInsertLicense.ResultMessage == false) || (resInsertLicense.IsError))
                    {

                        res = resInsertLicense;
                    }

                }
                //APPROVED_DOC = "Y" > RENEW_LICNESE
                else if (approveDoc == "Y")
                {
                    
                    //Get Head
                    DTO.PersonLicenseHead head = PersonLicenseH.Where(c => c.UPLOAD_GROUP_NO == chkdetail.UPLOAD_GROUP_NO).FirstOrDefault();
                    lsDetails.Add(chkdetail);
                    lsHeads.Add(head);

                    string Upload = head.UPLOAD_GROUP_NO;
                    DTO.ResponseService<DTO.PersonLicenseTransaction> CheckupladgroupNo = biz.GetLicenseDetailByUploadGroupNo(Upload);
                    if ((CheckupladgroupNo.DataResponse != null) && (!CheckupladgroupNo.IsError))
                    {
                        DTO.ResponseMessage<bool> resUpdateRenew = biz.updateRenewLicense(head, chkdetail);

                        if ((resUpdateRenew.IsError) || (resUpdateRenew.ResultMessage == false))
                        {
                            UCLicenseUCLicenseModelError.ShowMessageError = resUpdateRenew.ErrorMsg.ToString();
                            UCLicenseUCLicenseModelError.ShowModalError();
                            res = resUpdateRenew;
                        }
                        else if (resUpdateRenew.ResultMessage == true)
                        {
                            UCLicenseUCLicenseModelSuccess.ShowMessageSuccess = SysMessage.SaveSucess;
                            UCLicenseUCLicenseModelSuccess.ShowModalSuccess();
                            res = resUpdateRenew;
                        }

                    }
                    else
                    {
                          DTO.ResponseMessage<bool> resInsertLicense = biz.InsertPersonLicense(lsHeads.ToArray(), lsDetails.ToArray(), this.UserProfile, attachFilesLicense.ToArray());
                          if (resInsertLicense.ResultMessage == true)
                          {
                              res = resInsertLicense;
                          }
                          else if ((resInsertLicense.ResultMessage == false) || (resInsertLicense.IsError))
                          {

                              res = resInsertLicense;
                          }
                    }
             
                }


            }
            

            return res;
        }

        public ResponseMessage<bool> LicenseValidation()
        {
            LicenseBiz biz = new LicenseBiz();
            ResponseMessage<bool> res = new ResponseMessage<bool>();

            if ((PersonLicenseH != null) && (PersonLicenseD != null))
            {
                //NewLicense
                if (this.Menu.Equals(((int)DTO.PersonLicenses.New)))
                {
                    ResponseMessage<bool> result = biz.SingleLicenseValidation(this.CurrentPersonLicenseH, this.CurrentPersonLicenseD);
                    if ((result.IsError) || (result.ResultMessage == false))
                    {
                        UCLicenseUCLicenseModelError.ShowMessageError = result.ErrorMsg.ToString();
                        UCLicenseUCLicenseModelError.ShowModalError();
                        res = result;
                    }
                    else if (result.ResultMessage == true)
                    {
                        res = result;

                    }

                }
                //Renew License
                else if (this.Menu.Equals(((int)DTO.PersonLicenses.ReNew)))
                {
                    ResponseMessage<bool> result = biz.SingleLicenseValidation(this.CurrentPersonLicenseH, this.CurrentPersonLicenseD);
                    if ((result.IsError) || (result.ResultMessage == false))
                    {
                        UCLicenseUCLicenseModelError.ShowMessageError = result.ErrorMsg.ToString();
                        UCLicenseUCLicenseModelError.ShowModalError();
                        res = result;
                    }
                    else if (result.ResultMessage == true)
                    {
                        res = result;

                    }
                }
                //Expire License
                else if (this.Menu.Equals(((int)DTO.PersonLicenses.ExpireReNew)))
                {
                    ResponseMessage<bool> result = biz.SingleLicenseValidation(this.CurrentPersonLicenseH, this.CurrentPersonLicenseD);
                    if ((result.IsError) || (result.ResultMessage == false))
                    {
                        UCLicenseUCLicenseModelError.ShowMessageError = result.ErrorMsg.ToString();
                        UCLicenseUCLicenseModelError.ShowModalError();
                        res = result;
                    }
                    else if (result.ResultMessage == true)
                    {
                        res = result;

                    }

                }
                //ใบแทนใบอนุญาต (ชำรุดสูญหาย) Allow all Licnese Type
                else if (this.Menu.Equals(((int)DTO.PersonLicenses.Other)))
                {
                    //ไม่ต้องตรวจสอบ ผลการอบรม และผลการสมัคร เนื่องจากเป็นการขอใบแทน เคยได้รับใบอนุญาตนั้นๆมาแล้ว
                    ResponseMessage<bool> result = new ResponseMessage<bool>();
                    if ((PersonLicenseH != null) && (PersonLicenseH.Count > 0) && (PersonLicenseD != null) && (PersonLicenseD.Count > 0))
                    {
                        result.ResultMessage = true;
                        res = result;

                    }
                    else if (result.ResultMessage == true)
                    {
                        result.ResultMessage = false;
                        result.ErrorMsg = SysMessage.LicenseNull;
                        res = result;
                    }
                }

                //return res;

            }
            else
            {
                UCLicenseUCLicenseModelError.ShowMessageError = SysMessage.LicenseNull;
                UCLicenseUCLicenseModelError.ShowModalError();
                res.ResultMessage = false;

            }

            return res;

        }

        public ResponseMessage<bool> InitArgReport()
        {
            ResponseMessage<bool> resArg = new ResponseMessage<bool>();

            ReportDocument rpt = new ReportDocument();
            PersonBiz biz = new PersonBiz();
            var res = biz.GetById(this.UserProfile.Id);
            var ls = new List<PersonLicenseAgreement>();

            if ((!res.IsError) && (res.DataResponse != null))
            {
                PersonLicenseAgreement ent = new PersonLicenseAgreement();
                ent.SEX = res.DataResponse.SEX;
                ent.NAMES = res.DataResponse.NAMES;
                ent.LASTNAME = res.DataResponse.LASTNAME;
                ent.MEMBER_TYPE = res.DataResponse.MEMBER_TYPE;
                ls.Add(ent);
            }

            if (this.Menu.Equals((int)DTO.MenuLicenses.Step1))
            {
                rpt.Load(Server.MapPath("~/Reports/" + "RptAgreement_1.rpt"));
                rpt.SetDataSource(ls);
                //rpt.SetDataSource(new[] { res });
                //rpt.ExportToDisk(ExportFormatType.PortableDocFormat, Server.MapPath(this.ArgPath + this.ArgOutPutFile[1]));
                rpt.ExportToDisk(ExportFormatType.PortableDocFormat, Server.MapPath(this.ArgPath + UserProfile.Id + "_" + this.Menu + "_" + this.ArgOutPutFile[0]));

                this.LicensePath = Server.MapPath(this.ArgPath + UserProfile.Id + "_" + this.Menu + "_" + this.ArgOutPutFile[0]);

                resArg.ResultMessage = true;

            }
            else if (this.Menu.Equals((int)DTO.MenuLicenses.Step2))
            {
                rpt.Load(Server.MapPath("~/Reports/" + "RptAgreement_2.rpt"));
                rpt.SetDataSource(ls);
                //rpt.SetDataSource(new[] { res });
                //rpt.ExportToDisk(ExportFormatType.PortableDocFormat, Server.MapPath(this.ArgPath + this.ArgOutPutFile[1]));
                rpt.ExportToDisk(ExportFormatType.PortableDocFormat, Server.MapPath(this.ArgPath + UserProfile.Id + "_" + this.Menu + "_" + this.ArgOutPutFile[1]));

                this.LicensePath = Server.MapPath(this.ArgPath + UserProfile.Id + "_" + this.Menu + "_" + this.ArgOutPutFile[1]);

                resArg.ResultMessage = true;

            }
            else if (this.Menu.Equals((int)DTO.MenuLicenses.Step3))
            {
                rpt.Load(Server.MapPath("~/Reports/" + "RptAgreement_3.rpt"));
                rpt.SetDataSource(ls);
                //rpt.SetDataSource(new[] { res });
                //rpt.ExportToDisk(ExportFormatType.PortableDocFormat, Server.MapPath(this.ArgPath + this.ArgOutPutFile[2]));
                rpt.ExportToDisk(ExportFormatType.PortableDocFormat, Server.MapPath(this.ArgPath + UserProfile.Id + "_" + this.Menu + "_" + this.ArgOutPutFile[2]));

                this.LicensePath = Server.MapPath(this.ArgPath + UserProfile.Id + "_" + this.Menu + "_" + this.ArgOutPutFile[2]);

                resArg.ResultMessage = true;

            }

            //BindReport(rpt);
            return resArg;
        }

        public byte[] NullSignature_Img(string ImgPath)
        {
            byte[] buffer = null;
            Stream fileStream = new FileStream(Server.MapPath(ImgPath), FileMode.Open);
            buffer = new Byte[fileStream.Length + 1];
            BinaryReader br = new BinaryReader(fileStream);
            buffer = br.ReadBytes(Convert.ToInt32((fileStream.Length)));
            br.Close();
            return buffer;
        }

        public ResponseService<string> InitialReport()
        {
            var res = new ResponseService<string>();
            try
            {   var ls = new List<PersonLicenseAgreement>();
                var ReportName = string.Empty;
                var OutputName = string.Empty;
                string strMenu = this.Request.QueryString["M"];
                PersonBiz biz = new PersonBiz();
                PaymentBiz Pbiz = new PaymentBiz();
                string  pPic = @"Temp\NO_IMG.jpg";

                ReportDocument rpt = new ReportDocument();

                DTO.ResponseService<DataSet> resid = new ResponseService<DataSet>();

                //code by Chalermwut 10/09/2557
                if (strMenu == "2")
                {
                    resid = biz.GetDataRenewReport(this.UserProfile.IdCard, this.LicenseTypeCode, this.SelectedLicenseNo);
                }
                else
                {
                     resid = biz.GetDataTo8Report(this.UserProfile.IdCard, this.LicenseTypeCode);
                }

                //Get Doctype for personalImg Validation
                string message = SysMessage.DefaultSelecting;
                BLL.DataCenterBiz Docbiz = new BLL.DataCenterBiz();
                List<DTO.DataItem> Doctypels = Docbiz.GetDocumentType(message);
                var GetpPicc =new IAS.BLL.AttachFilesIAS.AttachFile();
                var resDoc = from A in Doctypels
                          from B in this.AttachFiles
                          where A.Id == B.ATTACH_FILE_TYPE
                          select new DTO.DataItem
                          {
                              Id = A.Id,
                              Name = A.Name
                          };
                if (resDoc.Count() > 0)
                {
                    if (resDoc.Where(n => n.Name.Contains("รูปถ่าย") || n.Name.Contains("ถ่าย") || n.Name.Contains("ภาพถ่าย")).Count() > 0)
                    {
                        DTO.DataItem picDoc = resDoc.Where(n => n.Name.Contains("รูปถ่าย") || n.Name.Contains("ถ่าย") || n.Name.Contains("ภาพถ่าย")).FirstOrDefault();
                        GetpPicc = this.AttachFiles.Where(f => f.ATTACH_FILE_TYPE == picDoc.Id).FirstOrDefault();

                    }
                }


                var pPicc = this.AttachFiles.Where(f => f.ATTACH_FILE_TYPE == GetpPicc.ATTACH_FILE_TYPE).FirstOrDefault();
                if ( pPicc != null)//รูปถ่าย?
                {
                    
                    var pic_path = this.AttachFiles
                                          .Where(f => f.ATTACH_FILE_TYPE == GetpPicc.ATTACH_FILE_TYPE)
                                          .FirstOrDefault();

                    if (pic_path != null)
                    {
                        if ((pic_path.EXTENSION.ToString().ToLower() == ".bmp") || (pic_path.EXTENSION.ToString().ToLower() == ".gif")
                            || (pic_path.EXTENSION.ToString().ToLower() == ".jpg") || (pic_path.EXTENSION.ToString().ToLower() == ".png")
                            || (pic_path.EXTENSION.ToString().ToLower() == ".tif") || (pic_path.EXTENSION.ToString().ToLower() == ".jpeg"))
                        {
                            
                            pPic = pic_path.ATTACH_FILE_PATH.ToString();    
                           
                        }
                       
                    }
                    
                    
                }
                
                byte[] Getbyte =  GetImage(pPic);
               
               

                #region This code by milk

                if ((this.LicenseTypeCode == "01") || (this.LicenseTypeCode == "07"))//ตัวแทนชีวิต
                {
                    if (strMenu == "2")
                    {
                       #region sendData2Report
                        if ((!resid.IsError) && (resid.DataResponse.Tables[0].Rows.Count != 0))
                        {
                            DataRow DTR = resid.DataResponse.Tables[0].Rows[0];
                            PersonLicenseAgreement ent = new PersonLicenseAgreement();
                            ent.SEX = " ";
                            ent.NAMES = (DTR["NAMES"] != null) ? DTR["NAMES"].ToString() : "          -           ";
                            ent.LASTNAME = (DTR["LASTNAME"] != null) ? DTR["LASTNAME"].ToString() : "        -             ";
                            ent.MEMBER_TYPE = " ";
                            if (DTR["COM_NAME"].ToString().StartsWith("บริษัท"))                     
                            {
                                ent.COMP_NAME = (DTR["COM_NAME"] != null) ? DTR["COM_NAME"].ToString().Replace("บริษัท", "") : "          -           ";
                            }
                            else
                            {
                                ent.COMP_NAME = (DTR["COM_NAME"] != null) ? DTR["COM_NAME"].ToString() : "       -              ";
                            }
                            ent.ID_CARD_NO = this.UserProfile.IdCard;
                            ent.LICENSE_NO = (DTR["LICENSE_NO"] != null) ? DTR["LICENSE_NO"].ToString() : "     -       ";
                            ent.RENEW_TIMES = (DTR["RENEW_TIME"] != null) ?  Convert.ToString(Convert.ToInt16(DTR["RENEW_TIME"])+1) : " -  ";
                            ent.EMAIL = (DTR["EMAIL"] != null) ? DTR["EMAIL"].ToString() : "           -          ";
                            ent.temp1 = (DTR["NOW_T"] != null) ? DTR["NOW_T"].ToString() : "          -           ";
                            ent.temp2 = (DTR["NOW_A"] != null) ? DTR["NOW_A"].ToString() : "         -            ";
                            ent.temp3 = (DTR["NOW_P"] != null) ? DTR["NOW_P"].ToString() : "           -          ";
                            ent.temp4 = (DTR["NOW_Z"] != null) ? DTR["NOW_Z"].ToString() : "         -            ";
                            ent.temp5 = (DTR["TEL"] != null) ? DTR["TEL"].ToString() : "          -           ";
                            ent.temp6 = (DTR["MOBILE"] != null) ? DTR["MOBILE"].ToString() : "      -               ";
                            ent.temp7 = "        -             ";
                            ent.temp8 = "        -             ";

                            ent.temp9 = "  ";
                            if (resDoc.Count() > 0)
                            {
                                if (resDoc.Where(n => n.Name.Contains("ใบอนุญาตเป็นตัวแทนประกันชีวิต") || (n.Name.Contains("ใบอนุญาต") && n.Name.Contains("ตัวแทน") && n.Name.Contains("ประกันชีวิต"))).Count() > 0)
                                {
                                    ent.temp9 = "✔";
                                }
                            }

                            ent.temp10 = "  ";
                            if (resDoc.Count() > 0)
                            {
                                if (resDoc.Where(n => n.Name.Contains("สำเนาทะเบียนบ้าน") || n.Name.Contains("ทะเบียนบ้าน")).Count() > 0)
                                {
                                    ent.temp10 = "✔";
                                }
                            }

                            ent.temp11 = " ";
                            ent.temp12 = " ";
                            ent.temp13 = " ";
                            ent.temp14 = this.LicenseTypeCode == "07" ? "✔" : "  ";//ขอต่อ ตัวแทน ชีวิต รายย่อย
                            ent.temp15 = this.LicenseTypeCode == "01" ? "✔" : "  ";//ขอต่อ ตัวแทน ชีวิต 
                            ent.temp16 = Convert.ToString(DateTime.Now.Day);
                            ent.temp17 = Convert_month.Num_to_full_string(Convert.ToInt16(DateTime.Now.Month));
                            ent.temp18 = Convert.ToString(Convert.ToInt16(DateTime.Now.Year) + 543);
                            ent.TITLE_NAME = (DTR["TITLE"] != null) ? DTR["TITLE"].ToString() : "      ";

                            #region address

                            if (DTR["NOW_ADDRESS"] != null)
                            {
                                string Address = DTR["NOW_ADDRESS"].ToString();

                                ent.CURRENT_ADDRESS_1 = GetAddress(Address, "เลขที่");
                                if (ent.CURRENT_ADDRESS_1 == null) ent.CURRENT_ADDRESS_1 = GetAddress(Address, "");

                                ent.Moo1 = GetAddress(Address, "หมู่ที่");
                                if (ent.Moo1 == null) ent.Moo1 = GetAddress(Address, "ม.");

                                ent.Soi1 = GetAddress(Address, "ซอย");
                                if (ent.Soi1 == null) ent.Soi1 = GetAddress(Address, "ซ.");

                                ent.Rood1 = GetAddress(Address, "ถนน");
                                if (ent.Rood1 == null) ent.Rood1 = GetAddress(Address, "ถ.");
                            }
                            if (ent.CURRENT_ADDRESS_1 == null) ent.CURRENT_ADDRESS_1 = "       -              ";
                            if (ent.Moo1 == null) ent.Moo1 = "          -           ";
                            if (ent.Soi1 == null) ent.Soi1 = "          -           ";
                            if (ent.Rood1 == null) ent.Rood1 = "        -             ";
                            #endregion address

                            ent.tempDate = Convert.ToString(DateTime.Now.Day + "  เดือน " + Convert_month.Num_to_full_string(Convert.ToInt16(DateTime.Now.Month)) + "  ปี " + (Convert.ToInt16(DateTime.Now.Year) + 543));
                            ent.IMG_pic = (Getbyte.Length > 0) ? Getbyte : new byte[0];
                            ls.Add(ent);
                        }

                        #endregion
                        ReportName = "RptAgreement_1.rpt";
                        OutputName = this.ArgOutPutFile[0];
                    }
                    else
                    {
                        #region sendData2Report
                        if ((!resid.IsError) && (resid.DataResponse.Tables[0].Rows.Count != 0))
                        {
                            DataRow DTR = resid.DataResponse.Tables[0].Rows[0];
                            PersonLicenseAgreement ent = new PersonLicenseAgreement();
                            ent.NAMES = (DTR["NAMES"] != null) ? DTR["NAMES"].ToString() : "          -           ";
                            ent.LASTNAME = (DTR["LASTNAME"] != null) ? DTR["LASTNAME"].ToString() : "         -            ";
                            ent.MEMBER_TYPE = " ";
                            
                            if (DTR["COM_NAME"].ToString().StartsWith("บริษัท"))
                            {
                                ent.COMP_NAME = (DTR["COM_NAME"] != null) ? DTR["COM_NAME"].ToString().Replace("บริษัท", "") : "      -               ";
                            }
                            else
                            {
                                ent.COMP_NAME = (DTR["COM_NAME"] != null) ? DTR["COM_NAME"].ToString() : "        -             ";
                            }
                            
                            ent.ID_CARD_NO = this.UserProfile.IdCard;
                            ent.LICENSE_NO = (DTR["LICENSE_NO"] != null) ? DTR["LICENSE_NO"].ToString() : "     -          ";
                            ent.RENEW_TIMES = (DTR["RENEW_TIME"] != null) ? Convert.ToString(Convert.ToInt16(DTR["RENEW_TIME"]) + 1) : "   -     ";
                            ent.EMAIL = (DTR["EMAIL"] != null) ? DTR["EMAIL"].ToString() : "                     ";

                            ent.temp1 = strMenu == "1" ? "✔" : " "; //ขอใหม่
                            ent.temp2 = strMenu == "3" ? "✔" : " "; //ขาดต่อขอใหม่
                            ent.temp5 = (strMenu == "6"  || strMenu == "4") ? "✔" :  " ";  // ใบแทน
                            ent.temp6 = Convert.ToString(DateTime.Now.Day);
                            ent.temp7 = Convert_month.Num_to_full_string(Convert.ToInt16(DateTime.Now.Month));
                            ent.temp8 = Convert.ToString(Convert.ToInt16(DateTime.Now.Year) + 543);

                            ent.temp9 = this.LicenseTypeCode == "01" ? "✔" : " ";
                            ent.temp10 = this.LicenseTypeCode == "07" ? "✔" : " ";

                            ent.temp11 = (DTR["LOCAL_T"] != null) ? DTR["LOCAL_T"].ToString() : "        -             ";
                            ent.temp12 = (DTR["LOCAL_A"] != null) ? DTR["LOCAL_A"].ToString() : "         -            ";
                            ent.temp13 = (DTR["LOCAL_P"] != null) ? DTR["LOCAL_P"].ToString() : "        -             ";
                            ent.temp14 = (DTR["LOCAL_Z"] != null) ? DTR["LOCAL_Z"].ToString() : "        -             ";

                            ent.temp15 = (DTR["NOW_T"] != null) ? DTR["NOW_T"].ToString() : "        -             ";
                            ent.temp16 = (DTR["NOW_A"] != null) ? DTR["NOW_A"].ToString() : "         -            ";
                            ent.temp17 = (DTR["NOW_P"] != null) ? DTR["NOW_P"].ToString() : "        -             ";
                            ent.temp18 = (DTR["NOW_Z"] != null) ? DTR["NOW_Z"].ToString() : "        -             ";
                            ent.temp19 = (DTR["TEL"] != null) ? DTR["TEL"].ToString() : "            -         ";
                            ent.temp20 = (DTR["MOBILE"] != null) ? DTR["MOBILE"].ToString() : "        -             ";

                            string edu = (DTR["EDU_CODE"] != null) ? DTR["EDU_CODE"].ToString() : "";
                            ent.temp3 =  edu != "" ? (Convert.ToInt16(edu) < 5 ? "✔" : " ") : " ";
                            ent.temp4 = edu != "" ? (edu == "05" ? "✔" : " ") : " ";
                            ent.SEX = edu != "" ? (edu == "06" ? "✔" : " ") : " ";

                            ent.ADDRESS_1 = "                   -                        ";//แทน บ.เก่าที่1
                            ent.ADDRESS_2 = "                   -                        ";//แทน บ.เก่าที่2


                            #region address

                            if (DTR["LOCAL_ADDRESS"] != null)
                            {
                                string Address = DTR["LOCAL_ADDRESS"].ToString();

                                ent.CURRENT_ADDRESS_1 = GetAddress(Address, "เลขที่");
                                if (ent.CURRENT_ADDRESS_1 == null) ent.CURRENT_ADDRESS_1 = GetAddress(Address, "");

                                ent.Moo2 = GetAddress(Address, "หมู่ที่");
                                if (ent.Moo2 == null) ent.Moo2 = GetAddress(Address, "ม.");

                                ent.Soi2 = GetAddress(Address, "ซอย");
                                if (ent.Soi2 == null) ent.Soi2 = GetAddress(Address, "ซ.");

                                ent.Rood2 = GetAddress(Address, "ถนน");
                                if (ent.Rood2 == null) ent.Rood2 = GetAddress(Address, "ถ.");
                            }

                            if (ent.CURRENT_ADDRESS_1 == null) ent.CURRENT_ADDRESS_1 = "           -          ";
                            if (ent.Moo2 == null) ent.Moo2 = "          -           ";
                            if (ent.Soi2 == null) ent.Soi2 = "           -          ";
                            if (ent.Rood2 == null) ent.Rood2 = "          -           ";

                            if (DTR["NOW_ADDRESS"] != null)
                            {
                                string Address = DTR["NOW_ADDRESS"].ToString();

                                ent.CURRENT_ADDRESS_2 = GetAddress(Address, "เลขที่");
                                if (ent.CURRENT_ADDRESS_2 == null) ent.CURRENT_ADDRESS_2 = GetAddress(Address, "");

                                ent.Moo1 = GetAddress(Address, "หมู่ที่");
                                if (ent.Moo1 == null) ent.Moo1 = GetAddress(Address, "ม.");

                                ent.Soi1 = GetAddress(Address, "ซอย");
                                if (ent.Soi1 == null) ent.Soi1 = GetAddress(Address, "ซ.");

                                ent.Rood1 = GetAddress(Address, "ถนน");
                                if (ent.Rood1 == null) ent.Rood1 = GetAddress(Address, "ถ.");
                            }
                            if (ent.CURRENT_ADDRESS_2 == null) ent.CURRENT_ADDRESS_2 = "           -          ";
                            if (ent.Moo1 == null) ent.Moo1 = "          -           ";
                            if (ent.Soi1 == null) ent.Soi1 = "          -           ";
                            if (ent.Rood1 == null) ent.Rood1 = "        -             ";
                            #endregion address


                            ent.TITLE_NAME = (DTR["TITLE"] != null) ? DTR["TITLE"].ToString() : "       ";
                            ent.tempDate = Convert.ToString(DateTime.Now.Day + "  เดือน " + Convert_month.Num_to_full_string(Convert.ToInt16(DateTime.Now.Month)) + "  ปี " + (Convert.ToInt16(DateTime.Now.Year) + 543));
                            ent.IMG_pic = (Getbyte.Length > 0) ? Getbyte : new byte[0];
                            ls.Add(ent);
                        }

                        #endregion
                        ReportName = "RptAgreement_5.rpt";
                        OutputName = this.ArgOutPutFile[4];
                    }
                }
                else if ((this.LicenseTypeCode == "02") || (this.LicenseTypeCode == "05") || (this.LicenseTypeCode == "06") || (this.LicenseTypeCode == "08"))//ตัวแทนวินาศ
                {
                    if (strMenu == "2")
                    {
                        #region sendData2Report
                        if ((!resid.IsError) && (resid.DataResponse.Tables[0].Rows.Count != 0))
                        {
                            DataRow DTR = resid.DataResponse.Tables[0].Rows[0];
                            PersonLicenseAgreement ent = new PersonLicenseAgreement();
                            ent.SEX = " ";
                            ent.NAMES = (DTR["NAMES"] != null) ?  DTR["NAMES"].ToString() : "       -              ";
                            ent.LASTNAME = (DTR["LASTNAME"] != null) ? DTR["LASTNAME"].ToString() : "        -             ";
                            ent.MEMBER_TYPE = " ";
                            if (DTR["COM_NAME"].ToString().StartsWith("บริษัท"))
                            {
                                ent.COMP_NAME = (DTR["COM_NAME"] != null) ? DTR["COM_NAME"].ToString().Replace("บริษัท", "") : "        -             ";
                            }
                            else
                            {
                                ent.COMP_NAME = (DTR["COM_NAME"] != null) ? DTR["COM_NAME"].ToString() : "         -            ";
                            }
                            ent.ID_CARD_NO = this.UserProfile.IdCard;
                            ent.LICENSE_NO = (DTR["LICENSE_NO"] != null) ? DTR["LICENSE_NO"].ToString() : "          -           ";
                            ent.RENEW_TIMES = (DTR["RENEW_TIME"] != null) ? Convert.ToString(DTR["RENEW_TIME"].ToInt() + 1) : "  -   ";
                            ent.EMAIL = (DTR["EMAIL"] != null) ? DTR["EMAIL"].ToString() : "          -           ";
                            ent.temp1 = (DTR["NOW_T"] != null) ? DTR["NOW_T"].ToString() : "           -          ";
                            ent.temp2 = (DTR["NOW_A"] != null) ? DTR["NOW_A"].ToString() : "          -           ";
                            ent.temp3 = (DTR["NOW_P"] != null) ? DTR["NOW_P"].ToString() : "          -           ";
                            ent.temp4 = (DTR["NOW_Z"] != null) ? DTR["NOW_Z"].ToString() : "           -          ";
                            ent.temp5 = (DTR["TEL"] != null) ? DTR["TEL"].ToString() : "           -          ";
                            ent.temp6 = (DTR["MOBILE"] != null) ? DTR["MOBILE"].ToString() : "     -               ";
                            ent.temp7 = this.LicenseTypeCode == "02" ? "✔" : " ";//ขอต่อ ตัวแทน วินาศ
                            ent.temp8 = this.LicenseTypeCode == "05" ? "✔" : " ";//ขอต่อ ตัวแทน วินาศ สุขภาพ+อุบัติเหตุ
                            ent.temp9 = this.LicenseTypeCode == "06" ? "✔" : " ";//ขอต่อ ตัวแทน วินาศ พรบ.
                            ent.temp10 = this.LicenseTypeCode == "08" ? "✔" : " ";//ขอต่อ ตัวแทน วินาศ รายย่อย

                            ent.temp11 = " ";
                            if (resDoc.Count() > 0)
                            {
                                if (resDoc.Where(n => n.Name.Contains("ใบอนุญาตเป็นตัวแทนประกันชีวิต") || (n.Name.Contains("ใบอนุญาต") && n.Name.Contains("ตัวแทน") && n.Name.Contains("ประกันชีวิต"))).Count() > 0)
                                {
                                    ent.temp11 = "✔";
                                }
                            }

                            ent.temp12 = " ";
                            if (resDoc.Count() > 0)
                            {
                                if (resDoc.Where(n => n.Name.Contains("สำเนาทะเบียนบ้าน") || n.Name.Contains("ทะเบียนบ้าน")).Count() > 0)
                                {
                                    ent.temp12 = "✔";
                                }
                            }

                            ent.temp13 = " ";
                            ent.temp14 = Convert_month.Num_to_full_string(Convert.ToInt16(DateTime.Now.Month));
                            ent.temp15 = Convert.ToString(Convert.ToInt16(DateTime.Now.Year) + 543);
                            ent.temp16 = Convert.ToString(DateTime.Now.Day);
                            ent.TITLE_NAME = (DTR["TITLE"] != null) ? DTR["TITLE"].ToString() : "        ";

                            #region address

                            //if (DTR["LOCAL_ADDRESS"] != null)
                            //{
                            //    string Address = DTR["LOCAL_ADDRESS"].ToString();

                            //    ent.ADDRESS_1 = GetAddress(Address, "เลขที่");
                            //    if (ent.ADDRESS_1 == null) ent.ADDRESS_1 = GetAddress(Address, "");

                            //    ent.Moo2 = GetAddress(Address, "หมู่ที่");
                            //    if (ent.Moo2 == null) ent.Moo2 = GetAddress(Address, "ม.");

                            //    ent.Soi2 = GetAddress(Address, "ซอย");
                            //    if (ent.Soi2 == null) ent.Soi2 = GetAddress(Address, "ซ.");

                            //    ent.Rood2 = GetAddress(Address, "ถนน");
                            //    if (ent.Rood2 == null) ent.Rood2 = GetAddress(Address, "ถ.");
                            //}
                            //if (ent.Moo2 == null) ent.Moo2 = "          -           ";
                            //if (ent.Soi2 == null) ent.Soi2 = "           -          ";
                            //if (ent.Rood2 == null) ent.Rood2 = "          -           ";


                            if (DTR["NOW_ADDRESS"] != null)
                            {
                                string Address = DTR["NOW_ADDRESS"].ToString();

                                ent.CURRENT_ADDRESS_1 = GetAddress(Address, "เลขที่");
                                if (ent.CURRENT_ADDRESS_1 == null) ent.CURRENT_ADDRESS_1 = GetAddress(Address, "");

                                ent.Moo1 = GetAddress(Address, "หมู่ที่");
                                if (ent.Moo1 == null) ent.Moo1 = GetAddress(Address, "ม.");

                                ent.Soi1 = GetAddress(Address, "ซอย");
                                if (ent.Soi1 == null) ent.Soi1 = GetAddress(Address, "ซ.");

                                ent.Rood1 = GetAddress(Address, "ถนน");
                                if (ent.Rood1 == null) ent.Rood1 = GetAddress(Address, "ถ.");
                            }
                            if (ent.CURRENT_ADDRESS_1 == null) ent.CURRENT_ADDRESS_1 = "                     ";
                            if (ent.Moo1 == null) ent.Moo1 = "          -           ";
                            if (ent.Soi1 == null) ent.Soi1 = "          -           ";
                            if (ent.Rood1 == null) ent.Rood1 = "        -             ";
                            #endregion address

                            ent.tempDate = Convert.ToString(DateTime.Now.Day + "  เดือน " + Convert_month.Num_to_full_string(Convert.ToInt16(DateTime.Now.Month)) + "  ปี " + (Convert.ToInt16(DateTime.Now.Year) + 543));
                            ent.IMG_pic = (Getbyte.Length > 0) ? Getbyte : new byte[0];
                            ls.Add(ent);
                        }

                        #endregion
                        ReportName = "RptAgreement_2.rpt";
                        OutputName = this.ArgOutPutFile[1];
                    }
                    else
                    {
                        #region sendData2Report
                        if ((!resid.IsError) && (resid.DataResponse.Tables[0].Rows.Count != 0))
                        {
                            DataRow DTR = resid.DataResponse.Tables[0].Rows[0];
                            PersonLicenseAgreement ent = new PersonLicenseAgreement();

                            ent.ID_CARD_NO = this.UserProfile.IdCard;

                            ent.temp1 = strMenu == "3" ? "✔" : " ";//ขาดต่อ
                            ent.temp2 = strMenu == "1" ? "✔" : " ";//ขอใหม่
                            ent.temp3 = (strMenu == "6"  || strMenu == "4") ? "✔" : " "; //ใบแทน

                            ent.temp4 = Convert.ToString(DateTime.Now.Day);
                            ent.temp5 = Convert_month.Num_to_full_string(Convert.ToInt16(DateTime.Now.Month));
                            ent.temp6 = Convert.ToString(Convert.ToInt16(DateTime.Now.Year) + 543);

                            ent.temp7 = this.LicenseTypeCode == "02"? "✔" : " ";
                            ent.temp8=this.LicenseTypeCode == "05"? "✔" : " ";
                            ent.temp9=this.LicenseTypeCode == "06"? "✔" : " ";
                            ent.temp10 = this.LicenseTypeCode == "08" ? "✔" : " ";


                            ent.EMAIL = (DTR["EMAIL"] != null) ? DTR["EMAIL"].ToString() : "         -            ";
                            ent.NAMES = (DTR["NAMES"] != null) ? DTR["NAMES"].ToString() : "          -           ";
                            ent.LASTNAME = (DTR["LASTNAME"] != null) ? DTR["LASTNAME"].ToString() : "        -             ";

                            if (DTR["COM_NAME"].ToString().StartsWith("บริษัท"))
                            {
                                ent.COMP_NAME = (DTR["COM_NAME"] != null) ? DTR["COM_NAME"].ToString().Replace("บริษัท", "") : "                  -                        ";
                            }
                            else
                            {
                                ent.COMP_NAME = (DTR["COM_NAME"] != null) ? DTR["COM_NAME"].ToString() : "        -             ";
                            }


                            ent.temp11 = (DTR["LOCAL_T"] != null) ? DTR["LOCAL_T"].ToString() : "         -            ";
                            ent.temp12 = (DTR["LOCAL_A"] != null) ? DTR["LOCAL_A"].ToString() : "          -           ";
                            ent.temp13 = (DTR["LOCAL_P"] != null) ? DTR["LOCAL_P"].ToString() : "                     ";
                            ent.temp14 = (DTR["LOCAL_Z"] != null) ? DTR["LOCAL_Z"].ToString() : "        -             ";

                            ent.temp15 = (DTR["NOW_T"] != null) ? DTR["NOW_T"].ToString() : "         -            ";
                            ent.temp16 = (DTR["NOW_A"] != null) ? DTR["NOW_A"].ToString() : "         -            ";
                            ent.temp17 = (DTR["NOW_P"] != null) ? DTR["NOW_P"].ToString() : "         -            ";
                            ent.temp18 = (DTR["NOW_Z"] != null) ? DTR["NOW_Z"].ToString() : "         -            ";
                            ent.temp19 = (DTR["TEL"] != null) ? DTR["TEL"].ToString() : "          -           ";
                            ent.temp20 = (DTR["MOBILE"] != null) ? DTR["MOBILE"].ToString() : "    -     ";

                            string edu = (DTR["EDU_CODE"] != null) ? DTR["EDU_CODE"].ToString() : "";
                            ent.PRE_NAME_CODE = edu != "" ? (Convert.ToInt16(edu) < 5 ? "✔" : " ") : " ";//เอามาใช้แทนการศึกษาชั่วคราว
                            ent.HEAD_REQUEST_NO = edu != "" ? (edu == "05" ? "✔" : " ") : " ";//เอามาใช้แทนการศึกษาชั่วคราว
                            ent.GROUP_REQUEST_NO = edu != "" ? (edu == "06" ? "✔" : " ") : " ";//เอามาใช้แทนการศึกษาชั่วคราว

                            ent.ADDRESS_1 = "";//แทน บ.เก่าที่1
                            ent.ADDRESS_2 = "";//แทน บ.เก่าที่2

                            #region address

                            if (DTR["LOCAL_ADDRESS"] != null)
                            {
                                string Address = DTR["LOCAL_ADDRESS"].ToString();

                                ent.CURRENT_ADDRESS_1 = GetAddress(Address, "เลขที่");
                                if (ent.CURRENT_ADDRESS_1 == null) ent.CURRENT_ADDRESS_1 = GetAddress(Address, "");

                                ent.Moo2 = GetAddress(Address, "หมู่ที่");
                                if (ent.Moo2 == null) ent.Moo2 = GetAddress(Address, "ม.");

                                ent.Soi2 = GetAddress(Address, "ซอย");
                                if (ent.Soi2 == null) ent.Soi2 = GetAddress(Address, "ซ.");

                                ent.Rood2 = GetAddress(Address, "ถนน");
                                if (ent.Rood2 == null) ent.Rood2 = GetAddress(Address, "ถ.");
                            }
                            if (ent.CURRENT_ADDRESS_1 == null) ent.CURRENT_ADDRESS_1 = "         -            ";
                            if (ent.Moo2 == null) ent.Moo2 = "          -           ";
                            if (ent.Soi2 == null) ent.Soi2 = "           -          ";
                            if (ent.Rood2 == null) ent.Rood2 = "          -           ";


                            if (DTR["NOW_ADDRESS"] != null)
                            {
                                string Address = DTR["NOW_ADDRESS"].ToString();

                                ent.CURRENT_ADDRESS_2 = GetAddress(Address, "เลขที่");
                                if (ent.CURRENT_ADDRESS_2 == null) ent.CURRENT_ADDRESS_2 = GetAddress(Address, "");

                                ent.Moo1 = GetAddress(Address, "หมู่ที่");
                                if (ent.Moo1 == null) ent.Moo1 = GetAddress(Address, "ม.");

                                ent.Soi1 = GetAddress(Address, "ซอย");
                                if (ent.Soi1 == null) ent.Soi1 = GetAddress(Address, "ซ.");

                                ent.Rood1 = GetAddress(Address, "ถนน");
                                if (ent.Rood1 == null) ent.Rood1 = GetAddress(Address, "ถ.");
                            }
                            if (ent.CURRENT_ADDRESS_2 == null) ent.CURRENT_ADDRESS_2 = "         -            ";
                            if (ent.Moo1 == null) ent.Moo1 = "          -           ";
                            if (ent.Soi1 == null) ent.Soi1 = "          -           ";
                            if (ent.Rood1 == null) ent.Rood1 = "        -             ";
                            #endregion address
                            ent.TITLE_NAME = (DTR["TITLE"] != null) ? DTR["TITLE"].ToString() : "      ";
                            ent.tempDate = Convert.ToString(DateTime.Now.Day + "  เดือน " + Convert_month.Num_to_full_string(Convert.ToInt16(DateTime.Now.Month)) + "  ปี " + (Convert.ToInt16(DateTime.Now.Year) + 543));
                            ent.IMG_pic = (Getbyte.Length > 0) ? Getbyte : new byte[0];
                            ls.Add(ent);
                        }

                        #endregion
                        ReportName = "RptAgreement_6.rpt";
                        OutputName = this.ArgOutPutFile[5];
                    }

                }
                else if (this.LicenseTypeCode == "03")//จัดการ(นายหน้า)ชีวิต
                {
                    if (strMenu == "2")
                    {
                        #region sendData2Report
                        if ((!resid.IsError) && (resid.DataResponse.Tables[0].Rows.Count != 0))
                        {
                            DataRow DTR = resid.DataResponse.Tables[0].Rows[0];
                            PersonLicenseAgreement ent = new PersonLicenseAgreement();
                            ent.SEX = " ";
                            ent.NAMES = (DTR["NAMES"] != null) ? DTR["NAMES"].ToString() : "          -           ";
                            ent.LASTNAME = (DTR["LASTNAME"] != null) ? DTR["LASTNAME"].ToString() : "        -             ";
                            ent.MEMBER_TYPE = " ";
                            if (DTR["COM_NAME"].ToString().StartsWith("บริษัท"))
                            {
                                ent.COMP_NAME = (DTR["COM_NAME"] != null) ? DTR["COM_NAME"].ToString().Replace("บริษัท", "") : "            -         ";
                            }
                            else
                            {
                                ent.COMP_NAME = (DTR["COM_NAME"] != null) ? DTR["COM_NAME"].ToString() : "        -             ";
                            }
                            ent.ID_CARD_NO = this.UserProfile.IdCard;

                            ent.LICENSE_NO = (DTR["LICENSE_NO"] != null) ? DTR["LICENSE_NO"].ToString() : "          -           ";
                            ent.RENEW_TIMES = (DTR["RENEW_TIME"] != null) ? Convert.ToString(DTR["RENEW_TIME"].ToInt() + 1) : "  -   ";
                            //old Code
                            //LicenseBiz Lbiz = new LicenseBiz();
                            //var Lres = Lbiz.GetAllLicenseByIDCard(this.UserProfile.IdCard, "1", 1);
                            //if (Lres != null)
                            //{
                            //    ent.LICENSE_NO = (Lres.DataResponse.FirstOrDefault().LICENSE_NO != null) ? Lres.DataResponse.FirstOrDefault().LICENSE_NO.ToString() : "      -           ";
                            //    ent.RENEW_TIMES = Lres.DataResponse.FirstOrDefault().RENEW_TIME.ToString() ;

                            //}
                            //else
                            //{
                            //    ent.LICENSE_NO = "      -           ";
                            //    ent.RENEW_TIMES = "       -         ";

                            //}
                            ent.EMAIL = (DTR["EMAIL"] != null) ? DTR["EMAIL"].ToString() : "         -            ";
                            ent.temp1 = (DTR["NOW_T"] != null) ? DTR["NOW_T"].ToString() : "        -             ";
                            ent.temp2 = (DTR["NOW_A"] != null) ? DTR["NOW_A"].ToString() : "         -            ";
                            ent.temp3 = (DTR["NOW_P"] != null) ? DTR["NOW_P"].ToString() : "         -            ";
                            ent.temp4 = (DTR["NOW_Z"] != null) ? DTR["NOW_Z"].ToString() : "          -           ";
                            ent.temp5 = (DTR["TEL"] != null) ? DTR["TEL"].ToString() : "   -     ";
                            ent.temp6 = (DTR["MOBILE"] != null) ? DTR["MOBILE"].ToString() : "   -     ";
                            ent.temp7 = "✔";
                            ent.temp8 = " ";
                            ent.temp9 = " ";
                            ent.temp10 = " ";
                            ent.temp11 = " ";
                            ent.temp12 = " ";
                            ent.temp13 = " ";
                            ent.temp14 = " ";
                            ent.temp15 = " ";
                            ent.temp16 = Convert.ToString(DateTime.Now.Day);
                            ent.temp17 = Convert_month.Num_to_full_string(Convert.ToInt16(DateTime.Now.Month));
                            ent.temp18 = Convert.ToString(Convert.ToInt16(DateTime.Now.Year) + 543);
                            ent.TITLE_NAME = (DTR["TITLE"] != null) ? DTR["TITLE"].ToString() : "       ";
                            ent.tempDate = Convert.ToString(DateTime.Now.Day + "  เดือน " + Convert_month.Num_to_full_string(Convert.ToInt16(DateTime.Now.Month)) + "  ปี " + (Convert.ToInt16(DateTime.Now.Year) + 543));

                            ent.temp19 = " ";
                            if (resDoc.Count() > 0)
                            {
                                if (resDoc.Where(n => n.Name.Contains("ใบอนุญาตเป็นนายหน้าประกันชีวิต") || (n.Name.Contains("ใบอนุญาต") && n.Name.Contains("นายหน้า") && n.Name.Contains("ชีวิต"))).Count() > 0)
                                {
                                    ent.temp19 = "✔";
                                }
                            }

                            ent.temp20 = " ";
                            if (resDoc.Count() > 0)
                            {
                                if (resDoc.Where(n => n.Name.Contains("สำเนาทะเบียนบ้าน") || n.Name.Contains("ทะเบียนบ้าน")).Count() > 0)
                                {
                                    ent.temp20 = "✔";
                                }
                            }

                            #region address

   

                            if (DTR["NOW_ADDRESS"] != null)
                            {
                                string Address = DTR["NOW_ADDRESS"].ToString();

                                ent.CURRENT_ADDRESS_1 = GetAddress(Address, "เลขที่");
                                if (ent.CURRENT_ADDRESS_1 == null) ent.CURRENT_ADDRESS_1 = GetAddress(Address, "");

                                ent.Moo1 = GetAddress(Address, "หมู่ที่");
                                if (ent.Moo1 == null) ent.Moo1 = GetAddress(Address, "ม.");

                                ent.Soi1 = GetAddress(Address, "ซอย");
                                if (ent.Soi1 == null) ent.Soi1 = GetAddress(Address, "ซ.");

                                ent.Rood1 = GetAddress(Address, "ถนน");
                                if (ent.Rood1 == null) ent.Rood1 = GetAddress(Address, "ถ.");
                            }
                            if (ent.CURRENT_ADDRESS_1 == null) ent.CURRENT_ADDRESS_1 = "         -            ";
                            if (ent.Moo1 == null) ent.Moo1 = "          -           ";
                            if (ent.Soi1 == null) ent.Soi1 = "          -           ";
                            if (ent.Rood1 == null) ent.Rood1 = "        -             ";
                            #endregion address


                            ent.IMG_pic = (Getbyte.Length > 0) ? Getbyte : new byte[0];
                            ls.Add(ent);
                        }

                        #endregion
                        ReportName = "RptAgreement_3.rpt"; 
                        OutputName = this.ArgOutPutFile[2];
                    }
                    else
                    {
                        #region sendData2Report
                        if ((!resid.IsError) && (resid.DataResponse.Tables[0].Rows.Count != 0))
                        {
                            DataRow DTR = resid.DataResponse.Tables[0].Rows[0];
                            PersonLicenseAgreement ent = new PersonLicenseAgreement();
                            ent.SEX = " ";
                            ent.NAMES = (DTR["NAMES"] != null) ? DTR["NAMES"].ToString() : "        -             ";
                            ent.LASTNAME = (DTR["LASTNAME"] != null) ? DTR["LASTNAME"].ToString() : "          -           ";
                            ent.MEMBER_TYPE = " ";
                            ent.COMP_NAME = " ";
                            ent.ID_CARD_NO = this.UserProfile.IdCard;
                            ent.LICENSE_NO = " ";
                            ent.RENEW_TIMES = " ";
                            ent.EMAIL = (DTR["EMAIL"] != null) ? DTR["EMAIL"].ToString() : "        -             ";
                            ent.temp1 = (DTR["LOCAL_T"] != null) ? DTR["LOCAL_T"].ToString() : "        -             ";
                            ent.temp10 = (DTR["TEL"] != null) ? "  " +DTR["TEL"].ToString() : "      -          ";
                            ent.temp11 = (DTR["MOBILE"] != null) ? "  " +DTR["MOBILE"].ToString() : "        -         ";
                            string edu = (DTR["EDU_CODE"] != null) ? DTR["EDU_CODE"].ToString() : "";
                            ent.temp12 = edu != "" ? (Convert.ToInt16(edu) < 5 ? "✔" : " ") : " ";
                            ent.temp13 = edu != "" ? (edu == "05" ? "✔" : " ") : " ";
                            ent.temp14 = edu != "" ? (edu == "06" ? "✔" : " ") : " ";
                            ent.temp15 = strMenu == "3" ? "✔" : " ";//กากบาทตรงขาดต่อขอรับใบอนุญาตใหม่
                            ent.temp16 = strMenu == "1" ? "✔" : " ";//กากบาทตรงขอรับครั้งแรก
                            ent.temp2 = (DTR["LOCAL_A"] != null) ? "  " +DTR["LOCAL_A"].ToString() : "        -             ";
                            ent.temp3 = (DTR["LOCAL_P"] != null) ? "  " +DTR["LOCAL_P"].ToString() : "        -             ";
                            ent.temp4 = (DTR["LOCAL_Z"] != null) ? "  " +DTR["LOCAL_Z"].ToString() : "         -            ";
                            ent.temp5 = (strMenu == "6" || strMenu == "4") ? "✔" : " ";//กากบาทตรงใบแทนใบอนุญาต
                            ent.temp6 = (DTR["NOW_T"] != null) ? "  " +DTR["NOW_T"].ToString() : "          -           ";
                            ent.temp7 = (DTR["NOW_A"] != null) ? "  " +DTR["NOW_A"].ToString() : "        -             ";
                            ent.temp8 = (DTR["NOW_P"] != null) ? "  " +DTR["NOW_P"].ToString() : "          -           ";
                            ent.temp9 = (DTR["NOW_Z"] != null) ? "  " +DTR["NOW_Z"].ToString() : "          -           ";
                            ent.temp17 = Convert_month.Num_to_full_string(Convert.ToInt16(DateTime.Now.Month));
                            ent.temp18 = Convert.ToString(Convert.ToInt16(DateTime.Now.Year) + 543);
                            #region address
                           
                            if (DTR["LOCAL_ADDRESS"] != null)
                            {
                                string Address = DTR["LOCAL_ADDRESS"].ToString();

                                ent.ADDRESS_1 = GetAddress(Address, "เลขที่");
                                if (ent.ADDRESS_1 == null) ent.ADDRESS_1 = GetAddress(Address, "");      

                                ent.Moo2 = GetAddress(Address, "หมู่ที่");
                                if (ent.Moo2 == null) ent.Moo2 = GetAddress(Address, "ม.");                               

                                ent.Soi2 = GetAddress(Address, "ซอย");
                                if (ent.Soi2 == null) ent.Soi2 = GetAddress(Address, "ซ.");                                

                                ent.Rood2 = GetAddress(Address, "ถนน");
                                if (ent.Rood2 == null) ent.Rood2 = GetAddress(Address, "ถ.");                                
                            }
                            if (ent.ADDRESS_1 == null) ent.ADDRESS_1 = "         -            ";
                            if (ent.Moo2 == null) ent.Moo2 = "          -           ";
                            if (ent.Soi2 == null) ent.Soi2 = "           -          ";
                            if (ent.Rood2 == null) ent.Rood2 = "          -           ";

                           
                            if(DTR["NOW_ADDRESS"] != null) 
                            {
                                string Address = DTR["NOW_ADDRESS"].ToString();

                                ent.CURRENT_ADDRESS_1 = GetAddress(Address,"เลขที่");
                                if (ent.CURRENT_ADDRESS_1 == null) ent.CURRENT_ADDRESS_1 = GetAddress(Address, "");  

                                ent.Moo1 = GetAddress(Address,"หมู่ที่");
                                if (ent.Moo1 == null) ent.Moo1 =  GetAddress(Address,"ม.");                                

                                ent.Soi1 = GetAddress(Address, "ซอย");
                                if (ent.Soi1 == null) ent.Soi1 = GetAddress(Address, "ซ.");                                

                                ent.Rood1 = GetAddress(Address, "ถนน");
                                if (ent.Rood1 == null) ent.Rood1 = GetAddress(Address, "ถ.");                                
                            }
                            if (ent.CURRENT_ADDRESS_1 == null) ent.CURRENT_ADDRESS_1 = "        -             ";
                            if (ent.Moo1 == null) ent.Moo1 = "          -           ";
                            if (ent.Soi1 == null) ent.Soi1 = "          -           ";
                            if (ent.Rood1 == null) ent.Rood1 = "        -             ";                            
                            #endregion address
                            ent.TITLE_NAME = (DTR["TITLE"] != null) ? "  " +DTR["TITLE"].ToString() : "         ";
                            ent.tempDate = Convert.ToString(DateTime.Now.Day);
                            ent.IMG_pic = (Getbyte.Length > 0) ? Getbyte : new byte[0];

                            ls.Add(ent);
                        }

                        #endregion

                        ReportName = "RptAgreement_7.rpt";//แก้แล้ว รอcheck
                        OutputName = this.ArgOutPutFile[6];
                    }
                }
                else if (this.LicenseTypeCode == "04")//จัดการ(นายหน้า)วินาศ
                {
                    if (strMenu == "2")
                    {
                        #region sendData2Report
                        if ((!resid.IsError) && (resid.DataResponse.Tables[0].Rows.Count != 0))
                        {
                            DataRow DTR = resid.DataResponse.Tables[0].Rows[0];
                            PersonLicenseAgreement ent = new PersonLicenseAgreement();
                            ent.SEX = " ";
                            ent.NAMES = (DTR["NAMES"] != null) ? DTR["NAMES"].ToString() : "          -           ";
                            ent.LASTNAME = (DTR["LASTNAME"] != null) ? DTR["LASTNAME"].ToString() : "        -             ";
                            ent.MEMBER_TYPE = " ";
                            if (DTR["COM_NAME"].ToString().StartsWith("บริษัท"))
                            {
                                ent.COMP_NAME = (DTR["COM_NAME"] != null) ? DTR["COM_NAME"].ToString().Replace("บริษัท", "") : "        -             ";
                            }
                            else
                            {
                                ent.COMP_NAME = (DTR["COM_NAME"] != null) ? DTR["COM_NAME"].ToString() : "       -              ";
                            }
                            ent.ID_CARD_NO = this.UserProfile.IdCard;

                            ent.LICENSE_NO = (DTR["LICENSE_NO"] != null) ? DTR["LICENSE_NO"].ToString() : "          -           ";
                            ent.RENEW_TIMES = (DTR["RENEW_TIME"] != null) ? Convert.ToString(DTR["RENEW_TIME"].ToInt() + 1) : "  -   ";

                            //old code
                              //LicenseBiz Lbiz = new LicenseBiz();
                              // var Lres = Lbiz.GetAllLicenseByIDCard(this.UserProfile.IdCard, "1", 1);
                              // if (Lres != null)
                              //{
                              //     ent.LICENSE_NO =


                                //ent.LICENSE_NO = (Lres.DataResponse.FirstOrDefault().LICENSE_NO != null) ? Lres.DataResponse.FirstOrDefault().LICENSE_NO.ToString() : "      -           ";
                                //ent.RENEW_TIMES =   Convert.ToString(Convert.ToInt16(Lres.DataResponse.FirstOrDefault().RENEW_TIME)+1);

                              // }
                              //  else
                              // {
                              //   ent.LICENSE_NO = "      -           ";
                              //    ent.RENEW_TIMES = "       -         ";

                              // }

                            ent.EMAIL = (DTR["EMAIL"] != null) ? DTR["EMAIL"].ToString() : "         -            ";
                            ent.temp1 = (DTR["NOW_T"] != null) ? DTR["NOW_T"].ToString() : "         -            ";
                            ent.temp2 = (DTR["NOW_A"] != null) ? DTR["NOW_A"].ToString() : "          -           ";
                            ent.temp3 = (DTR["NOW_P"] != null) ? DTR["NOW_P"].ToString() : "         -            ";
                            ent.temp4 = (DTR["NOW_Z"] != null) ? DTR["NOW_Z"].ToString() : "          -           ";
                            ent.temp5 = (DTR["TEL"] != null) ? DTR["TEL"].ToString() : "   -      ";
                            ent.temp6 = (DTR["MOBILE"] != null) ? DTR["MOBILE"].ToString() : "         ";
                            ent.temp7 = "✔";
                            ent.temp8 = " ";

                            ent.temp9 = " ";
                            if (resDoc.Count() > 0)
                            {
                                if (resDoc.Where(n => n.Name.Contains("ใบอนุญาตเป็นนายหน้าประกันวินาศภัย") || (n.Name.Contains("ใบอนุญาต") && n.Name.Contains("นายหน้า") && n.Name.Contains("วินาศภัย"))).Count() > 0)
                                {
                                    ent.temp9 = "✔";
                                }
                            }

                            ent.temp10 = " ";
                            if (resDoc.Count() > 0)
                            {
                                if (resDoc.Where(n => n.Name.Contains("สำเนาทะเบียนบ้าน") || n.Name.Contains("ทะเบียนบ้าน")).Count() > 0)
                                {
                                    ent.temp10 = "✔";
                                }
                            }
                            ent.temp11 = " ";
                            ent.temp12 = " ";
                            ent.temp13 = " ";

                            #region address

                            if (DTR["NOW_ADDRESS"] != null)
                            {
                                string Address = DTR["NOW_ADDRESS"].ToString();

                                ent.CURRENT_ADDRESS_1 = GetAddress(Address, "เลขที่");
                                if (ent.CURRENT_ADDRESS_1 == null) ent.CURRENT_ADDRESS_1 = GetAddress(Address, "");

                                ent.Moo1 = GetAddress(Address, "หมู่ที่");
                                if (ent.Moo1 == null) ent.Moo1 = GetAddress(Address, "ม.");

                                ent.Soi1 = GetAddress(Address, "ซอย");
                                if (ent.Soi1 == null) ent.Soi1 = GetAddress(Address, "ซ.");

                                ent.Rood1 = GetAddress(Address, "ถนน");
                                if (ent.Rood1 == null) ent.Rood1 = GetAddress(Address, "ถ.");
                            }
                            if (ent.CURRENT_ADDRESS_1 == null) ent.CURRENT_ADDRESS_1 = "         -            ";
                            if (ent.Moo1 == null) ent.Moo1 = "          -           ";
                            if (ent.Soi1 == null) ent.Soi1 = "          -           ";
                            if (ent.Rood1 == null) ent.Rood1 = "        -             ";
                            #endregion address

                            ent.temp14 = Convert.ToString(Convert.ToInt16(DateTime.Now.Year) + 543);
                            ent.temp15 = Convert_month.Num_to_full_string(Convert.ToInt16(DateTime.Now.Month));
                            ent.temp16 = Convert.ToString(DateTime.Now.Day);
                            ent.TITLE_NAME = (DTR["TITLE"] != null) ? DTR["TITLE"].ToString() : "       ";
                            ent.tempDate = Convert.ToString(DateTime.Now.Day + "  เดือน " + Convert_month.Num_to_full_string(Convert.ToInt16(DateTime.Now.Month)) + "  ปี " + (Convert.ToInt16(DateTime.Now.Year) + 543));
                            ent.IMG_pic = (Getbyte.Length > 0) ? Getbyte : new byte[0];
                            ls.Add(ent);
                        }

                        #endregion
                        ReportName = "RptAgreement_4.rpt";
                        OutputName = this.ArgOutPutFile[3];
                    }
                    else
                    {
                        #region sendData2Report
                        if ((!resid.IsError) && (resid.DataResponse.Tables[0].Rows.Count != 0))
                        {
                            DataRow DTR = resid.DataResponse.Tables[0].Rows[0];
                            PersonLicenseAgreement ent = new PersonLicenseAgreement();
                            ent.SEX = " ";
                            ent.NAMES = (DTR["NAMES"] != null) ? DTR["NAMES"].ToString() : "         -           ";
                            ent.LASTNAME = (DTR["LASTNAME"] != null) ? DTR["LASTNAME"].ToString() : "       -              ";        
                            ent.MEMBER_TYPE = " ";
                            ent.COMP_NAME = " ";
                            ent.ID_CARD_NO = this.UserProfile.IdCard;
                            ent.LICENSE_NO = " ";
                            ent.RENEW_TIMES = " ";
                            ent.EMAIL = (DTR["EMAIL"] != null) ? DTR["EMAIL"].ToString() : "       -              ";
                            ent.temp1 = (DTR["LOCAL_T"] != null) ? DTR["LOCAL_T"].ToString() : "       -              ";
                            ent.temp9 = (DTR["TEL"] != null) ? DTR["TEL"].ToString() : "           -          ";
                            ent.temp10 = (DTR["MOBILE"] != null) ? DTR["MOBILE"].ToString() : "       -      ";
                            string edu = (DTR["EDU_CODE"] != null) ? DTR["EDU_CODE"].ToString() : "";
                            ent.temp11 = edu != "" ? (Convert.ToInt16(edu) < 5 ? "✔" : " ") : " ";
                            ent.temp12 = edu != "" ? (edu == "05" ? "✔" : " ") : " ";
                            ent.temp13 = edu != "" ? (edu == "06" ? "✔" : " ") : " ";
                            ent.temp15 = strMenu == "3" ? "✔" : " ";//กากบาทตรงขาดต่อขอรับใบอนุญาตใหม่
                            ent.temp16 = strMenu == "1" ? "✔" : " ";//กากบาทตรงขอรับครั้งแรก
                            ent.temp2 = (DTR["LOCAL_A"] != null) ? DTR["LOCAL_A"].ToString() : "         -            ";
                            ent.temp3 = (DTR["LOCAL_P"] != null) ? DTR["LOCAL_P"].ToString() : "         -            ";
                            ent.temp4 = (DTR["LOCAL_Z"] != null) ? DTR["LOCAL_Z"].ToString() : "         -            ";
                            ent.temp14 = (strMenu == "6" || strMenu == "4") ? "✔" :  " "; //กากบาทตรงใบแทนใบอนุญาต
                            ent.temp5 = (DTR["NOW_T"] != null) ? DTR["NOW_T"].ToString() : "          -           ";
                            ent.temp6 = (DTR["NOW_A"] != null) ? DTR["NOW_A"].ToString() : "         -            ";
                            ent.temp7 = (DTR["NOW_P"] != null) ? DTR["NOW_P"].ToString() : "                     ";
                            ent.temp8 = (DTR["NOW_Z"] != null) ? DTR["NOW_Z"].ToString() : "      -       ";
                            ent.temp17 = Convert_month.Num_to_full_string(Convert.ToInt16(DateTime.Now.Month));
                            ent.temp18 = Convert.ToString(Convert.ToInt16(DateTime.Now.Year) + 543);
                            ent.TITLE_NAME = (DTR["TITLE"] != null) ? DTR["TITLE"].ToString() : "          ";
                            ent.tempDate = Convert.ToString(DateTime.Now.Day);


                            #region address

                            if (DTR["LOCAL_ADDRESS"] != null)
                            {
                                string Address = DTR["LOCAL_ADDRESS"].ToString();

                                ent.ADDRESS_1 = GetAddress(Address, "เลขที่");
                                if (ent.ADDRESS_1 == null) ent.ADDRESS_1 = GetAddress(Address, "");

                                ent.Moo2 = GetAddress(Address, "หมู่ที่");
                                if (ent.Moo2 == null) ent.Moo2 = GetAddress(Address, "ม.");

                                ent.Soi2 = GetAddress(Address, "ซอย");
                                if (ent.Soi2 == null) ent.Soi2 = GetAddress(Address, "ซ.");

                                ent.Rood2 = GetAddress(Address, "ถนน");
                                if (ent.Rood2 == null) ent.Rood2 = GetAddress(Address, "ถ.");
                            }
                            if (ent.ADDRESS_1 == null) ent.ADDRESS_1 = "         -            ";
                            if (ent.Moo2 == null) ent.Moo2 = "          -           ";
                            if (ent.Soi2 == null) ent.Soi2 = "           -          ";
                            if (ent.Rood2 == null) ent.Rood2 = "          -           ";

                            if (DTR["NOW_ADDRESS"] != null)
                            {
                                string Address = DTR["NOW_ADDRESS"].ToString();

                                ent.CURRENT_ADDRESS_1 = GetAddress(Address, "เลขที่");
                                if (ent.CURRENT_ADDRESS_1 == null) ent.CURRENT_ADDRESS_1 = GetAddress(Address, "");

                                ent.Moo1 = GetAddress(Address, "หมู่ที่");
                                if (ent.Moo1 == null) ent.Moo1 = GetAddress(Address, "ม.");

                                ent.Soi1 = GetAddress(Address, "ซอย");
                                if (ent.Soi1 == null) ent.Soi1 = GetAddress(Address, "ซ.");

                                ent.Rood1 = GetAddress(Address, "ถนน");
                                if (ent.Rood1 == null) ent.Rood1 = GetAddress(Address, "ถ.");
                            }
                            if (ent.CURRENT_ADDRESS_1 == null) ent.CURRENT_ADDRESS_1 = "         -            ";
                            if (ent.Moo1 == null) ent.Moo1 = "          -           ";
                            if (ent.Soi1 == null) ent.Soi1 = "          -           ";
                            if (ent.Rood1 == null) ent.Rood1 = "        -             ";
                            #endregion address
                            ent.IMG_pic = (Getbyte.Length > 0) ? Getbyte : new byte[0];
                            ls.Add(ent);
                        }

                        #endregion

                        ReportName = "RptAgreement_8.rpt"; //check แล้ว
                        OutputName = this.ArgOutPutFile[7];
                    }
                }


                if ((ReportName != "") || (ReportName!=null))
                {
                    rpt.Load(Server.MapPath("~/Reports/" + ReportName));
                    rpt.SetDataSource(ls);
                    
                    //Create Dirctory if Exists
                    if(!Directory.Exists(Server.MapPath(this.ArgPath)))
                    {
                        Directory.CreateDirectory(Server.MapPath(this.ArgPath));
                    }

                    rpt.ExportToDisk(ExportFormatType.PortableDocFormat, Server.MapPath(this.ArgPath + UserProfile.Id + "_" + this.Menu + "_" + OutputName));

                    this.LicensePath = Server.MapPath(this.ArgPath + UserProfile.Id + "_" + this.Menu + "_" + OutputName);

                    res.DataResponse = this.ArgPath + UserProfile.Id + "_" + this.Menu + "_" + OutputName;

                }


                #endregion milk
                //res.DataResponse = "";
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }

            return res;

        }

        public string GetAddress(string Address,string find)
        {
            string ReAddress= null;
            try
            {
                if (Address.Contains(find))
                {
                   
                    string aa = Address.Substring(Address.IndexOf(find)).Trim();
                    int b = aa.IndexOf(" ",aa.IndexOf(find).ToInt()+1);
                    if (b >= 0)
                        ReAddress = aa.Substring(aa.IndexOf(find), aa.IndexOf(" ")).Trim();
                    else
                        ReAddress = aa.Substring(aa.IndexOf(find), aa.Length);
                    ReAddress = (ReAddress.IndexOf(find)>0)? ReAddress.Replace(find, ""):ReAddress;
                }
            }
            catch
            {                 
            }
            return ReAddress;
        }

        public byte[] GetImage(String hostPath){
            using (FileService.FileTransferServiceClient svc = new FileService.FileTransferServiceClient())
            {
                try
                {
                    DownloadFileResponse response = svc.DownloadFile(new DownloadFileRequest() { TargetContainer = "", TargetFileName = hostPath });



                    if (response.Code == "0000")
                    {
                        return Utils.ReadStreamToBytes.ReadFully(response.FileByteStream);
                    }
                    else{
                        return new byte[0];
                    }
                }
                catch (Exception ex)
                {
                    return new byte[0];
                }


            }
        }

        private string GetAreaCode(string pvCode, string apCode, string tCode)
        {
            string areC = string.Empty;
            StringBuilder st = new StringBuilder();

            if ((pvCode != "") && (apCode != "") && (tCode != ""))
            {
                st.Append(pvCode);
                st.Append(apCode);
                st.Append(tCode);
                areC = st.ToString();

            }

            return areC;

        }

        private string GetLicenseConfigStatus()
        {
            string Status = string.Empty;
            LicenseBiz biz = new LicenseBiz();
            try
            {
                DTO.ResponseService<DTO.ConfigDocument[]> res = biz.GetLicenseConfigByPetition(this.PettionTypeCode, this.LicenseTypeCode);

                if (res.DataResponse.Count() > 0)
                {
                    Status = res.DataResponse[0].STATUS;

                }
            }
            catch (Exception ex)
            {
                this.ucLicModelError.ShowMessageError = ex.Message;
                this.ucLicModelError.ShowModalError();
            }

            return Status;

        }

        public DTO.ResponseMessage<bool> ControlValidationBeforeSubmit(Page child)
        {
            DTO.ResponseMessage<bool> res = new DTO.ResponseMessage<bool>();
            StringBuilder strBuild = new StringBuilder();

            this.Page.Validate(this.LicenseGroupValidation);
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
                                if (strBuild.Length > 0)
                                {
                                    bool chkError = strBuild.ToString().Equals(child.Validators[i].ErrorMessage + "<br/>");
                                    if (chkError == false)
                                    {
                                        strBuild.Append(child.Validators[i].ErrorMessage + "<br/>");
                                    }
                                }
                                else
                                {
                                    strBuild.Append(child.Validators[i].ErrorMessage + "<br/>");
                                }
                                //var chkError = strBuild.ToString().Where(str => str.Equals(child.Validators[i].ErrorMessage)).FirstOrDefault();
                                //if (chkError == null)
                                //{
                                //    strBuild.Append(child.Validators[i].ErrorMessage + "<br/>");
                                //}
                                //strBuild.Append(child.Validators[i].ErrorMessage + "<br/>");

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

        private string GetDocStatus()
        {
            //Get Special Type
            DataCenterBiz bizz = new DataCenterBiz();
            string ApproveStatus = string.Empty;
            try
            {
                DTO.ResponseService<DTO.SpecialDocument[]> lsExamSpecialType = bizz.GetExamSpecialDocType("A", "Y",this.LICENSE_TYPE_CODE);
                var specialFile = (from A in this.AttachFiles
                                   join B in lsExamSpecialType.DataResponse.ToList() on A.ATTACH_FILE_TYPE equals B.DOCUMENT_CODE
                                   select new DTO.SpecialDocument
                                   {
                                       DOCUMENT_CODE = B.DOCUMENT_CODE,
                                       DOCUMENT_NAME = B.DOCUMENT_NAME,
                                       ID_ATTACH_FILE = A.REGISTRATION_ID,
                                       ID_CARD_NO = A.ID_CARD_NO,
                                       ATTACH_FILE_TYPE = A.ATTACH_FILE_TYPE,
                                       ATTACH_FILE_PATH = A.ATTACH_FILE_PATH,
                                       REMARK = A.REMARK,
                                       CREATED_BY = A.CREATED_BY,
                                       CREATED_DATE = A.CREATED_DATE,
                                       UPDATED_BY = A.UPDATED_BY,
                                       UPDATED_DATE = A.UPDATED_DATE,
                                       FILE_STATUS = A.FILE_STATUS,
                                       LICENSE_NO = A.LICENSE_NO,
                                       RENEW_TIME = A.RENEW_TIME,
                                       GROUP_LICENSE_ID = A.GROUP_LICENSE_ID

                                   }).ToList();

                if (specialFile.Count() > 0)
                {
                    ApproveStatus = DTO.ApprocLicense.W.ToString();
                }
                else
                {
                    ApproveStatus = DTO.ApprocLicense.Y.ToString();
                }

            }
            catch (Exception ex)
            {
                throw;
            }

            return ApproveStatus;
            
        }
        #endregion

        #region UI Function
        protected void btnBackLast_Click(object sender, EventArgs e)
        {
            if (((this.PersonLicenseH != null) && (this.PersonLicenseH.Count > 0)) 
                && ((this.PersonLicenseD != null) && (this.PersonLicenseD.Count > 0)) 
                && (this.Redo != null) && (this.Redo != ""))
            {
                this.Step = 5;
                Response.Redirect("../License/LicenseContinue.aspx?M=" + this.Menu + "&S=" + this.Step + "");
                //Response.Redirect("../License/LicenseContinue.aspx?M=2&S=5");
            }
            else
            {
                UCLicenseUCLicenseModelError.ShowMessageError = Resources.errorMasterLicense_001;
                UCLicenseUCLicenseModelError.ShowModalError();
                return;

            }
        }

        protected void btnloop_Click(object sender, ImageClickEventArgs e)
        {
            if (((this.PersonLicenseH != null) && (this.PersonLicenseH.Count > 0)) && ((this.PersonLicenseD != null) && (this.PersonLicenseD.Count > 0)))
            {
                if ((this.Redo != null) && (this.Redo != ""))
                {
                    if (this.Redo.Equals("1"))
                    {
                        this.Step = 5;
                        Response.Redirect("../License/LicenseContinue.aspx?M=" + this.Menu + "&S=" + this.Step + "");
                        //Response.Redirect("../License/LicenseContinue.aspx?M=2&S=5");
                    }
                    else
                    {
                        Session["CheckClearSession"] = "";
                    }
                }
                else
                {
                    UCLicenseUCLicenseModelError.ShowMessageError = Resources.errorMasterLicense_001;
                    UCLicenseUCLicenseModelError.ShowModalError();
                    return;
                }

            }
            else
            {
                UCLicenseUCLicenseModelError.ShowMessageError = Resources.errorMasterLicense_001;
                UCLicenseUCLicenseModelError.ShowModalError();
                return;

            }
        }
        #endregion
    }
}
