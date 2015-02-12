using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAS.DataServices.Properties;

namespace IAS.DataServices.Payment.TransactionBanking
{
    public  class BankFileHeaderBusinessRules
    {
        static String requiredFormat = "ข้อมูล {0} ไม่ถูกต้อง.";

        public static readonly BusinessRule ID_Required =
            new BusinessRule("ID", String.Format(requiredFormat, BankFileHeaderFieldNames.ID));

        public static readonly BusinessRule RECORD_TYPE_Required =
            new BusinessRule("RECORD_TYPE", String.Format(requiredFormat, BankFileHeaderFieldNames.RECORD_TYPE));

        public static readonly BusinessRule SEQUENCE_NO_Required =
            new BusinessRule("SEQUENCE_NO", String.Format(requiredFormat, BankFileHeaderFieldNames.SEQUENCE_NO));

        public static readonly BusinessRule SEQUENCE_NO_Not_Equea_One =
            new BusinessRule("SEQUENCE_NO", Resources.errorBankFileHeaderBusinessRules_001);

        public static readonly BusinessRule BANK_CODE_Required =
            new BusinessRule("BANK_CODE", String.Format(requiredFormat, BankFileHeaderFieldNames.BANK_CODE));

        public static readonly BusinessRule COMPANY_ACCOUNT_Required =
            new BusinessRule("COMPANY_ACCOUNT", String.Format(requiredFormat, BankFileHeaderFieldNames.COMPANY_ACCOUNT));

        public static readonly BusinessRule COMPANY_NAME_Required =
            new BusinessRule("COMPANY_NAME", String.Format(requiredFormat, BankFileHeaderFieldNames.COMPANY_NAME));

        public static readonly BusinessRule EFFECTIVE_DATE_Required =
            new BusinessRule("EFFECTIVE_DATE", String.Format(requiredFormat, BankFileHeaderFieldNames.EFFECTIVE_DATE));

                                                                        
        public static readonly BusinessRule SERVICE_CODE_Required =
            new BusinessRule("SERVICE_CODE", String.Format(requiredFormat, BankFileHeaderFieldNames.SERVICE_CODE));
    }
}
