using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.Utils;
using System.Data;
using IAS.DTO;
using IAS.Properties;

namespace IAS.Register
{
    public partial class examSchedule : basepage
    {
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

            if (!IsPostBack)
            {
                DefaultDataWhenLoad(); //milk
            }
            else
            {
                IsCanRender = false;
                ExamSchduleReLoadSelectedDate(false);
            }
        }
        protected void DefaultDataWhenLoad()
        {
            CalculateMinMaxYears();
            IsCanRender = true;
            base.HasPermit();


            if (base.UserProfile.MemberType == DTO.RegistrationType.TestCenter.GetEnumValue())
            {
                GetExamPlaceGroup();
                GetExamPlace();
                GetLicenseType();
                GetExamTime();
                GetMonth();
                lsRender.Clear();

                Div1.Visible = false;
                boxgvTable.Visible = false;
            }
            else if (base.UserProfile.MemberType == DTO.RegistrationType.OIC.GetEnumValue())
            {
                GetExamPlaceGroupOIC();
                GetLicenseType();
                GetExamTime();
                GetMonth();
                lsRender.Clear();

                Div1.Visible = false;
                boxgvTable.Visible = false;
            }
        }  // code เดิมของพี่ฟิลด์(?) มิ้วจับแยกออกมา
        private void ExamSchduleReLoadSelectedDate(Boolean CountAgain)
        {
            Div1.Visible = true;
            gvExamSchedule.Visible = true;

            DateTime cldselectDate = cldExam.SelectedDate;

            string strYear = cldExam.SelectedDate.Year.ToString();
            string strMonth = cldExam.SelectedDate.Month.ToString();
            if (cldExam.SelectedDate.Month < 10)
            {
                strMonth = "0" + strMonth;
            }

            BLL.ExamScheduleBiz biz = new BLL.ExamScheduleBiz();
            Func<string, string> GetCrit = anyString =>
            {
                return string.IsNullOrEmpty(anyString) ? string.Empty : anyString;
            };






            //var res = biz.GetExamByCriteria(GetCrit(ddlPlaceGroup.SelectedIndex == 0 ? "" : ddlPlaceGroup.SelectedValue),
             //   GetCrit(ddlPlace.SelectedIndex == 0 ? "" : ddlPlace.SelectedValue), GetCrit(ddlTypeLicense.SelectedIndex == 0 ? "" : ddlTypeLicense.SelectedValue), strYear + strMonth, "", cldselectDate);
            //DataSet ds = res.DataResponse;
            //if (ds != null)
            //{
            //    DataTable dt = ds.Tables[0];
            //    if (dt.Rows.Count == 0)
            //    {
            //        Div1.Visible = false;
            //        boxgvTable.Visible = false;
            //        gvExamSchedule.Visible = false;

            //    }
            //    else
            //    {
            //        gvExamSchedule.Visible = true;
            //        gvExamSchedule.DataSource = res.DataResponse;
            //        gvExamSchedule.DataBind();
            //        Div1.Visible = true;
            //        boxgvTable.Visible = false;
            //    }


            //}

        }

        private Action<DropDownList, DTO.DataItem[]> BindToDDL = (ddl, ls) =>
        {
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Id";
            ddl.DataSource = ls;
            ddl.DataBind();
        };

        private void CalculateMinMaxYears()
        {
            int iYear = DateTime.Now.Year;
            txtYear.Text = Convert.ToString(iYear + 543);

            NumericUpDownExtender1.Maximum = iYear + 553;
            NumericUpDownExtender1.Minimum = 0;
        }

