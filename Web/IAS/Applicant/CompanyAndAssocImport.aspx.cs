using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.DTO;
using IAS.Common.Logging;
using IAS.BLL;
using IAS.Utils;

namespace IAS.Applicant
{
    public partial class CompanyAndAssocImport : basepage
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


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                UserIdRequest = UserProfile.Id;   // 140108111245765 //140117143703861 //140115134024826
                DateTime initDate = DateTime.Now;
                Init(UserIdRequest, initDate);
                SwitchModeDisplay();
                panContent.Visible = false;
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

            BLL.ExamScheduleBiz biz = new BLL.ExamScheduleBiz();
            var ls = biz.GetExamScheduleByCriteria(ddlPlaceGroup.SelectedValue, ddlPlace.SelectedValue, ddlTypeLicense.SelectedValue,
                    "", year, month, day, ddlTime.SelectedValue, txtCurrentPage.Text, txtPageSize.Text, lblTotalItems.Text, UserIdRequest, "");

            ExamInfos = ls.DataResponse.ExamInfos;
            gvExamSchedule.DataSource = ExamInfos;
            gvExamSchedule.DataBind();

            if (gvExamSchedule.Rows.Count > 0)
            {
                btnExportExcel.Visible = true;
            }
            else
            {
                btnExportExcel.Visible = false;
            }

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
            UplSearch.Update();
        }
        private void ShowTableOnly()
        {
            pnlTable.Visible = true;
            pnlCalendar.Visible = false;
            BindExamScheduleByTable();
            UplSearch.Update();
        }

        private void ShowTogether(String year, String month, String day)
        {
            pnlTable.Visible = true;
            pnlCalendar.Visible = true;
            SelectDataFrombase();
            BindExamScheduleByTable(year, month, day);
            UplSearch.Update();
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
                //DataTable dt = dsHoliday.Tables[0];
                GBHoliday holiday = Holidays.FirstOrDefault(s => s.HL_DATE.Date == e.Day.Date);
                DateTime dtToday = DateTime.Today.AddDays(-1);
                DateTime dtFinish = DateTime.Today.AddDays(+5);

                //ExamRender examrender = new ExamRender();
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
        protected void btnInsertExamSchedule_Click(object sender, EventArgs e)
        {

        }
        protected void btnExportExcel_Click(object sender, ImageClickEventArgs e)
        {

        }

        // ทุกๆครั้งที่เลือกรายการสอบแบบปฏิทิน
        protected void lnkExamNumber_Click(object sender, EventArgs e)
        {
            var gr = (GridViewRow)((LinkButton)sender).NamingContainer;
            var strPlaceCode = (Label)gr.FindControl("lblPlaceCode");
            var lblExamNumber = (Label)gr.FindControl("ExamNumberNo");
            var lblExamDate = (Label)gr.FindControl("lblExamDate");
            var lblExamTime = (Label)gr.FindControl("lblExamTime");
            var lblPlaceCode = (Label)gr.FindControl("lblPlaceCode");
            var lblTestTimeCode = (Label)gr.FindControl("lblTestTimeCode");
            var lblLicenseTypeCode = (Label)gr.FindControl("lblLicenseTypeCode");
            var lblExamPlaceGroupCode = (Label)gr.FindControl("lblExamPlaceGroupCode");

            Session["ExamNumber"] = lblExamNumber.Text;
            Session["ExamDate"] = lblExamDate.Text;
            Session["ExamTime"] = lblExamTime.Text;
            Session["PlaceCode"] = lblPlaceCode.Text;
            Session["ExamPlaceCode"] = strPlaceCode.Text;
            Session["TestingNo"] = lblExamNumber.Text;
            Session["TestingDate"] = lblExamDate.Text;
            Session["TestTimeCode"] = lblTestTimeCode.Text;
            Session["LicenseTypeCode"] = lblLicenseTypeCode.Text;
            Session["ExamPlaceGroupCode"] = lblExamPlaceGroupCode.Text;
            Session["TestingNo"] = lblExamNumber.Text;
            Response.Redirect("~/Applicant/GroupApplicantDetail.aspx");

        }
        protected void lnkExamManageTest_Click(object sender, EventArgs e)
        {

        }


        protected void gvExamSchedule_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

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
        #endregion
        public IAS.MasterPage.Site1 MasterSite
        {
            get
            {
                return (this.Page.Master as IAS.MasterPage.Site1);
            }
        }
        protected void btnSearchExamCode_Click(object sender, EventArgs e)
        {
            txtExamNumber.Text = txtExamNumber.Text.Trim();
            btnInsertExamSchedule.Visible = false;
            txtExamNumber.Visible = true;
            if (!string.IsNullOrEmpty(txtExamNumber.Text))
            {
                BLL.ExamScheduleBiz biz = new BLL.ExamScheduleBiz();
                bool result = biz.IsRightTestingNo(txtExamNumber.Text);
                if (result == false)
                {
                    this.MasterSite.ModelError.ShowMessageError = SysMessage.PleaseInputTestingNo;
                    this.MasterSite.ModelError.ShowModalError();
                }
                else
                {
                    Session["TestingNo"] = txtExamNumber.Text;

                    var res = biz.GetExamByTestingNo(txtExamNumber.Text);
                    if (res.IsError)
                    {
                        this.MasterSite.ModelError.ShowMessageError = "โปรดตรวจสอบข้อมูล";
                        this.MasterSite.ModelError.ShowModalError();
                    }
                    else
                    {
                        Session["ExamNumber"] = res.DataResponse.TESTING_NO;
                        Session["ExamDate"] = res.DataResponse.TESTING_DATE;
                        Session["ExamTime"] = res.DataResponse.TEST_TIME_CODE;
                        Session["PlaceCode"] = res.DataResponse.EXAM_PLACE_CODE;
                        Session["ExamPlaceCode"] = res.DataResponse.EXAM_PLACE_CODE;
                        Session["TestingNo"] = res.DataResponse.TESTING_NO;
                        Session["TestingDate"] = res.DataResponse.TESTING_DATE;
                        Session["TestTimeCode"] = res.DataResponse.TEST_TIME_CODE;
                        Session["LicenseTypeCode"] = res.DataResponse.LICENSE_TYPE_CODE;
                        Session["ExamPlaceGroupCode"] = res.DataResponse.EXAM_PLACE_GROUP_CODE;
                    }

                    Response.Redirect("~/Applicant/GroupApplicantDetail.aspx");
                }
            }
            else
            {
                panContent.Visible = true;
                pnlExamSearch.Visible = false;
                if (rblDisplay.SelectedValue == "1")
                {
                    pnlSearch.Visible = true;
                    pnlCalendar.Visible = true;
                }
                else
                {
                    pnlSearch.Visible = true;
                    pnlTable.Visible = true;
                }

            }
        }



        #endregion

        protected void btnCancleSearch_Click(object sender, EventArgs e)
        {

        } 
    }
}