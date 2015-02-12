using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    public class PaymentNotCompleteRequest
    {
        public String UserId { get; set; }
        public DateTime RequestTime { get; set; }

        public String Ref1 { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
