using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAS.DataServices.Payment.TransactionBanking
{
    public class BankFileTotalFieldNames
    {
        public static readonly String ID = "ID";
        public static readonly String RECORD_TYPE = "RECORD_TYPE";
        public static readonly String SEQUENCE_NO = "SEQUENCE_NO";
        public static readonly String BANK_CODE = "BANK_CODE";
        public static readonly String COMPANY_ACCOUNT = "COMPANY_ACCOUNT";
        public static readonly String TOTAL_DEBIT_AMOUNT = "TOTAL_DEBIT_AMOUNT";
        public static readonly String TOTAL_DEBIT_TRANSACTION = "TOTAL_DEBIT_TRANSACTION";
        public static readonly String TOTAL_CREDIT_AMOUNT = "TOTAL_CREDIT_AMOUNT";
        public static readonly String TOTAL_CREDIT_TRANSACTION = "TOTAL_CREDIT_TRANSACTION";
        public static readonly String HEADER_ID = "HEADER_ID"; 
    }
}