using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    [Serializable]
    public class TrainPersonHistory
    {
        public string TRAIN_CODE { get; set; }
        public string ID_CARD_NO { get; set; }
        public string LICENSE_TYPE_CODE { get; set; }
        public string LICENSE_TYPE_NAME { get; set; }
        public Int16? TRAIN_TIMES { get; set; }
        public DateTime? TRAIN_DATE { get; set; }
        public DateTime? TRAIN_DATE_EXP { get; set; }
        public string STATUS { get; set; }
        public Int16? HOURS { get; set; }
        public Int16? TRAIN_PERIOD { get; set; }
        public string RESULT_DESC { get; set; }


        public Int16? PILAR_1 { get; set; }
        public Int16? PILAR_2 { get; set; }
        public Int16? PILAR_3 { get; set; }
        public string TRAIN_TYPE { get; set; }
        public string RESULT { get; set; }
        public string LICENSE_NO { get; set; }
    }
}
