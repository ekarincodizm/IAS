using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using IAS.Utils;
using IAS.DTO;
using System.Text;
using System.Net;
using System.Data;
using IAS.Properties;
using IAS.BLL;

namespace IAS.Exam
{
    public partial class GroupApplicantDetail : basepage
    {
        public SummaryReceiveApplicant SummaryReceiveApplicant
        {
            get
            {
                return (Session["SummaryReceiveApplicant"] == null) ? CreateSummaryReceiveApplicant()
                                                                    : (SummaryReceiveApplicant)Session["SummaryReceiveApplicant"];
            }
        }
        private SummaryReceiveApplicant CreateSummaryReceiveApplicant()
        {
            SummaryReceiveApplicant summary = new SummaryReceiveApplicant();
            summary.Header = new DTO.UploadHeader();
            summary.ReceiveApplicantDetails = new List<ApplicantTemp>();
            summary.UploadGroupNo = "";
            summary.MessageError = "";

            return summary;
        }

        public IAS.MasterPage.Site1 MasterSite
        {
            get
            {
                return (this.Page.Master as IAS.MasterPage.Site1);
            }
        }

        private void GetTitleName()
        {
            var message = SysMessage.DefaultSelecting;
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetTitleName(message);
            BindToDDL(ddlTitle, ls);
        }

        public void GetEducation()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetEducation(SysMessage.DefaultSelecting);
            BindToDDL(ddlEducation, ls);
        }

