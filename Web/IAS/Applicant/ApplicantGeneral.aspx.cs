using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.Common.Logging;
using IAS.DTO;
using IAS.Utils;
using IAS.BLL;
using System.Data;

namespace IAS.Applicant
{
    public partial class ApplicantGeneral:basepage
    {
        #region Private

        #endregion
        #region Properties
        public String UserIdRequest
        {
            get { return (ViewState["UserIdRequest"] == null) ? "" : ViewState["UserIdRequest"].ToString(); }
            set { ViewState["UserIdRequest"] = value; }
        }
        public IEnumerable<DateTime> ExamSchedules
        {
            get { return (ViewState["ExamSchedules"] == null) ? new List<DateTime>() : (IEnumerable<DateTime>)ViewState["ExamSchedules"]; }
            set { ViewState["ExamSchedules"] = value; }
        }
        public IEnumerable<GBHoliday> Holidays
        {
            get { return (ViewState["Holidays"] == null) ? new List<GBHoliday>() : (IEnumerable<GBHoliday>)ViewState["Holidays"]; }
            set { ViewState["Holidays"] = value; }
        }

        public IEnumerable<DTO.Exams.ExamInfoDTO> ExamInfos
        {
            get { return (ViewState["ExamInfos"] == null) ? new List<DTO.Exams.ExamInfoDTO>() : (IEnumerable<DTO.Exams.ExamInfoDTO>)ViewState["ExamInfos"]; }
            set { ViewState["ExamInfos"] = value; }
        }

        public List<DTO.AddApplicant> ListApplicant
        {
            get
            {
                if (Session["listApplicant"] == null)
                {
                    Session["listApplicant"] = new List<DTO.AddApplicant>();
                }

                return (List<DTO.AddApplicant>)Session["listApplicant"];
            }
            set
            {
                Session["listApplicant"] = value;
            }
        }

        protected Int32 MaxDisplayRecord
        {
            get
            {
                return (System.Configuration.ConfigurationManager.AppSettings["PAGE_SIZE"] != null)
                    ? Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PAGE_SIZE"])
                    : 5;
            }
        }

        protected PagingInfo PageInfo
        {
            get { return (ViewState["PageInfo"] == null) ? new PagingInfo() : (PagingInfo)ViewState["PageInfo"]; }
            set { ViewState["PageInfo"] = value; }
        }
        #endregion

        BLL.DataCenterBiz centerbiz = new BLL.DataCenterBiz();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {     
                ListApplicant = new List<AddApplicant>();
                UserIdRequest = UserProfile.Id ;// "140324110012352"; 
                DateTime initDate = DateTime.Now;
                Init(UserIdRequest, initDate);
                SwitchModeDisplay();
                Session["lsCompCode"] = centerbiz.GetCompanyCodeByName("");
                
            }

        }



        #region Init Component
        public void ResetInit()
        {
            DateTime initDate = DateTime.Now;
            Init(UserIdRequest, initDate);
            SelectDataFrombase();
            BindExamScheduleByTable();
        }
        public void ResetInit(DateTime initDate)
        {
            Init(UserIdRequest, initDate);
            SelectDataFrombase();
            BindExamScheduleByTable();
        }
        protected void Init(String userId, DateTime initDate)
        {
            cldExam.VisibleDate = initDate;
            pnlCalendar.Visible = true;
            pnlSearch.Visible = true;
            pnlTable.Visible = true;

            BLL.ExamScheduleBiz examBiz = new ExamScheduleBiz();
            DTO.Exams.CarlendarExamInitRequest initRequest = new DTO.Exams.CarlendarExamInitRequest()
            {
                FirstItemLicenseType = "",
                FirstItemExamPlaceGroup = "เลือก",
                FirstItemExamPlace = "",
                FirstItemExamTime = "เลือก",
                UserId = UserIdRequest
            };

            var res = examBiz.CarlendarExamInit(initRequest);
            BindToDDL(ddlTypeLicense, res.DataResponse.LicenseTypes);
            BindToDDL(ddlPlaceGroup, res.DataResponse.ExamPlaceGroups);
            BindToDDL(ddlTime, res.DataResponse.ExamTimes);
            txtYear.Text = cldExam.VisibleDate.ToString("yyyy", System.Globalization.CultureInfo.CreateSpecificCulture("th-TH"));
            GetMonth();

        }

