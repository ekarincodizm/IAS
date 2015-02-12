using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.Utils;
using System.Threading;
using System.Globalization;
using System.Data;
using IAS.BLL;
using IAS.Properties;
using IAS.DTO;

namespace IAS.Applicant
{
    public partial class ApplicantDetail : basepage
    {
        int PageSize = 20;

        protected void Page_Load(object sender, EventArgs e)
        {
            txtEndExamDate.Attributes.Add("readonly", "true");
            txtStartExamDate.Attributes.Add("readonly", "true");

            txtEndCandidates.Attributes.Add("readonly", "true");
            txtStartCandidates.Attributes.Add("readonly", "true");

            txtBirthDay.Attributes.Add("readonly", "true");

            //ScriptManager.RegisterStartupScript(this, GetType(), "textreadonly", "setdate();", true);
            txtIdCard.Attributes.Add("onblur", "javascript:return checkUser(" + txtIdCard.ClientID + ");");

            txtIdCard.Attributes.Add("onkeydown", "javascript:return checkKeyNumberFixPoint(event, this, 13);");
            txtIdCard.Attributes.Add("onkeypress", "javascript:return checkKeyNumberKeyPressFixPoint(event, this, 13);");

            if (!Page.IsPostBack)
            {
                base.HasPermit();
                //PageSize = PAGE_SIZE_Key;
                UpdatePanelSearch.Update();
                //Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");
                txtStartExamDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                txtStartExamDate.Text = DateUtil.dd_MM_yyyy_Now_TH;
                txtEndExamDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                txtEndExamDate.Text = DateUtil.dd_MM_yyyy_Now_TH;

                txtStartCandidates.Text = DateTime.Today.ToString("dd/MM/yyyy");
                txtStartCandidates.Text = DateUtil.dd_MM_yyyy_Now_TH;
                txtEndCandidates.Text = DateTime.Today.ToString("dd/MM/yyyy");
                txtEndCandidates.Text = DateUtil.dd_MM_yyyy_Now_TH;

                bludDiv1.Visible = false;
                bludDiv2.Visible = false;
                txt_score.Visible = false;
                TXTrowperpage.Visible = false;
                txtNumberGvSearch.Text = "0";
                rowPerpage.Text = Convert.ToString(PageSize);
                if ((DTO.RegistrationType)base.UserProfile.MemberType == DTO.RegistrationType.General)
                {
                    UpdatePanelSearch.Update();
                    BindData(true);
                }
                else if (base.UserProfile.MemberType == DTO.RegistrationType.Insurance.GetEnumValue())
                {
                    GetExamPlaceGroup();
                }

                else if (base.UserProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue())
                {
                    GetExamPlaceGroup();
                }

                else if (base.UserProfile.MemberType == DTO.RegistrationType.TestCenter.GetEnumValue())
                {
                    GetExamPlaceGroupTestCenter();
                }

                else if (base.UserProfile.MemberType == DTO.RegistrationType.OIC.GetEnumValue() || base.UserProfile.MemberType == DTO.RegistrationType.OICAgent.GetEnumValue())
                {
                    GetExamPlaceGroupOIC();
                }

                BLL.DataCenterBiz bizCenter = new DataCenterBiz();
                var list = bizCenter.GetLicenseType("--เลือกประเภทใบอนุญาต--");
                ddlType.DataSource = list.DataResponse;
                ddlType.DataBind();

                var time = bizCenter.GetExamTime("--เลือกเวลาสอบ--");
                ddlTime.DataSource = time.DataResponse;
                ddlTime.DataBind();
            }
        }

