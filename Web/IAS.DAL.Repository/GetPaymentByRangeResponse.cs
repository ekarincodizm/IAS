using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    public class GetPaymentByRangeResponse
    {
        public IEnumerable<PaymentByRangeResult> PaymentByRangeResults { get; set; }
    }


    public class PaymentByRangeResult 
    {
        public String Id { get; set; }
        public String PaymentRefNo { get; set; }
        public String CreateDate { get; set; }
        public String PaymentAmount { get; set; }
        public String Status { get; set; }
    }
}
