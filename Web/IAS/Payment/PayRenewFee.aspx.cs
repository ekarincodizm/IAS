using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using IAS.Utils;
using System.Threading;
using IAS.Properties;

namespace IAS.Payment
{
    public partial class PayRenewFee : basepage
    {
        public IEnumerable<DTO.SummaryBankTransaction> GvImportFileSource
        {
            get { return (Session["gvImportFileSource"] != null) ? (IEnumerable<DTO.SummaryBankTransaction>)Session["gvImportFileSource"] : new List<DTO.SummaryBankTransaction>(); }
        }
        public IEnumerable<DTO.BankTransaction> GvCheckListSource
        {
            get { return (Session["gvCheckListSource"] != null) ? (IEnumerable<DTO.BankTransaction>)Session["gvCheckListSource"] : new List<DTO.BankTransaction>(); }
        }
        public DTO.SummaryBankTransaction SummaryUploadSource
        {
            get
            {
                //return (Session["SummaryUploadSource"] != null) ? (DTO.SummaryBankTransaction)Session["SummaryUploadSource"] : new DTO.SummaryBankTransaction(); 
                return GvImportFileSource.ElementAtOrDefault(0);
            }
        }

        public IEnumerable<DTO.BankTransaction> GvPaymentMissingSource
        {
            get
            {
                //return (Session["gvPaymentMissingSource"]!=null) ?  (IEnumerable<DTO.BankTransaction>)Session["gvPaymentMissingSource"]: new List<DTO.BankTransaction>();
                return (GvCheckListSource != null) ? (GvCheckListSource.Where(a => a.Status == 2) != null) ? GvCheckListSource.Where(a => a.Status == 2) : new List<DTO.BankTransaction>() : new List<DTO.BankTransaction>();

            }
        }
        public IEnumerable<DTO.BankTransaction> GvPaymentLateSource
        {
            get
            {
                return (GvCheckListSource != null) ? (GvCheckListSource.Where(a => a.Status == 5) != null) ? GvCheckListSource.Where(a => a.Status == 5) : new List<DTO.BankTransaction>() : new List<DTO.BankTransaction>();

            }
        }
        public IEnumerable<DTO.BankTransaction> GvPaymentInvalidSource
        {
            get
            {
                //return (Session["gvPaymentInvalidSource"] != null) ? (IEnumerable<DTO.BankTransaction>)Session["gvPaymentInvalidSource"] : new List<DTO.BankTransaction>();
                return (GvCheckListSource != null) ? (GvCheckListSource.Where(a => a.Status == 6 || a.Status == 3 || a.Status == 3 || a.Status == 4) != null) ? GvCheckListSource.Where(a => a.Status == 6 || a.Status == 3 || a.Status == 3 || a.Status == 4) : new List<DTO.BankTransaction>() : new List<DTO.BankTransaction>();

            }
        }

        public void ClearCollection()
        {
            Session["gvImportFileSource"] = null;

            Session["gvCheckListSource"] = null;
        }



        public IList<DTO.BankTransaction> ChangeDataRequest
        {
            get { return (Session["ChangeDataRequest"] != null) ? (IList<DTO.BankTransaction>)Session["ChangeDataRequest"] : new List<DTO.BankTransaction>(); }
            set { Session["ChangeDataRequest"] = value; }
        }
        private void AddRequestChange(DTO.BankTransaction banktransection)
        {
            if (banktransection != null)
                ChangeDataRequest.Add(banktransection);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                base.HasPermit();
            }
        }

        private List<DTO.AttachFileDetail> AttatchFileDetail
        {
            get
            {
                return Session["AttatchFileDetail"] == null
                        ? new List<DTO.AttachFileDetail>()
                        : (List<DTO.AttachFileDetail>)Session["AttatchFileDetail"];
            }
            set
            {
                Session["AttatchFileDetail"] = value;
            }
        }

