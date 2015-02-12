using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IAS.DAL;
using IAS.DataServices.Payment.Helpers;

namespace IAS.DataServices.Payment.TransactionBanking.KTB
{
    public class BankFileTotal : AG_IAS_TEMP_PAYMENT_TOTAL
    {
        private BankFileHeader _ktbFileHeader;

        public BankFileTotal() { }
        public BankFileTotal(BankFileHeader ktbFileHeader)
        {
            _ktbFileHeader = ktbFileHeader;
        }
        public BankFileHeader KTBFileHeader { get { return _ktbFileHeader; } }
        public void SetHeader(BankFileHeader header) 
        {
            this.HEADER_ID = header.ID;
            _ktbFileHeader = header;
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
            Int32 countDetail = KTBFileHeader.KTBFileDetails.Count();

            if (Convert.ToInt32(base.SEQUENCE_NO) != (countDetail+2))
                AddBrokenRule(BankFileTotalBusinessRules.SEQUENCE_NO_Required);


            if (countDetail != Convert.ToInt32(base.TOTAL_CREDIT_TRANSACTION))
                AddBrokenRule(BankFileTotalBusinessRules.TOTAL_CREDIT_TRANSACTION_Required);


            if (!ParsePaymentAmountImport.TryPhase(base.TOTAL_CREDIT_AMOUNT))
                AddBrokenRule(BankFileTotalBusinessRules.TOTAL_CREDIT_AMOUNT_Required);

            Decimal moneyTotal = ParsePaymentAmountImport.Phase(base.TOTAL_CREDIT_AMOUNT);
            if (KTBFileHeader.KTBFileDetails.Sum(a => ParsePaymentAmountImport.Phase(a.AMOUNT)) != moneyTotal)
                AddBrokenRule(BankFileTotalBusinessRules.TOTAL_CREDIT_AMOUNT_Required);
            
        }

    

        #endregion
    }
}