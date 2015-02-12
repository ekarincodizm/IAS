using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IAS.DAL;
using IAS.Common.Logging;

namespace IAS.DataServices.Payment.Receipts
{
    public class PaymentCollection
    {
        IList<PaymentTransaction> _payments;
        IList<AG_IAS_SUBPAYMENT_RECEIPT> _allNewReceipts = new List<AG_IAS_SUBPAYMENT_RECEIPT>();

        public PaymentCollection()
        {
            _payments = new List<PaymentTransaction>();
        }
        public IEnumerable<PaymentTransaction> Payments
        {
            get { return _payments; }
        }
        public void Add(PaymentTransaction payment)        
        {
            payment.SetParent(this);
            _payments.Add(payment);  
        }

        public IEnumerable<AG_IAS_SUBPAYMENT_RECEIPT> AllNewReceipts    
        {
            get { return _allNewReceipts; }  
        }
        public void AddNewReceipt(AG_IAS_SUBPAYMENT_RECEIPT receipt)
        {
            _allNewReceipts.Add(receipt);
        }
    }



    public class PaymentTransaction
    { 
        private AG_IAS_PAYMENT_G_T _transaction;
        private PaymentCollection _parent;
        IList<SubPaymentHead> _subPaymentHeads = new List<SubPaymentHead>();
        IList<SubPaymentReceipt> _overPaymentReceipts = new List<SubPaymentReceipt>();


        public PaymentTransaction (AG_IAS_PAYMENT_G_T transaction)
	    {
            _transaction = transaction;
	    }

        public AG_IAS_PAYMENT_G_T Get { 
            get { return _transaction; }
        }
        public IEnumerable<SubPaymentHead> SubPaymentHeads
        {
            get { return _subPaymentHeads; }
        }
        public void AddSubHead(SubPaymentHead subHead)
        {
            subHead.SetParent(this);
            _subPaymentHeads.Add(subHead);
        }
        public PaymentCollection Parent {
            get { return _parent; }
        }
        public void SetParent(PaymentCollection parent)
        {
            _parent = parent;
        }

        public IEnumerable<SubPaymentReceipt> OverPaymentReceipts
        {
            get { return _overPaymentReceipts; }
        }
        public void AddOverReceipt(SubPaymentReceipt receipt)
        {
            _overPaymentReceipts.Add(receipt);
        }
    }

    public class SubPaymentHead
    { 
         private AG_IAS_SUBPAYMENT_H_T _subPaymentHead;
         private PaymentTransaction _parent;  
         IList<SubPaymentDetail> _subPaymentDetails = new List<SubPaymentDetail>();
                                       
        public SubPaymentHead(AG_IAS_SUBPAYMENT_H_T subPaymentHead)      
	    {
            _subPaymentHead = subPaymentHead;
	    }

        public AG_IAS_SUBPAYMENT_H_T Get
        {                                   
            get { return _subPaymentHead; }
        }
        public IEnumerable<SubPaymentDetail> SubPaymentDetails
        {
            get { return _subPaymentDetails; }
        }
        public void AddSubDetail(SubPaymentDetail subDetail)
        {
            subDetail.SetParent(this);
            _subPaymentDetails.Add(subDetail);                                           
        }
        public PaymentTransaction Parent
        {
            get { return _parent; }
        }
        public void SetParent(PaymentTransaction parent)
        {
            _parent = parent;
        }
    }

    public class SubPaymentDetail
    {                     
        private AG_IAS_SUBPAYMENT_D_T _subPaymentDetail;
        private SubPaymentHead _parent;
        private AG_APPLICANT_T _applicant;
       
        IList<SubPaymentReceipt> _subPaymentReceipts = new List<SubPaymentReceipt>();

        public SubPaymentDetail(AG_IAS_SUBPAYMENT_D_T subPaymentDetail)
        {
            _subPaymentDetail = subPaymentDetail;
        }

        public AG_IAS_SUBPAYMENT_D_T Get
        {
            get { return _subPaymentDetail; }
        }
        public IEnumerable<SubPaymentReceipt> SubPaymentReceipts
        {
            get { return _subPaymentReceipts; }
        }
        public void AddSubReceipt(SubPaymentReceipt subReceipt)
        {
            subReceipt.SetParent(this);
            _subPaymentReceipts.Add(subReceipt);   
        }
        public SubPaymentHead Parent
        {
            get { return _parent; }
        }
        public void SetParent(SubPaymentHead parent)   
        {
            //if (String.IsNullOrWhiteSpace(_subPaymentDetail.TESTING_NO)) 
            //    throw new ApplicationException("ไม่สามารถ ทำรายการสอบได้.");

            _parent = parent;
            
        }
        public void SetApplicant(AG_APPLICANT_T applicant)
        {
            _applicant = applicant;      
        }
    }

    public class SubPaymentReceipt
    {
        private AG_IAS_SUBPAYMENT_RECEIPT _receipt;
        private SubPaymentDetail _parent;
        IList<String> _sqlCommand = new List<String>();

        public SubPaymentReceipt(AG_IAS_SUBPAYMENT_RECEIPT receipt)
        {
            _receipt = receipt;
        }

        public AG_IAS_SUBPAYMENT_RECEIPT Get
        {
            get { return _receipt; }
        }
        public IEnumerable<String> SQLCommmands
        {
            get { return _sqlCommand; }
        }
        public void AddSqlCommand(String sqlCommand)  
        {
            _sqlCommand.Add(sqlCommand);
        }

        public SubPaymentDetail Parent
        {
            get { return _parent; }
        }
        public void SetParent(SubPaymentDetail parent)
        {
            _parent = parent;
        }

        private String _docDate = "";
        private String _docCode = "";
        private String _docType = "";
        private String _dateMode = "";

        public void InitBillNumber(string doc_date,
                               string doc_code, string doc_type,
                               string date_mode) 
        {
            _docCode = doc_code;
            _docDate = doc_date;
            _docType = doc_type;
            _dateMode = date_mode;
        }

        public void GenBillNumber() {

            
            if (String.IsNullOrWhiteSpace(_docDate) || String.IsNullOrWhiteSpace(Get.USER_ID)) {
                LoggerFactory.CreateLog().LogError(String.Format("ข้อมูลไม่ครบถ้วน  DocDate = {0} and UserOicId= {1}", _docDate, Get.USER_ID));
                throw new ApplicationException("ข้อมูลไม่ครบถ้วนไม่สามารถออกเลขที่ใบเสร็จได้");
            }

            //if (!String.IsNullOrWhiteSpace(Get.RECEIPT_NO))
            //{
            //    LoggerFactory.CreateLog().LogError(String.Format("สร้างเลขที่ใบเสร็จ ทับข้อมูลเดิม  Receipt = {0} .", Get.RECEIPT_NO));
            //    throw new ApplicationException("สร้างเลขที่ใบเสร็จ ทับข้อมูลเดิม");
            //}
            Get.RECEIPT_NO = GenBillCodeFactory.GetBillNo(Get.USER_ID, _docDate, _docCode, _docType, _dateMode);
        }
    }
}