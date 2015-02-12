using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IAS.DataServices.Properties;

namespace IAS.DataServices.Payment.TransactionBanking
{
    public class BankFileDetailBusinessRules
    {
        static String requiredFormat = "ข้อมูล {0} ไม่ถูกต้อง.";

        public static readonly BusinessRule ID_Required = 
            new BusinessRule("ID", String.Format(requiredFormat, BankFileDetailFieldNames.ID));

        public static readonly BusinessRule RECORD_TYPE_Required = 
            new BusinessRule("RECORD_TYPE", String.Format(requiredFormat, BankFileDetailFieldNames.RECORD_TYPE));

        public static readonly BusinessRule BANK_CODE_Required = 
            new BusinessRule("BANK_CODE", String.Format(requiredFormat, BankFileDetailFieldNames.BANK_CODE));

        public static readonly BusinessRule COMPANY_ACCOUNT_Required = 
            new BusinessRule("COMPANY_ACCOUNT", String.Format(requiredFormat, BankFileDetailFieldNames.COMPANY_ACCOUNT));

        public static readonly BusinessRule PAYMENT_DATE_Required = 
            new BusinessRule("PAYMENT_DATE", String.Format(requiredFormat, BankFileDetailFieldNames.PAYMENT_DATE));

        public static readonly BusinessRule PAYMENT_TIME_Required = 
            new BusinessRule("PAYMENT_TIME", String.Format(requiredFormat, BankFileDetailFieldNames.PAYMENT_TIME));

        public static readonly BusinessRule CUSTOMER_NAME_Required = 
            new BusinessRule("CUSTOMER_NAME", String.Format(requiredFormat, BankFileDetailFieldNames.CUSTOMER_NAME));

        public static readonly BusinessRule CUSTOMER_NO_REF1_Required = 
            new BusinessRule("CUSTOMER_NO_REF1", String.Format(requiredFormat, BankFileDetailFieldNames.CUSTOMER_NO_REF1));

        public static readonly BusinessRule CUSTOMER_NO_REF1_Updated =  
            new BusinessRule("CUSTOMER_NO_REF1", Resources.errorBankFileDetailBusinessRules_001);

        public static readonly BusinessRule CUSTOMER_NO_REF1_DuplicateInFile =   
            new BusinessRule("CUSTOMER_NO_REF1", Resources.errorBankFileDetailBusinessRules_002); 
        public static readonly BusinessRule REF2_Required = 
            new BusinessRule("REF2", String.Format(requiredFormat, BankFileDetailFieldNames.REF2));

        public static readonly BusinessRule REF3_Required = 
            new BusinessRule("REF3", String.Format(requiredFormat, BankFileDetailFieldNames.REF3));

        public static readonly BusinessRule BRANCH_NO_Required = 
            new BusinessRule("BRANCH_NO", String.Format(requiredFormat, BankFileDetailFieldNames.BRANCH_NO));

        public static readonly BusinessRule TELLER_NO_Required = 
            new BusinessRule("TELLER_NO", String.Format(requiredFormat, BankFileDetailFieldNames.TELLER_NO));

        public static readonly BusinessRule KIND_OF_TRANSACTION_Required = 
            new BusinessRule("KIND_OF_TRANSACTION", String.Format(requiredFormat, BankFileDetailFieldNames.KIND_OF_TRANSACTION));

        public static readonly BusinessRule TRANSACTION_CODE_Required =
            new BusinessRule("TRANSACTION_CODE", String.Format(requiredFormat, BankFileDetailFieldNames.TRANSACTION_CODE));

        public static readonly BusinessRule CHEQUE_NO_Required = 
            new BusinessRule("CHEQUE_NO", String.Format(requiredFormat, BankFileDetailFieldNames.CHEQUE_NO));

        public static readonly BusinessRule AMOUNT_Required = 
            new BusinessRule("AMOUNT", String.Format(requiredFormat, BankFileDetailFieldNames.AMOUNT));


        public static readonly BusinessRule CHEQUE_BANK_CODE_Required = 
            new BusinessRule("CHEQUE_BANK_CODE", String.Format(requiredFormat, BankFileDetailFieldNames.CHEQUE_BANK_CODE));

        public static readonly BusinessRule HEADER_ID_Required = 
            new BusinessRule("HEADER_ID", String.Format(requiredFormat, BankFileDetailFieldNames.HEADER_ID));
    }
}