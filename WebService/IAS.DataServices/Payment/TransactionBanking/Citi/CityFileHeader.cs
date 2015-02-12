using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IAS.DAL;
using IAS.Utils;
using IAS.DataServices.Payment.Helpers;
using IAS.DataServices.Payment.States;

namespace IAS.DataServices.Payment.TransactionBanking.Citi
{
    public class CityFileHeader : AG_IAS_TEMP_PAYMENT_HEADER
    {
        // head type 1
        private IAS.DAL.Interfaces.IIASPersonEntities _ctx;
        private Boolean IsFinishSection = true;
        private Int32 currentSectionIndex = (-1);
        private Int32 rowCount = 0;  // type 9
        private IList<CityFileBoxHeader> _cityFileBoxHeaders = new List<CityFileBoxHeader>(); // type2
        private IList<CityFileOverFlow> _cityFileOverFlows = new List<CityFileOverFlow>();// type4
        private IList<CityFileBatchHeader> _cityFileBatchHeaders = new List<CityFileBatchHeader>(); // type 5;
        private IList<CityFileDetail> _cityFileDetails = new List<CityFileDetail>(); // type 6
        private IList<CityFileBatchTotal> _cityFileBatchTotals = new List<CityFileBatchTotal>(); // type 7
        private IList<CityFileTotal> _cityFileTotals = new List<CityFileTotal>();  // type 8

        
        private String _fileName;

        public CityFileHeader(IAS.DAL.Interfaces.IIASPersonEntities ctx)
        {
            this._ctx = ctx;  
        }
        public CityFileHeader(IAS.DAL.Interfaces.IIASPersonEntities ctx, String id, String fileName) 
        {
            this._ctx = ctx; 
            this.ID = id;
            _fileName = fileName;
        }
        public IAS.DAL.Interfaces.IIASPersonEntities Context { get { return _ctx; } }

        public Int32 RowCount { get { return rowCount; } set { rowCount = value; } } 
        public String FileName { get { return _fileName; } set { _fileName = value; } }
        public IEnumerable<CityFileDetail> CityFileDetails { get { return _cityFileDetails; } }
        public void AddDetail(CityFileDetail detail) 
        {
            if (IsFinishSection) 
                NewTotal();
 
            detail.HEADER_ID = this.ID;
            detail.SetHeader(this);
            detail.SetTotal(CityFileTotal);
            _cityFileDetails.Add(detail);
            CityFileTotal.AddDetail(detail);
        }

        private void NewTotal() 
        {
            IsFinishSection = false;
            CityFileTotal total = new CityFileTotal(this);
            currentSectionIndex++;
            _cityFileTotals.Add(total);
        }

        public void AddDetail(AG_IAS_TEMP_PAYMENT_DETAIL detial) 
        {
            if (IsFinishSection)
                NewTotal();

            CityFileDetail detail_A = detial as CityFileDetail;
            detail_A.SetHeader(this);
            detail_A.SetTotal(CityFileTotal);
            _cityFileDetails.Add(detail_A);
            CityFileTotal.AddDetail(detail_A);
        }

        public IEnumerable<CityFileTotal> CityFileTotals { get { return _cityFileTotals; } }
        public void AddTotal(CityFileTotal total)
        {
            total.HEADER_ID = this.ID;
            total.SetHeader(this);
            _cityFileTotals.Add(total);
        }

        public void AddTotal(AG_IAS_TEMP_PAYMENT_TOTAL total)    
        {
            CityFileTotal total_A = total as CityFileTotal;
            total_A.SetHeader(this);
            _cityFileTotals.Add(total_A);
        }

        public CityFileTotal CityFileTotal { 
            get { return _cityFileTotals.ElementAtOrDefault(currentSectionIndex); }
            set
            {
                CityFileTotal city = _cityFileTotals.ElementAtOrDefault(currentSectionIndex);
                city = value;
            }
        }