        #region Pageing_milk
        protected void VisibleGV(GridView GVname, double total_row_count, double rows_per_page, Boolean visible_or_disvisible)
        {
            switch (GVname.ID.ToString())
            {
                case "gvDetail":
                    lblTotal.Text = Convert.ToString(total_row_count);
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
            UpdatePanelSearch.Update();
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
            BindData(false);
            Hide_show(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "", txtTotalPage.Text.ToInt());
            UpdatePanelSearch.Update();
        }

        protected void btnNextGvSearch_Click(object sender, EventArgs e)
        {
            NPbutton(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "N", txtTotalPage);
            BindData(false);
            Hide_show(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "", txtTotalPage.Text.ToInt());
            UpdatePanelSearch.Update();
        }
        #endregion Pageing_milk


        private Action<DropDownList, DTO.DataItem[]> BindToDDL = (ddl, ls) =>
        {
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Id";
            ddl.DataSource = ls;
            ddl.DataBind();
        };

        private Action<DropDownList, List<DTO.DataItem>> BindToDDLAr = (ddl, ls) =>
        {
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Id";
            ddl.DataSource = ls;
            ddl.DataBind();
        };

        public void BindData(Boolean Count)
        {
            lblExaminationCandidate.Visible = false;
            if ((DTO.RegistrationType)base.UserProfile.MemberType == DTO.RegistrationType.General)
            {
                UpdatePanelSearch.Update();
                PnlDetailSearchGridView.Visible = true;

                #region page
                int Rpage = (txtNumberGvSearch.Text.Trim() == "") ? 0 : txtNumberGvSearch.Text.Trim().ToInt();
                int resultPage = (Rpage == 0) ? 1 : txtNumberGvSearch.Text.Trim().ToInt();

                resultPage = resultPage == 0 ? 1 : resultPage;
                if ((rowPerpage.Text.Trim() == null) || (rowPerpage.Text.Trim() == "") || (rowPerpage.Text.ToInt() == 0))
                {
                    rowPerpage.Text = PageSize.ToString();
                }
                else
                {
                    PageSize = Convert.ToInt32(rowPerpage.Text);
                }
                #endregion page

                Panel Detailll = (Panel)this.PnlSearch;
                Detailll.Visible = false;




                var biz = new BLL.ApplicantBiz();
                if (Count)
                {
                    #region Page
                    //var CountPage = biz.GetApplicantByCriteria((DTO.RegistrationType)base.UserProfile.MemberType,
                    //    "", base.IdCard, "", "", "", Convert.ToDateTime(txtStartExamDate.Text),
                    //    Convert.ToDateTime(txtEndExamDate.Text), "", "", resultPage, PageSize, true, ddlType.SelectedValue, ddlTime.SelectedValue, ddlPlaceGroup.SelectedValue, ddlPlace.SelectedValue, txtChequeNo.Text, ddlExamResult.SelectedValue, Convert.ToDateTime(txtStartCandidates.Text), Convert.ToDateTime(txtEndCandidates.Text));

                    var CountPage = biz.GetApplicantByCriteria((DTO.RegistrationType)base.UserProfile.MemberType,
                          base.UserProfile.Id, base.IdCard, "", "", "", Convert.ToDateTime(txtStartExamDate.Text),
                          Convert.ToDateTime(txtEndExamDate.Text), "", "", resultPage, PageSize, true, ddlType.SelectedValue, ddlTime.SelectedValue, ddlPlaceGroup.SelectedValue, ddlPlace.SelectedValue == "เลือก" ? "" : ddlPlace.SelectedValue, txtChequeNo.Text, ddlExamResult.SelectedValue, Convert.ToDateTime(txtStartCandidates.Text), Convert.ToDateTime(txtEndCandidates.Text));



                    if (CountPage.DataResponse != null)
                        if (CountPage.DataResponse.Tables[0].Rows.Count > 0)
                        {
                            Int64 totalROWs = Convert.ToInt32(CountPage.DataResponse.Tables[0].Rows[0]["CCount"].ToString());

                            VisibleGV(gvDetail, totalROWs, Convert.ToInt32(rowPerpage.Text), true);
                            if (Rpage == 0)
                                NPbutton(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "", txtTotalPage);
                        }
                        else
                        {
                            VisibleGV(gvDetail, 0, Convert.ToInt32(rowPerpage.Text), true);
                            if (Rpage == 0)
                                NPbutton(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "", txtTotalPage);
                            txtTotalPage.Text = "1";
                        }
                    #endregion Page
                }
                var res = biz.GetApplicantByCriteria((DTO.RegistrationType)base.UserProfile.MemberType, base.UserProfile.Id, base.IdCard, "", "", "", Convert.ToDateTime(txtStartExamDate.Text), Convert.ToDateTime(txtEndExamDate.Text), "", "", resultPage, PageSize, false, ddlType.SelectedValue, ddlTime.SelectedValue, ddlPlaceGroup.SelectedValue, ddlPlace.SelectedValue == "เลือก" ? "" : ddlPlace.SelectedValue, txtChequeNo.Text, ddlExamResult.SelectedValue, Convert.ToDateTime(txtStartCandidates.Text), Convert.ToDateTime(txtEndCandidates.Text));

                if (res.IsError)
                {
                    btnNextGvSearch.Visible = false;
                    btnPreviousGvSearch.Visible = false;
                    txtNumberGvSearch.Visible = false;
                    txtTotalPage.Visible = false;
                    TXTrowperpage.Visible = false;
                    lblParaPage.Visible = false;
                    rowPerpage.Visible = false;
                    pageGo.Visible = false;
                    div_totalPage.Visible = false;
                    UCModalError.ShowMessageError = res.ErrorMsg;
                    UCModalError.ShowModalError();


                }
                else
                {
                    lblExaminationCandidate.Visible = false;
                    bludDiv1.Visible = true;
                    gvDetail.Visible = true;
                    div_totalPage.Visible = true;
                    if (res != null)
                    {
                        if (res.DataResponse.Tables[0].Rows.Count > 0)
                        {
                            txtNumberGvSearch.Enabled = false;
                            btnPreviousGvSearch.Enabled = true;
                            btnNextGvSearch.Enabled = true;

                            bludDiv2.Visible = false;
                            gvDetail.DataSource = res.DataResponse;
                            gvDetail.DataBind();
                            // UpdatePanelSearch.Update();


                        }
                        else
                        {
                            gvDetail.DataSource = res.DataResponse;
                            gvDetail.DataBind();

                            txtNumberGvSearch.Enabled = false;
                            btnPreviousGvSearch.Enabled = false;
                            btnNextGvSearch.Enabled = false;
                            bludDiv2.Visible = false;
                        }
                    }



                }

            }

            else
            {

                if (base.UserProfile.MemberType != (int)DTO.RegistrationType.General)
                {
                    bludDiv1.Visible = true;
                    gvDetail.Visible = true;
                    gvDetail.Visible = true;
                    PnlSearch.Visible = true;
                    int Rpage = (txtNumberGvSearch.Text.Trim() == "") ? 0 : txtNumberGvSearch.Text.Trim().ToInt();
                    int resultPage = (Rpage == 0) ? 1 : txtNumberGvSearch.Text.Trim().ToInt();
                    // resultPage = resultPage == 0 ? 1 : resultPage;
                    if ((rowPerpage.Text.Trim() == null) || (rowPerpage.Text.Trim() == "") || (rowPerpage.Text.Trim() == "0"))
                    {
                        rowPerpage.Text = PageSize.ToString();
                    }
                    else
                    {
                        PageSize = Convert.ToInt32(rowPerpage.Text);
                    }

                    PnlDetailSearchGridView.Visible = true;
                    // UpdatePanelSearch.Update();
                    var biz = new BLL.ApplicantBiz();
                    #region Page
                    var CountPage = biz.GetApplicantByCriteria((DTO.RegistrationType)base.UserProfile.MemberType,
                       base.UserProfile.CompCode,
                       txtIdCard.Text,
                       txtEaxmID.Text,
                       txtName.Text,
                       txtSurName.Text,
                       Convert.ToDateTime(txtStartExamDate.Text),
                       Convert.ToDateTime(txtEndExamDate.Text),
                       "",
                       txtBillNo.Text, resultPage, PageSize, true, ddlType.SelectedValue, ddlTime.SelectedValue, ddlPlaceGroup.SelectedValue, ddlPlace.SelectedValue == "เลือก" ? "" : ddlPlace.SelectedValue, txtChequeNo.Text, ddlExamResult.SelectedValue, Convert.ToDateTime(txtStartCandidates.Text), Convert.ToDateTime(txtEndCandidates.Text));


                    if (CountPage.DataResponse != null)
                        if (Convert.ToInt32(CountPage.DataResponse.Tables[0].Rows[0]["CCount"].ToString()) > 0)
                        {
                            Int64 totalROWs = Convert.ToInt32(CountPage.DataResponse.Tables[0].Rows[0]["CCount"].ToString());

                            VisibleGV(gvDetail, totalROWs, Convert.ToInt32(rowPerpage.Text), true);
                            if (Rpage == 0)
                                NPbutton(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "", txtTotalPage);
                        }
                        else
                        {
                            VisibleGV(gvDetail, 0, Convert.ToInt32(rowPerpage.Text), true);
                            if (Rpage == 0)
                                NPbutton(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "", txtTotalPage);
                            txtTotalPage.Text = "1";
                        }
                    #endregion Page
                    var res = biz.GetApplicantByCriteria((DTO.RegistrationType)base.UserProfile.MemberType,
                    base.UserProfile.CompCode,
                    txtIdCard.Text,
                    txtEaxmID.Text,
                    txtName.Text,
                    txtSurName.Text,
                    Convert.ToDateTime(txtStartExamDate.Text),
                    Convert.ToDateTime(txtEndExamDate.Text),
                    "",
                    txtBillNo.Text, resultPage, PageSize, false, ddlType.SelectedValue, ddlTime.SelectedValue, ddlPlaceGroup.SelectedValue, ddlPlace.SelectedValue == "เลือก" ? "" : ddlPlace.SelectedValue, txtChequeNo.Text, ddlExamResult.SelectedValue, Convert.ToDateTime(txtStartCandidates.Text), Convert.ToDateTime(txtEndCandidates.Text));
                    bludDiv1.Visible = true;
                    if (res.IsError)
                    {
                        UCModalError.ShowMessageError = res.ErrorMsg;
                        UCModalError.ShowModalError();
                    }
                    else
                    {

                        gvDetail.DataSource = res.DataResponse;
                        gvDetail.DataBind();

                        bludDiv2.Visible = false;
                        // UpdatePanelSearch.Update();


                    }

                }
            }
            //UpdatePanelSearch.Update();


        }

        private void GetExamPlaceGroup()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetExamPlaceGroup(SysMessage.DefaultSelecting);
            BindToDDL(ddlPlaceGroup, ls.DataResponse);
        }

