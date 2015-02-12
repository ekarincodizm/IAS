using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    public class UnitLink
    {
        public string ID_CARD_NO { get; set; }
        public string LICENSE_TYPE_CODE { get; set; }
        public string LICENSE_TYPE_NAME { get; set; }
        public Int16 TRAIN_TIMES { get; set; }
        public DateTime TRAIN_DATE { get; set; }

    }
}