        private void GetCompanyNameById()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetCompanyNameById(base.UserProfile.CompCode);
            ddlCompany.Items.Insert(0, ls);
        }

        private void GetCompanyByLicenseType(string strLicenTypeCode)
        {


            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            List<string> lst = biz.GetCompanyCodeByName("");
            Session["lsCompCode"] = lst;

            List<string> ls = (List<string>)Session["lsCompCode"];
            List<String> ls2;
            switch (strLicenTypeCode)
            {
                case "01":
                case "07": ls2 = ls.Where(l => l.Contains("[1")).ToList();
                    break;
                case "03":
                case "04": ls2 = ls.Where(l => l.Contains("[3")).ToList();
                    break;
                case "02":
                case "05":
                case "06":
                case "08": ls2 = ls.Where(l => l.Contains("[2")).ToList();
                    break;
                default: ls2 = ls;
                    break;
            }
            ddlCompany.DataSource = ls2;
            ddlCompany.DataBind();
            ddlCompany.Items.Insert(0, new ListItem(SysMessage.DefaultSelecting, "0"));
        }

        private void CreateCSVFile()
        {

            string strExamNumber = Convert.ToString(Session["ExamNumber"]);
            string strExamTime = Convert.ToString(Session["ExamTime"]);
            string strExamPlaceGroup = Convert.ToString(Session["ExamPlaceGroup"]);
            string strExamPlace = Convert.ToString(Session["ExamPlace"]);
            string strProvince = Convert.ToString(Session["Province"]);
            string strSeat = Convert.ToString(Session["Seat"]);
            string strLicenseTypeName = Convert.ToString(Session["LicenseTypeName"]);
            string strExamFee = Convert.ToString(Session["ExamFee"]);
            string strAgentType = Convert.ToString(Session["AgentType"]);
            string strPlaceCode = Convert.ToString(Session["PlaceCode"]);

            string strTestingNo = Convert.ToString(Session["TestingNo"]);
            string strTestingDate = Convert.ToString(Session["TestingDate"]);
            string strTestTimeCode = Convert.ToString(Session["TestTimeCode"]);
            string strLicenseTypeCode = Convert.ToString(Session["LicenseTypeCode"]);
            string strProvineCode = Convert.ToString(Session["ProvinceCode"]);
            string strExamPlaceGroupCode = Convert.ToString(Session["ExamPlaceGroupCode"]);

            DateTime dtTestingDate = Convert.ToDateTime(strTestingDate);

            var arrComp = ddlCompany.SelectedValue.Split('[', ']');
            ExamScheduleBiz biz = new ExamScheduleBiz();
            List<DTO.ExamLicense> lsExamLicense = new List<ExamLicense>();
            DTO.ExamLicense examLicense = new ExamLicense();
            examLicense.TESTING_DATE = dtTestingDate;
            examLicense.TESTING_NO = strTestingNo;
            lsExamLicense.Add(examLicense);

            DateTime dtStart = DateTime.Today.AddDays(-3);
            int iCom = DateTime.Compare(dtStart, dtTestingDate);
            if (iCom == 0 || iCom == 1)
            {
                this.MasterSite.ModelError.ShowMessageError = "ต้องนำเข้าก่อน 3 วันทำการสอบ ";
                this.MasterSite.ModelError.ShowModalError();
            }
            var exam = biz.UpdateExamCondition(lsExamLicense);
            if (exam.ResultMessage == false)
            {
                this.MasterSite.ModelError.ShowMessageError = exam.ErrorMsg;
                this.MasterSite.ModelError.ShowModalError();
            }
            string examfee = biz.GetExamFee().DataResponse; ;

            using (var stream = new MemoryStream())
            {
                using (var csvWriter = new StreamWriter(stream, Encoding.UTF8))
                {
                    csvWriter.WriteLine("RECORD TYPE,จังหวัด,รหัสสนามสอบ,ประเภทใบอนุญาต,วัน/เดือน/ปี,จำนวนคน,จำนวนเงินทั้งหมด,เวลาสอบ");
                    csvWriter.WriteLine("H" + "," + strProvineCode + "," + strExamPlaceGroupCode + "," + strLicenseTypeCode + "," + strTestingDate + "," + "1" + "," + examfee + "," + strTestTimeCode);
                    csvWriter.WriteLine("เลขที่สอบ,หมายเลขบัตรประชาชน,คำนำหน้า,ชื่อ,นามสกุล,วัน/เดือน/ปี,เพศ,วุฒิการศึกษา,รหัสบริษัท,ที่อยู่,รหัสพื้นที่,หน่วยรับสมัคร");
                    //csvWriter.WriteLine("1" + "," + txtIDCard.Text + "," + ddlTitle.SelectedItem.Text + "," + txtFirstNane.Text + "," + txtLastName.Text + "," + txtBirthDay.Text + "," + rblSex.SelectedValue + "," + ddlEducation.SelectedValue == "0" ? null : ddlEducation.SelectedValue + "," + arrComp[1] + "," + txtAddress.Text + "," + txtAreaCode.Text);
                    csvWriter.WriteLine("1" + "," + txtIDCard.Text + "," + ddlTitle.SelectedItem.Text + "," + txtFirstNane.Text + "," + txtLastName.Text + "," + txtBirthDay.Text + "," + rblSex.SelectedValue + "," + ddlEducation.SelectedValue + "," + arrComp[1] + "," + txtAddress.Text + "," + txtAreaCode.Text);
                    csvWriter.Flush();
                }

                MemoryStream memoryStream = new MemoryStream(stream.ToArray());
                LoadFileSingle(memoryStream);
            }


        }


        private Action<DropDownList, DTO.DataItem[]> BindToDDLAr = (ddl, ls) =>
        {
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Id";
            ddl.DataSource = ls;
            ddl.DataBind();
        };

        /// <summary>
        /// This is used to file as CSV.
        /// </summary>
        public void SaveCSVFile(string fileName, string csvContentStr)
        {
            try
            {
                fileName = fileName + "_" + String.Format("{0:MMMM}", DateTime.Today) + "_" + String.Format("{0:yyyy}", DateTime.Today);

                Response.Clear();
                // This is content type for CSV.
                Response.ContentType = "Text/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=\"" + fileName + ".csv\"");

                Response.Write(csvContentStr); //Here Content write in page.
            }
            finally
            {
                Response.End();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TestingNo))
            {
                txtExamNumber.Text = TestingNo;
            }
            if (!IsPostBack)
            {
                GetTitleName();
                GetEducation();
                GetLicense();
                if (base.UserProfile.MemberType == DTO.RegistrationType.Insurance.GetEnumValue())
                {
                    GetCompanyNameById();
                }
                if (base.UserProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue())
                {
                    string strLicenseTypeCode = Convert.ToString(Session["LicenseTypeCode"]);
                    GetCompanyByLicenseType(strLicenseTypeCode);
                }
                ucPersonalApplicantDetail1.GetTitleName();
                ucPersonalApplicantDetail1.GetEducation();
            }

            linkApplicantFile.OnClientClick = String.Format("window.open('{0}','','')"
                            , UrlHelper.Resolve("/UserControl/FileApplicantUpload.aspx"));
        }

        private void GetLicense()
        {
            try
            {
                BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
                string temp = biz.GetLicensefromTestingNo(TestingNo).DataResponse;
                Session["LicenseTypeCode"] = (Convert.ToString(Session["LicenseTypeCode"]) == "") ? temp : Convert.ToString(Session["LicenseTypeCode"]);
            }
            catch
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

        public string TestingNo
        {
            get
            {
                return (string)Session["TestingNo"];
            }
        }

        public string ExamPlaceCode
        {
            get
            {
                return (string)Session["PlaceCode"];
            }

        }

        public string TempFolderOracle
        {
            get
            {
                return (string)Session["TempFolderOracle"];
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

        private void CanSubmitData()
        {
            Boolean result = false;
            if (!String.IsNullOrEmpty(SummaryReceiveApplicant.MessageError))
            {
                result = false;
            }
            else
            {
                Int32 trueAmount = SummaryReceiveApplicant.ReceiveApplicantDetails.Count(a => a.LOAD_STATUS == "T");
                if (trueAmount == SummaryReceiveApplicant.ReceiveApplicantDetails.Count())
                {
                    result = true;
                }
                else result = false;
            }


            btnImport.Visible = result;
            btnImportCancel.Visible = result;
            btnImport.Enabled = result;
            btnImportCancel.Enabled = result;

            lblErrMessage.Text = Resources.propGroupApplicantDetail_ErrMessage;
            lblErrMessage.Visible = (!result);
        }
        public void ClearSummaryApplicant()
        {
            Session["SummaryReceiveApplicant"] = null;
            pnlImportFile.Visible = false;
        }
        protected void btnLoadFile_Click(object sender, EventArgs e)
        {
            divSingle.Visible = false;
            string tempFileName = Path.GetFileName(FuFile.PostedFile.FileName);
            bool invalid = validateFileType(tempFileName);
            ClearSummaryApplicant();
            if (!string.IsNullOrEmpty(FuFile.FileName))
            {

                if (invalid)
                {
                    BLL.ApplicantBiz biz = new BLL.ApplicantBiz();
                    var res = biz.UploadData(FuFile.FileName, FuFile.PostedFile.InputStream, TestingNo, ExamPlaceCode, UserId, base.UserProfile);
                    if (!res.IsError)
                    {
                        Session["SummaryReceiveApplicant"] = res.DataResponse;
                        pnlImportFile.Visible = true;
                        List<DTO.UploadHeader> headers = new List<UploadHeader>();
                        headers.Add(res.DataResponse.Header);
                        gvImportFile.DataSource = headers;
                        gvImportFile.DataBind();

                        hdfGroupID.Value = res.DataResponse.UploadGroupNo;
                        var detail = res.DataResponse.ReceiveApplicantDetails;

                        gvCheckList.DataSource = res.DataResponse.ReceiveApplicantDetails;
                        gvCheckList.DataBind();
                        CanSubmitData();
                    }
                    else
                    {
                        var errorMsg = res.ErrorMsg;

                        this.MasterSite.ModelError.ShowMessageError = res.ErrorMsg;
                        this.MasterSite.ModelError.ShowModalError();
                    }
                }

                //check type 15/11/2556
                else
                {
                    this.MasterSite.ModelError.ShowMessageError = SysMessage.CannotWrongUploadFile;
                    this.MasterSite.ModelError.ShowModalError();
                }
            }
            else
            {
                this.MasterSite.ModelError.ShowMessageError = SysMessage.CannotUploadFile;
                this.MasterSite.ModelError.ShowModalError();
            }

        }

        private static bool validateFileType(string tempFileName)
        {
            string fileExtension = System.IO.Path.GetExtension(tempFileName).Replace(".", string.Empty).ToLower();
            bool invalidFileExtensions = DTO.DocumentFileCSV.CSV.ToString().ToLower().Contains(fileExtension);
            return invalidFileExtensions;
        }

        protected void gvCheckList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label error = ((Label)e.Row.FindControl("lblDetailGv"));
                LinkButton view = ((LinkButton)e.Row.FindControl("hplview"));

                if (error.Text != "")
                {
                    view.Visible = true;
                }
                else
                {
                    view.Visible = false;
                }
            }
        }

        protected void hplView_Click(object sender, EventArgs e)
        {
            var strGroupID = hdfGroupID.Value;
            var gv = (GridViewRow)((LinkButton)sender).NamingContainer;
            Label strSeqNo = (Label)gv.FindControl("lblSeqNoGv");
            if (!string.IsNullOrEmpty(strSeqNo.Text))
            {
                hdfSegNo.Value = strSeqNo.Text;
                ucPersonalApplicantDetail1.PersonApplicantReadOnlyMode();
                ucPersonalApplicantDetail1.BindApplicantUploadTempByID(strGroupID, strSeqNo.Text);
                ModGroupApplicant.Show();
                pnlModal.Visible = true;
            }

        }

        private void ImportInsertMode()
        {
            BLL.ApplicantBiz biz = new BLL.ApplicantBiz();
            ApplicantTemp app = new ApplicantTemp();

            var result = biz.ApplicantGroupUploadToSubmit(hdfGroupID.Value, base.UserProfile);
            if (result.ErrorMsg == null)
            {
                string strResult = result.DataResponse;
                this.MasterSite.ModelSuccess.ShowMessageSuccess = Resources.infoGroupApplicantDetail_001;
                this.MasterSite.ModelSuccess.ShowModalSuccess();
                pnlImportFile.Visible = false;
                CleanData();
                ClearControlSingle();
            }
            else
            {
                var errorMsg = result.ErrorMsg;
                this.MasterSite.ModelError.Visible = true;
                ReBindDatainGrid(hdfGroupID.Value);
                this.MasterSite.ModelError.ShowMessageError = result.ErrorMsg;
                this.MasterSite.ModelError.ShowModalError();
            }
        }

        private void ReBindDatainGrid(string strGroupUploadNo)
        {
            BLL.ApplicantBiz biz = new BLL.ApplicantBiz();

            var res = biz.GetApplicantGroupUploadByGroupUploadNo(strGroupUploadNo);
            if (!res.IsError)
            {
                gvImportFile.DataSource = res.DataResponse.Header;
                gvImportFile.DataBind();

                gvCheckList.DataSource = res.DataResponse.Detail;
                gvCheckList.DataBind();
            }
            else
            {
                var errorMsg = res.ErrorMsg;
                this.MasterSite.ModelError.ShowMessageError = res.ErrorMsg;
                this.MasterSite.ModelError.ShowModalError();
            }

        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            //ValidateBeforeSubmit();
            ImportInsertMode();
        }


        protected void btnClear_Click(object sender, EventArgs e)
        {
            CleanData();

            Response.Redirect("~/Applicant/CompanyAndAssocImport.aspx");
        }

        private void CleanData()
        {
            hdfGroupID.Value = string.Empty;
            hdfSegNo.Value = string.Empty;
        }

        private void ClearControlSingle()
        {
            txtIDCard.Text = string.Empty;
            txtFirstNane.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtBirthDay.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtAreaCode.Text = string.Empty;
        }


        protected void btnImportCancel_Click(object sender, EventArgs e)
        {
            CleanData();
            Response.Redirect("~/Applicant/SingleApplicant.aspx");
        }

        protected void gvImportFile_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblItemsIncorrectRemarkGv = (Label)e.Row.FindControl("lblItemsIncorrectRemark");

                if (lblItemsIncorrectRemarkGv != null)
                {
                    lblItemsIncorrectRemarkGv.Text = SummaryReceiveApplicant.MessageError;
                }

            }
        }

        protected void btnDownloadCSV_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void webClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {

        }

        protected void webClient_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            this.MasterSite.ModelSuccess.ShowMessageSuccess = Resources.infoGroupApplicantDetail_002;
            this.MasterSite.ModelSuccess.ShowModalSuccess();
        }

        protected void ddlTitle_SelectedIndexChanged(object sender, EventArgs e)
        {
            string resSex = Utils.GetTitleName.GetSex(ddlTitle.SelectedItem.Text);
            if ((resSex != null) && (resSex != ""))
            {
                if (resSex.Equals("M"))
                {
                    rblSex.SelectedValue = "ช";
                }
                if (resSex.Equals("F"))
                {
                    rblSex.SelectedValue = "ญ";
                }
            }
            else
            {
                //Enable for select Sex
                rblSex.SelectedValue = null;
                rblSex.Enabled = true;
            }
        }

        protected void btnLoadFileSingle_Click(object sender, EventArgs e)
        {

            divGroup.Visible = false;
            CreateCSVFile();

        }

        private Boolean IsPersonCanApplicant()
        {

            DTO.IsPersonCanApplicantRequest request = new IsPersonCanApplicantRequest() { IdCardNumber = txtIDCard.Text.Trim(), TestingNo = txtExamNumber.Text.Trim() };
            BLL.ApplicantBiz biz = new BLL.ApplicantBiz();

            return biz.IsPersonCanApplicant(request);
        }

        private void LoadFileSingle(MemoryStream memberStream)
        {
            bool CanSaveToList = ValidateBeforeSaveList();
            if (CanSaveToList)
            {
                if (true)
                {

                    if (true)
                    {
                        BLL.ApplicantBiz biz = new BLL.ApplicantBiz();
                        var res = biz.UploadData("CSVFileName", memberStream, Convert.ToString(Session["TestingNo"]), ExamPlaceCode, UserId, base.UserProfile);
                        if (!res.IsError)
                        {
                            Session["SummaryReceiveApplicant"] = res.DataResponse;
                            pnlImportFile.Visible = true;
                            List<DTO.UploadHeader> headers = new List<UploadHeader>();
                            headers.Add(res.DataResponse.Header);
                            gvImportFile.DataSource = headers;
                            gvImportFile.DataBind();

                            hdfGroupID.Value = res.DataResponse.UploadGroupNo;
                            var detail = res.DataResponse.ReceiveApplicantDetails;

                            gvCheckList.DataSource = res.DataResponse.ReceiveApplicantDetails;
                            gvCheckList.DataBind();
                            CanSubmitData();
                        }
                        else
                        {
                            var errorMsg = res.ErrorMsg;

                            this.MasterSite.ModelError.ShowMessageError = res.ErrorMsg;
                            this.MasterSite.ModelError.ShowModalError();
                        }
                    }

                    //check type 15/11/2556
                    else
                    {
                        this.MasterSite.ModelError.ShowMessageError = SysMessage.CannotWrongUploadFile;
                        this.MasterSite.ModelError.ShowModalError();
                    }
                }
                else
                {
                    this.MasterSite.ModelError.ShowMessageError = SysMessage.CannotUploadFile;
                    this.MasterSite.ModelError.ShowModalError();
                }
            }


        }

        private bool ValidateBeforeSaveList()
        {
            string strPlaceCode = Session["ExamPlaceCode"].ToString();
            string strExamNumber = Session["TestingNo"].ToString();
            string strLicenseTypeCode = Session["LicenseTypeCode"].ToString();
            string strTestTimeCode = Session["TestTimeCode"].ToString();
            DateTime dtTestingDate = Convert.ToDateTime(Session["TestingDate"].ToString());


            bool check = true;
            ApplicantBiz abiz = new ApplicantBiz();
            var checkIsDup = abiz.CheckApplicantIsDuplicate(txtExamNumber.Text, base.UserProfile.IdCard, dtTestingDate, strTestTimeCode, strPlaceCode);

            // สอบซ้ำ
            if (checkIsDup.ResultMessage == true)
            {
                this.MasterSite.ModelError.ShowMessageError = "ทำรายการสอบซ้ำ";
                this.MasterSite.ModelError.ShowModalError();
                return check = false;
            }


            ExamScheduleBiz ebiz = new ExamScheduleBiz();
            DTO.ResponseMessage<bool> IsCanSeat = ebiz.IsCanSeatRemainSingle(strExamNumber, strPlaceCode);

            // จำนวนคนเกินรอบสอบ
            if (!IsCanSeat.ResultMessage)
            {
                this.MasterSite.ModelError.ShowMessageError = "ห้องเต็ม";
                this.MasterSite.ModelError.ShowModalError();
                return check = false;
            }
            return check;
        }

        private void ValidateBeforeSubmit()
        {
            DTO.ResponseService<DTO.SummaryReceiveApplicant> res = new DTO.ResponseService<DTO.SummaryReceiveApplicant>();
            res.DataResponse = (DTO.SummaryReceiveApplicant)Session["SummaryReceiveApplicant"];
            List<DTO.ApplicantTemp> lst = new List<ApplicantTemp>();
            res.DataResponse.ReceiveApplicantDetails.ToList().ForEach(x =>
                {
                    DTO.ApplicantTemp app = new ApplicantTemp();
                    app.APPLICANT_CODE = x.APPLICANT_CODE;
                    app.TESTING_NO = x.TESTING_NO;
                    app.TESTING_DATE = x.TESTING_DATE;
                    app.EXAM_PLACE_CODE = x.EXAM_PLACE_CODE;
                    app.APPLY_DATE = DateTime.Today;
                    app.INSUR_COMP_CODE = x.INSUR_COMP_CODE;
                    app.USER_ID = base.UserId;
                    app.ID_CARD_NO = base.IdCard;
                    app.APPLY_DATE = x.APPLY_DATE;
                    //app.RUN_NO = Convert.ToString(x + 1);
                    lst.Add(app);
                });
            ApplicantBiz biz = new ApplicantBiz();
            var checkBeforeSubmit = biz.ValidateApplicantSingleBeforeSubmit(lst);
            if (checkBeforeSubmit.ResultMessage == true)
            {
                ModalPopupExtenderListExam.Show();
            }
            else
            {
                ImportInsertMode();
            }

        }

        protected void btnConfirmExamList_Click(object sender, EventArgs e)
        {
            ImportInsertMode();
        }

    }
}
