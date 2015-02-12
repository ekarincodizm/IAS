using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    [Serializable]
    public class PaymentDetail
    {
        public string PAYMENT_NO { get; set; }
        public string TESTING_NO { get; set; }
        public DateTime? PAYMENT_DATE { get; set; }
        public string PAYMENT_TYPE_NAME { get; set; }
        public string ID_CARD_NO { get; set; }
        public string COMPANY_CODE { get; set; }
        public string LICENSE_NO { get; set; }
        public string LICENSE_NO_REQUEST { get; set; }
        public string RECEIPT_NO { get; set; }
        public DateTime? RECEIPT_DATE { get; set; }
        public decimal? AMOUNT { get; set; }
        public string LICENSE_TYPE_CODE { get; set; }
        public string PAYMENT_FOR_REPORT { get; set; }
    }
}