        private void ClearAndDisbleImport()
        {
            ClearCollection();
            BindValidateGridAll();
            lblMessageError.Text = "";
            btnImport.Enabled = false;
            btnImport.Visible = false;

            IsEnableChangePanel(false);
        }
        private void IsEnableChangePanel(Boolean isEnable)
        {
            pnlConfirmImportList.Visible = isEnable;
            pnlConfirmImportList.Enabled = isEnable;

        }
        protected void btnLoadFile_Click(object sender, EventArgs e)
        {
            ClearAndDisbleImport();
            pnlImportFile.Visible = false;

            if (string.IsNullOrEmpty(FuFile.FileName))
            {
                UCModalError.ShowMessageError = SysMessage.PleaseSelectFileData;
                UCModalError.ShowModalError();
                return;
            }
            if (String.IsNullOrEmpty(ddlBankTranfer.SelectedValue))
            {
                UCModalError.ShowMessageError = SysMessage.PleaseSelectBankData;
                UCModalError.ShowModalError();
                return;
            }
            else
            {
                var biz = new BLL.PaymentBiz();
                var res = biz.UploadData(FuFile.FileName, FuFile.PostedFile.InputStream, base.UserId, ddlBankTranfer.SelectedValue);
                if (res.IsError)
                {
                    UCModalError.ShowMessageError = res.ErrorMsg;
                    UCModalError.ShowModalError();
                }
                else if (res.DataResponse != null && res.DataResponse.Header != null
                    && !String.IsNullOrEmpty(res.DataResponse.Header.FirstOrDefault().ErrMessage))
                {
                    UCModalError.ShowMessageError = res.DataResponse.Header.FirstOrDefault().ErrMessage;
                    UCModalError.ShowModalError();
                }
                else
                {
                    Session["GroupID"] = res.DataResponse.GroupId;
                    if (res.DataResponse.Header.Count > 0)
                    {


                        Session["gvImportFileSource"] = res.DataResponse.Header;
                        Session["gvCheckListSource"] = res.DataResponse.Detail;

                        //***********   ***********************
                        IsEnableChangePanel(true);

                        BindValidateGridAll();




                        //****************************************


                        hdfGroupID.Value = res.DataResponse.GroupId;


                        if ((GvCheckListSource != null && GvCheckListSource.Count() >= 0)
                            && (GvPaymentMissingSource != null && GvPaymentMissingSource.Count() == 0)
                            && (GvPaymentLateSource != null && GvPaymentLateSource.Count() == 0)
                            && (GvPaymentInvalidSource != null && GvPaymentInvalidSource.Count() == 0)
                        )
                        {
                            bntConfirmChange_Click(bntConfirmChange, e);
                        }
                    }

                }
            }

        }

        private void BindValidateGridAll()
        {
            //Session["gvPaymentLateSource"] = res.DataResponse.Detail.Where(a => a.Status == 5);
            gvPaymentLate.DataSource = GvPaymentLateSource;
            gvPaymentLate.DataBind();


            //Session["gvPaymentMissingSource"] = res.DataResponse.Detail.Where(a => a.Status == 2);
            gvPaymentMissing.DataSource = GvPaymentMissingSource;
            gvPaymentMissing.DataBind();

            //Session["gvPaymentInvalidSource"] = res.DataResponse.Detail.Where(a => a.Status == 6 || a.Status==3 || a.Status==3 || a.Status ==4);
            gvPaymentInvalid.DataSource = GvPaymentInvalidSource;
            gvPaymentInvalid.DataBind();
        }

