using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Globalization;
using IAS.Utils;
using System.IO;
using System.Data;
using IAS.Properties;
using IAS.DTO;
using AjaxControlToolkit;
using IAS.BLL;
using System.Text.RegularExpressions;
using System.Text;
using System.Configuration;
using System.Web.Configuration;



namespace IAS.Exam
{
    public partial class ExamResult : basepage
    {
        #region OldCode
       
        protected void Page_Load(object sender, EventArgs e)
        {
            //ScriptManager.RegisterStartupScript(this, GetType(), "textreadonly", "setdate();", true);

        
            if (!Page.IsPostBack)
            {
                base.HasPermit();
                FileUploadPanel.Attributes.Add("style", "visibility:hidden");

                gvCheckList.DataSource = null;
                gvCheckList.DataBind();
                PnlImport.Visible = false;
                gvSubject.DataSource = null;
                gvSubject.DataBind();
                Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");
                txtExpireExamDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                txtExpireExamDate.Text = DateUtil.dd_MM_yyyy_Now_TH;
                bludDiv1.Visible = false;
                bludDiv2.Visible = false;
                PnlImport.Visible = false;
                GVScorevieW.Visible = true;
                DefaultData();
            }
        }

        protected void btnLoadFile_Click(object sender, EventArgs e)
        {
            UpdatePanelUpload.Update();
            btnConfirm.Visible = true;
            //UpdatePanelUpload.Update();
            BindDataInGrid();
            
        }

        protected void hplView_Click(object sender, EventArgs e)
        {
            try
            {
                UpdatePanelUpload.Update();
                var gr = (GridViewRow)((LinkButton)sender).NamingContainer;
                DataTable DTview = new DataTable();

                var seqNo = (Label)gr.FindControl("lblSeqNoGv");

                if (seqNo != null)
                {
                    var biz = new BLL.ExamResultBiz();
                    var exambiz = new BLL.ExamRoomBiz();
                    var res = biz.GetExamResultTempEdit(hdfGroupID.Value, seqNo.Text);

                    if (res.IsError)
                    {
                        
                        this.MasterSite.ModelError.ShowMessageError = res.ErrorMsg;
                        this.MasterSite.ModelError.ShowModalError();
                    }
                    else
                    {
                        var Headder = res.DataResponse.Header;
                        var detail = res.DataResponse.Detail;

                        txtAssociationName.Text = Headder.ASSOCIATE_NAME;
                        txtAssociationID.Text = Headder.ASSOCIATE_CODE;
                        txtProvinceID.Text = detail.AREA_CODE;
                        txtTypePermitID.Text = Headder.LICENSE_TYPE_CODE;
                        txtExamDate.Text = Headder.TESTING_DATE.Substring(0, 6) + "" + (Headder.TESTING_DATE.Substring(6, 4).ToInt());
                        txtTimeExamID.Text = Headder.EXAM_TIME_CODE;
                        txtExamSitNumber.Text = detail.SEAT_NO;
                        txtTitleName.Text = detail.TITLE;
                        txtName.Text = detail.NAMES;
                        txtLastName.Text = detail.LAST_NAME;
                        txtIdCardNumber.Text = detail.ID_CARD_NO;
                        txtAddress1.Text = detail.ADDRESS1;
                        txtAddress2.Text = detail.ADDRESS2;
                        txtAreaCode.Text = detail.AREA_CODE;

                        if (detail.BIRTH_DATE != null)
                        {
                            txtBirthDay.Text = detail.BIRTH_DATE.Value.ToString("dd/MM/yyyy");
                        }

                        txtSex.Text = (detail.SEX=="F")?"หญิง":(detail.SEX =="M")?"ชาย":detail.SEX;
                        txtEducationCode.Text = detail.EDUCATION_CODE;
                        txtAffiliatedInsuranceID.Text = detail.COMP_CODE;

                        if (detail.APPROVE_DATE != null)
                        {
                            txtApproveDate.Text = detail.APPROVE_DATE.Value.ToString("dd/MM/yyyy");
                        }

                        txtResultExam.Text = detail.EXAM_RESULT==null?Resources.infoExamResult_001:detail.EXAM_RESULT=="F"?Resources.infoExamResult_002:detail.EXAM_RESULT=="P"?Resources.infoExamResult_003:Resources.infoExamResult_004;

                        DTview.Columns.Add("ListNo");
                        DTview.Columns.Add("SubCode");
                        DTview.Columns.Add("SubName");
                        DTview.Columns.Add("FullScore");
                        DTview.Columns.Add("Score");


                        //var testing_no = biz.GetTestingNoFrom_fileImport(res.DataResponse.Header);
                        var abiz = new BLL.ApplicantBiz();
                        var examDetail = exambiz.GetExamByTestingNo(testingNoInput.Text);
                        decimal CourseNumber = examDetail.DataResponse.COURSE_NUMBER;
                        string strCourseNumber = Convert.ToString(CourseNumber);
                        var resList = biz.Subject_List(strCourseNumber);
                        DataSet DSlist = resList.DataResponse;
                        DataTable DTlist = DSlist.Tables[0];

                        for (int scoreE = 0; scoreE <= 15; scoreE++)
                        {
                            InsertGridLine(DTview, detail, DTlist, scoreE);
                          
                        }

                        

                        //UplPopUp.Update();
                        //GVScorevieW.DataSource = DTview.DataSet;
                        GVScorevieW.DataSource = DTview;
                        GVScorevieW.DataBind();


                        //PnlPopup.Visible = true;
                        GVScorevieW.Visible = true;
                       // udpUpload.Update();
                        EnableControl();
                        mpDetail.Show();
                        //blueDiv.Visible = true;
                        //UplPopUp.Update();
 

                    }
                }


            }
            catch(Exception ex)
            { 
            
            }
        }