        private void GetExamPlace()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetExamPlaceByCompCode(ddlPlaceGroup.SelectedIndex == 0 ? "" : ddlPlaceGroup.SelectedItem.Value, ddlPlaceGroup.SelectedItem.Value);
            BindToDDL(ddlPlace, ls.DataResponse); 
            ddlPlace.Items.Insert(0, new System.Web.UI.WebControls.ListItem(SysMessage.DefaultSelecting, ""));
        }

        private void GetMonth()
        {
            var list = IAS.Utils.DateUtil.GetMonthList(SysMessage.DefaultSelecting);
            ddlMonth.DataTextField = "Name";
            ddlMonth.DataValueField = "Id";
            ddlMonth.DataSource = list;
            ddlMonth.DataBind();
            ddlMonth.SelectedValue = string.Format("{0:00}", DateTime.Now.Month);
        }
        private Action<DropDownList, IEnumerable<DTO.DataItem>> BindToDDL = (ddl, ls) =>
        {
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Id";
            ddl.DataSource = ls;
            ddl.DataBind();
        };

        private PagingInfo CreatePageInfo()
        {
            return new PagingInfo()
            {
                ItemsPerPage = MaxDisplayRecord
                ,
                TotalItems = ExamInfos.Count()
                ,
                CurrentPage = 1
            };

        }

        #endregion

        #region Functional

        private DateTime GetSearchMonth()
        {

            try
            {
                DateTime result = new DateTime(txtYear.Text.ToInt() - 543, ddlMonth.SelectedValue.ToInt(), 1);
                return result;
            }
            catch (Exception ex)
            {
                LoggerFactory.CreateLog().LogError(String.Format("ไม่สามารถแปลงวันที่ได้ {0}/{1}/{2}", (txtYear.Text.ToInt() - 543).ToString(), ddlMonth.SelectedValue, "1"));
                return (cldExam.SelectedDate != null) ? cldExam.SelectedDate : DateTime.Now;
            }

        }
        private void BindExamScheduleByTable()
        {
            BindExamScheduleByTable(txtYear.Text, ddlMonth.SelectedValue, "");
        }
        private void BindExamScheduleByTable(String year, String month, String day)
        {

            //DateTime date = new DateTime(

            BLL.ExamScheduleBiz biz = new BLL.ExamScheduleBiz();
            var ls = biz.GetExamScheduleByCriteria(ddlPlaceGroup.SelectedValue, ddlPlace.SelectedValue, ddlTypeLicense.SelectedValue,
                    "", year, month, day, ddlTime.SelectedValue, txtCurrentPage.Text, txtPageSize.Text, lblTotalItems.Text, UserIdRequest, "");  
            ExamInfos = ls.DataResponse.ExamInfos;
            gvExamSchedule.DataSource = ExamInfos;
            gvExamSchedule.DataBind();  
 
        }
        private void SelectDataFrombase()
        {
            try
            {
                DateTime visibleMonth = new DateTime((txtYear.Text.ToInt() - 543), ddlMonth.SelectedValue.ToInt(), 1);
                BLL.ExamScheduleBiz biz = new BLL.ExamScheduleBiz();
                Func<string, string> GetCrit = anyString =>
                {
                    return string.IsNullOrEmpty(anyString) ? string.Empty : anyString;
                };

                string strTime = GetCrit(ddlTime.SelectedIndex == 0 ? "" : ddlTime.SelectedValue);    

                DTO.Exams.GetExamByCriteriaRequest request = new DTO.Exams.GetExamByCriteriaRequest();
                request.ExamPlaceGroupCode = ddlPlaceGroup.SelectedValue; // strExamPlaceGroup;
                request.ExamPlaceCode = ddlPlace.SelectedValue;
                request.LicenseTypeCode = ddlTypeLicense.SelectedValue;
                request.Year = visibleMonth.Year;
                request.Month = visibleMonth.Month;
                request.TimeCode = ddlTime.SelectedValue;
                request.Owner = "";

                request.TestingDate = "";

                var ls = biz.GetExamByCriteria(request);
                if (ls.IsError)
                {
                    throw new ApplicationException(ls.ErrorMsg);
                }

                GBBiz gbBiz = new GBBiz();
                DTO.ResponseService<DTO.GBHoliday[]> lsHoliday = gbBiz.GetHolidayListByYearMonth(visibleMonth.ToString("yyyyMM", System.Globalization.CultureInfo.CreateSpecificCulture("en-US")));


                ExamSchedules = ls.DataResponse.ExamShedules;  
                Holidays = lsHoliday.DataResponse.ToList();             

            }
            catch (Exception ex)
            {
                LoggerFactory.CreateLog().LogError("ไม่สามารถเรียกข้อมูลได้.", ex);
                String a = ex.Message;
            }

        }

        #region switch Mode Display Exam
        private void ShowCalendarOnly()
        {
            pnlCalendar.Visible = true;
            pnlTable.Visible = false;
            SelectDataFrombase();
        }

        //แสดงตาราง
        private void ShowTableOnly()
        {
            pnlTable.Visible = true;
            pnlCalendar.Visible = false;
            BindExamScheduleByTable();
        }
        private void ShowTogether()
        {
            pnlTable.Visible = true;
            pnlCalendar.Visible = true;
            SelectDataFrombase();
            BindExamScheduleByTable();
        }
        private void ShowTogether(String year, String month, String day)
        {
            pnlTable.Visible = true;
            pnlCalendar.Visible = true;
            SelectDataFrombase();
            BindExamScheduleByTable(year, month, day);
        }
        private void SwitchModeDisplay()
        {
            switch (rblDisplay.SelectedValue)
            {
                case "2":
                    ShowTableOnly();
                    break;
                case "1":
                default:
                    ShowCalendarOnly();
                    break;
            }
        }
        protected void rblDisplay_SelectedIndexChanged(object sender, EventArgs e)
        {
            SwitchModeDisplay();

        }

        #endregion
     
        #endregion


        #region Event Handler


        #region Serarh Criteria Event
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            cldExam.SelectedDate = DateTime.MinValue;
            cldExam.VisibleDate = GetSearchMonth();  
            SwitchModeDisplay();
        }
        protected void ddlPlaceGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetExamPlace();
        }
        protected void btnCancle_Click(object sender, EventArgs e)
        {

        }
        #endregion


        #region Calendar Event
        private bool isLastRow = false;
        private int daysCounter;
        protected void cldExam_DayRender(object sender, DayRenderEventArgs e)
        {
            #region Day Render Rows
            /********** render day ************/
            daysCounter++;
            if (e.Day.Date.Day == 1 && !e.Day.IsOtherMonth) // 1st of current month. Turn visibility of row to ON.
            {
                isLastRow = false;
            }
            else if (daysCounter == 36 && e.Day.IsOtherMonth) // 5 rows already rendered. If its the next row is next month, hide it.
            {
                isLastRow = true;
            }
            else if (daysCounter == 1 && e.Day.IsOtherMonth && e.Day.Date.Month == e.Day.Date.AddDays(6).Month)
            {   // If first row completely is previous month, hide it.
                // Actually the flag should be isFirstRow, but I dont want one more boolean just for the sake of it.
                isLastRow = true;
            }

            if (isLastRow)
            {
                e.Cell.Visible = false;
                return;
            }
            if (e.Day.IsWeekend && !e.Day.IsSelected)
            {
                e.Cell.Style.Add("background-color", "#f0e7f1");
            }
            /********* end render ************/
            #endregion
            e.Day.IsSelectable = false;
            e.Cell.Enabled = false;
            e.Cell.ForeColor = System.Drawing.Color.Gray;

            BLL.ExamScheduleBiz biz = new BLL.ExamScheduleBiz();
            Func<string, string> GetCrit = anyString =>
            {
                return string.IsNullOrEmpty(anyString) ? string.Empty : anyString;
            };

            if (ExamSchedules.Count() > 0)
            {

                DateTime exam = ExamSchedules.FirstOrDefault(a => a.Date == e.Day.Date);


                if (exam != null && exam != DateTime.MinValue)
                {
                    LinkButton lnk = new LinkButton();

                    e.Cell.Controls.Add(new LiteralControl("</br>"));

                    DateTime datenow = DateTime.Now;
                    DateTime adddate = DateTime.Now.AddDays(5);
                    DateTime renderdate = e.Day.Date;
                    if (renderdate > datenow && renderdate < adddate)
                    {
                        lnk.ID = "lnkButton";
                        lnk.Text = "รายละเอียด";
                        lnk.Enabled = false;
                        lnk.ForeColor = System.Drawing.Color.Gray;
                        e.Cell.Attributes.Remove("onclick");
                        e.Cell.Style.Remove("cursor");
                        e.Cell.Style.Remove("pointer");
                    }
                    else
                    {

                        lnk.ID = "lnkButton";
                        lnk.Text = "รายละเอียด";
                        lnk.ForeColor = System.Drawing.Color.Green;
                        e.Cell.Attributes.Add("onclick", e.SelectUrl);
                        e.Cell.Style.Add("cursor", "pointer");
                    }



                    e.Cell.Controls.Add(lnk);
                }
            }


            if (Holidays.Count() > 0)
            {
                GBHoliday holiday = Holidays.FirstOrDefault(s => s.HL_DATE.Date == e.Day.Date);
                DateTime dtToday = DateTime.Today.AddDays(-1);
                DateTime dtFinish = DateTime.Today.AddDays(+5);
                if (holiday != null)
                {
                    e.Cell.Controls.Add(new LiteralControl("</br>"));
                    Label lnk = new Label();
                    lnk.ID = "lnkButton";
                    lnk.Enabled = false;
                    lnk.Text = holiday.HL_DESC;
                    lnk.ForeColor = System.Drawing.Color.Red;
                    lnk.BackColor = System.Drawing.Color.FromArgb(250, 200, 135);
                    e.Cell.Attributes.Add("onclick", e.SelectUrl);
                    e.Cell.Style.Add("cursor", "pointer");
                    e.Cell.Controls.Add(lnk);
                }
            }


            e.Cell.DataBind();
        }

        protected void cldExam_SelectionChanged(object sender, EventArgs e)
        {
            ShowTogether(cldExam.SelectedDate.Year.ToString(), cldExam.SelectedDate.Month.ToString(), cldExam.SelectedDate.Day.ToString());
        }

        protected void cldExam_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
        {
            if (e.NewDate.Month != e.PreviousDate.Month)
            {
                cldExam.SelectedDate = DateTime.MinValue;
                ddlMonth.SelectedIndex = e.NewDate.Month;
                txtYear.Text = e.NewDate.ToString("yyyy", System.Globalization.CultureInfo.CreateSpecificCulture("th-TH"));
                txtYear.DataBind();
                ddlMonth.DataBind();
                SwitchModeDisplay(); 
            }
        }
        #endregion

        #region Grid Event
    
        // ทุกๆครั้งที่เลือกรายการสอบแบบปฏิทิน
        protected void lnkExamNumber_Click(object sender, EventArgs e)
        { 
            var gr = (GridViewRow)((LinkButton)sender).NamingContainer;
            var strPlaceCode = (Label)gr.FindControl("lblPlaceCode");        
            var lblExamNumber = (Label)gr.FindControl("ExamNumberNo");
            var lblExamDate = (Label)gr.FindControl("lblExamDate");
            var lblExamTime = (Label)gr.FindControl("lblExamTime");
            var lblExamPlaceGroup = (Label)gr.FindControl("lblGroupExamYard");
            var lblExamPlace = (Label)gr.FindControl("lblPlaceName");
            var lblProvince = (Label)gr.FindControl("lblProvice");
            var lblSeat = (Label)gr.FindControl("lblExamAdmission");
            var lblLicenseTypeName = (Label)gr.FindControl("lblLicenseTypeName");
            var lblExamFee = (Label)gr.FindControl("lblExamFee");
            var lblAgentType = (Label)gr.FindControl("lblAgentType");
            var lblPlaceCode = (Label)gr.FindControl("lblPlaceCode");
            var lblTestTimeCode = (Label)gr.FindControl("lblTestTimeCode");
            var lblLicenseTypeCode = (Label)gr.FindControl("lblLicenseTypeCode");
            var lblProvineCode = (Label)gr.FindControl("lblProvinceCode");
            var lblExamPlaceGroupCode = (Label)gr.FindControl("lblExamPlaceGroupCode"); 
            var lblExamOwnerName = (Label)gr.FindControl("lblExamOwnerName");

            if (ListApplicant.Find(x => x.ExamDate == Convert.ToDateTime(lblExamDate.Text) && x.ExamTime == lblExamTime.Text) != null)
            {
                UCModalError1.ShowMessageError = "ได้มีการสมัครในวันและเวลาสอบนี้แล้วไม่สามารถสมัครสอบได้ กรุณาทำการสมัครสอบในวันพรุ่งนี้";
                UCModalError1.ShowModalError(); 
            }
            else if (ListApplicant.Find(x => x.ExamNumber == lblExamNumber.Text) == null)
            {   
                txtDetailExamCode.Text = lblExamNumber.Text;
                txtDetailDateExam.Text = lblExamDate.Text;
                txtTestTime.Text = lblExamTime.Text;
                txtExamPlaceName.Text = lblExamPlaceGroup.Text;
                txtProvincePopup.Text = lblProvince.Text;
                txtLicenseTypeName.Text = lblLicenseTypeName.Text;
                txtDetailPlaceCode.Text = lblExamPlace.Text;
                txtExamOwner.Text = lblExamOwnerName.Text;
                Session["ExamNumber"] = lblExamNumber.Text;
                Session["ExamDate"] = lblExamDate.Text;
                Session["ExamTime"] = lblExamTime.Text;
                Session["ExamPlaceGroup"] = lblExamPlaceGroup.Text;
                Session["ExamPlace"] = lblExamPlace.Text;
                Session["Province"] = lblProvince.Text;
                Session["Seat"] = lblSeat.Text;
                Session["LicenseTypeName"] = lblLicenseTypeName.Text;
                Session["ExamFee"] = lblExamFee.Text;
                Session["AgentType"] = lblAgentType.Text;
                Session["PlaceCode"] = lblPlaceCode.Text;
                Session["ExamPlaceCode"] = strPlaceCode.Text;
                Session["TestingNo"] = lblExamNumber.Text;
                Session["TestingDate"] = lblExamDate.Text;
                Session["TestTimeCode"] = lblTestTimeCode.Text;
                Session["LicenseTypeCode"] = lblLicenseTypeCode.Text;
                Session["ProvinceCode"] = lblProvineCode.Text;
                Session["ExamPlaceGroupCode"] = lblExamPlaceGroupCode.Text;

                var list = new List<DTO.ApplicantTemp>();
                        list.Add( new ApplicantTemp(){
                        APPLICANT_CODE = 0,
                        TESTING_NO = lblExamNumber.Text,
                        TESTING_DATE = Convert.ToDateTime(lblExamDate.Text),
                        EXAM_PLACE_CODE = lblPlaceCode.Text,
                        APPLY_DATE = DateTime.Today,
                        USER_ID = UserProfile.Id,
                        ID_CARD_NO = UserProfile.IdCard 
                    });

                DateTime dtTestingDate = Convert.ToDateTime(lblExamDate.Text);
                ApplicantBiz biz = new ApplicantBiz();
                DTO.ResultValidateApplicant res = biz.ValidateApplicantBeforeSaveList(lblExamNumber.Text, UserProfile.IdCard, dtTestingDate, lblTestTimeCode.Text, strPlaceCode.Text, lblExamTime.Text, ListApplicant);

                if (res.IsConfirm)
                {
                    ModalPopupExtenderListExam.Show();
                    lblConfirmExam.Text = res.ValidateMessage;
                    return;
                }

                if (!res.IsCanExam)
                {
                    UCModalError1.ShowMessageError = res.ValidateMessage;
                    UCModalError1.ShowModalError();
                    return;
                }
           
                var checkBeforeSubmit = biz.GeneralValidateApplicantSingleBeforeSubmit(list);
                if (checkBeforeSubmit.ResultMessage == true)
                {
                    UCModalError1.ShowMessageError = "สมัครสอบซ้ำ";
                    UCModalError1.ShowModalError();
                }
                else
                {
                    if (ddlTypeLicense.SelectedValue == "03" || ddlTypeLicense.SelectedValue == "04" || ddlTypeLicense.SelectedValue == "11" || ddlTypeLicense.SelectedValue == "12")
                    {
                        lblDetailCompanyCode.Visible = false;
                        ddlCompanyCode.Visible = false;
                    }
                    else
                    {
                        lblDetailCompanyCode.Visible = true;
                        ddlCompanyCode.Visible = true;
                        GetCompany(ddlTypeLicense.SelectedValue);
                    }
                    ModSingleApplicant.Show();
                }
            }
            else
            {
                ModalListExam.Show();
                UCModalError1.ShowMessageError = "รอบสอบนี้คุณได้เลือกเข้าไปในรายการแล้ว";
                UCModalError1.ShowModalError();
            }
            
            
        }

        private void GetCompany(string strLicenseTypeCode)
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            List<string> ls = (List<string>)Session["lsCompCode"];
            List<String> ls2;
            switch (strLicenseTypeCode)
            {
                case "01":
                case "07": ls2 = ls.Where(l => l.Contains("[1")).ToList();
                    break;
                case "02":
                case "05":
                case "06":
                case "08": ls2 = ls.Where(l => l.Contains("[2")).ToList();
                    break;
                default: ls2 = ls;
                    break;
            }
            ddlCompanyCode.DataSource = ls2;
            ddlCompanyCode.DataBind();
            ddlCompanyCode.Items.Insert(0, new ListItem(SysMessage.DefaultSelecting, "0"));
        }
        protected void gvExamSchedule_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblExamDate = (Label)e.Row.FindControl("lblExamDate");
                var dateExam = Convert.ToDateTime(lblExamDate.Text);
                if (dateExam < DateTime.Now.AddDays(5))
                {
                    LinkButton lnkExamRegister = (LinkButton)e.Row.FindControl("lnkExamRegister");
                    lnkExamRegister.Enabled = false;
                    lnkExamRegister.Font.Strikeout = true;
                    lnkExamRegister.ForeColor = System.Drawing.Color.Gray;
                    
                }
            }
                   
        }

        protected void btnNextGvSearch_Click(object sender, EventArgs e)
        {

        }

        protected void btnPreviousGvSearch_Click(object sender, EventArgs e)
        {

        }
        
        protected void btnDetailSubmit_Click(object sender, EventArgs e)
        {
            var arrComp = ddlCompanyCode.SelectedValue.Split('[', ']');
            if (ddlCompanyCode.Visible == true)
            {
                if (ddlCompanyCode.SelectedValue == "0")
                {
                    UCModalError1.ShowMessageError = "กรุณาเลือกรหัสบริษัทประกันภัย";
                    UCModalError1.ShowModalError();
                    ModSingleApplicant.Show();
                }
                else
                {
                    ListApplicant.Add(new DTO.AddApplicant
                    {
                        ExamNumber = Session["ExamNumber"].ToString(),
                        ExamDate = (Convert.ToDateTime(Session["ExamDate"])),
                        ExamTime = Session["ExamTime"].ToString(),
                        ExamPlaceGroup = Session["ExamPlaceGroup"].ToString(),
                        ExamPlace = Session["ExamPlace"].ToString(),
                        Province = Session["Province"].ToString(),
                        Seat = Session["Seat"].ToString(),
                        LicenseTypeName = Session["LicenseTypeName"].ToString(),
                        ExamFee = Session["ExamFee"].ToString(),
                        AgentType = Session["AgentType"].ToString(),
                        ExamPlaceCode = Session["PlaceCode"].ToString(),
                        InSurCompCode = arrComp[1],
                        TestTimeCode = Session["TestTimeCode"].ToString(),
                        ApplyDate = DateTime.Now
                    });

                    gvListExam.DataSource = ListApplicant;
                    gvListExam.DataBind();

                    ModSingleApplicant.Hide();
                    ModalListExam.Show();
                }
            }
            else
            {
                ListApplicant.Add(new DTO.AddApplicant
                {
                    ExamNumber = Session["ExamNumber"].ToString(),
                    ExamDate = (Convert.ToDateTime(Session["ExamDate"])),
                    ExamTime = Session["ExamTime"].ToString(),
                    ExamPlaceGroup = Session["ExamPlaceGroup"].ToString(),
                    ExamPlace = Session["ExamPlace"].ToString(),
                    Province = Session["Province"].ToString(),
                    Seat = Session["Seat"].ToString(),
                    LicenseTypeName = Session["LicenseTypeName"].ToString(),
                    ExamFee = Session["ExamFee"].ToString(),
                    AgentType = Session["AgentType"].ToString(),
                    ExamPlaceCode = Session["PlaceCode"].ToString(),
                    TestTimeCode = Session["TestTimeCode"].ToString(),
                    ApplyDate = DateTime.Now
                });
                gvListExam.DataSource = ListApplicant;
                gvListExam.DataBind();

                ModSingleApplicant.Hide();
                ModalListExam.Show();
            }
            

            
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }

        protected void btnListExamConfirm_Click(object sender, EventArgs e)
        {
            SaveListExam(); 
        }

        protected void btnListExamContinue_Click(object sender, EventArgs e)
        {
            ModalListExam.Hide();
        }

        protected void lnkListExamRegister_Click(object sender, EventArgs e)
        {
            var gr = (GridViewRow)((LinkButton)sender).NamingContainer;
            var ExamNumber = (Label)gr.FindControl("lblListExamNumberNo");
            ListApplicant.Remove(ListApplicant.Find(x => x.ExamNumber == ExamNumber.Text));
            gvListExam.DataSource = ListApplicant;
            gvListExam.DataBind();
        }

        protected void gvListExam_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var lblListExamNumberNo = (Label)e.Row.FindControl("lblListExamNumberNo");

                var LBUP = (LinkButton)e.Row.FindControl("LBUP");
                var LBDown = (LinkButton)e.Row.FindControl("LBDown");
                var list = ListApplicant.FirstOrDefault(x => x.ExamNumber == lblListExamNumberNo.Text);
                if (list != null)
                {
                    if (ListApplicant.Count == 1)
                    {
                        LBDown.Visible = false;
                        LBUP.Visible = false;
                    }
                    else if (e.Row.RowIndex == 0)
                    {
                        LBUP.Visible = false;
                    }
                    else if (e.Row.RowIndex == ListApplicant.Count - 1)
                    {
                        LBDown.Visible = false;
                    }

                }

            }
        }


        protected void gvListExam__RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int CurrentIndex = gvr.RowIndex; 
            SortApplicant(CurrentIndex, e.CommandName);
            this.gvListExam.DataSource = ListApplicant;
            this.gvListExam.DataBind();
            this.ModalListExam.Show();        
        }

        public List<DTO.AddApplicant> SortApplicant(int CurrentIndex, String Mode)
        {

            for (int i = 0; i < ListApplicant.Count; i++)
            {
                ListApplicant[i].RUN_NO = (i + 1).ToString();
            }
            if (Mode.Equals("Up"))
            {
                //Get Current & Previous Data
                AddApplicant cur = ListApplicant[CurrentIndex];
                AddApplicant pre = ListApplicant[CurrentIndex - 1];

                int order = Convert.ToInt32(cur.RUN_NO);
                cur.RUN_NO = pre.RUN_NO;
                pre.RUN_NO = Convert.ToString(order);

                //Resort
                ListApplicant[CurrentIndex] = pre;
                ListApplicant[CurrentIndex - 1] = cur;
            }
            else if (Mode.Equals("Down"))
            {
                //Get Current & Next Data
                AddApplicant cur = ListApplicant[CurrentIndex];
                AddApplicant next = ListApplicant[CurrentIndex + 1];

                int order = Convert.ToInt32(cur.RUN_NO);
                cur.RUN_NO = next.RUN_NO;
                next.RUN_NO = Convert.ToString(order);

                //Resort
                ListApplicant[CurrentIndex] = next;
                ListApplicant[CurrentIndex + 1] = cur;
            }

            return ListApplicant.OrderBy(idx => idx.RUN_NO).ToList();

        }

        private void SaveListExam()
        {
            string group = string.Empty;
            ApplicantBiz biz = new ApplicantBiz();
            string strAmount = biz.GetQuantityBillPerPageByConfig();

            if (ListApplicant.Count != 0 && ListApplicant.Count <= Convert.ToInt32(strAmount))
            {
                List<DTO.ApplicantTemp> lst = new List<ApplicantTemp>();
                for (int i = 0; i < ListApplicant.Count; i++)
                {
                    DTO.ApplicantTemp app = new ApplicantTemp();
                    app.TESTING_NO = ListApplicant[i].ExamNumber;
                    app.TESTING_DATE = ListApplicant[i].ExamDate;
                    app.EXAM_PLACE_CODE = ListApplicant[i].ExamPlaceCode;
                    app.APPLY_DATE = DateTime.Today;
                    app.INSUR_COMP_CODE = ListApplicant[i].InSurCompCode;
                    app.USER_ID = UserProfile.Id; // base.UserId;  140324110012352
                    app.ID_CARD_NO = UserProfile.IdCard;// base.IdCard;    4199682095940
                    app.APPLY_DATE = ListApplicant[i].ApplyDate;
                    app.TEST_TIME_CODE = ListApplicant[i].TestTimeCode;            
                    

                    app.RUN_NO = Convert.ToString(i + 1);
                    lst.Add(app);
                }
                string passGroup = string.Empty;

                //Check 
               // var checkBeforeSubmit = biz.GeneralValidateApplicantSingleBeforeSubmit(lst);
                //if (checkBeforeSubmit.ResultMessage == true)
                //{
                //    UCModalError1.ShowMessageError = "รอบสอบที่คุณเลือกในรายการมีรายการที่คุณได้สมัครสอบแล้ว";
                //    UCModalError1.ShowModalError();
                //}
                //
                //else
                //{
                    var res = biz.InsertSingleApplicant(lst,base.UserId);
                    if (res.IsError)
                    {
                        UCModalError1.ShowMessageError = res.ErrorMsg;
                        UCModalError1.ShowModalError();
                    }
                    else
                    {
                        ListApplicant = new List<AddApplicant>();
                        this.ModalListExam.Hide();
                        Session["lstApplicant"] = null;
                        UCModalSuccess1.ShowMessageSuccess = SysMessage.SuccessInsertApplicant;
                        UCModalSuccess1.ShowModalSuccess();
                        group = res.DataResponse;
                        passGroup = group + " " + base.UserId;

                       ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "", "OpenPopupSingle('" + passGroup + "')", true);
                              
                    }
                //}

            }
            else
            {               
                UCModalError1.ShowMessageError = "กรุณาตรวจสอบจำนวนรายการที่สอบ";
                UCModalError1.ShowModalError();
                ModalListExam.Show();
            }
        }

        protected void btnConfirmExamList_Click(object sender, EventArgs e)
        {
            if (ddlTypeLicense.SelectedValue == "03" || ddlTypeLicense.SelectedValue == "04" || ddlTypeLicense.SelectedValue == "11" || ddlTypeLicense.SelectedValue == "12")
            {
                lblDetailCompanyCode.Visible = false;
                ddlCompanyCode.Visible = false;
            }
            else
            {
                lblDetailCompanyCode.Visible = true;
                ddlCompanyCode.Visible = true;
                GetCompany(ddlTypeLicense.SelectedValue);
            }

            ModSingleApplicant.Show();
        }

        protected void btnCancelConfirmExamList_Click(object sender, EventArgs e)
        {

        }

        #endregion

        protected void btnList_Click(object sender, EventArgs e)
        {
            gvListExam.DataSource = ListApplicant;
            gvListExam.DataBind();
            ModalListExam.Show();
        } 

        #endregion
    }
}