using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    [Serializable]
    public class Tran3To7
    {
        public string SPECIAL_TYPE_CODE { get; set; }
        public string SPECIAL_TYPE_DESC { get; set; }
        public DateTime? START_DATE { get; set; }
        public DateTime? END_DATE { get; set; }
        public DateTime? SEND_DATE { get; set; }
        public string SEND_BY { get; set; }
        public string ID_CARD_NO { get; set; }
    }
}
