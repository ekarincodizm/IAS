using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Globalization;
using IAS.Utils;
using System.Web.UI.HtmlControls;
using System.Data;
using IAS.DTO;

namespace IAS.Applicant
{
    public partial class GroupApplicant : basepage
    {
        int PageSize = 20; //milk
        private Boolean _isCanRender = true;
        public Boolean IsCanRender
        {
            get { return _isCanRender; }
            set { _isCanRender = value; ViewState["IsCanRender"] = _isCanRender; }
        }

        public List<DTO.ExamRender> lsRender
        {
            get
            {
                if (Session["lsRender"] == null)
                {
                    Session["lsRender"] = new List<DTO.ExamRender>();
                }

                return (List<DTO.ExamRender>)Session["lsRender"];
            }
            set
            {
                Session["lsRender"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            CalculateMinMaxYears();

            if (!Page.IsPostBack)
            {
                BindData();
            }
        }

        private Action<DropDownList, DTO.DataItem[]> BindToDDL = (ddl, ls) =>
        {
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Id";
            ddl.DataSource = ls;
            ddl.DataBind();
        };

        private void BindData()
        {
            GetLicenseType();
            GetExamTime();
            GetExamPlaceGroup();
            GetMonth();
        }

        private void CalculateMinMaxYears()
        {
            int iYear = DateTime.Now.Year;
            txtYear.Text = Convert.ToString(iYear + 543);

            NumericUpDownExtender1.Maximum = iYear + 553;
            NumericUpDownExtender1.Minimum = 0;
        }

        private void GetLicenseType()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetLicenseType(SysMessage.DefaultSelecting);
            BindToDDL(ddlTypeLicense, ls.DataResponse);
        }

        private void GetExamTime()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetExamTime(SysMessage.DefaultSelecting);
            BindToDDL(ddlTime, ls.DataResponse);
        }

        private void GetExamPlaceGroup()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetExamPlaceGroup(SysMessage.DefaultSelecting);
            BindToDDL(ddlPlaceGroup, ls.DataResponse);
        }

        private void GetMonth()
        {
            var list = IAS.Utils.DateUtil.GetMonthList(SysMessage.DefaultSelecting);
            ddlMonth.DataTextField = "Name";
            ddlMonth.DataValueField = "Id";
            ddlMonth.DataSource = list;
            ddlMonth.DataBind();
            int imonth = DateTime.Today.Month;
            string strMonth = string.Empty;
            if (imonth < 10)
            {
                strMonth = "0" + imonth;
            }

            foreach (var item in list)
            {
                if (strMonth == item.Id)
                {
                    ddlMonth.SelectedValue = item.Id;
                }
            }
        }

        private string ConvertToYearMonth(int iMonth)
        {
            if (ddlMonth.SelectedValue != "")
            {
                int iYear = txtYear.Text.ToInt() - 543;
                DateTime dt = new DateTime(iYear, iMonth, 1);
                string strYear = Convert.ToString(dt.Year);
                string strMonth = dt.Month.ToString();
                if (dt.Month < 10)
                {
                    strMonth = "0" + strMonth;
                }
                string strYearMonth = strYear + strMonth;
                return strYearMonth;
            }
            else
            {
                int iYear = txtYear.Text.ToInt() - 543;
                DateTime dt = new DateTime(iYear, iMonth, 1);
                string strYear = Convert.ToString(dt.Year);
                string strMonth = dt.Month.ToString();
                if (dt.Month < 10)
                {
                    strMonth = "0" + strMonth;
                }
                string strYearMonth = strYear + strMonth;
                return strYearMonth;
            }

        }

        protected void btnClear_Click(object sender, EventArgs e)
        {

        }

