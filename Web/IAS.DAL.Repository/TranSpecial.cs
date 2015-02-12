using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    public class TranSpecial
    {
        public string SPECIAL_TYPE_CODE { get; set; }
        public string SPECIAL_TYPE_DESC { get; set; }
        public string USED_TYPE { get; set; }

        public string ID_CARD_NO { get; set; }
        public string START_DATE { get; set; }
        public DateTime? END_DATE { get; set; }
        public DateTime? SEND_DATE { get; set; }
        public string SEND_BY { get; set; }
        public string USER_ID { get; set; }
        public DateTime? USER_DATE { get; set; }
        public string SEND_YEAR { get; set; }
        public string UNI_CODE { get; set; }
        public string UNI_NAME { get; set; }
    }
}