        private static void InsertGridLine(DataTable DTview, DTO.ExamResultTemp detail, DataTable DTlist, int scoreE)
        {
            string score_EE = "";
            switch (scoreE)
            {
                case 0: score_EE = detail.SCORE_1; break;
                case 1: score_EE = detail.SCORE_2; break;
                case 2: score_EE = detail.SCORE_3; break;
                case 3: score_EE = detail.SCORE_4; break;
                case 4: score_EE = detail.SCORE_5; break;
                case 5: score_EE = detail.SCORE_6; break;
                case 6: score_EE = detail.SCORE_7; break;
                case 7: score_EE = detail.SCORE_8; break;
                case 8: score_EE = detail.SCORE_9; break;
                case 9: score_EE = detail.SCORE_10; break;
                case 10: score_EE = detail.SCORE_11; break;
                case 11: score_EE = detail.SCORE_12; break;
                case 12: score_EE = detail.SCORE_13; break;
                case 13: score_EE = detail.SCORE_14; break;
                case 14: score_EE = detail.SCORE_15; break;
                case 15: score_EE = detail.SCORE_16; break;
                default: score_EE = ""; break;
            }

            DataRow DR = DTview.NewRow();
            if (score_EE != "")
            {
                if (scoreE >= DTlist.Rows.Count)
                {
                    if (score_EE.ToInt() > 0)
                    {
                        DR["ListNo"] = scoreE + 1;
                        DR["SubCode"] = "";
                        DR["SubName"] = "";
                        DR["FullScore"] = "";
                        DR["Score"] = score_EE;
                        DTview.Rows.Add(DR);
                    }
                }
                else
                {
                    DR["ListNo"] = scoreE + 1;
                    DR["SubCode"] = DTlist.Rows[scoreE]["SUBJECT_CODE"].ToString();
                    DR["SubName"] = DTlist.Rows[scoreE]["SUBJECT_NAME"].ToString();
                    DR["FullScore"] = DTlist.Rows[scoreE]["FULLSCORE"].ToString();
                    DR["Score"] = score_EE;
                    DTview.Rows.Add(DR);
                }
            }
            else
            {
                if (scoreE < DTlist.Rows.Count)
                {
                    DR["ListNo"] = scoreE + 1;
                    DR["SubCode"] = DTlist.Rows[scoreE]["SUBJECT_CODE"].ToString();
                    DR["SubName"] = DTlist.Rows[scoreE]["SUBJECT_NAME"].ToString();
                    DR["FullScore"] = DTlist.Rows[scoreE]["FULLSCORE"].ToString();
                    DR["Score"] = "";
                    DTview.Rows.Add(DR);
                }
            }
        }

        private void EnableControl()
        {
            txtAssociationName.Enabled = false;
            txtTypePermitID.Enabled = false;
            txtProvinceID.Enabled = false;
            txtAssociationID.Enabled = false;
            txtExamDate.Enabled = false;
            txtTimeExamID.Enabled = false;
            txtExamSitNumber.Enabled = false;
            txtTitleName.Enabled = false;
            txtName.Enabled = false;
            txtLastName.Enabled = false;
            txtIdCardNumber.Enabled = false;
            txtAddress1.Enabled = false;
            txtAddress2.Enabled = false;
            txtAreaCode.Enabled = false;
            txtBirthDay.Enabled = false;
            txtSex.Enabled = false;
            txtEducationCode.Enabled = false;
            txtAffiliatedInsuranceID.Enabled = false;
            txtApproveDate.Enabled = false;
            txtResultExam.Enabled = false;
            GVScorevieW.Enabled = false;


        }

        private void BindDataInGrid()
        {
            UpdatePanelUpload.Update();
            if (fUpload.HasFile)
            {
                string fileName = fUpload.FileName;
                bool invalid = validateFileType(fileName);

                if (invalid)
                {
                    var biz = new BLL.ExamResultBiz();
                    var res = biz.UploadData(fUpload.FileName, fUpload.PostedFile.InputStream, base.UserId, testingNoInput.Text);
                    if (res.IsError)
                    {
                      
                        this.MasterSite.ModelError.ShowMessageError = res.ErrorMsg;
                        this.MasterSite.ModelError.ShowModalError();
                    
                    }
                    else
                    {
                        PnlImport.Visible = true;

                        var header = res.DataResponse.Header;
                        var detail = res.DataResponse.Detail;
                        var groupID = res.DataResponse.GroupId;

                        gvSubject.DataSource = header;
                        gvSubject.DataBind();

                        gvCheckList.DataSource = detail;
                        gvCheckList.DataBind();
                        bludDiv2.Visible = true;
                        bludDiv1.Visible = true;
                        hdfGroupID.Value = groupID;

                    }
                   
                }
                else
                {
                    this.MasterSite.ModelError.ShowMessageError = SysMessage.PleaseChooseFile;
                    this.MasterSite.ModelError.ShowModalError();
                }
               
            }
            else
            {
                this.MasterSite.ModelError.ShowMessageError = SysMessage.CannotAttachFile;
                this.MasterSite.ModelError.ShowModalError();
                ClearAndHide("");
            }
        }

        private static bool validateFileType(string fileName)
        {
            string fileExtension = System.IO.Path.GetExtension(fileName).Replace(".", string.Empty).ToLower();
            bool invalidFileExtensions = DTO.DocumentFileTXT.TXT.ToString().ToLower().Contains(fileExtension);
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

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            if (testingNoInput.Text.Trim() != "")
            {
                var biz = new BLL.ExamResultBiz();
                //Label lblTestNo = (Label)gvSubject.Rows[0].Cells[4].FindControl("lblTestingNo");
                string strTestNo = testingNoInput.Text;
                var res = biz.ExamResultUploadToSubmitNew(hdfGroupID.Value, UserProfile.Id, Convert.ToDateTime(txtExpireExamDate.Text), strTestNo);
                UpdatePanelUpload.Update();
                if (res.IsError)
                {

                    this.MasterSite.ModelError.ShowMessageError = res.ErrorMsg;
                    this.MasterSite.ModelError.ShowModalError();
                }
                else
                {
                    this.MasterSite.ModelSuccess.ShowMessageSuccess = SysMessage.SuccessImportData;
                    this.MasterSite.ModelSuccess.ShowModalSuccess();
                    ClearAndHide("all");
                }
            }
            else
            {
                this.MasterSite.ModelError.ShowMessageError = "ไม่พบรหัสรอบสอบ";
                this.MasterSite.ModelError.ShowModalError();
            }
        }
        
