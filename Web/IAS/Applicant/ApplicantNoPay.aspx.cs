using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.Utils;
using System.Data;
using System.Globalization;
using System.Threading;
using IAS.BLL;
using IAS.Properties;

namespace IAS.Applicant
{
    public partial class ApplicantNoPay : basepage
    {
        int PageSize=20 ;
        string NOW_REQUEST = string.Empty;
        
        public List<DTO.CancelApplicant> CancelApplicant
        {
            get
            {
                if (Session["CancelApplicant"] == null)
                {
                    Session["CancelApplicant"] = new List<DTO.CancelApplicant>();
                }

                return (List<DTO.CancelApplicant>)Session["CancelApplicant"];
            }

            set
            {
                Session["CancelApplicant"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            DataCenterBiz biz = new DataCenterBiz();
          
            txtStartPaidSubDate.Attributes.Add("readonly", "true");
            txtEndPaidSubDate.Attributes.Add("readonly", "true");
            txtTestingDate.Attributes.Add("readonly", "true");
            if (!Page.IsPostBack)
            {
                ListAppNoPay = new List<DTO.AppNoPay>();
                // PageSize = PAGE_SIZE_Key;
                base.HasPermit();
                PnlDetail.Visible = true;
                btnDelete.Visible = false;
                txtNumberGvSearch.Text = "0";
                txt_Page_now.Text = "0";
                txtTotalPage.Text = "0";
                lbltotalP.Text = "0";
                rowPerpage.Text = Convert.ToString(PageSize);
                txt_input.Text = Convert.ToString(PageSize);
                //BindDataInGridview();
                OBJ_page1(false);
                OBJ_page2(false);
                PnlSearch.Visible = true;
                getConfigDate();

                Set_textDate(0);
               
            }


        }

        private void getConfigDate()
        {
            try
            {   var biz = new BLL.ExamScheduleBiz();
                var res = biz.ManageApplicantIn_OutRoom().DataResponse;
                var DayValue = res.FirstOrDefault(x => x.Id == "10" && x.GROUP_CODE == "AP001");
                lblDayValue.Text = DayValue.Value.ToString();
            }
            catch
            { 
            }
        }

      
        protected void Set_textDate(int Teextbox)
        {
            int DayValue = lblDayValue.Text == "" ? 0 : lblDayValue.Text.ToInt();
            Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");
            switch (Teextbox)
            { 
                case 1:
                    txtStartPaidSubDate.Text = DateTime.Today.AddDays(-DayValue).ToString("dd/MM/yyyy");
                    break;
                case 2:
                    txtEndPaidSubDate.Text = DateTime.Today.AddDays(-DayValue).ToString("dd/MM/yyyy");
                    break;
                default:
                    txtStartPaidSubDate.Text = DateTime.Today.AddDays(-DayValue).ToString("dd/MM/yyyy");
                    txtEndPaidSubDate.Text = DateTime.Today.AddDays(-DayValue).ToString("dd/MM/yyyy");
                    break;
            }
           
        }
        protected void OBJ_page1(Boolean show_not)
        {
            btnPreviousGvSearch.Visible = show_not;
            txtNumberGvSearch.Visible = show_not;
            lblParaPage.Visible = show_not;
            txtTotalPage.Visible = show_not;
            btnNextGvSearch.Visible = show_not;
            TXTrowperpage.Visible = show_not;
            rowPerpage.Visible = show_not;
            pageGo.Visible = show_not;
            lblTotal.Visible = show_not;
            panel_count.Visible = show_not;
        }
        protected void OBJ_page2(Boolean show_not)
        {
            btn_GO.Visible = show_not;
            btn_N.Visible = show_not;
            btn_P.Visible = show_not;
            txt_input.Visible = show_not;
            txt_Page_now.Visible = show_not;
            lblPer.Visible = show_not;
            lbltotalP.Visible = show_not;
            panel_count.Visible = show_not;
            lblThai.Visible = show_not;
            lblRec.Visible = show_not;
            panel_count2.Visible = show_not;
        }
        protected void hplView_Click(object sender, EventArgs e)
        {
            var gr = (GridViewRow)((LinkButton)sender).NamingContainer;

            var TestingNo = (Label)gr.FindControl("lblTestingNoGv");
            var ApplicantCode = (Label)gr.FindControl("lblApplicantCodeGv");
            var ExamPlaceCode = (Label)gr.FindControl("lblExamPlaceCodeGv");

            var biz = new BLL.PaymentBiz();
            var res = biz.GetApplicantNoPayById(ApplicantCode.Text, TestingNo.Text, ExamPlaceCode.Text);


            if (res.IsError)
            {
                var errorMsg = res.ErrorMsg;

                UCModalError.ShowMessageError = res.ErrorMsg;
                UCModalError.ShowModalError();
            }
            else
            {
                PnlDetail.Visible = true;
                DataSet app = res.DataResponse;

                DataTable dt = app.Tables[0];

                DataRow dr = dt.Rows[0];

                DTO.ApplicantNoPay anp = dr.MapToEntity<DTO.ApplicantNoPay>();

                txtDetailTitle.Text = anp.TITLE_NAME;
                txtDetailName.Text = anp.NAMES;
                txtDetailSurName.Text = anp.LASTNAME;
                txtDetailIdNumber.Text = anp.ID_CARD_NO;
                rblDetailSex.SelectedValue = anp.SEX;

                if (anp.BIRTH_DATE != null)
                {
                    txtDetailBirthDay.Text = anp.BIRTH_DATE.Value.ToString("dd/MM/yyyy");
                }

                txtDetailEducation.Text = anp.EDUCATION_NAME;
                txtDetailExamDate.Text = anp.TESTING_DATE.ToString("dd/MM/yyyy");
                txtDetailRegExamID.Text = anp.APPLICANT_CODE.ToString();
                txtDetailExamNumber.Text = anp.TESTING_NO;

                lblDetailCandidateNoPay.Visible = true;
                detaiLL.Visible = true;
                var bizsub = new BLL.DataCenterBiz();

                if (anp.EXAM_PLACE_CODE != null)
                {
                    var resexam = bizsub.GetPlaceExamNameById(anp.EXAM_PLACE_CODE);

                    if (resexam.IsError)
                    {
                        var errorMsg = resexam.ErrorMsg;

                        UCModalError.ShowMessageError = resexam.ErrorMsg;
                        UCModalError.ShowModalError();
                    }
                    else
                    {
                        txtDetailPlaceExam.Text = resexam.DataResponse;
                    }
                }
                else
                {
                    txtDetailPlaceExam.Text = "";
                }

                if (anp.ACCEPT_OFF_CODE != null)
                {
                    var resacceptoff = bizsub.GetAcceptOffNameById(anp.ACCEPT_OFF_CODE);

                    if (resacceptoff.IsError)
                    {
                        var errorMsg = resacceptoff.ErrorMsg;

                        UCModalError.ShowMessageError = resacceptoff.ErrorMsg;
                        UCModalError.ShowModalError();
                    }
                    else
                    {
                        txtDetailAssociationExamID.Text = resacceptoff.DataResponse;
                    }
                }
                else
                {
                    txtDetailAssociationExamID.Text = "";
                }

                if (anp.INSUR_COMP_CODE != null)
                {
                    var resCompany = bizsub.GetCompanyNameByIdToText(anp.INSUR_COMP_CODE);

                    if (resCompany.IsError)
                    {
                        var errorMsg = resCompany.ErrorMsg;

                        UCModalError.ShowMessageError = resCompany.ErrorMsg;
                        UCModalError.ShowModalError();
                    }
                    else
                    {
                        txtInsuranceCompany.Text = resCompany.DataResponse;
                    }
                }
                else
                {
                    txtInsuranceCompany.Text = "";
                }

                lblDetailCandidateNoPay.Enabled = false;
                detaiLL.Visible = true;
               // PnlDetail.Enabled = false;

                UpdatePanelGv.Update();
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (ListAppNoPay != null)
            {
                var biz = new BLL.PaymentBiz();
                var res = biz.CancelApplicantsHeader(ListAppNoPay.ToArray());

                if (res.IsError)
                {
                    var errorMsg = res.ErrorMsg;

                    UCModalError.ShowMessageError = res.ErrorMsg;
                    UCModalError.ShowModalError();
                }
                else
                {
                    UCModalSuccess.ShowMessageSuccess = "ยกเลิกรายการ " + ListAppNoPay.Count + " รายการเรียบร้อยแล้ว";
                    UCModalSuccess.ShowModalSuccess();
                    btnDelete.Visible = true;
                    ClearData();
                    btnSearch_Click(sender, e);
                   // BindDataInGridview(true);
                    NPbutton(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "", txtTotalPage);
                    PnlDetail.Visible = true;
                    UpdatePanelGv.Update();
                    ListAppNoPay = new List<DTO.AppNoPay>();
                    if (gvSearch.Rows.Count == 0)
                        btnExportExcel.Visible = false;
                }

            }
            else
            {
                UCModalError.ShowMessageError = Resources.errorApplicantNoPay_003;
                UCModalError.ShowModalError();
            }

               
           
        }
        protected void chkSelectApplicantNoPay_CheckedChanged(object sender, EventArgs e)
        {
            btnDelete.Enabled = true;

            var data = new List<DTO.CancelApplicant>();

            if (gvApplicantNoPay.Visible == true)
            {
                foreach (GridViewRow gr in gvApplicantNoPay.Rows)
                {
                    if (((CheckBox)gr.FindControl("chkSelectApplicantNoPay")).Checked == true)
                    {
                        var lblApplicantCodeGv = (Label)gr.FindControl("lblApplicantCodeGv");
                        var lblTestingNoGv = (Label)gr.FindControl("lblTestingNoGv");
                        var lblExamPlaceCodeGv = (Label)gr.FindControl("lblExamPlaceCodeGv");


                        data.Add(new DTO.CancelApplicant
                        {
                            ApplicantCode = Convert.ToInt32(lblApplicantCodeGv.Text),
                            TestingNo = lblTestingNoGv.Text,
                            ExamPlaceCode = lblExamPlaceCodeGv.Text
                        });
                    }
                }

                Session["CancelApplicant"] = data;
            }

            UpdatePanelGv.Update();
        }

        #region Pageing_milk
        protected void VisibleGV(GridView GVname, double total_row_count, double rows_per_page, Boolean visible_or_disvisible)
        {
            switch (GVname.ID.ToString())
            {
                case "gvSearch":
                    lblTotal.Text = Convert.ToString(total_row_count);
                    rows_per_page = (rows_per_page == 0 ) ? 1 : rows_per_page;
                    double Paggge = Math.Ceiling(total_row_count / rows_per_page);
                    txtTotalPage.Text = (total_row_count > 0) ? Convert.ToString(Paggge) : "0";
                    OBJ_page1(visible_or_disvisible);
                    //if (total_row_count.ToInt() == 0)
                    //{
                    //    btnPreviousGvSearch.Visible = false;
                    //    btnNextGvSearch.Visible = false;
                    //}
                    //if(txtNumberGvSearch.Text=="1")
                    //{
                    //    btnPreviousGvSearch.Visible = false;
                    //}
                    //if (txtNumberGvSearch.Text == txtTotalPage.Text)
                    //{
                    //    btnNextGvSearch.Visible = false;
                    //}
                    break;
                case "gvApplicantNoPay":
                    lblRec.Text = Convert.ToString(total_row_count);
                    rows_per_page = (rows_per_page == 0) ? 1 : rows_per_page;
                    double Paaaage = Math.Ceiling(total_row_count / rows_per_page);
                    lbltotalP.Text = (total_row_count > 0) ? Convert.ToString(Paaaage) : "0";
                    OBJ_page2(visible_or_disvisible);
                    //if (total_row_count.ToInt() == 0)
                    //{
                    //    btn_P.Visible = false;
                    //    btn_N.Visible = false;
                    //}
                    //if(txt_Page_now.Text=="1")
                    //{
                    //    btn_P.Visible = false;
                    //}
                    //if (txt_Page_now.Text == txtTotalPage.Text)
                    //{
                    //    btn_N.Visible = false;
                    //}
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


            Hide_show(PreName,txtNum, NextName,N_or_P,MaxP);

        }
        protected void Hide_show(Button PreName, TextBox txtNum, Button NextName, string N_or_P, int MaxP)
        {
            if (txtNum.Text.ToInt() <= 1)
            {
                PreName.Visible = false;
            }
            else
            {
                PreName.Visible = true;
            }
            UpdatePanelGv.Update();
            if (txtNum.Text.ToInt() >= MaxP)
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
            BindDataInGridview(false);
            Hide_show(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "", txtTotalPage.Text.ToInt());
            gv2.Visible = false;
            detaiLL.Visible = false;
            UpdatePanelGv.Update();
        }

        protected void btnNextGvSearch_Click(object sender, EventArgs e)
        {
            NPbutton(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "N", txtTotalPage);
            BindDataInGridview(false);
            Hide_show(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "", txtTotalPage.Text.ToInt());
            gv2.Visible = false;
            detaiLL.Visible = false;
           UpdatePanelGv.Update();
       }

        protected void btnPreviousDGvSearch_Click(object sender, EventArgs e)
        {
            NPbutton(btn_P, txt_Page_now, btn_N, "P", lbltotalP);
            BindDATAdetail(NOW_REQUEST.Insert(6," ").Insert(11," "));
            Hide_show(btn_P, txt_Page_now, btn_N, "", txtTotalPage.Text.ToInt());
            UpdatePanelGv.Update();
            detaiLL.Visible = false;
        }

        protected void btnNextDGvSearch_Click(object sender, EventArgs e)
        {
            NPbutton(btn_P, txt_Page_now, btn_N, "N", lbltotalP);
            BindDATAdetail(NOW_REQUEST.Insert(6, " ").Insert(11, " "));
            Hide_show(btn_P, txt_Page_now, btn_N, "", txtTotalPage.Text.ToInt());
            UpdatePanelGv.Update();
            detaiLL.Visible = false;
        }
        #endregion Pageing_milk


        private void BindDataInGridview(Boolean CountAgain)
        {
            var biz = new BLL.PaymentBiz();
            string ErrMsg = "";
         
            if (txtStartPaidSubDate.Text == "")
                Set_textDate(1);
            if (txtEndPaidSubDate.Text == "")
                Set_textDate(2);
            int resultPage;

            int DayValue = lblDayValue.Text == "" ? 0 : lblDayValue.Text.ToInt(); 
            if (Convert.ToDateTime(txtStartPaidSubDate.Text) > DateTime.Today.AddDays(-DayValue))
            {
                ErrMsg = ErrMsg + "<br />- วันที่หมดอายุใบสั่งจ่าย(เริ่ม) ต้องน้อยกว่าวันที่ปัจจุบัน " + DayValue + " วัน";
                Set_textDate(1);
            }
            if (Convert.ToDateTime(txtEndPaidSubDate.Text) > DateTime.Today.AddDays(-DayValue))
            {
                ErrMsg = ErrMsg + "<br />- วันที่หมดอายุใบสั่งจ่าย(สิ้นสุด) ต้องน้อยกว่าวันที่ปัจจุบัน " + DayValue + " วัน";
                Set_textDate(2);
            }

            if (ErrMsg != "")
            {
                UCModalError.ShowMessageError =ErrMsg;
                UCModalError.ShowModalError();
            }
            else
            {
                if (!string.IsNullOrEmpty(txtStartPaidSubDate.Text) && !string.IsNullOrEmpty(txtEndPaidSubDate.Text))
                {
                    int Rpage = (txtNumberGvSearch.Text.Trim() == "") ? 0 : txtNumberGvSearch.Text.Trim().ToInt();
                    resultPage = (Rpage == 0) ? 1 : txtNumberGvSearch.Text.Trim().ToInt();
                    if ((rowPerpage.Text.Trim() == null) || (rowPerpage.Text.Trim() == "") || (rowPerpage.Text.ToInt() <= 0))
                    {
                        rowPerpage.Text = PageSize.ToString();
                    }
                    else
                    {
                        PageSize = Convert.ToInt32(rowPerpage.Text);
                    }


                    string TestNoo = txtTestingNo.Text.Trim() != "" ? txtTestingNo.Text.Trim() : "%";
                    string PlaceC = txtExamPlaceCode.Text.Trim() != "" ? txtExamPlaceCode.Text.Trim() : "%";
                    if (CountAgain)
                    {
                        #region Page

                        var CountPage = biz.GetApplicantNoPaymentHeadder(Convert.ToDateTime(txtStartPaidSubDate.Text),
                                  Convert.ToDateTime(txtEndPaidSubDate.Text), txtTestingDate.Text, TestNoo, PlaceC, resultPage, PageSize, true);

                        if (CountPage.DataResponse != null)
                        {
                            if (Convert.ToInt32(CountPage.DataResponse.Tables[0].Rows[0]["CCount"].ToString()) > 0)
                            {
                                Int32 totalROWs = Convert.ToInt32(CountPage.DataResponse.Tables[0].Rows[0]["CCount"].ToString());

                                VisibleGV(gvSearch, totalROWs, Convert.ToInt32(rowPerpage.Text), true);
                                if (Rpage == 0)
                                    NPbutton(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "", txtTotalPage);
                            }
                            else
                            {
                                VisibleGV(gvSearch, 0, Convert.ToInt32(rowPerpage.Text), true);
                                if (Rpage == 0)
                                    NPbutton(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "", txtTotalPage);
                                txtTotalPage.Text = "1";

                            }
                        }

                        #endregion Page
                    }
                    var res = biz.GetApplicantNoPaymentHeadder(Convert.ToDateTime(txtStartPaidSubDate.Text),
                              Convert.ToDateTime(txtEndPaidSubDate.Text), txtTestingDate.Text, TestNoo, PlaceC, resultPage, PageSize, false);

                    if (res.IsError)
                    {
                        var errorMsg = res.ErrorMsg;

                        UCModalError.ShowMessageError = res.ErrorMsg;
                        UCModalError.ShowModalError();
                    }
                    else
                    {
                        gvSearch.DataSource = res.DataResponse;
                        gvSearch.DataBind();

                        if (gvSearch.Rows.Count > 0)
                        {
                            btnDelete.Visible = true;
                            CheckBox ch_all = (CheckBox)gvSearch.HeaderRow.FindControl("CheckAll");
                            ch_all.Visible = true;
                        }
                        else
                        {
                            btnDelete.Visible = false;
                            CheckBox ch_all = (CheckBox)gvSearch.HeaderRow.FindControl("CheckAll");
                            ch_all.Visible = false;
                        }
                        gvSearch.Visible = true;
                        gv2.Visible = false;
                        gvApplicantNoPay.Visible = false;
                        lblDetailCandidateNoPay.Visible = false;
                        detaiLL.Visible = false;
                    }

                }

                else
                {
                    UCModalError.ShowMessageError = SysMessage.PleaseSelectDate;
                    UCModalError.ShowModalError();
                }
            }
           

        }

        private void ClearData()
        {
            txtDetailTitle.Text = string.Empty;
            txtDetailName.Text = string.Empty;
            txtDetailSurName.Text = string.Empty;
            txtDetailIdNumber.Text = string.Empty;
            rblDetailSex.SelectedValue = "M";
            txtDetailBirthDay.Text = string.Empty;
            txtDetailEducation.Text = string.Empty;
            txtDetailExamDate.Text = string.Empty;
            txtDetailRegExamID.Text = string.Empty;
            txtDetailExamNumber.Text = string.Empty;
            txtDetailPlaceExam.Text = string.Empty;
            txtDetailAssociationExamID.Text = string.Empty;
            txtInsuranceCompany.Text = string.Empty;
            
            gv2.Visible = false;
            detaiLL.Visible = false;
        }

        protected void ibtClearStartPaidDate_Click(object sender, ImageClickEventArgs e)
        {
            txtStartPaidSubDate.Text = string.Empty;
        }

        protected void ibtClearEndPaidDate_Click(object sender, ImageClickEventArgs e)
        {
            txtEndPaidSubDate.Text = string.Empty;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (Convert.ToDateTime(txtStartPaidSubDate.Text) > Convert.ToDateTime(txtEndPaidSubDate.Text))
            {
                UCModalError.ShowMessageError = Resources.errorApplicantNoPay_004;
                UCModalError.ShowModalError();
                PnlDetail.Visible = false;
            }
            else
            {
                UpdatePanelGv.Update();
                ListAppNoPay = new List<DTO.AppNoPay>();
                txtNumberGvSearch.Text = "0";
                txt_Page_now.Text = "0";
                lbltotalP.Text = "0";
                BindDataInGridview(true);
                PnlDetail.Visible = true;
                exportExcel2.Visible = false;
                if (gvSearch.Rows.Count > 0)
                {
                    btnExportExcel.Visible = true;
                }
                else
                {
                    btnExportExcel.Visible = false;
                }
            }

        }
        protected void BindDATAdetail(string GroupNo)
        {
            var biz = new BLL.PaymentBiz();

            #region Page
            int PageSizeDetail = (txt_input.Text.Trim() != "") ? txt_input.Text.ToInt() : PageSize;
            int Rpage = (txt_Page_now.Text.Trim() == "") ? 0 : txt_Page_now.Text.Trim().ToInt();
            int resultPage = (Rpage == 0) ? 1 : txt_Page_now.Text.Trim().ToInt();
            resultPage = resultPage == 0 ? 1 : resultPage;

            var CountPage = biz.GetApplicantNoPayment(txtTestingDate.Text, txtTestingNo.Text.Trim(), txtExamPlaceCode.Text.Trim(), Convert.ToDateTime(txtStartPaidSubDate.Text),
                      Convert.ToDateTime(txtEndPaidSubDate.Text), GroupNo, resultPage, PageSizeDetail, true);


            if ((txt_input.Text.Trim() == null) || (txt_input.Text.Trim() == "") || (txt_input.Text.ToInt() <=0))
            {
                txt_input.Text = PageSizeDetail.ToString();
            }
            else
            {
                PageSizeDetail = Convert.ToInt32(txt_input.Text);
            }

            if (CountPage.DataResponse != null)
                if (CountPage.DataResponse.Tables[0].Rows.Count > 0)
                {
                    Int32 totalROWs = Convert.ToInt32(CountPage.DataResponse.Tables[0].Rows[0]["CCount"].ToString());

                    VisibleGV(gvApplicantNoPay, totalROWs, Convert.ToInt32(txt_input.Text), true);
                    if (Rpage == 0)
                        NPbutton(btn_P, txt_Page_now, btn_N, "", lbltotalP);
                }
                else
                {
                    VisibleGV(gvApplicantNoPay, 0, Convert.ToInt32(txt_input.Text), true);
                    if (Rpage == 0)
                        NPbutton(btn_P, txt_Page_now, btn_N, "", lbltotalP);
                    lbltotalP.Text = "1";

                }
            #endregion Page

            var res = biz.GetApplicantNoPayment(txtTestingDate.Text, txtTestingNo.Text, txtExamPlaceCode.Text, Convert.ToDateTime(txtStartPaidSubDate.Text),
                      Convert.ToDateTime(txtEndPaidSubDate.Text), NOW_REQUEST,resultPage,PageSizeDetail,false);

            if (res.IsError)
            {
                var errorMsg = res.ErrorMsg;

                UCModalError.ShowMessageError = res.ErrorMsg;
                UCModalError.ShowModalError();
            }
            else
            {                
                gvApplicantNoPay.DataSource = res.DataResponse;
                gvApplicantNoPay.DataBind();
                gvApplicantNoPay.Visible = true;
                gv2.Visible = true;
                exportExcel2.Visible = true;
                UpdatePanelGv.Update();
            }
            detaiLL.Visible = false;
            detaiLL.Enabled = false;
            group_request.Text = Resources.propApplicantNoPay_group_request + GroupNo;
          

        }
        protected void hplViewG_Click(object sender, EventArgs e)
        {
            var gr = (GridViewRow)((LinkButton)sender).NamingContainer;
            var GroupNo = (Label)gr.FindControl("lblGroupRequsetNo");
            gv2.Visible = true;
            txt_Page_now.Text = "0";
            lbltotalP.Text = "0";
            NOW_REQUEST = GroupNo.Text.Replace(" ", "");
           // BindDATAdetail(GroupNo.Text);
            BindDATAdetail(NOW_REQUEST);
            H_NOW_REQUEST.Value = GroupNo.Text;
           
        }


        bool b_check = true;
        CheckBox check_all_head;
        protected void gvSearch_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                check_all_head = (CheckBox)e.Row.FindControl("CheckAll");
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label txtGno = (Label)e.Row.FindControl("lblGroupRequsetNo");
                string formatTxt = txtGno.Text.Insert(6, " ").Insert(11, " ");
                txtGno.Text = formatTxt;
                CheckBox check = (CheckBox)e.Row.FindControl("Chk_nopay");

                var appno = ListAppNoPay.FirstOrDefault(x => x.GroupNumber == txtGno.Text.Replace(" ", ""));
                if (appno != null)
                {
                    check.Checked = true;
                }
                else
                {
                    check.Checked = false;
                    b_check = false;
                }
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                if (b_check)
                {
                    check_all_head.Checked = true;
                }
            }
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            CancleCondition();
        }
        protected void CancleCondition()
        {
            PnlDetail.Visible = false;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");
            txtStartPaidSubDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
            txtStartPaidSubDate.Text = DateUtil.dd_MM_yyyy_Now_TH;
            txtEndPaidSubDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
            txtEndPaidSubDate.Text = DateUtil.dd_MM_yyyy_Now_TH;
            btnDelete.Visible = false;
            txtNumberGvSearch.Text = "0";
            txt_Page_now.Text = "0";
            txtTotalPage.Text = "0";
            lbltotalP.Text = "0";
            rowPerpage.Text = PageSize.ToString();
            txt_input.Text = PageSize.ToString();
            txtExamPlaceCode.Text = "";
            txtTestingNo.Text = "";
            btnExportExcel.Visible = false;
            gv2.Visible = false;
            detaiLL.Visible = false;
            txtTestingDate.Text = "";
        }

        private List<DTO.AppNoPay> ListAppNoPay
        {
            get
            {
                return Session["AppNoPay"] == null
                              ? new List<DTO.AppNoPay>()
                              : (List<DTO.AppNoPay>)Session["AppNoPay"];
            }
            set
            {
                Session["AppNoPay"] = value;
            }
        }

        protected void btn_GO_Click(object sender, EventArgs e)
        {
            BindDATAdetail(NOW_REQUEST.Insert(6," ").Insert(11," "));
        }
        public override void VerifyRenderingInServerForm(Control control) { }

        protected void btnExportExcel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                int total = lblTotal.Text == "" ? 0 : lblTotal.Text.ToInt();
                if (total > base.EXCEL_SIZE_Key)
                {
                    UCModalError.ShowMessageError = SysMessage.ExcelSizeError;
                    UCModalError.ShowModalError();
                    UpdatePanelGv.Update();
                }
                else
                {
                    ExportBiz export = new ExportBiz();
                    Dictionary<string, string> columns = new Dictionary<string, string>();
                    columns.Add("ลำดับ", "RUN_NO");
                    columns.Add("เลขที่ใบสั่งจ่ายกลุ่ม", "GROUP_REQUEST_NO");
                    columns.Add("วันครบกำหนดชำระ", "EXPIRATION_DATE");
                    columns.Add("จำนวนเงินในใบสั่งจ่ายกลุ่ม", "GROUP_AMOUNT");

                    List<HeaderExcel> header = new List<HeaderExcel>();
                    header.Add(new HeaderExcel
                    {
                        NameColumnsOne = "วันที่ต้องชำระเงิน(เริ่ม) ",
                        ValueColumnsOne = txtStartPaidSubDate.Text,
                        NameColumnsTwo = "วันที่ต้องชำระเงิน(สิ้นสุด) ",
                        ValueColumnsTwo = txtEndPaidSubDate.Text
                    });

                    header.Add(new HeaderExcel
                    {
                        NameColumnsOne = "วันที่สอบ ",
                        ValueColumnsOne = txtTestingDate.Text,
                        NameColumnsTwo = "รหัสรอบสอบ ",
                        ValueColumnsTwo = txtTestingNo.Text
                    });

                    header.Add(new HeaderExcel
                    {
                        NameColumnsOne = "รหัสสนามสอบ ",
                        ValueColumnsOne = txtExamPlaceCode.Text
                     
                    });

                 

                    var biz = new BLL.PaymentBiz();
                    var res = biz.GetApplicantNoPaymentHeadder(Convert.ToDateTime(txtStartPaidSubDate.Text),
                                 Convert.ToDateTime(txtEndPaidSubDate.Text), txtTestingDate.Text, txtTestingNo.Text, txtExamPlaceCode.Text, 1, base.EXCEL_SIZE_Key, false);               
                    export.CreateExcel(res.DataResponse.Tables[0],columns,header,base.UserProfile);
                }
            }
            catch { }
        }

