using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    public class LicensePaymentRequest
    {
        public String PETITION_TYPE_CODE { get; set; }
        public String ID_CARD_NO { get; set; }
        public String LICENSE_TYPE_CODE { get; set; }
        public String LICENSE_NO { get; set; }
        public String COMP_CODE { get; set; }
        public DateTime? RECEIPT_DATE { get; set; }
        public String RECEIPT_NO { get; set; }
        public DateTime? EXPIRATION_DATE { get; set; }
        public String PAYMENT_NO { get; set; }
        public String AREA { get; set; }
        public String REQUEST_NO { get; set; }
        public String UPLOAD_GROUP_NO { get; set; }
        public String SEQ_NO { get; set; }

    }
}
