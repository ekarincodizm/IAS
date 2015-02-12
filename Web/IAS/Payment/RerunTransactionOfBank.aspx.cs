using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.DTO;
using System.Xml;
using IAS.MasterPage;
using IAS.Common.Logging;
using IAS.Utils;

namespace IAS.Payment
{
    public partial class RerunTransactionOfBank : basepage
    {
        public IEnumerable<DTO.BankTransaction> GvPaymentMissingSource
        {
            get { return (Session["GvPaymentMissingSource"] != null) ? (IEnumerable<DTO.BankTransaction>)Session["GvPaymentMissingSource"] : new List<DTO.BankTransaction>(); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                ClearAndDisbleImport();
                pnlData.Visible = false;    

            }
        }

   

        public static DateTime ParseDateFromString(String headerDate)
        {
            string[] format = { "dd/MM/yyyy" };
            DateTime date;

            if (!DateTime.TryParseExact(headerDate, format, System.Globalization.CultureInfo.GetCultureInfo("th-TH"), System.Globalization.DateTimeStyles.None, out date))
            {
                date = DateTime.MinValue;
            }
            return date;
        }

        protected void btnSearchPayment_OnClick(object sender, EventArgs e)
        {
            Session["GvPaymentMissingSource"] = null;

            PaymentNotCompleteRequest request = new PaymentNotCompleteRequest();
            if (!String.IsNullOrWhiteSpace(txtFindRefNo.Text)) {
                request.Ref1 = txtFindRefNo.Text.Trim();
            }
            if (!String.IsNullOrWhiteSpace(txtPaymentDateStart.Text))    
            {
                request.StartDate = ParseDateFromString(txtPaymentDateStart.Text.Trim());
            }
            if (!String.IsNullOrWhiteSpace(txtPaymentDateEnd.Text))
            {
                request.EndDate = ParseDateFromString(txtPaymentDateEnd.Text.Trim());
            }


            BLL.PaymentBiz paymentBiz = new BLL.PaymentBiz();
            DTO.ResponseService<DTO.PaymentNotCompleteResponse> res = paymentBiz.PaymentNotComplete(request);

            if (res.IsError)
            {
                ((Site1)Master).ModelError.ShowMessageError = res.ErrorMsg;
                ((Site1)Master).ModelError.ShowModalError();
                pnlData.Visible = false;
            }
            else
            {
                Session["GvPaymentMissingSource"] = res.DataResponse.BankTransaction;
                gvPaymentMissing.DataSource = GvPaymentMissingSource;
                gvPaymentMissing.DataBind();
                pnlData.Visible = true;
                btnCancle.Visible = true;
                btnCancle.Enabled = true;
                //((Site1)Master).ModelSuccess.ShowModalSuccess();
            }

        }

        protected void btnFindPayment_OnClick(object sender, EventArgs e)
        {
            //DTO.BankTransaction bankTransaction = GvPaymentMissingSource.SingleOrDefault(a => a.Id == txtFindPaymentSelected.Text);

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
                ((Site1)Master).ModelError.ShowMessageError = "ไม่สามารถเลือกข้อมูลได้";
                ((Site1)Master).ModelError.ShowModalError();
                LoggerFactory.CreateLog().LogError("ไม่สามารถเลือกข้อมูลได้", ex);
            }

        }

        protected void btnSelectedMappingPaymentMissing_OnClick(Object sender, EventArgs e)
        {
            var gv = (GridViewRow)((Button)sender).NamingContainer;
            Label paymentRefNo = (Label)gv.FindControl("lblPaymentRefNo");
            Label paymentAmount = (Label)gv.FindControl("lblPaymentAmount");

            DTO.BankTransaction bankTransaction = GvPaymentMissingSource.SingleOrDefault(a => a.Id == txtFindPaymentSelected.Text);
            bankTransaction.ChangeRef1 = paymentRefNo.Text;
            bankTransaction.ChangeAmount = paymentAmount.Text;

            gvPaymentMissing.DataSource = GvPaymentMissingSource;
            gvPaymentMissing.DataBind();
            txtFindPaymentSelected.Text = "";

            btnImport.Enabled = true;
            btnImport.Visible = true;
            btnCancle.Visible = true;
            btnCancle.Enabled = true;
        }

        protected void btnImport_Click(object sender, EventArgs e)
        {

            try
            {
                BLL.PaymentBiz biz = new BLL.PaymentBiz();
                IList<DTO.ImportBankTransferData> refRequests = new List<DTO.ImportBankTransferData>();

                foreach (var item in GvPaymentMissingSource.Where(a => (a.ChangeRef1 != "" && !String.IsNullOrEmpty(a.ChangeRef1))))
                {
                    refRequests.Add(new DTO.ImportBankTransferData() { Id = item.Id, Ref1 = item.Ref1, Status = item.Status, ChangeRef1 = item.ChangeRef1, ChangeAmount = item.ChangeAmount });
                }
                DTO.ImportBankTransferRequest importBankTransferRequest = new DTO.ImportBankTransferRequest();
                importBankTransferRequest.ImportBankTransfers = refRequests;
                importBankTransferRequest.UserOicId = UserProfile.OIC_EMP_NO;
                //importBankTransferRequest.UserOicId = UserProfile.OIC_User_Id;

                var res = biz.ReSubmitBankTrans(importBankTransferRequest);

                if (res.IsError)
                {
                    ((Site1)Master).ModelError.ShowMessageError = res.ErrorMsg;
                    ((Site1)Master).ModelError.ShowModalError();
                }
                else
                {
                    ((Site1)Master).ModelSuccess.ShowMessageSuccess = res.DataResponse;
                    ((Site1)Master).ModelSuccess.ShowModalSuccess();
                    ClearAndDisbleImport();
                    ClearGridViewAll();
                    txtPaymentDateStart.Text = "";
                    txtPaymentDateEnd.Text = "";
                }
            }
            catch (Exception ex)
            {
                ((Site1)Master).ModelError.ShowMessageError = "ไม่สามารถทำรายการได้.";
                ((Site1)Master).ModelError.ShowModalError();
                LoggerFactory.CreateLog().LogError("ไม่สามารถทำรายการได้.", ex);
            }
            


        }

        private void ClearGridViewAll() {
            gvFindPaymentResult.DataSource = null;
            gvFindPaymentResult.DataBind();

            gvPaymentMissing.DataSource = null;
            gvPaymentMissing.DataBind(); 
        }


        protected void btnCancle_Click(object sender, EventArgs e)
        {
            ClearAndDisbleImport();
            ClearGridViewAll();
            Session["GvPaymentMissingSource"] = null;
            btnCancle.Visible = false;
            btnCancle.Enabled = false;
            pnlData.Visible = false;
         
        }
        private void ClearAndDisbleImport()
        {

            lblMessageError.Text = "";
            btnImport.Enabled = false;
            btnImport.Visible = false;

          
            
          
        }
    }
}

