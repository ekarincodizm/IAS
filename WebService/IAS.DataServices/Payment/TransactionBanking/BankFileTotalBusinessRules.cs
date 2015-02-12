using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAS.DataServices.Payment.TransactionBanking
{
    public class BankFileTotalBusinessRules
    {
        static String requiredFormat = "ข้อมูล {0} ไม่ถูกต้อง.";

        public static readonly BusinessRule ID_Required = 
            new BusinessRule("ID", String.Format(requiredFormat, BankFileTotalFieldNames.ID));

        public static readonly BusinessRule RECORD_TYPE_Required = 
            new BusinessRule("RECORD_TYPE", String.Format(requiredFormat, BankFileTotalFieldNames.RECORD_TYPE));

        public static readonly BusinessRule SEQUENCE_NO_Required = 
            new BusinessRule("SEQUENCE_NO", String.Format(requiredFormat, BankFileTotalFieldNames.SEQUENCE_NO));

        public static readonly BusinessRule BANK_CODE_Required = 
            new BusinessRule("BANK_CODE", String.Format(requiredFormat, BankFileTotalFieldNames.BANK_CODE));

        public static readonly BusinessRule COMPANY_ACCOUNT_Required = 
            new BusinessRule("COMPANY_ACCOUNT", String.Format(requiredFormat, BankFileTotalFieldNames.COMPANY_ACCOUNT));

        public static readonly BusinessRule TOTAL_DEBIT_AMOUNT_Required = 
            new BusinessRule("TOTAL_DEBIT_AMOUNT", String.Format(requiredFormat, BankFileTotalFieldNames.TOTAL_DEBIT_AMOUNT));

        public static readonly BusinessRule TOTAL_DEBIT_TRANSACTION_Required = 
            new BusinessRule("TOTAL_DEBIT_TRANSACTION", String.Format(requiredFormat, BankFileTotalFieldNames.TOTAL_DEBIT_TRANSACTION));

        public static readonly BusinessRule TOTAL_CREDIT_AMOUNT_Required = 
            new BusinessRule("TOTAL_CREDIT_AMOUNT", String.Format(requiredFormat, BankFileTotalFieldNames.TOTAL_CREDIT_AMOUNT));

        public static readonly BusinessRule TOTAL_CREDIT_TRANSACTION_Required = 
            new BusinessRule("TOTAL_CREDIT_TRANSACTION", String.Format(requiredFormat, BankFileTotalFieldNames.TOTAL_CREDIT_TRANSACTION));

        public static readonly BusinessRule HEADER_ID_Required = 
            new BusinessRule("HEADER_ID", String.Format(requiredFormat, BankFileTotalFieldNames.HEADER_ID));
    }
}