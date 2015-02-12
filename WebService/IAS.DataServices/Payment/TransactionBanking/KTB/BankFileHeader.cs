using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IAS.DAL;
using IAS.Utils;
using IAS.DataServices.Payment.Helpers;
using IAS.DataServices.Payment.States;

namespace IAS.DataServices.Payment.TransactionBanking.KTB
{
    public class BankFileHeader : AG_IAS_TEMP_PAYMENT_HEADER
    {

        private IAS.DAL.Interfaces.IIASPersonEntities _ctx;
        private BankFileTotal _ktbFileTotal;
        private IList<BankFileDetail> _ktbFileDetails = new List<BankFileDetail>();
        private String _fileName;

        public BankFileHeader(IAS.DAL.Interfaces.IIASPersonEntities ctx) {
            this._ctx = ctx;
        
        }
        public BankFileHeader(IAS.DAL.Interfaces.IIASPersonEntities ctx, String id, String fileName) 
        {
            this._ctx = ctx;
            this.ID = id;
            _fileName = fileName;
        }

        public IAS.DAL.Interfaces.IIASPersonEntities Context { get { return _ctx; }  }

        public String FileName { get { return _fileName; } set { _fileName = value; } }
        public IEnumerable<BankFileDetail> KTBFileDetails { get { return _ktbFileDetails; } }
        public void AddDetail(BankFileDetail detail) 
        {
            detail.SetHeader(this);
            _ktbFileDetails.Add(detail);
        }

        public BankFileTotal KTBFileTotal { get { return _ktbFileTotal; } }

        public void AddDetail(AG_IAS_TEMP_PAYMENT_DETAIL detial) {
            BankFileDetail detail_A = detial as BankFileDetail;
            detail_A.SetHeader(this);
            _ktbFileDetails.Add(detail_A);
        }
        public void SetTotal(AG_IAS_TEMP_PAYMENT_TOTAL total) {
            BankFileTotal total_A = total as BankFileTotal;
            total_A.SetHeader(this);

        }
        public void SetTotal(BankFileTotal total) {
            total.SetHeader( this);
            total.HEADER_ID = this.ID;
            total.ID = this.ID;
            _ktbFileTotal = total;
        }


        #region Validate
        private List<BusinessRule> _brokenRules = new List<BusinessRule>();
        public IEnumerable<BusinessRule> GetBrokenRules()
        {
            _brokenRules.Clear();
            Validate();
            return _brokenRules;
        }

        protected void AddBrokenRule(BusinessRule businessRule)
        {
            _brokenRules.Add(businessRule);
        }

        public DTO.UploadResult<DTO.SummaryBankTransaction, DTO.BankTransaction> ValidateData()
        {

            DTO.UploadResult<DTO.SummaryBankTransaction, DTO.BankTransaction> result = new DTO.UploadResult<DTO.SummaryBankTransaction, DTO.BankTransaction>();
            result.Header = new List<DTO.SummaryBankTransaction>();
            result.Detail = new List<DTO.BankTransaction>();
            result.GroupId = this.ID;


            foreach (BankFileDetail item in this.KTBFileDetails)
            {

                DTO.BankTransaction detail = new DTO.BankTransaction()
                {
                    Id = item.ID,
                    SequenceNo = item.SequenceNo,
                    PaymentDate = ParseDateFromString.ParseDateHeaderBank(item.PAYMENT_DATE).ToString("dd/MM/yyyy"), //  .Format("{0}/{1}/{2}", item.PAYMENT_DATE.Substring(0, 2), item.PAYMENT_DATE.Substring(2, 2), item.PAYMENT_DATE.Substring(4, 4)),
                    CustomerName = item.CUSTOMER_NAME,
                    Ref1 = item.CUSTOMER_NO_REF1,
                    Ref2 = item.REF2,
                    ChequeNo = item.CHEQUE_NO,
                    Amount = PhaseAmountHelper.ConvertStringAmount(item.AMOUNT),
                    AccountNo = item.COMPANY_ACCOUNT,
                    
                };

                detail.ErrorMessage = (item.GetBrokenRules().Count() > 0) ? item.GetBrokenRules().First().Rule : "";
                detail.Status = (int)item.Status;
                if (item.Status == DTO.ImportPaymentStatus.Paylate) {
                    detail.ErrorMessage += String.Format("- นำส่งล้าช้า <br />");
                }

              
                result.Detail.Add(detail);
        
            }
 
            Int32 countDetail = KTBFileDetails.Count();
            Int32 countOfInvalid = result.Detail.Where(a =>  !String.IsNullOrEmpty(a.ErrorMessage)).Count();

            Decimal sumAmount = PhaseAmountHelper.ConvertStringAmount(KTBFileTotal.TOTAL_CREDIT_AMOUNT);


            DTO.SummaryBankTransaction sumary = new DTO.SummaryBankTransaction() {
                UploadDate = DateTime.Today,
                FileName = FileName,
                NumberOfItems = countDetail,
                NumberOfValid = countDetail - countOfInvalid,
                NumberOfInValid = countOfInvalid,
                Total = PhaseAmountHelper.ConvertStringAmount(this.KTBFileTotal.TOTAL_CREDIT_AMOUNT) ,
            };

            //if (this.KTBFileTotal.GetBrokenRules().Count() > 0)
            //{
            //    sumary.ErrMessage = this.KTBFileTotal.GetBrokenRules().First().Rule;
            //}

            Decimal sumDetail = this.KTBFileDetails.Sum(a => PhaseAmountHelper.ConvertStringAmount(a.AMOUNT));
            if (sumary.Total != sumDetail) {
                sumary.ErrMessage = "จำนวนเงินรวม เอกสารไม่ถูกต้อง";
            }

            result.Header.Add(sumary);
            return result;
        }

        public void ValidCiticenDuplicate()
        {
            foreach (BankFileDetail detail in KTBFileDetails)
            {
                IEnumerable<BankFileDetail> bankFileDetails = this.KTBFileDetails.Where(a => a.CUSTOMER_NO_REF1 == detail.CUSTOMER_NO_REF1);
                if (bankFileDetails.Count() > 1)
                {
                    //this.DuplicateCitizen();
                    foreach (BankFileDetail detial in bankFileDetails)
                    {
                        detial.DuplicateCitizen();
                    }
                }
            }

        }

        protected virtual void Validate()
        {
            if (Convert.ToInt32(base.SEQUENCE_NO) != 1)
                AddBrokenRule(BankFileHeaderBusinessRules.SEQUENCE_NO_Not_Equea_One);

            if (DateUtil.ValidateDateFormatString("ddMMyyyy", base.EFFECTIVE_DATE))
            {
                AddBrokenRule(BankFileHeaderBusinessRules.EFFECTIVE_DATE_Required);
            }

            if (base.RECORD_TYPE.Trim() != "H")
                AddBrokenRule(BankFileHeaderBusinessRules.RECORD_TYPE_Required);

        }

        #endregion

    }
}