        protected void ClearAndHide(string type)         
        {
            if(type =="all")
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");
                txtExpireExamDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                txtExpireExamDate.Text = DateUtil.dd_MM_yyyy_Now_TH;
            }
            PnlImport.Visible = false;
            gvSubject.DataMember = null;
            gvCheckList.DataMember = null;
            gvCheckList.DataBind();
            gvSubject.DataBind();
            bludDiv1.Visible = false;
            bludDiv2.Visible = false;
           // blueDiv.Visible = false;
            mpDetail.Hide();
        }

        protected void gvSubject_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label errortxt = (Label)e.Row.FindControl("lblItemsIncorrectGv");
                if(errortxt.Text.ToInt()>0)
                {
                    btnConfirm.Visible = false;
                }
            }
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            //Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");
            //txtExpireExamDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
            //txtExpireExamDate.Text = DateUtil.dd_MM_yyyy_Now_TH;
            bludDiv1.Visible = false;
            bludDiv2.Visible = false;
            PnlImport.Visible = false;
           // blueDiv.Visible = false;
            mpDetail.Hide();
            gvSubject.DataSource = null;
            gvSubject.DataBind();
            gvCheckList.DataSource = null;
            gvCheckList.DataBind();
        }

        #endregion OldCode


        #region SingleApplicantCode

        #region Pageing_milk

        protected void VisibleGV(GridView GVname, double total_row_count, double rows_per_page, Boolean visible_or_disvisible)
        {
            switch (GVname.ID.ToString())
            {
                case "gvExamSchedule":
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
            UpdatePanelUpload.Update();
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
            BindExamScheduleByTableDefault(false);
            Hide_show(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "", txtTotalPage.Text.ToInt());
            UpdatePanelUpload.Update();
        }
        
        protected void btnNextGvSearch_Click(object sender, EventArgs e)
        {
            NPbutton(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "N", txtTotalPage);
            BindExamScheduleByTableDefault(false);
            Hide_show(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "", txtTotalPage.Text.ToInt());
            UpdatePanelUpload.Update();
        }
        
        #endregion Pageing_milk

        #region ParaCode

        public IAS.MasterPage.Site1 MasterSite
        {
            get
            {
                return (this.Page.Master as IAS.MasterPage.Site1);
            }
        }

        public int PageSize;

        private Boolean _isCanExRender = true;

        private Action<DropDownList, DTO.DataItem[]> BindToDDL = (ddl, ls) =>
        {
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Id";
            ddl.DataSource = ls;
            ddl.DataBind();
        };

        public Boolean IsCanExRender
        {
            get { return _isCanExRender; }
            set { _isCanExRender = value; ViewState["IsCanExRender"] = _isCanExRender; }
        }

        private int daysCounter;
        public List<DTO.ExamRender> lsExRender
        {
            get
            {
                if (Session["lsExRender"] == null)
                {
                    Session["lsExRender"] = new List<DTO.ExamRender>();
                }

                return (List<DTO.ExamRender>)Session["lsExRender"];
            }
            set
            {
                Session["lsExRender"] = value;
            }
        }

        protected void ShowOrHide(Boolean S_H)//milk
        {
            if (S_H)
            {
                if (rblDisplay.SelectedValue == "1")
                {
                    pnlCalendar.Visible = S_H;
                    pnlTable.Visible = !S_H;
                }
                else
                {
                    pnlTable.Visible = S_H;
                    pnlCalendar.Visible = !S_H;
                }
            }
        }

        #endregion ParaCode

        protected void btnCancleExam_Click(object sender, EventArgs e)
        {
            ShowOrHide(false);//milk
            txtNumberGvSearch.Text = "0";//milk
            DefaultData();
            SetFirstLoad();
            CheckDisplayExam();
            testingNoInput.Text = "";
            FileUploadPanel.Attributes.Add("style", "visibility:hidden");
            PnlImport.Visible = false;
            gvSubject.DataSource = null;
            gvSubject.DataBind();
            gvCheckList.DataSource = null;
            gvCheckList.DataBind();
        }

        private void CheckDisplayExam()
        {
            if (rblDisplay.SelectedValue == "1")
            {
                pnlCalendar.Visible = true;
                Div1.Visible = false;
                table_Paging.Visible = false; //ซ่อน Paging เพราะเป็นปฏิทิน
                txtNumberGvSearch.Text = "0";
                if (txtYear.Text.Trim().Length < 4)
                    txtYear.Text = Convert.ToString(Convert.ToInt16(DateTime.Now.Year) + 543);
                if (ddlMonth.SelectedIndex == 0)
                    ddlMonth.SelectedIndex = Convert.ToInt16(DateTime.Now.Month);
                SelectDataFrombase(Convert.ToDateTime(txtYear.Text + @"/" + ddlMonth.SelectedIndex.ToString("00") + @"/01"));
                UpdatePanelUpload.Update();
            }
            else
            {
                pnlTable.Visible = true;
                pnlCalendar.Visible = false;
                if (txtYear.Text.Trim().Length < 4)
                    txtYear.Text = Convert.ToString(Convert.ToInt16(DateTime.Now.Year) + 543);
                if (ddlMonth.SelectedIndex == 0)
                    ddlMonth.SelectedIndex = Convert.ToInt16(DateTime.Now.Month);
                BindExamScheduleByTableDefault(true);
                UpdatePanelUpload.Update();
            }
        }

        private bool isExLastRow = false;

        protected void cldExam_DayRender(object sender, DayRenderEventArgs e)
        {
            #region Day Render Rows
            /********** render day ************/
            daysCounter++;
            if (e.Day.Date.Day == 1 && !e.Day.IsOtherMonth) // 1st of current month. Turn visibility of row to ON.
            {
                isExLastRow = false;
            }
            else if (daysCounter == 36 && e.Day.IsOtherMonth) // 5 rows already rendered. If its the next row is next month, hide it.
            {
                isExLastRow = true;
            }
            else if (daysCounter == 1 && e.Day.IsOtherMonth && e.Day.Date.Month == e.Day.Date.AddDays(6).Month)
            {   // If first row completely is previous month, hide it.
                // Actually the flag should be isFirstRow, but I dont want one more boolean just for the sake of it.
                isExLastRow = true;
            }

            if (isExLastRow)
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
            if (IsCanExRender == true)
            {
                BLL.ExamScheduleBiz biz = new BLL.ExamScheduleBiz();
                Func<string, string> GetCrit = anyString =>
                {
                    return string.IsNullOrEmpty(anyString) ? string.Empty : anyString;
                };

                // Check First Load and Save Session

                if (Session["lsExDetailCalendar"] != null)
                {
                    DataSet ds = (DataSet)Session["lsExDetailCalendar"];
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataTable dt = ds.Tables[0];
                        var list = dt.AsEnumerable().Where(s => s.Field<DateTime>("TESTING_DATE").Date == e.Day.Date).ToList();
                        DateTime dtToday = DateTime.Today.AddDays(-1);
                        DateTime dtFinish = DateTime.Today.AddDays(+5);

                        ExamRender examrender = new ExamRender();
                        if (list != null && list.Count > 0)
                        {
                            e.Cell.Controls.Add(new LiteralControl("</br>"));
                            LinkButton lnk = new LinkButton();
                            lnk.ID = "lnkButton";
                            lnk.Text = "รายละเอียด";
                            lnk.ForeColor = System.Drawing.Color.Green;
                            examrender.ID = "lnkButton";
                            examrender.Name = "รายละเอียด";
                            examrender.testingDate = e.Day.Date;
                            lsExRender.Add(examrender);
                            e.Cell.Attributes.Add("onclick", e.SelectUrl);
                            e.Cell.Style.Add("cursor", "pointer");

                            //ใช้นะ
                                //DateTime dtTest = list[0].Field<DateTime>("TESTING_DATE").Date;
                                //if (e.Day.Date > DateTime.Now.Date || dtTest > DateTime.Now.Date)
                                //{
                                //    lnk.Enabled = false;
                                //    lnk.ForeColor = System.Drawing.Color.Gray;
                                //    e.Cell.Attributes.Remove("onclick");
                                //    e.Cell.Style.Remove("cursor");
                                //    e.Cell.Style.Remove("pointer");
                                //}
                            
                            e.Cell.Controls.Add(lnk);
                        }
                    }
                }
                if (Session["lsExHoliday"] != null)
                {
                    DTO.ResponseService<DTO.GBHoliday[]> lsHoliday = (DTO.ResponseService<DTO.GBHoliday[]>)Session["lsExHoliday"];
                    if (lsHoliday.DataResponse.Count() > 0)
                    {
                        //DataTable dt = dsHoliday.Tables[0];
                        var list = lsHoliday.DataResponse.Where(s => s.HL_DATE.Date == e.Day.Date).ToList();
                        DateTime dtToday = DateTime.Today.AddDays(-1);
                        DateTime dtFinish = DateTime.Today.AddDays(+5);

                        ExamRender examrender = new ExamRender();
                        if (list != null && list.Count > 0)
                        {
                            e.Cell.Controls.Add(new LiteralControl("</br>"));
                            Label lnk = new Label();
                            lnk.ID = "lnkButton";
                            lnk.Enabled = false;
                            lnk.Text = list[0].HL_DESC;
                            lnk.ForeColor = System.Drawing.Color.Red;
                            examrender.ID = "lnkButton";
                            examrender.Name = list[0].HL_DESC;
                            examrender.testingDate = e.Day.Date;
                            lsExRender.Add(examrender);
                            e.Cell.Attributes.Add("onclick", e.SelectUrl);
                            e.Cell.Style.Add("cursor", "pointer");
                            e.Cell.Controls.Add(lnk);
                        }
                    }
                }
            }
        }

        protected void cldExam_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                FileUploadPanel.Attributes.Add("style", "visibility:hidden");
                testingNoInput.Text = "";
                PnlImport.Visible = false;
                gvSubject.DataSource = null;
                gvSubject.DataBind();
                gvCheckList.DataSource = null;
                gvCheckList.DataBind();
                DateTime cldselectDate = cldExam.SelectedDate;
                string strMonth = ConvertToYearMonth(cldExam.SelectedDate.Year, cldExam.SelectedDate.Month);
                BLL.ExamScheduleBiz biz = new BLL.ExamScheduleBiz();
                DataSet ds = (DataSet)Session["lsExDetailCalendar"];
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                }

                // Get Session from Criteria Before
                string strExamPlaceGroup = Session["strExExamPlaceGroup"].ToString();
                string strExamPlace = Session["strExExamPlace"].ToString();
                string strLicenseType = Session["strExLicenseType"].ToString();
                string strYearMonth = Session["strExYearMonth"].ToString();
                string strTime = Session["strExTime"].ToString();

                string Owner = "";

                if (base.UserProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue())
                    Owner = base.UserProfile.CompCode;

                #region page
                int Rpage = (txtNumberGvSearch.Text.Trim() == "") ? 0 : txtNumberGvSearch.Text.Trim().ToInt();
                int resultPage = (Rpage == 0) ? 1 : txtNumberGvSearch.Text.Trim().ToInt();
                resultPage = resultPage == 0 ? 1 : resultPage;
                if ((rowPerpage.Text.Trim() == null) || (rowPerpage.Text.Trim() == "") || (rowPerpage.Text.Trim() == "0"))
                {
                    rowPerpage.Text = PAGE_SIZE_Key.ToString();
                    PageSize = PAGE_SIZE_Key;
                }
                else
                {
                    PageSize = Convert.ToInt32(rowPerpage.Text);
                }
                #endregion page

                var res = biz.GetExamByCriteria(strExamPlaceGroup, strExamPlace, strLicenseType,"", strYearMonth, strTime, cldselectDate, resultPage, PageSize, false,Owner);
                if (res.DataResponse != null)
                {
                    if (res.DataResponse.Tables[0].Rows.Count > 0)
                    {
                        Div1.Visible = true;
                        pnlTable.Visible = true;
                        gvExamSchedule.Visible = true;
                        gvExamSchedule.DataSource = res.DataResponse;
                        gvExamSchedule.DataBind();
                    }
                    else
                    {
                        pnlTable.Visible = false;
                    }
                }
            }
            catch
            {
            }
        }

        protected void cldExam_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
        {
            //btnConfirm_Click(sender,e);
            pnlTable.Visible = false;
            cldExam.SelectedDates.Clear();

            #region set background today
            if (cldExam.VisibleDate.ToString("MMyyyy") == DateTime.Now.ToString("MMyyyy"))
            {
                cldExam.TodaysDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                cldExam.VisibleDate = cldExam.TodaysDate;
                cldExam.TodayDayStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#fbfba6");
            }
            #endregion

            SelectDataFrombase(((System.Web.UI.WebControls.Calendar)sender).VisibleDate);

        }

        protected void gvExamSchedule_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvExamSchedule.PageIndex = e.NewPageIndex;
            gvExamSchedule.DataBind();
        }
        
        private int CheckToYear(int iYear)
        {
            if (iYear < 2500)
            {
                return (iYear + 543);
            }
            else
            {
                return iYear;
            }

        }

        private void SetFirstLoad()
        {
            cldExam.VisibleDate = DateTime.Now;
            cldExam.SelectedDate = DateTime.Now;
            txtYear.Text = CheckToYear(cldExam.VisibleDate.Year).ToString();
            ddlPlace.Items.Clear();
            SelectDataFrombase(cldExam.VisibleDate);
            UpdatePanelUpload.Update();
            cldExam.TodayDayStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#fbfba6");
            rblDisplay.SelectedValue = "1";
        }

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
            var ls = biz.GetExamPlaceGroup(SysMessage.DefaultSelecting);
            BindToDDL(ddlPlaceGroup, ls.DataResponse);
        }

        private void GetExamTime()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetExamTime(SysMessage.DefaultSelecting);
            BindToDDL(ddlTime, ls.DataResponse);
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

        private void GetLicenseType()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetLicenseTypeByCompCode(base.UserProfile.CompCode);
            BindToDDL(ddlTypeLicense, ls.DataResponse);
        }

        private void GetLicenseTypeCreateTest()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetLicenseTypeByCreateTest(base.UserProfile);
            BindToDDL(ddlTypeLicense, ls.DataResponse);
        }

        private void GetAssociationLicenseByAssocCode()
        {
            BLL.DataCenterBiz biz = new DataCenterBiz();
            var ls = biz.GetAssociationLicenseByAssocCode(base.UserProfile.CompCode);
            BindToDDL(ddlTypeLicense, ls.DataResponse);
        }

        protected void DefaultData() // ข้อมูลแรกเริ่มตั้งต้น (พี่ฟิลด์เขียนไว้มิ้วจับยกออกมาข้างนอก)
        {
            CalculateMinMaxYears();
            IsCanExRender = true;
            base.HasPermit();
           

            if (base.UserProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue())
            {

                pnlCalendar.Visible = true;
                pnlSearch.Visible = true;
                pnlTable.Visible = false;

                GetAssociationLicenseByAssocCode();
                GetLicenseType();
                GetExamTime();
                GetExamPlaceGroup();
                GetMonth();
            }
            else if (base.UserProfile.MemberType == DTO.RegistrationType.OIC.GetEnumValue() || base.UserProfile.MemberType == DTO.RegistrationType.OICAgent.GetEnumValue())
            {
                GetExamPlaceGroupOIC();
                GetLicenseTypeCreateTest();
                GetExamTime();
                GetMonth();
                lsExRender.Clear();
                Div1.Visible = false;
            }

            txtNumberGvSearch.Text = "0";//milk
            cldExam.VisibleDate = DateTime.Now;
            txtYear.Text = CheckToYear(cldExam.VisibleDate.Year).ToString();
            this.PageSize = PAGE_SIZE_Key;
            SelectDataFrombase(cldExam.VisibleDate);
        }

        private void GetExamSchedulePlaceGroup()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetExamPlaceGroupByCompCode(SysMessage.DefaultSelecting, base.UserProfile.CompCode);
            BindToDDL(ddlPlaceGroup, ls.DataResponse);
        }

        private void GetExamPlaceGroupOIC()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetExamPlaceGroup(SysMessage.DefaultSelecting);
            BindToDDL(ddlPlaceGroup, ls.DataResponse);
        }

        private void BindExamScheduleByTableDefault(Boolean CountAgain) //milk มาแก้ไขเพื่อเพิ่ม paging เท่านั้น
        {
            if (IsCanExRender == true)
            {

                #region page
                int Rpage = (txtNumberGvSearch.Text.Trim() == "") ? 0 : txtNumberGvSearch.Text.Trim().ToInt();
                int resultPage = (Rpage == 0) ? 1 : txtNumberGvSearch.Text.Trim().ToInt();
                resultPage = resultPage == 0 ? 1 : resultPage;
                if ((rowPerpage.Text.Trim() == null) || (rowPerpage.Text.Trim() == "") || (rowPerpage.Text.Trim() == "0"))
                {
                    rowPerpage.Text = PAGE_SIZE_Key.ToString();
                    PageSize = PAGE_SIZE_Key;
                }
                else
                {
                    PageSize = Convert.ToInt32(rowPerpage.Text);
                }
                #endregion page

                BLL.ExamScheduleBiz biz = new BLL.ExamScheduleBiz();
                string strYearMonth = string.Empty;
                if (ddlMonth.SelectedValue != null)
                {
                    strYearMonth = (ConvertToYearMonth(ddlMonth.SelectedValue.ToInt()));
                }
                else
                {
                    strYearMonth = (ConvertToYearMonth(DateTime.Now.Month.ToInt()));
                }

                string Owner = "";

                if (base.UserProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue())
                    Owner = base.UserProfile.CompCode;

                if (CountAgain)
                {
                    #region Page
                    var CountPage = biz.GetExamByCriteriaDefault(ddlPlaceGroup.SelectedValue, ddlPlace.SelectedIndex == 0 ? "" : ddlPlace.SelectedValue, ddlTypeLicense.SelectedValue == "" ? null : ddlTypeLicense.SelectedValue, base.UserProfile.AgentType == null ? "" : base.UserProfile.AgentType, strYearMonth, ddlTime.SelectedIndex == 0 ? "" : ddlTime.SelectedValue, null, resultPage, PageSize, true,Owner);


                    if (CountPage.DataResponse != null)
                        if (CountPage.DataResponse.Tables[0].Rows.Count > 0)
                        {
                            Int64 totalROWs = Convert.ToInt32(CountPage.DataResponse.Tables[0].Rows[0]["CCount"].ToString());

                            VisibleGV(gvExamSchedule, totalROWs, Convert.ToInt32(rowPerpage.Text), true);
                            if (Rpage == 0)
                                NPbutton(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "", txtTotalPage);
                        }
                        else
                        {
                            VisibleGV(gvExamSchedule, 0, Convert.ToInt32(rowPerpage.Text), true);
                            if (Rpage == 0)
                                NPbutton(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "", txtTotalPage);
                            txtTotalPage.Text = "1";
                        }
                    #endregion Page
                }
                
                var ls = biz.GetExamByCriteriaDefault(ddlPlaceGroup.SelectedValue, ddlPlace.SelectedIndex == 0 ? "" : ddlPlace.SelectedValue, ddlTypeLicense.SelectedValue == "" ? null : ddlTypeLicense.SelectedValue, base.UserProfile.AgentType == null ? "" : base.UserProfile.AgentType, strYearMonth, ddlTime.SelectedIndex == 0 ? "" : ddlTime.SelectedValue, null, resultPage, PageSize, false,Owner);
                DataSet ds = ls.DataResponse;
                DataTable dt = ds.Tables[0];
               
                Div1.Visible = true;
                gvExamSchedule.Visible = true;
                gvExamSchedule.DataSource = dt;
                gvExamSchedule.DataBind();
                table_Paging.Visible = true;
            }


        }

        protected void lnkExamNumber_Click(object sender, EventArgs e)
        {
            var gr = (GridViewRow)((LinkButton)sender).NamingContainer;
            var strExamNumber = (Label)gr.FindControl("ExamNumberNo");
            Session["ExTestingNo"] = strExamNumber.Text;
            testingNoInput.Text = strExamNumber.Text;
            FileUploadPanel.Attributes.Add("style", "visibility:visible");

            PnlImport.Visible = false;
            gvSubject.DataSource = null;
            gvSubject.DataBind();
            gvCheckList.DataSource = null;
            gvCheckList.DataBind();
        }

         
        protected void gvExamSchedule_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblTotal = ((Label)e.Row.FindControl("lblTotalApply"));
                Label lblExamAdmission = ((Label)e.Row.FindControl("lblExamAdmission"));
                Label lblLicenseTypeCodeNumber = ((Label)e.Row.FindControl("lblLicenseTypeCodeNumber"));
                Label lblExamDate = ((Label)e.Row.FindControl("lblExamDate"));
                Label lblExamRegister = ((Label)e.Row.FindControl("lblExamRegister"));
                LinkButton view = ((LinkButton)e.Row.FindControl("hplview"));
                LinkButton lnkExamRegister = ((LinkButton)e.Row.FindControl("lnkExamRegister"));

                lblExamAdmission.Text = lblTotal.Text == null ? "0" : lblExamAdmission.Text == null ? "0" : lblExamAdmission.Text;
                LinkButton lbnEditGv = (LinkButton)e.Row.FindControl("lnkExamNumber");
                LinkButton lb = e.Row.FindControl("lnkExamNumber") as LinkButton;

               
              
                    if (!string.IsNullOrEmpty(lblExamDate.Text))
                    {
                        DateTime dtExam = Convert.ToDateTime(lblExamDate.Text);
                        DateTime dtToday = DateTime.Today.AddDays(-1);
                        DateTime dtFinish = DateTime.Today.AddDays(+5);

                        //ใช้นะ
                        //if (dtExam.Date > DateTime.Now.Date)
                        //{
                        //    lblExamRegister.Visible = true;
                        //    lblExamRegister.ForeColor = System.Drawing.Color.Gray;
                        //    lblExamRegister.Font.Strikeout = true;
                        //}
                        //else
                        //{
                            lnkExamRegister.Visible = true;
                            lnkExamRegister.Enabled = true;
                        //}

                    }
                

                ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(lb);
            }
        }

        protected void btnSearchExam_Click(object sender, EventArgs e)
        {
            try
            {
                FileUploadPanel.Attributes.Add("style", "visibility:hidden");

                gvCheckList.DataSource = null;
                gvCheckList.DataBind();
                PnlImport.Visible = false;
                gvSubject.DataSource = null;
                gvSubject.DataBind();
                testingNoInput.Text = "";
                if (txtYear.Text.Trim().Length < 4)
                {
                    this.MasterSite.ModelError.ShowMessageError = Resources.errorSingleApplicant_002;
                    this.MasterSite.ModelError.ShowModalError();
                    UpdatePanelUpload.Update();
                    return;
                }
                else
                {
                    if (ddlMonth.SelectedIndex == 0)
                        ddlMonth.SelectedIndex = Convert.ToInt16(DateTime.Now.Month);

                    lsExRender.Clear();
                    IsCanExRender = true;
                    cldExam.SelectedDates.Clear();
                    txtNumberGvSearch.Text = "0"; //milk
                    ShowOrHide(true);//milk
                    gvExamSchedule.Visible = true;
                    gvExamSchedule.DataBind();
                   
                    SaveCriteriaToSession();
                    if (rblDisplay.SelectedValue == "1")
                    {

                        pnlCalendar.Visible = true;
                        table_Paging.Visible = false; //เป็นปฏิทินเลยซ่อนเพจจิ้ง
                       
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
                            cldExam.VisibleDate = cldExam.TodaysDate;

                            #region set background today
                            if (cldExam.TodaysDate.ToString("MMyyyy") == DateTime.Now.ToString("MMyyyy"))
                            {
                                cldExam.TodaysDate = new DateTime(strYear.ToInt(), strMonth.ToInt(), DateTime.Now.Day);
                                cldExam.VisibleDate = cldExam.TodaysDate;
                                cldExam.TodayDayStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#fbfba6");
                            }
                            else
                            {
                                cldExam.TodayDayStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#f6f6f6");
                            }
                            #endregion

                            SelectDataFrombase(cldExam.VisibleDate);
                            UpdatePanelUpload.Update();
                        }
                    }
                    else
                    {
                        pnlCalendar.Visible = false;
                        pnlTable.Visible = true;
                        Div1.Visible = true;
                        BindExamScheduleByTable(true);
                        UpdatePanelUpload.Update();
                        table_Paging.Visible = true;
                    }
                }
            }
            catch
            {

            }
        }

        private void SelectDataFrombase(DateTime visibleMonth)
        {
            BLL.ExamScheduleBiz biz = new BLL.ExamScheduleBiz();
            Func<string, string> GetCrit = anyString =>
            {
                return string.IsNullOrEmpty(anyString) ? string.Empty : anyString;
            };


            string strExamPlaceGroup = GetCrit(ddlPlaceGroup.SelectedValue);
            string strExamPlace = GetCrit(ddlPlace.SelectedIndex == 0 ? "" : ddlPlace.SelectedValue);
            string strLicenseType = GetCrit(ddlTypeLicense.SelectedValue == "" ? "" : ddlTypeLicense.SelectedValue);
            string strYearMonth = string.Empty;
            #region New

            string Owner = "";

            if (base.UserProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue())
                Owner = base.UserProfile.CompCode;

            strYearMonth = GetCrit(ConvertToYearMonth(visibleMonth.Month));
            string strCurrentYear = strYearMonth.Substring(0, 4);
            if (Convert.ToInt32(strCurrentYear) != visibleMonth.Year)
            {
                string strYear = visibleMonth.Year.ToString();
                string strMonth = visibleMonth.Month.ToString();
                if (visibleMonth.Month < 10)
                {
                    strMonth = "0" + strMonth;
                }
                strYearMonth = strYear + strMonth;
            }

            #endregion

            string strTime = GetCrit(ddlTime.SelectedIndex == 0 ? "" : ddlTime.SelectedValue);

            var ls = biz.GetExamMonthByCriteria(strExamPlaceGroup, strExamPlace, strLicenseType, strYearMonth, strTime, null,Owner);
            GBBiz gbBiz = new GBBiz();
            DTO.ResponseService<DTO.GBHoliday[]> lsHoliday = gbBiz.GetHolidayListByYearMonth(strYearMonth);

            DataSet ds = ls.DataResponse;


            Session["lsExDetailCalendar"] = null;
            Session["lsExHoliday"] = null;

            Session["strExExamPlaceGroup"] = null;
            Session["strExExamPlace"] = null;
            Session["strExLicenseType"] = null;
            Session["strExYearMonth"] = null;
            Session["strExTime"] = null;

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Session["lsExDetailCalendar"] = ds;

                    Session["strExExamPlaceGroup"] = strExamPlaceGroup;
                    Session["strExExamPlace"] = strExamPlace;
                    Session["strExLicenseType"] = strLicenseType;
                    Session["strExYearMonth"] = strYearMonth;
                    Session["strExTime"] = strTime;
                }
            }
            if (lsHoliday.DataResponse.ToList().Count > 0)
            {
                Session["lsExHoliday"] = lsHoliday;
            }

        }

        private string ConvertToYearMonth(int iMonth)
        {
            int iYear = (txtYear.Text.ToInt() > 2500) ? txtYear.Text.ToInt() - 543 : txtYear.Text.ToInt();
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

        private string ConvertToYearMonth(int tx, int iMonth)
        {
            int iYear = (tx > 2500) ? tx - 543 : tx;
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

        private void SaveCriteriaToSession()
        {
            BLL.ExamScheduleBiz biz = new BLL.ExamScheduleBiz();
            Func<string, string> GetCrit = anyString =>
            {
                return string.IsNullOrEmpty(anyString) ? string.Empty : anyString;
            };

            string strExamPlaceGroup = GetCrit(ddlPlaceGroup.SelectedValue);
            string strExamPlace = GetCrit(ddlPlace.SelectedIndex == 0 ? "" : ddlPlace.SelectedValue);
            string strLicenseType = GetCrit(ddlTypeLicense.SelectedValue == "" ? "" : ddlTypeLicense.SelectedValue);
            string strYearMonth = string.Empty;
            strYearMonth = GetCrit(ConvertToYearMonth(ddlMonth.SelectedValue.ToInt()));
            string strTime = GetCrit(ddlTime.SelectedIndex == 0 ? "" : ddlTime.SelectedValue);

            Session["strExExamPlaceGroup"] = strExamPlaceGroup;
            Session["strExExamPlace"] = strExamPlace;
            Session["strExLicenseType"] = strLicenseType;
            Session["strExYearMonth"] = strYearMonth;
            Session["strExTime"] = strTime;

        }

        protected void rblDisplay_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSearchExam_Click(sender, e);
            //CheckDisplayExam();
           
        }

        protected void ddlPlaceGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetExamPlace();
            if (rblDisplay.SelectedValue == "1")
            {
                IsCanExRender = false;
                table_Paging.Visible = false; //เป็นปฏิทิินเลยซ่อนการแบ่งเพจไว้
            }
            else if (rblDisplay.SelectedValue == "2")
            {
                lsExRender.Clear();

                IsCanExRender = true;
                pnlCalendar.Visible = false;
                pnlTable.Visible = true;
                BindExamScheduleByTable(true);
                table_Paging.Visible = true;
            }
        }

        private void BindExamScheduleByTable(Boolean CountAgain)
        {

            if (IsCanExRender == true)
            {

                #region page
                int Rpage = (txtNumberGvSearch.Text.Trim() == "") ? 0 : txtNumberGvSearch.Text.Trim().ToInt();
                int resultPage = (Rpage == 0) ? 1 : txtNumberGvSearch.Text.Trim().ToInt();
                resultPage = resultPage == 0 ? 1 : resultPage;
                if ((rowPerpage.Text.Trim() == null) || (rowPerpage.Text.Trim() == "") || (rowPerpage.Text.ToInt() == 0))
                {
                    rowPerpage.Text = PAGE_SIZE_Key.ToString();
                    PageSize = PAGE_SIZE_Key;
                }
                else
                {
                    PageSize = Convert.ToInt32(rowPerpage.Text);
                }
                #endregion page

                BLL.ExamScheduleBiz biz = new BLL.ExamScheduleBiz();
                Func<string, string> GetCrit = anyString =>
                {
                    return string.IsNullOrEmpty(anyString) ? string.Empty : anyString;
                };

                string strExamPlaceGroup = Session["strExExamPlaceGroup"].ToString();
                string strExamPlace = Session["strExExamPlace"].ToString();
                string strLicenseType = Session["strExLicenseType"].ToString();
                string strYearMonth = Session["strExYearMonth"].ToString();
                string strTime = Session["strExTime"].ToString();
                string Owner = "";

                if (base.UserProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue())
                    Owner = base.UserProfile.CompCode;

                if (CountAgain)
                {
                    #region Page
                    var CountPage = biz.GetExamByCriteriaDefault(
                                                    ddlPlaceGroup.SelectedValue, ddlPlace.SelectedIndex == 0 ? "" : ddlPlace.SelectedValue, 
                                                    ddlTypeLicense.SelectedValue == "" ? null : ddlTypeLicense.SelectedValue, 
                                                    base.UserProfile.AgentType == null ? "" : base.UserProfile.AgentType, strYearMonth, 
                                                    ddlTime.SelectedIndex == 0 ? "" : ddlTime.SelectedValue, null, 
                                                    resultPage, PageSize, true,Owner);


                    if (CountPage.DataResponse != null)
                        if (CountPage.DataResponse.Tables[0].Rows.Count > 0)
                        {
                            Int64 totalROWs = Convert.ToInt32(CountPage.DataResponse.Tables[0].Rows[0]["CCount"].ToString());

                            VisibleGV(gvExamSchedule, totalROWs, Convert.ToInt32(rowPerpage.Text), true);
                            if (Rpage == 0)
                                NPbutton(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "", txtTotalPage);

                            if (totalROWs == 0)
                                txtTotalPage.Text = "1";
                        }
                        else
                        {
                            VisibleGV(gvExamSchedule, 0, Convert.ToInt32(rowPerpage.Text), true);
                            if (Rpage == 0)
                                NPbutton(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "", txtTotalPage);
                            txtTotalPage.Text = "1";
                        }
                    #endregion Page
                }

                var ls = biz.GetExamByCriteriaDefault(strExamPlaceGroup, strExamPlace, strLicenseType,
                                                    base.UserProfile.AgentType == null ? "" : base.UserProfile.AgentType, strYearMonth,
                                                    strTime, null, resultPage, PageSize, false, Owner);

                DataSet ds = ls.DataResponse;
                DataTable dt = ds.Tables[0];

                Div1.Visible = true;
                gvExamSchedule.DataSource = dt;
                gvExamSchedule.DataBind();
               
            }


        }


        private void GetExamPlace()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetExamPlaceByCompCode(ddlPlaceGroup.SelectedIndex == 0 ? "" : ddlPlaceGroup.SelectedItem.Value, ddlPlaceGroup.SelectedItem.Value);
            BindToDDL(ddlPlace, ls.DataResponse);
            ddlPlace.Items.Insert(0, SysMessage.DefaultSelecting);
        }

      
        #endregion SingleApplicantCode

        //protected void btnCancleDetail_Click(object sender, EventArgs e)
        //{

        //    UpdatePanelUpload.Update();
        //    //blueDiv.Visible = false;
        //   //mpDetail.Hide();
        //    gvCheckList.DataSource = null;
        //    gvCheckList.DataBind();
        //}
    }
}