        protected void Chk_nopay_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox check = (CheckBox)sender;
            GridViewRow gridrow = (GridViewRow)check.Parent.Parent;
            check_all_head = (CheckBox)((GridView)gridrow.Parent.Parent).HeaderRow.FindControl("CheckAll");
            var GroupNo = (Label)gridrow.FindControl("lblGroupRequsetNo");

            if (check.Checked)
            {
                ListAppNoPay.Add(new DTO.AppNoPay
                {
                    GroupNumber = GroupNo.Text.Replace(" ", "")
                });
            }
            else
            {
              check_all_head.Checked = false;
              ListAppNoPay = ListAppNoPay.Where(x => x.GroupNumber != GroupNo.Text.Replace(" ", "")).ToList();               
            }
        }

        protected void CheckAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkall = (CheckBox)sender;

            foreach (GridViewRow gridrow in gvSearch.Rows)
            {
                
                CheckBox check = (CheckBox)gridrow.FindControl("Chk_nopay");
                var GroupNo = (Label)gridrow.FindControl("lblGroupRequsetNo");

                if (checkall.Checked)
                {
                    if (!check.Checked)
                    {
                        ListAppNoPay.Add(new DTO.AppNoPay
                        {
                            GroupNumber = GroupNo.Text.Replace(" ", "")
                        });
                    }
                    check.Checked = true;
                }
                else
                {
                    if (check.Checked)
                    {
                        ListAppNoPay = ListAppNoPay.Where(x => x.GroupNumber != GroupNo.Text.Replace(" ", "")).ToList();
                    }
                    check.Checked = false;
                }
            }
        }

        protected void exportExcel2_Click(object sender, ImageClickEventArgs e)
        {
              ExportBiz export = new ExportBiz();

              try
              {
                  int total = lblRec.Text == "" ? 0 : lblRec.Text.ToInt();
                  if (total > base.EXCEL_SIZE_Key)
                  {
                      UCModalError.ShowMessageError = SysMessage.ExcelSizeError;
                      UCModalError.ShowModalError();
                      UpdatePanelGv.Update();
                  }
                  else
                  {
                      Dictionary<string, string> columns = new Dictionary<string, string>();
                      columns.Add("ลำดับ", "RUN_NO");
                      columns.Add("เลขบัตรประชาชน", "ID_CARD_NO");
                      columns.Add("วันที่สมัคร", "CREATED_DATE");
                      columns.Add("ชื่อ", "FIRSTNAME");
                      columns.Add("นามสกุล", "LASTNAME");
                      columns.Add("วันที่สอบ", "TESTING_DATE");

                      var biz = new BLL.PaymentBiz();
                      var res = biz.GetApplicantNoPayment(txtTestingDate.Text, txtTestingNo.Text, txtExamPlaceCode.Text, Convert.ToDateTime(txtStartPaidSubDate.Text),
                              Convert.ToDateTime(txtEndPaidSubDate.Text), H_NOW_REQUEST.Value, 1, base.EXCEL_SIZE_Key, false);
                      int[] colum = new int[] { 9 };
                      export.CreateExcel(res.DataResponse,columns);
                  }
              }
              catch { }
            
        }        
    }
}