        protected void bntConfirmChange_Click(Object sender, EventArgs e)
        {
            pnlConfirmImportList.Visible = false;
            if (GvCheckListSource.Count() > 0)
            {
                if (GvPaymentLateSource.Count() > 0)
                {
                    foreach (DTO.BankTransaction item in GvPaymentLateSource)
                    {
                        DTO.BankTransaction data = GvCheckListSource.FirstOrDefault(a => a.Id == item.Id);
                        if (item.Selected)
                        {
                            data.ErrorMessage = "(อนุมัตินำเข้า) นำส่งล่าช้า ";
                        }
                        else
                        {
                            var result = GvCheckListSource.ToList();
                            result.Remove(data);
                            Session["gvCheckListSource"] = result;
                        }
                    }
                }
                if (GvPaymentMissingSource.Count() > 0)
                {
                    foreach (DTO.BankTransaction item in GvPaymentMissingSource)
                    {
                        DTO.BankTransaction data = GvCheckListSource.FirstOrDefault(a => a.Id == item.Id);
                        if (String.IsNullOrEmpty(item.ChangeRef1))
                        {
                            var result = GvCheckListSource.ToList();
                            result.Remove(data);
                            Session["gvCheckListSource"] = result;
                        }
                        else
                        {
                            data.ChangeRef1 = item.ChangeRef1;
                            data.ChangeAmount = item.ChangeAmount;
                            data.ErrorMessage = String.Format("เปลี่ยน เลขที่ใบสั่งจ่ายเป็น {0}", item.ChangeRef1);
                        }

                    }
                }
                if (GvPaymentInvalidSource.Count() > 0)
                {
                    foreach (DTO.BankTransaction item in GvPaymentInvalidSource)
                    {
                        DTO.BankTransaction data = GvCheckListSource.FirstOrDefault(a => a.Id == item.Id);
                        var result = GvCheckListSource.ToList();
                        result.Remove(data);
                        Session["gvCheckListSource"] = result;
                    }
                }

                btnImport.Enabled = true;
                btnImport.Visible = true;

                pnlImportFile.Visible = true;

                Int32 getResult = GvCheckListSource.Count();
                SummaryUploadSource.NumberOfValid = getResult;
                SummaryUploadSource.NumberOfInValid = SummaryUploadSource.NumberOfItems - getResult;


                gvImportFile.DataSource = GvImportFileSource;
                gvImportFile.DataBind();


                //if (GvCheckListSource.Where(a => !String.IsNullOrEmpty(a.ErrorMessage)).Count() > 0)
                //{
                //    lblMessageError.Text = Resources.errorPayRenewFee_001;
                //    lblMessageError.Visible = true;
                //    btnImport.Enabled = false;
                //    btnImport.Visible = false;
                //    Button1.Visible = true;
                //    Button1.Enabled = true;
                //}
                //else if (!String.IsNullOrEmpty(SummaryUploadSource.ErrMessage))
                //{
                //    lblMessageError.Text = SummaryUploadSource.ErrMessage;
                //    lblMessageError.Text = String.Format("ไม่สามารถนำเข้าข้อมูลได้ เนื่องจาก {0}!!!", SummaryUploadSource.ErrMessage);
                //    lblMessageError.Visible = true;
                //    btnImport.Enabled = false;
                //    btnImport.Visible = false;
                //    Button1.Visible = true;
                //    Button1.Enabled = true;
                //}
                //else if (GvCheckListSource.Count() <= 0)
                //{
                //    btnImport.Enabled = false;
                //    btnImport.Visible = false;
                //    Button1.Visible = true;
                //    Button1.Enabled = true;
                //}

                if (GvCheckListSource.Count() > 0)
                {
                    Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("th-TH");
                    gvCheckList.DataSource = GvCheckListSource;
                    gvCheckList.DataBind();
                }
            }
        }

        protected void hplView_Click(object sender, EventArgs e)
        {
            try
            {
                var strGroupID = hdfGroupID.Value;
                var gv = (GridViewRow)((LinkButton)sender).NamingContainer;
                Label strID = (Label)gv.FindControl("lblIDGv");
                hdfID.Value = strID.Text;
                GetTempBankTransDetail(strGroupID, strID.Text);
                ModRenewFee.Show();
            }
            catch (Exception ex)
            {
                UCModalError.ShowMessageError = Resources.errorPayRenewFee_003;
                UCModalError.ShowModalError();
            }


        }

