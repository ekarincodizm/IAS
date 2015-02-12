using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Globalization;
using IAS.Utils;
using AjaxControlToolkit;
using System.Data;
using IAS.BLL;
using IAS.Properties;

namespace IAS.Payment
{
    public partial class Invoice5 : basepage
    {
        #region Public Param & Session
        public class lsPrint
        {
            //public int str
            public string HEAD_REQUEST_NO { get; set; }
            public string GROUP_REQUEST_NO { get; set; }
            public string PERSON_NO { get; set; }
            public string GROUP_AMOUNT { get; set; }
            public string SUBPAYMENT_DATE { get; set; }
            public string REMARK { get; set; }
        }

        public List<lsPrint> lsRecivePrint
        {
            get
            {
                if (Session["lsRecivePrint"] == null)
                {
                    Session["lsRecivePrint"] = new List<lsPrint>();
                }

                return (List<lsPrint>)Session["lsRecivePrint"];
            }
            set
            {
                Session["lsRecivePrint"] = value;
            }
        }
        public List<string> ListPayment
        {
            get
            {
                if (Session["ListPayment"] == null)
                {
                    Session["ListPayment"] = new List<string>();
                }

                return (List<string>)Session["ListPayment"];
            }

            set
            {
                Session["ListPayment"] = value;
            }
        }

