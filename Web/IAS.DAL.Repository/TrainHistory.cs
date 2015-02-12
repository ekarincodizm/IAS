using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    [Serializable]
    public class TrainHistory
    {
        public string LICENSE_TYPE_CODE { get; set; }
        public string LICENSE_TYPE_NAME { get; set; }
        public Int16 TRAIN_TIMES { get; set; }
        public DateTime TRAIN_DATE { get; set; }
        public DateTime? TRAIN_DATE_EXP { get; set; }
        public string STATUS { get; set; }
        public Int16 HOURS { get; set; }
        public string RESULT_DESC { get; set; }

    }
}