        public void SetTotal(AG_IAS_TEMP_PAYMENT_TOTAL total)
        {
            CityFileTotal.RECORD_TYPE = total.RECORD_TYPE;
            CityFileTotal.SEQUENCE_NO = total.SEQUENCE_NO;
            CityFileTotal.BANK_CODE = total.BANK_CODE;
            CityFileTotal.COMPANY_ACCOUNT = total.COMPANY_ACCOUNT;
            CityFileTotal.TOTAL_DEBIT_AMOUNT = total.TOTAL_DEBIT_AMOUNT;
            CityFileTotal.TOTAL_DEBIT_TRANSACTION = total.TOTAL_DEBIT_TRANSACTION;
            CityFileTotal.TOTAL_CREDIT_AMOUNT = total.TOTAL_CREDIT_AMOUNT;
            CityFileTotal.TOTAL_CREDIT_TRANSACTION = total.TOTAL_CREDIT_TRANSACTION;

            IsFinishSection = true;

        }
        public void SetTotal(CityFileTotal total)
        {
            CityFileTotal.RECORD_TYPE = total.RECORD_TYPE;
            CityFileTotal.SEQUENCE_NO = total.SEQUENCE_NO;
            CityFileTotal.BANK_CODE = total.BANK_CODE;
            CityFileTotal.COMPANY_ACCOUNT = total.COMPANY_ACCOUNT;
            CityFileTotal.TOTAL_DEBIT_AMOUNT = total.TOTAL_DEBIT_AMOUNT;
            CityFileTotal.TOTAL_DEBIT_TRANSACTION = total.TOTAL_DEBIT_TRANSACTION;
            CityFileTotal.TOTAL_CREDIT_AMOUNT = total.TOTAL_CREDIT_AMOUNT;
            CityFileTotal.TOTAL_CREDIT_TRANSACTION = total.TOTAL_CREDIT_TRANSACTION;

            IsFinishSection = true;
        }


        public IEnumerable<CityFileBoxHeader> CityFileBoxHeaders { get { return _cityFileBoxHeaders; } }  // type2
        public IEnumerable<CityFileOverFlow> CityFileOverFlows { get { return _cityFileOverFlows; } } // type4
        public IEnumerable<CityFileBatchHeader> CityFileBatchHeaders { get { return _cityFileBatchHeaders; } }  // type 5;
        public IEnumerable<CityFileBatchTotal> CityFileBatchTotals { get { return _cityFileBatchTotals; } } // type 7

        public void AddBoxHeader(CityFileBoxHeader boxHeader)
        {
            _cityFileBoxHeaders.Add(boxHeader);                    
        }
        public void AddOverFlow(CityFileOverFlow overFlow)   
        {
            _cityFileOverFlows.Add(overFlow);
        }
        public void AddBatchHeader(CityFileBatchHeader batchHeader)
        {
            _cityFileBatchHeaders.Add(batchHeader);
        }
        public void AddBatchTotal(CityFileBatchTotal batchTotal)
        {
            _cityFileBatchTotals.Add(batchTotal);
        }

        public Decimal TotalAmount {
            get
            {
                Decimal allTotal = this.CityFileTotals.Sum(a => Convert.ToDecimal(a.TOTAL_CREDIT_AMOUNT));
                return allTotal;
            }
        
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


            foreach (CityFileDetail item in this.CityFileDetails)
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
                    Amount = PhaseAmountHelper.ConvertStringAmountCity(item.AMOUNT),
                    AccountNo = item.COMPANY_ACCOUNT,

                };

                detail.ErrorMessage = (item.GetBrokenRules().Count() > 0) ? item.GetBrokenRules().First().Rule : "";
                detail.Status = (int)item.Status;
                if (item.Status == DTO.ImportPaymentStatus.Paylate)
                {
                    detail.ErrorMessage += String.Format("- นำส่งล้าช้า <br />");
                }


                result.Detail.Add(detail);
        
            }


            Int32 countDetail = CityFileDetails.Count();
            Int32 countOfInvalid = result.Detail.Where(a =>  !String.IsNullOrEmpty(a.ErrorMessage)).Count();

            


            Decimal sumAmount = PhaseAmountHelper.ConvertStringAmountCity(CityFileTotal.TOTAL_CREDIT_AMOUNT);


            DTO.SummaryBankTransaction sumary = new DTO.SummaryBankTransaction() {
                UploadDate = DateTime.Today,
                FileName = FileName,
                NumberOfItems = countDetail,
                NumberOfValid = countDetail - countOfInvalid,
                NumberOfInValid = countOfInvalid,
                Total = this.TotalAmount ,
            };

            //foreach (CityFileTotal item in this.CityFileTotals)
            //{
            //    if (item.GetBrokenRules().Count() > 0)
            //    {
            //        sumary.ErrMessage = this.CityFileTotal.GetBrokenRules().First().Rule;
            //        break;
            //    }
            //}
            Decimal sumDetail = this.CityFileDetails.Sum(a => PhaseAmountHelper.ConvertStringAmountCity(a.AMOUNT));
            if (sumary.Total != sumDetail)
            {
                sumary.ErrMessage = "จำนวนเงินรวม เอกสารไม่ถูกต้อง";
            }

            result.Header.Add(sumary);
            return result;
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
