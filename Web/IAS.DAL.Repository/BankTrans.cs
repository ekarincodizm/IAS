using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    [Serializable]
    public class BankTransHeader
    {
        public string ID { get; set; }
        public string RecordType { get; set; }
        public string SequenceNo { get; set; }
        public string BankCode { get; set; }
        public string CompanyAccount { get; set; }
        public string CompanyName { get; set; }
        public string EffectiveDate { get; set; }
        public string ServiceCode { get; set; }
    }

    [Serializable]
    public class BankTransDetail 
    {
        public String BankType { get; set; }

        public String ID { get; set; } //	VARCHAR2	(	15	)
        public String RECORD_TYPE { get; set; } //	VARCHAR2	(	1	)
        public String BANK_CODE { get; set; } //	VARCHAR2	(	3	)
        public String COMPANY_ACCOUNT { get; set; } //	VARCHAR2	(	10	)
        public String PAYMENT_DATE { get; set; } //	VARCHAR2	(	8	)
        public String PAYMENT_TIME { get; set; } //	VARCHAR2	(	6	)
        public String CUSTOMER_NAME { get; set; } //	VARCHAR2	(	70	)
        public String CUSTOMER_NO_REF1 { get; set; } //	VARCHAR2	(	20	)
        public String REF2 { get; set; } //	VARCHAR2	(	20	)
        public String REF3 { get; set; } //	VARCHAR2	(	20	)
        public String BRANCH_NO { get; set; } //	VARCHAR2	(	4	)
        public String TELLER_NO { get; set; } //	VARCHAR2	(	4	)
        public String KIND_OF_TRANSACTION { get; set; } //	VARCHAR2	(	1	)
        public String TRANSACTION_CODE { get; set; } //	VARCHAR2	(	3	)
        public String CHEQUE_NO { get; set; } //	VARCHAR2	(	7	)
        public String AMOUNT { get; set; } //	VARCHAR2	(	13	)
        public String CHEQUE_BANK_CODE { get; set; } //	VARCHAR2	(	3	)
        public String HEADER_ID { get; set; } //	VARCHAR2	(	15	)


        public string COMPANY_NAME { get; set; }
        public string SERVICE_CODE { get; set; }
        public string EFFECTIVE_DATE { get; set; }
        public string TOTAL_DEBIT_AMOUNT { get; set; }
        public string TOTAL_CREDIT_AMOUNT { get; set; }  
    }

    [Serializable]
    public class BankTransTotal
    {
        public string ID { get; set; }
        public string RecordType { get; set; }
        public string SequenceNo { get; set; }
        public string BankCode { get; set; }
        public string CompanyAccount { get; set; }
        public decimal TotalDebitAmount { get; set; }
        public decimal TotalDebitTransaction { get; set; }
        public decimal TotalCreditAmount { get; set; }
        public decimal TotalCreditTransaction { get; set; }
        public string HeaderId { get; set; }
    }
}
