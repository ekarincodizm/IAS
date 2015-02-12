using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    [Serializable]
    public class GroupPayment
    {
        public string HeadNoSubPayment { get; set; }
        public string PaymentType { get; set; }
        public int? PersonNo { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? SubPaymentDate { get; set; }
        public string group_request_no { get; set; }
    }
}
