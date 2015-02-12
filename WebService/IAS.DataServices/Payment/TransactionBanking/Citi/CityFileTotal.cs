using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IAS.DAL;
using IAS.DataServices.Payment.Helpers;

namespace IAS.DataServices.Payment.TransactionBanking.Citi
{
    public class CityFileTotal : AG_IAS_TEMP_PAYMENT_TOTAL
    {
        private CityFileHeader _cityFileHeader;
        private IList<CityFileDetail> _cityFileDetails = new List<CityFileDetail>();

        public CityFileTotal() { }
        public CityFileTotal(CityFileHeader cityFileHeader)
        {
            _cityFileHeader = cityFileHeader;
        }
        public CityFileHeader CityFileHeader { get { return _cityFileHeader; } }
        public void SetHeader(CityFileHeader header)
        {
            _cityFileHeader = header;
        }

        public IEnumerable<CityFileDetail> CityFileDetails { get { return _cityFileDetails; } }
        public void AddDetail(CityFileDetail detail)
        {
            detail.SetTotal(this);
            _cityFileDetails.Add(detail);
        }

        //public void AddDetail(AG_IAS_TEMP_PAYMENT_DETAIL detial)
        //{
        //    CityFileDetail detail_A = detial as CityFileDetail;
        //    detail_A.SetTotal(this);
        //    _cityFileDetails.Add(detail_A);
        //}

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
            Int32 countDetail = CityFileHeader.CityFileDetails.Count();

            //if (Convert.ToInt32(base.SEQUENCE_NO) != (countDetail+2))
            //    AddBrokenRule(BankFileTotalBusinessRules.SEQUENCE_NO_Required);


            //if (countDetail != Convert.ToInt32(base.TOTAL_CREDIT_TRANSACTION))
            //    AddBrokenRule(BankFileTotalBusinessRules.TOTAL_CREDIT_TRANSACTION_Required);

            Decimal total;
            if (!Decimal.TryParse(base.TOTAL_CREDIT_AMOUNT, out total))
                AddBrokenRule(BankFileTotalBusinessRules.TOTAL_CREDIT_AMOUNT_Required);

            Decimal moneyTotal = ParsePaymentAmountImport.PhaseCityBank(base.TOTAL_CREDIT_AMOUNT);
            Decimal sumTotal = this.CityFileDetails.Sum(a => ParsePaymentAmountImport.PhaseCityBank(a.AMOUNT));
            if (sumTotal != moneyTotal)
                AddBrokenRule(BankFileTotalBusinessRules.TOTAL_CREDIT_AMOUNT_Required);
            
        }

    

        #endregion
    }
}