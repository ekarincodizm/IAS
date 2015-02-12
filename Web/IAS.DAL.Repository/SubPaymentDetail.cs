using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    [Serializable]
    public class SubPaymentDetail
    {
        public string ID_CARD_NO { get; set; }
        public string TESTING_NO { get; set; }
        public string COMPANY_CODE { get; set; }
        public string EXAM_PLACE_CODE { get; set; }
        public string Click { get; set; }
        public string HEAD_REQUEST_NO { get; set; }
        public string PAYMENT_NO { get; set; }
        public string RECORD_STATUS { get; set; }

        public string SEQ_NO { get; set; }
        public string SEQ_OF_SUBGROUP { get; set; }
        public string UPLOAD_GROUP_NO { get; set; }
        public string RECEIPT_NO { get; set; }
    }
}