        public MasterPage.Site1 MasterSite
        {

            get { return (this.Page.Master as MasterPage.Site1); }
        }
        public List<string> ListPrintPayment
        {
            get
            {
                if (Session["ListPrintPayment"] == null)
                {
                    Session["ListPrintPayment"] = new List<string>();
                }

                return (List<string>)Session["ListPrintPayment"];
            }

            set
            {
                Session["ListPrintPayment"] = value;
            }
        }
        public List<string> ListChkAll
        {
            get
            {
                if (Session["ListChkAll"] == null)
                {
                    Session["ListChkAll"] = new List<string>();
                }

                return (List<string>)Session["ListChkAll"];
            }

            set
            {
                Session["ListChkAll"] = value;
            }
        }
        public int PAGE_SIZE;
        public int _totalPages;
        public int TotalPages
        {
            get { return _totalPages; }
            set { _totalPages = value; }
        }
        public string GroupRequestNoValue
        {
            get
            {
                return (Session["GroupRequestNoValue"] == null) ? "" : Session["GroupRequestNoValue"].ToString();
            }
            set
            {
                Session["GroupRequestNoValue"] = value;
            }
        }
        public string HeadRequestNoValue
        {
            get
            {
                return (Session["HeadRequestNoValue"] == null) ? "" : Session["HeadRequestNoValue"].ToString();
            }
            set
            {
                Session["HeadRequestNoValue"] = value;
            }
        }
        public int countCheckDetail = 0;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            txtEndPaidSubDate.Attributes.Add("readonly", "true");
            txtStartPaidSubDate.Attributes.Add("readonly", "true");
            txtStartExamDate.Attributes.Add("readonly", "true");
            txtEndExamDate.Attributes.Add("readonly", "true");
            // ScriptManager.RegisterStartupScript(this, GetType(), "textreadonly", "setdate();", true);
            if (!Page.IsPostBack)
            {
                ListPayment = new List<string>();
                base.HasPermit();
                defaultData();
                //gv3.Visible = false;
                //gv2.Visible = false;
               
            }
        }
        string maxBefore = string.Empty;
        string maxBeforeNon = string.Empty;
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (Convert.ToDateTime(txtStartPaidSubDate.Text) > Convert.ToDateTime(txtEndPaidSubDate.Text))
            {
                this.MasterSite.ModelError.ShowMessageError = Resources.errorApplicantNoPay_004;
                this.MasterSite.ModelError.ShowModalError();
            }
            else if (txtStartExamDate.Text != "" && txtEndExamDate.Text != "")
            {
                if(Convert.ToDateTime(txtStartExamDate.Text) > Convert.ToDateTime(txtEndExamDate.Text))
                {
                this.MasterSite.ModelError.ShowMessageError = Resources.errorApplicantNoPay_004;
                this.MasterSite.ModelError.ShowModalError();
                }
                else
                {
                    ListPayment = new List<string>();
                    ListChkAll.Clear();
                    #region Gv2
                    txtNumberGvSearch2.Visible = false;
                    lblParaPage2.Visible = false;
                    txtTotalPage2.Visible = false;
                    btnNextGvSearch2.Visible = false;
                    btnPreviousGvSearch2.Visible = false;
                    divGv2.Visible = false;
                    divGvNonSubPayment.Visible = false;
                    #endregion
                    #region Gv1
                    txtNumberGvSearch.Visible = false;
                    txtNumberGvSearch.Text = "1";
                    lblParaPage.Visible = false;
                    txtTotalPage.Visible = false;
                    btnNextGvSearch.Visible = false;
                    btnPreviousGvSearch.Visible = false;

                    txtInputMaxrow.Visible = false;
                    lblHeadInputMaxrow.Visible = false;
                    lblHeadTotal.Visible = false;
                    lblTotalrecord.Visible = false;
                    lblEndTotal.Visible = false;
                    btngo.Visible = false;
                    PAGE_SIZE = PAGE_SIZE_Key;
                    maxBefore = txtInputMaxrow.Text;
                    if ((txtInputMaxrow.Text != Convert.ToString(PAGE_SIZE) && txtInputMaxrow.Text != "" && Convert.ToInt32(txtInputMaxrow.Text) != 0))
                    {

                        txtInputMaxrow.Text = maxBefore;
                    }
                    else if (txtInputMaxrow.Text == "" || Convert.ToInt32(txtInputMaxrow.Text) == 0)
                    {
                        txtInputMaxrow.Text = Convert.ToString(PAGE_SIZE);
                    }
                    maxBeforeNon = txtInputMaxrowNon.Text;
                    if ((txtInputMaxrowNon.Text != Convert.ToString(PAGE_SIZE) && txtInputMaxrowNon.Text != "" && Convert.ToInt32(txtInputMaxrowNon.Text) != 0))
                    {

                        txtInputMaxrowNon.Text = maxBeforeNon;
                    }
                    else if (txtInputMaxrowNon.Text == "" || Convert.ToInt32(txtInputMaxrowNon.Text) == 0)
                    {
                        txtInputMaxrowNon.Text = Convert.ToString(PAGE_SIZE);
                    }
                    divGv1.Visible = true;
                    divNon.Visible = true;
                    #endregion
                    if (base.UserProfile.MemberType == DTO.RegistrationType.General.GetEnumValue() 
                        || base.UserProfile.MemberType == DTO.RegistrationType.Insurance.GetEnumValue()
                        || base.UserProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue())
                    {
                        lblLegendGv1.Text = "ใบสั่งจ่ายที่ออกโดยตนเอง";
                        legendGV1.Visible = true;
                    }
                    BindDataInGridView();
                    gvSubDetail.Visible = false;
                    //gvDetailOfSub.Visible = false;
                    //gv2.Visible = false;
                    //gv3.Visible = false;
                }
            }
            else
            {
                ListPayment = new List<string>();
                ListChkAll.Clear();
                #region Gv2
                txtNumberGvSearch2.Visible = false;
                lblParaPage2.Visible = false;
                txtTotalPage2.Visible = false;
                btnNextGvSearch2.Visible = false;
                btnPreviousGvSearch2.Visible = false;
                divGv2.Visible = false;
                divGvNonSubPayment.Visible = false;
                #endregion
                #region Gv1
                txtNumberGvSearch.Visible = false;
                txtNumberGvSearch.Text = "1";
                lblParaPage.Visible = false;
                txtTotalPage.Visible = false;
                btnNextGvSearch.Visible = false;
                btnPreviousGvSearch.Visible = false;

                txtInputMaxrow.Visible = false;
                lblHeadInputMaxrow.Visible = false;
                lblHeadTotal.Visible = false;
                lblTotalrecord.Visible = false;
                lblEndTotal.Visible = false;
                btngo.Visible = false;
                PAGE_SIZE = PAGE_SIZE_Key;
                maxBefore = txtInputMaxrow.Text;
                if ((txtInputMaxrow.Text != Convert.ToString(PAGE_SIZE) && txtInputMaxrow.Text != "" && Convert.ToInt32(txtInputMaxrow.Text) != 0))
                {

                    txtInputMaxrow.Text = maxBefore;
                }
                else if (txtInputMaxrow.Text == "" || Convert.ToInt32(txtInputMaxrow.Text) == 0)
                {
                    txtInputMaxrow.Text = Convert.ToString(PAGE_SIZE);
                }
                maxBeforeNon = txtInputMaxrowNon.Text;
                if ((txtInputMaxrowNon.Text != Convert.ToString(PAGE_SIZE) && txtInputMaxrowNon.Text != "" && Convert.ToInt32(txtInputMaxrowNon.Text) != 0))
                {

                    txtInputMaxrowNon.Text = maxBeforeNon;
                }
                else if (txtInputMaxrowNon.Text == "" || Convert.ToInt32(txtInputMaxrowNon.Text) == 0)
                {
                    txtInputMaxrowNon.Text = Convert.ToString(PAGE_SIZE);
                }
                divGv1.Visible = true;
                divNon.Visible = true;
                #endregion
                if (base.UserProfile.MemberType == DTO.RegistrationType.General.GetEnumValue()
                    || base.UserProfile.MemberType == DTO.RegistrationType.Insurance.GetEnumValue() 
                    || base.UserProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue())
                {
                    lblLegendGv1.Text = "ใบสั่งจ่ายที่ออกโดยตนเอง";
                    legendGV1.Visible = true;
                }
                BindDataInGridView();
                gvSubDetail.Visible = false;
                //gvDetailOfSub.Visible = false;
                //gv2.Visible = false;
                //gv3.Visible = false;
            }
            
        }

        protected void ibtClearStartPaidDate_Click(object sender, ImageClickEventArgs e)
        {
            txtStartPaidSubDate.Text = string.Empty;
        }

        protected void ibtClearEndPaidDate_Click(object sender, ImageClickEventArgs e)
        {
            txtEndPaidSubDate.Text = string.Empty;
        }

        protected void BindGv2()
        {
            PAGE_SIZE = PAGE_SIZE_Key;
            txtNumberGvSearch2.Text = "1";
            maxBefore = txtInputMaxrow2.Text;
            if ((txtInputMaxrow2.Text != Convert.ToString(PAGE_SIZE) && txtInputMaxrow2.Text != "" && Convert.ToInt32(txtInputMaxrow2.Text) != 0))
            {

                txtInputMaxrow2.Text = maxBefore;
            }
            else if (txtInputMaxrow2.Text == "" || Convert.ToInt32(txtInputMaxrow2.Text) == 0)
            {
                txtInputMaxrow2.Text = Convert.ToString(PAGE_SIZE);
            }
            PAGE_SIZE = Convert.ToInt32(txtInputMaxrow2.Text);
            //txtNumberGvSearch3.Visible = false;
            //lblParaPage3.Visible = false;
            //txtTotalPage3.Visible = false;
            //btnNextGvSearch3.Visible = false;
            //btnPreviousGvSearch3.Visible = false;
            divGv2.Visible = true;
            var biz = new BLL.PaymentBiz();
            var resultPage = txtNumberGvSearch2.Text.ToInt();


            var resCount = biz.GetSubPaymentByHeaderRequestNo(GroupRequestNoValue.Replace(" ", ""), "Y", resultPage, PAGE_SIZE);
            DataSet ds = resCount.DataResponse;
            DataTable dt = ds.Tables[0];
            DataRow dr = dt.Rows[0];
            int rowcount = Convert.ToInt32(dr["rowcount"].ToString());
            double dblPageCount = (double)((decimal)rowcount / PAGE_SIZE);
            TotalPages = (int)Math.Ceiling(dblPageCount);
            txtTotalPage2.Text = Convert.ToString(TotalPages);
            var res = biz.GetSubPaymentByHeaderRequestNo(GroupRequestNoValue.Replace(" ", ""), "N", resultPage, PAGE_SIZE);
            gvSubDetail.DataSource = res.DataResponse;
            gvSubDetail.DataBind();
            if (res.IsError)
            {
                //var errorMsg = res.ErrorMsg;

                //AlertMessage.ShowAlertMessage(string.Empty, errorMsg);

                this.MasterSite.ModelError.ShowMessageError = "ไม่สามารถค้นหาข้อมูลได้";
                this.MasterSite.ModelError.ShowModalError();

            }
            else
            {
                group_request.Text = Resources.propGenPayment_group_request + GroupRequestNoValue;
                gvSubDetail.Visible = true;
                divGv2.Visible = true;

                if (TotalPages > 1)
                {
                    txtNumberGvSearch2.Visible = true;
                    lblParaPage2.Visible = true;
                    txtTotalPage2.Visible = true;
                    btnNextGvSearch2.Visible = true;
                    divGv2.Visible = true;
                    btngo2.Visible = true;
                    txtInputMaxrow2.Visible = true;
                    lblHeadInputMaxrow2.Visible = true;
                    lblHeadTotal2.Visible = true;
                    lblTotalrecord2.Visible = true;
                    lblEndTotal2.Visible = true;
                    lblTotalrecord2.Text = dr["rowcount"].ToString();
                }
                else if (TotalPages == 1)
                {
                    txtNumberGvSearch2.Visible = true;
                    lblParaPage2.Visible = true;
                    txtTotalPage2.Visible = true;
                    divGv2.Visible = true;
                    btngo2.Visible = true;
                    txtInputMaxrow2.Visible = true;
                    lblHeadInputMaxrow2.Visible = true;
                    lblHeadTotal2.Visible = true;
                    lblTotalrecord2.Visible = true;
                    lblEndTotal2.Visible = true;
                    btnNextGvSearch2.Visible = false;
                    lblTotalrecord2.Text = dr["rowcount"].ToString();
                }
                if (TotalPages == 0)
                {
                    txtNumberGvSearch2.Visible = true;
                    lblParaPage2.Visible = true;
                    txtTotalPage2.Visible = true;
                    divGv2.Visible = true;
                    btngo2.Visible = true;
                    txtInputMaxrow2.Visible = true;
                    lblHeadInputMaxrow2.Visible = true;
                    lblHeadTotal2.Visible = true;
                    lblTotalrecord2.Visible = true;
                    lblEndTotal2.Visible = true;
                    lblTotalrecord2.Text = "0";
                    txtTotalPage2.Text = "1";
                    //divGv3.Visible = false;
                }
                UpdatePanelSearch.Update();
            }
        }
        protected void hplView_Click(object sender, EventArgs e)
        {
            txtInputMaxrow2.Text = Convert.ToString(PAGE_SIZE_Key);//milk
            txtNumberGvSearch2.Text = "1";
           // divGv3.Visible = false;
            GroupRequestNoValue = string.Empty;
            var gr = (GridViewRow)((LinkButton)sender).NamingContainer;

            var PaidGroup = (Label)gr.FindControl("lblGroupRequsetNo");
            if (group_request.Text != PaidGroup.Text)
            {
                gvSubDetail.Visible = false;
               // gvDetailOfSub.Visible = false;
                //gv3.Visible = false;
            }
            GroupRequestNoValue = PaidGroup.Text;
            H_GroupRequestNoValue.Value = PaidGroup.Text;
            divGv2.Visible = true;
            divGvNonSubPayment.Visible = false ;
            BindGv2();
        }
        protected void hplGo2_Click(object sender, EventArgs e)
        {
            BindGv2();
            //gv3.Visible = false;
            //divGv3.Visible = false;
        }
        protected void hplPrint_Click(object sender, EventArgs e)
        {
            var gr = (GridViewRow)((LinkButton)sender).NamingContainer;

            var data = new List<lsPrint>();

            var lblUploadBySession = (Label)gr.FindControl("lblUploadBySession");
            var lblGroupRequsetNo = (Label)gr.FindControl("lblGroupRequsetNo");
            var lblEXPIRATION_DATE = (Label)gr.FindControl("lblEXPIRATION_DATE");
            LinkButton button = (LinkButton)gr.FindControl("hplPrint");
            LinkButton btnDelete = (LinkButton)gr.FindControl("hplDelete");
            
      
            ListPrintPayment.Add(lblGroupRequsetNo.Text.Replace(" ",""));
            var biz = new BLL.PaymentBiz();
            var res = biz.UpdatePrintGroupRequestNo(ListPrintPayment.ToArray());
            if (res.ResultMessage == true)
            {
                btnDelete.Visible = false;
            }
     
         
                button.Attributes.Add("onclick", string.Format("OpenPopupSingle('{0}')", lblGroupRequsetNo.Text.Replace(" ", "") + " " + lblUploadBySession.Text));
            
            UpdatePanelSearch.Update();


        }
        protected void hplDelete_Click(object sender, EventArgs e)
        {
            var gr = (GridViewRow)((LinkButton)sender).NamingContainer;
            var PaidGroup = (Label)gr.FindControl("lblGroupRequsetNo");
            var biz = new BLL.PaymentBiz();
            var res = biz.CancelGroupRequestNo(PaidGroup.Text.Replace(" ","").Trim());
            if (res.IsError)
            {
                this.MasterSite.ModelError.ShowMessageError = "ไม่สามารถลบข้อมูลใบจ่ายสั่งได้";
                this.MasterSite.ModelError.ShowModalError(); 
                BindDataInGridView();
            }
            else
            {
                this.MasterSite.ModelSuccess.ShowMessageSuccess = "ลบข้อมูลใบจ่ายสั่งเรียบร้อยแล้ว";
                this.MasterSite.ModelSuccess.ShowModalSuccess();
                BindDataInGridView();
            }
            UpdatePanelSearch.Update();
        }
        protected void BindGv3()
        {
            PAGE_SIZE = PAGE_SIZE_Key;
            txtNumberGvSearch3.Text = "1";
            maxBefore = txtInputMaxrow3.Text;
            if ((txtInputMaxrow3.Text != Convert.ToString(PAGE_SIZE) && txtInputMaxrow3.Text != "" && Convert.ToInt32(txtInputMaxrow3.Text) != 0))
            {

                txtInputMaxrow3.Text = maxBefore;
            }
            else if (txtInputMaxrow3.Text == "" || Convert.ToInt32(txtInputMaxrow3.Text) == 0)
            {
                txtInputMaxrow3.Text = Convert.ToString(PAGE_SIZE);
            }
            //divGv3.Visible = true;
            PAGE_SIZE = Convert.ToInt32(txtInputMaxrow3.Text);
            var biz = new BLL.PaymentBiz();
            var resultPage = txtNumberGvSearch3.Text.ToInt();
            var resCount = biz.GetSubPaymentByHeaderRequestNo(Non_GroupRequestNoValue.Value.Replace(" ",""), "Y", resultPage, PAGE_SIZE);
            DataSet ds = resCount.DataResponse;
            DataTable dt = ds.Tables[0];
            DataRow dr = dt.Rows[0];
            int rowcount = Convert.ToInt32(dr["rowcount"].ToString());
            double dblPageCount = (double)((decimal)rowcount / PAGE_SIZE);
            TotalPages = (int)Math.Ceiling(dblPageCount);
            txtTotalPage3.Text = Convert.ToString(TotalPages);
            var res = biz.GetSubPaymentByHeaderRequestNo(Non_GroupRequestNoValue.Value.Replace(" ", ""), "N", resultPage, PAGE_SIZE);
            GVSubNonPayment.DataSource = res.DataResponse;
            GVSubNonPayment.DataBind();

            if (res.IsError)
            {
                this.MasterSite.ModelError.ShowMessageError = "ไม่สามารถค้นหาข้อมูลได้";
                this.MasterSite.ModelError.ShowModalError();
            }
            else
            {
                GVSubNonPayment.Visible = true;
               // Headrequest.Text = Resources.propGenPayment_001 + HeadRequestNoValue;
                //gv3.Visible = true;
                btnExport4.Visible = true;
                if (TotalPages > 1)
                {
                    txtNumberGvSearch3.Visible = true;
                    lblParaPage3.Visible = true;
                    txtTotalPage3.Visible = true;
                    btnNextGvSearch3.Visible = true;
                    //divGv3.Visible = true;
                    btngo3.Visible = true;
                    txtInputMaxrow3.Visible = true;
                    lblHeadInputMaxrow3.Visible = true;
                    lblHeadTotal3.Visible = true;
                    lblTotalrecord3.Visible = true;
                    lblEndTotal3.Visible = true;
                    lblTotalrecord3.Text = dr["rowcount"].ToString();
                }
                else if (TotalPages == 1)
                {
                    txtNumberGvSearch3.Visible = true;
                    lblParaPage3.Visible = true;
                    txtTotalPage3.Visible = true;
                    btnNextGvSearch3.Visible = false;
                    btngo3.Visible = true;
                    txtInputMaxrow3.Visible = true;
                    lblHeadInputMaxrow3.Visible = true;
                    lblHeadTotal3.Visible = true;
                    lblTotalrecord3.Visible = true;
                    lblEndTotal3.Visible = true;
                    lblTotalrecord3.Text = dr["rowcount"].ToString();
                }
                if (GVSubNonPayment.Rows.Count == 0)
                {
                    txtNumberGvSearch3.Visible = true;
                    lblParaPage3.Visible = true;
                    txtTotalPage3.Visible = true;
                    //divGv3.Visible = true;
                    btngo3.Visible = true;
                    txtInputMaxrow3.Visible = true;
                    lblHeadInputMaxrow3.Visible = true;
                    lblHeadTotal3.Visible = true;
                    lblTotalrecord3.Visible = true;
                    lblEndTotal3.Visible = true;
                    lblTotalrecord3.Text = "0";
                    txtTotalPage3.Text = "1";
                    //divGv3.Visible = true;
                }
                GVSubNonPayment.Visible = true;
                UpdatePanelSearch.Update();
            }
        }
        protected void btnGo3_Click(object sender, EventArgs e)
        {
            BindGv3();
        }
        protected void hplViewNon_Click(object sender, EventArgs e)
        {
            txtInputMaxrow3.Text = PAGE_SIZE_Key.ToString();
            txtNumberGvSearch3.Text = "1";
            var gr = (GridViewRow)((LinkButton)sender).NamingContainer;

            var lblGroupRequsetNo = (Label)gr.FindControl("lblGroupRequsetNo");
        //    var lblTypePaidGv = (Label)gr.FindControl("lblTypePaidGv");
        //    if (Headrequest.Text != lblHeadRequestNoSubGv.Text)
        //    {
        //        gvDetailOfSub.Visible = false;
        //    }

            Non_GroupRequestNoValue.Value = lblGroupRequsetNo.Text;
        //    H_HeadRequestNoValue.Value = lblHeadRequestNoSubGv.Text;
            lblNonGroupPayment.Visible = true;
            lblNonGroupPayment.Text = "เลขที่ใบสั่งจ่ายกลุ่ม " + lblGroupRequsetNo.Text;
            GVSearchN.Visible = true;
            GVSubNonPayment.Visible = true;
            divGvNonSubPayment.Visible = true;
            divGv2.Visible = false;
            BindGv3();
        }
        string UserType = string.Empty;
        string Type = string.Empty;
        string UserTypeNon = string.Empty;
        string TypeNon = string.Empty;
        private void BindDataInGridView()
        {
            try
            {
                if ((txtStartPaidSubDate.Text != null && txtEndPaidSubDate.Text != null) && Convert.ToDateTime(txtStartPaidSubDate.Text) > Convert.ToDateTime(txtEndPaidSubDate.Text))
                {
                    PnlDetailSearchGridView.Visible = false;
                    this.MasterSite.ModelError.ShowMessageError = Resources.errorApplicantNoPay_004;
                    this.MasterSite.ModelError.ShowModalError();
                }
                else
                {
                    PnlDetailSearchGridView.Visible = true;
                    PAGE_SIZE = Convert.ToInt32(txtInputMaxrow.Text);
                    txtNumberGvSearch.Text = "1";
                    if (base.UserProfile.MemberType == DTO.RegistrationType.General.GetEnumValue())
                    {
                        UserType = "dt.id_card_no";
                        Type = base.IdCard;
                    }

                    else if (base.UserProfile.MemberType == DTO.RegistrationType.Insurance.GetEnumValue()
                        || base.UserProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue())
                    {
                        UserType = "UPLOAD_BY_SESSION";
                        Type = base.UserProfile.CompCode;
                    }
                    DateTime? StartD = null;
                    DateTime? EndD = null;
                    if (txtStartExamDate.Text != "")
                    {
                        StartD = Convert.ToDateTime(txtStartExamDate.Text);
                    }
                    if (txtEndExamDate.Text != "")
                    {
                        EndD = Convert.ToDateTime(txtEndExamDate.Text);
                    }
                    var biz = new BLL.PaymentBiz();
                    var resultPage = txtNumberGvSearch.Text.ToInt();
                    if (base.UserProfile.MemberType == DTO.RegistrationType.General.GetEnumValue() 
                        || base.UserProfile.MemberType == DTO.RegistrationType.Insurance.GetEnumValue() 
                        || base.UserProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue())
                    {
                        if (txtStartPaidSubDate.Text != "" && txtEndPaidSubDate.Text != "")
                        {
                            #region owner
         
                            var resCount1 = biz.GetSinglePayment(Type,
                                            Convert.ToDateTime(txtStartPaidSubDate.Text),
                                            Convert.ToDateTime(txtEndPaidSubDate.Text), txtPaidGroup.Text.Replace(" ", "").Trim(), StartD, EndD
                                            ,ddlLicenseType.SelectedValue,txtTestingNo.Text.Trim(), UserType, resultPage, PAGE_SIZE, "Y");
                            if (resCount1.IsError)
                            {
                                //div_totalR.Visible = false;
                                btnNextGvSearch.Visible = false;
                                btnPreviousGvSearch.Visible = false;
                                txtNumberGvSearch.Visible = false;
                                lblParaPage.Visible = false;
                                txtTotalPage.Visible = false;
                                lblHeadInputMaxrow.Visible = false;
                                txtInputMaxrow.Visible = false;
                                btngo.Visible = false;
                                divGv1.Visible = false;
                                this.MasterSite.ModelError.ShowMessageError = "ไม่สามารถค้นหาข้อมูลได้";
                                this.MasterSite.ModelError.ShowModalError();
                            }
                            else
                            {
                                //div_totalR.Visible = true;
                                DataSet ds = resCount1.DataResponse;
                                DataTable dt = ds.Tables[0];
                                DataRow dr = dt.Rows[0];
                                int rowcount = Convert.ToInt32(dr["rowcount"].ToString());
                                double dblPageCount = (double)((decimal)rowcount / PAGE_SIZE);
                                TotalPages = (int)Math.Ceiling(dblPageCount);
                                txtTotalPage.Text = Convert.ToString(TotalPages);

                                var res = biz.GetSinglePayment(Type,
                                            Convert.ToDateTime(txtStartPaidSubDate.Text),
                                            Convert.ToDateTime(txtEndPaidSubDate.Text), txtPaidGroup.Text.Replace(" ", "").Trim(), StartD, EndD
                                            ,ddlLicenseType.SelectedValue,txtTestingNo.Text.Trim(),
                                            UserType, resultPage, PAGE_SIZE, "N");
                                boxResult.Visible = true;


                                if (res.IsError)
                                {
                                    this.MasterSite.ModelError.ShowMessageError = "ไม่สามารถค้นหาข้อมูลได้";
                                    this.MasterSite.ModelError.ShowModalError();
                                }
                                else
                                {
                                    gvSearch.Visible = true;
                                    gvSearch.DataSource = res.DataResponse;
                                    gvSearch.DataBind();

                                    if (TotalPages == 0)
                                    {
                                        divGv1.Visible = true;
                                        txtNumberGvSearch.Visible = true;
                                        lblParaPage.Visible = true;
                                        txtTotalPage.Visible = true;

                                        btngo.Visible = true;
                                        lblTotalrecord.Text = "0";
                                        txtTotalPage.Text = "1";
                                        txtInputMaxrow.Visible = true;
                                        lblHeadInputMaxrow.Visible = true;
                                        lblHeadTotal.Visible = true;
                                        lblTotalrecord.Visible = true;
                                        lblEndTotal.Visible = true;
                                        btnsumPrint.Visible = false;
                                        btnExportExcel.Visible = false;
                                        CheckBox ckall = (CheckBox)gvSearch.HeaderRow.FindControl("Checkall");
                                        ckall.Visible = false;
                                    }
                                    else if (TotalPages > 1)
                                    {
                                        txtNumberGvSearch.Visible = true;
                                        lblParaPage.Visible = true;
                                        txtTotalPage.Visible = true;
                                        btnNextGvSearch.Visible = true;
                                        btnsumPrint.Visible = true;
                                        btngo.Visible = true;
                                        lblTotalrecord.Text = dr["rowcount"].ToString();
                                        txtInputMaxrow.Visible = true;
                                        lblHeadInputMaxrow.Visible = true;
                                        lblHeadTotal.Visible = true;
                                        lblTotalrecord.Visible = true;
                                        lblEndTotal.Visible = true;
                                        btnExportExcel.Visible = true;
                                        divGv1.Visible = true;
                                    }
                                    else if (TotalPages == 1)
                                    {
                                        txtNumberGvSearch.Visible = true;
                                        lblParaPage.Visible = true;
                                        txtTotalPage.Visible = true;
                                        btnsumPrint.Visible = true;
                                        btngo.Visible = true;
                                        lblTotalrecord.Text = dr["rowcount"].ToString();
                                        txtInputMaxrow.Visible = true;
                                        lblHeadInputMaxrow.Visible = true;
                                        lblHeadTotal.Visible = true;
                                        lblTotalrecord.Visible = true;
                                        lblEndTotal.Visible = true;
                                        btnExportExcel.Visible = true;
                                        divGv1.Visible = true;
                                    }
                                    UpdatePanelSearch.Update();
                                }
                            }
                            #endregion
                            #region not owner
                            if (base.UserProfile.MemberType == DTO.RegistrationType.General.GetEnumValue())
                            {
                                UserTypeNon = "dt.id_card_no";
                                TypeNon = base.IdCard;
                            }

                            else if (base.UserProfile.MemberType == DTO.RegistrationType.Insurance.GetEnumValue() )
                            {
                                UserTypeNon = "Insurance";
                                TypeNon = base.UserProfile.CompCode;
                            }
                            else if (base.UserProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue())
                            {
                                UserTypeNon = "Association";
                                TypeNon = base.UserProfile.CompCode;
                            }
                            PAGE_SIZE = Convert.ToInt32(txtInputMaxrowNon.Text);
                            txtNumberGvSearchNon.Text = "1";
                            var resCountNon = biz.GetNonPayment(TypeNon, Convert.ToDateTime(txtStartPaidSubDate.Text),
                             Convert.ToDateTime(txtEndPaidSubDate.Text), txtPaidGroup.Text.Replace(" ", "").Trim(), StartD, EndD, ddlLicenseType.SelectedValue, txtTestingNo.Text.Trim(), UserTypeNon, resultPage, PAGE_SIZE, "Y");
                            if (resCountNon.IsError)
                            {
                               // div_totalR.Visible = false;
                                btnNextGvSearchNon.Visible = false;
                                btnPreviousGvSearchNon.Visible = false;
                                txtNumberGvSearchNon.Visible = false;
                                lblParaPageNon.Visible = false;
                                txtTotalPageNon.Visible = false;
                                lblHeadInputMaxrowNon.Visible = false;
                                txtInputMaxrowNon.Visible = false;
                                btngoNon.Visible = false;
                                divGv1.Visible = false;
                                this.MasterSite.ModelError.ShowMessageError = "ไม่สามารถค้นหาข้อมูลได้";
                                this.MasterSite.ModelError.ShowModalError();
                            }
                            else
                            {
                               // div_totalR.Visible = true;
                                DataSet dsNon = resCountNon.DataResponse;
                                DataTable dtNon = dsNon.Tables[0];
                                DataRow drNon = dtNon.Rows[0];
                                int rowcountNon = Convert.ToInt32(drNon["rowcount"].ToString());
                                double dblPageCountNon = (double)((decimal)rowcountNon / PAGE_SIZE);
                                TotalPages = (int)Math.Ceiling(dblPageCountNon);
                                txtTotalPageNon.Text = Convert.ToString(TotalPages);

                                var resNon = biz.GetNonPayment(TypeNon,
                                            Convert.ToDateTime(txtStartPaidSubDate.Text),
                                            Convert.ToDateTime(txtEndPaidSubDate.Text), txtPaidGroup.Text.Replace(" ", "").Trim(), StartD, EndD, ddlLicenseType.SelectedValue, txtTestingNo.Text.Trim(), UserTypeNon, resultPage, PAGE_SIZE, "N");
                                boxResult.Visible = true;
                                if (resNon.IsError)
                                {
                                    this.MasterSite.ModelError.ShowMessageError = "ไม่สามารถค้นหาข้อมูลได้";
                                    this.MasterSite.ModelError.ShowModalError();
                                }
                                else
                                {
                                    GVSearchN.Visible = true;
                                    GVSearchN.DataSource = resNon.DataResponse;
                                    GVSearchN.DataBind();

                                    if (TotalPages == 0)
                                    {
                                        divGv1.Visible = true;
                                        divNon.Visible = true;
                                        txtNumberGvSearchNon.Visible = true;
                                        lblParaPageNon.Visible = true;
                                        txtTotalPageNon.Visible = true;
                                        btnPreviousGvSearchNon.Visible = false;
                                        btnNextGvSearchNon.Visible = false;
                                        btngoNon.Visible = true;
                                        lblTotalrecordNon.Text = "0";
                                        txtTotalPageNon.Text = "1";
                                        txtInputMaxrowNon.Visible = true;
                                        lblHeadInputMaxrowNon.Visible = true;
                                        lblHeadTotalNon.Visible = true;
                                        lblTotalrecordNon.Visible = true;
                                        lblEndTotalNon.Visible = true;
                                        //btnsumPrintNon.Visible = false;
                                        //btnExportExcel.Visible = false;
                             
                                    }
                                    else if (TotalPages > 1)
                                    {
                                        txtNumberGvSearchNon.Visible = true;
                                        lblParaPageNon.Visible = true;
                                        txtTotalPageNon.Visible = true;
                                        btnNextGvSearchNon.Visible = true;
                                       
                                        btngoNon.Visible = true;
                                        lblTotalrecordNon.Text = drNon["rowcount"].ToString();
                                        txtInputMaxrowNon.Visible = true;
                                        lblHeadInputMaxrowNon.Visible = true;
                                        lblHeadTotalNon.Visible = true;
                                        lblTotalrecordNon.Visible = true;
                                        lblEndTotalNon.Visible = true;
                                        divNon.Visible = true;
                                        divGv1.Visible = true;
                                    }
                                    else if (TotalPages == 1)
                                    {
                                        txtNumberGvSearchNon.Visible = true;
                                        lblParaPageNon.Visible = true;
                                        txtTotalPageNon.Visible = true;
                                        btnPreviousGvSearchNon.Visible = false;
                                        btnNextGvSearchNon.Visible = false;
                                        btngoNon.Visible = true;
                                        lblTotalrecordNon.Text = drNon["rowcount"].ToString();
                                        txtInputMaxrowNon.Visible = true;
                                        lblHeadInputMaxrowNon.Visible = true;
                                        lblHeadTotalNon.Visible = true;
                                        lblTotalrecordNon.Visible = true;
                                        lblEndTotalNon.Visible = true;
                                        divNon.Visible = true;
                                        divGv1.Visible = true;
                                    }
                                    UpdatePanelSearch.Update();
                                }
                                
                            }

                            #endregion
                        }
                    }
                    #region เจ้าหน้าที่/คปภ.
                    else if ((base.UserProfile.MemberType == DTO.RegistrationType.OIC.GetEnumValue())
                        || (base.UserProfile.MemberType == DTO.RegistrationType.OICFinace.GetEnumValue())
                        || (base.UserProfile.MemberType == DTO.RegistrationType.OICAgent.GetEnumValue()))
                    {
                        divNon.Visible = false;
                        if (txtStartPaidSubDate.Text != "" && txtEndPaidSubDate.Text != "")
                        {
                      
                            var resCount2 = biz.GetSinglePayment(base.UserProfile.CompCode,
                               Convert.ToDateTime(txtStartPaidSubDate.Text),
                               Convert.ToDateTime(txtEndPaidSubDate.Text), txtPaidGroup.Text.Replace(" ", "").Trim(),
                               StartD,EndD
                                            ,ddlLicenseType.SelectedValue,txtTestingNo.Text.Trim(),"5", resultPage, PAGE_SIZE, "Y");

                            if (resCount2.IsError)
                            {
                                divGv1.Visible = true;
                                txtNumberGvSearch.Visible = true;
                                lblParaPage.Visible = true;
                                txtTotalPage.Visible = true;

                                btngo.Visible = true;
                                lblTotalrecord.Text = "0";
                                txtTotalPage.Text = "1";
                                txtInputMaxrow.Visible = true;
                                lblHeadInputMaxrow.Visible = true;
                                lblHeadTotal.Visible = true;
                                lblTotalrecord.Visible = true;
                                lblEndTotal.Visible = true;
                                btnsumPrint.Visible = false;
                                btnExportExcel.Visible = false;
                                this.MasterSite.ModelError.ShowMessageError = "ไม่สามารถค้นหาข้อมูลได้";
                                this.MasterSite.ModelError.ShowModalError();
                            }
                            else
                            {
                                //div_totalR.Visible = true;
                                DataSet ds = resCount2.DataResponse;
                                DataTable dt = ds.Tables[0];
                                DataRow dr = dt.Rows[0];
                                int rowcount = Convert.ToInt32(dr["rowcount"].ToString());
                                double dblPageCount = (double)((decimal)rowcount / PAGE_SIZE);
                                TotalPages = (int)Math.Ceiling(dblPageCount);
                                txtTotalPage.Text = Convert.ToString(TotalPages);
                                var res = biz.GetSinglePayment(base.UserProfile.CompCode,
                                       Convert.ToDateTime(txtStartPaidSubDate.Text),
                                       Convert.ToDateTime(txtEndPaidSubDate.Text), txtPaidGroup.Text.Replace(" ", "").Trim(),StartD,EndD
                                            ,ddlLicenseType.SelectedValue,txtTestingNo.Text.Trim(), "5", resultPage, PAGE_SIZE, "N");
                                if (res.IsError)
                                {

                                    this.MasterSite.ModelError.ShowMessageError = "ไม่สามารถค้นหาข้อมูลได้";
                                    this.MasterSite.ModelError.ShowModalError();
                                }
                                else
                                {
                                    boxResult.Visible = true;
                                    gvSearch.Visible = true;
                                    gvSearch.DataSource = res.DataResponse;
                                    gvSearch.DataBind();

                                    if (TotalPages == 0)
                                    {
                                        divGv1.Visible = true;
                                        txtNumberGvSearch.Visible = true;
                                        lblParaPage.Visible = true;
                                        txtTotalPage.Visible = true;

                                        btngo.Visible = true;
                                        lblTotalrecord.Text = "0";
                                        txtTotalPage.Text = "1";
                                        txtInputMaxrow.Visible = true;
                                        lblHeadInputMaxrow.Visible = true;
                                        lblHeadTotal.Visible = true;
                                        lblTotalrecord.Visible = true;
                                        lblEndTotal.Visible = true;
                                        btnsumPrint.Visible = false;
                                        btnExportExcel.Visible = false;
                                        CheckBox ckall = (CheckBox)gvSearch.HeaderRow.FindControl("Checkall");
                                        ckall.Visible = false;
                                    }
                                    else if (TotalPages > 1)
                                    {
                                        txtNumberGvSearch.Visible = true;
                                        lblParaPage.Visible = true;
                                        txtTotalPage.Visible = true;
                                        btnNextGvSearch.Visible = true;
                                        divGv1.Visible = true;
                                        btngo.Visible = true;
                                        lblTotalrecord.Text = dr["rowcount"].ToString();
                                        txtInputMaxrow.Visible = true;
                                        lblHeadInputMaxrow.Visible = true;
                                        lblHeadTotal.Visible = true;
                                        lblTotalrecord.Visible = true;
                                        lblEndTotal.Visible = true;
                                        btnExportExcel.Visible = true;
                                        btnsumPrint.Visible = true;
                                    }
                                    else if (TotalPages == 1)
                                    {
                                        txtNumberGvSearch.Visible = true;
                                        lblParaPage.Visible = true;
                                        txtTotalPage.Visible = true;
                                        divGv1.Visible = true;
                                        btngo.Visible = true;
                                        lblTotalrecord.Text = dr["rowcount"].ToString();
                                        txtInputMaxrow.Visible = true;
                                        lblHeadInputMaxrow.Visible = true;
                                        lblHeadTotal.Visible = true;
                                        lblTotalrecord.Visible = true;
                                        lblEndTotal.Visible = true;
                                        btnExportExcel.Visible = true;
                                        btnsumPrint.Visible = true;
                                    }
                                    UpdatePanelSearch.Update();
                                }
                            }
                        }
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                this.MasterSite.ModelError.ShowMessageError = "ไม่สามารถค้นหาข้อมูลได้";
                this.MasterSite.ModelError.ShowModalError();
            }
        }
        protected void BindPage()
        {
            PAGE_SIZE = Convert.ToInt32(txtInputMaxrow.Text);
            var biz = new BLL.PaymentBiz();
            var resultPage = txtNumberGvSearch.Text.ToInt();
            if (base.UserProfile.MemberType == DTO.RegistrationType.General.GetEnumValue() 
                || base.UserProfile.MemberType ==DTO.RegistrationType.Insurance.GetEnumValue() 
                || base.UserProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue())
            {
                if (base.UserProfile.MemberType == DTO.RegistrationType.General.GetEnumValue())
                {
                    UserType = "CREATED_BY";
                    Type = base.UserId;
                }
                else if (base.UserProfile.MemberType == DTO.RegistrationType.Insurance.GetEnumValue() 
                    || base.UserProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue())
                {
                    UserType = "UPLOAD_BY_SESSION";
                    Type = base.UserProfile.CompCode;
                }
                DateTime? StartD = null;
                DateTime? EndD = null;
                if (txtStartExamDate.Text != "")
                {
                    StartD = Convert.ToDateTime(txtStartExamDate.Text);
                }
                if (txtEndExamDate.Text != "")
                {
                    EndD = Convert.ToDateTime(txtEndExamDate.Text);
                }
                var res = biz.GetSinglePayment(Type,
                                Convert.ToDateTime(txtStartPaidSubDate.Text),
                                Convert.ToDateTime(txtEndPaidSubDate.Text), txtPaidGroup.Text.Replace(" ", "").Trim(),
                                StartD,EndD
                                            ,ddlLicenseType.SelectedValue,txtTestingNo.Text.Trim(),UserType, resultPage, PAGE_SIZE, "N");
                if (res.IsError)
                {
                    //div_totalR.Visible = false;
                    btnNextGvSearch.Visible = false;
                    btnPreviousGvSearch.Visible = false;
                    txtNumberGvSearch.Visible = false;
                    lblParaPage.Visible = false;
                    txtTotalPage.Visible = false;
                    lblHeadInputMaxrow.Visible = false;
                    txtInputMaxrow.Visible = false;
                    btngo.Visible = false;

                    this.MasterSite.ModelError.ShowMessageError = "ไม่สามารถค้นหาข้อมูลได้";
                    this.MasterSite.ModelError.ShowModalError();
                }
                else
                {
                    //div_totalR.Visible = true;
                    boxResult.Visible = true;
                    gvSearch.Visible = true;
                    gvSearch.DataSource = res.DataResponse;
                    gvSearch.DataBind();
                }
            }
            else if ((base.UserProfile.MemberType == DTO.RegistrationType.OIC.GetEnumValue()) 
                || (base.UserProfile.MemberType == DTO.RegistrationType.OICFinace.GetEnumValue())
                || (base.UserProfile.MemberType == DTO.RegistrationType.OICAgent.GetEnumValue()))
            {
                DateTime? StartD = null;
                DateTime? EndD = null;
                if (txtStartExamDate.Text != "")
                {
                    StartD = Convert.ToDateTime(txtStartExamDate.Text);
                }
                if (txtEndExamDate.Text != "")
                {
                    EndD = Convert.ToDateTime(txtEndExamDate.Text);
                }
                var res = biz.GetSinglePayment(base.UserProfile.CompCode,
                          Convert.ToDateTime(txtStartPaidSubDate.Text),
                          Convert.ToDateTime(txtEndPaidSubDate.Text), txtPaidGroup.Text.Replace(" ", "").Trim(), StartD, EndD
                                            , ddlLicenseType.SelectedValue, txtTestingNo.Text.Trim(), "5", resultPage, PAGE_SIZE, "N");
                if (res.IsError)
                {
                    //div_totalR.Visible = false;
                    btnNextGvSearch.Visible = false;
                    btnPreviousGvSearch.Visible = false;
                    txtNumberGvSearch.Visible = false;
                    lblParaPage.Visible = false;
                    txtTotalPage.Visible = false;
                    lblHeadInputMaxrow.Visible = false;
                    txtInputMaxrow.Visible = false;
                    btngo.Visible = false;

                    this.MasterSite.ModelError.ShowMessageError = "ไม่สามารถค้นหาข้อมูลได้";
                    this.MasterSite.ModelError.ShowModalError();
                }
                else
                {
                    //div_totalR.Visible = true;
                    boxResult.Visible = true;
                    gvSearch.Visible = true;
                    gvSearch.DataSource = res.DataResponse;
                    gvSearch.DataBind();
                }
            }
        }

        protected void btnPreviousGvSearch_Click(object sender, EventArgs e)
        {
            //gv2.Visible = false;
            divGv2.Visible = false;
            var result = txtNumberGvSearch.Text.ToInt() - 1;

            txtNumberGvSearch.Text = result == 0 ? "1" : result.ToString();
            if (result.ToString() == "1")
            {
                txtNumberGvSearch.Visible = true;
                lblParaPage.Visible = true;
                txtTotalPage.Visible = true;
                btnNextGvSearch.Visible = true;
                btnPreviousGvSearch.Visible = false;

                btngo.Visible = true;

                txtInputMaxrow.Visible = true;
                lblHeadInputMaxrow.Visible = true;
                lblHeadTotal.Visible = true;
                lblTotalrecord.Visible = true;
                lblEndTotal.Visible = true;
            }
            else if (Convert.ToInt32(result) > 1)
            {
                txtNumberGvSearch.Visible = true;
                lblParaPage.Visible = true;
                txtTotalPage.Visible = true;
                btnNextGvSearch.Visible = true;
                btnPreviousGvSearch.Visible = true;

                btngo.Visible = true;
                txtInputMaxrow.Visible = true;
                lblHeadInputMaxrow.Visible = true;
                lblHeadTotal.Visible = true;
                lblTotalrecord.Visible = true;
                lblEndTotal.Visible = true;
            }
            BindPage();
        }

        protected void btnNextGvSearch_Click(object sender, EventArgs e)
        {
            //gv2.Visible = false;
            divGv2.Visible = false;

            var result = txtNumberGvSearch.Text.ToInt() + 1;
            if (Convert.ToInt32(result) < Convert.ToInt32(txtTotalPage.Text))
            {
                txtNumberGvSearch.Text = result.ToString();
                txtNumberGvSearch.Visible = true;
                lblParaPage.Visible = true;
                txtTotalPage.Visible = true;
                btnNextGvSearch.Visible = true;
                btnPreviousGvSearch.Visible = true;

                btngo.Visible = true;
                txtInputMaxrow.Visible = true;
                lblHeadInputMaxrow.Visible = true;
                lblHeadTotal.Visible = true;
                lblTotalrecord.Visible = true;
                lblEndTotal.Visible = true;

            }
            else
            {
                txtNumberGvSearch.Text = txtTotalPage.Text;
                txtNumberGvSearch.Visible = true;
                lblParaPage.Visible = true;
                txtTotalPage.Visible = true;
                btnNextGvSearch.Visible = false;
                btnPreviousGvSearch.Visible = true;

                btngo.Visible = true;
                txtInputMaxrow.Visible = true;
                lblHeadInputMaxrow.Visible = true;
                lblHeadTotal.Visible = true;
                lblTotalrecord.Visible = true;
                lblEndTotal.Visible = true;

            }
            BindPage();
        }

        #region GvSearchNon
        protected void btnPreviousGvSearchNon_Click(object sender, EventArgs e)
        {
            //gv2.Visible = false;
            divGv2.Visible = false;
            var result = txtNumberGvSearchNon.Text.ToInt() - 1;

            txtNumberGvSearchNon.Text = result == 0 ? "1" : result.ToString();
            if (txtNumberGvSearchNon.Text == "1")
            {
                txtNumberGvSearchNon.Visible = true;
                lblParaPageNon.Visible = true;
                txtTotalPageNon.Visible = true;
                btnNextGvSearchNon.Visible = true;
                btnPreviousGvSearchNon.Visible = false;

                btngoNon.Visible = true;

                txtInputMaxrowNon.Visible = true;
                lblHeadInputMaxrowNon.Visible = true;
                lblHeadTotalNon.Visible = true;
                lblTotalrecordNon.Visible = true;
                lblEndTotalNon.Visible = true;
            }
            else if (Convert.ToInt32(txtNumberGvSearchNon.Text) > 1)
            {
                txtNumberGvSearchNon.Visible = true;
                lblParaPageNon.Visible = true;
                txtTotalPageNon.Visible = true;
                btnNextGvSearchNon.Visible = true;
                btnPreviousGvSearchNon.Visible = true;

                btngoNon.Visible = true;
                txtInputMaxrowNon.Visible = true;
                lblHeadInputMaxrowNon.Visible = true;
                lblHeadTotalNon.Visible = true;
                lblTotalrecordNon.Visible = true;
                lblEndTotalNon.Visible = true;
            }
            BindPageNon();
        }
        protected void btnNextGvSearchNon_Click(object sender, EventArgs e)
        {
            //gv2.Visible = false;
            divGv2.Visible = false;

            var result = txtNumberGvSearchNon.Text.ToInt() + 1;
            if (Convert.ToInt32(result) < Convert.ToInt32(txtTotalPageNon.Text))
            {
                txtNumberGvSearchNon.Text = result.ToString();
                txtNumberGvSearchNon.Visible = true;
                lblParaPageNon.Visible = true;
                txtTotalPageNon.Visible = true;
                btnNextGvSearchNon.Visible = true;
                btnPreviousGvSearchNon.Visible = true;

                btngoNon.Visible = true;
                txtInputMaxrowNon.Visible = true;
                lblHeadInputMaxrowNon.Visible = true;
                lblHeadTotalNon.Visible = true;
                lblTotalrecordNon.Visible = true;
                lblEndTotalNon.Visible = true;

            }
            else
            {
                txtNumberGvSearchNon.Text = txtTotalPageNon.Text;
                txtNumberGvSearchNon.Visible = true;
                lblParaPageNon.Visible = true;
                txtTotalPageNon.Visible = true;
                btnNextGvSearchNon.Visible = false;
                btnPreviousGvSearchNon.Visible = true;

                btngoNon.Visible = true;
                txtInputMaxrowNon.Visible = true;
                lblHeadInputMaxrowNon.Visible = true;
                lblHeadTotalNon.Visible = true;
                lblTotalrecordNon.Visible = true;
                lblEndTotalNon.Visible = true;

            }
            BindPageNon();
        }
        protected void BindPageNon()
        {
            PAGE_SIZE = Convert.ToInt32(txtInputMaxrowNon.Text);
            var biz = new BLL.PaymentBiz();
            var resultPage = txtNumberGvSearchNon.Text.ToInt();
            if (base.UserProfile.MemberType == DTO.RegistrationType.General.GetEnumValue() ||
                base.UserProfile.MemberType == DTO.RegistrationType.Insurance.GetEnumValue() ||
                base.UserProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue())
            {
                if (base.UserProfile.MemberType == DTO.RegistrationType.General.GetEnumValue())
                {
                    UserTypeNon = "dt.id_card_no";
                    TypeNon = base.IdCard;
                }

                else if (base.UserProfile.MemberType == DTO.RegistrationType.Insurance.GetEnumValue())
                {
                    UserTypeNon = "Insurance";
                    TypeNon = base.UserProfile.CompCode;
                }
                else if (base.UserProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue())
                {
                    UserTypeNon = "Association";
                    TypeNon = base.UserProfile.CompCode;
                }
                DateTime? StartD = null;
                DateTime? EndD = null;
                if (txtStartExamDate.Text != "")
                {
                    StartD = Convert.ToDateTime(txtStartExamDate.Text);
                }
                if (txtEndExamDate.Text != "")
                {
                    EndD = Convert.ToDateTime(txtEndExamDate.Text);
                }
                var res = biz.GetNonPayment(TypeNon,
                                Convert.ToDateTime(txtStartPaidSubDate.Text),
                                Convert.ToDateTime(txtEndPaidSubDate.Text), txtPaidGroup.Text.Replace(" ", "").Trim(), StartD, EndD, ddlLicenseType.SelectedValue, txtTestingNo.Text.Trim(), UserTypeNon, resultPage, PAGE_SIZE, "N");
                if (res.IsError)
                {
                    //div_totalR.Visible = false;
                    btnNextGvSearchNon.Visible = false;
                    btnPreviousGvSearchNon.Visible = false;
                    txtNumberGvSearchNon.Visible = false;
                    lblParaPageNon.Visible = false;
                    txtTotalPageNon.Visible = false;
                    lblHeadInputMaxrowNon.Visible = false;
                    txtInputMaxrowNon.Visible = false;
                    btngoNon.Visible = false;

                    this.MasterSite.ModelError.ShowMessageError = "ไม่สามารถค้นหาข้อมูลได้";
                    this.MasterSite.ModelError.ShowModalError();
                }
                else
                {
                    //div_totalR.Visible = true;
                    boxResult.Visible = true;
                    GVSearchN.Visible = true;
                    GVSearchN.DataSource = res.DataResponse;
                    GVSearchN.DataBind();
                }
            }
        }
        #endregion

        protected void BindPage2()
        {
            PAGE_SIZE = Convert.ToInt32(txtInputMaxrow2.Text);
            var biz = new BLL.PaymentBiz();
            var resultPage = txtNumberGvSearch2.Text.ToInt();
            var res = biz.GetSubPaymentByHeaderRequestNo(GroupRequestNoValue.Replace(" ", ""), "N", resultPage, PAGE_SIZE);
            gvSubDetail.DataSource = res.DataResponse;
            gvSubDetail.DataBind();
        }

        protected void btnPreviousGvSearch2_Click(object sender, EventArgs e)
        {
            //gv3.Visible = false;
            //divGv3.Visible = false;
            var result = txtNumberGvSearch2.Text.ToInt() - 1;

            txtNumberGvSearch2.Text = result == 0 ? "1" : result.ToString();
            if (txtNumberGvSearch2.Text == "1")
            {
                txtNumberGvSearch2.Visible = true;
                lblParaPage2.Visible = true;
                txtTotalPage2.Visible = true;
                btnNextGvSearch2.Visible = true;
                btnPreviousGvSearch2.Visible = false;

                btngo2.Visible = true;
                txtInputMaxrow2.Visible = true;
                lblHeadInputMaxrow2.Visible = true;
                lblHeadTotal2.Visible = true;
                lblTotalrecord2.Visible = true;
                lblEndTotal2.Visible = true;
            }
            else if (Convert.ToInt32(txtNumberGvSearch2.Text) > 1)
            {
                txtNumberGvSearch2.Visible = true;
                lblParaPage2.Visible = true;
                txtTotalPage2.Visible = true;
                btnNextGvSearch2.Visible = true;
                btnPreviousGvSearch2.Visible = true;

                btngo2.Visible = true;
                txtInputMaxrow2.Visible = true;
                lblHeadInputMaxrow2.Visible = true;
                lblHeadTotal2.Visible = true;
                lblTotalrecord2.Visible = true;
                lblEndTotal2.Visible = true;
            }
            BindPage2();
        }

        protected void btnNextGvSearch2_Click(object sender, EventArgs e)
        {
            //gv3.Visible = false;
            //divGv3.Visible = false;
            var result = txtNumberGvSearch2.Text.ToInt() + 1;
            if (Convert.ToInt32(result) < Convert.ToInt32(txtTotalPage2.Text))
            {
                txtNumberGvSearch2.Text = result.ToString();
                txtNumberGvSearch2.Visible = true;
                lblParaPage2.Visible = true;
                txtTotalPage2.Visible = true;
                btnNextGvSearch2.Visible = true;
                btnPreviousGvSearch2.Visible = true;

                btngo2.Visible = true;
                txtInputMaxrow2.Visible = true;
                lblHeadInputMaxrow2.Visible = true;
                lblHeadTotal2.Visible = true;
                lblTotalrecord2.Visible = true;
                lblEndTotal2.Visible = true;
            }
            else
            {
                txtNumberGvSearch2.Text = txtTotalPage2.Text;
                txtNumberGvSearch2.Visible = true;
                lblParaPage2.Visible = true;
                txtTotalPage2.Visible = true;
                btnNextGvSearch2.Visible = false;
                btnPreviousGvSearch2.Visible = true;

                btngo2.Visible = true;
                txtInputMaxrow2.Visible = true;
                lblHeadInputMaxrow2.Visible = true;
                lblHeadTotal2.Visible = true;
                lblTotalrecord2.Visible = true;
                lblEndTotal2.Visible = true;
            }
            BindPage2();
        }


        protected void BindPage3()
        {
            PAGE_SIZE = Convert.ToInt32(txtInputMaxrow3.Text);
            var biz = new BLL.PaymentBiz();
            var resultPage = txtNumberGvSearch3.Text.ToInt();
            var res = biz.GetSubPaymentByHeaderRequestNo(Non_GroupRequestNoValue.Value.Replace(" ", ""), "N", resultPage, PAGE_SIZE);
            GVSubNonPayment.DataSource = res.DataResponse;
            GVSubNonPayment.DataBind();
        }

        protected void btnPreviousGvSearch3_Click(object sender, EventArgs e)
        {

            var result = txtNumberGvSearch3.Text.ToInt() - 1;

            txtNumberGvSearch3.Text = result == 0 ? "1" : result.ToString();
            if (txtNumberGvSearch3.Text == "1")
            {
                txtNumberGvSearch3.Visible = true;
                lblParaPage3.Visible = true;
                txtTotalPage3.Visible = true;
                btnNextGvSearch3.Visible = true;
                btnPreviousGvSearch3.Visible = false;

                //divGv3.Visible = true;
                btngo3.Visible = true;
                txtInputMaxrow3.Visible = true;
                lblHeadInputMaxrow3.Visible = true;
                lblHeadTotal3.Visible = true;
                lblTotalrecord3.Visible = true;
                lblEndTotal3.Visible = true;
            }
            else if (Convert.ToInt32(txtNumberGvSearch3.Text) > 1)
            {
                txtNumberGvSearch3.Visible = true;
                lblParaPage3.Visible = true;
                txtTotalPage3.Visible = true;
                btnNextGvSearch3.Visible = true;
                btnPreviousGvSearch3.Visible = true;

                //divGv3.Visible = true;
                btngo3.Visible = true;
                txtInputMaxrow3.Visible = true;
                lblHeadInputMaxrow3.Visible = true;
                lblHeadTotal3.Visible = true;
                lblTotalrecord3.Visible = true;
                lblEndTotal3.Visible = true;
            }
            BindPage3();
        }

        protected void btnNextGvSearch3_Click(object sender, EventArgs e)
        {

            var result = txtNumberGvSearch3.Text.ToInt() + 1;
            if (Convert.ToInt32(result) < Convert.ToInt32(txtTotalPage3.Text))
            {
                txtNumberGvSearch3.Text = result.ToString();
                txtNumberGvSearch3.Visible = true;
                lblParaPage3.Visible = true;
                txtTotalPage3.Visible = true;
                btnNextGvSearch3.Visible = true;
                btnPreviousGvSearch3.Visible = true;

               // divGv3.Visible = true;
                btngo3.Visible = true;
                txtInputMaxrow3.Visible = true;
                lblHeadInputMaxrow3.Visible = true;
                lblHeadTotal3.Visible = true;
                lblTotalrecord3.Visible = true;
                lblEndTotal3.Visible = true;
            }
            else
            {
                txtNumberGvSearch3.Text = txtTotalPage3.Text;
                txtNumberGvSearch3.Visible = true;
                lblParaPage3.Visible = true;
                txtTotalPage3.Visible = true;
                btnNextGvSearch3.Visible = false;
                btnPreviousGvSearch3.Visible = true;

                //divGv3.Visible = true;
                btngo3.Visible = true;
                txtInputMaxrow3.Visible = true;
                lblHeadInputMaxrow3.Visible = true;
                lblHeadTotal3.Visible = true;
                lblTotalrecord3.Visible = true;
                lblEndTotal3.Visible = true;

            }
            BindPage3();
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
                LinkButton buttonD = (LinkButton)e.Row.FindControl("hplDelete");
                Label ChkPrintPayment = (Label)e.Row.FindControl("lblLastPrint");
                LinkButton buttonP = (LinkButton)e.Row.FindControl("hplPrint");
                CheckBox checkselect = (CheckBox)e.Row.FindControl("chkSelectPayment");
                CheckBox chk_all = (CheckBox)gvSearch.HeaderRow.FindControl("Checkall");
                if (base.UserProfile.MemberType == DTO.RegistrationType.OIC.GetEnumValue() 
                    || base.UserProfile.MemberType == DTO.RegistrationType.OICFinace.GetEnumValue()
                    || base.UserProfile.MemberType == DTO.RegistrationType.OICAgent.GetEnumValue() 
                    )
                {
                    buttonD.Visible = false;
                    if (ChkPrintPayment.Text == "")
                    {
                        buttonP.Visible = false;
                    }
                }
              
                
                if (ChkPrintPayment.Text != "")
                {
                    buttonD.Visible = false;
                }
                if (buttonP.Visible == false)
                {
                    checkselect.Enabled = false;
                }
                else
                {
                    countCheckDetail++;
                }
                if (countCheckDetail > 0)
                {
                    chk_all.Enabled = true;
                }
                else
                {
                    chk_all.Enabled = false;
                }
         
                Label Gno = (Label)e.Row.FindControl("lblGroupRequsetNo");
              
                string desc = DataBinder.Eval(e.Row.DataItem, "GROUP_REQUEST_NO").ToString();
                Gno.Text = Gno.Text.Insert(6, " ").Insert(11, " ");
                Label Upload = (Label)e.Row.FindControl("lblUploadBySession");
                Label Status = (Label)e.Row.FindControl("lblStatus");
                Label StatusText = (Label)e.Row.FindControl("lblStatusText");
                Label PaymentDate = (Label)e.Row.FindControl("lblReceiptDatePayGv");
                Label ExpireDate = (Label)e.Row.FindControl("lblEXPIRATION_DATE");
                if (ExpireDate.Text == "")
                {
                    StatusText.Text = "รอชำระเงิน";
                    checkselect.Enabled = false;
                    buttonP.Visible = false;
                }
                else if (Status.Text == "S" && ExpireDate.Text != "") 
                {
                    StatusText.Text = "จ่ายบางส่วนยังไม่เต็มจำนวน";
                }
                else
                {

                    if (PaymentDate.Text != "")
                    {
                        if ((Status.Text == "P") && ((Convert.ToDateTime(PaymentDate.Text) < Convert.ToDateTime(ExpireDate.Text)) || (Convert.ToDateTime(PaymentDate.Text) == Convert.ToDateTime(ExpireDate.Text))))
                        {
                            StatusText.Text = "ชำระเงินแล้ว";
                        }
                        else if ((Status.Text == "P") && (Convert.ToDateTime(PaymentDate.Text) > Convert.ToDateTime(ExpireDate.Text)))
                        {
                            StatusText.Text = "ชำระเงินล่าช้า";
                        }
                    }
                    else if ((PaymentDate.Text == "") && (Convert.ToDateTime(ExpireDate.Text) < DateTime.Now))
                    {
                        StatusText.Text = "ยังไม่ได้ชำระเงิน";
                    }
                    else if ((PaymentDate.Text == "") && (DateTime.Now < Convert.ToDateTime(ExpireDate.Text)))
                    {
                        StatusText.Text = "รอชำระเงิน";
                    }
                }
                buttonP.Attributes.Add("onclick", string.Format("OpenPopupSingle('{0}')", Gno.Text.Replace(" ", "") + " " + Upload.Text));
                if (ListPayment.Count == 0)
                {
                    b_check = false;
                    if (buttonP.Visible == false)
                    {
                        checkselect.Enabled = false;//ถ้ารูปปริ้นซ่อนให้CheckPaymentติ๊กไม่ได้
                    }
                    else
                    {
                        checkselect.Enabled = true;
                    }
                }
                else
                {
                    var l = ListPayment.FirstOrDefault(x => x == Gno.Text.Replace(" ", ""));
                    if (l != null)
                    {
                        checkselect.Checked = true;
                    }
                    else
                    {
                        checkselect.Checked = false;
                       // b_check = false;
                    }
                 
                }
               
         
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                var HeaderAll = ListChkAll.FirstOrDefault(a => a ==  txtNumberGvSearch.Text);
                if (HeaderAll != null)
                {
                    check_all_head.Checked = true;
                }
            }

        }
        protected void gvSearchNon_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label Gno = (Label)e.Row.FindControl("lblGroupRequsetNo");
            
                
                Gno.Text = Gno.Text.Insert(6, " ").Insert(11, " ");

                Label Status = (Label)e.Row.FindControl("lblstatus");
               
                Label PaymentDate = (Label)e.Row.FindControl("lblReceiptDatePayGv");
                Label ExpireDate = (Label)e.Row.FindControl("lblEXPIRATION_DATE");

                if (ExpireDate.Text == "")
                {
                    Status.Text = "รอชำระเงิน";
                }
                else if (Status.Text == "S" && ExpireDate.Text != "")
                {
                    Status.Text = "จ่ายบางส่วนยังไม่เต็มจำนวน";
                }
                else
                {
                    if (PaymentDate.Text != "")
                    {
                        if ((Status.Text == "P") && ((Convert.ToDateTime(PaymentDate.Text) < Convert.ToDateTime(ExpireDate.Text)) || (Convert.ToDateTime(PaymentDate.Text) == Convert.ToDateTime(ExpireDate.Text))))
                        {
                            Status.Text = "ชำระเงินแล้ว";
                        }
                        else if ((Status.Text == "P") && (Convert.ToDateTime(PaymentDate.Text) > Convert.ToDateTime(ExpireDate.Text)))
                        {
                            Status.Text = "ชำระเงินล่าช้า";
                        }
                    }
                    else if ((PaymentDate.Text == "") && (Convert.ToDateTime(ExpireDate.Text) < DateTime.Now))
                    {
                        Status.Text = "ยังไม่ได้ชำระเงิน";
                    }
                    else if ((PaymentDate.Text == "") && (DateTime.Now < Convert.ToDateTime(ExpireDate.Text)))
                    {
                        Status.Text = "รอชำระเงิน";
                    }
                }
            }
          

        }
        protected void chkSelectPayment_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkSelectPayment = (CheckBox)sender;
            GridViewRow gr = (GridViewRow)chkSelectPayment.Parent.Parent;
            Label Gno = (Label)gr.FindControl("lblGroupRequsetNo");
            Label Upload = (Label)gr.FindControl("lblUploadBySession");
            if (chkSelectPayment.Checked)
            {
                ListPayment.Add(Gno.Text.Replace(" ", "") + " " + Upload.Text);
            }
            else
            {
                ListPayment.Remove(Gno.Text.Replace(" ", "") + " " + Upload.Text);
            }

        }

        protected void Checkall_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ck = (CheckBox)sender;
            ListChkAll.Add(txtNumberGvSearch.Text);
            foreach (GridViewRow row in gvSearch.Rows)
            {
                Label Gno = (Label)row.FindControl("lblGroupRequsetNo");
                CheckBox chkSelectPayment = (CheckBox)row.FindControl("chkSelectPayment");
                Label Upload = (Label)row.FindControl("lblUploadBySession");

                if (ck.Checked)
                {
                   
                    if ((!chkSelectPayment.Checked)&&(chkSelectPayment.Enabled))
                    {
                        ListPayment.Add(Gno.Text.Replace(" ", "") + " " + Upload.Text);
                        chkSelectPayment.Checked = true;
                    }
                    
                }
                else
                {
                    ListChkAll.Remove(txtNumberGvSearch.Text);
                    if ((chkSelectPayment.Checked)&&(chkSelectPayment.Enabled))
                    {
                        ListPayment.Remove(Gno.Text.Replace(" ", "") + " " + Upload.Text);
                        chkSelectPayment.Checked = false;
                    }
                 
                }
            }
        }
        protected void btnsumPrint_Click(object sender, EventArgs e)
        {
        
            if (this.ListPayment.ToList().Count == 0)
            {
                this.MasterSite.ModelError.ShowMessageError = Resources.errorInvoice5_001;
                this.MasterSite.ModelError.ShowModalError();
            }
            else
            {
                var biz = new BLL.PaymentBiz();
                var res = biz.UpdatePrintGroupRequestNo(ListPayment.ToArray());
                BindDataInGridView();
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", string.Format("OpenPopup('{0}')", this.ListPayment.ToList()), true);
                //btnsumPrint.Attributes.Add("onclick", string.Format("OpenPopup('{0}')", this.ListPayment.ToList()));
            }
        }
        ExportBiz export = new ExportBiz();
        protected void btnExportExcel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {

                int total = lblTotalrecord.Text == "" ? 0 : lblTotalrecord.Text.ToInt();
                if (total > base.EXCEL_SIZE_Key)
                {
                    this.MasterSite.ModelError.ShowMessageError = SysMessage.ExcelSizeError;
                    this.MasterSite.ModelError.ShowModalError();
                }
                else
                {
                    Dictionary<string, string> columns = new Dictionary<string, string>();
                    columns.Add("ลำดับที่", "RUN_NO");
                    columns.Add("เลขที่ใบสั่งจ่ายกลุ่ม", "GROUP_REQUEST_NO");
                    columns.Add("วันที่สร้างใบสั่งจ่ายกลุ่ม", "GROUP_DATE");
                    columns.Add("วันที่รับชำระเงิน", "PAYMENT_DATE");
                    columns.Add("จำนวนเงิน", "GROUP_AMOUNT");
                    columns.Add("จำนวนใบสั่งจ่ายย่อย", "PERSON_NO");
                    //columns.Add("สถานะการจ่ายเงิน", "STATUS");
                    columns.Add("หมายเหตุ", "REMARK");

                    List<HeaderExcel> header = new List<HeaderExcel>();
                    header.Add(new HeaderExcel
                    {
                        NameColumnsOne = "วันที่สร้างใบสั่งจ่าย(เริ่ม) ",
                        ValueColumnsOne = txtStartPaidSubDate.Text,
                        NameColumnsTwo = "วันที่สร้างใบสั่งจ่าย(สิ้นสุด) ",
                        ValueColumnsTwo = txtEndPaidSubDate.Text
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
                        NameColumnsOne = "เลขที่ใบสั่งจ่ายกลุ่ม ",
                        ValueColumnsOne = txtPaidGroup.Text,
                        NameColumnsTwo = "ประเภทใบอนุญาติ ",
                        ValueColumnsTwo = ddlLicenseType.SelectedItem.Text
                    });

                    header.Add(new HeaderExcel
                    {
                        NameColumnsOne = "รหัสสนามสอบ ",
                        ValueColumnsOne = txtTestingNo.Text
                    });

                    var biz = new BLL.PaymentBiz();
                    if (base.UserProfile.MemberType == DTO.RegistrationType.General.GetEnumValue())
                    {
                        UserType = "CREATED_BY";
                        Type = base.UserId;
                    }
                    else if (base.UserProfile.MemberType == DTO.RegistrationType.Insurance.GetEnumValue()
                        || base.UserProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue())
                    {
                        UserType = "UPLOAD_BY_SESSION";
                        Type = base.UserProfile.CompCode;
                    }

                    if (base.UserProfile.MemberType == DTO.RegistrationType.General.GetEnumValue() 
                        || base.UserProfile.MemberType == DTO.RegistrationType.Insurance.GetEnumValue() 
                        || base.UserProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue())
                    {
                        var res = biz.GetSinglePayment(Type,
                                    Convert.ToDateTime(txtStartPaidSubDate.Text),
                                    Convert.ToDateTime(txtEndPaidSubDate.Text), txtPaidGroup.Text.Replace(" ", "").Trim(),
                                    Convert.ToDateTime(txtStartExamDate.Text),Convert.ToDateTime(txtEndExamDate.Text)
                                            ,ddlLicenseType.SelectedValue,txtTestingNo.Text.Trim(),UserType, 1, base.EXCEL_SIZE_Key, "N");
                        if (res.IsError)
                        {

                            this.MasterSite.ModelError.ShowMessageError = res.ErrorMsg;
                            this.MasterSite.ModelError.ShowModalError();
                        }
                        else
                        {
                            export.CreateExcelInvoice5(res.DataResponse.Tables[0], columns, header, base.UserProfile);
                        }
                    }
                    else if ((base.UserProfile.MemberType == DTO.RegistrationType.OIC.GetEnumValue()) 
                        || (base.UserProfile.MemberType == DTO.RegistrationType.OICAgent.GetEnumValue())
                        || (base.UserProfile.MemberType == DTO.RegistrationType.OICFinace.GetEnumValue()))
                    {
                        //var res = biz.GetSinglePayment(base.UserProfile.CompCode,
                        //       Convert.ToDateTime(txtStartPaidSubDate.Text),
                        //       Convert.ToDateTime(txtEndPaidSubDate.Text), txtPaidGroup.Text.Replace(" ", "").Trim(),
                        //       Convert.ToDateTime(txtStartExamDate.Text),Convert.ToDateTime(txtEndExamDate.Text)
                        //                    ,ddlLicenseType.SelectedValue,txtTestingNo.Text.Trim(),"5", 1, base.EXCEL_SIZE_Key, "N");

                        DateTime? StartD = null;
                        DateTime? EndD = null;
                        if (txtStartExamDate.Text != "")
                        {
                            StartD = Convert.ToDateTime(txtStartExamDate.Text);
                        }
                        if (txtEndExamDate.Text != "")
                        {
                            EndD = Convert.ToDateTime(txtEndExamDate.Text);
                        }
                        var res = biz.GetSinglePayment(base.UserProfile.CompCode,
                      Convert.ToDateTime(txtStartPaidSubDate.Text),
                      Convert.ToDateTime(txtEndPaidSubDate.Text), txtPaidGroup.Text.Replace(" ", "").Trim(),
                      StartD, EndD
                                   , ddlLicenseType.SelectedValue, txtTestingNo.Text.Trim(), "5", 1, base.EXCEL_SIZE_Key, "N");

                        if (res.IsError)
                        {

                            this.MasterSite.ModelError.ShowMessageError = res.ErrorMsg;
                            this.MasterSite.ModelError.ShowModalError();
                        }
                        else
                        {
                            export.CreateExcelInvoice5(res.DataResponse.Tables[0], columns, header, base.UserProfile);
                        }
                    }
                }
            }
            catch { }

        }
        public override void VerifyRenderingInServerForm(Control control) { }

        protected void btnExportExcel2_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                int total = lblTotalrecord2.Text == "" ? 0 : lblTotalrecord2.Text.ToInt();
                if (total > base.EXCEL_SIZE_Key)
                {
                    this.MasterSite.ModelError.ShowMessageError = SysMessage.ExcelSizeError;
                    this.MasterSite.ModelError.ShowModalError();
                }
                else
                {
                    Dictionary<string, string> columns = new Dictionary<string, string>();
                    columns.Add("ลำดับที่", "RUN_NO");
                    columns.Add("เลขที่ใบสั่งจ่ายย่อย", "head_request_no");
                    columns.Add("ประเภทใบสั่งจ่าย", "petition_type_name");
                    columns.Add("ชื่อ-นามสกุล", "FIRSTLASTNAME");
                    columns.Add("วันที่ออกใบสั่งจ่าย", "SUBPAYMENT_DATE");
                    columns.Add("วันที่สร้างใบสั่งจ่ายย่อย", "created_date");
                   // columns.Add("จำนวนคน", "PERSON_NO");
                     columns.Add("จำนวนเงิน", "amount");
                    var biz = new BLL.PaymentBiz();
                  //  var res = biz.GetSubPaymentByHeaderRequestNo(H_GroupRequestNoValue.Value.Replace(" ", ""), "N", 1, base.EXCEL_SIZE_Key);

                    var res = biz.GetSubPaymentByHeaderRequestNo(GroupRequestNoValue.Replace(" ", ""), "N", 1, base.EXCEL_SIZE_Key);
                    try
                    {
                        export.CreateExcel(res.DataResponse.Tables[0], columns,base.UserProfile);
                    }
                    catch (Exception ex) { }
                }
            }
            catch { }

        }

        //protected void btnExportExcel3_Click(object sender, ImageClickEventArgs e)
        //{
        //    try
        //    {
        //        int total = lblTotalrecord3.Text == "" ? 0 : lblTotalrecord3.Text.ToInt();
        //        if (total > base.EXCEL_SIZE_Key)
        //        {
        //            UCModalError.ShowMessageError = SysMessage.ExcelSizeError;
        //            UCModalError.ShowModalError();
        //        }
        //        else
        //        {
        //            Dictionary<string, string> columns = new Dictionary<string, string>();
        //            columns.Add("ลำดับที่", "RUN_NO");
        //            columns.Add("เลขบัตรประชาชน", "ID_CARD_NO");
        //            columns.Add("ชื่อ-นามสกุล", "FIRSTLASTNAME");
        //            columns.Add("ประเภทใบสั่งจ่าย", "PETITION_TYPE_NAME");
        //            columns.Add("จำนวนเงิน", "AMOUNT");
        //            var biz = new BLL.PaymentBiz();
        //            var res = biz.GetDetailSubPayment(H_HeadRequestNoValue.Value, 1, base.EXCEL_SIZE_Key, "N");
        //            export.CreateExcel(res.DataResponse, columns);
        //        }
        //    }
        //    catch { }

        //}

        protected void btnMainCancle_Click(object sender, EventArgs e)
        {
            defaultData();
            divGv1.Visible = false;
            divGv2.Visible = false;
            ListChkAll.Clear();
            //divGv3.Visible = false;
        }
        protected void defaultData()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");
            txtStartPaidSubDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
            txtStartPaidSubDate.Text = DateUtil.dd_MM_yyyy_Now_TH;
            txtEndPaidSubDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
            txtEndPaidSubDate.Text = DateUtil.dd_MM_yyyy_Now_TH;
            txtStartExamDate.Text = "";
            //txtStartExamDate.Text = DateUtil.dd_MM_yyyy_Now_TH;
            txtEndExamDate.Text = "";
            //txtEndExamDate.Text = DateUtil.dd_MM_yyyy_Now_TH;
            txtPaidGroup.Text = string.Empty;
            txtTestingNo.Text = string.Empty;
            divGv1.Visible = false;
            divGv2.Visible = false;
            //divGv3.Visible = false;
            divNon.Visible = false;
            btnExportExcel.Visible = false;
            txtInputMaxrow.Text = PAGE_SIZE_Key.ToString();
            txtInputMaxrowNon.Text = PAGE_SIZE_Key.ToString();
            txtInputMaxrow2.Text = PAGE_SIZE_Key.ToString();
           txtInputMaxrow3.Text = PAGE_SIZE_Key.ToString();
            GetLicenseType();
          
        }
        private void GetLicenseType()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetLicenseType(SysMessage.DefaultSelecting);
            BindToDDL(ddlLicenseType, ls.DataResponse);
        }
        private Action<DropDownList, DTO.DataItem[]> BindToDDL = (ddl, ls) =>
        {
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Id";
            ddl.DataSource = ls;
            ddl.DataBind();
        };

        protected void btnExport3_Click(object sender, EventArgs e)
        {
            DateTime? StartD = null;
            DateTime? EndD = null;
            if (txtStartExamDate.Text != "")
            {
                StartD = Convert.ToDateTime(txtStartExamDate.Text);
            }
            if (txtEndExamDate.Text != "")
            {
                EndD = Convert.ToDateTime(txtEndExamDate.Text);
            }
            if (base.UserProfile.MemberType == DTO.RegistrationType.General.GetEnumValue())
            {
                UserTypeNon = "dt.id_card_no";
                TypeNon = base.IdCard;
            }

            else if (base.UserProfile.MemberType == DTO.RegistrationType.Insurance.GetEnumValue())
            {
                UserTypeNon = "Insurance";
                TypeNon = base.UserProfile.CompCode;
            }
            else if (base.UserProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue())
            {
                UserTypeNon = "Association";
                TypeNon = base.UserProfile.CompCode;
            }
            var biz = new BLL.PaymentBiz();
            var resNon = biz.GetNonPayment(TypeNon,
                                            Convert.ToDateTime(txtStartPaidSubDate.Text),
                                            Convert.ToDateTime(txtEndPaidSubDate.Text), txtPaidGroup.Text.Replace(" ", "").Trim(), StartD, EndD, ddlLicenseType.SelectedValue, txtTestingNo.Text.Trim(), UserTypeNon, 1, base.EXCEL_SIZE_Key, "N");
            Dictionary<string, string> columns = new Dictionary<string, string>();
            columns.Add("ลำดับที่", "RUN_NO");
            columns.Add("เลขที่ใบสั่งจ่ายกลุ่ม", "GROUP_REQUEST_NO");
            columns.Add("วันที่สร้างใบสั่งจ่ายกลุ่ม", "GROUP_DATE");
            columns.Add("วันที่รับชำระเงิน", "PAYMENT_DATE");
            columns.Add("สถานะการจ่ายเงิน", "status");
            List<HeaderExcel> header = new List<HeaderExcel>();
            export.CreateExcelInvoice5(resNon.DataResponse.Tables[0], columns,header,base.UserProfile);
        }

        protected void btnExport4_Click(object sender, EventArgs e)
        {
            var biz = new BLL.PaymentBiz();
            var res = biz.GetSubPaymentByHeaderRequestNo(Non_GroupRequestNoValue.Value.Replace(" ", ""), "N", 1, base.EXCEL_SIZE_Key);
            Dictionary<string, string> columns = new Dictionary<string, string>();
            columns.Add("ลำดับที่", "RUN_NO");
            columns.Add("เลขที่ใบสั่งจ่ายย่อย", "head_request_no");
            columns.Add("ประเภทใบสั่งจ่าย", "petition_type_name");
            columns.Add("เลขบัตรประชาชน", "ID_CARD_NO");
            columns.Add("ชื่อ-นามสกุล", "FIRSTLASTNAME");
            columns.Add("จำนวนเงิน", "amount");
            columns.Add("วันที่ออกใบสั่งจ่าย", "SUBPAYMENT_DATE");
            columns.Add("วันที่สร้างใบสั่งจ่ายย่อย", "created_date");
            export.CreateExcel(res.DataResponse.Tables[0], columns, base.UserProfile);
        }
    }
}
