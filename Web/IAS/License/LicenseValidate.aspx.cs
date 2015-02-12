using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.MasterPage;
using IAS.BLL;
using System.Data;
using IAS.DTO;
using IAS.Properties;
using System.Text;
using IAS.Utils;

namespace IAS.License
{
    public partial class LicenseValidate : basepage
    {
        #region Public Param & Session
        public MasterLicense MasterLicense
        {
            get
            {
                return (this.Page.Master as MasterLicense);
            }

        }

        public Site1 MasterPage
        {
            get { return (this.Page.Master as Site1); }
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

        public string PersonalIdCard
        {
            get
            {
                return Session["PersonalIdCard"] == null ? string.Empty : Session["PersonalIdCard"].ToString();
            }
            set
            {
                Session["PersonalIdCard"] = value;
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

        #endregion

        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                base.HasPermit();
                this.PersonalIdCard = this.UserProfile.IdCard;
                MenuInit();
            }
            else
            {
                if (this.MasterLicense != null)
                {

                }
            }

            this.GetPersonalSkill();
        }
        #endregion

        #region Main Public & Private Function
        private void MenuInit()
        {
            //New License
            if (this.MasterLicense.Menu == (int)(DTO.PersonLicenses.New))
            {
                GetExamTrain();

                pnlRenewPetitionName.Visible = false;
            }
            //ReNew License
            else if (this.MasterLicense.Menu == (int)(DTO.PersonLicenses.ReNew))
            {
                GetExamTrain();

                pnlRenewPetitionName.Visible = true;
                if ((this.MasterLicense.RenewPetitionName != null) && (this.MasterLicense.SelectedLicenseNo != null))
                {
                    StringBuilder strBuilder = new StringBuilder()
                        .Append(this.MasterLicense.RenewPetitionName)
                        .Append("&nbsp;เลขที่ใบอนุญาต&nbsp;:&nbsp;")
                        .Append(this.MasterLicense.SelectedLicenseNo);
                    lblRenewPetitionName.Text = strBuilder.ToString();
                }
                
            }

            //ExpireReNew License
            else if (this.MasterLicense.Menu == (int)(DTO.PersonLicenses.ExpireReNew))
            {
                GetExamTrain();
                pnlRenewPetitionName.Visible = false;
            }

            //Other License
            else if (this.MasterLicense.Menu == (int)(DTO.PersonLicenses.Other))
            {
                GetExamTrain();
                pnlRenewPetitionName.Visible = false;
            }

            else
            {
                GetExamTrain();
                pnlRenewPetitionName.Visible = false;
            }

        }

        private ResponseMessage<bool> LicenseValidation()
        {
            LicenseBiz biz = new LicenseBiz();
            ResponseMessage<bool> res = new ResponseMessage<bool>();

            foreach (DTO.PersonLicenseHead h in this.MasterLicense.PersonLicenseH)
            {
                DTO.PersonLicenseDetail details = this.MasterLicense.PersonLicenseD.Where(group => group.UPLOAD_GROUP_NO == h.UPLOAD_GROUP_NO).FirstOrDefault();
                ResponseMessage<bool> result = biz.SingleLicenseValidation(h, details);

                if ((result.IsError) || (result.ResultMessage == false))
                {
                    this.MasterLicense.UCLicenseUCLicenseModelError.ShowMessageError = result.ErrorMsg.ToString();
                    this.MasterLicense.UCLicenseUCLicenseModelError.ShowModalError();
                    this.MasterLicense.UpdatePanelLicense.Update();
                    lblResult.Text = result.ErrorMsg.ToString();
                    btnNext.Enabled = false;
                    pnlNext.Enabled = false;
                    res = result;
                    //return;
                }
                else if (result.ResultMessage == true)
                {
                    res = result;

                }

            }


            return res;

        }

        private void GetExamTrain()
        {
            LicenseBiz biz = new BLL.LicenseBiz();
            DTO.ResponseService<DTO.ExamHistory[]> resExam = biz.GetExamHistoryByIDWithCondition(this.UserProfile.IdCard, this.MasterLicense.LicenseTypeCode);
            DTO.ResponseService<DTO.TrainPersonHistory[]> resTrain = biz.GetTrainingHistoryByIDWithCondition(this.UserProfile.IdCard, this.MasterLicense.LicenseTypeCode);
            //DTO.ResponseService<DataSet> resTrain = biz.GetTrainingHistoryBy(this.UserProfile.IdCard);
            //DataTable dtTrain = resTrain.DataResponse.Tables[0];
            //DataRow resultRow = resTrain.DataResponse.Tables["dd"].AsEnumerable().Where(row => row.Field<string>("DECODE") == "ผ่าน").FirstOrDefault();

            #region Func
            Func<string, string, string> dateCompare = delegate(string input, string t)
            {
                DateTime currDate = DateTime.Now;
                DateTime expDate = Convert.ToDateTime(input);

                string currDateFormat = String.Format("{0:dd/MM/yyy}", currDate).ToString();
                string exphDateFormat = String.Format("{0:dd/MM/yyy}", expDate).ToString();
                DateTime currTime = DateTime.Parse(currDateFormat);
                DateTime expTime = DateTime.Parse(exphDateFormat);

                int resCompare = DateTime.Compare(expTime, currTime);
                if (t.Equals("exam"))
                {
                    //expTime < CurrentTime
                    if (resCompare == -1)
                    {
                        input = SysMessage.ExamResultF;
                    }
                    //expTime == CurrentTime
                    if (resCompare == 0)
                    {
                        input = SysMessage.ExamResultF;
                    }
                    //expTime > CurrentTime
                    if (resCompare == 1)
                    {
                        input = resExam.DataResponse[0].EXAM_RESULT;
                    }
                }
                else if (t.Equals("train"))
                {
                    //expTime < CurrentTime
                    if (resCompare == -1)
                    {
                        input = SysMessage.TrainResultF;
                    }
                    //expTime == CurrentTime
                    if (resCompare == 0)
                    {
                        input = SysMessage.TrainResultF;
                    }
                    //expTime > CurrentTime
                    if (resCompare == 1)
                    {
                        input = resTrain.DataResponse[0].STATUS;
                    }
                }
                return input;
            };
            #endregion
            
            if ((resExam.IsError) || (resTrain.IsError))
            {
                this.MasterLicense.UCLicenseUCLicenseModelError.ShowMessageError = resExam.ErrorMsg;
                this.MasterLicense.UCLicenseUCLicenseModelError.ShowModalError();
                this.MasterLicense.UpdatePanelLicense.Update();
                return;
            }
            else if ((resExam.DataResponse != null) && (resTrain.DataResponse != null))
            {
                this.MasterLicense.CurrentExamPerson = resExam.DataResponse.ToList();
                this.MasterLicense.CurrentTrainPerson = resTrain.DataResponse.ToList();

                if ((resExam.DataResponse.Count() > 0) && (resTrain.DataResponse.Count() > 0))
                {
                    if(!string.IsNullOrEmpty(resExam.DataResponse[0].EXAM_RESULT))
                    {
                        //lblResExam.Text = resExam.DataResponse[0].EXAM_RESULT;
                        this.curResultExam = resExam.DataResponse[0].EXAM_RESULT;
                    }
                    else if (string.IsNullOrEmpty(resExam.DataResponse[0].EXAM_RESULT))
                    {
                        //lblResExam.Text = Resources.propLicenseValidate_ResExam;
                        this.curResultExam = Resources.propLicenseValidate_ResExam;
                    }
                    //lblResTrain.Text = dateCompare(String.Format("{0:dd/MM/yyyy}", resTrain.DataResponse[0].TRAIN_DATE_EXP), "train");
                    this.curResultTrain = dateCompare(String.Format("{0:dd/MM/yyyy}", resTrain.DataResponse[0].TRAIN_DATE_EXP), "train");
                    gvHistoryExam.DataSource = resExam.DataResponse;
                    gvHistoryExam.DataBind();

                }
                else if ((resExam.DataResponse.Count() == 0) && (resTrain.DataResponse.Count() == 0))
                {
                    //btnNext.Enabled = false;
                    //pnlNext.Enabled = false;
                    btnNext.Enabled = true;
                    pnlNext.Enabled = true;
                    gvHistoryExam.DataSource = resExam.DataResponse;
                    gvHistoryExam.DataBind();
                }
                else
                {
                    if (resExam.DataResponse.Count() == 0)
                    {
                        //lblResExam.Text = Resources.propLicenseValidate_ResExam;
                        this.curResultExam = Resources.propLicenseValidate_ResExam;
                        //btnNext.Enabled = false;
                        //pnlNext.Enabled = false;
                        btnNext.Enabled = true;
                        pnlNext.Enabled = true;
                    }

                    if (resExam.DataResponse.Count() > 0)
                    {
                        //lblResExam.Text = resExam.DataResponse[0].EXAM_RESULT;
                        this.curResultExam = resExam.DataResponse[0].EXAM_RESULT;
                    }

                    if (resTrain.DataResponse.Count() == 0)
                    {
                        //lblResTrain.Text = Resources.propLicenseValidate_ResTrain;
                        this.curResultTrain = Resources.propLicenseValidate_ResTrain;
                        //btnNext.Enabled = false;
                        //pnlNext.Enabled = false;
                        btnNext.Enabled = true;
                        pnlNext.Enabled = true;
                    }

                    if (resTrain.DataResponse.Count() > 0)
                    {
                        //lblResTrain.Text = resTrain.DataResponse[0].STATUS;
                        this.curResultTrain = resTrain.DataResponse[0].STATUS;
                    }

                    gvHistoryExam.DataSource = resExam.DataResponse;
                    gvHistoryExam.DataBind();


                }

            }

        }

        private void MenuBackValidation()
        {
            if (this.MasterLicense.Menu != null)
            {
                switch (this.MasterLicense.Menu)
                {
                    case 1:
                        Session["CheckClearSession"] = "P";
                        MasterLicense.Step -= 1;
                        //Response.Redirect("../License/NewLicense.aspx?M=" + MasterLicense.Menu + "&S=" + MasterLicense.Step + "");
                        Response.Redirect("../License/LicenseAgreement.aspx?M=" + MasterLicense.Menu + "&S=" + MasterLicense.Step + "");
                        break;
                    case 2:
                        Session["CheckClearSession"] = "P";
                        MasterLicense.Step -= 1;
                        //Response.Redirect("../License/RenewLicense.aspx?M=" + MasterLicense.Menu + "&S=" + MasterLicense.Step + "");
                        Response.Redirect("../License/LicenseAgreement.aspx?M=" + MasterLicense.Menu + "&S=" + MasterLicense.Step + "");
                        break;
                    case 3:
                        Session["CheckClearSession"] = "P";
                        MasterLicense.Step -= 1;
                        //Response.Redirect("../License/ExpiredRenewLicense.aspx?M=" + MasterLicense.Menu + "&S=" + MasterLicense.Step + "");
                        Response.Redirect("../License/LicenseAgreement.aspx?M=" + MasterLicense.Menu + "&S=" + MasterLicense.Step + "");
                        break;
                    case 4:
                        Session["CheckClearSession"] = "P";
                        MasterLicense.Step -= 1;
                        //Response.Redirect("../License/LicenseReplace.aspx?M=" + MasterLicense.Menu + "&S=" + MasterLicense.Step + "");
                        Response.Redirect("../License/LicenseAgreement.aspx?M=" + MasterLicense.Menu + "&S=" + MasterLicense.Step + "");
                        break;
                }

            }
        }

        private string NullToString(string input)
        {
            if ((input == null) && (input == ""))
            {

            }

            return input;
        }

        private void GetPersonalSkill()
        {
            Func<string, string> resultConvert = delegate(string input)
            {
                if ((input == null) || (input == ""))
                {
                    input = SysMessage.TrainResultF;
                }

                return input;

            };

            this.ucPerSkills.CurrentIDCard = this.MasterLicense.UserProfile.IdCard;
            this.ucPerSkills.CurrentRegistrationID = this.MasterLicense.UserProfile.Id;
            this.ucPerSkills.LicenseTypeCode = this.MasterLicense.LicenseTypeCode;
            this.ucPerSkills.PettionTypeCode = this.MasterLicense.PettionTypeCode;
            this.ucPerSkills.CurrentLicenseRenewTime = this.MasterLicense.CurrentLicenseRenewTime;
            this.ucPerSkills.curResultExam = resultConvert(this.curResultExam);
            this.ucPerSkills.curResultTrain = resultConvert(this.curResultTrain);
            this.ucPerSkills.Mode = DTO.LicensePropMode.General.GetEnumValue().ToString();
            //this.ucPerSkills.Rule7 = this.Rule7;
            //this.ucPerSkills.Rule8 = this.Rule8;
            //this.ucPerSkills.Rule9 = this.Rule9;

            //this.ucPerSkills.ucInit();
        }
        #endregion

        #region UI Function
        /// <summary>
        /// Last Update 09/06/57
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNext_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                //MasterPage
                //ตรวจสอบ PettionTypeCode กรณีออกใบแทน PettionType=16
                if ((this.MasterLicense.PettionTypeCode != null) && (this.MasterLicense.PettionTypeCode != "")
                    && (this.MasterLicense.PettionTypeCode.Equals(Convert.ToString((int)DTO.PettionCode.OtherLicense_1))))
                {
                    MasterLicense.Step += 1;
                    //Response.Redirect("../License/LicenseAttatchFiles.aspx?M=" + MasterLicense.Menu + "&S=" + MasterLicense.Step + "");
                    Response.Redirect("../License/LicenseContinue.aspx?M=" + MasterLicense.Menu + "&S=" + MasterLicense.Step + "");
                }
                else
                {
                    //ResponseMessage<bool> res = this.MasterLicense.LicenseValidation();
                    ResponseMessage<bool> res = this.ucPerSkills.LicenseValidation();
                    if ((res.ResultMessage == false) || (res.IsError))
                    {

                        //Remove Current Entity from List
                        int HkeyIndex = this.MasterLicense.PersonLicenseH.FindIndex(h => h.LICENSE_TYPE_CODE == this.MasterLicense.CurrentPersonLicenseH.LICENSE_TYPE_CODE
                            && h.PETITION_TYPE_CODE == this.MasterLicense.CurrentPersonLicenseH.PETITION_TYPE_CODE
                            && h.UPLOAD_GROUP_NO == this.MasterLicense.CurrentPersonLicenseH.UPLOAD_GROUP_NO);
                        int DkeyIndex = this.MasterLicense.PersonLicenseD.FindIndex(d => d.UPLOAD_GROUP_NO == this.MasterLicense.CurrentPersonLicenseD.UPLOAD_GROUP_NO);
                        if ((HkeyIndex >= 0) && (DkeyIndex >= 0))
                        {
                            this.MasterLicense.PersonLicenseH.RemoveAt(HkeyIndex);
                            this.MasterLicense.PersonLicenseD.RemoveAt(DkeyIndex);
                        }

                        this.MasterLicense.UCLicenseUCLicenseModelError.ShowMessageError = res.ErrorMsg;
                        this.MasterLicense.UCLicenseUCLicenseModelError.ShowModalError();
                        this.MasterLicense.UpdatePanelLicense.Update();
                        return;
                    }
                    else
                    {
                        MasterLicense.Step += 1;
                        Response.Redirect("../License/LicenseContinue.aspx?M=" + MasterLicense.Menu + "&S=" + MasterLicense.Step + "");
                    }

                }
            }
            catch (Exception ex)
            {


            }
        }

