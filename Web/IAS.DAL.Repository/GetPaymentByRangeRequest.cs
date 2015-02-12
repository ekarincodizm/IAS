using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    public class GetPaymentByRangeRequest
    {
        public String PaymentStarting { get; set; }
        public String PaymentEnding { get; set; }
        //public Decimal Amount { get; set; }
    }                                              
}