        protected void btnSearchExamCode_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtExamNumber.Text))
            {
                BLL.ExamScheduleBiz biz = new BLL.ExamScheduleBiz();
                bool result = biz.IsRightTestingNo(txtExamNumber.Text);
                if (result == false)
                {
                    UCModalError.ShowMessageError = SysMessage.PleaseInputTestingNo;
                    UCModalError.ShowModalError();
                }
                else
                {
                    Session["TestingNo"] = txtExamNumber.Text;
                    Response.Redirect("~/Applicant/GroupApplicantDetail.aspx");
                }
            }
            else
            {
                pnlSearch.Visible = true;
                pnlCalendar.Visible = true;
            }
        }

        protected void ddlPlaceGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetExamPlaceByCompCode(ddlPlaceGroup.SelectedIndex == 0 ? "" : ddlPlaceGroup.SelectedItem.Value, ddlPlaceGroup.SelectedItem.Value);
            BindToDDL(ddlPlace, ls.DataResponse);
            ddlPlace.Items.Insert(0, SysMessage.DefaultSelecting);
        }

        protected void rblDisplay_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblDisplay.SelectedValue == "1")
            {
                pnlCalendar.Visible = true;
                pnlTable.Visible = false;
            }
            else
            {
                pnlCalendar.Visible = false;
                pnlTable.Visible = true;
                BindExamScheduleByTable(true);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lsRender.Clear();
            IsCanRender = true;

            if (rblDisplay.SelectedValue == "1")
            {
                pnlCalendar.Visible = true;
                pnlTable.Visible = false;

                if (!string.IsNullOrEmpty(ddlMonth.SelectedValue))
                {
                    int iYear = txtYear.Text.ToInt() - 543;
                    DateTime dtSearch = new DateTime(iYear, ddlMonth.SelectedValue.ToInt(), 1);
                    string strYear = Convert.ToString(dtSearch.Year);
                    string strMonth = dtSearch.Month.ToString();
                    if (dtSearch.Month < 10)
                    {
                        strMonth = "0" + strMonth;
                    }
                    cldGroupApplicant.TodaysDate = new DateTime(strYear.ToInt(), strMonth.ToInt(), 1);
                    upn.Update();
                }
            }
            else
            {
                pnlCalendar.Visible = false;
                pnlTable.Visible = true;
                BindExamScheduleByTable(true);
            }
        }

        // โหลดข้อมูลตาราง
        private void BindExamScheduleByTable(Boolean CountAgain)
        {
            #region page
            int Rpage = (txt_page_now.Text.Trim() == "") ? 0 : txt_page_now.Text.Trim().ToInt();
            int resultPage = (Rpage == 0) ? 1 : txt_page_now.Text.Trim().ToInt();
            resultPage = resultPage == 0 ? 1 : resultPage;
            if ((txt_input.Text.Trim() == null) || (txt_input.Text.Trim() == "") || (txt_input.Text.Trim() == "0"))
            {
                txt_input.Text = PageSize.ToString();
            }
            else
            {
                PageSize = Convert.ToInt32(txt_input.Text);
            }
            #endregion page

            BLL.ExamScheduleBiz biz = new BLL.ExamScheduleBiz();
            Func<string, string> GetCrit = anyString =>
            {
                return string.IsNullOrEmpty(anyString) ? string.Empty : anyString;
            };

            string strExamPlaceGroup = GetCrit(ddlPlaceGroup.SelectedValue);
            string strExamPlace = GetCrit(ddlPlace.SelectedValue);
            string strLicenseType = GetCrit(ddlTypeLicense.SelectedIndex == 0 ? "" : ddlTypeLicense.SelectedValue);

            string strYearMonth = GetCrit(ConvertToYearMonth());
            string strTime = GetCrit(ddlTime.SelectedIndex == 0 ? "" : ddlTime.SelectedValue);


            if (CountAgain)
            {
                #region Page
                var CountPage = biz.GetExamByCriteria(strExamPlaceGroup, strExamPlace, strLicenseType, strYearMonth, strTime, null, resultPage, PageSize, true);

                if (CountPage.DataResponse != null)
                    if (CountPage.DataResponse.Tables[0].Rows.Count > 0)
                    {
                        Int64 totalROWs = Convert.ToInt32(CountPage.DataResponse.Tables[0].Rows[0]["CCount"].ToString());

                        VisibleGV(gvTable, totalROWs, Convert.ToInt32(txt_input.Text), true);
                        if (Rpage == 0)
                            NPbutton(btnP_table, txt_page_now, btnN_table, "", lbl_pageMax);
                    }
                    else
                    {
                        VisibleGV(gvTable, 0, Convert.ToInt32(txt_input.Text), true);
                        if (Rpage == 0)
                            NPbutton(btnP_table, txt_page_now, btnN_table, "", lbl_pageMax);
                        lbl_pageMax.Text = "1";
                    }
                #endregion Page
            }


            var ls = biz.GetExamByCriteria(strExamPlaceGroup, strExamPlace, strLicenseType, strYearMonth, strTime, null, resultPage, PageSize, false);

            gvTable.DataSource = ls.DataResponse;
            gvTable.DataBind();
        }

        // ทุกๆครั้งที่เลือกรายการสอบแบบปฏิทิน
        protected void lnkExamNumber_Click(object sender, EventArgs e)
        {
            var gr = (GridViewRow)((LinkButton)sender).NamingContainer;
            var strExamNumber = (LinkButton)gr.FindControl("lnkExamNumber");
            var strPlaceCode = (Label)gr.FindControl("lblPlaceCode");
            BLL.ExamScheduleBiz biz = new BLL.ExamScheduleBiz();
            upn.Update();
            Session["TestingNo"] = strExamNumber.Text;
            Response.Redirect("~/Applicant/GroupApplicantDetail.aspx?");
        }

        // ทุกๆครั้งที่เลือกรายการสอบแบบตาราง
        protected void lnkTableExamNumber_Click(object sender, EventArgs e)
        {
            var gr = (GridViewRow)((LinkButton)sender).NamingContainer;
            var strExamNumber = (LinkButton)gr.FindControl("lnkTableExamNumber");
            var strPlaceCode = (Label)gr.FindControl("lblPlaceCode");
            BLL.ExamScheduleBiz biz = new BLL.ExamScheduleBiz();
            upn.Update();
            Session["TestingNo"] = strExamNumber.Text;
            Response.Redirect("~/Applicant/GroupApplicantDetail.aspx");
        }

        // โหลดข้อมูลปฏิทิน
        private void BindExamScheduleByCalendar()
        {
            if (ddlMonth.SelectedValue.ToInt() > 0)
            {
                int iYear = txtYear.Text.ToInt() - 543;
                DateTime dt = new DateTime(iYear, ddlMonth.SelectedValue.ToInt(), 1);
                //LoadCalendar(dt);
                upn.Update();
            }
        }

        // โหลดข้อมูลปฏิทินจากเงื่อนไข
        private void BindExamScheduleCalendarByCriteria(DateTime date)
        {
            Func<string, string> GetCrit = anyString =>
            {
                return string.IsNullOrEmpty(anyString) ? string.Empty : anyString;
            };

            string strExamPlaceGroup = GetCrit(ddlPlaceGroup.SelectedValue);
            string strExamPlace = GetCrit(ddlPlace.SelectedValue);
            string strLicenseType = GetCrit(ddlTypeLicense.SelectedValue);
            string strYearMonth = GetCrit(ConvertToYearMonth());
            string strTime = GetCrit(ddlTime.SelectedValue);

            BLL.ExamScheduleBiz biz = new BLL.ExamScheduleBiz();
            var ls = biz.GetExamByCriteria(strExamPlaceGroup, strExamPlace, strLicenseType, strYearMonth, strTime, null,0,0,false);
            DataSet ds = ls.DataResponse;
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];

                var list = dt.AsEnumerable().Select(s => s.Field<DateTime>("TESTING_DATE")).Distinct().ToList();

                for (int i = 0; i < list.Count; i++)
                {
                    if (date.Day == list[i].Date.Day)
                    {
                        Controls.Add(new LiteralControl("</br>"));
                        Label lbl = new Label { ID = "btn" + i.ToString(), Text = "รายละเอียด" };
                        Controls.Add(lbl);
                    }
                }
            }
            cldGroupApplicant.TodaysDate = date;
            upn.Update();
        }

        private string ConvertToYearMonth()
        {
            if (ddlMonth.SelectedValue.ToInt() > 0)
            {
                int iYear = txtYear.Text.ToInt() - 543;
                DateTime dt = new DateTime(iYear, ddlMonth.SelectedValue.ToInt(), 1);
                string strYear = Convert.ToString(dt.Year);
                string strMonth = dt.Month.ToString();
                if (dt.Month < 10)
                {
                    strMonth = "0" + strMonth;
                }
                string strYearMonth = strYear + strMonth;
                return strYearMonth;
            }
            else
            {
                int iYear = txtYear.Text.ToInt() - 543;
                DateTime dt = new DateTime(iYear, DateTime.Today.Month, 1);
                string strYear = Convert.ToString(dt.Year);
                string strMonth = dt.Month.ToString();
                if (dt.Month < 10)
                {
                    strMonth = "0" + strMonth;
                }
                string strYearMonth = strYear + strMonth;
                return strYearMonth;
            }

        }

        protected void cldGroupApplicant_DayRender(object sender, DayRenderEventArgs e)
        {
            if (IsCanRender == true)
            {
                BLL.ExamScheduleBiz biz = new BLL.ExamScheduleBiz();
                Func<string, string> GetCrit = anyString =>
                {
                    return string.IsNullOrEmpty(anyString) ? string.Empty : anyString;
                };

                string strExamPlaceGroup = GetCrit(ddlPlaceGroup.SelectedValue);
                string strExamPlace = GetCrit(ddlPlace.SelectedValue);
                string strLicenseType = GetCrit(ddlTypeLicense.SelectedValue);
                string strYearMonth = string.Empty;
                strYearMonth = GetCrit(ConvertToYearMonth(e.Day.Date.Month));
                string strTime = GetCrit(ddlTime.SelectedValue);

                var ls = biz.GetExamByCriteria(strExamPlaceGroup, strExamPlace, strLicenseType, strYearMonth, strTime, null,0,0,false);

                DataSet ds = ls.DataResponse;

                if (ds != null)
                {
                    DataTable dt = ds.Tables[0];

                    var list = dt.AsEnumerable().Select(s => s.Field<DateTime>("TESTING_DATE")).Distinct().ToList();
                    DateTime dtToday = DateTime.Today.AddDays(-1);
                    DateTime dtFinish = DateTime.Today.AddDays(+5);
                    for (int i = 0; i < list.Count; i++)
                    {
                        ExamRender examrender = new ExamRender();
                        if (e.Day.Date == list[i].Date)
                        {
                            e.Cell.Controls.Add(new LiteralControl("</br>"));
                            Label lbl = new Label { ID = "btn" + i.ToString(), Text = "รายละเอียด" };
                            e.Cell.Controls.Add(lbl);
                        }
                        if (e.Day.Date == list[i].Date && list[i].Date > dtToday && list[i].Date <= dtFinish)
                        {
                            examrender.IsSetProperty = true;
                            e.Cell.Enabled = false;
                            e.Day.IsSelectable = false;
                            e.Cell.ForeColor = System.Drawing.Color.Gray;
                        }
                    }
                }
            }

        }

        #region Pageing_milk
        protected void VisibleGV(GridView GVname, double total_row_count, double rows_per_page, Boolean visible_or_disvisible)
        {
            switch (GVname.ID.ToString())
            {
                case "gvDetail":
                    lblTotal.Text = "จำนวน <b>" + Convert.ToString(total_row_count) + "</b> รายการ";
                    rows_per_page = (rows_per_page == 0 || rows_per_page == null) ? 1 : rows_per_page;
                    double Paggge = Math.Ceiling(total_row_count / rows_per_page);
                    txtTotalPage.Text = (total_row_count > 0) ? Convert.ToString(Paggge) : "0";
                    lblTotal.Visible = visible_or_disvisible;
                    txtTotalPage.Visible = visible_or_disvisible;
                    rowPerpage.Visible = visible_or_disvisible;
                    lblParaPage.Visible = visible_or_disvisible;
                    pageGo.Visible = visible_or_disvisible;
                    TXTrowperpage.Visible = visible_or_disvisible;
                    break;

                default:
                    break;
            }
        }

        protected void NPbutton(Button PreName, TextBox txtNum, Button NextName, string N_or_P, Label Maxpage)
        {
            int MaxP = 1;
            MaxP = ((Maxpage.Text.Trim() == "") || (Maxpage.Text.Trim() == "0")) ? MaxP : Maxpage.Text.ToInt();
            Maxpage.Text = (Maxpage.Text.ToInt() != MaxP) ? MaxP.ToString() : Maxpage.Text;
            if (N_or_P == "P")
            {
                if (txtNum.Text.ToInt() > 1)
                {
                    txtNum.Text = Convert.ToString(Convert.ToInt32(txtNum.Text) - 1);
                }
            }
            else if (N_or_P == "N")
            {
                txtNum.Text = Convert.ToString(Convert.ToInt32(txtNum.Text) + 1);

            }
            else
            {
                txtNum.Text = "1";
                PreName.Visible = false;
            }


            Hide_show(PreName, txtNum, NextName, N_or_P, MaxP);
        }
        protected void Hide_show(Button PreName, TextBox txtNum, Button NextName, string N_or_P, int MaxP)
        {
            if (txtNum.Text.ToInt() == 1)
            {
                PreName.Visible = false;
            }
            else
            {
                PreName.Visible = true;
            }
            upn.Update();
            if (txtNum.Text.ToInt() == MaxP)
            {
                NextName.Visible = false;
            }
            else
            {
                NextName.Visible = true;
            }

        }
        protected void btnPreviousGvSearch_Click(object sender, EventArgs e)
        {
            NPbutton(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "P", txtTotalPage);
            cldGroupApplicant_CLICK(false);
            Hide_show(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "", txtTotalPage.Text.ToInt());
            upn.Update();
        }
        protected void btnNextGvSearch_Click(object sender, EventArgs e)
        {
            NPbutton(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "N", txtTotalPage);
            cldGroupApplicant_CLICK(false);
            Hide_show(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "", txtTotalPage.Text.ToInt());
            upn.Update();
        }

        #endregion Pageing_milk

        protected void cldGroupApplicant_SelectionChanged(object sender, EventArgs e)
        {
            cldGroupApplicant_CLICK(true); //milkจับแยกแล้วส่ง booleanไป
        }

        protected void cldGroupApplicant_CLICK(Boolean CountAgain)//code เดิมพี่ฟิลด์แค่มาเพิ่มboolean กับตรงที่regionไว้
        {
            gvGroupApplicant.Visible = true;

            DateTime cldselectDate = cldGroupApplicant.SelectedDate;
            BLL.ExamScheduleBiz biz = new BLL.ExamScheduleBiz();
            string strYear = cldGroupApplicant.SelectedDate.Year.ToString();
            string strMonth = cldGroupApplicant.SelectedDate.Month.ToString();
            if (cldGroupApplicant.SelectedDate.Month < 10)
            {
                strMonth = "0" + strMonth;
            }

            #region page
            int Rpage = (txtNumberGvSearch.Text.Trim() == "") ? 0 : txtNumberGvSearch.Text.Trim().ToInt();
            int resultPage = (Rpage == 0) ? 1 : txtNumberGvSearch.Text.Trim().ToInt();
            resultPage = resultPage == 0 ? 1 : resultPage;
            if ((rowPerpage.Text.Trim() == null) || (rowPerpage.Text.Trim() == "") || (rowPerpage.Text.Trim() == "0"))
            {
                rowPerpage.Text = PageSize.ToString();
            }
            else
            {
                PageSize = Convert.ToInt32(rowPerpage.Text);
            }
            #endregion page



            if (CountAgain)
            {
                #region Page
                var CountPage = biz.GetExamByCriteria("", "", "","", strYear + strMonth, "", cldselectDate, resultPage, PageSize, true);

                if (CountPage.DataResponse != null)
                    if (CountPage.DataResponse.Tables[0].Rows.Count > 0)
                    {
                        Int64 totalROWs = Convert.ToInt32(CountPage.DataResponse.Tables[0].Rows[0]["CCount"].ToString());

                        VisibleGV(gvGroupApplicant, totalROWs, Convert.ToInt32(rowPerpage.Text), true);
                        if (Rpage == 0)
                            NPbutton(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "", txtTotalPage);
                    }
                    else
                    {
                        VisibleGV(gvGroupApplicant, 0, Convert.ToInt32(rowPerpage.Text), true);
                        if (Rpage == 0)
                            NPbutton(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "", txtTotalPage);
                        txtTotalPage.Text = "1";
                    }
                #endregion Page
            }


          
            var res = biz.GetExamByCriteria("", "", "", strYear + strMonth, "", cldselectDate, resultPage, PageSize,false);

            gvGroupApplicant.DataSource = res.DataResponse;
            gvGroupApplicant.DataBind();
        }
        protected void pageGo_Click(object sender, EventArgs e)
        {
            cldGroupApplicant_CLICK(true);
        }

        protected void gvGroupApplicant_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblTotal = ((Label)e.Row.FindControl("lblTotalApply"));
                LinkButton lbnEditGv = (LinkButton)e.Row.FindControl("lnkExamNumber");
                LinkButton lb = e.Row.FindControl("lnkExamNumber") as LinkButton;
                Label lblExamAdmission = ((Label)e.Row.FindControl("lblExamAdmission"));
                Label lblLicenseTypeCodeNumber = ((Label)e.Row.FindControl("lblLicenseTypeCodeNumber"));
                LinkButton view = ((LinkButton)e.Row.FindControl("hplview"));
                ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(lb);

                lblExamAdmission.Text = lblTotal.Text == null ? "0" : lblTotal.Text + "/" + lblExamAdmission.Text == null ? "0" : lblTotal.Text + "/" + lblExamAdmission.Text;
            }

        }

        protected void gvGroupApplicant_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvGroupApplicant.PageIndex = e.NewPageIndex;
            gvGroupApplicant.DataBind();
        }

        protected void gvTable_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //BindExamScheduleByTable();
            gvTable.PageIndex = e.NewPageIndex;
            gvTable.DataBind();
        }

       

    }
}