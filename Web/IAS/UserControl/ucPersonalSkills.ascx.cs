using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.BLL;
using IAS.MasterPage;
using IAS.Utils;
using IAS.Properties;
using System.Text;
using IAS.DTO;

namespace IAS.UserControl
{
    public partial class ucPersonalSkills : System.Web.UI.UserControl
    {
        #region Private & Public Session
        public MasterLicense MasterLicense
        {
            get
            {
                return (this.Page.Master as MasterLicense);
            }

        }

        private DTO.Person CurrentPersonalEntity
        {
            get
            {
                return (DTO.Person)Session["per"] == null ? null : (DTO.Person)Session["per"];
            }
            set
            {
                Session["per"] = value;
            }
        }

        public List<string> GroupStringList
        {
            get
            {
                return (List<string>)Session["GroupStringList"] == null ? null : (List<string>)Session["GroupStringList"];
            }
            set
            {
                Session["GroupStringList"] = value;
            }

        }

        public List<DTO.RegistrationAttatchFile> RegistrationAttachFileList
        {
            get
            {
                return (List<DTO.RegistrationAttatchFile>)Session["RegistrationAttachFileList"] == null ? null : (List<DTO.RegistrationAttatchFile>)Session["RegistrationAttachFileList"];
            }
            set
            {
                Session["RegistrationAttachFileList"] = value;
            }
        }

        public List<DTO.PersonLicenseTransaction> PersonalLicense
        {
            get
            {
                return (List<DTO.PersonLicenseTransaction>)Session["PersonalLicense"] == null ? null : (List<DTO.PersonLicenseTransaction>)Session["PersonalLicense"];
            }
            set
            {
                Session["PersonalLicense"] = value;
            }

        }

        public List<DTO.ValidateLicense> GridviewGeneral
        {
            get
            {
                return (List<DTO.ValidateLicense>)Session["GridviewGeneral"] == null ? null : (List<DTO.ValidateLicense>)Session["GridviewGeneral"];
            }
            set
            {
                Session["GridviewGeneral"] = value;
            }

        }

        public List<DTO.ValidateLicense> GridviewExamResult
        {
            get
            {
                return (List<DTO.ValidateLicense>)Session["GridviewExamResult"] == null ? null : (List<DTO.ValidateLicense>)Session["GridviewExamResult"];
            }
            set
            {
                Session["GridviewExamResult"] = value;
            }

        }

        public List<DTO.ValidateLicense> GridviewEducation
        {
            get
            {
                return (List<DTO.ValidateLicense>)Session["GridviewEducation"] == null ? null : (List<DTO.ValidateLicense>)Session["GridviewEducation"];
            }
            set
            {
                Session["GridviewEducation"] = value;
            }

        }

        public List<DTO.ValidateLicense> GridviewTrainResult
        {
            get
            {
                return (List<DTO.ValidateLicense>)Session["GridviewTrainResult"] == null ? null : (List<DTO.ValidateLicense>)Session["GridviewTrainResult"];
            }
            set
            {
                Session["GridviewTrainResult"] = value;
            }

        }

        public List<DTO.ValidateLicense> GridviewOther
        {
            get
            {
                return (List<DTO.ValidateLicense>)Session["GridviewOther"] == null ? null : (List<DTO.ValidateLicense>)Session["GridviewOther"];
            }
            set
            {
                Session["GridviewOther"] = value;
            }

        }

        public string CurrentIDCard
        {
            get
            {
                return Session["CurrentIDCard"] == null ? string.Empty : Session["CurrentIDCard"].ToString();
            }
            set
            {
                Session["CurrentIDCard"] = value;
            }
        }

        public string CurrentRegistrationID
        {
            get
            {
                return Session["CurrentRegistrationID"] == null ? string.Empty : Session["CurrentRegistrationID"].ToString();
            }
            set
            {
                Session["CurrentRegistrationID"] = value;
            }
        }

        public string LicenseTypeCode
        {
            get
            {
                return Session["LicenseTypeCode"] == null ? string.Empty : Session["LicenseTypeCode"].ToString();
            }
            set
            {
                Session["LicenseTypeCode"] = value;
            }
        }

        public string PettionTypeCode
        {
            get
            {
                return Session["PettionTypeCode"] == null ? string.Empty : Session["PettionTypeCode"].ToString();
            }
            set
            {
                Session["PettionTypeCode"] = value;
            }
        }

        public string CurrentLicenseRenewTime
        {
            get
            {
                return Session["CurrentLicenseRenewTime"] == null ? string.Empty : Session["CurrentLicenseRenewTime"].ToString();
            }
            set
            {
                Session["CurrentLicenseRenewTime"] = value;
            }
        }

        public Int32 GroupID
        {
            get
            {
                return (Int32)Session["GroupID"];
            }
            set
            {
                Session["GroupID"] = value;
            }

        }

        public string Mode
        {
            get
            {
                return Session["Mode"] == null ? string.Empty : Session["Mode"].ToString();
            }
            set
            {
                Session["Mode"] = value;
            }
        }

        public string curResultExam
        {
            get
            {
                return Session["curResultExam"] == null ? string.Empty : Session["curResultExam"].ToString();
            }
            set
            {
                Session["curResultExam"] = value;
            }
        }

        public string curResultTrain
        {
            get
            {
                return Session["curResultTrain"] == null ? string.Empty : Session["curResultTrain"].ToString();
            }
            set
            {
                Session["curResultTrain"] = value;
            }
        }

        public string curResultEducation
        {
            get
            {
                return Session["curResultEducation"] == null ? string.Empty : Session["curResultEducation"].ToString();
            }
            set
            {
                Session["curResultEducation"] = value;
            }
        }

        public string FeePayment
        {
            get
            {
                return Session["FeePayment"] == null ? string.Empty : Session["FeePayment"].ToString();
            }
            set
            {
                Session["FeePayment"] = value;
            }
        }

        public string Rule7
        {
            get
            {
                return Session["Rule7"] == null ? string.Empty : Session["Rule7"].ToString();
            }
            set
            {
                Session["Rule7"] = value;
            }
        }

        public string Rule8
        {
            get
            {
                return Session["Rule8"] == null ? string.Empty : Session["Rule8"].ToString();
            }
            set
            {
                Session["Rule8"] = value;
            }
        }

        public string Rule9
        {
            get
            {
                return Session["Rule9"] == null ? string.Empty : Session["Rule9"].ToString();
            }
            set
            {
                Session["Rule9"] = value;
            }
        }

        public string LicenseStage
        {
            get
            {
                return Session["LicenseStage"] == null ? string.Empty : Session["LicenseStage"].ToString();
            }
            set
            {
                Session["LicenseStage"] = value;
            }
        }

        public string AttachfileStage
        {
            get
            {
                return Session["AttachfileStage"] == null ? string.Empty : Session["AttachfileStage"].ToString();
            }
            set
            {
                Session["AttachfileStage"] = value;
            }
        }

        public string GetPersonalTrainExResult
        {
            get
            {
                return Session["GetPersonalTrainExResult"] == null ? string.Empty : Session["GetPersonalTrainExResult"].ToString();
            }
            set
            {
                Session["GetPersonalTrainExResult"] = value;
            }
        }


        public List<string> GridviewOtherList
        {
            get
            {
                if (Session["GridviewOtherList"] == null)
                {
                    Session["GridviewOtherList"] = new List<string>();
                }

                return (List<string>)Session["GridviewOtherList"];
            }

            set
            {
                Session["GridviewOtherList"] = value;
            }
        }
        public string CheckApprove
        {
            get
            {
                return Session["CheckApprove"] == null ? string.Empty : Session["CheckApprove"].ToString();
            }
            set
            {
                Session["CheckApprove"] = value;
            }
        }
        //private DTO.Person personal = new DTO.Person();
        #endregion

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //this.LicenseStage = "N";
                //this.AttachfileStage = "N";
                this.ucInit();
            }
            else
            {
                this.GetPersonalSkill(this.LicenseTypeCode, this.PettionTypeCode, this.CurrentLicenseRenewTime);
            }

