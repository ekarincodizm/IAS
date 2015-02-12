using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAS.Class
{
    public class RcvPaymentClass
    {
        public string HEAD_REQUEST_NO { get; set; }
        public string GROUP_REQUEST_NO { get; set; }
        public string PERSON_NO { get; set; }
        public string GROUP_AMOUNT { get; set; }
        public string GROUP_DATE { get; set; }
        public string SUBPAYMENT_DATE { get; set; }
        public string REMARK { get; set; }
        public string HeadapplyDate { get; set; }
        public string HeadtimeTest { get; set; }
        public string applyDate { get; set; }
        public string timeTest { get; set; }
        public string HeadExamPlace { get; set; }
        public string ExamPlace { get; set; }
        public string LicenseName { get; set; }
        public string ExamRemark { get; set; }

        public string SumAmt { get; set; }
        public string SumAmtMulti { get; set; }
        public string PaymentBy  { get; set; } 
        public string Referance1No { get; set; }
        public string Referance2No { get; set; }
        public string PatitionName { get; set; }
        public string PatitionNameMulti { get; set; }
        public string BranchReceive { get; set; }
        public string BankAccountNumber { get; set; }
        public string PrintDateString { get; set; }
        public string ExpireDateString { get; set; }
        public string ExpireDateShortString { get; set; }
        public byte[] BarCodeImage { get; set; }
        public byte[] SigImg { get; set; }
        public string BarCode { get; set; }
        #region recive
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ReceiptDate { get; set; }
        public string BathThai { get; set; }
        #endregion
    }
}



