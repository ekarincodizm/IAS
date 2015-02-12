using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAS.DataServices.Payment.TransactionBanking
{
    public class BankFileHeaderFieldNames
    {
        public static readonly String ID = "ID";
        public static readonly String RECORD_TYPE = "RECORD_TYPE";
        public static readonly String SEQUENCE_NO = "SEQUENCE_NO";
        public static readonly String BANK_CODE = "BANK_CODE";
        public static readonly String COMPANY_ACCOUNT = "COMPANY_ACCOUNT";
        public static readonly String COMPANY_NAME = "COMPANY_NAME";
        public static readonly String EFFECTIVE_DATE = "EFFECTIVE_DATE";
        public static readonly String SERVICE_CODE = "SERVICE_CODE"; 
    }
}