        protected void hplFindPaymentControl_OnClick(Object sender, EventArgs e)
        {
            try
            {
                var gv = (GridViewRow)((LinkButton)sender).NamingContainer;
                Label paymentID = (Label)gv.FindControl("lblgvPaymentMissingId");
                txtFindPaymentSelected.Text = paymentID.Text;
                ModFindPayment.Show();

            }
            catch (Exception ex)
            {
                UCModalError.ShowMessageError = "";
                UCModalError.ShowModalError();
            }

        }

        private void GetTempBankTransDetail(string strGroupID, string strID)
        {
            BLL.PaymentBiz biz = new BLL.PaymentBiz();
            var res = biz.GetTempBankTransDetail(strGroupID, strID);
            if (res.IsError)
            {
                UCModalError.ShowMessageError = res.ErrorMsg;
                UCModalError.ShowModalError();
            }
            else
            {
                txtCompanyName.Text = res.DataResponse.COMPANY_NAME;
                txtEffectiveDate.Text = PaymentHtmlHelper.ResolveDate(res.DataResponse.EFFECTIVE_DATE);
                txtServiceCode.Text = res.DataResponse.SERVICE_CODE;
                if (res.DataResponse.BankType == "H")
                {
                    txtTotal.Text = PaymentHtmlHelper.PhaseKTBMoney(res.DataResponse.TOTAL_CREDIT_AMOUNT);
                    txtAmount.Text = PaymentHtmlHelper.PhaseKTBMoney(res.DataResponse.AMOUNT);
                }
                else if (res.DataResponse.BankType == "1")
                {
                    txtTotal.Text = PaymentHtmlHelper.PhaseCityBankMoney(res.DataResponse.TOTAL_CREDIT_AMOUNT);
                    txtAmount.Text = PaymentHtmlHelper.PhaseCityBankMoney(res.DataResponse.AMOUNT);
                }

                txtBankCode.Text = res.DataResponse.BANK_CODE;
                txtCompanyAccount.Text = res.DataResponse.COMPANY_ACCOUNT;
                txtPaymentDate.Text = PaymentHtmlHelper.ResolveDate(res.DataResponse.PAYMENT_DATE);
                txtPaymentTime.Text = res.DataResponse.PAYMENT_TIME;
                txtCustomerName.Text = res.DataResponse.CUSTOMER_NAME;
                txtCustomerNoRef1.Text = res.DataResponse.CUSTOMER_NO_REF1;
                txtRef2.Text = res.DataResponse.REF2;
                txtRef3.Text = res.DataResponse.REF3;
                txtBranchNo.Text = res.DataResponse.BRANCH_NO;
                txtTellerNo.Text = res.DataResponse.TELLER_NO;
                txtKindOfTransaction.Text = res.DataResponse.KIND_OF_TRANSACTION;
                txtTranSactionCode.Text = res.DataResponse.TRANSACTION_CODE;
                txtChequeNo.Text = res.DataResponse.CHEQUE_NO;

                txtChequeBankCode.Text = res.DataResponse.CHEQUE_BANK_CODE;
                UpdatePanelSearch.Update();
            }

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

        protected void btnImport_Click(object sender, EventArgs e)
        {


            BLL.PaymentBiz biz = new BLL.PaymentBiz();
            IList<DTO.ImportBankTransferData> refRequests = new List<DTO.ImportBankTransferData>();

            foreach (var item in GvCheckListSource)
            {
                refRequests.Add(new DTO.ImportBankTransferData() { Id = item.Id, Ref1 = item.Ref1, Status = item.Status, ChangeRef1 = item.ChangeRef1, ChangeAmount = item.ChangeAmount });
            }
            DTO.ImportBankTransferRequest importBankTransferRequest = new DTO.ImportBankTransferRequest();
            importBankTransferRequest.ImportBankTransfers = refRequests;
            importBankTransferRequest.UserOicId = UserProfile.OIC_EMP_NO;
            //importBankTransferRequest.UserOicId = UserProfile.OIC_User_Id;
            importBankTransferRequest.GroupId = hdfGroupID.Value;
            importBankTransferRequest.UserId = UserProfile.Id;
            var res = biz.SubmitBankTrans(importBankTransferRequest);

            if (res.IsError)
            {
                UCModalError.ShowMessageError = res.ErrorMsg;
                UCModalError.ShowModalError();
            }
            else
            {
                hdfID.Value = string.Empty;
                hdfGroupID.Value = string.Empty;
                pnlImportFile.Visible = false;
                UCModalSuccess.ShowMessageSuccess = res.DataResponse;
                UCModalSuccess.ShowModalSuccess();
            }


        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            ClearAndDisbleImport();
            Session["AttatchFileDetail"] = null;
            ddlBankTranfer.SelectedIndex = 0;
            hdfID.Value = string.Empty;
            hdfGroupID.Value = string.Empty;
            pnlImportFile.Visible = false;
        }

        protected void btnFindPayment_OnClick(object sender, EventArgs e)
        {

            //DTO.BankTransaction bankTransaction = GvPaymentMissingSource.SingleOrDefault(a => a.Ref1 == txtFindPaymentSelected.Text);

            var biz = new BLL.PaymentBiz();
            gvFindPaymentResult.DataSource = null;
            DTO.GetPaymentByRangeRequest request = new DTO.GetPaymentByRangeRequest();
            request.PaymentStarting = txtPaymentNumberStart.Text.Trim();
            request.PaymentEnding = txtPaymentNumberEnd.Text.Trim();
            //request.Amount = bankTransaction.Amount;

            var result = biz.GetPaymentByRange(request);

            gvFindPaymentResult.DataSource = result.DataResponse.PaymentByRangeResults;
            gvFindPaymentResult.DataBind();


            ModFindPayment.Show();


        }

        protected void btnSelectedMappingPaymentMissing_OnClick(Object sender, EventArgs e)
        {
            var gv = (GridViewRow)((Button)sender).NamingContainer;
            Label paymentID = (Label)gv.FindControl("lblPaymentRefNo");
            Label paymentAmount = (Label)gv.FindControl("lblPaymentAmount");

            DTO.BankTransaction bankTransaction = GvPaymentMissingSource.SingleOrDefault(a => a.Id == txtFindPaymentSelected.Text);
            bankTransaction.ChangeRef1 = paymentID.Text;
            bankTransaction.ChangeAmount = paymentAmount.Text;

            gvPaymentMissing.DataSource = GvPaymentMissingSource;
            gvPaymentMissing.DataBind();
            txtFindPaymentSelected.Text = "";
        }

        protected void chkgvPaymentLate_CheckedChanged(Object sender, EventArgs e)
        {
            var gv = (GridViewRow)((CheckBox)sender).NamingContainer;
            CheckBox _chkgvPaymentLate = (CheckBox)gv.FindControl("chkgvPaymentLate");
            Label _lblgvPaymentLateStatus = (Label)gv.FindControl("lblgvPaymentLateStatus");
            Label _lblgvPaymentLateRef1 = (Label)gv.FindControl("lblgvPaymentLateRef1");
            Label _lblgvPaymentLateId = (Label)gv.FindControl("lblgvPaymentLateId");

            DTO.BankTransaction result = GvPaymentLateSource.FirstOrDefault(a => a.Id == _lblgvPaymentLateId.Text);
            result.Selected = _chkgvPaymentLate.Checked;



            if (_chkgvPaymentLate.Checked)
            {
                _lblgvPaymentLateStatus.Text = "นำเข้า";
                _lblgvPaymentLateStatus.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                _lblgvPaymentLateStatus.Text = "ไม่นำเข้า";
                _lblgvPaymentLateStatus.ForeColor = System.Drawing.Color.Red;
            }
            _lblgvPaymentLateStatus.DataBind();


        }

        protected void btnFindPaymentOkey_Click(object sender, EventArgs e)
        {
            this.Form.DefaultButton = this.bntConfirmChange.UniqueID;
        }

    }
}