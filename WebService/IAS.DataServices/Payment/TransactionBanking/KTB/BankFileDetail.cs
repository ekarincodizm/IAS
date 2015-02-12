using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IAS.DAL;
using IAS.DataServices.Payment.States;
using IAS.DataServices.Payment.Helpers;
using System.Text.RegularExpressions;


namespace IAS.DataServices.Payment.TransactionBanking.KTB
{
    public class BankFileDetail : AG_IAS_TEMP_PAYMENT_DETAIL
    {
       
        private Boolean IsNotDuplicate = true;
        private BankFileHeader _ktbFileHeader;
        private String _sequanceNO = "";
        private DTO.ImportPaymentStatus _paymentStatus;

        public String SequenceNo { get { return _sequanceNO; } set { _sequanceNO = value; } }

        public BankFileDetail()
        {
            _paymentStatus = DTO.ImportPaymentStatus.Loading; 
        }

        public BankFileDetail( BankFileHeader ktbFileHeader)
        {
         
            _ktbFileHeader = ktbFileHeader;
            _paymentStatus = DTO.ImportPaymentStatus.Loading; 
        }
        public BankFileHeader KTBFileHeader { get { return _ktbFileHeader; } }
        public void SetHeader(BankFileHeader header) 
        {
            this.HEADER_ID = header.ID;
            _ktbFileHeader = header;
        }
        public void DuplicateCitizen()
        {
            if (IsNotDuplicate)
                SetDuplicateCitizen();
        }

        protected void SetDuplicateCitizen()
        {
            _brokenRules.Add(BankFileDetailBusinessRules.CUSTOMER_NO_REF1_DuplicateInFile);
       
            IsNotDuplicate = false;
        }

        public DTO.ImportPaymentStatus Status { get { return _paymentStatus; } set { _paymentStatus = value; } }

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

        private void Validate()
        {
            _paymentStatus = DTO.ImportPaymentStatus.Valid;

            if (base.RECORD_TYPE != "D")
                AddBrokenRule(BankFileDetailBusinessRules.RECORD_TYPE_Required);

            if (Utils.DateUtil.ValidateDateFormatString("ddMMyyyy", base.PAYMENT_DATE, "th-TH"))
                AddBrokenRule(BankFileDetailBusinessRules.PAYMENT_DATE_Required);

            if (Utils.DateUtil.ValidateDateFormatString("hhmmss", base.PAYMENT_TIME, "th-TH"))
                AddBrokenRule(BankFileDetailBusinessRules.PAYMENT_TIME_Required);

            if (String.IsNullOrEmpty(CUSTOMER_NO_REF1))
                AddBrokenRule(BankFileDetailBusinessRules.CUSTOMER_NO_REF1_Required);

            if (base.CUSTOMER_NO_REF1.Trim().Length != 18)
                AddBrokenRule(BankFileDetailBusinessRules.CUSTOMER_NO_REF1_Required);

            int tempInt;
            if (!Int32.TryParse(base.AMOUNT, out tempInt))
                AddBrokenRule(BankFileDetailBusinessRules.AMOUNT_Required);

            if (_brokenRules.Count > 0)
                _paymentStatus = DTO.ImportPaymentStatus.Invalid;
            else 
            {
                AG_IAS_PAYMENT_G_T payment = KTBFileHeader.Context.AG_IAS_PAYMENT_G_T.FirstOrDefault(a => a.GROUP_REQUEST_NO == CUSTOMER_NO_REF1.Trim());

                if (payment == null)
                {
                    Int32 ref1 = 0;
                    if (Regex.IsMatch(CUSTOMER_NO_REF1.Trim(), @"^\d+$"))// Int32.TryParse(CUSTOMER_NO_REF1.Trim(), out ref1))
                    {
                        _paymentStatus = DTO.ImportPaymentStatus.MissingRefNo;
                    }
                    else
                    {
                        AddBrokenRule(BankFileDetailBusinessRules.CUSTOMER_NO_REF1_Required);
                        _paymentStatus = DTO.ImportPaymentStatus.Invalid;
                    }
                }
                else
                {
                    if (payment.STATUS == PaymentStatus.P.ToString())
                    {
                        AddBrokenRule(BankFileDetailBusinessRules.CUSTOMER_NO_REF1_Updated);
                        _paymentStatus = DTO.ImportPaymentStatus.Paid;
                    }
                    else
                    {
                        if (payment.EXPIRATION_DATE != null && (((DateTime)payment.EXPIRATION_DATE).Date < ParseDateFromString.ParseDateHeaderBank(PAYMENT_DATE).Date))
                        {
                            _paymentStatus = DTO.ImportPaymentStatus.Paylate;
                        }
                    }
                    //else if (PhaseAmountHelper.ConvertStringAmount(AMOUNT) != payment.GROUP_AMOUNT)
                    //{
                    //   AddBrokenRule(BankFileDetailBusinessRules.AMOUNT_Required);

                    //}
                }
            }

           


            
        }

        #endregion
    }
}