        protected void btnBack_Click(object sender, ImageClickEventArgs e)
        {
            this.MenuBackValidation();
        }

        protected void TabDetail_ActiveTabChanged(object sender, EventArgs e)
        {
            try
            {
                LicenseBiz biz = new BLL.LicenseBiz();
                //Tab 1 = ประวัติการสอบ
                if (TabDetail.ActiveTabIndex == 0)
                {
                    //var res = biz.GetExamHistoryByID(this.UserProfile.IdCard);
                    if (this.MasterLicense.CurrentExamPerson != null)
                    {
                        gvHistoryExam.DataSource = this.MasterLicense.CurrentExamPerson;
                        gvHistoryExam.DataBind();
                    }
                    else
                    {

                        DTO.ResponseService<DTO.ExamHistory[]> res = biz.GetExamHistoryByIDWithCondition(this.UserProfile.IdCard, this.MasterLicense.LicenseTypeCode);
                        if (res.IsError)
                        {

                            this.MasterLicense.UCLicenseUCLicenseModelError.ShowMessageError = res.ErrorMsg;
                            this.MasterLicense.UCLicenseUCLicenseModelError.ShowModalError();
                            this.MasterLicense.UpdatePanelLicense.Update();
                            return;
                        }
                        else
                        {
                            gvHistoryExam.DataSource = res.DataResponse;
                            gvHistoryExam.DataBind();
                        }
                    }

                    //udpMain.Update();

                }
                //Tab 2 = ประวัติการอบรม
                else if (TabDetail.ActiveTabIndex == 1)
                {
                    //Old
                    //var res = biz.GetTrainingHistoryByID(this.UserProfile.IdCard);
                    if (this.MasterLicense.CurrentTrainPerson != null)
                    {
                        gvHistoryTraining.DataSource = this.MasterLicense.CurrentTrainPerson;
                        gvHistoryTraining.DataBind();
                    }
                    else
                    {
                        DTO.ResponseService<DTO.TrainPersonHistory[]> res = biz.GetTrainingHistoryByIDWithCondition(this.UserProfile.IdCard, this.MasterLicense.LicenseTypeCode);
                        if ((res.IsError) || (res.DataResponse == null))
                        {
                            this.MasterLicense.UCLicenseUCLicenseModelError.ShowMessageError = res.ErrorMsg;
                            this.MasterLicense.UCLicenseUCLicenseModelError.ShowModalError();
                            this.MasterLicense.UpdatePanelLicense.Update();
                            return;
                        }
                        else
                        {
                            gvHistoryTraining.DataSource = res.DataResponse;
                            gvHistoryTraining.DataBind();

                        }
                    }


                }
                //Tab 3 = คุณสมบัติ 7(1)-(3)
                else if (TabDetail.ActiveTabIndex == 2)
                {
                    var res = biz.GetTrain_3_To_7_ByID(this.UserProfile.IdCard);
                    if (res.IsError)
                    {
                        this.MasterLicense.UCLicenseUCLicenseModelError.ShowMessageError = res.ErrorMsg;
                        this.MasterLicense.UCLicenseUCLicenseModelError.ShowModalError();
                        this.MasterLicense.UpdatePanelLicense.Update();
                        return;
                    }
                    else
                    {
                        gvTraining.DataSource = res.DataResponse;
                        gvTraining.DataBind();

                        //udpMain.Update();
                    }

                }
                //Tab 4 = ประวัติการอบรม Unit Link
                else if (TabDetail.ActiveTabIndex == 3)
                {
                    var res = biz.GetUnitLinkByIDWithCondition(this.UserProfile.IdCard, this.MasterLicense.LicenseTypeCode);
                    if (res.IsError)
                    {
                        this.MasterLicense.UCLicenseUCLicenseModelError.ShowMessageError = res.ErrorMsg;
                        this.MasterLicense.UCLicenseUCLicenseModelError.ShowModalError();
                        this.MasterLicense.UpdatePanelLicense.Update();
                        return;
                    }
                    else
                    {
                        gvUnitLink.DataSource = res.DataResponse;
                        gvUnitLink.DataBind();

                        //udpMain.Update();
                    }

                }

                //this.ucPerSkills.GetPersonalSkill(this.ucPerSkills.LicenseTypeCode, this.ucPerSkills.PettionTypeCode, this.ucPerSkills.CurrentLicenseRenewTime);

                udpMain.Update();
            }
            catch
            {
                ApplicationException ex = new ApplicationException();
                this.MasterLicense.UCLicenseUCLicenseModelError.ShowMessageError = ex.Message;
                this.MasterLicense.UCLicenseUCLicenseModelError.ShowModalError();
                return;
            }
        }

        protected void btnSubmit_Click(object sender, ImageClickEventArgs e)
        {
            if (this.chkRuleID7.Checked == true)
            {
                this.Rule7 = "1";
            }
            else
            {
                this.Rule7 = "0";
            }

            if (this.chkRuleID8.Checked == true)
            {
                this.Rule8 = "1";
            }
            else
            {
                this.Rule8 = "0";
            }

            if (this.chkRuleID9.Checked == true)
            {
                this.Rule9 = "1";
            }
            else
            {
                this.Rule9 = "0";
            }

            this.GetPersonalSkill();

            this.pnlChk.Visible = false;
            this.pnlMain.Visible = true;

        }
        #endregion


    }
}