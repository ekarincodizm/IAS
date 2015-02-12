using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IAS.DAL;
using IAS.DataServices.Payment.States;
using IAS.DataServices.Payment.Helpers;
using System.Text.RegularExpressions;

namespace IAS.DataServices.Payment.TransactionBanking.Citi
{
    public class CityFileDetail : AG_IAS_TEMP_PAYMENT_DETAIL
    {
       
        private Boolean IsNotDuplicate = true;
        private CityFileHeader _cityFileHeader;
        private CityFileTotal _cityFileTotal;
        private String _sequanceNO = "";
        public String SequenceNo { get { return _sequanceNO; } set { _sequanceNO = value; } }
        private DTO.ImportPaymentStatus _paymentStatus;

        public CityFileDetail()
        {
            _paymentStatus = DTO.ImportPaymentStatus.Loading; 
        }
        public CityFileDetail(CityFileHeader cityFileHeader)
        {
            _paymentStatus = DTO.ImportPaymentStatus.Loading; 
            _cityFileHeader = cityFileHeader;
        }
        public CityFileHeader CityFileHeader { get { return _cityFileHeader; } }
        public void SetHeader(CityFileHeader header)
        {
            this.HEADER_ID = header.ID;
            _cityFileHeader = header;
        }
        public CityFileTotal CityFileTotal { get { return _cityFileTotal; } }
        public void SetTotal(CityFileTotal total)
        {
            _cityFileTotal = total;
        }
        public void DuplicateCitizen()
        {
            if (IsNotDuplicate)
                SetDuplicateCitizen();
        }
        public DTO.ImportPaymentStatus Status { get { return _paymentStatus; } set { _paymentStatus = value; } }
        protected void SetDuplicateCitizen()
        {
            _brokenRules.Add(BankFileDetailBusinessRules.CUSTOMER_NO_REF1_DuplicateInFile);

            IsNotDuplicate = false;
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

        private void Validate()
        {
            _paymentStatus = DTO.ImportPaymentStatus.Valid;

            if (base.RECORD_TYPE != "6")
                AddBrokenRule(BankFileDetailBusinessRules.RECORD_TYPE_Required);

            if (Utils.DateUtil.ValidateDateFormatString("ddMMyyyy", base.PAYMENT_DATE, "th-TH"))
                AddBrokenRule(BankFileDetailBusinessRules.PAYMENT_DATE_Required);

            //if (Utils.DateUtil.ValidateDateFormatString("hhmmss", base.PAYMENT_TIME, "th-TH"))
            //    AddBrokenRule(BankFileDetailBusinessRules.PAYMENT_TIME_Required);

            if (String.IsNullOrEmpty(CUSTOMER_NO_REF1.Trim()))
                AddBrokenRule(BankFileDetailBusinessRules.CUSTOMER_NO_REF1_Required);

            if (base.CUSTOMER_NO_REF1.Length != 20)
                AddBrokenRule(BankFileDetailBusinessRules.CUSTOMER_NO_REF1_Required);

            Decimal tempInt;
            if (!Decimal.TryParse(base.AMOUNT, out tempInt))
                AddBrokenRule(BankFileDetailBusinessRules.AMOUNT_Required);

            if (_brokenRules.Count > 0)
                _paymentStatus = DTO.ImportPaymentStatus.Invalid;
            else
            {
                AG_IAS_PAYMENT_G_T payment = CityFileHeader.Context.AG_IAS_PAYMENT_G_T.FirstOrDefault(a => a.GROUP_REQUEST_NO == CUSTOMER_NO_REF1.Trim());

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