        private void GetExamPlaceGroupOIC()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetExamPlaceGroup(SysMessage.DefaultSelecting);
            BindToDDL(ddlPlaceGroup, ls.DataResponse);
        }

        private void GetExamPlace()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetExamPlaceByCompCode(ddlPlaceGroup.SelectedIndex == 0 ? "" : ddlPlaceGroup.SelectedItem.Value, ddlPlaceGroup.SelectedItem.Value);
            BindToDDL(ddlPlace, ls.DataResponse);
            ddlPlace.Items.Insert(0, SysMessage.DefaultSelecting);
        }

        private void GetExamPlaceGroupTestCenter()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetExamPlaceGroupByCompCode(SysMessage.DefaultSelecting, base.UserProfile.CompCode);
            BindToDDL(ddlPlaceGroup, ls.DataResponse);
        }

        private void GetTitle()
        {
            var message = SysMessage.DefaultSelecting;
            DataCenterBiz biz = new DataCenterBiz();
            var ls = biz.GetTitleName(message);
            BindToDDLAr(ddlEditDetailTitle, ls);
        }

        private void GetEducation()
        {
            var message = SysMessage.DefaultSelecting;
            DataCenterBiz biz = new DataCenterBiz();
            var ls = biz.GetEducation(message);
            BindToDDLAr(ddlEducation, ls);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtStartExamDate.Text != null && txtEndExamDate.Text != null)
            {
                if (Convert.ToDateTime(txtStartExamDate.Text) > Convert.ToDateTime(txtEndExamDate.Text))
                {
                    UCModalError.ShowMessageError = Resources.errorApplicantDetail_StartExamDateMoreThanEndExamDate;
                    UCModalError.ShowModalError();
                    PnlDetailSearchGridView.Visible = false;
                    PnlDetail.Visible = false;
                }
                else
                {

                    txtNumberGvSearch.Text = "0";
                    UpdatePanelSearch.Update();
                    BindData(true);
                    if (gvDetail.Rows.Count > 0)
                    {
                        btnExportExcel.Visible = true;
                        if ((DTO.RegistrationType)base.UserProfile.MemberType != DTO.RegistrationType.General)
                            btnExportCSV.Visible = true;


                    }
                    else
                    {
                        btnExportExcel.Visible = false;
                        btnExportCSV.Visible = false;
                    }
                }
            }


        }

        //protected void gvDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    var a = base.UserProfile.MemberType;

        //    gvDetail.PageIndex = e.NewPageIndex;

        //    BindData();

        //    UpdatePanelSearch.Update();

        //}

        protected void hplView_Click(object sender, EventArgs e)
        {
            PnlDetail.Visible = true;
            UpdatePanelSearch.Update();
            var gr = (GridViewRow)((LinkButton)sender).NamingContainer;

            var applicantCode = (Label)gr.FindControl("lblAPPLICANTCODEGv");
            var examPlaceCode = (Label)gr.FindControl("lblEXAMPLACECODEGv");
            var testingNo = (Label)gr.FindControl("lblTestingNoGv");
            Label OrderGv = (Label)gr.FindControl("lblPaymentNoGv");
            BLL.ApplicantBiz biz = new BLL.ApplicantBiz();
            int resultPage = 1;//txtNumberGvSearch.Text.ToInt();
            UpdatePanelSearch.Update();


            var res = biz.GetApplicantInfo(applicantCode.Text, testingNo.Text, examPlaceCode.Text, resultPage, PageSize, false);

            if (!res.IsError)
            {
                txt_score.Visible = true;
                ClearControl();

                var app = res.DataResponse;

                if (!string.IsNullOrEmpty(app.TestingNo))
                {
                    txtDetailTitle.Text = app.Title;
                    txtDetailName.Text = app.FirstName;
                    txtDetailSurName.Text = app.LastName;
                    txtDetailIdNumber.Text = app.IdCard;
                    txtDetailExamDate.Text = app.TestingDate.ToString("dd/MM/yyyy");
                    txtCandidateDate.Text = (app.ApplyDate.ToString("dd/MM/yyyy") == "01/01/0544") ? "" : app.ApplyDate.ToString("dd/MM/yyyy");
                    txtDetailRegExamID.Text = app.ApplicantCode;
                    txtDetailExamNumber.Text = app.TestingNo;
                    txtDetailGroundExam.Text = app.ExamPlace;

                    if (!string.IsNullOrEmpty(app.PlaceGroupName))
                    {
                        txtDetailPlaceGroup.Text = app.PlaceGroupName;
                    }
                    if (string.IsNullOrEmpty(app.AssociationCode))
                    {
                        txtDetailPlaceGroup.Text = app.PlaceGroupName;
                    }
                    if (!string.IsNullOrEmpty(app.AssociationCode))
                    {
                        txtDetailPlaceGroup.Text = app.AssociationName;
                    }
                    txtInsuranceCompany.Text = app.InsuranceCompanyName;
                    txtDetailExamTime.Text = app.TestingTime;
                    txtNumberOrder.Text = OrderGv.Text;
                    txtDetailTotalExam.Text = app.ExamResult == null ? Resources.infoApplicantDetail_DetailTotalExam : app.ExamResult;

                    //chkSpecial.Checked = r1["Special"].ToString();
                    if (app.Special == "Y")
                    {
                        chkSpecial.Checked = true;
                    }
                    else
                    {
                        chkSpecial.Checked = false;
                    }
                    txtDetailExamLicenseType.Text = app.LicenseTypeName;
                    if (app.PaymentDate != Convert.ToDateTime("1/1/0544 0:00:00"))
                    {
                        txtDetailExamPaymentDate.Text = app.PaymentDate.ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        txtDetailExamPaymentDate.Text = "";
                    }
                    txtDetailExamBillNumber.Text = app.BillNumber;
                    txtDetailExamProvince.Text = app.Province;
                    txtDetailExamOwner.Text = app.ExamOwner;

                    gvSubject.DataSource = (app.Subjects != null && app.Subjects.Count > 0
                                                ? app.Subjects.ToList()
                                                : null);
                    gvSubject.DataBind();
                    //if (gvSubject.Rows.Count > 0)
                    //{
                    lDVGshow(true);
                    //}
                    //else
                    //{
                    //lDVGshow(false);

                    //}

                    lblExaminationCandidate.Visible = true;
                    bludDiv2.Visible = true;
                    DisEnabled();
                }
                else
                {
                    lblExaminationCandidate.Visible = false;
                    PnlDetail.Visible = false;
                }


            }
            else
            {
                UCModalError.ShowMessageError = res.ErrorMsg;
                UCModalError.ShowModalError();
            }
            UpdatePanelSearch.Update();
        }

        protected void hplCancel_Click(object sender, EventArgs e)
        {
            var gr = (GridViewRow)((LinkButton)sender).NamingContainer;
            var lblApplicantCode = (Label)gr.FindControl("lblApplicantCode");
            var lblExamPlaceCode = (Label)gr.FindControl("lblExamPlaceCode");
            var lblTestingNoGv = (Label)gr.FindControl("lblTestingNoGv");
            var lblIDCardNoGv = (Label)gr.FindControl("lblIDCardNoGv");

            var lblTestingDateGv = (Label)gr.FindControl("lblTestingDateGv");
            var lblTestTimeCode = (Label)gr.FindControl("lblTestTimeCode");
            var lblLicenseTypeCode = (Label)gr.FindControl("lblLicenseTypeCode");
            CancelApplicant(lblApplicantCode.Text, lblExamPlaceCode.Text, lblTestingNoGv.Text, lblIDCardNoGv.Text, lblTestingDateGv.Text, lblTestTimeCode.Text, lblLicenseTypeCode.Text);
            BindData(true);
        }

        private void CancelApplicant(string ApplicantCode, string ExamPlaceCode, string TestingNoGv, string IDCardNo, string TestingDate, string TestTimeCode, string LicenseTypeCode)
        {
            DTO.Applicant appl = new DTO.Applicant();
            appl.APPLICANT_CODE = (Convert.ToInt32(ApplicantCode));
            appl.EXAM_PLACE_CODE = (ExamPlaceCode);
            appl.TESTING_NO = (TestingNoGv);
            appl.RECORD_STATUS = "X";
            appl.CANCEL_REASON = Resources.propApplicantDetail_001;
            ExamLicense examl = new ExamLicense();
            examl.TESTING_NO = TestingNoGv;
            examl.LICENSE_TYPE_CODE = LicenseTypeCode;
            examl.EXAM_PLACE_CODE = ExamPlaceCode;
            examl.TESTING_DATE = (Convert.ToDateTime(TestingDate));
            examl.TEST_TIME_CODE = TestTimeCode;
            DataCenterBiz biz = new DataCenterBiz();
            var res = biz.UpdateCancelApplicant(appl, examl);
            if (res.IsError)
            {
                UCModalError.ShowMessageError = res.ErrorMsg;
                UCModalError.ShowModalError();
            }
            else
            {
                UCModalSuccess.ShowMessageSuccess = Resources.infoApplicantDetail_001;
                UCModalSuccess.ShowModalSuccess();
            }

        }

        public void lDVGshow(Boolean Sh)
        {
            txt_score.Visible = Sh;
            //txtNumberDGvSearch.Visible = Sh;
            //btnPreviousDGvSearch.Visible = Sh;
            //btnNextDGvSearch.Visible = Sh;
            gvSubject.Visible = Sh;
        }
        public void ClearControl()
        {
            txtDetailTitle.Text = string.Empty;
            txtDetailName.Text = string.Empty;
            txtDetailSurName.Text = string.Empty;
            txtDetailIdNumber.Text = string.Empty;
            txtDetailExamDate.Text = string.Empty;
            txtCandidateDate.Text = string.Empty;
            txtDetailRegExamID.Text = string.Empty;
            txtDetailExamNumber.Text = string.Empty;
            txtDetailGroundExam.Text = string.Empty;
            txtDetailPlaceGroup.Text = string.Empty;
            txtInsuranceCompany.Text = string.Empty;
            txtDetailTotalExam.Text = string.Empty;
            txtNumberOrder.Text = string.Empty;

            chkSpecial.Checked = false;
            txtDetailExamLicenseType.Text = string.Empty;
            txtDetailExamPaymentDate.Text = string.Empty;
            txtDetailExamBillNumber.Text = string.Empty;
            txtDetailExamProvince.Text = string.Empty;
            txtDetailExamOwner.Text = string.Empty;
        }

        public void DisEnabled()
        {
            txtDetailTitle.Enabled = false;
            txtDetailName.Enabled = false;
            txtDetailSurName.Enabled = false;
            txtDetailIdNumber.Enabled = false;
            txtDetailExamDate.Enabled = false;
            txtCandidateDate.Enabled = false;
            txtDetailRegExamID.Enabled = false;
            txtDetailExamNumber.Enabled = false;
            txtDetailGroundExam.Enabled = false;
            txtDetailPlaceGroup.Enabled = false;
            txtInsuranceCompany.Enabled = false;
            txtDetailTotalExam.Enabled = false;
            txtNumberOrder.Enabled = false;
            txtDetailExamTime.Enabled = false;

            chkSpecial.Enabled = false;
            txtDetailExamLicenseType.Enabled = false;
            txtDetailExamPaymentDate.Enabled = false;
            txtDetailExamBillNumber.Enabled = false;
            txtDetailExamProvince.Enabled = false;
            txtDetailExamOwner.Enabled = false;
        }

        protected void gvDetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label OrderGv = (Label)e.Row.FindControl("lblPaymentNoGv");
                LinkButton hplCancel = (LinkButton)e.Row.FindControl("hplCancel");
                LinkButton hplEdit = (LinkButton)e.Row.FindControl("hplEdit");
                Label PaymentDate = (Label)e.Row.FindControl("lblReceiptDateGv");
                Label ExpireDate = (Label)e.Row.FindControl("lblExpireDate");
                Label Status = (Label)e.Row.FindControl("lblStatus");
                Label CancelReason = (Label)e.Row.FindControl("lblCancelReason");
                if (Status.Text == "P")
                {
                    if ((PaymentDate.Text.Trim() != "") && (ExpireDate.Text.Trim() != ""))
                    {
                        if (Convert.ToDateTime(PaymentDate.Text) <= Convert.ToDateTime(ExpireDate.Text))
                            Status.Text = Resources.propApplicantDetail_002;

                        if (Convert.ToDateTime(PaymentDate.Text) > Convert.ToDateTime(ExpireDate.Text))
                            Status.Text = Resources.infoApplicantDetail_003;
                    }
                    else
                    {
                        string txtstatus = "";
                        if (PaymentDate.Text.Trim() == "")
                        {
                            txtstatus = "-พบข้อผิดพลาดเกี่ยวกับวันที่ชำระเงิน";

                        }

                        if (ExpireDate.Text.Trim() == "")
                        {
                            txtstatus = (txtstatus == "") ? "พบข้อผิดพลาดเกี่ยวกับวันที่หมดอายุใบสั่งจ่าย" : "<br/>-พบข้อผิดพลาดเกี่ยวกับวันที่หมดอายุใบสั่งจ่าย";
                        }

                        if (txtstatus != "")
                        {
                            Status.Text = txtstatus;
                            Status.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                }
                else if (Status.Text == "S")
                {
                    Status.Text = "ชำระไม่เต็มจำนวน";
                }
                //else if ((PaymentDate.Text == "") && (Convert.ToDateTime(ExpireDate.Text) < DateTime.Now))
                //{
                //    Status.Text = Resources.infoApplicantDetail_004;
                //}
                if ((OrderGv.Text != "") && (OrderGv.Text != null))
                {
                    OrderGv.Text = OrderGv.Text.Insert(6, " ").Insert(11, " ");
                }
                if (base.UserProfile.MemberType == DTO.RegistrationType.Insurance.GetEnumValue() || base.UserProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue() || base.UserProfile.MemberType == DTO.RegistrationType.OIC.GetEnumValue() || base.UserProfile.MemberType == DTO.RegistrationType.OICAgent.GetEnumValue())
                {
                    if (string.IsNullOrEmpty(OrderGv.Text) && string.IsNullOrEmpty(CancelReason.Text))
                    {
                        hplCancel.Visible = true;
                        hplEdit.Visible = true;
                    }
                    else
                    {
                        hplCancel.Visible = false;
                        hplEdit.Visible = false;
                    }

                }
                else
                {
                    gvDetail.Columns[20].Visible = false;
                    hplCancel.Visible = false;
                    hplEdit.Visible = false;
                }

            }
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            ClearCondition();
        }
        protected void ClearCondition()
        {
            txtStartExamDate.Text = string.Empty;
            txtEndExamDate.Text = string.Empty;
            txtIdCard.Text = string.Empty;
            txtName.Text = string.Empty;
            txtSurName.Text = string.Empty;
            txtEaxmID.Text = string.Empty;
            //txtPaymentNo.Text = string.Empty;
            txtBillNo.Text = string.Empty;
            //gvDetail.DataMember = null;
            //gvDetail.DataBind();
            gvDetail.Visible = false;
            btnPreviousGvSearch.Visible = false;
            btnNextGvSearch.Visible = false;
            txtNumberGvSearch.Text = "0";
            txtNumberGvSearch.Visible = true;
            txtTotalPage.Visible = false;
            lblExaminationCandidate.Visible = false;
            rowPerpage.Text = PageSize.ToString();
            ClearControl();
            txt_score.Visible = false;
            //gvSubject.DataMember = null;
            //gvSubject.DataBind();
            gvSubject.Visible = false;
            lblTotal.Visible = false;
            txtStartExamDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
            txtStartExamDate.Text = DateUtil.dd_MM_yyyy_Now_TH;
            txtEndExamDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
            txtEndExamDate.Text = DateUtil.dd_MM_yyyy_Now_TH;
            lblExaminationCandidate.Visible = false;
            bludDiv1.Visible = false;
            bludDiv2.Visible = false;
            txt_score.Visible = false;
            btnExportExcel.Visible = false;
            btnExportCSV.Visible = false;
        }

        protected void btnShowModalPopUpEmail(object sender, EventArgs e)
        {
            if (base.UserProfile.MemberType == MemberType.Insurance.GetEnumValue())
            {
                ModalPopupEmail.Show();
            }
            else
            {
                btnExportExcel_Click(sender, e);
            }
        }
        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {

                Int32 total = lblTotal.Text == "" ? 0 : lblTotal.Text.ToInt();
                var biz = new BLL.ApplicantBiz();
                if (total > base.EXCEL_SIZE_Key)
                {
                    UCModalError.ShowMessageError = SysMessage.ExcelSizeError;
                    UCModalError.ShowModalError();
                    UpdatePanelSearch.Update();
                }
                else
                {
                    Dictionary<string, string> columns = new Dictionary<string, string>();
                    columns.Add("ลำดับ", "RUN_NO");
                    columns.Add("เลขบัตรประชาชน", "ID_CARD_NO");
                    columns.Add("ชื่อ", "FIRSTNAME");
                    columns.Add("นามสกุล", "LASTNAME");
                    columns.Add("รหัสสอบ", "TESTING_NO");
                    columns.Add("วันที่สอบ", "TESTING_DATE");
                    columns.Add("เวลาสอบ", "TEST_TIME");
                    columns.Add("ผลการสอบ", "RESULT");
                    columns.Add("ใบสั่งจ่าย", "PAYMENT_NO");
                    //columns.Add("วันที่ชำระเงิน", "RECEIPT_DATE");
                    columns.Add("ใบเสร็จรับเงิน", "BILLNO");



                    List<HeaderExcel> header = new List<HeaderExcel>();
                    header.Add(new HeaderExcel
                    {
                        NameColumnsOne = "วันที่สมัครสอบ(เริ่ม) ",
                        ValueColumnsOne = txtStartCandidates.Text,
                        NameColumnsTwo = "วันที่สมัครสอบ(สิ้นสุด) ",
                        ValueColumnsTwo = txtEndCandidates.Text
                    });

                    header.Add(new HeaderExcel
                    {
                        NameColumnsOne = "วันที่สอบ(เริ่ม) ",
                        ValueColumnsOne = txtStartExamDate.Text,
                        NameColumnsTwo = "วันที่สอบ(สิ้นสุด) ",
                        ValueColumnsTwo = txtEndExamDate.Text
                    });

                    header.Add(new HeaderExcel
                    {
                        NameColumnsOne = "เวลาสอบ ",
                        ValueColumnsOne = ddlTime.SelectedItem.Text,
                        NameColumnsTwo = "เลขบัตรประชาชน ",
                        ValueColumnsTwo = txtIdCard.Text
                    });

                    header.Add(new HeaderExcel
                    {
                        NameColumnsOne = "ชื่อ ",
                        ValueColumnsOne = txtName.Text,
                        NameColumnsTwo = "นามสกุล ",
                        ValueColumnsTwo = txtSurName.Text
                    });

                    header.Add(new HeaderExcel
                    {
                        NameColumnsOne = "รหัสสอบ ",
                        ValueColumnsOne = txtEaxmID.Text,
                        NameColumnsTwo = "ประเภทใบอนุญาต ",
                        ValueColumnsTwo = ddlType.SelectedItem.Text
                    });

                    header.Add(new HeaderExcel
                    {
                        NameColumnsOne = "ใบสั่งจ่ายเลขที่ ",
                        ValueColumnsOne = txtChequeNo.Text,
                        NameColumnsTwo = "ใบเสร็จเลขที่ ",
                        ValueColumnsTwo = txtBillNo.Text
                    });

                    header.Add(new HeaderExcel
                    {
                        NameColumnsOne = "ผลสอบ ",
                        ValueColumnsOne = ddlExamResult.SelectedItem.Text,
                        NameColumnsTwo = "หน่วยงานที่จัดสอบ ",
                        ValueColumnsTwo = ddlPlaceGroup.SelectedItem.Text
                    });

                    try
                    {

                        if (ddlPlace.SelectedItem == null)
                        {
                               header.Add(new HeaderExcel
                        {
                            NameColumnsOne = "สนามสอบ ",
                            ValueColumnsOne = ""
                        });
                        }
                        else
                        {

          
                        header.Add(new HeaderExcel
                        {
                            NameColumnsOne = "สนามสอบ ",
                            ValueColumnsOne = ddlPlace.SelectedItem.Text
                        });

                        }

                    }
                    catch { }

                    ExportBiz export = new ExportBiz();
                    if ((DTO.RegistrationType)base.UserProfile.MemberType == DTO.RegistrationType.General)
                    {
                        var res = biz.GetApplicantByCriteria((DTO.RegistrationType)base.UserProfile.MemberType, "", base.IdCard, "", "", "", Convert.ToDateTime(txtStartExamDate.Text), Convert.ToDateTime(txtEndExamDate.Text), "", "", 1, base.EXCEL_SIZE_Key, false, ddlType.SelectedValue, ddlTime.SelectedValue, ddlPlaceGroup.SelectedValue == "เลือก" ? "" : ddlPlaceGroup.SelectedValue, ddlPlace.SelectedValue == "เลือก" ? "" : ddlPlace.SelectedValue, txtChequeNo.Text, ddlExamResult.SelectedValue, Convert.ToDateTime(txtStartCandidates.Text), Convert.ToDateTime(txtEndCandidates.Text));
                        export.CreateExcel(res.DataResponse.Tables[0], columns, header, base.UserProfile);
                    }
                    else
                    {

                        // export.CreateExcel(res.DataResponse.Tables[0], columns);


                        if (base.UserProfile.MemberType == MemberType.Insurance.GetEnumValue())
                        {
                            if (txtEmail.Text == "")
                            {
                                UCModalError.ShowMessageError = "กรุณาป้อน Email";
                                UCModalError.ShowModalError();
                                ModalPopupEmail.Show();
                                return;
                            }
                            var res = biz.GetApplicantByCriteriaSendMail((DTO.RegistrationType)base.UserProfile.MemberType,
                                    base.UserProfile.CompCode,
                                    txtIdCard.Text,
                                    txtEaxmID.Text,
                                    txtName.Text,
                                    txtSurName.Text,
                                    Convert.ToDateTime(txtStartExamDate.Text),
                                    Convert.ToDateTime(txtEndExamDate.Text),
                                    "",
                                    txtBillNo.Text, 1, base.EXCEL_SIZE_Key, false, ddlType.SelectedValue, ddlTime.SelectedValue, ddlPlaceGroup.SelectedValue == "เลือก" ? "" : ddlPlaceGroup.SelectedValue, ddlPlace.SelectedValue == "เลือก" ? "" : ddlPlace.SelectedValue, txtChequeNo.Text, ddlExamResult.SelectedValue, Convert.ToDateTime(txtStartCandidates.Text), Convert.ToDateTime(txtEndCandidates.Text), txtEmail.Text, base.UserNameLastName, UserProfile.LoginName);
                            txtEmail.Text = "";
                            ModalPopupEmail.Hide();
                            UCModalSuccess.ShowMessageSuccess = "ส่งไฟล์สำเร็จ";
                            UCModalSuccess.ShowModalSuccess();
                        }
                        else
                        {
                            var res = biz.GetApplicantByCriteria((DTO.RegistrationType)base.UserProfile.MemberType,
                                  base.UserProfile.CompCode,
                                  txtIdCard.Text,
                                  txtEaxmID.Text,
                                  txtName.Text,
                                  txtSurName.Text,
                                  Convert.ToDateTime(txtStartExamDate.Text),
                                  Convert.ToDateTime(txtEndExamDate.Text),
                                  "",
                                  txtBillNo.Text, 1, base.EXCEL_SIZE_Key, false, ddlType.SelectedValue, ddlTime.SelectedValue, ddlPlaceGroup.SelectedValue == "เลือก" ? "" : ddlPlaceGroup.SelectedValue, ddlPlace.SelectedValue == "เลือก" ? "" : ddlPlace.SelectedValue, txtChequeNo.Text, ddlExamResult.SelectedValue, Convert.ToDateTime(txtStartCandidates.Text), Convert.ToDateTime(txtEndCandidates.Text));
                            export.CreateExcel(res.DataResponse.Tables[0], columns, header, base.UserProfile);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                ModalPopupEmail.Hide();
                txtEmail.Text = "";
                UCModalError.ShowMessageError = "ไม่สามารถส่งไฟล์ได้";
                UCModalError.ShowModalError();
            }

        }


        protected void btnExportCSV_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 total = lblTotal.Text == "" ? 0 : lblTotal.Text.ToInt();
                if (total > base.EXCEL_SIZE_Key)
                {
                    UCModalError.ShowMessageError = SysMessage.ExcelSizeError;
                    UCModalError.ShowModalError();
                    UpdatePanelSearch.Update();
                }
                else
                {
                    ExportBiz export = new ExportBiz();
                    var biz = new BLL.ApplicantBiz();
                    if ((DTO.RegistrationType)base.UserProfile.MemberType == DTO.RegistrationType.General)
                    {
                        var res = biz.GetApplicantByCriteria((DTO.RegistrationType)base.UserProfile.MemberType, "", base.IdCard, "", "", "", Convert.ToDateTime(txtStartExamDate.Text), Convert.ToDateTime(txtEndExamDate.Text), "", "", 1, base.EXCEL_SIZE_Key, false, ddlType.SelectedValue, ddlTime.SelectedValue, ddlPlaceGroup.SelectedValue == "เลือก" ? "" : ddlPlaceGroup.SelectedValue, ddlPlace.SelectedValue == "เลือก" ? "" : ddlPlace.SelectedValue, txtChequeNo.Text, ddlExamResult.SelectedValue, Convert.ToDateTime(txtStartCandidates.Text), Convert.ToDateTime(txtEndCandidates.Text));
                        export.CreateApplcantCSV(res.DataResponse.Tables[0]);
                    }
                    else
                    {
                        var res = biz.GetApplicantByCriteria((DTO.RegistrationType)base.UserProfile.MemberType,
                                    base.UserProfile.CompCode,
                                    txtIdCard.Text,
                                    txtEaxmID.Text,
                                    txtName.Text,
                                    txtSurName.Text,
                                    Convert.ToDateTime(txtStartExamDate.Text),
                                    Convert.ToDateTime(txtEndExamDate.Text),
                                    "",
                                    txtBillNo.Text, 1, base.EXCEL_SIZE_Key, false, ddlType.SelectedValue, ddlTime.SelectedValue, ddlPlaceGroup.SelectedValue == "เลือก" ? "" : ddlPlaceGroup.SelectedValue, ddlPlace.SelectedValue == "เลือก" ? "" : ddlPlace.SelectedValue, txtChequeNo.Text, ddlExamResult.SelectedValue, Convert.ToDateTime(txtStartCandidates.Text), Convert.ToDateTime(txtEndCandidates.Text));
                        export.CreateApplcantCSV(res.DataResponse.Tables[0]);
                        
                    }
                }
            }
            catch { }
        }

        protected void btnCancelMail_Click(object sender, EventArgs e)
        {
            txtEmail.Text = "";
            ModalPopupEmail.Hide();
        }

        protected void hplEdit_Click(object sender, System.EventArgs e)
        {
            ModEditDetail.Show();
            PnlDetail.Visible = true;
            UplEditDetail.Update();
            GridViewRow gridViewRow = (GridViewRow)((Control)sender).NamingContainer;
            Label lblAPPLICANTCODEGv = (Label)gridViewRow.FindControl("lblAPPLICANTCODEGv");
            Label lblTestingNoGv = (Label)gridViewRow.FindControl("lblTestingNoGv");
            Label lblExamPlaceCode = (Label)gridViewRow.FindControl("lblExamPlaceCode");
            Session["ApplicantCode"] = lblAPPLICANTCODEGv.Text;
            Session["TestingNoGv"] = lblTestingNoGv.Text;
            Session["ExamPlaceCode"] = lblExamPlaceCode.Text;
            GetTitle();
            GetEducation();
            var res = new ApplicantBiz().GetApplicantDetail(Convert.ToInt32(lblAPPLICANTCODEGv.Text), lblTestingNoGv.Text, lblExamPlaceCode.Text);
            if (res.IsError)
            {
                UCModalError.ShowMessageError = res.ErrorMsg;
                UCModalError.ShowModalError();
            }
            else
            {
                txtDetailTestingNO.Text = res.DataResponse.TESTING_NO;
                txtEditDetailIDCard.Text = res.DataResponse.ID_CARD_NO;
                ddlEditDetailTitle.SelectedValue = res.DataResponse.PRE_NAME_CODE;
                txtFirstNane.Text = res.DataResponse.NAMES;
                txtLastName.Text = res.DataResponse.LASTNAME;
                txtBirthDay.Text = string.Format("{0:dd/MM/yyyy}", res.DataResponse.BIRTH_DATE);
                rblSex.SelectedValue = res.DataResponse.SEX;
                ddlEducation.SelectedValue = res.DataResponse.EDUCATION_CODE;
                txtAddress.Text = res.DataResponse.ADDRESS1;
                txtAreaCode.Text = res.DataResponse.AREA_CODE;
            }
        }

        protected void btnSubmitEditDatail_Click(object sender, System.EventArgs e)
        {

            if (this.EditDetailValidate())
            {
                ApplicantBiz biz = new ApplicantBiz();
                DTO.Applicant appl = new DTO.Applicant();
                appl.APPLICANT_CODE = Convert.ToInt32(base.Session["ApplicantCode"]);
                appl.TESTING_NO = Convert.ToString(base.Session["TestingNoGv"]);
                appl.EXAM_PLACE_CODE = Convert.ToString(base.Session["ExamPlaceCode"]);
                appl.ID_CARD_NO = txtEditDetailIDCard.Text;
                appl.PRE_NAME_CODE = ddlEditDetailTitle.SelectedValue;
                appl.NAMES = txtFirstNane.Text.Trim();
                appl.LASTNAME = txtLastName.Text;
                appl.BIRTH_DATE = Convert.ToDateTime(txtBirthDay.Text);
                appl.SEX = rblSex.SelectedValue;
                appl.EDUCATION_CODE = ddlEducation.SelectedValue;
                appl.ADDRESS1 = txtAddress.Text;
                appl.AREA_CODE = txtAreaCode.Text.Trim();
                var res = biz.Update(appl);
                if (res.IsError)
                {
                    UCModalError.ShowMessageError = res.ErrorMsg;
                    UCModalError.ShowModalError();
                }
                else
                {
                    UpdatePanelSearch.Update();
                    BindData(true);
                    UCModalSuccess.ShowMessageSuccess = "แก้ไขเรียบร้อยแล้ว";
                    UCModalSuccess.ShowModalSuccess();
                }
            }

        }

        private bool EditDetailValidate()
        {
            if (string.IsNullOrEmpty(txtEditDetailIDCard.Text))
                return false;
            if (string.IsNullOrEmpty(ddlEditDetailTitle.SelectedValue))
                return false;
            if (string.IsNullOrEmpty(txtFirstNane.Text))
                return false;
            if (string.IsNullOrEmpty(txtLastName.Text))
                return false;
            if (string.IsNullOrEmpty(txtBirthDay.Text))
                return false;
            if (string.IsNullOrEmpty(rblSex.SelectedValue))
                return false;
            if (string.IsNullOrEmpty(ddlEducation.SelectedValue))
                return false;

            return true;
        }

        protected void ddlEditDetailTitle_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sex = GetTitleName.GetSex(ddlEditDetailTitle.SelectedItem.Text);
            if (!string.IsNullOrEmpty(sex))
            {
                if (sex.Equals("M"))
                    rblSex.SelectedValue = "M";
                if (sex.Equals("F"))
                    rblSex.SelectedValue = "F";
            }
            else
            {
                rblSex.SelectedValue = null;
                rblSex.Enabled = true;
            }

            UplEditDetail.Update();
            ModEditDetail.Show();
        }




        protected void ddlPlaceGroup_SelectedIndexChanged(object sender, EventArgs e)
        {

            GetExamPlace();
        }

        protected void btnCancelEditDatail_Click(object sender, EventArgs e)
        {
            ModEditDetail.Hide();
        }


    }
}