        private void GetExamPlaceGroup()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetExamPlaceGroupByCompCode(SysMessage.DefaultSelecting, base.UserProfile.CompCode);
            BindToDDL(ddlPlaceGroup, ls.DataResponse);
            BindToDDL(ddlDetailYardGroupCode, ls.DataResponse);
        }

        private void GetExamPlaceGroupOIC()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetExamPlaceGroup(SysMessage.DefaultSelecting);
            BindToDDL(ddlPlaceGroup, ls.DataResponse);
            BindToDDL(ddlDetailYardGroupCode, ls.DataResponse);
        }

        private void GetLicenseType()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetLicenseType(SysMessage.DefaultSelecting);
            BindToDDL(ddlTypeLicense, ls.DataResponse);
            BindToDDL(ddlDetailOfficerCode, ls.DataResponse);
        }


        private void GetExamTime()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetExamTime(SysMessage.DefaultSelecting);
            BindToDDL(ddlTime, ls.DataResponse);
            BindToDDL(ddlDetailTimeExamCode, ls.DataResponse);
        }

        private void GetMonth()
        {
            var list = IAS.Utils.DateUtil.GetMonthList(SysMessage.DefaultSelecting);
            ddlMonth.DataTextField = "Name";
            ddlMonth.DataValueField = "Id";
            ddlMonth.DataSource = list;
            ddlMonth.DataBind();
            ddlMonth.SelectedValue = Convert.ToString(DateTime.Now.Month);
        }

        // โหลดข้อมูลตาราง
        private void BindExamScheduleByTable()
        {

            if (IsCanRender == true)
            {
                BLL.ExamScheduleBiz biz = new BLL.ExamScheduleBiz();
                Func<string, string> GetCrit = anyString =>
                {
                    return string.IsNullOrEmpty(anyString) ? string.Empty : anyString;
                };

                string strExamPlaceGroup = GetCrit(ddlPlaceGroup.SelectedValue);
                string strExamPlace = GetCrit(ddlPlace.SelectedIndex == 0 ? "" : ddlPlace.SelectedValue);
                string strLicenseType = GetCrit(ddlTypeLicense.SelectedIndex == 0 ? "" : ddlTypeLicense.SelectedValue);
                string strYearMonth = string.Empty;
                if (ddlMonth.SelectedValue != "")
                {
                    strYearMonth = GetCrit(ConvertToYearMonth(ddlMonth.SelectedValue.ToInt()));
                }
                else
                {
                    strYearMonth = GetCrit(ConvertToYearMonth(DateTime.Today.Month));
                }
                string strTime = GetCrit(ddlTime.SelectedValue);

                //var ls = biz.GetExamByCriteria(strExamPlaceGroup, strExamPlace, strLicenseType, strYearMonth, strTime, null);

                //DataSet ds = ls.DataResponse;
                //DataTable dt = ds.Tables[0];

                //gvTable.DataSource = dt;
                //gvTable.DataBind();
                //gvTable.Visible = true;
                //boxgvTable.Visible = true;
            }
        }

        // โชว์ข้อมูลตามการเลือกรหัสสอบ
        private void BindExamByTestingNoAndPlaceCode(string testingNo, string placeCode)
        {
            BLL.ExamScheduleBiz biz = new BLL.ExamScheduleBiz();
            var exam = biz.GetExamByTestingNoAndPlaceCode(testingNo, placeCode);

            txtDetailExamCode.Text = exam.DataResponse.TESTING_NO;
            txtDetailDateExam.Text = exam.DataResponse.TESTING_DATE.ToString();
            txtDetailFee.Text = exam.DataResponse.EXAM_FEE.ToString();
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
                BindExamScheduleByTable();
            }
        }

        protected void gvExamSchedule_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblTotal = ((Label)e.Row.FindControl("lblTotalApply"));
                Label lblExamAdmission = ((Label)e.Row.FindControl("lblExamAdmission"));
                Label lblLicenseTypeCodeNumber = ((Label)e.Row.FindControl("lblLicenseTypeCodeNumber"));
                LinkButton view = ((LinkButton)e.Row.FindControl("hplview"));

                lblExamAdmission.Text = lblTotal.Text == null ? "0" : lblTotal.Text + "/" + lblExamAdmission.Text == null ? "0" : lblTotal.Text + "/" + lblExamAdmission.Text;
                LinkButton lbnEditGv = (LinkButton)e.Row.FindControl("lnkExamNumber");
                LinkButton lb = e.Row.FindControl("lnkExamNumber") as LinkButton;
                ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(lb);
            }
        }

        // ทุกๆครั้งที่เลือกรายการสอบแบบปฏิทิน
        protected void lnkExamNumber_Click(object sender, EventArgs e)
        {
            var gr = (GridViewRow)((LinkButton)sender).NamingContainer;
            var strExamNumber = (LinkButton)gr.FindControl("lnkExamNumber");
            var strPlaceCode = (Label)gr.FindControl("lblPlaceCode");
            BLL.ExamScheduleBiz biz = new BLL.ExamScheduleBiz();

            upn.Update();
            btnDelete.Visible = true;
            ModSingleApplicant.Show();
            EditMode(strExamNumber.Text, strPlaceCode.Text);
        }

        // ทุกๆครั้งที่เลือกรายการสอบแบบตาราง
        protected void lnkTableExamNumber_Click(object sender, EventArgs e)
        {
            var gr = (GridViewRow)((LinkButton)sender).NamingContainer;
            var strExamNumber = (LinkButton)gr.FindControl("lnkTableExamNumber");
            var strPlaceCode = (Label)gr.FindControl("lblPlaceCode");
            BLL.ExamScheduleBiz biz = new BLL.ExamScheduleBiz();

            upn.Update();
            btnDelete.Visible = true;
            ModSingleApplicant.Show();
            EditMode(strExamNumber.Text, strPlaceCode.Text);
            BindExamByTestingNoAndPlaceCode(strExamNumber.Text, strPlaceCode.Text);
        }

        protected void ddlPlaceGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetExamPlace();
            if (rblDisplay.SelectedValue == "1")
            {
                IsCanRender = false;
            }
            else if (rblDisplay.SelectedValue == "2")
            {
                IsCanRender = true;
            }
        }

        private void GetExamPlace()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetExamPlaceByCompCode(ddlPlaceGroup.SelectedIndex == 0 ? "" : ddlPlaceGroup.SelectedItem.Value, ddlPlaceGroup.SelectedItem.Value);
            BindToDDL(ddlPlace, ls.DataResponse);
            ddlPlace.Items.Insert(0, SysMessage.DefaultSelecting);

        }

        private void GetDetailExamPlace()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetExamPlaceByCompCode(ddlDetailYardGroupCode.SelectedIndex == 0 ? "" : ddlDetailYardGroupCode.SelectedItem.Value, ddlDetailYardGroupCode.SelectedItem.Value);
            BindToDDL(ddlDetailExamYardCode, ls.DataResponse);
            ddlDetailExamYardCode.Items.Insert(0, SysMessage.DefaultSelecting);
        }

        protected void ddlDetailYardGroupCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetExamPlaceByCompCode(ddlDetailYardGroupCode.SelectedIndex == 0 ? "" : ddlDetailYardGroupCode.SelectedItem.Value, ddlDetailYardGroupCode.SelectedItem.Value);
            BindToDDL(ddlDetailExamYardCode, ls.DataResponse);
            ddlDetailExamYardCode.Items.Insert(0, SysMessage.DefaultSelecting);


            upn.Update();
            ModSingleApplicant.Show();
            //IsCanRender = false;
        }

        private void ClearValue()
        {
            txtDetailExamCode.Text = string.Empty;
            txtDetailDateExam.Text = string.Empty;
            ddlDetailTimeExamCode.SelectedIndex = 0;
            ddlDetailYardGroupCode.SelectedIndex = 0;
            txtDetailFee.Text = string.Empty;
            if (ddlDetailExamYardCode.SelectedValue != "")
            {
                ddlDetailExamYardCode.Items.Clear();
            }
            ddlDetailOfficerCode.SelectedIndex = 0;
            txtDetailNumberOfSeat.Text = string.Empty;
        }

        private void NewMode()
        {
            BLL.ExamScheduleBiz biz = new BLL.ExamScheduleBiz();
            txtDetailFee.Text = biz.GetExamFee().DataResponse;
            btnDelete.Visible = false;
            GetDetailExamPlace();
        }

        private void SaveMode()
        {
            BLL.ExamScheduleBiz biz = new BLL.ExamScheduleBiz();

            if (string.IsNullOrEmpty(txtDetailExamCode.Text))
            {
                ExamSchedule ent = new ExamSchedule();

                ent.USER_ID = "AGDOI";
                ent.EXAM_PLACE_GROUP_CODE = ddlDetailYardGroupCode.SelectedValue;
                ent.EXAM_PLACE_CODE = ddlDetailExamYardCode.SelectedValue;
                ent.TESTING_DATE = Convert.ToDateTime(txtDetailDateExam.Text);
                ent.TEST_TIME_CODE = ddlDetailTimeExamCode.SelectedValue;
                ent.LICENSE_TYPE_CODE = ddlDetailOfficerCode.SelectedValue;
                ent.USER_DATE = DateTime.Now;
                ent.EXAM_ADMISSION = txtDetailNumberOfSeat.Text.ToShort();
                ent.EXAM_FEE = txtDetailFee.Text.ToDecimal();
                ent.EXAM_STATUS = "E";
                ent.EXAM_APPLY = 0;
                //if (base.UserProfile.MemberType == DTO.RegistrationType.OIC.GetEnumValue())
                //{
                //    ent.EXAM_OWNER = "B";
                //}
                //else if (base.UserProfile.MemberType == DTO.RegistrationType.TestCenter.GetEnumValue())
                //{
                //    ent.EXAM_OWNER = "C";
                //}
                //else
                //{
                //    ent.EXAM_OWNER = "A";
                //}
                if (Convert.ToDateTime(txtDetailDateExam.Text) > DateTime.Now.Date)
                {
                    var res = biz.InsertExam(ent);
                    if (res.IsError)
                    {
                        UCModalError.ShowMessageError = res.ErrorMsg;
                        UCModalError.ShowModalError();
                    }
                    else
                    {
                        UCModalSuccess.ShowMessageSuccess = SysMessage.SaveSucess;
                        UCModalSuccess.ShowModalSuccess();
                    }
                }
                else
                {
                    UCModalError.ShowMessageError = Resources.errorExamSchedule_001;
                    UCModalError.ShowModalError();
                }

            }
            else
            {
                string testingNo = txtDetailExamCode.Text;
                string examPlaceCode = ddlDetailExamYardCode.SelectedValue;
                if (biz.CanChangeExam(testingNo, examPlaceCode).ResultMessage)
                {
                    ExamSchedule ent = new ExamSchedule();
                    var exam = biz.GetExamByTestingNoAndPlaceCode(testingNo, examPlaceCode);
                    ent.TESTING_NO = txtDetailExamCode.Text;
                    ent.TESTING_DATE = Convert.ToDateTime(txtDetailDateExam.Text);
                    ent.EXAM_FEE = txtDetailFee.Text.ToShort();
                    ent.EXAM_STATUS = "E";
                    ent.EXAM_FEE = txtDetailFee.Text.ToShort();
                    ent.TEST_TIME_CODE = ddlDetailTimeExamCode.SelectedValue;
                    ent.EXAM_PLACE_GROUP_CODE = ddlDetailYardGroupCode.SelectedValue;
                    if (ddlDetailYardGroupCode.SelectedValue != "")
                    {
                        ent.EXAM_PLACE_CODE = ddlDetailExamYardCode.SelectedValue;
                    }
                    ent.LICENSE_TYPE_CODE = ddlDetailOfficerCode.SelectedValue;
                    ent.EXAM_APPLY = 0;
                    ent.EXAM_ADMISSION = txtDetailNumberOfSeat.Text.ToShort();
                    //ent.EXAM_OWNER = "A";
                    biz.UpdateExam(ent);

                    DateTime dtExam = Convert.ToDateTime(txtDetailDateExam.Text);
                    string strMonth = string.Empty;
                    if (dtExam.Date.Month < 10)
                    {
                        strMonth = "0" + dtExam.Date.Month;
                    }
                    //var re = biz.GetExamByCriteria("", "", "", dtExam.Year.ToString() + strMonth, "", Convert.ToDateTime(txtDetailDateExam.Text));
                    //if (re.IsError)
                    //{
                    //    UCModalError.ShowMessageError = re.ErrorMsg;
                    //    UCModalError.ShowModalError();

                    //}

                    //UCModalSuccess.ShowMessageSuccess = SysMessage.SaveSucess;
                    //UCModalSuccess.ShowModalSuccess();

                    //gvExamSchedule.DataSource = re.DataResponse;
                    //gvExamSchedule.DataBind();


                    //gvTable.DataSource = re.DataResponse;
                    //gvTable.DataBind();
                }
                else
                {
                    UCModalError.ShowMessageError = SysMessage.CannotEditExamTestingNo;
                    UCModalError.ShowModalError();
                }
            }
        }

        private void EditMode(string testingNo, string placeCode)
        {
            BLL.ExamScheduleBiz biz = new BLL.ExamScheduleBiz();
            var exam = biz.GetExamByTestingNoAndPlaceCode(testingNo, placeCode);
            txtDetailExamCode.Text = exam.DataResponse.TESTING_NO;
            txtDetailDateExam.Text = exam.DataResponse.TESTING_DATE.ToString("dd/MM/yyyy");
            txtDetailFee.Text = exam.DataResponse.EXAM_FEE.ToString();
            txtDetailFee.Text = biz.GetExamFee().DataResponse;
            ddlDetailTimeExamCode.SelectedValue = exam.DataResponse.TEST_TIME_CODE;
            ddlDetailYardGroupCode.SelectedValue = exam.DataResponse.EXAM_PLACE_GROUP_CODE;
            if (ddlDetailYardGroupCode.SelectedValue != "")
            {
                BLL.DataCenterBiz dbiz = new BLL.DataCenterBiz();
                var res = dbiz.GetExamPlaceByCompCode(ddlDetailYardGroupCode.SelectedIndex == 0 ? "" : ddlDetailYardGroupCode.SelectedItem.Value, ddlDetailYardGroupCode.SelectedItem.Value);
                BindToDDL(ddlDetailExamYardCode, res.DataResponse);
                ddlDetailExamYardCode.SelectedValue = exam.DataResponse.EXAM_PLACE_CODE;
            }
            ddlDetailOfficerCode.SelectedValue = exam.DataResponse.LICENSE_TYPE_CODE;
            txtDetailNumberOfSeat.Text = biz.GetSeatAmount(ddlDetailExamYardCode.SelectedValue).DataResponse;



        }

        private void DeleteMode()
        {
            string testingNo = txtDetailExamCode.Text;
            string examPlaceCode = ddlDetailExamYardCode.SelectedValue;
            BLL.ExamScheduleBiz biz = new BLL.ExamScheduleBiz();
            if (biz.CanChangeExam(testingNo, examPlaceCode).ResultMessage)
            {
                biz.DeleteExam(testingNo, examPlaceCode);
                Func<string, string> GetCrit = anyString =>
                {
                    return string.IsNullOrEmpty(anyString) ? string.Empty : anyString;
                };

                string strExamPlaceGroup = GetCrit(ddlPlaceGroup.SelectedValue);
                string strExamPlace = GetCrit(ddlPlace.SelectedValue);
                string strLicenseType = GetCrit(ddlTypeLicense.SelectedValue);
                string strYearMonth = GetCrit(ConvertToYearMonth(1));
                string strTime = GetCrit(ddlTime.SelectedValue);

                //var res = biz.GetExamByCriteria(strExamPlaceGroup, strExamPlace, strLicenseType, strYearMonth, strTime, null);


                //DataSet ds = res.DataResponse;
                //if (ds != null)
                //{
                //    DataTable dt = ds.Tables[0];
                //    if (dt.Rows.Count == 0)
                //    {
                //        Div1.Visible = false;
                //        boxgvTable.Visible = false;
                //        gvExamSchedule.Visible = false;
                //    }
                //    else
                //    {
                //        upn.Update();
                //        gvExamSchedule.DataSource = res.DataResponse;
                //        gvExamSchedule.DataBind();

                //        gvTable.DataSource = res.DataResponse;
                //        gvTable.DataBind();
                //    }
                //}
            }
            else
            {
                UCModalError.ShowMessageError = SysMessage.CannotDeleteExamTestingNo;
                UCModalError.ShowModalError();
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

        protected void cldExam_DayRender(object sender, DayRenderEventArgs e)
        {
            if (IsCanRender == true)
            {
                BLL.ExamScheduleBiz biz = new BLL.ExamScheduleBiz();
                Func<string, string> GetCrit = anyString =>
                {
                    return string.IsNullOrEmpty(anyString) ? string.Empty : anyString;
                };

                string strExamPlaceGroup = GetCrit(ddlPlaceGroup.SelectedValue);
                string strExamPlace = GetCrit(ddlPlace.SelectedIndex == 0 ? "" : ddlPlace.SelectedValue);
                string strLicenseType = GetCrit(ddlTypeLicense.SelectedIndex == 0 ? "" : ddlTypeLicense.SelectedValue);
                string strYearMonth = string.Empty;
                strYearMonth = GetCrit(ConvertToYearMonth(e.Day.Date.Month));
                string strTime = GetCrit(ddlTime.SelectedIndex == 0 ? "" : ddlTime.SelectedValue);

                //var ls = biz.GetExamByCriteria(strExamPlaceGroup, strExamPlace, strLicenseType, strYearMonth, strTime, null);

                //DataSet ds = ls.DataResponse;

                //if (ds != null)
                //{
                //    DataTable dt = ds.Tables[0];

                //    var list = dt.AsEnumerable().Select(s => s.Field<DateTime>("TESTING_DATE")).Distinct().ToList();

                //    for (int i = 0; i < list.Count; i++)
                //    {
                //        ExamRender examrender = new ExamRender();
                //        if (e.Day.Date == list[i].Date)
                //        {
                //            e.Cell.Controls.Add(new LiteralControl("</br>"));
                //            Label lbl = new Label { ID = "btn" + i.ToString(), Text = "รายละเอียด" };
                //            e.Cell.Controls.Add(lbl);
                //            examrender.testingDate = e.Day.Date;
                //            examrender.ID = "btn" + i.ToString();
                //            examrender.Name = "รายละเอียด";
                //            lsRender.Add(examrender);
                //        }
                //        else 
                //        {
                           
                //        }


                //    }
                //}

            }
            else
            {
                if (lsRender.Count != 0)
                {

                    DateTime dtToday = DateTime.Today;
                    DateTime dtFinish = DateTime.Today.AddDays(+5);
                    foreach (ExamRender item in lsRender)
                    {
                        if (e.Day.Date == item.testingDate)
                        {

                            e.Cell.Controls.Add(new LiteralControl("</br>"));
                            Label lbl = new Label { ID = item.ID, Text = item.Name };
                            e.Cell.Controls.Add(lbl);
                        }
                        if (e.Day.Date == item.testingDate && item.IsSetProperty)
                        {
                            e.Cell.Enabled = false;
                            e.Day.IsSelectable = false;
                            e.Cell.ForeColor = System.Drawing.Color.Gray;
                        }
                    }
                }
            }
        }

        // กำหนดค่าเริ่มต้นของจำนวนรับสมัคร โดยอ้างอิงจากสนามสอบ
        protected void ddlDetailExamYardCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            BLL.ExamScheduleBiz biz = new BLL.ExamScheduleBiz();
            txtDetailNumberOfSeat.Text = biz.GetSeatAmount(ddlDetailExamYardCode.SelectedValue).DataResponse;
            ModSingleApplicant.Show();
            //IsCanRender = false;
        }

        // ทุกๆครั้งที่มีการเปลี่ยนเดือนบนปฏิทิน
        protected void cldExam_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
        {
            if (e.NewDate.Month < 10)
            {
                ddlMonth.SelectedValue = "0" + Convert.ToString(e.NewDate.Month);
            }
            else
            {
                ddlMonth.SelectedValue = Convert.ToString(e.NewDate.Month);
            }
            txtYear.Text = Convert.ToString(e.NewDate.Year + 543);
            gvExamSchedule.DataSource = null;
            gvExamSchedule.DataBind();

            gvTable.DataSource = null;
            gvTable.DataBind();

            BLL.ExamScheduleBiz biz = new BLL.ExamScheduleBiz();
            Func<string, string> GetCrit = anyString =>
            {
                return string.IsNullOrEmpty(anyString) ? string.Empty : anyString;
            };

            string strExamPlaceGroup = GetCrit(ddlPlaceGroup.SelectedValue);
            string strExamPlace = GetCrit(ddlPlace.SelectedValue);
            string strLicenseType = GetCrit(ddlTypeLicense.SelectedValue);
            string strYearMonth = GetCrit(ConvertToYearMonth(e.NewDate.Month));
            string strTime = GetCrit(ddlTime.SelectedValue);

            //var ls = biz.GetExamByCriteria(strExamPlaceGroup, strExamPlace, strLicenseType, strYearMonth, strTime, null);

            //DataSet ds = ls.DataResponse;

            //if (ds.Tables.Count > 0)
            //{
            //    DataTable dt = ds.Tables[0];

            //    var list = dt.AsEnumerable().Select(s => s.Field<DateTime>("TESTING_DATE")).Distinct().ToList();

            //    for (int i = 0; i < list.Count; i++)
            //    {
            //        if (e.NewDate.Date == list[i].Date)
            //        {
            //            Controls.Add(new LiteralControl("</br>"));
            //            Label lbl = new Label { ID = "btn" + i.ToString(), Text = "รายละเอียด" };
            //            Controls.Add(lbl);

            //        }

            //    }

            //}

        }

        // ทุกๆการเลือกวันบนปฏิทิน
        protected void cldExam_SelectionChanged(object sender, EventArgs e)
        {

            gvExamSchedule.Visible = true;

            DateTime cldselectDate = cldExam.SelectedDate;

            string strYear = cldExam.SelectedDate.Year.ToString();
            string strMonth = cldExam.SelectedDate.Month.ToString();
            if (cldExam.SelectedDate.Month < 10)
            {
                strMonth = "0" + strMonth;
            }

            BLL.ExamScheduleBiz biz = new BLL.ExamScheduleBiz();
            Func<string, string> GetCrit = anyString =>
            {
                return string.IsNullOrEmpty(anyString) ? string.Empty : anyString;
            };
            //var res = biz.GetExamByCriteria(GetCrit(ddlPlaceGroup.SelectedIndex == 0 ? "" : ddlPlaceGroup.SelectedValue),
            //    GetCrit(ddlPlace.SelectedIndex == 0 ? "" : ddlPlace.SelectedValue), GetCrit(ddlTypeLicense.SelectedIndex == 0 ? "" : ddlTypeLicense.SelectedValue), strYear + strMonth, "", cldselectDate);
            //DataSet ds = res.DataResponse;
            //if (ds != null)
            //{
            //    DataTable dt = ds.Tables[0];
            //    if (dt.Rows.Count == 0)
            //    {
            //        Div1.Visible = false;
            //        boxgvTable.Visible = false;
            //        gvExamSchedule.Visible = false;

            //    }
            //    else
            //    {
            //        gvExamSchedule.Visible = true;
            //        gvExamSchedule.DataSource = res.DataResponse;
            //        gvExamSchedule.DataBind();
            //        Div1.Visible = true;
            //        boxgvTable.Visible = false;
            //    }


            //}
        }

        // ค้นหาประเภทของการแสดงผลตารางสมัครสอบ
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lsRender.Clear();
            IsCanRender = true;
            gvExamSchedule.Visible = false;

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
                    cldExam.TodaysDate = new DateTime(strYear.ToInt(), strMonth.ToInt(), 1);
                    upn.Update();
                }
            }
            else
            {
                pnlCalendar.Visible = false;
                pnlTable.Visible = true;
                BindExamScheduleByTable();
            }
        }

        // เพิ่มใบสมัครสอบ
        protected void btnInsert_Click(object sender, EventArgs e)
        {
            ModSingleApplicant.Show();
            ClearValue();
            NewMode();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            lsRender.Clear();
            IsCanRender = true;
            DeleteMode();
            //gvExamSchedule.Visible = false;
            //gvTable.Visible = false;
        }

        protected void btnDetailSubmit_Click(object sender, EventArgs e)
        {
            lsRender.Clear();
            IsCanRender = true;
            SaveMode();
            //gvExamSchedule.Visible = false;
            gvTable.Visible = false;
        }

        protected void gvTable_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTable.PageIndex = e.NewPageIndex;
            gvTable.DataBind();
        }

        protected void gvExamSchedule_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void ddlDetailYardGroupCode_SelectedIndexChanged1(object sender, EventArgs e)
        {
            GetDetailExamPlaceOIC();
        }

        private void GetDetailExamPlaceOIC()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetExamPlaceByCompCode(ddlDetailYardGroupCode.SelectedIndex == 0 ? "" : ddlDetailYardGroupCode.SelectedItem.Value, ddlDetailYardGroupCode.SelectedItem.Value);
            BindToDDL(ddlDetailExamYardCode, ls.DataResponse);
            ddlDetailExamYardCode.Items.Insert(0, SysMessage.DefaultSelecting);

        }

        public class VariableTemp
        {
            public static IAS.DTO.ExamRender ValueTemp = new IAS.DTO.ExamRender();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ModSingleApplicant.Hide();
        }

        protected void gvTable_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblTotal = ((Label)e.Row.FindControl("lblTotalApply"));
                Label lblExamAdmission = ((Label)e.Row.FindControl("lblExamAdmission"));
                Label lblLicenseTypeCodeNumber = ((Label)e.Row.FindControl("lblLicenseTypeCodeNumber"));
                LinkButton view = ((LinkButton)e.Row.FindControl("hplview"));
                if (!string.IsNullOrEmpty(lblTotal.Text))
                {
                    lblExamAdmission.Text = lblTotal.Text + "/" + lblExamAdmission.Text;

                }
                else
                {
                    lblExamAdmission.Text = lblTotal.Text + "/" + "0";
                }
                LinkButton lbnEditGv = (LinkButton)e.Row.FindControl("lnkTableExamNumber");
                LinkButton lb = e.Row.FindControl("lnkTableExamNumber") as LinkButton;
                ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(lb);
            }
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            DefaultDataWhenLoad();
        }

    }
}
