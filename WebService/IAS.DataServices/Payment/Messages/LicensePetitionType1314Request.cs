using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAS.DataServices.Payment.Messages
{
    public class LicensePetitionType1314Request
    {
        public String SEQ_NO { get; set; }
        public String UPLOAD_GROUP_NO { get; set; }
        public String PETITION_TYPE_CODE { get; set; }
        public DateTime RECEIPT_DATE { get; set; }
        public String RECEIPT_NO { get; set; }
        public String REQUEST_NO { get; set; }
        public String PAYMENT_NO { get; set; }
        public String LICENSE_NO { get; set; }   

        public String AREA { get; set; }

        public DateTime EXPIRATION_DATE
        {
            get
            {
                if(PETITION_TYPE_CODE=="13")
                    return RECEIPT_DATE.AddYears(1).AddSeconds(-1) ;
                else
                    return RECEIPT_DATE.AddYears(5).AddSeconds(-1);
            }
        }

    }
}