            udpMain.Update();
        }
        #endregion

        #region Public & Private Function
        public void ucInit()
        {
            this.GetPersonalDetailByIDCard();
            this.GetPersoanlLicenseByIDCard();
            this.GetAttatchRegisterationFiles();
            this.GetSpecialAttachFiles();
            this.GetPersonalSkill(this.LicenseTypeCode, this.PettionTypeCode, this.CurrentLicenseRenewTime);


        }

        private void GetSpecialAttachFiles()
        {
            DataCenterBiz biz = new DataCenterBiz();


            if (this.MasterLicense.PettionTypeCode.Equals(PettionCode.NewLicense.GetEnumValue().ToString()))
            {
                //Get Special Type
                //DTO.ResponseService<DTO.SpecialDocument[]> lsExamSpecialType = biz.GetExamSpecialDocType("A", "Y");
                var lsExamSpecial = biz.GetExamSpecial(this.CurrentIDCard.Trim(), this.LicenseTypeCode);


                if (lsExamSpecial.DataResponse.Count() > 0)
                {
                    List<DTO.SpecialDocument> lsSpecial = new List<DTO.SpecialDocument>();

                    foreach (var entSp in lsExamSpecial.DataResponse)
                    {
                        DTO.SpecialDocument spList = new SpecialDocument();

                        spList.DOCUMENT_CODE = entSp.Id;
                        spList.DOCUMENT_NAME = entSp.Name;

                        lsSpecial.Add(spList);
                    }

                    gvExamSpecial.DataSource = lsSpecial;
                    gvExamSpecial.DataBind();
                }
                else
                {
                    DTO.ResponseService<DTO.SpecialDocument[]> lsExamSpecialType = biz.GetExamSpecialDocType("A", "Y", this.LicenseTypeCode);

                    if (this.MasterLicense != null)
                    {
                        var specialFile = (from A in this.MasterLicense.AttachFiles
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
                            gvExamSpecial.DataSource = specialFile;
                            gvExamSpecial.DataBind();
                        }
                    }
                }
            }
            //if (this.MasterLicense != null)
            //{


                    //var specialFile = (from A in this.MasterLicense.AttachFiles
            //                   join B in lsExamSpecialType.DataResponse.ToList() on A.ATTACH_FILE_TYPE equals B.DOCUMENT_CODE
            //                   select new DTO.SpecialDocument
            //                   {
            //                       DOCUMENT_CODE = B.DOCUMENT_CODE,
            //                       DOCUMENT_NAME = B.DOCUMENT_NAME,
            //                       ID_ATTACH_FILE = A.REGISTRATION_ID,
            //                       ID_CARD_NO = A.ID_CARD_NO,
            //                       ATTACH_FILE_TYPE = A.ATTACH_FILE_TYPE,
            //                       ATTACH_FILE_PATH = A.ATTACH_FILE_PATH,
            //                       REMARK = A.REMARK,
            //                       CREATED_BY = A.CREATED_BY,
            //                       CREATED_DATE = A.CREATED_DATE,
            //                       UPDATED_BY = A.UPDATED_BY,
            //                       UPDATED_DATE = A.UPDATED_DATE,
            //                       FILE_STATUS = A.FILE_STATUS,
            //                       LICENSE_NO = A.LICENSE_NO,
            //                       RENEW_TIME = A.RENEW_TIME,
            //                       GROUP_LICENSE_ID = A.GROUP_LICENSE_ID

                    //                   }).ToList();

                    //if (specialFile.Count() > 0)
            //{
            //    gvTrainSpecial.DataSource = specialFile;
            //    gvTrainSpecial.DataBind();
            //}


                //}
            else
            {
                //Get Special Type
                DTO.ResponseService<DTO.SpecialDocument[]> lsSpecialType = biz.GetSpecialDocType("A", "Y");

                if (this.MasterLicense != null)
                {
                    var specialFile = (from A in this.MasterLicense.AttachFiles
                                       join B in lsSpecialType.DataResponse.ToList() on A.ATTACH_FILE_TYPE equals B.DOCUMENT_CODE
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
                        gvTrainSpecial.DataSource = specialFile;
                        gvTrainSpecial.DataBind();
                    }
                }
            }
        }

        private void GetPersonalDetailByIDCard()
        {
            RegistrationBiz biz = new RegistrationBiz();


            if ((this.CurrentIDCard != "") || (this.CurrentIDCard != null))
            {
                if (this.CurrentPersonalEntity == null)
                {
                    DTO.ResponseService<DTO.Person> res = biz.GetPersonalDetailByIDCard(this.CurrentIDCard);
                    if (res.DataResponse != null)
                    {
                        this.CurrentPersonalEntity = res.DataResponse;
                    }
                }
            }


        }

        private void GetPersoanlLicenseByIDCard()
        {
            LicenseBiz biz = new LicenseBiz();

            if ((this.CurrentIDCard != "") || (this.CurrentIDCard != null))
            {
                if (this.LicenseStage == "")
                {
                    DTO.ResponseService<DTO.PersonLicenseTransaction[]> res = biz.GetAllLicenseByIDCard(this.CurrentIDCard, "1", 1);
                    if (res.DataResponse != null)
                    {
                        this.PersonalLicense = res.DataResponse.ToList();
                        this.LicenseStage = "Y";
                    }
                }

            }

        }

        public void GetAttatchRegisterationFiles()
        {
            RegistrationBiz biz = new RegistrationBiz();

            if ((this.CurrentRegistrationID != "") || (this.CurrentRegistrationID != null))
            {
                if (this.AttachfileStage == "")
                {
                    DTO.ResponseService<DTO.RegistrationAttatchFile[]> res = biz.GetAttatchFilesByRegisterationID(this.CurrentRegistrationID);
                    if (res.DataResponse != null)
                    {
                        this.RegistrationAttachFileList = res.DataResponse.ToList();
                        this.AttachfileStage = "Y";
                    }
                }

            }

        }

        public void GetPersonalSkill(string LicenseTypeCode, string PetitionCode, string RenewTime)
        {
            LicenseBiz biz = new LicenseBiz();
            Int16 IRenewTime = 0;

            if (!string.IsNullOrEmpty(RenewTime) &&  PetitionCode != "11")
            {
                if (RenewTime.ToInt() > 2)
                {
                    this.PettionTypeCode = "14";
                }
            }
       


            if (PetitionCode != "11")
            {
                IRenewTime = Convert.ToInt16(RenewTime);
            }
            if (this.CheckApprove != "ApproveL")
            {

                if (this.MasterLicense.PersonalSkillStage != "")
                {
                    #region gvGeneral
                    if (this.GridviewGeneral != null)
                    {
                        gvGeneral.DataSource = this.GridviewGeneral;
                        gvGeneral.DataBind();

                        //Create Head Genaral
                        if (gvGeneral.Controls.Count > 0)
                        {
                            AddSuperHeader(gvGeneral);
                        }
                        else
                        {
                            //AddSuperHeader(gvTrainResult);
                        }
                    }
                    else
                    {
                        gvGeneral.Visible = false;
                    }
                    #endregion

                    #region gvExamResult
                    if (this.GridviewExamResult != null)
                    {
                        gvExamResult.DataSource = this.GridviewExamResult;
                        gvExamResult.DataBind();
                        if (gvGeneral.Controls.Count > 0)
                        {
                            //AddSuperHeader(gvExamResult);
                        }
                        else
                        {
                            AddSuperHeader(gvExamResult);
                        }


                    }
                    else
                    {
                        gvExamResult.Visible = false;

                    }
                    #endregion

                    #region gvEducation
                    if (this.GridviewEducation != null)
                    {
                        gvEducation.DataSource = this.GridviewEducation;
                        gvEducation.DataBind();
                        //Create Head Genaral
                        if (gvExamResult.Controls.Count > 0)
                        {
                            //AddSuperHeader(gvEducation);
                        }
                        else
                        {
                            AddSuperHeader(gvEducation);
                        }
                    }
                    else
                    {
                        gvEducation.Visible = false;
                    }
                    #endregion

                    #region gvTrainResult
                    if (this.GridviewTrainResult != null)
                    {
                        gvTrainResult.DataSource = this.GridviewTrainResult;
                        gvTrainResult.DataBind();
                        //Create Head Genaral
                        if (gvEducation.Controls.Count > 0)
                        {
                            //AddSuperHeader(gvTrainResult);
                        }
                        else
                        {
                            AddSuperHeader(gvTrainResult);
                        }

                    }
                    else
                    {
                        gvTrainResult.Visible = false;
                    }
                    #endregion

                    #region gvOther
                    if (this.GridviewOther != null)
                    {
                        gvOther.DataSource = this.GridviewOther;
                        gvOther.DataBind();
                        //Create Head Genaral
                        if (gvTrainResult.Controls.Count > 0)
                        {
                            //AddSuperHeader(gvOther);
                        }
                        else
                        {
                            AddSuperHeader(gvOther);
                        }

                    }
                    else
                    {
                        //ยกเลิกการตรวจสอบ
                        gvOther.Visible = false;
                    }
                    #endregion

                    //ยกเลิกการตรวจสอบ
                    gvOther.Visible = false;

                }
                else
                {
                    #region gvGeneral
                    DTO.ResponseService<DTO.ValidateLicense[]> result = biz.GetPropLiecense(LicenseTypeCode, PetitionCode, IRenewTime, 1);
                    if (result.DataResponse.Count() > 0)
                    {
                        gvGeneral.DataSource = result.DataResponse;
                        gvGeneral.DataBind();
                        this.GridviewGeneral = result.DataResponse.ToList();
                        //Create Head Genaral
                        if (gvGeneral.Controls.Count > 0)
                        {
                            AddSuperHeader(gvGeneral);
                        }
                        else
                        {

                            //AddSuperHeader(gvTrainResult);
                        }
                    }
                    else
                    {
                        gvGeneral.Visible = false;
                    }
                    #endregion

                    #region gvExamResult
                    DTO.ResponseService<DTO.ValidateLicense[]> result2 = biz.GetPropLiecense(LicenseTypeCode, PetitionCode, IRenewTime, 2);
                    if (result2.DataResponse.Count() > 0)
                    {
                        gvExamResult.DataSource = result2.DataResponse;
                        gvExamResult.DataBind();
                        this.GridviewExamResult = result2.DataResponse.ToList();

                        //Create Head Genaral
                        if (gvGeneral.Controls.Count > 0)
                        {
                            //AddSuperHeader(gvExamResult);
                        }
                        else
                        {

                            AddSuperHeader(gvExamResult);
                        }
                    }
                    else
                    {
                        gvExamResult.Visible = false;
                    }
                    #endregion

                    #region gvEducation
                    DTO.ResponseService<DTO.ValidateLicense[]> result3 = biz.GetPropLiecense(LicenseTypeCode, PetitionCode, IRenewTime, 3);
                    if (result3.DataResponse.Count() > 0)
                    {
                        gvEducation.DataSource = result3.DataResponse;
                        gvEducation.DataBind();
                        this.GridviewEducation = result3.DataResponse.ToList();

                        //Create Head Genaral
                        if (gvExamResult.Controls.Count > 0)
                        {
                            //AddSuperHeader(gvEducation);
                        }
                        else
                        {

                            AddSuperHeader(gvEducation);
                        }
                    }
                    else
                    {
                        gvEducation.Visible = false;
                    }
                    #endregion

                    #region gvTrainResult
                    DTO.ResponseService<DTO.ValidateLicense[]> result4 = biz.GetPropLiecense(LicenseTypeCode, PetitionCode, IRenewTime, 4);
                    if (result4.DataResponse.Count() > 0)
                    {
                        gvTrainResult.DataSource = result4.DataResponse;
                        gvTrainResult.DataBind();
                        this.GridviewTrainResult = result4.DataResponse.ToList();

                        //Create Head Genaral
                        if (gvEducation.Controls.Count > 0)
                        {
                            //AddSuperHeader(gvTrainResult);
                        }
                        else
                        {

                            AddSuperHeader(gvTrainResult);
                        }
                    }
                    else
                    {
                        gvTrainResult.Visible = false;
                    }
                    #endregion

                    #region gvOther
                    DTO.ResponseService<DTO.ValidateLicense[]> result5 = biz.GetPropLiecense(LicenseTypeCode, PetitionCode, IRenewTime, 5);
                    if (result5.DataResponse.Count() > 0)
                    {
                        gvOther.DataSource = result5.DataResponse;
                        gvOther.DataBind();
                        this.GridviewOther = result5.DataResponse.ToList();

                        //Create Head Genaral
                        if (gvTrainResult.Controls.Count > 0)
                        {
                            //AddSuperHeader(gvOther);
                        }
                        else
                        {

                            AddSuperHeader(gvOther);
                        }
                    }
                    else
                    {
                        gvOther.Visible = false;
                    }
                    #endregion

                    //ยกเลิกการตรวจสอบ
                    gvOther.Visible = false;

                    //Update stage for Performance Tuning
                    this.MasterLicense.PersonalSkillStage = "Y";
                }

                this.chkValidateProp(this.GridviewOtherList, gvGeneral, true, this.Mode);
                this.chkValidateProp(this.GridviewOtherList, gvExamResult, true, this.Mode);
                this.chkValidateProp(this.GridviewOtherList, gvEducation, true, this.Mode);
                this.chkValidateProp(this.GridviewOtherList, gvTrainResult, true, this.Mode);
                this.chkValidateProp(this.GridviewOtherList, gvOther, true, this.Mode);
            }
            else
            {
                #region gvGeneral
                DTO.ResponseService<DTO.ValidateLicense[]> result = biz.GetPropLiecense(LicenseTypeCode, PetitionCode, IRenewTime, 1);
                gvGeneral.DataSource = result.DataResponse;
                gvGeneral.DataBind();
                this.GridviewGeneral = result.DataResponse.ToList();
                if (result.DataResponse.Count() > 0)
                {
                    //Create Head Genaral
                    if (gvGeneral.Controls.Count > 0)
                    {
                        AddSuperHeader(gvGeneral);
                    }
                    else
                    {
                        //AddSuperHeader(gvTrainResult);
                    }

                }
                else
                {
                    gvGeneral.Visible = false;
                }

                #endregion

                #region gvExamResult
                DTO.ResponseService<DTO.ValidateLicense[]> result2 = biz.GetPropLiecense(LicenseTypeCode, PetitionCode, IRenewTime, 2);
                if (result2.DataResponse.Count() > 0)
                {
                    gvExamResult.DataSource = result2.DataResponse;
                    gvExamResult.DataBind();
                    this.GridviewExamResult = result2.DataResponse.ToList();

                    //Create Head Genaral
                    if (gvGeneral.Controls.Count > 0)
                    {
                        //AddSuperHeader(gvExamResult);
                    }
                    else
                    {

                        AddSuperHeader(gvExamResult);
                    }
                }
                else
                {
                    gvExamResult.Visible = false;
                }


                #endregion

                #region gvEducation
                DTO.ResponseService<DTO.ValidateLicense[]> result3 = biz.GetPropLiecense(LicenseTypeCode, PetitionCode, IRenewTime, 3);
                if (result3.DataResponse.Count() > 0)
                {
                    gvEducation.DataSource = result3.DataResponse;
                    gvEducation.DataBind();
                    this.GridviewEducation = result3.DataResponse.ToList();

                    //Create Head Genaral
                    if (gvExamResult.Controls.Count > 0)
                    {
                        //AddSuperHeader(gvEducation);
                    }
                    else
                    {

                        AddSuperHeader(gvEducation);
                    }
                }
                else
                {
                    gvEducation.Visible = false;
                }

                #endregion

                #region gvTrainResult
                DTO.ResponseService<DTO.ValidateLicense[]> result4 = biz.GetPropLiecense(LicenseTypeCode, PetitionCode, IRenewTime, 4);
                if (result4.DataResponse.Count() > 0)
                {
                    gvTrainResult.DataSource = result4.DataResponse;
                    gvTrainResult.DataBind();
                    this.GridviewTrainResult = result4.DataResponse.ToList();

                    //Create Head Genaral
                    if (gvEducation.Controls.Count > 0)
                    {
                        //AddSuperHeader(gvTrainResult);
                    }
                    else
                    {

                        AddSuperHeader(gvTrainResult);
                    }
                }
                else
                {
                    gvTrainResult.Visible = false;
                }
                #endregion

                #region gvOther
                DTO.ResponseService<DTO.ValidateLicense[]> result5 = biz.GetPropLiecense(LicenseTypeCode, PetitionCode, IRenewTime, 5);
                if (result5.DataResponse.Count() > 0)
                {
                    gvOther.DataSource = result5.DataResponse;
                    gvOther.DataBind();
                    this.GridviewOther = result5.DataResponse.ToList();

                    //Create Head Genaral
                    if (gvTrainResult.Controls.Count > 0)
                    {
                        //AddSuperHeader(gvOther);
                    }
                    else
                    {

                        AddSuperHeader(gvOther);
                    }
                }
                else
                {
                    gvOther.Visible = false;
                }
                #endregion

                this.chkValidatePropLicenseApp(this.GridviewOtherList, gvGeneral, true, this.Mode);
                this.chkValidatePropLicenseApp(this.GridviewOtherList, gvExamResult, true, this.Mode);
                this.chkValidatePropLicenseApp(this.GridviewOtherList, gvEducation, true, this.Mode);
                this.chkValidatePropLicenseApp(this.GridviewOtherList, gvTrainResult, true, this.Mode);
                this.chkValidatePropLicenseApp(this.GridviewOtherList, gvOther, true, this.Mode);
            }
        }

        private void chkValidateProp(List<string> text, GridView gv, bool enable, string mode)
        {
            LicenseBiz biz = new LicenseBiz();

            foreach (GridViewRow row in gv.Rows)
            {
                Label lblID = (Label)row.FindControl("lblID");
                Label lblItemName = (Label)row.FindControl("lblItemName");
                Image impCorrect = (Image)row.FindControl("impCorrect");
                Image impdisCorrect = (Image)row.FindControl("impdisCorrect");

                //Single License
                if (mode.Equals(DTO.LicensePropMode.General.GetEnumValue().ToString()))
                {
                    var result = text.Where(l => l.Equals(lblItemName.Text.Trim())).FirstOrDefault();

                    if (gv.ID.Equals("gvGeneral"))
                    {
                        //1	บรรลุนิติภาวะ
                        //6	มีภูมิลำเนาในประเทศไทย
                        //7	ไม่เป็นคนวิกลจริตหรือจิตฟั่นเฟือนไม่สมประกอบ
                        //8	ไม่เคยต้องโทษจำคุกโดยคำพิพากษาถึงที่สุดให้จำคุก ในความผิดเกี่ยวกับทรัพย์ที่กระทำโดยทุจริต เว้นแต่พ้นโทษมาแล้วไม่น้อยกว่าห้าปีก่อนวันขอรับใบอนุญาต
                        //9	ไม่เป็นบุคคลล้มละลาย
                        //11 ไม่เป็นตัวแทนประกันชีวิต
                        //13 ไม่เคยถูกเพิกถอนใบอนุญาตเป็นตัวแทนประกันชีวิต หรือใบอนุญาตเป็นนายหน้าประกันชีวิต ระยะเวลาห้าปีก่อนวันขอรับใบอนุญาต
                        switch (lblID.Text)
                        {
                            case "1":
                                if (this.CurrentPersonalEntity.BIRTH_DATE != null)
                                {

                                    //code by Chalermwut 10/09/2014
                                    // check เอกสารแนบทะเบียนสมรส

                                    string marType = DTO.DocumentLicenseType.Marriage_license.GetEnumValue().ToString();
                                    //this.AttachfileStage;
                                    var marFile = (from a in this.MasterLicense.AttachFiles
                                                   where marType.Contains(a.ATTACH_FILE_TYPE)
                                                   select a.ATTACH_FILE_TYPE).FirstOrDefault();

                                    DateTime birthday = this.CurrentPersonalEntity.BIRTH_DATE.Value;

                                    Int32 CurAge = this.GetAge(birthday);

                                    if (string.IsNullOrEmpty(marFile))
                                    {
                                        //int YcurrentDatenow = DateTime.Now.Year;
                                        //int Ybirthday = birthday.Year;

                                        //if (!System.Globalization.CultureInfo.InstalledUICulture.Name.Equals("th-TH"))
                                        //{
                                        //    YcurrentDatenow = DateTime.Now.AddYears(543).Year;
                                        //    Ybirthday = Ybirthday + 543;
                                        //}
                                        if (CurAge >= 20)
                                        {
                                            impCorrect.Visible = true;
                                            impdisCorrect.Visible = false;
                                        }
                                        else
                                        {
                                            impdisCorrect.Visible = true;
                                            impCorrect.Visible = false;
                                        }
                                    }
                                    else
                                    {
                                        if (CurAge < 17)
                                        {
                                            impdisCorrect.Visible = true;
                                            impCorrect.Visible = false;
                                        }
                                        else
                                        {
                                            impCorrect.Visible = true;
                                            impdisCorrect.Visible = false;
                                        }

                                    }
                                }
                                else
                                {
                                    impdisCorrect.Visible = true;
                                    impCorrect.Visible = false;
                                }
                                break;
                            case "6":
                                if (this.CurrentPersonalEntity.NATIONALITY != null)
                                {
                                    string nataion = this.NationaConvert(this.CurrentPersonalEntity.NATIONALITY);
                                    if (nataion.Equals("ไทย"))
                                    {
                                        impCorrect.Visible = true;
                                        impdisCorrect.Visible = false;
                                    }
                                    else
                                    {
                                        impdisCorrect.Visible = true;
                                        impCorrect.Visible = false;
                                    }
                                }
                                break;
                            case "7":
                                impCorrect.Visible = true;
                                impdisCorrect.Visible = false;
                                break;
                            case "8":
                                impCorrect.Visible = true;
                                impdisCorrect.Visible = false;
                                break;
                            case "9":
                                impCorrect.Visible = true;
                                impdisCorrect.Visible = false;
                                break;
                            case "11":
                                if (this.LicenseTypeCode.Equals("03"))
                                {
                                    var licensechk = this.PersonalLicense.Where(li => li.LICENSE_TYPE_CODE.Equals("01")
                                        || li.LICENSE_TYPE_CODE.Equals("03") || li.LICENSE_TYPE_CODE.Equals("07")).FirstOrDefault();
                                    if (licensechk == null)
                                    {
                                        impCorrect.Visible = true;
                                        impdisCorrect.Visible = false;
                                    }
                                    else
                                    {
                                        impdisCorrect.Visible = true;
                                        impCorrect.Visible = false;
                                    }
                                }
                                else if (this.LicenseTypeCode.Equals("04"))
                                {
                                    var licensechk = this.PersonalLicense.Where(li => li.LICENSE_TYPE_CODE.Equals("02")
                                        || li.LICENSE_TYPE_CODE.Equals("04") || li.LICENSE_TYPE_CODE.Equals("05")
                                        || li.LICENSE_TYPE_CODE.Equals("06") || li.LICENSE_TYPE_CODE.Equals("08")).FirstOrDefault();
                                    if (licensechk == null)
                                    {
                                        impCorrect.Visible = true;
                                        impdisCorrect.Visible = false;
                                    }
                                    else
                                    {
                                        impdisCorrect.Visible = true;
                                        impCorrect.Visible = false;
                                    }
                                }
                                else
                                {
                                    impCorrect.Visible = true;
                                    impdisCorrect.Visible = false;
                                }
                                break;
                            case "12":
                                if (this.LicenseTypeCode.Equals("03"))
                                {
                                    var licensechk = this.PersonalLicense.Where(li => li.LICENSE_TYPE_CODE.Equals("01")
                                        || li.LICENSE_TYPE_CODE.Equals("03") || li.LICENSE_TYPE_CODE.Equals("07")).FirstOrDefault();
                                    if (licensechk == null)
                                    {
                                        impCorrect.Visible = true;
                                        impdisCorrect.Visible = false;
                                    }
                                    else
                                    {
                                        impdisCorrect.Visible = true;
                                        impCorrect.Visible = false;
                                    }
                                }
                                else if (this.LicenseTypeCode.Equals("04"))
                                {
                                    var licensechk = this.PersonalLicense.Where(li => li.LICENSE_TYPE_CODE.Equals("02")
                                        || li.LICENSE_TYPE_CODE.Equals("04") || li.LICENSE_TYPE_CODE.Equals("05")
                                        || li.LICENSE_TYPE_CODE.Equals("06") || li.LICENSE_TYPE_CODE.Equals("08")).FirstOrDefault();
                                    if (licensechk == null)
                                    {
                                        impCorrect.Visible = true;
                                        impdisCorrect.Visible = false;
                                    }
                                    else
                                    {
                                        impdisCorrect.Visible = true;
                                        impCorrect.Visible = false;
                                    }
                                }
                                else
                                {
                                    impCorrect.Visible = true;
                                    impdisCorrect.Visible = false;
                                }
                                break;
                            case "13":
                                if (this.PersonalLicense.Count > 0)
                                {
                                    IEnumerable<string> lic = this.PersonalLicense.Select(l => l.LICENSE_NO).ToList();
                                    DTO.ResponseMessage<bool> resRevokeLicense = biz.LicenseRevokedValidation(lic.ToArray(), this.LicenseTypeCode);
                                    if (resRevokeLicense.ResultMessage == true)
                                    {
                                        impCorrect.Visible = true;
                                        impdisCorrect.Visible = false;
                                    }
                                    else
                                    {
                                        impdisCorrect.Visible = true;
                                        impCorrect.Visible = false;
                                    }
                                }
                                else
                                {
                                    impCorrect.Visible = true;
                                    impdisCorrect.Visible = false;
                                }
                                break;
                            case "16":
                                if (this.PersonalLicense.Count > 0)
                                {
                                    IEnumerable<string> lic = this.PersonalLicense.Select(l => l.LICENSE_NO).ToList();
                                    DTO.ResponseMessage<bool> resRevokeLicense = biz.LicenseRevokedValidation(lic.ToArray(), this.LicenseTypeCode);
                                    if (resRevokeLicense.ResultMessage == true)
                                    {
                                        impCorrect.Visible = true;
                                        impdisCorrect.Visible = false;
                                    }
                                    else
                                    {
                                        impdisCorrect.Visible = true;
                                        impCorrect.Visible = false;
                                    }
                                }
                                else
                                {
                                    impCorrect.Visible = true;
                                    impdisCorrect.Visible = false;
                                }
                                break;
                            case "19":
                                if (this.MasterLicense.PettionTypeCode.Equals("16"))
                                {
                                    impCorrect.Visible = true;
                                    impdisCorrect.Visible = false;
                                }
                                else
                                {
                                    impdisCorrect.Visible = true;
                                    impCorrect.Visible = false;
                                }
                                break;
                            case "22":
                                if (this.PettionTypeCode.Equals("15"))
                                {
                                    if (this.MasterLicense.CurrentExpiredLicneseList.Count > 0)
                                    {
                                        var licensechk = this.MasterLicense.CurrentExpiredLicneseList.Where(li => li.LICENSE_TYPE_CODE.Equals(this.LicenseTypeCode)).FirstOrDefault();
                                        if (licensechk != null)
                                        {
                                            var chkLicenseAboutActive = biz.ChkLicenseAboutActive(this.CurrentIDCard, this.LicenseTypeCode);

                                            if (chkLicenseAboutActive.ResultMessage == true)
                                            {
                                                impCorrect.Visible = true;
                                                impdisCorrect.Visible = false;
                                            }
                                            else
                                            {
                                                impdisCorrect.Visible = true;
                                                impCorrect.Visible = false;
                                            }

                                        }
                                        else
                                        {
                                            impdisCorrect.Visible = true;
                                            impCorrect.Visible = false;
                                        }
                                    }
                                    else
                                    {
                                        impdisCorrect.Visible = true;
                                        impCorrect.Visible = false;
                                    }
                                }
                                else
                                {
                                    impCorrect.Visible = true;
                                    impdisCorrect.Visible = false;
                                }
                                break;

                        }
                    }
                    else if (gv.ID.Equals("gvExamResult"))
                    {
                        //2	ผลสอบ
                        switch (lblID.Text)
                        {
                            case "2":
                                if (this.curResultExam.Equals("ผ่าน"))
                                {
                                    impCorrect.Visible = true;
                                    impdisCorrect.Visible = false;
                                }
                                else
                                {
                                    impdisCorrect.Visible = true;
                                    impCorrect.Visible = false;
                                }
                                break;
                        }
                    }
                    else if (gv.ID.Equals("gvEducation"))
                    {
                        //3	สำเร็จการศึกษาไม่ต่ำกว่าปริญญาตรี วิชาการประกันชีวิตไม่ต่ำกว่าชั้นปริญญาตรีหรือเทียบเท่าไม่น้อยกว่า 6 หน่วยกิต
                        switch (lblID.Text)
                        {
                            case "3":
                                if (this.CurrentPersonalEntity != null)
                                {
                                    if (this.CurrentPersonalEntity.EDUCATION_CODE.Equals("05")
                                        || Convert.ToInt32(this.CurrentPersonalEntity.EDUCATION_CODE) >= 5)
                                    {
                                        impCorrect.Visible = true;
                                        impdisCorrect.Visible = false;
                                    }
                                    else
                                    {
                                        impdisCorrect.Visible = true;
                                        impCorrect.Visible = false;
                                    }
                                }
                                else
                                {
                                    impdisCorrect.Visible = true;
                                    impCorrect.Visible = false;
                                }
                                break;
                            case "17":
                                if (this.CurrentPersonalEntity != null)
                                {
                                    if (this.CurrentPersonalEntity.EDUCATION_CODE.Equals("05")
                                        || Convert.ToInt32(this.CurrentPersonalEntity.EDUCATION_CODE) >= 5)
                                    {
                                        impCorrect.Visible = true;
                                        impdisCorrect.Visible = false;
                                    }
                                    else
                                    {
                                        impdisCorrect.Visible = true;
                                        impCorrect.Visible = false;
                                    }
                                }
                                else
                                {
                                    impdisCorrect.Visible = true;
                                    impCorrect.Visible = false;
                                }
                                break;
                        }
                    }
                    else if (gv.ID.Equals("gvTrainResult"))
                    {
                        //4	ผลอบรม
                        switch (lblID.Text)
                        {
                            case "4":
                                if ((this.GetPersonalTrainExResult != ""))
                                {
                                    if (this.GetPersonalTrainExResult.Equals("ผ่าน"))
                                    {
                                        impCorrect.Visible = true;
                                        impdisCorrect.Visible = false;
                                    }
                                    else
                                    {
                                        impdisCorrect.Visible = true;
                                        impCorrect.Visible = false;
                                    }
                                }
                                else
                                {
                                    if (this.GetPersonalTrainEx().RESULT.Equals("ผ่าน"))
                                    {
                                        this.GetPersonalTrainExResult = "ผ่าน";
                                        impCorrect.Visible = true;
                                        impdisCorrect.Visible = false;
                                    }
                                    else
                                    {
                                        this.GetPersonalTrainExResult = "ไม่ผ่าน";
                                        impdisCorrect.Visible = true;
                                        impCorrect.Visible = false;
                                    }
                                }

                                if (this.MasterLicense.PettionTypeCode == "14")
                                {
                                    if (gvTrainSpecial.Rows.Count > 0)
                                    {
                                        if (impCorrect.Visible == true)
                                        {
                                            impdisCorrect.Visible = true;
                                            impCorrect.Visible = false;

                                            foreach (GridViewRow rowSp in gvTrainSpecial.Rows)
                                            {
                                                Image impdisCorect = (Image)rowSp.FindControl("impdisCorrect");
                                                Label lblwait = (Label)rowSp.FindControl("lblwait");

                                                impdisCorect.Visible = false;
                                                lblwait.Visible = true;
                                            
                                            }


                                        }
                                        else
                                        {
                                            impdisCorrect.Visible = true;
                                            impCorrect.Visible = false;

                                            foreach (GridViewRow rowSp in gvTrainSpecial.Rows)
                                            {
                                                Image impdisCorect = (Image)rowSp.FindControl("impdisCorrect");
                                                Label lblwait = (Label)rowSp.FindControl("lblwait");

                                                impdisCorect.Visible = true;
                                                lblwait.Visible = false;

                                            }

                                        }


                                    }
                                }





                                break;
                        }
                    }
                    else if (gv.ID.Equals("gvOther"))
                    {
                        //5	    รูปถ่าย
                        //14	มีการชำระค่าธรรมเนียมค่าขอรับใบอนุญาต
                        //18	มีการชำระค่าธรรมเนียมค่ขอต่ออายุใบอนุญาต
                        switch (lblID.Text)
                        {
                            case "5":
                                break;
                            case "14":
                                break;
                            case "18":
                                break;
                        }

                    }
                }
                else if (mode.Equals(DTO.LicensePropMode.Group.GetEnumValue().ToString()))
                {
                    var result = text.Where(l => l.Equals(lblID.Text.Trim())).FirstOrDefault();

                    if (result == null)
                    {
                        impCorrect.Visible = true;
                        impdisCorrect.Visible = false;
                    }
                    else
                    {
                        impdisCorrect.Visible = true;
                        impCorrect.Visible = false;
                    }
                }

            }

        }

        private void chkValidatePropLicenseApp(List<string> text, GridView gv, bool enable, string mode)
        {
            LicenseBiz biz = new LicenseBiz();

            foreach (GridViewRow row in gv.Rows)
            {
                Label lblID = (Label)row.FindControl("lblID");
                Label lblItemName = (Label)row.FindControl("lblItemName");
                Image impCorrect = (Image)row.FindControl("impCorrect");
                Image impdisCorrect = (Image)row.FindControl("impdisCorrect");

                //Single License
                if (mode.Equals(DTO.LicensePropMode.General.GetEnumValue().ToString()))
                {
                    var result = text.Where(l => l.Equals(lblItemName.Text.Trim())).FirstOrDefault();

                    if (gv.ID.Equals("gvGeneral"))
                    {
                        //1	บรรลุนิติภาวะ
                        //6	มีภูมิลำเนาในประเทศไทย
                        //7	ไม่เป็นคนวิกลจริตหรือจิตฟั่นเฟือนไม่สมประกอบ
                        //8	ไม่เคยต้องโทษจำคุกโดยคำพิพากษาถึงที่สุดให้จำคุก ในความผิดเกี่ยวกับทรัพย์ที่กระทำโดยทุจริต เว้นแต่พ้นโทษมาแล้วไม่น้อยกว่าห้าปีก่อนวันขอรับใบอนุญาต
                        //9	ไม่เป็นบุคคลล้มละลาย
                        //11 ไม่เป็นตัวแทนประกันชีวิต
                        //13 ไม่เคยถูกเพิกถอนใบอนุญาตเป็นตัวแทนประกันชีวิต หรือใบอนุญาตเป็นนายหน้าประกันชีวิต ระยะเวลาห้าปีก่อนวันขอรับใบอนุญาต
                        switch (lblID.Text)
                        {
                            case "1":
                                impCorrect.Visible = true;
                                impdisCorrect.Visible = false;
                                break;
                            case "6":
                                impCorrect.Visible = true;
                                impdisCorrect.Visible = false;
                                break;
                            case "7":

                                impCorrect.Visible = true;
                                impdisCorrect.Visible = false;
                                break;
                            case "8":

                                impCorrect.Visible = true;
                                impdisCorrect.Visible = false;
                                break;
                            case "9":

                                impCorrect.Visible = true;
                                impdisCorrect.Visible = false;
                                break;
                            case "11":
                                if (this.LicenseTypeCode.Equals("03"))
                                {
                                    var licensechk = this.PersonalLicense.Where(li => li.LICENSE_TYPE_CODE.Equals("01")
                                        || li.LICENSE_TYPE_CODE.Equals("03") || li.LICENSE_TYPE_CODE.Equals("07")).FirstOrDefault();
                                    if (licensechk == null)
                                    {
                                        impCorrect.Visible = true;
                                        impdisCorrect.Visible = false;
                                    }
                                    else
                                    {
                                        impdisCorrect.Visible = true;
                                        impCorrect.Visible = false;
                                    }
                                }
                                else if (this.LicenseTypeCode.Equals("04"))
                                {
                                    var licensechk = this.PersonalLicense.Where(li => li.LICENSE_TYPE_CODE.Equals("02")
                                        || li.LICENSE_TYPE_CODE.Equals("04") || li.LICENSE_TYPE_CODE.Equals("05")
                                        || li.LICENSE_TYPE_CODE.Equals("06") || li.LICENSE_TYPE_CODE.Equals("08")).FirstOrDefault();
                                    if (licensechk == null)
                                    {
                                        impCorrect.Visible = true;
                                        impdisCorrect.Visible = false;
                                    }
                                    else
                                    {
                                        impdisCorrect.Visible = true;
                                        impCorrect.Visible = false;
                                    }
                                }
                                else
                                {
                                    impCorrect.Visible = true;
                                    impdisCorrect.Visible = false;
                                }
                                break;
                            case "12":
                                if (this.LicenseTypeCode.Equals("03"))
                                {
                                    var licensechk = this.PersonalLicense.Where(li => li.LICENSE_TYPE_CODE.Equals("01")
                                        || li.LICENSE_TYPE_CODE.Equals("03") || li.LICENSE_TYPE_CODE.Equals("07")).FirstOrDefault();
                                    if (licensechk == null)
                                    {
                                        impCorrect.Visible = true;
                                        impdisCorrect.Visible = false;
                                    }
                                    else
                                    {
                                        impdisCorrect.Visible = true;
                                        impCorrect.Visible = false;
                                    }
                                }
                                else if (this.LicenseTypeCode.Equals("04"))
                                {
                                    var licensechk = this.PersonalLicense.Where(li => li.LICENSE_TYPE_CODE.Equals("02")
                                        || li.LICENSE_TYPE_CODE.Equals("04") || li.LICENSE_TYPE_CODE.Equals("05")
                                        || li.LICENSE_TYPE_CODE.Equals("06") || li.LICENSE_TYPE_CODE.Equals("08")).FirstOrDefault();
                                    if (licensechk == null)
                                    {
                                        impCorrect.Visible = true;
                                        impdisCorrect.Visible = false;
                                    }
                                    else
                                    {
                                        impdisCorrect.Visible = true;
                                        impCorrect.Visible = false;
                                    }
                                }
                                else
                                {
                                    impCorrect.Visible = true;
                                    impdisCorrect.Visible = false;
                                }
                                break;
                            case "13":
                                if (this.PersonalLicense.Count > 0)
                                {

                                    impCorrect.Visible = true;
                                    impdisCorrect.Visible = false;
                                }

                                else
                                {
                                    impCorrect.Visible = true;
                                    impdisCorrect.Visible = false;
                                }
                                break;
                            case "16":

                                impCorrect.Visible = true;
                                impdisCorrect.Visible = false;

                                break;
                            case "19":

                                impCorrect.Visible = true;
                                impdisCorrect.Visible = false;


                                break;
                        }


                    }
                    else if (gv.ID.Equals("gvExamResult"))
                    {
                        //2	ผลสอบ
                        switch (lblID.Text)
                        {
                            case "2":

                                impCorrect.Visible = true;
                                impdisCorrect.Visible = false;

                                break;
                        }
                    }
                    else if (gv.ID.Equals("gvEducation"))
                    {
                        //3	สำเร็จการศึกษาไม่ต่ำกว่าปริญญาตรี วิชาการประกันชีวิตไม่ต่ำกว่าชั้นปริญญาตรีหรือเทียบเท่าไม่น้อยกว่า 6 หน่วยกิต
                        switch (lblID.Text)
                        {
                            case "3":
                                impdisCorrect.Visible = true;
                                impCorrect.Visible = false;
                                break;
                            case "17":
                                impCorrect.Visible = true;
                                impdisCorrect.Visible = false;
                                break;
                        }
                    }
                    else if (gv.ID.Equals("gvTrainResult"))
                    {
                        //4	ผลอบรม
                        switch (lblID.Text)
                        {
                            case "4":
                                impCorrect.Visible = true;
                                impdisCorrect.Visible = false;
                                break;
                        }
                    }
                    else if (gv.ID.Equals("gvOther"))
                    {
                        //5	    รูปถ่าย
                        //14	มีการชำระค่าธรรมเนียมค่าขอรับใบอนุญาต
                        //18	มีการชำระค่าธรรมเนียมค่ขอต่ออายุใบอนุญาต
                        switch (lblID.Text)
                        {
                            case "5":
                                break;
                            case "14":
                                break;
                            case "18":
                                break;
                        }

                    }
                }
                else if (mode.Equals(DTO.LicensePropMode.Group.GetEnumValue().ToString()))
                {
                    var result = text.Where(l => l.Equals(lblID.Text.Trim())).FirstOrDefault();

                    if (result == null)
                    {
                        impCorrect.Visible = true;
                        impdisCorrect.Visible = false;
                    }
                    else
                    {
                        impdisCorrect.Visible = true;
                        impCorrect.Visible = false;
                    }
                }

            }

        }

        private static void AddSuperHeader(GridView gridView)
        {
            var myTable = (Table)gridView.Controls[0];
            var myNewRow = new GridViewRow(0, -1, DataControlRowType.Header, DataControlRowState.Normal);

            myNewRow.Cells.Add(MakeCell("ลำดับที่", 1));
            myNewRow.Cells.Add(MakeCell("เงื่อนไขการตรวจสอบ", 1));
            myNewRow.Cells.Add(MakeCell("ผลการตรวจสอบ", 1));
            myNewRow.Cells.Add(MakeCell("หมายเหตุ", 1));

            myTable.Rows.AddAt(0, myNewRow);

        }

        private static TableHeaderCell MakeCell(string text = null, int span = 1)
        {
            return new TableHeaderCell() { Text = text ?? string.Empty, ColumnSpan = span, HorizontalAlign = HorizontalAlign.Center };
        }

        private string NullToString(string input)
        {
            if ((input == null) || (input == ""))
            {
                input = "0";
            }

            return input;
        }

        public DTO.TrainPersonHistory GetPersonalTrainEx()
        {
            LicenseBiz biz = new LicenseBiz();
            DataCenterBiz dataBiz = new DataCenterBiz();
            DTO.TrainPersonHistory res = new DTO.TrainPersonHistory();

            string specialFileCode = string.Empty;


            try
            {
                //List<DTO.AttatchFileLicense> AttachFilesLicensels = new List<DTO.AttatchFileLicense>();

                //foreach (IAS.BLL.AttachFilesIAS.AttachFile lic in this.MasterLicense.AttachFiles)
                //{
                //    DTO.AttatchFileLicense ent = new DTO.AttatchFileLicense
                //    {
                //        ID_ATTACH_FILE = lic.REGISTRATION_ID,
                //        ID_CARD_NO = lic.ID_CARD_NO,
                //        ATTACH_FILE_TYPE = lic.ATTACH_FILE_TYPE,
                //        ATTACH_FILE_PATH = lic.ATTACH_FILE_PATH,
                //        REMARK = lic.REMARK,
                //        CREATED_BY = lic.CREATED_BY,
                //        CREATED_DATE = lic.CREATED_DATE,
                //        UPDATED_BY = lic.UPDATED_BY,
                //        UPDATED_DATE = lic.UPDATED_DATE,
                //        FILE_STATUS = lic.FILE_STATUS,
                //        LICENSE_NO = lic.LICENSE_NO,
                //        RENEW_TIME = lic.RENEW_TIME,
                //        GROUP_LICENSE_ID = lic.GROUP_LICENSE_ID,

                //    };

                //    AttachFilesLicensels.Add(ent);
                //}

                DTO.ResponseService<DTO.SpecialDocument[]> lsSpecialType = dataBiz.GetSpecialDocType("A", "Y");

                if (this.MasterLicense.AttachFiles != null)
                {


                    var specialFile = (from A in this.MasterLicense.AttachFiles
                                       join B in lsSpecialType.DataResponse.ToList() on A.ATTACH_FILE_TYPE equals B.DOCUMENT_CODE
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
                                           GROUP_LICENSE_ID = A.GROUP_LICENSE_ID,
                                           SPECIAL_TYPE_CODE_TRAIN = B.SPECIAL_TYPE_CODE_TRAIN

                                       }).FirstOrDefault();

                    if (specialFile != null)
                    {
                        specialFileCode = specialFile.SPECIAL_TYPE_CODE_TRAIN;
                    }

                }



                DTO.ResponseService<DTO.TrainPersonHistory> ressult = biz.GetPersonalTrainByCriteria(this.MasterLicense.LICENSE_TYPE_CODE,
                this.MasterLicense.PETITION_TYPE_CODE, this.MasterLicense.TRAIN_TIMES, Session["PersonalIdCard"].ToString(), this.MasterLicense.SelectedLicenseNo, specialFileCode);

                res = ressult.DataResponse;

            }
            catch (Exception ex)
            {

                throw;
            }
            return res;

        }

        public DTO.ResponseMessage<bool> LicenseValidation()
        {
            var res = new DTO.ResponseMessage<bool>();
            List<bool> lsCheck = new List<bool>();
            StringBuilder strb = new StringBuilder();
            try
            {
                List<GridView> gvList = new List<GridView>()
                {
                    gvGeneral,
                    gvExamResult,
                    gvEducation,
                    gvTrainResult
                };

                //มีการแนบเอกสารตรวจสอบคุณสมบัติพิเศษ > ขอรับใบอนุญาตใหม่, ขาดต่อขอใหม่
                if ((this.MasterLicense.PettionTypeCode == "11") || (this.MasterLicense.PettionTypeCode == "15"))
                {
                    //if (gvTrainSpecial.Rows.Count > 0)
                    //{
                    //    res.ResultMessage = true;
                    //}
                    //else
                    //{
                    //gvList.ForEach(s => s.Rows.OfType<Image>().FirstOrDefault(ctrl => ctrl.FindControl("impdisCorrect").ID.Equals("impdisCorrect")));
                    //gvList.Select(s => s.Rows.OfType<GridViewRow>().FirstOrDefault().FindControl("impdisCorrect").Visible);

                    for (int i = 0; i < gvList.Count; i++)
                    {
                        foreach (GridViewRow row in gvList[i].Rows)
                        {
                            Label lblItemName = (Label)row.FindControl("lblItemName");
                            Image impdisCorrect = (Image)row.FindControl("impdisCorrect");
                            if (impdisCorrect != null)
                            {
                                if (impdisCorrect.Visible == true)
                                {
                                    if (lblItemName.Text.Trim() == "ผลสอบ")
                                    {
                                        if (gvExamSpecial.Rows.Count == 0)
                                        {
                                            strb.Append("คุณสมบัติ " + lblItemName.Text + " ไม่ผ่าน" + "<br/>");
                                            lsCheck.Add(impdisCorrect.Visible);
                                        }
                                    }
                                    else
                                    {
                                        strb.Append("คุณสมบัติ " + lblItemName.Text + " ไม่ผ่าน" + "<br/>");
                                        lsCheck.Add(impdisCorrect.Visible);
                                    }

                                }

                            }

                        }
                    }



                    if (lsCheck.Where(s => s == true).Count() > 0)
                    {
                        res.ErrorMsg = strb.ToString();
                        res.ResultMessage = false;
                    }
                    else
                    {
                        res.ResultMessage = true;
                    }
                }

                else if (this.MasterLicense.PettionTypeCode == "14")
                {
                    //gvList.ForEach(s => s.Rows.OfType<Image>().FirstOrDefault(ctrl => ctrl.FindControl("impdisCorrect").ID.Equals("impdisCorrect")));
                    //gvList.Select(s => s.Rows.OfType<GridViewRow>().FirstOrDefault().FindControl("impdisCorrect").Visible);

                    if (gvTrainSpecial.Rows.Count > 0)
                    {
                        Label lblDocumentName = (Label)gvTrainSpecial.Rows[0].FindControl("lblItem");

                        //CAI ผ่านเลย
                        if (lblDocumentName.Text.Trim() == "CIA")
                        {
                            res.ResultMessage = true;
                        }
                        else
                        {
                            foreach (GridViewRow row in gvTrainSpecial.Rows)
                            {
                                Image impdisCorect = (Image)row.FindControl("impdisCorrect");
                                Label lblwait = (Label)row.FindControl("lblwait");

                                if (impdisCorect.Visible == true)
                                {
                                    for (int i = 0; i < gvList.Count; i++)
                                    {
                                        foreach (GridViewRow rowView in gvList[i].Rows)
                                        {
                                            Label lblItemName = (Label)rowView.FindControl("lblItemName");
                                            Image impdisCorrect = (Image)rowView.FindControl("impdisCorrect");
                                            if (impdisCorrect != null)
                                            {
                                                if (impdisCorrect.Visible == true)
                                                {
                                                    strb.Append("คุณสมบัติ " + lblItemName.Text + " ไม่ผ่าน" + "<br/>");
                                                    lsCheck.Add(impdisCorrect.Visible);
                                                }

                                            }

                                        }
                                    }

                                    if (lsCheck.Where(s => s == true).Count() > 0)
                                    {
                                        res.ErrorMsg = strb.ToString();
                                        res.ResultMessage = false;
                                    }
                                    else
                                    {
                                        res.ResultMessage = true;
                                    }
                                }
                                else
                                {
                                    res.ResultMessage = true;
                                }
                                //impCorrect.Visible = false;
                                //impdisCorrect.Visible = true;
                            }


                        }
                        //else
                        //{
                        //    foreach (GridViewRow row in gvTrainResult.Rows)
                        //    {
                        //        Image impCorrect = (Image)row.FindControl("impCorrect");
                        //        Image impdisCorrect = (Image)row.FindControl("impdisCorrect");

                        //        if (impCorrect.Visible==true)
                        //        {

                        //            impdisCorrect.Visible = true;
                        //            impCorrect.Visible = false;

                        //            foreach (GridViewRow specialrow in gvTrainSpecial.Rows)
                        //            {
                        //                Image impdisCor = (Image)row.FindControl("impdisCorrect");
                        //                Label lblwait = (Label)row.FindControl("lblwait");

                        //                lblwait.Visible = true;
                        //                impdisCor.Visible = false;
                        //            }
                        //        }
                        //        else
                        //        {
                        //            impdisCorrect.Visible = true;
                        //            impCorrect.Visible = false;

                        //            foreach (GridViewRow specialrow in gvTrainSpecial.Rows)
                        //            {
                        //                Image impdisCor = (Image)row.FindControl("impdisCorrect");
                        //                Label lblwait = (Label)row.FindControl("lblwait");

                        //                lblwait.Visible = false;
                        //                impdisCor.Visible = true;
                        //            }
                        //        }

                        //    }
                        //}

                    }
                    else
                    {
                        for (int i = 0; i < gvList.Count; i++)
                        {
                            foreach (GridViewRow row in gvList[i].Rows)
                            {
                                Label lblItemName = (Label)row.FindControl("lblItemName");
                                Image impdisCorrect = (Image)row.FindControl("impdisCorrect");
                                if (impdisCorrect != null)
                                {
                                    if (impdisCorrect.Visible == true)
                                    {
                                        strb.Append("คุณสมบัติ " + lblItemName.Text + " ไม่ผ่าน" + "<br/>");
                                        lsCheck.Add(impdisCorrect.Visible);
                                    }

                                }

                            }
                        }

                        if (lsCheck.Where(s => s == true).Count() > 0)
                        {
                            res.ErrorMsg = strb.ToString();
                            res.ResultMessage = false;
                        }
                        else
                        {
                            res.ResultMessage = true;
                        }
                    }

                }
                else
                {
                    for (int i = 0; i < gvList.Count; i++)
                    {
                        foreach (GridViewRow row in gvList[i].Rows)
                        {
                            Label lblItemName = (Label)row.FindControl("lblItemName");
                            Image impdisCorrect = (Image)row.FindControl("impdisCorrect");
                            if (impdisCorrect != null)
                            {
                                if (impdisCorrect.Visible == true)
                                {
                                    strb.Append("คุณสมบัติ " + lblItemName.Text + " ไม่ผ่าน" + "<br/>");
                                    lsCheck.Add(impdisCorrect.Visible);
                                }

                            }

                        }
                    }

                    if (lsCheck.Where(s => s == true).Count() > 0)
                    {
                        res.ErrorMsg = strb.ToString();
                        res.ResultMessage = false;
                    }
                    else
                    {
                        res.ResultMessage = true;
                    }
                }






            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                res.ResultMessage = false;
            }
            return res;

        }

        public Int32 GetAge(DateTime dateOfBirth)
        {
            DateTime birthday = dateOfBirth;
            DateTime today = DateTime.Now;

            //int YcurrentDatenow = DateTime.Now.Year;
            //int Ybirthday = birthday.Year;

            //if (!System.Globalization.CultureInfo.InstalledUICulture.Name.Equals("th-TH"))
            //{
            //    today = today.AddYears(543);
            //    birthday = birthday.AddYears(543);
            //}
            if (!System.Globalization.CultureInfo.CurrentCulture.Name.Equals("th-TH"))
            {
                today = today.AddYears(543);
                birthday = birthday.AddYears(543);
            }

            var a = (today.Year * 100 + today.Month) * 100 + today.Day;
            var b = (dateOfBirth.Year * 100 + dateOfBirth.Month) * 100 + dateOfBirth.Day;

            return (a - b) / 10000;
        }
        #endregion

        #region UI
        /// <summary>
        /// Datarow Create
        /// </summary>
        /// <param name="Mode"></param>
        /// <param name="e"></param>

        protected void Head_RowCreated(object sender, GridViewRowEventArgs e)
        {
            // Adding a column manualy onece the header creater
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].ColumnSpan = 4;
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[3].Visible = false;
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
            }
        }

        private Func<string, string> NationaConvert = delegate(string input)
        {
            if ((input != null) || (input != ""))
            {
                if (input.Equals("001") || input.Equals("ไทย"))
                {
                    input = "ไทย";
                }
                else
                {
                    input = "จีน";
                }
            }
            else
            {
                input = "ไม่พบสัญชาติ";
            }
            return input;
        };

        #endregion
    }
}