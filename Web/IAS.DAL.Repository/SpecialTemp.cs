using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    [Serializable]
    public class SpecialTemp
    {
        public string ID_CARD_NO { get; set; }
        public string SPECIAL_TYPE_CODE { get; set; }
        public DateTime? START_DATE { get; set; }
        public DateTime? END_DATE { get; set; }
        public DateTime? SEND_DATE { get; set; }
        public string SEND_BY { get; set; }
        public string USER_ID { get; set; }
        public DateTime? USER_DATE { get; set; }
        public string SEND_YEAR { get; set; }
        public string UNI_CODE { get; set; }
        public string UNI_NAME { get; set; }
        public string STATUS { get; set; }
        public string TRAIN_DISCOUNT_STATUS { get; set; }
        public string EXAM_DISCOUNT_STATUS { get; set; }
    }
}
