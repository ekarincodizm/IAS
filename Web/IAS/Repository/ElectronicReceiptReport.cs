using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAS.Repository
{
    public class ElectronicReceiptReport
    {
        public string PETITION_TYPE_NAME { get; set; }
        public string RECEIPT_NO { get; set; }
        public string FLNAME { get; set; }
        public string ID_CARD_NO { get; set; }
        public decimal AMOUNT { get; set; }
        public int PRINT_TIMES { get; set; }
        public DateTime ORDER_DATE { get; set; }
        public DateTime PAYMENT_DATE { get; set; }
        public string LICENSE_NO { get; set; }

    }
}