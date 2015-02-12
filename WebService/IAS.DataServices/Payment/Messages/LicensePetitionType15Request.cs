using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAS.DataServices.Payment.Messages
{
    public class LicensePetitionType15Request  
    {                              
        public String SEQ_NO { get; set; }
        public String UPLOAD_GROUP_NO { get; set; }
        public String LICENSE_TYPE_CODE { get; set; }
        public String ID_CARD_NO { get; set; }
        public String COMP_CODE { get; set; }
        public DateTime RECEIPT_DATE { get; set; }
        public String RECEIPT_NO { get; set; }
        public String REQUEST_NO { get; set; }
        public String PAYMENT_NO { get; set; }

        public DateTime EXPIRATION_DATE
        {
            get
            {
                return RECEIPT_DATE.AddYears(1).AddSeconds(-1) ;
            }
        }
    }
}