using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAS.DataServices.Payment.TransactionBanking
{
    public class BankFileDetailFieldNames
    {
        public static readonly String ID = "ID";
        public static readonly String RECORD_TYPE = "RECORD_TYPE";
        public static readonly String BANK_CODE = "BANK_CODE";
        public static readonly String COMPANY_ACCOUNT = "COMPANY_ACCOUNT";
        public static readonly String PAYMENT_DATE = "PAYMENT_DATE";
        public static readonly String PAYMENT_TIME = "PAYMENT_TIME";
        public static readonly String CUSTOMER_NAME = "CUSTOMER_NAME";
        public static readonly String CUSTOMER_NO_REF1 = "CUSTOMER_NO_REF1";
        public static readonly String REF2 = "REF2";
        public static readonly String REF3 = "REF3";
        public static readonly String BRANCH_NO = "BRANCH_NO";
        public static readonly String TELLER_NO = "TELLER_NO";
        public static readonly String KIND_OF_TRANSACTION = "KIND_OF_TRANSACTION";
        public static readonly String TRANSACTION_CODE = "TRANSACTION_CODE";
        public static readonly String CHEQUE_NO = "CHEQUE_NO";
        public static readonly String AMOUNT = "AMOUNT";
        public static readonly String CHEQUE_BANK_CODE = "CHEQUE_BANK_CODE";
        public static readonly String HEADER_ID = "HEADER_ID"; 
    }
}